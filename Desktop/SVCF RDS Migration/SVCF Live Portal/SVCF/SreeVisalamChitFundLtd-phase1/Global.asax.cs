using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Security.Principal;
using SVCF_BusinessAccessLayer;
using SVCF_TransactionLayer;
using SVCF_DataAccessLayer;
using MySql.Data.MySqlClient;

namespace SreeVisalamChitFundLtd_phase1
{
    public class Global : System.Web.HttpApplication
    {
        #region VarDeclaration
        BusinessLayer balayer = new BusinessLayer();
        TransactionLayer tranlayer = new TransactionLayer();
        static public System.Timers.Timer MyKillTimer = new System.Timers.Timer();
        public static string ConnectionString = "server=sreevisalam-prod-20mar18.crxftl2ep348.ap-south-1.rds.amazonaws.com;database=svcf;UID=svcf_admin;PWD=svcf95009;Allow Zero Datetime=true;port=3306;Old Guids=true;pooling=true;Max Pool Size=200;";
       
        #endregion
        protected void Application_PreRequestHandlerExecute(object sender, EventArgs e)
        {
            //Only access session state if it is available
            if (Context.Handler is IRequiresSessionState || Context.Handler is IReadOnlySessionState)
            {
                //If we are authenticated AND we dont have a session here.. redirect to login page.
                HttpCookie authenticationCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
                if (authenticationCookie != null)
                {
                    FormsAuthenticationTicket authenticationTicket = FormsAuthentication.Decrypt(authenticationCookie.Value);
                    if (!authenticationTicket.Expired)
                    {
                        //of course.. replace ANYKNOWNVALUEHERETOCHECK with "UserId" or something you set on the login that you can check here to see if its empty.
                        if (Session["Branchid"] == null)
                        {
                            //This means for some reason the session expired before the authentication ticket. Force a login.
                            FormsAuthentication.SignOut();
                            Response.Redirect(FormsAuthentication.LoginUrl, true);
                            return;
                        }
                    }
                }
            }
        }
        protected void Application_Start(object sender, EventArgs e)
        {
            try
            {
                MyKillTimer.Interval = 180000; // check sleeping connections every 3 minutes
                MyKillTimer.Elapsed += new System.Timers.ElapsedEventHandler(MyKillTimer_Event);
                MyKillTimer.AutoReset = true;
                MyKillTimer.Enabled = true;

                log4net.Config.XmlConfigurator.Configure();
            }
            catch (Exception excep)
            {
            }

        }

private void MyKillTimer_Event(object source, System.Timers.ElapsedEventArgs e)
{
    KillSleepingConnections(30);
}

static public int KillSleepingConnections(int iMinSecondsToExpire)
{
    string strSQL = "show processlist";
    System.Collections.ArrayList m_ProcessesToKill = new ArrayList();
    MySqlConnection myConn = new MySqlConnection(ConnectionString);
    MySqlCommand myCmd = new MySqlCommand(strSQL, myConn);
    MySqlDataReader MyReader = null;
    try
    {
        myConn.Open();
        // Get a list of processes to kill.
        MyReader = myCmd.ExecuteReader();
        while (MyReader.Read())
        {
            // Find all processes sleeping with a timeout value higher than our threshold.
            int iPID = Convert.ToInt32(MyReader["Id"].ToString());
            string strState = MyReader["Command"].ToString();
            string Host = MyReader["Host"].ToString();
            int iTime = Convert.ToInt32(MyReader["Time"].ToString());
            if (strState == "Sleep" && iTime >= iMinSecondsToExpire && iPID > 0)
            {
                // This connection is sitting around doing nothing. Kill it.
                //if (Host != "localhost:50883")
                //{
                //    m_ProcessesToKill.Add(iPID);
                //}
                m_ProcessesToKill.Add(iPID);

            }
        }
        MyReader.Close();
        foreach (int aPID in m_ProcessesToKill)
        {
            strSQL = "kill " + aPID;
            myCmd.CommandText = strSQL;
            myCmd.ExecuteNonQuery();
        }
    }
    catch (Exception excep)
    {
    }
    finally
    {
        if (MyReader != null && !MyReader.IsClosed)
        {
            MyReader.Close();
        }
        if (myConn != null && myConn.State == ConnectionState.Open)
        {
            myConn.Close();
        }
    }
    return m_ProcessesToKill.Count;
}

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
            try
            {
                List<string> arr = new List<string>();
                if (Request.IsAuthenticated)
                {
                    DataTable mdr = balayer.GetDataTable("SELECT G.`name` FROM rights R INNER JOIN roles G ON R.roleid = G.id INNER JOIN login U ON R.memberid = U.`Sl_No` AND U.`Sl_No` ='" + User.Identity.Name + "'");
                    if (mdr.Rows.Count > 0)
                    {
                        for (int i = 0; i < mdr.Rows.Count; i++)
                        {
                            arr.Add(balayer.ToobjectstrEvenNull(mdr.Rows[i]["name"]));
                        }
                        string[] ddd = arr.OfType<object>().Select(o => o.ToString()).ToArray();
                        HttpContext.Current.User = new GenericPrincipal(User.Identity, ddd);
                    }
                }
            }
            catch (Exception err)
            {
                balayer.Disposeconnection();
            }
        }

        protected void Application_AuthorizeRequest(Object sender, EventArgs e)
        {
            if (this.Request.Path.ToUpper().Contains("LOGIN.ASPX") && this.Request.IsAuthenticated)
            {
                this.Response.Redirect("~/error403.aspx");
            }
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            Exception exc = Server.GetLastError();

            if (exc is HttpUnhandledException)
            {
                if (exc.InnerException != null)
                {
                    exc = new Exception(exc.InnerException.Message);
                    Server.Transfer("ErrorPage.aspx?handler=Application_Error%20-%20Global.asax",
                        true);
                }
            }
        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}