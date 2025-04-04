using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SVCF_BusinessAccessLayer;
using SVCF_TransactionLayer;
using log4net;
using log4net.Config;

namespace SreeVisalamChitFundLtd_phase1.UserControl
{
    public partial class WebUserControl1 : System.Web.UI.UserControl
    {
        string query = "";
        string query1 = "";
        BusinessLayer balayer = new BusinessLayer();
        TransactionLayer trn = new TransactionLayer();
        ILog logger = log4net.LogManager.GetLogger(typeof(AppVoucherMoneyCollector));

        string userinfo = "";
        string qry = "";
        string usrRole = "";

        string fDate; string tDate;
        string s = "--Select--";
        string moneycol = "0";

        public void Page_Load(object sender, EventArgs e)
        {
            //LoadData();
            userinfo = HttpContext.Current.User.Identity.Name;
            qry = "select rs.name from roles as rs inner join rights as rt on (rt.roleid=rs.id) where memberid=" + userinfo + "";
            usrRole = balayer.GetSingleValue(qry);

            //ViewState["CurrentData"] = null;    //05/10/2021
        }

        public void LoadData(string moneyCollId, string fromDate, string toDate)
        {
            //commented by bala 24/12/2020
            //query = "SELECT t3.B_Name as BranchName,date_format(t5.ChoosenDate,'%d/%m/%Y') as ApprovedDate,date_format(t4.DateInCheque,'%d/%m/%Y') as ChequeDate,t4.ChequeDDNO, date_format( t1.ChoosenDate,'%d/%m/%Y') " +
            //      "as Choosendate,m1.Node as GrpMemberID,t1.Head_Id,t1.Voucher_No as ReceiptNumber,t1.Amount,t1.Narration as Description,t1.AppReceiptno as 'AppReceiptno' FROM `svcf`.`voucher` as t1 left " +
            //       "join membermaster as t2 on  t1.MemberID=t2.MemberIDNew join branchdetails as t3 on (t3.Head_Id=t1.BranchID) left join headstree as m1 on (m1.NodeID=t1.Head_Id) left join groupmaster as g1 " +
            //       "on (t1.ChitGroupId=g1.Head_Id) left join transbank as t4 on (t1.DualTransactionKey=t4.DualTransactionKey) left join appvoucherapproval as t5 on (t1.DualTransactionKey=t5.DualTransactionKey) where " +
            //       " t1.Trans_Type=1 and t1.ISActive=0 and t1.AppReceiptno <> '' and t1.Voucher_Type='C' and t1.Branchid=" + Session["Branchid"] + " group by t1.Head_Id;";

            ViewState["fromDate"] = fromDate;
            ViewState["toDate"] = toDate;
            ViewState["CurrentData"] = null;    //05/10/2021

            moneycol = moneyCollId;

            if (moneyCollId == "0")
            {
                //query = "SELECT distinct m1.AppReceiptno as AppReceiptno,m2.GrpMemberID as ChitNo,t1.B_Name as BranchName,v1.ChoosenDate as ApprovedDate,date_format(m1.CurrDate,'%d-%m-%Y') as RemittanceDate,m1.Amount,v1.Narration FROM mobileappvoucher AS m1 JOIN membertogroupmaster AS m2 ON (m1.Head_Id = m2.Head_Id) LEFT JOIN branchdetails AS t1 ON (t1.Head_Id = m1.BranchID) left join voucher as v1 on(m1.AppReceiptno=v1.AppReceiptno) WHERE m1.Voucher_Type = 'C' and m1.Series='BCAPP' and m1.IsAccepted=1 and v1.ChoosenDate between '" + balayer.changedateformat(Convert.ToDateTime(fromDate), 2) + "' and '" + balayer.changedateformat(Convert.ToDateTime(toDate), 2) + "' order by ApprovedDate desc;";
                //06/12/2021

                //sivanesan 11-11-2023
                //query = "SELECT distinct m1.AppReceiptno as AppReceiptno,uuid_from_bin(v1.DualTransactionKey) as DualKey,m1.Series,m1.MoneyCollId,m1.B_Group as BranchID,(case when v1.RootID=11 then 'Interest' WHEN m1.B_Group =" + Session["Branchid"] + " THEN m2.GrpMemberID ELSE t1.B_Name end )as ChitNo,m1.Head_Id,t1.B_Name as BranchName,v1.ChoosenDate as ApprovedDate,convert_tz( m1.CurrDate,'+00:00','+05:30') as RemittanceDate,(case when m1.B_Group=" + Session["Branchid"]+ " or v1.RootID=11 then  v1.Amount else 0.00 end )as 'CurrentBranchAmount' ,(case when m1.B_Group<>" + Session["Branchid"]+ " and v1.RootID<>11 then  v1.Amount else 0.00 end )as 'OtherBranchAmount',v1.Narration,m2.MemberName FROM mobileappvoucher AS m1 LEFT JOIN membertogroupmaster AS m2 ON (m1.Head_Id = m2.Head_Id) LEFT JOIN branchdetails AS t1 ON (t1.Head_Id = m1.B_Group) left join voucher as v1 on(m1.AppReceiptno=v1.AppReceiptno) WHERE v1.Voucher_Type = 'C' and m1.Voucher_Type='C' and m1.Series='BCAPP' and m1.IsAccepted=1 and v1.ChoosenDate between '" + balayer.changedateformat(Convert.ToDateTime(fromDate), 2) + "' and '" + balayer.changedateformat(Convert.ToDateTime(toDate), 2) + "' and v1.BranchID=" + Session["Branchid"] + ";";
                //query1 = "SELECT distinct m1.AppReceiptno as AppReceiptno,uuid_from_bin(v1.DualTransactionKey) as DualKey,m1.Series,m1.MoneyCollId,m1.B_Group as BranchID,(case when v1.RootID=11 then 'Interest' WHEN m1.B_Group ="+ Session["Branchid"]+ " THEN m2.GrpMemberID ELSE t1.B_Name end )as ChitNo,m1.Head_Id,t1.B_Name as BranchName,v1.ChoosenDate as ApprovedDate,convert_tz( m1.CurrDate,'+00:00','+05:30') as RemittanceDate,(case when m1.B_Group=" + Session["Branchid"]+ " and v1.RootID<>11 then  v1.Amount else 0.00 end )as 'CurrentBranchAmount' ,(case when m1.B_Group<>" + Session["Branchid"]+ " and v1.RootID<>11 then  v1.Amount else 0.00 end )as 'OtherBranchAmount',v1.Narration,(case when v1.RootID=11 then v1.Amount else 0.00 end) as 'P&L',m2.MemberName FROM mobileappvoucher AS m1 LEFT JOIN membertogroupmaster AS m2 ON (m1.Head_Id = m2.Head_Id) LEFT JOIN branchdetails AS t1 ON (t1.Head_Id = m1.B_Group) left join voucher as v1 on(m1.AppReceiptno=v1.AppReceiptno) WHERE v1.Voucher_Type = 'C' and m1.Voucher_Type='C' and m1.Series='BCAPP' and m1.IsAccepted=1 and v1.ChoosenDate between '" + balayer.changedateformat(Convert.ToDateTime(fromDate), 2) + "' and '" + balayer.changedateformat(Convert.ToDateTime(toDate), 2) + "' and v1.BranchID=" + Session["Branchid"] + ";";
                query = "SELECT distinct m1.AppReceiptno as AppReceiptno,uuid_from_bin(v1.DualTransactionKey) as DualKey,m1.Series,m1.MoneyCollId,m1.BranchID,(case when v1.RootID=11 then 'Interest' WHEN m1.B_Group =" + Session["Branchid"] + " THEN CONCAT(m2.GrpMemberID, ' - ', m2.MemberName) ELSE CONCAT(m2.GrpMemberID, ' - ', m2.MemberName) END) AS ChitNo,m1.Head_Id,t1.B_Name AS BranchName,v1.ChoosenDate as ApprovedDate,convert_tz( m1.CurrDate,'+00:00','+05:30') as RemittanceDate,(case when m1.B_Group=" + Session["Branchid"] + " and  v1.RootID<>11 then  v1.Amount else 0.00 end )as 'CurrentBranchAmount' ,(case when m1.B_Group<>" + Session["Branchid"] + " and v1.RootID<>11 then  v1.Amount else 0.00 end )as 'OtherBranchAmount',(case when v1.RootID=11 then v1.Amount else 0.00 end) AS Interest,v1.Narration,m2.MemberName FROM mobileappvoucher AS m1 LEFT JOIN membertogroupmaster AS m2 ON (m1.Head_Id = m2.Head_Id) LEFT JOIN branchdetails AS t1 ON (t1.Head_Id = m1.BranchID) left join voucher as v1 on(m1.AppReceiptno=v1.AppReceiptno) WHERE v1.Voucher_Type = 'C' and m1.Voucher_Type='C' and m1.Series='BCAPP' and m1.IsAccepted=1 and v1.ChoosenDate between '" + balayer.changedateformat(Convert.ToDateTime(fromDate), 2) + "' and '" + balayer.changedateformat(Convert.ToDateTime(toDate), 2) + "' and v1.BranchID=" + Session["Branchid"] + ";";
                query1 = "SELECT distinct m1.AppReceiptno as AppReceiptno,uuid_from_bin(v1.DualTransactionKey) as DualKey,m1.Series,m1.MoneyCollId,m1.BranchID,(case when v1.RootID=11 then 'Interest' WHEN m1.B_Group =" + Session["Branchid"] + " THEN CONCAT(m2.GrpMemberID, ' - ', m2.MemberName) ELSE CONCAT(m2.GrpMemberID, ' - ', m2.MemberName) END) AS ChitNo,m1.Head_Id,t1.B_Name AS BranchName,v1.ChoosenDate as ApprovedDate,convert_tz( m1.CurrDate,'+00:00','+05:30') as RemittanceDate,(case when m1.B_Group=" + Session["Branchid"] + " and v1.RootID<>11 then  v1.Amount else 0.00 end )as 'CurrentBranchAmount' ,(case when m1.B_Group<>" + Session["Branchid"] + " and v1.RootID<>11 then  v1.Amount else 0.00 end )as 'OtherBranchAmount',(case when v1.RootID=11 then v1.Amount else 0.00 end) AS Interest,v1.Narration,(case when v1.RootID=11 then v1.Amount else 0.00 end) as 'P&L',m2.MemberName FROM mobileappvoucher AS m1 LEFT JOIN membertogroupmaster AS m2 ON (m1.Head_Id = m2.Head_Id) LEFT JOIN branchdetails AS t1 ON (t1.Head_Id = m1.BranchID) left join voucher as v1 on(m1.AppReceiptno=v1.AppReceiptno) WHERE v1.Voucher_Type = 'C' and m1.Voucher_Type='C' and m1.Series='BCAPP' and m1.IsAccepted=1 and v1.ChoosenDate between '" + balayer.changedateformat(Convert.ToDateTime(fromDate), 2) + "' and '" + balayer.changedateformat(Convert.ToDateTime(toDate), 2) + "' and v1.BranchID=" + Session["Branchid"] + ";";


            }
            else
            {

                //sivanesan 11-11-2023
                query = "SELECT distinct m1.AppReceiptno as AppReceiptno,uuid_from_bin(v1.DualTransactionKey) as DualKey,m1.Series,m1.MoneyCollId,m1.BranchID,(case when v1.RootID=11 then 'Interest' WHEN m1.B_Group =" + Session["Branchid"] + " THEN CONCAT(m2.GrpMemberID, ' - ', m2.MemberName) ELSE CONCAT(m2.GrpMemberID, ' - ', m2.MemberName) END) AS ChitNo,m1.Head_Id,t1.B_Name as BranchName,v1.ChoosenDate as ApprovedDate,convert_tz( m1.CurrDate,'+00:00','+05:30') as RemittanceDate,(case when m1.B_Group=" + Session["Branchid"] + " or v1.RootID=11 then  v1.Amount else 0.00 end )as 'CurrentBranchAmount' ,(case when m1.B_Group<>" + Session["Branchid"] + " and v1.RootID<>11 then  v1.Amount else 0.00 end )as 'OtherBranchAmount',(case when v1.RootID=11 then v1.Amount else 0.00 end) AS Interest,v1.Narration,m2.MemberName FROM mobileappvoucher AS m1 LEFT JOIN membertogroupmaster AS m2 ON (m1.Head_Id = m2.Head_Id) LEFT JOIN branchdetails AS t1 ON (t1.Head_Id = m1.BranchID) left join voucher as v1 on(m1.AppReceiptno=v1.AppReceiptno) WHERE v1.Voucher_Type = 'C' and m1.Voucher_Type='C' and m1.Series='BCAPP' and m1.MoneyCollId=" + moneyCollId + " and m1.IsAccepted=1 and v1.ChoosenDate between '" + balayer.changedateformat(Convert.ToDateTime(fromDate), 2) + "' and '" + balayer.changedateformat(Convert.ToDateTime(toDate), 2) + "' and v1.BranchID=" + Session["Branchid"] + " ;";
                query1 = "SELECT distinct m1.AppReceiptno as AppReceiptno,uuid_from_bin(v1.DualTransactionKey) as DualKey,m1.Series,m1.MoneyCollId,m1.BranchID,(case when v1.RootID=11 then 'Interest' WHEN m1.B_Group =" + Session["Branchid"] + " THEN CONCAT(m2.GrpMemberID, ' - ', m2.MemberName) ELSE CONCAT(m2.GrpMemberID, ' - ', m2.MemberName) END) AS ChitNo,m1.Head_Id,t1.B_Name as BranchName,v1.ChoosenDate as ApprovedDate,convert_tz( m1.CurrDate,'+00:00','+05:30') as RemittanceDate,(case when m1.B_Group=" + Session["Branchid"] + " and v1.RootID<>11 then  v1.Amount else 0.00 end )as 'CurrentBranchAmount' ,(case when m1.B_Group<>" + Session["Branchid"] + " and v1.RootID<>11 then  v1.Amount else 0.00 end )as 'OtherBranchAmount',(case when v1.RootID=11 then v1.Amount else 0.00 end) AS Interest,v1.Narration,(case when v1.RootID=11 then v1.Amount else 0.00 end) as 'P&L',m2.MemberName FROM mobileappvoucher AS m1 LEFT JOIN membertogroupmaster AS m2 ON (m1.Head_Id = m1.BranchID) LEFT JOIN branchdetails AS t1 ON (t1.Head_Id = m1.B_Group) left join voucher as v1 on(m1.AppReceiptno=v1.AppReceiptno) WHERE v1.Voucher_Type = 'C' and m1.Voucher_Type='C' and m1.Series='BCAPP' and m1.MoneyCollId=" + moneyCollId + " and m1.IsAccepted=1 and v1.ChoosenDate between '" + balayer.changedateformat(Convert.ToDateTime(fromDate), 2) + "' and '" + balayer.changedateformat(Convert.ToDateTime(toDate), 2) + "' and v1.BranchID=" + Session["Branchid"] + " ;";




                //old code
                //query = "SELECT distinct m1.AppReceiptno as AppReceiptno,uuid_from_bin(v1.DualTransactionKey) as DualKey,m1.Series,m1.MoneyCollId,m1.B_Group as BranchID,(case when v1.RootID=11 then 'Interest' WHEN m1.B_Group =" + Session["Branchid"] + " THEN m2.GrpMemberID ELSE t1.B_Name end )as ChitNo,m1.Head_Id,t1.B_Name as BranchName,v1.ChoosenDate as ApprovedDate,convert_tz( m1.CurrDate,'+00:00','+05:30') as RemittanceDate,(case when m1.B_Group=" + Session["Branchid"] + " or v1.RootID=11 then  v1.Amount else 0.00 end )as 'CurrentBranchAmount' ,(case when m1.B_Group<>" + Session["Branchid"] + " and v1.RootID<>11 then  v1.Amount else 0.00 end )as 'OtherBranchAmount',v1.Narration,m2.MemberName FROM mobileappvoucher AS m1 LEFT JOIN membertogroupmaster AS m2 ON (m1.Head_Id = m2.Head_Id) LEFT JOIN branchdetails AS t1 ON (t1.Head_Id = m1.B_Group) left join voucher as v1 on(m1.AppReceiptno=v1.AppReceiptno) WHERE v1.Voucher_Type = 'C' and m1.Voucher_Type='C' and m1.Series='BCAPP' and m1.MoneyCollId=" + moneyCollId + " and m1.IsAccepted=1 and v1.ChoosenDate between '" + balayer.changedateformat(Convert.ToDateTime(fromDate), 2) + "' and '" + balayer.changedateformat(Convert.ToDateTime(toDate), 2) + "' and v1.BranchID=" + Session["Branchid"] + " ;";
                //query1 = "SELECT distinct m1.AppReceiptno as AppReceiptno,uuid_from_bin(v1.DualTransactionKey) as DualKey,m1.Series,m1.MoneyCollId,m1.B_Group as BranchID,(case when v1.RootID=11 then 'Interest' WHEN m1.B_Group =" + Session["Branchid"] + " THEN m2.GrpMemberID ELSE t1.B_Name end )as ChitNo,m1.Head_Id,t1.B_Name as BranchName,v1.ChoosenDate as ApprovedDate,convert_tz( m1.CurrDate,'+00:00','+05:30') as RemittanceDate,(case when m1.B_Group=" + Session["Branchid"] + " and v1.RootID<>11 then  v1.Amount else 0.00 end )as 'CurrentBranchAmount' ,(case when m1.B_Group<>" + Session["Branchid"] + " and v1.RootID<>11 then  v1.Amount else 0.00 end )as 'OtherBranchAmount',v1.Narration,(case when v1.RootID=11 then v1.Amount else 0.00 end) as 'P&L',m2.MemberName FROM mobileappvoucher AS m1 LEFT JOIN membertogroupmaster AS m2 ON (m1.Head_Id = m2.Head_Id) LEFT JOIN branchdetails AS t1 ON (t1.Head_Id = m1.B_Group) left join voucher as v1 on(m1.AppReceiptno=v1.AppReceiptno) WHERE v1.Voucher_Type = 'C' and m1.Voucher_Type='C' and m1.Series='BCAPP' and m1.MoneyCollId=" + moneyCollId + " and m1.IsAccepted=1 and v1.ChoosenDate between '" + balayer.changedateformat(Convert.ToDateTime(fromDate), 2) + "' and '" + balayer.changedateformat(Convert.ToDateTime(toDate), 2) + "' and v1.BranchID=" + Session["Branchid"] + " ;";


            }
            DataTable getdt = new DataTable();
            getdt = balayer.GetDataTable(query);
            DataView dv = getdt.DefaultView;
            dv.Sort = "ApprovedDate asc,MoneyCollId asc";
            DataTable dtSorted = dv.ToTable();
            Gd_ViewVoucher.DataSource = dtSorted;
            Gd_ViewVoucher.DataBind();

            //06/12/2021
            DataTable getdt1 = balayer.GetDataTable(query1);
            DataView dv1 = getdt1.DefaultView;
            dv1.Sort = "ApprovedDate asc,MoneyCollId asc";
            DataTable dtSorted1 = dv1.ToTable();
            //

            userinfo = HttpContext.Current.User.Identity.Name;
            qry = "select rs.name from roles as rs inner join rights as rt on (rt.roleid=rs.id) where memberid=" + userinfo + "";
            usrRole = balayer.GetSingleValue(qry);

            if (usrRole == "Administrator")
            {
                //Gd_ViewVoucher.Columns[8].Visible = true;
            }
            else
            {
                //Gd_ViewVoucher.Columns[8].Visible = false;
            }

            decimal sum = 0;
            for (int i = 0; i < Gd_ViewVoucher.Rows.Count; i++)
            {
                sum += Convert.ToDecimal(Gd_ViewVoucher.Rows[i].Cells[5].Text);
            }
            lblTotalAmt.Visible = true;
            lblTotal.Visible = true;
            //lblTotalAmt.Text = sum.ToString();
            lblTotalAmt.Text = string.Format("{0:#,##,###.00}", sum);

            decimal sum1 = 0;
            for (int i = 0; i < Gd_ViewVoucher.Rows.Count; i++)
            {
                sum1 += Convert.ToDecimal(Gd_ViewVoucher.Rows[i].Cells[6].Text);
            }
            Label1.Visible = true;
            //Label2.Text = sum1.ToString();
            Label2.Text = string.Format("{0:#,##,###.00}", sum1);

            //sivanesan added interest total 2-12-2023

            decimal sum2 = 0;
            for (int i = 0; i < Gd_ViewVoucher.Rows.Count; i++)
            {
                sum2 += Convert.ToDecimal(Gd_ViewVoucher.Rows[i].Cells[7].Text);
            }
            //Label4.Visible = true;
            Label3.Visible = true;
            //Label2.Text = sum1.ToString();
            Label4.Text = string.Format("{0:#,##,###.00}", sum2);



            //ViewState["CurrentData"] = getdt;   //05/10/2021
            ViewState["CurrentData"] = dtSorted1;
        }

