using System;
using System.Collections;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using System.Collections.Generic ;
using SVCF_BusinessAccessLayer;
using SVCF_DataAccessLayer;
namespace SreeVisalamChitFundLtd_phase1
{
    /// <summary>
    /// Summary description for $codebehindclassname$
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class ProspectSuggest : IHttpHandler
    {
        #region Object
        BusinessLayer balayer = new BusinessLayer();
        
        #endregion
        public void ProcessRequest(HttpContext context)
        {
            string prefixText = context.Request.QueryString["q"];
            List<string> fetchNames =CommonClassFile.lstSuggestions.Where(m => m.ToLower().Contains(prefixText.ToLower())).ToList<string>();



            context.Response.Write(string.Join(Environment.NewLine, fetchNames.ToArray()));
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
