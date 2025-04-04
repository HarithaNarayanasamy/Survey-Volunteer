using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SreeVisalamChitFundLtd_phase1
{
    public class ClsBs_St_01
    {
      
    }

    public class ModelBranchTrialBalance
    {
        public int Slno { get; set; }
        public string NameoftheBranch { get; set; }
        public decimal Credit { get; set; }
        public decimal Debit { get; set; }
        public decimal Remarks { get; set; }
    }

   
    public class ModelAdvanceDecree 
    {
        //public int SNo { get; set; }
        public string Heads { get; set; }
        public int NodeID { get; set; }
        public string MemberName { get; set; }
        public decimal Degree_Credit { get; set; }
        public decimal Degree_Debit { get; set; }
        public decimal Advocate_Credit { get; set; }
        public decimal Advocate_Debit { get; set; }

        public decimal ACA_Credit { get; set; }
        public decimal ACA_Debit { get; set; }

        public decimal CAL_Credit { get; set; }
        public decimal CAL_Debit { get; set; }
        public decimal Cort_Credit { get; set; }
        public decimal Cort_Debit { get; set; }

        public decimal VA_Credit { get; set; }
        public decimal VA_Debit { get; set; }

        public decimal PA_Credit { get; set; }
        public decimal PA_Debit { get; set; }
        public decimal STMISS_Credit { get; set; }
        public decimal STMISS_Debit { get; set; }
        public string Narration { get; set; }        
    }

    public class ModelAdvance
    {      
        public string Heads { get; set; }
        public int NodeID { get; set; }      
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
        public decimal SC_Debit { get; set; }
        public decimal SDeb_Credit { get; set; }
        public decimal SDeb_Debit { get; set; }
    }

    public class ModelOtherItems
    {       
        public int NodeID { get; set; }
        public string Heads { get; set; }
        public decimal Credit { get; set; }
        public decimal Debit { get; set; }       
    }

    public class ModelFixedAssets
    {
        public int PID { get; set; }
        public int CNodeID { get; set; }
        public string Heads { get; set; }
        public string Narration { get; set; }
        public decimal Credit { get; set; }
        public decimal Debit { get; set; }
    }

    public class Bind12Heads
    {
        public int RootId { get; set; }
        public string Node { get; set; }
        public decimal Credit { get; set; }
        public decimal Debit { get; set; }
    }

    public class ModelBanks
    {
        public string BankName { get; set; }
        public string BankLocation { get; set; }
        public string AccountNo { get; set; }
        public decimal Credit { get; set; }
        public decimal Debit { get; set; }
    }
}
