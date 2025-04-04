using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxGridView;
using System.IO;
using System.Drawing;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrintingLinks;
using System.Data;
using DevExpress.Web.ASPxMenu;
using System.Globalization;
using SVCF_BusinessAccessLayer;
using SVCF_TransactionLayer;
using SVCF_DataAccessLayer;
namespace SreeVisalamChitFundLtd_phase1
{
    public partial class _26carrear : System.Web.UI.Page
    {
        #region Object
        BusinessLayer balayer = new BusinessLayer();
        TransactionLayer trn = new TransactionLayer();
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
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
            DataTable dt = balayer.GetDataTable("select * from groupmaster where BranchID="+Session["Branchid"]);
            DataTable dtM = new DataTable();
            dtM.Columns.Add("GROUPNO", typeof(string));
            dtM.Columns.Add("ChitValue", typeof(string));
            dtM.Columns.Add("PArrear", typeof(string));
            dtM.Columns.Add("NPArrear", typeof(string));
            dtM.Columns.Add("TArrear", typeof(string));
            dtM.Columns.Add("total", typeof(string));
            DataRow dr = dtM.NewRow();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dr["GROUPNO"] = dt.Rows[i]["GROUPNO"];
                dr["ChitValue"] = dt.Rows[i]["ChitValue"];
                DataTable dtSum = balayer.GetDataTable("select cast(digits(mg1.GrpMemberID) as unsigned) as ChitNo1, concat(mm.MemberID, ' | ', mg1.MemberName) as `MemberName`,sum(v1.Amount) , sum(case when (v1.Voucher_Type='C' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) -(case when (v1.Voucher_Type='D' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) as paidAmount,  (case when( tp1.PaymentDate is null ) then sum(case when (v1.Voucher_Type='C' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 )then v1.Amount else 0.00 end) -(case when (v1.Voucher_Type='D' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) else 0.00 end ) as Credit, (case when( tp1.PaymentDate is not null ) then sum((case when (v1.Voucher_Type='D' and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) -case when (v1.Voucher_Type='C' and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) else 0.00 end ) as Debit, (case when( tp1.PaymentDate is not null ) then sum((case when (v1.Voucher_Type='C' and v1.Other_Trans_Type=5 ) then v1.Amount else 0.00 end)) else 0.00 end ) as PKasar, (case when( tp1.PaymentDate is null ) then sum((case when (v1.Voucher_Type='C' and v1.Other_Trans_Type=5 ) then v1.Amount else 0.00 end)) else 0.00 end ) as NPKasar, (case when( (sum(case when (v1.Voucher_Type='C' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) -(case when (v1.Voucher_Type='D' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) -vgwd1.TotaldueAmount)>0.00) then (sum(case when (v1.Voucher_Type='C' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) -(case when (v1.Voucher_Type='D' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) -vgwd1.TotaldueAmount) else 0.00 end) as ExcessRemittance ,vgwd1.TotaldueAmount, (case when( tp1.PaymentDate is not null and vgwd1.IsFinished=0) then (case when( (vgwd1.TotaldueAmount-sum(case when (v1.Voucher_Type='C' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) -(case when (v1.Voucher_Type='D' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) )>0.00) then (vgwd1.TotaldueAmount-sum(case when (v1.Voucher_Type='C' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else  0.00 end) -(case when (v1.Voucher_Type='D' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) ) else 0.00 end) else 0.00 end ) as PArrier, (case when( tp1.PaymentDate is null and vgwd1.IsFinished=0) then (case when( (vgwd1.TotaldueAmount-sum(case when  (v1.Voucher_Type='C' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) -(case when (v1.Voucher_Type='D'  and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) )>0.00) then (vgwd1.TotaldueAmount-sum(case when  (v1.Voucher_Type='C' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) -(case when (v1.Voucher_Type='D'  and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) ) else 0.00 end) else 0.00 end ) as NPArrier, (case when( vgwd1.IsFinished=1) then (case when( (vgwd1.TotaldueAmount-sum(case when  (v1.Voucher_Type='C' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) -(case when (v1.Voucher_Type='D'  and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) )>0.00) then (vgwd1.TotaldueAmount-sum(case when  (v1.Voucher_Type='C' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) -(case when (v1.Voucher_Type='D' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) ) else 0.00 end) else 0.00 end ) as TerminatedArrier from membertogroupmaster as mg1 join voucher as v1 on v1.Head_Id=mg1.Head_Id join view_groupwisedue as vgwd1 on vgwd1.`GroupId`=" + dt.Rows[i]["Head_Id"] + "  left join trans_payment as tp1 on v1.Head_Id =tp1.TokenNumber join membermaster as mm on (mg1.MemberID=mm.MemberIDNew) where mg1.BranchID=" + Session["Branchid"] + " and mg1.GroupID=" + dt.Rows[i]["Head_Id"] + " and v1.Head_Id in (select NodeID from headstree where ParentID=" + dt.Rows[i]["Head_Id"] + " ) and v1.ChoosenDate<= '" + balayer.indiandateToMysqlDate(txtFromDate.Text) + "' group by v1.Head_Id order by cast(digits(mg1.GrpMemberID) as unsigned) ;");
                object sumObjectPArrear;
                sumObjectPArrear = dtSum.Compute("Sum(PArrier)", "");
                if (sumObjectPArrear != DBNull.Value)
                {
                    dr["PArrear"] = sumObjectPArrear;
                }
                else
                {
                    sumObjectPArrear = 0.00;
                    dr["PArrear"] = "0.00";
                }
                object sumObjectNPArrear;
                sumObjectNPArrear = dtSum.Compute("Sum(NPArrier)", "");
                if (sumObjectNPArrear != DBNull.Value)
                {
                    dr["NPArrear"] = sumObjectNPArrear;
                }
                else
                {
                    sumObjectNPArrear = 0.00;
                    dr["NPArrear"] = "0.00";
                }

                object sumObjectTArrear;
                sumObjectTArrear = dtSum.Compute("Sum(TerminatedArrier)", "");
                if (sumObjectTArrear != DBNull.Value)
                {
                    dr["TArrear"] = sumObjectTArrear;
                }
                else
                {
                    sumObjectTArrear = 0.00;
                    dr["TArrear"] = "0.00";
                }
                
                object sumObject;
                sumObject = Convert.ToDecimal( sumObjectPArrear )+ Convert.ToDecimal( sumObjectNPArrear) + Convert.ToDecimal( sumObjectTArrear);
                dr["total"] = sumObject;
                dtM.Rows.Add(dr.ItemArray);
            }
            grid.DataSource = dtM;
            grid.DataBind();
        }
        protected void BtnStatisticsGo_Click(object sender, EventArgs e)
        {
            select();
        }
        protected void ASPxGridView1_SummaryDisplayText(object sender, ASPxGridViewSummaryDisplayTextEventArgs e)
        {
            //CultureInfo hindi = new CultureInfo("hi-IN");
            //if (e.Item.FieldName == "PArrier")
            //    e.Text = string.Format(hindi, "{0:c}", Convert.ToDouble(e.Value)).Replace("रु", "").Replace(" ", "");
            //if (e.Item.FieldName == "NPKasar")
            //    e.Text = string.Format(hindi, "{0:c}", Convert.ToDouble(e.Value)).Replace("रु", "").Replace(" ", "");
            //if (e.Item.FieldName == "PKasar")
            //    e.Text = string.Format(hindi, "{0:c}", Convert.ToDouble(e.Value)).Replace("रु", "").Replace(" ", "");
            if (e.Item.FieldName == "ChitValue")
                e.Text = "Total";
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