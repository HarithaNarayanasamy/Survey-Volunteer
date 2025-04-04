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
using DevExpress.Web.ASPxMenu;
using DevExpress.Web.ASPxGridView;
using DevExpress.Data;
using System.IO;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrintingLinks;
using System.Drawing;
using SVCF_BusinessAccessLayer;
using SVCF_TransactionLayer;
using SVCF_DataAccessLayer;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html;
using iTextSharp.text.html.simpleparser;
using ClosedXML.Excel;

namespace SreeVisalamChitFundLtd_phase1
{
    public partial class DirectCashLedger : System.Web.UI.Page
    {
        #region VarDeclaration
        BusinessLayer balayer = new BusinessLayer();
        TransactionLayer tranlayer = new TransactionLayer();
        #endregion
        private System.Drawing.Image headerImage;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {            
                dateFromConsolidated.Text = DateTime.Now.ToString("dd/MM/yyyy");
                dateToConsolidated.Text = DateTime.Now.ToString("dd/MM/yyyy");                     
            }
           
        }            
     
           
        [System.Web.Services.WebMethod]
        public static CashLedger[] GetCashLedger(string frmdt,string todt)
        {
            DataTable fillDt = new DataTable();            
            List<CashLedger> CLList = new List<CashLedger>();
            try
            {
                string query = "";
                fillDt.Columns.Add("Date", typeof(string));
                fillDt.Columns.Add("Heads", typeof(string));
                fillDt.Columns.Add("Narration", typeof(string));
                fillDt.Columns.Add("Credit", typeof(string));
                fillDt.Columns.Add("Debit", typeof(string));
                
                DataRow drBind = fillDt.NewRow();
                BusinessLayer balayer = new BusinessLayer();

                DataTable crdt = new DataTable();
                
                //Get date             
                DateTime start = DateTime.ParseExact(frmdt, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime end = DateTime.ParseExact(todt, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DataTable Vouhcer = new DataTable();
                DataTable AdviceCrTble = new DataTable();
                DataTable AdviceDrTble = new DataTable();
                int RootID = 12;
                for (var datetcount = start; datetcount <= end; datetcount = datetcount.AddDays(1))
                {
                    
                    query = "select sum(Amount) as 'CRR Balance',choosendate as Date from voucher where  Voucher_Type='D'  and RootID=12 " +
                                 "and ChoosenDate='" + balayer.changedateformat(Convert.ToDateTime(datetcount), 2) + "' and  " +
                                 "branchid=" + balayer.ToobjectstrEvenNull(HttpContext.Current.Session["Branchid"]) + " and Trans_Type= 1 group by ChoosenDate;";
                            
                    crdt = balayer.GetDataTable(query);

                    Vouhcer = new DataTable();

                    if (Convert.ToInt32(HttpContext.Current.Session["Branchid"]) == 161)
                    {
                        query = "select TransactionKey, uuid_from_bin(DualTransactionKey) as 'DualTransactionKey', BranchID, CurrDate, Voucher_No, Voucher_Type, Head_Id, ChoosenDate, Narration, Amount, Series, ReceievedBy, Trans_Type, MemberID, Trans_Medium, RootID, ChitGroupId, Other_Trans_Type from voucher where series in ('VOUCHER','FilingFees','Filing_Fees','SALARY','BRANCH','DECREE') and ChoosenDate='" + balayer.changedateformat(Convert.ToDateTime(datetcount), 2) + "' and branchid in(160,161,162)";
                        Vouhcer = balayer.GetDataTable(query);
                    }
                    else
                    {
                        query = "select TransactionKey, uuid_from_bin(DualTransactionKey) as 'DualTransactionKey', BranchID, CurrDate, Voucher_No, Voucher_Type, Head_Id, ChoosenDate, Narration, Amount, Series, ReceievedBy, Trans_Type, MemberID, Trans_Medium, RootID, ChitGroupId, Other_Trans_Type from voucher where series in ('VOUCHER','FilingFees','Filing_Fees','SALARY','BRANCH','DECREE') and ChoosenDate='" + balayer.changedateformat(Convert.ToDateTime(datetcount), 2) + "' and branchid=" + balayer.ToobjectstrEvenNull(HttpContext.Current.Session["Branchid"]) + "";
                        Vouhcer = balayer.GetDataTable(query);
                    }

                    AdviceCrTble = new DataTable();
                    try
                    {
                        query = "SELECT t1.TransactionKey, uuid_from_bin(t1.DualTransactionKey) as 'DualTransactionKey', t1.BranchID, t1.CurrDate, t1.Voucher_No, t1.Voucher_Type, " +
                            "t1.Head_Id, t1.ChoosenDate, t1.Narration, t1.Amount, t1.Series, t1.ReceievedBy, t1.Trans_Type, t1.MemberID, t1.Trans_Medium, t1.RootID, t1.ChitGroupId," +
                            "t1.Other_Trans_Type FROM `svcf`.`voucher` as t1 left join membermaster as t2  on  t1.MemberID=t2.MemberIDNew join branchdetails as t3 on " +
                            "(t3.Head_Id=t1.BranchID) left join headstree as m1 on (get_ref_no(t1.Narration)=m1.NodeID) left join groupmaster as g1 on (t1.ChitGroupId=g1.Head_Id) " +
                            "where t1.Trans_Type=1 and t1.Other_Trans_Type=1 and t1.Head_Id=" + balayer.ToobjectstrEvenNull(HttpContext.Current.Session["Branchid"]) + " and t1.ChoosenDate='" + balayer.changedateformat(Convert.ToDateTime(datetcount), 2) + "'";
                        AdviceCrTble = balayer.GetDataTable(query);
                        var DualTKey_AdvCr = (AdviceCrTble.AsEnumerable().Select(row => row.Field<string>("DualTransactionKey"))).Distinct().ToList();
                        foreach (var key in DualTKey_AdvCr)
                        {
                            // AdviceDrTble = balayer.GetDataTable("select TransactionKey, uuid_from_bin(DualTransactionKey) as 'DualTransactionKey', BranchID, CurrDate, Voucher_No, Voucher_Type, Head_Id, ChoosenDate, Narration, Amount, Series, ReceievedBy, Trans_Type, MemberID, Trans_Medium, RootID, ChitGroupId, Other_Trans_Type from voucher where DualTransactionKey='" + key + "'");
                            AdviceDrTble = balayer.GetDataTable("select TransactionKey, DualTransactionKey as 'DualTransactionKey', BranchID, CurrDate, Voucher_No, Voucher_Type, Head_Id, ChoosenDate, Narration, Amount, Series, ReceievedBy, Trans_Type, MemberID, Trans_Medium, RootID, ChitGroupId, Other_Trans_Type from voucher where DualTransactionKey='" + key + "'");
                            Vouhcer.Merge(AdviceDrTble);
                        }
                    }
                    catch (Exception) { }
                    var DualTransactionkeyList = (Vouhcer.AsEnumerable().Select(row => row.Field<string>("DualTransactionKey"))).Distinct().ToList();

                    DateTime firstdt = DateTime.MinValue;



                    firstdt = Convert.ToDateTime(datetcount);
                    drBind["Date"] = firstdt;

                    //Get Openig balance of previous day
                    query = "select sum(case when (Voucher_Type='D' and ChoosenDate < '" + balayer.changedateformat(firstdt, 2) + "'  ) then Amount else 0.00 end)-sum(case " +
                             "when (Voucher_Type='C' and ChoosenDate < '" + balayer.changedateformat(firstdt, 2) + "'  ) then Amount else 0.00 end) as Cash  from voucher " +
                             "where voucher.BranchID=" + balayer.ToobjectstrEvenNull(HttpContext.Current.Session["Branchid"]) + " and (Trans_Type= 1 or Trans_Type= 0) and rootid=12;";
                    
                    drBind["Narration"] = "Opening Balance";
                    drBind["Heads"] = "";
                    var openingBal = balayer.GetSingleValue(query);
                    if (Convert.ToDouble(openingBal) > 0)
                        drBind["Credit"] = openingBal;
                    else
                        drBind["Credit"] = "0";
                    //drBind["Credit"] = balayer.GetSingleValue(query);
                    drBind["Debit"] = "";
                    fillDt.Rows.Add(drBind.ItemArray);

                    drBind["Date"] = firstdt;
                    drBind["Narration"] = "CRR Balance";
                    if (crdt.Rows.Count > 0)
                    {
                        drBind["Credit"] = Convert.ToString(crdt.Rows[0]["CRR Balance"]);
                        drBind["Heads"] = "CRR";
                        fillDt.Rows.Add(drBind.ItemArray);
                    }
                    else
                    {
                        drBind["Credit"] = 0.0;
                    }
                    drBind["Debit"] = "";


                    foreach (var key in DualTransactionkeyList)
                    {
                        var filteredRows = (from n in Vouhcer.AsEnumerable()
                                            where n.Field<string>("DualTransactionKey").Contains(key)
                                            select n).ToList();
                        if (filteredRows.Count > 0)
                        {
                            if (Convert.ToString(filteredRows[0].ItemArray[5]) == "C")
                            {
                                if (Convert.ToString(filteredRows[0].ItemArray[15]) == "12")
                                {
                                    var filteredDebitRows = (from n in Vouhcer.AsEnumerable()
                                                             where n.Field<string>("DualTransactionKey").Contains(key) && n.Field<string>("Voucher_Type") == "D"
                                                             select n).ToList();
                                    foreach (var debitRows in filteredDebitRows)
                                    {
                                        drBind["Date"] = firstdt;
                                        drBind["Narration"] = Convert.ToString(debitRows.ItemArray[8]);
                                        drBind["Debit"] = Convert.ToString(debitRows.ItemArray[9]);
                                        query = "select Node from headstree where NodeID in( " + debitRows.ItemArray[6] + ")";
                                        drBind["Heads"] = balayer.GetSingleValue(query);
                                        drBind["Credit"] = "";
                                        fillDt.Rows.Add(drBind.ItemArray);
                                    }
                                }
                                //12/08/19 change voucher=0
                                else if (filteredRows.Count >= 2)
                                {
                                    //12/08/19 change voucher=0
                                    //Type fieldType = Vouhcer.Columns["RootID"].DataType;
                                    var filteredDebitRows = (from n in Vouhcer.AsEnumerable()
                                                             where n.Field<string>("DualTransactionKey").Contains(key) && n.Field<System.UInt32>("RootID") == Convert.ToUInt32(RootID)
                                                             select n).ToList();
                                    if (filteredDebitRows.Count > 0)
                                    {
                                        if (Convert.ToString(filteredDebitRows[0].ItemArray[5]) == "C")
                                        {
                                            var creditRows = (from n in filteredRows.AsEnumerable()
                                                              where n.Field<string>("DualTransactionKey").Contains(key) && n.Field<System.UInt32>("RootID") != Convert.ToUInt32(RootID)
                                                              select n).ToList();
                                            foreach (var crRow in creditRows)
                                            {
                                                drBind["Date"] = firstdt;
                                                drBind["Narration"] = Convert.ToString(crRow.ItemArray[8]);
                                                drBind["Debit"] = Convert.ToString(crRow.ItemArray[9]);
                                                query = "select Node from headstree where NodeID in( " + crRow.ItemArray[6] + ")";
                                                drBind["Heads"] = balayer.GetSingleValue(query);
                                                drBind["Credit"] = "";
                                                fillDt.Rows.Add(drBind.ItemArray);
                                            }
                                        }
                                        else
                                        {
                                            var debitRows = (from n in filteredRows.AsEnumerable()
                                                             where n.Field<string>("DualTransactionKey").Contains(key) && n.Field<System.UInt32>("RootID") != Convert.ToUInt32(RootID)
                                                             select n).ToList();
                                            foreach (var crRow in debitRows)
                                            {
                                                drBind["Date"] = firstdt;
                                                drBind["Narration"] = Convert.ToString(crRow.ItemArray[8]);
                                                drBind["Credit"] = Convert.ToString(crRow.ItemArray[9]);
                                                query = "select Node from headstree where NodeID in( " + crRow.ItemArray[6] + ")";
                                                drBind["Heads"] = balayer.GetSingleValue(query);
                                                drBind["Debit"] = "";
                                                fillDt.Rows.Add(drBind.ItemArray);
                                            }
                                        }
                                    }

                                }
                            }
                            else
                            {
                                if (Convert.ToString(filteredRows[0].ItemArray[15]) == "12")
                                {
                                    //31/08/2021 - commented by Bala- to display redraw difference in Credit Part
                                    //var filteredCreditRows = (from n in Vouhcer.AsEnumerable()
                                    //                          where n.Field<string>("DualTransactionKey").Contains(key) && n.Field<string>("Voucher_Type") == "C"
                                    //                          select n).ToList();
                                    var filteredCreditRows = (from n in Vouhcer.AsEnumerable()
                                                              where n.Field<string>("DualTransactionKey").Contains(key) && n.Field<string>("Voucher_Type") == "D"
                                                              select n).ToList();
                                    foreach (var creditRows in filteredCreditRows)
                                    {
                                        drBind["Date"] = firstdt;
                                        drBind["Narration"] = Convert.ToString(creditRows.ItemArray[8]);
                                        drBind["Credit"] = Convert.ToString(creditRows.ItemArray[9]);
                                        query = "select Node from headstree where NodeID in( " + creditRows.ItemArray[6] + ")";
                                        drBind["Heads"] = balayer.GetSingleValue(query);
                                        drBind["Debit"] = "";
                                        fillDt.Rows.Add(drBind.ItemArray);
                                    }
                                }
                            }
                        }
                    }

                    //Closing Balance
                    query = "select sum(case when (Voucher_Type='D' and ChoosenDate <= '" + balayer.changedateformat(firstdt, 2) + "'  ) then Amount else 0.00 end)-sum(case " +
                                 "when (Voucher_Type='C' and ChoosenDate <= '" + balayer.changedateformat(firstdt, 2) + "'  ) then Amount else 0.00 end) as Cash  from voucher " +
                                 "where voucher.BranchID=" + balayer.ToobjectstrEvenNull(HttpContext.Current.Session["Branchid"]) + " and rootid=12;";
                    drBind["Date"] = firstdt;
                    drBind["Narration"] = "Closing Balance";
                    var closingBal = balayer.GetSingleValue(query);
                    if (Convert.ToDouble(closingBal) > 0)
                        drBind["Debit"] = closingBal;
                    else
                        drBind["Debit"] = "0";
                  //  drBind["Debit"] = balayer.GetSingleValue(query);
                    drBind["Heads"] = "";
                    drBind["Credit"] = "";
                    fillDt.Rows.Add(drBind.ItemArray);
                }
                foreach (DataRow dtrow in fillDt.Rows)
                {
                    CashLedger cl = new CashLedger();
                    cl.Date = Convert.ToString(balayer.changedateformat(Convert.ToDateTime(dtrow["Date"]), 6));
                    cl.Narration = dtrow["Narration"].ToString();
                    cl.Credit = dtrow["Credit"].ToString();
                    cl.Heads = dtrow["Heads"].ToString();
                    cl.Debit = dtrow["Debit"].ToString();
                    CLList.Add(cl);
                }
                HttpContext.Current.Session["SuperSecret"] = fillDt;
               
            }
            catch (Exception) { }
            return CLList.ToArray();
        }
        protected void ExportExcel(object sender, EventArgs e)
        {
            DataTable dtexcel = new DataTable();
            dtexcel = (DataTable)HttpContext.Current.Session["SuperSecret"];
            var grid = new System.Web.UI.WebControls.GridView();
            grid.DataSource = dtexcel;
            grid.DataBind();
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment; filename=MyExcelFile.xls");
            Response.ContentType = "application/ms-excel";
            Response.Charset = "";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            grid.RenderControl(htw);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
           

        }


        protected void imgpdf_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                DataTable pdfdt = new DataTable();
                pdfdt = (DataTable)HttpContext.Current.Session["SuperSecret"];
                using (StringWriter sw = new StringWriter())
                {
                    using (HtmlTextWriter hw = new HtmlTextWriter(sw))
                    {
                        StringBuilder sb = new StringBuilder();


                        // sb.Append("<table align='center' style='width:500px;text-align:center'margin:0 auto; >");
                        sb.Append("<table  align='center' style='text-align:center'>");
                        sb.Append("<tr>");
                        sb.Append("<td class='style2' rowspan='3'>");

                        sb.Append("<img src=" + Server.MapPath(@"~\\Images\\visalam.png") + "  style='width: 75px; height: 41px'/></td>");
                        sb.Append("</td>");
                        sb.Append("</tr>");
                        sb.Append("<tr>");
                        sb.AppendLine("<th ><b>Sree Visalam Chit Fund Limited</b></th><br>");
                        sb.Append("</tr>");
                        sb.Append("<tr>");
                        sb.AppendLine("<br><th><b>Branch:</b></th>");
                        sb.Append("</tr>");
                        sb.Append("<tr>");
                        sb.AppendLine("<th>" + Convert.ToString(Session["BranchName"]) + "</th>");
                        sb.Append("</tr></table>");

                        sb.Append("<table border = '1'>");
                        sb.Append("<tr>");
                        foreach (DataColumn column in pdfdt.Columns)
                        {
                            sb.Append("<th style = 'background-color: #696969;color:#ffffff'>");
                            sb.Append(column.ColumnName);
                            sb.Append("</th>");
                        }
                        sb.Append("</tr>");
                        foreach (DataRow row in pdfdt.Rows)
                        {
                            sb.Append("<tr>");
                            foreach (DataColumn column in pdfdt.Columns)
                            {
                                sb.Append("<td>");
                                sb.Append(row[column]);
                                sb.Append("</td>");
                            }
                            sb.Append("</tr>");
                        }
                        sb.Append("<tr><td align = 'right' colspan = '");
                        sb.Append(pdfdt.Columns.Count - 1);
                        sb.Append("'></td>");

                        sb.Append("</tr></table>");

                        //Export HTML String as PDF.
                        StringReader sr = new StringReader(sb.ToString());
                        //  StringWriter ss = new StringWriter(sb.AppendLine());
                        iTextSharp.text.Document pdfDoc = new iTextSharp.text.Document(PageSize.A4, 10f, 10f, 10f, 0f);
                        HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
                        PdfWriter writer = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                        pdfDoc.Open();
                        htmlparser.Parse(sr);

                        pdfDoc.Close();
                        Response.ContentType = "application/pdf";
                        Response.AddHeader("content-disposition", "attachment;filename=CashLedger.pdf");
                        Response.Cache.SetCacheability(HttpCacheability.NoCache);
                        Response.Write(pdfDoc);
                        //Response.End();
                        HttpContext.Current.ApplicationInstance.CompleteRequest();
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }


        [System.Web.Services.WebMethod]
        public static CashLedger[] GetCashLedgerold(string frmdt, string todt)
        {
            DataTable fillDt = new DataTable();
            List<CashLedger> CLList = new List<CashLedger>();
            try
            {
                string query = "";
                fillDt.Columns.Add("Date", typeof(string));
                // fillDt.Columns.Add("OpeningBalance", typeof(string));
                fillDt.Columns.Add("Narration", typeof(string));
                fillDt.Columns.Add("Credit", typeof(string));
                fillDt.Columns.Add("Debit", typeof(string));

                DataRow drBind = fillDt.NewRow();
                BusinessLayer balayer = new BusinessLayer();

                DataTable crdt = new DataTable();

                //Get date             
                DateTime start = DateTime.ParseExact(frmdt, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime end = DateTime.ParseExact(todt, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                for (var datetcount = start; datetcount <= end; datetcount = datetcount.AddDays(1))
                {
                    query = "select sum(Amount) as 'CRR Balance',choosendate as Date from voucher where  Voucher_Type='C' and Voucher_No<>0 and RootID=12 " +
                            "and ChoosenDate='" + balayer.changedateformat(Convert.ToDateTime(datetcount), 2) + "' and  " +
                             "branchid=" + balayer.ToobjectstrEvenNull(HttpContext.Current.Session["Branchid"]) + " and Series<>'VOUCHER' group by ChoosenDate;";
                    crdt = balayer.GetDataTable(query);

                    DataTable Vouhcer = new DataTable();


                    //query = "Select * from(select Voucher_Type,ChoosenDate,Narration,Amount,Voucher_No, case when Series='FilingFees' then ''else Series end as Series from voucher where Voucher_Type='C' and BranchID=" + balayer.ToobjectstrEvenNull(HttpContext.Current.Session["Branchid"]) + " and Rootid=12  union " +
                    //     "select Voucher_Type,ChoosenDate,Narration,Amount,Voucher_No,Series from voucher where Voucher_Type='D' and BranchID=" + balayer.ToobjectstrEvenNull(HttpContext.Current.Session["Branchid"]) + " and Rootid=12 union " +
                    //    "select Voucher_Type,ChoosenDate,Narration,Amount,Voucher_No,Series from voucher where Voucher_Type='D' and BranchID=" + balayer.ToobjectstrEvenNull(HttpContext.Current.Session["Branchid"]) + " and Rootid=11  ) as creditDebit where " +
                    //     "creditDebit.Series='VOUCHER' or creditDebit.Series='FilingFees' and creditDebit.ChoosenDate= '" + balayer.changedateformat(Convert.ToDateTime(datetcount), 2) + "' order by creditDebit.ChoosenDate";
                    query = "Select * from(select Voucher_Type,ChoosenDate,Narration,Amount,Voucher_No, case when Series='FilingFees' then ''else Series end as Series from voucher where Voucher_Type='C' and BranchID=" + balayer.ToobjectstrEvenNull(HttpContext.Current.Session["Branchid"]) + " and Rootid=12  union " +
                         "select Voucher_Type,ChoosenDate,Narration,Amount,Voucher_No,Series from voucher where Voucher_Type='D' and BranchID=" + balayer.ToobjectstrEvenNull(HttpContext.Current.Session["Branchid"]) + " and Rootid=12 union " +
                        "select Voucher_Type,ChoosenDate,Narration,Amount,Voucher_No,Series from voucher where Voucher_Type='D' and BranchID=" + balayer.ToobjectstrEvenNull(HttpContext.Current.Session["Branchid"]) + " and Rootid=11  ) as creditDebit where " +
                         "creditDebit.Series='VOUCHER' or creditDebit.Series='FilingFees' or creditDebit.Series='DECREE' and creditDebit.ChoosenDate= '" + balayer.changedateformat(Convert.ToDateTime(datetcount), 2) + "' order by creditDebit.ChoosenDate";



                    Vouhcer = balayer.GetDataTable(query);
                    DateTime firstdt = DateTime.MinValue;

                    firstdt = Convert.ToDateTime(datetcount);
                    drBind["Date"] = firstdt;

                    //Get Openig balance of previous day
                    query = "select sum(case when (Voucher_Type='D' and ChoosenDate < '" + balayer.changedateformat(firstdt, 2) + "'  ) then Amount else 0.00 end)-sum(case " +
                             "when (Voucher_Type='C' and ChoosenDate < '" + balayer.changedateformat(firstdt, 2) + "'  ) then Amount else 0.00 end) as Cash  from voucher " +
                             "where voucher.BranchID=" + balayer.ToobjectstrEvenNull(HttpContext.Current.Session["Branchid"]) + " and rootid=12;";
                    drBind["Narration"] = "Opening Balance";
                    drBind["Credit"] = balayer.GetSingleValue(query);
                    drBind["Debit"] = "";
                    fillDt.Rows.Add(drBind.ItemArray);

                    drBind["Date"] = firstdt;
                    drBind["Narration"] = "CRR Balance";
                    if (crdt.Rows.Count > 0)
                    {
                        drBind["Credit"] = Convert.ToString(crdt.Rows[0]["CRR Balance"]);
                        fillDt.Rows.Add(drBind.ItemArray);
                    }
                    else
                    {
                        drBind["Credit"] = 0.0;
                    }
                    drBind["Debit"] = "";
                    //fillDt.Rows.Add(drBind.ItemArray);

                    for (int r1 = 0; r1 < Vouhcer.Rows.Count; r1++)
                    {
                        if (firstdt == Convert.ToDateTime(Vouhcer.Rows[r1]["ChoosenDate"]))
                        {
                            drBind["Date"] = firstdt;
                            drBind["Narration"] = Convert.ToString(Vouhcer.Rows[r1]["Narration"]);
                            if (Convert.ToString(Vouhcer.Rows[r1]["Voucher_Type"]) == "C")
                            {
                                drBind["Debit"] = Convert.ToString(Vouhcer.Rows[r1]["Amount"]);
                                drBind["Credit"] = "";
                            }
                            else if (Convert.ToString(Vouhcer.Rows[r1]["Voucher_Type"]) == "D")
                            {
                                drBind["Credit"] = Convert.ToString(Vouhcer.Rows[r1]["Amount"]);
                                drBind["Debit"] = "";
                            }
                            fillDt.Rows.Add(drBind.ItemArray);
                        }
                    }

                    //Closing Balance
                    query = "select sum(case when (Voucher_Type='D' and ChoosenDate <= '" + balayer.changedateformat(firstdt, 2) + "'  ) then Amount else 0.00 end)-sum(case " +
                                 "when (Voucher_Type='C' and ChoosenDate <= '" + balayer.changedateformat(firstdt, 2) + "'  ) then Amount else 0.00 end) as Cash  from voucher " +
                                 "where voucher.BranchID=" + balayer.ToobjectstrEvenNull(HttpContext.Current.Session["Branchid"]) + " and rootid=12;";
                    drBind["Date"] = firstdt;
                    drBind["Narration"] = "Closing Balance";
                    drBind["Debit"] = balayer.GetSingleValue(query);
                    drBind["Credit"] = "";
                    fillDt.Rows.Add(drBind.ItemArray);
                }
                foreach (DataRow dtrow in fillDt.Rows)
                {
                    CashLedger cl = new CashLedger();
                    cl.Date = Convert.ToString(balayer.changedateformat(Convert.ToDateTime(dtrow["Date"]), 6));
                    cl.Narration = dtrow["Narration"].ToString();
                    cl.Credit = dtrow["Credit"].ToString();
                    cl.Debit = dtrow["Debit"].ToString();
                    CLList.Add(cl);
                }
            }
            catch (Exception) { }
            return CLList.ToArray();
        }       
    }


    public class CashLedger
    {
        public string Date { get; set; }
        public string Heads { get; set; }     
        public string Narration { get; set; }
        public string Credit { get; set; }
        public string Debit { get; set; }
      
    }
}

       

       


     
      
