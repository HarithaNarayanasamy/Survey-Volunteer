using SVCF_BusinessAccessLayer;
using SVCF_DataAccessLayer;
using SVCF_TransactionLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace SreeVisalamChitFundLtd_phase1
{
    public partial class CourtAdvocateDegree : System.Web.UI.Page
    {
        #region Object
        BusinessLayer balayer = new BusinessLayer();
        CommonClassFile objCommonClass = new CommonClassFile();
        TransactionLayer trn = new TransactionLayer();
        
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                bindvalue();
            }
            bindvalue();
        }

        public void bindvalue()
        {
            string query = @"select Distinct ch.HeadId ,ch.ChitName,ch.ParentID,ch.MemberName,tc.cc,(select Node from headstree where NodeID= ch.ParentID) as head,ch.ParentID from chitheads as ch Right join headstree as hs on (ch.HeadId =hs.NodeID) 
            left join transcourt as tc on(ch.HeadId =tc.HeadID)  where ch.ParentID in (51,52,4638) and ch.branchid= '" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + "'   order by MemberName;";

            DataTable loadgrid = balayer.GetDataTable(query);
            EditCourt.DataSource = loadgrid;
            EditCourt.DataBind();
        }

        protected void OnPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            EditCourt.PageIndex = e.NewPageIndex;
            // EditCourt.BindGrid();
            bindvalue();
        }
    }
}