using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SVCF_BusinessAccessLayer;
using SVCF_DataAccessLayer;
using SVCF_TransactionLayer;
using log4net;
using log4net.Config;

namespace SreeVisalamChitFundLtd_phase1
{
    public partial class loaninsertion : System.Web.UI.Page
    {
        #region Object
        BusinessLayer balayer = new BusinessLayer();
        #endregion

        ILog logger = log4net.LogManager.GetLogger(typeof(loaninsertion));

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                rvDate.MaximumValue = DateTime.Now.ToString("dd/MM/yyyy");
               // rvDate.MinimumValue = balayer.GetSingleValue("SELECT DATE_FORMAT( min(ChoosenDate), '%d/%m/%Y') MinimumDate FROM svcf.voucher where BranchID=" + Session["Branchid"]); ;
                rvDate.MinimumValue = balayer.GetSingleValue("SELECT DATE_FORMAT( min(ChoosenDate), '%d/%m/%Y') MinimumDate FROM svcf.voucher where " +
                    "BranchID=" + Session["Branchid"] + " and ChoosenDate<>'0000-00-00'");

                DataTable dt = balayer.GetDataTable("SELECT t1.aid ,(case when (t1.loantype=0) then concat( m1.GrpMemberID , ' | ',m2.MemberID ,' | ',m1.MemberName) else e1.Emp_Name end) as `name` FROM svcf.transloan as t1 left join membertogroupmaster as m1 on (t1.GroupMemberID=m1.Head_Id) left join membermaster as m2 on (t1.MemberID=m2.MemberIDNew) left join employee_details as e1 on (t1.EmployeeID=e1.Emp_ID) where t1.Flag=1 and t1.isTransfered=0 and t1.BranchID=" + Session["Branchid"]);
                DataRow dr = dt.NewRow();
                dr[0] = "0";
                dr[1] = "--Select--";
                ddlHead.DataTextField = "name";
                ddlHead.DataValueField = "aid";
                dt.Rows.InsertAt(dr, 0);
                ddlHead.DataSource = dt;
                ddlHead.DataBind();
                Session["CheckRefresh"] = System.Guid.NewGuid().ToString();
            }
        }
        protected void btncancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.Url.AbsolutePath.ToString());
        }
        protected void Page_PreRender(object sender, EventArgs e)
        {
            ViewState["CheckRefresh"] = Session["CheckRefresh"];
        }
        protected void ddlMedium_SelectedIndexChanged(object sender, EventArgs e)
        {
            bindmedium();
        }
        void bindmedium()
        {
            if (ddlMedium.SelectedValue.ToString() == "Bank")
            {
                lblBankHead.Visible = true;
                lblChequeNo.Visible = true;
                ddlBankHead.Visible = true;
                txtChequeNo.Visible = true;
                CVddlBankHead.Visible = true;
                RFVtxtIfsc.Visible = true;
                txtChequeNo.Focus();
                DataTable dt = balayer.GetDataTable("SELECT concat(BankName,' _ ',IFCCode,' _ ',AccountNo) as Bank,Head_Id FROM svcf.bankdetails where BranchID="+Session["Branchid"]);
                DataRow dr = dt.NewRow();
                dr[0] = "--Select--";
                dr[1] = "0";
                ddlBankHead.DataSource = dt;
                ddlBankHead.DataTextField = "Bank";
                ddlBankHead.DataValueField = "Head_Id";
                dt.Rows.InsertAt(dr, 0);
                ddlBankHead.DataBind();
            }
            else
            {
                lblBankHead.Visible = false;
                lblChequeNo.Visible = false;
                ddlBankHead.Visible = false;
                txtChequeNo.Visible = false;
                CVddlBankHead.Visible = false;
                RFVtxtIfsc.Visible = false;
                btnPayLoan.Focus();
            }
        }
        protected void btnPayLoan_Click(object sender, EventArgs e)
        {
            Page.Validate("chit");
            if (!Page.IsValid)
            {
                return;
            }
            if (Session["CheckRefresh"].ToString() != ViewState["CheckRefresh"].ToString())
            {
                return;
            }
            DataTable dtCheck = balayer.GetDataTable("SELECT * FROM svcf.transloan where loannumber="+txtLoanNumber.Text+" and aid="+ddlHead.SelectedItem.Value);
            if (dtCheck.Rows.Count == 1)
            {
                bool isFinished = true;
                TransactionLayer trn = new TransactionLayer();
                try
                {
                    DataTable dt = balayer.GetDataTable("SELECT insertkey_from_bin(DualTransactionKey) as DualTransactionKey,GroupID,GroupMemberID,EmployeeID,MemberID,LoanHeadID,LoanAmount,Narration,BranchID,loantype,loannumber FROM svcf.transloan where aid=" + ddlHead.SelectedItem.Value.Split('|')[0]);
                    if ( balayer.ToobjectstrEvenNull( dt.Rows[0]["loantype"]) == "0")
                    {
                        if (ddlMedium.SelectedItem.Text == "Cash")
                        {
                            long iResult = trn.insertorupdateTrn("insert into voucher(`DualTransactionKey`,`BranchID`,`CurrDate`,`Voucher_No`,`Voucher_Type`,`Head_Id`,`ChoosenDate`,`Narration`,`Amount`,`Series`,`ReceievedBy`,`Trans_Type`,`T_Day`,`T_Month`,`T_Year`,`MemberID`,`Trans_Medium`,`RootID`,ChitGroupId,`Other_Trans_Type`) values (" + dt.Rows[0]["DualTransactionKey"] + "," + Session["Branchid"] + ",'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "',0,'D'," + dt.Rows[0]["LoanHeadID"] + ",'" + balayer.indiandateToMysqlDate(txtDate.Text) + "','" + balayer.MySQLEscapeString(balayer.ToobjectstrEvenNull(dt.Rows[0]["Narration"])) + "'," + balayer.ToobjectstrEvenNull(dt.Rows[0]["LoanAmount"]) + ",'LOAN','" + Session["UserName"] + "',0," + txtDate.Text.Split('/')[0] + "," + txtDate.Text.Split('/')[1] + "," + txtDate.Text.Split('/')[2] + "," + balayer.ToobjectstrEvenNull(dt.Rows[0]["MemberID"]) + ",0,8,0,2)");
                            long iResult1 = trn.insertorupdateTrn("insert into voucher(`DualTransactionKey`,`BranchID`,`CurrDate`,`Voucher_No`,`Voucher_Type`,`Head_Id`,`ChoosenDate`,`Narration`,`Amount`,`Series`,`ReceievedBy`,`Trans_Type`,`T_Day`,`T_Month`,`T_Year`,`MemberID`,`Trans_Medium`,`RootID`,ChitGroupId,`Other_Trans_Type`) values (" + dt.Rows[0]["DualTransactionKey"] + "," + Session["Branchid"] + ",'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "',0,'C',12,'" + balayer.indiandateToMysqlDate(txtDate.Text) + "','" + balayer.MySQLEscapeString(balayer.ToobjectstrEvenNull(dt.Rows[0]["Narration"])) + "','" + balayer.ToobjectstrEvenNull(dt.Rows[0]["LoanAmount"]) + "','LOAN','" + Session["UserName"] + "',0," + txtDate.Text.Split('/')[0] + "," + txtDate.Text.Split('/')[1] + "," + txtDate.Text.Split('/')[2] + "," + balayer.ToobjectstrEvenNull(dt.Rows[0]["MemberID"]) + ",0,12,0,2)");
                            long uResult = trn.insertorupdateTrn("UPDATE svcf.transloan set TransactionKey=" + iResult + ",ChoosenDate='" + balayer.indiandateToMysqlDate(txtDate.Text) + "',Trans_Medium=0,isTransfered=1 where aid=" + ddlHead.SelectedItem.Value);
                        }
                        else if (ddlMedium.SelectedItem.Text == "Bank")
                        {
                            long iResult = trn.insertorupdateTrn("insert into voucher(`DualTransactionKey`,`BranchID`,`CurrDate`,`Voucher_No`,`Voucher_Type`,`Head_Id`,`ChoosenDate`,`Narration`,`Amount`,`Series`,`ReceievedBy`,`Trans_Type`,`T_Day`,`T_Month`,`T_Year`,`MemberID`,`Trans_Medium`,`RootID`,ChitGroupId,`Other_Trans_Type`) values (" + dt.Rows[0]["DualTransactionKey"] + "," + Session["Branchid"] + ",'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "',0,'D'," + dt.Rows[0]["LoanHeadID"] + ",'" + balayer.indiandateToMysqlDate(txtDate.Text) + "','" + balayer.MySQLEscapeString(balayer.ToobjectstrEvenNull(dt.Rows[0]["Narration"])) + "'," + balayer.ToobjectstrEvenNull(dt.Rows[0]["LoanAmount"]) + ",'LOAN','" + Session["UserName"] + "',0," + txtDate.Text.Split('/')[0] + "," + txtDate.Text.Split('/')[1] + "," + txtDate.Text.Split('/')[2] + "," + balayer.ToobjectstrEvenNull(dt.Rows[0]["MemberID"]) + ",0,8,0,2)");
                            long iResult1 = trn.insertorupdateTrn("insert into voucher(`DualTransactionKey`,`BranchID`,`CurrDate`,`Voucher_No`,`Voucher_Type`,`Head_Id`,`ChoosenDate`,`Narration`,`Amount`,`Series`,`ReceievedBy`,`Trans_Type`,`T_Day`,`T_Month`,`T_Year`,`MemberID`,`Trans_Medium`,`RootID`,ChitGroupId,`Other_Trans_Type`) values (" + dt.Rows[0]["DualTransactionKey"] + "," + Session["Branchid"] + ",'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "',0,'C'," + ddlBankHead.SelectedItem.Value + ",'" + balayer.indiandateToMysqlDate(txtDate.Text) + "','" + balayer.MySQLEscapeString(balayer.ToobjectstrEvenNull(dt.Rows[0]["Narration"])) + "'," + balayer.ToobjectstrEvenNull(dt.Rows[0]["LoanAmount"]) + ",'LOAN','" + Session["UserName"] + "',0," + txtDate.Text.Split('/')[0] + "," + txtDate.Text.Split('/')[1] + "," + txtDate.Text.Split('/')[2] + "," + balayer.ToobjectstrEvenNull(dt.Rows[0]["MemberID"]) + ",0,3,0,2)");
                            string strBankName = balayer.GetSingleValue("SELECT BankName FROM svcf.bankdetails where Head_Id=" + ddlBankHead.SelectedItem.Value);
                            long iResult2 = trn.insertorupdateTrn("insert into svcf.transbank (BranchID,DualTransactionKey,T_Day,T_Month,T_Year,BankHeadID,Head_Id,MemberID,CustomersBankName,DateInCheque,ChequeDDNO,GivenAmount,TotalChequeDDAmount,IsBounced,Trans_Type) values (" + Session["Branchid"] + "," + dt.Rows[0]["DualTransactionKey"] + "," + txtDate.Text.Split('/')[0] + "," + txtDate.Text.Split('/')[1] + "," + txtDate.Text.Split('/')[2] + "," + ddlBankHead.SelectedItem.Value + "," + dt.Rows[0]["LoanHeadID"] + "," + balayer.ToobjectstrEvenNull(dt.Rows[0]["MemberID"]) + ",'" + balayer.MySQLEscapeString(strBankName) + "','" + balayer.indiandateToMysqlDate(txtDate.Text) + "','" + txtChequeNo.Text + "'," + balayer.ToobjectstrEvenNull(dt.Rows[0]["LoanAmount"]) + "," + balayer.ToobjectstrEvenNull(dt.Rows[0]["LoanAmount"]) + ",0,0)");
                            long uResult = trn.insertorupdateTrn("UPDATE svcf.transloan set TransactionKey=" + iResult + ",ChoosenDate='" + balayer.indiandateToMysqlDate(txtDate.Text) + "',Trans_Medium=1,isTransfered=1 where aid=" + ddlHead.SelectedItem.Value);
                        }
                    }
                    else if (balayer.ToobjectstrEvenNull( dt.Rows[0]["loantype"]) == "1")
                    {
                        if (ddlMedium.SelectedItem.Text == "Cash")
                        {
                            long iResult = trn.insertorupdateTrn("insert into voucher(`DualTransactionKey`,`BranchID`,`CurrDate`,`Voucher_No`,`Voucher_Type`,`Head_Id`,`ChoosenDate`,`Narration`,`Amount`,`Series`,`ReceievedBy`,`Trans_Type`,`T_Day`,`T_Month`,`T_Year`,`MemberID`,`Trans_Medium`,`RootID`,ChitGroupId,`Other_Trans_Type`) values (" + dt.Rows[0]["DualTransactionKey"] + "," + Session["Branchid"] + ",'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "',0,'D'," + dt.Rows[0]["LoanHeadID"] + ",'" + balayer.indiandateToMysqlDate(txtDate.Text) + "','" + balayer.MySQLEscapeString(balayer.ToobjectstrEvenNull(dt.Rows[0]["Narration"])) + "'," + balayer.ToobjectstrEvenNull(dt.Rows[0]["LoanAmount"]) + ",'LOAN','" + Session["UserName"] + "',0," + txtDate.Text.Split('/')[0] + "," + txtDate.Text.Split('/')[1] + "," + txtDate.Text.Split('/')[2] + ",0,0,8,0,2)");
                            long iResult1 = trn.insertorupdateTrn("insert into voucher(`DualTransactionKey`,`BranchID`,`CurrDate`,`Voucher_No`,`Voucher_Type`,`Head_Id`,`ChoosenDate`,`Narration`,`Amount`,`Series`,`ReceievedBy`,`Trans_Type`,`T_Day`,`T_Month`,`T_Year`,`MemberID`,`Trans_Medium`,`RootID`,ChitGroupId,`Other_Trans_Type`) values (" + dt.Rows[0]["DualTransactionKey"] + "," + Session["Branchid"] + ",'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "',0,'C',12,'" + balayer.indiandateToMysqlDate(txtDate.Text) + "','" + balayer.MySQLEscapeString(balayer.ToobjectstrEvenNull(dt.Rows[0]["Narration"])) + "'," + balayer.ToobjectstrEvenNull(dt.Rows[0]["LoanAmount"]) + ",'LOAN','" + Session["UserName"] + "',0," + txtDate.Text.Split('/')[0] + "," + txtDate.Text.Split('/')[1] + "," + txtDate.Text.Split('/')[2] + ",0,0,12,0,2)");
                            long uResult = trn.insertorupdateTrn("UPDATE svcf.transloan set TransactionKey=" + iResult + ",ChoosenDate='" + balayer.indiandateToMysqlDate(txtDate.Text) + "',Trans_Medium=0,isTransfered=1 where aid=" + ddlHead.SelectedItem.Value);
                        }
                        else if (ddlMedium.SelectedItem.Text == "Bank")
                        {
                            long iResult = trn.insertorupdateTrn("insert into voucher(`DualTransactionKey`,`BranchID`,`CurrDate`,`Voucher_No`,`Voucher_Type`,`Head_Id`,`ChoosenDate`,`Narration`,`Amount`,`Series`,`ReceievedBy`,`Trans_Type`,`T_Day`,`T_Month`,`T_Year`,`MemberID`,`Trans_Medium`,`RootID`,ChitGroupId,`Other_Trans_Type`) values (" + dt.Rows[0]["DualTransactionKey"] + "," + Session["Branchid"] + ",'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "',0,'D'," + dt.Rows[0]["LoanHeadID"] + ",'" + balayer.indiandateToMysqlDate(txtDate.Text) + "','" + balayer.MySQLEscapeString(balayer.ToobjectstrEvenNull(dt.Rows[0]["Narration"])) + "'," + balayer.ToobjectstrEvenNull(dt.Rows[0]["LoanAmount"]) + ",'LOAN','" + Session["UserName"] + "',0," + txtDate.Text.Split('/')[0] + "," + txtDate.Text.Split('/')[1] + "," + txtDate.Text.Split('/')[2] + ",0,0,8,0,2)");
                            long iResult1 = trn.insertorupdateTrn("insert into voucher(`DualTransactionKey`,`BranchID`,`CurrDate`,`Voucher_No`,`Voucher_Type`,`Head_Id`,`ChoosenDate`,`Narration`,`Amount`,`Series`,`ReceievedBy`,`Trans_Type`,`T_Day`,`T_Month`,`T_Year`,`MemberID`,`Trans_Medium`,`RootID`,ChitGroupId,`Other_Trans_Type`) values (" + dt.Rows[0]["DualTransactionKey"] + "," + Session["Branchid"] + ",'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "',0,'C'," + ddlBankHead.SelectedItem.Value + ",'" + balayer.indiandateToMysqlDate(txtDate.Text) + "','" + balayer.MySQLEscapeString(balayer.ToobjectstrEvenNull(dt.Rows[0]["Narration"])) + "'," + balayer.ToobjectstrEvenNull(dt.Rows[0]["LoanAmount"]) + ",'LOAN','" + Session["UserName"] + "',0," + txtDate.Text.Split('/')[0] + "," + txtDate.Text.Split('/')[1] + "," + txtDate.Text.Split('/')[2] + ",0,0,8,0,2)");
                            string strBankName = balayer.GetSingleValue("SELECT BankName FROM svcf.bankdetails where Head_Id=" + ddlBankHead.SelectedItem.Value);
                            long iResult2 = trn.insertorupdateTrn("insert into svcf.transbank (BranchID,DualTransactionKey,T_Day,T_Month,T_Year,BankHeadID,Head_Id,MemberID,CustomersBankName,DateInCheque,ChequeDDNO,GivenAmount,TotalChequeDDAmount,IsBounced,Trans_Type) values (" + Session["Branchid"] + "," + dt.Rows[0]["DualTransactionKey"] + "," + txtDate.Text.Split('/')[0] + "," + txtDate.Text.Split('/')[1] + "," + txtDate.Text.Split('/')[2] + "," + ddlBankHead.SelectedItem.Value + "," + dt.Rows[0]["LoanHeadID"] + "," + balayer.ToobjectstrEvenNull(dt.Rows[0]["MemberID"]) + ",'" + balayer.MySQLEscapeString(strBankName) + "','" + balayer.indiandateToMysqlDate(txtDate.Text) + "','" + txtChequeNo.Text + "'," + balayer.ToobjectstrEvenNull(dt.Rows[0]["LoanAmount"]) + "," + balayer.ToobjectstrEvenNull(dt.Rows[0]["LoanAmount"]) + ",0,0)");
                            long uResult = trn.insertorupdateTrn("UPDATE svcf.transloan set TransactionKey=" + iResult + ",ChoosenDate='" + balayer.indiandateToMysqlDate(txtDate.Text) + "',Trans_Medium=1,isTransfered=0 where aid=" + ddlHead.SelectedItem.Value);
                        }
                    }
                    trn.CommitTrn();
                    logger.Info("loaninsertion.aspx - btnanPayLoan_click():  Completed: " + DateTime.Now + " by: " + Convert.ToString(Session["UserName"]) + "");
                }
                catch (Exception error)
                {
                   trn.RollbackTrn();
                    ModalPopupExtender1.PopupControlID = "pnlmsg";
                    ModalPopupExtender1.Show();
                    pnlmsg.Visible = true;
                    lblcon.Text = error.Message;
                    isFinished = false;
                    logger.Info("loaninsertion.aspx - btnanPayLoan_click():  Error: " + error.Message + ": " + DateTime.Now + " by: " + Convert.ToString(Session["UserName"]) + "");
                }
                finally
                {
                    trn.DisposeTrn();
                    if (isFinished)
                    {
                        ModalPopupExtender1.PopupControlID = "pnlmsg";
                        ModalPopupExtender1.Show();
                        pnlmsg.Visible = true;
                        lblcon.Text = "Data saved suucessfully";
                    }
                }
            }
            else
            {
                ModalPopupExtender1.PopupControlID = "pnlmsg";
                ModalPopupExtender1.Show();
                pnlmsg.Visible = true;
                lblcon.Text = "Loan Number Does not match";
            }
        }
    }
}