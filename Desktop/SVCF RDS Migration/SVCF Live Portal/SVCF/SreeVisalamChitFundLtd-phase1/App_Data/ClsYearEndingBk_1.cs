using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SVCF_DataAccessLayer;
using SVCF_BusinessAccessLayer;
using System.Data;
using System.IO;
using System.Globalization;
namespace SreeVisalamChitFundLtd_phase1
{
    public class ClsYearEndingBk_1
    {
        BusinessLayer objBAL = new BusinessLayer();
        string query = "";
        public List<Emoluments_St_12> GetEmoluments(string fromdt, string todt,int BranchId)
        {
            List<Emoluments_St_12> EmolumentsSt12 = new List<Emoluments_St_12>();
            CultureInfo provider = CultureInfo.InvariantCulture;
            try
            {
                IFormatProvider culture2 = new System.Globalization.CultureInfo("fr-FR", true);
                DateTime FromDt = DateTime.Parse(fromdt, culture2, System.Globalization.DateTimeStyles.AssumeLocal);

                IFormatProvider culture3 = new System.Globalization.CultureInfo("fr-FR", true);
                DateTime ToDt = DateTime.Parse(todt, culture3, System.Globalization.DateTimeStyles.AssumeLocal);
              
             //  DateTime FromDt = DateTime.ParseExact(fromdt, "mm-DD-yyyy", provider);
               // DateTime ToDt = DateTime.ParseExact(todt, "mm-DD-yyyy", provider);

                query = "select * from voucher as vc where vc.Head_Id in (86, 87,168,169,1508,91,90) and vc.ChoosenDate between '" + objBAL.changedateformat(FromDt, 2) + "' " +
                    "and '" + objBAL.changedateformat(ToDt, 2) + "' and Series='SALARY' and BranchId=" + BranchId + " order by vc.ChoosenDate;";
                var VCList = objBAL.GetDataTable(query);
                List<ModelVoucher> VoucherData = VCList.DataTableToList<ModelVoucher>();
                Emoluments_St_12 objEmolumentsSt12 = new Emoluments_St_12();
                // var EmpNameList = VoucherData.AsEnumerable().Select(row => row.ReceievedBy).Distinct().ToList();
                DataTable EmpNameList = objBAL.GetDataTable("SELECT Emp_ID FROM svcf.employee_details where branchid=" + BranchId + " order by Designation_ID,Emp_SrNumber asc; ");
                List<ModelEmployee_Details> EmpDetails = null;
              
                decimal AggregateAmount = 0;
                int SlNo = 0;
                //bool brench = true;
                foreach (DataRow r in EmpNameList.Rows)
                {
                    var drw = r[0].ToString();
                    //brench = true;
                    objEmolumentsSt12 = new Emoluments_St_12();
                    var existingRows = (from row in VoucherData.AsEnumerable()
                                        where row.M_Id == Convert.ToInt32(drw)
                                        select row).ToList();
                  
                    
                    // Take drw as Employee ID
                    //var existingempidmethod =(from x in VoucherData.AsEnumerable() where x.M_Id == Convert.ToInt16(drw)
                    //                         select x).ToList();



                    SlNo++;
                    try
                    {
                        object value = existingRows[0].M_Id;
                        if ((value == DBNull.Value) || Convert.ToInt32(value) == 0)
                        {
                            query = "SELECT * FROM svcf.employee_details where Emp_ID like'%" + existingRows[0].M_Id + "%' and branchid='"+ BranchId + "'";
                            var emplist = objBAL.GetDataTable(query);
                            EmpDetails = emplist.DataTableToList<ModelEmployee_Details>();
                        }
                        else
                        {
                            query = "SELECT * FROM svcf.employee_details where Emp_ID=" + value + "";
                            var emplist = objBAL.GetDataTable(query);
                            EmpDetails = emplist.DataTableToList<ModelEmployee_Details>();
                            //brench = false;
                        }
                        if (EmpDetails.Count!=0)
                        {
                            objEmolumentsSt12.SlNo = Convert.ToString(SlNo);
                            objEmolumentsSt12.AprNo = Convert.ToString(EmpDetails[0].Emp_SrNumber);
                            var dateSpan = GetMonthDifference(ToDt, DateTime.Parse(EmpDetails[0].Emp_DateOfJoining.ToString()));
                            objEmolumentsSt12.PeriodWorked_inMonths = Convert.ToString(dateSpan);
                            TimeSpan difference = ToDt - FromDt;
                            objEmolumentsSt12.PeriodWorked_indays = Convert.ToString(difference.Days);
                            objEmolumentsSt12.NameoftheStaff = Convert.ToString(EmpDetails[0].Emp_Name);

                            var Salsum = existingRows.AsEnumerable().Where(e => Convert.ToInt32(e.Head_Id) == 86).Sum(e => e.Amount);
                            objEmolumentsSt12.Salary = Salsum;
                            AggregateAmount = AggregateAmount + Salsum;

                            var Dasum = existingRows.AsEnumerable().Where(e => Convert.ToInt32(e.Head_Id) == 87).Sum(e => e.Amount);
                            objEmolumentsSt12.DearnessAllowance = Dasum;
                            AggregateAmount = AggregateAmount + Dasum;
                            var Hrasum = existingRows.AsEnumerable().Where(e => Convert.ToInt32(e.Head_Id) == 168).Sum(e => e.Amount);
                            objEmolumentsSt12.HouseRentAllowance = Hrasum;
                            AggregateAmount = AggregateAmount + Hrasum;

                            //var MAasum = existingRows.AsEnumerable().Where(e => Convert.ToInt32(e.Head_Id) == 169).Sum(e => e.Amount);
                            //objEmolumentsSt12.MedicalAllowance = MAasum;
                            //AggregateAmount = AggregateAmount + MAasum;

                            var Bonussum = existingRows.AsEnumerable().Where(e => Convert.ToInt32(e.Head_Id) == 1508).Sum(e => e.Amount);
                            objEmolumentsSt12.Bonus = Bonussum;
                            AggregateAmount = AggregateAmount + Bonussum;

                            var BusinessIncentivesum = existingRows.AsEnumerable().Where(e => Convert.ToInt32(e.Head_Id) == 91).Sum(e => e.Amount);
                            objEmolumentsSt12.BusinessIncentive = BusinessIncentivesum;
                            AggregateAmount = AggregateAmount + BusinessIncentivesum;


                            var BppforCurrentBranch = existingRows.AsEnumerable().Where(e => Convert.ToInt32(e.Head_Id) == 90).Sum(e => e.Amount);
                            objEmolumentsSt12.BPPforCurrentBranch = BppforCurrentBranch;
                            AggregateAmount = AggregateAmount + BppforCurrentBranch;

                            var BppforBranch = objBAL.GetScalarDecimal("select if(sum(Amount)>0,Sum(Amount),0) as Amount  from svcf.voucher as vc join svcf.headstree as hd on hd.NodeId = vc.Head_Id " +
                                           " where vc.Head_Id in (hd.NodeID) and vc.ChoosenDate between '" + objBAL.indiandateToMysqlDate(fromdt) + "' and '" + objBAL.indiandateToMysqlDate(todt) + "'  and hd.ParentID = 1 and " +
                                           "vc.BranchId = " + BranchId + " and vc.Series = 'SALARY' and  vc.M_Id = '" + drw + "'; ");
                            objEmolumentsSt12.BPPforOtherBranch = Convert.ToDecimal(BppforBranch);
                            AggregateAmount = AggregateAmount + BppforBranch;

                            objEmolumentsSt12.AggregateTotal = AggregateAmount;
                            AggregateAmount = 0;
                            EmolumentsSt12.Add(objEmolumentsSt12);
                        }
                       
                    }
                    catch (Exception err) { }
                }

            }
            catch (Exception err) { }
            return EmolumentsSt12;
        }

        int GetMonthDifference(DateTime startDate, DateTime endDate)
        {
            int monthsApart = 12 * (startDate.Year - endDate.Year) + startDate.Month - endDate.Month;
            return Math.Abs(monthsApart);
        }
        
