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
using SVCF_BusinessAccessLayer;
using SVCF_DataAccessLayer;

namespace SreeVisalamChitFundLtd_phase1
{
    public partial class TopColumnDayBook : System.Web.UI.Page
    {
        #region Object
        BusinessLayer balayer = new BusinessLayer();
        #endregion
        protected void btnLoanConsolidated_Click(object sender, EventArgs e)
        {
            setStats();
            hfBranch.Value = @"select t1.ChoosenDate as `Date`,t8.Node as LedgerHead ,t3.Node as Branch , t1.Narration,(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )as `Credit`,(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )as `Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left join headstree as t8 on (t1.ChitGroupID=t8.NodeID) where `t1`.`BranchID` =" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and `t1`.`RootID` = 1 and t1.ChoosenDate = '" + balayer.indiandateToMysqlDate(dateToConsolidated.Text) + "'";
            hfInvestment.Value = @"select t1.ChoosenDate as `Date`,t8.Node as LedgerHead ,t3.Node as Head , t1.Narration,(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )as `Credit`,(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )as `Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left join headstree as t8 on (t1.ChitGroupID=t8.NodeID) where `t1`.`BranchID` =" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and `t1`.`RootID` = 2 and t1.ChoosenDate = '" + balayer.indiandateToMysqlDate(dateToConsolidated.Text) + "'";
            hfBank.Value = @"select t1.ChoosenDate as `Date`,t8.Node as LedgerHead ,bb.BankName as Bank,bb.AccountNo as `AccountNo.`,tt.CustomersBankName as CustomersBank,tt.ChequeDDNO as `ChequeNo.`,t1.Narration,(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )as `Credit`,(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )as `Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left Join headstree as t8 on (t1.ChitGroupID=t8.NodeID) left join transbank as tt on (t1.TransactionKey=tt.TransactionKey) left join bankdetails as bb on (t1.Head_Id=bb.Head_Id) where `t1`.`BranchID` =" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and `t1`.`RootID` = 3 and t1.ChoosenDate = '" + balayer.indiandateToMysqlDate(dateToConsolidated.Text) + "'";
            hfOtherItems.Value = @"select t1.ChoosenDate as `Date`,t8.Node as Head ,t3.Node as LedgerHead , t1.Narration,(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )as `Credit`,(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )as `Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left join headstree as t8 on (t1.ChitGroupID=t8.NodeID) where `t1`.`BranchID` =" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and `t1`.`RootID` = 4 and t1.ChoosenDate = '" + balayer.indiandateToMysqlDate(dateToConsolidated.Text) + "'";
            hfChits.Value = @"select t1.ChoosenDate as `Date`,t8.Node as LedgerHead ,t3.Node as Token ,t1.Narration,(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )as `Credit`,(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )as `Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left Join headstree as t8 on t1.ChitGroupID=t8.NodeID where `t1`.`BranchID` = " + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and `t1`.`RootID` = 5 and t1.ChoosenDate ='" + balayer.indiandateToMysqlDate(dateToConsolidated.Text) + "'";
            hfForemanChits.Value = @"select t1.ChoosenDate as `Date`,t8.Node as LedgerHead ,t3.Node as Token ,t1.Narration,(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )as `Credit`,(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )as `Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left Join headstree as t8 on (t1.ChitGroupID=t8.NodeID) where `t1`.`BranchID` = " + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and `t1`.`RootID` = 6 and t1.ChoosenDate ='" + balayer.indiandateToMysqlDate(dateToConsolidated.Text) + "';";
            hfDecreeDebtors.Value = @"SELECT  t1.ChoosenDate as `Date`,t3.Node as Head , t1.Narration,(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )as `Credit`,(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )as `Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left join headstree as t8 on (t1.ChitGroupID=t8.NodeID) where `t1`.`BranchID` = " + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and `t1`.`RootID` = 7 and t1.ChoosenDate ='" + balayer.indiandateToMysqlDate(dateToConsolidated.Text) + "';";
            hfLoans.Value = @"select t1.ChoosenDate as `Date`,t3.Node as `LedgerHead` ,t1.Narration,(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )as `Credit`,(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )as `Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left Join headstree as t8 on (t1.ChitGroupID=t8.NodeID) where `t1`.`BranchID` = " + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and `t1`.`RootID` = 8 and t1.ChoosenDate='" + balayer.indiandateToMysqlDate(dateToConsolidated.Text) + "';";
            hfAdvances.Value = @"SELECT voucher.RootID,`voucher`.`ChoosenDate` as `Date`,`voucher`.`Narration`,`headstree`.`Node` as `LedgerHead`,sum((case when voucher.Voucher_Type='C' then voucher.Amount else 0.00 end )) as `Credit`,sum((case when voucher.Voucher_Type='D' then voucher.Amount else 0.00 end )) as `Debit` FROM `svcf`.`voucher` join headstree on (voucher.Head_Id=headstree.NodeID) where `voucher`.`ChoosenDate`='" + balayer.indiandateToMysqlDate(dateToConsolidated.Text) + "' and voucher.BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and RootID=9 group by voucher.Head_Id";
            hfStamps.Value = @"SELECT distinct(v2.TransactionKey),ht2.Node as Head,ht1.Node as `LedgerHead`,v2.ChoosenDate as `Date`,v2.Narration,(case when v2.Voucher_Type='D' then v2.Amount else 0.00 end) as Credit, (case when v2.Voucher_Type='C' then v2.Amount else 0.00 end) as Debit FROM `svcf`.`voucher` as v1 left join `svcf`.`voucher` as v2 on (v1.DualTransactionKey=v2.DualTransactionKey )  join headstree as ht2 on v1.Head_Id=ht2.NodeID join headstree as ht1 on (v2.Head_Id=ht1.NodeID ) where v1.RootID=10 and v2.RootID<>10 and v1.BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and v2.BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and v2.ChoosenDate='" + balayer.indiandateToMysqlDate(dateToConsolidated.Text) + "' and v1.ChoosenDate='" + balayer.indiandateToMysqlDate(dateToConsolidated.Text) + "'";
            hfProfitAndLoss.Value = @"select t1.ChoosenDate as `Date`,t3.Node as `LedgerHead` ,t1.Narration,(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )as `Credit`,(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )as `Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on (t4.MemberIDNew=t1.MemberID) left Join headstree as t8 on (t1.ChitGroupID=t8.NodeID) where `t1`.`BranchID` = " + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and `t1`.`RootID` = 11 and t1.ChoosenDate ='" + balayer.indiandateToMysqlDate(dateToConsolidated.Text) + "' ";
            hfCash.Value = @"select t1.ChoosenDate as `Date`,t8.Node as LedgerHead ,t1.Narration,(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )as `Credit`,(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )as `Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left Join headstree as t8 on (t1.ChitGroupID=t8.NodeID) where `t1`.`BranchID` = " + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and `t1`.`RootID` = 12 and t1.ChoosenDate='" + balayer.indiandateToMysqlDate(dateToConsolidated.Text) + "'";
            select();
        }
        protected void btnConsolidatedView_Click(object sender, EventArgs e)
        {
            if (dateToConsolidated.Text != null || dateToConsolidated.Text != "")
            {
                Response.Redirect("TopColumnDaybookConsolidatedView.aspx?FromDate=" + dateToConsolidated.Text + "");
            }
            else
            {
                Response.Redirect("TopColumnDaybookConsolidatedView.aspx");
            }
        }
        protected void aDrBranchAmountprDay_Click(object sender, EventArgs e)
        {
            grid.FilterExpression = "";
            ClientScriptManager script = Page.ClientScript;
            grid.FilterExpression = " [Debit] != '0.00'";
            grid.Focus();
            lblMaintain.Text = "li_Branch";
            grid.SettingsText.Title = "Branch Debit Amount on : " + DateTime.Parse(dateToConsolidated.Text).AddDays(-1).ToString("dd/MM/yyyy");
            MySqldsBranch.SelectCommand = @"select t1.ChoosenDate as `Date`,t8.Node as LedgerHead ,t3.Node as Branch , t1.Narration,(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )as `Credit`,(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )as `Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left join headstree as t8 on (t1.ChitGroupID=t8.NodeID) where `t1`.`BranchID` =" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and `t1`.`RootID` = 1 and t1.ChoosenDate < '" + balayer.indiandateToMysqlDate( dateToConsolidated.Text) + "'";
            hfBranch.Value = MySqldsBranch.SelectCommand;
            grid.DataBind();
            lblMaintainPost.Text = "post";
        }

        protected void aCrBranchAmountprDay_Click(object sender, EventArgs e)
        {
            grid.FilterExpression = "";
            ClientScriptManager script = Page.ClientScript;
            grid.FilterExpression = "[Credit] != '0.00'";
            grid.Focus();
            grid.SettingsText.Title = "Branch Credit Amount on : " + DateTime.Parse(dateToConsolidated.Text).AddDays(-1).ToString("dd/MM/yyyy");
            MySqldsBranch.SelectCommand = @"select t1.ChoosenDate as `Date`,t8.Node as LedgerHead ,t3.Node as Branch , t1.Narration,(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )as `Credit`,(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )as `Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left join headstree as t8 on (t1.ChitGroupID=t8.NodeID) where `t1`.`BranchID` =" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and `t1`.`RootID` = 1 and t1.ChoosenDate < '" + balayer.indiandateToMysqlDate(dateToConsolidated.Text) + "'";
            lblMaintain.Text = "li_Branch";
            hfBranch.Value = MySqldsBranch.SelectCommand;
            grid.DataBind();
            lblMaintainPost.Text = "post";
        }

        protected void aCrBranchAmountcrDay_Click(object sender, EventArgs e)
        {
            grid.FilterExpression = "";
            ClientScriptManager script = Page.ClientScript;
            grid.FilterExpression = "[Credit] != '0.00'";
            grid.Focus();
            grid.SettingsText.Title = "Branch Credit Amount on : " + DateTime.Parse(dateToConsolidated.Text).ToString("dd/MM/yyyy");
            MySqldsBranch.SelectCommand = @"select t1.ChoosenDate as `Date`,t8.Node as LedgerHead ,t3.Node as Branch , t1.Narration,(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )as `Credit`,(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )as `Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left join headstree as t8 on (t1.ChitGroupID=t8.NodeID) where `t1`.`BranchID` =" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and `t1`.`RootID` = 1 and t1.ChoosenDate <= '" + balayer.indiandateToMysqlDate(dateToConsolidated.Text) + "'";
            lblMaintain.Text = "li_Branch";
            hfBranch.Value = MySqldsBranch.SelectCommand;
            grid.DataBind();
            lblMaintainPost.Text = "post";
        }

        protected void aDrBranchAmountcrDay_Click(object sender, EventArgs e)
        {
            grid.FilterExpression = "";
            ClientScriptManager script = Page.ClientScript;
            grid.FilterExpression = "[Debit] != '0.00'";
            grid.Focus();
            grid.SettingsText.Title = "Branch Debit Amount on : " + DateTime.Parse(dateToConsolidated.Text).ToString("dd/MM/yyyy");
            MySqldsBranch.SelectCommand = @"select t1.ChoosenDate as `Date`,t8.Node as LedgerHead ,t3.Node as Branch , t1.Narration,(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )as `Credit`,(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )as `Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left join headstree as t8 on (t1.ChitGroupID=t8.NodeID) where `t1`.`BranchID` =" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and `t1`.`RootID` = 1 and t1.ChoosenDate <= '" + balayer.indiandateToMysqlDate(dateToConsolidated.Text) + "'";
            lblMaintain.Text = "li_Branch";
            hfBranch.Value = MySqldsBranch.SelectCommand;
            grid.DataBind();
            lblMaintainPost.Text = "post";
        }

        protected void aCrBranchAmounttoDay_Click(object sender, EventArgs e)
        {
            grid.FilterExpression = "";
            ClientScriptManager script = Page.ClientScript;
            grid.FilterExpression = "[Credit] != '0.00'";
            grid.Focus();
            grid.SettingsText.Title = "Branch Credit Amount on Particular Day : " + DateTime.Parse(dateToConsolidated.Text).ToString("dd/MM/yyyy");
            MySqldsBranch.SelectCommand = @"select t1.ChoosenDate as `Date`,t8.Node as LedgerHead ,t3.Node as Branch , t1.Narration,(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )as `Credit`,(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )as `Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left join headstree as t8 on (t1.ChitGroupID=t8.NodeID) where `t1`.`BranchID` =" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and `t1`.`RootID` = 1 and t1.ChoosenDate = '" + balayer.indiandateToMysqlDate(dateToConsolidated.Text) + "'";
            lblMaintain.Text = "li_Branch";
            hfBranch.Value = MySqldsBranch.SelectCommand;
            grid.DataBind();
            lblMaintainPost.Text = "post";
        }

        protected void aDrBranchAmounttoDay_Click(object sender, EventArgs e)
        {
            grid.FilterExpression = "";
            ClientScriptManager script = Page.ClientScript;
            grid.FilterExpression = "[Debit] != '0.00'";
            grid.Focus();
            grid.SettingsText.Title = "Branch Debit Amount on Particular Day : " + DateTime.Parse(dateToConsolidated.Text).ToString("dd/MM/yyyy");
            MySqldsBranch.SelectCommand = @"select t1.ChoosenDate as `Date`,t8.Node as LedgerHead ,t3.Node as Branch , t1.Narration,(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )as `Credit`,(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )as `Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left join headstree as t8 on (t1.ChitGroupID=t8.NodeID) where `t1`.`BranchID` =" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and `t1`.`RootID` = 1 and t1.ChoosenDate = '" + balayer.indiandateToMysqlDate(dateToConsolidated.Text) + "'";
            lblMaintain.Text = "li_Branch";
            hfBranch.Value = MySqldsBranch.SelectCommand;
            grid.DataBind();
            lblMaintainPost.Text = "post";
        }

        protected void TotalBranchcr_Click(object sender, EventArgs e)
        {
            grid.FilterExpression = "";
            ClientScriptManager script = Page.ClientScript;
            grid.Focus();
            grid.SettingsText.Title = "Total Branch Amount on : " + DateTime.Parse(dateToConsolidated.Text).ToString("dd/MM/yyyy");
            MySqldsBranch.SelectCommand = @"select t1.ChoosenDate as `Date`,t8.Node as LedgerHead ,t3.Node as Branch , t1.Narration,(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )as `Credit`,(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )as `Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left join headstree as t8 on (t1.ChitGroupID=t8.NodeID) where `t1`.`BranchID` =" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and `t1`.`RootID` = 1 and t1.ChoosenDate <= '" + balayer.indiandateToMysqlDate(dateToConsolidated.Text) + "'";
            lblMaintain.Text = "li_Branch";
            hfBranch.Value = MySqldsBranch.SelectCommand;
            grid.DataBind();
            lblMaintainPost.Text = "post";
        }

        protected void TotalBranchpr_Click(object sender, EventArgs e)
        {
            grid.FilterExpression = "";
            ClientScriptManager script = Page.ClientScript;
            grid.Focus();
            grid.SettingsText.Title = "Total Branch Amount on : " + DateTime.Parse(dateToConsolidated.Text).AddDays(-1).ToString("dd/MM/yyyy");
            MySqldsBranch.SelectCommand = @"select t1.ChoosenDate as `Date`,t8.Node as LedgerHead ,t3.Node as Branch , t1.Narration,(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )as `Credit`,(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )as `Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left join headstree as t8 on (t1.ChitGroupID=t8.NodeID) where `t1`.`BranchID` =" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and `t1`.`RootID` = 1 and t1.ChoosenDate < '" + balayer.indiandateToMysqlDate(dateToConsolidated.Text) + "'";
            lblMaintain.Text = "li_Branch";
            hfBranch.Value = MySqldsBranch.SelectCommand;
            grid.DataBind();
            lblMaintainPost.Text = "post";
        }

        protected void TotalBranchto_Click(object sender, EventArgs e)
        {
            grid.FilterExpression = "";
            ClientScriptManager script = Page.ClientScript;
            grid.Focus();
            grid.SettingsText.Title = "Total Branch Amount on Particular Day: " + DateTime.Parse(dateToConsolidated.Text).AddDays(-1).ToString("dd/MM/yyyy");
            MySqldsBranch.SelectCommand = @"select t1.ChoosenDate as `Date`,t8.Node as LedgerHead ,t3.Node as Branch , t1.Narration,(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )as `Credit`,(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )as `Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left join headstree as t8 on (t1.ChitGroupID=t8.NodeID) where `t1`.`BranchID` =" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and `t1`.`RootID` = 1 and t1.ChoosenDate = '" + balayer.indiandateToMysqlDate(dateToConsolidated.Text) + "'";
            lblMaintain.Text = "li_Branch";
            hfBranch.Value = MySqldsBranch.SelectCommand;
            grid.DataBind();
            lblMaintainPost.Text = "post";
        }

        protected void aDrInvestmentAmountprDay_Click(object sender, EventArgs e)
        {
            gridInvestments.FilterExpression = "";
            ClientScriptManager script = Page.ClientScript;
            gridInvestments.FilterExpression = "[Debit] != '0.00'";
            gridInvestments.Focus();
            gridInvestments.SettingsText.Title = "Investments Debit Amount on : " + DateTime.Parse(dateToConsolidated.Text).AddDays(-1).ToString("dd/MM/yyyy");
            lblMaintain.Text = "li_InvestMents";
            MySqldsInvestments.ConnectionString = CommonClassFile.ConnectionString;
            MySqldsInvestments.SelectCommand = @"select t1.ChoosenDate as `Date`,t8.Node as LedgerHead ,t3.Node as Head , t1.Narration,(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )as `Credit`,(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )as `Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left join headstree as t8 on (t1.ChitGroupID=t8.NodeID) where `t1`.`BranchID` =" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and `t1`.`RootID` = 2 and t1.ChoosenDate < '" + balayer.indiandateToMysqlDate(dateToConsolidated.Text) + "'";
            hfInvestment.Value = MySqldsInvestments.SelectCommand;
            gridInvestments.DataBind();
            lblMaintainPost.Text = "post";
        }

        protected void aCrInvestmentAmountprDay_Click(object sender, EventArgs e)
        {
            gridInvestments.FilterExpression = "";
            ClientScriptManager script = Page.ClientScript;
            gridInvestments.FilterExpression = "[Credit] != '0.00'";
            gridInvestments.Focus();
            gridInvestments.SettingsText.Title = "Investments Credit Amount on : " + DateTime.Parse(dateToConsolidated.Text).AddDays(-1).ToString("dd/MM/yyyy");
            lblMaintain.Text = "li_InvestMents";
            MySqldsInvestments.ConnectionString = CommonClassFile.ConnectionString;
            MySqldsInvestments.SelectCommand = @"select t1.ChoosenDate as `Date`,t8.Node as LedgerHead ,t3.Node as Head , t1.Narration,(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )as `Credit`,(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )as `Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left join headstree as t8 on (t1.ChitGroupID=t8.NodeID) where `t1`.`BranchID` =" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and `t1`.`RootID` = 2 and t1.ChoosenDate < '" + balayer.indiandateToMysqlDate(dateToConsolidated.Text) + "'";
            hfInvestment.Value = MySqldsInvestments.SelectCommand;
            gridInvestments.DataBind();
            lblMaintainPost.Text = "post";
        }

        protected void aCrInvestmentsAmountcrDay_Click(object sender, EventArgs e)
        {
            gridInvestments.FilterExpression = "";
            ClientScriptManager script = Page.ClientScript;
            gridInvestments.FilterExpression = "[Credit] != '0.00'";
            gridInvestments.Focus();
            lblMaintain.Text = "li_InvestMents";
            gridInvestments.SettingsText.Title = "Investments Credit Amount on : " + DateTime.Parse(dateToConsolidated.Text).ToString("dd/MM/yyyy");
            MySqldsInvestments.ConnectionString = CommonClassFile.ConnectionString;
            MySqldsInvestments.SelectCommand = @"select t1.ChoosenDate as `Date`,t8.Node as LedgerHead ,t3.Node as Head , t1.Narration,(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )as `Credit`,(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )as `Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left join headstree as t8 on (t1.ChitGroupID=t8.NodeID) where `t1`.`BranchID` =" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and `t1`.`RootID` = 2 and t1.ChoosenDate <= '" + balayer.indiandateToMysqlDate(dateToConsolidated.Text) + "'";
            hfInvestment.Value = MySqldsInvestments.SelectCommand;
            gridInvestments.DataBind();
            lblMaintainPost.Text = "post";
        }

        protected void aDrInvestmentsAmountcrDay_Click(object sender, EventArgs e)
        {
            gridInvestments.FilterExpression = "";
            ClientScriptManager script = Page.ClientScript;
            gridInvestments.FilterExpression = "[Debit] != '0.00'";
            gridInvestments.Focus();
            lblMaintain.Text = "li_InvestMents";
            gridInvestments.SettingsText.Title = "Investments Debit Amount on: " + DateTime.Parse(dateToConsolidated.Text).ToString("dd/MM/yyyy");
            MySqldsInvestments.ConnectionString = CommonClassFile.ConnectionString;
            MySqldsInvestments.SelectCommand = @"select t1.ChoosenDate as `Date`,t8.Node as LedgerHead ,t3.Node as Head , t1.Narration,(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )as `Credit`,(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )as `Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left join headstree as t8 on (t1.ChitGroupID=t8.NodeID) where `t1`.`BranchID` =" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and `t1`.`RootID` = 2 and t1.ChoosenDate <= '" + balayer.indiandateToMysqlDate(dateToConsolidated.Text) + "'";
            hfInvestment.Value = MySqldsInvestments.SelectCommand;
            gridInvestments.DataBind();
            lblMaintainPost.Text = "post";
        }

        protected void aCrInvestmentsAmounttoDay_Click(object sender, EventArgs e)
        {
            gridInvestments.FilterExpression = "";
            ClientScriptManager script = Page.ClientScript;
            gridInvestments.FilterExpression = "[Credit] != '0.00'";
            gridInvestments.Focus();
            lblMaintain.Text = "li_InvestMents";
            gridInvestments.SettingsText.Title = "Investments Credit Amount on Particular Day: " + DateTime.Parse(dateToConsolidated.Text).ToString("dd/MM/yyyy");
            MySqldsInvestments.ConnectionString = CommonClassFile.ConnectionString;
            MySqldsInvestments.SelectCommand = @"select t1.ChoosenDate as `Date`,t8.Node as LedgerHead ,t3.Node as Head , t1.Narration,(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )as `Credit`,(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )as `Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left join headstree as t8 on (t1.ChitGroupID=t8.NodeID) where `t1`.`BranchID` =" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and `t1`.`RootID` = 2 and t1.ChoosenDate = '" + balayer.indiandateToMysqlDate(dateToConsolidated.Text) + "'";
            hfInvestment.Value = MySqldsInvestments.SelectCommand;
            gridInvestments.DataBind();
            lblMaintainPost.Text = "post";
        }

        protected void aDrInvestmentsAmounttoDay_Click(object sender, EventArgs e)
        {
            gridInvestments.FilterExpression = "";
            ClientScriptManager script = Page.ClientScript;
            gridInvestments.FilterExpression = "[Debit] != '0.00'";
            gridInvestments.Focus();
            lblMaintain.Text = "li_InvestMents";
            gridInvestments.SettingsText.Title = "Investments Debit Amount on Particular Day: " + DateTime.Parse(dateToConsolidated.Text).ToString("dd/MM/yyyy");
            MySqldsInvestments.ConnectionString = CommonClassFile.ConnectionString;
            MySqldsInvestments.SelectCommand = @"select t1.ChoosenDate as `Date`,t8.Node as LedgerHead ,t3.Node as Head , t1.Narration,(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )as `Credit`,(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )as `Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left join headstree as t8 on (t1.ChitGroupID=t8.NodeID) where `t1`.`BranchID` =" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and `t1`.`RootID` = 2 and t1.ChoosenDate = '" + balayer.indiandateToMysqlDate(dateToConsolidated.Text) + "'";
            hfInvestment.Value = MySqldsInvestments.SelectCommand;
            gridInvestments.DataBind();
            lblMaintainPost.Text = "post";
        }

