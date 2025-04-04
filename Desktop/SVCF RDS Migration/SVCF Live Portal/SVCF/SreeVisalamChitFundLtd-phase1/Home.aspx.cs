using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SVCF_BusinessAccessLayer;
using SVCF_TransactionLayer;
using SVCF_DataAccessLayer;
using System.Globalization;
using System.Security.Cryptography;
using System.Configuration;
using System.IO;
using System.Text;
using System.Data;
using log4net;
using log4net.Config;
namespace SreeVisalamChitFundLtd_phase1
{
    public partial class Home : System.Web.UI.Page
    {     
        #region VarDeclaration
        BusinessLayer balayer = new BusinessLayer();
        TransactionLayer tranlayer = new TransactionLayer();
        DataTable Getpwd = new DataTable();
      

        long pwdupdate = 0 ;
        string encrypwd = "";
        string valueCr = "";
        string valueDr = "";
        decimal decCr = 0;
        decimal decDr = 0;
        private static TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        #endregion
        ILog logger = log4net.LogManager.GetLogger(typeof(Home));

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {             
                cashbank.Visible = true;
                balayer.Disposeconnection();
                updateCash();
                updateBank();
                //Payment update for voucher [mismatched]
               // UpDateVoucherNumber_Payment();

                //Update Chit group id ---28.01.2019
                //ExecuteVoucher_ChitGroupid();

               balayer.Disposeconnection();

            }
            logger.Info("Logged into Site at: " + DateTime.Now);
        }

        public void ExecuteVoucher_ChitGroupid()
        {
            try
            {
                DataTable jj = balayer.GetDataTable("select uuid_from_bin(v1.DualTransactionKey) as dd,m1.GroupID as gid,v1.Voucher_No,v1.BranchID,v1.TransactionKey as tkey from svcf.voucher as v1 join svcf.membertogroupmaster as m1 on(v1.Head_Id=m1.Head_Id) where v1.Series='ADVICE' and v1.ChitGroupId=0");
                var qry = "";
                for (int i = 0; i < jj.Rows.Count; i++)
                {
                    //qry = "update  svcf.voucher set ChitGroupId=" + jj.Rows[i]["gid"] + " where uuid_from_bin(DualTransactionKey)='" + jj.Rows[i]["dd"] + "';";
                    qry = "update  svcf.voucher set ChitGroupId=" + jj.Rows[i]["gid"] + " where TransactionKey=" + jj.Rows[i]["tkey"] + ";";
                    long insertCmd = tranlayer.insertorupdateTrn(qry);
                }
            }
            catch (Exception err) { }
        }


        protected void tup_Tick(object sender, EventArgs e)
        {
            updateCash();
            updateBank();
        }
        protected void btn_RefreshCash_Click(object sender, EventArgs e)
        {
            updateCash();
        }
        protected void btn_Refreshbank_Click(object sender, EventArgs e)
        {
            updateBank();
        }
        protected void btn_ViewMoreCash_Click(object sender, EventArgs e)
        {
            dxPopup.HeaderText = "Cash Ledger";
            dxPopup.ContentUrl = "CashLedgerNew.aspx?fromDate=31/03/2008&toDate=" + DateTime.Now.ToString("dd/MM/yyyy");
            dxPopup.CloseButtonImage.Url = "Images\\Close.gif";
            dxPopup.ShowOnPageLoad = true;
        }
        protected void btn_ViewMoreBank_Click(object sender, EventArgs e)
        {
            dxPopup.HeaderText = "Bank Ledger";
            dxPopup.ContentUrl = "BanksLedgerNew.aspx?fromDate=31/03/2008&toDate=" + DateTime.Now.ToString("dd/MM/yyyy");
            dxPopup.CloseButtonImage.Url = "Images\\Close.gif";
            dxPopup.ShowOnPageLoad = true;
        }


