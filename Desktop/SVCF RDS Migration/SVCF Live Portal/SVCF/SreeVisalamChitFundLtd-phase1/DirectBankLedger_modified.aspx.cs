using System;
using System.Data;
using System.Linq;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxMenu;
using DevExpress.Data;
using System.IO;
using DevExpress.XtraPrintingLinks;
using DevExpress.XtraPrinting;
using System.Drawing;
using SVCF_BusinessAccessLayer;
using SVCF_TransactionLayer;
using SVCF_DataAccessLayer;
using log4net;
using log4net.Config;

namespace SreeVisalamChitFundLtd_phase1
{
    public partial class DirectBankLedger : System.Web.UI.Page
    {
        #region ObjectDeclaration
		    BusinessLayer balayer = new BusinessLayer();
            TransactionLayer tranlayer = new TransactionLayer();
	    #endregion
            string query = "";

        ILog logger = log4net.LogManager.GetLogger(typeof(DepositPayment));

        private System.Drawing.Image headerImage;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ddlGroup.SelectedIndex = 1;
                dateFromConsolidated.Text = DateTime.Now.ToString("dd/MM/yyyy");
                dateToConsolidated.Text = DateTime.Now.ToString("dd/MM/yyyy");
                fillBankHead();
                select();
                ApplyLayout(1);
                setStats(dateFromConsolidated.Text, dateToConsolidated.Text);
                grid.DataBind();
                foreach (GridViewColumn column in grid.Columns)
                {
                    if (column is GridViewDataColumn)
                    {
                        ((GridViewDataColumn)column).Settings.AutoFilterCondition = AutoFilterCondition.Contains;
                    }
                }
               // gridprev_total.Visible = false;
            }
            