        protected void TotalInvestmentcr_Click(object sender, EventArgs e)
        {
            gridInvestments.FilterExpression = "";
            ClientScriptManager script = Page.ClientScript;
            gridInvestments.Focus();
            lblMaintain.Text = "li_InvestMents";
            gridInvestments.SettingsText.Title = "Total Investments Amount on : " + DateTime.Parse(dateToConsolidated.Text).ToString("dd/MM/yyyy");
            MySqldsInvestments.ConnectionString = CommonClassFile.ConnectionString;
            MySqldsInvestments.SelectCommand = @"select t1.ChoosenDate as `Date`,t8.Node as LedgerHead ,t3.Node as Head , t1.Narration,(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )as `Credit`,(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )as `Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left join headstree as t8 on (t1.ChitGroupID=t8.NodeID) where `t1`.`BranchID` =" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and `t1`.`RootID` = 2 and t1.ChoosenDate <= '" + balayer.indiandateToMysqlDate(dateToConsolidated.Text) + "'";
            hfInvestment.Value = MySqldsInvestments.SelectCommand;
            gridInvestments.DataBind();
            lblMaintainPost.Text = "post";
        }

        protected void TotalInvestmentpr_Click(object sender, EventArgs e)
        {
            gridInvestments.FilterExpression = "";
            ClientScriptManager script = Page.ClientScript;
            gridInvestments.Focus();
            lblMaintain.Text = "li_InvestMents";
            gridInvestments.SettingsText.Title = "Total Investments Amount on : " + DateTime.Parse(dateToConsolidated.Text).AddDays(-1).ToString("dd/MM/yyyy");
            MySqldsInvestments.ConnectionString = CommonClassFile.ConnectionString;
            MySqldsInvestments.SelectCommand = @"select t1.ChoosenDate as `Date`,t8.Node as LedgerHead ,t3.Node as Head , t1.Narration,(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )as `Credit`,(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )as `Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left join headstree as t8 on (t1.ChitGroupID=t8.NodeID) where `t1`.`BranchID` =" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and `t1`.`RootID` = 2 and t1.ChoosenDate < '" + balayer.indiandateToMysqlDate(dateToConsolidated.Text) + "'";
            hfInvestment.Value = MySqldsInvestments.SelectCommand;
            gridInvestments.DataBind();
            lblMaintainPost.Text = "post";
        }

        protected void TotalInvestmentto_Click(object sender, EventArgs e)
        {
            gridInvestments.FilterExpression = "";
            ClientScriptManager script = Page.ClientScript;
            gridInvestments.Focus();
            lblMaintain.Text = "li_InvestMents";
            gridInvestments.SettingsText.Title = "Total Investments Amount on Particular Day: " + DateTime.Parse(dateToConsolidated.Text).ToString("dd/MM/yyyy");
            MySqldsInvestments.ConnectionString = CommonClassFile.ConnectionString;
            MySqldsInvestments.SelectCommand = @"select t1.ChoosenDate as `Date`,t8.Node as LedgerHead ,t3.Node as Head , t1.Narration,(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )as `Credit`,(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )as `Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left join headstree as t8 on (t1.ChitGroupID=t8.NodeID) where `t1`.`BranchID` =" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and `t1`.`RootID` = 2 and t1.ChoosenDate = '" + balayer.indiandateToMysqlDate(dateToConsolidated.Text) + "'";
            hfInvestment.Value = MySqldsInvestments.SelectCommand;
            gridInvestments.DataBind();
            lblMaintainPost.Text = "post";
        }

        protected void aDrBanksAmountprDay_Click(object sender, EventArgs e)
        {
            gridBanks.FilterExpression = "";
            ClientScriptManager script = Page.ClientScript;
            gridBanks.FilterExpression = "[Debit] != '0.00'";
            gridBanks.Focus();
            gridBanks.SettingsText.Title = "Banks Debit Amount on :" + DateTime.Parse(dateToConsolidated.Text).AddDays(-1).ToString("dd/MM/yyyy");
            MySqldsBanks.ConnectionString = CommonClassFile.ConnectionString;
            MySqldsBanks.SelectCommand = @"select t1.ChoosenDate as `Date`,t8.Node as LedgerHead ,bb.BankName as Bank,bb.AccountNo as `AccountNo.`,tt.CustomersBankName as CustomersBank,tt.ChequeDDNO as `ChequeNo.`,t1.Narration,(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )as `Credit`,(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )as `Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left Join headstree as t8 on (t1.ChitGroupID=t8.NodeID) left join transbank as tt on (t1.TransactionKey=tt.TransactionKey) left join bankdetails as bb on (t1.Head_Id=bb.Head_Id) where `t1`.`BranchID` =" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and `t1`.`RootID` = 3 and t1.ChoosenDate < '" + balayer.indiandateToMysqlDate(dateToConsolidated.Text) + "'";
            hfBank.Value = MySqldsBanks.SelectCommand;
            lblMaintain.Text = "li_Banks";
            gridBanks.DataBind();
            lblMaintainPost.Text = "post";
        }

        protected void aCrBanksAmountprDay_Click(object sender, EventArgs e)
        {
            gridBanks.FilterExpression = "";
            ClientScriptManager script = Page.ClientScript;
            gridBanks.FilterExpression = "[Credit] != '0.00'";
            gridBanks.Focus();
            gridBanks.SettingsText.Title = "Banks Credit Amount on :" + DateTime.Parse(dateToConsolidated.Text).AddDays(-1).ToString("dd/MM/yyyy");
            MySqldsBanks.ConnectionString = CommonClassFile.ConnectionString;
            MySqldsBanks.SelectCommand = @"select t1.ChoosenDate as `Date`,t8.Node as LedgerHead ,bb.BankName as Bank,bb.AccountNo as `AccountNo.`,tt.CustomersBankName as CustomersBank,tt.ChequeDDNO as `ChequeNo.`,t1.Narration,(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )as `Credit`,(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )as `Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left Join headstree as t8 on (t1.ChitGroupID=t8.NodeID) left join transbank as tt on (t1.TransactionKey=tt.TransactionKey) left join bankdetails as bb on (t1.Head_Id=bb.Head_Id) where `t1`.`BranchID` =" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and `t1`.`RootID` = 3 and t1.ChoosenDate < '" + balayer.indiandateToMysqlDate(dateToConsolidated.Text) + "'";
            hfBank.Value = MySqldsBanks.SelectCommand;
            lblMaintain.Text = "li_Banks";
            gridBanks.DataBind();
            lblMaintainPost.Text = "post";
        }

        protected void aCrBanksAmountcrDay_Click(object sender, EventArgs e)
        {
            gridBanks.FilterExpression = "";
            ClientScriptManager script = Page.ClientScript;
            gridBanks.FilterExpression = "[Credit] != '0.00'";
            gridBanks.Focus();
            gridBanks.SettingsText.Title = "Banks Credit Amount on :" + DateTime.Parse(dateToConsolidated.Text).ToString("dd/MM/yyyy");
            MySqldsBanks.ConnectionString = CommonClassFile.ConnectionString;
            MySqldsBanks.SelectCommand = @"select t1.ChoosenDate as `Date`,t8.Node as LedgerHead ,bb.BankName as Bank,bb.AccountNo as `AccountNo.`,tt.CustomersBankName as CustomersBank,tt.ChequeDDNO as `ChequeNo.`,t1.Narration,(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )as `Credit`,(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )as `Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left Join headstree as t8 on (t1.ChitGroupID=t8.NodeID) left join transbank as tt on (t1.TransactionKey=tt.TransactionKey) left join bankdetails as bb on (t1.Head_Id=bb.Head_Id) where `t1`.`BranchID` =" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and `t1`.`RootID` = 3 and t1.ChoosenDate <= '" + balayer.indiandateToMysqlDate(dateToConsolidated.Text) + "'";
            hfBank.Value = MySqldsBanks.SelectCommand;
            lblMaintain.Text = "li_Banks";
            gridBanks.DataBind();
            lblMaintainPost.Text = "post";
        }

        protected void aDrBanksAmountcrDay_Click(object sender, EventArgs e)
        {
            gridBanks.FilterExpression = "";
            ClientScriptManager script = Page.ClientScript;
            gridBanks.FilterExpression = "[Debit] != '0.00'";
            gridBanks.Focus();
            gridBanks.SettingsText.Title = "Banks Debit Amount on :" + DateTime.Parse(dateToConsolidated.Text).ToString("dd/MM/yyyy");
            MySqldsBanks.ConnectionString = CommonClassFile.ConnectionString;
            MySqldsBanks.SelectCommand = @"select t1.ChoosenDate as `Date`,t8.Node as LedgerHead ,bb.BankName as Bank,bb.AccountNo as `AccountNo.`,tt.CustomersBankName as CustomersBank,tt.ChequeDDNO as `ChequeNo.`,t1.Narration,(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )as `Credit`,(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )as `Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left Join headstree as t8 on (t1.ChitGroupID=t8.NodeID) left join transbank as tt on (t1.TransactionKey=tt.TransactionKey) left join bankdetails as bb on (t1.Head_Id=bb.Head_Id) where `t1`.`BranchID` =" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and `t1`.`RootID` = 3 and t1.ChoosenDate <= '" + balayer.indiandateToMysqlDate(dateToConsolidated.Text) + "'";
            hfBank.Value = MySqldsBanks.SelectCommand;
            lblMaintain.Text = "li_Banks";
            gridBanks.DataBind();
            lblMaintainPost.Text = "post";
        }

        protected void aCrBanksAmounttoDay_Click(object sender, EventArgs e)
        {
            gridBanks.FilterExpression = "";
            ClientScriptManager script = Page.ClientScript;
            gridBanks.FilterExpression = "[Credit] != '0.00'";
            gridBanks.Focus();
            gridBanks.SettingsText.Title = "Banks Credit Amount on Particular Day:" + DateTime.Parse(dateToConsolidated.Text).ToString("dd/MM/yyyy");
            MySqldsBanks.ConnectionString = CommonClassFile.ConnectionString;
            MySqldsBanks.SelectCommand = @"select t1.ChoosenDate as `Date`,t8.Node as LedgerHead ,bb.BankName as Bank,bb.AccountNo as `AccountNo.`,tt.CustomersBankName as CustomersBank,tt.ChequeDDNO as `ChequeNo.`,t1.Narration,(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )as `Credit`,(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )as `Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left Join headstree as t8 on (t1.ChitGroupID=t8.NodeID) left join transbank as tt on (t1.TransactionKey=tt.TransactionKey) left join bankdetails as bb on (t1.Head_Id=bb.Head_Id) where `t1`.`BranchID` =" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and `t1`.`RootID` = 3 and t1.ChoosenDate = '" + balayer.indiandateToMysqlDate(dateToConsolidated.Text) + "'";
            hfBank.Value = MySqldsBanks.SelectCommand;
            lblMaintain.Text = "li_Banks";
            gridBanks.DataBind();
            lblMaintainPost.Text = "post";
        }

        protected void aDrBanksAmounttoDay_Click(object sender, EventArgs e)
        {
            gridBanks.FilterExpression = "";
            ClientScriptManager script = Page.ClientScript;
            gridBanks.FilterExpression = "[Debit] != '0.00'";
            gridBanks.Focus();
            gridBanks.SettingsText.Title = "Banks Debit Amount on Particular Day:" + DateTime.Parse(dateToConsolidated.Text).ToString("dd/MM/yyyy");
            MySqldsBanks.ConnectionString = CommonClassFile.ConnectionString;
            MySqldsBanks.SelectCommand = @"select t1.ChoosenDate as `Date`,t8.Node as LedgerHead ,bb.BankName as Bank,bb.AccountNo as `AccountNo.`,tt.CustomersBankName as CustomersBank,tt.ChequeDDNO as `ChequeNo.`,t1.Narration,(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )as `Credit`,(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )as `Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left Join headstree as t8 on (t1.ChitGroupID=t8.NodeID) left join transbank as tt on (t1.TransactionKey=tt.TransactionKey) left join bankdetails as bb on (t1.Head_Id=bb.Head_Id) where `t1`.`BranchID` =" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and `t1`.`RootID` = 3 and t1.ChoosenDate = '" + balayer.indiandateToMysqlDate(dateToConsolidated.Text) + "'";
            hfBank.Value = MySqldsBanks.SelectCommand;
            lblMaintain.Text = "li_Banks";
            gridBanks.DataBind();
            lblMaintainPost.Text = "post";
        }

        protected void TotalBankscr_Click(object sender, EventArgs e)
        {
            gridBanks.FilterExpression = "";
            ClientScriptManager script = Page.ClientScript;
            gridBanks.Focus();
            gridBanks.SettingsText.Title = "Total Banks Amount on :" + DateTime.Parse(dateToConsolidated.Text).ToString("dd/MM/yyyy");
            MySqldsBanks.ConnectionString = CommonClassFile.ConnectionString;
            MySqldsBanks.SelectCommand = @"select t1.ChoosenDate as `Date`,t8.Node as LedgerHead ,bb.BankName as Bank,bb.AccountNo as `AccountNo.`,tt.CustomersBankName as CustomersBank,tt.ChequeDDNO as `ChequeNo.`,t1.Narration,(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )as `Credit`,(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )as `Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left Join headstree as t8 on (t1.ChitGroupID=t8.NodeID) left join transbank as tt on (t1.TransactionKey=tt.TransactionKey) left join bankdetails as bb on (t1.Head_Id=bb.Head_Id) where `t1`.`BranchID` =" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and `t1`.`RootID` = 3 and t1.ChoosenDate <= '" + balayer.indiandateToMysqlDate(dateToConsolidated.Text) + "'";
            hfBank.Value = MySqldsBanks.SelectCommand;
            lblMaintain.Text = "li_Banks";
            gridBanks.DataBind();
            lblMaintainPost.Text = "post";
        }

        protected void TotalBankspr_Click(object sender, EventArgs e)
        {
            gridBanks.FilterExpression = "";
            ClientScriptManager script = Page.ClientScript;
            gridBanks.Focus();
            gridBanks.SettingsText.Title = "Total Banks Amount on :" + DateTime.Parse(dateToConsolidated.Text).AddDays(-1).ToString("dd/MM/yyyy");
            MySqldsBanks.ConnectionString = CommonClassFile.ConnectionString;
            MySqldsBanks.SelectCommand = @"select t1.ChoosenDate as `Date`,t8.Node as LedgerHead ,bb.BankName as Bank,bb.AccountNo as `AccountNo.`,tt.CustomersBankName as CustomersBank,tt.ChequeDDNO as `ChequeNo.`,t1.Narration,(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )as `Credit`,(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )as `Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left Join headstree as t8 on (t1.ChitGroupID=t8.NodeID) left join transbank as tt on (t1.TransactionKey=tt.TransactionKey) left join bankdetails as bb on (t1.Head_Id=bb.Head_Id) where `t1`.`BranchID` =" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and `t1`.`RootID` = 3 and t1.ChoosenDate < '" + balayer.indiandateToMysqlDate(dateToConsolidated.Text) + "'";
            hfBank.Value = MySqldsBanks.SelectCommand;
            lblMaintain.Text = "li_Banks";
            gridBanks.DataBind();
            lblMaintainPost.Text = "post";
        }

        protected void TotalBanksto_Click(object sender, EventArgs e)
        {
            gridBanks.FilterExpression = "";
            ClientScriptManager script = Page.ClientScript;
            gridBanks.Focus();
            gridBanks.SettingsText.Title = "Total Banks Amount on Particular Day:" + DateTime.Parse(dateToConsolidated.Text).ToString("dd/MM/yyyy");
            MySqldsBanks.ConnectionString = CommonClassFile.ConnectionString;
            MySqldsBanks.SelectCommand = @"select t1.ChoosenDate as `Date`,t8.Node as LedgerHead ,bb.BankName as Bank,bb.AccountNo as `AccountNo.`,tt.CustomersBankName as CustomersBank,tt.ChequeDDNO as `ChequeNo.`,t1.Narration,(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )as `Credit`,(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )as `Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left Join headstree as t8 on (t1.ChitGroupID=t8.NodeID) left join transbank as tt on (t1.TransactionKey=tt.TransactionKey) left join bankdetails as bb on (t1.Head_Id=bb.Head_Id) where `t1`.`BranchID` =" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and `t1`.`RootID` = 3 and t1.ChoosenDate = '" + balayer.indiandateToMysqlDate(dateToConsolidated.Text) + "'";
            hfBank.Value = MySqldsBanks.SelectCommand;
            lblMaintain.Text = "li_Banks";
            gridBanks.DataBind();
            lblMaintainPost.Text = "post";
        }

        protected void aDrOtherItemsAmountprDay_Click(object sender, EventArgs e)
        {
            gridOtherItems.FilterExpression = "";
            ClientScriptManager script = Page.ClientScript;
            gridOtherItems.FilterExpression = "[Debit] != '0.00'";
            gridOtherItems.Focus();
            lblMaintain.Text = "li_OtherItems";
            gridOtherItems.SettingsText.Title = "Other Items Debit Amount on :" + DateTime.Parse(dateToConsolidated.Text).AddDays(-1).ToString("dd/MM/yyyy");
            MySqldsOtherItems.ConnectionString = CommonClassFile.ConnectionString;
            MySqldsOtherItems.SelectCommand = @"select t1.ChoosenDate as `Date`,t8.Node as Head ,t3.Node as LedgerHead , t1.Narration,(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )as `Credit`,(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )as `Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left join headstree as t8 on (t1.ChitGroupID=t8.NodeID) where `t1`.`BranchID` =" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and `t1`.`RootID` = 4 and t1.ChoosenDate < '" + balayer.indiandateToMysqlDate(dateToConsolidated.Text) + "'";
            hfOtherItems.Value = MySqldsOtherItems.SelectCommand;
            gridOtherItems.DataBind();
            lblMaintainPost.Text = "post";
        }

        protected void aCrOtherItemsAmountprDay_Click(object sender, EventArgs e)
        {
            gridOtherItems.FilterExpression = "";
            ClientScriptManager script = Page.ClientScript;
            gridOtherItems.FilterExpression = "[Credit] != '0.00'";
            gridOtherItems.Focus();
            lblMaintain.Text = "li_OtherItems";
            gridOtherItems.SettingsText.Title = "Other Items Credit Amount on :" + DateTime.Parse(dateToConsolidated.Text).AddDays(-1).ToString("dd/MM/yyyy");
            MySqldsOtherItems.ConnectionString = CommonClassFile.ConnectionString;
            MySqldsOtherItems.SelectCommand = @"select t1.ChoosenDate as `Date`,t8.Node as Head ,t3.Node as LedgerHead , t1.Narration,(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )as `Credit`,(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )as `Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left join headstree as t8 on (t1.ChitGroupID=t8.NodeID) where `t1`.`BranchID` =" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and `t1`.`RootID` = 4 and t1.ChoosenDate < '" + balayer.indiandateToMysqlDate(dateToConsolidated.Text) + "'";
            hfOtherItems.Value = MySqldsOtherItems.SelectCommand;
            gridOtherItems.DataBind();
            lblMaintainPost.Text = "post";
        }

        protected void aCrOtherItemsAmountcrDay_Click(object sender, EventArgs e)
        {
            gridOtherItems.FilterExpression = "";
            ClientScriptManager script = Page.ClientScript;
            gridOtherItems.FilterExpression = "[Credit] != '0.00'";
            gridOtherItems.Focus();
            lblMaintain.Text = "li_OtherItems";
            gridOtherItems.SettingsText.Title = "Other Items Credit Amount on :" + DateTime.Parse(dateToConsolidated.Text).ToString("dd/MM/yyyy");
            MySqldsOtherItems.ConnectionString = CommonClassFile.ConnectionString;
            MySqldsOtherItems.SelectCommand = @"select t1.ChoosenDate as `Date`,t8.Node as Head ,t3.Node as LedgerHead , t1.Narration,(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )as `Credit`,(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )as `Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left join headstree as t8 on (t1.ChitGroupID=t8.NodeID) where `t1`.`BranchID` =" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and `t1`.`RootID` = 4 and t1.ChoosenDate <= '" + balayer.indiandateToMysqlDate(dateToConsolidated.Text) + "'";
            hfOtherItems.Value = MySqldsOtherItems.SelectCommand;
            gridOtherItems.DataBind();
            lblMaintainPost.Text = "post";
        }

        protected void aDrOtherItemsAmountcrDay_Click(object sender, EventArgs e)
        {
            gridOtherItems.FilterExpression = "";
            ClientScriptManager script = Page.ClientScript;
            gridOtherItems.FilterExpression = "[Debit] != '0.00'";
            gridOtherItems.Focus();
            lblMaintain.Text = "li_OtherItems";
            gridOtherItems.SettingsText.Title = "Other Items Debit Amount on :" + DateTime.Parse(dateToConsolidated.Text).ToString("dd/MM/yyyy");
            MySqldsOtherItems.ConnectionString = CommonClassFile.ConnectionString;
            MySqldsOtherItems.SelectCommand = @"select t1.ChoosenDate as `Date`,t8.Node as Head ,t3.Node as LedgerHead , t1.Narration,(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )as `Credit`,(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )as `Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left join headstree as t8 on (t1.ChitGroupID=t8.NodeID) where `t1`.`BranchID` =" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and `t1`.`RootID` = 4 and t1.ChoosenDate <= '" + balayer.indiandateToMysqlDate(dateToConsolidated.Text) + "'";
            hfOtherItems.Value = MySqldsOtherItems.SelectCommand;
            gridOtherItems.DataBind();
            lblMaintainPost.Text = "post";
        }

        protected void aCrOtherItemsAmounttoDay_Click(object sender, EventArgs e)
        {
            gridOtherItems.FilterExpression = "";
            ClientScriptManager script = Page.ClientScript;
            gridOtherItems.FilterExpression = "[Credit] != '0.00'";
            gridOtherItems.Focus();
            lblMaintain.Text = "li_OtherItems";
            gridOtherItems.SettingsText.Title = "Other Items Credit Amount on Particular Day:" + DateTime.Parse(dateToConsolidated.Text).ToString("dd/MM/yyyy");
            MySqldsOtherItems.ConnectionString = CommonClassFile.ConnectionString;
            MySqldsOtherItems.SelectCommand = @"select t1.ChoosenDate as `Date`,t8.Node as Head ,t3.Node as LedgerHead , t1.Narration,(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )as `Credit`,(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )as `Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left join headstree as t8 on (t1.ChitGroupID=t8.NodeID) where `t1`.`BranchID` =" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and `t1`.`RootID` = 4 and t1.ChoosenDate = '" + balayer.indiandateToMysqlDate(dateToConsolidated.Text) + "'";
            hfOtherItems.Value = MySqldsOtherItems.SelectCommand;
            gridOtherItems.DataBind();
            lblMaintainPost.Text = "post";
        }

        protected void aDrOtherItemsAmounttoDay_Click(object sender, EventArgs e)
        {
            gridOtherItems.FilterExpression = "";
            ClientScriptManager script = Page.ClientScript;
            gridOtherItems.FilterExpression = "[Debit] != '0.00'";
            gridOtherItems.Focus();
            lblMaintain.Text = "li_OtherItems";
            gridOtherItems.SettingsText.Title = "Other Items Debit Amount on Particular Day:" + DateTime.Parse(dateToConsolidated.Text).ToString("dd/MM/yyyy");
            MySqldsOtherItems.ConnectionString = CommonClassFile.ConnectionString;
            MySqldsOtherItems.SelectCommand = @"select t1.ChoosenDate as `Date`,t8.Node as Head ,t3.Node as LedgerHead , t1.Narration,(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )as `Credit`,(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )as `Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left join headstree as t8 on (t1.ChitGroupID=t8.NodeID) where `t1`.`BranchID` =" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and `t1`.`RootID` = 4 and t1.ChoosenDate = '" + balayer.indiandateToMysqlDate(dateToConsolidated.Text) + "'";
            hfOtherItems.Value = MySqldsOtherItems.SelectCommand;
            gridOtherItems.DataBind();
            lblMaintainPost.Text = "post";
        }

        protected void TotalOtherItemscr_Click(object sender, EventArgs e)
        {
            gridOtherItems.FilterExpression = "";
            ClientScriptManager script = Page.ClientScript;
            gridOtherItems.Focus();
            lblMaintain.Text = "li_OtherItems";
            gridOtherItems.SettingsText.Title = "Total Other Items Amount on :" + DateTime.Parse(dateToConsolidated.Text).ToString("dd/MM/yyyy");
            MySqldsOtherItems.ConnectionString = CommonClassFile.ConnectionString;
            MySqldsOtherItems.SelectCommand = @"select t1.ChoosenDate as `Date`,t8.Node as Head ,t3.Node as LedgerHead , t1.Narration,(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )as `Credit`,(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )as `Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left join headstree as t8 on (t1.ChitGroupID=t8.NodeID) where `t1`.`BranchID` =" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and `t1`.`RootID` = 4 and t1.ChoosenDate <= '" + balayer.indiandateToMysqlDate(dateToConsolidated.Text) + "'";
            hfOtherItems.Value = MySqldsOtherItems.SelectCommand;
            gridOtherItems.DataBind();
            lblMaintainPost.Text = "post";
        }

