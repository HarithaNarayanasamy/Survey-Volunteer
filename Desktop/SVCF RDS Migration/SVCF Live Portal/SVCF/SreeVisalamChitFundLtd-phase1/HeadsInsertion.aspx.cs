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
using DevExpress.Web.ASPxGridView;
using SVCF_BusinessAccessLayer;
using SVCF_DataAccessLayer;
using SVCF_TransactionLayer;
using System.Collections.Generic;
namespace SreeVisalamChitFundLtd_phase1
{
    public partial class HeadsInsertion : System.Web.UI.Page
    {
        #region Object
        BusinessLayer balayer = new BusinessLayer();
        TransactionLayer trn = new TransactionLayer();
        CommonClassFile objCOM = new CommonClassFile();
        #endregion
        string rootid = "";
        string value = "";
        int dataval;
        List<string> TL = new List<string>();
        
        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (Session["UserName"] == null || Session["Branchid"] == null || Session["BranchName"] == null)
            {
                Response.Redirect(Page.ResolveUrl("~/Login.aspx"), true);
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var userinfo = HttpContext.Current.User.Identity.Name;
                var qry = "select rs.name from roles as rs inner join rights as rt on (rt.roleid=rs.id) where memberid=" + userinfo + "";
                var usrRole = balayer.GetSingleValue(qry);
                if (usrRole != "Administrator") Response.Redirect("Home.aspx", false);
                
                balayer.GetInsertItem("CREATE OR REPLACE VIEW `view_tree` AS select `root`.`NodeID` AS `NodeId_1`,(case when (`down3`.`Branchid` is null) then `down2`.`Branchid` else `down3`.`Branchid` end)  AS `BranchID`, `root`.`Node` AS `T1`, `down1`.`NodeID` AS `NodeId_2`, `down1`.`Node` AS `T2`, `down2`.`NodeID` AS `NodeId_3`, `down2`.`Node` AS `T3`, `down3`.`NodeID` AS `NodeId_4`, `down3`.`Node` AS `T4` from (((`headstree` `root` left join `headstree` `down1` ON ((`down1`.`ParentID` = `root`.`NodeID`))) left join `headstree` `down2` ON ((`down2`.`ParentID` = `down1`.`NodeID`))) left join `headstree` `down3` ON ((`down3`.`ParentID` = `down2`.`NodeID`))) where (`root`.`ParentID` = 0) order by `root`.`Node` , `down1`.`Node` , `down2`.`Node` , `down3`.`Node`");
                balayer.GetInsertItem("CREATE OR REPLACE  VIEW `view_parent` AS select `view_tree`.`NodeId_1` AS `RootID`,`view_tree`.`BranchID` AS `BranchID`, trim(trailing '>>' from (select concat(concat(`view_tree`.`T1`, '>>'), ifnull(`view_tree`.`T2`, ''), '>>', ifnull(`view_tree`.`T3`, ''), '>>', ifnull(`view_tree`.`T4`, ''), '>>') )) AS `TREE`, cast(SPLIT_STR(trim(trailing '>' from (select concat(concat(cast(`view_tree`.`NodeId_1` as char charset utf8), '>'), ifnull(cast(`view_tree`.`NodeId_2` as char charset utf8), ''), '>', ifnull(cast(`view_tree`.`NodeId_3` as char charset utf8), ''), '>', ifnull(cast(`view_tree`.`NodeId_4` as char charset utf8), ''), '>') )), '>') as unsigned) AS `TreeID` from `view_tree`");
                
                pnlpopup.Visible = false;
                DataTable dtAllHeads = balayer.GetDataTable("select concat(T1,'>>',T2) as Node,BranchID,concat( NodeId_2 ,'|', NodeId_1) as Noderoot FROM svcf.view_tree where NodeId_2 in (13,14,15,51,52,55,58,59,60,167,172,1057,1060,1061,4739,5335,5730,1113635,1113639,1113657,1119072,44,45) group by Node union select concat(T1,'>>',T2,'>>',T3) as Node,BranchID,"+
                    "concat( NodeId_3,'|', NodeId_1) as Noderoot FROM svcf.view_tree where nodeid_2=4727 group by Node union "+
                    "select T1 as Node, BranchID, NodeId_1 as Noderoot FROM svcf.view_tree where NodeId_1 not in (1,8,3,12) "+
                    "group by Node union SELECT concat(T1,'>>',T2) as Node,BranchID,concat( NodeId_2 ,'|', NodeId_1) as "+
                    "Noderoot FROM svcf.view_tree where NodeId_1 not in (1,3,10,5,6,12) "+
                    "group by Node");
                DataTable dtmember = balayer.GetDataTable("select customername from membermaster where  BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]));
             
                ddlMaster.DataSource = dtAllHeads;
                ddlMaster.DataTextField = "Node";
                ddlMaster.DataValueField = "Noderoot";              
                ddlLoan.Visible = false;
                ddlMaster.DataBind();
                lblqty.Visible = false;
                txtqty.Visible = false;
                ListItem li = new ListItem("--Select--", "--Select--");
                ddlMaster.Items.Insert(0, li);
            }         
        }
     
