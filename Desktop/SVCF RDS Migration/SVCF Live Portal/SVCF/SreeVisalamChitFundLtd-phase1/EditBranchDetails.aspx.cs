using System;
using System.Data;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.Text.RegularExpressions;
using DevExpress.Web.ASPxGridView;
using SVCF_BusinessAccessLayer;
using SVCF_TransactionLayer;
using SVCF_DataAccessLayer;
using log4net;
using log4net.Config;

namespace SreeVisalamChitFundLtd_phase1
{
    public partial class WebForm4 : System.Web.UI.Page
    {
        #region Object
        BusinessLayer balayer = new BusinessLayer();
        TransactionLayer trn = new TransactionLayer();
        #endregion

        ILog logger = log4net.LogManager.GetLogger(typeof(WebForm4));
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
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
        }

        public void select()
        {
            DataSourceBranch.ConnectionString = CommonClassFile.ConnectionString;
            DataSourceBranch.SelectCommand = "SELECT B_Code , B_Name,B_Prefix, B_Head, B_Address, B_PhoneNo, B_EMail, DATE_FORMAT( B_DOC, '%d/%m/%Y') as B_DOC, Head_Id FROM branchdetails";
            gridBranch.DataBind();
        }
        protected void gridBranch_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            string aaaaaa = balayer.GetSingleValue("SELECT `branchdetails`.`B_Code` FROM svcf.branchdetails WHERE `branchdetails`.`Head_Id`=" + e.Keys["Head_Id"] + "");
            balayer.GetInsertItem("delete from headstree where NodeID=" + e.Keys["Head_Id"]);
            balayer.GetInsertItem("delete FROM svcf.membermaster where BranchId=" + e.Keys["Head_Id"] + " and MemberID='" + aaaaaa + "-1"+"'");
            balayer.GetInsertItem("delete FROM svcf.prospect where ProspectID='" + aaaaaa + "-1" + "' and BranchId=" + e.Keys["Head_Id"]);
            balayer.GetInsertItem("delete from branchdetails where  Head_Id="+ e.Keys["Head_Id"]);
            ASPxGridView grid = (sender as ASPxGridView);
            grid.DataBind();
            e.Cancel = true;
        }
        void AddError(Dictionary<GridViewColumn, string> errors, GridViewColumn column, string errorText)
        {
            if (errors.ContainsKey(column)) return;
            errors[column] = errorText;
        }
        
        protected void gridBranch_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            System.Globalization.DateTimeFormatInfo usDtfi = new System.Globalization.CultureInfo("en-NZ", false).DateTimeFormat;
            string dt = e.NewValues["B_DOC"].ToString();
            DateTime dFirstAuctionDate = DateTime.Parse(dt, usDtfi);
            balayer.GetInsertItem("UPDATE svcf.branchdetails SET B_Code = '" + e.NewValues["B_Code"] + "', B_Head = '" + balayer.MySQLEscapeString(balayer.ToobjectstrEvenNull( e.NewValues["B_Head"]) )+"', B_Prefix = '"+balayer.MySQLEscapeString(balayer.ToobjectstrEvenNull(e.NewValues["B_Prefix"]))+ "', B_Address = '" + balayer.MySQLEscapeString( balayer.ToobjectstrEvenNull( e.NewValues["B_Address"])) + "', B_PhoneNo ='" + e.NewValues["B_PhoneNo"] + "' , B_EMail = '" + balayer.MySQLEscapeString( balayer.ToobjectstrEvenNull( e.NewValues["B_EMail"])) + "', B_DOC = '" + dFirstAuctionDate.Date.ToString("yyyy/MM/dd") + "' WHERE Head_Id=" + e.Keys["Head_Id"]);
            ASPxGridView grid = (sender as ASPxGridView);
            grid.DataBind();
            e.Cancel = true;
            logger.Info("EditBranchDetails.aspx - gridBranch_RowUpdating():  Completed: " + DateTime.Now + " by: " + Convert.ToString(Session["UserName"]) + "");
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