using System;
using System.Data;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using SVCF_BusinessAccessLayer;
using SVCF_TransactionLayer;
using SVCF_DataAccessLayer;
using log4net;
using log4net.Config;

namespace SreeVisalamChitFundLtd_phase1
{
    public partial class Transfer_Suggestor : System.Web.UI.Page
    {
        #region Object
        BusinessLayer balayer = new BusinessLayer();
        TransactionLayer trn = new TransactionLayer();
        #endregion

        ILog logger = log4net.LogManager.GetLogger(typeof(Transfer_Suggestor));

        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (Session["UserName"] == null || Session["Branchid"] == null || Session["BranchName"] == null)
            {
                Response.Redirect(Page.ResolveUrl("~/Login.aspx"), true);
            }
        }
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
                DataTable dtChitGroupNo = balayer.GetDataTable("select GroupNo,Head_Id from groupmaster where isFinished='0' and BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]));
                DataRow dr = dtChitGroupNo.NewRow();
                dr[0] = "--Select--";
                dr[1] = "0";
                ddlChitGroupNo.DataValueField = "Head_Id";
                ddlChitGroupNo.DataTextField = "GroupNo";
                ddlChitGroupNo.DataSource = dtChitGroupNo;
                dtChitGroupNo.Rows.InsertAt(dr, 0);
                ddlChitGroupNo.DataBind();
                member();
                LoadbranchList();

                DataTable dtMonicollectorName = balayer.GetDataTable("select moneycollid, Concat(replace(moneycollname,'|',''),'|',replace(moneycolladdress,'|','')) as moneycollname  from moneycollector where BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]));
                DataRow dr2 = dtMonicollectorName.NewRow();
                dr2[0] = "0";
                dr2[1] = "--Select--";
                ddlMonyCollector_Name.DataValueField = "moneycollid";
                ddlMonyCollector_Name.DataTextField = "moneycollname";
                ddlMonyCollector_Name.DataSource = dtMonicollectorName;
                dtMonicollectorName.Rows.InsertAt(dr2, 0);
                ddlMonyCollector_Name.DataBind();
                lblCheck.Text = "Approval";
                lblCheck.Visible = false;
            }
            logger.Info("Transfer Suggestor - at: " + DateTime.Now + " by: " + Convert.ToString(Session["UserName"]) + "");
        }
        
