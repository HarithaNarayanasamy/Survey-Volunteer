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
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.Text;
using System.Threading;
using System.Drawing.Printing;
using Spire.Pdf;

namespace SreeVisalamChitFundLtd_phase1
{
    public partial class DuplicateAppReceipts : System.Web.UI.Page
    {
        BusinessLayer balayer = new BusinessLayer();
        TransactionLayer trn = new TransactionLayer();
        CommonVariables objCOM = new CommonVariables();

        Dictionary<string, string> tempDic = new Dictionary<string, string>();

        int branch;
        int id;

        protected void Page_Load(object sender, EventArgs e)
        {
            id = Convert.ToInt16(Request.QueryString["id"]);
            branch = Convert.ToInt32(Session["Branchid"]);
            if (branch == 161 && id == 2)
                lblHeading.Text = "Customer App & Web Receipts";
            else
                lblHeading.Text = "Bill Collector App Receipts";

            if (!IsPostBack)
            {
                txtFrom.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtTo.Text = DateTime.Now.ToString("dd/MM/yyyy");
                gridReceipts.Visible = false;
                //bagya 03/11/2022
                loadSeries();
            }
        }
        #region dropdown load
        //public void loadBranch()
        //{
        //    if (branch == 161)
        //    {
        //        DataTable dtBranch = balayer.GetDataTable("select NodeID,Node from headstree where ParentID=1;");
        //        DataRow dr = dtBranch.NewRow();
        //        dr[0] = "0";
        //        dr[1] = "--Select--";
        //        ddlBranch.DataValueField = "NodeID";
        //        ddlBranch.DataTextField = "Node";
        //        dtBranch.Rows.InsertAt(dr, 0);
        //        ddlBranch.DataSource = dtBranch;
        //        ddlBranch.DataBind();
        //    }
        //    else
        //    {
        //        DataTable dtBranch = balayer.GetDataTable("select NodeID,Node from headstree where NodeID="+Session["Branchid"]);
        //        DataRow dr = dtBranch.NewRow();
        //        dr[0] = "0";
        //        dr[1] = "--Select--";
        //        ddlBranch.DataValueField = "NodeID";
        //        ddlBranch.DataTextField = "Node";
        //        dtBranch.Rows.InsertAt(dr, 0);
        //        ddlBranch.DataSource = dtBranch;
        //        ddlBranch.DataBind();
        //    }
        //}

        //public void loadMoneyCollector()
        //{
        //    DataTable dt =balayer.GetDataTable( "select moneycollid, moneycollname from svcf.moneycollector mc where mc.employeeid not in(select em.Emp_ID from employee_details em where em.Emp_Designation = 'Transfered' or em.Emp_Designation = 'Resigned') and mc.BranchID = " + ddlBranch.SelectedItem.Value);
        //    DataRow dr = dt.NewRow();
        //    dr[0] = "0";
        //    dr[1] = "--Select--";
        //    ddlMoneyCollector.DataValueField = "moneycollid";
        //    ddlMoneyCollector.DataTextField = "moneycollname";
        //    dt.Rows.InsertAt(dr, 0);
        //    ddlMoneyCollector.DataSource = dt;
        //    ddlMoneyCollector.DataBind();
        //}

        #endregion dropdown load

