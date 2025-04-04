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
    public partial class OBPvoucher : System.Web.UI.Page
    {
        //#region VarDeclaration
        //CommonClassFile objcls = new CommonClassFile();
        //#endregion
        #region VarDeclaration
        BusinessLayer balayer = new BusinessLayer();
        TransactionLayer tranlayer = new TransactionLayer();
        #endregion
        ILog logger = log4net.LogManager.GetLogger(typeof(OBPvoucher));

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
                    ddlHeads.TabIndex = ++currentTabIndex;
                    txtAmount.TabIndex = ++currentTabIndex;
                    txtDescription.TabIndex = ++currentTabIndex;
                    txtChequeNO.TabIndex = ++currentTabIndex;
                    //if (showbutton != null)
                    //{
                    //    showbutton.TabIndex = ++currentTabIndex;
                    //}
                    //if (showRemovebutton != null)
                    //{
                    //    showRemovebutton.TabIndex = ++currentTabIndex;
                    //}
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
                    ddlHeads.TabIndex = ++currentTabIndex;
                    txtAmount.TabIndex = ++currentTabIndex;
                    txtDescription.TabIndex = ++currentTabIndex;
                    //if (showbutton != null)
                    //{
                    //    showbutton.TabIndex = ++currentTabIndex;
                    //}
                    //if (showRemovebutton != null)
                    //{
                    //    showRemovebutton.TabIndex = ++currentTabIndex;
                    //}
                    //txtChequeNO.TabIndex = ++currentTabIndex;
                    // dt1.Rows.Add(dtrow);
                }
                btnGenerate.TabIndex = ++currentTabIndex;
                //after
            }
        }
        protected void bindHeads(DropDownList ddlHeads)
        {
            string qry = "";
            //DataTable dtAllHeads = balayer.GetDataTable("SELECT concat(cast(  `RootID` as char),',',cast(`TreeID` as char)) as ID,`TREE` FROM `svcf`.`view_parent` where RootID Not in(44)");
            //DataTable dtAllHeads = balayer.GetDataTable("SELECT concat(cast(  v1.`RootID` as char),',',cast(v1.`TreeID` as char)) as ID,v1.`TREE`,m1.`BranchID` FROM `svcf`.`view_parent` as v1 left join `membertogroupmaster` as m1 on (v1.`TreeID` =m1.Head_Id) where v1.RootID Not in(44) and m1.`BranchID` is null or m1.`BranchID`=" + balayer.ToobjectstrEvenNull(Session["Branchid"]));
            DataTable PandL = new DataTable();
            DataTable dtAllHeads = balayer.GetDataTable("SELECT concat(cast(  v1.`RootID` as char),',',cast(v1.`TreeID` as char)) as ID,v1.`TREE` FROM `svcf`.`view_parent` as v1 where v1.RootID in (3,11) and v1.BranchID=" + Session["Branchid"]);
            qry = "select concat(cast(  `RootID` as char),',',cast(`TreeID` as char)) as ID,TREE from view_parent where RootID=11";
            PandL = balayer.GetDataTable(qry);
            dtAllHeads.Merge(PandL);
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
            DataTable dtAllHeads = balayer.GetDataTable("SELECT concat(cast(  v1.`RootID` as unsigned),',',cast(v1.`TreeID` as char)) as ID,v1.`TREE` FROM `svcf`.`view_parent` as v1  where v1.RootID=1 and v1.TreeID!=" + Session["Branchid"]);
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
        protected void ddlChits_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlChits.SelectedIndex == 0)
            {
                txtIncidentialCharges.Text = "";
                foreach (GridViewRow dr in GridGuardiansDebit.Rows)
                {
                    DropDownList ddlHeads = (DropDownList)dr.FindControl("ddlHeadsDebit");
                    ddlHeads.SelectedValue = "--Select--";
                }
            }
            else
            {
                foreach (GridViewRow dr in GridGuardiansDebit.Rows)
                {
                    string strGroup = balayer.GetSingleValue("SELECT GroupID FROM svcf.membertogroupmaster where Head_Id=" + ddlChits.SelectedValue.Split('|')[0]);
                    string strBranch = balayer.GetSingleValue("SELECT BranchID FROM svcf.membertogroupmaster where Head_Id=" + ddlChits.SelectedValue.Split('|')[0]);
                    string strGroupValue = balayer.GetSingleValue("SELECT ChitValue FROM svcf.groupmaster where Head_Id=" + strGroup);
                    string strInci = balayer.GetSingleValue("SELECT IncidentalCharges FROM svcf.commissiondetails where ChitValue=" + strGroupValue);
                    txtIncidentialCharges.Text = strInci;

                    string strGst = balayer.GetSingleValue("SELECT if(GstAmount>0 and GstAmount is not null,GstAmount,0) as GstAmount FROM svcf.commissiondetails where ChitValue=" + strGroupValue);
                    txtGST.Text = strGst;

                    string strigst = balayer.GetSingleValue("SELECT if(IgstAmount>0 and IgstAmount is not null,IgstAmount,0) as IgstAmount FROM svcf.commissiondetails where ChitValue=" + strGroupValue);
                    txtigst.Text = strigst;

                    string strcgst = balayer.GetSingleValue("SELECT if(CgstAmount>0 and CgstAmount is not null,CgstAmount,0) as IgstAmount FROM svcf.commissiondetails where ChitValue=" + strGroupValue);
                    txtcgst.Text = strcgst;

                    string strsgst = balayer.GetSingleValue("SELECT if(SgstAmount>0 and SgstAmount is not null,SgstAmount,0) as SgstAmount FROM svcf.commissiondetails where ChitValue=" + strGroupValue);
                    txtsgst.Text = strsgst;

                    DropDownList ddlHeads = (DropDownList)dr.FindControl("ddlHeadsDebit");
                    ddlHeads.SelectedValue = "1," + strBranch;

                    TextBox txtAmount = (TextBox)dr.FindControl("txtAmountDebit");
                    txtAmount.Text = Convert.ToString(Convert.ToDecimal(balayer.GetSingleValue("SELECT PrizedAmount FROM svcf.auctiondetails where PrizedMemberID=" + ddlChits.SelectedValue.Split('|')[0])) - Convert.ToDecimal(txtIncidentialCharges.Text));

                    txtAmount.Text = Convert.ToString(Convert.ToDecimal(txtAmount.Text) - Convert.ToDecimal(txtGST.Text) - Convert.ToDecimal(txtsgst.Text) - Convert.ToDecimal(txtcgst.Text) - Convert.ToDecimal(txtigst.Text));
                }
                foreach (GridViewRow dr in GridGuardians.Rows)
                {
                    string strGroup = balayer.GetSingleValue("SELECT GroupID FROM svcf.membertogroupmaster where Head_Id=" + ddlChits.SelectedValue.Split('|')[0]);
                    string strGroupValue = balayer.GetSingleValue("SELECT ChitValue FROM svcf.groupmaster where Head_Id=" + strGroup);
                    string strInci = balayer.GetSingleValue("SELECT IncidentalCharges FROM svcf.commissiondetails where ChitValue=" + strGroupValue);

                    string strGst = balayer.GetSingleValue("SELECT if(GstAmount>0 and GstAmount is not null,GstAmount,0) as GstAmount FROM svcf.commissiondetails where ChitValue=" + strGroupValue);
                    txtGST.Text = strGst;

                    string strigst = balayer.GetSingleValue("SELECT if(IgstAmount>0 and IgstAmount is not null,IgstAmount,0) as IgstAmount FROM svcf.commissiondetails where ChitValue=" + strGroupValue);
                    txtigst.Text = strigst;

                    string strcgst = balayer.GetSingleValue("SELECT if(CgstAmount>0 and CgstAmount is not null,CgstAmount,0) as IgstAmount FROM svcf.commissiondetails where ChitValue=" + strGroupValue);
                    txtcgst.Text = strcgst;

                    string strsgst = balayer.GetSingleValue("SELECT if(SgstAmount>0 and SgstAmount is not null,SgstAmount,0) as SgstAmount FROM svcf.commissiondetails where ChitValue=" + strGroupValue);
                    txtsgst.Text = strsgst;

                    TextBox txtAmount = (TextBox)dr.FindControl("txtAmount");
                    txtAmount.Text = Convert.ToString(Convert.ToDecimal(balayer.GetSingleValue("SELECT PrizedAmount FROM svcf.auctiondetails where PrizedMemberID=" + ddlChits.SelectedValue.Split('|')[0])) - Convert.ToDecimal(txtIncidentialCharges.Text));
                    txtAmount.Text = Convert.ToString(Convert.ToDecimal(txtAmount.Text) - Convert.ToDecimal(txtGST.Text) - Convert.ToDecimal(txtsgst.Text) - Convert.ToDecimal(txtcgst.Text) - Convert.ToDecimal(txtigst.Text));
                }
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            pnlpopup.Visible = false;
            if (!IsPostBack)
            {
                
                //rvApproved.MinimumValue = balayer.GetSingleValue("SELECT DATE_FORMAT( min(ChoosenDate), '%d/%m/%Y') MinimumDate FROM svcf.voucher where " +
                //   "BranchID=" + Session["Branchid"] + " and ChoosenDate<>'0000-00-00'");

                Session["CheckRefresh"] = System.Guid.NewGuid().ToString();
                txtReceivedBy.Text = balayer.ToobjectstrEvenNull(Session["UserName"]);
                ViewState["tabnum"] = "57";
                // hiddenAccordionIndex.Value = "0";

                string strQuery = "SELECT concat(cast(m2.GrpMemberID as char),' | ',cast(m1.CustomerName as char)) as CustomerName,concat(cast(m2.Head_Id as char),'|',cast(m1.MemberIDNew as char)) as ID FROM svcf.auctiondetails as a1 join membermaster as m1 on (a1.MemberID=m1.MemberIDNew) join membertogroupmaster as m2 on (a1.PrizedMemberID=m2.Head_Id) where IsPrized='N' and a1.BranchID<>" + Session["Branchid"];

                DataTable dtAllHeads = balayer.GetDataTable(strQuery);
                DataRow dr = dtAllHeads.NewRow();
                dr[0] = "--Select--";
                dr[1] = "--Select--";
                dtAllHeads.Rows.InsertAt(dr, 0);
                ddlChits.DataSource = dtAllHeads;
                ddlChits.DataTextField = "CustomerName";
                ddlChits.DataValueField = "ID";
                ddlChits.DataBind();

                this.BindData();
                SetDefaultRow();
                this.BindDataDebit();
                SetDefaultRowDebit();
                txtDate.Focus();
                txtDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                SetGridTabIndex(true);
                SetGridTabIndex(false);
            }

            rvDate.MaximumValue = DateTime.Now.ToString("dd/MM/yyyy");
            rvDate.MinimumValue = balayer.GetSingleValue("SELECT DATE_FORMAT( min(MinimumDate), '%d/%m/%Y') MinimumDate FROM svcf.restrictionmaster where BranchID=" + Session["Branchid"]);
            //rvDate.MinimumValue = balayer.GetSingleValue("SELECT DATE_FORMAT( min(ChoosenDate), '%d/%m/%Y') MinimumDate FROM svcf.voucher where " +
            //   "BranchID=" + Session["Branchid"] + " and ChoosenDate<>'0000-00-00'");

            rvAppied.MaximumValue = DateTime.Now.ToString("dd/MM/yyyy");
            rvAppied.MinimumValue = balayer.GetSingleValue("SELECT DATE_FORMAT( min(MinimumDate), '%d/%m/%Y') MinimumDate FROM svcf.restrictionmaster where BranchID=" + Session["Branchid"]);
            //rvAppied.MinimumValue = balayer.GetSingleValue("SELECT DATE_FORMAT( min(ChoosenDate), '%d/%m/%Y') MinimumDate FROM svcf.voucher where " +
            //   "BranchID=" + Session["Branchid"] + " and ChoosenDate<>'0000-00-00'");

            rvApproved.MaximumValue = DateTime.Now.ToString("dd/MM/yyyy");
            rvApproved.MinimumValue = balayer.GetSingleValue("SELECT DATE_FORMAT( min(MinimumDate), '%d/%m/%Y') MinimumDate FROM svcf.restrictionmaster where BranchID=" + Session["Branchid"]);

        }
        public void BindBranch(DropDownList ddrname)
        {
            DataTable dtgroupno = null;
            dtgroupno = balayer.GetDataTable("select NodeID,Node from headstree where ParentID=1");
            DataRow dr = dtgroupno.NewRow();
            dr[0] = "0";
            dr[1] = "--Select--";
            ddrname.DataValueField = "NodeID";
            ddrname.DataTextField = "Node";
            dtgroupno.Rows.InsertAt(dr, 0);
            ddrname.DataSource = dtgroupno;
            ddrname.DataBind();
        }

        public void BindGroup(DropDownList ddrgroup, int branchid)
        {
            string qry = "select Head_Id, GROUPNO from groupmaster where BranchID=" + branchid + "";
            DataTable dtgrp = null;
            dtgrp = balayer.GetDataTable(qry);
            DataRow dr1 = dtgrp.NewRow();
            dr1[0] = "0";
            dr1[1] = "--Select--";
            ddrgroup.DataValueField = "Head_Id";
            ddrgroup.DataTextField = "GROUPNO";
            dtgrp.Rows.InsertAt(dr1, 0);
            ddrgroup.DataSource = dtgrp;
            ddrgroup.DataBind();
        }

        public void BindChitToken(DropDownList ddlToken, int groupid)
        {
            string qry = "SELECT Head_Id,GrpMemberID FROM membertogroupmaster where GroupID=" + groupid + "";
            DataTable dttokenList = null;
            dttokenList = balayer.GetDataTable(qry);
            DataRow dr2 = dttokenList.NewRow();
            dr2[0] = "0";
            dr2[1] = "--Select--";
            ddlToken.DataValueField = "Head_Id";
            ddlToken.DataTextField = "GrpMemberID";
            dttokenList.Rows.InsertAt(dr2, 0);
            ddlToken.DataSource = dttokenList;
            ddlToken.DataBind();
        }
        protected void txtAmount_TextChanged(object sender, EventArgs e)
        {
        }
        protected void GridGuardians_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            DropDownList ddlBranch = (DropDownList)e.Row.FindControl("ddl_Branch");
            DropDownList ddlGroup = (DropDownList)e.Row.FindControl("ddl_group");
            DropDownList ddlHeads = (DropDownList)e.Row.FindControl("ddlHeads");
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.RowIndex > 0)
                {
                    ddlBranch.DataSource = null;
                    BindBranch(ddlBranch);
                    if (((Label)e.Row.FindControl("lblBranch")).Text != "")
                    {
                        ddlBranch.Items.FindByText(((Label)e.Row.FindControl("lblBranch")).Text).Selected = true;
                    }

                    if ((Convert.ToInt32(ddlBranch.SelectedValue) != 0) && (ddlBranch.SelectedValue != ""))
                    {
                        ddlGroup.DataSource = null;
                        BindGroup(ddlGroup, Convert.ToInt32(ddlBranch.SelectedValue));
                        if (((Label)e.Row.FindControl("lblGroup")).Text != "")
                        {
                            ddlGroup.Items.FindByText(((Label)e.Row.FindControl("lblGroup")).Text).Selected = true;
                        }
                    }

                    if (ddlGroup.SelectedValue != "")
                    {
                        if (Convert.ToInt32(ddlGroup.SelectedValue) != 0)
                        {
                            ddlHeads.DataSource = null;
                            BindChitToken(ddlHeads, Convert.ToInt32(ddlGroup.SelectedValue));
                            if (((Label)e.Row.FindControl("lblHeads")).Text != "")
                            {
                                ddlHeads.Items.FindByText(((Label)e.Row.FindControl("lblHeads")).Text).Selected = true;
                            }
                        }
                    }
                    else
                    {
                        //DataTable dtAllHeads = (DataTable)((DropDownList)GridGuardians.Rows[0].FindControl("ddlHeads")).DataSource;
                        //ddlHeads.DataSource = dtAllHeads;
                        //ddlHeads.DataTextField = "TREE";
                        //ddlHeads.DataValueField = "ID";
                        //ddlHeads.DataBind();           
                        bindHeads(ddlHeads);
                    }
                }
                else
                {

                    ddlBranch.DataSource = null;
                    BindBranch(ddlBranch);
                    if (((Label)e.Row.FindControl("lblBranch")).Text != "")
                    {
                        ddlBranch.Items.FindByText(((Label)e.Row.FindControl("lblBranch")).Text).Selected = true;
                    }
                    if (Convert.ToInt32(ddlBranch.SelectedValue) != 0)
                    {
                        ddlGroup.DataSource = null;
                        BindGroup(ddlGroup, Convert.ToInt32(ddlBranch.SelectedValue));
                        if (((Label)e.Row.FindControl("lblGroup")).Text != "")
                        {
                            ddlGroup.Items.FindByText(((Label)e.Row.FindControl("lblGroup")).Text).Selected = true;
                        }
                        ddlHeads.DataSource = null;
                        BindChitToken(ddlHeads, Convert.ToInt32(ddlGroup.SelectedValue));
                        if (((Label)e.Row.FindControl("lblHeads")).Text != "")
                        {
                            ddlHeads.Items.FindByText(((Label)e.Row.FindControl("lblHeads")).Text).Selected = true;
                        }
                    }
                    else
                    {
                        bindHeads(ddlHeads);
                    }
                }
                if (((Label)e.Row.FindControl("lblHeads")).Text != "")
                {
                    ddlHeads.Items.FindByText(((Label)e.Row.FindControl("lblHeads")).Text).Selected = true;
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
            else if (ddlCred.SelectedValue.ToString().Contains("11,"))
            {
                TextBox txtgrdChequeNo = (TextBox)(((DropDownList)sender).Parent.Parent.FindControl("txtDescription"));
                txtgrdChequeNo.Text = "";
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
                string strMemberName = balayer.GetSingleValue("SELECT MemberName FROM svcf.membertogroupmaster where Head_Id=" + ddlCred.SelectedItem.Value);
                txtDescription.Text = strMemberName;

                //txtDescription.Text = "";
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
            dtDeb.Columns.Add(dcHead);
            dtDeb.Columns.Add(dcDescription);
            dtDeb.Columns.Add(dcAmount);
            foreach (GridViewRow dr in GridGuardiansDebit.Rows)
            {
                DataRow dtDbrow = dtDeb.NewRow();
                DropDownList ddlDbHeads = (DropDownList)dr.FindControl("ddlHeadsDebit");
                dtDbrow["Heads"] = ddlDbHeads.SelectedItem.Text;
                TextBox txtAmount = (TextBox)dr.FindControl("txtAmountDebit");
                dtDbrow["Amount"] = txtAmount.Text;
                TextBox txtDescription = (TextBox)dr.FindControl("txtDescriptionDebit");
                dtDbrow["Description"] = txtDescription.Text;
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
            dt1.Columns.Add(dcDescription);
            dt1.Columns.Add(dcHead);
            dt1.Columns.Add(dcAmount);
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
            DataColumn dcBranch = new DataColumn("Branch", typeof(string));
            DataColumn dcGroup = new DataColumn("Group", typeof(string));
            DataColumn dcHead = new DataColumn("Heads", typeof(string));
            DataColumn dcAmount = new DataColumn("Amount", typeof(string));
            DataColumn dcDescription = new DataColumn("Description", typeof(string));
            DataColumn dcchequeNO = new DataColumn("ChequeNO", typeof(string));
            dt.Columns.Add(dcchequeNO);
            dt.Columns.Add(dcDescription);
            dt.Columns.Add(dcHead);
            dt.Columns.Add(dcAmount);
            dt.Columns.Add(dcGroup);
            dt.Columns.Add(dcBranch);
            foreach (GridViewRow dr in GridGuardians.Rows)
            {
                DataRow dtrow = dt.NewRow();
                DropDownList ddl_Branch = (DropDownList)dr.FindControl("ddl_Branch");
                if (ddl_Branch.Items.Count > 0)
                {
                    dtrow["Branch"] = ddl_Branch.SelectedItem.Text;
                }

                DropDownList ddl_group = (DropDownList)dr.FindControl("ddl_group");
                if (ddl_group.Items.Count > 0)
                {
                    dtrow["Group"] = ddl_group.SelectedItem.Text;
                }

                DropDownList ddlHeads = (DropDownList)dr.FindControl("ddlHeads");
                dtrow["Heads"] = ddlHeads.SelectedItem.Text;
                TextBox txtAmount = (TextBox)dr.FindControl("txtAmount");
                dtrow["Amount"] = txtAmount.Text;
                TextBox txtDescription = (TextBox)dr.FindControl("txtDescription");
                dtrow["Description"] = txtDescription.Text;
                TextBox txtChequeNO = (TextBox)dr.FindControl("txtChequeNO");
                dtrow["ChequeNO"] = txtChequeNO.Text;
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
            DataColumn dcBranch = new DataColumn("Branch", typeof(string));
            DataColumn dcGroup = new DataColumn("Group", typeof(string));
            DataColumn dcHead = new DataColumn("Heads", typeof(string));
            DataColumn dcAmount = new DataColumn("Amount", typeof(string));
            DataColumn dcDescription = new DataColumn("Description", typeof(string));
            DataColumn dcchequeNo = new DataColumn("ChequeNO", typeof(string));
            dt1.Columns.Add(dcDescription);
            dt1.Columns.Add(dcchequeNo);
            dt1.Columns.Add(dcHead);
            dt1.Columns.Add(dcAmount);
            dt1.Columns.Add(dcGroup);
            dt1.Columns.Add(dcBranch);
            short i = 0;
            foreach (GridViewRow dr in GridGuardians.Rows)
            {
                i += 1;
                DataRow dtrow = dt1.NewRow();
                DropDownList ddl_Branch = (DropDownList)dr.FindControl("ddl_Branch");
                if (ddl_Branch.Items.Count > 0)
                {
                    dtrow["Branch"] = ddl_Branch.SelectedItem.Text;
                }

                DropDownList ddl_group = (DropDownList)dr.FindControl("ddl_group");
                if (ddl_group.Items.Count > 0)
                {
                    dtrow["Group"] = ddl_group.SelectedItem.Text;
                }
                DropDownList ddlHeads = (DropDownList)dr.FindControl("ddlHeads");
                dtrow["Heads"] = ddlHeads.SelectedItem.Text;
                TextBox txtAmount = (TextBox)dr.FindControl("txtAmount");
                dtrow["Amount"] = txtAmount.Text;
                TextBox txtDescription = (TextBox)dr.FindControl("txtDescription");
                dtrow["Description"] = txtDescription.Text;
                TextBox txtChequeNO = (TextBox)dr.FindControl("txtChequeNO");
                dtrow["ChequeNO"] = txtChequeNO.Text;
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
            dt.Columns.Add(dcDescription);
            dt.Columns.Add(dcAmount);
            dt.Columns.Add(dcHeads);
            GridGuardiansDebit.DataSource = dt;
            GridGuardiansDebit.DataBind();
        }
        private void BindData()
        {
            DataTable dtSelect = new DataTable();
            DataTable dt = new DataTable();
            DataColumn dcBranch = new DataColumn("Branch", typeof(string));
            DataColumn dcGroup = new DataColumn("Group", typeof(string));
            DataColumn dcHeads = new DataColumn("Heads", typeof(string));
            DataColumn dcAmount = new DataColumn("Amount", typeof(string));
            DataColumn dcDescription = new DataColumn("Description", typeof(string));
            DataColumn dcchequeNO = new DataColumn("chequeNO", typeof(string));
            dt.Columns.Add(dcDescription);
            dt.Columns.Add(dcchequeNO);
            dt.Columns.Add(dcAmount);
            dt.Columns.Add(dcHeads);
            GridGuardians.DataSource = dt;
            GridGuardians.DataBind();
        }
        public void SetDefaultRow()
        {
            DataTable dt = new DataTable();
            // DataColumn ShowButton = new DataColumn("ShowAddButton", typeof(bool));
            // DataColumn ShowRemoveButton = new DataColumn("ShowRemoveButton", typeof(bool));
            DataColumn dcBranch = new DataColumn("Branch", typeof(string));
            DataColumn dcGroup = new DataColumn("Group", typeof(string));
            DataColumn dcHeads = new DataColumn("Heads", typeof(string));
            DataColumn dcAmount = new DataColumn("Amount", typeof(string));
            DataColumn dcDescription = new DataColumn("Description", typeof(string));
            DataColumn dcchequeNO = new DataColumn("chequeNO", typeof(string));
            dt.Columns.Add(dcchequeNO);
            dt.Columns.Add(dcDescription);
            dt.Columns.Add(dcAmount);
            dt.Columns.Add(dcHeads);
            dt.Columns.Add(dcGroup);
            dt.Columns.Add(dcBranch);
            //dt.Columns.Add(ShowRemoveButton);
            // dt.Columns.Add(ShowButton);
            DataRow newRow = dt.NewRow();
            newRow["Heads"] = "--Select--";
            newRow["Amount"] = "";
            //newRow["ShowAddButton"] = true;
            // newRow["ShowRemoveButton"] = true;
            dt.Rows.Add(newRow);
            GridGuardians.DataSource = dt;
            GridGuardians.DataBind();
        }
        public void SetDefaultRowDebit()
        {
            DataTable dt = new DataTable();
            //DataColumn ShowButton = new DataColumn("ShowAddButton", typeof(bool));
            // DataColumn ShowRemoveButton = new DataColumn("ShowRemoveButton", typeof(bool));
            DataColumn dcHeads = new DataColumn("Heads", typeof(string));
            DataColumn dcAmount = new DataColumn("Amount", typeof(string));
            DataColumn dcDescription = new DataColumn("Description", typeof(string));
            DataColumn dcchequeNO = new DataColumn("chequeNO", typeof(string));
            dt.Columns.Add(dcDescription);
            dt.Columns.Add(dcAmount);
            dt.Columns.Add(dcHeads);
            //   dt.Columns.Add(ShowRemoveButton);
            //   dt.Columns.Add(ShowButton);
            dt.Columns.Add(dcchequeNO);
            DataRow newRow = dt.NewRow();
            newRow["Heads"] = "--Select--";
            newRow["Amount"] = "";
            // newRow["ShowAddButton"] = true;
            //newRow["ShowRemoveButton"] = true;
            dt.Rows.Add(newRow);
            GridGuardiansDebit.DataSource = dt;
            GridGuardiansDebit.DataBind();
        }
        protected void btnYes_Click(object sender, EventArgs e)
        {
            TransactionLayer trn = new TransactionLayer();
            string Voucher_No = balayer.GetSingleValue("SELECT max(Voucher_No)+1 FROM svcf.voucher where  BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and Trans_Type=2 and Series='PAYMENT'");
            if (lblHint.Text == "Reload")
            {
                lblHint.Text = "";
                this.Response.Redirect(this.Request.Url.AbsolutePath.ToString());
            }
            else
                if (lblHint.Text == "VExist")
                {
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
                            string NodeID = "";
                            int rootid = 0;
                            string strCreditHeadQuery = "";
                            string strDebitHeadQuery = "";
                            DateTime dtChoosenDate = DateTime.Parse(txtDate.Text);
                            try
                            {
                                for (int iTrans = 0; iTrans < GridGuardians.Rows.Count; iTrans++)
                                {
                                    TextBox txtSubAmount = (TextBox)GridGuardians.Rows[iTrans].FindControl("txtAmount");
                                    string strGroup = balayer.GetSingleValue("SELECT GroupID FROM svcf.membertogroupmaster where Head_Id=" + ddlChits.SelectedValue.Split('|')[0]);
                                    string qry = "INSERT INTO `svcf`.`branchpayment` (`ChitNumber`,`GroupID`,`MemberID`,`IncidentialCharges`,`DualTransactionKey`,Amount,BranchID,PaymentDate,GurantorName,AppliedDate,ApprovedDate,AOsanction,GstCharges,CGSTCharges,SGSTCharges,IGSTCharges)VALUES(" + ddlChits.SelectedValue.Split('|')[0] + "," + strGroup + "," + ddlChits.SelectedValue.Split('|')[1] + "," + txtIncidentialCharges.Text + "," + DualTransactionKey + "," + txtSubAmount.Text + "," + Session["Branchid"] + ",'" + balayer.indiandateToMysqlDate(txtDate.Text) + "','" + balayer.MySQLEscapeString(txtGurantorName.Text) + "','" + balayer.indiandateToMysqlDate(txtAppliedDate.Text) + "','" + balayer.indiandateToMysqlDate(txtApprovedDate.Text) + "','" + txtAOsanction.Text + "'," + Convert.ToDecimal(txtGST.Text.Trim()) + "," + Convert.ToDecimal(txtcgst.Text.Trim()) + "," + Convert.ToDecimal(txtsgst.Text.Trim()) + "," + Convert.ToDecimal(txtigst.Text.Trim()) + ");";
                                    trn.insertorupdateTrn(qry);
                                    break;
                                }

                                DateTime d;
                                bool isDate = DateTime.TryParseExact(txtDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out d);
                                string trans_medium = "";
                                string memberid = "";
                                for (int iTrans = 0; iTrans < GridGuardians.Rows.Count; iTrans++)
                                {
                                    DropDownList ddlSubsHeads = (DropDownList)GridGuardians.Rows[iTrans].FindControl("ddlHeads");
                                    rootid = int.Parse(ddlSubsHeads.SelectedValue.ToString().Split(',')[0].ToString().Trim().Trim(',').Trim());
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
                                if (trans_medium == "")
                                {
                                    for (int iTrans = 0; iTrans < GridGuardiansDebit.Rows.Count; iTrans++)
                                    {
                                        DropDownList ddlSubsHeads = (DropDownList)GridGuardiansDebit.Rows[iTrans].FindControl("ddlHeadsDebit");
                                        rootid = int.Parse(ddlSubsHeads.SelectedValue.ToString().Split(',')[0].ToString().Trim().Trim(',').Trim());
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
                                for (int iTrans = 0; iTrans < GridGuardians.Rows.Count; iTrans++)
                                {
                                    // trans_medium = "0";
                                    DropDownList ddlSubsHeads = (DropDownList)GridGuardians.Rows[iTrans].FindControl("ddlHeads");
                                    //By nithi
                                    //---------------
                                    rootid = int.Parse(ddlSubsHeads.SelectedValue.ToString().Split(',')[0].ToString().Trim().Trim(',').Trim());
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
                                    if (ddlSubsHeads.SelectedValue.ToString().Contains(','))
                                    {
                                        NodeID = ddlSubsHeads.SelectedValue.ToString().Split(',')[1].Trim();
                                    }
                                    else
                                    {
                                        NodeID = ddlSubsHeads.SelectedValue.ToString();
                                    }
                                    //string strInsertQuery = "INSERT INTO `voucher`(`DualTransactionKey`,`BranchID`,`Current_Date`,`Voucher_No`, `Voucher_Type`,`Head_Id`,`ChoosenDate`,`Narration`,`Amount`,`Series`,`ReceievedBy`,`Trans_Type`,`T_Day`, `T_Month`,`T_Year`,`MemberID`) Values ('" + DualTransactionKey + "','" +balayer.ToobjectstrEvenNull(Session["Branchid"]) + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + txtVoucherNo.Text + "','C','" + NodeID + "','" + balayer.indiandateToMysqlDate(txtDate.Text) + "','" + balayer.MySQLEscapeString(txtDescription.Text) + "','" + txtSubAmount.Text + "','" + txtSeries.Text + "','" + txtReceivedBy.Text + "','0','" + balayer.indiandateToMysqlDate(txtDate.Text).Split('/')[2] + "','" + balayer.indiandateToMysqlDate(txtDate.Text).Split('/')[1] + "','" + balayer.indiandateToMysqlDate(txtDate.Text).Split('/')[0] + "','')";
                                    TextBox txtChk = (TextBox)GridGuardians.Rows[iTrans].FindControl("txtChequeNO");
                                    //if (txtChk.Text.Trim() != "" & txtChk.Visible)
                                    //{
                                    //    trans_medium = "1";
                                    //    //balayer.GetInsertItem("INSERT INTO  `vouchersbank` (`BranchID`,`VoucherSeries`,`VoucherNo`,`AccountNO`,`ChequeAmount` ,`IFSC` ,`ChequeDate` ,`ChequeNO`) VALUES ('" +balayer.ToobjectstrEvenNull(Session["Branchid"]) + "','" + txtSeries.Text + "','" + txtVoucherNo.Text + "','" + ddlSubsHeads.SelectedItem.Text.Split('_')[2] + "','" + txtSubAmount.Text + "','" + "" + "','" + balayer.indiandateToMysqlDate(txtDate.Text) + "','" + txtChk.Text + "')");
                                    //}
                                    string chitGroupId = "0";
                                    if (int.Parse(ddlSubsHeads.SelectedValue.ToString().Split(',')[0].ToString().Trim().Trim(',').Trim()) == 5)
                                    {
                                        chitGroupId = balayer.GetSingleValue("select ParentID from headstree where NodeID=" + ddlSubsHeads.SelectedValue.ToString().Split(',')[1].ToString());
                                    }
                                    if (ddlSubsHeads.SelectedValue.ToString().Contains(','))
                                    {
                                        strCreditHeadQuery = "INSERT INTO `svcf`.`voucher` (`Other_Trans_Type`,`DualTransactionKey`,`BranchID`,`CurrDate`,`Voucher_No`,`Voucher_Type`,`Head_Id`,`ChoosenDate`,`Narration`,`Amount`,`Series`,`ReceievedBy`,`Trans_Type`,`T_Day`,`T_Month`,`T_Year`,`MemberID`,`Trans_Medium`,`RootID`,`ChitGroupId`) VALUES (0," + DualTransactionKey + "," + balayer.ToobjectstrEvenNull(Session["Branchid"]) + ",'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," + Voucher_No + ",'C'," + ddlSubsHeads.SelectedValue.Split(',')[1].ToString().Trim() + ",'" + dtChoosenDate.ToString("yyyy-MM-dd") + "','" + balayer.ReplaceJnk(txtDescription.Text) + "'," + txtSubAmount.Text + ",'Voucher','" + balayer.ReplaceJnk(txtReceivedBy.Text) + "',0," + dtChoosenDate.Day + "," + dtChoosenDate.Month + "," + dtChoosenDate.Year + "," + memberid + "," + trans_medium + "," + ddlSubsHeads.SelectedValue.ToString().Split(',')[0] + "," + chitGroupId + ") ";
                                    }
                                    else
                                    {
                                        strCreditHeadQuery = "INSERT INTO `svcf`.`voucher` (`Other_Trans_Type`,`DualTransactionKey`,`BranchID`,`CurrDate`,`Voucher_No`,`Voucher_Type`,`Head_Id`,`ChoosenDate`,`Narration`,`Amount`,`Series`,`ReceievedBy`,`Trans_Type`,`T_Day`,`T_Month`,`T_Year`,`MemberID`,`Trans_Medium`,`RootID`,`ChitGroupId`) VALUES (0," + DualTransactionKey + "," + balayer.ToobjectstrEvenNull(Session["Branchid"]) + ",'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," + Voucher_No + ",'C'," + ddlSubsHeads.SelectedValue.ToString().Trim() + ",'" + dtChoosenDate.ToString("yyyy-MM-dd") + "','" + balayer.ReplaceJnk(txtDescription.Text) + "'," + txtSubAmount.Text + ",'Voucher','" + balayer.ReplaceJnk(txtReceivedBy.Text) + "',0," + dtChoosenDate.Day + "," + dtChoosenDate.Month + "," + dtChoosenDate.Year + "," + memberid + "," + trans_medium + "," + ddlSubsHeads.SelectedValue.ToString() + "," + chitGroupId + ") ";
                                    }
                                    long iResult = 0;
                                    iResult = trn.insertorupdateTrn(strCreditHeadQuery);
                                    if (txtChk.Text.Trim() != "" & txtChk.Visible)
                                    {
                                        //string TransactionKey = balayer.GetSingleValue("SELECT TransactionKey  FROM `svcf`.`voucher` where uuid_from_bin(DualTransactionKey)=uuid_from_bin(" + DualTransactionKey + ") and Head_Id=" + ddlSubsHeads.SelectedValue.Split(',')[1] + " order by TransactionKey desc limit 1;");
                                        string strBankInsertQuery = "insert into transbank(BranchID, TransactionKey, DualTransactionKey, T_Day, T_Month, T_Year, BankHeadID, Head_Id, MemberID, CustomersBankName, DateInCheque, ChequeDDNO, GivenAmount, TotalChequeDDAmount, IsBounced, Trans_Type) values (" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + "," + balayer.ToobjectstrEvenNull((object)iResult) + "," + DualTransactionKey + "," + dtChoosenDate.Day + "," + dtChoosenDate.Month + "," + dtChoosenDate.Year + "," + ddlSubsHeads.SelectedValue.Split(',')[1].ToString().Trim() + ",000,000,'','" + balayer.indiandateToMysqlDate(txtDate.Text) + "'," + txtChk.Text + "," + txtSubAmount.Text + "," + txtSubAmount.Text + ",0,0)";
                                        trn.insertorupdateTrn(strBankInsertQuery);
                                    }
                                }
                                //trans_medium = "0";
                                for (int iTrans = 0; iTrans < GridGuardiansDebit.Rows.Count; iTrans++)
                                {
                                    //trans_medium = "0";
                                    DropDownList ddlSubsHeads = (DropDownList)GridGuardiansDebit.Rows[iTrans].FindControl("ddlHeadsDebit");
                                    if (ddlSubsHeads.SelectedValue.Contains(','))
                                    {
                                        rootid = int.Parse(ddlSubsHeads.SelectedValue.ToString().Split(',')[0].ToString().Trim().Trim(',').Trim());
                                    }
                                    else
                                    {
                                        rootid = int.Parse(ddlSubsHeads.SelectedValue.ToString());
                                    }
                                    if (rootid == 5)
                                    {
                                        if (ddlSubsHeads.SelectedValue.Contains(','))
                                        {
                                            memberid = balayer.GetSingleValue("select `MemberID` from membertogroupmaster where `Head_Id`=" + ddlSubsHeads.SelectedValue.ToString().Split(',')[1].ToString().Trim().Trim(',').Trim());
                                        }
                                        else
                                        {
                                            memberid = balayer.GetSingleValue("select `MemberID` from membertogroupmaster where `Head_Id`=" + ddlSubsHeads.SelectedValue.ToString());
                                        }
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
                                    if (ddlSubsHeads.SelectedValue.Contains(','))
                                    {
                                        NodeID = ddlSubsHeads.SelectedValue.Split(',')[1].Trim();
                                    }
                                    else
                                    {
                                        NodeID = ddlSubsHeads.SelectedValue;

                                    }
                                    // TextBox txtChk = (TextBox)GridGuardians.Rows[iTrans].FindControl("txtChequeNO");
                                    //if (ddlSubsHeads.SelectedValue.ToString().Contains("3,"))
                                    //{
                                    //    trans_medium = "1";
                                    //    //balayer.GetInsertItem("INSERT INTO  `vouchersbank` (`BranchID`,`VoucherSeries`,`VoucherNo`,`AccountNO`,`ChequeAmount` ,`IFSC` ,`ChequeDate` ,`ChequeNO`) VALUES ('" +balayer.ToobjectstrEvenNull(Session["Branchid"]) + "','" + txtSeries.Text + "','" + txtVoucherNo.Text + "','" + ddlSubsHeads.SelectedItem.Text.Split('_')[2] + "','" + txtSubAmount.Text + "','" + "" + "','" + balayer.indiandateToMysqlDate(txtDate.Text) + "','" + txtChk.Text + "')");
                                    //}
                                    string chitGroupId = "0";
                                    if (ddlSubsHeads.SelectedValue.Contains(','))
                                    {
                                        if (int.Parse(ddlSubsHeads.SelectedValue.ToString().Split(',')[0].ToString().Trim().Trim(',').Trim()) == 5)
                                        {
                                            chitGroupId = balayer.GetSingleValue("select ParentID from headstree where NodeID=" + ddlSubsHeads.SelectedValue.ToString().Split(',')[1].ToString());
                                        }
                                    }
                                    if (chitGroupId == "")
                                    {
                                        chitGroupId = "0";
                                    }
                                    if (ddlSubsHeads.SelectedValue.Contains(','))
                                    {
                                        strDebitHeadQuery = "INSERT INTO `svcf`.`voucher` (`Other_Trans_Type`,`DualTransactionKey`,`BranchID`,`CurrDate`,`Voucher_No`,`Voucher_Type`,`Head_Id`,`ChoosenDate`,`Narration`,`Amount`,`Series`,`ReceievedBy`,`Trans_Type`,`T_Day`,`T_Month`,`T_Year`,`MemberID`,`Trans_Medium`,`RootID`,`ChitGroupId`) VALUES (0," + DualTransactionKey + "," + balayer.ToobjectstrEvenNull(Session["Branchid"]) + ",'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," + Voucher_No + ",'D'," + ddlSubsHeads.SelectedValue.Split(',')[1].ToString().Trim() + ",'" + dtChoosenDate.ToString("yyyy-MM-dd") + "','" + balayer.ReplaceJnk(txtDescription.Text) + "'," + txtSubAmount.Text + ",'Voucher','" + balayer.ReplaceJnk(txtReceivedBy.Text) + "',0," + dtChoosenDate.Day + "," + dtChoosenDate.Month + "," + dtChoosenDate.Year + "," + memberid + "," + trans_medium + "," + ddlSubsHeads.SelectedValue.ToString().Split(',')[0] + "," + chitGroupId + ") ";
                                    }
                                    else
                                    {
                                        strDebitHeadQuery = "INSERT INTO `svcf`.`voucher` (`Other_Trans_Type`,`DualTransactionKey`,`BranchID`,`CurrDate`,`Voucher_No`,`Voucher_Type`,`Head_Id`,`ChoosenDate`,`Narration`,`Amount`,`Series`,`ReceievedBy`,`Trans_Type`,`T_Day`,`T_Month`,`T_Year`,`MemberID`,`Trans_Medium`,`RootID`,`ChitGroupId`) VALUES (0," + DualTransactionKey + "," + balayer.ToobjectstrEvenNull(Session["Branchid"]) + ",'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," + Voucher_No + ",'D'," + ddlSubsHeads.SelectedValue.ToString().Trim() + ",'" + dtChoosenDate.ToString("yyyy-MM-dd") + "','" + balayer.ReplaceJnk(txtDescription.Text) + "'," + txtSubAmount.Text + ",'Voucher','" + balayer.ReplaceJnk(txtReceivedBy.Text) + "',0," + dtChoosenDate.Day + "," + dtChoosenDate.Month + "," + dtChoosenDate.Year + "," + memberid + "," + trans_medium + "," + ddlSubsHeads.SelectedValue.ToString() + "," + chitGroupId + ") ";
                                    }
                                    long iResult = 0;
                                    iResult = trn.insertorupdateTrn(strDebitHeadQuery);
                                    if (ddlSubsHeads.SelectedValue.ToString().Contains("3,"))
                                    {
                                        //string TransactionKey = balayer.GetSingleValue("SELECT TransactionKey  FROM `svcf`.`voucher` where uuid_from_bin(DualTransactionKey)=uuid_from_bin(" + DualTransactionKey + ") and Head_Id=" + ddlSubsHeads.SelectedValue.Split(',')[1] + " order by TransactionKey desc limit 1;");
                                        string strBankInsertQuery = "insert into transbank(BranchID, TransactionKey, DualTransactionKey, T_Day, T_Month, T_Year, BankHeadID, Head_Id, MemberID, CustomersBankName, DateInCheque, ChequeDDNO, GivenAmount, TotalChequeDDAmount, IsBounced, Trans_Type) values (" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + "," + balayer.ToobjectstrEvenNull((object)iResult) + "," + DualTransactionKey + "," + dtChoosenDate.Day + "," + dtChoosenDate.Month + "," + dtChoosenDate.Year + "," + ddlSubsHeads.SelectedValue.Split(',')[1].ToString().Trim() + ",000,000,'','" + balayer.indiandateToMysqlDate(txtDate.Text) + "'," + "000" + "," + txtSubAmount.Text + "," + txtSubAmount.Text + ",0,0)";
                                        trn.insertorupdateTrn(strBankInsertQuery);
                                    }
                                }
                                trn.CommitTrn();
                                logger.Info("OBPVoucher.aspx - btnYes_click():  Completed: " + DateTime.Now + " by: " + Convert.ToString(Session["UserName"]) + "");
                            }
                            catch (Exception ex)
                            {
                                try
                                {
                                    trn.RollbackTrn();
                                    logger.Info("OBPVoucher.aspx - btnYes_click():  Error: " + ex.Message + ": " + DateTime.Now + " by: " + Convert.ToString(Session["UserName"]) + "");
                                }
                                catch
                                {
                                }
                                finally
                                {
                                    btnYes.Focus();
                                    isFailed = true;
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
                this.Response.Redirect(this.Request.Url.AbsolutePath.ToString());
            }
            else
                if (lblHint.Text == "VExist")
                {
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

        protected void ddl_Branch_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddlBranch = ((DropDownList)sender);
            string qry = "select Head_Id, GROUPNO from groupmaster where BranchID=" + ddlBranch.SelectedValue + "";
            DataTable dtgroupno = null;
            dtgroupno = balayer.GetDataTable(qry);
            DataRow dr = dtgroupno.NewRow();
            dr[0] = "0";
            dr[1] = "--Select--";
            DropDownList ddlgroup = (DropDownList)(((DropDownList)sender).Parent.Parent.FindControl("ddl_group"));
            ddlgroup.DataValueField = "Head_Id";
            ddlgroup.DataTextField = "GROUPNO";
            dtgroupno.Rows.InsertAt(dr, 0);
            ddlgroup.DataSource = dtgroupno;
            ddlgroup.DataBind();

        }

        protected void ddl_group_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddlGroup = ((DropDownList)sender);
            string qry = "SELECT Head_Id,GrpMemberID FROM membertogroupmaster where GroupID=" + ddlGroup.SelectedValue + "";
            DataTable dttokenList = null;
            dttokenList = balayer.GetDataTable(qry);
            DataRow dr = dttokenList.NewRow();
            dr[0] = "0";
            dr[1] = "--Select--";
            DropDownList ddlToken = (DropDownList)(((DropDownList)sender).Parent.Parent.FindControl("ddlHeads"));
            ddlToken.DataValueField = "Head_Id";
            ddlToken.DataTextField = "GrpMemberID";
            dttokenList.Rows.InsertAt(dr, 0);
            ddlToken.DataSource = dttokenList;
            ddlToken.DataBind();
        }


    }
}