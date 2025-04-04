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
    public partial class courtinsertion : System.Web.UI.Page
    {
        #region Object
        BusinessLayer balayer = new BusinessLayer();
        #endregion

        ILog logger = log4net.LogManager.GetLogger(typeof(courtinsertion));

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

                rvDate.MaximumValue = DateTime.Now.ToString("dd/MM/yyyy");
                rvDate.MinimumValue = DateTime.Now.AddYears(-1).ToString("dd/MM/yyyy");
                DataTable dt = balayer.GetDataTable("SELECT t1.ano,concat(h1.Node,' | ',h2.Node) as Head FROM svcf.transcourt as t1 join headstree as h1 on (t1.HeadID=h1.NodeID) join headstree as h2 on (t1.ReHeadId=h2.NodeID) where t1.Flag=1 and t1.istransfered=0 and t1.BranchID=" + Session["Branchid"]);
                DataRow dr = dt.NewRow();
                dr[0] = "0";
                dr[1] = "--Select--";
                ddlHead.DataTextField = "Head";
                ddlHead.DataValueField = "ano";
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
                DataTable dt = balayer.GetDataTable("SELECT concat(BankName,' _ ',IFCCode,' _ ',AccountNo) as Bank,Head_Id FROM svcf.bankdetails where BranchID=" + Session["Branchid"]);
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
            DataTable dtCheck = balayer.GetDataTable("SELECT * FROM svcf.transcourt where VerificationNumber=" + txtDecreeNumber.Text + " and ano=" + ddlHead.SelectedItem.Value);
            if (dtCheck.Rows.Count == 1)
            {
                bool isFinished = true;
                TransactionLayer trn = new TransactionLayer();
                try
                {
                    DataTable dt = balayer.GetDataTable("SELECT insertkey_from_bin(DualTransactionKey) as DualTransactionKey,Narration,Amount FROM svcf.transcourt where ano=" + ddlHead.SelectedItem.Value);

                    if (ddlMedium.SelectedItem.Text == "Cash")
                    {
                        long iResult = trn.insertorupdateTrn("insert into voucher(`DualTransactionKey`,`BranchID`,`CurrDate`,`Voucher_No`,`Voucher_Type`,`Head_Id`,`ChoosenDate`,`Narration`,`Amount`,`Series`,`ReceievedBy`,`Trans_Type`,`T_Day`,`T_Month`,`T_Year`,`MemberID`,`Trans_Medium`,`RootID`,ChitGroupId,`Other_Trans_Type`) values (" + dt.Rows[0]["DualTransactionKey"] + "," + Session["Branchid"] + ",'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "',0,'D',52,'" + balayer.indiandateToMysqlDate(txtDate.Text) + "','" + balayer.MySQLEscapeString(balayer.ToobjectstrEvenNull(dt.Rows[0]["Narration"])) + "'," + balayer.ToobjectstrEvenNull(dt.Rows[0]["Amount"]) + ",'Decree','" + Session["UserName"] + "',0," + txtDate.Text.Split('/')[0] + "," + txtDate.Text.Split('/')[1] + "," + txtDate.Text.Split('/')[2] + ",0,0,7,0,2)");
                        long iResult1 = trn.insertorupdateTrn("insert into voucher(`DualTransactionKey`,`BranchID`,`CurrDate`,`Voucher_No`,`Voucher_Type`,`Head_Id`,`ChoosenDate`,`Narration`,`Amount`,`Series`,`ReceievedBy`,`Trans_Type`,`T_Day`,`T_Month`,`T_Year`,`MemberID`,`Trans_Medium`,`RootID`,ChitGroupId,`Other_Trans_Type`) values (" + dt.Rows[0]["DualTransactionKey"] + "," + Session["Branchid"] + ",'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "',0,'C',12,'" + balayer.indiandateToMysqlDate(txtDate.Text) + "','" + balayer.MySQLEscapeString(balayer.ToobjectstrEvenNull(dt.Rows[0]["Narration"])) + "','" + balayer.ToobjectstrEvenNull(dt.Rows[0]["Amount"]) + "','Decree','" + Session["UserName"] + "',0," + txtDate.Text.Split('/')[0] + "," + txtDate.Text.Split('/')[1] + "," + txtDate.Text.Split('/')[2] + ",0,0,12,0,2)");
                        long uResult = trn.insertorupdateTrn("UPDATE svcf.transcourt set TransactionKey=" + iResult + ",ChoosenDate='" + balayer.indiandateToMysqlDate(txtDate.Text) + "',Trans_Medium=0,isTransfered=1 where ano=" + ddlHead.SelectedItem.Value);
                    }
                    else if (ddlMedium.SelectedItem.Text == "Bank")
                    {
                        long iResult = trn.insertorupdateTrn("insert into voucher(`DualTransactionKey`,`BranchID`,`CurrDate`,`Voucher_No`,`Voucher_Type`,`Head_Id`,`ChoosenDate`,`Narration`,`Amount`,`Series`,`ReceievedBy`,`Trans_Type`,`T_Day`,`T_Month`,`T_Year`,`MemberID`,`Trans_Medium`,`RootID`,ChitGroupId,`Other_Trans_Type`) values (" + dt.Rows[0]["DualTransactionKey"] + "," + Session["Branchid"] + ",'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "',0,'D',52,'" + balayer.indiandateToMysqlDate(txtDate.Text) + "','" + balayer.MySQLEscapeString(balayer.ToobjectstrEvenNull(dt.Rows[0]["Narration"])) + "'," + balayer.ToobjectstrEvenNull(dt.Rows[0]["Amount"]) + ",'Decree','" + Session["UserName"] + "',0," + txtDate.Text.Split('/')[0] + "," + txtDate.Text.Split('/')[1] + "," + txtDate.Text.Split('/')[2] + ",0,0,7,0,2)");
                        long iResult1 = trn.insertorupdateTrn("insert into voucher(`DualTransactionKey`,`BranchID`,`CurrDate`,`Voucher_No`,`Voucher_Type`,`Head_Id`,`ChoosenDate`,`Narration`,`Amount`,`Series`,`ReceievedBy`,`Trans_Type`,`T_Day`,`T_Month`,`T_Year`,`MemberID`,`Trans_Medium`,`RootID`,ChitGroupId,`Other_Trans_Type`) values (" + dt.Rows[0]["DualTransactionKey"] + "," + Session["Branchid"] + ",'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "',0,'C'," + ddlBankHead.SelectedItem.Value + ",'" + balayer.indiandateToMysqlDate(txtDate.Text) + "','" + balayer.MySQLEscapeString(balayer.ToobjectstrEvenNull(dt.Rows[0]["Narration"])) + "'," + balayer.ToobjectstrEvenNull(dt.Rows[0]["Amount"]) + ",'Decree','" + Session["UserName"] + "',0," + txtDate.Text.Split('/')[0] + "," + txtDate.Text.Split('/')[1] + "," + txtDate.Text.Split('/')[2] + ",0,0,3,0,2)");
                        string strBankName = balayer.GetSingleValue("SELECT BankName FROM svcf.bankdetails where Head_Id=" + ddlBankHead.SelectedItem.Value);
                        long iResult2 = trn.insertorupdateTrn("insert into svcf.transbank (BranchID,DualTransactionKey,T_Day,T_Month,T_Year,BankHeadID,Head_Id,MemberID,CustomersBankName,DateInCheque,ChequeDDNO,GivenAmount,TotalChequeDDAmount,IsBounced,Trans_Type) values (" + Session["Branchid"] + "," + dt.Rows[0]["DualTransactionKey"] + "," + txtDate.Text.Split('/')[0] + "," + txtDate.Text.Split('/')[1] + "," + txtDate.Text.Split('/')[2] + "," + ddlBankHead.SelectedItem.Value + ",52,0,'" + balayer.MySQLEscapeString(strBankName) + "','" + balayer.indiandateToMysqlDate(txtDate.Text) + "','" + txtChequeNo.Text + "'," + balayer.ToobjectstrEvenNull(dt.Rows[0]["Amount"]) + "," + balayer.ToobjectstrEvenNull(dt.Rows[0]["Amount"]) + ",0,0)");
                        long uResult = trn.insertorupdateTrn("UPDATE svcf.transcourt set TransactionKey=" + iResult + ",ChoosenDate='" + balayer.indiandateToMysqlDate(txtDate.Text) + "',Trans_Medium=1,isTransfered=1 where ano=" + ddlHead.SelectedItem.Value);
                    }

                    trn.CommitTrn();
                    logger.Info("CourtCost.aspx - btnPayLoan_Click() - Completed: " + DateTime.Now + " by: " + Convert.ToString(Session["UserName"]) + "");
                }
                catch (Exception error)
                {
                   trn.RollbackTrn();
                   logger.Info("CourtCost.aspx - btnPayLoan_Click() - Error: " + error.Message + ": " + DateTime.Now + " by: " + Convert.ToString(Session["UserName"]) + "");
                    ModalPopupExtender1.PopupControlID = "pnlmsg";
                    ModalPopupExtender1.Show();
                    pnlmsg.Visible = true;
                    lblcon.Text = error.Message;
                    isFinished = false;
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
                lblcon.Text = "Decree Number Does not match";
            }
        }
    }
}