using AjaxControlToolkit;
using SVCF_BusinessAccessLayer;
using SVCF_TransactionLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using log4net;
using log4net.Config;

namespace SreeVisalamChitFundLtd_phase1
{
    public partial class SalaryAdvices : System.Web.UI.Page
    {
        #region VarDeclaration
        BusinessLayer balayer = new BusinessLayer();
        TransactionLayer tranlayer = new TransactionLayer();
        ILog logger = log4net.LogManager.GetLogger(typeof(SalaryAdvices));
        int hdid = 0;
        #endregion
    
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                string Branchid = Convert.ToString(Session["Branchid"]);
            //    string BranchName = Convert.ToString(Session["BranchName"]);
                DataTable dtsadv = GetData(1);
                DataTable dtsadvclone = dtsadv.Clone();
                dtsadvclone.Columns["Date"].DataType = typeof(string);
                foreach (DataRow row in dtsadv.Rows)
                {
                    dtsadvclone.ImportRow(row);
                }
                GridView1.DataSource = dtsadvclone;
                GridView1.DataBind();
            }
        }
        private void FillDropDownList(DropDownList ddl, int iType, string MemberID)
        {  
            if (iType == 2)
            {
                ddl.DataSource = null;
                DataTable dtgroupno = null;
                dtgroupno = balayer.GetDataTable("SELECT cast(TreeID as char) as TreeID,TREE FROM svcf.view_parent where RootID=11 and (BranchID is null or BranchID=" + Session["Branchid"] + ")");
                // dtgroupno = balayer.GetDataTable("SELECT concat(cast(TreeID as char),',',cast(RootID as char)) as TreeID,TREE FROM svcf.view_parent where RootID = 11 and (BranchID is null or BranchID=" + Session["Branchid"] + ")");
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

        protected void btnLoad_Click(object sender, EventArgs e)
        {
            DataTable dtAcc = null;
            if (ddlStatus.SelectedIndex == 0)
            {
                //Waiting
                dtAcc = GetData(1);
            }
            else if (ddlStatus.SelectedIndex == 1)
            {
                //Accepted
                dtAcc = GetData(2);
            }
            DataTable dtCloned = dtAcc.Clone();
            dtCloned.Columns["Date"].DataType = typeof(string);
            foreach (DataRow row in dtAcc.Rows)
            {
                dtCloned.ImportRow(row);
            }
            GridView1.DataSource = dtCloned;
            GridView1.DataBind();
        }
        public DataTable GetData(int IFLAG)
        {
            string Branchid = Convert.ToString(Session["Branchid"]);
            return balayer.GetDataTable(@"select BranchID,TransactionKey,DualTransactionKey,Voucher_No,ChoosenDate as Date ,ReceievedBy as StaffName,Amount as Amount, SUBSTRING_INDEX(Narration, '#', 1) AS Narration,
               SUBSTRING_INDEX(SUBSTRING_INDEX(Narration, '#', 2), '#', -1) AS ApprovedNumber,
               SUBSTRING_INDEX(SUBSTRING_INDEX(Narration, '#', 3), '#', -1) AS ApprovedDate from voucher  where RootID=1  and Voucher_Type='D' and Series='Salary' and Head_Id='" + Branchid + "' and Other_Trans_Type=" + IFLAG + ";");
        }

        protected void Approve_Click(object sender, EventArgs e)
        {           
            bool isFinished = true;
            try
            {                
                ImageButton btndetails = sender as ImageButton;
                GridViewRow gvRow = (GridViewRow)btndetails.NamingContainer;             
                string TransactionKey = balayer.ToobjectstrEvenNull(GridView1.DataKeys[gvRow.RowIndex]["TransactionKey"]);
                string VoucherNo = balayer.ToobjectstrEvenNull(GridView1.DataKeys[gvRow.RowIndex]["Voucher_No"]);
                string creditbr_id = balayer.ToobjectstrEvenNull(GridView1.DataKeys[gvRow.RowIndex]["BranchID"]);
                txtBranchName.Text = balayer.GetSingleValue("select Node from svcf.headstree where parentid=1 and NodeID=" + creditbr_id + "");
                lblTransactionkey.Text = TransactionKey;
                lblCrBranchId.Text = creditbr_id;
                lblNarration.Text = balayer.ToobjectstrEvenNull(GridView1.DataKeys[gvRow.RowIndex]["Narration"]);
                lblamount.Text = balayer.ToobjectstrEvenNull(GridView1.DataKeys[gvRow.RowIndex]["Amount"]);
                lblStaffname.Text = balayer.ToobjectstrEvenNull(GridView1.DataKeys[gvRow.RowIndex]["StaffName"]);
                FillDropDownList(accept_ddlMisc, 2, "");
            }
            catch
            {
                try
                {
                    tranlayer.RollbackTrn();
                }
                catch { }
                finally
                {

                    ModalPopupExtender1.PopupControlID = "PnlProvide";
                    ModalPopupExtender1.Show();
                    PnlApprove.Visible = true;
                    isFinished = false;
                }
            }
            finally
            {
                tranlayer.DisposeTrn();
                if (isFinished == true)
                {

                    ModalPopupExtender1.PopupControlID = "PnlProvide";
                    ModalPopupExtender1.Show();
                    PnlApprove.Visible = true;
                    isFinished = false;
                }
            }
        }

        protected void btnAcceptCancel_Click(object sender, EventArgs e)
        {
            ModalPopupExtender1.Hide();
        }


        protected void btnAcceptOK_Click(object sender, EventArgs e)
        {
            bool isFinished = true;
         
            // Transcation trn = new Transcation(true);
            try
            {
                System.Guid guid = Guid.NewGuid();
                // Prepare GUID values in SQL format
                string hexstring = BitConverter.ToString(guid.ToByteArray());
                string guidForBinary16 = "0x" + hexstring.Replace("-", string.Empty);
                string DualTransactionKey = guidForBinary16;
                string receipt_no = Convert.ToString(ViewState["Receipt_No"]);
                //txtDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                DateTime dtChoosenDate = DateTime.Parse(txtDate.Text);

                if (accept_ddlMisc.SelectedItem != null && accept_ddlMisc.SelectedItem.Text != "--Select--")
                {
                    string strChitHeadQuery = "INSERT INTO `voucher` (`DualTransactionKey`,`BranchID`,`CurrDate`,`Voucher_No`,`Voucher_Type`,`Head_Id`,`ChoosenDate`,`Narration`,`Amount`,`Series`,`ReceievedBy`,`Trans_Type`,`T_Day`,`T_Month`,`T_Year`,`MemberID`,`Trans_Medium`,`RootID`,`ChitGroupId`,Other_Trans_Type) VALUES (" + DualTransactionKey + "," + balayer.ToobjectstrEvenNull(Session["Branchid"]) + ",'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + receipt_no + "','D'," + lblCrBranchId.Text + ",'" + balayer.changedateformat(Convert.ToDateTime(txtDate.Text), 2) + "','" + lblNarration.Text + "'," + lblamount.Text + ",'SALARY','" + lblStaffname.Text + "',1," + dtChoosenDate.Day + "," + dtChoosenDate.Month + "," + dtChoosenDate.Year + "," + 0 + "," + "2" + ",11," + 0 + ",2)";
                    string strCashHeadQuery = "INSERT INTO `voucher` (`DualTransactionKey`,`BranchID`,`CurrDate`,`Voucher_No`,`Voucher_Type`,`Head_Id`,`ChoosenDate`,`Narration`,`Amount`,`Series`,`ReceievedBy`,`Trans_Type`,`T_Day`,`T_Month`,`T_Year`,`MemberID`,`Trans_Medium`,`RootID`,`ChitGroupId`,Other_Trans_Type) VALUES (" + DualTransactionKey + "," + balayer.ToobjectstrEvenNull(Session["Branchid"]) + ",'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + receipt_no + "','C'," + accept_ddlMisc.SelectedValue + ",'" + balayer.changedateformat(Convert.ToDateTime(txtDate.Text), 2) + "','" + lblNarration.Text + "'," + lblamount.Text + ",'SALARY','" + lblStaffname.Text + "',1," + dtChoosenDate.Day + "," + dtChoosenDate.Month + "," + dtChoosenDate.Year + "," + 0 + "," + "2" + "," + 1 + "," + 0 + ",2)";

                    string strUpdateHeadQuery = "update  `voucher` set Other_Trans_Type=2 where TransactionKey=" + lblTransactionkey.Text;
                    long iResultDebit = tranlayer.insertorupdateTrn(strChitHeadQuery);
                    long iResultCredit = tranlayer.insertorupdateTrn(strCashHeadQuery);
                    long uResult = tranlayer.insertorupdateTrn(strUpdateHeadQuery);
                    tranlayer.CommitTrn();
                    logger.Info("ReceivedAdvices.aspx - btnAcceptOK_click():  Completed: " + DateTime.Now + " by: " + Convert.ToString(Session["UserName"]) + "");


                    lblT.Text = "Status";
                    lblContent.Text = "Advice Accepted!!!";
                    lblContent.ForeColor = System.Drawing.Color.Green;
                    ModalPopupExtender1.PopupControlID = "PnlApprove";
                    ModalPopupExtender1.Show();
                    PnlApprove.Visible = true;
                }
            }
            //}
            catch (Exception error)
            {
                try
                {
                    tranlayer.RollbackTrn();
                    logger.Info("ReceivedAdvices.aspx - btnAcceptOK_click():  Error: " + error.Message + ": " + DateTime.Now + " by: " + Convert.ToString(Session["UserName"]) + "");
                }
                catch { }
                finally
                {
                    lblT.Text = "Status";
                    lblContent.Text = error.Message;
                    lblContent.ForeColor = System.Drawing.Color.Red;
                    ModalPopupExtender1.PopupControlID = "PnlApprove";
                    ModalPopupExtender1.Show();
                    PnlApprove.Visible = true;
                    isFinished = false;
                }
            }
            finally
            {
                //lblT.Text = "Status";
                //lblContent.Text = "Advice Accepted!!!";
                //lblContent.ForeColor = System.Drawing.Color.Green;
                //ModalPopupExtender1.PopupControlID = "PnlApprove";
                //ModalPopupExtender1.Show();
                //PnlApprove.Visible = true;
            }
        }

        protected void btnMsgCancel_Click(object sender, EventArgs e)
        {

        }

        protected void btnMsgOK_Click(object sender, EventArgs e)
        {
            ModalPopupExtender1.Hide();
            lblContent.Text = "";
            lblT.Text = "";
        }




    }
}