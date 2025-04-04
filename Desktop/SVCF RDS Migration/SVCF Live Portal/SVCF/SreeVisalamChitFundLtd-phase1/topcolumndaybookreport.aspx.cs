using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using DevExpress.XtraPrinting;
using System.IO;
using DevExpress.XtraPrintingLinks;
using DevExpress.Web.ASPxMenu;
using DevExpress.Web.ASPxGridView;
using System.Data;
using System.Web.Security;
using SVCF_BusinessAccessLayer;
using SVCF_DataAccessLayer;
using SVCF_TransactionLayer;

namespace SreeVisalamChitFundLtd_phase1
{
    public partial class topcolumndaybookreport : System.Web.UI.Page
    {
        #region Object
        BusinessLayer balayer = new BusinessLayer();
        TransactionLayer trn = new TransactionLayer();
        #endregion
        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (Session["Branchid"] == null || Session["UserName"] == null || Session["BranchName"] == null)
            {
                Response.Cache.SetExpires(DateTime.UtcNow.AddMilliseconds(-1));
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.Cache.SetNoStore();
                Session.RemoveAll();
                Session.Abandon();
                Response.ClearHeaders();
                FormsAuthentication.SignOut();
                Response.Redirect(Page.ResolveUrl("~/Login.aspx"), true);
            }
            //Response.Redirect("Login.aspx");
        }
        private System.Drawing.Image headerImage;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtFromDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtToDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                select();

