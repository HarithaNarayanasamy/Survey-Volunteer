using SVCF_BusinessAccessLayer;
using SVCF_TransactionLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SreeVisalamChitFundLtd_phase1
{
    public partial class Userhome : System.Web.UI.Page
    {
        #region VarDeclaration
        BusinessLayer balayer = new BusinessLayer();
        TransactionLayer tranlayer = new TransactionLayer();
        string valueCr = "";
        string valueDr = "";
        decimal decCr = 0;
        decimal decDr = 0;
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
           
        }
    }
}