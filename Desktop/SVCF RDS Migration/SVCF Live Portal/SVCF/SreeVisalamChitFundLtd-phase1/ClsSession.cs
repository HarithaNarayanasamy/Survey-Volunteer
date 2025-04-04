using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SreeVisalamChitFundLtd_phase1
{
    public class ClsSession
    {

        private int roleid;
        private int branchid;
        private string Username;
        private string branchname;
        private string loginip;
        private int slno;
        private string sessionkey;

        public int RoleId
        {
            get
            {
                return roleid;
            }
            set
            {
                roleid = value;
            }
        }

        public int BranchId
        {
            get
            {
                return branchid;
            }
            set
            {
                branchid = value;
            }
        }
        public string UserName
        {
            get
            {
                return Username;
            }
            set
            {
                Username = value;
            }
        }
        public string BranchName
        {
            get
            {
                return branchname;
            }
            set
            {
                branchname = value;
            }
        }
        public string LoginIp
        {
            get
            {
                return loginip;
            }
            set
            {
                loginip = value;
            }
        }
        public int SlNo
        {
            get
            {
                return slno;
            }
            set
            {
                slno = value;
            }
        }

        public string SessionKey
        {
            get
            {
                return sessionkey;
            }
            set
            {
                sessionkey = value;
            }
        }

    
    }
}