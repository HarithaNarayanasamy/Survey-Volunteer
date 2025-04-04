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
    public partial class subscriberbook : System.Web.UI.Page
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
                DataTable dt = balayer.GetDataTable("select Head_Id,GROUPNO from groupmaster where BranchId="+Session["Branchid"]+" and IsFinished=0");
                //ddlGroupNumber.AppendDataBoundItems = true;
                ddlGroupNumber.DataSource = dt;
                DataRow dr = dt.NewRow();
                dr[0] = "0";
                dr[1] = "--Select--";
                ddlGroupNumber.DataTextField = "GROUPNO";
                ddlGroupNumber.DataValueField = "Head_Id";
                dt.Rows.InsertAt(dr, 0);
                ddlGroupNumber.DataBind();
                foreach (GridViewColumn column in gridSubscribed.Columns)
                {
                    if (column is GridViewDataColumn)
                    {
                        ((GridViewDataColumn)column).Settings.AutoFilterCondition = AutoFilterCondition.Contains;
                    }
                }
                foreach (GridViewColumn column in gridAssigned.Columns)
                {
                    if (column is GridViewDataColumn)
                    {
                        ((GridViewDataColumn)column).Settings.AutoFilterCondition = AutoFilterCondition.Contains;
                    }
                }
                foreach (GridViewColumn column in gridSubstitued.Columns)
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
            dsSubscribed.ConnectionString = CommonClassFile.ConnectionString;
            dsSubscribed.SelectCommand = @"SELECT distinct m2.GrpMemberID,m3.MemberID,m2.MemberName, m2.MemberAddress,date_format(m1.assigneddate,'%d/%m/%Y') as Dateofsigningthechitagreement,date_format(m1.receiptdate,'%d/%m/%Y') as dateofreceiptofcopyofthechitagreement,'1' as NoofTickets,g1.ChitValue as Amount FROM svcf.membersuggestion as m1 right join membertogroupmaster as m2 on (m1.GroupNo=m2.GroupID ) join membermaster as m3 on (m2.MemberID=m3.MemberIDNew) join groupmaster as g1 on (m1.GroupNo=g1.Head_Id) where m1.GroupNo=" + ddlGroupNumber.SelectedItem.Value + " and m1.NoOfAssignedToken<>0 and g1.Branchid=" + Session["Branchid"];
            gridSubscribed.DataBind();
            dsAssigned.ConnectionString = CommonClassFile.ConnectionString;
            dsAssigned.SelectCommand = @"SELECT m1.GrpMemberID, concat(m2.MemberID,' | ', m2.CustomerName,' | ',m2.ResidentialAddress) as  OldMemberDetails,concat(m3.MemberID,' | ', m3.CustomerName,' | ',m3.ResidentialAddress) as  NewMemberDetails,date_format(t1.assigndate,'%d/%m/%Y') as assigndate,'1' as NoofTickets,g1.ChitValue,date_format(t1.foremandate,'%d/%m/%Y') as foremandate FROM svcf.transfer_approval as t1 join membertogroupmaster as m1 on (t1.GrpMemberID=m1.Head_Id) join membermaster as m2 on (t1.Old_Member=m2.MemberIDNew) join membermaster as m3 on (t1.New_Member=m3.MemberIDNew) join groupmaster as g1 on (t1.ChitGroup=g1.Head_Id) where g1.Head_Id="+ddlGroupNumber.SelectedItem.Value+" and g1.Branchid=" + Session["Branchid"];
            gridAssigned.DataBind();
            dsSubstituted.ConnectionString = CommonClassFile.ConnectionString;
            dsSubstituted.SelectCommand = @"SELECT distinct r1.SNo, m2.GrpMemberId, concat(m3.MemberID,' | ',m3.CustomerName ,' | ',m3.ResidentialAddress) as OldMember,concat(m4.MemberID,' | ',m4.CustomerName ,' | ',m4.ResidentialAddress) as NewMember,r1.ReasonForRemoval,date_format(r1.DateOfRemoval,'%d/%m/%Y') as RemovalDate,date_format(r1.substitutedate,'%d/%m/%Y') as substitutedate,'1' as Nooftickets,g1.ChitValue,date_format(r1.intimationdate,'%d/%m/%Y') as intimationdate FROM svcf.removal_approval as r1 join membertogroupmaster as m2 on (r1.GroupMemberID=m2.Head_Id) join membermaster as m3 on (r1.OldMemberID=m3.MemberIDNew) join membermaster as m4 on (r1.OldMemberID=m4.MemberIDNew) join groupmaster as g1 on (r1.ChitNO=g1.Head_Id) where g1.Head_Id="+ddlGroupNumber.SelectedItem.Value+" and g1.Branchid="+Session["Branchid"];
            gridSubstitued.DataBind();
        }
        protected void Export_click(object sender, MenuItemEventArgs e)
        {
            if (e.Item.Text.ToString() == "PDF")
            {
                using (this.headerImage = System.Drawing.Image.FromFile(Server.MapPath("~\\Images\\visalam.png")))
                {
                    PrintingSystem ps = new PrintingSystem();
                    PrintableComponentLink gridSubscribed = new PrintableComponentLink(ps);
                    gridSubscribed.Component = gridSubscribedExport;

                    PrintableComponentLink gridAssigned = new PrintableComponentLink(ps);
                    gridAssigned.Component = gridAssignedExport;

                    PrintableComponentLink gridSubstitued = new PrintableComponentLink(ps);
                    gridSubstitued.Component = gridSubstituedExport;

                    Link header = new Link();
                    CompositeLink compositeLink = new CompositeLink(ps);
                    header.CreateDetailArea += new CreateAreaEventHandler(header_CreateDetailArea);
                    compositeLink.Links.AddRange(new object[] { header, gridSubscribed, gridAssigned, gridSubstitued });
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
                        WriteToResponse("subscriberbook", true, "pdf", stream);
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