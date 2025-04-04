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
using System.IO;
using DevExpress.XtraPrintingLinks;
using DevExpress.XtraPrinting;
using System.Drawing;
using System.Globalization;
using System.Text.RegularExpressions;
using DevExpress.XtraReports.UI;
using DevExpress.XtraPrinting.Shape;
using DevExpress.Web.ASPxEditors;
using SVCF_BusinessAccessLayer;
using SVCF_TransactionLayer;
using SVCF_DataAccessLayer;

namespace SreeVisalamChitFundLtd_phase1
{
    public partial class TerminatedTrial : System.Web.UI.Page
    {
        private System.Drawing.Image headerImage;
        //#region VarDeclaration
       // CommonClassFile objcls = new CommonClassFile();
        //#endregion

        #region VarDeclaration
        BusinessLayer balayer = new BusinessLayer();
        TransactionLayer tranlayer = new TransactionLayer();
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetGroupMember();
                ddlChit.SelectedItem.Value = "0";
                txtFromDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                select();
            }
            select();
        }
        public void GetGroupMember()
        {
            ddlChit.DataSource = null;
            ddlChit.DataBind();
            string data = "SELECT `GROUPNO`,`Head_Id` FROM `svcf`.`groupmaster` where `IsFinished`=0 and `BranchID`=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + "";
            DataTable dtChitGrp = balayer.GetDataTable(data);
            ddlChit.DataSource = dtChitGrp;
            DataRow dr = dtChitGrp.NewRow();
            dr[0] = "--select--";
            dr[1] = "0";
            ddlChit.DataTextField = "GROUPNO";
            ddlChit.DataValueField = "Head_Id";
            dtChitGrp.Rows.InsertAt(dr, 0);
            ddlChit.DataBind();
        }
        protected void select()
        {
            DataTable dtBind = new DataTable();
            dtBind.Columns.Add("ChitNo1", typeof(int));
            dtBind.Columns.Add("MemberName");
            dtBind.Columns.Add("ExcessRemittance", typeof(decimal));
            dtBind.Columns.Add("PArrier", typeof(decimal));
            dtBind.Columns.Add("Branches");
            DataRow drBind = dtBind.NewRow();

            balayer.GetInsertItem("CREATE OR REPLACE VIEW `unpaidprizedmoney` AS select `voucher`.`Head_Id` AS `Head_Id`, `membertogroupmaster`.`GrpMemberID` AS `GrpMemberID`, (case when (`voucher`.`Voucher_Type` = 'C') then sum(`voucher`.`Amount`) else 0.00 end) AS `Credit`, (case when (`voucher`.`Voucher_Type` = 'D') then sum(`voucher`.`Amount`) else 0.00 end) AS `Debit`, ((case when (`voucher`.`Voucher_Type` = 'C') then sum(`voucher`.`Amount`) else 0.00 end) - (case when (`voucher`.`Voucher_Type` = 'D') then sum(`voucher`.`Amount`) else 0.00 end)) AS `AmountActuallyremittedbytheParty` from (`voucher` join `membertogroupmaster` ON ((`voucher`.`Head_Id` = `membertogroupmaster`.`Head_Id`))) group by `voucher`.`Head_Id`");
            balayer.GetInsertItem("create or replace view `view_groupwisedue` as select `groupmaster`.`GROUPNO` AS `GroupIDOriginal`,`groupmaster`.`IsFinished`, `auctiondetails`.`GroupID` AS `GroupId`, sum(`auctiondetails`.`CurrentDueAmount`) AS `TotaldueAmount` from (`auctiondetails` join `groupmaster` ON ((`auctiondetails`.`GroupID` = `groupmaster`.`Head_Id`))) where (`auctiondetails`.`AuctionDate` <= '" + balayer.indiandateToMysqlDate(txtFromDate.Text) + "') group by `auctiondetails`.`GroupID`");
            if (ddlChit.SelectedItem.Text == "--select--")
            {
                grid.SettingsText.Title = "Trial And Arrear";
                grid.DataSource = dtBind;
            }
            else
            {
                DataTable dtInit = balayer.GetDataTable("select * from auctiondetails where IsPrized='Y' and AuctionDate<='" + balayer.indiandateToMysqlDate(txtFromDate.Text) + "' and GroupID=" + ddlChit.SelectedItem.Value);
                if (dtInit.Rows.Count > 0)
                {
                    DataTable dtHeads = balayer.GetDataTable(" select NodeID from headstree where ParentID=" + ddlChit.SelectedItem.Value);
                    for (int j = 0; j < dtHeads.Rows.Count; j++)
                    {
                        decimal strA = 0.00M;
                        decimal strE = 0.00M;
                        string sss = balayer.GetSingleValue(@"select cast(digits(mg1.GrpMemberID) as unsigned) as ChitNo1 from membertogroupmaster as mg1 join voucher as v1 on v1.Head_Id=mg1.Head_Id join view_groupwisedue as vgwd1 on vgwd1.`GroupId`=" + ddlChit.SelectedItem.Value + " left join trans_payment as tp1 on v1.Head_Id =tp1.TokenNumber join membermaster as mm on (mg1.MemberID=mm.MemberIDNew) where mg1.BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and mg1.GroupID=" + ddlChit.SelectedItem.Value + " and v1.Head_Id=" + dtHeads.Rows[j]["NodeID"] + " and v1.ChoosenDate<= '" + balayer.indiandateToMysqlDate(txtFromDate.Text) + "' group by v1.Head_Id order by cast(digits(mg1.GrpMemberID) as unsigned),v1.Voucher_Type DESC ;");
                        if (string.IsNullOrEmpty(sss))
                        {
                            drBind["ChitNo1"] =balayer.GetSingleValue("select cast(digits(GrpMemberID) as unsigned) as ChitNo1 from membertogroupmaster where Head_Id=" + dtHeads.Rows[j]["NodeID"]);
                        }
                        else
                        {
                            drBind["ChitNo1"] = sss;
                        }
                        string strName = balayer.GetSingleValue(@"select concat(mg1.MemberName) as `MemberName` from membertogroupmaster as mg1 join voucher as v1 on v1.Head_Id=mg1.Head_Id join view_groupwisedue as vgwd1 on vgwd1.`GroupId`=" + ddlChit.SelectedItem.Value + " left join trans_payment as tp1 on v1.Head_Id =tp1.TokenNumber join membermaster as mm on (mg1.MemberID=mm.MemberIDNew) where mg1.BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and mg1.GroupID=" + ddlChit.SelectedItem.Value + " and v1.Head_Id=" + dtHeads.Rows[j]["NodeID"] + " and v1.ChoosenDate<= '" + balayer.indiandateToMysqlDate(txtFromDate.Text) + "' group by v1.Head_Id order by v1.ChoosenDate DESC ;");
                        if (string.IsNullOrEmpty(strName))
                        {
                            string strMemID =balayer.GetSingleValue("SELECT MemberID FROM svcf.membertogroupmaster where Head_id=" + dtHeads.Rows[j]["NodeID"]);
                            drBind["MemberName"] =balayer.GetSingleValue("SELECT CustomerName FROM svcf.membermaster where MemberIDNew=" + strMemID);
                        }
                        else
                        {
                            drBind["MemberName"] = strName;
                        }
                        string excess = balayer.GetSingleValue(@"select  (case when( (sum(case when (v1.Voucher_Type='C' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) -sum(case when (v1.Voucher_Type='D' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) -vgwd1.TotaldueAmount)>0.00) then (sum(case when (v1.Voucher_Type='C' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) -sum(case when (v1.Voucher_Type='D' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) -vgwd1.TotaldueAmount) else 0.00 end) as ExcessRemittance from membertogroupmaster as mg1 join voucher as v1 on v1.Head_Id=mg1.Head_Id join view_groupwisedue as vgwd1 on vgwd1.`GroupId`=" + ddlChit.SelectedItem.Value + " left join trans_payment as tp1 on v1.Head_Id =tp1.TokenNumber join membermaster as mm on (mg1.MemberID=mm.MemberIDNew) where mg1.BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and mg1.GroupID=" + ddlChit.SelectedItem.Value + " and v1.Head_Id=" + dtHeads.Rows[j]["NodeID"] + " and v1.ChoosenDate<= '" + balayer.indiandateToMysqlDate(txtFromDate.Text) + "' group by v1.Head_Id order by cast(digits(mg1.GrpMemberID) as unsigned),v1.Voucher_Type DESC ;");
                        if (string.IsNullOrEmpty(excess))
                        {
                            strE = 0.00M;
                            drBind["ExcessRemittance"] = "0.00";
                        }
                        else
                        {
                            strE = Convert.ToDecimal( excess);
                            drBind["ExcessRemittance"] = excess;
                        }
                        string parr = balayer.GetSingleValue(@"select (case when( tp1.PaymentDate<='" + balayer.indiandateToMysqlDate(txtFromDate.Text) + "' ) then (case when( (vgwd1.TotaldueAmount-sum(case when (v1.Voucher_Type='C' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) +sum(case when (v1.Voucher_Type='D' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) )>0.00) then (vgwd1.TotaldueAmount-sum(case when (v1.Voucher_Type='C' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) +sum(case when (v1.Voucher_Type='D' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) ) else 0.00 end) else 0.00 end ) as PArrier from membertogroupmaster as mg1 join voucher as v1 on v1.Head_Id=mg1.Head_Id join view_groupwisedue as vgwd1 on vgwd1.`GroupId`=" + ddlChit.SelectedItem.Value + " left join trans_payment as tp1 on v1.Head_Id =tp1.TokenNumber join membermaster as mm on (mg1.MemberID=mm.MemberIDNew) where mg1.BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and mg1.GroupID=" + ddlChit.SelectedItem.Value + " and v1.Head_Id=" + dtHeads.Rows[j]["NodeID"] + " and v1.ChoosenDate<= '" + balayer.indiandateToMysqlDate(txtFromDate.Text) + "' group by v1.Head_Id order by cast(digits(mg1.GrpMemberID) as unsigned),v1.Voucher_Type DESC ;");
                        if (string.IsNullOrEmpty(excess))
                        {
                            strA = 0.00M;
                            drBind["PArrier"] = "0.00";
                        }
                        else
                        {
                            strA = Convert.ToDecimal(parr);
                            drBind["PArrier"] = parr;
                        }
                        string strMember =balayer.GetSingleValue("SELECT MemberID FROM svcf.voucher where Head_id=" + dtHeads.Rows[j]["NodeID"] + " and ChoosenDate<='" + balayer.indiandateToMysqlDate(txtFromDate.Text) + "' order by ChoosenDate desc");
                        if (string.IsNullOrEmpty(strMember))
                        {
                            strMember = "0";
                        }
                        string strBranches = balayer.GetSingleValue("SELECT b1.Place FROM svcf.membermaster as m1 join branchdetails as b1 on (m1.BranchID=b1.Head_Id) where m1.MemberIDNew=" + strMember);
                        if (string.IsNullOrEmpty(strBranches))
                        {
                            drBind["Branches"] = balayer.GetSingleValue("SELECT b1.Place FROM svcf.membertogroupmaster as m1 join branchdetails as b1 on (m1.B_Id=b1.Head_Id) where m1.Head_id=" + dtHeads.Rows[j]["NodeID"]);
                        }
                        else
                        {
                            drBind["Branches"] = strBranches;
                        }

                        //string strBranches = balayer.GetSingleValue("SELECT b1.Place FROM svcf.membermaster as m1 join svcf.branchdetails as b1 on (m1.BranchID=b1.Head_Id) join svcf.membertogroupmaster as mg1 on mg1.MemberID=m1.MemberIDNew where mg1.Head_Id=" + strMember);
                        //if (string.IsNullOrEmpty(strBranches))
                        //{
                        //    drBind["Branches"] = balayer.GetSingleValue("SELECT b1.Place FROM svcf.membertogroupmaster as m1 join branchdetails as b1 on (m1.B_Id=b1.Head_Id) where m1.Head_id=" + dtHeads.Rows[j]["NodeID"]);
                        //}
                        //else
                        //{
                        //    drBind["Branches"] = strBranches;
                        //}


                        if (strA != 0.00M || strE != 0.00M)
                        {
                            dtBind.Rows.Add(drBind.ItemArray);
                        }
                        
                    }
                  
                   
                    
                        ViewState["ExcessRemittance"] = dtBind.Compute("Sum(ExcessRemittance)", "");
                        ViewState["PArrier"] = dtBind.Compute("Sum(PArrier)", "");
                    
                    }
                DataTable sssssssss = balayer.GetDataTable("SELECT `groupmaster`.`NoofMembers`,`groupmaster`.`GROUPNO`,`branchdetails`.`B_Name`,max(`auctiondetails`.`DrawNO`) as `RunningCall`,`groupmaster`.`ChitValue` FROM svcf.groupmaster join auctiondetails on (`groupmaster`.`Head_Id`=`auctiondetails`.`GroupID`) join `branchdetails` on (`groupmaster`.`BranchID`=`branchdetails`.`Head_Id`) where `groupmaster`.`Head_Id`=" + ddlChit.SelectedValue + " and `auctiondetails`.`AuctionDate`<='" + balayer.indiandateToMysqlDate(txtFromDate.Text) + "'  and `auctiondetails`.`BranchID`=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + ";");
                string str = Convert.ToString(sssssssss.Rows[0]["RunningCall"]) == "" ? "0" : Convert.ToString(sssssssss.Rows[0]["RunningCall"]);
                grid.SettingsText.Title = "Sree Visalam Chit Fund Limited.,<br/>Terminated And Arrear <br/> Branch Name : " + sssssssss.Rows[0]["B_Name"].ToString() + "; \t Running Call : " + str + " <br/> Group No : " + sssssssss.Rows[0]["GROUPNO"].ToString() + "; \t Chit Amount : " + sssssssss.Rows[0]["ChitValue"].ToString() + "; \t for the month of " + Convert.ToDateTime(txtFromDate.Text).ToString("MMMM yyyy");
                grid.Settings.ShowTitlePanel = true;
                DataView dv = dtBind.DefaultView;
                dv.Sort = "ChitNo1 asc";
                DataTable sortedDT = dv.ToTable();
                grid.DataSource = sortedDT;
            }
            grid.DataBind();
        }
        protected void BtnStatisticsGo_Click(object sender, EventArgs e)
        {
            select();
        }
        protected void lbBalanceCR_Load(object sender, EventArgs e)
        {
            Label label = (Label)sender;
            if (ViewState["ExcessRemittance"] != DBNull.Value)
                label.Text = Math.Abs((Convert.ToDecimal(ViewState["ExcessRemittance"])) - Convert.ToDecimal(ViewState["PArrier"])).ToString();
        }
        protected void lbBalanceText_Load(object sender, EventArgs e)
        {
            Label label = (Label)sender;
            //DataTable  dt = (DataTable)ViewState["ExcessRemittance"];
            //int count = dt.Rows.Count;


            if (ViewState["ExcessRemittance"] != DBNull.Value)
            {
                decimal dddd = (Convert.ToDecimal(ViewState["ExcessRemittance"])) - Convert.ToDecimal(ViewState["PArrier"]);
                if (dddd < 0.00M)
                {
                    label.Text = "Balance DR";
                }
                else
                {
                    label.Text = "Balance CR";
                }
            }
            else
            {

            }
        }
        protected void lbDebitTotal_Load(object sender, EventArgs e)
        {
            Label label = (Label)sender;
            if (ViewState["ExcessRemittance"] != DBNull.Value)
                label.Text = Convert.ToString(ViewState["PArrier"]);
        }
        protected void lbCredit_Load(object sender, EventArgs e)
        {
            Label label = (Label)sender;
            if (ViewState["ExcessRemittance"] != DBNull.Value)
                label.Text = Convert.ToString(ViewState["ExcessRemittance"]);
        }
        protected void lbGrantTotal_Load(object sender, EventArgs e)
        {
            Label label = (Label)sender;
            if (ViewState["ExcessRemittance"] != DBNull.Value)
                label.Text = Convert.ToString((Convert.ToDecimal(ViewState["ExcessRemittance"])));
        }

        protected void Export_click(object sender, MenuItemEventArgs e)
        {
            using (this.headerImage = System.Drawing.Image.FromFile(Server.MapPath("~\\Images\\visalam.png")))
            {
                if (e.Item.Text.ToString() == "PDF")
                {
                    if (ddlChit.SelectedItem.Value != "--select--")
                    {
                        DataTable sssssssss = balayer.GetDataTable("SELECT `groupmaster`.`GROUPNO`,`branchdetails`.`B_Name`,max(`auctiondetails`.`DrawNO`) as `RunningCall`,`groupmaster`.`ChitValue` FROM svcf.groupmaster join auctiondetails on (`groupmaster`.`Head_Id`=`auctiondetails`.`GroupID`) join `branchdetails` on (`groupmaster`.`BranchID`=`branchdetails`.`Head_Id`) where `groupmaster`.`Head_Id`=" + ddlChit.SelectedValue + " and `auctiondetails`.`AuctionDate`<='" + balayer.indiandateToMysqlDate(txtFromDate.Text) + "'  and `auctiondetails`.`BranchID`=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + ";");
                      //  grid.SettingsText.Title = "Terminated And Arrear \n\r Branch Name : " + sssssssss.Rows[0]["B_Name"].ToString() + " \t Running Call : " + sssssssss.Rows[0]["RunningCall"].ToString() + " \n\r Group No : " + sssssssss.Rows[0]["GROUPNO"].ToString() + " \t Chit Amount : " + sssssssss.Rows[0]["ChitValue"].ToString();
                        grid.SettingsText.Title = "Terminated Arrears for the Group" + sssssssss.Rows[0]["GROUPNO"].ToString() + "\n\r Chit Value: " + sssssssss.Rows[0]["ChitValue"].ToString() + " as on" +  (txtFromDate.Text);
                        grid.Settings.ShowTitlePanel = true;
                    }
                    else
                    {
                        grid.SettingsText.Title = "Trial And Arrear";
                        grid.Settings.ShowTitlePanel = true;
                    }

                    gridExport.Styles.Header.HorizontalAlign = HorizontalAlign.Center;
                    gridExport.Styles.Header.VerticalAlign = VerticalAlign.Middle;
                    gridExport.Styles.Header.Wrap = DevExpress.Utils.DefaultBoolean.True;
                    gridExport.Styles.Footer.HorizontalAlign = HorizontalAlign.Right;
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
                        compositeLink.PaperKind = System.Drawing.Printing.PaperKind.Legal;
                        //compositeLink.Landscape = true;
                        compositeLink.CreateDocument(false);
                        compositeLink.PrintingSystem.Document.AutoFitToPagesWidth = 1;
                        compositeLink.PrintingSystem.ExportToPdf(stream);
                        WriteToResponse("Terminated and Arrear", true, "pdf", stream);
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
            tb1.Font = new Font("Arial", 8, FontStyle.Bold);
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
