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
using DevExpress.Web.ASPxGridView;
using DevExpress.Data;
using DevExpress.Web.ASPxMenu;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrintingLinks;
using System.IO;
using DevExpress.Web.ASPxGridView.Export;
using System.Drawing;
using SVCF_BusinessAccessLayer;
using SVCF_DataAccessLayer;
using SVCF_TransactionLayer;

namespace SreeVisalamChitFundLtd_phase1
{
    public partial class cashregister : System.Web.UI.Page
    {
        #region Object
        BusinessLayer balayer = new BusinessLayer();
        TransactionLayer trn = new TransactionLayer();
        #endregion
        private System.Drawing.Image headerImage;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //aaaaa.Checked = true;
                //chkLoadKasr.Checked = true;
                txtFromDate1.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtToDate1.Text = DateTime.Now.ToString("dd/MM/yyyy");
                
                foreach (GridViewColumn column in grid.Columns)
                {
                    if (column is GridViewDataColumn)
                    {
                        ((GridViewDataColumn)column).Settings.AutoFilterCondition = AutoFilterCondition.Contains;
                    }
                }
            }
            BindData();
        }
        protected void BtnStatisticsGo_Click1(object sender, EventArgs e)
        {
            BindData();
        }
        protected void ASPxGridView1_SummaryDisplayText(object sender, ASPxGridViewSummaryDisplayTextEventArgs e)
        {
            if (e.Item.FieldName == "Credit")
                e.Text = string.Format("{0}", Convert.ToDouble(e.Value));
            if (e.Item.FieldName == "Debit")
                e.Text = string.Format("{0}", Convert.ToDouble(e.Value));
           
        }
        protected void BindData()
        {
            AccessDataSource1.ConnectionString = CommonClassFile.ConnectionString;
            //AccessDataSource1.SelectCommand = @"SELECT `voucher`.`Narration`, voucher.RootID as RootID,`voucher`.`ChoosenDate` as `Date`,h1.Node as LedgerHead,`headstree`.`Node`,((case when voucher.Voucher_Type='C' then voucher.Amount else 0.00 end )) as `Credit`,((case when voucher.Voucher_Type='D' then voucher.Amount else 0.00 end )) as `Debit` FROM `svcf`.`voucher` join headstree on (voucher.RootID=headstree.NodeID) join headstree as h1 on (`voucher`.`Head_Id`=`h1`.`NodeID`) where voucher.BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and voucher.ChoosenDate between '" + balayer.indiandateToMysqlDate(txtFromDate1.Text) + "' and '" + balayer.indiandateToMysqlDate(txtToDate1.Text) + "' and Other_Trans_Type<>5 order by voucher.RootID";
            AccessDataSource1.SelectCommand = @"SELECT distinct v2.TransactionKey, date_format(v1.ChoosenDate,'%d-%m-%Y') as ChoosenDate,v1.Voucher_No,h1.Node,(case when v1.Voucher_Type='C' then v1.Amount else 0.00 end) as Credit,(case when v1.Voucher_Type='D' then v1.Amount else 0.00 end) as Debit  FROM svcf.voucher as v1 join voucher as v2 on (v1.DualTransactionKey=v2.DualTransactionKey) join headstree as h1 on ( v2.Head_ID=h1.NodeID) where v2.BranchID=" + Session["Branchid"] + " and v1.BranchID=" + Session["Branchid"] + " and v2.RootID<>12 and  v1.RootID=12 and v1.ChoosenDate between '" + balayer.indiandateToMysqlDate(txtFromDate1.Text) + "' and '" + balayer.indiandateToMysqlDate(txtToDate1.Text) + "'  order by v1.ChoosenDate";
            grid.DataBind();
        }
        protected void Export_click(object sender, MenuItemEventArgs e)
        {
            if (e.Item.Text.ToString() == "PDF")
            {
                using (this.headerImage = System.Drawing.Image.FromFile(Server.MapPath("~\\Images\\visalam.png")))
                {
                    PrintingSystem ps = new PrintingSystem();
                    PrintableComponentLink grid = new PrintableComponentLink(ps);
                    grid.Component = gridExport;



                    Link header = new Link();
                    CompositeLink compositeLink = new CompositeLink(ps);
                    header.CreateDetailArea += new CreateAreaEventHandler(header_CreateDetailArea);
                    compositeLink.Links.AddRange(new object[] { header, grid });
                    string leftColumn = "Pages : [Page # of Pages #]";
                    string rightColumn = "Date: [Date Printed]\r\nTime: " + DateTime.Now.ToString("hh:mm tt");
                    PageHeaderFooter phf = compositeLink.PageHeaderFooter as PageHeaderFooter;
                    phf.Footer.Content.Clear();
                    phf.Footer.Content.AddRange(new string[] { rightColumn, leftColumn });
                    phf.Footer.LineAlignment = BrickAlignment.Center;
                    using (MemoryStream stream = new MemoryStream())
                    {
                        compositeLink.PaperKind = System.Drawing.Printing.PaperKind.A4;
                        //compositeLink.Landscape = true;
                        compositeLink.CreateDocument(false);

                        compositeLink.PrintingSystem.Document.AutoFitToPagesWidth = 1;
                        compositeLink.PrintingSystem.ExportToPdf(stream);
                        WriteToResponse("cashregister", true, "pdf", stream);
                    }
                }
            }
            //else if (e.Item.Text.ToString() == "XLSX")
            //{
            //    gridCreditExport.WriteXlsxToResponse();
            //    gridDebitExport.WriteXlsxToResponse();
            //}
        }
        void header_CreateDetailArea(object sender, CreateAreaEventArgs e)
        {
            e.Graph.BorderWidth = 0;
            Rectangle r = new Rectangle(0, 0, 50, 50);
            e.Graph.DrawImage(headerImage, r);
            TextBrick tb = new TextBrick();
            tb.Text = "SREE VISALAM CHIT FUND LTD.,";
            tb.Font = new Font("Arial", 10, FontStyle.Bold);
            tb.Rect = new RectangleF(50, 15, 260, 19);
            tb.BorderWidth = 0;
            tb.BackColor = Color.Transparent;
            tb.HorzAlignment = DevExpress.Utils.HorzAlignment.Center;
            tb.VertAlignment = DevExpress.Utils.VertAlignment.Top;
            e.Graph.DrawBrick(tb);
            TextBrick tb1 = new TextBrick();
            tb1.Text = "BRANCH : " + Session["BranchName"];
            tb1.Font = new Font("Arial", 9, FontStyle.Bold);
            tb1.Rect = new RectangleF(50, 34, 260, 25);
            tb1.BorderWidth = 0;
            tb1.BackColor = Color.Transparent;
            tb1.HorzAlignment = DevExpress.Utils.HorzAlignment.Center;
            tb1.VertAlignment = DevExpress.Utils.VertAlignment.Top;
            e.Graph.DrawBrick(tb1);
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
        
    }
}