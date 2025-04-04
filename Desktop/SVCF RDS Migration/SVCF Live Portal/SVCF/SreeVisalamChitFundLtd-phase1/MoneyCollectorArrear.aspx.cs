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
using System.IO;
using System.Text;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html;
using iTextSharp.text.html.simpleparser;
using System.Web.UI.HtmlControls;
using System.Globalization;
using System.Drawing.Printing;
using System.Drawing;
using ClosedXML.Excel;
using System.Xml;
using Spire.Xls;



namespace SreeVisalamChitFundLtd_phase1
{
    public partial class MoneyCollectorArrear : System.Web.UI.Page
    {
        #region Object
        BusinessLayer balayer = new BusinessLayer();
        TransactionLayer trn = new TransactionLayer();
        CommonVariables objCOM = new CommonVariables();
        Dictionary<string, string> Tempdic = new Dictionary<string, string>();
        string qry = "";
        #endregion
        

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DateTime dddd = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                //txtFromDate.Text = dddd.ToString("dd/MM/yyyy");
                txtToDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                CollectorName();
                ViewState["CurrentData"] = null;
            }
        }
        //DataTable getallauction = new DataTable();
        //int paidinstno = 0;
        //Decimal adddueamount = 0;
        //Decimal amountdiff = 0;



        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (Session["UserName"] == null || Session["Branchid"] == null || Session["BranchName"] == null)
            {
                Response.Redirect(Page.ResolveUrl("~/Login.aspx"), true);
            }
        }

        public void CollectorName()
        {
            //qry = "Select moneycollid,moneycollname from moneycollector where BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]);
            qry = "select moneycollid,moneycollname from svcf.moneycollector mc where mc.employeeid not in(select em.Emp_ID from employee_details em where em.Emp_Designation='Transfered' or em.Emp_Designation='Resigned') and mc.BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]);
            Tempdic.Clear();
            Tempdic = balayer.CmnList(qry);
            ddlMname.DataValueField = "Key";
            ddlMname.DataTextField = "Value";
            ddlMname.DataSource = Tempdic;
            ddlMname.DataBind();
        }


        public void LoadGrid()
        {
            try
            {
                #region Vardeclaration
                double chitpaidamount = 0;
                DataTable findrdt = null;
                double auctiondueamnt = 0;
                decimal arrearamnt = 0;
                int maxdrawNo = 0;
                int insno = 0;
                DataTable paiddt = new DataTable();
                DataTable lastpaiddt = null;
                string NP = "";
                string P = "";
                double auctionsum = 0;
                decimal arrtotal = 0;
                decimal colltotal = 0;
                decimal adddueamount = 0;
                Decimal amountdiff = 0;
                ViewState["CurrentData"] = null;
                #endregion


                DataTable chitdt = new DataTable();
                DataTable dtBind = new DataTable();
                dtBind.Columns.Add("slno");
                dtBind.Columns.Add("GrpMemberID");
                dtBind.Columns.Add("MemberName");
                dtBind.Columns.Add("ArrAmount", typeof(decimal));
                dtBind.Columns.Add("Date");
               // dtBind.Columns.Add("Collected");
                dtBind.Columns.Add("DrawNO");
                dtBind.Columns.Add("IsPrized");
                dtBind.Columns.Add("AmountCollected",typeof(decimal));
                dtBind.Columns.Add("PArr", typeof(decimal));
                dtBind.Columns.Add("NPArr", typeof(decimal));
               

                DataRow dr = dtBind.NewRow();
                string tken = "";
                string tt = ddlMname.SelectedItem.Text;
                string part = "F";
                IFormatProvider culture2 = new System.Globalization.CultureInfo("fr-FR", true);
                DateTime indt = DateTime.Parse(txtToDate.Text, culture2, System.Globalization.DateTimeStyles.AssumeLocal);
                string Amount = "";
                string Due = "";
                Int64 firstValue = 0;
                Int64 secondValue = 0;
                decimal sumAmount = 0;
                double value = 0;
                string Maxdraw="";


                DataTable getallauction = new DataTable();
                int paidinstno = 0;
                

                //DateTime indt = Convert.ToDateTime(txtToDate.Text);
                auctiondueamnt = 0;
                string TotaldueAmount = "";
               // qry = "select Head_Id,GrpMemberID,BranchId,GroupID,MemberName from membertogroupmaster where M_Id=" + ddlMname.SelectedValue + " and GroupID in (select Head_Id from groupmaster where ChitStartDate <= '" + balayer.GetChangeDatFormat(indt, 2) + "')";
                qry = "select Head_Id,GrpMemberID,BranchId,GroupID,MemberName from membertogroupmaster where M_Id=" + ddlMname.SelectedValue + " and BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and GroupID in (select Head_Id from groupmaster where ChitStartDate <= '" + balayer.indiandateToMysqlDate(txtToDate.Text) + "')";
              
                chitdt = balayer.GetDataTable(qry);
                int rcnt = 0;
                for (int row = 0; row <= chitdt.Rows.Count - 1; row++)
                {
                  
                    dr["GrpMemberID"] = chitdt.Rows[row]["GrpMemberID"].ToString();
                    if (chitdt.Rows[row]["GrpMemberID"].ToString() == "CLV-21-8")
                    {
                        tken = "tken";
                    }
                    
                    dr["MemberName"] = chitdt.Rows[row]["MemberName"].ToString();
                    qry = "select sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end)as Amount,max(ChoosenDate) FROM svcf.voucher as t1 where " +
                          "ChitGroupID=" + chitdt.Rows[row]["GroupID"].ToString() + " and t1.RootID=5 and t1.Other_Trans_Type<>5 and t1.Voucher_Type='C' " +
                          "and t1.Head_ID=" + chitdt.Rows[row]["Head_Id"].ToString() + " and `t1`.`BranchID`=" + chitdt.Rows[row]["BranchId"].ToString() + " " +
                          " and ChoosenDate<='" + balayer.indiandateToMysqlDate(txtToDate.Text) + "' order by t1.ChoosenDate asc";

                    //paiddt = balayer.GetDataTable(qry);

                    chitpaidamount = balayer.GetScalarDataDbl(qry);
                    auctiondueamnt = 0;
                    qry = "";
                    qry = "select DrawNO,CurrentDueAmount from auctiondetails where GroupID=" + chitdt.Rows[row]["GroupID"].ToString() + " and PrizedMemberID<>0";
                    findrdt = balayer.GetDataTable(qry);
                    try
                    {
                        for (int lt = 0; lt <= findrdt.Rows.Count - 1; lt++)
                        {
                            if (findrdt.Rows[lt].IsNull("CurrentDueAmount") == false)
                            {
                                auctiondueamnt = auctiondueamnt + Convert.ToDouble(findrdt.Rows[lt]["CurrentDueAmount"]);
                                if (auctiondueamnt >= chitpaidamount)
                                {
                                    insno = Convert.ToInt32(findrdt.Rows[lt]["DrawNO"].ToString());

                                    auctiondueamnt = 0;
                                    chitpaidamount = 0;
                                    break;
                                }


                            }
                            else
                            {
                                qry = "select sum(CurrentDueAmount) from auctiondetails where GroupID=" + chitdt.Rows[row]["GroupID"].ToString() + " and PrizedMemberID<>0";
                                auctionsum = balayer.GetScalarDataDbl(qry);
                                if (chitpaidamount > auctionsum)
                                {
                                    insno = Convert.ToInt32(findrdt.Rows[lt]["DrawNO"]) + 1;
                                }
                            }
                        }
                    }
                    catch (Exception) { }

                   
                    Due = balayer.GetSingleValue("SELECT  currentDueAmount FROM svcf.auctiondetails where GroupID=" + chitdt.Rows[row]["GroupID"].ToString() + " and  DrawNO=1 and AuctionDate <='" + balayer.indiandateToMysqlDate(txtToDate.Text) + "'");

                  //   Amount = balayer.GetSingleValue("select sum(Amount) from voucher where Head_Id=" + chitdt.Rows[row]["Head_Id"].ToString() + " and Voucher_Type='C' and ChoosenDate <= '" + balayer.GetChangeDatFormat(indt, 2) + "'");
                     Amount = balayer.GetSingleValue("Select COALESCE((select sum(amount) from voucher where Head_Id=" + chitdt.Rows[row]["Head_Id"].ToString() + " and Voucher_Type='C' and ChoosenDate <= '" + balayer.indiandateToMysqlDate(txtToDate.Text) + "'and Other_Trans_Type<>5),0)"
+ "-COALESCE((select sum(amount) from voucher where Head_Id=" + chitdt.Rows[row]["Head_Id"].ToString() + " and Voucher_Type='D' and Trans_Type=0 and ChoosenDate <= '" + balayer.indiandateToMysqlDate(txtToDate.Text) + "' and Other_Trans_Type<>5),0) as amount");

                     Maxdraw = balayer.GetSingleValue(" SELECT max(DrawNO) FROM svcf.auctiondetails where GroupID=" + chitdt.Rows[row]["GroupID"].ToString() + " and AuctionDate <= '" + balayer.indiandateToMysqlDate(txtToDate.Text) + "'");
                   //  getallauction = balayer.GetDataTable("select DrawNO,coalesce(CurrentDueAmount,0) as  CurrentDueAmount from auctiondetails where GroupID=" + chitdt.Rows[row]["GroupID"].ToString() + " and PrizedMemberID<>0");
                     getallauction = balayer.GetDataTable("select DrawNO,coalesce(CurrentDueAmount,0) as  CurrentDueAmount from auctiondetails where GroupID=" + chitdt.Rows[row]["GroupID"].ToString() + " and PrizedMemberID<>0 and AuctionDate <= '" + balayer.indiandateToMysqlDate(txtToDate.Text) +"'");

                     adddueamount = 0;
                     for (int i = 0; i < getallauction.Rows.Count; i++)
                     {
                         if (Convert.ToString(getallauction.Rows[i]["CurrentDueAmount"]) != "0.00")
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
                                paidinstno = Convert.ToInt16(getallauction.Rows[i]["DrawNO"]);
                                // paidinstno = 0;
                            }
                         }
                     }
                  //   if (Amount != "")
                     //if (Amount != "" && Due == "")
                     //{
                     //    if (Convert.ToInt32(Amount) > 0)
                     //        // sumAmount = Convert.ToDecimal(Due) / Convert.ToDecimal(Amount);
                     //        sumAmount = 0 / Convert.ToDecimal(Amount);

                     //}
                     if (Amount != "" && Due != "")
                     {
                         if ((Convert.ToDecimal(Amount) > 0) && (Convert.ToDecimal(Due) > 0))
                             sumAmount = Convert.ToDecimal(Amount) / Convert.ToDecimal(Due);
                         else
                             sumAmount = 0;
                     }
                     //else if (Amount == "")
                     //{
                     //    sumAmount = 0 / Convert.ToDecimal(Due);
                     //}
                     //else if (Due == "")
                     //{
                     //    sumAmount = Convert.ToDecimal(Amount) / 0;
                     //}
                    

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
                       // dr["DrawNO"] = firstValue + " - " + Maxdraw;
                        dr["DrawNO"] = paidinstno + " - " + Maxdraw;
                    }
                    else
                    {
                        firstValue++;
                       // dr["DrawNO"] = firstValue +  " + " + part + " - " + Maxdraw ;
                        dr["DrawNO"] = paidinstno + " + " + part + " - " + Maxdraw;
                    }


                  
                    maxdrawNo = 0;
                    qry = "";
                    try
                    {
                        TotaldueAmount = balayer.GetSingleValue(@"select sum(CurrentDueAmount) from auctiondetails where GroupID=" + chitdt.Rows[row]["GroupID"].ToString() + " and AuctionDate<='" + balayer.indiandateToMysqlDate(txtToDate.Text) + "'");
                        qry = "select  (case when( (" + TotaldueAmount + "-sum(case when (v1.Voucher_Type='C' and " +
                            "v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) +sum(case when (v1.Voucher_Type='D' and v1.trans_Type<>2 and " +
                            "v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) )>0.00) then (" + TotaldueAmount + "-sum(case when (v1.Voucher_Type='C' and " +
                            "v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) +sum(case when (v1.Voucher_Type='D' and v1.trans_Type<>2 and " +
                            "v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) ) else 0.00 end) as 'Arrear Amount' from membertogroupmaster as mg1 join " +
                            "voucher as v1 on v1.Head_Id=mg1.Head_Id left join trans_payment as tp1 on " +
                            "v1.Head_Id =tp1.TokenNumber join membermaster as mm on (mg1.MemberID=mm.MemberIDNew) where mg1.BranchID=" + chitdt.Rows[row]["BranchId"].ToString() + " and mg1.GroupID=" + chitdt.Rows[row]["GroupID"].ToString() + " and " +
                            "v1.Head_Id=" + chitdt.Rows[row]["Head_Id"].ToString() + " and v1.ChoosenDate<='" + balayer.indiandateToMysqlDate(txtToDate.Text) + "' group by v1.Head_Id order by cast(digits(mg1.GrpMemberID) as unsigned),v1.Voucher_Type DESC";
                        var arrear = Convert.ToString(balayer.GetSingleValue(qry));
                        if (TotaldueAmount != "" && arrear == "")
                        {
                            dr["ArrAmount"] = TotaldueAmount;
                            dr["Date"] = "";
                            dr["AmountCollected"] = "0.00";
                           // dtBind.Rows.Add(dr.ItemArray);

                          //  break;
                        }
                        else
                        {
                            if (arrear == "") break;
                        }
                            arrearamnt = Convert.ToDecimal(balayer.GetScalarDecimal(qry));
                        if (arrear != "")
                        {
                            //if arrear amount is 0 skip loop
                            if (arrearamnt <= 0) continue;
                            dr["ArrAmount"] = arrearamnt;
                        }
                        //dr["ArrAmount"] = arrearamnt;
                        arrtotal = arrtotal + Convert.ToDecimal(arrearamnt);
                        //rcnt = rcnt + 1;
                        //if (rcnt == 159) 
                        //{

                        //}
                        //dr["slno"] = rcnt;
                        arrearamnt = 0;
                    }
                    catch (Exception e)
                    {
                        string hh = e.Message;
                    }
                    qry = "";

                  
                    NP = balayer.GetSingleValue("select  Replace(Replace(Replace (IsPrized,'Y','P'),'I','NP'),'N','NP') as IsPrized,GroupID from svcf.auctiondetails where PrizedMemberID='" + chitdt.Rows[row]["Head_Id"].ToString() + "' and GroupID='" + chitdt.Rows[row]["GroupID"].ToString() + "'  and AuctionDate<='" + balayer.indiandateToMysqlDate(txtToDate.Text) + "'  group by GroupID");
                    var PaymentRows = balayer.GetDataTable("select * from voucher where Series like '%Payment%' and Head_Id=" + chitdt.Rows[row]["Head_Id"].ToString() + " and choosendate<='" + balayer.indiandateToMysqlDate(txtToDate.Text) + "' and Other_Trans_Type<>5;");
                    if (string.IsNullOrEmpty(NP) || PaymentRows.Rows.Count == 0)
                    {
                        dr["IsPrized"] = "NP";
                        dr["NPArr"] = dr["ArrAmount"];
                        dr["PArr"] = 0;
                    }
                    else
                    {
                        dr["IsPrized"] = "P";
                        dr["PArr"] = dr["ArrAmount"];
                        dr["NPArr"] = 0;
                    }
                    qry = "";                   
                        
                    try
                    {
                        qry = "select case when t1.Voucher_Type='C' then t1.Amount else 0.00 end as Amount,ChoosenDate FROM svcf.voucher as t1 where " +
                            "ChitGroupID=" + chitdt.Rows[row]["GroupID"].ToString() + " and t1.RootID=5 and t1.Other_Trans_Type<>5 and t1.Voucher_Type='C' and t1.Amount <> 0 and " +
                            "t1.Head_ID=" + chitdt.Rows[row]["Head_Id"].ToString() + " and `t1`.`BranchID`=" + chitdt.Rows[row]["BranchId"].ToString() + " " +
                            " and ChoosenDate<='" + balayer.indiandateToMysqlDate(txtToDate.Text) + "' order by t1.ChoosenDate desc";
                        lastpaiddt = new DataTable();
                        lastpaiddt = balayer.GetDataTable(qry);
                        if (lastpaiddt.Rows.Count > 0)
                        {
                           //dr["Date"] = balayer.GetChangeDatFormat(Convert.ToDateTime(lastpaiddt.Rows[0]["ChoosenDate"]), 2).ToString();
                            dr["Date"] = balayer.GetChangeDatFormat(Convert.ToDateTime(lastpaiddt.Rows[0]["ChoosenDate"]), 6).ToString();
                           // dr["Date"] =(lastpaiddt.Rows[0]["ChoosenDate"].ToString());
                            dr["AmountCollected"] = lastpaiddt.Rows[0]["Amount"].ToString();
                            colltotal = colltotal + Convert.ToDecimal(lastpaiddt.Rows[0]["Amount"]);
                        }
                    }

                    catch (Exception) { }
                    rcnt = rcnt + 1;
                    if (rcnt == 159)
                    {

                    }
                    dr["slno"] = rcnt;
                    dtBind.Rows.Add(dr.ItemArray);
                }
                if (dtBind.Rows.Count > 0)
                {
                 
                    decimal sumprized = Convert.ToDecimal(dtBind.Compute("sum(PArr)", ""));
                    decimal sumnonprized = Convert.ToDecimal(dtBind.Compute("sum(NPArr)", ""));
                    decimal totalprized = sumprized + sumnonprized;
                    dr["PArr"] =  sumprized;
                    dr["NPArr"] = sumnonprized;
                    dr["Date"] = "Total Arear Amount";
                    dr["slno"] = "";
                    dr["GrpMemberID"] = "";
                    dr["MemberName"] = "";
                    dr["DrawNO"] = "SUM";
                    dr["AmountCollected"] = totalprized;
                }
                dtBind.Rows.Add(dr.ItemArray);
                dtBind.AcceptChanges();
                //Get branch Name
                string query="select Node from svcf.headstree where ParentID=1 and NodeID=" +  balayer.ToobjectstrEvenNull(Session["Branchid"]) + "";
                string branchname=balayer.GetSingleValue(query);
                //GV_MCArrear.Caption = "Sree Visalam Chit Fund Limited.,<br/>Trial And Arrear <br/> Branch Name : " + branchname + "; \t Bill Collector Name : " + ddlMname.SelectedItem.Text + " <br/> Arrear Statement for: " + txtToDate.Text + ";";
                lblCaption.Text = "Sree Visalam Chit Fund Limited.,<br/>Trial And Arrear <br/> Branch Name : " + branchname + "; \t Bill Collector Name : " + ddlMname.SelectedItem.Text + " <br/> Arrear Statement for: " + txtToDate.Text + ";";
                GV_MCArrear.DataSource = dtBind;
                GV_MCArrear.DataBind();
                txtarrtotal.Value = arrtotal.ToString("#0.00");
                // txtcoltotal.Value = colltotal.ToString("#0.00");
                ViewState["CurrentData"] = dtBind;


            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }

        protected void BtnStatisticsGo_Click(object sender, EventArgs e)
        {
            lblCaption.Text = "Sree Visalam Chit Fund Limited.,<br/><p style='display:inline-block;align-content:center;'>Trial And Arrear </p> <br/> <p style='display:inline-block;align-content:center;'>Branch Name : Tirunelveli Jn; \t Bill Collector Name : " + ddlMname.SelectedItem.Text + "</p> <br/><p style='display:inline-block;align-content:center;'> Arrear Statement for: " + txtToDate.Text + "</p>;";

            LoadGrid();
        }
        
       
        protected void imgprint_Click(object sender, ImageClickEventArgs e)
        {

            GV_MCArrear.PagerSettings.Visible = false;
            DataTable prntdt = new DataTable();
            prntdt = (DataTable)ViewState["CurrentData"];
            GV_MCArrear.DataSource = prntdt;
          
            GV_MCArrear.DataBind();

            prntdt.Dispose();
            //GV_MCArrear.DataBind();
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            GV_MCArrear.RenderControl(hw);
            string gridHTML = sw.ToString().Replace("\"", "'")
                .Replace(System.Environment.NewLine, "");
            StringBuilder sb = new StringBuilder();
            sb.Append("<script type = 'text/javascript'>");
            sb.Append("window.onload = new function(){");
            sb.Append("orientation: 'landscape'");
            sb.Append("var printWin = window.open('', '', 'left=0");
            sb.Append(",top=0,width=1000,height=600,status=0');");
            sb.Append("printWin.document.write(\"");
            sb.Append(gridHTML);
            sb.Append("\");");
            sb.Append("printWin.document.close();");
            sb.Append("printWin.focus();");
            sb.Append("printWin.print();");
            sb.Append("printWin.close();};");
            sb.Append("</script>");
            ClientScript.RegisterStartupScript(this.GetType(), "GridPrint", sb.ToString());
            GV_MCArrear.PagerSettings.Visible = true;
            GV_MCArrear.DataBind();
           
        }

        //protected void imgpdf_Click(object sender, ImageClickEventArgs e)
        //{
        //    Response.ContentType = "application/pdf";
        //    Response.AddHeader("content-disposition",
        //     "attachment;filename=GridViewExport.pdf");
        //    Response.Cache.SetCacheability(HttpCacheability.NoCache);
        //    StringWriter sw = new StringWriter();
        //    HtmlTextWriter hw = new HtmlTextWriter(sw);
        //    GV_MCArrear.AllowPaging = false;
        //    LoadGrid();
        //    DataTable pdfdt = new DataTable();
        //    pdfdt = (DataTable)ViewState["CurrentData"];

        //    GV_MCArrear.DataSource = pdfdt;
        //    GV_MCArrear.DataBind();
        //    pdfdt.Dispose();
        //    GV_MCArrear.DataBind();
        //    GV_MCArrear.RenderControl(hw);

        //    Panel1.RenderControl(hw);


        //    StringReader sr = new StringReader(sw.ToString());

        //    jey block                      ///////////////////////////////////////////////////////////////////////////////////////////////
        //    Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
        //    Document pdfDoc = new Document(new RectangleReadOnly(842, 595), 88f, 88f, 10f, 10f);
        //    pdfDoc.SetPageSize(iTextSharp.text.PageSize.A4.Rotate());

        //    Document pdfDoc = new Document(iTextSharp.text.PageSize.A4.Rotate(), 10, 10, 10, 10);
        //    Document pdfDoc = new Document(PageSize.A4, 25, 25, 30, 30);
        //    Document pdfDoc = new Document(PageSize.A4.Rotate(), 0, 0, 0, 0);
        //    PdfWriter pdfWriter = PdfWriter.GetInstance(pdfDoc, HttpContext.Current.Response.OutputStream);
        //    HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
        //    PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
        //    pdfDoc.Open();
        //    htmlparser.Parse(sr);
        //    pdfDoc.Close();
        //    Response.Write(pdfDoc);
        //    Response.Output.Write(sw);
        //    Response.Flush();
        //    Response.End();
        //}
        protected void imgpdf_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                DataTable captiondt = new DataTable();
                string tablecaption = "";
                string query = "select Node from svcf.headstree where ParentID=1 and NodeID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + "";
                string branchname = balayer.GetSingleValue(query);
                // lblCaption.Text = "Sree Visalam Chit Fund Limited.,<br/>Trial And Arrear <br/> Branch Name : " + branchname + "; \t Bill Collector Name : " + ddlMname.SelectedItem.Text + " <br/> Arrear Statement for: " + txtToDate.Text + ";";
                // objCOM.Str = Convert.ToString(captiondt.Rows[0]["RunningCall"]) == "" ? "0" : Convert.ToString(captiondt.Rows[0]["RunningCall"]);
                tablecaption = "Sree Visalam Chit Fund Limited.,< br /> Trial And Arrear < br /> Branch Name: " + branchname + "; \t Bill Collector Name : " + ddlMname.SelectedItem.Text + " < br /> Arrear Statement for: " + txtToDate.Text + "";

                GV_MCArrear.DataSource = (DataTable)ViewState["CurrentData"];
                GV_MCArrear.DataBind();
                Phrase phrase = null;
                StringBuilder sb = new StringBuilder();
                // DataTable GH = (DataTable)ViewState["CurrentData"];

                phrase = new Phrase();
                phrase.Add(new Chunk("\t\t\t\t\t\t\t\t Sree Visalam Chit Fund Limited.,\n", FontFactory.GetFont("TIMES_ROMAN", 16, 3, iTextSharp.text.Color.BLACK)));
                phrase.Add(new Chunk("\t\t\t\t\t\t\t\tTrial And Arrear\n", FontFactory.GetFont("Arial", 12, iTextSharp.text.Color.BLACK)));
                phrase.Add(new Chunk("\t\t\t\t\t\t\t\t Branch Name : " + branchname + "\n", FontFactory.GetFont("Arial", 13, iTextSharp.text.Font.NORMAL, iTextSharp.text.Color.BLACK)));
                phrase.Add(new Chunk("\t\t\t\t\t\t\t\t Bill Collector Name :  " + ddlMname.SelectedItem.Text + "\n", FontFactory.GetFont("Arial", 13, iTextSharp.text.Font.NORMAL, iTextSharp.text.Color.BLACK)));
                phrase.Add(new Chunk("\t\t\t\t\t\t\t\t Arrear Statement for: " + txtToDate.Text, FontFactory.GetFont("Arial", 13, iTextSharp.text.Font.NORMAL, iTextSharp.text.Color.BLACK)));

                BaseFont basefont = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false);
                iTextSharp.text.Font fnt = iTextSharp.text.FontFactory.GetFont(iTextSharp.text.FontFactory.HELVETICA, 12);



                iTextSharp.text.Table table = new iTextSharp.text.Table(GV_MCArrear.Columns.Count);

                iTextSharp.text.Cell cellcaption = new iTextSharp.text.Cell();
                cellcaption.Add(new Phrase(phrase));
                cellcaption.Colspan = GV_MCArrear.Columns.Count;
                cellcaption.HorizontalAlignment = PdfCell.ALIGN_CENTER;
                cellcaption.VerticalAlignment = PdfCell.ALIGN_TOP;



                table.AddCell(cellcaption);
                table.Cellpadding = 2;
                int[] widths = new int[GV_MCArrear.Columns.Count];

                for (int x = 0; x < GV_MCArrear.Columns.Count; x++)
                {
                    widths[x] = (int)GV_MCArrear.Columns[x].ItemStyle.Width.Value;
                    string cellText = Server.HtmlDecode(GV_MCArrear.HeaderRow.Cells[x].Text);
                    iTextSharp.text.Cell cell = new iTextSharp.text.Cell(cellText);
                    cell.BackgroundColor = new iTextSharp.text.Color(System
                                       .Drawing.ColorTranslator.FromHtml("#DCDCDC"));
                    GV_MCArrear.Columns[0].ItemStyle.Width = 30;
                    GV_MCArrear.Columns[1].ItemStyle.Width = 50;
                    GV_MCArrear.Columns[2].ItemStyle.Width = 80;
                    GV_MCArrear.Columns[3].ItemStyle.Width = 50;
                    GV_MCArrear.Columns[4].ItemStyle.Width = 50;
                    GV_MCArrear.Columns[5].ItemStyle.Width = 50;
                    GV_MCArrear.Columns[6].ItemStyle.Width = 50;
                    GV_MCArrear.Columns[7].ItemStyle.Width = 50;
                    GV_MCArrear.Columns[8].ItemStyle.Width = 30;
                    GV_MCArrear.Columns[9].ItemStyle.Width = 30;
                    GV_MCArrear.Columns[10].ItemStyle.Width = 30;
                    GV_MCArrear.Columns[11].ItemStyle.Width = 30;
                    // GV_MCArrear.Columns[12].ItemStyle.Width = 40;
                    table.AddCell(cell);
                }
                table.SetWidths(widths);

                //Transfer rows from GridView to table
                for (int i = 0; i < GV_MCArrear.Rows.Count; i++)
                {
                    BaseFont bfTimes = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false);
                    iTextSharp.text.Font font20 = iTextSharp.text.FontFactory.GetFont
                    (iTextSharp.text.FontFactory.HELVETICA, 12);

                    if (GV_MCArrear.Rows[i].RowType == DataControlRowType.DataRow)
                    {
                        //1col
                        Label lblsno = (Label)GV_MCArrear.Rows[i].FindControl("lblsno");
                        string cellText1 = Server.HtmlDecode
                                          (lblsno.Text);

                        iTextSharp.text.Cell cell1 = new iTextSharp.text.Cell();

                        cell1.Add(new Phrase(cellText1, font20));
                        //Set Color of Alternating row
                        if (i % 2 != 0)
                        {
                            cell1.BackgroundColor = new iTextSharp.text.Color(System.Drawing
                                                .ColorTranslator.FromHtml("#FFFFFF"));

                        }
                        table.AddCell(cell1);

                        //2nd column
                        Label lblmem = (Label)GV_MCArrear.Rows[i].FindControl("lblTicketNumber");
                        string cellText2 = Server.HtmlDecode
                                          (lblmem.Text);
                        iTextSharp.text.Cell cell2 = new iTextSharp.text.Cell();
                        cell2.Add(new Phrase(cellText2, font20));

                        //Set Color of Alternating row
                        if (i % 2 != 0)
                        {
                            cell2.BackgroundColor = new iTextSharp.text.Color(System.Drawing
                                                .ColorTranslator.FromHtml("#FFFFFF"));
                        }
                        table.AddCell(cell2);

                        //3rd column
                        Label lblcrd = (Label)GV_MCArrear.Rows[i].FindControl("lblMemberName");
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
                        Label lbldeb = (Label)GV_MCArrear.Rows[i].FindControl("lblDrawNO");
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
                        Label lblexremit = (Label)GV_MCArrear.Rows[i].FindControl("lblPArr");
                        string cellText5 = Server.HtmlDecode(lblexremit.Text);

                        iTextSharp.text.Cell cell5 = new iTextSharp.text.Cell();
                        cell5.HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT;
                        cell5.VerticalAlignment = iTextSharp.text.Element.ALIGN_RIGHT;
                        cell5.Add(new Phrase(cellText5, font20));

                        //Set Color of Alternating row
                        if (i % 2 != 0)
                        {
                            cell5.BackgroundColor = new iTextSharp.text.Color(System.Drawing
                                                .ColorTranslator.FromHtml("#FFFFFF"));
                        }
                        table.AddCell(cell5);

                        //6th column
                        Label lblnparrear = (Label)GV_MCArrear.Rows[i].FindControl("lblNPArr");
                        string cellText6 = Server.HtmlDecode
                                          (lblnparrear.Text);

                        iTextSharp.text.Cell cell6 = new iTextSharp.text.Cell();
                        cell6.Add(new Phrase(cellText6, font20));
                        cell6.HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT;
                        cell6.VerticalAlignment = iTextSharp.text.Element.ALIGN_RIGHT;

                        //Set Color of Alternating row
                        if (i % 2 != 0)
                        {
                            cell6.BackgroundColor = new iTextSharp.text.Color(System.Drawing
                                                .ColorTranslator.FromHtml("#FFFFFF"));
                        }
                        table.AddCell(cell6);

                        //7th column
                        Label lblparrear = (Label)GV_MCArrear.Rows[i].FindControl("lblDate");
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
                        Label lblbranches = (Label)GV_MCArrear.Rows[i].FindControl("lblAmountCollected");
                        string cellText8 = Server.HtmlDecode(lblbranches.Text);

                        iTextSharp.text.Cell cell8 = new iTextSharp.text.Cell();
                        cell8.Add(new Phrase(cellText8, font20));
                        cell8.HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT;
                        cell8.VerticalAlignment = iTextSharp.text.Element.ALIGN_RIGHT;
                        //Set Color of Alternating row
                        if (i % 2 != 0)
                        {
                            cell8.BackgroundColor = new iTextSharp.text.Color(System.Drawing
                                                .ColorTranslator.FromHtml("#FFFFFF"));
                        }
                        table.AddCell(cell8);
                        //9col
                        Label lblmobile = (Label)GV_MCArrear.Rows[i].FindControl("lblDtARRRealized");
                        string cellText9 = Server.HtmlDecode(lblmobile.Text);

                        iTextSharp.text.Cell cell9 = new iTextSharp.text.Cell();
                        cell9.Add(new Phrase(cellText9, font20));

                        //Set Color of Alternating row
                        if (i % 2 != 0)
                        {
                            cell9.BackgroundColor = new iTextSharp.text.Color(System.Drawing
                                                .ColorTranslator.FromHtml("#FFFFFF"));
                        }
                        table.AddCell(cell9);

                        //10
                        Label lblARRAmtColl = (Label)GV_MCArrear.Rows[i].FindControl("lblARRAmtCollected");
                        string cellText19 = Server.HtmlDecode(lblARRAmtColl.Text);

                        iTextSharp.text.Cell cell19 = new iTextSharp.text.Cell();
                        cell19.Add(new Phrase(cellText19, font20));

                        //Set Color of Alternating row
                        if (i % 2 != 0)
                        {
                            cell9.BackgroundColor = new iTextSharp.text.Color(System.Drawing
                                                .ColorTranslator.FromHtml("#FFFFFF"));
                        }
                        table.AddCell(cell19);
                        //11col
                        Label lblDefaultInterestColl = (Label)GV_MCArrear.Rows[i].FindControl("lblDefaultInterestCollected");
                        string cellText20 = Server.HtmlDecode(lblDefaultInterestColl.Text);

                        iTextSharp.text.Cell cell20 = new iTextSharp.text.Cell();
                        cell20.Add(new Phrase(cellText20, font20));

                        //Set Color of Alternating row
                        if (i % 2 != 0)
                        {
                            cell20.BackgroundColor = new iTextSharp.text.Color(System.Drawing
                                                .ColorTranslator.FromHtml("#FFFFFF"));
                        }
                        table.AddCell(cell20);
                        //12col
                        Label lblReport = (Label)GV_MCArrear.Rows[i].FindControl("lblReport");
                        string cellText21 = Server.HtmlDecode(lblReport.Text);

                        iTextSharp.text.Cell cell21 = new iTextSharp.text.Cell();
                        cell21.Add(new Phrase(cellText21, font20));

                        //Set Color of Alternating row
                        if (i % 2 != 0)
                        {
                            cell21.BackgroundColor = new iTextSharp.text.Color(System.Drawing
                                                .ColorTranslator.FromHtml("#FFFFFF"));
                        }
                        table.AddCell(cell21);


                    }
                }

                //BaseFont basefnt = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false);
                //iTextSharp.text.Font fnt1 = iTextSharp.text.FontFactory.GetFont(iTextSharp.text.FontFactory.HELVETICA, 12, 3);

                //GridViewRow row = GV_MCArrear.FooterRow;
                ////Total
                //table.AddCell("");

                //Label tot = (Label)row.FindControl("lbltexttotal");
                //string celltextfooter1 = Server.HtmlDecode
                //                  (tot.Text);
                //iTextSharp.text.Cell cellfooter1 = new iTextSharp.text.Cell();
                //cellfooter1.Add(new Phrase(celltextfooter1, fnt1));
                //table.AddCell(celltextfooter1);

                ////Total Credit
                //Label totcred = (Label)row.FindControl("totalcredit");
                //string celltextfooter2 = Server.HtmlDecode
                //                  (totcred.Text);
                //iTextSharp.text.Cell cellfooter2 = new iTextSharp.text.Cell();
                //cellfooter2.Add(new Phrase(celltextfooter2, fnt1));
                //table.AddCell(cellfooter2);

                ////Total Debit
                //Label totdeb = (Label)row.FindControl("totaldebit");
                //string celltextfooter3 = Server.HtmlDecode
                //                  (totdeb.Text);
                //iTextSharp.text.Cell cellfooter3 = new iTextSharp.text.Cell();
                //cellfooter3.Add(new Phrase(celltextfooter3, fnt1));
                //table.AddCell(cellfooter3);

                ////Total Excess Remittance
                //Label totalexcessremittance = (Label)row.FindControl("totalexcessremittance");
                //string celltextfooter4 = Server.HtmlDecode
                //                  (totalexcessremittance.Text);
                //iTextSharp.text.Cell cellfooter4 = new iTextSharp.text.Cell();
                //cellfooter4.Add(new Phrase(celltextfooter4, fnt1));
                //table.AddCell(cellfooter4);

                ////Total total non prized arrier
                //Label totalnparrier = (Label)row.FindControl("totalnparrier");
                //string celltextfooter5 = Server.HtmlDecode
                //                  (totalnparrier.Text);
                //iTextSharp.text.Cell cellfooter5 = new iTextSharp.text.Cell();
                //cellfooter5.Add(new Phrase(celltextfooter5, fnt1));
                //table.AddCell(cellfooter5);

                ////Total total prized arrier
                //Label totalparrier = (Label)row.FindControl("totalpaarrier");
                //string celltextfooter6 = Server.HtmlDecode
                //                  (totalparrier.Text);
                //iTextSharp.text.Cell cellfooter6 = new iTextSharp.text.Cell();
                //cellfooter6.Add(new Phrase(celltextfooter6, fnt1));
                //table.AddCell(cellfooter6);
                //table.AddCell("");

                //Summary - Non Prized Kasar
                double grandtotalbal = 0;
                double debitsum = 0;
                //for (int i = 0; i <= tble.Rows.Count - 1; i++)
                //{
                //    table.AddCell("");
                //    table.AddCell("");
                //    switch (i)
                //    {
                //        case 0:
                //            Label nptitle = (Label)tble.Rows[i].FindControl("Label3");
                //            iTextSharp.text.Cell cellsummary1 = new iTextSharp.text.Cell();
                //            cellsummary1.Add(new Phrase(nptitle.Text, fnt1));
                //            table.AddCell(cellsummary1);


                //            Label npvalue = (Label)tble.Rows[i].FindControl("lblsummary_NPkasar");
                //            iTextSharp.text.Cell cellsummaryval1 = new iTextSharp.text.Cell();
                //            cellsummaryval1.Colspan = 6;
                //            cellsummaryval1.Add(new Phrase(npvalue.Text, fnt1));
                //            table.AddCell(cellsummaryval1);
                //            break;

                //        case 1:
                //            //Prized Kasar
                //            Label prizedtitle = (Label)tble.Rows[i].FindControl("Label4");
                //            iTextSharp.text.Cell cellsummary2 = new iTextSharp.text.Cell();
                //            cellsummary2.Add(new Phrase(prizedtitle.Text, fnt1));
                //            table.AddCell(cellsummary2);

                //            Label prizedvalue = (Label)tble.Rows[i].FindControl("lblsummary_PKasar");
                //            iTextSharp.text.Cell cellsummaryval2 = new iTextSharp.text.Cell();
                //            cellsummaryval2.Colspan = 6;
                //            cellsummaryval2.Add(new Phrase(prizedvalue.Text, fnt1));
                //            table.AddCell(cellsummaryval2);
                //            break;

                //        case 2:
                //            //Grand Total 
                //            Label grndtitle = (Label)tble.Rows[i].FindControl("Label5");
                //            iTextSharp.text.Cell cellsummary3 = new iTextSharp.text.Cell();
                //            cellsummary3.Add(new Phrase(grndtitle.Text, fnt1));
                //            table.AddCell(cellsummary3);

                //            Label grandvalue = (Label)tble.Rows[i].FindControl("lblsummary_GrandTotal");
                //            iTextSharp.text.Cell cellsummaryval3 = new iTextSharp.text.Cell();
                //            cellsummaryval3.Colspan = 6;
                //            cellsummaryval3.Add(new Phrase(grandvalue.Text, fnt1));
                //            grandtotalbal = Convert.ToDouble(grandvalue.Text) - Convert.ToDouble(ViewState["Debit"]);
                //            table.AddCell(cellsummaryval3);
                //            break;

                //        case 3:
                //            //Balance DR                           
                //            Label baldrtitle = (Label)tble.Rows[i].FindControl("Label6");
                //            if (grandtotalbal < 0)
                //            {
                //                baldrtitle.Text = "Balance DR";
                //            }
                //            else if (grandtotalbal > 0)
                //            {
                //                baldrtitle.Text = "Balance CR";
                //            }
                //            iTextSharp.text.Cell cellsummary4 = new iTextSharp.text.Cell();
                //            cellsummary4.Add(new Phrase(baldrtitle.Text, fnt1));
                //            table.AddCell(cellsummary4);
                //            Label baldrvalue = (Label)tble.Rows[i].FindControl("BalanceDR");
                //            iTextSharp.text.Cell cellsummaryval4 = new iTextSharp.text.Cell();
                //            cellsummaryval4.Colspan = 6;
                //            cellsummaryval4.Add(new Phrase(baldrvalue.Text, fnt1));
                //            table.AddCell(cellsummaryval4);
                //            break;
                //    }
                //}

                StringWriter sw = new StringWriter();
                HtmlTextWriter hw = new HtmlTextWriter(sw);
                Panel1.RenderControl(hw);
                StringReader sr = new StringReader(sw.ToString());
                iTextSharp.text.Document pdfDoc = new iTextSharp.text.Document(PageSize.LEGAL.Rotate(), 0, 0, 19f, 0);
                // iTextSharp.text.Document pdfDoc = new iTextSharp.text.Document(PageSize.LEGAL.Rotate(), 10f, 10f, 10f, 0f);
                //  pdfDoc.SetPageSize(new iTextSharp.text.Rectangle(900f, 1150f));
                // pdfDoc.SetPageSize(new iTextSharp.text.Rectangle(520f, 800f));
                //pdfDoc.SetPageSize(new iTextSharp.text.Rectangle(10f, 10f, 10f, 0f));
                //    string leftColumn = "Pages : [Page # of Pages #]";
                pdfDoc.NewPage();

                // PrintDocument pd = new PrintDocument();
                //pd.DefaultPageSettings.Landscape = true;
                //pd.Print();
                HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
                PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                pdfDoc.Open();
                pdfDoc.Add(table);
                //       pdfDoc.Add("Left");
                pdfDoc.Close();
                //pdfDoc.Open();
                //htmlparser.Parse(sr);
                Response.ContentType = "application/pdf";
                Response.AddHeader("content-disposition", "attachment;filename=Moneycollectorarrear.pdf");
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.Write(pdfDoc);
                Response.Flush();
            }

            catch (Exception err) { }

        }
        /*protected void imgexport_Click(object sender, ImageClickEventArgs e)
        {
            Response.Clear();
            Response.Buffer = true;

            Response.AddHeader("content-disposition",
            "attachment;filename=GridViewExport.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);

            GV_MCArrear.AllowPaging = false;
            //LoadGrid();
            DataTable expdt = new DataTable();
            expdt = (DataTable)ViewState["CurrentData"];
            GV_MCArrear.DataSource = expdt;
            GV_MCArrear.DataBind();
            expdt.Dispose();
            //GV_MCArrear.DataBind();

            //Change the Header Row back to white color
            GV_MCArrear.HeaderRow.Style.Add("background-color", "#FFFFFF");

            //Apply style to Individual Cells
            GV_MCArrear.HeaderRow.Cells[0].Style.Add("background-color", "green");
            GV_MCArrear.HeaderRow.Cells[1].Style.Add("background-color", "green");
            GV_MCArrear.HeaderRow.Cells[2].Style.Add("background-color", "green");
            GV_MCArrear.HeaderRow.Cells[3].Style.Add("background-color", "green");
            GV_MCArrear.HeaderRow.Cells[4].Style.Add("background-color", "green");
            GV_MCArrear.HeaderRow.Cells[5].Style.Add("background-color", "green");
            GV_MCArrear.HeaderRow.Cells[6].Style.Add("background-color", "green");
            GV_MCArrear.HeaderRow.Cells[7].Style.Add("background-color", "green");

            for (int i = 0; i < GV_MCArrear.Rows.Count; i++)
            {
                GridViewRow row = GV_MCArrear.Rows[i];

                //Change Color back to white
                row.BackColor = System.Drawing.Color.White;

                //Apply text style to each Row
                row.Attributes.Add("class", "textmode");

                //Apply style to Individual Cells of Alternating Row
                if (i % 2 != 0)
                {
                    row.Cells[0].Style.Add("background-color", "#C2D69B");
                    row.Cells[1].Style.Add("background-color", "#C2D69B");
                    row.Cells[2].Style.Add("background-color", "#C2D69B");
                    row.Cells[3].Style.Add("background-color", "#C2D69B");
                    row.Cells[4].Style.Add("background-color", "#C2D69B");
                    row.Cells[5].Style.Add("background-color", "#C2D69B");
                    row.Cells[6].Style.Add("background-color", "#C2D69B");
                    row.Cells[7].Style.Add("background-color", "#C2D69B");
                }
            }
            GV_MCArrear.RenderControl(hw);

            //style to format numbers to string
            string style = @"<style> .textmode { mso-number-format:\@; } </style>";
            Response.Write(style);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();


         
        }*/

