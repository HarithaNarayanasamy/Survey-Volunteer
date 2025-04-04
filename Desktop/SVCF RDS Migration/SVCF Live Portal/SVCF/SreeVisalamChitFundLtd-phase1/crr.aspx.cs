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
    public partial class crr : System.Web.UI.Page
    {
        #region Object
        BusinessLayer balayer = new BusinessLayer();
        TransactionLayer trn = new TransactionLayer();
        #endregion

        ILog logger = log4net.LogManager.GetLogger(typeof(crr));

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
                rvDate.MinimumValue = "01/04/2014";
                rvDate.MaximumValue = "30/08/2014";
                Session["CheckRefresh"] = System.Guid.NewGuid().ToString();
                SetInitialRow();
                CollectorName();
                DataTable dt = balayer.GetDataTable("SELECT Emp_Name,Emp_Name FROM svcf.employee_details where BranchID=" + Session["Branchid"]);
                ddlCollected.DataSource = dt;
                DataRow dr = dt.NewRow();
                dr[0] = "--Select--";
                dr[1] = "--Select--";
                ddlCollected.DataTextField = "Emp_Name";
                ddlCollected.DataValueField = "Emp_Name";
                dt.Rows.InsertAt(dr, 0);
                ddlCollected.DataBind();
                ddlColloctorName.Focus();
                //fillBankHead();
            }
        }
        protected void Page_PreRender(object sender, EventArgs e)
        {
            ViewState["CheckRefresh"] = Session["CheckRefresh"];
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
                string strQuery = "select ifnull(max(Voucher_No)+1,0) from voucher where BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and Trans_Type=1 and Voucher_No>=" + from + " and Voucher_No<=" + t0 + " and `Series`='" + ddlReceiptSeries.SelectedItem.Text + "'";
                int RecNo = int.Parse(balayer.GetSingleValue(strQuery));
                
                if (RecNo != 0)
                {
                    txtReceiptNo.Text = RecNo.ToString();
                }
                else
                {
                    txtReceiptNo.Text = from.ToString();
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Warning", "alert(' Please Assign new Reciept Book!!!');", true);
            }
        }
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

        

        private void FillDropDownList(DropDownList ddl, int iType, string GroupID)
        {  //chit
            if (iType == 0)
            {
                ddl.DataSource = null;
                DataTable dtgroupno = null;
                dtgroupno = balayer.GetDataTable("SELECT concat(MemberID,' | ',CustomerName) as CustomerName,MemberIDNew FROM svcf.membermaster  where TypeOfMember<>'Foreman'");
                ddl.DataSource = dtgroupno;
                ddl.DataValueField = "MemberIDNew";
                ddl.DataTextField = "CustomerName";
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
                    DataTable dtgroupno = balayer.GetDataTable(@"SELECT GrpMemberID,Head_Id FROM svcf.membertogroupmaster where MemberID="+ddlGroupNo.SelectedItem.Value);
                    // DataTable dtgroupno = SreeVisalamChitFundLtd_phase1.balayer.GetDataTable("select Head_Id,GROUPNO from groupmaster where Head_Id='" + SreeVisalamChitFundLtd_phase1.balayer.ToobjectstrEvenNull(Session["Branchid"]) + "'");
                    ddl.DataSource = dtgroupno;
                    ddl.DataValueField = "Head_Id";
                    ddl.DataTextField = "GrpMemberID";
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
                DataTable dtgroupno = balayer.GetDataTable("SELECT concat(cast(RootID as char),',',cast(TreeID as char)) AS TreeID,TREE FROM svcf.view_parent where RootID in (11,1)");

                ddl.DataSource = dtgroupno;
                DataRow dr = dtgroupno.NewRow();
                dr[0] = "0,0";
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

            FillDropDownList(ddlGroupNo, 0, "");
            FillDropDownList(ddlMisc, 2, "");
            // FillDropDownList(ddl3);
        }
        
        
        protected void ddlGroupNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow gvRow = (GridViewRow)((DropDownList)sender).Parent.Parent;
            DropDownList ddlMemberName = (DropDownList)gvRow.FindControl("ddlMemberName");
            string selectedValue = ((DropDownList)gvRow.FindControl("ddlGroupNo")).SelectedValue;
            FillDropDownList(ddlMemberName, 1, selectedValue);
            ((DropDownList)gvRow.FindControl("ddlGroupNo")).Focus();
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
            TransactionLayer trn = new TransactionLayer();
            gvConfirm.DataSource = null;
            gvConfirm.DataBind();
            
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

                for (int i = 0; i < GridView1.Rows.Count; i++)
                {
                    DropDownList RowddlGroupNo = (DropDownList)GridView1.Rows[i].FindControl("ddlGroupNo");
                    DropDownList RowddlMemberName = (DropDownList)GridView1.Rows[i].FindControl("ddlMemberName");
                    TextBox RowtxtAmount = (TextBox)GridView1.Rows[i].FindControl("txtAmount");
                    DropDownList RowddlMisc = (DropDownList)GridView1.Rows[i].FindControl("ddlMisc");
                    TextBox RowtxtMisc = (TextBox)GridView1.Rows[i].FindControl("txtMisc");
                    string group = balayer.GetSingleValue("SELECT GroupID FROM svcf.membertogroupmaster where Head_Id=" + RowddlMemberName.SelectedItem.Value);
                    string ChitsBranchID = balayer.GetSingleValue("SELECT BranchID FROM `svcf`.`groupmaster` where Head_Id=" + group + "");
                    string MemberID = RowddlGroupNo.SelectedItem.Value;
                    string TokenNo = RowddlMemberName.SelectedItem.Value;
                    string RootID = "";

                    if (ChitsBranchID == balayer.ToobjectstrEvenNull(Session["Branchid"]))
                    {
                        //Local ChitsTransaction
                        //DueAmount
                        if (decimal.Parse(RowtxtAmount.Text) != 0.00M)
                        {
                            string strChitHeadQuery = "INSERT INTO `svcf`.`voucher` (`DualTransactionKey`,`BranchID`,`CurrDate`,`Voucher_No`,`Voucher_Type`,`Head_Id`,`ChoosenDate`,`Narration`,`Amount`,`Series`,`ReceievedBy`,`Trans_Type`,`T_Day`,`T_Month`,`T_Year`,`MemberID`,`Trans_Medium`,`RootID`,`ChitGroupId`) VALUES (" + DualTransactionKey + "," + balayer.ToobjectstrEvenNull(Session["Branchid"]) + ",'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," + txtReceiptNo.Text + ",'C'," + TokenNo + ",'" + dtChoosenDate.ToString("yyyy-MM-dd") + "','" + "Recd From:" + RowddlMemberName.SelectedItem.Text + " " + RowddlGroupNo.SelectedItem.Text + " For DrawNo:" + RowddlMemberName.ToolTip.ToString() + "'," + RowtxtAmount.Text + ",'" + ddlReceiptSeries.SelectedItem.Text + "','" + ddlCollected.SelectedItem.Text + "',1," + dtChoosenDate.Day + "," + dtChoosenDate.Month + "," + dtChoosenDate.Year + "," + MemberID + ",0,5," + group + ") ";
                            string strCashHeadQuery = "INSERT INTO `svcf`.`voucher` (`DualTransactionKey`,`BranchID`,`CurrDate`,`Voucher_No`,`Voucher_Type`,`Head_Id`,`ChoosenDate`,`Narration`,`Amount`,`Series`,`ReceievedBy`,`Trans_Type`,`T_Day`,`T_Month`,`T_Year`,`MemberID`,`Trans_Medium`,`RootID`,`ChitGroupId`) VALUES (" + DualTransactionKey + "," + balayer.ToobjectstrEvenNull(Session["Branchid"]) + ",'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," + txtReceiptNo.Text + ",'D',12,'" + dtChoosenDate.ToString("yyyy-MM-dd") + "','" + "Recd From:" + RowddlMemberName.SelectedItem.Text + " " + RowddlGroupNo.SelectedItem.Text + " For DrawNo:" + RowddlMemberName.ToolTip.ToString() + "'," + RowtxtAmount.Text + ",'" + ddlReceiptSeries.SelectedItem.Text + "','" + ddlCollected.SelectedItem.Text + "',1," + dtChoosenDate.Day + "," + dtChoosenDate.Month + "," + dtChoosenDate.Year + "," + MemberID + ",0,12," + group + ") ";
                            //balayer.GetInsertItem(strChitHeadQuery);
                            long strChitHead = trn.insertorupdateTrn(strChitHeadQuery);
                            //balayer.GetInsertItem(strCashHeadQuery);
                            long strCashHead = trn.insertorupdateTrn(strCashHeadQuery);

                        }
                        //   Mis amount
                        if (RowddlMisc.SelectedIndex > 0)
                        {
                            if (decimal.Parse(RowtxtMisc.Text) != 0.00M)
                            {
                                string strChitMiscHeadQuery = "INSERT INTO `svcf`.`voucher` (`DualTransactionKey`,`BranchID`,`CurrDate`,`Voucher_No`,`Voucher_Type`,`Head_Id`,`ChoosenDate`,`Narration`,`Amount`,`Series`,`ReceievedBy`,`Trans_Type`,`T_Day`,`T_Month`,`T_Year`,`MemberID`,`Trans_Medium`,`RootID`,`ChitGroupId`) VALUES (" + DualTransactionKey + "," + balayer.ToobjectstrEvenNull(Session["Branchid"]) + ",'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," + txtReceiptNo.Text + ",'C'," + RowddlMisc.SelectedItem.Value.Split(',')[1] + ",'" + dtChoosenDate.ToString("yyyy-MM-dd") + "','" + RowddlMisc.SelectedItem.Text + " Recd from " + RowddlMemberName.SelectedItem.Text + " " + RowddlGroupNo.SelectedItem.Text + "'," + RowtxtMisc.Text + ",'" + ddlReceiptSeries.SelectedItem.Text + "','" + ddlCollected.SelectedItem.Text + "',1," + dtChoosenDate.Day + "," + dtChoosenDate.Month + "," + dtChoosenDate.Year + "," + MemberID + ",0," + RowddlMisc.SelectedItem.Value.Split(',')[0] + "," + group + ") ";
                                string strCashHeadMiscQuery = "INSERT INTO `svcf`.`voucher` (`DualTransactionKey`,`BranchID`,`CurrDate`,`Voucher_No`,`Voucher_Type`,`Head_Id`,`ChoosenDate`,`Narration`,`Amount`,`Series`,`ReceievedBy`,`Trans_Type`,`T_Day`,`T_Month`,`T_Year`,`MemberID`,`Trans_Medium`,`RootID`,`ChitGroupId`) VALUES (" + DualTransactionKey + "," + balayer.ToobjectstrEvenNull(Session["Branchid"]) + ",'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," + txtReceiptNo.Text + ",'D',12,'" + dtChoosenDate.ToString("yyyy-MM-dd") + "','" + RowddlMisc.SelectedItem.Text + " Recd from " + RowddlMemberName.SelectedItem.Text + " " + RowddlGroupNo.SelectedItem.Text + "'," + RowtxtMisc.Text + ",'" + ddlReceiptSeries.SelectedItem.Text + "','" + ddlCollected.SelectedItem.Text + "',1," + dtChoosenDate.Day + "," + dtChoosenDate.Month + "," + dtChoosenDate.Year + "," + MemberID + ",0,12," + group + ") ";
                                long strChitMiscHead = trn.insertorupdateTrn(strChitMiscHeadQuery);
                                long strCashHeadMisc = trn.insertorupdateTrn(strCashHeadMiscQuery);
                            }
                        }
                    }
                    else
                    {
                        string strChitHeadQuery = "INSERT INTO `svcf`.`voucher` (`Other_Trans_Type`,`DualTransactionKey`,`BranchID`,`CurrDate`,`Voucher_No`,`Voucher_Type`,`Head_Id`,`ChoosenDate`,`Narration`,`Amount`,`Series`,`ReceievedBy`,`Trans_Type`,`T_Day`,`T_Month`,`T_Year`,`MemberID`,`Trans_Medium`,`RootID`,`ChitGroupId`) VALUES (1," + DualTransactionKey + "," + balayer.ToobjectstrEvenNull(Session["Branchid"]) + ",'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," + txtReceiptNo.Text + ",'C'," + ChitsBranchID + ",'" + dtChoosenDate.ToString("yyyy-MM-dd") + "','" + "Ref No:" + TokenNo + " |Recd From:" + RowddlMemberName.SelectedItem.Text + " " + RowddlGroupNo.SelectedItem.Text + " For DrawNo:" + RowddlMemberName.ToolTip.ToString() + " (aprox) Received By: " + balayer.ToobjectstrEvenNull(Session["BranchName"]) + "'," + RowtxtAmount.Text + ",'" + ddlReceiptSeries.SelectedItem.Text + "','" + ddlCollected.SelectedItem.Text + "',1," + dtChoosenDate.Day + "," + dtChoosenDate.Month + "," + dtChoosenDate.Year + "," + MemberID + ",0,1," + group + ")";
                        string strCashHeadQuery = "INSERT INTO `svcf`.`voucher` (`Other_Trans_Type`,`DualTransactionKey`,`BranchID`,`CurrDate`,`Voucher_No`,`Voucher_Type`,`Head_Id`,`ChoosenDate`,`Narration`,`Amount`,`Series`,`ReceievedBy`,`Trans_Type`,`T_Day`,`T_Month`,`T_Year`,`MemberID`,`Trans_Medium`,`RootID`,`ChitGroupId`) VALUES (1," + DualTransactionKey + "," + balayer.ToobjectstrEvenNull(Session["Branchid"]) + ",'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," + txtReceiptNo.Text + ",'D',12,'" + dtChoosenDate.ToString("yyyy-MM-dd") + "','" + "Ref No:" + TokenNo + " |Recd From:" + RowddlMemberName.SelectedItem.Text + " " + RowddlGroupNo.SelectedItem.Text + " For DrawNo:" + RowddlMemberName.ToolTip.ToString() + " (aprox) Received By: " + balayer.ToobjectstrEvenNull(Session["BranchName"]) + "'," + RowtxtAmount.Text + ",'" + ddlReceiptSeries.SelectedItem.Text + "','" + ddlCollected.SelectedItem.Text + "',1," + dtChoosenDate.Day + "," + dtChoosenDate.Month + "," + dtChoosenDate.Year + "," + MemberID + ",0,12," + group + ")";
                        //balayer.GetInsertItem(strChitHeadQuery);
                        //balayer.GetInsertItem(strCashHeadQuery);
                        long strChitHead = trn.insertorupdateTrn(strChitHeadQuery);
                        long strCashHead = trn.insertorupdateTrn(strCashHeadQuery);

                        if (RowddlMisc.SelectedIndex > 0)
                        {
                            string strChitmISCHeadQuery = "INSERT INTO `svcf`.`voucher` (`Other_Trans_Type`,`DualTransactionKey`,`BranchID`,`CurrDate`,`Voucher_No`,`Voucher_Type`,`Head_Id`,`ChoosenDate`,`Narration`,`Amount`,`Series`,`ReceievedBy`,`Trans_Type`,`T_Day`,`T_Month`,`T_Year`,`MemberID`,`Trans_Medium`,`RootID`,`ChitGroupId`) VALUES (1," + DualTransactionKey + "," + balayer.ToobjectstrEvenNull(Session["Branchid"]) + ",'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," + txtReceiptNo.Text + ",'C'," + ChitsBranchID + ",'" + dtChoosenDate.ToString("yyyy-MM-dd") + "','" + "Ref No:" + RowddlMisc.SelectedValue + " |Recd From:" + RowddlMemberName.SelectedItem.Text + " " + RowddlGroupNo.SelectedItem.Text + " For " + RowddlMisc.SelectedItem.Text + "  Received By: " + balayer.ToobjectstrEvenNull(Session["BranchName"]) + "'," + RowtxtMisc.Text + ",'" + ddlReceiptSeries.SelectedItem.Text + "','" + ddlCollected.SelectedItem.Text + "',1," + dtChoosenDate.Day + "," + dtChoosenDate.Month + "," + dtChoosenDate.Year + "," + MemberID + ",0,1," + group + ") ";
                            string strCashmISCHeadQuery = "INSERT INTO `svcf`.`voucher` (`Other_Trans_Type`,`DualTransactionKey`,`BranchID`,`CurrDate`,`Voucher_No`,`Voucher_Type`,`Head_Id`,`ChoosenDate`,`Narration`,`Amount`,`Series`,`ReceievedBy`,`Trans_Type`,`T_Day`,`T_Month`,`T_Year`,`MemberID`,`Trans_Medium`,`RootID`,`ChitGroupId`) VALUES (1," + DualTransactionKey + "," + balayer.ToobjectstrEvenNull(Session["Branchid"]) + ",'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," + txtReceiptNo.Text + ",'D',12,'" + dtChoosenDate.ToString("yyyy-MM-dd") + "','" + "Ref No:" + RowddlMisc.SelectedValue + " |Recd From:" + RowddlMemberName.SelectedItem.Text + " " + RowddlGroupNo.SelectedItem.Text + " For " + RowddlMisc.SelectedItem.Text + "  Received By: " + balayer.ToobjectstrEvenNull(Session["BranchName"]) + "'," + RowtxtMisc.Text + ",'" + ddlReceiptSeries.SelectedItem.Text + "','" + ddlCollected.SelectedItem.Text + "',1," + dtChoosenDate.Day + "," + dtChoosenDate.Month + "," + dtChoosenDate.Year + "," + MemberID + ",0,12," + group + ") ";
                            //balayer.GetInsertItem(strChitmISCHeadQuery);
                            //balayer.GetInsertItem(strCashmISCHeadQuery);
                            long strChitMiscHead = trn.insertorupdateTrn(strChitmISCHeadQuery);
                            long strCashHeadMisc = trn.insertorupdateTrn(strCashmISCHeadQuery);

                        }
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
                logger.Info("crr.aspx - btnConfirmationYes_click() - Completed: " + DateTime.Now + " by: " + Convert.ToString(Session["UserName"]) + "");
            }
            catch (Exception ex)
            {
                try
                {
                   trn.RollbackTrn();
                   logger.Info("CRRApply.aspx - btnConfirmationYes_click() - Error: " + ex.Message + ": " + DateTime.Now + " by: " + Convert.ToString(Session["UserName"]) + "");
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

            
            if (!Page.IsValid)
            {
                return;
            }
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                DropDownList RowddlMisc = (DropDownList)GridView1.Rows[i].FindControl("ddlMisc");
                TextBox RowtxtMisc = (TextBox)GridView1.Rows[i].FindControl("txtMisc");
                if (!string.IsNullOrEmpty(RowtxtMisc.Text))
                {
                    if (Convert.ToDecimal(RowtxtMisc.Text) > 0.00M && RowddlMisc.SelectedIndex == 0)
                    {
                        return;
                    }
                }
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
                bool keepGoing = true;
                string finishedReceiptNo = "";
                string strErrorMessage = "";
                string strExistMessage = "";
                DataTable dtAll = balayer.GetDataTable("SELECT alreadyusedreceipts,receiptnoto FROM svcf.assignreceiptbook where  moneycollid=" + ddlColloctorName.SelectedValue + "  and BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and receiptseries='" + ddlReceiptSeries.SelectedItem.Text + "' and IsFinished=0");
                for (int i = 0; i < GridView1.Rows.Count && keepGoing; i++)
                {
                    for (int j = 0; j < dtAll.Rows.Count; j++)
                    {
                        int ReceiptNo = int.Parse(txtReceiptNo.Text);
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
                                if (string.IsNullOrEmpty(strExistMessage))
                                {
                                    strErrorMessage = "";
                                    strExistMessage = "Following ReceiptNo Already Exist In Series " + ddlReceiptSeries.SelectedItem.Text + " :<br><br>" + ReceiptNo;
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
                            if (string.IsNullOrEmpty(strErrorMessage ))
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
                        string group = balayer.GetSingleValue("SELECT GroupID FROM svcf.membertogroupmaster where Head_Id="+RowddlMemberName.SelectedItem.Value);
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
                        decimal TotalPaidAmount = decimal.Parse(balayer.GetSingleValue("SELECT  ifnull( (sum(IF(Voucher_Type = 'C',Amount,0.00)) -sum(IF(Voucher_Type = 'D',Amount,0.00)) ),0.00) AS TransactionAmount FROM `svcf`.`voucher` where  Head_Id=" + RowddlMemberName.SelectedItem.Value + " and  (Trans_Type<>2) and Other_Trans_Type<>5"));
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
                            DataTable dtAuction = balayer.GetDataTable("SELECT DrawNO,CurrentDueAmount FROM `svcf`.`auctiondetails` where GroupID=" + group + " and CurrentDueAmount<>'0.00' order by DrawNO");
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
                            DataTable dtAuction = balayer.GetDataTable("SELECT DrawNO,CurrentDueAmount FROM `svcf`.`auctiondetails` where GroupID=" + group + " and CurrentDueAmount<>'0.00' order by DrawNO");
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