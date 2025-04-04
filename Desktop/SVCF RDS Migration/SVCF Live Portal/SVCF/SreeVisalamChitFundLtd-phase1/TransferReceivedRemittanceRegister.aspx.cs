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
using DevExpress.Web.ASPxMenu;
using DevExpress.Web.ASPxGridView;
using DevExpress.Data;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrintingLinks;
using System.IO;
using SVCF_BusinessAccessLayer;
using SVCF_DataAccessLayer;
using SVCF_TransactionLayer;
namespace SreeVisalamChitFundLtd_phase1
{
    public partial class TransferReceivedRemittanceRegister : System.Web.UI.Page
    {
        #region Object
        BusinessLayer balayer = new BusinessLayer();
        TransactionLayer trn = new TransactionLayer();
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            select();
            if (!IsPostBack)
            {
               // chkBox.Checked = true;
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

        protected void select()
        {
            AccessDataSource1.ConnectionString = CommonClassFile.ConnectionString;
            AccessDataSource1.SelectCommand = @"select `transbank`.`CustomersBankName` as `BankName`,`transbank`.`ChequeDDNO` as ChequeDDNO,`transbank`.`DateInCheque`,`voucher`.`TransactionKey` AS `TransactionKey`,  `voucher`.`DualTransactionKey` AS `DualTransactionKey`,  `voucher`.`BranchID` AS `BranchID`,  `voucher`.`CurrDate` AS `Current_Date`,  `voucher`.`Voucher_No` AS `Voucher_No`,  `voucher`.`Voucher_Type` AS `Voucher_Type`,  `voucher`.`Head_Id` AS `Head_Id`,  `voucher`.`ChoosenDate` AS `ChoosenDate`,  `voucher`.`Narration` AS `Narration`,(case when (`groupmaster`.`BranchID`<>" + Session["Branchid"] + ") then `voucher`.`Amount` else 0.00 end) AS `PLAmount`,(case when (`groupmaster`.`BranchID`=" + Session["Branchid"] + ") then `voucher`.`Amount` else 0.00 end) AS `Amount`,  `voucher`.`Series` AS `Series`,  `voucher`.`ReceievedBy` AS `ReceievedBy`,  `voucher`.`Trans_Type` AS `Trans_Type`,  `voucher`.`T_Day` AS `T_Day`,  `voucher`.`T_Month` AS `T_Month`,  `voucher`.`T_Year` AS `T_Year`,  `voucher`.`MemberID` AS `MemberID`,  `voucher`.`Trans_Medium` AS `Trans_Medium`,  `voucher`.`RootID` AS `RootID`,  `voucher`.`ChitGroupId` AS `ChitGroupId`,  `branchdetails`.`B_Name` AS `B_Name`,  `membertogroupmaster`.`GrpMemberID` AS `GrpMemberID`,  `membermaster`.`CustomerName` AS `MemberName`,  `groupmaster`.`GROUPNO` AS `GROUPNO`  from  ((((`voucher`  left join `membermaster` ON ((`voucher`.`MemberID` = `membermaster`.`MemberIDNew`)))  left join `membertogroupmaster` ON ((`voucher`.`Head_Id` = `membertogroupmaster`.`Head_Id`)))  left join `branchdetails` ON ((`voucher`.`BranchID` = `branchdetails`.`Head_Id`)))  left join `groupmaster` ON ((`voucher`.`ChitGroupId` = `groupmaster`.`Head_Id`))) left join `svcf`.`transbank` on (`voucher`.`TransactionKey`=`transbank`.`TransactionKey`) where  (((`voucher`.`Trans_Type` =1) and (`voucher`.`RootID` = 3)) or ((`voucher`.`Other_Trans_Type` = 2) and (`voucher`.`RootID` = 5))) and (`voucher`.`BranchID` = " + Session["Branchid"] + ") AND (`voucher`.`ChoosenDate` between '" + balayer.indiandateToMysqlDate(txtFromDate.Text) + "' AND '" + balayer.indiandateToMysqlDate(txtToDate.Text) + "')";
        }

        protected void BtnStatisticsGo_Click(object sender, EventArgs e)
        {
            select();
            grid.DataBind();
            //ApplyLayout(0);
        }
        protected void ASPxGridView1_SummaryDisplayText(object sender, ASPxGridViewSummaryDisplayTextEventArgs e)
        {
            if (e.Item.FieldName == "Amount")
                e.Text = string.Format("{0}", Convert.ToDouble(e.Value));
            if (e.Item.FieldName == "PLAmount")
                e.Text = string.Format("{0}", Convert.ToDouble(e.Value));
            if (e.Item.FieldName == "Narration")
                e.Text = string.Format(ViewState["crdr"].ToString(), Convert.ToDouble(e.Value));
        }

        protected void ASPxGridView1_OnCustomSummaryCalculate(object sender, CustomSummaryEventArgs e)
        {
            ASPxSummaryItem credit = (sender as ASPxGridView).TotalSummary["Amount"];
            ASPxSummaryItem debit = (sender as ASPxGridView).TotalSummary["PLAmount"];
            Decimal income = Convert.ToDecimal(((ASPxGridView)sender).GetTotalSummaryValue(credit));
            Decimal expense = Convert.ToDecimal(((ASPxGridView)sender).GetTotalSummaryValue(debit));
            if ((income + expense) != 0)
            {
                ViewState["crdr"] = "Total :";
                e.TotalValue = income + expense;
            }
            else
            {
                ViewState["crdr"] = "";
                e.TotalValue = income + expense;
            }
            ViewState["crdr"] += e.TotalValue.ToString();
        }


        protected void oncheck_load(object sender, EventArgs e)
        {
            //if (chkBox.Checked == true)
            //{
            //    select();
            //    ApplyLayout(0);
            //    grid.DataBind();
            //}
            //else
            //{
            //    select();
            //    ApplyLayout(2);
            //    grid.DataBind();
            //}
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
                        grid.GroupBy(grid.Columns["ChoosenDate"]);
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
           // ApplyLayout(Int32.Parse(e.Parameters));
        }

        protected void Export_click(object sender, MenuItemEventArgs e)
        {
            if (e.Item.Text.ToString() == "PDF")
            {
                //gridExport.PaperKind = System.Drawing.Printing.PaperKind.B4;
                //gridExport.Landscape = true;
                //gridExport.PaperName = "TRR";
                //gridExport.RightMargin = 0;
                //gridExport.WritePdfToResponse();

                PrintingSystem ps = new PrintingSystem();
                PrintableComponentLink gridcheque = new PrintableComponentLink(ps);
                gridcheque.Component = gridExport;

                CompositeLink compositeLink = new CompositeLink(ps);
                compositeLink.Links.AddRange(new object[] { gridcheque });

                using (MemoryStream stream = new MemoryStream())
                {
                    compositeLink.PaperKind = System.Drawing.Printing.PaperKind.A4;
                    compositeLink.Landscape = true;
                    compositeLink.CreateDocument(false);
                    compositeLink.PrintingSystem.Document.AutoFitToPagesWidth = 1;
                    compositeLink.PrintingSystem.ExportToPdf(stream);
                    WriteToResponse("ChequeReceivedRegister", true, "pdf", stream);
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
