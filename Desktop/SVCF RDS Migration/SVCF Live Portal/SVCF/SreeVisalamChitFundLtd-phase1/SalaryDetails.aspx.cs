using DevExpress.Web.ASPxGridView;
using SVCF_BusinessAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SreeVisalamChitFundLtd_phase1
{
    public partial class SalaryDetails : System.Web.UI.Page
    {
        BusinessLayer balayer = new BusinessLayer();

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
                select();
            }

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("SalaryCreate.aspx");
        }



        protected void select()
        {
            try
            {
                //DataSourceSalary.SelectCommand = "SELECT DualTransactionKey,EmployeeHeadId, EmployeeName,ChoosenDate, (SELECT SUM(amount)  FROM empsalarydetails where VoucherType='C' Group By DualTransactionKey) as CreditAmount ,(SELECT SUM(amount)  FROM empsalarydetails where VoucherType='D' Group By DualTransactionKey) as DebitAmount FROM empsalarydetails  Group By DualTransactionKey";
                DataSourceSalary.SelectCommand = "SELECT uuid_from_bin(DualTransactionKey) as DualTransactionKey,EmployeeHeadId, EmployeeName,Date_Format(ChoosenDate,'%d/%m/%Y') as ChoosenDate, sum(case when VoucherType='C' then Amount else 0 end) as CreditAmount,sum(case when VoucherType='D' then Amount else 0 end) as DebitAmount FROM empsalarydetails  where BranchID=" + Session["Branchid"] + " Group By DualTransactionKey";
                gridSalary.DataBind();
            }
            catch (Exception)
            {
                throw;
            }
            //DataSourceSalary.SelectCommand = "SELECT SalaryId, EmployeeId,   EmployeeName,CreditBankName, CreditChequeNumber FROM svcf.tbl_salarydetails";
        }

        protected void gridSalaryDetails_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            balayer.GetInsertItem("delete FROM  svcf.empsalarydetails where uuid_from_bin(DualTransactionKey) ='" + e.Keys["DualTransactionKey"] + "' and  BranchID=" + Session["Branchid"]);
            ASPxGridView grid = (sender as ASPxGridView);
            grid.DataBind();
            e.Cancel = true;
        }

        //protected void gridSalaryDetails_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        //{
        //    //System.Globalization.DateTimeFormatInfo usDtfi = new System.Globalization.CultureInfo("en-NZ", false).DateTimeFormat;
        //    //string dt = e.NewValues["B_DOC"].ToString();
        //    //DateTime dFirstAuctionDate = DateTime.Parse(dt, usDtfi);
        //    //balayer.GetInsertItem("UPDATE svcf.branchdetails SET B_Code = '" + e.NewValues["B_Code"] + "', B_Head = '" + balayer.MySQLEscapeString(balayer.ToobjectstrEvenNull(e.NewValues["B_Head"])) + "', B_Address = '" + balayer.MySQLEscapeString(balayer.ToobjectstrEvenNull(e.NewValues["B_Address"])) + "', B_PhoneNo ='" + e.NewValues["B_PhoneNo"] + "' , B_EMail = '" + balayer.MySQLEscapeString(balayer.ToobjectstrEvenNull(e.NewValues["B_EMail"])) + "', B_DOC = '" + dFirstAuctionDate.Date.ToString("yyyy/MM/dd") + "' WHERE Head_Id=" + e.Keys["Head_Id"]);
        //    //ASPxGridView grid = (sender as ASPxGridView);
        //    //grid.DataBind();
        //    e.Cancel = true;
        //}

        public void GridViewPartialDelete(int JETranID)
        {
            //...
        }
    }
}