        void member()
        {
            //if (chkLOad.Checked)
            //{
            //    DataTable dtNewMember = balayer.GetDataTable("select cast( MemberIDNew as char) as MemberIDNew,concat(MemberID, ' | ',replace(CustomerName,'|',''),'|',replace(AddressForCommunication,'|','')) as CustomerName  from membermaster where CustomerName<>'Sree Visalam Chit Fund Ltd' and TypeOfMember<>'Foreman'");
            //    DataRow dr1 = dtNewMember.NewRow();
            //    dr1[0] = "--Select--";
            //    dr1[1] = "--Select--";
            //    string memberID = balayer.GetSingleValue("SELECT MemberIDNew FROM svcf.membermaster where CustomerName='Sree Visalam Chit Fund Ltd' and TypeOfMember='Foreman' and BranchId=" + Session["Branchid"]);
            //    DataRow dr2 = dtNewMember.NewRow();
            //    dr2[0] = memberID;
            //    dr2[1] = "Foreman|Sree Visalam Chit Fund Ltd";
            //    ddlNewMember.DataValueField = "MemberIDNew";
            //    ddlNewMember.DataTextField = "CustomerName";
            //    ddlNewMember.DataSource = dtNewMember;
            //    dtNewMember.Rows.InsertAt(dr1, 0);
            //    dtNewMember.Rows.InsertAt(dr2, 1);
            //    ddlNewMember.DataBind();
            //}
            //else
            //{
                DataTable dtNewMember = balayer.GetDataTable("select cast(MemberIDNew as char) as MemberIDNew,concat(MemberID, ' | ',replace(CustomerName,'|',''),'|',replace(AddressForCommunication,'|','')) as CustomerName  from membermaster where CustomerName<>'Sree Visalam Chit Fund Ltd' and TypeOfMember<>'Foreman' and BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]));
                DataRow dr1 = dtNewMember.NewRow();
                dr1[0] = "--Select--";
                dr1[1] = "--Select--";
                string memberID = balayer.GetSingleValue("SELECT MemberIDNew FROM svcf.membermaster where CustomerName='Sree Visalam Chit Fund Ltd' and TypeOfMember='Foreman' and BranchId=" + Session["Branchid"]);
                DataRow dr2 = dtNewMember.NewRow();
                dr2[0] = memberID;
                dr2[1] = "Foreman|Sree Visalam Chit Fund Ltd";
                ddlNewMember.DataValueField = "MemberIDNew";
                ddlNewMember.DataTextField = "CustomerName";
                ddlNewMember.DataSource = dtNewMember;
                dtNewMember.Rows.InsertAt(dr1, 0);
                dtNewMember.Rows.InsertAt(dr2, 1);
                ddlNewMember.DataBind();
            //}
        }
        protected void Page_PreRender(object sender, EventArgs e)
        {
            ViewState["CheckRefresh"] = Session["CheckRefresh"];
        }
        protected void ddlChitGroupNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlChitGroupNo.SelectedItem.Text == "--Select--")
            {
                ddlOldMember.Items.Clear();
                ddlOldMember.DataBind();
                txtCurrent_DrawNo.Text = "";
                return;
            }
            txtCurrent_DrawNo.Text = "";
            string tyoeOfChit = balayer.GetSingleValue("select ChitCategory from  groupmaster where Head_Id=" + ddlChitGroupNo.SelectedValue);
            string addDays = "";
            tyoeOfChit = tyoeOfChit.ToLower();
            tyoeOfChit = tyoeOfChit.Trim();
            if (tyoeOfChit == "monthly")
            {
                addDays = "31";
                // string currentDrawNo = balayer.GetSingleValue("SELECT ifnull(max(DrawNO),0) as CurrentDrawNo FROM `auctiondetails` where GroupID='CLV-22' and (date(AuctionDate)  between  date_sub(now(),interval " + addDays + " day) and date_add(now(),interval " + addDays + " day));");
                //txtCurrent_DrawNo.Text = currentDrawNo;
            }
            else
                if (tyoeOfChit == "trymonthly")
                {
                    addDays = "10";
                    // string currentDrawNo = balayer.GetSingleValue("SELECT ifnull(max(DrawNO),0) as CurrentDrawNo FROM `auctiondetails` where GroupID='CLV-22' and (date(AuctionDate)  between  date_sub(now(),interval " + addDays + " day) and date_add(now(),interval " + addDays + " day));");
                    // txtCurrent_DrawNo.Text = currentDrawNo;
                }
                else
                    if (tyoeOfChit == "fortnightly")
                    {
                        addDays = "15";
                    }
            string currentDrawNo = balayer.GetSingleValue("SELECT ifnull(max(DrawNO),0) as CurrentDrawNo FROM `auctiondetails` where GroupID=" + ddlChitGroupNo.SelectedValue + " and (date(AuctionDate)  between  date_sub(now(),interval " + addDays + " day) and date_add(now(),interval " + addDays + " day));");
            txtCurrent_DrawNo.Text = currentDrawNo;
            DataTable dtOldMember = balayer.GetDataTable("SELECT  Concat(cast(m1.MemberID as char),'|',cast(m1.Head_Id as char)) as GrpMemberID, Concat(m1.GrpMemberID,'|',m2.MemberID,' | ',replace(m1.MemberName,'|','') ,'|',replace(m1.MemberAddress,'|','')) as MemberName FROM membertogroupmaster as m1 join membermaster as m2 on (m1.MemberID=m2.MemberIDNew) where m1.GroupID=" + ddlChitGroupNo.SelectedValue + " and m1.BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + "");
            DataRow dr = dtOldMember.NewRow();
            dr[0] = "--Select--";
            dr[1] = "--Select--";
            ddlOldMember.DataValueField = "GrpMemberID";
            ddlOldMember.DataTextField = "MemberName";
            ddlOldMember.DataSource = dtOldMember;
            dtOldMember.Rows.InsertAt(dr, 0);
            ddlOldMember.DataBind();
        }
        protected void btnTransfer_Click(object sender, EventArgs e)
        {
            Page.Validate("ts");
            if (!Page.IsValid)
            {
                return;
            }
            if (Session["CheckRefresh"].ToString() != ViewState["CheckRefresh"].ToString())
            {
                return;
            }
            int iExist = int.Parse(balayer.GetSingleValue("SELECT ifnull(count(* ) ,0) as ECount FROM svcf.transfer_approval where Old_Member=" + ddlOldMember.SelectedItem.Value.Split('|')[0] + " and New_Member="+ddlNewMember.SelectedItem.Value+" and `GrpMemberID`=" + ddlOldMember.SelectedItem.Value.Split('|')[1] + " and Flag=0"));
            if (iExist > 0)
            {
                ModalPopupExtender1.PopupControlID = "Pnlmsg1";
                this.ModalPopupExtender1.Show();
                Pnlmsg1.Visible = true;
                lblT.Text = "Failed Status";
                lblContent.Text ="New Member : "+ddlNewMember.SelectedItem.Text.Split('|')[0]+" instead of Old Member : "+ddlOldMember.SelectedItem.Text.Split('|')[1]+" for the Token : "+ ddlOldMember.SelectedItem.Text.Split('|')[0].Trim() + " is already Suggested";
                lblContent.ForeColor = System.Drawing.Color.Red;
                return;
            }
            ModalPopupExtender1.PopupControlID = "Pnlmsg1";
            this.ModalPopupExtender1.Show();
            Pnlmsg1.Visible = true;
            lblT.Text = "Status ";
            lblContent.Text = "Do you want to Suggest Token : " + ddlOldMember.SelectedItem.Text.Split('|')[0].Trim() + "'s New Member ; " + ddlNewMember.SelectedItem.Text.Split('|')[0] + " instead of Old Member : " + ddlOldMember.SelectedItem.Text.Split('|')[1] + "!!!!!!";
            lblContent.ForeColor = System.Drawing.Color.Green;
            lblCheck.Text = "Accept";
        }

