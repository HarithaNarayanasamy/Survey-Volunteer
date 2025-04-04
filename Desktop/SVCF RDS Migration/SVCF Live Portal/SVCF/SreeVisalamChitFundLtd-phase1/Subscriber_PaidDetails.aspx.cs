using SVCF_BusinessAccessLayer;
using SVCF_TransactionLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using System.IO;
using ClosedXML.Excel;
using System.Configuration;
using log4net;
using log4net.Config;

namespace SreeVisalamChitFundLtd_phase1
{
    public partial class Subscriber_PaidDetails : System.Web.UI.Page
    {
        #region Object

        BusinessLayer balayer = new BusinessLayer();
        TransactionLayer trn = new TransactionLayer();
        List<string> TempList = new List<string>();
        Dictionary<string, string> Tempdic = new Dictionary<string, string>();
        DataTable dtforeman = null;
        List<string> CommList = new List<string>();
        static int SelectedGroupid = 0;
        #endregion

        ILog logger = log4net.LogManager.GetLogger(typeof(Subscriber_PaidDetails));
        DataTable dtGroup;
        DataTable dtheads = new DataTable();
        string query = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {                
                BindGroup();
            }
            logger.Info("Subscriber Paid Details - at: " + DateTime.Now);
        }

        public void BindGroup()
        {
            string gpid = "";
            CommList.Clear();
            query = "SELECT distinct ChitGroupId FROM svcf.SubscriberDetails;";
            CommList = balayer.RetrveList(query);
            foreach(string item in CommList)
            {
                if (CommList.Count > 1)
                {
                    gpid = gpid + "," + item + ",";
                }
                else
                {
                    gpid = item + ",";
                }
            }

            gpid = gpid.Remove(gpid.Length - 1);
            Tempdic.Clear();
            Tempdic = balayer.CmnList(@"SELECT Head_Id,GROUPNO FROM groupmaster where BranchID=" + Session["Branchid"] + " and Head_Id not in (" + gpid + ")");
            ddlGroup.DataValueField = "Key";
            ddlGroup.DataTextField = "Value";
            ddlGroup.DataSource = Tempdic;
            ddlGroup.DataBind();
            ddlGroup.Items.Insert(0, new ListItem("--Select--", "0"));           
        }

