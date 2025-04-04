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
    public partial class DepositPayment : System.Web.UI.Page
    {
        #region Object
        BusinessLayer balayer = new BusinessLayer();
        #endregion

        ILog logger = log4net.LogManager.GetLogger(typeof(DepositPayment));
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                var userinfo = HttpContext.Current.User.Identity.Name;
                var qry = "select rs.name from roles as rs inner join rights as rt on (rt.roleid=rs.id) where memberid=" + userinfo + "";
                var usrRole = balayer.GetSingleValue(qry);
                if (usrRole == "Report")
                {
                    Response.Redirect("Home.aspx", false);
                }

                Session["CheckRefresh"] = System.Guid.NewGuid().ToString();
                rvDepositDate.MinimumValue = balayer.GetSingleValue("SELECT DATE_FORMAT( min(MinimumDate), '%d/%m/%Y') MinimumDate FROM svcf.restrictionmaster where BranchID=" + Session["Branchid"]); 
                //rvDepositDate.MinimumValue = balayer.GetSingleValue("SELECT DATE_FORMAT( min(ChoosenDate), '%d/%m/%Y') MinimumDate FROM svcf.voucher where " +
                //    "BranchID=" + Session["Branchid"] + " and ChoosenDate<>'0000-00-00'");

                rvDepositDate.MaximumValue = DateTime.Now.ToString("dd/MM/yyyy");
                rvDate.MinimumValue = DateTime.Now.AddYears(-1).ToString("dd/MM/yyyy");
                rvDate.MaximumValue = DateTime.Now.ToString("dd/MM/yyyy");
                txtDepositDate.Text = DateTime.Now.ToString("dd/MM/yyyy");

                DataTable dtBranch = new DataTable();
                dtBranch = balayer.GetDataTable("select B_Name,Head_Id from branchdetails order by B_Name asc");
                ddlBranch.DataSource = dtBranch;
                ddlBranch.DataTextField = "B_Name";
                ddlBranch.DataValueField = "Head_Id";
                ddlBranch.DataBind();
                dtBranch.Dispose();

                //DataTable dtChit = balayer.GetDataTable("SELECT concat(g1.GrpMemberID,'|',g1.MemberName) as MemberName,concat(cast(g1.GroupID as char),'|',cast(g1.Head_ID as char),'|',cast(g1.MemberID as char)) as MemberID FROM membertogroupmaster as g1 where  g1.BranchID=" + Session["Branchid"] + " union SELECT concat('RCM','|',Node) as MemberName,concat(cast(0 as char),'|',cast(NodeID as char),'|',cast(0 as char)) as MemberID FROM svcf.headstree where ParentID in (44,45) and BranchID=" + Session["Branchid"]);
                //DataRow drChit = dtChit.NewRow();
                //drChit[0] = "--Select--";
                //drChit[1] = "0";
                //ddlGroupNumber.DataSource = dtChit;
                //ddlGroupNumber.DataTextField = "MemberName";
                //ddlGroupNumber.DataValueField = "MemberID";
                //dtChit.Rows.InsertAt(drChit, 0);
                //ddlGroupNumber.DataBind();
                //DataTable dtBank = balayer.GetDataTable("SELECT Head_Id,concat(BankName, ' ',cast(AccountNo as char)) as BankName FROM svcf.bankdetails");
                //DataRow drBank = dtBank.NewRow();
                //drBank[0] = "0";
                //drBank[1] = "--Select--";
                //ddlBank.DataSource = dtBank;
                //ddlBank.DataTextField = "BankName";
                //ddlBank.DataValueField = "Head_Id";
                //ddlDepositBank.DataSource = dtBank;
                //ddlDepositBank.DataTextField = "BankName";
                //ddlDepositBank.DataValueField = "Head_Id";
                //dtBank.Rows.InsertAt(drBank, 0);
                //ddlBank.DataBind();
                //ddlDepositBank.DataBind();


                DataTable dtBank = balayer.GetDataTable("SELECT concat((bd.Head_Id),'|',(hs.Rootid)) as Head_Id,concat(bd.BankName, ' ',cast(bd.AccountNo as char)) as BankName FROM svcf.bankdetails as bd join headstree as hs on (bd.Head_Id=hs.NodeID) where  bd.BranchID=" + Session["Branchid"]);
                DataTable dtBranch1 = new DataTable();
                dtBranch1 = balayer.GetDataTable("select bd.B_Name as BankName ,concat((bd.Head_Id),'|',(hs.Rootid)) as Head_Id from branchdetails as bd join headstree as hs on (bd.Head_Id=hs.NodeID) order by B_Name asc");
                dtBank.Merge(dtBranch1);
                DataRow drBank = dtBank.NewRow();
                drBank[0] = "0";
                drBank[1] = "--Select--";
                ddlBank.DataSource = dtBank;
                ddlBank.DataTextField = "BankName";
                ddlBank.DataValueField = "Head_Id";
                ddlDepositBank.DataSource = dtBank;
                ddlDepositBank.DataTextField = "BankName";
                ddlDepositBank.DataValueField = "Head_Id";
                dtBank.Rows.InsertAt(drBank, 0);
                ddlBank.DataBind();
                ddlDepositBank.DataBind();


                DataTable dtBankbranch = balayer.GetDataTable("select TREE,concat(TreeID,'|',RootID) as TreeID  FROM svcf.view_parent where RootID in (1,3) and BranchID=" + Session["Branchid"]);
                

                DataRow drBankbranch = dtBankbranch.NewRow();
                //drBankbranch[0] = "0";
                //drBankbranch[1] = "--Select--";
                ddlDepositBank.DataSource = dtBankbranch;
                ddlDepositBank.DataTextField = "TREE";
                ddlDepositBank.DataValueField = "TreeID";

                dtBankbranch.Rows.InsertAt(drBankbranch, 0);
                ddlDepositBank.DataBind();

            }
        }
        protected void Page_PreRender(object sender, EventArgs e)
        {
            ViewState["CheckRefresh"] = Session["CheckRefresh"];
        }
        protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            //DataTable dtChit = balayer.GetSingleValue("SELECT concat(g1.GrpMemberID,'|',g1.MemberName) as MemberName,concat(cast(g1.GroupID as char),'|',cast(g1.Head_ID as char),'|',cast(g1.MemberID as char)) as MemberID FROM membertogroupmaster as g1 where  g1.BranchID=" + ddlBranch.SelectedValue.Split('|')[1] + " union SELECT concat('RCM','|',Node) as MemberName,concat(cast(0 as char),'|',cast(NodeID as char),'|',cast(0 as char)) as MemberID FROM svcf.headstree where ParentID in (44,45) and BranchID=" + ddlBranch.SelectedValue.Split('|')[1]);
            DataTable dtChit = balayer.GetDataTable("SELECT concat(g1.GrpMemberID,'|',g1.MemberName) as MemberName,concat(cast(g1.GroupID as char),'|',cast(g1.Head_ID as char),'|',cast(g1.MemberID as char)) as MemberID FROM membertogroupmaster as g1 where  g1.BranchID=" + ddlBranch.SelectedValue.Split('|')[0] + " union SELECT concat('RCM','|',Node) as MemberName,concat(cast(0 as char),'|',cast(NodeID as char),'|',cast(0 as char)) as MemberID FROM svcf.headstree where ParentID in (44,45) and BranchID=" + ddlBranch.SelectedValue.Split('|')[0]);
            DataRow drChit = dtChit.NewRow();
            drChit[0] = "--Select--";
            drChit[1] = "0";
            ddlGroupNumber.DataSource = dtChit;
            ddlGroupNumber.DataTextField = "MemberName";
            ddlGroupNumber.DataValueField = "MemberID";
            dtChit.Rows.InsertAt(drChit, 0);
            ddlGroupNumber.DataBind();
            //if (ddlGroupNumber.SelectedItem.Text.StartsWith("RCM"))
            //{
            //    trDraw.Visible = false;
            //}
            //else
            //{
            //    trDraw.Visible = true;
            //    DataTable dt = balayer.GetDataTable("SELECT DrawNO,PrizedAmount,date_format(AuctionDate,'%d/%m/%Y') as AuctionDate FROM svcf.auctiondetails where PrizedMemberID=" + ddlGroupNumber.SelectedValue.Split('|')[1]);
            //    if (dt.Rows.Count > 0)
            //    {
            //        txtAmount.Text = balayer.ToobjectstrEvenNull(dt.Rows[0]["PrizedAmount"]);
            //        txtDrawNumber.Text = balayer.ToobjectstrEvenNull(dt.Rows[0]["DrawNO"]);
            //        txtDrawDate.Text = balayer.ToobjectstrEvenNull(dt.Rows[0]["AuctionDate"]);
            //    }
            //}
            //txtMemberName.Text = ddlGroupNumber.SelectedItem.Text.Split('|')[1];
        }
        protected void ddlGroupNumber_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtChitAgreementNumber.Text = balayer.GetSingleValue("SELECT ChitAgreementNo FROM svcf.groupmaster where Head_Id=" + ddlGroupNumber.SelectedValue.Split('|')[0]);
            if (ddlGroupNumber.SelectedItem.Text.StartsWith("RCM"))
            {
                trDraw.Visible = false;
            }
            else
            {
                trDraw.Visible = true;
                DataTable dt = balayer.GetDataTable("SELECT DrawNO,PrizedAmount,date_format(AuctionDate,'%d/%m/%Y') as AuctionDate FROM svcf.auctiondetails where PrizedMemberID=" + ddlGroupNumber.SelectedValue.Split('|')[1]);
                if (dt.Rows.Count > 0)
                {
                    txtAmount.Text = balayer.ToobjectstrEvenNull(dt.Rows[0]["PrizedAmount"]);
                    txtDrawNumber.Text = balayer.ToobjectstrEvenNull(dt.Rows[0]["DrawNO"]);
                    txtDrawDate.Text = balayer.ToobjectstrEvenNull(dt.Rows[0]["AuctionDate"]);
                }
            }
            txtMemberName.Text = ddlGroupNumber.SelectedItem.Text.Split('|')[1];
        }
        protected void btnPayment_Click(object sender, EventArgs e)
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
            bool isVal = true;
            TransactionLayer trn = new TransactionLayer();
            try
            {
                //System.Guid guid = Guid.NewGuid();
                //string guidForChar36 = guid.ToString();
                //string hexstring = BitConverter.ToString(guid.ToByteArray());
                //string guidForBinary16 = "0x" + hexstring.Replace("-", string.Empty);

                //15/6/2023
                System.Guid guid = Guid.NewGuid();
                string hexstring = BitConverter.ToString(guid.ToByteArray());
                string guidForBinary16 = "0x" + hexstring.Replace("-", string.Empty);
                string DualTransactionKey = guidForBinary16;


                //stored procedure for DualTransactionKey

                //string DualTransactionKey = Convert.ToString(balayer.sp_gendratedt_key());

                // Random rnd = new Random();
                //   int rr = rnd.Next(1, 1000);

                //change on 13/11/2018***********
                string rr = balayer.GetSingleValue("SELECT ifnull(max(cast(Voucher_No as unsigned)),0)+1 FROM svcf.voucher where ChoosenDate>='2014/04/01' and  Series='Deposit' and Trans_Type='0' and BranchID=" + Session["Branchid"]);
                //change on 13/11/2018***********

                //long Credit = trn.insertorupdateTrn("insert into voucher (`DualTransactionKey`,`BranchID`,`Current_Date`,`Voucher_No`,`Voucher_Type`,`Head_Id`,`ChoosenDate`,`Narration`,`Amount`,`Series`,`ReceievedBy`,`Trans_Type`,`T_Day`,`T_Month`,`T_Year`,`MemberID`,`Trans_Medium`,`RootID`,`ChitGroupId`,`Other_Trans_Type`) values (" + guidForBinary16 + "," + balayer.ToobjectstrEvenNull(Session["Branchid"]) + ",'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," + rr + ",'C'," + ddlBank.SelectedValue + ",'" + balayer.indiandateToMysqlDate(txtDepositDate.Text) + "','" + ddlGroupNumber.SelectedItem.Text + " " + txtDrawNumber.Text + "th auction pay deposited thr " + ddlDepositBank.SelectedItem.Text + " Ch No. " + txtChequeNumber.Text + " '," + txtAmount.Text + ",'Deposit','" + balayer.MySQLEscapeString(balayer.ToobjectstrEvenNull(Session["UserName"])) + "',0," + balayer.indiandateToMysqlDate(txtDepositDate.Text).Split('/')[2] + "," + balayer.indiandateToMysqlDate(txtDepositDate.Text).Split('/')[1] + "," + balayer.indiandateToMysqlDate(txtDepositDate.Text).Split('/')[0] + "," + ddlGroupNumber.SelectedItem.Value.Split('|')[2] + ",1,3," + ddlGroupNumber.SelectedItem.Value.Split('|')[0] + ",0) ");
                //long CreditBank = trn.insertorupdateTrn("insert into transbank(`BranchID`,`TransactionKey`,`DualTransactionKey`,`T_Day`,`T_Month`,`T_Year`,`Head_Id`,`BankHeadID`,`MemberID`,`CustomersBankName`,`DateInCheque`,`ChequeDDNO`,`GivenAmount`,`TotalChequeDDAmount`,`IsBounced`,`Trans_Type`) values(" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + "," + Credit + "," + guidForBinary16 + "," + balayer.indiandateToMysqlDate(txtDepositDate.Text).Split('/')[2] + "," + balayer.indiandateToMysqlDate(txtDepositDate.Text).Split('/')[1] + "," + balayer.indiandateToMysqlDate(txtDepositDate.Text).Split('/')[0] + "," + ddlBank.SelectedValue + "," + ddlDepositBank.SelectedValue + "," + ddlGroupNumber.SelectedItem.Value.Split('|')[2] + ",'" + ddlBank.SelectedItem.Text + "','" + balayer.indiandateToMysqlDate(txtDepositDate.Text) + "'," + txtChequeNumber.Text + "," + txtAmount.Text + "," + txtAmount.Text + ",0,2)");
                //long Debit = trn.insertorupdateTrn("insert into voucher (`DualTransactionKey`,`BranchID`,`Current_Date`,`Voucher_No`,`Voucher_Type`,`Head_Id`,`ChoosenDate`,`Narration`,`Amount`,`Series`,`ReceievedBy`,`Trans_Type`,`T_Day`,`T_Month`,`T_Year`,`MemberID`,`Trans_Medium`,`RootID`,`ChitGroupId`,`Other_Trans_Type`) values (" + guidForBinary16 + "," + balayer.ToobjectstrEvenNull(Session["Branchid"]) + ",'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," + rr + ",'D'," + ddlDepositBank.SelectedValue + ",'" + balayer.indiandateToMysqlDate(txtDepositDate.Text) + "','" + ddlGroupNumber.SelectedItem.Text + " " + txtDrawNumber.Text + "th auction pay deposited thr " + ddlDepositBank.SelectedItem.Text + " Ch No. " + txtChequeNumber.Text + " '," + txtAmount.Text + ",'Deposit','" + balayer.MySQLEscapeString(balayer.ToobjectstrEvenNull(Session["UserName"])) + "',0," + balayer.indiandateToMysqlDate(txtDepositDate.Text).Split('/')[2] + "," + balayer.indiandateToMysqlDate(txtDepositDate.Text).Split('/')[1] + "," + balayer.indiandateToMysqlDate(txtDepositDate.Text).Split('/')[0] + "," + ddlGroupNumber.SelectedItem.Value.Split('|')[2] + ",1,3," + ddlGroupNumber.SelectedItem.Value.Split('|')[0] + ",0) ");
                //long DebitBank = trn.insertorupdateTrn("insert into transbank(`BranchID`,`TransactionKey`,`DualTransactionKey`,`T_Day`,`T_Month`,`T_Year`,`Head_Id`,`BankHeadID`,`MemberID`,`CustomersBankName`,`DateInCheque`,`ChequeDDNO`,`GivenAmount`,`TotalChequeDDAmount`,`IsBounced`,`Trans_Type`) values(" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + "," + Debit + "," + guidForBinary16 + "," + balayer.indiandateToMysqlDate(txtDepositDate.Text).Split('/')[2] + "," + balayer.indiandateToMysqlDate(txtDepositDate.Text).Split('/')[1] + "," + balayer.indiandateToMysqlDate(txtDepositDate.Text).Split('/')[0] + "," + ddlBank.SelectedItem.Value + "," + ddlDepositBank.SelectedItem.Value + "," + ddlGroupNumber.SelectedItem.Value.Split('|')[2] + ",'" + ddlDepositBank.SelectedItem.Text + "','" + balayer.indiandateToMysqlDate(txtDepositDate.Text) + "'," + txtChequeNumber.Text + "," + txtAmount.Text + "," + txtAmount.Text + ",0,2)");
                ////long l = trn.insertorupdateTrn("insert into svcf.DepositPayment(DualTransactionKey,ChitAgreementNumber,ChitNumber,MemberName,DrawNo,DrawDate,Amount,HeadId,DepositHeadId,ChequeNumber,TokenNumber,DepositDate,BranchID) values (" + guidForBinary16 + "," + txtChitAgreementNumber.Text + "," + ddlGroupNumber.SelectedItem.Value.Split('|')[0] + ",'" + balayer.MySQLEscapeString(ddlGroupNumber.SelectedItem.Text) + "'," + txtDrawNumber.Text + ",'" + balayer.indiandateToMysqlDate(txtDrawDate.Text) + "'," + txtAmount.Text + "," + ddlBank.SelectedItem.Value + "," + ddlDepositBank.SelectedItem.Value + "," + txtChequeNumber.Text + "," + ddlGroupNumber.SelectedItem.Value.Split('|')[1] + ",'" + balayer.indiandateToMysqlDate(txtDepositDate.Text) + "'," + Session["Branchid"] + ")");


                string rootid = balayer.GetSingleValue("select ParentID from svcf.headstree where NodeID='" + ddlDepositBank.SelectedValue + "'");
                string aa = "1";
                if (rootid == aa)
                {
                    //long Credit = trn.insertorupdateTrn("insert into voucher (`DualTransactionKey`,`BranchID`,`CurrDate`,`Voucher_No`,`Voucher_Type`,`Head_Id`,`ChoosenDate`,`Narration`,`Amount`,`Series`,`ReceievedBy`,`Trans_Type`,`T_Day`,`T_Month`,`T_Year`,`MemberID`,`Trans_Medium`,`RootID`,`ChitGroupId`,`Other_Trans_Type`) values (" + DualTransactionKey + "," + balayer.ToobjectstrEvenNull(Session["Branchid"]) + ",'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," + rr + ",'C'," + ddlBank.SelectedValue + ",'" + balayer.indiandateToMysqlDate(txtDepositDate.Text) + "','" + ddlGroupNumber.SelectedItem.Text + " " + txtDrawNumber.Text + "th auction pay deposited thr " + ddlDepositBank.SelectedItem.Text + " Ch No. " + txtChequeNumber.Text + " '," + txtAmount.Text + ",'Deposit','" + balayer.MySQLEscapeString(balayer.ToobjectstrEvenNull(Session["UserName"])) + "',0," + balayer.indiandateToMysqlDate(txtDepositDate.Text).Split('/')[2] + "," + balayer.indiandateToMysqlDate(txtDepositDate.Text).Split('/')[1] + "," + balayer.indiandateToMysqlDate(txtDepositDate.Text).Split('/')[0] + "," + ddlGroupNumber.SelectedItem.Value.Split('|')[2] + ",1,1," + ddlGroupNumber.SelectedItem.Value.Split('|')[0] + ",1) ");

                    //long CreditBank = trn.insertorupdateTrn("insert into transbank(`BranchID`,`TransactionKey`,`DualTransactionKey`,`T_Day`,`T_Month`,`T_Year`,`Head_Id`,`BankHeadID`,`MemberID`,`CustomersBankName`,`DateInCheque`,`ChequeDDNO`,`GivenAmount`,`TotalChequeDDAmount`,`IsBounced`,`Trans_Type`) values(" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + "," + Credit + "," + DualTransactionKey + "," + balayer.indiandateToMysqlDate(txtDepositDate.Text).Split('/')[2] + "," + balayer.indiandateToMysqlDate(txtDepositDate.Text).Split('/')[1] + "," + balayer.indiandateToMysqlDate(txtDepositDate.Text).Split('/')[0] + "," + ddlBank.SelectedValue + "," + ddlDepositBank.SelectedValue + "," + ddlGroupNumber.SelectedItem.Value.Split('|')[2] + ",'" + ddlBank.SelectedItem.Text + "','" + balayer.indiandateToMysqlDate(txtDepositDate.Text) + "'," + txtChequeNumber.Text + "," + txtAmount.Text + "," + txtAmount.Text + ",0,2)");

                    //long Debit = trn.insertorupdateTrn("insert into voucher (`DualTransactionKey`,`BranchID`,`CurrDate`,`Voucher_No`,`Voucher_Type`,`Head_Id`,`ChoosenDate`,`Narration`,`Amount`,`Series`,`ReceievedBy`,`Trans_Type`,`T_Day`,`T_Month`,`T_Year`,`MemberID`,`Trans_Medium`,`RootID`,`ChitGroupId`,`Other_Trans_Type`) values (" + DualTransactionKey + "," + balayer.ToobjectstrEvenNull(Session["Branchid"]) + ",'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," + rr + ",'D'," + ddlDepositBank.SelectedValue + ",'" + balayer.indiandateToMysqlDate(txtDepositDate.Text) + "','" + ddlGroupNumber.SelectedItem.Text + " " + txtDrawNumber.Text + "th auction pay deposited thr " + ddlDepositBank.SelectedItem.Text + " Ch No. " + txtChequeNumber.Text + " '," + txtAmount.Text + ",'Deposit','" + balayer.MySQLEscapeString(balayer.ToobjectstrEvenNull(Session["UserName"])) + "',0," + balayer.indiandateToMysqlDate(txtDepositDate.Text).Split('/')[2] + "," + balayer.indiandateToMysqlDate(txtDepositDate.Text).Split('/')[1] + "," + balayer.indiandateToMysqlDate(txtDepositDate.Text).Split('/')[0] + "," + ddlGroupNumber.SelectedItem.Value.Split('|')[2] + ",1,3," + ddlGroupNumber.SelectedItem.Value.Split('|')[0] + ",1) ");

                    //long DebitBank = trn.insertorupdateTrn("insert into transbank(`BranchID`,`TransactionKey`,`DualTransactionKey`,`T_Day`,`T_Month`,`T_Year`,`Head_Id`,`BankHeadID`,`MemberID`,`CustomersBankName`,`DateInCheque`,`ChequeDDNO`,`GivenAmount`,`TotalChequeDDAmount`,`IsBounced`,`Trans_Type`) values(" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + "," + Debit + "," + DualTransactionKey + "," + balayer.indiandateToMysqlDate(txtDepositDate.Text).Split('/')[2] + "," + balayer.indiandateToMysqlDate(txtDepositDate.Text).Split('/')[1] + "," + balayer.indiandateToMysqlDate(txtDepositDate.Text).Split('/')[0] + "," + ddlBank.SelectedItem.Value + "," + ddlDepositBank.SelectedItem.Value + "," + ddlGroupNumber.SelectedItem.Value.Split('|')[2] + ",'" + ddlDepositBank.SelectedItem.Text + "','" + balayer.indiandateToMysqlDate(txtDepositDate.Text) + "'," + txtChequeNumber.Text + "," + txtAmount.Text + "," + txtAmount.Text + ",0,2)");
                    long Credit = trn.insertorupdateTrn("insert into voucher (`DualTransactionKey`,`BranchID`,`CurrDate`,`Voucher_No`,`Voucher_Type`,`Head_Id`,`ChoosenDate`,`Narration`,`Amount`,`Series`,`ReceievedBy`,`Trans_Type`,`T_Day`,`T_Month`,`T_Year`,`MemberID`,`Trans_Medium`,`RootID`,`ChitGroupId`,`Other_Trans_Type`) values (" + DualTransactionKey + "," + balayer.ToobjectstrEvenNull(Session["Branchid"]) + ",'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," + rr + ",'C'," + ddlBank.SelectedValue.Split('|')[0] + ",'" + balayer.indiandateToMysqlDate(txtDepositDate.Text) + "','" + ddlGroupNumber.SelectedItem.Text + " " + txtDrawNumber.Text + "th auction pay deposited thr " + ddlDepositBank.SelectedItem.Text + " Ch No. " + txtChequeNumber.Text + " '," + txtAmount.Text + ",'Deposit','" + balayer.MySQLEscapeString(balayer.ToobjectstrEvenNull(Session["UserName"])) + "',0," + balayer.indiandateToMysqlDate(txtDepositDate.Text).Split('/')[2] + "," + balayer.indiandateToMysqlDate(txtDepositDate.Text).Split('/')[1] + "," + balayer.indiandateToMysqlDate(txtDepositDate.Text).Split('/')[0] + "," + ddlGroupNumber.SelectedItem.Value.Split('|')[2] + ",1," + ddlBank.SelectedValue.Split('|')[1] + "," + ddlGroupNumber.SelectedItem.Value.Split('|')[0] + ",0) ");

                    long CreditBank = trn.insertorupdateTrn("insert into transbank(`BranchID`,`TransactionKey`,`DualTransactionKey`,`T_Day`,`T_Month`,`T_Year`,`Head_Id`,`BankHeadID`,`MemberID`,`CustomersBankName`,`DateInCheque`,`ChequeDDNO`,`GivenAmount`,`TotalChequeDDAmount`,`IsBounced`,`Trans_Type`) values(" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + "," + Credit + "," + DualTransactionKey + "," + balayer.indiandateToMysqlDate(txtDepositDate.Text).Split('/')[2] + "," + balayer.indiandateToMysqlDate(txtDepositDate.Text).Split('/')[1] + "," + balayer.indiandateToMysqlDate(txtDepositDate.Text).Split('/')[0] + "," + ddlBank.SelectedValue.Split('|')[0] + "," + ddlDepositBank.SelectedValue.Split('|')[0] + "," + ddlGroupNumber.SelectedItem.Value.Split('|')[2] + ",'" + ddlBank.SelectedItem.Text + "','" + balayer.indiandateToMysqlDate(txtDepositDate.Text) + "'," + txtChequeNumber.Text + "," + txtAmount.Text + "," + txtAmount.Text + ",0,2)");

                    long Debit = trn.insertorupdateTrn("insert into voucher (`DualTransactionKey`,`BranchID`,`CurrDate`,`Voucher_No`,`Voucher_Type`,`Head_Id`,`ChoosenDate`,`Narration`,`Amount`,`Series`,`ReceievedBy`,`Trans_Type`,`T_Day`,`T_Month`,`T_Year`,`MemberID`,`Trans_Medium`,`RootID`,`ChitGroupId`,`Other_Trans_Type`) values (" + DualTransactionKey + "," + balayer.ToobjectstrEvenNull(Session["Branchid"]) + ",'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," + rr + ",'D'," + ddlDepositBank.SelectedValue.Split('|')[0] + ",'" + balayer.indiandateToMysqlDate(txtDepositDate.Text) + "','" + ddlGroupNumber.SelectedItem.Text + " " + txtDrawNumber.Text + "th auction pay deposited thr " + ddlDepositBank.SelectedItem.Text + " Ch No. " + txtChequeNumber.Text + " '," + txtAmount.Text + ",'Deposit','" + balayer.MySQLEscapeString(balayer.ToobjectstrEvenNull(Session["UserName"])) + "',0," + balayer.indiandateToMysqlDate(txtDepositDate.Text).Split('/')[2] + "," + balayer.indiandateToMysqlDate(txtDepositDate.Text).Split('/')[1] + "," + balayer.indiandateToMysqlDate(txtDepositDate.Text).Split('/')[0] + "," + ddlGroupNumber.SelectedItem.Value.Split('|')[2] + ",1," + ddlDepositBank.SelectedValue.Split('|')[1] + "," + ddlGroupNumber.SelectedItem.Value.Split('|')[0] + ",0) ");

                    long DebitBank = trn.insertorupdateTrn("insert into transbank(`BranchID`,`TransactionKey`,`DualTransactionKey`,`T_Day`,`T_Month`,`T_Year`,`Head_Id`,`BankHeadID`,`MemberID`,`CustomersBankName`,`DateInCheque`,`ChequeDDNO`,`GivenAmount`,`TotalChequeDDAmount`,`IsBounced`,`Trans_Type`) values(" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + "," + Debit + "," + DualTransactionKey + "," + balayer.indiandateToMysqlDate(txtDepositDate.Text).Split('/')[2] + "," + balayer.indiandateToMysqlDate(txtDepositDate.Text).Split('/')[1] + "," + balayer.indiandateToMysqlDate(txtDepositDate.Text).Split('/')[0] + "," + ddlBank.SelectedItem.Value.Split('|')[0] + "," + ddlDepositBank.SelectedItem.Value.Split('|')[0] + "," + ddlGroupNumber.SelectedItem.Value.Split('|')[2] + ",'" + ddlDepositBank.SelectedItem.Text + "','" + balayer.indiandateToMysqlDate(txtDepositDate.Text) + "'," + txtChequeNumber.Text + "," + txtAmount.Text + "," + txtAmount.Text + ",0,2)");


                }

                else
                {
                    long Credit = trn.insertorupdateTrn("insert into voucher (`DualTransactionKey`,`BranchID`,`CurrDate`,`Voucher_No`,`Voucher_Type`,`Head_Id`,`ChoosenDate`,`Narration`,`Amount`,`Series`,`ReceievedBy`,`Trans_Type`,`T_Day`,`T_Month`,`T_Year`,`MemberID`,`Trans_Medium`,`RootID`,`ChitGroupId`,`Other_Trans_Type`) values (" + DualTransactionKey + "," + balayer.ToobjectstrEvenNull(Session["Branchid"]) + ",'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," + rr + ",'C'," + ddlBank.SelectedValue.Split('|')[0] + ",'" + balayer.indiandateToMysqlDate(txtDepositDate.Text) + "','" + ddlGroupNumber.SelectedItem.Text + " " + txtDrawNumber.Text + "th auction pay deposited thr " + ddlDepositBank.SelectedItem.Text + " Ch No. " + txtChequeNumber.Text + " '," + txtAmount.Text + ",'Deposit','" + balayer.MySQLEscapeString(balayer.ToobjectstrEvenNull(Session["UserName"])) + "',0," + balayer.indiandateToMysqlDate(txtDepositDate.Text).Split('/')[2] + "," + balayer.indiandateToMysqlDate(txtDepositDate.Text).Split('/')[1] + "," + balayer.indiandateToMysqlDate(txtDepositDate.Text).Split('/')[0] + "," + ddlGroupNumber.SelectedItem.Value.Split('|')[2] + ",1,"+ ddlBank.SelectedValue.Split('|')[1] + "," + ddlGroupNumber.SelectedItem.Value.Split('|')[0] + ",0) ");

                    long CreditBank = trn.insertorupdateTrn("insert into transbank(`BranchID`,`TransactionKey`,`DualTransactionKey`,`T_Day`,`T_Month`,`T_Year`,`Head_Id`,`BankHeadID`,`MemberID`,`CustomersBankName`,`DateInCheque`,`ChequeDDNO`,`GivenAmount`,`TotalChequeDDAmount`,`IsBounced`,`Trans_Type`) values(" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + "," + Credit + "," + DualTransactionKey + "," + balayer.indiandateToMysqlDate(txtDepositDate.Text).Split('/')[2] + "," + balayer.indiandateToMysqlDate(txtDepositDate.Text).Split('/')[1] + "," + balayer.indiandateToMysqlDate(txtDepositDate.Text).Split('/')[0] + "," + ddlBank.SelectedValue.Split('|')[0] + "," + ddlDepositBank.SelectedValue.Split('|')[0] + "," + ddlGroupNumber.SelectedItem.Value.Split('|')[2] + ",'" + ddlBank.SelectedItem.Text + "','" + balayer.indiandateToMysqlDate(txtDepositDate.Text) + "'," + txtChequeNumber.Text + "," + txtAmount.Text + "," + txtAmount.Text + ",0,2)");

                    long Debit = trn.insertorupdateTrn("insert into voucher (`DualTransactionKey`,`BranchID`,`CurrDate`,`Voucher_No`,`Voucher_Type`,`Head_Id`,`ChoosenDate`,`Narration`,`Amount`,`Series`,`ReceievedBy`,`Trans_Type`,`T_Day`,`T_Month`,`T_Year`,`MemberID`,`Trans_Medium`,`RootID`,`ChitGroupId`,`Other_Trans_Type`) values (" + DualTransactionKey + "," + balayer.ToobjectstrEvenNull(Session["Branchid"]) + ",'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," + rr + ",'D'," + ddlDepositBank.SelectedValue.Split('|')[0] + ",'" + balayer.indiandateToMysqlDate(txtDepositDate.Text) + "','" + ddlGroupNumber.SelectedItem.Text + " " + txtDrawNumber.Text + "th auction pay deposited thr " + ddlDepositBank.SelectedItem.Text + " Ch No. " + txtChequeNumber.Text + " '," + txtAmount.Text + ",'Deposit','" + balayer.MySQLEscapeString(balayer.ToobjectstrEvenNull(Session["UserName"])) + "',0," + balayer.indiandateToMysqlDate(txtDepositDate.Text).Split('/')[2] + "," + balayer.indiandateToMysqlDate(txtDepositDate.Text).Split('/')[1] + "," + balayer.indiandateToMysqlDate(txtDepositDate.Text).Split('/')[0] + "," + ddlGroupNumber.SelectedItem.Value.Split('|')[2] + ",1,"+ ddlDepositBank.SelectedValue.Split('|')[1] + "," + ddlGroupNumber.SelectedItem.Value.Split('|')[0] + ",0) ");

                    long DebitBank = trn.insertorupdateTrn("insert into transbank(`BranchID`,`TransactionKey`,`DualTransactionKey`,`T_Day`,`T_Month`,`T_Year`,`Head_Id`,`BankHeadID`,`MemberID`,`CustomersBankName`,`DateInCheque`,`ChequeDDNO`,`GivenAmount`,`TotalChequeDDAmount`,`IsBounced`,`Trans_Type`) values(" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + "," + Debit + "," + DualTransactionKey + "," + balayer.indiandateToMysqlDate(txtDepositDate.Text).Split('/')[2] + "," + balayer.indiandateToMysqlDate(txtDepositDate.Text).Split('/')[1] + "," + balayer.indiandateToMysqlDate(txtDepositDate.Text).Split('/')[0] + "," + ddlBank.SelectedItem.Value.Split('|')[0] + "," + ddlDepositBank.SelectedItem.Value.Split('|')[0] + "," + ddlGroupNumber.SelectedItem.Value.Split('|')[2] + ",'" + ddlDepositBank.SelectedItem.Text + "','" + balayer.indiandateToMysqlDate(txtDepositDate.Text) + "'," + txtChequeNumber.Text + "," + txtAmount.Text + "," + txtAmount.Text + ",0,2)");

                    // long l = trn.insertorupdateTrn("insert into svcf.DepositPayment(DualTransactionKey,ChitAgreementNumber,ChitNumber,MemberName,DrawNo,DrawDate,Amount,HeadId,DepositHeadId,ChequeNumber,TokenNumber,DepositDate,BranchID) values (" + guidForBinary16 + "," + txtChitAgreementNumber.Text + "," + ddlGroupNumber.SelectedItem.Value.Split('|')[0] + ",'" + balayer.MySQLEscapeString(ddlGroupNumber.SelectedItem.Text) + "'," + txtDrawNumber.Text + ",'" + balayer.indiandateToMysqlDate(txtDrawDate.Text) + "'," + txtAmount.Text + "," + ddlBank.SelectedItem.Value + "," + ddlDepositBank.SelectedItem.Value + "," + txtChequeNumber.Text + "," + ddlGroupNumber.SelectedItem.Value.Split('|')[1] + ",'" + balayer.indiandateToMysqlDate(txtDepositDate.Text) + "'," + Session["Branchid"] + ")");
                }
             
                
                if (ddlGroupNumber.SelectedItem.Text.StartsWith("RCM"))
                {
                    long l = trn.insertorupdateTrn("insert into svcf.depositpayment(DualTransactionKey,ChitAgreementNumber,ChitNumber,MemberName,Amount,HeadId,DepositHeadId,ChequeNumber,TokenNumber,DepositDate,BranchID,Status) values (" + DualTransactionKey + "," + txtChitAgreementNumber.Text + "," + ddlGroupNumber.SelectedItem.Value.Split('|')[0] + ",'" + balayer.MySQLEscapeString(ddlGroupNumber.SelectedItem.Text) + "'," + txtAmount.Text + "," + ddlBank.SelectedItem.Value.Split('|')[0] + "," + ddlDepositBank.SelectedItem.Value.Split('|')[0] + "," + txtChequeNumber.Text + "," + ddlGroupNumber.SelectedItem.Value.Split('|')[1] + ",'" + balayer.indiandateToMysqlDate(txtDepositDate.Text) + "'," + Session["Branchid"] + ",1)");
                }
                else
                {
                    long l = trn.insertorupdateTrn("insert into svcf.depositpayment(DualTransactionKey,ChitAgreementNumber,ChitNumber,MemberName,DrawNo,DrawDate,Amount,HeadId,DepositHeadId,ChequeNumber,TokenNumber,DepositDate,BranchID) values (" + DualTransactionKey + "," + txtChitAgreementNumber.Text + "," + ddlGroupNumber.SelectedItem.Value.Split('|')[0] + ",'" + balayer.MySQLEscapeString(ddlGroupNumber.SelectedItem.Text) + "'," + txtDrawNumber.Text + ",'" + balayer.indiandateToMysqlDate(txtDrawDate.Text) + "'," + txtAmount.Text + "," + ddlBank.SelectedItem.Value.Split('|')[0] + "," + ddlDepositBank.SelectedItem.Value.Split('|')[0] + "," + txtChequeNumber.Text + "," + ddlGroupNumber.SelectedItem.Value.Split('|')[1] + ",'" + balayer.indiandateToMysqlDate(txtDepositDate.Text) + "'," + Session["Branchid"] + ")");
                }
                trn.CommitTrn();
                logger.Info("DepositPayment.aspx - btnPayment_Click():  Completed: " + DateTime.Now + " by: " + Convert.ToString(Session["UserName"]) + "");
            }
            catch (Exception ex)
            {
               trn.RollbackTrn();
                ModalPopupExtender1.PopupControlID = "pnlmsg";
                ModalPopupExtender1.Show();
                pnlmsg.Visible = true;
                lblh.Text = "Status";
                lblcon.Text = ex.Message;
                lblcon.ForeColor = System.Drawing.Color.Red;
                isVal = false;
                logger.Info("DepositPayment.aspx - btnPayment_Click():  Error: " + ex.Message + ": " + DateTime.Now + " by: " + Convert.ToString(Session["UserName"]) + "");
            }
            finally
            {
                trn.DisposeTrn();
                if (isVal)
                {
                    ModalPopupExtender1.PopupControlID = "pnlmsg";
                    ModalPopupExtender1.Show();
                    pnlmsg.Visible = true;
                    lblh.Text = "Status";
                    lblcon.Text = ddlGroupNumber.SelectedItem.Text + " - Deposited Successfully";
                    lblcon.ForeColor = System.Drawing.Color.Green;
                }
            }
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.Url.AbsolutePath.ToString());
        }
        protected void btnOK_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.Url.AbsolutePath.ToString());
        }
        protected void btnCan_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.Url.AbsolutePath.ToString());
        }
    }
}