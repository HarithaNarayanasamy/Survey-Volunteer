//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Entity
{
    using System;
    using System.Collections.Generic;
    
    public partial class transloan
    {
        public System.Guid DualTransactionKey { get; set; }
        public Nullable<long> TransactionKey { get; set; }
        public Nullable<long> GroupID { get; set; }
        public Nullable<long> GroupMemberID { get; set; }
        public Nullable<long> EmployeeID { get; set; }
        public Nullable<long> MemberID { get; set; }
        public long LoanHeadID { get; set; }
        public System.DateTime ApplyedOn { get; set; }
        public Nullable<System.DateTime> ApprovedOn { get; set; }
        public Nullable<System.DateTime> ChoosenDate { get; set; }
        public long AdminSanctionNo { get; set; }
        public string Narration { get; set; }
        public long Trans_Medium { get; set; }
        public string ReasonforRejection { get; set; }
        public long BranchID { get; set; }
        public long loantype { get; set; }
        public long Flag { get; set; }
        public Nullable<long> loannumber { get; set; }
        public long aid { get; set; }
        public Nullable<long> BankHeadId { get; set; }
        public long isTransfered { get; set; }
    }
}
