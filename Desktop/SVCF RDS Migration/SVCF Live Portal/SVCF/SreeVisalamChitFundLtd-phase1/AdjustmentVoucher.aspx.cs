using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SVCF_BusinessAccessLayer;
using SVCF_TransactionLayer;
using SVCF_DataAccessLayer;
using log4net;
using log4net.Config;

namespace SreeVisalamChitFundLtd_phase1
{
    public partial class AdjustmentVoucher : System.Web.UI.Page
    {
        //#region VarDeclaration
        //CommonClassFile objcls = new CommonClassFile();
        //#endregion

        #region VarDeclaration
        BusinessLayer balayer = new BusinessLayer();
        TransactionLayer tranlayer = new TransactionLayer();
        #endregion

        ILog logger = log4net.LogManager.GetLogger(typeof(AdjustmentVoucher));

        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (Session["UserName"] == null || Session["Branchid"] == null || Session["BranchName"] == null)
            {
                Response.Redirect(Page.ResolveUrl("~/Login.aspx"), false);
            }
        }
        protected void Page_Init(object sender, EventArgs e)
        {
            img16List.ImageUrl = Page.ResolveUrl("~/pertho_admin_v1.3/img/ico/icSw2/16-List.png");
            img16List1.ImageUrl = Page.ResolveUrl("~/pertho_admin_v1.3/img/ico/icSw2/16-List.png");
            Image img = (Image)UpdateProgress1.FindControl("imgWaiting");
            img.ImageUrl = Page.ResolveUrl("~/Styles/Image/waiting.gif");
        }
        protected void Page_PreRender(object sender, EventArgs e)
        {
            ViewState["CheckRefresh"] = Session["CheckRefresh"];
        }
        protected void Ib_Load(object sender, EventArgs e)
        {
            ImageButton btn = (ImageButton)sender;
            this.scm1.RegisterAsyncPostBackControl(btn);
            // UpdatePanel1.Triggers.Add(new AsyncPostBackTrigger { ControlID = btn.UniqueID, EventName = "click" });
        }
        private void SetGridTabIndex(bool isCredit)
        {
            short currentTabIndex = 0;
            if (isCredit == true)
            {
                currentTabIndex = 7;
                GridView theGridView = GridGuardians;
                foreach (GridViewRow dr in theGridView.Rows)
                {
                    ImageButton showbutton = (ImageButton)dr.FindControl("imgBtnAdd");
                    ImageButton showRemovebutton = (ImageButton)dr.FindControl("imgBtnRemove");
                    DropDownList ddlHeads = (DropDownList)dr.FindControl("ddlHeads");
                    TextBox txtAmount = (TextBox)dr.FindControl("txtAmount");
                    TextBox txtDescription = (TextBox)dr.FindControl("txtDescription");
                    TextBox txtChequeNO = (TextBox)dr.FindControl("txtChequeNO");
                    DropDownList ddlCRBranchName = (DropDownList)dr.FindControl("ddlCRBranch");
                    Label lblCrBranchId = (Label)dr.FindControl("lblCrBranchId");
                    ddlHeads.TabIndex = ++currentTabIndex;
                    txtAmount.TabIndex = ++currentTabIndex;
                    txtDescription.TabIndex = ++currentTabIndex;
                    txtChequeNO.TabIndex = ++currentTabIndex;
                    ddlCRBranchName.TabIndex = ++currentTabIndex;                                 
                }
            }
            else
            {
                GridView theGridView = GridGuardiansDebit;
                currentTabIndex = (short)((GridGuardians.Rows.Count * 4) + 9);
                foreach (GridViewRow dr in theGridView.Rows)
                {
                    //DataRow dtrow = dt1.NewRow();
                    DropDownList ddlHeads = (DropDownList)dr.FindControl("ddlHeadsDebit");
                    TextBox txtAmount = (TextBox)dr.FindControl("txtAmountDebit");
                    TextBox txtDescription = (TextBox)dr.FindControl("txtDescriptionDebit");
                    ImageButton showbutton = (ImageButton)dr.FindControl("imgBtnAddDebit");
                    ImageButton showRemovebutton = (ImageButton)dr.FindControl("imgBtnRemoveDebit");
                    DropDownList dbDB_BranchName = (DropDownList)dr.FindControl("ddlDBBranch");                  

                    ddlHeads.TabIndex = ++currentTabIndex;
                    txtAmount.TabIndex = ++currentTabIndex;
                    txtDescription.TabIndex = ++currentTabIndex;
                    dbDB_BranchName.TabIndex = ++currentTabIndex;                                  
                }
                btnGenerate.TabIndex = ++currentTabIndex;
                //after
            }
        }
        protected void bindHeads(DropDownList ddlHeads)
        {
            
            DataTable dtAllHeads = balayer.GetDataTable("SELECT concat(cast(  v1.`RootID` as char),',',cast(v1.`TreeID` as char)) as ID,v1.`TREE` FROM `svcf`.`view_parent` as v1 where v1.BranchID is null or v1.BranchID=" + Session["Branchid"]);
            DataRow dr = dtAllHeads.NewRow();
            dr[0] = "--Select--";
            dr[1] = "--Select--";
            dtAllHeads.Rows.InsertAt(dr, 0);
            ddlHeads.DataSource = dtAllHeads;
            ddlHeads.DataTextField = "TREE";
            ddlHeads.DataValueField = "ID";
            ddlHeads.DataBind();
        }
        protected void bindHeadsDebit(DropDownList ddlHeads)
        {
            
            DataTable dtAllHeads = balayer.GetDataTable("SELECT concat(cast(  v1.`RootID` as char),',',cast(v1.`TreeID` as char)) as ID,v1.`TREE` FROM `svcf`.`view_parent` as v1 where v1.BranchID is null or v1.BranchID=" + Session["Branchid"]);
          
            DataRow dr = dtAllHeads.NewRow();
            dr[0] = "--Select--";
            dr[1] = "--Select--";
            dtAllHeads.Rows.InsertAt(dr, 0);
            ddlHeads.DataSource = dtAllHeads;
            ddlHeads.DataTextField = "TREE";
            ddlHeads.DataValueField = "ID";
            ddlHeads.DataBind();
        }

