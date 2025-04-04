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
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html;
using iTextSharp.text.html.simpleparser;
using System.Text;

namespace SreeVisalamChitFundLtd_phase1.UserControl
{
    public partial class FrmLedger : System.Web.UI.UserControl
    {
        #region ObjDeclaration
        Dictionary<string, string> Tempdic = new Dictionary<string, string>();
        List<string> TempList = new List<string>();
        //CommonClassFile objcls = new CommonClassFile();
        string qry = "";
        #endregion

        #region VarDeclaration
        BusinessLayer balayer = new BusinessLayer();
        TransactionLayer tranlayer = new TransactionLayer();
        string captuion = "";
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                LoadGrpId(DD_GP, 1);
                LoadGrpId(DD_GPMem, 2);
                //  LoadGD_MemberList();
            }
        }
        protected void Page_Init(object sender, EventArgs e)
        {           
           // Image img = (Image)UpdateProgress1.FindControl("imgWaiting");
           // img.ImageUrl = Page.ResolveUrl("~/Styles/Image/waiting.gif");
        }
        public void LoadGrpId(DropDownList ddl, int iType)
        {
            try
            {
                Tempdic.Clear();
                switch (iType)
                {
                    case 1:
                        qry = "select Head_Id,GROUPNO from groupmaster where BranchId=" + Convert.ToInt32(Session["Branchid"]) + "";
                        Tempdic = balayer.CmnList(qry);
                        ddl.DataValueField = "Key";
                        ddl.DataTextField = "Value";
                        ddl.DataSource = Tempdic;
                        ddl.DataBind();
                        break;

                    case 2:
                        qry = "select Head_Id,concat(GrpMemberID,' | ', MemberName) as 'CustomerName' from membertogroupmaster where BranchID=" + Convert.ToInt32(Session["Branchid"]) + " and GroupID=" + DD_GP.SelectedValue + "";
                        Tempdic = balayer.CmnList(qry);
                        ddl.DataValueField = "Key";
                        ddl.DataTextField = "Value";
                        ddl.DataSource = Tempdic;
                        ddl.DataBind();
                        break;
                }

            }
            catch (Exception) { }
        }

        public void load()
        {
            DataTable bind = new DataTable();
            bind.Columns.Add("Date");
            bind.Columns.Add("Narration");
            bind.Columns.Add("Noofinstallments");
            bind.Columns.Add("Subscription");
            bind.Columns.Add("KasarAmount");
            bind.Columns.Add("ShareAmount");
            bind.Columns.Add("Total");
            bind.Columns.Add("PrizedAmount");

        }

        public void LoadLedger()
        {
            try
            {
                #region VarDeclaration               
                double dueamnt = 0;               
                double tempDueAmount = 0;
                double tokenpaidamnt = 0;              
                string[] narr;
                double existinginterest = 0;
                double totalpaid = 0;
                double auctionsum = 0;
                int maxdrawNo = 0;
                #endregion

                DataTable findrdt = new DataTable();                
                qry = "select NextDueAmount FROM svcf.auctiondetails where GroupID=" + DD_GP.SelectedValue + " and DrawNO=1";
                dueamnt = balayer.GetScalarDataDbl(qry);

                DataTable fillgrid = new DataTable();
                fillgrid.Columns.Add("Date");
                fillgrid.Columns.Add("Narration");
                fillgrid.Columns.Add("Noofinstallments");
                fillgrid.Columns.Add("Subscription");
                fillgrid.Columns.Add("KasarAmount");
                fillgrid.Columns.Add("ShareAmount");
                fillgrid.Columns.Add("Total");
                fillgrid.Columns.Add("PrizedAmount");
                //New Row
                DataRow drow = fillgrid.NewRow();

                qry = "select date_format(t1.ChoosenDate,'%d-%m-%Y') as 'Date',t1.Voucher_No as 'Narration', case when t1.Voucher_Type='C' then t1.Amount else 0.00 end  as Amount,t1.Head_ID" +
                     " FROM svcf.voucher as t1 where ChitGroupID=" + DD_GP.SelectedValue + " and t1.RootID=5 and t1.Other_Trans_Type<>5 and t1.Voucher_Type='C' " +
                     "and t1.Head_ID=" + DD_GPMem.SelectedValue + " and `t1`.`BranchID` =" + Session["Branchid"] + " order  by t1.ChoosenDate asc";
                DataTable dt1 = balayer.GetDataTable(qry);

                qry="select sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end)  as Amount,t1.Head_ID FROM svcf.voucher as t1 where ChitGroupID="+
                    "" + DD_GP.SelectedValue + " and t1.RootID=5 and t1.Other_Trans_Type<>5 and t1.Voucher_Type='C' and t1.Head_ID=" + DD_GPMem.SelectedValue  + " and `t1`.`BranchID`=" +
                    "" + Session["Branchid"] + " order  by t1.ChoosenDate asc";
                totalpaid= balayer.GetScalarDataDbl(qry);

                qry="select sum(CurrentDueAmount) from auctiondetails where GroupID=" + DD_GP.SelectedValue +" ";
                auctionsum=balayer.GetScalarDataDbl(qry);              
                tempDueAmount = 0;
                tokenpaidamnt = 0;

                for (int rw = 0; rw <= dt1.Rows.Count - 1; rw++)
                {
                    try
                    {
                        qry = "select DrawNO,CurrentDueAmount from auctiondetails where GroupID=" + DD_GP.SelectedValue + "";
                        findrdt = balayer.GetDataTable(qry);
                        tokenpaidamnt = tokenpaidamnt + Convert.ToDouble(dt1.Rows[rw]["Amount"]);
                        for (int lt = 0; lt <= findrdt.Rows.Count - 1; lt++)
                        {

                            if (tempDueAmount >= tokenpaidamnt)
                            {
                                drow["Noofinstallments"] = findrdt.Rows[lt]["DrawNO"];

                                break;
                            }
                            else
                            {
                                if (findrdt.Rows[lt]["CurrentDueAmount"].ToString() != "")
                                {
                                    tempDueAmount = tempDueAmount + Convert.ToDouble(findrdt.Rows[lt]["CurrentDueAmount"]);
                                }
                                else
                                {
                                    tempDueAmount = tempDueAmount + 0;
                                }
                                if (tokenpaidamnt >= auctionsum)
                                {
                                    qry = "select max(DrawNO) from auctiondetails where GroupID=" + DD_GP.SelectedValue + " and CurrentDueAmount<>0";
                                    maxdrawNo = balayer.GetScalarDataInt(qry);
                                    drow["Noofinstallments"] = maxdrawNo;
                                    break;
                                }
                                else
                                {
                                    if (tempDueAmount >= tokenpaidamnt)
                                    {
                                        drow["Noofinstallments"] = findrdt.Rows[lt]["DrawNO"];
                                        break;
                                    }
                                }
                            }
                        }
                        drow["Date"] = dt1.Rows[rw]["Date"];
                        drow["Narration"] = dt1.Rows[rw]["Narration"];
                        drow["ShareAmount"] = dt1.Rows[rw]["Amount"];
                        drow["Subscription"] = dueamnt.ToString();
                        if (rw == 0)
                        {
                            drow["Total"] = dt1.Rows[rw]["Amount"].ToString();
                            existinginterest = Convert.ToDouble(dt1.Rows[rw]["Amount"]);
                        }
                        else
                        {
                            existinginterest = existinginterest + Convert.ToDouble(dt1.Rows[rw]["Amount"]);
                            drow["Total"] = existinginterest.ToString();
                        }
                        fillgrid.Rows.Add(drow.ItemArray);
                        tempDueAmount = 0;

                    }
                    catch (Exception err) { }
                }
                    

                //Kasar Fill
                int kasarinsno = 0;
                qry = "select t1.Narration as 'Narration',t1.Amount as 'Amount' FROM svcf.voucher as t1 where ChitGroupID=" + DD_GP.SelectedValue + " and Series<>'A' and Trans_Type=1 and Other_Trans_Type=5 " +
                       "and t1.Head_ID=" + DD_GPMem.SelectedValue + " and `t1`.`BranchID` =" + Session["Branchid"] + " order  by t1.ChoosenDate";
                DataTable dt2 = new DataTable();
                dt2 = balayer.GetDataTable(qry);
               
                if (fillgrid.Rows.Count > 0)
                {
                    for (int i = 0; i <= dt2.Rows.Count - 1; i++)
                    {
                        try
                        {
                            narr = dt2.Rows[i][0].ToString().Split(':');
                            if ((narr[1].ToString() != "") && !(narr[1].ToString().Contains("Redraw")))
                            {
                                if (narr[1].Trim().Length > 2)
                                {
                                    kasarinsno = Convert.ToInt32(narr[1].Trim().Substring(0, 2));
                                }
                                else
                                {
                                    kasarinsno = Convert.ToInt32(narr[1].Trim());
                                }
                                for (int y1 = 0; y1 <= fillgrid.Rows.Count - 1; y1++)
                                {
                                    if (fillgrid.Rows[y1]["Noofinstallments"].ToString() != "")
                                    {
                                        if ((kasarinsno + 1) == Convert.ToInt32(fillgrid.Rows[y1]["Noofinstallments"].ToString()))
                                        {
                                            fillgrid.Rows[y1]["KasarAmount"] = dt2.Rows[i]["Amount"];
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                        catch (Exception err) { }
                    }

                    if (fillgrid.Rows[0]["Narration"].ToString().Contains("carry over"))
                    {
                        fillgrid.Rows[0]["Noofinstallments"] = "--";
                        fillgrid.Rows[0]["Subscription"] = "--";
                        fillgrid.Rows[0]["KasarAmount"] = "--";
                    }


                  // qry = "select PrizedAmount,TotalCommission,AuctionDate from auctiondetails where PrizedMemberID=" + DD_GPMem.SelectedValue + "";

                    qry = "select a1.KasarAmount,a1.TotalCommission,date_format(t1.PaymentDate,'%d-%m-%Y') as 'PaymentDate',t1.PrizedAmount from auctiondetails as a1 join trans_payment as t1 on (a1.PrizedMemberID =t1.TokenNumber)  where a1.PrizedMemberID=" + DD_GPMem.SelectedValue + " order by t1.PaymentDate";
                    DataTable GetDt = new DataTable();
                    GetDt = balayer.GetDataTable(qry);
                    if (GetDt.Rows.Count > 0)
                    {
                        DataRow row0 = fillgrid.NewRow();
                        row0["Date"] = GetDt.Rows[0][2].ToString();
                        row0["Narration"] = "Prized Amount";
                        row0["PrizedAmount"] = GetDt.Rows[0][3].ToString();
                        DataRow row1 = fillgrid.NewRow();
                        row1["Date"] = GetDt.Rows[0][2].ToString();
                        row1["Narration"] = "Commision";
                        row1["PrizedAmount"] = GetDt.Rows[0][1].ToString();
                        DataRow row2 = fillgrid.NewRow();
                        row2["Date"] = GetDt.Rows[0][2].ToString();
                        row2["Narration"] = "Dividend";
                        row2["PrizedAmount"] = GetDt.Rows[0][0].ToString();
                        fillgrid.Rows.InsertAt(row0, 0);
                        fillgrid.Rows.InsertAt(row1, 1);
                        fillgrid.Rows.InsertAt(row2, 2);

                    }
                }    
                
                var orderedRows = from row in fillgrid.AsEnumerable()
                                  let date = DateTime.ParseExact(row.Field<string>("date_purchase"), "dd-mm-yyyy", null)
                                  orderby date
                                  select row;   

              //  fillgrid.DefaultView.Sort = "Date ASC";
                DataTable chitdet = new DataTable();
                chitdet = balayer.GetDataTable("SELECT `groupmaster`.`NoofMembers`,`groupmaster`.`GROUPNO`,`branchdetails`.`B_Name`,`groupmaster`.`ChitValue` FROM svcf.groupmaster  join `branchdetails` on (`groupmaster`.`BranchID`=`branchdetails`.`Head_Id`) where `groupmaster`.`Head_Id`=" + DD_GP.SelectedValue + "   and `groupmaster`.`BranchID`=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + ";");
                string query = "select Node from svcf.headstree where ParentID=1 and NodeID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + "";
                string branchname = balayer.GetSingleValue(query);
                captuion = "Sree Visalam Chit Fund Limited.,<br/>Ledger maintained under Section 23 of the Chit Funds Act 1982 and Rule 25 of the Tamilnadu Chit Funds Rules 1951<br/>Section : 1 Receipts and Payments in respect of subscribers<br/> Branch Name : " + branchname + "; \t Group Name: " + DD_GP.SelectedItem.Text + " <br/> Member Name : " + DD_GPMem.SelectedItem.Text + "; \t Chit Amount : " + chitdet.Rows[0]["ChitValue"].ToString() + ";";
               // GD_Ledger.Caption = "Sree Visalam Chit Fund Limited.,<br/>Ledger maintained under Section 23 of the Chit Funds Act 1982 and Rule 25 of the Tamilnadu Chit Funds Rules 1951<br/>Section : 1 Receipts and Payments in respect of subscribers<br/> Branch Name : " + branchname + "; \t Group Name: " + DD_GP.SelectedItem.Text + " <br/> Member Name : " + DD_GPMem.SelectedItem.Text + "; \t Chit Amount : " + chitdet.Rows[0]["ChitValue"].ToString() + ";";
                ViewState["CurrentData"] = fillgrid;
                GD_Ledger.DataSource = fillgrid;
                GD_Ledger.DataBind();
                fillgrid.Dispose();
                dt1.Dispose();
                balayer.Disposeconnection();

            }
            catch (Exception ex) 
            {
                Response.Write(ex.Message);
            }
        }

        protected void imgpdf_Click(object sender, ImageClickEventArgs e)
        {
            DataTable chitdet = new DataTable();
            chitdet = balayer.GetDataTable("SELECT `groupmaster`.`NoofMembers`,`groupmaster`.`GROUPNO`,`branchdetails`.`B_Name`,`groupmaster`.`ChitValue` FROM svcf.groupmaster  join `branchdetails` on (`groupmaster`.`BranchID`=`branchdetails`.`Head_Id`) where `groupmaster`.`Head_Id`=" + DD_GP.SelectedValue + "   and `groupmaster`.`BranchID`=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + ";");
            string query = "select Node from svcf.headstree where ParentID=1 and NodeID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + "";
            string branchname = balayer.GetSingleValue(query);
            captuion = "Sree Visalam Chit Fund Limited.,<br/>Ledger maintained under Section 23 of the Chit Funds Act 1982 and Rule 25 of the Tamilnadu Chit Funds Rules 1951<br/>Section : 1 Receipts and Payments in respect of subscribers<br/> Branch Name : " + branchname + "; \t Group Name: " + DD_GP.SelectedItem.Text + " <br/> Member Name : " + DD_GPMem.SelectedItem.Text + "; \t Chit Amount : " + chitdet.Rows[0]["ChitValue"].ToString() + ";";

            GD_Ledger.DataSource = (DataTable)ViewState["CurrentData"];
            GD_Ledger.DataBind();

            Phrase phrase = null;

            //    tble = new PdfPTable(2);
            phrase = new Phrase();
            phrase.Add(new Chunk("Sree Visalam Chit Fund Limited., \tLedger maintained under Section 23 of the Chit Funds Act 1982 and Rule 25 of the Tamilnadu Chit Funds Rules 1951 \n", FontFactory.GetFont("Arial", 12, 3, iTextSharp.text.Color.BLACK)));
            phrase.Add(new Chunk("\t\t\t\t\t\t\t\t Section : 1 Receipts and Payments in respect of subscribers\n", FontFactory.GetFont("Arial", 11, 3, iTextSharp.text.Color.BLACK)));
            phrase.Add(new Chunk("\t\t\t\t\t\t\t\t Branch Name : " + branchname + "; \t Group Name: " + DD_GP.SelectedItem.Text + "\n", FontFactory.GetFont("Arial", 11, iTextSharp.text.Font.NORMAL, iTextSharp.text.Color.BLACK)));
            phrase.Add(new Chunk("\t\t\t\t\t\t\t\t Member Name : " + DD_GPMem.SelectedItem.Text + "; \t Chit Amount : " + chitdet.Rows[0]["ChitValue"].ToString() +"; \n", FontFactory.GetFont("Arial", 11, iTextSharp.text.Font.NORMAL, iTextSharp.text.Color.BLACK)));

            BaseFont basefont = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false);
            iTextSharp.text.Font fnt = iTextSharp.text.FontFactory.GetFont(iTextSharp.text.FontFactory.HELVETICA, 10);

            iTextSharp.text.Table table = new iTextSharp.text.Table(GD_Ledger.Columns.Count);

            iTextSharp.text.Cell cellcaption = new iTextSharp.text.Cell();
            cellcaption.Add(new Phrase(phrase));
            cellcaption.Colspan = GD_Ledger.Columns.Count;
            cellcaption.HorizontalAlignment = PdfCell.ALIGN_CENTER;
            cellcaption.VerticalAlignment = PdfCell.ALIGN_TOP;
            table.AddCell(cellcaption);

            table.Cellpadding = 2;
            //Set the column widths
            int[] widths = new int[GD_Ledger.Columns.Count];

            for (int x = 0; x < GD_Ledger.Columns.Count; x++)
            {
                widths[x] = (int)GD_Ledger.Columns[x].ItemStyle.Width.Value;
                string cellText = Server.HtmlDecode(GD_Ledger.HeaderRow.Cells[x].Text);
                iTextSharp.text.Cell cell = new iTextSharp.text.Cell(cellText);
                cell.BackgroundColor = new iTextSharp.text.Color(System
                                   .Drawing.ColorTranslator.FromHtml("#DCDCDC"));
                table.AddCell(cell);
            }
            table.SetWidths(widths);


            //Transfer rows from GridView to table
            for (int i = 0; i < GD_Ledger.Rows.Count; i++)
            {
                BaseFont bfTimes = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false);
                iTextSharp.text.Font font20 = iTextSharp.text.FontFactory.GetFont
                (iTextSharp.text.FontFactory.HELVETICA, 10);

                if (GD_Ledger.Rows[i].RowType == DataControlRowType.DataRow)
                {
                    //  for (int j = 0; j < gridTA.Columns.Count; j++)
                    //  {
                    Label lbldate = (Label)GD_Ledger.Rows[i].FindControl("lbldate");
                    string cellText1 = Server.HtmlDecode
                                      (lbldate.Text);
                    //  iTextSharp.text.Cell cell1 = new iTextSharp.text.Cell(cellText1);
                    iTextSharp.text.Cell cell1 = new iTextSharp.text.Cell();
                    cell1.Add(new Phrase(cellText1, font20));
                    //Set Color of Alternating row
                    if (i % 2 != 0)
                    {
                        cell1.BackgroundColor = new iTextSharp.text.Color(System.Drawing
                                            .ColorTranslator.FromHtml("#DCDCDC"));
                    }
                    table.AddCell(cell1);

                    //2nd column
                    Label lblnarration = (Label)GD_Ledger.Rows[i].FindControl("lblnarration");
                    string cellText2 = Server.HtmlDecode
                                      (lblnarration.Text);
                    iTextSharp.text.Cell cell2 = new iTextSharp.text.Cell();
                    cell2.Add(new Phrase(cellText2, font20));
                    //iTextSharp.text.Cell cell2 = new iTextSharp.text.Cell(cellText2);

                    //Set Color of Alternating row
                    if (i % 2 != 0)
                    {
                        cell2.BackgroundColor = new iTextSharp.text.Color(System.Drawing
                                            .ColorTranslator.FromHtml("#DCDCDC"));
                    }
                    table.AddCell(cell2);

                    //3rd column
                    Label lblnoof = (Label)GD_Ledger.Rows[i].FindControl("lblnoof");
                    string cellText3 = Server.HtmlDecode
                                      (lblnoof.Text);
                    iTextSharp.text.Cell cell3 = new iTextSharp.text.Cell();
                    cell3.Add(new Phrase(cellText3, font20));
                    //iTextSharp.text.Cell cell3 = new iTextSharp.text.Cell(cellText3);

                    //Set Color of Alternating row
                    if (i % 2 != 0)
                    {
                        cell3.BackgroundColor = new iTextSharp.text.Color(System.Drawing
                                            .ColorTranslator.FromHtml("#DCDCDC"));
                    }
                    table.AddCell(cell3);

                    //4th column
                    Label lblsubam = (Label)GD_Ledger.Rows[i].FindControl("lblsubam");
                    string cellText4 = Server.HtmlDecode(lblsubam.Text);

                    iTextSharp.text.Cell cell4 = new iTextSharp.text.Cell();
                    cell4.Add(new Phrase(cellText4, font20));
                    //iTextSharp.text.Cell cell4 = new iTextSharp.text.Cell(cellText4);

                    //Set Color of Alternating row
                    if (i % 2 != 0)
                    {
                        cell4.BackgroundColor = new iTextSharp.text.Color(System.Drawing
                                            .ColorTranslator.FromHtml("#DCDCDC"));
                    }
                    table.AddCell(cell4);

                    //5th column
                    Label lbldue = (Label)GD_Ledger.Rows[i].FindControl("lbldue");
                    string cellText5 = Server.HtmlDecode(lbldue.Text);
                    iTextSharp.text.Cell cell5 = new iTextSharp.text.Cell();
                    cell5.Add(new Phrase(cellText5, font20));
                    //iTextSharp.text.Cell cell5 = new iTextSharp.text.Cell(cellText5);

                    //Set Color of Alternating row
                    if (i % 2 != 0)
                    {
                        cell5.BackgroundColor = new iTextSharp.text.Color(System.Drawing
                                            .ColorTranslator.FromHtml("#DCDCDC"));
                    }
                    table.AddCell(cell5);

                    //6th column
                    Label lblpaidamt = (Label)GD_Ledger.Rows[i].FindControl("lblpaidamt");
                    string cellText6 = Server.HtmlDecode
                                      (lblpaidamt.Text);

                    iTextSharp.text.Cell cell6 = new iTextSharp.text.Cell();
                    cell6.Add(new Phrase(cellText6, font20));
                    //iTextSharp.text.Cell cell6 = new iTextSharp.text.Cell(cellText6);

                    //Set Color of Alternating row
                    if (i % 2 != 0)
                    {
                        cell6.BackgroundColor = new iTextSharp.text.Color(System.Drawing
                                            .ColorTranslator.FromHtml("#DCDCDC"));
                    }
                    table.AddCell(cell6);

                    //7th column
                    Label lbltotal = (Label)GD_Ledger.Rows[i].FindControl("lbltotal");
                    string cellText7 = Server.HtmlDecode(lbltotal.Text);

                    iTextSharp.text.Cell cell7 = new iTextSharp.text.Cell();
                    cell7.Add(new Phrase(cellText7, font20));
                    //iTextSharp.text.Cell cell7 = new iTextSharp.text.Cell(cellText7);

                    //Set Color of Alternating row
                    if (i % 2 != 0)
                    {
                        cell7.BackgroundColor = new iTextSharp.text.Color(System.Drawing
                                            .ColorTranslator.FromHtml("#DCDCDC"));
                    }
                    table.AddCell(cell7);

                    //8th column
                    Label lblpriamt = (Label)GD_Ledger.Rows[i].FindControl("lblpriamt");
                    string cellText8 = Server.HtmlDecode(lblpriamt.Text);

                    iTextSharp.text.Cell cell8 = new iTextSharp.text.Cell();
                    cell8.Add(new Phrase(cellText8, font20));
                    // iTextSharp.text.Cell cell8 = new iTextSharp.text.Cell(cellText8);

                    //Set Color of Alternating row
                    if (i % 2 != 0)
                    {
                        cell8.BackgroundColor = new iTextSharp.text.Color(System.Drawing
                                            .ColorTranslator.FromHtml("#DCDCDC"));
                    }
                    table.AddCell(cell8);

                    // }
                }
            }

            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            PrintPanel1.RenderControl(hw);

            //GV_MCArrear.DataBind();
            //gridTA.RenderControl(hw);
            StringReader sr = new StringReader(sw.ToString());
            iTextSharp.text.Document pdfDoc = new iTextSharp.text.Document(PageSize.A4.Rotate(), 0, 0, 5, 0);
            pdfDoc.SetPageSize(new iTextSharp.text.Rectangle(850f, 1100f));
            pdfDoc.NewPage();

            HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
            PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
            pdfDoc.Open();
            pdfDoc.Add(table);
            pdfDoc.Close();
            //pdfDoc.Open();
            //htmlparser.Parse(sr);
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=DayBook.pdf");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Write(pdfDoc);
            //Response.Output.Write(sw);
            Response.Flush();
            //Response.End();

          //  Response.ContentType = "application/pdf";
          //  Response.AddHeader("content-disposition",
          //   "attachment;filename=GridViewExport.pdf");
          //  Response.Cache.SetCacheability(HttpCacheability.NoCache);
          //  StringWriter sw = new StringWriter();
          //  HtmlTextWriter hw = new HtmlTextWriter(sw);
          //  PrintPanel1.RenderControl(hw);
          //  StringReader sr = new StringReader(sw.ToString());
          //  iTextSharp.text.Document pdfDoc = new iTextSharp.text.Document(PageSize.A4.Rotate(), 0, 0, 5, 0);
          //  pdfDoc.SetPageSize(new iTextSharp.text.Rectangle(850f, 1100f));
          //  pdfDoc.NewPage();
          //  HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
          //  PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
          //  pdfDoc.Open();
          //  pdfDoc.Add(table);
          ////  htmlparser.Parse(sr);
          //  pdfDoc.Close();
          //  //Response.Write(pdfDoc);
          //  Response.Output.Write(sw);
          //  Response.Flush();
          //  Response.End();
        }

        public void LoadGD_MemberList()
        {
            try
            {
                double dueamnt = 0;
                DataTable dt1 = new DataTable();
                double existinginterest = 0;
                //Next Due Amount
                qry = "select NextDueAmount FROM svcf.auctiondetails where GroupID=" + DD_GP.SelectedValue + " and DrawNO=1";
                dueamnt = balayer.GetScalarDataDbl(qry);
                qry = "";
                string[] narr;
                DataTable GetDt;
                //double shareamnt = 0;

                int installmentnumber = 0;
                //qry = "select date_format(ChoosenDate,'%d-%m-%Y') as 'Date',Narration,Amount,Head_ID FROM svcf.voucher where ChitGroupID=" + DD_GP.SelectedValue + " and Trans_Type=1 and Trans_Medium=0 and RootID=5 " +
                //        "and Other_Trans_Type=5 and Voucher_Type='C' and Series <>'A' and Head_ID=" + DD_GPMem.SelectedValue + "";

                qry = "select date_format(t1.ChoosenDate,'%d-%m-%Y') as 'Date',t1.Voucher_No as 'Narration', case when t1.Voucher_Type='C' then t1.Amount else 0.00 end  as Amount,t1.Head_ID" +
                      " FROM svcf.voucher as t1 where ChitGroupID=" + DD_GP.SelectedValue + " and t1.RootID=5 and t1.Other_Trans_Type<>5 and t1.Voucher_Type='C' " +
                      "and t1.Head_ID=" + DD_GPMem.SelectedValue + " and `t1`.`BranchID` =" + Session["Branchid"] + " order  by t1.ChoosenDate asc";
                dt1 = balayer.GetDataTable(qry);
                qry = "";

                installmentnumber = balayer.GetScalarDataInt("select ChitPeriod from groupmaster where Head_Id=" + DD_GP.SelectedValue + "");
                qry = "select PrizedAmount,TotalCommission,AuctionDate from auctiondetails where PrizedMemberID=" + DD_GP.SelectedValue + "";
                GetDt = new DataTable();
                GetDt = balayer.GetDataTable(qry);

                DataTable fillgrid = new DataTable();
                fillgrid.Columns.Add("Date");
                fillgrid.Columns.Add("Narration");
                fillgrid.Columns.Add("Noofinstallments");
                fillgrid.Columns.Add("DividendDue");
                fillgrid.Columns.Add("Amount");
                fillgrid.Columns.Add("ShareAmount");
                fillgrid.Columns.Add("Interest");
                fillgrid.Columns.Add("PrizedAmount");
                
                DataRow drow = fillgrid.NewRow();

                for (int i = 0; i <= dt1.Rows.Count - 1; i++)
                {

                    drow["Date"] = dt1.Rows[i][0].ToString();  //Date
                    drow["Narration"] = dt1.Rows[i][1].ToString();  //Narration         

                    //qry = "select t1.Amount FROM svcf.voucher as t1 where ChitGroupID=200 and Series<>'A' and Trans_Type=1 and Other_Trans_Type=5 " +
                    //     "and t1.Head_ID=958 and `t1`.`BranchID` =159 order  by t1.ChoosenDate asc";
                    //if (dt1.Rows[i][1].ToString().Contains(":"))
                    //{
                    //    narr = dt1.Rows[i][1].ToString().Split(':');
                    //    drow["Noofinstallments"] = installmentnumber;

                    //    //if (narr[1] != "")
                    //    //{
                    //    //    drow["Noofinstallments"] = narr[1];  //No Of Installments
                    //    //}
                    //    //else
                    //    //{
                    //    //    drow["Noofinstallments"] = "";
                    //    //}
                    //    drow["DividendDue"] = dueamnt.ToString();
                    //}
                    drow["DividendDue"] = dueamnt;
                    //shareamnt = dueamnt - Convert.ToDouble(dt1.Rows[i][2]);
                    //drow["ShareAmount"] = shareamnt.ToString();
                    drow["ShareAmount"] = dt1.Rows[i][2].ToString();
                    if (i == 0)
                    {
                        drow["Interest"] = dt1.Rows[i][2].ToString();
                        existinginterest = Convert.ToDouble(dt1.Rows[i][2]);
                    }
                    else
                    {
                        existinginterest = existinginterest + Convert.ToDouble(dt1.Rows[i][2]);
                        drow["Interest"] = existinginterest.ToString();
                    }
                    fillgrid.Rows.Add(drow.ItemArray);
                }

                qry = "select t1.Voucher_No as 'Narration',t1.Amount as 'Amount' FROM svcf.voucher as t1 where ChitGroupID=" + DD_GP.SelectedValue + " and Series<>'A' and Trans_Type=1 and Other_Trans_Type=5 " +
                        "and t1.Head_ID=" + DD_GPMem.SelectedValue + " and `t1`.`BranchID` =" + Session["Branchid"] + " order  by t1.ChoosenDate";
                DataTable dt2 = new DataTable();
                dt2 = balayer.GetDataTable(qry);
                foreach (DataRow dr in dt2.Rows)
                {
                    if (!dr["Narration"].ToString().Contains(":"))
                        dr.Delete();
                }
                dt2.AcceptChanges();
                for (int rcount = 0; rcount <= dt2.Rows.Count - 1; rcount++)
                {
                    if (rcount < fillgrid.Rows.Count)
                    {
                        if (fillgrid.Rows[rcount]["Narration"].ToString().Contains("carry over"))
                        {
                            fillgrid.Rows[rcount]["Noofinstallments"] = "--";
                            fillgrid.Rows[rcount]["Amount"] = "--";
                        }
                        else
                        {
                            if (dt2.Rows[rcount][0].ToString().Contains(":"))
                            {
                                narr = dt2.Rows[rcount][0].ToString().Split(':');
                                fillgrid.Rows[rcount]["Noofinstallments"] = narr[1];
                                fillgrid.Rows[rcount]["Amount"] = dt2.Rows[rcount]["Amount"].ToString();
                                //if (narr[1] != "")
                                //{
                                //    drow["Noofinstallments"] = narr[1];  //No Of Installments
                                //}
                                //else
                                //{
                                //    drow["Noofinstallments"] = "";
                                //}
                                //drow["DividendDue"] = dueamnt.ToString();
                            }
                        }
                    }
                }

                if (GetDt.Rows.Count > 0)
                {
                    DataRow row0 = fillgrid.NewRow();
                    row0["Date"] = GetDt.Rows[0][2].ToString();
                    row0["Narration"] = "Prized Amount";
                    row0["PrizedAmount"] = GetDt.Rows[0][3].ToString();
                    DataRow row1 = fillgrid.NewRow();
                    row1["Date"] = GetDt.Rows[0][2].ToString();
                    row1["Narration"] = "Commision";
                    row1["PrizedAmount"] = GetDt.Rows[0][1].ToString();
                    DataRow row2 = fillgrid.NewRow();
                    row2["Date"] = GetDt.Rows[0][2].ToString();
                    row2["Narration"] = "Dividend";
                    row2["PrizedAmount"] = GetDt.Rows[0][0].ToString();
                    fillgrid.Rows.InsertAt(row0, 0);
                    fillgrid.Rows.InsertAt(row1, 1);
                    fillgrid.Rows.InsertAt(row2, 2);
                }

                var orderedRows = from row in fillgrid.AsEnumerable()
                                  let date = DateTime.ParseExact(row.Field<string>("Date"), "dd-mm-yyyy", null)
                                  orderby date
                                  select row;

            
               // DataView dv = fillgrid.DefaultView;
               // dv.Sort = "Date";
              //  DataTable sortedDT = dv.ToTable();
                ViewState["CurrentData"] = fillgrid;
                GD_Ledger.DataSource = fillgrid;
                GD_Ledger.DataBind();
                fillgrid.Dispose();
                dt1.Dispose();


            }

            catch (Exception)
            {
            }
        }

        protected void DD_GP_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadGrpId(DD_GPMem, 2);
        }

        protected void BtnView_Click(object sender, EventArgs e)
        {
            //LoadGD_MemberList();
            LoadLedger();
        }
        //Btndown_Click
      
      
        protected void GD_Ledger_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                GridViewRow gvRow = e.Row;
                if (gvRow.RowType == DataControlRowType.Header)
                {
                    //GridViewRow gvrow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
                    //TableCell cell0 = new TableCell();
                    //cell0.Text = "Amount Paid By Subscriber";
                    //cell0.HorizontalAlign = HorizontalAlign.Center;
                    //cell0.ColumnSpan = 2;                   
                    //gvrow.Cells.Add(cell0);                    
                    //GD_Ledger.Controls[0].Controls.AddAt(5, gvrow);
                }
            }
            catch (Exception) { }
        }

        

    }
}