        protected void BtnGo_Click(object sender, EventArgs e)
        {
            try
            {
                SelectedGroupid = Convert.ToInt32(ddlGroup.SelectedValue);
                dtGroup = new DataTable();
                DataTable dtnew = new DataTable();
                int maxDrawNo = 0;
                int ForemanHeadId = 0;
                decimal TotalDueAmount = 0;
                DateTime auctionDate;
                decimal Due = 0;
                decimal TokenPaidAmount = 0;
                decimal ForemanPaidAmount = 0;
                decimal TotalArrearAmount = 0;
                string insno = "";
                decimal DefaultInterest = 0;
                double sumAmount = 0;
                long firstValue = 0;
                long secondValue = 0;
                string part = "F";
                query = "select mg.Head_Id,mg.GroupID,mg.GrpMemberID,mg.MemberID,mg.MemberName,mm.MobileNo,mm.ResidentialAddress,gm.ChitAgreementNo from membertogroupmaster as mg join membermaster as mm " +
                         "on (mm.MemberIDNew=mg.MemberID) join groupmaster as gm on (gm.Head_Id=mg.GroupID) where mg.branchid=" + Session["Branchid"] + " and mg.GroupID=" + ddlGroup.SelectedValue + "";
                dtGroup = balayer.GetDataTable(query);
                query = "select mg.GrpMemberID,mg.Head_Id,gm.NoofMembers from membertogroupmaster mg join membermaster mm on (mm.MemberIDNew=mg.MemberID) " +
                         "join groupmaster as gm on (gm.Head_Id=mg.GroupID) where mm.TypeOfMember = 'Foreman' and mg.branchid= " + Session["Branchid"] + " and mg.GroupID=" + ddlGroup.SelectedValue + ";";
                dtforeman = new DataTable();
                dtforeman = balayer.GetDataTable(query);
                //Get Foreman Id
                if (dtforeman.Rows.Count > 0)
                {
                    foreach (DataRow dr1 in dtforeman.Rows)
                    {
                        if (Convert.ToString(dr1["GrpMemberID"]).Contains('/'))
                        {
                            if (Convert.ToString(dr1["GrpMemberID"]).Split('/')[1] == Convert.ToString(dr1["NoofMembers"]))
                            {
                                ForemanHeadId = Convert.ToInt32(dr1["Head_Id"]);
                            }
                        }
                        else if (Convert.ToString(dr1["GrpMemberID"]).Contains('-'))
                        {
                            if (Convert.ToString(dr1["GrpMemberID"]).Split('-')[2] == Convert.ToString(dr1["NoofMembers"]))
                            {
                                ForemanHeadId = Convert.ToInt32(dr1["Head_Id"]);
                            }
                        }
                    }
                }
                query = "select sum(CurrentDueAmount) as 'TotalDueAmount',max(Drawno) as 'Drawno',max(AuctionDate) as 'AuctionDate' from auctiondetails where PrizedMemberID<>0 and GroupID=" + ddlGroup.SelectedValue + "";
                dtnew = balayer.GetDataTable(query);
                DataTable dtBind = new DataTable();
                dtBind.Columns.Add("Slno", typeof(int));
                dtBind.Columns.Add("ICM");
                dtBind.Columns.Add("TicketNumber");
                dtBind.Columns.Add("MemberName");
                dtBind.Columns.Add("MobileNumber");
                dtBind.Columns.Add("Address");
                dtBind.Columns.Add("InstallmentNo");
                dtBind.Columns.Add("ChitAgreementNo");
                dtBind.Columns.Add("DueAmount");
                dtBind.Columns.Add("DefaultInterest");
                DataRow dr = dtBind.NewRow();
                int count = 0;
                for (int i = 0; i < dtGroup.Rows.Count; i++)
                {
                    maxDrawNo = Convert.ToInt32(dtnew.Rows[0]["Drawno"]);
                    TotalDueAmount = Convert.ToDecimal(dtnew.Rows[0]["TotalDueAmount"]);
                    auctionDate = Convert.ToDateTime(dtnew.Rows[0]["AuctionDate"]);
                    ////Token paid amount
                    //query = "select sum(Amount) from voucher where head_id=" + dtGroup.Rows[i]["Head_Id"] + " and Other_Trans_Type<>5 and Voucher_Type='C' and RootID=5 and ChoosenDate<='" +balayer.changedateformat(auctionDate,2) + "'";
                    //TokenPaidAmount = balayer.GetScalarDecimal(query);

                    //Foreman Paid Amount
                    query = "select sum(Amount) from voucher where head_id=" + ForemanHeadId + " and Other_Trans_Type<>5 and Voucher_Type='C' and RootID=5 and ChoosenDate<='" + balayer.changedateformat(auctionDate, 2) + "'";
                    ForemanPaidAmount = balayer.GetScalarDecimal(query);

                    query = "SELECT CurrentDueAmount FROM svcf.auctiondetails where GroupID='" + ddlGroup.SelectedValue + "' and  DrawNO=1";
                    Due = balayer.GetScalarDecimal(query);
                    //sumAmount = TokenPaidAmount / Due;
                    sumAmount = balayer.GetInstallment(Convert.ToInt32(ddlGroup.SelectedValue), Convert.ToInt32(dtGroup.Rows[i]["Head_Id"]), auctionDate, sumAmount);
                    var values = sumAmount.ToString(CultureInfo.InvariantCulture).Split('.');
                    if (values.Length > 1)
                    {
                        firstValue = int.Parse(values[0]);
                        secondValue = long.Parse(values[1]);
                    }
                    else
                    {
                        firstValue = int.Parse(values[0]);
                    }
                    if (secondValue == '0')
                    {
                        firstValue++;
                        insno = firstValue + " - " + maxDrawNo;
                    }
                    else
                    {
                        firstValue++;
                        insno = firstValue + " - " + maxDrawNo;
                    }

                    TotalDueAmount = balayer.GetScalarDecimal("select CurrentDueAmount FROM svcf.auctiondetails where GroupID='" + ddlGroup.SelectedValue + "' and  DrawNO=" + maxDrawNo + "");
                    if ((ForemanPaidAmount - TokenPaidAmount) > 0)
                    {
                        TotalArrearAmount = TotalArrearAmount + (ForemanPaidAmount - TokenPaidAmount);
                    }
                    DefaultInterest = (Due / 100) * 24;
                    count = count + 1;
                    dr["Slno"] = count;
                    dr["ICM"] = "ICM";
                    dr["TicketNumber"] = dtGroup.Rows[i]["GrpMemberID"];
                    dr["MemberName"] = dtGroup.Rows[i]["MemberName"];
                    dr["MobileNumber"] = dtGroup.Rows[i]["MobileNo"];
                    dr["Address"] = dtGroup.Rows[i]["ResidentialAddress"];
                    dr["InstallmentNo"] = insno;
                    dr["ChitAgreementNo"] = dtGroup.Rows[i]["ChitAgreementNo"];
                    dr["DueAmount"] = TotalDueAmount;
                    dr["DefaultInterest"] = DefaultInterest;
                    dtBind.Rows.Add(dr.ItemArray);
                    insno = "";
                }
                gvDetails.DataSource = dtBind;
                gvDetails.DataBind();
                hdRowCount.Value = Convert.ToString(dtBind.Rows.Count);
            }
            catch (Exception err) { }
        }

