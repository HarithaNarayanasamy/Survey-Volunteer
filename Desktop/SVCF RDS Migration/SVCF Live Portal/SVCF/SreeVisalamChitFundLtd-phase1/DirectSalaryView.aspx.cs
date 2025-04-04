using SVCF_BusinessAccessLayer;
using SVCF_DataAccessLayer;
using SVCF_TransactionLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SreeVisalamChitFundLtd_phase1
{
    public partial class DirectSalaryView : System.Web.UI.Page
    {
        #region ObjectDeclaration
        BusinessLayer balayer = new BusinessLayer();
        TransactionLayer tranlayer = new TransactionLayer();
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {

            }
        }

        protected void select()
        {
            DataTable getempval = new DataTable();
            getempval.Columns.Add("S.NO");
            getempval.Columns.Add("Service no");
            getempval.Columns.Add("Name");
            getempval.Columns.Add("Designation");
            getempval.Columns.Add("sta_Salary", typeof(decimal));
            getempval.Columns.Add("sta_DA", typeof(decimal));
            getempval.Columns.Add("sta_hra", typeof(decimal));
            getempval.Columns.Add("sta_ma", typeof(decimal));
            getempval.Columns.Add("Salary",typeof(decimal));
            getempval.Columns.Add("DA",typeof(decimal));
            getempval.Columns.Add("hra",typeof(decimal));
            getempval.Columns.Add("ma",typeof(decimal));
            getempval.Columns.Add("Total_est", typeof(decimal));
            getempval.Columns.Add("ESI",typeof(decimal));
            getempval.Columns.Add("EPF",typeof(decimal));
            getempval.Columns.Add("PFLoan",typeof(decimal));
            getempval.Columns.Add("other",typeof(decimal));
            getempval.Columns.Add("Total_det", typeof(decimal));
            getempval.Columns.Add("NetTotal", typeof(decimal));
            string getname = @"SELECT Emp_Name FROM svcf.employee_details where BranchId=1481;";
            DataTable getemp = balayer.GetDataTable(getname);

            for (int i = 0; i <= getemp.Rows.Count; i++)
            {

                string dt = @"select e1.Emp_ID as ServiceNO.,e1.Emp_Designation as Designation, sum(case when v1.Head_id = 86 Then Amount else 0.00 end) as Salary, sum(case when v1.Head_id = 87 Then Amount else 0.00 end) as DA , sum(case when v1.Head_id = 168 Then Amount else 0.00 end) as HRA,
 sum(case when v1.Head_id = 169 Then Amount else 0.00 end) as MA,sum(case when v1.Head_id = 164 Then Amount else 0.00 end) as PFLoan ,
 sum(case when v1.Head_id = 1115173 Then Amount else 0.00 end) as ESI , sum(case when v1.Head_id = 165 Then Amount else 0.00 end) as EPF,sum(case when v1.Head_id in (1113732,1113733) Then Amount else 0.00 end) as Other
 from voucher as v1 join employee_details as e1 on(v1.ReceievedBy =e1.Emp_Name) 
           where e1.BranchId =1481 and v1.series='SALARY'  and e1.Emp_Name='" + getemp.Rows[i]["Emp_Name"] + "';";
                DataTable dt1Part2 = balayer.GetDataTable(dt);
                DataRow drow = getempval.NewRow();
                drow["S.NO"] = i + 1;
                drow["Service no"] = dt1Part2.Rows[0]["ServiceNO"];
                drow["Name"] = getemp.Rows[i]["Emp_Name"];
                drow["Designation"] = dt1Part2.Rows[0]["Designation"];
                drow["sta_Salary"] = 0.00;
                drow["sta_DA"] = 0.00;
                drow["sta_ma"] = 0.00;
                drow["Salary"] = dt1Part2.Rows[0]["Salary"];
                drow["DA"] = dt1Part2.Rows[0]["DA"];
                drow["hra"] = dt1Part2.Rows[0]["HRA"];
                drow["ma"] = dt1Part2.Rows[0]["MA"];
             //   drow["Total_est"] = dt1Part2.Rows[0]["Salary"] + dt1Part2.Rows[0]["DA"] + dt1Part2.Rows[0]["HRA"] + dt1Part2.Rows[0]["MA"];
                drow["ESI"] = dt1Part2.Rows[0]["ESI"];
                drow["EPF"] = dt1Part2.Rows[0]["EPF"];
                drow["PFLoan"] = dt1Part2.Rows[0]["PFLoan"];
                drow["other"] = dt1Part2.Rows[0]["Other"];
            
            }
            AccessDataSource1.ConnectionString = CommonClassFile.ConnectionString;
           
            
            
            AccessDataSource1.SelectCommand = @"select t1.ChoosenDate as `Date`,t8.Node as LedgerHead ,bb.BankName as Bank,bb.AccountNo as `AccountNo.`,tt.CustomersBankName as CustomersBank,tt.ChequeDDNO as `ChequeNo.`,t1.Narration,(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )as `Credit`,(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )as `Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left Join headstree as t8 on (t1.ChitGroupID=t8.NodeID) left join transbank as tt on (t1.TransactionKey=tt.TransactionKey) left join bankdetails as bb on (t1.Head_Id=bb.Head_Id) where `t1`.`BranchID` = " + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and `t1`.`RootID` = 3 and `t1`.`ChoosenDate` between '" + balayer.indiandateToMysqlDate(dateFromConsolidated.Text) + "' and '" + balayer.indiandateToMysqlDate(dateToConsolidated.Text) + "'  and t1.IsDeleted=0 order by t1.ChoosenDate asc";
            grid.DataBind();
        }
    }
}