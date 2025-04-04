using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
namespace SVCF_DataAccessLayer
{
    public class CommonVariables
    {
        private int _rowcount = 0;
        private int _BranchId;
        private int _GroupID;
        private int _HeadId;
        private string _membername;
        private string _sss;
        private string _strName;
        private string _strMemID;
        private string _credit;
        private string _debit;
        private string _excess;
        private string _nparr;
        private string _parr;
        private string _npkas;
        private string _pkas;
        private string _strMember;
        private string _strBranches;
        private DataTable _dtInit = new DataTable();
        private DataTable _dtHeads = new DataTable();
        private DataTable _dtChitGrp = new DataTable();
        private DataRow _dr;
        private string _data;
        private DataTable _dtBind = new DataTable();
        private DataTable _sssssssss = new DataTable();
        private DataRow _drBind;
        private DataView _dv = new DataView();
        private DataTable _Caption = new DataTable();
        private DataTable _sortedDT = new DataTable();
        private DataTable _dtremoval = new DataTable();
        private DateTime _removaldate;
        private List<string> _Subtokenlist = new List<string>();
        private string _str;
        private decimal _decCredit;
        private decimal _decDebit;
        private int _maxdrawno;
        private decimal _totalcr;
        private DataTable _dtnullck = new DataTable();

        public string ForemanToken { get; set; }
        public string ForemanHeadId { get; set; }
        public int ForemanNodeid { get; set; }
        public DataTable ForemanDt { get; set; }
        public string RegOfcename { get; set; }
        public decimal KasarAmount { get; set; }
        public decimal TotalCommission { get; set; }
        private string _mobilenumber { get; set; }


        public string MobileNumber
        {
            get
            {
                return _mobilenumber;
            }
            set
            {
                _mobilenumber = value;
            }
        }


        public DataTable Dtnullck
        {
            get
            {
                return _dtnullck;
            }
            set
            {
               _dtnullck = value ;
            }
        }


        public int Maxdrawno
        {
            get
            {
                return _maxdrawno;
            }
            set
            {
                _maxdrawno = value;
            }
        }

        public decimal Totalcr
        {
            get
            {
                return _totalcr;
            }
            set
            {
                _totalcr = value;
            }
        }

        public string MemberName
        {
            get
            {
                return _membername;
            }
            set
            {
                _membername = value;
            }
        }
        public int RowCount
        {
            get
            {
                return _rowcount;
            }
            set
            {
                _rowcount = value;
            }
        }
        public int HeadId
        {
            get
            {
                return _HeadId;
            }
            set
            {
                _HeadId = value;
            }
        }
        public int Branchid
        {
            get
            {
                return _BranchId;
            }
            set
            {
                _BranchId = value;
            }
        }
        public int GroupID
        {
            get
            {
                return _GroupID;
            }
            set
            {
                _GroupID = value;
            }
        }
        public string Sss
        {
            get
            {
                return _sss;
            }
            set
            {
                _sss = value;
            }
        }
        public string StrName
        {
            get
            {
                return _strName;
            }
            set
            {
                _strName = value;
            }
        }
        public string StrMemID
        {
            get
            {
                return _strMemID;
            }
            set
            {
                _strMemID = value;
            }
        }
        public string Credit
        {
            get
            {
                return _credit;
            }
            set
            {
                _credit = value;
            }
        }
        public string Debit
        {
            get
            {
                return _debit;
            }
            set
            {
                _debit = value;
            }
        }
        public string Excess
        {
            get
            {
                return _excess;
            }
            set
            {
                _excess = value;
            }
        }
        public string Nparr
        {
            get
            {
                return _nparr;
            }
            set
            {
                _nparr = value;
            }
        }
        public string Parr
        {
            get
            {
                return _parr;
            }
            set
            {
                _parr = value;
            }
        }
        public string Npkas
        {
            get
            {
                return _npkas;
            }
            set
            {
                _npkas = value;
            }
        }
        public string Pkas
        {
            get
            {
                return _pkas;
            }
            set
            {
                _pkas = value;
            }
        }
        public string StrMember
        {
            get
            {
                return _strMember;
            }
            set
            {
                _strMember = value;
            }
        }
        public string StrBranches
        {
            get
            {
                return _strBranches;
            }
            set
            {
                _strBranches = value;
            }
        }
        public string Data
        {
            get
            {
                return _data;
            }
            set
            {
                _data = value;
            }
        }
        public string Str
        {
            get
            {
                return _str;
            }
            set
            {
                _str = value;
            }
        }
        public decimal DecCredit
        {
            get
            {
                return _decCredit;
            }
            set
            {
                _decCredit = value;
            }
        }
        public decimal DecDebit
        {
            get
            {
                return _decDebit;
            }
            set
            {
                _decDebit = value;
            }
        }
        public DateTime Removaldate
        {
            get
            {
                return _removaldate;
            }
            set
            {
                _removaldate = value;
            }
        }
        public DataTable DtInit
        {
            get
            {
                return _dtInit;
            }
            set
            {
                _dtInit = value;
            }
        }
        public DataTable DtHeads
        {
            get
            {
                return _dtHeads;
            }
            set
            {
                _dtHeads = value;
            }
        }
        public DataTable DtChitGrp
        {
            get
            {
                return _dtChitGrp;
            }
            set
            {
                _dtChitGrp = value;
            }
        }
        public DataRow Dr
        {
            get
            {
                return _dr;
            }
            set
            {
                _dr = value;
            }
        }      
       
        public DataTable DtBind
        {
            get
            {
                return _dtBind;
            }
            set
            {
                _dtBind = value;
            }
        }
        public DataTable Sssssssss
        {
            get
            {
                return _sssssssss;
            }
            set
            {
                _sssssssss = value;
            }
        }
        public DataRow DrBind
        {
            get
            {
                return _drBind;
            }
            set
            {
                _drBind = value;
            }
        }
        public DataView Dv
        {
            get
            {
                return _dv;
            }
            set
            {
                _dv = value;
            }
        }
        public DataTable Caption
        {
            get
            {
                return _Caption;
            }
            set
            {
                _Caption = value;
            }
        }
        public DataTable SortedDT
        {
            get
            {
                return _sortedDT;
            }
            set
            {
                _sortedDT = value;
            }
        }
        public DataTable Dtremoval
        {
            get
            {
                return _dtremoval;
            }
            set
            {
                _dtremoval = value;
            }
        }
        public List<string> Subtokenlist
        {
            get
            {
                return _Subtokenlist;
            }
            set
            {
                _Subtokenlist = value;
            }
        }
    }
}
