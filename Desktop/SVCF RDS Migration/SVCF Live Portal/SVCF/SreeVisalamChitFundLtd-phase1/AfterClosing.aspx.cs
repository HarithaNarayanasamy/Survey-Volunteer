using log4net;
using MySql.Data.MySqlClient;
using SVCF_BusinessAccessLayer;
using SVCF_TransactionLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SreeVisalamChitFundLtd_phase1
{
    public partial class AfterClosing : System.Web.UI.Page
    {
        #region VarDeclaration
        int rootid;
        double credittotal = 0;
        double existing_crdtotal = 0;
        double hiddenamnt = 0;
        #endregion
        #region VarDeclaration
        BusinessLayer balayer = new BusinessLayer();
        string clientcrheads = "";
        string clientdbheads = "";
        string query = "";
        string userinfo = "";
        string qry = "";
        string usrRole = "";
        string memid = "";
        #endregion
        ILog logger = log4net.LogManager.GetLogger(typeof(VoucherMultiple_New));
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    userinfo = HttpContext.Current.User.Identity.Name;
                    qry = "select rs.name from roles as rs inner join rights as rt on (rt.roleid=rs.id) where memberid=" + userinfo + "";
                    usrRole = balayer.GetSingleValue(qry);
                    if (usrRole == "Report")
                    {
                        Response.Redirect(Page.ResolveUrl("~/Home.aspx"), true);
                    }
                    //else if (Convert.ToInt32(Session["Branchid"]) != 161)
                    //{
                    //    Response.Redirect(Page.ResolveUrl("~/Home.aspx"), true);
                    //}
                    bindHeads(ddlHeads);

                    Session["CheckRefresh"] = System.Guid.NewGuid().ToString();
                    txtReceivedBy.Text = balayer.ToobjectstrEvenNull(Session["UserName"]);
                    ViewState["tabnum"] = "57";
                    txtSeries.Text = "AfterClosing";
                    txtDate.Focus();
                    txtDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    txtVoucherNo.Text = balayer.GetSingleValue("SELECT ifnull(max(cast(Voucher_No as unsigned)),0)+1 FROM svcf.voucher where Series='AfterClosing' and Trans_Type='0' and BranchID=" + Session["Branchid"]);
                    string Choosendate = balayer.GetSingleValue("SELECT DATE_FORMAT(max(ChoosenDate),'%d/%m/%Y') from `svcf`.`voucher`where BranchID=" + Session["Branchid"] + " and ChoosenDate<>'0000-00-00'");
                    //string Choosendate = balayer.GetSingleValue("SELECT DATE_FORMAT( MinimumDate, '%d/%m/%Y') MinimumDate FROM svcf.restrictionmaster where BranchID=" + Session["Branchid"]);
                    txtDate.Text = Choosendate;
                    ViewState["CurrentTable"] = null;
                    ViewState["CurrentTableDB"] = null;

                    SetInitRow_Clnt();
                    SetInitDb_Clnt();
                }
                rvDate.MaximumValue = DateTime.Now.ToString("dd/MM/yyyy");

                rvDate.MinimumValue = balayer.GetSingleValue("SELECT DATE_FORMAT( MinimumDate, '%d/%m/%Y') MinimumDate FROM svcf.restrictionmaster where BranchID=" + Session["Branchid"]);
            }
            catch (Exception)
            {
                throw new InvalidOperationException("An InvalidOperationException " +
     "occurred in the Page_Load handler on the Default.aspx page.");
            }
            logger.Info("Voucher Multiple: " + DateTime.Now + " by: " + Convert.ToString(Session["UserName"]) + "");
        }
        private void SetInitDb_Clnt()
        {
            DataTable dt = new DataTable();
            DataRow dr = null;
            //Define the Columns           
            dt.Columns.Add(new DataColumn("DbHeads", typeof(string)));
            dt.Columns.Add(new DataColumn("DbAmount", typeof(string)));
            dt.Columns.Add(new DataColumn("DbDescription", typeof(string)));
            dt.Columns.Add(new DataColumn("Dbheadid", typeof(string)));
            dt.Columns.Add(new DataColumn("RowNumber", typeof(int)));
            //Add a Dummy Data on Initial Load
            dr = dt.NewRow();
            dt.Rows.Add(dr);
            ViewState["CurrentTableDB"] = dt;

            if (dt.Rows.Count == 0)
            {
                //If no records then add a dummy row.
                dt.Rows.Add();
            }
            GrdDbClnt.DataSource = dt;
            GrdDbClnt.DataBind();
            dt.Dispose();
        }
        private void SetInitRow_Clnt()
        {
            DataTable dt = new DataTable();
            DataRow dr = null;
            //Define the Columns           
            dt.Columns.Add(new DataColumn("Heads", typeof(string)));
            dt.Columns.Add(new DataColumn("Amount", typeof(string)));
            dt.Columns.Add(new DataColumn("Description", typeof(string)));
            dt.Columns.Add(new DataColumn("chequeNO", typeof(string)));
            dt.Columns.Add(new DataColumn("headid", typeof(string)));
            dt.Columns.Add(new DataColumn("RowNumber", typeof(int)));
            //Add a Dummy Data on Initial Load
            dr = dt.NewRow();
            dt.Rows.Add(dr);
            ViewState["CurrentTable"] = dt;

            if (dt.Rows.Count == 0)
            {
                //If no records then add a dummy row.
                dt.Rows.Add();
            }
            GridCr.DataSource = dt;
            GridCr.DataBind();
            //  dt.Dispose();
        }
        protected void bindHeads(DropDownList ddlHeads)
        {

            DataTable dtAllHeads = balayer.RetrieveVHeads(Convert.ToInt32(Session["Branchid"]), "VoucherHeads");
            DataTable filterdtheads = new DataTable();


            ////bind debit heads
            DataRow drow = dtAllHeads.NewRow();
            drow[0] = "select";
            drow[1] = "select";
            dtAllHeads.Rows.InsertAt(drow, 0);
            ddlHeadsDebit.DataSource = dtAllHeads;
            ddlHeadsDebit.DataTextField = "TREE";
            ddlHeadsDebit.DataValueField = "ID";
            ddlHeadsDebit.DataBind();



            ////bind credit heads
            DataTable dtChit1 = balayer.GetDataTable("SELECT concat(g1.GROUPNO,'|',g1.GROUPNO) as TREE,concat(cast(g1.Head_Id as char),'|',cast(g1.Head_Id as char),'|',cast(g1.Head_Id as char)) as ID FROM groupmaster as g1 where g1.BranchID=" + Session["Branchid"]);
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
        protected void btnNo_Click(object sender, EventArgs e)
        {
            action();
        }
        public void action()
        {
            if (lblHint.Text == "Reload")
            {
                lblHint.Text = "";
                this.Response.Redirect(this.Request.Url.AbsoluteUri, false);
                //this.Response.Redirect(this.Request.Url.AbsolutePath.ToString());
            }
            else
                if (lblHint.Text == "VExist")
            {
                //Added by Gayathri
                if (txtVoucherNo.Text != "")
                    balayer.ExecuteQuery("Delete From `svcf`.`voucher` Where Series = 'ZZZ' And Voucher_No = " + txtVoucherNo.Text + "");
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
            this.Response.Redirect(this.Request.Url.AbsoluteUri, false);
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
        protected void btnYes_Click(object sender, EventArgs e)
        {
            decimal totalleft = 0.0M;
            decimal totalright = 0.0M;
            List<string> DbAmount = new List<string>();
            List<string> amntlist = new List<string>();
            List<string> DbHeads = new List<string>();
            List<string> hdslist = new List<string>();

            foreach (GridViewRow gvRow in GrdDbClnt.Rows)
            {
                DbHeads.Add(gvRow.Cells[1].Text);
                DbAmount.Add(gvRow.Cells[2].Text);
            }

            foreach (GridViewRow gvRow in GridCr.Rows)
            {
                hdslist.Add(gvRow.Cells[1].Text);
                amntlist.Add(gvRow.Cells[2].Text);
            }


            for (int rcnt = 0; rcnt < hdslist.Count; rcnt++)
            {
                string txtcrsubAmount = amntlist[rcnt];
                totalleft += decimal.Parse(txtcrsubAmount.Trim());
            }

            for (int iRow = 0; iRow < DbHeads.Count; iRow++)
            {
                string txtsubAmount = DbAmount[iRow];
                totalright += decimal.Parse(txtsubAmount.Trim());
            }
            if (totalleft == totalright)
            {
                try
                {

                    //  lblcancelmsg.Text = "";
                    //TransactionLayer trn = new TransactionLayer();
                    TransactionLayer trn = new TransactionLayer();
                    string hexstring;
                    string guidForBinary16;
                    string DualTransactionKey;
                    string TransactionKey;
                    bool isFailed, isDate;
                    DateTime dtChoosenDate;
                    string NodeID;
                    string chitGroupId;
                    string strCreditHeadQuery = "";
                    long iResult;
                    string strBankInsertQuery;

                    #region Vardeclaration
                    string CRtxtsubamnt;
                    string CRtxtdesc;
                    string CRtxtcheqno;
                    string insert1 = "";
                    string CredDescription = "";
                    string DebDescription = "";
                    #endregion

                    if (lblHint.Text == "Reload")
                    {
                        lblHint.Text = "";
                        this.Response.Redirect(this.Request.Url.AbsolutePath.ToString(), false);
                    }
                    else
                        if (lblHint.Text == "VExist")
                    {
                        if (txtVoucherNo.Text != "")
                            balayer.ExecuteQuery("Delete From `svcf`.`voucher` Where Series = 'ZZZ' And Voucher_No = " + txtVoucherNo.Text);
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
                        hexstring = BitConverter.ToString(guid.ToByteArray());
                        guidForBinary16 = "0x" + hexstring.Replace("-", string.Empty);
                        DualTransactionKey = guidForBinary16;
                        // GetSeriesAndVoucherNo();

                        isFailed = false;
                        dtChoosenDate = DateTime.Parse(txtDate.Text);
                        #region VarDeclaration
                        string[] ChitGpNo;
                        string[] ChitGpId;
                        int KsrHeadID = 0, KsrNoOfMem = 0;
                        string qry = "";
                        MySqlDataReader dr;
                        List<string> GetMemList = new List<string>();
                        double kasaramnt = 0;
                        DateTime Ksrchoosendt;
                        string ddlSubsHeads = "";
                        string ddlheads = "";
                        string Tot_KsrAmnt = "";

                        string subheadval = "";
                        string ddlSubsHeadsisub = "";
                        string ddlsubsheadvalue = "";
                        string CR_HSelval = "";
                        string chit_gpid = "";
                        string strDebitHeadQuery = "";
                        bool getvc = false;
                        DataTable getcredit = new DataTable();
                        DataTable getdebit = new DataTable();
                        DataTable CRDT = new DataTable();
                        DataTable DBDT = new DataTable();
                        #endregion

                        try
                        {
                            ClsSession objSession = (ClsSession)Session["objSession"];
                            DateTime d;
                            isDate = DateTime.TryParseExact(txtDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out d);
                            string trans_medium = "";
                            string memberid = "";

                            //     CRDT = (DataTable)ViewState["CurrentTable"];
                            // CRDT = GridCr;
                            //   DBDT = (DataTable)ViewState["CurrentTableDB"];

                            qry = "select * from  `svcf`.`voucher` where Voucher_No=" + txtVoucherNo.Text + " and BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and Trans_Type=0 and Series='AfterClosing'";
                            getdebit = balayer.GetDataTable(qry);
                            if (getdebit.Rows.Count == 0)
                            {

                                #region Voucherinsert

                                #region GeTRootid

                                foreach (GridViewRow gvRow in GridCr.Rows)
                                {
                                    // ddlSubsHeads = CRDT.Rows[iTrans]["Heads"].ToString();    //GViewCR_Selected.Rows[iTrans].Cells[1].Text;
                                    ddlSubsHeads = gvRow.Cells[1].Text;
                                    subheadval = gvRow.Cells[5].Text;
                                    ddlSubsHeads = ddlSubsHeads.Replace("&gt;&gt;", ">>");
                                    if (ddlSubsHeads == "--Select--")
                                    {
                                        ddlSubsHeads = "";
                                    }
                                    if (subheadval.Contains("|"))
                                    {
                                        rootid = 5;
                                        trans_medium = "0";
                                        break;
                                    }
                                    else
                                    {

                                        rootid = int.Parse(subheadval.Split(':')[0].ToString().Trim().Trim(':').Trim());
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
                                            foreach (GridViewRow gvRow1 in GrdDbClnt.Rows)
                                            {
                                                ddlSubsHeadsisub = gvRow1.Cells[1].Text; //GViewDB_Selected.Rows[isub].Cells[1].Text;
                                                ddlSubsHeadsisub = ddlSubsHeadsisub.Replace("&gt;&gt;", ">>");
                                                ddlsubsheadvalue = gvRow1.Cells[4].Text;    //Dbheadid[isub];
                                                int subrootid = int.Parse(ddlsubsheadvalue.Split(':')[0].ToString().Trim().Trim(':').Trim());
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
                                            foreach (GridViewRow gvRow1 in GrdDbClnt.Rows)
                                            {
                                                ddlSubsHeadsisub = gvRow1.Cells[1].Text; //GViewDB_Selected.Rows[isub].Cells[1].Text;
                                                ddlSubsHeadsisub = ddlSubsHeadsisub.Replace("&gt;&gt;", ">>");
                                                ddlsubsheadvalue = gvRow1.Cells[4].Text;

                                                int subrootid = int.Parse(ddlsubsheadvalue.Split(':')[0].ToString().Trim().Trim(':').Trim());
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
                                            // }
                                        }
                                    }
                                }
                                // }

                                if (trans_medium == "")
                                {
                                    foreach (GridViewRow gvRow1 in GrdDbClnt.Rows)
                                    {

                                        ddlSubsHeadsisub = gvRow1.Cells[1].Text;
                                        ddlSubsHeadsisub = ddlSubsHeadsisub.Replace("&gt;&gt;", ">>");
                                        ddlsubsheadvalue = gvRow1.Cells[4].Text;

                                        int rootid = int.Parse(ddlsubsheadvalue.Split(':')[0].ToString().Trim().Trim(':').Trim());
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
                                    // }
                                }
                                if (trans_medium == "")
                                {
                                    trans_medium = "3";
                                }

                                #endregion

                                strCreditHeadQuery = "";
                                strBankInsertQuery = "";

                                #region CreidtInsert


                                foreach (GridViewRow gvRow in GridCr.Rows)
                                {

                                    ddlheads = gvRow.Cells[1].Text;
                                    ddlheads = ddlheads.Replace("&gt;&gt;", ">>");
                                    CredDescription = gvRow.Cells[3].Text;
                                    CR_HSelval = gvRow.Cells[5].Text;
                                    //var CR_HSelval1 = gvRow.Cells[5];

                                    if (!ddlheads.Contains("|"))
                                    {
                                        int rootid = int.Parse(CR_HSelval.Split(':')[0].ToString().Trim().Trim(':').Trim());
                                        if (rootid == 5)
                                        {
                                            memberid = balayer.GetSingleValue("select `MemberID` from membertogroupmaster where `Head_Id`=" + CR_HSelval.Split(':')[1].ToString().Trim().Trim(':').Trim());
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

                                        CRtxtsubamnt = gvRow.Cells[2].Text;
                                        NodeID = CR_HSelval.Split(':')[1].Trim();
                                        string txtChk = gvRow.Cells[4].Text;
                                        if (txtChk == "&nbsp;")
                                        {
                                            txtChk = "";
                                        }
                                        chitGroupId = "0";
                                        if (int.Parse(CR_HSelval.Split(':')[0].ToString().Trim().Trim(':').Trim()) == 5)
                                        {
                                            chitGroupId = balayer.GetSingleValue("select ParentID from headstree where NodeID=" + CR_HSelval.Split(':')[1].ToString());
                                        }

                                        strCreditHeadQuery = "INSERT INTO `svcf`.`voucher` (`DualTransactionKey`,`BranchID`,`CurrDate`,`Voucher_No`,`Voucher_Type`,`Head_Id`,`ChoosenDate`,`Narration`,`Amount`,`Series`,`ReceievedBy`,`Trans_Type`,`T_Day`,`T_Month`,`T_Year`,`MemberID`,`Trans_Medium`,`RootID`,`ChitGroupId`,LoginIP) VALUES (" + DualTransactionKey + "," + balayer.ToobjectstrEvenNull(Session["Branchid"]) + ",'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," + txtVoucherNo.Text + ",'C'," + CR_HSelval.Split(':')[1].ToString().Trim() + ",'" + dtChoosenDate.ToString("yyyy-MM-dd") + "','" + balayer.ReplaceJnk(CredDescription) + "'," + CRtxtsubamnt + ",'" + txtSeries.Text.ToString().Trim() + "','" + balayer.ReplaceJnk(txtReceivedBy.Text) + "',0," + dtChoosenDate.Day + "," + dtChoosenDate.Month + "," + dtChoosenDate.Year + "," + memberid + "," + trans_medium + "," + CR_HSelval.Split(':')[0] + "," + chitGroupId + ",'" + objSession.LoginIp + "'); ";
                                        iResult = trn.insertorupdateTrn(strCreditHeadQuery);
                                        if (txtChk.Trim() != "")
                                        {

                                            strBankInsertQuery = "insert into transbank(BranchID, TransactionKey, DualTransactionKey, T_Day, T_Month, T_Year, BankHeadID, Head_Id, MemberID, CustomersBankName, DateInCheque, ChequeDDNO, GivenAmount, TotalChequeDDAmount, IsBounced, Trans_Type) values (" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + "," + balayer.ToobjectstrEvenNull((object)iResult) + "," + DualTransactionKey + "," + dtChoosenDate.Day + "," + dtChoosenDate.Month + "," + dtChoosenDate.Year + "," + CR_HSelval.Split(':')[1].ToString().Trim() + ",000,000,'','" + balayer.indiandateToMysqlDate(txtDate.Text) + "'," + txtChk + "," + CRtxtsubamnt + "," + CRtxtsubamnt + ",0,0);";

                                            trn.insertorupdateTrn(strBankInsertQuery);
                                        }
                                    }
                                    else
                                    {
                                        ddlheads = gvRow.Cells[1].Text;    //GViewCR_Selected.Rows[iTrans].Cells[1].Text;
                                        ChitGpNo = ddlheads.Split('|');
                                        if (ChitGpNo[0] == ChitGpNo[1])
                                        {
                                            try
                                            {
                                                //Kasar Entry
                                                Ksrchoosendt = Convert.ToDateTime(txtDate.Text);
                                                //DropDownList ddlSHeads = (DropDownList)GridGuardians.Rows[iTrans].FindControl("ddlHeads");
                                                Tot_KsrAmnt = gvRow.Cells[2].Text;    //amntlist[iTrans];    //GViewCR_Selected.Rows[iTrans].Cells[2].Text;
                                                                                      //TextBox Tot_KsrAmnt = (TextBox)GridGuardians.Rows[iTrans].FindControl("txtAmount");
                                                                                      //ChitGpId = ddlSHeads.SelectedValue.ToString().Split('|');
                                                chit_gpid = gvRow.Cells[5].Text;
                                                //Label chit_gpid = (Label)GViewCR_Selected.Rows[iTrans].FindControl("lblhdid");
                                                ChitGpId = chit_gpid.Split('|');
                                                qry = "SELECT Head_Id,NoofMembers FROM svcf.groupmaster where Head_Id=" + ChitGpId[0] + "";
                                                dr = balayer.ExecuteReader(qry);
                                                qry = "";
                                                while (dr.Read())
                                                {
                                                    KsrHeadID = Convert.ToInt32(dr[0]);
                                                    KsrNoOfMem = Convert.ToInt32(dr[1]);
                                                }
                                                dr.Dispose();

                                                qry = "SELECT Head_Id FROM svcf.membertogroupmaster where GroupID=" + KsrHeadID + "";
                                                GetMemList = balayer.RetrveList(qry);
                                                kasaramnt = Convert.ToDouble(Tot_KsrAmnt) / KsrNoOfMem;
                                                //Insert Kasary amount
                                                for (int li = 0; li <= GetMemList.Count - 1; li++)
                                                {
                                                    getvc = balayer.CheckVoucher_Exist(Convert.ToInt32(txtVoucherNo.Text), Convert.ToInt32(Session["Branchid"]));
                                                    if (getvc == false)
                                                    {
                                                        qry = "insert into voucher(DualTransactionKey, BranchID, `CurrDate`, Voucher_No, Voucher_Type, Head_Id, ChoosenDate," +
                                                        "Narration, Amount, Series, ReceievedBy, Trans_Type, T_Day, T_Month, T_Year, MemberID, Trans_Medium, RootID, ChitGroupId, " +
                                                        "Other_Trans_Type,LoginIP) values('" + DualTransactionKey + "'," + Convert.ToInt32(Session["Branchid"]) + ",'" + balayer.GetChangeDatFormat(d.Date, 2) + "'," + Convert.ToInt32(txtVoucherNo.Text) + "," +
                                                        "'C'," + Convert.ToInt32(GetMemList[li]) + ",'" + balayer.GetChangeDatFormat(Ksrchoosendt, 2) + "','" + ChitGpNo[0] + ":Redraw Kasar Difference'," +
                                                        "" + kasaramnt + ",'AfterClosing','admin','1'," + Ksrchoosendt.Date.Day + "," + Ksrchoosendt.Date.Month + "," + Ksrchoosendt.Date.Year + ",0," +
                                                        "0,5," + KsrHeadID + ",5,'" + objSession.LoginIp + "')";
                                                        balayer.ExecuteQuery(qry);
                                                    }
                                                }
                                            }
                                            catch (Exception) { }
                                        }
                                        // }
                                    }
                                }
                                #endregion
                                strDebitHeadQuery = "";
                                strBankInsertQuery = "";

                                #region Debit insert
                                foreach (GridViewRow gvRow1 in GrdDbClnt.Rows)
                                {

                                    ddlheads = gvRow1.Cells[1].Text;
                                    ddlheads = ddlheads.Replace("&gt;&gt;", ">>");
                                    ddlsubsheadvalue = gvRow1.Cells[4].Text;

                                    DebDescription = gvRow1.Cells[3].Text;
                                    int rootid = int.Parse(ddlsubsheadvalue.Split(':')[0].ToString().Trim().Trim(':').Trim());
                                    if (rootid == 5)
                                    {
                                        memberid = balayer.GetSingleValue("select `MemberID` from membertogroupmaster where `Head_Id`=" + ddlsubsheadvalue.Split(':')[1].ToString().Trim().Trim(':').Trim());
                                    }
                                    else
                                    {
                                        memberid = "0";
                                    }
                                    if (memberid == "")
                                    {
                                        memberid = "0";
                                    }

                                    string db_txtSubAmount = gvRow1.Cells[2].Text;
                                    string db_txtDescription = gvRow1.Cells[3].Text;
                                    NodeID = ddlsubsheadvalue.Split(':')[1].Trim();
                                    chitGroupId = "0";
                                    if (int.Parse(ddlsubsheadvalue.Split(':')[0].ToString().Trim().Trim(':').Trim()) == 5)
                                    {
                                        chitGroupId = balayer.GetSingleValue("select ParentID from headstree where NodeID=" + ddlsubsheadvalue.Split(':')[1].ToString());
                                    }
                                    if (chitGroupId == "")
                                    {
                                        chitGroupId = "0";
                                    }

                                    strDebitHeadQuery = "INSERT INTO `svcf`.`voucher` (`DualTransactionKey`,`BranchID`,`CurrDate`,`Voucher_No`,`Voucher_Type`,`Head_Id`,`ChoosenDate`,`Narration`,`Amount`,`Series`,`ReceievedBy`,`Trans_Type`,`T_Day`,`T_Month`,`T_Year`,`MemberID`,`Trans_Medium`,`RootID`,`ChitGroupId`,LoginIP) VALUES (" + DualTransactionKey + "," + balayer.ToobjectstrEvenNull(Session["Branchid"]) + ",'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," + txtVoucherNo.Text + ",'D'," + ddlsubsheadvalue.Split(':')[1].ToString().Trim() + ",'" + dtChoosenDate.ToString("yyyy-MM-dd") + "','" + balayer.ReplaceJnk(DebDescription) + "'," + db_txtSubAmount + ",'" + txtSeries.Text.ToString().Trim() + "','" + balayer.ReplaceJnk(txtReceivedBy.Text) + "',0," + dtChoosenDate.Day + "," + dtChoosenDate.Month + "," + dtChoosenDate.Year + "," + memberid + "," + trans_medium + "," + ddlsubsheadvalue.Split(':')[0] + "," + chitGroupId + ",'" + objSession.LoginIp + "') ;";
                                    iResult = trn.insertorupdateTrn(strDebitHeadQuery);
                                    if (ddlsubsheadvalue.Contains("3:"))
                                    {
                                        strBankInsertQuery = "insert into transbank(BranchID, TransactionKey, DualTransactionKey, T_Day, T_Month, T_Year, BankHeadID, Head_Id, MemberID, CustomersBankName, DateInCheque, ChequeDDNO, GivenAmount, TotalChequeDDAmount, IsBounced, Trans_Type) values (" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + "," + balayer.ToobjectstrEvenNull((object)iResult) + "," + DualTransactionKey + "," + dtChoosenDate.Day + "," + dtChoosenDate.Month + "," + dtChoosenDate.Year + "," + ddlsubsheadvalue.Split(':')[1].ToString().Trim() + ",000,000,'','" + balayer.indiandateToMysqlDate(txtDate.Text) + "'," + "000" + "," + db_txtSubAmount + "," + db_txtSubAmount + ",0,0);";
                                        trn.insertorupdateTrn(strBankInsertQuery);
                                    }
                                }
                                #endregion
                                #endregion
                                trn.CommitTrn();

                                logger.Info("VoucherMultiple.aspx - btnYes_Click():  Completed: " + DateTime.Now + " by: " + Convert.ToString(Session["UserName"]) + "");

                            }


                            //      CRDT.Dispose();
                            //   DBDT.Dispose();
                            strDebitHeadQuery = "";
                            strBankInsertQuery = "";
                            ViewState["CurrentTable"] = null;
                            ViewState["CurrentTableDB"] = null;
                            ddlHeads.ClearSelection();
                            ddlHeadsDebit.ClearSelection();
                            // Request.Form.Clear();
                            balayer.ExecuteQuery("Delete From `svcf`.`voucher` Where Series = 'ZZZ' And Voucher_No = '" + txtVoucherNo.Text + "'");

                            logger.Info("VoucherMultiple.aspx - btnYes_Click():  Completed: " + DateTime.Now + " by: " + Convert.ToString(Session["UserName"]) + "");
                            txtVoucherNo.Text = balayer.GetSingleValue("SELECT ifnull(max(cast(Voucher_No as unsigned)),0)+1 FROM svcf.voucher where Series='AfterClosing' and Trans_Type='0' and BranchID=" + Session["Branchid"]);

                        }
                        catch (Exception ex)
                        {
                            try
                            {
                                trn.RollbackTrn();
                                //LogCls.LogError(ex, "Error in Voucher Multiple: btnYes_Click");
                                logger.Info("VoucherMultiple.aspx - btnYes_Click():  Error: " + ex.Message + ":  " + DateTime.Now + " by: " + Convert.ToString(Session["UserName"]) + "");

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
                                ViewState["CurrentTable"] = null;
                                ViewState["CurrentTableDB"] = null;

                            }
                        }
                        finally
                        {
                            trn.DisposeTrn();
                            if (!isFailed)
                            {
                                ViewState["CurrentTable"] = null;
                                ViewState["CurrentTableDB"] = null;
                                SetInitRow_Clnt();
                                SetInitDb_Clnt();
                                //commented by gayathri GetSeriesAndVoucherNo();
                            }
                        }
                        gvoldmember.DataSource = null;
                        gvoldmember.DataBind();

                        //Response.Redirect(Request.Url.AbsoluteUri);

                    }
                    else
                                    if (lblHint.Text == "ichk")
                    {
                        lblHint.Text = "";
                        if (DynamicControlsHolder.Controls.Count > 0)
                        {
                            DynamicControlsHolder.Controls.RemoveAt(0);
                        }
                        ViewState["CurrentTable"] = null;
                        ViewState["CurrentTableDB"] = null;
                        //Request.Form.Clear();
                        // Response.Redirect(Request.Url.AbsoluteUri);                                    
                    }
                    else
                    {
                    }
                }
                catch (Exception) { }
            }
            else
            {
                action();
            }
        }
        protected void btnGenerate_Click(object sender, EventArgs e)
        {
            decimal totalleft = 0.0M;
            decimal totalright = 0.0M;
            List<string> DbAmount = new List<string>();
            List<string> amntlist = new List<string>();
            List<string> DbHeads = new List<string>();
            List<string> hdslist = new List<string>();

            foreach (GridViewRow gvRow in GrdDbClnt.Rows)
            {
                DbHeads.Add(gvRow.Cells[1].Text);
                DbAmount.Add(gvRow.Cells[2].Text);
            }

            foreach (GridViewRow gvRow in GridCr.Rows)
            {
                hdslist.Add(gvRow.Cells[1].Text);
                amntlist.Add(gvRow.Cells[2].Text);
            }


            for (int rcnt = 0; rcnt < hdslist.Count; rcnt++)
            {
                string txtcrsubAmount = amntlist[rcnt];
                totalleft += decimal.Parse(txtcrsubAmount.Trim());
            }

            for (int iRow = 0; iRow < DbHeads.Count; iRow++)
            {
                string txtsubAmount = DbAmount[iRow];
                totalright += decimal.Parse(txtsubAmount.Trim());
            }
            if (totalleft == totalright)
            {
                Generate();
            }
            else
            {
                //   error = error.Trim().Trim('+');
                lblHeading.Text = "Warning!!!";
                lblContent.Text = "Please Tally Credit and Debit Amount!!!"; //+ "</br></br>" + error.Trim().Trim('+') + " !=  " + errorDebit.Trim().Trim('+');
                MpAll.PopupControlID = "pnlpopup";
                //      gvoldmember.DataSource = dtConfirmation;
                gvoldmember.DataBind();
                MpAll.PopupControlID = "pnlpopup";
                MpAll.Show();
                btnYes.Focus();
                pnlpopup.Visible = true;
                //   dtConfirmation.Dispose();
            }

        }
        public void Generate()
        {
            try
            {
                //  lblcancelmsg.Text = "";
                Page.Validate("Generate");
                Page.Validate("GrpRow");
                Page.Validate("GrpRowDebit");
                #region VarDeclaration
                string txtcrChk = "";
                string txtcrsubAmount = "";
                string subheadval = "";
                string bnkhead = "";
                List<string> hdslist = new List<string>();
                List<string> amntlist = new List<string>();
                List<string> memlist = new List<string>();
                List<string> cheqlist = new List<string>();
                List<string> hdidlist = new List<string>();
                List<string> DbHeads = new List<string>();
                List<string> DbAmount = new List<string>();
                List<string> DbDescription = new List<string>();
                List<string> Dbheadid = new List<string>();
                #endregion
                if (!Page.IsValid)
                {
                    return;
                }
                //if (Session["CheckRefresh"].ToString() != ViewState["CheckRefresh"].ToString())   19/07/2016
                //{
                //    return;
                //}
                //commented by gayathri GetSeriesAndVoucherNo();

                string ResExist = balayer.GetSingleValue("select count(*) from voucher where ChoosenDate>='2014/04/01' and Series='" + txtSeries.Text.ToUpper().Trim() + "' and  Voucher_No=" + txtVoucherNo.Text.Trim() + " and Trans_Type='0' and BranchID=" + Session["Branchid"]);
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

                foreach (GridViewRow gvRow in GrdDbClnt.Rows)
                {
                    DbHeads.Add(gvRow.Cells[1].Text);
                    DbAmount.Add(gvRow.Cells[2].Text);
                    DbDescription.Add(gvRow.Cells[3].Text);
                    Dbheadid.Add(gvRow.Cells[4].Text);
                }

                foreach (GridViewRow gvRow in GridCr.Rows)
                {
                    hdslist.Add(gvRow.Cells[1].Text);
                    memlist.Add(gvRow.Cells[3].Text);
                    cheqlist.Add(gvRow.Cells[4].Text);
                    hdidlist.Add(gvRow.Cells[5].Text);
                    amntlist.Add(gvRow.Cells[2].Text);

                    for (int rcnt = 0; rcnt < hdslist.Count; rcnt++)
                    {
                        txtcrChk = cheqlist[rcnt];
                        bnkhead = hdidlist[rcnt];
                        subheadval = bnkhead;

                        if (txtcrChk == "&nbsp;") txtcrChk = "";
                        if (subheadval.Contains('|'))
                        {
                            rootid = 1;
                        }
                        else
                        {
                            rootid = int.Parse(subheadval.Split(':')[0].ToString().Trim().Trim(':').Trim());
                        }
                        if (rootid == 3)
                        {
                            if (txtcrChk.Trim() == "")
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
                }
                // }

                DataTable dtConfirmation = new DataTable();
                dtConfirmation.Columns.Add("Heads");
                dtConfirmation.Columns.Add("Credit");
                dtConfirmation.Columns.Add("Debit");
                decimal totalright = 0.0M;
                decimal totalleft = 0.0M;
                string error = "";
                string errorDebit = "";
                int dtconfirmrow = 0;
                //clientcrheads = Request.Form["Heads"].Replace(".,", ".");
                //hdslist = clientcrheads.Split(',');    //Request.Form["Heads"].Split(',');
                //// hdslist = Request.Form["Heads"].Split(',');
                //amntlist = Request.Form["Amount"].Split(',');
                //memlist = Request.Form["Description"].Split(',');
                //cheqlist = Request.Form["ChequeNo"].Split(',');
                //hdidlist = Request.Form["headid"].Split(',');

                //clientdbheads = Request.Form["DbHeads"].Replace(".,", ".");
                //DbHeads = clientdbheads.Split(',');
                //DbAmount = Request.Form["DbAmount"].Split(',');
                //DbDescription = Request.Form["DbDescription"].Split(',');
                //Dbheadid = Request.Form["Dbheadid"].Split(',');
                //int TotalNoofRows = GViewCR_Selected.Rows.Count  + GViewDB_Selected.Rows.Count ;
                int TotalNoofRows = hdslist.Count + DbHeads.Count;
                for (int iRC = 0; iRC < TotalNoofRows; iRC++)
                {
                    dtConfirmation.Rows.Add();
                }


                for (int rcnt = 0; rcnt < hdslist.Count; rcnt++)
                {
                    txtcrsubAmount = amntlist[rcnt];
                    dtConfirmation.Rows[rcnt][1] = txtcrsubAmount;
                    dtConfirmation.Rows[rcnt][0] = hdslist[rcnt]; //((DropDownList)GridGuardians.Rows[iRow].FindControl("ddlHeads")).SelectedItem.Text;
                    error += txtcrsubAmount + " + ";
                    totalleft += decimal.Parse(txtcrsubAmount.Trim());
                    dtconfirmrow++;
                }
                foreach (GridViewRow gvRow in GrdDbClnt.Rows)
                {
                    if (!string.IsNullOrEmpty(gvRow.Cells[1].Text) && !string.IsNullOrEmpty(gvRow.Cells[2].Text))
                    {

                        for (int iRow = 0; iRow < DbHeads.Count; iRow++)
                        {
                            //TextBox txtsubAmount = (TextBox)GridGuardiansDebit.Rows[iRow].FindControl("txtAmountDebit");
                            string txtsubAmount = DbAmount[iRow];     //GViewDB_Selected.Rows[iRow].Cells[2].Text;
                                                                      //dtConfirmation.Rows[DbHeads.Length + iRow][2] = txtsubAmount;
                            dtConfirmation.Rows[dtconfirmrow + iRow][2] = txtsubAmount;
                            dtConfirmation.Rows[dtconfirmrow + iRow][0] = DbHeads[iRow];    //GViewDB_Selected.Rows[iRow].Cells[1].Text;
                                                                                            //dtConfirmation.Rows[DbHeads.Length + iRow][0] = DbHeads[iRow];    //GViewDB_Selected.Rows[iRow].Cells[1].Text;
                            errorDebit += txtsubAmount + " + ";
                            totalright += decimal.Parse(txtsubAmount.Trim());
                        }
                    }
                }

                #region CreditTable
                if (ViewState["CurrentTable"] != null)
                {
                    DataTable CRTable = new DataTable();
                    CRTable = (DataTable)ViewState["CurrentTable"];
                    if (CRTable.Rows.Count > 0)
                    {
                        //Remove initial blank rows
                        if (CRTable.Rows[0][0].ToString() == "")
                        {
                            CRTable.Rows[0].Delete();
                            CRTable.AcceptChanges();
                        }
                    }
                    DataRow drCurrentRow = CRTable.NewRow();
                    for (int creditrow = 0; creditrow < hdslist.Count; creditrow++)
                    {
                        drCurrentRow["Heads"] = hdslist[creditrow];
                        drCurrentRow["Amount"] = amntlist[creditrow];
                        drCurrentRow["Description"] = memlist[creditrow];
                        drCurrentRow["chequeNO"] = cheqlist[creditrow];
                        drCurrentRow["headid"] = hdidlist[creditrow];
                        //      CRTable.Rows.Add(drCurrentRow.ItemArray);
                    }

                    ViewState["CurrentTable"] = CRTable;
                }
                #endregion
                #region DebitTable
                if (ViewState["CurrentTableDB"] != null)
                {
                    DataTable DRTable = new DataTable();
                    DRTable = (DataTable)ViewState["CurrentTableDB"];
                    if (DRTable.Rows.Count > 0)
                    {
                        if (DRTable.Rows[0][0].ToString() == "")
                        {
                            DRTable.Rows[0].Delete();
                            DRTable.AcceptChanges();
                        }
                    }
                    DataRow drCurrentRow = DRTable.NewRow();
                    for (int debitrow = 0; debitrow < DbHeads.Count; debitrow++)
                    {
                        //drCurrentRow = DRTable.NewRow(); 
                        drCurrentRow["DbHeads"] = DbHeads[debitrow];
                        drCurrentRow["DbAmount"] = DbAmount[debitrow];
                        drCurrentRow["DbDescription"] = DbDescription[debitrow];
                        drCurrentRow["Dbheadid"] = Dbheadid[debitrow];
                        //DRTable.Rows.Add(drCurrentRow.ItemArray);
                    }

                    ViewState["CurrentTableDB"] = DRTable;
                }
                #endregion

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
                    dtConfirmation.Dispose();
                }
            }
            catch (Exception ex)
            {

                this.Response.Redirect(this.Request.Url.AbsoluteUri, false);
                //LogCls.LogError(ex, "Error in Voucher Multiple: Generate");
            }
        }
        protected void btn_crAdd(object sender, EventArgs e)
        {
            try
            {
                Page.Validate("GrpRow");
                if (!Page.IsValid)
                {
                    return;
                }

                if (balayer.GetSingleValue("SELECT TreeHint FROM `headstree` where NodeID='" + ddlHeads.SelectedValue + "'").StartsWith("3,"))
                {
                    return;
                }

                //*********************************
                //    lblcancelmsg.Text = "";
                //if (ddlHeads.SelectedValue.Contains('|'))
                //{
                //    rootid = 1;
                //}
                //else
                //{
                //   rootid = int.Parse(ddlHeads.SelectedValue.Split(':')[0].ToString().Trim().Trim(',').Trim());
                //}
                //if ((rootid == 3) && (txtChequeNO.Visible == true) && (txtChequeNO.Text == ""))
                //{

                //}
                //else
                //{
                if (ViewState["CurrentTable"] != null)
                {
                    credittotal = Convert.ToDouble(txtAmount.Text);
                    //  hidden_totalcred.Value = (existing_crdtotal + credittotal).ToString();

                    DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
                    DataRow drCurrentRow = null;
                    if (dtCurrentTable.Rows.Count > 0)
                    {

                        for (int i = 0; i <= dtCurrentTable.Rows.Count - 1; i++)
                        {
                            drCurrentRow = dtCurrentTable.NewRow();

                            drCurrentRow["Heads"] = ddlHeads.SelectedItem.Text;
                            drCurrentRow["Amount"] = txtAmount.Text;
                            drCurrentRow["Description"] = txtDescription.Text.Replace(",", ";");
                            drCurrentRow["chequeNO"] = txtChequeNO.Text;
                            drCurrentRow["headid"] = ddlHeads.SelectedValue;
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
                        ViewState["CurrentTable"] = dtCurrentTable;

                        //Rebind the Grid with the current data
                        GridCr.DataSource = dtCurrentTable;
                        GridCr.DataBind();

                        ddlHeads.ClearSelection();
                        txtAmount.Text = "";
                        txtDescription.Text = "";
                        txtChequeNO.Text = "";
                        //ViewState["tabnum"] = (57 + GViewCR_Selected.Rows.Count * 4);

                        // SetGridTabIndex(true);

                        ScriptManager.RegisterClientScriptBlock(this, GetType(), "none", "<script>GetVisible();</script>", false);
                    }
                }
                //    }

                //**************************************

            }
            catch (Exception ex)
            {
                string ee = ex.Message;
            }
        }
        protected void btn_debit(object sender, EventArgs e)
        {
            try
            {
                Page.Validate("GrpRowDebit");
                if (!Page.IsValid)
                {
                    return;
                }
                if (ViewState["CurrentTableDB"] != null)
                {
                    DataTable dtCurrentTable = (DataTable)ViewState["CurrentTableDB"];
                    DataRow drCurrentRow = null;
                    if (dtCurrentTable.Rows.Count > 0)
                    {

                        for (int i = 0; i <= dtCurrentTable.Rows.Count - 1; i++)
                        {
                            drCurrentRow = dtCurrentTable.NewRow();
                            drCurrentRow["DbHeads"] = ddlHeadsDebit.SelectedItem.Text;
                            drCurrentRow["DbDescription"] = txtDebitdesc.Text.Replace(",", ";");
                            drCurrentRow["Dbheadid"] = ddlHeadsDebit.SelectedValue;
                            drCurrentRow["DbAmount"] = debitAmnt.Text;
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
                        ViewState["CurrentTableDB"] = dtCurrentTable;
                        GrdDbClnt.DataSource = dtCurrentTable;
                        GrdDbClnt.DataBind();
                        //Rebind the Grid with the current data
                        //   hiddenamnt = Convert.ToDouble(hidden_totalcred.Value);

                        ddlHeadsDebit.ClearSelection();
                        txtDebitdesc.Text = "";
                        debitAmnt.Text = "";
                        ScriptManager.RegisterClientScriptBlock(this, GetType(), "none", "<script>GetVisible();</script>", false);
                    }
                }
                //Page.Validate("GrpRow");
                //if (!Page.IsValid)
                //{
                //    return;
                //}

                //if (balayer.GetSingleValue("SELECT TreeHint FROM `headstree` where NodeID='" + ddlHeads.SelectedValue + "'").StartsWith("3,"))
                //{
                //    return;
                //}
                //if (ViewState["CurrentTable"] != null)
                //{
                //    credittotal = Convert.ToDouble(txtAmount.Text);
                //    hidden_totalcred.Value = (existing_crdtotal + credittotal).ToString();

                //    DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
                //    DataRow drCurrentRow = null;
                //    if (dtCurrentTable.Rows.Count > 0)
                //    {

                //        for (int i = 0; i <= dtCurrentTable.Rows.Count - 1; i++)
                //        {
                //            drCurrentRow = dtCurrentTable.NewRow();

                //            drCurrentRow["Heads"] = ddlHeads.SelectedItem.Text;
                //            drCurrentRow["Amount"] = txtAmount.Text;
                //            drCurrentRow["Description"] = txtDescription.Text.Replace(",", ";");
                //            drCurrentRow["chequeNO"] = txtChequeNO.Text;
                //            drCurrentRow["headid"] = ddlHeads.SelectedValue;
                //        }


                //        if (dtCurrentTable.Rows[0][0].ToString() == "")
                //        {
                //            dtCurrentTable.Rows[0].Delete();
                //            dtCurrentTable.AcceptChanges();
                //        }
                //        drCurrentRow["RowNumber"] = dtCurrentTable.Rows.Count + 1;

                //        dtCurrentTable.Rows.Add(drCurrentRow);
                //        ViewState["CurrentTable"] = dtCurrentTable;
                //        GridCr.DataSource = dtCurrentTable;
                //        GridCr.DataBind();
                //        ddlHeads.ClearSelection();
                //        txtAmount.Text = "";
                //        txtDescription.Text = "";
                //        txtChequeNO.Text = "";
                //        ScriptManager.RegisterClientScriptBlock(this, GetType(), "none", "<script>GetVisible();</script>", false);
            }
            catch (Exception ex)
            {
                string jj = ex.Message;
            }
        }
        private void SetInitialRow()
        {
            DataTable dt = new DataTable();
            DataRow dr = null;
            //Define the Columns
            dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
            dt.Columns.Add(new DataColumn("DbHeads", typeof(string)));
            dt.Columns.Add(new DataColumn("DbAmount", typeof(string)));
            dt.Columns.Add(new DataColumn("DbDescription", typeof(string)));
            dt.Columns.Add(new DataColumn("Dbheadid", typeof(string)));


            //Add a Dummy Data on Initial Load
            dr = dt.NewRow();
            //dr["RowNumber"] = 1;
            dt.Rows.Add(dr);
            //Store the DataTable in ViewState
            ViewState["CurrentTable"] = dt;
            //Bind the DataTable to the Grid
            //GridView1.DataSource = dt;
            //GridView1.DataBind();

            GrdDbClnt.DataSource = dt;
            GrdDbClnt.DataBind();
            //Extract and Fill the DropDownList with Data
            //DropDownList ddlMemberName = (DropDownList)GridView1.Rows[0].Cells[1].FindControl("ddlMemberName");
            //DropDownList ddlMisc = (DropDownList)GridView1.Rows[0].Cells[2].FindControl("ddlMisc");

            //FillDropDownList(ddlMemberName, 0, "");
            //FillDropDownList(ddlMisc, 2, "");
            // FillDropDownList(ddlTokennew, 3, "");
            // FillDropDownList(ddl3);
        }
        private void SetInitialRow1()
        {
            DataTable dt = new DataTable();
            DataRow dr = null;
            //Define the Columns
            dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
            dt.Columns.Add(new DataColumn("Heads", typeof(string)));
            dt.Columns.Add(new DataColumn("Amount", typeof(string)));
            dt.Columns.Add(new DataColumn("Description", typeof(string)));
            dt.Columns.Add(new DataColumn("ChequeNo", typeof(string)));
            dt.Columns.Add(new DataColumn("headid", typeof(string)));

            //Add a Dummy Data on Initial Load
            dr = dt.NewRow();
            //dr["RowNumber"] = 1;
            dt.Rows.Add(dr);
            //Store the DataTable in ViewState
            ViewState["CurrentTable"] = dt;
            //Bind the DataTable to the Grid
            //GridView1.DataSource = dt;
            //GridView1.DataBind();

            GridCr.DataSource = dt;
            GridCr.DataBind();
            //Extract and Fill the DropDownList with Data
            //DropDownList ddlMemberName = (DropDownList)GridView1.Rows[0].Cells[1].FindControl("ddlMemberName");
            //DropDownList ddlMisc = (DropDownList)GridView1.Rows[0].Cells[2].FindControl("ddlMisc");

            //FillDropDownList(ddlMemberName, 0, "");
            //FillDropDownList(ddlMisc, 2, "");
            // FillDropDownList(ddlTokennew, 3, "");
            // FillDropDownList(ddl3);
        }
        protected void GridCr_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                int index = Convert.ToInt32(e.RowIndex);
                DataTable dtable = ViewState["CurrentTable"] as DataTable;
                dtable.Rows[index].Delete();
                ViewState["CurrentTable"] = dtable;
                GridCr.DataSource = ViewState["CurrentTable"];
                GridCr.DataBind();

                //RefNo.RemoveAt(index);
                //RefNo1.RemoveAt(index);

                if (GridCr.Rows.Count == 0)
                {
                    SetInitialRow1();
                }
            }
            catch (Exception)
            {

            }
            finally
            {

            }
        }
        protected void GrdDbClnt_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                int index = Convert.ToInt32(e.RowIndex);
                DataTable dtable = ViewState["CurrentTableDB"] as DataTable;
                dtable.Rows[index].Delete();
                ViewState["CurrentTable"] = dtable;
                GrdDbClnt.DataSource = ViewState["CurrentTableDB"];
                GrdDbClnt.DataBind();

                //RefNo.RemoveAt(index);
                //RefNo1.RemoveAt(index);

                if (GrdDbClnt.Rows.Count == 0)
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
    }
}