        public void LoadWithDate(string series, string fromDate, string toDate)
        {
            ViewState["fromDate"] = fromDate;
            ViewState["toDate"] = toDate;
            ViewState["CurrentData"] = null;    //05/10/2021

            s = series;

            if (series == "--Select--")
            {
                //query = "SELECT distinct m1.AppReceiptno as AppReceiptno,uuid_from_bin(v1.DualTransactionKey) as DualKey,m1.Series,m1.MoneyCollId,(case when v1.RootID=11 then 'Interest' else m2.GrpMemberID end )as ChitNo,t1.B_Name as BranchName,v1.ChoosenDate as ApprovedDate,convert_tz( m1.CurrDate,'+00:00','+05:30') as RemittanceDate,(case when m1.B_Group=" + Session["Branchid"] + " or v1.RootID=11 then  v1.Amount else 0.00 end )as 'CurrentBranchAmount' ,(case when m1.B_Group<>" + Session["Branchid"] + " and v1.RootID<>11 then  v1.Amount else 0.00 end )as 'OtherBranchAmount',v1.Narration,m1.Head_Id FROM mobileappvoucher AS m1 JOIN membertogroupmaster AS m2 ON (m1.Head_Id = m2.Head_Id) LEFT JOIN branchdetails AS t1 ON (t1.Head_Id = m1.B_Group) left join voucher as v1 on(m1.AppReceiptno=v1.AppReceiptno) WHERE m1.Voucher_Type = 'C' and (m1.Series='CPAPP' or m1.Series='CPWEB') and v1.ChoosenDate between '" + balayer.changedateformat(Convert.ToDateTime(fromDate), 2) + "' and '" + balayer.changedateformat(Convert.ToDateTime(toDate), 2) + "' and m1.IsAccepted=1 and m1.BranchID=161 ";
                //query1 = "SELECT distinct m1.AppReceiptno as AppReceiptno,uuid_from_bin(v1.DualTransactionKey) as DualKey,m1.Series,m1.MoneyCollId,(case when v1.RootID=11 then 'Interest' else m2.GrpMemberID end )as ChitNo,t1.B_Name as BranchName,v1.ChoosenDate as ApprovedDate,convert_tz( m1.CurrDate,'+00:00','+05:30') as RemittanceDate,(case when m1.B_Group=" + Session["Branchid"] + " or v1.RootID=11 then  v1.Amount else 0.00 end )as 'CurrentBranchAmount' ,(case when m1.B_Group<>" + Session["Branchid"] + " and v1.RootID<>11 then  v1.Amount else 0.00 end )as 'OtherBranchAmount',v1.Narration,(case when v1.RootID=11 then v1.Amount else 0.00 end) as 'P&L',m1.Head_Id FROM mobileappvoucher AS m1 JOIN membertogroupmaster AS m2 ON (m1.Head_Id = m2.Head_Id) LEFT JOIN branchdetails AS t1 ON (t1.Head_Id = m1.B_Group) left join voucher as v1 on(m1.AppReceiptno=v1.AppReceiptno) WHERE m1.Voucher_Type = 'C' and (m1.Series='CPAPP' or m1.Series='CPWEB') and v1.ChoosenDate between '" + balayer.changedateformat(Convert.ToDateTime(fromDate), 2) + "' and '" + balayer.changedateformat(Convert.ToDateTime(toDate), 2) + "' and m1.IsAccepted=1 and m1.BranchID=161 ";

                //02/11/2022 bagya
                query = "SELECT distinct m1.AppReceiptno as AppReceiptno,uuid_from_bin(v1.DualTransactionKey) as DualKey,m1.Series,m1.MoneyCollId,(case when v1.RootID=11 then 'Interest' else m2.GrpMemberID end )as ChitNo,t1.B_Name as BranchName,v1.ChoosenDate as ApprovedDate,convert_tz( m1.CurrDate,'+00:00','+05:30') as RemittanceDate,(case when m1.B_Group=" + Session["Branchid"] + " or v1.RootID=11 then  v1.Amount else 0.00 end )as 'CurrentBranchAmount' ,(case when m1.B_Group<>" + Session["Branchid"] + " and v1.RootID<>11 then  v1.Amount else 0.00 end )as 'OtherBranchAmount',v1.Narration,m1.Head_Id FROM mobileappvoucher AS m1 JOIN membertogroupmaster AS m2 ON (m1.Head_Id = m2.Head_Id) LEFT JOIN branchdetails AS t1 ON (t1.Head_Id = m1.B_Group) left join voucher as v1 on(m1.AppReceiptno=v1.AppReceiptno) WHERE v1.Voucher_Type = 'C' and  m1.Voucher_Type = 'C' and (m1.Series='CPAPP' or m1.Series='CPWEB') and v1.ChoosenDate between '" + balayer.changedateformat(Convert.ToDateTime(fromDate), 2) + "' and '" + balayer.changedateformat(Convert.ToDateTime(toDate), 2) + "' and m1.IsAccepted=1 and m1.BranchID=161 ";
                query1 = "SELECT distinct m1.AppReceiptno as AppReceiptno,uuid_from_bin(v1.DualTransactionKey) as DualKey,m1.Series,m1.MoneyCollId,(case when v1.RootID=11 then 'Interest' else m2.GrpMemberID end )as ChitNo,t1.B_Name as BranchName,v1.ChoosenDate as ApprovedDate,convert_tz( m1.CurrDate,'+00:00','+05:30') as RemittanceDate,(case when m1.B_Group=" + Session["Branchid"] + " or v1.RootID=11 then  v1.Amount else 0.00 end )as 'CurrentBranchAmount' ,(case when m1.B_Group<>" + Session["Branchid"] + " and v1.RootID<>11 then  v1.Amount else 0.00 end )as 'OtherBranchAmount',v1.Narration,(case when v1.RootID=11 then v1.Amount else 0.00 end) as 'P&L',m1.Head_Id FROM mobileappvoucher AS m1 JOIN membertogroupmaster AS m2 ON (m1.Head_Id = m2.Head_Id) LEFT JOIN branchdetails AS t1 ON (t1.Head_Id = m1.B_Group) left join voucher as v1 on(m1.AppReceiptno=v1.AppReceiptno) WHERE v1.Voucher_Type = 'C' and m1.Voucher_Type = 'C' and (m1.Series='CPAPP' or m1.Series='CPWEB') and v1.ChoosenDate between '" + balayer.changedateformat(Convert.ToDateTime(fromDate), 2) + "' and '" + balayer.changedateformat(Convert.ToDateTime(toDate), 2) + "' and m1.IsAccepted=1 and m1.BranchID=161 ";
            
            
            }
            else
            {
                //query = "SELECT distinct m1.AppReceiptno as AppReceiptno,uuid_from_bin(v1.DualTransactionKey) as DualKey,m1.Series,m1.MoneyCollId,(case when v1.RootID=11 then 'Interest' else m2.GrpMemberID end )as ChitNo,t1.B_Name as BranchName,v1.ChoosenDate as ApprovedDate,convert_tz( m1.CurrDate,'+00:00','+05:30') as RemittanceDate,(case when m1.B_Group=" + Session["Branchid"] + " or v1.RootID=11 then  v1.Amount else 0.00 end )as 'CurrentBranchAmount' ,(case when m1.B_Group<>" + Session["Branchid"] + " and v1.RootID<>11 then  v1.Amount else 0.00 end )as 'OtherBranchAmount',v1.Narration,m1.Head_Id FROM mobileappvoucher AS m1 JOIN membertogroupmaster AS m2 ON (m1.Head_Id = m2.Head_Id) LEFT JOIN branchdetails AS t1 ON (t1.Head_Id = m1.B_Group) left join voucher as v1 on(m1.AppReceiptno=v1.AppReceiptno) WHERE m1.Voucher_Type = 'C' and m1.Series='" + series + "' and v1.ChoosenDate between '" + balayer.changedateformat(Convert.ToDateTime(fromDate), 2) + "' and '" + balayer.changedateformat(Convert.ToDateTime(toDate), 2) + "' and m1.IsAccepted=1 and m1.BranchID=161 ";
                //query1 = "SELECT distinct m1.AppReceiptno as AppReceiptno,uuid_from_bin(v1.DualTransactionKey) as DualKey,m1.Series,m1.MoneyCollId,(case when v1.RootID=11 then 'Interest' else m2.GrpMemberID end )as ChitNo,t1.B_Name as BranchName,v1.ChoosenDate as ApprovedDate,convert_tz( m1.CurrDate,'+00:00','+05:30') as RemittanceDate,(case when m1.B_Group=" + Session["Branchid"] + " or v1.RootID=11 then  v1.Amount else 0.00 end )as 'CurrentBranchAmount' ,(case when m1.B_Group<>" + Session["Branchid"] + " and v1.RootID<>11 then  v1.Amount else 0.00 end )as 'OtherBranchAmount',v1.Narration,(case when v1.RootID=11 then v1.Amount else 0.00 end) as 'P&L',m1.Head_Id FROM mobileappvoucher AS m1 JOIN membertogroupmaster AS m2 ON (m1.Head_Id = m2.Head_Id) LEFT JOIN branchdetails AS t1 ON (t1.Head_Id = m1.B_Group) left join voucher as v1 on(m1.AppReceiptno=v1.AppReceiptno) WHERE m1.Voucher_Type = 'C' and m1.Series='" + series + "' and v1.ChoosenDate between '" + balayer.changedateformat(Convert.ToDateTime(fromDate), 2) + "' and '" + balayer.changedateformat(Convert.ToDateTime(toDate), 2) + "' and m1.IsAccepted=1 and m1.BranchID=161 ";

                //02/11/2022 bagya
                query = "SELECT distinct m1.AppReceiptno as AppReceiptno,uuid_from_bin(v1.DualTransactionKey) as DualKey,m1.Series,m1.MoneyCollId,(case when v1.RootID=11 then 'Interest' else m2.GrpMemberID end )as ChitNo,t1.B_Name as BranchName,v1.ChoosenDate as ApprovedDate,convert_tz( m1.CurrDate,'+00:00','+05:30') as RemittanceDate,(case when m1.B_Group=" + Session["Branchid"] + " or v1.RootID=11 then  v1.Amount else 0.00 end )as 'CurrentBranchAmount' ,(case when m1.B_Group<>" + Session["Branchid"] + " and v1.RootID<>11 then  v1.Amount else 0.00 end )as 'OtherBranchAmount',v1.Narration,m1.Head_Id FROM mobileappvoucher AS m1 JOIN membertogroupmaster AS m2 ON (m1.Head_Id = m2.Head_Id) LEFT JOIN branchdetails AS t1 ON (t1.Head_Id = m1.B_Group) left join voucher as v1 on(m1.AppReceiptno=v1.AppReceiptno) WHERE v1.Voucher_Type = 'C' and m1.Voucher_Type = 'C' and m1.Series='" + series + "' and v1.ChoosenDate between '" + balayer.changedateformat(Convert.ToDateTime(fromDate), 2) + "' and '" + balayer.changedateformat(Convert.ToDateTime(toDate), 2) + "' and m1.IsAccepted=1 and m1.BranchID=161 ";
                query1 = "SELECT distinct m1.AppReceiptno as AppReceiptno,uuid_from_bin(v1.DualTransactionKey) as DualKey,m1.Series,m1.MoneyCollId,(case when v1.RootID=11 then 'Interest' else m2.GrpMemberID end )as ChitNo,t1.B_Name as BranchName,v1.ChoosenDate as ApprovedDate,convert_tz( m1.CurrDate,'+00:00','+05:30') as RemittanceDate,(case when m1.B_Group=" + Session["Branchid"] + " or v1.RootID=11 then  v1.Amount else 0.00 end )as 'CurrentBranchAmount' ,(case when m1.B_Group<>" + Session["Branchid"] + " and v1.RootID<>11 then  v1.Amount else 0.00 end )as 'OtherBranchAmount',v1.Narration,(case when v1.RootID=11 then v1.Amount else 0.00 end) as 'P&L',m1.Head_Id FROM mobileappvoucher AS m1 JOIN membertogroupmaster AS m2 ON (m1.Head_Id = m2.Head_Id) LEFT JOIN branchdetails AS t1 ON (t1.Head_Id = m1.B_Group) left join voucher as v1 on(m1.AppReceiptno=v1.AppReceiptno) WHERE v1.Voucher_Type = 'C' and m1.Voucher_Type = 'C' and m1.Series='" + series + "' and v1.ChoosenDate between '" + balayer.changedateformat(Convert.ToDateTime(fromDate), 2) + "' and '" + balayer.changedateformat(Convert.ToDateTime(toDate), 2) + "' and m1.IsAccepted=1 and m1.BranchID=161 ";

            }
            DataTable getdt = new DataTable();
            getdt = balayer.GetDataTable(query);
            DataView dv = getdt.DefaultView;
            dv.Sort = "ApprovedDate asc,MoneyCollId asc";
            DataTable dtSorted = dv.ToTable();
            Gd_ViewVoucher.DataSource = dtSorted;
            Gd_ViewVoucher.DataBind();

            //06/12/2021
            DataTable getdt1 = balayer.GetDataTable(query1);
            DataView dv1 = getdt1.DefaultView;
            dv1.Sort = "ApprovedDate asc,MoneyCollId asc";
            DataTable dtSorted1 = dv1.ToTable();
            //

            ViewState["CurrentData"] = dtSorted1;    //05/10/2021

            userinfo = HttpContext.Current.User.Identity.Name;
            qry = "select rs.name from roles as rs inner join rights as rt on (rt.roleid=rs.id) where memberid=" + userinfo + "";
            usrRole = balayer.GetSingleValue(qry);

            if (usrRole == "Administrator")
            {
                //Gd_ViewVoucher.Columns[8].Visible = true;
            }
            else
            {
                //Gd_ViewVoucher.Columns[8].Visible = false;
            }

            decimal sum = 0;
            for (int i = 0; i < Gd_ViewVoucher.Rows.Count; i++)
            {
                sum += Convert.ToDecimal(Gd_ViewVoucher.Rows[i].Cells[5].Text);
            }
            lblTotalAmt.Visible = true;
            lblTotal.Visible = true;
            //lblTotalAmt.Text = sum.ToString();
            lblTotalAmt.Text = string.Format("{0:#,##,###.00}", sum);

            decimal sum1 = 0;
            for (int i = 0; i < Gd_ViewVoucher.Rows.Count; i++)
            {
                sum1 += Convert.ToDecimal(Gd_ViewVoucher.Rows[i].Cells[6].Text);
            }
            Label1.Visible = true;
            //Label2.Text = sum1.ToString();
            Label2.Text = string.Format("{0:#,##,###.00}", sum1);

        }

