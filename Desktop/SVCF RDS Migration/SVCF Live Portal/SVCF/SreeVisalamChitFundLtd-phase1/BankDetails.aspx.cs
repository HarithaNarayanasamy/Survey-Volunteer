using System;
using System.Data;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using SVCF_BusinessAccessLayer;
using SVCF_TransactionLayer;
using SVCF_DataAccessLayer;
using log4net;
using log4net.Config;

namespace SreeVisalamChitFundLtd_phase1.Admin
{
    public partial class BankDetails : System.Web.UI.Page
    {
        #region Object
        BusinessLayer balayer = new BusinessLayer();
        TransactionLayer trn = new TransactionLayer();
        #endregion

        ILog logger = log4net.LogManager.GetLogger(typeof(BankDetails));

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
                txtDoAccount.ToolTip = "Ex. " + DateTime.Now.ToString("dd/MM/yyyy") + " (dd/mm/yyyy)";
                Session["CheckRefresh"] = System.Guid.NewGuid().ToString();
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
        protected void btnAdd_Click(object sender, EventArgs e)
        {

            Page.Validate("bankgroup");
            if (!Page.IsValid)
            {
                return;
            }
            if (Session["CheckRefresh"].ToString() != ViewState["CheckRefresh"].ToString())
            {
                return;
            }
           
            try
            {
                int HasRow = Convert.ToInt32(balayer.GetSingleValue("SELECT count(*) FROM bankdetails where BankName='" + balayer.MySQLEscapeString(txtBankName.Text) + "' and AccountNo='" + balayer.MySQLEscapeString(txtAccountNo.Text) + "'"));
                if (HasRow > 0)
                {
                    ClientScript.RegisterStartupScript(this.Page.GetType(), "Validation", "<script Language='Javascript'>alert('Account Number Already Exist!!!!')</script>");
                    txtAccountNo.Text = "";
                    txtAccountNo.Focus();
                }
                else
                {
                    int parentID = 0;
                    string strPreviousID = "";
                    string strIdtoInsert = "";
                    if (balayer.MySQLEscapeString(balayer.ToobjectstrEvenNull(ddlTypeBank.SelectedValue)) == "Scheduled Banks")
                    {
                        parentID = int.Parse(balayer.GetSingleValue("select NodeID from headstree Where Node ='Scheduled Banks'"));
                        strPreviousID = balayer.GetSingleValue("select TreeHint from headstree WHERE NodeID=" + parentID + "");
                    }
                    else
                        if (balayer.MySQLEscapeString(balayer.ToobjectstrEvenNull(ddlTypeBank.SelectedValue)) == "Non Scheduled Banks")
                        {
                            parentID = int.Parse(balayer.GetSingleValue("select NodeID from headstree Where Node ='Non Scheduled Banks'"));
                            strPreviousID = balayer.GetSingleValue("select TreeHint from headstree WHERE NodeID=" + parentID + "");
                        }
                        else
                            if (balayer.MySQLEscapeString(balayer.ToobjectstrEvenNull(ddlTypeBank.SelectedValue)) == "Fixed deposits with Banks")
                            {
                                parentID = int.Parse(balayer.GetSingleValue("select NodeID from headstree Where Node ='Fixed deposits with Banks'"));
                                strPreviousID = balayer.GetSingleValue("select TreeHint from headstree WHERE NodeID=" + parentID + "");
                            }
                    long strCurrentID = trn.insertorupdateTrn("insert into headstree(ParentID, Node, TreeHint,BranchID) values(" + parentID + ",'" + balayer.MySQLEscapeString(txtbanklocation.Text) + " _ " + balayer.MySQLEscapeString(txtBankName.Text) + " _ " + balayer.MySQLEscapeString(txtIFCcode.Text) + " _ " + balayer.MySQLEscapeString(txtAccountNo.Text) + "','Null'," + Session["Branchid"] + ")");
                    strIdtoInsert = strPreviousID.Trim(',') + "," + strCurrentID;
                    trn.insertorupdateTrn("UPDATE headstree SET TreeHint='" + strIdtoInsert + "' WHERE NodeID=" + strCurrentID + "");
                    trn.insertorupdateTrn("insert into bankdetails (Head_Id,BankName,IFCCode,AccountNo,Address,DateofAccount,TypeofBank,BranchID,BankLocation) values(" + strCurrentID + ",'" + balayer.MySQLEscapeString(txtBankName.Text) + "','" + balayer.MySQLEscapeString(txtIFCcode.Text) + "','" + balayer.MySQLEscapeString(txtAccountNo.Text) + "','" + balayer.MySQLEscapeString(txtAddress.Text) + "','" + balayer.indiandateToMysqlDate(txtDoAccount.Text) + "','" + ddlTypeBank.SelectedItem.Text + "'," + balayer.ToobjectstrEvenNull(Session["Branchid"]) + ",'" + balayer.MySQLEscapeString(txtbanklocation.Text) + "')");
                    ModalPopupExtender1.PopupControlID = "Pnlmsg";
                    this.ModalPopupExtender1.Show();
                    Pnlmsg.Visible = true;
                    lblHeading.Text = "Status";
                    lblContent.Text = Session["BranchName"] + "_" + balayer.MySQLEscapeString(txtBankName.Text) + "_" + balayer.MySQLEscapeString(txtIFCcode.Text) + "_" + balayer.MySQLEscapeString(txtAccountNo.Text) + " Inserted Successfully";
                    lblContent.ForeColor = System.Drawing.Color.Green;
                }
                trn.CommitTrn();
                logger.Info("BankDetails.aspx - btnAdd_Click() - Completed: " + DateTime.Now + " by: " + Convert.ToString(Session["UserName"]) + "");
            }
            catch (Exception Error)
            {
                try
                {
                    trn.RollbackTrn();
                    logger.Info("BankDetails.aspx - btnAdd_Click() - Error: " + Error.Message + ": " + DateTime.Now + " by: " + Convert.ToString(Session["UserName"]) + "");
                }
                catch
                { }
                finally
                {
                    ClientScript.RegisterStartupScript(this.Page.GetType(), "Validation", "<script Language='Javascript'>alert('" + Error.Message.ToString().Replace("'", "\\'") + "')</script>");
                }
            }
            finally
            {
                trn.DisposeTrn();
            }
        }
        protected void btn_ok(object sender, EventArgs e)
        {
            Response.Redirect(Request.Url.AbsolutePath.ToString());
        }
    }
}