        protected void TotalOtherItemspr_Click(object sender, EventArgs e)
        {
            gridOtherItems.FilterExpression = "";
            ClientScriptManager script = Page.ClientScript;
            gridOtherItems.Focus();
            lblMaintain.Text = "li_OtherItems";
            gridOtherItems.SettingsText.Title = "Total Other Items Amount on :" + DateTime.Parse(dateToConsolidated.Text).ToString("dd/MM/yyyy");
            MySqldsOtherItems.ConnectionString = CommonClassFile.ConnectionString;
            MySqldsOtherItems.SelectCommand = @"select t1.ChoosenDate as `Date`,t8.Node as Head ,t3.Node as LedgerHead , t1.Narration,(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )as `Credit`,(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )as `Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left join headstree as t8 on (t1.ChitGroupID=t8.NodeID) where `t1`.`BranchID` =" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and `t1`.`RootID` = 4 and t1.ChoosenDate < '" + balayer.indiandateToMysqlDate(dateToConsolidated.Text) + "'";
            hfOtherItems.Value = MySqldsOtherItems.SelectCommand;
            gridOtherItems.DataBind();
            lblMaintainPost.Text = "post";
        }

        protected void TotalOtherItemsto_Click(object sender, EventArgs e)
        {
            gridOtherItems.FilterExpression = "";
            ClientScriptManager script = Page.ClientScript;
            gridOtherItems.Focus();
            lblMaintain.Text = "li_OtherItems";
            gridOtherItems.SettingsText.Title = "Total Other Items Amount on Particular Day:" + DateTime.Parse(dateToConsolidated.Text).AddDays(-1).ToString("dd/MM/yyyy");
            MySqldsOtherItems.ConnectionString = CommonClassFile.ConnectionString;
            MySqldsOtherItems.SelectCommand = @"select t1.ChoosenDate as `Date`,t8.Node as Head ,t3.Node as LedgerHead , t1.Narration,(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )as `Credit`,(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )as `Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left join headstree as t8 on (t1.ChitGroupID=t8.NodeID) where `t1`.`BranchID` =" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and `t1`.`RootID` = 4 and t1.ChoosenDate = '" + balayer.indiandateToMysqlDate(dateToConsolidated.Text) + "'";
            hfOtherItems.Value = MySqldsOtherItems.SelectCommand;
            gridOtherItems.DataBind();
            lblMaintainPost.Text = "post";
        }

        protected void aDrChitsAmountprDay_Click(object sender, EventArgs e)
        {
            gridChits.FilterExpression = "";
            ClientScriptManager script = Page.ClientScript;
            gridChits.FilterExpression = "[Debit] != '0.00'";
            gridChits.Focus();
            lblMaintain.Text = "li_Chits";
            gridChits.SettingsText.Title = "Chits Debit Amount on :" + DateTime.Parse(dateToConsolidated.Text).AddDays(-1).ToString("dd/MM/yyyy");

            MySqldsChits.SelectCommand = @"select t1.ChoosenDate as `Date`,t8.Node as LedgerHead ,t3.Node as Token ,t1.Narration,(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )as `Credit`,(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )as `Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left Join headstree as t8 on t1.ChitGroupID=t8.NodeID where `t1`.`BranchID` = " + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and `t1`.`RootID` = 5 and t1.ChoosenDate <'" + balayer.indiandateToMysqlDate(dateToConsolidated.Text) + "'";
            hfChits.Value = MySqldsChits.SelectCommand;
            gridChits.DataBind();
            lblMaintainPost.Text = "post";
        }

        protected void aCrChitsAmountprDay_Click(object sender, EventArgs e)
        {
            gridChits.FilterExpression = "";
            ClientScriptManager script = Page.ClientScript;
            gridChits.FilterExpression = "[Credit] != '0.00'";
            gridChits.Focus();
            lblMaintain.Text = "li_Chits";
            gridChits.SettingsText.Title = "Chits Credit Amount on :" + DateTime.Parse(dateToConsolidated.Text).AddDays(-1).ToString("dd/MM/yyyy");
            MySqldsChits.SelectCommand = @"select t1.ChoosenDate as `Date`,t8.Node as LedgerHead ,t3.Node as Token ,t1.Narration,(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )as `Credit`,(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )as `Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left Join headstree as t8 on t1.ChitGroupID=t8.NodeID where `t1`.`BranchID` = " + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and `t1`.`RootID` = 5 and t1.ChoosenDate <'" + balayer.indiandateToMysqlDate(dateToConsolidated.Text) + "'";
            hfChits.Value = MySqldsChits.SelectCommand;
            gridChits.DataBind();
            lblMaintainPost.Text = "post";
        }

        protected void aCrChitsAmountcrDay_Click(object sender, EventArgs e)
        {
            gridChits.FilterExpression = "";
            ClientScriptManager script = Page.ClientScript;
            gridChits.FilterExpression = "[Credit] != '0.00'";
            gridChits.Focus();
            lblMaintain.Text = "li_Chits";
            gridChits.SettingsText.Title = "Chits Credit Amount on :" + DateTime.Parse(dateToConsolidated.Text).ToString("dd/MM/yyyy");
            MySqldsChits.SelectCommand = @"select t1.ChoosenDate as `Date`,t8.Node as LedgerHead ,t3.Node as Token ,t1.Narration,(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )as `Credit`,(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )as `Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left Join headstree as t8 on t1.ChitGroupID=t8.NodeID where `t1`.`BranchID` = " + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and `t1`.`RootID` = 5 and t1.ChoosenDate <='" + balayer.indiandateToMysqlDate(dateToConsolidated.Text) + "'";
            hfChits.Value = MySqldsChits.SelectCommand;
            gridChits.DataBind();
            lblMaintainPost.Text = "post";
        }

        protected void aDrChitsAmountcrDay_Click(object sender, EventArgs e)
        {
            gridChits.FilterExpression = "";
            ClientScriptManager script = Page.ClientScript;
            gridChits.FilterExpression = "[Debit] != '0.00'";
            gridChits.Focus();
            lblMaintain.Text = "li_Chits";
            gridChits.SettingsText.Title = "Chits Debit Amount on :" + DateTime.Parse(dateToConsolidated.Text).ToString("dd/MM/yyyy");
            MySqldsChits.SelectCommand = @"select t1.ChoosenDate as `Date`,t8.Node as LedgerHead ,t3.Node as Token ,t1.Narration,(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )as `Credit`,(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )as `Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left Join headstree as t8 on t1.ChitGroupID=t8.NodeID where `t1`.`BranchID` = " + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and `t1`.`RootID` = 5 and t1.ChoosenDate <='" + balayer.indiandateToMysqlDate(dateToConsolidated.Text) + "'";
            hfChits.Value = MySqldsChits.SelectCommand;
            gridChits.DataBind();
            lblMaintainPost.Text = "post";
        }

        protected void aCrChitsAmounttoDay_Click(object sender, EventArgs e)
        {
            gridChits.FilterExpression = "";
            ClientScriptManager script = Page.ClientScript;
            gridChits.FilterExpression = "[Credit] != '0.00'";
            gridChits.Focus();
            lblMaintain.Text = "li_Chits";
            gridChits.SettingsText.Title = "Chits Credit Amount on Particular Day:" + DateTime.Parse(dateToConsolidated.Text).ToString("dd/MM/yyyy");
            MySqldsChits.SelectCommand = @"select t1.ChoosenDate as `Date`,t8.Node as LedgerHead ,t3.Node as Token ,t1.Narration,(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )as `Credit`,(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )as `Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left Join headstree as t8 on t1.ChitGroupID=t8.NodeID where `t1`.`BranchID` = " + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and `t1`.`RootID` = 5 and t1.ChoosenDate ='" + balayer.indiandateToMysqlDate(dateToConsolidated.Text) + "'";
            hfChits.Value = MySqldsChits.SelectCommand;
            gridChits.DataBind();
            lblMaintainPost.Text = "post";
        }

        protected void aDrChitstoDay_Click(object sender, EventArgs e)
        {
            gridChits.FilterExpression = "";
            ClientScriptManager script = Page.ClientScript;
            gridChits.FilterExpression = "[Debit] != '0.00'";
            gridChits.Focus();
            lblMaintain.Text = "li_Chits";
            gridChits.SettingsText.Title = "Chits Debit Amount on Particular Day:" + DateTime.Parse(dateToConsolidated.Text).ToString("dd/MM/yyyy");
            MySqldsChits.SelectCommand = @"select t1.ChoosenDate as `Date`,t8.Node as LedgerHead ,t3.Node as Token ,t1.Narration,(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )as `Credit`,(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )as `Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left Join headstree as t8 on t1.ChitGroupID=t8.NodeID where `t1`.`BranchID` = " + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and `t1`.`RootID` = 5 and t1.ChoosenDate ='" + balayer.indiandateToMysqlDate(dateToConsolidated.Text) + "'";
            hfChits.Value = MySqldsChits.SelectCommand;
            gridChits.DataBind();
            lblMaintainPost.Text = "post";
        }

        protected void TotalChitscr_Click(object sender, EventArgs e)
        {
            gridChits.FilterExpression = "";
            ClientScriptManager script = Page.ClientScript;
            gridChits.Focus();
            lblMaintain.Text = "li_Chits";
            gridChits.SettingsText.Title = "Total Chits Amount on :" + DateTime.Parse(dateToConsolidated.Text).ToString("dd/MM/yyyy");
            MySqldsChits.SelectCommand = @"select t1.ChoosenDate as `Date`,t8.Node as LedgerHead ,t3.Node as Token ,t1.Narration,(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )as `Credit`,(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )as `Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left Join headstree as t8 on t1.ChitGroupID=t8.NodeID where `t1`.`BranchID` = " + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and `t1`.`RootID` = 5 and t1.ChoosenDate <='" + balayer.indiandateToMysqlDate(dateToConsolidated.Text) + "'";
            hfChits.Value = MySqldsChits.SelectCommand;
            gridChits.DataBind();
            lblMaintainPost.Text = "post";
        }

        protected void TotalChitsprDay_Click(object sender, EventArgs e)
        {
            gridChits.FilterExpression = "";
            ClientScriptManager script = Page.ClientScript;
            gridChits.Focus();
            lblMaintain.Text = "li_Chits";
            gridChits.SettingsText.Title = "Total Chits Amount on :" + DateTime.Parse(dateToConsolidated.Text).AddDays(-1).ToString("dd/MM/yyyy");
            MySqldsChits.SelectCommand = @"select t1.ChoosenDate as `Date`,t8.Node as LedgerHead ,t3.Node as Token ,t1.Narration,(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )as `Credit`,(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )as `Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left Join headstree as t8 on t1.ChitGroupID=t8.NodeID where `t1`.`BranchID` = " + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and `t1`.`RootID` = 5 and t1.ChoosenDate <'" + balayer.indiandateToMysqlDate(dateToConsolidated.Text) + "'";
            hfChits.Value = MySqldsChits.SelectCommand;
            gridChits.DataBind();
            lblMaintainPost.Text = "post";
        }

        protected void TotalChitsto_Click(object sender, EventArgs e)
        {
            gridChits.FilterExpression = "";
            ClientScriptManager script = Page.ClientScript;
            gridChits.Focus();
            lblMaintain.Text = "li_Chits";
            gridChits.SettingsText.Title = "Total Chits Amount on Particular Day :" + DateTime.Parse(dateToConsolidated.Text).ToString("dd/MM/yyyy");
            MySqldsChits.SelectCommand = @"select t1.ChoosenDate as `Date`,t8.Node as LedgerHead ,t3.Node as Token ,t1.Narration,(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )as `Credit`,(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )as `Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left Join headstree as t8 on t1.ChitGroupID=t8.NodeID where `t1`.`BranchID` = " + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and `t1`.`RootID` = 5 and t1.ChoosenDate ='" + balayer.indiandateToMysqlDate(dateToConsolidated.Text) + "'";
            hfChits.Value = MySqldsChits.SelectCommand;
            gridChits.DataBind();
            lblMaintainPost.Text = "post";
        }

        protected void aDrForemanChitsAmountprDay_Click(object sender, EventArgs e)
        {
            gridForemanChits.FilterExpression = "";
            ClientScriptManager script = Page.ClientScript;
            gridForemanChits.FilterExpression = "[Debit] != '0.00'";
            gridForemanChits.Focus();
            lblMaintain.Text = "li_CompanyChits";
            gridForemanChits.SettingsText.Title = "Foreman Chits Debit Amount on :" + DateTime.Parse(dateToConsolidated.Text).AddDays(-1).ToString("dd/MM/yyyy");

            MySqldsForemanChits.ConnectionString = CommonClassFile.ConnectionString;
            MySqldsForemanChits.SelectCommand = @"select t1.ChoosenDate as `Date`,t8.Node as LedgerHead ,t3.Node as Token ,t1.Narration,(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )as `Credit`,(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )as `Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left Join headstree as t8 on (t1.ChitGroupID=t8.NodeID) where `t1`.`BranchID` = " + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and `t1`.`RootID` = 6 and t1.ChoosenDate <'" + balayer.indiandateToMysqlDate(dateToConsolidated.Text) + "';";
            hfForemanChits.Value = MySqldsForemanChits.SelectCommand;
            gridForemanChits.DataBind();
            lblMaintainPost.Text = "post";
        }

        protected void aCrForemanChitsAmountprDay_Click(object sender, EventArgs e)
        {
            gridForemanChits.FilterExpression = "";
            ClientScriptManager script = Page.ClientScript;
            gridForemanChits.FilterExpression = "[Credit] != '0.00'";
            gridForemanChits.Focus();
            lblMaintain.Text = "li_CompanyChits";
            gridForemanChits.SettingsText.Title = "Foreman Chits Credit Amount on :" + DateTime.Parse(dateToConsolidated.Text).AddDays(-1).ToString("dd/MM/yyyy");
            MySqldsForemanChits.ConnectionString = CommonClassFile.ConnectionString;
            MySqldsForemanChits.SelectCommand = @"select t1.ChoosenDate as `Date`,t8.Node as LedgerHead ,t3.Node as Token ,t1.Narration,(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )as `Credit`,(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )as `Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left Join headstree as t8 on (t1.ChitGroupID=t8.NodeID) where `t1`.`BranchID` = " + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and `t1`.`RootID` = 6 and t1.ChoosenDate <'" + balayer.indiandateToMysqlDate(dateToConsolidated.Text) + "';";
            hfForemanChits.Value = MySqldsForemanChits.SelectCommand;
            gridForemanChits.DataBind();
            lblMaintainPost.Text = "post";
        }

        protected void aCrForemanChitsAmountcrDay_Click(object sender, EventArgs e)
        {
            gridForemanChits.FilterExpression = "";
            ClientScriptManager script = Page.ClientScript;
            gridForemanChits.FilterExpression = "[Credit] != '0.00'";
            gridForemanChits.Focus();
            lblMaintain.Text = "li_CompanyChits";
            gridForemanChits.SettingsText.Title = "Foreman Chits Credit Amount on :" + DateTime.Parse(dateToConsolidated.Text).ToString("dd/MM/yyyy");
            MySqldsForemanChits.ConnectionString = CommonClassFile.ConnectionString;
            MySqldsForemanChits.SelectCommand = @"select t1.ChoosenDate as `Date`,t8.Node as LedgerHead ,t3.Node as Token ,t1.Narration,(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )as `Credit`,(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )as `Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left Join headstree as t8 on (t1.ChitGroupID=t8.NodeID) where `t1`.`BranchID` = " + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and `t1`.`RootID` = 6 and t1.ChoosenDate <='" + balayer.indiandateToMysqlDate(dateToConsolidated.Text) + "';";
            hfForemanChits.Value = MySqldsForemanChits.SelectCommand;
            gridForemanChits.DataBind();
            lblMaintainPost.Text = "post";
        }

        protected void aDrForemanChitsAmountcrDay_Click(object sender, EventArgs e)
        {
            gridForemanChits.FilterExpression = "";
            ClientScriptManager script = Page.ClientScript;
            gridForemanChits.FilterExpression = "[Debit] != '0.00'";
            gridForemanChits.Focus();
            lblMaintain.Text = "li_CompanyChits";
            gridForemanChits.SettingsText.Title = "Foreman Chits Debit Amount on :" + DateTime.Parse(dateToConsolidated.Text).ToString("dd/MM/yyyy");
            MySqldsForemanChits.ConnectionString = CommonClassFile.ConnectionString;
            MySqldsForemanChits.SelectCommand = @"select t1.ChoosenDate as `Date`,t8.Node as LedgerHead ,t3.Node as Token ,t1.Narration,(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )as `Credit`,(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )as `Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left Join headstree as t8 on (t1.ChitGroupID=t8.NodeID) where `t1`.`BranchID` = " + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and `t1`.`RootID` = 6 and t1.ChoosenDate <='" + balayer.indiandateToMysqlDate(dateToConsolidated.Text) + "';";
            hfForemanChits.Value = MySqldsForemanChits.SelectCommand;
            gridForemanChits.DataBind();
            lblMaintainPost.Text = "post";
        }

        protected void aCrForemanChitsAmounttoDay_Click(object sender, EventArgs e)
        {
            gridForemanChits.FilterExpression = "";
            ClientScriptManager script = Page.ClientScript;
            gridForemanChits.FilterExpression = "[Credit] != '0.00'";
            gridForemanChits.Focus();
            lblMaintain.Text = "li_CompanyChits";
            gridForemanChits.SettingsText.Title = "Foreman Chits Credit Amount on Particular Day:" + DateTime.Parse(dateToConsolidated.Text).ToString("dd/MM/yyyy");
            MySqldsForemanChits.ConnectionString = CommonClassFile.ConnectionString;
            MySqldsForemanChits.SelectCommand = @"select t1.ChoosenDate as `Date`,t8.Node as LedgerHead ,t3.Node as Token ,t1.Narration,(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )as `Credit`,(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )as `Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left Join headstree as t8 on (t1.ChitGroupID=t8.NodeID) where `t1`.`BranchID` = " + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and `t1`.`RootID` = 6 and t1.ChoosenDate ='" + balayer.indiandateToMysqlDate(dateToConsolidated.Text) + "';";
            hfForemanChits.Value = MySqldsForemanChits.SelectCommand;
            gridForemanChits.DataBind();
            lblMaintainPost.Text = "post";
        }

        protected void aDrForemanChitsAmounttoDay_Click(object sender, EventArgs e)
        {
            gridForemanChits.FilterExpression = "";
            ClientScriptManager script = Page.ClientScript;
            gridForemanChits.FilterExpression = "[Debit] != '0.00'";
            gridForemanChits.Focus();
            lblMaintain.Text = "li_CompanyChits";
            gridForemanChits.SettingsText.Title = "Foreman Chits Debit Amount on Particular Day:" + DateTime.Parse(dateToConsolidated.Text).ToString("dd/MM/yyyy");
            MySqldsForemanChits.ConnectionString = CommonClassFile.ConnectionString;
            MySqldsForemanChits.SelectCommand = @"select t1.ChoosenDate as `Date`,t8.Node as LedgerHead ,t3.Node as Token ,t1.Narration,(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )as `Credit`,(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )as `Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left Join headstree as t8 on (t1.ChitGroupID=t8.NodeID) where `t1`.`BranchID` = " + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and `t1`.`RootID` = 6 and t1.ChoosenDate ='" + balayer.indiandateToMysqlDate(dateToConsolidated.Text) + "';";
            hfForemanChits.Value = MySqldsForemanChits.SelectCommand;
            gridForemanChits.DataBind();
            lblMaintainPost.Text = "post";
        }

        protected void TotalForemanChitscr_Click(object sender, EventArgs e)
        {
            gridForemanChits.FilterExpression = "";
            ClientScriptManager script = Page.ClientScript;
            gridForemanChits.Focus();
            lblMaintain.Text = "li_CompanyChits";
            gridForemanChits.SettingsText.Title = "Total Foreman Chits Amount on :" + DateTime.Parse(dateToConsolidated.Text).ToString("dd/MM/yyyy");
            MySqldsForemanChits.ConnectionString = CommonClassFile.ConnectionString;
            MySqldsForemanChits.SelectCommand = @"select t1.ChoosenDate as `Date`,t8.Node as LedgerHead ,t3.Node as Token ,t1.Narration,(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )as `Credit`,(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )as `Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left Join headstree as t8 on (t1.ChitGroupID=t8.NodeID) where `t1`.`BranchID` = " + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and `t1`.`RootID` = 6 and t1.ChoosenDate <='" + balayer.indiandateToMysqlDate(dateToConsolidated.Text) + "';";
            hfForemanChits.Value = MySqldsForemanChits.SelectCommand;
            gridForemanChits.DataBind();
            lblMaintainPost.Text = "post";
        }

        protected void TotalForemanChitspr_Click(object sender, EventArgs e)
        {
            gridForemanChits.FilterExpression = "";
            ClientScriptManager script = Page.ClientScript;
            gridForemanChits.Focus();
            lblMaintain.Text = "li_CompanyChits";
            gridForemanChits.SettingsText.Title = "Total Foreman Chits Amount on :" + DateTime.Parse(dateToConsolidated.Text).AddDays(-1).ToString("dd/MM/yyyy");
            MySqldsForemanChits.ConnectionString = CommonClassFile.ConnectionString;
            MySqldsForemanChits.SelectCommand = @"select t1.ChoosenDate as `Date`,t8.Node as LedgerHead ,t3.Node as Token ,t1.Narration,(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )as `Credit`,(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )as `Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left Join headstree as t8 on (t1.ChitGroupID=t8.NodeID) where `t1`.`BranchID` = " + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and `t1`.`RootID` = 6 and t1.ChoosenDate <'" + balayer.indiandateToMysqlDate(dateToConsolidated.Text) + "';";
            hfForemanChits.Value = MySqldsForemanChits.SelectCommand;
            gridForemanChits.DataBind();
            lblMaintainPost.Text = "post";
        }

        protected void TotalForemanChitsto_Click(object sender, EventArgs e)
        {
            gridForemanChits.FilterExpression = "";
            ClientScriptManager script = Page.ClientScript;
            gridForemanChits.Focus();
            lblMaintain.Text = "li_CompanyChits";
            gridForemanChits.SettingsText.Title = "Total Foreman Chits Amount on Particular Day:" + DateTime.Parse(dateToConsolidated.Text).ToString("dd/MM/yyyy");
            MySqldsForemanChits.ConnectionString = CommonClassFile.ConnectionString;
            MySqldsForemanChits.SelectCommand = @"select t1.ChoosenDate as `Date`,t8.Node as LedgerHead ,t3.Node as Token ,t1.Narration,(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )as `Credit`,(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )as `Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left Join headstree as t8 on (t1.ChitGroupID=t8.NodeID) where `t1`.`BranchID` = " + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and `t1`.`RootID` = 6 and t1.ChoosenDate ='" + balayer.indiandateToMysqlDate(dateToConsolidated.Text) + "';";
            hfForemanChits.Value = MySqldsForemanChits.SelectCommand;
            gridForemanChits.DataBind();
            lblMaintainPost.Text = "post";
        }

        protected void aDrDecreeDebtorsAmountprDay_Click(object sender, EventArgs e)
        {
            gridDecreeDebtors.FilterExpression = "";
            ClientScriptManager script = Page.ClientScript;
            gridDecreeDebtors.FilterExpression = "[Debit] != '0.00'";
            gridDecreeDebtors.Focus();
            lblMaintain.Text = "li_DecreeDebtors";
            gridDecreeDebtors.SettingsText.Title = "Decree Debtors Debit Amount on :" + DateTime.Parse(dateToConsolidated.Text).AddDays(-1).ToString("dd/MM/yyyy");
            MySqldsDecreeDebtors.ConnectionString = CommonClassFile.ConnectionString;
            MySqldsDecreeDebtors.SelectCommand = @"SELECT  t1.ChoosenDate as `Date`,t3.Node as Head , t1.Narration,(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )as `Credit`,(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )as `Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left join headstree as t8 on (t1.ChitGroupID=t8.NodeID) where `t1`.`BranchID` = " + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and `t1`.`RootID` = 7 and t1.ChoosenDate <'" + balayer.indiandateToMysqlDate(dateToConsolidated.Text) + "';";
            hfDecreeDebtors.Value = MySqldsDecreeDebtors.SelectCommand;
            gridDecreeDebtors.DataBind();
            lblMaintainPost.Text = "post";
        }