            //select();
            
        }
        protected void ResetStats()
        {
            lblCrAmount.Text = "0.00";
            lblCrBalance.Text = "0.00";
            lblDrAmount.Text = "0.00";
            lblDrBalance.Text = "0.00";
        }

        public void fillBankHead()
        {
            query = "SELECT concat( concat(t1.BankName,' _ ',t1.IFCCode),' _ ',t1.AccountNo) as BankDetails, t1.Head_Id as Head_Id, t1.Banklocation as Banklocation FROM svcf.bankdetails as t1 where t1.BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + "";
            DataTable dtBank = balayer.GetDataTable(query);

            //DataRow dr = dtBank.NewRow();
            //dr[0] = "--Select--";
            //dr[1] = "0";
            DDL_BankList.DataValueField = "Head_Id";
            DDL_BankList.DataTextField = "BankDetails";
            //dtBank.Rows.InsertAt(dr, 0);
            DDL_BankList.DataSource = dtBank;
            DDL_BankList.DataBind();
            dtBank.Dispose();

        }
        protected void btnLoanConsolidated_Click(object sender, EventArgs e)
        {
            setStats(dateFromConsolidated.Text, dateToConsolidated.Text);
        }
        protected void setStats(string Fromdate, string Todate)
        {
            ResetStats();
            DataTable dtStats = balayer.GetDataTable("SELECT RootID,sum((case when Voucher_Type='C' then Amount else 0.00 end ))as `Credit Amount`,sum((case when Voucher_Type='D' then Amount else 0.00 end ))as `Debit Amount` FROM `svcf`.`voucher` where BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and ChoosenDate between '" + balayer.indiandateToMysqlDate(Fromdate) + "' and '" + balayer.indiandateToMysqlDate(Todate) + "' and RootID=3 group by RootID");
            if (dtStats.Rows.Count == 1)
            {
                decimal decCredit = Convert.ToDecimal( dtStats.Rows[0]["Credit Amount"].ToString());
                decimal decDebit = Convert.ToDecimal( dtStats.Rows[0]["Debit Amount"].ToString());
                decimal decDebitBalance = 0.00M;
                decimal decCreditBalance = 0.00M;
                decimal.TryParse(balayer.ToobjectstrEvenNull(dtStats.Rows[0][1].ToString()), out decCredit);
                decimal.TryParse(balayer.ToobjectstrEvenNull(dtStats.Rows[0][2].ToString()), out decDebit);
                if ((decCredit - decDebit) > 0.00M)
                {
                    decCreditBalance = decCredit - decDebit;
                }
                else if ((decDebit - decCredit) > 0.00M)
                {
                    decDebitBalance = decDebit - decCredit;
                }
                lblCrAmount.Text = decCredit.ToString();
                lblCrBalance.Text = decCreditBalance.ToString();
                lblDrAmount.Text = decDebit.ToString();
                lblDrBalance.Text = decDebitBalance.ToString();
            }
        }
        protected virtual string GetLabelText(GridViewGroupRowTemplateContainer container)
        {
            return container.GroupText;
        }

        protected void select()
        {
            AccessDataSource1.ConnectionString = CommonClassFile.ConnectionString;
            AccessDataSource1.SelectCommand = @"select t1.ChoosenDate as `Date`,t8.Node as LedgerHead ,bb.BankName as Bank,bb.AccountNo as `AccountNo.`,tt.CustomersBankName as CustomersBank,tt.ChequeDDNO as `ChequeNo.`,t1.Narration,(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )as `Credit`,(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )as `Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left Join headstree as t8 on (t1.ChitGroupID=t8.NodeID) left join transbank as tt on (t1.TransactionKey=tt.TransactionKey) left join bankdetails as bb on (t1.Head_Id=bb.Head_Id) where "+
                "`t1`.`BranchID` = " + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and `t1`.`RootID` = 3 and `t1`.`ChoosenDate` between '" + balayer.indiandateToMysqlDate(dateFromConsolidated.Text) + "' and '" + balayer.indiandateToMysqlDate(dateToConsolidated.Text) + "'  and t1.IsDeleted=0 and bb.BankName='" + DDL_BankList.SelectedItem.Text.Split('_')[0].Trim() + "' and bb.AccountNo='" + DDL_BankList.SelectedItem.Text.Split('_')[2].Trim() + "'  order by t1.ChoosenDate asc";
            grid.DataBind();
            AccessDataSource2.ConnectionString = CommonClassFile.ConnectionString;
            AccessDataSource2.SelectCommand = @"select t1.ChoosenDate as `Date`,t8.Node as LedgerHead ,bb.BankName as Bank,bb.AccountNo as `AccountNo.`,tt.CustomersBankName as CustomersBank,tt.ChequeDDNO as `ChequeNo.`,t1.Narration,(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )as `Credit`,(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )as `Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left Join headstree as t8 on (t1.ChitGroupID=t8.NodeID) left join transbank as tt on (t1.TransactionKey=tt.TransactionKey) left join bankdetails as bb on (t1.Head_Id=bb.Head_Id) where " +
                "`t1`.`BranchID` = " + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and `t1`.`RootID` = 3 and `t1`.`ChoosenDate` < '" + balayer.indiandateToMysqlDate(dateFromConsolidated.Text) + "' and t1.IsDeleted=0 and bb.BankName='" + DDL_BankList.SelectedItem.Text.Split('_')[0].Trim() + "' and bb.AccountNo='" + DDL_BankList.SelectedItem.Text.Split('_')[2].Trim() + "' order by t1.ChoosenDate asc";
            gridprev.DataBind();

            //DataTable BindPrev_Balance = new DataTable();
            
            //query = " select t1.ChoosenDate as `Date`,t8.Node as LedgerHead,bb.BankName as Bank,bb.AccountNo as `AccountNo.`,tt.CustomersBankName as CustomersBank," +
            //        "tt.ChequeDDNO as `ChequeNo.`,t1.Narration,(case when sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end ) > sum(case when t1.Voucher_Type='D' " +
            //        "then t1.Amount else 0.00 end ) then CONCAT('Cr.Bal. ',CAST(sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end ) - sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )AS char(50))) else  CONCAT('Dr.Bal.',CAST(sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) - sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )AS char(50)))  end)  as Narration," +
            //        "sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end ) as `Credit`,sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )as `Debit`" +
            //        "from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left Join headstree as t8 " +
            //        "on (t1.ChitGroupID=t8.NodeID) left join transbank as tt on (t1.TransactionKey=tt.TransactionKey) left join bankdetails as bb on (t1.Head_Id=bb.Head_Id) " +
            //        "where `t1`.`BranchID` =  " + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and `t1`.`RootID` = 3 and `t1`.`ChoosenDate` < '" + balayer.indiandateToMysqlDate(dateFromConsolidated.Text) + "' and t1.IsDeleted=0 and bb.BankName='" + DDL_BankList.SelectedItem.Text.Split('_')[0].Trim() + "' and " +
            //        "bb.AccountNo='" + DDL_BankList.SelectedItem.Text.Split('_')[2].Trim() + "' order by t1.ChoosenDate asc;";

            //BindPrev_Balance = new DataTable();
            //BindPrev_Balance = balayer.GetDataTable(query);
            //gridprev_total.DataSource = BindPrev_Balance;
            //gridprev_total.DataBind();
        }

        protected void BtnStatisticsGo_Click(object sender, EventArgs e)
        {
            setStats(dateFromConsolidated.Text, dateToConsolidated.Text);
            select();
            grid.DataBind();
        }
        protected void ASPxGridView1_SummaryDisplayText(object sender, ASPxGridViewSummaryDisplayTextEventArgs e)
        {
            if (!string.IsNullOrEmpty( Convert.ToString( e.Value)))
            {
                string parsed = balayer.ConvertToIndCurrency(e.Value.ToString());
                if (e.Item.FieldName == "Credit")
                {
                    e.Text = "Cr. " + parsed;
                }
                if (e.Item.FieldName == "Debit")
                {
                    e.Text = "Dr. " + parsed;
                }
                if (e.Item.FieldName == "Narration")
                {
                    e.Text = e.Item.DisplayFormat.ToString() + parsed;
                }

                if (e.Item.FieldName == "Bank")
                {
                    e.Text = parsed;
                }
                if (e.Item.FieldName == "CustomersBank")
                {
                    e.Text = parsed;
                }
            }
        }

        protected void ASPxGridView1_OnCustomSummaryCalculate(object sender, CustomSummaryEventArgs e)
        {
            if (e.IsTotalSummary)
            {
                ASPxSummaryItem x = (ASPxSummaryItem)e.Item;
                if (x.FieldName.ToString() == "Debit")
                {
                    if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Finalize)
                    {
                        decimal total = 0.00M;
                        for (int i = 0; i < grid.VisibleRowCount; i++)
                        {
                            if (grid.IsGroupRow(i))
                            {
                                total += Convert.ToDecimal(grid.GetGroupSummaryValue(i, grid.GroupSummary["Bank"]));
                            }
                        }
                        e.TotalValue = total;
                        e.TotalValueReady = true;
                    }
                }
                if (x.FieldName.ToString() == "Credit")
                {
                    if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Finalize)
                    {
                        decimal total = 0.00M;
                        for (int i = 0; i < grid.VisibleRowCount; i++)
                        {
                            if (grid.IsGroupRow(i))
                            {
                                total += Convert.ToDecimal(grid.GetGroupSummaryValue(i, grid.GroupSummary["CustomersBank"]));
                            }
                        }
                        e.TotalValue = total;
                        e.TotalValueReady = true;
                    }
                }
                if (x.FieldName.ToString() == "Narration")
                {
                    decimal cre = Convert.ToDecimal(grid.GetTotalSummaryValue(grid.TotalSummary["Credit"]));
                    decimal deb = Convert.ToDecimal(grid.GetTotalSummaryValue(grid.TotalSummary["Debit"]));
                    if (cre > deb)
                    {

                        x.DisplayFormat = "Cr.Bal.";
                        e.TotalValue = cre - deb;
                        e.TotalValueReady = true;
                    }
                    else if (cre < deb)
                    {
                        x.DisplayFormat = "Dr.Bal.";
                        e.TotalValue = deb - cre;
                        e.TotalValueReady = true;
                    }
                    else
                    {
                        e.TotalValue = 0.00;
                        e.TotalValueReady = true;
                    }
                }
            }
            if (e.IsGroupSummary)
            {
                ASPxSummaryItem x = (ASPxSummaryItem)e.Item;
                if (x.FieldName.ToString() == "Bank")
                {
                    decimal Credit1 = Convert.ToDecimal(e.GetGroupSummary(e.GroupRowHandle, grid.GroupSummary["Credit"]));
                    decimal Debit1 = Convert.ToDecimal(e.GetGroupSummary(e.GroupRowHandle, grid.GroupSummary["Debit"]));
                    if (Credit1 <= Debit1)
                    {
                        e.TotalValue = Debit1 - Credit1;
                    }
                }
                if (x.FieldName.ToString() == "CustomersBank")
                {
                    decimal Credit1 = Convert.ToDecimal(e.GetGroupSummary(e.GroupRowHandle, grid.GroupSummary["Credit"]));
                    decimal Debit1 = Convert.ToDecimal(e.GetGroupSummary(e.GroupRowHandle, grid.GroupSummary["Debit"]));
                    if (Credit1 >= Debit1)
                    {
                        e.TotalValue = Credit1 - Debit1;
                    }
                }
            }
        }
        protected void ddlGroup_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            select();
            if (ddlGroup.SelectedIndex == 0)
            {
              //  select();
                ApplyLayout(0);
                grid.TotalSummary["Credit"].SummaryType = SummaryItemType.Sum;
                grid.TotalSummary["Debit"].SummaryType = SummaryItemType.Sum;
                grid.SettingsPager.Mode = GridViewPagerMode.ShowPager;
            }
            else if (ddlGroup.SelectedIndex == 1)
            {
               // select();
                ApplyLayout(1);
                grid.TotalSummary["Credit"].SummaryType = SummaryItemType.Custom;
                grid.TotalSummary["Debit"].SummaryType = SummaryItemType.Custom;
                grid.SettingsPager.Mode = GridViewPagerMode.ShowAllRecords;
            }
            else
            {
               
                ApplyLayout(2);
                grid.TotalSummary["Credit"].SummaryType = SummaryItemType.Custom;
                grid.TotalSummary["Debit"].SummaryType = SummaryItemType.Custom;
                grid.SettingsPager.Mode = GridViewPagerMode.ShowAllRecords;
            }
          
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
                        break;
                    case 1:
                        grid.GroupBy(grid.Columns["Bank"]);
                        grid.GroupBy(grid.Columns["Date"]);
                        break;
                    case 2:
                        grid.GroupBy(grid.Columns["Bank"]);
                        grid.GroupBy(grid.Columns["Date"]);
                        break;
                }
            }
            finally
            {
                grid.EndUpdate();
            }
            grid.CollapseAll();
        }

        protected void oncheck_load(object sender, EventArgs e)
        {
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
                    PrintableComponentLink gridcheque = new PrintableComponentLink(ps);
                    PrintableComponentLink gridchequeprev = new PrintableComponentLink(ps);                 
                   // gridprev_total.Visible = true;
                   // gridExportprev.GridViewID = "gridprev_total";
                    gridcheque.Component = gridExport;
                    gridchequeprev.Component = gridExportprev;
                    gridExport.PreserveGroupRowStates = true;
                    gridExportprev.PreserveGroupRowStates = true;
                    Link header = new Link();
                    CompositeLink compositeLink = new CompositeLink(ps);
                    header.CreateDetailArea += new CreateAreaEventHandler(header_CreateDetailArea);
                    compositeLink.Links.AddRange(new object[] { header, gridchequeprev, gridcheque });
                    string leftColumn = "Pages : [Page # of Pages #]";
                    string rightColumn = "Date: [Date Printed]\r\nTime: " + DateTime.Now.ToString("hh:mm tt");
                    PageHeaderFooter phf = compositeLink.PageHeaderFooter as PageHeaderFooter;
                    phf.Footer.Content.Clear();
                    phf.Footer.Content.AddRange(new string[] { rightColumn, leftColumn });
                    phf.Footer.LineAlignment = BrickAlignment.Center;

                    using (MemoryStream stream = new MemoryStream())
                    {
                        compositeLink.PaperKind = System.Drawing.Printing.PaperKind.A4;
                        compositeLink.CreateDocument(false);
                        compositeLink.PrintingSystem.Document.AutoFitToPagesWidth = 1;
                        compositeLink.PrintingSystem.ExportToPdf(stream);
                        WriteToResponse("BankLedger", true, "pdf", stream);
                    }
                }
               // gridprev_total.Visible = false;
            }
        }
        void header_CreateDetailArea(object sender, CreateAreaEventArgs e)
        {
            e.Graph.BorderWidth = 0;
            Rectangle r = new Rectangle(0, 0, 40, 40);
            e.Graph.DrawImage(headerImage, r);
            TextBrick tb = new TextBrick();
            tb.Text = "Sree Visalam Chit Fund Limited";
            tb.Font = new Font("Arial", 12);
            tb.Rect = new RectangleF(50, 15, (e.Graph.ClientPageSize.Width ), 20);
            tb.BorderWidth = 0;
            tb.BackColor = Color.Transparent;
            tb.HorzAlignment = DevExpress.Utils.HorzAlignment.Center;
            tb.VertAlignment = DevExpress.Utils.VertAlignment.Top;
            e.Graph.DrawBrick(tb);
            TextBrick tb1 = new TextBrick();
            tb1.Text = "Branch : " + Session["BranchName"];
            tb1.Font = new Font("Arial", 10);
            tb1.Rect = new RectangleF(50, 35, (e.Graph.ClientPageSize.Width), 30);
            tb1.BorderWidth = 0;
            tb1.BackColor = Color.Transparent;
            tb1.HorzAlignment = DevExpress.Utils.HorzAlignment.Center;
            tb1.VertAlignment = DevExpress.Utils.VertAlignment.Top;
            e.Graph.DrawBrick(tb1);
            //RectangleF r1;
            //r1 = new RectangleF(e.Graph.ClientPageSize.Width / 2, 10, e.Graph.ClientPageSize.Width / 2, 20);
            //e.Graph.DrawString("Sree Visalam Chit Fund Limited", r1);
            //r1 = new RectangleF(e.Graph.ClientPageSize.Width / 2, 25, e.Graph.ClientPageSize.Width / 2, 20);
            //e.Graph.DrawString("Branch : " + Session["BranchName"], r1);
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
        protected void gridprev_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
        {
            if (!e.Row.ClientID.ToLower().Contains("footerrow") || grid.VisibleRowCount <= 0)
                e.Row.Visible = false;
            else
            {
                System.Web.UI.WebControls.TableCell dataCell = e.Row.Cells[0] as System.Web.UI.WebControls.TableCell;
                dataCell.Text = "Previous Net Balance";
                e.Row.Visible = true;
                // for (int iCol = 0; iCol<gridprev.Columns.Count; iCol++)
            }
        }

        protected void gridprev_SummaryDisplayText(object sender, ASPxGridViewSummaryDisplayTextEventArgs e)
        {
            if (!(e.Value == null))
            {
                string parsed = balayer.ConvertToIndCurrency(e.Value.ToString());
                if (e.Item.FieldName == "Credit")
                {
                    e.Text = "Cr. " + parsed;
                }
                if (e.Item.FieldName == "Debit")
                {
                    e.Text = "Dr. " + parsed;
                }
                if (e.Item.FieldName == "Narration")
                {
                    e.Text = e.Item.DisplayFormat.ToString() + parsed;
                }
                if (e.Item.FieldName == "Bank")
                {
                    e.Text = parsed;
                }
                if (e.Item.FieldName == "CustomersBank")
                {
                    e.Text = parsed;
                }
            }
        }

        protected void gridprev_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            if (grid.FilterExpression != "")
            {
                if (grid.FilterExpression != gridprev.FilterExpression)
                {
                    gridprev.FilterExpression = grid.FilterExpression;
                    gridprev.SettingsText.Title = gridprev.FilterExpression;
                }
            }
            else
                if (gridprev.FilterExpression != "")
                    gridprev.FilterExpression = string.Empty;
        }

        protected void gridprev_CustomSummaryCalculate(object sender, CustomSummaryEventArgs e)
        {
            if (e.IsTotalSummary)
            {
                ASPxSummaryItem x = (ASPxSummaryItem)e.Item;
                if (x.FieldName.ToString() == "Debit")
                {
                    if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Finalize)
                    {
                        decimal total = 0.00M;
                        for (int i = 0; i < gridprev.VisibleRowCount; i++)
                        {
                            if (gridprev.IsGroupRow(i))
                            {
                                total += Convert.ToDecimal(gridprev.GetGroupSummaryValue(i, gridprev.GroupSummary["Date"]));
                            }
                        }
                        e.TotalValue = total;
                        e.TotalValueReady = true;
                    }
                }
                if (x.FieldName.ToString() == "Credit")
                {
                    if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Finalize)
                    {
                        decimal total = 0.00M;
                        for (int i = 0; i < gridprev.VisibleRowCount; i++)
                        {
                            if (gridprev.IsGroupRow(i))
                            {
                                total += Convert.ToDecimal(gridprev.GetGroupSummaryValue(i, gridprev.GroupSummary["Branch"]));
                            }
                        }
                        e.TotalValue = total;
                        e.TotalValueReady = true;
                    }
                }
                if (x.FieldName.ToString() == "Narration")
                {
                    decimal cre = Convert.ToDecimal(gridprev.GetTotalSummaryValue(gridprev.TotalSummary["Credit"]));
                    decimal deb = Convert.ToDecimal(gridprev.GetTotalSummaryValue(gridprev.TotalSummary["Debit"]));
                    if (cre > deb)
                    {

                        x.DisplayFormat = "Cr.Bal.";
                        e.TotalValue = cre - deb;
                        e.TotalValueReady = true;
                    }
                    else if (cre < deb)
                    {
                        x.DisplayFormat = "Dr.Bal.";
                        e.TotalValue = deb - cre;
                        e.TotalValueReady = true;
                    }
                    else
                    {
                        e.TotalValue = 0.00;
                        e.TotalValueReady = true;
                    }
                }
            }
            if (e.IsGroupSummary)
            {
                ASPxSummaryItem x = (ASPxSummaryItem)e.Item;
                if (x.FieldName.ToString() == "Bank")
                {
                    decimal Credit1 = Convert.ToDecimal(e.GetGroupSummary(e.GroupRowHandle, gridprev.GroupSummary["Credit"]));
                    decimal Debit1 = Convert.ToDecimal(e.GetGroupSummary(e.GroupRowHandle, gridprev.GroupSummary["Debit"]));
                    if (Credit1 <= Debit1)
                    {
                        e.TotalValue = Debit1 - Credit1;
                        //decimal ss = Convert.ToDecimal(e.TotalValue);
                        //ViewState["Debit"] = Convert.ToDecimal( ViewState["Debit"])+ ss;
                    }
                }
                if (x.FieldName.ToString() == "CustomersBank")
                {
                    decimal Credit1 = Convert.ToDecimal(e.GetGroupSummary(e.GroupRowHandle, gridprev.GroupSummary["Credit"]));
                    decimal Debit1 = Convert.ToDecimal(e.GetGroupSummary(e.GroupRowHandle, gridprev.GroupSummary["Debit"]));
                    if (Credit1 >= Debit1)
                    {
                        e.TotalValue = Credit1 - Debit1;
                        //decimal ss = Convert.ToDecimal(e.TotalValue);
                        //ViewState["Credit"] = Convert.ToDecimal(ViewState["Credit"]) + ss;
                    }
                }
            }
        }

        protected void DDL_BankList_SelectedIndexChanged(object sender, EventArgs e)
        {
            //TxtAccountNumber.Text = DDL_BankList.SelectedItem.Text.Split('_')[2];
        }
    }
}
