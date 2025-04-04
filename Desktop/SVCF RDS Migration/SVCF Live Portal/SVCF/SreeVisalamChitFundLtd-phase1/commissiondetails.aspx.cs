using System;
using System.Data;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using DevExpress.Web.ASPxGridView;
using SVCF_BusinessAccessLayer;
using SVCF_DataAccessLayer;
using SVCF_TransactionLayer;
using log4net;
using log4net.Config;

namespace SreeVisalamChitFundLtd_phase1
{
    public partial class WebForm11 : System.Web.UI.Page
    {
        #region Object
        BusinessLayer balayer = new BusinessLayer();
        TransactionLayer trn = new TransactionLayer();
        #endregion

        ILog logger = log4net.LogManager.GetLogger(typeof(WebForm11));

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
                else
                {
                    foreach (GridViewColumn column in gridBranch.Columns)
                    {
                        if (column is GridViewDataColumn)
                        {
                            ((GridViewDataColumn)column).Settings.AutoFilterCondition = AutoFilterCondition.Contains;
                        }
                    }
                }
            }
            select();
            gridBranch.DataBind();
        }
        protected void select()
        {
            DataSourceBranch.ConnectionString = CommonClassFile.ConnectionString;
            DataSourceBranch.SelectCommand = "SELECT SINo ,ChitValue, Commission, IncidentalCharges,Total, ValueOfTheTaxableService, ServiceTax,if(GstAmount>0,GstAmount,0)"+
            "as GstAmount,if(CgstAmount>0,CgstAmount,0) as CgstAmount,if(SgstAmount>0,SgstAmount,0) as SgstAmount,if(IgstAmount>0,IgstAmount,0) as IgstAmount FROM svcf.commissiondetails";
        }
        protected void gridBranch_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            balayer.GetInsertItem("delete from commissiondetails where SINo=" + e.Keys["SINo"]);
            ASPxGridView grid = (sender as ASPxGridView);
            select();
            grid.DataBind();
            e.Cancel = true;
            logger.Info("Commissiondetails.aspx - gridBranch_RowDeleting() - Completed: " + DateTime.Now + " by: " + Convert.ToString(Session["UserName"]) + "");
        }
        protected void gridBranch_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        {
        }
        void AddError(Dictionary<GridViewColumn, string> errors, GridViewColumn column, string errorText)
        {
            if (errors.ContainsKey(column)) return;
            errors[column] = errorText;
        }
        protected void gridBranch_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            balayer.GetInsertItem("update commissiondetails set ChitValue=" + e.NewValues["ChitValue"] + ","+
                "Commission=" + e.NewValues["Commission"] + ",IncidentalCharges=" + e.NewValues["IncidentalCharges"] + ",Total=" + e.NewValues["Total"] + ","+               
                "CgstAmount=" + e.NewValues["CgstAmount"] + ",SgstAmount=" + e.NewValues["SgstAmount"] + ",IgstAmount=" + e.NewValues["IgstAmount"] + " " +
                "where SINo=" + e.Keys["SINo"]);
            //balayer.GetInsertItem("update commissiondetails set GstAmount=" + e.NewValues["GstAmount"] + ",ChitValue=" + e.NewValues["ChitValue"] + "," +
            //   "Commission=" + e.NewValues["Commission"] + ",IncidentalCharges=" + e.NewValues["IncidentalCharges"] + ",Total=" + e.NewValues["Total"] + "," +
            //   "ValueOfTheTaxableService=" + e.NewValues["ValueOfTheTaxableService"] + ",ServiceTax=" + e.NewValues["ServiceTax"] + " ," +
            //   "CgstAmount=" + e.NewValues["CgstAmount"] + ",SgstAmount=" + e.NewValues["SgstAmount"] + ",IgstAmount=" + e.NewValues["IgstAmount"] + " " +
            //   "where SINo=" + e.Keys["SINo"]);
            ASPxGridView grid = (sender as ASPxGridView);
            select();
            grid.DataBind();
            e.Cancel = true;
            logger.Info("Commissiondetails.aspx - gridBranch_RowUpdating() - Completed: " + DateTime.Now + " by: " + Convert.ToString(Session["UserName"]) + "");
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

