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
    public partial class FilingDocument : System.Web.UI.Page
    {
        #region Object
        BusinessLayer balayer = new BusinessLayer();
        #endregion

        ILog logger = log4net.LogManager.GetLogger(typeof(FilingDocument));

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
               
                txtDepositDate.Text = DateTime.Now.ToString("dd/MM/yyyy");

                DataTable dtChit1 = balayer.GetDataTable("SELECT concat(g1.GROUPNO,'|',g1.GROUPNO) as MemberName,concat(cast(g1.Head_Id as char),'|',cast(g1.Head_Id as char),'|',cast(g1.Head_Id as char)) as MemberID FROM groupmaster as g1 where g1.BranchID=" + Session["Branchid"]);


                DataTable dtChit = balayer.GetDataTable("SELECT concat(g1.GrpMemberID,'|',g1.MemberName) as MemberName,concat(cast(g1.GroupID as char),'|',cast(g1.Head_ID as char),'|',cast(g1.MemberID as char)) as MemberID FROM membertogroupmaster as g1 where g1.BranchID=" + Session["Branchid"]);

                dtChit.Merge(dtChit1);

                DataRow drChit = dtChit.NewRow();
                drChit[0] = "--Select--";
                drChit[1] = "0";
                ddlGroupNumber.DataSource = dtChit;
                ddlGroupNumber.DataTextField = "MemberName";
                ddlGroupNumber.DataValueField = "MemberID";
                dtChit.Rows.InsertAt(drChit, 0);
                ddlGroupNumber.DataBind();
                //DataTable dtBank = balayer.GetDataTable("SELECT concat(cast(  v1.`RootID` as char),',',cast(v1.`TreeID` as char)) as ID,v1.`TREE`,m1.`BranchID`,b1.BranchID,b2.Head_Id FROM `svcf`.`view_parent` as v1 left join `membertogroupmaster` as m1 on (v1.`TreeID` =m1.Head_Id) left join `bankdetails` as b1 on (v1.`TreeID` =b1.Head_Id) left join `branchdetails` as b2 on (v1.`TreeID` =b2.Head_Id) where (m1.`BranchID` is null or m1.`BranchID`=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + ") and (b1.BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " or b1.BranchID is null) and (b2.Head_Id<>" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " or b2.Head_Id is null)");
                DataTable dtBank = balayer.GetDataTable("SELECT concat(cast(TreeID as char),',',cast(RootID as char)) as ID,TREE FROM svcf.view_parent where RootID not in (5,6,7,2,4,11,8,9) and (BranchID is null or BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " )");
                DataRow drBank = dtBank.NewRow();
                drBank[0] = "0";
                drBank[1] = "--Select--";
                ddlCredit.DataSource = dtBank;
                ddlCredit.DataTextField = "TREE";
                ddlCredit.DataValueField = "ID";
                dtBank.Rows.InsertAt(drBank, 0);
                ddlCredit.DataBind();
            }

            rvDepositDate.MinimumValue = balayer.GetSingleValue("SELECT DATE_FORMAT( min(MinimumDate), '%d/%m/%Y') MinimumDate FROM svcf.restrictionmaster where BranchID=" + Session["Branchid"]);
            //rvDepositDate.MinimumValue = balayer.GetSingleValue("SELECT DATE_FORMAT( min(ChoosenDate), '%d/%m/%Y') MinimumDate FROM svcf.voucher where " +
            //  "BranchID=" + Session["Branchid"] + " and ChoosenDate<>'0000-00-00'");

            rvDepositDate.MaximumValue = DateTime.Now.ToString("dd/MM/yyyy");
        }
        protected void Page_PreRender(object sender, EventArgs e)
        {
            ViewState["CheckRefresh"] = Session["CheckRefresh"];
        }
        //protected void ddlGroupNumber_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    string prizedmemberID=ddlGroupNumber.SelectedValue.Split('|')[1];
        //    DataTable dt = balayer.GetDataTable("SELECT DrawNO,PrizedAmount,AuctionDate FROM svcf.auctiondetails where PrizedMemberID=" + prizedmemberID);
        //    if (dt.Rows.Count > 0)
        //    {
        //        txtDrawNumber.Text = balayer.ToobjectstrEvenNull(dt.Rows[0]["DrawNO"]);
        //        txtDrawDate.Text = balayer.ToobjectstrEvenNull(dt.Rows[0]["AuctionDate"].ToString());
        //    }
        //    else
        //    {
        //        txtDrawNumber.Text = "0";
        //        txtDrawDate.Text = "00/00/0000";
        //    }
        //}

        [System.Web.Services.WebMethod]
        public static List<string> LoadDetails(string prizedmemID)
        {
            BusinessLayer balayer = new BusinessLayer();
           
              List<string> TList = new List<string>();
              TList = balayer.BindAuctionDetails("SELECT DrawNO,PrizedAmount,date_format(AuctionDate,'%d/%m/%Y') as AuctionDate FROM svcf.auctiondetails where PrizedMemberID=" + prizedmemID);
            
            return TList;
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
                System.Guid guid = Guid.NewGuid();
                string guidForChar36 = guid.ToString();
                string hexstring = BitConverter.ToString(guid.ToByteArray());
                string guidForBinary16 = "0x" + hexstring.Replace("-", string.Empty);
                //Random rnd = new Random();
                //int rr = rnd.Next(1, 1000);
                string rr = balayer.GetSingleValue("SELECT ifnull(max(cast(Voucher_No as unsigned)),0)+1 FROM svcf.voucher where ChoosenDate>='2014/04/01' and  Series='Filing_Fees' and Trans_Type='0' and BranchID=" + Session["Branchid"]);
                long Credit = trn.insertorupdateTrn("insert into voucher (`DualTransactionKey`,`BranchID`,`CurrDate`,`Voucher_No`,`Voucher_Type`,`Head_Id`,`ChoosenDate`,`Narration`,`Amount`,`Series`,`ReceievedBy`,`Trans_Type`,`T_Day`,`T_Month`,`T_Year`,`MemberID`,`Trans_Medium`,`RootID`,`ChitGroupId`,`Other_Trans_Type`) values (" + guidForBinary16 + "," + balayer.ToobjectstrEvenNull(Session["Branchid"]) + ",'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," + rr + ",'C'," + ddlCredit.SelectedValue.Split(',')[0] + ",'" + balayer.indiandateToMysqlDate(txtDepositDate.Text) + "','" + txtDescription.Text + "'," + txtAmount.Text + ",'Filing_Fees','" + balayer.MySQLEscapeString(balayer.ToobjectstrEvenNull(Session["UserName"])) + "',0," + balayer.indiandateToMysqlDate(txtDepositDate.Text).Split('/')[2] + "," + balayer.indiandateToMysqlDate(txtDepositDate.Text).Split('/')[1] + "," + balayer.indiandateToMysqlDate(txtDepositDate.Text).Split('/')[0] + "," + ddlGroupNumber.SelectedItem.Value.Split('|')[2] + ",0," + ddlCredit.SelectedValue.Split(',')[1] + "," + ddlGroupNumber.SelectedItem.Value.Split('|')[0] + ",0) ");
                if (ddlCredit.SelectedItem.Value.Split(',')[0] == "3")
                {
                    long CreditBank = trn.insertorupdateTrn("insert into transbank(`BranchID`,`TransactionKey`,`DualTransactionKey`,`T_Day`,`T_Month`,`T_Year`,`Head_Id`,`BankHeadID`,`MemberID`,`CustomersBankName`,`DateInCheque`,`ChequeDDNO`,`GivenAmount`,`TotalChequeDDAmount`,`IsBounced`,`Trans_Type`) values(" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + "," + Credit + "," + guidForBinary16 + "," + balayer.indiandateToMysqlDate(txtDepositDate.Text).Split('/')[2] + "," + balayer.indiandateToMysqlDate(txtDepositDate.Text).Split('/')[1] + "," + balayer.indiandateToMysqlDate(txtDepositDate.Text).Split('/')[0] + "," + ddlCredit.SelectedValue.Split(',')[1] + "," + ddlCredit.SelectedValue.Split(',')[1] + "," + ddlGroupNumber.SelectedItem.Value.Split('|')[2] + ",'" + ddlCredit.SelectedItem.Text + "','" + balayer.indiandateToMysqlDate(txtDepositDate.Text) + "',000001," + txtAmount.Text + "," + txtAmount.Text + ",0,2)");
                }
                long Debit = trn.insertorupdateTrn("insert into voucher (`DualTransactionKey`,`BranchID`,`CurrDate`,`Voucher_No`,`Voucher_Type`,`Head_Id`,`ChoosenDate`,`Narration`,`Amount`,`Series`,`ReceievedBy`,`Trans_Type`,`T_Day`,`T_Month`,`T_Year`,`MemberID`,`Trans_Medium`,`RootID`,`ChitGroupId`,`Other_Trans_Type`) values (" + guidForBinary16 + "," + balayer.ToobjectstrEvenNull(Session["Branchid"]) + ",'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," + rr + ",'D',119,'" + balayer.indiandateToMysqlDate(txtDepositDate.Text) + "','" + txtDescription.Text + "'," + txtAmount.Text + ",'Filing_Fees','" + balayer.MySQLEscapeString(balayer.ToobjectstrEvenNull(Session["UserName"])) + "',0," + balayer.indiandateToMysqlDate(txtDepositDate.Text).Split('/')[2] + "," + balayer.indiandateToMysqlDate(txtDepositDate.Text).Split('/')[1] + "," + balayer.indiandateToMysqlDate(txtDepositDate.Text).Split('/')[0] + "," + ddlGroupNumber.SelectedItem.Value.Split('|')[2] + ",1,11," + ddlGroupNumber.SelectedItem.Value.Split('|')[0] + ",0) ");

                long l = trn.insertorupdateTrn("insert into svcf.filingfees (DualTransactionKey,ChitNumber,GroupNumber,MemberName,ChoosenDate,DrawDate,DrawNumber,Amount,Details,HeadID,FilingID,MemberID,BranchID) values (" + guidForBinary16 + "," + ddlGroupNumber.SelectedItem.Value.Split('|')[1] + "," + ddlGroupNumber.SelectedItem.Value.Split('|')[0] + ",'" + ddlGroupNumber.SelectedItem.Text.Split('|')[1] + "','" + balayer.indiandateToMysqlDate(txtDepositDate.Text) + "','" + balayer.indiandateToMysqlDate(txtDrawDate.Text) + "','" + txtDrawNumber.Text + "'," + txtAmount.Text + ",'" + balayer.MySQLEscapeString(txtDescription.Text) + "'," + ddlCredit.SelectedItem.Value.Split(',')[1] + ",119," + ddlGroupNumber.SelectedItem.Value.Split('|')[2] + "," + Session["Branchid"] + ")");
                trn.CommitTrn();
                logger.Info("FilingDocument.aspx - btnPayment_Click():  Completed: " + DateTime.Now + " by: " + Convert.ToString(Session["UserName"]) + "");
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
                logger.Info("FilingDocument.aspx - btnPayment_Click():  Error:" + ex.Message + ": " + DateTime.Now + " by: " + Convert.ToString(Session["UserName"]) + "");
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