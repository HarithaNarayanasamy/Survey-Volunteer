using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Web.UI.WebControls;

using SVCF_DataAccessLayer;
using SVCF_PropertyLayer;

namespace SVCF_BusinessAccessLayer
{
  public  class BusinessObjects
    {
        //GetMethods objgtmethods = new GetMethods();       
        CmnMethods objcmn = new CmnMethods();
        //public DataTable GetGroup(PALayer objPAL)
        //{
        //    return objgtmethods.GetGroupMember(objPAL);
        //}
        public string Generate_DualtransactionKey()
        {
            return objcmn.Generate_DualtransactionKey();
        }

         
    }
}
