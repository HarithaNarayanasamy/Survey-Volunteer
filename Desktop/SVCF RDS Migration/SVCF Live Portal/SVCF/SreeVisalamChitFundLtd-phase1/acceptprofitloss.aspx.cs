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
    public partial class acceptprofitloss : System.Web.UI.Page
    {
        #region VarDeclaration
        BusinessLayer balayer = new BusinessLayer();
        TransactionLayer tranlayer = new TransactionLayer();
        #endregion
        ILog logger = log4net.LogManager.GetLogger(typeof(acceptprofitloss));
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
                txtDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                DataTable dtAcc = GetData(0,txtDate.Text);
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
        protected void btnLoad_OnClick(object sender, EventArgs e)
        {
            if (ddlStatus.SelectedItem.Text == "Waiting")
            {
                DataTable dtAcc = GetData(0,txtDate.Text);

                GridView1.DataSource = dtAcc;
                GridView1.DataBind();
            }
            else if (ddlStatus.SelectedItem.Text == "Accepted")
            {
                DataTable dtAcc = GetData(1,txtDate.Text);

                GridView1.DataSource = dtAcc;
                GridView1.DataBind();
            }
        }
        protected void GridView1_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (ddlStatus.SelectedIndex != 0)
                {
                    ImageButton imgBtn1 = (ImageButton)e.Row.FindControl("btnview");
                    imgBtn1.Visible = false;
                }
                else
                {
                    ImageButton imgBtn1 = (ImageButton)e.Row.FindControl("btnview");
                    imgBtn1.Visible = true;
                }
            }
        }

        protected DataTable GetData(int aaaa,string date)
        {
            return balayer.GetDataTable("SELECT t1.BranchID,t1.DualTransactionKey, b1.B_Name,h1.Node,t1.Amount,t1.Narration,date_format(t1.ApplyedOn,'%d/%m/%Y') as Applied,t1.Flag,t1.ano FROM svcf.transprofitandloss as t1 join headstree as h1 on (t1.HeadID=h1.NodeID) join branchdetails as b1 on (t1.BranchID=b1.Head_Id) where t1.Flag=" + aaaa + " and t1.ApplyedOn='" + balayer.indiandateToMysqlDate(date) + "'");
        }
        protected void btnview_OnClick(object sender, EventArgs e)
        {
            ImageButton btndetails = sender as ImageButton;
            GridViewRow gv = (GridViewRow)btndetails.NamingContainer;
            Session["Rowprofitandloss"] = gv;
            lblContent.Text = "Do you want to approve";
            ModalPopupExtender1.PopupControlID = "PnlApprove";
            ModalPopupExtender1.Show();
            PnlApprove.Visible = true;
            btnMsgOK.Visible = true;
        }

        protected void btnMsgCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.Url.AbsolutePath, false);
        }
        protected void btnMsgOK_Click(object sender, EventArgs e)
        {
            ModalPopupExtender1.PopupControlID = "PnlApprove";
            ModalPopupExtender1.Show();
            PnlApprove.Visible = false;
            ModalPopupExtender1.PopupControlID = "pnlfinal";
            ModalPopupExtender1.Show();
            pnlfinal.Visible = true;
            btnMsgOK.Visible = false;
        }
        protected void btnOK_Click(object sender, EventArgs e)
        {
            Page.Validate("a");
            if (!IsValid)
            {
                return;
            }
            GridViewRow gvRow = (GridViewRow)Session["Rowprofitandloss"];
            string aid = balayer.ToobjectstrEvenNull(GridView1.DataKeys[gvRow.RowIndex]["ano"]);
            DataTable dt = balayer.GetDataTable("select * from transprofitandloss where AcceptNumber="+txtApprove.Text);
            if (dt.Rows.Count > 0)
            {
                lblContent.Text = "Profit & Loss Number Already Exists!!!";
                ModalPopupExtender1.PopupControlID = "PnlApprove";
                ModalPopupExtender1.Show();
                PnlApprove.Visible = true;
            }
            else
            {
                bool isFinished = true;
                TransactionLayer trn = new TransactionLayer();
                try
                {
                    long uResult = trn.insertorupdateTrn("update svcf.transprofitandloss set Flag=1, AcceptNumber='" + txtApprove.Text + "' where ano= " + aid);
                    trn.CommitTrn();
                    logger.Info("acceptprofitloss.aspx - btnOk_Click() - Completed: " + DateTime.Now + " by: " + Convert.ToString(Session["UserName"]) + "");
                }
                catch (Exception error)
                {
                    try
                    {
                       trn.RollbackTrn();
                    }
                    catch(Exception err)
                    {
                        logger.Info("acceptprofitloss.aspx - btnOk_Click() - Error: " + err.Message + ": " + DateTime.Now + " by: " + Convert.ToString(Session["UserName"]) + "");
                    }
                    finally 
                    {
                        isFinished = false;
                        lblContent.Text = error.Message;
                        lblContent.ForeColor = System.Drawing.Color.Red;
                        ModalPopupExtender1.PopupControlID = "PnlApprove";
                        ModalPopupExtender1.Show();
                        PnlApprove.Visible = true;
                    }
                }
                finally
                {
                    trn.DisposeTrn();
                    if (isFinished)
                    {
                        lblContent.Text = "Data saved successfully";
                        lblContent.ForeColor = System.Drawing.Color.Green;
                        ModalPopupExtender1.PopupControlID = "PnlApprove";
                        ModalPopupExtender1.Show();
                        PnlApprove.Visible = true;
                    }
                }
            }
        }
    }
}