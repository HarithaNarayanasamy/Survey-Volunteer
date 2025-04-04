using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Web.UI.WebControls;
using SVCF_DataAccessLayer;
using SVCF_PropertyLayer;


namespace SVCF_BusinessAccessLayer
{
    public class BusinessLayer
    {
        CommonClassFile objcls;
        PALayer objPAL;
        
        #region Constructor
        public BusinessLayer()
        {
            objcls = new CommonClassFile();            
        }
        #endregion

        #region Common Class Methods
        public DataTable GetDataTable(string qry)
        {
            return objcls.SelectTable1(qry);
        }

        public DataSet GetDataSet(string qry)
        {
            return objcls.SelectDataset(qry);
        }

        public int GetScalarDataInt(string qry)
        {
            return objcls.GetScalarInt(qry);
        }
        public string Getstringdaymonth(string date)
        {
            return objcls.Getstringdaymonth(date);
        }
        //public string GetScalarDataStr(string qry)
        //{
        //    return objcls.Retrive_ScalarData(qry);
        //}
        public string Get24HrTime(int hour, int minute, string ToD, double AddTime)
        {
            return objcls.Get24HourTime(hour, minute, ToD, AddTime);
        }

        public string Get12HrTime(int hour, int minute, string ToD)
        {
            return objcls.Get12HourTime(hour, minute, ToD);
        }

        public string sp_gendratedt_key()
        {
            return objcls.sp_gendratedt_key();
        }

        public MySqlConnection OpenConnection()
        {
            return objcls.openConnection();
        }
     

        public DataTable GetCustomersPageWisestore(int aa)
        {
            return objcls.GetCustomersPageWise(aa);
        }

        public List<ListItem> Getlistdata(string query)
        {
            return objcls.Getlistdata(query);
        }
        public List<ListItem> Bindaccloan_list(string query)
        {
            return objcls.Bindaccloan(query);
        }
        public double GetScalarDataDbl(string qry)
        {
            return objcls.GetScalarDble(qry);
        }
        public decimal GetScalarDecimal(string qry)
        {
            return objcls.GetScalarDecimal(qry);
        }
        //public string TostrEvenNull(string qry)
        //{
        //    return objcls.TostringEvenNull(qry);
        //}
        public string TostrEvenNull(string qry)
        {
            return objcls.ToobjstringEvenNull(qry);
        }

        public string TostringEvenNull(string str)        
        {
            return objcls.TostrEvenNull(str);
        }

        public string IndiandyeToMysqlDate(string ddmmyy)
        {
            return objcls.indiandateToMysqlDate(ddmmyy);
        }

        public string ReplaceJnk(string ip)
        {
            return objcls.ReplaceJunk(ip);
        }

        public MySqlCommand GetCmd(string commandText, MySqlConnection connection)
        {
            return objcls.GetCommand(commandText, connection);
        }

        public bool CheckVoucher_Exist(int vno, int bid)
        {
            return objcls.CheckVCExist(vno, bid);
        }
        //public bool GetScalarDataBool(string qry)
        //{
        //    return objcls.Retrive_Scalarbool(qry);
        //}

        //public DataSet GetScalarDataDSet(string qry)
        //{
        //    return objcls.Retrive_DataSet(qry);
        //}

        //public List<string> GetListItem(string qry)
        //{
        //    return objcls.GetCommonList(qry);
        //}

        public int GetInsertItem(string qry)
        {
            return objcls.InsertOrUpdateorDelete(qry);
        }

        public void ExecuteQuery(string qry)
        {
            objcls.ExecuteQry(qry);
        }

        //public object GetChangeDate(string qry)
        //{
        //    return objcls.changedateformat(qry);
        //}



        public object GetChangeDatFormat(DateTime dt, int bln)
        {
            return objcls.changeformat(dt, bln);
        }

        public string MySQLEscapeString(string str)
        {
            return objcls.MySQLEscapeString1(str);
        }
        public DataTable RetrieveDtable_SP(int gpid, int SBranchID, int SParentID, string SChoosenDate, string spname)
        {
            return objcls.RetrieveDt_SP(gpid, SBranchID, SParentID, SChoosenDate, spname);
        }
        public DataTable RetrieveDtable_TASP(int gpid, int SBranchID, int SHeadID, string SChoosenDate, string spname)
        {
            return objcls.RetrieveDt_TASP(gpid, SBranchID, SHeadID, SChoosenDate, spname);
        }

        public DataTable RetrieveVHeads(int SBranchID, string spname)
        {
            return objcls.RetVoucherHeads(SBranchID, spname);
        }

        //public DataTable RetVHeads(int SBranchID, string spname,string srvalue)
        //{
        //    return objcls.RetVrHeads(SBranchID, spname, srvalue);
        //}

        public MySqlDataReader ExecuteReader(string cmdText)
        {
            return objcls.ExecuteRdr(cmdText);
        }

        public void fillBankHd(DropDownList ddl)
        {
            objcls.fillBnkHd(ddl);
        }

        public string indiandateToMysqlDate(string ddmmyy)
        {
           return objcls.indiandtToMysqlDate(ddmmyy);
        }

