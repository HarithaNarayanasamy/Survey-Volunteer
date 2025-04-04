using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SVCF_PropertyLayer
{
    public class BindAdvance
    {
        public string Heads { get; set; }
        public long NodeID { get; set; }
        public long ParentID { get; set; }
        public string Headname { get; set; }
        public string Narration { get; set; }
        public decimal EB_Credit { get; set; }
        public decimal EB_Debit { get; set; }
        public decimal TD_Credit { get; set; }
        public decimal TD_Debit { get; set; }
        public decimal RA_Credit { get; set; }
        public decimal RA_Debit { get; set; }
        public decimal S_Credit { get; set; }
        public decimal S_Debit { get; set; }
        public decimal PPA_Credit { get; set; }
        public decimal PPA_Debit { get; set; }
        public decimal VRA_Credit { get; set; }
        public decimal VRA_Debit { get; set; }
        public decimal SC_Credit { get; set; }
        public decimal SDeb_Debit { get; set; }
        public decimal SC_Debit { get; set; }
        public decimal SDeb_Credit { get; set; }
        public string remarks { get; set; }
    }

    public class BindAdvancePart2
    {
        public string Heads { get; set; }
        public long NodeID { get; set; }
        public long ParentID { get; set; }
        public string Headname { get; set; }
        public string Narration { get; set; }
        public decimal Degree_Credit { get; set; }
        public decimal Degree_Debit { get; set; }
        public decimal Advocate_Credit { get; set; }
        public decimal Advocate_Debit { get; set; }
        public decimal ACA_Credit { get; set; }
        public decimal ACA_Debit { get; set; }
        public decimal Cort_Credit { get; set; }
        public decimal Cort_Debit { get; set; }
        public decimal VA_Credit { get; set; }
        public decimal VA_Debit { get; set; }
        public decimal PA_Credit { get; set; }
        public decimal PA_Debit { get; set; }
        public decimal CAL_Credit { get; set; }
        public decimal CAL_Debit { get; set; }
        public decimal STMISS_Credit { get; set; }
        public decimal STMISS_Debit { get; set; }
        public string remarks { get; set; }
    }

    public class BindDecree
    {
        public long TransactionKey { get; set; }
        public long NodeID { get; set; }
        public decimal Crtotal { get; set; }
        public decimal BalCredit { get; set; }
        public decimal BalDebit { get; set; }
        public decimal CourtCredit { get; set; }
        public decimal CourtDebit { get; set; }
        public decimal AdvocateCredit { get; set; }
        public decimal AdvocateDebit { get; set; }
        public string Head { get; set; }
        public string HeadName { get; set; }
    }

    public class BindPandL
    {
        public System.DateTime Date { get; set; }
        public string Heads { get; set; }
        public decimal Credit { get; set; }
        public decimal Debit { get; set; }
    }

    public class BindScheduledbanks
    {
        public string BankName { get; set; }
        public string BankLocation { get; set; }
        public string AccountNo { get; set; }
        public decimal Credit { get; set; }
        public decimal Debit { get; set; }
    }

    public class BindFixedDepositwithBanks
    {
        public string BankName { get; set; }
        public string BankLocation { get; set; }
        public string AccountNo { get; set; }
        public decimal Credit { get; set; }
        public decimal Debit { get; set; }
    }
}
