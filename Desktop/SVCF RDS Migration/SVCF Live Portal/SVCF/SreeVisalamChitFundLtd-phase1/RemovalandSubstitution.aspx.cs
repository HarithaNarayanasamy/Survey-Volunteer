using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text.RegularExpressions;
using SVCF_BusinessAccessLayer;
using SVCF_DataAccessLayer;
using SVCF_TransactionLayer;
using log4net;
using log4net.Config;
namespace SreeVisalamChitFundLtd_phase1
{
    public partial class RemovalandSubstitution : System.Web.UI.Page
    {

        #region Object
        BusinessLayer balayer = new BusinessLayer();
        TransactionLayer trn = new TransactionLayer();
        #endregion

        ILog logger = log4net.LogManager.GetLogger(typeof(RemovalandSubstitution));
        protected void Page_Load(object sender, EventArgs e)
        {
            Pnlmsg1.Visible = false;
            if (!IsPostBack)
            {
                var userinfo = HttpContext.Current.User.Identity.Name;
                var qry = "select rs.name from roles as rs inner join rights as rt on (rt.roleid=rs.id) where memberid=" + userinfo + "";
                var usrRole = balayer.GetSingleValue(qry);
                if (usrRole == "Report")
                {
                    Response.Redirect("Home.aspx", false);
                }

                Session["CheckRefresh"] = System.Guid.NewGuid().ToString();
                txt1stNoticeDate.ToolTip = "Ex. " + DateTime.Now.ToString("dd/MM/yyyy") + " (dd/mm/yyyy)";
                txt2ndNoticeDate.ToolTip = "Ex. " + DateTime.Now.ToString("dd/MM/yyyy") + " (dd/mm/yyyy)";
                txtDateofRemoval.ToolTip = "Ex. " + DateTime.Now.ToString("dd/MM/yyyy") + " (dd/mm/yyyy)";
                select();
                LoadbranchList();
                ddlChitGroupNo.Focus();
            }
        }
        
        protected void Page_PreRender(object sender, EventArgs e)
        {
            ViewState["CheckRefresh"] = Session["CheckRefresh"];
        }
        
