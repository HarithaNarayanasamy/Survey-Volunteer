using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.IO;
namespace SreeVisalamChitFundLtd_phase1
{
    public class VarDeclaration
    {
        public DataTable GtDt { get; set; }
        public string varQuery { get; set; }
        public string memberid { get; set; }
        public string MemberName { get; set; }
        public string filterText { get; set; }
        public string filterColumnName { get; set; }
        public string filterExpression { get; set; }
        public string varquery { get; set; }
        public DataTable tempDt { get; set; }
        public string SelectedBranchList { get; set; }
        public DataTable tempDt1 { get; set; }
        public string varquery1 { get; set; }
        public DataTable tempDt2 { get; set; }
        public string varquery2 { get; set; }


    }

    public class VarDeclaration1
    {
        public DataTable tempDt1 { get; set; }
        public string varquery1 { get; set; }
    }

    public class VarDeclaration2
    {
        public DataTable tempDt2 { get; set; }
        public string varquery2 { get; set; }

    }
   
}