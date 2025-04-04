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
    public partial class EditEmpDetails : System.Web.UI.Page
    {
        #region Object
        BusinessLayer balayer = new BusinessLayer();
        TransactionLayer trn = new TransactionLayer();
        #endregion

        string userinfo = "";
        string qry = "";
        string usrRole = "";
        ILog logger = log4net.LogManager.GetLogger(typeof(EditEmpDetails));

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
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

            //20/05/2021 - Bala- For Edit not working

            //DataSourceEmployee.SelectCommand = @"SELECT BranchID,Emp_ID,Emp_Name,Emp_Address,Emp_PhoneNo,Emp_Salary,Emp_Designation,Emp_Email,Emp_SrNumber,Date_Format(Emp_DateOfJoining,'%d/%m/%Y') as 'Emp_DateOfJoining' " +
            //    " FROM svcf.employee_details where BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]);
            DataSourceEmployee.SelectCommand = @"SELECT BranchID,Emp_ID,Emp_Name,Emp_Address,Emp_PhoneNo,Emp_Salary,Emp_Designation,Emp_Email,ifnull(Emp_SrNumber,0) as Emp_SrNumber , Date_Format(Emp_DateOfJoining,'%d/%m/%Y') as 'Emp_DateOfJoining' " +
                " FROM svcf.employee_details where BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]);
        }
        protected void gridBranch_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            userinfo = HttpContext.Current.User.Identity.Name;
            qry = "select rs.name from roles as rs inner join rights as rt on (rt.roleid=rs.id) where memberid=" + userinfo + "";
            usrRole = balayer.GetSingleValue(qry);
            if (usrRole == "Administrator")
            {
                balayer.GetInsertItem("delete from employee_details where Emp_ID='" + e.Keys["Emp_ID"] + "'");
                ASPxGridView grid = (sender as ASPxGridView);
                select();
                grid.DataBind();
                e.Cancel = true;
                logger.Info("EditEmpDetails.aspx - gridBranch_RowDeleting():  Completed: " + DateTime.Now + " by: " + Convert.ToString(Session["UserName"]) + "");
            }
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
            try
            {
                string EmpDateofJoining = Convert.ToString(e.NewValues["Emp_DateOfJoining"]);
                DateTime EmpDate = DateTime.Parse(EmpDateofJoining);
                balayer.GetInsertItem("UPDATE svcf.employee_details SET  Emp_Name = '" + balayer.MySQLEscapeString(balayer.ReplaceJnk(balayer.ToobjectstrEvenNull(e.NewValues["Emp_Name"]))) + "', Emp_Address = '" + balayer.MySQLEscapeString(balayer.ReplaceJnk(balayer.ToobjectstrEvenNull(e.NewValues["Emp_Address"]))) + "', Emp_PhoneNo ='" + e.NewValues["Emp_PhoneNo"] + "' , Emp_Salary = " + e.NewValues["Emp_Salary"] + ", Emp_Designation = '" + balayer.MySQLEscapeString(balayer.ReplaceJnk(balayer.ToobjectstrEvenNull(e.NewValues["Emp_Designation"]))) + "',Emp_Email='" + balayer.MySQLEscapeString(balayer.ToobjectstrEvenNull(e.NewValues["Emp_Email"])) + "', " +
                    "Emp_SrNumber='" + balayer.MySQLEscapeString(balayer.ReplaceJnk(balayer.ToobjectstrEvenNull(e.NewValues["Emp_SrNumber"]))) + "',Emp_DateOfJoining='" + balayer.changedateformat(EmpDate, 2) + "'" +
                    "WHERE Emp_ID='" + e.Keys["Emp_ID"] + "'");
                string empid = balayer.GetSingleValue("select EmpDes_ID from employeedesignation where Designationname like '%" + balayer.MySQLEscapeString(balayer.ReplaceJnk(balayer.ToobjectstrEvenNull(e.NewValues["Emp_Designation"])))+"%'");
                balayer.GetInsertItem("update svcf.employee_details set Designation_ID="+ empid + " WHERE Emp_ID='" + e.Keys["Emp_ID"] + "'");
                ASPxGridView grid = (sender as ASPxGridView);
                select();
                grid.DataBind();
                e.Cancel = true;                
                logger.Info(DateTime.Now + " | " + "EditEmpDetails.aspx - gridBranch_RowUpdating():  | Branch ID : " + Convert.ToString(Session["UserName"]) + " | Completed.");
            }
            catch(Exception err)
            {
                logger.Error(DateTime.Now + " | " + "EditEmpDetails.aspx - gridBranch_RowUpdating():  | Branch ID : " + Convert.ToString(Session["UserName"]) + " | " + err.Message);
            }
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