        protected void Gd_ViewVoucher_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Download")
            {
                Response.Clear();
                var link = e.CommandArgument.ToString();
                Response.Redirect(link);
            }
        }

        protected void btnCancel_Click(object sender, ImageClickEventArgs e)
        {
            if (Convert.ToString(Session["CheckRefresh"]) != Convert.ToString(ViewState["CheckRefresh"]))
            {
                return;
            }
            try
            {
                ImageButton btnDetails = sender as ImageButton;
                GridViewRow gvRow = (GridViewRow)btnDetails.NamingContainer;
                Session["Row"] = gvRow;
                string appReceiptno = balayer.ToobjectstrEvenNull(Gd_ViewVoucher.DataKeys[gvRow.RowIndex]["AppReceiptno"]).ToString();
                lblReceiptno.Text = appReceiptno;
                txtReason.Text = "";
                modalPopupExtender1.Enabled = true;
                modalPopupExtender1.PopupControlID = "msgbox";
                modalPopupExtender1.TargetControlID = "show";
                modalPopupExtender1.Show();
            }
            catch (Exception ex)
            { }
        }

        protected void btnRejectConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                GridViewRow gvRow = (GridViewRow)Session["Row"];
                string appReceiptno = balayer.ToobjectstrEvenNull(Gd_ViewVoucher.DataKeys[gvRow.RowIndex]["AppReceiptno"]);
                string head_id = balayer.ToobjectstrEvenNull(Gd_ViewVoucher.DataKeys[gvRow.RowIndex]["Head_Id"]);
                string series = balayer.ToobjectstrEvenNull(Gd_ViewVoucher.DataKeys[gvRow.RowIndex]["Series"]);
                string approvedDate = balayer.ToobjectstrEvenNull(Gd_ViewVoucher.DataKeys[gvRow.RowIndex]["ApprovedDate"]);
                string appDate = Convert.ToDateTime(approvedDate).ToString("yyyy/MM/dd");
                string dualTransactionKey = "0x" + balayer.ToobjectstrEvenNull(Gd_ViewVoucher.DataKeys[gvRow.RowIndex]["DualKey"]).Replace("-", "");

