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
    public partial class Pallathurbranch3 : System.Web.UI.Page
    {
        #region VarDeclaration
        BusinessLayer balayer = new BusinessLayer();
        TransactionLayer tranlayer = new TransactionLayer();
        DataTable BindPrev_Balance = null;
        #endregion
        private System.Drawing.Image headerImage;
        VarDeclaration objVar = new VarDeclaration();
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
                        MySqlDataAdapter mem2GrpAdp = new MySqlDataAdapter("select distinct t3.Node as Branch from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left join headstree as t8 on (t1.ChitGroupID=t8.NodeID) where    `t1`.`RootID` = 1 and t1.IsDeleted=0  order by t3.Node asc;", con);
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
                setStats(dateFromConsolidated.Text, dateToConsolidated.Text);
                ApplyLayout(1);
                foreach (GridViewColumn column in grid.Columns)
                {
                    if (column is GridViewDataColumn)
                    {
                        ((GridViewDataColumn)column).Settings.AutoFilterCondition = AutoFilterCondition.Contains;
                    }
                }

               
            }
            //select(ddlbranch.SelectedIndex);
            grid.DataSource = select(ddlbranch.SelectedIndex);

            grid.DataBind();
        }
        protected void btnExportExcel_Click1(object sender, EventArgs e)
        {
            gridexcel.FileName = "Direct Pallathur2" + DateTime.Now.Millisecond.ToString();
            grid.DataSource = select(ddlbranch.SelectedIndex);
            grid.DataBind();

            gridexcel.WriteXlsToResponse();
        }
        protected void ResetStats()
        {
            if (Convert.ToInt32(Session["Branchid"]) == 161)
            {
               // Panel_P1.Visible = true;
           //     Panel_P2.Visible = true;
                Pallathur_3.Visible = true;

                

                lblCrAmount.Text = "0.00";
                lblCrBalance.Text = "0.00";
                lblDrAmount.Text = "0.00";
                lblDrBalance.Text = "0.00";
            }
            else
            {
             //   Panel_P1.Visible = false;
              //  Panel_P2.Visible = false;
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
               

                DataTable dtStats_P3 = balayer.GetDataTable(" SELECT RootID,sum((case when Voucher_Type='C' then Amount else 0.00 end ))as `Credit Amount`,sum((case when Voucher_Type='D' then Amount else 0.00 end ))as `Debit Amount` FROM `svcf`.`voucher` where BranchID=162 and ChoosenDate between '" + balayer.indiandateToMysqlDate(Fromdate) + "' and '" + balayer.indiandateToMysqlDate(Todate) + "' and RootID=1 group by RootID");
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

        protected DataTable select(int id)
        {
            DataTable finalDt = new DataTable();
            if (Convert.ToInt32(Session["Branchid"]) == 161)
            {
                if (id == 0)
                {
                    // AccessDataSource1.ConnectionString = CommonClassFile.ConnectionString;
                    objVar.varquery = @"select t1.ChoosenDate as `Date`,t8.Node as LedgerHead ,t3.Node as Branch , t1.Narration,(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )as `Credit`,(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )as `Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left join headstree as t8 on (t1.ChitGroupID=t8.NodeID) where `t1`.`BranchID` =162 and `t1`.`RootID` = 1 and t1.ChoosenDate between '" + balayer.indiandateToMysqlDate(dateFromConsolidated.Text) + "' and '" + balayer.indiandateToMysqlDate(dateToConsolidated.Text) + "' and t1.IsDeleted=0 order by t1.ChoosenDate asc";
                    finalDt = balayer.GetDataTable(objVar.varquery);
                    AccessDataSource6.ConnectionString = CommonClassFile.ConnectionString;
                    AccessDataSource6.SelectCommand = @"select t1.ChoosenDate as `Date`,t8.Node as LedgerHead ,t3.Node as Branch , t1.Narration,(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )as `Credit`,(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )as `Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left join headstree as t8 on (t1.ChitGroupID=t8.NodeID) where `t1`.`BranchID` =162 and `t1`.`RootID` = 1 and t1.ChoosenDate < '" + balayer.indiandateToMysqlDate(dateFromConsolidated.Text) + "' and t1.IsDeleted=0 order by t1.ChoosenDate asc";
                    gridprev_pallathur3.DataBind();
                }
                else
                {
                    string value = ddlbranch.SelectedItem.Text;
                    //AccessDataSource1.ConnectionString = CommonClassFile.ConnectionString;
                    objVar.varquery = @"select t1.ChoosenDate as `Date`,t8.Node as LedgerHead ,t3.Node as Branch , t1.Narration,(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )as `Credit`,(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )as `Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left join headstree as t8 on (t1.ChitGroupID=t8.NodeID) where `t1`.`BranchID` =162 and `t1`.`RootID` = 1 and t1.ChoosenDate between '" + balayer.indiandateToMysqlDate(dateFromConsolidated.Text) + "' and '" + balayer.indiandateToMysqlDate(dateToConsolidated.Text) + "' and t1.IsDeleted=0 and t3.Node='" + value + "' order by t1.ChoosenDate asc";
                    finalDt = balayer.GetDataTable(objVar.varquery);
                    AccessDataSource6.ConnectionString = CommonClassFile.ConnectionString;
                    AccessDataSource6.SelectCommand = @"select t1.ChoosenDate as `Date`,t8.Node as LedgerHead ,t3.Node as Branch , t1.Narration,(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )as `Credit`,(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )as `Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left join headstree as t8 on (t1.ChitGroupID=t8.NodeID) where `t1`.`BranchID` =162 and `t1`.`RootID` = 1 and t1.ChoosenDate < '" + balayer.indiandateToMysqlDate(dateFromConsolidated.Text) + "' and t1.IsDeleted=0 and t3.Node='" + value + "' order by t1.ChoosenDate asc";
                    gridprev_pallathur3.DataBind();
                }
            }
            else
            {

                AccessDataSource1.ConnectionString = CommonClassFile.ConnectionString;
                AccessDataSource1.SelectCommand = @"select t1.ChoosenDate as `Date`,t8.Node as LedgerHead ,t3.Node as Branch , t1.Narration,(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )as `Credit`,(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )as `Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left join headstree as t8 on (t1.ChitGroupID=t8.NodeID) where `t1`.`BranchID` =" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and `t1`.`RootID` = 1 and t1.ChoosenDate between '" + balayer.indiandateToMysqlDate(dateFromConsolidated.Text) + "' and '" + balayer.indiandateToMysqlDate(dateToConsolidated.Text) + "' and t1.IsDeleted=0 order by t1.ChoosenDate asc";
                grid.DataBind();


                string query = "";

                query = "select 'Previous Net Balance' as 'Title',(case when sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end ) > sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) then CONCAT('Cr.Bal. ',CAST(sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end ) - sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )AS char(50))) else  CONCAT('Dr.Bal.',CAST(sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) - sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )AS char(50)))  end)  as Narration, CONCAT('Cr. INR ', CAST(sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )AS char(50))) as `Credit`,CONCAT('Dr. INR ',CAST(sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )AS char(50))) as `Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left join headstree as t8 on (t1.ChitGroupID=t8.NodeID) where `t1`.`BranchID` =" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + "  and `t1`.`RootID` = 1 and t1.ChoosenDate < '" + balayer.indiandateToMysqlDate(dateFromConsolidated.Text) + "' and t1.IsDeleted=0 order by t1.ChoosenDate asc;";
                BindPrev_Balance = new DataTable();
                BindPrev_Balance = balayer.GetDataTable(query);

                //gridprev1.DataSource = BindPrev_Balance;
                //gridprev1.DataBind();

            }
            LoadGridTemp();

            //finalDt.Columns[4].DataType = typeof(decimal);
            //finalDt.Columns[5].DataType = typeof(decimal);


            //foreach (DataRow dr in objVar.tempDt.Rows)
            //{
            //    DataRow newRow = finalDt.NewRow();
            //    newRow[3] = dr.ItemArray[0].ToString() + "(" + dr.ItemArray[1].ToString() + ")";
            //    string second = dr.ItemArray[2].ToString();
            //    string third = dr.ItemArray[3].ToString();
            //    if (second == "")
            //    {
            //        second = "0";
            //    }
            //    if (third == "")
            //    {
            //        third = "0";
            //    }
            //    newRow[4] = Convert.ToDecimal(second);
            //    newRow[5] = Convert.ToDecimal(third);
            //    finalDt.Rows.InsertAt(newRow, 0);
            //}

            finalDt.Columns[4].DataType = typeof(decimal);
            finalDt.Columns[5].DataType = typeof(decimal);

            if (objVar.tempDt.Rows.Count > 0)
            {
                foreach (DataRow dr in objVar.tempDt.Rows)
                {
                    DataRow newRow = finalDt.NewRow();
                    //onchange in 01/11/2018
                    if (Convert.ToString(dr.ItemArray[2]) != "" || Convert.ToString(dr.ItemArray[3]) != "")
                    {
                        if (Convert.ToDecimal(dr.ItemArray[2]) > Convert.ToDecimal(dr.ItemArray[3]))
                        {
                            newRow[3] = dr.ItemArray[0].ToString();
                            newRow[4] = Convert.ToDecimal(dr.ItemArray[2]) - Convert.ToDecimal(dr.ItemArray[3]);
                            newRow[5] = Convert.ToDecimal("0.00");
                        }
                        else if (Convert.ToDecimal(dr.ItemArray[3]) > Convert.ToDecimal(dr.ItemArray[2]))
                        {
                            newRow[3] = dr.ItemArray[0].ToString();
                            newRow[5] = Convert.ToDecimal(dr.ItemArray[3]) - Convert.ToDecimal(dr.ItemArray[2]);
                            newRow[4] = Convert.ToDecimal("0.00");
                        }
                        else
                        {
                            newRow[3] = dr.ItemArray[0].ToString();
                            newRow[5] = Convert.ToDecimal("0.00");
                            newRow[4] = Convert.ToDecimal("0.00");
                        }
                    }
                    else
                    {
                        newRow[3] = "Previous Net Balance";
                        newRow[5] = Convert.ToDecimal("0.00");
                        newRow[4] = Convert.ToDecimal("0.00");
                    }
                    finalDt.Rows.InsertAt(newRow, 0);
                }
            }

            return finalDt;
        }

        protected void oncheck_load(object sender, EventArgs e)
        {

        }
        protected void ddlGroup_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlGroup.SelectedIndex == 0)
            {
                select(ddlbranch.SelectedIndex);
                ApplyLayout(0);
                grid.TotalSummary["Credit"].SummaryType = SummaryItemType.Sum;
                grid.TotalSummary["Debit"].SummaryType = SummaryItemType.Sum;
            }
            else if (ddlGroup.SelectedIndex == 1)
            {
                select(ddlbranch.SelectedIndex);
                ApplyLayout(1);
                grid.TotalSummary["Credit"].SummaryType = SummaryItemType.Custom;
                grid.TotalSummary["Debit"].SummaryType = SummaryItemType.Custom;
            }
            //else
            //{
            //    select(ddlbranch.SelectedIndex);
            //    ApplyLayout(2);
            //    grid.TotalSummary["Credit"].SummaryType = SummaryItemType.Custom;
            //    grid.TotalSummary["Debit"].SummaryType = SummaryItemType.Custom;
            //}
        }
        protected void ASPxGridView1_SummaryDisplayText(object sender, ASPxGridViewSummaryDisplayTextEventArgs e)
        {
            string parsed = balayer.ConvertToIndCurrency(e.Value == null ? "0" : e.Value.ToString());
            if (e.Item.FieldName == "Credit")
            {
                e.Text = "Cr. " + parsed;
            }
            if (e.Item.FieldName == "Debit")
            {
                e.Text = "Dr." + parsed.Replace(" ", "");
            }
            if (e.Item.FieldName == "Narration")
            {
                e.Text = Convert.ToString(e.Item.DisplayFormat.ToString() + parsed.Replace(" ", ""));
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
                decimal gridAmount = Convert.ToDecimal(grid.GetTotalSummaryValue(grid.TotalSummary["Narration"]));

              //  GridViewDataColumn colNarration = gridprev.Columns["Narration"] as GridViewDataColumn;
                decimal gridprevAmount = 0;
                // gridprevAmount = Convert.ToDecimal(gridprev.GetRowValues(0, "Narration"));
                //decimal gridprevAmount = Convert.ToDecimal(gridprev.GetTotalSummaryValue(gridprev.TotalSummary["Narration"]));
                e.Text = Convert.ToString("Total:₹" + (gridprevAmount + gridAmount));
            }

        }

        protected void grid_pallathur1_SummaryDisplayText(object sender, ASPxGridViewSummaryDisplayTextEventArgs e)
        {
           
        }

        protected void grid_pallathur2_SummaryDisplayText(object sender, ASPxGridViewSummaryDisplayTextEventArgs e)
        {
            
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

                ///on change date:01/11/2018
                if (x.FieldName.ToString() == "Narration")
                {
                    decimal cre = Convert.ToDecimal(grid.GetTotalSummaryValue(grid.TotalSummary["Credit"]));
                    decimal deb = Convert.ToDecimal(grid.GetTotalSummaryValue(grid.TotalSummary["Debit"]));
                    string ion = "";
                    var iii = "";
                    if (cre > 0)
                    {
                        objVar.varquery = "select (case when sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end ) > sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) then CONCAT('Cr.Bal- ',CAST(sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end ) - sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )AS char(50))) else  CONCAT('Dr.Bal-',CAST(sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) - sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )AS char(50)))  end)  as Narration from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left join headstree as t8 on (t1.ChitGroupID=t8.NodeID) where `t1`.`BranchID` = 162  and `t1`.`RootID` = 1 and t1.ChoosenDate < '" + balayer.indiandateToMysqlDate(dateFromConsolidated.Text) + "' and t1.IsDeleted=0 order by t1.ChoosenDate asc;";
                        ion = balayer.GetSingleValue(objVar.varquery);
                        //  iii = ion.Split('-')[1];

                        if (ddlbranch.SelectedIndex != 0)
                        {
                            string value = ddlbranch.SelectedItem.Text;

                            objVar.varquery = "select (case when sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end ) > sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) then CONCAT('Cr.Bal- ',CAST(sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end ) - sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )AS char(50))) else  CONCAT('Dr.Bal-',CAST(sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) - sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )AS char(50)))  end)  as Narration from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left join headstree as t8 on (t1.ChitGroupID=t8.NodeID) where `t1`.`BranchID` = 162  and `t1`.`RootID` = 1 and t1.ChoosenDate < '" + balayer.indiandateToMysqlDate(dateFromConsolidated.Text) + "' and t1.IsDeleted=0 and t3.Node='" + value + "' order by t1.ChoosenDate asc;";
                            // objVar.varquery = "select 'Previous Net Balance' as 'Title',(case when sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end ) > sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) then CONCAT('Cr.Bal. ',CAST(sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end ) - sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )AS char(50))) else  CONCAT('Dr.Bal.',CAST(sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) - sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )AS char(50)))  end)  as Narration, CONCAT('Cr. INR ', CAST(sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )AS char(50))) as `Credit`,CONCAT('Dr. INR ',CAST(sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )AS char(50))) as `Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left join headstree as t8 on (t1.ChitGroupID=t8.NodeID) where `t1`.`BranchID` =" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + "  and `t1`.`RootID` = 1 and t1.ChoosenDate < '" + balayer.indiandateToMysqlDate(dateFromConsolidated.Text) + "' and t1.IsDeleted=0 order by t1.ChoosenDate asc;";
                            ion = balayer.GetSingleValue(objVar.varquery);
                            if (ion == "")
                            {
                                iii = "0.00";
                            }
                            else
                            {
                                iii = ion.Split('-')[1];
                            }
                        }
                        else
                        {
                            objVar.varquery = "select (case when sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end ) > sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) then CONCAT('Cr.Bal- ',CAST(sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end ) - sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )AS char(50))) else  CONCAT('Dr.Bal-',CAST(sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) - sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )AS char(50)))  end)  as Narration from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left join headstree as t8 on (t1.ChitGroupID=t8.NodeID) where `t1`.`BranchID` = 162  and `t1`.`RootID` = 1 and t1.ChoosenDate < '" + balayer.indiandateToMysqlDate(dateFromConsolidated.Text) + "' and t1.IsDeleted=0 order by t1.ChoosenDate asc;";
                            // objVar.varquery = "select 'Previous Net Balance' as 'Title',(case when sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end ) > sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) then CONCAT('Cr.Bal. ',CAST(sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end ) - sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )AS char(50))) else  CONCAT('Dr.Bal.',CAST(sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) - sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )AS char(50)))  end)  as Narration, CONCAT('Cr. INR ', CAST(sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )AS char(50))) as `Credit`,CONCAT('Dr. INR ',CAST(sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )AS char(50))) as `Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left join headstree as t8 on (t1.ChitGroupID=t8.NodeID) where `t1`.`BranchID` =" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + "  and `t1`.`RootID` = 1 and t1.ChoosenDate < '" + balayer.indiandateToMysqlDate(dateFromConsolidated.Text) + "' and t1.IsDeleted=0 order by t1.ChoosenDate asc;";
                            ion = balayer.GetSingleValue(objVar.varquery);
                            if (ion == "")
                            {
                                iii = "0.00";
                            }
                            else
                            {
                                iii = ion.Split('-')[1];
                            }
                        }






                    }
                    if (cre > deb)
                    {
                        if (ion.Contains("Cr"))
                        {
                            var val = Convert.ToDecimal(iii);
                            var val1 = cre - deb;
                            x.DisplayFormat = "Cr.Bal.";
                            e.TotalValue = val + val1;
                        }

                        else if (ion.Contains("Dr"))
                        {
                            decimal val = Convert.ToDecimal(iii);
                            decimal val1 = cre - deb;
                            if (val > val1)
                            {
                                x.DisplayFormat = "Dr.Bal.";
                                e.TotalValue = val - val1;
                            }
                            else if (val < val1)
                            {
                                x.DisplayFormat = "Cr.Bal.";
                                e.TotalValue = val1 - val;
                            }
                            else
                            {
                                e.TotalValue = 0.00;
                            }

                        }
                        else if (ion == "")
                        {
                            var val1 = cre - deb;
                            x.DisplayFormat = "Cr.Bal.";
                            e.TotalValue = val1;
                        }



                        e.TotalValueReady = true;

                    }
                    else if (cre < deb)
                    {
                        if (ion.Contains("Dr"))
                        {
                            var val = Convert.ToDecimal(iii);
                            var val1 = deb - cre;
                            x.DisplayFormat = "Dr.Bal.";
                            e.TotalValue = val + val1;
                        }

                        else if (ion.Contains("Cr"))
                        {
                            var val = Convert.ToDecimal(iii);
                            var val1 = deb - cre;
                            if (val > val1)
                            {
                                x.DisplayFormat = "Cr.Bal.";
                                e.TotalValue = val - val1;
                            }
                            else if (val < val1)
                            {
                                x.DisplayFormat = "Dr.Bal.";
                                e.TotalValue = val1 - val;
                            }


                        }
                        else if (ion == "")
                        {
                            var val1 = deb - cre;
                            x.DisplayFormat = "Dr.Bal.";
                            e.TotalValue = val1;
                        }

                        e.TotalValueReady = true;
                    }
                    else
                    {
                        e.TotalValue = 0.00;
                        e.TotalValueReady = true;

                    }
                }
                ///on change date:01/11/2018
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
          
        }

        protected void grid_pallathur2_OnCustomSummaryCalculate(object sender, CustomSummaryEventArgs e)
        {
         
        }



        protected void Export_click(object sender, MenuItemEventArgs e)
        {
            string Value1 = balayer.ToobjectstrEvenNull(Session["Branchid"]);
            if (Value1 == "161")
            {
                if (e.Item.Text.ToString() == "PDF")
                {
                    using (this.headerImage = System.Drawing.Image.FromFile(Server.MapPath("~\\Images\\visalam.png")))
                    {
                        //PrintingSystem ps = new PrintingSystem();
                        //PrintableComponentLink gridcheque_pallathur1 = new PrintableComponentLink(ps);
                        //PrintableComponentLink gridcheque_pallathur2 = new PrintableComponentLink(ps);
                        //PrintableComponentLink gridcheque = new PrintableComponentLink(ps);
                        //PrintableComponentLink gridchequeprev_pallathur1 = new PrintableComponentLink(ps);
                        //PrintableComponentLink gridchequeprev_pallathur2 = new PrintableComponentLink(ps);
                        //PrintableComponentLink gridchequeprev = new PrintableComponentLink(ps);

                        PrintingSystem ps = new PrintingSystem();
                        PrintableComponentLink gridcheque = new PrintableComponentLink(ps);
                        PrintableComponentLink gridchequeprev = new PrintableComponentLink(ps);
                        gridcheque.Component = gridExport;

                        gridprev_pallathur3.Settings.ShowColumnHeaders = false;
                        gridprev_pallathur3.Settings.ShowHeaderFilterButton = false;
                        gridprev_pallathur3.Settings.ShowFooter = true;
                        gridprev_pallathur3.Settings.ShowFilterRow = false;
                        gridprev_pallathur3.Settings.ShowFilterRowMenu = false;
                        gridprev_pallathur3.Settings.ShowGroupPanel = false;
                        gridprev_pallathur3.Settings.ShowGroupedColumns = true;

                        grid.DataSource = select(ddlbranch.SelectedIndex);
                        grid.SettingsText.Title = "";
                        grid.DataBind();
                        gridchequeprev.Component = gridExportprev_Pallathur3;
                        gridExport.PreserveGroupRowStates = true;

                        
                        //gridcheque.Component = gridExport;
                        //gridchequeprev_pallathur1.Component = gridExportprev_pallathur1;
                        //gridchequeprev_pallathur2.Component = gridExportprev_Pallathur2;
                        //gridchequeprev.Component = gridExport;
                        //gridExport.PreserveGroupRowStates = true;
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
                            compositeLink.PaperKind = System.Drawing.Printing.PaperKind.A4;
                            compositeLink.CreateDocument(false);
                            compositeLink.PrintingSystem.Document.AutoFitToPagesWidth = 1;
                            compositeLink.PrintingSystem.ExportToPdf(stream);
                            WriteToResponse("BranchLedger", true, "pdf", stream);
                        }
                    }
                }
                else if (e.Item.Text.ToString() == "XLSX")
                {
                   
                    gridExport.PreserveGroupRowStates = true;
                    gridExport.WriteXlsxToResponse();
                }
            }
            else
            {
                if (e.Item.Text.ToString() == "PDF")
                {
                    using (this.headerImage = System.Drawing.Image.FromFile(Server.MapPath("~\\Images\\visalam.png")))
                    {
                        PrintingSystem ps = new PrintingSystem();
                        PrintableComponentLink gridcheque = new PrintableComponentLink(ps);
                        PrintableComponentLink gridchequeprev = new PrintableComponentLink(ps);



                        gridcheque.Component = gridExport;
                      //  gridchequeprev.Component = gridExportprev;
                        gridExport.PreserveGroupRowStates = true;
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
                            WriteToResponse("BranchLedger", true, "pdf", stream);
                        }
                    }
                }
                else if (e.Item.Text.ToString() == "XLSX")
                {
                    //gridExportprev.PreserveGroupRowStates = true;
                    //gridExportprev.WriteXlsxToResponse();
                    gridExport.PreserveGroupRowStates = true;
                    gridExport.WriteXlsxToResponse();
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
            tb.Rect = new RectangleF(50, 8, (e.Graph.ClientPageSize.Width / 2), 20);

            tb.BorderWidth = 0;
            tb.BackColor = Color.Transparent;
            tb.HorzAlignment = DevExpress.Utils.HorzAlignment.Center;
            tb.VertAlignment = DevExpress.Utils.VertAlignment.Top;
            e.Graph.DrawBrick(tb);

            TextBrick tb1 = new TextBrick();
            tb1.Text = "Branch : " + Session["BranchName"];
            tb1.Font = new Font("Arial", 9);
            tb1.Rect = new RectangleF(50, 28, (e.Graph.ClientPageSize.Width / 2), 20);
            tb1.BorderWidth = 0;
            tb1.BackColor = Color.Transparent;
            tb1.HorzAlignment = DevExpress.Utils.HorzAlignment.Center;
            tb1.VertAlignment = DevExpress.Utils.VertAlignment.Top;
            e.Graph.DrawBrick(tb1);


            if (ddlbranch.SelectedItem.Text != "--select--")
            {
                TextBrick tb2 = new TextBrick();
                tb2.Text = "Branches III A/c : " + ddlbranch.SelectedItem.Text;
                tb2.Font = new Font("Arial", 9);
                tb2.Rect = new RectangleF(50, 48, (e.Graph.ClientPageSize.Width / 2), 20);
                tb2.BorderWidth = 0;
                tb2.BackColor = Color.Transparent;
                tb2.HorzAlignment = DevExpress.Utils.HorzAlignment.Center;
                tb2.VertAlignment = DevExpress.Utils.VertAlignment.Top;
                e.Graph.DrawBrick(tb2);

                TextBrick tb3 = new TextBrick();
                tb3.Text = "From : " + dateFromConsolidated.Text + " to " + dateToConsolidated.Text;
                tb3.Font = new Font("Arial", 9);
                tb3.Rect = new RectangleF(50, 68, (e.Graph.ClientPageSize.Width / 2), 20);
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
                tb3.Rect = new RectangleF(50, 48, (e.Graph.ClientPageSize.Width / 2), 20);
                tb3.BorderWidth = 0;
                tb3.BackColor = Color.Transparent;
                tb3.HorzAlignment = DevExpress.Utils.HorzAlignment.Center;
                tb3.VertAlignment = DevExpress.Utils.VertAlignment.Top;
                e.Graph.DrawBrick(tb3);
            }

            //TextBrick tb2 = new TextBrick();
            //tb2.Text = "Branches III A/c : " + ddlbranch.SelectedItem.Text;
            //tb2.Font = new Font("Arial", 10);
            //tb2.Rect = new RectangleF(50, 48, (e.Graph.ClientPageSize.Width / 2), 20);
            //tb2.BorderWidth = 0;
            //tb2.BackColor = Color.Transparent;
            //tb2.HorzAlignment = DevExpress.Utils.HorzAlignment.Near;
            //tb2.VertAlignment = DevExpress.Utils.VertAlignment.Top;
            //e.Graph.DrawBrick(tb2);

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
              //  grid_pallathur1.BeginUpdate();
                //grid_pallathur2.BeginUpdate();
                grid.BeginUpdate();
                try
                {
                    //grid_pallathur1.ClearSort();
                  //  grid_pallathur2.ClearSort();
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
                        //    //grid_pallathur1.GroupBy(grid_pallathur1.Columns["Branch"]);
                        //  //  grid_pallathur2.GroupBy(grid_pallathur2.Columns["Branch"]);
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
                            grid.GroupBy(grid.Columns["Branch"]);
                            grid.GroupBy(grid.Columns["Date"]);
                            break;
                        case 2:
                            grid.GroupBy(grid.Columns["Branch"]);
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
        public DataTable LoadGridTemp()
        {
            objVar.tempDt = new DataTable();
            if (gridprev_pallathur3.FilterExpression != "")
            {
                //objVar.filterExpression = grid_pallathur1.FilterExpression;
                //objVar.filterColumnName = objVar.filterExpression.Split('[')[1];
                //objVar.filterColumnName = objVar.filterColumnName.Split(']')[0];
                ////filter text
                //objVar.filterText = objVar.filterExpression.Split(',')[1];
                //objVar.filterText = objVar.filterText.Trim();
                //objVar.filterText = objVar.filterText.Split(')')[0];
                //objVar.filterText = objVar.filterText.Substring(1, objVar.filterText.Length - 1);
                //objVar.filterText = objVar.filterText.Substring(0, objVar.filterText.Length - 1);

                //objVar.varquery = "";
                //switch (objVar.filterColumnName)
                //{
                //    case "Date":
                //        break;
                //    case "Branch":
                //        objVar.varquery = "select cast(group_concat(NodeID) as char) as NodeID FROM svcf.headstree  where parentid=1 and node like '%" + objVar.filterText + "%'";
                //        objVar.SelectedBranchList = balayer.GetSingleValue(objVar.varquery);
                //        //objVar.varquery = "select 'Previous Net Balance' as 'Title',(case when sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end ) > sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) then CONCAT('Cr.Bal. ',CAST(sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end ) - sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )AS char(50))) else  CONCAT('Dr.Bal.',CAST(sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) - sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )AS char(50)))  end)  as Narration, CONCAT('Cr. INR ', CAST(sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )AS char(50))) as `Credit`,CONCAT('Dr. INR ',CAST(sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )AS char(50))) as `Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left join headstree as t8 on (t1.ChitGroupID=t8.NodeID) where `t1`.`BranchID` in (" + objVar.SelectedBranchList + ")   and t3.NodeID in(" + objVar.SelectedBranchList + ") and `t1`.`RootID` = 1 and t1.ChoosenDate < '" + balayer.indiandateToMysqlDate(dateFromConsolidated.Text) + "' and t1.IsDeleted=0  order by t1.ChoosenDate asc;";
                //        objVar.varquery = "select 'Previous Net Balance' as 'Title',(case when sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end ) > sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) then CONCAT('Cr.Bal. ',CAST(sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end ) - sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )AS char(50))) else  CONCAT('Dr.Bal.',CAST(sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) - sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )AS char(50)))  end)  as Narration, CONCAT('Cr. INR ', CAST(sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )AS char(50))) as `Credit`,CONCAT('Dr. INR ',CAST(sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )AS char(50))) as `Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left join headstree as t8 on (t1.ChitGroupID=t8.NodeID) where `t1`.`BranchID`=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + "   and t3.NodeID in(" + objVar.SelectedBranchList + ") and `t1`.`RootID` = 1 and t1.ChoosenDate < '" + balayer.indiandateToMysqlDate(dateFromConsolidated.Text) + "' and t1.IsDeleted=0  order by t1.ChoosenDate asc;";
                //        objVar.tempDt = balayer.GetDataTable(objVar.varquery);
                //        break;
                //    case "LedgerHead":
                //        break;
                //    case "Narration":
                //        break;
                //    case "Credit":
                //        break;
                //    case "Debit":
                //        break;
                //}
            }
            else
            {
                //objVar.varquery = "select 'Previous Net Balance' as 'Title',(case when sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end ) > sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) then CONCAT('Cr.Bal. ',CAST(sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end ) - sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )AS char(50))) else  CONCAT('Dr.Bal.',CAST(sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) - sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )AS char(50)))  end)  as Narration,sum((case when t1.Voucher_Type='C' then t1.Amount else 0.00 end ))as `Credit`,sum((case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ))as `Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left join headstree as t8 on (t1.ChitGroupID=t8.NodeID) where `t1`.`BranchID` = 162  and `t1`.`RootID` = 1 and t1.ChoosenDate < '" + balayer.indiandateToMysqlDate(dateFromConsolidated.Text) + "' and t1.IsDeleted=0 order by t1.ChoosenDate asc;";
                //objVar.tempDt = balayer.GetDataTable(objVar.varquery);
                if (ddlbranch.SelectedIndex != 0)
                {
                    string value = ddlbranch.SelectedItem.Text;

                    objVar.varquery = "select 'Previous Net Balance' as 'Title',(case when sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end ) > sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) then CONCAT('Cr.Bal. ',CAST(sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end ) - sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )AS char(50))) else  CONCAT('Dr.Bal.',CAST(sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) - sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )AS char(50)))  end)  as Narration,sum((case when t1.Voucher_Type='C' then t1.Amount else 0.00 end ))as `Credit`,sum((case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ))as `Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left join headstree as t8 on (t1.ChitGroupID=t8.NodeID)  where `t1`.`BranchID` = 162  and `t1`.`RootID` = 1 and t1.ChoosenDate < '" + balayer.indiandateToMysqlDate(dateFromConsolidated.Text) + "' and t1.IsDeleted=0 and t3.Node='" + value + "' order by t1.ChoosenDate asc;";
                    // objVar.varquery = "select 'Previous Net Balance' as 'Title',(case when sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end ) > sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) then CONCAT('Cr.Bal. ',CAST(sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end ) - sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )AS char(50))) else  CONCAT('Dr.Bal.',CAST(sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) - sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )AS char(50)))  end)  as Narration, CONCAT('Cr. INR ', CAST(sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )AS char(50))) as `Credit`,CONCAT('Dr. INR ',CAST(sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )AS char(50))) as `Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left join headstree as t8 on (t1.ChitGroupID=t8.NodeID) where `t1`.`BranchID` =" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + "  and `t1`.`RootID` = 1 and t1.ChoosenDate < '" + balayer.indiandateToMysqlDate(dateFromConsolidated.Text) + "' and t1.IsDeleted=0 order by t1.ChoosenDate asc;";
                    objVar.tempDt = balayer.GetDataTable(objVar.varquery);

                }
                else
                {
                    objVar.varquery = "select 'Previous Net Balance' as 'Title',(case when sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end ) > sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) then CONCAT('Cr.Bal. ',CAST(sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end ) - sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )AS char(50))) else  CONCAT('Dr.Bal.',CAST(sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) - sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )AS char(50)))  end)  as Narration,sum((case when t1.Voucher_Type='C' then t1.Amount else 0.00 end ))as `Credit`,sum((case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ))as `Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left join headstree as t8 on (t1.ChitGroupID=t8.NodeID)  where `t1`.`BranchID` = 162  and `t1`.`RootID` = 1 and t1.ChoosenDate < '" + balayer.indiandateToMysqlDate(dateFromConsolidated.Text) + "' and t1.IsDeleted=0 order by t1.ChoosenDate asc;";
                    // objVar.varquery = "select 'Previous Net Balance' as 'Title',(case when sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end ) > sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) then CONCAT('Cr.Bal. ',CAST(sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end ) - sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )AS char(50))) else  CONCAT('Dr.Bal.',CAST(sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) - sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )AS char(50)))  end)  as Narration, CONCAT('Cr. INR ', CAST(sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )AS char(50))) as `Credit`,CONCAT('Dr. INR ',CAST(sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )AS char(50))) as `Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left join headstree as t8 on (t1.ChitGroupID=t8.NodeID) where `t1`.`BranchID` =" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + "  and `t1`.`RootID` = 1 and t1.ChoosenDate < '" + balayer.indiandateToMysqlDate(dateFromConsolidated.Text) + "' and t1.IsDeleted=0 order by t1.ChoosenDate asc;";
                    objVar.tempDt = balayer.GetDataTable(objVar.varquery);
                }
            }

            return objVar.tempDt;
        }
        protected void ddlbranch_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlbranch.SelectedIndex != 0)
            {

                select(ddlbranch.SelectedIndex);

            }
        }
        protected void BtnStatisticsGo_Click(object sender, EventArgs e)
        {
            setStats(dateFromConsolidated.Text, dateToConsolidated.Text);
            // select(ddlbranch.SelectedIndex);
            grid.DataSource = select(ddlbranch.SelectedIndex);

            grid.DataBind();
            //DataTable dtPrevDateTrial = balayer.GetDataTable(@"select t8.Node as LedgerHead ,t3.Node as Branch , t1.Narration,(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )as `Credit`,(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )as `Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left join headstree as t8 on (t1.ChitGroupID=t8.NodeID) where `t1`.`BranchID` =" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and `t1`.`RootID` = 1 and t1.ChoosenDate < '" + balayer.indiandateToMysqlDate(dateFromConsolidated.Text) + "' and t1.IsDeleted=0 order by t1.ChoosenDate asc");
            if (Convert.ToInt32(Session["Branchid"]) == 161)
            {
               
                grid.SettingsText.Title = "Trial Balance of Branches [Pallatur-III] from " + dateFromConsolidated.Text + " to " + dateToConsolidated.Text;
            }
            else
            {
               // grid_pallathur1.Visible = false;
               // grid_pallathur2.Visible = false;
                grid.SettingsText.Title = "Trial Balance of Branches from " + dateFromConsolidated.Text + " to " + dateToConsolidated.Text;
               // gridprev.SettingsText.Title = "Trial Balance of Branches from ";

            }
            //grid.SettingsText.Title = grid.SettingsText.Title + "<br>" + Convert.ToString(dtPrevDateTrial.Compute("Sum(Credit)", ""));
        }

        protected void gridprev_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
        {
            //if (!e.Row.ClientID.ToLower().Contains("footerrow") || grid.VisibleRowCount <= 0)
            //    e.Row.Visible = false;
            //else
            //{
            System.Web.UI.WebControls.TableCell dataCell = e.Row.Cells[0] as System.Web.UI.WebControls.TableCell;
            dataCell.Text = "Previous Net Balance";
            e.Row.Visible = true;
            // for (int iCol = 0; iCol<gridprev.Columns.Count; iCol++)
            //}
        }

        protected void gridprev_SummaryDisplayText(object sender, ASPxGridViewSummaryDisplayTextEventArgs e)
        {
            
        }

        protected void gridprev_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
           
        }

        protected void gridprev_CustomSummaryCalculate(object sender, CustomSummaryEventArgs e)
        {
          
        }

        protected void gridprev_pallathur1_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
        {
          
        }

        protected void gridprev_pallathur1_SummaryDisplayText(object sender, ASPxGridViewSummaryDisplayTextEventArgs e)
        {
            
        }

        protected void gridprev_pallathur1_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
          
        }

        protected void gridprev_pallathur1_CustomSummaryCalculate(object sender, CustomSummaryEventArgs e)
        {
           
        }

        protected void gridprev_pallathur3_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
        {
            if (!e.Row.ClientID.ToLower().Contains("footerrow") || grid.VisibleRowCount <= 0)
                e.Row.Visible = false;
            else
            {
                System.Web.UI.WebControls.TableCell dataCell = e.Row.Cells[0] as System.Web.UI.WebControls.TableCell;
                dataCell.Text = "Previous Net Balance";
                e.Row.Visible = true;

            }
        }

        protected void gridprev_pallathur3_SummaryDisplayText(object sender, ASPxGridViewSummaryDisplayTextEventArgs e)
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

        protected void gridprev_pallathur3_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            if (grid.FilterExpression != "")
                if (grid.FilterExpression != grid.FilterExpression)
                {
                    grid.FilterExpression = grid.FilterExpression;
                    grid.SettingsText.Title = grid.FilterExpression;
                }
                else
                    if (grid.FilterExpression != "")
                        grid.FilterExpression = string.Empty;
          
        }

        protected void gridprev_pallathur3_CustomSummaryCalculate(object sender, CustomSummaryEventArgs e)
        {
            if (e.IsTotalSummary)
            {
                ASPxSummaryItem x = (ASPxSummaryItem)e.Item;
                if (x.FieldName.ToString() == "Debit")
                {
                    if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Finalize)
                    {
                        decimal total = 0.00M;
                        for (int i = 0; i < gridprev_pallathur3.VisibleRowCount; i++)
                        {
                            if (gridprev_pallathur3.IsGroupRow(i))
                            {
                                total += Convert.ToDecimal(gridprev_pallathur3.GetGroupSummaryValue(i, gridprev_pallathur3.GroupSummary["Date"]));
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
                        for (int i = 0; i < gridprev_pallathur3.VisibleRowCount; i++)
                        {
                            if (gridprev_pallathur3.IsGroupRow(i))
                            {
                                total += Convert.ToDecimal(gridprev_pallathur3.GetGroupSummaryValue(i, gridprev_pallathur3.GroupSummary["Branch"]));
                            }
                        }
                        e.TotalValue = total;
                        e.TotalValueReady = true;
                    }
                }
                if (x.FieldName.ToString() == "Narration")
                {
                    decimal cre = Convert.ToDecimal(gridprev_pallathur3.GetTotalSummaryValue(gridprev_pallathur3.TotalSummary["Credit"]));
                    decimal deb = Convert.ToDecimal(gridprev_pallathur3.GetTotalSummaryValue(gridprev_pallathur3.TotalSummary["Debit"]));
                    if (cre > deb)
                    {
                       
                        x.DisplayFormat = "Cr.Bal.";
                        e.TotalValue = cre - deb;
                        e.TotalValueReady = true;
                       // ViewState["prevcre"] = cre - deb;
                    }
                    else if (cre < deb)
                    {
                        //x.DisplayFormat = "Dr.Bal.";
                        //e.TotalValue = deb - cre;
                        //e.TotalValueReady = true;
                        x.DisplayFormat = "Dr.Bal.";
                        e.TotalValue = deb - cre;
                        e.TotalValueReady = true;
                        //ViewState["prevdeb"] = deb - cre;
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
                    decimal Credit1 = Convert.ToDecimal(e.GetGroupSummary(e.GroupRowHandle, gridprev_pallathur3.GroupSummary["Credit"]));
                    decimal Debit1 = Convert.ToDecimal(e.GetGroupSummary(e.GroupRowHandle, gridprev_pallathur3.GroupSummary["Debit"]));
                    if (Credit1 <= Debit1)
                    {
                        e.TotalValue = Debit1 - Credit1;

                    }
                }
                if (x.FieldName.ToString() == "Branch")
                {
                    decimal Credit1 = Convert.ToDecimal(e.GetGroupSummary(e.GroupRowHandle, gridprev_pallathur3.GroupSummary["Credit"]));
                    decimal Debit1 = Convert.ToDecimal(e.GetGroupSummary(e.GroupRowHandle, gridprev_pallathur3.GroupSummary["Debit"]));
                    if (Credit1 >= Debit1)
                    {
                        e.TotalValue = Credit1 - Debit1;

                    }
                }
            }
        }

    }
}