        protected Boolean IsCheque(string Voucher_Type)
        {
            return Voucher_Type.ToUpper().Equals("B");
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            pnlpopup.Visible = false;
            if (!IsPostBack)
            {

                var userinfo = HttpContext.Current.User.Identity.Name;
                var qry = "select rs.name from roles as rs inner join rights as rt on (rt.roleid=rs.id) where memberid=" + userinfo + "";
                var usrRole = balayer.GetSingleValue(qry);
                if (usrRole == "Report")
                {
                    Response.Redirect("Home.aspx", false);
                }

                Session["CheckRefresh"] = System.Guid.NewGuid().ToString();
                txtReceivedBy.Text = balayer.ToobjectstrEvenNull(Session["UserName"]);
                ViewState["tabnum"] = "57";
                // hiddenAccordionIndex.Value = "0";
                this.BindData();
                SetDefaultRow();
                this.BindDataDebit();
                SetDefaultRowDebit();
                txtDate.Focus();
                txtDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtSeries.Text = "VOUCHER";
                GetSeriesAndVoucherNo();
                SetGridTabIndex(true);
                SetGridTabIndex(false);            
            }
            rvDate.MaximumValue = DateTime.Now.ToString("dd/MM/yyyy");
            rvDate.MinimumValue = balayer.GetSingleValue("SELECT DATE_FORMAT( min(MinimumDate), '%d/%m/%Y') MinimumDate FROM svcf.restrictionmaster where BranchID=" + Session["Branchid"]); 
        }
        protected void txtAmount_TextChanged(object sender, EventArgs e)
        {
        }
        protected void GridGuardians_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DropDownList ddlHeads = (DropDownList)e.Row.FindControl("ddlHeads");
                DropDownList _ddlCrBrnch = (DropDownList)e.Row.FindControl("ddlCRBranch");
                if (Convert.ToInt32(Session["Branchid"]) != 161)
                {
                    Label lblCreditTitle = (Label)e.Row.FindControl("Label6");
                    _ddlCrBrnch.Visible = false;
                    lblCreditTitle.Visible = false;
                }
                else
                {
                    Label lblCreditTitle = (Label)e.Row.FindControl("Label6");
                    _ddlCrBrnch.Visible = true;
                    lblCreditTitle.Visible = true;
                }
                if (e.Row.RowIndex > 0)
                {
                    DataTable dtAllHeads = (DataTable)((DropDownList)GridGuardians.Rows[0].FindControl("ddlHeads")).DataSource;
                    ddlHeads.DataSource = dtAllHeads;
                    ddlHeads.DataTextField = "TREE";
                    ddlHeads.DataValueField = "ID";
                    ddlHeads.DataBind();
                }
                else
                {
                    bindHeads(ddlHeads);
                }
                ddlHeads.Items.FindByText(((Label)e.Row.FindControl("lblHeads")).Text).Selected = true;
                if (((Label)e.Row.FindControl("lblCRBranch")).Text != "")
                {
                    _ddlCrBrnch.Items.FindByValue(((Label)e.Row.FindControl("lblCRBranch")).Text).Selected = true;
                }
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                ImageButton imgBtnRemove = (e.Row.FindControl("imgBtnRemove") as ImageButton);
                scm1.RegisterAsyncPostBackControl(imgBtnRemove);
            }
        }
        protected void GridGuardiansDebit_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DropDownList ddlDBHeads = (DropDownList)e.Row.FindControl("ddlHeadsDebit");
                DropDownList _ddlDBBrnch = (DropDownList)e.Row.FindControl("ddlDBBranch");
                if (Convert.ToInt32(Session["Branchid"]) != 161)
                {
                    Label lblDebitTitle = (Label)e.Row.FindControl("Label7");
                    _ddlDBBrnch.Visible = false;
                    lblDebitTitle.Visible = false;
                }
                else
                {
                    Label lblDebitTitle = (Label)e.Row.FindControl("Label7");
                    _ddlDBBrnch.Visible = true;
                    lblDebitTitle.Visible = true;
                }
                if (e.Row.RowIndex > 0)
                {
                    DataTable dtAllHeads = (DataTable)((DropDownList)GridGuardiansDebit.Rows[0].FindControl("ddlHeadsDebit")).DataSource;
                    ddlDBHeads.DataSource = dtAllHeads;
                    ddlDBHeads.DataTextField = "TREE";
                    ddlDBHeads.DataValueField = "ID";
                    ddlDBHeads.DataBind();
                }
                else
                {
                    bindHeadsDebit(ddlDBHeads);
                }
                ddlDBHeads.Items.FindByText(((Label)e.Row.FindControl("lblDBHeads")).Text).Selected = true;
                if (((Label)e.Row.FindControl("lblDRBranch")).Text != "")
                {
                    _ddlDBBrnch.Items.FindByValue(((Label)e.Row.FindControl("lblDRBranch")).Text).Selected = true;
                }

            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                ImageButton imgBtnAddDebit = (e.Row.FindControl("imgBtnAddDebit") as ImageButton);
                ImageButton imgBtnRemoveDebit = (e.Row.FindControl("imgBtnRemoveDebit") as ImageButton);
                //scm1.RegisterAsyncPostBackControl(imgBtnAddDebit);
                scm1.RegisterAsyncPostBackControl(imgBtnRemoveDebit);
            }
        }
        protected void GridGuardiansDebit_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }
        protected void ddlBothGridHead1_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddlDeb = ((DropDownList)sender);
            if (ddlDeb.SelectedValue.ToString().Contains("5,"))
            {
                TextBox txtDescriptionDebit = (TextBox)(((DropDownList)sender).Parent.Parent.FindControl("txtDescriptionDebit"));
                string strMemberName = balayer.GetSingleValue("SELECT MemberName FROM svcf.membertogroupmaster where Head_Id=" + ddlDeb.SelectedItem.Value.Split(',')[1]);
                txtDescriptionDebit.Text = strMemberName;
            }
            TextBox txtAmount = ((TextBox)(((DropDownList)sender).Parent.Parent.FindControl("txtAmountDebit")));
            txtAmount.Focus();
        }
        public bool DecideVisibility(object xcv)
        {
            if (balayer.ToobjectstrEvenNull(xcv).Trim() != "")
            {
                return true;

            }
            else
            {
                return false;
            }
        }
        protected void ddlBothGridHead_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddlCred = ((DropDownList)sender);
            if (ddlCred.SelectedValue.ToString().Contains("3,"))
            {
                TextBox txtgrdChequeNo = (TextBox)(((DropDownList)sender).Parent.Parent.FindControl("txtChequeNO"));
                txtgrdChequeNo.Visible = true;
                Label lblgrdChequeNo = (Label)(((DropDownList)sender).Parent.Parent.FindControl("lblChequeNO"));
                lblgrdChequeNo.Visible = true;
                RequiredFieldValidator reqChequeNO = (RequiredFieldValidator)(((DropDownList)sender).Parent.Parent.FindControl("reqChequeNO"));
                reqChequeNO.Visible = true;
                TextBox txtDescription = (TextBox)(((DropDownList)sender).Parent.Parent.FindControl("txtDescription"));
                txtDescription.Text = "";
            }
            else if (ddlCred.SelectedValue.ToString().Contains("1,"))
            {
                TextBox txtgrdChequeNo = (TextBox)(((DropDownList)sender).Parent.Parent.FindControl("txtDescription"));
                string strMemberName = balayer.GetSingleValue("SELECT MemberName FROM svcf.membertogroupmaster where Head_Id=" + ddlCred.SelectedItem.Value.Split(',')[1]);
                txtgrdChequeNo.Text = strMemberName;
                TextBox txtgrdChequeNo1 = (TextBox)(((DropDownList)sender).Parent.Parent.FindControl("txtChequeNO"));
                txtgrdChequeNo1.Visible = false;
                Label lblgrdChequeNo = (Label)(((DropDownList)sender).Parent.Parent.FindControl("lblChequeNO"));
                lblgrdChequeNo.Visible = false;
                RequiredFieldValidator reqChequeNO = (RequiredFieldValidator)(((DropDownList)sender).Parent.Parent.FindControl("reqChequeNO"));
                reqChequeNO.Visible = false;
            }
            else if (ddlCred.SelectedValue.ToString().Contains("5,"))
            {
                TextBox txtgrdChequeNo = (TextBox)(((DropDownList)sender).Parent.Parent.FindControl("txtDescription"));
                string strMemberName = balayer.GetSingleValue("SELECT MemberName FROM svcf.membertogroupmaster where Head_Id=" + ddlCred.SelectedItem.Value.Split(',')[1]);
                txtgrdChequeNo.Text = strMemberName;
                TextBox txtgrdChequeNo1 = (TextBox)(((DropDownList)sender).Parent.Parent.FindControl("txtChequeNO"));
                txtgrdChequeNo1.Visible = false;
                Label lblgrdChequeNo = (Label)(((DropDownList)sender).Parent.Parent.FindControl("lblChequeNO"));
                lblgrdChequeNo.Visible = false;
                RequiredFieldValidator reqChequeNO = (RequiredFieldValidator)(((DropDownList)sender).Parent.Parent.FindControl("reqChequeNO"));
                reqChequeNO.Visible = false;
            }
            else
            {
                TextBox txtgrdChequeNo = (TextBox)(((DropDownList)sender).Parent.Parent.FindControl("txtChequeNO"));
                txtgrdChequeNo.Visible = false;
                Label lblgrdChequeNo = (Label)(((DropDownList)sender).Parent.Parent.FindControl("lblChequeNO"));
                lblgrdChequeNo.Visible = false;
                RequiredFieldValidator reqChequeNO = (RequiredFieldValidator)(((DropDownList)sender).Parent.Parent.FindControl("reqChequeNO"));
                reqChequeNO.Visible = false;
                TextBox txtDescription = (TextBox)(((DropDownList)sender).Parent.Parent.FindControl("txtDescription"));
                txtDescription.Text = "";
            }
            TextBox txtAmount = (TextBox)(((DropDownList)sender).Parent.Parent.FindControl("txtAmount"));
            txtAmount.Focus();
        }


        protected void btnAdd_GridGuardiansDebit_RowCommand_click(object sender, ImageClickEventArgs e)
        {
            Page.Validate("GrpRowDebit");
            if (!Page.IsValid)
            {
                return;
            }
            GridViewRow gridRow = (GridViewRow)((ImageButton)sender).Parent.Parent;
            DataTable dtDeb = new DataTable();
            DataColumn dcHead = new DataColumn("Heads", typeof(string));
            DataColumn dcAmount = new DataColumn("Amount", typeof(string));
            DataColumn dcDescription = new DataColumn("Description", typeof(string));
            DataColumn dcBranchName = new DataColumn("Branch", typeof(string));     
            dtDeb.Columns.Add(dcHead);
            dtDeb.Columns.Add(dcDescription);
            dtDeb.Columns.Add(dcAmount);
            dtDeb.Columns.Add(dcBranchName);        
            foreach (GridViewRow dr in GridGuardiansDebit.Rows)
            {
                DataRow dtDbrow = dtDeb.NewRow();
                DropDownList ddlDbHeads = (DropDownList)dr.FindControl("ddlHeadsDebit");
                dtDbrow["Heads"] = ddlDbHeads.SelectedItem.Text;
                TextBox txtAmount = (TextBox)dr.FindControl("txtAmountDebit");
                dtDbrow["Amount"] = txtAmount.Text;
                TextBox txtDescription = (TextBox)dr.FindControl("txtDescriptionDebit");
                dtDbrow["Description"] = txtDescription.Text;
                DropDownList ddlC_rBranchName = (DropDownList)dr.FindControl("ddlDBBranch");
                dtDbrow["Branch"] = ddlC_rBranchName.Text; 
                dtDeb.Rows.Add(dtDbrow);                      
            }
            DataRow newRow = dtDeb.NewRow();
            newRow["Amount"] = "";
            newRow["Heads"] = "--Select--";
            dtDeb.Rows.Add(newRow);
            GridGuardiansDebit.DataSource = dtDeb;
            GridGuardiansDebit.DataBind();
            SetGridTabIndex(false);
        }
        protected void btnRemove_GridGuardiansDebit_RowCommand_click(object sender, ImageClickEventArgs e)
        {
            if (GridGuardiansDebit.Rows.Count == 1)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Warning", "alert('You Cant Delete!!!');", true);
                return;
            }
            DataTable dt1 = new DataTable();
            DataColumn ShowButton = new DataColumn("ShowAddButton", typeof(bool));
            DataColumn ShowRemoveButton = new DataColumn("ShowRemoveButton", typeof(bool));
            DataColumn dcHead = new DataColumn("Heads", typeof(string));
            DataColumn dcAmount = new DataColumn("Amount", typeof(string));
            DataColumn dcDescription = new DataColumn("Description", typeof(string));
            DataColumn dcBranchName = new DataColumn("Branch", typeof(string));   
            dt1.Columns.Add(dcDescription);
            dt1.Columns.Add(dcHead);
            dt1.Columns.Add(dcAmount);
            dt1.Columns.Add(dcBranchName);
            dt1.Columns.Add(ShowRemoveButton);
            dt1.Columns.Add(ShowButton);
            short i = 0;
            foreach (GridViewRow dr in GridGuardiansDebit.Rows)
            {
                i += 1;
                DataRow dtrow = dt1.NewRow();
                DropDownList ddlHeads = (DropDownList)dr.FindControl("ddlHeadsDebit");
                dtrow["Heads"] = ddlHeads.SelectedItem.Text;
                TextBox txtAmount = (TextBox)dr.FindControl("txtAmountDebit");
                dtrow["Amount"] = txtAmount.Text;
                TextBox txtDescription = (TextBox)dr.FindControl("txtDescriptionDebit");
                dtrow["Description"] = txtDescription.Text;
                DropDownList ddlC_rBranchName = (DropDownList)dr.FindControl("ddlDBBranch");
                dtrow["Branch"] = ddlC_rBranchName.Text; 
                dt1.Rows.Add(dtrow);
            }
            dt1.Rows.RemoveAt(GridGuardiansDebit.Rows.Count - 1);
            GridGuardiansDebit.DataSource = dt1;
            GridGuardiansDebit.DataBind();
            SetGridTabIndex(false);
        }
        protected void btnAdd_GridGuardians_RowCommand_click(object sender, ImageClickEventArgs e)
        {
            Page.Validate("GrpRow");
            if (!Page.IsValid)
            {
                return;
            }
            DropDownList ddlCred = (DropDownList)GridGuardians.Rows[0].FindControl("ddlHeads");
            if (balayer.GetSingleValue("SELECT TreeHint FROM `headstree` where NodeID='" + ddlCred.SelectedValue + "'").StartsWith("3,"))
            {
                return;
            }
            GridViewRow gridRow = (GridViewRow)(((ImageButton)sender).Parent.Parent);
            int RowIndex = gridRow.RowIndex;
            DataTable dt = new DataTable();
            DataColumn dcHead = new DataColumn("Heads", typeof(string));
            DataColumn dcAmount = new DataColumn("Amount", typeof(string));
            DataColumn dcDescription = new DataColumn("Description", typeof(string));
            DataColumn dcchequeNO = new DataColumn("ChequeNO", typeof(string));
            DataColumn dcBranchName = new DataColumn("Branch", typeof(string));          

            dt.Columns.Add(dcchequeNO);
            dt.Columns.Add(dcDescription);
            dt.Columns.Add(dcHead);
            dt.Columns.Add(dcAmount);
            dt.Columns.Add(dcBranchName);          

            foreach (GridViewRow dr in GridGuardians.Rows)
            {
                DataRow dtrow = dt.NewRow();
                DropDownList ddlHeads = (DropDownList)dr.FindControl("ddlHeads");
                dtrow["Heads"] = ddlHeads.SelectedItem.Text;
                TextBox txtAmount = (TextBox)dr.FindControl("txtAmount");
                dtrow["Amount"] = txtAmount.Text;
                TextBox txtDescription = (TextBox)dr.FindControl("txtDescription");
                dtrow["Description"] = txtDescription.Text;
                TextBox txtChequeNO = (TextBox)dr.FindControl("txtChequeNO");
                dtrow["ChequeNO"] = txtChequeNO.Text;
                DropDownList ddlCr_BranchName = (DropDownList)dr.FindControl("ddlCRBranch");
                dtrow["Branch"] = ddlCr_BranchName.Text;              
                dt.Rows.Add(dtrow);
            }
            DataRow newRow = dt.NewRow();
            newRow["Amount"] = "";
            newRow["Heads"] = "--Select--";
            dt.Rows.Add(newRow);
            GridGuardians.DataSource = dt;
            GridGuardians.DataBind();
            ViewState["tabnum"] = (57 + GridGuardians.Rows.Count * 4);
            SetGridTabIndex(true);
        }
        protected void btnRemove_GridGuardians_RowCommand_click(object sender, ImageClickEventArgs e)
        {
            if (GridGuardians.Rows.Count == 1)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Warning", "alert('You Cant Delete!!!');", true);
                return;
            }
            DataTable dt1 = new DataTable();
            DataColumn dcHead = new DataColumn("Heads", typeof(string));
            DataColumn dcAmount = new DataColumn("Amount", typeof(string));
            DataColumn dcDescription = new DataColumn("Description", typeof(string));
            DataColumn dcchequeNo = new DataColumn("ChequeNO", typeof(string));
            DataColumn dcBranchName = new DataColumn("Branch", typeof(string));     

            dt1.Columns.Add(dcDescription);
            dt1.Columns.Add(dcchequeNo);
            dt1.Columns.Add(dcHead);
            dt1.Columns.Add(dcAmount);
            dt1.Columns.Add(dcBranchName);
            short i = 0;
            foreach (GridViewRow dr in GridGuardians.Rows)
            {
                i += 1;
                DataRow dtrow = dt1.NewRow();
                DropDownList ddlHeads = (DropDownList)dr.FindControl("ddlHeads");
                dtrow["Heads"] = ddlHeads.SelectedItem.Text;
                TextBox txtAmount = (TextBox)dr.FindControl("txtAmount");
                dtrow["Amount"] = txtAmount.Text;
                TextBox txtDescription = (TextBox)dr.FindControl("txtDescription");
                dtrow["Description"] = txtDescription.Text;
                TextBox txtChequeNO = (TextBox)dr.FindControl("txtChequeNO");
                dtrow["ChequeNO"] = txtChequeNO.Text;
                DropDownList ddlCr_BranchName = (DropDownList)dr.FindControl("ddlCRBranch");
                dtrow["Branch"] = ddlCr_BranchName.Text;       
                ImageButton showbutton = (ImageButton)dr.FindControl("imgBtnAdd");
                dt1.Rows.Add(dtrow);
            }
            dt1.Rows.RemoveAt(GridGuardians.Rows.Count - 1);
            GridGuardians.DataSource = dt1;
            GridGuardians.DataBind();
            if (GridGuardians.Rows.Count > 0)
            {
            }
            SetGridTabIndex(true);
        }
        protected void GridGuardians_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }
        private void BindDataDebit()
        {
            DataTable dtSelect = new DataTable();
            DataTable dt = new DataTable();
            DataColumn dcHeads = new DataColumn("Heads", typeof(string));
            DataColumn dcAmount = new DataColumn("Amount", typeof(string));
            DataColumn dcDescription = new DataColumn("Description", typeof(string));
            DataColumn dcBranchName = new DataColumn("DbBranchName", typeof(string));
            DataColumn dcBranchId = new DataColumn("DbBranchId", typeof(string));
            dt.Columns.Add(dcDescription);
            dt.Columns.Add(dcAmount);
            dt.Columns.Add(dcHeads);
            dt.Columns.Add(dcBranchName);
            dt.Columns.Add(dcBranchId);

            GridGuardiansDebit.DataSource = dt;
            GridGuardiansDebit.DataBind();
        }
        private void BindData()
        {
            DataTable dtSelect = new DataTable();
            DataTable dt = new DataTable();
            DataColumn dcHeads = new DataColumn("Heads", typeof(string));
            DataColumn dcAmount = new DataColumn("Amount", typeof(string));
            DataColumn dcDescription = new DataColumn("Description", typeof(string));
            DataColumn dcchequeNO = new DataColumn("chequeNO", typeof(string));
            DataColumn dcBranchName = new DataColumn("Branch", typeof(string));
           
            dt.Columns.Add(dcDescription);
            dt.Columns.Add(dcchequeNO);
            dt.Columns.Add(dcAmount);
            dt.Columns.Add(dcHeads);
            dt.Columns.Add(dcBranchName);           
            GridGuardians.DataSource = dt;
            GridGuardians.DataBind();
        }
        public void SetDefaultRow()
        {
            DataTable dt = new DataTable();           
            DataColumn dcHeads = new DataColumn("Heads", typeof(string));
            DataColumn dcAmount = new DataColumn("Amount", typeof(string));
            DataColumn dcDescription = new DataColumn("Description", typeof(string));
            DataColumn dcchequeNO = new DataColumn("chequeNO", typeof(string));
            DataColumn dcBranchName = new DataColumn("Branch", typeof(string));
          
            dt.Columns.Add(dcchequeNO);
            dt.Columns.Add(dcDescription);
            dt.Columns.Add(dcAmount);
            dt.Columns.Add(dcHeads);
            dt.Columns.Add(dcBranchName);          
            DataRow newRow = dt.NewRow();
            newRow["Heads"] = "--Select--";
            newRow["Amount"] = "";
           
            dt.Rows.Add(newRow);
            GridGuardians.DataSource = dt;
            GridGuardians.DataBind();
        }
        public void SetDefaultRowDebit()
        {
            DataTable dt = new DataTable();            
            DataColumn dcHeads = new DataColumn("Heads", typeof(string));
            DataColumn dcAmount = new DataColumn("Amount", typeof(string));
            DataColumn dcDescription = new DataColumn("Description", typeof(string));
            DataColumn dcchequeNO = new DataColumn("chequeNO", typeof(string));
            DataColumn dcBranchName = new DataColumn("Branch", typeof(string));          

            dt.Columns.Add(dcDescription);
            dt.Columns.Add(dcAmount);
            dt.Columns.Add(dcHeads);
            dt.Columns.Add(dcBranchName);          
           
            dt.Columns.Add(dcchequeNO);
            DataRow newRow = dt.NewRow();
            newRow["Heads"] = "--Select--";
            newRow["Amount"] = "";           
            dt.Rows.Add(newRow);
            GridGuardiansDebit.DataSource = dt;
            GridGuardiansDebit.DataBind();
        }
        protected void btnYes_Click(object sender, EventArgs e)
        {
            TransactionLayer trn = new TransactionLayer();
            if (lblHint.Text == "Reload")
            {
                lblHint.Text = "";
                this.Response.Redirect(this.Request.Url.AbsolutePath.ToString(), false);
            }
            else
                if (lblHint.Text == "VExist")
                {
                    txtVoucherNo.Text = "";
                    txtSeries.Text = "";
                    txtSeries.Focus();
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
                            System.Guid guid = Guid.NewGuid();
                            // Prepare GUID values in SQL format
                            string hexstring = BitConverter.ToString(guid.ToByteArray());
                            string guidForBinary16 = "0x" + hexstring.Replace("-", string.Empty);
                            string DualTransactionKey = guidForBinary16;
                            bool isFailed = false;
                            DateTime dtChoosenDate = DateTime.Parse(txtDate.Text);
                            try
                            {

                                DateTime d;
                                bool isDate = DateTime.TryParseExact(txtDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out d);
                                string trans_medium = "";
                                string memberid = "";
                                for (int iTrans = 0; iTrans < GridGuardians.Rows.Count; iTrans++)
                                {
                                    DropDownList ddlSubsHeads = (DropDownList)GridGuardians.Rows[iTrans].FindControl("ddlHeads");
                                    int rootid = int.Parse(ddlSubsHeads.SelectedValue.ToString().Split(',')[0].ToString().Trim().Trim(',').Trim());
                                    if (rootid == 3)
                                    {
                                        trans_medium = "1";
                                        break;
                                    }
                                    else if (rootid == 12)
                                    {
                                        trans_medium = "0";
                                        break;
                                    }
                                    else if (rootid == 5)
                                    {
                                        for (int isub = 0; isub < GridGuardiansDebit.Rows.Count; isub++)
                                        {
                                            DropDownList ddlSubsHeadsisub = (DropDownList)GridGuardiansDebit.Rows[isub].FindControl("ddlHeadsDebit");
                                            int subrootid = int.Parse(ddlSubsHeadsisub.SelectedValue.ToString().Split(',')[0].ToString().Trim().Trim(',').Trim());
                                            if (subrootid == 1 || subrootid == 6)
                                            {
                                                trans_medium = "1";
                                                break;
                                            }
                                        }
                                        break;
                                    }
                                    else if (rootid == 1)
                                    {
                                        for (int isub = 0; isub < GridGuardiansDebit.Rows.Count; isub++)
                                        {
                                            DropDownList ddlSubsHeadsisub = (DropDownList)GridGuardiansDebit.Rows[isub].FindControl("ddlHeadsDebit");
                                            int subrootid = int.Parse(ddlSubsHeadsisub.SelectedValue.ToString().Split(',')[0].ToString().Trim().Trim(',').Trim());
                                            if (subrootid == 12)
                                            {
                                                trans_medium = "0";
                                                break;
                                            }
                                            else if (subrootid == 3)
                                            {
                                                trans_medium = "1";
                                                break;
                                            }
                                            //nithi
                                            //if branch credit as well as branch debit
                                            else if (subrootid == 1)
                                            {
                                                trans_medium = "1";
                                                break;
                                            }
                                            //---------
                                        }
                                        break;
                                    }
                                }
                                int othertranstype=0;
                                DropDownList ddlC = (DropDownList)GridGuardians.Rows[0].FindControl("ddlHeads");
                                DropDownList ddlD = (DropDownList)GridGuardiansDebit.Rows[0].FindControl("ddlHeadsDebit");
                                if (int.Parse(ddlC.SelectedValue.ToString().Split(',')[0].ToString().Trim().Trim(',').Trim()) ==1 || int.Parse(ddlD.SelectedValue.ToString().Split(',')[0].ToString().Trim().Trim(',').Trim())==1)
                                {
                                    othertranstype = 1;
                                }
                                if (trans_medium == "")
                                {
                                    for (int iTrans = 0; iTrans < GridGuardiansDebit.Rows.Count; iTrans++)
                                    {
                                        DropDownList ddlSubsHeads = (DropDownList)GridGuardiansDebit.Rows[iTrans].FindControl("ddlHeadsDebit");
                                        int rootid = int.Parse(ddlSubsHeads.SelectedValue.ToString().Split(',')[0].ToString().Trim().Trim(',').Trim());
                                        if (rootid == 3)
                                        {
                                            trans_medium = "1";
                                            break;
                                        }
                                        else if (rootid == 12)
                                        {
                                            trans_medium = "0";
                                            break;
                                        }
                                    }
                                }
                                if (trans_medium == "")
                                {
                                    trans_medium = "3";
                                }
                                int CRBranchid = 0;
                                int CrVoucherNo_Pallathur = 0;
                                string crVoucher_Series = "BRANCH";
                                int DBRBranchid = 0;
                                int DbrVoucherNo_Pallathur = 0;
                                string DbrVoucher_Series = "BRANCH";
                                int CR_SelectedBranchid = 0;
                                int DBR_SelectedBranchid = 0;
                                for (int iTrans = 0; iTrans < GridGuardians.Rows.Count; iTrans++)
                                {
                                    // trans_medium = "0";
                                    DropDownList ddlSubsHeads = (DropDownList)GridGuardians.Rows[iTrans].FindControl("ddlHeads");
                                    DropDownList ddlCrd_Branch = (DropDownList)GridGuardians.Rows[iTrans].FindControl("ddlCRBranch");
                                    //By nithi
                                    //---------------
                                    int rootid = int.Parse(ddlSubsHeads.SelectedValue.ToString().Split(',')[0].ToString().Trim().Trim(',').Trim());
                                    if (rootid == 5)
                                    {
                                        memberid = balayer.GetSingleValue("select `MemberID` from membertogroupmaster where `Head_Id`=" + ddlSubsHeads.SelectedValue.ToString().Split(',')[1].ToString().Trim().Trim(',').Trim());
                                    }
                                    else
                                    {
                                        memberid = "0";
                                    }
                                    if (memberid == "")
                                    {
                                        memberid = "0";
                                    }
                                    //----------------
                                    TextBox txtSubAmount = (TextBox)GridGuardians.Rows[iTrans].FindControl("txtAmount");
                                    TextBox txtDescription = (TextBox)GridGuardians.Rows[iTrans].FindControl("txtDescription");
                                    string NodeID = ddlSubsHeads.SelectedValue.ToString().Split(',')[1].Trim();
                                   
                                    TextBox txtChk = (TextBox)GridGuardians.Rows[iTrans].FindControl("txtChequeNO");
                                   
                                    string chitGroupId = "0";
                                    if (int.Parse(ddlSubsHeads.SelectedValue.ToString().Split(',')[0].ToString().Trim().Trim(',').Trim()) == 5)
                                    {
                                        chitGroupId = balayer.GetSingleValue("select ParentID from headstree where NodeID=" + ddlSubsHeads.SelectedValue.ToString().Split(',')[1].ToString());
                                    }
                                    if ((ddlCrd_Branch.Text != "Select") && (ddlCrd_Branch.Text != "0"))
                                    {
                                        CRBranchid = Convert.ToInt32(ddlCrd_Branch.SelectedValue);
                                        CrVoucherNo_Pallathur = 0;
                                        crVoucher_Series = "BRANCH";
                                        CR_SelectedBranchid = Convert.ToInt32(ddlSubsHeads.SelectedValue.Split(',')[1].ToString().Trim());
                                    }
                                    else
                                    {
                                        CRBranchid = Convert.ToInt32(Session["Branchid"]);
                                        CrVoucherNo_Pallathur = Convert.ToInt32(txtVoucherNo.Text);
                                        crVoucher_Series = txtSeries.Text.ToString().Trim();
                                    }
                                   
                                    string strCreditHeadQuery = "INSERT INTO `svcf`.`voucher` (`Other_Trans_Type`,`DualTransactionKey`,`BranchID`,`CurrDate`,`Voucher_No`,`Voucher_Type`,`Head_Id`,`ChoosenDate`,`Narration`,`Amount`,`Series`,`ReceievedBy`,`Trans_Type`,`T_Day`,`T_Month`,`T_Year`,`MemberID`,`Trans_Medium`,`RootID`,`ChitGroupId`) VALUES (" + othertranstype + "," + DualTransactionKey + "," + CRBranchid + ",'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," + CrVoucherNo_Pallathur + ",'C'," + ddlSubsHeads.SelectedValue.Split(',')[1].ToString().Trim() + ",'" + dtChoosenDate.ToString("yyyy-MM-dd") + "','" + balayer.ReplaceJnk(txtDescription.Text) + "'," + txtSubAmount.Text + ",'" + crVoucher_Series + "','" + balayer.ReplaceJnk(txtReceivedBy.Text) + "',0," + dtChoosenDate.Day + "," + dtChoosenDate.Month + "," + dtChoosenDate.Year + "," + memberid + "," + trans_medium + "," + ddlSubsHeads.SelectedValue.ToString().Split(',')[0] + "," + chitGroupId + ") ";
                                    long iResult = trn.insertorupdateTrn(strCreditHeadQuery);
                                    if (txtChk.Text.Trim() != "" & txtChk.Visible)
                                    {
                                        string strBankInsertQuery = "insert into transbank(BranchID, TransactionKey, DualTransactionKey, T_Day, T_Month, T_Year, BankHeadID, Head_Id, MemberID, CustomersBankName, DateInCheque, ChequeDDNO, GivenAmount, TotalChequeDDAmount, IsBounced, Trans_Type) values (" + CRBranchid + "," + balayer.ToobjectstrEvenNull((object)iResult) + "," + DualTransactionKey + "," + dtChoosenDate.Day + "," + dtChoosenDate.Month + "," + dtChoosenDate.Year + "," + ddlSubsHeads.SelectedValue.Split(',')[1].ToString().Trim() + ",000,000,'','" + balayer.indiandateToMysqlDate(txtDate.Text) + "'," + txtChk.Text + "," + txtSubAmount.Text + "," + txtSubAmount.Text + ",0,0)";
                                        trn.insertorupdateTrn(strBankInsertQuery);
                                    }
                                }
                                //trans_medium = "0";
                                for (int iTrans = 0; iTrans < GridGuardiansDebit.Rows.Count; iTrans++)
                                {
                                    //trans_medium = "0";
                                    DropDownList ddlSubsHeads = (DropDownList)GridGuardiansDebit.Rows[iTrans].FindControl("ddlHeadsDebit");
                                    DropDownList ddlDbr_Branch = (DropDownList)GridGuardiansDebit.Rows[iTrans].FindControl("ddlDBBranch");
                                    int rootid = int.Parse(ddlSubsHeads.SelectedValue.ToString().Split(',')[0].ToString().Trim().Trim(',').Trim());
                                    if (rootid == 5)
                                    {
                                        memberid = balayer.GetSingleValue("select `MemberID` from membertogroupmaster where `Head_Id`=" + ddlSubsHeads.SelectedValue.ToString().Split(',')[1].ToString().Trim().Trim(',').Trim());
                                    }
                                    else
                                    {
                                        memberid = "0";
                                    }
                                    if (memberid == "")
                                    {
                                        memberid = "0";
                                    }
                                    TextBox txtSubAmount = (TextBox)GridGuardiansDebit.Rows[iTrans].FindControl("txtAmountDebit");
                                    TextBox txtDescription = (TextBox)GridGuardiansDebit.Rows[iTrans].FindControl("txtDescriptionDebit");
                                    string NodeID = ddlSubsHeads.SelectedValue.Split(',')[1].Trim();
                                  
                                    string chitGroupId = "0";
                                    if (int.Parse(ddlSubsHeads.SelectedValue.ToString().Split(',')[0].ToString().Trim().Trim(',').Trim()) == 5)
                                    {
                                        chitGroupId = balayer.GetSingleValue("select ParentID from headstree where NodeID=" + ddlSubsHeads.SelectedValue.ToString().Split(',')[1].ToString());
                                    }
                                    if (chitGroupId == "")
                                    {
                                        chitGroupId = "0";
                                    }
                                    if ((ddlDbr_Branch.Text != "Select") && (ddlDbr_Branch.Text != "0"))
                                    {
                                        DBRBranchid = Convert.ToInt32(ddlDbr_Branch.SelectedValue);
                                        DbrVoucherNo_Pallathur = 0;
                                        DbrVoucher_Series = "BRANCH";
                                        DBR_SelectedBranchid = Convert.ToInt32(ddlSubsHeads.SelectedValue.Split(',')[1].ToString().Trim());
                                    }
                                    else
                                    {
                                        DBRBranchid = Convert.ToInt32(Session["Branchid"]);
                                        DbrVoucherNo_Pallathur = Convert.ToInt32(txtVoucherNo.Text);
                                        DbrVoucher_Series = txtSeries.Text.ToString().Trim();
                                    }
                                    string strDebitHeadQuery = "";
                                    //if (CR_SelectedBranchid == DBR_SelectedBranchid)
                                    //{
                                    //    strDebitHeadQuery = "INSERT INTO `svcf`.`voucher` (`Other_Trans_Type`,`DualTransactionKey`,`BranchID`,`CurrDate`,`Voucher_No`,`Voucher_Type`,`Head_Id`,`ChoosenDate`,`Narration`,`Amount`,`Series`,`ReceievedBy`,`Trans_Type`,`T_Day`,`T_Month`,`T_Year`,`MemberID`,`Trans_Medium`,`RootID`,`ChitGroupId`) VALUES (" + othertranstype + "," + DualTransactionKey + "," + CRBranchid + ",'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," + DbrVoucherNo_Pallathur + ",'D'," + ddlSubsHeads.SelectedValue.Split(',')[1].ToString().Trim() + ",'" + dtChoosenDate.ToString("yyyy-MM-dd") + "','" + balayer.ReplaceJnk(txtDescription.Text) + "'," + txtSubAmount.Text + ",'" + DbrVoucher_Series + "','" + balayer.ReplaceJnk(txtReceivedBy.Text) + "',0," + dtChoosenDate.Day + "," + dtChoosenDate.Month + "," + dtChoosenDate.Year + "," + memberid + "," + trans_medium + "," + ddlSubsHeads.SelectedValue.ToString().Split(',')[0] + "," + chitGroupId + ") ";
                                    //}
                                    //else
                                    //{
                                        strDebitHeadQuery = "INSERT INTO `svcf`.`voucher` (`Other_Trans_Type`,`DualTransactionKey`,`BranchID`,`CurrDate`,`Voucher_No`,`Voucher_Type`,`Head_Id`,`ChoosenDate`,`Narration`,`Amount`,`Series`,`ReceievedBy`,`Trans_Type`,`T_Day`,`T_Month`,`T_Year`,`MemberID`,`Trans_Medium`,`RootID`,`ChitGroupId`) VALUES (" + othertranstype + "," + DualTransactionKey + "," + DBRBranchid + ",'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," + DbrVoucherNo_Pallathur + ",'D'," + ddlSubsHeads.SelectedValue.Split(',')[1].ToString().Trim() + ",'" + dtChoosenDate.ToString("yyyy-MM-dd") + "','" + balayer.ReplaceJnk(txtDescription.Text) + "'," + txtSubAmount.Text + ",'" + DbrVoucher_Series + "','" + balayer.ReplaceJnk(txtReceivedBy.Text) + "',0," + dtChoosenDate.Day + "," + dtChoosenDate.Month + "," + dtChoosenDate.Year + "," + memberid + "," + trans_medium + "," + ddlSubsHeads.SelectedValue.ToString().Split(',')[0] + "," + chitGroupId + ") ";
                                   // }
                                    long iResult = trn.insertorupdateTrn(strDebitHeadQuery);
                                    if (ddlSubsHeads.SelectedValue.ToString().Contains("3,"))
                                    {
                                        //string TransactionKey = balayer.GetSingleValue("SELECT TransactionKey  FROM `svcf`.`voucher` where uuid_from_bin(DualTransactionKey)=uuid_from_bin(" + DualTransactionKey + ") and Head_Id=" + ddlSubsHeads.SelectedValue.Split(',')[1] + " order by TransactionKey desc limit 1;");
                                        string strBankInsertQuery = "insert into transbank(BranchID, TransactionKey, DualTransactionKey, T_Day, T_Month, T_Year, BankHeadID, Head_Id, MemberID, CustomersBankName, DateInCheque, ChequeDDNO, GivenAmount, TotalChequeDDAmount, IsBounced, Trans_Type) values (" + DBRBranchid + "," + balayer.ToobjectstrEvenNull((object)iResult) + "," + DualTransactionKey + "," + dtChoosenDate.Day + "," + dtChoosenDate.Month + "," + dtChoosenDate.Year + "," + ddlSubsHeads.SelectedValue.Split(',')[1].ToString().Trim() + ",000,000,'','" + balayer.indiandateToMysqlDate(txtDate.Text) + "'," + "000" + "," + txtSubAmount.Text + "," + txtSubAmount.Text + ",0,0)";
                                        trn.insertorupdateTrn(strBankInsertQuery);
                                    }
                                }
                                trn.CommitTrn();
                                logger.Info("AdjustmentVoucher.aspx - btnYes_Click() - Completed: " + DateTime.Now + " by: " + Convert.ToString(Session["UserName"]) + "");
                            }
                            catch (Exception ex)
                            {
                                try
                                {
                                   trn.RollbackTrn();
                                }
                                catch(Exception err)
                                {
                                    logger.Info("AdjustmentVoucher.aspx - btnYes_Click() - Error: " + err.Message + ": " + DateTime.Now + " by: " + Convert.ToString(Session["UserName"]) + "");
                                }
                                finally
                                {
                                    btnYes.Focus();
                                    isFailed = true;
                                    //balayer.GetInsertItem("delete  FROM `svcf`.`voucher` where DualTransactionKey=" + DualTransactionKey);
                                    //balayer.GetInsertItem("delete  FROM `svcf`.`transbank` where DualTransactionKey=" + DualTransactionKey);
                                    MpAll.PopupControlID = "pnlpopup";
                                    MpAll.Show();
                                    pnlpopup.Visible = true;
                                    //lblHD.Text = "Status";
                                    lblHint.Text = "Reload";
                                    lblHeading.Text = "Error Status!!!";
                                    //lblContent.Text = "Problem with Your Transaction Please Contact Administrator!!!";
                                    lblContent.Text = ex.Message;
                                    lblContent.ForeColor = System.Drawing.Color.Red;
                                }
                            }
                            finally
                            {
                                trn.DisposeTrn();
                                if (!isFailed)
                                {
                                    btnYes.Focus();
                                    lblHint.Text = "Reload";
                                    lblHeading.Text = "Status!!!";
                                    lblContent.Text = "Voucher Added Successfully!!! ";
                                    MpAll.PopupControlID = "pnlpopup";
                                    btnYes.Focus();
                                    MpAll.Show();
                                    pnlpopup.Visible = true;
                                }
                            }
                            gvoldmember.DataSource = null;
                            gvoldmember.DataBind();
                        }
                        else
                            if (lblHint.Text == "ichk")
                            {
                                lblHint.Text = "";
                                if (DynamicControlsHolder.Controls.Count > 0)
                                {
                                    DynamicControlsHolder.Controls.RemoveAt(0);
                                }
                            }
                            else
                            {
                            }
        }
        protected void btnNo_Click(object sender, EventArgs e)
        {
            if (lblHint.Text == "Reload")
            {
                lblHint.Text = "";
                this.Response.Redirect(this.Request.Url.AbsolutePath.ToString(), false);
            }
            else
                if (lblHint.Text == "VExist")
                {
                    txtVoucherNo.Text = "";
                    txtSeries.Text = "";
                    txtSeries.Focus();
                    lblHint.Text = "";
                }
                else
                    if (lblHint.Text == "HeadConfirmation")
                    {
                        lblHint.Text = "";
                    }
                    else
                        if (lblHint.Text == "vConfirm")
                        {
                            gvoldmember.DataSource = null;
                            gvoldmember.DataBind();
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
        protected void BtnChequeCancel_Click(object sender, EventArgs e)
        {
            ClearControls("panCheque");
        }
        protected void BtnChequeUpdate_Click(object sender, EventArgs e)
        {
        }
        protected void ddlInstrument_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
        protected void btnGenerate_Click(object sender, EventArgs e)
        {
            Page.Validate("Generate");
            Page.Validate("GrpRow");
            Page.Validate("GrpRowDebit");
            if (!Page.IsValid)
            {
                return;
            }
            if (Session["CheckRefresh"].ToString() != ViewState["CheckRefresh"].ToString())
            {
                return;
            }
            string ResExist = balayer.GetSingleValue("select count(*) from voucher where ChoosenDate>='2014/04/01' and Series='" + txtSeries.Text.ToUpper().Trim() + "' and  Voucher_No=" + txtVoucherNo.Text.Trim() + " and Trans_Type='0' and BranchId=" + Session["Branchid"] + "");
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
            for (int iTrans = 0; iTrans < GridGuardians.Rows.Count; iTrans++)
            {
                TextBox txtChk = (TextBox)GridGuardians.Rows[iTrans].FindControl("txtChequeNO");
                if (txtChk.Visible)
                {
                    if (txtChk.Text.Trim() == "")
                    {
                        lblHeading.Text = "Error!!!";
                        lblContent.Text = "Please fill ChequeNo!!!" + "</br></br>&nbsp;&nbsp;";
                        MpAll.PopupControlID = "pnlpopup";
                        gvoldmember.Columns.Clear();
                        gvoldmember.DataSource = null;
                        gvoldmember.DataBind();
                        MpAll.PopupControlID = "pnlpopup";
                        MpAll.Show();
                        btnYes.Focus();
                        pnlpopup.Visible = true;
                        return;
                    }
                }
            }
            DataTable dtConfirmation = new DataTable();
            dtConfirmation.Columns.Add("Heads");
            dtConfirmation.Columns.Add("Credit");
            dtConfirmation.Columns.Add("Debit");
            decimal totalright = 0.0M;
            decimal totalleft = 0.0M;
            string error = "";
            string errorDebit = "";
            int TotalNoofRows = GridGuardians.Rows.Count + GridGuardiansDebit.Rows.Count;
            for (int iRC = 0; iRC < TotalNoofRows; iRC++)
            {
                dtConfirmation.Rows.Add();
            }
            for (int iRow = 0; iRow < GridGuardians.Rows.Count; iRow++)
            {
                TextBox txtsubAmount = (TextBox)GridGuardians.Rows[iRow].FindControl("txtAmount");
                dtConfirmation.Rows[iRow][1] = txtsubAmount.Text;
                dtConfirmation.Rows[iRow][0] = ((DropDownList)GridGuardians.Rows[iRow].FindControl("ddlHeads")).SelectedItem.Text;
                error += txtsubAmount.Text + " + ";
                totalleft += decimal.Parse(txtsubAmount.Text.Trim());
            }
            for (int iRow = 0; iRow < GridGuardiansDebit.Rows.Count; iRow++)
            {
                TextBox txtsubAmount = (TextBox)GridGuardiansDebit.Rows[iRow].FindControl("txtAmountDebit");
                dtConfirmation.Rows[GridGuardians.Rows.Count + iRow][2] = txtsubAmount.Text;
                dtConfirmation.Rows[GridGuardians.Rows.Count + iRow][0] = ((DropDownList)GridGuardiansDebit.Rows[iRow].FindControl("ddlHeadsDebit")).SelectedItem.Text;
                errorDebit += txtsubAmount.Text + " + ";
                totalright += decimal.Parse(txtsubAmount.Text.Trim());
            }
            if (totalleft == totalright)
            {
                lblHint.Text = "VConfirm";
                lblHeading.Text = "Confirmation";
                lblContent.Text = " Please Confirm Your Voucher Details???";
                gvoldmember.DataSource = dtConfirmation;
                gvoldmember.DataBind();
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
                gvoldmember.DataSource = dtConfirmation;
                gvoldmember.DataBind();
                MpAll.PopupControlID = "pnlpopup";
                MpAll.Show();
                btnYes.Focus();
                pnlpopup.Visible = true;
            }
        }
        protected void GetSeriesAndVoucherNo()
        {
            txtVoucherNo.Text = balayer.GetSingleValue("SELECT ifnull(max(cast(Voucher_No as unsigned)),0)+1 FROM svcf.voucher where ChoosenDate>='2014/04/01' and  Series='VOUCHER' and BranchID=" + Session["Branchid"]);
        }
        protected void Member_Choose_Click(object sender, EventArgs e)
        {
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
    }
}