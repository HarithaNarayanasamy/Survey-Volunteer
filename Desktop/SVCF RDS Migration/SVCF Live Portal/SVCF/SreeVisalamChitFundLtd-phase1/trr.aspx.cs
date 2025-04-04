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
using System.Globalization;

namespace SreeVisalamChitFundLtd_phase1
{
    public partial class trr : System.Web.UI.Page
    {



        #region VarDeclaration
        Dictionary<string, string> Tempdic = new Dictionary<string, string>();
        List<string> TempList = new List<string>();

        string mindat, maxdt;
        static string[] ddltooltip;
       // static List<string> RefNo = new List<string>();
       // static List<string> RefNo1 = new List<string>();
        static long RCNumber = 0;

        #endregion

        #region VarDeclaration
        BusinessLayer balayer = new BusinessLayer();
        TransactionLayer trn = new TransactionLayer();
        int index = 0;
        #endregion


        #region VarDeclaration
        int from;
        int t0;
        int RecNo;
        decimal lastAmount;
        bool isMisc;
        string GroupID;
        string ChitsBranchID;
        string strChitmISCHeadQuery;
        string strCashmISCHeadQuery;
        string strBankMiscInsertQuery;        
        string strChitMiscHeadQuery;
        string strCashHeadMiscQuery;        
        string strChitHeadQuery;
        string strCashHeadQuery;
        long strChitHead;
        long strCashHead;       
        string strBankInsertQuery;       
        #endregion