        //protected void chkLOad_OnCheckedChanged(object sender, EventArgs e)
        //{
        //    member();
        //}
        
        protected void btnok_Click(object sender, EventArgs e)
        {
            if (lblT.Text == "Failed Status")
            {
                Pnlmsg1.Visible = false;
                ddlOldMember.Focus();
                return;
            }
            if (lblCheck.Text == "Accept")
            {
                TransactionLayer trn = new TransactionLayer();
                bool isFailed = true;
                try
                {
                    if (ddlBranchName.SelectedItem.Text == "--Select--")
                    {
                        long result = trn.insertorupdateTrn("insert into transfer_approval (BranchID,ChitGroup,CurrentDrawNo,GrpMemberID,Old_Member,New_Member,kasaramount,EstimatedDrawNoForAuction,EstimatedSuretyDetails,Commission,M_ID,Reason,SuggestedDate,B_ID,Monthlyincome,ProfessionBusiness,TransferAmount) values (" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + "," + ddlChitGroupNo.SelectedValue + "," + txtCurrent_DrawNo.Text + "," + ddlOldMember.SelectedItem.Value.Split('|')[1] + "," + ddlOldMember.SelectedItem.Value.Split('|')[0] + "," + ddlNewMember.SelectedItem.Value + "," + txtKasar.Text + ",'" + balayer.MySQLEscapeString(txtEstimated_DrawNo_for_Auction.Text) + "','" + balayer.MySQLEscapeString(txtEstimated_Surety_Details.Text) + "'," + txtCommision.Text + "," + ddlMonyCollector_Name.SelectedItem.Value + ",'" + balayer.MySQLEscapeString(txtReason.Text) + "','" + balayer.indiandateToMysqlDate(txtSuggestionDate.Text) + "'," +  balayer.ToobjectstrEvenNull(Session["Branchid"]) + "," + txtIncome.Text + ",'" + balayer.MySQLEscapeString(txtProfession.Text) + "'," + txtTransfer.Text + ")");
                    }
                    else
                    {
                        string memberBranch = balayer.GetSingleValue("SELECT BranchID FROM svcf.membermaster where MemberIDNew=" + ddlNewMember.SelectedItem.Value);

                        long result = trn.insertorupdateTrn("insert into transfer_approval (BranchID,ChitGroup,CurrentDrawNo,GrpMemberID,Old_Member,New_Member,kasaramount,EstimatedDrawNoForAuction,EstimatedSuretyDetails,Commission,M_ID,Reason,SuggestedDate,B_ID,Monthlyincome,ProfessionBusiness,TransferAmount) values (" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + "," + ddlChitGroupNo.SelectedValue + "," + txtCurrent_DrawNo.Text + "," + ddlOldMember.SelectedItem.Value.Split('|')[1] + "," + ddlOldMember.SelectedItem.Value.Split('|')[0] + "," + ddlNewMember.SelectedItem.Value + "," + txtKasar.Text + ",'" + balayer.MySQLEscapeString(txtEstimated_DrawNo_for_Auction.Text) + "','" + balayer.MySQLEscapeString(txtEstimated_Surety_Details.Text) + "'," + txtCommision.Text + "," + ddlMonyCollector_Name.SelectedItem.Value + ",'" + balayer.MySQLEscapeString(txtReason.Text) + "','" + balayer.indiandateToMysqlDate(txtSuggestionDate.Text) + "'," + memberBranch + "," + txtIncome.Text + ",'" + balayer.MySQLEscapeString(txtProfession.Text) + "'," + txtTransfer.Text + ")");
                    }
                    
                    trn.CommitTrn();
                    logger.Info("Transfer_suggestor.aspx - btnok_click():  Completed: " + DateTime.Now + " by: " + Convert.ToString(Session["UserName"]) + "");
                }
                catch (Exception error)
                {
                    try
                    {
                       trn.RollbackTrn();
                       logger.Info("Transfer_suggestor.aspx - btnok_click():  Error: " + error.Message + ": " + DateTime.Now + " by: " + Convert.ToString(Session["UserName"]) + "");
                    }
                    catch (Exception ex)
                    {

                    }
                    finally
                    {
                        isFailed = false;
                        ModalPopupExtender1.PopupControlID = "Pnlmsg1";
                        this.ModalPopupExtender1.Show();
                        Pnlmsg1.Visible = true;
                        lblT.Text = "Error";
                        lblContent.Text = error.Message;
                        lblContent.ForeColor = System.Drawing.Color.Red;
                        lblCheck.Text = "Rej";
                    }
                }
                finally
                {
                    trn.DisposeTrn();
                    if (isFailed)
                    {
                        ModalPopupExtender1.PopupControlID = "Pnlmsg1";
                        this.ModalPopupExtender1.Show();
                        Pnlmsg1.Visible = true;
                        lblT.Text = "Status ";
                        lblContent.Text = ddlNewMember.SelectedItem.Text.Split('|')[0] + " is Suggested instead of " + ddlOldMember.SelectedItem.Text.Split('|')[1].Trim() + " Successfully for the Token " + ddlOldMember.SelectedItem.Text.Split('|')[0];
                        lblContent.ForeColor = System.Drawing.Color.Green;
                        lblCheck.Text = "Rej";
                    }
                }
            }
            else if (lblCheck.Text == "Rej")
            {
                Response.Redirect(Request.Url.AbsolutePath);
            }

        }
        
