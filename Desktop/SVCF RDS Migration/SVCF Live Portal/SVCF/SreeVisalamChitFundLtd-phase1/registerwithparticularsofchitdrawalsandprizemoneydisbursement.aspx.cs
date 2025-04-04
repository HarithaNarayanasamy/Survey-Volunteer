using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.XtraPrinting;
using System.Drawing;
using System.IO;
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
    public partial class registerwithparticularsofchitdrawalsandprizemoneydisbursement : System.Web.UI.Page
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
                GetGroupMember();
                txtFromDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtToDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
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
        public void GetGroupMember()
        {
            ddlChit.DataSource = null;
            ddlChit.DataBind();
            DataTable dtChitGrp = balayer.GetDataTable("SELECT distinct t1.ChitGroupID,g1.GROUPNO FROM svcf.trans_payment as t1 join groupmaster as g1 on (t1.ChitGroupID=g1.Head_Id) where g1.IsFinished=0 and g1.BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + "");
            //ddlChit.AppendDataBoundItems = true;
            DataRow dr = dtChitGrp.NewRow();
            dr[1] = "--Select--";
            dr[0] = "0";
            ddlChit.DataSource = dtChitGrp;
            ddlChit.DataTextField = "GROUPNO";
            ddlChit.DataValueField = "ChitGroupID";
            dtChitGrp.Rows.InsertAt(dr, 0);
            ddlChit.DataBind();

        }
        protected void select()
        {
            if (ddlChit.SelectedIndex == 0)
            {
                AccessDataSource1.ConnectionString = CommonClassFile.ConnectionString;
                AccessDataSource1.SelectCommand = @"SELECT g1.GROUPNO as ChitNo,t1.DrawNo as InstNo,date_format(a1.AuctionDate,'%d-%m-%Y') as DateofAuction,g1.ChitValue as ChitAmount,concat( m2.GrpMemberID,' | ',m1.MemberID, ' | ', m1.CustomerName) as NameofthePrizedSubscriber,t1.PrizedAmount as PrizedMoney,date_format(PaymentApplyedOn,'%d-%m-%Y') as  formsentforapprovalon,date_format(PaymentDate,'%d-%m-%Y') as dateofpayment,g2.GuarantorName,t1.Description as Remarks FROM svcf.trans_payment as t1 join svcf.auctiondetails as a1 on (t1.ChitGroupID=a1.GroupID and t1.DrawNO=a1.DrawNO) join groupmaster as g1 on (t1.ChitGroupID=g1.Head_Id) join membermaster as m1 on (a1.PrizedMemberID=m1.MemberIDNew) join guarantormaster as g2 on (t1.GuarantorID=g2.GuarantorID) join membertogroupmaster as m2 on (t1.TokenNumber=m2.Head_Id) where t1.ChitGroupID=null order by t1.DrawNo ";
            }
            else
            {
                grid.SettingsPager.Mode = GridViewPagerMode.ShowAllRecords;
                AccessDataSource1.ConnectionString = CommonClassFile.ConnectionString;
                AccessDataSource1.SelectCommand = @"SELECT g1.GROUPNO as ChitNo,t1.DrawNo as InstNo,date_format(a1.AuctionDate,'%d-%m-%Y') as DateofAuction,g1.ChitValue as ChitAmount,concat( m2.GrpMemberID,' | ',m1.MemberID, ' | ', m1.CustomerName) as NameofthePrizedSubscriber,t1.PrizedAmount as PrizedMoney,date_format(PaymentApplyedOn,'%d-%m-%Y') as  formsentforapprovalon,date_format(PaymentDate,'%d-%m-%Y') as dateofpayment,g2.GuarantorName,t1.Description as Remarks FROM svcf.trans_payment as t1 join svcf.auctiondetails as a1 on (t1.TokenNumber=a1.PrizedMemberID) join groupmaster as g1 on (t1.ChitGroupID=g1.Head_Id) left join membermaster as m1 on (a1.MemberID=m1.MemberIDNew) left join guarantormaster as g2 on (t1.GuarantorID=g2.GuarantorID) join membertogroupmaster as m2 on (t1.TokenNumber=m2.Head_Id) where t1.ChitGroupID=" + ddlChit.SelectedItem.Value + " and t1.BranchID=" + Session["Branchid"] + " and t1.PaymentDate between '" + balayer.indiandateToMysqlDate(txtFromDate.Text) + "' and '" + balayer.indiandateToMysqlDate(txtToDate.Text) + "' order by t1.DrawNo";
            }
            grid.DataBind();
        }
        protected void grid_CustomUnboundColumnData(object sender, ASPxGridViewColumnDataEventArgs e)
        {
            if (e.Column.FieldName == "Sl. No.")
            {
                e.Value = string.Format("{0}", e.ListSourceRowIndex+1);
            }
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
                        WriteToResponse("registerwithparticularsofchitdrawalsandprizemoneydisbursement", true, "pdf", stream);
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