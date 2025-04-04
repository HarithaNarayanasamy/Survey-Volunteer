using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using DevExpress.Web.ASPxClasses;
using DevExpress.Web;
using DevExpress.Web.ASPxEditors;
using SVCF_BusinessAccessLayer;
using SVCF_TransactionLayer;
using SVCF_DataAccessLayer;
using log4net;
using log4net.Config;

namespace SreeVisalamChitFundLtd_phase1
{
    public partial class New_Receipt_Voucher : System.Web.UI.Page
    {
        #region Object
        BusinessLayer balayer = new BusinessLayer();
        TransactionLayer trn = new TransactionLayer();
        #endregion

        ILog logger = log4net.LogManager.GetLogger(typeof(New_Receipt_Voucher));
        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (Session["UserName"] == null || Session["Branchid"] == null || Session["BranchName"] == null)
            {
                Response.Redirect(Page.ResolveUrl("~/Login.aspx"), true);
            }
        }
        protected void Page_Init(object sender, EventArgs e)
        {
            img16List.ImageUrl = Page.ResolveUrl("~/pertho_admin_v1.3/img/ico/icSw2/16-List.png");
            Image img = (Image)UpdateProgress1.FindControl("imgWaiting");
            img.ImageUrl = Page.ResolveUrl("~/Styles/Image/waiting.gif");
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            pnlmsg.Visible = false;
            Pnlgendrate.Visible = false;
            if (!Page.IsPostBack)
            {
                Session["CheckRefresh"] = System.Guid.NewGuid().ToString();
                SetInitialRow();
                CollectorName();
                txtReceivedBy.Text = balayer.ToobjectstrEvenNull(Session["UserName"]);
                ddlColloctorName.Focus();
                //fillBankHead();
            }
        }
        protected void Page_PreRender(object sender, EventArgs e)
        {
            ViewState["CheckRefresh"] = Session["CheckRefresh"];
        }
        protected void OrderGrid_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                ImageButton ButtonAdd = (e.Row.FindControl("aDDpAN") as Panel).FindControl("ButtonAdd") as ImageButton;
                ToolkitScriptManager1.RegisterAsyncPostBackControl(ButtonAdd);
            }
        }
        protected void ddlMemberName_IndexChanged(object sender, EventArgs e)
        {

            DropDownList ddl = (DropDownList)sender;
            if (ddl.SelectedItem.Text != "")
            {
                ddl.ToolTip = ddl.SelectedItem.Text;
            }
            else
            {
                ddl.ToolTip = string.Empty;
            }
        }
        void fillBankHead()
        {
            DataTable dtBank = balayer.GetDataTable("SELECT concat( concat(t1.BankName,'_',t1.IFCCode),'_',t1.AccountNo) as BankDetails, t1.Head_Id as Head_Id, t1.Banklocation as Banklocation FROM svcf.bankdetails as t1 join headstree as t2 on (t1.Head_Id=t2.NodeId) where t1.BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]));
            DataRow dr = dtBank.NewRow();
            dr[0] = "--Select--";
            dr[1] = "0";
            ddlBankHead.DataValueField = "Head_Id";
            ddlBankHead.DataTextField = "BankDetails";
            ddlBankHead.DataSource = dtBank;
            dtBank.Rows.InsertAt(dr, 0);
            ddlBankHead.DataBind();
        }
        public void CollectorName()
        {
            DataTable dtCollector = balayer.GetDataTable("Select BranchID, moneycollid,moneycollname from moneycollector where BranchID='" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + "'");
            DataRow dr = dtCollector.NewRow();
            dr[1] = "0";
            dr[2] = "--Select--";
            ddlColloctorName.DataValueField = "moneycollid";
            ddlColloctorName.DataTextField = "moneycollname";
            ddlColloctorName.DataSource = dtCollector;
            dtCollector.Rows.InsertAt(dr, 0);
            ddlColloctorName.DataBind();
            ddlReceiptSeries.Focus();
        }
        protected void ddlReceiptSeries_SelectedIndexChanged(object sender, EventArgs e)
        {
            getRecieptBookNO(ddlReceiptSeries.SelectedValue, ddlColloctorName.SelectedValue);
            txtTotalAmount.Focus();
        }
        public void getRecieptBookNO(string Series, string CollectorID)
        {
            DataTable dtAll = balayer.GetDataTable("SELECT  alreadyusedreceipts,receiptnoto   FROM svcf.assignreceiptbook where  moneycollid=" + ddlColloctorName.SelectedValue + "  and IsFinished=0 and BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and receiptseries='" + ddlReceiptSeries.SelectedItem.Text + "'");
            if (dtAll.Rows.Count != 0)
            {
                int from = int.Parse(dtAll.Rows[0][0].ToString());
                int t0 = int.Parse(dtAll.Rows[0][1].ToString());
                string strQuery = "select ifnull(max(Voucher_No)+1,0) from voucher where Voucher_No>=" + from + " and Voucher_No<=" + t0 + " and `Series`='" + ddlReceiptSeries.SelectedItem.Text + "'";
                int RecNo = int.Parse(balayer.GetSingleValue(strQuery));
                TextBox ReceiptNo = (TextBox)GridView1.Rows[GridView1.Rows.Count - 1].FindControl("txtReceiptNo");
                if (RecNo != 0)
                {
                    ReceiptNo.Text = RecNo.ToString();
                }
                else
                {
                    ReceiptNo.Text = from.ToString();
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Warning", "alert(' Please Assign new Reciept Book!!!');", true);
            }
            //txtReceiptNo.Text = balayer.GetSingleValue(strQuery);
            //if (txtReceiptNo.Text.Trim() == "1" || string.IsNullOrEmpty(txtReceiptNo.Text.Trim()))
            //{
            //    txtReceiptNo.Text = balayer.GetSingleValue("SELECT min(receiptnofrom)  FROM  `assignreceiptbook` where isFinished='0' and moneycollid='" + ddlColloctorName.SelectedValue + "' and receiptseries='" + ddlReceiptSeries.SelectedItem.Text + "'");
            //    if (balayer.GetSingleValue("select Count(*) from  receiptmaster where ReceiptNO='" + txtReceiptNo.Text + "' and Series='" + Series + "'") == "1")
            //    {
            //        
            //        txtReceiptNo.Text = "";
            //    }
            //}
        }
        void series()
        {
            DataTable dtMC = balayer.GetDataTable("Select distinct receiptseries from assignreceiptbook where moneycollid='" + ddlColloctorName.SelectedValue + "' and IsFinished='0'");
            if (dtMC.Rows.Count == 0)
            {
                ClientScript.RegisterStartupScript(this.Page.GetType(), "Validation", "<script Language='Javascript'>alert('Please Assign Receipt Book For " + ddlColloctorName.SelectedItem.Text + " (" + ddlColloctorName.SelectedValue + ") ')</script>");
            }
            else if ((dtMC.Rows.Count > 1))
            {
                DataTable dtMCtemp = balayer.GetDataTable("select distinct Series as receiptseries from receiptmaster where (select mod(max(ReceiptNo) ,200) <>'0') and CollectedBy='" + ddlColloctorName.SelectedItem.Text + "' ");
                if (dtMCtemp.Rows.Count != 0)
                {
                    dtMC = dtMCtemp.Copy();
                }
            }
            DataRow dr = dtMC.NewRow();
            dr[0] = "--Select--";
            ddlReceiptSeries.DataValueField = "receiptseries";
            ddlReceiptSeries.DataTextField = "receiptseries";
            ddlReceiptSeries.DataSource = dtMC;
            dtMC.Rows.InsertAt(dr, 0);
            ddlReceiptSeries.DataBind();
            //if (dtMC.Rows.Count == 1)
            //{
            //    ddlReceiptSeries.SelectedIndex = 1;
            //}
            ddlColloctorName.Focus();
            SetInitialRow();
        }
        protected void ddlColloctorName_SelectedIndexChanged(object sender, EventArgs e)
        {
            series();
        }
        
        protected void chkLoadAllChit_CheckedChanged(object sender, EventArgs e)
        {
            
            //By nithi
            SetInitialRow();
            CollectorName();
            series();
            txtReceivedBy.Text = balayer.ToobjectstrEvenNull(Session["UserName"]);
            ddlColloctorName.Focus();
            //fillBankHead();
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Choose Your Transactional Details  Again!!!');", true);
           
        }
        protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            check();
        }
        public void check()
        {
            if (CheckBox1.Checked)
            {
                fillBankHead();
                txtIfsc.Focus();
                txtIfsc.Visible = true;
                txtDate_in_Cheque.Visible = true;
                txtBankLocation.Visible = true;
                lblIFSC.Visible = true;
                lblDate.Visible = true;
                lblBL.Visible = true;
                txtIfsc.Focus();
                RFVtxtIfsc.Visible = true;
                RFVtxtBankLocation.Visible = true;
                RFVtxtDate_in_Cheque.Visible = true;
                ddlBankHead.Visible = true;
                CVddlBankHead.Visible = true;
                lblBankHead.Visible = true;              
            }
            else
            {
                RFVtxtIfsc.Visible = false;
                RFVtxtBankLocation.Visible = false;
                RFVtxtDate_in_Cheque.Visible = false;
                txtIfsc.Visible = false;
                txtDate_in_Cheque.Visible = false;
                txtBankLocation.Visible = false;
                lblIFSC.Visible = false;
                lblDate.Visible = false;
                lblBL.Visible = false;
                ddlBankHead.Visible = false;
                lblBankHead.Visible = false;
                CVddlBankHead.Visible = false;
                btnGenerate.Focus();
            }
            // UpdatePanel1.Update();
        }
     
        private void FillDropDownList(DropDownList ddl, int iType, string GroupID)
        {  //chit
            if (iType == 0)
            {
                ddl.DataSource = null;
                DataTable dtgroupno = null;
                if (chkLoadAllChit.Checked == false)
                {
                    dtgroupno = balayer.GetDataTable("select Head_Id,GROUPNO from groupmaster where BranchID='" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + "'");
                }
                else
                {
                    dtgroupno = balayer.GetDataTable("select Head_Id,GROUPNO from groupmaster");
                }
                ddl.DataSource = dtgroupno;
                ddl.DataValueField = "Head_Id";
                ddl.DataTextField = "GROUPNO";
                ddl.DataBind();
                ddl.Items.Insert(0, "--Select--");
                //.groupbindingCode
            }
            //token
            else if (iType == 1)
            {
                DropDownList ddlGroupNo = (DropDownList)GridView1.Rows[0].Cells[1].FindControl("ddlGroupNo");
                if (ddlGroupNo.SelectedItem.Text != "--Select--")
                {
                    DataTable dtgroupno = balayer.GetDataTable(@"SELECT concat(cast(Head_Id as char),'|',cast(MemberID as char)) as IDS,concat(MemberName,'          ',',',GrpMemberID) as MemberName FROM membertogroupmaster where  GroupID=" + GroupID + ";");
                    // DataTable dtgroupno = SreeVisalamChitFundLtd_phase1.balayer.GetDataTable("select Head_Id,GROUPNO from groupmaster where Head_Id='" + SreeVisalamChitFundLtd_phase1.balayer.ToobjectstrEvenNull(Session["Branchid"]) + "'");
                    ddl.DataSource = dtgroupno;
                    ddl.DataValueField = "IDS";
                    ddl.DataTextField = "MemberName";
                    ddl.DataBind();
                    ddl.Items.Insert(0, "--Select--");
                    //.groupbindingCode
                }
                else
                {
                    ddl.DataSource = null;
                    ddl.Items.Clear();
                    ddl.Items.Insert(0, "--Select--");
                    ddl.DataBind();
                }
            }
            //misc
            else if (iType == 2)
            {
                
                DataTable dtgroupno = balayer.GetDataTable("SELECT TreeID,TREE FROM svcf.view_parent where RootID=11");
               
                ddl.DataSource = dtgroupno;
                DataRow dr = dtgroupno.NewRow();
                dr[0] = "0";
                dr[1] = "--Select--";
                ddl.DataValueField = "TreeID";
                ddl.DataTextField = "TREE";
                dtgroupno.Rows.InsertAt(dr, 0);
                ddl.DataBind();
            }
        }
        private void SetInitialRow()
        {
            DataTable dt = new DataTable();
            DataRow dr = null;
            //Define the Columns
            dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
            dt.Columns.Add(new DataColumn("GroupNo", typeof(string)));
            dt.Columns.Add(new DataColumn("MemberName", typeof(string)));
            dt.Columns.Add(new DataColumn("Amount", typeof(string)));
            dt.Columns.Add(new DataColumn("MiscHead", typeof(string)));
            dt.Columns.Add(new DataColumn("MiscAmount", typeof(string)));
            dt.Columns.Add(new DataColumn("txtReceiptNo", typeof(string)));
            //Add a Dummy Data on Initial Load
            dr = dt.NewRow();
            dr["RowNumber"] = 1;
            dt.Rows.Add(dr);
            //Store the DataTable in ViewState
            ViewState["CurrentTable"] = dt;
            //Bind the DataTable to the Grid
            GridView1.DataSource = dt;
            GridView1.DataBind();
            //Extract and Fill the DropDownList with Data
            DropDownList ddlGroupNo = (DropDownList)GridView1.Rows[0].Cells[1].FindControl("ddlGroupNo");
            DropDownList ddlMisc = (DropDownList)GridView1.Rows[0].Cells[2].FindControl("ddlMisc");
            //ddlMisc.DataSource = balayer.GetDataTable("SELECT * FROM svcf.view_parent where RootID=11");
            //ddlMisc.DataBind();
            // TextBox ddl3 = (TextBox) GridView1.Rows[0].Cells[3].FindControl("txtAmount");
            FillDropDownList(ddlGroupNo, 0, "");
            FillDropDownList(ddlMisc, 2, "");
            // FillDropDownList(ddl3);
        }
        private void RemoveLastRowToGrid()
        {
            if (ViewState["CurrentTable"] != null)
            {
                DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
                DataRow drCurrentRow = null;
                if (dtCurrentTable.Rows.Count > 1)
                {
                    dtCurrentTable.Rows.RemoveAt(dtCurrentTable.Rows.Count - 1);                   
                    GridView1.DataSource = dtCurrentTable;
                    GridView1.DataBind();
                    ViewState["CurrentTable"] = dtCurrentTable;
                }
            }
            //else
            //{
            //    Response.Write("ViewState is null");
            //}
            //Set Previous Data on Postbacks
            SetPreviousData(true);
            TextBox ReceiptNo = (TextBox)GridView1.Rows[GridView1.Rows.Count - 1].FindControl("txtReceiptNo");
            ReceiptNo.Focus();
        }
        private void AddNewRowToGrid()
        {
            if (ViewState["CurrentTable"] != null)
            {
                DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
                DataRow drCurrentRow = null;
                if (dtCurrentTable.Rows.Count > 0)
                {
                    DropDownList ddlMiscVal = (DropDownList)GridView1.Rows[dtCurrentTable.Rows.Count - 1].FindControl("ddlMisc");
                    TextBox txtMiscVal = (TextBox)GridView1.Rows[dtCurrentTable.Rows.Count - 1].FindControl("txtMisc");
                    decimal lastAmount = 0.0M;
                    bool isMisc = decimal.TryParse(txtMiscVal.Text, out lastAmount);
                    if ((isMisc == true & lastAmount > 0.0M))
                    {
                        isMisc = true;
                    }
                    else
                    {
                        isMisc = false;
                    }
                    if ((ddlMiscVal.SelectedIndex <= 0) & isMisc == true)
                    {
                        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Choose Misc Head Valid Details');", true);
                        //Response.Write("<script>alert('Please Provide Valid Details');</script>");
                        return;
                    }
                    if (ddlMiscVal.SelectedIndex > 0 & isMisc != true)
                    {
                        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Provide Misc Amount Details');", true);
                        return;
                        //Response.Write("<script>alert('Please Provide Valid Details');</script>");
                    }
                    drCurrentRow = dtCurrentTable.NewRow();
                    drCurrentRow["RowNumber"] = dtCurrentTable.Rows.Count + 1;
                    //add new row to DataTable
                    dtCurrentTable.Rows.Add(drCurrentRow);
                    //Store the current data to ViewState
                    ViewState["CurrentTable"] = dtCurrentTable;
                    for (int i = 0; i < dtCurrentTable.Rows.Count - 1; i++)
                    {
                        //extract the DropDownList Selected Items
                        DropDownList ddlGroupNo = (DropDownList)GridView1.Rows[i].FindControl("ddlGroupNo");
                        DropDownList ddlMemberName = (DropDownList)GridView1.Rows[i].FindControl("ddlMemberName");
                        TextBox txtAmount = (TextBox)GridView1.Rows[i].FindControl("txtAmount");
                        DropDownList ddlMisc = (DropDownList)GridView1.Rows[i].FindControl("ddlMisc");
                        TextBox txtReceiptNo = (TextBox)GridView1.Rows[i].FindControl("txtReceiptNo");
                        TextBox txtMisc = (TextBox)GridView1.Rows[i].FindControl("txtMisc");
                        // Update the DataRow with the DDL Selected Items
                        dtCurrentTable.Rows[i]["GroupNo"] = ddlGroupNo.SelectedValue;
                        dtCurrentTable.Rows[i]["MemberName"] = ddlMemberName.SelectedValue;
                        dtCurrentTable.Rows[i]["Amount"] = txtAmount.Text;
                        dtCurrentTable.Rows[i]["MiscHead"] = ddlMisc.SelectedValue;
                        dtCurrentTable.Rows[i]["MiscAmount"] = txtMisc.Text;
                        dtCurrentTable.Rows[i]["txtReceiptNo"] = txtReceiptNo.Text;
                    }
                    //Rebind the Grid with the current data
                    GridView1.DataSource = dtCurrentTable;
                    GridView1.DataBind();
                    DropDownList ddlGroupNonew = (DropDownList)GridView1.Rows[GridView1.Rows.Count - 1].FindControl("ddlGroupNo");
                    DropDownList ddlMiscnew = (DropDownList)GridView1.Rows[GridView1.Rows.Count - 1].FindControl("ddlMisc");
                    FillDropDownList(ddlGroupNonew, 0, "");
                    FillDropDownList(ddlMiscnew, 2, "");
                    TextBox ReceiptNo = (TextBox)GridView1.Rows[GridView1.Rows.Count - 1].FindControl("txtReceiptNo");
                    ReceiptNo.Focus();
                }
            }
            //else
            //{
            //    Response.Write("ViewState is null");
            //}
            //Set Previous Data on Postbacks
            SetPreviousData(false);
        }
        private void SetPreviousData(bool isRemove)
        {
            int rowIndex = 0;
            if (ViewState["CurrentTable"] != null)
            {
                DataTable dt = (DataTable)ViewState["CurrentTable"];
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        //Set the Previous Selected Items on Each DropDownList on Postbacks
                        DropDownList ddlGroupNo = (DropDownList)GridView1.Rows[i].FindControl("ddlGroupNo");
                        DropDownList ddlMemberName = (DropDownList)GridView1.Rows[i].FindControl("ddlMemberName");
                        TextBox txtAmount = (TextBox)GridView1.Rows[i].FindControl("txtAmount");
                        DropDownList ddlMisc = (DropDownList)GridView1.Rows[i].FindControl("ddlMisc");
                        TextBox txtMisc = (TextBox)GridView1.Rows[i].FindControl("txtMisc");
                        TextBox txtReceiptNo = (TextBox)GridView1.Rows[i].FindControl("txtReceiptNo");
                        //Fill the DropDownList with Data
                        FillDropDownList(ddlGroupNo, 0, "");
                        FillDropDownList(ddlMisc, 2, "");
                        //FillDropDownList(ddl3);
                        if (i < dt.Rows.Count)
                        {
                            if (isRemove == false & i == dt.Rows.Count - 1)
                            {
                                break;
                            }
                            ddlGroupNo.ClearSelection();
                            ddlGroupNo.Items.FindByValue(dt.Rows[i]["GroupNo"].ToString()).Selected = true;
                            FillDropDownList(ddlMemberName, 1, ddlGroupNo.SelectedValue);
                            ddlMemberName.ClearSelection();
                            ddlMemberName.Items.FindByValue(dt.Rows[i]["MemberName"].ToString()).Selected = true;
                            ddlMisc.ClearSelection();
                            ddlMisc.Items.FindByValue(dt.Rows[i]["MiscHead"].ToString()).Selected = true;
                            txtAmount.Text = dt.Rows[i]["Amount"].ToString();
                            txtMisc.Text = dt.Rows[i]["MiscAmount"].ToString();
                            txtReceiptNo.Text = dt.Rows[i]["txtReceiptNo"].ToString();
                            //ddl3.ClearSelection();
                            //ddl3.Items.FindByValue(dt.Rows[i]["Column3"].ToString()).Selected = true;
                        }
                        rowIndex++;
                    }
                    if (isRemove == false)
                    {
                        ((TextBox)GridView1.Rows[GridView1.Rows.Count - 1].FindControl("txtReceiptNo")).Text = ((TextBox)GridView1.Rows[GridView1.Rows.Count - 2].FindControl("txtReceiptNo")).Text;
                    }
                }
            }
        }
        protected void ddlGroupNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow gvRow = (GridViewRow)((DropDownList)sender).Parent.Parent;
            DropDownList ddlMemberName = (DropDownList)gvRow.FindControl("ddlMemberName");
            string selectedValue = ((DropDownList)gvRow.FindControl("ddlGroupNo")).SelectedValue;
            FillDropDownList(ddlMemberName, 1, selectedValue);
            ((DropDownList)gvRow.FindControl("ddlGroupNo")).Focus();
        }
        protected void ButtonRemove_Click(object sender, ImageClickEventArgs e)
        {
            if (GridView1.Rows.Count > 1)
            {
                RemoveLastRowToGrid();
            }
        }
        protected void ButtonAdd_Click(object sender, ImageClickEventArgs e)
        {
            Page.Validate("GrpRow");
            foreach (GridViewRow gvRow in GridView1.Rows)
            {
                ((RequiredFieldValidator)gvRow.FindControl("RFVtxtAmount")).Validate();
                ((RegularExpressionValidator)gvRow.FindControl("REVtxtAmount")).Validate();
                ((CompareValidator)gvRow.FindControl("CVddlGroupNo")).Validate();
                ((CompareValidator)gvRow.FindControl("CVddlMemberName")).Validate();
                ((CompareValidator)gvRow.FindControl("CVGrpRowtxtReceiptNo")).Validate();
                ((RequiredFieldValidator)gvRow.FindControl("RFVGrpRowtxtReceiptNo")).Validate();             
            }
            string errorMessage = "";
            if (Page.IsValid)
            {
                AddNewRowToGrid();
            }
        }
        //btnConfirmationYes_Click
        protected void btnConfirmationNo_Click(object sender, EventArgs e)
        {
            gvConfirm.DataSource = null;
            gvConfirm.DataBind();
            ModalPopupExtender1.Hide();
            pnlConfirmation.Visible = false;
        }
        protected void btnConfirmationYes_Click(object sender, EventArgs e)
        {
           // TransactionLayer trn = new TransactionLayer();
            string TransactionKeyDue = "";
            gvConfirm.DataSource = null;
            gvConfirm.DataBind();
            //byte[] ba = new byte[16];
            //Random rd = new Random();
            //rd.NextBytes(ba);
            System.Guid guid = Guid.NewGuid();
            // Prepare GUID values in SQL format
            string hexstring = BitConverter.ToString(guid.ToByteArray());
            string guidForBinary16 = "0x" + hexstring.Replace("-", string.Empty);
            string DualTransactionKey = guidForBinary16;
            try
            {
                DateTime dtChoosenDate = DateTime.Parse(txtReceivedDate.Text);
                string CashOrBankID = "";
                string trans_medium = "";
                if (CheckBox1.Checked != true)
                {
                    CashOrBankID = "12";
                    trans_medium = "0";
                }
                else
                {
                    CashOrBankID = ddlBankHead.SelectedValue;
                    trans_medium = "1";
                }
                //  string ChequeGivenMemberID = ((DropDownList)GridView1.Rows[0].FindControl("ddlMemberName")).SelectedValue.ToString().Split('|')[1]; ;
                // string BankHeadID = ddlBankHead.SelectedValue;
                //if (CheckBox1.Checked != true)
                //{
                //Cash
                for (int i = 0; i < GridView1.Rows.Count; i++)
                {
                    DropDownList RowddlGroupNo = (DropDownList)GridView1.Rows[i].FindControl("ddlGroupNo");
                    DropDownList RowddlMemberName = (DropDownList)GridView1.Rows[i].FindControl("ddlMemberName");
                    TextBox RowtxtAmount = (TextBox)GridView1.Rows[i].FindControl("txtAmount");
                    DropDownList RowddlMisc = (DropDownList)GridView1.Rows[i].FindControl("ddlMisc");
                    TextBox RowtxtMisc = (TextBox)GridView1.Rows[i].FindControl("txtMisc");
                    TextBox txtReceiptNo = (TextBox)GridView1.Rows[i].FindControl("txtReceiptNo");
                    string ChitsBranchID = balayer.GetSingleValue("SELECT BranchID FROM `svcf`.`groupmaster` where Head_Id=" + RowddlGroupNo.SelectedValue + "");
                    string MemberID = RowddlMemberName.SelectedValue.Split('|')[1];
                    string TokenNo = RowddlMemberName.SelectedValue.Split('|')[0];
                    string RootID = "";
                    if (CheckBox1.Checked == true)
                    {
                        RootID = "3";
                    }
                    else
                    {
                        RootID = "12";
                    }
                    if (ChitsBranchID == balayer.ToobjectstrEvenNull(Session["Branchid"]))
                    {
                        //Local ChitsTransaction
                        //DueAmount
                        if (decimal.Parse(RowtxtAmount.Text) != 0.00M)
                        {
                            string strChitHeadQuery = "INSERT INTO `svcf`.`voucher` (`DualTransactionKey`,`BranchID`,`CurrDate`,`Voucher_No`,`Voucher_Type`,`Head_Id`,`ChoosenDate`,`Narration`,`Amount`,`Series`,`ReceievedBy`,`Trans_Type`,`T_Day`,`T_Month`,`T_Year`,`MemberID`,`Trans_Medium`,`RootID`,`ChitGroupId`) VALUES (" + DualTransactionKey + "," + balayer.ToobjectstrEvenNull(Session["Branchid"]) + ",'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," + txtReceiptNo.Text + ",'C'," + TokenNo + ",'" + dtChoosenDate.ToString("yyyy-MM-dd") + "','" + "Recd From:" + RowddlMemberName.SelectedItem.Text + " For DrawNo:" + RowddlMemberName.ToolTip.ToString() + "'," + RowtxtAmount.Text + ",'" + ddlReceiptSeries.SelectedItem.Text + "','" + txtReceivedBy.Text + "',1," + dtChoosenDate.Day + "," + dtChoosenDate.Month + "," + dtChoosenDate.Year + "," + MemberID + "," + trans_medium + ",5," + RowddlGroupNo.SelectedValue + ") ";
                            string strCashHeadQuery = "INSERT INTO `svcf`.`voucher` (`DualTransactionKey`,`BranchID`,`CurrDate`,`Voucher_No`,`Voucher_Type`,`Head_Id`,`ChoosenDate`,`Narration`,`Amount`,`Series`,`ReceievedBy`,`Trans_Type`,`T_Day`,`T_Month`,`T_Year`,`MemberID`,`Trans_Medium`,`RootID`,`ChitGroupId`) VALUES (" + DualTransactionKey + "," + balayer.ToobjectstrEvenNull(Session["Branchid"]) + ",'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," + txtReceiptNo.Text + ",'D'," + CashOrBankID + ",'" + dtChoosenDate.ToString("yyyy-MM-dd") + "','" + "Recd From:" + RowddlMemberName.SelectedItem.Text + " For DrawNo:" + RowddlMemberName.ToolTip.ToString() + "'," + RowtxtAmount.Text + ",'" + ddlReceiptSeries.SelectedItem.Text + "','" + txtReceivedBy.Text + "',1," + dtChoosenDate.Day + "," + dtChoosenDate.Month + "," + dtChoosenDate.Year + "," + MemberID + "," + trans_medium + "," + RootID + "," + RowddlGroupNo.SelectedValue + ") ";
                            //balayer.GetInsertItem(strChitHeadQuery);
                            long strChitHead = trn.insertorupdateTrn(strChitHeadQuery);
                            //balayer.GetInsertItem(strCashHeadQuery);
                            long strCashHead = trn.insertorupdateTrn(strCashHeadQuery);
                            if (CheckBox1.Checked == true)
                            {
                                TransactionKeyDue =strCashHead.ToString();
                            }
                        }
                        //   Mis amount
                        if (RowddlMisc.SelectedIndex > 0)
                        {
                            if (decimal.Parse(RowtxtMisc.Text) != 0.00M)
                            {
                                string strChitMiscHeadQuery = "INSERT INTO `svcf`.`voucher` (`DualTransactionKey`,`BranchID`,`CurrDate`,`Voucher_No`,`Voucher_Type`,`Head_Id`,`ChoosenDate`,`Narration`,`Amount`,`Series`,`ReceievedBy`,`Trans_Type`,`T_Day`,`T_Month`,`T_Year`,`MemberID`,`Trans_Medium`,`RootID`,`ChitGroupId`) VALUES (" + DualTransactionKey + "," + balayer.ToobjectstrEvenNull(Session["Branchid"]) + ",'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," + txtReceiptNo.Text + ",'C'," + RowddlMisc.SelectedValue + ",'" + dtChoosenDate.ToString("yyyy-MM-dd") + "','" + RowddlMisc.SelectedItem.Text + " Recd from " + RowddlMemberName.SelectedItem.Text + "'," + RowtxtMisc.Text + ",'" + ddlReceiptSeries.SelectedItem.Text + "','" + balayer.ToobjectstrEvenNull(Session["UserName"]) + "',1," + dtChoosenDate.Day + "," + dtChoosenDate.Month + "," + dtChoosenDate.Year + "," + MemberID + "," + trans_medium + ",11," + RowddlGroupNo.SelectedValue + ") ";
                                string strCashHeadMiscQuery = "INSERT INTO `svcf`.`voucher` (`DualTransactionKey`,`BranchID`,`CurrDate`,`Voucher_No`,`Voucher_Type`,`Head_Id`,`ChoosenDate`,`Narration`,`Amount`,`Series`,`ReceievedBy`,`Trans_Type`,`T_Day`,`T_Month`,`T_Year`,`MemberID`,`Trans_Medium`,`RootID`,`ChitGroupId`) VALUES (" + DualTransactionKey + "," + balayer.ToobjectstrEvenNull(Session["Branchid"]) + ",'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," + txtReceiptNo.Text + ",'D'," + CashOrBankID + ",'" + dtChoosenDate.ToString("yyyy-MM-dd") + "','" + RowddlMisc.SelectedItem.Text + " Recd from " + RowddlMemberName.SelectedItem.Text + "'," + RowtxtMisc.Text + ",'" + ddlReceiptSeries.SelectedItem.Text + "','" + balayer.ToobjectstrEvenNull(Session["UserName"]) + "',1," + dtChoosenDate.Day + "," + dtChoosenDate.Month + "," + dtChoosenDate.Year + "," + MemberID + "," + trans_medium + "," + RootID + "," + RowddlGroupNo.SelectedValue + ") ";
                                long strChitMiscHead = trn.insertorupdateTrn(strChitMiscHeadQuery);
                                long strCashHeadMisc = trn.insertorupdateTrn(strCashHeadMiscQuery);
                                //balayer.GetInsertItem(strChitMiscHeadQuery);
                                //balayer.GetInsertItem(strCashHeadMiscQuery);
                                if (CheckBox1.Checked == true)
                                {
                                    string BankName = "";
                                    //string TransactionKey = balayer.GetSingleValue("SELECT max(TransactionKey)  FROM `svcf`.`voucher` where uuid_from_bin(DualTransactionKey)=uuid_from_bin(" + DualTransactionKey + ") and Head_Id='" + RowddlMisc.SelectedValue + "';");
                                    string strBankMiscInsertQuery = "insert into transbank(BranchID, TransactionKey, DualTransactionKey, T_Day, T_Month, T_Year, BankHeadID, Head_Id, MemberID, CustomersBankName, DateInCheque, ChequeDDNO, GivenAmount, TotalChequeDDAmount, IsBounced, Trans_Type) values (" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + "," + strChitMiscHead + "," + DualTransactionKey + "," + dtChoosenDate.Day + "," + dtChoosenDate.Month + "," + dtChoosenDate.Year + "," + ddlBankHead.SelectedValue + "," + RowddlMisc.SelectedValue + "," + MemberID + ",'" + txtBankLocation.Text + "','" + balayer.indiandateToMysqlDate(txtDate_in_Cheque.Text) + "'," + txtIfsc.Text + "," + RowtxtMisc.Text + "," + txtTotalAmount.Text + ",0,1)";
                                    trn.insertorupdateTrn(strBankMiscInsertQuery);
                                }
                            }
                        }
                    }
                    else
                    {
                        string strChitHeadQuery = "INSERT INTO `svcf`.`voucher` (`Other_Trans_Type`,`DualTransactionKey`,`BranchID`,`CurrDate`,`Voucher_No`,`Voucher_Type`,`Head_Id`,`ChoosenDate`,`Narration`,`Amount`,`Series`,`ReceievedBy`,`Trans_Type`,`T_Day`,`T_Month`,`T_Year`,`MemberID`,`Trans_Medium`,`RootID`,`ChitGroupId`) VALUES (1," + DualTransactionKey + "," + balayer.ToobjectstrEvenNull(Session["Branchid"]) + ",'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," + txtReceiptNo.Text + ",'C'," + ChitsBranchID + ",'" + dtChoosenDate.ToString("yyyy-MM-dd") + "','" + "Ref No:" + TokenNo + " |Recd From:" + RowddlMemberName.SelectedItem.Text + " For DrawNo:" + RowddlMemberName.ToolTip.ToString() + " (aprox) Received By: " + balayer.ToobjectstrEvenNull(Session["BranchName"]) + "'," + RowtxtAmount.Text + ",'" + ddlReceiptSeries.SelectedItem.Text + "','" + balayer.ToobjectstrEvenNull(Session["UserName"]) + "',1," + dtChoosenDate.Day + "," + dtChoosenDate.Month + "," + dtChoosenDate.Year + "," + MemberID + "," + trans_medium + ",1," + RowddlGroupNo.SelectedValue + ")";
                        string strCashHeadQuery = "INSERT INTO `svcf`.`voucher` (`Other_Trans_Type`,`DualTransactionKey`,`BranchID`,`CurrDate`,`Voucher_No`,`Voucher_Type`,`Head_Id`,`ChoosenDate`,`Narration`,`Amount`,`Series`,`ReceievedBy`,`Trans_Type`,`T_Day`,`T_Month`,`T_Year`,`MemberID`,`Trans_Medium`,`RootID`,`ChitGroupId`) VALUES (1," + DualTransactionKey + "," + balayer.ToobjectstrEvenNull(Session["Branchid"]) + ",'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," + txtReceiptNo.Text + ",'D'," + CashOrBankID + ",'" + dtChoosenDate.ToString("yyyy-MM-dd") + "','" + "Ref No:" + TokenNo + " |Recd From" + RowddlMemberName.SelectedItem.Text + " For DrawNo:" + RowddlMemberName.ToolTip.ToString() + " (aprox) Received By: " + balayer.ToobjectstrEvenNull(Session["BranchName"]) + "'," + RowtxtAmount.Text + ",'" + ddlReceiptSeries.SelectedItem.Text + "','" + balayer.ToobjectstrEvenNull(Session["UserName"]) + "',1," + dtChoosenDate.Day + "," + dtChoosenDate.Month + "," + dtChoosenDate.Year + "," + MemberID + "," + trans_medium + "," + RootID + "," + RowddlGroupNo.SelectedValue + ")";
                        //balayer.GetInsertItem(strChitHeadQuery);
                        //balayer.GetInsertItem(strCashHeadQuery);
                        long strChitHead = trn.insertorupdateTrn(strChitHeadQuery);
                        long strCashHead = trn.insertorupdateTrn(strCashHeadQuery);
                        if (CheckBox1.Checked == true)
                        {
                            //TransactionKeyDue = balayer.GetSingleValue("SELECT TransactionKey  FROM `svcf`.`voucher` where uuid_from_bin(DualTransactionKey)=uuid_from_bin(" + DualTransactionKey + ") and RootID=3 order by TransactionKey desc limit 1" + ";");
                            TransactionKeyDue = strCashHead.ToString();
                        }
                        if (RowddlMisc.SelectedIndex > 0)
                        {
                            string strChitmISCHeadQuery = "INSERT INTO `svcf`.`voucher` (`Other_Trans_Type`,`DualTransactionKey`,`BranchID`,`CurrDate`,`Voucher_No`,`Voucher_Type`,`Head_Id`,`ChoosenDate`,`Narration`,`Amount`,`Series`,`ReceievedBy`,`Trans_Type`,`T_Day`,`T_Month`,`T_Year`,`MemberID`,`Trans_Medium`,`RootID`,`ChitGroupId`) VALUES (1," + DualTransactionKey + "," + balayer.ToobjectstrEvenNull(Session["Branchid"]) + ",'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," + txtReceiptNo.Text + ",'C'," + ChitsBranchID + ",'" + dtChoosenDate.ToString("yyyy-MM-dd") + "','" + "Ref No:" + RowddlMisc.SelectedValue + " |Recd From:" + RowddlMemberName.SelectedItem.Text + " For " + RowddlMisc.SelectedItem.Text + "  Received By: " + balayer.ToobjectstrEvenNull(Session["BranchName"]) + "'," + RowtxtMisc.Text + ",'" + ddlReceiptSeries.SelectedItem.Text + "','" + balayer.ToobjectstrEvenNull(Session["UserName"]) + "',1," + dtChoosenDate.Day + "," + dtChoosenDate.Month + "," + dtChoosenDate.Year + "," + MemberID + "," + trans_medium + ",1," + RowddlGroupNo.SelectedValue + ") ";
                            string strCashmISCHeadQuery = "INSERT INTO `svcf`.`voucher` (`Other_Trans_Type`,`DualTransactionKey`,`BranchID`,`CurrDate`,`Voucher_No`,`Voucher_Type`,`Head_Id`,`ChoosenDate`,`Narration`,`Amount`,`Series`,`ReceievedBy`,`Trans_Type`,`T_Day`,`T_Month`,`T_Year`,`MemberID`,`Trans_Medium`,`RootID`,`ChitGroupId`) VALUES (1," + DualTransactionKey + "," + balayer.ToobjectstrEvenNull(Session["Branchid"]) + ",'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," + txtReceiptNo.Text + ",'D'," + CashOrBankID + ",'" + dtChoosenDate.ToString("yyyy-MM-dd") + "','" + "Ref No:" + RowddlMisc.SelectedValue + " |Recd From:" + RowddlMemberName.SelectedItem.Text + " For " + RowddlMisc.SelectedItem.Text + "  Received By: " + balayer.ToobjectstrEvenNull(Session["BranchName"]) + "'," + RowtxtMisc.Text + ",'" + ddlReceiptSeries.SelectedItem.Text + "','" + balayer.ToobjectstrEvenNull(Session["UserName"]) + "',1," + dtChoosenDate.Day + "," + dtChoosenDate.Month + "," + dtChoosenDate.Year + "," + MemberID + "," + trans_medium + "," + RootID + "," + RowddlGroupNo.SelectedValue + ") ";
                            //balayer.GetInsertItem(strChitmISCHeadQuery);
                            //balayer.GetInsertItem(strCashmISCHeadQuery);
                            long strChitMiscHead = trn.insertorupdateTrn(strChitmISCHeadQuery);
                            long strCashHeadMisc = trn.insertorupdateTrn(strCashmISCHeadQuery);
                            if (CheckBox1.Checked == true)
                            {
                                string BankName = "";
                                //string TransactionKey = balayer.GetSingleValue("SELECT max(TransactionKey)  FROM `svcf`.`voucher` where uuid_from_bin(DualTransactionKey)=uuid_from_bin(" + DualTransactionKey + ") and Head_Id=" + RowddlMisc.SelectedValue + ";");
                                string strBankMiscInsertQuery = "insert into transbank(BranchID, TransactionKey, DualTransactionKey, T_Day, T_Month, T_Year, BankHeadID, Head_Id, MemberID, CustomersBankName, DateInCheque, ChequeDDNO, GivenAmount, TotalChequeDDAmount, IsBounced, Trans_Type) values (" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + "," + strChitMiscHead + "," + DualTransactionKey + "," + dtChoosenDate.Day + "," + dtChoosenDate.Month + "," + dtChoosenDate.Year + "," + ddlBankHead.SelectedValue + "," + RowddlMisc.SelectedValue + "," + MemberID + ",'" + txtBankLocation.Text + "','" + balayer.indiandateToMysqlDate(txtDate_in_Cheque.Text) + "'," + txtIfsc.Text + "," + RowtxtMisc.Text + "," + txtTotalAmount.Text + ",0,1)";
                                trn.insertorupdateTrn(strBankMiscInsertQuery);
                            }
                        }
                    }
                    if (CheckBox1.Checked == true)
                    {
                        string BankName = "";
                        string strBankInsertQuery = "insert into transbank(BranchID, TransactionKey, DualTransactionKey, T_Day, T_Month, T_Year, BankHeadID, Head_Id, MemberID, CustomersBankName, DateInCheque, ChequeDDNO, GivenAmount, TotalChequeDDAmount, IsBounced, Trans_Type) values (" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + "," + TransactionKeyDue + "," + DualTransactionKey + "," + dtChoosenDate.Day + "," + dtChoosenDate.Month + "," + dtChoosenDate.Year + "," + ddlBankHead.SelectedValue + "," + TokenNo + "," + MemberID + ",'" + txtBankLocation.Text + "','" + balayer.indiandateToMysqlDate(txtDate_in_Cheque.Text) + "'," + txtIfsc.Text + "," + RowtxtAmount.Text + "," + txtTotalAmount.Text + ",0,1)";
                        trn.insertorupdateTrn(strBankInsertQuery);
                    }
                }
                if (ddlColloctorName.ToolTip.ToString().Trim() != "")
                {
                    //put  the  query to check all the numbers used or not 
                    trn.insertorupdateTrn("update svcf.assignreceiptbook set IsFinished=1 where receiptseries='" + ddlReceiptSeries.SelectedItem.Text + "' and receiptnoto in(" + ddlColloctorName.ToolTip.ToString().Trim() + ") and BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]));
                }
                ModalPopupExtender1.PopupControlID = "pnlmsg";
                BtnOK.Focus();
                ModalPopupExtender1.Show();
                BtnOK.Focus();
                pnlmsg.Visible = true;
                lblh.Text = "Status";
                lblcon.Text = "Your Transaction Processed Successfully!!!";
                lblcon.ForeColor = System.Drawing.Color.Green;
                trn.CommitTrn();

                logger.Info("New_Receipt_Voucher.aspx - btnConfirmationYes_click():  Completed: " + DateTime.Now + " by: " + Convert.ToString(Session["UserName"]) + "");
            }
            catch (Exception ex)
            {
                try
                {
                    trn.RollbackTrn();
                    logger.Info("New_Receipt_Voucher.aspx - btnConfirmationYes_click():  Error: " + ex.Message + ": " + DateTime.Now + " by: " + Convert.ToString(Session["UserName"]) + "");
                }
                catch (Exception error)
                { }
                finally
                {
                    //balayer.GetInsertItem("delete  FROM `svcf`.`voucher` where DualTransactionKey=" + DualTransactionKey + "");
                    //balayer.GetInsertItem("delete  FROM `svcf`.`transbank` where DualTransactionKey=" + DualTransactionKey + "");
                    ModalPopupExtender1.PopupControlID = "Pnlgendrate";
                    ModalPopupExtender1.Show();
                    Pnlgendrate.Visible = true;
                    lblHD.Text = "Status";
                    lblContent.Text = "Problem with Your Transaction Please Contact Administrator!!!";
                    lblContent.ForeColor = System.Drawing.Color.Red;
                }
            }
            finally
            {
                trn.DisposeTrn();
            }
            //ModalPopupExtender1.Hide();
            pnlConfirmation.Visible = false;
        }
        protected void btnGenerate_Click(object sender, EventArgs e)
        {
            Page.Validate("a");
            Page.Validate("GrpRow");
            
            //validation start
            if (CheckBox1.Checked == true)
            {
               
                txtIfsc.Visible = true;
                txtDate_in_Cheque.Visible = true;
                txtBankLocation.Visible = true;
                lblIFSC.Visible = true;
                lblDate.Visible = true;
                lblBL.Visible = true;
                txtIfsc.Focus();
                RFVtxtIfsc.Visible = true;
                RFVtxtBankLocation.Visible = true;
                RFVtxtDate_in_Cheque.Visible = true;
                ddlBankHead.Visible = true;
                CVddlBankHead.Visible = true;
                lblBankHead.Visible = true;
                lblBankHead.Visible = true;
                Page.Validate("b");
            }
            else
            {
               
                lblBankHead.Visible = false;
                RFVtxtIfsc.Visible = false;
                RFVtxtBankLocation.Visible = false;
                RFVtxtDate_in_Cheque.Visible = false;
                txtIfsc.Visible = false;
                txtDate_in_Cheque.Visible = false;
                txtBankLocation.Visible = false;
                lblIFSC.Visible = false;
                lblDate.Visible = false;
                lblBL.Visible = false;
                ddlBankHead.Visible = false;
                CVddlBankHead.Visible = false;
                lblBankHead.Visible = false;
            }
            if (!Page.IsValid)
            {
                return;
            }
            if (Session["CheckRefresh"].ToString() != ViewState["CheckRefresh"].ToString())
            {
                return;
            }
            if (Page.IsValid == true)
            {
                decimal dblTotalAmount = decimal.Parse(txtTotalAmount.Text);
                decimal dblDueAmount = 0.0M;
                bool isMiscIssue = false;
                foreach (GridViewRow gvRow in GridView1.Rows)
                {
                    decimal dblDueTemp = decimal.Parse(((TextBox)gvRow.FindControl("txtAmount")).Text);
                    decimal dblMiscTemp = 0.0M;
                    bool isMisc = decimal.TryParse(((TextBox)gvRow.FindControl("txtMisc")).Text, out dblMiscTemp);
                    dblDueAmount += dblDueTemp + dblMiscTemp;
                    DropDownList ddlMisc = ((DropDownList)gvRow.FindControl("ddlMisc"));
                    if (isMisc == true & ddlMisc.SelectedIndex <= 0)
                    {
                        isMiscIssue = true;
                    }
                    else if (isMisc == false & ddlMisc.SelectedIndex != -1)
                    {
                        isMiscIssue = true;
                    }
                }
                if (dblTotalAmount != dblDueAmount)
                {
                    ModalPopupExtender1.PopupControlID = "Pnlgendrate";
                    ModalPopupExtender1.Show();
                    Pnlgendrate.Visible = true;
                    lblHD.Text = "Status";
                    if (isMiscIssue == false)
                    {
                        lblContent.Text = "Total Amount Not Tally With Due Amount and Misc Amount!!!";
                    }
                    else
                    {
                        lblContent.Text = "Total Amount Not Tally With Due Amount and Misc Amount!!!<br><br>Please  Check Misc Area!!!";
                    }
                    lblContent.ForeColor = System.Drawing.Color.Red;
                    return;
                }
                string finishedReceiptNo = "";
                string strErrorMessage = "";
                string strExistMessage = "";
                DataTable dtAll = balayer.GetDataTable("SELECT  alreadyusedreceipts,receiptnoto   FROM svcf.assignreceiptbook where  moneycollid=" + ddlColloctorName.SelectedValue + "  and IsFinished=0 and BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and receiptseries='" + ddlReceiptSeries.SelectedItem.Text + "'");
                for (int i = 0; i < GridView1.Rows.Count; i++)
                {
                    TextBox txtReceiptNo = (TextBox)GridView1.Rows[i].FindControl("txtReceiptNo");
                    for (int j = 0; j < dtAll.Rows.Count; j++)
                    {
                        int ReceiptNo = int.Parse(txtReceiptNo.Text);
                        int FromRange = int.Parse(dtAll.Rows[j][0].ToString());
                        int toRange = int.Parse(dtAll.Rows[j][1].ToString());
                        if (ReceiptNo >= FromRange & ReceiptNo <= toRange)
                        {
                            if (0 != int.Parse(balayer.GetSingleValue("select ifnull(Count(*),0) from voucher where BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and  Voucher_No=" + ReceiptNo + " and Series='" + ddlReceiptSeries.SelectedItem.Text + "'")))
                            {
                                if (strExistMessage == "")
                                {
                                    strExistMessage = "Following ReceiptNo Already Exist In Series " + ddlReceiptSeries.SelectedItem.Text + " :<br><br>" + ReceiptNo;
                                }
                                else
                                {
                                    strExistMessage += "<br>" + ReceiptNo;
                                }
                                
                            }
                            if (ReceiptNo == toRange)
                            {
                                finishedReceiptNo = ReceiptNo + ",";
                            }
                            break;
                        }
                        else
                        {
                            if (strErrorMessage == "")
                            {
                                strErrorMessage = "Following ReceiptNo Not Resides inside The Allocated Range In Series " + ddlReceiptSeries.SelectedItem.Text + ":<br><br> " + ReceiptNo;
                            }
                            else
                            {
                                strErrorMessage += "<br>" + ReceiptNo;
                            }
                            //error message 
                        }
                    }
                }
                ddlColloctorName.ToolTip = finishedReceiptNo.Trim().Trim(',');
                if (strErrorMessage.Trim() != "" || strExistMessage.Trim() != "")
                {
                    ModalPopupExtender1.PopupControlID = "Pnlgendrate";
                    ModalPopupExtender1.Show();
                    Pnlgendrate.Visible = true;
                    lblHD.Text = "Status";
                    string finalError = "";
                    if (strErrorMessage.Trim() != "")
                    {
                        finalError += strErrorMessage + "<br><br>";
                    }
                    if (strExistMessage.Trim() != "")
                    {
                        finalError += strExistMessage + "<br>";
                    }
                    lblContent.Text = finalError;
                    lblContent.ForeColor = System.Drawing.Color.Red;
                    return;
                }
                //validation end
                DataTable dtConfirmation = new DataTable();
                dtConfirmation.Columns.Add("Member Name");
                dtConfirmation.Columns.Add("Amount Paying");
                dtConfirmation.Columns.Add("Draw Details");
                for (int i = 0; i < GridView1.Rows.Count; i++)
                {
                    try
                    {
                        dtConfirmation.Rows.Add();
                        DropDownList RowddlGroupNo = (DropDownList)GridView1.Rows[i].FindControl("ddlGroupNo");
                        DropDownList RowddlMemberName = (DropDownList)GridView1.Rows[i].FindControl("ddlMemberName");
                        TextBox RowtxtAmount = (TextBox)GridView1.Rows[i].FindControl("txtAmount");
                        DropDownList RowddlMisc = (DropDownList)GridView1.Rows[i].FindControl("ddlMisc");
                        TextBox RowtxtMisc = (TextBox)GridView1.Rows[i].FindControl("txtMisc");
                        decimal dblMiscTemp = 0.0M;
                        bool isMisc = decimal.TryParse(RowtxtMisc.Text, out dblMiscTemp);
                        //new change
                        if ((isMisc == true & dblMiscTemp > 0.0M))
                        {
                            isMisc = true;
                        }
                        else
                        {
                            isMisc = false;
                        }
                        //new change
                        dtConfirmation.Rows[i]["Member Name"] = RowddlMemberName.SelectedItem.Text;
                        dtConfirmation.Rows[i]["Amount Paying"] = RowtxtAmount.Text;
                        decimal TotalPaidAmount = decimal.Parse(balayer.GetSingleValue("SELECT  ifnull( (sum(IF(Voucher_Type = 'C',Amount,0.00)) -sum(IF(Voucher_Type = 'D',Amount,0.00)) ),0.00) AS TransactionAmount FROM `svcf`.`voucher` where  Head_Id=" + RowddlMemberName.SelectedValue.Split('|')[0] + " and  (Trans_Type<>2) and Other_Trans_Type<>5"));
                        decimal AddTotalPaidAmount = TotalPaidAmount;
                        string FromNarration = "";
                        string ToNarration = "";
                        int FromDraw = 0;
                        int ToDraw = 0;
                        if (TotalPaidAmount == 0.00M)
                        {
                            FromNarration = "1";
                            FromDraw = 1;
                            TotalPaidAmount = TotalPaidAmount + decimal.Parse(RowtxtAmount.Text);
                            DataTable dtAuction = balayer.GetDataTable("SELECT DrawNO,CurrentDueAmount FROM `svcf`.`auctiondetails` where GroupID=" + RowddlGroupNo.SelectedValue + " and CurrentDueAmount<>'0.00' order by DrawNO");
                            for (int iAuc = 0; iAuc < dtAuction.Rows.Count; iAuc++)
                            {
                                decimal currentDueAmount = decimal.Parse(dtAuction.Rows[iAuc]["CurrentDueAmount"].ToString());
                                TotalPaidAmount = TotalPaidAmount - currentDueAmount;
                                decimal tempDueAmount = TotalPaidAmount;
                                if (tempDueAmount == 0.00M)
                                {
                                    ToNarration = (iAuc + 1).ToString();
                                    ToDraw = iAuc + 1;
                                    break;
                                }
                                else if (tempDueAmount < 0.00M)
                                {
                                    ToDraw = iAuc + 1;
                                    ToNarration = iAuc + 1 + "Part Payment";
                                    break;
                                }
                            }
                            if (ToNarration == "")
                            {
                                FromNarration += " To " + (int.Parse(dtAuction.Rows[dtAuction.Rows.Count - 1]["DrawNO"].ToString()) + 1).ToString() + "+ Excess Payment";
                            }
                            if (FromDraw != ToDraw)
                            {
                                FromNarration += " To" + ToNarration;
                            }
                            RowddlMemberName.ToolTip = FromNarration;
                            //dtConfirmation.Rows[i]["Misc Head"] = RowddlMemberName.ToolTip.ToString();
                        }
                        else
                        {
                            DataTable dtAuction = balayer.GetDataTable("SELECT DrawNO,CurrentDueAmount FROM `svcf`.`auctiondetails` where GroupID=" + RowddlGroupNo.SelectedValue + " and CurrentDueAmount<>'0.00' order by DrawNO");
                            TotalPaidAmount = AddTotalPaidAmount;
                            for (int iAuc = 0; iAuc < dtAuction.Rows.Count; iAuc++)
                            {
                                decimal currentDueAmount = decimal.Parse(dtAuction.Rows[iAuc]["CurrentDueAmount"].ToString());
                                TotalPaidAmount = TotalPaidAmount - currentDueAmount;
                                decimal tempDueAmount = TotalPaidAmount;
                                if (tempDueAmount == 0.00M)
                                {
                                    FromNarration = (iAuc + 2).ToString();
                                    FromDraw = iAuc + 2;
                                    break;
                                }
                                else if (tempDueAmount < 0.00M)
                                {
                                    FromNarration = iAuc + 1 + "Part Payment";
                                    FromDraw = iAuc + 1;
                                    break;
                                }
                            }
                            if (FromNarration == "")
                            {
                                FromNarration = (int.Parse(dtAuction.Rows[dtAuction.Rows.Count - 1]["DrawNO"].ToString()) + 1).ToString() + "+ Excess Payment";
                            }
                            else
                            {
                                TotalPaidAmount = AddTotalPaidAmount;
                                TotalPaidAmount = TotalPaidAmount + decimal.Parse(RowtxtAmount.Text);
                                for (int iAuc = 0; iAuc < dtAuction.Rows.Count; iAuc++)
                                {
                                    decimal currentDueAmount = decimal.Parse(dtAuction.Rows[iAuc]["CurrentDueAmount"].ToString());
                                    TotalPaidAmount = TotalPaidAmount - currentDueAmount;
                                    decimal tempDueAmount = TotalPaidAmount;
                                    if (tempDueAmount == 0.00M)
                                    {
                                        ToNarration = (iAuc + 1).ToString();
                                        ToDraw = iAuc + 1;
                                        break;
                                    }
                                    else if (tempDueAmount < 0.00M)
                                    {
                                        ToDraw = iAuc + 1;
                                        ToNarration = iAuc + 1 + "Part Payment";
                                        break;
                                    }
                                }
                                if (ToNarration == "")
                                {
                                    ToNarration = "+ Excess Payment";
                                }
                            }
                            if (ToNarration != "")
                            {
                                if (FromDraw != ToDraw)
                                {
                                    RowddlMemberName.ToolTip = FromNarration + " To " + ToNarration;
                                }
                                else
                                {
                                    RowddlMemberName.ToolTip = ToNarration
                                        ;
                                }
                            }
                            else
                            {
                                RowddlMemberName.ToolTip = FromNarration;
                            }
                        }
                        dtConfirmation.Rows[i]["Draw Details"] = RowddlMemberName.ToolTip.ToString();
                        if (isMisc == true)
                        {
                            if (!dtConfirmation.Columns.Contains("Misc Head"))
                            {
                                dtConfirmation.Columns.Add("Misc Head");
                                dtConfirmation.Columns.Add("Misc Amount");
                            }
                            dtConfirmation.Rows[i]["Misc Head"] = RowddlMisc.SelectedItem.Text;
                            dtConfirmation.Rows[i]["Misc Amount"] = RowtxtMisc.Text;
                        }
                    }
                    catch
                    {
                    }
                    finally
                    {
                    }
                }
                gvConfirm.DataSource = dtConfirmation;
                gvConfirm.DataBind();
                lblHeadingConfirmation.Text = "Confirmation";
                ModalPopupExtender1.PopupControlID = "pnlConfirmation";
                ModalPopupExtender1.Show();
                pnlConfirmation.Visible = true;
                Button1.Focus();
            }
        }
        protected void btnyes_Click(object sender, EventArgs e)
        {
            ModalPopupExtender1.Hide();
        }
        protected void btnNo_Click(object sender, EventArgs e)
        {
            ModalPopupExtender1.Hide();
        }
        protected void btnOK_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.Url.AbsolutePath);
           
        }
        protected void ResetPage()
        {
            Response.Redirect(Request.Url.AbsolutePath);
          
        }
    }
}
