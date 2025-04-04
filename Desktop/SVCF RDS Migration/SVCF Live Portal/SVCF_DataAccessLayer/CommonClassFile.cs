using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Data;
using System.IO;
using System.Net;
using System.Drawing;
using System.Globalization;
using System.Web.UI.WebControls;
using System.Net.Mail;
using SVCF_PropertyLayer;
using System.Diagnostics;
using System.Collections;


namespace SVCF_DataAccessLayer
{
    public class CommonClassFile
    {      

        public static string branchesxrlabel;
        public static string oixrlabel;
        public static string pandlxrLabel;
        public static bool isKyc_member = false;
        public static bool isAnydrawHappened = false;
        public static string series;
        public static string Branchid;
        public static string MemberID;
        public static string MT1_NewID;
        public static string MT1_OldID;
        public static string MT1_OldmemID;
        public static string MS_NewID;
        public static string MR_OldID;
        public static string CCS_Grpno;
        public static string CCS_Grpmemno;
        public static string receiptno;
        public static bool isAnyArrear = false;
        public static HttpPostedFile IMGusersPhoto;
        public static List<string> lstSuggestions;
        //
        public static string BranchName;
        public static string UserName;

        public static string CashHeadID;
        public static string BankHeadID;
        //public static bool isAnydrawHappened = false ;
        //public static string  Branchid;
        //public static string MemberID;
        //public static bool isAnyArrear = false;


        public Dictionary<string, string> ListItems = new Dictionary<string, string>();
        public List<string> ListStr = new List<string>();
        MySqlDataReader dr;

        //public static string ConnectionString = "server=122.165.215.25;database=svcf;UID=root;PWD=sqladcl;Allow Zero Datetime=true;port=3306;Old Guids=true;pooling=true;Max Pool Size=200; ";
        //public static string ConnectionString = "server=192.168.0.222;database=svcf;pwd=redhat;UID=root;Allow Zero Datetime=true;port=3306;Old Guids=true;pooling=true;Max Pool Size=200;";

        private MySqlConnection Conn = null;
        private MySqlCommand Cmd = null;

        //public static string ConnectionString = "server=localhost;database=svcf;UID=root;PWD=sqladcl;Allow Zero Datetime=true;port=3306;Old Guids=true;pooling=true;Max Pool Size=200; ";
       // public static string ConnectionString = "server=db.sreevisalam.internal;database=svcf;UID=svcf_admin;PWD=5buo0e0i495Cpnn;Allow Zero Datetime=true;port=3306;Old Guids=true;pooling=true;Max Pool Size=200;";
//        public static string ConnectionString = "server=sreevisalam-prod-dec.cluster-crxftl2ep348.ap-south-1.rds.amazonaws.com;database=svcf;UID=svcf_admin;PWD=svcf1234;Allow Zero Datetime=true;port=3306;Old Guids=true;pooling=true;Max Pool Size=200;";
     
        //public static string ConnectionString = "server=sreevisalam-prod-12feb18.crxftl2ep348.ap-south-1.rds.amazonaws.com;database=svcf;UID=svcf_admin;PWD=DB8092feb18;Allow Zero Datetime=true;port=3306;Old Guids=true;pooling=true;Max Pool Size=200;";       
        //public static string ConnectionString = "server=10.140.3.78;database=svcf;UID=svcf_admin;PWD=5buo0e0i495Cpnn;Allow Zero Datetime=true;port=3306;Old Guids=true;pooling=true;Max Pool Size=200;";      
        //public static string ConnectionString = "server=sreevisalam-prod-1.cluster-crxftl2ep348.ap-south-1.rds.amazonaws.com;database=svcf;UID=svcf_admin;PWD=5buo0e0i495Cpnn;Allow Zero Datetime=true;port=3306;Old Guids=true;pooling=true;Max Pool Size=200;";

        //Working Connection
        //public static string ConnectionString = "server=sreevisalam-prod-22mar18.crxftl2ep348.ap-south-1.rds.amazonaws.com;database=svcf;UID=svcf_admin;PWD=svcf22mar2018;Allow Zero Datetime=true;port=3306;Old Guids=true;pooling=true;Max Pool Size=200;";
        private int PageSize=10;

        public static int pageIn; public static int recordCount;

        public static string ConnectionString = Convert.ToString(ConfigurationManager.ConnectionStrings["SVCFLIVE"].ConnectionString);

        #region Trigger Creation 
            //Trigger structure which cannot be seen in mysql....DO NOT REMOVE THIS STRUCTURE FOR REFERENCE
            //        create trigger voucher_after_delete
            //AFTER DELETE
            //on voucher FOR EACH ROW
            //insert into vouchertrigger(TransactionKey, DualTransactionKey, BranchID, CurrDate, Voucher_No, Voucher_Type, Head_Id, ChoosenDate, Narration, 
            //Amount, Series, ReceievedBy, Trans_Type, T_Day, T_Month, T_Year, MemberID, Trans_Medium, RootID, ChitGroupId, Other_Trans_Type, IsDeleted, 
            //IsAccepted, CreatedDate, ModifiedDate, ISActive, AppReceiptno, M_Id, LoginIP)
            //values(
            //old.TransactionKey,old.DualTransactionKey, old.BranchID, old.CurrDate, old.Voucher_No, old.Voucher_Type,old.Head_Id, old.ChoosenDate,
            //old.Narration, old.Amount, old.Series, old.ReceievedBy, old.Trans_Type, old.T_Day, old.T_Month, old.T_Year, old.MemberID, old.Trans_Medium, old.RootID,
            //old.ChitGroupId, old.Other_Trans_Type, old.IsDeleted,old.IsAccepted, old.CreatedDate, old.ModifiedDate, old.ISActive, old.AppReceiptno, old.M_Id,
            //old.LoginIP);

        #endregion

        public string Get24HourTime(int hour, int minute, string ToD, double AddTime)
        {
            int year = DateTime.Now.Year;
            int month = DateTime.Now.Month;
            int day = DateTime.Now.Day;
            if (ToD.ToUpper() == "PM")
                hour = (hour % 12) + 12;
            DateTime dati = new DateTime(year, month, day, hour, minute, 0);
            dati = dati.AddMinutes(AddTime);
            return dati.ToString("HH:mm");
        }
        
        public string Get12HourTime(int hour, int minute, string ToD)
        {
            int year = DateTime.Now.Year;
            int month = DateTime.Now.Month;
            int day = DateTime.Now.Day;
            //if (ToD.ToUpper() == "PM") hour = (hour % 12) + 12;
            DateTime dati = new DateTime(year, month, day, hour, minute, 0);
            // dati = dati.AddMinutes(AddTime);
            return dati.ToString("hh:mm tt");
        }
        //public static MySqlConnection openConnection()
        //{
        //    string SVCFConn = ConnectionString;
        //    MySqlConnection Conn = new MySqlConnection(SVCFConn);
        //    Conn.Open();
        //    return Conn;
        //}

        public string Getstringdaymonth(string date)
        {
            DateTime oodate = DateTime.ParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            string DateOfJoin = String.Format("{0}{1} {2}",
                                  oodate.Day,
                                  GetDaySuffix(oodate.Day),
                                  oodate.ToString("MMM yyyy", CultureInfo.InvariantCulture));
            return DateOfJoin;
        }
        public string GetDaySuffix(int day)
        {
            switch (day)
            {
                case 1:
                case 21:
                case 31:
                    return "st";
                case 2:
                case 22:
                    return "nd";
                case 3:
                case 23:
                    return "rd";
                default:
                    return "th";
            }
        }
        public string sp_gendratedt_key()
        {
            string newdualtrnskey = "";
            try
            {


                using (MySqlConnection myCon = openConnection())
                {
                    using (MySqlCommand myCmd = new MySqlCommand("sp_gendratedt_key", myCon))
                    {
                        if (myCon.State == ConnectionState.Closed)
                        {
                            myCon.Open();
                        }
                        myCmd.CommandType = CommandType.StoredProcedure;
                        object obj = myCmd.ExecuteScalar();
                        if (obj != null)
                        {
                            newdualtrnskey = Convert.ToString(obj);
                        }

                        if (myCon.State == ConnectionState.Open) myCon.Close();
                        Dispose();

                    }

                }
            }
            catch (Exception Ex)
            {

            }
            return newdualtrnskey;

        }
      

        public DataTable GetCustomersPageWise(int pageIndex)
        {

            DataTable dt = new DataTable("name");
            DataTable firstTable = new DataTable();
            var ds = new DataSet();

            // string constring = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            using (MySqlConnection con = openConnection())
            {
                using (MySqlCommand cmd = new MySqlCommand("GetCustomers_Pager", con))
                {
                    con.Close();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("_PageIndex", pageIndex);
                    cmd.Parameters.AddWithValue("_PageSize", PageSize);
                    cmd.Parameters.Add("_RecordCount", MySqlDbType.Int32, 4);
                    
                    cmd.Parameters["_RecordCount"].Direction = ParameterDirection.Output;
                    con.Open();
                    MySqlDataAdapter myad = new MySqlDataAdapter(cmd);
                    
                    ds.Clear();
                    ds.Tables.Add(dt);
                    
                    myad.Fill(ds);
                    firstTable = ds.Tables[1];
                   
                    con.Close();
                    pageIn = Convert.ToInt32(cmd.Parameters["_PageIndex"].Value);
                    recordCount = Convert.ToInt32(cmd.Parameters["_RecordCount"].Value);
                    
                }
            }
            return firstTable;
        }


        public MySqlConnection openConnection()
        {
            string SVCFConn = ConnectionString;
            MySqlConnection Conn = new MySqlConnection(SVCFConn);
            try
            {              
                
                if (Conn.State == ConnectionState.Closed)
                {
                    Conn.Open();
                }
                
            }
            catch (Exception e) {
                this.LogError(e);
            }
            return Conn;
        }

