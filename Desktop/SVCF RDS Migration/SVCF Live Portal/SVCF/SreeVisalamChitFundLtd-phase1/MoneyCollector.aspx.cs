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
using System.Collections.Generic;
using SVCF_BusinessAccessLayer;
using SVCF_DataAccessLayer;
using SVCF_TransactionLayer;
using log4net;
using log4net.Config;

namespace SreeVisalamChitFundLtd_phase1
{
    public partial class WebForm10 : System.Web.UI.Page
    {
        #region Object
        BusinessLayer balayer = new BusinessLayer();
        TransactionLayer trn = new TransactionLayer();
        #endregion
        ILog logger = log4net.LogManager.GetLogger(typeof(WebForm10));

        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (Session["UserName"] == null || Session["Branchid"] == null || Session["BranchName"] == null)
            {
                Response.Redirect(Page.ResolveUrl("~/Login.aspx"), true);
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            Pnlmsg1.Visible = false;
            if (!IsPostBack)
            {
                var userinfo = HttpContext.Current.User.Identity.Name;
                var qry = "select rs.name from roles as rs inner join rights as rt on (rt.roleid=rs.id) where memberid=" + userinfo + "";
                var usrRole = balayer.GetSingleValue(qry);
                if (usrRole == "Report")
                {
                    Response.Redirect("Home.aspx", false);
                }

                select();
                Session["CheckRefresh"] = System.Guid.NewGuid().ToString();
            }
        }
        protected void Page_PreRender(object sender, EventArgs e)
        {
            ViewState["CheckRefresh"] = Session["CheckRefresh"];
        }
        void select()
        {
            DataTable DaTab = balayer.GetDataTable("SELECT Emp_Name,Emp_ID FROM svcf.employee_details where  BranchID=" + Session["Branchid"]);

            DataRow dr = DaTab.NewRow();
            dr[0] = "--select--";
            dr[1] = "0";
            ddlMoneyCollector.DataSource = DaTab;
            ddlMoneyCollector.DataTextField = "Emp_Name";
            ddlMoneyCollector.DataValueField = "Emp_ID";
            DaTab.Rows.InsertAt(dr, 0);
            ddlMoneyCollector.DataBind();
        }
        protected void ddlMoneyCollector_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlMoneyCollector.SelectedIndex != 0)
            {
                DataTable dt = balayer.GetDataTable("SELECT Emp_Address,Emp_PhoneNo FROM svcf.employee_details where Emp_ID="+ddlMoneyCollector.SelectedItem.Value+" and BranchID=" + Session["Branchid"]);
                txtcollecorphoneno.Text = balayer.ToobjectstrEvenNull(dt.Rows[0]["Emp_PhoneNo"]);
                txtcollectoradd.Text = balayer.ToobjectstrEvenNull(dt.Rows[0]["Emp_Address"]);

            }
            else
            {
                txtcollectoradd.Text = "";
                txtcollecorphoneno.Text = "";
            }
        }
        protected void btnAddcollector_Click(object sender, EventArgs e)
        {
            Page.Validate("mid");
            if (!Page.IsValid)
            {
                return;
            }
            if (Session["CheckRefresh"].ToString() != ViewState["CheckRefresh"].ToString())
            {
                return;
            }
          
            try
            {
                DataTable dt = balayer.GetDataTable("SELECT * FROM svcf.moneycollector where moneycollphno='" + txtcollecorphoneno.Text + "' and employeeid=" + ddlMoneyCollector.SelectedItem.Value );
                if (dt.Rows.Count == 0)
                {

                    long insertCmd = trn.insertorupdateTrn("insert into moneycollector(`BranchID`,`moneycollname`,`moneycolladdress`,moneycollphno,employeeid) values(" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + ",'" + balayer.MySQLEscapeString(ddlMoneyCollector.SelectedItem.Text) + "','" + balayer.MySQLEscapeString(txtcollectoradd.Text) + "','" + txtcollecorphoneno.Text + "',"+ddlMoneyCollector.SelectedItem.Value+")");
                    ModalPopupExtender2.PopupControlID = "Pnlmsg1";
                    this.ModalPopupExtender2.Show();
                    Pnlmsg1.Visible = true;
                    lblT.Text = "Status";
                    lblContent.Text = " Money Collector Name : " + balayer.MySQLEscapeString(ddlMoneyCollector.SelectedItem.Text) + " Added Successfully";
                    lblContent.ForeColor = System.Drawing.Color.Green;
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Warning", "alert('Already Exists');", true);
                }
                trn.CommitTrn();
                logger.Info("MoneyCollector.aspx - btnAddcollector_Click():  Completed: " + DateTime.Now + " by: " + Convert.ToString(Session["UserName"]) + "");
            }
            catch (Exception error)
            {
                try
                {
                    trn.RollbackTrn();
                    logger.Info("MoneyCollector.aspx - btnAddcollector_Click():  Error: " + error.Message + ": " + DateTime.Now + " by: " + Convert.ToString(Session["UserName"]) + "");
                }
                catch { }
                finally
                {

                    ScriptManager.RegisterStartupScript(this, GetType(), "Warning", "alert('" + error.Message.ToString().Replace("'", "\\'") + "');", true);
                }
            }
            finally {
                trn.DisposeTrn();
            }
        }

        protected void btnOK_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.Url.AbsolutePath.ToString());
        }

        protected void btnCan_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.Url.AbsolutePath.ToString());
        }


    }
}
