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
using DevExpress.XtraPrinting;
using DevExpress.XtraPrintingLinks;
using System.IO;
using System.Drawing;
using SVCF_BusinessAccessLayer;
using SVCF_TransactionLayer;
using SVCF_DataAccessLayer;
using MySql.Data.MySqlClient;
namespace SreeVisalamChitFundLtd_phase1
{
    public partial class DirectBranchLedger : System.Web.UI.Page
    {
        #region VarDeclaration
        BusinessLayer balayer = new BusinessLayer();
        TransactionLayer tranlayer = new TransactionLayer();
        VarDeclaration objVar = new VarDeclaration();
        VarDeclaration1 objvar1 = new VarDeclaration1();
        VarDeclaration2 objvar2 = new VarDeclaration2();
        #endregion
        private System.Drawing.Image headerImage;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                String BrId = balayer.ToobjectstrEvenNull(balayer.ToobjectstrEvenNull(Session["Branchid"]));
                DataTable Accbank = new DataTable();
                MySqlConnection con;
                using (con = balayer.OpenConnection())
                {
                    try
                    {
                        MySqlDataAdapter mem2GrpAdp = new MySqlDataAdapter("select distinct t3.Node as Branch from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left join headstree as t8 on (t1.ChitGroupID=t8.NodeID) where `t1`.`BranchID` = " + balayer.ToobjectstrEvenNull(Session["Branchid"]) + "  and `t1`.`RootID` = 1 and t1.IsDeleted=0  order by t3.Node asc;", con);
                        mem2GrpAdp.Fill(Accbank);
                        //DataRow dr = ChitGrp.NewRow();
                        //dr[0] = "--select--";
                        //ChitGrp.Rows.InsertAt(dr, 0);
                        ddlbranch.Items.Insert(0, "--select--");
                        for (int i = 0; i < Accbank.Rows.Count; i++)
                        {
                            ddlbranch.Items.Add(balayer.ToobjectstrEvenNull(Accbank.Rows[i]["Branch"]));
                        }
                    }
                    catch (Exception ex)
                    {

                    }
                }

                //chkBox.Checked = true;
                ddlGroup.SelectedIndex = 1;
                dateFromConsolidated.Text = DateTime.Now.ToString("dd/MM/yyyy");
                dateToConsolidated.Text = DateTime.Now.ToString("dd/MM/yyyy");
                select(ddlbranch.SelectedIndex);
                grdTemp.Visible = false;
                setStats(dateFromConsolidated.Text, dateToConsolidated.Text);
                ApplyLayout(1);
                foreach (GridViewColumn column in grid.Columns)
                {
                    if (column is GridViewDataColumn)
                    {
                        ((GridViewDataColumn)column).Settings.AutoFilterCondition = AutoFilterCondition.Contains;
                    }
                }

                foreach (GridViewColumn column in grid_pallathur1.Columns)
                {
                    if (column is GridViewDataColumn)
                    {
                        ((GridViewDataColumn)column).Settings.AutoFilterCondition = AutoFilterCondition.Contains;
                    }
                }

