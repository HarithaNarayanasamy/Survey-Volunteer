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
    public partial class ReceivedAdjustments : System.Web.UI.Page
    {
        #region Object
        BusinessLayer balayer = new BusinessLayer();
        TransactionLayer trn = new TransactionLayer();
        #endregion
        ILog logger = log4net.LogManager.GetLogger(typeof(ReceivedAdjustments));
       
        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (Session["UserName"] == null || Session["Branchid"] == null || Session["BranchName"] == null)
            {
                Response.Redirect(Page.ResolveUrl("~/Login.aspx"), true);
            }
        }
        protected void btnAcceptCancel_Click(object sender, EventArgs e)
        {
        }
        int transtype;
        string strOther;
        string strTrans;
        protected void btnAcceptOK_Click(object sender, EventArgs e)
        {
            Page.Validate("Rej");
         
          
            if (!Page.IsValid)
            {
                return;
            }

            if (Session["CheckRefresh"].ToString() != ViewState["CheckRefresh"].ToString())
            {
                return;
            }

            bool isFinished = true;
          //  TransactionLayer trn = new TransactionLayer();
            try
            {
                DataTable dtConfirmation = new DataTable();
                dtConfirmation.Columns.Add("Heads");
                dtConfirmation.Columns.Add("Amount");
                decimal totalright = 0.0M;
                decimal totalleft = 0.0M;
                decimal total = Convert.ToDecimal(lblAmount.Text);
                string error = "";
                string errorDebit = "";
                int TotalNoofRows = GridGuardians.Rows.Count + GridGuardiansDebit.Rows.Count;
                for (int iRC = 0; iRC < TotalNoofRows; iRC++)
                {
                    dtConfirmation.Rows.Add();
                }
                System.Guid guid = Guid.NewGuid();
                // Prepare GUID values in SQL format
                string hexstring = BitConverter.ToString(guid.ToByteArray());
                string guidForBinary16 = "0x" + hexstring.Replace("-", string.Empty);
                string DualTransactionKey = guidForBinary16;

             
                

                if (GridGuardiansDebit.Visible)
                {

                    //DropDownList ddlCredit = new DropDownList();
                    //if (Convert.ToInt32(ddlCredit.SelectedValue.Split(',')[0]) == 3)
                    //{
                    //    strTrans = "1";
                    //}
                    //if (Convert.ToInt32(ddlCredit.SelectedValue.Split(',')[0]) == 1)
                    //{
                    //    strOther = "1";
                    //}

                    for (int i = 0; i < GridGuardiansDebit.Rows.Count; i++)
                    {
                        TextBox txtsubAmount = (TextBox)GridGuardiansDebit.Rows[i].FindControl("txtAmount");
                        dtConfirmation.Rows[i][1] = txtsubAmount.Text;
                        dtConfirmation.Rows[i][0] = ((DropDownList)GridGuardiansDebit.Rows[i].FindControl("ddlCredit")).SelectedValue;
                       // transtype = Convert.ToInt32(dtConfirmation.Rows[i][0]);  
                        errorDebit += txtsubAmount.Text + " + ";
                        totalright += decimal.Parse(txtsubAmount.Text.Trim());
                    }




                    if ((Convert.ToDecimal(lblAmount.Text)) == totalright)
                    {
                        for (int iRow = 0; iRow < GridGuardiansDebit.Rows.Count; iRow++)
                        {
                            TextBox txtsubAmount = (TextBox)GridGuardiansDebit.Rows[iRow].FindControl("txtAmount");
                            dtConfirmation.Rows[iRow][1] = txtsubAmount.Text;
                            dtConfirmation.Rows[iRow][0] = ((DropDownList)GridGuardiansDebit.Rows[iRow].FindControl("ddlCredit")).SelectedValue;



                            if (Convert.ToInt32(((DropDownList)GridGuardiansDebit.Rows[iRow].FindControl("ddlCredit")).SelectedValue.Split(',')[0]) == 3)
                            {
                                strTrans = "1";
                            }
                            else
                            {
                                strTrans = "0";
                            }
                            if (Convert.ToInt32(((DropDownList)GridGuardiansDebit.Rows[iRow].FindControl("ddlCredit")).SelectedValue.Split(',')[0]) == 1)
                            {
                                strOther = "1";
                            }
                            else
                            {
                                strOther = "0";
                            }


                            string strChitHeadQuery = "INSERT INTO `voucher` (`DualTransactionKey`,`BranchID`,`CurrDate`,`Voucher_No`,`Voucher_Type`,`Head_Id`,`ChoosenDate`,`Narration`,`Amount`,`Series`,`ReceievedBy`,`Trans_Type`,`T_Day`,`T_Month`,`T_Year`,`MemberID`,`Trans_Medium`,`RootID`,`ChitGroupId`,Other_Trans_Type) VALUES (" + DualTransactionKey + "," + balayer.ToobjectstrEvenNull(Session["Branchid"]) + ",'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," + 0 + ",'C','" + Convert.ToString(dtConfirmation.Rows[iRow][0]).Split(',')[1] + "','" + Convert.ToDateTime(txtDate.Text).ToString("yyyy-MM-dd") + "','" + balayer.ToobjectstrEvenNull(txtDesc.Text) + "','" + dtConfirmation.Rows[iRow][1] + "','ADVICE','" + balayer.ToobjectstrEvenNull(Session["UserName"]) + "',0," + Convert.ToDateTime(txtDate.Text).Day + "," + Convert.ToDateTime(txtDate.Text).Month + "," + Convert.ToDateTime(txtDate.Text).Year + "," + 0 + ",'" + strTrans + "','" + Convert.ToString(dtConfirmation.Rows[iRow][0]).Split(',')[0] + "'," + 0 + ",'" + strOther + "')";

                            string strCashHeadQuery = "INSERT INTO `voucher` (`DualTransactionKey`,`BranchID`,`CurrDate`,`Voucher_No`,`Voucher_Type`,`Head_Id`,`ChoosenDate`,`Narration`,`Amount`,`Series`,`ReceievedBy`,`Trans_Type`,`T_Day`,`T_Month`,`T_Year`,`MemberID`,`Trans_Medium`,`RootID`,`ChitGroupId`,Other_Trans_Type) VALUES (" + DualTransactionKey + "," + balayer.ToobjectstrEvenNull(Session["Branchid"]) + ",'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," + 0 + ",'D','" + lblDebitID.Text + "','" + Convert.ToDateTime(txtDate.Text).ToString("yyyy-MM-dd") + "','" + balayer.ToobjectstrEvenNull(txtDesc.Text) + "','" + dtConfirmation.Rows[iRow][1] + "','ADVICE','" + balayer.ToobjectstrEvenNull(Session["UserName"]) + "',0," + Convert.ToDateTime(txtDate.Text).Day + "," + Convert.ToDateTime(txtDate.Text).Month + "," + Convert.ToDateTime(txtDate.Text).Year + "," + 0 + ",'" + strTrans + "'," + 1 + "," + 0 + ",2)";
                  
                            string strUpdateHeadQuery = "update  `voucher` set Other_Trans_Type=2 where TransactionKey=" + lblDual.Text;
                            long iResultDebit = trn.insertorupdateTrn(strChitHeadQuery);
                            long iResultCredit = trn.insertorupdateTrn(strCashHeadQuery);
                            long uResult = trn.insertorupdateTrn(strUpdateHeadQuery);

                        }

                    }

                    else
                    {
                        //lblContent.Text = "Your amount is not tally please check it out";
                        //lblContent.ForeColor = System.Drawing.Color.Green;

                        lblT.Text = "Status";
                        lblerror.Text = "Your amount is not tally please check it out";
                        lblerror.ForeColor = System.Drawing.Color.Green;
                        ModalPopupExtender1.PopupControlID = "PnlProvide";
                        ModalPopupExtender1.Show();
                        PnlProvide.Visible = true;
                        isFinished = false;

                    }

                   
                }
                else
                {
                    //DropDownList ddlDebit = new DropDownList();
                    //if (Convert.ToInt32(ddlDebit.SelectedValue.Split(',')[0]) == 3)
                    //{
                    //    strTrans = "1";
                    //}
                    //if (Convert.ToInt32(ddlDebit.SelectedValue.Split(',')[0]) == 1)
                    //{
                    //    strOther = "1";
                    //}
                    for (int i = 0; i < GridGuardians.Rows.Count; i++)
                    {
                        TextBox txtsubAmount = (TextBox)GridGuardians.Rows[i].FindControl("txtAmountDebit");
                        dtConfirmation.Rows[i][1] = txtsubAmount.Text;
                        error += txtsubAmount.Text + " + ";
                        totalleft += decimal.Parse(txtsubAmount.Text.Trim());
                        dtConfirmation.Rows[i][0] = ((DropDownList)GridGuardians.Rows[i].FindControl("ddlDebit")).SelectedValue;
             
                                  
                    }

                  

                    if ((Convert.ToDecimal(lblAmount.Text)) == totalleft)
                    {
                        for (int iRow = 0; iRow < GridGuardians.Rows.Count; iRow++)
                        {

                            TextBox txtsubAmount = (TextBox)GridGuardians.Rows[iRow].FindControl("txtAmountDebit");
                            dtConfirmation.Rows[iRow][1] = txtsubAmount.Text;
                            dtConfirmation.Rows[iRow][0] = ((DropDownList)GridGuardians.Rows[iRow].FindControl("ddlDebit")).SelectedValue;
                           
                            if (Convert.ToInt32(((DropDownList)GridGuardians.Rows[iRow].FindControl("ddlDebit")).SelectedValue.Split (',')[0]) == 3)
                            {
                                strTrans = "1";
                            }
                            else
                            {
                                strTrans = "0";
                            }
                            if (Convert.ToInt32(((DropDownList)GridGuardians.Rows[iRow].FindControl("ddlDebit")).SelectedValue.Split(',')[0]) == 1)
                
                            {
                                strOther = "1";
                            }
                            else
                            {
                                strOther = "0";
                            }

                            string strChitHeadQuery = "INSERT INTO `voucher` (`DualTransactionKey`,`BranchID`,`CurrDate`,`Voucher_No`,`Voucher_Type`,`Head_Id`,`ChoosenDate`,`Narration`,`Amount`,`Series`,`ReceievedBy`,`Trans_Type`,`T_Day`,`T_Month`,`T_Year`,`MemberID`,`Trans_Medium`,`RootID`,`ChitGroupId`,Other_Trans_Type) VALUES (" + DualTransactionKey + "," + balayer.ToobjectstrEvenNull(Session["Branchid"]) + ",'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," + 0 + ",'C','" + lblCreditID.Text + "','" + Convert.ToDateTime(txtDate.Text).ToString("yyyy-MM-dd") + "','" + balayer.ToobjectstrEvenNull(txtDesc.Text) + "','" + dtConfirmation.Rows[iRow][1] + "','ADVICE','" + balayer.ToobjectstrEvenNull(Session["UserName"]) + "',0," + Convert.ToDateTime(txtDate.Text).Day + "," + Convert.ToDateTime(txtDate.Text).Month + "," + Convert.ToDateTime(txtDate.Text).Year + "," + 0 + "," + strTrans + "," + 1 + "," + 0 + ",2)";
                            string strCashHeadQuery = "INSERT INTO `voucher` (`DualTransactionKey`,`BranchID`,`CurrDate`,`Voucher_No`,`Voucher_Type`,`Head_Id`,`ChoosenDate`,`Narration`,`Amount`,`Series`,`ReceievedBy`,`Trans_Type`,`T_Day`,`T_Month`,`T_Year`,`MemberID`,`Trans_Medium`,`RootID`,`ChitGroupId`,Other_Trans_Type) VALUES (" + DualTransactionKey + "," + balayer.ToobjectstrEvenNull(Session["Branchid"]) + ",'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," + 0 + ",'D','" + Convert.ToString(dtConfirmation.Rows[iRow][0]).Split(',')[1] + "','" + Convert.ToDateTime(txtDate.Text).ToString("yyyy-MM-dd") + "','" + balayer.ToobjectstrEvenNull(txtDesc.Text) + "','" + dtConfirmation.Rows[iRow][1] + "','ADVICE','" + balayer.ToobjectstrEvenNull(Session["UserName"]) + "',0," + Convert.ToDateTime(txtDate.Text).Day + "," + Convert.ToDateTime(txtDate.Text).Month + "," + Convert.ToDateTime(txtDate.Text).Year + "," + 0 + "," + strTrans + ",'" + Convert.ToString(dtConfirmation.Rows[iRow][0]).Split(',')[0] + "'," + 0 + ",'" + strOther + "')";
                            string strUpdateHeadQuery = "update  `voucher` set Other_Trans_Type=2 where TransactionKey=" + lblDual.Text;
                            long iResultDebit = trn.insertorupdateTrn(strChitHeadQuery);
                            long iResultCredit = trn.insertorupdateTrn(strCashHeadQuery);
                            long uResult = trn.insertorupdateTrn(strUpdateHeadQuery);
                        }
                       
                    }

                    else
                    {
                        //lblContent.Text = "Your amount is not tally please check it out";
                        //lblContent.ForeColor = System.Drawing.Color.Green;

                        lblT.Text = "Status";
                        lblerror .Text = "Your amount is not tally please check it out";
                        lblerror.ForeColor = System.Drawing.Color.Green;
                        ModalPopupExtender1.PopupControlID = "PnlProvide";
                        ModalPopupExtender1.Show();
                        PnlProvide.Visible = true;
                        isFinished = false;


                    }
                 
                }
                //PnlProvide.Visible = true;
                trn.CommitTrn();
                logger.Info("ReceivedAdjustments.aspx - btnAcceptOK_click():  Completed: " + DateTime.Now + " by: " + Convert.ToString(Session["UserName"]) + "");
            }
            catch (Exception ex)
            {
                try
                {
                    trn.RollbackTrn();
                    logger.Info("ReceivedAdjustments.aspx - btnAcceptOK_click():  Error: " + ex.Message + ": " + DateTime.Now + " by: " + Convert.ToString(Session["UserName"]) + "");
                }
                catch { }
                finally
                {
                    lblT.Text = "Status";
                    lblerror.Text = "Your amount is not tally please check it out";
                    lblerror.ForeColor = System.Drawing.Color.Green;
                    ModalPopupExtender1.PopupControlID = "PnlProvide";
                    ModalPopupExtender1.Show();
                    PnlProvide.Visible = true;
                    isFinished = false;
                }
            }
            finally
            {
                trn.DisposeTrn();
                if (isFinished == true)
                {
                    Response.Redirect(Request.Url.AbsolutePath.ToString());
                }
            }
        }
        protected void btnMsgCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.Url.AbsolutePath);
        }
        protected void btnMsgOK_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.Url.AbsolutePath);
        }
        protected void Approve_Click(object sender, EventArgs e)
        {
            this.BindData();
            SetDefaultRowCreditHeadt();
            this.BindDataDebit();
            SetDefaultRowDebit();
            SetGridTabIndex(true);
            SetGridTabIndex(false);
            ImageButton btndetails = sender as ImageButton;
            GridViewRow gvRow = (GridViewRow)btndetails.NamingContainer;
            string Amount = balayer.ToobjectstrEvenNull(GridView1.DataKeys[gvRow.RowIndex]["Amount"]);
            string BranchHead = balayer.ToobjectstrEvenNull(GridView1.DataKeys[gvRow.RowIndex]["CollectedBranchID"]);
            string DualTransactionKey = balayer.ToobjectstrEvenNull(GridView1.DataKeys[gvRow.RowIndex]["DualTransactionKey"]);
            string TransactionKey = balayer.ToobjectstrEvenNull(GridView1.DataKeys[gvRow.RowIndex]["TransactionKey"]);
            string Narration = balayer.ToobjectstrEvenNull(GridView1.DataKeys[gvRow.RowIndex]["Description"]);
            string VoucherType = balayer.ToobjectstrEvenNull(GridView1.DataKeys[gvRow.RowIndex]["Voucher_Type"]);
            DateTime dtChoosenDate = DateTime.Parse(balayer.ToobjectstrEvenNull(GridView1.DataKeys[gvRow.RowIndex]["ChoosenDate"]));
            if (VoucherType.ToLower() == "c")
            {
                lblDebit.Text = balayer.ToobjectstrEvenNull(GridView1.DataKeys[gvRow.RowIndex]["BranchName"]);
                lblDebitID.Text = balayer.ToobjectstrEvenNull(GridView1.DataKeys[gvRow.RowIndex]["CollectedBranchID"]);
                lbldebitamount.Text = Amount;
                lblAmount.Text = Amount;
                txtDesc.Text = Narration;
                lblDual.Text = TransactionKey;
                GridGuardians.Visible = false;
                // CreditHead.Visible = false;
                CreditID.Visible = false;
                //DebitID.Visible = true;
                DebitHead.Visible = true;
                GridGuardiansDebit.Visible = true;
            }
            else
            {
                lblCredit.Text = balayer.ToobjectstrEvenNull(GridView1.DataKeys[gvRow.RowIndex]["BranchName"]);
                lblCreditID.Text = balayer.ToobjectstrEvenNull(GridView1.DataKeys[gvRow.RowIndex]["CollectedBranchID"]);
                lblAmount.Text = Amount;
                lbldebitamount.Text = Amount;
                txtDesc.Text = Narration;
                lblDual.Text = TransactionKey;
                CreditID.Visible = true;
                //DebitID.Visible = false;
                DebitHead.Visible = false;
                GridGuardians.Visible = true;
                GridGuardiansDebit.Visible = false;
            }
            lblT.Text = "Status";
            lblContent.Text = "Advice Accepted!!!";
            lblContent.ForeColor = System.Drawing.Color.Green;
            ModalPopupExtender1.PopupControlID = "PnlProvide";
            ModalPopupExtender1.Show();
            PnlProvide.Visible = true;
        }
        protected void Page_PreRender(object sender, EventArgs e)
        {
            ViewState["CheckRefresh"] = Session["CheckRefresh"];
        }
        protected DataTable GetData(int iFlag)
        {
            return balayer.GetDataTable("SELECT t2.Node as BranchName,(case when(Trans_Medium=0) then 'Cash' else 'Cheque' end) as Type, date_format( t1.ChoosenDate,'%d/%m/%Y') as ChoosenDate,t1.Amount,t1.Narration as Description,insertkey_from_bin(t1.DualTransactionKey) as DualTransactionKey,t1.TransactionKey,t1.BranchID as CollectedBranchID,t1.Voucher_Type FROM `svcf`.`voucher` as t1 left join headstree as t2  on  t1.BranchID=t2.NodeID  where t1.Trans_Type=0 and  t1.Other_Trans_Type=1 and t1.Head_Id=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + "");
        }
        protected void Ib_Load(object sender, EventArgs e)
        {
            ImageButton btn = (ImageButton)sender;
            this.ToolkitScriptManager1.RegisterAsyncPostBackControl(btn);

        }
        protected void btnLoad_Click(object sender, EventArgs e)
        {
            DataTable dtAcc = null;
            if (ddlStatus.SelectedIndex == 0)
            {
                dtAcc = GetData(1);
            }
            else if (ddlStatus.SelectedIndex == 1)
            {
                dtAcc = GetData(2);
            }
            DataTable dtCloned = dtAcc.Clone();
            dtCloned.Columns["ChoosenDate"].DataType = typeof(string);
            foreach (DataRow row in dtAcc.Rows)
            {
                dtCloned.ImportRow(row);
            }
            GridView1.DataSource = dtCloned;
            GridView1.DataBind();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["CheckRefresh"] = System.Guid.NewGuid().ToString();
                DataTable dtAcc = GetData(1);
                DataTable dtCloned = dtAcc.Clone();
                dtCloned.Columns["ChoosenDate"].DataType = typeof(string);
                ViewState["tabnum"] = "57";
                //dtCloned.Columns["ReceiptDate"].DataType = typeof(string);
                foreach (DataRow row in dtAcc.Rows)
                {
                    dtCloned.ImportRow(row);
                }
                rvDate.MaximumValue = DateTime.Now.ToString("dd/MM/yyyy");
                rvDate.MinimumValue = balayer.GetSingleValue("SELECT DATE_FORMAT( MinimumDate, '%d/%m/%Y') MinimumDate FROM svcf.restrictionmaster where BranchID=" + Session["Branchid"]); ;
                GridView1.DataSource = dtCloned;
                GridView1.DataBind();
            }
            
        }
        protected void GridGuardians_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DropDownList ddlDebit = (DropDownList)e.Row.FindControl("ddlDebit");
                if (e.Row.RowIndex > 0)
                {
                    DataTable dtAllHeads = (DataTable)((DropDownList)GridGuardians.Rows[0].FindControl("ddlDebit")).DataSource;
                    ddlDebit.DataSource = dtAllHeads;
                    ddlDebit.DataTextField = "TREE";
                    ddlDebit.DataValueField = "ID";
                    ddlDebit.DataBind();
               }
                else
                {
                    bindHeads(ddlDebit);
                }
                ddlDebit.Items.FindByText(((Label)e.Row.FindControl("lblDebit1")).Text).Selected = true;
            }
            else if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ImageButton imgBtnRemove = (e.Row.FindControl("imgBtnRemove") as ImageButton);
                ToolkitScriptManager1.RegisterAsyncPostBackControl(imgBtnRemove);
            }
        }
        protected void btnAdd_GridGuardians_RowCommand_click(object sender, ImageClickEventArgs e)
        {
            Page.Validate("Rej");
            if (!Page.IsValid)
            {
                return;
            }
            GridViewRow gridRow = (GridViewRow)((ImageButton)sender).Parent.Parent;
            int RowIndex = gridRow.RowIndex;
            DataTable dtDeb = new DataTable();
            DataColumn dcHead = new DataColumn("Heads", typeof(string));
            DataColumn dcAmount = new DataColumn("Amount", typeof(string));
            dtDeb.Columns.Add(dcHead);
            dtDeb.Columns.Add(dcAmount);
            foreach (GridViewRow dr in GridGuardians.Rows)
            {
                DataRow dtDbrow = dtDeb.NewRow();
                DropDownList ddlDebit = (DropDownList)dr.FindControl("ddlDebit");
                dtDbrow["Heads"] = ddlDebit.SelectedItem.Text;
                TextBox txtAmountDebit = (TextBox)dr.FindControl("txtAmountDebit");
                dtDbrow["Amount"] = txtAmountDebit.Text;
                dtDeb.Rows.Add(dtDbrow);
            }
            DataRow newRow = dtDeb.NewRow();
            newRow["Amount"] = "";
            newRow["Heads"] = "--Select--";
            dtDeb.Rows.Add(newRow);
            GridGuardians.DataSource = dtDeb;
            GridGuardians.DataBind();
            // ViewState["tabnum"] = (57 + GridGuardians.Rows.Count * 4);
            SetGridTabIndex(true);

        }

        protected void btnAdd_GridGuardiansDebit_RowCommand_click(object sender, ImageClickEventArgs e)
        {
            Page.Validate("Rej");

            if (!Page.IsValid)
            {
                return;
            }
            GridViewRow gridRow = (GridViewRow)((ImageButton)sender).Parent.Parent;
            DataTable dtDeb = new DataTable();
            DataColumn dcHead = new DataColumn("Heads", typeof(string));
            DataColumn dcAmount = new DataColumn("Amount", typeof(string));
            dtDeb.Columns.Add(dcHead);
            dtDeb.Columns.Add(dcAmount);
            foreach (GridViewRow dr in GridGuardiansDebit.Rows)
            {
                DataRow dtDbrow = dtDeb.NewRow();
                DropDownList ddlCredit = (DropDownList)dr.FindControl("ddlCredit");
                dtDbrow["Heads"] = ddlCredit.SelectedItem.Text;
                TextBox txtAmount = (TextBox)dr.FindControl("txtAmount");
                dtDbrow["Amount"] = txtAmount.Text;
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
        public void SetDefaultRowCreditHeadt()
        {
            DataTable dt = new DataTable();
            DataColumn dcHeads = new DataColumn("Heads", typeof(string));
            DataColumn dcAmount = new DataColumn("Amount", typeof(string));
            dt.Columns.Add(dcAmount);
            dt.Columns.Add(dcHeads);
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
            dt.Columns.Add(dcAmount);
            dt.Columns.Add(dcHeads);
            DataRow newRow = dt.NewRow();
            newRow["Heads"] = "--Select--";
            newRow["Amount"] = "";
            dt.Rows.Add(newRow);
            GridGuardiansDebit.DataSource = dt;
            GridGuardiansDebit.DataBind();
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
                    DropDownList ddlDebit = (DropDownList)dr.FindControl("ddlDebit");
                    TextBox txtAmountDebit = (TextBox)dr.FindControl("txtAmountDebit");
                    ddlDebit.TabIndex = ++currentTabIndex;
                    txtAmountDebit.TabIndex = ++currentTabIndex;

                }
            }

            else
            {
                GridView theGridView = GridGuardiansDebit;
                currentTabIndex = (short)((GridGuardians.Rows.Count * 4) + 9);
                foreach (GridViewRow dr in theGridView.Rows)
                {
                    ImageButton showbutton = (ImageButton)dr.FindControl("imgBtnAddDebit");
                    ImageButton showRemovebutton = (ImageButton)dr.FindControl("imgBtnRemoveDebit");
                    DropDownList ddlCredit = (DropDownList)dr.FindControl("ddlCredit");
                    TextBox txtAmount = (TextBox)dr.FindControl("txtAmount");
                    ddlCredit.TabIndex = ++currentTabIndex;
                    txtAmount.TabIndex = ++currentTabIndex;
                }
            }
        }

        protected void bindHeads(DropDownList ddlDebit)
        {
            DataTable dtAllHeads = balayer.GetDataTable("SELECT concat(cast(  v1.`RootID` as char),',',cast(v1.`TreeID` as char)) as ID,v1.`TREE` FROM `svcf`.`view_parent` as v1  where v1.BranchID is null or v1.BranchID=" + Session["Branchid"]);
            DataRow dr = dtAllHeads.NewRow();
            dr[0] = "--Select--";
            dr[1] = "--Select--";
            dtAllHeads.Rows.InsertAt(dr, 0);
            ddlDebit.DataSource = dtAllHeads;
            ddlDebit.DataTextField = "TREE";
            ddlDebit.DataValueField = "ID";
            ddlDebit.DataBind();
        }

        protected void bindHeadsDebit(DropDownList ddlCredit)
        {
            DataTable dtAllHeads = balayer.GetDataTable("SELECT concat(cast(  v1.`RootID` as char),',',cast(v1.`TreeID` as char)) as ID,v1.`TREE` FROM `svcf`.`view_parent` as v1  where v1.BranchID is null or v1.BranchID=" + Session["Branchid"]);
            DataRow dr = dtAllHeads.NewRow();
            dr[0] = "--Select--";
            dr[1] = "--Select--";
            dtAllHeads.Rows.InsertAt(dr, 0);
            ddlCredit.DataSource = dtAllHeads;
            ddlCredit.DataTextField = "TREE";
            ddlCredit.DataValueField = "ID";
            ddlCredit.DataBind();
        }




        protected void GridGuardiansDebit_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DropDownList ddlCredit = (DropDownList)e.Row.FindControl("ddlCredit");
                if (e.Row.RowIndex > 0)
                {
                    DataTable dtAllHeads = (DataTable)((DropDownList)GridGuardiansDebit.Rows[0].FindControl("ddlCredit")).DataSource;
                    ddlCredit.DataSource = dtAllHeads;
                    ddlCredit.DataTextField = "TREE";
                    ddlCredit.DataValueField = "ID";
                    ddlCredit.DataBind();
                }
                else
                {
                    bindHeadsDebit(ddlCredit);
                }
                ddlCredit.Items.FindByText(((Label)e.Row.FindControl("lblCredit1")).Text).Selected = true;

            }
            else if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ImageButton imgBtnAddDebit = (e.Row.FindControl("imgBtnAddDebit") as ImageButton);
                ImageButton imgBtnRemoveDebit = (e.Row.FindControl("imgBtnRemoveDebit") as ImageButton);

                ToolkitScriptManager1.RegisterAsyncPostBackControl(imgBtnRemoveDebit);
            }
        }

        private void BindData()
        {
            DataTable dtSelect = new DataTable();
            DataTable dt = new DataTable();
            DataColumn dcHeads = new DataColumn("Heads", typeof(string));
            DataColumn dcAmount = new DataColumn("Amount", typeof(string));
            dt.Columns.Add(dcAmount);
            dt.Columns.Add(dcHeads);
            GridGuardians.DataSource = dt;
            GridGuardians.DataBind();
        }
        private void BindDataDebit()
        {
            DataTable dtSelect = new DataTable();
            DataTable dt = new DataTable();
            DataColumn dcHeads = new DataColumn("Heads", typeof(string));
            DataColumn dcAmount = new DataColumn("Amount", typeof(string));
            dt.Columns.Add(dcAmount);
            dt.Columns.Add(dcHeads);
            GridGuardiansDebit.DataSource = dt;
            GridGuardiansDebit.DataBind();
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
            dt1.Columns.Add(dcHead);
            dt1.Columns.Add(dcAmount);
            short i = 0;
            foreach (GridViewRow dr in GridGuardians.Rows)
            {
                i += 1;
                DataRow dtrow = dt1.NewRow();
                DropDownList ddlDebit = (DropDownList)dr.FindControl("ddlDebit");
                dtrow["Heads"] = ddlDebit.SelectedItem.Text;
                TextBox txtAmountDebit = (TextBox)dr.FindControl("txtAmountDebit");
                dtrow["Amount"] = txtAmountDebit.Text;
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
            dt1.Columns.Add(dcHead);
            dt1.Columns.Add(dcAmount);
            dt1.Columns.Add(ShowRemoveButton);
            dt1.Columns.Add(ShowButton);
            short i = 0;
            foreach (GridViewRow dr in GridGuardiansDebit.Rows)
            {
                i += 1;
                DataRow dtrow = dt1.NewRow();
                DropDownList ddlCredit = (DropDownList)dr.FindControl("ddlCredit");
                dtrow["Heads"] = ddlCredit.SelectedItem.Text;
                TextBox txtAmount = (TextBox)dr.FindControl("txtAmount");
                dtrow["Amount"] = txtAmount.Text;
                dt1.Rows.Add(dtrow);
            }
            dt1.Rows.RemoveAt(GridGuardiansDebit.Rows.Count - 1);
            GridGuardiansDebit.DataSource = dt1;
            GridGuardiansDebit.DataBind();
            SetGridTabIndex(false);
        }
    }
}