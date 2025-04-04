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
using SVCF_BusinessAccessLayer;
using SVCF_TransactionLayer;
using SVCF_DataAccessLayer;
using log4net;
using log4net.Config;
using System.Collections.Generic;

namespace SreeVisalamChitFundLtd_phase1
{
    public partial class paymentnew : System.Web.UI.Page
    {
        #region VarDeclaration
      
        string mindate1, mindate2, mindate3;
        string query = "";
        string userinfo = "";
        string qry = "";
        string usrRole = "";
        string memid = "";
        #endregion

        #region VarDeclaration
        BusinessLayer balayer = new BusinessLayer();
        TransactionLayer tranlayer = new TransactionLayer();
        #endregion
        ILog logger = log4net.LogManager.GetLogger(typeof(paymentnew));

        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (Session["UserName"] == null || Session["Branchid"] == null || Session["BranchName"] == null)
            {
                Response.Redirect(Page.ResolveUrl("~/Login.aspx"), true);
            }
        }
        protected void bindHeads(DropDownList ddlHeads)
        {
            string GetMemberId = balayer.GetSingleValue("SELECT MemberID FROM svcf.membertogroupmaster where Head_Id='" + ddlMemberName.SelectedValue + "'");
            DataTable dtAllHeads = balayer.GetDataTable("SELECT GrpMemberID,concat(cast(Head_Id as char),'|',5) as Head_Id FROM svcf.membertogroupmaster where BranchID= "+ Session["Branchid"]);
            DataTable dtgrphd = balayer.GetDataTable("SELECT concat(g1.GROUPNO,'|',g1.GROUPNO) as GrpMemberID,concat(cast(g1.Head_Id as char),'|',5) as Head_Id FROM groupmaster as g1 where g1.BranchID=" + Session["Branchid"]);
            DataTable dtBranches = balayer.GetDataTable("SELECT B_Name AS GrpMemberID,concat(Head_id,'|',1) AS Head_Id FROM svcf.branchdetails ;");
            //DataTable dtRCM = balayer.GetDataTable("SELECT Node As GrpMemberID,concat(NodeID ,'|',5) AS Head_Id FROM svcf.headstree where NodeID between 44 and 45");
            DataTable dtRCM = balayer.GetDataTable("SELECT TREE as GrpMemberID,concat(cast(TreeID as char),'|',cast(RootID as char)) as Head_Id FROM svcf.view_parent where Tree like '%CHITS>>Removed Chit members Account%' and  BranchID=" + Session["Branchid"]);          
            DataTable dtpandl = balayer.GetDataTable("SELECT TREE as GrpMemberID,concat(cast(TreeID as char),'|',cast(RootID as char)) as Head_Id FROM svcf.view_parent where Tree like '%PROFIT AND LOSS ACCOUNT%' ;");
            dtRCM.Merge(dtpandl);
            dtBranches.Merge(dtRCM);
            dtgrphd.Merge(dtBranches);
            dtAllHeads.Merge(dtgrphd);

            DataRow dr = dtAllHeads.NewRow();
            dr[0] = "--Select--";
            dr[1] = "--Select--";
            dtAllHeads.Rows.InsertAt(dr, 0);
            ddlHeads.DataSource = dtAllHeads;
            ddlHeads.DataTextField = "GrpMemberID";
            ddlHeads.DataValueField = "Head_Id";
            ddlHeads.DataBind();
        }
        protected void GridGuardians_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DropDownList ddlHeads = (DropDownList)e.Row.FindControl("ddlRangeHeads");
                if (e.Row.RowIndex > 0)
                {
                    DataTable dtAllHeads = (DataTable)((DropDownList)GridGuardians.Rows[0].FindControl("ddlRangeHeads")).DataSource;
                    ddlHeads.DataSource = dtAllHeads;
                    ddlHeads.DataTextField = "GrpMemberID";
                    ddlHeads.DataValueField = "Head_Id";
                    ddlHeads.DataBind();
                }
                else
                {
                    bindHeads(ddlHeads);
                }
                ddlHeads.Items.FindByText(((Label)e.Row.FindControl("lblHeads")).Text).Selected = true;
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                ImageButton imgBtnRemove = (e.Row.FindControl("imgBtnRemove") as ImageButton);
                scm1.RegisterAsyncPostBackControl(imgBtnRemove);
            }
        }
        protected void btnAdd_GridGuardians_RowCommand_click(object sender, ImageClickEventArgs e)
        {
            Page.Validate("GrpRow");
            if (!Page.IsValid)
            {
                return;
            }
            GridViewRow gridRow = (GridViewRow)(((ImageButton)sender).Parent.Parent);
            int RowIndex = gridRow.RowIndex;
            DataTable dt = new DataTable();
            DataColumn dcHead = new DataColumn("Heads", typeof(string));
            DataColumn dcAmount = new DataColumn("Amount", typeof(string));
            DataColumn dcDescription = new DataColumn("Description", typeof(string));
            dt.Columns.Add(dcDescription);
            dt.Columns.Add(dcHead);
            dt.Columns.Add(dcAmount);
            foreach (GridViewRow dr in GridGuardians.Rows)
            {
                DataRow dtrow = dt.NewRow();
                DropDownList ddlRangeHeads = (DropDownList)dr.FindControl("ddlRangeHeads");
                dtrow["Heads"] = ddlRangeHeads.SelectedItem.Text;
                TextBox txtRangeAmount = (TextBox)dr.FindControl("txtRangeAmount");
                dtrow["Amount"] = txtRangeAmount.Text;
                TextBox txtRange = (TextBox)dr.FindControl("txtRange");
                dtrow["Description"] = txtRange.Text;
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
                    DropDownList ddlRangeHeads = (DropDownList)dr.FindControl("ddlRangeHeads");
                    TextBox txtRangeAmount = (TextBox)dr.FindControl("txtRangeAmount");
                    TextBox txtRange = (TextBox)dr.FindControl("txtRange");
                    ddlRangeHeads.TabIndex = ++currentTabIndex;
                    txtRangeAmount.TabIndex = ++currentTabIndex;
                    txtRange.TabIndex = ++currentTabIndex;
                }
            }
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
            dt1.Columns.Add(dcDescription);
            dt1.Columns.Add(dcHead);
            dt1.Columns.Add(dcAmount);
            short i = 0;
            foreach (GridViewRow dr in GridGuardians.Rows)
            {
                i += 1;
                DataRow dtrow = dt1.NewRow();
                DropDownList ddlRangeHeads = (DropDownList)dr.FindControl("ddlRangeHeads");
                dtrow["Heads"] = ddlRangeHeads.SelectedItem.Text;
                TextBox txtRangeAmount = (TextBox)dr.FindControl("txtRangeAmount");
                dtrow["Amount"] = txtRangeAmount.Text;
                TextBox txtRange = (TextBox)dr.FindControl("txtRange");
                dtrow["Description"] = txtRange.Text;
                ImageButton showbutton = (ImageButton)dr.FindControl("imgBtnAdd");
                dt1.Rows.Add(dtrow);
            }
            dt1.Rows.RemoveAt(GridGuardians.Rows.Count - 1);
            GridGuardians.DataSource = dt1;
            GridGuardians.DataBind();
            SetGridTabIndex(true);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            pnlmsg.Visible = false;
            if (!IsPostBack)
            {
                userinfo = HttpContext.Current.User.Identity.Name;
                qry = "select rs.name from roles as rs inner join rights as rt on (rt.roleid=rs.id) where memberid=" + userinfo + "";
                usrRole = balayer.GetSingleValue(qry);
                if (usrRole == "Report")
                {
                    Response.Redirect(Page.ResolveUrl("~/Home.aspx"), true);
                }
                //rvDate.MaximumValue = DateTime.Now.ToString("dd/MM/yyyy");
                //rvDate.MinimumValue = balayer.GetSingleValue("SELECT DATE_FORMAT( min(MinimumDate), '%d/%m/%Y') MinimumDate FROM svcf.restrictionmaster where BranchID=" + Session["Branchid"]);

                string voucherno = "";
                voucherno = balayer.GetSingleValue("SELECT max(Voucher_No) FROM svcf.voucher where  BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and Trans_Type=2 and Series='PAYMENT'");
                txtPaymentNumber.Text = Convert.ToString(Convert.ToInt32(voucherno) + 1);
             
                Session["CheckRefresh"] = System.Guid.NewGuid().ToString();
                clear();
                txtPaymentonDate.ToolTip = "Ex. "+DateTime.Now.ToString("dd/MM/yyyy")+" (dd/mm/yyyy)";
                txtDate.ToolTip = "Ex. " + DateTime.Now.ToString("dd/MM/yyyy") + " (dd/mm/yyyy)";
                txtApplyedOn.ToolTip = "Ex. " + DateTime.Now.ToString("dd/MM/yyyy") + " (dd/mm/yyyy)";
                this.BindData();
                SetDefaultRow();

               // txtGurantor.Visible = false;
            }
            rvDate.MaximumValue = DateTime.Now.ToString("dd/MM/yyyy");
            rvDate.MinimumValue = balayer.GetSingleValue("SELECT DATE_FORMAT( min(MinimumDate), '%d/%m/%Y') MinimumDate FROM svcf.restrictionmaster where BranchID=" + Session["Branchid"]);
        }
        public void SetDefaultRow()
        {
            DataTable dt = new DataTable();
            DataColumn dcHeads = new DataColumn("Heads", typeof(string));
            DataColumn dcAmount = new DataColumn("Amount", typeof(string));
            DataColumn dcDescription = new DataColumn("Description", typeof(string));
            dt.Columns.Add(dcDescription);
            dt.Columns.Add(dcAmount);
            dt.Columns.Add(dcHeads);
            DataRow newRow = dt.NewRow();
            newRow["Heads"] = "--Select--";
            newRow["Amount"] = "";
            dt.Rows.Add(newRow);
            GridGuardians.DataSource = dt;
            GridGuardians.DataBind();
        }
        protected void Page_PreRender(object sender, EventArgs e)
        {
            ViewState["CheckRefresh"] = Session["CheckRefresh"];
        }
        public void clear()
        {
            //Default Interest 31/07/2019
            //txtDefault.Text="0.00";
            //Default Interest 31/07/2019
            txtSeries.Text = "PAYMENT";
            //txtPaymentNumber.Text = "";
            LabelPrizedAmount.Text = "Member Name";
            txtPaymentNumber.Focus();
            txtDate.Text = "";
            txtBankAmount.Text = "0.00";
            txtChitKasarAmount.Text = "0.00";
            txtCommision.Text = "0.00";
            txtLoanAmount.Text = "0.00";
            txtIncidentialCharge.Text = "0.00";
            txtGST.Text = "0.00";
            txtCgst.Text = "0.00";
            txtSgst.Text = "0.00";
            txtCgst.Text = "0.00";

           // txtServiceCharge.Text = "0.00";
            txtDebitAmount.Text = "0.00";
            txtLoanInterest.Text = "0.00";
            ddlMemberName.Items.Clear();
            txtChequeDDno.Text = "";
            txtDrawNumber.Text = "";
            txtPaymentonDate.Text = "";
            txtDescription.Text = "";
            txtApplyedOn.Text = "";
            getGroup();
            getBranch();

            Docnotxt.Text = "";
            InFavtxt.Text = "";
            Propvaltxt.Text = "";
          //  DropRegistmode.SelectedItem.Text = "";
            txtGurantor.Text = "";
            txtDrawNumber.Text = "";
        }
        private void BindData()
        {
            DataTable dtSelect = new DataTable();
            DataTable dt = new DataTable();
            DataColumn dcHeads = new DataColumn("Heads", typeof(string));
            DataColumn dcAmount = new DataColumn("Amount", typeof(string));
            DataColumn dcDescription = new DataColumn("Description", typeof(string));
            dt.Columns.Add(dcDescription);
            dt.Columns.Add(dcAmount);
            dt.Columns.Add(dcHeads);
            GridGuardians.DataSource = dt;
            GridGuardians.DataBind();
        }
        public void getGroup()
        {
            DataTable dtchitgrpno = balayer.GetDataTable("Select distinct(groupmaster.GROUPNO) ,auctiondetails.GroupID from auctiondetails join groupmaster on (auctiondetails.GroupID=groupmaster.Head_Id) where auctiondetails.IsPrized='N'and auctiondetails.BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + "");
            DataRow drchitgrpno = dtchitgrpno.NewRow();
            drchitgrpno[0] = "--Select--";
            drchitgrpno[1] = "0";
            ddlGroupNumber.DataSource = dtchitgrpno;
            ddlGroupNumber.DataTextField = "GROUPNO";
            ddlGroupNumber.DataValueField = "GroupID";
            dtchitgrpno.Rows.InsertAt(drchitgrpno, 0);
            ddlGroupNumber.DataBind();
        }

        protected void txtPaymentNumber_Validate(object sender, ServerValidateEventArgs e)
        {
            //DataTable ssss = balayer.GetDataTable("SELECT Voucher_No FROM svcf.voucher where Voucher_No="+txtPaymentNumber.Text+" and BranchID="+balayer.ToobjectstrEvenNull(Session["Branchid"])+" and Trans_Type=2");
            //string voucherno = "";
            //if (ssss.Rows.Count > 0)
            //{
            //    voucherno = balayer.GetSingleValue("SELECT max(Voucher_No) FROM svcf.voucher where  BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and Trans_Type=2");

            //    CustomValidator2.ErrorMessage = "Already exists";
            //    e.IsValid = false;
            //}
            //else
            //    e.IsValid = true;
        }
        
        protected void load_ddlGroupNumber(object sender, EventArgs e)
        {
            if (ddlGroupNumber.SelectedItem.Value != "0")
            {
                var commission_ID = balayer.GetSingleValue("select Commission_ID from svcf.groupmaster where Head_Id=" + ddlGroupNumber.SelectedItem.Value + "");
                decimal chitValue = Convert.ToDecimal(balayer.GetSingleValue("SELECT ChitValue FROM svcf.groupmaster where Head_Id=" + ddlGroupNumber.SelectedItem.Value + ""));
                txtDebitAmount.Text = chitValue.ToString();
                DataTable dtAmount = balayer.GetDataTable("SELECT Commission,IncidentalCharges,ServiceTax,if(GstAmount>0 and GstAmount is not null,GstAmount,0) as GstAmount, "+
                    "if(CgstAmount>0 and CgstAmount is not null,CgstAmount,0) as CgstAmount,if(SgstAmount>0 and SgstAmount is not null,SgstAmount,0) as SgstAmount,if(IgstAmount>0 and IgstAmount is not null,IgstAmount,0) as IgstAmount" +
                    " FROM svcf.commissiondetails where chitValue=" + chitValue + " and Commission_ID='" + commission_ID + "'");
                txtCommision.Text = dtAmount.Rows[0][0].ToString();
                txtIncidentialCharge.Text = dtAmount.Rows[0][1].ToString();
               // txtServiceCharge.ToolTip = dtAmount.Rows[0][2].ToString();
               // txtServiceCharge.Text = "0.00";            
                txtGST.Text = dtAmount.Rows[0][3].ToString();
                txtSgst.Text = dtAmount.Rows[0]["SgstAmount"].ToString();
                txtCgst.Text = dtAmount.Rows[0]["CgstAmount"].ToString();
                txtIgst.Text = dtAmount.Rows[0]["IgstAmount"].ToString();



                DataTable dtchit = balayer.GetDataTable("SELECT DISTINCT auctiondetails.PrizedMemberID, concat(membertogroupmaster.GrpMemberID,'=', membertogroupmaster.MemberName) as MemberName from auctiondetails inner join membertogroupmaster on auctiondetails.PrizedMemberID=membertogroupmaster.Head_Id where auctiondetails.GroupID=" + ddlGroupNumber.SelectedItem.Value + " and auctiondetails.IsPrized='N'");
                ddlMemberName.DataSource = dtchit;
                ddlMemberName.DataTextField = "MemberName";
                ddlMemberName.DataValueField = "PrizedMemberID";
                ddlMemberName.DataBind();
                ddlMemberName.Focus();
                ListItem lst1 = new ListItem("--Select--", "--Select--");
                ddlMemberName.Items.Insert(0, lst1);
                txtDrawNumber.Text = "";
                txtChitKasarAmount.Text = "0.00";
                txtBankAmount.Text = "0.00";
                ddlBankName.SelectedIndex = 0;
            }
            else
            {
                txtDebitAmount.Text = "0.00";
                txtCommision.Text="0.00";
                txtIncidentialCharge.Text = "0.00";
                txtGST.Text = "0.00";
                txtSgst.Text = "0.00";
                txtCgst.Text = "0.00";
                txtIgst.Text = "0.00";
                //txtServiceCharge.ToolTip = "0.00";
               // txtServiceCharge.ToolTip = "0.00";
                ddlMemberName.DataSource = null;
                ddlMemberName.Items.Clear();
                ddlMemberName.DataBind();
                txtDrawNumber.Text = "";
                txtChitKasarAmount.Text = "0.00";
                txtBankAmount.Text = "0.00";
            }
        }
        protected void load_ddlMemberName(object sender, EventArgs e)
        {
            decimal prizedamnt=0;
            decimal totcommission=0;
            if (ddlGroupNumber.SelectedItem.Text != "--Select--")
            {
                if (ddlMemberName.SelectedItem.Text != "--Select--")
                {
                    int DrawNo = Convert.ToInt32(balayer.GetSingleValue("SELECT DrawNO FROM svcf.auctiondetails where PrizedMemberID=" + ddlMemberName.SelectedValue + ""));
                    txtDrawNumber.Text = DrawNo.ToString();
                    LabelPrizedAmount.Text = (ddlMemberName.SelectedItem.Text).Split('=')[0];
                    decimal chitValue = Convert.ToDecimal(balayer.GetSingleValue("SELECT ChitValue FROM svcf.groupmaster where Head_Id=" + ddlGroupNumber.SelectedItem.Value + ""));
                    var commission_ID = balayer.GetSingleValue("select Commission_ID from svcf.groupmaster where Head_Id=" + ddlGroupNumber.SelectedItem.Value + "");
                    txtDebitAmount.Text = chitValue.ToString();
                    decimal commission = Convert.ToDecimal(balayer.GetSingleValue("SELECT Commission FROM svcf.commissiondetails where chitValue=" + chitValue + " and Commission_ID='" + commission_ID + " '"));
                    decimal GetKasarAmount = Convert.ToDecimal(balayer.GetSingleValue("SELECT KasarAmount FROM svcf.auctiondetails where PrizedMemberId=" + ddlMemberName.SelectedItem.Value + ""));
                    
                    decimal decDefault=Convert.ToDecimal( balayer.GetSingleValue("SELECT DefaultInterest FROM svcf.auctiondetails where PrizedMemberId=" + ddlMemberName.SelectedItem.Value + ""));
                    //Default Interest 31/07/2019
                    //   txtDefault.Text = decDefault.ToString();
                    //Default Interest 31/07/2019
                    //   decimal kasar = GetKasarAmount - commission - decDefault;
                    prizedamnt = Convert.ToDecimal(balayer.GetSingleValue("SELECT PrizedAmount FROM svcf.auctiondetails where PrizedMemberID=" + ddlMemberName.SelectedValue + ""));
                    totcommission=Convert.ToDecimal(balayer.GetSingleValue("SELECT TotalCommission FROM svcf.auctiondetails where PrizedMemberID=" + ddlMemberName.SelectedValue + ""));
                    decimal kasar = (chitValue - prizedamnt) - totcommission;
                    
                    int totalMembers = Convert.ToInt32(balayer.GetSingleValue("SELECT `groupmaster`.`NoofMembers` FROM svcf.groupmaster where Head_Id=" + ddlGroupNumber.SelectedItem.Value + ""));
                    txtChitKasarAmount.Text = kasar.ToString();
                    txtChitKasarAmount.ToolTip = (kasar / totalMembers).ToString();
                    lbTool.ToolTip = totalMembers.ToString();

                    decimal inCharge = Convert.ToDecimal(balayer.GetSingleValue("SELECT IncidentalCharges FROM svcf.commissiondetails where chitValue=" + chitValue.ToString()+ " and Commission_ID='" + commission_ID + " '"));
                    decimal GSTCharges = Convert.ToDecimal(balayer.GetSingleValue("SELECT sum(CgstAmount+SgstAmount) FROM svcf.commissiondetails where chitValue=" + chitValue.ToString()+ " and Commission_ID='" + commission_ID + " '"));
                    lbFuture.Text = (chitValue - commission - kasar - decDefault - inCharge - GSTCharges).ToString();
                    txtBankAmount.Text = (chitValue - commission - kasar - decDefault - inCharge - GSTCharges).ToString();
                    this.BindData();
                    SetDefaultRow();
                }
                else
                {
                    txtDrawNumber.Text = "";
                    LabelPrizedAmount.Text = "";
                    txtDebitAmount.Text = "0.00";
                    txtChitKasarAmount.Text = "0.00";
                    txtChitKasarAmount.ToolTip = "0.00";
                    lbTool.ToolTip = "";
                    lbFuture.Text = "0.00";
                    txtBankAmount.Text = "0.00";
                    //Default Interest 31/07/2019
                    // txtDefault.Text = "0.00";
                    //Default Interest 31/07/2019
                }
            }
        }

        public void getBranch()
        {
            //DataTable dtNode = balayer.GetDataTable(" SELECT concat( concat(t1.BankName,'_',t1.IFCCode),'_',t1.AccountNo) as BankDetails, t1.Head_Id as Head_Id, t1.Banklocation as Banklocation FROM svcf.bankdetails as t1 join headstree as t2 on (t1.Head_Id=t2.NodeId) where t1.BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + "");
            DataTable dtNode = balayer.GetDataTable(" SELECT concat( concat(t1.BankName,'_',t1.IFCCode),'_',t1.AccountNo) as BankDetails, t1.Head_Id as Head_Id, t1.Banklocation as Banklocation FROM svcf.bankdetails as t1 join headstree as t2 on (t1.Head_Id=t2.NodeId) where t1.BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) 
                    + " UNION select Node as BankDetails,NodeId as Head_Id,'' AS Banklocation from headstree where NodeID in (47,48);");
            DataRow drNode;
            drNode = dtNode.NewRow();
            drNode[0] = "--Select--";
            drNode[1] = "0";
            dtNode.Rows.InsertAt(drNode, 0);
            ddlBankName.DataSource = dtNode;
            ddlBankName.DataTextField = "BankDetails";
            ddlBankName.DataValueField = "Head_Id";
            ddlBankName.DataBind();
        }
        protected void LoanAmount_OnTextChanged(object sender, EventArgs e)
        {
            decimal lon = 0;
            bool islon = decimal.TryParse(txtLoanAmount.Text, out lon);
            if (!islon)
            {
                txtLoanAmount.Text = "0.00";
            }

            txtBankAmount.Text=( Convert.ToDecimal( lbFuture.Text )-(Convert.ToDecimal( txtLoanAmount.Text))).ToString();
        }
        protected void KasarAmount_OnTextChanged(object sender, EventArgs e)
        {
            decimal lon = 0;
            bool islon = decimal.TryParse(txtChitKasarAmount.Text, out lon);
            if (!islon)
            {
                txtChitKasarAmount.Text = "0.00";
            }
            int totalMembers = Convert.ToInt32(balayer.GetSingleValue("SELECT `groupmaster`.`NoofMembers` FROM svcf.groupmaster where Head_Id=" + ddlGroupNumber.SelectedItem.Value + ""));
            txtChitKasarAmount.ToolTip = (Convert.ToDecimal(txtChitKasarAmount.Text) / totalMembers).ToString();
            lbTool.ToolTip = totalMembers.ToString();
        }

        protected void GR_Type_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (GR_Type.Items[0].Selected == true) txtGurantor.Visible = true;
            else txtGurantor.Visible = false;
        }

        protected void load_Payment(object sender, EventArgs e)
        {
            Page.Validate("pa");
            Page.Validate("GrpRow");
            if (!Page.IsValid)
            {
                return;
            }
            if (Session["CheckRefresh"].ToString() != ViewState["CheckRefresh"].ToString())
            {
                return;
            }
            decimal ser = 0;
            //bool isser = decimal.TryParse(txtServiceCharge.Text, out ser);
            //if (!isser)
            //{
            //    txtServiceCharge.Text = "0.00";
            //}
            decimal ban = 0;
            bool isban = decimal.TryParse(txtBankAmount.Text, out ban);
            if (!isban)
            {
                txtBankAmount.Text = "0.00";
            }
            decimal inc = 0;
            bool isinc = decimal.TryParse(txtIncidentialCharge.Text, out inc);
            if (!isinc)
            {
                txtIncidentialCharge.Text = "0.00";
            }
            decimal gst = 0;
            bool isgst = decimal.TryParse(txtGST.Text, out gst);
            if (!isgst)
            {
                txtGST.Text = "0.00";
            }

            decimal cgst = 0;
            bool iscgst = decimal.TryParse(txtCgst.Text, out cgst);
            if (!iscgst)
            {
                txtCgst.Text = "0.00";
            }

            decimal sgst = 0;
            bool issgst = decimal.TryParse(txtSgst.Text, out sgst);
            if (!issgst)
            {
                txtSgst.Text = "0.00";
            }

            decimal igst = 0;
            bool isigst = decimal.TryParse(txtIgst.Text, out igst);
            if (!isigst)
            {
                txtIgst.Text = "0.00";
            }

            decimal com = 0;
            bool iscom = decimal.TryParse(txtCommision.Text, out com);
            if (!iscom)
            {
                txtCommision.Text = "0.00";
            }
            decimal kas = 0;
            bool iskas = decimal.TryParse(txtChitKasarAmount.Text, out kas);
            if (!iskas)
            {
                txtChitKasarAmount.Text = "0.00";
            }
            decimal lon = 0;
            bool islon = decimal.TryParse(txtLoanAmount.Text, out lon);
            if (!islon)
            {
                txtLoanAmount.Text = "0.00";
            }
            decimal future = 0;
            decimal futurecall = 0;
            for (int iTrans = 0; iTrans < GridGuardians.Rows.Count; iTrans++)
            {
                // trans_medium = "0";
                TextBox txtRangeAmount = (TextBox)GridGuardians.Rows[iTrans].FindControl("txtRangeAmount");
                bool isfuture = decimal.TryParse(txtRangeAmount.Text, out future);
                if (!isfuture)
                {
                    txtRangeAmount.Text = "0.00";
                }
                else
                {
                    futurecall += Convert.ToDecimal(txtRangeAmount.Text);
                }
            }
            
            decimal li = 0;
            //bool isli = decimal.TryParse(txtServiceCharge.Text, out li);
            //if (!isli)
            //{
            //    txtLoanInterest.Text = "0.00";
            //}
            decimal de = 0;
            //Default Interest 31/07/2019
            //bool isde = decimal.TryParse(txtDefault.Text, out de);
            //if (!isde)
            //{
            //    txtDefault.Text = "0.00";
            //}
            //Default Interest 31/07/2019


            //decimal Credit = Convert.ToDecimal(txtServiceCharge.Text) + Convert.ToDecimal(txtBankAmount.Text) + Convert.ToDecimal(txtChitKasarAmount.Text) + Convert.ToDecimal(txtCommision.Text) + Convert.ToDecimal(txtIncidentialCharge.Text) + Convert.ToDecimal(txtLoanAmount.Text) + Convert.ToDecimal(futurecall) + Convert.ToDecimal(txtLoanInterest.Text) + Convert.ToDecimal(txtDefault.Text) + Convert.ToDecimal(txtGST.Text);
            //Default Interest 31/07/2019
            //decimal Credit = Convert.ToDecimal(txtBankAmount.Text) + Convert.ToDecimal(txtChitKasarAmount.Text) + Convert.ToDecimal(txtCommision.Text) + Convert.ToDecimal(txtIncidentialCharge.Text) + Convert.ToDecimal(txtLoanAmount.Text) + Convert.ToDecimal(futurecall) + Convert.ToDecimal(txtLoanInterest.Text) + Convert.ToDecimal(txtDefault.Text) + Convert.ToDecimal(txtGST.Text) + Convert.ToDecimal(txtSgst.Text) + Convert.ToDecimal(txtCgst.Text) + Convert.ToDecimal(txtIgst.Text);
            //Default Interest 31/07/2019
            decimal Credit = Convert.ToDecimal(txtBankAmount.Text) + Convert.ToDecimal(txtChitKasarAmount.Text) + Convert.ToDecimal(txtCommision.Text) + Convert.ToDecimal(txtIncidentialCharge.Text) + Convert.ToDecimal(txtLoanAmount.Text) + Convert.ToDecimal(futurecall) + Convert.ToDecimal(txtLoanInterest.Text) + Convert.ToDecimal(txtGST.Text) + Convert.ToDecimal(txtSgst.Text) + Convert.ToDecimal(txtCgst.Text) + Convert.ToDecimal(txtIgst.Text);
            decimal Debit = Convert.ToDecimal(txtDebitAmount.Text);
            if (Credit != Debit)
            {
                ModalPopupExtender1.PopupControlID = "pnlmsg";
                ModalPopupExtender1.Show();
                pnlmsg.Visible = true;
                lblh.Text = "Status";
                lblcon.Text = "Credit=" + Credit + " and Debit=" + Debit + " Should not be same!!!";
                lblcon.ForeColor = System.Drawing.Color.Red;
                BtnOK.Visible = false;
                Button2.Visible = false;
                Button3.Visible = false;
                btnHide.Visible = true;
            }
            else
            {
                ModalPopupExtender1.PopupControlID = "pnlmsg";
                ModalPopupExtender1.Show();
                pnlmsg.Visible = true;
                lblh.Text = "Status";
                lblcon.Text = "Do you want to insert the payment?";
                lblcon.ForeColor = System.Drawing.Color.Green;
                BtnOK.Visible = true;
                Button2.Visible = false;
                Button3.Visible = true;
                btnHide.Visible = false;
            }
        }
        protected void load_hidePanel(object sender, EventArgs e)
        {
            pnlmsg.Visible = false;
            ModalPopupExtender1.Hide();
        }
        protected void load_cancel(object sender, EventArgs e)
        {
            Response.Redirect(Request.Url.AbsolutePath.ToString());
        }
        protected static string indianToMysqlDate(string ddmmyy)
        {
            if (string.IsNullOrEmpty(ddmmyy))
            {
                ddmmyy = "";
            }
            string strAuctionDate = "";
            if (ddmmyy.Trim() == "")
            {
                strAuctionDate = "";
            }
            else
            {
                strAuctionDate = ddmmyy.Split('/')[2] + "-" + ddmmyy.Split('/')[1] + "-" + ddmmyy.Split('/')[0];
            }
            return strAuctionDate;
        }

        protected void Guarontorsinglejoinchanged(object sender, EventArgs e)
        {
            //if (Guajoin.Checked == true)
            //{
            //    txtGurantor.Visible = true;
            //}
            //if (Guasingle.Checked == true)
            //{
            //    txtGurantor.Visible = false;
            //}
        }

        protected void btn_ok(object sender, EventArgs e)
        {
            //Response.Redirect(Request.Url.AbsolutePath.ToString());
            TransactionLayer trn = new TransactionLayer();
            string strNarration = "";
            string paymentNarration = "";
            string futureCallNarration = "";
            string DualTransactionKey = "";
            string DrawNo = "";
            string qry = "";
            List<string> GetMemList = new List<string>();
            bool getvc = false;
            try
            {
                strNarration = balayer.MySQLEscapeString(ddlMemberName.SelectedItem.Text).Split('=')[0] + "-" + ddlMemberName.SelectedItem.Text.Split('=')[1] + ": through payment of Draw Number :" + txtDrawNumber.Text;
                paymentNarration = balayer.MySQLEscapeString(ddlMemberName.SelectedItem.Text).Split('=')[0] + "  - Draw Number :" + txtDrawNumber.Text;
                futureCallNarration = balayer.MySQLEscapeString(ddlMemberName.SelectedItem.Text).Split('=')[0] + " Chit: " + txtDrawNumber.Text + "th Draw : Payment adjustd to:";

                System.Guid guid = Guid.NewGuid();
                string guidForChar36 = guid.ToString();
                string hexstring = BitConverter.ToString(guid.ToByteArray());
                string guidForBinary16 = "0x" + hexstring.Replace("-", string.Empty);
                //13/06/2023

                DualTransactionKey = guidForBinary16;





                //stored procedure for DualTransactionKey
                //DualTransactionKey = Convert.ToString(balayer.sp_gendratedt_key());

                string BranchHeadId = ddlBankName.SelectedItem.Value;
                string GetHeadId = balayer.GetSingleValue("SELECT Head_Id FROM svcf.membertogroupmaster where Head_Id=" + ddlMemberName.SelectedItem.Value + "");
                string GetMemberId = balayer.GetSingleValue("SELECT MemberID FROM svcf.membertogroupmaster where Head_Id=" + ddlMemberName.SelectedItem.Value + "");
                long GetCommision = trn.insertorupdateTrn("insert into voucher (`DualTransactionKey`,`BranchID`,`CurrDate`,`Voucher_No`,`Voucher_Type`,`Head_Id`,`ChoosenDate`,`Narration`,`Amount`,`Series`,`ReceievedBy`,`Trans_Type`,`T_Day`,`T_Month`,`T_Year`,`MemberID`,`Trans_Medium`,`RootID`,`ChitGroupId`,`Other_Trans_Type`) values (" + DualTransactionKey + "," + balayer.ToobjectstrEvenNull(Session["Branchid"]) + ",'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," + txtPaymentNumber.Text + ",'C',64,'" + balayer.indiandateToMysqlDate(txtDate.Text) + "','Payment for " + paymentNarration + "'," + txtCommision.Text + ",'" + txtSeries.Text + "','" + balayer.MySQLEscapeString(balayer.ToobjectstrEvenNull(Session["UserName"])) + "',2," + balayer.indiandateToMysqlDate(txtDate.Text).Split('/')[2] + "," + balayer.indiandateToMysqlDate(txtDate.Text).Split('/')[1] + "," + balayer.indiandateToMysqlDate(txtDate.Text).Split('/')[0] + "," + GetMemberId + ",0,11," + ddlGroupNumber.SelectedItem.Value + ",0) ");
                Decimal dblLoan;
                if (decimal.TryParse(txtLoanAmount.Text, out dblLoan))
                {
                    if (dblLoan > 0)
                    {
                        string strGROUP = balayer.GetSingleValue("select Node from headstree where NodeID=" + GetHeadId);

                        string strMemberName = balayer.GetSingleValue("select MemberName from membertogroupmaster where Head_Id=" + GetHeadId);

                        string strLoanHead = balayer.GetSingleValue("SELECT HeadId FROM svcf.chitheads where ParentID=53 and ChitName='" + strGROUP + "';");
                        long iresult = 53;
                        if (string.IsNullOrEmpty(strLoanHead))
                        {
                            iresult = trn.insertorupdateTrn("insert into headstree(ParentID, Node, Branchid) values(53,'" + balayer.MySQLEscapeString(strGROUP) + " " + balayer.MySQLEscapeString(strMemberName) + "'," + Session["Branchid"] + ")");
                            string str = balayer.GetSingleValue("select TreeHint from headstree where NodeID=" + 53);

                            trn.insertorupdateTrn("update headstree set TreeHint='" + str + "," + iresult + "' where NodeID=" + iresult);

                            trn.insertorupdateTrn("insert into chitheads(`ChitNumber`,`ChitName`,`MemberID`,`MemberName`,`BranchID`,`ParentID`,HeadId) values (" + GetHeadId + ",'" + balayer.MySQLEscapeString(strGROUP) + "'," + GetMemberId + ",'" + balayer.MySQLEscapeString(strMemberName) + "'," + Session["Branchid"] + ",53," + iresult + ")");
                        }
                        else
                        {
                            iresult = long.Parse(strLoanHead);
                        }
                        long GetLoanAmount = trn.insertorupdateTrn("insert into voucher (`DualTransactionKey`,`BranchID`,`CurrDate`,`Voucher_No`,`Voucher_Type`,`Head_Id`,`ChoosenDate`,`Narration`,`Amount`,`Series`,`ReceievedBy`,`Trans_Type`,`T_Day`,`T_Month`,`T_Year`,`MemberID`,`Trans_Medium`,`RootID`,`ChitGroupId`,`Other_Trans_Type`) values (" + DualTransactionKey + "," + balayer.ToobjectstrEvenNull(Session["Branchid"]) + ",'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," + txtPaymentNumber.Text + ",'C'," + iresult + ",'" + balayer.indiandateToMysqlDate(txtDate.Text) + "','Loan Amount Adjusted for " + strNarration + "'," + txtLoanAmount.Text + ",'" + txtSeries.Text + "','" + balayer.MySQLEscapeString(balayer.ToobjectstrEvenNull(Session["UserName"])) + "',0," + balayer.indiandateToMysqlDate(txtDate.Text).Split('/')[2] + "," + balayer.indiandateToMysqlDate(txtDate.Text).Split('/')[1] + "," + balayer.indiandateToMysqlDate(txtDate.Text).Split('/')[0] + "," + GetMemberId + ",2,8," + ddlGroupNumber.SelectedItem.Value + ",3)");
                    }
                }
                Decimal dblLoanInterest;
                if (decimal.TryParse(txtLoanAmount.Text, out dblLoanInterest))
                {
                    if (dblLoanInterest > 0)
                    {
                        long GetLoanInterest = trn.insertorupdateTrn("insert into voucher (`DualTransactionKey`,`BranchID`,`CurrDate`,`Voucher_No`,`Voucher_Type`,`Head_Id`,`ChoosenDate`,`Narration`,`Amount`,`Series`,`ReceievedBy`,`Trans_Type`,`T_Day`,`T_Month`,`T_Year`,`MemberID`,`Trans_Medium`,`RootID`,`ChitGroupId`,`Other_Trans_Type`) values (" + DualTransactionKey + "," + balayer.ToobjectstrEvenNull(Session["Branchid"]) + ",'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," + txtPaymentNumber.Text + ",'C',67,'" + balayer.indiandateToMysqlDate(txtDate.Text) + "','Loan Interest Adjusted for " + strNarration + "'," + txtLoanInterest.Text + ",'" + txtSeries.Text + "','" + balayer.MySQLEscapeString(balayer.ToobjectstrEvenNull(Session["UserName"])) + "',0," + balayer.indiandateToMysqlDate(txtDate.Text).Split('/')[2] + "," + balayer.indiandateToMysqlDate(txtDate.Text).Split('/')[1] + "," + balayer.indiandateToMysqlDate(txtDate.Text).Split('/')[0] + "," + GetMemberId + ",2,11," + ddlGroupNumber.SelectedItem.Value + ",0)");
                    }
                }
              //  Decimal dbServiceCharge;
              
                decimal future = 0;
                decimal futurecall = 0;

                decimal divedend = 0;
                

                for (int iTrans = 0; iTrans < GridGuardians.Rows.Count; iTrans++)
                {
                    DropDownList ddlRangeHeads = (DropDownList)GridGuardians.Rows[iTrans].FindControl("ddlRangeHeads");
                    TextBox txtRangeAmount = (TextBox)GridGuardians.Rows[iTrans].FindControl("txtRangeAmount");
                    TextBox txtRange = (TextBox)GridGuardians.Rows[iTrans].FindControl("txtRange");

                    if (ddlRangeHeads.SelectedItem.Text != "--Select--")
                    {
                        string ddl = ddlRangeHeads.SelectedItem.Text;
                        bool isfuture = decimal.TryParse(txtRangeAmount.Text, out future);
                        if (!isfuture)
                        {
                            txtRangeAmount.Text = "0.00";
                        }
                        else if (!ddl.Contains("|"))
                        {
                            string strHead = ddlRangeHeads.SelectedValue.Split('|')[0];
                            string strRoot = ddlRangeHeads.SelectedValue.Split('|')[1];
                            long futureAmt = trn.insertorupdateTrn("insert into voucher (`DualTransactionKey`,`BranchID`,`CurrDate`,`Voucher_No`,`Voucher_Type`,`Head_Id`,`ChoosenDate`,`Narration`,`Amount`,`Series`,`ReceievedBy`,`Trans_Type`,`T_Day`,`T_Month`,`T_Year`,`MemberID`,`Trans_Medium`,`RootID`,`ChitGroupId`,`Other_Trans_Type`) values (" + DualTransactionKey + "," + balayer.ToobjectstrEvenNull(Session["Branchid"]) + ",'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," + txtPaymentNumber.Text + ",'C'," + strHead + ",'" + balayer.indiandateToMysqlDate(txtDate.Text) + "','" + futureCallNarration + balayer.MySQLEscapeString(txtRange.Text) + "'," + txtRangeAmount.Text + ",'" + txtSeries.Text + "','" + balayer.MySQLEscapeString(balayer.ToobjectstrEvenNull(Session["UserName"])) + "',1," + balayer.indiandateToMysqlDate(txtDate.Text).Split('/')[2] + "," + balayer.indiandateToMysqlDate(txtDate.Text).Split('/')[1] + "," + balayer.indiandateToMysqlDate(txtDate.Text).Split('/')[0] + "," + GetMemberId + ",2," + strRoot + "," + ddlGroupNumber.SelectedItem.Value + ",3)");
                            futurecall += Convert.ToDecimal(txtRangeAmount.Text);
                        }
                        else
                        {
                            try
                            {

                                string strHead = ddlRangeHeads.SelectedValue.Split('|')[0];
                                string strRoot = ddlRangeHeads.SelectedValue.Split('|')[1];
                                int totalMembers = Convert.ToInt32(balayer.GetSingleValue("SELECT `groupmaster`.`NoofMembers` FROM svcf.groupmaster where Head_Id=" + ddlGroupNumber.SelectedItem.Value + ""));
                                qry = "SELECT Head_Id FROM svcf.membertogroupmaster where GroupID=" + strHead + "";
                                GetMemList = balayer.RetrveList(qry);
                                divedend += (Convert.ToDecimal(txtRangeAmount.Text) / totalMembers);
                                for (int li = 0; li <= GetMemList.Count - 1; li++)
                                {
                                    getvc = balayer.CheckVoucher_Exist(Convert.ToInt32(txtPaymentNumber.Text), Convert.ToInt32(Session["Branchid"]));
                                    if (getvc == false)
                                    {
                                        long futureAmt1 = trn.insertorupdateTrn("insert into voucher (`DualTransactionKey`,`BranchID`,`CurrDate`,`Voucher_No`,`Voucher_Type`,`Head_Id`,`ChoosenDate`,`Narration`,`Amount`,`Series`,`ReceievedBy`,`Trans_Type`,`T_Day`,`T_Month`,`T_Year`,`MemberID`,`Trans_Medium`,`RootID`,`ChitGroupId`,`Other_Trans_Type`) values (" + DualTransactionKey + "," + balayer.ToobjectstrEvenNull(Session["Branchid"]) + ",'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," + txtPaymentNumber.Text + ",'C'," + Convert.ToInt32(GetMemList[li]) + ",'" + balayer.indiandateToMysqlDate(txtDate.Text) + "','" + futureCallNarration + balayer.MySQLEscapeString(txtRange.Text) + "'," + divedend + ",'" + txtSeries.Text + "','" + balayer.MySQLEscapeString(balayer.ToobjectstrEvenNull(Session["UserName"])) + "',1," + balayer.indiandateToMysqlDate(txtDate.Text).Split('/')[2] + "," + balayer.indiandateToMysqlDate(txtDate.Text).Split('/')[1] + "," + balayer.indiandateToMysqlDate(txtDate.Text).Split('/')[0] + "," + GetMemberId + ",2," + strRoot + "," + ddlGroupNumber.SelectedItem.Value + ",5)");
                                    }
                                }
                                trn.CommitTrn();
                            }
                            catch(Exception )
                            {
                                trn.RollbackTrn();
                            }
                           
                        }

                    }

                }


                Decimal dbDefault;
                //Default Interest 31/07/2019
                //if (decimal.TryParse(txtDefault.Text, out dbDefault))
                //{
                //    if (dbDefault > 0)
                //    {
                //        long defaulet = trn.insertorupdateTrn("insert into voucher (`DualTransactionKey`,`BranchID`,`CurrDate`,`Voucher_No`,`Voucher_Type`,`Head_Id`,`ChoosenDate`,`Narration`,`Amount`,`Series`,`ReceievedBy`,`Trans_Type`,`T_Day`,`T_Month`,`T_Year`,`MemberID`,`Trans_Medium`,`RootID`,`ChitGroupId`,`Other_Trans_Type`) values (" + DualTransactionKey + "," + balayer.ToobjectstrEvenNull(Session["Branchid"]) + ",'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," + txtPaymentNumber.Text + ",'C',1131147,'" + balayer.indiandateToMysqlDate(txtDate.Text) + "','Default Interest'," + txtDefault.Text + ",'" + txtSeries.Text + "','" + balayer.MySQLEscapeString(balayer.ToobjectstrEvenNull(Session["UserName"])) + "',1," + balayer.indiandateToMysqlDate(txtDate.Text).Split('/')[2] + "," + balayer.indiandateToMysqlDate(txtDate.Text).Split('/')[1] + "," + balayer.indiandateToMysqlDate(txtDate.Text).Split('/')[0] + "," + GetMemberId + ",2,11," + ddlGroupNumber.SelectedItem.Value + ",3)");
                //    }
                //}
                //Default Interest 31/07/2019
                if (Convert.ToDecimal(txtIncidentialCharge.Text) != 0)
                {
                   long GetIncidentialCharge = trn.insertorupdateTrn("insert into voucher (`DualTransactionKey`,`BranchID`,`CurrDate`,`Voucher_No`,`Voucher_Type`,`Head_Id`,`ChoosenDate`,`Narration`,`Amount`,`Series`,`ReceievedBy`,`Trans_Type`,`T_Day`,`T_Month`,`T_Year`,`MemberID`,`Trans_Medium`,`RootID`,`ChitGroupId`,`Other_Trans_Type`) values (" + DualTransactionKey + "," + balayer.ToobjectstrEvenNull(Session["Branchid"]) + ",'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," + txtPaymentNumber.Text + ",'C',76,'" + balayer.indiandateToMysqlDate(txtDate.Text) + "','Incidential Charge  for " + strNarration + "'," + txtIncidentialCharge.Text + ",'" + txtSeries.Text + "','" + balayer.MySQLEscapeString(balayer.ToobjectstrEvenNull(Session["UserName"])) + "',2," + balayer.indiandateToMysqlDate(txtDate.Text).Split('/')[2] + "," + balayer.indiandateToMysqlDate(txtDate.Text).Split('/')[1] + "," + balayer.indiandateToMysqlDate(txtDate.Text).Split('/')[0] + "," + GetMemberId + ",0,11," + ddlGroupNumber.SelectedItem.Value + ",0)");
                }
                if (txtGST.Text != "")
                {
                    if (Convert.ToDecimal(txtGST.Text) != 0)
                    {
                        long GetGSTCharge = trn.insertorupdateTrn("insert into voucher (`DualTransactionKey`,`BranchID`,`CurrDate`,`Voucher_No`,`Voucher_Type`,`Head_Id`,`ChoosenDate`,`Narration`,`Amount`,`Series`,`ReceievedBy`,`Trans_Type`,`T_Day`,`T_Month`,`T_Year`,`MemberID`,`Trans_Medium`,`RootID`,`ChitGroupId`,`Other_Trans_Type`) values (" + DualTransactionKey + "," + balayer.ToobjectstrEvenNull(Session["Branchid"]) + ",'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," + txtPaymentNumber.Text + ",'C',1131156,'" + balayer.indiandateToMysqlDate(txtDate.Text) + "','GST(Goods and Service Tax) for " + strNarration + "'," + txtGST.Text + ",'" + txtSeries.Text + "','" + balayer.MySQLEscapeString(balayer.ToobjectstrEvenNull(Session["UserName"])) + "',2," + balayer.indiandateToMysqlDate(txtDate.Text).Split('/')[2] + "," + balayer.indiandateToMysqlDate(txtDate.Text).Split('/')[1] + "," + balayer.indiandateToMysqlDate(txtDate.Text).Split('/')[0] + "," + GetMemberId + ",0,4," + ddlGroupNumber.SelectedItem.Value + ",0)");
                    }
                }

                if (chkRegisteredGST.Checked == true)
                {
                    if (txtCgst.Text != "")
                    {
                        if (Convert.ToDecimal(txtCgst.Text) != 0)
                        {
                            long GetCGSTCharge = trn.insertorupdateTrn("insert into voucher (`DualTransactionKey`,`BranchID`,`CurrDate`,`Voucher_No`,`Voucher_Type`,`Head_Id`,`ChoosenDate`,`Narration`,`Amount`,`Series`,`ReceievedBy`,`Trans_Type`,`T_Day`,`T_Month`,`T_Year`,`MemberID`,`Trans_Medium`,`RootID`,`ChitGroupId`,`Other_Trans_Type`) values (" + DualTransactionKey + "," + balayer.ToobjectstrEvenNull(Session["Branchid"]) + ",'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," + txtPaymentNumber.Text + ",'C',1131803,'" + balayer.indiandateToMysqlDate(txtDate.Text) + "','CGST(Goods and Service Tax) for " + strNarration + "'," + txtCgst.Text + ",'" + txtSeries.Text + "','" + balayer.MySQLEscapeString(balayer.ToobjectstrEvenNull(Session["UserName"])) + "',2," + balayer.indiandateToMysqlDate(txtDate.Text).Split('/')[2] + "," + balayer.indiandateToMysqlDate(txtDate.Text).Split('/')[1] + "," + balayer.indiandateToMysqlDate(txtDate.Text).Split('/')[0] + "," + GetMemberId + ",0,4," + ddlGroupNumber.SelectedItem.Value + ",0)");
                        }
                    }

                    if (txtSgst.Text != "")
                    {
                        if (Convert.ToDecimal(txtSgst.Text) != 0)
                        {
                            long GetSGSTCharge = trn.insertorupdateTrn("insert into voucher (`DualTransactionKey`,`BranchID`,`CurrDate`,`Voucher_No`,`Voucher_Type`,`Head_Id`,`ChoosenDate`,`Narration`,`Amount`,`Series`,`ReceievedBy`,`Trans_Type`,`T_Day`,`T_Month`,`T_Year`,`MemberID`,`Trans_Medium`,`RootID`,`ChitGroupId`,`Other_Trans_Type`) values (" + DualTransactionKey + "," + balayer.ToobjectstrEvenNull(Session["Branchid"]) + ",'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," + txtPaymentNumber.Text + ",'C',1131801,'" + balayer.indiandateToMysqlDate(txtDate.Text) + "','SGST(Goods and Service Tax) for " + strNarration + "'," + txtSgst.Text + ",'" + txtSeries.Text + "','" + balayer.MySQLEscapeString(balayer.ToobjectstrEvenNull(Session["UserName"])) + "',2," + balayer.indiandateToMysqlDate(txtDate.Text).Split('/')[2] + "," + balayer.indiandateToMysqlDate(txtDate.Text).Split('/')[1] + "," + balayer.indiandateToMysqlDate(txtDate.Text).Split('/')[0] + "," + GetMemberId + ",0,4," + ddlGroupNumber.SelectedItem.Value + ",0)");
                        }
                    }

                    if (txtIgst.Text != "")
                    {
                        if (Convert.ToDecimal(txtIgst.Text) != 0)
                        {
                            long GetIGSTCharge = trn.insertorupdateTrn("insert into voucher (`DualTransactionKey`,`BranchID`,`CurrDate`,`Voucher_No`,`Voucher_Type`,`Head_Id`,`ChoosenDate`,`Narration`,`Amount`,`Series`,`ReceievedBy`,`Trans_Type`,`T_Day`,`T_Month`,`T_Year`,`MemberID`,`Trans_Medium`,`RootID`,`ChitGroupId`,`Other_Trans_Type`) values (" + DualTransactionKey + "," + balayer.ToobjectstrEvenNull(Session["Branchid"]) + ",'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," + txtPaymentNumber.Text + ",'C',1131799,'" + balayer.indiandateToMysqlDate(txtDate.Text) + "','IGST(Goods and Service Tax) for " + strNarration + "'," + txtIgst.Text + ",'" + txtSeries.Text + "','" + balayer.MySQLEscapeString(balayer.ToobjectstrEvenNull(Session["UserName"])) + "',2," + balayer.indiandateToMysqlDate(txtDate.Text).Split('/')[2] + "," + balayer.indiandateToMysqlDate(txtDate.Text).Split('/')[1] + "," + balayer.indiandateToMysqlDate(txtDate.Text).Split('/')[0] + "," + GetMemberId + ",0,4," + ddlGroupNumber.SelectedItem.Value + ",0)");
                        }
                    }
                }
                else
                {
                    if (txtCgst.Text != "")
                    {
                        if (Convert.ToDecimal(txtCgst.Text) != 0)
                        {
                            long GetCGSTCharge = trn.insertorupdateTrn("insert into voucher (`DualTransactionKey`,`BranchID`,`CurrDate`,`Voucher_No`,`Voucher_Type`,`Head_Id`,`ChoosenDate`,`Narration`,`Amount`,`Series`,`ReceievedBy`,`Trans_Type`,`T_Day`,`T_Month`,`T_Year`,`MemberID`,`Trans_Medium`,`RootID`,`ChitGroupId`,`Other_Trans_Type`) values (" + DualTransactionKey + "," + balayer.ToobjectstrEvenNull(Session["Branchid"]) + ",'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," + txtPaymentNumber.Text + ",'C',1131804,'" + balayer.indiandateToMysqlDate(txtDate.Text) + "','CGST(Goods and Service Tax) for " + strNarration + "'," + txtCgst.Text + ",'" + txtSeries.Text + "','" + balayer.MySQLEscapeString(balayer.ToobjectstrEvenNull(Session["UserName"])) + "',2," + balayer.indiandateToMysqlDate(txtDate.Text).Split('/')[2] + "," + balayer.indiandateToMysqlDate(txtDate.Text).Split('/')[1] + "," + balayer.indiandateToMysqlDate(txtDate.Text).Split('/')[0] + "," + GetMemberId + ",0,4," + ddlGroupNumber.SelectedItem.Value + ",0)");
                        }
                    }

                    if (txtSgst.Text != "")
                    {
                        if (Convert.ToDecimal(txtSgst.Text) != 0)
                        {
                            long GetSGSTCharge = trn.insertorupdateTrn("insert into voucher (`DualTransactionKey`,`BranchID`,`CurrDate`,`Voucher_No`,`Voucher_Type`,`Head_Id`,`ChoosenDate`,`Narration`,`Amount`,`Series`,`ReceievedBy`,`Trans_Type`,`T_Day`,`T_Month`,`T_Year`,`MemberID`,`Trans_Medium`,`RootID`,`ChitGroupId`,`Other_Trans_Type`) values (" + DualTransactionKey + "," + balayer.ToobjectstrEvenNull(Session["Branchid"]) + ",'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," + txtPaymentNumber.Text + ",'C',1131802,'" + balayer.indiandateToMysqlDate(txtDate.Text) + "','SGST(Goods and Service Tax) for " + strNarration + "'," + txtSgst.Text + ",'" + txtSeries.Text + "','" + balayer.MySQLEscapeString(balayer.ToobjectstrEvenNull(Session["UserName"])) + "',2," + balayer.indiandateToMysqlDate(txtDate.Text).Split('/')[2] + "," + balayer.indiandateToMysqlDate(txtDate.Text).Split('/')[1] + "," + balayer.indiandateToMysqlDate(txtDate.Text).Split('/')[0] + "," + GetMemberId + ",0,4," + ddlGroupNumber.SelectedItem.Value + ",0)");
                        }
                    }

                    if (txtIgst.Text != "")
                    {
                        if (Convert.ToDecimal(txtIgst.Text) != 0)
                        {
                            long GetIGSTCharge = trn.insertorupdateTrn("insert into voucher (`DualTransactionKey`,`BranchID`,`CurrDate`,`Voucher_No`,`Voucher_Type`,`Head_Id`,`ChoosenDate`,`Narration`,`Amount`,`Series`,`ReceievedBy`,`Trans_Type`,`T_Day`,`T_Month`,`T_Year`,`MemberID`,`Trans_Medium`,`RootID`,`ChitGroupId`,`Other_Trans_Type`) values (" + DualTransactionKey + "," + balayer.ToobjectstrEvenNull(Session["Branchid"]) + ",'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," + txtPaymentNumber.Text + ",'C',1131800,'" + balayer.indiandateToMysqlDate(txtDate.Text) + "','IGST(Goods and Service Tax) for " + strNarration + "'," + txtIgst.Text + ",'" + txtSeries.Text + "','" + balayer.MySQLEscapeString(balayer.ToobjectstrEvenNull(Session["UserName"])) + "',2," + balayer.indiandateToMysqlDate(txtDate.Text).Split('/')[2] + "," + balayer.indiandateToMysqlDate(txtDate.Text).Split('/')[1] + "," + balayer.indiandateToMysqlDate(txtDate.Text).Split('/')[0] + "," + GetMemberId + ",0,4," + ddlGroupNumber.SelectedItem.Value + ",0)");
                        }
                    }
                }

                int a = 0;
                DataTable dd = new DataTable();
                if (Convert.ToDecimal(txtChitKasarAmount.ToolTip) != 0.00M)
                {
                    a = Convert.ToInt32(lbTool.ToolTip);
                    dd = balayer.GetDataTable("SELECT MemberID,Head_Id,MemberName,GrpMemberID FROM svcf.membertogroupmaster where GroupID=" + ddlGroupNumber.SelectedItem.Value + "");
                    if (a == dd.Rows.Count)
                    {
                        for (int i = 0; i < a; i++)
                        {
                            string GetMemberId1 = dd.Rows[i][0].ToString();
                            string GetHeadId1 = dd.Rows[i][1].ToString();
                              long GetChitKasarAmount1 = trn.insertorupdateTrn("insert into voucher (`DualTransactionKey`,`BranchID`,`CurrDate`,`Voucher_No`,`Voucher_Type`,`Head_Id`,`ChoosenDate`,`Narration`,`Amount`,`Series`,`ReceievedBy`,`Trans_Type`,`T_Day`,`T_Month`,`T_Year`,`MemberID`,`Trans_Medium`,`RootID`,`ChitGroupId`,`Other_Trans_Type`) values (" + DualTransactionKey + "," + balayer.ToobjectstrEvenNull(Session["Branchid"]) + ",'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," + txtPaymentNumber.Text + ",'C'," + GetHeadId1 + ",'" + balayer.indiandateToMysqlDate(txtDate.Text) + "','Chit Dividend Amount for " + dd.Rows[i][3].ToString() + " and Draw Number : " + txtDrawNumber.Text + "'," + txtChitKasarAmount.ToolTip + ",'" + txtSeries.Text + "','" + balayer.MySQLEscapeString(balayer.ToobjectstrEvenNull(Session["UserName"])) + "',1," + balayer.indiandateToMysqlDate(txtDate.Text).Split('/')[2] + "," + balayer.indiandateToMysqlDate(txtDate.Text).Split('/')[1] + "," + balayer.indiandateToMysqlDate(txtDate.Text).Split('/')[0] + "," + GetMemberId1 + ",0,5," + ddlGroupNumber.SelectedItem.Value + ",5)");
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "Warning", "alert(Member count does not match with dividend. ');", true);

                    }
                }
                if (a == dd.Rows.Count)
                {
                    long GetBankAmount = trn.insertorupdateTrn("insert into voucher (`DualTransactionKey`,`BranchID`,`CurrDate`,`Voucher_No`,`Voucher_Type`,`Head_Id`,`ChoosenDate`,`Narration`,`Amount`,`Series`,`ReceievedBy`,`Trans_Type`,`T_Day`,`T_Month`,`T_Year`,`MemberID`,`Trans_Medium`,`RootID`,`ChitGroupId`,`Other_Trans_Type`) values (" + DualTransactionKey + "," + balayer.ToobjectstrEvenNull(Session["Branchid"]) + ",'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," + txtPaymentNumber.Text + ",'C'," + BranchHeadId + ",'" + balayer.indiandateToMysqlDate(txtDate.Text) + "','" + txtDescription.Text + "'," + txtBankAmount.Text + ",'" + txtSeries.Text + "','" + balayer.MySQLEscapeString(balayer.ToobjectstrEvenNull(Session["UserName"])) + "',2," + balayer.indiandateToMysqlDate(txtDate.Text).Split('/')[2] + "," + balayer.indiandateToMysqlDate(txtDate.Text).Split('/')[1] + "," + balayer.indiandateToMysqlDate(txtDate.Text).Split('/')[0] + "," + GetMemberId + ",1,3," + ddlGroupNumber.SelectedItem.Value + ",0) ");

                    string DebitNarration = ddlMemberName.SelectedItem.Text.Split('=')[0] + " Draw Number :" + txtDrawNumber.Text + " payment Breakup as follows:\r\nPrized Amount Rs. : " + (Convert.ToDecimal(txtIncidentialCharge.Text) + Convert.ToDecimal(txtBankAmount.Text) + Convert.ToDecimal(txtGST.Text) + Convert.ToDecimal(txtCgst.Text) + Convert.ToDecimal(txtSgst.Text) + Convert.ToDecimal(txtIgst.Text));
                    if (Convert.ToDecimal(txtCommision.Text) > 0)
                    {
                        DebitNarration += "\r\nCommision : " + txtCommision.Text;
                    }
                    if (Convert.ToDecimal(txtChitKasarAmount.Text) > 0)
                    {
                        DebitNarration += "\r\nChit Dividend Amount : " + txtChitKasarAmount.Text;
                    }
                    if (Convert.ToDecimal(txtLoanAmount.Text) > 0)
                    {
                        DebitNarration += "\r\nLoan Amount : " + txtLoanAmount.Text;
                    }
                    if (Convert.ToDecimal(futurecall) > 0)
                    {
                        DebitNarration += "\r\nFuture Call Amount : " + futurecall;
                    }
                    long GetDebitAmount = trn.insertorupdateTrn("insert into voucher (`DualTransactionKey`,`BranchID`,`CurrDate`,`Voucher_No`,`Voucher_Type`,`Head_Id`,`ChoosenDate`,`Narration`,`Amount`,`Series`,`ReceievedBy`,`Trans_Type`,`T_Day`,`T_Month`,`T_Year`,`MemberID`,`Trans_Medium`,`RootID`,`ChitGroupId`,`Other_Trans_Type`) values (" + DualTransactionKey + "," + balayer.ToobjectstrEvenNull(Session["Branchid"]) + ",'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," + txtPaymentNumber.Text + ",'D'," + GetHeadId + ",'" + balayer.indiandateToMysqlDate(txtDate.Text) + "','" + txtDescription.Text + "'," + txtDebitAmount.Text + ",'" + txtSeries.Text + "','" + balayer.MySQLEscapeString(balayer.ToobjectstrEvenNull(Session["UserName"])) + "',2," + balayer.indiandateToMysqlDate(txtDate.Text).Split('/')[2] + "," + balayer.indiandateToMysqlDate(txtDate.Text).Split('/')[1] + "," + balayer.indiandateToMysqlDate(txtDate.Text).Split('/')[0] + "," + GetMemberId + ",0,5," + ddlGroupNumber.SelectedItem.Value + ",0)");
                    if (GetBankAmount > 0)
                    {
                        int lastdraw = Convert.ToInt32(txtDrawNumber.Text) + 1;
                        int drawlast = Convert.ToInt32(balayer.GetSingleValue("SELECT `groupmaster`.`ChitPeriod` FROM svcf.groupmaster where `groupmaster`.`Head_Id`=" + ddlGroupNumber.SelectedItem.Value + ""));
                        if (drawlast >= lastdraw)
                        {
                            DataTable auction = balayer.GetDataTable("SELECT `auctiondetails`.`PrizedMemberID`,`auctiondetails`.`GroupID`,`auctiondetails`.`AuctionDate`,`auctiondetails`.`NextDueAmount`,`auctiondetails`.`PrizedAmount`,`groupmaster`.`ChitValue`,`auctiondetails`.`CurrentDueAmount`  FROM svcf.auctiondetails left join groupmaster on (`auctiondetails`.`GroupID`=`groupmaster`.`Head_Id`) where `auctiondetails`.`GroupID`=" + ddlGroupNumber.SelectedItem.Value + " and `auctiondetails`.`PrizedMemberID`=" + ddlMemberName.SelectedItem.Value + " and `auctiondetails`.`BranchID`=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + "");
                            string AuctionDt = auction.Rows[0]["AuctionDate"].ToString();
                            decimal NextDue = Convert.ToDecimal(auction.Rows[0]["NextDueAmount"]);
                            decimal Priced = Convert.ToDecimal(auction.Rows[0]["PrizedAmount"]);
                            decimal chitvalue = Convert.ToDecimal(auction.Rows[0]["ChitValue"]);
                            decimal CurrentDue = Convert.ToDecimal(auction.Rows[0]["CurrentDueAmount"]);

                            long TransPaymentQuery = trn.insertorupdateTrn("insert into trans_payment (`TransactionKey_Bank`,`TransactionKey`,`DualTransactionKey`,`BranchID`,`ChitGroupID`,`DrawNo`,`PaymentApplyedOn`,`ApprovedOn`,`PaymentDate`,`GuarantorID`,`Description`,`ChitAmount`,`PrizedAmount`,`AuctionDate`,`NextDueAmount`,`NextDrawNo`,`AOSanctionNo`,`CurrentDueAmount`,`TokenNumber`,`GuarantorName`) values (" + GetBankAmount + "," + GetDebitAmount + "," + DualTransactionKey + "," + balayer.ToobjectstrEvenNull(Session["Branchid"]) + "," + ddlGroupNumber.SelectedItem.Value + "," + txtDrawNumber.Text + ",'" + balayer.indiandateToMysqlDate(txtApplyedOn.Text) + "','" + balayer.indiandateToMysqlDate(txtPaymentonDate.Text) + "','" + balayer.indiandateToMysqlDate(txtDate.Text) + "','0','" + balayer.MySQLEscapeString(txtDescription.Text) + "'," + chitvalue + "," + Priced + ",'" + balayer.indiandateToMysqlDate(AuctionDt) + "'," + NextDue + "," + lastdraw + "," + txtAOSanctiion.Text + "," + CurrentDue + "," + ddlMemberName.SelectedItem.Value + ",'" + balayer.MySQLEscapeString(txtGurantor.Text) + "')");
                            if (ddlBankName.SelectedItem.Value != "47" && ddlBankName.SelectedItem.Value != "48")
                            {
                                string tbBankName = (ddlBankName.SelectedItem.Text).Split('_')[0];
                                string tbIFCCode = (ddlBankName.SelectedItem.Text).Split('_')[1];
                                string tbAccountNumber = (ddlBankName.SelectedItem.Text).Split('_')[2];
                                string GetBankLocation = balayer.GetSingleValue("SELECT BankLocation FROM svcf.bankdetails where  IFCCode='" + tbIFCCode + "' and AccountNo='" + tbAccountNumber + "' and BankName='" + tbBankName + "'");
                                string chequeDD = txtChequeDDno.Text;
                                string totalchequeDDamount = txtBankAmount.Text;
                                string givenAmount = txtBankAmount.Text;
                                long GetTransactionBank = trn.insertorupdateTrn("insert into transbank(`BranchID`,`TransactionKey`,`DualTransactionKey`,`T_Day`,`T_Month`,`T_Year`,`Head_Id`,`BankHeadID`,`MemberID`,`CustomersBankName`,`DateInCheque`,`ChequeDDNO`,`GivenAmount`,`TotalChequeDDAmount`,`IsBounced`,`Trans_Type`) values(" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + "," + GetBankAmount + "," + DualTransactionKey + "," + balayer.indiandateToMysqlDate(txtDate.Text).Split('/')[2] + "," + balayer.indiandateToMysqlDate(txtDate.Text).Split('/')[1] + "," + balayer.indiandateToMysqlDate(txtDate.Text).Split('/')[0] + "," + BranchHeadId + "," + GetHeadId + "," + GetMemberId + ",'" + tbBankName + "','" + balayer.indiandateToMysqlDate(txtDate.Text) + "'," + chequeDD + "," + givenAmount + "," + totalchequeDDamount + ",0,2)");
                            }
                        }
                        else
                        {
                            DataTable auction = balayer.GetDataTable("SELECT `auctiondetails`.`PrizedMemberID`,`auctiondetails`.`GroupID`,`auctiondetails`.`AuctionDate`,`auctiondetails`.`NextDueAmount`,`auctiondetails`.`PrizedAmount`,`groupmaster`.`ChitValue`,`auctiondetails`.`CurrentDueAmount`  FROM svcf.auctiondetails left join groupmaster on (`auctiondetails`.`GroupID`=`groupmaster`.`Head_Id`) where `auctiondetails`.`GroupID`=" + ddlGroupNumber.SelectedItem.Value + " and `auctiondetails`.`PrizedMemberID`=" + ddlMemberName.SelectedItem.Value + " and `auctiondetails`.`BranchID`=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + "");
                            string AuctionDt = auction.Rows[0]["AuctionDate"].ToString();
                            decimal NextDue = Convert.ToDecimal(auction.Rows[0]["NextDueAmount"]);
                            decimal Priced = Convert.ToDecimal(auction.Rows[0]["PrizedAmount"]);
                            decimal chitvalue = Convert.ToDecimal(auction.Rows[0]["ChitValue"]);
                            decimal CurrentDue = Convert.ToDecimal(auction.Rows[0]["CurrentDueAmount"]);

                            long TransPaymentQuery = trn.insertorupdateTrn("insert into trans_payment (`TransactionKey_Bank`,`TransactionKey`,`DualTransactionKey`,`BranchID`,`ChitGroupID`,`DrawNo`,`PaymentApplyedOn`,`ApprovedOn`,`PaymentDate`,`GuarantorID`,`Description`,`ChitAmount`,`PrizedAmount`,`AuctionDate`,`NextDueAmount`,`NextDrawNo`,`AOSanctionNo`,`CurrentDueAmount`,`TokenNumber`,`GuarantorName`) values (" + GetBankAmount + "," + GetDebitAmount + "," + DualTransactionKey + "," + balayer.ToobjectstrEvenNull(Session["Branchid"]) + "," + ddlGroupNumber.SelectedItem.Value + "," + txtDrawNumber.Text + ",'" + balayer.indiandateToMysqlDate(txtApplyedOn.Text) + "','" + balayer.indiandateToMysqlDate(txtPaymentonDate.Text) + "','" + balayer.indiandateToMysqlDate(txtDate.Text) + "','1','" + balayer.MySQLEscapeString(txtDescription.Text) + "'," + chitvalue + "," + Priced + ",'" + balayer.indiandateToMysqlDate(AuctionDt) + "'," + NextDue + ",null," + txtAOSanctiion.Text + "," + CurrentDue + "," + ddlMemberName.SelectedItem.Value + ",'" + balayer.MySQLEscapeString(txtGurantor.Text) + "')");
                            if (ddlBankName.SelectedItem.Value != "47" && ddlBankName.SelectedItem.Value != "48")
                            {
                                string tbBankName = (ddlBankName.SelectedItem.Text).Split('_')[0];
                                string tbIFCCode = (ddlBankName.SelectedItem.Text).Split('_')[1];
                                string tbAccountNumber = (ddlBankName.SelectedItem.Text).Split('_')[2];
                                string GetBankLocation = balayer.GetSingleValue("SELECT BankLocation FROM svcf.bankdetails where  IFCCode='" + tbIFCCode + "' and AccountNo='" + tbAccountNumber + "' and BankName='" + tbBankName + "'");
                                string chequeDD = txtChequeDDno.Text;
                                string totalchequeDDamount = txtBankAmount.Text;
                                string givenAmount = txtBankAmount.Text;
                                long GetTransactionBank = trn.insertorupdateTrn("insert into transbank(`BranchID`,`TransactionKey`,`DualTransactionKey`,`T_Day`,`T_Month`,`T_Year`,`Head_Id`,`BankHeadID`,`MemberID`,`CustomersBankName`,`DateInCheque`,`ChequeDDNO`,`GivenAmount`,`TotalChequeDDAmount`,`IsBounced`,`Trans_Type`) values(" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + "," + GetBankAmount + "," + DualTransactionKey + "," + balayer.indiandateToMysqlDate(txtDate.Text).Split('/')[2] + "," + balayer.indiandateToMysqlDate(txtDate.Text).Split('/')[1] + "," + balayer.indiandateToMysqlDate(txtDate.Text).Split('/')[0] + "," + BranchHeadId + "," + GetHeadId + "," + GetMemberId + ",'" + tbBankName + "','" + balayer.indiandateToMysqlDate(txtDate.Text) + "'," + chequeDD + "," + givenAmount + "," + totalchequeDDamount + ",0,2)");
                            }
                        }
                    }

                    if (GrDocument.Items[0].Selected == true)
                    {
                        var Docno = Docnotxt.Text;
                        var Infav = InFavtxt.Text;
                        var Propval = Propvaltxt.Text;
                        var regmode = DropRegistmode.SelectedItem.Text;
                        var guanme = txtGurantor.Text;
                        var drawno = txtDrawNumber.Text;
                        var groupno = ddlGroupNumber.SelectedItem.Text;
                        var groupid = balayer.GetSingleValue("SELECT Head_Id FROM svcf.groupmaster where groupno='" + groupno + "';");
                        var memid = GetMemberId;
                        var insertdocumentdetail = @"insert into svcf.documentdetails (prizedmemberid,groupid,drawno,choosendate,documentno,infavourof,propertyvalue,registration,guaranteer,persontype) 
                                                  values('" + memid + "','" + groupid + "','" + drawno + "','" + balayer.indiandateToMysqlDate(txtDate.Text) + "','" + Docno + "','" + Infav + "','" + Propval + "','" + regmode + "','" + guanme + "','Document')";

                        trn.insertorupdateTrn(insertdocumentdetail);

                    }
                    DrawNo = txtDrawNumber.Text;
                    long s = trn.insertorupdateTrn("update auctiondetails set IsPrized='Y' where PrizedMemberID=" + ddlMemberName.SelectedValue + " and DrawNO=" + DrawNo + "");
                    ModalPopupExtender1.PopupControlID = "pnlmsg";
                    ModalPopupExtender1.Show();
                    pnlmsg.Visible = true;
                    lblh.Text = "Status";
                    lblcon.Text = ddlMemberName.SelectedItem.Text + " - Payment Inserted Successfully";
                    lblcon.ForeColor = System.Drawing.Color.Green;
                    BtnOK.Visible = false;
                    Button2.Visible = true;
                    Button3.Visible = false;
                    btnHide.Visible = false;
                    trn.CommitTrn();

                    txtPaymentNumber.Text = balayer.GetSingleValue("SELECT max(Voucher_No)+1 FROM svcf.voucher where  BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and Trans_Type=2 and Series='PAYMENT'");

                    logger.Info("paymentnew.aspx - btn_ok():  Completed: " + DateTime.Now + " by: " + Convert.ToString(Session["UserName"]) + "");
                }
            }
            catch (Exception error)
            {
               try
                {
                    trn.RollbackTrn();
                    balayer.GetInsertItem("update `auctiondetails` set IsPrized='N' where PrizedMemberID='" + ddlMemberName.SelectedValue + "' and DrawNO=" + DrawNo);
                    logger.Info("paymentnew.aspx - btn_ok():  Error: " + error.Message + ": " + DateTime.Now + " by: " + Convert.ToString(Session["UserName"]) + "");
                }
                catch
                { }
                finally
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Warning", "alert('" + error.Message.ToString().Replace("'", "\\'") + "');", true);
                }
            }
            finally
            {
                trn.DisposeTrn();
            }
        }

      
        protected void GrDocument_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (GrDocument.Items[0].Selected == true) Guadetailpanel.Visible = true;
            else Guadetailpanel.Visible = false;
        }
    }
}