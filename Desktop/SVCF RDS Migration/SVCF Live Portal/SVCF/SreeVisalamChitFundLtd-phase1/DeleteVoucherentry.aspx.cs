using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SVCF_BusinessAccessLayer;
using SVCF_TransactionLayer;
using SVCF_DataAccessLayer;
using System.Data;


namespace SreeVisalamChitFundLtd_phase1
{
    public partial class DeleteVoucherentry : System.Web.UI.Page
    {
    

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [System.Web.Services.WebMethod]
        public static string GetVoucherDefault()
        {
            DataSet ds = new DataSet();
            try
            {
                BusinessLayer blayer = new BusinessLayer();
                //IFormatProvider culture3 = new System.Globalization.CultureInfo("fr-FR", true);
                //DateTime fromdate = DateTime.Parse(frmdt, culture3, System.Globalization.DateTimeStyles.AssumeLocal);

                //IFormatProvider culture2 = new System.Globalization.CultureInfo("fr-FR", true);
                //DateTime tdate = DateTime.Parse(todate, culture2, System.Globalization.DateTimeStyles.AssumeLocal);

                string qry = "SELECT DATE_FORMAT( v1.`ChoosenDate`, '%d/%m/%Y') as ChoosenDate,v1.TransactionKey,uuid_from_bin(v1.DualTransactionKey) as key1, ht1.Node as Head, v1.Amount , v1. Voucher_No,v1.Series ,v1.Narration ,(case when v1.`Voucher_Type`='C' then 'Credit' else 'Debit' end) as Voucher_Type ,(case when v1.`Trans_Type`=0 then 'Voucher' else 'Receipt' end) as TransationType FROM voucher as v1 left Join headstree as ht1 on (v1.Head_Id=ht1.NodeID) where v1.`ChoosenDate` between '2016-09-07' and '2016-09-07' and v1.`BranchID`=1481 and ( v1.Trans_Type=0  or v1.Trans_Type=1) and `Other_Trans_Type`<>5";
                ds = blayer.GetDataSet(qry);
            }
            catch (Exception) { }
            return ds.GetXml();
        }

        [System.Web.Services.WebMethod]
        public static string VCList_Date(string frmdt, string todt)
        {
            DataSet ds = new DataSet();
            try
            {
                CommonClassFile objcls = new CommonClassFile();
                IFormatProvider culture3 = new System.Globalization.CultureInfo("fr-FR", true);
                DateTime fromdate = DateTime.Parse(frmdt, culture3, System.Globalization.DateTimeStyles.AssumeLocal);

                IFormatProvider culture2 = new System.Globalization.CultureInfo("fr-FR", true);
                DateTime tdate = DateTime.Parse(todt, culture2, System.Globalization.DateTimeStyles.AssumeLocal);

                string qry = "SELECT DATE_FORMAT( v1.`ChoosenDate`, '%d/%m/%Y') as ChoosenDate,v1.TransactionKey,uuid_from_bin(v1.DualTransactionKey) as key1, ht1.Node as Head, v1.Amount , v1. Voucher_No,v1.Series ,v1.Narration ,(case when v1.`Voucher_Type`='C' then 'Credit' else 'Debit' end) as Voucher_Type ,(case when v1.`Trans_Type`=0 then 'Voucher' else 'Receipt' end) as TransationType FROM voucher as v1 left Join headstree as ht1 on (v1.Head_Id=ht1.NodeID) where v1.`ChoosenDate` between '" + objcls.changeformat(fromdate, 2) + "' and '" + objcls.changeformat(tdate, 2) + "' and v1.`BranchID`=1481 and ( v1.Trans_Type=0  or v1.Trans_Type=1) and `Other_Trans_Type`<>5";
                ds = objcls.SelectDataset(qry);
            }
            catch (Exception) { }
            return ds.GetXml();
        }

