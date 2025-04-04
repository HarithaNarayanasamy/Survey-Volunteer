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
using SVCF_BusinessAccessLayer;
using SVCF_DataAccessLayer;
using SVCF_TransactionLayer;

namespace SreeVisalamChitFundLtd_phase1
{
    public partial class NewTopColunDayBook : System.Web.UI.Page
    {
        #region Object
        BusinessLayer balayer = new BusinessLayer();
        TransactionLayer trn = new TransactionLayer();
        #endregion
        protected void btnLoanConsolidated_Click(object sender, EventArgs e)
        {
            setStats(dateFromConsolidated.Date.ToString("yyyy/MM/dd"), dateToConsolidated.Date.ToString("yyyy/MM/dd"));
            select();
            grid.DataBind();
            gridInvestments.DataBind();
            gridBanks.DataBind();
            gridOtherItems.DataBind();
            gridChits.DataBind();
            gridCash.DataBind();
            gridDecreeDebtors.DataBind();
            gridLoans.DataBind();
            gridForemanChits.DataBind();
            gridStamps.DataBind();
            gridProfitAndLoss.DataBind();
            gridSundriesAndAdvances.DataBind();
        
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                dateToConsolidated.Text = DateTime.Now.ToString("dd/MM/yyyy"); ;
                dateFromConsolidated.Text = dateToConsolidated.Date.AddDays(-1).ToString("dd/MM/yyyy");
                select();
                grid.DataBind();
                foreach (GridViewColumn column in grid.Columns)
                {
                    if (column is GridViewDataColumn)
                    {
                        ((GridViewDataColumn)column).Settings.AutoFilterCondition = AutoFilterCondition.Contains;
                    }
                }
            }
            else
            {
                select();
            }
        }
        protected void ResetStats()
        {//Branch
            lblCrBranchAmountFinal.Text = "0.00";
            lblCrBranchBalanceFinal.Text = "0.00";
            lblDrBranchAmountFinal.Text = "0.00";
            lblDrBranchBalanceFinal.Text = "0.00";
            lblCrBranchAmountprDay.Text = "0.00";
            lblCrBranchBalanceprDay.Text = "0.00";
            lblDrBranchAmountprDay.Text = "0.00";
            lblDrBranchBalanceprDay.Text = "0.00";
            lblCrBranchAmountprDay.Text = "0.00";
            lblCrBranchBalanceprDay.Text = "0.00";
            lblDrBranchAmountprDay.Text = "0.00";
            lblDrBranchBalanceprDay.Text = "0.00"; 
            //Investments

            lblCrInvestmentsAmountFinal.Text = "0.00";
            lblCrInvestmentsBalanceFinal.Text = "0.00";
            lblDrInvestmentsAmountFinal.Text = "0.00";
            lblDrInvestmentsBalanceFinal.Text = "0.00";
            lblCrInvestmentsAmountprDay.Text = "0.00";
            lblCrInvestmentsBalanceprDay.Text = "0.00";
            lblDrInvestmentsAmountprDay.Text = "0.00";
            lblDrInvestmentsBalanceprDay.Text = "0.00";
            lblCrInvestmentsAmountprDay.Text = "0.00";
            lblCrInvestmentsBalanceprDay.Text = "0.00";
            lblDrInvestmentsAmountprDay.Text = "0.00";
            lblDrInvestmentsBalanceprDay.Text = "0.00"; 

            //Banks
            lblCrBanksAmountFinal.Text = "0.00";
            lblCrBanksBalanceFinal.Text = "0.00";
            lblDrBanksAmountFinal.Text = "0.00";
            lblDrBanksBalanceFinal.Text = "0.00";
            lblCrBanksAmountprDay.Text = "0.00";
            lblCrBanksBalanceprDay.Text = "0.00";
            lblDrBanksAmountprDay.Text = "0.00";
            lblDrBanksBalanceprDay.Text = "0.00";
            lblCrBanksAmountprDay.Text = "0.00";
            lblCrBanksBalanceprDay.Text = "0.00";
            lblDrBanksAmountprDay.Text = "0.00";
            lblDrBanksBalanceprDay.Text = "0.00";
            //Other Items
            lblCrOtherItemsAmountFinal.Text = "0.00";
            lblCrOtherItemsBalanceFinal.Text = "0.00";
            lblDrOtherItemsAmountFinal.Text = "0.00";
            lblDrOtherItemsBalanceFinal.Text = "0.00";
            lblCrOtherItemsAmountprDay.Text = "0.00";
            lblCrOtherItemsBalanceprDay.Text = "0.00";
            lblDrOtherItemsAmountprDay.Text = "0.00";
            lblDrOtherItemsBalanceprDay.Text = "0.00";
            lblCrOtherItemsAmountprDay.Text = "0.00";
            lblCrOtherItemsBalanceprDay.Text = "0.00";
            lblDrOtherItemsAmountprDay.Text = "0.00";
            lblDrOtherItemsBalanceprDay.Text = "0.00"; 
            //chit

            lblCrChitsAmountFinal.Text = "0.00";
            lblCrChitsBalanceFinal.Text = "0.00";
            lblDrChitsAmountFinal.Text = "0.00";
            lblDrChitsBalanceFinal.Text = "0.00";
            lblCrChitsAmountprDay.Text = "0.00";
            lblCrChitsBalanceprDay.Text = "0.00";
            lblDrChitsAmountprDay.Text = "0.00";
            lblDrChitsBalanceprDay.Text = "0.00";
            lblCrChitsAmountprDay.Text = "0.00";
            lblCrChitsBalanceprDay.Text = "0.00";
            lblDrChitsAmountprDay.Text = "0.00";
            lblDrChitsBalanceprDay.Text = "0.00"; 
            //Foreman Chits

            lblCrForemanChitsAmountFinal.Text = "0.00";
            lblCrForemanChitsBalanceFinal.Text = "0.00";
            lblDrForemanChitsAmountFinal.Text = "0.00";
            lblDrForemanChitsBalanceFinal.Text = "0.00";
            lblCrForemanChitsAmountprDay.Text = "0.00";
            lblCrForemanChitsBalanceprDay.Text = "0.00";
            lblDrForemanChitsAmountprDay.Text = "0.00";
            lblDrForemanChitsBalanceprDay.Text = "0.00";
            lblCrForemanChitsAmountprDay.Text = "0.00";
            lblCrForemanChitsBalanceprDay.Text = "0.00";
            lblDrForemanChitsAmountprDay.Text = "0.00";
            lblDrForemanChitsBalanceprDay.Text = "0.00"; 

            //DecreeDebtors

            lblCrDecreeDebtorsAmountFinal.Text = "0.00";
            lblCrDecreeDebtorsBalanceFinal.Text = "0.00";
            lblDrDecreeDebtorsAmountFinal.Text = "0.00";
            lblDrDecreeDebtorsBalanceFinal.Text = "0.00";
            lblCrDecreeDebtorsAmountprDay.Text = "0.00";
            lblCrDecreeDebtorsBalanceprDay.Text = "0.00";
            lblDrDecreeDebtorsAmountprDay.Text = "0.00";
            lblDrDecreeDebtorsBalanceprDay.Text = "0.00";
            lblCrDecreeDebtorsAmountprDay.Text = "0.00";
            lblCrDecreeDebtorsBalanceprDay.Text = "0.00";
            lblDrDecreeDebtorsAmountprDay.Text = "0.00";
            lblDrDecreeDebtorsBalanceprDay.Text = "0.00"; 

            //loans

            lblCrLoansAmountFinal.Text = "0.00";
            lblCrLoansBalanceFinal.Text = "0.00";
            lblDrLoansAmountFinal.Text = "0.00";
            lblDrLoansBalanceFinal.Text = "0.00";
            lblCrLoansAmountprDay.Text = "0.00";
            lblCrLoansBalanceprDay.Text = "0.00";
            lblDrLoansAmountprDay.Text = "0.00";
            lblDrLoansBalanceprDay.Text = "0.00";
            lblCrLoansAmountprDay.Text = "0.00";
            lblCrLoansBalanceprDay.Text = "0.00";
            lblDrLoansAmountprDay.Text = "0.00";
            lblDrLoansBalanceprDay.Text = "0.00"; 

            //SundriesAndAdvances
            lblCrSundriesAndAdvancesAmountFinal.Text = "0.00";
            lblCrSundriesAndAdvancesBalanceFinal.Text = "0.00";
            lblDrSundriesAndAdvancesAmountFinal.Text = "0.00";
            lblDrSundriesAndAdvancesBalanceFinal.Text = "0.00";
            lblCrSundriesAndAdvancesAmountprDay.Text = "0.00";
            lblCrSundriesAndAdvancesBalanceprDay.Text = "0.00";
            lblDrSundriesAndAdvancesAmountprDay.Text = "0.00";
            lblDrSundriesAndAdvancesBalanceprDay.Text = "0.00";
            lblCrSundriesAndAdvancesAmountprDay.Text = "0.00";
            lblCrSundriesAndAdvancesBalanceprDay.Text = "0.00";
            lblDrSundriesAndAdvancesAmountprDay.Text = "0.00";
            lblDrSundriesAndAdvancesBalanceprDay.Text = "0.00";

            //Stamps
            lblCrStampsAmountFinal.Text = "0.00";
            lblCrStampsBalanceFinal.Text = "0.00";
            lblDrStampsAmountFinal.Text = "0.00";
            lblDrStampsBalanceFinal.Text = "0.00";
            lblCrStampsAmountprDay.Text = "0.00";
            lblCrStampsBalanceprDay.Text = "0.00";
            lblDrStampsAmountprDay.Text = "0.00";
            lblDrStampsBalanceprDay.Text = "0.00";
            lblCrStampsAmountprDay.Text = "0.00";
            lblCrStampsBalanceprDay.Text = "0.00";
            lblDrStampsAmountprDay.Text = "0.00";
            lblDrStampsBalanceprDay.Text = "0.00"; 

            //Profit And Loss

            lblCrProfitAndLossAmountFinal.Text = "0.00";
            lblCrProfitAndLossBalanceFinal.Text = "0.00";
            lblDrProfitAndLossAmountFinal.Text = "0.00";
            lblDrProfitAndLossBalanceFinal.Text = "0.00";
            lblCrProfitAndLossAmountprDay.Text = "0.00";
            lblCrProfitAndLossBalanceprDay.Text = "0.00";
            lblDrProfitAndLossAmountprDay.Text = "0.00";
            lblDrProfitAndLossBalanceprDay.Text = "0.00";
            lblCrProfitAndLossAmountprDay.Text = "0.00";
            lblCrProfitAndLossBalanceprDay.Text = "0.00";
            lblDrProfitAndLossAmountprDay.Text = "0.00";
            lblDrProfitAndLossBalanceprDay.Text = "0.00"; 

            //Cash
            lblCrCashAmountFinal.Text = "0.00";
            lblCrCashBalanceFinal.Text = "0.00";
            lblDrCashAmountFinal.Text = "0.00";
            lblDrCashBalanceFinal.Text = "0.00";
            lblCrCashAmountprDay.Text = "0.00";
            lblCrCashBalanceprDay.Text = "0.00";
            lblDrCashAmountprDay.Text = "0.00";
            lblDrCashBalanceprDay.Text = "0.00";
            lblCrCashAmountprDay.Text = "0.00";
            lblCrCashBalanceprDay.Text = "0.00";
            lblDrCashAmountprDay.Text = "0.00";
            lblDrCashBalanceprDay.Text = "0.00"; 
        }

