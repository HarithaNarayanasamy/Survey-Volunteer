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
using SVCF_TransactionLayer;
using SVCF_DataAccessLayer;
using log4net;
using log4net.Config;

namespace SreeVisalamChitFundLtd_phase1
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        #region Object
        BusinessLayer balayer = new BusinessLayer();
        TransactionLayer trn = new TransactionLayer();
        #endregion

        ILog logger = log4net.LogManager.GetLogger(typeof(WebForm2));

        protected void Page_Load(object sender, EventArgs e)
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
                    if (usrRole == "User" && column is GridViewCommandColumn)
                    {
                        ((GridViewCommandColumn)column).DeleteButton.Visible = false;
                    }
                    else if (column is GridViewDataColumn)
                    {
                        ((GridViewDataColumn)column).Settings.AutoFilterCondition = AutoFilterCondition.Contains;
                    }
                }
                select();
                gridBranch.DataBind();
            }
        }
        protected void select()
        {
            DataSourceBranch.ConnectionString = CommonClassFile.ConnectionString;
            DataSourceBranch.SelectCommand = "SELECT BranchID,BankName,IFCCode,AccountNo,Address, cast(DATE_FORMAT(DateOfAccount, '%d/%m/%Y') as char) as DateOfAccount,BankLocation,TypeofBank,Head_Id FROM svcf.bankdetails where BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]);
        }
        protected void gridBranch_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            balayer.GetInsertItem("delete from headstree where NodeID=" + e.Keys["Head_Id"]);
            balayer.GetInsertItem("delete from bankdetails where Head_Id=" + e.Keys["Head_Id"]);
            ASPxGridView grid = (sender as ASPxGridView);
            select();
            grid.DataBind();
            e.Cancel = true;
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
            string Branch=e.NewValues["BranchID"].ToString();
            string BranchName= balayer.GetSingleValue("SELECT B_Name FROM svcf.branchdetails where Head_Id="+Branch);
            System.Globalization.DateTimeFormatInfo usDtfi = new System.Globalization.CultureInfo("en-NZ", false).DateTimeFormat;
            string dt = e.NewValues["DateOfAccount"].ToString().Replace(" 00:00:00", "");
            DateTime dFirstAuctionDate = DateTime.Parse(dt, usDtfi);

            balayer.GetInsertItem("UPDATE svcf.bankdetails SET BankName = '" + balayer.MySQLEscapeString( balayer.ToobjectstrEvenNull( e.NewValues["BankName"])) + "', IFCCode = '" + balayer.MySQLEscapeString( balayer.ToobjectstrEvenNull( e.NewValues["IFCCode"])) + "', AccountNo = '" + balayer.MySQLEscapeString(balayer.ToobjectstrEvenNull( e.NewValues["AccountNo"])) + "', Address ='" + balayer.MySQLEscapeString( balayer.ToobjectstrEvenNull( e.NewValues["Address"])) + "' , DateOfAccount = '" + dFirstAuctionDate.Date.ToString("yyyy/MM/dd") + "', TypeofBank = '" + e.NewValues["TypeofBank"] + "',BankLocation= '" + balayer.MySQLEscapeString( balayer.ToobjectstrEvenNull( e.NewValues["BankLocation"])) + "' WHERE Head_Id=" + e.Keys["Head_Id"]);
            if (e.NewValues["TypeofBank"].ToString() == "Scheduled Banks")
            {
                balayer.GetInsertItem("update headstree set Node='" + balayer.MySQLEscapeString( BranchName) + "_" + balayer.MySQLEscapeString( balayer.ToobjectstrEvenNull( e.NewValues["BankName"])) + "_" + balayer.MySQLEscapeString( balayer.ToobjectstrEvenNull( e.NewValues["IFCCode"])) + "_" + balayer.MySQLEscapeString( balayer.ToobjectstrEvenNull( e.NewValues["AccountNo"])) + "',ParentID=23,TreeHint='" + "3,23," + e.Keys["Head_Id"] + "' where NodeID=" + e.Keys["Head_Id"]);
            }
            if (e.NewValues["TypeofBank"].ToString() == "Non Scheduled Banks")
            {
                balayer.GetInsertItem("update headstree set Node='" + balayer.MySQLEscapeString(BranchName) + "_" + balayer.MySQLEscapeString( balayer.ToobjectstrEvenNull( e.NewValues["BankName"])) + "_" + balayer.MySQLEscapeString( balayer.ToobjectstrEvenNull( e.NewValues["IFCCode"])) + "_" + balayer.MySQLEscapeString( balayer.ToobjectstrEvenNull( e.NewValues["AccountNo"])) + "',ParentID=23,TreeHint='" + "3,24," + e.Keys["Head_Id"] + "' where NodeID=" + e.Keys["Head_Id"]);
            }
            if (e.NewValues["TypeofBank"].ToString() == "Fixed deposits with Banks")
            {
                balayer.GetInsertItem("update headstree set Node='" + balayer.MySQLEscapeString(BranchName) + "_" + balayer.MySQLEscapeString( balayer.ToobjectstrEvenNull( e.NewValues["BankName"])) + "_" + balayer.MySQLEscapeString( balayer.ToobjectstrEvenNull( e.NewValues["IFCCode"])) + "_" + balayer.MySQLEscapeString( balayer.ToobjectstrEvenNull( e.NewValues["AccountNo"])) + "',ParentID=23,TreeHint='" + "3,25," + e.Keys["Head_Id"] + "' where NodeID=" + e.Keys["Head_Id"]);
            }
            ASPxGridView grid = (sender as ASPxGridView);
            select();
            grid.DataBind();
            e.Cancel = true;
            logger.Info("EditBankDetails1.aspx - gridBranch_RowUpdating():  Completed: " + DateTime.Now + " by: " + Convert.ToString(Session["UserName"]) + "");
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