                var checkCheque = balayer.GetDataTable("select AppReceiptno from svcf.mobileappchequevoucher where AppReceiptno='" + appReceiptno + "' limit 1;");
                if(checkCheque.Rows.Count>0)
                {
                    var updateCheque= "Update svcf.mobileappchequevoucher set IsAccepted=2,RejectReason='" + txtReason.Text + "' where AppReceiptno='" + appReceiptno + "' and Series='" + series + "';";
                    trn.insertorupdateTrn(updateCheque);
                }
                var qry = "Update svcf.mobileappvoucher set IsAccepted=2,RejectReason='" + txtReason.Text + "' where AppReceiptno='" + appReceiptno + "' and Series='" + series + "';";
                trn.insertorupdateTrn(qry);
                //09/08/2021 - Access denied to cancelled receipt from mobile app server
                var qry1 = "Update svcf.receiptprint_pdf set Status=1 where AppReceiptno='" + appReceiptno + "';";
                trn.insertorupdateTrn(qry1);

                var transKey = balayer.GetDataTable("select TransactionKey from voucher where DualTransactionKey=" + dualTransactionKey + " and ChoosenDate='" + appDate + "' and Voucher_Type='D';");
                string transactionkey = "";
                for (int i = 0; i < transKey.Rows.Count; i++)
                {
                    if (transactionkey == "")
                    {
                        transactionkey = transKey.Rows[i]["TransactionKey"].ToString();
                    }
                    else
                    {
                        transactionkey = transactionkey + "," + transKey.Rows[i]["TransactionKey"].ToString();
                    }

                }
                

