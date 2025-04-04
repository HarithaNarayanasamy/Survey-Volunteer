using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Web.UI.WebControls;


namespace SreeVisalamChitFundLtd_phase1
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

        
        public static string ConnectionString = "server=svcf.cel0eakw696q.ap-southeast-1.rds.amazonaws.com;database=svcf;UID=root;PWD=rootsvcf;Allow Zero Datetime=true;port=3306;Old Guids=true;pooling=true;Max Pool Size=200; ";


        public static string Get24HourTime(int hour, int minute, string ToD, double AddTime)
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
        public static string Get12HourTime(int hour, int minute, string ToD)
        {
            int year = DateTime.Now.Year;
            int month = DateTime.Now.Month;
            int day = DateTime.Now.Day;
            //if (ToD.ToUpper() == "PM") hour = (hour % 12) + 12;
            DateTime dati = new DateTime(year, month, day, hour, minute, 0);
            // dati = dati.AddMinutes(AddTime);
            return dati.ToString("hh:mm tt");
        }
        public static MySqlConnection openConnection()
        {
            string SVCFConn = ConnectionString;
            MySqlConnection Conn = new MySqlConnection(SVCFConn);
            Conn.Open();
            return Conn;
        }

        public double GetScalarDble(string cmdText)
        {
            double op = 0;
            using (MySqlConnection conn = openConnection())
            {
                using (MySqlCommand cmd = new MySqlCommand(cmdText, conn))
                {
                    op = Convert.ToDouble(cmd.ExecuteScalar());
                    if (op <= 0)
                    {
                        op = 0;
                    }
                }
            }

            return op;
        }

        public int GetScalarInt(string cmdText)
        {
            int op = 0;
            using (MySqlConnection conn = openConnection())
            {
                using (MySqlCommand cmd = new MySqlCommand(cmdText, conn))
                {
                    op = Convert.ToInt32(cmd.ExecuteScalar());
                    if (op <= 0)
                    {
                        op = 0;
                    }
                }
            }

            return op;
        }
        public static string TostringEvenNull(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                str = "";
            }
            return  str.ToString();
        }

        public static string TostringEvenNull(object str)
        {
            if (str == null| str==System.DBNull.Value )
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

        public static string indiandateToMysqlDate(string ddmmyy)
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
        public static string ReplaceJunk(string ip)
        {
            string Result = "";

            Result = ip.Replace("\r", " ").Replace("\n", " ").Replace("\t", " ").Replace("'", "''").Replace("\\", "\\\\");
            return Result;
        }
        public static MySqlCommand GetCommand(string commandText, MySqlConnection  connection)
        {
            MySqlCommand command = new MySqlCommand();
            command.CommandText = commandText;
            return command;
        }

        //Insert or update
        public static int InsertOrUpdateorDelete(string cmdText)
        {
            using (MySqlConnection conn = openConnection())
            {
                using (MySqlCommand cmd = new MySqlCommand(cmdText, conn))
                {
                    return cmd.ExecuteNonQuery();
                }
            }
        }

        //Select and Return DataTable
        public static DataTable SelectTable(string strQuery)
        {
            DataTable dtSelectedTable = new DataTable();
            using (MySqlConnection myCon = openConnection())
            {
                using (MySqlCommand myCmd = new MySqlCommand(strQuery, myCon))
                {
                    MySqlDataAdapter myAdapter = new MySqlDataAdapter(myCmd);
                    myAdapter.Fill(dtSelectedTable);
                    return dtSelectedTable;
                }
            }
        }

        public DataTable SelectTable1(string strQuery)
        {
            DataTable dtSelectedTable = new DataTable();
            using (MySqlConnection myCon = openConnection())
            {
                using (MySqlCommand myCmd = new MySqlCommand(strQuery, myCon))
                {
                    MySqlDataAdapter myAdapter = new MySqlDataAdapter(myCmd);
                    myAdapter.Fill(dtSelectedTable);
                    return dtSelectedTable;
                }
            }
        }

        //avoid Sql injection and query error
        public static string MySQLEscapeString(string usString)
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
                    myCmd.CommandType = CommandType.StoredProcedure;
                    myCmd.Parameters.AddWithValue("@SGroupId", gpid);
                    myCmd.Parameters.AddWithValue("@SBranchID", SBranchID);
                    myCmd.Parameters.AddWithValue("@SParentID", SParentID);
                    myCmd.Parameters.AddWithValue("@SChoosenDate", SChoosenDate);
                    MySqlDataAdapter myAdapter = new MySqlDataAdapter(myCmd);
                    myAdapter.Fill(dtSelectedTable);
                    return dtSelectedTable;
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
                    myCmd.CommandType = CommandType.StoredProcedure;
                    myCmd.Parameters.AddWithValue("@SGroupId", gpid);
                    myCmd.Parameters.AddWithValue("@SBranchID", SBranchID);
                    myCmd.Parameters.AddWithValue("@SHeadID", SHeadID);
                    myCmd.Parameters.AddWithValue("@SChoosenDate", SChoosenDate);
                    MySqlDataAdapter myAdapter = new MySqlDataAdapter(myCmd);
                    myAdapter.Fill(dtSelectedTable);
                    return dtSelectedTable;
                }
            }
        }

        public MySqlDataReader ExecuteRdr(string cmdText)
        {
            MySqlConnection conn = new MySqlConnection(ConnectionString);
            using (MySqlCommand cmd = new MySqlCommand(cmdText, conn))
            {
                conn.Open();
                return cmd.ExecuteReader(CommandBehavior.CloseConnection);
            }
        }

        public void fillBnkHd(DropDownList ddl)
        {
            List<ListItem> Items = new List<ListItem>();
            Items.Add(new ListItem("--Select--", "--Select--"));
            Items.Add(new ListItem("Allahabad Bank", "Allahabad Bank"));
            Items.Add(new ListItem("Andhra Bank", "Andhra Bank"));
            Items.Add(new ListItem("Axis Bank", "Axis Bank"));
            Items.Add(new ListItem("Bank of Baroda [Corporate]", "Bank of Baroda [Corporate]"));
            Items.Add(new ListItem("Bank of Baroda [Retail]", "Bank of Baroda [Retail]"));
            Items.Add(new ListItem("Bank of Bahrain and Kuwait", "Bank of Bahrain and Kuwait"));
            Items.Add(new ListItem("Bank of India", "Bank of India"));
            Items.Add(new ListItem("Bank of Maharashtra", "Bank of Maharashtra"));
            Items.Add(new ListItem("BNP Paribas", "BNP Paribas"));
            Items.Add(new ListItem("Canara Bank", "Canara Bank"));
            Items.Add(new ListItem("Catholic Syrian Bank", "Catholic Syrian Bank"));
            Items.Add(new ListItem("Central Bank of India", "Central Bank of India"));
            Items.Add(new ListItem("Corporation Bank", "Corporation Bank"));
            Items.Add(new ListItem("Cosmos Bank", "Cosmos Bank"));
            Items.Add(new ListItem("City Union Bank", "City Union Bank"));
            Items.Add(new ListItem("Citi Bank", "Citi Bank"));
            Items.Add(new ListItem("DENA", "DENA"));
            Items.Add(new ListItem("Deutsche Bank", "Deutsche Bank"));
            Items.Add(new ListItem("DCB Bank", "DCB Bank"));
            Items.Add(new ListItem("Allahabad Bank", "Allahabad Bank"));
            Items.Add(new ListItem("Dhanlaxmi Bank", "Dhanlaxmi Bank"));
            Items.Add(new ListItem("Federal Bank", "Federal Bank"));
            Items.Add(new ListItem("HDFC Bank", "HDFC Bank"));
            Items.Add(new ListItem("HSBC", "HSBC"));
            Items.Add(new ListItem("ICICI Bank", "ICICI Bank"));
            Items.Add(new ListItem("IDBI Bank", "IDBI Bank"));
            Items.Add(new ListItem("IndusInd Bank", "IndusInd Bank"));
            Items.Add(new ListItem("Indian Bank", "Indian Bank"));
            Items.Add(new ListItem("Indian Overseas Bank", "Indian Overseas Bank"));
            Items.Add(new ListItem("ING Vysya Bank", "ING Vysya Bank"));
            Items.Add(new ListItem("Jammu & Kashmir Bank", "Jammu & Kashmir Bank"));
            Items.Add(new ListItem("Karnataka Bank", "Karnataka Bank"));
            Items.Add(new ListItem("Karur Vysya Bank", "Karur Vysya Bank"));
            Items.Add(new ListItem("Kotak Bank", "Kotak Bank"));
            Items.Add(new ListItem("Lakshmi Vilas Bank [Corporate]", "Lakshmi Vilas Bank [Corporate]"));
            Items.Add(new ListItem("Lakshmi Vilas Bank [Retail]", "Lakshmi Vilas Bank [Retail]"));
            Items.Add(new ListItem("Oriental Bank of Commerce", "Oriental Bank of Commerce"));
            Items.Add(new ListItem("Punjab and Sind Bank", "Punjab and Sind Bank"));
            Items.Add(new ListItem("Punjab National Bank [Retail]", "Punjab National Bank [Retail]"));
            Items.Add(new ListItem("Punjab National Bank [Corporate]", "Punjab National Bank [Corporate]"));
            Items.Add(new ListItem("Punjab & Maharashtra Co-op Bank", "Punjab & Maharashtra Co-op Bank"));
            Items.Add(new ListItem("Ratnakar Bank", "Ratnakar Bank"));
            Items.Add(new ListItem("RBS (The Royal Bank of Scotland)", "RBS (The Royal Bank of Scotland)"));
            Items.Add(new ListItem("Saraswat Bank", "Saraswat Bank"));
            Items.Add(new ListItem("South Indian Bank", "South Indian Bank"));
            Items.Add(new ListItem("Standard Chartered Bank", "Standard Chartered Bank"));
            Items.Add(new ListItem("State Bank of India", "State Bank of India"));
            Items.Add(new ListItem("State Bank of Bikaner & Jaipur", "State Bank of Bikaner & Jaipur"));
            Items.Add(new ListItem("State Bank of Hyderabad", "State Bank of Hyderabad"));
            Items.Add(new ListItem("State Bank of Mysore", "State Bank of Mysore"));
            Items.Add(new ListItem("State Bank of Patiala", "State Bank of Patiala"));
            Items.Add(new ListItem("State Bank of Travancore", "State Bank of Travancore"));
            Items.Add(new ListItem("Syndicate Bank", "Syndicate Bank"));
            Items.Add(new ListItem("Tamilnad Mercantile Bank", "Tamilnad Mercantile Bank"));
            Items.Add(new ListItem("TNSC Bank", "TNSC Bank"));
            Items.Add(new ListItem("UCO Bank", "UCO Bank"));
            Items.Add(new ListItem("Union Bank of India", "Union Bank of India"));
            Items.Add(new ListItem("Vijaya Bank", "Vijaya Bank"));
            Items.Add(new ListItem("YES Bank", "YES Bank"));
            Items.Add(new ListItem("United Bank of India", "United Bank of India"));
            ddl.Items.AddRange(Items.ToArray());

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
            using (MySqlConnection conn = openConnection())
            {
                using (MySqlCommand cmd = new MySqlCommand(cmdText, conn))
                {
                    cmd.ExecuteNonQuery();
                }
                conn.Close();
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

        //Convert .NET ShortDateString (mm/dd/yyyy) to MySQLDate (yyyy-mm-dd)
        public static string DateConversion_DotNetDateToMySQLDate(string shortDate)
        {
            string[] dateParts = shortDate.Split('/');
            return dateParts[2] + "/" + dateParts[0] + "/" + dateParts[1];
        }

        public static string  DateConversion_DotNetDateToMySQLDate(DateTime shortDate)
        {
            return shortDate.Year + "/" + shortDate.Month + "/" + shortDate.Day;
        }

        //Convert  MySQLDate to .NET ShortDateString (mm/dd/yyyy)  (yyyy-mm-dd)
        public static string DateConversion_MySQLDateToDotNetDate(string shortDate)
        {
            string[] dateParts = shortDate.Split('/');
            return dateParts[1] + "/" + dateParts[2] + "/" + dateParts[0];
        }
        public static string DateConversion_MySQLDateToDotNetDate(DateTime shortDate)
        {
            return shortDate.Month + "/" + shortDate.Day  + "/" + shortDate.Year;
        }
        //Convert   .NET ShortDateString  (mm/dd/yyyy)   to indian (dd/mm/yyyy)

        public static string DateConversion_DotNetDateToIndian(DateTime shortDate)
        {
            return shortDate.Day  + "/" + shortDate.Month + "/" + shortDate.Year;
        }
        public static string DateConversion_DotNetDateToIndian(string  shortDate)
        {
            string[] dateParts = shortDate.Split('/');
            return dateParts[1] + "/" + dateParts[0] + "/" + dateParts[2];
        }
        public static string DateConversion_IndianToDotNet(string shortDate)
        {
            string[] dateParts = shortDate.Split('/');
            return dateParts[1] + "/" + dateParts[0] + "/" + dateParts[2];
        }

        public static string DateConversion_MysqlDateToIndian(string shortDate)
        {
            string[] dateParts = shortDate.Split('/');
            return dateParts[2] + "/" + dateParts[1] + "/" + dateParts[0];
        }
        public static string DateConversion_MysqlDateToIndian(DateTime shortDate)
        {
            return shortDate.Day + "/" + shortDate.Month + "/" + shortDate.Year;
        }

        public static  void SetParamValueFormulaeField(CrystalDecisions.CrystalReports.Engine.ReportDocument cr, string paramName, string paramValue)
        {
            for (int i = 0; i < cr.DataDefinition.FormulaFields.Count; i++)

                if (cr.DataDefinition.FormulaFields[i].FormulaName == "{" + paramName + "}")

                    cr.DataDefinition.FormulaFields[i].Text = "\"" + paramValue + "\"";
        }



        public static string ConvertToIndianCurrency(string fare)
        {
            decimal parsed = decimal.Parse(fare, CultureInfo.InvariantCulture);
            CultureInfo hindi = new CultureInfo("hi-IN");
            string op = string.Format(hindi, "{0:c}", parsed).Replace("रु", "");
            return op;
        }

        public Dictionary<string, string> CommonList(string query)
        {
            MySqlConnection conn = new MySqlConnection(ConnectionString);
            //int i=0;
            ListItems.Clear();
            using (MySqlCommand cmd = new MySqlCommand(query, conn))
            {
                conn.Open();
                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dr.Read())
                {
                    ListItems.Add(dr[0].ToString(), dr[1].ToString());
                    // i++;
                }
                dr.Dispose();
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


       
        public static string GetSingleValue(string cmdText)
        {
            string op = null ;
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
            }


            return op;
        }

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
            }


            return op;
        }


        public List<string> RetreiveList(string query)
        {
            MySqlConnection conn = new MySqlConnection(ConnectionString);
            int i = 0;
            ListStr.Clear();
            using (MySqlCommand cmd = new MySqlCommand(query, conn))
            {
                conn.Open();
                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dr.Read())
                {
                    ListStr.Add(dr[0].ToString());
                    i++;
                }
                dr.Dispose();
                return ListStr;
            }

        }


        public List<ListItem> BindToken(string query)
        {
            DataTable dt = new DataTable();
            List<ListItem> objMC = new List<ListItem>();
            objMC.Clear();

            using (MySqlConnection con = new MySqlConnection(ConnectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand(query, con))
                {
                    con.Open();
                    using (MySqlDataReader sdr = cmd.ExecuteReader())
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
                    con.Close();
                    return objMC;                  
                }
            }
        }

        public List<ListItem> BindMembername(string query)
        {
            DataTable dt = new DataTable();
            List<ListItem> objMC = new List<ListItem>();
            objMC.Clear();

            using (MySqlConnection con = new MySqlConnection(ConnectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand(query, con))
                {
                    con.Open();
                    using (MySqlDataReader sdr = cmd.ExecuteReader())
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
                    con.Close();
                    return objMC;
                }
            }
        }

        public static MySqlDataReader ExecuteReader(string cmdText)
        {
            MySqlConnection conn = new MySqlConnection(ConnectionString);
            using (MySqlCommand cmd = new MySqlCommand(cmdText, conn))
            {
                conn.Open();
                return cmd.ExecuteReader(CommandBehavior.CloseConnection);
            }
        }
        //public static string indiandateToMysqlDate(string ddmmyy)
        //{
        //    string strDate = ddmmyy.Split('/')[2] + "/" + ddmmyy.Split('/')[1] + "/" + ddmmyy.Split('/')[0];
        //    return strDate;
        //}
        public static void fillBankHead(DropDownList ddl)
        {
            List<ListItem> Items = new List<ListItem>();
            Items.Add(new ListItem("--Select--", "--Select--"));
            Items.Add(new ListItem("Allahabad Bank", "Allahabad Bank"));
            Items.Add(new ListItem("Andhra Bank", "Andhra Bank"));
            Items.Add(new ListItem("Axis Bank", "Axis Bank"));
            Items.Add(new ListItem("Bank of Baroda [Corporate]", "Bank of Baroda [Corporate]"));
            Items.Add(new ListItem("Bank of Baroda [Retail]", "Bank of Baroda [Retail]"));
            Items.Add(new ListItem("Bank of Bahrain and Kuwait", "Bank of Bahrain and Kuwait"));
            Items.Add(new ListItem("Bank of India", "Bank of India"));
            Items.Add(new ListItem("Bank of Maharashtra", "Bank of Maharashtra"));
            Items.Add(new ListItem("BNP Paribas", "BNP Paribas"));
            Items.Add(new ListItem("Canara Bank", "Canara Bank"));
            Items.Add(new ListItem("Catholic Syrian Bank", "Catholic Syrian Bank"));
            Items.Add(new ListItem("Central Bank of India", "Central Bank of India"));
            Items.Add(new ListItem("Corporation Bank", "Corporation Bank"));
            Items.Add(new ListItem("Cosmos Bank", "Cosmos Bank"));
            Items.Add(new ListItem("City Union Bank", "City Union Bank"));
            Items.Add(new ListItem("Citi Bank", "Citi Bank"));
            Items.Add(new ListItem("DENA", "DENA"));
            Items.Add(new ListItem("Deutsche Bank", "Deutsche Bank"));
            Items.Add(new ListItem("DCB Bank", "DCB Bank"));
            Items.Add(new ListItem("Allahabad Bank", "Allahabad Bank"));
            Items.Add(new ListItem("Dhanlaxmi Bank", "Dhanlaxmi Bank"));
            Items.Add(new ListItem("Federal Bank", "Federal Bank"));
            Items.Add(new ListItem("HDFC Bank", "HDFC Bank"));
            Items.Add(new ListItem("HSBC", "HSBC"));
            Items.Add(new ListItem("ICICI Bank", "ICICI Bank"));
            Items.Add(new ListItem("IDBI Bank", "IDBI Bank"));
            Items.Add(new ListItem("IndusInd Bank", "IndusInd Bank"));
            Items.Add(new ListItem("Indian Bank", "Indian Bank"));
            Items.Add(new ListItem("Indian Overseas Bank", "Indian Overseas Bank"));
            Items.Add(new ListItem("ING Vysya Bank", "ING Vysya Bank"));
            Items.Add(new ListItem("Jammu & Kashmir Bank", "Jammu & Kashmir Bank"));
            Items.Add(new ListItem("Karnataka Bank", "Karnataka Bank"));
            Items.Add(new ListItem("Karur Vysya Bank", "Karur Vysya Bank"));
            Items.Add(new ListItem("Kotak Bank", "Kotak Bank"));
            Items.Add(new ListItem("Lakshmi Vilas Bank [Corporate]", "Lakshmi Vilas Bank [Corporate]"));
            Items.Add(new ListItem("Lakshmi Vilas Bank [Retail]", "Lakshmi Vilas Bank [Retail]"));
            Items.Add(new ListItem("Oriental Bank of Commerce", "Oriental Bank of Commerce"));
            Items.Add(new ListItem("Punjab and Sind Bank", "Punjab and Sind Bank"));
            Items.Add(new ListItem("Punjab National Bank [Retail]", "Punjab National Bank [Retail]"));
            Items.Add(new ListItem("Punjab National Bank [Corporate]", "Punjab National Bank [Corporate]"));
            Items.Add(new ListItem("Punjab & Maharashtra Co-op Bank", "Punjab & Maharashtra Co-op Bank"));
            Items.Add(new ListItem("Ratnakar Bank", "Ratnakar Bank"));
            Items.Add(new ListItem("RBS (The Royal Bank of Scotland)", "RBS (The Royal Bank of Scotland)"));
            Items.Add(new ListItem("Saraswat Bank", "Saraswat Bank"));
            Items.Add(new ListItem("South Indian Bank", "South Indian Bank"));
            Items.Add(new ListItem("Standard Chartered Bank", "Standard Chartered Bank"));
            Items.Add(new ListItem("State Bank of India", "State Bank of India"));
            Items.Add(new ListItem("State Bank of Bikaner & Jaipur", "State Bank of Bikaner & Jaipur"));
            Items.Add(new ListItem("State Bank of Hyderabad", "State Bank of Hyderabad"));
            Items.Add(new ListItem("State Bank of Mysore", "State Bank of Mysore"));
            Items.Add(new ListItem("State Bank of Patiala", "State Bank of Patiala"));
            Items.Add(new ListItem("State Bank of Travancore", "State Bank of Travancore"));
            Items.Add(new ListItem("Syndicate Bank", "Syndicate Bank"));
            Items.Add(new ListItem("Tamilnad Mercantile Bank", "Tamilnad Mercantile Bank"));
            Items.Add(new ListItem("TNSC Bank", "TNSC Bank"));
            Items.Add(new ListItem("UCO Bank", "UCO Bank"));
            Items.Add(new ListItem("Union Bank of India", "Union Bank of India"));
            Items.Add(new ListItem("Vijaya Bank", "Vijaya Bank"));
            Items.Add(new ListItem("YES Bank", "YES Bank"));
            Items.Add(new ListItem("United Bank of India", "United Bank of India"));
            ddl.Items.AddRange(Items.ToArray());

        }

        public static string NumberToText(int number)
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

                var unitsMap = new [] { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen" };
                var tensMap = new [] { "zero", "ten", "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety" };

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

        public Transcation(bool isTransaction)
        {
            connectionString = CommonClassFile.ConnectionString;
            mysqlConnection = new MySqlConnection(connectionString);
            mysqlConnection.Open();
            if (isTransaction) mysqlTransaction = mysqlConnection.BeginTransaction();
            mysqlCommand = mysqlConnection.CreateCommand();
            mysqlCommand.Connection = mysqlConnection;
        }

        // Dispose
        public void Dispose()
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
            mysqlTransaction.Commit();
        }

        // Rollback transaction
        public void Rollback()
        {
            mysqlTransaction.Rollback();
        }

        // Add data to table
        public long AddRow(string database, string table, string[] columns, object[] values, string binary_column = null, byte[] binary_data = null, string updateWhere = null)
        {
            string valuetags = "";

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
            mysqlCommand.ExecuteNonQuery();

            mysqlCommand.Parameters.Clear();

            return mysqlCommand.LastInsertedId;
        }

        // Add data using Column & Data class
        public long AddRow(string database, string table, List<ColumnAndData> listColData, string updateWhere = null)
        {
            return AddRow(database, table, listColData.Select(n => n.columnName).ToArray(), listColData.Select(n => n.dataValue).ToArray(), updateWhere: updateWhere);

        }
        public long insertorupdate(string strQurey)
        {
            mysqlCommand.CommandText = strQurey;
            mysqlCommand.ExecuteNonQuery();
            return mysqlCommand.LastInsertedId;
        }
        // Sends a query to the database
        public void SendQuery(string query)
        {
            mysqlCommand.CommandText = query;
            mysqlCommand.ExecuteNonQuery();
        }

        // Returns object
        public object GetObject(string query)
        {
            mysqlCommand.CommandText = query;
            return mysqlCommand.ExecuteScalar();
        }

        // Returns signed integer
        public int GetInt(string query)
        {
            return int.Parse(GetObject(query).ToString());
        }

        // Returns unsigned integer
        public uint GetUint(string query)
        {
            return uint.Parse(GetObject(query).ToString());
        }

        // Returns string
        public string GetString(string query)
        {
            return GetObject(query).ToString();
        }

        // Returns datatable
        public DataTable GetTable(string query)
        {
            using (DataSet ds = new DataSet())
            {
                using (MySqlDataAdapter _adapter = new MySqlDataAdapter(query, mysqlConnection))
                    _adapter.Fill(ds, "map");
                
                return ds.Tables[0];
            }
        }

        public void BulkSend(string database, string table, string column, List<object> listData)
        {
            using (DataTable dataTable = new DataTable())
            {
                dataTable.Columns.Add(column);
                listData.ForEach(n => dataTable.Rows.Add(n));
                BulkSend(database, table, dataTable);
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
}

