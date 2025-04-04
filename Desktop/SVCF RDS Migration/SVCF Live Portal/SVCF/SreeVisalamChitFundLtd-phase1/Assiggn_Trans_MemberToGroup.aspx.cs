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
using SVCF_DataAccessLayer;
using SVCF_TransactionLayer;
using log4net;
using log4net.Config;


namespace SreeVisalamChitFundLtd_phase1
{
    public partial class Assiggn_Trans_MemberToGroup_ : System.Web.UI.Page
    {
        #region Object
        BusinessLayer balayer = new BusinessLayer();
        #endregion
        ILog logger = log4net.LogManager.GetLogger(typeof(Assiggn_Trans_MemberToGroup_));
        protected void GetMembers()
        {
            string strMemberID = balayer.GetSingleValue("SELECT New_Member FROM svcf.transfer_approval where Flag=1 and IsTranasfered=0 and GrpMemberID=" + ddlGrpMemberID.SelectedValue + "");
            DataTable dt = balayer.GetDataTable("SELECT concat(MemberID, ' | ',CustomerName,'|',AddressForCommunication) as CustomerName,cast(MemberIDNew as char) as MemberIDNew FROM svcf.membermaster where MemberIDNew=" + strMemberID);
            ddlMemberName.DataSource = dt;
            DataRow dr = dt.NewRow();
            dr[0] = "--Select--";
            dr[1] = "--Select--";
            ddlMemberName.DataTextField = "CustomerName";
            ddlMemberName.DataValueField = "MemberIDNew";
            dt.Rows.InsertAt(dr, 0);
            ddlMemberName.DataBind();
        }
        protected void GetGroups()
        {
            ddlGrpID.DataSource = null;
            ddlGrpID.DataBind();
            DataTable dtGroups = balayer.GetDataTable("SELECT distinct ta.ChitGroup,gm.GROUPNO FROM svcf.transfer_approval  as ta join groupmaster as gm on (ta.ChitGroup=gm.Head_Id) where ta.Flag=1 and ta.IsTranasfered=0 and ta.BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]));
            DataRow dr = dtGroups.NewRow();
            ddlGrpID.DataSource = dtGroups;
            dr[0] = "0";
            dr[1] = "--Select--";
            ddlGrpID.DataTextField = "GROUPNO";
            ddlGrpID.DataValueField = "ChitGroup";
            dtGroups.Rows.InsertAt(dr, 0);
            ddlGrpID.DataBind();
        }
        private void BindData()
        {
            DataTable dtSelect = new DataTable();
            DataTable dt = balayer.GetDataTable("SELECT distinct NomineeName, NomineeAge, Relation, NomineeAddress, NomineeTelephoneNo, NomineeMobileNo  FROM `nomineetable` where MemberID='" + ddlMemberName.SelectedValue + "'");
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
        protected void btnInfo_yes_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.Url.AbsolutePath.ToString(), false);
        }