        protected void setStats(string Fromdate, string Todate)
        {
            ResetStats();
            
            //Branch
            DataTable dtStats = balayer.GetDataTable(" SELECT RootID,sum((case when Voucher_Type='C' then Amount else 0.00 end ))as `Credit Amount`,sum((case when Voucher_Type='D' then Amount else 0.00 end ))as `Debit Amount` FROM `svcf`.`voucher` where BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and ChoosenDate between '" + Fromdate + "' and '" + Todate + "' and RootID=1 group by RootID");
            if (dtStats.Rows.Count == 1)
            {

                decimal decCredit = 0.00M;
                decimal decDebit = 0.00M;
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
                lblCrBranchAmountFinal.Text = decCredit.ToString();
                lblCrBranchBalanceFinal.Text = decCreditBalance.ToString();
                lblDrBranchAmountFinal.Text = decDebit.ToString();
                lblDrBranchBalanceFinal.Text = decDebitBalance.ToString();
            }

            DataTable dtStatsPreviousday = balayer.GetDataTable(" SELECT RootID,sum((case when Voucher_Type='C' then Amount else 0.00 end ))as `Credit Amount`,sum((case when Voucher_Type='D' then Amount else 0.00 end ))as `Debit Amount` FROM `svcf`.`voucher` where BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and ChoosenDate between '" + Fromdate + "' and '" + dateToConsolidated.Date.AddDays(-1).ToString("yyyy/MM/dd") + "' and RootID=1 group by RootID");
            if (dtStatsPreviousday.Rows.Count == 1)
            {

                decimal decCredit = 0.00M;
                decimal decDebit = 0.00M;
                decimal decDebitBalance = 0.00M;
                decimal decCreditBalance = 0.00M;
                decimal.TryParse(balayer.ToobjectstrEvenNull(dtStatsPreviousday.Rows[0][1].ToString()), out decCredit);
                decimal.TryParse(balayer.ToobjectstrEvenNull(dtStatsPreviousday.Rows[0][2].ToString()), out decDebit);
                if ((decCredit - decDebit) > 0.00M)
                {
                    decCreditBalance = decCredit - decDebit;
                }
                else if ((decDebit - decCredit) > 0.00M)
                {
                    decDebitBalance = decDebit - decCredit;
                }
                lblCrBranchAmountprDay.Text = decCredit.ToString();
                lblCrBranchBalanceprDay.Text = decCreditBalance.ToString();
                lblDrBranchAmountprDay.Text = decDebit.ToString();
                lblDrBranchBalanceprDay.Text = decDebitBalance.ToString();
            }


            DataTable dtStatsCurrentDay = balayer.GetDataTable(" SELECT RootID,sum((case when Voucher_Type='C' then Amount else 0.00 end ))as `Credit Amount`,sum((case when Voucher_Type='D' then Amount else 0.00 end ))as `Debit Amount` FROM `svcf`.`voucher` where BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and ChoosenDate = '" + Todate  + "' and RootID=1 group by RootID");
            if (dtStatsCurrentDay.Rows.Count == 1)
            {

                decimal decCredit = 0.00M;
                decimal decDebit = 0.00M;
                decimal decDebitBalance = 0.00M;
                decimal decCreditBalance = 0.00M;
                decimal.TryParse(balayer.ToobjectstrEvenNull(dtStatsCurrentDay.Rows[0][1].ToString()), out decCredit);
                decimal.TryParse(balayer.ToobjectstrEvenNull(dtStatsCurrentDay.Rows[0][2].ToString()), out decDebit);
                if ((decCredit - decDebit) > 0.00M)
                {
                    decCreditBalance = decCredit - decDebit;
                }
                else if ((decDebit - decCredit) > 0.00M)
                {
                    decDebitBalance = decDebit - decCredit;
                }
                lblCrBranchAmount.Text = decCredit.ToString();
                lblCrBranchBalance.Text = decCreditBalance.ToString();
                lblDrBranchAmount.Text = decDebit.ToString();
                lblDrBranchBalance.Text = decDebitBalance.ToString();
            }

            //Investments
            DataTable dtStatsInvestments = balayer.GetDataTable(" SELECT RootID,sum((case when Voucher_Type='C' then Amount else 0.00 end ))as `Credit Amount`,sum((case when Voucher_Type='D' then Amount else 0.00 end ))as `Debit Amount` FROM `svcf`.`voucher` where BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and ChoosenDate between '" + Fromdate + "' and '" + Todate + "' and RootID=2 group by RootID");
            if (dtStatsInvestments.Rows.Count == 1)
            {

                decimal decCredit = 0.00M;
                decimal decDebit = 0.00M;
                decimal decDebitBalance = 0.00M;
                decimal decCreditBalance = 0.00M;
                decimal.TryParse(balayer.ToobjectstrEvenNull(dtStatsInvestments.Rows[0][1].ToString()), out decCredit);
                decimal.TryParse(balayer.ToobjectstrEvenNull(dtStatsInvestments.Rows[0][2].ToString()), out decDebit);
                if ((decCredit - decDebit) > 0.00M)
                {
                    decCreditBalance = decCredit - decDebit;
                }
                else if ((decDebit - decCredit) > 0.00M)
                {
                    decDebitBalance = decDebit - decCredit;
                }
                lblCrInvestmentsAmountFinal.Text = decCredit.ToString();
                lblCrInvestmentsBalanceFinal.Text = decCreditBalance.ToString();
                lblDrInvestmentsAmountFinal.Text = decDebit.ToString();
                lblDrInvestmentsBalanceFinal.Text = decDebitBalance.ToString();
            }

            DataTable dtStatsPreviousdayInvestments = balayer.GetDataTable(" SELECT RootID,sum((case when Voucher_Type='C' then Amount else 0.00 end ))as `Credit Amount`,sum((case when Voucher_Type='D' then Amount else 0.00 end ))as `Debit Amount` FROM `svcf`.`voucher` where BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and ChoosenDate between '" + Fromdate + "' and '" + dateToConsolidated.Date.AddDays(-1).ToString("yyyy/MM/dd") + "' and RootID=2 group by RootID");
            if (dtStatsPreviousdayInvestments.Rows.Count == 1)
            {

                decimal decCredit = 0.00M;
                decimal decDebit = 0.00M;
                decimal decDebitBalance = 0.00M;
                decimal decCreditBalance = 0.00M;
                decimal.TryParse(balayer.ToobjectstrEvenNull(dtStatsPreviousdayInvestments.Rows[0][1].ToString()), out decCredit);
                decimal.TryParse(balayer.ToobjectstrEvenNull(dtStatsPreviousdayInvestments.Rows[0][2].ToString()), out decDebit);
                if ((decCredit - decDebit) > 0.00M)
                {
                    decCreditBalance = decCredit - decDebit;
                }
                else if ((decDebit - decCredit) > 0.00M)
                {
                    decDebitBalance = decDebit - decCredit;
                }
                lblCrInvestmentsAmountprDay.Text = decCredit.ToString();
                lblCrInvestmentsBalanceprDay.Text = decCreditBalance.ToString();
                lblDrInvestmentsAmountprDay.Text = decDebit.ToString();
                lblDrInvestmentsBalanceprDay.Text = decDebitBalance.ToString();
            }


            DataTable dtStatsCurrentDayInvestments = balayer.GetDataTable(" SELECT RootID,sum((case when Voucher_Type='C' then Amount else 0.00 end ))as `Credit Amount`,sum((case when Voucher_Type='D' then Amount else 0.00 end ))as `Debit Amount` FROM `svcf`.`voucher` where BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and ChoosenDate = '" + Todate + "' and RootID=2 group by RootID");
            if (dtStatsCurrentDayInvestments.Rows.Count == 1)
            {

                decimal decCredit = 0.00M;
                decimal decDebit = 0.00M;
                decimal decDebitBalance = 0.00M;
                decimal decCreditBalance = 0.00M;
                decimal.TryParse(balayer.ToobjectstrEvenNull(dtStatsCurrentDayInvestments.Rows[0][1].ToString()), out decCredit);
                decimal.TryParse(balayer.ToobjectstrEvenNull(dtStatsCurrentDayInvestments.Rows[0][2].ToString()), out decDebit);
                if ((decCredit - decDebit) > 0.00M)
                {
                    decCreditBalance = decCredit - decDebit;
                }
                else if ((decDebit - decCredit) > 0.00M)
                {
                    decDebitBalance = decDebit - decCredit;
                }
                lblCrInvestmentsAmount.Text = decCredit.ToString();
                lblCrInvestmentsBalance.Text = decCreditBalance.ToString();
                lblDrInvestmentsAmount.Text = decDebit.ToString();
                lblDrInvestmentsBalance.Text = decDebitBalance.ToString();
            }

            //Banks
            DataTable dtStatsBanks = balayer.GetDataTable(" SELECT RootID,sum((case when Voucher_Type='C' then Amount else 0.00 end ))as `Credit Amount`,sum((case when Voucher_Type='D' then Amount else 0.00 end ))as `Debit Amount` FROM `svcf`.`voucher` where BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and ChoosenDate between '" + Fromdate + "' and '" + Todate + "' and RootID=3 group by RootID");
            if (dtStatsBanks.Rows.Count == 1)
            {

                decimal decCredit = 0.00M;
                decimal decDebit = 0.00M;
                decimal decDebitBalance = 0.00M;
                decimal decCreditBalance = 0.00M;
                decimal.TryParse(balayer.ToobjectstrEvenNull(dtStatsBanks.Rows[0][1].ToString()), out decCredit);
                decimal.TryParse(balayer.ToobjectstrEvenNull(dtStatsBanks.Rows[0][2].ToString()), out decDebit);
                if ((decCredit - decDebit) > 0.00M)
                {
                    decCreditBalance = decCredit - decDebit;
                }
                else if ((decDebit - decCredit) > 0.00M)
                {
                    decDebitBalance = decDebit - decCredit;
                }
                lblCrBanksAmountFinal.Text = decCredit.ToString();
                lblCrBanksBalanceFinal.Text = decCreditBalance.ToString();
                lblDrBanksAmountFinal.Text = decDebit.ToString();
                lblDrBanksBalanceFinal.Text = decDebitBalance.ToString();
            }

            DataTable dtStatsPreviousdayBanks = balayer.GetDataTable(" SELECT RootID,sum((case when Voucher_Type='C' then Amount else 0.00 end ))as `Credit Amount`,sum((case when Voucher_Type='D' then Amount else 0.00 end ))as `Debit Amount` FROM `svcf`.`voucher` where BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and ChoosenDate between '" + Fromdate + "' and '" + dateToConsolidated.Date.AddDays(-1).ToString("yyyy/MM/dd") + "' and RootID=3 group by RootID");
            if (dtStatsPreviousdayBanks.Rows.Count == 1)
            {

                decimal decCredit = 0.00M;
                decimal decDebit = 0.00M;
                decimal decDebitBalance = 0.00M;
                decimal decCreditBalance = 0.00M;
                decimal.TryParse(balayer.ToobjectstrEvenNull(dtStatsPreviousdayBanks.Rows[0][1].ToString()), out decCredit);
                decimal.TryParse(balayer.ToobjectstrEvenNull(dtStatsPreviousdayBanks.Rows[0][2].ToString()), out decDebit);
                if ((decCredit - decDebit) > 0.00M)
                {
                    decCreditBalance = decCredit - decDebit;
                }
                else if ((decDebit - decCredit) > 0.00M)
                {
                    decDebitBalance = decDebit - decCredit;
                }
                lblCrBanksAmountprDay.Text = decCredit.ToString();
                lblCrBanksBalanceprDay.Text = decCreditBalance.ToString();
                lblDrBanksAmountprDay.Text = decDebit.ToString();
                lblDrBanksBalanceprDay.Text = decDebitBalance.ToString();
            }


            DataTable dtStatsCurrentDayBanks = balayer.GetDataTable(" SELECT RootID,sum((case when Voucher_Type='C' then Amount else 0.00 end ))as `Credit Amount`,sum((case when Voucher_Type='D' then Amount else 0.00 end ))as `Debit Amount` FROM `svcf`.`voucher` where BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and ChoosenDate = '" + Todate + "' and RootID=3 group by RootID");
            if (dtStatsCurrentDayBanks.Rows.Count == 1)
            {

                decimal decCredit = 0.00M;
                decimal decDebit = 0.00M;
                decimal decDebitBalance = 0.00M;
                decimal decCreditBalance = 0.00M;
                decimal.TryParse(balayer.ToobjectstrEvenNull(dtStatsCurrentDayBanks.Rows[0][1].ToString()), out decCredit);
                decimal.TryParse(balayer.ToobjectstrEvenNull(dtStatsCurrentDayBanks.Rows[0][2].ToString()), out decDebit);
                if ((decCredit - decDebit) > 0.00M)
                {
                    decCreditBalance = decCredit - decDebit;
                }
                else if ((decDebit - decCredit) > 0.00M)
                {
                    decDebitBalance = decDebit - decCredit;
                }
                lblCrBanksAmount.Text = decCredit.ToString();
                lblCrBanksBalance.Text = decCreditBalance.ToString();
                lblDrBanksAmount.Text = decDebit.ToString();
                lblDrBanksBalance.Text = decDebitBalance.ToString();
            }

            //OtherItems
            DataTable dtStatsOtherItems = balayer.GetDataTable(" SELECT RootID,sum((case when Voucher_Type='C' then Amount else 0.00 end ))as `Credit Amount`,sum((case when Voucher_Type='D' then Amount else 0.00 end ))as `Debit Amount` FROM `svcf`.`voucher` where BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and ChoosenDate between '" + Fromdate + "' and '" + Todate + "' and RootID=4 group by RootID");
            if (dtStatsOtherItems.Rows.Count == 1)
            {

                decimal decCredit = 0.00M;
                decimal decDebit = 0.00M;
                decimal decDebitBalance = 0.00M;
                decimal decCreditBalance = 0.00M;
                decimal.TryParse(balayer.ToobjectstrEvenNull(dtStatsOtherItems.Rows[0][1].ToString()), out decCredit);
                decimal.TryParse(balayer.ToobjectstrEvenNull(dtStatsOtherItems.Rows[0][2].ToString()), out decDebit);
                if ((decCredit - decDebit) > 0.00M)
                {
                    decCreditBalance = decCredit - decDebit;
                }
                else if ((decDebit - decCredit) > 0.00M)
                {
                    decDebitBalance = decDebit - decCredit;
                }
                lblCrOtherItemsAmountFinal.Text = decCredit.ToString();
                lblCrOtherItemsBalanceFinal.Text = decCreditBalance.ToString();
                lblDrOtherItemsAmountFinal.Text = decDebit.ToString();
                lblDrOtherItemsBalanceFinal.Text = decDebitBalance.ToString();
            }

            DataTable dtStatsPreviousdayOtherItems = balayer.GetDataTable(" SELECT RootID,sum((case when Voucher_Type='C' then Amount else 0.00 end ))as `Credit Amount`,sum((case when Voucher_Type='D' then Amount else 0.00 end ))as `Debit Amount` FROM `svcf`.`voucher` where BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and ChoosenDate between '" + Fromdate + "' and '" + dateToConsolidated.Date.AddDays(-1).ToString("yyyy/MM/dd") + "' and RootID=4 group by RootID");
            if (dtStatsPreviousdayOtherItems.Rows.Count == 1)
            {

                decimal decCredit = 0.00M;
                decimal decDebit = 0.00M;
                decimal decDebitBalance = 0.00M;
                decimal decCreditBalance = 0.00M;
                decimal.TryParse(balayer.ToobjectstrEvenNull(dtStatsPreviousdayOtherItems.Rows[0][1].ToString()), out decCredit);
                decimal.TryParse(balayer.ToobjectstrEvenNull(dtStatsPreviousdayOtherItems.Rows[0][2].ToString()), out decDebit);
                if ((decCredit - decDebit) > 0.00M)
                {
                    decCreditBalance = decCredit - decDebit;
                }
                else if ((decDebit - decCredit) > 0.00M)
                {
                    decDebitBalance = decDebit - decCredit;
                }
                lblCrOtherItemsAmountprDay.Text = decCredit.ToString();
                lblCrOtherItemsBalanceprDay.Text = decCreditBalance.ToString();
                lblDrOtherItemsAmountprDay.Text = decDebit.ToString();
                lblDrOtherItemsBalanceprDay.Text = decDebitBalance.ToString();
            }


            DataTable dtStatsCurrentDayOtherItems = balayer.GetDataTable(" SELECT RootID,sum((case when Voucher_Type='C' then Amount else 0.00 end ))as `Credit Amount`,sum((case when Voucher_Type='D' then Amount else 0.00 end ))as `Debit Amount` FROM `svcf`.`voucher` where BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and ChoosenDate = '" + Todate + "' and RootID=4 group by RootID");
            if (dtStatsCurrentDayOtherItems.Rows.Count == 1)
            {

                decimal decCredit = 0.00M;
                decimal decDebit = 0.00M;
                decimal decDebitBalance = 0.00M;
                decimal decCreditBalance = 0.00M;
                decimal.TryParse(balayer.ToobjectstrEvenNull(dtStatsCurrentDayOtherItems.Rows[0][1].ToString()), out decCredit);
                decimal.TryParse(balayer.ToobjectstrEvenNull(dtStatsCurrentDayOtherItems.Rows[0][2].ToString()), out decDebit);
                if ((decCredit - decDebit) > 0.00M)
                {
                    decCreditBalance = decCredit - decDebit;
                }
                else if ((decDebit - decCredit) > 0.00M)
                {
                    decDebitBalance = decDebit - decCredit;
                }
                lblCrOtherItemsAmount.Text = decCredit.ToString();
                lblCrOtherItemsBalance.Text = decCreditBalance.ToString();
                lblDrOtherItemsAmount.Text = decDebit.ToString();
                lblDrOtherItemsBalance.Text = decDebitBalance.ToString();
            }

            //Chits
            DataTable dtStatsChits = balayer.GetDataTable(" SELECT RootID,sum((case when Voucher_Type='C' then Amount else 0.00 end ))as `Credit Amount`,sum((case when Voucher_Type='D' then Amount else 0.00 end ))as `Debit Amount` FROM `svcf`.`voucher` where BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and ChoosenDate between '" + Fromdate + "' and '" + Todate + "' and RootID=5 group by RootID");
            if (dtStatsChits.Rows.Count == 1)
            {

                decimal decCredit = 0.00M;
                decimal decDebit = 0.00M;
                decimal decDebitBalance = 0.00M;
                decimal decCreditBalance = 0.00M;
                decimal.TryParse(balayer.ToobjectstrEvenNull(dtStatsChits.Rows[0][1].ToString()), out decCredit);
                decimal.TryParse(balayer.ToobjectstrEvenNull(dtStatsChits.Rows[0][2].ToString()), out decDebit);
                if ((decCredit - decDebit) > 0.00M)
                {
                    decCreditBalance = decCredit - decDebit;
                }
                else if ((decDebit - decCredit) > 0.00M)
                {
                    decDebitBalance = decDebit - decCredit;
                }
                lblCrChitsAmountFinal.Text = decCredit.ToString();
                lblCrChitsBalanceFinal.Text = decCreditBalance.ToString();
                lblDrChitsAmountFinal.Text = decDebit.ToString();
                lblDrChitsBalanceFinal.Text = decDebitBalance.ToString();
            }

            DataTable dtStatsPreviousdayChits = balayer.GetDataTable(" SELECT RootID,sum((case when Voucher_Type='C' then Amount else 0.00 end ))as `Credit Amount`,sum((case when Voucher_Type='D' then Amount else 0.00 end ))as `Debit Amount` FROM `svcf`.`voucher` where BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and ChoosenDate between '" + Fromdate + "' and '" + dateToConsolidated.Date.AddDays(-1).ToString("yyyy/MM/dd") + "' and RootID=5 group by RootID");
            if (dtStatsPreviousdayChits.Rows.Count == 1)
            {

                decimal decCredit = 0.00M;
                decimal decDebit = 0.00M;
                decimal decDebitBalance = 0.00M;
                decimal decCreditBalance = 0.00M;
                decimal.TryParse(balayer.ToobjectstrEvenNull(dtStatsPreviousdayChits.Rows[0][1].ToString()), out decCredit);
                decimal.TryParse(balayer.ToobjectstrEvenNull(dtStatsPreviousdayChits.Rows[0][2].ToString()), out decDebit);
                if ((decCredit - decDebit) > 0.00M)
                {
                    decCreditBalance = decCredit - decDebit;
                }
                else if ((decDebit - decCredit) > 0.00M)
                {
                    decDebitBalance = decDebit - decCredit;
                }
                lblCrChitsAmountprDay.Text = decCredit.ToString();
                lblCrChitsBalanceprDay.Text = decCreditBalance.ToString();
                lblDrChitsAmountprDay.Text = decDebit.ToString();
                lblDrChitsBalanceprDay.Text = decDebitBalance.ToString();
            }


            DataTable dtStatsCurrentDayChits = balayer.GetDataTable(" SELECT RootID,sum((case when Voucher_Type='C' then Amount else 0.00 end ))as `Credit Amount`,sum((case when Voucher_Type='D' then Amount else 0.00 end ))as `Debit Amount` FROM `svcf`.`voucher` where BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and ChoosenDate = '" + Todate + "' and RootID=5 group by RootID");
            if (dtStatsCurrentDayChits.Rows.Count == 1)
            {

                decimal decCredit = 0.00M;
                decimal decDebit = 0.00M;
                decimal decDebitBalance = 0.00M;
                decimal decCreditBalance = 0.00M;
                decimal.TryParse(balayer.ToobjectstrEvenNull(dtStatsCurrentDayChits.Rows[0][1].ToString()), out decCredit);
                decimal.TryParse(balayer.ToobjectstrEvenNull(dtStatsCurrentDayChits.Rows[0][2].ToString()), out decDebit);
                if ((decCredit - decDebit) > 0.00M)
                {
                    decCreditBalance = decCredit - decDebit;
                }
                else if ((decDebit - decCredit) > 0.00M)
                {
                    decDebitBalance = decDebit - decCredit;
                }
                lblCrChitsAmount.Text = decCredit.ToString();
                lblCrChitsBalance.Text = decCreditBalance.ToString();
                lblDrChitsAmount.Text = decDebit.ToString();
                lblDrChitsBalance.Text = decDebitBalance.ToString();
            }


            //ForemanChits
            DataTable dtStatsForemanChits = balayer.GetDataTable(" SELECT RootID,sum((case when Voucher_Type='C' then Amount else 0.00 end ))as `Credit Amount`,sum((case when Voucher_Type='D' then Amount else 0.00 end ))as `Debit Amount` FROM `svcf`.`voucher` where BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and ChoosenDate between '" + Fromdate + "' and '" + Todate + "' and RootID=6 group by RootID");
            if (dtStatsForemanChits.Rows.Count == 1)
            {

                decimal decCredit = 0.00M;
                decimal decDebit = 0.00M;
                decimal decDebitBalance = 0.00M;
                decimal decCreditBalance = 0.00M;
                decimal.TryParse(balayer.ToobjectstrEvenNull(dtStatsForemanChits.Rows[0][1].ToString()), out decCredit);
                decimal.TryParse(balayer.ToobjectstrEvenNull(dtStatsForemanChits.Rows[0][2].ToString()), out decDebit);
                if ((decCredit - decDebit) > 0.00M)
                {
                    decCreditBalance = decCredit - decDebit;
                }
                else if ((decDebit - decCredit) > 0.00M)
                {
                    decDebitBalance = decDebit - decCredit;
                }
                lblCrForemanChitsAmountFinal.Text = decCredit.ToString();
                lblCrForemanChitsBalanceFinal.Text = decCreditBalance.ToString();
                lblDrForemanChitsAmountFinal.Text = decDebit.ToString();
                lblDrForemanChitsBalanceFinal.Text = decDebitBalance.ToString();
            }

            DataTable dtStatsPreviousdayForemanChits = balayer.GetDataTable(" SELECT RootID,sum((case when Voucher_Type='C' then Amount else 0.00 end ))as `Credit Amount`,sum((case when Voucher_Type='D' then Amount else 0.00 end ))as `Debit Amount` FROM `svcf`.`voucher` where BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and ChoosenDate between '" + Fromdate + "' and '" + dateToConsolidated.Date.AddDays(-1).ToString("yyyy/MM/dd") + "' and RootID=6 group by RootID");
            if (dtStatsPreviousdayForemanChits.Rows.Count == 1)
            {

                decimal decCredit = 0.00M;
                decimal decDebit = 0.00M;
                decimal decDebitBalance = 0.00M;
                decimal decCreditBalance = 0.00M;
                decimal.TryParse(balayer.ToobjectstrEvenNull(dtStatsPreviousdayForemanChits.Rows[0][1].ToString()), out decCredit);
                decimal.TryParse(balayer.ToobjectstrEvenNull(dtStatsPreviousdayForemanChits.Rows[0][2].ToString()), out decDebit);
                if ((decCredit - decDebit) > 0.00M)
                {
                    decCreditBalance = decCredit - decDebit;
                }
                else if ((decDebit - decCredit) > 0.00M)
                {
                    decDebitBalance = decDebit - decCredit;
                }
                lblCrForemanChitsAmountprDay.Text = decCredit.ToString();
                lblCrForemanChitsBalanceprDay.Text = decCreditBalance.ToString();
                lblDrForemanChitsAmountprDay.Text = decDebit.ToString();
                lblDrForemanChitsBalanceprDay.Text = decDebitBalance.ToString();
            }


            DataTable dtStatsCurrentDayForemanChits = balayer.GetDataTable(" SELECT RootID,sum((case when Voucher_Type='C' then Amount else 0.00 end ))as `Credit Amount`,sum((case when Voucher_Type='D' then Amount else 0.00 end ))as `Debit Amount` FROM `svcf`.`voucher` where BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and ChoosenDate = '" + Todate + "' and RootID=6 group by RootID");
            if (dtStatsCurrentDayForemanChits.Rows.Count == 1)
            {

                decimal decCredit = 0.00M;
                decimal decDebit = 0.00M;
                decimal decDebitBalance = 0.00M;
                decimal decCreditBalance = 0.00M;
                decimal.TryParse(balayer.ToobjectstrEvenNull(dtStatsCurrentDayForemanChits.Rows[0][1].ToString()), out decCredit);
                decimal.TryParse(balayer.ToobjectstrEvenNull(dtStatsCurrentDayForemanChits.Rows[0][2].ToString()), out decDebit);
                if ((decCredit - decDebit) > 0.00M)
                {
                    decCreditBalance = decCredit - decDebit;
                }
                else if ((decDebit - decCredit) > 0.00M)
                {
                    decDebitBalance = decDebit - decCredit;
                }
                lblCrForemanChitsAmount.Text = decCredit.ToString();
                lblCrForemanChitsBalance.Text = decCreditBalance.ToString();
                lblDrForemanChitsAmount.Text = decDebit.ToString();
                lblDrForemanChitsBalance.Text = decDebitBalance.ToString();
            }

            //Loans
            DataTable dtStatsLoans = balayer.GetDataTable(" SELECT RootID,sum((case when Voucher_Type='C' then Amount else 0.00 end ))as `Credit Amount`,sum((case when Voucher_Type='D' then Amount else 0.00 end ))as `Debit Amount` FROM `svcf`.`voucher` where BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and ChoosenDate between '" + Fromdate + "' and '" + Todate + "' and RootID=8 group by RootID");
            if (dtStatsLoans.Rows.Count == 1)
            {

                decimal decCredit = 0.00M;
                decimal decDebit = 0.00M;
                decimal decDebitBalance = 0.00M;
                decimal decCreditBalance = 0.00M;
                decimal.TryParse(balayer.ToobjectstrEvenNull(dtStatsLoans.Rows[0][1].ToString()), out decCredit);
                decimal.TryParse(balayer.ToobjectstrEvenNull(dtStatsLoans.Rows[0][2].ToString()), out decDebit);
                if ((decCredit - decDebit) > 0.00M)
                {
                    decCreditBalance = decCredit - decDebit;
                }
                else if ((decDebit - decCredit) > 0.00M)
                {
                    decDebitBalance = decDebit - decCredit;
                }
                lblCrLoansAmountFinal.Text = decCredit.ToString();
                lblCrLoansBalanceFinal.Text = decCreditBalance.ToString();
                lblDrLoansAmountFinal.Text = decDebit.ToString();
                lblDrLoansBalanceFinal.Text = decDebitBalance.ToString();
            }

            DataTable dtStatsPreviousdayLoans = balayer.GetDataTable(" SELECT RootID,sum((case when Voucher_Type='C' then Amount else 0.00 end ))as `Credit Amount`,sum((case when Voucher_Type='D' then Amount else 0.00 end ))as `Debit Amount` FROM `svcf`.`voucher` where BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and ChoosenDate between '" + Fromdate + "' and '" + dateToConsolidated.Date.AddDays(-1).ToString("yyyy/MM/dd") + "' and RootID=8 group by RootID");
            if (dtStatsPreviousdayLoans.Rows.Count == 1)
            {

                decimal decCredit = 0.00M;
                decimal decDebit = 0.00M;
                decimal decDebitBalance = 0.00M;
                decimal decCreditBalance = 0.00M;
                decimal.TryParse(balayer.ToobjectstrEvenNull(dtStatsPreviousdayLoans.Rows[0][1].ToString()), out decCredit);
                decimal.TryParse(balayer.ToobjectstrEvenNull(dtStatsPreviousdayLoans.Rows[0][2].ToString()), out decDebit);
                if ((decCredit - decDebit) > 0.00M)
                {
                    decCreditBalance = decCredit - decDebit;
                }
                else if ((decDebit - decCredit) > 0.00M)
                {
                    decDebitBalance = decDebit - decCredit;
                }
                lblCrLoansAmountprDay.Text = decCredit.ToString();
                lblCrLoansBalanceprDay.Text = decCreditBalance.ToString();
                lblDrLoansAmountprDay.Text = decDebit.ToString();
                lblDrLoansBalanceprDay.Text = decDebitBalance.ToString();
            }


            DataTable dtStatsCurrentDayLoans = balayer.GetDataTable(" SELECT RootID,sum((case when Voucher_Type='C' then Amount else 0.00 end ))as `Credit Amount`,sum((case when Voucher_Type='D' then Amount else 0.00 end ))as `Debit Amount` FROM `svcf`.`voucher` where BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and ChoosenDate = '" + Todate + "' and RootID=8 group by RootID");
            if (dtStatsCurrentDayLoans.Rows.Count == 1)
            {

                decimal decCredit = 0.00M;
                decimal decDebit = 0.00M;
                decimal decDebitBalance = 0.00M;
                decimal decCreditBalance = 0.00M;
                decimal.TryParse(balayer.ToobjectstrEvenNull(dtStatsCurrentDayLoans.Rows[0][1].ToString()), out decCredit);
                decimal.TryParse(balayer.ToobjectstrEvenNull(dtStatsCurrentDayLoans.Rows[0][2].ToString()), out decDebit);
                if ((decCredit - decDebit) > 0.00M)
                {
                    decCreditBalance = decCredit - decDebit;
                }
                else if ((decDebit - decCredit) > 0.00M)
                {
                    decDebitBalance = decDebit - decCredit;
                }
                lblCrLoansAmount.Text = decCredit.ToString();
                lblCrLoansBalance.Text = decCreditBalance.ToString();
                lblDrLoansAmount.Text = decDebit.ToString();
                lblDrLoansBalance.Text = decDebitBalance.ToString();
            }


            //DecreeDebtors
            DataTable dtStatsDecreeDebtors = balayer.GetDataTable(" SELECT RootID,sum((case when Voucher_Type='C' then Amount else 0.00 end ))as `Credit Amount`,sum((case when Voucher_Type='D' then Amount else 0.00 end ))as `Debit Amount` FROM `svcf`.`voucher` where BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and ChoosenDate between '" + Fromdate + "' and '" + Todate + "' and RootID=7 group by RootID");
            if (dtStatsDecreeDebtors.Rows.Count == 1)
            {

                decimal decCredit = 0.00M;
                decimal decDebit = 0.00M;
                decimal decDebitBalance = 0.00M;
                decimal decCreditBalance = 0.00M;
                decimal.TryParse(balayer.ToobjectstrEvenNull(dtStatsDecreeDebtors.Rows[0][1].ToString()), out decCredit);
                decimal.TryParse(balayer.ToobjectstrEvenNull(dtStatsDecreeDebtors.Rows[0][2].ToString()), out decDebit);
                if ((decCredit - decDebit) > 0.00M)
                {
                    decCreditBalance = decCredit - decDebit;
                }
                else if ((decDebit - decCredit) > 0.00M)
                {
                    decDebitBalance = decDebit - decCredit;
                }
                lblCrDecreeDebtorsAmountFinal.Text = decCredit.ToString();
                lblCrDecreeDebtorsBalanceFinal.Text = decCreditBalance.ToString();
                lblDrDecreeDebtorsAmountFinal.Text = decDebit.ToString();
                lblDrDecreeDebtorsBalanceFinal.Text = decDebitBalance.ToString();
            }

            DataTable dtStatsPreviousdayDecreeDebtors = balayer.GetDataTable(" SELECT RootID,sum((case when Voucher_Type='C' then Amount else 0.00 end ))as `Credit Amount`,sum((case when Voucher_Type='D' then Amount else 0.00 end ))as `Debit Amount` FROM `svcf`.`voucher` where BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and ChoosenDate between '" + Fromdate + "' and '" + dateToConsolidated.Date.AddDays(-1).ToString("yyyy/MM/dd") + "' and RootID=7 group by RootID");
            if (dtStatsPreviousdayDecreeDebtors.Rows.Count == 1)
            {

                decimal decCredit = 0.00M;
                decimal decDebit = 0.00M;
                decimal decDebitBalance = 0.00M;
                decimal decCreditBalance = 0.00M;
                decimal.TryParse(balayer.ToobjectstrEvenNull(dtStatsPreviousdayDecreeDebtors.Rows[0][1].ToString()), out decCredit);
                decimal.TryParse(balayer.ToobjectstrEvenNull(dtStatsPreviousdayDecreeDebtors.Rows[0][2].ToString()), out decDebit);
                if ((decCredit - decDebit) > 0.00M)
                {
                    decCreditBalance = decCredit - decDebit;
                }
                else if ((decDebit - decCredit) > 0.00M)
                {
                    decDebitBalance = decDebit - decCredit;
                }
                lblCrDecreeDebtorsAmountprDay.Text = decCredit.ToString();
                lblCrDecreeDebtorsBalanceprDay.Text = decCreditBalance.ToString();
                lblDrDecreeDebtorsAmountprDay.Text = decDebit.ToString();
                lblDrDecreeDebtorsBalanceprDay.Text = decDebitBalance.ToString();
            }


            DataTable dtStatsCurrentDayDecreeDebtors = balayer.GetDataTable(" SELECT RootID,sum((case when Voucher_Type='C' then Amount else 0.00 end ))as `Credit Amount`,sum((case when Voucher_Type='D' then Amount else 0.00 end ))as `Debit Amount` FROM `svcf`.`voucher` where BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and ChoosenDate = '" + Todate + "' and RootID=7 group by RootID");
            if (dtStatsCurrentDayDecreeDebtors.Rows.Count == 1)
            {

                decimal decCredit = 0.00M;
                decimal decDebit = 0.00M;
                decimal decDebitBalance = 0.00M;
                decimal decCreditBalance = 0.00M;
                decimal.TryParse(balayer.ToobjectstrEvenNull(dtStatsCurrentDayDecreeDebtors.Rows[0][1].ToString()), out decCredit);
                decimal.TryParse(balayer.ToobjectstrEvenNull(dtStatsCurrentDayDecreeDebtors.Rows[0][2].ToString()), out decDebit);
                if ((decCredit - decDebit) > 0.00M)
                {
                    decCreditBalance = decCredit - decDebit;
                }
                else if ((decDebit - decCredit) > 0.00M)
                {
                    decDebitBalance = decDebit - decCredit;
                }
                lblCrDecreeDebtorsAmount.Text = decCredit.ToString();
                lblCrDecreeDebtorsBalance.Text = decCreditBalance.ToString();
                lblDrDecreeDebtorsAmount.Text = decDebit.ToString();
                lblDrDecreeDebtorsBalance.Text = decDebitBalance.ToString();
            }

            //SundriesAndAdvances
            DataTable dtStatsSundriesAndAdvances = balayer.GetDataTable(" SELECT RootID,sum((case when Voucher_Type='C' then Amount else 0.00 end ))as `Credit Amount`,sum((case when Voucher_Type='D' then Amount else 0.00 end ))as `Debit Amount` FROM `svcf`.`voucher` where BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and ChoosenDate between '" + Fromdate + "' and '" + Todate + "' and RootID=9 group by RootID");
            if (dtStatsSundriesAndAdvances.Rows.Count == 1)
            {

                decimal decCredit = 0.00M;
                decimal decDebit = 0.00M;
                decimal decDebitBalance = 0.00M;
                decimal decCreditBalance = 0.00M;
                decimal.TryParse(balayer.ToobjectstrEvenNull(dtStatsSundriesAndAdvances.Rows[0][1].ToString()), out decCredit);
                decimal.TryParse(balayer.ToobjectstrEvenNull(dtStatsSundriesAndAdvances.Rows[0][2].ToString()), out decDebit);
                if ((decCredit - decDebit) > 0.00M)
                {
                    decCreditBalance = decCredit - decDebit;
                }
                else if ((decDebit - decCredit) > 0.00M)
                {
                    decDebitBalance = decDebit - decCredit;
                }
                lblCrSundriesAndAdvancesAmountFinal.Text = decCredit.ToString();
                lblCrSundriesAndAdvancesBalanceFinal.Text = decCreditBalance.ToString();
                lblDrSundriesAndAdvancesAmountFinal.Text = decDebit.ToString();
                lblDrSundriesAndAdvancesBalanceFinal.Text = decDebitBalance.ToString();
            }

            DataTable dtStatsPreviousdaySundriesAndAdvances = balayer.GetDataTable(" SELECT RootID,sum((case when Voucher_Type='C' then Amount else 0.00 end ))as `Credit Amount`,sum((case when Voucher_Type='D' then Amount else 0.00 end ))as `Debit Amount` FROM `svcf`.`voucher` where BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and ChoosenDate between '" + Fromdate + "' and '" + dateToConsolidated.Date.AddDays(-1).ToString("yyyy/MM/dd") + "' and RootID=9 group by RootID");
            if (dtStatsPreviousdaySundriesAndAdvances.Rows.Count == 1)
            {

                decimal decCredit = 0.00M;
                decimal decDebit = 0.00M;
                decimal decDebitBalance = 0.00M;
                decimal decCreditBalance = 0.00M;
                decimal.TryParse(balayer.ToobjectstrEvenNull(dtStatsPreviousdaySundriesAndAdvances.Rows[0][1].ToString()), out decCredit);
                decimal.TryParse(balayer.ToobjectstrEvenNull(dtStatsPreviousdaySundriesAndAdvances.Rows[0][2].ToString()), out decDebit);
                if ((decCredit - decDebit) > 0.00M)
                {
                    decCreditBalance = decCredit - decDebit;
                }
                else if ((decDebit - decCredit) > 0.00M)
                {
                    decDebitBalance = decDebit - decCredit;
                }
                lblCrSundriesAndAdvancesAmountprDay.Text = decCredit.ToString();
                lblCrSundriesAndAdvancesBalanceprDay.Text = decCreditBalance.ToString();
                lblDrSundriesAndAdvancesAmountprDay.Text = decDebit.ToString();
                lblDrSundriesAndAdvancesBalanceprDay.Text = decDebitBalance.ToString();
            }


            DataTable dtStatsCurrentDaySundriesAndAdvances = balayer.GetDataTable(" SELECT RootID,sum((case when Voucher_Type='C' then Amount else 0.00 end ))as `Credit Amount`,sum((case when Voucher_Type='D' then Amount else 0.00 end ))as `Debit Amount` FROM `svcf`.`voucher` where BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and ChoosenDate = '" + Todate + "' and RootID=9 group by RootID");
            if (dtStatsCurrentDaySundriesAndAdvances.Rows.Count == 1)
            {

                decimal decCredit = 0.00M;
                decimal decDebit = 0.00M;
                decimal decDebitBalance = 0.00M;
                decimal decCreditBalance = 0.00M;
                decimal.TryParse(balayer.ToobjectstrEvenNull(dtStatsCurrentDaySundriesAndAdvances.Rows[0][1].ToString()), out decCredit);
                decimal.TryParse(balayer.ToobjectstrEvenNull(dtStatsCurrentDaySundriesAndAdvances.Rows[0][2].ToString()), out decDebit);
                if ((decCredit - decDebit) > 0.00M)
                {
                    decCreditBalance = decCredit - decDebit;
                }
                else if ((decDebit - decCredit) > 0.00M)
                {
                    decDebitBalance = decDebit - decCredit;
                }
                lblCrSundriesAndAdvancesAmount.Text = decCredit.ToString();
                lblCrSundriesAndAdvancesBalance.Text = decCreditBalance.ToString();
                lblDrSundriesAndAdvancesAmount.Text = decDebit.ToString();
                lblDrSundriesAndAdvancesBalance.Text = decDebitBalance.ToString();
            }
            //Stamps
            DataTable dtStatsStamps = balayer.GetDataTable(" SELECT RootID,sum((case when Voucher_Type='C' then Amount else 0.00 end ))as `Credit Amount`,sum((case when Voucher_Type='D' then Amount else 0.00 end ))as `Debit Amount` FROM `svcf`.`voucher` where BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and ChoosenDate between '" + Fromdate + "' and '" + Todate + "' and RootID=10 group by RootID");
            if (dtStatsStamps.Rows.Count == 1)
            {

                decimal decCredit = 0.00M;
                decimal decDebit = 0.00M;
                decimal decDebitBalance = 0.00M;
                decimal decCreditBalance = 0.00M;
                decimal.TryParse(balayer.ToobjectstrEvenNull(dtStatsStamps.Rows[0][1].ToString()), out decCredit);
                decimal.TryParse(balayer.ToobjectstrEvenNull(dtStatsStamps.Rows[0][2].ToString()), out decDebit);
                if ((decCredit - decDebit) > 0.00M)
                {
                    decCreditBalance = decCredit - decDebit;
                }
                else if ((decDebit - decCredit) > 0.00M)
                {
                    decDebitBalance = decDebit - decCredit;
                }
                lblCrStampsAmountFinal.Text = decCredit.ToString();
                lblCrStampsBalanceFinal.Text = decCreditBalance.ToString();
                lblDrStampsAmountFinal.Text = decDebit.ToString();
                lblDrStampsBalanceFinal.Text = decDebitBalance.ToString();
            }

            DataTable dtStatsPreviousdayStamps = balayer.GetDataTable(" SELECT RootID,sum((case when Voucher_Type='C' then Amount else 0.00 end ))as `Credit Amount`,sum((case when Voucher_Type='D' then Amount else 0.00 end ))as `Debit Amount` FROM `svcf`.`voucher` where BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and ChoosenDate between '" + Fromdate + "' and '" + dateToConsolidated.Date.AddDays(-1).ToString("yyyy/MM/dd") + "' and RootID=10 group by RootID");
            if (dtStatsPreviousdayStamps.Rows.Count == 1)
            {

                decimal decCredit = 0.00M;
                decimal decDebit = 0.00M;
                decimal decDebitBalance = 0.00M;
                decimal decCreditBalance = 0.00M;
                decimal.TryParse(balayer.ToobjectstrEvenNull(dtStatsPreviousdayStamps.Rows[0][1].ToString()), out decCredit);
                decimal.TryParse(balayer.ToobjectstrEvenNull(dtStatsPreviousdayStamps.Rows[0][2].ToString()), out decDebit);
                if ((decCredit - decDebit) > 0.00M)
                {
                    decCreditBalance = decCredit - decDebit;
                }
                else if ((decDebit - decCredit) > 0.00M)
                {
                    decDebitBalance = decDebit - decCredit;
                }
                lblCrStampsAmountprDay.Text = decCredit.ToString();
                lblCrStampsBalanceprDay.Text = decCreditBalance.ToString();
                lblDrStampsAmountprDay.Text = decDebit.ToString();
                lblDrStampsBalanceprDay.Text = decDebitBalance.ToString();
            }


            DataTable dtStatsCurrentDayStamps = balayer.GetDataTable(" SELECT RootID,sum((case when Voucher_Type='C' then Amount else 0.00 end ))as `Credit Amount`,sum((case when Voucher_Type='D' then Amount else 0.00 end ))as `Debit Amount` FROM `svcf`.`voucher` where BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and ChoosenDate = '" + Todate + "' and RootID=10 group by RootID");
            if (dtStatsCurrentDayStamps.Rows.Count == 1)
            {

                decimal decCredit = 0.00M;
                decimal decDebit = 0.00M;
                decimal decDebitBalance = 0.00M;
                decimal decCreditBalance = 0.00M;
                decimal.TryParse(balayer.ToobjectstrEvenNull(dtStatsCurrentDayStamps.Rows[0][1].ToString()), out decCredit);
                decimal.TryParse(balayer.ToobjectstrEvenNull(dtStatsCurrentDayStamps.Rows[0][2].ToString()), out decDebit);
                if ((decCredit - decDebit) > 0.00M)
                {
                    decCreditBalance = decCredit - decDebit;
                }
                else if ((decDebit - decCredit) > 0.00M)
                {
                    decDebitBalance = decDebit - decCredit;
                }
                lblCrStampsAmount.Text = decCredit.ToString();
                lblCrStampsBalance.Text = decCreditBalance.ToString();
                lblDrStampsAmount.Text = decDebit.ToString();
                lblDrStampsBalance.Text = decDebitBalance.ToString();
            }

            //ProfitAndLoss
            DataTable dtStatsProfitAndLoss = balayer.GetDataTable(" SELECT RootID,sum((case when Voucher_Type='C' then Amount else 0.00 end ))as `Credit Amount`,sum((case when Voucher_Type='D' then Amount else 0.00 end ))as `Debit Amount` FROM `svcf`.`voucher` where BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and ChoosenDate between '" + Fromdate + "' and '" + Todate + "' and RootID=11 group by RootID");
            if (dtStatsProfitAndLoss.Rows.Count == 1)
            {

                decimal decCredit = 0.00M;
                decimal decDebit = 0.00M;
                decimal decDebitBalance = 0.00M;
                decimal decCreditBalance = 0.00M;
                decimal.TryParse(balayer.ToobjectstrEvenNull(dtStatsProfitAndLoss.Rows[0][1].ToString()), out decCredit);
                decimal.TryParse(balayer.ToobjectstrEvenNull(dtStatsProfitAndLoss.Rows[0][2].ToString()), out decDebit);
                if ((decCredit - decDebit) > 0.00M)
                {
                    decCreditBalance = decCredit - decDebit;
                }
                else if ((decDebit - decCredit) > 0.00M)
                {
                    decDebitBalance = decDebit - decCredit;
                }
                lblCrProfitAndLossAmountFinal.Text = decCredit.ToString();
                lblCrProfitAndLossBalanceFinal.Text = decCreditBalance.ToString();
                lblDrProfitAndLossAmountFinal.Text = decDebit.ToString();
                lblDrProfitAndLossBalanceFinal.Text = decDebitBalance.ToString();
            }

            DataTable dtStatsPreviousdayProfitAndLoss = balayer.GetDataTable(" SELECT RootID,sum((case when Voucher_Type='C' then Amount else 0.00 end ))as `Credit Amount`,sum((case when Voucher_Type='D' then Amount else 0.00 end ))as `Debit Amount` FROM `svcf`.`voucher` where BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and ChoosenDate between '" + Fromdate + "' and '" + dateToConsolidated.Date.AddDays(-1).ToString("yyyy/MM/dd") + "' and RootID=11 group by RootID");
            if (dtStatsPreviousdayProfitAndLoss.Rows.Count == 1)
            {

                decimal decCredit = 0.00M;
                decimal decDebit = 0.00M;
                decimal decDebitBalance = 0.00M;
                decimal decCreditBalance = 0.00M;
                decimal.TryParse(balayer.ToobjectstrEvenNull(dtStatsPreviousdayProfitAndLoss.Rows[0][1].ToString()), out decCredit);
                decimal.TryParse(balayer.ToobjectstrEvenNull(dtStatsPreviousdayProfitAndLoss.Rows[0][2].ToString()), out decDebit);
                if ((decCredit - decDebit) > 0.00M)
                {
                    decCreditBalance = decCredit - decDebit;
                }
                else if ((decDebit - decCredit) > 0.00M)
                {
                    decDebitBalance = decDebit - decCredit;
                }
                lblCrProfitAndLossAmountprDay.Text = decCredit.ToString();
                lblCrProfitAndLossBalanceprDay.Text = decCreditBalance.ToString();
                lblDrProfitAndLossAmountprDay.Text = decDebit.ToString();
                lblDrProfitAndLossBalanceprDay.Text = decDebitBalance.ToString();
            }


            DataTable dtStatsCurrentDayProfitAndLoss = balayer.GetDataTable(" SELECT RootID,sum((case when Voucher_Type='C' then Amount else 0.00 end ))as `Credit Amount`,sum((case when Voucher_Type='D' then Amount else 0.00 end ))as `Debit Amount` FROM `svcf`.`voucher` where BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and ChoosenDate = '" + Todate + "' and RootID=11 group by RootID");
            if (dtStatsCurrentDayProfitAndLoss.Rows.Count == 1)
            {

                decimal decCredit = 0.00M;
                decimal decDebit = 0.00M;
                decimal decDebitBalance = 0.00M;
                decimal decCreditBalance = 0.00M;
                decimal.TryParse(balayer.ToobjectstrEvenNull(dtStatsCurrentDayProfitAndLoss.Rows[0][1].ToString()), out decCredit);
                decimal.TryParse(balayer.ToobjectstrEvenNull(dtStatsCurrentDayProfitAndLoss.Rows[0][2].ToString()), out decDebit);
                if ((decCredit - decDebit) > 0.00M)
                {
                    decCreditBalance = decCredit - decDebit;
                }
                else if ((decDebit - decCredit) > 0.00M)
                {
                    decDebitBalance = decDebit - decCredit;
                }
                lblCrProfitAndLossAmount.Text = decCredit.ToString();
                lblCrProfitAndLossBalance.Text = decCreditBalance.ToString();
                lblDrProfitAndLossAmount.Text = decDebit.ToString();
                lblDrProfitAndLossBalance.Text = decDebitBalance.ToString();
            }

            //Cash
            DataTable dtStatsCash = balayer.GetDataTable(" SELECT RootID,sum((case when Voucher_Type='C' then Amount else 0.00 end ))as `Credit Amount`,sum((case when Voucher_Type='D' then Amount else 0.00 end ))as `Debit Amount` FROM `svcf`.`voucher` where BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and ChoosenDate between '" + Fromdate + "' and '" + Todate + "' and RootID=12 group by RootID");
            if (dtStatsCash.Rows.Count == 1)
            {

                decimal decCredit = 0.00M;
                decimal decDebit = 0.00M;
                decimal decDebitBalance = 0.00M;
                decimal decCreditBalance = 0.00M;
                decimal.TryParse(balayer.ToobjectstrEvenNull(dtStatsCash.Rows[0][1].ToString()), out decCredit);
                decimal.TryParse(balayer.ToobjectstrEvenNull(dtStatsCash.Rows[0][2].ToString()), out decDebit);
                if ((decCredit - decDebit) > 0.00M)
                {
                    decCreditBalance = decCredit - decDebit;
                }
                else if ((decDebit - decCredit) > 0.00M)
                {
                    decDebitBalance = decDebit - decCredit;
                }
                lblCrCashAmountFinal.Text = decCredit.ToString();
                lblCrCashBalanceFinal.Text = decCreditBalance.ToString();
                lblDrCashAmountFinal.Text = decDebit.ToString();
                lblDrCashBalanceFinal.Text = decDebitBalance.ToString();
            }

            DataTable dtStatsPreviousdayCash = balayer.GetDataTable(" SELECT RootID,sum((case when Voucher_Type='C' then Amount else 0.00 end ))as `Credit Amount`,sum((case when Voucher_Type='D' then Amount else 0.00 end ))as `Debit Amount` FROM `svcf`.`voucher` where BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and ChoosenDate between '" + Fromdate + "' and '" + dateToConsolidated.Date.AddDays(-1).ToString("yyyy/MM/dd") + "' and RootID=12 group by RootID");
            if (dtStatsPreviousdayCash.Rows.Count == 1)
            {

                decimal decCredit = 0.00M;
                decimal decDebit = 0.00M;
                decimal decDebitBalance = 0.00M;
                decimal decCreditBalance = 0.00M;
                decimal.TryParse(balayer.ToobjectstrEvenNull(dtStatsPreviousdayCash.Rows[0][1].ToString()), out decCredit);
                decimal.TryParse(balayer.ToobjectstrEvenNull(dtStatsPreviousdayCash.Rows[0][2].ToString()), out decDebit);
                if ((decCredit - decDebit) > 0.00M)
                {
                    decCreditBalance = decCredit - decDebit;
                }
                else if ((decDebit - decCredit) > 0.00M)
                {
                    decDebitBalance = decDebit - decCredit;
                }
                lblCrCashAmountprDay.Text = decCredit.ToString();
                lblCrCashBalanceprDay.Text = decCreditBalance.ToString();
                lblDrCashAmountprDay.Text = decDebit.ToString();
                lblDrCashBalanceprDay.Text = decDebitBalance.ToString();
            }


            DataTable dtStatsCurrentDayCash = balayer.GetDataTable(" SELECT RootID,sum((case when Voucher_Type='C' then Amount else 0.00 end ))as `Credit Amount`,sum((case when Voucher_Type='D' then Amount else 0.00 end ))as `Debit Amount` FROM `svcf`.`voucher` where BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and ChoosenDate = '" + Todate + "' and RootID=12 group by RootID");
            if (dtStatsCurrentDayCash.Rows.Count == 1)
            {

                decimal decCredit = 0.00M;
                decimal decDebit = 0.00M;
                decimal decDebitBalance = 0.00M;
                decimal decCreditBalance = 0.00M;
                decimal.TryParse(balayer.ToobjectstrEvenNull(dtStatsCurrentDayCash.Rows[0][1].ToString()), out decCredit);
                decimal.TryParse(balayer.ToobjectstrEvenNull(dtStatsCurrentDayCash.Rows[0][2].ToString()), out decDebit);
                if ((decCredit - decDebit) > 0.00M)
                {
                    decCreditBalance = decCredit - decDebit;
                }
                else if ((decDebit - decCredit) > 0.00M)
                {
                    decDebitBalance = decDebit - decCredit;
                }
                lblCrCashAmount.Text = decCredit.ToString();
                lblCrCashBalance.Text = decCreditBalance.ToString();
                lblDrCashAmount.Text = decDebit.ToString();
                lblDrCashBalance.Text = decDebitBalance.ToString();
            }

        }
        protected virtual string GetLabelText(GridViewGroupRowTemplateContainer container)
        {
            return container.GroupText;
        }

