using System;
using System.Collections;
using System.Collections.Generic;
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
using SVCF_TransactionLayer;
using SVCF_DataAccessLayer;
using log4net;
using log4net.Config;

namespace SreeVisalamChitFundLtd_phase1
{
    public partial class AssiggnMemberToGroup_ : System.Web.UI.Page
    {
        #region Object
        BusinessLayer balayer = new BusinessLayer();
        TransactionLayer trn = new TransactionLayer();
        #endregion

        ILog logger = log4net.LogManager.GetLogger(typeof(AssiggnMemberToGroup_));

        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (Session["UserName"] == null || Session["Branchid"] == null || Session["BranchName"] == null)
            {
                Response.Redirect(Page.ResolveUrl("~/Login.aspx"), false);
            }
        }
        protected void GetMembers()
        {
            string strQuery = "SELECT cast(t1.MemberID as char) as MemberID, concat( t2.CustomerName ,'|', t2.AddressForCommunication) as CustomerName FROM `membersuggestion` as t1  left Join membermaster as t2 on t1.MemberID=t2.MemberIDNew where t1.GroupNo=" + ddlGrpID.SelectedValue + " and  (ApprovedDate) is not null and t1.`NoOfAssignedToken`<t1.`NoofTokensApproved`";
            DataTable dt = balayer.GetDataTable(strQuery);
            ddlMemberName.DataSource = dt;
            DataRow dr = dt.NewRow();
            dr[0] = "--Select--";
            dr[1] = "--Select--";
            ddlMemberName.DataTextField = "CustomerName";
            ddlMemberName.DataValueField = "MemberID";
            dt.Rows.InsertAt(dr, 0);
            ddlMemberName.DataBind();
            if (dt.Rows.Count == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Warning", "alert('No suggested Members!!!');", true);
            }
        }
        protected void GetGroups()
        {
            DataTable dtGroups = balayer.GetDataTable("SELECT t1.GROUPNO as GroupNo,t1.Head_Id,t1.NoofMembers as NoofMembers,Count(t2.GroupID) as NoofFilledToken FROM `groupmaster` as t1 left Join  membertogroupmaster as t2 on (t1.Head_Id=t2.GroupID) where t1.BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " Group by t1.GroupNo HAVING Count(t2.GroupID)<>NoofMembers");
            ddlGrpID.DataSource = null;
            ddlGrpID.DataBind();
            ddlGrpID.DataSource = dtGroups;
            DataRow dr = dtGroups.NewRow();
            dr[0] = "--Select--";
            dr[1] = "0";
            ddlGrpID.DataTextField = "GroupNo";
            ddlGrpID.DataValueField = "Head_Id";
            dtGroups.Rows.InsertAt(dr, 0);
            ddlGrpID.DataBind();
        }
        private void BindData()
        {
            DataTable dtSelect = new DataTable();
            DataTable dt = balayer.GetDataTable("SELECT distinct  NomineeName, NomineeAge, Relation, NomineeAddress, NomineeTelephoneNo, NomineeMobileNo  FROM `nomineetable` where MemberID='" + ddlMemberName.SelectedValue + "'");
            DataColumn dcShowButton = new DataColumn("ShowAddButton", typeof(bool));
            DataColumn dcShowRemoveButton = new DataColumn("ShowRemoveButton", typeof(bool));
            DataColumn dcIsNominee = new DataColumn("IsNominee", typeof(bool));
            dcShowButton.DefaultValue = false;
            dcShowRemoveButton.DefaultValue = false;
            dcIsNominee.DefaultValue = false;
            dt.Columns.Add(dcShowButton);
            dt.Columns.Add(dcShowRemoveButton);
            dt.Columns.Add(dcIsNominee);
            if (dt.Rows.Count == 0)
            {
                dt.Rows.Add();
            }
            dt.Rows[dt.Rows.Count - 1]["ShowAddButton"] = true;
            dt.Rows[dt.Rows.Count - 1]["ShowRemoveButton"] = true;
            GridGuardians.DataSource = dt;
            GridGuardians.DataBind();
        }
        public void SetDefaultRow()
        {
            DataTable dt = balayer.GetDataTable("SELECT distinct  NomineeName, NomineeAge, Relation, NomineeAddress, NomineeTelephoneNo, NomineeMobileNo   FROM `nomineetable` where MemberID='UnknownForDefaultRow'");
            DataColumn dcShowButton = new DataColumn("ShowAddButton", typeof(bool));
            DataColumn dcShowRemoveButton = new DataColumn("ShowRemoveButton", typeof(bool));
            DataColumn dcIsNominee = new DataColumn("IsNominee", typeof(bool));
            dcShowButton.DefaultValue = true;
            dcShowRemoveButton.DefaultValue = true;
            dcIsNominee.DefaultValue = false;
            dt.Columns.Add(dcShowButton);
            dt.Columns.Add(dcShowRemoveButton);
            dt.Columns.Add(dcIsNominee);
            dt.Rows.Add();
            GridGuardians.DataSource = dt;
            GridGuardians.DataBind();
        }
        protected void AddChit_Click(object sender, EventArgs e)
        {
           // TransactionLayer trn = new TransactionLayer();
            Page.Validate("GrpRow");
            Page.Validate("Add");
            if (Page.IsValid == false)
            {
                return;
            }
            if (Session["CheckRefresh"].ToString() != ViewState["CheckRefresh"].ToString())
            {
                return;
            }
            int chjed = 0;
            int tselRow = -1;
            int selRow = 0;
            foreach (GridViewRow dr in GridGuardians.Rows)
            {
                tselRow += 1;
                CheckBox chkNominee = (CheckBox)dr.FindControl("chkNominee");
                if (chkNominee.Checked == true)
                {
                    if (selRow == 0)
                    {
                        selRow = tselRow;
                    }
                    
                    
                    chjed += 1;
                }
            }
            if (chjed == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Warning", "alert('Please Choose Nominee!!!');", true);
                return;
            }
            bool isFinished = true;
            try
            {
                int count = 1;
                string brid = balayer.ToobjectstrEvenNull(balayer.ToobjectstrEvenNull(Session["Branchid"]));
                // EstSuretyDocument, EstCallNoOfAuction
                long groupheadid = trn.insertorupdateTrn("insert into headstree(ParentID, Node, TreeHint,Branchid) values(" + ddlGrpID.SelectedValue + ",'" + ddlGrpMemberID.SelectedItem.Text + "','Null'," + Session["Branchid"] + ")");
                string strPreviousID = balayer.GetSingleValue("select TreeHint from headstree where `headstree`.`Node`='" + ddlGrpID.SelectedItem.Text + "' and TreeHint like '5,%'");
                string strIdtoInsert = strPreviousID.Trim(',') + "," + groupheadid;
                strIdtoInsert = strIdtoInsert.Trim(',');
                string strUpdateQuery = "UPDATE headstree SET TreeHint='" + strIdtoInsert + "'" + " WHERE NodeID=" + groupheadid + "";
                long dddd=trn.insertorupdateTrn(strUpdateQuery);
                DataTable dtDetails = balayer.GetDataTable("SELECT * FROM `membersuggestion` where MemberID=" + ddlMemberName.SelectedValue + " and GroupNo=" + ddlGrpID.SelectedValue);

                string suggestBranch = balayer.ToobjectstrEvenNull(dtDetails.Rows[0]["SuggestedBranchId"]);

                long sss = trn.insertorupdateTrn("update svcf.membersuggestion set NoOfAssignedToken=NoOfAssignedToken+1,assigneddate='" + balayer.indiandateToMysqlDate(txtReceiptDate.Text) + "',receiptdate='" + balayer.indiandateToMysqlDate(txtReceiptDate.Text) + "'  where slno=" + dtDetails.Rows[0]["slno"].ToString());

                foreach (GridViewRow dr in GridGuardians.Rows)
                {
                    CheckBox chkNominee = (CheckBox)dr.FindControl("chkNominee");
                    if (chkNominee.Checked == true)
                    {
                        TextBox txtNomineeNamey = (TextBox)dr.FindControl("txtNomineeName");
                        TextBox txtNomineeAgey = (TextBox)dr.FindControl("txtNomineeAge");
                        if (txtNomineeAgey.Text == "")
                        {
                            txtNomineeAgey.Text = "0";
                        }
                        TextBox txtNomineeRelationy = (TextBox)dr.FindControl("txtNomineeRelation");
                        TextBox txtNomineeNomineeAddressy = (TextBox)dr.FindControl("txtNomineeNomineeAddress");
                        TextBox txtNomineeTelephoneNoy = (TextBox)dr.FindControl("txtNomineeTelephoneNo");
                        TextBox txtNomineeMobileNoy = (TextBox)dr.FindControl("txtNomineeMobileNo");
                        if (count == 1)
                        {
                            long incmd = trn.insertorupdateTrn("insert into membertogroupmaster(`Head_Id`,`BranchID`,`MemberName`,`MemberID`,`MemberAddress`,`GroupID`,`GrpMemberID`,`NomineeName`,`NomineeAddress`,`card`,`EstCallNoOfAuction`,`EstSuretyDocument`,`M_Id`,`PoolNo`,`B_Id`,headofficesuggession,SuggestDate) values(" + groupheadid + "," + balayer.ToobjectstrEvenNull(Session["Branchid"]) + ",'" + balayer.ToobjectstrEvenNull(ddlMemberName.SelectedItem.Text.Split('|')[0].Trim()) + "','" + balayer.MySQLEscapeString(ddlMemberName.SelectedValue) + "','" + balayer.MySQLEscapeString(ddlMemberName.SelectedItem.Text.Split('|')[1]) + "','" + ddlGrpID.SelectedValue + "','" + balayer.ToobjectstrEvenNull(ddlGrpMemberID.SelectedValue) + "','" + balayer.MySQLEscapeString(txtNomineeNamey.Text) + "','" + balayer.MySQLEscapeString(txtNomineeNomineeAddressy.Text) + "'," + balayer.MySQLEscapeString(ddlNoCard.SelectedItem.Value) + ",'" + balayer.MySQLEscapeString(balayer.ToobjectstrEvenNull(dtDetails.Rows[0]["EstCallNoOfAuction"])) + "','" + balayer.MySQLEscapeString(balayer.ToobjectstrEvenNull(dtDetails.Rows[0]["EstSuretyDocument"])) + "'," + balayer.ToobjectstrEvenNull(dtDetails.Rows[0]["M_ID"]) + ",null," + suggestBranch + ",'" + balayer.MySQLEscapeString(balayer.ToobjectstrEvenNull(dtDetails.Rows[0]["Suggessionifany"])) + "','" + balayer.indiandateToMysqlDate(txtReceiptDate.Text) + "')");
                            string sssss = balayer.GetSingleValue("SELECT TypeOfMember FROM svcf.membermaster where MemberIDNew=" + balayer.MySQLEscapeString(ddlMemberName.SelectedValue));
                            if (sssss.ToLower() == "foreman")
                            {
                                long iResult49 = trn.insertorupdateTrn("insert into headstree (ParentID,Node,Branchid) values (49,'" + ddlGrpMemberID.SelectedItem.Text + "'," + Session["Branchid"] + ")");
                                long uResult49 = trn.insertorupdateTrn("update headstree set TreeHint='6,49," + iResult49 + "' where NodeID=" + iResult49);
                                long iResult50 = trn.insertorupdateTrn("insert into headstree (ParentID,Node,Branchid) values (50,'" + ddlGrpMemberID.SelectedItem.Text + "'," + Session["Branchid"] + ")");
                                long uResult50 = trn.insertorupdateTrn("update headstree set TreeHint='6,50," + iResult50 + "' where NodeID=" + iResult50);
                                long iResult = trn.insertorupdateTrn("insert into removedmaster (GroupmemberID,foremansubstitutedchitprized,foremansubstitutedchitcall,fromdate) values (" + ddlGrpMemberID.SelectedItem.Value + "," + iResult49 + "," + iResult50 + ",'" + balayer.indiandateToMysqlDate(txtReceiptDate.Text) + "')");
                            }
                        }
                        long innominee = trn.insertorupdateTrn("insert into nomineetable(`MemberID`,`NomineeName`,`NomineeAge`,`Relation`,`NomineeAddress`,`NomineeTelephoneNo`,`NomineeMobileNo`,`GrpMemberID`) values(" + balayer.MySQLEscapeString(ddlMemberName.SelectedItem.Value) + ",'" + balayer.MySQLEscapeString(txtNomineeNamey.Text) + "'," + balayer.MySQLEscapeString(txtNomineeAgey.Text) + ",'" + balayer.MySQLEscapeString(txtNomineeRelationy.Text) + "','" + balayer.MySQLEscapeString(txtNomineeNomineeAddressy.Text) + "','" + balayer.MySQLEscapeString(txtNomineeTelephoneNoy.Text) + "','" + balayer.MySQLEscapeString(txtNomineeMobileNoy.Text) + "'," + groupheadid + ")");
                    }
                }
                trn.CommitTrn();
                logger.Info("AssignMemberToGroup.aspx - AddChit_Click() - Completed: " + DateTime.Now + " by: " + Convert.ToString(Session["UserName"]) + "");
            }
            catch (Exception ex)
            {
                try
                {
                    trn.RollbackTrn();
                }
                catch (Exception error)
                {
                    logger.Info("AssignMemberToGroup.aspx - AddChit_Click() - Error: " + error.Message + ": " + DateTime.Now + " by: " + Convert.ToString(Session["UserName"]) + "");
                }
                finally
                {
                    isFinished = false;
                    lblMsgInfoContent.Text = ex.Message;
                    lblMsgInfoContent.ForeColor = System.Drawing.Color.Red;
                    // cnt1.Attributes["class"] = "info";
                    ModalPopupExtender1.PopupControlID = "msgbox";
                    ModalPopupExtender1.Show();
                    msgbox.Visible = true;
                }
            }
            finally
            {
                trn.DisposeTrn();
                if (isFinished)
                {
                    lblMsgInfoContent.Text = " Assigned Successfully !!!";
                    lblMsgInfoContent.ForeColor = System.Drawing.Color.Green;
                    // cnt1.Attributes["class"] = "info";
                    ModalPopupExtender1.PopupControlID = "msgbox";
                    ModalPopupExtender1.Show();
                    msgbox.Visible = true;
                }
            }
        }
        protected void btnInfo_yes_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.Url.AbsolutePath.ToString(), false);
        }
        protected void GridGuardians_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Add")
            {
                Page.Validate("GrpRow");
                if (Page.IsValid == false)
                {
                    return;
                }
                DataTable dt = balayer.GetDataTable("SELECT distinct  NomineeName, NomineeAge, Relation, NomineeAddress, NomineeTelephoneNo, NomineeMobileNo   FROM `nomineetable` where MemberID='UnknownForDefaultRow'");
                DataColumn dcShowButton = new DataColumn("ShowAddButton", typeof(bool));
                DataColumn dcShowRemoveButton = new DataColumn("ShowRemoveButton", typeof(bool));
                DataColumn dcIsNominee = new DataColumn("IsNominee", typeof(bool));
                dcShowButton.DefaultValue = true;
                dcShowRemoveButton.DefaultValue = true;
                dcIsNominee.DefaultValue = false;
                dt.Columns.Add(dcShowButton);
                dt.Columns.Add(dcShowRemoveButton);
                dt.Columns.Add(dcIsNominee);
                foreach (GridViewRow dr in GridGuardians.Rows)
                {
                    DataRow dtrow = dt.NewRow();
                    CheckBox chkNominee = (CheckBox)dr.FindControl("chkNominee");
                    dtrow["IsNominee"] = chkNominee.Checked;
                    TextBox txtNomineeName = (TextBox)dr.FindControl("txtNomineeName");
                    dtrow["NomineeName"] = txtNomineeName.Text;
                    TextBox txtNomineeAge = (TextBox)dr.FindControl("txtNomineeAge");
                    if (txtNomineeAge.Text == "")
                    {
                        txtNomineeAge.Text = "0";
                    }
                    dtrow["NomineeAge"] = txtNomineeAge.Text;
                    TextBox txtNomineeRelation = (TextBox)dr.FindControl("txtNomineeRelation");
                    dtrow["Relation"] = txtNomineeRelation.Text;
                    TextBox txtNomineeNomineeAddress = (TextBox)dr.FindControl("txtNomineeNomineeAddress");
                    dtrow["NomineeAddress"] = txtNomineeNomineeAddress.Text;
                    TextBox txtNomineeTelephoneNo = (TextBox)dr.FindControl("txtNomineeTelephoneNo");
                    dtrow["NomineeTelephoneNo"] = txtNomineeTelephoneNo.Text;
                    TextBox txtNomineeMobileNo = (TextBox)dr.FindControl("txtNomineeMobileNo");
                    dtrow["NomineeMobileNo"] = txtNomineeMobileNo.Text;
                    //txtNomineeNomineeAddress
                    ImageButton showbutton = (ImageButton)dr.FindControl("imgBtnAdd");
                    dtrow["ShowAddButton"] = false;
                    ImageButton showRemovebutton = (ImageButton)dr.FindControl("imgBtnRemove");
                    dtrow["ShowRemoveButton"] = false;
                    dt.Rows.Add(dtrow);
                }
                DataRow newRow = dt.NewRow();
                newRow["IsNominee"] = false;
                //newRow["First_Name"] = "";
                //newRow["Middle_Name"] = "";
                //newRow["Last_Name"] = "";
                newRow["ShowAddButton"] = true;
                newRow["ShowRemoveButton"] = true;
                dt.Rows.Add(newRow);
                GridGuardians.DataSource = dt;
                GridGuardians.DataBind();
            }
            else
                if (e.CommandName == "Remove")
                {
                    GridViewRow gridRow = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                    int RowIndex = gridRow.RowIndex;
                    if (RowIndex == 0)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "Warning", "alert('You Cant Delete!!!');", true);
                        return;
                    }
                    DataTable dt = balayer.GetDataTable("SELECT distinct  NomineeName, NomineeAge, Relation, NomineeAddress, NomineeTelephoneNo, NomineeMobileNo   FROM `nomineetable` where MemberID='UnknownForDefaultRow'");
                    DataColumn dcShowButton = new DataColumn("ShowAddButton", typeof(bool));
                    DataColumn dcShowRemoveButton = new DataColumn("ShowRemoveButton", typeof(bool));
                    DataColumn dcIsNominee = new DataColumn("IsNominee", typeof(bool));
                    dcShowButton.DefaultValue = true;
                    dcShowRemoveButton.DefaultValue = true;
                    dcIsNominee.DefaultValue = false;
                    dt.Columns.Add(dcShowButton);
                    dt.Columns.Add(dcShowRemoveButton);
                    dt.Columns.Add(dcIsNominee);
                    foreach (GridViewRow dr in GridGuardians.Rows)
                    {
                        DataRow dtrow = dt.NewRow();
                        CheckBox chkNominee = (CheckBox)dr.FindControl("chkNominee");
                        dtrow["IsNominee"] = chkNominee.Checked;
                        TextBox txtNomineeName = (TextBox)dr.FindControl("txtNomineeName");
                        dtrow["NomineeName"] = txtNomineeName.Text;
                        TextBox txtNomineeAge = (TextBox)dr.FindControl("txtNomineeAge");
                        if (txtNomineeAge.Text == "")
                        {
                            txtNomineeAge.Text = "0";
                        }
                        dtrow["NomineeAge"] = txtNomineeAge.Text;
                        TextBox txtNomineeRelation = (TextBox)dr.FindControl("txtNomineeRelation");
                        dtrow["Relation"] = txtNomineeRelation.Text;
                        TextBox txtNomineeNomineeAddress = (TextBox)dr.FindControl("txtNomineeNomineeAddress");
                        dtrow["NomineeAddress"] = txtNomineeNomineeAddress.Text;
                        TextBox txtNomineeTelephoneNo = (TextBox)dr.FindControl("txtNomineeTelephoneNo");
                        dtrow["NomineeTelephoneNo"] = txtNomineeTelephoneNo.Text;
                        TextBox txtNomineeMobileNo = (TextBox)dr.FindControl("txtNomineeMobileNo");
                        dtrow["NomineeMobileNo"] = txtNomineeMobileNo.Text;
                        //txtNomineeNomineeAddress
                        ImageButton showbutton = (ImageButton)dr.FindControl("imgBtnAdd");
                        dtrow["ShowAddButton"] = false;
                        ImageButton showRemovebutton = (ImageButton)dr.FindControl("imgBtnRemove");
                        dtrow["ShowRemoveButton"] = false;
                        dt.Rows.Add(dtrow);
                    }
                    dt.Rows.RemoveAt(dt.Rows.Count - 1);
                    dt.Rows[dt.Rows.Count - 1]["ShowAddButton"] = true ;
                    dt.Rows[dt.Rows.Count - 1]["ShowRemoveButton"] = true;
                    //DataRow newRow = dt.NewRow();
                    //newRow["IsNominee"] = false;
                    ////newRow["First_Name"] = "";
                    ////newRow["Middle_Name"] = "";
                    ////newRow["Last_Name"] = "";
                    //newRow["ShowAddButton"] = true;
                    //newRow["ShowRemoveButton"] = true;
                    //dt.Rows.Add(newRow);
                    GridGuardians.DataSource = dt;
                    GridGuardians.DataBind();
                }
        }
        protected void ddlMemberName_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetDefaultRow();
            BindData();
        }
        protected void ddlGrpID_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetMembers();
            if (!balayer.ToobjectstrEvenNull(ddlGrpID.SelectedItem).Equals("--Select--"))
            {
                ddlMemberName.ClearSelection();
                List<int> loadGrpMemid = new List<int>();
                List<int> existingGrpMemID = new List<int>();
                //ddlMemberName.Enabled = true;
                int noOfMembers;
                string GrpMemID = "SELECT cast(digits(GrpMemberID) as unsigned) as GrpMemberID FROM   svcf.membertogroupmaster  where GroupID=" + ddlGrpID.SelectedValue;
                DataTable grpmemIDTab = balayer.GetDataTable(GrpMemID);

                noOfMembers = Convert.ToInt32(balayer.GetSingleValue("Select NoofMembers from groupmaster where Head_Id=" + ddlGrpID.SelectedValue));
                    for (int noid = 0; noid < grpmemIDTab.Rows.Count; noid++)
                    {
                        existingGrpMemID.Add(int.Parse(grpmemIDTab.Rows[noid]["GrpMemberID"].ToString()));
                    }
                
                    /*Load all grpId initially and add it to seperate list*/
                    for (int loadid = 1; loadid <= noOfMembers; loadid++)
                    {
                        //loadGrpMemid.Add(balayer.ToobjectstrEvenNull(ddlGrpID.SelectedItem.Text) + "/" + loadid);
                        loadGrpMemid.Add(loadid);
                    
                    }
                    List<int> RemainingId = loadGrpMemid.Except(existingGrpMemID).ToList();
                    ddlGrpMemberID.Items.Clear();
                 ddlGrpMemberID.Items.Add("--Select--");
                 for (int iItem = 0; iItem < RemainingId.Count(); iItem++)
                 {
                     ddlGrpMemberID.Items.Add(ddlGrpID.SelectedItem.Text +"/"+  RemainingId[iItem].ToString());
                     
                 }
                    /*Load existing grpId from membertogroupmaster*/
                    //List<string> existingGrpMemID = new List<string>();
                    //if (grpmemIDTab.Rows.Count > 0)
                    //{
                    //    for (int noid = 0; noid < grpmemIDTab.Rows.Count; noid++)
                    //    {
                    //        existingGrpMemID.Add(balayer.ToobjectstrEvenNull(grpmemIDTab.Rows[noid]["GrpMemberID"]));
                    //    }
                    //    int toloadno = noOfMembers - existingGrpMemID.Count;
                    //    List<string> remGrpID = new List<string>();
                    //    remGrpID = loadGrpMemid.Except(existingGrpMemID).ToList();
                    //    ddlGrpMemberID.Items.Clear();
                    //    //if (remGrpID.Count == 0)
                    //    //{
                    //    //    ddlGrpMemberID.Items.Add("completed");
                    //    //    ddlMemberName.SelectedIndex = -1;
                    //    //    ddlMemberName.Enabled = false;
                    //    //}
                    //    //else
                    //    //{
                    //    ddlGrpMemberID.Items.Add("--Select--");
                    //    ddlMemberName.Enabled = true;
                    //    ddlMemberName.ClearSelection();
                    //    ;
                    //    foreach (string grpid in remGrpID)
                    //    {
                    //        ddlGrpMemberID.Items.Add(grpid);
                    //    }
                    //    //}
                    //    ddlGrpMemberID.ClearSelection();
                    //}
                    //else
                    //{
                    //    ddlGrpMemberID.Items.Add("--Select--");
                    //    ddlMemberName.ClearSelection();
                    //    foreach (string grpid in loadGrpMemid)
                    //    {
                    //        ddlGrpMemberID.Items.Add(grpid);
                    //    }
                    //}
                
            }
            else
            {
                Page.Validate();
                ddlGrpMemberID.Items.Clear();
                ddlMemberName.ClearSelection();
                ddlMemberName.Enabled = false;
            }
        }
        protected void Page_PreRender(object sender, EventArgs e)
        {
            ViewState["CheckRefresh"] = Session["CheckRefresh"];
        }
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

                Session["CheckRefresh"] =
                    Server.UrlDecode(System.DateTime.Now.ToString());
                GetGroups();
                SetDefaultRow();
            }
        }
    }
}
