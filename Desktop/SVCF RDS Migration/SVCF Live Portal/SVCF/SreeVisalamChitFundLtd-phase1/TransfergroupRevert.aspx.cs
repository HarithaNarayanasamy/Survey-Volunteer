using SVCF_BusinessAccessLayer;
using SVCF_TransactionLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SreeVisalamChitFundLtd_phase1
{
    public partial class TransfergroupRevert : System.Web.UI.Page
    {
        #region Object
        BusinessLayer balayer = new BusinessLayer();
        TransactionLayer trn = new TransactionLayer();
        #endregion
        string userinfo = "";
        string qry = "";
        string usrRole = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            Pnlmsg.Visible = false;
            if (!IsPostBack)
            {
                userinfo = HttpContext.Current.User.Identity.Name;
                qry = "select rs.name from roles as rs inner join rights as rt on (rt.roleid=rs.id) where memberid=" + userinfo + "";
                usrRole = balayer.GetSingleValue(qry);
                if (usrRole == "Administrator")
                {
                    DataTable ChitGrp = new DataTable();
                    ChitGrp = balayer.GetDataTable("select distinct(Head_Id),GROUPNO from svcf.transfer_approval as ta join svcf.groupmaster as gm on(ta.ChitGroup=gm.Head_Id) where ta.BranchID=" + (Session["Branchid"]) + "");
                    ddlGroupNo.DataSource = ChitGrp;
                    ddlGroupNo.DataValueField = "Head_Id";
                    ddlGroupNo.DataTextField = "GROUPNO";
                    ddlGroupNo.DataBind();
                    ddlGroupNo.Items.Insert(0, "--select--");
                }
                else
                {
                    Response.Redirect("Home.aspx", true);
                }
            }
        }
        protected void ddlGroupNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable Chit = new DataTable();
            Chit = balayer.GetDataTable("select mg.Head_Id,concat(mg.GrpMemberID,'|',mg.MemberName) as GrpMemberID from svcf.transfer_approval as ta join svcf.membertogroupmaster as mg on(ta.GrpMemberID=mg.Head_Id) join membermaster as mm on(ta.Old_Member=mm.MemberIDNew)  where ta.BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + "  and ta.ChitGroup=" + ddlGroupNo.SelectedValue + "");
            ddlChit.DataSource = Chit;
            ddlChit.DataValueField = "Head_Id";
            ddlChit.DataTextField = "GrpMemberID";
            ddlChit.DataBind();
            ddlChit.Items.Insert(0, "--select--");


        }
        protected void btnclose(object sender, EventArgs e)
        {
            clearGroup();
        }
        protected void clearGroup()
        {
            Response.Redirect(Request.Url.AbsolutePath.ToString());
        }
        protected void btnrevert_Click(object sender, EventArgs e)
        {
            Page.Validate("add");
            if (!Page.IsValid)
            {
                return;
            }

            DataTable dtvoucher = new DataTable();
            DataTable dtmember = new DataTable();
            string query = "";
            string membername = "";
            string Branchid = "";
            string memberid = "";
            string typeofmember = "";
            string groupid = "";
            string grpmemberid = "";
            string b_id = "";
            string m_id = "";
            string CustomerName = "";
            string residentialaddress = "";
            query = "select * from svcf.voucher where BranchID = " + balayer.ToobjectstrEvenNull(Session["Branchid"]) + "  and ChitGroupId = " + ddlGroupNo.SelectedValue + " and Head_Id=" + ddlChit.SelectedValue + "";
            dtvoucher = balayer.GetDataTable(query);
           // var  membermaster = balayer.GetDataTable("select * from membermaster   ");
          // List<Membermaster> membermaster1 = membermaster.DataTableToList<Membermaster>();
            if (dtvoucher.Rows.Count != 0)
            {
                query = "select * from svcf.transfer_approval as ta join membertogroupmaster as mg on(ta.New_Member=mg.MemberID) join membermaster as mm on(ta.Old_Member=mm.MemberIDNew) where ta.BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and ta.ChitGroup=mg.GroupID and ta.ChitGroup=" + ddlGroupNo.SelectedValue + " and ta.GrpMemberID=" + ddlChit.SelectedValue + "";
                dtmember = balayer.GetDataTable(query);
                if (dtmember.Rows.Count > 0)
                {
                    residentialaddress = dtmember.Rows[0]["ResidentialAddress"].ToString();
                    membername = dtmember.Rows[0]["MemberName"].ToString();
                    memberid = Convert.ToString(dtmember.Rows[0]["Old_Member"]);
                    CustomerName = dtmember.Rows[0]["CustomerName"].ToString();
                    Branchid = dtmember.Rows[0]["BranchId"].ToString();
                    typeofmember = dtmember.Rows[0]["TypeOfMember"].ToString();
                    if (membername != "" && typeofmember != "foreman")
                    {
                        string insertGroup = "update membertogroupmaster set `MemberName`='" + CustomerName + "',`MemberID`=" + memberid + ",`MemberAddress`='" + residentialaddress + "',B_Id=" + Branchid + " where Head_Id=" + balayer.ToobjectstrEvenNull(ddlChit.SelectedValue) ;
                        long incmd = trn.insertorupdateTrn(insertGroup);
                    }
                }
                ModalPopupExtender2.PopupControlID = "Pnlmsg";
                this.ModalPopupExtender2.Show();
                Pnlmsg.Visible = true;
                lblT.Text = "Status";
                lblContent.Text = "Group : " + ddlChit.SelectedItem.Text + " Revert Successfully";
                lblContent.ForeColor = System.Drawing.Color.Green;
                trn.CommitTrn();

            }
            else
            {
                ModalPopupExtender2.PopupControlID = "Pnlmsg";
                this.ModalPopupExtender2.Show();
                Pnlmsg.Visible = true;
                lblT.Text = "Status";
                lblContent.Text = "Already insert Entry";
                lblContent.ForeColor = System.Drawing.Color.Green;
                trn.CommitTrn();
            }

        }
    }
}