        protected void btncancel_Click(object sender, EventArgs e)
        {
            if (lblT.Text == "Failed Status")
            {
                Pnlmsg1.Visible = false;
                ddlOldMember.Focus();
                return;
            }
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
                member();
            }
            else
            {
                //dtmember = balayer.GetDataTable("select concat(CustomerName,'|',`AddressForCommunication`) as CustomerName,MemberIDNew from membermaster where Branchid=" + ddlBranchName.SelectedValue + " order by CustomerName ");
                dtmember = balayer.GetDataTable("select cast(MemberIDNew as char) as MemberIDNew,concat(MemberID, ' | ',replace(CustomerName,'|',''),'|',replace(AddressForCommunication,'|','')) as CustomerName  from membermaster where CustomerName<>'Sree Visalam Chit Fund Ltd' and TypeOfMember<>'Foreman' and Branchid=" + ddlBranchName.SelectedValue + " order by CustomerName ");
                ddlNewMember.DataSource = dtmember;
                DataRow dr = dtmember.NewRow();
                dr[0] = "0";
                dr[1] = "--Select--";
                ddlNewMember.DataTextField = "CustomerName";
                ddlNewMember.DataValueField = "MemberIDNew";
                dtmember.Rows.InsertAt(dr, 0);
                ddlNewMember.DataBind();

                //Load Money Collector List
                DataTable dtMonicollectorName = balayer.GetDataTable("select moneycollid, Concat(replace(moneycollname,'|',''),'|',replace(moneycolladdress,'|','')) as moneycollname  from moneycollector where BranchID=" + ddlBranchName.SelectedValue);
                DataRow dr2 = dtMonicollectorName.NewRow();
                dr2[0] = "0";
                dr2[1] = "--Select--";
                ddlMonyCollector_Name.DataValueField = "moneycollid";
                ddlMonyCollector_Name.DataTextField = "moneycollname";
                ddlMonyCollector_Name.DataSource = dtMonicollectorName;
                dtMonicollectorName.Rows.InsertAt(dr2, 0);
                ddlMonyCollector_Name.DataBind();

            }
        }
    }
}