        protected void aCrDecreeDebtorsAmountprDay_Click(object sender, EventArgs e)
        {
            gridDecreeDebtors.FilterExpression = "";
            ClientScriptManager script = Page.ClientScript;
            gridDecreeDebtors.FilterExpression = "[Credit] != '0.00'";
            gridDecreeDebtors.Focus();
            lblMaintain.Text = "li_DecreeDebtors";
            gridDecreeDebtors.SettingsText.Title = "Decree Debtors Credit Amount on :" + DateTime.Parse(dateToConsolidated.Text).AddDays(-1).ToString("dd/MM/yyyy");
            MySqldsDecreeDebtors.ConnectionString = CommonClassFile.ConnectionString;
            MySqldsDecreeDebtors.SelectCommand = @"SELECT  t1.ChoosenDate as `Date`,t3.Node as Head , t1.Narration,(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )as `Credit`,(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )as `Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left join headstree as t8 on (t1.ChitGroupID=t8.NodeID) where `t1`.`BranchID` = " + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and `t1`.`RootID` = 7 and t1.ChoosenDate <'" + balayer.indiandateToMysqlDate(dateToConsolidated.Text) + "';";
            hfDecreeDebtors.Value = MySqldsDecreeDebtors.SelectCommand;
            gridDecreeDebtors.DataBind();
            lblMaintainPost.Text = "post";
        }

        protected void aCrDecreeDebtorsAmountcrDay_Click(object sender, EventArgs e)
        {
            gridDecreeDebtors.FilterExpression = "";
            ClientScriptManager script = Page.ClientScript;
            gridDecreeDebtors.FilterExpression = "[Credit] != '0.00'";
            gridDecreeDebtors.Focus();
            lblMaintain.Text = "li_DecreeDebtors";
            gridDecreeDebtors.SettingsText.Title = "Decree Debtors Credit Amount on :" + DateTime.Parse(dateToConsolidated.Text).ToString("dd/MM/yyyy");
            MySqldsDecreeDebtors.ConnectionString = CommonClassFile.ConnectionString;
            MySqldsDecreeDebtors.SelectCommand = @"SELECT  t1.ChoosenDate as `Date`,t3.Node as Head , t1.Narration,(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )as `Credit`,(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )as `Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left join headstree as t8 on (t1.ChitGroupID=t8.NodeID) where `t1`.`BranchID` = " + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and `t1`.`RootID` = 7 and t1.ChoosenDate <='" + balayer.indiandateToMysqlDate(dateToConsolidated.Text) + "';";
            hfDecreeDebtors.Value = MySqldsDecreeDebtors.SelectCommand;
            gridDecreeDebtors.DataBind();
            lblMaintainPost.Text = "post";
        }

        protected void aDrDecreeDebtorsAmountcrDay_Click(object sender, EventArgs e)
        {
            gridDecreeDebtors.FilterExpression = "";
            ClientScriptManager script = Page.ClientScript;
            gridDecreeDebtors.FilterExpression = "[Debit] != '0.00'";
            gridDecreeDebtors.Focus();
            lblMaintain.Text = "li_DecreeDebtors";
            gridDecreeDebtors.SettingsText.Title = "Decree Debtors Debit Amount on :" + DateTime.Parse(dateToConsolidated.Text).ToString("dd/MM/yyyy");
            MySqldsDecreeDebtors.ConnectionString = CommonClassFile.ConnectionString;
            MySqldsDecreeDebtors.SelectCommand = @"SELECT  t1.ChoosenDate as `Date`,t3.Node as Head , t1.Narration,(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )as `Credit`,(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )as `Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left join headstree as t8 on (t1.ChitGroupID=t8.NodeID) where `t1`.`BranchID` = " + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and `t1`.`RootID` = 7 and t1.ChoosenDate <='" + balayer.indiandateToMysqlDate(dateToConsolidated.Text) + "';";
            hfDecreeDebtors.Value = MySqldsDecreeDebtors.SelectCommand;
            gridDecreeDebtors.DataBind();
            lblMaintainPost.Text = "post";
        }

        protected void aCrDecreeDebtorsAmounttoDay_Click(object sender, EventArgs e)
        {
            gridDecreeDebtors.FilterExpression = "";
            ClientScriptManager script = Page.ClientScript;
            gridDecreeDebtors.FilterExpression = "[Credit] != '0.00'";
            gridDecreeDebtors.Focus();
            lblMaintain.Text = "li_DecreeDebtors";
            gridDecreeDebtors.SettingsText.Title = "Decree Debtors Credit Amount on Particular Day:" + DateTime.Parse(dateToConsolidated.Text).ToString("dd/MM/yyyy");
            MySqldsDecreeDebtors.ConnectionString = CommonClassFile.ConnectionString;
            MySqldsDecreeDebtors.SelectCommand = @"SELECT  t1.ChoosenDate as `Date`,t3.Node as Head , t1.Narration,(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )as `Credit`,(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )as `Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left join headstree as t8 on (t1.ChitGroupID=t8.NodeID) where `t1`.`BranchID` = " + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and `t1`.`RootID` = 7 and t1.ChoosenDate ='" + balayer.indiandateToMysqlDate(dateToConsolidated.Text) + "';";
            hfDecreeDebtors.Value = MySqldsDecreeDebtors.SelectCommand;
            gridDecreeDebtors.DataBind();
            lblMaintainPost.Text = "post";
        }

        protected void aDrDecreeDebtorsAmounttoDay_Click(object sender, EventArgs e)
        {
            gridDecreeDebtors.FilterExpression = "";
            ClientScriptManager script = Page.ClientScript;
            gridDecreeDebtors.FilterExpression = "[Debit] != '0.00'";
            gridDecreeDebtors.Focus();
            lblMaintain.Text = "li_DecreeDebtors";
            gridDecreeDebtors.SettingsText.Title = "Decree Debtors Debit Amount on Particular Day :" + DateTime.Parse(dateToConsolidated.Text).ToString("dd/MM/yyyy");
            MySqldsDecreeDebtors.ConnectionString = CommonClassFile.ConnectionString;
            MySqldsDecreeDebtors.SelectCommand = @"SELECT  t1.ChoosenDate as `Date`,t3.Node as Head , t1.Narration,(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )as `Credit`,(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )as `Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left join headstree as t8 on (t1.ChitGroupID=t8.NodeID) where `t1`.`BranchID` = " + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and `t1`.`RootID` = 7 and t1.ChoosenDate ='" + balayer.indiandateToMysqlDate(dateToConsolidated.Text) + "';";
            hfDecreeDebtors.Value = MySqldsDecreeDebtors.SelectCommand;
            gridDecreeDebtors.DataBind();
            lblMaintainPost.Text = "post";
        }

        protected void TotalDecreeDebtorscr_Click(object sender, EventArgs e)
        {
            gridDecreeDebtors.FilterExpression = "";
            ClientScriptManager script = Page.ClientScript;
            gridDecreeDebtors.Focus();
            lblMaintain.Text = "li_DecreeDebtors";
            gridDecreeDebtors.SettingsText.Title = "Total Decree Debtors Amount on :" + DateTime.Parse(dateToConsolidated.Text).ToString("dd/MM/yyyy");
            MySqldsDecreeDebtors.ConnectionString = CommonClassFile.ConnectionString;
            MySqldsDecreeDebtors.SelectCommand = @"SELECT  t1.ChoosenDate as `Date`,t3.Node as Head , t1.Narration,(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )as `Credit`,(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )as `Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left join headstree as t8 on (t1.ChitGroupID=t8.NodeID) where `t1`.`BranchID` = " + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and `t1`.`RootID` = 7 and t1.ChoosenDate <='" + balayer.indiandateToMysqlDate(dateToConsolidated.Text) + "';";
            hfDecreeDebtors.Value = MySqldsDecreeDebtors.SelectCommand;
            gridDecreeDebtors.DataBind();
            lblMaintainPost.Text = "post";
        }

        protected void TotalDecreeDebtorspr_Click(object sender, EventArgs e)
        {
            gridDecreeDebtors.FilterExpression = "";
            ClientScriptManager script = Page.ClientScript;
            gridDecreeDebtors.Focus();
            lblMaintain.Text = "li_DecreeDebtors";
            gridDecreeDebtors.SettingsText.Title = "Total Decree Debtors Amount on :" + DateTime.Parse(dateToConsolidated.Text).AddDays(-1).ToString("dd/MM/yyyy");
            MySqldsDecreeDebtors.ConnectionString = CommonClassFile.ConnectionString;
            MySqldsDecreeDebtors.SelectCommand = @"SELECT  t1.ChoosenDate as `Date`,t3.Node as Head , t1.Narration,(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )as `Credit`,(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )as `Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left join headstree as t8 on (t1.ChitGroupID=t8.NodeID) where `t1`.`BranchID` = " + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and `t1`.`RootID` = 7 and t1.ChoosenDate <'" + balayer.indiandateToMysqlDate(dateToConsolidated.Text) + "';";
            hfDecreeDebtors.Value = MySqldsDecreeDebtors.SelectCommand;
            gridDecreeDebtors.DataBind();
            lblMaintainPost.Text = "post";
        }

        protected void TotalDecreeDebtorsto_Click(object sender, EventArgs e)
        {
            gridDecreeDebtors.FilterExpression = "";
            ClientScriptManager script = Page.ClientScript;
            gridDecreeDebtors.Focus();
            lblMaintain.Text = "li_DecreeDebtors";
            gridDecreeDebtors.SettingsText.Title = "Total Decree Debtors Amount on Particular Day :" + DateTime.Parse(dateToConsolidated.Text).ToString("dd/MM/yyyy");
            MySqldsDecreeDebtors.ConnectionString = CommonClassFile.ConnectionString;
            MySqldsDecreeDebtors.SelectCommand = @"SELECT  t1.ChoosenDate as `Date`,t3.Node as Head , t1.Narration,(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )as `Credit`,(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )as `Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left join headstree as t8 on (t1.ChitGroupID=t8.NodeID) where `t1`.`BranchID` = " + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and `t1`.`RootID` = 7 and t1.ChoosenDate ='" + balayer.indiandateToMysqlDate(dateToConsolidated.Text) + "';";
            hfDecreeDebtors.Value = MySqldsDecreeDebtors.SelectCommand;
            gridDecreeDebtors.DataBind();
            lblMaintainPost.Text = "post";
        }

        protected void aDrLoansAmountprDay_Click(object sender, EventArgs e)
        {
            gridLoans.FilterExpression = "";
            ClientScriptManager script = Page.ClientScript;
            gridLoans.FilterExpression = "[Debit] != '0.00'";
            gridLoans.Focus();
            lblMaintain.Text = "li_Loans";
            gridLoans.SettingsText.Title = "Loan Debit Amount on :" + DateTime.Parse(dateToConsolidated.Text).AddDays(-1).ToString("dd/MM/yyyy");

            MySqldsLoans.ConnectionString = CommonClassFile.ConnectionString;
            MySqldsLoans.SelectCommand = @"select t1.ChoosenDate as `Date`,t3.Node as `LedgerHead` ,t1.Narration,(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )as `Credit`,(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )as `Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left Join headstree as t8 on (t1.ChitGroupID=t8.NodeID) where `t1`.`BranchID` = " + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and `t1`.`RootID` = 8 and t1.ChoosenDate<'" + balayer.indiandateToMysqlDate(dateToConsolidated.Text) + "';";
            hfLoans.Value = MySqldsLoans.SelectCommand;
            gridLoans.DataBind();
            lblMaintainPost.Text = "post";
        }

        protected void aCrLoansAmountprDay_Click(object sender, EventArgs e)
        {
            gridLoans.FilterExpression = "";
            ClientScriptManager script = Page.ClientScript;
            gridLoans.FilterExpression = "[Credit] != '0.00'";
            gridLoans.Focus();
            lblMaintain.Text = "li_Loans";
            gridLoans.SettingsText.Title = "Loan Credit Amount on :" + DateTime.Parse(dateToConsolidated.Text).AddDays(-1).ToString("dd/MM/yyyy");
            MySqldsLoans.ConnectionString = CommonClassFile.ConnectionString;
            MySqldsLoans.SelectCommand = @"select t1.ChoosenDate as `Date`,t3.Node as `LedgerHead` ,t1.Narration,(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )as `Credit`,(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )as `Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left Join headstree as t8 on (t1.ChitGroupID=t8.NodeID) where `t1`.`BranchID` = " + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and `t1`.`RootID` = 8 and t1.ChoosenDate<'" + balayer.indiandateToMysqlDate(dateToConsolidated.Text) + "';";
            hfLoans.Value = MySqldsLoans.SelectCommand;
            gridLoans.DataBind();
            lblMaintainPost.Text = "post";
        }

        protected void aCrLoansAmountcrDay_Click(object sender, EventArgs e)
        {
            gridLoans.FilterExpression = "";
            ClientScriptManager script = Page.ClientScript;
            gridLoans.FilterExpression = "[Credit] != '0.00'";
            gridLoans.Focus();
            lblMaintain.Text = "li_Loans";
            gridLoans.SettingsText.Title = "Loan Credit Amount on :" + DateTime.Parse(dateToConsolidated.Text).ToString("dd/MM/yyyy");
            MySqldsLoans.ConnectionString = CommonClassFile.ConnectionString;
            MySqldsLoans.SelectCommand = @"select t1.ChoosenDate as `Date`,t3.Node as `LedgerHead` ,t1.Narration,(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )as `Credit`,(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )as `Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left Join headstree as t8 on (t1.ChitGroupID=t8.NodeID) where `t1`.`BranchID` = " + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and `t1`.`RootID` = 8 and t1.ChoosenDate<='" + balayer.indiandateToMysqlDate(dateToConsolidated.Text) + "';";
            hfLoans.Value = MySqldsLoans.SelectCommand;
            gridLoans.DataBind();
            lblMaintainPost.Text = "post";
        }

        protected void aDrLoansAmountcrDay_Click(object sender, EventArgs e)
        {
            gridLoans.FilterExpression = "";
            ClientScriptManager script = Page.ClientScript;
            gridLoans.FilterExpression = "[Debit] != '0.00'";
            gridLoans.Focus();
            lblMaintain.Text = "li_Loans";
            gridLoans.SettingsText.Title = "Loan Debit Amount on :" + DateTime.Parse(dateToConsolidated.Text).ToString("dd/MM/yyyy");
            MySqldsLoans.ConnectionString = CommonClassFile.ConnectionString;
            MySqldsLoans.SelectCommand = @"select t1.ChoosenDate as `Date`,t3.Node as `LedgerHead` ,t1.Narration,(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )as `Credit`,(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )as `Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left Join headstree as t8 on (t1.ChitGroupID=t8.NodeID) where `t1`.`BranchID` = " + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and `t1`.`RootID` = 8 and t1.ChoosenDate<='" + balayer.indiandateToMysqlDate(dateToConsolidated.Text) + "';";
            hfLoans.Value = MySqldsLoans.SelectCommand;
            gridLoans.DataBind();
            lblMaintainPost.Text = "post";
        }

        protected void aCrLoansAmounttoDay_Click(object sender, EventArgs e)
        {
            gridLoans.FilterExpression = "";
            ClientScriptManager script = Page.ClientScript;
            gridLoans.FilterExpression = "[Credit] != '0.00'";
            gridLoans.Focus();
            lblMaintain.Text = "li_Loans";
            gridLoans.SettingsText.Title = "Loan Credit Amount on Particular Day:" + DateTime.Parse(dateToConsolidated.Text).ToString("dd/MM/yyyy");
            MySqldsLoans.ConnectionString = CommonClassFile.ConnectionString;
            MySqldsLoans.SelectCommand = @"select t1.ChoosenDate as `Date`,t3.Node as `LedgerHead` ,t1.Narration,(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )as `Credit`,(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )as `Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left Join headstree as t8 on (t1.ChitGroupID=t8.NodeID) where `t1`.`BranchID` = " + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and `t1`.`RootID` = 8 and t1.ChoosenDate='" + balayer.indiandateToMysqlDate(dateToConsolidated.Text) + "';";
            hfLoans.Value = MySqldsLoans.SelectCommand;
            gridLoans.DataBind();
            lblMaintainPost.Text = "post";
        }

        protected void aDrLoansAmounttoDay_Click(object sender, EventArgs e)
        {
            gridLoans.FilterExpression = "";
            ClientScriptManager script = Page.ClientScript;
            gridLoans.FilterExpression = "[Debit] != '0.00'";
            gridLoans.Focus();
            lblMaintain.Text = "li_Loans";
            gridLoans.SettingsText.Title = "Loan Debit Amount on Particular Day:" + DateTime.Parse(dateToConsolidated.Text).ToString("dd/MM/yyyy");
            MySqldsLoans.ConnectionString = CommonClassFile.ConnectionString;
            MySqldsLoans.SelectCommand = @"select t1.ChoosenDate as `Date`,t3.Node as `LedgerHead` ,t1.Narration,(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )as `Credit`,(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )as `Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left Join headstree as t8 on (t1.ChitGroupID=t8.NodeID) where `t1`.`BranchID` = " + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and `t1`.`RootID` = 8 and t1.ChoosenDate='" + balayer.indiandateToMysqlDate(dateToConsolidated.Text) + "';";
            hfLoans.Value = MySqldsLoans.SelectCommand;
            gridLoans.DataBind();
            lblMaintainPost.Text = "post";
        }

        protected void TotalLoanscr_Click(object sender, EventArgs e)
        {
            gridLoans.FilterExpression = "";
            ClientScriptManager script = Page.ClientScript;
            gridLoans.Focus();
            lblMaintain.Text = "li_Loans";
            gridLoans.SettingsText.Title = "Total Loan Amount on :" + DateTime.Parse(dateToConsolidated.Text).ToString("dd/MM/yyyy");
            MySqldsLoans.ConnectionString = CommonClassFile.ConnectionString;
            MySqldsLoans.SelectCommand = @"select t1.ChoosenDate as `Date`,t3.Node as `LedgerHead` ,t1.Narration,(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )as `Credit`,(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )as `Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left Join headstree as t8 on (t1.ChitGroupID=t8.NodeID) where `t1`.`BranchID` = " + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and `t1`.`RootID` = 8 and t1.ChoosenDate<='" + balayer.indiandateToMysqlDate(dateToConsolidated.Text) + "';";
            hfLoans.Value = MySqldsLoans.SelectCommand;
            gridLoans.DataBind();
            lblMaintainPost.Text = "post";
        }

        protected void TotalLoanspr_Click(object sender, EventArgs e)
        {
            gridLoans.FilterExpression = "";
            ClientScriptManager script = Page.ClientScript;
            gridLoans.Focus();
            lblMaintain.Text = "li_Loans";
            gridLoans.SettingsText.Title = "Total Loan Amount on :" + DateTime.Parse(dateToConsolidated.Text).AddDays(-1).ToString("dd/MM/yyyy");
            MySqldsLoans.ConnectionString = CommonClassFile.ConnectionString;
            MySqldsLoans.SelectCommand = @"select t1.ChoosenDate as `Date`,t3.Node as `LedgerHead` ,t1.Narration,(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )as `Credit`,(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )as `Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left Join headstree as t8 on (t1.ChitGroupID=t8.NodeID) where `t1`.`BranchID` = " + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and `t1`.`RootID` = 8 and t1.ChoosenDate<'" + balayer.indiandateToMysqlDate(dateToConsolidated.Text) + "';";
            hfLoans.Value = MySqldsLoans.SelectCommand;
            gridLoans.DataBind();
            lblMaintainPost.Text = "post";
        }

        protected void TotalLoansto_Click(object sender, EventArgs e)
        {
            gridLoans.FilterExpression = "";
            ClientScriptManager script = Page.ClientScript;
            gridLoans.Focus();
            lblMaintain.Text = "li_Loans";
            gridLoans.SettingsText.Title = "Total Loans Amount on Particular Day :" + DateTime.Parse(dateToConsolidated.Text).ToString("dd/MM/yyyy");
            MySqldsLoans.ConnectionString = CommonClassFile.ConnectionString;
            MySqldsLoans.SelectCommand = @"select t1.ChoosenDate as `Date`,t3.Node as `LedgerHead` ,t1.Narration,(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )as `Credit`,(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )as `Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left Join headstree as t8 on (t1.ChitGroupID=t8.NodeID) where `t1`.`BranchID` = " + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and `t1`.`RootID` = 8 and t1.ChoosenDate='" + balayer.indiandateToMysqlDate(dateToConsolidated.Text) + "';";
            hfLoans.Value = MySqldsLoans.SelectCommand;
            gridLoans.DataBind();
            lblMaintainPost.Text = "post";
        }

        protected void aDrAdvancesAmountprDay_Click(object sender, EventArgs e)
        {
            gridSundriesAndAdvances.FilterExpression = "";
            ClientScriptManager script = Page.ClientScript;
            gridSundriesAndAdvances.FilterExpression = "[Debit] != '0.00'";
            gridSundriesAndAdvances.Focus();
            lblMaintain.Text = "li_Advances";
            gridSundriesAndAdvances.SettingsText.Title = "Advances Debit Amount on :" + DateTime.Parse(dateToConsolidated.Text).AddDays(-1).ToString("dd/MM/yyyy");
            MySqldsSundriesAndAdvances.ConnectionString = CommonClassFile.ConnectionString;
            MySqldsSundriesAndAdvances.SelectCommand = @"select t1.ChoosenDate as `Date`,t3.Node as `LedgerHead` ,t1.Narration,(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )as `Credit`,(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )as `Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left Join headstree as t8 on (t1.ChitGroupID=t8.NodeID) where `t1`.`BranchID` = " + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and `t1`.`RootID` = 9 and t1.ChoosenDate <'" + balayer.indiandateToMysqlDate(dateToConsolidated.Text) + "';";
            hfAdvances.Value = MySqldsSundriesAndAdvances.SelectCommand;
            gridSundriesAndAdvances.DataBind();
            lblMaintainPost.Text = "post";
        }

        protected void aCrAdvancesAmountprDay_Click(object sender, EventArgs e)
        {
            gridSundriesAndAdvances.FilterExpression = "";
            ClientScriptManager script = Page.ClientScript;
            gridSundriesAndAdvances.FilterExpression = "[Credit] != '0.00'";
            gridSundriesAndAdvances.Focus();
            lblMaintain.Text = "li_Advances";
            gridSundriesAndAdvances.SettingsText.Title = "Advances Credit Amount on :" + DateTime.Parse(dateToConsolidated.Text).AddDays(-1).ToString("dd/MM/yyyy");
            MySqldsSundriesAndAdvances.ConnectionString = CommonClassFile.ConnectionString;
            MySqldsSundriesAndAdvances.SelectCommand = @"select t1.ChoosenDate as `Date`,t3.Node as `LedgerHead` ,t1.Narration,(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )as `Credit`,(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )as `Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left Join headstree as t8 on (t1.ChitGroupID=t8.NodeID) where `t1`.`BranchID` = " + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and `t1`.`RootID` = 9 and t1.ChoosenDate <'" + balayer.indiandateToMysqlDate(dateToConsolidated.Text) + "';";
            hfAdvances.Value = MySqldsSundriesAndAdvances.SelectCommand;
            gridSundriesAndAdvances.DataBind();
            lblMaintainPost.Text = "post";
        }

        protected void aCrAdvancesAmountcrDay_Click(object sender, EventArgs e)
        {
            gridSundriesAndAdvances.FilterExpression = "";
            ClientScriptManager script = Page.ClientScript;
            gridSundriesAndAdvances.FilterExpression = "[Credit] != '0.00'";
            gridSundriesAndAdvances.Focus();
            lblMaintain.Text = "li_Advances";
            gridSundriesAndAdvances.SettingsText.Title = "Advances Credit Amount on :" + DateTime.Parse(dateToConsolidated.Text).ToString("dd/MM/yyyy");
            MySqldsSundriesAndAdvances.ConnectionString = CommonClassFile.ConnectionString;
            MySqldsSundriesAndAdvances.SelectCommand = @"select t1.ChoosenDate as `Date`,t3.Node as `LedgerHead` ,t1.Narration,(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )as `Credit`,(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )as `Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left Join headstree as t8 on (t1.ChitGroupID=t8.NodeID) where `t1`.`BranchID` = " + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and `t1`.`RootID` = 9 and t1.ChoosenDate <='" + balayer.indiandateToMysqlDate(dateToConsolidated.Text) + "';";
            hfAdvances.Value = MySqldsSundriesAndAdvances.SelectCommand;
            gridSundriesAndAdvances.DataBind();
            lblMaintainPost.Text = "post";
        }

