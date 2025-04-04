using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SreeVisalamChitFundLtd_phase1
{
    public class ModelTokenDetails
    {
        public int Head_Id { get; set; }
        public int ChitGroupId { get; set; }
        public decimal Amount { get; set; }
        public DateTime ChoosenDate { get; set; }     
        public string GrpMemberID { get; set; }
        public string Voucher_Type { get; set; }
        public int Other_Trans_Type { get; set; }
        public string Series { get; set; }
        public string Narration { get; set; }        
    }

    public class DebtsoutsExceed6month
    {
        public int SlNo { get; set; }
        public string ChitNumber { get; set; }
        public string NameoftheParty { get; set; }
        public DateTime ArrearsfromDate { get; set; }
        public decimal Running_AmountinChitAccnt { get; set; }
        public decimal Terminated_AmountinChitAccnt { get; set; }
        public decimal ChitLoan { get; set; }
        public DateTime DateOfLoan { get; set; }
        public decimal AdvanceExcluding { get; set; }
        public DateTime DateofAdvance { get; set; }
    }
}