using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail;
using MySql.Data.MySqlClient;
using System.Security;
using System.Web.Security;
using SVCF_BusinessAccessLayer;
using SVCF_TransactionLayer;
using SVCF_DataAccessLayer;
using System.Security.Cryptography;
using System.Text;
using System.Configuration;
using System.IO;

using DevOne.Security.Cryptography.BCrypt;


namespace SreeVisalamChitFundLtd_phase1
{
    public partial class Login : System.Web.UI.Page
    {
        #region VarDeclaration
        BusinessLayer balayer = new BusinessLayer();
        TransactionLayer tranlayer = new TransactionLayer();
        EntityBusinessAccess EDA = new EntityBusinessAccess();

        string cookiestr = "";
        string userid = "";
        string strRedirect = "";      
        string query = "";
        bool result;

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    //  UpdateEncrypedPwrd();
                    // UpdateEmpDesignation();
                   // EDA.GetAllBranches();

                    if (Request.QueryString["upname"] != null)
                    {
                        SendPassword(Server.HtmlDecode(Request.QueryString["upname"].ToString()));
                    }
                    else
                    {
                        balayer.Disposeconnection();
                        txtUser.Focus();
                        var dt = new DataTable();
                        dt = balayer.GetDataTable("select B_Name,Head_Id from branchdetails order by B_Name asc");
                        ddlBranch.DataSource = dt;
                        ddlBranch.DataTextField = "B_Name";
                        ddlBranch.DataValueField = "Head_Id";
                        ddlBranch.DataBind();
                        dt.Dispose();
                    }
                }
            }
            catch (Exception err)
            {
                balayer.Disposeconnection();
            }
        }

        public string GetUniqueKey(int maxSize)
        {
            char[] chars = new char[62];
            chars = "123456789".ToCharArray();
            byte[] data = new byte[1];
            RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider();
            crypto.GetNonZeroBytes(data);
            data = new byte[maxSize];
            crypto.GetNonZeroBytes(data);
            StringBuilder result = new StringBuilder(maxSize);
            foreach (byte b in data)
            {
                result.Append(chars[b % (chars.Length)]);
            }
            return result.ToString();
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



        public void UpdateEmpDesignation()
        {
            try
            {               
                DataTable jj = balayer.GetDataTable("SELECT * FROM svcf.employee_details as ed join employeedesignation as edw on(ed.Emp_Designation=edw.Designationname)");
                for (int i = 0; i < jj.Rows.Count; i++)
                {
                    long insertcmd = tranlayer.insertorupdateTrn("update svcf.employee_details set Designation_ID=" + jj.Rows[i]["EmpDes_ID"] + " where Emp_ID=" + jj.Rows[i]["Emp_ID"] + "");
                }
            }
            catch (Exception err) { }
        }


        public void UpdateEncrypedPwrd(string pwrd)
        {
            var encrypwd = "";
            var spwrd = "";
            var hpwrd = "";
            int branchid = 1485;
            int slno = 37;
            long pwdupdate;
            //karaikudi M =1485
            var Getpwd = balayer.GetDataTable("select * from login where BranchID=" + branchid + " and Sl_No=" + slno + "");
            for (int i = 0; i < Getpwd.Rows.Count; i++)
            {
                encrypwd = Encrypt(pwrd);
                pwdupdate = tranlayer.insertorupdateTrn("update login set Encryptpwd= '" + encrypwd + "'  where Sl_No=" + Getpwd.Rows[i]["Sl_No"] + " and BranchID=" + branchid + "");
                spwrd = BCryptHelper.GenerateSalt();
                hpwrd = BCryptHelper.HashPassword(pwrd, spwrd);
                pwdupdate = tranlayer.insertorupdateTrn("update login set Password='" + pwrd + "', Salt= '" + spwrd + "',HashPassword='" + hpwrd + "'  where Sl_No=" + Getpwd.Rows[i]["Sl_No"] + " and BranchID=" + branchid + "");
            }
        }



        public void UpdateEncrypedPwrd_RptUser()
        {
            var encrypwd = "";
            var spwrd = "";
            var hpwrd = "";
            int branchid = 1485;
            int slno = 37;
            long pwdupdate;
            //karaikudi M =1485
            var Getpwd = balayer.GetDataTable("select * from login where UserName='user@svcf.com';");
            for (int i = 0; i < Getpwd.Rows.Count; i++)
            {
                encrypwd = Encrypt(Convert.ToString(Getpwd.Rows[i]["Password"]));
                spwrd = BCryptHelper.GenerateSalt();
                hpwrd = BCryptHelper.HashPassword(Convert.ToString(Getpwd.Rows[i]["Password"]), spwrd);
                pwdupdate = tranlayer.insertorupdateTrn("update login set Encryptpwd= '" + encrypwd + "', Salt= '" + spwrd + "',HashPassword='" + hpwrd + "'  where Sl_No=" + Getpwd.Rows[i]["Sl_No"] + " and BranchID=" + Getpwd.Rows[i]["BranchID"] + "");              
                //pwdupdate = tranlayer.insertorupdateTrn("update login set Password='" + pwrd + "', Salt= '" + spwrd + "',HashPassword='" + hpwrd + "'  where Sl_No=" + Getpwd.Rows[i]["Sl_No"] + " and BranchID=" + branchid + "");
            }
        }

        protected void btnlogin_Click(object sender, EventArgs e)
        {
            try
            {
                //string pwrd = Encrypt(txtPassword.Text);
                //string spwrd = BCryptHelper.GenerateSalt();
                //string hpwrd = BCryptHelper.HashPassword(txtPassword.Text, spwrd);
               // UpdateEncrypedPwrd("K@m#admin");
                ClsSession objSession = new ClsSession();
                var dtlogin = balayer.GetDataTable("Select * from login where BINARY UserName = '" + txtUser.Text + "' And  Password = '" + txtPassword.Text + "' and BranchID=" + ddlBranch.SelectedValue + "");
                string presentime = "";
                
                int logInCnt = 0;

                if (dtlogin.Rows.Count > 0 )
                {
                    logInCnt = Convert.ToInt32(dtlogin.Rows[0]["Countlog"]);
                    string logtimeck = Convert.ToString(dtlogin.Rows[0]["Timeoflog"]);

                    if (logtimeck != "")
                    {
                        DateTime logDate = Convert.ToDateTime(dtlogin.Rows[0]["Timeoflog"]);
                        TimeSpan span = DateTime.Now - logDate;
                        double totalMinutes = span.TotalMinutes;

                        if (totalMinutes > 30)
                        {
                            logInCnt = 0;
                        }
                    }
                    
                    result = BCryptHelper.CheckPassword(txtPassword.Text, dtlogin.Rows[0]["HashPassword"].ToString());                    

                    if (result == true && logInCnt < 3)
                    {
                        userid = dtlogin.Rows[0]["Sl_No"].ToString();
                        string roleid = balayer.GetSingleValue("select roleid from svcf.rights where memberid=" + userid + "");
                        Session["roleid"] = roleid;
                        Session["Branchid"] = ddlBranch.SelectedValue;
                        Session["UserName"] = txtUser.Text;
                        Session["BranchName"] = ddlBranch.SelectedItem.Text;

                        //userid = dtlogin.Rows[0]["Sl_No"].ToString();
                        objSession.RoleId = Convert.ToInt32(roleid);
                        objSession.BranchId = Convert.ToInt32(ddlBranch.SelectedValue);
                        objSession.UserName = txtUser.Text;
                        objSession.BranchName = ddlBranch.SelectedItem.Text;
                        objSession.SlNo = Convert.ToInt32(userid);

                        FormsAuthenticationTicket tkt;
                        presentime = Convert.ToString(DateTime.Now.TimeOfDay.Hours) + Convert.ToString(DateTime.Now.TimeOfDay.Minutes) + Convert.ToString(DateTime.Now.TimeOfDay.Seconds);
                        presentime = presentime + GetUniqueKey(4);
                        HttpCookie ck;
                        //userid = balayer.GetSingleValue("SELECT Sl_No FROM svcf.login where BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and UserName='" + balayer.ToobjectstrEvenNull(Session["UserName"]) + "'");
                        tkt = new FormsAuthenticationTicket(1, userid, DateTime.Now, DateTime.Now.AddMinutes(30), false, "Custom Data");
                        cookiestr = FormsAuthentication.Encrypt(tkt);
                        ck = new HttpCookie(FormsAuthentication.FormsCookieName, cookiestr);

                        ck.Path = FormsAuthentication.FormsCookiePath;
                        Response.Cookies.Add(ck);
                        strRedirect = Request["ReturnUrl"];
                        if (strRedirect == null)
                            strRedirect = "Home.aspx";
                        balayer.Disposeconnection();

                        string ipaddress;
                        string hostname;
                        ipaddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                        if (ipaddress == "" || ipaddress == null)
                            ipaddress = Request.ServerVariables["REMOTE_ADDR"];
                        hostname = Request.UserHostName;
                        hostname = hostname + ":" + ipaddress;

                        objSession.LoginIp = hostname;
                        objSession.SessionKey = presentime;
                        Session["objSession"] = objSession;

                        //for live DB
                        //query = "insert into svcf.LoginSession(UserId, BranchId, LoginTime,SessionKey,LoginIp) values(" + userid + "," + Convert.ToInt32(Session["Branchid"]) + "," +
                        //"'" + balayer.changedateformat(DateTime.Now, 1) + "','" + presentime + "','" + hostname + "')";

                        //for developement DB
                        query = "insert into svcf.loginsession(UserId, BranchId, LoginTime,SessionKey,LoginIp) values(" + userid + "," + Convert.ToInt32(Session["Branchid"]) + "," +
                              "'" + balayer.changedateformat(DateTime.Now, 1) + "','" + presentime + "','" + hostname + "')";
                        balayer.ExecuteQuery(query);

                        string logtime = ("update svcf.login set Timeoflog=null , Countlog=0 where HashPassword='" + Convert.ToString(dtlogin.Rows[0]["HashPassword"]) + "';");
                        tranlayer.insertorupdateTrn(logtime);
                        
                        Response.Redirect(strRedirect, true);
                        // FormsAuthentication.RedirectFromLoginPage(txtUser.Text,false);
                        //Response.Redirect("Home.aspx");
                        //}
                        //else
                        //{
                        //    ClientScript.RegisterStartupScript(this.Page.GetType(), "Validation", "<script Language='Javascript'>alert('Invalid UserName or Password')</script>");
                        //}                     
                      
                    }
                    else
                    {

                        string logtimeck1 = Convert.ToString(dtlogin.Rows[0]["Timeoflog"]);
                        if (logtimeck1 != "")
                        {
                                DateTime logDate1 = Convert.ToDateTime(dtlogin.Rows[0]["Timeoflog"]);

                                TimeSpan span = DateTime.Now - logDate1;
                                double totalMinutes = span.TotalMinutes;

                                if (totalMinutes > 30)
                                {
                                    logInCnt = 0;
                                }
                            }

                        if (logInCnt >= 3)
                        {
                            
                            var login1 = balayer.GetDataTable("Select * from login where BINARY UserName = '" + txtUser.Text + "' and BranchID=" + ddlBranch.SelectedValue + "");
                            logInCnt =Convert.ToInt32(login1.Rows[0]["Countlog"]) + 1;
                            string logtime = ("update svcf.login set Timeoflog='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', Countlog="+ logInCnt +" where HashPassword='" + Convert.ToString(dtlogin.Rows[0]["HashPassword"]) + "';");
                            tranlayer.insertorupdateTrn(logtime);
                        }

                        ClientScript.RegisterStartupScript(this.Page.GetType(), "Validation", "<script Language='Javascript'>alert('Invalid UserName or Password')</script>");
                    }
                }
              
                else
                {
                    var login2 = balayer.GetDataTable("Select * from login where BINARY UserName = '" + txtUser.Text + "' and  BranchID=" + ddlBranch.SelectedValue + "");
                    logInCnt = Convert.ToInt32(login2.Rows[0]["Countlog"]) + 1;
                    string logtime = ("update svcf.login set Timeoflog='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', Countlog=" + logInCnt + " where UserName='" + txtUser.Text + "' and BranchID =" + ddlBranch.SelectedValue + ";");
                    tranlayer.insertorupdateTrn(logtime);
                   
                    ClientScript.RegisterStartupScript(this.Page.GetType(), "Validation", "<script Language='Javascript'>alert('Invalid UserName or Password')</script>");
                }
            }

            catch (Exception err) { }
        }

        void SendPassword(string email)
        {
            try
            {
                var mailmsg = "";
                mailmsg = balayer.SendpasswordMail(email);
                ClientScript.RegisterStartupScript(this.Page.GetType(), "Validation", "<script Language='Javascript'>alert('" + mailmsg + "')</script>");
            }
            catch (Exception) { }
        }      
    }
}
