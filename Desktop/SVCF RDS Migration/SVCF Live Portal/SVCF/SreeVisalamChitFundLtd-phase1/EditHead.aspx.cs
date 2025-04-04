using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System;
using System.Data;
using System.Configuration;
using MySql.Data.MySqlClient;
using System.Text.RegularExpressions;
using DevExpress.Web.ASPxGridView;
using SVCF_BusinessAccessLayer;
using SVCF_TransactionLayer;
using SVCF_DataAccessLayer;
using log4net;
using log4net.Config;

namespace SreeVisalamChitFundLtd_phase1
{
    public partial class EditHead : System.Web.UI.Page
    {
        #region Object
        BusinessLayer balayer = new BusinessLayer();
        TransactionLayer trn = new TransactionLayer();
        #endregion
        ILog logger = log4net.LogManager.GetLogger(typeof(EditHead));

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
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
                    foreach (GridViewColumn column in gridHead.Columns)
                    {
                        if (column is GridViewDataColumn)
                        {
                            ((GridViewDataColumn)column).Settings.AutoFilterCondition = AutoFilterCondition.Contains;
                        }
                    }
                }
            }

            select();
        }


        protected void select()
        {
            DataSourceHead.ConnectionString = CommonClassFile.ConnectionString;
            DataSourceHead.SelectCommand =@"select NodeId_1 as NodeId,T1 as Mainhead,T2 as Sub1,T3 as Sub2,T4 as Sub3,NodeId_2, NodeId_3,NodeId_4,case when isnull(NodeId_4) then NodeId_3 else NodeId_4 end as HeadID 
                    from view_tree where BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and NodeId_1 not in (1,3,10,5,6,12)";
            gridHead.DataBind();
            
        }



        protected void gridHead_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            //string aaaaaa = balayer.GetSingleValue("SELECT `headstree`.`NodeID` FROM svcf.headstree WHERE `headstree`.`NodeID`=" + e.Keys["HeadID"] + "");
            //balayer.GetInsertItem("delete from headstree where NodeID=" + aaaaaa +"");
            //ASPxGridView grid = (sender as ASPxGridView);
            //grid.DataBind();
            //e.Cancel = true;
        }
        void AddError(Dictionary<GridViewColumn, string> errors, GridViewColumn column, string errorText)
        {
            if (errors.ContainsKey(column)) return;
            errors[column] = errorText;
        }

        protected void gridHead_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            //System.Globalization.DateTimeFormatInfo usDtfi = new System.Globalization.CultureInfo("en-NZ", false).DateTimeFormat;
            //string dt = e.NewValues["B_DOC"].ToString();
            //DateTime dFirstAuctionDate = DateTime.Parse(dt, usDtfi);
            string sub;
            string subb;
            string Headsub = balayer.GetSingleValue("select case when isnull(NodeId_4)  then NodeId_3 else NodeId_4 end as HeadID from view_tree where NodeId_3=" + e.Keys["HeadID"] + " or NodeId_4=" + e.Keys["HeadID"] + "");
            DataTable subhead=balayer.GetDataTable("select NodeId_3,NodeId_4 from view_tree where NodeId_3=" + e.Keys["HeadID"] + " or NodeId_4=" + e.Keys["HeadID"] + "");
            sub = subhead.Rows[0]["NodeId_3"].ToString();
            if (sub == Headsub)
            {
                balayer.GetInsertItem("UPDATE svcf.headstree SET Node='" + e.NewValues["Sub2"] + "' WHERE NodeID="+ Headsub +"");
            }
            else 
            {
                balayer.GetInsertItem("UPDATE svcf.headstree SET Node='" + e.NewValues["Sub3"] + "' WHERE NodeID=" + Headsub + "");
            }
           
            ASPxGridView grid = (sender as ASPxGridView);
            grid.DataBind();
            e.Cancel = true;
            logger.Info("EditHead.aspx - gridHead_RowUpdating():  Completed: " + DateTime.Now + " by: " + Convert.ToString(Session["UserName"]) + "");
        }
        protected void gridHead_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
        {
            ASPxGridView grid = (sender as ASPxGridView);
            if (!grid.IsNewRowEditing)
            {
                grid.DoRowValidation();
            }
        }
    }
}