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
using System.Globalization;

namespace SreeVisalamChitFundLtd_phase1
{
    public partial class TRROtherBranch : System.Web.UI.Page
    {
        #region Object
        BusinessLayer balayer = new BusinessLayer();
        TransactionLayer trn = new TransactionLayer();
        #endregion

        #region VarDeclaration
        Dictionary<string, string> Tempdic = new Dictionary<string, string>();
        List<string> TempList = new List<string>();

        string mindat, maxdt;
        static string[] ddltooltip;
       // static List<string> RefNo = new List<string>();
       // static List<string> RefNo1 = new List<string>();
        static long RCNumber = 0;

        string query = "";
        string userinfo = "";
        string qry = "";
        string usrRole = "";
        string memid = "";
        #endregion

        ILog logger = log4net.LogManager.GetLogger(typeof(TRROtherBranch));
        string narrationname = "";
        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (Session["UserName"] == null || Session["Branchid"] == null || Session["BranchName"] == null)
            {
                Response.Redirect(Page.ResolveUrl("~/Login.aspx"), true);
            }
        }
        protected void Page_Init(object sender, EventArgs e)
        {
            //img16List.ImageUrl = Page.ResolveUrl("~/pertho_admin_v1.3/img/ico/icSw2/16-List.png");
            //Image img = (Image)UpdateProgress1.FindControl("imgWaiting");
            //img.ImageUrl = Page.ResolveUrl("~/Styles/Image/waiting.gif");
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            pnlmsg.Visible = false;
            Pnlgendrate.Visible = false;
            if (!Page.IsPostBack)
            {
                userinfo = HttpContext.Current.User.Identity.Name;
                qry = "select rs.name from roles as rs inner join rights as rt on (rt.roleid=rs.id) where memberid=" + userinfo + "";
                usrRole = balayer.GetSingleValue(qry);
                if (usrRole == "Report")
                {
                    Response.Redirect(Page.ResolveUrl("~/Home.aspx"), true);
                }
                //rvDate.MinimumValue = balayer.GetSingleValue("SELECT DATE_FORMAT( min(ChoosenDate), '%d/%m/%Y') MinimumDate FROM svcf.voucher where " +
                //    "BranchID=" + Session["Branchid"] + " and ChoosenDate<>'0000-00-00'");
                //if (rvDate.MinimumValue=="")
                //{
                //    rvDate.MinimumValue = DateTime.Now.ToString("dd/MM/yyyy");
                //}
                //rvDate.MinimumValue = balayer.GetSingleValue("SELECT DATE_FORMAT( MinimumDate, '%d/%m/%Y') MinimumDate FROM svcf.restrictionmaster where BranchID=" + Session["Branchid"]);
                //rvDate.MaximumValue = DateTime.Now.ToString("dd/MM/yyyy");
                rvChequeDate.MinimumValue = "01/02/2013";
                rvChequeDate.MaximumValue = DateTime.Now.ToString("dd/MM/yyyy");
                Session["CheckRefresh"] = System.Guid.NewGuid().ToString();
                SetInitialRow();
                CollectorName();
                balayer.fillBankHd(ddlBanklDetails);
                ddlColloctorName.Focus();
                LoadbranchList();
                FillDropDownList(ddlMisc, 2, "");
                fillBankHead();
                fillEmployee();
                lbCardType.Visible = false;
                lbIdCardNumber.Visible = false;
                txtIdcardNumber.Visible = false;
                ddlCardType.Visible = false;
                cvddlCardType.Visible = false;
                rvCardNumber.Visible = false;
                string Choosendate = balayer.GetSingleValue("SELECT DATE_FORMAT(max(ChoosenDate),'%d/%m/%Y') from `svcf`.`voucher`where BranchID=" + Session["Branchid"] + " and ChoosenDate<>'0000-00-00'");
                txtReceivedDate.Text = "";
            }
            rvDate.MinimumValue = balayer.GetSingleValue("SELECT DATE_FORMAT( MinimumDate, '%d/%m/%Y') MinimumDate FROM svcf.restrictionmaster where BranchID=" + Session["Branchid"]);
            rvDate.MaximumValue = DateTime.Now.ToString("dd/MM/yyyy");
            logger.Info("TRR Other Branch - at: " + DateTime.Now + " by: " + Convert.ToString(Session["UserName"]) + "");
        }
        public void LoadbranchList()
        {
            listbranch.DataSource = null;
            DataTable dtgroupno = null;
            dtgroupno = balayer.GetDataTable("select NodeID,Node from headstree where ParentID=1 and NodeID<>" + Session["Branchid"]);
            DataRow dr = dtgroupno.NewRow();
            dr[0] = "0";
            dr[1] = "--Select--";
            listbranch.DataValueField = "NodeID";
            listbranch.DataTextField = "Node";
            dtgroupno.Rows.InsertAt(dr, 0);
            listbranch.DataSource = dtgroupno;
            listbranch.DataBind();
        }
        void fillEmployee()
        {
            DataTable dt = balayer.GetDataTable("SELECT Emp_Name FROM svcf.employee_details where BranchID="+Session["Branchid"]);
            DataRow dr = dt.NewRow();
            dr[0] = "--Select--";
            ddlEmployee.DataTextField = "Emp_Name";
            ddlEmployee.DataValueField = "Emp_Name";
            dt.Rows.InsertAt(dr, 0);
            ddlEmployee.DataSource = dt;
            ddlEmployee.DataBind();
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
        protected void ddlToken_IndexChanged(object sender, EventArgs e)
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
            DataTable dtBank = balayer.GetDataTable("SELECT concat( concat(t1.BankName,' _ ',t1.IFCCode),' _ ',t1.AccountNo) as BankDetails, t1.Head_Id as Head_Id, t1.Banklocation as Banklocation FROM svcf.bankdetails as t1 where t1.BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]));
            DataRow dr = dtBank.NewRow();
            dr[0] = "--Select--";
            dr[1] = "0";
            ddlBankHead.DataValueField = "Head_Id";
            ddlBankHead.DataTextField = "BankDetails";
            dtBank.Rows.InsertAt(dr, 0);
            ddlBankHead.DataSource = dtBank;
            ddlBankHead.DataBind();
        }
        public void CollectorName()
        {
            DataTable dtCollector = balayer.GetDataTable("Select moneycollid,moneycollname from moneycollector where BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]));
            DataRow dr = dtCollector.NewRow();
            dr[0] = "0";
            dr[1] = "--Select--";
            ddlColloctorName.DataValueField = "moneycollid";
            ddlColloctorName.DataTextField = "moneycollname";
            dtCollector.Rows.InsertAt(dr, 0);
            ddlColloctorName.DataSource = dtCollector;
            ddlColloctorName.DataBind();
            ddlReceiptSeries.Focus();
        }
        //protected void ddlReceiptSeries_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    getRecieptBookNO(ddlReceiptSeries.SelectedValue, ddlColloctorName.SelectedValue);
        //    ddlEmployee.Focus();
        //}
        //public void getRecieptBookNO(string Series, string CollectorID)
        //{
        //    DataTable dtAll = balayer.GetDataTable("SELECT  alreadyusedreceipts,receiptnoto   FROM svcf.assignreceiptbook where  moneycollid=" + ddlColloctorName.SelectedValue + "  and IsFinished=0 and BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and receiptseries='" + ddlReceiptSeries.SelectedItem.Text + "'");
        //    if (dtAll.Rows.Count != 0)
        //    {
        //        int from = int.Parse(dtAll.Rows[0][0].ToString());
        //        int t0 = int.Parse(dtAll.Rows[0][1].ToString());
        //        string strQuery = "select ifnull(max(Voucher_No)+1,0) from voucher where BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and Trans_Type=1 and Voucher_No>=" + from + " and Voucher_No<=" + t0 + " and `Series`='" + ddlReceiptSeries.SelectedItem.Text + "'";
        //        int RecNo = int.Parse(balayer.GetSingleValue(strQuery));
        //        TextBox ReceiptNo = (TextBox)GridView1.Rows[GridView1.Rows.Count - 1].FindControl("txtReceiptNo");
        //        if (RecNo != 0)
        //        {
        //            ReceiptNo.Text = RecNo.ToString();
        //        }
        //        else
        //        {
        //            ReceiptNo.Text = from.ToString();
        //        }
        //    }
        //    else
        //    {
        //        TextBox ReceiptNo = (TextBox)GridView1.Rows[GridView1.Rows.Count - 1].FindControl("txtReceiptNo");
        //        ReceiptNo.Text = "0";
        //        ScriptManager.RegisterStartupScript(this, GetType(), "Warning", "alert(' Please Assign new Reciept Book!!!');", true);
        //    }
        //}
        void series()
        {
            DataTable dtMC = balayer.GetDataTable("Select distinct receiptseries from assignreceiptbook where moneycollid='" + ddlColloctorName.SelectedValue + "' and IsFinished=0");
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

            ddlColloctorName.Focus();
            SetInitialRow();
        }
        protected void ddlColloctorName_SelectedIndexChanged(object sender, EventArgs e)
        {
            series();        
        }
        private void FillDropDownList(DropDownList ddl, int iType, string MemberID)
        {  //chit
            if (iType == 0)
            {
                ddl.DataSource = null;
                DataTable dtgroupno = null;
                dtgroupno = balayer.GetDataTable("select NodeID,Node from headstree where ParentID=1 and NodeID<>" + Session["Branchid"]);
                DataRow dr = dtgroupno.NewRow();
                dr[0] = "0";
                dr[1] = "--Select--";
                ddl.DataValueField = "NodeID";
                ddl.DataTextField = "Node";
                dtgroupno.Rows.InsertAt(dr, 0);
                ddl.DataSource = dtgroupno;
                
                ddl.DataBind();
            }
            //token
            else if (iType == 1)
            {
                //DropDownList ddlMemberName = (DropDownList)GridView1.Rows[0].Cells[1].FindControl("ddlMemberName");
                //if (ddlMemberName.SelectedItem.Text != "--Select--")
                //{
                //    ddl.DataSource = null;
                //    DataTable dt = null;
                //    dt = balayer.GetDataTable(@"SELECT GrpMemberID,Head_Id FROM membertogroupmaster where  MemberID=" + MemberID );
                //    DataRow dr = dt.NewRow();
                //    dr[0] = "--Select--";
                //    dr[1] = "0";
                //    dt.Rows.InsertAt(dr, 0);
                //    ddl.DataSource = dt;
                //    ddl.DataValueField = "Head_Id";
                //    ddl.DataTextField = "GrpMemberID";
                //    ddl.DataBind();
                //}
                //else
                //{
                //    ddl.DataSource = null;
                //    ddl.DataSource = null;
                //    DataTable dt = balayer.GetDataTable(@"SELECT GrpMemberID,Head_Id FROM membertogroupmaster where  MemberID=0");
                //    DataRow dr = dt.NewRow();
                //    dr[0] = "--Select--";
                //    dr[1] = "0";
                //    dt.Rows.InsertAt(dr, 0);
                //    ddl.DataBind();
                //}
            }
            //misc
            else if (iType == 2)
            {
                ddl.DataSource = null;
                DataTable dtgroupno = null;
                dtgroupno = balayer.GetDataTable("SELECT concat(cast(TreeID as char),',',cast(RootID as char)) as TreeID,TREE FROM svcf.view_parent where RootID not in(5,6,3) and (BranchID is null or BranchID=" + Session["Branchid"] + ")");
                DataRow dr = dtgroupno.NewRow();
                dr[0] = "0,0";
                dr[1] = "--Select--";
                ddl.DataValueField = "TreeID";
                ddl.DataTextField = "TREE";
                dtgroupno.Rows.InsertAt(dr, 0);
                ddl.DataSource = dtgroupno;
                ddl.DataBind();
            }
        }
        private void SetInitialRow()
        {
             try
            {
                DataTable dt = new DataTable();
                DataRow dr = null;
                //Define the Columns
                dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
                dt.Columns.Add(new DataColumn("BranchName", typeof(string)));
                dt.Columns.Add(new DataColumn("chittoken", typeof(string)));                
                dt.Columns.Add(new DataColumn("Narration", typeof(string)));
                dt.Columns.Add(new DataColumn("Amount", typeof(string)));
                dt.Columns.Add(new DataColumn("MiscHead", typeof(string)));
                dt.Columns.Add(new DataColumn("MiscAmount", typeof(string)));                              
                dt.Columns.Add(new DataColumn("GrpTokenid", typeof(string)));
                dt.Columns.Add(new DataColumn("Branchid", typeof(string)));
                dt.Columns.Add(new DataColumn("RCNumber", typeof(string)));
                dt.Columns.Add(new DataColumn("firstmisc", typeof(string)));
                dt.Columns.Add(new DataColumn("secmisc", typeof(string)));

                
                //Add a Dummy Data on Initial Load
                dr = dt.NewRow();
                //dr["RowNumber"] = 1;
                dt.Rows.Add(dr);
                //Store the DataTable in ViewState
                ViewState["CurrentTable"] = dt;
                //Bind the DataTable to the Grid
                GViewCROther_Selected.DataSource = dt;
                GViewCROther_Selected.DataBind();
            }
            catch (Exception) { }
        }
        
        private void RemoveLastRowToGrid()
        {
           // if (ViewState["CurrentTable"] != null)
           // {
           //     DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
           //     if (dtCurrentTable.Rows.Count > 1)
           //     {
           //         dtCurrentTable.Rows.RemoveAt(dtCurrentTable.Rows.Count - 1);

           //         GridView1.DataSource = dtCurrentTable;
           //         GridView1.DataBind();
           //         ViewState["CurrentTable"] = dtCurrentTable;
           //     }
           // }

           //// SetPreviousData(true);
           // TextBox ReceiptNo = (TextBox)GridView1.Rows[GridView1.Rows.Count - 1].FindControl("txtReceiptNo");
           // ReceiptNo.Focus();
        }

        public void PopulateDropDownList(List<ListItem> list, DropDownList ddl)
        {
            ddl.DataSource = list;
            ddl.DataTextField = "Text";
            ddl.DataValueField = "Value";
            ddl.DataBind();
        }

        private void AddNewRowToGrid()
        {
            try
            {

                //string selectedToken = "";
                string selectedRSeries = "";
                string GpChitToken = "";
                string LoadedSeries = "";
                string selectedbranch = "";
                string selectedchit = "";
                string selseries = "";
                string seltokenname = "";
                DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
                //if (dtCurrentTable.Rows[0][0].ToString() == "")
                //{           

                // if (txtReceiptNumber.Text != "")
                // {
                //   RCNumber = Convert.ToInt32(txtReceiptNumber.Text);
                //  }
                // }
                //Collector name
                selectedRSeries = Request.Form[ddlColloctorName.UniqueID];

                //Receipt series
                PopulateDropDownList(CRSeries_OtherBranch(selectedRSeries), ddlReceiptSeries);
                selseries = HD_RSeriesid.Value;
                ddlReceiptSeries.Items.FindByText(selseries).Selected = true;


                //Selected Branch
                selectedbranch = Request.Form[listbranch.UniqueID];
                listbranch.Items.FindByValue(selectedbranch).Selected = true;

                //Chit token
                selectedchit = tkn_id.Value;   //token id
                seltokenname = hiddentkn_text.Value;    // Selected token name
                seltokenname = seltokenname.Trim();
                //PopulateDropDownList(ChitToken(selectedbranch), ddlChitno);
                //selectedchit = HD_SelectedChit.Value;   

                //ddlBranchName.Items.FindByValue(selectedbranch).Selected = true;

                //ddlGroup.Items.FindByValue(selectedchit).Selected = true;

                //Chit Token
                //GpChitToken = Request.Form[ddlChitno.UniqueID];
                //ddlChitno.Items.FindByValue(GpChitToken).Selected = true;

                //LoadedSeries = HD_Tokenid.Value;


                //seriesid = HD_SID.Value;
                //  PopulateDropDownList(CRSeries_OtherBranch(selectedbranch), ddlReceiptSeries);
                //if (ddlReceiptSeries.Items.Count > 0)
                //{

                // }
                //GpChitToken = HD_Tokenid.Value;
                //if (ddlChitno.Items.Count > 0)
                //{
                //    ddlChitno.Items.FindByValue(GpChitToken).Selected = true;
                //}
                //ddlReceiptSeries.Items.FindByText(LoadedSeries).Selected = true;

                //txtReceiptNumber.Text = RCNumber.ToString();


                if (ViewState["CurrentTable"] != null)
                {
                    DataRow drCurrentRow = null;
                    if (dtCurrentTable.Rows.Count > 0)
                    {
                        //DropDownList ddlMiscVal = (DropDownList)GridView1.Rows[dtCurrentTable.Rows.Count - 1].FindControl("ddlMisc");
                        //TextBox txtMiscVal = (TextBox)GridView1.Rows[dtCurrentTable.Rows.Count - 1].FindControl("txtMisc");                        
                        decimal lastAmount = 0.0M;
                        bool isMisc = decimal.TryParse(txtMisc.Text, out lastAmount);
                        if ((isMisc == true & lastAmount > 0.0M))
                        {
                            isMisc = true;
                        }
                        else
                        {
                            isMisc = false;
                        }
                        if ((ddlMisc.SelectedIndex <= 0) & isMisc == true)
                        {
                            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Choose Misc Head Valid Details');", true);
                            //Response.Write("<script>alert('Please Provide Valid Details');</script>");
                            return;
                        }
                        if (ddlMisc.SelectedIndex > 0 & isMisc != true)
                        {
                            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Provide Misc Amount Details');", true);
                            return;
                            //Response.Write("<script>alert('Please Provide Valid Details');</script>");
                        }

                        for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                        {
                            drCurrentRow = dtCurrentTable.NewRow();
                            drCurrentRow["BranchName"] = listbranch.SelectedItem;    //ddlBranchName.SelectedItem;
                            drCurrentRow["chittoken"] = seltokenname;     //ddlChitno.SelectedItem;
                            drCurrentRow["Narration"] = txtNarration.Text;
                            drCurrentRow["Amount"] = txtAmount.Text;
                            drCurrentRow["MiscHead"] = ddlMisc.SelectedItem;
                            drCurrentRow["MiscAmount"] = txtMisc.Text;
                            drCurrentRow["GrpTokenid"] = selectedchit;     //ddlChitno.SelectedValue;
                            drCurrentRow["Branchid"] = listbranch.SelectedValue;      //ddlBranchName.SelectedValue;
                            drCurrentRow["RCNumber"] = txtReceiptNumber.Text;

                            if ((ddlMisc.SelectedValue != null) && (ddlMisc.SelectedValue != "0,0"))
                            {
                                if (ddlMisc.SelectedValue.Contains(','))
                                {
                                    drCurrentRow["firstmisc"] = ddlMisc.SelectedValue.Split(',')[0];
                                    drCurrentRow["secmisc"] = ddlMisc.SelectedItem.Value.Split(',')[1];
                                }
                                else
                                {
                                    drCurrentRow["firstmisc"] = "0";
                                    drCurrentRow["secmisc"] = "0";
                                }
                            }
                            else
                            {
                                drCurrentRow["firstmisc"] = "0";
                                drCurrentRow["secmisc"] = "0";
                            }
                        }
                        //Rebind the Grid with the current data
                        //Remove initial blank row
                        if (dtCurrentTable.Rows[0][0].ToString() == "")
                        {
                            dtCurrentTable.Rows[0].Delete();
                            dtCurrentTable.AcceptChanges();
                        }
                        drCurrentRow["RowNumber"] = dtCurrentTable.Rows.Count + 1;
                        dtCurrentTable.Rows.Add(drCurrentRow);
                        ViewState["CurrentTable"] = dtCurrentTable;

                        //if (ddlMisc.SelectedValue != null)
                        //{
                        //    RefNo.Add(ddlMisc.SelectedValue.Split(',')[1]);
                        //    RefNo1.Add(ddlMisc.SelectedItem.Value.Split(',')[0]);
                        //}
                        //Rebind the Grid with the current data
                        GViewCROther_Selected.DataSource = dtCurrentTable;
                        GViewCROther_Selected.DataBind();

                        //Clear Existing Values                        
                        // ddlBranchName.ClearSelection();
                        listbranch.ClearSelection();
                        //ddlChitno.ClearSelection();
                        // ddlChitno.Items.Clear();
                        txtNarration.Text = "";
                        txtMisc.Text = "";
                        txtAmount.Text = "";
                        ddlMisc.ClearSelection();
                        btnGenerate.Focus();
                    }

                }

                //SetPreviousData(false);
            }
            catch (Exception e)
            { }
        }
        //private void SetPreviousData(bool isRemove)
        //{
        //    int rowIndex = 0;
        //    if (ViewState["CurrentTable"] != null)
        //    {
        //        DataTable dt = (DataTable)ViewState["CurrentTable"];
        //        if (dt.Rows.Count > 0)
        //        {
        //            for (int i = 0; i < dt.Rows.Count; i++)
        //            {
        //                //Set the Previous Selected Items on Each DropDownList on Postbacks
        //                DropDownList ddlBranchName = (DropDownList)GridView1.Rows[i].FindControl("ddlBranchName");
        //                TextBox txtNarration = (TextBox)GridView1.Rows[i].FindControl("txtNarration");
        //                TextBox txtAmount = (TextBox)GridView1.Rows[i].FindControl("txtAmount");
        //                DropDownList ddlMisc = (DropDownList)GridView1.Rows[i].FindControl("ddlMisc");
        //                TextBox txtMisc = (TextBox)GridView1.Rows[i].FindControl("txtMisc");
        //                TextBox txtReceiptNo = (TextBox)GridView1.Rows[i].FindControl("txtReceiptNo");
        //                //Fill the DropDownList with Data
        //                FillDropDownList(ddlBranchName, 0, "");
        //                FillDropDownList(ddlMisc, 2, "");
        //                //FillDropDownList(ddl3);
        //                if (i < dt.Rows.Count)
        //                {
        //                    if (isRemove == false & i == dt.Rows.Count - 1)
        //                    {
        //                        break;
        //                    }
        //                    ddlBranchName.ClearSelection();
        //                    ddlBranchName.Items.FindByValue(dt.Rows[i]["BranchName"].ToString()).Selected = true;
                            
        //                    ddlMisc.ClearSelection();
        //                    ddlMisc.Items.FindByValue(dt.Rows[i]["MiscHead"].ToString()).Selected = true;
        //                    txtNarration.Text = dt.Rows[i]["Narration"].ToString();
        //                    txtAmount.Text = dt.Rows[i]["Amount"].ToString();
        //                    txtMisc.Text = dt.Rows[i]["MiscAmount"].ToString();
        //                    txtReceiptNo.Text = dt.Rows[i]["txtReceiptNo"].ToString();
        //                    //ddl3.ClearSelection();
        //                    //ddl3.Items.FindByValue(dt.Rows[i]["Column3"].ToString()).Selected = true;
        //                }
        //                rowIndex++;
        //            }
        //            if (isRemove == false)
        //            {
        //                ((TextBox)GridView1.Rows[GridView1.Rows.Count - 1].FindControl("txtReceiptNo")).Text = (Convert.ToInt32(((TextBox)GridView1.Rows[GridView1.Rows.Count - 2].FindControl("txtReceiptNo")).Text)+1).ToString();
        //            }
        //        }
        //    }
        //}
        public List<string> Code
        {
            get
            {
                if (HttpContext.Current.Session["Code"] == null)
                {
                    HttpContext.Current.Session["Code"] = new List<string>();
                }
                return HttpContext.Current.Session["Code"] as List<string>;
            }
            set
            {
                HttpContext.Current.Session["Code"] = value;
            }

        }
        void visibility2()
        {
            //for (int i = 0; i < GridView1.Rows.Count; i++)
            //{
            //    DropDownList ddl = (DropDownList)GridView1.Rows[i].FindControl("ddlBranchName");
            //    if (Code[0] != ddl.SelectedItem.Value)
            //    {
            //        Session["fill"] = "";
            //        break;
            //    }
            //    else
            //    {
            //        Session["fill"] = null;
            //        //  break;
            //    }
            //}
            if (Session["fill"] != null)
            {
                lbCardType.Visible = true;
                lbIdCardNumber.Visible = true;
                txtIdcardNumber.Visible = true;
                ddlCardType.Visible = true;
                cvddlCardType.Visible = true;
                rvCardNumber.Visible = true;
                return;
            }
            else
            {
                lbCardType.Visible = false;
                lbIdCardNumber.Visible = false;
                txtIdcardNumber.Visible = false;
                ddlCardType.Visible = false;
                cvddlCardType.Visible = false;
                rvCardNumber.Visible = false;
            }
        }
        protected void ButtonRemove_Click(object sender, ImageClickEventArgs e)
        {
            //if (GridView1.Rows.Count > 1)
            //{
            //    RemoveLastRowToGrid();
            //    for (int i = GridView1.Rows.Count - 1; i < GridView1.Rows.Count; i++)
            //    {
            //        DropDownList ddl = (DropDownList)GridView1.Rows[i].FindControl("ddlBranchName");
            //        ddl.Enabled = true;
            //    }
            //    for (int i = 0; i < GridView1.Rows.Count - 1; i++)
            //    {
            //        DropDownList ddl = (DropDownList)GridView1.Rows[i].FindControl("ddlBranchName");
            //        ddl.Enabled = false;
            //    }
            //    visibility2();
            //}
            //else
            //{
            //    lbCardType.Visible = false;
            //    lbIdCardNumber.Visible = false;
            //    txtIdcardNumber.Visible = false;
            //    ddlCardType.Visible = false;
            //    cvddlCardType.Visible = false;
            //    rvCardNumber.Visible = false;
            //}
        }
        protected void ButtonAdd_Click(object sender, ImageClickEventArgs e)
        {
            //Page.Validate("GrpRow");
            //foreach (GridViewRow gvRow in GridView1.Rows)
            //{
            //    ((RequiredFieldValidator)gvRow.FindControl("RFVtxtAmount")).Validate();
            //    ((CompareValidator)gvRow.FindControl("CVddlBranchName")).Validate();
            //    ((RequiredFieldValidator)gvRow.FindControl("RFVGrpRowtxtReceiptNo")).Validate();
            //}
            //if (Page.IsValid)
            //{

                AddNewRowToGrid();
                //for (int i = 0; i < GridView1.Rows.Count-1; i++)
                //{
                //    DropDownList ddl = (DropDownList)GridView1.Rows[i].FindControl("ddlBranchName");
                //    ddl.Enabled = false;
                //}
           // }
        }
        protected void btnConfirmationNo_Click(object sender, EventArgs e)
        {
            gvConfirm.DataSource = null;
            gvConfirm.DataBind();
            ModalPopupExtender1.Hide();
            pnlConfirmation.Visible = false;
        }


        protected void btnConfirmationYes_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["CheckRefresh"].ToString() != ViewState["CheckRefresh"].ToString())
                {
                    return;
                }
                decimal dblTotalAmount = decimal.Parse(txtTotalAmount.Text);
                decimal dblDueAmount = 0.0M;
                decimal dblDueTemp;
                bool isMisc;
                bool isMiscIssue = false;
                string mischead = "";
                string miscamnt = "";

                string selectedRSeries = "";
                string selectedbranch = "";


                string selseries = "";
                dblTotalAmount = decimal.Parse(txtTotalAmount.Text);
                dblDueAmount = 0.0M;
                isMiscIssue = false;
                RCNumber = Convert.ToInt32(txtReceiptNumber.Text);


                string LoadedSeries = "";
                string Rcptnumber = "";
                selectedRSeries = Request.Form[ddlColloctorName.UniqueID];
                //LoadedSeries = Request.Form[ddlReceiptSeries.UniqueID];
                LoadedSeries = HD_RSeriesid.Value;

                LoadedSeries.Trim();
                PopulateDropDownList(CRSeries_OtherBranch(selectedRSeries), ddlReceiptSeries);
                ddlReceiptSeries.Items.FindByText(LoadedSeries).Selected = true;

                //txtReceiptNo.Text = Rcptnumber.ToString();
                // GetRCBookno(LoadedSeries, selectedRSeries);
                //TransactionLayer trn = new TransactionLayer();
                string TransactionKeyDue = "";
                gvConfirm.DataSource = null;
                gvConfirm.DataBind();


                //System.Guid guid = Guid.NewGuid();
                //string hexstring = BitConverter.ToString(guid.ToByteArray());
                //string guidForBinary16 = "0x" + hexstring.Replace("-", string.Empty);
                //string DualTransactionKey = guidForBinary16;

                //stored procedure for DualTransactionKey

                //string DualTransactionKey = Convert.ToString(balayer.sp_gendratedt_key());

                //13/06/2023
                System.Guid guid = Guid.NewGuid();
                string guidForChar36 = guid.ToString();
                string hexstring = BitConverter.ToString(guid.ToByteArray());
                string guidForBinary16 = "0x" + hexstring.Replace("-", string.Empty);


                string DualTransactionKey = guidForBinary16;


                string countval = "";
                string getddl = "";
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

                    ClsSession objSession = (ClsSession)Session["objSession"];

                    string branchname = "", narration = "",  Branchspt2 = "";
                    string txtamnt = "", ddlmisc = "", txtmisc = "",  rcptno = "";
                    string MemberID = "", TokenNo = "", RootID = "", branchid="";
                    string mischd1 = "", mischd2 = "";
                   
                    for (int i = 0; i < GViewCROther_Selected.Rows.Count; i++)
                    {
                        branchname = GViewCROther_Selected.Rows[i].Cells[1].Text;
                        narration = GViewCROther_Selected.Rows[i].Cells[3].Text;
                        if (narration.Contains("&amp;"))
                        {
                            narrationname = narration.Replace("&amp;", "&");
                        }
                        txtamnt = GViewCROther_Selected.Rows[i].Cells[4].Text;
                        ddlmisc = GViewCROther_Selected.Rows[i].Cells[5].Text;
                        txtmisc = GViewCROther_Selected.Rows[i].Cells[6].Text;
                      //rcptno = GViewCROther_Selected.Rows[i].Cells[6].Text;
                        Label ChitgpTokenid = (Label)GViewCROther_Selected.Rows[i].FindControl("lblgp_tokenid");
                        Label Branchid = (Label)GViewCROther_Selected.Rows[i].FindControl("lblBranchid");
                        Label Rcptno = (Label)GViewCROther_Selected.Rows[i].FindControl("lblrcnumber");
                        //string getddl = ddlmisc.Split(';')[2];
                       // string countval = balayer.GetSingleValue("select count(*) from svcf.headstree where ParentID=1 and node ='" + getddl + "';");
                       
                        
                            //ddlmisc = ddlmisc.Replace("&gt;&gt;", ">>");
                        
                       
                        string excesspayment = Convert.ToString(ViewState["excesspayment"]);


                        if (txtmisc == "&nbsp;")
                        {
                            txtmisc = "0.0";
                        }
                        if (ddlmisc != "--Select--")
                        {
                            Label r1 = (Label)GViewCROther_Selected.Rows[i].FindControl("lblref1");
                            Label r2 = (Label)GViewCROther_Selected.Rows[i].FindControl("lblref2");

                            mischd1 = r1.Text;
                            mischd2 = r2.Text;
                           // ddlmisc = ddlmisc.Replace("&gt;&gt;", ">>");
                           // Branchspt2 = ddlmisc.Split('>')[2];
                        }

                        if (ddlmisc == "--Select--")
                        {
                            ddlmisc = "";
                        }
                        else
                        {
                            getddl = ddlmisc.Split(';')[2];
                            countval = balayer.GetSingleValue("select count(*) from svcf.headstree where ParentID=1 and node ='" + getddl + "';");
                            ddlmisc = ddlmisc.Replace("&gt;&gt;", ">>");
                        }
                        if (countval == "1")
                        {
                            ddlmisc = " ";
                        }


                        string GroupID = "0";
                        string ChitsBranchID = listbranch.SelectedValue;
                        MemberID = "0";
                        TokenNo = ChitgpTokenid.Text;
                        rcptno = Rcptno.Text;
                        branchid = Branchid.Text;
                        RootID = "";

                        GroupID = balayer.GetSingleValue("select GroupID from membertogroupmaster where Head_Id=" + TokenNo);
                        ChitsBranchID = balayer.GetSingleValue("SELECT BranchID FROM `svcf`.`groupmaster` where Head_Id=" + GroupID);
                        //string GroupID = "0";

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

                            //if (decimal.Parse(txtamnt) != 0.00M)
                            //{
                            //    string strChitHeadQuery = "INSERT INTO `svcf`.`voucher` (`DualTransactionKey`,`BranchID`,`CurrDate`,`Voucher_No`,`Voucher_Type`,`Head_Id`,`ChoosenDate`,`Narration`,`Amount`,`Series`,`ReceievedBy`,`Trans_Type`,`T_Day`,`T_Month`,`T_Year`,`MemberID`,`Trans_Medium`,`RootID`,`ChitGroupId`,`M_Id`,LoginIP) VALUES (" + DualTransactionKey + "," + balayer.ToobjectstrEvenNull(Session["Branchid"]) + ",'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," + rcptno + ",'C'," + TokenNo + ",'" + dtChoosenDate.ToString("yyyy-MM-dd") + "','" + (GViewCROther_Selected.Rows[i].Cells[2].Text + ":" + balayer.MySQLEscapeString(GViewCROther_Selected.Rows[i].Cells[3].Text)) + "'," + txtamnt + ",'" + ddlReceiptSeries.SelectedItem.Text + "','" + ddlEmployee.SelectedItem.Text + "',1," + dtChoosenDate.Day + "," + dtChoosenDate.Month + "," + dtChoosenDate.Year + "," + MemberID + "," + trans_medium + ",5," + GroupID + "," + ddlColloctorName.SelectedItem.Value + ",'" + objSession.LoginIp + "') ";
                            //    string strCashHeadQuery = "INSERT INTO `svcf`.`voucher` (`DualTransactionKey`,`BranchID`,`CurrDate`,`Voucher_No`,`Voucher_Type`,`Head_Id`,`ChoosenDate`,`Narration`,`Amount`,`Series`,`ReceievedBy`,`Trans_Type`,`T_Day`,`T_Month`,`T_Year`,`MemberID`,`Trans_Medium`,`RootID`,`ChitGroupId`,`M_Id`,LoginIP) VALUES (" + DualTransactionKey + "," + balayer.ToobjectstrEvenNull(Session["Branchid"]) + ",'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," + rcptno + ",'D'," + CashOrBankID + ",'" + dtChoosenDate.ToString("yyyy-MM-dd") + "','" + (GViewCROther_Selected.Rows[i].Cells[2].Text + ":" + balayer.MySQLEscapeString(GViewCROther_Selected.Rows[i].Cells[3].Text)) + "'," + txtamnt + ",'" + ddlReceiptSeries.SelectedItem.Text + "','" + ddlEmployee.SelectedItem.Text + "',1," + dtChoosenDate.Day + "," + dtChoosenDate.Month + "," + dtChoosenDate.Year + "," + MemberID + "," + trans_medium + "," + RootID + "," + GroupID + "," + ddlColloctorName.SelectedItem.Value + ",'" + objSession.LoginIp + "') ";
                            //    long strChitHead = trn.insertorupdateTrn(strChitHeadQuery);
                            //    long strCashHead = trn.insertorupdateTrn(strCashHeadQuery);
                            //    if (CheckBox1.Checked == true)
                            //    {
                            //        TransactionKeyDue = strCashHead.ToString();
                            //    }
                            //}
                            //if (ddlmisc != "")
                            //{
                            //    if (decimal.Parse(txtmisc) != 0.00M)
                            //    {
                            //        string strChitMiscHeadQuery = "INSERT INTO `svcf`.`voucher` (`DualTransactionKey`,`BranchID`,`CurrDate`,`Voucher_No`,`Voucher_Type`,`Head_Id`,`ChoosenDate`,`Narration`,`Amount`,`Series`,`ReceievedBy`,`Trans_Type`,`T_Day`,`T_Month`,`T_Year`,`MemberID`,`Trans_Medium`,`RootID`,`ChitGroupId`,`M_Id`,LoginIP) VALUES (" + DualTransactionKey + "," + balayer.ToobjectstrEvenNull(Session["Branchid"]) + ",'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," + rcptno + ",'C'," + mischd1 + ",'" + dtChoosenDate.ToString("yyyy-MM-dd") + "','" + Branchspt2 + "'," + ddlmisc + ",'" + ddlReceiptSeries.SelectedItem.Text + "','" + balayer.ToobjectstrEvenNull(Session["UserName"]) + "',1," + dtChoosenDate.Day + "," + dtChoosenDate.Month + "," + dtChoosenDate.Year + "," + MemberID + "," + trans_medium + "," + mischd2 + "," + GroupID + "," + ddlColloctorName.SelectedItem.Value + ",'" + objSession.LoginIp + "') ";
                            //        string strCashHeadMiscQuery = "INSERT INTO `svcf`.`voucher` (`DualTransactionKey`,`BranchID`,`CurrDate`,`Voucher_No`,`Voucher_Type`,`Head_Id`,`ChoosenDate`,`Narration`,`Amount`,`Series`,`ReceievedBy`,`Trans_Type`,`T_Day`,`T_Month`,`T_Year`,`MemberID`,`Trans_Medium`,`RootID`,`ChitGroupId`,`M_Id`,LoginIP) VALUES (" + DualTransactionKey + "," + balayer.ToobjectstrEvenNull(Session["Branchid"]) + ",'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," + rcptno + ",'D'," + CashOrBankID + ",'" + dtChoosenDate.ToString("yyyy-MM-dd") + "','" + Branchspt2 + "'," + ddlmisc + ",'" + ddlReceiptSeries.SelectedItem.Text + "','" + balayer.ToobjectstrEvenNull(Session["UserName"]) + "',1," + dtChoosenDate.Day + "," + dtChoosenDate.Month + "," + dtChoosenDate.Year + "," + MemberID + "," + trans_medium + "," + RootID + "," + GroupID + "," + ddlColloctorName.SelectedItem.Value + ",'" + objSession.LoginIp + "') ";
                            //        long strChitMiscHead = trn.insertorupdateTrn(strChitMiscHeadQuery);
                            //        long strCashHeadMisc = trn.insertorupdateTrn(strCashHeadMiscQuery);
                            //        if (CheckBox1.Checked == true)
                            //        {
                            //            if (!ddlCardType.Visible)
                            //            {
                            //                string strBankMiscInsertQuery = "insert into transbank(BranchID, TransactionKey, DualTransactionKey, T_Day, T_Month, T_Year, BankHeadID, Head_Id, MemberID, CustomersBankName, DateInCheque, ChequeDDNO, GivenAmount, TotalChequeDDAmount, IsBounced, Trans_Type,IsChequeAvailable) values (" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + "," + strCashHeadMisc + "," + DualTransactionKey + "," + dtChoosenDate.Day + "," + dtChoosenDate.Month + "," + dtChoosenDate.Year + "," + ddlBankHead.SelectedValue + "," + mischd1 + "," + MemberID + ",'" + ddlBanklDetails.SelectedItem.Text + "','" + balayer.indiandateToMysqlDate(txtDateinCheque.Text) + "'," + txtCheque.Text + "," + txtmisc + "," + txtTotalAmount.Text + ",0,1," + lbVisible.Visible + ")";
                            //                trn.insertorupdateTrn(strBankMiscInsertQuery);
                            //            }
                            //            else
                            //            {
                            //                string strBankMiscInsertQuery = "insert into transbank(BranchID, TransactionKey, DualTransactionKey, T_Day, T_Month, T_Year, BankHeadID, Head_Id, MemberID, CustomersBankName, DateInCheque, ChequeDDNO, GivenAmount, TotalChequeDDAmount, IsBounced, Trans_Type,cardnumber,cardtype,IsChequeAvailable) values (" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + "," + strCashHeadMisc + "," + DualTransactionKey + "," + dtChoosenDate.Day + "," + dtChoosenDate.Month + "," + dtChoosenDate.Year + "," + ddlBankHead.SelectedValue + "," + mischd1 + "," + MemberID + ",'" + ddlBanklDetails.SelectedItem.Text + "','" + balayer.indiandateToMysqlDate(txtDateinCheque.Text) + "'," + txtCheque.Text + "," + txtmisc + "," + txtTotalAmount.Text + ",0,1,'" + balayer.MySQLEscapeString(txtIdcardNumber.Text) + "','" + balayer.MySQLEscapeString(ddlCardType.SelectedItem.Text) + "'," + lbVisible.Visible + ")";
                            //                trn.insertorupdateTrn(strBankMiscInsertQuery);
                            //            }
                            //        }
                            //    }
                            //}
                        }
                        else
                        {
                              long strCashHead = 0;
                              if (decimal.Parse(GViewCROther_Selected.Rows[i].Cells[4].Text) != 0.00M)
                              {

                                //   string strChitHeadQuery = "INSERT INTO `svcf`.`voucher` (`Other_Trans_Type`,`DualTransactionKey`,`BranchID`,`CurrDate`,`Voucher_No`,`Voucher_Type`,`Head_Id`,`ChoosenDate`,`Narration`,`Amount`,`Series`,`ReceievedBy`,`Trans_Type`,`T_Day`,`T_Month`,`T_Year`,`MemberID`,`Trans_Medium`,`RootID`,`ChitGroupId`,`M_Id`,LoginIP) VALUES (1," + DualTransactionKey + "," + balayer.ToobjectstrEvenNull(Session["Branchid"]) + ",'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," + Rcptno.Text + ",'C'," + ChitsBranchID + ",'" + dtChoosenDate.ToString("yyyy-MM-dd") + "','" + " Recd from :" + "-" + ":" + "-" + ":" + Rcptno.Text + "-" + (GViewCROther_Selected.Rows[i].Cells[3].Text + ":" + balayer.MySQLEscapeString(GViewCROther_Selected.Rows[i].Cells[2].Text)) + "'," + GViewCROther_Selected.Rows[i].Cells[4].Text + ",'" + ddlReceiptSeries.SelectedItem.Text + "','" + balayer.ToobjectstrEvenNull(Session["UserName"]) + "',1," + dtChoosenDate.Day + "," + dtChoosenDate.Month + "," + dtChoosenDate.Year + "," + MemberID + "," + trans_medium + ",1," + GroupID + "," + ddlColloctorName.SelectedItem.Value + ",'" + objSession.LoginIp + "')";
                                //   string strCashHeadQuery = "INSERT INTO `svcf`.`voucher` (`Other_Trans_Type`,`DualTransactionKey`,`BranchID`,`CurrDate`,`Voucher_No`,`Voucher_Type`,`Head_Id`,`ChoosenDate`,`Narration`,`Amount`,`Series`,`ReceievedBy`,`Trans_Type`,`T_Day`,`T_Month`,`T_Year`,`MemberID`,`Trans_Medium`,`RootID`,`ChitGroupId`,`M_Id`,LoginIP) VALUES (1," + DualTransactionKey + "," + balayer.ToobjectstrEvenNull(Session["Branchid"]) + ",'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," + Rcptno.Text + ",'D'," + CashOrBankID + ",'" + dtChoosenDate.ToString("yyyy-MM-dd") + "','" + "  Recd from :" + "-" + ":" + "-" + ":" + Rcptno.Text + "-" + (GViewCROther_Selected.Rows[i].Cells[3].Text + ":" + balayer.MySQLEscapeString(GViewCROther_Selected.Rows[i].Cells[2].Text)) + "'," + GViewCROther_Selected.Rows[i].Cells[4].Text + ",'" + ddlReceiptSeries.SelectedItem.Text + "','" + balayer.ToobjectstrEvenNull(Session["UserName"]) + "',1," + dtChoosenDate.Day + "," + dtChoosenDate.Month + "," + dtChoosenDate.Year + "," + MemberID + "," + trans_medium + "," + RootID + "," + GroupID + "," + ddlColloctorName.SelectedItem.Value + ",'" + objSession.LoginIp + "')";
                                //string strChitHeadQuery = "INSERT INTO `svcf`.`voucher` (`Other_Trans_Type`,`DualTransactionKey`,`BranchID`,`CurrDate`,`Voucher_No`,`Voucher_Type`,`Head_Id`,`ChoosenDate`,`Narration`,`Amount`,`Series`,`ReceievedBy`,`Trans_Type`,`T_Day`,`T_Month`,`T_Year`,`MemberID`,`Trans_Medium`,`RootID`,`ChitGroupId`,`M_Id`,LoginIP) VALUES (1," + DualTransactionKey + "," + balayer.ToobjectstrEvenNull(Session["Branchid"]) + ",'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," + Rcptno.Text + ",'C'," + ChitsBranchID + ",'" + dtChoosenDate.ToString("yyyy-MM-dd") + "','" + " Recd from :" + "-" + ":" + "-" + ":" + Rcptno.Text + "-" + (narrationname + ":" + balayer.MySQLEscapeString(GViewCROther_Selected.Rows[i].Cells[2].Text)) + "'," + GViewCROther_Selected.Rows[i].Cells[4].Text + ",'" + ddlReceiptSeries.SelectedItem.Text + "','" + balayer.ToobjectstrEvenNull(Session["UserName"]) + "',1," + dtChoosenDate.Day + "," + dtChoosenDate.Month + "," + dtChoosenDate.Year + "," + MemberID + "," + trans_medium + ",1," + GroupID + "," + ddlColloctorName.SelectedItem.Value + ",'" + objSession.LoginIp + "')";
                                //string strCashHeadQuery = "INSERT INTO `svcf`.`voucher` (`Other_Trans_Type`,`DualTransactionKey`,`BranchID`,`CurrDate`,`Voucher_No`,`Voucher_Type`,`Head_Id`,`ChoosenDate`,`Narration`,`Amount`,`Series`,`ReceievedBy`,`Trans_Type`,`T_Day`,`T_Month`,`T_Year`,`MemberID`,`Trans_Medium`,`RootID`,`ChitGroupId`,`M_Id`,LoginIP) VALUES (1," + DualTransactionKey + "," + balayer.ToobjectstrEvenNull(Session["Branchid"]) + ",'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," + Rcptno.Text + ",'D'," + CashOrBankID + ",'" + dtChoosenDate.ToString("yyyy-MM-dd") + "','" + "  Recd from :" + "-" + ":" + "-" + ":" + Rcptno.Text + "-" + (narrationname + ":" + balayer.MySQLEscapeString(GViewCROther_Selected.Rows[i].Cells[2].Text)) + "'," + GViewCROther_Selected.Rows[i].Cells[4].Text + ",'" + ddlReceiptSeries.SelectedItem.Text + "','" + balayer.ToobjectstrEvenNull(Session["UserName"]) + "',1," + dtChoosenDate.Day + "," + dtChoosenDate.Month + "," + dtChoosenDate.Year + "," + MemberID + "," + trans_medium + "," + RootID + "," + GroupID + "," + ddlColloctorName.SelectedItem.Value + ",'" + objSession.LoginIp + "')";
                                //long strChitHead = trn.insertorupdateTrn(strChitHeadQuery);
                                //   strCashHead = trn.insertorupdateTrn(strCashHeadQuery);
                                narration = GViewCROther_Selected.Rows[i].Cells[3].Text;

                                if (narration.Contains("&amp;"))
                                {
                                    narration = narration.Replace("&amp;", "&");
                                }
                                else
                                {
                                    narration = GViewCROther_Selected.Rows[i].Cells[3].Text;
                                }
                                if (CheckBox2.Checked)
                                {
                                    var chitheadid = ChitgpTokenid.Text;
                                    var chitnumber = GViewCROther_Selected.Rows[i].Cells[2].Text;
                                    var Nameofsubscriber = narration;
                                    var amount = GViewCROther_Selected.Rows[i].Cells[4].Text;
                                    var branch = GViewCROther_Selected.Rows[i].Cells[1].Text;
                                    var branchid1 = branchid;
                                    var ChoosenDate = dtChoosenDate.ToString("yyyy-MM-dd");
                                   // var DualTransactionKey1 = DualTransactionKey;
                                    var recno = Rcptno.Text;
                                    var ss = "Insert into `svcf`.`chitcollection` (DualTransactionKey,chitnumber,Nameofsubscriber,amount,BranchID,ChoosenDate,ChitHead_id,VoucherHeadid) values('" + DualTransactionKey + "','" + chitnumber + "','" + Nameofsubscriber + "','" + amount + "','" + branchid + "','" + ChoosenDate + "'," + chitheadid + ",'43')";
                                    trn.insertorupdateTrn(ss);
                                    //string strChitHeadQuery = "INSERT INTO `svcf`.`voucher` (`Other_Trans_Type`,`DualTransactionKey`,`BranchID`,`CurrDate`,`Voucher_No`,`Voucher_Type`,`Head_Id`,`ChoosenDate`,`Narration`,`Amount`,`Series`,`ReceievedBy`,`Trans_Type`,`T_Day`,`T_Month`,`T_Year`,`MemberID`,`Trans_Medium`,`RootID`,`ChitGroupId`,`M_Id`,LoginIP) VALUES (1," + DualTransactionKey + "," + balayer.ToobjectstrEvenNull(Session["Branchid"]) + ",'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," + Rcptno.Text + ",'C','43','" + dtChoosenDate.ToString("yyyy-MM-dd") + "','" + " Recd from :" + "-" + ":" + "-" + ":" + Rcptno.Text + "-" + (narration + ":" + balayer.MySQLEscapeString(GViewCROther_Selected.Rows[i].Cells[2].Text)) + "'," + GViewCROther_Selected.Rows[i].Cells[4].Text + ",'" + ddlReceiptSeries.SelectedItem.Text + "','" + balayer.ToobjectstrEvenNull(Session["UserName"]) + "',1," + dtChoosenDate.Day + "," + dtChoosenDate.Month + "," + dtChoosenDate.Year + "," + MemberID + "," + trans_medium + ",5," + GroupID + "," + ddlColloctorName.SelectedItem.Value + ",'" + objSession.LoginIp + "')";

                                    //string strCashHeadQuery = "INSERT INTO `svcf`.`voucher` (`Other_Trans_Type`,`DualTransactionKey`,`BranchID`,`CurrDate`,`Voucher_No`,`Voucher_Type`,`Head_Id`,`ChoosenDate`,`Narration`,`Amount`,`Series`,`ReceievedBy`,`Trans_Type`,`T_Day`,`T_Month`,`T_Year`,`MemberID`,`Trans_Medium`,`RootID`,`ChitGroupId`,`M_Id`,LoginIP) VALUES (1," + DualTransactionKey + "," + balayer.ToobjectstrEvenNull(Session["Branchid"]) + ",'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," + Rcptno.Text + ",'D'," + CashOrBankID + ",'" + dtChoosenDate.ToString("yyyy-MM-dd") + "','" + "  Recd from :" + "-" + ":" + "-" + ":" + Rcptno.Text + "-" + (narration + ":" + balayer.MySQLEscapeString(GViewCROther_Selected.Rows[i].Cells[2].Text)) + "'," + GViewCROther_Selected.Rows[i].Cells[4].Text + ",'" + ddlReceiptSeries.SelectedItem.Text + "','" + balayer.ToobjectstrEvenNull(Session["UserName"]) + "',1," + dtChoosenDate.Day + "," + dtChoosenDate.Month + "," + dtChoosenDate.Year + "," + MemberID + "," + trans_medium + "," + RootID + "," + GroupID + "," + ddlColloctorName.SelectedItem.Value + ",'" + objSession.LoginIp + "')";
                                    string strChitHeadQuery = "INSERT INTO `svcf`.`voucher` (`Other_Trans_Type`,`DualTransactionKey`,`BranchID`,`CurrDate`,`Voucher_No`,`Voucher_Type`,`Head_Id`,`ChoosenDate`,`Narration`,`Amount`,`Series`,`ReceievedBy`,`Trans_Type`,`T_Day`,`T_Month`,`T_Year`,`MemberID`,`Trans_Medium`,`RootID`,`ChitGroupId`,`M_Id`,LoginIP) VALUES (1," + DualTransactionKey + "," + balayer.ToobjectstrEvenNull(Session["Branchid"]) + ",'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," + Rcptno.Text + ",'C','43','" + dtChoosenDate.ToString("yyyy-MM-dd") + "','" + " Recd from :" + "-" + ":" + "-" + ":" + Rcptno.Text + "-" + (narration + ":" + balayer.MySQLEscapeString(GViewCROther_Selected.Rows[i].Cells[2].Text)) + ":For Draw No-" + ddltooltip[i] + "'," + GViewCROther_Selected.Rows[i].Cells[4].Text + ",'" + ddlReceiptSeries.SelectedItem.Text + "','" + balayer.ToobjectstrEvenNull(Session["UserName"]) + "',1," + dtChoosenDate.Day + "," + dtChoosenDate.Month + "," + dtChoosenDate.Year + "," + MemberID + "," + trans_medium + ",5," + GroupID + "," + ddlColloctorName.SelectedItem.Value + ",'" + objSession.LoginIp + "')";

                                    string strCashHeadQuery = "INSERT INTO `svcf`.`voucher` (`Other_Trans_Type`,`DualTransactionKey`,`BranchID`,`CurrDate`,`Voucher_No`,`Voucher_Type`,`Head_Id`,`ChoosenDate`,`Narration`,`Amount`,`Series`,`ReceievedBy`,`Trans_Type`,`T_Day`,`T_Month`,`T_Year`,`MemberID`,`Trans_Medium`,`RootID`,`ChitGroupId`,`M_Id`,LoginIP) VALUES (1," + DualTransactionKey + "," + balayer.ToobjectstrEvenNull(Session["Branchid"]) + ",'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," + Rcptno.Text + ",'D'," + CashOrBankID + ",'" + dtChoosenDate.ToString("yyyy-MM-dd") + "','" + "  Recd from :" + "-" + ":" + "-" + ":" + Rcptno.Text + "-" + (narration + ":" + balayer.MySQLEscapeString(GViewCROther_Selected.Rows[i].Cells[2].Text)) + ":For Draw No-" + ddltooltip[i] + "'," + GViewCROther_Selected.Rows[i].Cells[4].Text + ",'" + ddlReceiptSeries.SelectedItem.Text + "','" + balayer.ToobjectstrEvenNull(Session["UserName"]) + "',1," + dtChoosenDate.Day + "," + dtChoosenDate.Month + "," + dtChoosenDate.Year + "," + MemberID + "," + trans_medium + "," + RootID + "," + GroupID + "," + ddlColloctorName.SelectedItem.Value + ",'" + objSession.LoginIp + "')";
                                    long strChitHead = trn.insertorupdateTrn(strChitHeadQuery);
                                    strCashHead = trn.insertorupdateTrn(strCashHeadQuery);

                                }
                                else
                                {
                                    //string strChitHeadQuery = "INSERT INTO `svcf`.`voucher` (`Other_Trans_Type`,`DualTransactionKey`,`BranchID`,`CurrDate`,`Voucher_No`,`Voucher_Type`,`Head_Id`,`ChoosenDate`,`Narration`,`Amount`,`Series`,`ReceievedBy`,`Trans_Type`,`T_Day`,`T_Month`,`T_Year`,`MemberID`,`Trans_Medium`,`RootID`,`ChitGroupId`,`M_Id`,LoginIP) VALUES (1," + DualTransactionKey + "," + balayer.ToobjectstrEvenNull(Session["Branchid"]) + ",'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," + Rcptno.Text + ",'C'," + ChitsBranchID + ",'" + dtChoosenDate.ToString("yyyy-MM-dd") + "','" + " Recd from :" + "-" + ":" + "-" + ":" + Rcptno.Text + "-" + (narration + ":" + balayer.MySQLEscapeString(GViewCROther_Selected.Rows[i].Cells[2].Text)) + "'," + GViewCROther_Selected.Rows[i].Cells[4].Text + ",'" + ddlReceiptSeries.SelectedItem.Text + "','" + balayer.ToobjectstrEvenNull(Session["UserName"]) + "',1," + dtChoosenDate.Day + "," + dtChoosenDate.Month + "," + dtChoosenDate.Year + "," + MemberID + "," + trans_medium + ",1," + GroupID + "," + ddlColloctorName.SelectedItem.Value + ",'" + objSession.LoginIp + "')";

                                    //string strCashHeadQuery = "INSERT INTO `svcf`.`voucher` (`Other_Trans_Type`,`DualTransactionKey`,`BranchID`,`CurrDate`,`Voucher_No`,`Voucher_Type`,`Head_Id`,`ChoosenDate`,`Narration`,`Amount`,`Series`,`ReceievedBy`,`Trans_Type`,`T_Day`,`T_Month`,`T_Year`,`MemberID`,`Trans_Medium`,`RootID`,`ChitGroupId`,`M_Id`,LoginIP) VALUES (1," + DualTransactionKey + "," + balayer.ToobjectstrEvenNull(Session["Branchid"]) + ",'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," + Rcptno.Text + ",'D'," + CashOrBankID + ",'" + dtChoosenDate.ToString("yyyy-MM-dd") + "','" + "  Recd from :" + "-" + ":" + "-" + ":" + Rcptno.Text + "-" + (narration + ":" + balayer.MySQLEscapeString(GViewCROther_Selected.Rows[i].Cells[2].Text)) + "'," + GViewCROther_Selected.Rows[i].Cells[4].Text + ",'" + ddlReceiptSeries.SelectedItem.Text + "','" + balayer.ToobjectstrEvenNull(Session["UserName"]) + "',1," + dtChoosenDate.Day + "," + dtChoosenDate.Month + "," + dtChoosenDate.Year + "," + MemberID + "," + trans_medium + "," + RootID + "," + GroupID + "," + ddlColloctorName.SelectedItem.Value + ",'" + objSession.LoginIp + "')";
                                    string strChitHeadQuery = "INSERT INTO `svcf`.`voucher` (`Other_Trans_Type`,`DualTransactionKey`,`BranchID`,`CurrDate`,`Voucher_No`,`Voucher_Type`,`Head_Id`,`ChoosenDate`,`Narration`,`Amount`,`Series`,`ReceievedBy`,`Trans_Type`,`T_Day`,`T_Month`,`T_Year`,`MemberID`,`Trans_Medium`,`RootID`,`ChitGroupId`,`M_Id`,LoginIP) VALUES (1," + DualTransactionKey + "," + balayer.ToobjectstrEvenNull(Session["Branchid"]) + ",'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," + Rcptno.Text + ",'C'," + ChitsBranchID + ",'" + dtChoosenDate.ToString("yyyy-MM-dd") + "','" + " Recd from :" + "-" + ":" + "-" + ":" + Rcptno.Text + "-" + (narration + ":" + balayer.MySQLEscapeString(GViewCROther_Selected.Rows[i].Cells[2].Text)) + ":For Draw No-" + ddltooltip[i] + "'," + GViewCROther_Selected.Rows[i].Cells[4].Text + ",'" + ddlReceiptSeries.SelectedItem.Text + "','" + balayer.ToobjectstrEvenNull(Session["UserName"]) + "',1," + dtChoosenDate.Day + "," + dtChoosenDate.Month + "," + dtChoosenDate.Year + "," + MemberID + "," + trans_medium + ",1," + GroupID + "," + ddlColloctorName.SelectedItem.Value + ",'" + objSession.LoginIp + "')";

                                    string strCashHeadQuery = "INSERT INTO `svcf`.`voucher` (`Other_Trans_Type`,`DualTransactionKey`,`BranchID`,`CurrDate`,`Voucher_No`,`Voucher_Type`,`Head_Id`,`ChoosenDate`,`Narration`,`Amount`,`Series`,`ReceievedBy`,`Trans_Type`,`T_Day`,`T_Month`,`T_Year`,`MemberID`,`Trans_Medium`,`RootID`,`ChitGroupId`,`M_Id`,LoginIP) VALUES (1," + DualTransactionKey + "," + balayer.ToobjectstrEvenNull(Session["Branchid"]) + ",'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," + Rcptno.Text + ",'D'," + CashOrBankID + ",'" + dtChoosenDate.ToString("yyyy-MM-dd") + "','" + "  Recd from :" + "-" + ":" + "-" + ":" + Rcptno.Text + "-" + (narration + ":" + balayer.MySQLEscapeString(GViewCROther_Selected.Rows[i].Cells[2].Text)) + ":For Draw No-" + ddltooltip[i] + "'," + GViewCROther_Selected.Rows[i].Cells[4].Text + ",'" + ddlReceiptSeries.SelectedItem.Text + "','" + balayer.ToobjectstrEvenNull(Session["UserName"]) + "',1," + dtChoosenDate.Day + "," + dtChoosenDate.Month + "," + dtChoosenDate.Year + "," + MemberID + "," + trans_medium + "," + RootID + "," + GroupID + "," + ddlColloctorName.SelectedItem.Value + ",'" + objSession.LoginIp + "')";
                                    long strChitHead = trn.insertorupdateTrn(strChitHeadQuery);
                                    strCashHead = trn.insertorupdateTrn(strCashHeadQuery);
                                }
                            }
                            if (CheckBox1.Checked == true)
                            {
                                TransactionKeyDue = strCashHead.ToString();
                            }
                            if (ddlmisc != "")
                            {
                                // string strChitmISCHeadQuery = "INSERT INTO `svcf`.`voucher` (`Other_Trans_Type`,`DualTransactionKey`,`BranchID`,`CurrDate`,`Voucher_No`,`Voucher_Type`,`Head_Id`,`ChoosenDate`,`Narration`,`Amount`,`Series`,`ReceievedBy`,`Trans_Type`,`T_Day`,`T_Month`,`T_Year`,`MemberID`,`Trans_Medium`,`RootID`,`ChitGroupId`,`M_Id`,LoginIP) VALUES (1," + DualTransactionKey + "," + balayer.ToobjectstrEvenNull(Session["Branchid"]) + ",'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," + Rcptno.Text + ",'C'," + mischd1 + ",'" + dtChoosenDate.ToString("yyyy-MM-dd") + "','" + "Misc :" + " Recd from :" + "-" + ":" + "-" + ":" + Rcptno.Text + "-" + (GViewCROther_Selected.Rows[i].Cells[3].Text + ":" + balayer.MySQLEscapeString(GViewCROther_Selected.Rows[i].Cells[2].Text)) + "'," + txtmisc + ",'" + ddlReceiptSeries.SelectedItem.Text + "','" + balayer.ToobjectstrEvenNull(Session["UserName"]) + "',1," + dtChoosenDate.Day + "," + dtChoosenDate.Month + "," + dtChoosenDate.Year + "," + MemberID + "," + trans_medium + "," + mischd2 + "," + GroupID + "," + ddlColloctorName.SelectedItem.Value + ",'" + objSession.LoginIp + "') ";
                                // string strCashmISCHeadQuery = "INSERT INTO `svcf`.`voucher` (`Other_Trans_Type`,`DualTransactionKey`,`BranchID`,`CurrDate`,`Voucher_No`,`Voucher_Type`,`Head_Id`,`ChoosenDate`,`Narration`,`Amount`,`Series`,`ReceievedBy`,`Trans_Type`,`T_Day`,`T_Month`,`T_Year`,`MemberID`,`Trans_Medium`,`RootID`,`ChitGroupId`,`M_Id`,LoginIP) VALUES (1," + DualTransactionKey + "," + balayer.ToobjectstrEvenNull(Session["Branchid"]) + ",'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," + Rcptno.Text + ",'D'," + CashOrBankID + ",'" + dtChoosenDate.ToString("yyyy-MM-dd") + "','" + "Misc :" + " Recd from :" + "-" + "-" + Rcptno.Text + "-" + (GViewCROther_Selected.Rows[i].Cells[3].Text + ":" + balayer.MySQLEscapeString(GViewCROther_Selected.Rows[i].Cells[2].Text)) + "'," + txtmisc + ",'" + ddlReceiptSeries.SelectedItem.Text + "','" + balayer.ToobjectstrEvenNull(Session["UserName"]) + "',1," + dtChoosenDate.Day + "," + dtChoosenDate.Month + "," + dtChoosenDate.Year + "," + MemberID + "," + trans_medium + "," + RootID + "," + GroupID + "," + ddlColloctorName.SelectedItem.Value + ",'" + objSession.LoginIp + "') ";
                                string strChitmISCHeadQuery = "INSERT INTO `svcf`.`voucher` (`Other_Trans_Type`,`DualTransactionKey`,`BranchID`,`CurrDate`,`Voucher_No`,`Voucher_Type`,`Head_Id`,`ChoosenDate`,`Narration`,`Amount`,`Series`,`ReceievedBy`,`Trans_Type`,`T_Day`,`T_Month`,`T_Year`,`MemberID`,`Trans_Medium`,`RootID`,`ChitGroupId`,`M_Id`,LoginIP) VALUES (1," + DualTransactionKey + "," + balayer.ToobjectstrEvenNull(Session["Branchid"]) + ",'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," + Rcptno.Text + ",'C'," + mischd1 + ",'" + dtChoosenDate.ToString("yyyy-MM-dd") + "','" + "Misc :" + " Recd from :" + "-" + ":" + "-" + ":" + Rcptno.Text + "-" + (narrationname + ":" + balayer.MySQLEscapeString(GViewCROther_Selected.Rows[i].Cells[2].Text)) + ":For Draw No-" + ddltooltip[i] + "'," + txtmisc + ",'" + ddlReceiptSeries.SelectedItem.Text + "','" + balayer.ToobjectstrEvenNull(Session["UserName"]) + "',1," + dtChoosenDate.Day + "," + dtChoosenDate.Month + "," + dtChoosenDate.Year + "," + MemberID + "," + trans_medium + "," + mischd2 + "," + GroupID + "," + ddlColloctorName.SelectedItem.Value + ",'" + objSession.LoginIp + "') ";
                                string strCashmISCHeadQuery = "INSERT INTO `svcf`.`voucher` (`Other_Trans_Type`,`DualTransactionKey`,`BranchID`,`CurrDate`,`Voucher_No`,`Voucher_Type`,`Head_Id`,`ChoosenDate`,`Narration`,`Amount`,`Series`,`ReceievedBy`,`Trans_Type`,`T_Day`,`T_Month`,`T_Year`,`MemberID`,`Trans_Medium`,`RootID`,`ChitGroupId`,`M_Id`,LoginIP) VALUES (1," + DualTransactionKey + "," + balayer.ToobjectstrEvenNull(Session["Branchid"]) + ",'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," + Rcptno.Text + ",'D'," + CashOrBankID + ",'" + dtChoosenDate.ToString("yyyy-MM-dd") + "','" + "Misc :" + " Recd from :" + "-" + "-" + Rcptno.Text + "-" + (narrationname + ":" + balayer.MySQLEscapeString(GViewCROther_Selected.Rows[i].Cells[2].Text)) + ":For Draw No-" + ddltooltip[i] + "'," + txtmisc + ",'" + ddlReceiptSeries.SelectedItem.Text + "','" + balayer.ToobjectstrEvenNull(Session["UserName"]) + "',1," + dtChoosenDate.Day + "," + dtChoosenDate.Month + "," + dtChoosenDate.Year + "," + MemberID + "," + trans_medium + "," + RootID + "," + GroupID + "," + ddlColloctorName.SelectedItem.Value + ",'" + objSession.LoginIp + "') ";
                                long strChitMiscHead = trn.insertorupdateTrn(strChitmISCHeadQuery);
                                long strCashHeadMisc = trn.insertorupdateTrn(strCashmISCHeadQuery);
                                if (CheckBox1.Checked == true)
                                {
                                    if (!ddlCardType.Visible)
                                    {
                                        string strBankMiscInsertQuery = "insert into transbank(BranchID, TransactionKey, DualTransactionKey, T_Day, T_Month, T_Year, BankHeadID, Head_Id, MemberID, CustomersBankName, DateInCheque, ChequeDDNO, GivenAmount, TotalChequeDDAmount, IsBounced, Trans_Type,IsChequeAvailable) values (" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + "," + strCashHeadMisc + "," + DualTransactionKey + "," + dtChoosenDate.Day + "," + dtChoosenDate.Month + "," + dtChoosenDate.Year + "," + ddlBankHead.SelectedValue + "," + mischd1 + "," + MemberID + ",'" + ddlBanklDetails.SelectedItem.Text + "','" + balayer.indiandateToMysqlDate(txtDateinCheque.Text) + "'," + txtCheque.Text + "," + txtmisc + "," + txtTotalAmount.Text + ",0,1," + lbVisible.Visible + ")";
                                        trn.insertorupdateTrn(strBankMiscInsertQuery);
                                    }
                                    else
                                    {
                                        string strBankMiscInsertQuery = "insert into transbank(BranchID, TransactionKey, DualTransactionKey, T_Day, T_Month, T_Year, BankHeadID, Head_Id, MemberID, CustomersBankName, DateInCheque, ChequeDDNO, GivenAmount, TotalChequeDDAmount, IsBounced, Trans_Type,cardnumber,cardtype,IsChequeAvailable) values (" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + "," + strCashHeadMisc + "," + DualTransactionKey + "," + dtChoosenDate.Day + "," + dtChoosenDate.Month + "," + dtChoosenDate.Year + "," + ddlBankHead.SelectedValue + "," + mischd2 + "," + MemberID + ",'" + ddlBanklDetails.SelectedItem.Text + "','" + balayer.indiandateToMysqlDate(txtDateinCheque.Text) + "'," + txtCheque.Text + "," + txtmisc + "," + txtTotalAmount.Text + ",0,1,'" + balayer.MySQLEscapeString(txtIdcardNumber.Text) + "','" + balayer.MySQLEscapeString(ddlCardType.SelectedItem.Text) + "'," + lbVisible.Visible + ")";
                                        trn.insertorupdateTrn(strBankMiscInsertQuery);
                                    }
                                }
                            }

                            if (CheckBox1.Checked == true)
                            {
                                if (string.IsNullOrEmpty(TransactionKeyDue))
                                {
                                    TransactionKeyDue = "0";
                                }
                                if (Convert.ToDouble(TransactionKeyDue) > 0)
                                {
                                    if (!ddlCardType.Visible)
                                    {
                                        string strBankInsertQuery = "insert into transbank(BranchID, TransactionKey, DualTransactionKey, T_Day, T_Month, T_Year, BankHeadID, Head_Id, MemberID, CustomersBankName, DateInCheque, ChequeDDNO, GivenAmount, TotalChequeDDAmount, IsBounced, Trans_Type,IsChequeAvailable) values (" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + "," + TransactionKeyDue + "," + DualTransactionKey + "," + dtChoosenDate.Day + "," + dtChoosenDate.Month + "," + dtChoosenDate.Year + "," + ddlBankHead.SelectedValue + "," + TokenNo + "," + MemberID + ",'" + ddlBanklDetails.SelectedItem.Text + "','" + balayer.indiandateToMysqlDate(txtDateinCheque.Text) + "'," + txtCheque.Text + "," + txtamnt + "," + txtTotalAmount.Text + ",0,1," + lbVisible.Visible + ")";
                                        trn.insertorupdateTrn(strBankInsertQuery);
                                    }
                                    else
                                    {
                                        string strBankInsertQuery = "insert into transbank(BranchID, TransactionKey, DualTransactionKey, T_Day, T_Month, T_Year, BankHeadID, Head_Id, MemberID, CustomersBankName, DateInCheque, ChequeDDNO, GivenAmount, TotalChequeDDAmount, IsBounced, Trans_Type,cardnumber,cardtype,IsChequeAvailable) values (" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + "," + TransactionKeyDue + "," + DualTransactionKey + "," + dtChoosenDate.Day + "," + dtChoosenDate.Month + "," + dtChoosenDate.Year + "," + ddlBankHead.SelectedValue + "," + TokenNo + "," + MemberID + ",'" + ddlBanklDetails.SelectedItem.Text + "','" + balayer.indiandateToMysqlDate(txtDateinCheque.Text) + "'," + txtCheque.Text + "," + txtamnt + "," + txtTotalAmount.Text + ",0,1,'" + balayer.MySQLEscapeString(txtIdcardNumber.Text) + "','" + balayer.MySQLEscapeString(ddlCardType.SelectedItem.Text) + "'," + lbVisible.Visible + ")";
                                        trn.insertorupdateTrn(strBankInsertQuery);
                                    }
                                }
                            }
                        }

                      
                    }

                  
                    if (ddlColloctorName.ToolTip.ToString().Trim() != "")
                    {
                        //put  the  query to check all the numbers used or not 
                        trn.insertorupdateTrn("update svcf.assignreceiptbook set IsFinished=1 where receiptseries='" + ddlReceiptSeries.SelectedItem.Text + "' and receiptnoto in(" + ddlColloctorName.ToolTip.ToString().Trim() + ") and BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]));
                    }
                    else
                    {
                        //update last used receipt no. in assignreceiptbook                    
                        trn.insertorupdateTrn("update svcf.assignreceiptbook set alreadyusedReceipts=" + RCNumber + " where receiptseries='" + ddlReceiptSeries.SelectedItem.Text + "' and receiptnoto in(" + rcpttorange.Value + ") and BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]));
                    }
                    //ModalPopupExtender1.PopupControlID = "pnlmsg";
                    //BtnOK.Focus();
                    //ModalPopupExtender1.Show();
                    //BtnOK.Focus();
                    //pnlmsg.Visible = true;
                    //lblh.Text = "Status";
                    //lblcon.Text = "Your Transaction Processed Successfully!!!";
                    //lblcon.ForeColor = System.Drawing.Color.Green;
                    trn.CommitTrn();

                    logger.Info("TRROtherBranch.aspx - btnConfirmationYes_Click():  Completed: " + DateTime.Now + " by: " + Convert.ToString(Session["UserName"]) + "");

                    SetInitialRow();
                    ddlColloctorName.ClearSelection();
                    ddlReceiptSeries.ClearSelection();
                    ddlEmployee.ClearSelection();
                    txtTotalAmount.Text = "";
                }
                catch(Exception ex)
                {
                    try
                    {
                        trn.RollbackTrn();
                        logger.Info("TRROtherBranch.aspx - btnConfirmationYes_Click():  Error: " + ex.Message + ": " + DateTime.Now + " by: " + Convert.ToString(Session["UserName"]) + "");
                    }
                    catch
                    { }
                    finally
                    {
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
                pnlConfirmation.Visible = false;
            }
            catch (Exception ex)
            {

            }
        }
        
        
        protected void btnGenerate_Click(object sender, EventArgs e)
        {
            try
            {
                //Page.Validate("a");
                //Page.Validate("GrpRow");
                //Page.Validate("b");
                //if (!Page.IsValid)
                //{
                //    return;
                //}
                if (Session["CheckRefresh"].ToString() != ViewState["CheckRefresh"].ToString())
                {
                    return;
                }
                if (Page.IsValid == true)
                {


                    decimal dblTotalAmount = decimal.Parse(txtTotalAmount.Text);
                    decimal dblDueAmount = 0.0M;
                    decimal dblDueTemp;
                    bool isMisc;
                    bool isMiscIssue = false;
                    string mischead = "";
                    string miscamnt = "";

                    string selectedRSeries = "";
                    string selectedbranch = "";


                    string selseries = "";
                    dblTotalAmount = decimal.Parse(txtTotalAmount.Text);
                    dblDueAmount = 0.0M;
                    isMiscIssue = false;
                    RCNumber = Convert.ToInt32(txtReceiptNumber.Text);


                    string LoadedSeries = "";
                    string Rcptnumber = "";
                    selectedRSeries = Request.Form[ddlColloctorName.UniqueID];
                    //LoadedSeries = Request.Form[ddlReceiptSeries.UniqueID];
                    LoadedSeries = HD_RSeriesid.Value;

                    LoadedSeries.Trim();
                    PopulateDropDownList(CRSeries_OtherBranch(selectedRSeries), ddlReceiptSeries);
                    ddlReceiptSeries.Items.FindByText(LoadedSeries).Selected = true;


                    //GetRCBookno(LoadedSeries, selectedRSeries);
                    foreach (GridViewRow gvRow in GViewCROther_Selected.Rows)
                    {

                        dblDueTemp = decimal.Parse(gvRow.Cells[4].Text);
                        decimal dblMiscTemp = 0.0M;

                        //isMisc = decimal.TryParse(((TextBox)gvRow.FindControl("txtMisc")).Text, out dblMiscTemp);
                        miscamnt = gvRow.Cells[6].Text;
                        if (miscamnt == "&nbsp;")
                        {
                            miscamnt = "0";
                        }
                        isMisc = decimal.TryParse((miscamnt), out dblMiscTemp);
                        dblDueAmount += dblDueTemp + dblMiscTemp;

                        //DropDownList ddlMisc = ((DropDownList)gvRow.FindControl("ddlMisc"));
                        mischead = gvRow.Cells[5].Text;
                        mischead = mischead.Replace("&gt;&gt;", ">>");
                        if (mischead == "--Select--")
                        {
                            mischead = "";
                        }
                        if (isMisc == true & mischead != "")
                        {
                            isMiscIssue = true;
                        }
                        else if (isMisc == false & mischead != "")
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
                    bool keepGoing = true;
                    string finishedReceiptNo = "";
                    string strErrorMessage = "";
                    string strExistMessage = "";
                    int ReceiptNo = 0;
                    int FromRange = 0;
                    int toRange = 0;
                    DataTable dtAll = balayer.GetDataTable("SELECT  (receiptnoto-total),receiptnoto FROM svcf.assignreceiptbook where  moneycollid=" + ddlColloctorName.SelectedValue + "  and BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and receiptseries='" + ddlReceiptSeries.SelectedItem.Text + "' and IsFinished=0");
                    for (int j = 0; j < dtAll.Rows.Count; j++)
                    {
                        // ReceiptNo = int.Parse(txtReceiptNumber.Text);
                        Label RCNUM = (Label)GViewCROther_Selected.Rows[0].FindControl("lblrcnumber");
                        ReceiptNo = Convert.ToInt32(RCNUM.Text);
                        FromRange = int.Parse(dtAll.Rows[j][0].ToString());
                        rcptfrmrange.Value = Convert.ToString(FromRange);

                        toRange = int.Parse(dtAll.Rows[j][1].ToString());
                        rcpttorange.Value = Convert.ToString(toRange);

                        if (ReceiptNo >= FromRange & ReceiptNo <= toRange)
                        {
                            if (ReceiptNo == toRange)
                            {
                                finishedReceiptNo = ReceiptNo + ",";
                            }
                            if (0 != int.Parse(balayer.GetSingleValue("select ifnull(Count(*),0) from voucher where BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and  Voucher_No=" + ReceiptNo + " and Series='" + ddlReceiptSeries.SelectedItem.Text + "'")))
                            {
                                if (strExistMessage == "")
                                {
                                    strErrorMessage = "";
                                    strExistMessage = "Following ReceiptNo Already Exist In Series " + ddlReceiptSeries.SelectedItem.Text + " :<br><br>" + ReceiptNo;
                                    break;
                                }
                                else
                                {
                                    strExistMessage += "<br>" + ReceiptNo;
                                }
                            }
                            else
                            {
                                strExistMessage = "";
                                strErrorMessage = "";
                                break;
                            }
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
                    if (!string.IsNullOrEmpty(strErrorMessage) || !string.IsNullOrEmpty(strExistMessage))
                    {
                        keepGoing = false;
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
                    dtConfirmation.Columns.Add("ChitNo");

                    string memtoken;    //08/09/2021

                    //03/09/2021
                    string Due = "";
                    Int64 firstValue = 0;
                    Int64 secondValue = 0;
                    string maxDraw = "";
                    //

                    ddltooltip = new string[GViewCROther_Selected.Rows.Count];
                    for (int i = 0; i <= GViewCROther_Selected.Rows.Count - 1; i++)
                    {
                        try
                        {
                            dtConfirmation.Rows.Add();
                            Label branchid = (Label)GViewCROther_Selected.Rows[i].FindControl("lblBranchid");
                            Label gpTokenid = (Label)GViewCROther_Selected.Rows[i].FindControl("lblgp_tokenid");
                            memtoken = gpTokenid.Text;  //08/09/2021
                            mischead = GViewCROther_Selected.Rows[i].Cells[5].Text;
                            mischead = mischead.Replace("&gt;&gt;", ">>");
                            miscamnt = GViewCROther_Selected.Rows[i].Cells[6].Text;
                            if (miscamnt == "&nbsp;")
                            {
                                miscamnt = "0";
                            }
                            if (mischead == "--Select--")
                            {
                                mischead = "";
                            }
                            //DropDownList RowddlBranchName = (DropDownList)GridView1.Rows[i].FindControl("ddlBranchName");
                            //TextBox txtNarration = (TextBox)GridView1.Rows[i].FindControl("txtNarration");
                            //TextBox RowtxtAmount = (TextBox)GridView1.Rows[i].FindControl("txtAmount");
                            //DropDownList RowddlMisc = (DropDownList)GridView1.Rows[i].FindControl("ddlMisc");
                            //TextBox RowtxtMisc = (TextBox)GridView1.Rows[i].FindControl("txtMisc");
                            decimal dblMiscTemp = 0.0M;
                            //isMisc = decimal.TryParse(RowtxtMisc.Text, out dblMiscTemp);
                            isMisc = decimal.TryParse(GViewCROther_Selected.Rows[i].Cells[6].Text, out dblMiscTemp);
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
                            //08/09/2021
                            //string GroupID = "0";
                            string GroupID = balayer.GetSingleValue("select GroupID from membertogroupmaster where Head_Id=" + memtoken);
                            //
                            dtConfirmation.Rows[i]["Member Name"] = GViewCROther_Selected.Rows[i].Cells[1].Text;
                            dtConfirmation.Rows[i]["Amount Paying"] = GViewCROther_Selected.Rows[i].Cells[4].Text;
                            //08/09/2021
                            //decimal TotalPaidAmount = 0.00M;
                            decimal TotalPaidAmount = decimal.Parse(balayer.GetSingleValue("SELECT ifnull( (sum(IF(Voucher_Type = 'C',Amount,0.00)) -sum(IF(Voucher_Type = 'D',Amount,0.00)) ),0.00) AS TransactionAmount FROM `svcf`.`voucher` where  Head_Id=" + memtoken + " and  (Trans_Type<>2) and Other_Trans_Type<>5"));
                            //
                            decimal AddTotalPaidAmount = TotalPaidAmount;
                            string FromNarration = "";
                            string ToNarration = "";
                            int FromDraw = 0;
                            int ToDraw = 0;
                            decimal excess = 0; //03/09/2021
                            Due = balayer.GetSingleValue("Select currentdueamount from auctiondetails where GroupID=" + GroupID + " and DrawNO=1 and AuctionDate<='" + DateTime.Now.ToString("yyyy/MM/dd") + "'");   //12/05/2021
                            maxDraw = balayer.GetSingleValue("select max(DrawNO) from auctiondetails where GroupID=" + GroupID);

                            if (TotalPaidAmount == 0.00M)
                            {
                                FromNarration = "1";
                                FromDraw = 1;
                                //TotalPaidAmount = TotalPaidAmount + decimal.Parse(RowtxtAmount.Text);
                                TotalPaidAmount = TotalPaidAmount + decimal.Parse(GViewCROther_Selected.Rows[i].Cells[4].Text);
                                //DataTable dtAuction = balayer.GetDataTable("SELECT DrawNO,CurrentDueAmount FROM `svcf`.`auctiondetails` where GroupID=" + GroupID + " and CurrentDueAmount<>'0.00' order by DrawNO");
                                DataTable dtAuction = balayer.GetDataTable("SELECT DrawNO,CurrentDueAmount  FROM `svcf`.`auctiondetails` where CurrentDueAmount<>'0.00' and Branchid=" + branchid.Text + " and PrizedMemberID=" + gpTokenid.Text + " order by DrawNO");
                                for (int iAuc = 0; iAuc < dtAuction.Rows.Count; iAuc++)
                                {
                                    decimal currentDueAmount = decimal.Parse(dtAuction.Rows[iAuc]["CurrentDueAmount"].ToString());
                                    if (TotalPaidAmount > currentDueAmount)
                                    {
                                        TotalPaidAmount = TotalPaidAmount - currentDueAmount;
                                    }
                                    else
                                    {
                                        TotalPaidAmount = currentDueAmount - TotalPaidAmount;
                                    }
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
                                        ToNarration = iAuc + 1 + " PP";
                                        break;
                                    }
                                }
                                if (ToNarration == "")
                                {
                                    if (dtAuction.Rows.Count > 0)
                                    {
                                        //03/09/2021//FromNarration += " To " + (int.Parse(dtAuction.Rows[dtAuction.Rows.Count - 1]["DrawNO"].ToString()) + 1).ToString() + "+ Excess Payment";
                                        decimal excessCount = excess / Convert.ToDecimal(Due);
                                        var values = excessCount.ToString(CultureInfo.InvariantCulture).Split('.');

                                        if (values.Length > 1)
                                        {
                                            firstValue = int.Parse(values[0]);
                                            secondValue = long.Parse(values[1]);
                                        }
                                        else
                                        {
                                            firstValue = int.Parse(values[0]);
                                        }

                                        if (secondValue == '0')
                                        {
                                            ToNarration = "-" + Convert.ToInt16(dtAuction.Rows[dtAuction.Rows.Count - 1]["DrawNo"]) + firstValue + "F";
                                        }
                                        else
                                        {
                                            ToNarration = "-" + Convert.ToInt16(dtAuction.Rows[dtAuction.Rows.Count - 1]["DrawNo"]) + firstValue + 1 + "P";
                                        }

                                        FromNarration += ToNarration;
                                        //
                                    }
                                }
                                if (FromDraw != ToDraw)
                                {
                                    FromNarration += " To " + ToNarration;
                                }
                                txtNarration.ToolTip = FromNarration;
                                ddltooltip[i] = FromNarration;
                                ViewState["excesspayment"] = FromNarration;
                                //dtConfirmation.Rows[i]["Misc Head"] = RowddlBranchName.ToolTip.ToString();
                            }
                            else
                            {
                                DataTable dtAuction = balayer.GetDataTable("SELECT DrawNO,CurrentDueAmount FROM `svcf`.`auctiondetails` where GroupID=" + GroupID + " and CurrentDueAmount<>'0.00' order by DrawNO");
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
                                        FromNarration = iAuc + 1 + " PP";
                                        FromDraw = iAuc + 1;
                                        break;
                                    }
                                    //03/09/2021
                                    else
                                    {
                                        excess = tempDueAmount;
                                    }
                                    //
                                }
                                if (FromNarration == "")
                                {
                                    if (dtAuction.Rows.Count > 0)
                                    {
                                        //03/09/2021//FromNarration = (int.Parse(dtAuction.Rows[dtAuction.Rows.Count - 1]["DrawNO"].ToString()) + 1).ToString() + "+ Excess Payment";
                                        decimal excessCount = excess / Convert.ToDecimal(Due);
                                        var values = excessCount.ToString(CultureInfo.InvariantCulture).Split('.');

                                        if (values.Length > 1)
                                        {
                                            firstValue = int.Parse(values[0]);
                                            secondValue = long.Parse(values[1]);
                                        }
                                        else
                                        {
                                            firstValue = int.Parse(values[0]);
                                        }

                                        if (secondValue == '0')
                                        {
                                            if (Convert.ToInt16(dtAuction.Rows[dtAuction.Rows.Count - 1]["DrawNo"]) == Convert.ToInt16(maxDraw))
                                            {
                                                ToNarration = Convert.ToInt16(maxDraw) + " + Excess";
                                            }
                                            else
                                            {
                                                ToNarration = Convert.ToInt16(dtAuction.Rows[dtAuction.Rows.Count - 1]["DrawNo"]) + firstValue + "F";
                                            }
                                        }
                                        else
                                        {
                                            if (Convert.ToInt16(dtAuction.Rows[dtAuction.Rows.Count - 1]["DrawNo"]) + firstValue == Convert.ToInt16(maxDraw))
                                            {
                                                ToNarration = Convert.ToInt16(maxDraw) + " + Excess";
                                            }
                                            else
                                            {
                                                ToNarration = Convert.ToInt16(dtAuction.Rows[dtAuction.Rows.Count - 1]["DrawNo"]) + firstValue + 1 + "P";
                                            }
                                        }

                                        FromNarration = ToNarration;
                                        //
                                    }
                                }
                                else
                                {
                                    TotalPaidAmount = AddTotalPaidAmount;
                                    //TotalPaidAmount = TotalPaidAmount + decimal.Parse(RowtxtAmount.Text);
                                    TotalPaidAmount = TotalPaidAmount + decimal.Parse(GViewCROther_Selected.Rows[i].Cells[4].Text);
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
                                            ToNarration = iAuc + 1 + " PP";
                                            break;
                                        }
                                        //03/09/2021
                                        else
                                        {
                                            excess = tempDueAmount;
                                        }
                                        //
                                    }
                                    if (ToNarration == "")
                                    {
                                        //03/09/2021//ToNarration = "+ Excess Payment";
                                        decimal excessCount = excess / Convert.ToDecimal(Due);
                                        var values = excessCount.ToString(CultureInfo.InvariantCulture).Split('.');

                                        if (values.Length > 1)
                                        {
                                            firstValue = int.Parse(values[0]);
                                            secondValue = long.Parse(values[1]);
                                        }
                                        else
                                        {
                                            firstValue = int.Parse(values[0]);
                                        }

                                        if (secondValue == '0')
                                        {
                                            ToNarration = Convert.ToInt16(dtAuction.Rows[dtAuction.Rows.Count - 1]["DrawNo"]) + firstValue + "F";
                                        }
                                        else
                                        {
                                            ToNarration = Convert.ToInt16(dtAuction.Rows[dtAuction.Rows.Count - 1]["DrawNo"]) + firstValue + 1 + "P";
                                        }
                                        //
                                    }
                                }
                                if (ToNarration != "")
                                {
                                    if (FromDraw != ToDraw)
                                    {
                                        //txtNarration.ToolTip = FromNarration + " To " + ToNarration;
                                        //03/09/2021
                                        if (FromNarration.Contains("P"))
                                            FromNarration = FromNarration.Trim('P');
                                        else if (FromNarration.Contains("F"))
                                            FromNarration = FromNarration.Trim('F');
                                        //
                                        ddltooltip[i] = FromNarration + "F To " + ToNarration;
                                    }
                                    else
                                    {
                                        //txtNarration.ToolTip = ToNarration;
                                        ddltooltip[i] = ToNarration;
                                    }
                                }
                                else
                                {
                                    //txtNarration.ToolTip = FromNarration;
                                    ddltooltip[i] = FromNarration;
                                }
                            }
                            //dtConfirmation.Rows[i]["Draw Details"] = txtNarration.ToolTip.ToString();
                            dtConfirmation.Rows[i]["Draw Details"] = ddltooltip[i];
                            dtConfirmation.Rows[i]["ChitNo"] = GViewCROther_Selected.Rows[i].Cells[2].Text;
                            if (isMisc == true)
                            {
                                if (!dtConfirmation.Columns.Contains("Misc Head"))
                                {
                                    dtConfirmation.Columns.Add("Misc Head");
                                    dtConfirmation.Columns.Add("Misc Amount");
                                }
                                //dtConfirmation.Rows[i]["Misc Head"] = RowddlMisc.SelectedItem.Text;
                                //dtConfirmation.Rows[i]["Misc Amount"] = RowtxtMisc.Text;
                                dtConfirmation.Rows[i]["Misc Head"] = mischead;
                                dtConfirmation.Rows[i]["Misc Amount"] = miscamnt;
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
            catch(Exception ex)
            {
            }
        }
    
        protected void Cheque_TextCahanged(object sender, EventArgs e)
        {
            if (ddlBanklDetails.SelectedItem.Text != "--Select--")
            {
                if (string.IsNullOrEmpty(txtCheque.Text))
                {
                    txtCheque.Text = "0";
                }
                DataTable dt = balayer.GetDataTable("select * from transbank where ChequeDDNO=" + txtCheque.Text + " and CustomersBankName='" + balayer.MySQLEscapeString(ddlBanklDetails.SelectedItem.Text) + "'");
                if (dt.Rows.Count > 0)
                {
                    lbVisible.Visible = true;
                }
                else
                {
                    lbVisible.Visible = false;
                }
            }
            else
            {
                lbVisible.Visible = false;
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

        protected void GView_Selected_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                int index = Convert.ToInt32(e.RowIndex);
                DataTable dtable = ViewState["CurrentTable"] as DataTable;
                dtable.Rows[index].Delete();
                ViewState["CurrentTable"] = dtable;
                GViewCROther_Selected.DataSource = ViewState["CurrentTable"];
                GViewCROther_Selected.DataBind();

                if (GViewCROther_Selected.Rows.Count == 0)
                {
                    SetInitialRow();
                }
            }
            catch (Exception)
            {

            }
            finally
            {

            }
        }

        [System.Web.Services.WebMethod]
        public static string GetCustomername(string hdid)
        {
            string custname = "";
            try
            {
                //custname = balayer.CmnList("select MemberIDNew,concat(MemberID,' | ', CustomerName) as 'CustomerName' from membermaster where TypeOfMember<>'Foreman'");
                BusinessLayer blayer = new BusinessLayer();
                custname = blayer.GetSingleValue("select MemberName from membertogroupmaster where Head_Id=" + hdid + "");
            }
            catch (Exception) { }
            return custname;
        }


        [System.Web.Services.WebMethod]
        public static List<ListItem> ChitToken(string branchid)
        {
            //DataTable dtMC = balayer.GetDataTable("Select distinct receiptseries from assignreceiptbook where moneycollid='" + ddlColloctorName.SelectedValue + "' and IsFinished=0");
            //CommonClassFile objcls = new CommonClassFile();
            BusinessLayer blayer = new BusinessLayer();
            List<ListItem> TList = new List<ListItem>();
            TList.Clear();
            TList = blayer.BindTokenist("SELECT Head_Id,GrpMemberID FROM membertogroupmaster where BranchID=" + branchid + " ");
            return TList;
        }



        [System.Web.Services.WebMethod]
        public static List<ListItem> Getsrchlist(string branchid, string seltext)
        {
            //DataTable dtMC = balayer.GetDataTable("Select distinct receiptseries from assignreceiptbook where moneycollid='" + ddlColloctorName.SelectedValue + "' and IsFinished=0");
            //CommonClassFile objcls = new CommonClassFile();
            BusinessLayer blayer = new BusinessLayer();
            List<ListItem> TList = new List<ListItem>();
            TList.Clear();
            TList = blayer.BindTokenist("SELECT Head_Id,GrpMemberID FROM membertogroupmaster where BranchID=" + branchid + " and  GrpMemberID like '%" + seltext + "%'");
            return TList;
        }


        [System.Web.Services.WebMethod]
        public static List<ListItem> ChitGrp(string branchid)
        {
            //DataTable dtMC = balayer.GetDataTable("Select distinct receiptseries from assignreceiptbook where moneycollid='" + ddlColloctorName.SelectedValue + "' and IsFinished=0");
            BusinessLayer blayer = new BusinessLayer();
            List<ListItem> TList = new List<ListItem>();
            TList.Clear();
            TList = blayer.BindGrpList("select Head_Id, GROUPNO from groupmaster where BranchID=" + branchid + " ");
            return TList;
        }



        [System.Web.Services.WebMethod]
        public static List<ListItem> CRSeries_OtherBranch(string mcid)
        {
            //DataTable dtMC = balayer.GetDataTable("Select distinct receiptseries from assignreceiptbook where moneycollid='" + ddlColloctorName.SelectedValue + "' and IsFinished=0");
            //CommonClassFile objcls = new CommonClassFile();
            BusinessLayer blayer = new BusinessLayer();
            List<ListItem> TList = new List<ListItem>();
            TList.Clear();
            TList = blayer.BindDD_List("Select distinct receiptseries,moneycollid from assignreceiptbook where moneycollid='" + mcid + "' and IsFinished=0");
            return TList;
        }


        [System.Web.Services.WebMethod]
        public static string getRcptNumber(string Series, string CollectorID)
        {
            string receiptno = "";
            try
            {
                //  CommonClassFile objcls = new CommonClassFile();
                BusinessLayer blayer = new BusinessLayer();

                List<ListItem> TList = new List<ListItem>();
                TList.Clear();
                TList = blayer.BindDD_List("Select distinct receiptseries,moneycollid from assignreceiptbook where moneycollid='" + CollectorID + "' and IsFinished=0");

                DataTable dtAll = blayer.GetDataTable("SELECT  alreadyusedreceipts,receiptnoto   FROM svcf.assignreceiptbook where  moneycollid=" + CollectorID + "  and IsFinished=0 and BranchID=" + blayer.ToobjectstrEvenNull(HttpContext.Current.Session["Branchid"]) + " and receiptseries='" + TList[0].Text + "'");
                if (dtAll.Rows.Count != 0)
                {
                    //receiptno = dtAll.Rows[0][1].ToString();
                    int from = int.Parse(dtAll.Rows[0][0].ToString());
                    int t0 = int.Parse(dtAll.Rows[0][1].ToString());
                    string strQuery = "select ifnull(max(Voucher_No)+1,0) from voucher where BranchID=" + blayer.ToobjectstrEvenNull(HttpContext.Current.Session["Branchid"]) + " and Trans_Type=1 and Voucher_No>=" + from + " and Voucher_No<=" + t0 + " and `Series`='" + TList[0].Text + "'";
                    int RecNo = int.Parse(blayer.GetSingleValue(strQuery));
                    if (RecNo != 0)
                    {
                        receiptno = RecNo.ToString();
                    }
                    else
                    {
                        receiptno = from.ToString();
                    }
                }
                else
                {
                    receiptno = "0";
                    //ScriptManager.RegisterStartupScript(this, GetType(), "Warning", "alert(' Please Assign new Reciept Book!!!');", true);
                }

            }
            catch (Exception) { }
            return receiptno;
        }

        [System.Web.Services.WebMethod]
        public static string gtRcptBkNumber(string Series, string CollectorID)
        {
            string receiptno = "";
            try
            {
                // CommonClassFile objcls = new CommonClassFile();
                BusinessLayer blayer = new BusinessLayer();

                DataTable dtAll = blayer.GetDataTable("SELECT  alreadyusedreceipts,receiptnoto   FROM svcf.assignreceiptbook where  moneycollid=" + CollectorID + "  and IsFinished=0 and BranchID=" + blayer.ToobjectstrEvenNull(HttpContext.Current.Session["Branchid"]) + " and receiptseries='" + Series + "'");
                if (dtAll.Rows.Count != 0)
                {
                    int from = int.Parse(dtAll.Rows[0][0].ToString());
                    int t0 = int.Parse(dtAll.Rows[0][1].ToString());
                    string strQuery = "select ifnull(max(Voucher_No)+1,0) from voucher where BranchID=" + blayer.ToobjectstrEvenNull(HttpContext.Current.Session["Branchid"]) + " and Trans_Type=1 and Voucher_No>=" + from + " and Voucher_No<=" + t0 + " and `Series`='" + Series + "'";
                    int RecNo = int.Parse(blayer.GetSingleValue(strQuery));
                    if (RecNo != 0)
                    {
                        receiptno = RecNo.ToString();
                    }
                    else
                    {
                        receiptno = from.ToString();
                    }
                }
                else
                {
                    receiptno = "0";
                    //ScriptManager.RegisterStartupScript(this, GetType(), "Warning", "alert(' Please Assign new Reciept Book!!!');", true);
                }

            }
            catch (Exception) { }
            return receiptno;
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            try
            {
                AddNewRowToGrid();
            }
            catch (Exception) { }
        }

        protected void GViewCROther_Selected_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}