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
    public partial class chitsdetails : System.Web.UI.Page
    {
        #region Object
        BusinessLayer balayer = new BusinessLayer();
        TransactionLayer trn = new TransactionLayer();
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataTable dt = balayer.GetDataTable("select * from groupmaster where BranchId=" + Session["Branchid"] + " and IsFinished=0");
                ddlGroupNumber.AppendDataBoundItems = true;
                ddlGroupNumber.DataSource = dt;
                ddlGroupNumber.DataTextField = "GROUPNO";
                ddlGroupNumber.DataValueField = "Head_Id";
                ddlGroupNumber.DataBind();
                foreach (GridViewColumn column in gridApprove.Columns)
                {
                    if (column is GridViewDataColumn)
                    {
                        ((GridViewDataColumn)column).Settings.AutoFilterCondition = AutoFilterCondition.Contains;
                    }
                }
                
                foreach (GridViewColumn column in gridSuggest.Columns)
                {
                    if (column is GridViewDataColumn)
                    {
                        ((GridViewDataColumn)column).Settings.AutoFilterCondition = AutoFilterCondition.Contains;
                    }
                }
            }
            BindData();
        }
        protected void BtnStatisticsGo_Click(object sender, EventArgs e)
        {
            BindData();
        }
        protected void BindData()
        {
            dsSuggested.ConnectionString = CommonClassFile.ConnectionString;
            dsSuggested.SelectCommand = @"SELECT m1.slno, concat( m2.memberID, ' | ', m2.CustomerName,' | ', m2.ResidentialAddress) as  subscriber ,date_format(m1.SuggestedDate ,'%d/%m/%Y') as SuggestedDate,date_format(m1.ApprovedDate ,'%d/%m/%Y') as ApprovedDate,m1.NoofTokens as Nooftokens,m1.NoofTokensApproved FROM svcf.membersuggestion as m1 join membermaster as m2 on (m1.MemberID=m2.MemberIDNew) join groupmaster as g1 on (m1.GroupNo=g1.Head_Id) where m1.GroupNo=" + ddlGroupNumber.SelectedItem.Value + " and g1.Head_Id=" + ddlGroupNumber.SelectedItem.Value;
            gridSuggest.DataBind();
            dsApproved.ConnectionString = CommonClassFile.ConnectionString;
            dsApproved.SelectCommand = @"SELECT m1.GrpMemberID,m2.MemberID,m2.CustomerName ,  m2.ResidentialAddress FROM svcf.membertogroupmaster as m1 join membermaster as m2 on (m1.MemberID=m2.MemberIDNew) where m1.GroupID=" + ddlGroupNumber.SelectedItem.Value + " order by cast(digits(m1.GrpMemberID) as signed)";
            gridApprove.DataBind();
           
        }
        private System.Drawing.Image headerImage;
        protected void Export_click(object sender, MenuItemEventArgs e)
        {
            if (e.Item.Text.ToString() == "PDF")
            {
                using (this.headerImage = System.Drawing.Image.FromFile(Server.MapPath("~\\Images\\visalam.png")))
                {
                    PrintingSystem ps = new PrintingSystem();
                    PrintableComponentLink pAdvances = new PrintableComponentLink(ps);
                    pAdvances.Component = gridExport;

                    PrintableComponentLink pstamps = new PrintableComponentLink(ps);
                    pstamps.Component = gridExport1;

                    Link header = new Link();
                    CompositeLink compositeLink = new CompositeLink(ps);
                    header.CreateDetailArea += new CreateAreaEventHandler(header_CreateDetailArea);
                    compositeLink.Links.AddRange(new object[] { header, pAdvances, pstamps });
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
                        compositeLink.CreateDocument();
                        compositeLink.PrintingSystem.Document.AutoFitToPagesWidth = 1;
                        compositeLink.PrintingSystem.ExportToPdf(stream);
                        WriteToResponse("ChitDetails", true, "pdf", stream);
                    }
                    ps.Dispose();
                }
                
            }
            else if (e.Item.Text.ToString() == "XLSX")
            {
                gridExport.PreserveGroupRowStates = true;
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