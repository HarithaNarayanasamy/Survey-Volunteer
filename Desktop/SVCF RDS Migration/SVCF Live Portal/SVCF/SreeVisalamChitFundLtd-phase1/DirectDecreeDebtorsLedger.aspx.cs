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
using DevExpress.XtraPrinting;
using DevExpress.XtraPrintingLinks;
using System.Drawing;
using SVCF_BusinessAccessLayer;
using SVCF_DataAccessLayer;
using SVCF_TransactionLayer;
using MySql.Data.MySqlClient;

namespace SreeVisalamChitFundLtd_phase1
{
    public partial class DirectDecreeDebtorsLedger : System.Web.UI.Page
    {
        #region Object
        BusinessLayer balayer = new BusinessLayer();
        TransactionLayer trn = new TransactionLayer();
        #endregion
        string branchids ;
        private System.Drawing.Image headerImage;
        BusinessLayer objBAL = new BusinessLayer();
        VarDeclaration objVar = new VarDeclaration();
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                GetGroupData();
                GetDropDown();
                LoadbranchList();
                ddlGroup.SelectedIndex = 1;
                //chkBox.Checked = true;

                dateFromConsolidated.Text = DateTime.Now.ToString("dd/MM/yyyy");
                dateToConsolidated.Text = DateTime.Now.ToString("dd/MM/yyyy");

                //select();
                grid.DataSource = LoadGrid();
                grid.DataBind();

                ApplyLayout(1);
                setStats(dateFromConsolidated.Text, dateToConsolidated.Text);
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

