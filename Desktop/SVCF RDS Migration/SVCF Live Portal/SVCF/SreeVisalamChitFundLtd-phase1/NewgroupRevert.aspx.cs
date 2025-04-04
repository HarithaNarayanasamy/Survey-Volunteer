using SVCF_BusinessAccessLayer;
using SVCF_TransactionLayer;
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SreeVisalamChitFundLtd_phase1
{
    public partial class NewgroupRevert : System.Web.UI.Page
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
                    ChitGrp = balayer.GetDataTable("select GROUPNO,Head_Id from svcf.groupmaster where BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + "");
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
        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (Session["UserName"] == null || Session["Branchid"] == null || Session["BranchName"] == null)
            {
                Response.Redirect(Page.ResolveUrl("~/Login.aspx"), true);
            }
        }
        protected void ddlGroupNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable Chit = new DataTable();
            Chit = balayer.GetDataTable("select Head_Id,GrpMemberID from svcf.membertogroupmaster where BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + "  and GroupID=" + ddlGroupNo.SelectedValue + "");
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
            string headid = "";
            string groupid = "";
            string grpmemberid = "";
            string b_id = "";
            string m_id = "";
            query = "select * from svcf.voucher where BranchID = " + balayer.ToobjectstrEvenNull(Session["Branchid"]) + "  and ChitGroupId = " + ddlGroupNo.SelectedValue + " and Head_Id=" + ddlChit.SelectedValue + "";
            dtvoucher = balayer.GetDataTable(query);
            DataTable memeberid = balayer.GetDataTable("select * from membermaster where  BranchID = " + balayer.ToobjectstrEvenNull(Session["Branchid"]) + "");
            if (dtvoucher.Rows.Count == 0)
            {
                query = "select * from membertogroupmaster where BranchID = " + balayer.ToobjectstrEvenNull(Session["Branchid"]) + "  and GroupID = " + ddlGroupNo.SelectedValue + " and Head_Id=" + ddlChit.SelectedValue + "";

                dtmember = balayer.GetDataTable(query);
             
                membername = dtmember.Rows[0]["MemberName"].ToString();
                    if (membername != "")
                    {
                        string qq = "delete from membersuggestion  where  BranchID = " + balayer.ToobjectstrEvenNull(Session["Branchid"]) + "  and GroupNo = " + dtmember.Rows[0]["GroupID"] + " and MemberID=" + dtmember.Rows[0]["MemberID"] + "";
                        var up = trn.insertorupdateTrn(qq);
                        query = "delete from membertogroupmaster  where  BranchID = " + balayer.ToobjectstrEvenNull(Session["Branchid"]) + "  and GroupID = " + dtmember.Rows[0]["GroupID"] + " and Head_Id=" + dtmember.Rows[0]["Head_Id"] + "";
                        var update = trn.insertorupdateTrn(query);

                        string nomi = "delete from nomineetable  where    MemberID=" + dtmember.Rows[0]["MemberID"] + "   and GrpMemberID=" + dtmember.Rows[0]["Head_Id"] + "";
                        var updatenomi = trn.insertorupdateTrn(query);

                        string heads= "delete from headstree  where    NodeID=" + dtmember.Rows[0]["Head_Id"] + "";
                    var updatenomiheas = trn.insertorupdateTrn(heads);
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