        public void loadSeries()
        {
            ddlSeries.Items.Clear();

            if (branch == 161 && id == 2)
            {
                ddlSeries.Items.Add(new System.Web.UI.WebControls.ListItem("--Select--", "--Select--"));
                ddlSeries.Items.Add(new System.Web.UI.WebControls.ListItem("Customer App", "CPAPP"));
                ddlSeries.Items.Add(new System.Web.UI.WebControls.ListItem("Website", "CPWEB"));
            }
            else
            {
                ddlSeries.Items.Add(new System.Web.UI.WebControls.ListItem("Money Collector App", "BCAPP"));
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
            gridReceipts.Visible = true;
            string qry;
                //qry = "select distinct m.ChoosenDate,m.AppReceiptno,m.Amount,m.Type,mg.GrpMemberID,rp.Pdf_Location as ReceiptLink,m.IsAccepted,(case when IsAccepted='1' then 'Accepted' when IsAccepted='2' then 'Cancelled' else 'Waiting' end ) as Status from svcf.mobileappvoucher m left join svcf.membertogroupmaster mg on (m.Head_Id = mg.Head_Id) join svcf.receiptprint_pdf rp on (m.AppReceiptno = rp.AppReceiptno) where m.BranchID=" + ddlBranch.SelectedItem.Value + " and m.MoneyCollId=" + ddlMoneyCollector.SelectedValue + " and m.Series = '" + ddlSeries.SelectedItem.Value + "' and m.ChoosenDate between '" + balayer.changedateformat(Convert.ToDateTime(txtFrom.Text), 2) + "' and '" + balayer.changedateformat(Convert.ToDateTime(txtTo.Text), 2) + "'";
                //bagya 03/02/2023 add mobilelnumber
                qry = "select distinct m.ChoosenDate,m.AppReceiptno,m.Amount,m.Type,mg.GrpMemberID,rp.Pdf_Location as ReceiptLink,m.IsAccepted,(case when IsAccepted = '1' then 'Accepted' when IsAccepted = '2' then 'Cancelled' else 'Waiting' end ) as Status, m.mobilenumber as mobileno from svcf.mobileappvoucher m left join svcf.membertogroupmaster mg on (m.Head_Id = mg.Head_Id) join svcf.receiptprint_pdf rp on (m.AppReceiptno = rp.AppReceiptno) where m.Series = '" + ddlSeries.SelectedItem.Value + "' and m.ChoosenDate between '" + balayer.changedateformat(Convert.ToDateTime(txtFrom.Text), 2) + "' and '" + balayer.changedateformat(Convert.ToDateTime(txtTo.Text), 2) + "' AND m.BranchID='"+branch+"';";
            DataTable dt = balayer.GetDataTable(qry);
            gridReceipts.DataSource = dt;
            gridReceipts.DataBind();
                //bagya 03/02/2023 view mobile number for admin
                string userinfo = "";
                string usrRole = "";
                userinfo = HttpContext.Current.User.Identity.Name;
                qry = "select rs.name from roles as rs inner join rights as rt on (rt.roleid=rs.id) where memberid=" + userinfo + "";
                usrRole = balayer.GetSingleValue(qry);
                if (usrRole == "Administrator")
                {
                    gridReceipts.Columns[7].Visible = true;
                }
                else
                {
                    gridReceipts.Columns[7].Visible = false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void gridReceipts_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Download")
            {
                //Response.Clear();
                //Response.ContentType = "application/octect-stream";
                //Response.AppendHeader("content-disposition", "filename=" + e.CommandArgument+".pdf");
                //Response.TransmitFile(Server.MapPath("~/PDF_Receipts/") + e.CommandArgument+".pdf");
                //Response.End();

                //reduce the font size of pdf 11/10/2021

                #region PDF Creation
                try
                {
                    string appReceiptno = e.CommandArgument.ToString();
                    string branchName;
                    string receiptDate;
                    string receiptTime;
                    string agreementNo;
                    string chitno;
                    string paidInslmntNo;
                    string amount;
                    decimal Interest = 0;
                    decimal otherAmt = 0;
                    decimal total = 0;

                    string imageFile = HttpContext.Current.Server.MapPath("~\\Images\\SVCF_Logo_New.jpg");

                    //string qry = "SELECT b.B_Name,mg.MemberName,convert_tz(m.CurrDate,'+00:00','+05:30') as CurrDate,mg.GrpMemberID as ChitNo,concat(gm.ChitAgreementNo,'/',gm.ChitAgreementYear) as AgreementNo,m.Amount,m.Interest,m.OtherAmt,m.PaidInstallment,m.IsAccepted FROM svcf.mobileappvoucher m join svcf.branchdetails b on (m.BranchID=b.Head_Id) join svcf.groupmaster gm on (m.ChitGroupId=gm.Head_Id) join svcf.membertogroupmaster mg on (m.Head_Id=mg.Head_Id) where  AppReceiptno='" + appReceiptno + "' and Voucher_Type='C';";
                    string qry = "SELECT b.B_Name,mg.MemberName,convert_tz(m.CurrDate,'+00:00','+05:30') as CurrDate,mg.GrpMemberID as ChitNo,CASE WHEN gm.ChitAgreementNo=NULL OR gm.ChitAgreementNo='' THEN CONCAT(gm.PSOOrderNo,'/',YEAR(gm.PSOOrderDate)) ELSE concat(gm.ChitAgreementNo,'/',gm.ChitAgreementYear) END as AgreementNo,m.Amount,m.Interest,m.OtherAmt,m.PaidInstallment,m.IsAccepted FROM svcf.mobileappvoucher m join svcf.branchdetails b on (m.BranchID=b.Head_Id) join svcf.groupmaster gm on (m.ChitGroupId=gm.Head_Id) join svcf.membertogroupmaster mg on (m.Head_Id=mg.Head_Id) where  AppReceiptno='" + appReceiptno + "' and Voucher_Type='C';";
                    DataTable dt = balayer.GetDataTable(qry);

                    if (dt.Rows.Count > 0)
                    {
                        branchName = dt.Rows[0]["B_Name"].ToString();
                        string memberName = dt.Rows[0]["MemberName"].ToString();

                        string status = dt.Rows[0]["IsAccepted"].ToString();
                        if (status == "2")
                            status = "Cancelled";
                        else
                            status = "";

                        receiptDate = Convert.ToDateTime(dt.Rows[0]["CurrDate"]).ToString("dd/MM/yyyy");
                        receiptTime = Convert.ToDateTime(dt.Rows[0]["CurrDate"]).ToLongTimeString();
                        agreementNo = dt.Rows[0]["AgreementNo"].ToString();
                        chitno = dt.Rows[0]["ChitNo"].ToString();
                        paidInslmntNo = dt.Rows[0]["PaidInstallment"].ToString();
                        amount = dt.Rows[0]["Amount"].ToString();
                        if (dt.Rows.Count > 1)
                        {
                            if (!string.IsNullOrEmpty(dt.Rows[1]["Interest"].ToString()))
                                Interest = Convert.ToDecimal(dt.Rows[1]["Interest"]);
                            if (!string.IsNullOrEmpty(dt.Rows[1]["OtherAmt"].ToString()))
                                otherAmt = Convert.ToDecimal(dt.Rows[1]["OtherAmt"]);
                            total = Convert.ToDecimal(amount) + Interest + otherAmt;
                        }
                        else
                        {
                            total = Convert.ToDecimal(amount);
                        }

                        System.Globalization.CultureInfo hindi = new System.Globalization.CultureInfo("hi-IN");
                        string totalAmt = string.Format(hindi, "{0:C}", total);
                        string amt = string.Format(hindi, "{0:C}", Convert.ToDecimal(amount));
                        string intrst = string.Format(hindi, "{0:C}", Convert.ToDecimal(Interest));
                        string other = string.Format(hindi, "{0:C}", Convert.ToDecimal(otherAmt));

                        using (StringWriter sw = new StringWriter())
                        {
                            using (HtmlTextWriter hw = new HtmlTextWriter(sw))
                            {
                                StringBuilder sb = new StringBuilder();
                                //sb.Append(@"<html><body><table font-family width='100%' cellpadding='1'><tbody><tr><td style='width:50%'> <strong>Estd:1947</strong></td><td style='text-align: right;width:30%'> <strong>Form : XVI</strong></td></tr><tr align='center'><td colspan='2' style='line-height:75pt;'><img src=" + imageFile + "></td></tr></tbody></table>");
                                /*sb.Append(@"<html><body><table font-family width='100%' cellpadding='1'><tbody><tr><td style='width:50%'> <strong>Estd:1947</strong></td><td style='text-align: right;width:30%'> <strong>Form : XVI</strong></td></tr><tr><td></td><td align='right'>Duplicate</td></tr><tr><td></td><td align='right' style='color:red'>"+status+"</td></tr><tr align='center'><td colspan='2' style='line-height:75pt;'><img src=" + imageFile + "></td></tr></tbody></table>");
                                sb.Append(@"<center>
<h3 style='text-align:center;'>SREE VISALAM CHIT FUND LTD.</h3>

<p style='text-align:center;'>Registerd Office: Tirunelveli - 6</p>
<p style='text-align:center;line-height:8;'>(See Section 23 And Rule 25)</p>
<p style='text-align:center;line-height:8;'>Receipt Issued Under The Chit Funds Act 1982</p>
<p style='text-align:center;line-height:8;'><b>Branch Name: " + branchName + "</b></p></center><br/><body><center><div style='background-color:lavender;text-align:center;padding:5px;width:100%;'><table><tr><td align='left'><b>CD/REC.SERIES</b></td><td>Date: " + receiptDate + "</td></tr><tr><td align='left'>Receipt No: " + appReceiptno + "</td><td>Time: " + receiptTime + "</td></tr></table>" +
            "<p style='text-align:left;'>-----------------------------------------------------------------------------------------------------</p>" +
            "</div><table align='left' cellspacing='0' cellpadding='2' width=100%><tr><td colspan='3'>Received From </td><td colspan='1'>:</td><td colspan='6'>" + memberName + " </td></tr>" +
            "<tr><td colspan='3' >Ag. No/Year</td><td colspan='1'>:</td><td colspan='6'>" + agreementNo + " </td></tr><tr><td colspan='3'>Chit No</td>" +
            "<td colspan='1'>:</td><td colspan='6'>" + chitno + " </td></tr><tr><td align='left' colspan='3'>Installment No</td><td colspan='1'>:</td><td align='left' colspan='6'>" + paidInslmntNo + " </td></tr>" +
            "<tr><td align='left' colspan='3'>Amount Received</td><td colspan='1'>:</td><td align='left' colspan='6'>" + hindi.NumberFormat.CurrencySymbol + amt + " </td></tr><tr><td align='left' colspan='3'>Interest</td><td colspan='1'>:</td><td align='left' colspan='6'>" + intrst + " </td></tr>" +
            "<tr><td align='left' colspan='3'>Other Receipts</td><td colspan='1'>:</td><td align='left' colspan='6'>" + other + " </td></tr></table><br/><div style='background-color:royalblue; padding:5px;width:100%;'>" +
            "<table><tr style='color:white;'><td align='right' colspan='3'><strong>Total Paid&nbsp;&nbsp;</td><td colspan='1'></td><td align='left' colspan='6'>&#8377;" + totalAmt + "</td></tr></table> </strong></p></div><br/>" +
            "<p><strong>&#8377;&#x20B9;Note:</strong>This is computer generated Receipt hence signature is not required</p></center></body></html>");
            */
                                //alignment changed to fit monarch envelope papersize - bagya 07/02/2023


                                sb.Append(@"<html><body><table font-family width='100%' cellpadding='1'><tbody><tr><td style='width:50%'> <strong>Estd:1947</strong></td><td style='text-align: right;width:30%' > <strong>Form : XVI</strong></td></tr><tr align='right'><td></td><td>Duplicate</td></tr><tr><td></td><td align='right' style='color:red'>" + status + "</td></tr><tr align='center'><td colspan='2' style='line-height:75pt;'><img src=" + imageFile + "></td></tr></tbody></table>");
                                sb.Append(@"<center>
                <h3 style='text-align:center;line-height:0;'>SREE VISALAM CHIT FUND LTD.</h3>

                <p style='text-align:center;line-height:0;'>Registerd Office: Tirunelveli - 6</p>
                <p style='text-align:center;line-height:0;'>(See Section 23 And Rule 25)</p>
                <p style='text-align:center;line-height:0;'>Receipt Issued Under The Chit Funds Act 1982</p>
                <p style='text-align:left;line-height:0;'>---------------------------------------------------------------------------------------------------</p>
                <p style='text-align:center;line-height:0;'><b>Branch Name: " + branchName + "</b></p></center><p style='text-align:left;'>---------------------------------------------------------------------------------------------------</p><body><center><div style='background-color:lavender;text-align:center;padding:5px;'><table style='width:100%'><tr><td align='left'><b>CD/REC.SERIES</b></td><td><strong>Date:</strong> " + receiptDate + "</td></tr><tr><td align='left'><strong>Receipt No:</strong> " + appReceiptno + "</td><td><strong>Time:</strong> " + receiptTime + "</td></tr></table>" +
            "<p style='text-align:left;'>---------------------------------------------------------------------------------------------------</p><table align='left' cellspacing='0' cellpadding='2'><tr><td align='left'>Received From</td><td>:</td><td align='left'>" + memberName + " </td></tr>" +
            "<tr><td align='left'>Ag. No/Year</td><td>:</td><td align='left'>" + agreementNo + " </td></tr><tr><td align='left' >Chit No</td>" +
            "<td>:</td><td align='left'>" + chitno + " </td></tr><tr><td align='left'>Installment No</td><td>:</td><td align='left'>" + paidInslmntNo + " </td></tr>" +
            "<tr><td align='left'>Amount Received</td><td>:</td><td align='left'>" + amt + " </td></tr><tr><td align='left'>Interest</td><td>:</td><td align='left'>" + Interest + " </td></tr>" +
            "<tr><td align='left'>Other Receipts</td><td>:</td><td align='left'>" + other + " </td></tr></table><br/><div style='background-color:blue; padding:5px; width:40%;'>" +
            "<p style='text-align:left;line-height:0;'>---------------------------------------------------------------------------------------------------</p>" +
            "<table><tr ><td align='center'><strong>Total Paid&nbsp;&nbsp;" + totalAmt + "</strong></td></tr></table><p style='text-align:left;'>---------------------------------------------------------------------------------------------------</p>" +
            "<p><strong>Note:</strong>This is computer generated Receipt hence signature is not required</p></div></center></body></html>");


                                //sivanesan

                                //sivanesan added to fix pdf download error
                                StringReader sr = new StringReader(sb.ToString());

                                //var pageSize = new iTextSharp.text.Rectangle(372, 720);
                                var pageSize = new iTextSharp.text.Rectangle(300, 550);
                                //iTextSharp.text.Document pdfDoc = new iTextSharp.text.Document(pageSize, 25f, 25f, 25f, 25f);

                                iTextSharp.text.Document pdfDoc = new iTextSharp.text.Document(pageSize, 15f, 15f, 15f, 15f);
                                HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
                                PdfWriter writer = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);


                                var style = new StyleSheet();
                                style.LoadTagStyle("body", "size", "8px");
                                htmlparser.Style = style;

                                pdfDoc.Open();

                                htmlparser.Parse(sr);
                                pdfDoc.Close();
                                Response.ContentType = "application/pdf";
                                Response.AddHeader("content-disposition", "attachment;filename= " + appReceiptno + ".pdf");
                                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                                Response.Write(pdfDoc);
                                string path = HttpContext.Current.Server.MapPath("~/PDF_Receipts/" + appReceiptno + ".pdf");

                                HttpContext.Current.ApplicationInstance.CompleteRequest();


                                // old code

                                /* StringReader sr = new StringReader(sb.ToString());
                                //iTextSharp.text.Document pdfDoc = new iTextSharp.text.Document(PageSize.EXECUTIVE, 25f, 25f, 25f, 25f);
                                 //bagya 03/02/2023
                                 //iTextSharp.text.Document pdfDoc = new iTextSharp.text.Document(PageSize.LEGAL, 15f, 15f, 15f, 15f);
                                var pageSize = new iTextSharp.text.Rectangle(300, 550);
                                iTextSharp.text.Document pdfDoc = new iTextSharp.text.Document(pageSize, 15f, 15f, 15f, 15f);
                                HTMLWorker htmlparser = new HTMLWorker(pdfDoc);

                                //reduce the font size of pdf 11/10/2021
                                var style = new StyleSheet();
                                style.LoadTagStyle("body", "size", "8px");
                                htmlparser.Style = style;

                                 string path = HttpContext.Current.Server.MapPath("~/PDF_Receipts/" + appReceiptno + ".pdf");
                                 PdfWriter writer = PdfWriter.GetInstance(pdfDoc, new FileStream(path, FileMode.Create));

                                pdfDoc.Open();
                                htmlparser.Parse(sr);
                                pdfDoc.Close();
                                Response.ContentType = "application/pdf";
                                Response.AddHeader("content-disposition", "attachment;filename=" + appReceiptno + ".pdf");
                                 //Response.Cache.SetCacheability(HttpCacheability.NoCache);
                                Response.Write(pdfDoc);
                                 HttpContext.Current.ApplicationInstance.CompleteRequest();*/



                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                #endregion PDF Creation
            }
        }

        #region ddl_selectedIndex
        //protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    loadMoneyCollector();
        //}

        //protected void ddlMoneyCollector_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    loadSeries();
        //}

        #endregion ddl_selectedIndex
    }
}