        public List<Business_Perform_St_17> GetBPPFor_OtherBranch(int BranchID,string fromdt,string todt)
        {
            List<Business_Perform_St_17> BusinessPerform_St17 = new List<Business_Perform_St_17>();
            try
            {
                DateTime FromDt = DateTime.ParseExact(fromdt, "dd/MM/yyyy", null);
                DateTime ToDt = DateTime.ParseExact(todt, "dd/MM/yyyy", null);
                query = "select * from svcf.voucher where BranchID=" + BranchID + " and  Voucher_Type = 'D' and Series='Salary' and Head_Id=90 and ChoosenDate between '" + objBAL.changedateformat(FromDt, 2) + "'" +
                        " and '" + objBAL.changedateformat(ToDt, 2) + "'";
                var VcBPPList = objBAL.GetDataTable(query);
                List<ModelVoucher> VoucherBppData = VcBPPList.DataTableToList<ModelVoucher>();
                var EmpIDList = VoucherBppData.AsEnumerable().Select(row => row.ReceievedBy).Distinct().ToList();
                List<ModelEmployee_Details> EmpDetails = null;
                Business_Perform_St_17 objBPP = new Business_Perform_St_17();
                int SlNo = 0;
                foreach (var id in EmpIDList)
                {
                    try
                    {
                        objBPP = new Business_Perform_St_17();
                        var existingRows = (from row in VoucherBppData.AsEnumerable()
                                            where row.ReceievedBy == Convert.ToString(id)
                                            select row).ToList();


                        query = "SELECT * FROM svcf.employee_details where Emp_Name='" + id + "' ";
                        var emplist = objBAL.GetDataTable(query);
                        EmpDetails = emplist.DataTableToList<ModelEmployee_Details>();
                        if (EmpDetails.Count > 0)
                        {
                            SlNo++;
                            objBPP.SlNo = SlNo;
                            objBPP.NameoftheStaff = EmpDetails[0].Emp_Name;
                            objBPP.FullResidentialAddressofStaff = EmpDetails[0].Emp_Address;
                            objBPP.NoOfPaymentsmade = VoucherBppData.Count(r => r.ReceievedBy == id);
                            var Totalpaid = VoucherBppData.Where(r => r.ReceievedBy == id).Sum(r => r.Amount);
                            objBPP.TotalAmountPaid = Totalpaid;
                            var appno = string.Empty;
                            var IdList = VoucherBppData.Where(r => r.ReceievedBy == id).Select(r => r);
                            foreach (var row in IdList)
                            {
                                // appno = appno + row.Narration.Split('#')[1].Split('-')[0] + ",";
                                appno = appno + row.Narration.Split('#')[1].Split(':')[0] + ",";
                            }
                            appno = appno.Remove(appno.Length - 1);
                            objBPP.ApprovalNoOfAdminOffice = appno;   

                            BusinessPerform_St17.Add(objBPP);
                        }
                    }
                    catch (Exception ex ) { BusinessPerform_St17.Add(objBPP); }
                }


            }
            catch (Exception exx)
            {
                //LogCls.LogError(exx, "BPP ST-17");
            }
            return BusinessPerform_St17;
        }

