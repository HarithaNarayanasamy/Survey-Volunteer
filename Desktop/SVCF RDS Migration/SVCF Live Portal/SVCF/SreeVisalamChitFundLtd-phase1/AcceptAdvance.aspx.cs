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
    public partial class AcceptAdvance : System.Web.UI.Page
    {
        #region VarDeclaration
        BusinessLayer balayer = new BusinessLayer();
        TransactionLayer trn = new TransactionLayer();
        #endregion
        ILog logger = log4net.LogManager.GetLogger(typeof(AcceptAdvance));
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
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
            //TransactionLayer trn = new TransactionLayer();
            try
            {
                PnlApprove.Visible = false;
                GridViewRow gvrow = (GridViewRow)Session["Rowadvance"];
                string sno = balayer.ToobjectstrEvenNull(GridView1.DataKeys[gvrow.RowIndex]["sno"]);
                long sss= trn.insertorupdateTrn("update svcf.transadvance set `Flag`=2 where  sno=" + sno);
                ModalPopupExtender1.PopupControlID = "PnlApprove";
                ModalPopupExtender1.Show();
                lblContent.Text = "Advance Rejected";
                PnlApprove.Visible = true;
                btnRejectOk.Visible = false;
                btnMsgOK.Visible = false;
                btnMsgCancel.Visible = true;
                btnMsgCancel.Text = "Ok";
                trn.CommitTrn();
            }
            catch(Exception error)
            {
                try
                {
                    trn.RollbackTrn();
                }
                catch (Exception ex)
                {
                    
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
            logger.Info("Accept Advance - btnRejectOk_Click() - Completed: " + DateTime.Now + " by: " + Convert.ToString(Session["UserName"]) + "");
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
            GridViewRow gvrow = (GridViewRow)Session["Rowadvance"];
           // string HeadID = balayer.ToobjectstrEvenNull(GridView1.DataKeys[gvrow.RowIndex]["HeadID"]);
           // string Head = balayer.ToobjectstrEvenNull(GridView1.DataKeys[gvrow.RowIndex]["Head"]);
           // string ssss = balayer.GetSingleValue("SELECT sum(LoanAmount) FROM svcf.transadvance  where HeadID=" + HeadID);
            Label2.Text = "Accept";
            //lbHeadandHeadid.Text = "Advance Head : "+Head +" and Previous Total Advance : "+ ssss;
            lblAccept.Text = "If you want to accept the advance, Please enter the Advance Number.";
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
            return balayer.GetDataTable("select `branchdetails`.`B_Name` as B_Name,headstree.Node as Child,transadvance.BranchID,transadvance.HeadID,transadvance.Amount,transadvance.ApplyedOn,transadvance.Narration,`transadvance`.`Parentid`,h1.Node as Head,transadvance.sno from transadvance JOIN headstree on (transadvance.HeadID=headstree.NodeID)  join branchdetails on (`transadvance`.`BranchID`=`branchdetails`.`Head_Id`) join headstree as h1 on (`transadvance`.`Parentid`=`h1`.`NodeID`) where `transadvance`.`Flag`=" + aaaa + " and `transadvance`.`Applyedon`='" + balayer.indiandateToMysqlDate(txtDate.Text) + "'");
        }
        protected void btnview_OnClick(object sender, EventArgs e)
        {
            ImageButton btndetails = sender as ImageButton;
            GridViewRow gv = (GridViewRow)btndetails.NamingContainer;
            Session["Rowadvance"] = gv;
            string str = balayer.ToobjectstrEvenNull(GridView1.DataKeys[gv.RowIndex]["sno"]);
            
            lbViewHeader.Text = "Advance";
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
            Session["Rowadvance"] = gv;
            int Transmedium = Convert.ToInt32(GridView1.DataKeys[gv.RowIndex]["Transmedium"]);
            if (Transmedium == 1)
            {
                LbUNdoText.Text = "Do you want to undo Accepted Advance!!!";
                ModalPopupExtender1.PopupControlID = "pnlUndo";
                ModalPopupExtender1.Show();
                pnlUndo.Visible = true;
                UndoConfirmation.Visible = true;
                Button3.Visible = true;
                UndoConfirmation.Text = "Yes";
                Button3.Text = "No";
            }
            else if(Transmedium==2)
            {
                LbUNdoText.Text = "Do you want to undo Rejected Advance!!!";
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
           // TransactionLayer trn = new TransactionLayer();
            try
            {
                GridViewRow gvRow = (GridViewRow)Session["Rowadvance"];
                int Transmedium = Convert.ToInt32(GridView1.DataKeys[gvRow.RowIndex]["Transmedium"]);
                if (Transmedium == 1)
                {
                    pnlUndo.Visible = false;
                    string DualTransactionKey = balayer.ToobjectstrEvenNull(GridView1.DataKeys[gvRow.RowIndex]["DualTransactionKey"]);
                    long s = trn.insertorupdateTrn("delete FROM svcf.voucher where DualTransactionKey=" + DualTransactionKey);
                    long s2 = trn.insertorupdateTrn("delete from `transbank` where DualTransactionKey=" + DualTransactionKey);
                    long s1 = trn.insertorupdateTrn("update svcf.transadvance set TransactionKey=null,Transmedium=0,AdvanceNumber=null where DualTransactionKey=" + DualTransactionKey);
                    LbUNdoText.Text = "Accepted Advance Successfully undo.";
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
                    long s=trn.insertorupdateTrn("update svcf.transadvance set Transmedium=0 where DualTransactionKey=" + DualTransactionKey);
                    LbUNdoText.Text = "Rejected Advance Successfully undo.";
                    ModalPopupExtender1.PopupControlID = "pnlUndo";
                    ModalPopupExtender1.Show();
                    pnlUndo.Visible = true;
                    UndoConfirmation.Visible = false;
                    Button3.Visible = true;
                    UndoConfirmation.Text = "Yes";
                    Button3.Text = "Ok";
                }
                trn.CommitTrn();

                logger.Info("Accept Advance - UndoConfirmation_OnClick() - Completed: " + DateTime.Now + " by: " + Convert.ToString(Session["UserName"]) + "");
            }
            catch (Exception error)
            {
                try
                {
                    trn.RollbackTrn();
                }
                catch(Exception ex)
                {
                    logger.Info("Accept Advance - UndoConfirmation_OnClick() - Catch: "+ ex.Message + ":" + DateTime.Now + " by: " + Convert.ToString(Session["UserName"]) + "");
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
           // TransactionLayer trn = new TransactionLayer();
            Page.Validate("Rej");
            if (!Page.IsValid)
            {
                return;
            }
            PnlAccept.Visible = false;
            try
            {
                DataTable dt = balayer.GetDataTable("SELECT * FROM svcf.transadvance where AdvanceNumber=" + txtAccept.Text + "");
                if (dt.Rows.Count > 0)
                {

                    lblContent.Text = "Advise Number Already Exists!!!";
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
                    GridViewRow gvRow = (GridViewRow)Session["Rowadvance"];
                    string sno = balayer.ToobjectstrEvenNull(GridView1.DataKeys[gvRow.RowIndex]["sno"]);
                    long www = trn.insertorupdateTrn("update transadvance set AdvanceNumber=" + txtAccept.Text + ",Flag=1,ApprovedOn='" + DateTime.Now.ToString("yyyy/MM/dd") + "' where sno=" + sno);
                    lblContent.Text = "Advance Accepted!!!";
                    ModalPopupExtender1.PopupControlID = "PnlApprove";
                    ModalPopupExtender1.Show();
                    PnlApprove.Visible = true;
                    btnMsgOK.Visible = false;
                    btnRejectOk.Visible = false;
                    btnMsgCancel.Visible = true;
                    btnMsgCancel.Text = "Ok";
                }
                trn.CommitTrn();
                logger.Info("Accept Advance - btnAccept_Click() - : "+ DateTime.Now + " by: " + Convert.ToString(Session["UserName"]) + "");
            }
            catch (Exception error)
            {
                try
                {
                    trn.RollbackTrn();
                    logger.Info("Accept Advance - btnAccept_Click() - Catch: " + error.Message + ":" + DateTime.Now + " by: " + Convert.ToString(Session["UserName"]) + "");
                }
                catch (Exception ex)
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