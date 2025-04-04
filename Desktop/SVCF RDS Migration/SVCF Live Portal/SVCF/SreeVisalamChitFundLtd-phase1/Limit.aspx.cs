using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SVCF_BusinessAccessLayer;
using SVCF_DataAccessLayer;
using SVCF_TransactionLayer;
using log4net;
using log4net.Config;

namespace SreeVisalamChitFundLtd_phase1
{
    public partial class Limit : System.Web.UI.Page
    {
        #region Object
        BusinessLayer balayer = new BusinessLayer();
        #endregion
        string userinfo = "";
        string qry = "";
        string usrRole = "";

        ILog logger = log4net.LogManager.GetLogger(typeof(Limit));

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                userinfo = HttpContext.Current.User.Identity.Name;
                qry = "select rs.name from roles as rs inner join rights as rt on (rt.roleid=rs.id) where memberid=" + userinfo + "";
                usrRole = balayer.GetSingleValue(qry);
                if (usrRole == "Administrator")
                {
                    DataTable dtNode = balayer.GetDataTable("SELECT B_Name,Head_id FROM svcf.branchdetails;");
                    DataRow drNode;
                    drNode = dtNode.NewRow();
                    drNode[0] = "--Select--";
                    drNode[1] = "0";
                    dtNode.Rows.InsertAt(drNode, 0);
                    ddlBranch.DataSource = dtNode;
                    ddlBranch.DataTextField = "B_Name";
                    ddlBranch.DataValueField = "Head_id";
                    ddlBranch.DataBind();
                }
                else
                {
                    Response.Redirect("Home.aspx", false);
                }
            }
        }
        protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strDate = balayer.GetSingleValue("SELECT DATE_FORMAT( MinimumDate, '%d/%m/%Y') MinimumDate FROM svcf.restrictionmaster where BranchID=" + ddlBranch.SelectedValue);
            txtDate.Text = strDate;
        }
        protected void btnOk_Click(object sender, EventArgs e)
        {
            TransactionLayer trn = new TransactionLayer();
            bool isVal = true;
            try
            {
                DataTable strDate = balayer.GetDataTable("SELECT DATE_FORMAT( MinimumDate, '%d/%m/%Y') MinimumDate FROM svcf.restrictionmaster where BranchID=" + ddlBranch.SelectedValue);
                if (strDate.Rows.Count > 0)
                {
                    trn.insertorupdateTrn("update restrictionmaster set MinimumDate='" + balayer.indiandateToMysqlDate(txtDate.Text) + "' where BranchID="+ddlBranch.SelectedValue);
                }
                else
                {
                    trn.insertorupdateTrn("insert into restrictionmaster (MinimumDate, BranchID) values ('"+balayer.indiandateToMysqlDate(txtDate.Text)+"'," + ddlBranch.SelectedValue+")");
                }
                trn.CommitTrn();
                logger.Info("Limit.aspx - btnOk_click():  Completed: " + DateTime.Now + " by: " + Convert.ToString(Session["UserName"]) + "");

            }
            catch (Exception ex)
            {
               trn.RollbackTrn();
                lbError.Text = ex.Message;
                lbError.ForeColor = System.Drawing.Color.Red;
                isVal = false;
                logger.Info("Limit.aspx - btnOk_click():  Error: " + ex.Message + ": " + DateTime.Now + " by: " + Convert.ToString(Session["UserName"]) + "");
            }
            finally
            {
                trn.DisposeTrn();
                if (isVal)
                {
                    lbError.Text = "Data Saved Successfully.";
                    lbError.ForeColor = System.Drawing.Color.Green;
                }
            }
        }
        protected void btncancel1_Click(object sender, EventArgs e)
        {

        }
    }
}