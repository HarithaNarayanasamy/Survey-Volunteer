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
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using SVCF_BusinessAccessLayer;
using SVCF_TransactionLayer;
using SVCF_DataAccessLayer;
using System.Security.Cryptography;
using System.Text;
using System.Web.Services;

namespace SreeVisalamChitFundLtd_phase1
{
    public partial class PallathurVoucherMultiples : System.Web.UI.Page
    {
        #region VarDeclaration
        int rootid1;
        double credittotal1 = 0;
        double existing_crdtotal1 = 0;
        double hiddenamnt1 = 0;

        int rootid2;
        double credittotal2 = 0;
        double existing_crdtotal2 = 0;
        double hiddenamnt2 = 0;

        int rootid3;
        double credittotal3 = 0;
        double existing_crdtotal3 = 0;
        double hiddenamnt3 = 0;
        #endregion

        #region VarDeclaration
        BusinessLayer balayer = new BusinessLayer();
        string clientcrheads1 = "";
        string clientdbheads1 = "";
        string clientcrheads2 = "";
        string clientdbheads2 = "";
        string clientcrheads3 = "";
        string clientdbheads3 = "";
        #endregion

        protected void bindHeads(DropDownList ddlHeads, int iTabPage)
        {
            int iBranchID = Convert.ToInt32(Session["Branchid"]);
            if (Convert.ToInt32(Session["Branchid"]) == 161)
                iBranchID = 159 + iTabPage;

            DataTable dtAllHeads = balayer.RetrieveVHeads(iBranchID, "VoucherHeads");

            ////bind debit heads
            DataRow drow = dtAllHeads.NewRow();
            drow[0] = "select";
            drow[1] = "select";
            dtAllHeads.Rows.InsertAt(drow, 0);
            //DropDownList ddlHeadsDebit = (DropDownList)this.FindControl("ddlHeadsDebit" + iTabPage.ToString());
            if (iTabPage == 1)
            {
                ddlHeadsDebit1.DataSource = dtAllHeads;
                ddlHeadsDebit1.DataTextField = "TREE";
                ddlHeadsDebit1.DataValueField = "ID";
                ddlHeadsDebit1.DataBind();
            }
            else if (iTabPage == 2)
            {
                ddlHeadsDebit2.DataSource = dtAllHeads;
                ddlHeadsDebit2.DataTextField = "TREE";
                ddlHeadsDebit2.DataValueField = "ID";
                ddlHeadsDebit2.DataBind();
            }
            else if (iTabPage == 3)
            {
                ddlHeadsDebit3.DataSource = dtAllHeads;
                ddlHeadsDebit3.DataTextField = "TREE";
                ddlHeadsDebit3.DataValueField = "ID";
                ddlHeadsDebit3.DataBind();
            }
            
            ////bind credit heads
            string strQuery = "";
            if (Convert.ToInt32(Session["Branchid"]) == 161)
            {
                strQuery = "SELECT concat(g1.GROUPNO,'|',g1.GROUPNO) as TREE,concat(cast(g1.Head_Id as char),'|',cast(g1.Head_Id as char),'|',cast(g1.Head_Id as char)) as ID FROM groupmaster as g1 where g1.BranchID = " + (iTabPage + 159).ToString();
            }
            else
            {
                strQuery = "SELECT concat(g1.GROUPNO,'|',g1.GROUPNO) as TREE,concat(cast(g1.Head_Id as char),'|',cast(g1.Head_Id as char),'|',cast(g1.Head_Id as char)) as ID FROM groupmaster as g1 where g1.BranchID=" + Session["Branchid"];
            }
            DataTable dtChit1 = balayer.GetDataTable(strQuery);
            dtAllHeads.Merge(dtChit1);

            DataRow dr = dtAllHeads.NewRow();
            dr[0] = "select";
            dr[1] = "select";
            dtAllHeads.Rows.InsertAt(dr, 0);
            ddlHeads.DataSource = dtAllHeads;
            ddlHeads.DataTextField = "TREE";
            ddlHeads.DataValueField = "ID";
            ddlHeads.DataBind();
            dtAllHeads.Dispose();
        }
        //protected void bindHeadsDebit(DropDownList ddlHeads)
        //{

        //    DataTable dtAllHeads = balayer.RetrieveVHeads(Convert.ToInt32(Session["Branchid"]), "VoucherHeads");
        //    DataRow dr = dtAllHeads.NewRow();
        //    dr[0] = "select";
        //    dr[1] = "select";
        //    dtAllHeads.Rows.InsertAt(dr, 0);
        //    ddlHeads.DataSource = dtAllHeads;
        //    ddlHeads.DataTextField = "TREE";
        //    ddlHeads.DataValueField = "ID";
        //    ddlHeads.DataBind();
        //    dtAllHeads.Dispose();
        //}
        protected Boolean IsCheque(string Voucher_Type)
        {
            return Voucher_Type.ToUpper().Equals("B");
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            pnlpopup.Visible = false;
            if (!IsPostBack)
            {
                //if (Convert.ToInt32(Session["Branchid"]) == 161)
                //{
                    bindHeads(ddlHeads1, 1);
                    bindHeads(ddlHeads2, 2);
                    bindHeads(ddlHeads3, 3);

                    //carTabPage.TabPages[1].Visible = true;
                    //carTabPage.TabPages[2].Visible = true;

                    rvDate1.MaximumValue = DateTime.Now.ToString("dd/MM/yyyy");
                    rvDate1.MinimumValue = balayer.GetSingleValue("SELECT DATE_FORMAT( MinimumDate, '%d/%m/%Y') MinimumDate FROM svcf.restrictionmaster where BranchID=160");

                    rvDate2.MaximumValue = DateTime.Now.ToString("dd/MM/yyyy");
                    rvDate2.MinimumValue = balayer.GetSingleValue("SELECT DATE_FORMAT( MinimumDate, '%d/%m/%Y') MinimumDate FROM svcf.restrictionmaster where BranchID=161");

                    rvDate3.MaximumValue = DateTime.Now.ToString("dd/MM/yyyy");
                    rvDate3.MinimumValue = balayer.GetSingleValue("SELECT DATE_FORMAT( MinimumDate, '%d/%m/%Y') MinimumDate FROM svcf.restrictionmaster where BranchID=162");

                    Session["CheckRefresh"] = System.Guid.NewGuid().ToString();
                    
                    txtReceivedBy1.Text = balayer.ToobjectstrEvenNull(Session["UserName"]);
                    txtReceivedBy2.Text = balayer.ToobjectstrEvenNull(Session["UserName"]);
                    txtReceivedBy3.Text = balayer.ToobjectstrEvenNull(Session["UserName"]);

                    ViewState["tabnum"] = "57";
                    //this.BindData();

                    //SetDefaultRow();
                    SetDefaultRowDB();
                    //this.BindDataDebit();
                    //SetDefaultRowDebit();
                    txtDate1.Focus();
                    
                    txtDate1.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    txtDate2.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    txtDate3.Text = DateTime.Now.ToString("dd/MM/yyyy");

                    txtSeries1.Text = "VOUCHER";
                    txtSeries2.Text = "VOUCHER";
                    txtSeries3.Text = "VOUCHER";
                    
                    //commented by gayathri GetSeriesAndVoucherNo();
                    txtVoucherNo1.Text = balayer.GetSingleValue("SELECT ifnull(max(cast(Voucher_No as unsigned)),0)+1 FROM svcf.voucher where ChoosenDate>='2014/04/01' and  Series='VOUCHER' and Trans_Type='0' and BranchID=160");
                    txtVoucherNo2.Text = balayer.GetSingleValue("SELECT ifnull(max(cast(Voucher_No as unsigned)),0)+1 FROM svcf.voucher where ChoosenDate>='2014/04/01' and  Series='VOUCHER' and Trans_Type='0' and BranchID=161");
                    txtVoucherNo3.Text = balayer.GetSingleValue("SELECT ifnull(max(cast(Voucher_No as unsigned)),0)+1 FROM svcf.voucher where ChoosenDate>='2014/04/01' and  Series='VOUCHER' and Trans_Type='0' and BranchID=162");
                    //SetGridTabIndex(true);
                    //SetGridTabIndex(false);
                    string Choosendate1 = balayer.GetSingleValue("SELECT DATE_FORMAT(max(ChoosenDate),'%d/%m/%Y') from `svcf`.`voucher`where BranchID=160 and ChoosenDate<>'0000-00-00'");
                    string Choosendate2 = balayer.GetSingleValue("SELECT DATE_FORMAT(max(ChoosenDate),'%d/%m/%Y') from `svcf`.`voucher`where BranchID=161 and ChoosenDate<>'0000-00-00'");
                    string Choosendate3 = balayer.GetSingleValue("SELECT DATE_FORMAT(max(ChoosenDate),'%d/%m/%Y') from `svcf`.`voucher`where BranchID=162 and ChoosenDate<>'0000-00-00'");
                    //string Choosendate = balayer.GetSingleValue("SELECT DATE_FORMAT( MinimumDate, '%d/%m/%Y') MinimumDate FROM svcf.restrictionmaster where BranchID=" + Session["Branchid"]);
                    txtDate1.Text = Choosendate1;
                    txtDate2.Text = Choosendate2;
                    txtDate3.Text = Choosendate3;
                    
                    lblcancelmsg1.Text = "";
                    lblcancelmsg2.Text = "";
                    lblcancelmsg3.Text = "";

                    ViewState["CurrentTable1"] = null;
                    ViewState["CurrentTableDB1"] = null;
                    ViewState["CurrentTable2"] = null;
                    ViewState["CurrentTableDB2"] = null;
                    ViewState["CurrentTable3"] = null;
                    ViewState["CurrentTableDB3"] = null;

                    SetInitRow_Clnt1();
                    SetInitDb_Clnt1();
                    SetInitRow_Clnt2();
                    SetInitDb_Clnt2();
                    SetInitRow_Clnt3();
                    SetInitDb_Clnt3();
                //}
                //else
                //{
                //    bindHeads(ddlHeads1, 1);

                //    carTabPage.TabPages[1].Visible = false;
                //    carTabPage.TabPages[2].Visible = false;

                //    rvDate1.MaximumValue = DateTime.Now.ToString("dd/MM/yyyy");

                //    rvDate1.MinimumValue = balayer.GetSingleValue("SELECT DATE_FORMAT( MinimumDate, '%d/%m/%Y') MinimumDate FROM svcf.restrictionmaster where BranchID=" + Session["Branchid"]);

                //    Session["CheckRefresh"] = System.Guid.NewGuid().ToString();
                //    txtReceivedBy1.Text = balayer.ToobjectstrEvenNull(Session["UserName"]);
                //    ViewState["tabnum"] = "57";
                //    SetDefaultRowDB();
                //    txtDate1.Focus();
                //    txtDate1.Text = DateTime.Now.ToString("dd/MM/yyyy");
                //    txtSeries1.Text = "VOUCHER";
                //    //commented by gayathri GetSeriesAndVoucherNo();
                //    txtVoucherNo1.Text = balayer.GetSingleValue("SELECT ifnull(max(cast(Voucher_No as unsigned)),0)+1 FROM svcf.voucher where ChoosenDate>='2014/04/01' and  Series='VOUCHER' and Trans_Type='0' and BranchID=" + Session["Branchid"]);
                //    //SetGridTabIndex(true);
                //    //SetGridTabIndex(false);
                //    string Choosendate = balayer.GetSingleValue("SELECT DATE_FORMAT(max(ChoosenDate),'%d/%m/%Y') from `svcf`.`voucher`where BranchID=" + Session["Branchid"] + " and ChoosenDate<>'0000-00-00'");
                //    //string Choosendate = balayer.GetSingleValue("SELECT DATE_FORMAT( MinimumDate, '%d/%m/%Y') MinimumDate FROM svcf.restrictionmaster where BranchID=" + Session["Branchid"]);
                //    txtDate1.Text = Choosendate;
                //    lblcancelmsg1.Text = "";
                //    ViewState["CurrentTable1"] = null;
                //    ViewState["CurrentTableDB1"] = null;

                //    SetInitRow_Clnt1();
                //    SetInitDb_Clnt1();
                //}
            }
        }
        //private void BindDataDebit()
        //{

        //}
        //private void BindData()
        //{

        //}

        //private void SetDefaultRow()
        //{

        //}

        //public void SetDefaultRowold()
        //{

        //}
        //public void SetDefaultRowDebit()
        //{

        //}
        protected void ddlHeadsDebit1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strMemberName = "";
            if (ddlHeadsDebit1.SelectedValue.ToString().Contains("5,"))
            {
                strMemberName = balayer.GetSingleValue("SELECT MemberName FROM svcf.membertogroupmaster where Head_Id=" + ddlHeadsDebit1.SelectedItem.Value.Split(',')[1]);
                txtDebitdesc1.Text = strMemberName;
            }
        }