        ILog logger = log4net.LogManager.GetLogger(typeof(trr));
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
                Session["CheckRefresh"] = System.Guid.NewGuid().ToString();
                SetInitialRow();
                CollectorName();
                balayer.fillBankHd(ddlBanklDetails);
                ddlColloctorName.Focus();
                LoadDropDownList();
                fillBankHead();
                fillEmployee();
                lbCardType.Visible = false;
                lbIdCardNumber.Visible = false;
                txtIdcardNumber.Visible = false;
                ddlCardType.Visible = false;
                cvddlCardType.Visible = false;
                rvCardNumber.Visible = false;
                string Choosendate = balayer.GetSingleValue("SELECT DATE_FORMAT(DateInCheque,'%d/%m/%Y') from `svcf`.`transbank` where BranchID=" + Session["Branchid"] + " order by DateInCheque desc");
                txtReceivedDate.Text = "";
                lblcancelmsg.Text = "";
                RCNumber = 0;

            }
            rvDate.MinimumValue = balayer.GetSingleValue("SELECT DATE_FORMAT( MinimumDate, '%d/%m/%Y') MinimumDate FROM svcf.restrictionmaster where BranchID=" + Session["Branchid"]);
            rvDate.MaximumValue = DateTime.Now.ToString("dd/MM/yyyy");
            logger.Info("TRR Page - at: " + DateTime.Now + " by: " + Convert.ToString(Session["UserName"]) + "");
        }
        void fillEmployee()
        {
            //Tempdic.Clear();

            //Tempdic = balayer.CmnList("SELECT Emp_Name,Emp_ID FROM svcf.employee_details where BranchID=" + Session["Branchid"]);

            //ddlEmployee.DataSource = Tempdic;
            //ddlEmployee.DataTextField = "Key";
            //ddlEmployee.DataValueField = "Value";
            DataTable dt = balayer.GetDataTable("SELECT Emp_Name,Emp_ID FROM svcf.employee_details where BranchID=" + Session["Branchid"]);
            DataRow dr = dt.NewRow();
            dr[0] = "--Select--";
            dr[1] = "0";
            ddlEmployee.DataTextField = "Emp_Name";
            ddlEmployee.DataValueField = "Emp_ID";
            dt.Rows.InsertAt(dr, 0);
            ddlEmployee.DataSource = dt;
            ddlEmployee.DataBind();
            //ddlEmployee.Items.Insert(0, new ListItem("--Select--", "0"));
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
            dtBank.Dispose();
            //**********************************************

            //Tempdic.Clear();


            //Tempdic = balayer.CmnList("SELECT concat( concat(t1.BankName,' _ ',t1.IFCCode),' _ ',t1.AccountNo) as BankDetails, t1.Head_Id as Head_Id, t1.Banklocation as Banklocation FROM svcf.bankdetails as t1 where t1.BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]));


            //ddlBankHead.DataSource = Tempdic;
            //ddlBankHead.DataTextField = "Key";
            //ddlBankHead.DataValueField = "Value";
            //ddlBankHead.DataBind();
            //ddlBankHead.Items.Insert(0, new ListItem("--Select--", "0"));


        }

        [System.Web.Services.WebMethod]
        public static List<ListItem> PopulateTRSeries(string mcid)
        {
            //DataTable dtMC = balayer.GetDataTable("Select distinct receiptseries from assignreceiptbook where moneycollid='" + ddlColloctorName.SelectedValue + "' and IsFinished=0");
            CommonClassFile objcls = new CommonClassFile();
            List<ListItem> TList = new List<ListItem>();
            TList.Clear();
            TList = objcls.BindTRDD("Select distinct receiptseries,moneycollid from assignreceiptbook where moneycollid='" + mcid + "' and IsFinished=0");
            return TList;
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
        public static string GetMemberid(string hdid)
        {
            string custid = "";
            try
            {
                //custname = balayer.CmnList("select MemberIDNew,concat(MemberID,' | ', CustomerName) as 'CustomerName' from membermaster where TypeOfMember<>'Foreman'");
                BusinessLayer blayer = new BusinessLayer();
                custid = blayer.GetSingleValue("select MemberID from membertogroupmaster where Head_Id=" + hdid + "");
            }
            catch (Exception) { }
            return custid;
        }

        [System.Web.Services.WebMethod]
        public static string getRcptNumber(string Series, string CollectorID)
        {
            string receiptno = "";
            try
            {
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
        public static string gtTRcptBkNumber(string Series, string CollectorID)
        {
            string receiptno = "";
            try
            {
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


        [System.Web.Services.WebMethod]
        public static List<ListItem> PopulateTToken(string mcid)
        {
            BusinessLayer blayer = new BusinessLayer();
            List<ListItem> TmpList = new List<ListItem>();
            TmpList.Clear();
            TmpList = blayer.BndTkn("SELECT GrpMemberID,Head_Id FROM membertogroupmaster where  MemberID=" + mcid + "");
            return TmpList;
        }

        public void CollectorName()
        {
            //    DataTable dtCollector = balayer.GetDataTable("Select moneycollid,moneycollname from moneycollector where BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]));
            //    DataRow dr = dtCollector.NewRow();
            //    dr[0] = "0";
            //    dr[1] = "--Select--";
            //    ddlColloctorName.DataValueField = "moneycollid";
            //    ddlColloctorName.DataTextField = "moneycollname";
            //    dtCollector.Rows.InsertAt(dr, 0);
            //    ddlColloctorName.DataSource = dtCollector;
            //    ddlColloctorName.DataBind();
            //    ddlReceiptSeries.Focus();

            //*******************************************************

            Tempdic.Clear();


            // Tempdic = balayer.CmnList("Select moneycollid,moneycollname from moneycollector where BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]));
            Tempdic = balayer.CmnList("Select distinct asr.moneycollid,mc.moneycollname from moneycollector as mc join assignreceiptbook as asr " +
                   "on (asr.moneycollid=mc.moneycollid) where asr.IsFinished=0 and asr.BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]));

            ddlColloctorName.DataSource = Tempdic;
            ddlColloctorName.DataTextField = "Value";
            ddlColloctorName.DataValueField = "Key";
            ddlColloctorName.DataBind();
            //ddlColloctorName.Items.Insert(0, new ListItem("--Select--", "0"));
            ddlColloctorName.Items.Insert(0, new ListItem("--Select--", "0"));
            ddlReceiptSeries.Focus();
        }
        protected void ddlReceiptSeries_SelectedIndexChanged(object sender, EventArgs e)
        {
            getRecieptBookNO(ddlReceiptSeries.SelectedValue, ddlColloctorName.SelectedValue);
            ddlEmployee.Focus();
        }
        public void getRecieptBookNO(string Series, string CollectorID)
        {
            DataTable dtAll = balayer.GetDataTable("SELECT  alreadyusedreceipts,receiptnoto   FROM svcf.assignreceiptbook where  moneycollid=" + ddlColloctorName.SelectedValue + "  and IsFinished=0 and BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and receiptseries='" + ddlReceiptSeries.SelectedItem.Text + "'");
            if (dtAll.Rows.Count != 0)
            {
                from = int.Parse(dtAll.Rows[0][0].ToString());
                t0 = int.Parse(dtAll.Rows[0][1].ToString());
                string strQuery = "select ifnull(max(Voucher_No)+1,0) from voucher where BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and Trans_Type=1 and Voucher_No>=" + from + " and Voucher_No<=" + t0 + " and `Series`='" + ddlReceiptSeries.SelectedItem.Text + "'";
                RecNo = int.Parse(balayer.GetSingleValue(strQuery));
                //TextBox ReceiptNo = (TextBox)GridView1.Rows[GridView1.Rows.Count - 1].FindControl("txtReceiptNo");
                if (RecNo != 0)
                {
                    //ReceiptNo.Text = RecNo.ToString();
                }
                else
                {
                    // ReceiptNo.Text = from.ToString();
                }
            }
            else
            {
                //TextBox ReceiptNo = (TextBox)GridView1.Rows[GridView1.Rows.Count - 1].FindControl("txtReceiptNo");
                //ReceiptNo.Text = "0";
                ScriptManager.RegisterStartupScript(this, GetType(), "Warning", "alert(' Please Assign new Reciept Book!!!');", true);
            }
        }
        void series()
        {
            //DataTable dtMC = balayer.GetDataTable("Select distinct receiptseries from assignreceiptbook where moneycollid='" + ddlColloctorName.SelectedValue + "' and IsFinished=0");
            //if (dtMC.Rows.Count == 0)
            //{
            //    ClientScript.RegisterStartupScript(this.Page.GetType(), "Validation", "<script Language='Javascript'>alert('Please Assign Receipt Book For " + ddlColloctorName.SelectedItem.Text + " (" + ddlColloctorName.SelectedValue + ") ')</script>");
            //}
            //else if ((dtMC.Rows.Count > 1))
            //{
            //    DataTable dtMCtemp = balayer.GetDataTable("select distinct Series as receiptseries from receiptmaster where (select mod(max(ReceiptNo) ,200) <>'0') and CollectedBy='" + ddlColloctorName.SelectedItem.Text + "' ");
            //    if (dtMCtemp.Rows.Count != 0)
            //    {
            //        dtMC = dtMCtemp.Copy();
            //    }
            //}
            //DataRow dr = dtMC.NewRow();
            //dr[0] = "--Select--";
            //ddlReceiptSeries.DataValueField = "receiptseries";
            //ddlReceiptSeries.DataTextField = "receiptseries";
            //ddlReceiptSeries.DataSource = dtMC;
            //dtMC.Rows.InsertAt(dr, 0);
            //ddlReceiptSeries.DataBind();

            //ddlColloctorName.Focus();
            ////SetInitialRow();

            //****************************************************************

            List<string> TL = new List<string>();

            //DataTable dtMC = balayer.GetDataTable("Select distinct receiptseries from assignreceiptbook where moneycollid='" + ddlColloctorName.SelectedValue + "' and IsFinished=0");
            TL = balayer.RetrveList("Select distinct receiptseries from assignreceiptbook where moneycollid='" + ddlColloctorName.SelectedValue + "' and IsFinished=0");

            if (TL.Count == 0)
            {
                ClientScript.RegisterStartupScript(this.Page.GetType(), "Validation", "<script Language='Javascript'>alert('Please Assign Receipt Book For " + ddlColloctorName.SelectedItem.Text + " (" + ddlColloctorName.SelectedValue + ") ')</script>");
            }
            else if ((TL.Count >= 1))
            {
                //DataTable dtMCtemp = balayer.GetDataTable("select distinct Series as receiptseries from receiptmaster where (select mod(max(ReceiptNo) ,200) <>'0') and CollectedBy='" + ddlColloctorName.SelectedItem.Text + "' ");
                TempList.Clear();
                TempList = balayer.RetrveList("select distinct Series as receiptseries from receiptmaster where (select mod(max(ReceiptNo) ,200) <>'0') and CollectedBy='" + ddlColloctorName.SelectedItem.Text + "' ");
                if (TempList.Count != 0)
                {
                    TL = TempList.ToList();
                }
            }

            ddlReceiptSeries.DataSource = TL;

            ddlReceiptSeries.DataBind();
            ddlReceiptSeries.Items.Insert(0, new ListItem("--Select--"));
            ddlColloctorName.Focus();
        }
        protected void ddlColloctorName_SelectedIndexChanged(object sender, EventArgs e)
        {
            series();
        }
        private void FillDropDownList(DropDownList ddl, int iType, string MemberID)
        {  //chit
            //if (iType == 0)
            //{
            //    ddl.DataSource = null;
            //    DataTable dtgroupno = null;
            //    dtgroupno = balayer.GetDataTable("select MemberIDNew,concat(MemberID,' | ', CustomerName) as CustomerName from membermaster where TypeOfMember<>'Foreman'");
            //    DataRow dr = dtgroupno.NewRow();
            //    dr[0] = "0";
            //    dr[1] = "--Select--";
            //    ddl.DataValueField = "MemberIDNew";
            //    ddl.DataTextField = "CustomerName";
            //    dtgroupno.Rows.InsertAt(dr, 0);
            //    ddl.DataSource = dtgroupno;

            //    ddl.DataBind();
            //}
            ////token
            //else if (iType == 1)
            //{
            //    DropDownList ddlMemberName = (DropDownList)GridView1.Rows[0].Cells[1].FindControl("ddlMemberName");
            //    if (ddlMemberName.SelectedItem.Text != "--Select--")
            //    {
            //        ddl.DataSource = null;
            //        DataTable dt = null;
            //        dt = balayer.GetDataTable(@"SELECT GrpMemberID,Head_Id FROM membertogroupmaster where  MemberID=" + MemberID );
            //        DataRow dr = dt.NewRow();
            //        dr[0] = "--Select--";
            //        dr[1] = "0";
            //        dt.Rows.InsertAt(dr, 0);
            //        ddl.DataSource = dt;
            //        ddl.DataValueField = "Head_Id";
            //        ddl.DataTextField = "GrpMemberID";
            //        ddl.DataBind();
            //    }
            //    else
            //    {
            //        ddl.DataSource = null;
            //        ddl.DataSource = null;
            //        DataTable dt = balayer.GetDataTable(@"SELECT GrpMemberID,Head_Id FROM membertogroupmaster where  MemberID=0");
            //        DataRow dr = dt.NewRow();
            //        dr[0] = "--Select--";
            //        dr[1] = "0";
            //        dt.Rows.InsertAt(dr, 0);
            //        ddl.DataBind();
            //    }
            //}
            ////misc
            //else if (iType == 2)
            //{
            //    ddl.DataSource = null;
            //    DataTable dtgroupno = null;
            //    dtgroupno = balayer.GetDataTable("SELECT concat(cast(TreeID as char),',',cast(RootID as char)) as TreeID,TREE FROM svcf.view_parent where RootID not in(5,6,3) and( BranchID is null or BranchID=" + Session["Branchid"] + ")");
            //    DataRow dr = dtgroupno.NewRow();
            //    dr[0] = "0,0";
            //    dr[1] = "--Select--";
            //    ddl.DataValueField = "TreeID";
            //    ddl.DataTextField = "TREE";
            //    dtgroupno.Rows.InsertAt(dr, 0);
            //    ddl.DataSource = dtgroupno;
            //    ddl.DataBind();
            //}

            //**************************************************

            if (iType == 0)
            {
                ddl.DataSource = null;

                Tempdic.Clear();

                Tempdic = balayer.CmnList("select MemberIDNew,concat(MemberID,' | ', CustomerName) as 'CustomerName' from membermaster where TypeOfMember<>'Foreman'");

                ddl.DataValueField = "Key";
                ddl.DataTextField = "Value";
                ddl.DataSource = Tempdic;


                ddl.DataBind();
                ddl.Items.Insert(0, new ListItem("--Select--", "0"));
            }
            //token
            else if (iType == 1)
            {
                //DropDownList ddlMemberName = (DropDownList)GridView1.Rows[0].Cells[1].FindControl("ddlMemberName");
                //if (ddlMemberName.SelectedItem.Text != "--Select--")
                //{
                //    ddl.DataSource = null;
                //    //DataTable dt = null;
                //    Tempdic.Clear();
                //    Tempdic = balayer.CmnList(@"SELECT Head_Id,GrpMemberID FROM membertogroupmaster where  MemberID=" + MemberID);

                //    ddl.DataValueField = "Key";
                //    ddl.DataTextField = "Value";
                //    ddl.DataSource = Tempdic;
                //    ddl.DataBind();
                //    ddl.Items.Insert(0, new ListItem("--Select--", "0"));

                //}
                //else
                //{
                //    ddl.DataSource = null;
                //    ddl.DataSource = null;
                //    Tempdic.Clear();

                //    Tempdic = balayer.CmnList(@"SELECT Head_Id,GrpMemberID FROM membertogroupmaster where  MemberID=0");

                //    ddl.DataValueField = "Key";
                //    ddl.DataTextField = "Value";
                //    ddl.DataSource = Tempdic;
                //    ddl.DataBind();
                //    ddl.Items.Insert(0, new ListItem("--Select--", "0"));
                //}
            }
            //misc
            else if (iType == 2)
            {
                ddl.DataSource = null;
                //DataTable dtgroupno = null;
                Tempdic.Clear();
                //dtgroupno = balayer.GetDataTable("SELECT concat(cast(TreeID as char),',',cast(RootID as char)) as TreeID,TREE FROM svcf.view_parent where RootID<>5 and (BranchID is null or BranchID=" + Session["Branchid"] + ")");
                Tempdic = balayer.CmnList("SELECT concat(cast(TreeID as char),',',cast(RootID as char)) as TreeID,TREE FROM svcf.view_parent where RootID<>5 and (BranchID is null or BranchID=" + Session["Branchid"] + ")");

                ddl.DataValueField = "Key";
                ddl.DataTextField = "Value";
                ddl.DataSource = Tempdic;
                ddl.DataBind();
                ddl.Items.Insert(0, new ListItem("--Select--", "0,0"));
            }
            else if (iType == 3)
            {
                // DropDownList ddlMemberName = (DropDownList)GridView1.Rows[0].Cells[1].FindControl("ddlMemberName");

                Tempdic.Clear();
                //Tempdic = balayer.CmnList(@"SELECT Head_Id,GrpMemberID FROM membertogroupmaster where  MemberID=" + MemberID + " and BranchID=" + Session["Branchid"]);
                //Tempdic = balayer.CmnList(@"SELECT Head_Id,GrpMemberID FROM membertogroupmaster where  MemberID=" + MemberID);
                Tempdic = balayer.CmnList(@"SELECT Head_Id,GrpMemberID FROM membertogroupmaster where BranchID=" + Session["Branchid"]);
                ddl.DataValueField = "Key";
                ddl.DataTextField = "Value";

                ddl.DataSource = Tempdic;
                ddl.DataBind();

                ddl.Items.Insert(0, new ListItem("--Select--", "0"));
            }

        }

        private void SetInitialRow()
        {
            DataTable dt = new DataTable();
            DataRow dr = null;
            //Define the Columns
            dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
            dt.Columns.Add(new DataColumn("RecptNumber", typeof(string)));
            dt.Columns.Add(new DataColumn("MemberName", typeof(string)));
            dt.Columns.Add(new DataColumn("Token", typeof(string)));
            dt.Columns.Add(new DataColumn("Amount", typeof(string)));
            dt.Columns.Add(new DataColumn("MiscHead", typeof(string)));
            dt.Columns.Add(new DataColumn("MiscAmount", typeof(string)));
            dt.Columns.Add(new DataColumn("Head_Id", typeof(string)));
            dt.Columns.Add(new DataColumn("MemberId", typeof(string)));

            dt.Columns.Add(new DataColumn("firstmisc", typeof(string)));
            dt.Columns.Add(new DataColumn("secmisc", typeof(string)));



            //Add a Dummy Data on Initial Load
            dr = dt.NewRow();

            dt.Rows.Add(dr);
            ViewState["CurrentTable"] = dt;

            GView_Selected.DataSource = dt;
            GView_Selected.DataBind();

        }

        public void LoadDropDownList()
        {
            FillDropDownList(ddlMisc, 2, "");
            FillDropDownList(ddlToken, 3, "");
        }


        private void SetInitialRowol()
        {
            DataTable dt = new DataTable();
            DataRow dr = null;
            //Define the Columns
            dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
            dt.Columns.Add(new DataColumn("MemberName", typeof(string)));
            dt.Columns.Add(new DataColumn("Token", typeof(string)));
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
            //GridView1.DataSource = dt;
            //GridView1.DataBind();
            //Extract and Fill the DropDownList with Data
            // DropDownList ddlMemberName = (DropDownList)GridView1.Rows[0].Cells[1].FindControl("ddlMemberName");
            //DropDownList ddlMisc = (DropDownList)GridView1.Rows[0].Cells[2].FindControl("ddlMisc");

            //FillDropDownList(ddlMemberName, 0, "");
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

                    //    GridView1.DataSource = dtCurrentTable;
                    //   GridView1.DataBind();
                    ViewState["CurrentTable"] = dtCurrentTable;
                }
            }

            SetPreviousData(true);
            //TextBox ReceiptNo = (TextBox)GridView1.Rows[GridView1.Rows.Count - 1].FindControl("txtReceiptNo");
            // ReceiptNo.Focus();
        }
        private void AddNewRowToGrid()
        {
            if (ViewState["CurrentTable"] != null)
            {
                DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
                DataRow drCurrentRow = null;

                if (dtCurrentTable.Rows.Count > 0)
                {
                    // DropDownList ddlMiscVal = (DropDownList)GridView1.Rows[dtCurrentTable.Rows.Count - 1].FindControl("ddlMisc");
                    // TextBox txtMiscVal = (TextBox)GridView1.Rows[dtCurrentTable.Rows.Count - 1].FindControl("txtMisc");
                    decimal lastAmount = 0.0M;
                    // bool isMisc = decimal.TryParse(txtMiscVal.Text, out lastAmount);
                    //if ((isMisc == true & lastAmount > 0.0M))
                    //{
                    //    isMisc = true;
                    //}
                    //else
                    //{
                    //    isMisc = false;
                    //}
                    //if ((ddlMiscVal.SelectedIndex <= 0) & isMisc == true)
                    //{
                    //    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Choose Misc Head Valid Details');", true);
                    //    //Response.Write("<script>alert('Please Provide Valid Details');</script>");
                    //    return;
                    //}
                    //if (ddlMiscVal.SelectedIndex > 0 & isMisc != true)
                    //{
                    //    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Provide Misc Amount Details');", true);
                    //    return;
                    //    //Response.Write("<script>alert('Please Provide Valid Details');</script>");
                    //}
                    drCurrentRow = dtCurrentTable.NewRow();
                    drCurrentRow["RowNumber"] = dtCurrentTable.Rows.Count + 1;
                    //add new row to DataTable
                    dtCurrentTable.Rows.Add(drCurrentRow);
                    //Store the current data to ViewState
                    ViewState["CurrentTable"] = dtCurrentTable;
                    for (int i = 0; i < dtCurrentTable.Rows.Count - 1; i++)
                    {
                        // Update the DataRow with the DDL Selected Items
                        dtCurrentTable.Rows[i]["MemberName"] = TxtMemberName.Text;
                        dtCurrentTable.Rows[i]["Token"] = ddlToken.SelectedValue;
                        dtCurrentTable.Rows[i]["Amount"] = txtAmount.Text;
                        dtCurrentTable.Rows[i]["MiscHead"] = ddlMisc.SelectedValue;
                        dtCurrentTable.Rows[i]["MiscAmount"] = txtMisc.Text;
                        dtCurrentTable.Rows[i]["txtReceiptNo"] = txtReceiptNo.Text;
                    }
                }
            }

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
                    for (int i = 0; i <= dt.Rows.Count - 1; i++)
                    {
                        FillDropDownList(ddlMisc, 2, "");
                        FillDropDownList(ddlToken, 3, "");
                        //FillDropDownList(ddl3);
                        if (i < dt.Rows.Count)
                        {
                            if (isRemove == false & i == dt.Rows.Count - 1)
                            {
                                break;
                            }
                            ddlMisc.ClearSelection();
                            ddlMisc.Items.FindByValue(dt.Rows[i]["MiscHead"].ToString()).Selected = true;
                            txtAmount.Text = dt.Rows[i]["Amount"].ToString();
                            txtMisc.Text = dt.Rows[i]["MiscAmount"].ToString();
                            txtReceiptNo.Text = dt.Rows[i]["txtReceiptNo"].ToString();
                        }
                        rowIndex++;
                    }
                    if (isRemove == false)
                    {

                    }
                }
            }
        }
        protected void ddlMemberName_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow gvRow = (GridViewRow)((DropDownList)sender).Parent.Parent;
            DropDownList ddlToken = (DropDownList)gvRow.FindControl("ddlToken");
            string selectedValue = ((DropDownList)gvRow.FindControl("ddlMemberName")).SelectedValue;
            FillDropDownList(ddlToken, 1, selectedValue);
            ((DropDownList)gvRow.FindControl("ddlMemberName")).Focus();

        }
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
        void visibility()
        {

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
        void visibility2()
        {

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
        void visibility1()
        {
            Code = new List<string>();


            lbCardType.Visible = false;
            lbIdCardNumber.Visible = false;
            txtIdcardNumber.Visible = false;
            ddlCardType.Visible = false;
            cvddlCardType.Visible = false;
            rvCardNumber.Visible = false;

        }
        protected void ButtonRemove_Click(object sender, ImageClickEventArgs e)
        {

        }

        public void PopulateDropDownList(List<ListItem> list, DropDownList ddl)
        {
            ddl.DataSource = list;
            ddl.DataTextField = "Text";
            ddl.DataValueField = "Value";
            ddl.DataBind();
        }

        public void GetRCBookno(string Series, string CollectorID)
        {
            DataTable dtAll = balayer.GetDataTable("SELECT  alreadyusedreceipts,receiptnoto   FROM svcf.assignreceiptbook where  moneycollid=" + CollectorID + "  and IsFinished=0 and BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and receiptseries='" + Series + "'");
            if (dtAll.Rows.Count != 0)
            {
                int from = int.Parse(dtAll.Rows[0][0].ToString());
                int t0 = int.Parse(dtAll.Rows[0][1].ToString());
                string strQuery = "select ifnull(max(Voucher_No)+1,0) from voucher where BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and Trans_Type=1 and Voucher_No>=" + from + " and Voucher_No<=" + t0 + " and `Series`='" + Series + "'";
                int RecNo = int.Parse(balayer.GetSingleValue(strQuery));
                txtReceiptNo.Text = RecNo.ToString();
                //TextBox ReceiptNo = (TextBox)GridView1.Rows[GridView1.Rows.Count - 1].FindControl("txtReceiptNo");

            }
            else
            {
                //TextBox ReceiptNo = (TextBox)GridView1.Rows[GridView1.Rows.Count - 1].FindControl("txtReceiptNo");
                //ReceiptNo.Text = "0";
                ScriptManager.RegisterStartupScript(this, GetType(), "Warning", "alert(' Please Assign new Reciept Book!!!');", true);
            }
        }

        protected void ButtonAdd_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                Page.Validate("GrpRow");

                if (Page.IsValid)
                {

                    string selectedMemId = "";
                    string selectedRSeries = "";
                    string LoadedSeries = "";
                    lblcancelmsg.Text = "";

                    RCNumber = Convert.ToInt32(txtReceiptNo.Text);

                    selectedRSeries = Request.Form[ddlColloctorName.UniqueID];
                    selectedMemId = hiddenmemberid.Value;
                    LoadedSeries = HD_RSeriesid.Value;

                    PopulateDropDownList(PopulateTRSeries(selectedRSeries), ddlReceiptSeries);
                    ddlReceiptSeries.Items.FindByText(LoadedSeries).Selected = true;
                    if (Chksamercno.Checked == true)
                    {
                        RCNumber = Convert.ToInt32(Hd_SameRCNo.Value);
                        txtReceiptNo.Text = Hd_SameRCNo.Value;
                    }
                    else
                    {
                        GetRCBookno(LoadedSeries, selectedRSeries);
                    }
                    //txtReceiptNo.Text = Rcptnumber.ToString();

                    if (ViewState["CurrentTable"] != null)
                    {
                        DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
                        DataRow drCurrentRow = null;
                        if (dtCurrentTable.Rows.Count > 0)
                        {
                            lastAmount = 0.0M;
                            isMisc = decimal.TryParse(txtMisc.Text, out lastAmount);
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
                            //drCurrentRow = dtCurrentTable.NewRow();
                            //drCurrentRow["RowNumber"] = dtCurrentTable.Rows.Count + 1;
                            ////add new row to DataTable
                            //dtCurrentTable.Rows.Add(drCurrentRow);
                            //Store the current data to ViewState
                            //ViewState["CurrentTable"] = dtCurrentTable;
                            for (int i = 0; i <= dtCurrentTable.Rows.Count - 1; i++)
                            {
                                drCurrentRow = dtCurrentTable.NewRow();
                                // Update the DataRow with the DDL Selected Items
                                drCurrentRow["RecptNumber"] = RCNumber.ToString();
                                drCurrentRow["MemberName"] = TxtMemberName.Text;
                                drCurrentRow["Token"] = ddlToken.SelectedItem;
                                drCurrentRow["Amount"] = txtAmount.Text;
                                drCurrentRow["MiscHead"] = ddlMisc.SelectedItem;
                                drCurrentRow["MiscAmount"] = txtMisc.Text;
                                drCurrentRow["Head_Id"] = ddlToken.SelectedValue;
                                drCurrentRow["MemberId"] = selectedMemId;
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

                            //Remove initial blank row
                            if (dtCurrentTable.Rows[0][0].ToString() == "")
                            {
                                dtCurrentTable.Rows[0].Delete();
                                dtCurrentTable.AcceptChanges();
                            }
                            //Rebind the Grid with the current data
                            drCurrentRow["RowNumber"] = dtCurrentTable.Rows.Count + 1;
                            //if (ddlMisc.SelectedValue != null)
                            //{
                            //    RefNo.Add(ddlMisc.SelectedValue.Split(',')[0]);
                            //    RefNo1.Add(ddlMisc.SelectedItem.Value.Split(',')[1]);
                            //}
                            dtCurrentTable.Rows.Add(drCurrentRow);
                            ViewState["CurrentTable"] = dtCurrentTable;

                            //Rebind the Grid with the current data
                            GView_Selected.DataSource = dtCurrentTable;
                            GView_Selected.DataBind();
                            txtMisc.Text = "";
                            txtAmount.Text = "";

                            ddlToken.ClearSelection();
                            ddlMisc.ClearSelection();
                            lblcancelmsg.Text = "";
                            TxtMemberName.Text = "";
                            btnGenerate.Focus();
                        }
                    }

                    // SetPreviousData(false);
                }

            }
            catch (Exception) { }
        }
        protected void btnConfirmationNo_Click(object sender, EventArgs e)
        {
            gvConfirm.DataSource = null;
            gvConfirm.DataBind();
            ModalPopupExtender1.Hide();
            pnlConfirmation.Visible = false;
            string selectedRSeries = "";
            string LoadedSeries = "";
            //long RCNumber = 0;
            // RCNumber = Convert.ToInt32(txtReceiptNo.Text);

            selectedRSeries = Request.Form[ddlColloctorName.UniqueID];
            //LoadedSeries = Request.Form[ddlReceiptSeries.UniqueID];
            LoadedSeries = HD_RSeriesid.Value;
            LoadedSeries.Trim();
            PopulateDropDownList(PopulateTRSeries(selectedRSeries), ddlReceiptSeries);
            ddlReceiptSeries.Items.FindByText(LoadedSeries).Selected = true;
            GetRCBookno(LoadedSeries, selectedRSeries);
        }
        protected void btnConfirmationYes_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["CheckRefresh"].ToString() != ViewState["CheckRefresh"].ToString())
                {
                    return;
                }
                lblcancelmsg.Text = "";
                string selectedRSeries = "";
                string LoadedSeries = "";
                string Rcptnumber = "";
                long strChitMiscHead = 0;
                long strCashHeadMisc = 0;
                //long RCNumber = 0;
                // RCNumber = Convert.ToInt32(txtReceiptNo.Text);

                selectedRSeries = Request.Form[ddlColloctorName.UniqueID];
                //LoadedSeries = Request.Form[ddlReceiptSeries.UniqueID];
                LoadedSeries = HD_RSeriesid.Value;
                LoadedSeries.Trim();
                PopulateDropDownList(PopulateTRSeries(selectedRSeries), ddlReceiptSeries);
                ddlReceiptSeries.Items.FindByText(LoadedSeries).Selected = true;

                //txtReceiptNo.Text = Rcptnumber.ToString();
                //GetRCBookno(LoadedSeries, selectedRSeries);

                //  TransactionLayer trn = new TransactionLayer();
                string TransactionKeyDue = "";
                gvConfirm.DataSource = null;
                gvConfirm.DataBind();
                System.Guid guid = Guid.NewGuid();

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
                    ClsSession objSession = (ClsSession)Session["objSession"]; 
                    string memname = "", tokentxt = "", txtamnt = "", ddlmisc = "", txtmisc = "", tokenhid = "", rcptno = "";
                    string MemberID = "", TokenNo = "", RootID = "";
                    string mischd1 = "", mischd2 = "";
                    long receiptno = 0;
                    for (int i = 0; i <= GView_Selected.Rows.Count - 1; i++)
                    {
                        rcptno = GView_Selected.Rows[i].Cells[1].Text;
                        receiptno = Convert.ToInt32(rcptno);
                        //receiptno = RCNumber.ToString();
                        memname = GView_Selected.Rows[i].Cells[2].Text;
                        if (memname.Contains("&amp;"))
                        {
                            memname = memname.Replace("&amp;", "&");
                        }

                        tokentxt = GView_Selected.Rows[i].Cells[3].Text;
                        Label tokenid = (Label)GView_Selected.Rows[i].FindControl("lblheadid");
                        Label memberid = (Label)GView_Selected.Rows[i].FindControl("lblmemberid");
                        tokenhid = tokenid.Text;
                        txtamnt = GView_Selected.Rows[i].Cells[4].Text;
                        ddlmisc = GView_Selected.Rows[i].Cells[5].Text;
                        ddlmisc = ddlmisc.Replace("&gt;&gt;", ">>");
                        //&gt;&gt;
                        txtmisc = GView_Selected.Rows[i].Cells[6].Text;
                        if (txtmisc == "&nbsp;")
                        {
                            txtmisc = "0.0";
                        }
                        if (ddlmisc != "--Select--")
                        {
                            Label r1 = (Label)GView_Selected.Rows[i].FindControl("lblref1");
                            Label r2 = (Label)GView_Selected.Rows[i].FindControl("lblref2");

                            mischd1 = r1.Text;
                            mischd2 = r2.Text;
                        }

                        if (ddlmisc == "--Select--")
                        {
                            ddlmisc = "";
                        }
                        GroupID = balayer.GetSingleValue("select GroupID from membertogroupmaster where Head_Id=" + tokenhid);
                        ChitsBranchID = balayer.GetSingleValue("SELECT BranchID FROM `svcf`.`groupmaster` where Head_Id=" + GroupID);
                        MemberID = memberid.Text;
                        TokenNo = tokenhid;
                        RootID = "";
                        if (CheckBox1.Checked == true)
                        {
                            RootID = "3";
                        }
                        else
                        {
                            RootID = "12";
                        }

                        //CURRENT BRANCH
                        if (ChitsBranchID == balayer.ToobjectstrEvenNull(Session["Branchid"]))
                        {
                            if (decimal.Parse(txtamnt) != 0.00M)
                            {
                                if (receiptno > 0)
                                {
                                    string strChitHeadQuery = "INSERT INTO `svcf`.`voucher` (`DualTransactionKey`,`BranchID`,`CurrDate`,`Voucher_No`,`Voucher_Type`,`Head_Id`,`ChoosenDate`,`Narration`,`Amount`,`Series`,`ReceievedBy`,`Trans_Type`,`T_Day`,`T_Month`,`T_Year`,`MemberID`,`Trans_Medium`,`RootID`,`ChitGroupId`,`M_Id`,LoginIP) VALUES (" + DualTransactionKey + "," + balayer.ToobjectstrEvenNull(Session["Branchid"]) + ",'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," + receiptno + ",'C'," + TokenNo + ",'" + dtChoosenDate.ToString("yyyy-MM-dd") + "','" + "Recd From:" + memname +":"+ tokentxt +":"+ rcptno+ " For DrawNo:" + ddltooltip[i] + "'," + txtamnt + ",'" + ddlReceiptSeries.SelectedItem.Text + "','" + ddlEmployee.SelectedItem.Text + "',1," + dtChoosenDate.Day + "," + dtChoosenDate.Month + "," + dtChoosenDate.Year + "," + MemberID + "," + trans_medium + ",5," + GroupID + "," + ddlColloctorName.SelectedItem.Value + ",'" + objSession.LoginIp + "') ";
                                    string strCashHeadQuery = "INSERT INTO `svcf`.`voucher` (`DualTransactionKey`,`BranchID`,`CurrDate`,`Voucher_No`,`Voucher_Type`,`Head_Id`,`ChoosenDate`,`Narration`,`Amount`,`Series`,`ReceievedBy`,`Trans_Type`,`T_Day`,`T_Month`,`T_Year`,`MemberID`,`Trans_Medium`,`RootID`,`ChitGroupId`,`M_Id`,LoginIP) VALUES (" + DualTransactionKey + "," + balayer.ToobjectstrEvenNull(Session["Branchid"]) + ",'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," + receiptno + ",'D'," + CashOrBankID + ",'" + dtChoosenDate.ToString("yyyy-MM-dd") + "','" + "Recd From:" + memname +":" +tokentxt +":"+rcptno +" For DrawNo:" + ddltooltip[i] + "'," + txtamnt + ",'" + ddlReceiptSeries.SelectedItem.Text + "','" + ddlEmployee.SelectedItem.Text + "',1," + dtChoosenDate.Day + "," + dtChoosenDate.Month + "," + dtChoosenDate.Year + "," + MemberID + "," + trans_medium + "," + RootID + "," + GroupID + "," + ddlColloctorName.SelectedItem.Value + ",'" + objSession.LoginIp + "') ";
                                    long strChitHead = trn.insertorupdateTrn(strChitHeadQuery);
                                    long strCashHead = trn.insertorupdateTrn(strCashHeadQuery);
                                    if (CheckBox1.Checked == true)
                                    {
                                        TransactionKeyDue = strCashHead.ToString();
                                    }
                                }
                            }
                            else
                            {
                                TransactionKeyDue = "0";
                            }
                            if (ddlmisc != "")
                            {
                                if (decimal.Parse(txtmisc) != 0.00M)
                                {
                                    if (Convert.ToInt32(mischd2) == 1)
                                    {
                                        if (receiptno > 0)
                                        {
                                            strChitmISCHeadQuery = "INSERT INTO `svcf`.`voucher` (`Other_Trans_Type`,`DualTransactionKey`,`BranchID`,`CurrDate`,`Voucher_No`,`Voucher_Type`,`Head_Id`,`ChoosenDate`,`Narration`,`Amount`,`Series`,`ReceievedBy`,`Trans_Type`,`T_Day`,`T_Month`,`T_Year`,`MemberID`,`Trans_Medium`,`RootID`,`ChitGroupId`,`M_Id`,LoginIP) VALUES (1," + DualTransactionKey + "," + balayer.ToobjectstrEvenNull(Session["Branchid"]) + ",'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," + receiptno + ",'C'," + mischd1 + ",'" + dtChoosenDate.ToString("yyyy-MM-dd") + "','" + "Ref No:" + mischd1 + " |Recd From:" + memname +":"+tokentxt +":"+ receiptno+" For " + ddlmisc + "  Received By: " + balayer.ToobjectstrEvenNull(Session["BranchName"]) + "'," + txtmisc + ",'" + ddlReceiptSeries.SelectedItem.Text + "','" + balayer.ToobjectstrEvenNull(Session["UserName"]) + "',1," + dtChoosenDate.Day + "," + dtChoosenDate.Month + "," + dtChoosenDate.Year + "," + MemberID + "," + trans_medium + ",1," + GroupID + "," + ddlColloctorName.SelectedItem.Value + ",'" + objSession.LoginIp + "') ";

                                            strCashmISCHeadQuery = "INSERT INTO `svcf`.`voucher` (`Other_Trans_Type`,`DualTransactionKey`,`BranchID`,`CurrDate`,`Voucher_No`,`Voucher_Type`,`Head_Id`,`ChoosenDate`,`Narration`,`Amount`,`Series`,`ReceievedBy`,`Trans_Type`,`T_Day`,`T_Month`,`T_Year`,`MemberID`,`Trans_Medium`,`RootID`,`ChitGroupId`,`M_Id`,LoginIP) VALUES (1," + DualTransactionKey + "," + balayer.ToobjectstrEvenNull(Session["Branchid"]) + ",'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," + receiptno + ",'D'," + CashOrBankID + ",'" + dtChoosenDate.ToString("yyyy-MM-dd") + "','" + "Ref No:" + mischd1 + " |Recd From:" + memname +":"+ tokentxt+":"+receiptno +" For " + ddlmisc + "  Received By: " + balayer.ToobjectstrEvenNull(Session["BranchName"]) + "'," + txtmisc + ",'" + ddlReceiptSeries.SelectedItem.Text + "','" + balayer.ToobjectstrEvenNull(Session["UserName"]) + "',1," + dtChoosenDate.Day + "," + dtChoosenDate.Month + "," + dtChoosenDate.Year + "," + MemberID + "," + trans_medium + "," + RootID + "," + GroupID + "," + ddlColloctorName.SelectedItem.Value + ",'" + objSession.LoginIp + "') ";

                                            strChitMiscHead = trn.insertorupdateTrn(strChitmISCHeadQuery);
                                            strCashHeadMisc = trn.insertorupdateTrn(strCashmISCHeadQuery);
                                            if (CheckBox1.Checked == true)
                                            {
                                                if (!ddlCardType.Visible)
                                                {
                                                    strBankMiscInsertQuery = "insert into transbank(BranchID, TransactionKey, DualTransactionKey, T_Day, T_Month, T_Year, BankHeadID, Head_Id, MemberID, CustomersBankName, DateInCheque, ChequeDDNO, GivenAmount, TotalChequeDDAmount, IsBounced, Trans_Type,IsChequeAvailable) values (" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + "," + strCashHeadMisc + "," + DualTransactionKey + "," + dtChoosenDate.Day + "," + dtChoosenDate.Month + "," + dtChoosenDate.Year + "," + ddlBankHead.SelectedValue + "," + mischd1 + "," + MemberID + ",'" + ddlBanklDetails.SelectedItem.Text + "','" + balayer.indiandateToMysqlDate(txtDateinCheque.Text) + "'," + txtCheque.Text + "," + txtmisc + "," + txtTotalAmount.Text + ",0,1," + lbVisible.Visible + ")";
                                                    trn.insertorupdateTrn(strBankMiscInsertQuery);
                                                }
                                                else
                                                {
                                                    strBankMiscInsertQuery = "insert into transbank(BranchID, TransactionKey, DualTransactionKey, T_Day, T_Month, T_Year, BankHeadID, Head_Id, MemberID, CustomersBankName, DateInCheque, ChequeDDNO, GivenAmount, TotalChequeDDAmount, IsBounced, Trans_Type,cardnumber,cardtype,IsChequeAvailable) values (" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + "," + strCashHeadMisc + "," + DualTransactionKey + "," + dtChoosenDate.Day + "," + dtChoosenDate.Month + "," + dtChoosenDate.Year + "," + ddlBankHead.SelectedValue + "," + mischd1 + "," + MemberID + ",'" + ddlBanklDetails.SelectedItem.Text + "','" + balayer.indiandateToMysqlDate(txtDateinCheque.Text) + "'," + txtCheque.Text + "," + txtmisc + "," + txtTotalAmount.Text + ",0,1,'" + balayer.MySQLEscapeString(txtIdcardNumber.Text) + "','" + balayer.MySQLEscapeString(ddlCardType.SelectedItem.Text) + "'," + lbVisible.Visible + ")";
                                                    trn.insertorupdateTrn(strBankMiscInsertQuery);
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (receiptno > 0)
                                        {
                                            strChitMiscHead = 0;
                                            strCashHeadMisc = 0;

                                            strChitMiscHeadQuery = "INSERT INTO `svcf`.`voucher` (`DualTransactionKey`,`BranchID`,`CurrDate`,`Voucher_No`,`Voucher_Type`,`Head_Id`,`ChoosenDate`,`Narration`,`Amount`,`Series`,`ReceievedBy`,`Trans_Type`,`T_Day`,`T_Month`,`T_Year`,`MemberID`,`Trans_Medium`,`RootID`,`ChitGroupId`,`M_Id`,LoginIP) VALUES (" + DualTransactionKey + "," + balayer.ToobjectstrEvenNull(Session["Branchid"]) + ",'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," + receiptno + ",'C'," + mischd1 + ",'" + dtChoosenDate.ToString("yyyy-MM-dd") + "','" + ddlmisc + " Recd from " + memname +":"+ tokentxt +":"+receiptno+ "'," + txtmisc + ",'" + ddlReceiptSeries.SelectedItem.Text + "','" + balayer.ToobjectstrEvenNull(Session["UserName"]) + "',1," + dtChoosenDate.Day + "," + dtChoosenDate.Month + "," + dtChoosenDate.Year + "," + MemberID + "," + trans_medium + "," + mischd2 + "," + GroupID + "," + ddlColloctorName.SelectedItem.Value + ",'" + objSession.LoginIp + "') ";

                                            strCashHeadMiscQuery = "INSERT INTO `svcf`.`voucher` (`DualTransactionKey`,`BranchID`,`CurrDate`,`Voucher_No`,`Voucher_Type`,`Head_Id`,`ChoosenDate`,`Narration`,`Amount`,`Series`,`ReceievedBy`,`Trans_Type`,`T_Day`,`T_Month`,`T_Year`,`MemberID`,`Trans_Medium`,`RootID`,`ChitGroupId`,`M_Id`,LoginIP) VALUES (" + DualTransactionKey + "," + balayer.ToobjectstrEvenNull(Session["Branchid"]) + ",'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," + receiptno + ",'D'," + CashOrBankID + ",'" + dtChoosenDate.ToString("yyyy-MM-dd") + "','" + ddlmisc + " Recd from " + memname + ":" + tokentxt + ":" + receiptno + "'," + txtmisc + ",'" + ddlReceiptSeries.SelectedItem.Text + "','" + balayer.ToobjectstrEvenNull(Session["UserName"]) + "',1," + dtChoosenDate.Day + "," + dtChoosenDate.Month + "," + dtChoosenDate.Year + "," + MemberID + "," + trans_medium + "," + RootID + "," + GroupID + "," + ddlColloctorName.SelectedItem.Value + ",'" + objSession.LoginIp + "') ";
                                            strChitMiscHead = trn.insertorupdateTrn(strChitMiscHeadQuery);
                                            strCashHeadMisc = trn.insertorupdateTrn(strCashHeadMiscQuery);

                                            if (CheckBox1.Checked == true)
                                            {
                                                if (!ddlCardType.Visible)
                                                {
                                                    strBankMiscInsertQuery = "insert into transbank(BranchID, TransactionKey, DualTransactionKey, T_Day, T_Month, T_Year, BankHeadID, Head_Id, MemberID, CustomersBankName, DateInCheque, ChequeDDNO, GivenAmount, TotalChequeDDAmount, IsBounced, Trans_Type,IsChequeAvailable) values (" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + "," + strCashHeadMisc + "," + DualTransactionKey + "," + dtChoosenDate.Day + "," + dtChoosenDate.Month + "," + dtChoosenDate.Year + "," + ddlBankHead.SelectedValue + "," + mischd1 + "," + MemberID + ",'" + ddlBanklDetails.SelectedItem.Text + "','" + balayer.indiandateToMysqlDate(txtDateinCheque.Text) + "'," + txtCheque.Text + "," + txtmisc + "," + txtTotalAmount.Text + ",0,1," + lbVisible.Visible + ")";
                                                    trn.insertorupdateTrn(strBankMiscInsertQuery);
                                                }
                                                else
                                                {
                                                    strBankMiscInsertQuery = "insert into transbank(BranchID, TransactionKey, DualTransactionKey, T_Day, T_Month, T_Year, BankHeadID, Head_Id, MemberID, CustomersBankName, DateInCheque, ChequeDDNO, GivenAmount, TotalChequeDDAmount, IsBounced, Trans_Type,cardnumber,cardtype,IsChequeAvailable) values (" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + "," + strCashHeadMisc + "," + DualTransactionKey + "," + dtChoosenDate.Day + "," + dtChoosenDate.Month + "," + dtChoosenDate.Year + "," + ddlBankHead.SelectedValue + "," + mischd1 + "," + MemberID + ",'" + ddlBanklDetails.SelectedItem.Text + "','" + balayer.indiandateToMysqlDate(txtDateinCheque.Text) + "'," + txtCheque.Text + "," + txtmisc + "," + txtTotalAmount.Text + ",0,1,'" + balayer.MySQLEscapeString(txtIdcardNumber.Text) + "','" + balayer.MySQLEscapeString(ddlCardType.SelectedItem.Text) + "'," + lbVisible.Visible + ")";
                                                    trn.insertorupdateTrn(strBankMiscInsertQuery);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }

                            //OTHER BRANCH
                        else
                        {
                            if (receiptno > 0)
                            {
                                if (decimal.Parse(txtamnt) != 0.00M)
                                {
                                    strChitHeadQuery = "INSERT INTO `svcf`.`voucher` (`Other_Trans_Type`,`DualTransactionKey`,`BranchID`,`CurrDate`,`Voucher_No`,`Voucher_Type`,`Head_Id`,`ChoosenDate`,`Narration`,`Amount`,`Series`,`ReceievedBy`,`Trans_Type`,`T_Day`,`T_Month`,`T_Year`,`MemberID`,`Trans_Medium`,`RootID`,`ChitGroupId`,`M_Id`,LoginIP) VALUES (1," + DualTransactionKey + "," + balayer.ToobjectstrEvenNull(Session["Branchid"]) + ",'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," + receiptno + ",'C'," + ChitsBranchID + ",'" + dtChoosenDate.ToString("yyyy-MM-dd") + "','" + "Ref No:" + TokenNo + " |Recd From:" + memname +":"+ tokentxt +":"+receiptno+  " For DrawNo:" + ddltooltip[i] + " (aprox) Received By: " + balayer.ToobjectstrEvenNull(Session["BranchName"]) + "'," + txtamnt + ",'" + ddlReceiptSeries.SelectedItem.Text + "','" + balayer.ToobjectstrEvenNull(Session["UserName"]) + "',1," + dtChoosenDate.Day + "," + dtChoosenDate.Month + "," + dtChoosenDate.Year + "," + MemberID + "," + trans_medium + ",1," + GroupID + "," + ddlColloctorName.SelectedItem.Value + ",'" + objSession.LoginIp + "')";
                                    strCashHeadQuery = "INSERT INTO `svcf`.`voucher` (`Other_Trans_Type`,`DualTransactionKey`,`BranchID`,`CurrDate`,`Voucher_No`,`Voucher_Type`,`Head_Id`,`ChoosenDate`,`Narration`,`Amount`,`Series`,`ReceievedBy`,`Trans_Type`,`T_Day`,`T_Month`,`T_Year`,`MemberID`,`Trans_Medium`,`RootID`,`ChitGroupId`,`M_Id`,LoginIP) VALUES (1," + DualTransactionKey + "," + balayer.ToobjectstrEvenNull(Session["Branchid"]) + ",'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," + receiptno + ",'D'," + CashOrBankID + ",'" + dtChoosenDate.ToString("yyyy-MM-dd") + "','" + "Ref No:" + TokenNo + " |Recd From" + memname + ":" + tokentxt + ":" + receiptno + " For DrawNo:" + ddltooltip[i] + " (aprox) Received By: " + balayer.ToobjectstrEvenNull(Session["BranchName"]) + "'," + txtamnt + ",'" + ddlReceiptSeries.SelectedItem.Text + "','" + balayer.ToobjectstrEvenNull(Session["UserName"]) + "',1," + dtChoosenDate.Day + "," + dtChoosenDate.Month + "," + dtChoosenDate.Year + "," + MemberID + "," + trans_medium + "," + RootID + "," + GroupID + "," + ddlColloctorName.SelectedItem.Value + ",'" + objSession.LoginIp + "')";
                                    strChitHead = trn.insertorupdateTrn(strChitHeadQuery);
                                    strCashHead = trn.insertorupdateTrn(strCashHeadQuery);
                                }

                                if (CheckBox1.Checked == true)
                                {
                                    TransactionKeyDue = strCashHead.ToString();
                                }
                            }
                            if (ddlmisc != "")
                            {
                                if (decimal.Parse(txtmisc) != 0.00M)
                                {
                                    if (Convert.ToInt32(mischd2) == 1)
                                    {
                                        if (receiptno > 0)
                                        {
                                            string strChitmISCHeadQuery = "INSERT INTO `svcf`.`voucher` (`Other_Trans_Type`,`DualTransactionKey`,`BranchID`,`CurrDate`,`Voucher_No`,`Voucher_Type`,`Head_Id`,`ChoosenDate`,`Narration`,`Amount`,`Series`,`ReceievedBy`,`Trans_Type`,`T_Day`,`T_Month`,`T_Year`,`MemberID`,`Trans_Medium`,`RootID`,`ChitGroupId`,`M_Id`,LoginIP) VALUES (1," + DualTransactionKey + "," + balayer.ToobjectstrEvenNull(Session["Branchid"]) + ",'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," + receiptno + ",'C'," + mischd1 + ",'" + dtChoosenDate.ToString("yyyy-MM-dd") + "','" + "Ref No:" + mischd1 + " |Recd From:" + memname + ":" + tokentxt + ":" + receiptno + " For " + ddlmisc + "  Received By: " + balayer.ToobjectstrEvenNull(Session["BranchName"]) + "'," + txtmisc + ",'" + ddlReceiptSeries.SelectedItem.Text + "','" + balayer.ToobjectstrEvenNull(Session["UserName"]) + "',1," + dtChoosenDate.Day + "," + dtChoosenDate.Month + "," + dtChoosenDate.Year + "," + MemberID + "," + trans_medium + ",1," + GroupID + "," + ddlColloctorName.SelectedItem.Value + ",'" + objSession.LoginIp + "') ";
                                            string strCashmISCHeadQuery = "INSERT INTO `svcf`.`voucher` (`Other_Trans_Type`,`DualTransactionKey`,`BranchID`,`CurrDate`,`Voucher_No`,`Voucher_Type`,`Head_Id`,`ChoosenDate`,`Narration`,`Amount`,`Series`,`ReceievedBy`,`Trans_Type`,`T_Day`,`T_Month`,`T_Year`,`MemberID`,`Trans_Medium`,`RootID`,`ChitGroupId`,`M_Id`,LoginIP) VALUES (1," + DualTransactionKey + "," + balayer.ToobjectstrEvenNull(Session["Branchid"]) + ",'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," + receiptno + ",'D'," + CashOrBankID + ",'" + dtChoosenDate.ToString("yyyy-MM-dd") + "','" + "Ref No:" + mischd1 + " |Recd From:" + memname + ":" + tokentxt + ":" + receiptno + " For " + ddlmisc + "  Received By: " + balayer.ToobjectstrEvenNull(Session["BranchName"]) + "'," + txtmisc + ",'" + ddlReceiptSeries.SelectedItem.Text + "','" + balayer.ToobjectstrEvenNull(Session["UserName"]) + "',1," + dtChoosenDate.Day + "," + dtChoosenDate.Month + "," + dtChoosenDate.Year + "," + MemberID + "," + trans_medium + "," + RootID + "," + GroupID + "," + ddlColloctorName.SelectedItem.Value + ",'" + objSession.LoginIp + "') ";

                                            strChitMiscHead = trn.insertorupdateTrn(strChitmISCHeadQuery);
                                            strCashHeadMisc = trn.insertorupdateTrn(strCashmISCHeadQuery);
                                        }
                                        if (CheckBox1.Checked == true)
                                        {
                                            if (!ddlCardType.Visible)
                                            {
                                                if (receiptno > 0)
                                                {
                                                    strBankMiscInsertQuery = "insert into transbank(BranchID, TransactionKey, DualTransactionKey, T_Day, T_Month, T_Year, BankHeadID, Head_Id, MemberID, CustomersBankName, DateInCheque, ChequeDDNO, GivenAmount, TotalChequeDDAmount, IsBounced, Trans_Type,IsChequeAvailable) values (" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + "," + strCashHeadMisc + "," + DualTransactionKey + "," + dtChoosenDate.Day + "," + dtChoosenDate.Month + "," + dtChoosenDate.Year + "," + ddlBankHead.SelectedValue + "," + mischd1 + "," + MemberID + ",'" + ddlBanklDetails.SelectedItem.Text + "','" + balayer.indiandateToMysqlDate(txtDateinCheque.Text) + "'," + txtCheque.Text + "," + txtmisc + "," + txtTotalAmount.Text + ",0,1," + lbVisible.Visible + ")";
                                                    trn.insertorupdateTrn(strBankMiscInsertQuery);
                                                }
                                            }
                                            else
                                            {
                                                if (receiptno > 0)
                                                {
                                                    strBankMiscInsertQuery = "insert into transbank(BranchID, TransactionKey, DualTransactionKey, T_Day, T_Month, T_Year, BankHeadID, Head_Id, MemberID, CustomersBankName, DateInCheque, ChequeDDNO, GivenAmount, TotalChequeDDAmount, IsBounced, Trans_Type,cardnumber,cardtype,IsChequeAvailable) values (" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + "," + strCashHeadMisc + "," + DualTransactionKey + "," + dtChoosenDate.Day + "," + dtChoosenDate.Month + "," + dtChoosenDate.Year + "," + ddlBankHead.SelectedValue + "," + mischd2 + "," + MemberID + ",'" + ddlBanklDetails.SelectedItem.Text + "','" + balayer.indiandateToMysqlDate(txtDateinCheque.Text) + "'," + txtCheque.Text + "," + txtmisc + "," + txtTotalAmount.Text + ",0,1,'" + balayer.MySQLEscapeString(txtIdcardNumber.Text) + "','" + balayer.MySQLEscapeString(ddlCardType.SelectedItem.Text) + "'," + lbVisible.Visible + ")";
                                                    trn.insertorupdateTrn(strBankMiscInsertQuery);
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (receiptno > 0)
                                        {
                                            strChitMiscHeadQuery = "INSERT INTO `svcf`.`voucher` (`DualTransactionKey`,`BranchID`,`CurrDate`,`Voucher_No`,`Voucher_Type`,`Head_Id`,`ChoosenDate`,`Narration`,`Amount`,`Series`,`ReceievedBy`,`Trans_Type`,`T_Day`,`T_Month`,`T_Year`,`MemberID`,`Trans_Medium`,`RootID`,`ChitGroupId`,`M_Id`,LoginIP) VALUES (" + DualTransactionKey + "," + balayer.ToobjectstrEvenNull(Session["Branchid"]) + ",'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," + receiptno + ",'C'," + mischd1 + ",'" + dtChoosenDate.ToString("yyyy-MM-dd") + "','" + ddlmisc + " Recd from " + memname + ":" + tokentxt + ":" + receiptno + "'," + txtmisc + ",'" + ddlReceiptSeries.SelectedItem.Text + "','" + balayer.ToobjectstrEvenNull(Session["UserName"]) + "',1," + dtChoosenDate.Day + "," + dtChoosenDate.Month + "," + dtChoosenDate.Year + "," + MemberID + "," + trans_medium + "," + mischd2 + "," + GroupID + "," + ddlColloctorName.SelectedItem.Value + ",'" + objSession.LoginIp + "') ";
                                            strCashHeadMiscQuery = "INSERT INTO `svcf`.`voucher` (`DualTransactionKey`,`BranchID`,`CurrDate`,`Voucher_No`,`Voucher_Type`,`Head_Id`,`ChoosenDate`,`Narration`,`Amount`,`Series`,`ReceievedBy`,`Trans_Type`,`T_Day`,`T_Month`,`T_Year`,`MemberID`,`Trans_Medium`,`RootID`,`ChitGroupId`,`M_Id`,LoginIP) VALUES (" + DualTransactionKey + "," + balayer.ToobjectstrEvenNull(Session["Branchid"]) + ",'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," + receiptno + ",'D'," + CashOrBankID + ",'" + dtChoosenDate.ToString("yyyy-MM-dd") + "','" + ddlmisc + " Recd from " + memname + ":" + tokentxt + ":" + receiptno + "'," + txtmisc + ",'" + ddlReceiptSeries.SelectedItem.Text + "','" + balayer.ToobjectstrEvenNull(Session["UserName"]) + "',1," + dtChoosenDate.Day + "," + dtChoosenDate.Month + "," + dtChoosenDate.Year + "," + MemberID + "," + trans_medium + "," + RootID + "," + GroupID + "," + ddlColloctorName.SelectedItem.Value + ",'" + objSession.LoginIp + "') ";
                                            strChitMiscHead = trn.insertorupdateTrn(strChitMiscHeadQuery);
                                            strCashHeadMisc = trn.insertorupdateTrn(strCashHeadMiscQuery);
                                        }
                                        if (CheckBox1.Checked == true)
                                        {
                                            if (!ddlCardType.Visible)
                                            {
                                                if (receiptno > 0)
                                                {
                                                    strBankMiscInsertQuery = "insert into transbank(BranchID, TransactionKey, DualTransactionKey, T_Day, T_Month, T_Year, BankHeadID, Head_Id, MemberID, CustomersBankName, DateInCheque, ChequeDDNO, GivenAmount, TotalChequeDDAmount, IsBounced, Trans_Type,IsChequeAvailable) values (" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + "," + strCashHeadMisc + "," + DualTransactionKey + "," + dtChoosenDate.Day + "," + dtChoosenDate.Month + "," + dtChoosenDate.Year + "," + ddlBankHead.SelectedValue + "," + mischd1 + "," + MemberID + ",'" + ddlBanklDetails.SelectedItem.Text + "','" + balayer.indiandateToMysqlDate(txtDateinCheque.Text) + "'," + txtCheque.Text + "," + txtmisc + "," + txtTotalAmount.Text + ",0,1," + lbVisible.Visible + ")";                                                 
                                                    trn.insertorupdateTrn(strBankMiscInsertQuery);
                                                }
                                            }
                                            else
                                            {
                                                if (receiptno > 0)
                                                {
                                                    strBankMiscInsertQuery = "insert into transbank(BranchID, TransactionKey, DualTransactionKey, T_Day, T_Month, T_Year, BankHeadID, Head_Id, MemberID, CustomersBankName, DateInCheque, ChequeDDNO, GivenAmount, TotalChequeDDAmount, IsBounced, Trans_Type,cardnumber,cardtype,IsChequeAvailable) values (" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + "," + strCashHeadMisc + "," + DualTransactionKey + "," + dtChoosenDate.Day + "," + dtChoosenDate.Month + "," + dtChoosenDate.Year + "," + ddlBankHead.SelectedValue + "," + mischd2 + "," + MemberID + ",'" + ddlBanklDetails.SelectedItem.Text + "','" + balayer.indiandateToMysqlDate(txtDateinCheque.Text) + "'," + txtCheque.Text + "," + txtmisc + "," + txtTotalAmount.Text + ",0,1,'" + balayer.MySQLEscapeString(txtIdcardNumber.Text) + "','" + balayer.MySQLEscapeString(ddlCardType.SelectedItem.Text) + "'," + lbVisible.Visible + ")";
                                                    trn.insertorupdateTrn(strBankMiscInsertQuery);
                                                }
                                            }
                                        }
                                    }

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
                                    strBankInsertQuery = "insert into transbank(BranchID, TransactionKey, DualTransactionKey, T_Day, T_Month, T_Year, BankHeadID, Head_Id, MemberID, CustomersBankName, DateInCheque, ChequeDDNO, GivenAmount, TotalChequeDDAmount, IsBounced, Trans_Type,IsChequeAvailable) values (" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + "," + TransactionKeyDue + "," + DualTransactionKey + "," + dtChoosenDate.Day + "," + dtChoosenDate.Month + "," + dtChoosenDate.Year + "," + ddlBankHead.SelectedValue + "," + TokenNo + "," + MemberID + ",'" + ddlBanklDetails.SelectedItem.Text + "','" + balayer.indiandateToMysqlDate(txtDateinCheque.Text) + "'," + txtCheque.Text + "," + txtamnt + "," + txtTotalAmount.Text + ",0,1," + lbVisible.Visible + ")";
                                    trn.insertorupdateTrn(strBankInsertQuery);
                                }
                                else
                                {
                                    strBankInsertQuery = "insert into transbank(BranchID, TransactionKey, DualTransactionKey, T_Day, T_Month, T_Year, BankHeadID, Head_Id, MemberID, CustomersBankName, DateInCheque, ChequeDDNO, GivenAmount, TotalChequeDDAmount, IsBounced, Trans_Type,cardnumber,cardtype,IsChequeAvailable) values (" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + "," + TransactionKeyDue + "," + DualTransactionKey + "," + dtChoosenDate.Day + "," + dtChoosenDate.Month + "," + dtChoosenDate.Year + "," + ddlBankHead.SelectedValue + "," + TokenNo + "," + MemberID + ",'" + ddlBanklDetails.SelectedItem.Text + "','" + balayer.indiandateToMysqlDate(txtDateinCheque.Text) + "'," + txtCheque.Text + "," + txtamnt + "," + txtTotalAmount.Text + ",0,1,'" + balayer.MySQLEscapeString(txtIdcardNumber.Text) + "','" + balayer.MySQLEscapeString(ddlCardType.SelectedItem.Text) + "'," + lbVisible.Visible + ")";
                                    trn.insertorupdateTrn(strBankInsertQuery);
                                }
                            }
                        }
                       
                    }
                  
                   
                    if (ddlColloctorName.ToolTip.ToString().Trim() != "")
                    {
                        //put  the  query to check all the numbers used or not 
                        trn.insertorupdateTrn("update svcf.assignreceiptbook set IsFinished=1 where receiptseries='" + ddlReceiptSeries.SelectedItem.Text + "' and receiptnoto in(" + ddlColloctorName.ToolTip.ToString().Trim() + ") and BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]));
                    }

                    ViewState["CurrentTable"] = null;
                    GView_Selected.DataSource = (DataTable)ViewState["CurrentTable"];
                    GView_Selected.DataBind();
                    SetInitialRow();

                    //RefNo.Clear();
                   // RefNo1.Clear();
                    trn.CommitTrn();

                    logger.Info("trr.aspx - btnConfirmationYes_Click():  Completed: " + DateTime.Now + " by: " + Convert.ToString(Session["UserName"]) + "");
                    ddlColloctorName.ClearSelection();
                    ddlReceiptSeries.ClearSelection();
                    txtReceiptNo.Text = "";
                    ddlEmployee.ClearSelection();
                    txtTotalAmount.Text = "";
                    txtCheque.Text = "";
                    ddlBanklDetails.ClearSelection();
                    ddlBankHead.ClearSelection();
                    txtDateinCheque.Text = "";
                    txtIdcardNumber.Text = "";
                    ddlReceiptSeries.ClearSelection();

                    Response.Redirect("trr.aspx", false);
                }
                catch (Exception ex)
                {
                    try
                    {
                        trn.RollbackTrn();
                        logger.Info("trr.aspx - btnConfirmationYes_Click():  Error: " + ex.Message + ": " + DateTime.Now + " by: " + Convert.ToString(Session["UserName"]) + "");
                    }
                    catch (Exception error)
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
            catch (Exception) { }

            finally
            {
                RCNumber = 0;
            }
        }
        protected void btnGenerate_Click(object sender, EventArgs e)
        {
            try
            {
                Page.Validate("a");
                Page.Validate("GrpRow");
                Page.Validate("b");
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
                    lblcancelmsg.Text = "";
                    decimal dblTotalAmount = decimal.Parse(txtTotalAmount.Text);
                    decimal dblDueAmount = 0.0M;
                    bool isMiscIssue = false;
                    //long RCNumber = 0;
                    RCNumber = Convert.ToInt32(txtReceiptNo.Text);

                    string selectedRSeries = "";
                    string LoadedSeries = "";
                    string Rcptnumber = "";
                    selectedRSeries = Request.Form[ddlColloctorName.UniqueID];
                    //LoadedSeries = Request.Form[ddlReceiptSeries.UniqueID];
                    LoadedSeries = HD_RSeriesid.Value;

                    LoadedSeries.Trim();
                    PopulateDropDownList(PopulateTRSeries(selectedRSeries), ddlReceiptSeries);
                    ddlReceiptSeries.Items.FindByText(LoadedSeries).Selected = true;
                    //GetRCBookno(LoadedSeries, selectedRSeries);
                   
                    foreach (GridViewRow gvRow in GView_Selected.Rows)
                    {
                        //decimal dblDueTemp = decimal.Parse(((TextBox)gvRow.FindControl("txtAmount")).Text);
                        decimal dblDueTemp = decimal.Parse(gvRow.Cells[4].Text);
                        decimal dblMiscTemp = 0.0M;
                        bool isMisc = decimal.TryParse(gvRow.Cells[6].Text, out dblMiscTemp);
                        //bool isMisc = decimal.TryParse(((TextBox)gvRow.FindControl("txtMisc")).Text, out dblMiscTemp);
                        dblDueAmount += dblDueTemp + dblMiscTemp;
                        string ddlMisc = gvRow.Cells[5].Text;
                        //DropDownList ddlMisc = ((DropDownList)gvRow.FindControl("ddlMisc"));
                        if (isMisc == true & ddlMisc != "")
                        {
                            isMiscIssue = true;
                        }
                        else if (isMisc == false & ddlMisc != "")
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
                    string strReceiptNo = "";
                    DataTable dtAll = balayer.GetDataTable("SELECT  (receiptnoto-total),receiptnoto FROM svcf.assignreceiptbook where  moneycollid=" + ddlColloctorName.SelectedValue + "  and BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and receiptseries='" + ddlReceiptSeries.SelectedItem.Text + "' and IsFinished=0");
                    for (int i = 0; i <= GView_Selected.Rows.Count - 1 && keepGoing; i++)
                    {
                        //TextBox txtReceiptNo = (TextBox)GView_Selected.Rows[i].FindControl("txtReceiptNo");
                        strReceiptNo = GView_Selected.Rows[i].Cells[1].Text;
                        //strReceiptNo = RCNumber.ToString();
                        for (int j = 0; j < dtAll.Rows.Count; j++)
                        {
                            int ReceiptNo = int.Parse(strReceiptNo);
                            int FromRange = int.Parse(dtAll.Rows[j][0].ToString());
                            int toRange = int.Parse(dtAll.Rows[j][1].ToString());
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
                    string memname, memtoken, txtamnt, miscamnt, txtmiscamnt, memtokentxt, mischead;
                    string tonarration = "", grdreceiptno;
                    //03/09/2021
                    string Due = "";
                    Int64 firstValue = 0;
                    Int64 secondValue = 0;
                    string maxDraw = "";
                    //
                    //for (int i = 0; i < GridView1.Rows.Count; i++)
                    ddltooltip = new string[GView_Selected.Rows.Count];
                    for (int i = 0; i <= GView_Selected.Rows.Count - 1; i++)
                    {
                        try
                        {
                            dtConfirmation.Rows.Add();                            
                            grdreceiptno = GView_Selected.Rows[i].Cells[1].Text;
                            //grdreceiptno = RCNumber.ToString();
                            memname = GView_Selected.Rows[i].Cells[2].Text;
                            memtokentxt = GView_Selected.Rows[i].Cells[3].Text;
                            Label tokenid = (Label)GView_Selected.Rows[i].FindControl("lblheadid");
                            memtoken = tokenid.Text;
                            txtamnt = GView_Selected.Rows[i].Cells[4].Text;
                            mischead = GView_Selected.Rows[i].Cells[5].Text;
                            mischead = mischead.Replace("&gt;&gt;", ">>");
                            if (mischead == "--Select--")
                            {
                                mischead = "";
                            }

                            miscamnt = GView_Selected.Rows[i].Cells[6].Text;
                            if (miscamnt == "&nbsp;")
                            {
                                miscamnt = "0.0";
                            }
                            decimal dblMiscTemp = 0.0M;
                            bool isMisc = decimal.TryParse(miscamnt, out dblMiscTemp);
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
                            string GroupID = balayer.GetSingleValue("select GroupID from membertogroupmaster where Head_Id=" + memtoken);
                            dtConfirmation.Rows[i]["Member Name"] = memname;
                            dtConfirmation.Rows[i]["Amount Paying"] = txtamnt;
                            decimal TotalPaidAmount = decimal.Parse(balayer.GetSingleValue("SELECT ifnull( (sum(IF(Voucher_Type = 'C',Amount,0.00)) -sum(IF(Voucher_Type = 'D',Amount,0.00)) ),0.00) AS TransactionAmount FROM `svcf`.`voucher` where  Head_Id=" + memtoken + " and  (Trans_Type<>2) and Other_Trans_Type<>5"));
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
                                TotalPaidAmount = TotalPaidAmount + decimal.Parse(txtamnt);
                                DataTable dtAuction = balayer.GetDataTable("SELECT DrawNO,CurrentDueAmount FROM `svcf`.`auctiondetails` where GroupID=" + GroupID + " and CurrentDueAmount<>'0.00' order by DrawNO");
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
                                }
                                if (ToNarration == "")
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
                                if (FromDraw != ToDraw)
                                {
                                    FromNarration += " To " + ToNarration;
                                }
                                //RowddlToken.ToolTip = FromNarration;
                                //dtConfirmation.Rows[i]["Misc Head"] = RowddlMemberName.ToolTip.ToString();                            
                                ddltooltip[i] = FromNarration;
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
                                        FromNarration = iAuc + 1 + "PP";
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
                                    TotalPaidAmount = TotalPaidAmount + decimal.Parse(txtamnt);
                                    for (int iAuc = 0; iAuc < dtAuction.Rows.Count; iAuc++)
                                    {
                                        decimal currentDueAmount = decimal.Parse(dtAuction.Rows[iAuc]["CurrentDueAmount"].ToString());
                                        TotalPaidAmount = TotalPaidAmount - currentDueAmount;
                                        decimal tempDueAmount = TotalPaidAmount;
                                        if (tempDueAmount == 0.00M)
                                        {
                                            ToNarration = (iAuc + 1).ToString()+"F";
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
                                        ddltooltip[i] = ToNarration;
                                    }
                                }
                                else
                                {
                                    ddltooltip[i] = FromNarration;
                                }
                            }
                            dtConfirmation.Rows[i]["Draw Details"] = ddltooltip[i];
                            if (isMisc == true)
                            {
                                if (!dtConfirmation.Columns.Contains("Misc Head"))
                                {
                                    dtConfirmation.Columns.Add("Misc Head");
                                    dtConfirmation.Columns.Add("Misc Amount");
                                }
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
            catch (Exception) { }
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
                index = Convert.ToInt32(e.RowIndex);
                DataTable dtable = ViewState["CurrentTable"] as DataTable;
                dtable.Rows[index].Delete();
                ViewState["CurrentTable"] = dtable;
                GView_Selected.DataSource = ViewState["CurrentTable"];
                GView_Selected.DataBind();

             //   RefNo.RemoveAt(index);
               // RefNo1.RemoveAt(index);

                if (GView_Selected.Rows.Count == 0)
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

        protected void ImgCancelRcpt_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                TransactionLayer trn = new TransactionLayer();

                string qry = "";
                System.Guid guid = Guid.NewGuid();
                string hexstring = BitConverter.ToString(guid.ToByteArray());
                string guidForBinary16 = "0x" + hexstring.Replace("-", string.Empty);
                string DualTransactionKey = guidForBinary16;

                DateTime dtChoosenDate = DateTime.Parse(txtReceivedDate.Text);
                qry = "INSERT INTO `svcf`.`voucher` (`DualTransactionKey`,`BranchID`,`CurrDate`,`Voucher_No`,`Voucher_Type`,`Head_Id`,`ChoosenDate`,`Narration`," +
                    "`Amount`,`Series`,`ReceievedBy`,`Trans_Type`,`T_Day`,`T_Month`,`T_Year`,`MemberID`,`Trans_Medium`,`RootID`,`ChitGroupId`) VALUES (" + DualTransactionKey + "," +
                    "" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + ",'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," + txtReceiptNo.Text + "," +
                    "'C',0,'" + dtChoosenDate.ToString("yyyy-MM-dd") + "','" + "Cancelled Receipt',0,'---'," +
                    "'---',1," + dtChoosenDate.Day + "," + dtChoosenDate.Month + "," + dtChoosenDate.Year + ",0," +
                    " 0,0,0)";

                long strChitHead = trn.insertorupdateTrn(qry);

                lblcancelmsg.Text = "Selected Receipt Deleted successfully!!!";

                //    trn.CommitTrn();

            }
            catch (Exception) { }
        }

        protected void Chksamercno_CheckedChanged(object sender, EventArgs e)
        {
            if (Chksamercno.Checked == true)
            {
                Hd_SameRCNo.Value = "";
                Hd_SameRCNo.Value = txtReceiptNo.Text;
            }
            else
            {
                Hd_SameRCNo.Value = "";
            }
        }
    }
}
