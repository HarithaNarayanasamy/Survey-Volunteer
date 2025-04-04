using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.Data;
using SVCF_BusinessAccessLayer;
using SVCF_TransactionLayer;
using SVCF_DataAccessLayer;
using log4net;
using log4net.Config;
namespace SreeVisalamChitFundLtd_phase1
{
    public partial class CreateAccount : System.Web.UI.Page
    {
        #region Object
        BusinessLayer balayer = new BusinessLayer();
        #endregion

        ILog logger = log4net.LogManager.GetLogger(typeof(CreateAccount));
         
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

                DataTable dtBranch = balayer.GetDataTable("Select Head_Id,B_Name from branchdetails");
                DataRow dr;
                dr = dtBranch.NewRow();
                dr[0] = "0";
                dr[1] = "--Select--";
                //dtBranch.Rows.Add(dr);//this will add the row at the end of the datatable
                //OR
                int yourPosition = 0;

                DDlBranchName.DataSource = dtBranch;
                DDlBranchName.DataValueField = "Head_Id";
                DDlBranchName.DataTextField = "B_Name";
                DDlBranchName.DataBind();
                //ListItem li = new ListItem("--Select--","--Select--");
                //DDlBranchName.Items.Insert(0,li);
                Session["CheckRefresh"] = System.Guid.NewGuid().ToString();
                dtBranch.Rows.InsertAt(dr, yourPosition);
            }
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            ViewState["CheckRefresh"] = Session["CheckRefresh"];
        }
        protected void chkRoles_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            txtCustomer.Text = "";
            for (int i = 0; i < chkRoles.Items.Count; i++)
            {
                if (chkRoles.Items[i].Selected == true)
                {
                    txtCustomer.Text += chkRoles.Items[i].Text.ToString() + ";";
                }
            }
            if (txtCustomer.Text == "")
            {
                txtCustomer.Text = "Select Roles;";
            }
            txtCustomer.Text = txtCustomer.Text.TrimEnd(';');
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
            string DualTransactionKey = "";
            TransactionLayer trn = new TransactionLayer();
            try
            {
                System.Guid guid = Guid.NewGuid();
                string guidForChar36 = guid.ToString();
                string hexstring = BitConverter.ToString(guid.ToByteArray());
                string guidForBinary16 = "0x" + hexstring.Replace("-", string.Empty);
                DualTransactionKey = guidForBinary16;
                Pnlmsg.Visible = false;
                Pnlmsg.Visible = false;
                if (!( balayer.ExecuteReader("Select * from login where BranchID=" + balayer.ReplaceJnk(DDlBranchName.SelectedValue) + " and UserName='" + balayer.MySQLEscapeString(TxtUserName.Text) + "'")).HasRows)
                {
                    long id = trn.insertorupdateTrn("insert into login(DualTransactionKey,BranchID,EmailID,UserName,Password)values(" + DualTransactionKey + "," + balayer.ReplaceJnk(DDlBranchName.SelectedValue) + ",'" + balayer.MySQLEscapeString(balayer.ReplaceJnk(TxtEmailID.Text)) + "','" + balayer.MySQLEscapeString(balayer.ReplaceJnk(TxtUserName.Text)) + "','" + balayer.MySQLEscapeString(balayer.ReplaceJnk(TxtPassword.Text)) + "')");
                    string[] sss = txtCustomer.Text.Split(';');
                    for (int i = 0; i < sss.Length; i++)
                    {
                        long ssssss;
                        string ssss = txtCustomer.Text.Split(';')[i];
                        if (ssss == "Manager")
                        {
                            ssssss = trn.insertorupdateTrn("insert into `rights` (`DualTransactionKey`,memberid,roleid) values (" + DualTransactionKey + "," + id + ",1)");
                        }
                        else if (ssss == "Administrator")
                        {
                            ssssss = trn.insertorupdateTrn("insert into `rights` (`DualTransactionKey`,memberid,roleid) values (" + DualTransactionKey + "," + id + ",2)");
                        }
                        else if (ssss == "cashier")
                        {
                            ssssss = trn.insertorupdateTrn("insert into `rights` (`DualTransactionKey`,memberid,roleid) values (" + DualTransactionKey + "," + id + ",3)");
                        }
                        else if (ssss == "head")
                        {
                            ssssss = trn.insertorupdateTrn("insert into `rights` (`DualTransactionKey`,memberid,roleid) values (" + DualTransactionKey + "," + id + ",4)");
                        }
                        else if (ssss == "User")
                        {
                            ssssss = trn.insertorupdateTrn("insert into `rights` (`DualTransactionKey`,memberid,roleid) values (" + DualTransactionKey + "," + id + ",5)");
                        }
                        else if (ssss == "Report")
                        {
                            ssssss = trn.insertorupdateTrn("insert into `rights` (`DualTransactionKey`,memberid,roleid) values (" + DualTransactionKey + "," + id + ",6)");
                        }
                    }
                    ModalPopupExtender2.PopupControlID = "Pnlmsg";
                    ModalPopupExtender2.Show();
                    Pnlmsg.Visible = true;
                    lblTs.Text = "Status";
                    lblContent.Text = "Account Created Successfully!!!";
                    lblContent.ForeColor = System.Drawing.Color.Green;
                }
                else
                {
                    string ss = TxtUserName.Text;
                    TxtUserName.Text = "";
                    TxtPassword.Text = "";
                    ScriptManager.RegisterStartupScript(this, GetType(), "Warning", "alert('" + ss.ToString() + " Already Exists!!!');", true);
                }
                trn.CommitTrn();
                logger.Info("CreateAccount.aspx - btnCreate_Click() - Completed: " + DateTime.Now + " by: " + Convert.ToString(Session["UserName"]) + "");
            }
            catch (Exception error)
            {
                try
                {
                   trn.RollbackTrn();
                   logger.Info("CreateAccount.aspx - btnCreate_Click() - Error: " + error.Message + ": " + DateTime.Now + " by: " + Convert.ToString(Session["UserName"]) + "");
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
    }
}
