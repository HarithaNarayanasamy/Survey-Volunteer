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
using DevExpress.Web.ASPxMenu;
using DevExpress.Data;
using System.IO;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrintingLinks;
using SVCF_BusinessAccessLayer;
using SVCF_TransactionLayer;
using SVCF_DataAccessLayer;

namespace SreeVisalamChitFundLtd_phase1
{
    public partial class ChitPaymentRegister : System.Web.UI.Page
    {
        #region VarDeclaration
        BusinessLayer balayer = new BusinessLayer();
        TransactionLayer trn = new TransactionLayer();
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            select();
            if (!IsPostBack)
            {
                //chkBox.Checked = true;
                GetGroupMember();
                txtFromDate.Text = "31/03/2013";
                txtToDate.Text = "31/05/2013";
                select();
                grid.DataBind();
                //ApplyLayout(0);
                foreach (GridViewColumn column in grid.Columns)
                {
                    if (column is GridViewDataColumn)
                    {
                        ((GridViewDataColumn)column).Settings.AutoFilterCondition = AutoFilterCondition.Contains;
                    }
                }
            }
        }
        protected virtual string GetLabelText(GridViewGroupRowTemplateContainer container)
        {
            return container.GroupText;
        }
        public void GetGroupMember()
        {
            ddlChit.DataSource = null;
            ddlChit.DataBind();
            DataTable dtChitGrp = balayer.GetDataTable("SELECT distinct `groupmaster`.`GROUPNO`,`voucher`.`ChitGroupId` FROM `svcf`.`voucher` join `svcf`.`groupmaster` on (`groupmaster`.`Head_Id`=`voucher`.`ChitGroupId`) where `voucher`.`Trans_Type`=2 and `voucher`.`BranchID`=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + "");
            for (int iR = 0; iR < dtChitGrp.Rows.Count; iR++)
            {
                dtChitGrp.Rows[iR][0] = dtChitGrp.Rows[iR][0].ToString();
            }
            ddlChit.DataSource = dtChitGrp;
            ddlChit.DataTextField = "GROUPNO";
            ddlChit.DataValueField = "ChitGroupId";
            ddlChit.DataBind();
            ListItem li = new ListItem("--select--", "--select--");
            ddlChit.Items.Insert(0, li);
            ListItem ll = new ListItem("All", "All");
            ddlChit.Items.Insert(1, ll);
        }
        protected void select()
        {
            if (ddlChit.SelectedValue == "--select--")
            {
                AccessDataSource1.ConnectionString = CommonClassFile.ConnectionString;
                AccessDataSource1.SelectCommand = @"select `ht1`.`Node` AS `ChitGroup`, `ht2`.`Node` AS `ChitNo`, `tp1`.`DrawNo` AS `InstNo`, `tp1`.`AuctionDate` AS `DateOfAuction`, `tp1`.`ChitAmount` AS `ChitAmount`, `m1`.`MemberName` AS `NameOfThePrizedSubscriber`, `tp1`.`PrizedAmount` AS `PrizedMoney`, `tp1`.`PaymentApplyedOn` AS `FormSentForApprovalOn`, `tp1`.`PaymentDate` AS `DateOfPayment`, concat(`tp1`.`AOSanctionNo`, '/', date_format(`tp1`.`ApprovedOn`,'%d-%b-%y')) AS `AOSanctionNoandDate`, `tp1`.`GuarantorName` AS `GuarantorName`, `tp1`.`NextDrawNo` AS `NextInstalmentNo`, `tp1`.`NextDueAmount` AS `NextInstalmentAmount`, concat(`gm1`.`ChitAgreementNo`, '/', `gm1`.`ChitAgreementYear`) AS `CANo`, `tp1`.`ChitGroupID` AS `ChitGroupID`, `v1`.`Voucher_No` AS `Voucher_No`, `m1`.`MemberAddress` AS `MemberAddress`, `ht3`.`Node` AS `BranchName`, `tb1`.`CustomersBankName` AS `CustomersBankName`, `tp1`.`CurrentDueAmount` AS `CurrentDueAmount` from (((((((`trans_payment` `tp1` join `headstree` `ht1` ON ((`tp1`.`ChitGroupID` = `ht1`.`NodeID`))) join `voucher` `v1` ON ((`tp1`.`TransactionKey` = `v1`.`TransactionKey`))) join `headstree` `ht2` ON ((`tp1`.`TokenNumber` = `ht2`.`NodeID`))) join `headstree` `ht3` ON ((`tp1`.`BranchID` = `ht3`.`NodeID`))) join `transbank` `tb1` ON ((`tp1`.`TransactionKey_Bank` = `tb1`.`TransactionKey`))) join `membertogroupmaster` `m1` ON ((`tp1`.`TokenNumber` = `m1`.`Head_Id`))) join `groupmaster` `gm1` ON ((`tp1`.`ChitGroupID` = `gm1`.`Head_Id`))) where ((`tp1`.`BranchID` = " + Session["Branchid"] + ") and (`tp1`.`ChitGroupID` = 0) and (`tp1`.`PaymentDate` between '" + balayer.indiandateToMysqlDate(txtFromDate.Text) + "' and '" + balayer.indiandateToMysqlDate(txtToDate.Text) + "'))";
            }
            else if (ddlChit.SelectedValue == "All")
            {
                grid.SettingsPager.Mode = GridViewPagerMode.ShowPager;
                grid.SettingsPager.Position = PagerPosition.TopAndBottom;
                AccessDataSource1.ConnectionString = CommonClassFile.ConnectionString;
                AccessDataSource1.SelectCommand = @"select `ht1`.`Node` AS `ChitGroup`, `ht2`.`Node` AS `ChitNo`, `tp1`.`DrawNo` AS `InstNo`, `tp1`.`AuctionDate` AS `DateOfAuction`, `tp1`.`ChitAmount` AS `ChitAmount`, `m1`.`MemberName` AS `NameOfThePrizedSubscriber`, `tp1`.`PrizedAmount` AS `PrizedMoney`, `tp1`.`PaymentApplyedOn` AS `FormSentForApprovalOn`, `tp1`.`PaymentDate` AS `DateOfPayment`, concat(`tp1`.`AOSanctionNo`, '/', date_format(`tp1`.`ApprovedOn`,'%d-%b-%y')) AS `AOSanctionNoandDate`, `tp1`.`GuarantorName` AS `GuarantorName`, `tp1`.`NextDrawNo` AS `NextInstalmentNo`, `tp1`.`NextDueAmount` AS `NextInstalmentAmount`, concat(`gm1`.`ChitAgreementNo`, '/', `gm1`.`ChitAgreementYear`) AS `CANo`, `tp1`.`ChitGroupID` AS `ChitGroupID`, `v1`.`Voucher_No` AS `Voucher_No`, `m1`.`MemberAddress` AS `MemberAddress`, `ht3`.`Node` AS `BranchName`, `tb1`.`CustomersBankName` AS `CustomersBankName`, `tp1`.`CurrentDueAmount` AS `CurrentDueAmount` from (((((((`trans_payment` `tp1` join `headstree` `ht1` ON ((`tp1`.`ChitGroupID` = `ht1`.`NodeID`))) join `voucher` `v1` ON ((`tp1`.`TransactionKey` = `v1`.`TransactionKey`))) join `headstree` `ht2` ON ((`tp1`.`TokenNumber` = `ht2`.`NodeID`))) join `headstree` `ht3` ON ((`tp1`.`BranchID` = `ht3`.`NodeID`))) join `transbank` `tb1` ON ((`tp1`.`TransactionKey_Bank` = `tb1`.`TransactionKey`))) join `membertogroupmaster` `m1` ON ((`tp1`.`TokenNumber` = `m1`.`Head_Id`))) join `groupmaster` `gm1` ON ((`tp1`.`ChitGroupID` = `gm1`.`Head_Id`))) where ((`tp1`.`BranchID` = " + Session["Branchid"] + ") and (`tp1`.`PaymentDate` between '" + balayer.indiandateToMysqlDate(txtFromDate.Text) + "' and '" + balayer.indiandateToMysqlDate(txtToDate.Text) + "'))";
            }
            else
            {
                grid.SettingsPager.Mode = GridViewPagerMode.ShowAllRecords;
                AccessDataSource1.ConnectionString = CommonClassFile.ConnectionString;
                AccessDataSource1.SelectCommand = @"select `ht1`.`Node` AS `ChitGroup`, `ht2`.`Node` AS `ChitNo`, `tp1`.`DrawNo` AS `InstNo`, `tp1`.`AuctionDate` AS `DateOfAuction`, `tp1`.`ChitAmount` AS `ChitAmount`, `m1`.`MemberName` AS `NameOfThePrizedSubscriber`, `tp1`.`PrizedAmount` AS `PrizedMoney`, `tp1`.`PaymentApplyedOn` AS `FormSentForApprovalOn`, `tp1`.`PaymentDate` AS `DateOfPayment`, concat(`tp1`.`AOSanctionNo`, '/', date_format(`tp1`.`ApprovedOn`,'%d-%b-%y')) AS `AOSanctionNoandDate`, `tp1`.`GuarantorName` AS `GuarantorName`, `tp1`.`NextDrawNo` AS `NextInstalmentNo`, `tp1`.`NextDueAmount` AS `NextInstalmentAmount`, concat(`gm1`.`ChitAgreementNo`, '/', `gm1`.`ChitAgreementYear`) AS `CANo`, `tp1`.`ChitGroupID` AS `ChitGroupID`, `v1`.`Voucher_No` AS `Voucher_No`, `m1`.`MemberAddress` AS `MemberAddress`, `ht3`.`Node` AS `BranchName`, `tb1`.`CustomersBankName` AS `CustomersBankName`, `tp1`.`CurrentDueAmount` AS `CurrentDueAmount` from (((((((`trans_payment` `tp1` join `headstree` `ht1` ON ((`tp1`.`ChitGroupID` = `ht1`.`NodeID`))) join `voucher` `v1` ON ((`tp1`.`TransactionKey` = `v1`.`TransactionKey`))) join `headstree` `ht2` ON ((`tp1`.`TokenNumber` = `ht2`.`NodeID`))) join `headstree` `ht3` ON ((`tp1`.`BranchID` = `ht3`.`NodeID`))) join `transbank` `tb1` ON ((`tp1`.`TransactionKey_Bank` = `tb1`.`TransactionKey`))) join `membertogroupmaster` `m1` ON ((`tp1`.`TokenNumber` = `m1`.`Head_Id`))) join `groupmaster` `gm1` ON ((`tp1`.`ChitGroupID` = `gm1`.`Head_Id`))) where ((`tp1`.`BranchID` = " + Session["Branchid"] + ") and (`tp1`.`ChitGroupID` = " + ddlChit.SelectedValue + ") and (`tp1`.`PaymentDate` between '" + balayer.indiandateToMysqlDate(txtFromDate.Text) + "' and '" + balayer.indiandateToMysqlDate(txtToDate.Text) + "'))";
            }
        }

        protected void BtnStatisticsGo_Click(object sender, EventArgs e)
        {
            select();
            grid.DataBind();
        }
        protected void ASPxGridView1_SummaryDisplayText(object sender, ASPxGridViewSummaryDisplayTextEventArgs e)
        {

        }

        protected void ASPxGridView1_OnCustomSummaryCalculate(object sender, CustomSummaryEventArgs e)
        {

        }

        protected void oncheck_load(object sender, EventArgs e)
        {

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
            //ApplyLayout(Int32.Parse(e.Parameters));
        }
        protected void Export_click(object sender, MenuItemEventArgs e)
        {
            if (e.Item.Text.ToString() == "PDF")
            {
                PrintingSystem ps = new PrintingSystem();
                PrintableComponentLink gridPayment = new PrintableComponentLink(ps);
                gridPayment.Component = gridExport;

                CompositeLink compositeLink = new CompositeLink(ps);
                compositeLink.Links.AddRange(new object[] { gridPayment });
                using (MemoryStream stream = new MemoryStream())
                {
                    compositeLink.PaperKind = System.Drawing.Printing.PaperKind.A4;
                    compositeLink.Landscape = true;
                    compositeLink.CreateDocument(false);
                    compositeLink.PrintingSystem.Document.AutoFitToPagesWidth = 1;
                    compositeLink.PrintingSystem.ExportToPdf(stream);
                    WriteToResponse("DrawalsandPrizedMoneyRegister", true, "pdf", stream);
                }
            }
            else if (e.Item.Text.ToString() == "XLSX")
            {
                gridExport.WriteXlsxToResponse();
            }
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
