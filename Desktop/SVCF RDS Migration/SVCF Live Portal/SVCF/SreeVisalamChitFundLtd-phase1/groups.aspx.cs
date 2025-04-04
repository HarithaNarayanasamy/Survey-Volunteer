using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Xml.Linq;
using System.Text.RegularExpressions;
using SVCF_BusinessAccessLayer;
using SVCF_DataAccessLayer;
using SVCF_TransactionLayer;
using log4net;
using log4net.Config;

namespace SreeVisalamChitFundLtd_phase1
{
    public partial class groups : System.Web.UI.Page
    {
        #region Object
        BusinessLayer balayer = new BusinessLayer();
        TransactionLayer trn = new TransactionLayer();
        #endregion

        ILog logger = log4net.LogManager.GetLogger(typeof(groups));

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
                Session["CheckRefresh"] = System.Guid.NewGuid().ToString();
                txtgroup_no.Focus();
                cmbchit_category.Items.Add("--select--");
                cmbchit_category.Items.Add("Monthly");
                cmbchit_category.Items.Add("Trimonthly");
                cmbchit_category.Items.Add("Fortnightly");
                chitValue();
                txtauction_dt1.ToolTip="Ex. "+DateTime.Now.ToString("dd/MM/yyyy")+ " (dd/mm/yyyy)";
                txtauction_dt.ToolTip = "Ex. " + DateTime.Now.ToString("dd/MM/yyyy") + " (dd/mm/yyyy)";
                txtpso_order_date.ToolTip = "Ex. " + DateTime.Now.ToString("dd/MM/yyyy") + " (dd/mm/yyyy)";
                txtdt_of_agree.ToolTip = "Ex. " + DateTime.Now.ToString("dd/MM/yyyy") + " (dd/mm/yyyy)";
                txtsdpComm.ToolTip = "Ex. " + DateTime.Now.ToString("dd/MM/yyyy") + " (dd/mm/yyyy)";
                txtsdpMatur.ToolTip = "Ex. " + DateTime.Now.ToString("dd/MM/yyyy") + " (dd/mm/yyyy)";
                txtchit_start_dt.ToolTip = "Ex. " + DateTime.Now.ToString("dd/MM/yyyy") + " (dd/mm/yyyy)";
                txtchit_end_dt.ToolTip = "Ex. " + DateTime.Now.ToString("dd/MM/yyyy") + " (dd/mm/yyyy)";
            }
        }
        void chitValue()
        {
            DataTable dt = balayer.GetDataTable("SELECT cast(ChitValue as char) as ChitValue FROM svcf.commissiondetails;");
            DataRow dr = dt.NewRow();
            dr[0] = "--select--";
            txtchit_value.DataSource = dt;
            txtchit_value.DataValueField = "ChitValue";
            txtchit_value.DataTextField = "ChitValue";
            dt.Rows.InsertAt(dr, 0);
            txtchit_value.DataBind();
        }
        protected void Page_PreRender(object sender, EventArgs e)
        {
            ViewState["CheckRefresh"] = Session["CheckRefresh"];
        }

        protected void btnclose(object sender, EventArgs e)
        {
            clearGroup();
        }

        protected void btnaddGroup_click(object sender, EventArgs e)
        {
            Page.Validate("group");
            if (!Page.IsValid)
            {
                return;
            }
            if (Session["CheckRefresh"].ToString() != ViewState["CheckRefresh"].ToString())
            {
                return;
            }
            TransactionLayer trn = new TransactionLayer();
            string DualTransactionKey = ""; 
            try
            {
                int count = Convert.ToInt32(balayer.GetSingleValue("SELECT count(*) FROM  `groupmaster` where GROUPNO='" + balayer.MySQLEscapeString(txtgroup_no.Text) + "'"));
                if (count > 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Warning", "alert('" + balayer.MySQLEscapeString( txtgroup_no.Text ) + " Already Exists!!!');", true);
                    txtgroup_no.Text = "";
                    txtgroup_no.Focus();
                    return;
                }
                int parentID = 0;
                string strPreviousID = "";
                long iresult = 0;
                string strIdtoInsert = "";
                long strCall;
                long strPrized;

                if (balayer.MySQLEscapeString(balayer.ToobjectstrEvenNull(cmbchit_category.SelectedValue)) == "Monthly")
                {
                    parentID = int.Parse(balayer.GetSingleValue("select NodeID from headstree Where Node ='Monthly Chits'"));
                    strPreviousID = balayer.GetSingleValue("select TreeHint from headstree WHERE NodeID=" + parentID + "");
                }
                else if (balayer.MySQLEscapeString(balayer.ToobjectstrEvenNull(cmbchit_category.SelectedValue)) == "Trimonthly")
                {
                    parentID = int.Parse(balayer.GetSingleValue("select NodeID from headstree Where Node ='Trimonthly Chits'"));
                    strPreviousID = balayer.GetSingleValue("select TreeHint from headstree WHERE NodeID=" + parentID + "");
                }
                else
                    if (balayer.MySQLEscapeString(balayer.ToobjectstrEvenNull(cmbchit_category.SelectedValue)) == "Fortnightly")
                    {
                        parentID = int.Parse(balayer.GetSingleValue("select NodeID from headstree Where Node ='Fortnightly Chits'"));
                        strPreviousID = balayer.GetSingleValue("select TreeHint from headstree WHERE NodeID=" + parentID + "");
                    }
                iresult = trn.insertorupdateTrn("insert into headstree(ParentID, Node, TreeHint,Branchid) values(" + parentID + ",'" + balayer.MySQLEscapeString(txtgroup_no.Text) + "','Null'," + balayer.ToobjectstrEvenNull(Session["Branchid"]) + ")");
                strIdtoInsert = strPreviousID.Trim(',') + "," + iresult;
                strIdtoInsert = strIdtoInsert.Trim(',');
                trn.insertorupdateTrn("UPDATE headstree SET TreeHint='" + strIdtoInsert + "'" + " WHERE NodeID=" + iresult + "");
                strCall = trn.insertorupdateTrn("insert into headstree(ParentID, Node,Branchid) values(1054,'" + balayer.MySQLEscapeString(txtgroup_no.Text + "/" + txtno_of_members.Text) + "'," + balayer.ToobjectstrEvenNull(Session["Branchid"]) + ")");
                trn.insertorupdateTrn("update headstree set TreeHint='6,1054," + strCall + "' where NodeID=" + strCall + "");
                strPrized = trn.insertorupdateTrn("insert into headstree(ParentID, Node,Branchid) values(1052,'" + balayer.MySQLEscapeString(txtgroup_no.Text + "/" + txtno_of_members.Text) + "'," + balayer.ToobjectstrEvenNull(Session["Branchid"]) + ")");
                trn.insertorupdateTrn("update headstree set TreeHint='6,1052," + strPrized + "' where NodeID=" + strPrized + "");
                int AucHour = int.Parse(TimeSelAuction.DateTime.ToString("hh:mm:ss tt").Replace("PM", "").Replace("AM", "").Split(':')[0].Trim());
                int AucMin = int.Parse(TimeSelAuction.DateTime.ToString("hh:mm:ss tt").Replace("PM", "").Replace("AM", "").Split(':')[1].Trim());
                string AucAMPM = "";
                if (TimeSelAuction.Value.ToString().Contains("AM"))
                {
                    AucAMPM = "AM";
                }
                else
                {
                    AucAMPM = "PM";
                }
                string AuctionTime = balayer.Get24HrTime(AucHour, AucMin, AucAMPM, 0);
                string AuctionEndTime = balayer.Get24HrTime(AucHour, AucMin, AucAMPM, 5);
                string branch = balayer.GetSingleValue("SELECT B_Code FROM svcf.branchdetails where Head_Id=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + "");
                DataTable selectedBranchTable = balayer.GetDataTable("select MemberID,AddressForCommunication from membermaster where BranchId=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + "");
                string MemberID = balayer.ToobjectstrEvenNull(selectedBranchTable.Rows[0]["MemberID"]);
                string AddressForCommunication = balayer.MySQLEscapeString( balayer.ToobjectstrEvenNull(selectedBranchTable.Rows[0]["AddressForCommunication"]));
                long insertCmd = trn.insertorupdateTrn("insert into groupmaster(BranchID,GROUPNO,PSOOrderNo,PSOOrderDate,PSODROffice,chitAgreementNo,ChitAgreementYear,AgreementDate,ChitValue,ChitPeriod,ChitCategory,ChitStartDate,ChitEndDate,NoofMembers,AuctionDate,AuctionTime,AuctionEndTime,`SDP_FDRNO`,`SDP_Bank`,`SDP_BankPlace`,`SDP_Commencement`,`SDP_Maturity`,`SDP_RateofInterest`,`SDP_PeriodinMonths`,`SDP_Amount`,`Head_Id`) values(" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + ",'" + balayer.MySQLEscapeString(txtgroup_no.Text) + "','" + balayer.MySQLEscapeString(txtpso_order_no.Text) + "','" + balayer.indiandateToMysqlDate(txtpso_order_date.Text) + "','" + balayer.MySQLEscapeString(txtpso_dr_office.Text) + "','" + balayer.MySQLEscapeString(txtchit_agree_no.Text) + "'," + txtdt_of_agree.Text.Split('/')[2] + ",'" + balayer.indiandateToMysqlDate(txtdt_of_agree.Text) + "'," + balayer.MySQLEscapeString(txtchit_value.SelectedItem.Value) + "," + balayer.MySQLEscapeString(txtchit_period.Text) + ",'" + balayer.MySQLEscapeString(balayer.ToobjectstrEvenNull(cmbchit_category.SelectedValue)) + "','" + balayer.indiandateToMysqlDate(txtchit_start_dt.Text) + "','" + balayer.indiandateToMysqlDate(txtchit_end_dt.Text) + "'," + balayer.MySQLEscapeString(txtno_of_members.Text) + ",'" + balayer.indiandateToMysqlDate(txtauction_dt.Text) + "','" + balayer.MySQLEscapeString(AuctionTime) + "','" + AuctionEndTime + "','" + balayer.MySQLEscapeString(txtSDPFDR.Text) + "','" + balayer.MySQLEscapeString(txtSDP_bank.Text) + "','" + balayer.MySQLEscapeString(txtsdpbankPlace.Text) + "','" + balayer.indiandateToMysqlDate(txtsdpComm.Text) + "','" + balayer.indiandateToMysqlDate(txtsdpMatur.Text) + "'," + balayer.MySQLEscapeString(txtInterest.Text) + "," + balayer.MySQLEscapeString(txtPeriodMonths.Text) + "," + balayer.MySQLEscapeString(txtAmount.Text) + "," + iresult + ")");
                //string MemIDTab = balayer.GetSingleValue("select MemberIDNew from membermaster where  BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + "");
                string MemIDTab = balayer.GetSingleValue("select MemberIDNew from membermaster where CustomerName like '%Visalam%' and branchid=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + "");
                long iresult1 = trn.insertorupdateTrn("insert into headstree (ParentID, Node, TreeHint,Branchid) values(" + iresult + ",'" + balayer.MySQLEscapeString(txtgroup_no.Text + "/" + txtno_of_members.Text) + "','Null'," + balayer.ToobjectstrEvenNull(Session["Branchid"]) + ")");
                string strIdtoInsert1 = strIdtoInsert.Trim(',') + "," + iresult1;
                trn.insertorupdateTrn("UPDATE headstree SET TreeHint='" + strIdtoInsert1 + "' WHERE NodeID=" + iresult1 + "");
                long insertCmdReturned = trn.insertorupdateTrn("insert into membertogroupmaster(`BranchID`,`MemberName`,`MemberID`,`MemberAddress`,`GroupID`,`GrpMemberID`,`Head_Id`,M_Id,B_Id,EstCallNoOfAuction) values(" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + ",'Sree Visalam Chit Fund Ltd'," + MemIDTab + ",'" + balayer.ToobjectstrEvenNull(AddressForCommunication) + "'," + iresult + ",'" + balayer.MySQLEscapeString(txtgroup_no.Text) + "/" + txtno_of_members.Text + "'," + iresult1 + ",0," + balayer.ToobjectstrEvenNull(Session["Branchid"]) + ",'1')");
                string GrpNo = balayer.MySQLEscapeString( txtgroup_no.Text );
                int numberofDraw = Convert.ToInt32( txtno_of_members.Text);
                System.Globalization.DateTimeFormatInfo usDtfi = new System.Globalization.CultureInfo("en-NZ", false).DateTimeFormat;
                int RebidNo = 0;
                string d = balayer.ToobjectstrEvenNull(txtauction_dt.Text);
                DateTime dFirstAuctionDate = DateTime.Parse(d, usDtfi);
                DateTime tempDAuctiondates = dFirstAuctionDate;
                tempDAuctiondates = tempDAuctiondates.AddMonths(-1);
                decimal currDueAmt = Convert.ToDecimal(txtchit_value.SelectedItem.Value) / numberofDraw;
                decimal nextDueAmt = currDueAmt;
                int noofmonths = AuctionDates();
                string d1 = balayer.ToobjectstrEvenNull(txtauction_dt1.Text);
                if (balayer.ToobjectstrEvenNull(cmbchit_category.SelectedItem) == "Fortnightly")
                {
                    for (int drno = 1; drno <= numberofDraw; drno++)
                    {
                        Boolean b = Convert.ToBoolean("sunday".Equals(balayer.ToobjectstrEvenNull(tempDAuctiondates.Date.Day), StringComparison.InvariantCultureIgnoreCase));
                        if (b == true)
                        {
                            dFirstAuctionDate = dFirstAuctionDate.AddDays(1);
                        }
                        string s = tempDAuctiondates.Date.ToString("dd/MM/yyyy");
                        if ((drno != 1) && (drno != 2))
                        {
                            long inscmd = trn.insertorupdateTrn("insert into auctiondetails(`BranchID`,`GroupID`,`AuctionDate`,`ReBidNO`,`DrawNO`,`IsPrized`) values(" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + "," + iresult + ",'" + balayer.indiandateToMysqlDate(s) + "'," + RebidNo + "," + drno + ",'I')");
                            tempDAuctiondates = tempDAuctiondates.AddDays(15);
                        }
                        else
                            if (drno == 1)
                            {
                                string fdfdfd = balayer.GetSingleValue("SELECT min(MemberIDNew) FROM svcf.membermaster where BranchId=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and CustomerName='Sree Visalam Chit Fund Ltd'");
                                long inscmd = trn.insertorupdateTrn("insert into auctiondetails(`BranchID`,`GroupID`,`AuctionDate`,`ReBidNO`,`DrawNO`,`PrizedMemberID`,MemberID,`CurrentDueAmount`,`NextDueAmount`,PrizedAmount,`IsPrized`) values(" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + "," + iresult + ",'" + balayer.indiandateToMysqlDate(d1) + "'," + RebidNo + "," + drno + "," + iresult1 + "," + fdfdfd + "," + currDueAmt + "," + nextDueAmt + "," + txtchit_value.SelectedItem.Value + ",'F')");
                                tempDAuctiondates = tempDAuctiondates.AddDays(15);
                            }
                            else
                                if (drno == 2)
                                {
                                    long inscmd = trn.insertorupdateTrn("insert into auctiondetails(`BranchID`,`GroupID`,`AuctionDate`,`ReBidNO`,`DrawNO`,`CurrentDueAmount`,`IsPrized`) values(" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + "," + iresult + ",'" + balayer.indiandateToMysqlDate(s) + "'," + RebidNo + "," + drno + "," + currDueAmt + ",'I')");
                                    tempDAuctiondates = tempDAuctiondates.AddDays(15);
                                }
                    }
                }
                else if (balayer.ToobjectstrEvenNull(cmbchit_category.SelectedItem) == "Monthly")
                {
                    for (int drno = 1; drno <= numberofDraw; drno++)
                    {
                        Boolean b = Convert.ToBoolean("sunday".Equals(balayer.ToobjectstrEvenNull(tempDAuctiondates.Date.Day), StringComparison.InvariantCultureIgnoreCase));
                        if (b == true)
                        {
                            dFirstAuctionDate = dFirstAuctionDate.AddDays(noofmonths);
                        }
                        string s = tempDAuctiondates.Date.ToString("dd/MM/yyyy");
                        if ((drno != 1) && (drno != 2))
                        {
                            long inscmd = trn.insertorupdateTrn("insert into auctiondetails(`BranchID`,`GroupID`,`AuctionDate`,`ReBidNO`,`DrawNO`,`IsPrized`) values(" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + "," + iresult + ",'" + balayer.indiandateToMysqlDate(s) + "'," + RebidNo + "," + drno + ",'I')");
                            tempDAuctiondates = tempDAuctiondates.AddMonths(noofmonths);
                        }
                        else
                            if (drno == 1)
                            {
                                string fdfdfd = balayer.GetSingleValue("SELECT MemberIDNew FROM svcf.membermaster where BranchId=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and CustomerName='Sree Visalam Chit Fund Ltd'");
                                long inscmd = trn.insertorupdateTrn("insert into auctiondetails(`BranchID`,`GroupID`,`AuctionDate`,`ReBidNO`,`DrawNO`,`PrizedMemberID`,MemberID,`CurrentDueAmount`,`NextDueAmount`,PrizedAmount,`IsPrized`) values(" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + "," + iresult + ",'" + balayer.indiandateToMysqlDate(d1) + "'," + RebidNo + "," + drno + "," + iresult1 + "," + fdfdfd + "," + currDueAmt + "," + nextDueAmt + "," + txtchit_value.SelectedItem.Value + ",'F')");
                                tempDAuctiondates = tempDAuctiondates.AddMonths(noofmonths);
                            }
                            else
                                if (drno == 2)
                                {
                                    long inscmd = trn.insertorupdateTrn("insert into auctiondetails(`BranchID`,`GroupID`,`AuctionDate`,`ReBidNO`,`DrawNO`,`CurrentDueAmount`,`IsPrized`) values(" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + "," + iresult + ",'" + balayer.indiandateToMysqlDate(s) + "'," + RebidNo + "," + drno + "," + currDueAmt + ",'I')");
                                    tempDAuctiondates = tempDAuctiondates.AddMonths(noofmonths);
                                }
                    }
                }
                else if (balayer.ToobjectstrEvenNull(cmbchit_category.SelectedItem) == "Trimonthly")
                {
                    for (int drno = 1; drno <= numberofDraw; drno++)
                    {
                        Boolean b = Convert.ToBoolean("sunday".Equals(balayer.ToobjectstrEvenNull(tempDAuctiondates.Date.Day), StringComparison.InvariantCultureIgnoreCase));
                        if (b == true)
                        {
                            dFirstAuctionDate = dFirstAuctionDate.AddDays(1);
                        }
                        string s = tempDAuctiondates.Date.ToString("dd/MM/yyyy");
                        if ((drno != 1) && (drno != 2))
                        {
                            long inscmd = trn.insertorupdateTrn("insert into auctiondetails(`BranchID`,`GroupID`,`AuctionDate`,`ReBidNO`,`DrawNO`,`IsPrized`) values(" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + "," + iresult + ",'" + balayer.indiandateToMysqlDate(s) + "'," + RebidNo + "," + drno + ",'I')");
                            tempDAuctiondates = tempDAuctiondates.AddDays(10);
                        }
                        else
                            if (drno == 1)
                            {
                                string fdfdfd = balayer.GetSingleValue("SELECT min(MemberIDNew) FROM svcf.membermaster where BranchId=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and CustomerName='Sree Visalam Chit Fund Ltd'");
                                long inscmd = trn.insertorupdateTrn("insert into auctiondetails(`BranchID`,`GroupID`,`AuctionDate`,`ReBidNO`,`DrawNO`,`PrizedMemberID`,MemberID,`CurrentDueAmount`,`NextDueAmount`,PrizedAmount,`IsPrized`) values(" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + "," + iresult + ",'" + balayer.indiandateToMysqlDate(d1) + "'," + RebidNo + "," + drno + "," + iresult1 + "," + fdfdfd + "," + currDueAmt + "," + nextDueAmt + "," + txtchit_value.SelectedItem.Value + ",'F')");
                                tempDAuctiondates = tempDAuctiondates.AddDays(10);
                            }
                            else
                                if (drno == 2)
                                {
                                    long inscmd = trn.insertorupdateTrn("insert into auctiondetails(`BranchID`,`GroupID`,`AuctionDate`,`ReBidNO`,`DrawNO`,`CurrentDueAmount`,`IsPrized`) values(" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + "," + iresult + ",'" + balayer.indiandateToMysqlDate(s) + "'," + RebidNo + "," + drno + "," + currDueAmt + ",'I')");
                                    tempDAuctiondates = tempDAuctiondates.AddDays(10);
                                }
                    }
                }
                //Start------Disable payment in group addition
                //System.Guid guid = Guid.NewGuid();
                //string hexstring = BitConverter.ToString(guid.ToByteArray());
                //string guidForBinary16 = "0x" + hexstring.Replace("-", string.Empty);
                //DualTransactionKey = guidForBinary16;
                //string cd1 = balayer.ToobjectstrEvenNull(txtauction_dt1.Text);
                //DateTime dtChoosenDate1 = DateTime.Parse(cd1, usDtfi);
                //string CompanyMemberId = balayer.GetSingleValue("SELECT min(MemberIDNew) FROM svcf.membermaster where BranchId=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and CustomerName='Sree Visalam Chit Fund Ltd'");
                //decimal commision = decimal.Parse(balayer.GetSingleValue("SELECT Commission FROM svcf.commissiondetails where ChitValue=" + txtchit_value.SelectedItem.Value));
                //decimal fcpAmount = decimal.Parse(txtchit_value.SelectedItem.Value) - commision;
                //long dhhffhffhfg = trn.insertorupdateTrn("INSERT INTO `svcf`.`voucher` (`DualTransactionKey`,`BranchID`,`Current_Date`,`Voucher_No`,`Voucher_Type`,`Head_Id`,`ChoosenDate`,`Narration`,`Amount`,`Series`,`ReceievedBy`,`Trans_Type`,`T_Day`,`T_Month`,`T_Year`,`MemberID`,`Trans_Medium`,`RootID`,`ChitGroupId`,`Other_Trans_Type`) VALUES (" + DualTransactionKey + "," + balayer.ToobjectstrEvenNull(Session["Branchid"]) + ",'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," + "000" + ",'D'," + iresult1 + ",'" + dtChoosenDate1.ToString("yyyy-MM-dd") + "','" + "Company chit payment Amount Debited for " + balayer.MySQLEscapeString(txtgroup_no.Text) + "/" + txtno_of_members.Text + "'," + txtchit_value.SelectedItem.Value + ",'" + "FCP" + "','" + balayer.MySQLEscapeString(balayer.ToobjectstrEvenNull(Session["UserName"])) + "',2," + dtChoosenDate1.Day + "," + dtChoosenDate1.Month + "," + dtChoosenDate1.Year + "," + CompanyMemberId + "," + "3" + "," + "5" + "," + iresult + ",4" + ") ");
                //trn.insertorupdateTrn("INSERT INTO `svcf`.`voucher` (`DualTransactionKey`,`BranchID`,`Current_Date`,`Voucher_No`,`Voucher_Type`,`Head_Id`,`ChoosenDate`,`Narration`,`Amount`,`Series`,`ReceievedBy`,`Trans_Type`,`T_Day`,`T_Month`,`T_Year`,`MemberID`,`Trans_Medium`,`RootID`,`ChitGroupId`,`Other_Trans_Type`) VALUES (" + DualTransactionKey + "," + balayer.ToobjectstrEvenNull(Session["Branchid"]) + ",'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," + "000" + ",'C'," + strPrized + ",'" + dtChoosenDate1.ToString("yyyy-MM-dd") + "','" + "Company chit payment Amount Excluding Commision Credited from FCP for " + balayer.MySQLEscapeString(txtgroup_no.Text) + "/" + txtno_of_members.Text + "'," + fcpAmount + ",'" + "FCP" + "','" + balayer.MySQLEscapeString(balayer.ToobjectstrEvenNull(Session["UserName"])) + "',2," + dtChoosenDate1.Day + "," + dtChoosenDate1.Month + "," + dtChoosenDate1.Year + "," + CompanyMemberId + "," + "3" + "," + "6" + "," + iresult + ",4" + ") ");
                //trn.insertorupdateTrn("INSERT INTO `svcf`.`voucher` (`DualTransactionKey`,`BranchID`,`Current_Date`,`Voucher_No`,`Voucher_Type`,`Head_Id`,`ChoosenDate`,`Narration`,`Amount`,`Series`,`ReceievedBy`,`Trans_Type`,`T_Day`,`T_Month`,`T_Year`,`MemberID`,`Trans_Medium`,`RootID`,`ChitGroupId`,`Other_Trans_Type`) VALUES (" + DualTransactionKey + "," + balayer.ToobjectstrEvenNull(Session["Branchid"]) + ",'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," + "000" + ",'C'," + "64" + ",'" + dtChoosenDate1.ToString("yyyy-MM-dd") + "','" + " Commision Credited  for " + balayer.MySQLEscapeString(txtgroup_no.Text) + "/" + txtno_of_members.Text + "'," + commision + ",'" + "FCP" + "','" + balayer.MySQLEscapeString(balayer.ToobjectstrEvenNull(Session["UserName"])) + "',2," + dtChoosenDate1.Day + "," + dtChoosenDate1.Month + "," + dtChoosenDate1.Year + "," + CompanyMemberId + "," + "3" + "," + "11" + "," + iresult + ",4" + ") ");
                //trn.insertorupdateTrn("INSERT INTO `svcf`.`voucher` (`DualTransactionKey`,`BranchID`,`Current_Date`,`Voucher_No`,`Voucher_Type`,`Head_Id`,`ChoosenDate`,`Narration`,`Amount`,`Series`,`ReceievedBy`,`Trans_Type`,`T_Day`,`T_Month`,`T_Year`,`MemberID`,`Trans_Medium`,`RootID`,`ChitGroupId`,`Other_Trans_Type`) VALUES (" + DualTransactionKey + "," + balayer.ToobjectstrEvenNull(Session["Branchid"]) + ",'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," + "000" + ",'D'," + "68" + ",'" + dtChoosenDate1.ToString("yyyy-MM-dd") + "','" + "Foreman Chit Kasar Debited For" + balayer.MySQLEscapeString(txtgroup_no.Text) + "/" + txtno_of_members.Text + "'," + commision + ",'" + "FCP" + "','" + balayer.MySQLEscapeString(balayer.ToobjectstrEvenNull(Session["UserName"])) + "',2," + dtChoosenDate1.Day + "," + dtChoosenDate1.Month + "," + dtChoosenDate1.Year + "," + CompanyMemberId + "," + "3" + "," + "11" + "," + iresult + ",4" + ") ");
                //trn.insertorupdateTrn("INSERT INTO `svcf`.`voucher` (`DualTransactionKey`,`BranchID`,`Current_Date`,`Voucher_No`,`Voucher_Type`,`Head_Id`,`ChoosenDate`,`Narration`,`Amount`,`Series`,`ReceievedBy`,`Trans_Type`,`T_Day`,`T_Month`,`T_Year`,`MemberID`,`Trans_Medium`,`RootID`,`ChitGroupId`,`Other_Trans_Type`) VALUES (" + DualTransactionKey + "," + balayer.ToobjectstrEvenNull(Session["Branchid"]) + ",'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," + "000" + ",'C'," + strPrized + ",'" + dtChoosenDate1.ToString("yyyy-MM-dd") + "','" + "cc credited from fc kasar for " + balayer.MySQLEscapeString(txtgroup_no.Text) + "/" + txtno_of_members.Text + "'," + commision + ",'" + "FCP" + "','" + balayer.MySQLEscapeString(balayer.ToobjectstrEvenNull(Session["UserName"])) + "',2," + dtChoosenDate1.Day + "," + dtChoosenDate1.Month + "," + dtChoosenDate1.Year + "," + CompanyMemberId + "," + "3" + "," + "6" + "," + iresult + ",4" + ") ");
                //trn.insertorupdateTrn("insert into trans_payment (`TransactionKey_Bank`,`TransactionKey`,`DualTransactionKey`,`BranchID`,`ChitGroupID`,`DrawNo`,`PaymentApplyedOn`,`ApprovedOn`,`PaymentDate`,`GuarantorID`,`Description`,`ChitAmount`,`PrizedAmount`,`AuctionDate`,`NextDueAmount`,`NextDrawNo`,`AOSanctionNo`,`CurrentDueAmount`,`TokenNumber`,`GuarantorName`) values (null ," + dhhffhffhfg + "," + DualTransactionKey + "," + balayer.ToobjectstrEvenNull(Session["Branchid"]) + "," + iresult + ",1,'" + dtChoosenDate1.ToString("yyyy-MM-dd") + "','" + dtChoosenDate1.ToString("yyyy-MM-dd") + "','" + dtChoosenDate1.ToString("yyyy-MM-dd") + "',null,'-'," + txtchit_value.SelectedItem.Value + "," + txtchit_value.SelectedItem.Value + ",'" + dtChoosenDate1.ToString("yyyy-MM-dd") + "'," + nextDueAmt + ",2,1," + currDueAmt + "," + iresult1 + ",'Sree visalam chit Fund Limited')");
                //trn.insertorupdateTrn("update auctiondetails set IsPrized='Y' where PrizedMemberID=" + iresult1 + " and DrawNO=1");
                //End------Disable payment in group addition
                ModalPopupExtender2.PopupControlID = "Pnlmsg";
                this.ModalPopupExtender2.Show();
                Pnlmsg.Visible = true;
                lblT.Text = "Status";
                lblContent.Text = "Group : " + txtgroup_no.Text + " inserted Successfully";
                lblContent.ForeColor = System.Drawing.Color.Green;
                trn.CommitTrn();
                logger.Info("groups.aspx - btnaddGroup_Click():  Completed: " + DateTime.Now + " by: " + Convert.ToString(Session["UserName"]) + "");
            }
            catch (Exception error)
            {
                try 
                {
                   trn.RollbackTrn();
                   logger.Info("groups.aspx - btnaddGroup_Click():  Error:"  + error.Message + ": " + DateTime.Now + " by: " + Convert.ToString(Session["UserName"]) + "");
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
        protected void IndianDateValidator_ServerValidate(object sender, ServerValidateEventArgs e)
        {
            DateTime d;
            e.IsValid = DateTime.TryParseExact(e.Value, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out d);
        }

        protected int AuctionDates()
        {
            int numofmon;
            if (balayer.ToobjectstrEvenNull(cmbchit_category.SelectedItem) == "Monthly")
            {
                numofmon = 1;
                return numofmon;
            }
            else
                if (balayer.ToobjectstrEvenNull(cmbchit_category.SelectedItem) == "Trimonthly")
                {
                    numofmon = 3;
                    return numofmon;
                }
                else
                {
                    return 0;
                }
        }

        protected void clearGroup()
        {
            Response.Redirect(Request.Url.AbsolutePath.ToString());
        }

        protected void BtnCancel_click(object sender, EventArgs e)
        {
            clearGroup();
        }
    }
}