        protected void aDrAdvancesAmountcrDay_Click(object sender, EventArgs e)
        {
            gridSundriesAndAdvances.FilterExpression = "";
            ClientScriptManager script = Page.ClientScript;
            gridSundriesAndAdvances.FilterExpression = "[Debit] != '0.00'";
            gridSundriesAndAdvances.Focus();
            lblMaintain.Text = "li_Advances";
            gridSundriesAndAdvances.SettingsText.Title = "Advances Debit Amount on :" + DateTime.Parse(dateToConsolidated.Text).ToString("dd/MM/yyyy");
            MySqldsSundriesAndAdvances.ConnectionString = CommonClassFile.ConnectionString;
            MySqldsSundriesAndAdvances.SelectCommand = @"select t1.ChoosenDate as `Date`,t3.Node as `LedgerHead` ,t1.Narration,(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )as `Credit`,(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )as `Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left Join headstree as t8 on (t1.ChitGroupID=t8.NodeID) where `t1`.`BranchID` = " + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and `t1`.`RootID` = 9 and t1.ChoosenDate <='" + balayer.indiandateToMysqlDate(dateToConsolidated.Text) + "';";
            hfAdvances.Value = MySqldsSundriesAndAdvances.SelectCommand;
            gridSundriesAndAdvances.DataBind();
            lblMaintainPost.Text = "post";
        }

        protected void aCrAdvancesAmounttoDay_Click(object sender, EventArgs e)
        {
            gridSundriesAndAdvances.FilterExpression = "";
            ClientScriptManager script = Page.ClientScript;
            gridSundriesAndAdvances.FilterExpression = "[Credit] != '0.00'";
            gridSundriesAndAdvances.Focus();
            lblMaintain.Text = "li_Advances";
            gridSundriesAndAdvances.SettingsText.Title = "Advances Credit Amount on Particular Day:" + DateTime.Parse(dateToConsolidated.Text).ToString("dd/MM/yyyy");
            MySqldsSundriesAndAdvances.ConnectionString = CommonClassFile.ConnectionString;
            MySqldsSundriesAndAdvances.SelectCommand = @"select t1.ChoosenDate as `Date`,t3.Node as `LedgerHead` ,t1.Narration,(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )as `Credit`,(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )as `Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left Join headstree as t8 on (t1.ChitGroupID=t8.NodeID) where `t1`.`BranchID` = " + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and `t1`.`RootID` = 9 and t1.ChoosenDate ='" + balayer.indiandateToMysqlDate(dateToConsolidated.Text) + "';";
            hfAdvances.Value = MySqldsSundriesAndAdvances.SelectCommand;
            gridSundriesAndAdvances.DataBind();
            lblMaintainPost.Text = "post";
        }

        protected void aDrAdvancesAmounttoDay_Click(object sender, EventArgs e)
        {
            gridSundriesAndAdvances.FilterExpression = "";
            ClientScriptManager script = Page.ClientScript;
            gridSundriesAndAdvances.FilterExpression = "[Debit] != '0.00'";
            gridSundriesAndAdvances.Focus();
            lblMaintain.Text = "li_Advances";
            gridSundriesAndAdvances.SettingsText.Title = "Advances Debit Amount on Particular Day :" + DateTime.Parse(dateToConsolidated.Text).ToString("dd/MM/yyyy");
            MySqldsSundriesAndAdvances.ConnectionString = CommonClassFile.ConnectionString;
            MySqldsSundriesAndAdvances.SelectCommand = @"select t1.ChoosenDate as `Date`,t3.Node as `LedgerHead` ,t1.Narration,(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )as `Credit`,(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )as `Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left Join headstree as t8 on (t1.ChitGroupID=t8.NodeID) where `t1`.`BranchID` = " + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and `t1`.`RootID` = 9 and t1.ChoosenDate ='" + balayer.indiandateToMysqlDate(dateToConsolidated.Text) + "';";
            hfAdvances.Value = MySqldsSundriesAndAdvances.SelectCommand;
            gridSundriesAndAdvances.DataBind();
            lblMaintainPost.Text = "post";
        }

        protected void TotalAdvancescr_Click(object sender, EventArgs e)
        {
            gridSundriesAndAdvances.FilterExpression = "";
            ClientScriptManager script = Page.ClientScript;
            gridSundriesAndAdvances.Focus();
            lblMaintain.Text = "li_Advances";
            gridSundriesAndAdvances.SettingsText.Title = "Total Advances Amount on :" + DateTime.Parse(dateToConsolidated.Text).ToString("dd/MM/yyyy");
            MySqldsSundriesAndAdvances.ConnectionString = CommonClassFile.ConnectionString;
            MySqldsSundriesAndAdvances.SelectCommand = @"select t1.ChoosenDate as `Date`,t3.Node as `LedgerHead` ,t1.Narration,(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )as `Credit`,(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )as `Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left Join headstree as t8 on (t1.ChitGroupID=t8.NodeID) where `t1`.`BranchID` = " + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and `t1`.`RootID` = 9 and t1.ChoosenDate <='" + balayer.indiandateToMysqlDate(dateToConsolidated.Text) + "';";
            hfAdvances.Value = MySqldsSundriesAndAdvances.SelectCommand;
            gridSundriesAndAdvances.DataBind();
            lblMaintainPost.Text = "post";
        }

        protected void TotalAdvancespr_Click(object sender, EventArgs e)
        {
            gridSundriesAndAdvances.FilterExpression = "";
            ClientScriptManager script = Page.ClientScript;
            gridSundriesAndAdvances.Focus();
            lblMaintain.Text = "li_Advances";
            gridSundriesAndAdvances.SettingsText.Title = "Total Advances Amount on :" + DateTime.Parse(dateToConsolidated.Text).AddDays(-1).ToString("dd/MM/yyyy");
            MySqldsSundriesAndAdvances.ConnectionString = CommonClassFile.ConnectionString;
            MySqldsSundriesAndAdvances.SelectCommand = @"select t1.ChoosenDate as `Date`,t3.Node as `LedgerHead` ,t1.Narration,(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )as `Credit`,(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )as `Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left Join headstree as t8 on (t1.ChitGroupID=t8.NodeID) where `t1`.`BranchID` = " + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and `t1`.`RootID` = 9 and t1.ChoosenDate <'" + balayer.indiandateToMysqlDate(dateToConsolidated.Text) + "';";
            hfAdvances.Value = MySqldsSundriesAndAdvances.SelectCommand;
            gridSundriesAndAdvances.DataBind();
            lblMaintainPost.Text = "post";
        }

        protected void TotalAdvancesto_Click(object sender, EventArgs e)
        {
            gridSundriesAndAdvances.FilterExpression = "";
            ClientScriptManager script = Page.ClientScript;
            gridSundriesAndAdvances.Focus();
            lblMaintain.Text = "li_Advances";
            gridSundriesAndAdvances.SettingsText.Title = "Total Advances Amount on Particular Day :" + DateTime.Parse(dateToConsolidated.Text).ToString("dd/MM/yyyy");
            MySqldsSundriesAndAdvances.ConnectionString = CommonClassFile.ConnectionString;
            MySqldsSundriesAndAdvances.SelectCommand = @"select t1.ChoosenDate as `Date`,t3.Node as `LedgerHead` ,t1.Narration,(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )as `Credit`,(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )as `Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left Join headstree as t8 on (t1.ChitGroupID=t8.NodeID) where `t1`.`BranchID` = " + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and `t1`.`RootID` = 9 and t1.ChoosenDate ='" + balayer.indiandateToMysqlDate(dateToConsolidated.Text) + "';";
            hfAdvances.Value = MySqldsSundriesAndAdvances.SelectCommand;
            gridSundriesAndAdvances.DataBind();
            lblMaintainPost.Text = "post";
        }

        protected void aDrStampsAmountprDay_Click(object sender, EventArgs e)
        {
            gridStamps.FilterExpression = "";
            ClientScriptManager script = Page.ClientScript;
            gridStamps.FilterExpression = "[Debit] != '0.00'";
            gridStamps.Focus();
            lblMaintain.Text = "li_Stamps";
            gridStamps.SettingsText.Title = "Stamps Debit Amount on :" + DateTime.Parse(dateToConsolidated.Text).AddDays(-1).ToString("dd/MM/yyyy");
            MySqldsStamps.ConnectionString = CommonClassFile.ConnectionString;
            MySqldsStamps.SelectCommand = @"SELECT distinct(v2.TransactionKey),ht2.Node as Head,ht1.Node as `LedgerHead`,v2.ChoosenDate as `Date`,v2.Narration,(case when v2.Voucher_Type='D' then v2.Amount else 0.00 end) as Credit, (case when v2.Voucher_Type='C' then v2.Amount else 0.00 end) as Debit FROM `svcf`.`voucher` as v1 left join `svcf`.`voucher` as v2 on (v1.DualTransactionKey=v2.DualTransactionKey )  join headstree as ht2 on v1.Head_Id=ht2.NodeID join headstree as ht1 on (v2.Head_Id=ht1.NodeID ) where v1.RootID=10 and v2.RootID<>10 and v1.BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and v2.BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and v2.ChoosenDate<'" + balayer.indiandateToMysqlDate(dateToConsolidated.Text) + "' and v1.ChoosenDate<'" + balayer.indiandateToMysqlDate(dateToConsolidated.Text) + "'";
            hfStamps.Value = MySqldsStamps.SelectCommand;
            gridStamps.DataBind();
            lblMaintainPost.Text = "post";
        }

        protected void aCrStampsAmountprDay_Click(object sender, EventArgs e)
        {
            gridStamps.FilterExpression = "";
            ClientScriptManager script = Page.ClientScript;
            gridStamps.FilterExpression = "[Credit] != '0.00'";
            gridStamps.Focus();
            lblMaintain.Text = "li_Stamps";
            gridStamps.SettingsText.Title = "Stamps Credit Amount on :" + DateTime.Parse(dateToConsolidated.Text).AddDays(-1).ToString("dd/MM/yyyy");
            MySqldsStamps.ConnectionString = CommonClassFile.ConnectionString;
            MySqldsStamps.SelectCommand = @"SELECT distinct(v2.TransactionKey),ht2.Node as Head,ht1.Node as `LedgerHead`,v2.ChoosenDate as `Date`,v2.Narration,(case when v2.Voucher_Type='D' then v2.Amount else 0.00 end) as Credit, (case when v2.Voucher_Type='C' then v2.Amount else 0.00 end) as Debit FROM `svcf`.`voucher` as v1 left join `svcf`.`voucher` as v2 on (v1.DualTransactionKey=v2.DualTransactionKey )  join headstree as ht2 on v1.Head_Id=ht2.NodeID join headstree as ht1 on (v2.Head_Id=ht1.NodeID ) where v1.RootID=10 and v2.RootID<>10 and v1.BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and v2.BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and v2.ChoosenDate<'" + balayer.indiandateToMysqlDate(dateToConsolidated.Text) + "' and v1.ChoosenDate<'" + balayer.indiandateToMysqlDate(dateToConsolidated.Text) + "'";
            hfStamps.Value = MySqldsStamps.SelectCommand;
            gridStamps.DataBind();
            lblMaintainPost.Text = "post";
        }

        protected void aCrStampsAmountcrDay_Click(object sender, EventArgs e)
        {
            gridStamps.FilterExpression = "";
            ClientScriptManager script = Page.ClientScript;
            gridStamps.FilterExpression = "[Credit] != '0.00'";
            gridStamps.Focus();
            lblMaintain.Text = "li_Stamps";
            gridStamps.SettingsText.Title = "Stamps Credit Amount on :" + DateTime.Parse(dateToConsolidated.Text).ToString("dd/MM/yyyy");
            MySqldsStamps.ConnectionString = CommonClassFile.ConnectionString;
            MySqldsStamps.SelectCommand = @"SELECT distinct(v2.TransactionKey),ht2.Node as Head,ht1.Node as `LedgerHead`,v2.ChoosenDate as `Date`,v2.Narration,(case when v2.Voucher_Type='D' then v2.Amount else 0.00 end) as Credit, (case when v2.Voucher_Type='C' then v2.Amount else 0.00 end) as Debit FROM `svcf`.`voucher` as v1 left join `svcf`.`voucher` as v2 on (v1.DualTransactionKey=v2.DualTransactionKey )  join headstree as ht2 on v1.Head_Id=ht2.NodeID join headstree as ht1 on (v2.Head_Id=ht1.NodeID ) where v1.RootID=10 and v2.RootID<>10 and v1.BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and v2.BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and v2.ChoosenDate<='" + balayer.indiandateToMysqlDate(dateToConsolidated.Text) + "' and v1.ChoosenDate<='" + balayer.indiandateToMysqlDate(dateToConsolidated.Text) + "'";
            hfStamps.Value = MySqldsStamps.SelectCommand;
            gridStamps.DataBind();
            lblMaintainPost.Text = "post";
        }

        protected void aDrStampsAmountcrDay_Click(object sender, EventArgs e)
        {
            gridStamps.FilterExpression = "";
            ClientScriptManager script = Page.ClientScript;
            gridStamps.FilterExpression = "[Debit] != '0.00'";
            gridStamps.Focus();
            lblMaintain.Text = "li_Stamps";
            gridStamps.SettingsText.Title = "Stamps Debit Amount on :" + DateTime.Parse(dateToConsolidated.Text).ToString("dd/MM/yyyy");
            MySqldsStamps.ConnectionString = CommonClassFile.ConnectionString;
            MySqldsStamps.SelectCommand = @"SELECT distinct(v2.TransactionKey),ht2.Node as Head,ht1.Node as `LedgerHead`,v2.ChoosenDate as `Date`,v2.Narration,(case when v2.Voucher_Type='D' then v2.Amount else 0.00 end) as Credit, (case when v2.Voucher_Type='C' then v2.Amount else 0.00 end) as Debit FROM `svcf`.`voucher` as v1 left join `svcf`.`voucher` as v2 on (v1.DualTransactionKey=v2.DualTransactionKey )  join headstree as ht2 on v1.Head_Id=ht2.NodeID join headstree as ht1 on (v2.Head_Id=ht1.NodeID ) where v1.RootID=10 and v2.RootID<>10 and v1.BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and v2.BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and v2.ChoosenDate<='" + balayer.indiandateToMysqlDate(dateToConsolidated.Text) + "' and v1.ChoosenDate<='" + balayer.indiandateToMysqlDate(dateToConsolidated.Text) + "'";
            hfStamps.Value = MySqldsStamps.SelectCommand;
            gridStamps.DataBind();
            lblMaintainPost.Text = "post";
        }

        protected void aCrStampsAmounttoDay_Click(object sender, EventArgs e)
        {
            gridStamps.FilterExpression = "";
            ClientScriptManager script = Page.ClientScript;
            gridStamps.FilterExpression = "[Credit] != '0.00'";
            gridStamps.Focus();
            lblMaintain.Text = "li_Stamps";
            gridStamps.SettingsText.Title = "Stamps Credit Amount on Particular Day:" + DateTime.Parse(dateToConsolidated.Text).ToString("dd/MM/yyyy");
            MySqldsStamps.ConnectionString = CommonClassFile.ConnectionString;
            MySqldsStamps.SelectCommand = @"SELECT distinct(v2.TransactionKey),ht2.Node as Head,ht1.Node as `LedgerHead`,v2.ChoosenDate as `Date`,v2.Narration,(case when v2.Voucher_Type='D' then v2.Amount else 0.00 end) as Credit, (case when v2.Voucher_Type='C' then v2.Amount else 0.00 end) as Debit FROM `svcf`.`voucher` as v1 left join `svcf`.`voucher` as v2 on (v1.DualTransactionKey=v2.DualTransactionKey )  join headstree as ht2 on v1.Head_Id=ht2.NodeID join headstree as ht1 on (v2.Head_Id=ht1.NodeID ) where v1.RootID=10 and v2.RootID<>10 and v1.BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and v2.BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and v2.ChoosenDate<='" + balayer.indiandateToMysqlDate(dateToConsolidated.Text) + "' and v1.ChoosenDate='" + balayer.indiandateToMysqlDate(dateToConsolidated.Text) + "'";
            hfStamps.Value = MySqldsStamps.SelectCommand;
            gridStamps.DataBind();
            lblMaintainPost.Text = "post";
        }

        protected void aDrStampsAmounttoDay_Click(object sender, EventArgs e)
        {
            gridStamps.FilterExpression = "";
            ClientScriptManager script = Page.ClientScript;
            gridStamps.FilterExpression = "[Debit] != '0.00'";
            gridStamps.Focus();
            lblMaintain.Text = "li_Stamps";
            gridStamps.SettingsText.Title = "Stamps Debit Amount on Particular Day:" + DateTime.Parse(dateToConsolidated.Text).ToString("dd/MM/yyyy");
            MySqldsStamps.ConnectionString = CommonClassFile.ConnectionString;
            MySqldsStamps.SelectCommand = @"SELECT distinct(v2.TransactionKey),ht2.Node as Head,ht1.Node as `LedgerHead`,v2.ChoosenDate as `Date`,v2.Narration,(case when v2.Voucher_Type='D' then v2.Amount else 0.00 end) as Credit, (case when v2.Voucher_Type='C' then v2.Amount else 0.00 end) as Debit FROM `svcf`.`voucher` as v1 left join `svcf`.`voucher` as v2 on (v1.DualTransactionKey=v2.DualTransactionKey )  join headstree as ht2 on v1.Head_Id=ht2.NodeID join headstree as ht1 on (v2.Head_Id=ht1.NodeID ) where v1.RootID=10 and v2.RootID<>10 and v1.BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and v2.BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and v2.ChoosenDate<='" + balayer.indiandateToMysqlDate(dateToConsolidated.Text) + "' and v1.ChoosenDate='" + balayer.indiandateToMysqlDate(dateToConsolidated.Text) + "'";
            hfStamps.Value = MySqldsStamps.SelectCommand;
            gridStamps.DataBind();
            lblMaintainPost.Text = "post";
        }

        protected void TotalStampscr_Click(object sender, EventArgs e)
        {
            gridStamps.FilterExpression = "";
            ClientScriptManager script = Page.ClientScript;
            gridStamps.Focus();
            lblMaintain.Text = "li_Stamps";
            gridStamps.SettingsText.Title = "Total Stamps Amount on :" + DateTime.Parse(dateToConsolidated.Text).ToString("dd/MM/yyyy");
            MySqldsStamps.ConnectionString = CommonClassFile.ConnectionString;
            MySqldsStamps.SelectCommand = @"SELECT distinct(v2.TransactionKey),ht2.Node as Head,ht1.Node as `LedgerHead`,v2.ChoosenDate as `Date`,v2.Narration,(case when v2.Voucher_Type='D' then v2.Amount else 0.00 end) as Credit, (case when v2.Voucher_Type='C' then v2.Amount else 0.00 end) as Debit FROM `svcf`.`voucher` as v1 left join `svcf`.`voucher` as v2 on (v1.DualTransactionKey=v2.DualTransactionKey )  join headstree as ht2 on v1.Head_Id=ht2.NodeID join headstree as ht1 on (v2.Head_Id=ht1.NodeID ) where v1.RootID=10 and v2.RootID<>10 and v1.BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and v2.BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and v2.ChoosenDate<='" + balayer.indiandateToMysqlDate(dateToConsolidated.Text) + "' and v1.ChoosenDate<='" + balayer.indiandateToMysqlDate(dateToConsolidated.Text) + "'";
            hfStamps.Value = MySqldsStamps.SelectCommand;
            gridStamps.DataBind();
            lblMaintainPost.Text = "post";
        }

        protected void TotalStampspr_Click(object sender, EventArgs e)
        {
            gridStamps.FilterExpression = "";
            ClientScriptManager script = Page.ClientScript;
            gridStamps.Focus();
            lblMaintain.Text = "li_Stamps";
            gridStamps.SettingsText.Title = "Total Stamps Amount on :" + DateTime.Parse(dateToConsolidated.Text).AddDays(-1).ToString("dd/MM/yyyy");
            MySqldsStamps.ConnectionString = CommonClassFile.ConnectionString;
            MySqldsStamps.SelectCommand = @"SELECT distinct(v2.TransactionKey),ht2.Node as Head,ht1.Node as `LedgerHead`,v2.ChoosenDate as `Date`,v2.Narration,(case when v2.Voucher_Type='D' then v2.Amount else 0.00 end) as Credit, (case when v2.Voucher_Type='C' then v2.Amount else 0.00 end) as Debit FROM `svcf`.`voucher` as v1 left join `svcf`.`voucher` as v2 on (v1.DualTransactionKey=v2.DualTransactionKey )  join headstree as ht2 on v1.Head_Id=ht2.NodeID join headstree as ht1 on (v2.Head_Id=ht1.NodeID ) where v1.RootID=10 and v2.RootID<>10 and v1.BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and v2.BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and v2.ChoosenDate<'" + balayer.indiandateToMysqlDate(dateToConsolidated.Text) + "' and v1.ChoosenDate<'" + balayer.indiandateToMysqlDate(dateToConsolidated.Text) + "'";
            hfStamps.Value = MySqldsStamps.SelectCommand;
            gridStamps.DataBind();
            lblMaintainPost.Text = "post";
        }

        protected void TotalStampsto_Click(object sender, EventArgs e)
        {
            gridStamps.FilterExpression = "";
            ClientScriptManager script = Page.ClientScript;
            gridStamps.Focus();
            lblMaintain.Text = "li_Stamps";
            gridStamps.SettingsText.Title = "Total Stamps Amount on Particular Day:" + DateTime.Parse(dateToConsolidated.Text).ToString("dd/MM/yyyy");
            MySqldsStamps.ConnectionString = CommonClassFile.ConnectionString;
            MySqldsStamps.SelectCommand = @"SELECT distinct(v2.TransactionKey),ht2.Node as Head,ht1.Node as `LedgerHead`,v2.ChoosenDate as `Date`,v2.Narration,(case when v2.Voucher_Type='D' then v2.Amount else 0.00 end) as Credit, (case when v2.Voucher_Type='C' then v2.Amount else 0.00 end) as Debit FROM `svcf`.`voucher` as v1 left join `svcf`.`voucher` as v2 on (v1.DualTransactionKey=v2.DualTransactionKey )  join headstree as ht2 on v1.Head_Id=ht2.NodeID join headstree as ht1 on (v2.Head_Id=ht1.NodeID ) where v1.RootID=10 and v2.RootID<>10 and v1.BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and v2.BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and v2.ChoosenDate<='" + balayer.indiandateToMysqlDate(dateToConsolidated.Text) + "' and v1.ChoosenDate='" + balayer.indiandateToMysqlDate(dateToConsolidated.Text) + "'";
            hfStamps.Value = MySqldsStamps.SelectCommand;
            gridStamps.DataBind();
            lblMaintainPost.Text = "post";
        }

        protected void aDrProfitLossAmountprDay_Click(object sender, EventArgs e)
        {
            gridProfitAndLoss.FilterExpression = "";
            ClientScriptManager script = Page.ClientScript;
            gridProfitAndLoss.FilterExpression = "[Debit] != '0.00'";
            gridProfitAndLoss.Focus();
            lblMaintain.Text = "li_PandL";
            gridProfitAndLoss.SettingsText.Title = "Profit & Loss Debit Amount on :" + DateTime.Parse(dateToConsolidated.Text).AddDays(-1).ToString("dd/MM/yyyy");
            MySqldsProfitAndLoss.ConnectionString = CommonClassFile.ConnectionString;
            MySqldsProfitAndLoss.SelectCommand = @"select t1.ChoosenDate as `Date`,t3.Node as `LedgerHead` ,t1.Narration,(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )as `Credit`,(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )as `Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on (t4.MemberIDNew=t1.MemberID) left Join headstree as t8 on (t1.ChitGroupID=t8.NodeID) where `t1`.`BranchID` = " + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and `t1`.`RootID` = 11 and t1.ChoosenDate <'" + balayer.indiandateToMysqlDate(dateToConsolidated.Text) + "' ";
            hfProfitAndLoss.Value = MySqldsProfitAndLoss.SelectCommand;
            gridProfitAndLoss.DataBind();
            lblMaintainPost.Text = "post";
        }

        protected void aCrProfitLossAmountprDay_Click(object sender, EventArgs e)
        {
            gridProfitAndLoss.FilterExpression = "";
            ClientScriptManager script = Page.ClientScript;
            gridProfitAndLoss.FilterExpression = "[Credit] != '0.00'";
            gridProfitAndLoss.Focus();
            lblMaintain.Text = "li_PandL";
            gridProfitAndLoss.SettingsText.Title = "Profit & Loss Credit Amount on :" + DateTime.Parse(dateToConsolidated.Text).AddDays(-1).ToString("dd/MM/yyyy");
            MySqldsProfitAndLoss.ConnectionString = CommonClassFile.ConnectionString;
            MySqldsProfitAndLoss.SelectCommand = @"select t1.ChoosenDate as `Date`,t3.Node as `LedgerHead` ,t1.Narration,(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )as `Credit`,(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )as `Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on (t4.MemberIDNew=t1.MemberID) left Join headstree as t8 on (t1.ChitGroupID=t8.NodeID) where `t1`.`BranchID` = " + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and `t1`.`RootID` = 11 and t1.ChoosenDate <'" + balayer.indiandateToMysqlDate(dateToConsolidated.Text) + "' ";
            hfProfitAndLoss.Value = MySqldsProfitAndLoss.SelectCommand;
            gridProfitAndLoss.DataBind();
            lblMaintainPost.Text = "post";
        }

