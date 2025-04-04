using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxMenu;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrintingLinks;
using System.IO;
using DevExpress.Web.ASPxGridView.Export;
using System.Text;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using SVCF_BusinessAccessLayer;
using SVCF_DataAccessLayer;
using SVCF_TransactionLayer;
using System.Globalization;

namespace SreeVisalamChitFundLtd_phase1
{
    public partial class TRRReport : System.Web.UI.Page
    {
        #region Object
        BusinessLayer balayer = new BusinessLayer();
        TransactionLayer trn = new TransactionLayer();
        CommonVariables objCOM = new CommonVariables();
        #endregion

        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (Session["UserName"] == null || Session["Branchid"] == null || Session["BranchName"] == null)
            {
                Response.Redirect(Page.ResolveUrl("~/Login.aspx"), true);
            }
        }
        private System.Drawing.Image headerImage;
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                DateTime dddd = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                txtFromDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtToDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                select();
                lblBranch.Text = "Branch : " + balayer.ToobjectstrEvenNull(Session["BranchName"]);
            }
            //select();
        }

        //protected void select()
        //{
        //    DataTable dtTRR = new DataTable();
        //    DataTable dtOther = new DataTable();
        //    DataTable dtPandL = new DataTable();
        //    DataTable dtBind = new DataTable();
        //    dtBind.Columns.Add("slno");
        //    dtBind.Columns.Add("Voucher_No", typeof(string));
        //    dtBind.Columns.Add("Narration", typeof(string));
        //    dtBind.Columns.Add("ChitNumber", typeof(string));
        //    dtBind.Columns.Add("MemberName");
        //    dtBind.Columns.Add("ChitAmount");
        //    dtBind.Columns.Add("Heads");
        //    dtBind.Columns.Add("Amount");
        //    dtBind.Columns.Add("OtherAmount");
        //    dtBind.Columns.Add("GrandTotal");
        //    dtBind.Columns.Add("Remarks");
        //    DataRow dr = dtBind.NewRow();
        //    DataTable dtDate = balayer.GetDataTable("select distinct ChoosenDate from voucher where Trans_Type<>2 and Voucher_Type='C' and BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and choosenDate between '" + balayer.indiandateToMysqlDate(txtFromDate.Text) + "' and '" + balayer.indiandateToMysqlDate(txtToDate.Text) + "' and Trans_Medium=1 and Other_Trans_Type not in (3,5) order by ChoosenDate asc");
        //    for (int i = 0; i < dtDate.Rows.Count; i++)
        //    {
        //        dtTRR = balayer.GetDataTable("select distinct v1.TransactionKey,v1.`CurrDate`,v1.`Voucher_No`,v1.Series,ht1.Node as ChitNumber, v1.`ChoosenDate`, REPLACE( v1.Narration,'  ','') as Narration,concat( m2.MemberID,' | ', m1.MemberName) as MemberName,  v1.Amount as ChitAmount from voucher as v1  join headstree as ht1 on (v1.Head_Id=ht1.NodeID) left join `membertogroupmaster` as m1 on (v1.Head_Id=m1.Head_Id) left join membermaster as m2 on (v1.MemberID=m2.MemberIdNew) where v1.RootID=5 and v1.IsAccepted=1 and v1.Trans_Type<>2 and Voucher_Type='C' and v1.BranchID =" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and v1.choosenDate ='" + balayer.indiandateToMysqlDate(balayer.ToobjectstrEvenNull(dtDate.Rows[i][0])) + "' and v1.Trans_Medium=1 and v1.Other_Trans_Type not in (3,5)");
        //        dtOther = balayer.GetDataTable("SELECT distinct v1.TransactionKey,v1.`CurrDate`,v1.`Voucher_No`,v1.Series,ht1.Node as OtherBranch, v1.`ChoosenDate`,REPLACE( v1.Narration,'  ','') as Narration, v1.Amount as BranchAmount FROM svcf.voucher as v1 join voucher as v2 on (v1.DualTransactionKey=v2.DualTransactionKey) join headstree as ht1 on (v1.Head_Id=ht1.NodeID) where v1.Rootid=1 and v1.Trans_Medium=1 and v1.Trans_Type=1 and v1.IsAccepted=1 and v1.ChoosenDate='" + balayer.indiandateToMysqlDate(balayer.ToobjectstrEvenNull(dtDate.Rows[i][0])) + "'  and v1.branchid=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and v1.voucher_type='C' and v2.rootid=3");
        //        dtPandL = balayer.GetDataTable("SELECT distinct v1.TransactionKey,v1.`CurrDate`,v1.`Voucher_No`,v1.Series,h1.Node as Heads, v1.`ChoosenDate`, REPLACE( v1.Narration,'  ','') as Narration, v1.Amount as HeadAmount FROM svcf.voucher as v1 join headstree as h1 on (v1.Head_Id=h1.NodeID) join headstree as h2 on (h1.ParentID=h2.NodeID) where v1.Rootid not in (1,5) and v1.Trans_Medium in (1,3) and v1.Trans_Type=1 and v1.Voucher_Type='C' and v1.IsAccepted=1 and v1.BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and v1.ChoosenDate='" + balayer.indiandateToMysqlDate(balayer.ToobjectstrEvenNull(dtDate.Rows[i][0])) + "'");
        //        dr["slno"] = "";
        //        dr["Voucher_No"] = "";
        //        dr["Narration"] = "Date : " + dtDate.Rows[i][0];
        //        dr["ChitNumber"] = "";
        //        dr["MemberName"] = "";
        //        dr["ChitAmount"] = "";
        //        dr["Heads"] = "";
        //        dr["Amount"] = "";
        //        dr["OtherAmount"] = "";
        //        dr["GrandTotal"] = "";
        //        dr["Remarks"] = "";
        //        dtBind.Rows.Add(dr.ItemArray);
        //        int count = 0;
        //        for (int j = 0; j < dtTRR.Rows.Count; j++)
        //        {
        //            string strRange = balayer.GetSingleValue("SELECT moneycollid FROM svcf.assignreceiptbook WHERE " + dtTRR.Rows[j]["Voucher_No"] + " between receiptnofrom  and receiptnoto and receiptseries='" + dtTRR.Rows[j]["Series"] + "'");
        //            string strName = "";
        //            if (!string.IsNullOrEmpty(strRange))
        //            {
        //                strName = balayer.GetSingleValue("SELECT moneycollname FROM svcf.moneycollector where moneyCollid=" + strRange + "");
        //            }

        //            dr["slno"] = j + 1;
        //            count += 1;
        //            dr["Voucher_No"] = dtTRR.Rows[j]["Voucher_No"];
        //            dr["Narration"] = dtTRR.Rows[j]["Narration"]; ;
        //            dr["ChitNumber"] = dtTRR.Rows[j]["ChitNumber"]; ;
        //            dr["MemberName"] = dtTRR.Rows[j]["MemberName"]; ;
        //            dr["ChitAmount"] = dtTRR.Rows[j]["ChitAmount"];
        //            dr["Heads"] = "";
        //            dr["Amount"] = "";
        //            dr["OtherAmount"] = "";
        //            dr["GrandTotal"] = dtTRR.Rows[j]["ChitAmount"];
        //            dr["Remarks"] = strName;
        //            dtBind.Rows.Add(dr.ItemArray);
        //        }
        //        for (int j = 0; j < dtOther.Rows.Count; j++)
        //        {
        //            string strRange = balayer.GetSingleValue("SELECT moneycollid FROM svcf.assignreceiptbook WHERE " + dtOther.Rows[j]["Voucher_No"] + " between receiptnofrom  and receiptnoto and receiptseries='" + dtOther.Rows[j]["Series"] + "'");
        //            string strName = "";
        //            if (!string.IsNullOrEmpty(strRange))
        //            {
        //                strName = balayer.GetSingleValue("SELECT moneycollname FROM svcf.moneycollector where moneyCollid=" + strRange + "");
        //            }

        //            dr["slno"] = count + 1;
        //            count += 1;
        //            dr["Voucher_No"] = dtOther.Rows[j]["Voucher_No"];
        //            dr["Narration"] = dtOther.Rows[j]["Narration"]; ;
        //            dr["ChitNumber"] = "";
        //            dr["MemberName"] = "";
        //            dr["ChitAmount"] = "";
        //            dr["Heads"] = dtOther.Rows[j]["OtherBranch"];
        //            dr["Amount"] = "";
        //            dr["OtherAmount"] = dtOther.Rows[j]["BranchAmount"];
        //            dr["GrandTotal"] = dtOther.Rows[j]["BranchAmount"];
        //            dr["Remarks"] = strName;
        //            dtBind.Rows.Add(dr.ItemArray);
        //        }
        //        for (int j = 0; j < dtPandL.Rows.Count; j++)
        //        {
        //            string strRange = balayer.GetSingleValue("SELECT moneycollid FROM svcf.assignreceiptbook WHERE " + dtPandL.Rows[j]["Voucher_No"] + " between receiptnofrom  and receiptnoto and receiptseries='" + dtPandL.Rows[j]["Series"] + "'");
        //            string strName = "";
        //            if (!string.IsNullOrEmpty(strRange))
        //            {
        //                strName = balayer.GetSingleValue("SELECT moneycollname FROM svcf.moneycollector where moneyCollid=" + strRange + "");
        //            }

        //            dr["slno"] = count + 1;
        //            count += 1;
        //            dr["Voucher_No"] = dtPandL.Rows[j]["Voucher_No"];
        //            dr["Narration"] = dtPandL.Rows[j]["Narration"]; ;
        //            dr["ChitNumber"] = "";
        //            dr["MemberName"] = "";
        //            dr["ChitAmount"] = "";
        //            dr["Heads"] = dtPandL.Rows[j]["Heads"];
        //            dr["Amount"] = dtPandL.Rows[j]["HeadAmount"];
        //            dr["OtherAmount"] = "";
        //            dr["GrandTotal"] = dtPandL.Rows[j]["HeadAmount"];
        //            dr["Remarks"] = strName;
        //            dtBind.Rows.Add(dr.ItemArray);
        //        }
        //        object smTRR;
        //        smTRR = dtTRR.Compute("sum(ChitAmount)", "");
        //        object smPL;
        //        smPL = dtPandL.Compute("sum(HeadAmount)", "");
        //        object smB;
        //        smB = dtOther.Compute("sum(BranchAmount)", "");
        //        if (string.IsNullOrEmpty(balayer.ToobjectstrEvenNull(smTRR)))
        //            smTRR = 0.00M;
        //        if (string.IsNullOrEmpty(balayer.ToobjectstrEvenNull(smB)))
        //            smB = 0.00M;
        //        if (string.IsNullOrEmpty(balayer.ToobjectstrEvenNull(smPL)))
        //            smPL = 0.00M;
        //        dr["slno"] = "";
        //        dr["Voucher_No"] = "";
        //        dr["Narration"] = "";
        //        dr["ChitNumber"] = "";
        //        dr["MemberName"] = "Total";
        //        dr["ChitAmount"] = smTRR;
        //        dr["Heads"] = "";
        //        dr["Amount"] = Convert.ToDecimal(smPL);
        //        dr["OtherAmount"] = Convert.ToDecimal(smB);
        //        dr["GrandTotal"] = Convert.ToDecimal(smTRR) + Convert.ToDecimal(smPL) + Convert.ToDecimal(smB);
        //        dr["Remarks"] = "";
        //        dtBind.Rows.Add(dr.ItemArray);
        //        // grid.DataSource = dtBind;
        //        // grid.DataBind();

        //        GridTRRReport.DataSource = dtBind;
        //        GridTRRReport.DataBind();

        //        ViewState["CurrentData"] = dtBind;
        //    }
        //}
        protected void select()
        {
            try
            {
                DataTable dtTRR = new DataTable();
                DataTable dtOther = new DataTable();
                DataTable Trramt = new DataTable();
                DataTable dtBind = new DataTable();
                dtBind.Columns.Add("slno");
                dtBind.Columns.Add("Voucher_No", typeof(string));
                dtBind.Columns.Add("Narration", typeof(string));
                dtBind.Columns.Add("chequeno");
                dtBind.Columns.Add("chequedate");
                dtBind.Columns.Add("Bank");
                dtBind.Columns.Add("ChitNumber", typeof(string));
                dtBind.Columns.Add("MemberName");
                dtBind.Columns.Add("ChitAmount");
                dtBind.Columns.Add("Heads");
                dtBind.Columns.Add("Amount");
                dtBind.Columns.Add("OtherAmount");
                dtBind.Columns.Add("GrandTotal");
                dtBind.Columns.Add("Remarks");
                DataRow dr = dtBind.NewRow();
                string formattedDate = "";
                DataTable dtDate = balayer.GetDataTable("select distinct  date_format(ChoosenDate,'%Y/%m/%d') as ChoosenDate from voucher where Trans_Type<>2 and Voucher_Type='C' and BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and choosenDate between '" + balayer.indiandateToMysqlDate(txtFromDate.Text) + "' and '" + balayer.indiandateToMysqlDate(txtToDate.Text) + "' and Trans_Medium=1 and Other_Trans_Type not in (3,5) order by ChoosenDate asc");
                if (dtDate.Rows.Count > 0)
                {
                    for (int i = 0; i < dtDate.Rows.Count; i++)
                    {
                        DateTime parsedDateTime;

                        //  Trramt = balayer.GetDataTable("select distinct v1.TransactionKey,v1.`CurrDate`,v1.`Voucher_No`,v1.Series,ht1.Node as ChitNumber, v1.`ChoosenDate`, REPLACE( v1.Narration,'  ','') as Narration,concat( m2.MemberID,' | ', m1.MemberName) as MemberName,v1.Amount as AdviceAmount,tt.CustomersBankName,tt.DateInCheque,tt.ChequeDDNO from voucher as v1  join `membertogroupmaster` as m1 on (v1.Head_Id=m1.Head_Id) left join headstree as ht1 on (v1.Head_Id=ht1.NodeID)  left join membermaster as m2 on (v1.MemberID=m2.MemberIdNew) left join transbank as tt on (v1.TransactionKey=tt.TransactionKey) where v1.BranchID =" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and (v1.Series='ADVICE' and v1.Voucher_Type='C' and v1.Trans_Type=1 or v1.Series='voucher' and v1.Voucher_Type='C'  and v1.Trans_Type=0) and v1.choosenDate='" + balayer.indiandateToMysqlDate(txtToDate.Text) + "'");
                        //09/11/2022
                        //dtTRR = balayer.GetDataTable("select distinct v1.TransactionKey,v1.`CurrDate`,v1.`Voucher_No`,v1.Series,v1.ReceievedBy,ht1.Node as ChitNumber,null as heads, v1.`ChoosenDate`, REPLACE( v1.Narration,'  ','') as Narration,concat( m2.MemberID,' | ', m1.MemberName) as MemberName,  v1.Amount as ChitAmount,null as HeadAmount,tt.CustomersBankName,date_format(tt.DateInCheque,'%d/%m/%Y') as DateInCheque,tt.ChequeDDNO from voucher as v1  join headstree as ht1 on (v1.Head_Id=ht1.NodeID) left join `membertogroupmaster` as m1 on (v1.Head_Id=m1.Head_Id) left join membermaster as m2 on (v1.MemberID=m2.MemberIdNew) left join transbank as tt on (v1.DualTransactionKey=tt.DualTransactionKey) where v1.RootID=5 and v1.IsAccepted=0 and (v1.Voucher_Type='C'and ((v1.Trans_Medium=1 or v1.Trans_Medium=2))) and v1.Other_Trans_Type<>2 and v1.Trans_Type<>0 and v1.Other_Trans_Type not in (3,5) and v1.BranchID =" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and v1.choosenDate ='" + dtDate.Rows[i][0] + "'UNION select v1.TransactionKey,v1.`CurrDate`,v1.`Voucher_No`,v1.Series,v1.ReceievedBy,null as ChitNumber,h1.Node as Heads, v1.`ChoosenDate`, REPLACE( v1.Narration,'  ','') as Narration, null as MemberName,null as ChitAmount,v1.Amount as HeadAmount,null as CustomersBankName,null as DateInCheque,null as  ChequeDDNO FROM svcf.voucher as v1 join headstree as h1 on (v1.Head_Id=h1.NodeID) join headstree as h2 on (h1.ParentID=h2.NodeID) where v1.Rootid not in (1,5) and v1.Trans_Medium in (1,3) and v1.Trans_Type=1 and v1.Voucher_Type='C' and Series<>'salary' and v1.IsAccepted=0 and v1.BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and v1.ChoosenDate='" + dtDate.Rows[i][0] + "' order by Voucher_No");
                        //dtOther = balayer.GetDataTable("SELECT distinct v1.TransactionKey,v1.`CurrDate`,v1.`Voucher_No`,v1.Series,v1.ReceievedBy,ht1.Node as OtherBranch, v1.`ChoosenDate`,REPLACE( v1.Narration,'  ','') as Narration, v1.Amount as BranchAmount,tt.CustomersBankName,date_format(tt.DateInCheque,'%d/%m/%Y') as DateInCheque,tt.ChequeDDNO FROM svcf.voucher as v1 join voucher as v2 on (v1.DualTransactionKey=v2.DualTransactionKey) join headstree as ht1 on (v1.Head_Id=ht1.NodeID) left join transbank as tt on (v1.DualTransactionKey=tt.DualTransactionKey) where v1.Rootid=1 and v1.Trans_Medium=1 and v1.Trans_Type=1 and v1.IsAccepted=0 and v1.ChoosenDate='" + dtDate.Rows[i][0] + "'  and v1.branchid=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and v1.voucher_type='C' and v2.rootid=3");
                        //09/11/2022 bagya appreceiptno added
                        dtTRR = balayer.GetDataTable("select distinct v1.TransactionKey,v1.`CurrDate`,case when v1.AppReceiptno is null or v1.AppReceiptno='' then v1.Voucher_No else v1.AppReceiptno end as Voucher_No,v1.Series,v1.ReceievedBy,ht1.Node as ChitNumber,null as heads, v1.`ChoosenDate`, REPLACE( v1.Narration,'  ','') as Narration,concat( m2.MemberID,' | ', m1.MemberName) as MemberName,  v1.Amount as ChitAmount,null as HeadAmount,tt.CustomersBankName,date_format(tt.DateInCheque,'%d/%m/%Y') as DateInCheque,tt.ChequeDDNO from voucher as v1  join headstree as ht1 on (v1.Head_Id=ht1.NodeID) left join `membertogroupmaster` as m1 on (v1.Head_Id=m1.Head_Id) left join membermaster as m2 on (v1.MemberID=m2.MemberIdNew) left join transbank as tt on (v1.DualTransactionKey=tt.DualTransactionKey) where v1.RootID=5 and v1.IsAccepted=0 and (v1.Voucher_Type='C'and ((v1.Trans_Medium=1 or v1.Trans_Medium=2))) and v1.Other_Trans_Type<>2 and v1.Trans_Type<>0 and v1.Other_Trans_Type not in (3,5) and v1.BranchID =" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and v1.choosenDate ='" + dtDate.Rows[i][0] + "'UNION select v1.TransactionKey,v1.`CurrDate`,case when v1.AppReceiptno is null or v1.AppReceiptno='' then v1.Voucher_No else v1.AppReceiptno end as Voucher_No,v1.Series,v1.ReceievedBy,null as ChitNumber,h1.Node as Heads, v1.`ChoosenDate`, REPLACE( v1.Narration,'  ','') as Narration, null as MemberName,null as ChitAmount,v1.Amount as HeadAmount,null as CustomersBankName,null as DateInCheque,null as  ChequeDDNO FROM svcf.voucher as v1 join headstree as h1 on (v1.Head_Id=h1.NodeID) join headstree as h2 on (h1.ParentID=h2.NodeID) where v1.Rootid not in (1,5) and v1.Trans_Medium in (1,3) and v1.Trans_Type=1 and v1.Voucher_Type='C' and Series<>'salary' and v1.IsAccepted=0 and v1.BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and v1.ChoosenDate='" + dtDate.Rows[i][0] + "' order by Voucher_No");
                        dtOther = balayer.GetDataTable("SELECT distinct v1.TransactionKey,v1.`CurrDate`,case when v1.AppReceiptno is null or v1.AppReceiptno='' then v1.Voucher_No else v1.AppReceiptno end as Voucher_No,v1.Series,v1.ReceievedBy,ht1.Node as OtherBranch, v1.`ChoosenDate`,REPLACE( v1.Narration,'  ','') as Narration, v1.Amount as BranchAmount,tt.CustomersBankName,date_format(tt.DateInCheque,'%d/%m/%Y') as DateInCheque,tt.ChequeDDNO FROM svcf.voucher as v1 join voucher as v2 on (v1.DualTransactionKey=v2.DualTransactionKey) join headstree as ht1 on (v1.Head_Id=ht1.NodeID) left join transbank as tt on (v1.DualTransactionKey=tt.DualTransactionKey) where v1.Rootid=1 and v1.Trans_Medium=1 and v1.Trans_Type=1 and v1.IsAccepted=0 and v1.ChoosenDate='" + dtDate.Rows[i][0] + "'  and v1.branchid=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and v1.voucher_type='C' and v2.rootid=3");
                        //          DateTime ss = DateTime.ParseExact(dtDate.Rows[i][0].ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);

                        if (DateTime.TryParseExact(dtDate.Rows[i][0].ToString(), "yyyy/MM/dd",
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.None,
                    out parsedDateTime))
                        {
                            formattedDate = parsedDateTime.ToString("dd/MM/yyyy");
                        }
                        dr["slno"] = "";
                        dr["Voucher_No"] = "";
                        dr["Narration"] = "Date : " + formattedDate;
                        //   dr["Narration"] = "Date : " + txtToDate.Text + "";
                        dr["chequeno"] = "";
                        dr["chequedate"] = "";
                        dr["Bank"] = "";
                        dr["ChitNumber"] = "";
                        dr["MemberName"] = "";
                        dr["ChitAmount"] = "";
                        dr["Heads"] = "";
                        dr["Amount"] = "";
                        dr["OtherAmount"] = "";
                        dr["GrandTotal"] = "";
                        dr["Remarks"] = "";
                        dtBind.Rows.Add(dr.ItemArray);
                        int count = 0;


                        if(dtTRR.Rows.Count>0)
                        for (int j = 0; j < dtTRR.Rows.Count; j++)
                        {

                            dr["slno"] = j + 1;
                            count += 1;
                            dr["Voucher_No"] = dtTRR.Rows[j]["Voucher_No"];
                            dr["Narration"] = dtTRR.Rows[j]["Narration"]; ;
                            dr["ChitNumber"] = dtTRR.Rows[j]["ChitNumber"]; ;
                            dr["MemberName"] = dtTRR.Rows[j]["MemberName"]; ;
                            dr["ChitAmount"] = dtTRR.Rows[j]["ChitAmount"];
                            dr["Heads"] = "";
                            dr["Amount"] = "";
                            dr["OtherAmount"] = "";
                            dr["GrandTotal"] = dtTRR.Rows[j]["ChitAmount"];
                            dr["chequeno"] = dtTRR.Rows[j]["ChequeDDNO"];
                            dr["chequedate"] = dtTRR.Rows[j]["DateInCheque"];
                            dr["Bank"] = dtTRR.Rows[j]["CustomersBankName"];
                            dr["Remarks"] = dtTRR.Rows[j]["ReceievedBy"];

                            if (Convert.ToString(dtTRR.Rows[j]["ChitNumber"]) == "")
                            {
                                if (Convert.ToString(dtTRR.Rows[j]["Heads"]) != "")
                                {
                                    dr["Heads"] = dtTRR.Rows[j]["Heads"];
                                    dr["Amount"] = dtTRR.Rows[j]["HeadAmount"];
                                    dr["GrandTotal"] = dtTRR.Rows[j]["HeadAmount"];
                                }
                            }
                            dtBind.Rows.Add(dr.ItemArray);
                        }
                        if(dtOther.Rows.Count>0)
                        for (int j = 0; j < dtOther.Rows.Count; j++)
                        {
                            dr["slno"] = count + 1;
                            count += 1;
                            dr["Voucher_No"] = dtOther.Rows[j]["Voucher_No"];
                            dr["Narration"] = dtOther.Rows[j]["Narration"]; ;
                            dr["ChitNumber"] = "";
                            dr["MemberName"] = "";
                            dr["ChitAmount"] = "";
                            dr["Heads"] = dtOther.Rows[j]["OtherBranch"];
                            dr["Amount"] = "";
                            dr["chequeno"] = dtOther.Rows[j]["ChequeDDNO"];
                            dr["chequedate"] = dtOther.Rows[j]["DateInCheque"];
                            dr["Bank"] = dtOther.Rows[j]["CustomersBankName"];
                            dr["OtherAmount"] = dtOther.Rows[j]["BranchAmount"];
                            dr["GrandTotal"] = dtOther.Rows[j]["BranchAmount"];
                            dr["Remarks"] = dtOther.Rows[j]["ReceievedBy"];
                            dtBind.Rows.Add(dr.ItemArray);
                        }

                        //                        for (int j = 0; j < Trramt.Rows.Count; j++)
                        //                        {
                        //                            dr["slno"] = count + 1;
                        //                            count += 1;
                        //                            dr["Voucher_No"] = Trramt.Rows[j]["Voucher_No"];
                        //                            dr["Heads"] = "";
                        //                            dr["OtherAmount"] = "";
                        //                            dr["GrandTotal"] = Trramt.Rows[j]["AdviceAmount"];
                        //                            dr["Narration"] = Trramt.Rows[j]["Narration"]; ;
                        //                            dr["ChitNumber"] = Trramt.Rows[j]["ChitNumber"]; ;
                        //                            dr["MemberName"] = Trramt.Rows[j]["MemberName"];
                        //                            dr["Amount"] = "";
                        //                            dr["ChitAmount"] = "";
                        ////dr["AdviceAmount"] = Trramt.Rows[j]["AdviceAmount"];

                        //                            dtBind.Rows.Add(dr.ItemArray);
                        //                        }
                        object smTRR;
                        smTRR = dtTRR.Compute("sum(ChitAmount)", "");
                        object smPL;
                        smPL = dtTRR.Compute("sum(HeadAmount)", "");
                        object smB;
                        smB = dtOther.Compute("sum(BranchAmount)", "");
                        //object smB1;
                        //smB1 = Trramt.Compute("sum(AdviceAmount)", "");
                        if (string.IsNullOrEmpty(balayer.ToobjectstrEvenNull(smTRR)))
                            smTRR = 0.00M;
                        if (string.IsNullOrEmpty(balayer.ToobjectstrEvenNull(smB)))
                            smB = 0.00M;
                        if (string.IsNullOrEmpty(balayer.ToobjectstrEvenNull(smPL)))
                            smPL = 0.00M;
                        // if (string.IsNullOrEmpty(balayer.ToobjectstrEvenNull(smB1)))
                        //   smB1 = 0.00M;
                        dr["slno"] = "";
                        dr["Voucher_No"] = "";
                        dr["Narration"] = "";
                        dr["ChitNumber"] = "";
                        dr["chequeno"] = "Total";
                        dr["ChitAmount"] = smTRR;
                        //  dr["AdviceAmount"] = "";
                        dr["Heads"] = "";
                        dr["chequedate"] = "";
                        dr["Bank"] = "";
                        dr["Remarks"] = "";
                        dr["Amount"] = Convert.ToDecimal(smPL);
                        dr["OtherAmount"] = Convert.ToDecimal(smB);
                        // dr["AdviceAmount"] = Convert.ToDecimal(smB1);
                        dr["GrandTotal"] = Convert.ToDecimal(smTRR) + Convert.ToDecimal(smPL) + Convert.ToDecimal(smB);
                        dtBind.Rows.Add(dr.ItemArray);

                    }
                    GridTRRReport.DataSource = dtBind;
                    GridTRRReport.DataBind();
                    ViewState["CurrentData"] = dtBind;
                }
                else
                {
                    //dr["Narration"] = "Date : " + dtDate.Rows[i][0];
                    dr["Narration"] = "Date : " + txtToDate.Text + "";
                    dtBind.Rows.Add(dr.ItemArray);
                    GridTRRReport.DataSource = dtBind;
                    GridTRRReport.DataBind();
                }
            }
            catch (Exception ex)
            {
                string hh = ex.Message;
            }

        }
        protected void grid_HtmlRowPrepared(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewTableRowEventArgs e)
        {
            if (e.RowType != GridViewRowType.Data) return;
            string Voucher = balayer.ToobjectstrEvenNull(e.GetValue("Voucher_No"));
            string narration = balayer.ToobjectstrEvenNull(e.GetValue("Narration"));
            if (string.IsNullOrEmpty(Voucher) | string.IsNullOrEmpty(narration))
            {
                e.Row.ForeColor = System.Drawing.Color.Red;
                e.Row.Font.Bold = true;
            }
        }
        protected void BtnStatisticsGo_Click(object sender, EventArgs e)
        {
            select();
        }

        protected void exporter_RenderBrick(object sender, ASPxGridViewExportRenderingEventArgs e)
        {
            //GridViewDataColumn dataColumn = e.Column as GridViewDataColumn;
            //if (dataColumn != null & e.RowType != GridViewRowType.Header)
            //{
            //    if (dataColumn.FieldName == "slno")
            //    {
            //        if (string.IsNullOrEmpty(balayer.ToobjectstrEvenNull(e.GetValue("Voucher_No"))) | string.IsNullOrEmpty(balayer.ToobjectstrEvenNull(e.GetValue("Narration"))))
            //        {
            //            e.BrickStyle.BackColor = Color.Wheat;
            //            e.BrickStyle.ForeColor = Color.White;
            //        }
            //    }
            //    if (dataColumn.FieldName == "Voucher_No")
            //    {
            //        if (string.IsNullOrEmpty(balayer.ToobjectstrEvenNull(e.GetValue("Voucher_No"))) | string.IsNullOrEmpty(balayer.ToobjectstrEvenNull(e.GetValue("Narration"))))
            //        {
            //            e.BrickStyle.BackColor = Color.Wheat;
            //            e.BrickStyle.ForeColor = Color.White;
            //        }
            //    }
            //    if (dataColumn.FieldName == "Narration")
            //    {
            //        if (string.IsNullOrEmpty(balayer.ToobjectstrEvenNull(e.GetValue("Voucher_No"))) | string.IsNullOrEmpty(balayer.ToobjectstrEvenNull(e.GetValue("Narration"))))
            //        {
            //            e.BrickStyle.BackColor = Color.Wheat;
            //            e.BrickStyle.ForeColor = Color.White;
            //        }
            //    }
            //    if (dataColumn.FieldName == "ChitNumber")
            //    {
            //        if (string.IsNullOrEmpty(balayer.ToobjectstrEvenNull(e.GetValue("Voucher_No"))) | string.IsNullOrEmpty(balayer.ToobjectstrEvenNull(e.GetValue("Narration"))))
            //        {
            //            e.BrickStyle.BackColor = Color.Wheat;
            //            e.BrickStyle.ForeColor = Color.White;
            //        }
            //    }
            //    if (dataColumn.FieldName == "MemberName")
            //    {
            //        if (string.IsNullOrEmpty(balayer.ToobjectstrEvenNull(e.GetValue("Voucher_No"))) | string.IsNullOrEmpty(balayer.ToobjectstrEvenNull(e.GetValue("Narration"))))
            //        {
            //            e.BrickStyle.BackColor = Color.Wheat;
            //            e.BrickStyle.ForeColor = Color.White;
            //        }
            //    }
            //    if (dataColumn.FieldName == "ChitAmount")
            //    {
            //        if (string.IsNullOrEmpty(balayer.ToobjectstrEvenNull(e.GetValue("Voucher_No"))) | string.IsNullOrEmpty(balayer.ToobjectstrEvenNull(e.GetValue("Narration"))))
            //        {
            //            e.BrickStyle.BackColor = Color.Wheat;
            //            e.BrickStyle.ForeColor = Color.White;
            //        }
            //    }
            //    if (dataColumn.FieldName == "Heads")
            //    {
            //        if (string.IsNullOrEmpty(balayer.ToobjectstrEvenNull(e.GetValue("Voucher_No"))) | string.IsNullOrEmpty(balayer.ToobjectstrEvenNull(e.GetValue("Narration"))))
            //        {
            //            e.BrickStyle.BackColor = Color.Wheat;
            //            e.BrickStyle.ForeColor = Color.White;
            //        }
            //    }
            //    if (dataColumn.FieldName == "Amount")
            //    {
            //        if (string.IsNullOrEmpty(balayer.ToobjectstrEvenNull(e.GetValue("Voucher_No"))) | string.IsNullOrEmpty(balayer.ToobjectstrEvenNull(e.GetValue("Narration"))))
            //        {
            //            e.BrickStyle.BackColor = Color.Wheat;
            //            e.BrickStyle.ForeColor = Color.White;
            //        }
            //    }
            //    if (dataColumn.FieldName == "GrandTotal")
            //    {
            //        if (string.IsNullOrEmpty(balayer.ToobjectstrEvenNull(e.GetValue("Voucher_No"))) | string.IsNullOrEmpty(balayer.ToobjectstrEvenNull(e.GetValue("Narration"))))
            //        {
            //            e.BrickStyle.BackColor = Color.Wheat;
            //            e.BrickStyle.ForeColor = Color.White;
            //        }
            //    }

            //}
        }
        protected void Export_click(object sender, MenuItemEventArgs e)
        {
            if (e.Item.Text.ToString() == "PDF")
            {
                using (this.headerImage = System.Drawing.Image.FromFile(Server.MapPath("~\\Images\\visalam.png")))
                {
                    PrintingSystem ps = new PrintingSystem();
                    PrintableComponentLink grid = new PrintableComponentLink(ps);
                    //grid.Component = GridTRRReport;

                    Link header = new Link();
                    CompositeLink compositeLink = new CompositeLink(ps);
                    header.CreateDetailArea += new CreateAreaEventHandler(header_CreateDetailArea);
                    compositeLink.Links.AddRange(new object[] { header, grid });
                    string leftColumn = "Pages : [Page # of Pages #]";
                    string rightColumn = "Date from: " + txtFromDate.Text + " to: " + txtToDate.Text;
                    PageHeaderFooter phf = compositeLink.PageHeaderFooter as PageHeaderFooter;
                    phf.Footer.Content.Clear();
                    phf.Footer.Content.AddRange(new string[] { rightColumn, leftColumn });
                    phf.Footer.LineAlignment = BrickAlignment.Center;
                    using (MemoryStream stream = new MemoryStream())
                    {
                        compositeLink.PaperKind = System.Drawing.Printing.PaperKind.A4;
                        compositeLink.CreateDocument(false);

                        compositeLink.PrintingSystem.Document.AutoFitToPagesWidth = 1;
                        compositeLink.PrintingSystem.ExportToPdf(stream);
                        WriteToResponse("_11TransferRemittanceRegister", true, "pdf", stream);
                    }
                }
            }
        }
        protected void imgpdf_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                DataTable captiondt = new DataTable();
                string tablecaption = "";
                captiondt = balayer.GetDataTable("SELECT `groupmaster`.`NoofMembers`,`groupmaster`.`GROUPNO`,`branchdetails`.`B_Name`,max(`auctiondetails`.`DrawNO`) as `RunningCall`,`groupmaster`.`ChitValue` FROM svcf.groupmaster join auctiondetails on (`groupmaster`.`Head_Id`=`auctiondetails`.`GroupID`) join `branchdetails` on (`groupmaster`.`BranchID`=`branchdetails`.`Head_Id`) where  `auctiondetails`.`AuctionDate`<='" + balayer.indiandateToMysqlDate(txtFromDate.Text) + "'  and `auctiondetails`.`BranchID`=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + ";");
                //objCOM.Str = Convert.ToString(captiondt.Rows[0]["RunningCall"]) == "" ? "0" : Convert.ToString(captiondt.Rows[0]["RunningCall"]);
                tablecaption = "Sree Visalam Chit Fund Limited.,<br/>TRR Report <br/> Branch Name : " + captiondt.Rows[0]["B_Name"].ToString();

                GridTRRReport.DataSource = (DataTable)ViewState["CurrentData"];
                GridTRRReport.DataBind();
                Phrase phrase = null;
                StringBuilder sb = new StringBuilder();
                // DataTable GH = (DataTable)ViewState["CurrentData"];

                phrase = new Phrase();
                phrase.Add(new Chunk("\t\t\t\t\t\t\t\t Sree Visalam Chit Fund Limited.,\n", FontFactory.GetFont("TIMES_ROMAN", 16, 3, iTextSharp.text.Color.BLACK)));
                phrase.Add(new Chunk("\t\t\t\t\t\t\t\tTRRReport\n", FontFactory.GetFont("Arial", 12, iTextSharp.text.Color.BLACK)));
                phrase.Add(new Chunk("\t\t\t\t\t\t\t\t Branch Name : " + captiondt.Rows[0]["B_Name"].ToString()));
                //   phrase.Add(new Chunk("\t\t\t\t\t\t\t\t Group No : " + captiondt.Rows[0]["GROUPNO"].ToString() + ";\t Chit Value : " + captiondt.Rows[0]["ChitValue"].ToString() + "\n", FontFactory.GetFont("Arial", 13, iTextSharp.text.Font.NORMAL, iTextSharp.text.Color.BLACK)));
                //  phrase.Add(new Chunk("\t\t\t\t\t\t\t\t As On " + txtFromDate.Text, FontFactory.GetFont("Arial", 13, iTextSharp.text.Font.NORMAL, iTextSharp.text.Color.BLACK)));

                BaseFont basefont = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false);
                iTextSharp.text.Font fnt = iTextSharp.text.FontFactory.GetFont(iTextSharp.text.FontFactory.HELVETICA, 12);



                iTextSharp.text.Table table = new iTextSharp.text.Table(GridTRRReport.Columns.Count);

                iTextSharp.text.Cell cellcaption = new iTextSharp.text.Cell();
                cellcaption.Add(new Phrase(phrase));
                cellcaption.Colspan = GridTRRReport.Columns.Count;
                cellcaption.HorizontalAlignment = PdfCell.ALIGN_CENTER;
                cellcaption.VerticalAlignment = PdfCell.ALIGN_TOP;



                table.AddCell(cellcaption);
                table.Cellpadding = 2;
                int[] widths = new int[GridTRRReport.Columns.Count];
                // int[] widths = { 12, 10, 26, 10 };
                for (int x = 0; x < GridTRRReport.Columns.Count; x++)
                {
                    widths[x] = (int)GridTRRReport.Columns[x].ItemStyle.Width.Value;
                    string cellText = Server.HtmlDecode(GridTRRReport.HeaderRow.Cells[x].Text);
                    iTextSharp.text.Cell cell = new iTextSharp.text.Cell(cellText);
                    cell.BackgroundColor = new iTextSharp.text.Color(System
                                       .Drawing.ColorTranslator.FromHtml("#DCDCDC"));
                    GridTRRReport.Columns[0].ItemStyle.Width = 40;
                    GridTRRReport.Columns[1].ItemStyle.Width = 40;
                    GridTRRReport.Columns[2].ItemStyle.Width = 40;
                    GridTRRReport.Columns[3].ItemStyle.Width = 100;
                    GridTRRReport.Columns[4].ItemStyle.Width = 50;
                    GridTRRReport.Columns[5].ItemStyle.Width = 40;
                    GridTRRReport.Columns[6].ItemStyle.Width = 40;
                    GridTRRReport.Columns[7].ItemStyle.Width = 100;
                    GridTRRReport.Columns[8].ItemStyle.Width = 40;
                    GridTRRReport.Columns[9].ItemStyle.Width = 40;
                    GridTRRReport.Columns[10].ItemStyle.Width = 40;
                    GridTRRReport.Columns[11].ItemStyle.Width = 40;
                    GridTRRReport.Columns[12].ItemStyle.Width = 40;
                    // GridTRRReport.Columns[13].ItemStyle.Width = 40;
                    // GridTRRReport.Columns[14].ItemStyle.Width = 40;
                    table.AddCell(cell);
                }
                table.SetWidths(widths);

                //Transfer rows from GridView to table
                for (int i = 0; i < GridTRRReport.Rows.Count; i++)
                {
                    BaseFont bfTimes = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false);
                    iTextSharp.text.Font font20 = iTextSharp.text.FontFactory.GetFont
                    (iTextSharp.text.FontFactory.HELVETICA, 12);

                    if (GridTRRReport.Rows[i].RowType == DataControlRowType.DataRow)
                    {

                        Label lblsno = (Label)GridTRRReport.Rows[i].FindControl("lblslno");
                        string cellText1 = Server.HtmlDecode
                                          (lblsno.Text);

                        iTextSharp.text.Cell cell1 = new iTextSharp.text.Cell();
                        cell1.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                        cell1.VerticalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                        cell1.Add(new Phrase(cellText1, font20));
                        //Set Color of Alternating row
                        if (i % 2 != 0)
                        {
                            cell1.BackgroundColor = new iTextSharp.text.Color(System.Drawing
                                                .ColorTranslator.FromHtml("#FFFFFF"));

                        }
                        table.AddCell(cell1);
                        //2nd column
                        Label lblmem = (Label)GridTRRReport.Rows[i].FindControl("lblrcno");
                        string cellText2 = Server.HtmlDecode
                                          (lblmem.Text);
                        iTextSharp.text.Cell cell2 = new iTextSharp.text.Cell();
                        cell2.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                        cell2.VerticalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                        cell2.Add(new Phrase(cellText2, font20));

                        //Set Color of Alternating row
                        if (i % 2 != 0)
                        {
                            cell2.BackgroundColor = new iTextSharp.text.Color(System.Drawing
                                                .ColorTranslator.FromHtml("#FFFFFF"));
                        }
                        table.AddCell(cell2);

                        //3rd column
                        Label lblcrd = (Label)GridTRRReport.Rows[i].FindControl("lblchitno");
                        string cellText3 = Server.HtmlDecode
                                          (lblcrd.Text);
                        iTextSharp.text.Cell cell3 = new iTextSharp.text.Cell();
                        cell3.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                        cell3.VerticalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                        cell3.Add(new Phrase(cellText3, font20));

                        //Set Color of Alternating row
                        if (i % 2 != 0)
                        {
                            cell3.BackgroundColor = new iTextSharp.text.Color(System.Drawing
                                                .ColorTranslator.FromHtml("#FFFFFF"));
                        }
                        table.AddCell(cell3);

                        //4th column
                        Label lbldeb = (Label)GridTRRReport.Rows[i].FindControl("lblnarr");
                        string cellText4 = Server.HtmlDecode(lbldeb.Text);

                        iTextSharp.text.Cell cell4 = new iTextSharp.text.Cell();

                        cell4.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                        cell4.VerticalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                        cell4.Add(new Phrase(cellText4, font20));

                        //Set Color of Alternating row
                        if (i % 2 != 0)
                        {
                            cell4.BackgroundColor = new iTextSharp.text.Color(System.Drawing
                                                .ColorTranslator.FromHtml("#FFFFFF"));
                        }
                        table.AddCell(cell4);

                        //5th column
                        Label lblexremit = (Label)GridTRRReport.Rows[i].FindControl("lblchequeno");
                        string cellText5 = Server.HtmlDecode(lblexremit.Text);

                        iTextSharp.text.Cell cell5 = new iTextSharp.text.Cell();
                        cell5.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                        cell5.VerticalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                        cell5.Add(new Phrase(cellText5, font20));

                        //Set Color of Alternating row
                        if (i % 2 != 0)
                        {
                            cell5.BackgroundColor = new iTextSharp.text.Color(System.Drawing
                                                .ColorTranslator.FromHtml("#FFFFFF"));
                        }
                        table.AddCell(cell5);

                        //6th column
                        Label lblnparrear = (Label)GridTRRReport.Rows[i].FindControl("lblchitdate");
                        string cellText6 = Server.HtmlDecode
                                          (lblnparrear.Text);

                        iTextSharp.text.Cell cell6 = new iTextSharp.text.Cell();
                        cell6.Add(new Phrase(cellText6, font20));
                        cell6.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                        cell6.VerticalAlignment = iTextSharp.text.Element.ALIGN_LEFT;

                        //Set Color of Alternating row
                        if (i % 2 != 0)
                        {
                            cell6.BackgroundColor = new iTextSharp.text.Color(System.Drawing
                                                .ColorTranslator.FromHtml("#FFFFFF"));
                        }
                        table.AddCell(cell6);

                        //7th column
                        Label lblparrear = (Label)GridTRRReport.Rows[i].FindControl("lblchitbank");
                        string cellText7 = Server.HtmlDecode(lblparrear.Text);

                        iTextSharp.text.Cell cell7 = new iTextSharp.text.Cell();
                        cell7.Add(new Phrase(cellText7, font20));
                        cell7.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                        cell7.VerticalAlignment = iTextSharp.text.Element.ALIGN_LEFT;

                        //Set Color of Alternating row
                        if (i % 2 != 0)
                        {
                            cell7.BackgroundColor = new iTextSharp.text.Color(System.Drawing
                                                .ColorTranslator.FromHtml("#FFFFFF"));
                        }
                        table.AddCell(cell7);

                        //8th column
                        //Label lblbranches = (Label)GridTRRReport.Rows[i].FindControl("lblmem_name");
                        //string cellText8 = Server.HtmlDecode(lblbranches.Text);

                        //iTextSharp.text.Cell cell8 = new iTextSharp.text.Cell();
                        //cell8.Add(new Phrase(cellText8, font20));

                        ////Set Color of Alternating row
                        //if (i % 2 != 0)
                        //{
                        //    cell8.BackgroundColor = new iTextSharp.text.Color(System.Drawing
                        //                        .ColorTranslator.FromHtml("#FFFFFF"));
                        //}
                        //table.AddCell(cell8);
                        //9colum
                        Label lblmobile = (Label)GridTRRReport.Rows[i].FindControl("lblchit_amnt");
                        string cellText9 = Server.HtmlDecode(lblmobile.Text);

                        iTextSharp.text.Cell cell9 = new iTextSharp.text.Cell();
                        cell9.HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT;
                        cell9.VerticalAlignment = iTextSharp.text.Element.ALIGN_RIGHT;
                        cell9.Add(new Phrase(cellText9, font20));

                        //Set Color of Alternating row
                        if (i % 2 != 0)
                        {
                            cell9.BackgroundColor = new iTextSharp.text.Color(System.Drawing
                                                .ColorTranslator.FromHtml("#FFFFFF"));
                        }
                        table.AddCell(cell9);

                        //10colum
                        Label lblchitamt = (Label)GridTRRReport.Rows[i].FindControl("lblamnt");
                        string cellText10 = Server.HtmlDecode(lblchitamt.Text);

                        iTextSharp.text.Cell cell11 = new iTextSharp.text.Cell();
                        cell11.HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT;
                        cell11.VerticalAlignment = iTextSharp.text.Element.ALIGN_RIGHT;
                        cell11.Add(new Phrase(cellText10, font20));

                        //Set Color of Alternating row
                        if (i % 2 != 0)
                        {
                            cell11.BackgroundColor = new iTextSharp.text.Color(System.Drawing
                                                .ColorTranslator.FromHtml("#FFFFFF"));
                        }
                        table.AddCell(cell11);
                        //11colum
                        Label lblchitothramt = (Label)GridTRRReport.Rows[i].FindControl("lblothr_amnt");
                        string cellText12 = Server.HtmlDecode(lblchitothramt.Text);

                        iTextSharp.text.Cell cell13 = new iTextSharp.text.Cell();
                        cell13.HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT;
                        cell13.VerticalAlignment = iTextSharp.text.Element.ALIGN_RIGHT;
                        cell13.Add(new Phrase(cellText12, font20));

                        //Set Color of Alternating row
                        if (i % 2 != 0)
                        {
                            cell13.BackgroundColor = new iTextSharp.text.Color(System.Drawing
                                                .ColorTranslator.FromHtml("#FFFFFF"));
                        }
                        table.AddCell(cell13);
                        //12colum
                        Label lblchithead = (Label)GridTRRReport.Rows[i].FindControl("lblheads");
                        string cellText13 = Server.HtmlDecode(lblchithead.Text);

                        iTextSharp.text.Cell cell14 = new iTextSharp.text.Cell();
                        cell14.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                        cell14.VerticalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                        cell14.Add(new Phrase(cellText13, font20));

                        //Set Color of Alternating row
                        if (i % 2 != 0)
                        {
                            cell14.BackgroundColor = new iTextSharp.text.Color(System.Drawing
                                                .ColorTranslator.FromHtml("#FFFFFF"));
                        }
                        table.AddCell(cell14);
                        //13colum
                        Label lblchitgrndtot = (Label)GridTRRReport.Rows[i].FindControl("lblgrnd_tot");
                        string cellText15 = Server.HtmlDecode(lblchitgrndtot.Text);

                        iTextSharp.text.Cell cell16 = new iTextSharp.text.Cell();
                        cell16.HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT;
                        cell16.VerticalAlignment = iTextSharp.text.Element.ALIGN_RIGHT;
                        cell16.Add(new Phrase(cellText15, font20));

                        //Set Color of Alternating row
                        if (i % 2 != 0)
                        {
                            cell16.BackgroundColor = new iTextSharp.text.Color(System.Drawing
                                                .ColorTranslator.FromHtml("#FFFFFF"));
                        }
                        table.AddCell(cell16);
                        //14colum
                        Label lblchitremark = (Label)GridTRRReport.Rows[i].FindControl("lblremarks");
                        string cellText16 = Server.HtmlDecode(lblchitremark.Text);

                        iTextSharp.text.Cell cell17 = new iTextSharp.text.Cell();
                        cell17.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                        cell17.VerticalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                        cell17.Add(new Phrase(cellText16, font20));

                        //Set Color of Alternating row
                        if (i % 2 != 0)
                        {
                            cell17.BackgroundColor = new iTextSharp.text.Color(System.Drawing
                                                .ColorTranslator.FromHtml("#FFFFFF"));
                        }
                        table.AddCell(cell17);

                    }
                }


                //////Total
                ////table.AddCell("");

                ////Label tot = (Label)row.FindControl("lbltexttotal");
                ////string celltextfooter1 = Server.HtmlDecode
                ////                  (tot.Text);
                ////iTextSharp.text.Cell cellfooter1 = new iTextSharp.text.Cell();
                ////cellfooter1.Add(new Phrase(celltextfooter1, fnt1));
                ////table.AddCell(celltextfooter1);

                //////Total Credit
                ////Label totcred = (Label)row.FindControl("totalcredit");
                ////string celltextfooter2 = Server.HtmlDecode
                ////                  (totcred.Text);
                ////iTextSharp.text.Cell cellfooter2 = new iTextSharp.text.Cell();
                ////cellfooter2.Add(new Phrase(celltextfooter2, fnt1));
                ////table.AddCell(cellfooter2);

                //////Total Debit
                ////Label totdeb = (Label)row.FindControl("totaldebit");
                ////string celltextfooter3 = Server.HtmlDecode
                ////                  (totdeb.Text);
                ////iTextSharp.text.Cell cellfooter3 = new iTextSharp.text.Cell();
                ////cellfooter3.Add(new Phrase(celltextfooter3, fnt1));
                ////table.AddCell(cellfooter3);

                //////Total Excess Remittance
                ////Label totalexcessremittance = (Label)row.FindControl("totalexcessremittance");
                ////string celltextfooter4 = Server.HtmlDecode
                ////                  (totalexcessremittance.Text);
                ////iTextSharp.text.Cell cellfooter4 = new iTextSharp.text.Cell();
                ////cellfooter4.Add(new Phrase(celltextfooter4, fnt1));
                ////table.AddCell(cellfooter4);

                //////Total total non prized arrier
                ////Label totalnparrier = (Label)row.FindControl("totalnparrier");
                ////string celltextfooter5 = Server.HtmlDecode
                ////                  (totalnparrier.Text);
                ////iTextSharp.text.Cell cellfooter5 = new iTextSharp.text.Cell();
                ////cellfooter5.Add(new Phrase(celltextfooter5, fnt1));
                ////table.AddCell(cellfooter5);

                //////Total total prized arrier
                ////Label totalparrier = (Label)row.FindControl("totalpaarrier");
                ////string celltextfooter6 = Server.HtmlDecode
                ////                  (totalparrier.Text);
                ////iTextSharp.text.Cell cellfooter6 = new iTextSharp.text.Cell();
                ////cellfooter6.Add(new Phrase(celltextfooter6, fnt1));
                ////table.AddCell(cellfooter6);
                ////table.AddCell("");

                //Summary - Non Prized Kasar
                double grandtotalbal = 0;
                double debitsum = 0;


                StringWriter sw = new StringWriter();
                HtmlTextWriter hw = new HtmlTextWriter(sw);
                PrintPanel1.RenderControl(hw);
                StringReader sr = new StringReader(sw.ToString());
                iTextSharp.text.Document pdfDoc = new iTextSharp.text.Document(PageSize.LEGAL.Rotate(), 0, 0, 19f, 0);
                // iTextSharp.text.Document pdfDoc = new iTextSharp.text.Document(PageSize.LEGAL.Rotate(), 10f, 10f, 10f, 0f);
                // pdfDoc.SetPageSize(new iTextSharp.text.Rectangle(1000f, 1150f));
                // pdfDoc.SetPageSize(new iTextSharp.text.Rectangle(520f, 800f));
                //pdfDoc.SetPageSize(new iTextSharp.text.Rectangle(10f, 10f, 10f, 0f));
                //    string leftColumn = "Pages : [Page # of Pages #]";
                pdfDoc.NewPage();
                HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
                PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                pdfDoc.Open();
                pdfDoc.Add(table);
                //       pdfDoc.Add("Left");
                pdfDoc.Close();
                //pdfDoc.Open();
                //htmlparser.Parse(sr);
                Response.ContentType = "application/pdf";
                Response.AddHeader("content-disposition", "attachment;filename=Trrreport.pdf");
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.Write(pdfDoc);
                Response.Flush();
            }

            catch (Exception err) { }

        }
        void header_CreateDetailArea(object sender, CreateAreaEventArgs e)
        {
            //e.Graph.BorderWidth = 0;
            //Rectangle r = new Rectangle(0, 0, 50, 50);
            //e.Graph.DrawImage(headerImage, r);
            //TextBrick tb = new TextBrick();
            //tb.Text = "SREE VISALAM CHIT FUND LTD.,";
            //tb.Font = new Font("Arial", 10, FontStyle.Bold);
            //tb.Rect = new RectangleF(50, 15, 260, 19);
            //tb.BorderWidth = 0;
            //tb.BackColor = Color.Transparent;
            //tb.HorzAlignment = DevExpress.Utils.HorzAlignment.Center;
            //tb.VertAlignment = DevExpress.Utils.VertAlignment.Top;
            //e.Graph.DrawBrick(tb);
            //TextBrick tb1 = new TextBrick();
            //tb1.Text = "BRANCH : " + Session["BranchName"];
            //tb1.Font = new Font("Arial", 9, FontStyle.Bold);
            //tb1.Rect = new RectangleF(50, 34, 260, 25);
            //tb1.BorderWidth = 0;
            //tb1.BackColor = Color.Transparent;
            //tb1.HorzAlignment = DevExpress.Utils.HorzAlignment.Center;
            //tb1.VertAlignment = DevExpress.Utils.VertAlignment.Top;
            //e.Graph.DrawBrick(tb1);
        }
        void WriteToResponse(string fileName, bool saveAsFile, string fileFormat, MemoryStream stream)
        {
            if (Page == null || Page.Response == null)
                return;
            string disposition = saveAsFile ? "attachment" : "inline";
            Page.Response.Clear();
            Page.Response.Buffer = false;
            Page.Response.AppendHeader("Content-Type", string.Format("application/{0}", fileFormat));
            Page.Response.AppendHeader("Content-Transfer-Encoding", "binary");
            Page.Response.AppendHeader("Content-Disposition",
                string.Format("{0}; filename={1}.{2}", disposition, fileName, fileFormat));
            Page.Response.BinaryWrite(stream.GetBuffer());
            Page.Response.End();
        }

        protected void GridTRRReport_RowCreated(object sender, GridViewRowEventArgs e)
        {
            try
            {
                // if (e.Row.RowType == DataControlRowType.Header) // If header created
                //{
                //    GridView Projectgrid = (GridView)sender;

                //    // Creating a Row
                //    GridViewRow HeaderRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);


                //    TableCell HeaderCell = new TableCell();
                //    HeaderCell.Text = "S. No.";
                //    HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                //    HeaderCell.RowSpan = 2;
                //    HeaderCell.ColumnSpan = 1;
                //    HeaderCell.CssClass = "HeaderStyle";
                //    HeaderRow.Cells.Add(HeaderCell);


                //    HeaderCell = new TableCell();
                //    HeaderCell.Text = "Receipt or Reference No.";
                //    HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                //    HeaderCell.RowSpan = 2;
                //    HeaderCell.ColumnSpan = 1;
                //    HeaderCell.CssClass = "HeaderStyle";
                //    HeaderRow.Cells.Add(HeaderCell);


                //    HeaderCell = new TableCell();
                //    HeaderCell.Text = "Chit Number";
                //    HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                //    HeaderCell.RowSpan = 2;
                //    HeaderCell.ColumnSpan = 1;
                //    HeaderCell.CssClass = "HeaderStyle";
                //    HeaderRow.Cells.Add(HeaderCell);


                //    HeaderCell = new TableCell();
                //    HeaderCell.Text = "Receipt or Reference No.";
                //    HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                //    HeaderCell.ColumnSpan = 3;
                //    HeaderCell.CssClass = "HeaderStyle";
                //    HeaderRow.Cells.Add(HeaderCell);


                //    HeaderCell = new TableCell();
                //    HeaderCell.Text = "Call No.";
                //    HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                //    HeaderCell.ColumnSpan = 3;
                //    HeaderCell.CssClass = "HeaderStyle";
                //    HeaderRow.Cells.Add(HeaderCell);

                //    HeaderCell = new TableCell();
                //    HeaderCell.Text = "Name";
                //    HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                //    HeaderCell.ColumnSpan = 3;
                //    HeaderCell.CssClass = "HeaderStyle";
                //    HeaderRow.Cells.Add(HeaderCell);


                //    HeaderCell = new TableCell();
                //    HeaderCell.Text = "Amount";
                //    HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                //    HeaderCell.ColumnSpan = 3;
                //    HeaderCell.CssClass = "HeaderStyle";
                //    HeaderRow.Cells.Add(HeaderCell);


                //    HeaderCell = new TableCell();
                //    HeaderCell.Text = "P & L A/c-Amount";
                //    HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                //    HeaderCell.ColumnSpan = 3;
                //    HeaderCell.CssClass = "HeaderStyle";
                //    HeaderRow.Cells.Add(HeaderCell);

                //    HeaderCell = new TableCell();
                //    HeaderCell.Text = "OtherBranch-Amount";
                //    HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                //    HeaderCell.ColumnSpan = 3;
                //    HeaderCell.CssClass = "HeaderStyle";
                //    HeaderRow.Cells.Add(HeaderCell);


                //    HeaderCell = new TableCell();
                //    HeaderCell.Text = "Branch Amount";
                //    HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                //    HeaderCell.ColumnSpan = 3;
                //    HeaderCell.CssClass = "HeaderStyle";
                //    HeaderRow.Cells.Add(HeaderCell);


                //    HeaderCell = new TableCell();
                //    HeaderCell.Text = "Branch Amount";
                //    HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                //    HeaderCell.ColumnSpan = 3;
                //    HeaderCell.CssClass = "HeaderStyle";
                //    HeaderRow.Cells.Add(HeaderCell);

                //    //Adding the Row at the 0th position (first row) in the Grid
                //    Projectgrid.Controls[0].Controls.AddAt(0, HeaderRow);
                // }
            }
            catch (Exception) { }
        }

        protected void imgpdf1_Click(object sender, ImageClickEventArgs e)
        {
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=GridViewRpt.pdf");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            GridTRRReport.AllowPaging = false;
            //LoadGrid();
            DataTable pdfdt = new DataTable();
            pdfdt = (DataTable)ViewState["CurrentData"];
            GridTRRReport.DataSource = pdfdt;
            GridTRRReport.DataBind();
            pdfdt.Dispose();
            //GV_MCArrear.DataBind();
            GridTRRReport.RenderControl(hw);
            StringReader sr = new StringReader(sw.ToString());
            iTextSharp.text.Document pdfDoc = new iTextSharp.text.Document(PageSize.A4, 20f, 20f, 20f, 20f);
            HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
            PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
            pdfDoc.Open();
            htmlparser.Parse(sr);
            pdfDoc.Close();
            Response.Write(pdfDoc);
            Response.End();
            ViewState["CurrentData"] = null;
            pdfdt.Dispose();
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }
    }
}