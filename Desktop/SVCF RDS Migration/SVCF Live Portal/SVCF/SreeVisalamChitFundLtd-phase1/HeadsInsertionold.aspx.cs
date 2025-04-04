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
namespace SreeVisalamChitFundLtd_phase1
{
    public partial class HeadsInsertion : System.Web.UI.Page
    {
        #region Object
        BusinessLayer balayer = new BusinessLayer();
        TransactionLayer trn = new TransactionLayer();
        #endregion
        string rootid = "";
        string value = "";
        int dataval;
      //  string[] dataval = new string[0];

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
                balayer.GetInsertItem("CREATE OR REPLACE VIEW `view_tree` AS select `root`.`NodeID` AS `NodeId_1`,(case when (`down3`.`Branchid` is null) then `down2`.`Branchid` else `down3`.`Branchid` end)  AS `BranchID`, `root`.`Node` AS `T1`, `down1`.`NodeID` AS `NodeId_2`, `down1`.`Node` AS `T2`, `down2`.`NodeID` AS `NodeId_3`, `down2`.`Node` AS `T3`, `down3`.`NodeID` AS `NodeId_4`, `down3`.`Node` AS `T4` from (((`headstree` `root` left join `headstree` `down1` ON ((`down1`.`ParentID` = `root`.`NodeID`))) left join `headstree` `down2` ON ((`down2`.`ParentID` = `down1`.`NodeID`))) left join `headstree` `down3` ON ((`down3`.`ParentID` = `down2`.`NodeID`))) where (`root`.`ParentID` = 0) order by `root`.`Node` , `down1`.`Node` , `down2`.`Node` , `down3`.`Node`");
                balayer.GetInsertItem("CREATE OR REPLACE  VIEW `view_parent` AS select `view_tree`.`NodeId_1` AS `RootID`,`view_tree`.`BranchID` AS `BranchID`, trim(trailing '>>' from (select concat(concat(`view_tree`.`T1`, '>>'), ifnull(`view_tree`.`T2`, ''), '>>', ifnull(`view_tree`.`T3`, ''), '>>', ifnull(`view_tree`.`T4`, ''), '>>') )) AS `TREE`, cast(SPLIT_STR(trim(trailing '>' from (select concat(concat(cast(`view_tree`.`NodeId_1` as char charset utf8), '>'), ifnull(cast(`view_tree`.`NodeId_2` as char charset utf8), ''), '>', ifnull(cast(`view_tree`.`NodeId_3` as char charset utf8), ''), '>', ifnull(cast(`view_tree`.`NodeId_4` as char charset utf8), ''), '>') )), '>') as unsigned) AS `TreeID` from `view_tree`");
                //select();
                pnlpopup.Visible = false;
                DataTable dtAllHeads = balayer.GetDataTable("SELECT concat(T1,'>>',T2) as Node,BranchID,concat( NodeId_2 ,'|', NodeId_1) as Noderoot FROM svcf.view_tree where NodeId_1 not in (1,3,10,5,6,12) and BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " group by Node");
                //DataRow drAllHeads = dtAllHeads.NewRow();
                ddlMaster.DataSource = dtAllHeads;
                ddlMaster.DataTextField = "Node";
                ddlMaster.DataValueField = "Noderoot";
               //value = ddlMaster.DataValueField;
               // dataval = value.Split('|');
               // dataval = int.Parse(value.Split('|')[0].ToString());
                ddlMaster.DataBind();
                lblqty.Visible = false;
                txtqty.Visible = false;
                ListItem li = new ListItem("--Select--", "--Select--");
                ddlMaster.Items.Insert(0, li);
                //grid.DataBind();
                //txtMasterHead.Focus();
                txtChildHead.Focus();
                //foreach (GridViewColumn column in grid.Columns)
                //{
                //    if (column is GridViewDataColumn)
                //    {
                //        ((GridViewDataColumn)column).Settings.AutoFilterCondition = AutoFilterCondition.Contains;
                //    }
                //}
                //Session["CheckRefresh"] = System.Guid.NewGuid().ToString();
            }
            else
            {
                //select();
            }
        }
        protected void Page_PreRender(object sender, EventArgs e)
        {
            ViewState["CheckRefresh"] = Session["CheckRefresh"];
        }
        //protected void select()
        //{
        //    AccessDataSource1.SelectCommand = "SELECT * FROM svcf.view_parent";
        //}
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


