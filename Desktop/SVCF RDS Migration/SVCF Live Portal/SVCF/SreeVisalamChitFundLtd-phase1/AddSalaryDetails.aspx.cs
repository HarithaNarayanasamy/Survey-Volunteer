using SVCF_BusinessAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SVCF_TransactionLayer;
using SVCF_DataAccessLayer;
using System.Data;
using log4net;
using log4net.Config;
using System.Text.RegularExpressions;

namespace SreeVisalamChitFundLtd_phase1
{
    public partial class SalaryCreate : System.Web.UI.Page
    {
        ILog logger = log4net.LogManager.GetLogger(typeof(SalaryCreate));

        Dictionary<string, string> Tempdic = new Dictionary<string, string>();
        List<string> TempList = new List<string>();       
        string query = "";
         int rootid;
        bool isFailed, isDate;
        string clientcrheads = "";
        string clientdbheads = "";
        string credithead = "";
        string creditamount = "";
        string creditnarration = "";
        string creditcheqno = "";
        string creditemployeename = "";
        string debithead = "";
        string debitamount = "";
        string debitnarration = "";
        string debitemployeename = "";
        #region ObjectDecl
        BusinessLayer balayer = new BusinessLayer();
        TransactionLayer trn = new TransactionLayer();
        #endregion