        protected void select()
        {
            MySqldsBranch.ConnectionString = CommonClassFile.ConnectionString;
            MySqldsBranch.SelectCommand = @"select t1.ChoosenDate as `Date`,t8.Node as LedgerHead ,t3.Node as Branch , t1.Narration,(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )as `Credit`,(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )as `Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left join headstree as t8 on (t1.ChitGroupID=t8.NodeID) where `t1`.`BranchID` =" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and `t1`.`RootID` = 1 and t1.ChoosenDate = '" + dateToConsolidated.Date.ToString("yyyy/MM/dd") + "'";
            MySqldsInvestments.ConnectionString = CommonClassFile.ConnectionString;
            MySqldsInvestments.SelectCommand = @"select t1.ChoosenDate as `Date`,t8.Node as LedgerHead ,t3.Node as Head , t1.Narration,(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )as `Credit`,(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )as `Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left join headstree as t8 on (t1.ChitGroupID=t8.NodeID) where `t1`.`BranchID` =" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and `t1`.`RootID` = 2 and t1.ChoosenDate = '" + dateToConsolidated.Date.ToString("yyyy/MM/dd") + "'";
            
            MySqldsBanks.ConnectionString = CommonClassFile.ConnectionString;
            MySqldsBanks.SelectCommand = @"select t1.ChoosenDate as `Date`,t8.Node as LedgerHead ,bb.BankName as Bank,bb.AccountNo as `AccountNo.`,tt.CustomersBankName as CustomersBank,tt.ChequeDDNO as `ChequeNo.`,t1.Narration,(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )as `Credit`,(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )as `Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left Join headstree as t8 on (t1.ChitGroupID=t8.NodeID) left join transbank as tt on (t1.TransactionKey=tt.TransactionKey) left join bankdetails as bb on (t1.Head_Id=bb.Head_Id) where `t1`.`BranchID` =" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and `t1`.`RootID` = 3 and t1.ChoosenDate = '" + dateToConsolidated.Date.ToString("yyyy/MM/dd") + "'";

            MySqldsOtherItems.ConnectionString = CommonClassFile.ConnectionString;
            MySqldsOtherItems.SelectCommand = @"select t1.ChoosenDate as `Date`,t8.Node as Head ,t3.Node as LedgerHead , t1.Narration,(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )as `Credit`,(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )as `Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left join headstree as t8 on (t1.ChitGroupID=t8.NodeID) where `t1`.`BranchID` =" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and `t1`.`RootID` = 4 and t1.ChoosenDate = '" + dateToConsolidated.Date.ToString("yyyy/MM/dd") + "'";
            
            MySqldsChits.ConnectionString = CommonClassFile.ConnectionString;
            MySqldsChits.SelectCommand = @"select t1.ChoosenDate as `Date`,t8.Node as LedgerHead ,t3.Node as Token ,t1.Narration,(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )as `Credit`,(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )as `Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left Join headstree as t8 on t1.ChitGroupID=t8.NodeID where `t1`.`BranchID` = " + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and `t1`.`RootID` = 5 and t1.ChoosenDate ='" + dateToConsolidated.Date.ToString("yyyy/MM/dd") + "'";

            MySqldsForemanChits.ConnectionString = CommonClassFile.ConnectionString;
            MySqldsForemanChits.SelectCommand = @"select t1.ChoosenDate as `Date`,t8.Node as LedgerHead ,t3.Node as Token ,t1.Narration,(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )as `Credit`,(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )as `Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left Join headstree as t8 on (t1.ChitGroupID=t8.NodeID) where `t1`.`BranchID` = " + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and `t1`.`RootID` = 6 and t1.ChoosenDate ='" + dateToConsolidated.Date.ToString("yyyy/MM/dd") + "';";

            MySqldsDecreeDebtors.ConnectionString = CommonClassFile.ConnectionString;
            MySqldsDecreeDebtors.SelectCommand = @"SELECT  t1.ChoosenDate as `Date`,t3.Node as Head , t1.Narration,(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )as `Credit`,(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )as `Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left join headstree as t8 on (t1.ChitGroupID=t8.NodeID) where `t1`.`BranchID` = " + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and `t1`.`RootID` = 7 and t1.ChoosenDate ='" + dateToConsolidated.Date.ToString("yyyy/MM/dd") + "';";

            MySqldsLoans.ConnectionString = CommonClassFile.ConnectionString;
            MySqldsLoans.SelectCommand = @"select t1.ChoosenDate as `Date`,t3.Node as `LedgerHead` ,t1.Narration,(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )as `Credit`,(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )as `Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left Join headstree as t8 on (t1.ChitGroupID=t8.NodeID) where `t1`.`BranchID` = " + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and `t1`.`RootID` = 8 and t1.ChoosenDate='" + dateToConsolidated.Date.ToString("yyyy/MM/dd") + "';";

            MySqldsSundriesAndAdvances.ConnectionString = CommonClassFile.ConnectionString;
            MySqldsSundriesAndAdvances.SelectCommand = @"SELECT voucher.RootID,`voucher`.`ChoosenDate` as `Date`,`voucher`.`Narration`,`headstree`.`Node` as `LedgerHead`,sum((case when voucher.Voucher_Type='C' then voucher.Amount else 0.00 end )) as `Credit`,sum((case when voucher.Voucher_Type='D' then voucher.Amount else 0.00 end )) as `Debit` FROM `svcf`.`voucher` join headstree on (voucher.Head_Id=headstree.NodeID) where `voucher`.`ChoosenDate`='" + dateToConsolidated.Date.ToString("yyyy/MM/dd") + "' and voucher.BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and RootID=9 group by voucher.Head_Id";

            MySqldsStamps.ConnectionString = CommonClassFile.ConnectionString;
            MySqldsStamps.SelectCommand = @"SELECT distinct(v2.TransactionKey),ht2.Node as Head,ht1.Node as `LedgerHead`,v2.ChoosenDate as `Date`,v2.Narration,(case when v2.Voucher_Type='D' then v2.Amount else 0.00 end) as Credit, (case when v2.Voucher_Type='C' then v2.Amount else 0.00 end) as Debit FROM `svcf`.`voucher` as v1 left join `svcf`.`voucher` as v2 on (v1.DualTransactionKey=v2.DualTransactionKey )  join headstree as ht2 on v1.Head_Id=ht2.NodeID join headstree as ht1 on (v2.Head_Id=ht1.NodeID ) where v1.RootID=10 and v2.RootID<>10 and v1.BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and v2.BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and v2.ChoosenDate='" + dateToConsolidated.Date.ToString("yyyy/MM/dd") + "' and v1.ChoosenDate='" + dateToConsolidated.Date.ToString("yyyy/MM/dd") + "'";

            MySqldsProfitAndLoss.ConnectionString = CommonClassFile.ConnectionString;
            MySqldsProfitAndLoss.SelectCommand = @"select t1.ChoosenDate as `Date`,t3.Node as `LedgerHead` ,t1.Narration,(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )as `Credit`,(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )as `Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on (t4.MemberIDNew=t1.MemberID) left Join headstree as t8 on (t1.ChitGroupID=t8.NodeID) where `t1`.`BranchID` = " + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and `t1`.`RootID` = 11 and t1.ChoosenDate ='" + dateToConsolidated.Date.ToString("yyyy/MM/dd") + "' ";

            MySqldsCash.ConnectionString = CommonClassFile.ConnectionString;
            MySqldsCash.SelectCommand = @"select t1.ChoosenDate as `Date`,t8.Node as LedgerHead ,t1.Narration,(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )as `Credit`,(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )as `Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left Join headstree as t8 on (t1.ChitGroupID=t8.NodeID) where `t1`.`BranchID` = " + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and `t1`.`RootID` = 12 and t1.ChoosenDate='" + dateToConsolidated.Date.ToString("yyyy/MM/dd") + "'";
        }

