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
    public partial class DirectForemanChitsLedger : System.Web.UI.Page
    {
        #region VarDeclaration
        BusinessLayer balayer = new BusinessLayer();
        TransactionLayer tranlayer = new TransactionLayer();
        #endregion
        private System.Drawing.Image headerImage;
        VarDeclaration objVar = new VarDeclaration();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

              
                GetGroupData();
                GetDropDown();


                ddlGroup.SelectedIndex = 1;
                dateFromConsolidated.Text = DateTime.Now.ToString("dd/MM/yyyy");
                dateToConsolidated.Text = DateTime.Now.ToString("dd/MM/yyyy");
                select();
              //  select(ddlForman.SelectedIndex);
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

       // select();
            grid.DataSource = select();
            grid.DataBind();
            //    select(ddlForman.SelectedIndex);

        }



        protected void ddlFormannode_OnSelectedIndexChanged(object sender, EventArgs e)
        {

            if (ddlFormannode.SelectedItem.Text != "--select--")
            {
                DataTable dd = balayer.GetDataTable("select distinct t9.Node as degree,t9.NodeID as NodeId, t10.NodeID as nod from voucher as t1   left join headstree as t9 on (t9.NodeID=t1.Head_Id)  left join headstree as t10 on (t10.NodeID=t9.ParentID) where  `t1`.`BranchID` =" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + "   and   t10.NodeID='" + ddlFormannode.SelectedItem.Value + "'    and`t1`.`RootID` = 6 and t1.IsDeleted=0  ;");
                DataRow dri;
                dri = dd.NewRow();
                dri[0] = "--select--";
                dri[1] = "0";
                dd.Rows.InsertAt(dri, 0);
                ddlForman.DataSource = dd;
                ddlForman.DataTextField = "degree";
                ddlForman.DataValueField = "NodeId";
                ddlForman.DataBind();
                foreach (GridViewColumn column in grid.Columns)
                {
                    if (column is GridViewDataColumn)
                    {
                        ((GridViewDataColumn)column).Settings.AutoFilterCondition = AutoFilterCondition.Contains;
                    }
                }
                //ListItem lst1 = new ListItem("--select--");
                //ddlInves.Items.Insert(0, lst1);
                //ddlInves.DataBind();
            }
            else
            {
                ddlForman.DataSource = null;
                ddlForman.Items.Clear();
                ddlForman.Items.Insert(0, "--select--");
                ddlForman.DataBind();
            }

        }

        private void GetGroupData()
        {
            ddlForman.DataSource = null;
            ddlForman.DataBind();
            DataTable Accbank = balayer.GetDataTable("select distinct t3.Node as Forman,t3.NodeID as NodeId from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left join headstree as t8 on (t1.ChitGroupID=t8.NodeID) where `t1`.`BranchID` = " + balayer.ToobjectstrEvenNull(Session["Branchid"]) + "  and `t1`.`RootID` = 6 and t1.IsDeleted=0  order by t3.Node asc;");

            DataRow drinves;
            drinves = Accbank.NewRow();
            drinves[0] = "--select--";
            drinves[1] = "0";
            Accbank.Rows.InsertAt(drinves, 0);
            ddlForman.DataSource = Accbank;
            ddlForman.DataTextField = "Forman";
            ddlForman.DataValueField = "NodeId";
            ddlForman.DataBind();

        }
        private void GetDropDown()
        {
            ddlFormannode.DataSource = null;
            ddlFormannode.DataBind();
            DataTable Accbank1 = balayer.GetDataTable("select distinct t10.Node as Forman,t10.NodeID as NodeId from voucher as t1   left join headstree as t9 on (t9.NodeID=t1.Head_Id) left join headstree as t10 on (t10.NodeID=t9.ParentID) where `t1`.`BranchID` = " + balayer.ToobjectstrEvenNull(Session["Branchid"]) + "  and `t1`.`RootID` = 6 and t1.IsDeleted=0  order by t10.Node asc;");
            DataRow drinvestment;
            drinvestment = Accbank1.NewRow();
            drinvestment[0] = "--select--";
            drinvestment[1] = "0";
            Accbank1.Rows.InsertAt(drinvestment, 0);
            ddlFormannode.DataSource = Accbank1;
            ddlFormannode.DataTextField = "Forman";
            ddlFormannode.DataValueField = "NodeId";
            ddlFormannode.DataBind();
        }
        protected void btnExportExcel_Click1(object sender, EventArgs e)
        {
            gridexcel.FileName = "DirectFormanChitsLedger" + DateTime.Now.Millisecond.ToString();
            grid.DataBind();
            gridexcel.WriteXlsToResponse();
        }
        private DataTable select()
        {
            DataTable finalDt = new DataTable();
            if (ddlForman.SelectedIndex == 0 && ddlFormannode.SelectedIndex == 0)
            {
                string value = ddlForman.SelectedItem.Text;
               // grid.SettingsText.Title = "Trial Balance of Forman from " + dateFromConsolidated.Text + " to " + dateToConsolidated.Text;
              //  AccessDataSource1.ConnectionString = CommonClassFile.ConnectionString;
                objVar.varquery = @"select t1.ChoosenDate as `Date`,t8.Node as 'LedgerHead' ,t3.Node as Token ,t1.Narration,(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )as `Credit`,(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )as `Debit`,t10.Node as 'Node' from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID  " +
                                                   "left join headstree as t8 on (t1.ChitGroupID=t8.NodeID) left join headstree as t9 on (t9.NodeID=t1.Head_Id)" +
                                                   " left join headstree as t10 on (t10.NodeID=t9.ParentID)" +
                                                   " where `t1`.`BranchID` = " + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and `t1`.`RootID` = 6 and t1.ChoosenDate between '" + balayer.indiandateToMysqlDate(dateFromConsolidated.Text) + "' and '" + balayer.indiandateToMysqlDate(dateToConsolidated.Text) + "' and t1.IsDeleted=0 order by t1.ChoosenDate asc";
                finalDt = balayer.GetDataTable(objVar.varquery);

                AccessDataSource2.ConnectionString = CommonClassFile.ConnectionString;
                AccessDataSource2.SelectCommand = @"select t1.ChoosenDate as `Date`,t8.Node as 'LedgerHead' ,t3.Node as Token ,t1.Narration,(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )as `Credit`,(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )as `Debit`,t10.Node as 'Node' from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID  " +
                                                    "left join headstree as t8 on (t1.ChitGroupID=t8.NodeID) left join headstree as t9 on (t9.NodeID=t1.Head_Id)" +
                                                    " left join headstree as t10 on (t10.NodeID=t9.ParentID)" +
                                                   " where `t1`.`BranchID` = " + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and `t1`.`RootID` = 6 and t1.ChoosenDate < '" + balayer.indiandateToMysqlDate(dateFromConsolidated.Text) + "' and t1.IsDeleted=0 order by t1.ChoosenDate asc";
                gridprev.DataBind();
            }
            else if(ddlForman.SelectedIndex!=0 && ddlFormannode.SelectedIndex!=0)
            {
                string value = ddlForman.SelectedItem.Text;
               // grid.SettingsText.Title = "Trial Balance of Forman" + value + "from " + dateFromConsolidated.Text + " to " + dateToConsolidated.Text;
                AccessDataSource1.ConnectionString = CommonClassFile.ConnectionString;
                objVar.varquery = @"select t1.ChoosenDate as `Date`,t8.Node as 'LedgerHead' ,t3.Node as Token ,t1.Narration,(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )as `Credit`,(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )as `Debit`,t10.Node as 'Node' from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID  " +
                                                   "left join headstree as t8 on (t1.ChitGroupID=t8.NodeID) left join headstree as t9 on (t9.NodeID=t1.Head_Id)" +
                                                   " left join headstree as t10 on (t10.NodeID=t9.ParentID)" +
                                                   " where `t1`.`BranchID` = " + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and `t1`.`RootID` = 6 and t1.ChoosenDate between '" + balayer.indiandateToMysqlDate(dateFromConsolidated.Text) + "' and '" + balayer.indiandateToMysqlDate(dateToConsolidated.Text) + "' and t1.IsDeleted=0 and t9.NodeID='" + ddlForman.SelectedItem.Value + "' order by t1.ChoosenDate asc";

                finalDt = balayer.GetDataTable(objVar.varquery);

                //AccessDataSource2.ConnectionString = CommonClassFile.ConnectionString;
                //AccessDataSource2.SelectCommand = @"select t1.ChoosenDate as `Date`,t8.Node as 'LedgerHead' ,t3.Node as Token ,t1.Narration,(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )as `Credit`,(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )as `Debit`,t10.Node as 'Node' from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID  " +
                //                                    "left join headstree as t8 on (t1.ChitGroupID=t8.NodeID) left join headstree as t9 on (t9.NodeID=t1.Head_Id)" +
                //                                    " left join headstree as t10 on (t10.NodeID=t9.ParentID)" +
                //                                   " where `t1`.`BranchID` = " + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and `t1`.`RootID` = 6 and t1.ChoosenDate < '" + balayer.indiandateToMysqlDate(dateFromConsolidated.Text) + "' and t1.IsDeleted=0  and t9.NodeID='" + ddlForman.SelectedItem.Value + "' order by t1.ChoosenDate asc";
                //gridprev.DataBind();
            }
            else
            {
                string value = ddlFormannode.SelectedItem.Text;
                grid.SettingsText.Title = "Trial Balance of Forman" + value + "from " + dateFromConsolidated.Text + " to " + dateToConsolidated.Text;
                AccessDataSource1.ConnectionString = CommonClassFile.ConnectionString;
                objVar.varquery = @"select t1.ChoosenDate as `Date`,t8.Node as 'LedgerHead' ,t3.Node as Token ,t1.Narration,(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )as `Credit`,(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )as `Debit`,t10.Node as 'Node' from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID  " +
                                                   "left join headstree as t8 on (t1.ChitGroupID=t8.NodeID) left join headstree as t9 on (t9.NodeID=t1.Head_Id)" +
                                                   " left join headstree as t10 on (t10.NodeID=t9.ParentID)" +
                                                   " where `t1`.`BranchID` = " + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and `t1`.`RootID` = 6 and t1.ChoosenDate between '" + balayer.indiandateToMysqlDate(dateFromConsolidated.Text) + "' and '" + balayer.indiandateToMysqlDate(dateToConsolidated.Text) + "' and t1.IsDeleted=0 and t10.NodeID='" + ddlFormannode.SelectedItem.Value + "' order by t1.ChoosenDate asc";
                finalDt = balayer.GetDataTable(objVar.varquery);
                //grid.DataBind();
                //AccessDataSource2.ConnectionString = CommonClassFile.ConnectionString;
                //AccessDataSource2.SelectCommand = @"select t1.ChoosenDate as `Date`,t8.Node as 'LedgerHead' ,t3.Node as Token ,t1.Narration,(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )as `Credit`,(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )as `Debit`,t10.Node as 'Node' from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID  " +
                //                                    "left join headstree as t8 on (t1.ChitGroupID=t8.NodeID) left join headstree as t9 on (t9.NodeID=t1.Head_Id)" +
                //                                    " left join headstree as t10 on (t10.NodeID=t9.ParentID)" +
                //                                   " where `t1`.`BranchID` = " + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and `t1`.`RootID` = 6 and t1.ChoosenDate < '" + balayer.indiandateToMysqlDate(dateFromConsolidated.Text) + "' and t1.IsDeleted=0  and t10.NodeID='" + ddlFormannode.SelectedItem.Value + "' order by t1.ChoosenDate asc";
                //gridprev.DataBind();
            }
            LoadGridTemp();
            finalDt.Columns[5].DataType = typeof(decimal);
            finalDt.Columns[4].DataType = typeof(decimal);


            if (objVar.tempDt.Rows.Count > 0)
            {
                foreach (DataRow dr in objVar.tempDt.Rows)
                {
                    DataRow newRow = finalDt.NewRow();
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
        protected void btnLoanConsolidated_Click(object sender, EventArgs e)
        {
            setStats(dateFromConsolidated.Text, dateToConsolidated.Text);
        }
        protected void ResetStats()
        {
            lblCrAmount.Text = "0.00";
            lblCrBalance.Text = "0.00";
            lblDrAmount.Text = "0.00";
            lblDrBalance.Text = "0.00";
        }
        protected void ddlForman_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlForman.SelectedIndex != 0)
            {

               // select(ddlForman.SelectedIndex);

            }
        }
        protected void setStats(string Fromdate, string Todate)
        {
            ResetStats();
            DataTable dtStats = balayer.GetDataTable(" SELECT RootID,sum((case when Voucher_Type='C' then Amount else 0.00 end ))as `Credit Amount`,sum((case when Voucher_Type='D' then Amount else 0.00 end ))as `Debit Amount` FROM `svcf`.`voucher` where BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and ChoosenDate between '" + balayer.indiandateToMysqlDate(Fromdate) + "' and '" + balayer.indiandateToMysqlDate(Todate) + "' and RootID=6 group by RootID");
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

        //protected void select()
        //{
        //    AccessDataSource1.ConnectionString = CommonClassFile.ConnectionString;
        //    AccessDataSource1.SelectCommand = @"select t1.ChoosenDate as `Date`,t8.Node as 'LedgerHead' ,t3.Node as Token ,t1.Narration,(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )as `Credit`,(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )as `Debit`,t10.Node as 'Node' from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID  " +
        //                                       "left join headstree as t8 on (t1.ChitGroupID=t8.NodeID) left join headstree as t9 on (t9.NodeID=t1.Head_Id)" +
        //                                       " left join headstree as t10 on (t10.NodeID=t9.ParentID)" +
        //                                       " where `t1`.`BranchID` = " + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and `t1`.`RootID` = 6 and t1.ChoosenDate between '" + balayer.indiandateToMysqlDate(dateFromConsolidated.Text) + "' and '" + balayer.indiandateToMysqlDate(dateToConsolidated.Text) + "' and t1.IsDeleted=0 order by t1.ChoosenDate asc";
        //    grid.DataBind();
        //    AccessDataSource2.ConnectionString = CommonClassFile.ConnectionString;
        //    AccessDataSource2.SelectCommand = @"select t1.ChoosenDate as `Date`,t8.Node as 'LedgerHead' ,t3.Node as Token ,t1.Narration,(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )as `Credit`,(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )as `Debit`,t10.Node as 'Node' from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID  " +
        //                                        "left join headstree as t8 on (t1.ChitGroupID=t8.NodeID) left join headstree as t9 on (t9.NodeID=t1.Head_Id)" +
        //                                        " left join headstree as t10 on (t10.NodeID=t9.ParentID)" +
        //                                       " where `t1`.`BranchID` = " + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and `t1`.`RootID` = 6 and t1.ChoosenDate < '" + balayer.indiandateToMysqlDate(dateFromConsolidated.Text) + "' and t1.IsDeleted=0 order by t1.ChoosenDate asc";
        //    gridprev.DataBind();
        //}
        public DataTable LoadGridTemp()
        {
            objVar.tempDt = new DataTable();
            if (ddlForman.SelectedIndex == 0 && ddlFormannode.SelectedIndex == 0)
            {
                objVar.varquery = "select 'Previous Net Balance' as 'Title',(case when sum(case when t1.Voucher_Type = 'C' then t1.Amount else 0.00 end ) > sum(case when t1.Voucher_Type = 'D' then t1.Amount else 0.00 end) then CONCAT('Cr.Bal. ', CAST(sum(case when t1.Voucher_Type = 'C' then t1.Amount else 0.00 end) - sum(case when t1.Voucher_Type = 'D' then t1.Amount else 0.00 end)AS char(50))) else  CONCAT('Dr.Bal.', CAST(sum(case when t1.Voucher_Type = 'D' then t1.Amount else 0.00 end) - sum(case when t1.Voucher_Type = 'C' then t1.Amount else 0.00 end)AS char(50)))  end)  as Narration,sum((case when t1.Voucher_Type = 'C' then t1.Amount else 0.00 end))as `Credit`,sum((case when t1.Voucher_Type = 'D' then t1.Amount else 0.00 end))as `Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID  " +
                                                   "left join headstree as t8 on (t1.ChitGroupID=t8.NodeID) left join headstree as t9 on (t9.NodeID=t1.Head_Id)" +
                                                   " left join headstree as t10 on (t10.NodeID=t9.ParentID)" +
                                                  " where `t1`.`BranchID` = " + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and `t1`.`RootID` = 6 and t1.ChoosenDate < '" + balayer.indiandateToMysqlDate(dateFromConsolidated.Text) + "' and t1.IsDeleted=0 order by t1.ChoosenDate asc";
                objVar.tempDt = balayer.GetDataTable(objVar.varquery);
            }
            else if (ddlForman.SelectedIndex != 0 && ddlFormannode.SelectedIndex != 0)
            {
                //  string value = ddlForman.SelectedItem.Text;
                objVar.varquery = @"select 'Previous Net Balance' as 'Title',(case when sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end ) > sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) then CONCAT('Cr.Bal. ',CAST(sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end ) - sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )AS char(50))) else  CONCAT('Dr.Bal.',CAST(sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) - sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )AS char(50)))  end)  as Narration,sum((case when t1.Voucher_Type='C' then t1.Amount else 0.00 end ))as `Credit`,sum((case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ))as `Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID  " +
                                                    "left join headstree as t8 on (t1.ChitGroupID=t8.NodeID) left join headstree as t9 on (t9.NodeID=t1.Head_Id)" +
                                                    " left join headstree as t10 on (t10.NodeID=t9.ParentID)" +
                                                   " where `t1`.`BranchID` = " + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and `t1`.`RootID` = 6 and t1.ChoosenDate < '" + balayer.indiandateToMysqlDate(dateFromConsolidated.Text) + "' and t1.IsDeleted=0  and t9.NodeID='" + ddlForman.SelectedItem.Value + "' and t10.NodeID='" + ddlFormannode.SelectedItem.Value + "' order by t1.ChoosenDate asc";
               // objVar.varquery = "select 'Previous Net Balance' as 'Title',(case when sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end ) > sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) then CONCAT('Cr.Bal. ',CAST(sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end ) - sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )AS char(50))) else  CONCAT('Dr.Bal.',CAST(sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) - sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )AS char(50)))  end)  as Narration,sum((case when t1.Voucher_Type='C' then t1.Amount else 0.00 end ))as `Credit`,sum((case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ))as `Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left join headstree as t8 on (t1.ChitGroupID=t8.NodeID)  where `t1`.`BranchID`=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + "  and `t1`.`RootID` = 6 and t1.ChoosenDate < '" + balayer.indiandateToMysqlDate(dateFromConsolidated.Text) + "' and t1.IsDeleted=0 and t3.NodeID='" + ddlForman.SelectedItem.Value + "' order by t1.ChoosenDate asc;";
                objVar.tempDt = balayer.GetDataTable(objVar.varquery);
            }
            else
            {
                objVar.varquery = @"select 'Previous Net Balance' as 'Title',(case when sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end ) > sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) then CONCAT('Cr.Bal. ',CAST(sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end ) - sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )AS char(50))) else  CONCAT('Dr.Bal.',CAST(sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) - sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )AS char(50)))  end)  as Narration,sum((case when t1.Voucher_Type='C' then t1.Amount else 0.00 end ))as `Credit`,sum((case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ))as `Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID  " +
                                                    "left join headstree as t8 on (t1.ChitGroupID=t8.NodeID) left join headstree as t9 on (t9.NodeID=t1.Head_Id)" +
                                                    " left join headstree as t10 on (t10.NodeID=t9.ParentID)" +
                                                   " where `t1`.`BranchID` = " + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and `t1`.`RootID` = 6 and t1.ChoosenDate < '" + balayer.indiandateToMysqlDate(dateFromConsolidated.Text) + "' and t1.IsDeleted=0  and t10.NodeID='" + ddlFormannode.SelectedItem.Value + "' order by t1.ChoosenDate asc";
               // objVar.varquery = "select 'Previous Net Balance' as 'Title',(case when sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end ) > sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) then CONCAT('Cr.Bal. ',CAST(sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end ) - sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )AS char(50))) else  CONCAT('Dr.Bal.',CAST(sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) - sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )AS char(50)))  end)  as Narration,sum((case when t1.Voucher_Type='C' then t1.Amount else 0.00 end ))as `Credit`,sum((case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ))as `Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left join headstree as t8 on (t1.ChitGroupID=t8.NodeID)  where `t1`.`BranchID`=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + "  and `t1`.`RootID` = 6 and t1.ChoosenDate < '" + balayer.indiandateToMysqlDate(dateFromConsolidated.Text) + "' and t1.IsDeleted=0 order by t1.ChoosenDate asc;";
                objVar.tempDt = balayer.GetDataTable(objVar.varquery);
            }

            return objVar.tempDt;
        }

        protected void BtnStatisticsGo_Click(object sender, EventArgs e)
        {
            setStats(dateFromConsolidated.Text, dateToConsolidated.Text);
            grid.DataSource= select();
            grid.DataBind();
            //   select(ddlForman.SelectedIndex);
            //string value = ddlForman.SelectedItem.Text;
            // grid.SettingsText.Title = "Trial Balance of Forman from " + dateFromConsolidated.Text + " to " + dateToConsolidated.Text;
        }
        protected void ASPxGridView1_SummaryDisplayText(object sender, ASPxGridViewSummaryDisplayTextEventArgs e)
        {
            string parsed = "";
            if (e.Value!=null)
            {
                 parsed = balayer.ConvertToIndCurrency(e.Value.ToString());
            }
            else
            {
                parsed = "0.00";
            }
            if (e.Item.FieldName == "Credit")
            {
                e.Text =  parsed;
            }
            if (e.Item.FieldName == "Debit")
            {
                e.Text = parsed;
            }
            //if (e.Item.FieldName == "Narration")
            //{
            //    e.Text = e.Item.DisplayFormat.ToString() + parsed;
            //}
            if (e.Item.FieldName == "Narration")
            {
                e.Text = e.Item.DisplayFormat.ToString() + parsed;
            }

            if (e.Item.FieldName == "Token")
            {
                e.Text = parsed;
            }
            if (e.Item.FieldName == "Date")
            {
                e.Text = parsed;

            }
        }
        protected void ddlGroup_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlGroup.SelectedIndex == 0)
            {
               select();
             //   select(ddlForman.SelectedIndex);
                ApplyLayout(0);
                grid.TotalSummary["Credit"].SummaryType = SummaryItemType.Sum;
                grid.TotalSummary["Debit"].SummaryType = SummaryItemType.Sum;
                grid.SettingsPager.Mode = GridViewPagerMode.ShowPager;
            }
            else if (ddlGroup.SelectedIndex == 1)
            {
             //   select(ddlForman.SelectedIndex);
                ApplyLayout(1);
                grid.TotalSummary["Credit"].SummaryType = SummaryItemType.Custom;
                grid.TotalSummary["Debit"].SummaryType = SummaryItemType.Custom;
                grid.SettingsPager.Mode = GridViewPagerMode.ShowAllRecords;
            }
            else
            {
                select();
                ApplyLayout(2);
                grid.TotalSummary["Credit"].SummaryType = SummaryItemType.Custom;
                grid.TotalSummary["Debit"].SummaryType = SummaryItemType.Custom;
                grid.SettingsPager.Mode = GridViewPagerMode.ShowAllRecords;
            }
        }
        protected void oncheck_load(object sender, EventArgs e)
        {

        }
        protected virtual string GetLabelText(GridViewGroupRowTemplateContainer container)
        {
            return container.GroupText;
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
                        grid.GroupBy(grid.Columns["Token"]);
                        grid.GroupBy(grid.Columns["Date"]);
                        break;
                    case 2:
                        grid.GroupBy(grid.Columns["Token"]);
                        break;
                }
            }
            finally
            {
                grid.EndUpdate();
            }
            grid.CollapseAll();
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
                                total += Convert.ToDecimal(grid.GetGroupSummaryValue(i, grid.GroupSummary["Token"]));
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
                        var val1 = cre - deb;
                        x.DisplayFormat = "NetBalance Cr";
                        e.TotalValue = val1;
                    }
                    else if (deb > cre)
                    {
                        var val1 = deb - cre;
                        x.DisplayFormat = "NetBalance Dr";
                        e.TotalValue = val1;
                    }
                    else
                    {
                        x.DisplayFormat = "NetBalance ";
                        e.TotalValue = "0.00";
                    }
                    //var iii = "";
                    //if (cre > 0)
                    //{
                    //    objVar.varquery = "select (case when sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end ) > sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) then CONCAT('Cr.Bal- ',CAST(sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end ) - sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )AS char(50))) else  CONCAT('Dr.Bal-',CAST(sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) - sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )AS char(50)))  end)  as Narration from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left join headstree as t8 on (t1.ChitGroupID=t8.NodeID) where `t1`.`BranchID` =  " + balayer.ToobjectstrEvenNull(Session["Branchid"]) + "   and `t1`.`RootID` = 6 and t1.ChoosenDate < '" + balayer.indiandateToMysqlDate(dateFromConsolidated.Text) + "' and t1.IsDeleted=0 order by t1.ChoosenDate asc;";
                    //    ion = balayer.GetSingleValue(objVar.varquery);
                    //    //  iii = ion.Split('-')[1];

                    //    if (ddlForman.SelectedIndex != 0)
                    //    {
                    //        string value = ddlForman.SelectedItem.Text;

                    //        objVar.varquery = "select (case when sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end ) > sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) then CONCAT('Cr.Bal- ',CAST(sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end ) - sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )AS char(50))) else  CONCAT('Dr.Bal-',CAST(sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) - sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )AS char(50)))  end)  as Narration from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left join headstree as t8 on (t1.ChitGroupID=t8.NodeID) where `t1`.`BranchID` =  " + balayer.ToobjectstrEvenNull(Session["Branchid"]) + "   and `t1`.`RootID` = 6 and t1.ChoosenDate < '" + balayer.indiandateToMysqlDate(dateFromConsolidated.Text) + "' and t1.IsDeleted=0 and t3.NodeID='" + ddlForman.SelectedItem.Value + "' order by t1.ChoosenDate asc;";
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
                    //        objVar.varquery = "select (case when sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end ) > sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) then CONCAT('Cr.Bal- ',CAST(sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end ) - sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )AS char(50))) else  CONCAT('Dr.Bal-',CAST(sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) - sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )AS char(50)))  end)  as Narration from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left join headstree as t8 on (t1.ChitGroupID=t8.NodeID) where `t1`.`BranchID` =  " + balayer.ToobjectstrEvenNull(Session["Branchid"]) + "   and `t1`.`RootID` = 6 and t1.ChoosenDate < '" + balayer.indiandateToMysqlDate(dateFromConsolidated.Text) + "' and t1.IsDeleted=0 order by t1.ChoosenDate asc;";
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
                    //    objVar.varquery = "select (case when sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end ) > sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) then CONCAT('Cr.Bal- ',CAST(sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end ) - sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )AS char(50))) else  CONCAT('Dr.Bal-',CAST(sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) - sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )AS char(50)))  end)  as Narration from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left join headstree as t8 on (t1.ChitGroupID=t8.NodeID) where `t1`.`BranchID` =  " + balayer.ToobjectstrEvenNull(Session["Branchid"]) + "   and `t1`.`RootID` = 6 and t1.ChoosenDate < '" + balayer.indiandateToMysqlDate(dateFromConsolidated.Text) + "' and t1.IsDeleted=0 order by t1.ChoosenDate asc;";
                    //    ion = balayer.GetSingleValue(objVar.varquery);
                    //    //  iii = ion.Split('-')[1];

                    //    if (ddlForman.SelectedIndex != 0)
                    //    {
                    //        string value = ddlForman.SelectedItem.Text;

                    //        objVar.varquery = "select (case when sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end ) > sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) then CONCAT('Cr.Bal- ',CAST(sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end ) - sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )AS char(50))) else  CONCAT('Dr.Bal-',CAST(sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) - sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )AS char(50)))  end)  as Narration from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left join headstree as t8 on (t1.ChitGroupID=t8.NodeID) where `t1`.`BranchID` =  " + balayer.ToobjectstrEvenNull(Session["Branchid"]) + "   and `t1`.`RootID` = 6 and t1.ChoosenDate < '" + balayer.indiandateToMysqlDate(dateFromConsolidated.Text) + "' and t1.IsDeleted=0 and t3.NodeID='" + ddlForman.SelectedItem.Value + "' order by t1.ChoosenDate asc;";
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
                    //        objVar.varquery = "select (case when sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end ) > sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) then CONCAT('Cr.Bal- ',CAST(sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end ) - sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )AS char(50))) else  CONCAT('Dr.Bal-',CAST(sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) - sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )AS char(50)))  end)  as Narration from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left join headstree as t8 on (t1.ChitGroupID=t8.NodeID) where `t1`.`BranchID` =  " + balayer.ToobjectstrEvenNull(Session["Branchid"]) + "   and `t1`.`RootID` = 6 and t1.ChoosenDate < '" + balayer.indiandateToMysqlDate(dateFromConsolidated.Text) + "' and t1.IsDeleted=0 order by t1.ChoosenDate asc;";
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
                    //        //x.DisplayFormat = "Cr.Bal.";
                    //        x.DisplayFormat = "NetBalance Cr";
                    //        e.TotalValue = val + val1;
                    //    }

                    //    else if (ion.Contains("Dr"))
                    //    {
                    //        decimal val = Convert.ToDecimal(iii);
                    //        decimal val1 = cre - deb;
                    //        if (val > val1)
                    //        {
                    //            x.DisplayFormat = "NetBalance Dr";
                    //            e.TotalValue = val - val1;
                    //        }
                    //        else if (val < val1)
                    //        {
                    //            x.DisplayFormat = "NetBalance Cr";
                    //            e.TotalValue = val1 - val;
                    //        }
                    //        else
                    //        {
                    //            e.TotalValue = 0.00;
                    //        }

                    //    }
                    //    else if (ion == "")
                    //    {
                    //        var val1 = cre - deb;
                    //        x.DisplayFormat = "Cr.Bal.";
                    //        e.TotalValue = val1;
                    //    }



                    //    e.TotalValueReady = true;

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

                    //    else if (ion.Contains("Cr"))
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
                    //        else
                    //        {
                    //            e.TotalValue = 0.00;
                    //        }


                    //    }
                    //    else if (ion == "")
                    //    {
                    //        var val1 = deb - cre;
                    //        x.DisplayFormat = "Dr.Bal.";
                    //        e.TotalValue = val1;
                    //    }

                    //    e.TotalValueReady = true;
                    //}
                    //else
                    //{
                    //    e.TotalValue = 0.00;
                    //    e.TotalValueReady = true;

                    //}
                    //if (cre > deb)
                    //{

                    //    x.DisplayFormat = "Cr.Bal.";
                    //    e.TotalValue = cre - deb;
                    //    e.TotalValueReady = true;
                    //}
                    //else if (cre < deb)
                    //{
                    //    x.DisplayFormat = "Dr.Bal.";
                    //    e.TotalValue = deb - cre;
                    //    e.TotalValueReady = true;
                    //}
                    //else
                    //{
                    //    e.TotalValue = 0.00;
                    //    e.TotalValueReady = true;
                    //}
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
                if (x.FieldName.ToString() == "Token")
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


        protected void grid_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            //ApplyLayout(Int32.Parse(e.Parameters));
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

                    //grdTemp.DataSource = LoadGridTemp();
                    //grdTemp.DataBind();
                    //grdTemp.Visible = true;

                    grdTemp.Visible = false;

                    grid.DataSource = select();
                    grid.SettingsText.Title = "";
                    grid.DataBind();

                    gridchequeprev.Component = gridExportprev;
                    gridExport.PreserveGroupRowStates = true;
                    gridExportprev.PreserveGroupRowStates = true;
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
                        WriteToResponse("ForemanChitLedger", true, "pdf", stream);
                    }
                }
            }
            else if (e.Item.Text.ToString() == "XLSX")
            {
                gridExport.PreserveGroupRowStates = true;
                gridExport.WriteXlsxToResponse();
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
         //   tb.Rect = new RectangleF(50, 8, (e.Graph.ClientPageSize.Width / 2), 20);
            tb.Rect = new RectangleF(200, 8, (e.Graph.ClientPageSize.Width / 2), 20);
            tb.BorderWidth = 0;
            tb.BackColor = Color.Transparent;
            tb.HorzAlignment = DevExpress.Utils.HorzAlignment.Center;
            tb.VertAlignment = DevExpress.Utils.VertAlignment.Top;
            e.Graph.DrawBrick(tb);
            TextBrick tb1 = new TextBrick();
            tb1.Text = "Branch : " + Session["BranchName"];
            tb1.Font = new Font("Arial", 9);
           // tb1.Rect = new RectangleF(50, 28, (e.Graph.ClientPageSize.Width / 2), 20);
            tb1.Rect = new RectangleF(200, 32, (e.Graph.ClientPageSize.Width / 2), 20);
            tb1.BorderWidth = 0;
            tb1.BackColor = Color.Transparent;
            tb1.HorzAlignment = DevExpress.Utils.HorzAlignment.Center;
            tb1.VertAlignment = DevExpress.Utils.VertAlignment.Top;
            e.Graph.DrawBrick(tb1);


            if (ddlFormannode.SelectedItem.Text == "--select--" && ddlForman.SelectedItem.Text == "--select--")
            {
                TextBrick tb10 = new TextBrick();
                tb10.Text = "Company Chit";
                tb10.Font = new Font("Arial", 9);
                //tb2.Rect = new RectangleF(50, 52,624, 20);
                tb10.Rect = new RectangleF(175, 48, (e.Graph.ClientPageSize.Width / 2), 20);
                tb10.BorderWidth = 0;
                tb10.BackColor = Color.Transparent;
                tb10.HorzAlignment = DevExpress.Utils.HorzAlignment.Center;
                tb10.VertAlignment = DevExpress.Utils.VertAlignment.Top;
                e.Graph.DrawBrick(tb10);

                   ddlForman.SelectedItem.Text = "";
                TextBrick tb4 = new TextBrick();
                tb4.Text = " From " + dateFromConsolidated.Text + " To " + dateToConsolidated.Text;
                tb4.Font = new Font("Arial", 9);
                // tb3.Rect = new RectangleF(50, 52, 624, 20);
                tb4.Rect = new RectangleF(200, 78, (e.Graph.ClientPageSize.Width / 2), 20);
                tb4.BorderWidth = 0;
                tb4.BackColor = Color.Transparent;
                tb4.HorzAlignment = DevExpress.Utils.HorzAlignment.Center;
                tb4.VertAlignment = DevExpress.Utils.VertAlignment.Top;
                e.Graph.DrawBrick(tb4);
            }
            else if (ddlFormannode.SelectedIndex != 0 && ddlForman.SelectedIndex != 0)
            {
                TextBrick tb2 = new TextBrick();
                tb2.Text = "Company Chit" + "-" + ddlFormannode.SelectedItem.Text + "-" + ddlForman.SelectedItem.Text;
                tb2.Font = new Font("Arial", 9);
                //tb2.Rect = new RectangleF(50, 52,624, 20);
                tb2.Rect = new RectangleF(200, 68, (e.Graph.ClientPageSize.Width / 1), 10);
                tb2.BorderWidth = 0;
                tb2.BackColor = Color.Transparent;
                tb2.HorzAlignment = DevExpress.Utils.HorzAlignment.Default;
                tb2.VertAlignment = DevExpress.Utils.VertAlignment.Top;
                e.Graph.DrawBrick(tb2);

                TextBrick tb3 = new TextBrick();
                tb3.Text = " From " + dateFromConsolidated.Text + " To " + dateToConsolidated.Text;
                tb3.Font = new Font("Arial", 9);
                // tb3.Rect = new RectangleF(50, 52, 624, 20);
                tb3.Rect = new RectangleF(200, 78, (e.Graph.ClientPageSize.Width / 2), 20);
                tb3.BorderWidth = 0;
                tb3.BackColor = Color.Transparent;
                tb3.HorzAlignment = DevExpress.Utils.HorzAlignment.Center;
                tb3.VertAlignment = DevExpress.Utils.VertAlignment.Top;
                e.Graph.DrawBrick(tb3);
            }
            else 
            {
                TextBrick tb7 = new TextBrick();
                tb7.Text = "Company Chit" + "-" + ddlFormannode.SelectedItem.Text;
                tb7.Font = new Font("Arial", 9);
                //tb2.Rect = new RectangleF(50, 52,624, 20);
                tb7.Rect = new RectangleF(50, 48, (e.Graph.ClientPageSize.Width / 1), 20);
                tb7.BorderWidth = 0;
                tb7.BackColor = Color.Transparent;
                tb7.HorzAlignment = DevExpress.Utils.HorzAlignment.Center;
                tb7.VertAlignment = DevExpress.Utils.VertAlignment.Top;
                e.Graph.DrawBrick(tb7);

                ddlFormannode.SelectedItem.Text = "";
                TextBrick tb8 = new TextBrick();
                tb8.Text = " From " + dateFromConsolidated.Text + " To " + dateToConsolidated.Text;
                tb8.Font = new Font("Arial", 9);
                // tb3.Rect = new RectangleF(50, 52, 624, 20);
                tb8.Rect = new RectangleF(200, 78, (e.Graph.ClientPageSize.Width / 2), 20);
                tb8.BorderWidth = 0;
                tb8.BackColor = Color.Transparent;
                tb8.HorzAlignment = DevExpress.Utils.HorzAlignment.Center;
                tb8.VertAlignment = DevExpress.Utils.VertAlignment.Top;
                e.Graph.DrawBrick(tb8);
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
                    decimal Credit1 = Convert.ToDecimal(e.GetGroupSummary(e.GroupRowHandle, gridprev.GroupSummary["Credit"]));
                    decimal Debit1 = Convert.ToDecimal(e.GetGroupSummary(e.GroupRowHandle, gridprev.GroupSummary["Debit"]));
                    if (Credit1 <= Debit1)
                    {
                        e.TotalValue = Debit1 - Credit1;
                    }
                }
                if (x.FieldName.ToString() == "Token")
                {
                    decimal Credit1 = Convert.ToDecimal(e.GetGroupSummary(e.GroupRowHandle, gridprev.GroupSummary["Credit"]));
                    decimal Debit1 = Convert.ToDecimal(e.GetGroupSummary(e.GroupRowHandle, gridprev.GroupSummary["Debit"]));
                    if (Credit1 >= Debit1)
                    {
                        e.TotalValue = Credit1 - Debit1;
                    }
                }
            }
        }
    }
}
