using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SVCF_BusinessAccessLayer;
using SVCF_TransactionLayer;
using SVCF_DataAccessLayer;
using log4net;
using log4net.Config;

namespace SreeVisalamChitFundLtd_phase1
{
    public partial class ChitsUnderHeads : System.Web.UI.Page
    {
        #region Object
        BusinessLayer balayer = new BusinessLayer();
        TransactionLayer trn = new TransactionLayer();
        #endregion

        ILog logger = log4net.LogManager.GetLogger(typeof(ChitsUnderHeads));

        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (Session["UserName"] == null || Session["Branchid"] == null || Session["BranchName"] == null)
            {
                Response.Redirect(Page.ResolveUrl("~/Login.aspx"), true);
            }
        }

       
        
        [System.Web.Services.WebMethod]
        public static string getmembername(string MemberID)
        {
            string Tokenname = "";
            try
            {
               BusinessLayer balayer = new BusinessLayer();
               Tokenname = balayer.GetSingleValue("select cast(group_concat(GrpMemberID) as char) from membertogroupmaster where MemberID="  + MemberID );
              
            }
            catch (Exception) { 
            }
            return Tokenname;
        }




        protected void Page_Load(object sender, EventArgs e)
        {
            this.ModalPopupExtender2.Hide();
            Pnlmsg.Visible = false;
            if (!IsPostBack)
            {
                GetDropDown();
                this.ModalPopupExtender2.Hide();
                Pnlmsg.Visible = false;

                var userinfo = HttpContext.Current.User.Identity.Name;
                var qry = "select rs.name from roles as rs inner join rights as rt on (rt.roleid=rs.id) where memberid=" + userinfo + "";
                var usrRole = balayer.GetSingleValue(qry);
                if (usrRole != "Administrator") Response.Redirect("Home.aspx", false);

                ////DataTable dt = balayer.GetDataTable("SELECT concat(GrpMemberID,' | ',MemberName) as ChitName,concat(Head_Id,'|',MemberID) as ChitNumber FROM svcf.membertogroupmaster where BranchId="+Session["Branchid"]);
                //DataTable dt = balayer.GetDataTable("SELECT concat(GrpMemberID,' | ',MemberName) as ChitName,concat(Head_Id,'|',MemberID) as ChitNumber FROM svcf.membertogroupmaster");
                //DataRow dr = dt.NewRow();
                //dr[0] = "--Select--";
                //dr[1] = "0";
                //dt.Rows.InsertAt(dr,0);
                //ddlChitNumber.DataSource = dt;
                //ddlChitNumber.DataTextField = "ChitName";
                //ddlChitNumber.DataValueField = "ChitNumber";
                //ddlChitNumber.DataBind();
                Session["CheckRefresh"] = System.Guid.NewGuid().ToString();
            }
        }

        private void GetDropDown()
        {
            ddlbranch.DataSource = null;
            ddlbranch.DataBind();
            DataTable Accbank1 = balayer.GetDataTable("select B_Name,Head_Id from branchdetails order by B_Name asc");
            DataRow drinvestment;
            drinvestment = Accbank1.NewRow();
            drinvestment[0] = "--select--";
            drinvestment[1] = "0";
            Accbank1.Rows.InsertAt(drinvestment, 0);
            ddlbranch.DataSource = Accbank1;
            ddlbranch.DataTextField = "B_Name";
            ddlbranch.DataValueField = "Head_Id";
            ddlbranch.DataBind();
        }