        /// <summary>
        /// Automatic Page_Load  function
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            pnlPopup.Visible = false;
           // pnl.Visible = false;
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
                DDEmployee(ddlEmployee, 2, "");
                DDBranch(Cr_DDLHead, 2, "");
                DDBranch(Db_DDLHead, 3, "");
                Cr_SetInitialRow();
                Db_SetInitialRow();
                txtReceiptNumber.Text = balayer.GetSingleValue("SELECT ifnull(max(cast(Voucher_No as unsigned)),0)+1 FROM svcf.voucher where ChoosenDate>='2014/04/01' and  Series='SALARY' and Trans_Type='1' and BranchID=" + Session["Branchid"]);
                CRDHD_TotalAmount.Value = "";
                DBRHD_TotalAmount.Value = "";
                Approval_DateID.Visible = false;
                Approval_numberID.Visible = false;
                NumberLabel.Visible = false;
                Datelabel.Visible = false;
            }
            //rvDate.MinimumValue = balayer.GetSingleValue("SELECT DATE_FORMAT( min(MinimumDate), '%d/%m/%Y') MinimumDate FROM svcf.restrictionmaster where BranchID=" + Session["Branchid"]);
            //rvDate.MaximumValue = DateTime.Now.ToString("dd/MM/yyyy");
            rvDate.MinimumValue = balayer.GetSingleValue("SELECT DATE_FORMAT( MinimumDate, '%d/%m/%Y') MinimumDate FROM svcf.restrictionmaster where BranchID=" + Session["Branchid"]);
            rvDate.MaximumValue = DateTime.Now.ToString("dd/MM/yyyy");

        }

        /// <summary>
        /// DDEmployee
        /// </summary>
        /// <param name="ddl">ddlEmployee</param>
        void DDEmployee(DropDownList ddl, int iType, string MemberID)
        {
            try
            {
                CommonClassFile objcls = new CommonClassFile();
                DataTable dt = null;
                if (iType == 2)
                {
                    dt = objcls.SelectTable1(@"select  concat(cast(  emp.Emp_Name as char),'/',cast(hd.`Node` as char)) as Emp_Name,emp.EMP_ID from svcf.employee_details as emp "+
                                    "join svcf.headstree as hd on hd.NodeID=emp.BranchID where hd.ParentID=1 and emp.BranchID=" + Session["Branchid"]);
                    DataRow dr = dt.NewRow();
                    dt.Rows.InsertAt(dr, 0);
                    ddl.DataSource = dt;
                    ddl.DataValueField = "EMP_ID";
                    ddl.DataTextField = "Emp_Name";
                    ddl.DataBind();
                    ddl.Items.Insert(0, new ListItem("--Select--", "0"));
                }
                else if (iType == 3)
                {
                    dt = objcls.SelectTable1(@"select  concat(cast(  emp.Emp_Name as char),'/',cast(hd.`Node` as char)) as Emp_Name,emp.EMP_ID from svcf.employee_details as emp "+
                                        "join svcf.headstree as hd on hd.NodeID=emp.BranchID where hd.ParentID=1;");
                    DataRow dr = dt.NewRow();
                    dt.Rows.InsertAt(dr, 0);
                    ddl.DataSource = dt;
                    ddl.DataValueField = "EMP_ID";
                    ddl.DataTextField = "Emp_Name";
                    ddl.DataBind();
                    ddl.Items.Insert(0, new ListItem("--Select--", "0"));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// DDBranch method
        /// </summary>
        /// <param name="ddl">ddlBranch</param>
        /// <param name="iType"></param>
        /// <param name="MemberID"></param>
        public void DDBranch(DropDownList ddl, int iType, string MemberID)
        {
            try
            {               
                //Credit Heads
                if (iType == 2)
                {
                    query = "SELECT concat(cast(  v1.`RootID` as char),':',cast(v1.`TreeID` as char)) as ID,v1.`TREE` FROM `svcf`.`view_parent` as v1  where v1.BranchID is null or v1.BranchID=" + Session["Branchid"] + ";";
                    DataTable dtChit1 = balayer.GetDataTable(query);
                    DataRow drow = dtChit1.NewRow();
                    drow[0] = "select";
                    drow[1] = "select";
                    dtChit1.Rows.InsertAt(drow, 0);
                    ddl.DataSource = dtChit1;
                    ddl.DataTextField = "TREE";
                    ddl.DataValueField = "ID";
                    ddl.DataBind();
                }

                    //Debit Heads
                else if (iType == 3)
                {
                    // query = "SELECT concat(cast(  v1.`RootID` as char),':',cast(v1.`TreeID` as char)) as ID,v1.`TREE` FROM `svcf`.`view_parent` as v1  where " +
                    // "if(v1.BranchID=null, v1.`RootID` in (11,1),v1.`RootID` in (11,1));";
                    query = "SELECT concat(cast(  v1.`RootID` as char),':',cast(v1.`TreeID` as char)) as ID,v1.`TREE` FROM `svcf`.`view_parent` as v1  where " +
                "if(v1.BranchID=null, v1.`RootID` in (11,1),v1.`RootID` in (11,1)) or if(v1.BranchID=null, v1.`RootID` in (12,0),v1.`RootID` in (12,0));";
                    DataTable dtChit1 = balayer.GetDataTable(query);
                    DataRow drow = dtChit1.NewRow();
                    drow[0] = "select";
                    drow[1] = "select";
                    dtChit1.Rows.InsertAt(drow, 0);
                    ddl.DataSource = dtChit1;
                    ddl.DataTextField = "TREE";
                    ddl.DataValueField = "ID";
                    ddl.DataBind();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        protected void Onselect_headsChanged(object sender, EventArgs e)
        {
            if(Db_DDLHead.SelectedItem.Text!= "select")
            {
               string val= Db_DDLHead.SelectedValue;
                string zero = val.Split(':')[0];
                string one = val.Split(':')[1];
                if(zero=="1"||one=="90")
                {
                    Approval_DateID.Visible = true;
                    Approval_numberID.Visible = true;
                    NumberLabel.Visible = true;
                    Datelabel.Visible = true;
                }
                else
                {
                    Approval_DateID.Visible = false;
                    Approval_numberID.Visible = false;
                    NumberLabel.Visible = false;
                    Datelabel.Visible = false;
                }
            }
        }
        protected void btnConfirmationNo_Click(object sender, EventArgs e)
        {
            gvoldmember.DataSource = null;
            gvoldmember.DataBind();
            ModalPopupExtender2.Hide();
            pnlPopup.Visible = false;
        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                lblcancelmsg.Text = "";
                Page.Validate("Generate");
                Page.Validate("GrpRow");
                Page.Validate("GrpRowDebit");
                #region VarDeclaration
                string txtcrChk = "";
                string txtcrsubAmount = "";
                string subheadval = "";
                string bnkhead = "";
                //string[] hdslist = new string[0];
                //string[] amntlist = new string[0];
                //string[] memlist = new string[0];
                //string[] cheqlist = new string[0];
                //string[] hdidlist = new string[0];
                //string[] DbHeads = new string[0];
                //string[] DbAmount = new string[0];
                //string[] DbDescription = new string[0];
                //string[] Dbheadid = new string[0];


                List<string> hdslist1 = new List<string>();
                List<string> amntlist1 = new List<string>();
                List<string> memlist1 = new List<string>();
                List<string> cheqlist1 = new List<string>();
                List<string> hdidlist1 = new List<string>();
                List<string> DbHeads1= new List<string>();
                List<string> DbAmount1 = new List<string>();
                List<string> DbDescription1 = new List<string>();
                List<string> Dbheadid1= new List<string>();
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

                string ResExist = balayer.GetSingleValue("select count(*) from voucher where ChoosenDate>='2014/04/01' and Voucher_No=" + txtReceiptNumber.Text.Trim() + " and Trans_Type='1' and Series='SALARY' and BranchID=" + Session["Branchid"]);
                int iExist = int.Parse(ResExist);
                if (iExist != 0)
                {
                    lblHint.Text = "VExist";
                    lblHeading.Text = "Status!!!";
                    lblContent.Text = "AddSalary Already Exsist!!! ";
                    ModalPopupExtender2.PopupControlID = "pnlpopup";
                    btnYes.Focus();
                    ModalPopupExtender2.Show();
                    pnlPopup.Visible = true;
                    return;
                }
                foreach (GridViewRow gvRow in Credit_GView.Rows)
                {

                     credithead = Convert.ToString(gvRow.Cells[5].Text);
                     creditamount = Convert.ToString(gvRow.Cells[2].Text);
                     creditnarration= Convert.ToString(gvRow.Cells[3].Text);
                     creditcheqno= Convert.ToString(gvRow.Cells[4].Text);
                     creditemployeename = Convert.ToString(gvRow.Cells[1].Text);
                     decimal dblMiscTemp = 0.0M;
                   

                        clientcrheads = credithead.Replace(".,", ".");
                        hdslist1.Add(credithead);    //Request.Form["Heads"].Split(',');
                        amntlist1.Add( creditamount);
                         memlist1.Add(creditnarration);
                        cheqlist1.Add( creditcheqno);
                        hdidlist1.Add( creditemployeename);
                        
                   

                }
                foreach (GridViewRow gvRowdebit in Debit_GView.Rows)
                {
                    debithead = Convert.ToString(gvRowdebit.Cells[4].Text);
                    debitamount = Convert.ToString(gvRowdebit.Cells[2].Text);
                    debitnarration = Convert.ToString(gvRowdebit.Cells[3].Text);
                    debitemployeename = Convert.ToString(gvRowdebit.Cells[1].Text);


                    clientdbheads = debithead.Replace(".,", ".");
                    DbHeads1 .Add(debithead);
                    DbAmount1.Add( debitamount);
                    DbDescription1.Add(debitnarration);
                    Dbheadid1.Add( debitemployeename);
                }

                    DataTable dtConfirmation = new DataTable();
                dtConfirmation.Columns.Add("Heads");
                dtConfirmation.Columns.Add("Credit");
                dtConfirmation.Columns.Add("Debit");
                decimal totalright = 0.0M;
                decimal totalleft = 0.0M;
                string error = "";
                string errorDebit = "";
                int dtconfirmrow = 0;
                //clientcrheads = credithead.Replace(".,", ".");
                //hdslist1.Add(clientcrheads);    //Request.Form["Heads"].Split(',');
                //amntlist1.Add(creditamount);
                //memlist1.Add(creditnarration);
                //cheqlist1.Add(creditcheqno);
                //hdidlist1.Add(creditemployeename);

                //clientdbheads = debithead.Replace(".,", ".");
                //DbHeads1.Add(clientdbheads);
                //DbAmount1.Add(debitamount);
                //DbDescription1.Add(debitnarration);
                //Dbheadid1.Add(debitemployeename);
                //int TotalNoofRows = GViewCR_Selected.Rows.Count  + GViewDB_Selected.Rows.Count ;
               // int TotalNoofRows = hdslist1.count + DbHeads1.count;
                int TotalNoofRows = hdslist1.Count+ DbHeads1.Count;
                for (int iRC = 0; iRC < TotalNoofRows; iRC++)
                {
                    dtConfirmation.Rows.Add();
                }


                for (int rcnt = 0; rcnt < hdslist1.Count; rcnt++)
                {
                    txtcrsubAmount = amntlist1[rcnt];
                    dtConfirmation.Rows[rcnt][1] = txtcrsubAmount;
                    dtConfirmation.Rows[rcnt][0] = hdslist1[rcnt]; //((DropDownList)GridGuardians.Rows[iRow].FindControl("ddlHeads")).SelectedItem.Text;
                    error += txtcrsubAmount + " + ";
                    totalleft += decimal.Parse(txtcrsubAmount.Trim());
                    dtconfirmrow++;
                }

                if (!string.IsNullOrEmpty(credithead) && !string.IsNullOrEmpty(creditamount))
                {

                    for (int iRow = 0; iRow < DbHeads1.Count; iRow++)
                    {
                        //TextBox txtsubAmount = (TextBox)GridGuardiansDebit.Rows[iRow].FindControl("txtAmountDebit");
                        string txtsubAmount = DbAmount1[iRow];     //GViewDB_Selected.Rows[iRow].Cells[2].Text;
                        //dtConfirmation.Rows[DbHeads.Length + iRow][2] = txtsubAmount;
                        dtConfirmation.Rows[dtconfirmrow + iRow][2] = txtsubAmount;
                        dtConfirmation.Rows[dtconfirmrow + iRow][0] = DbHeads1[iRow];    //GViewDB_Selected.Rows[iRow].Cells[1].Text;
                        //dtConfirmation.Rows[DbHeads.Length + iRow][0] = DbHeads[iRow];    //GViewDB_Selected.Rows[iRow].Cells[1].Text;
                        errorDebit += txtsubAmount + " + ";
                        totalright += decimal.Parse(txtsubAmount.Trim());
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
                    for (int creditrow = 0; creditrow < hdslist1.Count; creditrow++)
                    {
                        drCurrentRow["HeadName"] = hdslist1[creditrow];
                        drCurrentRow["Amount"] = amntlist1[creditrow];
                        drCurrentRow["Narration"] = memlist1[creditrow];
                        drCurrentRow["chequeNO"] = cheqlist1[creditrow];
                        drCurrentRow["Employee Name"] = hdidlist1[creditrow];
                        CRTable.Rows.Add(drCurrentRow.ItemArray);
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
                    for (int debitrow = 0; debitrow < DbHeads1.Count; debitrow++)
                    {
                        //drCurrentRow = DRTable.NewRow(); 
                        drCurrentRow["Head"] = DbHeads1[debitrow];
                        drCurrentRow["Amount"] = DbAmount1[debitrow];
                        drCurrentRow["Narration"] = DbDescription1[debitrow];
                        drCurrentRow["Employee Name"] = Dbheadid1[debitrow];
                        DRTable.Rows.Add(drCurrentRow.ItemArray);
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
                    ModalPopupExtender2.PopupControlID = "pnlpopup";
                    pnlPopup.Visible = true;
                    btnYes.Focus();
                    ModalPopupExtender2.Show();
                }
                else
                {
                    error = error.Trim().Trim('+');
                    lblHeading.Text = "Warning!!!";
                    lblContent.Text = "Please Tally Credit and Debit Amount!!!" + "</br></br>" + error.Trim().Trim('+') + " !=  " + errorDebit.Trim().Trim('+');
                    ModalPopupExtender2.PopupControlID = "pnlpopup";
                    gvoldmember.DataSource = dtConfirmation;
                    gvoldmember.DataBind();
                    ModalPopupExtender2.PopupControlID = "pnlpopup";
                    ModalPopupExtender2.Show();
                    btnYes.Focus();
                    pnlPopup.Visible = true;
                    dtConfirmation.Dispose();
                }
            }
            catch (Exception ex)
            {

                this.Response.Redirect(this.Request.Url.AbsoluteUri, false);
                //LogCls.LogError(ex, "Error in Voucher Multiple: Generate");
            }
        }
        protected void btnyes_Click(object sender, EventArgs e)
        {
            ModalPopupExtender2.Hide();
        }
        protected void btnNo_Click(object sender, EventArgs e)
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
                //    if (txtReceiptNumber.Text != "")
                //     balayer.ExecuteQuery("Delete From `svcf`.`voucher` Where Series = 'ZZZ' And Voucher_No = " + txtVoucherNo.Text + "");
                txtReceiptNumber.Text = "";
              //  txtSeries.Text = "";
               // txtSeries.Focus();
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
        /// <summary>
        /// Salary Details create post method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnYes_Click(object sender, EventArgs e)
        {
        
            DateTime dtChoosenDate;
            string query = "";
            string SelectedDebit_Head = "";
            long strCreditQuery_TKey = 0;
            int rootid = 0;
            string CrHeadId = "";
            string DbHeadId = "";
            string[] BankName;
            string[] DbHead;
            string SelectedBankName = "";
            string CrHeadName = "";
            string DBHeadName = "";
           
            dtChoosenDate = DateTime.Parse(txtDate.Text);
            if (ddlEmployee.Text == "0")
            {
                return;
            }
         
            Page.Validate("salarydetailsgroup");
            if (!Page.IsValid)
                return;
            try
            {
                if (lblHint.Text == "Reload")
                {
                    lblHint.Text = "";
                    this.Response.Redirect(this.Request.Url.AbsolutePath.ToString(), false);
                }
                else
                  if (lblHint.Text == "VExist")
                {
                    if (txtReceiptNumber.Text != "")
                        balayer.ExecuteQuery("Delete From `svcf`.`voucher` Where Series = 'ZZZ' And Voucher_No = " + txtReceiptNumber.Text);
                    txtReceiptNumber.Text = "";
                 //   txtSeries.Text = "";
                  //  txtSeries.Focus();
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
                    decimal dblDueAmountCr = 0, dblDueAmountDb = 0;
                    foreach (GridViewRow gvRow in Credit_GView.Rows)
                    {
                        decimal dblDueTemp = decimal.Parse(gvRow.Cells[2].Text);
                        dblDueAmountCr += dblDueTemp;
                    }


                    foreach (GridViewRow gvRow in Debit_GView.Rows)
                    {
                        decimal dblDueTemp = decimal.Parse(gvRow.Cells[2].Text);
                        dblDueAmountDb += dblDueTemp;
                    }

                    if (dblDueAmountCr == dblDueAmountDb)
                    {
                        if (0 != int.Parse(balayer.GetSingleValue("select ifnull(Count(*),0) from voucher where BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and Trans_Type='1' and  Voucher_No=" + txtReceiptNumber.Text + " and Series='SALARY'")))
                        {
                            Response.Write("<script>alert('Following ReceiptNo Already Exist In Series: SALARY : <br><br> " + txtReceiptNumber.Text + "');</script>");
                        }
                        else
                        {
                            ClsSession objSession = (ClsSession)Session["objSession"];

                            //System.Guid guid = Guid.NewGuid();
                            //string hexstring = BitConverter.ToString(guid.ToByteArray());
                            //string DualTransactionKey = "0x" + hexstring.Replace("-", string.Empty);                      

                            //13/06/2023
                            System.Guid guid = Guid.NewGuid();
                            string guidForChar36 = guid.ToString();
                            string hexstring = BitConverter.ToString(guid.ToByteArray());
                            string guidForBinary16 = "0x" + hexstring.Replace("-", string.Empty);


                            string DualTransactionKey = guidForBinary16;



                            //stored procedure for DualTransactionKey

                            //string DualTransactionKey = Convert.ToString(balayer.sp_gendratedt_key());


                            string trans_medium = "1";
                            string Other_Trans_Type = "1";
                            string chequeno = "";
                            //Credit Entry
                            for (int i = 0; i <= Credit_GView.Rows.Count - 1; i++)
                            {
                                Label Headid = (Label)Credit_GView.Rows[i].FindControl("Crlblheadid");
                                Label MemberId = (Label)Credit_GView.Rows[i].FindControl("Crlblmemberid");
                                chequeno = Credit_GView.Rows[i].Cells[4].Text;
                                CrHeadName = Credit_GView.Rows[i].Cells[5].Text;
                                rootid = int.Parse(Headid.Text.Split(':')[0].ToString().Trim().Trim(':').Trim());
                                CrHeadId = Headid.Text.Split(':')[1].Trim();
                                CrHeadName = CrHeadName.Replace("&gt;&gt;", ">>");

                                query = "INSERT INTO `svcf`.`voucher` (`DualTransactionKey`,`BranchID`,`CurrDate`,`Voucher_No`,`Voucher_Type`,`Head_Id`,`ChoosenDate`,`Narration`,`Amount`,`Series`,`ReceievedBy`,`Trans_Type`,`T_Day`," +
                                    "`T_Month`,`T_Year`,`MemberID`,`Trans_Medium`,Other_Trans_Type,`RootID`,`ChitGroupId`,`M_Id`,LoginIP) VALUES (" + DualTransactionKey + "," +
                                    "" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + ",'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," + txtReceiptNumber.Text + "," +
                                    "'C'," + CrHeadId + ",'" + dtChoosenDate.ToString("yyyy-MM-dd") + "','" + Credit_GView.Rows[i].Cells[3].Text + "'," + Credit_GView.Rows[i].Cells[2].Text + ",'SALARY'," +
                                    "'" + ddlEmployee.SelectedItem.Text.Split('/')[0] + "',1," + dtChoosenDate.Day + "," + dtChoosenDate.Month + "," + dtChoosenDate.Year + "," + 0 + "," +
                                    "" + trans_medium + "," + Other_Trans_Type + "," + rootid + "," + 0 + "," + ddlEmployee.SelectedValue + ",'" + objSession.LoginIp + "') ";
                                strCreditQuery_TKey = trn.insertorupdateTrn(query);

                                query = "";

                                //Cheque no
                                if (CrHeadName.Contains('>') && chequeno != "" && chequeno != "&nbsp;")
                                {
                                    BankName = Regex.Split(CrHeadName, ">>");
                                    if (BankName.Length > 2)
                                    {
                                        SelectedBankName = BankName[2].Trim();
                                    }
                                    else
                                    {
                                        SelectedBankName = BankName[1].Trim();
                                    }
                                    if ((Credit_GView.Rows[i].Cells[4].Text != "") && (Credit_GView.Rows[i].Cells[4].Text != "&nbsp;"))
                                    {
                                        query = "insert into transbank(BranchID, TransactionKey, DualTransactionKey, T_Day, T_Month, T_Year,BankHeadID,  Head_Id, MemberID, CustomersBankName, ChequeDDNO, GivenAmount, TotalChequeDDAmount, IsBounced, Trans_Type) values (" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + "," +
                                            "" + strCreditQuery_TKey + "," + DualTransactionKey + "," + dtChoosenDate.Day + "," + dtChoosenDate.Month + "," + dtChoosenDate.Year + "," +
                                            "" + CrHeadId + "," + ddlEmployee.SelectedValue + "," + 0 + ",'" + SelectedBankName + "'," + Credit_GView.Rows[i].Cells[4].Text + "," + Credit_GView.Rows[i].Cells[2].Text + "," +
                                            "" + Credit_GView.Rows[i].Cells[2].Text + ",0,1)";
                                        trn.insertorupdateTrn(query);
                                        query = "";
                                    }
                                }
                            }

                            //Debit Entry
                            for (int i = 0; i <= Debit_GView.Rows.Count - 1; i++)
                            {
                                Label Headid = (Label)Debit_GView.Rows[i].FindControl("Dblblheadid");
                                Label MemberId = (Label)Debit_GView.Rows[i].FindControl("Dblblmemberid");
                                DBHeadName = Debit_GView.Rows[i].Cells[4].Text;
                                DBHeadName = DBHeadName.Replace("&gt;&gt;", ">>");
                                rootid = int.Parse(Headid.Text.Split(':')[0].ToString().Trim().Trim(':').Trim());
                                DbHeadId = Headid.Text.Split(':')[1].Trim();
                                DbHead = Regex.Split(DBHeadName, ">>");
                                if (DbHead.Length > 2)
                                {
                                    SelectedDebit_Head = DbHead[2].Trim();
                                }
                                else
                                {
                                    if (DbHead.Length > 1)
                                        SelectedDebit_Head = DbHead[1].Trim();
                                    else
                                        SelectedDebit_Head = DbHead[0].Trim();
                                }

                                query = "";
                                query = "INSERT INTO `svcf`.`voucher` (`DualTransactionKey`,`BranchID`,`CurrDate`,`Voucher_No`,`Voucher_Type`,`Head_Id`,`ChoosenDate`,`Narration`," +
                                    "`Amount`,`Series`,`ReceievedBy`,`Trans_Type`,`T_Day`,`T_Month`,`T_Year`,`MemberID`,`Trans_Medium`,Other_Trans_Type,`RootID`,`ChitGroupId`," +
                                    "`M_Id`,LoginIP) VALUES (" + DualTransactionKey + "," + balayer.ToobjectstrEvenNull(Session["Branchid"]) + "," +
                                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," + txtReceiptNumber.Text + ",'D'," + DbHeadId + ",'" + dtChoosenDate.ToString("yyyy-MM-dd") + "'," +
                                    "'" + Debit_GView.Rows[i].Cells[3].Text + "'," + Debit_GView.Rows[i].Cells[2].Text + ",'SALARY','" + ddlEmployee.SelectedItem.Text.Split('/')[0] + "',1," + dtChoosenDate.Day + "," +
                                    "" + dtChoosenDate.Month + "," + dtChoosenDate.Year + "," + 0 + "," + trans_medium + "," + Other_Trans_Type + "," + rootid + "," + 0 + "," +
                                    "" + ddlEmployee.SelectedValue + ",'" + objSession.LoginIp + "') ";
                                strCreditQuery_TKey = trn.insertorupdateTrn(query);
                                query = "";
                            }
                            trn.CommitTrn();
                            logger.Info("SalaryCreate.aspx - btnAdd_Click():  Completed: " + DateTime.Now + " by: " + Convert.ToString(Session["UserName"]) + "");
                            CRDHD_TotalAmount.Value = "";
                            DBRHD_TotalAmount.Value = "";
                            txtReceiptNumber.Text = balayer.GetSingleValue("SELECT ifnull(max(cast(Voucher_No as unsigned)),0)+1 FROM svcf.voucher where ChoosenDate>='2014/04/01' and  Series='SALARY' and Trans_Type=1 and BranchID=" + Session["Branchid"]);
                            //lblT.Text = "Status";
                            lblContent.Text = "Salary details created successfully";
                            lblContent.ForeColor = System.Drawing.Color.Green;
                            ModalPopupExtender2.PopupControlID = "PnlApprove";
                            ModalPopupExtender2.Show();
                          //  PnlApprove.Visible = true;
                        }
                    }
                    else
                    {
                        Response.Write("<script>alert('Total Amount is not equal');</script>");
                        CRDHD_TotalAmount.Value = "";
                        DBRHD_TotalAmount.Value = "";
                    }
                }
                gvoldmember.DataSource = null;
                gvoldmember.DataBind();
            }
            catch (Exception error)
            {
                try
                {
                    trn.RollbackTrn();
                    logger.Info("SalaryCreate.aspx - btnAdd_Click():  Error: " + error.Message + ": " + DateTime.Now + " by: " + Convert.ToString(Session["UserName"]) + "");
                    CRDHD_TotalAmount.Value = "";
                    DBRHD_TotalAmount.Value = "";
                    ViewState["Cr_CurrentTable"] = null;
                    Credit_GView.DataSource = (DataTable)ViewState["Cr_CurrentTable"];
                    Credit_GView.DataBind();
                    ViewState["Db_CurrentTable"] = null;
                    Debit_GView.DataSource = (DataTable)ViewState["Db_CurrentTable"];
                    Debit_GView.DataBind();
                    Cr_SetInitialRow();
                    Db_SetInitialRow();
                }
                catch (Exception ex) {
                    LogCls.LogError(ex, "Add salary");
                }
                finally
                {
                    btnYes.Focus();
                    isFailed = true;
                    ModalPopupExtender2.PopupControlID = "pnlpopup";
                    ModalPopupExtender2.Show();
                    pnlPopup.Visible = true;
                    //lblHD.Text = "Status";
                    lblHint.Text = "Reload";
                    lblHeading.Text = "Error Status!!!";
                    //lblT.Text = "Status";
                    lblContent.Text = error.Message;
                    lblContent.ForeColor = System.Drawing.Color.Red;
                    //ModalPopupExtender2.PopupControlID = "PnlApprove";
                    //ModalPopupExtender2.Show();
                 //   PnlApprove.Visible = true;                  
                }

            }
            finally
            {
                trn.DisposeTrn();
                ViewState["Cr_CurrentTable"] = null;
                Credit_GView.DataSource = (DataTable)ViewState["Cr_CurrentTable"];
                Credit_GView.DataBind();
                ViewState["Db_CurrentTable"] = null;
                Debit_GView.DataSource = (DataTable)ViewState["Db_CurrentTable"];
                Debit_GView.DataBind();
                Cr_SetInitialRow();
                Db_SetInitialRow();
            }         
        }


        protected void btnMsgOkCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.Url.AbsolutePath);
        }

        protected void btn_ok(object sender, EventArgs e)
        {
            Response.Redirect(Request.Url.AbsolutePath.ToString());
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("SalaryDetails.aspx");
        }

        protected void BtnAddCredit_Click(object sender, EventArgs e)
        {
            try
            {               
                if (Page.IsValid)
                {
                    if (ViewState["Cr_CurrentTable"] != null)
                    {
                        DataTable dtCurrentTable = (DataTable)ViewState["Cr_CurrentTable"];
                        DataRow drCurrentRow = null;
                        if (dtCurrentTable.Rows.Count > 0)
                        {                            
                            for (int i = 0; i <= dtCurrentTable.Rows.Count - 1; i++)
                            {
                                drCurrentRow = dtCurrentTable.NewRow();
                                // Update the DataRow with the DDL Selected Items
                                drCurrentRow["CrRecptNumber"] = txtReceiptNumber.Text.ToString();
                                drCurrentRow["CrEmpName"] = ddlEmployee.SelectedItem.Text; 
                                drCurrentRow["CrAmount"] = CrtxtAmount.Text;
                                drCurrentRow["CrNarration"] = CrTxtNarration.Text;
                                drCurrentRow["CrCheqno"] = txtChequeNumber.Text;
                                drCurrentRow["CrHead_Id"] = Cr_DDLHead.SelectedValue;
                                drCurrentRow["CrMemberId"] = ddlEmployee.SelectedValue;
                                drCurrentRow["CrHead"] = Cr_DDLHead.SelectedItem.Text; 
                                
                            }

                            //Remove initial blank row
                            if (dtCurrentTable.Rows[0][0].ToString() == "")
                            {
                                dtCurrentTable.Rows[0].Delete();
                                dtCurrentTable.AcceptChanges();
                            }
                            //Rebind the Grid with the current data
                            dtCurrentTable.Rows.Add(drCurrentRow);
                            ViewState["Cr_CurrentTable"] = dtCurrentTable;

                            //Rebind the Grid with the current data
                            Credit_GView.DataSource = dtCurrentTable;
                            Credit_GView.DataBind();                         
                        }
                        if (CRDHD_TotalAmount.Value != "")
                        {
                            CRDHD_TotalAmount.Value = (Convert.ToDouble(CrtxtAmount.Text) + Convert.ToDouble(CRDHD_TotalAmount.Value)).ToString();
                        }
                        else
                        {
                            CRDHD_TotalAmount.Value = CrtxtAmount.Text;
                        }
                        Cr_DDLHead.ClearSelection();
                        txtChequeNumber.Text = "";
                        CrTxtNarration.Text = "";
                        CrtxtAmount.Text = "";
                        CrTxtNarration.Text = "";
                    }
                }

            }
            catch (Exception) { }           
        }

        private void Cr_SetInitialRow()
        {
            DataTable dt = new DataTable();
            DataRow dr = null;
            //Define the Columns
            dt.Columns.Add(new DataColumn("CrRecptNumber", typeof(string)));
            dt.Columns.Add(new DataColumn("CrEmpName", typeof(string)));
            dt.Columns.Add(new DataColumn("CrAmount", typeof(string)));
            dt.Columns.Add(new DataColumn("CrNarration", typeof(string)));
            dt.Columns.Add(new DataColumn("CrCheqno", typeof(string)));
            dt.Columns.Add(new DataColumn("CrHead_Id", typeof(string)));
            dt.Columns.Add(new DataColumn("CrMemberId", typeof(string)));
            dt.Columns.Add(new DataColumn("CrHead", typeof(string)));
            
            //Add a Dummy Data on Initial Load
            dr = dt.NewRow();

            dt.Rows.Add(dr);
            ViewState["Cr_CurrentTable"] = dt;

            Credit_GView.DataSource = dt;
            Credit_GView.DataBind();

        }

        private void Db_SetInitialRow()
        {
            DataTable dt = new DataTable();
            DataRow dr = null;
            //Define the Columns
            dt.Columns.Add(new DataColumn("DbRecptNumber", typeof(string)));
            dt.Columns.Add(new DataColumn("DbEmpName", typeof(string)));
            dt.Columns.Add(new DataColumn("DbAmount", typeof(string)));
            dt.Columns.Add(new DataColumn("DbNarration", typeof(string)));
            dt.Columns.Add(new DataColumn("DbHead_Id", typeof(string)));
            dt.Columns.Add(new DataColumn("DbMemberId", typeof(string)));
            dt.Columns.Add(new DataColumn("DbHead", typeof(string)));
           
            //Add a Dummy Data on Initial Load
            dr = dt.NewRow();

            dt.Rows.Add(dr);
            ViewState["Db_CurrentTable"] = dt;

            Debit_GView.DataSource = dt;
            Debit_GView.DataBind();

        }
       

        protected void btnDebitAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (Page.IsValid)
                {
                    if (ViewState["Db_CurrentTable"] != null)
                    {
                        DataTable dtCurrentTable = (DataTable)ViewState["Db_CurrentTable"];
                        DataRow drCurrentRow = null;
                        if (dtCurrentTable.Rows.Count > 0)
                        {

                            for (int i = 0; i <= dtCurrentTable.Rows.Count - 1; i++)
                            {
                                drCurrentRow = dtCurrentTable.NewRow();
                                // Update the DataRow with the DDL Selected Items
                                drCurrentRow["DbRecptNumber"] = txtReceiptNumber.Text.ToString();
                                drCurrentRow["DbEmpName"] = ddlEmployee.SelectedItem .Text;
                                drCurrentRow["DbAmount"] = DbTxtAmount.Text;
                                drCurrentRow["DbNarration"] = DbtxtNarration.Text+"#"+Approval_numberID.Text+"-"+Approval_DateID.Text;
                                drCurrentRow["DbHead_Id"] = Db_DDLHead.SelectedValue;
                                drCurrentRow["DbMemberId"] = ddlEmployee.SelectedValue;
                                drCurrentRow["DbHead"] = Db_DDLHead.SelectedItem.Text; 
                            }

                            //Remove initial blank row
                            if (dtCurrentTable.Rows[0][0].ToString() == "")
                            {
                                dtCurrentTable.Rows[0].Delete();
                                dtCurrentTable.AcceptChanges();
                            }
                            //Rebind the Grid with the current data
                            dtCurrentTable.Rows.Add(drCurrentRow);
                            ViewState["Db_CurrentTable"] = dtCurrentTable;

                            //Rebind the Grid with the current data
                            Debit_GView.DataSource = dtCurrentTable;
                            Debit_GView.DataBind();
                           
                        }
                        if (DBRHD_TotalAmount.Value != "")
                        {
                            DBRHD_TotalAmount.Value = (Convert.ToDouble(DbTxtAmount.Text) + Convert.ToDouble(DBRHD_TotalAmount.Value)).ToString();
                        }
                        else
                        {
                            DBRHD_TotalAmount.Value = DbTxtAmount.Text;
                        }

                        DbTxtAmount.Text = "";
                        DbtxtNarration.Text = "";
                        Approval_DateID.Text = "";
                        Approval_numberID.Text = "";
                        Db_DDLHead.ClearSelection();
                    }
                }

            }
            catch (Exception) { }     
        }

        protected void btnDebitCancel_Click(object sender, EventArgs e)
        {

        }

        protected void Credit_GView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                int index = 0;
                index = Convert.ToInt32(e.RowIndex);
                DataTable dtable = ViewState["Cr_CurrentTable"] as DataTable;
                dtable.Rows[index].Delete();
                ViewState["Cr_CurrentTable"] = dtable;
                Credit_GView.DataSource = ViewState["Cr_CurrentTable"];
                Credit_GView.DataBind();
                if (Credit_GView.Rows.Count == 0)
                {
                    Cr_SetInitialRow();
                }
            }
            catch (Exception)
            {

            }
        }

        protected void Debit_GView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                int index = 0;
                index = Convert.ToInt32(e.RowIndex);
                DataTable dtable = ViewState["Db_CurrentTable"] as DataTable;
                dtable.Rows[index].Delete();
                ViewState["Db_CurrentTable"] = dtable;
                Debit_GView.DataSource = ViewState["Db_CurrentTable"];
                Debit_GView.DataBind();
                if (Debit_GView.Rows.Count == 0)
                {
                    Db_SetInitialRow();
                }
            }
            catch (Exception)
            {

            }
        }

        protected void chkgetotherbranchlist_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if(chkgetotherbranchlist.Checked==true)
                {
                    //Load all branch Employees
                    DDEmployee(ddlEmployee, 3, "");
                }
                else
                {
                    //Load only current branch Employees
                    DDEmployee(ddlEmployee, 2, "");
                }

            }
            catch(Exception)
            {

            }
        }

    }
}