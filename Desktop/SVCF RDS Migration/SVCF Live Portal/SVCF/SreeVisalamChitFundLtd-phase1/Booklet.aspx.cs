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
    public partial class Booklet : System.Web.UI.Page
    {
        #region Object
        BusinessLayer balayer = new BusinessLayer();
        TransactionLayer trn = new TransactionLayer();
        #endregion

        private System.Drawing.Image headerImage;

        protected void chkLoadKasr_CheckedChanged(object sender, EventArgs e)
        {
            select();
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
                                total += Convert.ToDecimal(grid.GetGroupSummaryValue(i, grid.GroupSummary["Date"]));
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
                                //total += Convert.ToDecimal(grid.GetGroupSummaryValue(i, grid.GroupSummary["LedgerHead"]));
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
                if (x.FieldName.ToString() == "Date")
                {
                    decimal Credit1 = Convert.ToDecimal(e.GetGroupSummary(e.GroupRowHandle, grid.GroupSummary["Credit"]));
                    decimal Debit1 = Convert.ToDecimal(e.GetGroupSummary(e.GroupRowHandle, grid.GroupSummary["Debit"]));
                    if (Credit1 <= Debit1)
                    {
                        e.TotalValue = Debit1 - Credit1;
                    }
                }
                if (x.FieldName.ToString() == "LedgerHead")
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
        protected void gridPalCustomers_OnCustomSummaryCalculate(object sender, CustomSummaryEventArgs e)
        {
            try
            {
                if (e.IsTotalSummary)
                {
                    ASPxSummaryItem x = (ASPxSummaryItem)e.Item;
                    if (x.FieldName.ToString() == "Debit")
                    {
                        if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Finalize)
                        {
                            decimal total = 0.00M;
                            for (int i = 0; i < gridPalCustomers.VisibleRowCount; i++)
                            {
                                if (gridPalCustomers.IsGroupRow(i))
                                {
                                    total += Convert.ToDecimal(gridPalCustomers.GetGroupSummaryValue(i, gridPalCustomers.GroupSummary["Date"]));
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
                            for (int i = 0; i < gridPalCustomers.VisibleRowCount; i++)
                            {
                                if (gridPalCustomers.IsGroupRow(i))
                                {
                                    total += Convert.ToDecimal(gridPalCustomers.GetGroupSummaryValue(i, gridPalCustomers.GroupSummary["LedgerHead"]));
                                }
                            }
                            e.TotalValue = total;
                            e.TotalValueReady = true;
                        }
                    }
                    if (x.FieldName.ToString() == "Narration")
                    {
                        decimal cre = Convert.ToDecimal(gridPalCustomers.GetTotalSummaryValue(gridPalCustomers.TotalSummary["Credit"]));
                        decimal deb = Convert.ToDecimal(gridPalCustomers.GetTotalSummaryValue(gridPalCustomers.TotalSummary["Debit"]));
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
                    if (x.FieldName.ToString() == "Date")
                    {
                        decimal Credit1 = Convert.ToDecimal(e.GetGroupSummary(e.GroupRowHandle, gridPalCustomers.GroupSummary["Credit"]));
                        decimal Debit1 = Convert.ToDecimal(e.GetGroupSummary(e.GroupRowHandle, gridPalCustomers.GroupSummary["Debit"]));
                        if (Credit1 <= Debit1)
                        {
                            e.TotalValue = Debit1 - Credit1;
                        }
                    }
                    if (x.FieldName.ToString() == "LedgerHead")
                    {
                        decimal Credit1 = Convert.ToDecimal(e.GetGroupSummary(e.GroupRowHandle, gridPalCustomers.GroupSummary["Credit"]));
                        decimal Debit1 = Convert.ToDecimal(e.GetGroupSummary(e.GroupRowHandle, gridPalCustomers.GroupSummary["Debit"]));
                        if (Credit1 >= Debit1)
                        {
                            e.TotalValue = Credit1 - Debit1;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string stTemp = ex.Message;
            }
        }
        protected void gridPal2Customers_OnCustomSummaryCalculate(object sender, CustomSummaryEventArgs e)
        {
            try
            {
                if (e.IsTotalSummary)
                {
                    ASPxSummaryItem x = (ASPxSummaryItem)e.Item;
                    if (x.FieldName.ToString() == "Debit")
                    {
                        if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Finalize)
                        {
                            decimal total = 0.00M;
                            for (int i = 0; i < gridPal2Customers.VisibleRowCount; i++)
                            {
                                if (gridPal2Customers.IsGroupRow(i))
                                {
                                    total += Convert.ToDecimal(gridPal2Customers.GetGroupSummaryValue(i, gridPal2Customers.GroupSummary["Date"]));
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
                            for (int i = 0; i < gridPal2Customers.VisibleRowCount; i++)
                            {
                                if (gridPal2Customers.IsGroupRow(i))
                                {
                                    total += Convert.ToDecimal(gridPal2Customers.GetGroupSummaryValue(i, gridPal2Customers.GroupSummary["LedgerHead4"]));
                                }
                            }
                            e.TotalValue = total;
                            e.TotalValueReady = true;
                        }
                    }
                    if (x.FieldName.ToString() == "Narration")
                    {
                        decimal cre = Convert.ToDecimal(gridPal2Customers.GetTotalSummaryValue(gridPal2Customers.TotalSummary["Credit"]));
                        decimal deb = Convert.ToDecimal(gridPal2Customers.GetTotalSummaryValue(gridPal2Customers.TotalSummary["Debit"]));
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
                    if (x.FieldName.ToString() == "Date")
                    {
                        decimal Credit1 = Convert.ToDecimal(e.GetGroupSummary(e.GroupRowHandle, gridPal2Customers.GroupSummary["Credit"]));
                        decimal Debit1 = Convert.ToDecimal(e.GetGroupSummary(e.GroupRowHandle, gridPal2Customers.GroupSummary["Debit"]));
                        if (Credit1 <= Debit1)
                        {
                            e.TotalValue = Debit1 - Credit1;
                        }
                    }
                    if (x.FieldName.ToString() == "LedgerHead")
                    {
                        decimal Credit1 = Convert.ToDecimal(e.GetGroupSummary(e.GroupRowHandle, gridPal2Customers.GroupSummary["Credit"]));
                        decimal Debit1 = Convert.ToDecimal(e.GetGroupSummary(e.GroupRowHandle, gridPal2Customers.GroupSummary["Debit"]));
                        if (Credit1 >= Debit1)
                        {
                            e.TotalValue = Credit1 - Debit1;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string stTemp = ex.Message;
            }
        }
        protected void gridPal3Customers_OnCustomSummaryCalculate(object sender, CustomSummaryEventArgs e)
        {
            try
            {
                if (e.IsTotalSummary)
                {
                    ASPxSummaryItem x = (ASPxSummaryItem)e.Item;
                    if (x.FieldName.ToString() == "Debit")
                    {
                        if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Finalize)
                        {
                            decimal total = 0.00M;
                            for (int i = 0; i < gridPal3Customers.VisibleRowCount; i++)
                            {
                                if (gridPal3Customers.IsGroupRow(i))
                                {
                                    total += Convert.ToDecimal(gridPal3Customers.GetGroupSummaryValue(i, gridPal3Customers.GroupSummary["Date"]));
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
                            for (int i = 0; i < gridPal3Customers.VisibleRowCount; i++)
                            {
                                if (gridPal3Customers.IsGroupRow(i))
                                {
                                    total += Convert.ToDecimal(gridPal3Customers.GetGroupSummaryValue(i, gridPal3Customers.GroupSummary["LedgerHead"]));
                                }
                            }
                            e.TotalValue = total;
                            e.TotalValueReady = true;
                        }
                    }
                    if (x.FieldName.ToString() == "Narration")
                    {
                        decimal cre = Convert.ToDecimal(gridPal3Customers.GetTotalSummaryValue(gridPal3Customers.TotalSummary["Credit"]));
                        decimal deb = Convert.ToDecimal(gridPal3Customers.GetTotalSummaryValue(gridPal3Customers.TotalSummary["Debit"]));
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
                    if (x.FieldName.ToString() == "Date")
                    {
                        decimal Credit1 = Convert.ToDecimal(e.GetGroupSummary(e.GroupRowHandle, gridPal3Customers.GroupSummary["Credit"]));
                        decimal Debit1 = Convert.ToDecimal(e.GetGroupSummary(e.GroupRowHandle, gridPal3Customers.GroupSummary["Debit"]));
                        if (Credit1 <= Debit1)
                        {
                            e.TotalValue = Debit1 - Credit1;
                        }
                    }
                    if (x.FieldName.ToString() == "LedgerHead")
                    {
                        decimal Credit1 = Convert.ToDecimal(e.GetGroupSummary(e.GroupRowHandle, gridPal3Customers.GroupSummary["Credit"]));
                        decimal Debit1 = Convert.ToDecimal(e.GetGroupSummary(e.GroupRowHandle, gridPal3Customers.GroupSummary["Debit"]));
                        if (Credit1 >= Debit1)
                        {
                            e.TotalValue = Credit1 - Debit1;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string stTemp = ex.Message;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                aaaaa.Checked = true;
                chkLoadKasr.Checked = true;
                txtFromDate1.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtToDate1.Text = DateTime.Now.ToString("dd/MM/yyyy");
                select();
                ApplyLayout(0);
                foreach (GridViewColumn column in grid.Columns)
                {
                    if (column is GridViewDataColumn)
                    {
                        ((GridViewDataColumn)column).Settings.AutoFilterCondition = AutoFilterCondition.Contains;
                    }
                }
                foreach (GridViewColumn column in gridPalCustomers.Columns)
                {
                    if (column is GridViewDataColumn)
                    {
                        ((GridViewDataColumn)column).Settings.AutoFilterCondition = AutoFilterCondition.Contains;
                    }
                }
                foreach (GridViewColumn column in gridPal2Customers.Columns)
                {
                    if (column is GridViewDataColumn)
                    {
                        ((GridViewDataColumn)column).Settings.AutoFilterCondition = AutoFilterCondition.Contains;
                    }
                }
                foreach (GridViewColumn column in gridPal3Customers.Columns)
                {
                    if (column is GridViewDataColumn)
                    {
                        ((GridViewDataColumn)column).Settings.AutoFilterCondition = AutoFilterCondition.Contains;
                    }
                }
            }
            select();
        }
        protected void oncheck_load(object sender, EventArgs e)
        {
            if (aaaaa.Checked == true)
            {
                select();
                ApplyLayout(0);
                if (balayer.ToobjectstrEvenNull(Session["Branchid"]) == "161")
                {
                    gridPalCustomers.TotalSummary["Credit"].SummaryType = SummaryItemType.Custom;
                    gridPalCustomers.TotalSummary["Debit"].SummaryType = SummaryItemType.Custom;
                    gridPal2Customers.TotalSummary["Credit"].SummaryType = SummaryItemType.Custom;
                    gridPal2Customers.TotalSummary["Debit"].SummaryType = SummaryItemType.Custom;
                    gridPal3Customers.TotalSummary["Credit"].SummaryType = SummaryItemType.Custom;
                    gridPal3Customers.TotalSummary["Debit"].SummaryType = SummaryItemType.Custom;
                }
                else
                {
                    grid.TotalSummary["Credit"].SummaryType = SummaryItemType.Custom;
                    grid.TotalSummary["Debit"].SummaryType = SummaryItemType.Custom;
                }
            }
            else
            {
                select();
                ApplyLayout(2);
                if (balayer.ToobjectstrEvenNull(Session["Branchid"]) == "161")
                {
                    //gridPalCustomers.TotalSummary["Credit"].SummaryType = SummaryItemType.Sum;
                    //gridPalCustomers.TotalSummary["Debit"].SummaryType = SummaryItemType.Sum;
                    gridPal2Customers.TotalSummary["Credit"].SummaryType = SummaryItemType.Sum;
                    gridPal2Customers.TotalSummary["Debit"].SummaryType = SummaryItemType.Sum;
                    //gridPal3Customers.TotalSummary["Credit"].SummaryType = SummaryItemType.Sum;
                    //gridPal3Customers.TotalSummary["Debit"].SummaryType = SummaryItemType.Sum;
                }
                else
                {
                    grid.TotalSummary["Credit"].SummaryType = SummaryItemType.Sum;
                    grid.TotalSummary["Debit"].SummaryType = SummaryItemType.Sum;
                }
            }
        }
        protected void BtnStatisticsGo_Click1(object sender, EventArgs e)
        {
            select();
        }
        void ApplyLayout(int layoutIndex)
        {
            if (balayer.ToobjectstrEvenNull(Session["Branchid"]) == "161")
            {
               // gridPalCustomers.BeginUpdate();
                gridPal2Customers.BeginUpdate();
               // gridPal3Customers.BeginUpdate();
                try
                {
                  //  gridPalCustomers.ClearSort();
                    gridPal2Customers.ClearSort();
                //    gridPal3Customers.ClearSort();
                    switch (layoutIndex)
                    {
                        case 0:
                            //gridPalCustomers.SettingsPager.Mode = GridViewPagerMode.ShowAllRecords;
                            //gridPalCustomers.GroupBy(gridPalCustomers.Columns["Node"]);
                            //gridPalCustomers.GroupBy(gridPalCustomers.Columns["LedgerHead"]);
                            gridPal2Customers.SettingsPager.Mode = GridViewPagerMode.ShowAllRecords;
                            gridPal2Customers.GroupBy(gridPal2Customers.Columns["Node"]);
                            gridPal2Customers.GroupBy(gridPal2Customers.Columns["LedgerHead1"]);
                            gridPal2Customers.GroupBy(gridPal2Customers.Columns["LedgerHead2"]);
                            gridPal2Customers.GroupBy(gridPal2Customers.Columns["LedgerHead3"]);
                            gridPal2Customers.GroupBy(gridPal2Customers.Columns["LedgerHead4"]);
                            //gridPal3Customers.SettingsPager.Mode = GridViewPagerMode.ShowAllRecords;
                            //gridPal3Customers.GroupBy(gridPal3Customers.Columns["Node"]);
                            //gridPal3Customers.GroupBy(gridPal3Customers.Columns["LedgerHead"]);

                            break;
                        case 2:
                            //gridPalCustomers.SettingsPager.Mode = GridViewPagerMode.ShowPager;
                            //gridPalCustomers.SettingsPager.Position = PagerPosition.Bottom;
                            gridPal2Customers.SettingsPager.Mode = GridViewPagerMode.ShowPager;
                            gridPal2Customers.SettingsPager.Position = PagerPosition.Bottom;
                            //gridPal3Customers.SettingsPager.Mode = GridViewPagerMode.ShowPager;
                            //gridPal3Customers.SettingsPager.Position = PagerPosition.Bottom;
                            break;
                    }
                }
                finally
                {
                  //  gridPalCustomers.EndUpdate();
                    gridPal2Customers.EndUpdate();
                 //   gridPal3Customers.EndUpdate();
                }
             //   gridPalCustomers.CollapseAll();
                gridPal2Customers.CollapseAll();
             //   gridPal3Customers.CollapseAll();
            }
            else
            {
                grid.BeginUpdate();
                try
                {
                    grid.ClearSort();
                    switch (layoutIndex)
                    {
                        case 0:
                            grid.SettingsPager.Mode = GridViewPagerMode.ShowAllRecords;
                            grid.GroupBy(grid.Columns["Node"]);
                            grid.GroupBy(grid.Columns["LedgerHead1"]);
                            grid.GroupBy(grid.Columns["LedgerHead2"]);
                            grid.GroupBy(grid.Columns["LedgerHead3"]);
                            grid.GroupBy(grid.Columns["LedgerHead4"]);
                            break;
                        case 2:
                            grid.SettingsPager.Mode = GridViewPagerMode.ShowPager;
                            grid.SettingsPager.Position = PagerPosition.Bottom;
                            break;
                    }
                }
                finally
                {
                    grid.EndUpdate();
                }
                grid.CollapseAll();
            }
        }
        protected void grid_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            ApplyLayout(Int32.Parse(e.Parameters));
        }

        protected void gridPalCustomers_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            ApplyLayout(Int32.Parse(e.Parameters));
        }

        protected void gridPal2Customers_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            ApplyLayout(Int32.Parse(e.Parameters));
        }

        protected void gridPal3Customers_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            ApplyLayout(Int32.Parse(e.Parameters));
        }

        protected void gridCustomers_CustomColumnSort(object sender, CustomColumnSortEventArgs e)
        {
            try
            {
                if (e.Column != null & (e.Column.FieldName == "Node" || e.Column.FieldName == "LedgerHead1" || e.Column.FieldName == "LedgerHead2" || e.Column.FieldName == "LedgerHead3" || e.Column.FieldName == "LedgerHead4"))
                {

                    object country1 = e.GetRow1Value("RootID");

                    object country2 = e.GetRow2Value("RootID");

                    int res = Comparer.Default.Compare(country1, country2);

                    if (res == 0)
                    {

                        object city1 = e.Value1;

                        object city2 = e.Value2;

                        res = Comparer.Default.Compare(city1, city2);

                    }

                    e.Result = res;

                    e.Handled = true;

                }
            }
            catch (Exception ex)
            {
                string stTemp = ex.Message;
            }

        }

        protected void gridPalCustomers_CustomColumnSort(object sender, CustomColumnSortEventArgs e)
        {
            try
            {
                if (e.Column != null & (e.Column.FieldName == "Node" || e.Column.FieldName == "LedgerHead"))
                {

                    object country1 = e.GetRow1Value("RootID");

                    object country2 = e.GetRow2Value("RootID");

                    int res = Comparer.Default.Compare(country1, country2);

                    if (res == 0)
                    {

                        object city1 = e.Value1;

                        object city2 = e.Value2;

                        res = Comparer.Default.Compare(city1, city2);

                    }

                    e.Result = res;

                    e.Handled = true;

                }
            }
            catch (Exception ex)
            {
                string stTemp = ex.Message;
            }

        }
        protected void gridPal2Customers_CustomColumnSort(object sender, CustomColumnSortEventArgs e)
        {
            try
            {
                if (e.Column != null & (e.Column.FieldName == "Node" || e.Column.FieldName == "LedgerHead1" || e.Column.FieldName == "LedgerHead2" || e.Column.FieldName == "LedgerHead3" || e.Column.FieldName == "LedgerHead4"))
                {

                    object country1 = e.GetRow1Value("RootID");

                    object country2 = e.GetRow2Value("RootID");

                    int res = Comparer.Default.Compare(country1, country2);

                    if (res == 0)
                    {

                        object city1 = e.Value1;

                        object city2 = e.Value2;

                        res = Comparer.Default.Compare(city1, city2);

                    }

                    e.Result = res;

                    e.Handled = true;

                }
            }
            catch (Exception ex)
            {
                string stTemp = ex.Message;
            }

        }
        protected void gridPal3Customers_CustomColumnSort(object sender, CustomColumnSortEventArgs e)
        {
            try
            {
                if (e.Column != null & (e.Column.FieldName == "Node" || e.Column.FieldName == "LedgerHead"))
                {

                    object country1 = e.GetRow1Value("RootID");

                    object country2 = e.GetRow2Value("RootID");

                    int res = Comparer.Default.Compare(country1, country2);

                    if (res == 0)
                    {

                        object city1 = e.Value1;

                        object city2 = e.Value2;

                        res = Comparer.Default.Compare(city1, city2);

                    }

                    e.Result = res;

                    e.Handled = true;

                }
            }
            catch (Exception ex)
            {
                string stTemp = ex.Message;
            }

        }
        protected void select()
        {
            gridPalCustomers.Visible = false;
            gridPal2Customers.Visible = false;
            gridPal3Customers.Visible = false;
            grid.Visible = false;
            if (balayer.ToobjectstrEvenNull(Session["Branchid"]) == "161")
            {
                gridPalCustomers.Visible = false;
                gridPal2Customers.Visible = true;
                gridPal3Customers.Visible = false;
                if (!chkLoadKasr.Checked)
                {
                    // AccessDataSource2.SelectCommand = @"SELECT `voucher`.`Narration`, voucher.RootID as RootID,`voucher`.`ChoosenDate` as `Date`,h1.Node as LedgerHead,`headstree`.`Node`,((case when voucher.Voucher_Type='C' then voucher.Amount else 0.00 end )) as `Credit`,((case when voucher.Voucher_Type='D' then voucher.Amount else 0.00 end )) as `Debit` FROM `svcf`.`voucher` join headstree on (voucher.RootID=headstree.NodeID) join headstree as h1 on (`voucher`.`Head_Id`=`h1`.`NodeID`) where voucher.BranchID=160 and voucher.ChoosenDate between '" + balayer.indiandateToMysqlDate(txtFromDate1.Text) + "' and '" + balayer.indiandateToMysqlDate(txtToDate1.Text) + "' and Other_Trans_Type<>5 order by voucher.RootID";
                    // AccessDataSource3.SelectCommand = @"SELECT `voucher`.`Narration`, voucher.RootID as RootID,`voucher`.`ChoosenDate` as `Date`,h1.Node as LedgerHead,`headstree`.`Node`,((case when voucher.Voucher_Type='C' then voucher.Amount else 0.00 end )) as `Credit`,((case when voucher.Voucher_Type='D' then voucher.Amount else 0.00 end )) as `Debit` FROM `svcf`.`voucher` join headstree on (voucher.RootID=headstree.NodeID) join headstree as h1 on (`voucher`.`Head_Id`=`h1`.`NodeID`) where voucher.BranchID=161 and voucher.ChoosenDate between '" + balayer.indiandateToMysqlDate(txtFromDate1.Text) + "' and '" + balayer.indiandateToMysqlDate(txtToDate1.Text) + "' and Other_Trans_Type<>5 order by voucher.RootID";
                    // AccessDataSource4.SelectCommand = @"SELECT `voucher`.`Narration`, voucher.RootID as RootID,`voucher`.`ChoosenDate` as `Date`,h1.Node as LedgerHead,`headstree`.`Node`,((case when voucher.Voucher_Type='C' then voucher.Amount else 0.00 end )) as `Credit`,((case when voucher.Voucher_Type='D' then voucher.Amount else 0.00 end )) as `Debit` FROM `svcf`.`voucher` join headstree on (voucher.RootID=headstree.NodeID) join headstree as h1 on (`voucher`.`Head_Id`=`h1`.`NodeID`) where voucher.BranchID=162 and voucher.ChoosenDate between '" + balayer.indiandateToMysqlDate(txtFromDate1.Text) + "' and '" + balayer.indiandateToMysqlDate(txtToDate1.Text) + "' and Other_Trans_Type<>5 order by voucher.RootID";
                    AccessDataSource3.ConnectionString = CommonClassFile.ConnectionString;
                    AccessDataSource3.SelectCommand = @"select SUBSTRING_INDEX(h1.TreeHint, ',', 1 ) as s1,SUBSTRING_INDEX(SUBSTRING_INDEX(h1.TreeHint, ',', 2 ),',','-1') as s2,SUBSTRING_INDEX(SUBSTRING_INDEX(h1.TreeHint, ',', 3 ),',','-1') as s3,SUBSTRING_INDEX(SUBSTRING_INDEX(h1.TreeHint, ',', 4 ),',','-1') as s4,(SELECT Node FROM svcf.headstree where NodeID=s1)as LedgerHead1,((case when ROUND((LENGTH(h1.TreeHint)- LENGTH( REPLACE (h1.TreeHint, ',', ''))) / LENGTH(','))  >0 then (SELECT Node FROM svcf.headstree where NodeID=s2) else '' end )) as LedgerHead2,((case when ROUND((LENGTH(h1.TreeHint)- LENGTH( REPLACE (h1.TreeHint, ',', ''))) / LENGTH(','))  >1 then (SELECT Node FROM svcf.headstree where NodeID=s3) else '' end )) as LedgerHead3,((case when ROUND((LENGTH(h1.TreeHint)- LENGTH( REPLACE (h1.TreeHint, ',', ''))) / LENGTH(','))  >2 then (SELECT Node FROM svcf.headstree where NodeID=s4) else '' end )) as LedgerHead4,`voucher`.`Narration`, voucher.RootID as RootID,`voucher`.`ChoosenDate` as `Date`,((case when voucher.Voucher_Type='C' then voucher.Amount else 0.00 end )) as `Credit`,((case when voucher.Voucher_Type='D' then voucher.Amount else 0.00 end )) as `Debit` FROM `svcf`.`voucher` join headstree on (voucher.RootID=headstree.NodeID) join headstree as h1 on (`voucher`.`Head_Id`=`h1`.`NodeID`) where voucher.BranchID in(160,161,162) and voucher.ChoosenDate between '" + balayer.indiandateToMysqlDate(txtFromDate1.Text) + "' and '" + balayer.indiandateToMysqlDate(txtToDate1.Text) + "' order by voucher.ChoosenDate";
                }
                else
                {
                    //    AccessDataSource2.SelectCommand = @"SELECT `voucher`.`Narration`, voucher.RootID as RootID,`voucher`.`ChoosenDate` as `Date`,h1.Node as LedgerHead,`headstree`.`Node`,((case when voucher.Voucher_Type='C' then voucher.Amount else 0.00 end )) as `Credit`,((case when voucher.Voucher_Type='D' then voucher.Amount else 0.00 end )) as `Debit` FROM `svcf`.`voucher` join headstree on (voucher.RootID=headstree.NodeID) join headstree as h1 on (`voucher`.`Head_Id`=`h1`.`NodeID`) where voucher.BranchID=160 and voucher.ChoosenDate between '" + balayer.indiandateToMysqlDate(txtFromDate1.Text) + "' and '" + balayer.indiandateToMysqlDate(txtToDate1.Text) + "' order by voucher.RootID";
                    //AccessDataSource3.SelectCommand = @"SELECT `voucher`.`Narration`, voucher.RootID as RootID,`voucher`.`ChoosenDate` as `Date`,h1.Node as LedgerHead,`headstree`.`Node`,((case when voucher.Voucher_Type='C' then voucher.Amount else 0.00 end )) as `Credit`,((case when voucher.Voucher_Type='D' then voucher.Amount else 0.00 end )) as `Debit` FROM `svcf`.`voucher` join headstree on (voucher.RootID=headstree.NodeID) join headstree as h1 on (`voucher`.`Head_Id`=`h1`.`NodeID`) where voucher.BranchID=161 and voucher.ChoosenDate between '" + balayer.indiandateToMysqlDate(txtFromDate1.Text) + "' and '" + balayer.indiandateToMysqlDate(txtToDate1.Text) + "' order by voucher.RootID";
                    //AccessDataSource4.SelectCommand = @"SELECT `voucher`.`Narration`, voucher.RootID as RootID,`voucher`.`ChoosenDate` as `Date`,h1.Node as LedgerHead,`headstree`.`Node`,((case when voucher.Voucher_Type='C' then voucher.Amount else 0.00 end )) as `Credit`,((case when voucher.Voucher_Type='D' then voucher.Amount else 0.00 end )) as `Debit` FROM `svcf`.`voucher` join headstree on (voucher.RootID=headstree.NodeID) join headstree as h1 on (`voucher`.`Head_Id`=`h1`.`NodeID`) where voucher.BranchID=162 and voucher.ChoosenDate between '" + balayer.indiandateToMysqlDate(txtFromDate1.Text) + "' and '" + balayer.indiandateToMysqlDate(txtToDate1.Text) + "' order by voucher.RootID";
                    AccessDataSource3.ConnectionString = CommonClassFile.ConnectionString;
                    AccessDataSource3.SelectCommand = @"select SUBSTRING_INDEX(h1.TreeHint, ',', 1 ) as s1,SUBSTRING_INDEX(SUBSTRING_INDEX(h1.TreeHint, ',', 2 ),',','-1') as s2,SUBSTRING_INDEX(SUBSTRING_INDEX(h1.TreeHint, ',', 3 ),',','-1') as s3,SUBSTRING_INDEX(SUBSTRING_INDEX(h1.TreeHint, ',', 4 ),',','-1') as s4,(SELECT Node FROM svcf.headstree where NodeID=s1)as LedgerHead1,((case when ROUND((LENGTH(h1.TreeHint)- LENGTH( REPLACE (h1.TreeHint, ',', ''))) / LENGTH(','))  >0 then (SELECT Node FROM svcf.headstree where NodeID=s2) else '' end )) as LedgerHead2,((case when ROUND((LENGTH(h1.TreeHint)- LENGTH( REPLACE (h1.TreeHint, ',', ''))) / LENGTH(','))  >1 then (SELECT Node FROM svcf.headstree where NodeID=s3) else '' end )) as LedgerHead3,((case when ROUND((LENGTH(h1.TreeHint)- LENGTH( REPLACE (h1.TreeHint, ',', ''))) / LENGTH(','))  >2 then (SELECT Node FROM svcf.headstree where NodeID=s4) else '' end )) as LedgerHead4,`voucher`.`Narration`, voucher.RootID as RootID,`voucher`.`ChoosenDate` as `Date`,((case when voucher.Voucher_Type='C' then voucher.Amount else 0.00 end )) as `Credit`,((case when voucher.Voucher_Type='D' then voucher.Amount else 0.00 end )) as `Debit` FROM `svcf`.`voucher` join headstree on (voucher.RootID=headstree.NodeID) join headstree as h1 on (`voucher`.`Head_Id`=`h1`.`NodeID`) where voucher.BranchID in(160,161,162) and voucher.ChoosenDate between '" + balayer.indiandateToMysqlDate(txtFromDate1.Text) + "' and '" + balayer.indiandateToMysqlDate(txtToDate1.Text) + "'  order by voucher.ChoosenDate";
                }
               // gridPalCustomers.Settings.GroupFormat = "{1}{2}";
             //   gridPalCustomers.DataBind();
                gridPal2Customers.Settings.GroupFormat = "{1}{2}";
                gridPal2Customers.DataBind();
               // gridPal3Customers.Settings.GroupFormat = "{1}{2}";
              //  gridPal3Customers.DataBind();
            }
            else
            {
                grid.Visible = true;
                if (!chkLoadKasr.Checked)
                {
                    AccessDataSource1.ConnectionString = CommonClassFile.ConnectionString;
                    AccessDataSource1.SelectCommand = @"select SUBSTRING_INDEX(h1.TreeHint, ',', 1 ) as s1,SUBSTRING_INDEX(SUBSTRING_INDEX(h1.TreeHint, ',', 2 ),',','-1') as s2,SUBSTRING_INDEX(SUBSTRING_INDEX(h1.TreeHint, ',', 3 ),',','-1') as s3,SUBSTRING_INDEX(SUBSTRING_INDEX(h1.TreeHint, ',', 4 ),',','-1') as s4,(SELECT Node FROM svcf.headstree where NodeID=s1)as LedgerHead1,((case when ROUND((LENGTH(h1.TreeHint)- LENGTH( REPLACE (h1.TreeHint, ',', ''))) / LENGTH(','))  >0 then (SELECT Node FROM svcf.headstree where NodeID=s2) else '' end )) as LedgerHead2,((case when ROUND((LENGTH(h1.TreeHint)- LENGTH( REPLACE (h1.TreeHint, ',', ''))) / LENGTH(','))  >1 then (SELECT Node FROM svcf.headstree where NodeID=s3) else '' end )) as LedgerHead3,((case when ROUND((LENGTH(h1.TreeHint)- LENGTH( REPLACE (h1.TreeHint, ',', ''))) / LENGTH(','))  >2 then (SELECT Node FROM svcf.headstree where NodeID=s4) else '' end )) as LedgerHead4,`voucher`.`Narration`, voucher.RootID as RootID,`voucher`.`ChoosenDate` as `Date`,((case when voucher.Voucher_Type='C' then voucher.Amount else 0.00 end )) as `Credit`,((case when voucher.Voucher_Type='D' then voucher.Amount else 0.00 end )) as `Debit` FROM `svcf`.`voucher` join headstree on (voucher.RootID=headstree.NodeID) join headstree as h1 on (`voucher`.`Head_Id`=`h1`.`NodeID`) where voucher.BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and voucher.ChoosenDate between '" + balayer.indiandateToMysqlDate(txtFromDate1.Text) + "' and '" + balayer.indiandateToMysqlDate(txtToDate1.Text) + "' and Other_Trans_Type<>5 order by voucher.ChoosenDate";
                }
                else
                {
                    AccessDataSource1.ConnectionString = CommonClassFile.ConnectionString;
                    AccessDataSource1.SelectCommand = @"select SUBSTRING_INDEX(h1.TreeHint, ',', 1 ) as s1,SUBSTRING_INDEX(SUBSTRING_INDEX(h1.TreeHint, ',', 2 ),',','-1') as s2,SUBSTRING_INDEX(SUBSTRING_INDEX(h1.TreeHint, ',', 3 ),',','-1') as s3,SUBSTRING_INDEX(SUBSTRING_INDEX(h1.TreeHint, ',', 4 ),',','-1') as s4,(SELECT Node FROM svcf.headstree where NodeID=s1)as LedgerHead1,((case when ROUND((LENGTH(h1.TreeHint)- LENGTH( REPLACE (h1.TreeHint, ',', ''))) / LENGTH(','))  >0 then (SELECT Node FROM svcf.headstree where NodeID=s2) else '' end )) as LedgerHead2,((case when ROUND((LENGTH(h1.TreeHint)- LENGTH( REPLACE (h1.TreeHint, ',', ''))) / LENGTH(','))  >1 then (SELECT Node FROM svcf.headstree where NodeID=s3) else '' end )) as LedgerHead3,((case when ROUND((LENGTH(h1.TreeHint)- LENGTH( REPLACE (h1.TreeHint, ',', ''))) / LENGTH(','))  >2 then (SELECT Node FROM svcf.headstree where NodeID=s4) else '' end )) as LedgerHead4,`voucher`.`Narration`, voucher.RootID as RootID,`voucher`.`ChoosenDate` as `Date`,((case when voucher.Voucher_Type='C' then voucher.Amount else 0.00 end )) as `Credit`,((case when voucher.Voucher_Type='D' then voucher.Amount else 0.00 end )) as `Debit`  FROM `svcf`.`voucher` join headstree on (voucher.RootID=headstree.NodeID) join headstree as h1 on (`voucher`.`Head_Id`=`h1`.`NodeID`) where voucher.BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and voucher.ChoosenDate between '" + balayer.indiandateToMysqlDate(txtFromDate1.Text) + "' and '" + balayer.indiandateToMysqlDate(txtToDate1.Text) + "' order by voucher.ChoosenDate";
                }
                grid.Settings.GroupFormat = "{1}{2}";
                grid.DataBind();
            }
        }
        protected void ASPxGridView1_SummaryDisplayText(object sender, ASPxGridViewSummaryDisplayTextEventArgs e)
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
            if (e.Item.FieldName == "Date")
            {
                e.Text = parsed;
            }
            if (e.Item.FieldName == "LedgerHead1")
            {
                e.Text = parsed;
            }
            if (e.Item.FieldName == "LedgerHead2")
            {
                e.Text = parsed;
            }
            if (e.Item.FieldName == "LedgerHead3")
            {
                e.Text = parsed;
            }
            if (e.Item.FieldName == "LedgerHead4")
            {
                e.Text = parsed;
            }
        }
        protected void gridPalCustomers_SummaryDisplayText(object sender, ASPxGridViewSummaryDisplayTextEventArgs e)
        {
            try
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
                if (e.Item.FieldName == "Date")
                {
                    e.Text = parsed;
                }
                if (e.Item.FieldName == "LedgerHead")
                {
                    e.Text = parsed;
                }
            }
            catch (Exception ex)
            {
                string stTemp = ex.Message;
            }

        }
        protected void gridPal2Customers_SummaryDisplayText(object sender, ASPxGridViewSummaryDisplayTextEventArgs e)
        {
            try
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
                if (e.Item.FieldName == "Date")
                {
                    e.Text = parsed;
                }
                if (e.Item.FieldName == "LedgerHead1")
                {
                    e.Text = parsed;
                }
                if (e.Item.FieldName == "LedgerHead2")
                {
                    e.Text = parsed;
                }
                if (e.Item.FieldName == "LedgerHead3")
                {
                    e.Text = parsed;
                }
                if (e.Item.FieldName == "LedgerHead4")
                {
                    e.Text = parsed;
                }
            }
            catch (Exception ex)
            {
                string stTemp = ex.Message;
            }

        }
        protected void gridPal3Customers_SummaryDisplayText(object sender, ASPxGridViewSummaryDisplayTextEventArgs e)
        {
            try
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
                if (e.Item.FieldName == "Date")
                {
                    e.Text = parsed;
                }
                if (e.Item.FieldName == "LedgerHead")
                {
                    e.Text = parsed;
                }
            }
            catch (Exception ex)
            {
                string stTemp = ex.Message;
            }

        }
        protected void Export_click(object sender, MenuItemEventArgs e)
        {
            if (e.Item.Text.ToString() == "PDF")
            {
                if (balayer.ToobjectstrEvenNull(Session["Branchid"]) == "161")
                {
                    using (this.headerImage = System.Drawing.Image.FromFile(Server.MapPath("~\\Images\\visalam.png")))
                    {
                        PrintingSystem ps = new PrintingSystem();
                        //PrintableComponentLink gridcheque = new PrintableComponentLink(ps);
                        //gridcheque.Component = gridPalCustomersExporter;
                        //gridPalCustomersExporter.PreserveGroupRowStates = true;
                        //gridPalCustomersExporter.PaperKind = System.Drawing.Printing.PaperKind.A4;
                        PrintableComponentLink gridcheque2 = new PrintableComponentLink(ps);
                        gridcheque2.Component = gridPal2CustomersExporter;
                        gridPal2CustomersExporter.PreserveGroupRowStates = true;
                        gridPal2CustomersExporter.PaperKind = System.Drawing.Printing.PaperKind.A4;
                        //PrintableComponentLink gridcheque3 = new PrintableComponentLink(ps);
                        //gridcheque3.Component = gridPal3CustomersExporter;
                        //gridPal3CustomersExporter.PreserveGroupRowStates = true;
                        //gridPal3CustomersExporter.PaperKind = System.Drawing.Printing.PaperKind.A4;
                        Link header = new Link();
                        CompositeLink compositeLink = new CompositeLink(ps);

                        header.CreateDetailArea += new CreateAreaEventHandler(header_CreateDetailArea);
                        //compositeLink.Links.AddRange(new object[] { header, gridcheque, gridcheque2, gridcheque3 });
                        compositeLink.Links.AddRange(new object[] { header,  gridcheque2 });

                        string leftColumn = "Pages : [Page # of Pages #]";
                        string rightColumn = "Date: [Date Printed]\r\nTime: [Time Printed]";
                        PageHeaderFooter phf = compositeLink.PageHeaderFooter as PageHeaderFooter;
                        phf.Footer.Content.Clear();
                        phf.Footer.Content.AddRange(new string[] { rightColumn, leftColumn });
                        phf.Footer.LineAlignment = BrickAlignment.Center;

                        using (MemoryStream stream = new MemoryStream())
                        {
                            compositeLink.PaperKind = System.Drawing.Printing.PaperKind.A4;
                            compositeLink.CreateDocument();
                            compositeLink.PrintingSystem.Document.AutoFitToPagesWidth = 1;
                            compositeLink.PrintingSystem.ExportToPdf(stream);
                            WriteToResponse("Booklet", true, "pdf", stream);
                        }
                    }
                }
                else
                {
                    using (this.headerImage = System.Drawing.Image.FromFile(Server.MapPath("~\\Images\\visalam.png")))
                    {
                        PrintingSystem ps = new PrintingSystem();
                        PrintableComponentLink gridcheque = new PrintableComponentLink(ps);
                        gridcheque.Component = gridExport;
                        gridExport.PreserveGroupRowStates = true;
                        gridExport.PaperKind = System.Drawing.Printing.PaperKind.A4;
                        Link header = new Link();
                        CompositeLink compositeLink = new CompositeLink(ps);

                        header.CreateDetailArea += new CreateAreaEventHandler(header_CreateDetailArea);
                        compositeLink.Links.AddRange(new object[] { header, gridcheque });

                        string leftColumn = "Pages : [Page # of Pages #]";
                        string rightColumn = "Date: [Date Printed]\r\nTime: [Time Printed]";
                        PageHeaderFooter phf = compositeLink.PageHeaderFooter as PageHeaderFooter;
                        phf.Footer.Content.Clear();
                        phf.Footer.Content.AddRange(new string[] { rightColumn, leftColumn });
                        phf.Footer.LineAlignment = BrickAlignment.Center;

                        using (MemoryStream stream = new MemoryStream())
                        {
                            compositeLink.PaperKind = System.Drawing.Printing.PaperKind.A4;
                            compositeLink.CreateDocument();
                            compositeLink.PrintingSystem.Document.AutoFitToPagesWidth = 1;
                            compositeLink.PrintingSystem.ExportToPdf(stream);
                            WriteToResponse("Booklet", true, "pdf", stream);
                        }
                    }
                }
            }
            else if (e.Item.Text.ToString() == "XLSX")
            {
                if (balayer.ToobjectstrEvenNull(Session["Branchid"]) == "161")
                {
                    //gridPalCustomersExporter.PreserveGroupRowStates = true;
                    //gridPalCustomersExporter.WriteXlsxToResponse();
                    gridPal2CustomersExporter.PreserveGroupRowStates = true;
                    gridPal2CustomersExporter.WriteXlsxToResponse();
                    //gridPal3CustomersExporter.PreserveGroupRowStates = true;
                    //gridPal3CustomersExporter.WriteXlsxToResponse();
                }
                else
                {
                    gridExport.PreserveGroupRowStates = true;
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