            //select();
            grid.DataSource = LoadGrid();
            grid.DataBind();


        }
        public void LoadbranchList()
        {
            if (balayer.ToobjectstrEvenNull(Session["Branchid"]) == "161")
            {
                drpdownBranchlist.DataSource = null;
                DataTable dtgroupno = null;
                dtgroupno = objBAL.GetDataTable("select NodeID,Node from headstree where ParentID=1");
                DataRow dr = dtgroupno.NewRow();
                dr[0] = "0";
                dr[1] = "--Select--";
                drpdownBranchlist.DataValueField = "NodeID";
                drpdownBranchlist.DataTextField = "Node";
                dtgroupno.Rows.InsertAt(dr, 0);
                drpdownBranchlist.DataSource = dtgroupno;
                drpdownBranchlist.DataBind();
                drpdownBranchlist.Visible = true;
            }
            else
            {
                drpdownBranchlist.DataSource = null;
                DataTable dtgroupno = null;
                dtgroupno = objBAL.GetDataTable("select NodeID,Node from headstree where NodeID='" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + "'");
                DataRow dr = dtgroupno.NewRow();
                dr[0] = "0";
                dr[1] = "--Select--";
                drpdownBranchlist.DataValueField = "NodeID";
                drpdownBranchlist.DataTextField = "Node";
                dtgroupno.Rows.InsertAt(dr, 0);
                drpdownBranchlist.DataSource = dtgroupno;
                drpdownBranchlist.DataBind();
                drpdownBranchlist.Visible = false;
            }
        }
        private void GetGroupData()
        {
            ddldegree.DataSource = null;

                branchids = balayer.ToobjectstrEvenNull(Session["Branchid"]);
            
            ddldegree.DataBind();
            DataTable Accbank = balayer.GetDataTable("select distinct t3.Node as Degree,t3.NodeID as NodeId from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left join headstree as t8 on (t1.ChitGroupID=t8.NodeID) where `t1`.`BranchID` = " + branchids + "  and `t1`.`RootID` = 7 and t1.IsDeleted=0  order by t3.Node asc;");

            DataRow drinves;
            drinves = Accbank.NewRow();
            drinves[0] = "--select--";
            drinves[1] = "0";
            Accbank.Rows.InsertAt(drinves, 0);
            ddldegree.DataSource = Accbank;
            ddldegree.DataTextField = "Degree";
            ddldegree.DataValueField = "NodeId";
            ddldegree.DataBind();

        }
        private void GetDropDown()
        {

                branchids = balayer.ToobjectstrEvenNull(Session["Branchid"]);
            
            ddldegreenode.DataSource = null;
            ddldegreenode.DataBind();
            DataTable Accbank1 = balayer.GetDataTable("select distinct t10.Node as Degree,t10.NodeID as NodeId from voucher as t1   left join headstree as t9 on (t9.NodeID=t1.Head_Id) left join headstree as t10 on (t10.NodeID=t9.ParentID) where `t1`.`BranchID` = " + branchids + "  and `t1`.`RootID` = 7 and t1.IsDeleted=0  order by t10.Node asc;");
            DataRow drinvestment;
            drinvestment = Accbank1.NewRow();
            drinvestment[0] = "--select--";
            drinvestment[1] = "0";
            Accbank1.Rows.InsertAt(drinvestment, 0);
            ddldegreenode.DataSource = Accbank1;
            ddldegreenode.DataTextField = "Degree";
            ddldegreenode.DataValueField = "NodeId";
            ddldegreenode.DataBind();
        }
        protected void ddldegreenode_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (balayer.ToobjectstrEvenNull(Session["Branchid"]) == "161")
            {
                branchids = drpdownBranchlist.SelectedItem.Value;
            }
            else
            {
                branchids = balayer.ToobjectstrEvenNull(Session["Branchid"]);
            }
            if (ddldegreenode.SelectedItem.Text != "--select--")
            {
                DataTable dd = balayer.GetDataTable("select distinct t9.Node as degree,t9.NodeID as NodeId, t10.NodeID as nod from voucher as t1   left join headstree as t9 on (t9.NodeID=t1.Head_Id)  left join headstree as t10 on (t10.NodeID=t9.ParentID) where  `t1`.`BranchID` =" + branchids + "   and   t10.NodeID='" + ddldegreenode.SelectedItem.Value + "'    and`t1`.`RootID` = 7 and t1.IsDeleted=0  ;");
                DataRow dri;
                dri = dd.NewRow();
                dri[0] = "--select--";
                dri[1] = "0";
                dd.Rows.InsertAt(dri, 0);
                ddldegree.DataSource = dd;
                ddldegree.DataTextField = "degree";
                ddldegree.DataValueField = "NodeId";
                ddldegree.DataBind();
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
                ddldegree.DataSource = null;
                ddldegree.Items.Clear();
                ddldegree.Items.Insert(0,"--select--");
                ddldegree.DataBind();
            }

        }
        private void select()
        {
            if(balayer.ToobjectstrEvenNull(Session["Branchid"]) == "161")
            {
                branchids = drpdownBranchlist.SelectedItem.Value;
            }
            else
            {
                branchids = balayer.ToobjectstrEvenNull(Session["Branchid"]);
            }
            if (ddldegree.SelectedItem.Text == "--select--" && ddldegreenode.SelectedItem.Text == "--select--")
            {
                AccessDataSource1.ConnectionString = CommonClassFile.ConnectionString;
                AccessDataSource1.SelectCommand = @"select t1.ChoosenDate as `Date`,t3.Node as Head , t1.Narration,(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )as `Credit`,(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )as `Debit`,t10.Node as 'Node' from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left join headstree as t8 on (t1.ChitGroupID=t8.NodeID) " +
                    "left join headstree as t9 on (t9.NodeID=t1.Head_Id) " +
                    "left join headstree as t10 on (t10.NodeID=t9.ParentID) " +
                    "where  `t1`.`BranchID` = " + branchids + " and `t1`.`RootID` = 7 and t1.ChoosenDate between '" + balayer.indiandateToMysqlDate(dateFromConsolidated.Text) + "' and '" + balayer.indiandateToMysqlDate(dateToConsolidated.Text) + "' and t1.IsDeleted=0 order by t1.ChoosenDate asc";
                grid.DataBind();
                AccessDataSource2.ConnectionString = CommonClassFile.ConnectionString;
                AccessDataSource2.SelectCommand = @"select t1.ChoosenDate as `Date`,t3.Node as Head , t1.Narration,(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )as `Credit`,(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )as `Debit`,t10.Node as 'Node' from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left join headstree as t8 on (t1.ChitGroupID=t8.NodeID) " +
                    "left join headstree as t9 on (t9.NodeID=t1.Head_Id) " +
                    "left join headstree as t10 on (t10.NodeID=t9.ParentID) " +
                    "where  `t1`.`BranchID` = " + branchids + " and `t1`.`RootID` = 7 and t1.ChoosenDate < '" + balayer.indiandateToMysqlDate(dateFromConsolidated.Text) + "' and t1.IsDeleted=0 order by t1.ChoosenDate asc";
                gridprev.DataBind();
            }
            else if(ddldegree.SelectedIndex!=0 && ddldegreenode.SelectedIndex!=0)
            {
                string value = ddldegree.SelectedItem.Text;
                setStats(dateFromConsolidated.Text, dateToConsolidated.Text);
                grid.SettingsText.Title = "Trial Balance of Degree Debtors" + value + "from " + dateFromConsolidated.Text + " to " + dateToConsolidated.Text;
                AccessDataSource1.ConnectionString = CommonClassFile.ConnectionString;
                AccessDataSource1.SelectCommand = @"select t1.ChoosenDate as `Date`,t3.Node as Head , t1.Narration,(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )as `Credit`,(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )as `Debit`,t10.Node as 'Node' from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left join headstree as t8 on (t1.ChitGroupID=t8.NodeID) " +
                    "left join headstree as t9 on (t9.NodeID=t1.Head_Id) " +
                    "left join headstree as t10 on (t10.NodeID=t9.ParentID) " +
                    "where  `t1`.`BranchID` = " + branchids + " and `t1`.`RootID` = 7 and t1.ChoosenDate between '" + balayer.indiandateToMysqlDate(dateFromConsolidated.Text) + "' and '" + balayer.indiandateToMysqlDate(dateToConsolidated.Text) + "' and t1.IsDeleted=0 and t9.NodeID='" + ddldegree.SelectedItem.Value + "' order by t1.ChoosenDate asc";
                grid.DataBind();
                AccessDataSource2.ConnectionString = CommonClassFile.ConnectionString;
                AccessDataSource2.SelectCommand = @"select t1.ChoosenDate as `Date`,t3.Node as Head , t1.Narration,(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )as `Credit`,(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )as `Debit`,t10.Node as 'Node' from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left join headstree as t8 on (t1.ChitGroupID=t8.NodeID) " +
                    "left join headstree as t9 on (t9.NodeID=t1.Head_Id) " +
                    "left join headstree as t10 on (t10.NodeID=t9.ParentID) " +
                    "where  `t1`.`BranchID` = " + branchids + " and `t1`.`RootID` = 7 and t1.ChoosenDate < '" + balayer.indiandateToMysqlDate(dateFromConsolidated.Text) + "' and t1.IsDeleted=0 and t9.NodeID='" + ddldegree.SelectedItem.Value + "' order by t1.ChoosenDate asc";
                gridprev.DataBind();
            }
            else
            {
                string value = ddldegreenode.SelectedItem.Text;
                grid.SettingsText.Title = "Trial Balance of Degree Debtors" + value + "from " + dateFromConsolidated.Text + " to " + dateToConsolidated.Text;
                AccessDataSource1.ConnectionString = CommonClassFile.ConnectionString;
                AccessDataSource1.SelectCommand = @"select t1.ChoosenDate as `Date`,t3.Node as Head , t1.Narration,(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )as `Credit`,(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )as `Debit`,t10.Node as 'Node' from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left join headstree as t8 on (t1.ChitGroupID=t8.NodeID) " +
                    "left join headstree as t9 on (t9.NodeID=t1.Head_Id) " +
                    "left join headstree as t10 on (t10.NodeID=t9.ParentID) " +
                    "where  `t1`.`BranchID` = " + branchids + " and `t1`.`RootID` = 7 and t1.ChoosenDate between '" + balayer.indiandateToMysqlDate(dateFromConsolidated.Text) + "' and '" + balayer.indiandateToMysqlDate(dateToConsolidated.Text) + "' and t1.IsDeleted=0 and t10.NodeID='" + ddldegreenode.SelectedItem.Value + "' order by t1.ChoosenDate asc";
                grid.DataBind();
                AccessDataSource2.ConnectionString = CommonClassFile.ConnectionString;
                AccessDataSource2.SelectCommand = @"select t1.ChoosenDate as `Date`,t3.Node as Head , t1.Narration,(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )as `Credit`,(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )as `Debit`,t10.Node as 'Node' from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left join headstree as t8 on (t1.ChitGroupID=t8.NodeID) " +
                    "left join headstree as t9 on (t9.NodeID=t1.Head_Id) " +
                    "left join headstree as t10 on (t10.NodeID=t9.ParentID) " +
                    "where  `t1`.`BranchID` = " + branchids + " and `t1`.`RootID` = 7 and t1.ChoosenDate < '" + balayer.indiandateToMysqlDate(dateFromConsolidated.Text) + "' and t1.IsDeleted=0 and t10.NodeID='" + ddldegreenode.SelectedItem.Value + "' order by t1.ChoosenDate asc";
                gridprev.DataBind();
            }
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
        protected void ddldegree_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddldegree.SelectedIndex != 0)
            {

              //  select(ddldegree.SelectedIndex);

            }
        }
        protected void btnExportExcel_Click1(object sender, EventArgs e)
        {
            gridexcel.FileName = "DirectDecreeDebtorsLedger" + DateTime.Now.Millisecond.ToString();
            grid.DataSource = LoadGrid();
            grid.DataBind();
            gridexcel.WriteXlsToResponse();
        }
        protected void setStats(string Fromdate, string Todate)
        {
            ResetStats();
            DataTable dtStats = balayer.GetDataTable(" SELECT RootID,sum((case when Voucher_Type='C' then Amount else 0.00 end ))as `Credit Amount`,sum((case when Voucher_Type='D' then Amount else 0.00 end ))as `Debit Amount` FROM `svcf`.`voucher` where BranchID=" + branchids + " and ChoosenDate between '" + balayer.indiandateToMysqlDate(Fromdate) + "' and '" + balayer.indiandateToMysqlDate(Todate) + "' and RootID=7 group by RootID");
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
        //    AccessDataSource1.SelectCommand = @"select t1.ChoosenDate as `Date`,t3.Node as Head , t1.Narration,(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )as `Credit`,(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )as `Debit`,t10.Node as 'Node' from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left join headstree as t8 on (t1.ChitGroupID=t8.NodeID) " +
        //        "left join headstree as t9 on (t9.NodeID=t1.Head_Id) "+
        //        "left join headstree as t10 on (t10.NodeID=t9.ParentID) "+
        //        "where  `t1`.`BranchID` = " + branchids + " and `t1`.`RootID` = 7 and t1.ChoosenDate between '" + balayer.indiandateToMysqlDate(dateFromConsolidated.Text) + "' and '" + balayer.indiandateToMysqlDate(dateToConsolidated.Text) + "' and t1.IsDeleted=0 order by t1.ChoosenDate asc";
        //    grid.DataBind();
        //    AccessDataSource2.ConnectionString = CommonClassFile.ConnectionString;
        //    AccessDataSource2.SelectCommand = @"select t1.ChoosenDate as `Date`,t3.Node as Head , t1.Narration,(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )as `Credit`,(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )as `Debit`,t10.Node as 'Node' from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left join headstree as t8 on (t1.ChitGroupID=t8.NodeID) " +
        //        "left join headstree as t9 on (t9.NodeID=t1.Head_Id) "+
        //        "left join headstree as t10 on (t10.NodeID=t9.ParentID) "+
        //        "where  `t1`.`BranchID` = " + branchids + " and `t1`.`RootID` = 7 and t1.ChoosenDate < '" + balayer.indiandateToMysqlDate(dateFromConsolidated.Text) + "' and t1.IsDeleted=0 order by t1.ChoosenDate asc";
        //    gridprev.DataBind();
        //}



        public DataTable LoadGrid()
        {
            string qry = "";
            DataTable finalDt = new DataTable();
            if (balayer.ToobjectstrEvenNull(Session["Branchid"]) == "161")
            {
                branchids = drpdownBranchlist.SelectedItem.Value;
            }
            else
            {
                branchids = balayer.ToobjectstrEvenNull(Session["Branchid"]);
            }
            if (ddldegree.SelectedItem.Text == "--select--" && ddldegreenode.SelectedItem.Text == "--select--")
            {
                qry = @"select t1.ChoosenDate as `Date`,t3.Node as Head , t1.Narration,(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )as `Credit`,(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )as `Debit`,t10.Node as 'Node' from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left join headstree as t8 on (t1.ChitGroupID=t8.NodeID) " +
                    "left join headstree as t9 on (t9.NodeID=t1.Head_Id) " +
                    "left join headstree as t10 on (t10.NodeID=t9.ParentID) " +
                    "where  `t1`.`BranchID` = " + branchids + " and `t1`.`RootID` = 7 and t1.ChoosenDate between '" + balayer.indiandateToMysqlDate(dateFromConsolidated.Text) + "' and '" + balayer.indiandateToMysqlDate(dateToConsolidated.Text) + "' and t1.IsDeleted=0 order by t1.ChoosenDate asc";
                finalDt = balayer.GetDataTable(qry);

            }
            else if (ddldegree.SelectedIndex != 0 && ddldegreenode.SelectedIndex != 0)
            {
                string value = ddldegree.SelectedItem.Text;

                setStats(dateFromConsolidated.Text, dateToConsolidated.Text);

                grid.SettingsText.Title = "Trial Balance of Degree Debtors" + value + "from " + dateFromConsolidated.Text + " to " + dateToConsolidated.Text;

                qry = @"select t1.ChoosenDate as `Date`,t3.Node as Head , t1.Narration,(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )as `Credit`,(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )as `Debit`,t10.Node as 'Node' from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left join headstree as t8 on (t1.ChitGroupID=t8.NodeID) " +
                    "left join headstree as t9 on (t9.NodeID=t1.Head_Id) " +
                    "left join headstree as t10 on (t10.NodeID=t9.ParentID) " +
                    "where  `t1`.`BranchID` = " + branchids + " and `t1`.`RootID` = 7 and t1.ChoosenDate between '" + balayer.indiandateToMysqlDate(dateFromConsolidated.Text) + "' and '" + balayer.indiandateToMysqlDate(dateToConsolidated.Text) + "' and t1.IsDeleted=0 and t9.NodeID='" + ddldegree.SelectedItem.Value + "' and t10.NodeID='" + ddldegreenode.SelectedItem.Value + "' order by t1.ChoosenDate asc";
                finalDt = balayer.GetDataTable(qry);
            }
            else
            {
                string value = ddldegreenode.SelectedItem.Text;
                grid.SettingsText.Title = "Trial Balance of Degree Debtors" + value + "from " + dateFromConsolidated.Text + " to " + dateToConsolidated.Text;

                qry = @"select t1.ChoosenDate as `Date`,t3.Node as Head , t1.Narration,(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )as `Credit`,(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )as `Debit`,t10.Node as 'Node' from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left join headstree as t8 on (t1.ChitGroupID=t8.NodeID) " +
                    "left join headstree as t9 on (t9.NodeID=t1.Head_Id) " +
                    "left join headstree as t10 on (t10.NodeID=t9.ParentID) " +
                    "where  `t1`.`BranchID` = " + branchids + " and `t1`.`RootID` = 7 and t1.ChoosenDate between '" + balayer.indiandateToMysqlDate(dateFromConsolidated.Text) + "' and '" + balayer.indiandateToMysqlDate(dateToConsolidated.Text) + "' and t1.IsDeleted=0 and t10.NodeID='" + ddldegreenode.SelectedItem.Value + "' order by t1.ChoosenDate asc";
                finalDt = balayer.GetDataTable(qry);
            }


            LoadGridTemp();

            finalDt.Columns[3].DataType = typeof(decimal);
            finalDt.Columns[4].DataType = typeof(decimal);
           // decimal previouscre = Convert.ToDecimal(objVar.tempDt.Rows[0].ItemArray[2]);
          //  decimal previousdeb = Convert.ToDecimal(objVar.tempDt.Rows[0].ItemArray[3]);
            if (objVar.tempDt.Rows.Count > 0)
            {
                foreach (DataRow dr in objVar.tempDt.Rows)
                {
                    DataRow newRow = finalDt.NewRow();
                    if (Convert.ToString(dr.ItemArray[2]) != "" || Convert.ToString(dr.ItemArray[3]) != "")
                    {
                        if (Convert.ToDecimal(dr.ItemArray[2]) > Convert.ToDecimal(dr.ItemArray[3]))
                        {
                            newRow[2] = dr.ItemArray[0].ToString();
                            newRow[3] = Convert.ToDecimal(dr.ItemArray[2]) - Convert.ToDecimal(dr.ItemArray[3]);
                            newRow[4] = Convert.ToDecimal("0.00");
                        }
                        else if (Convert.ToDecimal(dr.ItemArray[3]) > Convert.ToDecimal(dr.ItemArray[2]))
                        {
                            newRow[2] = dr.ItemArray[0].ToString();
                            newRow[4] = Convert.ToDecimal(dr.ItemArray[3]) - Convert.ToDecimal(dr.ItemArray[2]);
                            newRow[3] = Convert.ToDecimal("0.00");
                        }
                        else
                        {
                            newRow[2] = dr.ItemArray[0].ToString();
                            newRow[3] = Convert.ToDecimal("0.00");
                            newRow[4] = Convert.ToDecimal("0.00");
                        }
                    }
                    else
                    {
                        newRow[2] = "Previous Net Balance";
                        newRow[3] = Convert.ToDecimal("0.00");
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
            if (balayer.ToobjectstrEvenNull(Session["Branchid"]) == "161")
            {
                branchids = drpdownBranchlist.SelectedItem.Value;
            }
            else
            {
                branchids = balayer.ToobjectstrEvenNull(Session["Branchid"]);
            }
            if (ddldegree.SelectedIndex == 0 && ddldegreenode.SelectedIndex == 0)
            {
                objVar.varquery = @"select 'Previous Net Balance' as 'Title',(case when sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end ) > sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) then CONCAT('Cr.Bal. ',CAST(sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end ) - sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )AS char(50))) else  CONCAT('Dr.Bal.',CAST(sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) - sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )AS char(50)))  end)  as Narration,sum((case when t1.Voucher_Type='C' then t1.Amount else 0.00 end ))as `Credit`,sum((case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ))as `Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left join headstree as t8 on (t1.ChitGroupID=t8.NodeID) " +
                    "left join headstree as t9 on (t9.NodeID=t1.Head_Id) " +
                    "left join headstree as t10 on (t10.NodeID=t9.ParentID) " +
                    "where  `t1`.`BranchID` = " + branchids + " and `t1`.`RootID` = 7 and t1.ChoosenDate < '" + balayer.indiandateToMysqlDate(dateFromConsolidated.Text) + "' and t1.IsDeleted=0 order by t1.ChoosenDate asc";
                // objVar.varquery = "select 'Previous Net Balance' as 'Title',(case when sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end ) > sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) then CONCAT('Cr.Bal. ',CAST(sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end ) - sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )AS char(50))) else  CONCAT('Dr.Bal.',CAST(sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) - sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )AS char(50)))  end)  as Narration,sum((case when t1.Voucher_Type='C' then t1.Amount else 0.00 end ))as `Credit`,sum((case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ))as `Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left join headstree as t8 on (t1.ChitGroupID=t8.NodeID)  where `t1`.`BranchID`=" + branchids + "  and `t1`.`RootID` = 7 and t1.ChoosenDate < '" + balayer.indiandateToMysqlDate(dateFromConsolidated.Text) + "' and t1.IsDeleted=0 and t3.NodeID='" + ddldegree.SelectedItem.Value + "' order by t1.ChoosenDate asc;";
                objVar.tempDt = balayer.GetDataTable(objVar.varquery);
            }
            else if (ddldegree.SelectedIndex != 0 && ddldegreenode.SelectedIndex != 0)
            {
                objVar.varquery = @"select 'Previous Net Balance' as 'Title',(case when sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end ) > sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) then CONCAT('Cr.Bal. ',CAST(sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end ) - sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )AS char(50))) else  CONCAT('Dr.Bal.',CAST(sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) - sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )AS char(50)))  end)  as Narration,sum((case when t1.Voucher_Type='C' then t1.Amount else 0.00 end ))as `Credit`,sum((case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ))as `Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left join headstree as t8 on (t1.ChitGroupID=t8.NodeID) " +
                   "left join headstree as t9 on (t9.NodeID=t1.Head_Id) " +
                   "left join headstree as t10 on (t10.NodeID=t9.ParentID) " +
                   "where  `t1`.`BranchID` = " + branchids + " and `t1`.`RootID` = 7 and t1.ChoosenDate < '" + balayer.indiandateToMysqlDate(dateFromConsolidated.Text) + "' and t1.IsDeleted=0 and t9.NodeID='" + ddldegree.SelectedItem.Value + "' and  t10.NodeID='" + ddldegreenode.SelectedItem.Value + "' order by t1.ChoosenDate asc";
                objVar.tempDt = balayer.GetDataTable(objVar.varquery);
            }
            else
            {
                objVar.varquery = @"select 'Previous Net Balance' as 'Title',(case when sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end ) > sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) then CONCAT('Cr.Bal. ',CAST(sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end ) - sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )AS char(50))) else  CONCAT('Dr.Bal.',CAST(sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) - sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )AS char(50)))  end)  as Narration,sum((case when t1.Voucher_Type='C' then t1.Amount else 0.00 end ))as `Credit`,sum((case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ))as `Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left join headstree as t8 on (t1.ChitGroupID=t8.NodeID) " +
                    "left join headstree as t9 on (t9.NodeID=t1.Head_Id) " +
                    "left join headstree as t10 on (t10.NodeID=t9.ParentID) " +
                    "where  `t1`.`BranchID` = " + branchids + " and `t1`.`RootID` = 7 and t1.ChoosenDate < '" + balayer.indiandateToMysqlDate(dateFromConsolidated.Text) + "' and t1.IsDeleted=0 and t10.NodeID='" + ddldegreenode.SelectedItem.Value + "' order by t1.ChoosenDate asc";
                //objVar.varquery = "select 'Previous Net Balance' as 'Title',(case when sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end ) > sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) then CONCAT('Cr.Bal. ',CAST(sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end ) - sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )AS char(50))) else  CONCAT('Dr.Bal.',CAST(sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) - sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )AS char(50)))  end)  as Narration,sum((case when t1.Voucher_Type='C' then t1.Amount else 0.00 end ))as `Credit`,sum((case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ))as `Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left join headstree as t8 on (t1.ChitGroupID=t8.NodeID)  where `t1`.`BranchID`=" + branchids + "  and `t1`.`RootID` = 7 and t1.ChoosenDate < '" + balayer.indiandateToMysqlDate(dateFromConsolidated.Text) + "' and t1.IsDeleted=0 order by t1.ChoosenDate asc;";
                objVar.tempDt = balayer.GetDataTable(objVar.varquery);
            }

            return objVar.tempDt;
        }


        protected void BtnStatisticsGo_Click(object sender, EventArgs e)
        {
            setStats(dateFromConsolidated.Text, dateToConsolidated.Text);

            grid.DataSource = LoadGrid();
            grid.DataBind();
            //select();
            //select(ddldegree.SelectedIndex);
            //    string value = ddldegree.SelectedItem.Text;
            grid.SettingsText.Title = "Trial Balance of Degree from " + dateFromConsolidated.Text + " to " + dateToConsolidated.Text;
            
        }
        protected void ASPxGridView1_SummaryDisplayText(object sender, ASPxGridViewSummaryDisplayTextEventArgs e)
        {
            string parsed = "";
            if (e.Value != null)
            {
                parsed = balayer.ConvertToIndCurrency(e.Value.ToString());
            }
            else
            {
                parsed = "";
            }
            
            if (e.Item.FieldName == "Credit")
            {
                e.Text =   parsed;
            }
            if (e.Item.FieldName == "Debit")
            {
                e.Text =   parsed;
            }
            if (e.Item.FieldName == "Narration")
            {
                e.Text = e.Item.DisplayFormat.ToString() + parsed;
            }

            if (e.Item.FieldName == "Node")
            {
                e.Text = e.Item.DisplayFormat.ToString() + parsed;
            }

            if (e.Item.FieldName == "Date")
            {
                e.Text = parsed;
            }
            if (e.Item.FieldName == "Head")
            {
                e.Text = parsed;
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
                                total += Convert.ToDecimal(grid.GetGroupSummaryValue(i, grid.GroupSummary["Head"]));
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
                    if (balayer.ToobjectstrEvenNull(Session["Branchid"]) == "161")
                    {
                        branchids = drpdownBranchlist.SelectedItem.Value;
                    }
                    else
                    {
                        branchids = balayer.ToobjectstrEvenNull(Session["Branchid"]);
                    }
                    string ion = "";
                    var iii = "";
                    if (cre > 0)
                    {
                        objVar.varquery = "select (case when sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end ) > sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) then CONCAT('Cr.Bal- ',CAST(sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end ) - sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )AS char(50))) else  CONCAT('Dr.Bal-',CAST(sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) - sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )AS char(50)))  end)  as Narration from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left join headstree as t8 on (t1.ChitGroupID=t8.NodeID) where `t1`.`BranchID` =  " + branchids + "   and `t1`.`RootID` = 2 and t1.ChoosenDate < '" + balayer.indiandateToMysqlDate(dateFromConsolidated.Text) + "' and t1.IsDeleted=0 order by t1.ChoosenDate asc;";
                        ion = balayer.GetSingleValue(objVar.varquery);
                        //  iii = ion.Split('-')[1];

                        if (ddldegree.SelectedIndex != 0)
                        {
                            string value = ddldegree.SelectedItem.Text;

                            objVar.varquery = "select (case when sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end ) > sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) then CONCAT('Cr.Bal- ',CAST(sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end ) - sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )AS char(50))) else  CONCAT('Dr.Bal-',CAST(sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) - sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )AS char(50)))  end)  as Narration from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left join headstree as t8 on (t1.ChitGroupID=t8.NodeID) where `t1`.`BranchID` =  " + branchids + "   and `t1`.`RootID` = 2 and t1.ChoosenDate < '" + balayer.indiandateToMysqlDate(dateFromConsolidated.Text) + "' and t1.IsDeleted=0 and t3.NodeID='" + ddldegree.SelectedItem.Value + "' order by t1.ChoosenDate asc;";
                            // objVar.varquery = "select 'Previous Net Balance' as 'Title',(case when sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end ) > sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) then CONCAT('Cr.Bal. ',CAST(sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end ) - sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )AS char(50))) else  CONCAT('Dr.Bal.',CAST(sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) - sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )AS char(50)))  end)  as Narration, CONCAT('Cr. INR ', CAST(sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )AS char(50))) as `Credit`,CONCAT('Dr. INR ',CAST(sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )AS char(50))) as `Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left join headstree as t8 on (t1.ChitGroupID=t8.NodeID) where `t1`.`BranchID` =" + branchids + "  and `t1`.`RootID` = 1 and t1.ChoosenDate < '" + balayer.indiandateToMysqlDate(dateFromConsolidated.Text) + "' and t1.IsDeleted=0 order by t1.ChoosenDate asc;";
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
                            objVar.varquery = "select (case when sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end ) > sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) then CONCAT('Cr.Bal- ',CAST(sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end ) - sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )AS char(50))) else  CONCAT('Dr.Bal-',CAST(sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) - sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )AS char(50)))  end)  as Narration from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left join headstree as t8 on (t1.ChitGroupID=t8.NodeID) where `t1`.`BranchID` =  " + branchids + "   and `t1`.`RootID` = 2 and t1.ChoosenDate < '" + balayer.indiandateToMysqlDate(dateFromConsolidated.Text) + "' and t1.IsDeleted=0 order by t1.ChoosenDate asc;";
                            // objVar.varquery = "select 'Previous Net Balance' as 'Title',(case when sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end ) > sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) then CONCAT('Cr.Bal. ',CAST(sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end ) - sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )AS char(50))) else  CONCAT('Dr.Bal.',CAST(sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) - sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )AS char(50)))  end)  as Narration, CONCAT('Cr. INR ', CAST(sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )AS char(50))) as `Credit`,CONCAT('Dr. INR ',CAST(sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )AS char(50))) as `Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left join headstree as t8 on (t1.ChitGroupID=t8.NodeID) where `t1`.`BranchID` =" + branchids + "  and `t1`.`RootID` = 1 and t1.ChoosenDate < '" + balayer.indiandateToMysqlDate(dateFromConsolidated.Text) + "' and t1.IsDeleted=0 order by t1.ChoosenDate asc;";
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
                    else
                    {
                        objVar.varquery = "select (case when sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end ) > sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) then CONCAT('Cr.Bal- ',CAST(sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end ) - sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )AS char(50))) else  CONCAT('Dr.Bal-',CAST(sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) - sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )AS char(50)))  end)  as Narration from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left join headstree as t8 on (t1.ChitGroupID=t8.NodeID) where `t1`.`BranchID` =  " + branchids + "   and `t1`.`RootID` = 2 and t1.ChoosenDate < '" + balayer.indiandateToMysqlDate(dateFromConsolidated.Text) + "' and t1.IsDeleted=0 order by t1.ChoosenDate asc;";
                        ion = balayer.GetSingleValue(objVar.varquery);
                        //  iii = ion.Split('-')[1];

                        if (ddldegree.SelectedIndex != 0)
                        {
                            string value = ddldegree.SelectedItem.Text;

                            objVar.varquery = "select (case when sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end ) > sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) then CONCAT('Cr.Bal- ',CAST(sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end ) - sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )AS char(50))) else  CONCAT('Dr.Bal-',CAST(sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) - sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )AS char(50)))  end)  as Narration from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left join headstree as t8 on (t1.ChitGroupID=t8.NodeID) where `t1`.`BranchID` =  " + branchids + "   and `t1`.`RootID` = 2 and t1.ChoosenDate < '" + balayer.indiandateToMysqlDate(dateFromConsolidated.Text) + "' and t1.IsDeleted=0 and t3.NodeID='" + ddldegree.SelectedItem.Value + "' order by t1.ChoosenDate asc;";
                            // objVar.varquery = "select 'Previous Net Balance' as 'Title',(case when sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end ) > sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) then CONCAT('Cr.Bal. ',CAST(sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end ) - sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )AS char(50))) else  CONCAT('Dr.Bal.',CAST(sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) - sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )AS char(50)))  end)  as Narration, CONCAT('Cr. INR ', CAST(sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )AS char(50))) as `Credit`,CONCAT('Dr. INR ',CAST(sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )AS char(50))) as `Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left join headstree as t8 on (t1.ChitGroupID=t8.NodeID) where `t1`.`BranchID` =" + branchids + "  and `t1`.`RootID` = 1 and t1.ChoosenDate < '" + balayer.indiandateToMysqlDate(dateFromConsolidated.Text) + "' and t1.IsDeleted=0 order by t1.ChoosenDate asc;";
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
                            objVar.varquery = "select (case when sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end ) > sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) then CONCAT('Cr.Bal- ',CAST(sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end ) - sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )AS char(50))) else  CONCAT('Dr.Bal-',CAST(sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) - sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )AS char(50)))  end)  as Narration from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left join headstree as t8 on (t1.ChitGroupID=t8.NodeID) where `t1`.`BranchID` =  " + branchids + "   and `t1`.`RootID` = 2 and t1.ChoosenDate < '" + balayer.indiandateToMysqlDate(dateFromConsolidated.Text) + "' and t1.IsDeleted=0 order by t1.ChoosenDate asc;";
                            // objVar.varquery = "select 'Previous Net Balance' as 'Title',(case when sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end ) > sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) then CONCAT('Cr.Bal. ',CAST(sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end ) - sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )AS char(50))) else  CONCAT('Dr.Bal.',CAST(sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) - sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )AS char(50)))  end)  as Narration, CONCAT('Cr. INR ', CAST(sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )AS char(50))) as `Credit`,CONCAT('Dr. INR ',CAST(sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )AS char(50))) as `Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left join headstree as t8 on (t1.ChitGroupID=t8.NodeID) where `t1`.`BranchID` =" + branchids + "  and `t1`.`RootID` = 1 and t1.ChoosenDate < '" + balayer.indiandateToMysqlDate(dateFromConsolidated.Text) + "' and t1.IsDeleted=0 order by t1.ChoosenDate asc;";
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
                        
                        var val1 = cre - deb;
                        x.DisplayFormat = "NetBalance Cr";
                        e.TotalValue = val1;

                        
                        e.TotalValueReady = true;

                    }
                    else if (cre < deb)
                    {

                        var val1 = deb - cre;
                        x.DisplayFormat = " NetBalance Dr";
                        e.TotalValue = val1;


                       
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
                if (x.FieldName.ToString() == "Head")
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
        protected void grid_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            //ApplyLayout(Int32.Parse(e.Parameters));
        }
        protected virtual string GetLabelText(GridViewGroupRowTemplateContainer container)
        {
            return container.GroupText;
        }
        protected void ddlGroup_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlGroup.SelectedIndex == 0)
            {
                select();
                //select(ddldegree.SelectedIndex);
                ApplyLayout(0);
                grid.TotalSummary["Credit"].SummaryType = SummaryItemType.Sum;
                grid.TotalSummary["Debit"].SummaryType = SummaryItemType.Sum;
                grid.SettingsPager.Mode = GridViewPagerMode.ShowPager;
            }
            else if (ddlGroup.SelectedIndex == 1)
            {
               /// select(ddldegree.SelectedIndex);
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
        //public DataTable LoadGridTemp()
        //{
        //    objVar.tempDt = new DataTable();

        //    objVar.varquery = "select 'Previous Net Balance' as 'Title',(case when sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end ) > sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) then CONCAT('Cr.Bal. ',CAST(sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end ) - sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )AS char(50))) else  CONCAT('Dr.Bal.',CAST(sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) - sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )AS char(50)))  end)  as Narration,sum((case when t1.Voucher_Type='C' then t1.Amount else 0.00 end ))as `Credit`,sum((case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ))as `Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left join headstree as t8 on (t1.ChitGroupID=t8.NodeID)  where `t1`.`BranchID`=" + branchids + "  and `t1`.`RootID` = 7 and t1.ChoosenDate < '" + balayer.indiandateToMysqlDate(dateFromConsolidated.Text) + "' and t1.IsDeleted=0 order by t1.ChoosenDate asc;";
        //    objVar.tempDt = balayer.GetDataTable(objVar.varquery);

        //    return objVar.tempDt;
        //}
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

                    grid.DataSource = LoadGrid();
                    grid.DataBind();

                    grid.Settings.ShowTitlePanel = false;
                    gridchequeprev.Component = gridExportprev;
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
                        WriteToResponse("DecreeDebtorsLedger", true, "pdf", stream);
                    }
                }
            }
            else if (e.Item.Text.ToString() == "XLSX")
            {
                gridExportprev.PreserveGroupRowStates = true;
                gridExportprev.WriteXlsxToResponse();
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
            //tb.Rect = new RectangleF(50, 8, (e.Graph.ClientPageSize.Width / 2), 20);
            tb.Rect = new RectangleF(200, 8, (e.Graph.ClientPageSize.Width / 2), 20);
            tb.BorderWidth = 0;
            tb.BackColor = Color.Transparent;
            tb.HorzAlignment = DevExpress.Utils.HorzAlignment.Center;
            tb.VertAlignment = DevExpress.Utils.VertAlignment.Top;
            e.Graph.DrawBrick(tb);
            TextBrick tb1 = new TextBrick();
            tb1.Text = "Branch : " + Session["BranchName"];
            tb1.Font = new Font("Arial", 9);
            tb1.Rect = new RectangleF(200, 28, (e.Graph.ClientPageSize.Width / 2), 20);
            tb1.BorderWidth = 0;
            tb1.BackColor = Color.Transparent;
            tb1.HorzAlignment = DevExpress.Utils.HorzAlignment.Center;
            tb1.VertAlignment = DevExpress.Utils.VertAlignment.Top;
            e.Graph.DrawBrick(tb1);

            if (ddldegreenode.SelectedItem.Text != "--select--" && ddldegree.SelectedItem.Text != "--select--")
            {
                TextBrick tb2 = new TextBrick();
                tb2.Text = "Decree Debtors  "+"-" + ddldegreenode.SelectedItem.Text + " - "+ ddldegree.SelectedItem.Text;
                tb2.Font = new Font("Arial", 9);
                tb2.Rect = new RectangleF(200, 48, (e.Graph.ClientPageSize.Width / 1), 30);
                tb2.BorderWidth = 0;
                tb2.BackColor = Color.Transparent;
                tb2.HorzAlignment = DevExpress.Utils.HorzAlignment.Default;
                tb2.VertAlignment = DevExpress.Utils.VertAlignment.Top;
                e.Graph.DrawBrick(tb2);

                TextBrick tb3 = new TextBrick();
                tb3.Text = "From : " + dateFromConsolidated.Text + " to " + dateToConsolidated.Text;
                tb3.Font = new Font("Arial", 9);
                tb3.Rect = new RectangleF(200, 78, (e.Graph.ClientPageSize.Width / 2), 20);
                tb3.BorderWidth = 0;
                tb3.BackColor = Color.Transparent;
                tb3.HorzAlignment = DevExpress.Utils.HorzAlignment.Center;
                tb3.VertAlignment = DevExpress.Utils.VertAlignment.Top;
                e.Graph.DrawBrick(tb3);
            }
            else if(ddldegreenode.SelectedItem.Text != "--select--")
            {
                TextBrick tb4 = new TextBrick();
                tb4.Text = "Decree Debtors  " + "-" + ddldegreenode.SelectedItem.Text ;
                tb4.Font = new Font("Arial", 9);
                tb4.Rect = new RectangleF(200, 48, (e.Graph.ClientPageSize.Width / 2), 30);
                tb4.BorderWidth = 0;
                tb4.BackColor = Color.Transparent;
                tb4.HorzAlignment = DevExpress.Utils.HorzAlignment.Center;
                tb4.VertAlignment = DevExpress.Utils.VertAlignment.Top;
                e.Graph.DrawBrick(tb4);

                TextBrick tb5 = new TextBrick();
                tb5.Text = "From : " + dateFromConsolidated.Text + " to " + dateToConsolidated.Text;
                tb5.Font = new Font("Arial", 9);
                tb5.Rect = new RectangleF(200, 78, (e.Graph.ClientPageSize.Width / 2), 20);
                tb5.BorderWidth = 0;
                tb5.BackColor = Color.Transparent;
                tb5.HorzAlignment = DevExpress.Utils.HorzAlignment.Center;
                tb5.VertAlignment = DevExpress.Utils.VertAlignment.Top;
                e.Graph.DrawBrick(tb5);
            }
            else
            {
                TextBrick tb14 = new TextBrick();
                tb14.Text = "Decree Debtors  ";
                tb14.Font = new Font("Arial", 9);
                tb14.Rect = new RectangleF(200, 48, (e.Graph.ClientPageSize.Width / 2), 30);
                tb14.BorderWidth = 0;
                tb14.BackColor = Color.Transparent;
                tb14.HorzAlignment = DevExpress.Utils.HorzAlignment.Center;
                tb14.VertAlignment = DevExpress.Utils.VertAlignment.Top;
                e.Graph.DrawBrick(tb14);

                TextBrick tb3 = new TextBrick();
                tb3.Text = "From : " + dateFromConsolidated.Text + " to " + dateToConsolidated.Text;
                tb3.Font = new Font("Arial", 9);
                tb3.Rect = new RectangleF(200, 78, (e.Graph.ClientPageSize.Width / 2), 20);
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
            grid.BeginUpdate();
            try
            {
                grid.ClearSort();
                switch (layoutIndex)
                {
                    case 0:
                        break;
                    case 1:
                        grid.GroupBy(grid.Columns["Head"]);
                        grid.GroupBy(grid.Columns["Date"]);
                        break;
                    case 2:
                        grid.GroupBy(grid.Columns["Head"]);
                        break;
                }
            }
            finally
            {
                grid.EndUpdate();
            }
            grid.CollapseAll();
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
                if (e.Item.FieldName == "Head")
                {
                    e.Text = parsed;
                }
                if (e.Item.FieldName == "Node")
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
                if (x.FieldName.ToString() == "Head")
                {
                    decimal Credit1 = Convert.ToDecimal(e.GetGroupSummary(e.GroupRowHandle, gridprev.GroupSummary["Credit"]));
                    decimal Debit1 = Convert.ToDecimal(e.GetGroupSummary(e.GroupRowHandle, gridprev.GroupSummary["Debit"]));
                    if (Credit1 >= Debit1)
                    {
                        e.TotalValue = Credit1 - Debit1;
                    }
                }

                if (x.FieldName.ToString() == "Node")
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
