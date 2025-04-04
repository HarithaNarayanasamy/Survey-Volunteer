using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SreeVisalamChitFundLtd_phase1
{
    public class ClsMemberList
    {
        public string GrpMemberID { get; set; }
        public int MemberIDNew { get; set; }
        public int MemberID { get; set; }
        public int mem { get; set; }
        public int BranchId { get; set; }
        public string Title { get; set; }
        public string CustomerName { get; set; }
        public string TypeOfMember { get; set; }
        public string Gender { get; set; }
        public string Age { get; set; }
        public string FatherHusbandName { get; set; }
        public string AadharNumber { get; set; }
        public string MobileNo { get; set; }
        public string ImageUrl { get; set; }        
    }

    public class ClsMemtoGroup
    {
        public string GrpMemberID { get; set; }
        public int MemberID { get; set; }
    }

    public class ClsMembers
    {
        public string GrpMemberID { get; set; }
        public string MemberIDNew { get; set; }
        public int MemberID { get; set; }
        public string mem { get; set; }
        public int BranchId { get; set; }
        public string Title { get; set; }
        public string CustomerName { get; set; }
        public string TypeOfMember { get; set; }
        public string Gender { get; set; }
        public string Age { get; set; }
        public string FatherHusbandName { get; set; }
        public string AadharNumber { get; set; }
        public string MobileNo { get; set; }
        public string ImageUrl { get; set; }
    }
}