                foreach (GridViewColumn column in gridCredit.Columns)
                {
                    if (column is GridViewDataColumn)
                    {
                        ((GridViewDataColumn)column).Settings.AutoFilterCondition = AutoFilterCondition.Contains;
                    }
                }
                foreach (GridViewColumn column in gridDebit.Columns)
                {
                    if (column is GridViewDataColumn)
                    {
                        ((GridViewDataColumn)column).Settings.AutoFilterCondition = AutoFilterCondition.Contains;
                    }
                }
            }
            select();
        }
        protected void select()
        {
            dsCredit.ConnectionString = CommonClassFile.ConnectionString;
            dsDebit.ConnectionString = CommonClassFile.ConnectionString;
            if (Convert.ToInt32(Session["Branchid"]) == 161)
            {
                dsDebit.SelectCommand = @"select date_format(v1.ChoosenDate,'%d-%m-%Y') as Date, SUM(case when(v1.Voucher_Type='D' and v1.Rootid=1) then v1.Amount else 0.00 end) as BranchDebit,sum(case when(v1.Voucher_Type='D' and v1.Rootid=2) then v1.Amount else 0.00 end) as InvestmentDebit,sum(case when(v1.Voucher_Type='D' and v1.Rootid=3) then v1.Amount else 0.00 end) as BankDebit,sum(case when(v1.Voucher_Type='D' and v1.Rootid=4) then v1.Amount else 0.00 end) as OtherItemsDebit,sum(case when(v1.Voucher_Type='D' and v1.Rootid=5) then v1.Amount else 0.00 end) as ChitsDebit,sum(case when(v1.Voucher_Type='D' and v1.Rootid=6) then v1.Amount else 0.00 end) as ForemanDebit,sum(case when(v1.Voucher_Type='D' and v1.Rootid=7) then v1.Amount else 0.00 end) as DecreeDebtorsDebit,sum(case when(v1.Voucher_Type='D' and v1.Rootid=8) then v1.Amount else 0.00 end) as LoansDebit,sum(case when(v1.Voucher_Type='D' and v1.Rootid=9) then v1.Amount else 0.00 end) as AdvancesDebit,sum(case when(v1.Voucher_Type='D' and v1.Rootid=10) then v1.Amount else 0.00 end) as StampsDebit,sum(case when(v1.Voucher_Type='D' and v1.Rootid=11) then v1.Amount else 0.00 end) as ProfitandlossDebit,sum(case when(v1.Voucher_Type='D' and v1.Rootid=12) then v1.Amount else 0.00 end) as CashDebit,sum(case when(v1.Voucher_Type='D') then v1.Amount else 0.00 end) as Debit from voucher as v1 join headstree as h1 on (v1.rootid=h1.Nodeid) where v1.choosendate between '" + balayer.indiandateToMysqlDate(txtFromDate.Text) + "' and '" + balayer.indiandateToMysqlDate(txtToDate.Text) + "' and (v1.branchid=" + Session["Branchid"] + " or v1.`BranchID`=160 or v1.`BranchID`=162) group by v1.ChoosenDate order by v1.ChoosenDate";
                dsCredit.SelectCommand = @"select date_format(v1.ChoosenDate,'%d-%m-%Y') as Date, SUM(case when(v1.Voucher_Type='C' and v1.Rootid=1) then v1.Amount else 0.00 end) as BranchCredit,sum(case when(v1.Voucher_Type='C' and v1.Rootid=2) then v1.Amount else 0.00 end) as InvestmentCredit,sum(case when(v1.Voucher_Type='C' and v1.Rootid=3) then v1.Amount else 0.00 end) as BankCredit,sum(case when(v1.Voucher_Type='C' and v1.Rootid=4) then v1.Amount else 0.00 end) as OtherItemsCredit,sum(case when(v1.Voucher_Type='C' and v1.Rootid=5) then v1.Amount else 0.00 end) as ChitsCredit,sum(case when(v1.Voucher_Type='C' and v1.Rootid=6) then v1.Amount else 0.00 end) as ForemanCredit,sum(case when(v1.Voucher_Type='C' and v1.Rootid=7) then v1.Amount else 0.00 end) as DecreeDebtorsCredit,sum(case when(v1.Voucher_Type='C' and v1.Rootid=8) then v1.Amount else 0.00 end) as LoansCredit,sum(case when(v1.Voucher_Type='C' and v1.Rootid=9) then v1.Amount else 0.00 end) as AdvancesCredit,sum(case when(v1.Voucher_Type='C' and v1.Rootid=10) then v1.Amount else 0.00 end) as StampsCredit,sum(case when(v1.Voucher_Type='C' and v1.Rootid=11) then v1.Amount else 0.00 end) as ProfitandlossCredit,sum(case when(v1.Voucher_Type='C' and v1.Rootid=12) then v1.Amount else 0.00 end) as CashCredit,sum(case when(v1.Voucher_Type='C') then v1.Amount else 0.00 end) as Credit from voucher as v1 join headstree as h1 on (v1.rootid=h1.Nodeid) where v1.choosendate between '" + balayer.indiandateToMysqlDate(txtFromDate.Text) + "' and '" + balayer.indiandateToMysqlDate(txtToDate.Text) + "' and (v1.branchid=" + Session["Branchid"] + " or v1.`BranchID`=160 or v1.`BranchID`=162) group by v1.ChoosenDate order by v1.ChoosenDate";
            }
            else
            {
                dsDebit.SelectCommand = @"select date_format(v1.ChoosenDate,'%d-%m-%Y') as Date, SUM(case when(v1.Voucher_Type='D' and v1.Rootid=1) then v1.Amount else 0.00 end) as BranchDebit,sum(case when(v1.Voucher_Type='D' and v1.Rootid=2) then v1.Amount else 0.00 end) as InvestmentDebit,sum(case when(v1.Voucher_Type='D' and v1.Rootid=3) then v1.Amount else 0.00 end) as BankDebit,sum(case when(v1.Voucher_Type='D' and v1.Rootid=4) then v1.Amount else 0.00 end) as OtherItemsDebit,sum(case when(v1.Voucher_Type='D' and v1.Rootid=5) then v1.Amount else 0.00 end) as ChitsDebit,sum(case when(v1.Voucher_Type='D' and v1.Rootid=6) then v1.Amount else 0.00 end) as ForemanDebit,sum(case when(v1.Voucher_Type='D' and v1.Rootid=7) then v1.Amount else 0.00 end) as DecreeDebtorsDebit,sum(case when(v1.Voucher_Type='D' and v1.Rootid=8) then v1.Amount else 0.00 end) as LoansDebit,sum(case when(v1.Voucher_Type='D' and v1.Rootid=9) then v1.Amount else 0.00 end) as AdvancesDebit,sum(case when(v1.Voucher_Type='D' and v1.Rootid=10) then v1.Amount else 0.00 end) as StampsDebit,sum(case when(v1.Voucher_Type='D' and v1.Rootid=11) then v1.Amount else 0.00 end) as ProfitandlossDebit,sum(case when(v1.Voucher_Type='D' and v1.Rootid=12) then v1.Amount else 0.00 end) as CashDebit,sum(case when(v1.Voucher_Type='D') then v1.Amount else 0.00 end) as Debit from voucher as v1 join headstree as h1 on (v1.rootid=h1.Nodeid) where v1.choosendate between '" + balayer.indiandateToMysqlDate(txtFromDate.Text) + "' and '" + balayer.indiandateToMysqlDate(txtToDate.Text) + "' and v1.branchid=" + Session["Branchid"] + " group by v1.ChoosenDate order by v1.ChoosenDate";
                dsCredit.SelectCommand = @"select date_format(v1.ChoosenDate,'%d-%m-%Y') as Date, SUM(case when(v1.Voucher_Type='C' and v1.Rootid=1) then v1.Amount else 0.00 end) as BranchCredit,sum(case when(v1.Voucher_Type='C' and v1.Rootid=2) then v1.Amount else 0.00 end) as InvestmentCredit,sum(case when(v1.Voucher_Type='C' and v1.Rootid=3) then v1.Amount else 0.00 end) as BankCredit,sum(case when(v1.Voucher_Type='C' and v1.Rootid=4) then v1.Amount else 0.00 end) as OtherItemsCredit,sum(case when(v1.Voucher_Type='C' and v1.Rootid=5) then v1.Amount else 0.00 end) as ChitsCredit,sum(case when(v1.Voucher_Type='C' and v1.Rootid=6) then v1.Amount else 0.00 end) as ForemanCredit,sum(case when(v1.Voucher_Type='C' and v1.Rootid=7) then v1.Amount else 0.00 end) as DecreeDebtorsCredit,sum(case when(v1.Voucher_Type='C' and v1.Rootid=8) then v1.Amount else 0.00 end) as LoansCredit,sum(case when(v1.Voucher_Type='C' and v1.Rootid=9) then v1.Amount else 0.00 end) as AdvancesCredit,sum(case when(v1.Voucher_Type='C' and v1.Rootid=10) then v1.Amount else 0.00 end) as StampsCredit,sum(case when(v1.Voucher_Type='C' and v1.Rootid=11) then v1.Amount else 0.00 end) as ProfitandlossCredit,sum(case when(v1.Voucher_Type='C' and v1.Rootid=12) then v1.Amount else 0.00 end) as CashCredit,sum(case when(v1.Voucher_Type='C') then v1.Amount else 0.00 end) as Credit from voucher as v1 join headstree as h1 on (v1.rootid=h1.Nodeid) where v1.choosendate between '" + balayer.indiandateToMysqlDate(txtFromDate.Text) + "' and '" + balayer.indiandateToMysqlDate(txtToDate.Text) + "' and v1.branchid=" + Session["Branchid"] + " group by v1.ChoosenDate order by v1.ChoosenDate";
            }          


            gridCredit.DataBind();
            gridDebit.DataBind();
        }
        protected void gridCredit_CustomUnboundColumnData(object sender, ASPxGridViewColumnDataEventArgs e)
        {
            if (e.Column.FieldName == "Sl. No.")
            {
                e.Value = string.Format("{0}", e.ListSourceRowIndex + 1);
            }
        }
        protected void gridDebit_CustomUnboundColumnData(object sender, ASPxGridViewColumnDataEventArgs e)
        {
            if (e.Column.FieldName == "Sl. No.")
            {
                e.Value = string.Format("{0}", e.ListSourceRowIndex + 1);
            }
        }
        protected void BtnStatisticsGo_Click(object sender, EventArgs e)
        {
            select();
        }

        void ApplyLayout(int layoutIndex)
        {
            gridCredit.BeginUpdate();
            try
            {
                gridCredit.ClearSort();
                switch (layoutIndex)
                {
                    case 0:
                        gridCredit.GroupBy(gridCredit.Columns["ChitGroup"]);
                        break;
                    case 1:
                        break;
                    case 2:
                        break;
                }
            }
            finally
            {
                gridCredit.EndUpdate();
            }
            gridCredit.CollapseAll();
        }

        protected void Export_click(object sender, MenuItemEventArgs e)
        {
            if (e.Item.Text.ToString() == "PDF")
            {
                using (this.headerImage = System.Drawing.Image.FromFile(Server.MapPath("~\\Images\\visalam.png")))
                {
                    PrintingSystem ps = new PrintingSystem();
                    PrintableComponentLink gridCredit = new PrintableComponentLink(ps);
                    gridCredit.Component = gridCreditExport;

                    PrintableComponentLink gridDebit = new PrintableComponentLink(ps);
                    gridDebit.Component = gridDebitExport;

                    Link header = new Link();
                    CompositeLink compositeLink = new CompositeLink(ps);
                    header.CreateDetailArea += new CreateAreaEventHandler(header_CreateDetailArea);
                    compositeLink.Links.AddRange(new object[] { header, gridCredit,gridDebit });
                    string leftColumn = "Pages : [Page # of Pages #]";
                    string rightColumn = "Date: [Date Printed]\r\nTime: " + DateTime.Now.ToString("hh:mm tt");
                    PageHeaderFooter phf = compositeLink.PageHeaderFooter as PageHeaderFooter;
                    phf.Footer.Content.Clear();
                    phf.Footer.Content.AddRange(new string[] { rightColumn, leftColumn });
                    phf.Footer.LineAlignment = BrickAlignment.Center;
                    using (MemoryStream stream = new MemoryStream())
                    {
                        compositeLink.PaperKind = System.Drawing.Printing.PaperKind.A4;
                        compositeLink.Landscape = true;
                        compositeLink.CreateDocument(false);
                        compositeLink.PrintingSystem.Document.AutoFitToPagesWidth = 1;
                        compositeLink.PrintingSystem.ExportToPdf(stream);
                        WriteToResponse("topcolumndaybook", true, "pdf", stream);
                    }
                }
            }
            else if (e.Item.Text.ToString() == "XLSX")
            {
                gridCreditExport.WriteXlsxToResponse();
                gridDebitExport.WriteXlsxToResponse();
            }
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