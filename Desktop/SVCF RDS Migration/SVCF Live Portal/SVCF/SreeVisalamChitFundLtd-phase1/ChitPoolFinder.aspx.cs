using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using SVCF_BusinessAccessLayer;
using SVCF_DataAccessLayer;

namespace SreeVisalamChitFundLtd_phase1
{
    public partial class ChitPoolFinder : System.Web.UI.Page
    {
        #region Object
        BusinessLayer balayer = new BusinessLayer();
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            string strQuery = "SELECT t1.PoolNo,t2.CustomerName, t2.Age, t2.DOB, t2.FatherHusbandName, t2.MotherWifeName, t2.ProprietorName, t2.ProfessionBusiness, t2.NatureofProfessionBusiness, t2.ResidentialAddress, t2.AddressForCommunication, t2.ProofofResidence, t2.AddressProfessionBusiness, t2.TelephoneNoProfessionBusiness, t2.TelephoneNoResidence, t2.MobileNo FROM `membersuggestion` as t1  left Join membermaster as t2 on t1.MemberID=t2.MemberIDNew where t1.SuggestedBranchId='" +balayer.ToobjectstrEvenNull(Session["Branchid"]) + "'";
            DataTable dt = balayer.GetDataTable(strQuery);
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }
    }
}