        public void UpDateVoucherNumber_Payment()
        {
            try
            {
                string query = "";
                int VCNumber = 1;
                //  VCNumber = 460;
                query = "select distinct voucher_no,chitgroupid from svcf.voucher where branchid=" + Session["Branchid"] + " and Series='PAYMENT' and  Voucher_No>0 and choosendate>'2016/01/31' order by Voucher_No; ";
                var DistVoucherList = balayer.GetDataTable(query);
             
                foreach (DataRow dr in DistVoucherList.Rows)
                {
                    try
                    {
                        query = "select * from svcf.voucher where Voucher_No=" + dr.ItemArray[0] + " and Series='PAYMENT' and ChoosenDate>'2016/01/31' and branchid=" + Session["Branchid"] + "";
                        var DtVoucherList = balayer.GetDataTable(query);

                        var c_gpid = DtVoucherList.AsEnumerable().Select(x => x.Field<System.UInt32>("ChitGroupId")).Distinct().ToList();
                        foreach (var id in c_gpid)
                        {
                            if (c_gpid.Count > 1)
                            {
                                //var test = "";
                            }
                            try
                            {
                                // VCNumber = 455;
                                query = "update svcf.voucher set voucher_no=" + VCNumber + " where BranchID=" + Session["Branchid"] + " and Series='PAYMENT' and Voucher_No=" + dr.ItemArray[0] + " and ChitGroupId=" + id + "";
                                balayer.ExecuteQuery(query);
                                VCNumber = VCNumber + 1;

                            }
                            catch (Exception err)
                            {

                            }
                        }
                    }
                    catch (Exception err)
                    {

                    }
                }
            }
            catch (Exception err)
            {

            }
        }


        public void Memberadd()
        {
            try
            {
                var dtt = new DataTable();
                dtt = balayer.GetDataTable("select distinct BranchId from svcf.membermaster");

                foreach (DataRow dtRow in dtt.Rows)
                {
                    var ddd = balayer.GetDataTable("select * from svcf.membermaster where BranchId = " + dtRow[0] + "");
                    int increment = 1;
                    foreach (DataRow ddrow in ddd.Rows)
                    {
                        balayer.GetInsertItem("update svcf.membermaster SET MemberID = " + increment + " where MemberIDNew = " + ddrow["MemberIDNew"] + "");
                        increment = increment + 1;
                    }
                }
            }
            catch (Exception err) { }
        }

        void updateCash()
        {//cash
            valueCr = balayer.GetSingleValue("SELECT ifnull(Sum(Amount),0.00) as Amt FROM svcf.voucher WHERE RootID=12 and BranchID=" + Session["Branchid"].ToString() + "    and  Voucher_Type='C'");
            valueDr = balayer.GetSingleValue("SELECT ifnull(Sum(Amount),0.00) as Amt FROM svcf.voucher WHERE RootID=12 and BranchID=" + Session["Branchid"].ToString() + "    and  Voucher_Type='D'");
            
            decCr = decimal.Parse(valueCr);
            decDr = decimal.Parse(valueDr);          
            CultureInfo hindi = new CultureInfo("hi-IN");
            string amnt_withcomma = ""; 

            if (decDr > decCr)
            {
                amnt_withcomma= string.Format(hindi, "{0:c}", (decDr - decCr));
                lblCashTodayBalance.Text = "Dr." + amnt_withcomma;  //balayer.ConvertToIndCurrency((decDr - decCr).ToString());
                amnt_withcomma = "";
            }
            else
            {
                amnt_withcomma = string.Format(hindi, "{0:c}", (decCr - decDr));
                lblCashTodayBalance.Text = "Cr." + amnt_withcomma;   //balayer.ConvertToIndCurrency((decCr - decDr).ToString());
                amnt_withcomma = "";
            }
            DateTime indianTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);
            lblLastUpdatedCash.Text = "Last Updated On: " + indianTime; //DateTime.Now.ToString("dd/MMM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);
        }

            
        void updateBank()
        {//cash
            valueCr = balayer.GetSingleValue("SELECT ifnull(Sum(Amount),0.00) as Amt FROM svcf.voucher WHERE RootID=3 and BranchID=" + Session["Branchid"].ToString() + "    and  Voucher_Type='C'");
            valueDr = balayer.GetSingleValue("SELECT ifnull(Sum(Amount),0.00) as Amt FROM svcf.voucher WHERE RootID=3 and BranchID=" + Session["Branchid"].ToString() + "    and  Voucher_Type='D'");

            decCr = decimal.Parse(valueCr);
            decDr = decimal.Parse(valueDr);

            if (decDr > decCr)
            {
                lblbankTodayBalance.Text = "Dr." + balayer.ConvertToIndCurrency((decDr - decCr).ToString());
            }
            else
            {
                lblbankTodayBalance.Text = "Cr." + balayer.ConvertToIndCurrency((decCr - decDr).ToString());
            }
            DateTime indianTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);
            lblLastUpdatedbank.Text = "Last Updated On: " + indianTime; //DateTime.Now.ToString("dd/MMM/yyyy hh:mm:ss tt",CultureInfo.InvariantCulture);
        }
    }
}