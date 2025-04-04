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
    public partial class RCMRelease : System.Web.UI.Page
    {
        #region Object
        BusinessLayer balayer = new BusinessLayer();
        #endregion

        ILog logger = log4net.LogManager.GetLogger(typeof(RCMRelease));

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

                txtDepositDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                Session["CheckRefresh"] = System.Guid.NewGuid().ToString();
                DataTable dtChit = balayer.GetDataTable("SELECT Node,NodeId FROM svcf.headstree where BranchId=" + Session["Branchid"] + " and ParentID in (44,45)");
                DataRow drChit = dtChit.NewRow();
                drChit[0] = "--Select--";
                drChit[1] = "0";
                ddlGroupNumber.DataSource = dtChit;
                ddlGroupNumber.DataTextField = "Node";
                ddlGroupNumber.DataValueField = "NodeId";
                dtChit.Rows.InsertAt(drChit, 0);
                ddlGroupNumber.DataBind();
                DataTable dtBank = balayer.GetDataTable("SELECT Head_Id,concat(BankName, ' ',cast(AccountNo as char)) as BankName FROM svcf.bankdetails");
                DataRow drBank = dtBank.NewRow();
                drBank[0] = "0";
                drBank[1] = "--Select--";
                ddlBank.DataSource = dtBank;
                ddlBank.DataTextField = "BankName";
                ddlBank.DataValueField = "Head_Id";
                dtBank.Rows.InsertAt(drBank, 0);
                ddlBank.DataBind();
            }

            rvDepositDate.MinimumValue = balayer.GetSingleValue("SELECT DATE_FORMAT( min(MinimumDate), '%d/%m/%Y') MinimumDate FROM svcf.restrictionmaster where BranchID=" + Session["Branchid"]);
            rvDepositDate.MaximumValue = DateTime.Now.ToString("dd/MM/yyyy");
        }
        protected void Page_PreRender(object sender, EventArgs e)
        {
            ViewState["CheckRefresh"] = Session["CheckRefresh"];
        }
        protected void btnPayment_Click(object sender, EventArgs e)
        {
            Page.Validate("pa");
            if (!Page.IsValid)
            {
                return;
            }
            if (Session["CheckRefresh"].ToString() != ViewState["CheckRefresh"].ToString())
            {
                return;
            }
            bool isVal = true;
            TransactionLayer trn = new TransactionLayer();
            try
            {
                System.Guid guid = Guid.NewGuid();
                string guidForChar36 = guid.ToString();
                string hexstring = BitConverter.ToString(guid.ToByteArray());
                string guidForBinary16 = "0x" + hexstring.Replace("-", string.Empty);
                Random rnd = new Random();
                int rr = rnd.Next(1, 1000);
                long Credit = trn.insertorupdateTrn("insert into voucher (`DualTransactionKey`,`BranchID`,`CurrDate`,`Voucher_No`,`Voucher_Type`,`Head_Id`,`ChoosenDate`,`Narration`,`Amount`,`Series`,`ReceievedBy`,`Trans_Type`,`T_Day`,`T_Month`,`T_Year`,`MemberID`,`Trans_Medium`,`RootID`,`ChitGroupId`,`Other_Trans_Type`) values (" + guidForBinary16 + "," + balayer.ToobjectstrEvenNull(Session["Branchid"]) + ",'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," + rr + ",'C'," + ddlBank.SelectedValue + ",'" + balayer.indiandateToMysqlDate(txtDepositDate.Text) + "','By " + ddlGroupNumber.SelectedItem.Text + " RCM amount received towards " + ddlBank.SelectedItem.Text + " " + txtChequeNumber.Text + " '," + txtAmount.Text + ",'RCM','" + balayer.MySQLEscapeString(balayer.ToobjectstrEvenNull(Session["UserName"])) + "',0," + balayer.indiandateToMysqlDate(txtDepositDate.Text).Split('/')[2] + "," + balayer.indiandateToMysqlDate(txtDepositDate.Text).Split('/')[1] + "," + balayer.indiandateToMysqlDate(txtDepositDate.Text).Split('/')[0] + ",0,1,3,0,0) ");
                long CreditBank = trn.insertorupdateTrn("insert into transbank(`BranchID`,`TransactionKey`,`DualTransactionKey`,`T_Day`,`T_Month`,`T_Year`,`Head_Id`,`BankHeadID`,`MemberID`,`CustomersBankName`,`DateInCheque`,`ChequeDDNO`,`GivenAmount`,`TotalChequeDDAmount`,`IsBounced`,`Trans_Type`) values(" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + "," + Credit + "," + guidForBinary16 + "," + balayer.indiandateToMysqlDate(txtDepositDate.Text).Split('/')[2] + "," + balayer.indiandateToMysqlDate(txtDepositDate.Text).Split('/')[1] + "," + balayer.indiandateToMysqlDate(txtDepositDate.Text).Split('/')[0] + ", 161," + ddlBank.SelectedValue + ",0,'" + ddlBank.SelectedItem.Text + "','" + balayer.indiandateToMysqlDate(txtDepositDate.Text) + "'," + txtChequeNumber.Text + "," + txtAmount.Text + "," + txtAmount.Text + ",0,2)");
                long Debit = trn.insertorupdateTrn("insert into voucher (`DualTransactionKey`,`BranchID`,`CurrDate`,`Voucher_No`,`Voucher_Type`,`Head_Id`,`ChoosenDate`,`Narration`,`Amount`,`Series`,`ReceievedBy`,`Trans_Type`,`T_Day`,`T_Month`,`T_Year`,`MemberID`,`Trans_Medium`,`RootID`,`ChitGroupId`,`Other_Trans_Type`) values (" + guidForBinary16 + "," + balayer.ToobjectstrEvenNull(Session["Branchid"]) + ",'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," + rr + ",'D'," + ddlGroupNumber.SelectedValue + ",'" + balayer.indiandateToMysqlDate(txtDepositDate.Text) + "','By " + ddlGroupNumber.SelectedItem.Text + " RCM amount received towards " + ddlBank.SelectedItem.Text + " FDR No. " + txtChequeNumber.Text + " '," + txtAmount.Text + ",'RCM','" + balayer.MySQLEscapeString(balayer.ToobjectstrEvenNull(Session["UserName"])) + "',0," + balayer.indiandateToMysqlDate(txtDepositDate.Text).Split('/')[2] + "," + balayer.indiandateToMysqlDate(txtDepositDate.Text).Split('/')[1] + "," + balayer.indiandateToMysqlDate(txtDepositDate.Text).Split('/')[0] + ",0,1,5,0,0) ");

                long l = trn.insertorupdateTrn("update svcf.depositpayment set ReleseDate='"+balayer.indiandateToMysqlDate(txtDepositDate.Text)+"' where HeadId="+ddlGroupNumber.SelectedValue);
                trn.CommitTrn();
                logger.Info("RCMRelease.aspx - btnPayment_click():  Completed: " + DateTime.Now + " by: " + Convert.ToString(Session["UserName"]) + "");

            }
            catch (Exception ex)
            {
               trn.RollbackTrn();
               logger.Info("RCMRelease.aspx - btnPayment_click():  Error: " + ex.Message + ": " + DateTime.Now + " by: " + Convert.ToString(Session["UserName"]) + "");
                ModalPopupExtender1.PopupControlID = "pnlmsg";
                ModalPopupExtender1.Show();
                pnlmsg.Visible = true;
                lblh.Text = "Status";
                lblcon.Text = ex.Message;
                lblcon.ForeColor = System.Drawing.Color.Red;
                isVal = false;
            }
            finally
            {
                trn.DisposeTrn();
                if (isVal)
                {
                    ModalPopupExtender1.PopupControlID = "pnlmsg";
                    ModalPopupExtender1.Show();
                    pnlmsg.Visible = true;
                    lblh.Text = "Status";
                    lblcon.Text = ddlGroupNumber.SelectedItem.Text + " - FD release Successfully";
                    lblcon.ForeColor = System.Drawing.Color.Green;
                }
            }
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.Url.AbsolutePath.ToString());
        }
        protected void btnOK_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.Url.AbsolutePath.ToString());
        }
        protected void btnCan_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.Url.AbsolutePath.ToString());
        }
    }
}