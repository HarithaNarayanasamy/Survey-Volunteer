using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SreeVisalamChitFundLtd_phase1
{
    public class Emoluments_St_12
    {
        public string SlNo { get; set; }
        public string AprNo { get; set; }
        public string PeriodWorked_inMonths { get; set; }
        public string PeriodWorked_indays { get; set; }
        public string NameoftheStaff { get; set; }
        public decimal Salary { get; set; }
        public decimal DearnessAllowance { get; set; }
        public decimal HouseRentAllowance { get; set; }
        public decimal MedicalAllowance { get; set; }
        public decimal Bonus { get; set; }
        public decimal BusinessIncentive { get; set; }
        public decimal BPPforCurrentBranch { get; set; }
        public decimal BPPforOtherBranch { get; set; }
        public decimal AggregateTotal { get; set; }
        public string ConfirmationReceipt { get; set; }
    }

    public class Business_Perform_St_17
    {
        public int SlNo { get; set; }  
        public string NameoftheStaff { get; set; }
        public string FullResidentialAddressofStaff { get; set; }
        public int NoOfPaymentsmade { get; set; }
        public decimal TotalAmountPaid { get; set; }
        public string ApprovalNoOfAdminOffice { get; set; }    
    }
}