        public List<ListItem> Getlistdata(String query)
        {
            using (MySqlConnection con = new MySqlConnection(ConnectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand(query, con))
                {
                    List<ListItem> customers = new List<ListItem>();
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = con;
                    con.Open();
                    using (MySqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            customers.Add(new ListItem
                            {
                                Value = sdr[0].ToString(),
                                Text = sdr[1].ToString()
                            });
                        }
                    }
                    con.Close();
                    return customers;
                }
            }
        }
            public List<ListItem> Bindaccloan(string query)
        {
            //DataTable dt = new DataTable();
            List<ListItem> objMC = new List<ListItem>();
            objMC.Clear();

            using (MySqlConnection con = openConnection())
            {
                using (MySqlCommand cmd = new MySqlCommand(query, con))
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    using (MySqlDataReader rser = cmd.ExecuteReader())
                    {
                        while (rser.Read())
                        {
                            objMC.Add(new ListItem
                            {
                                Text = Convert.ToString(rser["Node"]),
                                Value = Convert.ToString(rser["NodeID"])
                            });
                        }
                    }
                    con.Close();
                    return objMC;

                }
            }
        }

        public void Dispose()
        {
            Dispose(true);
            KillSleepingConnections(1000);           
            GC.SuppressFinalize(this);
        }
     
        public int KillSleepingConnections(int iMinSecondsToExpire)
        {
            string SVCFConn = ConnectionString;
            string strSQL = "show processlist";
            System.Collections.ArrayList m_ProcessesToKill = new ArrayList();
            MySqlConnection myConn = new MySqlConnection(SVCFConn);
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
                        //if (Host != "localhost:50883" && Host != "localhost:17386" && Host != "localhost:59822")
                        //{
                        //    m_ProcessesToKill.Add(iPID);
                        //}
                        m_ProcessesToKill.Add(iPID);
                    }
                }
                MyReader.Close();
                //foreach (int aPID in m_ProcessesToKill)
                //{
                //    strSQL = "kill " + aPID;
                //    myCmd.CommandText = strSQL;
                //    myCmd.ExecuteNonQuery();
                //}
            }
            catch (Exception excep)
            {
                this.LogError(excep);
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


        //// Dispose

        private void LogError(Exception ex)
        {
            string message = string.Format("Time: {0}", DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt"));
            message += Environment.NewLine;
            message += "-----------------------------------------------------------";
            message += Environment.NewLine;
            message += string.Format("Message: {0}", ex.Message);
            message += Environment.NewLine;
            message += string.Format("StackTrace: {0}", ex.StackTrace);
            message += Environment.NewLine;
            message += string.Format("Source: {0}", ex.Source);
            message += Environment.NewLine;
            message += string.Format("TargetSite: {0}", ex.TargetSite.ToString());
            message += Environment.NewLine;
            message += "-----------------------------------------------------------";
            message += Environment.NewLine;
            string path = HttpContext.Current.Server.MapPath("~/ErrorLog/ErrorLog.txt");
            using (StreamWriter writer = new StreamWriter(path, true))
            {
                writer.WriteLine(message);
                writer.Close();
            }
        }

        public void Dispose(bool disposing)
        {
            if (Conn != null) Conn.Dispose();
            if (Cmd != null) Cmd.Dispose();
            if (Conn != null)
            {
                Conn.Close();
                Conn.Dispose();
            }  
          
        }

        public double GetScalarDble(string cmdText)
        {
            double op = 0;
            object Result;
            using (Conn = openConnection())
            {
                using (Cmd = new MySqlCommand(cmdText, Conn))
                {
                    Result = Cmd.ExecuteScalar();
                    if (Result.GetType() != typeof(DBNull))
                    {
                        op = Convert.ToDouble(Result);
                    }
                }
                if (Conn.State == ConnectionState.Open) Conn.Close();
                Dispose();
            }

            return op;
        }

        public bool CheckVCExist(int vcno, int branchid)
        {
            bool res = false;
            string qry = "";
            qry = "select isFinished from VoucherSer where VoucherNo=" + vcno + " and Branchid=" + branchid + "";
            using (Conn = openConnection())
            {
                using (MySqlCommand Cmd = new MySqlCommand(qry, Conn))
                {
                    res = Convert.ToBoolean(Cmd.ExecuteScalar());
                }
                if (Conn.State == ConnectionState.Open) Conn.Close();
                Dispose();
            }
            return res;
        }

        public decimal GetScalarDecimal(string cmdText)
        {
            decimal op = 0;
            using (Conn = openConnection())
            {
                using (Cmd = new MySqlCommand(cmdText, Conn))
                {
                    op = Convert.ToDecimal(Cmd.ExecuteScalar());
                    if (op <= 0)
                    {
                        op = 0;
                    }
                }
                if (Conn.State == ConnectionState.Open) Conn.Close();
                Dispose();
            }

            return op;
        }

        public int GetScalarInt(string cmdText)
        {
            int op = 0;
            using (Conn = openConnection())
            {
                using (MySqlCommand Cmd = new MySqlCommand(cmdText, Conn))
                {
                    op = Convert.ToInt32(Cmd.ExecuteScalar());
                    if (op <= 0)
                    {
                        op = 0;
                    }
                }
                if (Conn.State == ConnectionState.Open) Conn.Close();
                Dispose();
            }

            return op;
        }
        //public static string TostringEvenNull(string str)
        //{
        //    if (string.IsNullOrEmpty(str))
        //    {
        //        str = "";
        //    }
        //    return  str.ToString();
        //}

        public string TostringEvenNull(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                str = "";
            }
            return str.ToString();
        }

        //public static string TostringEvenNull(object str)
        //{
        //    if (str == null| str==System.DBNull.Value )
        //    {
        //        str = "";
        //    }
        //    return str.ToString();
        //}

        public string ToobjstringEvenNull(object str)
        {
            if (str == null | str == System.DBNull.Value)
            {
                str = "";
            }
            return str.ToString();
        }

        public string TostrEvenNull(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                str = "";
            }
            return str.ToString();
        }

        //public static string indiandateToMysqlDate(string ddmmyy)
        //{
        //    if (string.IsNullOrEmpty(ddmmyy))
        //    {
        //        ddmmyy = "";
        //    }
        //    string strAuctionDate = "";
        //    if (ddmmyy.Trim() == "")
        //    {
        //        strAuctionDate = "";
        //    }
        //    else
        //    {
        //        strAuctionDate = ddmmyy.Split('/')[2] + "/" + ddmmyy.Split('/')[1] + "/" + ddmmyy.Split('/')[0];
        //    }
        //    return strAuctionDate;
        //}

        public string indiandateToMysqlDate(string ddmmyy)
        {
            if (string.IsNullOrEmpty(ddmmyy))
            {
                ddmmyy = "";
            }
            string strAuctionDate = "";
            if (ddmmyy.Trim() == "")
            {
                strAuctionDate = "";
            }
            else
            {
                strAuctionDate = ddmmyy.Split('/')[2] + "/" + ddmmyy.Split('/')[1] + "/" + ddmmyy.Split('/')[0];
            }
            return strAuctionDate;
        }

        //public static string ReplaceJunk(string ip)
        //{
        //    string Result = "";

        //    Result = ip.Replace("\r", " ").Replace("\n", " ").Replace("\t", " ").Replace("'", "''").Replace("\\", "\\\\");
        //    return Result;
        //}

        public string ReplaceJunk(string ip)
        {
            string Result = "";

            Result = ip.Replace("\r", " ").Replace("\n", " ").Replace("\t", " ").Replace("'", "''").Replace("\\", "\\\\");
            return Result;
        }
        //public static MySqlCommand GetCommand(string commandText, MySqlConnection  connection)
        //{
        //    MySqlCommand command = new MySqlCommand();
        //    command.CommandText = commandText;
        //    return command;
        //}

        public MySqlCommand GetCommand(string commandText, MySqlConnection connection)
        {
            Cmd = new MySqlCommand();
            Cmd.CommandText = commandText;
            return Cmd;
        }

        //Insert or update
        //public static int InsertOrUpdateorDelete(string cmdText)
        //{
        //    using (MySqlConnection conn = openConnection())
        //    {
        //        using (MySqlCommand cmd = new MySqlCommand(cmdText, conn))
        //        {
        //            return cmd.ExecuteNonQuery();
        //        }
        //    }
        //}

        //Insert or update
        public int InsertOrUpdateorDelete(string cmdText)
        {
            int ret = 0;
            try
            {              
                using (Conn = openConnection())
                {
                    if (Conn.State == ConnectionState.Closed)
                    {
                        Conn.Open();
                    }
                    using (MySqlCommand cmd = new MySqlCommand(cmdText, Conn))
                    {
                        ret = cmd.ExecuteNonQuery();
                        Conn.Close();
                        Dispose();
                       
                    }
                }

            }
            catch (Exception err)
            {

            }
            return ret;
        }

        ////Select and Return DataTable
        //public static DataTable SelectTable(string strQuery)
        //{
        //    DataTable dtSelectedTable = new DataTable();
        //    using (MySqlConnection myCon = openConnection())
        //    {
        //        using (MySqlCommand myCmd = new MySqlCommand(strQuery, myCon))
        //        {
        //            MySqlDataAdapter myAdapter = new MySqlDataAdapter(myCmd);
        //            myAdapter.Fill(dtSelectedTable);
        //            return dtSelectedTable;
        //        }
        //    }
        //}

        public DataTable SelectTable1(string strQuery)
        {
            DataTable dtSelectedTable = new DataTable();
            using (MySqlConnection myCon = openConnection())
            {
                using (MySqlCommand myCmd = new MySqlCommand(strQuery, myCon))
                {
                    if (myCon.State == ConnectionState.Closed)
                    {
                        myCon.Open();
                    }
                    MySqlDataAdapter myAdapter = new MySqlDataAdapter(myCmd);
                    myCmd.CommandTimeout = 1000;
                    myAdapter.Fill(dtSelectedTable);
                    if (myCon.State == ConnectionState.Open) myCon.Close();
                    Dispose();
                    return dtSelectedTable;
                }
                if (Conn.State == ConnectionState.Open) Conn.Close();
            }
        }



        public DataSet SelectDataset(string strQuery)
        {
            DataSet dtSelectedTable = new DataSet();
            using (MySqlConnection myCon = openConnection())
            {
                using (MySqlCommand myCmd = new MySqlCommand(strQuery, myCon))
                {
                    MySqlDataAdapter myAdapter = new MySqlDataAdapter(myCmd);
                    myAdapter.Fill(dtSelectedTable);
                    if (myCon.State == ConnectionState.Open) myCon.Close();
                    return dtSelectedTable;
                }
            }
        }
        ////avoid Sql injection and query error
        //public static string MySQLEscapeString(string usString)
        //{
        //    if (usString == null)
        //    {
        //        return null;
        //    }
        //    // it escapes \r, \n, \x00, \x1a, \, ', and "
        //    return System.Text.RegularExpressions.Regex.Replace(usString, @"[\r\n\x00\x1a\\'""]", @"\$0");
        //}

        //avoid Sql injection and query error
        public string MySQLEscapeString(string usString)
        {
            if (usString == null)
            {
                return null;
            }
            // it escapes \r, \n, \x00, \x1a, \, ', and "
            return System.Text.RegularExpressions.Regex.Replace(usString, @"[\r\n\x00\x1a\\'""]", @"\$0");
        }

        public DataTable RetrieveDt_SP(int gpid, int SBranchID, int SParentID, string SChoosenDate, string spname)
        {
            DataTable dtSelectedTable = new DataTable();
            using (MySqlConnection myCon = openConnection())
            {
                using (MySqlCommand myCmd = new MySqlCommand(spname, myCon))
                {
                    if (myCon.State == ConnectionState.Closed)
                    {
                        myCon.Open();
                    }
                    myCmd.CommandType = CommandType.StoredProcedure;
                    myCmd.Parameters.AddWithValue("@SGroupId", gpid);
                    myCmd.Parameters.AddWithValue("@SBranchID", SBranchID);
                    myCmd.Parameters.AddWithValue("@SParentID", SParentID);
                    myCmd.Parameters.AddWithValue("@SChoosenDate", SChoosenDate);
                    MySqlDataAdapter myAdapter = new MySqlDataAdapter(myCmd);
                    myAdapter.Fill(dtSelectedTable);
                    if (myCon.State == ConnectionState.Open) myCon.Close();
                    Dispose();
                    return dtSelectedTable;
                }

            }
        }


        public void Updatemembertogroup(string mname, string Bid, string Maddress, string Mid, int Head_id, string Groupid)
        {

            DataTable dtSelectedTable = new DataTable();
            using (MySqlConnection sqlcon = openConnection())
            {
                using (MySqlCommand myCmd = new MySqlCommand("UpdateMembertogroup", sqlcon))
                {
                    myCmd.CommandType = CommandType.StoredProcedure;
                    myCmd.Parameters.AddWithValue("@mname", mname);
                    myCmd.Parameters.AddWithValue("@Bid", Bid);
                    myCmd.Parameters.AddWithValue("@Maddress", Maddress);
                    myCmd.Parameters.AddWithValue("@Mid", Mid);
                    myCmd.Parameters.AddWithValue("@hid", Head_id);
                    myCmd.Parameters.AddWithValue("@gid", Groupid);
                    myCmd.ExecuteNonQuery();
                    sqlcon.Close();
                }
            }
        }


        public List<ListItem> Cr_srch(int BranchID, string spname,string srchtxt)
        {
            DataTable dtSelectedTable = new DataTable();
            List<ListItem> objMC = new List<ListItem>();
            objMC.Clear();
            using (MySqlConnection myCon = openConnection())
            {
                using (MySqlCommand myCmd = new MySqlCommand(spname, myCon))
                {
                    if (myCon.State == ConnectionState.Closed)
                    {
                        myCon.Open();
                    }
                    myCmd.CommandType = CommandType.StoredProcedure;
                    myCmd.Parameters.AddWithValue("@BranchID", BranchID);
                    myCmd.Parameters.AddWithValue("@cr_srch", srchtxt);
                    using (MySqlDataReader sdr = myCmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            objMC.Add(new ListItem
                            {
                                Text = Convert.ToString(sdr["TREE"]),
                                Value = Convert.ToString(sdr["ID"])
                            });
                        }
                    }
                    myCon.Close();
                    Dispose();
                    return objMC;
                }

            }          

        }


        public List<ListItem> Credit_srch(int BranchID, string srchtxt)
        {
            string qry = "";
            qry = "SELECT concat(cast(  v1.`RootID` as char),':',cast(v1.`TreeID` as char)) as ID,v1.`TREE` as TREE FROM `svcf`.`view_parent` as v1  where  " +
                  " v1.BranchID=" + BranchID + " and v1.`TREE` like '%" + srchtxt + "%'";
            List<ListItem> objMC = new List<ListItem>();
            objMC.Clear();
            using (MySqlConnection con = openConnection())
            {
                using (MySqlCommand cmd = new MySqlCommand(qry, con))
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    using (MySqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            objMC.Add(new ListItem
                            {
                                Text = Convert.ToString(sdr["TREE"]),
                                Value = Convert.ToString(sdr["ID"])
                            });
                        }
                    }
                    con.Close();
                    return objMC;

                }
            }

        }

        public List<ListItem> DefaultList_Voucher(int BranchID)
        {
            string qry = "";
            qry = "SELECT concat(cast(  v1.`RootID` as char),':',cast(v1.`TreeID` as char)) as ID,v1.`TREE` as TREE FROM `svcf`.`view_parent` as v1  where  " +
                  " v1.BranchID=" + BranchID + "";
            List<ListItem> objMC = new List<ListItem>();
            objMC.Clear();
            using (MySqlConnection con = openConnection())
            {
                using (MySqlCommand cmd = new MySqlCommand(qry, con))
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    using (MySqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            objMC.Add(new ListItem
                            {
                                Text = Convert.ToString(sdr["TREE"]),
                                Value = Convert.ToString(sdr["ID"])
                            });
                        }
                    }
                    con.Close();
                    return objMC;

                }
            }

        }


        //SGroupId int,in SBranchID int,in SChoosenDate date,in SHeadID
        public DataTable RetrieveDt_TASP(int gpid, int SBranchID, int SHeadID, string SChoosenDate, string spname)
        {
            DataTable dtSelectedTable = new DataTable();
            using (MySqlConnection myCon = openConnection())
            {
                using (MySqlCommand myCmd = new MySqlCommand(spname, myCon))
                {
                    if (myCon.State == ConnectionState.Closed)
                    {
                        myCon.Open();
                    }
                    myCmd.CommandType = CommandType.StoredProcedure;
                    myCmd.Parameters.AddWithValue("@SGroupId", gpid);
                    myCmd.Parameters.AddWithValue("@SBranchID", SBranchID);
                    myCmd.Parameters.AddWithValue("@SHeadID", SHeadID);
                    myCmd.Parameters.AddWithValue("@SChoosenDate", SChoosenDate);
                    MySqlDataAdapter myAdapter = new MySqlDataAdapter(myCmd);
                    myAdapter.Fill(dtSelectedTable);
                    if (myCon.State == ConnectionState.Open) myCon.Close();
                    Dispose();
                    return dtSelectedTable;
                }
            }
        }

        //SGroupId int,in SBranchID int,in SChoosenDate date,in SHeadID
        public DataTable RetVoucherHeads(int BranchID, string spname)
        {
            DataTable dtVHeads = new DataTable();
            try
            {
                using (MySqlConnection myCon = openConnection())
                {
                    using (MySqlCommand myCmd = new MySqlCommand(spname, myCon))
                    {
                        if (myCon.State == ConnectionState.Closed)
                        {
                            myCon.Open();
                        }
                        myCmd.CommandType = CommandType.StoredProcedure;
                        myCmd.CommandTimeout = 300;
                        myCmd.Parameters.AddWithValue("@Branchid", BranchID);                        
                        MySqlDataAdapter myAdapter = new MySqlDataAdapter(myCmd);
                        myAdapter.Fill(dtVHeads);
                        if (myCon.State == ConnectionState.Open) myCon.Close();
                        Dispose();
                    }
                }
            }
            catch (Exception ex) { }
            return dtVHeads;
        }


        //SGroupId int,in SBranchID int,in SChoosenDate date,in SHeadID
        public DataTable RetVrHeads(int BranchID, string spname,string srvalue)
        {
            DataTable dtVHeads = new DataTable();
            try
            {
                using (MySqlConnection myCon = openConnection())
                {
                    using (MySqlCommand myCmd = new MySqlCommand(spname, myCon))
                    {
                        if (myCon.State == ConnectionState.Closed)
                        {
                            myCon.Open();
                        }
                        myCmd.CommandType = CommandType.StoredProcedure;
                        myCmd.CommandTimeout = 300;
                        myCmd.Parameters.AddWithValue("@Branchid", BranchID);
                        myCmd.Parameters.AddWithValue("@vcvalue", srvalue);
                        MySqlDataAdapter myAdapter = new MySqlDataAdapter(myCmd);
                        myAdapter.Fill(dtVHeads);
                        if (myCon.State == ConnectionState.Open) myCon.Close();
                        Dispose();
                    }
                }
            }
            catch (Exception ex) { }
            return dtVHeads;
        }

        public MySqlDataReader ExecuteRdr(string cmdText)
        {
            Conn = new MySqlConnection(ConnectionString);
            using (MySqlCommand cmd = new MySqlCommand(cmdText, Conn))
            {
                if (Conn.State == ConnectionState.Closed)
                {
                    Conn.Open();
                }
                //  Dispose();
                return cmd.ExecuteReader(CommandBehavior.CloseConnection);

            }
        }

        public void fillBnkHd(DropDownList ddl)
        {
            List<ListItem> Heads1 = new List<ListItem>();
            try
            {
                string query = "SELECT * FROM customerbank";
                using (Conn = openConnection())
                {
                    using (MySqlCommand cmd = new MySqlCommand(query, Conn))
                    {

                        using (MySqlDataReader sdr = cmd.ExecuteReader())
                        {
                            while (sdr.Read())
                            {
                                Heads1.Add(new ListItem
                                {
                                    Value = sdr["bankkey"].ToString(),
                                    Text = sdr["bankname"].ToString()
                                });
                            }
                        }
                        ddl.Items.AddRange(Heads1.ToArray());
                    }
                }
            }
            catch (Exception err)
            {
                LogError(err);
            }


            #region existing portion
            
          
            //List<ListItem> Items = new List<ListItem>();
            //Items.Add(new ListItem("--Select--", "--Select--"));
            //Items.Add(new ListItem("Allahabad Bank", "Allahabad Bank"));
            //Items.Add(new ListItem("Andhra Bank", "Andhra Bank"));
            //Items.Add(new ListItem("Axis Bank", "Axis Bank"));
            //Items.Add(new ListItem("Bank of Baroda [Corporate]", "Bank of Baroda [Corporate]"));
            //Items.Add(new ListItem("Bank of Baroda [Retail]", "Bank of Baroda [Retail]"));
            //Items.Add(new ListItem("Bank of Bahrain and Kuwait", "Bank of Bahrain and Kuwait"));
            //Items.Add(new ListItem("Bank of Ceylon", "Bank of Ceylon"));
            //Items.Add(new ListItem("Bank of India", "Bank of India"));
            //Items.Add(new ListItem("Bank of Maharashtra", "Bank of Maharashtra"));
            //Items.Add(new ListItem("BNP Paribas", "BNP Paribas"));
            //Items.Add(new ListItem("Canara Bank", "Canara Bank"));
            //Items.Add(new ListItem("Catholic Syrian Bank", "Catholic Syrian Bank"));
            //Items.Add(new ListItem("Central Bank of India", "Central Bank of India"));
            //Items.Add(new ListItem("Chennai Central Co-Operative Bank", "Chennai Central Co-Operative Bank"));
            //Items.Add(new ListItem("Co-Operative Urban Bank", "Co-Operative Urban Bank"));
            //Items.Add(new ListItem("Corporation Bank", "Corporation Bank"));
            //Items.Add(new ListItem("Cosmos Bank", "Cosmos Bank"));
            //Items.Add(new ListItem("City Union Bank", "City Union Bank"));
            //Items.Add(new ListItem("Citi Bank", "Citi Bank"));
            //Items.Add(new ListItem("Cuddalore District Central Co-Operative Bank", "Cuddalore District Central Co-Operative Bank"));
            //Items.Add(new ListItem("DENA", "DENA"));
            //Items.Add(new ListItem("Deutsche Bank", "Deutsche Bank"));
            //Items.Add(new ListItem("DCB Bank", "DCB Bank"));
            //Items.Add(new ListItem("Allahabad Bank", "Allahabad Bank"));
            //Items.Add(new ListItem("Dhanlaxmi Bank", "Dhanlaxmi Bank"));
            //Items.Add(new ListItem("Equitas Small Financial Bank", "Equitas Small Financial Bank"));
            //Items.Add(new ListItem("Federal Bank", "Federal Bank"));
            //Items.Add(new ListItem("HDFC Bank", "HDFC Bank"));
            //Items.Add(new ListItem("HSBC", "HSBC"));
            //Items.Add(new ListItem("ICICI Bank", "ICICI Bank"));
            //Items.Add(new ListItem("IDBI Bank", "IDBI Bank"));
            //Items.Add(new ListItem("IndusInd Bank", "IndusInd Bank"));
            //Items.Add(new ListItem("IDFC Bank (Infrastructure Development Finance Company )", "IDFC Bank (Infrastructure Development Finance Company )"));
            //Items.Add(new ListItem("Indian Bank", "Indian Bank"));
            //Items.Add(new ListItem("Indian Overseas Bank", "Indian Overseas Bank"));
            //Items.Add(new ListItem("ING Vysya Bank", "ING Vysya Bank"));
            //Items.Add(new ListItem("Jammu & Kashmir Bank", "Jammu & Kashmir Bank"));
            //Items.Add(new ListItem("Karnataka Bank", "Karnataka Bank"));
            //Items.Add(new ListItem("Karur Vysya Bank", "Karur Vysya Bank"));
            //Items.Add(new ListItem("Kotak Bank", "Kotak Bank"));
            //Items.Add(new ListItem("Lakshmi Vilas Bank [Corporate]", "Lakshmi Vilas Bank [Corporate]"));
            //Items.Add(new ListItem("Lakshmi Vilas Bank [Retail]", "Lakshmi Vilas Bank [Retail]"));
            //Items.Add(new ListItem("Oriental Bank of Commerce", "Oriental Bank of Commerce"));
            //Items.Add(new ListItem("Pallavan Grama Bank", "Pallavan Grama Bank"));
            //Items.Add(new ListItem("Pandian Grama Bank", "Pandian Grama Bank"));
            //Items.Add(new ListItem("Post Office Savings Bank", "Post Office Savings Bank"));
            //Items.Add(new ListItem("Punjab and Sind Bank", "Punjab and Sind Bank"));
            //Items.Add(new ListItem("Punjab National Bank [Retail]", "Punjab National Bank [Retail]"));
            //Items.Add(new ListItem("Punjab National Bank [Corporate]", "Punjab National Bank [Corporate]"));
            //Items.Add(new ListItem("Punjab & Maharashtra Co-op Bank", "Punjab & Maharashtra Co-op Bank"));
            //Items.Add(new ListItem("Puduvai Bharathiyar Grama Bank", "Puduvai Bharathiyar Grama Bank"));
            //Items.Add(new ListItem("Ratnakar Bank", "Ratnakar Bank"));
            //Items.Add(new ListItem("RBS (The Royal Bank of Scotland)", "RBS (The Royal Bank of Scotland)"));
            //Items.Add(new ListItem("Saraswat Bank", "Saraswat Bank"));
            //Items.Add(new ListItem("South Indian Bank", "South Indian Bank"));
            //Items.Add(new ListItem("Standard Chartered Bank", "Standard Chartered Bank"));
            //Items.Add(new ListItem("State Bank of India", "State Bank of India"));
            //Items.Add(new ListItem("State Bank of Bikaner & Jaipur", "State Bank of Bikaner & Jaipur"));
            //Items.Add(new ListItem("State Bank of Hyderabad", "State Bank of Hyderabad"));
            //Items.Add(new ListItem("State Bank of Mysore", "State Bank of Mysore"));
            //Items.Add(new ListItem("State Bank of Patiala", "State Bank of Patiala"));
            //Items.Add(new ListItem("State Bank of Travancore", "State Bank of Travancore"));
            //Items.Add(new ListItem("Syndicate Bank", "Syndicate Bank"));
            //Items.Add(new ListItem("Tamilnadu Industrial Co-operative Bank", "Tamilnadu Industrial Co-operative Bank"));
            //Items.Add(new ListItem("Tamilnadu Mercantile Bank", "Tamilnadu Mercantile Bank"));
            //Items.Add(new ListItem("The Kanniyakumari Dist.Co.Op.Bank Ltd", "The Kanniyakumari Dist.Co.Op.Bank Ltd"));             
            //Items.Add(new ListItem("The Shamrao Vidhal Co-Operative Bank", "The Shamrao Vidhal Co-Operative Bank"));
            //Items.Add(new ListItem("The Sivagangai District Central Co-Op Bank", "The Sivagangai District Central Co-Op Bank"));
            //Items.Add(new ListItem("The Madurai District Central Co-Operative Bank", "The Madurai District Central Co-Operative Bank"));
            //Items.Add(new ListItem("Tiruchirappalli District Central Co-Operative Bank","Tiruchirappalli District Central Co-Operative Bank"));
            //Items.Add(new ListItem("Tirunelveli District Central Co-Operative", "Tirunelveli District Central Co-Operative"));
            //Items.Add(new ListItem("Tuticorin District Central Co-Operative Bank", "Tuticorin District Central Co-Operative Bank"));
            //Items.Add(new ListItem("Tuticorin Melur Co-Operative Bank", "Tuticorin Melur Co-Operative Bank"));
            //Items.Add(new ListItem("TNSC Bank", "TNSC Bank"));
            //Items.Add(new ListItem("The Kumbakonam Central Co-operative Bank Ltd", "The Kumbakonam Central Co-operative Bank Ltd"));
            //Items.Add(new ListItem("UCO Bank", "UCO Bank"));
            //Items.Add(new ListItem("Union Bank of India", "Union Bank of India"));
            //Items.Add(new ListItem("Vallalar Co-Operative Bank", "Vallalar Co-Operative Bank"));
            //Items.Add(new ListItem("Vijaya Bank", "Vijaya Bank"));
            //Items.Add(new ListItem("Villupuram District Central Co-Operative Bank", "Villupuram District Central Co-Operative Bank"));
            //Items.Add(new ListItem("YES Bank", "YES Bank"));
            //Items.Add(new ListItem("United Bank of India", "United Bank of India"));
            //Items.Add(new ListItem("Pondicherry Central Co-Operative Bank", "Pondicherry Central Co-Operative Bank"));
            //Items.Add(new ListItem("Post Office Savings Bank", "Central Government of India"));
            //ddl.Items.AddRange(Items.ToArray());
            #endregion

        }
        public List<ListItem> GetDropdownVMdr(int headiddr, int branchiddr)
        {

            //string query = "SELECT * FROM svcf.headstree where  NULLIF(Branchid, '" + branchid + "') IS NULL  and   Rootid ='" + headid + "'";

            string query = "SELECT  Node , CONCAT(Rootid,':',NodeID) as NodeID FROM svcf.headstree where  NULLIF(Branchid, '" + branchiddr + "') IS NULL  and   Rootid ='" + headiddr + "'";



            using (Conn = openConnection())
            {
                using (MySqlCommand cmd = new MySqlCommand(query, Conn))
                {

                    List<ListItem> Heads1 = new List<ListItem>();

                    using (MySqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            Heads1.Add(new ListItem
                            {
                                Value = sdr["NodeID"].ToString(),
                                Text = sdr["Node"].ToString()
                            });
                        }

                    }

                    return Heads1;
                }
            }
        }

        public List<ListItem> GetCustomersdr()
        {

            string query = "SELECT Node,NodeID FROM svcf.headstree where NodeID between 1 and 12";

            using (Conn = openConnection())
            {
                using (MySqlCommand cmd = new MySqlCommand(query, Conn))
                {

                    List<ListItem> customers1 = new List<ListItem>();

                    using (MySqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            customers1.Add(new ListItem
                            {
                                Value = sdr["NodeID"].ToString(),
                                Text = sdr["Node"].ToString()
                            });
                        }

                    }

                    return customers1;
                }
            }
        }


        public double GetInstallment(int GroupId, int HeadId, DateTime ActionDate, double SumAmount)
        {
            try
            {
                using (MySqlConnection myCon = openConnection())
                {
                    using (MySqlCommand myCmd = new MySqlCommand("GetInstallmentNumber", myCon))
                    {
                        if (myCon.State == ConnectionState.Closed)
                        {
                            myCon.Open();
                        }
                        myCmd.CommandType = CommandType.StoredProcedure;
                        myCmd.CommandTimeout = 300;
                        myCmd.Parameters.AddWithValue("@gpid", GroupId);
                        myCmd.Parameters.AddWithValue("@hdid", HeadId);
                        myCmd.Parameters.AddWithValue("@acDate", changeformat(ActionDate, 2));
                        myCmd.Parameters.AddWithValue("@sumAmount", SumAmount);
                        myCmd.Parameters["@sumAmount"].Direction = ParameterDirection.Output;
                        myCmd.ExecuteNonQuery();
                        myCon.Close();
                        SumAmount = Convert.ToDouble(myCmd.Parameters["@sumAmount"].Value);
                    }
                }
            }
            catch (Exception) { }
            return SumAmount;
        }

        public List<ListItem> GetDropdownVM(int headid, int branchid)
        {

            //string query = "SELECT * FROM svcf.headstree where  NULLIF(Branchid, '" + branchid + "') IS NULL  and   Rootid ='" + headid + "'";

            string query = "SELECT  Node , CONCAT(Rootid,':',NodeID) as NodeID FROM svcf.headstree where  NULLIF(Branchid, '" + branchid + "') IS NULL  and   Rootid ='" + headid + "'";



            using (Conn = openConnection())
            {
                using (MySqlCommand cmd = new MySqlCommand(query, Conn))
                {

                    List<ListItem> Heads = new List<ListItem>();

                    using (MySqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            Heads.Add(new ListItem
                            {
                                Value = sdr["NodeID"].ToString(),
                                Text = sdr["Node"].ToString()
                            });
                        }

                    }

                    return Heads;
                }
            }
        }

        public string indiandtToMysqlDate(string ddmmyy)
        {
            if (string.IsNullOrEmpty(ddmmyy))
            {
                ddmmyy = "";
            }
            string strAuctionDate = "";
            if (ddmmyy.Trim() == "")
            {
                strAuctionDate = "";
            }
            else
            {
                strAuctionDate = ddmmyy.Split('/')[2] + "/" + ddmmyy.Split('/')[1] + "/" + ddmmyy.Split('/')[0];
            }
            return strAuctionDate;
        }

        public string ToobjstrEvenNull(object str)
        {
            if (str == null | str == System.DBNull.Value)
            {
                str = "";
            }
            return str.ToString();
        }

        public string MySQLEscapeString1(string usString)
        {
            if (usString == null)
            {
                return null;
            }
            // it escapes \r, \n, \x00, \x1a, \, ', and "
            return System.Text.RegularExpressions.Regex.Replace(usString, @"[\r\n\x00\x1a\\'""]", @"\$0");
        }

        //Insert or update
        public void ExecuteQry(string cmdText)
        {
            using (Conn = openConnection())
            {
                using (Cmd = new MySqlCommand(cmdText, Conn))
                {
                    Cmd.ExecuteNonQuery();
                }
                if (Conn.State == ConnectionState.Open) Conn.Close();
                Dispose();
            }
        }

        public object changeformat(DateTime dt, int bln)
        {
            string dateformat;
            dateformat = "";
            try
            {
                switch (bln)
                {
                    case 1:
                        dateformat = string.Format("{0:yyyy-MM-dd HH:mm:ss}", dt);
                        break;
                    case 2:
                        dateformat = string.Format("{0:yyyy-MM-dd}", dt);
                        break;
                    case 3:
                        dateformat = string.Format("{0:HH:mm:ss}", dt);
                        break;
                    case 4:
                        dateformat = string.Format("{0:yyyy-dd-MM HH:mm:ss}", dt);
                        break;
                    case 5:
                        dateformat = string.Format("{0:dd-MM-yyyy HH:mm:ss}", dt);
                        break;
                    case 6:
                        dateformat = string.Format("{0:dd-MM-yyyy}", dt);
                        break;
                    case 7:
                        dateformat = string.Format("{0:dd-MM-yyyy hh:mm:ss tt}", dt);
                        break;
                    case 8:
                        dateformat = string.Format("{0:hh:mm:ss}", dt);
                        break;
                    case 9:
                        dateformat = string.Format("{0:yyyy-dd-MM}", dt);
                        break;
                    case 10:
                        dateformat = string.Format("{0:dd/MM/yyyy}", dt);
                        break;
                    case 11:
                        dateformat = string.Format("hh:mm:ss tt}", dt);
                        break;
                    case 12:
                        dateformat = string.Format("{0:HH:mm}", dt);
                        break;
                    case 13:
                        dateformat = string.Format("{0:hh:mm tt}", dt);
                        break;
                    case 14:
                        dateformat = string.Format("{0:HH:mm}", dt);
                        break;
                    case 15:
                        dateformat = string.Format("{0:dd-MM-yy}", dt);
                        break;
                    case 16:
                        dateformat = string.Format("{0:dd/MM/yy}", dt);
                        break;
                    case 17:
                        dateformat = string.Format("{0:dd-MM-yyyy hh:mm tt}", dt);
                        break;
                }
                return dateformat;
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                return null;
            }
        }

        public List<ListItem> GetCustomers()
        {

            string query = "SELECT Node,NodeID FROM svcf.headstree where NodeID between 1 and 12";

            using (Conn = openConnection())
            {
                using (MySqlCommand cmd = new MySqlCommand(query, Conn))
                {

                    List<ListItem> customers = new List<ListItem>();

                    using (MySqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            customers.Add(new ListItem
                            {
                                Value = sdr["NodeID"].ToString(),
                                Text = sdr["Node"].ToString()
                            });
                        }

                    }

                    return customers;
                }
            }
        }

        //Convert .NET ShortDateString (mm/dd/yyyy) to MySQLDate (yyyy-mm-dd)
        //public static string DateConversion_DotNetDateToMySQLDate(string shortDate)
        //{
        //    string[] dateParts = shortDate.Split('/');
        //    return dateParts[2] + "/" + dateParts[0] + "/" + dateParts[1];
        //}

        public string DateConversion_DotNetDateToMySQLDate(string shortDate)
        {
            string[] dateParts = shortDate.Split('/');
            return dateParts[2] + "/" + dateParts[0] + "/" + dateParts[1];
        }

        //public static string  DateConversion_DotNetDateToMySQLDate(DateTime shortDate)
        //{
        //    return shortDate.Year + "/" + shortDate.Month + "/" + shortDate.Day;
        //}

        public string DtSlashConv_DotNetDateToMySQLDate(DateTime shortDate)
        {
            return shortDate.Year + "/" + shortDate.Month + "/" + shortDate.Day;
        }

        //Convert  MySQLDate to .NET ShortDateString (mm/dd/yyyy)  (yyyy-mm-dd)


        public string DateConversion_MySQLDateToDotNetDate(string shortDate)
        {
            string[] dateParts = shortDate.Split('/');
            return dateParts[1] + "/" + dateParts[2] + "/" + dateParts[0];
        }

        //public static string DateConversion_MySQLDateToDotNetDate(DateTime shortDate)
        //{
        //    return shortDate.Month + "/" + shortDate.Day + "/" + shortDate.Year;
        //}

        public string DteSlashConv_MySQLDateToDotNetDate(DateTime shortDate)
        {
            return shortDate.Month + "/" + shortDate.Day + "/" + shortDate.Year;
        }
        //Convert   .NET ShortDateString  (mm/dd/yyyy)   to indian (dd/mm/yyyy)

        //public static string DateConversion_DotNetDateToIndian(DateTime shortDate)
        //{
        //    return shortDate.Day  + "/" + shortDate.Month + "/" + shortDate.Year;
        //}


        public string DtslashConv_DotNetDateToIndian(DateTime shortDate)
        {
            return shortDate.Day + "/" + shortDate.Month + "/" + shortDate.Year;
        }

        //public static string DateConversion_DotNetDateToIndian(string  shortDate)
        //{
        //    string[] dateParts = shortDate.Split('/');
        //    return dateParts[1] + "/" + dateParts[0] + "/" + dateParts[2];
        //}

        public string DtSlConversion_DotNetDateToIndian(string shortDate)
        {
            string[] dateParts = shortDate.Split('/');
            return dateParts[1] + "/" + dateParts[0] + "/" + dateParts[2];
        }

        //public static string DateConversion_IndianToDotNet(string shortDate)
        //{
        //    string[] dateParts = shortDate.Split('/');
        //    return dateParts[1] + "/" + dateParts[0] + "/" + dateParts[2];
        //}

        public string DteConversion_IndianToDotNet(string shortDate)
        {
            string[] dateParts = shortDate.Split('/');
            return dateParts[1] + "/" + dateParts[0] + "/" + dateParts[2];
        }

        //public static string DateConversion_MysqlDateToIndian(string shortDate)
        //{
        //    string[] dateParts = shortDate.Split('/');
        //    return dateParts[2] + "/" + dateParts[1] + "/" + dateParts[0];
        //}

        public string DtConv_MysqlDateToIndian(string shortDate)
        {
            string[] dateParts = shortDate.Split('/');
            return dateParts[2] + "/" + dateParts[1] + "/" + dateParts[0];
        }

        //public static string DateConversion_MysqlDateToIndian(DateTime shortDate)
        //{
        //    return shortDate.Day + "/" + shortDate.Month + "/" + shortDate.Year;
        //}
        public string DtConv_MysqlDateToIndian(DateTime shortDate)
        {
            return shortDate.Day + "/" + shortDate.Month + "/" + shortDate.Year;
        }


        //public static  void SetParamValueFormulaeField(CrystalDecisions.CrystalReports.Engine.ReportDocument cr, string paramName, string paramValue)
        //{
        //    for (int i = 0; i < cr.DataDefinition.FormulaFields.Count; i++)

        //        if (cr.DataDefinition.FormulaFields[i].FormulaName == "{" + paramName + "}")

        //            cr.DataDefinition.FormulaFields[i].Text = "\"" + paramValue + "\"";
        //}

        //public void SetParamValueFormulaeField(CrystalDecisions.CrystalReports.Engine.ReportDocument cr, string paramName, string paramValue)
        //{
        //    for (int i = 0; i < cr.DataDefinition.FormulaFields.Count; i++)

        //        if (cr.DataDefinition.FormulaFields[i].FormulaName == "{" + paramName + "}")

        //            cr.DataDefinition.FormulaFields[i].Text = "\"" + paramValue + "\"";
        //}


        //public static string ConvertToIndianCurrency(string fare)
        //{
        //    decimal parsed = decimal.Parse(fare, CultureInfo.InvariantCulture);
        //    CultureInfo hindi = new CultureInfo("hi-IN");
        //    string op = string.Format(hindi, "{0:c}", parsed).Replace("रु", "");
        //    return op;
        //}

        public string ConvertToIndianCurrency(string fare)
        {
            decimal parsed = decimal.Parse(fare, CultureInfo.InvariantCulture);
            CultureInfo hindi = new CultureInfo("hi-IN");
            string op = string.Format(hindi, "{0:c}", parsed).Replace("रु", "");
            return op;
        }


        public Dictionary<string, string> CommonList(string query)
        {
            Conn = new MySqlConnection(ConnectionString);
            //int i=0;
            ListItems.Clear();
            using (Cmd = new MySqlCommand(query, Conn))
            {
                Conn.Open();
                dr = Cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dr.Read())
                {
                    ListItems.Add(dr[0].ToString(), dr[1].ToString());
                    // i++;
                }
                Conn.Close();
                dr.Dispose();
                Dispose();
                return ListItems;
            }

        }


        public List<ListItem> BindTRDD(string query)
        {
            DataTable dt = new DataTable();
            List<ListItem> objMC = new List<ListItem>();
            objMC.Clear();

            using (MySqlConnection con = openConnection())
            {
                using (MySqlCommand cmd = new MySqlCommand(query, con))
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    using (MySqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            objMC.Add(new ListItem
                            {
                                Text = Convert.ToString(sdr["receiptseries"]),
                                Value = Convert.ToString(sdr["moneycollid"])
                            });
                        }
                    }
                    con.Close();
                    return objMC;

                }
            }
        }
        public List<ListItem> BindDD(string query)
        {
            //DataTable dt = new DataTable();
            List<ListItem> objMC = new List<ListItem>();
            objMC.Clear();

            using (MySqlConnection con = openConnection())
            {
                using (MySqlCommand cmd = new MySqlCommand(query, con))
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    using (MySqlDataReader rser = cmd.ExecuteReader())
                    {
                        while (rser.Read())
                        {
                            objMC.Add(new ListItem
                            {
                                Text = Convert.ToString(rser["receiptseries"]),
                                Value = Convert.ToString(rser["moneycollid"])
                            });
                        }
                    }
                    con.Close();
                    return objMC;

                }
            }
        }


        public List<ListItem> BindvoucherCD(string query)
        {
            //DataTable dt = new DataTable();
            List<ListItem> objMC = new List<ListItem>();
            objMC.Clear();

            using (MySqlConnection con = openConnection())
            {
                using (MySqlCommand cmd = new MySqlCommand(query, con))
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    using (MySqlDataReader rser = cmd.ExecuteReader())
                    {
                        while (rser.Read())
                        {
                            objMC.Add(new ListItem
                            {
                                Text = Convert.ToString(rser["TREE"]),
                                Value = Convert.ToString(rser["TreeID"])
                            });
                        }
                    }
                    con.Close();
                    return objMC;

                }
            }
        }

        public List<string> BindAuctionDetails(string query)
        {
            Conn = new MySqlConnection(ConnectionString);
            int i = 0;
            ListStr.Clear();
            using (Cmd = new MySqlCommand(query, Conn))
            {
                if (Conn.State == ConnectionState.Closed)
                {
                    Conn.Open();
                }
                dr = Cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dr.Read())
                {
                    ListStr.Add(dr[0].ToString());
                    ListStr.Add(dr[1].ToString());
                    ListStr.Add(dr[2].ToString());
                    i++;
                }
                dr.Dispose();
                if (Conn.State == ConnectionState.Open) Conn.Close();
                Dispose();
                return ListStr;
            }
        }

        public List<ListItem> BinVHeads(int Branchid, string spname)
        {
            List<ListItem> objMC = new List<ListItem>();
            try
            {
                //SGroupId int,in SBranchID int,in SChoosenDate date,in SHeadID     

                objMC.Clear();
                //DataTable dtVHeads = new DataTable();
                using (MySqlConnection myCon = openConnection())
                {
                    using (MySqlCommand myCmd = new MySqlCommand(spname, myCon))
                    {
                        if (myCon.State == ConnectionState.Closed)
                        {
                            myCon.Open();
                        }
                        myCmd.CommandType = CommandType.StoredProcedure;
                        myCmd.CommandTimeout = 300;
                        myCmd.Parameters.AddWithValue("@Branchid", Branchid);
                        using (MySqlDataReader rser = myCmd.ExecuteReader())
                        {
                            while (rser.Read())
                            {
                                objMC.Add(new ListItem
                                {
                                    Text = Convert.ToString(rser["TREE"]),
                                    Value = Convert.ToString(rser["ID"])
                                });
                            }
                        }
                        myCon.Close();

                    }
                }
            }
            catch (Exception ex) { }
            return objMC;
        }

       
        //public static string GetSingleValue(string cmdText)
        //{
        //    string op = null ;
        //    using (MySqlConnection con = openConnection())
        //    {
        //        using (MySqlCommand cmd = new MySqlCommand(cmdText, con))
        //        {
        //            object obj = cmd.ExecuteScalar();
        //            if (obj != null)
        //            {
        //                op = Convert.ToString(obj);
        //            }
        //            else
        //            {
        //                op = "";
        //            }
        //        }
        //        con.Close();
        //    }


        //    return op;
        //}

        public string GetSingleValue1(string cmdText)
        {
            string op = null;
            using (MySqlConnection conn = openConnection())
            {
                using (MySqlCommand cmd = new MySqlCommand(cmdText, conn))
                {
                    object obj = cmd.ExecuteScalar();
                    if (obj != null)
                    {
                        op = Convert.ToString(obj);
                    }
                    else
                    {
                        op = "";
                    }
                }
                if (conn.State == ConnectionState.Open) conn.Close();
            }


            return op;
        }


        public List<string> RetreiveList(string query)
        {
            Conn = new MySqlConnection(ConnectionString);
            int i = 0;
            ListStr.Clear();
            using (Cmd = new MySqlCommand(query, Conn))
            {
                if (Conn.State == ConnectionState.Closed)
                {
                    Conn.Open();
                }
                dr = Cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dr.Read())
                {
                    ListStr.Add(dr[0].ToString());
                    i++;
                }
                dr.Dispose();
                if (Conn.State == ConnectionState.Open) Conn.Close();
                Dispose();
                return ListStr;
            }

        }


        public List<ListItem> BindToken(string query)
        {
            DataTable dt = new DataTable();
            List<ListItem> objMC = new List<ListItem>();
            objMC.Clear();

            using (Conn = new MySqlConnection(ConnectionString))
            {
                using (Cmd = new MySqlCommand(query, Conn))
                {
                    if (Conn.State == ConnectionState.Closed)
                    {
                        Conn.Open();
                    }
                    using (MySqlDataReader sdr = Cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            objMC.Add(new ListItem
                            {
                                Text = Convert.ToString(sdr["GrpMemberID"]),
                                Value = Convert.ToString(sdr["Head_Id"])
                            });
                        }
                    }
                    Conn.Close();
                    return objMC;
                }
            }
        }


       


        public List<ListItem> BindChitToken(string query)
        {
            DataTable dt = new DataTable();
            List<ListItem> objMC = new List<ListItem>();
            objMC.Clear();

            using (Conn = new MySqlConnection(ConnectionString))
            {
                using (Cmd = new MySqlCommand(query, Conn))
                {
                    if (Conn.State == ConnectionState.Closed)
                    {
                        Conn.Open();
                    }
                    using (MySqlDataReader sdr = Cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            objMC.Add(new ListItem
                            {
                                Text = Convert.ToString(sdr["GrpMemberID"]),
                                Value = Convert.ToString(sdr["Head_Id"])
                            });
                        }
                    }
                    Conn.Close();
                    Dispose();
                    return objMC;

                }
            }
        }


        public List<ListItem> BindChitGrp(string query)
        {
            DataTable dt = new DataTable();
            List<ListItem> objMC = new List<ListItem>();
            objMC.Clear();

            using (Conn = new MySqlConnection(ConnectionString))
            {
                if (Conn.State == ConnectionState.Closed)
                {
                    Conn.Open();
                }
                using (Cmd = new MySqlCommand(query, Conn))
                {
                    using (MySqlDataReader sdr = Cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            objMC.Add(new ListItem
                            {
                                Text = Convert.ToString(sdr["GROUPNO"]),
                                Value = Convert.ToString(sdr["Head_Id"])
                            });
                        }
                    }
                    Conn.Close();
                    Dispose();
                    return objMC;
                }
            }
        }





        public List<ListItem> BindMembername(string query)
        {
            DataTable dt = new DataTable();
            List<ListItem> objMC = new List<ListItem>();
            objMC.Clear();

            using (Conn = new MySqlConnection(ConnectionString))
            {
                if (Conn.State == ConnectionState.Closed)
                {
                    Conn.Open();
                }
                using (Cmd = new MySqlCommand(query, Conn))
                {
                    Conn.Open();
                    using (MySqlDataReader sdr = Cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            objMC.Add(new ListItem
                            {
                                Text = Convert.ToString(sdr["MemberName"]),
                                Value = Convert.ToString(sdr["Head_Id"])
                            });
                        }
                    }
                    if (Conn.State == ConnectionState.Open) Conn.Close();
                    Dispose();
                    return objMC;
                }
            }
        }

        //public static MySqlDataReader ExecuteReader(string cmdText)
        //{
        //    MySqlConnection conn = new MySqlConnection(ConnectionString);
        //    using (MySqlCommand cmd = new MySqlCommand(cmdText, conn))
        //    {
        //        conn.Open();
        //        return cmd.ExecuteReader(CommandBehavior.CloseConnection);
        //    }
        //}

        public MySqlDataReader ExecuteReader(string cmdText)
        {
            Conn = new MySqlConnection(ConnectionString);
            using (MySqlCommand cmd = new MySqlCommand(cmdText, Conn))
            {
                if (Conn.State == ConnectionState.Closed)
                {
                    Conn.Open();
                }

                return cmd.ExecuteReader(CommandBehavior.CloseConnection);
            }
        }

        //public static string indiandateToMysqlDate(string ddmmyy)
        //{
        //    string strDate = ddmmyy.Split('/')[2] + "/" + ddmmyy.Split('/')[1] + "/" + ddmmyy.Split('/')[0];
        //    return strDate;
        //}
        //public static void fillBankHead(DropDownList ddl)
        //{
        //    List<ListItem> Items = new List<ListItem>();
        //    Items.Add(new ListItem("--Select--", "--Select--"));
        //    Items.Add(new ListItem("Allahabad Bank", "Allahabad Bank"));
        //    Items.Add(new ListItem("Andhra Bank", "Andhra Bank"));
        //    Items.Add(new ListItem("Axis Bank", "Axis Bank"));
        //    Items.Add(new ListItem("Bank of Baroda [Corporate]", "Bank of Baroda [Corporate]"));
        //    Items.Add(new ListItem("Bank of Baroda [Retail]", "Bank of Baroda [Retail]"));
        //    Items.Add(new ListItem("Bank of Bahrain and Kuwait", "Bank of Bahrain and Kuwait"));
        //    Items.Add(new ListItem("Bank of India", "Bank of India"));
        //    Items.Add(new ListItem("Bank of Maharashtra", "Bank of Maharashtra"));
        //    Items.Add(new ListItem("BNP Paribas", "BNP Paribas"));
        //    Items.Add(new ListItem("Canara Bank", "Canara Bank"));
        //    Items.Add(new ListItem("Catholic Syrian Bank", "Catholic Syrian Bank"));
        //    Items.Add(new ListItem("Central Bank of India", "Central Bank of India"));
        //    Items.Add(new ListItem("Corporation Bank", "Corporation Bank"));
        //    Items.Add(new ListItem("Cosmos Bank", "Cosmos Bank"));
        //    Items.Add(new ListItem("City Union Bank", "City Union Bank"));
        //    Items.Add(new ListItem("Citi Bank", "Citi Bank"));
        //    Items.Add(new ListItem("DENA", "DENA"));
        //    Items.Add(new ListItem("Deutsche Bank", "Deutsche Bank"));
        //    Items.Add(new ListItem("DCB Bank", "DCB Bank"));
        //    Items.Add(new ListItem("Allahabad Bank", "Allahabad Bank"));
        //    Items.Add(new ListItem("Dhanlaxmi Bank", "Dhanlaxmi Bank"));
        //    Items.Add(new ListItem("Federal Bank", "Federal Bank"));
        //    Items.Add(new ListItem("HDFC Bank", "HDFC Bank"));
        //    Items.Add(new ListItem("HSBC", "HSBC"));
        //    Items.Add(new ListItem("ICICI Bank", "ICICI Bank"));
        //    Items.Add(new ListItem("IDBI Bank", "IDBI Bank"));
        //    Items.Add(new ListItem("IndusInd Bank", "IndusInd Bank"));
        //    Items.Add(new ListItem("Indian Bank", "Indian Bank"));
        //    Items.Add(new ListItem("Indian Overseas Bank", "Indian Overseas Bank"));
        //    Items.Add(new ListItem("ING Vysya Bank", "ING Vysya Bank"));
        //    Items.Add(new ListItem("Jammu & Kashmir Bank", "Jammu & Kashmir Bank"));
        //    Items.Add(new ListItem("Karnataka Bank", "Karnataka Bank"));
        //    Items.Add(new ListItem("Karur Vysya Bank", "Karur Vysya Bank"));
        //    Items.Add(new ListItem("Kotak Bank", "Kotak Bank"));
        //    Items.Add(new ListItem("Lakshmi Vilas Bank [Corporate]", "Lakshmi Vilas Bank [Corporate]"));
        //    Items.Add(new ListItem("Lakshmi Vilas Bank [Retail]", "Lakshmi Vilas Bank [Retail]"));
        //    Items.Add(new ListItem("Oriental Bank of Commerce", "Oriental Bank of Commerce"));
        //    Items.Add(new ListItem("Punjab and Sind Bank", "Punjab and Sind Bank"));
        //    Items.Add(new ListItem("Punjab National Bank [Retail]", "Punjab National Bank [Retail]"));
        //    Items.Add(new ListItem("Punjab National Bank [Corporate]", "Punjab National Bank [Corporate]"));
        //    Items.Add(new ListItem("Punjab & Maharashtra Co-op Bank", "Punjab & Maharashtra Co-op Bank"));
        //    Items.Add(new ListItem("Ratnakar Bank", "Ratnakar Bank"));
        //    Items.Add(new ListItem("RBS (The Royal Bank of Scotland)", "RBS (The Royal Bank of Scotland)"));
        //    Items.Add(new ListItem("Saraswat Bank", "Saraswat Bank"));
        //    Items.Add(new ListItem("South Indian Bank", "South Indian Bank"));
        //    Items.Add(new ListItem("Standard Chartered Bank", "Standard Chartered Bank"));
        //    Items.Add(new ListItem("State Bank of India", "State Bank of India"));
        //    Items.Add(new ListItem("State Bank of Bikaner & Jaipur", "State Bank of Bikaner & Jaipur"));
        //    Items.Add(new ListItem("State Bank of Hyderabad", "State Bank of Hyderabad"));
        //    Items.Add(new ListItem("State Bank of Mysore", "State Bank of Mysore"));
        //    Items.Add(new ListItem("State Bank of Patiala", "State Bank of Patiala"));
        //    Items.Add(new ListItem("State Bank of Travancore", "State Bank of Travancore"));
        //    Items.Add(new ListItem("Syndicate Bank", "Syndicate Bank"));
        //    Items.Add(new ListItem("Tamilnad Mercantile Bank", "Tamilnad Mercantile Bank"));
        //    Items.Add(new ListItem("TNSC Bank", "TNSC Bank"));
        //    Items.Add(new ListItem("UCO Bank", "UCO Bank"));
        //    Items.Add(new ListItem("Union Bank of India", "Union Bank of India"));
        //    Items.Add(new ListItem("Vijaya Bank", "Vijaya Bank"));
        //    Items.Add(new ListItem("YES Bank", "YES Bank"));
        //    Items.Add(new ListItem("United Bank of India", "United Bank of India"));
        //    ddl.Items.AddRange(Items.ToArray());

        //}


        public string SendPassword(string email)
        {
            string retmsg = "";
            try
            {
                DataTable dt = SelectTable1("SELECT UserName,Password FROM login Where EmailID= '" + email + "'");
                if (dt.Rows.Count > 0)
                {
                    MailMessage Msg = new MailMessage();
                    Msg.From = new MailAddress("kalpana.gunasekaran@adcltech.com");
                    Msg.To.Add(email);
                    Msg.Subject = "Sree Visalam Chitfunds Login information";
                    Msg.Body = "Hi, <br/>Please check your Login Details<br/><br/>Your Username: " + dt.Rows[0]["UserName"] + "<br/><br/>Your Password: " + dt.Rows[0]["Password"] + "<br/><br/>";
                    Msg.IsBodyHtml = true;
                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = "smtp.gmail.com";
                    smtp.Port = 587;
                    smtp.EnableSsl = true;
                    smtp.Credentials = new System.Net.NetworkCredential("kalpana.gunasekaran@adcltech.com", "kalpanaaltius");

                    smtp.Send(Msg);
                    retmsg = "Your Password Details Sent to your mail";
                    //ClientScript.RegisterStartupScript(this.Page.GetType(), "Validation", "<script Language='Javascript'>alert('Your Password Details Sent to your mail')</script>");
                    //txtEmail.Text = "";
                    //this.ModalPopupExtender1.PopupControlID = "Pnlmsg1";
                    //this.ModalPopupExtender1.Show();
                    //Pnlmsg1.Visible = true;
                    //lblT.Text = "Status";
                    //lblContent.Text = "Your Password Details Sent to your mail";
                    //lblContent.ForeColor = System.Drawing.Color.Green;

                }
                else
                {
                    //ClientScript.RegisterStartupScript(this.Page.GetType(), "Validation", "<script Language='Javascript'>alert('Your Password Email Address Not Registered With us')</script>");
                    retmsg = "Your Password Email Address Not Registered With us";
                }
            }
            catch (Exception) { }
            return retmsg;
        }

        public string NumberToText(int number)
        {
            if (number == 0)
                return "zero";

            if (number < 0)
                return "minus " + NumberToText(Math.Abs(number));

            string words = "";
            if ((number / 10000000) > 0)
            {
                words += NumberToText(number / 10000000) + " Crore ";
                number %= 10000000;
            }
            //if ((number / 1000000) > 0)
            //{
            //    words += NumberToText(number / 1000000) + " million ";
            //    number %= 1000000;
            //}
            if ((number / 100000) > 0)
            {
                words += NumberToText(number / 100000) + " Lakh ";
                number %= 100000;
            }
            if ((number / 1000) > 0)
            {
                words += NumberToText(number / 1000) + " thousand ";
                number %= 1000;
            }

            if ((number / 100) > 0)
            {
                words += NumberToText(number / 100) + " hundred ";
                number %= 100;
            }

            if (number > 0)
            {
                if (words != "")
                    words += "and ";

                var unitsMap = new[] { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen" };
                var tensMap = new[] { "zero", "ten", "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety" };

                if (number < 20)
                    words += unitsMap[number];
                else
                {
                    words += tensMap[number / 10];
                    if ((number % 10) > 0)
                        words += "-" + unitsMap[number % 10];
                }
            }

            return words;
            //if (number == 0) return "Zero";
            //string and = isUK ? "and " : ""; // deals with UK or US numbering
            //if (number == -2147483648) return "Minus Two Billion One Hundred " + and +
            //"Forty Seven Million Four Hundred " + and + "Eighty Three Thousand " +
            //"Six Hundred " + and + "Forty Eight";
            //int[] num = new int[4];
            //int first = 0;
            //int u, h, t;
            //System.Text.StringBuilder sb = new System.Text.StringBuilder(); 
            //if (number < 0)
            //{
            //    sb.Append("Minus ");
            //    number = -number;
            //}
            //string[] words0 = {"", "One ", "Two ", "Three ", "Four ", "Five ", "Six ", "Seven ", "Eight ", "Nine "};
            //string[] words1 = {"Ten ", "Eleven ", "Twelve ", "Thirteen ", "Fourteen ", "Fifteen ", "Sixteen ", "Seventeen ", "Eighteen ", "Nineteen "};
            //string[] words2 = {"Twenty ", "Thirty ", "Forty ", "Fifty ", "Sixty ", "Seventy ", "Eighty ", "Ninety "};
            //string[] words3 = { "Thousand ", "Million ", "Billion " };
            //num[0] = number % 1000;           // units
            //num[1] = number / 1000;
            //num[2] = number / 1000000;
            //num[1] = num[1] - 1000 * num[2];  // thousands
            //num[3] = number / 1000000000;     // billions
            //num[2] = num[2] - 1000 * num[3];  // millions
            //for (int i = 3; i > 0; i--)
            //{
            //    if (num[i] != 0)
            //    {
            //        first = i;
            //        break;
            //    }
            //}
            //for (int i = first; i >= 0; i--)
            //{
            //    if (num[i] == 0) continue;
            //    u = num[i] % 10;              // ones
            //    t = num[i] / 10;
            //    h = num[i] / 100;             // hundreds
            //    t = t - 10 * h;               // tens
            //    if (h > 0) sb.Append(words0[h] + "Hundred ");
            //    if (u > 0 || t > 0)
            //    {
            //        if (h > 0 || i < first) sb.Append(and);
            //        if (t == 0)
            //            sb.Append(words0[u]);
            //        else if (t == 1)
            //            sb.Append(words1[u]);
            //        else
            //            sb.Append(words2[t - 2] + words0[u]);
            //    }
            //    if (i != 0) sb.Append(words3[i - 1]);
            //}
            //return sb.ToString().TrimEnd();
        }
    }
    public class Transcation : IDisposable
    {
        private string connectionString = null;
        private MySqlConnection mysqlConnection = null;
        private MySqlCommand mysqlCommand = null;
        private MySqlTransaction mysqlTransaction = null;

        //public Transcation(bool isTransaction)
        //{
        //    connectionString = CommonClassFile.ConnectionString;
        //    mysqlConnection = new MySqlConnection(connectionString);
        //    if (mysqlConnection.State == ConnectionState.Closed)
        //    {
        //        mysqlConnection.Open();
        //    }
        //    if (isTransaction) mysqlTransaction = mysqlConnection.BeginTransaction();
        //    mysqlCommand = mysqlConnection.CreateCommand();
        //    mysqlCommand.Connection = mysqlConnection;
        //}
        public Transcation()
        {
            //mysqlTransaction = mysqlConnection.BeginTransaction();
            //mysqlCommand = mysqlConnection.CreateCommand();
            //mysqlCommand.Connection = mysqlConnection;
        }

        public MySqlConnection openConnection()
        {
            connectionString = CommonClassFile.ConnectionString;
            MySqlConnection Conn = new MySqlConnection(connectionString);
            if (Conn.State == ConnectionState.Closed)
            {
                Conn.Open();
            }
         
            return Conn;
        }

        public void Dispose()
        {
            Dispose(true);
            //MySqlconnection.Close();
            GC.SuppressFinalize(this);
        }

        // Dispose
        public void Dispose(bool disposing)
        {
            if (mysqlTransaction != null) mysqlTransaction.Dispose();
            if (mysqlCommand != null) mysqlCommand.Dispose();
            if (mysqlConnection != null)
            {
                mysqlConnection.Close();
                mysqlConnection.Dispose();
            }
        }

        // Commit transaction
        public void Commit()
        {
            using (mysqlConnection = openConnection())
            {
                if (mysqlConnection.State == ConnectionState.Closed)
                {
                    mysqlConnection.Open();
                }
                mysqlTransaction = mysqlConnection.BeginTransaction();
                mysqlTransaction.Commit();
                mysqlConnection.Close();
                Dispose();
            }
            if (mysqlConnection.State == ConnectionState.Open) mysqlConnection.Close();
        }

        // Rollback transaction
        public void Rollback()
        {
            using (mysqlConnection = openConnection())
            {
                if (mysqlConnection.State == ConnectionState.Closed)
                {
                    mysqlConnection.Open();
                }
                mysqlTransaction = mysqlConnection.BeginTransaction();
                mysqlTransaction.Rollback();
                mysqlConnection.Close();
                Dispose();
                if (mysqlConnection.State == ConnectionState.Open) mysqlConnection.Close();

            }
        }

        // Add data to table
        public long AddRow(string database, string table, string[] columns, object[] values, string binary_column = null, byte[] binary_data = null, string updateWhere = null)
        {
            string valuetags = "";

            long lastreturenedid = 0 ;

            if (columns.Length != values.Length) throw new Exception("Columns and value count does not match");

            if (binary_column != null) valuetags += "@bin,";

            for (int i = 0; i < columns.Length; i++)
            {
                if (i != 0) valuetags += ",";
                valuetags += "@p" + i.ToString();
            }

            if (updateWhere == null)
            {
                mysqlCommand.CommandText = "insert into `" + database + "`.`" + table + "` " + (binary_column != null ? "(`" + binary_column + "`,`" : "(`") + string.Join("`,`", columns) + "`) values (" + valuetags + ")";

                if (binary_data != null)
                    mysqlCommand.Parameters.AddWithValue("@bin", binary_data);

                for (int i = 0; i < columns.Length; i++)
                    mysqlCommand.Parameters.AddWithValue("@p" + i.ToString(), values[i]);
            }
            else
            {
                mysqlCommand.CommandText = string.Empty;

                for (int i = 0; i < columns.Length; i++)
                {
                    mysqlCommand.CommandText += "update `" + database + "`.`" + table + "` SET `" + columns[i] + "`=@p" + i.ToString() + "x" + " WHERE " + updateWhere + " LIMIT 1;";
                    mysqlCommand.Parameters.AddWithValue("@p" + i.ToString() + "x", values[i]);
                }
            }
            using (mysqlConnection = openConnection())
            {
                if (mysqlConnection.State == ConnectionState.Closed)
                {
                    mysqlConnection.Open();
                }
                mysqlCommand.ExecuteNonQuery();
                mysqlCommand.Parameters.Clear();
            }

            lastreturenedid = mysqlCommand.LastInsertedId;

            mysqlConnection.Close();
            return lastreturenedid;
            

        }

        // Add data using Column & Data class
        public long AddRow(string database, string table, List<ColumnAndData> listColData, string updateWhere = null)
        {
            return AddRow(database, table, listColData.Select(n => n.columnName).ToArray(), listColData.Select(n => n.dataValue).ToArray(), updateWhere: updateWhere);

        }
        //public long insertorupdate(string strQurey)
        //{
        //    mysqlCommand.CommandText = strQurey;
        //    mysqlCommand.ExecuteNonQuery();
        //    Dispose();
        //    return mysqlCommand.LastInsertedId;
        //}

        public long insertorupdate(string cmdText)
        {
            long ret = 0;
            using (mysqlConnection = openConnection())
            {
                if (mysqlConnection.State == ConnectionState.Closed)
                {
                    mysqlConnection.Open();
                }
                using (mysqlCommand = new MySqlCommand(cmdText, mysqlConnection))
                {
                    mysqlCommand.ExecuteNonQuery();                    
                    ret = mysqlCommand.LastInsertedId;
                    mysqlConnection.Close();
                    Dispose();
                    return ret;
                }
            }
        }

        // Sends a query to the database
        public void SendQuery(string query)
        {
            using (mysqlConnection = openConnection())
            {
                if (mysqlConnection.State == ConnectionState.Closed)
                {
                    mysqlConnection.Open();
                }
                using (mysqlCommand = new MySqlCommand(query, mysqlConnection))
                {
                    mysqlCommand.CommandText = query;
                    mysqlCommand.ExecuteNonQuery();
                    mysqlConnection.Close();
                    Dispose();

                    if (mysqlConnection.State == ConnectionState.Open) mysqlConnection.Close();
                }
            }

        }

        // Returns object
        public object GetObject(string query)
        {

            object ret = 0;
            using (mysqlConnection = openConnection())
            {
                if (mysqlConnection.State == ConnectionState.Closed)
                {
                    mysqlConnection.Open();
                }
                using (mysqlCommand = new MySqlCommand(query, mysqlConnection))
                {
                    mysqlCommand.CommandText = query;
                    ret = mysqlCommand.ExecuteScalar();
                    if (mysqlConnection.State == ConnectionState.Open) mysqlConnection.Close();
                    Dispose();
                }
            }
            return ret;
        }

        // Returns signed integer
        public int GetInt(string query)
        {
            int ret = 0;
            using (mysqlConnection = openConnection())
            {
                if (mysqlConnection.State == ConnectionState.Closed)
                {
                    mysqlConnection.Open();
                }
                using (mysqlCommand = new MySqlCommand(query, mysqlConnection))
                {
                    ret = int.Parse(GetObject(query).ToString());
                    if (mysqlConnection.State == ConnectionState.Open) mysqlConnection.Close();
                    Dispose();
                }
            }
                return ret;
        }

        // Returns unsigned integer
        public uint GetUint(string query)
        {
            uint ret = 0;
            using (mysqlConnection = openConnection())
            {
                if (mysqlConnection.State == ConnectionState.Closed)
                {
                    mysqlConnection.Open();
                }
                using (mysqlCommand = new MySqlCommand(query, mysqlConnection))
                {
                    ret = uint.Parse(GetObject(query).ToString());
                    if (mysqlConnection.State == ConnectionState.Open) mysqlConnection.Close();
                    Dispose();
                }
            }
                return ret;
        }

        // Returns string
        public string GetString(string query)
        {
            return GetObject(query).ToString();
        }

        // Returns datatable
        public DataTable GetTable(string query)
        {
            using (mysqlConnection = openConnection())
            {
                if (mysqlConnection.State == ConnectionState.Closed)
                {
                    mysqlConnection.Open();
                }

                using (DataSet ds = new DataSet())
                {
                    using (MySqlDataAdapter _adapter = new MySqlDataAdapter(query, mysqlConnection))
                        _adapter.Fill(ds, "map");
                    mysqlConnection.Close();
                    Dispose();
                    return ds.Tables[0];
                }
            }

        }

        public void BulkSend(string database, string table, string column, List<object> listData)
        {

            using (DataTable dataTable = new DataTable())
            {
                dataTable.Columns.Add(column);
                listData.ForEach(n => dataTable.Rows.Add(n));
                BulkSend(database, table, dataTable);
                Dispose();
            }
        }


        public void BulkSend(string database, string table, DataTable dataTable)
        {
            List<string> columnNames = new List<string>();
            List<string> columnIds = new List<string>();

            foreach (DataColumn column in dataTable.Columns)
            {
                columnNames.Add(column.ColumnName);
                columnIds.Add("?s" + columnNames.Count().ToString());
            }
            using (mysqlConnection = openConnection())
            {
                if (mysqlConnection.State == ConnectionState.Closed)
                {
                    mysqlConnection.Open();
                }
                using (MySqlCommand command = new MySqlCommand("INSERT INTO `" + database + "`.`" + table + "` (" + string.Join(",", columnNames) + ") VALUES (" + string.Join(",", columnIds) + ");", mysqlConnection))
                {
                    command.CommandType = CommandType.Text;
                    command.UpdatedRowSource = UpdateRowSource.None;

                    for (int i = 0; i < columnNames.Count; i++)
                    {
                        command.Parameters.Add(columnIds[i], MySqlDbType.String).SourceColumn = columnNames[i];
                    }

                    using (MySqlDataAdapter adapter = new MySqlDataAdapter())
                    {
                        adapter.ContinueUpdateOnError = true;
                        adapter.InsertCommand = command;
                        adapter.UpdateBatchSize = 100;
                        adapter.Update(dataTable);
                    }
                }
            }
        }




    }

    public class ColumnAndData
    {
        public string columnName { get; set; }
        public object dataValue { get; set; }

        public ColumnAndData()
        {
        }

        public ColumnAndData(string columnName, object dataValue)
        {
            this.columnName = columnName;
            this.dataValue = dataValue;
        }
    }
}

