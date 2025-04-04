using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SVCF_BusinessAccessLayer;
using SVCF_TransactionLayer;
using SVCF_DataAccessLayer;
using System.Text.RegularExpressions;
using log4net;
using log4net.Config;

namespace SreeVisalamChitFundLtd_phase1
{
    public partial class CompanySubstitutedChitPayment : System.Web.UI.Page
    {
      
        #region Object
        BusinessLayer balayer = new BusinessLayer();
        TransactionLayer trn = new TransactionLayer();
        #endregion


        string userinfo = "";
        string qry = "";
        string usrRole = "";

        ILog logger = log4net.LogManager.GetLogger(typeof(CompanySubstitutedChitPayment));

      
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
                txtPaymentonDate.ToolTip = "Ex. " + DateTime.Now.ToString("dd/MM/yyyy") + " (dd/mm/yyyy)";
                txtDate.ToolTip = "Ex. " + DateTime.Now.ToString("dd/MM/yyyy") + " (dd/mm/yyyy)";
                txtApplyedOn.ToolTip = "Ex. " + DateTime.Now.ToString("dd/MM/yyyy") + " (dd/mm/yyyy)";

                string voucherno = "";
                voucherno = balayer.GetSingleValue("SELECT max(Voucher_No) FROM svcf.voucher where  BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and Trans_Type=2 and Series='PAYMENT'");
                txtPaymentNumber.Text = Convert.ToString(Convert.ToInt32(voucherno) + 1);

            }

            rvDate.MaximumValue = DateTime.Now.ToString("dd/MM/yyyy");
            rvDate.MinimumValue = balayer.GetSingleValue("SELECT DATE_FORMAT( min(MinimumDate), '%d/%m/%Y') MinimumDate FROM svcf.restrictionmaster where BranchID=" + Session["Branchid"]); 

