using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SVCF_PropertyLayer
{
    public class PALayer
    {
        #region prv Group variables
        private int _BranchId;
        private int _GroupID;
        private DateTime _getdate;
        private int _HeadId;

        #endregion

        #region CAbstractVariables
          
        #endregion

        #region CommonVariables
        private string _hexstring;
        private string _guidForBinary16;
        private string _DualTransactionKey;
        #endregion


        #region GlobalVar_prv grp

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
        public DateTime getdate
        {
            get
            {
                return _getdate;
            }
            set
            {
                _getdate = value;
            }
        }



        #endregion

        #region Global_Common
        public string hexstring
        {
            get
            {
                return _hexstring;
            }
            set
            {
                _hexstring = value;
            }
        }
        public string guidForBinary16
        {
            get
            {
                return _guidForBinary16;
            }
            set
            {
                _guidForBinary16 = value;
            }
        }
        public string DualTransactionKey
        {
            get
            {
                return _DualTransactionKey;
            }
            set
            {
                _DualTransactionKey = value;
            }
        }

        #endregion
    }
}
