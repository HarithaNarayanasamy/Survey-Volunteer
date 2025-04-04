using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SreeVisalamChitFundLtd_phase1
{
    public class ModelVoucher
    {
        public int TransactionKey { get; set; }
        public string DualTransactionKey { get; set; }
        public int BranchID { get; set; }
        public DateTime CurrDate { get; set; }
        public int Voucher_No { get; set; }
        public string Voucher_Type { get; set; }
        public int Head_Id { get; set; }
        public DateTime ChoosenDate { get; set; }
        public string Narration { get; set; }
        public long Amount { get; set; }
        public string Series { get; set; }
        public string ReceievedBy { get; set; }
        public int Trans_Type { get; set; }
        public int T_Day { get; set; }
        public int T_Month { get; set; }
        public int T_Year { get; set; }
        public int MemberID { get; set; }
        public int Trans_Medium { get; set; }
        public int RootID { get; set; }
        public int ChitGroupId { get; set; }
        public int Other_Trans_Type { get; set; }
        public int IsDeleted { get; set; }
        public int IsAccepted { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string ISActive { get; set; }
        public string AppReceiptno { get; set; }
        public int M_Id { get; set; }
        public string LoginIP { get; set; }
    }

    public class ModelBranchTrial
    {
        public string Date { get; set; }
        public string Branch { get; set; }      
        public decimal Credit { get; set; }
        public decimal Debit { get; set; }
    }
}