            txtPaymentNumber.ReadOnly = true;
        }
        protected void Page_PreRender(object sender, EventArgs e)
        {
            ViewState["CheckRefresh"] = Session["CheckRefresh"];
        }
        public void clear()
        {
            txtSeries.Text = "PAYMENT";
            txtPaymentNumber.Text = "";
            LabelPrizedAmount.Text = "Member Name";
            txtPaymentNumber.Focus();
            txtBankAmount.Text = "0.00";
            txtCommision.Text = "0.00";
            txtDebitAmount.Text = "0.00";
            ddlMemberName.Items.Clear();
            txtDrawNumber.Text = "";
            txtPaymentonDate.Text = "";
            txtDescription.Text = "";
            txtApplyedOn.Text = "";
            txtDate.Text = "";
            getGroup();
        }
        public void getGroup()
        {
            DataTable dtchitgrpno =  balayer.GetDataTable("Select distinct(groupmaster.GROUPNO) ,auctiondetails.GroupID from auctiondetails join groupmaster on (auctiondetails.GroupID=groupmaster.Head_Id) where auctiondetails.IsPrized='N' and auctiondetails.BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + "");
            DataRow drchitgrpno = dtchitgrpno.NewRow();
            drchitgrpno[0] = "--Select--";
            drchitgrpno[1] = "0";
            ddlGroupNumber.DataSource = dtchitgrpno;
            ddlGroupNumber.DataTextField = "GROUPNO";
            ddlGroupNumber.DataValueField = "GroupID";
            dtchitgrpno.Rows.InsertAt(drchitgrpno, 0);
            ddlGroupNumber.DataBind();
        }

        protected void txtPaymentNumber_Validate(object sender, ServerValidateEventArgs e)
        {
            DataTable ssss = balayer.GetDataTable("SELECT Voucher_No FROM svcf.voucher where Voucher_No='" + txtPaymentNumber.Text + "' and BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and Trans_Type=2");
            if (ssss.Rows.Count > 0)
            {
                CustomValidator2.ErrorMessage = "Already exists";
                e.IsValid = false;
            }
            else
                e.IsValid = true;
        }

        protected void load_ddlGroupNumber(object sender, EventArgs e)
        {
            if (ddlGroupNumber.SelectedItem.Text != "--Select--")
            {
                decimal chitValue = Convert.ToDecimal(balayer.GetSingleValue("SELECT ChitValue FROM svcf.groupmaster where Head_Id=" + ddlGroupNumber.SelectedItem.Value + ""));
                txtDebitAmount.Text = chitValue.ToString();
                DataTable dtAmount = balayer.GetDataTable("SELECT Commission FROM svcf.commissiondetails where chitValue=" + chitValue + "");
                txtCommision.Text = dtAmount.Rows[0][0].ToString();
                DataTable dtchit = balayer.GetDataTable("SELECT DISTINCT auctiondetails.PrizedMemberID, concat( membertogroupmaster.GrpMemberID,'|', membertogroupmaster.MemberName) as MemberName from auctiondetails inner join membertogroupmaster on auctiondetails.PrizedMemberID=membertogroupmaster.Head_Id where auctiondetails.GroupID=" + ddlGroupNumber.SelectedItem.Value + " and auctiondetails.IsPrized='N'");
                ddlMemberName.DataSource = dtchit;
                ddlMemberName.DataTextField = "MemberName";
                ddlMemberName.DataValueField = "PrizedMemberID";
                ddlMemberName.DataBind();
                ddlMemberName.Focus();
                ListItem lst1 = new ListItem("--Select--", "--Select--");
                ddlMemberName.Items.Insert(0, lst1);
                txtDrawNumber.Text = "";
                txtBankAmount.Text = "0.00";
            }
            else
            {
                ddlMemberName.DataSource = null;
                ddlMemberName.Items.Clear();
                ddlMemberName.DataBind();
                txtDrawNumber.Text = "";
                txtBankAmount.Text = "0.00";
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
                    decimal strForemanChitPrized = Convert.ToDecimal(balayer.GetSingleValue("SELECT PrizedAmount FROM svcf.auctiondetails where GroupID=" + ddlGroupNumber.SelectedItem.Value + " and PrizedMemberID=" + ddlMemberName.SelectedItem.Value + " and IsPrized='N' and BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + ""));
                    int DrawNo = Convert.ToInt32(balayer.GetSingleValue("SELECT DrawNO FROM svcf.auctiondetails where PrizedMemberID=" + ddlMemberName.SelectedValue + ""));
                    txtDrawNumber.Text = DrawNo.ToString();
                    LabelPrizedAmount.Text = (ddlMemberName.SelectedItem.Text).Split('|')[0];
                    decimal chitValue = Convert.ToDecimal(txtDebitAmount.Text);
                    decimal commission = Convert.ToDecimal(balayer.GetSingleValue("SELECT Commission FROM svcf.commissiondetails where chitValue=" + chitValue + " "));
                    int totalMembers = Convert.ToInt32(balayer.GetSingleValue("SELECT `groupmaster`.`NoofMembers` FROM svcf.groupmaster where Head_Id=" + ddlGroupNumber.SelectedItem.Value + ""));
                    lbTool.ToolTip = totalMembers.ToString();
                    txtBankAmount.Text = (strForemanChitPrized).ToString();
                    lbFuture.ToolTip = ((chitValue - strForemanChitPrized - commission) / totalMembers).ToString();
                }
                else
                {
                    txtDrawNumber.Text = "";
                    LabelPrizedAmount.Text = "";
                    lbTool.ToolTip = "0";
                    txtBankAmount.Text = "0.00";
                    lbFuture.ToolTip = "0.00";
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
            bool isban = decimal.TryParse(txtBankAmount.Text, out ban);
            if (!isban)
            {
                txtBankAmount.Text = "0.00";
            }
            decimal com = 0;
            bool iscom = decimal.TryParse(txtCommision.Text, out com);
            if (!iscom)
            {
                txtCommision.Text = "0.00";
            }
            decimal Credit = Convert.ToDecimal(txtCommision.Text) + Convert.ToDecimal(txtBankAmount.Text)+(Convert.ToDecimal(lbFuture.ToolTip)*Convert.ToInt32(lbTool.ToolTip));
            decimal Debit = Convert.ToDecimal(txtDebitAmount.Text);
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
            //TranscationLayer trn = new Transcation(true);
            TransactionLayer trn = new TransactionLayer();
            string strNarration = "";
            string DualTransactionKey = "";
            string Splittoken = "";
            string DrawNo = "";
            string Cscprizedid = "";
           
            try
            {
                strNarration = balayer.MySQLEscapeString(ddlMemberName.SelectedItem.Text).Split('|')[0] + " through payment of Draw Number :" + txtDrawNumber.Text;
                System.Guid guid = Guid.NewGuid();
                string guidForChar36 = guid.ToString();
                string hexstring = BitConverter.ToString(guid.ToByteArray());
                string guidForBinary16 = "0x" + hexstring.Replace("-", string.Empty);
                DualTransactionKey = guidForBinary16;
                //string GetHeadId = balayer.GetSingleValue("SELECT Head_Id FROM svcf.membertogroupmaster where Head_Id=" + ddlMemberName.SelectedItem.Value + "");
                string GetMemberId = balayer.GetSingleValue("SELECT MemberID FROM svcf.membertogroupmaster where Head_Id=" + ddlMemberName.SelectedItem.Value + "");
                //long GetCommision = trn.insertorupdateTrn("insert into voucher (`DualTransactionKey`,`BranchID`,`CurrDate`,`Voucher_No`,`Voucher_Type`,`Head_Id`,`ChoosenDate`,`Narration`,`Amount`,`Series`,`ReceievedBy`,`Trans_Type`,`T_Day`,`T_Month`,`T_Year`,`MemberID`,`Trans_Medium`,`RootID`,`ChitGroupId`,`Other_Trans_Type`) values (" + DualTransactionKey + "," + balayer.ToobjectstrEvenNull(Session["Branchid"]) + ",'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," + txtPaymentNumber.Text + ",'C',64,'" + balayer.indiandateToMysqlDate(txtDate.Text) + "','Commision for " + strNarration + "'," + txtCommision.Text + ",'" + txtSeries.Text + "','" + balayer.MySQLEscapeString(balayer.ToobjectstrEvenNull(Session["UserName"])) + "',2," + balayer.indiandateToMysqlDate(txtDate.Text).Split('/')[2] + "," + balayer.indiandateToMysqlDate(txtDate.Text).Split('/')[1] + "," + balayer.indiandateToMysqlDate(txtDate.Text).Split('/')[0] + "," + GetMemberId + ",0,11," + ddlGroupNumber.SelectedItem.Value + ",0) ");
                long GetCommision = trn.insertorupdateTrn("insert into voucher (`DualTransactionKey`,`BranchID`,`CurrDate`,`Voucher_No`,`Voucher_Type`,`Head_Id`,`ChoosenDate`,`Narration`,`Amount`,`Series`,`ReceievedBy`,`Trans_Type`,`T_Day`,`T_Month`,`T_Year`,`MemberID`,`Trans_Medium`,`RootID`,`ChitGroupId`,`Other_Trans_Type`) values (" + DualTransactionKey + "," + balayer.ToobjectstrEvenNull(Session["Branchid"]) + ",'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," + txtPaymentNumber.Text + ",'C',1128098,'" + balayer.indiandateToMysqlDate(txtDate.Text) + "','Commision for " + strNarration + "'," + txtCommision.Text + ",'" + txtSeries.Text + "','" + balayer.MySQLEscapeString(balayer.ToobjectstrEvenNull(Session["UserName"])) + "',2," + balayer.indiandateToMysqlDate(txtDate.Text).Split('/')[2] + "," + balayer.indiandateToMysqlDate(txtDate.Text).Split('/')[1] + "," + balayer.indiandateToMysqlDate(txtDate.Text).Split('/')[0] + "," + GetMemberId + ",0,11," + ddlGroupNumber.SelectedItem.Value + ",0) ");
                long GetDebitAmount = trn.insertorupdateTrn("insert into voucher (`DualTransactionKey`,`BranchID`,`CurrDate`,`Voucher_No`,`Voucher_Type`,`Head_Id`,`ChoosenDate`,`Narration`,`Amount`,`Series`,`ReceievedBy`,`Trans_Type`,`T_Day`,`T_Month`,`T_Year`,`MemberID`,`Trans_Medium`,`RootID`,`ChitGroupId`,`Other_Trans_Type`) values (" + DualTransactionKey + "," + balayer.ToobjectstrEvenNull(Session["Branchid"]) + ",'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," + txtPaymentNumber.Text + ",'D'," + ddlMemberName.SelectedItem.Value + ",'" + balayer.indiandateToMysqlDate(txtDate.Text) + "','Debit Amount for " + strNarration + "'," + txtDebitAmount.Text + ",'" + txtSeries.Text + "','" + balayer.MySQLEscapeString(balayer.ToobjectstrEvenNull(Session["UserName"])) + "',2," + balayer.indiandateToMysqlDate(txtDate.Text).Split('/')[2] + "," + balayer.indiandateToMysqlDate(txtDate.Text).Split('/')[1] + "," + balayer.indiandateToMysqlDate(txtDate.Text).Split('/')[0] + "," + GetMemberId + ",0,5," + ddlGroupNumber.SelectedItem.Value + ",0)");
                //string ForemanSubstitutedChitPrizedAmountHeadid = balayer.GetSingleValue("select foremansubstitutedchitprized from removedmaster where GroupmemberID=" + ddlMemberName.SelectedItem.Value);
                //long ForemanSubstitutedChitPrizedAmount = trn.insertorupdateTrn("insert into voucher (`DualTransactionKey`,`BranchID`,`Current_Date`,`Voucher_No`,`Voucher_Type`,`Head_Id`,`ChoosenDate`,`Narration`,`Amount`,`Series`,`ReceievedBy`,`Trans_Type`,`T_Day`,`T_Month`,`T_Year`,`MemberID`,`Trans_Medium`,`RootID`,`ChitGroupId`,`Other_Trans_Type`) values (" + DualTransactionKey + "," + balayer.ToobjectstrEvenNull(Session["Branchid"]) + ",'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," + txtPaymentNumber.Text + ",'C'," + ForemanSubstitutedChitPrizedAmountHeadid + ",'" + balayer.indiandateToMysqlDate(txtDate.Text) + "','Foreman Substituted Chit Prized Amount for " + strNarration + "'," + txtBankAmount.Text + ",'" + balayer.MySQLEscapeString(txtSeries.Text) + "','" + balayer.MySQLEscapeString(balayer.ToobjectstrEvenNull(Session["UserName"])) + "',2," + balayer.indiandateToMysqlDate(txtDate.Text).Split('/')[2] + "," + balayer.indiandateToMysqlDate(txtDate.Text).Split('/')[1] + "," + balayer.indiandateToMysqlDate(txtDate.Text).Split('/')[0] + "," + GetMemberId + ",3,6," + ddlGroupNumber.SelectedItem.Value + ",6)");
                //MLR-08/39

                Cscprizedid = balayer.GetSingleValue("SELECT foremansubstitutedchitprized FROM svcf.removedmaster where GroupmemberID= " + ddlMemberName.SelectedItem.Value + "");
                   

                long ForemanSubstitutedChitPrizedAmount = trn.insertorupdateTrn("insert into voucher (`DualTransactionKey`,`BranchID`,`CurrDate`,`Voucher_No`,`Voucher_Type`,`Head_Id`,`ChoosenDate`,`Narration`,`Amount`,`Series`,`ReceievedBy`,`Trans_Type`,`T_Day`,`T_Month`,`T_Year`,`MemberID`,`Trans_Medium`,`RootID`,`ChitGroupId`,`Other_Trans_Type`) values (" + DualTransactionKey + "," + balayer.ToobjectstrEvenNull(Session["Branchid"]) + ",'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," + txtPaymentNumber.Text + ",'C'," + Cscprizedid + ",'" + balayer.indiandateToMysqlDate(txtDate.Text) + "','Foreman Substituted Chit Prized Amount for " + strNarration + "'," + txtBankAmount.Text + ",'" + balayer.MySQLEscapeString(txtSeries.Text) + "','" + balayer.MySQLEscapeString(balayer.ToobjectstrEvenNull(Session["UserName"])) + "',2," + balayer.indiandateToMysqlDate(txtDate.Text).Split('/')[2] + "," + balayer.indiandateToMysqlDate(txtDate.Text).Split('/')[1] + "," + balayer.indiandateToMysqlDate(txtDate.Text).Split('/')[0] + "," + GetMemberId + ",3,6," + ddlGroupNumber.SelectedItem.Value + ",0)");
                int lastdraw = Convert.ToInt32(txtDrawNumber.Text) + 1;
                int drawlast = Convert.ToInt32(balayer.GetSingleValue("SELECT `groupmaster`.`ChitPeriod` FROM svcf.groupmaster where `groupmaster`.`Head_Id`=" + ddlGroupNumber.SelectedItem.Value + ""));
                if (drawlast >= lastdraw)
                {
                    DataTable auction = balayer.GetDataTable("SELECT `auctiondetails`.`PrizedMemberID`,`auctiondetails`.`GroupID`,`auctiondetails`.`AuctionDate`,`auctiondetails`.`NextDueAmount`,`auctiondetails`.`PrizedAmount`,`groupmaster`.`ChitValue`,`auctiondetails`.`CurrentDueAmount`  FROM svcf.auctiondetails left join groupmaster on (`auctiondetails`.`GroupID`=`groupmaster`.`Head_Id`) where `auctiondetails`.`GroupID`=" + ddlGroupNumber.SelectedItem.Value + " and `auctiondetails`.`PrizedMemberID`=" + ddlMemberName.SelectedItem.Value + " and `auctiondetails`.`BranchID`=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + "");
                    string AuctionDt = auction.Rows[0]["AuctionDate"].ToString();
                    decimal NextDue = Convert.ToDecimal(auction.Rows[0]["NextDueAmount"]);
                    decimal Priced = Convert.ToDecimal(auction.Rows[0]["PrizedAmount"]);
                    decimal chitvalue = Convert.ToDecimal(auction.Rows[0]["ChitValue"]);
                    decimal CurrentDue = Convert.ToDecimal(auction.Rows[0]["CurrentDueAmount"]);
                    //string bbbbb = balayer.GetSingleValue("SELECT TransactionKey FROM svcf.voucher where BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and Head_Id=" + ddlMemberName.SelectedItem.Value + " and DualTransactionKey=" + DualTransactionKey + " and Voucher_Type='D'");
                    long TransPaymentQuery = trn.insertorupdateTrn("insert into trans_payment (`TransactionKey_Bank`,`TransactionKey`,`DualTransactionKey`,`BranchID`,`ChitGroupID`,`DrawNo`,`PaymentApplyedOn`,`ApprovedOn`,`PaymentDate`,`GuarantorID`,`Description`,`ChitAmount`,`PrizedAmount`,`AuctionDate`,`NextDueAmount`,`NextDrawNo`,`AOSanctionNo`,`CurrentDueAmount`,`TokenNumber`,`GuarantorName`,`Flags`) values (null," + GetDebitAmount + "," + DualTransactionKey + "," + balayer.ToobjectstrEvenNull(Session["Branchid"]) + "," + ddlGroupNumber.SelectedItem.Value + "," + txtDrawNumber.Text + ",'" + balayer.indiandateToMysqlDate(txtApplyedOn.Text) + "','" + balayer.indiandateToMysqlDate(txtPaymentonDate.Text) + "','" + balayer.indiandateToMysqlDate(txtDate.Text) + "','1','" + balayer.MySQLEscapeString(txtDescription.Text) + "'," + chitvalue + "," + Priced + ",'" + balayer.indiandateToMysqlDate(AuctionDt) + "'," + NextDue + "," + lastdraw + "," + txtAOSanctiion.Text + "," + CurrentDue + "," + ddlMemberName.SelectedItem.Value + ",'Sree Visalam Chit Fund Limited',2)");
                }
                else
                {
                    DataTable auction = balayer.GetDataTable("SELECT `auctiondetails`.`PrizedMemberID`,`auctiondetails`.`GroupID`,`auctiondetails`.`AuctionDate`,`auctiondetails`.`NextDueAmount`,`auctiondetails`.`PrizedAmount`,`groupmaster`.`ChitValue`,`auctiondetails`.`CurrentDueAmount`  FROM svcf.auctiondetails left join groupmaster on (`auctiondetails`.`GroupID`=`groupmaster`.`Head_Id`) where `auctiondetails`.`GroupID`=" + ddlGroupNumber.SelectedItem.Value + " and `auctiondetails`.`PrizedMemberID`=" + ddlMemberName.SelectedItem.Value + " and `auctiondetails`.`BranchID`=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + "");
                    string AuctionDt = auction.Rows[0]["AuctionDate"].ToString();
                    decimal NextDue = Convert.ToDecimal(auction.Rows[0]["NextDueAmount"]);
                    decimal Priced = Convert.ToDecimal(auction.Rows[0]["PrizedAmount"]);
                    decimal chitvalue = Convert.ToDecimal(auction.Rows[0]["ChitValue"]);
                    decimal CurrentDue = Convert.ToDecimal(auction.Rows[0]["CurrentDueAmount"]);
                    //int bbbbb = Convert.ToInt32(balayer.GetSingleValue("SELECT TransactionKey FROM svcf.voucher where BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and Head_Id=" + ddlMemberName.SelectedItem.Value + " and DualTransactionKey=" + DualTransactionKey + " and Voucher_Type='D'"));
                    long TransPaymentQuery = trn.insertorupdateTrn("insert into trans_payment (`TransactionKey_Bank`,`TransactionKey`,`DualTransactionKey`,`BranchID`,`ChitGroupID`,`DrawNo`,`PaymentApplyedOn`,`ApprovedOn`,`PaymentDate`,`GuarantorID`,`Description`,`ChitAmount`,`PrizedAmount`,`AuctionDate`,`NextDueAmount`,`NextDrawNo`,`AOSanctionNo`,`CurrentDueAmount`,`TokenNumber`,`GuarantorName`,`Flags`) values (null," + GetDebitAmount + "," + DualTransactionKey + "," + balayer.ToobjectstrEvenNull(Session["Branchid"]) + "," + ddlGroupNumber.SelectedItem.Value + "," + txtDrawNumber.Text + ",'" + balayer.indiandateToMysqlDate(txtApplyedOn.Text) + "','" + balayer.indiandateToMysqlDate(txtPaymentonDate.Text) + "','" + balayer.indiandateToMysqlDate(txtDate.Text) + "','1','" + balayer.MySQLEscapeString(txtDescription.Text) + "'," + chitvalue + "," + Priced + ",'" + balayer.indiandateToMysqlDate(AuctionDt) + "'," + NextDue + ",null," + txtAOSanctiion.Text + "," + CurrentDue + "," + ddlMemberName.SelectedItem.Value + ",'Sree Visalam Chit Fund Limited',2)");
                }
                if (Convert.ToDecimal(lbFuture.ToolTip) > 0)
                {
                    int a = Convert.ToInt32(lbTool.ToolTip);
                    DataTable dd = balayer.GetDataTable("SELECT MemberID,Head_Id,MemberName,GrpMemberID FROM svcf.membertogroupmaster where GroupID=" + ddlGroupNumber.SelectedItem.Value + "");
                    for (int i = 0; i < a; i++)
                    {
                        string GetMemberId1 = dd.Rows[i][0].ToString();
                        string GetHeadId1 = dd.Rows[i][1].ToString();
                        long ChitKasarAmount1 = trn.insertorupdateTrn("insert into voucher (`DualTransactionKey`,`BranchID`,`CurrDate`,`Voucher_No`,`Voucher_Type`,`Head_Id`,`ChoosenDate`,`Narration`,`Amount`,`Series`,`ReceievedBy`,`Trans_Type`,`T_Day`,`T_Month`,`T_Year`,`MemberID`,`Trans_Medium`,`RootID`,`ChitGroupId`,`Other_Trans_Type`) values (" + DualTransactionKey + "," + balayer.ToobjectstrEvenNull(Session["Branchid"]) + ",'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," + txtPaymentNumber.Text + ",'C'," + GetHeadId1 + ",'" + balayer.indiandateToMysqlDate(txtDate.Text) + "','Chit Kasar Amount for " + dd.Rows[i][3].ToString() + "'," + lbFuture.ToolTip + ",'" + txtSeries.Text + "','" + balayer.MySQLEscapeString(balayer.ToobjectstrEvenNull(Session["UserName"])) + "',1," + balayer.indiandateToMysqlDate(txtDate.Text).Split('/')[2] + "," + balayer.indiandateToMysqlDate(txtDate.Text).Split('/')[1] + "," + balayer.indiandateToMysqlDate(txtDate.Text).Split('/')[0] + "," + GetMemberId1 + ",0,5," + ddlGroupNumber.SelectedItem.Value + ",5)");
                    }
                }
                DrawNo = txtDrawNumber.Text;
                long s= trn.insertorupdateTrn("update auctiondetails set IsPrized='Y' where PrizedMemberID=" + ddlMemberName.SelectedValue + " and DrawNO=" + DrawNo + "");
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

                txtPaymentNumber.Text = balayer.GetSingleValue("SELECT max(Voucher_No)+1 FROM svcf.voucher where  BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and Trans_Type=2 and Series='PAYMENT'");

                logger.Info("CompanySubstituteChitPayment.aspx - btn_ok() - Completed: " + DateTime.Now + " by: " + Convert.ToString(Session["UserName"]) + "");
            }
            catch (Exception error)
            {
                try
                {
                    trn.RollbackTrn();
                    balayer.GetInsertItem("update `auctiondetails` set IsPrized='N' where PrizedMemberID='" + ddlMemberName.SelectedValue + "' and DrawNO=" + DrawNo );
                    logger.Info("CompanySubstituteChitPayment.aspx - btn_ok() - Error: " + error.Message + ": " + DateTime.Now + " by: " + Convert.ToString(Session["UserName"]) + "");
                }
                catch(Exception ex)
                    {}
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