        protected void aCrProfitLossAmountcrDay_Click(object sender, EventArgs e)
        {
            gridProfitAndLoss.FilterExpression = "";
            ClientScriptManager script = Page.ClientScript;
            gridProfitAndLoss.FilterExpression = "[Credit] != '0.00'";
            gridProfitAndLoss.Focus();
            lblMaintain.Text = "li_PandL";
            gridProfitAndLoss.SettingsText.Title = "Profit & Loss Credit Amount on :" + DateTime.Parse(dateToConsolidated.Text).ToString("dd/MM/yyyy");
            MySqldsProfitAndLoss.ConnectionString = CommonClassFile.ConnectionString;
            MySqldsProfitAndLoss.SelectCommand = @"select t1.ChoosenDate as `Date`,t3.Node as `LedgerHead` ,t1.Narration,(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )as `Credit`,(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )as `Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on (t4.MemberIDNew=t1.MemberID) left Join headstree as t8 on (t1.ChitGroupID=t8.NodeID) where `t1`.`BranchID` = " + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and `t1`.`RootID` = 11 and t1.ChoosenDate <='" + balayer.indiandateToMysqlDate(dateToConsolidated.Text) + "' ";
            hfProfitAndLoss.Value = MySqldsProfitAndLoss.SelectCommand;
            gridProfitAndLoss.DataBind();
            lblMaintainPost.Text = "post";
        }

        protected void aDrProfitLossAmountcrDay_Click(object sender, EventArgs e)
        {
            gridProfitAndLoss.FilterExpression = "";
            ClientScriptManager script = Page.ClientScript;
            gridProfitAndLoss.FilterExpression = "[Debit] != '0.00'";
            gridProfitAndLoss.Focus();
            lblMaintain.Text = "li_PandL";
            gridProfitAndLoss.SettingsText.Title = "Profit & Loss Debit Amount on :" + DateTime.Parse(dateToConsolidated.Text).ToString("dd/MM/yyyy");
            MySqldsProfitAndLoss.ConnectionString = CommonClassFile.ConnectionString;
            MySqldsProfitAndLoss.SelectCommand = @"select t1.ChoosenDate as `Date`,t3.Node as `LedgerHead` ,t1.Narration,(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )as `Credit`,(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )as `Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on (t4.MemberIDNew=t1.MemberID) left Join headstree as t8 on (t1.ChitGroupID=t8.NodeID) where `t1`.`BranchID` = " + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and `t1`.`RootID` = 11 and t1.ChoosenDate <='" + balayer.indiandateToMysqlDate(dateToConsolidated.Text) + "' ";
            hfProfitAndLoss.Value = MySqldsProfitAndLoss.SelectCommand;
            gridProfitAndLoss.DataBind();
            lblMaintainPost.Text = "post";
        }

        protected void aCrProfitLossAmounttoDay_Click(object sender, EventArgs e)
        {
            gridProfitAndLoss.FilterExpression = "";
            ClientScriptManager script = Page.ClientScript;
            gridProfitAndLoss.FilterExpression = "[Credit] != '0.00'";
            gridProfitAndLoss.Focus();
            lblMaintain.Text = "li_PandL";
            gridProfitAndLoss.SettingsText.Title = "Profit & Loss Credit Amount on Particular Day:" + DateTime.Parse(dateToConsolidated.Text).ToString("dd/MM/yyyy");
            MySqldsProfitAndLoss.ConnectionString = CommonClassFile.ConnectionString;
            MySqldsProfitAndLoss.SelectCommand = @"select t1.ChoosenDate as `Date`,t3.Node as `LedgerHead` ,t1.Narration,(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )as `Credit`,(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )as `Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on (t4.MemberIDNew=t1.MemberID) left Join headstree as t8 on (t1.ChitGroupID=t8.NodeID) where `t1`.`BranchID` = " + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and `t1`.`RootID` = 11 and t1.ChoosenDate ='" + balayer.indiandateToMysqlDate(dateToConsolidated.Text) + "' ";
            hfProfitAndLoss.Value = MySqldsProfitAndLoss.SelectCommand;
            gridProfitAndLoss.DataBind();
            lblMaintainPost.Text = "post";
        }

        protected void aDrProfitLossAmounttoDay_Click(object sender, EventArgs e)
        {
            gridProfitAndLoss.FilterExpression = "";
            ClientScriptManager script = Page.ClientScript;
            gridProfitAndLoss.FilterExpression = "[Debit] != '0.00'";
            gridProfitAndLoss.Focus();
            lblMaintain.Text = "li_PandL";
            gridProfitAndLoss.SettingsText.Title = "Profit & Loss Debit Amount on Particular Day:" + DateTime.Parse(dateToConsolidated.Text).ToString("dd/MM/yyyy");
            MySqldsProfitAndLoss.ConnectionString = CommonClassFile.ConnectionString;
            MySqldsProfitAndLoss.SelectCommand = @"select t1.ChoosenDate as `Date`,t3.Node as `LedgerHead` ,t1.Narration,(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )as `Credit`,(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )as `Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on (t4.MemberIDNew=t1.MemberID) left Join headstree as t8 on (t1.ChitGroupID=t8.NodeID) where `t1`.`BranchID` = " + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and `t1`.`RootID` = 11 and t1.ChoosenDate ='" + balayer.indiandateToMysqlDate(dateToConsolidated.Text) + "' ";
            hfProfitAndLoss.Value = MySqldsProfitAndLoss.SelectCommand;
            gridProfitAndLoss.DataBind();
            lblMaintainPost.Text = "post";
        }

        protected void TotalProfitLosscr_Click(object sender, EventArgs e)
        {
            gridProfitAndLoss.FilterExpression = "";
            ClientScriptManager script = Page.ClientScript;
            gridProfitAndLoss.Focus();
            lblMaintain.Text = "li_PandL";
            gridProfitAndLoss.SettingsText.Title = "Total Profit & Loss Amount on :" + DateTime.Parse(dateToConsolidated.Text).ToString("dd/MM/yyyy");
            MySqldsProfitAndLoss.ConnectionString = CommonClassFile.ConnectionString;
            MySqldsProfitAndLoss.SelectCommand = @"select t1.ChoosenDate as `Date`,t3.Node as `LedgerHead` ,t1.Narration,(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )as `Credit`,(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )as `Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on (t4.MemberIDNew=t1.MemberID) left Join headstree as t8 on (t1.ChitGroupID=t8.NodeID) where `t1`.`BranchID` = " + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and `t1`.`RootID` = 11 and t1.ChoosenDate <='" + balayer.indiandateToMysqlDate(dateToConsolidated.Text) + "' ";
            hfProfitAndLoss.Value = MySqldsProfitAndLoss.SelectCommand;
            gridProfitAndLoss.DataBind();
            lblMaintainPost.Text = "post";
        }

        protected void TotalProfitLosspr_Click(object sender, EventArgs e)
        {
            gridProfitAndLoss.FilterExpression = "";
            ClientScriptManager script = Page.ClientScript;
            gridProfitAndLoss.Focus();
            lblMaintain.Text = "li_PandL";
            gridProfitAndLoss.SettingsText.Title = "Total Profit & Loss Amount on :" + DateTime.Parse(dateToConsolidated.Text).AddDays(-1).ToString("dd/MM/yyyy");
            MySqldsProfitAndLoss.ConnectionString = CommonClassFile.ConnectionString;
            MySqldsProfitAndLoss.SelectCommand = @"select t1.ChoosenDate as `Date`,t3.Node as `LedgerHead` ,t1.Narration,(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )as `Credit`,(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )as `Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on (t4.MemberIDNew=t1.MemberID) left Join headstree as t8 on (t1.ChitGroupID=t8.NodeID) where `t1`.`BranchID` = " + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and `t1`.`RootID` = 11 and t1.ChoosenDate <'" + balayer.indiandateToMysqlDate(dateToConsolidated.Text) + "' ";
            hfProfitAndLoss.Value = MySqldsProfitAndLoss.SelectCommand;
            gridProfitAndLoss.DataBind();
            lblMaintainPost.Text = "post";
        }

        protected void TotalProfitLossto_Click(object sender, EventArgs e)
        {
            gridProfitAndLoss.FilterExpression = "";
            ClientScriptManager script = Page.ClientScript;
            gridProfitAndLoss.Focus();
            lblMaintain.Text = "li_PandL";
            gridProfitAndLoss.SettingsText.Title = "Total Profit & Loss Amount on Particular Day:" + DateTime.Parse(dateToConsolidated.Text).ToString("dd/MM/yyyy");
            MySqldsProfitAndLoss.ConnectionString = CommonClassFile.ConnectionString;
            MySqldsProfitAndLoss.SelectCommand = @"select t1.ChoosenDate as `Date`,t3.Node as `LedgerHead` ,t1.Narration,(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )as `Credit`,(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )as `Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on (t4.MemberIDNew=t1.MemberID) left Join headstree as t8 on (t1.ChitGroupID=t8.NodeID) where `t1`.`BranchID` = " + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and `t1`.`RootID` = 11 and t1.ChoosenDate ='" + balayer.indiandateToMysqlDate(dateToConsolidated.Text) + "' ";
            hfProfitAndLoss.Value = MySqldsProfitAndLoss.SelectCommand;
            gridProfitAndLoss.DataBind();
            lblMaintainPost.Text = "post";
        }

        protected void aDrCashprDay_Click(object sender, EventArgs e)
        {
            gridCash.FilterExpression = "";
            ClientScriptManager script = Page.ClientScript;
            gridCash.FilterExpression = "[Debit] != '0.00'";
            gridCash.Focus();
            lblMaintain.Text = "li1";
            gridCash.SettingsText.Title = "Cash Debit Amount on :" + DateTime.Parse(dateToConsolidated.Text).AddDays(-1).ToString("dd/MM/yyyy");
            MySqldsCash.ConnectionString = CommonClassFile.ConnectionString;
            MySqldsCash.SelectCommand = @"select t1.ChoosenDate as `Date`,t8.Node as LedgerHead ,t1.Narration,(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )as `Credit`,(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )as `Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left Join headstree as t8 on (t1.ChitGroupID=t8.NodeID) where `t1`.`BranchID` = " + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and `t1`.`RootID` = 12 and t1.ChoosenDate<'" + balayer.indiandateToMysqlDate(dateToConsolidated.Text) + "'";
            hfCash.Value = MySqldsCash.SelectCommand;
            gridCash.DataBind();
            lblMaintainPost.Text = "post";
        }

        protected void aCrCashprDay_Click(object sender, EventArgs e)
        {
            gridCash.FilterExpression = "";
            ClientScriptManager script = Page.ClientScript;
            gridCash.FilterExpression = "[Credit] != '0.00'";
            gridCash.Focus();
            lblMaintain.Text = "li1";
            gridCash.SettingsText.Title = "Cash Credit Amount on :" + DateTime.Parse(dateToConsolidated.Text).AddDays(-1).ToString("dd/MM/yyyy");
            MySqldsCash.ConnectionString = CommonClassFile.ConnectionString;
            MySqldsCash.SelectCommand = @"select t1.ChoosenDate as `Date`,t8.Node as LedgerHead ,t1.Narration,(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )as `Credit`,(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )as `Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left Join headstree as t8 on (t1.ChitGroupID=t8.NodeID) where `t1`.`BranchID` = " + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and `t1`.`RootID` = 12 and t1.ChoosenDate<'" + balayer.indiandateToMysqlDate(dateToConsolidated.Text) + "'";
            hfCash.Value = MySqldsCash.SelectCommand;
            gridCash.DataBind();
            lblMaintainPost.Text = "post";
        }

        protected void aCrCashAmountcrDay_Click(object sender, EventArgs e)
        {
            gridCash.FilterExpression = "";
            ClientScriptManager script = Page.ClientScript;
            gridCash.FilterExpression = "[Credit] != '0.00'";
            gridCash.Focus();
            lblMaintain.Text = "li1";
            gridCash.SettingsText.Title = "Cash Credit Amount on :" + DateTime.Parse(dateToConsolidated.Text).ToString("dd/MM/yyyy");
            MySqldsCash.ConnectionString = CommonClassFile.ConnectionString;
            MySqldsCash.SelectCommand = @"select t1.ChoosenDate as `Date`,t8.Node as LedgerHead ,t1.Narration,(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )as `Credit`,(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )as `Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left Join headstree as t8 on (t1.ChitGroupID=t8.NodeID) where `t1`.`BranchID` = " + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and `t1`.`RootID` = 12 and t1.ChoosenDate <='" + balayer.indiandateToMysqlDate(dateToConsolidated.Text) + "'";
            hfCash.Value = MySqldsCash.SelectCommand;
            gridCash.DataBind();
            lblMaintainPost.Text = "post";
        }

        protected void aDrCashAmountcrDay_Click(object sender, EventArgs e)
        {
            gridCash.FilterExpression = "";
            ClientScriptManager script = Page.ClientScript;
            gridCash.FilterExpression = "[Debit] != '0.00'";
            gridCash.Focus();
            lblMaintain.Text = "li1";
            gridCash.SettingsText.Title = "Cash Debit Amount on :" + DateTime.Parse(dateToConsolidated.Text).ToString("dd/MM/yyyy");
            MySqldsCash.ConnectionString = CommonClassFile.ConnectionString;
            MySqldsCash.SelectCommand = @"select t1.ChoosenDate as `Date`,t8.Node as LedgerHead ,t1.Narration,(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )as `Credit`,(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )as `Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left Join headstree as t8 on (t1.ChitGroupID=t8.NodeID) where `t1`.`BranchID` = " + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and `t1`.`RootID` = 12 and t1.ChoosenDate <='" + balayer.indiandateToMysqlDate(dateToConsolidated.Text) + "'";
            hfCash.Value = MySqldsCash.SelectCommand;
            gridCash.DataBind();
            lblMaintainPost.Text = "post";
        }

        protected void aCrCashAmounttoDay_Click(object sender, EventArgs e)
        {
            gridCash.FilterExpression = "";
            ClientScriptManager script = Page.ClientScript;
            gridCash.FilterExpression = "[Credit] != '0.00'";
            gridCash.Focus();
            lblMaintain.Text = "li1";
            gridCash.SettingsText.Title = "Cash Credit Amount on Particular Day:" + DateTime.Parse(dateToConsolidated.Text).ToString("dd/MM/yyyy");
            MySqldsCash.ConnectionString = CommonClassFile.ConnectionString;
            MySqldsCash.SelectCommand = @"select t1.ChoosenDate as `Date`,t8.Node as LedgerHead ,t1.Narration,(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )as `Credit`,(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )as `Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left Join headstree as t8 on (t1.ChitGroupID=t8.NodeID) where `t1`.`BranchID` = " + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and `t1`.`RootID` = 12 and t1.ChoosenDate ='" + balayer.indiandateToMysqlDate(dateToConsolidated.Text) + "'";
            hfCash.Value = MySqldsCash.SelectCommand;
            gridCash.DataBind();
            lblMaintainPost.Text = "post";
        }

        protected void aDrCashAmounttoDay_Click(object sender, EventArgs e)
        {
            gridCash.FilterExpression = "";
            ClientScriptManager script = Page.ClientScript;
            gridCash.FilterExpression = "[Debit] != '0.00'";
            gridCash.Focus();
            lblMaintain.Text = "li1";
            gridCash.SettingsText.Title = "Cash Debit Amount on Particular Day:" + DateTime.Parse(dateToConsolidated.Text).ToString("dd/MM/yyyy");
            MySqldsCash.ConnectionString = CommonClassFile.ConnectionString;
            MySqldsCash.SelectCommand = @"select t1.ChoosenDate as `Date`,t8.Node as LedgerHead ,t1.Narration,(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )as `Credit`,(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )as `Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left Join headstree as t8 on (t1.ChitGroupID=t8.NodeID) where `t1`.`BranchID` = " + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and `t1`.`RootID` = 12 and t1.ChoosenDate ='" + balayer.indiandateToMysqlDate(dateToConsolidated.Text) + "'";
            hfCash.Value = MySqldsCash.SelectCommand;
            gridCash.DataBind();
            lblMaintainPost.Text = "post";
        }

        protected void TotalCashcr_Click(object sender, EventArgs e)
        {
            gridCash.FilterExpression = "";
            ClientScriptManager script = Page.ClientScript;
            gridCash.Focus();
            lblMaintain.Text = "li1";
            gridCash.SettingsText.Title = "Total Cash Amount on :" + DateTime.Parse(dateToConsolidated.Text).ToString("dd/MM/yyyy");
            MySqldsCash.ConnectionString = CommonClassFile.ConnectionString;
            MySqldsCash.SelectCommand = @"select t1.ChoosenDate as `Date`,t8.Node as LedgerHead ,t1.Narration,(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )as `Credit`,(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )as `Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left Join headstree as t8 on (t1.ChitGroupID=t8.NodeID) where `t1`.`BranchID` = " + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and `t1`.`RootID` = 12 and t1.ChoosenDate <='" + balayer.indiandateToMysqlDate(dateToConsolidated.Text) + "'";
            hfCash.Value = MySqldsCash.SelectCommand;
            gridCash.DataBind();
            lblMaintainPost.Text = "post";
        }

        protected void TotalCashpr_Click(object sender, EventArgs e)
        {
            gridCash.FilterExpression = "";
            ClientScriptManager script = Page.ClientScript;
            gridCash.Focus();
            lblMaintain.Text = "li1";
            gridCash.SettingsText.Title = "Total Cash Amount on :" + DateTime.Parse(dateToConsolidated.Text).AddDays(-1).ToString("dd/MM/yyyy");
            MySqldsCash.ConnectionString = CommonClassFile.ConnectionString;
            MySqldsCash.SelectCommand = @"select t1.ChoosenDate as `Date`,t8.Node as LedgerHead ,t1.Narration,(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )as `Credit`,(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )as `Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left Join headstree as t8 on (t1.ChitGroupID=t8.NodeID) where `t1`.`BranchID` = " + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and `t1`.`RootID` = 12 and t1.ChoosenDate <'" + balayer.indiandateToMysqlDate(dateToConsolidated.Text) + "'";
            hfCash.Value = MySqldsCash.SelectCommand;
            gridCash.DataBind();
            lblMaintainPost.Text = "post";
        }

        protected void TotalCashto_Click(object sender, EventArgs e)
        {
            gridCash.FilterExpression = "";
            ClientScriptManager script = Page.ClientScript;
            gridCash.Focus();
            lblMaintain.Text = "li1";
            gridCash.SettingsText.Title = "Total Cash Amount on Particular Day:" + DateTime.Parse(dateToConsolidated.Text).ToString("dd/MM/yyyy");
            MySqldsCash.ConnectionString = CommonClassFile.ConnectionString;
            MySqldsCash.SelectCommand = @"select t1.ChoosenDate as `Date`,t8.Node as LedgerHead ,t1.Narration,(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )as `Credit`,(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )as `Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left Join headstree as t8 on (t1.ChitGroupID=t8.NodeID) where `t1`.`BranchID` = " + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and `t1`.`RootID` = 12 and t1.ChoosenDate ='" + balayer.indiandateToMysqlDate(dateToConsolidated.Text) + "'";
            hfCash.Value = MySqldsCash.SelectCommand;
            gridCash.DataBind();
            lblMaintainPost.Text = "post";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            lblMaintainPost.Text = "";
            ClientScriptManager script = Page.ClientScript;
            if (!IsPostBack)
            {
                if (Request.QueryString == null || Request.QueryString.ToString() == "")
                {
                    dateToConsolidated.Text = "31/05/2013";
                }
                else
                {
                    dateToConsolidated.Text = Request.QueryString["FromDate"];
                }
                hfBranch.Value= @"select t1.ChoosenDate as `Date`,t8.Node as LedgerHead ,t3.Node as Branch , t1.Narration,(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )as `Credit`,(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )as `Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left join headstree as t8 on (t1.ChitGroupID=t8.NodeID) where `t1`.`BranchID` =" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and `t1`.`RootID` = 1 and t1.ChoosenDate = '" + balayer.indiandateToMysqlDate(dateToConsolidated.Text) + "'";
                hfInvestment.Value = @"select t1.ChoosenDate as `Date`,t8.Node as LedgerHead ,t3.Node as Head , t1.Narration,(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )as `Credit`,(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )as `Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left join headstree as t8 on (t1.ChitGroupID=t8.NodeID) where `t1`.`BranchID` =" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and `t1`.`RootID` = 2 and t1.ChoosenDate = '" + balayer.indiandateToMysqlDate(dateToConsolidated.Text) + "'";
                hfBank.Value = @"select t1.ChoosenDate as `Date`,t8.Node as LedgerHead ,bb.BankName as Bank,bb.AccountNo as `AccountNo.`,tt.CustomersBankName as CustomersBank,tt.ChequeDDNO as `ChequeNo.`,t1.Narration,(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )as `Credit`,(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )as `Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left Join headstree as t8 on (t1.ChitGroupID=t8.NodeID) left join transbank as tt on (t1.TransactionKey=tt.TransactionKey) left join bankdetails as bb on (t1.Head_Id=bb.Head_Id) where `t1`.`BranchID` =" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and `t1`.`RootID` = 3 and t1.ChoosenDate = '" + balayer.indiandateToMysqlDate(dateToConsolidated.Text) + "'";
                hfOtherItems.Value = @"select t1.ChoosenDate as `Date`,t8.Node as Head ,t3.Node as LedgerHead , t1.Narration,(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )as `Credit`,(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )as `Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left join headstree as t8 on (t1.ChitGroupID=t8.NodeID) where `t1`.`BranchID` =" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and `t1`.`RootID` = 4 and t1.ChoosenDate = '" + balayer.indiandateToMysqlDate(dateToConsolidated.Text) + "'";
                hfChits.Value = @"select t1.ChoosenDate as `Date`,t8.Node as LedgerHead ,t3.Node as Token ,t1.Narration,(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )as `Credit`,(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )as `Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left Join headstree as t8 on t1.ChitGroupID=t8.NodeID where `t1`.`BranchID` = " + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and `t1`.`RootID` = 5 and t1.ChoosenDate ='" + balayer.indiandateToMysqlDate(dateToConsolidated.Text) + "'";
                hfForemanChits.Value = @"select t1.ChoosenDate as `Date`,t8.Node as LedgerHead ,t3.Node as Token ,t1.Narration,(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )as `Credit`,(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )as `Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left Join headstree as t8 on (t1.ChitGroupID=t8.NodeID) where `t1`.`BranchID` = " + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and `t1`.`RootID` = 6 and t1.ChoosenDate ='" + balayer.indiandateToMysqlDate(dateToConsolidated.Text) + "';";
                hfDecreeDebtors.Value = @"SELECT  t1.ChoosenDate as `Date`,t3.Node as Head , t1.Narration,(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )as `Credit`,(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )as `Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left join headstree as t8 on (t1.ChitGroupID=t8.NodeID) where `t1`.`BranchID` = " + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and `t1`.`RootID` = 7 and t1.ChoosenDate ='" + balayer.indiandateToMysqlDate(dateToConsolidated.Text) + "';";
                hfLoans.Value = @"select t1.ChoosenDate as `Date`,t3.Node as `LedgerHead` ,t1.Narration,(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )as `Credit`,(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )as `Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left Join headstree as t8 on (t1.ChitGroupID=t8.NodeID) where `t1`.`BranchID` = " + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and `t1`.`RootID` = 8 and t1.ChoosenDate='" + balayer.indiandateToMysqlDate(dateToConsolidated.Text) + "';";
                hfAdvances.Value = @"SELECT voucher.RootID,`voucher`.`ChoosenDate` as `Date`,`voucher`.`Narration`,`headstree`.`Node` as `LedgerHead`,sum((case when voucher.Voucher_Type='C' then voucher.Amount else 0.00 end )) as `Credit`,sum((case when voucher.Voucher_Type='D' then voucher.Amount else 0.00 end )) as `Debit` FROM `svcf`.`voucher` join headstree on (voucher.Head_Id=headstree.NodeID) where `voucher`.`ChoosenDate`='" + balayer.indiandateToMysqlDate(dateToConsolidated.Text) + "' and voucher.BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and RootID=9 group by voucher.Head_Id";
                hfStamps.Value = @"SELECT distinct(v2.TransactionKey),ht2.Node as Head,ht1.Node as `LedgerHead`,v2.ChoosenDate as `Date`,v2.Narration,(case when v2.Voucher_Type='D' then v2.Amount else 0.00 end) as Credit, (case when v2.Voucher_Type='C' then v2.Amount else 0.00 end) as Debit FROM `svcf`.`voucher` as v1 left join `svcf`.`voucher` as v2 on (v1.DualTransactionKey=v2.DualTransactionKey )  join headstree as ht2 on v1.Head_Id=ht2.NodeID join headstree as ht1 on (v2.Head_Id=ht1.NodeID ) where v1.RootID=10 and v2.RootID<>10 and v1.BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and v2.BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and v2.ChoosenDate='" + balayer.indiandateToMysqlDate(dateToConsolidated.Text) + "' and v1.ChoosenDate='" + balayer.indiandateToMysqlDate(dateToConsolidated.Text) + "'";
                hfProfitAndLoss.Value = @"select t1.ChoosenDate as `Date`,t3.Node as `LedgerHead` ,t1.Narration,(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )as `Credit`,(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )as `Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on (t4.MemberIDNew=t1.MemberID) left Join headstree as t8 on (t1.ChitGroupID=t8.NodeID) where `t1`.`BranchID` = " + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and `t1`.`RootID` = 11 and t1.ChoosenDate ='" + balayer.indiandateToMysqlDate(dateToConsolidated.Text) + "' ";
                hfCash.Value = @"select t1.ChoosenDate as `Date`,t8.Node as LedgerHead ,t1.Narration,(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )as `Credit`,(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )as `Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left Join headstree as t8 on (t1.ChitGroupID=t8.NodeID) where `t1`.`BranchID` = " + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and `t1`.`RootID` = 12 and t1.ChoosenDate='" + balayer.indiandateToMysqlDate(dateToConsolidated.Text) + "'";
                setStats();
                foreach (GridViewColumn column in grid.Columns)
                {
                    if (column is GridViewDataColumn)
                    {
                        ((GridViewDataColumn)column).Settings.AutoFilterCondition = AutoFilterCondition.Contains;
                    }
                }
                foreach (GridViewColumn column in gridBanks.Columns)
                {
                    if (column is GridViewDataColumn)
                    {
                        ((GridViewDataColumn)column).Settings.AutoFilterCondition = AutoFilterCondition.Contains;
                    }
                }
                foreach (GridViewColumn column in gridCash.Columns)
                {
                    if (column is GridViewDataColumn)
                    {
                        ((GridViewDataColumn)column).Settings.AutoFilterCondition = AutoFilterCondition.Contains;
                    }
                }
                foreach (GridViewColumn column in gridChits.Columns)
                {
                    if (column is GridViewDataColumn)
                    {
                        ((GridViewDataColumn)column).Settings.AutoFilterCondition = AutoFilterCondition.Contains;
                    }
                }

                foreach (GridViewColumn column in gridDecreeDebtors.Columns)
                {
                    if (column is GridViewDataColumn)
                    {
                        ((GridViewDataColumn)column).Settings.AutoFilterCondition = AutoFilterCondition.Contains;
                    }
                }
                foreach (GridViewColumn column in gridForemanChits.Columns)
                {
                    if (column is GridViewDataColumn)
                    {
                        ((GridViewDataColumn)column).Settings.AutoFilterCondition = AutoFilterCondition.Contains;
                    }
                }
                foreach (GridViewColumn column in gridInvestments.Columns)
                {
                    if (column is GridViewDataColumn)
                    {
                        ((GridViewDataColumn)column).Settings.AutoFilterCondition = AutoFilterCondition.Contains;
                    }
                }
                foreach (GridViewColumn column in gridLoans.Columns)
                {
                    if (column is GridViewDataColumn)
                    {
                        ((GridViewDataColumn)column).Settings.AutoFilterCondition = AutoFilterCondition.Contains;
                    }
                }
                foreach (GridViewColumn column in gridOtherItems.Columns)
                {
                    if (column is GridViewDataColumn)
                    {
                        ((GridViewDataColumn)column).Settings.AutoFilterCondition = AutoFilterCondition.Contains;
                    }
                }
                foreach (GridViewColumn column in gridProfitAndLoss.Columns)
                {
                    if (column is GridViewDataColumn)
                    {
                        ((GridViewDataColumn)column).Settings.AutoFilterCondition = AutoFilterCondition.Contains;
                    }
                }

                foreach (GridViewColumn column in gridStamps.Columns)
                {
                    if (column is GridViewDataColumn)
                    {
                        ((GridViewDataColumn)column).Settings.AutoFilterCondition = AutoFilterCondition.Contains;
                    }
                }
                foreach (GridViewColumn column in gridSundriesAndAdvances.Columns)
                {
                    if (column is GridViewDataColumn)
                    {
                        ((GridViewDataColumn)column).Settings.AutoFilterCondition = AutoFilterCondition.Contains;
                    }
                }
            }
            select();
        }