protected void imgexport_Click(object sender, ImageClickEventArgs e)
        {
            decimal pArrear=0;
            decimal npArrear=0;
            decimal amtCollected=0;

            Workbook workbook = new Workbook();
            workbook.CreateEmptySheets(1);
            Worksheet sheet = workbook.Worksheets[0];

            ExcelFont fontbold = workbook.CreateFont();
            fontbold.IsBold = true;

            DataTable dt = (DataTable)ViewState["CurrentData"];

            sheet.Name = "MoneyCollectorArrear";

            string branchname=balayer.GetSingleValue("select Node from svcf.headstree where ParentID=1 and NodeID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]));

            sheet.Range["E1"].Value = "Sree Visalam Chit Fund Ltd.,";
            RichText richText1 = sheet.Range["E1"].RichText;
            richText1.SetFont(0, richText1.Text.Length - 1, fontbold);

            var bb = 2;
            if (branchname == "Triplicane")
            {

                sheet.Range["E" + bb + ""].Value = "Branch: " + "Mount Road";
                RichText richText02 = sheet.Range["E" + bb + ""].RichText;
                richText02.SetFont(0, richText02.Text.Length - 1, fontbold);

            }
            else if (branchname == "Pallathur II")
            {
                sheet.Range["E" + bb + ""].Value = "Branch: " + "Pallathur";
                RichText richText02 = sheet.Range["E" + bb + ""].RichText;
                richText02.SetFont(0, richText02.Text.Length - 1, fontbold);
            }
            else
            {
                sheet.Range["E" + bb + ""].Value = "Branch: " + branchname;
                RichText richText02 = sheet.Range["E" + bb + ""].RichText;
                richText02.SetFont(0, richText02.Text.Length - 1, fontbold);
            }

            sheet.Range["E3"].Value = "Trial And Arrear";
            RichText richText03 = sheet.Range["E3"].RichText;
            richText03.SetFont(0, richText03.Text.Length - 1, fontbold);

            sheet.Range["E4"].Value = "Bill Collector Name : "+ddlMname.SelectedItem.Text;
            RichText richText04 = sheet.Range["E4"].RichText;
            richText04.SetFont(0, richText04.Text.Length - 1, fontbold);

            sheet.Range["E5"].Value = "Arrear Statement for: "+txtToDate.Text;
            RichText richText05 = sheet.Range["E5"].RichText;
            richText05.SetFont(0, richText05.Text.Length - 1, fontbold);

            sheet.Range["A6"].Value = "SNo.";
            RichText richText06 = sheet.Range["A6"].RichText;
            richText06.SetFont(0, richText06.Text.Length - 1, fontbold);
            sheet.Range["A6"].Style.HorizontalAlignment = HorizontalAlignType.Center;
            sheet.Range["A6"].Style.VerticalAlignment = VerticalAlignType.Center;

            sheet.Range["B6"].Value = "Ticket Number.";
            RichText richText07 = sheet.Range["B6"].RichText;
            richText07.SetFont(0, richText07.Text.Length - 1, fontbold);
            sheet.Range["B6"].Style.HorizontalAlignment = HorizontalAlignType.Center;
            sheet.Range["B6"].Style.VerticalAlignment = VerticalAlignType.Center;

            sheet.Range["C6"].Value = "Name";
            RichText richText09 = sheet.Range["C6"].RichText;
            richText09.SetFont(0, richText09.Text.Length - 1, fontbold);
            sheet.Range["C6"].Style.HorizontalAlignment = HorizontalAlignType.Center;
            sheet.Range["C6"].Style.VerticalAlignment = VerticalAlignType.Center;

            sheet.Range["D6"].Value = "Call No";
            RichText richText10 = sheet.Range["D6"].RichText;
            richText10.SetFont(0, richText10.Text.Length - 1, fontbold);
            sheet.Range["D6"].Style.HorizontalAlignment = HorizontalAlignType.Center;
            sheet.Range["D6"].Style.VerticalAlignment = VerticalAlignType.Center;

            sheet.Range["E6"].Value = "Prized Arrear";
            RichText richText11 = sheet.Range["E6"].RichText;
            richText11.SetFont(0, richText11.Text.Length - 1, fontbold);
            sheet.Range["E6"].Style.HorizontalAlignment = HorizontalAlignType.Center;
            sheet.Range["E6"].Style.VerticalAlignment = VerticalAlignType.Center;

            sheet.Range["F6"].Value = "NonPrized Arrear";
            RichText richText12 = sheet.Range["F6"].RichText;
            richText12.SetFont(0, richText12.Text.Length - 1, fontbold);
            sheet.Range["F6"].Style.HorizontalAlignment = HorizontalAlignType.Center;
            sheet.Range["F6"].Style.VerticalAlignment = VerticalAlignType.Center;

            sheet.Range["G6"].Value = "Date of Last Realization";
            RichText richText13 = sheet.Range["G6"].RichText;
            richText13.SetFont(0, richText13.Text.Length - 1, fontbold);
            sheet.Range["G6"].Style.HorizontalAlignment = HorizontalAlignType.Center;
            sheet.Range["G6"].Style.VerticalAlignment = VerticalAlignType.Center;

            sheet.Range["H6"].Value = "Last Amnt Collected";
            RichText richText14 = sheet.Range["H6"].RichText;
            richText14.SetFont(0, richText14.Text.Length - 1, fontbold);
            sheet.Range["H6"].Style.HorizontalAlignment = HorizontalAlignType.Center;
            sheet.Range["H6"].Style.VerticalAlignment = VerticalAlignType.Center;

            sheet.Range["I6"].Value = "Dt ARR Realized";
            RichText richText15 = sheet.Range["I6"].RichText;
            richText15.SetFont(0, richText15.Text.Length - 1, fontbold);
            sheet.Range["I6"].Style.HorizontalAlignment = HorizontalAlignType.Center;
            sheet.Range["I6"].Style.VerticalAlignment = VerticalAlignType.Center;

            sheet.Range["J6"].Value = "ARR Amt Collected";
            RichText richText16 = sheet.Range["J6"].RichText;
            richText16.SetFont(0, richText16.Text.Length - 1, fontbold);
            sheet.Range["J6"].Style.HorizontalAlignment = HorizontalAlignType.Center;
            sheet.Range["J6"].Style.VerticalAlignment = VerticalAlignType.Center;

            sheet.Range["K6"].Value = "Default Interest Collected";
            RichText richText17 = sheet.Range["K6"].RichText;
            richText17.SetFont(0, richText17.Text.Length - 1, fontbold);
            sheet.Range["K6"].Style.HorizontalAlignment = HorizontalAlignType.Center;
            sheet.Range["K6"].Style.VerticalAlignment = VerticalAlignType.Center;

            sheet.Range["L6"].Value = "Report";
            RichText richText18 = sheet.Range["L6"].RichText;
            richText18.SetFont(0, richText18.Text.Length - 1, fontbold);
            sheet.Range["L6"].Style.HorizontalAlignment = HorizontalAlignType.Center;
            sheet.Range["L6"].Style.VerticalAlignment = VerticalAlignType.Center;

            int rowcount = 6;
            int sno = 1;
            foreach(DataRow row in dt.Rows)
            {
                rowcount = rowcount + 1;

                sheet.Range["A" + rowcount].Value = sno.ToString();
                sheet.Range["B" + rowcount].Value = row.ItemArray[1].ToString();
                sheet.Range["C" + rowcount].Value = row.ItemArray[2].ToString();
                sheet.Range["D" + rowcount].Value = row.ItemArray[5].ToString();
                sheet.Range["E" + rowcount].NumberValue = Convert.ToDouble( row.ItemArray[8]);
                sheet.Range["E" + rowcount].NumberFormat = "#,##0.00";
                pArrear += Convert.ToDecimal(row.ItemArray[8]);
                sheet.Range["F" + rowcount].NumberValue = Convert.ToDouble(row.ItemArray[9]);
                sheet.Range["F" + rowcount].NumberFormat = "#,##0.00";
                npArrear += Convert.ToDecimal(row.ItemArray[9]);
                sheet.Range["G" + rowcount].Value = row.ItemArray[4].ToString();
                sheet.Range["H" + rowcount].NumberValue = Convert.ToDouble(row.ItemArray[7]);
                sheet.Range["H" + rowcount].NumberFormat = "#,##0.00";
                amtCollected += Convert.ToDecimal(row.ItemArray[7]);

                sno++;
            }
                        CellRange range21 = sheet.Range["A6:" + "L" + rowcount];
            range21.Borders.LineStyle = LineStyleType.Thin;
            range21.Borders[BordersLineType.DiagonalDown].LineStyle = LineStyleType.None;
            range21.Borders[BordersLineType.DiagonalUp].LineStyle = LineStyleType.None;

            rowcount = rowcount + 2;

            sheet.AllocatedRange.AutoFitColumns();
            sheet.AllocatedRange.AutoFitRows();

            sheet.SetColumnWidth(1,6);
            sheet.SetColumnWidth(2,10);
            sheet.SetColumnWidth(3,20);
            sheet.SetColumnWidth(4,15);
            sheet.SetColumnWidth(5,15);
            sheet.SetColumnWidth(6,15);
            sheet.SetColumnWidth(7,15);
            sheet.SetColumnWidth(8,15);
            sheet.SetColumnWidth(9,15);
            sheet.SetColumnWidth(10,15);
            sheet.SetColumnWidth(11,15);
            sheet.SetColumnWidth(12,15);

            workbook.SaveToHttpResponse("MoneyCollectorArrear.xlsx", HttpContext.Current.Response);

        }


        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }

        protected void GV_MCArrear_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void GV_MCArrear_RowDataBound(object sender, GridViewRowEventArgs e)
        {   
            try
            {
                GridView grid = (GridView)sender;
                BoundField col1 = (BoundField)grid.Columns[4];
                int numDecimals = 2; // from database
               // col1.DataFormatString = "{0:N" + numDecimals + "}";
                col1.DataFormatString = "{0:N" + numDecimals + "}";

                BoundField col2 = (BoundField)grid.Columns[5];
                numDecimals = 2; // from database
                col2.DataFormatString = "{0:N" + numDecimals + "}";

                BoundField col3 = (BoundField)grid.Columns[7];
                numDecimals = 2; // from database
                col3.DataFormatString = "{0:N" + numDecimals + "}";
            }
            catch (Exception) { }
        }
    }

}