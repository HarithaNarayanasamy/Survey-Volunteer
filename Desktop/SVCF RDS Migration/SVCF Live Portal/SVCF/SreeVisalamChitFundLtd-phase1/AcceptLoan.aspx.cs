using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SVCF_BusinessAccessLayer;
using SVCF_TransactionLayer;
using SVCF_DataAccessLayer;
using log4net;
using log4net.Config;

namespace SreeVisalamChitFundLtd_phase1
{
    public partial class AcceptLoan : System.Web.UI.Page
    {
        #region VarDeclaration
        BusinessLayer balayer = new BusinessLayer();
        TransactionLayer trn = new TransactionLayer();
        #endregion
        ILog logger = log4net.LogManager.GetLogger(typeof(AcceptLoan));
        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (Session["UserName"] == null || Session["Branchid"] == null || Session["BranchName"] == null)
            {
                Response.Redirect(Page.ResolveUrl("~/Login.aspx"), false);
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                Session["CheckRefresh"] = System.Guid.NewGuid().ToString();
                DataTable dtAcc = GetData(0);
                DataTable dtCloned = dtAcc.Clone();
                foreach (DataRow row in dtAcc.Rows)
                {
                    dtCloned.ImportRow(row);
                }
                GridView1.DataSource = dtCloned;
                GridView1.DataBind();
            }
        }
        protected void Page_PreRender(object sender, EventArgs e)
        {
            ViewState["CheckRefresh"] = Session["CheckRefresh"];
        }
        protected void GridView1_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (ddlStatus.SelectedIndex != 0)
                {
                    //ImageButton imgBtn = (ImageButton)e.Row.FindControl("btnundo");
                    //imgBtn.Visible = true;
                    ImageButton imgBtn1 = (ImageButton)e.Row.FindControl("btnview");
                    imgBtn1.Visible = false;
                }
                else
                {
                    //ImageButton imgBtn = (ImageButton)e.Row.FindControl("btnundo");
                    //imgBtn.Visible = false;
                    ImageButton imgBtn1 = (ImageButton)e.Row.FindControl("btnview");
                    imgBtn1.Visible = true;
                }
            }
        }
        protected void btnLoad_OnClick(object sender, EventArgs e)
        {
            if (ddlStatus.SelectedItem.Text == "Waiting")
            {
                DataTable dtAcc = GetData(0);
                DataTable dtCloned = dtAcc.Clone();
                dtCloned.Columns["ApplyedOn"].DataType = typeof(string);
                //dtCloned.Columns["ApprovedOn"].DataType = typeof(string);
                foreach (DataRow row in dtAcc.Rows)
                {
                    dtCloned.ImportRow(row);
                }

                GridView1.DataSource = dtCloned;
                GridView1.DataBind();
            }
            else if (ddlStatus.SelectedItem.Text == "Accepted")
            {
                DataTable dtAcc = GetData(1);
                DataTable dtCloned = dtAcc.Clone();
                dtCloned.Columns["ApplyedOn"].DataType = typeof(string);
                dtCloned.Columns["ApprovedOn"].DataType = typeof(string);
                foreach (DataRow row in dtAcc.Rows)
                {
                    dtCloned.ImportRow(row);
                }

                GridView1.DataSource = dtCloned;
                GridView1.DataBind();
            }
            else if (ddlStatus.SelectedItem.Text == "Rejected")
            {
                DataTable dtAcc = GetData(2);
                DataTable dtCloned = dtAcc.Clone();
                dtCloned.Columns["ApplyedOn"].DataType = typeof(string);
                dtCloned.Columns["ApprovedOn"].DataType = typeof(string);
                foreach (DataRow row in dtAcc.Rows)
                {
                    dtCloned.ImportRow(row);
                }
                GridView1.DataSource = dtCloned;
                GridView1.DataBind();
            }
        }
        protected void btnRejectOk_Click(object sender, EventArgs e)
        {
           // TransactionLayer trn = new TransactionLayer();
            try
            {
                PnlApprove.Visible = false;
                GridViewRow gvrow = (GridViewRow)Session["RowAcceptLoan"];
                string aid = balayer.ToobjectstrEvenNull(GridView1.DataKeys[gvrow.RowIndex]["aid"]);
                trn.insertorupdateTrn("update `svcf`.`transloan` set `transloan`.`Flag`=2 where  aid=" + aid);
                ModalPopupExtender1.PopupControlID = "PnlApprove";
                ModalPopupExtender1.Show();
                lblContent.Text = "Loan Rejected";
                PnlApprove.Visible = true;
                btnRejectOk.Visible = false;
                btnMsgOK.Visible = false;
                btnMsgCancel.Visible = true;
                btnMsgCancel.Text = "Ok";
                trn.CommitTrn();
                logger.Info("AcceptLoan.aspx - btnRejectOk_Click() - Completed: " + DateTime.Now + " by: " + Convert.ToString(Session["UserName"]) + "");
            }
            catch (Exception error)
            {
                try
                {
                    trn.RollbackTrn();
                }
                catch(Exception ex)
                {
                    logger.Info("AcceptCourt.aspx - btnRejectOk_Click() - Error: " + ex.Message + ": " + DateTime.Now + " by: " + Convert.ToString(Session["UserName"]) + "");
                
                }
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

        protected void btnReject_OnClick(object sender, EventArgs e)
        {
            if (Session["CheckRefresh"].ToString() != ViewState["CheckRefresh"].ToString())
            {
                return;
            }
            pnlView.Visible = false;
            lblContent.Text = "Do you want to Reject!!!";
            ModalPopupExtender1.PopupControlID = "PnlApprove";
            ModalPopupExtender1.Show();
            PnlApprove.Visible = true;
            btnRejectOk.Visible = true;
            btnRejectOk.Text = "Yes";
            btnMsgOK.Visible = false;
            btnMsgCancel.Visible = true;
            btnMsgCancel.Text = "No";
        }
        protected void Approve_Click(object sender, EventArgs e)
        {
            if (Session["CheckRefresh"].ToString() != ViewState["CheckRefresh"].ToString())
            {
                return;
            }
            pnlView.Visible = false;
            GridViewRow gvrow = (GridViewRow)Session["RowAcceptLoan"];
            string HeadID = balayer.ToobjectstrEvenNull(GridView1.DataKeys[gvrow.RowIndex]["LoanHeadID"]);
            string Head = balayer.ToobjectstrEvenNull(GridView1.DataKeys[gvrow.RowIndex]["ID"]);
            string Loan = balayer.ToobjectstrEvenNull(GridView1.DataKeys[gvrow.RowIndex]["loan"]);
            string Name = balayer.ToobjectstrEvenNull(GridView1.DataKeys[gvrow.RowIndex]["Name"]);
            string TotalAmount = balayer.ToobjectstrEvenNull(GridView1.DataKeys[gvrow.RowIndex]["LoanAmount"]);
            string ssss = balayer.GetSingleValue("SELECT sum(LoanAmount) FROM svcf.transloan where LoanHeadID=" + HeadID +" and Flag=1");
            if (ssss == "")
            {
                ssss = "0.00";
            }
            Label21.Text = "Accept";
            lbHeadandHeadid.Text = "Loan : " + Loan + " , Id : " + Head + " , Name : " + Name + " , Amount : Rs." + TotalAmount + " Total Amount(includes previous Amount) : Rs. " + ssss;
            lblAccept.Text = "If you want to accept the Loan, Please enter the Loan Number.";
            ModalPopupExtender1.PopupControlID = "PnlAccept";
            ModalPopupExtender1.Show();
            PnlAccept.Visible = true;
            btnAcceptOK.Visible = true;
            btnAcceptOK.Text = "Ok";
            btnRejectCancel.Visible = true;
            btnRejectCancel.Text = "Cancel";
        }
        protected void dis_Approve_Click(object sender, EventArgs e)
        {
            ImageButton btndetails = sender as ImageButton;
            GridViewRow gvRow = (GridViewRow)btndetails.NamingContainer;
            Session["RowIndex"] = gvRow.RowIndex;
            ModalPopupExtender1.PopupControlID = "PnlReject";
            ModalPopupExtender1.Show();
        }

        protected DataTable GetData(int aaaa)
        {
            return balayer.GetDataTable("select `branchdetails`.`B_Name`,(case when (`transloan`.`loantype`=0) then 'Chit Loan' else 'Employee Loan' end) as loan,(case when(`transloan`.`loantype`=0) then  `membertogroupmaster`.`GrpMemberID` else '-' end) as ID,membermaster.MemberID as MemberIDNEW,(case when(`transloan`.`loantype`=0) then concat(membermaster.MemberID,' | ', `membertogroupmaster`.`MemberName`) else `employee_details`.`Emp_Name` end) as `Name`, `transloan`.`GroupID` AS `GroupID`, `transloan`.`GroupMemberID` AS `GroupMemberID`, `transloan`.`EmployeeID` AS `EmployeeID`, `transloan`.`MemberID` AS `MemberID`, `transloan`.`LoanHeadID` AS `LoanHeadID`, `transloan`.`LoanAmount` AS `LoanAmount`, `transloan`.`ApplyedOn` AS `ApplyedOn`, `transloan`.`AdminSanctionNo` AS `AdminSanctionNo`, `transloan`.`Narration` AS `Narration`, `transloan`.`Trans_Medium` AS `Trans_Medium`, `transloan`.`BranchID` AS `BranchID`, `transloan`.`Flag` AS `Flag`, `transloan`.`loantype` AS `loantype`,`transloan`.`BankHeadId` as BankHeadId, `transloan`.`aid` AS `aid` from `transloan` left join `membertogroupmaster` on (`transloan`.`GroupMemberID`=`membertogroupmaster`.`Head_Id`) left join membermaster on (transloan.MemberID=membermaster.MemberIDNew) left join `svcf`.`employee_details` on  (`transloan`.`EmployeeID`=`employee_details`.`Emp_ID`)  join `svcf`.`branchdetails` on (`transloan`.`BranchID`=`branchdetails`.`Head_Id`)  where `transloan`.`Flag`=" + aaaa + " and `transloan`.`ApplyedOn`='" + balayer.indiandateToMysqlDate(txtDate.Text) + "'");
        }
        protected void btnview_OnClick(object sender, EventArgs e)
        {
            ImageButton btndetails = sender as ImageButton;
            GridViewRow gv = (GridViewRow)btndetails.NamingContainer;
            Session["RowAcceptLoan"] = gv;

            string loantype = balayer.ToobjectstrEvenNull(GridView1.DataKeys[gv.RowIndex]["loantype"]);
            if (loantype == "0")
            {
                DataTable dt = balayer.GetDataTable("SELECT MemberID,GroupMemberID FROM svcf.transloan where aid=" + GridView1.DataKeys[gv.RowIndex]["aid"]);
                DataTable dtM = balayer.GetDataTable("SELECT t1.loannumber,sum(t1.LoanAmount),(case when(t1.GroupMemberID in (SELECT Head_ID FROM svcf.membertogroupmaster where MemberID=" + dt.Rows[0]["MemberID"] + " and Head_Id not in (select PrizedMemberID from auctiondetails where MemberID=" + dt.Rows[0]["MemberID"] + "))) then m1.GrpMemberID else '' end) as NonprizedNo,sum(case when(t1.GroupMemberID in (SELECT Head_ID FROM svcf.membertogroupmaster where MemberID=" + dt.Rows[0]["MemberID"] + " and Head_Id not in (select PrizedMemberID from auctiondetails where MemberID=" + dt.Rows[0]["MemberID"] + "))) then t1.LoanAmount else 0.00 end) as Nonprized,(case when(t1.GroupMemberID in (SELECT Head_ID FROM svcf.membertogroupmaster where MemberID=" + dt.Rows[0]["MemberID"] + " and Head_Id in (select PrizedMemberID from auctiondetails where MemberID=" + dt.Rows[0]["MemberID"] + "))) then m1.GrpMemberID else '' end) as prizedno,sum(case when(t1.GroupMemberID in (SELECT Head_ID FROM svcf.membertogroupmaster where MemberID=" + dt.Rows[0]["MemberID"] + " and Head_Id in (select PrizedMemberID from auctiondetails where MemberID=" + dt.Rows[0]["MemberID"] + "))) then t1.LoanAmount else 0.00 end) as prized FROM svcf.transloan as t1 join membertogroupmaster as m1 on (t1.GroupMemberID=m1.Head_Id) where t1.MemberID=" + dt.Rows[0]["MemberID"] + " and t1.Flag=1 and t1.istransfered=1 group by t1.GroupMemberID");
                if (dtM.Rows.Count != 0)
                {
                    gridOverview.DataSource = dtM;
                    gridOverview.DataBind();
                    lbNoRecords.Visible = false;
                        gridOverview.Visible=true;
                }
                else
                {
                    lbNoRecords.Text = "No Previous Transaction Available";
                    gridOverview.Visible = false;
                    lbNoRecords.Visible = true;
                }
            }
            else
            {
                DataTable dt = balayer.GetDataTable("SELECT EmployeeID FROM svcf.transloan where aid=" + GridView1.DataKeys[gv.RowIndex]["aid"]);
                DataTable dtM = balayer.GetDataTable("SELECT e1.Emp_Name,sum(t1.LoanAmount) FROM svcf.transloan as t1 join employee_details as e1 on (t1.EmployeeID=e1.Emp_Id) where t1.EmployeeID=" + dt.Rows[0]["EmployeeID"]);
                if (dtM.Rows.Count != 0)
                {
                    gridOverview.DataSource = dtM;
                    gridOverview.DataBind();
                    lbNoRecords.Visible = false;
                    gridOverview.Visible = true;
                }
                else
                {
                    lbNoRecords.Text = "No Previous Transaction Available";
                    gridOverview.Visible = false;
                    lbNoRecords.Visible = true;
                }
            }
        
            lbViewHeader.Text = "Loan";
            ModalPopupExtender1.PopupControlID = "pnlView";
            ModalPopupExtender1.Show();
            pnlView.Visible = true;
        }

        protected void btnRejectCancel_Click(object sender, EventArgs e)
        {

        }
        protected void btnundo_OnClick(object sender, EventArgs e)
        {
            ImageButton btndetails = sender as ImageButton;
            GridViewRow gv = (GridViewRow)btndetails.NamingContainer;
            Session["RowAcceptLoan"] = gv;
            int Transmedium = Convert.ToInt32(GridView1.DataKeys[gv.RowIndex]["Flag"]);
            if (Transmedium == 1)
            {
                LbUNdoText.Text = "Do you want to undo Accepted Loan!!!";
                ModalPopupExtender1.PopupControlID = "pnlUndo";
                ModalPopupExtender1.Show();
                pnlUndo.Visible = true;
                UndoConfirmation.Visible = true;
                Button3.Visible = true;
                UndoConfirmation.Text = "Yes";
                Button3.Text = "No";
            }
            else if (Transmedium == 2)
            {
                LbUNdoText.Text = "Do you want to undo Rejected Loan!!!";
                ModalPopupExtender1.PopupControlID = "pnlUndo";
                ModalPopupExtender1.Show();
                pnlUndo.Visible = true;
                UndoConfirmation.Visible = true;
                Button3.Visible = true;
                UndoConfirmation.Text = "Yes";
                Button3.Text = "No";
            }
        }

        protected void UndoConfirmation_OnClick(object sender, EventArgs e)
        {
            TransactionLayer trn = new TransactionLayer();
            try
            {
                GridViewRow gvRow = (GridViewRow)Session["RowAcceptLoan"];
                int Transmedium = Convert.ToInt32(GridView1.DataKeys[gvRow.RowIndex]["Flag"]);
                if (Transmedium == 1)
                {
                    pnlUndo.Visible = false;
                    string DualTransactionKey = balayer.ToobjectstrEvenNull(GridView1.DataKeys[gvRow.RowIndex]["DualTransactionKey"]);
                    trn.insertorupdateTrn("update svcf.transloan set TransactionKey=null,Flag=0,loannumber=null,ApprovedOn=null where DualTransactionKey=" + DualTransactionKey);
                    LbUNdoText.Text = "Accepted Loan Successfully undo.";
                    ModalPopupExtender1.PopupControlID = "pnlUndo";
                    ModalPopupExtender1.Show();
                    pnlUndo.Visible = true;
                    UndoConfirmation.Visible = false;
                    Button3.Visible = true;
                    UndoConfirmation.Text = "Yes";
                    Button3.Text = "Ok";
                }
                else if (Transmedium == 2)
                {
                    pnlUndo.Visible = false;
                    string DualTransactionKey = balayer.ToobjectstrEvenNull(GridView1.DataKeys[gvRow.RowIndex]["DualTransactionKey"]);
                    trn.insertorupdateTrn("update svcf.transloan set Flag=0,ReasonforRejection=null where DualTransactionKey=" + DualTransactionKey);
                    LbUNdoText.Text = "Rejected Loan Successfully undo.";
                    ModalPopupExtender1.PopupControlID = "pnlUndo";
                    ModalPopupExtender1.Show();
                    pnlUndo.Visible = true;
                    UndoConfirmation.Visible = false;
                    Button3.Visible = true;
                    UndoConfirmation.Text = "Yes";
                    Button3.Text = "Ok";
                }
                trn.CommitTrn();
                logger.Info("AcceptLoan.aspx - UndoConfirmation_OnClick() - Completed: " + DateTime.Now + " by: " + Convert.ToString(Session["UserName"]) + "");
            }
            catch (Exception error)
            {
                try
                {
                   trn.RollbackTrn();
                }
                catch (Exception ex)
                {
                    logger.Info("AcceptLoan.aspx - UndoConfirmation_OnClick() - Error: " + ex.Message + ": " + DateTime.Now + " by: " + Convert.ToString(Session["UserName"]) + "");
                }
                finally {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Warning", "alert('" + error.Message.ToString().Replace("'", "\\'") + "');", true);
                }
            }
            finally
            {
                trn.DisposeTrn();
            }
        }

        protected void btnAccept_Click(object sender, EventArgs e)
        {
            TransactionLayer trn = new TransactionLayer();
            try
            {
                Page.Validate("Rej");
                if (!Page.IsValid)
                {
                    return;
                }
                PnlAccept.Visible = false;
                DataTable dt = balayer.GetDataTable("SELECT * FROM svcf.transloan where loannumber=" + txtAccept.Text + "");
                if (dt.Rows.Count > 0)
                {
                    lblContent.Text = "Loan Number Already Exists!!!";
                    ModalPopupExtender1.PopupControlID = "PnlApprove";
                    ModalPopupExtender1.Show();
                    PnlApprove.Visible = true;
                    btnMsgOK.Visible = false;
                    btnRejectOk.Visible = false;
                    btnMsgCancel.Visible = true;
                    btnMsgCancel.Text = "Ok";
                }
                else
                {
                    GridViewRow gvRow = (GridViewRow)Session["RowAcceptLoan"];
                    string aid = balayer.ToobjectstrEvenNull(GridView1.DataKeys[gvRow.RowIndex]["aid"]);
                    //string TransactionKey = balayer.GetSingleValue("SELECT TransactionKey FROM svcf.voucher where DualTransactionKey=" + DualTransactionKey + " and Voucher_Type='D'");
                    trn.insertorupdateTrn("update transloan set loannumber=" + txtAccept.Text + ",Flag=1,ApprovedOn='"+DateTime.Now.ToString("yyyy-MM-dd")+"' where aid=" + aid);
                    lblContent.Text = "Loan Accepted!!!";
                    ModalPopupExtender1.PopupControlID = "PnlApprove";
                    ModalPopupExtender1.Show();
                    PnlApprove.Visible = true;
                    btnMsgOK.Visible = false;
                    btnRejectOk.Visible = false;
                    btnMsgCancel.Visible = true;
                    btnMsgCancel.Text = "Ok";
                }
                trn.CommitTrn();
                logger.Info("AcceptLoan.aspx - btnAccept_Click() - Completed: " + DateTime.Now + " by: " + Convert.ToString(Session["UserName"]) + "");

            }
            catch (Exception error)
            {
                try
                {
                   trn.RollbackTrn();
                }
                catch (Exception ex) {
                    logger.Info("AcceptLoan.aspx - btnAccept_Click() - Error: " + ex.Message + ": " + DateTime.Now + " by: " + Convert.ToString(Session["UserName"]) + "");
                }
                finally {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Warning", "alert('" + error.Message.ToString().Replace("'", "\\'") + "');", true);
                }

            }
            finally {
                trn.DisposeTrn();
            }
        }
        protected void btnMsgCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.Url.AbsolutePath, false);

        }
        protected void btnMsgOK_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.Url.AbsolutePath, false);
        }
    }
}