        protected void AddChit_Click(object sender, EventArgs e)
        {
            TransactionLayer trn = new TransactionLayer();
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
            bool isFinished = true;
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
            try
            {
                int count = 1;
                string brid = balayer.ToobjectstrEvenNull(balayer.ToobjectstrEvenNull(Session["Branchid"]));

                DataTable dtDetails = balayer.GetDataTable("SELECT * FROM transfer_approval where New_Member=" + ddlMemberName.SelectedItem.Value + " and GrpMemberID=" + ddlGrpMemberID.SelectedItem.Value + " and Flag=1 and IsTranasfered=0 and ChitGroup=" + ddlGrpID.SelectedItem.Value);
                //TextBox txtNomineeNamex = (TextBox)GridGuardians.Rows[0].FindControl("txtNomineeName");
                //TextBox txtNomineeNomineeAddressx = (TextBox)GridGuardians.Rows[0].FindControl("txtNomineeNomineeAddress");
                //string FirstNomonee = txtNomineeNamex.Text;
                //string FirstNomineeAddress = txtNomineeNomineeAddressx.Text;
                DataTable dtMemberName = balayer.GetDataTable("SELECT CustomerName,ResidentialAddress,TypeOfMember FROM svcf.membermaster where MemberIDNew=" + balayer.MySQLEscapeString(balayer.ToobjectstrEvenNull(dtDetails.Rows[0]["New_Member"])));
                
                long ssss = trn.insertorupdateTrn("update transfer_approval set IsTranasfered=1,TransferredDate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "',assigndate='" + balayer.indiandateToMysqlDate(txtSuggestedDate.Text) + "',foremandate='"+balayer.indiandateToMysqlDate(txtForemanDate.Text)+"'  where SlNo=" + dtDetails.Rows[0]["SlNo"].ToString());
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
                            if (balayer.ToobjectstrEvenNull(dtMemberName.Rows[0]["TypeOfMember"]).ToString().ToLower() != "foreman")
                            {
                                string insertGroup = "update membertogroupmaster set `MemberName`='" + balayer.MySQLEscapeString(balayer.ToobjectstrEvenNull(dtMemberName.Rows[0]["CustomerName"])) + "',`MemberID`=" + balayer.MySQLEscapeString(balayer.ToobjectstrEvenNull(dtDetails.Rows[0]["New_Member"])) + ",`MemberAddress`='" + balayer.MySQLEscapeString(balayer.ToobjectstrEvenNull(dtMemberName.Rows[0]["ResidentialAddress"])) + "',`NomineeName`='" + balayer.MySQLEscapeString(txtNomineeNamey.Text) + "',`NomineeAddress`='" + balayer.MySQLEscapeString(txtNomineeNomineeAddressy.Text) + "',`card`=" + ddlNoCard.SelectedItem.Value + ",`EstCallNoOfAuction`='" + balayer.MySQLEscapeString(balayer.ToobjectstrEvenNull(dtDetails.Rows[0]["EstimatedDrawNoForAuction"])) + "',`EstSuretyDocument`='" + balayer.MySQLEscapeString(balayer.ToobjectstrEvenNull(dtDetails.Rows[0]["EstimatedSuretyDetails"])) + "',`M_Id`=" + balayer.MySQLEscapeString(balayer.ToobjectstrEvenNull(dtDetails.Rows[0]["M_ID"])) + ",`PoolNo`=null,B_Id=" + balayer.MySQLEscapeString(balayer.ToobjectstrEvenNull(dtDetails.Rows[0]["B_ID"])) + ",SuggestDate='" + balayer.indiandateToMysqlDate(txtSuggestedDate.Text) + "',headofficesuggession='" + balayer.MySQLEscapeString(balayer.ToobjectstrEvenNull(dtDetails.Rows[0]["Suggessionifany"])) + "' where Head_Id=" + dtDetails.Rows[0]["GrpMemberID"].ToString();
                                long incmd = trn.insertorupdateTrn(insertGroup);
                                string s = balayer.GetSingleValue("SELECT GroupmemberID FROM svcf.removedmaster where GroupmemberID=" + ddlGrpMemberID.SelectedItem.Value);
                                if (string.IsNullOrEmpty(s))
                                {
                                }
                                else
                                {
                                    long iResult = trn.insertorupdateTrn("update removedmaster set todate='" + balayer.indiandateToMysqlDate(txtSuggestedDate.Text) + "' where GroupmemberID=" + ddlGrpMemberID.SelectedItem.Value);
                                }
                            }
                            else
                            {
                                string insertGroup = "update membertogroupmaster set `MemberName`='" + balayer.MySQLEscapeString(balayer.ToobjectstrEvenNull(dtMemberName.Rows[0]["CustomerName"])) + "',`MemberID`=" + balayer.MySQLEscapeString(balayer.ToobjectstrEvenNull(dtDetails.Rows[0]["New_Member"])) + ",`MemberAddress`='" + balayer.MySQLEscapeString(balayer.ToobjectstrEvenNull(dtMemberName.Rows[0]["ResidentialAddress"])) + "',`NomineeName`='" + balayer.MySQLEscapeString(txtNomineeNamey.Text) + "',`NomineeAddress`='" + balayer.MySQLEscapeString(txtNomineeNomineeAddressy.Text) + "',`card`=" + ddlNoCard.SelectedItem.Value + ",`EstCallNoOfAuction`='" + balayer.MySQLEscapeString(balayer.ToobjectstrEvenNull(dtDetails.Rows[0]["EstimatedDrawNoForAuction"])) + "',`EstSuretyDocument`='" + balayer.MySQLEscapeString(balayer.ToobjectstrEvenNull(dtDetails.Rows[0]["EstimatedSuretyDetails"])) + "',`M_Id`=" + balayer.MySQLEscapeString(balayer.ToobjectstrEvenNull(dtDetails.Rows[0]["M_ID"])) + ",`PoolNo`=null,B_Id=" + balayer.MySQLEscapeString(balayer.ToobjectstrEvenNull(dtDetails.Rows[0]["B_ID"])) + ",SuggestDate='" + balayer.indiandateToMysqlDate(txtSuggestedDate.Text) + "',headofficesuggession='" + balayer.MySQLEscapeString(balayer.ToobjectstrEvenNull(dtDetails.Rows[0]["Suggessionifany"])) + "' where Head_Id=" + balayer.ToobjectstrEvenNull(dtDetails.Rows[0]["GrpMemberID"]);
                                long incmd = trn.insertorupdateTrn(insertGroup);
                                string s = balayer.GetSingleValue("SELECT GroupmemberID FROM svcf.removedmaster where GroupmemberID=" + ddlGrpMemberID.SelectedItem.Value);
                                if (string.IsNullOrEmpty(s))
                                {
                                    long iResult49 = trn.insertorupdateTrn("insert into headstree (ParentID,Node,Branchid) values (49,'" + ddlGrpMemberID.SelectedItem.Text + "'," + Session["Branchid"] + ")");
                                    long uResult49 = trn.insertorupdateTrn("update headstree set TreeHint='6,49," + iResult49 + "' where NodeID=" + iResult49);
                                    long iResult50 = trn.insertorupdateTrn("insert into headstree (ParentID,Node,Branchid) values (50,'" + ddlGrpMemberID.SelectedItem.Text + "'," + Session["Branchid"] + ")");
                                    long uResult50 = trn.insertorupdateTrn("update headstree set TreeHint='6,50," + iResult50 + "' where NodeID=" + iResult50);
                                    long iResult = trn.insertorupdateTrn("insert into removedmaster (GroupmemberID,foremansubstitutedchitprized,foremansubstitutedchitcall,fromdate) values (" + ddlGrpMemberID.SelectedItem.Value + "," + iResult49 + "," + iResult50 + ",'" + balayer.indiandateToMysqlDate(txtSuggestedDate.Text) + "')");
                                }

                            }
                        }
                        string insertnominee = "insert into nomineetable(`MemberID`,`NomineeName`,`NomineeAge`,`Relation`,`NomineeAddress`,`NomineeTelephoneNo`,`NomineeMobileNo`,`GrpMemberID`) values(" + balayer.MySQLEscapeString(ddlMemberName.SelectedItem.Value) + ",'" + balayer.MySQLEscapeString(txtNomineeNamey.Text) + "'," + balayer.MySQLEscapeString(txtNomineeAgey.Text) + ",'" + balayer.MySQLEscapeString(txtNomineeRelationy.Text) + "','" + balayer.MySQLEscapeString(txtNomineeNomineeAddressy.Text) + "','" + balayer.MySQLEscapeString(txtNomineeTelephoneNoy.Text) + "','" + balayer.MySQLEscapeString(txtNomineeMobileNoy.Text) + "'," + balayer.MySQLEscapeString(balayer.ToobjectstrEvenNull(ddlGrpMemberID.SelectedItem.Value)) + ")";
                        long innominee = trn.insertorupdateTrn(insertnominee);
                    }
                }
                trn.CommitTrn();
                logger.Info("Assign_Trans_MemberToGroup.aspx - AddChit_Click() - Completed: " + DateTime.Now + " by: " + Convert.ToString(Session["UserName"]) + "");
            }
            catch(Exception error)
            {
                try
                {
                   trn.RollbackTrn();
                }
                catch(Exception ex)
                {
                    logger.Info("Assign_Trans_MemberToGroup.aspx - AddChit_Click() - Error: " + ex.Message + ": " + DateTime.Now + " by: " + Convert.ToString(Session["UserName"]) + "");
                }
                finally
                {
                    isFinished = false;
                    lblMsgInfoContent.Text = error.Message;
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
        protected void ddlGrpMemberID_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetMembers();
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
            if (!balayer.ToobjectstrEvenNull(ddlGrpID.SelectedItem.Text).Equals("--Select--"))
            {
                DataTable dtToken = balayer.GetDataTable("SELECT distinct ta.GrpMemberID,m1.`GrpMemberID` as GroupName FROM svcf.transfer_approval as ta join `membertogroupmaster` as m1 on (ta.GrpMemberID=m1.Head_Id) where ta.Flag=1 and ta.ChitGroup=" + ddlGrpID.SelectedValue);
                ddlGrpMemberID.DataSource = dtToken;
                DataRow dr = dtToken.NewRow();
                dr[0] = "0";
                dr[1] = "--Select--";
                ddlGrpMemberID.DataTextField = "GroupName";
                ddlGrpMemberID.DataValueField = "GrpMemberID";
                dtToken.Rows.InsertAt(dr, 0);
                ddlGrpMemberID.DataBind();
            }
            else
            {
                Page.Validate();
                ddlGrpMemberID.Items.Clear();
                ddlMemberName.ClearSelection();
                ddlMemberName.Enabled = false;
            }
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (Session["UserName"] == null || Session["Branchid"] == null || Session["BranchName"] == null)
            {
                Response.Redirect(Page.ResolveUrl("~/Login.aspx"), false);
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

                Session["CheckRefresh"] = System.Guid.NewGuid().ToString();
                GetGroups();
                SetDefaultRow();
            }
        }
    }
}