        void ApplyLayout(int layoutIndex)
        {
            try
            {
                switch (layoutIndex)
                {
                    case 1:
                        grid.BeginUpdate();
                        grid.ClearSort();
                        grid.GroupBy(grid.Columns["Branch"]);
                        grid.GroupBy(grid.Columns["Date"]);
                        grid.EndUpdate();
                        grid.CollapseAll();
                        break;
                    case 3:
                        gridBanks.BeginUpdate();
                        gridBanks.ClearSort();
                        gridBanks.GroupBy(gridBanks.Columns["Bank"]);
                        gridBanks.GroupBy(gridBanks.Columns["Date"]);
                        gridBanks.EndUpdate();
                        gridBanks.CollapseAll();
                        break;
               }
            }
            finally
            {
                
            }
        }
        protected void ResetStats()
        {
            //Branch
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

        protected void setStats()
        {
            ResetStats();
            DateTime previousDate = DateTime.Parse( dateToConsolidated.Text).AddDays(-1);
            DataTable dtStats = balayer.GetDataTable(" SELECT RootID,sum((case when Voucher_Type='C' then Amount else 0.00 end ))as `Credit Amount`,sum((case when Voucher_Type='D' then Amount else 0.00 end ))as `Debit Amount` FROM `svcf`.`voucher` where BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and ChoosenDate <='" + balayer.indiandateToMysqlDate(dateToConsolidated.Text) + "' and RootID=1 group by RootID");
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

            DataTable dtStatsPreviousday = balayer.GetDataTable(" SELECT RootID,sum((case when Voucher_Type='C' then Amount else 0.00 end ))as `Credit Amount`,sum((case when Voucher_Type='D' then Amount else 0.00 end ))as `Debit Amount` FROM `svcf`.`voucher` where BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and ChoosenDate  <='" +previousDate.ToString("yyyy/MM/dd") + "' and RootID=1 group by RootID");
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


            DataTable dtStatsCurrentDay = balayer.GetDataTable(" SELECT RootID,sum((case when Voucher_Type='C' then Amount else 0.00 end ))as `Credit Amount`,sum((case when Voucher_Type='D' then Amount else 0.00 end ))as `Debit Amount` FROM `svcf`.`voucher` where BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and ChoosenDate between '" + (previousDate.AddDays(1)).ToString("yyyy/MM/dd") + "' and '" + balayer.indiandateToMysqlDate(dateToConsolidated.Text) + "' and RootID=1 group by RootID");
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
            DataTable dtStatsInvestments = balayer.GetDataTable(" SELECT RootID,sum((case when Voucher_Type='C' then Amount else 0.00 end ))as `Credit Amount`,sum((case when Voucher_Type='D' then Amount else 0.00 end ))as `Debit Amount` FROM `svcf`.`voucher` where BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and ChoosenDate <='" + balayer.indiandateToMysqlDate(dateToConsolidated.Text) + "' and RootID=2 group by RootID");
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

            DataTable dtStatsPreviousdayInvestments = balayer.GetDataTable(" SELECT RootID,sum((case when Voucher_Type='C' then Amount else 0.00 end ))as `Credit Amount`,sum((case when Voucher_Type='D' then Amount else 0.00 end ))as `Debit Amount` FROM `svcf`.`voucher` where BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and ChoosenDate  <='" + previousDate.ToString("yyyy/MM/dd") + "' and RootID=2 group by RootID");
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


            DataTable dtStatsCurrentDayInvestments = balayer.GetDataTable(" SELECT RootID,sum((case when Voucher_Type='C' then Amount else 0.00 end ))as `Credit Amount`,sum((case when Voucher_Type='D' then Amount else 0.00 end ))as `Debit Amount` FROM `svcf`.`voucher` where BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and ChoosenDate between '" + (previousDate.AddDays(1)).ToString("yyyy/MM/dd") + "' and '" + balayer.indiandateToMysqlDate(dateToConsolidated.Text) + "' and RootID=2 group by RootID");
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
            DataTable dtStatsBanks = balayer.GetDataTable(" SELECT RootID,sum((case when Voucher_Type='C' then Amount else 0.00 end ))as `Credit Amount`,sum((case when Voucher_Type='D' then Amount else 0.00 end ))as `Debit Amount` FROM `svcf`.`voucher` where BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and ChoosenDate <='" + balayer.indiandateToMysqlDate(dateToConsolidated.Text) + "' and RootID=3 group by RootID");
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

            DataTable dtStatsPreviousdayBanks = balayer.GetDataTable(" SELECT RootID,sum((case when Voucher_Type='C' then Amount else 0.00 end ))as `Credit Amount`,sum((case when Voucher_Type='D' then Amount else 0.00 end ))as `Debit Amount` FROM `svcf`.`voucher` where BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and ChoosenDate  <='" + previousDate.ToString("yyyy/MM/dd") + "' and RootID=3 group by RootID");
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


            DataTable dtStatsCurrentDayBanks = balayer.GetDataTable(" SELECT RootID,sum((case when Voucher_Type='C' then Amount else 0.00 end ))as `Credit Amount`,sum((case when Voucher_Type='D' then Amount else 0.00 end ))as `Debit Amount` FROM `svcf`.`voucher` where BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and ChoosenDate between '" + (previousDate.AddDays(1)).ToString("yyyy/MM/dd") + "' and '" + balayer.indiandateToMysqlDate(dateToConsolidated.Text) + "' and RootID=3 group by RootID");
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
            DataTable dtStatsOtherItems = balayer.GetDataTable(" SELECT RootID,sum((case when Voucher_Type='C' then Amount else 0.00 end ))as `Credit Amount`,sum((case when Voucher_Type='D' then Amount else 0.00 end ))as `Debit Amount` FROM `svcf`.`voucher` where BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and ChoosenDate <='" + balayer.indiandateToMysqlDate(dateToConsolidated.Text) + "' and RootID=4 group by RootID");

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

            DataTable dtStatsPreviousdayOtherItems = balayer.GetDataTable(" SELECT RootID,sum((case when Voucher_Type='C' then Amount else 0.00 end ))as `Credit Amount`,sum((case when Voucher_Type='D' then Amount else 0.00 end ))as `Debit Amount` FROM `svcf`.`voucher` where BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and ChoosenDate  <='" + previousDate.ToString("yyyy/MM/dd") + "' and RootID=4 group by RootID");
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


            DataTable dtStatsCurrentDayOtherItems = balayer.GetDataTable(" SELECT RootID,sum((case when Voucher_Type='C' then Amount else 0.00 end ))as `Credit Amount`,sum((case when Voucher_Type='D' then Amount else 0.00 end ))as `Debit Amount` FROM `svcf`.`voucher` where BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and ChoosenDate between '" + (previousDate.AddDays(1)).ToString("yyyy/MM/dd") + "' and '" + balayer.indiandateToMysqlDate(dateToConsolidated.Text) + "' and RootID=4 group by RootID");
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
            DataTable dtStatsChits = balayer.GetDataTable(" SELECT RootID,sum((case when Voucher_Type='C' then Amount else 0.00 end ))as `Credit Amount`,sum((case when Voucher_Type='D' then Amount else 0.00 end ))as `Debit Amount` FROM `svcf`.`voucher` where BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and ChoosenDate <='" + balayer.indiandateToMysqlDate(dateToConsolidated.Text) + "' and RootID=5 group by RootID");
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

            DataTable dtStatsPreviousdayChits = balayer.GetDataTable(" SELECT RootID,sum((case when Voucher_Type='C' then Amount else 0.00 end ))as `Credit Amount`,sum((case when Voucher_Type='D' then Amount else 0.00 end ))as `Debit Amount` FROM `svcf`.`voucher` where BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and ChoosenDate  <='" + previousDate.ToString("yyyy/MM/dd") + "' and RootID=5 group by RootID");
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


            DataTable dtStatsCurrentDayChits = balayer.GetDataTable(" SELECT RootID,sum((case when Voucher_Type='C' then Amount else 0.00 end ))as `Credit Amount`,sum((case when Voucher_Type='D' then Amount else 0.00 end ))as `Debit Amount` FROM `svcf`.`voucher` where BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and ChoosenDate between '" + (previousDate.AddDays(1)).ToString("yyyy/MM/dd") + "' and '" + balayer.indiandateToMysqlDate(dateToConsolidated.Text) + "' and RootID=5 group by RootID");
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
            DataTable dtStatsForemanChits = balayer.GetDataTable(" SELECT RootID,sum((case when Voucher_Type='C' then Amount else 0.00 end ))as `Credit Amount`,sum((case when Voucher_Type='D' then Amount else 0.00 end ))as `Debit Amount` FROM `svcf`.`voucher` where BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and ChoosenDate <='" + balayer.indiandateToMysqlDate(dateToConsolidated.Text) + "' and RootID=6 group by RootID");
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

            DataTable dtStatsPreviousdayForemanChits = balayer.GetDataTable(" SELECT RootID,sum((case when Voucher_Type='C' then Amount else 0.00 end ))as `Credit Amount`,sum((case when Voucher_Type='D' then Amount else 0.00 end ))as `Debit Amount` FROM `svcf`.`voucher` where BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and ChoosenDate  <='" + balayer.indiandateToMysqlDate(dateToConsolidated.Text) + "' and RootID=6 group by RootID");
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


            DataTable dtStatsCurrentDayForemanChits = balayer.GetDataTable(" SELECT RootID,sum((case when Voucher_Type='C' then Amount else 0.00 end ))as `Credit Amount`,sum((case when Voucher_Type='D' then Amount else 0.00 end ))as `Debit Amount` FROM `svcf`.`voucher` where BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and ChoosenDate between '" + (previousDate.AddDays(1)).ToString("yyyy/MM/dd") + "' and '" + balayer.indiandateToMysqlDate(dateToConsolidated.Text) + "' and RootID=6 group by RootID");
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
            DataTable dtStatsLoans = balayer.GetDataTable(" SELECT RootID,sum((case when Voucher_Type='C' then Amount else 0.00 end ))as `Credit Amount`,sum((case when Voucher_Type='D' then Amount else 0.00 end ))as `Debit Amount` FROM `svcf`.`voucher` where BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and ChoosenDate <='" + balayer.indiandateToMysqlDate(dateToConsolidated.Text) + "' and RootID=8 group by RootID");
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

            DataTable dtStatsPreviousdayLoans = balayer.GetDataTable(" SELECT RootID,sum((case when Voucher_Type='C' then Amount else 0.00 end ))as `Credit Amount`,sum((case when Voucher_Type='D' then Amount else 0.00 end ))as `Debit Amount` FROM `svcf`.`voucher` where BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and ChoosenDate  <='" + previousDate.ToString("yyyy/MM/dd") + "' and RootID=8 group by RootID");
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


            DataTable dtStatsCurrentDayLoans = balayer.GetDataTable(" SELECT RootID,sum((case when Voucher_Type='C' then Amount else 0.00 end ))as `Credit Amount`,sum((case when Voucher_Type='D' then Amount else 0.00 end ))as `Debit Amount` FROM `svcf`.`voucher` where BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and ChoosenDate between '" + (previousDate.AddDays(1)).ToString("yyyy/MM/dd") + "' and '" + balayer.indiandateToMysqlDate(dateToConsolidated.Text) + "' and RootID=8 group by RootID");
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
            DataTable dtStatsDecreeDebtors = balayer.GetDataTable(" SELECT RootID,sum((case when Voucher_Type='C' then Amount else 0.00 end ))as `Credit Amount`,sum((case when Voucher_Type='D' then Amount else 0.00 end ))as `Debit Amount` FROM `svcf`.`voucher` where BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and ChoosenDate <='" + balayer.indiandateToMysqlDate(dateToConsolidated.Text) + "' and RootID=7 group by RootID");
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

            DataTable dtStatsPreviousdayDecreeDebtors = balayer.GetDataTable(" SELECT RootID,sum((case when Voucher_Type='C' then Amount else 0.00 end ))as `Credit Amount`,sum((case when Voucher_Type='D' then Amount else 0.00 end ))as `Debit Amount` FROM `svcf`.`voucher` where BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and ChoosenDate  <='" + previousDate.ToString("yyyy/MM/dd") + "' and RootID=7 group by RootID");
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


            DataTable dtStatsCurrentDayDecreeDebtors = balayer.GetDataTable(" SELECT RootID,sum((case when Voucher_Type='C' then Amount else 0.00 end ))as `Credit Amount`,sum((case when Voucher_Type='D' then Amount else 0.00 end ))as `Debit Amount` FROM `svcf`.`voucher` where BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and ChoosenDate between '" + (previousDate.AddDays(1)).ToString("yyyy/MM/dd") + "' and '" + balayer.indiandateToMysqlDate(dateToConsolidated.Text) + "' and RootID=7 group by RootID");
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
            DataTable dtStatsSundriesAndAdvances = balayer.GetDataTable(" SELECT RootID,sum((case when Voucher_Type='C' then Amount else 0.00 end ))as `Credit Amount`,sum((case when Voucher_Type='D' then Amount else 0.00 end ))as `Debit Amount` FROM `svcf`.`voucher` where BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and ChoosenDate <='" + balayer.indiandateToMysqlDate(dateToConsolidated.Text) + "' and RootID=9 group by RootID");
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

            DataTable dtStatsPreviousdaySundriesAndAdvances = balayer.GetDataTable(" SELECT RootID,sum((case when Voucher_Type='C' then Amount else 0.00 end ))as `Credit Amount`,sum((case when Voucher_Type='D' then Amount else 0.00 end ))as `Debit Amount` FROM `svcf`.`voucher` where BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and ChoosenDate  <='" + previousDate.ToString("yyyy/MM/dd") + "' and RootID=9 group by RootID");
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


            DataTable dtStatsCurrentDaySundriesAndAdvances = balayer.GetDataTable(" SELECT RootID,sum((case when Voucher_Type='C' then Amount else 0.00 end ))as `Credit Amount`,sum((case when Voucher_Type='D' then Amount else 0.00 end ))as `Debit Amount` FROM `svcf`.`voucher` where BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and ChoosenDate between '" + (previousDate.AddDays(1)).ToString("yyyy/MM/dd") + "' and '" + balayer.indiandateToMysqlDate(dateToConsolidated.Text) + "' and RootID=9 group by RootID");
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
            DataTable dtStatsStamps = balayer.GetDataTable(" SELECT RootID,sum((case when Voucher_Type='C' then Amount else 0.00 end ))as `Credit Amount`,sum((case when Voucher_Type='D' then Amount else 0.00 end ))as `Debit Amount` FROM `svcf`.`voucher` where BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and ChoosenDate <='" + balayer.indiandateToMysqlDate(dateToConsolidated.Text) + "' and RootID=10 group by RootID");
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

            DataTable dtStatsPreviousdayStamps = balayer.GetDataTable(" SELECT RootID,sum((case when Voucher_Type='C' then Amount else 0.00 end ))as `Credit Amount`,sum((case when Voucher_Type='D' then Amount else 0.00 end ))as `Debit Amount` FROM `svcf`.`voucher` where BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and ChoosenDate  <='" + previousDate.ToString("yyyy/MM/dd") + "' and RootID=10 group by RootID");

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


            DataTable dtStatsCurrentDayStamps = balayer.GetDataTable(" SELECT RootID,sum((case when Voucher_Type='C' then Amount else 0.00 end ))as `Credit Amount`,sum((case when Voucher_Type='D' then Amount else 0.00 end ))as `Debit Amount` FROM `svcf`.`voucher` where BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and ChoosenDate between '" + (previousDate.AddDays(1)).ToString("yyyy/MM/dd") + "' and '" + balayer.indiandateToMysqlDate(dateToConsolidated.Text) + "' and RootID=10 group by RootID");
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
            DataTable dtStatsProfitAndLoss = balayer.GetDataTable("SELECT RootID,sum((case when Voucher_Type='C' then Amount else 0.00 end ))as `Credit Amount`,sum((case when Voucher_Type='D' then Amount else 0.00 end ))as `Debit Amount` FROM `svcf`.`voucher` where BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and ChoosenDate <='" + balayer.indiandateToMysqlDate(dateToConsolidated.Text) + "' and RootID=11 group by RootID");
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

            DataTable dtStatsPreviousdayProfitAndLoss = balayer.GetDataTable(" SELECT RootID,sum((case when Voucher_Type='C' then Amount else 0.00 end ))as `Credit Amount`,sum((case when Voucher_Type='D' then Amount else 0.00 end ))as `Debit Amount` FROM `svcf`.`voucher` where BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and ChoosenDate  <='" + previousDate.ToString("yyyy/MM/dd") + "' and RootID=11 group by RootID");
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


            DataTable dtStatsCurrentDayProfitAndLoss = balayer.GetDataTable(" SELECT RootID,sum((case when Voucher_Type='C' then Amount else 0.00 end ))as `Credit Amount`,sum((case when Voucher_Type='D' then Amount else 0.00 end ))as `Debit Amount` FROM `svcf`.`voucher` where BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and ChoosenDate between '" + (previousDate.AddDays(1)).ToString("yyyy/MM/dd") + "' and '" + balayer.indiandateToMysqlDate(dateToConsolidated.Text) + "' and RootID=11 group by RootID");
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
            DataTable dtStatsCash = balayer.GetDataTable(" SELECT RootID,sum((case when Voucher_Type='C' then Amount else 0.00 end ))as `Credit Amount`,sum((case when Voucher_Type='D' then Amount else 0.00 end ))as `Debit Amount` FROM `svcf`.`voucher` where BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and ChoosenDate <='" + balayer.indiandateToMysqlDate(dateToConsolidated.Text) + "' and RootID=12 group by RootID");
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

            DataTable dtStatsPreviousdayCash = balayer.GetDataTable(" SELECT RootID,sum((case when Voucher_Type='C' then Amount else 0.00 end ))as `Credit Amount`,sum((case when Voucher_Type='D' then Amount else 0.00 end ))as `Debit Amount` FROM `svcf`.`voucher` where BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and ChoosenDate  <='" + previousDate.ToString("yyyy/MM/dd") + "' and RootID=12 group by RootID");
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


            DataTable dtStatsCurrentDayCash = balayer.GetDataTable(" SELECT RootID,sum((case when Voucher_Type='C' then Amount else 0.00 end ))as `Credit Amount`,sum((case when Voucher_Type='D' then Amount else 0.00 end ))as `Debit Amount` FROM `svcf`.`voucher` where BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and ChoosenDate between '" + (previousDate.AddDays(1)).ToString("yyyy/MM/dd") + "' and '" + balayer.indiandateToMysqlDate(dateToConsolidated.Text) + "' and RootID=12 group by RootID");
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
            MySqldsBranch.SelectCommand = hfBranch.Value;
            MySqldsInvestments.ConnectionString = CommonClassFile.ConnectionString;
            MySqldsInvestments.SelectCommand = hfInvestment.Value;

            MySqldsBanks.ConnectionString = CommonClassFile.ConnectionString;
            MySqldsBanks.SelectCommand = hfBank.Value;

            MySqldsOtherItems.ConnectionString = CommonClassFile.ConnectionString;
            MySqldsOtherItems.SelectCommand = hfOtherItems.Value;

            MySqldsChits.ConnectionString = CommonClassFile.ConnectionString;
            MySqldsChits.SelectCommand = hfChits.Value;

            MySqldsForemanChits.ConnectionString = CommonClassFile.ConnectionString;
            MySqldsForemanChits.SelectCommand = hfForemanChits.Value;


            MySqldsDecreeDebtors.ConnectionString = CommonClassFile.ConnectionString;
            MySqldsDecreeDebtors.SelectCommand = hfDecreeDebtors.Value;

            MySqldsLoans.ConnectionString = CommonClassFile.ConnectionString;
            MySqldsLoans.SelectCommand = hfLoans.Value;

            MySqldsSundriesAndAdvances.ConnectionString = CommonClassFile.ConnectionString;
            MySqldsSundriesAndAdvances.SelectCommand = hfAdvances.Value;

            MySqldsStamps.ConnectionString = CommonClassFile.ConnectionString;
            MySqldsStamps.SelectCommand = hfStamps.Value;

            MySqldsProfitAndLoss.ConnectionString = CommonClassFile.ConnectionString;
            MySqldsProfitAndLoss.SelectCommand = hfProfitAndLoss.Value;


            MySqldsCash.ConnectionString = CommonClassFile.ConnectionString;
            MySqldsCash.SelectCommand = hfCash.Value;

            grid.DataBind();
            gridInvestments.DataBind();
            gridBanks.DataBind();
            gridOtherItems.DataBind();
            gridChits.DataBind();
            gridForemanChits.DataBind();
            gridDecreeDebtors.DataBind();
            gridLoans.DataBind();
            gridSundriesAndAdvances.DataBind();
            gridStamps.DataBind();
            gridProfitAndLoss.DataBind();
            gridCash.DataBind();
           
        }

        protected void BtnStatisticsGo_Click(object sender, EventArgs e)
        {
            select();
        }

        protected void ASPxGridView1_SummaryDisplayTextBranch(object sender, ASPxGridViewSummaryDisplayTextEventArgs e)
        {
            if (e.Item.FieldName == "Credit")
                e.Text = string.Format("{0}", Convert.ToDouble(e.Value));
            if (e.Item.FieldName == "Debit")
                e.Text = string.Format("{0}", Convert.ToDouble(e.Value));
            if (e.Item.FieldName == "Narration")
                e.Text = string.Format(ViewState["crdrBranch"].ToString(), Convert.ToDouble(e.Value));
        }

        protected void ASPxGridView1_SummaryDisplayTextBanks(object sender, ASPxGridViewSummaryDisplayTextEventArgs e)
        {
            if (e.Item.FieldName == "Credit")
                e.Text = string.Format("{0}", Convert.ToDouble(e.Value));
            if (e.Item.FieldName == "Debit")
                e.Text = string.Format("{0}", Convert.ToDouble(e.Value));
            if (e.Item.FieldName == "Narration")
                e.Text = string.Format(ViewState["crdrBanks"].ToString(), Convert.ToDouble(e.Value));
        }

        protected void ASPxGridView1_SummaryDisplayTextInvestments(object sender, ASPxGridViewSummaryDisplayTextEventArgs e)
        {
            if (e.Item.FieldName == "Credit")
                e.Text = string.Format("{0}", Convert.ToDouble(e.Value));
            if (e.Item.FieldName == "Debit")
                e.Text = string.Format("{0}", Convert.ToDouble(e.Value));
            if (e.Item.FieldName == "Narration")
                e.Text = string.Format(ViewState["crdrInvestments"].ToString(), Convert.ToDouble(e.Value));
        }

        protected void ASPxGridView1_SummaryDisplayTextOtherItems(object sender, ASPxGridViewSummaryDisplayTextEventArgs e)
        {
            if (e.Item.FieldName == "Credit")
                e.Text = string.Format("{0}", Convert.ToDouble(e.Value));
            if (e.Item.FieldName == "Debit")
                e.Text = string.Format("{0}", Convert.ToDouble(e.Value));
            if (e.Item.FieldName == "Narration")
                e.Text = string.Format(ViewState["crdrOtherItems"].ToString(), Convert.ToDouble(e.Value));
        }

        protected void ASPxGridView1_SummaryDisplayTextChits(object sender, ASPxGridViewSummaryDisplayTextEventArgs e)
        {
            if (e.Item.FieldName == "Credit")
                e.Text = string.Format("{0}", Convert.ToDouble(e.Value));
            if (e.Item.FieldName == "Debit")
                e.Text = string.Format("{0}", Convert.ToDouble(e.Value));
            if (e.Item.FieldName == "Narration")
                e.Text = string.Format(ViewState["crdrChits"].ToString(), Convert.ToDouble(e.Value));
        }

        protected void ASPxGridView1_SummaryDisplayTextForemanChits(object sender, ASPxGridViewSummaryDisplayTextEventArgs e)
        {
            if (e.Item.FieldName == "Credit")
                e.Text = string.Format("{0}", Convert.ToDouble(e.Value));
            if (e.Item.FieldName == "Debit")
                e.Text = string.Format("{0}", Convert.ToDouble(e.Value));
            if (e.Item.FieldName == "Narration")
                e.Text = string.Format(ViewState["crdrForemanChits"].ToString(), Convert.ToDouble(e.Value));
        }

        protected void ASPxGridView1_SummaryDisplayTextDecreeDebtors(object sender, ASPxGridViewSummaryDisplayTextEventArgs e)
        {
            if (e.Item.FieldName == "Credit")
                e.Text = string.Format("{0}", Convert.ToDouble(e.Value));
            if (e.Item.FieldName == "Debit")
                e.Text = string.Format("{0}", Convert.ToDouble(e.Value));
            if (e.Item.FieldName == "Narration")
                e.Text = string.Format(ViewState["crdrDecreeDebtors"].ToString(), Convert.ToDouble(e.Value));
        }

        protected void ASPxGridView1_SummaryDisplayTextLoans(object sender, ASPxGridViewSummaryDisplayTextEventArgs e)
        {
            if (e.Item.FieldName == "Credit")
                e.Text = string.Format("{0}", Convert.ToDouble(e.Value));
            if (e.Item.FieldName == "Debit")
                e.Text = string.Format("{0}", Convert.ToDouble(e.Value));
            if (e.Item.FieldName == "Narration")
                e.Text = string.Format(ViewState["crdrLoans"].ToString(), Convert.ToDouble(e.Value));
        }

        protected void ASPxGridView1_SummaryDisplayTextAdvances(object sender, ASPxGridViewSummaryDisplayTextEventArgs e)
        {
            if (e.Item.FieldName == "Credit")
                e.Text = string.Format("{0}", Convert.ToDouble(e.Value));
            if (e.Item.FieldName == "Debit")
                e.Text = string.Format("{0}", Convert.ToDouble(e.Value));
            if (e.Item.FieldName == "Narration")
                e.Text = string.Format(ViewState["crdrAdvances"].ToString(), Convert.ToDouble(e.Value));
        }

        protected void ASPxGridView1_SummaryDisplayTextStamps(object sender, ASPxGridViewSummaryDisplayTextEventArgs e)
        {
            if (e.Item.FieldName == "Credit")
                e.Text = string.Format("{0}", Convert.ToDouble(e.Value));
            if (e.Item.FieldName == "Debit")
                e.Text = string.Format("{0}", Convert.ToDouble(e.Value));
            if (e.Item.FieldName == "Narration")
                e.Text = string.Format(ViewState["crdrStamps"].ToString(), Convert.ToDouble(e.Value));
        }

        protected void ASPxGridView1_SummaryDisplayTextProfitandLoss(object sender, ASPxGridViewSummaryDisplayTextEventArgs e)
        {
            if (e.Item.FieldName == "Credit")
                e.Text = string.Format("{0}", Convert.ToDouble(e.Value));
            if (e.Item.FieldName == "Debit")
                e.Text = string.Format("{0}", Convert.ToDouble(e.Value));
            if (e.Item.FieldName == "Narration")
                e.Text = string.Format(ViewState["crdrProfitandLoss"].ToString(), Convert.ToDouble(e.Value));
        }

        protected void ASPxGridView1_SummaryDisplayTextCash(object sender, ASPxGridViewSummaryDisplayTextEventArgs e)
        {
            if (e.Item.FieldName == "Credit")
                e.Text = string.Format("{0}", Convert.ToDouble(e.Value));
            if (e.Item.FieldName == "Debit")
                e.Text = string.Format("{0}", Convert.ToDouble(e.Value));
            if (e.Item.FieldName == "Narration")
                e.Text = string.Format(ViewState["crdrCash"].ToString(), Convert.ToDouble(e.Value));
        }

        protected void ASPxGridView1_OnCustomSummaryCalculateBranch(object sender, CustomSummaryEventArgs e)
        {
            ASPxSummaryItem credit = (sender as ASPxGridView).TotalSummary["Credit"];
            ASPxSummaryItem debit = (sender as ASPxGridView).TotalSummary["Debit"];
            Decimal income = Convert.ToDecimal(((ASPxGridView)sender).GetTotalSummaryValue(credit));
            Decimal expense = Convert.ToDecimal(((ASPxGridView)sender).GetTotalSummaryValue(debit));
            if (income > expense)
            {
                ViewState["crdrBranch"] = "Cr. ";
                e.TotalValue = income - expense;
            }
            else if (income < expense)
            {
                ViewState["crdrBranch"] = "Dr. ";
                e.TotalValue = expense - income;
            }
            else
            {
                ViewState["crdrBranch"] = "";
                e.TotalValue = 0.00;
            }
            ViewState["crdrBranch"] += e.TotalValue.ToString();
        }

        protected void ASPxGridView1_OnCustomSummaryCalculateInvestment(object sender, CustomSummaryEventArgs e)
        {
            ASPxSummaryItem credit = (sender as ASPxGridView).TotalSummary["Credit"];
            ASPxSummaryItem debit = (sender as ASPxGridView).TotalSummary["Debit"];
            Decimal income = Convert.ToDecimal(((ASPxGridView)sender).GetTotalSummaryValue(credit));
            Decimal expense = Convert.ToDecimal(((ASPxGridView)sender).GetTotalSummaryValue(debit));
            if (income > expense)
            {
                ViewState["crdrInvestments"] = "Cr. ";
                e.TotalValue = income - expense;
            }
            else if (income < expense)
            {
                ViewState["crdrInvestments"] = "Dr. ";
                e.TotalValue = expense - income;
            }
            else
            {
                ViewState["crdrInvestments"] = "";
                e.TotalValue = 0.00;
            }
            ViewState["crdrInvestments"] += e.TotalValue.ToString();
        }

        protected void ASPxGridView1_OnCustomSummaryCalculateOtherItems(object sender, CustomSummaryEventArgs e)
        {
            ASPxSummaryItem credit = (sender as ASPxGridView).TotalSummary["Credit"];
            ASPxSummaryItem debit = (sender as ASPxGridView).TotalSummary["Debit"];
            Decimal income = Convert.ToDecimal(((ASPxGridView)sender).GetTotalSummaryValue(credit));
            Decimal expense = Convert.ToDecimal(((ASPxGridView)sender).GetTotalSummaryValue(debit));
            if (income > expense)
            {
                ViewState["crdrOtherItems"] = "Cr. ";
                e.TotalValue = income - expense;
            }
            else if (income < expense)
            {
                ViewState["crdrOtherItems"] = "Dr. ";
                e.TotalValue = expense - income;
            }
            else
            {
                ViewState["crdrOtherItems"] = "";
                e.TotalValue = 0.00;
            }
            ViewState["crdrOtherItems"] += e.TotalValue.ToString();
        }

        protected void ASPxGridView1_OnCustomSummaryCalculateBanks(object sender, CustomSummaryEventArgs e)
        {
            ASPxSummaryItem credit = (sender as ASPxGridView).TotalSummary["Credit"];
            ASPxSummaryItem debit = (sender as ASPxGridView).TotalSummary["Debit"];
            Decimal income = Convert.ToDecimal(((ASPxGridView)sender).GetTotalSummaryValue(credit));
            Decimal expense = Convert.ToDecimal(((ASPxGridView)sender).GetTotalSummaryValue(debit));
            if (income > expense)
            {
                ViewState["crdrBanks"] = "Cr. ";
                e.TotalValue = income - expense;
            }
            else if (income < expense)
            {
                ViewState["crdrBanks"] = "Dr. ";
                e.TotalValue = expense - income;
            }
            else
            {
                ViewState["crdrBanks"] = "";
                e.TotalValue = 0.00;
            }
            ViewState["crdrBanks"] += e.TotalValue.ToString();
        }

        protected void ASPxGridView1_OnCustomSummaryCalculateChits(object sender, CustomSummaryEventArgs e)
        {
            ASPxSummaryItem credit = (sender as ASPxGridView).TotalSummary["Credit"];
            ASPxSummaryItem debit = (sender as ASPxGridView).TotalSummary["Debit"];
            Decimal income = Convert.ToDecimal(((ASPxGridView)sender).GetTotalSummaryValue(credit));
            Decimal expense = Convert.ToDecimal(((ASPxGridView)sender).GetTotalSummaryValue(debit));
            if (income > expense)
            {
                ViewState["crdrChits"] = "Cr. ";
                e.TotalValue = income - expense;
            }
            else if (income < expense)
            {
                ViewState["crdrChits"] = "Dr. ";
                e.TotalValue = expense - income;
            }
            else
            {
                ViewState["crdrChits"] = "";
                e.TotalValue = 0.00;
            }
            ViewState["crdrChits"] += e.TotalValue.ToString();
        }

        protected void ASPxGridView1_OnCustomSummaryCalculateForemanChits(object sender, CustomSummaryEventArgs e)
        {
            ASPxSummaryItem credit = (sender as ASPxGridView).TotalSummary["Credit"];
            ASPxSummaryItem debit = (sender as ASPxGridView).TotalSummary["Debit"];
            Decimal income = Convert.ToDecimal(((ASPxGridView)sender).GetTotalSummaryValue(credit));
            Decimal expense = Convert.ToDecimal(((ASPxGridView)sender).GetTotalSummaryValue(debit));
            if (income > expense)
            {
                ViewState["crdrForemanChits"] = "Cr. ";
                e.TotalValue = income - expense;
            }
            else if (income < expense)
            {
                ViewState["crdrForemanChits"] = "Dr. ";
                e.TotalValue = expense - income;
            }
            else
            {
                ViewState["crdrForemanChits"] = "";
                e.TotalValue = 0.00;
            }
            ViewState["crdrForemanChits"] += e.TotalValue.ToString();
        }

        protected void ASPxGridView1_OnCustomSummaryCalculateDecreeDebtors(object sender, CustomSummaryEventArgs e)
        {
            ASPxSummaryItem credit = (sender as ASPxGridView).TotalSummary["Credit"];
            ASPxSummaryItem debit = (sender as ASPxGridView).TotalSummary["Debit"];
            Decimal income = Convert.ToDecimal(((ASPxGridView)sender).GetTotalSummaryValue(credit));
            Decimal expense = Convert.ToDecimal(((ASPxGridView)sender).GetTotalSummaryValue(debit));
            if (income > expense)
            {
                ViewState["crdrDecreeDebtors"] = "Cr. ";
                e.TotalValue = income - expense;
            }
            else if (income < expense)
            {
                ViewState["crdrDecreeDebtors"] = "Dr. ";
                e.TotalValue = expense - income;
            }
            else
            {
                ViewState["crdrDecreeDebtors"] = "";
                e.TotalValue = 0.00;
            }
            ViewState["crdrDecreeDebtors"] += e.TotalValue.ToString();
        }

        protected void ASPxGridView1_OnCustomSummaryCalculateLoans(object sender, CustomSummaryEventArgs e)
        {
            ASPxSummaryItem credit = (sender as ASPxGridView).TotalSummary["Credit"];
            ASPxSummaryItem debit = (sender as ASPxGridView).TotalSummary["Debit"];
            Decimal income = Convert.ToDecimal(((ASPxGridView)sender).GetTotalSummaryValue(credit));
            Decimal expense = Convert.ToDecimal(((ASPxGridView)sender).GetTotalSummaryValue(debit));
            if (income > expense)
            {
                ViewState["crdrLoans"] = "Cr. ";
                e.TotalValue = income - expense;
            }
            else if (income < expense)
            {
                ViewState["crdrLoans"] = "Dr. ";
                e.TotalValue = expense - income;
            }
            else
            {
                ViewState["crdrLoans"] = "";
                e.TotalValue = 0.00;
            }
            ViewState["crdrLoans"] += e.TotalValue.ToString();
        }

        protected void ASPxGridView1_OnCustomSummaryCalculateAdvances(object sender, CustomSummaryEventArgs e)
        {
            ASPxSummaryItem credit = (sender as ASPxGridView).TotalSummary["Credit"];
            ASPxSummaryItem debit = (sender as ASPxGridView).TotalSummary["Debit"];
            Decimal income = Convert.ToDecimal(((ASPxGridView)sender).GetTotalSummaryValue(credit));
            Decimal expense = Convert.ToDecimal(((ASPxGridView)sender).GetTotalSummaryValue(debit));
            if (income > expense)
            {
                ViewState["crdrAdvances"] = "Cr. ";
                e.TotalValue = income - expense;
            }
            else if (income < expense)
            {
                ViewState["crdrAdvances"] = "Dr. ";
                e.TotalValue = expense - income;
            }
            else
            {
                ViewState["crdrAdvances"] = "";
                e.TotalValue = 0.00;
            }
            ViewState["crdrAdvances"] += e.TotalValue.ToString();
        }

        protected void ASPxGridView1_OnCustomSummaryCalculateStamps(object sender, CustomSummaryEventArgs e)
        {
            ASPxSummaryItem credit = (sender as ASPxGridView).TotalSummary["Credit"];
            ASPxSummaryItem debit = (sender as ASPxGridView).TotalSummary["Debit"];
            Decimal income = Convert.ToDecimal(((ASPxGridView)sender).GetTotalSummaryValue(credit));
            Decimal expense = Convert.ToDecimal(((ASPxGridView)sender).GetTotalSummaryValue(debit));
            if (income > expense)
            {
                ViewState["crdrStamps"] = "Cr. ";
                e.TotalValue = income - expense;
            }
            else if (income < expense)
            {
                ViewState["crdrStamps"] = "Dr. ";
                e.TotalValue = expense - income;
            }
            else
            {
                ViewState["crdrStamps"] = "";
                e.TotalValue = 0.00;
            }
            ViewState["crdrStamps"] += e.TotalValue.ToString();
        }

        protected void ASPxGridView1_OnCustomSummaryCalculateProfitandLoss(object sender, CustomSummaryEventArgs e)
        {
            ASPxSummaryItem credit = (sender as ASPxGridView).TotalSummary["Credit"];
            ASPxSummaryItem debit = (sender as ASPxGridView).TotalSummary["Debit"];
            Decimal income = Convert.ToDecimal(((ASPxGridView)sender).GetTotalSummaryValue(credit));
            Decimal expense = Convert.ToDecimal(((ASPxGridView)sender).GetTotalSummaryValue(debit));
            if (income > expense)
            {
                ViewState["crdrProfitandLoss"] = "Cr. ";
                e.TotalValue = income - expense;
            }
            else if (income < expense)
            {
                ViewState["crdrProfitandLoss"] = "Dr. ";
                e.TotalValue = expense - income;
            }
            else
            {
                ViewState["crdrProfitandLoss"] = "";
                e.TotalValue = 0.00;
            }
            ViewState["crdrProfitandLoss"] += e.TotalValue.ToString();
        }

        protected void ASPxGridView1_OnCustomSummaryCalculateCash(object sender, CustomSummaryEventArgs e)
        {
            ASPxSummaryItem credit = (sender as ASPxGridView).TotalSummary["Credit"];
            ASPxSummaryItem debit = (sender as ASPxGridView).TotalSummary["Debit"];
            Decimal income = Convert.ToDecimal(((ASPxGridView)sender).GetTotalSummaryValue(credit));
            Decimal expense = Convert.ToDecimal(((ASPxGridView)sender).GetTotalSummaryValue(debit));
            if (income > expense)
            {
                ViewState["crdrCash"] = "Cr. ";
                e.TotalValue = income - expense;
            }
            else if (income < expense)
            {
                ViewState["crdrCash"] = "Dr. ";
                e.TotalValue = expense - income;
            }
            else
            {
                ViewState["crdrCash"] = "";
                e.TotalValue = 0.00;
            }
            ViewState["crdrCash"] += e.TotalValue.ToString();
        }
        protected void grid_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            //ApplyLayout(Int32.Parse(e.Parameters));
        }
        protected void gridBranch_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            //ApplyLayout(Int32.Parse(e.Parameters));
        }

