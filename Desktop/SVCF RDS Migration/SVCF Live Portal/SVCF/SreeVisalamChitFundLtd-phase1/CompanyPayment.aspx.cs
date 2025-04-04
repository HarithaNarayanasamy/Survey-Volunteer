using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SVCF_BusinessAccessLayer;
using SVCF_DataAccessLayer;
using SVCF_TransactionLayer;
using log4net;
using log4net.Config;

namespace SreeVisalamChitFundLtd_phase1
{
    public partial class CompanyPayment : System.Web.UI.Page
    {
        #region Object
        BusinessLayer balayer = new BusinessLayer();

        #endregion

        string userinfo = "";
        string qry = "";
        string usrRole = "";

        ILog logger = log4net.LogManager.GetLogger(typeof(CompanyPayment));

        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (Session["UserName"] == null || Session["Branchid"] == null || Session["BranchName"] == null)
            {
                Response.Redirect(Page.ResolveUrl("~/Login.aspx"), true);
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            pnlmsg.Visible = false;
            if (!IsPostBack)
            {

                userinfo = HttpContext.Current.User.Identity.Name;
                qry = "select rs.name from roles as rs inner join rights as rt on (rt.roleid=rs.id) where memberid=" + userinfo + "";
                usrRole = balayer.GetSingleValue(qry);
                if (usrRole == "Report")
                {
                    Response.Redirect("Home.aspx", false);
                }

                Session["CheckRefresh"] = System.Guid.NewGuid().ToString();
                clear();
                txtDate.ToolTip = "Ex. " + DateTime.Now.ToString("dd/MM/yyyy") + " (dd/mm/yyyy)";
            }
            rvDate.MaximumValue = DateTime.Now.ToString("dd/MM/yyyy");
            rvDate.MinimumValue = balayer.GetSingleValue("SELECT DATE_FORMAT( min(MinimumDate), '%d/%m/%Y') MinimumDate FROM svcf.restrictionmaster where BranchID=" + Session["Branchid"]); ;
        }
        protected void Page_PreRender(object sender, EventArgs e)
        {
            ViewState["CheckRefresh"] = Session["CheckRefresh"];
        }
        public void clear()
        {
            LabelPrizedAmount.Text = "Member Name";
            txtPrizedAmount.Text = "0.00";
            txtCommision.Text = "0.00";
            txtDebitAmount.Text = "0.00";
            ddlMemberName.Items.Clear();
            txtDrawNumber.Text = "";
            txtKasar.Text = "0.00";
            txtDate.Text = "";
            getGroup();
        }
        public void getGroup()
        {
            DataTable dtchitgrpno = balayer.GetDataTable("Select distinct(groupmaster.GROUPNO) ,auctiondetails.GroupID from auctiondetails join groupmaster on (auctiondetails.GroupID=groupmaster.Head_Id) where (auctiondetails.IsPrized='N' or auctiondetails.IsPrized='F') and auctiondetails.DrawNO=1 and auctiondetails.BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + "");
            DataRow drchitgrpno = dtchitgrpno.NewRow();
            drchitgrpno[0] = "--Select--";
            drchitgrpno[1] = "0";
            ddlGroupNumber.DataSource = dtchitgrpno;
            ddlGroupNumber.DataTextField = "GROUPNO";
            ddlGroupNumber.DataValueField = "GroupID";
            dtchitgrpno.Rows.InsertAt(drchitgrpno, 0);
            ddlGroupNumber.DataBind();
        }

        protected void load_ddlGroupNumber(object sender, EventArgs e)
        {
            if (ddlGroupNumber.SelectedItem.Text != "--Select--")
            {
                decimal chitValue = Convert.ToDecimal(balayer.GetSingleValue("SELECT ChitValue FROM svcf.groupmaster where Head_Id=" + ddlGroupNumber.SelectedItem.Value + ""));
                txtDebitAmount.Text = chitValue.ToString();
                DataTable dtAmount = balayer.GetDataTable("SELECT Commission FROM svcf.commissiondetails where chitValue=" + chitValue + "");
                txtCommision.Text = dtAmount.Rows[0][0].ToString();
                
                txtKasar.Text = dtAmount.Rows[0][0].ToString();
                DataTable dtchit = balayer.GetDataTable("SELECT DISTINCT auctiondetails.PrizedMemberID, concat( membertogroupmaster.GrpMemberID,'|', membertogroupmaster.MemberName) as MemberName from auctiondetails inner join membertogroupmaster on auctiondetails.PrizedMemberID=membertogroupmaster.Head_Id where auctiondetails.GroupID=" + ddlGroupNumber.SelectedItem.Value + " and (auctiondetails.IsPrized='N' or auctiondetails.IsPrized='F') and auctiondetails.DrawNO=1");
                decimal PrizedAmount = Convert.ToDecimal(txtDebitAmount.Text) - Convert.ToDecimal(txtCommision.Text);
                txtPrizedAmount.Text = chitValue.ToString();   // chitValue.ToString();

                ddlMemberName.DataSource = dtchit;
                ddlMemberName.DataTextField = "MemberName";
                ddlMemberName.DataValueField = "PrizedMemberID";
                ddlMemberName.DataBind();
                ddlMemberName.Focus();
                ListItem lst1 = new ListItem("--Select--", "--Select--");
                ddlMemberName.Items.Insert(0, lst1);
                txtDrawNumber.Text = "";
            }
            else
            {
                ddlMemberName.DataSource = null;
                ddlMemberName.Items.Clear();
                ddlMemberName.DataBind();
                txtDrawNumber.Text = "";
                txtKasar.Text = "0.00";
                txtPrizedAmount.Text = "0.00";
                txtDebitAmount.Text = "0.00";
                txtCommision.Text = "0.00";
            }
        }
        protected void load_ddlMemberName(object sender, EventArgs e)
        {
            if (ddlGroupNumber.SelectedItem.Text != "--Select--")
            {
                if (ddlMemberName.SelectedItem.Text != "--Select--")
                {
                    //decimal strForemanChitPrized = Convert.ToDecimal(balayer.GetSingleValue("SELECT PrizedAmount FROM svcf.auctiondetails where GroupID=" + ddlGroupNumber.SelectedItem.Value + " and PrizedMemberID=" + ddlMemberName.SelectedItem.Value + " and (IsPrized='N' or IsPrized='F') and DrawNO=1 and BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + ""));
                    int DrawNo = 1;
                    txtDrawNumber.Text = DrawNo.ToString();
                    LabelPrizedAmount.Text = (ddlMemberName.SelectedItem.Text).Split('|')[0];
                    //decimal chitValue = Convert.ToDecimal(txtDebitAmount.Text);
                    //decimal commission = Convert.ToDecimal(balayer.GetSingleValue("SELECT Commission FROM svcf.commissiondetails where chitValue=" + chitValue + " "));
                    //int totalMembers = Convert.ToInt32(balayer.GetSingleValue("SELECT `groupmaster`.`NoofMembers` FROM svcf.groupmaster where Head_Id=" + ddlGroupNumber.SelectedItem.Value + ""));
                    //txtPrizedAmount.Text = (strForemanChitPrized).ToString();
                }
                else
                {
                    txtDrawNumber.Text = "";
                    LabelPrizedAmount.Text = "";
                    txtPrizedAmount.Text = "0.00";
                }
            }
        }

        protected void load_Payment(object sender, EventArgs e)
        {
            Page.Validate("pa");
            if (!Page.IsValid)
            {
                return;
            }
            if (Session["CheckRefresh"].ToString() != ViewState["CheckRefresh"].ToString())
            {
                return;
            }
            decimal ban = 0;
            bool isban = decimal.TryParse(txtPrizedAmount.Text, out ban);
            if (!isban)
            {
                txtPrizedAmount.Text = "0.00";
            }
            decimal com = 0;
            bool iscom = decimal.TryParse(txtCommision.Text, out com);
            if (!iscom)
            {
                txtCommision.Text = "0.00";
            }
            decimal Credit = 0.00m;
            decimal Debit = 0.00m;
            if (Credit != Debit)
            {
                ModalPopupExtender1.PopupControlID = "pnlmsg";
                ModalPopupExtender1.Show();
                pnlmsg.Visible = true;
                lblh.Text = "Status";
                lblcon.Text = "Credit = " + Credit + " and Debit = " + Debit + " is not same!!!'";
                lblcon.ForeColor = System.Drawing.Color.Green;
                BtnOK.Visible = false;
                Button2.Visible = false;
                Button3.Visible = false;
                btnHide.Visible = true;
            }
            else
            {
                ModalPopupExtender1.PopupControlID = "pnlmsg";
                ModalPopupExtender1.Show();
                pnlmsg.Visible = true;
                lblh.Text = "Status";
                lblcon.Text = "Do you want to insert the payment";
                lblcon.ForeColor = System.Drawing.Color.Green;
                BtnOK.Visible = true;
                Button2.Visible = false;
                Button3.Visible = true;
                btnHide.Visible = false;
            }
        }
        protected void load_cancel(object sender, EventArgs e)
        {
            Response.Redirect(Request.Url.AbsolutePath.ToString());
        }
        protected void load_hidePanel(object sender, EventArgs e)
        {
            pnlmsg.Visible = false;
            ModalPopupExtender1.Hide();
        }
        protected static string indianToMysqlDate(string ddmmyy)
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
                strAuctionDate = ddmmyy.Split('/')[2] + "-" + ddmmyy.Split('/')[1] + "-" + ddmmyy.Split('/')[0];
            }
            return strAuctionDate;
        }
        protected void btn_ok(object sender, EventArgs e)
        {
            
            TransactionLayer trn = new TransactionLayer();

            //Voucher number
            string Voucher_No = balayer.GetSingleValue("SELECT max(Voucher_No)+1 FROM svcf.voucher where  BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and Trans_Type=2 and Series='FCP'");

            string strNarration = "";
            string DualTransactionKey = "";
            string DrawNo = "";
            try
            {

                //strNarration = balayer.MySQLEscapeString(ddlMemberName.SelectedItem.Text).Split('|')[0] + " through payment of Draw Number :" + txtDrawNumber.Text;
                //System.Guid guid = Guid.NewGuid();
                //string guidForChar36 = guid.ToString();
                //string hexstring = BitConverter.ToString(guid.ToByteArray());
                //string guidForBinary16 = "0x" + hexstring.Replace("-", string.Empty);
                //DualTransactionKey = guidForBinary16;
                //string GetMemberId = balayer.GetSingleValue("SELECT MemberID FROM svcf.membertogroupmaster where Head_Id=" + ddlMemberName.SelectedItem.Value + "");
                //long GetCommision = trn.insertorupdateTrn("insert into voucher (`DualTransactionKey`,`BranchID`,`CurrDate`,`Voucher_No`,`Voucher_Type`,`Head_Id`,`ChoosenDate`,`Narration`,`Amount`,`Series`,`ReceievedBy`,`Trans_Type`,`T_Day`,`T_Month`,`T_Year`,`MemberID`,`Trans_Medium`,`RootID`,`ChitGroupId`,`Other_Trans_Type`) values (" + DualTransactionKey + "," + balayer.ToobjectstrEvenNull(Session["Branchid"]) + ",'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," + txtPaymentNumber.Text + ",'C',64,'" + balayer.indiandateToMysqlDate(txtDate.Text) + "','Commision for " + strNarration + "'," + txtCommision.Text + ",'" + txtSeries.Text + "','" + balayer.MySQLEscapeString(balayer.ToobjectstrEvenNull(Session["UserName"])) + "',2," + balayer.indiandateToMysqlDate(txtDate.Text).Split('/')[2] + "," + balayer.indiandateToMysqlDate(txtDate.Text).Split('/')[1] + "," + balayer.indiandateToMysqlDate(txtDate.Text).Split('/')[0] + "," + GetMemberId + ",0,11," + ddlGroupNumber.SelectedItem.Value + ",0) ");
                //long GetDebitAmount = trn.insertorupdateTrn("insert into voucher (`DualTransactionKey`,`BranchID`,`Current_Date`,`Voucher_No`,`Voucher_Type`,`Head_Id`,`ChoosenDate`,`Narration`,`Amount`,`Series`,`ReceievedBy`,`Trans_Type`,`T_Day`,`T_Month`,`T_Year`,`MemberID`,`Trans_Medium`,`RootID`,`ChitGroupId`,`Other_Trans_Type`) values (" + DualTransactionKey + "," + balayer.ToobjectstrEvenNull(Session["Branchid"]) + ",'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," + txtPaymentNumber.Text + ",'D'," + ddlMemberName.SelectedItem.Value + ",'" + balayer.indiandateToMysqlDate(txtDate.Text) + "','Debit Amount for " + strNarration + "'," + txtDebitAmount.Text + ",'" + txtSeries.Text + "','" + balayer.MySQLEscapeString(balayer.ToobjectstrEvenNull(Session["UserName"])) + "',2," + balayer.indiandateToMysqlDate(txtDate.Text).Split('/')[2] + "," + balayer.indiandateToMysqlDate(txtDate.Text).Split('/')[1] + "," + balayer.indiandateToMysqlDate(txtDate.Text).Split('/')[0] + "," + GetMemberId + ",0,5," + ddlGroupNumber.SelectedItem.Value + ",0)");
                //string ForemanSubstitutedChitPrizedAmountHeadid = balayer.GetSingleValue("select foremansubstitutedchitprized from removedmaster where GroupmemberID=" + ddlMemberName.SelectedItem.Value);
                //long ForemanSubstitutedChitPrizedAmount = trn.insertorupdateTrn("insert into voucher (`DualTransactionKey`,`BranchID`,`Current_Date`,`Voucher_No`,`Voucher_Type`,`Head_Id`,`ChoosenDate`,`Narration`,`Amount`,`Series`,`ReceievedBy`,`Trans_Type`,`T_Day`,`T_Month`,`T_Year`,`MemberID`,`Trans_Medium`,`RootID`,`ChitGroupId`,`Other_Trans_Type`) values (" + DualTransactionKey + "," + balayer.ToobjectstrEvenNull(Session["Branchid"]) + ",'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," + txtPaymentNumber.Text + ",'C'," + ForemanSubstitutedChitPrizedAmountHeadid + ",'" + balayer.indiandateToMysqlDate(txtDate.Text) + "','Foreman Substituted Chit Prized Amount for " + strNarration + "'," + txtPrizedAmount.Text + ",'" + balayer.MySQLEscapeString(txtSeries.Text) + "','" + balayer.MySQLEscapeString(balayer.ToobjectstrEvenNull(Session["UserName"])) + "',2," + balayer.indiandateToMysqlDate(txtDate.Text).Split('/')[2] + "," + balayer.indiandateToMysqlDate(txtDate.Text).Split('/')[1] + "," + balayer.indiandateToMysqlDate(txtDate.Text).Split('/')[0] + "," + GetMemberId + ",3,6," + ddlGroupNumber.SelectedItem.Value + ",6)");
                //DataTable auction = balayer.GetDataTable("SELECT `auctiondetails`.`PrizedMemberID`,`auctiondetails`.`GroupID`,`auctiondetails`.`AuctionDate`,`auctiondetails`.`NextDueAmount`,`auctiondetails`.`PrizedAmount`,`groupmaster`.`ChitValue`,`auctiondetails`.`CurrentDueAmount`  FROM svcf.auctiondetails left join groupmaster on (`auctiondetails`.`GroupID`=`groupmaster`.`Head_Id`) where `auctiondetails`.`GroupID`=" + ddlGroupNumber.SelectedItem.Value + " and `auctiondetails`.`PrizedMemberID`=" + ddlMemberName.SelectedItem.Value + " and `auctiondetails`.`BranchID`=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + "");
                //string AuctionDt = auction.Rows[0]["AuctionDate"].ToString();
                //decimal NextDue = Convert.ToDecimal(auction.Rows[0]["NextDueAmount"]);
                //decimal Priced = Convert.ToDecimal(auction.Rows[0]["PrizedAmount"]);
                //decimal chitvalue = Convert.ToDecimal(auction.Rows[0]["ChitValue"]);
                //decimal CurrentDue = Convert.ToDecimal(auction.Rows[0]["CurrentDueAmount"]);
                //long TransPaymentQuery = trn.insertorupdateTrn("insert into trans_payment (`TransactionKey_Bank`,`TransactionKey`,`DualTransactionKey`,`BranchID`,`ChitGroupID`,`DrawNo`,`PaymentApplyedOn`,`ApprovedOn`,`PaymentDate`,`GuarantorID`,`Description`,`ChitAmount`,`PrizedAmount`,`AuctionDate`,`NextDueAmount`,`NextDrawNo`,`AOSanctionNo`,`CurrentDueAmount`,`TokenNumber`,`GuarantorName`,`Flags`) values (null," + GetDebitAmount + "," + DualTransactionKey + "," + balayer.ToobjectstrEvenNull(Session["Branchid"]) + "," + ddlGroupNumber.SelectedItem.Value + "," + txtDrawNumber.Text + ",'" + balayer.indiandateToMysqlDate(txtApplyedOn.Text) + "','" + balayer.indiandateToMysqlDate(txtPaymentonDate.Text) + "','" + balayer.indiandateToMysqlDate(txtDate.Text) + "','1','" + balayer.MySQLEscapeString(txtDescription.Text) + "'," + chitvalue + "," + Priced + ",'" + balayer.indiandateToMysqlDate(AuctionDt) + "'," + 1 + "," + 2 + "," + txtAOSanctiion.Text + "," + CurrentDue + "," + ddlMemberName.SelectedItem.Value + ",'Sree Visalam Chit Fund Limited',2)");
                //DrawNo = txtDrawNumber.Text;
                //long s = trn.insertorupdateTrn("update auctiondetails set IsPrized='Y' where PrizedMemberID=" + ddlMemberName.SelectedValue + " and DrawNO=" + DrawNo + "");
                System.Globalization.DateTimeFormatInfo usDtfi = new System.Globalization.CultureInfo("en-NZ", false).DateTimeFormat;
                System.Guid guid = Guid.NewGuid();
                string hexstring = BitConverter.ToString(guid.ToByteArray());
                string guidForBinary16 = "0x" + hexstring.Replace("-", string.Empty);
                DualTransactionKey = guidForBinary16;
                string cd1 = balayer.ToobjectstrEvenNull(txtDate.Text);
                DateTime dtChoosenDate1 = DateTime.Parse(cd1, usDtfi);
                string CompanyMemberId = balayer.GetSingleValue("SELECT min(MemberIDNew) FROM svcf.membermaster where BranchId=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and CustomerName='Sree Visalam Chit Fund Ltd'");
                decimal commision = decimal.Parse(balayer.GetSingleValue("SELECT Commission FROM svcf.commissiondetails where ChitValue=" + txtPrizedAmount.Text));
                commision = 0;
                decimal fcpAmount = decimal.Parse(txtPrizedAmount.Text) - commision;
                string strPrized = balayer.GetSingleValue("select NodeID from headstree where ParentID=1052 and Node='" + balayer.MySQLEscapeString(ddlMemberName.SelectedItem.Text).Split('|')[0] + "'");
                long dhhffhffhfg = trn.insertorupdateTrn("INSERT INTO `svcf`.`voucher` (`DualTransactionKey`,`BranchID`,`CurrDate`,`Voucher_No`,`Voucher_Type`,`Head_Id`,`ChoosenDate`,`Narration`,`Amount`,`Series`,`ReceievedBy`,`Trans_Type`,`T_Day`,`T_Month`,`T_Year`,`MemberID`,`Trans_Medium`,`RootID`,`ChitGroupId`,`Other_Trans_Type`) VALUES (" + DualTransactionKey + "," + balayer.ToobjectstrEvenNull(Session["Branchid"]) + ",'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," + Voucher_No + ",'D'," + ddlMemberName.SelectedItem.Value + ",'" + dtChoosenDate1.ToString("yyyy-MM-dd") + "','" + "Company chit payment Amount Debited for " + balayer.MySQLEscapeString(ddlMemberName.SelectedItem.Text).Split('|')[0] + "'," + txtPrizedAmount.Text + ",'" + "FCP" + "','" + balayer.MySQLEscapeString(balayer.ToobjectstrEvenNull(Session["UserName"])) + "',2," + dtChoosenDate1.Day + "," + dtChoosenDate1.Month + "," + dtChoosenDate1.Year + "," + CompanyMemberId + "," + "3" + "," + "5" + "," + ddlGroupNumber.SelectedItem.Value + ",4" + ") ");
                //Credit Entry
                long CreditEntry = trn.insertorupdateTrn("INSERT INTO `svcf`.`voucher` (`DualTransactionKey`,`BranchID`,`CurrDate`,`Voucher_No`,`Voucher_Type`,`Head_Id`,`ChoosenDate`,`Narration`,`Amount`,`Series`,`ReceievedBy`,`Trans_Type`,`T_Day`,`T_Month`,`T_Year`,`MemberID`,`Trans_Medium`,`RootID`,`ChitGroupId`,`Other_Trans_Type`) VALUES (" + DualTransactionKey + "," + balayer.ToobjectstrEvenNull(Session["Branchid"]) + ",'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," + Voucher_No + ",'C'," + strPrized + ",'" + dtChoosenDate1.ToString("yyyy-MM-dd") + "','" + "Company chit payment Amount Credited from FCP for " + balayer.MySQLEscapeString(ddlMemberName.SelectedItem.Text).Split('|')[0] + "'," + fcpAmount + ",'" + "FCP" + "','" + balayer.MySQLEscapeString(balayer.ToobjectstrEvenNull(Session["UserName"])) + "',2," + dtChoosenDate1.Day + "," + dtChoosenDate1.Month + "," + dtChoosenDate1.Year + "," + CompanyMemberId + "," + "3" + "," + "6" + "," + ddlGroupNumber.SelectedItem.Value + ",4" + ") ");

                //Debit Entry
               // trn.insertorupdateTrn("INSERT INTO `svcf`.`voucher` (`DualTransactionKey`,`BranchID`,`CurrDate`,`Voucher_No`,`Voucher_Type`,`Head_Id`,`ChoosenDate`,`Narration`,`Amount`,`Series`,`ReceievedBy`,`Trans_Type`,`T_Day`,`T_Month`,`T_Year`,`MemberID`,`Trans_Medium`,`RootID`,`ChitGroupId`,`Other_Trans_Type`) VALUES (" + DualTransactionKey + "," + balayer.ToobjectstrEvenNull(Session["Branchid"]) + ",'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," + "000" + ",'D'," + strPrized + ",'" + dtChoosenDate1.ToString("yyyy-MM-dd") + "','" + "Company chit payment Amount Debited from FCP for " + balayer.MySQLEscapeString(ddlMemberName.SelectedItem.Text).Split('|')[0] + "'," + fcpAmount + ",'" + "FCP" + "','" + balayer.MySQLEscapeString(balayer.ToobjectstrEvenNull(Session["UserName"])) + "',2," + dtChoosenDate1.Day + "," + dtChoosenDate1.Month + "," + dtChoosenDate1.Year + "," + CompanyMemberId + "," + "3" + "," + "5" + "," + ddlGroupNumber.SelectedItem.Value + ",4" + ") ");

                //trn.insertorupdateTrn("INSERT INTO `svcf`.`voucher` (`DualTransactionKey`,`BranchID`,`CurrDate`,`Voucher_No`,`Voucher_Type`,`Head_Id`,`ChoosenDate`,`Narration`,`Amount`,`Series`,`ReceievedBy`,`Trans_Type`,`T_Day`,`T_Month`,`T_Year`,`MemberID`,`Trans_Medium`,`RootID`,`ChitGroupId`,`Other_Trans_Type`) VALUES (" + DualTransactionKey + "," + balayer.ToobjectstrEvenNull(Session["Branchid"]) + ",'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," + "000" + ",'C'," + "64" + ",'" + dtChoosenDate1.ToString("yyyy-MM-dd") + "','" + " Commision Credited  for " + balayer.MySQLEscapeString(ddlMemberName.SelectedItem.Text).Split('|')[0] + "'," + commision + ",'" + "FCP" + "','" + balayer.MySQLEscapeString(balayer.ToobjectstrEvenNull(Session["UserName"])) + "',2," + dtChoosenDate1.Day + "," + dtChoosenDate1.Month + "," + dtChoosenDate1.Year + "," + CompanyMemberId + "," + "3" + "," + "11" + "," + ddlGroupNumber.SelectedItem.Value + ",4" + ") ");
                //trn.insertorupdateTrn("INSERT INTO `svcf`.`voucher` (`DualTransactionKey`,`BranchID`,`CurrDate`,`Voucher_No`,`Voucher_Type`,`Head_Id`,`ChoosenDate`,`Narration`,`Amount`,`Series`,`ReceievedBy`,`Trans_Type`,`T_Day`,`T_Month`,`T_Year`,`MemberID`,`Trans_Medium`,`RootID`,`ChitGroupId`,`Other_Trans_Type`) VALUES (" + DualTransactionKey + "," + balayer.ToobjectstrEvenNull(Session["Branchid"]) + ",'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," + "000" + ",'D'," + "68" + ",'" + dtChoosenDate1.ToString("yyyy-MM-dd") + "','" + "Foreman Chit Kasar Debited For " + balayer.MySQLEscapeString(ddlMemberName.SelectedItem.Text).Split('|')[0] + "'," + commision + ",'" + "FCP" + "','" + balayer.MySQLEscapeString(balayer.ToobjectstrEvenNull(Session["UserName"])) + "',2," + dtChoosenDate1.Day + "," + dtChoosenDate1.Month + "," + dtChoosenDate1.Year + "," + CompanyMemberId + "," + "3" + "," + "11" + "," + ddlGroupNumber.SelectedItem.Value + ",4" + ") ");
                //trn.insertorupdateTrn("INSERT INTO `svcf`.`voucher` (`DualTransactionKey`,`BranchID`,`CurrDate`,`Voucher_No`,`Voucher_Type`,`Head_Id`,`ChoosenDate`,`Narration`,`Amount`,`Series`,`ReceievedBy`,`Trans_Type`,`T_Day`,`T_Month`,`T_Year`,`MemberID`,`Trans_Medium`,`RootID`,`ChitGroupId`,`Other_Trans_Type`) VALUES (" + DualTransactionKey + "," + balayer.ToobjectstrEvenNull(Session["Branchid"]) + ",'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," + "000" + ",'C'," + strPrized + ",'" + dtChoosenDate1.ToString("yyyy-MM-dd") + "','CC credited from fc kasar for " + balayer.MySQLEscapeString(ddlMemberName.SelectedItem.Text).Split('|')[0] + "'," + commision + ",'" + "FCP" + "','" + balayer.MySQLEscapeString(balayer.ToobjectstrEvenNull(Session["UserName"])) + "',2," + dtChoosenDate1.Day + "," + dtChoosenDate1.Month + "," + dtChoosenDate1.Year + "," + CompanyMemberId + "," + "3" + "," + "6" + "," + ddlGroupNumber.SelectedItem.Value + ",4" + ") ");

                decimal decDue = Convert.ToDecimal(txtPrizedAmount.Text)/ Convert.ToDecimal(balayer.GetSingleValue("SELECT NoofMembers FROM svcf.groupmaster where Head_Id="+ddlGroupNumber.SelectedItem.Value));

                trn.insertorupdateTrn("insert into trans_payment (`TransactionKey_Bank`,`TransactionKey`,`DualTransactionKey`,`BranchID`,`ChitGroupID`,`DrawNo`,`PaymentApplyedOn`,`ApprovedOn`,`PaymentDate`,`GuarantorID`,`Description`,`ChitAmount`,`PrizedAmount`,`AuctionDate`,`NextDueAmount`,`NextDrawNo`,`AOSanctionNo`,`CurrentDueAmount`,`TokenNumber`,`GuarantorName`) values (null ," + dhhffhffhfg + "," + DualTransactionKey + "," + balayer.ToobjectstrEvenNull(Session["Branchid"]) + "," + ddlGroupNumber.SelectedItem.Value + ",1,'" + dtChoosenDate1.ToString("yyyy-MM-dd") + "','" + dtChoosenDate1.ToString("yyyy-MM-dd") + "','" + dtChoosenDate1.ToString("yyyy-MM-dd") + "',null,'-'," + txtPrizedAmount.Text + "," + fcpAmount + ",'" + dtChoosenDate1.ToString("yyyy-MM-dd") + "'," + decDue + ",2,1," + decDue + "," + ddlMemberName.SelectedItem.Value + ",'Sree visalam chit Fund Limited')");
                trn.insertorupdateTrn("update auctiondetails set IsPrized='Y' where PrizedMemberID=" + ddlMemberName.SelectedItem.Value + " and DrawNO=1");

                ModalPopupExtender1.PopupControlID = "pnlmsg";
                ModalPopupExtender1.Show();
                pnlmsg.Visible = true;
                lblh.Text = "Status";
                lblcon.Text = ddlMemberName.SelectedItem.Text + " Payment Inserted Successfully";
                lblcon.ForeColor = System.Drawing.Color.Green;
                BtnOK.Visible = false;
                Button2.Visible = true;
                Button3.Visible = false;
                btnHide.Visible = false;
                trn.CommitTrn();
                logger.Info("CompanyPayment.aspx - btn_ok() - Completed: " + DateTime.Now + " by: " + Convert.ToString(Session["UserName"]) + "");
            }
            catch (Exception error)
            {
                try
                {
                   trn.RollbackTrn();
                   logger.Info("CompanyPayment.aspx - btn_ok() - Error: " + error.Message + ": " + DateTime.Now + " by: " + Convert.ToString(Session["UserName"]) + "");
                   // balayer.GetInsertItem("update `auctiondetails` set IsPrized='N' where PrizedMemberID='" + ddlMemberName.SelectedValue + "' and DrawNO=" + DrawNo);
                }
                catch (Exception ex)
                { }
                finally
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Warning", "alert('" + error.Message.ToString().Replace("'", "\\'") + "');", true);
                }
            }
            finally
            {
                trn.DisposeTrn();
            }
        }
    }
}