        [System.Web.Services.WebMethod]
        public static void RemoveVoucher(string dualtkey)
        {
            TransactionLayer trn = new TransactionLayer();
            BusinessLayer blayer = new BusinessLayer();
            try
            {
                //TransactionKey, DualTransactionKey, BranchID, CurrDate, Voucher_No, Voucher_Type, Head_Id, ChoosenDate, Narration, Amount, Series, ReceievedBy,
                //Trans_Type, T_Day, T_Month, T_Year, MemberID, Trans_Medium, RootID, ChitGroupId, Other_Trans_Type, IsDeleted, IsAccepted, ApprovedDate,
                //CreatedDate, ModifiedDate, ISActive, AppReceiptno
                string DualTransactionKey = dualtkey;
                string qry = "";
                DataTable vcdt = new DataTable();
                qry = "select * from voucher where DualTransactionKey=" + DualTransactionKey;
                vcdt = blayer.GetDataTable(qry);
                DateTime presentdt = DateTime.Now;
                string ipaddress;
                //ipaddress =Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                //if (ipaddress == "" || ipaddress == null)
                ipaddress = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                // string hostname = Request.UserHostName;
                string hostname = HttpContext.Current.Request.Url.Host;
                hostname = hostname + ":" + ipaddress;
                for (int i = 0; i <= vcdt.Rows.Count - 1; i++)
                {
                    qry = "insert into voucherdeleted(TransactionKey, DualTransactionKey, BranchID, CurrDate, Voucher_No, Voucher_Type, Head_Id, ChoosenDate, Narration, " +
                        "Amount, Series, ReceievedBy,Trans_Type, T_Day, T_Month, T_Year, MemberID, Trans_Medium, RootID, ChitGroupId,Other_Trans_Type, IsDeleted," +
                        "ApprovedDate,CreatedDate, ModifiedDate, ISActive, AppReceiptno,removedDt,sysip) values(" + vcdt.Rows[i][0].ToString() + ",'" + vcdt.Rows[i][1].ToString() + "'," +
                        "" + vcdt.Rows[i][2].ToString() + ",'" + vcdt.Rows[i][3].ToString() + "'," + vcdt.Rows[i][4].ToString() + ",'" + vcdt.Rows[i][5].ToString() + "'," +
                        "'" + vcdt.Rows[i][6].ToString() + "','" + vcdt.Rows[i][7].ToString() + "','" + vcdt.Rows[i][8].ToString() + "'," + vcdt.Rows[i][9].ToString() + "," +
                        "'" + vcdt.Rows[i][10].ToString() + "','" + vcdt.Rows[i][11].ToString() + "'," + vcdt.Rows[i][12].ToString() + "," + vcdt.Rows[i][13].ToString() + "," +
                        "" + vcdt.Rows[i][14].ToString() + "," + vcdt.Rows[i][15].ToString() + "," + vcdt.Rows[i][16].ToString() + "," + vcdt.Rows[i][17].ToString() + "," +
                        "" + vcdt.Rows[i][18].ToString() + "," + vcdt.Rows[i][19].ToString() + "," + vcdt.Rows[i][20].ToString() + ",'" + vcdt.Rows[i][21].ToString() + "'," +
                        "'" + vcdt.Rows[i][22].ToString() + "','" + vcdt.Rows[i][23].ToString() + "','" + vcdt.Rows[i][24].ToString() + "','" + vcdt.Rows[i][25].ToString() + "'," +
                        "'" + vcdt.Rows[i][26].ToString() + "','" + blayer.GetChangeDatFormat(presentdt, 1) + "','" + hostname + "')";
                    trn.insertorupdateTrn(qry);
                }

                //                insert into voucherdeleted(TransactionKey, DualTransactionKey, BranchID, CurrDate, Voucher_No, Voucher_Type, Head_Id, ChoosenDate, Narration,
                //                    Amount, Series, ReceievedBy,Trans_Type, T_Day, T_Month, T_Year, MemberID, Trans_Medium, RootID, ChitGroupId,Other_Trans_Type, 
                //                    IsDeleted,ApprovedDate,CreatedDate, ModifiedDate, ISActive, AppReceiptno) values(10123272,'1ada0068-2d55-463f-b276-82376787b90a',3371,
                //                        '20/08/2016 16:36:06',105599,'C','1051','20/08/2016','CU-11/29:Sakthivel.S',15.00,'SH528','admin',1,208,2016,0,01,0,1,00,'','','','','',
                //'','');

                trn.insertorupdateTrn("delete FROM `svcf`.`voucher` where DualTransactionKey=" + DualTransactionKey);
                trn.insertorupdateTrn("delete FROM `svcf`.`transbank` where DualTransactionKey=" + DualTransactionKey);
                trn.insertorupdateTrn("delete FROM `svcf`.`transcourt` where DualTransactionKey=" + DualTransactionKey);
                trn.insertorupdateTrn("delete FROM `svcf`.`transloan` where DualTransactionKey=" + DualTransactionKey);
                trn.insertorupdateTrn("delete FROM `svcf`.`transprofitandloss` where DualTransactionKey=" + DualTransactionKey);
                trn.insertorupdateTrn("delete FROM `svcf`.`transadvance` where DualTransactionKey=" + DualTransactionKey);
                trn.insertorupdateTrn("delete FROM svcf.depositpayment where DualTransactionKey=" + DualTransactionKey);
                trn.insertorupdateTrn("delete FROM svcf.fd where DualTransactionKey=" + DualTransactionKey);
                trn.insertorupdateTrn("delete FROM svcf.filingfees where DualTransactionKey=" + DualTransactionKey);
                trn.insertorupdateTrn("delete FROM svcf.branchpayment where DualTransactionKey=" + DualTransactionKey);
                trn.CommitTrn();                
            }
            catch (Exception ex)
            {
                try
                {
                    trn.RollbackTrn();
                }
                catch { }
                finally { }
            }
            finally
            {
                trn.DisposeTrn();
            }
        }
    }
}