                foreach (GridViewColumn column in grid_pallathur2.Columns)
                {
                    if (column is GridViewDataColumn)
                    {
                        ((GridViewDataColumn)column).Settings.AutoFilterCondition = AutoFilterCondition.Contains;
                    }
                }
            }
            select(ddlbranch.SelectedIndex);
            grid.DataSource = LoadGrid();
            grid.DataBind();
        }
        protected void ResetStats()
        {
            if (Convert.ToInt32(Session["Branchid"]) == 161)
            {
                Panel_P1.Visible = true;
                Panel_P2.Visible = true;
                Pallathur_3.Visible = true;

                lblCrAmount_pallathur1.Text = "0.00";
                lblCrBalance_pallathur1.Text = "0.00";
                lblDrAmount_pallathur1.Text = "0.00";
                lblDrBalance_pallathur1.Text = "0.00";

                lblCrAmount_pallathur2.Text = "0.00";
                lblCrBalance_pallathur2.Text = "0.00";
                lblDrAmount_pallathur2.Text = "0.00";
                lblDrBalance_pallathur2.Text = "0.00";

                lblCrAmount.Text = "0.00";
                lblCrBalance.Text = "0.00";
                lblDrAmount.Text = "0.00";
                lblDrBalance.Text = "0.00";
            }
            else
            {
                Panel_P1.Visible = false;
                Panel_P2.Visible = false;
                Pallathur_3.Visible = false;

                lblCrAmount.Text = "0.00";
                lblCrBalance.Text = "0.00";
                lblDrAmount.Text = "0.00";
                lblDrBalance.Text = "0.00";
            }
        }

        protected void setStats(string Fromdate, string Todate)
        {
            ResetStats();

            if (Convert.ToInt32(Session["Branchid"]) == 161)
            {
                DataTable dtStats_P1 = balayer.GetDataTable(" SELECT RootID,sum((case when Voucher_Type='C' then Amount else 0.00 end ))as `Credit Amount`,sum((case when Voucher_Type='D' then Amount else 0.00 end ))as `Debit Amount` FROM `svcf`.`voucher` where BranchID=160 and ChoosenDate between '" + balayer.indiandateToMysqlDate(Fromdate) + "' and '" + balayer.indiandateToMysqlDate(Todate) + "' and RootID=1 group by RootID");
                if (dtStats_P1.Rows.Count == 1)
                {
                    decimal decCredit = Convert.ToDecimal(dtStats_P1.Rows[0]["Credit Amount"].ToString());
                    decimal decDebit = Convert.ToDecimal(dtStats_P1.Rows[0]["Debit Amount"].ToString());
                    decimal decDebitBalance = 0.00M;
                    decimal decCreditBalance = 0.00M;
                    decimal.TryParse(balayer.ToobjectstrEvenNull(dtStats_P1.Rows[0][1].ToString()), out decCredit);
                    decimal.TryParse(balayer.ToobjectstrEvenNull(dtStats_P1.Rows[0][2].ToString()), out decDebit);
                    if ((decCredit - decDebit) > 0.00M)
                    {
                        decCreditBalance = decCredit - decDebit;
                    }
                    else if ((decDebit - decCredit) > 0.00M)
                    {
                        decDebitBalance = decDebit - decCredit;
                    }
                    lblCrAmount_pallathur1.Text = decCredit.ToString();
                    lblCrBalance_pallathur1.Text = decCreditBalance.ToString();
                    lblDrAmount_pallathur1.Text = decDebit.ToString();
                    lblDrBalance_pallathur1.Text = decDebitBalance.ToString();
                }

                DataTable dtStats_P2 = balayer.GetDataTable(" SELECT RootID,sum((case when Voucher_Type='C' then Amount else 0.00 end ))as `Credit Amount`,sum((case when Voucher_Type='D' then Amount else 0.00 end ))as `Debit Amount` FROM `svcf`.`voucher` where BranchID=161 and ChoosenDate between '" + balayer.indiandateToMysqlDate(Fromdate) + "' and '" + balayer.indiandateToMysqlDate(Todate) + "' and RootID=1 group by RootID");
                if (dtStats_P2.Rows.Count == 1)
                {
                    decimal decCredit = Convert.ToDecimal(dtStats_P2.Rows[0]["Credit Amount"].ToString());
                    decimal decDebit = Convert.ToDecimal(dtStats_P2.Rows[0]["Debit Amount"].ToString());
                    decimal decDebitBalance = 0.00M;
                    decimal decCreditBalance = 0.00M;
                    decimal.TryParse(balayer.ToobjectstrEvenNull(dtStats_P2.Rows[0][1].ToString()), out decCredit);
                    decimal.TryParse(balayer.ToobjectstrEvenNull(dtStats_P2.Rows[0][2].ToString()), out decDebit);
                    if ((decCredit - decDebit) > 0.00M)
                    {
                        decCreditBalance = decCredit - decDebit;
                    }
                    else if ((decDebit - decCredit) > 0.00M)
                    {
                        decDebitBalance = decDebit - decCredit;
                    }
                    lblCrAmount_pallathur2.Text = decCredit.ToString();
                    lblCrBalance_pallathur2.Text = decCreditBalance.ToString();
                    lblDrAmount_pallathur2.Text = decDebit.ToString();
                    lblDrBalance_pallathur2.Text = decDebitBalance.ToString();
                }

                DataTable dtStats_P3 = balayer.GetDataTable(" SELECT RootID,sum((case when Voucher_Type='C' then Amount else 0.00 end ))as `Credit Amount`,sum((case when Voucher_Type='D' then Amount else 0.00 end ))as `Debit Amount` FROM `svcf`.`voucher` where BranchID=161 and ChoosenDate between '" + balayer.indiandateToMysqlDate(Fromdate) + "' and '" + balayer.indiandateToMysqlDate(Todate) + "' and RootID=1 group by RootID");
                if (dtStats_P3.Rows.Count == 1)
                {
                    decimal decCredit = Convert.ToDecimal(dtStats_P3.Rows[0]["Credit Amount"].ToString());
                    decimal decDebit = Convert.ToDecimal(dtStats_P3.Rows[0]["Debit Amount"].ToString());
                    decimal decDebitBalance = 0.00M;
                    decimal decCreditBalance = 0.00M;
                    decimal.TryParse(balayer.ToobjectstrEvenNull(dtStats_P3.Rows[0][1].ToString()), out decCredit);
                    decimal.TryParse(balayer.ToobjectstrEvenNull(dtStats_P3.Rows[0][2].ToString()), out decDebit);
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
            else
            {
                DataTable dtStats = balayer.GetDataTable(" SELECT RootID,sum((case when Voucher_Type='C' then Amount else 0.00 end ))as `Credit Amount`,sum((case when Voucher_Type='D' then Amount else 0.00 end ))as `Debit Amount` FROM `svcf`.`voucher` where BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and ChoosenDate between '" + balayer.indiandateToMysqlDate(Fromdate) + "' and '" + balayer.indiandateToMysqlDate(Todate) + "' and RootID=1 group by RootID");
                if (dtStats.Rows.Count == 1)
                {
                    decimal decCredit = Convert.ToDecimal(dtStats.Rows[0]["Credit Amount"].ToString());
                    decimal decDebit = Convert.ToDecimal(dtStats.Rows[0]["Debit Amount"].ToString());
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
        }

        protected void select(int id)
        {
            if (Convert.ToInt32(Session["Branchid"]) == 161)
            {
                gridprev_pallathur1.Visible = true;
                grid_pallathur1.Visible = true;
                gridprev_pallathur2.Visible = true;
                grid_pallathur2.Visible = true;
                AccessDataSource3.ConnectionString = CommonClassFile.ConnectionString;
                AccessDataSource3.SelectCommand = @"select t1.ChoosenDate as `Date`,t8.Node as LedgerHead ,t3.Node as Branch , t1.Narration,(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )as `Credit`,(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )as `Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left join headstree as t8 on (t1.ChitGroupID=t8.NodeID) where `t1`.`BranchID` =160 and `t1`.`RootID` = 1 and t1.ChoosenDate between '" + balayer.indiandateToMysqlDate(dateFromConsolidated.Text) + "' and '" + balayer.indiandateToMysqlDate(dateToConsolidated.Text) + "' and t1.IsDeleted=0 order by t1.ChoosenDate asc";
                grid_pallathur1.DataBind();
                AccessDataSource4.ConnectionString = CommonClassFile.ConnectionString;
                AccessDataSource4.SelectCommand = @"select t1.ChoosenDate as `Date`,t8.Node as LedgerHead ,t3.Node as Branch , t1.Narration,(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )as `Credit`,(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )as `Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left join headstree as t8 on (t1.ChitGroupID=t8.NodeID) where `t1`.`BranchID` =160 and `t1`.`RootID` = 1 and t1.ChoosenDate < '" + balayer.indiandateToMysqlDate(dateFromConsolidated.Text) + "' and t1.IsDeleted=0 order by t1.ChoosenDate asc";
                gridprev_pallathur1.DataBind();

                AccessDataSource5.ConnectionString = CommonClassFile.ConnectionString;
                AccessDataSource5.SelectCommand = @"select t1.ChoosenDate as `Date`,t8.Node as LedgerHead ,t3.Node as Branch , t1.Narration,(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )as `Credit`,(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )as `Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left join headstree as t8 on (t1.ChitGroupID=t8.NodeID) where `t1`.`BranchID` =161 and `t1`.`RootID` = 1 and t1.ChoosenDate between '" + balayer.indiandateToMysqlDate(dateFromConsolidated.Text) + "' and '" + balayer.indiandateToMysqlDate(dateToConsolidated.Text) + "' and t1.IsDeleted=0 order by t1.ChoosenDate asc";
                grid_pallathur2.DataBind();
                AccessDataSource6.ConnectionString = CommonClassFile.ConnectionString;
                AccessDataSource6.SelectCommand = @"select t1.ChoosenDate as `Date`,t8.Node as LedgerHead ,t3.Node as Branch , t1.Narration,(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )as `Credit`,(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )as `Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left join headstree as t8 on (t1.ChitGroupID=t8.NodeID) where `t1`.`BranchID` =161 and `t1`.`RootID` = 1 and t1.ChoosenDate < '" + balayer.indiandateToMysqlDate(dateFromConsolidated.Text) + "' and t1.IsDeleted=0 order by t1.ChoosenDate asc";
                gridprev_pallathur2.DataBind();

                AccessDataSource1.ConnectionString = CommonClassFile.ConnectionString;
                AccessDataSource1.SelectCommand = @"select t1.ChoosenDate as `Date`,t8.Node as LedgerHead ,t3.Node as Branch , t1.Narration,(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )as `Credit`,(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )as `Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left join headstree as t8 on (t1.ChitGroupID=t8.NodeID) where `t1`.`BranchID` =162 and `t1`.`RootID` = 1 and t1.ChoosenDate between '" + balayer.indiandateToMysqlDate(dateFromConsolidated.Text) + "' and '" + balayer.indiandateToMysqlDate(dateToConsolidated.Text) + "' and t1.IsDeleted=0 order by t1.ChoosenDate asc";
                grid.DataBind();
                AccessDataSource2.ConnectionString = CommonClassFile.ConnectionString;
                AccessDataSource2.SelectCommand = @"select t1.ChoosenDate as `Date`,t8.Node as LedgerHead ,t3.Node as Branch , t1.Narration,(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )as `Credit`,(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )as `Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left join headstree as t8 on (t1.ChitGroupID=t8.NodeID) where `t1`.`BranchID` =162 and `t1`.`RootID` = 1 and t1.ChoosenDate < '" + balayer.indiandateToMysqlDate(dateFromConsolidated.Text) + "' and t1.IsDeleted=0 order by t1.ChoosenDate asc";
                gridprev.DataBind();
            }
            else
            {
                gridprev_pallathur1.Visible = false;
                grid_pallathur1.Visible = false;
                gridprev_pallathur2.Visible = false;
                grid_pallathur2.Visible = false;
                if (id == 0)
                {
                    string value = ddlbranch.SelectedItem.Text;
                    grid.SettingsText.Title = "Trial Balance of  Branche Ledger  from " + dateFromConsolidated.Text + " to " + dateToConsolidated.Text;
                    grid.SettingsText.Title = "Trial Balance of  Branche Ledger  from " + dateFromConsolidated.Text + " to " + dateToConsolidated.Text;
                    AccessDataSource1.ConnectionString = CommonClassFile.ConnectionString;
                    AccessDataSource1.SelectCommand = @"select t1.ChoosenDate as `Date`,t8.Node as LedgerHead ,t3.Node as Branch , t1.Narration,(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )as `Credit`,(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )as `Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left join headstree as t8 on (t1.ChitGroupID=t8.NodeID) where `t1`.`BranchID` =" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and `t1`.`RootID` = 1 and t1.ChoosenDate between '" + balayer.indiandateToMysqlDate(dateFromConsolidated.Text) + "' and '" + balayer.indiandateToMysqlDate(dateToConsolidated.Text) + "' and t1.IsDeleted=0 order by t1.ChoosenDate asc";
                    grid.DataBind();
                    AccessDataSource2.ConnectionString = CommonClassFile.ConnectionString;
                    AccessDataSource2.SelectCommand = @"select t1.ChoosenDate as `Date`,t8.Node as LedgerHead ,t3.Node as Branch , t1.Narration,(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )as `Credit`,(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )as `Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left join headstree as t8 on (t1.ChitGroupID=t8.NodeID) where `t1`.`BranchID` =" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and `t1`.`RootID` = 1 and t1.ChoosenDate < '" + balayer.indiandateToMysqlDate(dateFromConsolidated.Text) + "' and t1.IsDeleted=0 order by t1.ChoosenDate asc";
                    gridprev.DataBind();
                }
                else
                {
                    //string value = ddlbranch.SelectedItem.Text;
                    string value = ddlbranch.SelectedItem.Text;
                    grid.SettingsText.Title = "Trial Balance of " + value + " Branche Ledger  from " + dateFromConsolidated.Text + " to " + dateToConsolidated.Text;
                    AccessDataSource1.ConnectionString = CommonClassFile.ConnectionString;
                    AccessDataSource1.SelectCommand = @"select t1.ChoosenDate as `Date`,t8.Node as LedgerHead ,t3.Node as Branch , t1.Narration,(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )as `Credit`,(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )as `Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left join headstree as t8 on (t1.ChitGroupID=t8.NodeID) where `t1`.`BranchID` =" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and `t1`.`RootID` = 1 and t1.ChoosenDate between '" + balayer.indiandateToMysqlDate(dateFromConsolidated.Text) + "' and '" + balayer.indiandateToMysqlDate(dateToConsolidated.Text) + "' and t1.IsDeleted=0 and t3.Node='" + value + "' order by t1.ChoosenDate asc";
                    grid.DataBind();
                    AccessDataSource2.ConnectionString = CommonClassFile.ConnectionString;
                    AccessDataSource2.SelectCommand = @"select t1.ChoosenDate as `Date`,t8.Node as LedgerHead ,t3.Node as Branch , t1.Narration,(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )as `Credit`,(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )as `Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left join headstree as t8 on (t1.ChitGroupID=t8.NodeID) where `t1`.`BranchID` =" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and `t1`.`RootID` = 1 and t1.ChoosenDate < '" + balayer.indiandateToMysqlDate(dateFromConsolidated.Text) + "' and t1.IsDeleted=0 and t3.Node='" + value + "' order by t1.ChoosenDate asc";
                    gridprev.DataBind();
                }
            }

        }
        protected void oncheck_load(object sender, EventArgs e)
        {
        }
        protected void ddlGroup_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlGroup.SelectedIndex == 0)
            {
                //select(ddlbranch.SelectedIndex);
                ApplyLayout(0);
                grid.TotalSummary["Credit"].SummaryType = SummaryItemType.Sum;
                grid.TotalSummary["Debit"].SummaryType = SummaryItemType.Sum;
            }
            else if (ddlGroup.SelectedIndex == 1)
            {
                //select(ddlbranch.SelectedIndex);
                ApplyLayout(1);
                grid.TotalSummary["Credit"].SummaryType = SummaryItemType.Custom;
                grid.TotalSummary["Debit"].SummaryType = SummaryItemType.Custom;
            }
            else
            {
                //select(ddlbranch.SelectedIndex);
                ApplyLayout(2);
                grid.TotalSummary["Credit"].SummaryType = SummaryItemType.Custom;
                grid.TotalSummary["Debit"].SummaryType = SummaryItemType.Custom;
            }
        }
        protected void btnExportExcel_Click1(object sender, EventArgs e)
        {
            gridexcel.FileName = "Direct Branch" + DateTime.Now.Millisecond.ToString();
            grid.DataSource = LoadGrid();
            grid.DataBind();
            gridexcel.WriteXlsToResponse();
        }
        protected void ASPxGridView1_SummaryDisplayText(object sender, ASPxGridViewSummaryDisplayTextEventArgs e)
        {
            if (!(e.Value == null))
            {
                string parsed = balayer.ConvertToIndCurrency(e.Value == null ? "0" : e.Value.ToString());
                if (e.Item.FieldName == "Credit")
                {
                 //   e.Text = "Cr. " + parsed;
                    e.Text =  parsed;
                }
                if (e.Item.FieldName == "Debit")
                {
              //      e.Text = "Dr. " + parsed;
                    e.Text = parsed;
                }
                if (e.Item.FieldName == "Narration")
                {
                    e.Text = e.Item.DisplayFormat.ToString() + parsed;
                }
                if (e.Item.FieldName == "Date")
                {
                    e.Text = parsed;
                }
                if (e.Item.FieldName == "Token")
                {
                    e.Text = parsed;
                }
                if (e.Item.FieldName == "LedgerHead")
                {
                    //  decimal creditAmount = Convert.ToDecimal(grid.GetTotalSummaryValue(grid.TotalSummary["Credit"]));
                    // decimal debitAmount = Convert.ToDecimal(grid.GetTotalSummaryValue(grid.TotalSummary["Debit"]));
                    // decimal colCreditAmount = Convert.ToDecimal(gridprev.GetTotalSummaryValue(gridprev.TotalSummary["Credit"]));
                    // decimal colDebitAmount = Convert.ToDecimal(gridprev.GetTotalSummaryValue(gridprev.TotalSummary["Debit"]));
                    var crtotal = Convert.ToDecimal(grid.GetTotalSummaryValue(grid.TotalSummary["Credit"])) + Convert.ToDecimal(gridprev.GetTotalSummaryValue(gridprev.TotalSummary["Credit"]));
                    var drTotal = Convert.ToDecimal(grid.GetTotalSummaryValue(grid.TotalSummary["Debit"])) + Convert.ToDecimal(gridprev.GetTotalSummaryValue(gridprev.TotalSummary["Debit"]));
                    decimal gridprevAmount = (crtotal < drTotal) ? gridprevAmount = drTotal - crtotal : gridprevAmount = crtotal - drTotal;
                    e.Text = Convert.ToString("Total:₹" + (gridprevAmount));

                }
            }
        }

        protected void grid_pallathur1_SummaryDisplayText(object sender, ASPxGridViewSummaryDisplayTextEventArgs e)
        {
            if (!(e.Value == null))
            {
                string parsed = balayer.ConvertToIndCurrency(e.Value == null ? "0" : e.Value.ToString());
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
                if (e.Item.FieldName == "Token")
                {
                    e.Text = parsed;
                }
            }
        }

        protected void grid_pallathur2_SummaryDisplayText(object sender, ASPxGridViewSummaryDisplayTextEventArgs e)
        {
            if (!(e.Value == null))
            {
                string parsed = balayer.ConvertToIndCurrency(e.Value == null ? "0" : e.Value.ToString());
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
                if (e.Item.FieldName == "Token")
                {
                    e.Text = parsed;
                }
            }
        }

        protected void ddlbranch_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlbranch.SelectedIndex != 0)
            {

                select(ddlbranch.SelectedIndex);

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
                                total += Convert.ToDecimal(grid.GetGroupSummaryValue(i, grid.GroupSummary["Branch"]));
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

                   

                    //string ion = "";
                    //var iii = "";
                    //if (cre  > 0)
                    //{
                    //    objVar.varquery = "select (case when sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end ) > sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) then CONCAT('Cr.Bal- ',CAST(sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end ) - sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )AS char(50))) else  CONCAT('Dr.Bal-',CAST(sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) - sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )AS char(50)))  end)  as Narration from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left join headstree as t8 on (t1.ChitGroupID=t8.NodeID) where `t1`.`BranchID` =  " + balayer.ToobjectstrEvenNull(Session["Branchid"]) + "   and `t1`.`RootID` = 1 and t1.ChoosenDate < '" + balayer.indiandateToMysqlDate(dateFromConsolidated.Text) + "' and t1.IsDeleted=0 order by t1.ChoosenDate asc;";
                    //    ion = balayer.GetSingleValue(objVar.varquery);
                    //    //  iii = ion.Split('-')[1];

                    //    if (ddlbranch.SelectedIndex != 0)
                    //    {
                    //        string value = ddlbranch.SelectedItem.Text;

                    //        objVar.varquery = "select (case when sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end ) > sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) then CONCAT('Cr.Bal- ',CAST(sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end ) - sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )AS char(50))) else  CONCAT('Dr.Bal-',CAST(sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) - sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )AS char(50)))  end)  as Narration from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left join headstree as t8 on (t1.ChitGroupID=t8.NodeID) where `t1`.`BranchID` =  " + balayer.ToobjectstrEvenNull(Session["Branchid"]) + "   and `t1`.`RootID` = 1 and t1.ChoosenDate < '" + balayer.indiandateToMysqlDate(dateFromConsolidated.Text) + "' and t1.IsDeleted=0 and t3.Node='" + value + "' order by t1.ChoosenDate asc;";
                    //        // objVar.varquery = "select 'Previous Net Balance' as 'Title',(case when sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end ) > sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) then CONCAT('Cr.Bal. ',CAST(sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end ) - sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )AS char(50))) else  CONCAT('Dr.Bal.',CAST(sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) - sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )AS char(50)))  end)  as Narration, CONCAT('Cr. INR ', CAST(sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )AS char(50))) as `Credit`,CONCAT('Dr. INR ',CAST(sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )AS char(50))) as `Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left join headstree as t8 on (t1.ChitGroupID=t8.NodeID) where `t1`.`BranchID` =" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + "  and `t1`.`RootID` = 1 and t1.ChoosenDate < '" + balayer.indiandateToMysqlDate(dateFromConsolidated.Text) + "' and t1.IsDeleted=0 order by t1.ChoosenDate asc;";
                    //        ion = balayer.GetSingleValue(objVar.varquery);
                    //        if (ion == "")
                    //        {
                    //            iii = "0.00";
                    //        }
                    //        else
                    //        {
                    //            iii = ion.Split('-')[1];
                    //        }
                    //    }
                    //    else
                    //    {
                    //        objVar.varquery = "select (case when sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end ) > sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) then CONCAT('Cr.Bal- ',CAST(sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end ) - sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )AS char(50))) else  CONCAT('Dr.Bal-',CAST(sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) - sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )AS char(50)))  end)  as Narration from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left join headstree as t8 on (t1.ChitGroupID=t8.NodeID) where `t1`.`BranchID` =  " + balayer.ToobjectstrEvenNull(Session["Branchid"]) + "   and `t1`.`RootID` = 1 and t1.ChoosenDate < '" + balayer.indiandateToMysqlDate(dateFromConsolidated.Text) + "' and t1.IsDeleted=0 order by t1.ChoosenDate asc;";
                    //        // objVar.varquery = "select 'Previous Net Balance' as 'Title',(case when sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end ) > sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) then CONCAT('Cr.Bal. ',CAST(sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end ) - sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )AS char(50))) else  CONCAT('Dr.Bal.',CAST(sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) - sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )AS char(50)))  end)  as Narration, CONCAT('Cr. INR ', CAST(sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )AS char(50))) as `Credit`,CONCAT('Dr. INR ',CAST(sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )AS char(50))) as `Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left join headstree as t8 on (t1.ChitGroupID=t8.NodeID) where `t1`.`BranchID` =" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + "  and `t1`.`RootID` = 1 and t1.ChoosenDate < '" + balayer.indiandateToMysqlDate(dateFromConsolidated.Text) + "' and t1.IsDeleted=0 order by t1.ChoosenDate asc;";
                    //        ion = balayer.GetSingleValue(objVar.varquery);
                    //        if (ion == "")
                    //        {
                    //            iii = "0.00";
                    //        }
                    //        else
                    //        {
                    //            iii = ion.Split('-')[1];
                    //        }
                    //    }
                    //}
                    //else
                    //{
                    //    objVar.varquery = "select (case when sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end ) > sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) then CONCAT('Cr.Bal- ',CAST(sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end ) - sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )AS char(50))) else  CONCAT('Dr.Bal-',CAST(sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) - sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )AS char(50)))  end)  as Narration from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left join headstree as t8 on (t1.ChitGroupID=t8.NodeID) where `t1`.`BranchID` =  " + balayer.ToobjectstrEvenNull(Session["Branchid"]) + "   and `t1`.`RootID` = 1 and t1.ChoosenDate < '" + balayer.indiandateToMysqlDate(dateFromConsolidated.Text) + "' and t1.IsDeleted=0 order by t1.ChoosenDate asc;";
                    //    ion = balayer.GetSingleValue(objVar.varquery);
                    //    //  iii = ion.Split('-')[1];

                    //    if (ddlbranch.SelectedIndex != 0)
                    //    {
                    //        string value = ddlbranch.SelectedItem.Text;

                    //        objVar.varquery = "select (case when sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end ) > sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) then CONCAT('Cr.Bal- ',CAST(sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end ) - sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )AS char(50))) else  CONCAT('Dr.Bal-',CAST(sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) - sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )AS char(50)))  end)  as Narration from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left join headstree as t8 on (t1.ChitGroupID=t8.NodeID) where `t1`.`BranchID` =  " + balayer.ToobjectstrEvenNull(Session["Branchid"]) + "   and `t1`.`RootID` = 1 and t1.ChoosenDate < '" + balayer.indiandateToMysqlDate(dateFromConsolidated.Text) + "' and t1.IsDeleted=0 and t3.Node='" + value + "' order by t1.ChoosenDate asc;";
                    //        // objVar.varquery = "select 'Previous Net Balance' as 'Title',(case when sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end ) > sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) then CONCAT('Cr.Bal. ',CAST(sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end ) - sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )AS char(50))) else  CONCAT('Dr.Bal.',CAST(sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) - sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )AS char(50)))  end)  as Narration, CONCAT('Cr. INR ', CAST(sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )AS char(50))) as `Credit`,CONCAT('Dr. INR ',CAST(sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )AS char(50))) as `Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left join headstree as t8 on (t1.ChitGroupID=t8.NodeID) where `t1`.`BranchID` =" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + "  and `t1`.`RootID` = 1 and t1.ChoosenDate < '" + balayer.indiandateToMysqlDate(dateFromConsolidated.Text) + "' and t1.IsDeleted=0 order by t1.ChoosenDate asc;";
                    //        ion = balayer.GetSingleValue(objVar.varquery);
                    //        if (ion == "")
                    //        {
                    //            iii = "0.00";
                    //        }
                    //        else
                    //        {
                    //            iii = ion.Split('-')[1];
                    //        }
                    //    }
                    //    else
                    //    {
                    //        objVar.varquery = "select (case when sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end ) > sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) then CONCAT('Cr.Bal- ',CAST(sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end ) - sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )AS char(50))) else  CONCAT('Dr.Bal-',CAST(sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) - sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )AS char(50)))  end)  as Narration from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left join headstree as t8 on (t1.ChitGroupID=t8.NodeID) where `t1`.`BranchID` =  " + balayer.ToobjectstrEvenNull(Session["Branchid"]) + "   and `t1`.`RootID` = 1 and t1.ChoosenDate < '" + balayer.indiandateToMysqlDate(dateFromConsolidated.Text) + "' and t1.IsDeleted=0 order by t1.ChoosenDate asc;";
                    //        // objVar.varquery = "select 'Previous Net Balance' as 'Title',(case when sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end ) > sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) then CONCAT('Cr.Bal. ',CAST(sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end ) - sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )AS char(50))) else  CONCAT('Dr.Bal.',CAST(sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) - sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )AS char(50)))  end)  as Narration, CONCAT('Cr. INR ', CAST(sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )AS char(50))) as `Credit`,CONCAT('Dr. INR ',CAST(sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )AS char(50))) as `Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left join headstree as t8 on (t1.ChitGroupID=t8.NodeID) where `t1`.`BranchID` =" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + "  and `t1`.`RootID` = 1 and t1.ChoosenDate < '" + balayer.indiandateToMysqlDate(dateFromConsolidated.Text) + "' and t1.IsDeleted=0 order by t1.ChoosenDate asc;";
                    //        ion = balayer.GetSingleValue(objVar.varquery);
                    //        if (ion == "")
                    //        {
                    //            iii = "0.00";
                    //        }
                    //        else
                    //        {
                    //            iii = ion.Split('-')[1];
                    //        }
                    //    }
                    //}
                  
                    if (cre > deb)
                    {
                        var val1 = cre - deb;
                        e.TotalValue = val1;
                        x.DisplayFormat = "NetBalance Cr";

                        //if (ion.Contains("Cr"))
                        //{
                        //    var val = Convert.ToDecimal(iii);
                        //    var val1 = cre - deb;
                        //    //x.DisplayFormat = "Cr.Bal.";
                        //    x.DisplayFormat = "NetBalance Cr";
                        //    e.TotalValue = val + val1;
                        //}

                       
                        e.TotalValueReady = true;

                    }
                    else if (cre < deb)
                    {
                        
                        var val1 = deb - cre;
                        x.DisplayFormat = "Dr.Bal.";
                        e.TotalValue = val1;

                        //if (ion.Contains("Dr"))
                        //{
                        //    var val = Convert.ToDecimal(iii);
                        //    var val1 = deb - cre;
                        //    x.DisplayFormat = "Dr.Bal.";
                        //    e.TotalValue = val + val1;
                        //}

                        //else if (ion.Contains("Cr"))
                        //{
                        //    var val = Convert.ToDecimal(iii);
                        //    var val1 = deb - cre;
                        //    if (val > val1)
                        //    {
                        //        x.DisplayFormat = "Cr.Bal.";
                        //        e.TotalValue = val - val1;
                        //    }
                        //    else if (val < val1)
                        //    {
                        //        x.DisplayFormat = "Dr.Bal.";
                        //        e.TotalValue = val1 - val;
                        //    }
                        //    else
                        //    {
                        //        e.TotalValue = 0.00;
                        //    }


                        //}
                        //else if (ion == "")
                        //{
                        //    var val1 = deb - cre;
                        //    x.DisplayFormat = "Dr.Bal.";
                        //    e.TotalValue = val1;
                        //}

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
                        //decimal ss = Convert.ToDecimal(e.TotalValue);
                        //ViewState["Debit"] = Convert.ToDecimal( ViewState["Debit"])+ ss;
                    }
                }
                if (x.FieldName.ToString() == "Branch")
                {
                    decimal Credit1 = Convert.ToDecimal(e.GetGroupSummary(e.GroupRowHandle, grid.GroupSummary["Credit"]));
                    decimal Debit1 = Convert.ToDecimal(e.GetGroupSummary(e.GroupRowHandle, grid.GroupSummary["Debit"]));
                    if (Credit1 >= Debit1)
                    {
                        e.TotalValue = Credit1 - Debit1;
                        //decimal ss = Convert.ToDecimal(e.TotalValue);
                        //ViewState["Credit"] = Convert.ToDecimal(ViewState["Credit"]) + ss;
                    }
                }
            }
        }

        protected void grid_pallathur1_OnCustomSummaryCalculate(object sender, CustomSummaryEventArgs e)
        {
            if (e.IsTotalSummary)
            {
                ASPxSummaryItem x = (ASPxSummaryItem)e.Item;
                if (x.FieldName.ToString() == "Debit")
                {
                    if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Finalize)
                    {
                        decimal total = 0.00M;
                        for (int i = 0; i < grid_pallathur1.VisibleRowCount; i++)
                        {
                            if (grid_pallathur1.IsGroupRow(i))
                            {
                                total += Convert.ToDecimal(grid_pallathur1.GetGroupSummaryValue(i, grid_pallathur1.GroupSummary["Date"]));
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
                        for (int i = 0; i < grid_pallathur1.VisibleRowCount; i++)
                        {
                            if (grid_pallathur1.IsGroupRow(i))
                            {
                                total += Convert.ToDecimal(grid_pallathur1.GetGroupSummaryValue(i, grid_pallathur1.GroupSummary["Branch"]));
                            }
                        }
                        e.TotalValue = total;
                        e.TotalValueReady = true;
                    }
                }
                if (x.FieldName.ToString() == "Narration")
                {
                    decimal cre = Convert.ToDecimal(grid_pallathur1.GetTotalSummaryValue(grid_pallathur1.TotalSummary["Credit"]));
                    decimal deb = Convert.ToDecimal(grid_pallathur1.GetTotalSummaryValue(grid_pallathur1.TotalSummary["Debit"]));
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
                    decimal Credit1 = Convert.ToDecimal(e.GetGroupSummary(e.GroupRowHandle, grid_pallathur1.GroupSummary["Credit"]));
                    decimal Debit1 = Convert.ToDecimal(e.GetGroupSummary(e.GroupRowHandle, grid_pallathur1.GroupSummary["Debit"]));
                    if (Credit1 <= Debit1)
                    {
                        e.TotalValue = Debit1 - Credit1;
                        //decimal ss = Convert.ToDecimal(e.TotalValue);
                        //ViewState["Debit"] = Convert.ToDecimal( ViewState["Debit"])+ ss;
                    }
                }
                if (x.FieldName.ToString() == "Branch")
                {
                    decimal Credit1 = Convert.ToDecimal(e.GetGroupSummary(e.GroupRowHandle, grid_pallathur1.GroupSummary["Credit"]));
                    decimal Debit1 = Convert.ToDecimal(e.GetGroupSummary(e.GroupRowHandle, grid_pallathur1.GroupSummary["Debit"]));
                    if (Credit1 >= Debit1)
                    {
                        e.TotalValue = Credit1 - Debit1;
                        //decimal ss = Convert.ToDecimal(e.TotalValue);
                        //ViewState["Credit"] = Convert.ToDecimal(ViewState["Credit"]) + ss;
                    }
                }
            }
        }

        protected void grid_pallathur2_OnCustomSummaryCalculate(object sender, CustomSummaryEventArgs e)
        {
            if (e.IsTotalSummary)
            {
                ASPxSummaryItem x = (ASPxSummaryItem)e.Item;
                if (x.FieldName.ToString() == "Debit")
                {
                    if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Finalize)
                    {
                        decimal total = 0.00M;
                        for (int i = 0; i < grid_pallathur2.VisibleRowCount; i++)
                        {
                            if (grid_pallathur2.IsGroupRow(i))
                            {
                                total += Convert.ToDecimal(grid_pallathur2.GetGroupSummaryValue(i, grid_pallathur2.GroupSummary["Date"]));
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
                        for (int i = 0; i < grid_pallathur2.VisibleRowCount; i++)
                        {
                            if (grid_pallathur2.IsGroupRow(i))
                            {
                                total += Convert.ToDecimal(grid_pallathur2.GetGroupSummaryValue(i, grid_pallathur2.GroupSummary["Branch"]));
                            }
                        }
                        e.TotalValue = total;
                        e.TotalValueReady = true;
                    }
                }
                if (x.FieldName.ToString() == "Narration")
                {
                    decimal cre = Convert.ToDecimal(grid_pallathur2.GetTotalSummaryValue(grid_pallathur2.TotalSummary["Credit"]));
                    decimal deb = Convert.ToDecimal(grid_pallathur2.GetTotalSummaryValue(grid_pallathur2.TotalSummary["Debit"]));
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
                    decimal Credit1 = Convert.ToDecimal(e.GetGroupSummary(e.GroupRowHandle, grid_pallathur2.GroupSummary["Credit"]));
                    decimal Debit1 = Convert.ToDecimal(e.GetGroupSummary(e.GroupRowHandle, grid_pallathur2.GroupSummary["Debit"]));
                    if (Credit1 <= Debit1)
                    {
                        e.TotalValue = Debit1 - Credit1;
                        //decimal ss = Convert.ToDecimal(e.TotalValue);
                        //ViewState["Debit"] = Convert.ToDecimal( ViewState["Debit"])+ ss;
                    }
                }
                if (x.FieldName.ToString() == "Branch")
                {
                    decimal Credit1 = Convert.ToDecimal(e.GetGroupSummary(e.GroupRowHandle, grid_pallathur2.GroupSummary["Credit"]));
                    decimal Debit1 = Convert.ToDecimal(e.GetGroupSummary(e.GroupRowHandle, grid_pallathur2.GroupSummary["Debit"]));
                    if (Credit1 >= Debit1)
                    {
                        e.TotalValue = Credit1 - Debit1;
                        //decimal ss = Convert.ToDecimal(e.TotalValue);
                        //ViewState["Credit"] = Convert.ToDecimal(ViewState["Credit"]) + ss;
                    }
                }
            }
        }
        public DataTable loadgrid2()
        {
            objvar2.tempDt2 = new DataTable();
            if (grid.FilterExpression != "")
            {
                objVar.filterExpression = grid.FilterExpression;
                objVar.filterColumnName = objVar.filterExpression.Split('[')[1];
                objVar.filterColumnName = objVar.filterColumnName.Split(']')[0];
                //filter text
                objVar.filterText = objVar.filterExpression.Split(',')[1];
                objVar.filterText = objVar.filterText.Trim();
                objVar.filterText = objVar.filterText.Split(')')[0];
                objVar.filterText = objVar.filterText.Substring(1, objVar.filterText.Length - 1);
                objVar.filterText = objVar.filterText.Substring(0, objVar.filterText.Length - 1);

                objVar.varquery = "";
                switch (objVar.filterColumnName)
                {
                    case "Date":
                        break;
                    case "Branch":
                        objVar.varquery = "select cast(group_concat(NodeID) as char) as NodeID FROM svcf.headstree  where parentid=1 and node like '%" + objVar.filterText + "%'";
                        objVar.SelectedBranchList = balayer.GetSingleValue(objVar.varquery);
                        //objVar.varquery = "select 'Previous Net Balance' as 'Title',(case when sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end ) > sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) then CONCAT('Cr.Bal. ',CAST(sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end ) - sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )AS char(50))) else  CONCAT('Dr.Bal.',CAST(sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) - sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )AS char(50)))  end)  as Narration, CONCAT('Cr. INR ', CAST(sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )AS char(50))) as `Credit`,CONCAT('Dr. INR ',CAST(sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )AS char(50))) as `Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left join headstree as t8 on (t1.ChitGroupID=t8.NodeID) where `t1`.`BranchID` in (" + objVar.SelectedBranchList + ")   and t3.NodeID in(" + objVar.SelectedBranchList + ") and `t1`.`RootID` = 1 and t1.ChoosenDate < '" + balayer.indiandateToMysqlDate(dateFromConsolidated.Text) + "' and t1.IsDeleted=0  order by t1.ChoosenDate asc;";
                        objVar.varquery = "select 'Previous Net 1111111 Balance' as 'Title',(case when sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end ) > sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) then CONCAT('Cr.Bal. ',CAST(sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end ) - sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )AS char(50))) else  CONCAT('Dr.Bal.',CAST(sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) - sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )AS char(50)))  end)  as Narration, CONCAT('Cr. INR ', CAST(sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )AS char(50))) as `Credit`,CONCAT('Dr. INR ',CAST(sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )AS char(50))) as `Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left join headstree as t8 on (t1.ChitGroupID=t8.NodeID) where `t1`.`BranchID`=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + "   and t3.NodeID in(" + objVar.SelectedBranchList + ") and `t1`.`RootID` = 1 and t1.ChoosenDate < '" + balayer.indiandateToMysqlDate(dateFromConsolidated.Text) + "' and t1.IsDeleted=0  order by t1.ChoosenDate asc;";
                        objVar.tempDt = balayer.GetDataTable(objVar.varquery);
                        break;
                    case "LedgerHead":
                        break;
                    case "Narration":
                        break;
                    case "Credit":
                        break;
                    case "Debit":
                        break;
                }
            }
            else
            {
                objvar2.varquery2 = "select 'Previous 222 Net Balance' as 'Title',(case when sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end ) > sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) then CONCAT('Cr.Bal. ',CAST(sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end ) - sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )AS char(50))) else  CONCAT('Dr.Bal.',CAST(sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) - sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )AS char(50)))  end)  as Narration, CONCAT('Cr. INR ', CAST(sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )AS char(50))) as `Credit`,CONCAT('Dr. INR ',CAST(sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )AS char(50))) as `Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left join headstree as t8 on (t1.ChitGroupID=t8.NodeID) where `t1`.`BranchID` = 162 and `t1`.`RootID` = 1 and t1.ChoosenDate < '" + balayer.indiandateToMysqlDate(dateFromConsolidated.Text) + "' and t1.IsDeleted=0 order by t1.ChoosenDate asc;";
                objvar2.tempDt2 = balayer.GetDataTable(objvar2.varquery2);
            }

            return objvar2.tempDt2;
        }
        public DataTable loadgrid1()
        {
            objvar1.tempDt1 = new DataTable();
            if (grid.FilterExpression != "")
            {
                objVar.filterExpression = grid.FilterExpression;
                objVar.filterColumnName = objVar.filterExpression.Split('[')[1];
                objVar.filterColumnName = objVar.filterColumnName.Split(']')[0];
                //filter text
                objVar.filterText = objVar.filterExpression.Split(',')[1];
                objVar.filterText = objVar.filterText.Trim();
                objVar.filterText = objVar.filterText.Split(')')[0];
                objVar.filterText = objVar.filterText.Substring(1, objVar.filterText.Length - 1);
                objVar.filterText = objVar.filterText.Substring(0, objVar.filterText.Length - 1);

                objVar.varquery = "";
                switch (objVar.filterColumnName)
                {
                    case "Date":
                        break;
                    case "Branch":
                        objVar.varquery = "select cast(group_concat(NodeID) as char) as NodeID FROM svcf.headstree  where parentid=1 and node like '%" + objVar.filterText + "%'";
                        objVar.SelectedBranchList = balayer.GetSingleValue(objVar.varquery);
                        //objVar.varquery = "select 'Previous Net Balance' as 'Title',(case when sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end ) > sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) then CONCAT('Cr.Bal. ',CAST(sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end ) - sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )AS char(50))) else  CONCAT('Dr.Bal.',CAST(sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) - sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )AS char(50)))  end)  as Narration, CONCAT('Cr. INR ', CAST(sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )AS char(50))) as `Credit`,CONCAT('Dr. INR ',CAST(sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )AS char(50))) as `Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left join headstree as t8 on (t1.ChitGroupID=t8.NodeID) where `t1`.`BranchID` in (" + objVar.SelectedBranchList + ")   and t3.NodeID in(" + objVar.SelectedBranchList + ") and `t1`.`RootID` = 1 and t1.ChoosenDate < '" + balayer.indiandateToMysqlDate(dateFromConsolidated.Text) + "' and t1.IsDeleted=0  order by t1.ChoosenDate asc;";
                        objVar.varquery = "select 'Previous Net Balance' as 'Title',(case when sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end ) > sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) then CONCAT('Cr.Bal. ',CAST(sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end ) - sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )AS char(50))) else  CONCAT('Dr.Bal.',CAST(sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) - sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )AS char(50)))  end)  as Narration, CONCAT('Cr. INR ', CAST(sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )AS char(50))) as `Credit`,CONCAT('Dr. INR ',CAST(sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )AS char(50))) as `Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left join headstree as t8 on (t1.ChitGroupID=t8.NodeID) where `t1`.`BranchID`=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + "   and t3.NodeID in(" + objVar.SelectedBranchList + ") and `t1`.`RootID` = 1 and t1.ChoosenDate < '" + balayer.indiandateToMysqlDate(dateFromConsolidated.Text) + "' and t1.IsDeleted=0  order by t1.ChoosenDate asc;";
                        objVar.tempDt = balayer.GetDataTable(objVar.varquery);
                        break;
                    case "LedgerHead":
                        break;
                    case "Narration":
                        break;
                    case "Credit":
                        break;
                    case "Debit":
                        break;
                }
            }
            else
            {
                //objvar1.varquery1 = "select 'Previous  Net 111 Balance' as 'Title', " + (1 + 1) + " as Narration, 'CR.INR 1111111.00' as `Credit`,TreeHint as `Debit` FROM svcf.headstree where ParentID= 1184; ";
                ////objvar1.varquery1 = "select 'Previous  Net 111 Balance' as 'Title',(case when sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end ) > sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) then CONCAT('Cr.Bal. ',CAST(sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end ) - sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )AS char(50))) else  CONCAT('Dr.Bal.',CAST(sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) - sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )AS char(50)))  end)  as Narration, CONCAT('Cr. INR ', CAST(sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )AS char(50))) as `Credit`,CONCAT('Dr. INR ',CAST(sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )AS char(50))) as `Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left join headstree as t8 on (t1.ChitGroupID=t8.NodeID) where `t1`.`BranchID` = 162 and `t1`.`RootID` = 1 and t1.ChoosenDate < '" + balayer.indiandateToMysqlDate(dateFromConsolidated.Text) + "' and t1.IsDeleted=0 order by t1.ChoosenDate asc;";
                //objvar1.tempDt1 = balayer.GetDataTable(objvar1.varquery1);

                string Value1 = balayer.ToobjectstrEvenNull(Session["Branchid"]);
                if (Value1 == "161")
                {
                    objVar.varquery1 = "select 'Previous Net Balance Pallathur_3' as 'Title',(case when sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end ) > sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) then CONCAT('Cr.Bal. ',CAST(sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end ) - sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )AS char(50))) else  CONCAT('Dr.Bal.',CAST(sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) - sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )AS char(50)))  end)  as Narration, CONCAT('Cr. INR ', CAST(sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )AS char(50))) as `Credit`,CONCAT('Dr. INR ',CAST(sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )AS char(50))) as `Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left join headstree as t8 on (t1.ChitGroupID=t8.NodeID) where `t1`.`BranchID` = 162 and `t1`.`RootID` = 1 and t1.ChoosenDate < '" + balayer.indiandateToMysqlDate(dateFromConsolidated.Text) + "' and t1.IsDeleted=0 order by t1.ChoosenDate asc;";
                    objVar.tempDt1 = balayer.GetDataTable(objVar.varquery1);
                    objVar.varquery2 = "select 'Previous Net Balance Pallathur_2' as 'Title',(case when sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end ) > sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) then CONCAT('Cr.Bal. ',CAST(sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end ) - sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )AS char(50))) else  CONCAT('Dr.Bal.',CAST(sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) - sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )AS char(50)))  end)  as Narration, CONCAT('Cr. INR ', CAST(sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )AS char(50))) as `Credit`,CONCAT('Dr. INR ',CAST(sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )AS char(50))) as `Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left join headstree as t8 on (t1.ChitGroupID=t8.NodeID) where `t1`.`BranchID` = 161 and `t1`.`RootID` = 1 and t1.ChoosenDate < '" + balayer.indiandateToMysqlDate(dateFromConsolidated.Text) + "' and t1.IsDeleted=0 order by t1.ChoosenDate asc;";
                    objVar.tempDt2 = balayer.GetDataTable(objVar.varquery2);
                    objVar.varquery = "select 'Previous Net Balance Pallathur_1' as 'Title',(case when sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end ) > sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) then CONCAT('Cr.Bal. ',CAST(sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end ) - sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )AS char(50))) else  CONCAT('Dr.Bal.',CAST(sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) - sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )AS char(50)))  end)  as Narration, CONCAT('Cr. INR ', CAST(sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )AS char(50))) as `Credit`,CONCAT('Dr. INR ',CAST(sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )AS char(50))) as `Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left join headstree as t8 on (t1.ChitGroupID=t8.NodeID) where `t1`.`BranchID` = 160 and `t1`.`RootID` = 1 and t1.ChoosenDate < '" + balayer.indiandateToMysqlDate(dateFromConsolidated.Text) + "' and t1.IsDeleted=0 order by t1.ChoosenDate asc;";
                    objVar.tempDt = balayer.GetDataTable(objVar.varquery);

                    objVar.tempDt2.Merge(objVar.tempDt1);
                    objVar.tempDt2.AcceptChanges();
                    objVar.tempDt.Merge(objVar.tempDt2);
                    objVar.tempDt.AcceptChanges();
                }
            }

            return objVar.tempDt;
        }
        public DataTable LoadGrid()
        {
            
            DataTable finalDt = new DataTable();

            if (ddlbranch.SelectedIndex != 0)
            {
                string value = ddlbranch.SelectedItem.Text;

                objVar.varquery = @"select t1.ChoosenDate as `Date`,t8.Node as LedgerHead ,t3.Node as Branch , t1.Narration,(case when t1.Voucher_Type = 'C' then t1.Amount else 0.00 end )as `Credit`,(case when t1.Voucher_Type = 'D' then t1.Amount else 0.00 end )as `Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID = t3.NodeID left join membermaster as t4 on t4.MemberIDNew = t1.MemberID left join headstree as t8 on(t1.ChitGroupID = t8.NodeID) where `t1`.`BranchID` = " + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and `t1`.`RootID` = 1 and t1.ChoosenDate between '" + balayer.indiandateToMysqlDate(dateFromConsolidated.Text) + "' and '" + balayer.indiandateToMysqlDate(dateToConsolidated.Text) + "' and t1.IsDeleted = 0 and t3.Node = '" + value + "' order by t1.ChoosenDate asc";
                // objVar.varquery = "select 'Previous Net Balance' as 'Title',(case when sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end ) > sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) then CONCAT('Cr.Bal. ',CAST(sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end ) - sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )AS char(50))) else  CONCAT('Dr.Bal.',CAST(sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) - sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )AS char(50)))  end)  as Narration, CONCAT('Cr. INR ', CAST(sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )AS char(50))) as `Credit`,CONCAT('Dr. INR ',CAST(sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )AS char(50))) as `Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left join headstree as t8 on (t1.ChitGroupID=t8.NodeID) where `t1`.`BranchID` =" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + "  and `t1`.`RootID` = 1 and t1.ChoosenDate < '" + balayer.indiandateToMysqlDate(dateFromConsolidated.Text) + "' and t1.IsDeleted=0 order by t1.ChoosenDate asc;";
                finalDt = balayer.GetDataTable(objVar.varquery);
            }
            else
            {
                objVar.varquery = @"select t1.ChoosenDate as `Date`,t8.Node as LedgerHead ,t3.Node as Branch , t1.Narration,(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )as `Credit`,(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )as `Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left join headstree as t8 on (t1.ChitGroupID=t8.NodeID) where `t1`.`BranchID` =" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and `t1`.`RootID` = 1 and t1.ChoosenDate between '" + balayer.indiandateToMysqlDate(dateFromConsolidated.Text) + "' and '" + balayer.indiandateToMysqlDate(dateToConsolidated.Text) + "' and t1.IsDeleted=0 order by t1.ChoosenDate asc";
                // objVar.varquery = "select 'Previous Net Balance' as 'Title',(case when sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end ) > sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) then CONCAT('Cr.Bal. ',CAST(sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end ) - sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )AS char(50))) else  CONCAT('Dr.Bal.',CAST(sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) - sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )AS char(50)))  end)  as Narration, CONCAT('Cr. INR ', CAST(sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )AS char(50))) as `Credit`,CONCAT('Dr. INR ',CAST(sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )AS char(50))) as `Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left join headstree as t8 on (t1.ChitGroupID=t8.NodeID) where `t1`.`BranchID` =" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + "  and `t1`.`RootID` = 1 and t1.ChoosenDate < '" + balayer.indiandateToMysqlDate(dateFromConsolidated.Text) + "' and t1.IsDeleted=0 order by t1.ChoosenDate asc;";
                finalDt = balayer.GetDataTable(objVar.varquery);
            }
            

            LoadGridTemp();

            finalDt.Columns[4].DataType = typeof(decimal);
            finalDt.Columns[5].DataType = typeof(decimal);

            if (objVar.tempDt.Rows.Count > 0)
            {
                foreach (DataRow dr in objVar.tempDt.Rows)
                {
                    DataRow newRow = finalDt.NewRow();

                    //if (Convert.ToDecimal(dr.ItemArray[2]) > Convert.ToDecimal(dr.ItemArray[3]))
                    //{
                    //    newRow[3] = dr.ItemArray[0].ToString();
                    //    newRow[4] = Convert.ToDecimal(dr.ItemArray[2]) - Convert.ToDecimal(dr.ItemArray[3]);
                    //    newRow[5] = Convert.ToDecimal("0.00");
                    //}
                    //else if (Convert.ToDecimal(dr.ItemArray[3]) > Convert.ToDecimal(dr.ItemArray[2]))
                    //{
                    //    newRow[3] = dr.ItemArray[0].ToString();
                    //    newRow[5] = Convert.ToDecimal(dr.ItemArray[3]) - Convert.ToDecimal(dr.ItemArray[2]);
                    //    newRow[4] = Convert.ToDecimal("0.00");
                    //}
                    var credit = dr.ItemArray[2].ToString();
                    var debit = dr.ItemArray[3].ToString();
                    if (string.IsNullOrEmpty(credit))
                        credit = "0.00";
                    if (string.IsNullOrEmpty(debit))
                        debit = "0.00";
                    if (Convert.ToDecimal(credit) > Convert.ToDecimal(debit))
                    {
                        newRow[3] = dr.ItemArray[0].ToString();
                        newRow[4] = Convert.ToDecimal(credit) - Convert.ToDecimal(debit);
                        newRow[5] = Convert.ToDecimal("0.00");
                    }
                    else if (Convert.ToDecimal(debit) > Convert.ToDecimal(credit))
                    {
                        newRow[3] = dr.ItemArray[0].ToString();
                        newRow[5] = Convert.ToDecimal(debit) - Convert.ToDecimal(credit);
                        newRow[4] = Convert.ToDecimal("0.00");
                    }
                    else
                    {
                        newRow[3] = dr.ItemArray[0].ToString();
                        newRow[5] = Convert.ToDecimal("0.00");
                        newRow[4] = Convert.ToDecimal("0.00");
                    }
                    finalDt.Rows.InsertAt(newRow, 0);
                }
            }
            return finalDt;
        }
        


        public DataTable LoadGridTemp()
        {
            objVar.tempDt = new DataTable();
            if (grid.FilterExpression != "")
            {
                objVar.filterExpression = grid.FilterExpression;
                objVar.filterColumnName = objVar.filterExpression.Split('[')[1];
                objVar.filterColumnName = objVar.filterColumnName.Split(']')[0];
                //filter text
                objVar.filterText = objVar.filterExpression.Split(',')[1];
                objVar.filterText = objVar.filterText.Trim();
                objVar.filterText = objVar.filterText.Split(')')[0];
                objVar.filterText = objVar.filterText.Substring(1, objVar.filterText.Length - 1);
                objVar.filterText = objVar.filterText.Substring(0, objVar.filterText.Length - 1);

                objVar.varquery = "";
                switch (objVar.filterColumnName)
                {
                    case "Date":
                        break;
                    case "Branch":
                        objVar.varquery = "select cast(group_concat(NodeID) as char) as NodeID FROM svcf.headstree  where parentid=1 and node like '%" + objVar.filterText + "%'";
                        objVar.SelectedBranchList = balayer.GetSingleValue(objVar.varquery);
                        //objVar.varquery = "select 'Previous Net Balance' as 'Title',(case when sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end ) > sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) then CONCAT('Cr.Bal. ',CAST(sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end ) - sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )AS char(50))) else  CONCAT('Dr.Bal.',CAST(sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) - sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )AS char(50)))  end)  as Narration, CONCAT('Cr. INR ', CAST(sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )AS char(50))) as `Credit`,CONCAT('Dr. INR ',CAST(sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )AS char(50))) as `Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left join headstree as t8 on (t1.ChitGroupID=t8.NodeID) where `t1`.`BranchID` in (" + objVar.SelectedBranchList + ")   and t3.NodeID in(" + objVar.SelectedBranchList + ") and `t1`.`RootID` = 1 and t1.ChoosenDate < '" + balayer.indiandateToMysqlDate(dateFromConsolidated.Text) + "' and t1.IsDeleted=0  order by t1.ChoosenDate asc;";
                        objVar.varquery = "select 'Previous Net Balance' as 'Title',(case when sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end ) > sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) then CONCAT('Cr.Bal. ',CAST(sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end ) - sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )AS char(50))) else  CONCAT('Dr.Bal.',CAST(sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) - sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )AS char(50)))  end)  as Narration, CONCAT('Cr. INR ', CAST(sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )AS char(50))) as `Credit`,CONCAT('Dr. INR ',CAST(sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )AS char(50))) as `Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left join headstree as t8 on (t1.ChitGroupID=t8.NodeID) where `t1`.`BranchID`=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + "   and t3.NodeID in(" + objVar.SelectedBranchList + ") and `t1`.`RootID` = 1 and t1.ChoosenDate < '" + balayer.indiandateToMysqlDate(dateFromConsolidated.Text) + "' and t1.IsDeleted=0  order by t1.ChoosenDate asc;";
                        objVar.tempDt = balayer.GetDataTable(objVar.varquery);
                        break;
                    case "LedgerHead":
                        break;
                    case "Narration":
                        break;
                    case "Credit":
                        break;
                    case "Debit":
                        break;
                }
            }
            else
            {
                    //objVar.varquery = "select 'Previous Net Balance' as 'Title',(case when sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end ) > sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) then CONCAT('Cr.Bal. ',CAST(sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end ) - sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )AS char(50))) else  CONCAT('Dr.Bal.',CAST(sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) - sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )AS char(50)))  end)  as Narration, CONCAT('Cr. INR ', CAST(sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )AS char(50))) as `Credit`,CONCAT('Dr. INR ',CAST(sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )AS char(50))) as `Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left join headstree as t8 on (t1.ChitGroupID=t8.NodeID) where `t1`.`BranchID` =" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + "  and `t1`.`RootID` = 1 and t1.ChoosenDate < '" + balayer.indiandateToMysqlDate(dateFromConsolidated.Text) + "' and t1.IsDeleted=0 order by t1.ChoosenDate asc;";
                    //objVar.tempDt = balayer.GetDataTable(objVar.varquery);
                if (ddlbranch.SelectedIndex != 0)
                {
                    string value = ddlbranch.SelectedItem.Text;

                    objVar.varquery = "select 'Previous Net Balance' as 'Title',(case when sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end ) > sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) then CONCAT('Cr.Bal. ',CAST(sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end ) - sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )AS char(50))) else  CONCAT('Dr.Bal.',CAST(sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) - sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )AS char(50)))  end)  as Narration,sum((case when t1.Voucher_Type='C' then t1.Amount else 0.00 end ))as `Credit`,sum((case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ))as `Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left join headstree as t8 on (t1.ChitGroupID=t8.NodeID)  where `t1`.`BranchID` =  " + balayer.ToobjectstrEvenNull(Session["Branchid"]) + "   and `t1`.`RootID` = 1 and t1.ChoosenDate < '" + balayer.indiandateToMysqlDate(dateFromConsolidated.Text) + "' and t1.IsDeleted=0 and t3.Node='" + value + "' order by t1.ChoosenDate asc;";
                    // objVar.varquery = "select 'Previous Net Balance' as 'Title',(case when sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end ) > sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) then CONCAT('Cr.Bal. ',CAST(sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end ) - sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )AS char(50))) else  CONCAT('Dr.Bal.',CAST(sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) - sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )AS char(50)))  end)  as Narration, CONCAT('Cr. INR ', CAST(sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )AS char(50))) as `Credit`,CONCAT('Dr. INR ',CAST(sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )AS char(50))) as `Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left join headstree as t8 on (t1.ChitGroupID=t8.NodeID) where `t1`.`BranchID` =" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + "  and `t1`.`RootID` = 1 and t1.ChoosenDate < '" + balayer.indiandateToMysqlDate(dateFromConsolidated.Text) + "' and t1.IsDeleted=0 order by t1.ChoosenDate asc;";
                    objVar.tempDt = balayer.GetDataTable(objVar.varquery);

                }
                else
                {
                    objVar.varquery = "select 'Previous Net Balance' as 'Title',(case when sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end ) > sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) then CONCAT('Cr.Bal. ',CAST(sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end ) - sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )AS char(50))) else  CONCAT('Dr.Bal.',CAST(sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) - sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )AS char(50)))  end)  as Narration,sum((case when t1.Voucher_Type='C' then t1.Amount else 0.00 end ))as `Credit`,sum((case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ))as `Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left join headstree as t8 on (t1.ChitGroupID=t8.NodeID)  where `t1`.`BranchID` = " + balayer.ToobjectstrEvenNull(Session["Branchid"]) + "   and `t1`.`RootID` = 1 and t1.ChoosenDate < '" + balayer.indiandateToMysqlDate(dateFromConsolidated.Text) + "' and t1.IsDeleted=0 order by t1.ChoosenDate asc;";
                    // objVar.varquery = "select 'Previous Net Balance' as 'Title',(case when sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end ) > sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) then CONCAT('Cr.Bal. ',CAST(sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end ) - sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )AS char(50))) else  CONCAT('Dr.Bal.',CAST(sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) - sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )AS char(50)))  end)  as Narration, CONCAT('Cr. INR ', CAST(sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )AS char(50))) as `Credit`,CONCAT('Dr. INR ',CAST(sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )AS char(50))) as `Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left join headstree as t8 on (t1.ChitGroupID=t8.NodeID) where `t1`.`BranchID` =" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + "  and `t1`.`RootID` = 1 and t1.ChoosenDate < '" + balayer.indiandateToMysqlDate(dateFromConsolidated.Text) + "' and t1.IsDeleted=0 order by t1.ChoosenDate asc;";
                    objVar.tempDt = balayer.GetDataTable(objVar.varquery);
                }
            
            }

            return objVar.tempDt;
        }
        protected void Export_click(object sender, MenuItemEventArgs e)
        {
            string Value1 = balayer.ToobjectstrEvenNull(Session["Branchid"]);
         //   if (Value1 == "161")
          //  {
                if (e.Item.Text.ToString() == "PDF")
                {
                    using (this.headerImage = System.Drawing.Image.FromFile(Server.MapPath("~\\Images\\visalam.png")))
                    {
                        PrintingSystem ps = new PrintingSystem();
                        PrintableComponentLink gridcheque = new PrintableComponentLink(ps);
                        PrintableComponentLink gridchequeprev = new PrintableComponentLink(ps);

                        

                       grid.DataSource = LoadGrid();
                       grid.DataBind();

                       grid.Settings.ShowTitlePanel = false;
                       gridcheque.Component = gridExport;
                    
                        gridprev.Settings.ShowColumnHeaders = false;
                        gridprev.Settings.ShowHeaderFilterButton = false;
                        gridprev.Settings.ShowFooter = true;
                        gridprev.Settings.ShowFilterRow = false;
                        gridprev.Settings.ShowFilterRowMenu = false;
                        gridprev.Settings.ShowGroupPanel = false;   
                        gridprev.Settings.ShowGroupedColumns = true;

                        gridchequeprev.Component = gridExportprev;

                        gridExportprev.PreserveGroupRowStates = true;
                        gridExport.PreserveGroupRowStates = true;
                        
                        Link header = new Link();
                        CompositeLink compositeLink = new CompositeLink(ps);

                        header.CreateDetailArea += new CreateAreaEventHandler(header_CreateDetailArea);
                        compositeLink.Links.AddRange(new object[] { header, gridcheque });
                        string leftColumn = "Pages : [Page # of Pages #]";
                        string rightColumn = "Date: [Date Printed]\r\nTime: " + DateTime.Now.ToString("hh:mm tt");
                        PageHeaderFooter phf = compositeLink.PageHeaderFooter as PageHeaderFooter;
                        phf.Footer.Content.Clear();
                        phf.Footer.Content.AddRange(new string[] { rightColumn, leftColumn });
                        phf.Footer.LineAlignment = BrickAlignment.Center;

                        using (MemoryStream stream = new MemoryStream())
                        {
                            compositeLink.PaperKind = System.Drawing.Printing.PaperKind.Legal;
                            compositeLink.CreateDocument(false);
                            compositeLink.PrintingSystem.Document.AutoFitToPagesWidth = 1;
                            compositeLink.PrintingSystem.ExportToPdf(stream);
                            WriteToResponse("BranchLedger", true, "pdf", stream);
                        }
                    }
                }
                else if (e.Item.Text.ToString() == "XLSX")
                {
                    gridExportprev_pallathur1.PreserveGroupRowStates = true;
                    gridExportprev_pallathur1.WriteXlsxToResponse();
                    gridExport_pallathur1.PreserveGroupRowStates = true;
                    gridExport_pallathur1.WriteXlsxToResponse();

                    gridExportprev_Pallathur2.PreserveGroupRowStates = true;
                    gridExportprev_Pallathur2.WriteXlsxToResponse();
                    gridExport_pallathur2.PreserveGroupRowStates = true;
                    gridExport_pallathur2.WriteXlsxToResponse();

                    gridExportprev.PreserveGroupRowStates = true;
                    gridExportprev.WriteXlsxToResponse();
                    gridExport.PreserveGroupRowStates = true;
                    gridExport.WriteXlsxToResponse();
                }
           // }
            else
            {
                if (e.Item.Text.ToString() == "PDF")
                {
                    using (this.headerImage = System.Drawing.Image.FromFile(Server.MapPath("~\\Images\\visalam.png")))
                    {
                        PrintingSystem ps = new PrintingSystem();
                        PrintableComponentLink gridcheque = new PrintableComponentLink(ps);
                        PrintableComponentLink gridchequeprev = new PrintableComponentLink(ps);
                       
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
                            compositeLink.PaperKind = System.Drawing.Printing.PaperKind.Legal;
                            compositeLink.CreateDocument(false);
                            compositeLink.PrintingSystem.Document.AutoFitToPagesWidth = 1;
                            compositeLink.PrintingSystem.ExportToPdf(stream);
                            WriteToResponse("BranchLedger", true, "pdf", stream);
                        }
                    }
                }
                else if (e.Item.Text.ToString() == "XLSX")
                {
                    
                }
            }
           
        }
       
        void header_CreateDetailArea(object sender, CreateAreaEventArgs e)
        {
            
            e.Graph.BorderWidth = 0;
            Rectangle r = new Rectangle(0, 0, 40, 40);
            e.Graph.DrawImage(headerImage, r);
            TextBrick tb = new TextBrick();
            tb.Text = "Sree Visalam Chit Fund Limited";
            tb.Font = new Font("Arial", 10);
          //  tb.Rect = new RectangleF(50, 8, (e.Graph.ClientPageSize.Width / 2), 20);
            tb.Rect = new RectangleF(160, 8, (e.Graph.ClientPageSize.Width / 2), 20);

            tb.BorderWidth = 0;
            tb.BackColor = Color.Transparent;
            tb.HorzAlignment = DevExpress.Utils.HorzAlignment.Center;
            tb.VertAlignment = DevExpress.Utils.VertAlignment.Top;
            e.Graph.DrawBrick(tb);

            TextBrick tb1 = new TextBrick();
            tb1.Text = "Branch : " + Session["BranchName"];
            tb1.Font = new Font("Arial", 9);
            tb1.Rect = new RectangleF(160, 28, (e.Graph.ClientPageSize.Width / 2), 20);
            tb1.BorderWidth = 0;
            tb1.BackColor = Color.Transparent;
            tb1.HorzAlignment = DevExpress.Utils.HorzAlignment.Center;
            tb1.VertAlignment = DevExpress.Utils.VertAlignment.Top;
            e.Graph.DrawBrick(tb1);

            if (ddlbranch.SelectedItem.Text != "--select--")
            {
                TextBrick tb2 = new TextBrick();
                tb2.Text = "Branch Ledger : " + ddlbranch.SelectedItem.Text;
                tb2.Font = new Font("Arial", 9);
                tb2.Rect = new RectangleF(160, 48, (e.Graph.ClientPageSize.Width / 2), 20);
                tb2.BorderWidth = 0;
                tb2.BackColor = Color.Transparent;
                tb2.HorzAlignment = DevExpress.Utils.HorzAlignment.Center;
                tb2.VertAlignment = DevExpress.Utils.VertAlignment.Top;
                e.Graph.DrawBrick(tb2);

                TextBrick tb3 = new TextBrick();
                tb3.Text = "From : " + dateFromConsolidated.Text + " to " + dateToConsolidated.Text;
                tb3.Font = new Font("Arial", 9);
                tb3.Rect = new RectangleF(160, 68, (e.Graph.ClientPageSize.Width / 2), 20);
                tb3.BorderWidth = 0;
                tb3.BackColor = Color.Transparent;
                tb3.HorzAlignment = DevExpress.Utils.HorzAlignment.Center;
                tb3.VertAlignment = DevExpress.Utils.VertAlignment.Top;
                e.Graph.DrawBrick(tb3);
            }
            else
            {
                TextBrick tb3 = new TextBrick();
                tb3.Text = "From : " + dateFromConsolidated.Text + " to " + dateToConsolidated.Text;
                tb3.Font = new Font("Arial", 9);
                tb3.Rect = new RectangleF(160, 48, (e.Graph.ClientPageSize.Width / 2), 20);
                tb3.BorderWidth = 0;
                tb3.BackColor = Color.Transparent;
                tb3.HorzAlignment = DevExpress.Utils.HorzAlignment.Center;
                tb3.VertAlignment = DevExpress.Utils.VertAlignment.Top;
                e.Graph.DrawBrick(tb3);
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
        void ApplyLayout(int layoutIndex)
        {
            if (Convert.ToInt32(Session["Branchid"]) == 161)
            {
                grid_pallathur1.BeginUpdate();
                grid_pallathur2.BeginUpdate();
                grid.BeginUpdate();
                try
                {
                    grid_pallathur1.ClearSort();
                    grid_pallathur2.ClearSort();
                    grid.ClearSort();
                    switch (layoutIndex)
                    {
                        case 0:
                            break;
                        case 1:
                            grid_pallathur1.GroupBy(grid_pallathur1.Columns["Branch"]);
                            grid_pallathur1.GroupBy(grid_pallathur1.Columns["Date"]);
                            grid_pallathur2.GroupBy(grid_pallathur2.Columns["Branch"]);
                            grid_pallathur2.GroupBy(grid_pallathur2.Columns["Date"]);
                            grid.GroupBy(grid.Columns["Branch"]);
                            grid.GroupBy(grid.Columns["Date"]);
                            break;
                        case 2:
                            grid_pallathur1.GroupBy(grid_pallathur1.Columns["Branch"]);
                            grid_pallathur2.GroupBy(grid_pallathur2.Columns["Branch"]);
                            grid.GroupBy(grid.Columns["Branch"]);
                            break;
                    }
                }
                finally
                {
                    grid_pallathur1.EndUpdate();
                    grid_pallathur2.EndUpdate();
                    grid.EndUpdate();
                }
                grid_pallathur1.CollapseAll();
                grid_pallathur2.CollapseAll();
                grid.CollapseAll();
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
                            break;
                        case 1:
                           // grid.GroupBy(grid.Columns["Branch"]);
                            grid.GroupBy(grid.Columns["Date"]);
                            break;
                        //case 2:
                        //    grid.GroupBy(grid.Columns["Branch"]);
                        //    break;
                    }
                }
                finally
                {
                    grid.EndUpdate();
                }
                grid.CollapseAll();
            }
        }

        protected void BtnStatisticsGo_Click(object sender, EventArgs e)
        {
           
            grid.DataSource = LoadGrid();
            grid.DataBind();
                
            setStats(dateFromConsolidated.Text, dateToConsolidated.Text);
            select(ddlbranch.SelectedIndex);
            //DataTable dtPrevDateTrial = balayer.GetDataTable(@"select t8.Node as LedgerHead ,t3.Node as Branch , t1.Narration,(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )as `Credit`,(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )as `Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left join headstree as t8 on (t1.ChitGroupID=t8.NodeID) where `t1`.`BranchID` =" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and `t1`.`RootID` = 1 and t1.ChoosenDate < '" + balayer.indiandateToMysqlDate(dateFromConsolidated.Text) + "' and t1.IsDeleted=0 order by t1.ChoosenDate asc");
            if (Convert.ToInt32(Session["Branchid"]) == 161)
            {
           
             //   string value = ddlbranch.SelectedItem.Text;
               grid_pallathur1.Visible = true;
                grid_pallathur2.Visible = true;
             //  // grid_pallathur1.SettingsText.Title = "Trial Balance of" + ddlbranch.SelectedItem.Value + "Branches [Pallatur-I] from " + dateFromConsolidated.Text + " to " + dateToConsolidated.Text;
             ////   grid_pallathur1.SettingsText.Title = "Trial Balance of" + ddlbranch.SelectedItem.Value + "First A/C of Branches Ledgers from " + dateFromConsolidated.Text + " to " + dateToConsolidated.Text;
             //   grid_pallathur1.SettingsText.Title = "Trial Balance of" + value + "First A/C of Branches Ledgers from " + dateFromConsolidated.Text + " to " + dateToConsolidated.Text;
             //   grid_pallathur2.SettingsText.Title = "Trial Balance of" + value + "Second A/C of Branches Ledgers from " + dateFromConsolidated.Text + " to " + dateToConsolidated.Text;

               
             
            }
            else
            {
                grid_pallathur1.Visible = false;
                grid_pallathur2.Visible = false;
                
                grid.SettingsText.Title = "Trial Balance of  Branches Ledgers from " + dateFromConsolidated.Text + " to " + dateToConsolidated.Text;
            }
            //grid.SettingsText.Title = grid.SettingsText.Title + "<br>" + Convert.ToString(dtPrevDateTrial.Compute("Sum(Credit)", ""));
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
                string parsed = balayer.ConvertToIndCurrency(e.Value == null ? "0" : e.Value.ToString());
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
                if (e.Item.FieldName == "Token")
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
                        //e.TotalValue = cre - deb;
                        //e.TotalValueReady = true;
                  //      x.DisplayFormat = "Net Balance Cr.";
                        e.TotalValue = cre - deb;
                        e.TotalValueReady = true;
                        ViewState["prevcre"] = cre - deb;
                    }
                    else if (cre < deb)
                    {
                        x.DisplayFormat = "Dr.Bal.";
                        //e.TotalValue = deb - cre;
                        //e.TotalValueReady = true;
                       // x.DisplayFormat = "Net Balance Dr.";
                        e.TotalValue = deb - cre;
                        e.TotalValueReady = true;
                        ViewState["prevdeb"] = deb - cre;
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
                    decimal Credit1 = Convert.ToDecimal(e.GetGroupSummary(e.GroupRowHandle, gridprev.GroupSummary["Credit"]));
                    decimal Debit1 = Convert.ToDecimal(e.GetGroupSummary(e.GroupRowHandle, gridprev.GroupSummary["Debit"]));
                    if (Credit1 <= Debit1)
                    {
                        e.TotalValue = Debit1 - Credit1;
                        //decimal ss = Convert.ToDecimal(e.TotalValue);
                        //ViewState["Debit"] = Convert.ToDecimal( ViewState["Debit"])+ ss;
                    }
                }
                if (x.FieldName.ToString() == "Branch")
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

        protected void gridprev_pallathur1_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
        {
            if (!e.Row.ClientID.ToLower().Contains("footerrow") || grid_pallathur1.VisibleRowCount <= 0)
                e.Row.Visible = false;
            else
            {
                System.Web.UI.WebControls.TableCell dataCell = e.Row.Cells[0] as System.Web.UI.WebControls.TableCell;
                dataCell.Text = "Previous Net Balance";
                e.Row.Visible = true;
                // for (int iCol = 0; iCol<gridprev.Columns.Count; iCol++)
            }
        }

        protected void gridprev_pallathur1_SummaryDisplayText(object sender, ASPxGridViewSummaryDisplayTextEventArgs e)
        {
            if (!(e.Value == null))
            {
                string parsed = balayer.ConvertToIndCurrency(e.Value == null ? "0" : e.Value.ToString());
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
                if (e.Item.FieldName == "Token")
                {
                    e.Text = parsed;
                }
            }
        }

        protected void gridprev_pallathur1_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            if (grid_pallathur1.FilterExpression != "")
                if (grid_pallathur1.FilterExpression != gridprev_pallathur1.FilterExpression)
                {
                    gridprev_pallathur1.FilterExpression = grid_pallathur1.FilterExpression;
                    gridprev_pallathur1.SettingsText.Title = gridprev_pallathur1.FilterExpression;
                }
                else
                    if (gridprev_pallathur1.FilterExpression != "")
                        gridprev_pallathur1.FilterExpression = string.Empty;
        }

        protected void gridprev_pallathur1_CustomSummaryCalculate(object sender, CustomSummaryEventArgs e)
        {
            if (e.IsTotalSummary)
            {
                ASPxSummaryItem x = (ASPxSummaryItem)e.Item;
                if (x.FieldName.ToString() == "Debit")
                {
                    if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Finalize)
                    {
                        decimal total = 0.00M;
                        for (int i = 0; i < gridprev_pallathur1.VisibleRowCount; i++)
                        {
                            if (gridprev_pallathur1.IsGroupRow(i))
                            {
                                total += Convert.ToDecimal(gridprev_pallathur1.GetGroupSummaryValue(i, gridprev_pallathur1.GroupSummary["Date"]));
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
                        for (int i = 0; i < gridprev_pallathur1.VisibleRowCount; i++)
                        {
                            if (gridprev_pallathur1.IsGroupRow(i))
                            {
                                total += Convert.ToDecimal(gridprev_pallathur1.GetGroupSummaryValue(i, gridprev_pallathur1.GroupSummary["Branch"]));
                            }
                        }
                        e.TotalValue = total;
                        e.TotalValueReady = true;
                    }
                }
                if (x.FieldName.ToString() == "Narration")
                {
                    decimal cre = Convert.ToDecimal(gridprev_pallathur1.GetTotalSummaryValue(gridprev_pallathur1.TotalSummary["Credit"]));
                    decimal deb = Convert.ToDecimal(gridprev_pallathur1.GetTotalSummaryValue(gridprev_pallathur1.TotalSummary["Debit"]));
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
                    decimal Credit1 = Convert.ToDecimal(e.GetGroupSummary(e.GroupRowHandle, gridprev_pallathur1.GroupSummary["Credit"]));
                    decimal Debit1 = Convert.ToDecimal(e.GetGroupSummary(e.GroupRowHandle, gridprev_pallathur1.GroupSummary["Debit"]));
                    if (Credit1 <= Debit1)
                    {
                        e.TotalValue = Debit1 - Credit1;
                        //decimal ss = Convert.ToDecimal(e.TotalValue);
                        //ViewState["Debit"] = Convert.ToDecimal( ViewState["Debit"])+ ss;
                    }
                }
                if (x.FieldName.ToString() == "Branch")
                {
                    decimal Credit1 = Convert.ToDecimal(e.GetGroupSummary(e.GroupRowHandle, gridprev_pallathur1.GroupSummary["Credit"]));
                    decimal Debit1 = Convert.ToDecimal(e.GetGroupSummary(e.GroupRowHandle, gridprev_pallathur1.GroupSummary["Debit"]));
                    if (Credit1 >= Debit1)
                    {
                        e.TotalValue = Credit1 - Debit1;
                        //decimal ss = Convert.ToDecimal(e.TotalValue);
                        //ViewState["Credit"] = Convert.ToDecimal(ViewState["Credit"]) + ss;
                    }
                }
            }
        }

        protected void gridprev_pallathur2_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
        {
            if (!e.Row.ClientID.ToLower().Contains("footerrow") || grid_pallathur2.VisibleRowCount <= 0)
                e.Row.Visible = false;
            else
            {
                System.Web.UI.WebControls.TableCell dataCell = e.Row.Cells[0] as System.Web.UI.WebControls.TableCell;
                dataCell.Text = "Previous Net Balance";
                e.Row.Visible = true;
                // for (int iCol = 0; iCol<gridprev.Columns.Count; iCol++)
            }
        }

        protected void gridprev_pallathur2_SummaryDisplayText(object sender, ASPxGridViewSummaryDisplayTextEventArgs e)
        {
            if (!(e.Value == null))
            {
                string parsed = balayer.ConvertToIndCurrency(e.Value == null ? "0" : e.Value.ToString());
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
                if (e.Item.FieldName == "Token")
                {
                    e.Text = parsed;
                }
            }
        }

        protected void gridprev_pallathur2_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            if (grid_pallathur2.FilterExpression != "")
                if (grid_pallathur2.FilterExpression != gridprev_pallathur2.FilterExpression)
                {
                    gridprev_pallathur2.FilterExpression = grid_pallathur2.FilterExpression;
                    gridprev_pallathur2.SettingsText.Title = gridprev_pallathur2.FilterExpression;
                }
                else
                    if (gridprev_pallathur2.FilterExpression != "")
                        gridprev_pallathur2.FilterExpression = string.Empty;
        }

        protected void gridprev_pallathur2_CustomSummaryCalculate(object sender, CustomSummaryEventArgs e)
        {
            if (e.IsTotalSummary)
            {
                ASPxSummaryItem x = (ASPxSummaryItem)e.Item;
                if (x.FieldName.ToString() == "Debit")
                {
                    if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Finalize)
                    {
                        decimal total = 0.00M;
                        for (int i = 0; i < gridprev_pallathur2.VisibleRowCount; i++)
                        {
                            if (gridprev_pallathur2.IsGroupRow(i))
                            {
                                total += Convert.ToDecimal(gridprev_pallathur2.GetGroupSummaryValue(i, gridprev_pallathur2.GroupSummary["Date"]));
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
                        for (int i = 0; i < gridprev_pallathur2.VisibleRowCount; i++)
                        {
                            if (gridprev_pallathur2.IsGroupRow(i))
                            {
                                total += Convert.ToDecimal(gridprev_pallathur2.GetGroupSummaryValue(i, gridprev_pallathur2.GroupSummary["Branch"]));
                            }
                        }
                        e.TotalValue = total;
                        e.TotalValueReady = true;
                    }
                }
                if (x.FieldName.ToString() == "Narration")
                {
                    decimal cre = Convert.ToDecimal(gridprev_pallathur2.GetTotalSummaryValue(gridprev_pallathur2.TotalSummary["Credit"]));
                    decimal deb = Convert.ToDecimal(gridprev_pallathur2.GetTotalSummaryValue(gridprev_pallathur2.TotalSummary["Debit"]));
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
                    decimal Credit1 = Convert.ToDecimal(e.GetGroupSummary(e.GroupRowHandle, gridprev_pallathur2.GroupSummary["Credit"]));
                    decimal Debit1 = Convert.ToDecimal(e.GetGroupSummary(e.GroupRowHandle, gridprev_pallathur2.GroupSummary["Debit"]));
                    if (Credit1 <= Debit1)
                    {
                        e.TotalValue = Debit1 - Credit1;
                        //decimal ss = Convert.ToDecimal(e.TotalValue);
                        //ViewState["Debit"] = Convert.ToDecimal( ViewState["Debit"])+ ss;
                    }
                }
                if (x.FieldName.ToString() == "Branch")
                {
                    decimal Credit1 = Convert.ToDecimal(e.GetGroupSummary(e.GroupRowHandle, gridprev_pallathur2.GroupSummary["Credit"]));
                    decimal Debit1 = Convert.ToDecimal(e.GetGroupSummary(e.GroupRowHandle, gridprev_pallathur2.GroupSummary["Debit"]));
                    if (Credit1 >= Debit1)
                    {
                        e.TotalValue = Credit1 - Debit1;
                        //decimal ss = Convert.ToDecimal(e.TotalValue);
                        //ViewState["Credit"] = Convert.ToDecimal(ViewState["Credit"]) + ss;
                    }
                }
            }
        }

        //protected void btnExportExcel_Click1(object sender, EventArgs e)
        //{
        //    gridexcel.FileName = "Direct Branch" + DateTime.Now.Millisecond.ToString();
        //    grid.DataSource = LoadGrid();
        //    grid.DataBind();
        //    gridexcel.WriteXlsToResponse();
        //}

    }
}
