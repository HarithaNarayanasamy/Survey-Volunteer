using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxGridView;
using SVCF_BusinessAccessLayer;
using SVCF_TransactionLayer;
using SVCF_DataAccessLayer;

namespace SreeVisalamChitFundLtd_phase1
{
    public partial class receiptbook : System.Web.UI.Page
    {
        //#region VarDeclaration
        //CommonClassFile objcls = new CommonClassFile();
        //#endregion
        #region VarDeclaration
        BusinessLayer balayer = new BusinessLayer();
        TransactionLayer tranlayer = new TransactionLayer();
        
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            var userinfo = HttpContext.Current.User.Identity.Name;
            var qry = "select rs.name from roles as rs inner join rights as rt on (rt.roleid=rs.id) where memberid=" + userinfo + "";
            var usrRole = balayer.GetSingleValue(qry);
            if (usrRole == "Report")
            {
                Response.Redirect("Home.aspx", false);
            }
            else
            {

                select();
            }
        }
        void select()
        {
            DataSourceBranch.ConnectionString = CommonClassFile.ConnectionString;
            DataSourceBranch.SelectCommand = @"SELECT a1.slNO, m1.moneycollname, a1.receiptseries,a1.receiptnofrom,a1.receiptnoto,(case when (a1.IsFinished=0)then 'Not finished' else 'Finished' end) as status FROM svcf.assignreceiptbook as a1 join moneycollector as m1 on (a1.moneycollid=m1.moneycollid) where a1.BranchID=" + Session["Branchid"];
            gridBranch.DataBind();
        }
        protected void gridBranch_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        {
        }
        void AddError(Dictionary<GridViewColumn, string> errors, GridViewColumn column, string errorText)
        {
            if (errors.ContainsKey(column)) return;
            errors[column] = errorText;
        }
        protected void gridBranch_RowDeleting(object sender,DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {

            if (Convert.ToInt32(Session["roleid"]) == 2)
            {
                if (e.Values["status"].ToString() == "Finished")
                {
                    balayer.GetInsertItem("delete from  svcf.assignreceiptbook   where slNO=" + e.Keys["slNO"] + " and IsFinished = 1");
                }
                else
                {
                    balayer.GetInsertItem("delete from svcf.assignreceiptbook   where slNO=" + e.Keys["slNO"] + " and IsFinished=0");
                }
                ASPxGridView grid = (sender as ASPxGridView);
                select();
                grid.DataBind();
                e.Cancel = true;
            }
    
        }
       
        protected void gridBranch_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            if (e.NewValues["status"].ToString() == "Finished")
            {
                balayer.GetInsertItem("update svcf.assignreceiptbook set IsFinished=1 where slNO=" + e.Keys["slNO"]);
            }
            else
            {
                balayer.GetInsertItem("update svcf.assignreceiptbook set IsFinished=0 where slNO=" + e.Keys["slNO"]);
            }
            
            ASPxGridView grid = (sender as ASPxGridView);
            select();
            grid.DataBind();
            e.Cancel = true;
        }
        protected void gridBranch_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
        {
            ASPxGridView grid = (sender as ASPxGridView);
            if (!grid.IsNewRowEditing)
            {
                grid.DoRowValidation();
            }
        }
    }
}