                if (ddlMaster.SelectedValue.Split('|')[0].ToString().Trim() == "7")
                {
                    lblHint.Text = "";
                    long strCurrentID = trn.insertorupdateTrn("Insert into headstree (ParentID, Node, TreeHint) values(" + balayer.MySQLEscapeString(ddlMaster.SelectedValue.Split('|')[0].ToString().Trim()) + ",'" + balayer.MySQLEscapeString(txtChildHead.Text) + "',null)");
                    long strCurrentIDD = trn.insertorupdateTrn("Insert into headstree (ParentID, Node, TreeHint) values(" + balayer.MySQLEscapeString(ddlMaster.SelectedValue.Split('|')[0].ToString().Trim()) + ",'" + balayer.MySQLEscapeString(txtChildHead.Text) + "',null)");
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
                    string invest = txtChildHead.Text + "_" + txtqty.Text;
                    long strCurrentID = trn.insertorupdateTrn("Insert into headstree (ParentID, Node, TreeHint) values(" + balayer.MySQLEscapeString(ddlMaster.SelectedValue.Split('|')[0].ToString().Trim()) + ",'" + invest + "',null)");
                    string strPreviousID = balayer.GetSingleValue("select TreeHint from headstree WHERE NodeID=" + balayer.MySQLEscapeString(ddlMaster.SelectedValue.Split('|')[0].ToString().Trim()) + "");
                    string strIdtoInsert = strPreviousID.Trim(',') + "," + strCurrentID;
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
                    long strCurrentID = trn.insertorupdateTrn("Insert into headstree (ParentID, Node, TreeHint) values(" + balayer.MySQLEscapeString(ddlMaster.SelectedValue.Split('|')[0].ToString().Trim()) + ",'" + balayer.MySQLEscapeString(txtChildHead.Text) + "',null)");
                    string strPreviousID = balayer.GetSingleValue("select TreeHint from headstree WHERE NodeID=" + balayer.MySQLEscapeString(ddlMaster.SelectedValue.Split('|')[0].ToString().Trim()) + "");
                    string strIdtoInsert = strPreviousID.Trim(',') + "," + strCurrentID;
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
        //protected void btnMaterInsert_OnClick(object sender, EventArgs e)
        //{

        //    Page.Validate("Root");
        //    if (!Page.IsValid)
        //    {
        //        return;
        //    }
        //    if (Session["CheckRefresh"].ToString() != ViewState["CheckRefresh"].ToString())
        //    {
        //        return;
        //    }
        //    TransactionLayer trn = new TransactionLayer();
        //    try
        //    {
        //        int iExist = int.Parse(balayer.GetSingleValue("select Count(*) from headstree Where Node ='" + balayer.MySQLEscapeString(txtMasterHead.Text) + "'"));
        //        if (iExist == 0)
        //        {
        //            long iResult = trn.insertorupdateTrn("Insert into headstree (ParentID, Node, TreeHint) values(0,'" + balayer.MySQLEscapeString(txtMasterHead.Text) + "',null)");
        //            trn.insertorupdateTrn("UPDATE headstree SET TreeHint='" + iResult + "' WHERE NodeID=" + iResult + "");
        //            pnlpopup.Visible = true;
        //            lblHeading.Text = "Status";
        //            btnYes.Text = "OK";
        //            btnNo.Visible = false;
        //            lblContent.Text = "Master Head - " + balayer.MySQLEscapeString(txtMasterHead.Text) + " Inserted Successfully";
        //            lblContent.ForeColor = System.Drawing.Color.Green;
        //            ModalPopupExtender1.Show();
        //            txtMasterHead.Text = "";
        //        }
        //        else
        //        {
        //            pnlpopup.Visible = true;
        //            lblHeading.Text = "Status";
        //            lblContent.Text = txtMasterHead.Text + "  Already Exist!!!";
        //            lblContent.ForeColor = System.Drawing.Color.Green;
        //            btnYes.Visible = false;
        //            btnNo.Visible = true;
        //            btnNo.Text = "Ok";
        //            ModalPopupExtender1.Show();
        //        }
        //        trn.CommitTrn();
        //    }
        //    catch (Exception error)
        //    {
        //        try
        //        {
        //            trn.RollbackTrn();
        //        }
        //        catch { }
        //        finally
        //        {
        //            ScriptManager.RegisterStartupScript(this, GetType(), "Warning", "alert('" + error.Message.ToString().Replace("'", "\\'") + "');", true);
        //        }
        //    }
        //    finally
        //    {
        //        trn.DisposeTrn();
        //    }
        //}
        protected void btnChildInsert_OnClick(object sender, EventArgs e)
        {
            Page.Validate("Child");
            if (!Page.IsValid)
            {
                return;
            }
            //if (Session["CheckRefresh"].ToString() != ViewState["CheckRefresh"].ToString())
            //{
            //    return;
            //}
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

            if (ddlMaster.SelectedValue.Split('|')[1].ToString().Trim() == "2")
            {
                lblqty.Visible = true;
                txtqty.Visible = true;
            }
        }
    }
}
     