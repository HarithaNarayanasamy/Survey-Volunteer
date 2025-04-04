using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using SVCF_BusinessAccessLayer;
using SVCF_DataAccessLayer;
using SVCF_TransactionLayer;

namespace SreeVisalamChitFundLtd_phase1
{
    public partial class Transfer_Suggestor_Approval : System.Web.UI.Page
    {
        #region Object
        BusinessLayer balayer = new BusinessLayer();
        TransactionLayer trn = new TransactionLayer();
        #endregion
        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (Session["UserName"] == null || Session["Branchid"] == null || Session["BranchName"] == null)
            {
                Response.Redirect(Page.ResolveUrl("~/Login.aspx"), true);
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
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
                GroupDetails();
            }
        }
        protected void exit_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.Url.AbsolutePath.ToString());
        }
        
        protected void Page_PreRender(object sender, EventArgs e)
        {
            ViewState["CheckRefresh"] = Session["CheckRefresh"];
        }
        protected void btnInfo_yes_Click(object sender, EventArgs e)
        {
            
            if (Session["CheckRefresh"].ToString() != ViewState["CheckRefresh"].ToString())
            {
                return;
            }
           
            try
            {
                lblSuggession.Visible = false;
                txtSuggession.Visible = false;
                if (lblHint.Text == "Approve")
                {
                    lblHint.Text = "";
                    GridViewRow gvRow = (GridViewRow)Session["Row"];
                    string MemberID = balayer.ToobjectstrEvenNull(GridView1.DataKeys[gvRow.RowIndex]["New_Member"]);
                    string oldMember = balayer.ToobjectstrEvenNull(GridView1.DataKeys[gvRow.RowIndex]["Old_Member"]);
                    string GrpMemberId = balayer.ToobjectstrEvenNull(GridView1.DataKeys[gvRow.RowIndex]["GrpMemberID"]);
                    string date = balayer.ToobjectstrEvenNull(GridView1.DataKeys[gvRow.RowIndex]["SuggestedDate"]);
                    //DataTable dtMemberNameandAddress=balayer.GetDataTable("SELECT CustomerName,ResidentialAddress FROM svcf.membermaster where MemberIDNew="+MemberID);
                    //DataTable dtNomineeNameandAddress = balayer.GetDataTable("SELECT NomineeName,NomineeAddress FROM svcf.nomineetable where GrpMemberID=" + GrpMemberId + " and MemberID=" + MemberID + " and Flag=1 order by sLno desc");
                    string strquery = "update transfer_approval set RejectedDate='" + balayer.indiandateToMysqlDate(date) + "',ReasonforRejection='Another chit is transfered',Flag=2  where ChitGroup=" + ddlChitgroupNo.SelectedItem.Value + " and GrpMemberID=" + GrpMemberId + " and Old_Member=" + oldMember;
                    string strquery1 = "update transfer_approval set RejectedDate=null,ReasonforRejection=null,Flag=1,IsTranasfered=0,ApprovedDate='" + balayer.indiandateToMysqlDate(date) + "',Suggessionifany='" + balayer.MySQLEscapeString(txtSuggession.Text) + "' where ChitGroup=" + ddlChitgroupNo.SelectedItem.Value + " and GrpMemberID=" + GrpMemberId + " and Old_Member=" + oldMember + " and New_Member=" + MemberID;
                    //string strUpdateMembertoGroupMaster = "update membertogroupmaster set `MemberName`='" + balayer.MySQLEscapeString(balayer.ToobjectstrEvenNull(dtMemberNameandAddress.Rows[0]["CustomerName"])) + "',`MemberID`=" + MemberID + ",`MemberAddress`='" + balayer.MySQLEscapeString(balayer.ToobjectstrEvenNull(dtMemberNameandAddress.Rows[0]["ResidentialAddress"])) + "',`NomineeName`='" + balayer.MySQLEscapeString(balayer.ToobjectstrEvenNull(dtNomineeNameandAddress.Rows[0]["NomineeName"])) + "',`NomineeAddress`='" + balayer.MySQLEscapeString(balayer.ToobjectstrEvenNull(dtNomineeNameandAddress.Rows[0]["NomineeAddress"])) + "',`card`=" + balayer.ToobjectstrEvenNull(GridView1.DataKeys[gvRow.RowIndex]["card"]) + ",`EstCallNoOfAuction`='" + balayer.MySQLEscapeString(balayer.ToobjectstrEvenNull(GridView1.DataKeys[gvRow.RowIndex]["EstimatedDrawNoForAuction"])) + "',`EstSuretyDocument`='" + balayer.MySQLEscapeString(balayer.ToobjectstrEvenNull(GridView1.DataKeys[gvRow.RowIndex]["EstimatedSuretyDetails"])) + "',`M_Id`=" + balayer.MySQLEscapeString(balayer.ToobjectstrEvenNull(GridView1.DataKeys[gvRow.RowIndex]["M_ID"])) + ",`PoolNo`=null,B_Id=" + balayer.MySQLEscapeString(balayer.ToobjectstrEvenNull(GridView1.DataKeys[gvRow.RowIndex]["B_ID"])) + ",headofficesuggession='"+balayer.MySQLEscapeString(txtSuggession.Text)+"' where Head_Id=" + GrpMemberId;
                    //string strUpdateNominee = "update svcf.nomineetable set Flag=0 where GrpMemberID="+GrpMemberId+" and MemberID="+MemberID+" and Flag=1";
                    long insert=trn.insertorupdateTrn(strquery);
                    long insert1 = trn.insertorupdateTrn(strquery1);
                    //long insertUpdateNominee = trn.insertorupdateTrn(strUpdateNominee);
                    lblMsgInfoContent.Text = "Approval Finished Successfullly!!!";
                    lblMsgInfoContent.ForeColor = System.Drawing.Color.Green;
                    ModalPopupExtender1.PopupControlID = "msgbox";
                    msgbox.Visible = true;
                    ModalPopupExtender1.Show();
                    MemberDetails();
                }
                else
                    if (lblHint.Text == "Reject")
                    {
                        GridViewRow gvRow = (GridViewRow)Session["Row"];
                        string slno = balayer.ToobjectstrEvenNull(GridView1.DataKeys[gvRow.RowIndex]["SlNo"]);
                        
                        string date = balayer.ToobjectstrEvenNull(GridView1.DataKeys[gvRow.RowIndex]["SuggestedDate"]);
                        
                        string strquery = "update transfer_approval set RejectedDate='" + balayer.indiandateToMysqlDate(date) + "',Flag=2,ReasonforRejection='" + balayer.MySQLEscapeString(txtSuggession.Text) + "'  where ChitGroup=" + ddlChitgroupNo.SelectedItem.Value + " and SlNo=" + slno;
                        long insert = trn.insertorupdateTrn(strquery);
                        lblMsgInfoContent.Text = "Rejected Finished Successfullly!!!";
                        lblMsgInfoContent.ForeColor = System.Drawing.Color.Green;
                        
                        ModalPopupExtender1.PopupControlID = "msgbox";
                        msgbox.Visible = true;
                        ModalPopupExtender1.Show();
                        MemberDetails();
                        lblHint.Text = "";
                    }
                    else
                    {
                        Response.Redirect(Request.Url.AbsolutePath.ToString());
                    }
                trn.CommitTrn();
            }
            catch (Exception error)
            {
                try
                {
                    trn.RollbackTrn();
                }
                catch (Exception ex)
                { }
                finally
                {

                    ModalPopupExtender1.PopupControlID = "msgbox";
                    msgbox.Visible = true;
                    ModalPopupExtender1.Show();
                    lblMsgInfoContent.Text = error.Message;
                    lblMsgInfoContent.ForeColor = System.Drawing.Color.Red;
                    MemberDetails();
                    lblHint.Text = "";
                }
            }
            finally
            {
                trn.DisposeTrn();
            }
        }
        public void MemberDetails()
        {
            string strQueryable = "";

            strQueryable = "SELECT t1.BranchID,b1.`B_Name`AS `BranchName`, g1.`GROUPNO` as `ChitGroup`, t1.`CurrentDrawNO` as DrawNumber, mg1.`GrpMemberID` as Token, concat(m2.MemberID,' | ',m2.CustomerName)  as  `OldMemberName`, m2.AddressForCommunication  as  `OldMemberAddress`, concat(m1.MemberID,' | ',m1.CustomerName)  as  `NewMemberName`, m1.AddressForCommunication  as  `NewMemberAddress`, t1.`EstimatedDrawNoForAuction`, t1.`EstimatedSuretyDetails`, t1.`Commission`, mc1.`moneycollname` as `MoneyCollectorName`, DATE_FORMAT( t1.`SuggestedDate`, '%d/%m/%Y') as SuggestedDate, DATE_FORMAT( t1.`TransferredDate`, '%d/%m/%Y') as TransferredDate,t1.`New_Member`,t1.ChitGroup,t1.B_ID,b2.B_Name as `SuggestedBranchName`,t1.`RejectedDate`,t1.Reason,t1.`Old_Member`,t1.GrpMemberID,t1.M_ID,t1.ApprovedDate,t1.IsTranasfered,t1.Flag,t1.SlNo,t1.kasaramount,t1.Monthlyincome,t1.ProfessionBusiness,t1.TransferAmount FROM svcf.transfer_approval as t1 join groupmaster as g1 on (t1.ChitGroup=g1.Head_Id) join membermaster as m1 on (t1.New_Member=m1.MemberIDNew) left join membermaster as m2 on (t1.Old_Member=m2.MemberIDNew) join membertogroupmaster as mg1 on (t1.GrpMemberID=mg1.Head_Id) join moneycollector as mc1 on( t1.M_ID=mc1.`moneycollid`) join `branchdetails` as b1 on(t1.BranchID=b1.Head_Id) join `branchdetails` as b2 on(t1.B_ID=b2.Head_Id) where t1.`ChitGroup`=" + ddlChitgroupNo.SelectedItem.Value + " and t1.Flag=0";
            DataTable dt = balayer.GetDataTable(strQueryable);
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }
        public void GroupDetails()
        {
            DataTable dtChitGroupNo = balayer.GetDataTable("select distinct ta1.ChitGroup,gm1.GROUPNO FROM transfer_approval as ta1 join  groupmaster as gm1 on ta1.ChitGroup=gm1.Head_Id where Flag=0");
            ddlChitgroupNo.DataValueField = "ChitGroup";
            ddlChitgroupNo.DataTextField = "GROUPNO";
            DataRow dr = dtChitGroupNo.NewRow();
            dr[0] = "0";
            dr[1] = "--Select--";
            ddlChitgroupNo.DataSource = dtChitGroupNo;
            dtChitGroupNo.Rows.InsertAt(dr, 0);
            ddlChitgroupNo.DataBind();
        }
        protected void btnLoad_Click(object sender, EventArgs e)
        {
            if (ddlChitgroupNo.SelectedItem.Text == "--Select--")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "javascript:alert('Please Choose The Chit Group!!!')", true);
                return;
            }
            else
            {
                MemberDetails();
            }
        }
        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            MemberDetails();
        }
        protected void btnOk_Click(object sender, EventArgs e)
        {
            ModalPopupExtender1.Hide();
        }
        protected void Audit_Click(object sender, EventArgs e)
        {
            ImageButton btndetails = sender as ImageButton;
            GridViewRow gvRow = (GridViewRow)btndetails.NamingContainer;
            string memID = balayer.ToobjectstrEvenNull(GridView1.DataKeys[gvRow.RowIndex]["New_Member"]);
            DataTable dt = balayer.GetDataTable("SELECT m1.Head_Id,m1.GroupID FROM svcf.membertogroupmaster as m1 join groupmaster as g1 on (m1.GroupID=g1.Head_Id) where  m1.MemberID=" + memID + "  and g1.IsFinished=0");
            DataTable dtM = new DataTable();
            dtM.Columns.Add("Token", typeof(string));
            dtM.Columns.Add("Arrear Amount", typeof(string));
            dtM.Columns.Add("Status", typeof(string));
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataTable dtDetails = balayer.GetDataTable("select mg1.GrpMemberID, mg1.MemberName as `MemberName`, sum(case when (v1.Voucher_Type='C' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) -(case when (v1.Voucher_Type='D' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) as paidAmount, vgwd1.TotaldueAmount, (case when( tp1.PaymentDate is not null ) then 'Prized' else 'Non-Prized' end) as status,(case when(sum(case when (v1.Voucher_Type='C' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) -(case when (v1.Voucher_Type='D' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end)<vgwd1.TotaldueAmount) then (vgwd1.TotaldueAmount-sum(case when (v1.Voucher_Type='C' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) -(case when (v1.Voucher_Type='D' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end)) else 0.00 end)  as ArrearAmount from membertogroupmaster as mg1 join voucher as v1 on v1.Head_Id=mg1.Head_Id join view_groupwisedue as vgwd1 on vgwd1.`GroupId`=" + dt.Rows[i]["GroupID"] + " left join trans_payment as tp1 on v1.Head_Id =tp1.TokenNumber where mg1.GroupID=" + dt.Rows[i]["GroupID"] + " and v1.Head_Id in (" + dt.Rows[i]["Head_Id"] + " ) and v1.ChoosenDate<= '" + DateTime.Now.ToString("yyyy-MM-dd") + "' and v1.head_id=" + dt.Rows[i]["Head_Id"] + " group by v1.Head_Id order by cast(digits(mg1.GrpMemberID) as unsigned);");
                if (dtDetails.Rows.Count > 0)
                {
                    DataRow dr = dtM.NewRow();
                    dr["Token"] = dtDetails.Rows[0]["GrpMemberID"];
                    dr["Arrear Amount"] = dtDetails.Rows[0]["ArrearAmount"];
                    dr["Status"] = dtDetails.Rows[0]["status"];
                    dtM.Rows.Add(dr.ItemArray);
                }
            }
            gvAudit.DataSource = dtM;
            gvAudit.DataBind();
            ModalPopupExtender1.PopupControlID = "pnAudit";
            ModalPopupExtender1.Show();
            pnAudit.Visible = true;
            
        }


        protected void Approve_Click(object sender, ImageClickEventArgs e)
        {
            lblHint.Text = "Approve";
            lblSuggession.Visible = true;
            txtSuggession.Visible = true;
            lblSuggession.Text = "Suggession if any";
            ImageButton btndetails = sender as ImageButton;
            GridViewRow gvrow = (GridViewRow)btndetails.NamingContainer;
            Session["Row"] = gvrow;
            string strCustomerName = balayer.GetSingleValue("SELECT CustomerName FROM svcf.membermaster where MemberIDNew=" + balayer.ToobjectstrEvenNull(GridView1.DataKeys[gvrow.RowIndex]["New_Member"]));
            string strToken = balayer.GetSingleValue("SELECT GrpMemberID FROM svcf.membertogroupmaster where Head_Id=" + balayer.ToobjectstrEvenNull(GridView1.DataKeys[gvrow.RowIndex]["GrpMemberID"]));
            lblMsgInfoContent.Text = " Do You Want to Approve " + strCustomerName + " for the Token : "+strToken+" !!!";
            lblMsgInfoContent.ForeColor = System.Drawing.Color.Green;
            ModalPopupExtender1.PopupControlID = "msgbox";
            ModalPopupExtender1.Show();
            msgbox.Visible = true;
        }
        protected void dis_Approve_Click(object sender, ImageClickEventArgs e)
        {
            lblSuggession.Visible = true;
            txtSuggession.Visible = true;
            lblSuggession.Text = "Reason for Rejection";
            lblHint.Text = "Reject";
            ImageButton btndetails = sender as ImageButton;
            GridViewRow gvrow = (GridViewRow)btndetails.NamingContainer;
            Session["Row"] = gvrow;
            string strCustomerName = balayer.GetSingleValue("SELECT CustomerName FROM svcf.membermaster where MemberIDNew=" + balayer.ToobjectstrEvenNull(GridView1.DataKeys[gvrow.RowIndex]["New_Member"]));
            string strToken = balayer.GetSingleValue("SELECT GrpMemberID FROM svcf.membertogroupmaster where Head_Id=" + balayer.ToobjectstrEvenNull(GridView1.DataKeys[gvrow.RowIndex]["GrpMemberID"]));
            lblMsgInfoContent.Text = " Do You Want to Reject " + strCustomerName + " for the Token : " + strToken + " !!!";
            lblMsgInfoContent.ForeColor = System.Drawing.Color.Green;
            // cnt1.Attributes["class"] = "info";
            ModalPopupExtender1.PopupControlID = "msgbox";
            ModalPopupExtender1.Show();
            msgbox.Visible = true;
        }
    }
}
