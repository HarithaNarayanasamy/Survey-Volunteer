using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using DevExpress.XtraPrinting;
using System.Drawing;
using DevExpress.XtraPrintingLinks;
using DevExpress.Web.ASPxMenu;
using DevExpress.Web.ASPxGridView;
using System.Data;
using System.Web.Security;
using SVCF_BusinessAccessLayer;
using SVCF_TransactionLayer;
using SVCF_DataAccessLayer;
namespace SreeVisalamChitFundLtd_phase1
{
    public partial class thirdcolumnbook : System.Web.UI.Page
    {
        #region VarDeclaration
        BusinessLayer balayer = new BusinessLayer();
        TransactionLayer tranlayer = new TransactionLayer();
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
               // GetGroupMember();
                txtFromDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                //txtToDate.Text = "31/05/2013";
                select();

                foreach (GridViewColumn column in grid.Columns)
                {
                    if (column is GridViewDataColumn)
                    {
                        ((GridViewDataColumn)column).Settings.AutoFilterCondition = AutoFilterCondition.Contains;
                    }
                }
            }
            select();
        }
        //public void GetGroupMember()
        //{
        //    ddlChit.DataSource = null;
        //    ddlChit.DataBind();
        //    DataTable dtChitGrp = balayer.GetDataTable("SELECT distinct `voucher`.`ChitGroupId`, `groupmaster`.`GROUPNO` FROM `svcf`.`voucher` join `svcf`.`groupmaster` on (`groupmaster`.`Head_Id`=`voucher`.`ChitGroupId`) where `voucher`.`Trans_Type`=2 and `voucher`.`BranchID`=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + "");
        //    DataRow dr = dtChitGrp.NewRow();
        //    dr[0] = "0";
        //    dr[1] = "--Select--";
        //    ddlChit.DataSource = dtChitGrp;
        //    ddlChit.DataTextField = "GROUPNO";
        //    ddlChit.DataValueField = "ChitGroupId";
        //    dtChitGrp.Rows.InsertAt(dr, 0);
        //    ddlChit.DataBind();

        //}
        protected void select()
        {
            //if (ddlChit.SelectedIndex == 0)
            //{
            //    AccessDataSource1.SelectCommand = @"select date_format(ChoosenDate,'%d-%m-%Y') as Date,Voucher_No,m1.MemberName,m1.GrpMemberId, sum(case when (v1.Voucher_Type='C' and (v1.Trans_Type in (0,1))) then Amount else 0.00 end) as Credit,sum(case when (v1.Voucher_Type='D' and (v1.Trans_Type in (2))) then Amount else 0.00 end) as Debit,(case when (sum(case when (v1.Voucher_Type='C' and (v1.Trans_Type in (0,1))) then Amount else 0.00 end)>sum(case when (v1.Voucher_Type='D' and (v1.Trans_Type in (2))) then Amount else 0.00 end) ) then 'Cr' else (case when (sum(case when (v1.Voucher_Type='C' and (v1.Trans_Type in (0,1))) then Amount else 0.00 end)<sum(case when (v1.Voucher_Type='D' and (v1.Trans_Type in (2))) then Amount else 0.00 end) ) then 'Dr' else 'CrDr' end) end) as CrDr ,(case when (sum(case when (v1.Voucher_Type='C' and (v1.Trans_Type in (0,1))) then Amount else 0.00 end)>sum(case when (v1.Voucher_Type='D' and (v1.Trans_Type in (2))) then Amount else 0.00 end) ) then sum(case when (v1.Voucher_Type='C' and (v1.Trans_Type in (0,1))) then Amount else 0.00 end)-sum(case when (v1.Voucher_Type='D' and (v1.Trans_Type in (2))) then Amount else 0.00 end) else (case when (sum(case when (v1.Voucher_Type='C' and (v1.Trans_Type in (0,1))) then Amount else 0.00 end)<sum(case when (v1.Voucher_Type='D' and (v1.Trans_Type in (2))) then Amount else 0.00 end) ) then sum(case when (v1.Voucher_Type='D' and (v1.Trans_Type in (2))) then Amount else 0.00 end)-sum(case when (v1.Voucher_Type='C' and (v1.Trans_Type in (0,1))) then Amount else 0.00 end)else '0.00' end) end) as Balance from voucher as v1 join membertogroupmaster as m1 on (v1.Head_Id=m1.Head_Id) where v1.ChoosenDate='ssss' group by v1.MemberID order by v1.ChitGroupId";
            //}
            //else
            //{
                grid.SettingsPager.Mode = GridViewPagerMode.ShowAllRecords;
                AccessDataSource1.ConnectionString = CommonClassFile.ConnectionString;
                AccessDataSource1.SelectCommand = @"select date_format(ChoosenDate,'%d-%m-%Y') as Date,g1.GROUPNO, sum(case when (v1.Voucher_Type='C' ) then Amount else 0.00 end) as Credit,sum(case when (v1.Voucher_Type='D') then Amount else 0.00 end) as Debit,(case when (sum(case when (v1.Voucher_Type='C' ) then Amount else 0.00 end)>sum(case when (v1.Voucher_Type='D' ) then Amount else 0.00 end) ) then 'Cr' else (case when (sum(case when (v1.Voucher_Type='C' ) then Amount else 0.00 end)<sum(case when (v1.Voucher_Type='D' ) then Amount else 0.00 end) ) then 'Dr' else 'CrDr' end) end) as CrDr ,(case when (sum(case when (v1.Voucher_Type='C' ) then Amount else 0.00 end)>sum(case when (v1.Voucher_Type='D' ) then Amount else 0.00 end) ) then sum(case when (v1.Voucher_Type='C' ) then Amount else 0.00 end)-sum(case when (v1.Voucher_Type='D' ) then Amount else 0.00 end) else (case when (sum(case when (v1.Voucher_Type='C' ) then Amount else 0.00 end)<sum(case when (v1.Voucher_Type='D' ) then Amount else 0.00 end) ) then sum(case when (v1.Voucher_Type='D' ) then Amount else 0.00 end)-sum(case when (v1.Voucher_Type='C' ) then Amount else 0.00 end)else '0.00' end) end) as Balance from voucher as v1 join groupmaster as g1 on (v1.ChitGroupID=g1.Head_Id) where v1.ChoosenDate='" + balayer.indiandateToMysqlDate(txtFromDate.Text) + "' and v1.BranchID=" + Session["Branchid"] + " and v1.Rootid=5 group by v1.ChitGroupId";
           // }
            grid.DataBind();
        }

        protected void BtnStatisticsGo_Click(object sender, EventArgs e)
        {
            select();
        }

        void ApplyLayout(int layoutIndex)
        {
            grid.BeginUpdate();
            try
            {
                grid.ClearSort();
                switch (layoutIndex)
                {
                    case 0:
                        grid.GroupBy(grid.Columns["ChitGroup"]);
                        break;
                    case 1:
                        break;
                    case 2:
                        break;
                }
            }
            finally
            {
                grid.EndUpdate();
            }
            grid.CollapseAll();
        }
        protected void grid_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
        }

        protected void Export_click(object sender, MenuItemEventArgs e)
        {
            if (e.Item.Text.ToString() == "PDF")
            {
                using (this.headerImage = System.Drawing.Image.FromFile(Server.MapPath("~\\Images\\visalam.png")))
                {
                    PrintingSystem ps = new PrintingSystem();
                    PrintableComponentLink gridPayment = new PrintableComponentLink(ps);
                    gridPayment.Component = gridExport;
                    Link header = new Link();
                    CompositeLink compositeLink = new CompositeLink(ps);
                    header.CreateDetailArea += new CreateAreaEventHandler(header_CreateDetailArea);
                    compositeLink.Links.AddRange(new object[] { header, gridPayment });
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
                        WriteToResponse("thirdcolumnbook", true, "pdf", stream);
                    }
                }
            }
            else if (e.Item.Text.ToString() == "XLSX")
            {
                gridExport.WriteXlsxToResponse();
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