using System;
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
using System.Data;
using System.Security.Cryptography;
using System.Text;
using System.IO;
using DevOne.Security.Cryptography.BCrypt;

namespace SreeVisalamChitFundLtd_phase1
{
    public partial class ChangeAccount : System.Web.UI.Page
    {
        #region Object
        BusinessLayer balayer = new BusinessLayer();
        string query = "";
        #endregion

        ILog logger = log4net.LogManager.GetLogger(typeof(ChangeAccount));

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
                if (usrRole == "Report")
                {
                    Response.Redirect("Home.aspx", false);
                }
                else
                {
                    Session["CheckRefresh"] = System.Guid.NewGuid().ToString();
                    GetBranch();
                }

            }
            if (Convert.ToInt32(Session["Branchid"]) != 161)
            {
                Response.Redirect(Page.ResolveUrl("~/Home.aspx"), false);
            }
            //TxtUserName.Text = balayer.ToobjectstrEvenNull(Session["UserName"]);
            //TxtUserName.ReadOnly = true;
            //TxtEmailID.Text = balayer.GetSingleValue("select EmailID From login where BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and UserName='" + balayer.MySQLEscapeString(balayer.ToobjectstrEvenNull(Session["UserName"])) + "'");
        }

        private void GetBranch()
        {
            ddlBranchs.DataSource = null;
            ddlBranchs.DataBind();
            DataTable dt = balayer.GetDataTable("select B_Name,Head_Id from branchdetails order by B_Name asc");
            DataRow drow;
            drow = dt.NewRow();

            ddlBranchs.DataSource = dt;
            ddlBranchs.DataTextField = "B_Name";
            ddlBranchs.DataValueField = "Head_Id";
            ddlBranchs.DataBind();
            dt.Dispose();
        }

        private string Encrypt(string clearText)
        {
            string EncryptionKey = "MAKV2SPBNI99212";
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }


        protected void Page_PreRender(object sender, EventArgs e)
        {
            ViewState["CheckRefresh"] = Session["CheckRefresh"];
        }
        protected void btnCreate_Click(object sender, EventArgs e)
        {

            Page.Validate("aaa");
            if (!Page.IsValid)
            {
                return;
            }
            if (Session["CheckRefresh"].ToString() != ViewState["CheckRefresh"].ToString())
            {
                return;
            }
            string encrypwd = "";
            string spwrd = "";
            string hpwrd = "";
            string ipaddress;
            string narr = "";
            TransactionLayer trn = new TransactionLayer();
            try
            {
                
                if ((balayer.ExecuteReader("select * from login where BranchID=" + ddlBranchs.SelectedValue + " and UserName='" + balayer.MySQLEscapeString(balayer.ReplaceJnk(TxtUserName.Text)) + "'and Password='" + balayer.MySQLEscapeString(TxtOldPassword.Text) + "'")).HasRows)
                {
                   
                    DataTable Getpwd = balayer.GetDataTable("Select * from login where UserName = '" + balayer.MySQLEscapeString(balayer.ReplaceJnk(TxtUserName.Text)) + "' And  Password = '" + balayer.MySQLEscapeString(TxtOldPassword.Text) + "' and BranchID=" + ddlBranchs.SelectedValue + "");

                      #region Adding row into Password change history 
		 
                       ipaddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                    if (ipaddress == "" || ipaddress == null)
                        ipaddress = Request.ServerVariables["REMOTE_ADDR"];
                    string hostname = Request.UserHostName;
                    hostname = hostname + ":" + ipaddress;

                    //query = "insert into svcf.PasswordChangeHistory(Sl_No, BranchId, Username, OldPassword, NewPassword, ChangedDate, LoginIP) values(" + Getpwd.Rows[0]["Sl_No"] + "," + balayer.ToobjectstrEvenNull(Session["Branchid"]) + ",'" + balayer.MySQLEscapeString(balayer.ReplaceJnk(TxtUserName.Text)) + "'," +
                    //      "'" + TxtOldPassword.Text + "','" + TxtNewPassword.Text.Trim() + "','" + balayer.GetChangeDatFormat(DateTime.Now, 2) + "','" + hostname + "')";
                    query = "insert into svcf.PasswordChangeHistory(Sl_No, BranchId, Username, OldPassword, NewPassword, ChangedDate, LoginIP) values(" + Getpwd.Rows[0]["Sl_No"] + "," + ddlBranchs.SelectedValue + ",'" + balayer.MySQLEscapeString(balayer.ReplaceJnk(TxtUserName.Text)) + "'," +
                          "'" + TxtOldPassword.Text + "','" + TxtNewPassword.Text.Trim() + "','" + balayer.GetChangeDatFormat(DateTime.Now, 2) + "','" + hostname + "')";
                    balayer.GetInsertItem(query);

	                #endregion                 
                 

                    encrypwd = Encrypt(TxtNewPassword.Text.Trim());
                    spwrd = BCryptHelper.GenerateSalt();
                    hpwrd = BCryptHelper.HashPassword(TxtNewPassword.Text.Trim(), spwrd);
                    long nResult = trn.insertorupdateTrn("Update login set Encryptpwd='" + encrypwd + "',Password='" + TxtNewPassword.Text.Trim() + "',Salt= '" + spwrd + "',HashPassword='" + hpwrd + "' where UserName='" + balayer.MySQLEscapeString(balayer.ReplaceJnk(TxtUserName.Text)) + "' and  BranchID=" + ddlBranchs.SelectedValue + " and Sl_No=" + Getpwd.Rows[0]["Sl_No"] + "");
                    Pnlmsg.Visible = true;
                    this.ModalPopupExtender1.Show();
                    lblHeading.Text = "Status";
                    lblContent.Text = TxtUserName.Text + " Updated Successfully";
                    lblContent.ForeColor = System.Drawing.Color.Green;
                }
                else
                {
                    Pnlmsg.Visible = true;
                    this.ModalPopupExtender1.Show();
                    lblHeading.Text = "Status";
                    lblContent.Text = "Invalid User Name or Password";
                    lblContent.ForeColor = System.Drawing.Color.Green;
                }
                trn.CommitTrn();
                logger.Info("CDecree.aspx - btnCreate_Click() - Completed: " + DateTime.Now + " by: " + Convert.ToString(Session["UserName"]) + "");
            }
            catch (Exception error)
            {
                try
                {
                    trn.RollbackTrn();
                    logger.Info("CDecree.aspx - btnCreate_Click() - Error: " + error.Message + ": " + DateTime.Now + " by: " + Convert.ToString(Session["UserName"]) + "");
                }
                catch { }
                finally
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Warning", "alert('" + error.Message.ToString().Replace("'", "\\'") + "');", true);
                }
            }
            finally {

                trn.DisposeTrn();


            }
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.Url.AbsolutePath.ToString());
        }

        protected void ddUserType_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable getDt = new DataTable();
            try
            {
                if (ddUserType.Text.Trim() == "User")
                {
                    query = "select * from svcf.rights as rights join svcf.login as lg on lg.Sl_No=rights.memberid where lg.BranchID=" + ddlBranchs.SelectedValue + " and rights.roleid=5;";
                    getDt = balayer.GetDataTable(query);
                    TxtUserName.Text = Convert.ToString(getDt.Rows[0]["UserName"]);
                   
                }
                else if (ddUserType.Text.Trim() == "Admin")
                {
                    query = "select lg.UserName from svcf.rights as rights join svcf.login as lg on lg.Sl_No=rights.memberid where lg.BranchID=" + ddlBranchs.SelectedValue + " and rights.roleid=2;";
                    getDt = balayer.GetDataTable(query);
                    TxtUserName.Text = Convert.ToString(getDt.Rows[0]["UserName"]);
                  
                }
                else if(ddUserType.Text.Trim()=="Report")
                {
                    query = "select lg.UserName from svcf.rights as rights join svcf.login as lg on lg.Sl_No=rights.memberid where lg.BranchID=" + ddlBranchs.SelectedValue + " and rights.roleid=6;";
                    getDt = balayer.GetDataTable(query);
                    TxtUserName.Text = Convert.ToString(getDt.Rows[0]["UserName"]);
                }
            }
            catch(Exception )
            {

            }
        }
    }
}