        protected void ddlHeadsDebit2_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strMemberName = "";
            if (ddlHeadsDebit2.SelectedValue.ToString().Contains("5,"))
            {
                strMemberName = balayer.GetSingleValue("SELECT MemberName FROM svcf.membertogroupmaster where Head_Id=" + ddlHeadsDebit2.SelectedItem.Value.Split(',')[1]);
                txtDebitdesc2.Text = strMemberName;
            }
        }

        protected void ddlHeadsDebit3_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strMemberName = "";
            if (ddlHeadsDebit3.SelectedValue.ToString().Contains("5,"))
            {
                strMemberName = balayer.GetSingleValue("SELECT MemberName FROM svcf.membertogroupmaster where Head_Id=" + ddlHeadsDebit3.SelectedItem.Value.Split(',')[1]);
                txtDebitdesc3.Text = strMemberName;
            }
        }

        [System.Web.Services.WebMethod]
        public static string getcustomername(string hdid)
        {
            string custname = "";
            try
            {
                BusinessLayer blayer = new BusinessLayer();
                DataTable dtAll = blayer.GetDataTable("SELECT MemberName,MemberID FROM svcf.membertogroupmaster where Head_Id=" + hdid);
                if (dtAll.Rows.Count != 0)
                {
                    custname = dtAll.Rows[0][0].ToString();
                }
                else
                {
                    custname = "";
                }

            }
            catch (Exception) { }
            return custname;
        }

        protected void btnAdd1_GridGuardiansDebit_RowCommand_click(object sender, ImageClickEventArgs e)
        {
            try
            {
                //Page.Validate("GrpRowDebit");
                //if (!Page.IsValid)
                //{
                //    return;
                //}  CurrentTableDB1
                if (ViewState["CurrentTableDB1"] != null)
                {
                    DataTable dtCurrentTable = (DataTable)ViewState["CurrentTableDB1"];
                    DataRow drCurrentRow = null;
                    if (dtCurrentTable.Rows.Count > 0)
                    {

                        for (int i = 0; i <= dtCurrentTable.Rows.Count - 1; i++)
                        {
                            drCurrentRow = dtCurrentTable.NewRow();
                            drCurrentRow["DbHeads1"] = ddlHeadsDebit1.SelectedItem.Text;
                            drCurrentRow["DbDescription1"] = txtDebitdesc1.Text;
                            drCurrentRow["Dbheadid1"] = ddlHeadsDebit1.SelectedValue;
                            drCurrentRow["DbAmount1"] = debitAmnt1.Text;
                        }

                        //Remove initial blank row
                        if (dtCurrentTable.Rows[0][0].ToString() == "")
                        {
                            dtCurrentTable.Rows[0].Delete();
                            dtCurrentTable.AcceptChanges();
                        }

                        //Rebind the Grid with the current data
                        drCurrentRow["RowNumber"] = dtCurrentTable.Rows.Count + 1;

                        dtCurrentTable.Rows.Add(drCurrentRow);
                        ViewState["CurrentTableDB1"] = dtCurrentTable;
                        GrdDbClnt1.DataSource = dtCurrentTable;
                        GrdDbClnt1.DataBind();

                        //Rebind the Grid with the current data
                        hiddenamnt1 = Convert.ToDouble(hidden_totalcred1.Value);

                        ddlHeadsDebit1.ClearSelection();
                        txtDebitdesc1.Text = "";
                        debitAmnt1.Text = "";
                    }
                }
            }
            catch (Exception) { }
        }

        protected void btnAdd2_GridGuardiansDebit_RowCommand_click(object sender, ImageClickEventArgs e)
        {
            try
            {
                //Page.Validate("GrpRowDebit");
                //if (!Page.IsValid)
                //{
                //    return;
                //}
                if (ViewState["CurrentTableDB2"] != null)
                {
                    DataTable dtCurrentTable = (DataTable)ViewState["CurrentTableDB2"];
                    DataRow drCurrentRow = null;
                    if (dtCurrentTable.Rows.Count > 0)
                    {

                        for (int i = 0; i <= dtCurrentTable.Rows.Count - 1; i++)
                        {
                            drCurrentRow = dtCurrentTable.NewRow();
                            drCurrentRow["DbHeads2"] = ddlHeadsDebit2.SelectedItem.Text;
                            drCurrentRow["DbDescription2"] = txtDebitdesc2.Text;
                            drCurrentRow["Dbheadid2"] = ddlHeadsDebit2.SelectedValue;
                            drCurrentRow["DbAmount2"] = debitAmnt2.Text;
                        }

                        //Remove initial blank row
                        if (dtCurrentTable.Rows[0][0].ToString() == "")
                        {
                            dtCurrentTable.Rows[0].Delete();
                            dtCurrentTable.AcceptChanges();
                        }

                        //Rebind the Grid with the current data
                        drCurrentRow["RowNumber"] = dtCurrentTable.Rows.Count + 1;

                        dtCurrentTable.Rows.Add(drCurrentRow);
                        ViewState["CurrentTableDB2"] = dtCurrentTable;
                        GrdDbClnt2.DataSource = dtCurrentTable;
                        GrdDbClnt2.DataBind();
                        //Rebind the Grid with the current data
                        hiddenamnt2 = Convert.ToDouble(hidden_totalcred2.Value);

                        ddlHeadsDebit2.ClearSelection();
                        txtDebitdesc2.Text = "";
                        debitAmnt2.Text = "";
                    }
                }
            }
            catch (Exception) { }
        }

        protected void btnAdd3_GridGuardiansDebit_RowCommand_click(object sender, ImageClickEventArgs e)
        {
            try
            {
                //Page.Validate("GrpRowDebit");
                //if (!Page.IsValid)
                //{
                //    return;
                //}
                if (ViewState["CurrentTableDB3"] != null)
                {
                    DataTable dtCurrentTable = (DataTable)ViewState["CurrentTableDB3"];
                    DataRow drCurrentRow = null;
                    if (dtCurrentTable.Rows.Count > 0)
                    {

                        for (int i = 0; i <= dtCurrentTable.Rows.Count - 1; i++)
                        {
                            drCurrentRow = dtCurrentTable.NewRow();
                            drCurrentRow["DbHeads3"] = ddlHeadsDebit3.SelectedItem.Text;
                            drCurrentRow["DbDescription3"] = txtDebitdesc3.Text;
                            drCurrentRow["Dbheadid3"] = ddlHeadsDebit3.SelectedValue;
                            drCurrentRow["DbAmount3"] = debitAmnt3.Text;
                        }

                        //Remove initial blank row
                        if (dtCurrentTable.Rows[0][0].ToString() == "")
                        {
                            dtCurrentTable.Rows[0].Delete();
                            dtCurrentTable.AcceptChanges();
                        }

                        //Rebind the Grid with the current data
                        drCurrentRow["RowNumber"] = dtCurrentTable.Rows.Count + 1;

                        dtCurrentTable.Rows.Add(drCurrentRow);
                        ViewState["CurrentTableDB3"] = dtCurrentTable;
                        GrdDbClnt3.DataSource = dtCurrentTable;
                        GrdDbClnt3.DataBind();

                        //Rebind the Grid with the current data
                        hiddenamnt3 = Convert.ToDouble(hidden_totalcred3.Value);

                        ddlHeadsDebit3.ClearSelection();
                        txtDebitdesc3.Text = "";
                        debitAmnt3.Text = "";
                    }
                }
            }
            catch (Exception) { }
        }
        
        protected void btnAdd1_GridGuardians_RowCommand_click(object sender, ImageClickEventArgs e)
        {
            try
            {
                //Page.Validate("GrpRow");
                //if (!Page.IsValid)
                //{
                //    return;
                //}

                if (balayer.GetSingleValue("SELECT TreeHint FROM `headstree` where NodeID='" + ddlHeads1.SelectedValue + "'").StartsWith("3,"))
                {
                    return;
                }

                //*********************************
                lblcancelmsg1.Text = "";
                if (ddlHeads1.SelectedValue.Contains(':'))
                {
                    rootid1 = 1;
                }
                else
                {
                    rootid1 = int.Parse(ddlHeads1.SelectedValue.Split(',')[0].ToString().Trim().Trim(',').Trim());
                }
                if ((rootid1 == 3) && (txtChequeNO1.Visible == true) && (txtChequeNO1.Text == ""))
                {

                }
                else
                {
                    if (ViewState["CurrentTable1"] != null)
                    {
                        credittotal1 = Convert.ToDouble(txtAmount1.Text);
                        hidden_totalcred1.Value = (existing_crdtotal1 + credittotal1).ToString();

                        DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable1"];
                        DataRow drCurrentRow = null;
                        if (dtCurrentTable.Rows.Count > 0)
                        {

                            for (int i = 0; i <= dtCurrentTable.Rows.Count - 1; i++)
                            {
                                drCurrentRow = dtCurrentTable.NewRow();

                                drCurrentRow["Heads1"] = ddlHeads1.SelectedItem.Text;
                                drCurrentRow["Amount1"] = txtAmount1.Text;
                                drCurrentRow["Description1"] = txtDescription1.Text;
                                drCurrentRow["chequeNO1"] = txtChequeNO1.Text;
                                drCurrentRow["headid1"] = ddlHeads1.SelectedValue;
                            }

                            //Remove initial blank row
                            if (dtCurrentTable.Rows[0][0].ToString() == "")
                            {
                                dtCurrentTable.Rows[0].Delete();
                                dtCurrentTable.AcceptChanges();
                            }
                            //Rebind the Grid with the current data
                            drCurrentRow["RowNumber"] = dtCurrentTable.Rows.Count + 1;

                            dtCurrentTable.Rows.Add(drCurrentRow);
                            ViewState["CurrentTable1"] = dtCurrentTable;

                            //Rebind the Grid with the current data
                            GridClnt1.DataSource = dtCurrentTable;
                            GridClnt1.DataBind();                          

                            ddlHeads1.ClearSelection();
                            txtAmount1.Text = "";
                            txtDescription1.Text = "";
                            txtChequeNO1.Text = "";
                            //ViewState["tabnum"] = (57 + GViewCR_Selected.Rows.Count * 4);

                            // SetGridTabIndex(true);

                            ScriptManager.RegisterClientScriptBlock(this, GetType(), "none", "<script>GetVisible1();</script>", false);
                        }
                    }
                }

                //**************************************

            }
            catch (Exception) { }
        }

        protected void btnAdd2_GridGuardians_RowCommand_click(object sender, ImageClickEventArgs e)
        {
            try
            {
                //Page.Validate("GrpRow");
                //if (!Page.IsValid)
                //{
                //    return;
                //}

                if (balayer.GetSingleValue("SELECT TreeHint FROM `headstree` where NodeID='" + ddlHeads2.SelectedValue + "'").StartsWith("3,"))
                {
                    return;
                }

                //*********************************
                lblcancelmsg2.Text = "";
                if (ddlHeads2.SelectedValue.Contains(':'))
                {
                    rootid2 = 1;
                }
                else
                {
                    rootid2 = int.Parse(ddlHeads2.SelectedValue.Split(',')[0].ToString().Trim().Trim(',').Trim());
                }
                if ((rootid2 == 3) && (txtChequeNO2.Visible == true) && (txtChequeNO2.Text == ""))
                {

                }
                else
                {
                    if (ViewState["CurrentTable2"] != null)
                    {
                        credittotal2 = Convert.ToDouble(txtAmount2.Text);
                        hidden_totalcred2.Value = (existing_crdtotal2 + credittotal2).ToString();

                        DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable2"];
                        DataRow drCurrentRow = null;
                        if (dtCurrentTable.Rows.Count > 0)
                        {

                            for (int i = 0; i <= dtCurrentTable.Rows.Count - 1; i++)
                            {
                                drCurrentRow = dtCurrentTable.NewRow();

                                drCurrentRow["Heads2"] = ddlHeads2.SelectedItem.Text;
                                drCurrentRow["Amount2"] = txtAmount2.Text;
                                drCurrentRow["Description2"] = txtDescription2.Text;
                                drCurrentRow["chequeNO2"] = txtChequeNO2.Text;
                                drCurrentRow["headid2"] = ddlHeads2.SelectedValue;
                            }

                            //Remove initial blank row
                            if (dtCurrentTable.Rows[0][0].ToString() == "")
                            {
                                dtCurrentTable.Rows[0].Delete();
                                dtCurrentTable.AcceptChanges();
                            }
                            //Rebind the Grid with the current data
                            drCurrentRow["RowNumber"] = dtCurrentTable.Rows.Count + 1;

                            dtCurrentTable.Rows.Add(drCurrentRow);
                            ViewState["CurrentTable2"] = dtCurrentTable;

                            //Rebind the Grid with the current data
                            GridClnt2.DataSource = dtCurrentTable;
                            GridClnt2.DataBind();

                            ddlHeads2.ClearSelection();
                            txtAmount2.Text = "";
                            txtDescription2.Text = "";
                            txtChequeNO2.Text = "";
                            //ViewState["tabnum"] = (57 + GViewCR_Selected.Rows.Count * 4);

                            // SetGridTabIndex(true);

                            ScriptManager.RegisterClientScriptBlock(this, GetType(), "none", "<script>GetVisible2();</script>", false);
                        }
                    }
                }

                //**************************************

            }
            catch (Exception) { }
        }

        protected void btnAdd3_GridGuardians_RowCommand_click(object sender, ImageClickEventArgs e)
        {
            try
            {
                //Page.Validate("GrpRow");
                //if (!Page.IsValid)
                //{
                //    return;
                //}

                if (balayer.GetSingleValue("SELECT TreeHint FROM `headstree` where NodeID='" + ddlHeads3.SelectedValue + "'").StartsWith("3,"))
                {
                    return;
                }

                //*********************************
                lblcancelmsg3.Text = "";
                if (ddlHeads3.SelectedValue.Contains(':'))
                {
                    rootid3 = 1;
                }
                else
                {
                    rootid3 = int.Parse(ddlHeads3.SelectedValue.Split(',')[0].ToString().Trim().Trim(',').Trim());
                }
                if ((rootid3 == 3) && (txtChequeNO3.Visible == true) && (txtChequeNO3.Text == ""))
                {

                }
                else
                {
                    if (ViewState["CurrentTable3"] != null)
                    {
                        credittotal3 = Convert.ToDouble(txtAmount3.Text);
                        hidden_totalcred3.Value = (existing_crdtotal3 + credittotal3).ToString();

                        DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable3"];
                        DataRow drCurrentRow = null;
                        if (dtCurrentTable.Rows.Count > 0)
                        {

                            for (int i = 0; i <= dtCurrentTable.Rows.Count - 1; i++)
                            {
                                drCurrentRow = dtCurrentTable.NewRow();

                                drCurrentRow["Heads3"] = ddlHeads3.SelectedItem.Text;
                                drCurrentRow["Amount3"] = txtAmount3.Text;
                                drCurrentRow["Description3"] = txtDescription3.Text;
                                drCurrentRow["chequeNO3"] = txtChequeNO3.Text;
                                drCurrentRow["headid3"] = ddlHeads3.SelectedValue;
                            }

                            //Remove initial blank row
                            if (dtCurrentTable.Rows[0][0].ToString() == "")
                            {
                                dtCurrentTable.Rows[0].Delete();
                                dtCurrentTable.AcceptChanges();
                            }
                            //Rebind the Grid with the current data
                            drCurrentRow["RowNumber"] = dtCurrentTable.Rows.Count + 1;

                            dtCurrentTable.Rows.Add(drCurrentRow);
                            ViewState["CurrentTable3"] = dtCurrentTable;

                            //Rebind the Grid with the current data
                            GridClnt3.DataSource = dtCurrentTable;
                            GridClnt3.DataBind();

                            ddlHeads3.ClearSelection();
                            txtAmount3.Text = "";
                            txtDescription3.Text = "";
                            txtChequeNO3.Text = "";
                            //ViewState["tabnum"] = (57 + GViewCR_Selected.Rows.Count * 4);

                            // SetGridTabIndex(true);

                            ScriptManager.RegisterClientScriptBlock(this, GetType(), "none", "<script>GetVisible3();</script>", false);
                        }
                    }
                }

                //**************************************

            }
            catch (Exception) { }
        }

        private void SetInitRow_Clnt1()
        {
            DataTable dt = new DataTable();
            DataRow dr = null;
            //Define the Columns           
            dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
            dt.Columns.Add(new DataColumn("Heads1", typeof(string)));
            dt.Columns.Add(new DataColumn("Amount1", typeof(string)));
            dt.Columns.Add(new DataColumn("Description1", typeof(string)));
            dt.Columns.Add(new DataColumn("chequeNO1", typeof(string)));
            dt.Columns.Add(new DataColumn("headid1", typeof(string)));
            //Add a Dummy Data on Initial Load
            dr = dt.NewRow();
            dt.Rows.Add(dr);
            ViewState["CurrentTable1"] = dt;

            if (dt.Rows.Count == 0)
            {
                //If no records then add a dummy row.
                dt.Rows.Add();
            }
            GridClnt1.DataSource = dt;
            GridClnt1.DataBind();
            dt.Dispose();
        }

        private void SetInitRow_Clnt2()
        {
            DataTable dt = new DataTable();
            DataRow dr = null;
            //Define the Columns           
            dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
            dt.Columns.Add(new DataColumn("Heads2", typeof(string)));
            dt.Columns.Add(new DataColumn("Amount2", typeof(string)));
            dt.Columns.Add(new DataColumn("Description2", typeof(string)));
            dt.Columns.Add(new DataColumn("chequeNO2", typeof(string)));
            dt.Columns.Add(new DataColumn("headid2", typeof(string)));
            //Add a Dummy Data on Initial Load
            dr = dt.NewRow();
            dt.Rows.Add(dr);
            ViewState["CurrentTable2"] = dt;

            if (dt.Rows.Count == 0)
            {
                //If no records then add a dummy row.
                dt.Rows.Add();
            }
            GridClnt2.DataSource = dt;
            GridClnt2.DataBind();
            dt.Dispose();
        }

        private void SetInitRow_Clnt3()
        {
            DataTable dt = new DataTable();
            DataRow dr = null;
            //Define the Columns           
            dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
            dt.Columns.Add(new DataColumn("Heads3", typeof(string)));
            dt.Columns.Add(new DataColumn("Amount3", typeof(string)));
            dt.Columns.Add(new DataColumn("Description3", typeof(string)));
            dt.Columns.Add(new DataColumn("chequeNO3", typeof(string)));
            dt.Columns.Add(new DataColumn("headid3", typeof(string)));
            //Add a Dummy Data on Initial Load
            dr = dt.NewRow();
            dt.Rows.Add(dr);
            ViewState["CurrentTable3"] = dt;

            if (dt.Rows.Count == 0)
            {
                //If no records then add a dummy row.
                dt.Rows.Add();
            }
            GridClnt3.DataSource = dt;
            GridClnt3.DataBind();
            dt.Dispose();
        }
        
        private void SetInitDb_Clnt1()
        {
            DataTable dt = new DataTable();
            DataRow dr = null;
            //Define the Columns           
            dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
            dt.Columns.Add(new DataColumn("DbHeads1", typeof(string)));
            dt.Columns.Add(new DataColumn("DbAmount1", typeof(string)));
            dt.Columns.Add(new DataColumn("DbDescription1", typeof(string)));
            dt.Columns.Add(new DataColumn("Dbheadid1", typeof(string)));
            //Add a Dummy Data on Initial Load
            dr = dt.NewRow();
            dt.Rows.Add(dr);
            ViewState["CurrentTableDB1"] = dt;

            if (dt.Rows.Count == 0)
            {
                //If no records then add a dummy row.
                dt.Rows.Add();
            }
            GrdDbClnt1.DataSource = dt;
            GrdDbClnt1.DataBind();
            dt.Dispose();
        }

        private void SetInitDb_Clnt2()
        {
            DataTable dt = new DataTable();
            DataRow dr = null;
            //Define the Columns           
            dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
            dt.Columns.Add(new DataColumn("DbHeads2", typeof(string)));
            dt.Columns.Add(new DataColumn("DbAmount2", typeof(string)));
            dt.Columns.Add(new DataColumn("DbDescription2", typeof(string)));
            dt.Columns.Add(new DataColumn("Dbheadid2", typeof(string)));
            //Add a Dummy Data on Initial Load
            dr = dt.NewRow();
            dt.Rows.Add(dr);
            ViewState["CurrentTableDB2"] = dt;

            if (dt.Rows.Count == 0)
            {
                //If no records then add a dummy row.
                dt.Rows.Add();
            }
            GrdDbClnt2.DataSource = dt;
            GrdDbClnt2.DataBind();
            dt.Dispose();
        }

        private void SetInitDb_Clnt3()
        {
            DataTable dt = new DataTable();
            DataRow dr = null;
            //Define the Columns           
            dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
            dt.Columns.Add(new DataColumn("DbHeads3", typeof(string)));
            dt.Columns.Add(new DataColumn("DbAmount3", typeof(string)));
            dt.Columns.Add(new DataColumn("DbDescription3", typeof(string)));
            dt.Columns.Add(new DataColumn("Dbheadid3", typeof(string)));
            //Add a Dummy Data on Initial Load
            dr = dt.NewRow();
            dt.Rows.Add(dr);
            ViewState["CurrentTableDB3"] = dt;

            if (dt.Rows.Count == 0)
            {
                //If no records then add a dummy row.
                dt.Rows.Add();
            }
            GrdDbClnt3.DataSource = dt;
            GrdDbClnt3.DataBind();
            dt.Dispose();
        }

        private void SetDefaultRowDB()
        {
            DataTable dt = new DataTable();
        }
        public static string GetUniqueKey(int maxSize)
        {
            char[] chars = new char[62];
            chars = "123456789".ToCharArray();
            byte[] data = new byte[1];
            RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider();
            crypto.GetNonZeroBytes(data);
            data = new byte[maxSize];
            crypto.GetNonZeroBytes(data);
            StringBuilder result = new StringBuilder(maxSize);
            foreach (byte b in data)
            {
                result.Append(chars[b % (chars.Length)]);
            }
            return result.ToString();
        }
        protected void btnYes_Click(object sender, EventArgs e)
        {
            try
            {
                string strBranchID = balayer.ToobjectstrEvenNull(Session["Branchid"]);
                if (strBranchID == "161") strBranchID = "160";

                lblcancelmsg1.Text = "";
                lblcancelmsg2.Text = "";
                lblcancelmsg3.Text = "";
                //TransactionLayer trn = new TransactionLayer();
                TransactionLayer trn = new TransactionLayer();

                string hexstring1;
                string guidForBinary161;
                string DualTransactionKey1;
                string TransactionKey1;
                bool isFailed1 = false, isDate1;
                DateTime dtChoosenDate1;
                string NodeID1;
                string chitGroupId1;
                string strCreditHeadQuery1 = "";
                long iResult1;
                string strBankInsertQuery1;

                string hexstring2;
                string guidForBinary162;
                string DualTransactionKey2;
                string TransactionKey2;
                bool isFailed2 = false, isDate2;
                DateTime dtChoosenDate2;
                string NodeID2;
                string chitGroupId2;
                string strCreditHeadQuery2 = "";
                long iResult2;
                string strBankInsertQuery2;

                string hexstring3;
                string guidForBinary163;
                string DualTransactionKey3;
                string TransactionKey3;
                bool isFailed3 = false, isDate3;
                DateTime dtChoosenDate3;
                string NodeID3;
                string chitGroupId3;
                string strCreditHeadQuery3 = "";
                long iResult3;
                string strBankInsertQuery3;

                #region Vardeclaration
                string CRtxtsubamnt1;
                string CRtxtdesc1;
                string CRtxtcheqno1;
                string insert11 = "";
                string CredDescription1 = "";
                string DebDescription1 = "";

                string CRtxtsubamnt2;
                string CRtxtdesc2;
                string CRtxtcheqno2;
                string insert12 = "";
                string CredDescription2 = "";
                string DebDescription2 = "";

                string CRtxtsubamnt3;
                string CRtxtdesc3;
                string CRtxtcheqno3;
                string insert13 = "";
                string CredDescription3 = "";
                string DebDescription3 = "";
                #endregion

                if (lblHint.Text == "Reload")
                {
                    lblHint.Text = "";
                    this.Response.Redirect(this.Request.Url.AbsolutePath.ToString());
                }
                else
                    if (lblHint.Text == "VExist")
                    {
                        if (txtVoucherNo1.Text != "")
                            balayer.ExecuteQuery("Delete From `svcf`.`voucher` Where Series = 'ZZZ' And Voucher_No = " + txtVoucherNo1.Text);
                        txtVoucherNo1.Text = "";
                        txtSeries1.Text = "";
                        txtSeries1.Focus();
                        lblHint.Text = "";

                        if (txtVoucherNo2.Text != "")
                            balayer.ExecuteQuery("Delete From `svcf`.`voucher` Where Series = 'ZZZ' And Voucher_No = " + txtVoucherNo2.Text);
                        txtVoucherNo2.Text = "";
                        txtSeries2.Text = "";
                        txtSeries2.Focus();
                        lblHint.Text = "";

                        if (txtVoucherNo3.Text != "")
                            balayer.ExecuteQuery("Delete From `svcf`.`voucher` Where Series = 'ZZZ' And Voucher_No = " + txtVoucherNo3.Text);
                        txtVoucherNo3.Text = "";
                        txtSeries3.Text = "";
                        txtSeries3.Focus();
                        lblHint.Text = "";
                    }
                    else
                        if (lblHint.Text == "HeadConfirmation")
                        {
                            lblHint.Text = "";
                        }
                        else
                            if (lblHint.Text.ToLower() == "vconfirm")
                            {
                                try
                                {
                                    System.Guid guid1 = Guid.NewGuid();
                                    // Prepare GUID values in SQL format
                                    hexstring1 = BitConverter.ToString(guid1.ToByteArray());
                                    guidForBinary161 = "0x" + hexstring1.Replace("-", string.Empty);
                                    DualTransactionKey1 = guidForBinary161;
                                    GetSeriesAndVoucherNo(1);

                                    isFailed1 = false;
                                    dtChoosenDate1 = DateTime.Parse(txtDate1.Text);
                                    #region VarDeclaration
                                    string[] ChitGpNo1;
                                    string[] ChitGpId1;
                                    int KsrHeadID1 = 0, KsrNoOfMem1 = 0;
                                    string qry1 = "";
                                    MySqlDataReader dr1;
                                    List<string> GetMemList1 = new List<string>();
                                    double kasaramnt1 = 0;
                                    DateTime Ksrchoosendt1;
                                    string ddlSubsHeads1 = "";
                                    string ddlheads1 = "";
                                    string Tot_KsrAmnt1 = "";

                                    string subheadval1 = "";
                                    string ddlSubsHeadsisub1 = "";
                                    string ddlsubsheadvalue1 = "";
                                    string CR_HSelval1 = "";
                                    string chit_gpid1 = "";
                                    string strDebitHeadQuery1 = "";
                                    bool getvc1 = false;
                                    DataTable getcredit1 = new DataTable();
                                    DataTable getdebit1 = new DataTable();
                                    DataTable CRDT1 = new DataTable();
                                    DataTable DBDT1 = new DataTable();
                                    #endregion

                                    DateTime d1;
                                    isDate1 = DateTime.TryParseExact(txtDate1.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out d1);
                                    string trans_medium1 = "";
                                    string memberid1 = "";

                                    CRDT1 = (DataTable)ViewState["CurrentTable1"];
                                    DBDT1 = (DataTable)ViewState["CurrentTableDB1"];

                                    qry1 = "select * from  `svcf`.`voucher` where Voucher_No=" + txtVoucherNo1.Text + " and BranchID=" + strBranchID + " and Series='VOUCHER'";
                                    getdebit1 = balayer.GetDataTable(qry1);
                                    if (getdebit1.Rows.Count == 0)
                                    {

                                        #region Voucherinsert

                                        #region GeTRootid

                                        for (int iTrans = 0; iTrans < CRDT1.Rows.Count; iTrans++)
                                        {
                                            ddlSubsHeads1 = CRDT1.Rows[iTrans]["Heads1"].ToString();    //GViewCR_Selected.Rows[iTrans].Cells[1].Text;
                                            subheadval1 = CRDT1.Rows[iTrans]["headid1"].ToString();
                                            ddlSubsHeads1 = ddlSubsHeads1.Replace("&gt;&gt;", ">>");
                                            if (ddlSubsHeads1 == "--Select--")
                                            {
                                                ddlSubsHeads1 = "";
                                            }
                                            if (subheadval1.Contains("|"))
                                            {
                                                rootid1 = 5;
                                                trans_medium1 = "0";
                                                break;
                                            }
                                            else
                                            {
                                                //rootid = int.Parse(ddlSubsHeads.SelectedValue.ToString().Split(',')[0].ToString().Trim().Trim(',').Trim());
                                                rootid1 = int.Parse(subheadval1.Split(':')[0].ToString().Trim().Trim(':').Trim());
                                                if (rootid1 == 3)
                                                {
                                                    trans_medium1 = "1";
                                                    break;
                                                }
                                                else if (rootid1 == 12)
                                                {
                                                    trans_medium1 = "0";
                                                    break;
                                                }
                                                else if (rootid1 == 5)
                                                {
                                                    for (int isub = 0; isub < DBDT1.Rows.Count; isub++)
                                                    {
                                                        ddlSubsHeadsisub1 = DBDT1.Rows[isub]["DbHeads1"].ToString(); //GViewDB_Selected.Rows[isub].Cells[1].Text;
                                                        ddlSubsHeadsisub1 = ddlSubsHeadsisub1.Replace("&gt;&gt;", ">>");
                                                        ddlsubsheadvalue1 = DBDT1.Rows[isub]["Dbheadid1"].ToString();    //Dbheadid[isub];
                                                        int subrootid1 = int.Parse(ddlsubsheadvalue1.Split(':')[0].ToString().Trim().Trim(':').Trim());
                                                        if (subrootid1 == 1 || subrootid1 == 6)
                                                        {
                                                            trans_medium1 = "1";
                                                            break;
                                                        }
                                                    }
                                                    break;
                                                }
                                                else if (rootid1 == 1)
                                                {
                                                    for (int isub = 0; isub < DBDT1.Rows.Count; isub++)
                                                    {
                                                        ddlSubsHeadsisub1 = DBDT1.Rows[isub]["DbHeads1"].ToString(); //GViewDB_Selected.Rows[isub].Cells[1].Text;
                                                        ddlSubsHeadsisub1 = ddlSubsHeadsisub1.Replace("&gt;&gt;", ">>");
                                                        ddlsubsheadvalue1 = DBDT1.Rows[isub]["Dbheadid1"].ToString();
                                                        //Label ddlsubsheadvalue = (Label)GViewDB_Selected.Rows[isub].FindControl("lbldbhdid");
                                                        int subrootid1 = int.Parse(ddlsubsheadvalue1.Split(':')[0].ToString().Trim().Trim(':').Trim());
                                                        if (subrootid1 == 12)
                                                        {
                                                            trans_medium1 = "0";
                                                            break;
                                                        }
                                                        else if (subrootid1 == 3)
                                                        {
                                                            trans_medium1 = "1";
                                                            break;
                                                        }
                                                        //nithi
                                                        //if branch credit as well as branch debit
                                                        else if (subrootid1 == 1)
                                                        {
                                                            trans_medium1 = "1";
                                                            break;
                                                        }
                                                        //---------
                                                    }
                                                    break;
                                                }
                                            }
                                        }

                                        if (trans_medium1 == "")
                                        {
                                            for (int iTrans = 0; iTrans < DBDT1.Rows.Count; iTrans++)
                                            {
                                                ddlSubsHeadsisub1 = DBDT1.Rows[iTrans]["DbHeads1"].ToString();     // GViewDB_Selected.Rows[iTrans].Cells[1].Text;
                                                ddlSubsHeadsisub1 = ddlSubsHeadsisub1.Replace("&gt;&gt;", ">>");
                                                ddlsubsheadvalue1 = DBDT1.Rows[iTrans]["Dbheadid1"].ToString();
                                                //Label ddlsubsheadvalue = (Label)GViewDB_Selected.Rows[iTrans].FindControl("lbldbhdid");
                                                //int rootid = int.Parse(ddlSubsdebHeads.SelectedValue.ToString().Split(',')[0].ToString().Trim().Trim(',').Trim());
                                                int rootid1 = int.Parse(ddlsubsheadvalue1.Split(':')[0].ToString().Trim().Trim(':').Trim());
                                                if (rootid1 == 3)
                                                {
                                                    trans_medium1 = "1";
                                                    break;
                                                }
                                                else if (rootid1 == 12)
                                                {
                                                    trans_medium1 = "0";
                                                    break;
                                                }
                                            }
                                        }
                                        if (trans_medium1 == "")
                                        {
                                            trans_medium1 = "3";
                                        }

                                        #endregion

                                        strCreditHeadQuery1 = "";
                                        strBankInsertQuery1 = "";

                                        #region CreidtInsert


                                        for (int iTrans = 0; iTrans < CRDT1.Rows.Count; iTrans++)
                                        {
                                            ddlheads1 = CRDT1.Rows[iTrans]["Heads1"].ToString();   //GViewCR_Selected.Rows[iTrans].Cells[1].Text;
                                            ddlheads1 = ddlheads1.Replace("&gt;&gt;", ">>");
                                            CredDescription1 = CRDT1.Rows[iTrans]["Description1"].ToString();    //GViewCR_Selected.Rows[iTrans].Cells[3].Text;
                                            CR_HSelval1 = CRDT1.Rows[iTrans]["headid1"].ToString();

                                            if (!ddlheads1.Contains("|"))
                                            {
                                                int rootid1 = int.Parse(CR_HSelval1.Split(':')[0].ToString().Trim().Trim(':').Trim());
                                                if (rootid1 == 5)
                                                {
                                                    memberid1 = balayer.GetSingleValue("select `MemberID` from membertogroupmaster where `Head_Id`=" + CR_HSelval1.Split(':')[1].ToString().Trim().Trim(':').Trim());
                                                }
                                                else
                                                {
                                                    memberid1 = "0";
                                                }
                                                if (memberid1 == "")
                                                {
                                                    memberid1 = "0";
                                                }

                                                CRtxtsubamnt1 = CRDT1.Rows[iTrans]["Amount1"].ToString();    //GViewCR_Selected.Rows[iTrans].Cells[2].Text;
                                                NodeID1 = CR_HSelval1.Split(':')[1].Trim();
                                                string txtChk1 = CRDT1.Rows[iTrans]["chequeNO1"].ToString();    //GViewCR_Selected.Rows[iTrans].Cells[4].Text;
                                                if (txtChk1 == "&nbsp;")
                                                {
                                                    txtChk1 = "";
                                                }
                                                chitGroupId1 = "0";
                                                if (int.Parse(CR_HSelval1.Split(':')[0].ToString().Trim().Trim(':').Trim()) == 5)
                                                {
                                                    chitGroupId1 = balayer.GetSingleValue("select ParentID from headstree where NodeID=" + CR_HSelval1.Split(':')[1].ToString());
                                                }

                                                strCreditHeadQuery1 = "INSERT INTO `svcf`.`voucher` (`DualTransactionKey`,`BranchID`,`CurrDate`,`Voucher_No`,`Voucher_Type`,`Head_Id`,`ChoosenDate`,`Narration`,`Amount`,`Series`,`ReceievedBy`,`Trans_Type`,`T_Day`,`T_Month`,`T_Year`,`MemberID`,`Trans_Medium`,`RootID`,`ChitGroupId`) VALUES (" + DualTransactionKey1 + "," + strBranchID + ",'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," + txtVoucherNo1.Text + ",'C'," + CR_HSelval1.Split(':')[1].ToString().Trim() + ",'" + dtChoosenDate1.ToString("yyyy-MM-dd") + "','" + balayer.ReplaceJnk(CredDescription1) + "'," + CRtxtsubamnt1 + ",'" + txtSeries1.Text.ToString().Trim() + "','" + balayer.ReplaceJnk(txtReceivedBy1.Text) + "',0," + dtChoosenDate1.Day + "," + dtChoosenDate1.Month + "," + dtChoosenDate1.Year + "," + memberid1 + "," + trans_medium1 + "," + CR_HSelval1.Split(':')[0] + "," + chitGroupId1 + "); ";
                                                iResult1 = trn.insertorupdateTrn(strCreditHeadQuery1);
                                                if (txtChk1.Trim() != "")
                                                {
                                                    strBankInsertQuery1 = "insert into transbank(BranchID, TransactionKey, DualTransactionKey, T_Day, T_Month, T_Year, BankHeadID, Head_Id, MemberID, CustomersBankName, DateInCheque, ChequeDDNO, GivenAmount, TotalChequeDDAmount, IsBounced, Trans_Type) values (" + strBranchID + "," + balayer.ToobjectstrEvenNull((object)iResult1) + "," + DualTransactionKey1 + "," + dtChoosenDate1.Day + "," + dtChoosenDate1.Month + "," + dtChoosenDate1.Year + "," + CR_HSelval1.Split(':')[1].ToString().Trim() + ",000,000,'','" + balayer.indiandateToMysqlDate(txtDate1.Text) + "'," + txtChk1 + "," + CRtxtsubamnt1 + "," + CRtxtsubamnt1 + ",0,0);";

                                                    trn.insertorupdateTrn(strBankInsertQuery1);
                                                }
                                            }
                                            else
                                            {
                                                ddlheads1 = CRDT1.Rows[iTrans]["Heads1"].ToString();   //GViewCR_Selected.Rows[iTrans].Cells[1].Text;
                                                ChitGpNo1 = ddlheads1.Split('|');
                                                if (ChitGpNo1[0] == ChitGpNo1[1])
                                                {
                                                    try
                                                    {
                                                        //Kasar Entry
                                                        Ksrchoosendt1 = Convert.ToDateTime(txtDate1.Text);
                                                        Tot_KsrAmnt1 = CRDT1.Rows[iTrans]["Amount1"].ToString();    //amntlist[iTrans];    //GViewCR_Selected.Rows[iTrans].Cells[2].Text;
                                                        chit_gpid1 = CRDT1.Rows[iTrans]["headid1"].ToString();
                                                        ChitGpId1 = chit_gpid1.Split('|');
                                                        qry1 = "SELECT Head_Id,NoofMembers FROM svcf.groupmaster where Head_Id=" + ChitGpId1[0] + "";
                                                        dr1 = balayer.ExecuteReader(qry1);
                                                        qry1 = "";
                                                        while (dr1.Read())
                                                        {
                                                            KsrHeadID1 = Convert.ToInt32(dr1[0]);
                                                            KsrNoOfMem1 = Convert.ToInt32(dr1[1]);
                                                        }
                                                        dr1.Dispose();

                                                        qry1 = "SELECT Head_Id FROM svcf.membertogroupmaster where GroupID=" + KsrHeadID1 + "";
                                                        GetMemList1 = balayer.RetrveList(qry1);
                                                        kasaramnt1 = Convert.ToDouble(Tot_KsrAmnt1) / KsrNoOfMem1;
                                                        //Insert Kasary amount
                                                        for (int li = 0; li <= GetMemList1.Count - 1; li++)
                                                        {
                                                            getvc1 = balayer.CheckVoucher_Exist(Convert.ToInt32(txtVoucherNo1.Text), Convert.ToInt32(strBranchID));
                                                            if (getvc1 == false)
                                                            {
                                                                qry1 = "insert into voucher(DualTransactionKey, BranchID, `CurrDate`, Voucher_No, Voucher_Type, Head_Id, ChoosenDate," +
                                                                "Narration, Amount, Series, ReceievedBy, Trans_Type, T_Day, T_Month, T_Year, MemberID, Trans_Medium, RootID, ChitGroupId, " +
                                                                "Other_Trans_Type) values('" + DualTransactionKey1 + "'," + Convert.ToInt32(strBranchID) + ",'" + balayer.GetChangeDatFormat(d1.Date, 2) + "'," + Convert.ToInt32(txtVoucherNo1.Text) + "," +
                                                                "'C'," + Convert.ToInt32(GetMemList1[li]) + ",'" + balayer.GetChangeDatFormat(Ksrchoosendt1, 2) + "','" + ChitGpNo1[0] + ":Redraw Kasar Difference'," +
                                                                "" + kasaramnt1 + ",'Voucher','admin','1'," + Ksrchoosendt1.Date.Day + "," + Ksrchoosendt1.Date.Month + "," + Ksrchoosendt1.Date.Year + ",0," +
                                                                "0,5," + KsrHeadID1 + ",5)";
                                                                balayer.ExecuteQuery(qry1);
                                                            }
                                                        }
                                                    }
                                                    catch (Exception) { }
                                                }
                                                // }
                                            }
                                        }
                                        #endregion

                                        strDebitHeadQuery1 = "";
                                        strBankInsertQuery1 = "";

                                        #region Debit insert

                                        for (int iTrans = 0; iTrans < DBDT1.Rows.Count; iTrans++)
                                        {
                                            ddlheads1 = DBDT1.Rows[iTrans]["DbHeads1"].ToString();    //GViewDB_Selected.Rows[iTrans].Cells[1].Text;
                                            ddlheads1 = ddlheads1.Replace("&gt;&gt;", ">>");
                                            ddlsubsheadvalue1 = DBDT1.Rows[iTrans]["Dbheadid1"].ToString();
                                            //Label ddlsubsheadvalue = (Label)GViewDB_Selected.Rows[iTrans].FindControl("lbldbhdid");
                                            DebDescription1 = DBDT1.Rows[iTrans]["DbDescription1"].ToString();   //GViewDB_Selected.Rows[iTrans].Cells[3].Text;
                                            int rootid1 = int.Parse(ddlsubsheadvalue1.Split(':')[0].ToString().Trim().Trim(':').Trim());
                                            if (rootid1 == 5)
                                            {
                                                memberid1 = balayer.GetSingleValue("select `MemberID` from membertogroupmaster where `Head_Id`=" + ddlsubsheadvalue1.Split(':')[1].ToString().Trim().Trim(':').Trim());
                                            }
                                            else
                                            {
                                                memberid1 = "0";
                                            }
                                            if (memberid1 == "")
                                            {
                                                memberid1 = "0";
                                            }
                                            //TextBox txtSubAmount = (TextBox)GridGuardiansDebit.Rows[iTrans].FindControl("txtAmountDebit");
                                            //TextBox txtDescription = (TextBox)GridGuardiansDebit.Rows[iTrans].FindControl("txtDescriptionDebit");
                                            //NodeID = ddlSubsDBHeads.SelectedValue.Split(',')[1].Trim();
                                            string db_txtSubAmount1 = DBDT1.Rows[iTrans]["DbAmount1"].ToString();   //GViewDB_Selected.Rows[iTrans].Cells[2].Text;
                                            string db_txtDescription1 = DBDT1.Rows[iTrans]["DbDescription1"].ToString();   //ViewDB_Selected.Rows[iTrans].Cells[1].Text;
                                            NodeID1 = ddlsubsheadvalue1.Split(':')[1].Trim();
                                            chitGroupId1 = "0";
                                            if (int.Parse(ddlsubsheadvalue1.Split(':')[0].ToString().Trim().Trim(':').Trim()) == 5)
                                            {
                                                chitGroupId1 = balayer.GetSingleValue("select ParentID from headstree where NodeID=" + ddlsubsheadvalue1.Split(':')[1].ToString());
                                            }
                                            if (chitGroupId1 == "")
                                            {
                                                chitGroupId1 = "0";
                                            }

                                            strDebitHeadQuery1 = "INSERT INTO `svcf`.`voucher` (`DualTransactionKey`,`BranchID`,`CurrDate`,`Voucher_No`,`Voucher_Type`,`Head_Id`,`ChoosenDate`,`Narration`,`Amount`,`Series`,`ReceievedBy`,`Trans_Type`,`T_Day`,`T_Month`,`T_Year`,`MemberID`,`Trans_Medium`,`RootID`,`ChitGroupId`) VALUES (" + DualTransactionKey1 + "," + strBranchID + ",'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," + txtVoucherNo1.Text + ",'D'," + ddlsubsheadvalue1.Split(':')[1].ToString().Trim() + ",'" + dtChoosenDate1.ToString("yyyy-MM-dd") + "','" + balayer.ReplaceJnk(DebDescription1) + "'," + db_txtSubAmount1 + ",'" + txtSeries1.Text.ToString().Trim() + "','" + balayer.ReplaceJnk(txtReceivedBy1.Text) + "',0," + dtChoosenDate1.Day + "," + dtChoosenDate1.Month + "," + dtChoosenDate1.Year + "," + memberid1 + "," + trans_medium1 + "," + ddlsubsheadvalue1.Split(':')[0] + "," + chitGroupId1 + ") ;";
                                            iResult1 = trn.insertorupdateTrn(strDebitHeadQuery1);
                                            if (ddlsubsheadvalue1.Contains("3:"))
                                            {
                                                strBankInsertQuery1 = "insert into transbank(BranchID, TransactionKey, DualTransactionKey, T_Day, T_Month, T_Year, BankHeadID, Head_Id, MemberID, CustomersBankName, DateInCheque, ChequeDDNO, GivenAmount, TotalChequeDDAmount, IsBounced, Trans_Type) values (" + strBranchID + "," + balayer.ToobjectstrEvenNull((object)iResult1) + "," + DualTransactionKey1 + "," + dtChoosenDate1.Day + "," + dtChoosenDate1.Month + "," + dtChoosenDate1.Year + "," + ddlsubsheadvalue1.Split(':')[1].ToString().Trim() + ",000,000,'','" + balayer.indiandateToMysqlDate(txtDate1.Text) + "'," + "000" + "," + db_txtSubAmount1 + "," + db_txtSubAmount1 + ",0,0);";
                                                trn.insertorupdateTrn(strBankInsertQuery1);
                                            }
                                        }
                                        #endregion
                                        #endregion
                                        trn.CommitTrn();
                                    }

                                    CRDT1.Dispose();
                                    DBDT1.Dispose();
                                    strDebitHeadQuery1 = "";
                                    strBankInsertQuery1 = "";
                                    ViewState["CurrentTable1"] = null;
                                    ViewState["CurrentTableDB1"] = null;
                                    ddlHeads1.ClearSelection();
                                    ddlHeadsDebit1.ClearSelection();
                                    balayer.ExecuteQuery("Delete From `svcf`.`voucher` Where Series = 'ZZZ' And Voucher_No = '" + txtVoucherNo1.Text + "'");

                                    if (Session["Branchid"].ToString() == "161")
                                    {
                                        #region Branch2
                                        System.Guid guid2 = Guid.NewGuid();
                                        // Prepare GUID values in SQL format
                                        hexstring2 = BitConverter.ToString(guid2.ToByteArray());
                                        guidForBinary162 = "0x" + hexstring2.Replace("-", string.Empty);
                                        DualTransactionKey2 = guidForBinary162;
                                        GetSeriesAndVoucherNo(2);

                                        isFailed2 = false;
                                        dtChoosenDate2 = DateTime.Parse(txtDate2.Text);
                                        #region VarDeclaration
                                        string[] ChitGpNo2;
                                        string[] ChitGpId2;
                                        int KsrHeadID2 = 0, KsrNoOfMem2 = 0;
                                        string qry2 = "";
                                        MySqlDataReader dr2;
                                        List<string> GetMemList2 = new List<string>();
                                        double kasaramnt2 = 0;
                                        DateTime Ksrchoosendt2;
                                        string ddlSubsHeads2 = "";
                                        string ddlheads2 = "";
                                        string Tot_KsrAmnt2 = "";

                                        string subheadval2 = "";
                                        string ddlSubsHeadsisub2 = "";
                                        string ddlsubsheadvalue2 = "";
                                        string CR_HSelval2 = "";
                                        string chit_gpid2 = "";
                                        string strDebitHeadQuery2 = "";
                                        bool getvc2 = false;
                                        DataTable getcredit2 = new DataTable();
                                        DataTable getdebit2 = new DataTable();
                                        DataTable CRDT2 = new DataTable();
                                        DataTable DBDT2 = new DataTable();
                                        #endregion

                                        DateTime d2;
                                        isDate2 = DateTime.TryParseExact(txtDate2.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out d2);
                                        string trans_medium2 = "";
                                        string memberid2 = "";

                                        CRDT2 = (DataTable)ViewState["CurrentTable2"];
                                        DBDT2 = (DataTable)ViewState["CurrentTableDB2"];

                                        qry2 = "select * from  `svcf`.`voucher` where Voucher_No=" + txtVoucherNo2.Text + " and BranchID=161 and Series='VOUCHER'";
                                        getdebit2 = balayer.GetDataTable(qry2);
                                        if (getdebit2.Rows.Count == 0)
                                        {

                                            #region Voucherinsert

                                            #region GeTRootid

                                            for (int iTrans = 0; iTrans < CRDT2.Rows.Count; iTrans++)
                                            {
                                                ddlSubsHeads2 = CRDT2.Rows[iTrans]["Heads2"].ToString();    //GViewCR_Selected.Rows[iTrans].Cells[1].Text;
                                                subheadval2 = CRDT2.Rows[iTrans]["headid2"].ToString();
                                                ddlSubsHeads2 = ddlSubsHeads2.Replace("&gt;&gt;", ">>");
                                                if (ddlSubsHeads2 == "--Select--")
                                                {
                                                    ddlSubsHeads2 = "";
                                                }
                                                if (subheadval2.Contains("|"))
                                                {
                                                    rootid2 = 5;
                                                    trans_medium2 = "0";
                                                    break;
                                                }
                                                else
                                                {
                                                    //rootid = int.Parse(ddlSubsHeads.SelectedValue.ToString().Split(',')[0].ToString().Trim().Trim(',').Trim());
                                                    rootid2 = int.Parse(subheadval2.Split(':')[0].ToString().Trim().Trim(':').Trim());
                                                    if (rootid2 == 3)
                                                    {
                                                        trans_medium2 = "1";
                                                        break;
                                                    }
                                                    else if (rootid2 == 12)
                                                    {
                                                        trans_medium2 = "0";
                                                        break;
                                                    }
                                                    else if (rootid2 == 5)
                                                    {
                                                        for (int isub = 0; isub < DBDT2.Rows.Count; isub++)
                                                        {
                                                            ddlSubsHeadsisub2 = DBDT2.Rows[isub]["DbHeads2"].ToString(); //GViewDB_Selected.Rows[isub].Cells[1].Text;
                                                            ddlSubsHeadsisub2 = ddlSubsHeadsisub2.Replace("&gt;&gt;", ">>");
                                                            ddlsubsheadvalue2 = DBDT2.Rows[isub]["Dbheadid2"].ToString();    //Dbheadid[isub];
                                                            int subrootid2 = int.Parse(ddlsubsheadvalue2.Split(':')[0].ToString().Trim().Trim(':').Trim());
                                                            if (subrootid2 == 1 || subrootid2 == 6)
                                                            {
                                                                trans_medium2 = "1";
                                                                break;
                                                            }
                                                        }
                                                        break;
                                                    }
                                                    else if (rootid2 == 1)
                                                    {
                                                        for (int isub = 0; isub < DBDT2.Rows.Count; isub++)
                                                        {
                                                            ddlSubsHeadsisub2 = DBDT2.Rows[isub]["DbHeads2"].ToString(); //GViewDB_Selected.Rows[isub].Cells[1].Text;
                                                            ddlSubsHeadsisub2 = ddlSubsHeadsisub2.Replace("&gt;&gt;", ">>");
                                                            ddlsubsheadvalue2 = DBDT2.Rows[isub]["Dbheadid2"].ToString();
                                                            //Label ddlsubsheadvalue = (Label)GViewDB_Selected.Rows[isub].FindControl("lbldbhdid");
                                                            int subrootid2 = int.Parse(ddlsubsheadvalue2.Split(':')[0].ToString().Trim().Trim(':').Trim());
                                                            if (subrootid2 == 12)
                                                            {
                                                                trans_medium2 = "0";
                                                                break;
                                                            }
                                                            else if (subrootid2 == 3)
                                                            {
                                                                trans_medium2 = "1";
                                                                break;
                                                            }
                                                            //nithi
                                                            //if branch credit as well as branch debit
                                                            else if (subrootid2 == 1)
                                                            {
                                                                trans_medium2 = "1";
                                                                break;
                                                            }
                                                            //---------
                                                        }
                                                        break;
                                                        // }
                                                    }
                                                }
                                            }
                                            // }

                                            if (trans_medium2 == "")
                                            {

                                                for (int iTrans = 0; iTrans < DBDT2.Rows.Count; iTrans++)
                                                {
                                                    ddlSubsHeadsisub2 = DBDT2.Rows[iTrans]["DbHeads2"].ToString();     // GViewDB_Selected.Rows[iTrans].Cells[1].Text;
                                                    ddlSubsHeadsisub2 = ddlSubsHeadsisub2.Replace("&gt;&gt;", ">>");
                                                    ddlsubsheadvalue2 = DBDT2.Rows[iTrans]["Dbheadid2"].ToString();
                                                    int rootid2 = int.Parse(ddlsubsheadvalue2.Split(':')[0].ToString().Trim().Trim(':').Trim());
                                                    if (rootid2 == 3)
                                                    {
                                                        trans_medium2 = "1";
                                                        break;
                                                    }
                                                    else if (rootid2 == 12)
                                                    {
                                                        trans_medium2 = "0";
                                                        break;
                                                    }
                                                }
                                            }
                                            if (trans_medium2 == "")
                                            {
                                                trans_medium2 = "3";
                                            }

                                            #endregion

                                            strCreditHeadQuery2 = "";
                                            strBankInsertQuery2 = "";

                                            #region CreidtInsert

                                            for (int iTrans = 0; iTrans < CRDT2.Rows.Count; iTrans++)
                                            {
                                                ddlheads2 = CRDT2.Rows[iTrans]["Heads2"].ToString();   //GViewCR_Selected.Rows[iTrans].Cells[1].Text;
                                                ddlheads2 = ddlheads2.Replace("&gt;&gt;", ">>");
                                                CredDescription2 = CRDT2.Rows[iTrans]["Description2"].ToString();    //GViewCR_Selected.Rows[iTrans].Cells[3].Text;
                                                CR_HSelval2 = CRDT2.Rows[iTrans]["headid2"].ToString();
                                                if (!ddlheads2.Contains("|"))
                                                {
                                                    int rootid2 = int.Parse(CR_HSelval2.Split(':')[0].ToString().Trim().Trim(':').Trim());
                                                    if (rootid2 == 5)
                                                    {
                                                        memberid2 = balayer.GetSingleValue("select `MemberID` from membertogroupmaster where `Head_Id`=" + CR_HSelval2.Split(':')[1].ToString().Trim().Trim(':').Trim());
                                                    }
                                                    else
                                                    {
                                                        memberid2 = "0";
                                                    }
                                                    if (memberid2 == "")
                                                    {
                                                        memberid2 = "0";
                                                    }

                                                    CRtxtsubamnt2 = CRDT2.Rows[iTrans]["Amount2"].ToString();    //GViewCR_Selected.Rows[iTrans].Cells[2].Text;
                                                    NodeID2 = CR_HSelval2.Split(':')[1].Trim();
                                                    string txtChk2 = CRDT2.Rows[iTrans]["chequeNO2"].ToString();    //GViewCR_Selected.Rows[iTrans].Cells[4].Text;
                                                    if (txtChk2 == "&nbsp;")
                                                    {
                                                        txtChk2 = "";
                                                    }
                                                    chitGroupId2 = "0";
                                                    if (int.Parse(CR_HSelval2.Split(':')[0].ToString().Trim().Trim(':').Trim()) == 5)
                                                    {
                                                        chitGroupId2 = balayer.GetSingleValue("select ParentID from headstree where NodeID=" + CR_HSelval2.Split(':')[1].ToString());
                                                    }

                                                    strCreditHeadQuery2 = "INSERT INTO `svcf`.`voucher` (`DualTransactionKey`,`BranchID`,`CurrDate`,`Voucher_No`,`Voucher_Type`,`Head_Id`,`ChoosenDate`,`Narration`,`Amount`,`Series`,`ReceievedBy`,`Trans_Type`,`T_Day`,`T_Month`,`T_Year`,`MemberID`,`Trans_Medium`,`RootID`,`ChitGroupId`) VALUES (" + DualTransactionKey2 + ",161,'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," + txtVoucherNo2.Text + ",'C'," + CR_HSelval2.Split(':')[1].ToString().Trim() + ",'" + dtChoosenDate2.ToString("yyyy-MM-dd") + "','" + balayer.ReplaceJnk(CredDescription2) + "'," + CRtxtsubamnt2 + ",'" + txtSeries2.Text.ToString().Trim() + "','" + balayer.ReplaceJnk(txtReceivedBy2.Text) + "',0," + dtChoosenDate2.Day + "," + dtChoosenDate2.Month + "," + dtChoosenDate2.Year + "," + memberid2 + "," + trans_medium2 + "," + CR_HSelval2.Split(':')[0] + "," + chitGroupId2 + "); ";
                                                    iResult2 = trn.insertorupdateTrn(strCreditHeadQuery2);
                                                    if (txtChk2.Trim() != "")
                                                    {
                                                        //string TransactionKey = balayer.GetSingleValue("SELECT TransactionKey  FROM `svcf`.`voucher` where uuid_from_bin(DualTransactionKey)=uuid_from_bin(" + DualTransactionKey + ") and Head_Id=" + ddlSubsHeads.SelectedValue.Split(',')[1] + " order by TransactionKey desc limit 1;");
                                                        strBankInsertQuery2 = "insert into transbank(BranchID, TransactionKey, DualTransactionKey, T_Day, T_Month, T_Year, BankHeadID, Head_Id, MemberID, CustomersBankName, DateInCheque, ChequeDDNO, GivenAmount, TotalChequeDDAmount, IsBounced, Trans_Type) values (161," + balayer.ToobjectstrEvenNull((object)iResult2) + "," + DualTransactionKey2 + "," + dtChoosenDate2.Day + "," + dtChoosenDate2.Month + "," + dtChoosenDate2.Year + "," + CR_HSelval2.Split(':')[1].ToString().Trim() + ",000,000,'','" + balayer.indiandateToMysqlDate(txtDate2.Text) + "'," + txtChk2 + "," + CRtxtsubamnt2 + "," + CRtxtsubamnt2 + ",0,0);";

                                                        trn.insertorupdateTrn(strBankInsertQuery2);
                                                    }
                                                }
                                                else
                                                {
                                                    ddlheads2 = CRDT2.Rows[iTrans]["Heads2"].ToString();   //GViewCR_Selected.Rows[iTrans].Cells[1].Text;
                                                    ChitGpNo2 = ddlheads2.Split('|');
                                                    if (ChitGpNo2[0] == ChitGpNo2[1])
                                                    {
                                                        try
                                                        {
                                                            //Kasar Entry
                                                            Ksrchoosendt2 = Convert.ToDateTime(txtDate2.Text);
                                                            //DropDownList ddlSHeads = (DropDownList)GridGuardians.Rows[iTrans].FindControl("ddlHeads");
                                                            Tot_KsrAmnt2 = CRDT2.Rows[iTrans]["Amount2"].ToString();    //amntlist[iTrans];    //GViewCR_Selected.Rows[iTrans].Cells[2].Text;
                                                            //TextBox Tot_KsrAmnt = (TextBox)GridGuardians.Rows[iTrans].FindControl("txtAmount");
                                                            //ChitGpId = ddlSHeads.SelectedValue.ToString().Split('|');
                                                            chit_gpid2 = CRDT2.Rows[iTrans]["headid2"].ToString();
                                                            //Label chit_gpid = (Label)GViewCR_Selected.Rows[iTrans].FindControl("lblhdid");
                                                            ChitGpId2 = chit_gpid2.Split('|');
                                                            qry2 = "SELECT Head_Id,NoofMembers FROM svcf.groupmaster where Head_Id=" + ChitGpId2[0] + "";
                                                            dr2 = balayer.ExecuteReader(qry2);
                                                            qry2 = "";
                                                            while (dr2.Read())
                                                            {
                                                                KsrHeadID2 = Convert.ToInt32(dr2[0]);
                                                                KsrNoOfMem2 = Convert.ToInt32(dr2[1]);
                                                            }
                                                            dr2.Dispose();

                                                            qry2 = "SELECT Head_Id FROM svcf.membertogroupmaster where GroupID=" + KsrHeadID2 + "";
                                                            GetMemList2 = balayer.RetrveList(qry2);
                                                            kasaramnt2 = Convert.ToDouble(Tot_KsrAmnt2) / KsrNoOfMem2;
                                                            //Insert Kasary amount
                                                            for (int li = 0; li <= GetMemList2.Count - 1; li++)
                                                            {
                                                                getvc2 = balayer.CheckVoucher_Exist(Convert.ToInt32(txtVoucherNo2.Text), 161);
                                                                if (getvc2 == false)
                                                                {
                                                                    qry2 = "insert into voucher(DualTransactionKey, BranchID, `CurrDate`, Voucher_No, Voucher_Type, Head_Id, ChoosenDate," +
                                                                    "Narration, Amount, Series, ReceievedBy, Trans_Type, T_Day, T_Month, T_Year, MemberID, Trans_Medium, RootID, ChitGroupId, " +
                                                                    "Other_Trans_Type) values('" + DualTransactionKey2 + "',161,'" + balayer.GetChangeDatFormat(d2.Date, 2) + "'," + Convert.ToInt32(txtVoucherNo2.Text) + "," +
                                                                    "'C'," + Convert.ToInt32(GetMemList2[li]) + ",'" + balayer.GetChangeDatFormat(Ksrchoosendt2, 2) + "','" + ChitGpNo2[0] + ":Redraw Kasar Difference'," +
                                                                    "" + kasaramnt2 + ",'Voucher','admin','1'," + Ksrchoosendt2.Date.Day + "," + Ksrchoosendt2.Date.Month + "," + Ksrchoosendt2.Date.Year + ",0," +
                                                                    "0,5," + KsrHeadID2 + ",5)";
                                                                    balayer.ExecuteQuery(qry2);
                                                                }
                                                            }
                                                        }
                                                        catch (Exception) { }
                                                    }
                                                    // }
                                                }
                                            }
                                            #endregion

                                            strDebitHeadQuery2 = "";
                                            strBankInsertQuery2 = "";

                                            #region Debit insert

                                            for (int iTrans = 0; iTrans < DBDT2.Rows.Count; iTrans++)
                                            {
                                                ddlheads2 = DBDT2.Rows[iTrans]["DbHeads2"].ToString();    //GViewDB_Selected.Rows[iTrans].Cells[1].Text;
                                                ddlheads2 = ddlheads2.Replace("&gt;&gt;", ">>");
                                                ddlsubsheadvalue2 = DBDT2.Rows[iTrans]["Dbheadid2"].ToString();

                                                DebDescription2 = DBDT2.Rows[iTrans]["DbDescription2"].ToString();   //GViewDB_Selected.Rows[iTrans].Cells[3].Text;
                                                int rootid2 = int.Parse(ddlsubsheadvalue2.Split(':')[0].ToString().Trim().Trim(':').Trim());
                                                if (rootid2 == 5)
                                                {
                                                    memberid2 = balayer.GetSingleValue("select `MemberID` from membertogroupmaster where `Head_Id`=" + ddlsubsheadvalue2.Split(':')[1].ToString().Trim().Trim(':').Trim());
                                                }
                                                else
                                                {
                                                    memberid2 = "0";
                                                }
                                                if (memberid2 == "")
                                                {
                                                    memberid2 = "0";
                                                }

                                                string db_txtSubAmount2 = DBDT2.Rows[iTrans]["DbAmount2"].ToString();   //GViewDB_Selected.Rows[iTrans].Cells[2].Text;
                                                string db_txtDescription2 = DBDT2.Rows[iTrans]["DbDescription2"].ToString();   //ViewDB_Selected.Rows[iTrans].Cells[1].Text;
                                                NodeID2 = ddlsubsheadvalue2.Split(':')[1].Trim();
                                                chitGroupId2 = "0";
                                                if (int.Parse(ddlsubsheadvalue2.Split(':')[0].ToString().Trim().Trim(':').Trim()) == 5)
                                                {
                                                    chitGroupId2 = balayer.GetSingleValue("select ParentID from headstree where NodeID=" + ddlsubsheadvalue2.Split(':')[1].ToString());
                                                }
                                                if (chitGroupId2 == "")
                                                {
                                                    chitGroupId2 = "0";
                                                }

                                                strDebitHeadQuery2 = "INSERT INTO `svcf`.`voucher` (`DualTransactionKey`,`BranchID`,`CurrDate`,`Voucher_No`,`Voucher_Type`,`Head_Id`,`ChoosenDate`,`Narration`,`Amount`,`Series`,`ReceievedBy`,`Trans_Type`,`T_Day`,`T_Month`,`T_Year`,`MemberID`,`Trans_Medium`,`RootID`,`ChitGroupId`) VALUES (" + DualTransactionKey2 + ",161,'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," + txtVoucherNo2.Text + ",'D'," + ddlsubsheadvalue2.Split(':')[1].ToString().Trim() + ",'" + dtChoosenDate2.ToString("yyyy-MM-dd") + "','" + balayer.ReplaceJnk(DebDescription2) + "'," + db_txtSubAmount2 + ",'" + txtSeries2.Text.ToString().Trim() + "','" + balayer.ReplaceJnk(txtReceivedBy2.Text) + "',0," + dtChoosenDate2.Day + "," + dtChoosenDate2.Month + "," + dtChoosenDate2.Year + "," + memberid2 + "," + trans_medium2 + "," + ddlsubsheadvalue2.Split(':')[0] + "," + chitGroupId2 + ") ;";
                                                iResult2 = trn.insertorupdateTrn(strDebitHeadQuery2);
                                                if (ddlsubsheadvalue2.Contains("3:"))
                                                {
                                                    strBankInsertQuery2 = "insert into transbank(BranchID, TransactionKey, DualTransactionKey, T_Day, T_Month, T_Year, BankHeadID, Head_Id, MemberID, CustomersBankName, DateInCheque, ChequeDDNO, GivenAmount, TotalChequeDDAmount, IsBounced, Trans_Type) values (161," + balayer.ToobjectstrEvenNull((object)iResult2) + "," + DualTransactionKey2 + "," + dtChoosenDate2.Day + "," + dtChoosenDate2.Month + "," + dtChoosenDate2.Year + "," + ddlsubsheadvalue2.Split(':')[1].ToString().Trim() + ",000,000,'','" + balayer.indiandateToMysqlDate(txtDate2.Text) + "'," + "000" + "," + db_txtSubAmount2 + "," + db_txtSubAmount2 + ",0,0);";
                                                    trn.insertorupdateTrn(strBankInsertQuery2);
                                                }
                                            }
                                            #endregion
                                            #endregion
                                            trn.CommitTrn();
                                        }

                                        CRDT2.Dispose();
                                        DBDT2.Dispose();
                                        strDebitHeadQuery2 = "";
                                        strBankInsertQuery2 = "";
                                        ViewState["CurrentTable2"] = null;
                                        ViewState["CurrentTableDB2"] = null;
                                        ddlHeads2.ClearSelection();
                                        ddlHeadsDebit2.ClearSelection();
                                        // Request.Form.Clear();
                                        balayer.ExecuteQuery("Delete From `svcf`.`voucher` Where Series = 'ZZZ' And Voucher_No = '" + txtVoucherNo2.Text + "'");

                                        #endregion

                                        #region Branch3
                                        System.Guid guid3 = Guid.NewGuid();
                                        // Prepare GUID values in SQL format
                                        hexstring3 = BitConverter.ToString(guid3.ToByteArray());
                                        guidForBinary163 = "0x" + hexstring3.Replace("-", string.Empty);
                                        DualTransactionKey3 = guidForBinary163;
                                        GetSeriesAndVoucherNo(3);

                                        isFailed3 = false;
                                        dtChoosenDate3 = DateTime.Parse(txtDate3.Text);
                                        #region VarDeclaration
                                        string[] ChitGpNo3;
                                        string[] ChitGpId3;
                                        int KsrHeadID3 = 0, KsrNoOfMem3 = 0;
                                        string qry3 = "";
                                        MySqlDataReader dr3;
                                        List<string> GetMemList3 = new List<string>();
                                        double kasaramnt3 = 0;
                                        DateTime Ksrchoosendt3;
                                        string ddlSubsHeads3 = "";
                                        string ddlheads3 = "";
                                        string Tot_KsrAmnt3 = "";

                                        string subheadval3 = "";
                                        string ddlSubsHeadsisub3 = "";
                                        string ddlsubsheadvalue3 = "";
                                        string CR_HSelval3 = "";
                                        string chit_gpid3 = "";
                                        string strDebitHeadQuery3 = "";
                                        bool getvc3 = false;
                                        DataTable getcredit3 = new DataTable();
                                        DataTable getdebit3 = new DataTable();
                                        DataTable CRDT3 = new DataTable();
                                        DataTable DBDT3 = new DataTable();
                                        #endregion

                                        DateTime d3;
                                        isDate3 = DateTime.TryParseExact(txtDate3.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out d3);
                                        string trans_medium3 = "";
                                        string memberid3 = "";

                                        CRDT3 = (DataTable)ViewState["CurrentTable3"];
                                        DBDT3 = (DataTable)ViewState["CurrentTableDB3"];

                                        qry3 = "select * from  `svcf`.`voucher` where Voucher_No=" + txtVoucherNo3.Text + " and BranchID=162 and Series='VOUCHER'";
                                        getdebit3 = balayer.GetDataTable(qry3);
                                        if (getdebit3.Rows.Count == 0)
                                        {

                                            #region Voucherinsert

                                            #region GeTRootid

                                            for (int iTrans = 0; iTrans < CRDT3.Rows.Count; iTrans++)
                                            {
                                                ddlSubsHeads3 = CRDT3.Rows[iTrans]["Heads3"].ToString();    //GViewCR_Selected.Rows[iTrans].Cells[1].Text;
                                                subheadval3 = CRDT3.Rows[iTrans]["headid3"].ToString();
                                                ddlSubsHeads3 = ddlSubsHeads3.Replace("&gt;&gt;", ">>");
                                                if (ddlSubsHeads3 == "--Select--")
                                                {
                                                    ddlSubsHeads3 = "";
                                                }
                                                if (subheadval3.Contains("|"))
                                                {
                                                    rootid3 = 5;
                                                    trans_medium3 = "0";
                                                    break;
                                                }
                                                else
                                                {
                                                    //rootid = int.Parse(ddlSubsHeads.SelectedValue.ToString().Split(',')[0].ToString().Trim().Trim(',').Trim());
                                                    rootid3 = int.Parse(subheadval3.Split(':')[0].ToString().Trim().Trim(':').Trim());
                                                    if (rootid3 == 3)
                                                    {
                                                        trans_medium3 = "1";
                                                        break;
                                                    }
                                                    else if (rootid3 == 12)
                                                    {
                                                        trans_medium3 = "0";
                                                        break;
                                                    }
                                                    else if (rootid3 == 5)
                                                    {
                                                        for (int isub = 0; isub < DBDT3.Rows.Count; isub++)
                                                        {
                                                            ddlSubsHeadsisub3 = DBDT3.Rows[isub]["DbHeads3"].ToString(); //GViewDB_Selected.Rows[isub].Cells[1].Text;
                                                            ddlSubsHeadsisub3 = ddlSubsHeadsisub3.Replace("&gt;&gt;", ">>");
                                                            ddlsubsheadvalue3 = DBDT3.Rows[isub]["Dbheadid3"].ToString();    //Dbheadid[isub];
                                                            int subrootid3 = int.Parse(ddlsubsheadvalue3.Split(':')[0].ToString().Trim().Trim(':').Trim());
                                                            if (subrootid3 == 1 || subrootid3 == 6)
                                                            {
                                                                trans_medium3 = "1";
                                                                break;
                                                            }
                                                        }
                                                        break;
                                                    }
                                                    else if (rootid3 == 1)
                                                    {
                                                        for (int isub = 0; isub < DBDT3.Rows.Count; isub++)
                                                        {
                                                            ddlSubsHeadsisub3 = DBDT3.Rows[isub]["DbHeads3"].ToString(); //GViewDB_Selected.Rows[isub].Cells[1].Text;
                                                            ddlSubsHeadsisub3 = ddlSubsHeadsisub3.Replace("&gt;&gt;", ">>");
                                                            ddlsubsheadvalue3 = DBDT3.Rows[isub]["Dbheadid3"].ToString();

                                                            int subrootid3 = int.Parse(ddlsubsheadvalue3.Split(':')[0].ToString().Trim().Trim(':').Trim());
                                                            if (subrootid3 == 12)
                                                            {
                                                                trans_medium3 = "0";
                                                                break;
                                                            }
                                                            else if (subrootid3 == 3)
                                                            {
                                                                trans_medium3 = "1";
                                                                break;
                                                            }
                                                            //nithi
                                                            //if branch credit as well as branch debit
                                                            else if (subrootid3 == 1)
                                                            {
                                                                trans_medium3 = "1";
                                                                break;
                                                            }
                                                            //---------
                                                        }
                                                        break;
                                                    }
                                                }
                                            }

                                            if (trans_medium3 == "")
                                            {

                                                for (int iTrans = 0; iTrans < DBDT3.Rows.Count; iTrans++)
                                                {
                                                    ddlSubsHeadsisub3 = DBDT3.Rows[iTrans]["DbHeads3"].ToString();     // GViewDB_Selected.Rows[iTrans].Cells[1].Text;
                                                    ddlSubsHeadsisub3 = ddlSubsHeadsisub3.Replace("&gt;&gt;", ">>");
                                                    ddlsubsheadvalue3 = DBDT3.Rows[iTrans]["Dbheadid3"].ToString();
                                                    //Label ddlsubsheadvalue = (Label)GViewDB_Selected.Rows[iTrans].FindControl("lbldbhdid");
                                                    //int rootid = int.Parse(ddlSubsdebHeads.SelectedValue.ToString().Split(',')[0].ToString().Trim().Trim(',').Trim());
                                                    int rootid3 = int.Parse(ddlsubsheadvalue3.Split(':')[0].ToString().Trim().Trim(':').Trim());
                                                    if (rootid3 == 3)
                                                    {
                                                        trans_medium3 = "1";
                                                        break;
                                                    }
                                                    else if (rootid3 == 12)
                                                    {
                                                        trans_medium3 = "0";
                                                        break;
                                                    }
                                                }
                                                // }
                                            }
                                            if (trans_medium3 == "")
                                            {
                                                trans_medium3 = "3";
                                            }

                                            #endregion

                                            strCreditHeadQuery3 = "";
                                            strBankInsertQuery3 = "";

                                            #region CreidtInsert


                                            for (int iTrans = 0; iTrans < CRDT3.Rows.Count; iTrans++)
                                            {
                                                ddlheads3 = CRDT3.Rows[iTrans]["Heads3"].ToString();   //GViewCR_Selected.Rows[iTrans].Cells[1].Text;
                                                ddlheads3 = ddlheads3.Replace("&gt;&gt;", ">>");
                                                CredDescription3 = CRDT3.Rows[iTrans]["Description3"].ToString();    //GViewCR_Selected.Rows[iTrans].Cells[3].Text;
                                                CR_HSelval3 = CRDT3.Rows[iTrans]["headid3"].ToString();

                                                //By nithi
                                                //---------------
                                                if (!ddlheads3.Contains("|"))
                                                {
                                                    int rootid3 = int.Parse(CR_HSelval3.Split(':')[0].ToString().Trim().Trim(':').Trim());
                                                    if (rootid3 == 5)
                                                    {
                                                        memberid3 = balayer.GetSingleValue("select `MemberID` from membertogroupmaster where `Head_Id`=" + CR_HSelval3.Split(':')[1].ToString().Trim().Trim(':').Trim());
                                                    }
                                                    else
                                                    {
                                                        memberid3 = "0";
                                                    }
                                                    if (memberid3 == "")
                                                    {
                                                        memberid3 = "0";
                                                    }

                                                    CRtxtsubamnt3 = CRDT3.Rows[iTrans]["Amount3"].ToString();    //GViewCR_Selected.Rows[iTrans].Cells[2].Text;
                                                    NodeID3 = CR_HSelval3.Split(':')[1].Trim();
                                                    string txtChk3 = CRDT3.Rows[iTrans]["chequeNO3"].ToString();    //GViewCR_Selected.Rows[iTrans].Cells[4].Text;
                                                    if (txtChk3 == "&nbsp;")
                                                    {
                                                        txtChk3 = "";
                                                    }
                                                    chitGroupId3 = "0";
                                                    if (int.Parse(CR_HSelval3.Split(':')[0].ToString().Trim().Trim(':').Trim()) == 5)
                                                    {
                                                        chitGroupId3 = balayer.GetSingleValue("select ParentID from headstree where NodeID=" + CR_HSelval3.Split(':')[1].ToString());
                                                    }

                                                    strCreditHeadQuery3 = "INSERT INTO `svcf`.`voucher` (`DualTransactionKey`,`BranchID`,`CurrDate`,`Voucher_No`,`Voucher_Type`,`Head_Id`,`ChoosenDate`,`Narration`,`Amount`,`Series`,`ReceievedBy`,`Trans_Type`,`T_Day`,`T_Month`,`T_Year`,`MemberID`,`Trans_Medium`,`RootID`,`ChitGroupId`) VALUES (" + DualTransactionKey3 + ",162,'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," + txtVoucherNo3.Text + ",'C'," + CR_HSelval3.Split(':')[1].ToString().Trim() + ",'" + dtChoosenDate3.ToString("yyyy-MM-dd") + "','" + balayer.ReplaceJnk(CredDescription3) + "'," + CRtxtsubamnt3 + ",'" + txtSeries3.Text.ToString().Trim() + "','" + balayer.ReplaceJnk(txtReceivedBy3.Text) + "',0," + dtChoosenDate3.Day + "," + dtChoosenDate3.Month + "," + dtChoosenDate3.Year + "," + memberid3 + "," + trans_medium3 + "," + CR_HSelval3.Split(':')[0] + "," + chitGroupId3 + "); ";
                                                    iResult3 = trn.insertorupdateTrn(strCreditHeadQuery3);
                                                    if (txtChk3.Trim() != "")
                                                    {
                                                        //string TransactionKey = balayer.GetSingleValue("SELECT TransactionKey  FROM `svcf`.`voucher` where uuid_from_bin(DualTransactionKey)=uuid_from_bin(" + DualTransactionKey + ") and Head_Id=" + ddlSubsHeads.SelectedValue.Split(',')[1] + " order by TransactionKey desc limit 1;");
                                                        strBankInsertQuery3 = "insert into transbank(BranchID, TransactionKey, DualTransactionKey, T_Day, T_Month, T_Year, BankHeadID, Head_Id, MemberID, CustomersBankName, DateInCheque, ChequeDDNO, GivenAmount, TotalChequeDDAmount, IsBounced, Trans_Type) values (162," + balayer.ToobjectstrEvenNull((object)iResult3) + "," + DualTransactionKey3 + "," + dtChoosenDate3.Day + "," + dtChoosenDate3.Month + "," + dtChoosenDate3.Year + "," + CR_HSelval3.Split(':')[1].ToString().Trim() + ",000,000,'','" + balayer.indiandateToMysqlDate(txtDate3.Text) + "'," + txtChk3 + "," + CRtxtsubamnt3 + "," + CRtxtsubamnt3 + ",0,0);";

                                                        trn.insertorupdateTrn(strBankInsertQuery3);
                                                    }
                                                }
                                                else
                                                {
                                                    ddlheads3 = CRDT3.Rows[iTrans]["Heads3"].ToString();   //GViewCR_Selected.Rows[iTrans].Cells[1].Text;
                                                    ChitGpNo3 = ddlheads3.Split('|');
                                                    if (ChitGpNo3[0] == ChitGpNo3[1])
                                                    {
                                                        try
                                                        {
                                                            //Kasar Entry
                                                            Ksrchoosendt3 = Convert.ToDateTime(txtDate3.Text);
                                                            Tot_KsrAmnt3 = CRDT3.Rows[iTrans]["Amount3"].ToString();    //amntlist[iTrans];    //GViewCR_Selected.Rows[iTrans].Cells[2].Text;
                                                            chit_gpid3 = CRDT3.Rows[iTrans]["headid3"].ToString();
                                                            //Label chit_gpid = (Label)GViewCR_Selected.Rows[iTrans].FindControl("lblhdid");
                                                            ChitGpId3 = chit_gpid3.Split('|');
                                                            qry3 = "SELECT Head_Id,NoofMembers FROM svcf.groupmaster where Head_Id=" + ChitGpId3[0] + "";
                                                            dr3 = balayer.ExecuteReader(qry3);
                                                            qry3 = "";
                                                            while (dr3.Read())
                                                            {
                                                                KsrHeadID3 = Convert.ToInt32(dr3[0]);
                                                                KsrNoOfMem3 = Convert.ToInt32(dr3[1]);
                                                            }
                                                            dr3.Dispose();

                                                            qry3 = "SELECT Head_Id FROM svcf.membertogroupmaster where GroupID=" + KsrHeadID3 + "";
                                                            GetMemList3 = balayer.RetrveList(qry3);
                                                            kasaramnt3 = Convert.ToDouble(Tot_KsrAmnt3) / KsrNoOfMem3;
                                                            //Insert Kasary amount
                                                            for (int li = 0; li <= GetMemList3.Count - 1; li++)
                                                            {
                                                                getvc3 = balayer.CheckVoucher_Exist(Convert.ToInt32(txtVoucherNo3.Text), 162);
                                                                if (getvc3 == false)
                                                                {
                                                                    qry3 = "insert into voucher(DualTransactionKey, BranchID, `CurrDate`, Voucher_No, Voucher_Type, Head_Id, ChoosenDate," +
                                                                    "Narration, Amount, Series, ReceievedBy, Trans_Type, T_Day, T_Month, T_Year, MemberID, Trans_Medium, RootID, ChitGroupId, " +
                                                                    "Other_Trans_Type) values('" + DualTransactionKey3 + "',162,'" + balayer.GetChangeDatFormat(d3.Date, 2) + "'," + Convert.ToInt32(txtVoucherNo3.Text) + "," +
                                                                    "'C'," + Convert.ToInt32(GetMemList3[li]) + ",'" + balayer.GetChangeDatFormat(Ksrchoosendt3, 2) + "','" + ChitGpNo3[0] + ":Redraw Kasar Difference'," +
                                                                    "" + kasaramnt3 + ",'Voucher','admin','1'," + Ksrchoosendt3.Date.Day + "," + Ksrchoosendt3.Date.Month + "," + Ksrchoosendt3.Date.Year + ",0," +
                                                                    "0,5," + KsrHeadID3 + ",5)";
                                                                    balayer.ExecuteQuery(qry3);
                                                                }
                                                            }
                                                        }
                                                        catch (Exception) { }
                                                    }
                                                    // }
                                                }
                                            }
                                            #endregion

                                            strDebitHeadQuery3 = "";
                                            strBankInsertQuery3 = "";

                                            #region Debit insert

                                            for (int iTrans = 0; iTrans < DBDT3.Rows.Count; iTrans++)
                                            {
                                                //for (int iTrans = 0; iTrans < GViewDB_Selected.Rows.Count; iTrans++)
                                                // {
                                                //trans_medium = "0";
                                                // DropDownList ddlSubsDBHeads = (DropDownList)GridGuardiansDebit.Rows[iTrans].FindControl("ddlHeadsDebit");
                                                ddlheads3 = DBDT3.Rows[iTrans]["DbHeads3"].ToString();    //GViewDB_Selected.Rows[iTrans].Cells[1].Text;
                                                ddlheads3 = ddlheads3.Replace("&gt;&gt;", ">>");
                                                ddlsubsheadvalue3 = DBDT3.Rows[iTrans]["Dbheadid3"].ToString();
                                                //Label ddlsubsheadvalue = (Label)GViewDB_Selected.Rows[iTrans].FindControl("lbldbhdid");
                                                DebDescription3 = DBDT3.Rows[iTrans]["DbDescription3"].ToString();   //GViewDB_Selected.Rows[iTrans].Cells[3].Text;
                                                int rootid3 = int.Parse(ddlsubsheadvalue3.Split(':')[0].ToString().Trim().Trim(':').Trim());
                                                if (rootid3 == 5)
                                                {
                                                    memberid3 = balayer.GetSingleValue("select `MemberID` from membertogroupmaster where `Head_Id`=" + ddlsubsheadvalue3.Split(':')[1].ToString().Trim().Trim(':').Trim());
                                                }
                                                else
                                                {
                                                    memberid3 = "0";
                                                }
                                                if (memberid3 == "")
                                                {
                                                    memberid3 = "0";
                                                }
                                                //TextBox txtSubAmount = (TextBox)GridGuardiansDebit.Rows[iTrans].FindControl("txtAmountDebit");
                                                //TextBox txtDescription = (TextBox)GridGuardiansDebit.Rows[iTrans].FindControl("txtDescriptionDebit");
                                                //NodeID = ddlSubsDBHeads.SelectedValue.Split(',')[1].Trim();
                                                string db_txtSubAmount3 = DBDT3.Rows[iTrans]["DbAmount3"].ToString();   //GViewDB_Selected.Rows[iTrans].Cells[2].Text;
                                                string db_txtDescription3 = DBDT3.Rows[iTrans]["DbDescription3"].ToString();   //ViewDB_Selected.Rows[iTrans].Cells[1].Text;
                                                NodeID3 = ddlsubsheadvalue3.Split(':')[1].Trim();
                                                chitGroupId3 = "0";
                                                if (int.Parse(ddlsubsheadvalue3.Split(':')[0].ToString().Trim().Trim(':').Trim()) == 5)
                                                {
                                                    chitGroupId3 = balayer.GetSingleValue("select ParentID from headstree where NodeID=" + ddlsubsheadvalue3.Split(':')[1].ToString());
                                                }
                                                if (chitGroupId3 == "")
                                                {
                                                    chitGroupId3 = "0";
                                                }

                                                strDebitHeadQuery3 = "INSERT INTO `svcf`.`voucher` (`DualTransactionKey`,`BranchID`,`CurrDate`,`Voucher_No`,`Voucher_Type`,`Head_Id`,`ChoosenDate`,`Narration`,`Amount`,`Series`,`ReceievedBy`,`Trans_Type`,`T_Day`,`T_Month`,`T_Year`,`MemberID`,`Trans_Medium`,`RootID`,`ChitGroupId`) VALUES (" + DualTransactionKey3 + ",162,'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," + txtVoucherNo3.Text + ",'D'," + ddlsubsheadvalue3.Split(':')[1].ToString().Trim() + ",'" + dtChoosenDate3.ToString("yyyy-MM-dd") + "','" + balayer.ReplaceJnk(DebDescription3) + "'," + db_txtSubAmount3 + ",'" + txtSeries3.Text.ToString().Trim() + "','" + balayer.ReplaceJnk(txtReceivedBy3.Text) + "',0," + dtChoosenDate3.Day + "," + dtChoosenDate3.Month + "," + dtChoosenDate3.Year + "," + memberid3 + "," + trans_medium3 + "," + ddlsubsheadvalue3.Split(':')[0] + "," + chitGroupId3 + ") ;";
                                                iResult3 = trn.insertorupdateTrn(strDebitHeadQuery3);
                                                if (ddlsubsheadvalue3.Contains("3:"))
                                                {
                                                    strBankInsertQuery3 = "insert into transbank(BranchID, TransactionKey, DualTransactionKey, T_Day, T_Month, T_Year, BankHeadID, Head_Id, MemberID, CustomersBankName, DateInCheque, ChequeDDNO, GivenAmount, TotalChequeDDAmount, IsBounced, Trans_Type) values (162," + balayer.ToobjectstrEvenNull((object)iResult3) + "," + DualTransactionKey3 + "," + dtChoosenDate3.Day + "," + dtChoosenDate3.Month + "," + dtChoosenDate3.Year + "," + ddlsubsheadvalue3.Split(':')[1].ToString().Trim() + ",000,000,'','" + balayer.indiandateToMysqlDate(txtDate3.Text) + "'," + "000" + "," + db_txtSubAmount3 + "," + db_txtSubAmount3 + ",0,0);";
                                                    trn.insertorupdateTrn(strBankInsertQuery3);
                                                }
                                            }
                                            #endregion
                                            #endregion
                                            trn.CommitTrn();
                                        }

                                        CRDT3.Dispose();
                                        DBDT3.Dispose();
                                        strDebitHeadQuery3 = "";
                                        strBankInsertQuery3 = "";
                                        ViewState["CurrentTable3"] = null;
                                        ViewState["CurrentTableDB3"] = null;
                                        ddlHeads3.ClearSelection();
                                        ddlHeadsDebit3.ClearSelection();
                                        // Request.Form.Clear();
                                        balayer.ExecuteQuery("Delete From `svcf`.`voucher` Where Series = 'ZZZ' And Voucher_No = '" + txtVoucherNo3.Text + "'");
                                        #endregion
                                    }

                                    Response.Redirect(Request.Url.AbsoluteUri);
                                }
                                catch (Exception ex)
                                {
                                    try
                                    {
                                        trn.RollbackTrn();
                                        Response.Redirect(Request.Url.AbsoluteUri);
                                    }
                                    catch
                                    {
                                    }
                                    finally
                                    {
                                        btnYes.Focus();
                                        isFailed1 = true;
                                        isFailed2 = true;
                                        isFailed3 = true;
                                        MpAll.PopupControlID = "pnlpopup";
                                        MpAll.Show();
                                        pnlpopup.Visible = true;
                                        lblHint.Text = "Reload";
                                        lblHeading.Text = "Error Status!!!";
                                        lblContent.Text = ex.Message;
                                        lblContent.ForeColor = System.Drawing.Color.Red;
                                        ViewState["CurrentTable1"] = null;
                                        ViewState["CurrentTableDB1"] = null;
                                        ViewState["CurrentTable2"] = null;
                                        ViewState["CurrentTableDB2"] = null;
                                        ViewState["CurrentTable3"] = null;
                                        ViewState["CurrentTableDB3"] = null;

                                        Response.Redirect(Request.Url.AbsoluteUri);

                                    }
                                }
                                finally
                                {
                                    trn.DisposeTrn();
                                    if (!isFailed1)
                                    {
                                        ViewState["CurrentTable1"] = null;
                                        ViewState["CurrentTableDB1"] = null;
                                        SetInitRow_Clnt1();
                                        SetInitDb_Clnt1();
                                    }
                                    if (!isFailed2)
                                    {
                                        ViewState["CurrentTable2"] = null;
                                        ViewState["CurrentTableDB2"] = null;
                                        SetInitRow_Clnt2();
                                        SetInitDb_Clnt2();
                                    }
                                    if (!isFailed3)
                                    {
                                        ViewState["CurrentTable3"] = null;
                                        ViewState["CurrentTableDB3"] = null;
                                        SetInitRow_Clnt3();
                                        SetInitDb_Clnt3();
                                    }
                                }
                                gvoldmember1.DataSource = null;
                                gvoldmember1.DataBind();
                                gvoldmember2.DataSource = null;
                                gvoldmember2.DataBind();
                                gvoldmember3.DataSource = null;
                                gvoldmember3.DataBind();

                                Response.Redirect(Request.Url.AbsoluteUri);
                            }
                            else
                                if (lblHint.Text == "ichk")
                                {
                                    lblHint.Text = "";
                                    if (DynamicControlsHolder.Controls.Count > 0)
                                    {
                                        DynamicControlsHolder.Controls.RemoveAt(0);
                                    }
                                    ViewState["CurrentTable1"] = null;
                                    ViewState["CurrentTableDB1"] = null;
                                    ViewState["CurrentTable2"] = null;
                                    ViewState["CurrentTableDB2"] = null;
                                    ViewState["CurrentTable3"] = null;
                                    ViewState["CurrentTableDB3"] = null;

                                    Response.Redirect(Request.Url.AbsoluteUri);
                                }
                                else
                                {
                                }
            }
            catch (Exception) { }
        }

        protected void btnNo_Click(object sender, EventArgs e)
        {
            if (lblHint.Text == "Reload")
            {
                lblHint.Text = "";
                this.Response.Redirect(this.Request.Url.AbsoluteUri);
            }
            else
                if (lblHint.Text == "VExist")
                {
                    //Added by Gayathri
                    if (txtVoucherNo1.Text != "")
                        balayer.ExecuteQuery("Delete From `svcf`.`voucher` Where Series = 'ZZZ' And Voucher_No = " + txtVoucherNo1.Text + "");
                    txtVoucherNo1.Text = "";
                    txtSeries1.Text = "";
                    txtSeries1.Focus();
                    lblHint.Text = "";

                    if (Session["Branchid"].ToString() == "161")
                    {
                        if (txtVoucherNo2.Text != "")
                            balayer.ExecuteQuery("Delete From `svcf`.`voucher` Where Series = 'ZZZ' And Voucher_No = " + txtVoucherNo2.Text + "");
                        txtVoucherNo2.Text = "";
                        txtSeries2.Text = "";
                        txtSeries2.Focus();
                        
                        if (txtVoucherNo3.Text != "")
                            balayer.ExecuteQuery("Delete From `svcf`.`voucher` Where Series = 'ZZZ' And Voucher_No = " + txtVoucherNo3.Text + "");
                        txtVoucherNo3.Text = "";
                        txtSeries3.Text = "";
                        txtSeries3.Focus();
                    }
                }
                else
                    if (lblHint.Text == "HeadConfirmation")
                    {
                        lblHint.Text = "";
                    }
                    else
                        if (lblHint.Text == "vConfirm")
                        {
                            gvoldmember1.DataSource = null;
                            gvoldmember1.DataBind();
                            gvoldmember2.DataSource = null;
                            gvoldmember2.DataBind();
                            gvoldmember3.DataSource = null;
                            gvoldmember3.DataBind();
                        }
                        else
                            if (lblHint.Text == "ichk")
                            {
                                lblHint.Text = "";
                            }
                            else
                            {
                                ClearControls("cnt1");
                            }
            lblHint.Text = "";
            lblContent.Text = "";
            lblHeading.Text = "";
            this.Response.Redirect(this.Request.Url.AbsoluteUri);
        }
        private void ClearControls(string id)
        {
            Control cont = Page.Master.FindControl("cphMainContent").FindControl(id);
            if (cont != null)
            {
                foreach (Control ctrl in cont.Controls)
                {
                    if (ctrl.GetType().ToString().ToLower().Contains("textbox"))
                    {
                        ((TextBox)ctrl).Text = "";
                    }
                }
            }
        }
        //protected void BtnChequeCancel_Click(object sender, EventArgs e)
        //{
        //    ClearControls("panCheque");
        //}

        protected void btnGenerateTest_Click(object sender, EventArgs e)
        {
            try
            {
                Page.Validate("Generate");
                Page.Validate("GrpRow");
                Page.Validate("GrpRowDebit");
                #region VarDeclaration
                string txtcrChk = "";
                string txtcrsubAmount = "";
                string subheadval = "";
                int rootid;
                #endregion
                if (!Page.IsValid)
                {
                    return;
                }

                string ResExist = balayer.GetSingleValue("select count(*) from voucher where ChoosenDate>='2014/04/01' and Series='" + txtSeries1.Text.ToUpper().Trim() + "' and  Voucher_No=" + txtVoucherNo1.Text.Trim() + " and Trans_Type='0' and BranchID=" + Session["Branchid"]);
                int iExist = int.Parse(ResExist);
                if (iExist != 0)
                {
                    lblHint.Text = "VExist";
                    lblHeading.Text = "Status!!!";
                    lblContent.Text = "Voucher Already Exsist!!! ";
                    MpAll.PopupControlID = "pnlpopup";
                    btnYes.Focus();
                    MpAll.Show();
                    pnlpopup.Visible = true;
                    return;
                }
                for (int iTrans = 0; iTrans <= GridClnt1.Rows.Count - 1; iTrans++)
                {
                    //TextBox txtChk = (TextBox)GViewCR_Selected.Rows[iTrans].FindControl("txtChequeNO");
                    Label Crhd1 = (Label)GridClnt1.Rows[iTrans].FindControl("Cr1_lblhd1");
                    Label CrAmnt1 = (Label)GridClnt1.Rows[iTrans].FindControl("Cr1_lblamnt1");
                    Label Crdesc1 = (Label)GridClnt1.Rows[iTrans].FindControl("Cr1_desc1");
                    Label Crcheq1 = (Label)GridClnt1.Rows[iTrans].FindControl("Cr1_cheq1");
                    Label Crhdid1 = (Label)GridClnt1.Rows[iTrans].FindControl("Cr1_hdid1");

                    txtcrChk = Crcheq1.Text;
                    // Label bankhead = (Label)GViewCR_Selected.Rows[iTrans].FindControl("lblhdid");
                    subheadval = Crhdid1.Text;
                    //if (txtChk.Visible)
                    //{
                    if (txtcrChk == "&nbsp;") txtcrChk = "";
                    if (subheadval.Contains(':'))
                    {
                        rootid = 1;
                    }
                    else
                    {
                        rootid = int.Parse(subheadval.Split(',')[0].ToString().Trim().Trim(',').Trim());
                    }
                    if (rootid == 3)
                    {
                        if (txtcrChk.Trim() == "")
                        {
                            lblHeading.Text = "Error!!!";
                            lblContent.Text = "Please fill ChequeNo!!!" + "</br></br>&nbsp;&nbsp;";
                            MpAll.PopupControlID = "pnlpopup";
                            gvoldmember1.Columns.Clear();
                            gvoldmember1.DataSource = null;
                            gvoldmember1.DataBind();
                            MpAll.PopupControlID = "pnlpopup";
                            MpAll.Show();
                            btnYes.Focus();
                            pnlpopup.Visible = true;
                            return;
                        }
                    }
                    // }
                }
                DataTable dtConfirmation = new DataTable();
                dtConfirmation.Columns.Add("Heads");
                dtConfirmation.Columns.Add("Credit");
                dtConfirmation.Columns.Add("Debit");
                decimal totalright = 0.0M;
                decimal totalleft = 0.0M;
                string error = "";
                string errorDebit = "";
                int TotalNoofRows = GridClnt1.Rows.Count + GrdDbClnt1.Rows.Count;
                for (int iRC = 0; iRC < TotalNoofRows; iRC++)
                {
                    dtConfirmation.Rows.Add();
                }
                for (int iRow = 0; iRow <= GridClnt1.Rows.Count - 1; iRow++)
                {
                    //TextBox txtsubAmount = (TextBox)GridGuardians.Rows[iRow].FindControl("txtAmount");
                    Label txtsubAmount = (Label)GridClnt1.Rows[iRow].FindControl("Cr1_lblamnt1");
                    dtConfirmation.Rows[iRow][1] = txtsubAmount.Text;
                    Label Crhds1 = (Label)GridClnt1.Rows[iRow].FindControl("Cr1_lblhd1");
                    dtConfirmation.Rows[iRow][0] = Crhds1.Text; //((DropDownList)GridGuardians.Rows[iRow].FindControl("ddlHeads")).SelectedItem.Text;
                    error += txtcrsubAmount + " + ";
                    totalleft += decimal.Parse(txtcrsubAmount.Trim());
                }
                for (int iRow = 0; iRow <= GrdDbClnt1.Rows.Count - 1; iRow++)
                {
                    //TextBox txtsubAmount = (TextBox)GridGuardiansDebit.Rows[iRow].FindControl("txtAmountDebit");
                    Label txtDbsubAmount = (Label)GridClnt1.Rows[iRow].FindControl("Db1_amnt1");
                    string txtsubAmount = txtDbsubAmount.Text;
                    dtConfirmation.Rows[GridClnt1.Rows.Count + iRow][2] = txtsubAmount;
                    dtConfirmation.Rows[GridClnt1.Rows.Count + iRow][0] = GrdDbClnt1.Rows[iRow].Cells[1].Text;
                    errorDebit += txtsubAmount + " + ";
                    totalright += decimal.Parse(txtsubAmount.Trim());
                }
                if (totalleft == totalright)
                {
                    lblHint.Text = "VConfirm";
                    lblHeading.Text = "Confirmation";
                    lblContent.Text = " Please Confirm Your Voucher Details???";
                    gvoldmember1.DataSource = dtConfirmation;
                    gvoldmember1.DataBind();
                    MpAll.PopupControlID = "pnlpopup";
                    pnlpopup.Visible = true;
                    btnYes.Focus();
                    MpAll.Show();
                }
                else
                {
                    error = error.Trim().Trim('+');
                    lblHeading.Text = "Warning!!!";
                    lblContent.Text = "Please Tally Credit and Debit Amount!!!" + "</br></br>" + error.Trim().Trim('+') + " !=  " + errorDebit.Trim().Trim('+');
                    MpAll.PopupControlID = "pnlpopup";
                    gvoldmember1.DataSource = dtConfirmation;
                    gvoldmember1.DataBind();
                    MpAll.PopupControlID = "pnlpopup";
                    MpAll.Show();
                    btnYes.Focus();
                    pnlpopup.Visible = true;
                    dtConfirmation.Dispose();
                }
            }
            catch (Exception) { }
        }


        protected void btnGenerate_Click(object sender, EventArgs e)
        {
            try
            {
                lblcancelmsg1.Text = "";
                lblcancelmsg2.Text = "";
                lblcancelmsg3.Text = "";
                
                //Page.Validate("Generate");
                //Page.Validate("GrpRow");
                //Page.Validate("GrpRowDebit");

                #region VarDeclaration
                string txtcrChk1 = "";
                string txtcrsubAmount1 = "";
                string subheadval1 = "";
                string bnkhead1 = "";
                string[] hdslist1 = new string[0];
                string[] amntlist1 = new string[0];
                string[] memlist1 = new string[0];
                string[] cheqlist1 = new string[0];
                string[] hdidlist1 = new string[0];
                string[] DbHeads1 = new string[0];
                string[] DbAmount1 = new string[0];
                string[] DbDescription1 = new string[0];
                string[] Dbheadid1 = new string[0];
                string strBranchID1 = Session["Branchid"].ToString();
                if (strBranchID1 == "161")
                    strBranchID1 = "160";

                string txtcrChk2 = "";
                string txtcrsubAmount2 = "";
                string subheadval2 = "";
                string bnkhead2 = "";
                string[] hdslist2 = new string[0];
                string[] amntlist2 = new string[0];
                string[] memlist2 = new string[0];
                string[] cheqlist2 = new string[0];
                string[] hdidlist2 = new string[0];
                string[] DbHeads2 = new string[0];
                string[] DbAmount2 = new string[0];
                string[] DbDescription2 = new string[0];
                string[] Dbheadid2 = new string[0];

                string txtcrChk3 = "";
                string txtcrsubAmount3 = "";
                string subheadval3 = "";
                string bnkhead3 = "";
                string[] hdslist3 = new string[0];
                string[] amntlist3 = new string[0];
                string[] memlist3 = new string[0];
                string[] cheqlist3 = new string[0];
                string[] hdidlist3 = new string[0];
                string[] DbHeads3 = new string[0];
                string[] DbAmount3 = new string[0];
                string[] DbDescription3 = new string[0];
                string[] Dbheadid3 = new string[0];
                #endregion

                //if (!Page.IsValid)
                //{
                //    return;
                //}
                //if (Session["CheckRefresh"].ToString() != ViewState["CheckRefresh"].ToString())   19/07/2016
                //{
                //    return;
                //}
                //commented by gayathri GetSeriesAndVoucherNo();
                string ResExist = balayer.GetSingleValue("select count(*) from voucher where ChoosenDate>='2014/04/01' and Series='" + txtSeries1.Text.ToUpper().Trim() + "' and  Voucher_No=" + txtVoucherNo1.Text.Trim() + " and Trans_Type='0' and BranchID=" + strBranchID1);
                int iExist = int.Parse(ResExist);
                if (iExist != 0)
                {
                    lblHint.Text = "VExist - I";
                    lblHeading.Text = "Status!!!";
                    lblContent.Text = "Voucher Already Exsist!!! ";
                    MpAll.PopupControlID = "pnlpopup";
                    btnYes.Focus();
                    MpAll.Show();
                    pnlpopup.Visible = true;
                    return;
                }

                string ResExist2 = balayer.GetSingleValue("select count(*) from voucher where ChoosenDate>='2014/04/01' and Series='" + txtSeries2.Text.ToUpper().Trim() + "' and  Voucher_No=" + txtVoucherNo2.Text.Trim() + " and Trans_Type='0' and BranchID=161");
                int iExist2 = int.Parse(ResExist2);
                if (iExist2 != 0)
                {
                    lblHint.Text = "VExist - II";
                    lblHeading.Text = "Status!!!";
                    lblContent.Text = "Voucher Already Exsist!!! ";
                    MpAll.PopupControlID = "pnlpopup";
                    btnYes.Focus();
                    MpAll.Show();
                    pnlpopup.Visible = true;
                    return;
                }

                string ResExist3 = balayer.GetSingleValue("select count(*) from voucher where ChoosenDate>='2014/04/01' and Series='" + txtSeries3.Text.ToUpper().Trim() + "' and  Voucher_No=" + txtVoucherNo3.Text.Trim() + " and Trans_Type='0' and BranchID=162");
                int iExist3 = int.Parse(ResExist3);
                if (iExist3 != 0)
                {
                    lblHint.Text = "VExist - III";
                    lblHeading.Text = "Status!!!";
                    lblContent.Text = "Voucher Already Exsist!!!";
                    MpAll.PopupControlID = "pnlpopup";
                    btnYes.Focus();
                    MpAll.Show();
                    pnlpopup.Visible = true;
                    return;
                }

                #region Branch1
                for (int iRow1 = 0; iRow1 < GridClnt1.Rows.Count; iRow1++)
                {
                    Label lblHead = (Label)GridClnt1.Rows[iRow1].FindControl("Cr1_lblhd1");
                    Label lblAmount = (Label)GridClnt1.Rows[iRow1].FindControl("Cr1_lblamnt1");
                    Label lblDesc = (Label)GridClnt1.Rows[iRow1].FindControl("Cr1_desc1");
                    Label lblChequeNo = (Label)GridClnt1.Rows[iRow1].FindControl("Cr1_cheq1");
                    Label lblHeadID = (Label)GridClnt1.Rows[iRow1].FindControl("Cr1_hdid1");

                    if (lblHead.Text != "")
                    {
                        txtcrChk1 = lblChequeNo.Text;
                        bnkhead1 = lblHeadID.Text;
                        subheadval1 = bnkhead1;

                        if (txtcrChk1 == "&nbsp;") txtcrChk1 = "";
                        if (subheadval1.Contains('|'))
                        {
                            rootid1 = 1;
                        }
                        else
                        {
                            rootid1 = int.Parse(subheadval1.Split(':')[0].ToString().Trim().Trim(':').Trim());
                        }
                        if (rootid1 == 3)
                        {
                            if (txtcrChk1.Trim() == "")
                            {
                                lblHeading.Text = "Error!!!";
                                lblContent.Text = "Please fill ChequeNo!!!" + "</br></br>&nbsp;&nbsp;";
                                MpAll.PopupControlID = "pnlpopup";
                                gvoldmember1.Columns.Clear();
                                gvoldmember1.DataSource = null;
                                gvoldmember1.DataBind();
                                MpAll.PopupControlID = "pnlpopup";
                                MpAll.Show();
                                btnYes.Focus();
                                pnlpopup.Visible = true;
                                return;
                            }
                        }
                    }
                }

                DataTable dtConfirmation1 = new DataTable();
                dtConfirmation1.Columns.Add("Heads1");
                dtConfirmation1.Columns.Add("Credit1");
                dtConfirmation1.Columns.Add("Debit1");

                decimal totalright1 = 0.0M;
                decimal totalleft1 = 0.0M;
                string error1 = "";
                string errorDebit1 = "";
                int dtconfirmrow1 = 0;

                for (int iRow1 = 0; iRow1 < GridClnt1.Rows.Count; iRow1++)
                {
                    Label lblHead = (Label)GridClnt1.Rows[iRow1].FindControl("Cr1_lblhd1");
                    Label lblAmount = (Label)GridClnt1.Rows[iRow1].FindControl("Cr1_lblamnt1");

                    var drTemp = dtConfirmation1.NewRow();
                    txtcrsubAmount1 = lblAmount.Text;
                    drTemp[1] = txtcrsubAmount1;
                    drTemp[0] = lblHead.Text; 
                    dtConfirmation1.Rows.Add(drTemp);
                    error1 += txtcrsubAmount1 + " + ";
                    totalleft1 += decimal.Parse(txtcrsubAmount1.Trim());
                    dtconfirmrow1++;
                }

                for (int iRow1 = 0; iRow1 < GrdDbClnt1.Rows.Count; iRow1++)
                {
                    Label lblHead = (Label)GrdDbClnt1.Rows[iRow1].FindControl("Db1_hd1");
                    Label lblAmount = (Label)GrdDbClnt1.Rows[iRow1].FindControl("Db1_amnt1");
                    Label lblDesc = (Label)GrdDbClnt1.Rows[iRow1].FindControl("Db1_desc1");
                    Label lblHeadID = (Label)GrdDbClnt1.Rows[iRow1].FindControl("Db1_hdid1");
                    
                    string txtsubAmount1 = lblAmount.Text;

                    var drTemp = dtConfirmation1.NewRow();
                    drTemp[2] = txtsubAmount1;
                    drTemp[0] = lblHead.Text;
                    dtConfirmation1.Rows.Add(drTemp);

                    errorDebit1 += txtsubAmount1 + " + ";
                    totalright1 += decimal.Parse(txtsubAmount1.Trim());
                }

                #region CreditTable
                if (ViewState["CurrentTable1"] != null)
                {
                    DataTable CRTable1 = new DataTable();
                    CRTable1 = (DataTable)ViewState["CurrentTable1"];
                    CRTable1.Rows.Clear();
                    //if (CRTable1.Rows.Count > 0)
                    //{
                    //    //Remove initial blank row
                    //    if (CRTable1.Rows[0][0].ToString() == "")
                    //    {
                    //        CRTable1.Rows[0].Delete();
                    //        CRTable1.AcceptChanges();
                    //    }
                    //}
                    for (int iRow1 = 0; iRow1 < GridClnt1.Rows.Count; iRow1++)
                    {
                        Label lblHead = (Label)GridClnt1.Rows[iRow1].FindControl("Cr1_lblhd1");
                        Label lblAmount = (Label)GridClnt1.Rows[iRow1].FindControl("Cr1_lblamnt1");
                        Label lblDesc = (Label)GridClnt1.Rows[iRow1].FindControl("Cr1_desc1");
                        Label lblChequeNo = (Label)GridClnt1.Rows[iRow1].FindControl("Cr1_cheq1");
                        Label lblHeadID = (Label)GridClnt1.Rows[iRow1].FindControl("Cr1_hdid1");

                        DataRow drCurrentRow1 = CRTable1.NewRow();

                        drCurrentRow1["Heads1"] = lblHead.Text;
                        drCurrentRow1["Amount1"] = lblAmount.Text;
                        drCurrentRow1["Description1"] = lblDesc.Text;
                        drCurrentRow1["chequeNO1"] = lblChequeNo.Text;
                        drCurrentRow1["headid1"] = lblHeadID.Text;
                        
                        CRTable1.Rows.Add(drCurrentRow1);
                    }

                    ViewState["CurrentTable1"] = CRTable1;
                }
                #endregion

                #region DebitTable
                if (ViewState["CurrentTableDB1"] != null)
                {
                    DataTable DRTable1 = new DataTable();
                    DRTable1 = (DataTable)ViewState["CurrentTableDB1"];
                    DRTable1.Rows.Clear();
                    //if (DRTable1.Rows.Count > 0)
                    //{
                    //    if (DRTable1.Rows[0][0].ToString() == "")
                    //    {
                    //        DRTable1.Rows[0].Delete();
                    //        DRTable1.AcceptChanges();
                    //    }
                    //}

                    for (int iRow1 = 0; iRow1 < GrdDbClnt1.Rows.Count; iRow1++)
                    {
                        Label lblHead = (Label)GrdDbClnt1.Rows[iRow1].FindControl("Db1_hd1");
                        Label lblAmount = (Label)GrdDbClnt1.Rows[iRow1].FindControl("Db1_amnt1");
                        Label lblDesc = (Label)GrdDbClnt1.Rows[iRow1].FindControl("Db1_desc1");
                        Label lblHeadID = (Label)GrdDbClnt1.Rows[iRow1].FindControl("Db1_hdid1");

                        DataRow drCurrentRow1 = DRTable1.NewRow();

                        drCurrentRow1["DbHeads1"] = lblHead.Text;
                        drCurrentRow1["DbAmount1"] = lblAmount.Text;
                        drCurrentRow1["DbDescription1"] = lblDesc.Text;
                        drCurrentRow1["Dbheadid1"] = lblHeadID.Text;
                        
                        DRTable1.Rows.Add(drCurrentRow1);
                    }

                    ViewState["CurrentTableDB1"] = DRTable1;
                }
                #endregion

                if (totalleft1 == totalright1)
                {
                    lblHint.Text = "VConfirm";
                    lblHeading.Text = "Confirmation";
                    lblContent.Text = " Please Confirm Your Voucher Details...";
                    gvoldmember1.DataSource = dtConfirmation1;
                    gvoldmember1.DataBind();
                    MpAll.PopupControlID = "pnlpopup";
                    pnlpopup.Visible = true;
                    btnYes.Focus();
                    MpAll.Show();
                }
                else
                {
                    error1 = error1.Trim().Trim('+');
                    lblHeading.Text = "Warning!!!";
                    lblContent.Text = "Please Tally Credit and Debit Amount!!!" + "</br></br>" + error1.Trim().Trim('+') + " !=  " + errorDebit1.Trim().Trim('+');
                    MpAll.PopupControlID = "pnlpopup";
                    gvoldmember1.DataSource = dtConfirmation1;
                    gvoldmember1.DataBind();
                    MpAll.PopupControlID = "pnlpopup";
                    MpAll.Show();
                    btnYes.Focus();
                    pnlpopup.Visible = true;
                    dtConfirmation1.Dispose();
                }
                #endregion

                #region Branch2
                for (int iRow2 = 0; iRow2 < GridClnt2.Rows.Count; iRow2++)
                {
                    Label lblHead = (Label)GridClnt2.Rows[iRow2].FindControl("Cr2_hds2");
                    Label lblAmount = (Label)GridClnt2.Rows[iRow2].FindControl("Cr2_amnt2");
                    Label lblDesc = (Label)GridClnt2.Rows[iRow2].FindControl("Cr2_desc2");
                    Label lblChequeNo = (Label)GridClnt2.Rows[iRow2].FindControl("Cr2_chq2");
                    Label lblHeadID = (Label)GridClnt2.Rows[iRow2].FindControl("Cr2_hdid2");

                    if (lblHead.Text != "")
                    {
                        txtcrChk2 = lblChequeNo.Text;
                        bnkhead2 = lblHeadID.Text;
                        subheadval2 = bnkhead2;

                        if (txtcrChk2 == "&nbsp;") txtcrChk2 = "";
                        if (subheadval2.Contains('|'))
                        {
                            rootid2 = 1;
                        }
                        else
                        {
                            rootid2 = int.Parse(subheadval2.Split(':')[0].ToString().Trim().Trim(':').Trim());
                        }
                        if (rootid2 == 3)
                        {
                            if (txtcrChk2.Trim() == "")
                            {
                                lblHeading.Text = "Error!!!";
                                lblContent.Text = "Please fill ChequeNo!!!" + "</br></br>&nbsp;&nbsp;";
                                MpAll.PopupControlID = "pnlpopup";
                                gvoldmember2.Columns.Clear();
                                gvoldmember2.DataSource = null;
                                gvoldmember2.DataBind();
                                MpAll.PopupControlID = "pnlpopup";
                                MpAll.Show();
                                btnYes.Focus();
                                pnlpopup.Visible = true;
                                return;
                            }
                        }
                    }
                }

                DataTable dtConfirmation2 = new DataTable();
                dtConfirmation2.Columns.Add("Heads2");
                dtConfirmation2.Columns.Add("Credit2");
                dtConfirmation2.Columns.Add("Debit2");

                decimal totalright2 = 0.0M;
                decimal totalleft2 = 0.0M;
                string error2 = "";
                string errorDebit2 = "";
                int dtconfirmrow2 = 0;

                for (int iRow2 = 0; iRow2 < GridClnt2.Rows.Count; iRow2++)
                {
                    Label lblHead = (Label)GridClnt2.Rows[iRow2].FindControl("Cr2_hds2");
                    Label lblAmount = (Label)GridClnt2.Rows[iRow2].FindControl("Cr2_amnt2");

                    var drTemp = dtConfirmation2.NewRow();
                    txtcrsubAmount2 = lblAmount.Text;
                    drTemp[1] = txtcrsubAmount2;
                    drTemp[0] = lblHead.Text;
                    dtConfirmation2.Rows.Add(drTemp);
                    error2 += txtcrsubAmount2 + " + ";
                    totalleft2 += decimal.Parse(txtcrsubAmount2.Trim());
                    dtconfirmrow2++;
                }

                for (int iRow2 = 0; iRow2 < GrdDbClnt2.Rows.Count; iRow2++)
                {
                    Label lblHead = (Label)GrdDbClnt2.Rows[iRow2].FindControl("Db2_hds2");
                    Label lblAmount = (Label)GrdDbClnt2.Rows[iRow2].FindControl("Db2_amnt2");
                    Label lblDesc = (Label)GrdDbClnt2.Rows[iRow2].FindControl("Db2_desc2");
                    Label lblHeadID = (Label)GrdDbClnt2.Rows[iRow2].FindControl("Db2_hdid2");

                    string txtsubAmount2 = lblAmount.Text;

                    var drTemp = dtConfirmation2.NewRow();
                    drTemp[2] = txtsubAmount2;
                    drTemp[0] = lblHead.Text;
                    dtConfirmation2.Rows.Add(drTemp);

                    errorDebit2 += txtsubAmount2 + " + ";
                    totalright2 += decimal.Parse(txtsubAmount2.Trim());
                }

                #region CreditTable
                if (ViewState["CurrentTable2"] != null)
                {
                    DataTable CRTable2 = new DataTable();
                    CRTable2 = (DataTable)ViewState["CurrentTable2"];
                    CRTable2.Rows.Clear();
                    //if (CRTable2.Rows.Count > 0)
                    //{
                    //    //Remove initial blank row
                    //    if (CRTable2.Rows[0][0].ToString() == "")
                    //    {
                    //        CRTable2.Rows[0].Delete();
                    //        CRTable2.AcceptChanges();
                    //    }
                    //}
                    for (int iRow2 = 0; iRow2 < GridClnt2.Rows.Count; iRow2++)
                    {
                        Label lblHead = (Label)GridClnt2.Rows[iRow2].FindControl("Cr2_hds2");
                        Label lblAmount = (Label)GridClnt2.Rows[iRow2].FindControl("Cr2_amnt2");
                        Label lblDesc = (Label)GridClnt2.Rows[iRow2].FindControl("Cr2_desc2");
                        Label lblChequeNo = (Label)GridClnt2.Rows[iRow2].FindControl("Cr2_chq2");
                        Label lblHeadID = (Label)GridClnt2.Rows[iRow2].FindControl("Cr2_hdid2");

                        DataRow drCurrentRow2 = CRTable2.NewRow();

                        drCurrentRow2["Heads2"] = lblHead.Text;
                        drCurrentRow2["Amount2"] = lblAmount.Text;
                        drCurrentRow2["Description2"] = lblDesc.Text;
                        drCurrentRow2["chequeNO2"] = lblChequeNo.Text;
                        drCurrentRow2["headid2"] = lblHeadID.Text;

                        CRTable2.Rows.Add(drCurrentRow2);
                    }

                    ViewState["CurrentTable2"] = CRTable2;
                }
                #endregion

                #region DebitTable
                if (ViewState["CurrentTableDB2"] != null)
                {
                    DataTable DRTable2 = new DataTable();
                    DRTable2 = (DataTable)ViewState["CurrentTableDB2"];
                    DRTable2.Rows.Clear();
                    //if (DRTable2.Rows.Count > 0)
                    //{
                    //    if (DRTable2.Rows[0][0].ToString() == "")
                    //    {
                    //        DRTable2.Rows[0].Delete();
                    //        DRTable2.AcceptChanges();
                    //    }
                    //}

                    for (int iRow2 = 0; iRow2 < GrdDbClnt2.Rows.Count; iRow2++)
                    {
                        Label lblHead = (Label)GrdDbClnt2.Rows[iRow2].FindControl("Db2_hds2");
                        Label lblAmount = (Label)GrdDbClnt2.Rows[iRow2].FindControl("Db2_amnt2");
                        Label lblDesc = (Label)GrdDbClnt2.Rows[iRow2].FindControl("Db2_desc2");
                        Label lblHeadID = (Label)GrdDbClnt2.Rows[iRow2].FindControl("Db2_hdid2");

                        DataRow drCurrentRow2 = DRTable2.NewRow();

                        drCurrentRow2["DbHeads2"] = lblHead.Text;
                        drCurrentRow2["DbAmount2"] = lblAmount.Text;
                        drCurrentRow2["DbDescription2"] = lblDesc.Text;
                        drCurrentRow2["Dbheadid2"] = lblHeadID.Text;

                        DRTable2.Rows.Add(drCurrentRow2);
                    }

                    ViewState["CurrentTableDB2"] = DRTable2;
                }
                #endregion

                if (totalleft2 == totalright2)
                {
                    lblHint.Text = "VConfirm";
                    lblHeading.Text = "Confirmation";
                    lblContent.Text = " Please Confirm Your Voucher Details...";
                    gvoldmember2.DataSource = dtConfirmation2;
                    gvoldmember2.DataBind();
                    MpAll.PopupControlID = "pnlpopup";
                    pnlpopup.Visible = true;
                    btnYes.Focus();
                    MpAll.Show();
                }
                else
                {
                    error2 = error2.Trim().Trim('+');
                    lblHeading.Text = "Warning!!!";
                    lblContent.Text = "Please Tally Credit and Debit Amount!!!" + "</br></br>" + error2.Trim().Trim('+') + " !=  " + errorDebit2.Trim().Trim('+');
                    MpAll.PopupControlID = "pnlpopup";
                    gvoldmember2.DataSource = dtConfirmation2;
                    gvoldmember2.DataBind();
                    MpAll.PopupControlID = "pnlpopup";
                    MpAll.Show();
                    btnYes.Focus();
                    pnlpopup.Visible = true;
                    dtConfirmation2.Dispose();
                }
                #endregion

                #region Branch3
                for (int iRow3 = 0; iRow3 < GridClnt3.Rows.Count; iRow3++)
                {
                    Label lblHead = (Label)GridClnt3.Rows[iRow3].FindControl("Cr3_hds3");
                    Label lblAmount = (Label)GridClnt3.Rows[iRow3].FindControl("Cr3_amnt3");
                    Label lblDesc = (Label)GridClnt3.Rows[iRow3].FindControl("Cr3_desc3");
                    Label lblChequeNo = (Label)GridClnt3.Rows[iRow3].FindControl("Cr3_chq3");
                    Label lblHeadID = (Label)GridClnt3.Rows[iRow3].FindControl("Cr3_hdid3");

                    if (lblHead.Text != "")
                    {
                        txtcrChk3 = lblChequeNo.Text;
                        bnkhead3 = lblHeadID.Text;
                        subheadval3 = bnkhead3;

                        if (txtcrChk3 == "&nbsp;") txtcrChk3 = "";
                        if (subheadval3.Contains('|'))
                        {
                            rootid3 = 1;
                        }
                        else
                        {
                            rootid3 = int.Parse(subheadval3.Split(':')[0].ToString().Trim().Trim(':').Trim());
                        }
                        if (rootid3 == 3)
                        {
                            if (txtcrChk3.Trim() == "")
                            {
                                lblHeading.Text = "Error!!!";
                                lblContent.Text = "Please fill ChequeNo!!!" + "</br></br>&nbsp;&nbsp;";
                                MpAll.PopupControlID = "pnlpopup";
                                gvoldmember3.Columns.Clear();
                                gvoldmember3.DataSource = null;
                                gvoldmember3.DataBind();
                                MpAll.PopupControlID = "pnlpopup";
                                MpAll.Show();
                                btnYes.Focus();
                                pnlpopup.Visible = true;
                                return;
                            }
                        }
                    }
                }

                DataTable dtConfirmation3 = new DataTable();
                dtConfirmation3.Columns.Add("Heads3");
                dtConfirmation3.Columns.Add("Credit3");
                dtConfirmation3.Columns.Add("Debit3");

                decimal totalright3 = 0.0M;
                decimal totalleft3 = 0.0M;
                string error3 = "";
                string errorDebit3 = "";
                int dtconfirmrow3 = 0;

                for (int iRow3 = 0; iRow3 < GridClnt3.Rows.Count; iRow3++)
                {
                    Label lblHead = (Label)GridClnt3.Rows[iRow3].FindControl("Cr3_hds3");
                    Label lblAmount = (Label)GridClnt3.Rows[iRow3].FindControl("Cr3_amnt3");

                    var drTemp = dtConfirmation3.NewRow();
                    txtcrsubAmount3 = lblAmount.Text;
                    drTemp[1] = txtcrsubAmount3;
                    drTemp[0] = lblHead.Text;
                    dtConfirmation3.Rows.Add(drTemp);
                    error3 += txtcrsubAmount3 + " + ";
                    totalleft3 += decimal.Parse(txtcrsubAmount3.Trim());
                    dtconfirmrow3++;
                }

                for (int iRow3 = 0; iRow3 < GrdDbClnt3.Rows.Count; iRow3++)
                {
                    Label lblHead = (Label)GrdDbClnt3.Rows[iRow3].FindControl("Db3_hds3");
                    Label lblAmount = (Label)GrdDbClnt3.Rows[iRow3].FindControl("Db3_amnt3");
                    Label lblDesc = (Label)GrdDbClnt3.Rows[iRow3].FindControl("Db3_desc3");
                    Label lblHeadID = (Label)GrdDbClnt3.Rows[iRow3].FindControl("Db3_hdid3");

                    string txtsubAmount3 = lblAmount.Text;

                    var drTemp = dtConfirmation3.NewRow();
                    drTemp[2] = txtsubAmount3;
                    drTemp[0] = lblHead.Text;
                    dtConfirmation3.Rows.Add(drTemp);

                    errorDebit3 += txtsubAmount3 + " + ";
                    totalright3 += decimal.Parse(txtsubAmount3.Trim());
                }

                #region CreditTable
                if (ViewState["CurrentTable3"] != null)
                {
                    DataTable CRTable3 = new DataTable();
                    CRTable3 = (DataTable)ViewState["CurrentTable3"];
                    CRTable3.Rows.Clear();
                    //if (CRTable3.Rows.Count > 0)
                    //{
                    //    //Remove initial blank row
                    //    if (CRTable3.Rows[0][0].ToString() == "")
                    //    {
                    //        CRTable3.Rows[0].Delete();
                    //        CRTable3.AcceptChanges();
                    //    }
                    //}
                    for (int iRow3 = 0; iRow3 < GridClnt3.Rows.Count; iRow3++)
                    {
                        Label lblHead = (Label)GridClnt3.Rows[iRow3].FindControl("Cr3_hds3");
                        Label lblAmount = (Label)GridClnt3.Rows[iRow3].FindControl("Cr3_amnt3");
                        Label lblDesc = (Label)GridClnt3.Rows[iRow3].FindControl("Cr3_desc3");
                        Label lblChequeNo = (Label)GridClnt3.Rows[iRow3].FindControl("Cr3_chq3");
                        Label lblHeadID = (Label)GridClnt3.Rows[iRow3].FindControl("Cr3_hdid3");

                        DataRow drCurrentRow3 = CRTable3.NewRow();

                        drCurrentRow3["Heads3"] = lblHead.Text;
                        drCurrentRow3["Amount3"] = lblAmount.Text;
                        drCurrentRow3["Description3"] = lblDesc.Text;
                        drCurrentRow3["chequeNO3"] = lblChequeNo.Text;
                        drCurrentRow3["headid3"] = lblHeadID.Text;

                        CRTable3.Rows.Add(drCurrentRow3);
                    }

                    ViewState["CurrentTable3"] = CRTable3;
                }
                #endregion

                #region DebitTable
                if (ViewState["CurrentTableDB3"] != null)
                {
                    DataTable DRTable3 = new DataTable();
                    DRTable3 = (DataTable)ViewState["CurrentTableDB3"];
                    DRTable3.Rows.Clear();
                    //if (DRTable3.Rows.Count > 0)
                    //{
                    //    if (DRTable3.Rows[0][0].ToString() == "")
                    //    {
                    //        DRTable3.Rows[0].Delete();
                    //        DRTable3.AcceptChanges();
                    //    }
                    //}

                    for (int iRow3 = 0; iRow3 < GrdDbClnt3.Rows.Count; iRow3++)
                    {
                        Label lblHead = (Label)GrdDbClnt3.Rows[iRow3].FindControl("Db3_hds3");
                        Label lblAmount = (Label)GrdDbClnt3.Rows[iRow3].FindControl("Db3_amnt3");
                        Label lblDesc = (Label)GrdDbClnt3.Rows[iRow3].FindControl("Db3_desc3");
                        Label lblHeadID = (Label)GrdDbClnt3.Rows[iRow3].FindControl("Db3_hdid3");

                        DataRow drCurrentRow3 = DRTable3.NewRow();

                        drCurrentRow3["DbHeads3"] = lblHead.Text;
                        drCurrentRow3["DbAmount3"] = lblAmount.Text;
                        drCurrentRow3["DbDescription3"] = lblDesc.Text;
                        drCurrentRow3["Dbheadid3"] = lblHeadID.Text;

                        DRTable3.Rows.Add(drCurrentRow3);
                    }

                    ViewState["CurrentTableDB3"] = DRTable3;
                }
                #endregion

                if (totalleft3 == totalright3)
                {
                    lblHint.Text = "VConfirm";
                    lblHeading.Text = "Confirmation";
                    lblContent.Text = " Please Confirm Your Voucher Details...";
                    gvoldmember3.DataSource = dtConfirmation3;
                    gvoldmember3.DataBind();
                    MpAll.PopupControlID = "pnlpopup";
                    pnlpopup.Visible = true;
                    btnYes.Focus();
                    MpAll.Show();
                }
                else
                {
                    error3 = error3.Trim().Trim('+');
                    lblHeading.Text = "Warning!!!";
                    lblContent.Text = "Please Tally Credit and Debit Amount!!!" + "</br></br>" + error3.Trim().Trim('+') + " !=  " + errorDebit3.Trim().Trim('+');
                    MpAll.PopupControlID = "pnlpopup";
                    gvoldmember3.DataSource = dtConfirmation3;
                    gvoldmember3.DataBind();
                    MpAll.PopupControlID = "pnlpopup";
                    MpAll.Show();
                    btnYes.Focus();
                    pnlpopup.Visible = true;
                    dtConfirmation3.Dispose();
                }
                #endregion
            
            }
            catch (Exception ex)
            {
                this.Response.Redirect(this.Request.Url.AbsoluteUri);
            }
        }

        protected void GetSeriesAndVoucherNo(int iTabPage)
        {
            string strBranchID = Session["Branchid"].ToString();
            if (strBranchID == "161")
                strBranchID = (159 + iTabPage).ToString();
            
            CommonClassFile objCcls = new CommonClassFile();
            TextBox txtVoucherNo = txtVoucherNo1;
            if (iTabPage == 1)
                txtVoucherNo = txtVoucherNo1;
            else if (iTabPage == 2)
                txtVoucherNo = txtVoucherNo2;
            else if (iTabPage == 3)
                txtVoucherNo = txtVoucherNo3;

            using (MySqlConnection conn = objCcls.openConnection())
            {
                //MySqlTransaction trans = conn.BeginTransaction();
                try
                {
                    new MySqlCommand("LOCK TABLES `svcf`.`voucher` WRITE, `svcf`.`voucher` as v1 READ", conn).ExecuteNonQuery();
                    new MySqlCommand("INSERT INTO `svcf`.`voucher` (`DualTransactionKey`, `BranchID`, `CurrDate`, `Voucher_No`, `Voucher_Type`, `Head_Id`, `ChoosenDate`, `Narration`, `Amount`, `Series`, `ReceievedBy`, `Trans_Type`, `T_Day`, `T_Month`, `T_Year`, `Trans_Medium`, `RootID`, `Other_Trans_Type`, `IsDeleted`, `IsAccepted`, `ISActive`) SELECT unhex(replace(UUID(), '-', '')), " + strBranchID + ", curdate(), ifnull(max(cast(Voucher_No as unsigned)),0)+1, 'C', 0000, curdate(), '', 0.00, 'ZZZ', 'admin', 0, day(curdate()), month(curdate()), year(curdate()), 0, 1, 2, 0, 0, 0 FROM `svcf`.`voucher` as v1 where ChoosenDate>='2014/04/01' and  Series in ('VOUCHER', 'ZZZ') and Trans_Type in ('0') and BranchID = " + strBranchID + ";", conn).ExecuteNonQuery();
                    object obj = new MySqlCommand("select voucher_no from voucher where TransactionKey = (select LAST_INSERT_ID());", conn).ExecuteScalar();
                    new MySqlCommand("UNLOCK TABLES", conn).ExecuteNonQuery();
                    if (obj != null)
                    {
                        txtVoucherNo.Text = Convert.ToString(obj);
                    }
                    else
                    {
                        txtVoucherNo.Text = "";
                    }
                }
                catch (Exception)
                {

                }
            }
        }

        protected void IndianDateValidator_ServerValidate(object sender, ServerValidateEventArgs e)
        {
            DateTime d;
            bool isDate = DateTime.TryParseExact(e.Value, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out d);
            e.IsValid = isDate;
        }
        
        protected void IndianDateValidatorTodayorYesterday_ServerValidate(object sender, ServerValidateEventArgs e)
        {
            DateTime d;
            bool isDate = DateTime.TryParseExact(e.Value, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out d);
            bool isTodayorPreviousDate = false;
            if (isDate)
            {
                isTodayorPreviousDate = DateTime.Now.ToString("dd/MM/yyyy") == d.ToString("dd/MM/yyyy") || DateTime.Now.AddDays(-1).ToString("dd/MM/yyyy") == d.ToString("dd/MM/yyyy");
            }
            if (isDate == true & isTodayorPreviousDate == true)
            {
                e.IsValid = true;
            }
            else
            {
                e.IsValid = false;
            }
        }

        protected void GridClnt1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                int index = Convert.ToInt32(e.RowIndex);
                DataTable dtable = ViewState["CurrentTable1"] as DataTable;
                dtable.Rows[index].Delete();
                ViewState["CurrentTable1"] = dtable;
                GridClnt1.DataSource = ViewState["CurrentTable1"];
                GridClnt1.DataBind();

                //RefNo.RemoveAt(index);
                //RefNo1.RemoveAt(index);

                if (GridClnt1.Rows.Count == 0)
                {
                    SetInitRow_Clnt1();
                }
            }
            catch (Exception)
            {

            }
            finally
            {

            }
        }

        protected void GrdDbClnt1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                int index = Convert.ToInt32(e.RowIndex);
                DataTable dtable = ViewState["CurrentTableDB1"] as DataTable;
                dtable.Rows[index].Delete();
                ViewState["CurrentTableDB1"] = dtable;
                GrdDbClnt1.DataSource = ViewState["CurrentTableDB1"];
                GrdDbClnt1.DataBind();

                //RefNo.RemoveAt(index);
                //RefNo1.RemoveAt(index);

                if (GrdDbClnt1.Rows.Count == 0)
                {
                    SetInitDb_Clnt1();
                }
            }
            catch (Exception)
            {

            }
            finally
            {

            }
        }

        protected void GridClnt2_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                int index = Convert.ToInt32(e.RowIndex);
                DataTable dtable = ViewState["CurrentTable2"] as DataTable;
                dtable.Rows[index].Delete();
                ViewState["CurrentTable2"] = dtable;
                GridClnt2.DataSource = ViewState["CurrentTable2"];
                GridClnt2.DataBind();

                //RefNo.RemoveAt(index);
                //RefNo1.RemoveAt(index);

                if (GridClnt2.Rows.Count == 0)
                {
                    SetInitRow_Clnt2();
                }
            }
            catch (Exception)
            {

            }
        }

        protected void GrdDbClnt2_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                int index = Convert.ToInt32(e.RowIndex);
                DataTable dtable = ViewState["CurrentTableDB2"] as DataTable;
                dtable.Rows[index].Delete();
                ViewState["CurrentTableDB2"] = dtable;
                GrdDbClnt2.DataSource = ViewState["CurrentTableDB2"];
                GrdDbClnt2.DataBind();

                //RefNo.RemoveAt(index);
                //RefNo1.RemoveAt(index);

                if (GrdDbClnt2.Rows.Count == 0)
                {
                    SetInitDb_Clnt2();
                }
            }
            catch (Exception)
            {

            }
        }

        protected void GridClnt3_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                int index = Convert.ToInt32(e.RowIndex);
                DataTable dtable = ViewState["CurrentTable3"] as DataTable;
                dtable.Rows[index].Delete();
                ViewState["CurrentTable3"] = dtable;
                GridClnt3.DataSource = ViewState["CurrentTable3"];
                GridClnt3.DataBind();

                //RefNo.RemoveAt(index);
                //RefNo1.RemoveAt(index);

                if (GridClnt3.Rows.Count == 0)
                {
                    SetInitRow_Clnt3();
                }
            }
            catch (Exception)
            {

            }
        }

        protected void GrdDbClnt3_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                int index = Convert.ToInt32(e.RowIndex);
                DataTable dtable = ViewState["CurrentTableDB3"] as DataTable;
                dtable.Rows[index].Delete();
                ViewState["CurrentTableDB3"] = dtable;
                GrdDbClnt3.DataSource = ViewState["CurrentTableDB3"];
                GrdDbClnt3.DataBind();

                //RefNo.RemoveAt(index);
                //RefNo1.RemoveAt(index);

                if (GrdDbClnt3.Rows.Count == 0)
                {
                    SetInitDb_Clnt3();
                }
            }
            catch (Exception)
            {

            }
        }
        
    }
}