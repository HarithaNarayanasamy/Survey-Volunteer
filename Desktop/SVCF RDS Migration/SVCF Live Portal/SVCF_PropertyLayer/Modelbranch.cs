using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SVCF_PropertyLayer
{
    public class Modelbranch
    {
        public long branchid { get; set; }
        public string branchname { get; set; }
    }
    public class Trailbalance
    {
        public string date { get; set; }
        public string Branch { get; set; }
        public decimal Credit { get; set; }
        public decimal Debit { get; set; }
    }
    public class Investments
    {
        public string Heads { get; set; }
        public string Narration { get; set; }
        public decimal Credit { get; set; }
        public decimal Debit { get; set; }
    }
    public class TrialBalanceofOtherItems
    {
        public long NodeID { get; set; }
        public string Heads { get; set; }
        public decimal credit { get; set; }
        public decimal Debit { get; set; }
    }
    public class Loan
    {
        public System.DateTime ChoosenDate { get; set; }
        public string Name { get; set; }
        public long NodeID { get; set; }
        public long ParentID { get; set; }
        public decimal Credit { get; set; }
        public decimal Debit { get; set; }
    }
    public class chitheads
    {
        public string MemberName { get; set; }
        public string ChitName { get; set; }
        public int HeadId { get; set; }
    }
    public class monthlychit
    {
        public string Heads { get; set; }
        public decimal MothlyCredit { get; set; }
        public decimal MonthlyDebit { get; set; }
    }
    public class Trimonthly
    {
        public string Heads { get; set; }
        public decimal TrimonthlyChits { get; set; }
        public decimal TrimonthlyChitsdebit { get; set; }
    }
    public class FortnightlyChit
    {
        public string Heads { get; set; }
        public decimal FortnightlyCredit { get; set; }
        public decimal FortnightlyDebit { get; set; }
    }
    public class RCM1Credit
    {
        public string Heads { get; set; }
        public decimal RCM1Credit1 { get; set; }
        public decimal RCM1Debit { get; set; }
    }
    public class RCM2Credit
    {
        public string Heads { get; set; }
        public decimal RCM2Credit1 { get; set; }
        public decimal RCM2Debit { get; set; }
    }
    public class UnpaidPrizeMoney
    {
        public string Heads { get; set; }
        public decimal UnCredit { get; set; }
        public decimal UnDebit { get; set; }
    }
    public class UnpaidPrizemoneypayable
    {
        public string Heads { get; set; }
        public decimal OutCredit { get; set; }
        public decimal OutDebit { get; set; }
    }
    public class OutStanding
    {
        public string Heads { get; set; }
        public decimal OutCredit { get; set; }
        public decimal OutDebit { get; set; }
    }
    public class ChitCredit
    {
        public string Heads { get; set; }
        public decimal ChitCredit1 { get; set; }
        public decimal ChitDebit { get; set; }
    }
}