        public DataTable GetTelephone_Deposit(int Branchid, string toDate)
        {
            DateTime ToDate = Convert.ToDateTime(toDate);
            
            string str = @"select t3.Node as Heads,t3.NodeID,t3.ParentID,t1.Narration ,                            
                            (case when (sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )>
                            sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) and t3.TreeHint like '9,4739%') then sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )-
                            sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) else 0.00 end ) as `TD_Credit`,(case when (sum((case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ))>
                            sum((case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )) and t3.TreeHint like '9,4739%') then sum((case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ))-
                            sum((case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )) else 0.00 end ) as `TD_Debit`
                            from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID where `t1`.`BranchID` =" + Branchid + "  and `t1`.`RootID` = 9  and (t3.TreeHint like '9,1119072%' or t3.TreeHint like '9,4739%' or t3.TreeHint like '9,5730%' or t3.TreeHint like '9,167%' or t3.TreeHint like '9,1060%' or  " +
                            "t3.TreeHint like '9,58%' or  t3.TreeHint like '9,172%' or t3.TreeHint like '9,1061%') and " +
                            "t1.ChoosenDate between '2009/03/31' and '" + objBAL.changedateformat(ToDate, 2) + "' group by `t1`.`Head_ID` order by t3.ParentID asc,t1.ChoosenDate asc;";
            var dt = objBAL.GetDataTable(str);
            return dt;
        }


        public List<ModelAdvance> BindAdvance(int BranchId,string todate)
        {
            DateTime ToDate = Convert.ToDateTime(todate);
            List<ModelAdvance> objAdv = new List<ModelAdvance>();
            try
            {
                //PARTICULARS OF SUNDRIES AND ADVANCES            
                string str = @"select t3.Node as Heads,t3.NodeID,t3.ParentID,t1.Narration , 
                            (case when (sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )>sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) and t3.TreeHint like '9,1119072%') then sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )-sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) else 0.00 end ) as `EB_Credit`,
                            (case when (sum((case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ))>sum((case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )) and t3.TreeHint like '9,1119072%') then sum((case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ))-sum((case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )) else 0.00 end ) as `EB_Debit`,
                            (case when (sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )>
                            sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) and t3.TreeHint like '9,5730%') then sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )-
                            sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) else 0.00 end ) as `RA_Credit`,(case when (sum((case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ))>
                            sum((case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )) and t3.TreeHint like '9,5730%') then sum((case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ))-
                            sum((case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )) else 0.00 end ) as `RA_Debit`,
                            (case when (sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )>sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) and t3.TreeHint like '9,167%') then sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )-sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) else 0.00 end ) as `S_Credit`,
                            (case when (sum((case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ))>sum((case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )) and t3.TreeHint like '9,167%') then sum((case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ))-sum((case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )) else 0.00 end ) as `S_Debit`,
                            (case when (sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )>
                            sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) and t3.TreeHint like '9,1060%') then sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )-
                            sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) else 0.00 end ) as `PPA_Credit`,(case when (sum((case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ))>
                            sum((case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )) and t3.TreeHint like '9,1060%') then sum((case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ))-
                            sum((case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )) else 0.00 end ) as `PPA_Debit`,
                            (case when (sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )>sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) and t3.TreeHint like '9,172%') then sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )-sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) else 0.00 end ) as `VRA_Credit`,
                            (case when (sum((case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ))>sum((case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )) and t3.TreeHint like '9,172%') then sum((case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ))-sum((case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )) else 0.00 end ) as `VRA_Debit`,
                            (case when (sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )>sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) and t3.TreeHint like '9,58%') then sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )-sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) else 0.00 end ) as `SC_Credit`,
                            (case when (sum((case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ))>sum((case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )) and t3.TreeHint like '9,58%') then sum((case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ))-sum((case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )) else 0.00 end ) as `SC_Debit`
                            ,(case when (sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )>sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) and t3.TreeHint like '9,1061%') then sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )-sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) else 0.00 end ) as `SDeb_Credit`,
                            (case when (sum((case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ))>sum((case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )) and t3.TreeHint like '9,1061%') then sum((case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ))-sum((case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )) else 0.00 end ) as `SDeb_Debit` 
                            from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID where `t1`.`BranchID` =" + BranchId + "  and `t1`.`RootID` = 9  and (t3.TreeHint like '9,1119072%' or t3.TreeHint like '9,4739%' or t3.TreeHint like '9,5730%' or t3.TreeHint like '9,167%' or t3.TreeHint like '9,1060%' or  " +
                           "t3.TreeHint like '9,58%' or  t3.TreeHint like '9,172%' or t3.TreeHint like '9,1061%') and " +
                           "t1.ChoosenDate between '2009/03/31' and '" + objBAL.changedateformat(ToDate, 2) + "' group by `t1`.`Head_ID` order by t3.ParentID asc,t1.ChoosenDate asc;";

                var dt1 = objBAL.GetDataTable(str);
                objAdv = dt1.DataTableToList<ModelAdvance>();
            }
            catch (Exception) { }
            return objAdv;
        }

        public List<ModelAdvanceDecree> BindAdvancePart1(int branchid, string fromdate, string todate)
        {
            List<ModelAdvanceDecree> AdvanceDataPart1 = new List<ModelAdvanceDecree>();
            try
            {
                //DateTime FromDt = DateTime.Parse(fromdate);
                //DateTime ToDt = DateTime.Parse(todate);
                IFormatProvider culture2 = new System.Globalization.CultureInfo("fr-FR", true);
                DateTime FromDt = DateTime.Parse(fromdate, culture2, System.Globalization.DateTimeStyles.AssumeLocal);

                IFormatProvider culture3 = new System.Globalization.CultureInfo("fr-FR", true);
                DateTime ToDt = DateTime.Parse(todate, culture3, System.Globalization.DateTimeStyles.AssumeLocal);


                //PARTICULARS OF SUNDRIES AND ADVANCES AS ON " + txtToDate.Text;

                string str = @"select t3.Node as Heads,t3.NodeID,t3.ParentID,t1.Narration ,
                                                 (case when (sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )>sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) and t3.TreeHint like '9,59%') then sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )-sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) else 0.00 end ) as `Degree_Credit`,
                            (case when (sum((case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ))>sum((case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )) and t3.TreeHint like '9,59%') then sum((case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ))-sum((case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )) else 0.00 end ) as `Degree_Debit`,
                             (case when (sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )>sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) and t3.TreeHint like '9,60%') then sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )-sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) else 0.00 end ) as `Advocate_Credit`,
                            (case when (sum((case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ))>sum((case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )) and t3.TreeHint like '9,60%') then sum((case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ))-sum((case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )) else 0.00 end ) as `Advocate_Debit`,
                                                 (case when (sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )>
sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) and t3.TreeHint like '9,5731%') then sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )-
sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) else 0.00 end ) as `ACA_Credit`,(case when (sum((case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ))>
sum((case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )) and t3.TreeHint like '9,5731%') then sum((case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ))-
sum((case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )) else 0.00 end ) as `ACA_Debit`
, (case when (sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )>
sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) and t3.TreeHint like '9,5335%') then sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )-
sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) else 0.00 end ) as `Cort_Credit`,(case when (sum((case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ))>
sum((case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )) and t3.TreeHint like '9,5335%') then sum((case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ))-
sum((case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )) else 0.00 end ) as `Cort_Debit` 
,(case when (sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )>
sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) and t3.TreeHint like '9,5733%') then sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )-
sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) else 0.00 end ) as `VA_Credit`,(case when (sum((case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ))>
sum((case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )) and t3.TreeHint like '9,5733%') then sum((case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ))-
sum((case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )) else 0.00 end ) as `VA_Debit` 
,(case when (sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )>
sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) and t3.TreeHint like '9,5732%') then sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )-
sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) else 0.00 end ) as `PA_Credit`,(case when (sum((case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ))>
sum((case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )) and t3.TreeHint like '9,5732%') then sum((case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ))-
sum((case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )) else 0.00 end ) as `PA_Debit`
,(case when (sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )>
sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) and t3.TreeHint like '9,5734%') then sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )-
sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) else 0.00 end ) as `CAL_Credit`,(case when (sum((case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ))>
sum((case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )) and t3.TreeHint like '9,5734%') then sum((case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ))-
sum((case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )) else 0.00 end ) as `CAL_Debit`
,(case when (sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )>sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) and t3.TreeHint like '9,1115600%') then sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )-sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) else 0.00 end ) as `STMISS_Credit`,
  (case when (sum((case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ))>sum((case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )) and t3.TreeHint like '9,1115600%') then sum((case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ))-sum((case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )) else 0.00 end ) as `STMISS_Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID where
`t1`.`BranchID` =" + branchid + "  and `t1`.`RootID` = 9  and (t3.TreeHint like '9,5335%' or t3.TreeHint like '9,59%' or t3.TreeHint like '9,5731%' or t3.TreeHint like '9,60%' or t3.TreeHint like '9,5733%' or t3.TreeHint like '9,5732%' or t3.TreeHint like '9,5734%' or t3.TreeHint like '9,1115600%' ) and t1.ChoosenDate between '2009/03/31' and '" + objBAL.changedateformat(ToDt, 2) + "' group by `t1`.`Head_ID` order by t3.ParentID asc,t1.ChoosenDate asc;";

                DataTable dt1 = objBAL.GetDataTable(str);
                AdvanceDataPart1 = dt1.DataTableToList<ModelAdvanceDecree>();
            }
            catch (Exception err) { }
            return AdvanceDataPart1;
        }
        

        public List<ModelOtherItems> BindOtherItems(int BranchId,string todate)
        {
            //"Trial Balance Of Other Items
            List<ModelOtherItems> ListOtherItems = new List<ModelOtherItems>();
            try
            {
              //  DateTime ToDt = DateTime.Parse(todate);
               
                IFormatProvider culture3 = new System.Globalization.CultureInfo("fr-FR", true);
                DateTime ToDt = DateTime.Parse(todate, culture3, System.Globalization.DateTimeStyles.AssumeLocal);
                string str = @"select t3.NodeID,t3.Node as Heads , (case when (sum(case when t1.Voucher_Type='C' then t1.Amount 
                else 0.00 end )>sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )) then sum(case when 
                t1.Voucher_Type='C' then t1.Amount else 0.00 end )-sum(case when t1.Voucher_Type='D' then t1.Amount else 
                0.00 end ) else 0.00 end ) as `Credit`,(case when (sum((case when t1.Voucher_Type='D' then t1.Amount else 
                0.00 end ))>sum((case when t1.Voucher_Type='C' then t1.Amount else 0.00 end ))) then sum((case when 
                t1.Voucher_Type='D' then t1.Amount else 0.00 end ))-sum((case when t1.Voucher_Type='C' then t1.Amount else 
                0.00 end )) else 0.00 end ) as `Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID 
                where `t1`.`BranchID` =" + BranchId + " and `t1`.`RootID` = 4 and t1.ChoosenDate between " +
                    "'2009/03/31' and '" + objBAL.changedateformat(ToDt, 2) + "' group by `t1`.`Head_ID` order by `t1`.`Head_ID` asc";

                DataTable dt1 = objBAL.GetDataTable(str);
                ListOtherItems = dt1.DataTableToList<ModelOtherItems>();
            }
            catch (Exception) { }
            return ListOtherItems;
        }

        public List<ModelAdvanceDecree> BindAdvanceDec(string todate,int branchid)
        {
            DateTime ToDate = Convert.ToDateTime(todate);
            List<ModelAdvanceDecree> objAdvDec = new List<ModelAdvanceDecree>();
            string strPart2 = @"select t3.Node as Heads,t3.NodeID,t3.ParentID,t1.Narration ,
                                                 (case when (sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )>sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) and t3.TreeHint like '9,59%') then sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )-sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) else 0.00 end ) as `Degree_Credit`,
                                                    (case when (sum((case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ))>sum((case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )) and t3.TreeHint like '9,59%') then sum((case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ))-sum((case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )) else 0.00 end ) as `Degree_Debit`,
                                                     (case when (sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )>sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) and t3.TreeHint like '9,60%') then sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )-sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) else 0.00 end ) as `Advocate_Credit`,
                                                    (case when (sum((case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ))>sum((case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )) and t3.TreeHint like '9,60%') then sum((case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ))-sum((case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )) else 0.00 end ) as `Advocate_Debit`,
                                                                         (case when (sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )>
                        sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) and t3.TreeHint like '9,5731%') then sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )-
                        sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) else 0.00 end ) as `ACA_Credit`,(case when (sum((case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ))>
                        sum((case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )) and t3.TreeHint like '9,5731%') then sum((case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ))-
                        sum((case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )) else 0.00 end ) as `ACA_Debit`
                        , (case when (sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )>
                        sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) and t3.TreeHint like '9,5335%') then sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )-
                        sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) else 0.00 end ) as `Cort_Credit`,(case when (sum((case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ))>
                        sum((case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )) and t3.TreeHint like '9,5335%') then sum((case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ))-
                        sum((case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )) else 0.00 end ) as `Cort_Debit` 
                        ,(case when (sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )>
                        sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) and t3.TreeHint like '9,5733%') then sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )-
                        sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) else 0.00 end ) as `VA_Credit`,(case when (sum((case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ))>
                        sum((case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )) and t3.TreeHint like '9,5733%') then sum((case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ))-
                        sum((case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )) else 0.00 end ) as `VA_Debit` 
                        ,(case when (sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )>
                        sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) and t3.TreeHint like '9,5732%') then sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )-
                        sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) else 0.00 end ) as `PA_Credit`,(case when (sum((case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ))>
                        sum((case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )) and t3.TreeHint like '9,5732%') then sum((case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ))-
                        sum((case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )) else 0.00 end ) as `PA_Debit`
                        ,(case when (sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )>
                        sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) and t3.TreeHint like '9,5734%') then sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )-
                        sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) else 0.00 end ) as `CAL_Credit`,(case when (sum((case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ))>
                        sum((case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )) and t3.TreeHint like '9,5734%') then sum((case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ))-
                        sum((case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )) else 0.00 end ) as `CAL_Debit`
                        ,(case when (sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )>sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) and t3.TreeHint like '9,1115600%') then sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )-sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) else 0.00 end ) as `STMISS_Credit`,
                          (case when (sum((case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ))>sum((case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )) and t3.TreeHint like '9,1115600%') then sum((case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ))-sum((case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )) else 0.00 end ) as `STMISS_Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID where
                        `t1`.`BranchID` =" + branchid + "  and `t1`.`RootID` = 9  and (t3.TreeHint like '9,5335%' or t3.TreeHint like '9,59%' or t3.TreeHint like '9,5731%' or t3.TreeHint like '9,60%' or t3.TreeHint like '9,5733%' or t3.TreeHint like '9,5732%' or t3.TreeHint like '9,5734%' or t3.TreeHint like '9,1115600%' ) "+
                        "and t1.ChoosenDate between '2009/03/31' and '" + objBAL.changedateformat(ToDate,2) + "' group by `t1`.`Head_ID` order by t3.ParentID asc,t1.ChoosenDate asc;";
            var dt = objBAL.GetDataTable(strPart2);
            objAdvDec = dt.DataTableToList<ModelAdvanceDecree>();
            return objAdvDec;
        }

        public List<ModelFixedAssets> FixedAssetSummary1(string toDate, int branchid)
        {
            List<ModelFixedAssets> FixedAsset = new List<ModelFixedAssets>();
            try
            {
                //Trial Balance Of Investments 
                IFormatProvider culture2 = new System.Globalization.CultureInfo("fr-FR", true);
                DateTime FromDt = DateTime.Parse(toDate, culture2, System.Globalization.DateTimeStyles.AssumeLocal);
             
              //  DateTime FromDt = DateTime.Parse(toDate);
                string str = @"select t4.Node as Heads,t4.NodeID as 'PID', t3.Node as Narration,t3.NodeID as 'CNodeID',(case when (sum(case when t1.Voucher_Type='C' 
                    then t1.Amount else 0.00 end )>sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )) then 
                    sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )-sum(case when t1.Voucher_Type='D' 
                    then t1.Amount else 0.00 end ) else 0.00 end ) as `Credit`,(case when (sum((case when t1.Voucher_Type='D' 
                    then t1.Amount else 0.00 end ))>sum((case when t1.Voucher_Type='C' then t1.Amount else 0.00 end ))) then 
                    sum((case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ))-sum((case when t1.Voucher_Type='C' 
                    then t1.Amount else 0.00 end )) else 0.00 end ) as `Debit` from voucher as t1  left Join headstree as t3 
                    on t1.Head_ID=t3.NodeID left Join headstree as t4 on (t3.ParentID=t4.NodeID) where 
                    t4.NodeID in (14,15,17,18) and `t1`.`BranchID` =" + branchid + " and `t1`.`RootID` = 2 and t1.ChoosenDate between '2009/03/01' " +
                   " and '" + objBAL.changedateformat(FromDt, 2) + "' group by `t1`.`Head_ID`";
                DataTable dt1 = objBAL.GetDataTable(str);
                FixedAsset = dt1.DataTableToList<ModelFixedAssets>();
            }
            catch (Exception) { }
            return FixedAsset;
        }

        public List<ModelFixedAssets> FixedAssetSummary2(string toDate, int branchid)
        {
            List<ModelFixedAssets> FixedAsset = new List<ModelFixedAssets>();
            try
            {
                //Trial Balance Of Investments 
               // DateTime FromDt = DateTime.Parse(toDate);
                IFormatProvider culture2 = new System.Globalization.CultureInfo("fr-FR", true);
                DateTime FromDt = DateTime.Parse(toDate, culture2, System.Globalization.DateTimeStyles.AssumeLocal);
               
                string str = @"select t4.Node as Heads,t4.NodeID as 'PID', t3.Node as Narration,t3.NodeID as 'CNodeID',(case when (sum(case when t1.Voucher_Type='C' 
                    then t1.Amount else 0.00 end )>sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )) then 
                    sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )-sum(case when t1.Voucher_Type='D' 
                    then t1.Amount else 0.00 end ) else 0.00 end ) as `Credit`,(case when (sum((case when t1.Voucher_Type='D' 
                    then t1.Amount else 0.00 end ))>sum((case when t1.Voucher_Type='C' then t1.Amount else 0.00 end ))) then 
                    sum((case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ))-sum((case when t1.Voucher_Type='C' 
                    then t1.Amount else 0.00 end )) else 0.00 end ) as `Debit` from voucher as t1  left Join headstree as t3 
                    on t1.Head_ID=t3.NodeID left Join headstree as t4 on (t3.ParentID=t4.NodeID) where 
                    t4.NodeID in (1113639) and `t1`.`BranchID` =" + branchid + " and `t1`.`RootID` = 2 and t1.ChoosenDate between '2009/03/01' " +
                   " and '" + objBAL.changedateformat(FromDt, 2) + "' group by `t1`.`Head_ID`";
                DataTable dt1 = objBAL.GetDataTable(str);
                FixedAsset = dt1.DataTableToList<ModelFixedAssets>();
            }
            catch (Exception) { }
            return FixedAsset;
        }


        public List<ModelBranchTrialBalance> BindBranches(int Branchid,string todate)
        {
            //"Trial Balance Of Branches as on
            List<ModelBranchTrialBalance> TrialBalList = new List<ModelBranchTrialBalance>();

            try
            {
               // DateTime ToDt = DateTime.Parse(todate);
             
                IFormatProvider culture3 = new System.Globalization.CultureInfo("fr-FR", true);
                DateTime ToDt = DateTime.Parse(todate, culture3, System.Globalization.DateTimeStyles.AssumeLocal);
                ModelBranchTrialBalance objBranchBal = new ModelBranchTrialBalance();
                int icount = 0;
                decimal decCredit = 0, decDebit = 0;
                string str = @"select t1.ChoosenDate as `Date`,t3.Node as Branch , (case when (sum(case when " +
                    "t1.Voucher_Type='C' then t1.Amount else 0.00 end )>sum(case when t1.Voucher_Type='D' then t1.Amount else " +
                    "0.00 end )) then sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )-sum(case when " +
                    "t1.Voucher_Type='D' then t1.Amount else 0.00 end ) else 0.00 end ) as `Credit`,(case when " +
                    "(sum((case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ))>sum((case when t1.Voucher_Type='C' " +
                    "then t1.Amount else 0.00 end ))) then sum((case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ))" +
                    "-sum((case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )) else 0.00 end ) as `Debit` from " +
                    "voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID where " +
                    "`t1`.`BranchID` =" + Branchid + " and `t1`.`RootID` = 1 and t1.ChoosenDate between '2009/03/31' and '" + objBAL.changedateformat(ToDt, 2) + "' group by `t1`.`Head_ID`";

                var VCList = objBAL.GetDataTable(str);
                List<ModelBranchTrial> VoucherData = VCList.DataTableToList<ModelBranchTrial>();
                foreach (var row in VoucherData)
                {
                    icount++;
                    objBranchBal = new ModelBranchTrialBalance();
                    objBranchBal.Slno = icount;
                    objBranchBal.NameoftheBranch = row.Branch;
                    objBranchBal.Credit = row.Credit;
                    objBranchBal.Debit = row.Debit;
                    TrialBalList.Add(objBranchBal);
                }
                decCredit = TrialBalList.Sum(r => r.Credit);   // Convert.ToDecimal(dt.Compute("Sum(Credit)", ""));
                decDebit = TrialBalList.Sum(r => r.Debit);     // Convert.ToDecimal(dt.Compute("Sum(Debit)", ""));
                icount++;
                objBranchBal.Slno = icount;
                objBranchBal.NameoftheBranch = "TOTAL";
                objBranchBal.Credit = decCredit;
                objBranchBal.Debit = decDebit;
                TrialBalList.Add(objBranchBal);

                icount++;
                objBranchBal.Slno = icount;

                if (decCredit == decDebit)
                {
                    objBranchBal.NameoftheBranch = "Balance";
                    objBranchBal.Credit = 0;
                    objBranchBal.Debit = 0;
                }
                else if (decCredit > decDebit)
                {
                    objBranchBal.NameoftheBranch = "Balance CR";
                    objBranchBal.Credit = decCredit - decDebit;
                    objBranchBal.Debit = 0;
                }
                else if (decCredit < decDebit)
                {
                    objBranchBal.NameoftheBranch = "Balance DR";
                    objBranchBal.Credit = 0;
                    objBranchBal.Debit = decDebit - decCredit;
                }
                TrialBalList.Add(objBranchBal);                
            }
            catch (Exception) { }
            return TrialBalList;
        }

     
        public List<DebtsoutsExceed6month> GetDebtsOutExceed_6months(string todt,int branchid,string fromdt)
        {
            List<DebtsoutsExceed6month> DebsOutExceed = new List<DebtsoutsExceed6month>();
            try
            {
                //DateTime ToDt = DateTime.Parse(todt);
                //DateTime Fromdate = DateTime.Parse(fromdt);

                IFormatProvider culture2 = new System.Globalization.CultureInfo("fr-FR", true);
                DateTime FromDt = DateTime.Parse(fromdt, culture2, System.Globalization.DateTimeStyles.AssumeLocal);

                IFormatProvider culture3 = new System.Globalization.CultureInfo("fr-FR", true);
                DateTime ToDt = DateTime.Parse(todt, culture3, System.Globalization.DateTimeStyles.AssumeLocal);

                int Count = 0;
                var grpList = objBAL.GetDataTable("select Head_Id from groupmaster where branchid=" + branchid + "");
                DebtsoutsExceed6month objExceedDetails = new DebtsoutsExceed6month();
                List<ModelTokenDetails> VoucherData = null;
                string TotaldueAmount = "", Parr="";
      //          DateTime s1 = Convert.ToDateTime(todt);
                DateTime newDate = ToDt.AddMonths(-6);
                var s3 = newDate.ToString("yyyy-MM-dd");
                foreach (var gpid in grpList.AsEnumerable())
                {

                    //query = @"SELECT * FROM(SELECT DISTINCT vc.Head_Id, vc.ChitGroupId, vc.Amount, vc.ChoosenDate, 
                    //         mg.GrpMemberID, vc.Voucher_Type, vc.Other_Trans_Type, vc.Series, vc.Narration
                    //         FROM voucher as vc  join membertogroupmaster as mg on mg.Head_Id = vc.Head_Id
                    //         where vc.Voucher_Type = 'C' and vc.Other_Trans_Type <> 5 and vc.Series <> 'PAYMENT' and 
                    //         vc.Series <> 'VOUCHER' and vc.BranchID = " + branchid + " and vc.ChitGroupId = " + gpid.ItemArray[0] + " and vc.Amount > 0 and " +
                    //  " vc.Head_Id  NOT IN " +
                    //  "(SELECT DISTINCT vc.Head_Id" +
                    // " FROM voucher as vc" +
                    // " WHERE  vc.Other_Trans_Type <> 5 and vc.Series <> 'PAYMENT' and vc.Series <> 'VOUCHER' and" +
                    // " vc.ChitGroupId = " + gpid.ItemArray[0] + " and  vc.Amount > 0 and vc.choosendate <= '" + s3 + "')" +
                    // " ) query;";


















                    query = @"SELECT * FROM(SELECT DISTINCT vc.Head_Id, vc.ChitGroupId, vc.Amount, vc.ChoosenDate, 
                             mg.GrpMemberID, vc.Voucher_Type, vc.Other_Trans_Type, vc.Series, vc.Narration
                             FROM svcf.voucher as vc  join svcf.membertogroupmaster as mg on mg.Head_Id = vc.Head_Id
                             where vc.Voucher_Type = 'C' and vc.Other_Trans_Type <> 5 and vc.Series <> 'PAYMENT' and 
                             vc.Series <> 'VOUCHER' and vc.BranchID = " + branchid + " and vc.ChitGroupId = " + gpid.ItemArray[0] + " and vc.Amount > 0 and " +
                            " vc.Head_Id  NOT IN " +
                            "(SELECT DISTINCT vc.Head_Id" +
                           " FROM svcf.voucher as vc" +
                           " WHERE  vc.Other_Trans_Type <> 5 and vc.Series <> 'PAYMENT' and vc.Series <> 'VOUCHER' and" +
                           " vc.ChitGroupId = " + gpid.ItemArray[0] + " and  vc.Amount > 0 and vc.choosendate >= DATE_ADD('" + objBAL.changedateformat(ToDt, 2) + "', interval - 6 month))" +
                           " ) query;";
                    //query = @"SELECT * FROM(SELECT DISTINCT vc.Head_Id, vc.ChitGroupId, vc.Amount, vc.ChoosenDate, 
                    //         mg.GrpMemberID, vc.Voucher_Type, vc.Other_Trans_Type, vc.Series, vc.Narration
                    //         FROM voucher as vc  join membertogroupmaster as mg on mg.Head_Id = vc.Head_Id
                    //         where vc.Voucher_Type = 'C' and vc.Other_Trans_Type <> 5 and vc.Series <> 'PAYMENT' and 
                    //          vc.BranchID = " + branchid + " and vc.ChitGroupId = " + gpid.ItemArray[0] + " and vc.Amount > 0 and " +
                    //       " vc.Head_Id  NOT IN " +
                    //       "(SELECT DISTINCT vc.Head_Id" +
                    //      " FROM voucher as vc" +
                    //      " WHERE  vc.Other_Trans_Type <> 5 and vc.Series <> 'PAYMENT' and vc.Series <> 'VOUCHER' and" +
                    //      " vc.ChitGroupId = " + gpid.ItemArray[0] + " and  vc.Amount > 0 and vc.choosendate >= DATE_ADD('" + objBAL.changedateformat(ToDt, 2) + "', interval - 6 month))" +
                    //      " ) query;";
                        var HeadList_Todt = objBAL.GetDataTable(query);
                    VoucherData = HeadList_Todt.DataTableToList<ModelTokenDetails>();

                    var uniqueHeadId_List = VoucherData.Select(r => r.Head_Id).Distinct().ToList();

                    foreach (var hdid in uniqueHeadId_List)
                    {
                        try
                        {
                            objExceedDetails = new DebtsoutsExceed6month();
                            var hdRow = VoucherData.Where(x => x.Head_Id == hdid).OrderByDescending(x => x.ChoosenDate).First();
                            TotaldueAmount = objBAL.GetSingleValue(@"select sum(CurrentDueAmount) from auctiondetails where GroupID=" + hdRow.ChitGroupId + " and AuctionDate<='" + objBAL.changedateformat(ToDt, 2) + "';");
                            query = @"select (case when( tp1.PaymentDate<='" + objBAL.changedateformat(ToDt, 2) + "' ) then " +
                            "(case when( (" + TotaldueAmount + "-sum(case when (v1.Voucher_Type='C' and v1.trans_Type<>2 and " +
                            "v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) +sum(case when (v1.Voucher_Type='D' and v1.trans_Type<>2 " +
                            "and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) )>0.00) then (" + TotaldueAmount + "-sum(case when " +
                            "(v1.Voucher_Type='C' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 " +
                            "end) +sum(case when (v1.Voucher_Type='D' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then " +
                            "v1.Amount else 0.00 end) ) else 0.00 end) else 0.00 end ) as PArrier from membertogroupmaster as " +
                            "mg1 join voucher as v1 on v1.Head_Id=mg1.Head_Id join groupmaster as vgwd1 on " +
                            "vgwd1.`Head_Id`=" + hdRow.ChitGroupId + " left join trans_payment as tp1 on " +
                            "v1.Head_Id =tp1.TokenNumber join membermaster as mm on (mg1.MemberID=mm.MemberIDNew) where " +
                            "mg1.BranchID=" + branchid + " and mg1.GroupID=" + hdRow.ChitGroupId + " and " +
                            "v1.Head_Id=" + hdRow.Head_Id + " and v1.ChoosenDate<= '" + objBAL.changedateformat(ToDt, 2) + "' " +
                            "group by v1.Head_Id order by cast(digits(mg1.GrpMemberID) as unsigned),v1.Voucher_Type DESC ;";
                            Parr = objBAL.GetSingleValue(query);
                            if ((Parr != "") && (Parr != "0.00"))
                            {
                                    objExceedDetails = new DebtsoutsExceed6month();
                                    Count++;
                                    objExceedDetails.SlNo = Count;
                                    objExceedDetails.ChitNumber = hdRow.GrpMemberID;
                                    objExceedDetails.NameoftheParty = objBAL.GetSingleValue("select MemberName from svcf.membertogroupmaster where Head_Id=" + hdRow.Head_Id + ";");
                                    objExceedDetails.ArrearsfromDate = hdRow.ChoosenDate;
                                    string Amount = objBAL.GetSingleValue("Select COALESCE((select sum(amount) from voucher where Head_Id=" + hdid + " and Voucher_Type='C' and ChoosenDate <= '" + objBAL.changedateformat(ToDt, 2) + "'and Other_Trans_Type<>5),0)"
                                            + "-COALESCE((select sum(amount) from voucher where Head_Id=" + hdRow.Head_Id + " and Voucher_Type='D' and Trans_Type=0 and ChoosenDate <= '" + objBAL.changedateformat(ToDt, 2) + "' and Other_Trans_Type<>5),0) as amount");

                                    var dtInit = objBAL.GetDataTable("select * from svcf.groupmaster where BranchID='" + branchid + "' and ChitEndDate<='" + objBAL.changedateformat(ToDt, 2) + "' and Head_Id=" + hdRow.ChitGroupId);
                                    if (dtInit.Rows.Count > 0)
                                    {
                                        objExceedDetails.Terminated_AmountinChitAccnt = Convert.ToDecimal(Parr);
                                    }
                                    else
                                    {
                                        objExceedDetails.Running_AmountinChitAccnt = Convert.ToDecimal(Parr);

                                    }
                                    objExceedDetails.ChitLoan = 0;


                                    var dateeoff = objBAL.GetDataTable(@"SELECT * FROM(SELECT  DISTINCT  vc.ChoosenDate as date
                             FROM voucher as vc  join membertogroupmaster as mg on mg.Head_Id = vc.Head_Id
                             where vc.Voucher_Type = 'C' and vc.Other_Trans_Type <> 5 and vc.Series <> 'PAYMENT' and 
                               vc.BranchID = 1481 and vc.ChitGroupId = '" + hdRow.ChitGroupId + "' and vc.Amount > 0 "
                              + "and  vc.Head_Id='" + hdRow.Head_Id + "' ORDER BY date DESC limit 1)  query;");
                                    if (dateeoff.Rows.Count != 0)
                                    {
                                        // var dateform =(dateeoff.Rows[0]["date"]);
                                        objExceedDetails.ArrearsfromDate = Convert.ToDateTime(dateeoff.Rows[0]["date"]);
                                    }


                                    //  objExceedDetails.ArrearsfromDate = Convert.ToDateTime(GetLastRealisationDate(Convert.ToDecimal(Amount), hdRow.ChitGroupId, branchid, todt));
                                    DebsOutExceed.Add(objExceedDetails);
                                
                            }
                        }
                        catch (Exception) { }
                    }
                }
            }
            catch (Exception) { }

            return DebsOutExceed;
        }

        public string GetLastRealisationDate(decimal summary,int groupid,int branchid,string todt)
        {
            int paidinstno = 0;
            string lastRealisationDate = "";
            try
            {
               // DateTime ToDt = DateTime.Parse(todt);
               
                IFormatProvider culture3 = new System.Globalization.CultureInfo("fr-FR", true);
                DateTime ToDt = DateTime.Parse(todt, culture3, System.Globalization.DateTimeStyles.AssumeLocal);

                int Maxdraw = 0;
                decimal adddueamount = 0, amountdiff = 0;
                Maxdraw = objBAL.GetScalarDataInt("SELECT max(DrawNO) FROM svcf.auctiondetails where GroupID='" + groupid + "' and AuctionDate <= '" + objBAL.changedateformat(ToDt,2) + "'");

                var getallauction = objBAL.GetDataTable("select * from svcf.auctiondetails where GroupID='" + groupid + "' and AuctionDate <= '" + objBAL.changedateformat(ToDt, 2) + "'");
                
                for (int i = 0; i < getallauction.Rows.Count; i++)
                {

                    if (adddueamount == 0)
                        adddueamount = Convert.ToDecimal(getallauction.Rows[i]["CurrentDueAmount"]);
                    else
                        adddueamount = adddueamount + Convert.ToDecimal(getallauction.Rows[i]["CurrentDueAmount"]);


                    if (Convert.ToDecimal(summary) == adddueamount)
                    {
                        paidinstno = Convert.ToInt16(getallauction.Rows[i]["DrawNO"]);
                        if (paidinstno != Convert.ToInt32(getallauction.Rows.Count))
                        {
                            paidinstno = paidinstno + 1;
                        }
                        adddueamount = 0;
                        break;
                    }
                    if (Convert.ToDecimal(summary) < adddueamount)
                    {
                        amountdiff = (adddueamount - Convert.ToDecimal(summary));

                        if (amountdiff < Convert.ToDecimal(getallauction.Rows[i]["CurrentDueAmount"]))
                        //if (!(Convert.ToDecimal(amountdiff) < Convert.ToDecimal(getallauction.Rows[i]["CurrentDueAmount"])))
                        {
                            paidinstno = Convert.ToInt16(getallauction.Rows[i]["DrawNO"]);
                            adddueamount = 0;
                            break;
                        }
                        if (!(Convert.ToDecimal(amountdiff) < Convert.ToDecimal(getallauction.Rows[i]["CurrentDueAmount"])))
                        {
                            paidinstno = Convert.ToInt16(getallauction.Rows[i]["DrawNO"]);
                            adddueamount = 0;
                            break;
                        }
                    }

                    if (!(Convert.ToDecimal(summary) == adddueamount) && !(Convert.ToDecimal(summary) < adddueamount))
                    {
                        paidinstno = 0;
                    }
                }
                lastRealisationDate = objBAL.GetSingleValue("select date_format(AuctionDate,'%d/%m/%Y') as  AuctionDate from svcf.auctiondetails where DrawNO=" + paidinstno + " and GroupID=" + groupid + ";");
                    
            }
            catch (Exception) { }

            return lastRealisationDate;
        }


        public List<Bind12Heads> Get12Heads(string fromdate,string todate,int BranchId)
        {
            List<Bind12Heads> Obj12Heads = new List<Bind12Heads>();
            try
            {
                // "12 Heads 
                //DateTime ToDt = DateTime.Parse(todate);
                //DateTime Fromdate = DateTime.Parse(fromdate);

                IFormatProvider culture2 = new System.Globalization.CultureInfo("fr-FR", true);
                DateTime Fromdate = DateTime.Parse(fromdate, culture2, System.Globalization.DateTimeStyles.AssumeLocal);

                IFormatProvider culture3 = new System.Globalization.CultureInfo("fr-FR", true);
                DateTime ToDt = DateTime.Parse(todate, culture3, System.Globalization.DateTimeStyles.AssumeLocal);

                var dt = objBAL.GetDataTable(@"SELECT voucher.RootID,`headstree`.`Node`,(case when (sum((case when voucher.Voucher_Type='C' then "+
                    "voucher.Amount else 0.00 end ))>sum((case when voucher.Voucher_Type='D' then voucher.Amount else 0.00 "+
                    "end ))) then sum((case when voucher.Voucher_Type='C' then voucher.Amount else 0.00 end ))-sum((case "+
                    "when voucher.Voucher_Type='D' then voucher.Amount else 0.00 end )) else 0.00 end ) as `Credit`,"+
                    "(case when (sum((case when voucher.Voucher_Type='D' then voucher.Amount else 0.00 end ))>sum((case when "+
                    "voucher.Voucher_Type='C' then voucher.Amount else 0.00 end ))) then sum((case when voucher.Voucher_Type='D' "+
                    "then voucher.Amount else 0.00 end ))-sum((case when voucher.Voucher_Type='C' then voucher.Amount else "+
                    "0.00 end )) else 0.00 end ) as `Debit` FROM `svcf`.`voucher` join headstree on (voucher.RootID=headstree.NodeID) where "+
                    "voucher.BranchID=" + BranchId + " and voucher.ChoosenDate between '2009/03/31' and '" + objBAL.changedateformat(ToDt,2) + "' group by voucher.RootID");
                Obj12Heads = dt.DataTableToList<Bind12Heads>();
             
                //string str = "select cast(NodeID as unsigned) as `SNo`, Node as Heads,0.00 as `Credit`,0.00 as `Debit`,'' as Remarks from headstree where NodeID not in (" + DefaultId + ") and ParentID=0";
                //var dt1 = objBAL.GetDataTable(str);
                //for (int i = dt1.Rows.Count - 1; i >= 0; i--)
                //{
                //    if (dt1.Rows[i][1] == DBNull.Value)
                //        dt1.Rows[i].Delete();
                //}
                //List<Bind12Heads> Obj12Heads1 = new List<Bind12Heads>();
                //Obj12Heads1 = dt1.DataTableToList<Bind12Heads>();
                //foreach(var item in Obj12Heads1)
                //{
                //    Obj12Heads.Add(item);
                //}             
            }
            catch (Exception) { }
            return Obj12Heads;
        }
        

        public DataTable BindDecree(string fromdate,string todate,int branchid)
        {
            //"Decree Statement as on          
            DateTime FromDt = Convert.ToDateTime(fromdate);
            DateTime ToDt = Convert.ToDateTime(todate);

            DataTable dtDistinct = objBAL.GetDataTable("SELECT v1.TransactionKey,ht2.NodeID, ht2.Node as Head,(sum(case when (v1.Voucher_Type='C' and ht2.TreeHint like '7,51%') then v1.Amount else 0.00 end))  as `Cr.total`,(case when (sum(case when (v1.Voucher_Type='C' and ht2.TreeHint like '7,51%') then v1.Amount else 0.00 end )>sum(case when (v1.Voucher_Type='D' and ht2.TreeHint like '7,51%') then v1.Amount else 0.00 end )) then sum(case when (v1.Voucher_Type='C' and ht2.TreeHint like '7,51%') then v1.Amount else 0.00 end )-sum(case when (v1.Voucher_Type='D' and ht2.TreeHint like '7,51%')  then v1.Amount else 0.00 end ) else 0.00 end ) as `Bal.Credit`,(case when (sum((case when (v1.Voucher_Type='D' and ht2.TreeHint like '7,51%') then v1.Amount else 0.00 end ))>sum((case when (v1.Voucher_Type='C' and ht2.TreeHint like '7,51%') then v1.Amount else 0.00 end ))) then sum((case when (v1.Voucher_Type='D' and ht2.TreeHint like '7,51%') then v1.Amount else 0.00 end ))-sum((case when (v1.Voucher_Type='C' and ht2.TreeHint like '7,51%') then v1.Amount else 0.00 end )) else 0.00 end ) as `Bal.Debit`,(case when (sum(case when (v1.Voucher_Type='C' "+
                "and ht2.TreeHint like '7,52%') then v1.Amount else 0.00 end )>sum(case when (v1.Voucher_Type='D' and "+
                "ht2.TreeHint like '7,52%') then v1.Amount else 0.00 end )) then sum(case when (v1.Voucher_Type='C' and "+
                "ht2.TreeHint like '7,52%') then v1.Amount else 0.00 end )-sum(case when (v1.Voucher_Type='D' and "+
                "ht2.TreeHint like '7,52%')  then v1.Amount else 0.00 end ) else 0.00 end ) as `Court.Credit`,(case when "+
                "(sum((case when (v1.Voucher_Type='D' and ht2.TreeHint like '7,52%') then v1.Amount else 0.00 end ))>sum((case "+
                "when (v1.Voucher_Type='C' and ht2.TreeHint like '7,52%') then v1.Amount else 0.00 end ))) then sum((case "+
                "when (v1.Voucher_Type='D' and ht2.TreeHint like '7,52%') then v1.Amount else 0.00 end ))-sum((case when "+
                "(v1.Voucher_Type='C' and ht2.TreeHint like '7,52%') then v1.Amount else 0.00 end )) else 0.00 end ) as "+
                "`Court.Debit`,(case when (sum(case when (v1.Voucher_Type='C' and ht2.TreeHint like '7,4638%') then v1.Amount "+
                "else 0.00 end )>sum(case when (v1.Voucher_Type='D' and ht2.TreeHint like '7,4638%') then v1.Amount else 0.00 "+
                "end )) then sum(case when (v1.Voucher_Type='C' and ht2.TreeHint like '7,4638%') then v1.Amount else 0.00 "+
                "end )-sum(case when (v1.Voucher_Type='D' and ht2.TreeHint like '7,4638%')  then v1.Amount else 0.00 end ) "+
                "else 0.00 end ) as `Advocate.Credit`,(case when (sum((case when (v1.Voucher_Type='D' and ht2.TreeHint like "+
                "'7,4638%') then v1.Amount else 0.00 end ))>sum((case when (v1.Voucher_Type='C' and ht2.TreeHint like "+
                "'7,4638%') then v1.Amount else 0.00 end ))) then sum((case when (v1.Voucher_Type='D' and ht2.TreeHint "+
                "like '7,4638%') then v1.Amount else 0.00 end ))-sum((case when (v1.Voucher_Type='C' and ht2.TreeHint "+
                "like '7,4638%') then v1.Amount else 0.00 end )) else 0.00 end ) as `Advocate.Debit`  FROM `voucher` as v1 "+
                "join headstree as ht2 on v1.Head_Id=ht2.NodeID where v1.RootID=7 and v1.BranchID=" + branchid + " and "+
                "v1.ChoosenDate<='" + objBAL.changedateformat(ToDt,2) + "' group by ht2.NodeID order by ht2.NodeID asc");
           
            int iCount = 0;
           
            var dtBind = new DataTable();
            DataRow dr = dtBind.NewRow();
            dtBind.Columns.Add("SlNo");
            dtBind.Columns.Add("CC No");
            dtBind.Columns.Add("EP No./OS No./ARC No./ARB No.");
            dtBind.Columns.Add("ChitName", typeof(string));
            dtBind.Columns.Add("Name");
            dtBind.Columns.Add("Totalamount", typeof(decimal));
            dtBind.Columns.Add("Date");
            dtBind.Columns.Add("AmountReceived", typeof(decimal));
            dtBind.Columns.Add("CreditDECREE", typeof(decimal));
            dtBind.Columns.Add("DebitDECREE", typeof(decimal));
            dtBind.Columns.Add("CreditCOST", typeof(decimal));
            dtBind.Columns.Add("DebitCOST", typeof(decimal));
            dtBind.Columns.Add("CreditAdvocate", typeof(decimal));
            dtBind.Columns.Add("DebitAdvocate", typeof(decimal));
          
            iCount = 0;
          
            var ChitName = dtDistinct.AsEnumerable().Where(r => (r.Field<decimal>("Bal.Credit") > 0) || (r.Field<decimal>("Bal.Debit") > 0) ||
                                                                (r.Field<decimal>("Court.Credit") > 0) || (r.Field<decimal>("Court.Debit") > 0) ||
                                                                (r.Field<decimal>("Advocate.Credit") > 0) || (r.Field<decimal>("Advocate.Debit") > 0)).
                                                     Select(row => row.Field<string>("Head")).Distinct().ToList();
            foreach (var chname in ChitName)
            {
                var existingRows = (from row in dtDistinct.AsEnumerable()
                                    where row.Field<string>("Head") == Convert.ToString(chname)
                                    select row).ToList();
                iCount++;
                dr["SlNo"] = iCount + 1;
                dr["CC No"] = objBAL.GetSingleValue("SELECT CC FROM svcf.transcourt where TransactionKey=" + existingRows[0].ItemArray[0]);
                dr["EP No./OS No./ARC No./ARB No."] = objBAL.GetSingleValue("SELECT Number FROM svcf.transcourt where TransactionKey=" + existingRows[0].ItemArray[0]);
                dr["ChitName"] = objBAL.GetSingleValue("SELECT ChitName FROM svcf.chitheads where HeadId=" + existingRows[0].ItemArray[1]);
                dr["Name"] = objBAL.GetSingleValue("SELECT MemberName FROM svcf.chitheads where HeadId=" + existingRows[0].ItemArray[1]);

                string amtRec = Convert.ToString(objBAL.GetSingleValue("select t1.Amount as `Credit` from voucher as t1 left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left join headstree as t8 on (t1.ChitGroupID=t8.NodeID) left join headstree as t9 on (t9.NodeID=t1.Head_Id) left join headstree as t10 on (t10.NodeID=t9.ParentID)where t1.Voucher_Type='C' and `t1`.`BranchID` =" + branchid + "  and `t1`.`RootID` = 7  and t1.Amount<>0 and t1.ChoosenDate<='" + objBAL.changedateformat(ToDt,2) + "' and t1.IsDeleted=0 and t1.Head_Id=" + existingRows[0].ItemArray[1] + " order by t1.ChoosenDate desc Limit 1"));
                if (amtRec != "")
                {

                    //dr["AmountReceived"] = Convert.ToDecimal(objBAL.GetSingleValue("select (case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )as `Credit` from voucher as t1 left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left join headstree as t8 on (t1.ChitGroupID=t8.NodeID) left join headstree as t9 on (t9.NodeID=t1.Head_Id) left join headstree as t10 on (t10.NodeID=t9.ParentID)where  `t1`.`BranchID` =" + branchid + " and `t1`.`RootID` = 7  and t1.Amount<>0 and t1.ChoosenDate<='" + objBAL.changedateformat(ToDt,2) + "' and t1.IsDeleted=0 and t1.Head_Id=" + dtDistinct.Rows[i]["NodeID"] + " order by t1.ChoosenDate desc Limit 1"));
                    dr["AmountReceived"] = Convert.ToDecimal(objBAL.GetSingleValue("select t1.Amount as `Credit` from voucher as t1 left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left join headstree as t8 on (t1.ChitGroupID=t8.NodeID) left join headstree as t9 on (t9.NodeID=t1.Head_Id) left join headstree as t10 on (t10.NodeID=t9.ParentID)where t1.Voucher_Type='C' and `t1`.`BranchID` =" + branchid + "  and `t1`.`RootID` = 7  and t1.Amount<>0 and t1.ChoosenDate<='" + objBAL.changedateformat(ToDt,2) + "' and t1.IsDeleted=0 and t1.Head_Id=" + existingRows[0].ItemArray[1] + " order by t1.ChoosenDate desc Limit 1"));
                    dr["Totalamount"] = Convert.ToDecimal(objBAL.GetSingleValue("select(sum(case when (v1.Voucher_Type='C' and ht2.TreeHint like '7,51%') then v1.Amount else 0.00 end))  as `Cr.total`FROM `voucher` as v1  join headstree as ht2 on v1.Head_Id=ht2.NodeID where v1.RootID=7 and v1.BranchID=" + branchid + "  and ht2.NodeID=" + existingRows[0].ItemArray[1] + " and v1.ChoosenDate<='" + objBAL.changedateformat(ToDt,2) + "'"));
                }
                else
                {
                    dr["AmountReceived"] = "0.00";
                    dr["Totalamount"] = "0.00";
                }
                string date = objBAL.GetSingleValue("select DATE_FORMAT( t1.`ChoosenDate`, '%d/%m/%Y') as `Date`from voucher as t1 left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left join headstree as t8 on (t1.ChitGroupID=t8.NodeID) left join headstree as t9 on (t9.NodeID=t1.Head_Id) left join headstree as t10 on (t10.NodeID=t9.ParentID)where t1.Voucher_Type='C' and `t1`.`BranchID` =" + branchid + " and `t1`.`RootID` = 7  and t1.Amount<>0 and t1.ChoosenDate<='" + objBAL.changedateformat(ToDt,2) + "' and t1.IsDeleted=0 and t1.Head_Id=" + existingRows[0].ItemArray[1] + " order by t1.ChoosenDate desc Limit 1");
                if (date != "")
                {
                    dr["Date"] = objBAL.GetSingleValue("select DATE_FORMAT( t1.`ChoosenDate`, '%d/%m/%Y') as `Date`from voucher as t1 left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left join headstree as t8 on (t1.ChitGroupID=t8.NodeID) left join headstree as t9 on (t9.NodeID=t1.Head_Id) left join headstree as t10 on (t10.NodeID=t9.ParentID)where t1.Voucher_Type='C' and `t1`.`BranchID` =" + branchid + " and `t1`.`RootID` = 7  and t1.Amount<>0 and t1.ChoosenDate<='" + objBAL.changedateformat(ToDt,2) + "' and t1.IsDeleted=0 and t1.Head_Id=" + existingRows[0].ItemArray[1] + " order by t1.ChoosenDate desc Limit 1");
                    dr["Totalamount"] = Convert.ToDecimal(objBAL.GetSingleValue("select(sum(case when (v1.Voucher_Type='C' and ht2.TreeHint like '7,51%') then v1.Amount else 0.00 end))  as `Cr.total`FROM `voucher` as v1  join headstree as ht2 on v1.Head_Id=ht2.NodeID where v1.RootID=7 and v1.BranchID=" + branchid + "  and ht2.NodeID=" + existingRows[0].ItemArray[1] + " and v1.ChoosenDate<='" + objBAL.changedateformat(ToDt,2) + "'"));
                    // dr["Totalamount"] = Convert.ToDecimal(dtDistinct.Rows[i]["Cr.total"]);
                }
                else
                {
                    dr["Date"] = "";
                    dr["Totalamount"] = "0.00";
                }

                foreach (var row in existingRows)
                {
                    if (Convert.ToDecimal(row.ItemArray[4]) > 0)
                    {
                        dr["CreditDECREE"] = row.ItemArray[4];                      
                    }
                    else if (Convert.ToDecimal(row.ItemArray[5]) > 0)
                    {
                        dr["DebitDECREE"] = row.ItemArray[5];                       
                    }

                    else if (Convert.ToDecimal(row.ItemArray[6]) > 0)
                    {
                        dr["CreditCOST"] = row.ItemArray[6];                        
                    }
                    else if (Convert.ToDecimal(row.ItemArray[7]) > 0)
                    {
                        dr["DebitCOST"] = row.ItemArray[7];                      
                    }
                    else if (Convert.ToDecimal(row.ItemArray[8]) > 0)
                    {
                        dr["CreditAdvocate"] = row.ItemArray[8];                        
                    }
                    else if (Convert.ToDecimal(row.ItemArray[9]) > 0)
                    {
                        dr["DebitAdvocate"] = row.ItemArray[9];                        
                    }

                }
                dtBind.Rows.Add(dr.ItemArray);

                dr["CreditDECREE"] = 0;
                dr["CreditCOST"] = 0;
                dr["DebitDECREE"] = 0;
                dr["CreditCOST"] = 0;
                dr["DebitCOST"] = 0;
                //Advocate
                dr["CreditAdvocate"] = 0;
                dr["DebitAdvocate"] = 0;           
                dr["AmountReceived"] = "0.00";
                dr["Totalamount"] = "0.00";
                dr["CC No"] = "";
                dr["EP No./OS No./ARC No./ARB No."] = "";
                dr["ChitName"] = "";
                dr["Name"] = "";

            }
            
            if (dtBind.Rows.Count > 0)
            {
                try
                {
                    decimal Deecreeamount = Convert.ToDecimal(dtBind.Compute("sum(AmountReceived)", ""));
                    decimal Deecreetotal = Convert.ToDecimal(dtBind.Compute("sum(Totalamount)", ""));
                    decimal DeecreeCR = Convert.ToDecimal(dtBind.Compute("sum(CreditDECREE)", ""));
                    decimal DeecreeDR = Convert.ToDecimal(dtBind.Compute("sum(DebitDECREE)", ""));
                    decimal CostCR = Convert.ToDecimal(dtBind.Compute("sum(CreditCOST)", ""));
                    decimal CostDR = Convert.ToDecimal(dtBind.Compute("sum(DebitCOST)", ""));
                    decimal AdvocateCR = Convert.ToDecimal(dtBind.Compute("sum(CreditAdvocate)", ""));
                    decimal AdvocateDR = Convert.ToDecimal(dtBind.Compute("sum(DebitAdvocate)", ""));

                    decimal DecreeCrNetBal = 0, DecreeDrNetBal = 0, CostCrNetBal = 0, CostDrNetBal = 0, AdvocateCrNetBal = 0, AdvocateDrNetBal = 0;

                    DataRow rowTotal = dtBind.NewRow();
                    rowTotal["Name"] = "Total";
                    rowTotal["AmountReceived"] = Deecreeamount;
                    rowTotal["Totalamount"] = Deecreetotal;
                    rowTotal["CreditDECREE"] = DeecreeCR;
                    rowTotal["DebitDECREE"] = DeecreeDR;
                    rowTotal["CreditCOST"] = CostCR;
                    rowTotal["DebitCOST"] = CostDR;
                    rowTotal["CreditAdvocate"] = AdvocateCR;
                    rowTotal["DebitAdvocate"] = AdvocateDR;
                    dtBind.Rows.Add(rowTotal.ItemArray);

                    DataRow rowNet = dtBind.NewRow();
                    rowNet["Name"] = "Net Balance";

                    if (DeecreeCR > DeecreeDR)
                    {
                        DecreeCrNetBal = DeecreeCR - DeecreeDR;
                        rowNet["CreditDECREE"] = DecreeCrNetBal;
                    }
                    else
                    {
                        DecreeDrNetBal = DeecreeDR - DeecreeCR;
                        rowNet["DebitDECREE"] = DecreeDrNetBal;
                    }
                    if (CostCR > CostDR)
                    {
                        CostCrNetBal = CostCR - CostDR;
                        rowNet["CreditCOST"] = CostCrNetBal;
                    }
                    else
                    {
                        CostDrNetBal = CostDR - CostCR;
                        rowNet["DebitCOST"] = CostDrNetBal;
                    }

                    if (AdvocateCR > AdvocateDR)
                    {
                        AdvocateCrNetBal = AdvocateCR - AdvocateDR;
                        rowNet["CreditAdvocate"] = AdvocateCrNetBal;
                    }
                    else
                    {
                        AdvocateDrNetBal = AdvocateDR - AdvocateCR;
                        rowNet["DebitAdvocate"] = AdvocateDrNetBal;
                    }
                    dtBind.Rows.Add(rowNet.ItemArray);


                    DataRow rowBalSummary = dtBind.NewRow();
                    rowBalSummary["EP No./OS No./ARC No./ARB No."] = "Net Balance Summary";
                    rowBalSummary["ChitName"] = "Credit";
                    rowBalSummary["Name"] = "Debit";
                    dtBind.Rows.Add(rowBalSummary.ItemArray);


                    DataRow rowBalSummaryData = dtBind.NewRow();
                    rowBalSummaryData["ChitName"] = (DecreeCrNetBal + CostCrNetBal + AdvocateCrNetBal).ToString();
                    rowBalSummaryData["Name"] = (DecreeDrNetBal + CostDrNetBal + AdvocateDrNetBal).ToString();
                    dtBind.Rows.Add(rowBalSummaryData.ItemArray);


                    DataRow rowNetBalSummary = dtBind.NewRow();
                    if ((DecreeCrNetBal + CostCrNetBal + AdvocateCrNetBal) > (DecreeDrNetBal + CostDrNetBal + AdvocateDrNetBal))
                    {
                        rowNetBalSummary["EP No./OS No./ARC No./ARB No."] = "Net Balance CR";
                        rowNetBalSummary["ChitName"] = ((DecreeCrNetBal + CostCrNetBal + AdvocateCrNetBal) - (DecreeDrNetBal + CostDrNetBal + AdvocateDrNetBal)).ToString();
                    }
                    else
                    {
                        rowNetBalSummary["EP No./OS No./ARC No./ARB No."] = "Net Balance DR";
                        rowNetBalSummary["Name"] = ((DecreeDrNetBal + CostDrNetBal + AdvocateDrNetBal) - (DecreeCrNetBal + CostCrNetBal + AdvocateCrNetBal)).ToString();
                    }
                    dtBind.Rows.Add(rowNetBalSummary.ItemArray);
                }
                catch (Exception) { }              
            }
            return dtBind;
        }

        public List<ModelBanks> BindScheduledBanks(string todate,int branchid)
        {
            List<ModelBanks> objScheduledbanks = new List<ModelBanks>();
            try
            {
                DateTime Todt = Convert.ToDateTime(todate);

                var qry = @"select t3.BankName as BankName,  t3.BankLocation as BankLocation,t3.AccountNo as AccountNo, (case when(sum(case when t1.Voucher_Type = 'C' " +
                  "then t1.Amount else 0.00 end )>sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )) then sum(case when t1.Voucher_Type='C' then t1.Amount " +
             "else 0.00 end )-sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) else 0.00 end ) as `Credit`,(case when (sum((case when t1.Voucher_Type='D' " +
             "then t1.Amount else 0.00 end ))>sum((case when t1.Voucher_Type='C' then t1.Amount else 0.00 end ))) then sum((case when t1.Voucher_Type='D' then t1.Amount " +
             "else 0.00 end ))-sum((case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )) else 0.00 end ) as `Debit` from voucher as t1  left Join bankdetails as t3 " +
             "on t1.Head_ID=t3.Head_ID where t3.TypeofBank='Scheduled Banks' and t1.RootID=3 and `t1`.`BranchID` =" + branchid + " and t1.ChoosenDate between '2009/03/31' and '" + objBAL.changedateformat(Todt, 2) + "' group by `t1`.`Head_ID`";
                var SB_Dt = objBAL.GetDataTable(qry);
                objScheduledbanks = SB_Dt.DataTableToList<ModelBanks>();

            }
            catch (Exception) { }
            return objScheduledbanks;
        }


        public List<ModelBanks> BindFixedBanks(string todate, int branchid)
        {
            List<ModelBanks> objFixedbanks = new List<ModelBanks>();
            try
            {
                DateTime Todt = Convert.ToDateTime(todate);

                var qry = @"select t3.BankName as BankName,  t3.BankLocation as BankLocation,t3.AccountNo as AccountNo, (case when (sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )>sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )) then sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )-sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) else 0.00 end ) as `Credit`,(case when (sum((case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ))>sum((case when t1.Voucher_Type='C' then t1.Amount else 0.00 end ))) then sum((case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ))-sum((case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )) else 0.00 end ) as `Debit` from voucher as t1  left Join bankdetails as t3 on t1.Head_ID=t3.Head_ID where " +
                    "t3.TypeofBank='Fixed deposits with Banks' and t1.RootID=3 and `t1`.`BranchID` =" + branchid + " and t1.ChoosenDate between '2009/03/31' and '" + objBAL.changedateformat(Todt, 2) + "' group by `t1`.`Head_ID`";
                var SB_Dt = objBAL.GetDataTable(qry);
                objFixedbanks = SB_Dt.DataTableToList<ModelBanks>();

            }
            catch (Exception) { }
            return objFixedbanks;
        }
    }
}