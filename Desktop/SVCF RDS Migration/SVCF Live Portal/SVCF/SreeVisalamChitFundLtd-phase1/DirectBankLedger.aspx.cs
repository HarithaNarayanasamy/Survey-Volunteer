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
using MySql.Data.MySqlClient;

namespace SreeVisalamChitFundLtd_phase1
{
    public partial class DirectBankLedger : System.Web.UI.Page
    {
        #region ObjectDeclaration
		    BusinessLayer balayer = new BusinessLayer();
            TransactionLayer tranlayer = new TransactionLayer();
	    #endregion
            VarDeclaration objVar = new VarDeclaration();
        
        private System.Drawing.Image headerImage;
        protected void Page_Load(object sender, EventArgs e)
        {
            int ddlbankaccid;
            if (!IsPostBack)
            {
                String BrId = balayer.ToobjectstrEvenNull(balayer.ToobjectstrEvenNull(Session["Branchid"]));
                                                                                                                                                                  // Add Dropdown
                DataTable Accbank = new DataTable();
                MySqlConnection con;
                using (con = balayer.OpenConnection())
                {
                    try
                    {
                        MySqlDataAdapter mem2GrpAdp = new MySqlDataAdapter("select concat(BankName,'|',AccountNo) as AccDetails from bankdetails where BranchId='" + BrId + "'", con);
                        mem2GrpAdp.Fill(Accbank);
                        //DataRow dr = ChitGrp.NewRow();
                        //dr[0] = "--select--";
                        //ChitGrp.Rows.InsertAt(dr, 0);
                        ddlbankAcc.Items.Insert(0, "--select--");
                        for (int i = 0; i < Accbank.Rows.Count; i++)
                        {
                            ddlbankAcc.Items.Add(balayer.ToobjectstrEvenNull(Accbank.Rows[i]["AccDetails"]));
                        }
                    }
                    catch (Exception ex)
                    {

                    }
                }
                ddlbankaccid = ddlbankAcc.SelectedIndex = 0;
                select(ddlbankaccid);
                                                                                                                                                               /////////////
                ddlGroup.SelectedIndex = 1;
                dateFromConsolidated.Text = DateTime.Now.ToString("dd/MM/yyyy");
                dateToConsolidated.Text = DateTime.Now.ToString("dd/MM/yyyy");
               
                
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
            }

            // select(ddlbankAcc.SelectedIndex);
            grid.DataSource = select(ddlbankAcc.SelectedIndex);

            grid.DataBind();
        }
        protected void ResetStats()
        {
            lblCrAmount.Text = "0.00";
            lblCrBalance.Text = "0.00";
            lblDrAmount.Text = "0.00";
            lblDrBalance.Text = "0.00";
        }
        protected void btnExportExcel_Click1(object sender, EventArgs e)
        {
            gridexcel.FileName = "DirectBankLedger" + DateTime.Now.Millisecond.ToString();
            grid.DataBind();
            gridexcel.WriteXlsToResponse();
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

        protected DataTable select(int id)
        {
            DataTable finalDt = new DataTable();
            if (id==0)
            {
              //  AccessDataSource1.ConnectionString = CommonClassFile.ConnectionString;
                objVar.varquery = @"select t1.ChoosenDate as `Date`,t8.Node as LedgerHead ,bb.BankName as Bank,bb.AccountNo as `AccountNo.`,tt.CustomersBankName as CustomersBank,tt.ChequeDDNO as `ChequeNo.`,t1.Narration,(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )as `Credit`,(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )as `Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left Join headstree as t8 on (t1.ChitGroupID=t8.NodeID) left join transbank as tt on (t1.TransactionKey=tt.TransactionKey) left join bankdetails as bb on (t1.Head_Id=bb.Head_Id) where `t1`.`BranchID` = " + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and `t1`.`RootID` = 3 and `t1`.`ChoosenDate` between '" + balayer.indiandateToMysqlDate(dateFromConsolidated.Text) + "' and '" + balayer.indiandateToMysqlDate(dateToConsolidated.Text) + "'  and t1.IsDeleted=0 order by t1.ChoosenDate asc";
                finalDt = balayer.GetDataTable(objVar.varquery);
                AccessDataSource2.ConnectionString = CommonClassFile.ConnectionString;
                AccessDataSource2.SelectCommand = @"select t1.ChoosenDate as `Date`,t8.Node as LedgerHead ,bb.BankName as Bank,bb.AccountNo as `AccountNo.`,tt.CustomersBankName as CustomersBank,tt.ChequeDDNO as `ChequeNo.`,t1.Narration,(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )as `Credit`,(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )as `Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left Join headstree as t8 on (t1.ChitGroupID=t8.NodeID) left join transbank as tt on (t1.TransactionKey=tt.TransactionKey) left join bankdetails as bb on (t1.Head_Id=bb.Head_Id) where `t1`.`BranchID` = " + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and `t1`.`RootID` = 3 and `t1`.`ChoosenDate` < '" + balayer.indiandateToMysqlDate(dateFromConsolidated.Text) + "' and t1.IsDeleted=0 order by t1.ChoosenDate asc";
                gridprev.DataBind();
            }
            else
            {
                string valuetext = ddlbankAcc.SelectedItem.Text;
                string splitBank = valuetext.Split('|')[0];
                string splitacc = valuetext.Split('|')[1];
                //grid.SettingsText.Title = "Trial Balance of Bank Ledger" + valuetext + "from " + dateFromConsolidated.Text + " to " + dateToConsolidated.Text;
                //AccessDataSource1.ConnectionString = CommonClassFile.ConnectionString;
                objVar.varquery = @"select t1.ChoosenDate as `Date`,t8.Node as LedgerHead ,bb.BankName as Bank,bb.AccountNo as `AccountNo.`,tt.CustomersBankName as CustomersBank,tt.ChequeDDNO as `ChequeNo.`,t1.Narration,(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )as `Credit`,(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )as `Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left Join headstree as t8 on (t1.ChitGroupID=t8.NodeID) left join transbank as tt on (t1.TransactionKey=tt.TransactionKey) left join bankdetails as bb on (t1.Head_Id=bb.Head_Id) where `t1`.`BranchID` = " + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and `t1`.`RootID` = 3 and `t1`.`ChoosenDate` between '" + balayer.indiandateToMysqlDate(dateFromConsolidated.Text) + "' and '" + balayer.indiandateToMysqlDate(dateToConsolidated.Text) + "'  and t1.IsDeleted=0  and bb.BankName= '" + splitBank + "'and bb.AccountNo='" + splitacc + "'order by t1.ChoosenDate asc";
                finalDt = balayer.GetDataTable(objVar.varquery);
                AccessDataSource2.ConnectionString = CommonClassFile.ConnectionString;
                AccessDataSource2.SelectCommand = @"select t1.ChoosenDate as `Date`,t8.Node as LedgerHead ,bb.BankName as Bank,bb.AccountNo as `AccountNo.`,tt.CustomersBankName as CustomersBank,tt.ChequeDDNO as `ChequeNo.`,t1.Narration,(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )as `Credit`,(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )as `Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left Join headstree as t8 on (t1.ChitGroupID=t8.NodeID) left join transbank as tt on (t1.TransactionKey=tt.TransactionKey) left join bankdetails as bb on (t1.Head_Id=bb.Head_Id) where `t1`.`BranchID` = " + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and `t1`.`RootID` = 3 and `t1`.`ChoosenDate` < '" + balayer.indiandateToMysqlDate(dateFromConsolidated.Text) + "' and t1.IsDeleted=0 and bb.BankName= '" + splitBank + "'and bb.AccountNo='" + splitacc + "' order by t1.ChoosenDate asc";
               
                //grid.TotalSummary["Credit"].SummaryType = SummaryItemType.Sum;
                //grid.TotalSummary["Debit"].SummaryType = SummaryItemType.Sum;
                //grid.SettingsPager.Mode = GridViewPagerMode.ShowAllRecords;
               
                gridprev.DataBind();
                //ApplyLayout(0);
            }
            LoadGridTemp();

            finalDt.Columns[7].DataType = typeof(decimal);
            finalDt.Columns[8].DataType = typeof(decimal);

            if (objVar.tempDt.Rows.Count > 0)
            {
                foreach (DataRow dr in objVar.tempDt.Rows)
                {
                    DataRow newRow = finalDt.NewRow();

                    if (dr.ItemArray[2].ToString() != "" || dr.ItemArray[3].ToString() != "")
                    {
                        if (Convert.ToDecimal(dr.ItemArray[2]) > Convert.ToDecimal(dr.ItemArray[3]))
                        {
                            newRow[6] = dr.ItemArray[0].ToString();
                            newRow[7] = Convert.ToDecimal(dr.ItemArray[2]) - Convert.ToDecimal(dr.ItemArray[3]);
                            newRow[8] = Convert.ToDecimal("0.00");
                        }
                        else if (Convert.ToDecimal(dr.ItemArray[3]) > Convert.ToDecimal(dr.ItemArray[2]))
                        {
                            newRow[6] = dr.ItemArray[0].ToString();
                            newRow[8] = Convert.ToDecimal(dr.ItemArray[3]) - Convert.ToDecimal(dr.ItemArray[2]);
                            newRow[7] = Convert.ToDecimal("0.00");
                        }
                        else
                        {
                            newRow[6] = dr.ItemArray[0].ToString();
                            newRow[8] = Convert.ToDecimal("0.00");
                            newRow[7] = Convert.ToDecimal("0.00");
                        }

                        finalDt.Rows.InsertAt(newRow, 0);

                    }
                }
            }





            finalDt.Columns[7].DataType = typeof(decimal);
            finalDt.Columns[8].DataType = typeof(decimal);


            //foreach (DataRow dr in objVar.tempDt.Rows)
            //{
            //    DataRow newRow = finalDt.NewRow();
            //    newRow[6] = dr.ItemArray[0].ToString() + "(" + dr.ItemArray[1].ToString() + ")";
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
            //    newRow[7] = Convert.ToDecimal(second);
            //    newRow[8] = Convert.ToDecimal(third);
            //    finalDt.Rows.InsertAt(newRow, 0);
            //}


            return finalDt;
        }

        protected void BtnStatisticsGo_Click(object sender, EventArgs e)
        {
            setStats(dateFromConsolidated.Text, dateToConsolidated.Text);
            //select(ddlbankAcc.SelectedIndex);

            grid.DataSource = select(ddlbankAcc.SelectedIndex);

            grid.DataBind();

           
        }
        protected void ASPxGridView1_SummaryDisplayText(object sender, ASPxGridViewSummaryDisplayTextEventArgs e)
        {
            if (!string.IsNullOrEmpty( Convert.ToString( e.Value)))
            {
                string parsed = balayer.ConvertToIndCurrency(e.Value.ToString());
                if (e.Item.FieldName == "Credit")
                {
                    e.Text = "Cr:" + parsed;
                }
                if (e.Item.FieldName == "Debit")
                {
                    e.Text = "Dr:" + parsed;
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
           // grid.DataBind();
        }

        //protected void ASPxGridView1_OnCustomSummaryCalculate(object sender, CustomSummaryEventArgs e)
        //{
        //    if (e.IsTotalSummary)
        //    {
        //        ASPxSummaryItem x = (ASPxSummaryItem)e.Item;
        //        if (x.FieldName.ToString() == "Debit")
        //        {
        //            if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Finalize)
        //            {
        //                decimal total = 0.00M;
        //                for (int i = 0; i < grid.VisibleRowCount; i++)
        //                {
        //                    if (grid.IsGroupRow(i))
        //                    {
        //                        total += Convert.ToDecimal(grid.GetGroupSummaryValue(i, grid.GroupSummary["Bank"]));
        //                    }
        //                }
        //                e.TotalValue = total;
        //                e.TotalValueReady = true;
        //            }
                 

                  
                   
        //        }
        //        if (x.FieldName.ToString() == "Credit")
        //        {
        //            if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Finalize)
        //            {
        //                decimal total = 0.00M;
        //                for (int i = 0; i < grid.VisibleRowCount; i++)
        //                {
        //                    if (grid.IsGroupRow(i))
        //                    {
        //                        total += Convert.ToDecimal(grid.GetGroupSummaryValue(i, grid.GroupSummary["CustomersBank"]));
        //                    }
        //                }
        //                e.TotalValue = total;
        //                e.TotalValueReady = true;
        //            }
                  
        //        }
        //        if (x.FieldName.ToString() == "Narration")
        //        {
        //            decimal cre = Convert.ToDecimal(grid.GetTotalSummaryValue(grid.TotalSummary["Credit"]));
        //            decimal deb = Convert.ToDecimal(grid.GetTotalSummaryValue(grid.TotalSummary["Debit"]));
        //            string ion = "";
        //            var iii = "";
        //            if (cre > 0)
        //            {
        //                //objVar.varquery = "select (case when sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end ) > sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) then CONCAT('Cr.Bal- ',CAST(sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end ) - sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )AS char(50))) else  CONCAT('Dr.Bal-',CAST(sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) - sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )AS char(50)))  end)  as Narration from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left Join headstree as t8 on (t1.ChitGroupID=t8.NodeID) left join transbank as tt on (t1.TransactionKey=tt.TransactionKey) left join bankdetails as bb on (t1.Head_Id=bb.Head_Id) where `t1`.`BranchID` = " + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and `t1`.`RootID` = 3 and `t1`.`ChoosenDate` < '" + balayer.indiandateToMysqlDate(dateFromConsolidated.Text) + "' and t1.IsDeleted=0 order by t1.ChoosenDate asc;";
        //                // ion = balayer.GetSingleValue(objVar.varquery);
        //                // iii = ion.Split('-')[1];

        //                 if (ddlbankAcc.SelectedIndex != 0)
        //                 {
        //                     string valuetext = ddlbankAcc.SelectedItem.Text;
        //                     string splitBank = valuetext.Split('|')[0];
        //                     string splitacc = valuetext.Split('|')[1];
        //                     objVar.varquery = "select (case when sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end ) > sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) then CONCAT('Cr.Bal- ',CAST(sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end ) - sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )AS char(50))) else  CONCAT('Dr.Bal-',CAST(sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) - sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )AS char(50)))  end)  as Narration from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left Join headstree as t8 on (t1.ChitGroupID=t8.NodeID) left join transbank as tt on (t1.TransactionKey=tt.TransactionKey) left join bankdetails as bb on (t1.Head_Id=bb.Head_Id) where `t1`.`BranchID` = " + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and `t1`.`RootID` = 3 and `t1`.`ChoosenDate` < '" + balayer.indiandateToMysqlDate(dateFromConsolidated.Text) + "' and bb.BankName='" + splitBank + "' and bb.AccountNo= '" + splitacc + "'  and t1.IsDeleted=0 order by t1.ChoosenDate asc";
        //                     // objVar.varquery = "select 'Previous Net Balance' as 'Title',(case when sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end ) > sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) then CONCAT('Cr.Bal. ',CAST(sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end ) - sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )AS char(50))) else  CONCAT('Dr.Bal.',CAST(sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) - sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )AS char(50)))  end)  as Narration, CONCAT('Cr. INR ', CAST(sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )AS char(50))) as `Credit`,CONCAT('Dr. INR ',CAST(sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )AS char(50))) as `Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left join headstree as t8 on (t1.ChitGroupID=t8.NodeID) where `t1`.`BranchID` =" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + "  and `t1`.`RootID` = 1 and t1.ChoosenDate < '" + balayer.indiandateToMysqlDate(dateFromConsolidated.Text) + "' and t1.IsDeleted=0 order by t1.ChoosenDate asc;";
        //                     ion = balayer.GetSingleValue(objVar.varquery);
        //                     iii = ion.Split('-')[1];
        //                 }
        //                 else
        //                 {
        //                     objVar.varquery = "select (case when sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end ) > sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) then CONCAT('Cr.Bal- ',CAST(sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end ) - sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )AS char(50))) else  CONCAT('Dr.Bal-',CAST(sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) - sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )AS char(50)))  end)  as Narration from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left Join headstree as t8 on (t1.ChitGroupID=t8.NodeID) left join transbank as tt on (t1.TransactionKey=tt.TransactionKey) left join bankdetails as bb on (t1.Head_Id=bb.Head_Id) where `t1`.`BranchID` = " + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and `t1`.`RootID` = 3 and `t1`.`ChoosenDate` < '" + balayer.indiandateToMysqlDate(dateFromConsolidated.Text) + "' and t1.IsDeleted=0 order by t1.ChoosenDate asc";
        //                     // objVar.varquery = "select 'Previous Net Balance' as 'Title',(case when sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end ) > sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) then CONCAT('Cr.Bal. ',CAST(sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end ) - sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )AS char(50))) else  CONCAT('Dr.Bal.',CAST(sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) - sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )AS char(50)))  end)  as Narration, CONCAT('Cr. INR ', CAST(sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )AS char(50))) as `Credit`,CONCAT('Dr. INR ',CAST(sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )AS char(50))) as `Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left join headstree as t8 on (t1.ChitGroupID=t8.NodeID) where `t1`.`BranchID` =" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + "  and `t1`.`RootID` = 1 and t1.ChoosenDate < '" + balayer.indiandateToMysqlDate(dateFromConsolidated.Text) + "' and t1.IsDeleted=0 order by t1.ChoosenDate asc;";
        //                     ion = balayer.GetSingleValue(objVar.varquery);
        //                     iii = ion.Split('-')[1];
        //                 }
                    
        //            }
        //            if (cre > deb)
        //            {
        //                if (ion.Contains("Cr"))
        //                {
        //                    var val = Convert.ToDecimal(iii);
        //                    var val1 = cre - deb;
        //                    x.DisplayFormat = "Cr.Bal.";
        //                    e.TotalValue = val + val1;
        //                }

        //                if (ion.Contains("Dr"))
        //                {
        //                    decimal val = Convert.ToDecimal(iii);
        //                    decimal val1 = cre - deb;
        //                    if (val > val1)
        //                    {
        //                        x.DisplayFormat = "Dr.Bal.";
        //                        e.TotalValue = val - val1;
        //                    }
        //                    else if (val < val1)
        //                    {
        //                        x.DisplayFormat = "Cr.Bal.";
        //                        e.TotalValue = val1 - val;
        //                    }

        //                }



        //                e.TotalValueReady = true;
        //                //
                        
                       
        //            }
        //            else if (cre < deb)
        //            {
        //                if (ion.Contains("Dr"))
        //                {
        //                    var val = Convert.ToDecimal(iii);
        //                    var val1 = deb - cre;
        //                    x.DisplayFormat = "Dr.Bal.";
        //                    e.TotalValue = val + val1;
        //                }

        //                if (ion.Contains("Cr"))
        //                {
        //                    var val = Convert.ToDecimal(iii);
        //                    var val1 = deb - cre;
        //                    if (val > val1)
        //                    {
        //                        x.DisplayFormat = "Cr.Bal.";
        //                        e.TotalValue = val - val1;
        //                    }
        //                    else if (val < val1)
        //                    {
        //                        x.DisplayFormat = "Dr.Bal.";
        //                        e.TotalValue = val1 - val;
        //                    }

        //                }

        //                e.TotalValueReady = true;
        //            }
        //            else
        //            {
        //                e.TotalValue = 0.00;
        //                e.TotalValueReady = true;
        //            }
        //        }
        //    }
        //    if (e.IsGroupSummary)
        //    {
        //        ASPxSummaryItem x = (ASPxSummaryItem)e.Item;
        //        if (x.FieldName.ToString() == "Bank")
        //        {
        //            decimal Credit1 = Convert.ToDecimal(e.GetGroupSummary(e.GroupRowHandle, grid.GroupSummary["Credit"]));
        //            decimal Debit1 = Convert.ToDecimal(e.GetGroupSummary(e.GroupRowHandle, grid.GroupSummary["Debit"]));
        //            if (Credit1 <= Debit1)
        //            {
        //                e.TotalValue = Debit1 - Credit1;
        //            }
        //        }
        //        if (x.FieldName.ToString() == "CustomersBank")
        //        {
        //            decimal Credit1 = Convert.ToDecimal(e.GetGroupSummary(e.GroupRowHandle, grid.GroupSummary["Credit"]));
        //            decimal Debit1 = Convert.ToDecimal(e.GetGroupSummary(e.GroupRowHandle, grid.GroupSummary["Debit"]));
        //            if (Credit1 >= Debit1)
        //            {
        //                e.TotalValue = Credit1 - Debit1;
        //            }
        //        }
        //    }
        //    //grid.DataBind();
        //}
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
                    string ion = "";
                    var iii = "";

                    if (cre > deb)
                    {
                        var val1 = cre - deb;
                        //x.DisplayFormat = "Cr.Bal.";
                        x.DisplayFormat = " Cr";
                        e.TotalValue = val1;
                    }
                    else if (deb > cre)
                    {
                        var val1 = deb - cre;
                        //x.DisplayFormat = "Cr.Bal.";
                        x.DisplayFormat = "Dr";
                        e.TotalValue = val1;
                    }
                    else
                    {
                        x.DisplayFormat = " ";
                        e.TotalValue = "0.00";
                    }

                    if(x.FieldName.ToString()== "ChequeNo.")
                    {
                        x.DisplayFormat = "";
						e.TotalValue = "NetBalance ";
                    }


                    //if (cre > 0)
                    //{
                    //    if (ddlbankAcc.SelectedIndex != 0)
                    //    {
                    //        string valuetext = ddlbankAcc.SelectedItem.Text;
                    //        string splitBank = valuetext.Split('|')[0];
                    //        string splitacc = valuetext.Split('|')[1];
                    //        objVar.varquery = "select (case when sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end ) > sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) then CONCAT('Cr.Bal- ',CAST(sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end ) - sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )AS char(50))) else  CONCAT('Dr.Bal-',CAST(sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) - sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )AS char(50)))  end)  as Narration from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left Join headstree as t8 on (t1.ChitGroupID=t8.NodeID) left join transbank as tt on (t1.TransactionKey=tt.TransactionKey) left join bankdetails as bb on (t1.Head_Id=bb.Head_Id) where `t1`.`BranchID` = " + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and `t1`.`RootID` = 3 and `t1`.`ChoosenDate` < '" + balayer.indiandateToMysqlDate(dateFromConsolidated.Text) + "' and bb.BankName='" + splitBank + "' and bb.AccountNo= '" + splitacc + "'  and t1.IsDeleted=0 order by t1.ChoosenDate asc";
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
                    //        objVar.varquery = "select (case when sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end ) > sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) then CONCAT('Cr.Bal- ',CAST(sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end ) - sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )AS char(50))) else  CONCAT('Dr.Bal-',CAST(sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) - sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )AS char(50)))  end)  as Narration from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left Join headstree as t8 on (t1.ChitGroupID=t8.NodeID) left join transbank as tt on (t1.TransactionKey=tt.TransactionKey) left join bankdetails as bb on (t1.Head_Id=bb.Head_Id) where `t1`.`BranchID` = " + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and `t1`.`RootID` = 3 and `t1`.`ChoosenDate` < '" + balayer.indiandateToMysqlDate(dateFromConsolidated.Text) + "' and t1.IsDeleted=0 order by t1.ChoosenDate asc";
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
                    //if (cre > deb)
                    //{
                    //    if (ion.Contains("Cr"))
                    //    {
                    //        var val = Convert.ToDecimal(iii);
                    //        var val1 = cre - deb;
                    //        x.DisplayFormat = "Cr.Bal.";
                    //        e.TotalValue = val + val1;
                    //    }

                    //    else if (ion.Contains("Dr"))
                    //    {
                    //        decimal val = Convert.ToDecimal(iii);
                    //        decimal val1 = cre - deb;
                    //        if (val > val1)
                    //        {
                    //            x.DisplayFormat = "Dr.Bal.";
                    //            e.TotalValue = val - val1;
                    //        }
                    //        else if (val < val1)
                    //        {
                    //            x.DisplayFormat = "Cr.Bal.";
                    //            e.TotalValue = val1 - val;
                    //        }

                    //    }
                    //    else if (ion == "")
                    //    {
                    //        x.DisplayFormat = "Cr.Bal.";
                    //        e.TotalValue = cre - deb;
                    //    }



                    //    e.TotalValueReady = true;
                    //    //


                    //}
                    //else if (cre < deb)
                    //{
                    //    if (ion.Contains("Dr"))
                    //    {
                    //        var val = Convert.ToDecimal(iii);
                    //        var val1 = deb - cre;
                    //        x.DisplayFormat = "Dr.Bal.";
                    //        e.TotalValue = val + val1;
                    //    }

                    //    if (ion.Contains("Cr"))
                    //    {
                    //        var val = Convert.ToDecimal(iii);
                    //        var val1 = deb - cre;
                    //        if (val > val1)
                    //        {
                    //            x.DisplayFormat = "Cr.Bal.";
                    //            e.TotalValue = val - val1;
                    //        }
                    //        else if (val < val1)
                    //        {
                    //            x.DisplayFormat = "Dr.Bal.";
                    //            e.TotalValue = val1 - val;
                    //        }

                    //    }
                    //    else if (ion == "")
                    //    {

                    //        x.DisplayFormat = "Dr.Bal";
                    //        e.TotalValue = deb - cre;
                    //    }

                    //    e.TotalValueReady = true;
                    //}
                    //else
                    //{
                    //    if (balayer.indiandateToMysqlDate(dateFromConsolidated.Text) != balayer.indiandateToMysqlDate(dateToConsolidated.Text))
                    //    {
                    //        if (ViewState["prevcre"] != null)
                    //        {
                    //            x.DisplayFormat = "Cr.Bal.";
                    //            e.TotalValue = ViewState["prevcre"];
                    //        }
                    //        if (ViewState["prevdeb"] != null)
                    //        {
                    //            x.DisplayFormat = "Dr.Bal";
                    //            e.TotalValue = ViewState["prevdeb"];
                    //        }
                    //    }
                    //    else
                    //    {
                    //        e.TotalValue = 0.00;
                    //        e.TotalValueReady = true;
                    //    }
                    //}
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
            //grid.DataBind();
        }
        protected void ddlbankAcc_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlbankAcc.SelectedIndex != 0)
            {
                string valuetext = ddlbankAcc.SelectedItem.Text;
                string splitBank = valuetext.Split('|')[0];
                string splitacc = valuetext.Split('|')[1];
                select(ddlbankAcc.SelectedIndex);
                grid.DataBind();
                gridprev.DataBind();
            }
        }
        protected void ddlGroup_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlGroup.SelectedIndex == 0)
            {
                select(ddlbankAcc.SelectedIndex);
                ApplyLayout(0);
                grid.TotalSummary["Credit"].SummaryType = SummaryItemType.Sum;
                grid.TotalSummary["Debit"].SummaryType = SummaryItemType.Sum;
                grid.SettingsPager.Mode = GridViewPagerMode.ShowPager;
            }
            else if (ddlGroup.SelectedIndex == 1)
            {
                select(ddlbankAcc.SelectedIndex);
                ApplyLayout(1);
                grid.TotalSummary["Credit"].SummaryType = SummaryItemType.Custom;
                grid.TotalSummary["Debit"].SummaryType = SummaryItemType.Custom;
                grid.SettingsPager.Mode = GridViewPagerMode.ShowAllRecords;
            }
            else
            {
                select(ddlbankAcc.SelectedIndex);
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
                        //grid.GroupBy(grid.Columns["LedgerHead"]);
                        grid.GroupBy(grid.Columns["Date"]);
                        break;
                    case 2:
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
        public DataTable LoadGridTemp()
        {
            objVar.tempDt = new DataTable();
            if (grid.FilterExpression != "")
            {
               
            }
            else
            {
                if (ddlbankAcc.SelectedIndex != 0)
                {
                    string valuetext = ddlbankAcc.SelectedItem.Text;
                    string splitBank = valuetext.Split('|')[0];
                    string splitacc = valuetext.Split('|')[1];
                    objVar.varquery = "select 'Previous Net Balance' as 'Title', (case when sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end ) > sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) then CONCAT('Cr.Bal. ',CAST(sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end ) - sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )AS char(50))) else  CONCAT('Dr.Bal.',CAST(sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) - sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )AS char(50)))  end)  as Narration ,sum((case when t1.Voucher_Type='C' then t1.Amount else 0.00 end ))as `Credit`,sum((case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ))as `Debit`from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left Join headstree as t8 on (t1.ChitGroupID=t8.NodeID) left join transbank as tt on (t1.TransactionKey=tt.TransactionKey) left join bankdetails as bb on (t1.Head_Id=bb.Head_Id) where `t1`.`BranchID` = " + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and `t1`.`RootID` = 3 and `t1`.`ChoosenDate` < '" + balayer.indiandateToMysqlDate(dateFromConsolidated.Text) + "' and bb.BankName='" + splitBank + "' and bb.AccountNo= '" + splitacc + "'  and t1.IsDeleted=0 order by t1.ChoosenDate asc";
                    // objVar.varquery = "select 'Previous Net Balance' as 'Title',(case when sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end ) > sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) then CONCAT('Cr.Bal. ',CAST(sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end ) - sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )AS char(50))) else  CONCAT('Dr.Bal.',CAST(sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) - sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )AS char(50)))  end)  as Narration, CONCAT('Cr. INR ', CAST(sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )AS char(50))) as `Credit`,CONCAT('Dr. INR ',CAST(sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )AS char(50))) as `Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left join headstree as t8 on (t1.ChitGroupID=t8.NodeID) where `t1`.`BranchID` =" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + "  and `t1`.`RootID` = 1 and t1.ChoosenDate < '" + balayer.indiandateToMysqlDate(dateFromConsolidated.Text) + "' and t1.IsDeleted=0 order by t1.ChoosenDate asc;";
                    objVar.tempDt = balayer.GetDataTable(objVar.varquery);
                }
                else
                {
                    objVar.varquery = "select 'Previous Net Balance ' as 'Title',(case when sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end ) > sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) then CONCAT('Cr.Bal. ',CAST(sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end ) - sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )AS char(50))) else  CONCAT('Dr.Bal.',CAST(sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) - sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )AS char(50)))  end)  as Narration, CONCAT('', CAST(sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )AS char(50))) as `Credit`,CONCAT('',CAST(sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )AS char(50))) as `Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left Join headstree as t8 on (t1.ChitGroupID=t8.NodeID) left join transbank as tt on (t1.TransactionKey=tt.TransactionKey) left join bankdetails as bb on (t1.Head_Id=bb.Head_Id) where `t1`.`BranchID` = " + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and `t1`.`RootID` = 3 and `t1`.`ChoosenDate` < '" + balayer.indiandateToMysqlDate(dateFromConsolidated.Text) + "' and t1.IsDeleted=0 order by t1.ChoosenDate asc";
                    // objVar.varquery = "select 'Previous Net Balance' as 'Title',(case when sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end ) > sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) then CONCAT('Cr.Bal. ',CAST(sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end ) - sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )AS char(50))) else  CONCAT('Dr.Bal.',CAST(sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) - sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )AS char(50)))  end)  as Narration, CONCAT('Cr. INR ', CAST(sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )AS char(50))) as `Credit`,CONCAT('Dr. INR ',CAST(sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )AS char(50))) as `Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left join headstree as t8 on (t1.ChitGroupID=t8.NodeID) where `t1`.`BranchID` =" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + "  and `t1`.`RootID` = 1 and t1.ChoosenDate < '" + balayer.indiandateToMysqlDate(dateFromConsolidated.Text) + "' and t1.IsDeleted=0 order by t1.ChoosenDate asc;";
                    objVar.tempDt = balayer.GetDataTable(objVar.varquery);
                }
            }

            return objVar.tempDt;
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
                    gridcheque.Component = gridExport;

                    gridprev.Settings.ShowColumnHeaders = false;
                    gridprev.Settings.ShowHeaderFilterButton = false;
                    gridprev.Settings.ShowFooter = true;
                    gridprev.Settings.ShowFilterRow = false;
                    gridprev.Settings.ShowFilterRowMenu = false;
                    gridprev.Settings.ShowGroupPanel = false;
                    gridprev.Settings.ShowGroupedColumns = true;
                    gridprev.Visible = false;
                    //grdTemp.DataSource = LoadGridTemp();
                    //grdTemp.DataBind();
                    grdTemp.Visible = false;

                    grid.DataSource = select(ddlbankAcc.SelectedIndex);
                    grid.SettingsText.Title = "";
                    grid.DataBind();

                    gridchequeprev.Component = gridExportprev;
                    gridExport.PreserveGroupRowStates = true;
                  //  gridExportprev.PreserveGroupRowStates = true;
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
                        WriteToResponse("BankLedger", true, "pdf", stream);
                    }
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
            tb.Rect = new RectangleF(280, 8, (e.Graph.ClientPageSize.Width / 2), 20);

            tb.BorderWidth = 0;
            tb.BackColor = Color.Transparent;
            tb.HorzAlignment = DevExpress.Utils.HorzAlignment.Center;
            tb.VertAlignment = DevExpress.Utils.VertAlignment.Top;
            e.Graph.DrawBrick(tb);

            TextBrick tb1 = new TextBrick();
            tb1.Text = "Branch : " + Session["BranchName"];
            tb1.Font = new Font("Arial", 9);
            tb1.Rect = new RectangleF(280, 28, (e.Graph.ClientPageSize.Width / 2), 20);
            tb1.BorderWidth = 0;
            tb1.BackColor = Color.Transparent;
            tb1.HorzAlignment = DevExpress.Utils.HorzAlignment.Center;
            tb1.VertAlignment = DevExpress.Utils.VertAlignment.Top;
            e.Graph.DrawBrick(tb1);



            if (ddlbankAcc.SelectedItem.Text != "--select--")
            {
                string valuetext = ddlbankAcc.SelectedItem.Text;
                string splitBank = valuetext.Split('|')[0];
                string splitacc = valuetext.Split('|')[1];
                TextBrick tb2 = new TextBrick();
                tb2.Text = "Banks " + splitBank + " A/C No " + splitacc;
                tb2.Font = new Font("Arial", 9);
                tb2.Rect = new RectangleF(280, 48, (e.Graph.ClientPageSize.Width / 2), 20);
                tb2.BorderWidth = 0;
                tb2.BackColor = Color.Transparent;
                tb2.HorzAlignment = DevExpress.Utils.HorzAlignment.Center;
                tb2.VertAlignment = DevExpress.Utils.VertAlignment.Top;
                e.Graph.DrawBrick(tb2);

                TextBrick tb3 = new TextBrick();
                tb3.Text = "From : " + dateFromConsolidated.Text + " to " + dateToConsolidated.Text;
                tb3.Font = new Font("Arial", 9);
                tb3.Rect = new RectangleF(280, 68, (e.Graph.ClientPageSize.Width / 2), 20);
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
                tb3.Rect = new RectangleF(280, 48, (e.Graph.ClientPageSize.Width / 2), 20);
                tb3.BorderWidth = 0;
                tb3.BackColor = Color.Transparent;
                tb3.HorzAlignment = DevExpress.Utils.HorzAlignment.Center;
                tb3.VertAlignment = DevExpress.Utils.VertAlignment.Top;
                e.Graph.DrawBrick(tb3);
            }
            
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
                        ViewState["prevcre"] = cre - deb;
                    }
                    else if (cre < deb)
                    {
                        x.DisplayFormat = "Dr.Bal.";
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
    }
}
