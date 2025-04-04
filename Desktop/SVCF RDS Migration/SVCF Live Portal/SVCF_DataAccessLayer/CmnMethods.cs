using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SVCF_PropertyLayer;
using System.Data;


namespace SVCF_DataAccessLayer
{
   public class CmnMethods
    {
        #region VarDeclaration
        string qry = "";
        CommonClassFile objcls = new CommonClassFile();
        PALayer objpal = new PALayer();
        #endregion

        public DataTable GetGroupMember(PALayer objpal)
        {
            qry = "SELECT `groupmaster`.`GROUPNO`,`groupmaster`.`Head_Id` FROM `svcf`.`groupmaster` where `groupmaster`.`IsFinished`=0 and `groupmaster`.`BranchID`=" + objpal.Branchid + "";
            return objcls.SelectTable1(qry);
        }

        public string Generate_DualtransactionKey()
        {
            System.Guid guid = Guid.NewGuid();
            // Prepare GUID values in SQL format           
            objpal.hexstring = BitConverter.ToString(guid.ToByteArray());
            objpal.guidForBinary16 = "0x" + objpal.hexstring.Replace("-", string.Empty);
            objpal.DualTransactionKey = objpal.guidForBinary16;
            return objpal.DualTransactionKey;
        }
    }
}
