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
    public partial class FixedDepositRelease : System.Web.UI.Page
    {
        #region Object
        BusinessLayer balayer = new BusinessLayer();
        string query = "";
        string userinfo = "";
        string qry = "";
        string usrRole = "";
        string memid = "";
        #endregion

        ILog logger = log4net.LogManager.GetLogger(typeof(FixedDepositRelease));

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                userinfo = HttpContext.Current.User.Identity.Name;
                qry = "select rs.name from roles as rs inner join rights as rt on (rt.roleid=rs.id) where memberid=" + userinfo + "";
                usrRole = balayer.GetSingleValue(qry);
                if (usrRole == "Report")
                {
                    Response.Redirect(Page.ResolveUrl("~/Home.aspx"), true);
                }
                rvDepositDate.MinimumValue = balayer.GetSingleValue("SELECT DATE_FORMAT( min(MinimumDate), '%d/%m/%Y') MinimumDate FROM svcf.restrictionmaster where BranchID=" + Session["Branchid"]); 
                rvDepositDate.MaximumValue = DateTime.Now.ToString("dd/MM/yyyy");
                txtDepositDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                Session["CheckRefresh"] = System.Guid.NewGuid().ToString();
                DataTable dtChit = balayer.GetDataTable("SELECT GROUPNO,Head_Id FROM svcf.groupmaster where BranchId=" + Session["Branchid"] + " and Head_id not in (SELECT ChitNumber FROM svcf.fd where BranchId=" + Session["Branchid"] + ")");
                DataRow drChit = dtChit.NewRow();
                drChit[0] = "--Select--";
                drChit[1] = "0";
                ddlGroupNumber.DataSource = dtChit;
                ddlGroupNumber.DataTextField = "GROUPNO";
                ddlGroupNumber.DataValueField = "Head_Id";
                dtChit.Rows.InsertAt(drChit, 0);
                ddlGroupNumber.DataBind();


                DataTable dtBank = balayer.GetDataTable("SELECT Head_Id,concat(BankName, ' ',cast(AccountNo as char)) as BankName FROM svcf.bankdetails where ISActive=1 and TypeofBank='Fixed deposits with Banks' and BranchID in (00000," + Session["Branchid"] + ")");
                DataRow drBank = dtBank.NewRow();
                drBank[0] = "0";
                drBank[1] = "--Select--";
                ddlBank.DataSource = dtBank;
                ddlBank.DataTextField = "BankName";
                ddlBank.DataValueField = "Head_Id";
                dtBank.Rows.InsertAt(drBank, 0);
                ddlBank.DataBind();


                DataTable dTbranch = balayer.GetDataTable("select NodeID,Node from svcf.headstree where NodeID in (160,161,162)");
                DataRow drbranch = dTbranch.NewRow();
                drbranch[1] = "--Select--";
                drbranch[0] = "0";
                ddlBranchid.DataSource = dTbranch;
                ddlBranchid.DataTextField = "Node";
                ddlBranchid.DataValueField = "NodeID";
                dTbranch.Rows.InsertAt(drbranch, 0);
                ddlBranchid.DataBind();
            }
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
                string select = ddlBranchid.SelectedValue.ToString();


                //System.Guid guid = Guid.NewGuid();
                //string guidForChar36 = guid.ToString();
                //string hexstring = BitConverter.ToString(guid.ToByteArray());
                //string guidForBinary16 = "0x" + hexstring.Replace("-", string.Empty);

                //stored procedure for DualTransactionKey

                //string DualTransactionKey = Convert.ToString(balayer.sp_gendratedt_key());

                //15/6/2023
                System.Guid guid = Guid.NewGuid();
                string hexstring = BitConverter.ToString(guid.ToByteArray());
                string guidForBinary16 = "0x" + hexstring.Replace("-", string.Empty);
                string DualTransactionKey = guidForBinary16;

                //Random rnd = new Random();
                //int rr = rnd.Next(1, 1000);
                string rr = balayer.GetSingleValue("SELECT ifnull(max(cast(Voucher_No as unsigned)),0)+1 FROM svcf.voucher where ChoosenDate>='2014/04/01' and  Series='FDR' and Trans_Type='0' and BranchID=" + Session["Branchid"]);
                long Credit = trn.insertorupdateTrn("insert into voucher (`DualTransactionKey`,`BranchID`,`CurrDate`,`Voucher_No`,`Voucher_Type`,`Head_Id`,`ChoosenDate`,`Narration`,`Amount`,`Series`,`ReceievedBy`,`Trans_Type`,`T_Day`,`T_Month`,`T_Year`,`MemberID`,`Trans_Medium`,`RootID`,`ChitGroupId`,`Other_Trans_Type`) values (" + DualTransactionKey + "," + balayer.ToobjectstrEvenNull(Session["Branchid"]) + ",'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," + rr + ",'C'," + ddlBank.SelectedValue + ",'" + balayer.indiandateToMysqlDate(txtDepositDate.Text) + "','By " + ddlGroupNumber.SelectedItem.Text + " terminated group chit security FDR amount received towards "+ddlBank.SelectedItem.Text+" FDR No. " + txtChequeNumber.Text + " '," + txtAmount.Text + ",'FDR','" + balayer.MySQLEscapeString(balayer.ToobjectstrEvenNull(Session["UserName"])) + "',0," + balayer.indiandateToMysqlDate(txtDepositDate.Text).Split('/')[2] + "," + balayer.indiandateToMysqlDate(txtDepositDate.Text).Split('/')[1] + "," + balayer.indiandateToMysqlDate(txtDepositDate.Text).Split('/')[0] + ",0,1,3," + ddlGroupNumber.SelectedItem.Value + ",0) ");
                long CreditBank = trn.insertorupdateTrn("insert into transbank(`BranchID`,`TransactionKey`,`DualTransactionKey`,`T_Day`,`T_Month`,`T_Year`,`Head_Id`,`BankHeadID`,`MemberID`,`CustomersBankName`,`DateInCheque`,`ChequeDDNO`,`GivenAmount`,`TotalChequeDDAmount`,`IsBounced`,`Trans_Type`) values(" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + "," + Credit + "," + DualTransactionKey + "," + balayer.indiandateToMysqlDate(txtDepositDate.Text).Split('/')[2] + "," + balayer.indiandateToMysqlDate(txtDepositDate.Text).Split('/')[1] + "," + balayer.indiandateToMysqlDate(txtDepositDate.Text).Split('/')[0] + ", " + select + "," + ddlBank.SelectedValue + ",0,'" + ddlBank.SelectedItem.Text + "','" + balayer.indiandateToMysqlDate(txtDepositDate.Text) + "'," + txtChequeNumber.Text + "," + txtAmount.Text + "," + txtAmount.Text + ",0,2)");
                long Debit = trn.insertorupdateTrn("insert into voucher (`DualTransactionKey`,`BranchID`,`CurrDate`,`Voucher_No`,`Voucher_Type`,`Head_Id`,`ChoosenDate`,`Narration`,`Amount`,`Series`,`ReceievedBy`,`Trans_Type`,`T_Day`,`T_Month`,`T_Year`,`MemberID`,`Trans_Medium`,`RootID`,`ChitGroupId`,`Other_Trans_Type`) values (" + DualTransactionKey + "," + balayer.ToobjectstrEvenNull(Session["Branchid"]) + ",'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," + rr + ",'D'," + select + ",'" + balayer.indiandateToMysqlDate(txtDepositDate.Text) + "','By " + ddlGroupNumber.SelectedItem.Text + " terminated group chit security FDR amount received towards " + ddlBank.SelectedItem.Text + " FDR No. " + txtChequeNumber.Text + " '," + txtAmount.Text + ",'FDR','" + balayer.MySQLEscapeString(balayer.ToobjectstrEvenNull(Session["UserName"])) + "',0," + balayer.indiandateToMysqlDate(txtDepositDate.Text).Split('/')[2] + "," + balayer.indiandateToMysqlDate(txtDepositDate.Text).Split('/')[1] + "," + balayer.indiandateToMysqlDate(txtDepositDate.Text).Split('/')[0] + ",0,1,1," + ddlGroupNumber.SelectedItem.Value + ",0) ");

                long l = trn.insertorupdateTrn("insert into svcf.fd(ChitNumber,Date,BranchID,DualTransactionKey) values (" + ddlGroupNumber.SelectedItem.Value + ",'" + balayer.indiandateToMysqlDate(txtDepositDate.Text) + "'," + Session["Branchid"] + "," + DualTransactionKey + ")");
                trn.CommitTrn();
                logger.Info("FixedDepositRelease.aspx - btnPayment_Click():  Completed: " + DateTime.Now + " by: " + Convert.ToString(Session["UserName"]) + "");
            }
            catch (Exception ex)
            {
               trn.RollbackTrn();
                ModalPopupExtender1.PopupControlID = "pnlmsg";
                ModalPopupExtender1.Show();
                pnlmsg.Visible = true;
                lblh.Text = "Status";
                lblcon.Text = ex.Message;
                lblcon.ForeColor = System.Drawing.Color.Red;
                isVal = false;
                logger.Info("FixedDepositRelease.aspx - btnPayment_Click():  Error: " + ex.Message + ": " + DateTime.Now + " by: " + Convert.ToString(Session["UserName"]) + "");
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

        protected void ddlGroupNumber_SelectedIndexChanged(object sender, EventArgs e)
        {
            var ss = ddlGroupNumber.SelectedItem.Text;

            txtAmount.Text = balayer.GetSingleValue("select SDP_Amount from svcf.groupmaster where GROUPNO='" + ss+"' ;");

            txtChequeNumber.Text= balayer.GetSingleValue("select SDP_FDRNO from svcf.groupmaster where GROUPNO='" + ss + "' ;");

            txtNarration.Text = "Being "+ss+" Terminated Group Chit Security FDR Amount Received from CUB Ltd., Karaikudi towards FDR No. "+ txtChequeNumber.Text + "";
        }
    }
}