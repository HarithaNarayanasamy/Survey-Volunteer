using CrystalDecisions.Enterprise;
using DevExpress.Web.ASPxGridView;
using SVCF_BusinessAccessLayer;
using SVCF_DataAccessLayer;
using SVCF_TransactionLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using log4net;

namespace SreeVisalamChitFundLtd_phase1
{
    public partial class CustomerAppEdit : System.Web.UI.Page
    {
        #region Object
        BusinessLayer balayer = new BusinessLayer();
        TransactionLayer trn = new TransactionLayer();
        #endregion
        int memid; int bname; string m1;
        ILog logger = log4net.LogManager.GetLogger(typeof(WebForm4));

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {


                //GetMembers(balayer.ToobjectstrEvenNull(Session["Branchid"]));

                LoadbranchList();


                var userinfo = HttpContext.Current.User.Identity.Name;
                var qry = "select rs.name from roles as rs inner join rights as rt on (rt.roleid=rs.id) where memberid=" + userinfo + "";
                var usrRole = balayer.GetSingleValue(qry);
                if (usrRole == "Report")
                {
                    Response.Redirect("Home.aspx", false);
                }

                foreach (GridViewColumn column in gridBranch.Columns)
                {
                    if (column is GridViewDataColumn)
                    {
                        ((GridViewDataColumn)column).Settings.AutoFilterCondition = AutoFilterCondition.Contains;
                    }
                }
            }
            select();
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (Session["UserName"] == null || Session["Branchid"] == null || Session["BranchName"] == null)
            {
                Response.Redirect(Page.ResolveUrl("~/Login.aspx"), true);
            }

        }


        public void LoadbranchList()
        {
            DDLBName.DataSource = null;
            DataTable dtgroupno = null;
            dtgroupno = balayer.GetDataTable("select Head_Id , B_Name from svcf.branchdetails");
            DataRow dr = dtgroupno.NewRow();
            dr[0] = "0";
            dr[1] = "--Select--";
            DDLBName.DataValueField = "Head_Id";
            DDLBName.DataTextField = "B_Name";

            dtgroupno.Rows.InsertAt(dr, 0);
            DDLBName.DataSource = dtgroupno;
            DDLBName.DataBind();
        }


        protected void DDLBName_Click(object sender, EventArgs e)
        {
            DataTable dtmemTab;
            //DropDownList d1 = new DropDownList();
            //d1.SelectedValue=ddlMName.SelectedItem.Value;
            //lbn1.Text = d1.SelectedValue; 
            dtmemTab = balayer.GetDataTable("select concat(CustomerName,'|',`AddressForCommunication`) as CustomerName,MemberIDNew from membermaster where Branchid=" + DDLBName.SelectedItem.Value + " order by CustomerName ");
            ddlMName.DataSource = dtmemTab;
            DataRow dr = dtmemTab.NewRow();
            dr[0] = "--Select--";
            dr[1] = "0";
            ddlMName.DataTextField = "CustomerName";
            ddlMName.DataValueField = "MemberIDNew";
            dtmemTab.Rows.InsertAt(dr, 0);
            ddlMName.DataBind();



        }
        protected void btnADD_Click(object sender, EventArgs e)
        {
            //txtusername.Text = "";
            //txtpsw.Text = "";
            //memid = Convert.ToInt32(ddlMName.DataValueField);
            if (!Page.IsValid)
            {
                return;
            }

            try
            {
                //16/07/2021 - Bala
                var userName = txtusername.Text;
                if (userName != "")
                {
                    var isExists = balayer.GetSingleValue("select username from membermaster where username='" + userName + "' union select username from moneycollector where username='" + userName + "';");
                    if (!string.IsNullOrEmpty(isExists))
                    {
                        Response.Write("<script>alert('Username is already exists!Try something different.');</script>");
                    }
                    else
                    {
                        trn.insertorupdateTrn("UPDATE svcf.membermaster SET username = '" + txtusername.Text.Trim() + "', password='" + txtpsw.Text.Trim() + "' where MemberIDNew='" + ddlMName.SelectedItem.Value + "'");
                        trn.CommitTrn();
                    }
                }
            }
            catch (Exception error)
            {
                try
                {
                    trn.RollbackTrn();
                }
                catch
                { }
                finally
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Warning", "alert('" + error.Message.ToString().Replace("'", "\\'") + "');", true);
                }
            }
            finally
            {
                trn.DisposeTrn();
            }

        }

        public void select()
        {
            DataSourceBranch.ConnectionString = CommonClassFile.ConnectionString;
            DataSourceBranch.SelectCommand = ("select m1.CustomerName , m1.username ,m1.password,b1.B_Name,m1.MemberIDNew from svcf.membermaster as m1 inner join svcf.branchdetails as b1 where b1.Head_Id =m1.BranchId");
            gridBranch.DataBind();
        }
        protected void gridBranch_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            //string aaaaaa = balayer.GetSingleValue("SELECT `branchdetails`.`B_Code` FROM svcf.branchdetails WHERE `branchdetails`.`Head_Id`=" + e.Keys["Head_Id"] + "");
            //balayer.GetInsertItem("delete from headstree where NodeID=" + e.Keys["Head_Id"]);
            //balayer.GetInsertItem("delete FROM svcf.membermaster where BranchId=" + e.Keys["Head_Id"] + " and MemberID='" + aaaaaa + "-1" + "'");
            //balayer.GetInsertItem("delete FROM svcf.prospect where ProspectID='" + aaaaaa + "-1" + "' and BranchId=" + e.Keys["Head_Id"]);
            //balayer.GetInsertItem("delete from branchdetails where  Head_Id=" + e.Keys["Head_Id"]);
            //ASPxGridView grid = (sender as ASPxGridView);
            //grid.DataBind();
            //e.Cancel = true;
        }
        void AddError(Dictionary<GridViewColumn, string> errors, GridViewColumn column, string errorText)
        {
            if (errors.ContainsKey(column)) return;
            errors[column] = errorText;
        }

        protected void gridBranch_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            System.Globalization.DateTimeFormatInfo usDtfi = new System.Globalization.CultureInfo("en-NZ", false).DateTimeFormat;
            string dt = e.NewValues["MemberIDNew"].ToString();
            // DateTime dFirstAuctionDate = DateTime.Parse(dt, usDtfi);
            balayer.GetInsertItem("UPDATE svcf.membermaster SET username = '" + balayer.MySQLEscapeString(balayer.ToobjectstrEvenNull(e.NewValues["username"])) + "', password = '" + balayer.MySQLEscapeString(balayer.ToobjectstrEvenNull(e.NewValues["password"])) + "' WHERE MemberIDNew=" + e.Keys["MemberIDNew"]);
            ASPxGridView grid = (sender as ASPxGridView);
            grid.DataBind();
            e.Cancel = true;
            logger.Info("EditBranchDetails.aspx - gridBranch_RowUpdating():  Completed: " + DateTime.Now + " by: " + Convert.ToString(Session["User Name"]) + "");
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