        protected void chkSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSelectAll.Checked == true)
            {
                for (int i = 0; i <= gvDetails.Rows.Count - 1; i++)
                {
                    CheckBox chk = (CheckBox)gvDetails.Rows[i].FindControl("chkSelect");
                    chk.Checked = true;
                }
            }
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i <= gvDetails.Rows.Count - 1; i++)
                {
                    if ((((CheckBox)gvDetails.Rows[i].FindControl("chkSelect")).Checked))
                    {
                        query = "insert into ";
                        balayer.ExecuteQuery(query);
                    }
                }
            }
            catch (Exception) { }
        }

        [System.Web.Services.WebMethod]
        public static string GetMemberid(string data)
        {        
            try
            {
                BusinessLayer objBA = new BusinessLayer();
                string qry="";
                JavaScriptSerializer json = new JavaScriptSerializer();
                var reqDetails = JsonConvert.DeserializeObject<List<DtList>>(data);
                for (int i = 0; i < reqDetails.Count; i++)
                {
                    qry = "insert into SubscriberDetails(ICM, BranchID, ChitGroupId, Ticket_NO, SubscriberName, Mobile, Address, InstallmentNo, ChitAgreementNo, DueAmount, DefaultInterest) values(" +
                        "'" + reqDetails[i].lblicm + "','" + objBA.ToobjectstrEvenNull(HttpContext.Current.Session["Branchid"]) + "'," + SelectedGroupid + "," +
                        "'" + reqDetails[i].lblticketnumber + "','" + reqDetails[i].lblmembername + "','" + reqDetails[i].txtMobile + "','" + reqDetails[i].txtAddress + "'," +
                        "'" + reqDetails[i].lblinsNo + "','" + reqDetails[i].lblAgreementNo + "'," + reqDetails[i].txtDueAmount + "," + reqDetails[i].txtinterest + ")";
                    objBA.ExecuteQuery(qry);
                }

            }
            catch (Exception) { }
            return "Added Successfully";
        }

        protected void BtnExport_Click(object sender, EventArgs e)
        {
            DataTable dtDetails = new System.Data.DataTable();
            dtDetails = balayer.GetDataTable("SELECT ICM,Ticket_NO,SubscriberName,Mobile,Address,InstallmentNo,ChitAgreementNo,DueAmount,DefaultInterest FROM svcf.SubscriberDetails");

            using (XLWorkbook wb = new XLWorkbook())
            {

                //wb.Worksheets.Add(dtDetails, "SubscriberDetails");
                var ws = wb.Worksheets.Add("SubscriberDetails");
                ws.FirstRow().FirstCell().InsertData(dtDetails.Rows);
                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename=Subscriber_PaidDetails.xlsx");

                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(Response.OutputStream);
                    DocumentFormat.OpenXml.Spreadsheet.Row headerRow = new DocumentFormat.OpenXml.Spreadsheet.Row();

                    Response.Flush();
                    Response.End();
                }


            }
        }

    }

   
    public class DtList
    {
        public string lblicm { get; set; }
        public string lblticketnumber { get; set; }
        public string lblmembername { get; set; }
        public string txtMobile { get; set; }
        public string txtAddress { get; set; }
        public string lblinsNo { get; set; }
        public string lblAgreementNo { get; set; }
        public string txtDueAmount { get; set; }
        public string txtinterest { get; set; }
    }
}