        protected void BtnStatisticsGo_Click(object sender, EventArgs e)
        {
            select();
            grid.DataBind();
        }

        protected void ASPxGridView1_SummaryDisplayText(object sender, ASPxGridViewSummaryDisplayTextEventArgs e)
        {
            if (e.Item.FieldName == "Credit")
                e.Text = string.Format("{0}", Convert.ToDouble(e.Value));
            if (e.Item.FieldName == "Debit")
                e.Text = string.Format("{0}", Convert.ToDouble(e.Value));
            if (e.Item.FieldName == "Narration")
                e.Text = string.Format(ViewState["crdr1"].ToString(), Convert.ToDouble(e.Value));
        }

        protected void ASPxGridView1_OnCustomSummaryCalculate(object sender, CustomSummaryEventArgs e)
        {
            ASPxSummaryItem credit = (sender as ASPxGridView).TotalSummary["Credit"];
            ASPxSummaryItem debit = (sender as ASPxGridView).TotalSummary["Debit"];
            Decimal income = Convert.ToDecimal(((ASPxGridView)sender).GetTotalSummaryValue(credit));
            Decimal expense = Convert.ToDecimal(((ASPxGridView)sender).GetTotalSummaryValue(debit));
            if (income > expense)
            {
                ViewState["crdr1"] = "Cr. Balance:";
                e.TotalValue = income - expense;
            }
            else if (income < expense)
            {
                ViewState["crdr1"] = "Dr. Balance:";
                e.TotalValue = expense - income;
            }
            else
            {
                ViewState["crdr1"] = "";
                e.TotalValue = 0.00;
            }
            ViewState["crdr1"] += e.TotalValue.ToString();
        }

        protected void grid_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            //ApplyLayout(Int32.Parse(e.Parameters));
        }
        protected void grid_CustomUnboundColumnData(object sender, ASPxGridViewColumnDataEventArgs e)
        {

        }
        protected void Export_click(object sender, EventArgs e)
        {

        }
    }
}
