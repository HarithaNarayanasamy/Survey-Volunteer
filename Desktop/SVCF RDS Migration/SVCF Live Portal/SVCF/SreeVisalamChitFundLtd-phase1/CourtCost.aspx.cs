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
    public partial class CourtCost : System.Web.UI.Page
    {
        #region Object
        BusinessLayer balayer = new BusinessLayer();
        TransactionLayer trn = new TransactionLayer();
        #endregion

        ILog logger = log4net.LogManager.GetLogger(typeof(CourtCost));
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                dxAppliedDate.ToolTip = "Ex. " + DateTime.Now.ToString("dd/MM/yyyy") + " (dd/mm/yyyy)";
                fillHead();
                ddlLoan.Focus();
                Session["CheckRefresh"] = System.Guid.NewGuid().ToString();
            }
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (Session["UserName"] == null || Session["Branchid"] == null || Session["BranchName"] == null)
            {
                Response.Redirect(Page.ResolveUrl("~/Login.aspx"), true);
            }
        }
        protected void Page_PreRender(object sender, EventArgs e)
        {
            ViewState["CheckRefresh"] = Session["CheckRefresh"];
        }
        void fillHead()
        {
            DataTable dt = balayer.GetDataTable("select NodeID,Node from headstree where NodeID=52");
            DataRow dr1 = dt.NewRow();
            dr1[0] = "0";
            dr1[1] = "--Select--";
            ddlLoan.DataSource = dt;
            ddlLoan.DataTextField = "Node";
            ddlLoan.DataValueField = "NodeID";
            dt.Rows.InsertAt(dr1, 0);
            ddlLoan.DataBind();

            DataTable dtCourt = balayer.GetDataTable("SELECT NodeID,Node FROM svcf.headstree where ParentID=7 and NodeID not in (51,52)");
            DataRow dr = dtCourt.NewRow();
            dr[1] = "--Select--";
            dr[0] = "0";
            ddlMember.DataValueField = "NodeID";
            ddlMember.DataTextField = "Node";
            ddlMember.DataSource = dtCourt;
            dtCourt.Rows.InsertAt(dr, 0);
            ddlMember.DataBind();
        }
        
        protected void btncancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.Url.AbsolutePath.ToString());
        }
        protected void btncancel1_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.Url.AbsolutePath.ToString());
        }

        protected void btn_ok(object sender, EventArgs e)
        {
            //TransactionLayer trn = new TransactionLayer();
            pnlmsg.Visible = false;
            //string node = "";
            string DualTransactionKey = "";
            try
            {
                System.Guid guid = Guid.NewGuid();
                string guidForChar36 = guid.ToString();
                string hexstring = BitConverter.ToString(guid.ToByteArray());
                string guidForBinary16 = "0x" + hexstring.Replace("-", string.Empty);
                DualTransactionKey = guidForBinary16;
                long insertTransLoan = trn.insertorupdateTrn("insert into `svcf`.`transcourt` (DualTransactionKey,Amount,ApplyedOn,AdminSanctionNo,HeadID,Narration,BranchID,ReHeadid) values (" + DualTransactionKey + "," + txtAmount.Text + ",'" + balayer.indiandateToMysqlDate(dxAppliedDate.Text) + "'," + txtAOSanctionNumber.Text + ",52,'" + balayer.MySQLEscapeString(txtNarration.Text) + "',"+Session["Branchid"] + ","+ddlMember.SelectedItem.Value+")");
                ModalPopupExtender1.PopupControlID = "Panel1";
                ModalPopupExtender1.Show();
                Panel1.Visible = true;
                Label8.Text = "Status";
                Label9.Text = "Head : Court cost paid and Amount : " + txtAmount.Text + " inserted Successfully!!!";
                Label9.ForeColor = System.Drawing.Color.Green;
                trn.CommitTrn();
                logger.Info("CourtCost.aspx - btn_ok() - Completed: " + DateTime.Now + " by: " + Convert.ToString(Session["UserName"]) + "");
            }
            catch (Exception error)
            {
                try
                {
                    trn.RollbackTrn();
                    logger.Info("CourtCost.aspx - btn_ok() - Error: " + error.Message + ": " + DateTime.Now + " by: " + Convert.ToString(Session["UserName"]) + "");
                }
                catch (Exception ex)
                { }
                finally
                {
                    //balayer.GetInsertItem("delete from `svcf`.`transloan` where `transloan`.`DualTransactionKey`=" + DualTransactionKey + "");
                    ScriptManager.RegisterStartupScript(this, GetType(), "Warning", "alert('" + error.Message.ToString().Replace("'", "\\'") + "');", true);
                }
            }
            finally
            {
                trn.DisposeTrn();
            }
        }

        protected void btnPayLoan_Click(object sender, EventArgs e)
        {
            Page.Validate("chit");
            if (!Page.IsValid)
            {
                return;
            }
            if (Session["CheckRefresh"].ToString() != ViewState["CheckRefresh"].ToString())
            {
                return;
            }
            ModalPopupExtender1.PopupControlID = "pnlmsg";
            ModalPopupExtender1.Show();
            pnlmsg.Visible = true;
            lblh.Text = "Status";
            lblcon.Text = "Do you want to insert for Head : Court cost paid and Amount :" + txtAmount.Text + "";
            lblcon.ForeColor = System.Drawing.Color.Green;
        }
    }
}