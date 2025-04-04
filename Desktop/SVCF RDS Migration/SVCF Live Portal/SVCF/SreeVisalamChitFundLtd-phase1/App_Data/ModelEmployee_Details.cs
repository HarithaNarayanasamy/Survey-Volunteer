using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SreeVisalamChitFundLtd_phase1
{
    public class ModelEmployee_Details
    {
        public string BranchID { get; set; }
        public string Emp_ID { get; set; }
        public string Emp_Name { get; set; }
        public string Emp_Address { get; set; }
        public string Emp_PhoneNo { get; set; }
        public string Emp_Salary { get; set; }
        public string Emp_Designation { get; set; }
        public string Emp_Email { get; set; }
        public string employee_detailscol { get; set; }
        public string IsDeleted { get; set; }
        public string Emp_SrNumber { get; set; }
        public DateTime Emp_DateOfJoining { get; set; }
    }
}