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
    public partial class Advances : System.Web.UI.Page
    {
        #region Object
        BusinessLayer balayer = new BusinessLayer();
        TransactionLayer trn = new TransactionLayer();
        #endregion

        ILog logger = log4net.LogManager.GetLogger(typeof(Advances));
        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (Session["UserName"] == null || Session["Branchid"] == null || Session["BranchName"] == null)
            {
                Response.Redirect(Page.ResolveUrl("~/Login.aspx"), false);
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            Panel2.Visible = false;
            pnlmsg.Visible = false;
            if (!IsPostBack)
            {
                txtChild.Visible = false;
                RequiredFieldValidator3.Visible = false;
                dxAppliedDate.ToolTip = "Ex. " + DateTime.Now.ToString("dd/MM/yyyy") + " (dd/mm/yyyy)";
                
                bindddlChitGroupNo();
                ddlHeadAdvances.Focus();
                Session["CheckRefresh"] = System.Guid.NewGuid().ToString();
            }
        }
        protected void Page_PreRender(object sender, EventArgs e)
        {
            ViewState["CheckRefresh"] = Session["CheckRefresh"];
        }

        void bindddlChitGroupNo()
        {
            DataTable dtgroupno = balayer.GetDataTable("select Node,NodeID from headstree where NodeID  in (58,167)");
            ddlHeadAdvances.DataSource = dtgroupno;
            DataRow dr = dtgroupno.NewRow();
            dr[0] = "--Select--";
            dr[1] = "0";
            ddlHeadAdvances.DataValueField = "NodeID";
            ddlHeadAdvances.DataTextField = "Node";
            dtgroupno.Rows.InsertAt(dr, 0);
            ddlHeadAdvances.DataBind();
            ddlHeadAdvances.Focus();
        }
        void bindToken()
        {
            if (ddlHeadAdvances.SelectedItem.Text == "Sundry Creditors")
            {
                ddlChildHead.Items.Clear();
                DataTable dtgroupno1 = balayer.GetDataTable("SELECT NodeID, Node  FROM svcf.headstree where BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and  ParentID=" + ddlHeadAdvances.SelectedItem.Value);
                ddlChildHead.DataSource = dtgroupno1;
                ddlChildHead.DataValueField = "NodeID";
                ddlChildHead.DataTextField = "Node";
                ddlChildHead.DataBind();
                ListItem li = new ListItem("--Select--", "--Select--");
                ddlChildHead.Items.Insert(0, li);
                ddlChildHead.Visible = true;
                CompareValidator1.Visible = true;
                txtChild.Visible = false;
                RequiredFieldValidator3.Visible = false;
            }
            else if (ddlHeadAdvances.SelectedItem.Text == "Staff Advances")
            {
                ddlChildHead.Items.Clear();
                DataTable dtgroupno2 = balayer.GetDataTable("SELECT  NodeID, Node FROM svcf.headstree where Branchid="+Session["Branchid"]+" and ParentID=" + ddlHeadAdvances.SelectedItem.Value);
                ddlChildHead.DataSource = dtgroupno2;
                ddlChildHead.DataValueField = "NodeID";
                ddlChildHead.DataTextField = "Node";
                ddlChildHead.DataBind();
                ListItem li = new ListItem("--Select--", "--Select--");
                ddlChildHead.Items.Insert(0, li);
                ddlChildHead.Visible = true;
                CompareValidator1.Visible = true;
                txtChild.Visible = false;
                RequiredFieldValidator3.Visible = false;
            }
            
            else
            {
                ddlChildHead.Visible = false;
                CompareValidator1.Visible = false;
                txtChild.Visible = true;
                RequiredFieldValidator3.Visible = true;
            }
        }
        protected void ddlHeadAdvances_SelectedIndexChanged(object sender, EventArgs e)
        {
            bindToken();
        }

        protected void btncancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.Url.AbsolutePath.ToString(), false);
        }
        protected void btncancel1_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.Url.AbsolutePath.ToString(), false);
        }
        protected void btn_ok(object sender, EventArgs e)
        {
            TransactionLayer trn = new TransactionLayer();
            pnlmsg.Visible = false;
            string DualTransactionKey = "";
            try
            {
                System.Guid guid = Guid.NewGuid();
                string guidForChar36 = guid.ToString();
                string hexstring = BitConverter.ToString(guid.ToByteArray());
                string guidForBinary16 = "0x" + hexstring.Replace("-", string.Empty);
                DualTransactionKey = guidForBinary16;
                if (ddlHeadAdvances.SelectedItem.Text == "Sundry Creditors" | ddlHeadAdvances.SelectedItem.Text == "Sundry Debtors")
                {
                    long strBank = trn.insertorupdateTrn("insert into transadvance (DualTransactionKey,HeadID,Amount,ApplyedOn,AdminSanctionNo,Narration,BranchID,Parentid,`Flag`) values (" + DualTransactionKey + "," + ddlChildHead.SelectedItem.Value + "," + txtAmount.Text + ",'" + balayer.indiandateToMysqlDate(dxAppliedDate.Text) + "'," + txtAOSanctionNumber.Text + ",'" + balayer.MySQLEscapeString(txtNarration.Text) + "'," + balayer.ToobjectstrEvenNull(Session["Branchid"]) + "," + ddlHeadAdvances.SelectedItem.Value + ",0)");

                    ModalPopupExtender1.PopupControlID = "Panel2";
                    ModalPopupExtender1.Show();
                    Panel2.Visible = true;
                    Label8.Text = "Status";
                    Label9.Text = "Token Number : " + ddlChildHead.SelectedItem.Text + " and Amount :" + txtAmount.Text + " inserted Successfully!!!";
                    Label9.ForeColor = System.Drawing.Color.Green;
                }
                else if (ddlHeadAdvances.SelectedItem.Text == "Staff Advances" | ddlHeadAdvances.SelectedItem.Text == "vehicle recovery advance" | ddlHeadAdvances.SelectedItem.Text == "Prepaid Advance")
                {

                    long strBank = trn.insertorupdateTrn("insert into transadvance (DualTransactionKey,HeadID,Amount,ApplyedOn,AdminSanctionNo,Narration,BranchID,Parentid,`Flag`,employeeid) values (" + DualTransactionKey + "," + ddlChildHead.SelectedItem.Value + "," + txtAmount.Text + ",'" + balayer.indiandateToMysqlDate(dxAppliedDate.Text) + "'," + txtAOSanctionNumber.Text + ",'" + balayer.MySQLEscapeString(txtNarration.Text) + "'," + balayer.ToobjectstrEvenNull(Session["Branchid"]) + "," + ddlHeadAdvances.SelectedItem.Value + ",0,"+ddlChildHead.SelectedItem.Value+")");

                    ModalPopupExtender1.PopupControlID = "Panel2";
                    ModalPopupExtender1.Show();
                    Panel2.Visible = true;
                    Label8.Text = "Status";
                    Label9.Text = "Token Number : " + ddlChildHead.SelectedItem.Text + " and Amount :" + txtAmount.Text + " inserted Successfully!!!";
                    Label9.ForeColor = System.Drawing.Color.Green;
                }
                
                trn.CommitTrn();
                logger.Info("Advances.aspx - btn_ok() - Completed: " + DateTime.Now + " by: " + Convert.ToString(Session["UserName"]) + "");
            }
            catch (Exception error)
            {
                try
                {
                   trn.RollbackTrn();
                }
                catch (Exception ex)
                {
                    logger.Info("Advances.aspx - btn_ok() - Error: " + ex.Message + ": " + DateTime.Now + " by: " + Convert.ToString(Session["UserName"]) + "");
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
            if (ddlHeadAdvances.SelectedItem.Text == "Sundry Creditors")
            {
                ModalPopupExtender1.PopupControlID = "pnlmsg";
                ModalPopupExtender1.Show();
                pnlmsg.Visible = true;
                lblh.Text = "Status";
                lblcon.Text = "Do you want to insert for Advance : " + ddlChildHead.SelectedItem.Text + " and Amount :" + txtAmount.Text + "";
                lblcon.ForeColor = System.Drawing.Color.Green;
            }
            else if (ddlHeadAdvances.SelectedItem.Text == "Staff Advances")
            {
                ModalPopupExtender1.PopupControlID = "pnlmsg";
                ModalPopupExtender1.Show();
                pnlmsg.Visible = true;
                lblh.Text = "Status";
                lblcon.Text = "Do you want to insert for Advance : " + ddlChildHead.SelectedItem.Text + " and Amount :" + txtAmount.Text + "";
                lblcon.ForeColor = System.Drawing.Color.Green;
            }
        }
    }
}