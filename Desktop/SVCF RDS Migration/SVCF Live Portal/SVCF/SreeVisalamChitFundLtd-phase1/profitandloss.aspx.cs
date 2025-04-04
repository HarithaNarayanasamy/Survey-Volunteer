using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Data;
using SVCF_BusinessAccessLayer;
using SVCF_DataAccessLayer;
using SVCF_TransactionLayer;
using log4net;
using log4net.Config;
namespace SreeVisalamChitFundLtd_phase1
{
    public partial class profitandloss : System.Web.UI.Page
    {
        #region Object
        BusinessLayer balayer = new BusinessLayer();
        #endregion
        ILog logger = log4net.LogManager.GetLogger(typeof(profitandloss));

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                Session["CheckRefresh"] = System.Guid.NewGuid().ToString();
                DataTable dt = balayer.GetDataTable("select Node,NodeID from headstree where ParentID=11");
                DataRow dr=dt.NewRow();
                dr[0] = "--Select--";
                dr[1] = "0";
                ddlLossandprofit.DataSource = dt;
                ddlLossandprofit.DataTextField = "Node";
                ddlLossandprofit.DataValueField = "NodeID";
                dt.Rows.InsertAt(dr, 0);
                ddlLossandprofit.DataBind();
                DataTable dtBank = balayer.GetDataTable("SELECT concat( concat(t1.BankName,' _ ',t1.IFCCode),' _ ',t1.AccountNo) as BankDetails, t1.Head_Id as Head_Id, t1.Banklocation as Banklocation FROM svcf.bankdetails as t1 join headstree as t2 on (t1.Head_Id=t2.NodeId) where t1.BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]));
                DataRow drBank = dtBank.NewRow();
                drBank[0] = "--Select--";
                drBank[1] = "0";
                ddlBankHead.DataSource = dtBank;
                ddlBankHead.DataTextField = "BankDetails";
                ddlBankHead.DataValueField = "Head_Id";
                dtBank.Rows.InsertAt(drBank, 0);
                ddlBankHead.DataBind();
            }
        }
        protected void Page_PreRender(object sender, EventArgs e)
        {
            ViewState["CheckRefresh"] = Session["CheckRefresh"];
        }
        protected void btncancel1_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.Url.AbsolutePath.ToString());
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
            bool isFinished = true;
            TransactionLayer trn = new TransactionLayer();
            try
            {
                System.Guid guid = Guid.NewGuid();
                // Prepare GUID values in SQL format
                string hexstring = BitConverter.ToString(guid.ToByteArray());
                string guidForBinary16 = "0x" + hexstring.Replace("-", string.Empty);
                string DualTransactionKey = guidForBinary16;
                if (ddlTransaction.SelectedItem.Text == "Cash")
                {
                    long iResult = trn.insertorupdateTrn("INSERT INTO `svcf`.`voucher` (`Other_Trans_Type`,`DualTransactionKey`,`BranchID`,`CurrDate`,`Voucher_No`,`Voucher_Type`,`Head_Id`,`ChoosenDate`,`Narration`,`Amount`,`Series`,`ReceievedBy`,`Trans_Type`,`T_Day`,`T_Month`,`T_Year`,`MemberID`,`Trans_Medium`,`RootID`,`ChitGroupId`) VALUES (0," + DualTransactionKey + "," + balayer.ToobjectstrEvenNull(Session["Branchid"]) + ",'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "',0,'C',12,'" + balayer.indiandateToMysqlDate(txtDate.Text) + "','" + balayer.MySQLEscapeString(txtNarration.Text) + "'," + txtamount.Text + ",'ProfitandLoss','" + balayer.ToobjectstrEvenNull(Session["UserName"]) + "',0," + txtDate.Text.Split('/')[0] + "," + txtDate.Text.Split('/')[1] + "," + txtDate.Text.Split('/')[2] + ",0,0,12,0)");
                    long iResult1 = trn.insertorupdateTrn("INSERT INTO `svcf`.`voucher` (`Other_Trans_Type`,`DualTransactionKey`,`BranchID`,`CurrDate`,`Voucher_No`,`Voucher_Type`,`Head_Id`,`ChoosenDate`,`Narration`,`Amount`,`Series`,`ReceievedBy`,`Trans_Type`,`T_Day`,`T_Month`,`T_Year`,`MemberID`,`Trans_Medium`,`RootID`,`ChitGroupId`) VALUES (0," + DualTransactionKey + "," + balayer.ToobjectstrEvenNull(Session["Branchid"]) + ",'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "',0,'D'," + ddlLossandprofit.SelectedItem.Value + ",'" + balayer.indiandateToMysqlDate(txtDate.Text) + "','" + balayer.MySQLEscapeString(txtNarration.Text) + "'," + txtamount.Text + ",'ProfitandLoss','" + balayer.ToobjectstrEvenNull(Session["UserName"]) + "',0," + txtDate.Text.Split('/')[0] + "," + txtDate.Text.Split('/')[1] + "," + txtDate.Text.Split('/')[2] + ",0,0,11,0 )");
                    long iResult3 = trn.insertorupdateTrn("insert into svcf.transprofitandloss (BranchID,DualTransactionKey,TransactionKey,HeadID,Narration,Amount,ApplyedOn,TransMedium,Flag,aosanction) values (" + Session["Branchid"] + "," + DualTransactionKey + "," + iResult1 + "," + ddlLossandprofit.SelectedItem.Value + ",'" + balayer.MySQLEscapeString(txtNarration.Text) + "'," + txtamount.Text + ",'" + balayer.indiandateToMysqlDate(txtDate.Text) + "',0,0,"+txtAOSanction.Text+")");
                }
                else if (ddlTransaction.SelectedItem.Text == "Bank")
                {
                    long iResult = trn.insertorupdateTrn("INSERT INTO `svcf`.`voucher` (`Other_Trans_Type`,`DualTransactionKey`,`BranchID`,`CurrDate`,`Voucher_No`,`Voucher_Type`,`Head_Id`,`ChoosenDate`,`Narration`,`Amount`,`Series`,`ReceievedBy`,`Trans_Type`,`T_Day`,`T_Month`,`T_Year`,`MemberID`,`Trans_Medium`,`RootID`,`ChitGroupId`) VALUES (0," + DualTransactionKey + "," + balayer.ToobjectstrEvenNull(Session["Branchid"]) + ",'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "',0,'C'," + ddlBankHead.SelectedItem.Value + ",'" + balayer.indiandateToMysqlDate(txtDate.Text) + "','" + balayer.MySQLEscapeString(txtNarration.Text) + "'," + txtamount.Text + ",'ProfitandLoss','" + balayer.ToobjectstrEvenNull(Session["UserName"]) + "',0," + txtDate.Text.Split('/')[0] + "," + txtDate.Text.Split('/')[1] + "," + txtDate.Text.Split('/')[2] + ",0,1,3,0)");
                    long iResult1 = trn.insertorupdateTrn("INSERT INTO `svcf`.`voucher` (`Other_Trans_Type`,`DualTransactionKey`,`BranchID`,`CurrDate`,`Voucher_No`,`Voucher_Type`,`Head_Id`,`ChoosenDate`,`Narration`,`Amount`,`Series`,`ReceievedBy`,`Trans_Type`,`T_Day`,`T_Month`,`T_Year`,`MemberID`,`Trans_Medium`,`RootID`,`ChitGroupId`) VALUES (0," + DualTransactionKey + "," + balayer.ToobjectstrEvenNull(Session["Branchid"]) + ",'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "',0,'D'," + ddlLossandprofit.SelectedItem.Value + ",'" + balayer.indiandateToMysqlDate(txtDate.Text) + "','" + balayer.MySQLEscapeString(txtNarration.Text) + "'," + txtamount.Text + ",'ProfitandLoss','" + balayer.ToobjectstrEvenNull(Session["UserName"]) + "',0," + txtDate.Text.Split('/')[0] + "," + txtDate.Text.Split('/')[1] + "," + txtDate.Text.Split('/')[2] + ",0,1,11,0 )");
                    long iResult2 = trn.insertorupdateTrn("insert into transbank(BranchID, TransactionKey, DualTransactionKey, T_Day, T_Month, T_Year, BankHeadID, Head_Id, MemberID, CustomersBankName, DateInCheque, ChequeDDNO, GivenAmount, TotalChequeDDAmount, IsBounced, Trans_Type) values (" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + "," + iResult1 + "," + DualTransactionKey + "," + txtDate.Text.Split('/')[0] + "," + txtDate.Text.Split('/')[1] + "," + txtDate.Text.Split('/')[2] + "," + ddlBankHead.SelectedItem.Value + "," + ddlLossandprofit.SelectedItem.Value + ",0,'" + ddlBankHead.SelectedItem.Text.Split('_')[0] + "','" + balayer.indiandateToMysqlDate(txtDate.Text) + "'," + txtChequeNo.Text + "," + txtamount.Text + "," + txtamount.Text + ",0,0)");
                    long iResult3 = trn.insertorupdateTrn("insert into svcf.transprofitandloss (BranchID,DualTransactionKey,TransactionKey,HeadID,Narration,Amount,BankHeadId,ChequeNo,ApplyedOn,TransMedium,Flag,aosanction) values (" + Session["Branchid"] + "," + DualTransactionKey + "," + iResult1 + "," + ddlLossandprofit.SelectedItem.Value + ",'" + balayer.MySQLEscapeString(txtNarration.Text) + "'," + txtamount.Text + "," + ddlBankHead.SelectedItem.Value + "," + txtChequeNo.Text + ",'" + balayer.indiandateToMysqlDate(txtDate.Text) + "',1,0,"+txtAOSanction.Text+")");
                }
                trn.CommitTrn();
                logger.Info("profitandloss.aspx - btnPayLoan_click():  Completed: " + DateTime.Now + " by: " + Convert.ToString(Session["UserName"]) + "");
            }
            catch (Exception error)
            {
                try
                {
                   trn.RollbackTrn();
                   logger.Info("profitandloss.aspx - btnPayLoan_click():  Error: " + error.Message + ": " + DateTime.Now + " by: " + Convert.ToString(Session["UserName"]) + "");
                }
                catch { }
                finally {
                    ModalPopupExtender1.PopupControlID = "pnlmsg";
                    ModalPopupExtender1.Show();
                    pnlmsg.Visible = true;
                    lblcon.Text = error.Message;
                    lblcon.ForeColor = System.Drawing.Color.Red;
                    isFinished = false;
                }
            }
            finally
            {
                trn.DisposeTrn();
                if (isFinished)
                {
                    ModalPopupExtender1.PopupControlID = "pnlmsg";
                    ModalPopupExtender1.Show();
                    pnlmsg.Visible = true;
                    lblcon.Text = "Data saved Successfully";
                    lblcon.ForeColor = System.Drawing.Color.Red;
                }
            }
        }
        protected void ddlTransaction_SelectedIndexChanged(object sender, EventArgs e)
        {
            bindmedium();
        }
        void bindmedium()
        {
            if (ddlTransaction.SelectedValue.ToString() == "Bank")
            {
                lblBankHead.Visible = true;
                lblChequeNo.Visible = true;
                ddlBankHead.Visible = true;
                txtChequeNo.Visible = true;
                CVddlBankHead.Visible = true;
                RFVtxtIfsc.Visible = true;
                txtChequeNo.Focus();
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

    }
}