                var qry2 = "delete from svcf.voucher where DualTransactionKey=" + dualTransactionKey + " and ChoosenDate='" + appDate + "';";
                trn.insertorupdateTrn(qry2);


                var qry3 = "delete from svcf.transbank where TransactionKey in (" + transactionkey + ");";
                trn.insertorupdateTrn(qry3);

                //08/10/2021
                var dtV = balayer.GetDataTable("select uuid_from_bin(dualtransactionkey) as DualKey from svcf.voucher where AppReceiptno='" + appReceiptno + "'and Head_Id="+head_id+";");
                if(dtV.Rows.Count>0)
                {
                    for (int r = 0; r < dtV.Rows.Count; r++)
                    {
                        var dual = "0x" + balayer.ToobjectstrEvenNull(dtV.Rows[r]["DualKey"]).Replace("-", "");
                        //trn.insertorupdateTrn("delete from svcf.voucher where AppReceiptno='" + appReceiptno + "';");
                        trn.insertorupdateTrn("delete from svcf.voucher where DualTransactionKey=" + dual + ";");
                    }
                }


                lblWarning.Text = "Receipt Rejected Successfully!";
                lblWarning.ForeColor = System.Drawing.Color.Green;
                modalPopupExtender1.PopupControlID = "warning";
                warning.Visible = true;
                modalPopupExtender1.Show();
            }
            catch (Exception ex)
            {
                try
                {
                    trn.RollbackTrn();
                }
                catch (Exception err)
                {
                    logger.Info("AppVoucherMoneyCollector.aspx - btnRejectConfirm_Click() - Error: " + err.Message + ": " + DateTime.Now + " by: " + Convert.ToString(Session["UserName"]) + "");
                }
                finally
                {
                    lblWarning.Text = "Reject the receipt Unsuccess!\n" + ex.Message;
                    lblWarning.ForeColor = System.Drawing.Color.Red;
                    modalPopupExtender1.PopupControlID = "warning";
                    warning.Visible = true;
                    modalPopupExtender1.Show();
                }
            }
            finally
            {
                trn.DisposeTrn();
            }
        }

        protected void btnCancel_Click1(object sender, EventArgs e)
        {
            modalPopupExtender1.Hide();
        }

        protected void btnOk_Click(object sender, EventArgs e)
        {

            modalPopupExtender1.Hide();
            if (ViewState["fromDate"] != null)
            {
                fDate = ViewState["fromDate"].ToString();
            }
            if (ViewState["toDate"] != null)
            {
                tDate = ViewState["toDate"].ToString();
            }
            if (Session["Branchid"].ToString() == "161")
            {
                LoadWithDate(s, fDate, tDate);
            }
            else
            {
                LoadData(moneycol, fDate, tDate);
            }
        }

        public DataTable loadGridExport()
        {
            DataTable dt = (DataTable)ViewState["CurrentData"];    //05/10/2021
            return dt;
            
        }

        protected void show_Click(object sender, EventArgs e)
        {

        }
    }
}