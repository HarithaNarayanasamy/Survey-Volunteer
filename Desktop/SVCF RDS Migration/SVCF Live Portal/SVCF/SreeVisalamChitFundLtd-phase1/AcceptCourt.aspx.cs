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
    public partial class AcceptCourt : System.Web.UI.Page
    {
        #region VarDeclaration
        BusinessLayer balayer = new BusinessLayer();
        TransactionLayer trn = new TransactionLayer();
        #endregion

        ILog logger = log4net.LogManager.GetLogger(typeof(AcceptCourt));
        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (Session["UserName"] == null || Session["Branchid"] == null || Session["BranchName"] == null)
            {
                Response.Redirect(Page.ResolveUrl("~/Login.aspx"), false);
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["CheckRefresh"] = System.Guid.NewGuid().ToString();
                DataTable dtAcc = GetData(0);
                DataTable dtCloned = dtAcc.Clone();
                foreach (DataRow row in dtAcc.Rows)
                {
                    dtCloned.ImportRow(row);
                }
                GridView1.DataSource = dtCloned;
                GridView1.DataBind();
            }
        }
        protected void Page_PreRender(object sender, EventArgs e)
        {
            ViewState["CheckRefresh"] = Session["CheckRefresh"];
        }
        protected void GridView1_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (ddlStatus.SelectedIndex != 0)
                {
                    //ImageButton imgBtn = (ImageButton)e.Row.FindControl("btnundo");
                    //imgBtn.Visible = true;
                    ImageButton imgBtn1 = (ImageButton)e.Row.FindControl("btnview");
                    imgBtn1.Visible = false;
                }
                else
                {
                    //ImageButton imgBtn = (ImageButton)e.Row.FindControl("btnundo");
                    //imgBtn.Visible = false;
                    ImageButton imgBtn1 = (ImageButton)e.Row.FindControl("btnview");
                    imgBtn1.Visible = true;
                }
            }
        }
        protected void btnLoad_OnClick(object sender, EventArgs e)
        {
            if (ddlStatus.SelectedItem.Text == "Waiting")
            {
                DataTable dtAcc = GetData(0);
                DataTable dtCloned = dtAcc.Clone();
                dtCloned.Columns["ApplyedOn"].DataType = typeof(string);
                //dtCloned.Columns["ApprovedOn"].DataType = typeof(string);
                foreach (DataRow row in dtAcc.Rows)
                {
                    dtCloned.ImportRow(row);
                }

                GridView1.DataSource = dtCloned;
                GridView1.DataBind();
            }
            else if (ddlStatus.SelectedItem.Text == "Accepted")
            {
                DataTable dtAcc = GetData(1);
                DataTable dtCloned = dtAcc.Clone();
                dtCloned.Columns["ApplyedOn"].DataType = typeof(string);
                dtCloned.Columns["ApprovedOn"].DataType = typeof(string);
                foreach (DataRow row in dtAcc.Rows)
                {
                    dtCloned.ImportRow(row);
                }

                GridView1.DataSource = dtCloned;
                GridView1.DataBind();
            }
            else if (ddlStatus.SelectedItem.Text == "Rejected")
            {
                DataTable dtAcc = GetData(2);
                DataTable dtCloned = dtAcc.Clone();
                dtCloned.Columns["ApplyedOn"].DataType = typeof(string);
                dtCloned.Columns["ApprovedOn"].DataType = typeof(string);
                foreach (DataRow row in dtAcc.Rows)
                {
                    dtCloned.ImportRow(row);
                }
                GridView1.DataSource = dtCloned;
                GridView1.DataBind();
            }
        }
        protected void btnRejectOk_Click(object sender, EventArgs e)
        {
           // TransactionLayer trn = new TransactionLayer();
            try
            {
                PnlApprove.Visible = false;
                GridViewRow gvrow = (GridViewRow)Session["Rowdecree"];
                string ano = balayer.ToobjectstrEvenNull(GridView1.DataKeys[gvrow.RowIndex]["ano"]);
                trn.insertorupdateTrn("update `svcf`.`transcourt` set `transcourt`.`Flag`=2 where  ano=" + ano);
                ModalPopupExtender1.PopupControlID = "PnlApprove";
                ModalPopupExtender1.Show();
                lblContent.Text = "Decree Debtors Rejected";
                PnlApprove.Visible = true;
                btnRejectOk.Visible = false;
                btnMsgOK.Visible = false;
                btnMsgCancel.Visible = true;
                btnMsgCancel.Text = "Ok";
                trn.CommitTrn();
                logger.Info("AcceptCourt.aspx - btnRejectOk_Click() - Completed: " + DateTime.Now + " by: " + Convert.ToString(Session["UserName"]) + "");
            }
            catch (Exception error)
            {
                try
                {
                    trn.RollbackTrn();
                }
                catch (Exception ex)
                {
                    logger.Info("AcceptCourt.aspx - btnRejectOk_Click() - Error:" + ex.Message + ":" + DateTime.Now + " by: " + Convert.ToString(Session["UserName"]) + "");
                }
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

        protected void btnReject_OnClick(object sender, EventArgs e)
        {
            if (Session["CheckRefresh"].ToString() != ViewState["CheckRefresh"].ToString())
            {
                return;
            }
            pnlView.Visible = false;
            lblContent.Text = "Do you want to Reject!!!";
            ModalPopupExtender1.PopupControlID = "PnlApprove";
            ModalPopupExtender1.Show();
            PnlApprove.Visible = true;
            btnRejectOk.Visible = true;
            btnRejectOk.Text = "Yes";
            btnMsgOK.Visible = false;
            btnMsgCancel.Visible = true;
            btnMsgCancel.Text = "No";
        }
        protected void Approve_Click(object sender, EventArgs e)
        {
            if (Session["CheckRefresh"].ToString() != ViewState["CheckRefresh"].ToString())
            {
                return;
            }
            pnlView.Visible = false;
            GridViewRow gvrow = (GridViewRow)Session["Rowdecree"];
            string ssss = balayer.ToobjectstrEvenNull(GridView1.DataKeys[gvrow.RowIndex]["Amount"]);
            Label21.Text = "Accept";
            lbHeadandHeadid.Text = " Head : Court Cost Paid & Amount : " + ssss;
            lblAccept.Text = "If you want to accept the Decree Debtors, Please enter the Verification Number.";
            ModalPopupExtender1.PopupControlID = "PnlAccept";
            ModalPopupExtender1.Show();
            PnlAccept.Visible = true;
            btnAcceptOK.Visible = true;
            btnAcceptOK.Text = "Ok";
            btnRejectCancel.Visible = true;
            btnRejectCancel.Text = "Cancel";
        }
        protected void dis_Approve_Click(object sender, EventArgs e)
        {
            ImageButton btndetails = sender as ImageButton;
            GridViewRow gvRow = (GridViewRow)btndetails.NamingContainer;
            Session["RowIndex"] = gvRow.RowIndex;
            ModalPopupExtender1.PopupControlID = "PnlReject";
            ModalPopupExtender1.Show();
        }

        protected DataTable GetData(int aaaa)
        {
            return balayer.GetDataTable("select `branchdetails`.`B_Name`,h1.Node,`transcourt`.`HeadID` AS `HeadID`, `transcourt`.`Amount` AS `Amount`, `transcourt`.`ApplyedOn` AS `ApplyedOn`, `transcourt`.`AdminSanctionNo` AS `AdminSanctionNo`, `transcourt`.`Narration` AS `Narration`,  `transcourt`.`BranchID` AS `BranchID`, `transcourt`.`Flag` AS `Flag`, `transcourt`.`ano` AS `ano` from `transcourt` join `svcf`.`branchdetails` on (`transcourt`.`BranchID`=`branchdetails`.`Head_Id`) join headstree as h1 on (transcourt.ReHeadId=h1.NodeID) where `transcourt`.`Flag`=" + aaaa);
        }
        protected void btnview_OnClick(object sender, EventArgs e)
        {
            ImageButton btndetails = sender as ImageButton;
            GridViewRow gv = (GridViewRow)btndetails.NamingContainer;
            Session["Rowdecree"] = gv;

            lbViewHeader.Text = "Decree Debtors";
            ModalPopupExtender1.PopupControlID = "pnlView";
            ModalPopupExtender1.Show();
            pnlView.Visible = true;
        }

        protected void btnRejectCancel_Click(object sender, EventArgs e)
        {

        }
        protected void btnundo_OnClick(object sender, EventArgs e)
        {
            ImageButton btndetails = sender as ImageButton;
            GridViewRow gv = (GridViewRow)btndetails.NamingContainer;
            Session["Rowdecree"] = gv;
            int Transmedium = Convert.ToInt32(GridView1.DataKeys[gv.RowIndex]["Flag"]);
            if (Transmedium == 1)
            {
                LbUNdoText.Text = "Do you want to undo Accepted Decree Debtors!!!";
                ModalPopupExtender1.PopupControlID = "pnlUndo";
                ModalPopupExtender1.Show();
                pnlUndo.Visible = true;
                UndoConfirmation.Visible = true;
                Button3.Visible = true;
                UndoConfirmation.Text = "Yes";
                Button3.Text = "No";
            }
            else if (Transmedium == 2)
            {
                LbUNdoText.Text = "Do you want to undo Rejected Decree Debtors!!!";
                ModalPopupExtender1.PopupControlID = "pnlUndo";
                ModalPopupExtender1.Show();
                pnlUndo.Visible = true;
                UndoConfirmation.Visible = true;
                Button3.Visible = true;
                UndoConfirmation.Text = "Yes";
                Button3.Text = "No";
            }
        }

        protected void UndoConfirmation_OnClick(object sender, EventArgs e)
        {
            //TransactionLayer trn = new TransactionLayer();
            try
            {
                GridViewRow gvRow = (GridViewRow)Session["Rowdecree"];
                int Transmedium = Convert.ToInt32(GridView1.DataKeys[gvRow.RowIndex]["Flag"]);
                if (Transmedium == 1)
                {
                    pnlUndo.Visible = false;
                    string DualTransactionKey = balayer.ToobjectstrEvenNull(GridView1.DataKeys[gvRow.RowIndex]["DualTransactionKey"]);
                    trn.insertorupdateTrn("delete FROM svcf.voucher where DualTransactionKey=" + DualTransactionKey);
                    long s2 = trn.insertorupdateTrn("delete from `transbank` where DualTransactionKey=" + DualTransactionKey);
                    trn.insertorupdateTrn("update svcf.transcourt set TransactionKey=null,Flag=0,VerificationNumber=null where DualTransactionKey=" + DualTransactionKey);
                    LbUNdoText.Text = "Accepted Decree Debtor Successfully undo.";
                    ModalPopupExtender1.PopupControlID = "pnlUndo";
                    ModalPopupExtender1.Show();
                    pnlUndo.Visible = true;
                    UndoConfirmation.Visible = false;
                    Button3.Visible = true;
                    UndoConfirmation.Text = "Yes";
                    Button3.Text = "Ok";
                }
                else if (Transmedium == 2)
                {
                    pnlUndo.Visible = false;
                    string DualTransactionKey = balayer.ToobjectstrEvenNull(GridView1.DataKeys[gvRow.RowIndex]["DualTransactionKey"]);
                    trn.insertorupdateTrn("update svcf.transcourt set Flag=0 where DualTransactionKey=" + DualTransactionKey);
                    LbUNdoText.Text = "Rejected Decree Debtor Successfully undo.";
                    ModalPopupExtender1.PopupControlID = "pnlUndo";
                    ModalPopupExtender1.Show();
                    pnlUndo.Visible = true;
                    UndoConfirmation.Visible = false;
                    Button3.Visible = true;
                    UndoConfirmation.Text = "Yes";
                    Button3.Text = "Ok";
                }
                trn.CommitTrn();
                logger.Info("AcceptCourt.aspx - UndoConfirmation_OnClick() - Completed:" + DateTime.Now + " by: " + Convert.ToString(Session["UserName"]) + "");
            }
            catch (Exception error)
            {
                try
                {
                    trn.RollbackTrn();
                }
                catch (Exception ex)
                {
                    logger.Info("AcceptCourt.aspx - UndoConfirmation_OnClick() - Error: " + ex.Message + ":"+ DateTime.Now + " by: " + Convert.ToString(Session["UserName"]) + "");
                }
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

        protected void btnAccept_Click(object sender, EventArgs e)
        {
         //   TransactionLayer trn = new TransactionLayer();
            try
            {
                Page.Validate("Rej");
                if (!Page.IsValid)
                {
                    return;
                }
                PnlAccept.Visible = false;
                DataTable dt = balayer.GetDataTable("SELECT * FROM svcf.transcourt where VerificationNumber=" + txtAccept.Text + "");
                if (dt.Rows.Count > 0)
                {

                    lblContent.Text = "Verification Number Already Exists!!!";
                    ModalPopupExtender1.PopupControlID = "PnlApprove";
                    ModalPopupExtender1.Show();
                    PnlApprove.Visible = true;
                    btnMsgOK.Visible = false;
                    btnRejectOk.Visible = false;
                    btnMsgCancel.Visible = true;
                    btnMsgCancel.Text = "Ok";
                }
                else
                {
                    GridViewRow gvRow = (GridViewRow)Session["Rowdecree"];
                    string ano = balayer.ToobjectstrEvenNull(GridView1.DataKeys[gvRow.RowIndex]["ano"]);
                    trn.insertorupdateTrn("update transcourt set VerificationNumber=" + txtAccept.Text + ",Flag=1,isTransfered=0 where ano=" + ano);
                    lblContent.Text = "Decree Debtors Accepted!!!";
                    ModalPopupExtender1.PopupControlID = "PnlApprove";
                    ModalPopupExtender1.Show();
                    PnlApprove.Visible = true;
                    btnMsgOK.Visible = false;
                    btnRejectOk.Visible = false;
                    btnMsgCancel.Visible = true;
                    btnMsgCancel.Text = "Ok";
                }
                trn.CommitTrn();
                logger.Info("AcceptCourt.aspx - btnAccept_Click() - Completed:" + DateTime.Now + " by: " + Convert.ToString(Session["UserName"]) + "");
            }
            catch (Exception error)
            {
                try
                {
                    trn.RollbackTrn();
                }
                catch (Exception ex) { }
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
        protected void btnMsgCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.Url.AbsolutePath, false);

        }
        protected void btnMsgOK_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.Url.AbsolutePath, false);
        }
    }
}