        protected void ddlbranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlChitNumber.DataSource = null;
            ddlChitNumber.DataBind();
            DataTable dt = balayer.GetDataTable("SELECT concat(GrpMemberID,' | ',MemberName) as ChitName,concat(Head_Id,'|',MemberID) as ChitNumber FROM svcf.membertogroupmaster where BranchID= " + ddlbranch.SelectedItem.Value + "");
            DataRow dr = dt.NewRow();
            dr[0] = "--Select--";
            dr[1] = "0";
            dt.Rows.InsertAt(dr, 0);
            ddlChitNumber.DataSource = dt;
            ddlChitNumber.DataTextField = "ChitName";
            ddlChitNumber.DataValueField = "ChitNumber";
            ddlChitNumber.DataBind();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Page.Validate("group");
            if (!Page.IsValid)
            {
                return;
            }
            if (Session["CheckRefresh"].ToString() != ViewState["CheckRefresh"].ToString())
            {
                return;
            }
            bool isVal = true;
           // TransactionLayer trn = new TransactionLayer();
            try
            {
                if (ddlHeads.SelectedItem.Value == "51")
                {
                    long iresult51 = trn.insertorupdateTrn("insert into headstree(ParentID, Node, Branchid) values(51,'" + balayer.MySQLEscapeString(txtChitNumber.Text) + " " + balayer.MySQLEscapeString(txtName.Text) + "'," + Session["Branchid"] + ")");
                    string str51 = balayer.GetSingleValue("select TreeHint from headstree where NodeID=51");

                    trn.insertorupdateTrn("update headstree set TreeHint='" + str51 + "," + iresult51 + "' where NodeID=" + iresult51);

                    trn.insertorupdateTrn("insert into chitheads(`ChitNumber`,`ChitName`,`MemberID`,`MemberName`,`BranchID`,`ParentID`,HeadId) values (" + ddlChitNumber.SelectedValue.Split('|')[0] + ",'" + balayer.MySQLEscapeString(txtChitNumber.Text) + "'," + ddlChitNumber.SelectedValue.Split('|')[1] + ",'" + balayer.MySQLEscapeString(txtName.Text) + "'," + Session["Branchid"] + ",51," + iresult51 + ")");

                    long iresult52 = trn.insertorupdateTrn("insert into headstree(ParentID, Node, Branchid) values(52,'" + balayer.MySQLEscapeString(txtChitNumber.Text) + " " + balayer.MySQLEscapeString(txtName.Text) + "'," + Session["Branchid"] + ")");
                    string str52 = balayer.GetSingleValue("select TreeHint from headstree where NodeID=52");

                    trn.insertorupdateTrn("update headstree set TreeHint='" + str52 + "," + iresult52 + "' where NodeID=" + iresult52);

                    trn.insertorupdateTrn("insert into chitheads(`ChitNumber`,`ChitName`,`MemberID`,`MemberName`,`BranchID`,`ParentID`,HeadId) values (" + ddlChitNumber.SelectedValue.Split('|')[0] + ",'" + balayer.MySQLEscapeString(txtChitNumber.Text) + "'," + ddlChitNumber.SelectedValue.Split('|')[1] + ",'" + balayer.MySQLEscapeString(txtName.Text) + "'," + Session["Branchid"] + ",52," + iresult52 + ")");


                    //4638

                    long iresult4638 = trn.insertorupdateTrn("insert into headstree(ParentID, Node, Branchid) values(4638,'" + balayer.MySQLEscapeString(txtChitNumber.Text) + " " + balayer.MySQLEscapeString(txtName.Text) + "'," + Session["Branchid"] + ")");
                    string str4638 = balayer.GetSingleValue("select TreeHint from headstree where NodeID=4638");

                    trn.insertorupdateTrn("update headstree set TreeHint='" + str4638 + "," + iresult4638 + "' where NodeID=" + iresult4638);

                    trn.insertorupdateTrn("insert into chitheads(`ChitNumber`,`ChitName`,`MemberID`,`MemberName`,`BranchID`,`ParentID`,HeadId) values (" + ddlChitNumber.SelectedValue.Split('|')[0] + ",'" + balayer.MySQLEscapeString(txtChitNumber.Text) + "'," + ddlChitNumber.SelectedValue.Split('|')[1] + ",'" + balayer.MySQLEscapeString(txtName.Text) + "'," + Session["Branchid"] + ",4638," + iresult4638 + ")");

                }
                else
                {
                    long iresult = trn.insertorupdateTrn("insert into headstree(ParentID, Node, Branchid) values(" + ddlHeads.SelectedItem.Value + ",'" + balayer.MySQLEscapeString(txtChitNumber.Text) + " " + balayer.MySQLEscapeString(txtName.Text) + "'," + Session["Branchid"] + ")");
                    string str = balayer.GetSingleValue("select TreeHint from headstree where NodeID=" + ddlHeads.SelectedValue);

                    trn.insertorupdateTrn("update headstree set TreeHint='" + str + "," + iresult + "' where NodeID=" + iresult);

                    trn.insertorupdateTrn("insert into chitheads(`ChitNumber`,`ChitName`,`MemberID`,`MemberName`,`BranchID`,`ParentID`,HeadId) values (" + ddlChitNumber.SelectedValue.Split('|')[0] + ",'" + balayer.MySQLEscapeString(txtChitNumber.Text) + "'," + ddlChitNumber.SelectedValue.Split('|')[1] + ",'" + balayer.MySQLEscapeString(txtName.Text) + "'," + Session["Branchid"] + "," + ddlHeads.SelectedValue + "," + iresult + ")");
                }
                
                trn.CommitTrn();
                logger.Info("ChitsUnderHeads.aspx - btnAdd_Click() - Completed: " + DateTime.Now + " by: " + Convert.ToString(Session["UserName"]) + "");
            }
            catch (Exception error)
            {
                trn.RollbackTrn();
                ModalPopupExtender2.PopupControlID = "Pnlmsg";
                this.ModalPopupExtender2.Show();
                Pnlmsg.Visible = true;
                lblT.Text = "Status";
                lblContent.Text = error.Message;
                lblContent.ForeColor = System.Drawing.Color.Red;
                isVal = false;
                logger.Info("ChitsUnderHeads.aspx - btnAdd_Click() - Error: " + error.Message + ": " + DateTime.Now + " by: " + Convert.ToString(Session["UserName"]) + "");
            }
            finally
            {
                trn.DisposeTrn();
                if (isVal)
                {
                    ModalPopupExtender2.PopupControlID = "Pnlmsg";
                    this.ModalPopupExtender2.Show();
                    Pnlmsg.Visible = true;
                    lblT.Text = "Status";
                    lblContent.Text = "Data Saved Successfully";
                    lblContent.ForeColor = System.Drawing.Color.Green;
                }
            }
        }
        protected void Page_PreRender(object sender, EventArgs e)
        {
            ViewState["CheckRefresh"] = Session["CheckRefresh"];
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.Url.AbsolutePath.ToString());
        }
    }
}