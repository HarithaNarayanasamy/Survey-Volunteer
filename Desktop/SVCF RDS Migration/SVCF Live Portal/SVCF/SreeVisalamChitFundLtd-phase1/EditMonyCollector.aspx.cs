using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using DevExpress.Web.ASPxGridView;
using SVCF_BusinessAccessLayer;
using SVCF_TransactionLayer;
using SVCF_DataAccessLayer;
using log4net;
using log4net.Config;

namespace SreeVisalamChitFundLtd_phase1
{
    public partial class EditMonyCollector : System.Web.UI.Page
    {
        #region Object
        BusinessLayer balayer = new BusinessLayer();
        TransactionLayer trn = new TransactionLayer();
        #endregion
        ILog logger = log4net.LogManager.GetLogger(typeof(EditMonyCollector));
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var userinfo = HttpContext.Current.User.Identity.Name;
                var qry = "select rs.name from roles as rs inner join rights as rt on (rt.roleid=rs.id) where memberid=" + userinfo + "";
                var usrRole = balayer.GetSingleValue(qry);
                if (usrRole == "Report")
                {
                    Response.Redirect("Home.aspx", false);
                }

                foreach (GridViewColumn column in gridBranch.Columns)
                {
                    if (column is GridViewDataColumn)
                    {
                        ((GridViewDataColumn)column).Settings.AutoFilterCondition = AutoFilterCondition.Contains;
                    }
                }
            }
            select();
            gridBranch.DataBind();            
        }
        protected void select()
        {
            DataSourceEmployee.ConnectionString = CommonClassFile.ConnectionString;
            DataSourceEmployee.SelectCommand = @"SELECT BranchID,moneycollid,moneycollname,moneycolladdress,moneycollphno FROM svcf.moneycollector where BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]);
        }
        protected void gridBranch_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            string userinfo = HttpContext.Current.User.Identity.Name;
            string qry = "select rs.name from roles as rs inner join rights as rt on (rt.roleid=rs.id) where memberid=" + userinfo + "";
            string usrRole = balayer.GetSingleValue(qry);
            if (usrRole == "Administrator")
            {
                balayer.GetInsertItem("delete from moneycollector where moneycollid='" + e.Keys["moneycollid"] + "'");
                ASPxGridView grid = (sender as ASPxGridView);
                select();
                grid.DataBind();
                e.Cancel = true;
            }
        }

        void AddError(Dictionary<GridViewColumn, string> errors, GridViewColumn column, string errorText)
        {
            if (errors.ContainsKey(column)) return;
            errors[column] = errorText;
        }

        protected void gridBranch_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            balayer.GetInsertItem("UPDATE svcf.moneycollector SET moneycollname = '" + balayer.MySQLEscapeString(balayer.ToobjectstrEvenNull(e.NewValues["moneycollname"])) + "', moneycolladdress = '" + balayer.MySQLEscapeString(balayer.ToobjectstrEvenNull(e.NewValues["moneycolladdress"])) + "',moneycollphno='" + e.NewValues["moneycollphno"] + "' WHERE moneycollid='" + e.Keys["moneycollid"] + "'");
            ASPxGridView grid = (sender as ASPxGridView);
            select();
            grid.DataBind();
            e.Cancel = true;
            logger.Info("EditMoneyCollector.aspx - gridBranch_RowUpdating():  Completed: " + DateTime.Now + " by: " + Convert.ToString(Session["UserName"]) + "");
        }

        protected void gridBranch_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
        {
            ASPxGridView grid = (sender as ASPxGridView);
            if (!grid.IsNewRowEditing)
            {
                grid.DoRowValidation();
            }
        }
    }
}
