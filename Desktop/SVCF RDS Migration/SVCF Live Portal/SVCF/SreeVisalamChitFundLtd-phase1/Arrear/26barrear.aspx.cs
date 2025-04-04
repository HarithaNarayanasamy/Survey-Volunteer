using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxGridView;
using System.Data;
using DevExpress.XtraPrinting;
using System.Drawing;
using System.IO;
using DevExpress.XtraPrintingLinks;
using DevExpress.Web.ASPxMenu;
using SVCF_BusinessAccessLayer;
using SVCF_DataAccessLayer;
namespace SreeVisalamChitFundLtd_phase1
{
    public partial class _26barrear : System.Web.UI.Page
    {
        #region Object
        BusinessLayer balayer = new BusinessLayer();
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataTable dt = balayer.GetDataTable("select GROUPNO,Head_Id from groupmaster where BranchID=" + Session["Branchid"] + "");
                DataRow dr = dt.NewRow();
                dr[0] = "--Select--";
                dr[1] = "0";
                ddlGroup.DataSource = dt;
                ddlGroup.DataTextField = "GROUPNO";
                ddlGroup.DataValueField = "Head_Id";
                dt.Rows.InsertAt(dr, 0);
                ddlGroup.DataBind();
                txtFromDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
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
        void select()
        {
            balayer.GetInsertItem("CREATE OR REPLACE VIEW `unpaidprizedmoney` AS select `voucher`.`Head_Id` AS `Head_Id`, `membertogroupmaster`.`GrpMemberID` AS `GrpMemberID`, (case when (`voucher`.`Voucher_Type` = 'C') then sum(`voucher`.`Amount`) else 0.00 end) AS `Credit`, (case when (`voucher`.`Voucher_Type` = 'D') then sum(`voucher`.`Amount`) else 0.00 end) AS `Debit`, ((case when (`voucher`.`Voucher_Type` = 'C') then sum(`voucher`.`Amount`) else 0.00 end) - (case when (`voucher`.`Voucher_Type` = 'D') then sum(`voucher`.`Amount`) else 0.00 end)) AS `AmountActuallyremittedbytheParty` from (`voucher` join `membertogroupmaster` ON ((`voucher`.`Head_Id` = `membertogroupmaster`.`Head_Id`))) group by `voucher`.`Head_Id`");
            balayer.GetInsertItem("create or replace view `view_groupwisedue` as select `groupmaster`.`GROUPNO` AS `GroupIDOriginal`,`groupmaster`.`IsFinished`, `auctiondetails`.`GroupID` AS `GroupId`, sum(`auctiondetails`.`CurrentDueAmount`) AS `TotaldueAmount` from (`auctiondetails` join `groupmaster` ON ((`auctiondetails`.`GroupID` = `groupmaster`.`Head_Id`))) where (`auctiondetails`.`AuctionDate` <= '" + balayer.indiandateToMysqlDate(txtFromDate.Text) + "') group by `auctiondetails`.`GroupID`");
            DataTable dt = balayer.GetDataTable("SELECT * FROM svcf.auctiondetails where GroupID="+ddlGroup.SelectedItem.Value+" and PrizedMemberID is not null order by DrawNO desc");
            DataTable dtM = new DataTable();
            
            dtM.Columns.Add("GrpMemberId", typeof(string));
            dtM.Columns.Add("Name", typeof(string));
            dtM.Columns.Add("NoofArrears", typeof(string));
            dtM.Columns.Add("ArrearAmount", typeof(string));
            dtM.Columns.Add("status", typeof(string));
            DataRow dr = dtM.NewRow();
            
                
            DataTable dtSum = balayer.GetDataTable("select mg1.GrpMemberId, concat( mm.memberID,' | ',mm.CustomerName) as Name, (case when( vgwd1.IsFinished=1 ) then 'Terminated' else (case when (tp1.PaymentDate is not null) then 'Prized' else 'NPrized' end) end) as status,(case when (vgwd1.IsFinished=1) then (case when( (vgwd1.TotaldueAmount-sum(case when (v1.Voucher_Type='C' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) -(case when (v1.Voucher_Type='D' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) )>0.00) then (vgwd1.TotaldueAmount-sum(case when (v1.Voucher_Type='C' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) -(case when (v1.Voucher_Type='D' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) ) else 0.00 end) else ((case when (tp1.PaymentDate is not null and vgwd1.IsFinished=0) then (case when ( (vgwd1.TotaldueAmount-sum(case when (v1.Voucher_Type='C' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) -(case when (v1.Voucher_Type='D' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) )> 0.00) then (vgwd1.TotaldueAmount-sum(case when (v1.Voucher_Type='C' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 )  then v1.Amount else 0.00 end) -(case when (v1.Voucher_Type='D' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then  v1.Amount else 0.00 end) ) else 0.00 end)  else   (case when( (vgwd1.TotaldueAmount-sum(case when (v1.Voucher_Type='C' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) -(case when (v1.Voucher_Type='D' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) )>0.00) then (vgwd1.TotaldueAmount-sum(case when (v1.Voucher_Type='C' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) -(case when (v1.Voucher_Type='D' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) ) else 0.00 end) end)) end) as ArrearAmount from membertogroupmaster as mg1 join voucher as v1 on v1.Head_Id=mg1.Head_Id join  view_groupwisedue as vgwd1 on vgwd1.`GroupId`="+ddlGroup.SelectedItem.Value+" left join trans_payment as tp1 on v1.Head_Id =tp1.TokenNumber join membermaster  as mm on (mg1.MemberID=mm.MemberIDNew) where mg1.BranchID="+Session["Branchid"]+" and mg1.GroupID="+ddlGroup.SelectedItem.Value+" and v1.Head_Id in (select NodeID from  headstree where ParentID="+ddlGroup.SelectedItem.Value+" ) and v1.ChoosenDate<= '"+balayer.indiandateToMysqlDate(txtFromDate.Text)+"' group by v1.Head_Id order by cast(digits(mg1.GrpMemberID) as unsigned) ; ");
            for (int i = 0; i < dtSum.Rows.Count; i++)
            {

                if (balayer.ToobjectstrEvenNull(dtSum.Rows[i]["ArrearAmount"]) != "0.00")
                {
                    dr["GrpMemberId"] = dtSum.Rows[i]["GrpMemberId"];
                    dr["Name"] = dtSum.Rows[i]["Name"];
                    DataTable dtt = balayer.GetDataTable("SELECT * FROM svcf.auctiondetails where GroupID="+ddlGroup.SelectedItem.Value+" and PrizedMemberID is not null order by DrawNO desc");
                    decimal amt = Convert.ToDecimal(dtSum.Rows[i]["ArrearAmount"]);
                    int count = 1;
                    for (int j = 0; j < dtt.Rows.Count; j++)
                    {
                        if (amt > Convert.ToDecimal(dtt.Rows[j]["CurrentDueAmount"]))
                        {
                            count += 1;
                            amt -= Convert.ToDecimal(dtt.Rows[j]["CurrentDueAmount"]);
                        }
                        else
                        {
                            dr["NoofArrears"] = count;
                            break;
                        }
                    }
                        //dr["NoofArrears"] = "";
                    dr["ArrearAmount"] = dtSum.Rows[i]["ArrearAmount"];
                    dr["status"] = dtSum.Rows[i]["status"];
                    dtM.Rows.Add(dr.ItemArray);
                }
            }
            grid.DataSource = dtM;
            grid.DataBind();
        }
        protected void BtnStatisticsGo_Click(object sender, EventArgs e)
        {
            select();
        }
        private System.Drawing.Image headerImage;
        protected void Export_click(object sender, MenuItemEventArgs e)
        {
            using (this.headerImage = System.Drawing.Image.FromFile(Server.MapPath("~\\Images\\visalam.png")))
            {
                if (e.Item.Text.ToString() == "PDF")
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
                        compositeLink.PaperKind = System.Drawing.Printing.PaperKind.A3;
                        //compositeLink.Landscape = true;
                        compositeLink.CreateDocument(false);
                        compositeLink.PrintingSystem.Document.AutoFitToPagesWidth = 1;
                        compositeLink.PrintingSystem.ExportToPdf(stream);
                        WriteToResponse("TrialandArrear", true, "pdf", stream);
                    }
                }

                else if (e.Item.Text.ToString() == "XLSX")
                {
                    gridExport.WriteXlsxToResponse();
                }
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