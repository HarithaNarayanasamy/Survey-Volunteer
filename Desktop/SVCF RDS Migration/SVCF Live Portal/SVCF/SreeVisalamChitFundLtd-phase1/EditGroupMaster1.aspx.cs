using System;
using System.Data;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.Globalization;
using DevExpress.Web.ASPxGridView;
using SVCF_BusinessAccessLayer;
using SVCF_TransactionLayer;
using SVCF_DataAccessLayer;
using log4net;
using log4net.Config;
using DevExpress.Web.ASPxEditors;

namespace SreeVisalamChitFundLtd_phase1
{
    public partial class WebForm7 : System.Web.UI.Page
    {
        #region VarDeclaration
        BusinessLayer balayer = new BusinessLayer();
        TransactionLayer tranlayer = new TransactionLayer();
        #endregion
        string userinfo = "";
        string qry = "";
        string usrRole = "";

        ILog logger = log4net.LogManager.GetLogger(typeof(WebForm7));

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                userinfo = HttpContext.Current.User.Identity.Name;
                qry = "select rs.name from roles as rs inner join rights as rt on (rt.roleid=rs.id) where memberid=" + userinfo + "";
                usrRole = balayer.GetSingleValue(qry);
                if (usrRole == "Administrator")
                {
                    foreach (GridViewColumn column in gridBranch.Columns)
                    {
                        if (column is GridViewDataColumn)
                        {
                            ((GridViewDataColumn)column).Settings.AutoFilterCondition = AutoFilterCondition.Contains;
                        }
                    }
                }
                else
                {
                    Response.Redirect("Home.aspx", false);
                }              
            }
            select();
            gridBranch.DataBind();
        }
        protected void select()
        {
            DataSourceEmployee.ConnectionString = CommonClassFile.ConnectionString;
            DataSourceEmployee.SelectCommand = @"SELECT BranchID,GROUPNO,PSOOrderNo,cast(DATE_FORMAT(PSOOrderDate, '%d/%m/%Y') as char) as PSOOrderDate,PSODROffice,ChitAgreementNo,ChitAgreementYear,cast(DATE_FORMAT(AgreementDate, '%d/%m/%Y') as char) as AgreementDate,ChitValue,Commission_ID,ChitPeriod,ChitCategory,cast(DATE_FORMAT(ChitStartDate, '%d/%m/%Y') as char) as ChitStartDate,cast(DATE_FORMAT(ChitEndDate, '%d/%m/%Y') as char) as ChitEndDate,NoofMembers,cast(DATE_FORMAT(AuctionDate, '%d/%m/%Y') as char) as AuctionDate,cast( DATE_FORMAT( AuctionTime ,  '%h:%i %p' ) as char) as AuctionTime,TIME_FORMAT(AuctionEndTime,'%h:%i %p') as AuctionEndTime,SDP_FDRNO,SDP_Bank,SDP_BankPlace,cast(DATE_FORMAT(PriorIntimationDate, '%d/%m/%Y') as char) as PriorIntimationDate,cast(DATE_FORMAT(SDP_Commencement, '%d/%m/%Y') as char) as SDP_Commencement,cast(DATE_FORMAT(SDP_Maturity, '%d/%m/%Y') as char) as SDP_Maturity,SDP_RateofInterest,SDP_PeriodinMonths,SDP_Amount,Head_Id FROM svcf.groupmaster where IsFinished=0 and BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + "";

        }
        protected void gridBranch_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            userinfo = HttpContext.Current.User.Identity.Name;
            qry = "select rs.name from roles as rs inner join rights as rt on (rt.roleid=rs.id) where memberid=" + userinfo + "";
            usrRole = balayer.GetSingleValue(qry);
            if (usrRole == "Administrator")
            {
                qry = "";
                DataTable getgrpdt = new DataTable();
                qry = "select * from voucher where chitgroupid=" + e.Keys["Head_Id"] + "";
                getgrpdt = balayer.GetDataTable(qry);

                //Delete group if there is not payment entry in voucher
                if (getgrpdt.Rows.Count <= 0)
                {
                    DataTable deldt = balayer.GetDataTable("select NodeID, max(digits(Node)), Node from headstree where ParentID=" + e.Keys["Head_Id"] + "");
                    string aaaaaaa = deldt.Rows[0]["Node"].ToString();
                    int ddddd = balayer.GetInsertItem("delete from headstree where Node='" + aaaaaaa + "'");
                    balayer.GetInsertItem("delete from headstree where NodeID=" + e.Keys["Head_Id"]);
                    balayer.GetInsertItem("delete from groupmaster where Head_Id=" + e.Keys["Head_Id"]);
                    balayer.GetInsertItem("delete from auctiondetails where GroupID=" + e.Keys["Head_Id"]);
                    balayer.GetInsertItem("delete from membertogroupmaster where GroupID=" + e.Keys["Head_Id"]);
                    balayer.GetInsertItem("delete from voucher where ChitGroupId=" + e.Keys["Head_Id"] + "");
                    balayer.GetInsertItem("delete from trans_payment where ChitGroupID=" + e.Keys["Head_Id"] + "");
                    balayer.GetInsertItem("delete FROM svcf.receiptmaster where GroupID=" + e.Keys["Head_Id"] + "");
                    balayer.GetInsertItem("delete FROM svcf.transfer_approval where ChitGroup=" + e.Keys["Head_Id"] + "");
                    ASPxGridView grid = (sender as ASPxGridView);
                    select();
                    grid.DataBind();
                    e.Cancel = true;
                }
            }
            logger.Info("EditGroupMaster1.aspx - gridBranch_RowDeleting():  Completed: " + DateTime.Now + " by: " + Convert.ToString(Session["UserName"]) + "");
        }
        protected void gridBranch_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        {

        }
        void AddError(Dictionary<GridViewColumn, string> errors, GridViewColumn column, string errorText)
        {
            if (errors.ContainsKey(column)) return;
            errors[column] = errorText;
        }

        protected void gridBranch_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            System.Globalization.DateTimeFormatInfo usDtfi = new System.Globalization.CultureInfo("en-NZ", false).DateTimeFormat;
            string PSOOrderDate1 = e.NewValues["PSOOrderDate"].ToString().Replace(" 00:00:00", "");
            DateTime PSOOrderDate2 = DateTime.Parse(PSOOrderDate1, usDtfi);
            string AgreementDate1 = e.NewValues["AgreementDate"].ToString().Replace(" 00:00:00", "");
            DateTime AgreementDate2 = DateTime.Parse(AgreementDate1, usDtfi);
            string ChitStartDate1 = e.NewValues["ChitStartDate"].ToString().Replace(" 00:00:00", "");
            DateTime ChitStartDate2 = DateTime.Parse(ChitStartDate1, usDtfi);
            string ChitEndDate1 = e.NewValues["ChitEndDate"].ToString().Replace(" 00:00:00", "");
            DateTime ChitEndDate2 = DateTime.Parse(ChitEndDate1, usDtfi);
            string AuctionDate1 = e.NewValues["AuctionDate"].ToString().Replace(" 00:00:00", "");
            DateTime AuctionDate2 = DateTime.Parse(AuctionDate1, usDtfi);
            string SDP_Commencement1 = e.NewValues["SDP_Commencement"].ToString().Replace(" 00:00:00", "");
            DateTime SDP_Commencement2 = DateTime.Parse(SDP_Commencement1, usDtfi);
            string SDP_Maturity1 = e.NewValues["SDP_Maturity"].ToString().Replace(" 00:00:00", "");
            DateTime SDP_Maturity2 = DateTime.Parse(SDP_Maturity1, usDtfi);
            System.Globalization.DateTimeFormatInfo usDtfi1 = new System.Globalization.CultureInfo("en-GB", false).DateTimeFormat;
            //string AuctionTime1 = e.NewValues["AuctionTime"].ToString().Replace("0000/00/00 ","");
            //string AuctionTime3 = e.NewValues["AuctionTime"].ToString();
            //DateTime AuctionTime2 = DateTime.Parse(AuctionTime1,usDtfi1);
            string AuctionTime1 = e.NewValues["AuctionTime"].ToString().Replace("0000/00/00 ", "");
            DateTime AuctionTime2 = DateTime.Parse(AuctionTime1, usDtfi1);
            string Auctionendtime = e.NewValues["AuctionEndTime"].ToString().Replace("000/00/00", "");
            DateTime AuctionendTime2 = DateTime.Parse(Auctionendtime, usDtfi1);
            string chitcategory = "";
            chitcategory = e.NewValues["ChitCategory"].ToString();
            chitcategory = chitcategory.ToUpper();
            if (chitcategory == "MONTHLY")
            {

                ///Summary
                ///keerthana
                ///Change Date:10/08/2018 update ChitAgreementYear 
                balayer.GetInsertItem(@"UPDATE svcf.groupmaster SET PSOOrderNo = '" + balayer.MySQLEscapeString(balayer.ToobjectstrEvenNull( e.NewValues["PSOOrderNo"])) + "', PSOOrderDate = '" + PSOOrderDate2.Date.ToString("yyyy/MM/dd") + "', PSODROffice = '" + balayer.MySQLEscapeString(balayer.ToobjectstrEvenNull( e.NewValues["PSODROffice"])) + "', ChitAgreementNo ='" + balayer.MySQLEscapeString(balayer.ToobjectstrEvenNull( e.NewValues["ChitAgreementNo"])) + "', AgreementDate = '" + AgreementDate2.Date.ToString("yyyy/MM/dd") + "',ChitAgreementYear = '" + AgreementDate2.Year + "',ChitValue= " + e.NewValues["ChitValue"] + ",Commission_ID='" + e.NewValues["Commission_ID"]+ "',ChitPeriod=" + e.NewValues["ChitPeriod"] + ",ChitCategory='" + e.NewValues["ChitCategory"] + "',ChitStartDate='" + ChitStartDate2.Date.ToString("yyyy/MM/dd") + "',ChitEndDate='" + ChitEndDate2.Date.ToString("yyyy/MM/dd") + "',NoofMembers=" + e.NewValues["NoofMembers"] + ",AuctionDate='" + AuctionDate2.Date.ToString("yyyy/MM/dd") + "',AuctionTime='" + AuctionTime2.ToString("HH:mm:ss") + "',AuctionEndTime='" + AuctionendTime2.ToString("HH:mm:ss") + "',SDP_FDRNO='" +  balayer.MySQLEscapeString(balayer.ToobjectstrEvenNull( e.NewValues["SDP_FDRNO"] )) + "',SDP_Bank='" + balayer.MySQLEscapeString(balayer.ToobjectstrEvenNull( e.NewValues["SDP_Bank"])) + "',SDP_BankPlace='" + balayer.MySQLEscapeString(balayer.ToobjectstrEvenNull( e.NewValues["SDP_BankPlace"])) + "',SDP_RateofInterest=" + e.NewValues["SDP_RateofInterest"] + ",SDP_PeriodinMonths=" + e.NewValues["SDP_PeriodinMonths"] + ",SDP_Amount=" + e.NewValues["SDP_Amount"] + ",SDP_Commencement='" + SDP_Commencement2.Date.ToString("yyyy/MM/dd") + "',SDP_Maturity='" + SDP_Maturity2.Date.ToString("yyyy/MM/dd") + "' WHERE Head_Id=" + e.Keys["Head_Id"]);
                ///Summary
                ///Change Date:10/08/2018 update ChitAgreementYear 
                DataTable GroupNode = balayer.GetDataTable("SELECT Node FROM svcf.headstree where NodeID=" + e.Keys["Head_Id"] + "");
                DataTable HeadNode = balayer.GetDataTable("select NodeID, max(digits(Node)), Node from headstree where ParentID=" + e.Keys["Head_Id"] + "");
                string LastNode = GroupNode.Rows[0]["Node"].ToString();
                string LastFirstNode=HeadNode.Rows[0]["Node"].ToString();
                string LastParent = HeadNode.Rows[0]["NodeID"].ToString();
                //Commented on 03.04.2017
                //balayer.GetInsertItem(@"update headstree set Node='" + LastNode + "/" + e.NewValues["NoofMembers"] + "' where Node='" + LastFirstNode + "'");
                //balayer.GetInsertItem(@"update headstree set Node='" + LastNode + "/" + e.NewValues["NoofMembers"] + "',TreeHint='5,40," + e.Keys["Head_Id"] + "," + LastParent + "' where Node='" + LastNode + "/" + e.NewValues["NoofMembers"] + "' and ParentID=" + e.Keys["Head_Id"] + "");
                //balayer.GetInsertItem(@"update membertogroupmaster set GrpMemberID='" + LastNode + "/" + e.NewValues["NoofMembers"] + "' where Head_Id="+LastParent+"");
                //Commented on 03.04.2017

                //balayer.GetInsertItem("delete from auctiondetails where GroupID=" + e.Keys["Head_Id"]);
                //DataTable dtReport = balayer.GetDataTable("SELECT NoofMembers,ChitPeriod,AuctionDate,ChitValue FROM `groupmaster` where Head_Id=" + e.Keys["Head_Id"] + "");
                //int numberofDraw = int.Parse(balayer.ToobjectstrEvenNull(dtReport.Rows[0]["NoofMembers"]));
                //decimal currDueAmt = Convert.ToDecimal(balayer.ToobjectstrEvenNull(dtReport.Rows[0]["ChitValue"])) / numberofDraw;
                //decimal nextDueAmt = currDueAmt;
                //int RebidNo = 0;
                //int noofmonths = AuctionDates(e.NewValues["ChitCategory"].ToString());
                //for (int drno = 1; drno <= numberofDraw; drno++)
                //{
                //    Boolean b = Convert.ToBoolean("sunday".Equals(balayer.ToobjectstrEvenNull(AuctionDate2.Date.Day), StringComparison.InvariantCultureIgnoreCase));
                //    if (b == true)
                //    {
                //        AuctionDate2 = AuctionDate2.AddDays(noofmonths);
                //    }
                //    if ((drno != 1) && (drno != 2))
                //    {
                //        int inscmd = balayer.GetInsertItem("insert into auctiondetails(`BranchID`,`GroupID`,`AuctionDate`,`ReBidNO`,`DrawNO`,`IsPrized`) values(" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + "," + e.Keys["Head_Id"] + ",'" + AuctionDate2.Date.ToString("yyyy/MM/dd") + "'," + RebidNo + "," + drno + ",'I')");
                //        AuctionDate2 = AuctionDate2.AddMonths(noofmonths);
                //    }
                //    else if (drno == 1)
                //    {
                //        string fdfdfd = balayer.GetSingleValue("SELECT MemberIDNew FROM svcf.membermaster where BranchId=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and CustomerName='Sree Visalam Chit Fund Ltd'");
                //        int inscmd = balayer.GetInsertItem("insert into auctiondetails(`BranchID`,`GroupID`,`AuctionDate`,`ReBidNO`,`DrawNO`,`PrizedMemberID`,MemberID,`CurrentDueAmount`,`NextDueAmount`,`IsPrized`) values(" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + "," + e.Keys["Head_Id"] + ",'" + AuctionDate2.Date.ToString("yyyy/MM/dd") + "'," + RebidNo + "," + drno + "," + LastParent + "," + fdfdfd + "," + currDueAmt + "," + nextDueAmt + ",'F')");
                //        AuctionDate2 = AuctionDate2.AddMonths(noofmonths);
                //    }
                //    else if (drno == 2)
                //    {
                //        int inscmd = balayer.GetInsertItem("insert into auctiondetails(`BranchID`,`GroupID`,`AuctionDate`,`ReBidNO`,`DrawNO`,`CurrentDueAmount`,`IsPrized`) values(" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + "," + e.Keys["Head_Id"] + ",'" + AuctionDate2.Date.ToString("yyyy/MM/dd") + "'," + RebidNo + "," + drno + "," + currDueAmt + ",'I')");
                //        AuctionDate2 = AuctionDate2.AddMonths(noofmonths);
                //    }
                //}
            }
            else if (chitcategory == "TRIMONTHLY")
            {
                ///Summary
                ///keerthana
                ///Change Date:10/08/2018 update ChitAgreementYear 
                balayer.GetInsertItem("UPDATE svcf.groupmaster SET PSOOrderNo = '" + balayer.MySQLEscapeString(balayer.ToobjectstrEvenNull( e.NewValues["PSOOrderNo"])) + "', PSOOrderDate = '" + PSOOrderDate2.Date.ToString("yyyy/MM/dd") + "', PSODROffice = '" + balayer.MySQLEscapeString(balayer.ToobjectstrEvenNull( e.NewValues["PSODROffice"])) + "', ChitAgreementNo ='" + balayer.MySQLEscapeString(balayer.ToobjectstrEvenNull( e.NewValues["ChitAgreementNo"])) + "', AgreementDate = '" + AgreementDate2.Date.ToString("yyyy/MM/dd") + "',ChitAgreementYear = '" + AgreementDate2.Year + "',ChitValue= " + e.NewValues["ChitValue"] + ",Commission_ID='" + e.NewValues["Commission_ID"]+ "',ChitPeriod=" + e.NewValues["ChitPeriod"] + ",ChitCategory='" + e.NewValues["ChitCategory"] + "',ChitStartDate='" + ChitStartDate2.Date.ToString("yyyy/MM/dd") + "',ChitEndDate='" + ChitEndDate2.Date.ToString("yyyy/MM/dd") + "',NoofMembers=" + e.NewValues["NoofMembers"] + ",AuctionDate='" + AuctionDate2.Date.ToString("yyyy/MM/dd") + "',AuctionTime='" + AuctionTime2.ToString("HH:mm;ss") + "',AuctionEndTime='" + AuctionendTime2.ToString("HH:mm:ss") + "',SDP_FDRNO='" + balayer.MySQLEscapeString(balayer.ToobjectstrEvenNull( e.NewValues["SDP_FDRNO"])) + "',SDP_Bank='" + balayer.MySQLEscapeString(balayer.ToobjectstrEvenNull( e.NewValues["SDP_Bank"] )) + "',SDP_BankPlace='" +  balayer.MySQLEscapeString(balayer.ToobjectstrEvenNull( e.NewValues["SDP_BankPlace"])) + "',SDP_RateofInterest=" + e.NewValues["SDP_RateofInterest"] + ",SDP_PeriodinMonths=" + e.NewValues["SDP_PeriodinMonths"] + ",SDP_Amount=" + e.NewValues["SDP_Amount"] + ",SDP_Commencement='" + SDP_Commencement2.Date.ToString("yyyy/MM/dd") + "',SDP_Maturity='" + SDP_Maturity2.Date.ToString("yyyy/MM/dd") + "' WHERE Head_Id=" + e.Keys["Head_Id"]);
                ///Summary
                ///keerthana
                ///Change Date:10/08/2018 update ChitAgreementYear 
                DataTable GroupNode = balayer.GetDataTable("SELECT Node FROM svcf.headstree where NodeID=" + e.Keys["Head_Id"] + "");
                DataTable HeadNode = balayer.GetDataTable("select NodeID, max(digits(Node)), Node from headstree where ParentID=" + e.Keys["Head_Id"] + "");
                string LastNode = GroupNode.Rows[0]["Node"].ToString();
                string LastFirstNode = HeadNode.Rows[0]["Node"].ToString();
                string LastParent = HeadNode.Rows[0]["NodeID"].ToString();
                //Commented on 03.04.2017
                //balayer.GetInsertItem("update headstree set Node='" + LastNode + "/" + e.NewValues["NoofMembers"] + "' where Node='" + LastFirstNode + "'");
                //balayer.GetInsertItem("update headstree set Node='" + LastNode + "/" + e.NewValues["NoofMembers"] + "',TreeHint='5,42," + e.Keys["Head_Id"] + "," + LastParent + "' where Node='" + LastNode + "/" + e.NewValues["NoofMembers"] + "' and ParentID=" + e.Keys["Head_Id"] + "");
                //balayer.GetInsertItem("update membertogroupmaster set GrpMemberID='" + LastNode + "/" + e.NewValues["NoofMembers"] + "' where Head_Id=" + LastParent + "");
                //Commented on 03.04.2017




                //balayer.GetInsertItem("delete from auctiondetails where GroupID=" + e.Keys["Head_Id"]);
                //DataTable dtReport = balayer.GetDataTable("SELECT NoofMembers,ChitPeriod,AuctionDate,ChitValue FROM `groupmaster` where Head_Id=" + e.Keys["Head_Id"] + "");
                //int numberofDraw = int.Parse(balayer.ToobjectstrEvenNull(dtReport.Rows[0]["NoofMembers"]));
                //decimal currDueAmt = Convert.ToDecimal(balayer.ToobjectstrEvenNull(dtReport.Rows[0]["ChitValue"])) / numberofDraw;
                //decimal nextDueAmt = currDueAmt;
                //int RebidNo = 0;
                //int noofmonths = AuctionDates(e.NewValues["ChitCategory"].ToString());
                //for (int drno = 1; drno <= numberofDraw; drno++)
                //{
                //    Boolean b = Convert.ToBoolean("sunday".Equals(balayer.ToobjectstrEvenNull(AuctionDate2.Date.Day), StringComparison.InvariantCultureIgnoreCase));
                //    if (b == true)
                //    {
                //        AuctionDate2 = AuctionDate2.AddDays(1);
                //    }
                //    if ((drno != 1) && (drno != 2))
                //    {
                //        int inscmd = balayer.GetInsertItem("insert into auctiondetails(`BranchID`,`GroupID`,`AuctionDate`,`ReBidNO`,`DrawNO`) values(" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + "," + e.Keys["Head_Id"] + ",'" + AuctionDate2.Date.ToString("yyyy/MM/dd") + "'," + RebidNo + "," + drno + ")");
                //        AuctionDate2 = AuctionDate2.AddDays(10);
                //    }
                //    else if (drno == 1)
                //    {
                //        string fdfdfd = balayer.GetSingleValue("SELECT min(MemberIDNew) FROM svcf.membermaster where BranchId=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and CustomerName='Sree Visalam Chit Fund Ltd'");
                //        int inscmd = balayer.GetInsertItem("insert into auctiondetails(`BranchID`,`GroupID`,`AuctionDate`,`ReBidNO`,`DrawNO`,`PrizedMemberID`,MemberID,`CurrentDueAmount`,`NextDueAmount`) values(" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + "," + e.Keys["Head_Id"] + ",'" + AuctionDate2.Date.ToString("yyyy/MM/dd") + "'," + RebidNo + "," + drno + "," + LastParent + "," + fdfdfd + "," + currDueAmt + "," + nextDueAmt + ")");
                //        AuctionDate2 = AuctionDate2.AddDays(10);
                //    }
                //    else if (drno == 2)
                //    {
                //        int inscmd = balayer.GetInsertItem("insert into auctiondetails(`BranchID`,`GroupID`,`AuctionDate`,`ReBidNO`,`DrawNO`,`CurrentDueAmount`) values(" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + "," + e.Keys["Head_Id"] + ",'" + AuctionDate2.Date.ToString("yyyy/MM/dd") + "'," + RebidNo + "," + drno + "," + currDueAmt + ")");
                //        AuctionDate2 = AuctionDate2.AddDays(10);
                //    }
                //}
            }
            else if (chitcategory == "FORTNIGHTLY")
            {
                //balayer.GetInsertItem("update headstree set ParentID=41,TreeHint='5,41," + e.Keys["Head_Id"] + "' where NodeID=" + e.Keys["Head_Id"] + " ");
                ///Summary
                ///keerthana
                ///Change Date:10/08/2018 update ChitAgreementYear 
                balayer.GetInsertItem("UPDATE svcf.groupmaster SET PSOOrderNo = '" + balayer.MySQLEscapeString(balayer.ToobjectstrEvenNull( e.NewValues["PSOOrderNo"])) + "', PSOOrderDate = '" + PSOOrderDate2.Date.ToString("yyyy/MM/dd") + "', PSODROffice = '" + balayer.MySQLEscapeString(balayer.ToobjectstrEvenNull( e.NewValues["PSODROffice"])) + "', ChitAgreementNo ='" + balayer.MySQLEscapeString(balayer.ToobjectstrEvenNull( e.NewValues["ChitAgreementNo"])) + "', AgreementDate = '" + AgreementDate2.Date.ToString("yyyy/MM/dd") + "',ChitAgreementYear = '" + AgreementDate2.Year + "',ChitValue= " + e.NewValues["ChitValue"] + ",Commission_ID='" + e.NewValues["Commission_ID"]+ "',ChitPeriod=" + e.NewValues["ChitPeriod"] + ",ChitCategory='" + e.NewValues["ChitCategory"] + "',ChitStartDate='" + ChitStartDate2.Date.ToString("yyyy/MM/dd") + "',ChitEndDate='" + ChitEndDate2.Date.ToString("yyyy/MM/dd") + "',NoofMembers=" + e.NewValues["NoofMembers"] + ",AuctionDate='" + AuctionDate2.Date.ToString("yyyy/MM/dd") + "',AuctionTime='" + AuctionTime2.ToString("HH:mm;ss") + "',AuctionEndTime='" + AuctionendTime2.ToString("HH:mm:ss") + "',SDP_FDRNO='" + balayer.MySQLEscapeString( balayer.ToobjectstrEvenNull( e.NewValues["SDP_FDRNO"] )) + "',SDP_Bank='" + balayer.MySQLEscapeString(balayer.ToobjectstrEvenNull( e.NewValues["SDP_Bank"])) + "',SDP_BankPlace='" + balayer.MySQLEscapeString(balayer.ToobjectstrEvenNull( e.NewValues["SDP_BankPlace"])) + "',SDP_RateofInterest=" + e.NewValues["SDP_RateofInterest"] + ",SDP_PeriodinMonths=" + e.NewValues["SDP_PeriodinMonths"] + ",SDP_Amount=" + e.NewValues["SDP_Amount"] + ",SDP_Commencement='" + SDP_Commencement2.Date.ToString("yyyy/MM/dd") + "',SDP_Maturity='" + SDP_Maturity2.Date.ToString("yyyy/MM/dd") + "' WHERE Head_Id=" + e.Keys["Head_Id"]);
                ///Summary
                ///keerthana
                ///Change Date:10/08/2018 update ChitAgreementYear 
                DataTable GroupNode = balayer.GetDataTable("SELECT Node FROM svcf.headstree where NodeID=" + e.Keys["Head_Id"] + "");
                DataTable HeadNode = balayer.GetDataTable("select NodeID, max(digits(Node)), Node from headstree where ParentID=" + e.Keys["Head_Id"] + "");
                string LastNode = GroupNode.Rows[0]["Node"].ToString();
                string LastFirstNode = HeadNode.Rows[0]["Node"].ToString();
                string LastParent = HeadNode.Rows[0]["NodeID"].ToString();
                //Commented on 03.04.2017
                //balayer.GetInsertItem("update headstree set Node='" + LastNode + "/" + e.NewValues["NoofMembers"] + "' where Node='" + LastFirstNode + "'");
                //balayer.GetInsertItem("update headstree set Node='" + LastNode + "/" + e.NewValues["NoofMembers"] + "',TreeHint='5,41," + e.Keys["Head_Id"] + "," + LastParent + "' where Node='" + LastNode + "/" + e.NewValues["NoofMembers"] + "' and ParentID=" + e.Keys["Head_Id"] + "");
                //balayer.GetInsertItem("update membertogroupmaster set GrpMemberID='" + LastNode + "/" + e.NewValues["NoofMembers"] + "' where Head_Id=" + LastParent + "");
                //Commented on 03.04.2017

                //balayer.GetInsertItem("delete from auctiondetails where GroupID=" + e.Keys["Head_Id"]);
                //DataTable dtReport = balayer.GetDataTable("SELECT NoofMembers,ChitPeriod,AuctionDate,ChitValue FROM `groupmaster` where Head_Id=" + e.Keys["Head_Id"] + "");
                //int numberofDraw = int.Parse(balayer.ToobjectstrEvenNull(dtReport.Rows[0]["NoofMembers"]));
                //decimal currDueAmt = Convert.ToDecimal(balayer.ToobjectstrEvenNull(dtReport.Rows[0]["ChitValue"])) / numberofDraw;
                //decimal nextDueAmt = currDueAmt;
                //int RebidNo = 0;
                //int noofmonths = AuctionDates(e.NewValues["ChitCategory"].ToString());

                //for (int drno = 1; drno <= numberofDraw; drno++)
                //{
                //    Boolean b = Convert.ToBoolean("sunday".Equals(balayer.ToobjectstrEvenNull(AuctionDate2.Date.Day), StringComparison.InvariantCultureIgnoreCase));
                //    if (b == true)
                //    {
                //        AuctionDate2 = AuctionDate2.AddDays(1);
                //    }
                //    if ((drno != 1) && (drno != 2))
                //    {
                //        int inscmd = balayer.GetInsertItem("insert into auctiondetails(`BranchID`,`GroupID`,`AuctionDate`,`ReBidNO`,`DrawNO`) values(" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + "," + e.Keys["Head_Id"] + ",'" + AuctionDate2.Date.ToString("yyyy/MM/dd") + "'," + RebidNo + "," + drno + ")");
                //        AuctionDate2 = AuctionDate2.AddDays(15);
                //    }
                //    else if (drno == 1)
                //    {
                //        string fdfdfd = balayer.GetSingleValue("SELECT min(MemberIDNew) FROM svcf.membermaster where BranchId=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and CustomerName='Sree Visalam Chit Fund Ltd'");
                //        int inscmd = balayer.GetInsertItem("insert into auctiondetails(`BranchID`,`GroupID`,`AuctionDate`,`ReBidNO`,`DrawNO`,`PrizedMemberID`,MemberID,`CurrentDueAmount`,`NextDueAmount`) values(" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + "," + e.Keys["Head_Id"] + ",'" + AuctionDate2.Date.ToString("yyyy/MM/dd") + "'," + RebidNo + "," + drno + "," + LastParent + "," + fdfdfd + "," + currDueAmt + "," + nextDueAmt + ")");
                //        AuctionDate2 = AuctionDate2.AddDays(15);
                //    }
                //    else if (drno == 2)
                //    {
                //        int inscmd = balayer.GetInsertItem("insert into auctiondetails(`BranchID`,`GroupID`,`AuctionDate`,`ReBidNO`,`DrawNO`,`CurrentDueAmount`) values(" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + "," + e.Keys["Head_Id"] + ",'" + AuctionDate2.Date.ToString("yyyy/MM/dd") + "'," + RebidNo + "," + drno + "," + currDueAmt + ")");
                //        AuctionDate2 = AuctionDate2.AddDays(15);
                //    }
                //}
            }
            ASPxGridView grid = (sender as ASPxGridView);           
            select();
            grid.DataBind();
            grid.CancelEdit();
            e.Cancel = true;
            logger.Info("EditGroupMaster1.aspx - gridBranch_RowUpdating():  Completed: " + DateTime.Now + " by: " + Convert.ToString(Session["UserName"]) + "");
        }
        protected int AuctionDates(string aaaaa)
        {
            int numofmon;
            if (aaaaa.ToString() == "Monthly")
            {
                numofmon = 1;
                return numofmon;
            }
            else if (aaaaa.ToString() == "Trimonthly")
            {
                numofmon = 3;
                return numofmon;
            }
            else
            {
                return 0;
            }
        }
        protected void gridBranch_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
        {

            ASPxGridView grid = (sender as ASPxGridView);
            if (!grid.IsNewRowEditing)
            {
                grid.DoRowValidation();
            }
        }
    }
}
