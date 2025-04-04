using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SVCF_BusinessAccessLayer;
using SVCF_DataAccessLayer;
using SVCF_TransactionLayer;
using log4net;
using log4net.Config;

namespace SreeVisalamChitFundLtd_phase1
{
    public partial class OtherBranchPaymentPermanent : System.Web.UI.Page
    {
        #region Object
        BusinessLayer balayer = new BusinessLayer();
        #endregion
         ILog logger = log4net.LogManager.GetLogger(typeof(OtherBranchPaymentPermanent));

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
            DataTable dtAllHeads = balayer.GetDataTable("SELECT GrpMemberID,concat(cast(Head_Id as char),'|',5) as Head_Id FROM svcf.membertogroupmaster where MemberID='" + GetMemberId + "'");
            DataTable dtBranches = balayer.GetDataTable("SELECT B_Name AS GrpMemberID,concat(cast(Head_id as char),'|',1) AS Head_Id FROM svcf.branchdetails ;");
            dtAllHeads.Merge(dtBranches);
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
                rvDate.MaximumValue = DateTime.Now.ToString("dd/MM/yyyy");
                //rvDate.MinimumValue = balayer.GetSingleValue("SELECT DATE_FORMAT( MinimumDate, '%d/%m/%Y') MinimumDate FROM svcf.restrictionmaster where BranchID=" + Session["Branchid"]); ;
                rvDate.MinimumValue = balayer.GetSingleValue("SELECT DATE_FORMAT( min(ChoosenDate), '%d/%m/%Y') MinimumDate FROM svcf.voucher where " +
                    "BranchID=" + Session["Branchid"] + " and ChoosenDate<>'0000-00-00'");
                Session["CheckRefresh"] = System.Guid.NewGuid().ToString();
                clear();
                txtPaymentonDate.ToolTip = "Ex. " + DateTime.Now.ToString("dd/MM/yyyy") + " (dd/mm/yyyy)";
                txtDate.ToolTip = "Ex. " + DateTime.Now.ToString("dd/MM/yyyy") + " (dd/mm/yyyy)";
                txtApplyedOn.ToolTip = "Ex. " + DateTime.Now.ToString("dd/MM/yyyy") + " (dd/mm/yyyy)";
                this.BindData();
                SetDefaultRow();
            }
           
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
        protected void Page_PreRender(object sender, EventArgs e)
        {
            ViewState["CheckRefresh"] = Session["CheckRefresh"];
        }
        public void clear()
        {
            txtDefault.Text = "0.00";
            txtSeries.Text = "ADVICE";
            txtPaymentNumber.Text = "";
            LabelPrizedAmount.Text = "Member Name";
            txtPaymentNumber.Focus();
            txtDate.Text = "";
            txtBankAmount.Text = "0.00";
            txtChitKasarAmount.Text = "0.00";
            txtCommision.Text = "0.00";
            txtLoanAmount.Text = "0.00";
            txtIncidentialCharge.Text = "0.00";
            txtGST.Text = "0.00";
           // txtServiceCharge.Text = "0.00";
            txtDebitAmount.Text = "0.00";
            ddlMemberName.Items.Clear();
            txtDrawNumber.Text = "";
            txtGurantor.Text = "";
            txtDate.Text = "";
            txtPaymentonDate.Text = "";
            txtApplyedOn.Text = "";
            txtAOSanctiion.Text = "";
            txtPaymentonDate.Text = "";
            txtDescription.Text = "";
            txtApplyedOn.Text = "";
            getGroup();
        }
        public void getGroup()
        {
            DataTable dtchitgrpno = balayer.GetDataTable("Select distinct(groupmaster.GROUPNO) ,auctiondetails.GroupID from auctiondetails join groupmaster on (auctiondetails.GroupID=groupmaster.Head_Id) where auctiondetails.IsPrized='N'and auctiondetails.BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + "");
            DataRow drchitgrpno;
            drchitgrpno = dtchitgrpno.NewRow();
            drchitgrpno[0] = "--Select--";
            drchitgrpno[1] = "0";
            ddlGroupNumber.DataSource = dtchitgrpno;
            ddlGroupNumber.DataTextField = "GROUPNO";
            ddlGroupNumber.DataValueField = "GroupID";
            dtchitgrpno.Rows.InsertAt(drchitgrpno, 0);
            ddlGroupNumber.DataBind();
            string gpname=Request.QueryString["groupname"];
            string hdid = Request.QueryString["Hdid"];
            if (gpname != null)
            {
                if (ddlGroupNumber.Items.FindByValue(gpname.ToString()) != null)
                {
                    ddlGroupNumber.Items.FindByValue(gpname).Selected = true;
                    load_ddlGroupNumber(null, null);
                    if (hdid != null)
                    {
                        if (ddlMemberName.Items.FindByValue(hdid.ToString()) != null)
                        {
                            ddlMemberName.Items.FindByValue(hdid).Selected = true;
                            load_ddlMemberName(null, null);
                        }
                    }
                }
            }
        }

        protected void txtPaymentNumber_Validate(object sender, ServerValidateEventArgs e)
        {
            DataTable ssss = balayer.GetDataTable("SELECT Voucher_No FROM svcf.voucher where Voucher_No='" + txtPaymentNumber.Text + "' and BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and Trans_Type=2");
            if (ssss.Rows.Count > 0)
            {
                CustomValidator2.ErrorMessage = "Already exists";
                e.IsValid = false;
            }
            else
                e.IsValid = true;
        }

        protected void load_ddlGroupNumber(object sender, EventArgs e)
        {
            decimal chitValue = Convert.ToDecimal(balayer.GetSingleValue("SELECT ChitValue FROM svcf.groupmaster where Head_Id=" + ddlGroupNumber.SelectedItem.Value + ""));
            txtDebitAmount.Text = chitValue.ToString();
            DataTable dtAmount = balayer.GetDataTable("SELECT Commission,IncidentalCharges,ServiceTax,if(GstAmount>0 and GstAmount is not null,GstAmount,0) as GstAmount FROM svcf.commissiondetails where chitValue=" + chitValue + "");
            txtCommision.Text = dtAmount.Rows[0][0].ToString();
            txtIncidentialCharge.Text = dtAmount.Rows[0][1].ToString();
            //txtServiceCharge.ToolTip = dtAmount.Rows[0][2].ToString();
            //txtServiceCharge.Text = "0.00";           
            txtGST.Text = dtAmount.Rows[0][3].ToString();
            DataTable dtchit = balayer.GetDataTable("SELECT DISTINCT auctiondetails.PrizedMemberID,concat(membertogroupmaster.GrpMemberID,'=', membertogroupmaster.MemberName) as MemberName from auctiondetails inner join membertogroupmaster on (auctiondetails.PrizedMemberID=membertogroupmaster.Head_Id) join branchpayment as B1 on (auctiondetails.PrizedMemberID=B1.ChitNumber) where auctiondetails.GroupID=" + ddlGroupNumber.SelectedItem.Value + " and auctiondetails.IsPrized='N' and B1.Status=0");
            ddlMemberName.DataSource = dtchit;
            ddlMemberName.DataTextField = "MemberName";
            ddlMemberName.DataValueField = "PrizedMemberID";
            ddlMemberName.DataBind();
            ddlMemberName.Focus();
            ListItem lst1 = new ListItem("--Select--", "--Select--");
            ddlMemberName.Items.Insert(0, lst1);
            txtDrawNumber.Text = "";
            txtGurantor.Text = "";
            txtDate.Text = "";
            txtPaymentonDate.Text = "";
            txtApplyedOn.Text = "";
            txtAOSanctiion.Text = "";
            txtChitKasarAmount.Text = "0.00";
            txtBankAmount.Text = "0.00";
            ddlBankName.SelectedIndex = 0;
        }
        protected void load_ddlMemberName(object sender, EventArgs e)
        {
            if (ddlGroupNumber.SelectedItem.Text != "--Select--")
            {
                if (ddlMemberName.SelectedItem.Text != "--Select--")
                {
                    int DrawNo = Convert.ToInt32(balayer.GetSingleValue("SELECT DrawNO FROM svcf.auctiondetails where PrizedMemberID=" + ddlMemberName.SelectedValue + ""));
                    txtDrawNumber.Text = DrawNo.ToString();
                    LabelPrizedAmount.Text = (ddlMemberName.SelectedItem.Text).Split('=')[0];
                    decimal chitValue = Convert.ToDecimal(balayer.GetSingleValue("SELECT ChitValue FROM svcf.groupmaster where Head_Id=" + ddlGroupNumber.SelectedItem.Value + ""));
                    txtDebitAmount.Text = chitValue.ToString();
                    decimal commission = Convert.ToDecimal(balayer.GetSingleValue("SELECT Commission FROM svcf.commissiondetails where chitValue=" + chitValue + " "));
                    decimal GetKasarAmount = Convert.ToDecimal(balayer.GetSingleValue("SELECT KasarAmount FROM svcf.auctiondetails where PrizedMemberId=" + ddlMemberName.SelectedItem.Value + ""));
                    decimal decDefault = Convert.ToDecimal(balayer.GetSingleValue("SELECT DefaultInterest FROM svcf.auctiondetails where PrizedMemberId=" + ddlMemberName.SelectedItem.Value + ""));
                    txtDefault.Text = decDefault.ToString();
                    decimal kasar = GetKasarAmount - commission - decDefault;
                    int totalMembers = Convert.ToInt32(balayer.GetSingleValue("SELECT `groupmaster`.`NoofMembers` FROM svcf.groupmaster where Head_Id=" + ddlGroupNumber.SelectedItem.Value + ""));
                    txtChitKasarAmount.Text = kasar.ToString();
                    txtChitKasarAmount.ToolTip = (kasar / totalMembers).ToString();
                    lbTool.ToolTip = totalMembers.ToString();
                    decimal inCharge = Convert.ToDecimal(balayer.GetSingleValue("SELECT IncidentalCharges FROM svcf.commissiondetails where chitValue=" + chitValue.ToString()));
                    lbFuture.Text = (chitValue - commission - kasar - decDefault - inCharge).ToString();
                    txtBankAmount.Text = (chitValue - commission - kasar - decDefault - inCharge).ToString();
                    getBranch();
                    this.BindData();
                    SetDefaultRow();

                }
                else
                {
                    txtDrawNumber.Text = "";
                    txtGurantor.Text = "";
                    txtDate.Text = "";
                    txtPaymentonDate.Text = "";
                    txtApplyedOn.Text = "";
                    txtAOSanctiion.Text = "";
                    LabelPrizedAmount.Text = "";
                    txtDebitAmount.Text = "0.00";
                    txtChitKasarAmount.Text = "0.00";
                    txtChitKasarAmount.ToolTip = "0.00";
                    lbTool.ToolTip = "";
                    lbFuture.Text = "0.00";
                    txtBankAmount.Text = "0.00";
                    txtDefault.Text = "0.00";
                }
            }
        }

        public void getBranch()
        {
            DataTable dt = balayer.GetDataTable("SELECT BranchID,Amount,IncidentialCharges,GurantorName,DATE_FORMAT( PaymentDate, '%d/%m/%Y') as PaymentDate,DATE_FORMAT( AppliedDate, '%d/%m/%Y') as AppliedDate,DATE_FORMAT( ApprovedDate, '%d/%m/%Y') as ApprovedDate,AOsanction,GstCharges FROM svcf.branchpayment where ChitNumber=" + ddlMemberName.SelectedValue);
            //label10.Visible = true;
            //txtGurantor.Visible = true;
            //label8.Visible = true;
            //txtDate.Visible = true;
            //label13.Visible = true;
            //txtPaymentonDate.Visible = true;
            //label17.Visible = true;
            //txtApplyedOn.Visible = true;
            //label19.Visible = true;
            //txtAOSanctiion.Visible = true;
            if (dt.Rows.Count > 0)
            {

                DataTable dtNode = balayer.GetDataTable("SELECT B_Name,Head_Id FROM svcf.branchdetails where Head_Id in (" + dt.Rows[0]["BranchID"] + ")");
                ddlBankName.DataSource = dtNode;
                ddlBankName.DataTextField = "B_Name";
                ddlBankName.DataValueField = "Head_Id";
                ddlBankName.DataBind();
                txtIncidentialCharge.Text = Convert.ToString(dt.Rows[0]["IncidentialCharges"]);
                txtBankAmount.Text = Convert.ToString(dt.Rows[0]["Amount"]);
                txtGurantor.Text = Convert.ToString(dt.Rows[0]["GurantorName"]);
                txtDate.Text = Convert.ToString(dt.Rows[0]["PaymentDate"]);
                txtPaymentonDate.Text = Convert.ToString(dt.Rows[0]["ApprovedDate"]);
                txtApplyedOn.Text = Convert.ToString(dt.Rows[0]["AppliedDate"]);
                txtAOSanctiion.Text = Convert.ToString(dt.Rows[0]["AOsanction"]);
               
                txtGST.Text = Convert.ToString(dt.Rows[0]["GstCharges"]);

            }
            else
            {
                DataTable dtNode = balayer.GetDataTable("SELECT B_Name,Head_Id FROM svcf.branchdetails where Head_Id not in (" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + ")");
                DataRow dr = dtNode.NewRow();
                dr[0] = "--Select--";
                dr[1] = "0";
                ddlBankName.DataSource = dtNode;
                ddlBankName.DataTextField = "B_Name";
                ddlBankName.DataValueField = "Head_Id";
                dtNode.Rows.InsertAt(dr, 0);
                ddlBankName.DataBind();
                txtIncidentialCharge.Text = "0";
                txtGST.Text = "0";
                txtBankAmount.Text = "0";
                txtGurantor.Text = "";
                txtDate.Text = "";
                txtPaymentonDate.Text = "";
                txtApplyedOn.Text = "";
                txtAOSanctiion.Text = "";
            }
        }
        protected void LoanAmount_OnTextChanged(object sender, EventArgs e)
        {
            decimal lon = 0;
            bool islon = decimal.TryParse(txtLoanAmount.Text, out lon);
            if (!islon)
            {
                txtLoanAmount.Text = "0.00";
            }

            txtBankAmount.Text = (Convert.ToDecimal(lbFuture.Text) - (Convert.ToDecimal(txtLoanAmount.Text))).ToString();
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
            decimal de = 0;
            bool isde = decimal.TryParse(txtDefault.Text, out de);
            if (!isde)
            {
                txtDefault.Text = "0.00";
            }
            //decimal Credit = Convert.ToDecimal(txtServiceCharge.Text) + Convert.ToDecimal(txtBankAmount.Text) + Convert.ToDecimal(txtChitKasarAmount.Text) + Convert.ToDecimal(txtCommision.Text) + Convert.ToDecimal(txtIncidentialCharge.Text) + Convert.ToDecimal(txtLoanAmount.Text) + Convert.ToDecimal(futurecall) + Convert.ToDecimal(txtDefault.Text) + Convert.ToDecimal(txtGST.Text);
            decimal Credit = Convert.ToDecimal(txtBankAmount.Text) + Convert.ToDecimal(txtChitKasarAmount.Text) + Convert.ToDecimal(txtCommision.Text) + Convert.ToDecimal(txtIncidentialCharge.Text) + Convert.ToDecimal(txtLoanAmount.Text) + Convert.ToDecimal(futurecall) + Convert.ToDecimal(txtDefault.Text) + Convert.ToDecimal(txtGST.Text);
            decimal Debit = Convert.ToDecimal(txtDebitAmount.Text);
            if (Credit != Debit)
            {
                ModalPopupExtender1.PopupControlID = "pnlmsg";
                ModalPopupExtender1.Show();
                pnlmsg.Visible = true;
                lblh.Text = "Status";
                lblcon.Text = "Credit = " + Credit + " and Debit = " + Debit + " is not same!!!";
                lblcon.ForeColor = System.Drawing.Color.Green;
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
                lblcon.Text = "Do you want to insert the payment";
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
        protected void btn_ok(object sender, EventArgs e)
        {
            TransactionLayer trn = new TransactionLayer();
            string strNarration = "";
            string DualTransactionKey = "";
            string DrawNo = "";
            try
            {
                strNarration = balayer.MySQLEscapeString(ddlMemberName.SelectedItem.Text).Split('=')[0] + " through payment of Draw Number :" + txtDrawNumber.Text;
                System.Guid guid = Guid.NewGuid();
                string guidForChar36 = guid.ToString();
                string hexstring = BitConverter.ToString(guid.ToByteArray());
                string guidForBinary16 = "0x" + hexstring.Replace("-", string.Empty);
                DualTransactionKey = guidForBinary16;
                string BranchHeadId = ddlBankName.SelectedItem.Value;
                string GetHeadId = balayer.GetSingleValue("SELECT Head_Id FROM svcf.membertogroupmaster where Head_Id=" + ddlMemberName.SelectedItem.Value + "");
                string GetMemberId = balayer.GetSingleValue("SELECT MemberID FROM svcf.membertogroupmaster where Head_Id=" + ddlMemberName.SelectedItem.Value + "");
                long GetCommision = trn.insertorupdateTrn("insert into voucher (`DualTransactionKey`,`BranchID`,`CurrDate`,`Voucher_No`,`Voucher_Type`,`Head_Id`,`ChoosenDate`,`Narration`,`Amount`,`Series`,`ReceievedBy`,`Trans_Type`,`T_Day`,`T_Month`,`T_Year`,`MemberID`,`Trans_Medium`,`RootID`,`ChitGroupId`,`Other_Trans_Type`) values (" + DualTransactionKey + "," + balayer.ToobjectstrEvenNull(Session["Branchid"]) + ",'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," + txtPaymentNumber.Text + ",'C',64,'" + balayer.indiandateToMysqlDate(txtDate.Text) + "','Commision for " + strNarration + "'," + txtCommision.Text + ",'" + txtSeries.Text + "','" + balayer.MySQLEscapeString(balayer.ToobjectstrEvenNull(Session["UserName"])) + "',2," + balayer.indiandateToMysqlDate(txtDate.Text).Split('/')[2] + "," + balayer.indiandateToMysqlDate(txtDate.Text).Split('/')[1] + "," + balayer.indiandateToMysqlDate(txtDate.Text).Split('/')[0] + "," + GetMemberId + ",0,11," + ddlGroupNumber.SelectedItem.Value + ",0) ");
                Decimal dblLoan;
                if (decimal.TryParse(txtLoanAmount.Text, out dblLoan))
                {
                    if (dblLoan > 0)
                    {
                        long GetLoanAmount = trn.insertorupdateTrn("insert into voucher (`DualTransactionKey`,`BranchID`,`CurrDate`,`Voucher_No`,`Voucher_Type`,`Head_Id`,`ChoosenDate`,`Narration`,`Amount`,`Series`,`ReceievedBy`,`Trans_Type`,`T_Day`,`T_Month`,`T_Year`,`MemberID`,`Trans_Medium`,`RootID`,`ChitGroupId`,`Other_Trans_Type`) values (" + DualTransactionKey + "," + balayer.ToobjectstrEvenNull(Session["Branchid"]) + ",'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," + txtPaymentNumber.Text + ",'C',8,'" + balayer.indiandateToMysqlDate(txtDate.Text) + "','Loan Amount Adjusted for " + strNarration + "'," + txtLoanAmount.Text + ",'" + txtSeries.Text + "','" + balayer.MySQLEscapeString(balayer.ToobjectstrEvenNull(Session["UserName"])) + "',0," + balayer.indiandateToMysqlDate(txtDate.Text).Split('/')[2] + "," + balayer.indiandateToMysqlDate(txtDate.Text).Split('/')[1] + "," + balayer.indiandateToMysqlDate(txtDate.Text).Split('/')[0] + "," + GetMemberId + ",2,8," + ddlGroupNumber.SelectedItem.Value + ",3)");
                    }
                }
                Decimal dbServiceCharge;
                //if (decimal.TryParse(txtServiceCharge.Text, out dbServiceCharge))
                //{
                //    if (dbServiceCharge > 0)
                //    {
                //        long GetServiceCharge = trn.insertorupdateTrn("insert into voucher (`DualTransactionKey`,`BranchID`,`CurrDate`,`Voucher_No`,`Voucher_Type`,`Head_Id`,`ChoosenDate`,`Narration`,`Amount`,`Series`,`ReceievedBy`,`Trans_Type`,`T_Day`,`T_Month`,`T_Year`,`MemberID`,`Trans_Medium`,`RootID`,`ChitGroupId`,`Other_Trans_Type`) values (" + DualTransactionKey + "," + balayer.ToobjectstrEvenNull(Session["Branchid"]) + ",'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," + txtPaymentNumber.Text + ",'C',1062,'" + balayer.indiandateToMysqlDate(txtDate.Text) + "','Service Charge for " + strNarration + "'," + txtServiceCharge.Text + ",'" + txtSeries.Text + "','" + balayer.MySQLEscapeString(balayer.ToobjectstrEvenNull(Session["UserName"])) + "',2," + balayer.indiandateToMysqlDate(txtDate.Text).Split('/')[2] + "," + balayer.indiandateToMysqlDate(txtDate.Text).Split('/')[1] + "," + balayer.indiandateToMysqlDate(txtDate.Text).Split('/')[0] + "," + GetMemberId + ",0,11," + ddlGroupNumber.SelectedItem.Value + ",0) ");
                //    }
                //}
                decimal future = 0;
                decimal futurecall = 0;
                for (int iTrans = 0; iTrans < GridGuardians.Rows.Count; iTrans++)
                {
                    DropDownList ddlRangeHeads = (DropDownList)GridGuardians.Rows[iTrans].FindControl("ddlRangeHeads");
                    TextBox txtRangeAmount = (TextBox)GridGuardians.Rows[iTrans].FindControl("txtRangeAmount");
                    TextBox txtRange = (TextBox)GridGuardians.Rows[iTrans].FindControl("txtRange");
                    bool isfuture = decimal.TryParse(txtRangeAmount.Text, out future);
                    if (!isfuture)
                    {
                        txtRangeAmount.Text = "0.00";
                    }
                    else
                    {
                        string strHead = ddlRangeHeads.SelectedValue.Split('|')[0];
                        string strRoot = ddlRangeHeads.SelectedValue.Split('|')[1];
                        long futureAmt = trn.insertorupdateTrn("insert into voucher (`DualTransactionKey`,`BranchID`,`CurrDate`,`Voucher_No`,`Voucher_Type`,`Head_Id`,`ChoosenDate`,`Narration`,`Amount`,`Series`,`ReceievedBy`,`Trans_Type`,`T_Day`,`T_Month`,`T_Year`,`MemberID`,`Trans_Medium`,`RootID`,`ChitGroupId`,`Other_Trans_Type`) values (" + DualTransactionKey + "," + balayer.ToobjectstrEvenNull(Session["Branchid"]) + ",'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," + txtPaymentNumber.Text + ",'C'," + strHead + ",'" + balayer.indiandateToMysqlDate(txtDate.Text) + "','Future Call Amount Adjusted for " + strNarration + " and Range : " + balayer.MySQLEscapeString(txtRange.Text) + "'," + txtRangeAmount.Text + ",'" + txtSeries.Text + "','" + balayer.MySQLEscapeString(balayer.ToobjectstrEvenNull(Session["UserName"])) + "',1," + balayer.indiandateToMysqlDate(txtDate.Text).Split('/')[2] + "," + balayer.indiandateToMysqlDate(txtDate.Text).Split('/')[1] + "," + balayer.indiandateToMysqlDate(txtDate.Text).Split('/')[0] + "," + GetMemberId + ",2," + strRoot + "," + ddlGroupNumber.SelectedItem.Value + ",3)");
                        futurecall += Convert.ToDecimal(txtRangeAmount.Text);
                    }
                }
                Decimal dbDefault;
                if (decimal.TryParse(txtDefault.Text, out dbDefault))
                {
                    if (dbDefault > 0)
                    {
                        long defaulet = trn.insertorupdateTrn("insert into voucher (`DualTransactionKey`,`BranchID`,`CurrDate`,`Voucher_No`,`Voucher_Type`,`Head_Id`,`ChoosenDate`,`Narration`,`Amount`,`Series`,`ReceievedBy`,`Trans_Type`,`T_Day`,`T_Month`,`T_Year`,`MemberID`,`Trans_Medium`,`RootID`,`ChitGroupId`,`Other_Trans_Type`) values (" + DualTransactionKey + "," + balayer.ToobjectstrEvenNull(Session["Branchid"]) + ",'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," + txtPaymentNumber.Text + ",'C',00174,'" + balayer.indiandateToMysqlDate(txtDate.Text) + "','Default Interest'," + txtDefault.Text + ",'" + txtSeries.Text + "','" + balayer.MySQLEscapeString(balayer.ToobjectstrEvenNull(Session["UserName"])) + "',1," + balayer.indiandateToMysqlDate(txtDate.Text).Split('/')[2] + "," + balayer.indiandateToMysqlDate(txtDate.Text).Split('/')[1] + "," + balayer.indiandateToMysqlDate(txtDate.Text).Split('/')[0] + "," + GetMemberId + ",2,11," + ddlGroupNumber.SelectedItem.Value + ",3)");
                    }
                }
                if (Convert.ToDecimal(txtIncidentialCharge.Text) != 0)
                {
                    long GetIncidentialCharge = trn.insertorupdateTrn("insert into voucher (`DualTransactionKey`,`BranchID`,`CurrDate`,`Voucher_No`,`Voucher_Type`,`Head_Id`,`ChoosenDate`,`Narration`,`Amount`,`Series`,`ReceievedBy`,`Trans_Type`,`T_Day`,`T_Month`,`T_Year`,`MemberID`,`Trans_Medium`,`RootID`,`ChitGroupId`,`Other_Trans_Type`) values (" + DualTransactionKey + "," + balayer.ToobjectstrEvenNull(Session["Branchid"]) + ",'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," + txtPaymentNumber.Text + ",'C',76,'" + balayer.indiandateToMysqlDate(txtDate.Text) + "','Incidential Charge  for " + strNarration + "'," + txtIncidentialCharge.Text + ",'" + txtSeries.Text + "','" + balayer.MySQLEscapeString(balayer.ToobjectstrEvenNull(Session["UserName"])) + "',2," + balayer.indiandateToMysqlDate(txtDate.Text).Split('/')[2] + "," + balayer.indiandateToMysqlDate(txtDate.Text).Split('/')[1] + "," + balayer.indiandateToMysqlDate(txtDate.Text).Split('/')[0] + "," + GetMemberId + ",0,11," + ddlGroupNumber.SelectedItem.Value + ",0)");
                }
                int a = Convert.ToInt32(lbTool.ToolTip);

                if (Convert.ToDecimal(txtGST.Text) != 0)
                {
                    long GetGstCharge = trn.insertorupdateTrn("insert into voucher (`DualTransactionKey`,`BranchID`,`CurrDate`,`Voucher_No`,`Voucher_Type`,`Head_Id`,`ChoosenDate`,`Narration`,`Amount`,`Series`,`ReceievedBy`,`Trans_Type`,`T_Day`,`T_Month`,`T_Year`,`MemberID`,`Trans_Medium`,`RootID`,`ChitGroupId`,`Other_Trans_Type`) values (" + DualTransactionKey + "," + balayer.ToobjectstrEvenNull(Session["Branchid"]) + ",'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," + txtPaymentNumber.Text + ",'C',1131156,'" + balayer.indiandateToMysqlDate(txtDate.Text) + "','GST(Goods and Service Tax) for " + strNarration + "'," + txtGST.Text + ",'" + txtSeries.Text + "','" + balayer.MySQLEscapeString(balayer.ToobjectstrEvenNull(Session["UserName"])) + "',2," + balayer.indiandateToMysqlDate(txtDate.Text).Split('/')[2] + "," + balayer.indiandateToMysqlDate(txtDate.Text).Split('/')[1] + "," + balayer.indiandateToMysqlDate(txtDate.Text).Split('/')[0] + "," + GetMemberId + ",0,4," + ddlGroupNumber.SelectedItem.Value + ",0)");
                }

                DataTable dd = balayer.GetDataTable("SELECT MemberID,Head_Id,MemberName,GrpMemberID FROM svcf.membertogroupmaster where GroupID=" + ddlGroupNumber.SelectedItem.Value + "");
                for (int i = 0; i < a; i++)
                {
                    string GetMemberId1 = dd.Rows[i][0].ToString();
                    string GetHeadId1 = dd.Rows[i][1].ToString();
                    long GetChitKasarAmount1 = trn.insertorupdateTrn("insert into voucher (`DualTransactionKey`,`BranchID`,`CurrDate`,`Voucher_No`,`Voucher_Type`,`Head_Id`,`ChoosenDate`,`Narration`,`Amount`,`Series`,`ReceievedBy`,`Trans_Type`,`T_Day`,`T_Month`,`T_Year`,`MemberID`,`Trans_Medium`,`RootID`,`ChitGroupId`,`Other_Trans_Type`) values (" + DualTransactionKey + "," + balayer.ToobjectstrEvenNull(Session["Branchid"]) + ",'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," + txtPaymentNumber.Text + ",'C'," + GetHeadId1 + ",'" + balayer.indiandateToMysqlDate(txtDate.Text) + "','Chit Kasar Amount for " + dd.Rows[i][3].ToString() + " and Draw Number : " + txtDrawNumber.Text + "'," + txtChitKasarAmount.ToolTip + ",'" + txtSeries.Text + "','" + balayer.MySQLEscapeString(balayer.ToobjectstrEvenNull(Session["UserName"])) + "',1," + balayer.indiandateToMysqlDate(txtDate.Text).Split('/')[2] + "," + balayer.indiandateToMysqlDate(txtDate.Text).Split('/')[1] + "," + balayer.indiandateToMysqlDate(txtDate.Text).Split('/')[0] + "," + GetMemberId1 + ",0,5," + ddlGroupNumber.SelectedItem.Value + ",5)");
                }
                long GetBankAmount = trn.insertorupdateTrn("insert into voucher (`DualTransactionKey`,`BranchID`,`CurrDate`,`Voucher_No`,`Voucher_Type`,`Head_Id`,`ChoosenDate`,`Narration`,`Amount`,`Series`,`ReceievedBy`,`Trans_Type`,`T_Day`,`T_Month`,`T_Year`,`MemberID`,`Trans_Medium`,`RootID`,`ChitGroupId`,`Other_Trans_Type`) values (" + DualTransactionKey + "," + balayer.ToobjectstrEvenNull(Session["Branchid"]) + ",'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," + txtPaymentNumber.Text + ",'C'," + BranchHeadId + ",'" + balayer.indiandateToMysqlDate(txtDate.Text) + "','Other Branch Amount for " + strNarration + "'," + txtBankAmount.Text + ",'" + txtSeries.Text + "','" + balayer.MySQLEscapeString(balayer.ToobjectstrEvenNull(Session["UserName"])) + "',2," + balayer.indiandateToMysqlDate(txtDate.Text).Split('/')[2] + "," + balayer.indiandateToMysqlDate(txtDate.Text).Split('/')[1] + "," + balayer.indiandateToMysqlDate(txtDate.Text).Split('/')[0] + "," + GetMemberId + ",1,1," + ddlGroupNumber.SelectedItem.Value + ",0) ");
                //string DebitNarration = ddlMemberName.SelectedItem.Text.Split('=')[0] + " Draw Number :" + txtDrawNumber.Text + " payment Breakup as follows:\r\nPrized Amount Rs. : " + (Convert.ToDecimal(txtIncidentialCharge.Text) + Convert.ToDecimal(txtBankAmount.Text) + Convert.ToDecimal(txtServiceCharge.Text) + Convert.ToDecimal(txtGST.Text));
                string DebitNarration = ddlMemberName.SelectedItem.Text.Split('=')[0] + " Draw Number :" + txtDrawNumber.Text + " payment Breakup as follows:\r\nPrized Amount Rs. : " + (Convert.ToDecimal(txtIncidentialCharge.Text) + Convert.ToDecimal(txtBankAmount.Text) +  Convert.ToDecimal(txtGST.Text));
                if (Convert.ToDecimal(txtCommision.Text) > 0)
                {
                    DebitNarration += "\r\nCommision : " + txtCommision.Text;
                }
                if (Convert.ToDecimal(txtChitKasarAmount.Text) > 0)
                {
                    DebitNarration += "\r\nChit Kasar Amount : " + txtChitKasarAmount.Text;
                }
                if (Convert.ToDecimal(txtLoanAmount.Text) > 0)
                {
                    DebitNarration += "\r\nLoan Amount : " + txtLoanAmount.Text;
                }
                if (Convert.ToDecimal(futurecall) > 0)
                {
                    DebitNarration += "\r\nFuture Call Amount : " + futurecall;
                }
                long GetDebitAmount = trn.insertorupdateTrn("insert into voucher (`DualTransactionKey`,`BranchID`,`CurrDate`,`Voucher_No`,`Voucher_Type`,`Head_Id`,`ChoosenDate`,`Narration`,`Amount`,`Series`,`ReceievedBy`,`Trans_Type`,`T_Day`,`T_Month`,`T_Year`,`MemberID`,`Trans_Medium`,`RootID`,`ChitGroupId`,`Other_Trans_Type`) values (" + DualTransactionKey + "," + balayer.ToobjectstrEvenNull(Session["Branchid"]) + ",'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," + txtPaymentNumber.Text + ",'D'," + GetHeadId + ",'" + balayer.indiandateToMysqlDate(txtDate.Text) + "','" + DebitNarration + "'," + txtDebitAmount.Text + ",'" + txtSeries.Text + "','" + balayer.MySQLEscapeString(balayer.ToobjectstrEvenNull(Session["UserName"])) + "',2," + balayer.indiandateToMysqlDate(txtDate.Text).Split('/')[2] + "," + balayer.indiandateToMysqlDate(txtDate.Text).Split('/')[1] + "," + balayer.indiandateToMysqlDate(txtDate.Text).Split('/')[0] + "," + GetMemberId + ",0,5," + ddlGroupNumber.SelectedItem.Value + ",0)");
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
                        //string bbbbb = balayer.GetSingleValue("SELECT TransactionKey FROM svcf.voucher where BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and Head_Id=" + ddlMemberName.SelectedItem.Value + " and DualTransactionKey=" + DualTransactionKey + " and Voucher_Type='D'");
                        long TransPaymentQuery = trn.insertorupdateTrn("insert into trans_payment (`TransactionKey`,`DualTransactionKey`,`BranchID`,`ChitGroupID`,`DrawNo`,`PaymentApplyedOn`,`ApprovedOn`,`PaymentDate`,`GuarantorID`,`Description`,`ChitAmount`,`PrizedAmount`,`AuctionDate`,`NextDueAmount`,`NextDrawNo`,`AOSanctionNo`,`CurrentDueAmount`,`TokenNumber`,`GuarantorName`,`Flags`) values (" + GetDebitAmount + "," + DualTransactionKey + "," + balayer.ToobjectstrEvenNull(Session["Branchid"]) + "," + ddlGroupNumber.SelectedItem.Value + "," + txtDrawNumber.Text + ",'" + balayer.indiandateToMysqlDate(txtApplyedOn.Text) + "','" + balayer.indiandateToMysqlDate(txtPaymentonDate.Text) + "','" + balayer.indiandateToMysqlDate(txtDate.Text) + "','" + 0 + "','" + balayer.MySQLEscapeString(txtDescription.Text) + "'," + chitvalue + "," + Priced + ",'" + balayer.indiandateToMysqlDate(AuctionDt) + "'," + NextDue + "," + lastdraw + "," + txtAOSanctiion.Text + "," + CurrentDue + "," + ddlMemberName.SelectedItem.Value + ",'" + balayer.MySQLEscapeString(txtGurantor.Text) + "',1)");
                    }
                    else
                    {
                        DataTable auction = balayer.GetDataTable("SELECT `auctiondetails`.`PrizedMemberID`,`auctiondetails`.`GroupID`,`auctiondetails`.`AuctionDate`,`auctiondetails`.`NextDueAmount`,`auctiondetails`.`PrizedAmount`,`groupmaster`.`ChitValue`,`auctiondetails`.`CurrentDueAmount`  FROM svcf.auctiondetails left join groupmaster on (`auctiondetails`.`GroupID`=`groupmaster`.`Head_Id`) where `auctiondetails`.`GroupID`=" + ddlGroupNumber.SelectedItem.Value + " and `auctiondetails`.`PrizedMemberID`=" + ddlMemberName.SelectedItem.Value + " and `auctiondetails`.`BranchID`=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + "");
                        string AuctionDt = auction.Rows[0]["AuctionDate"].ToString();
                        decimal NextDue = Convert.ToDecimal(auction.Rows[0]["NextDueAmount"]);
                        decimal Priced = Convert.ToDecimal(auction.Rows[0]["PrizedAmount"]);
                        decimal chitvalue = Convert.ToDecimal(auction.Rows[0]["ChitValue"]);
                        decimal CurrentDue = Convert.ToDecimal(auction.Rows[0]["CurrentDueAmount"]);
                        //int bbbbb = Convert.ToInt32(balayer.GetSingleValue("SELECT TransactionKey FROM svcf.voucher where BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and Head_Id=" + ddlMemberName.SelectedItem.Value + " and DualTransactionKey=" + DualTransactionKey + " and Voucher_Type='D'"));
                        long TransPaymentQuery = trn.insertorupdateTrn("insert into trans_payment (`TransactionKey`,`DualTransactionKey`,`BranchID`,`ChitGroupID`,`DrawNo`,`PaymentApplyedOn`,`ApprovedOn`,`PaymentDate`,`GuarantorID`,`Description`,`ChitAmount`,`PrizedAmount`,`AuctionDate`,`NextDueAmount`,`NextDrawNo`,`AOSanctionNo`,`CurrentDueAmount`,`TokenNumber`,`GuarantorName`,`Flags`) values (" + GetDebitAmount + "," + DualTransactionKey + "," + balayer.ToobjectstrEvenNull(Session["Branchid"]) + "," + ddlGroupNumber.SelectedItem.Value + "," + txtDrawNumber.Text + ",'" + balayer.indiandateToMysqlDate(txtApplyedOn.Text) + "','" + balayer.indiandateToMysqlDate(txtPaymentonDate.Text) + "','" + balayer.indiandateToMysqlDate(txtDate.Text) + "','" + 0 + "','" + balayer.MySQLEscapeString(txtDescription.Text) + "'," + chitvalue + "," + Priced + ",'" + balayer.indiandateToMysqlDate(AuctionDt) + "'," + NextDue + ",null," + txtAOSanctiion.Text + "," + CurrentDue + "," + ddlMemberName.SelectedItem.Value + ",'" + balayer.MySQLEscapeString(txtGurantor.Text) + "',1)");
                    }
                }
                DrawNo = txtDrawNumber.Text;
                long s = trn.insertorupdateTrn("update auctiondetails set IsPrized='Y' where PrizedMemberID=" + ddlMemberName.SelectedValue + " and DrawNO=" + DrawNo + "");
                trn.insertorupdateTrn("update svcf.branchpayment set Status=1 where ChitNumber=" + ddlMemberName.SelectedValue);
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
                logger.Info("OtherBranchPaymentPaymet.aspx - btn_ok():  Completed: " + DateTime.Now + " by: " + Convert.ToString(Session["UserName"]) + "");
            }
            catch (Exception error)
            {
                try
                {
                    trn.RollbackTrn();
                    balayer.GetInsertItem("update auctiondetails set IsPrized='N' where PrizedMemberID='" + ddlMemberName.SelectedValue + "' and DrawNO=" + DrawNo);
                    logger.Info("OtherBranchPaymentPaymet.aspx - btn_ok():  Error:  " + error.Message + ": " + DateTime.Now + " by: " + Convert.ToString(Session["UserName"]) + "");
                }
                catch (Exception ex)
                { }
                finally
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Warning", "alert('" + error.Message.ToString().Replace("'", "\\'") + "');", true);
                }
                //balayer.GetInsertItem("delete FROM `svcf`.`voucher` where DualTransactionKey=" + DualTransactionKey + ";");
                //balayer.GetInsertItem("delete FROM `svcf`.`trans_payment` where DualTransactionKey=" + DualTransactionKey + "");


            }
            finally
            {
                trn.DisposeTrn();
            }
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
    }
}