        protected void btnNo_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.Url.AbsolutePath.ToString());
        }

       

        protected void btnYes_Click(object sender, EventArgs e)
        {

            if (lblHint.Text.ToString() != "Child")
            {
                Response.Redirect(Request.Url.AbsolutePath.ToString());
                return;
            }
            TransactionLayer trn = new TransactionLayer();
            try
            {

                if (ddlMaster.SelectedValue.Contains('|'))
                {
                    if (ddlMaster.SelectedValue.Split('|')[0].ToString().Trim() == "7")
                    {
                       
                        string hdnode = txtChildHead.Text + " " + Membername.Text;
                        lblHint.Text = "";
                       
                        long strCurrentID = trn.insertorupdateTrn("Insert into headstree (ParentID, Node, TreeHint) values(" + balayer.MySQLEscapeString(ddlMaster.SelectedValue.Split('|')[0].ToString().Trim()) + ",'" + hdnode + "',null)");
                        long strCurrentIDD = trn.insertorupdateTrn("Insert into headstree (ParentID, Node, TreeHint) values(" + balayer.MySQLEscapeString(ddlMaster.SelectedValue.Split('|')[0].ToString().Trim()) + ",'" + hdnode + "',null)");
                        long chithd1 = trn.insertorupdateTrn("Insert into chitheads(ChitName,MemberName,BranchID,ParentID,HeadId) values('" + balayer.MySQLEscapeString(txtChildHead.Text) + "','" + balayer.MySQLEscapeString(Membername.Text) + "'," + balayer.ToobjectstrEvenNull(Session["Branchid"]) + "," + balayer.MySQLEscapeString(ddlMaster.SelectedValue.Split('|')[0].ToString().Trim()) + "," + strCurrentID + ")");
                        long chithd2 = trn.insertorupdateTrn("Insert into chitheads(ChitName,MemberName,BranchID,ParentID,HeadId) values('" + balayer.MySQLEscapeString(txtChildHead.Text) + "','" + balayer.MySQLEscapeString(Membername.Text) + "'," + balayer.ToobjectstrEvenNull(Session["Branchid"]) + "," + balayer.MySQLEscapeString(ddlMaster.SelectedValue.Split('|')[0].ToString().Trim()) + "," + strCurrentIDD + ")");
                        string treeint1 = "7,51," + strCurrentID;
                        string treeint2 = "7,52," + strCurrentIDD;
                        trn.insertorupdateTrn("UPDATE headstree SET BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " , TreeHint='" + treeint1 + "',ParentID=51 ,RootID=7 WHERE NodeID=" + strCurrentID + "");
                        trn.insertorupdateTrn("UPDATE headstree SET BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " , TreeHint='" + treeint2 + "' ,ParentID=52,RootID=7 WHERE NodeID=" + strCurrentIDD + "");
                      
                        pnlpopup.Visible = true;
                        lblHeading.Text = "Status";
                        lblContent.Text = balayer.MySQLEscapeString(txtChildHead.Text) + " Inserted Successfully";
                        lblContent.ForeColor = System.Drawing.Color.Green;
                        btnYes.Text = "OK";
                        btnNo.Visible = false;
                        ModalPopupExtender1.Show();
                        txtChildHead.Text = "";
                        trn.CommitTrn();
                    }
                    else if (ddlMaster.SelectedValue.Split('|')[1].ToString().Trim() == "2")
                    {
                        lblHint.Text = "";
                     
                        string hdnode = txtChildHead.Text + " " + Membername.Text;
                        string invest = hdnode + "_" + txtqty.Text;
                     
                        long strCurrentID = trn.insertorupdateTrn("Insert into headstree (ParentID, Node, TreeHint) values(" + balayer.MySQLEscapeString(ddlMaster.SelectedValue.Split('|')[0].ToString().Trim()) + ",'" + invest + "',null)");
                        string strPreviousID = balayer.GetSingleValue("select TreeHint from headstree WHERE NodeID=" + balayer.MySQLEscapeString(ddlMaster.SelectedValue.Split('|')[0].ToString().Trim()) + "");
                        string strIdtoInsert = strPreviousID.Trim(',') + "," + strCurrentID;
                        long chithd1 = trn.insertorupdateTrn("Insert into chitheads(ChitName,MemberName,BranchID,ParentID,HeadId) values('" + balayer.MySQLEscapeString(txtChildHead.Text) + "','" + balayer.MySQLEscapeString(Membername.Text) + "'," + balayer.ToobjectstrEvenNull(Session["Branchid"]) + "," + balayer.MySQLEscapeString(ddlMaster.SelectedValue.Split('|')[0].ToString().Trim()) + "," + strCurrentID + ")");
                        trn.insertorupdateTrn("UPDATE headstree SET BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " , TreeHint='" + strIdtoInsert + "',RootID=2 WHERE NodeID=" + strCurrentID + "");
                        pnlpopup.Visible = true;
                        lblHeading.Text = "Status";
                        lblContent.Text = balayer.MySQLEscapeString(txtChildHead.Text) + " Inserted Successfully";
                        lblContent.ForeColor = System.Drawing.Color.Green;
                        btnYes.Text = "OK";
                        btnNo.Visible = false;
                        ModalPopupExtender1.Show();
                        txtChildHead.Text = "";
                        trn.CommitTrn();
                    }
                    else
                    {
                        lblHint.Text = "";
                     
                        string hdnode = txtChildHead.Text + " " + Membername.Text;
                        long strCurrentID = trn.insertorupdateTrn("Insert into headstree (ParentID, Node, TreeHint) values(" + balayer.MySQLEscapeString(ddlMaster.SelectedValue.Split('|')[0].ToString().Trim()) + ",'" + hdnode + "',null)");
                        string strPreviousID = balayer.GetSingleValue("select TreeHint from headstree WHERE NodeID=" + balayer.MySQLEscapeString(ddlMaster.SelectedValue.Split('|')[0].ToString().Trim()) + "");
                        string strIdtoInsert = strPreviousID.Trim(',') + "," + strCurrentID;
                        long chithd1 = trn.insertorupdateTrn("Insert into chitheads(ChitName,MemberName,BranchID,ParentID,HeadId) values('" + balayer.MySQLEscapeString(txtChildHead.Text) + "','" + balayer.MySQLEscapeString(Membername.Text) + "'," + balayer.ToobjectstrEvenNull(Session["Branchid"]) + "," + balayer.MySQLEscapeString(ddlMaster.SelectedValue.Split('|')[0].ToString().Trim()) + "," + strCurrentID + ")");
                        trn.insertorupdateTrn("UPDATE headstree SET BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " , TreeHint='" + strIdtoInsert + "' WHERE NodeID=" + strCurrentID + "");
                        pnlpopup.Visible = true;
                        lblHeading.Text = "Status";
                        lblContent.Text = balayer.MySQLEscapeString(txtChildHead.Text) + " Inserted Successfully";
                        lblContent.ForeColor = System.Drawing.Color.Green;
                        btnYes.Text = "OK";
                        btnNo.Visible = false;
                        ModalPopupExtender1.Show();
                        txtChildHead.Text = "";
                        trn.CommitTrn();
                    }
                }
                else
                {
                    lblHint.Text = "";
                   
                    string hdnode = txtChildHead.Text + " " + Membername.Text;
                    long strCurrentID = trn.insertorupdateTrn("Insert into headstree (ParentID, Node, TreeHint) values(" + balayer.MySQLEscapeString(ddlMaster.SelectedValue.ToString().Trim()) + ",'" + hdnode + "',null)");
                    string strPreviousID = balayer.GetSingleValue("select TreeHint from headstree WHERE NodeID=" + balayer.MySQLEscapeString(ddlMaster.SelectedValue.ToString().Trim()) + "");
                    string strIdtoInsert = strPreviousID.Trim(',') + "," + strCurrentID;
                    long chithd1 = trn.insertorupdateTrn("Insert into chitheads(ChitName,MemberName,BranchID,ParentID,HeadId) values('" + balayer.MySQLEscapeString(txtChildHead.Text) + "','" + balayer.MySQLEscapeString(Membername.Text) + "'," + balayer.ToobjectstrEvenNull(Session["Branchid"]) + "," + balayer.MySQLEscapeString(ddlMaster.SelectedValue.Split('|')[0].ToString().Trim()) + "," + strCurrentID + ")");
                    trn.insertorupdateTrn("UPDATE headstree SET BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " , TreeHint='" + strIdtoInsert + "' WHERE NodeID=" + strCurrentID + "");
                    pnlpopup.Visible = true;
                    lblHeading.Text = "Status";
                    lblContent.Text = balayer.MySQLEscapeString(txtChildHead.Text) + " Inserted Successfully";
                    lblContent.ForeColor = System.Drawing.Color.Green;
                    btnYes.Text = "OK";
                    btnNo.Visible = false;
                    ModalPopupExtender1.Show();
                    txtChildHead.Text = "";
                    trn.CommitTrn();
                }
            }
            catch (Exception error)
            {
                try
                {
                    trn.RollbackTrn();
                }
                catch { }
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

        protected void branchsubofficeInsert_OnClick(object sender, EventArgs e)
        {
            string officename = offinme.Text;
            string purpose = Purposeid.Text;
            string resadd = resaddid.Text;
            string officeadd = offaddid.Text;

            string BranchSubInsertion = "insert into svcf.branchsuboffice (branchid,officename,purpose,residentialaddress,officeaddress) values ('" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + "','" + officename + "','" + purpose + "','" + resadd + "','" + officeadd + "')";
            try
            {
                trn.insertorupdateTrn(BranchSubInsertion);
                pnlpopup.Visible = true;
                lblHeading.Text = "Status";
                lblContent.Text = "Branchsuboffice Inserted Successfully";
                lblContent.ForeColor = System.Drawing.Color.Green;
                btnYes.Text = "OK";
                btnNo.Visible = false;
                ModalPopupExtender1.Show();
                trn.CommitTrn();

            }
            catch (Exception ex)
            {
                pnlpopup.Visible = true;
                lblHeading.Text = "Status";
                lblContent.Text = "Insert Problem";
                lblContent.ForeColor = System.Drawing.Color.Red;
                btnYes.Text = "OK";
                btnNo.Visible = false;
                ModalPopupExtender1.Show();
                trn.CommitTrn();
            }
        }

        void fillstaff()
        {
            BusinessLayer blayer = new BusinessLayer();

            DataTable dt = new DataTable();

            TL = blayer.RetrveList("SELECT Emp_Name FROM svcf.employee_details where BranchID=" + blayer.ToobjectstrEvenNull(HttpContext.Current.Session["Branchid"]) + "");

            ddlLoan.DataSource = TL;
            ddlLoan.DataBind();
            ddlLoan.Items.Insert(0, new ListItem("--Select--"));
        }



        protected void btnChildInsert_OnClick(object sender, EventArgs e)
        {
            Page.Validate("Child");
            if (!Page.IsValid)
            {
                return;
            }

            string chkstrPreviousID = balayer.GetSingleValue("select TreeHint from headstree WHERE NodeID=" + ddlMaster.SelectedValue.Split('|')[0].ToString().Trim() + "");
            lblContent.Text = "Please Verify Your Heads Order is correct???";
            lblHeading.Text = "Confirmation";
            DataTable dtLst = balayer.GetDataTable("select Node from headstree where NodeID in (" + chkstrPreviousID + ")");
            BulletedList btl = new BulletedList();
            for (int il = 0; il < dtLst.Rows.Count; il++)
            {
                btl.Items.Add(dtLst.Rows[il][0].ToString());
            }
            btl.Items.Add(balayer.MySQLEscapeString(txtChildHead.Text));
            DynamicControlsHolder.Controls.Add(btl);
            pnlpopup.Visible = true;
            lblHint.Text = "Child";
            btnYes.Text = "OK";
            btnNo.Visible = true;
            btnNo.Text = "CANCEL";
            ModalPopupExtender1.Show();
        }

        protected void ddlMaster_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlMaster.SelectedValue.Contains('|'))
            {
                if (ddlMaster.SelectedValue.Split('|')[1].ToString().Trim() == "2")
                {
                    lblqty.Visible = true;
                    txtqty.Visible = true;
                    ddlLoan.Visible = false;
                }
                else
                {
                    lblqty.Visible = false;
                    txtqty.Visible = false;
                    ddlLoan.Visible = false;
                }
            }
            else if (ddlMaster.SelectedValue != "2")
            {
                lblqty.Visible = false;
                txtqty.Visible = false;
                ddlLoan.Visible = false;
            }
            if(ddlMaster.SelectedValue.Split('|')[0].ToString().Trim() == "55")
            {
                ddlLoan.Visible = true;
                fillstaff();
            }
        }

    }
   
}
     