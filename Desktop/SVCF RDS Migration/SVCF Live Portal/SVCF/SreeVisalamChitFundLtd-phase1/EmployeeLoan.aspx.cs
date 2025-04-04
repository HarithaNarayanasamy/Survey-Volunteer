using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using SVCF_BusinessAccessLayer;
using SVCF_DataAccessLayer;
using SVCF_TransactionLayer;
using log4net;
using log4net.Config;

namespace SreeVisalamChitFundLtd_phase1
{
    public partial class EmployeeLoan : System.Web.UI.Page
    {
        #region Object
        BusinessLayer balayer = new BusinessLayer();
        TransactionLayer trn = new TransactionLayer();
        #endregion

        ILog logger = log4net.LogManager.GetLogger(typeof(EmployeeLoan));

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["CheckRefresh"] = System.Guid.NewGuid().ToString();
                dxAppliedDate.ToolTip = "Ex. " + DateTime.Now.ToString("dd/MM/yyyy") +" (dd/mm/yyyy)";
                dxApprovedDate.ToolTip = "Ex. " + DateTime.Now.ToString("dd/MM/yyyy") + " (dd/mm/yyyy)";
                fillBankHead();
                bindddlEmployeeNo();
            }
        }
        protected void Page_PreRender(object sender, EventArgs e)
        {
            ViewState["CheckRefresh"] = Session["CheckRefresh"];
        }
        protected void ddlMedium_SelectedIndexChanged(object sender, EventArgs e)
        {
            bindMedium();
        }
        void bindMedium()
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
        void bindddlEmployeeNo()
        {
            ddlEmployeeNo.DataSource = null;
            DataTable dtgroupno = balayer.GetDataTable("SELECT concat(`employee_details`.`Emp_ID`,'=',`employee_details`.`Emp_Name`) as EMP,`employee_details`.`Emp_ID` FROM svcf.employee_details where `employee_details`.`BranchID`=" +balayer.ToobjectstrEvenNull(Session["Branchid"]) + "");
            ddlEmployeeNo.DataSource = dtgroupno;
            ddlEmployeeNo.DataValueField = "Emp_ID";
            ddlEmployeeNo.DataTextField = "EMP";
            ddlEmployeeNo.DataBind();
            ddlEmployeeNo.Items.Insert(0, "--Select--");
        }
        void fillBankHead()
        {
            DataTable dtBank = balayer.GetDataTable("SELECT concat( concat(t1.BankName,'_',t1.IFCCode),'_',t1.AccountNo) as BankDetails, cast( t1.Head_Id as char) as Head_Id, t1.Banklocation as Banklocation FROM svcf.bankdetails as t1 join headstree as t2 on (t1.Head_Id=t2.NodeId) where t1.BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]));
            ddlBankHead.DataValueField = "Head_Id";
            ddlBankHead.DataTextField = "BankDetails";
            ddlBankHead.DataSource = dtBank;
            ddlBankHead.DataBind();
            ddlBankHead.Items.Insert(0, "--Select--");
        }
        protected void btncancel1_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.Url.AbsolutePath.ToString());
        }
        protected void btncancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.Url.AbsolutePath.ToString());
        }
        protected void btn_ok(object sender, EventArgs e)
        {
            pnlmsg.Visible = false;
            string node = "";
            string DualTransactionKey = "";
            try
            {
                //string strNarration = balayer.MySQLEscapeString(dd.SelectedItem.Text).Split('=')[0];
                int strHeadsTree = balayer.GetInsertItem("insert into `svcf`.`headstree` (`headstree`.`ParentID`,`headstree`.`Node`) values (55,'" + balayer.MySQLEscapeString( ddlEmployeeNo.SelectedItem.Text ) + "')");
                node = balayer.GetSingleValue("SELECT max(NodeID) FROM svcf.headstree where Node='" + balayer.MySQLEscapeString( ddlEmployeeNo.SelectedItem.Text ) + "'");
                int updateHeadsTree = balayer.GetInsertItem("update svcf.headstree set `headstree`.`TreeHint`='8,55," + node + "' where `headstree`.`NodeID`=" + node + "");
                System.Guid guid = Guid.NewGuid();
                string guidForChar36 = guid.ToString();
                string hexstring = BitConverter.ToString(guid.ToByteArray());
                string guidForBinary16 = "0x" + hexstring.Replace("-", string.Empty);
                DualTransactionKey = guidForBinary16;
                if (ddlMedium.SelectedItem.Text == "Cash")
                {
                    int GetLoanAmount = balayer.GetInsertItem("insert into voucher (`DualTransactionKey`,`BranchID`,`CurrDate`,`voucher`.`Voucher_No`,`Voucher_Type`,`Head_Id`,`ChoosenDate`,`Narration`,`Amount`,`Series`,`ReceievedBy`,`Trans_Type`,`T_Day`,`T_Month`,`T_Year`,`Trans_Medium`,`RootID`,`Other_Trans_Type`) values (" + DualTransactionKey + "," +balayer.ToobjectstrEvenNull(Session["Branchid"]) + ",'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," + balayer.indiandateToMysqlDate(dxAppliedDate.Text).Replace("/", "") + "" + txtAOSanctionNumber.Text + ",'D'," + node + ",'" + balayer.indiandateToMysqlDate(dxAppliedDate.Text) + "','" + balayer.MySQLEscapeString( txtNarration.Text) + "'," + txtAmount.Text + ",'LOAN','" + balayer.MySQLEscapeString( balayer.ToobjectstrEvenNull(Session["UserName"] )) + "',0," + balayer.indiandateToMysqlDate(dxAppliedDate.Text).Split('/')[2] + "," + balayer.indiandateToMysqlDate(dxAppliedDate.Text).Split('/')[1] + "," + balayer.indiandateToMysqlDate(dxAppliedDate.Text).Split('/')[0] + ",0,8,0)");
                    int GetLoanAmount1 = balayer.GetInsertItem("insert into voucher (`DualTransactionKey`,`BranchID`,`CurrDate`,`voucher`.`Voucher_No`,`Voucher_Type`,`Head_Id`,`ChoosenDate`,`Narration`,`Amount`,`Series`,`ReceievedBy`,`Trans_Type`,`T_Day`,`T_Month`,`T_Year`,`Trans_Medium`,`RootID`,`Other_Trans_Type`) values (" + DualTransactionKey + "," +balayer.ToobjectstrEvenNull(Session["Branchid"]) + ",'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," + balayer.indiandateToMysqlDate(dxAppliedDate.Text).Replace("/", "") + "" + txtAOSanctionNumber.Text + ",'C',12,'" + balayer.indiandateToMysqlDate(dxAppliedDate.Text) + "','" + balayer.MySQLEscapeString( txtNarration.Text ) + "'," + txtAmount.Text + ",'LOAN','" + balayer.MySQLEscapeString( balayer.ToobjectstrEvenNull(Session["UserName"] )) + "',0," + balayer.indiandateToMysqlDate(dxAppliedDate.Text).Split('/')[2] + "," + balayer.indiandateToMysqlDate(dxAppliedDate.Text).Split('/')[1] + "," + balayer.indiandateToMysqlDate(dxAppliedDate.Text).Split('/')[0] + ",0,12,0)");
                    int transactionKey = Convert.ToInt32(balayer.GetSingleValue("SELECT TransactionKey FROM svcf.voucher where `voucher`.`DualTransactionKey`=" + DualTransactionKey + " and `voucher`.`BranchID`=" +balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and `voucher`.`Head_Id`=" + node + " and `voucher`.`RootID`=8"));
                    int insertTransLoan = balayer.GetInsertItem("insert into `svcf`.`transloan` (`transloan`.`DualTransactionKey`,`transloan`.`TransactionKey`,`transloan`.`EmployeeID`,`transloan`.`LoanHeadID`,`transloan`.`LoanAmount`,`transloan`.`ApplyedOn`,`transloan`.`ApprovedOn`,`transloan`.`AdminSanctionNo`,`transloan`.`Narration`,`transloan`.`Trans_Medium`,`transloan`.`BranchID`) values (" + DualTransactionKey + "," + transactionKey + ",'" + ddlEmployeeNo.SelectedItem.Value + "'," + node + "," + txtAmount.Text + ",'" + balayer.indiandateToMysqlDate(dxAppliedDate.Text) + "','" + balayer.indiandateToMysqlDate(dxApprovedDate.Text) + "'," + txtAOSanctionNumber.Text + ",'" + balayer.MySQLEscapeString( txtNarration.Text) + "',0," +balayer.ToobjectstrEvenNull(Session["Branchid"]) + ")");
                }
                else if (ddlMedium.SelectedItem.Text == "Bank")
                {
                    int GetLoanAmount = balayer.GetInsertItem("insert into voucher (`DualTransactionKey`,`BranchID`,`CurrDate`,`voucher`.`Voucher_No`,`Voucher_Type`,`Head_Id`,`ChoosenDate`,`Narration`,`Amount`,`Series`,`ReceievedBy`,`Trans_Type`,`T_Day`,`T_Month`,`T_Year`,`Trans_Medium`,`RootID`,`Other_Trans_Type`) values (" + DualTransactionKey + "," +balayer.ToobjectstrEvenNull(Session["Branchid"]) + ",'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," + balayer.indiandateToMysqlDate(dxAppliedDate.Text).Replace("/", "") + "" + txtAOSanctionNumber.Text + ",'D'," + node + ",'" + balayer.indiandateToMysqlDate(dxAppliedDate.Text) + "','" + balayer.MySQLEscapeString( txtNarration.Text )+ "'," + txtAmount.Text + ",'LOAN','" + balayer.MySQLEscapeString( balayer.ToobjectstrEvenNull(Session["UserName"] )) + "',0," + balayer.indiandateToMysqlDate(dxAppliedDate.Text).Split('/')[2] + "," + balayer.indiandateToMysqlDate(dxAppliedDate.Text).Split('/')[1] + "," + balayer.indiandateToMysqlDate(dxAppliedDate.Text).Split('/')[0] + ",1,8,0)");
                    int GetLoanAmount1 = balayer.GetInsertItem("insert into voucher (`DualTransactionKey`,`BranchID`,`CurrDate`,`voucher`.`Voucher_No`,`Voucher_Type`,`Head_Id`,`ChoosenDate`,`Narration`,`Amount`,`Series`,`ReceievedBy`,`Trans_Type`,`T_Day`,`T_Month`,`T_Year`,`Trans_Medium`,`RootID`,`Other_Trans_Type`) values (" + DualTransactionKey + "," +balayer.ToobjectstrEvenNull(Session["Branchid"]) + ",'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," + balayer.indiandateToMysqlDate(dxAppliedDate.Text).Replace("/", "") + "" + txtAOSanctionNumber.Text + ",'C',3,'" + balayer.indiandateToMysqlDate(dxAppliedDate.Text) + "','" + balayer.MySQLEscapeString( txtNarration.Text )+ "'," + txtAmount.Text + ",'LOAN','" + balayer.MySQLEscapeString( balayer.ToobjectstrEvenNull(Session["UserName"] )) + "',0," + balayer.indiandateToMysqlDate(dxAppliedDate.Text).Split('/')[2] + "," + balayer.indiandateToMysqlDate(dxAppliedDate.Text).Split('/')[1] + "," + balayer.indiandateToMysqlDate(dxAppliedDate.Text).Split('/')[0] + ",1,3,0)");
                    int transactionKey = Convert.ToInt32(balayer.GetSingleValue("SELECT TransactionKey FROM svcf.voucher where `voucher`.`DualTransactionKey`=" + DualTransactionKey + " and `voucher`.`BranchID`=" +balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and `voucher`.`Head_Id`=" + node + " and `voucher`.`RootID`=8"));
                    int insertTransLoan = balayer.GetInsertItem("insert into `svcf`.`transloan` (`transloan`.`DualTransactionKey`,`transloan`.`TransactionKey`,`transloan`.`EmployeeID`,`transloan`.`LoanHeadID`,`transloan`.`LoanAmount`,`transloan`.`ApplyedOn`,`transloan`.`ApprovedOn`,`transloan`.`AdminSanctionNo`,`transloan`.`Narration`,`transloan`.`Trans_Medium`,`transloan`.`ChequeNo`,`transloan`.`BankName`,`transloan`.`BranchID`) values (" + DualTransactionKey + "," + transactionKey + ",'" + ddlEmployeeNo.SelectedItem.Value + "'," + node + "," + txtAmount.Text + ",'" + balayer.indiandateToMysqlDate(dxAppliedDate.Text) + "','" + balayer.indiandateToMysqlDate(dxApprovedDate.Text) + "'," + txtAOSanctionNumber.Text + ",'" + balayer.MySQLEscapeString( txtNarration.Text )+ "',1," + txtChequeNo.Text + ",'" + balayer.MySQLEscapeString( ddlBankHead.SelectedItem.Text )+ "'," +balayer.ToobjectstrEvenNull(Session["Branchid"]) + ")");
                }
                else
                { }
                logger.Info("EmployeeLoan.aspx - btn_ok():  Completed: " + DateTime.Now + " by: " + Convert.ToString(Session["UserName"]) + "");
                ModalPopupExtender1.PopupControlID = "Panel1";
                ModalPopupExtender1.Show();
                Panel1.Visible = true;
                Label8.Text = "Status";
                Label9.Text = "Employee ID : " + balayer.MySQLEscapeString( ddlEmployeeNo.SelectedItem.Text ) + " and Amount :" + txtAmount.Text + " inserted Successfully!!!";
                Label9.ForeColor = System.Drawing.Color.Green;
            }
            catch (Exception error)
            {
              //  balayer.GetInsertItem("delete from `svcf`.`voucher` where uuid_from_bin(`voucher`.`DualTransactionKey`)=uuid_from_bin(" + DualTransactionKey + ")");
               // balayer.GetInsertItem("delete from `svcf`.`transloan` where uuid_from_bin(`voucher`.`DualTransactionKey`)=uuid_from_bin(" + DualTransactionKey + ")");
                logger.Info("EmployeeLoan.aspx - error():  Error:" + error.Message + ": " + DateTime.Now + " by: " + Convert.ToString(Session["UserName"]) + "");
                ScriptManager.RegisterStartupScript(this, GetType(), "Warning", "alert('" + error.Message.ToString().Replace("'", "\\'") + "');", true);
            }
            finally
            { }
        }
        protected void btnPayLoan_Click(object sender, EventArgs e)
        {
            Page.Validate("Employee");
            if (!Page.IsValid)
            {
                return;
            }
            if (Session["CheckRefresh"].ToString() != ViewState["CheckRefresh"].ToString())
            {
                return;
            }
            ModalPopupExtender1.PopupControlID = "pnlmsg";
            ModalPopupExtender1.Show();
            pnlmsg.Visible = true;
            lblh.Text = "Status";
            lblcon.Text = "Do you want to insert for Employee Number : " + balayer.MySQLEscapeString( ddlEmployeeNo.SelectedItem.Text ) + " and Amount :" + txtAmount.Text + "";
            lblcon.ForeColor = System.Drawing.Color.Green;
        }
    }
}