        void select()
        {
            DataTable dtChitGroupNo = balayer.GetDataTable("select GroupNo,Head_Id from groupmaster where isFinished=0 and BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]));
            DataRow dr = dtChitGroupNo.NewRow();
            dr[0] = "--select--";
            dr[1] = "0";
            ddlChitGroupNo.DataValueField = "Head_Id";
            ddlChitGroupNo.DataTextField = "GroupNo";
            dtChitGroupNo.Rows.InsertAt(dr, 0);
            ddlChitGroupNo.DataSource = dtChitGroupNo;
            ddlChitGroupNo.DataBind();
            
            DataTable dtNewMember;
            //if (!chkBranch.Checked)
            //{
                dtNewMember = balayer.GetDataTable("select cast(MemberIDNew as char) as MemberIDNew,concat(MemberID, ' | ',replace(CustomerName,'|',''),'|',replace(AddressForCommunication,'|','')) as CustomerName  from membermaster where CustomerName<>'Sree Visalam Chit Fund Ltd' and TypeOfMember<>'Foreman' and BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]));
            //}
            //else
            //{
                //dtNewMember = balayer.GetDataTable("select cast(MemberIDNew as char) as MemberIDNew,concat(MemberID, ' | ',replace(CustomerName,'|',''),'|',replace(AddressForCommunication,'|','')) as CustomerName  from membermaster where CustomerName<>'Sree Visalam Chit Fund Ltd' and TypeOfMember<>'Foreman'");
            //}
            DataRow dr1 = dtNewMember.NewRow();
            dr1[0] = "--Select--";
            dr1[1] = "--select--";
            string memberID = balayer.GetSingleValue("SELECT MemberIDNew FROM svcf.membermaster where CustomerName='Sree Visalam Chit Fund Ltd' and TypeOfMember='Foreman' and BranchId=" + Session["Branchid"]);
            DataRow dr2 = dtNewMember.NewRow();
            dr2[0] = memberID;
            dr2[1] = "Foreman|Sree Visalam Chit Fund Ltd";
            ddlNewMember.DataValueField = "MemberIDNew";
            ddlNewMember.DataTextField = "CustomerName";
            dtNewMember.Rows.InsertAt(dr1, 0);
            dtNewMember.Rows.InsertAt(dr2, 1);
            ddlNewMember.DataSource = dtNewMember;
            ddlNewMember.DataBind();
            

            DataTable dtMonicollectorName = balayer.GetDataTable("select moneycollid,Concat(moneycollname,'|',moneycolladdress) as moneycollname from moneycollector where BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]));
            DataRow dm = dtMonicollectorName.NewRow();
            dm[0] = "0";
            dm[1] = "--select--";
            ddlMonyCollector_Name.DataValueField = "moneycollid";
            ddlMonyCollector_Name.DataTextField = "moneycollname";
            dtMonicollectorName.Rows.InsertAt(dm, 0);
            ddlMonyCollector_Name.DataSource = dtMonicollectorName;
            ddlMonyCollector_Name.DataBind();
            
        }
        void loadmember()
        {
            //if (ddlChitGroupNo.SelectedItem.Text == "--select--")
            //{
            //    ddlOldMember.Items.Clear();
            //    ddlOldMember.DataBind();
            //    return;
            //}
            if (ddlChitGroupNo.SelectedItem.Text != "--select--")
            {
                txtCurrent_DrawNo.Text = "";
                string tyoeOfChit = balayer.GetSingleValue("select ChitCategory from  groupmaster where Head_Id=" + ddlChitGroupNo.SelectedItem.Value);
                string addDays = "";
                tyoeOfChit = tyoeOfChit.ToLower();
                tyoeOfChit = tyoeOfChit.Trim();
                //if ((tyoeOfChit == "Monthly") || (tyoeOfChit == "MONTHLY") || ((tyoeOfChit == "monthly")))
                if (tyoeOfChit == "monthly")
                {
                    addDays = "31";
                }
                else
                    if (tyoeOfChit == "trymonthly")
                    {
                        addDays = "10";
                    }
                    else
                        if (tyoeOfChit == "fortnightly")
                        {
                            addDays = "15";
                        }
                string currentDrawNo = balayer.GetSingleValue("SELECT ifnull(max(DrawNO),0) as CurrentDrawNo FROM `auctiondetails` where GroupID=" + ddlChitGroupNo.SelectedItem.Value + " and (date(AuctionDate)  between  date_sub(now(),interval " + addDays + " day) and date_add(now(),interval " + addDays + " day));");
                txtCurrent_DrawNo.Text = currentDrawNo;
                DataTable dtOldMember = balayer.GetDataTable("SELECT Concat(cast(m1.MemberID as char),'|',m1.Head_Id) as GrpMemberID, concat(m2.MemberID, ' |', replace( m1.GrpMemberID,'|',''),'|', Concat(replace(m1.MemberName,'|',''))) as MemberName FROM membertogroupmaster as m1 join membermaster as m2 on (m1.MemberID=m2.MemberIDNew) where m1.GroupID=" + ddlChitGroupNo.SelectedItem.Value + " and m1.BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + "");
                //ddlOldMember.DataValueField = "GrpMemberID";
                //ddlOldMember.DataTextField = "MemberName";
                //ddlOldMember.DataSource = dtOldMember;
                //ddlOldMember.DataBind();
                ListItem lioldMember = new ListItem("--select--", "--select--");
                //ddlOldMember.Items.Insert(0, lioldMember);
                //ddlOldMember.Focus();
                DropDownList1.Items.Clear();
                for (int i = 0; i < dtOldMember.Rows.Count; i++)
                {
                    DropDownList1.Items.Add(Convert.ToString(dtOldMember.Rows[i]["MemberName"]));
                }
               // DropDownList1.DataSource = dtOldMember;
               // DropDownList1.DataBind();
            }         
           
        }
        protected void ddlChitGroupNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadmember();
        }
       
        protected void btnTransfer_Click(object sender, EventArgs e)
        {
            Page.Validate("ras");
            if (!Page.IsValid)
            {
                return;
            }
            if (Session["CheckRefresh"].ToString() != ViewState["CheckRefresh"].ToString())
            {
                return;
            }

            ModalPopupExtender1.PopupControlID = "Pnlmsg1";
            this.ModalPopupExtender1.Show();
            Pnlmsg1.Visible = true;
            lblT.Text = "Status";
            btncancel.Visible = true;
            btnok.Text = "Yes";
            btncancel.Text = "No";
           // string ddlselectedval = "";
           // string ddlselectedtext = "";
            //string testingtext = "";
            //ddlselectedval = Convert.ToString(ddlOldMember.SelectedValue);
           // ddlselectedtext = ddlOldMember.SelectedItem.Text;
            string testingtext = "";
            testingtext = DropDownList1.SelectedItem.Text;
            lblContent.Text = "Do you want to suggest " + ddlNewMember.SelectedItem.Text.Split('|')[0].Trim() + " instead of " + testingtext.Split('|')[1] + " for the Token : " + testingtext.Split('|')[0];
            lblContent.ForeColor = System.Drawing.Color.Green;
        }

        protected void btnok_Click(object sender, EventArgs e)
        {
            TransactionLayer trn = new TransactionLayer();
            try
            {
                string gpmemid = DropDownList1.SelectedItem.Text.Split('|')[1];
                string oldmemid = balayer.GetSingleValue("SELECT memberid from membertogroupmaster where GroupID=" + ddlChitGroupNo.SelectedItem.Value + " and BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and GrpMemberID='" + gpmemid +"'"+ "");
                string oldhdid = balayer.GetSingleValue("SELECT Head_Id from membertogroupmaster where GroupID=" + ddlChitGroupNo.SelectedItem.Value + " and BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) +" and GrpMemberID='" + gpmemid +"'"+ "");
              
                if(ddlBranchName.SelectedItem.Text == "--Select--")
                {
                    long result = trn.insertorupdateTrn("insert into removal_approval (BranchID,ChitNO,OldMemberID,GroupMemberID,NewMemberID,DateOfRemoval,ReasonForRemoval,1stNoticeDate,2ndNoticeDate,CurrentDrawNumber,EstimatedDrawNumber,EstimatedSurityDetails,M_ID,B_ID,commision,kasar,removalamount,monthlyincome,profession) values (" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + "," + ddlChitGroupNo.SelectedItem.Value + "," + oldmemid + "," + oldhdid + "," + ddlNewMember.SelectedItem.Value + ",'" + balayer.indiandateToMysqlDate(txtDateofRemoval.Text) + "','" + balayer.MySQLEscapeString(txtReason.Text) + "','" + balayer.indiandateToMysqlDate(txt1stNoticeDate.Text) + "','" + balayer.indiandateToMysqlDate(txt2ndNoticeDate.Text) + "'," + txtCurrent_DrawNo.Text + ",'" + balayer.MySQLEscapeString(txtEstimated_DrawNo_for_Auction.Text) + "','" + balayer.MySQLEscapeString(txtEstimated_Surety_Details.Text) + "'," + ddlMonyCollector_Name.SelectedItem.Value + "," + balayer.ToobjectstrEvenNull(Session["Branchid"]) + "," + txtCommision.Text + "," + txtKasar.Text + "," + txtRemovalAmount.Text + "," + txtMonthlyIncome.Text + ",'" + balayer.MySQLEscapeString(balayer.ToobjectstrEvenNull(txtProfession.Text)) + "')");
                }
                else
                {

                
                string strOtherBranchMember = balayer.GetSingleValue("SELECT BranchId FROM svcf.membermaster where MemberIDNew=" + ddlNewMember.SelectedItem.Value);
                long result = trn.insertorupdateTrn("insert into removal_approval (BranchID,ChitNO,OldMemberID,GroupMemberID,NewMemberID,DateOfRemoval,ReasonForRemoval,1stNoticeDate,2ndNoticeDate,CurrentDrawNumber,EstimatedDrawNumber,EstimatedSurityDetails,M_ID,B_ID,commision,kasar,removalamount,monthlyincome,profession) values (" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + "," + ddlChitGroupNo.SelectedItem.Value + "," + oldmemid + "," + oldhdid + "," + ddlNewMember.SelectedItem.Value + ",'" + balayer.indiandateToMysqlDate(txtDateofRemoval.Text) + "','" + balayer.MySQLEscapeString(txtReason.Text) + "','" + balayer.indiandateToMysqlDate(txt1stNoticeDate.Text) + "','" + balayer.indiandateToMysqlDate(txt2ndNoticeDate.Text) + "'," + txtCurrent_DrawNo.Text + ",'" + balayer.MySQLEscapeString(txtEstimated_DrawNo_for_Auction.Text) + "','" + balayer.MySQLEscapeString(txtEstimated_Surety_Details.Text) + "'," + ddlMonyCollector_Name.SelectedItem.Value + "," + strOtherBranchMember + "," + txtCommision.Text + "," + txtKasar.Text + "," + txtRemovalAmount.Text + "," + txtMonthlyIncome.Text + ",'" + balayer.MySQLEscapeString(balayer.ToobjectstrEvenNull(txtProfession.Text)) + "')");
               
                }

                ModalPopupExtender1.PopupControlID = "Pnlmsg1";
                this.ModalPopupExtender1.Show();
                Pnlmsg1.Visible = true;
                lblT.Text = "Status";
                btnok.Visible = false;
                btncancel.Text = "Ok";
                lblContent.Text = ddlNewMember.SelectedItem.Text.Split('|')[0].Trim() + " is suggested for the Token " + DropDownList1.SelectedItem.Text.Split('|')[0];
                lblContent.ForeColor = System.Drawing.Color.Green;
                trn.CommitTrn();
                logger.Info("RemovalandSubstition.aspx - select():  Completed: " + DateTime.Now + " by: " + Convert.ToString(Session["UserName"]) + "");
            }
            catch (Exception error)
            {
                try
                {
                   trn.RollbackTrn();
                   logger.Info("RemovalandSubstition.aspx - select():  Error: " + error.Message + ": " + DateTime.Now + " by: " + Convert.ToString(Session["UserName"]) + "");
                }
                catch (Exception ex)
                { }
                finally
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Warning", "alert('" + error.Message.ToString().Replace("'", "\\'") + "');", true);
                }
            }
            finally 
            {
                trn.DisposeTrn();
            }
        }

        protected void btncancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.Url.AbsolutePath);
        }
        public void LoadbranchList()
        {
            ddlBranchName.DataSource = null;
            DataTable dtgroupno = null;
            dtgroupno = balayer.GetDataTable("select NodeID,Node from headstree where ParentID=1 and NodeID<>" + Session["Branchid"]);
            DataRow dr = dtgroupno.NewRow();
            dr[0] = "0";
            dr[1] = "--Select--";
            ddlBranchName.DataValueField = "NodeID";
            ddlBranchName.DataTextField = "Node";
            dtgroupno.Rows.InsertAt(dr, 0);
            ddlBranchName.DataSource = dtgroupno;
            ddlBranchName.DataBind();
        }

        protected void ddlBranchName_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlNewMember.DataSource = null;
            DataTable dtmember = null;
            if (ddlBranchName.SelectedItem.Text == "--Select--")
            {
                select();
            }
            else
            {
                //dtmember = balayer.GetDataTable("select concat(CustomerName,'|',`AddressForCommunication`) as CustomerName,MemberIDNew from membermaster where Branchid=" + ddlBranchName.SelectedValue + " order by CustomerName ");
                dtmember = balayer.GetDataTable("select cast(MemberIDNew as char) as MemberIDNew,concat(MemberID, ' | ',replace(CustomerName,'|',''),'|',replace(AddressForCommunication,'|','')) as CustomerName  from membermaster where CustomerName<>'Sree Visalam Chit Fund Ltd' and TypeOfMember<>'Foreman' and BranchID=" + ddlBranchName.SelectedValue + " order by CustomerName ");
                ddlNewMember.DataSource = dtmember;
                DataRow dr = dtmember.NewRow();
                dr[0] = "0";
                dr[1] = "--Select--";
                ddlNewMember.DataTextField = "CustomerName";
                ddlNewMember.DataValueField = "MemberIDNew";
                dtmember.Rows.InsertAt(dr, 0);
                ddlNewMember.DataBind();

                //Load Money collector for the branch
                DataTable dtMonicollectorName = balayer.GetDataTable("select moneycollid,Concat(moneycollname,'|',moneycolladdress) as moneycollname from moneycollector where BranchID=" + ddlBranchName.SelectedValue);
                DataRow dm = dtMonicollectorName.NewRow();
                dm[0] = "0";
                dm[1] = "--select--";
                ddlMonyCollector_Name.DataValueField = "moneycollid";
                ddlMonyCollector_Name.DataTextField = "moneycollname";
                dtMonicollectorName.Rows.InsertAt(dm, 0);
                ddlMonyCollector_Name.DataSource = dtMonicollectorName;
                ddlMonyCollector_Name.DataBind();
            }
        }
    }
}