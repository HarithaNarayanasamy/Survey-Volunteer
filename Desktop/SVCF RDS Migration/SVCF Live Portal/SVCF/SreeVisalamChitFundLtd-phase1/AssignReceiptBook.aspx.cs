using System;
using System.Collections;
using System.Collections.Generic;
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
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Text.RegularExpressions;
using SVCF_BusinessAccessLayer;
using SVCF_TransactionLayer;
using SVCF_DataAccessLayer;
using log4net;
using log4net.Config;

namespace SreeVisalamChitFundLtd_phase1
{
    public partial class AssignReceiptBook : System.Web.UI.Page
    {
        #region VarDeclaration
        BusinessLayer balayer = new BusinessLayer();
        TransactionLayer tranlayer = new TransactionLayer();
        #endregion

        ILog logger = log4net.LogManager.GetLogger(typeof(AssignReceiptBook));

        static List<string> lstDupName = new List<string>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (txtreceiptnoto_h.Value != null)
            {
                txtreceiptnoto.Text = txtreceiptnoto_h.Value;
            }
            pandupName.Visible = false;
            if (!IsPostBack)
            {

                var userinfo = HttpContext.Current.User.Identity.Name;
                var qry = "select rs.name from roles as rs inner join rights as rt on (rt.roleid=rs.id) where memberid=" + userinfo + "";
                var usrRole = balayer.GetSingleValue(qry);
                if (usrRole == "Report")
                {
                    Response.Redirect("Home.aspx", false);
                }

                DataTable DaTab = null;
                using (MySqlConnection conDaTab = balayer.OpenConnection())
                {
                    DaTab = balayer.GetDataTable("SELECT concat(moneycollname,'|',moneycolladdress) as moneycollname,moneycollid FROM svcf.moneycollector where BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]));
                    // lstDupName = (from dr in DaTab.AsEnumerable()
                    //  select dr.Field<string>("moneycollname")).ToList();
                    // lstDupName.Sort();
                }
                DataRow dr = DaTab.NewRow();
                dr[0] = "--select--";
                dr[1] = "0";
                ddlMoneyCollectorName.DataSource = DaTab;
                ddlMoneyCollectorName.DataTextField = "moneycollname";
                ddlMoneyCollectorName.DataValueField = "moneycollid";
                DaTab.Rows.InsertAt(dr, 0);
                ddlMoneyCollectorName.DataBind();
                ddlMoneyCollectorName.SelectedIndex = 0;
                ddlMoneyCollectorName.Focus();
                txtreceiptnofrom.Text = "";
                txtreceiptnoto.Text = "";
                Session["CheckRefresh"] = System.Guid.NewGuid().ToString();
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.Url.AbsoluteUri, false);
        }
        protected void Page_PreRender(object sender, EventArgs e)
        {
            ViewState["CheckRefresh"] = Session["CheckRefresh"];
        }
        protected void cancel_OnClick(object sender, EventArgs e)
        {
            Response.Redirect(Request.Url.AbsoluteUri, false);
        }
        protected void ddlMoneyCollectorName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlMoneyCollectorName.SelectedIndex != 0)
            {
                DataTable selmoneycoll = balayer.GetDataTable("select moneycollid,moneycollname,moneycolladdress,moneycollphno from moneycollector where moneycollid='" + ddlMoneyCollectorName.SelectedValue + "'");
                TxtcollectorID.Text = selmoneycoll.Rows[0]["moneycollname"].ToString();
                txtcollectoradd.Text = selmoneycoll.Rows[0]["moneycolladdress"].ToString();
                txtcollecorphoneno.Text = selmoneycoll.Rows[0]["moneycollphno"].ToString();
                txtreceiptnofrom.Text = "";
                txtreceiptnoto.Text = "";
                txtbookseries.Text = "";
                txtLastUsed.Text = "0";
                txtbookseries.Focus();
                txtbookseries.ToolTip = "Ex. KR";
            }
            else
            {
                TxtcollectorID.Text = "";
                txtcollectoradd.Text = "";
                txtcollecorphoneno.Text = "";
            }
        }

        protected void btnAddcollector_Click(object sender, EventArgs e)
        {
            if (("--select--".Equals(ddlMoneyCollectorName.SelectedItem.ToString())))
            {
                return;
            }
            Page.Validate("aaa");
            if (!Page.IsValid)
            {
                return;
            }
            if (Session["CheckRefresh"].ToString() != ViewState["CheckRefresh"].ToString())
            {
                return;
            }
            TransactionLayer trn = new TransactionLayer();
            try
            {
                DataTable ifExists = balayer.GetDataTable("select * from assignreceiptbook where receiptseries='" + balayer.MySQLEscapeString(txtbookseries.Text.Trim().ToUpper()) + "' and ((receiptnofrom between " + txtreceiptnofrom.Text + " and " + txtreceiptnoto.Text + ") or (receiptnoto between " + txtreceiptnofrom.Text + " and " + txtreceiptnoto.Text + ")) and BranchID=" + Session["Branchid"]);
                if (ifExists.Rows.Count == 0)
                {
                    string value = "";
                    if (string.IsNullOrEmpty(txtLastUsed.Text))
                    {
                        value = txtreceiptnofrom.Text;
                    }
                    else
                    {
                        value = txtLastUsed.Text;
                    }
                    int total=0;
                    if(Convert.ToInt32(txtLastUsed.Text)==0)
                    {
                        total = 200;
                    }
                    else
                    {
                        total= Convert.ToInt32(txtreceiptnoto.Text)- Convert.ToInt32( txtLastUsed.Text);
                    }
                    trn.insertorupdateTrn("insert into assignreceiptbook(`BranchID`,`moneycollid`,`receiptseries`,`receiptnofrom`,`receiptnoto`,`IsFinished`,`alreadyusedReceipts`,`total`) values(" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + "," + ddlMoneyCollectorName.SelectedValue + ",'" + balayer.MySQLEscapeString(txtbookseries.Text.Trim().ToUpper()) + "'," + txtreceiptnofrom.Text + "," + txtreceiptnoto.Text + ",0," + value + ","+total+")");
                    pandupName.Visible = false;
                    ModalPopup.PopupControlID = "Pnlmsg1";
                    lblT.Text = "Status";
                    btnOK.Text = "Ok";
                    btnCa.Visible = false;
                    btnCa.Text = "Cancel";
                    lblContent.Text = "Money Collector Name : " + TxtcollectorID.Text + ", Series : " + balayer.MySQLEscapeString(txtbookseries.Text.Trim().ToUpper()) + " Receipt From : " + Regex.Replace(txtreceiptnofrom.Text, @",", "") + " To : " + txtreceiptnoto.Text + " Assigned Successfully";
                    lblContent.ForeColor = System.Drawing.Color.Green;
                    this.ModalPopup.Show();
                    Pnlmsg1.Visible = true;
                }
                else
                {
                    ModalPopup.PopupControlID = "Pnlmsg1";
                    lblHint.Text = "Error";
                    Pnlmsg1.Visible = true;
                    lblT.Text = "Status";
                    btnOK.Text = "Ok";
                    btnCa.Visible = false;
                    btnCa.Text = "Cancel";
                    lblContent.Text = "Series : " + balayer.MySQLEscapeString(txtbookseries.Text.Trim().ToUpper()) + " Receipt From : " + ifExists.Rows[0]["receiptnofrom"] + " To : " + ifExists.Rows[0]["receiptnoto"] + " Already Alloted!!!";
                    lblContent.ForeColor = System.Drawing.Color.Red;
                    this.ModalPopup.Show();
                }
                trn.CommitTrn();
                logger.Info("AssignReceiptBook.aspx - btnAddCollector_Click() - Completed: " + DateTime.Now + " by: " + Convert.ToString(Session["UserName"]) + "");
            }
            catch (Exception error)
            {
                try
                {
                   trn.RollbackTrn();
                   logger.Info("AssignReceiptBook.aspx - btnAddCollector_Click() - Error: " + error.Message + ": " + DateTime.Now + " by: " + Convert.ToString(Session["UserName"]) + "");
                }
                catch { }
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
        protected void cusRange_ServerValidate(object sender, ServerValidateEventArgs e)
        {
            string value = "";
            if (balayer.ToobjectstrEvenNull(e.Value) == "")
            {
                value = "0";
            }
            else
            {
                value = e.Value.ToString();
            }
            int no = int.Parse(value);
            int fromReceipt = int.Parse(txtreceiptnofrom.Text);

            int toReceipt = int.Parse(txtreceiptnoto.Text);
            if ( (no==0))
            {
                e.IsValid = true;
            }
            else if (no >= fromReceipt & no < toReceipt)
            {
                e.IsValid = true;
            }
            else
            {
                e.IsValid = false;
            }
        }
        protected void btnOK_Click(object sender, EventArgs e)
        {
            if (lblHint.Text == "Error")
            {
                Pnlmsg1.Visible = false;
                lblHint.Text = "";
                txtbookseries.Text = "";
                txtreceiptnofrom.Text = "";
                txtreceiptnoto.Text = "";
            }
            else
            {
                Response.Redirect(Request.Url.AbsolutePath.ToString(), false);
            }
        }

        protected void btnCa_Click(object sender, EventArgs e)
        {
            if (lblHint.Text == "Error")
            {
                lblHint.Text = "";
                txtbookseries.Text = "";
                txtreceiptnofrom.Text = "";
                txtreceiptnoto.Text = "";
            }
            else
            {
                Response.Redirect(Request.Url.AbsolutePath.ToString(), false);
            }
        }

        protected void btnAdd_click(object sender, EventArgs e)
        {
            Button selbtn = (Button)sender;
            GridViewRow gr = (GridViewRow)selbtn.Parent.Parent;
            TxtcollectorID.Text = ((Label)GridView1.Rows[gr.RowIndex].FindControl("lblMoneyCollID")).Text;
            txtcollectoradd.Text = ((Label)GridView1.Rows[gr.RowIndex].FindControl("lblMoneycollAddr")).Text;
            txtcollecorphoneno.Text = ((Label)GridView1.Rows[gr.RowIndex].FindControl("lblphno")).Text;
            txtreceiptnofrom.Text = "";
            pandupName.Visible = false;
            GridView1.Visible = false;
            this.ModalPopup.Hide();
        }

        public void clear()
        {
            TxtcollectorID.Text = "";
            txtcollectoradd.Text = "";
            txtbookseries.Text = "";
            txtcollecorphoneno.Text = "";
            txtreceiptnofrom.Text = "";
            txtreceiptnoto.Text = "";
            ddlMoneyCollectorName.SelectedIndex = 0;
        }
    }
}