        public string ToobjectstrEvenNull(object str)
        {
            return objcls.ToobjstrEvenNull(str);
        }
        //public string MySQLEscapeString(string usString)
        //{
        //    return objcls.MySQLEscapeString1(usString);
        //}

        public object changedateformat(DateTime dt, int bln)
        {
            return objcls.changeformat(dt, bln);
        }

        public string DateConversion_DtNtDateToMySQLDate(string shortDate)
        {
            return objcls.DateConversion_DotNetDateToMySQLDate(shortDate);
        }

        public string DtSlConv_DotNetDateToMySQLDate(DateTime shortDate)
        {
            return objcls.DtSlashConv_DotNetDateToMySQLDate(shortDate);
        }

        public string DteConversion_MySQLDateToDotNetDate(string shortDate)
        {
            return objcls.DateConversion_MySQLDateToDotNetDate(shortDate);
        }

        public string DtslConv_DotNetDateToIndian(DateTime shortDate)
        {
            return objcls.DtslashConv_DotNetDateToIndian(shortDate);
        }

        public string DtSlashConv_DotNetDateToIndian(string shortDate)
        {
            return objcls.DtSlConversion_DotNetDateToIndian(shortDate);
        }

        public string DteConv_IndianToDotNet(string shortDate)
        {
            return objcls.DteConversion_IndianToDotNet(shortDate);
        }

        public string DtCnv_MysqlDateToIndian(string shortDate)
        {
            return objcls.DtConv_MysqlDateToIndian(shortDate);
        }
        
        public string DateConv_MysqlDateToIndian(DateTime shortDate)
        {
            return objcls.DtConv_MysqlDateToIndian(shortDate);
        }

        //public void SetParamFormulaeField(CrystalDecisions.CrystalReports.Engine.ReportDocument cr, string paramName, string paramValue)
        //{
        //    objcls.SetParamValueFormulaeField(cr, paramName, paramValue);
        //}

        public string ConvertToIndCurrency(string fare)
        {
           return objcls.ConvertToIndianCurrency(fare);
        }

        public Dictionary<string, string> CmnList(string query)
        {
            return objcls.CommonList(query);
        }

        public List<ListItem> BindTRDD_List(string query)
        {
            return objcls.BindTRDD(query);
        }


        public List<ListItem> BindDD_List(string query)
        {
            return objcls.BindDD(query);
        }

        public List<ListItem> BindvoucherCD_List(string query)
        {
            return objcls.BindvoucherCD(query);
        }

        public List<ListItem> BindVoucherHeads(int Branchid,string spname)
        {
            return objcls.BinVHeads(Branchid, spname);
        }

        public List<ListItem> BindTokenist(string query)
        {
            return objcls.BindChitToken(query);
        }

        public List<ListItem> BindGrpList(string query)
        {
            return objcls.BindChitGrp(query);
        }        

        public string GetSingleValue(string cmdText)
        {
            return objcls.GetSingleValue1(cmdText);
        }

        public List<string> RetrveList(string query)
        {
            return objcls.RetreiveList(query);
        }

        public List<ListItem> BndTkn(string query)
        {
            return objcls.BindToken(query);
        }

        public List<ListItem> BindMemname(string query)
        {
            return objcls.BindMembername(query);
        }

        public MySqlDataReader ExReader(string query)
        {
            return objcls.ExecuteReader(query);
        }

        public string NumberToText(int number)
        {
            return objcls.NumberToText(number);
        }
        public List<ListItem> GetCustomers()
        {
            return objcls.GetCustomers();
        }


        public List<ListItem> GetCustomersdr()
        {
            return objcls.GetCustomersdr();
        }


        public List<ListItem> GetDropdownVMdr(int headiddr, int branchiddr)
        {
            return objcls.GetDropdownVMdr(headiddr, branchiddr);
        }

        public List<ListItem> GetDropdownVM(int headid, int branchid)
        {
            return objcls.GetDropdownVM(headid, branchid);
        }


        public string SendpasswordMail(string email)
        {
            return objcls.SendPassword(email);
        }
        public void Disposeconnection()
        {
            objcls.Dispose();
        }
        public List<string> BindAuctionDetails(string query)
        {
            return objcls.BindAuctionDetails(query);
        }

        public double GetInstallment(int GroupId, int HeadId, DateTime ActionDate, double SumAmount)
        {
            return objcls.GetInstallment(GroupId, HeadId, ActionDate, SumAmount);
        }

        public void Updatemembertogroup_st(string mname, string Bid, string Maddress, string Mid, int Head_id, string Groupid)
        {
            objcls.Updatemembertogroup(mname, Bid, Maddress, Mid, Head_id, Groupid);

        }
        #endregion

        #region VouchePage
        public List<ListItem> Cr_srch(int BranchID, string spname, string srchtxt)
        {
            return objcls.Cr_srch(BranchID, spname, srchtxt);
        }

        public List<ListItem> Credit_srch(int BranchID, string srchtxt)
        {
            return objcls.Credit_srch(BranchID, srchtxt);
        }

        public List<ListItem> DefaultList_Voucher(int BranchID)
        {
            return objcls.DefaultList_Voucher(BranchID);
        }



        #endregion


    }
}
