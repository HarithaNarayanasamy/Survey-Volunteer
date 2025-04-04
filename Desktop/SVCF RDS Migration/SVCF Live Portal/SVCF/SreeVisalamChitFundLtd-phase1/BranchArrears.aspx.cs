using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SVCF_BusinessAccessLayer;
using SVCF_TransactionLayer;
using SVCF_DataAccessLayer;
using System.Globalization;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrintingLinks;
using DevExpress.Web.ASPxMenu;
using DevExpress.Web.ASPxGridView;
using System.Drawing;
using System.IO;
using DevExpress.Data;
using DevExpress.Web.ASPxGridView.Export;

namespace SreeVisalamChitFundLtd_phase1
{
    public partial class BranchArrears : System.Web.UI.Page
    {


        #region Object
        BusinessLayer balayer = new BusinessLayer();
        TransactionLayer trn = new TransactionLayer();
        string qry = "";
        #endregion



        DataTable dtBind = new DataTable();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DateTime dddd = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                //txtFromDate.Text = dddd.ToString("dd/MM/yyyy");
                txtToDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                Branch();

            }
        }

        Object sum1;
        Object sum2;
        string Sname;
        string head;
        string draw;
        string amt;
        decimal val1;
        string Nextrow;
        string Amount = "";
        string Due = "";
        Int64 firstValue = 0;
        Int64 secondValue = 0;
        decimal sumAmount = 0;
        double arrearamnt = 0;
        decimal arrtotal = 0;
        double value = 0;
        string Maxdraw = "";
        string part = "F";




        DataTable getallauction = new DataTable();

        int paidinstno = 0;
        Decimal adddueamount = 0;
        Decimal amountdiff = 0;

        protected void Branch()
        {
            DataTable dtCollector = balayer.GetDataTable("SELECT NodeID,Node FROM svcf.headstree where ParentId=1");
            DataRow dr = dtCollector.NewRow();

            ddlBranch.DataValueField = "NodeID";
            ddlBranch.DataTextField = "Node";
            dtCollector.Rows.InsertAt(dr, 0);
            ddlBranch.DataSource = dtCollector;
            ddlBranch.DataBind();
        }

        protected void select()
        {
            try
            {
                grid.SettingsText.Title = "SREE VISALAM CHIT FUND LTD.,\n Branch Arrear for " + Session["BranchName"].ToString() + " Chit\n Branch : " + ddlBranch.SelectedItem.Text+"\n"+ "Arrears Statement as on " + txtToDate.Text; ;
                //grid.SettingsText.Title = "Arrears Statement as on " + txtToDate.Text;

                dtBind.Columns.Add("slno", typeof(int));
                dtBind.Columns.Add("GrpMemberID");
                dtBind.Columns.Add("MemberName");
                dtBind.Columns.Add("Amount", typeof(decimal));
                dtBind.Columns.Add("Date");
                dtBind.Columns.Add("Collected");
                dtBind.Columns.Add("DrawNO");
                dtBind.Columns.Add("IsPrized");
                dtBind.Columns.Add("Prizedarrear");
                dtBind.Columns.Add("NonPrizedarrear");
                dtBind.Columns.Add("TransactionKey");
                dtBind.Columns.Add("MobileNumber");

                DataRow dr = dtBind.NewRow();
                balayer.GetInsertItem("CREATE OR REPLACE VIEW `unpaidprizedmoney` AS select `voucher`.`Head_Id` AS `Head_Id`, `membertogroupmaster`.`GrpMemberID` AS `GrpMemberID`, (case when (`voucher`.`Voucher_Type` = 'C') then sum(`voucher`.`Amount`) else 0.00 end) AS `Credit`, (case when (`voucher`.`Voucher_Type` = 'D') then sum(`voucher`.`Amount`) else 0.00 end) AS `Debit`, ((case when (`voucher`.`Voucher_Type` = 'C') then sum(`voucher`.`Amount`) else 0.00 end) - (case when (`voucher`.`Voucher_Type` = 'D') then sum(`voucher`.`Amount`) else 0.00 end)) AS `AmountActuallyremittedbytheParty` from (`voucher` join `membertogroupmaster` ON ((`voucher`.`Head_Id` = `membertogroupmaster`.`Head_Id`))) group by `voucher`.`Head_Id`");


                DataTable dtDate = balayer.GetDataTable("select distinct ChoosenDate from voucher where Trans_Type<>2 and Voucher_Type='C' and BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and choosenDate<='" + balayer.indiandateToMysqlDate(txtToDate.Text) + "' and Trans_Medium=0 and Other_Trans_Type not in (3,5) order by ChoosenDate asc");

                int count = 0;
                //04/03/2021
                //DataTable dtInit = balayer.GetDataTable("select DISTINCT m1.GrpMemberID,m1.MemberName ,m1.GroupID,m1.head_Id,m1.MemberID,mm.MobileNo from svcf.voucher v1 join svcf.membertogroupmaster m1 on v1.MemberID=m1.MemberID join svcf.membermaster mm on mm.memberidnew=m1.memberid  where  m1.B_Id='" + ddlBranch.SelectedItem.Value + "' and m1. BranchID=" + Session["Branchid"]);
                DataTable dtInit = balayer.GetDataTable("select DISTINCT m1.GrpMemberID,m1.MemberName ,m1.GroupID,m1.head_Id,m1.MemberID,mm.MobileNo from svcf.membertogroupmaster m1 join svcf.membermaster mm on mm.memberidnew=m1.memberid  where  m1.B_Id='" + ddlBranch.SelectedItem.Value + "' and m1. BranchID=" + Session["Branchid"]);
                
                string arrAmt = "";
                ViewState["CurrentData"] = "";  //25/04/2021

                for (int j = 0; j < dtInit.Rows.Count; j++)
                {

                    bool isPending = false; //25/04/2021
                    arrearamnt = 0; //25/04/2021

                    dr["GrpMemberID"] = dtInit.Rows[j]["GrpMemberID"];
                    dr["MemberName"] = dtInit.Rows[j]["MemberName"];
                    dr["MobileNumber"] = dtInit.Rows[j]["MobileNo"];
                    Sname = dtInit.Rows[j]["GroupID"].ToString();
                    head = dtInit.Rows[j]["head_Id"].ToString();
                    draw = dtInit.Rows[j]["MemberID"].ToString();

                    string NP = balayer.GetSingleValue("select  count(*) as Count  FROM svcf.trans_payment where TokenNumber='" + head + "' and ChitGroupID='" + Sname + "' and PaymentDate<='" + balayer.indiandateToMysqlDate(txtToDate.Text) + "' and  BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]));
                    DataTable dt1 = balayer.GetDataTable("SELECT DISTINCT sum(Amount) as Collected,max(ChoosenDate) as Date FROM svcf.voucher where  head_Id='" + head + "' and voucher_type='C' and ChoosenDate<='" + balayer.indiandateToMysqlDate(txtToDate.Text) + "' and Other_Trans_Type<>5 group by head_Id");

                    if (dt1.Rows.Count == 0)
                    {

                        string drawNo = balayer.GetSingleValue("select max(DrawNO) from svcf.auctiondetails where GroupID=" + Sname + " and AuctionDate<='" + balayer.indiandateToMysqlDate(txtToDate.Text) + "'");
                        if (!string.IsNullOrEmpty(drawNo))
                        {
                            if (Convert.ToInt16(drawNo) > 0 && Convert.ToInt16(drawNo) <= 5)
                            {
                                isPending = true;
                            }
                        }
                    }
                    if (dt1.Rows.Count > 0 || isPending==true)
                    {
                        string date = "";
                        if (dt1.Rows.Count == 0)
                        {
                             date = txtToDate.Text;
                        }
                        else
                        {
                             date = dt1.Rows[0]["Date"].ToString();
                        }

                        // string coll = balayer.GetSingleValue("SELECT DISTINCT sum(Amount) as Collected FROM svcf.voucher where  head_Id='" + head + "' and voucher_type='C' and Other_Trans_Type<>5 and ChoosenDate<='" + balayer.indiandateToMysqlDate(txtToDate.Text) + "' group by head_Id");
                        string coll = balayer.GetSingleValue("Select COALESCE((select sum(amount) from voucher where Head_Id='" + head + "' and Voucher_Type='C' and ChoosenDate <= '" + balayer.indiandateToMysqlDate(txtToDate.Text) + "'and Other_Trans_Type<>5),0)"
+ "-COALESCE((select sum(amount) from voucher where Head_Id='" + head + "' and Voucher_Type='D' and Trans_Type=0 and ChoosenDate <= '" + balayer.indiandateToMysqlDate(txtToDate.Text) + "' and Other_Trans_Type<>5),0) as amount");
                        string lostcoll = balayer.GetSingleValue("SELECT Amount  FROM svcf.voucher where head_Id='" + head + "' and voucher_type='C' and ChoosenDate='" + balayer.indiandateToMysqlDate(date) + "' and Other_Trans_Type<>5");

                        string TotaldueAmount = balayer.GetSingleValue(@"select sum(CurrentDueAmount) from auctiondetails where GroupID='" + Sname + "' and AuctionDate<='" + balayer.indiandateToMysqlDate(txtToDate.Text) + "'");
                        qry = "select  (case when( (" + TotaldueAmount + "-sum(case when (v1.Voucher_Type='C' and " +
                            "v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) +sum(case when (v1.Voucher_Type='D' and v1.trans_Type<>2 and " +
                            "v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) )>0.00) then (" + TotaldueAmount + "-sum(case when (v1.Voucher_Type='C' and " +
                            "v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) +sum(case when (v1.Voucher_Type='D' and v1.trans_Type<>2 and " +
                            "v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) ) else 0.00 end) as 'Arrear Amount' from membertogroupmaster as mg1 join " +
                            "voucher as v1 on v1.Head_Id=mg1.Head_Id left join trans_payment as tp1 on " +
                            "v1.Head_Id =tp1.TokenNumber join membermaster as mm on (mg1.MemberID=mm.MemberIDNew) where mg1.BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and mg1.GroupID='" + Sname + "' and " +
                            "v1.Head_Id='" + head + "' and v1.ChoosenDate<='" + balayer.indiandateToMysqlDate(txtToDate.Text) + "' group by v1.Head_Id order by cast(digits(mg1.GrpMemberID) as unsigned),v1.Voucher_Type DESC";

                        //if (isPending == true)
                        //{
                        //    arrAmt = balayer.GetSingleValue(qry);
                        //    if (string.IsNullOrEmpty(arrAmt))
                        //        arrearamnt = Convert.ToDouble(TotaldueAmount);
                        //    else
                        //        arrearamnt = Convert.ToDouble(arrAmt);

                        //}
                        //else
                        if (dt1.Rows.Count == 0 && (coll == "0.00" || string.IsNullOrEmpty(lostcoll)))
                        {
                            if (!string.IsNullOrEmpty(TotaldueAmount))  //23/04/2021
                                arrearamnt = Convert.ToDouble(TotaldueAmount);
                        }
                        else
                        {

                            arrearamnt = balayer.GetScalarDataDbl(qry);
                        }
                        //if arrear amount is 0 skip loop
                        
                        if (arrearamnt <= 0) continue;

                        dr["Amount"] = arrearamnt;

                        arrtotal = arrtotal + Convert.ToDecimal(arrearamnt);

                        //arrearamnt = 0;

                        Due = balayer.GetSingleValue("SELECT CurrentDueAmount FROM svcf.auctiondetails where GroupID='" + Sname + "' and  DrawNO=1 and AuctionDate <='" + balayer.indiandateToMysqlDate(txtToDate.Text) + "'");

                        //Amount = balayer.GetSingleValue("select sum(Amount) from voucher where Head_Id='" + head + "' and Voucher_Type='C' and ChoosenDate <= '" + balayer.indiandateToMysqlDate(txtToDate.Text) + "' and Other_Trans_Type<>5");
                        Amount = balayer.GetSingleValue("Select COALESCE((select sum(amount) from voucher where Head_Id='" + head + "' and Voucher_Type='C' and ChoosenDate <= '" + balayer.indiandateToMysqlDate(txtToDate.Text) + "'and Other_Trans_Type<>5),0)"
+ "-COALESCE((select sum(amount) from voucher where Head_Id='" + head + "' and Voucher_Type='D' and Trans_Type=0 and ChoosenDate <= '" + balayer.indiandateToMysqlDate(txtToDate.Text) + "' and Other_Trans_Type<>5),0) as amount");

                        Maxdraw = balayer.GetSingleValue("SELECT max(DrawNO) FROM svcf.auctiondetails where GroupID='" + Sname + "' and AuctionDate <= '" + balayer.indiandateToMysqlDate(txtToDate.Text) + "'");

                        getallauction = balayer.GetDataTable("select * from svcf.auctiondetails where GroupID='" + Sname + "' and AuctionDate <= '" + balayer.indiandateToMysqlDate(txtToDate.Text) + "'");


                        for (int i = 0; i < getallauction.Rows.Count; i++)
                        {

                            if (adddueamount == 0)
                                adddueamount = Convert.ToDecimal(getallauction.Rows[i]["CurrentDueAmount"]);
                            else
                                adddueamount = adddueamount + Convert.ToDecimal(getallauction.Rows[i]["CurrentDueAmount"]);


                            if (Convert.ToDecimal(Amount) == adddueamount)
                            {
                                paidinstno = Convert.ToInt16(getallauction.Rows[i]["DrawNO"]);
                                if (paidinstno != Convert.ToInt32(getallauction.Rows.Count))
                                {
                                    paidinstno = paidinstno + 1;
                                }
                                adddueamount = 0;

                                break;
                            }
                            if (Convert.ToDecimal(Amount) < adddueamount)
                            {
                                amountdiff = (adddueamount - Convert.ToDecimal(Amount));

                                if (amountdiff < Convert.ToDecimal(getallauction.Rows[i]["CurrentDueAmount"]))
                                //if (!(Convert.ToDecimal(amountdiff) < Convert.ToDecimal(getallauction.Rows[i]["CurrentDueAmount"])))
                                {
                                    paidinstno = Convert.ToInt16(getallauction.Rows[i]["DrawNO"]);

                                    adddueamount = 0;

                                    break;
                                }
                                if (!(Convert.ToDecimal(amountdiff) < Convert.ToDecimal(getallauction.Rows[i]["CurrentDueAmount"])))
                                {
                                    paidinstno = Convert.ToInt16(getallauction.Rows[i]["DrawNO"]);
                                    adddueamount = 0;

                                    break;
                                }
                            }

                            if (!(Convert.ToDecimal(Amount) == adddueamount) && !(Convert.ToDecimal(Amount) < adddueamount))
                            {
                                paidinstno = 0;
                            }
                        }


                        sumAmount = Convert.ToDecimal(Amount) / Convert.ToDecimal(Due);

                        value = Convert.ToDouble(sumAmount);
                        var values = value.ToString(CultureInfo.InvariantCulture).Split('.');


                        if (values.Length > 1)
                        {
                            firstValue = int.Parse(values[0]);
                            secondValue = long.Parse(values[1]);
                        }
                        else
                        {
                            firstValue = int.Parse(values[0]);
                        }
                        if (secondValue == '0')
                        {
                            firstValue++;

                            //dr["DrawNO"] = firstValue + " - " + Maxdraw;
                            dr["DrawNO"] = paidinstno + " - " + Maxdraw;
                        }
                        else if (paidinstno == 0)
                        {
                            firstValue++;
                            //dr["DrawNO"] = firstValue + " + " + part + " - " + Maxdraw;
                            //  dr["DrawNO"] = part + " - " + Maxdraw;
                            dr["DrawNO"] = Maxdraw + " - " + part;

                        }
                        else
                        {
                            firstValue++;
                            dr["DrawNO"] = paidinstno + " + " + part + " - " + Maxdraw;

                        }

                        dr["Collected"] = lostcoll.ToString();

                        //if (isPending == true)
                        //    dr["Date"] = Convert.ToDateTime((txtToDate.Text).ToString()).ToShortDateString();
                        //else
                        if (dt1.Rows.Count == 0)
                        {
                            dr["Date"] = "";
                        }
                        else
                        {
                            dr["Date"] = Convert.ToDateTime(dt1.Rows[0]["Date"].ToString()).ToShortDateString();
                        }

                        string ans1 = "P"; string ans2 = "NP";
                        string c = "0";


                        if (NP == c)
                        {
                            dr["IsPrized"] = ans2;
                        }

                        else
                        {
                            dr["IsPrized"] = ans1;

                        }

                        if (Convert.ToString(dr["IsPrized"]) == "NP")
                        {
                            dr["NonPrizedarrear"] = arrearamnt;
                            dr["Prizedarrear"] = 0;
                        }
                        else
                        {
                            dr["NonPrizedarrear"] = 0;
                            dr["Prizedarrear"] = arrearamnt;
                        }

                        //dr["Prizedarrear"] = 0;
                        //dr["NonPrizedarrear"] = 0;
                        count = count + 1;
                        dr["slno"] = count;
                        dtBind.Rows.Add(dr.ItemArray);
                    }

                   
                }

                ViewState["Amount"] = dtBind.Compute("Sum(Amount)", "");
                sum1 = dtBind.Compute("Sum(Amount)", "IsPrized='NP'");
                sum2 = dtBind.Compute("Sum(Amount)", "IsPrized='P'");
                Non.Text = sum1.ToString();
                Pr.Text = sum2.ToString();

                //count = count + 1;
                //DataRow drnew1 = dtBind.NewRow();
                //drnew1["slno"] = count;
                //drnew1["GrpMemberID"] = "";
                //drnew1["MemberName"] = "";
                //drnew1["Amount"] = 0;
                //drnew1["Date"] = "";
                //drnew1["Collected"] = "";
                //drnew1["DrawNO"] = "";
                //drnew1["IsPrized"] = "";
                //drnew1["Collected"] = "";
                //drnew1["Prizedarrear"] = sum2;
                //drnew1["NonPrizedarrear"] = sum1;

                //dtBind.Rows.Add(drnew1);
                //DataRow drnew2 = dtBind.NewRow();
                //drnew2["NonPrizedarrear"] = sum1;

                ViewState["NParrear"] = dtBind.Compute("Sum(Amount)", "IsPrized='NP'");
                ViewState["Parrear"] = dtBind.Compute("Sum(Amount)", "IsPrized='P'");

                //Label lblParrear = (Label)this.grid.FindFooterCellTemplateControl(this.grid.Columns[5], "lblParrear");
                //lblParrear.Text = Convert.ToString(ViewState["Parrear"]);

                //Label lblnparrear = (Label)this.grid.FindFooterCellTemplateControl(this.grid.Columns[5], "lblNParrear");
                //lblnparrear.Text = Convert.ToString(ViewState["NParrear"]);

                ViewState["CurrentData"] = dtBind;
                grid.DataSource = dtBind;
                grid.DataBind();

                //grid.DeleteRow(2);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void BtnStatisticsGo_Click(object sender, EventArgs e)
        {
            select();
        }

        //private System.Drawing.Image headerImage;
        //protected void Export_click(object sender, MenuItemEventArgs e)
        //{
        //    using (this.headerImage = System.Drawing.Image.FromFile(Server.MapPath("~\\Images\\visalam.png")))
        //    {
        //        if (e.Item.Text.ToString() == "PDF")
        //        {
        //            PrintingSystem ps = new PrintingSystem();
        //            PrintableComponentLink gridPayment = new PrintableComponentLink(ps);

        //            //Label lblParrear = (Label)this.grid.FindFooterCellTemplateControl(this.grid.Columns[5], "lblParrear");
        //            //lblParrear.Text = Convert.ToString(ViewState["Parrear"]);
        //            //Label lblnparrear = (Label)this.grid.FindFooterCellTemplateControl(this.grid.Columns[5], "lblNParrear");
        //            //lblnparrear.Text = Convert.ToString(ViewState["NParrear"]);


        //            grid.DataSource = (DataTable)ViewState["CurrentData"];
        //            grid.DataBind();
        //            gridExport.DataBind();
        //            gridPayment.Component = gridExport;
        //            Link header = new Link();
        //            CompositeLink compositeLink = new CompositeLink(ps);
        //            header.CreateDetailArea += new CreateAreaEventHandler(header_CreateDetailArea);

        //            compositeLink.Links.AddRange(new object[] { header, gridPayment });

        //            string leftColumn = "Pages : [Page # of Pages #]";
        //            string rightColumn = "Date: [Date Printed]\r\nTime: " + DateTime.Now.ToString("hh:mm tt");
        //            PageHeaderFooter phf = compositeLink.PageHeaderFooter as PageHeaderFooter;
        //            phf.Footer.Content.Clear();
        //            phf.Footer.Content.AddRange(new string[] { rightColumn, leftColumn });
        //            phf.Footer.LineAlignment = BrickAlignment.Center;

        //            using (MemoryStream stream = new MemoryStream())
        //            {
        //                compositeLink.PaperKind = System.Drawing.Printing.PaperKind.Legal;
        //                compositeLink.Landscape = true;
        //                compositeLink.CreateDocument(false);
        //                compositeLink.PrintingSystem.Document.AutoFitToPagesWidth = 1;
        //                compositeLink.PrintingSystem.ExportToPdf(stream);
        //                WriteToResponse("BranchArrear", true, "pdf", stream);
        //            }
        //        }
        //        else if (e.Item.Text.ToString() == "XLSX")
        //        {
        //            gridExport.WriteXlsxToResponse();
        //        }
        //    }
        //}
        //void header_CreateDetailArea(object sender, CreateAreaEventArgs e)
        //{
        //    e.Graph.BorderWidth = 0;
        //    Rectangle r = new Rectangle(100, 0, 50, 50);
        //    e.Graph.DrawImage(headerImage, r);
        //    TextBrick tb = new TextBrick();
        //    tb.Text = "SREE VISALAM CHIT FUND LTD..,";
        //    tb.Font = new Font("Arial", 10, FontStyle.Bold);
        //    tb.Rect = new RectangleF(350, 0, 400, 25);
        //    tb.BorderWidth = 0;
        //    tb.BackColor = Color.Transparent;
        //    tb.HorzAlignment = DevExpress.Utils.HorzAlignment.Default;
        //    tb.VertAlignment = DevExpress.Utils.VertAlignment.Top;
        //    e.Graph.DrawBrick(tb);
        //    TextBrick tb1 = new TextBrick();
        //    tb1.Text = "Branch Arrear for " + Session["BranchName"].ToString() + " Chit,";
        //    tb1.Font = new Font("Arial", 10, FontStyle.Bold);
        //    tb1.Rect = new RectangleF(350, 20, 400, 15);
        //    tb1.Rect = new RectangleF(350, 20, 400, 15);
        //    tb1.BorderWidth = 0;
        //    tb1.BackColor = Color.Transparent;
        //    tb1.HorzAlignment = DevExpress.Utils.HorzAlignment.Center;
        //    tb1.VertAlignment = DevExpress.Utils.VertAlignment.Top;
        //    e.Graph.DrawBrick(tb1);
        //    TextBrick tb2 = new TextBrick();
        //    tb2.Text = "Branch : " + ddlBranch.SelectedItem.Text + ".";
        //    tb2.Font = new Font("Arial", 10, FontStyle.Bold);
        //    tb2.Rect = new RectangleF(350, 40, 400, 18);
        //    tb2.BorderWidth = 0;
        //    tb2.BackColor = Color.Transparent;
        //    tb2.HorzAlignment = DevExpress.Utils.HorzAlignment.Center;
        //    tb2.VertAlignment = DevExpress.Utils.VertAlignment.Top;
        //    e.Graph.DrawBrick(tb2);
        //}
        //void WriteToResponse(string fileName, bool saveAsFile, string fileFormat, MemoryStream stream)
        //{
        //    if (Page == null || Page.Response == null)
        //        return;
        //    string disposition = saveAsFile ? "attachment" : "inline";
        //    Page.Response.Clear();
        //    Page.Response.Buffer = false;
        //    Page.Response.AppendHeader("Content-Type", string.Format("application/{0}", fileFormat));
        //    Page.Response.AppendHeader("Content-Transfer-Encoding", "binary");
        //    Page.Response.AppendHeader("Content-Disposition",
        //        string.Format("{0}; filename={1}.{2}", disposition, fileName, fileFormat));
        //    Page.Response.BinaryWrite(stream.GetBuffer());
        //    Page.Response.End();
        //}
        private System.Drawing.Image headerImage;

        //protected void gridExport(object sender, ASPxGridViewExporter e)
        //{
        //   if(e.FileName=="Name")
        //   {
        //       e.MaxColumnWidth="
        //   }

        //}
        protected void Export_click(object sender, MenuItemEventArgs e)
        {
            using (this.headerImage = System.Drawing.Image.FromFile(Server.MapPath("~\\Images\\visalam.png")))
            {
                if (e.Item.Text.ToString() == "PDF")
                {
                    PrintingSystem ps = new PrintingSystem();
                    PrintableComponentLink gridPayment = new PrintableComponentLink(ps);

                    //Label lblParrear = (Label)this.grid.FindFooterCellTemplateControl(this.grid.Columns[5], "lblParrear");
                    //lblParrear.Text = Convert.ToString(ViewState["Parrear"]);
                    //Label lblnparrear = (Label)this.grid.FindFooterCellTemplateControl(this.grid.Columns[5], "lblNParrear");
                    //lblnparrear.Text = Convert.ToString(ViewState["NParrear"]);


                    grid.DataSource = (DataTable)ViewState["CurrentData"];
                    grid.DataBind();
                    gridExport.DataBind();
                    gridPayment.Component = gridExport;
                    Link header = new Link();
                    CompositeLink compositeLink = new CompositeLink(ps);
                    //header.CreateDetailArea += new CreateAreaEventHandler(header_CreateDetailArea);

                    compositeLink.Links.AddRange(new object[] { header, gridPayment });

                    string leftColumn = "Pages : [Page # of Pages #]";
                    //string rightColumn = "Date: [Date Printed]\r\nTime: " + DateTime.Now.ToString("hh:mm tt");
                    string rightColumn = "Date: [Date Printed]";
                    PageHeaderFooter phf = compositeLink.PageHeaderFooter as PageHeaderFooter;
                    phf.Footer.Content.Clear();
                    phf.Footer.Content.AddRange(new string[] { rightColumn, leftColumn });
                    phf.Footer.LineAlignment = BrickAlignment.Center;

                    using (MemoryStream stream = new MemoryStream())
                    {
                        compositeLink.PaperKind = System.Drawing.Printing.PaperKind.Legal;
                        compositeLink.Landscape = true;
                        compositeLink.CreateDocument(false);
                        compositeLink.PrintingSystem.Document.AutoFitToPagesWidth = 1;
                        compositeLink.PrintingSystem.ExportToPdf(stream);
                        WriteToResponse("BranchArrear", true, "pdf", stream);
                    }
                }
                else if (e.Item.Text.ToString() == "XLSX")
                {
                    grid.DataSource = (DataTable)ViewState["CurrentData"];
                    grid.DataBind();
                    gridExport.FileName = "BranchArrears";
                    gridExport.DataBind();
                    gridExport.WriteXlsxToResponse();
                }
            }
        }
        void header_CreateDetailArea(object sender, CreateAreaEventArgs e)
        {
            e.Graph.BorderWidth = 0;
            //Rectangle r = new Rectangle(100, 0, 50, 50);
            //e.Graph.DrawImage(headerImage, r);
            TextBrick tb = new TextBrick();
            tb.Text = "SREE VISALAM CHIT FUND LTD.,";
            tb.Font = new Font("Arial", 14, FontStyle.Bold);
            tb.Rect = new RectangleF(355, 0, 400, 25);
            tb.BorderWidth = 0;
            tb.BackColor = Color.Transparent;
            tb.HorzAlignment = DevExpress.Utils.HorzAlignment.Default;
            tb.VertAlignment = DevExpress.Utils.VertAlignment.Top;
            e.Graph.DrawBrick(tb);
            TextBrick tb1 = new TextBrick();
            tb1.Text = "Branch Arrear for " + Session["BranchName"].ToString() + " Chit";
            tb1.Font = new Font("Arial", 12, FontStyle.Bold);
            tb1.Rect = new RectangleF(290, 20, 400, 15);
            tb1.BorderWidth = 0;
            tb1.BackColor = Color.Transparent;
            tb1.HorzAlignment = DevExpress.Utils.HorzAlignment.Center;
            tb1.VertAlignment = DevExpress.Utils.VertAlignment.Top;
            e.Graph.DrawBrick(tb1);
            TextBrick tb2 = new TextBrick();
            tb2.Text = "Branch : " + ddlBranch.SelectedItem.Text;
            tb2.Font = new Font("Arial", 12, FontStyle.Bold);
            tb2.Rect = new RectangleF(285, 40, 400, 18);
            tb2.BorderWidth = 0;
            tb2.BackColor = Color.Transparent;
            tb2.HorzAlignment = DevExpress.Utils.HorzAlignment.Center;
            tb2.VertAlignment = DevExpress.Utils.VertAlignment.Top;
            e.Graph.DrawBrick(tb2);
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

        protected void grid_CustomUnboundColumnData(object sender, ASPxGridViewColumnDataEventArgs e)
        {
            //GridView view = (GridView)sender;
            //if (e.Column.FieldName == "Prizedarrear")
            //    if (e.IsGetData)
            //        e.Value = Convert.ToDecimal(view.GetListSourceRowCellValue(e.ListSourceRowIndex, colUnitPrice)) *
            //            Convert.ToInt32(view.GetListSourceRowCellValue(e.ListSourceRowIndex, colUnitsInStock));
        }

        protected void grid_CustomSummaryCalculate(object sender, CustomSummaryEventArgs e)
        {
            if (e.IsTotalSummary)
            {
                ASPxSummaryItem x = (ASPxSummaryItem)e.Item;
                if (x.FieldName.ToString() == "Prizedarrear")
                {
                    if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Finalize)
                    {
                        decimal total = 0.00M;
                        for (int i = 0; i < grid.VisibleRowCount; i++)
                        {
                            if (grid.IsGroupRow(i))
                            {
                                total += Convert.ToDecimal(grid.GetGroupSummaryValue(i, grid.GroupSummary["Prizedarrear"]));
                            }
                        }
                        e.TotalValue = total;
                        e.TotalValueReady = true;
                    }
                }
                if (x.FieldName.ToString() == "NonPrizedarrear")
                {
                    if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Finalize)
                    {
                        decimal total = 0.00M;
                        for (int i = 0; i < grid.VisibleRowCount; i++)
                        {
                            if (grid.IsGroupRow(i))
                            {
                                total += Convert.ToDecimal(grid.GetGroupSummaryValue(i, grid.GroupSummary["NonPrizedarrear"]));
                            }
                        }
                        e.TotalValue = total;
                        e.TotalValueReady = true;
                    }
                }

                if (x.FieldName.ToString() == "Date")
                {
                    decimal totParrear = Convert.ToDecimal(grid.GetTotalSummaryValue(grid.TotalSummary["Prizedarrear"]));
                    decimal totNParrear = Convert.ToDecimal(grid.GetTotalSummaryValue(grid.TotalSummary["NonPrizedarrear"]));

                    decimal overallsum = totParrear + totNParrear;

                    if (overallsum > 0)
                    {
                        e.TotalValue = overallsum;
                        e.TotalValueReady = true;
                    }
                    else
                    {
                        e.TotalValue = 0;
                        e.TotalValueReady = true;
                    }
                }


            }

            if (e.IsGroupSummary)
            {
                ASPxSummaryItem x = (ASPxSummaryItem)e.Item;
                if (x.FieldName.ToString() == "Prizedarrear")
                {
                    decimal Prgroupsum = Convert.ToDecimal(e.GetGroupSummary(e.GroupRowHandle, grid.GroupSummary["Prizedarrear"]));

                    e.TotalValue = Prgroupsum;

                }
                if (x.FieldName.ToString() == "NonPrizedarrear")
                {
                    decimal NPgroupsum = Convert.ToDecimal(e.GetGroupSummary(e.GroupRowHandle, grid.GroupSummary["NonPrizedarrear"]));

                    e.TotalValue = NPgroupsum;

                }
            }
        }




        protected void grid_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {

        }


    }
}