        protected void grid_CustomUnboundColumnData(object sender, ASPxGridViewColumnDataEventArgs e)
        {

        }
        protected void Export_click(object sender, MenuItemEventArgs e)
        {
            if (e.Item.Text.ToString() == "PDF")
            {
                PrintingSystem ps = new PrintingSystem();
                PrintableComponentLink exportBranch = new PrintableComponentLink(ps);
                exportBranch.Component = gridExport;
                exportBranch.PaperKind = System.Drawing.Printing.PaperKind.A3;
                gridExport.PreserveGroupRowStates = true;

                PrintableComponentLink exportInvestment = new PrintableComponentLink(ps);
                exportInvestment.Component = geInvestmentsExport;
                exportInvestment.PaperKind = System.Drawing.Printing.PaperKind.A3;
                geInvestmentsExport.PreserveGroupRowStates = true;

                PrintableComponentLink exportBanks = new PrintableComponentLink(ps);
                exportBanks.Component = geBanksExport;
                exportBanks.PaperKind = System.Drawing.Printing.PaperKind.A3;
                geBanksExport.PreserveGroupRowStates = true;

                PrintableComponentLink exporttherItems = new PrintableComponentLink(ps);
                exporttherItems.Component = geOtherItemsExport;
                exporttherItems.PaperKind = System.Drawing.Printing.PaperKind.A3;
                geOtherItemsExport.PreserveGroupRowStates = true;

                PrintableComponentLink exportChits = new PrintableComponentLink(ps);
                exportChits.Component = geChitsExport;
                exportChits.PaperKind = System.Drawing.Printing.PaperKind.A3;
                geChitsExport.PreserveGroupRowStates = true;

                PrintableComponentLink exportForemanChits = new PrintableComponentLink(ps);
                exportForemanChits.Component = geForemanChitsExport;
                exportForemanChits.PaperKind = System.Drawing.Printing.PaperKind.A3;
                geForemanChitsExport.PreserveGroupRowStates = true;

                PrintableComponentLink exportDecreeDebtors = new PrintableComponentLink(ps);
                exportDecreeDebtors.Component = geDecreeDebtorsExport;
                exportDecreeDebtors.PaperKind = System.Drawing.Printing.PaperKind.A3;
                geDecreeDebtorsExport.PreserveGroupRowStates = true;

                PrintableComponentLink exportLoans = new PrintableComponentLink(ps);
                exportLoans.Component = geLoansExport;
                exportLoans.PaperKind = System.Drawing.Printing.PaperKind.A3;
                geLoansExport.PreserveGroupRowStates = true;

                PrintableComponentLink exportSundriesAndAdvances = new PrintableComponentLink(ps);
                exportSundriesAndAdvances.Component = geSundriesAndAdvancesExport;
                exportSundriesAndAdvances.PaperKind = System.Drawing.Printing.PaperKind.A3;
                geSundriesAndAdvancesExport.PreserveGroupRowStates = true;

                PrintableComponentLink exportStamps = new PrintableComponentLink(ps);
                exportStamps.Component = geStampsExport;
                exportStamps.PaperKind = System.Drawing.Printing.PaperKind.A3;
                geStampsExport.PreserveGroupRowStates = true;

                PrintableComponentLink exportProfitAndLoss = new PrintableComponentLink(ps);
                exportProfitAndLoss.Component = geProfitAndLossExport;
                exportProfitAndLoss.PaperKind = System.Drawing.Printing.PaperKind.A3;
                geProfitAndLossExport.PreserveGroupRowStates = true;

                PrintableComponentLink exportCash = new PrintableComponentLink(ps);
                exportCash.Component = geCashExport;
                exportCash.PaperKind = System.Drawing.Printing.PaperKind.A3;
                geCashExport.PreserveGroupRowStates = true;

                CompositeLink compositeLink = new CompositeLink(ps);
                compositeLink.Links.AddRange(new object[] { exportBranch, exportInvestment, exportBanks, exportChits, exportCash, exportLoans, exportForemanChits, exportDecreeDebtors, exporttherItems, exportProfitAndLoss, exportStamps, exportSundriesAndAdvances });

                using (MemoryStream stream = new MemoryStream())
                {
                    compositeLink.CreateDocument(false);
                    compositeLink.PrintingSystem.Document.AutoFitToPagesWidth = 1;
                    compositeLink.PrintingSystem.ExportToPdf(stream);
                    WriteToResponse("TopColumnDayBook", true, "pdf", stream);
                }
                ps.Dispose();
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
