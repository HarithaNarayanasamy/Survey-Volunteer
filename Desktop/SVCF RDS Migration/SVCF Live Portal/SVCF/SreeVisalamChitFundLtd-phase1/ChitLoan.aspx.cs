using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using SVCF_BusinessAccessLayer;
using SVCF_TransactionLayer;
using SVCF_DataAccessLayer;
using log4net;
using log4net.Config;

namespace SreeVisalamChitFundLtd_phase1
{
    public partial class ChitLoan : System.Web.UI.Page
    {
        //#region VarDeclaration
        //CommonClassFile objcls = new CommonClassFile();
        //#endregion
        #region VarDeclaration
        BusinessLayer balayer = new BusinessLayer();
        TransactionLayer tranlayer = new TransactionLayer();
        #endregion

        ILog logger = log4net.LogManager.GetLogger(typeof(ChitLoan));

        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (Session["UserName"] == null || Session["Branchid"] == null || Session["BranchName"] == null)
            {
                Response.Redirect(Page.ResolveUrl("~/Login.aspx"), true);
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                dxAppliedDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                //fillBankHead();
                bindToken();
                ddlLoan.Focus();
                Session["CheckRefresh"] = System.Guid.NewGuid().ToString();
            }
        }
        protected void Page_PreRender(object sender, EventArgs e)
        {
            ViewState["CheckRefresh"] = Session["CheckRefresh"];
        }

        void bindToken()
        {
            if (ddlLoan.SelectedIndex == 0)
            {
                DataTable dt = balayer.GetDataTable("select NodeID,Node from headstree where ParentID=53");
                DataRow dr = dt.NewRow();
                dr[0] = "0";
                dr[1] = "--Select--";
                ddlTokenNo.DataTextField = "Node";
                ddlTokenNo.DataValueField = "NodeID";
                ddlTokenNo.DataSource = dt;
                dt.Rows.InsertAt(dr, 0);
                ddlTokenNo.DataBind();
            }
            else if (ddlLoan.SelectedIndex == 1)
            {
                DataTable dt = balayer.GetDataTable("select Node,NodeID from headstree where ParentID=55");
                DataRow dr = dt.NewRow();
                dr[1] = "0";
                dr[0] = "--Select--";
                ddlTokenNo.DataTextField = "Node";
                ddlTokenNo.DataValueField = "NodeID";
                ddlTokenNo.DataSource = dt;
                dt.Rows.InsertAt(dr, 0);
                ddlTokenNo.DataBind();
            }
        }
        protected void ddlLoan_SelectedIndexChanged(object sender, EventArgs e)
        {
            bindToken();
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
            TransactionLayer trn = new TransactionLayer();
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

                if (ddlLoan.SelectedIndex == 0)
                {
                    long insertTransLoan = trn.insertorupdateTrn("insert into transloan (DualTransactionKey,LoanAmount,ApplyedOn,AdminSanctionNo,LoanHeadID,Narration,BranchID,loantype) values (" + DualTransactionKey + "," + txtAmount.Text + ",'" + balayer.indiandateToMysqlDate(dxAppliedDate.Text) + "'," + txtAOSanctionNumber.Text + "," + ddlTokenNo.SelectedValue + ",'" + balayer.MySQLEscapeString(txtNarration.Text) + "'," + balayer.ToobjectstrEvenNull(Session["Branchid"]) + ",0)");
                    ModalPopupExtender1.PopupControlID = "Panel1";
                    ModalPopupExtender1.Show();
                    Panel1.Visible = true;
                    Label8.Text = "Status";
                    Label9.Text = "Loan Amount : Rs. " + txtAmount.Text + " for the Token : " + ddlTokenNo.SelectedItem.Text;
                    Label9.ForeColor = System.Drawing.Color.Green;
                }

                else if (ddlLoan.SelectedIndex == 1)
                {
                    long insertTransLoan = trn.insertorupdateTrn("insert into transloan (DualTransactionKey,EmployeeID,LoanHeadID,LoanAmount,ApplyedOn,AdminSanctionNo,Narration,BranchID,loantype) values (" + DualTransactionKey + "," + ddlTokenNo.SelectedItem.Value + "," + ddlTokenNo.SelectedItem.Value + "," + txtAmount.Text + ",'" + balayer.indiandateToMysqlDate(dxAppliedDate.Text) + "'," + txtAOSanctionNumber.Text + ",'" + balayer.MySQLEscapeString(txtNarration.Text) + "'," + balayer.ToobjectstrEvenNull(Session["Branchid"]) + ",1)");

                    ModalPopupExtender1.PopupControlID = "Panel1";
                    ModalPopupExtender1.Show();
                    Panel1.Visible = true;
                    Label8.Text = "Status";
                    Label9.Text = "Employee : " + balayer.MySQLEscapeString(ddlTokenNo.SelectedItem.Text) + " and Amount :" + txtAmount.Text + " inserted Successfully!!!";
                    Label9.ForeColor = System.Drawing.Color.Green;
                }
                trn.CommitTrn();
                logger.Info("ChitLoan.aspx - btn_ok() - Completed: " + DateTime.Now + " by: " + Convert.ToString(Session["UserName"]) + "");
            }
            catch (Exception error)
            {
                try
                {
                   trn.RollbackTrn();
                }
                catch (Exception ex)
                {
                    logger.Info("ChitLoan.aspx - btn_ok() - Error: " + ex.Message + ": " + DateTime.Now + " by: " + Convert.ToString(Session["UserName"]) + "");
                }
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

            if (ddlLoan.SelectedIndex == 0)
            {
                lblcon.Text = "Do you want to give an Amount : Rs." + txtAmount.Text + " for the Token :" + ddlTokenNo.SelectedItem.Text;
            }
            else
            {
                lblcon.Text = "Do you want to give an Amount : Rs." + txtAmount.Text + " for the Employee :" + ddlTokenNo.SelectedItem.Text + "";
            }
            lblcon.ForeColor = System.Drawing.Color.Green;
        }
    }
}
