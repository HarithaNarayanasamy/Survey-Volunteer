using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SVCF_BusinessAccessLayer;
using SVCF_TransactionLayer;
using System.Data;
using log4net;
using log4net.Config;
using System.Globalization;
using Spire.Xls;

namespace SreeVisalamChitFundLtd_phase1
{
    public partial class AppVoucherMoneyCollector : System.Web.UI.Page
    {
        #region var Declaration
        BusinessLayer balayer = new BusinessLayer();
        TransactionLayer trn = new TransactionLayer();

        ILog logger = log4net.LogManager.GetLogger(typeof(AppVoucherMoneyCollector));

        string userinfo = "";
        string qry = "";
        string usrRole = "";

        #endregion
        protected void Page_PreRender(object sender, EventArgs e)
        {
            ViewState["CheckRefresh"] = Session["CheckRefresh"];
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                RadApprove.Checked = false;
                RadView.Checked = false;
                Appvoucherapproval.Visible = true;
                loadMoneyCollector();

                txtFrom.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtTo.Text = DateTime.Now.ToString("dd/MM/yyyy");
            }


        }
        public DataTable getReceipts()
        {
            //DataTable dtReceipts = balayer.GetDataTable("select mv.DualTransactionKey,mv.BranchID,mv.Head_Id,mr.AppReceiptno,mv.CurrDate,mv.Amount,mv.Series,mv.M_Id,mv.ChitGroupId,mv.MoneyCollId,mv.TransactionKey,mv.ChoosenDate,mr.BranchName,mv.Interest,mg.GrpMemberID as ChitNo from mobileappvoucher as mv join mobilereceipt as mr on (mr.AppReceiptno=mv.AppReceiptno) left join membertogroupmaster as mg on (mv.Head_Id=mg.Head_Id) where mr.BranchID=mv.BranchID and mv.Voucher_Type='C' and mv.Series='BCAPP' and mv.BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"])+" and mv.MoneyCollId="+moneyCollId);
            //DataTable dtReceipts = balayer.GetDataTable("SELECT mv.DualTransactionKey,mv.BranchID,b1.B_Name as BranchName,mv.Head_Id,mv.AppReceiptno,mv.CurrDate,mv.Amount,mv.Series,mv.M_Id,mv.ChitGroupId,mv.MoneyCollId,mv.TransactionKey,mv.ChoosenDate,mv.Interest,mg.GrpMemberID AS ChitNo FROM mobileappvoucher AS mv LEFT JOIN membertogroupmaster AS mg ON (mv.Head_Id = mg.Head_Id) left join branchdetails as b1 on(mv.BranchID=b1.Head_Id) WHERE mv.Voucher_Type = 'C' and mv.Series='BCAPP' and mv.IsAccepted=0 and mv.BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"])+" and mv.MoneyCollId="+moneyCollId);
            int moneyCollId = Convert.ToInt32(ddlMoneyColl.SelectedValue);
            if (ddlMoneyColl.SelectedItem.Text == "--Select--")
            {
                DataTable dtReceipts = balayer.GetDataTable("SELECT mv.DualTransactionKey,mv.BranchID,b1.B_Name as BranchName,mv.Head_Id,mv.AppReceiptno,convert_tz( mv.CurrDate,'+00:00','+05:30') as CurrDate,mv.Amount,mv.Series,mv.M_Id,mv.ChitGroupId,mv.MoneyCollId,mv.TransactionKey,mv.ChoosenDate,mv.Interest,mv.Type,mg.GrpMemberID AS ChitNo FROM mobileappvoucher AS mv LEFT JOIN membertogroupmaster AS mg ON (mv.Head_Id = mg.Head_Id) left join branchdetails as b1 on(mv.BranchID=b1.Head_Id) WHERE mv.Voucher_Type = 'C' and mv.Series='BCAPP' and mv.IsAccepted=0 and mv.BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and mv.ChoosenDate between '" + balayer.changedateformat(Convert.ToDateTime(txtFrom.Text), 2) + "' and '" + balayer.changedateformat(Convert.ToDateTime(txtTo.Text), 2) + "';");
                return dtReceipts;
            }
            else
            {
                DataTable dtReceipts = balayer.GetDataTable("SELECT mv.DualTransactionKey,mv.BranchID,b1.B_Name as BranchName,mv.Head_Id,mv.AppReceiptno,convert_tz( mv.CurrDate,'+00:00','+05:30') as CurrDate,mv.Amount,mv.Series,mv.M_Id,mv.ChitGroupId,mv.MoneyCollId,mv.TransactionKey,mv.ChoosenDate,mv.Interest,mv.Type,mg.GrpMemberID AS ChitNo FROM mobileappvoucher AS mv LEFT JOIN membertogroupmaster AS mg ON (mv.Head_Id = mg.Head_Id) left join branchdetails as b1 on(mv.BranchID=b1.Head_Id) WHERE mv.Voucher_Type = 'C' and mv.Series='BCAPP' and mv.IsAccepted=0 and mv.BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and mv.MoneyCollId=" + moneyCollId + " and mv.ChoosenDate between '" + balayer.changedateformat(Convert.ToDateTime(txtFrom.Text), 2) + "' and '" + balayer.changedateformat(Convert.ToDateTime(txtTo.Text), 2) + "';");
                return dtReceipts;
            }

        }

        public void loadMoneyCollector()
        {
            DataTable dtMoneyColl = balayer.GetDataTable("select moneycollid,moneycollname from moneycollector where BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]));
            DataRow dr = dtMoneyColl.NewRow();
            dr["moneycollid"] = 0;
            dr["moneycollname"] = "--Select--";
            dtMoneyColl.Rows.InsertAt(dr, 0);
            ddlMoneyColl.DataTextField = "moneycollname";
            ddlMoneyColl.DataValueField = "moneycollid";
            ddlMoneyColl.Items.Insert(0, "--Select--");
            ddlMoneyColl.DataSource = dtMoneyColl;
            ddlMoneyColl.DataBind();
        }

        protected void RadApprove_CheckedChanged(object sender, EventArgs e)
        {
            if (RadApprove.Checked == true)
            {
                RadApprove.Checked = true;
                RadView.Checked = false;
                Appvoucherapproval.Visible = true;
                ViewApproved.Visible = false;
                imgexport.Visible = false;
            }
        }

        protected void RadView_CheckedChanged(object sender, EventArgs e)
        {
            if (RadView.Checked == true)
            {
                RadView.Checked = true;
                RadApprove.Checked = false;
                ViewApproved.Visible = true;
                Appvoucherapproval.Visible = false;
                imgexport.Visible = true;
                //string moneyCollId = ddlMoneyColl.SelectedValue;
                //string series = "BCAPP";
                //approvalview.LoadData(series, moneyCollId);
            }
        }

        protected void Approve_Click(object sender, EventArgs e)
        {
            if (Convert.ToString(Session["CheckRefresh"]) != Convert.ToString(ViewState["CheckRefresh"]))
            {
                return;
            }
            try
            {
                //lblAppDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                btnConfirm.Visible = true;
                btnRejectConfirm.Visible = false;
                lblAppDate.Visible = true;
                txtReason.Visible = false;
                ImageButton btnDetails = sender as ImageButton;
                GridViewRow gvRow = (GridViewRow)btnDetails.NamingContainer;
                lblDate.Text = "Approval date :";
                lblAppDate.Text = balayer.ToobjectstrEvenNull(Gridview1.DataKeys[gvRow.RowIndex]["ChoosenDate"]).ToString();
                Session["Row"] = gvRow;
                ModalPopupExtender1.PopupControlID = "msgbox";
                ModalPopupExtender1.TargetControlID = "show";

                lblContent.Text = "Do you want to continue?";
                ModalPopupExtender1.Show();
            }
            catch (Exception ex)
            {

            }
        }

        protected void Reject_Click(object sender, ImageClickEventArgs e)
        {
            if (Convert.ToString(Session["CheckRefresh"]) != Convert.ToString(ViewState["CheckRefresh"]))
            {
                return;
            }
            try
            {
                btnRejectConfirm.Visible = true;
                txtReason.Visible = true;
                lblAppDate.Visible = false;
                btnConfirm.Visible = false;
                ImageButton btnDetails = sender as ImageButton;
                GridViewRow gvRow = (GridViewRow)btnDetails.NamingContainer;
                Session["Row"] = gvRow;
                lblDate.Text = "Reason : ";
                txtReason.Text = "";
                ModalPopupExtender1.PopupControlID = "msgbox";
                ModalPopupExtender1.TargetControlID = "show";
                lblContent.Text = "Do you want to Cancel the Receipt?";
                ModalPopupExtender1.Show();
            }
            catch (Exception ex)
            {

            }
        }

        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            //if(Session["CheckRefresh"].ToString()!=ViewState["CheckRefresh"].ToString())
            //{
            //    return;
            //}

            ClsSession objSession = (ClsSession)Session["objSession"];

            try
            {
                //System.Guid guid = Guid.NewGuid();
                //string hexstring = BitConverter.ToString(guid.ToByteArray());
                //string guidForBinary16 = "0x" + hexstring.Replace("-", string.Empty);
                //string DualTransactionKey = Convert.ToString( guidForBinary16 );

                //15/6/2023
                System.Guid guid = Guid.NewGuid();
                string hexstring = BitConverter.ToString(guid.ToByteArray());
                string guidForBinary16 = "0x" + hexstring.Replace("-", string.Empty);
                string DualTransactionKey = guidForBinary16;

                //string DualTransactionKey = Convert.ToString(balayer.sp_gendratedt_key());

                GridViewRow gvRow = (GridViewRow)Session["Row"];
                string transactionKey = balayer.ToobjectstrEvenNull(Gridview1.DataKeys[gvRow.RowIndex]["TransactionKey"]);
                //string dualTransactionKey = balayer.ToobjectstrEvenNull(Gridview1.DataKeys[gvRow.RowIndex]["DualTransactionKey"]);
                string Head_Id = balayer.ToobjectstrEvenNull(Gridview1.DataKeys[gvRow.RowIndex]["Head_Id"]);
                string appReceiptno = balayer.ToobjectstrEvenNull(Gridview1.DataKeys[gvRow.RowIndex]["AppReceiptno"]);
                string branchName = balayer.ToobjectstrEvenNull(Gridview1.DataKeys[gvRow.RowIndex]["BranchName"]);
                string branchId = balayer.ToobjectstrEvenNull(Gridview1.DataKeys[gvRow.RowIndex]["BranchID"]);
                string currDate = balayer.ToobjectstrEvenNull(Gridview1.DataKeys[gvRow.RowIndex]["CurrDate"]);
                string series = balayer.ToobjectstrEvenNull(Gridview1.DataKeys[gvRow.RowIndex]["Series"]);
                string memberId = balayer.ToobjectstrEvenNull(Gridview1.DataKeys[gvRow.RowIndex]["M_Id"]);
                string chitGrpId = balayer.ToobjectstrEvenNull(Gridview1.DataKeys[gvRow.RowIndex]["ChitGroupId"]);
                //string type = balayer.ToobjectstrEvenNull(Gridview1.DataKeys[gvRow.RowIndex]["Type"]);
                string type = "";
                //string Head_Id = "";

                //voucher number
                var vouch_no = balayer.GetSingleValue("SELECT ifnull(max(cast(Voucher_No as unsigned)),0)+1 FROM svcf.voucher where ChoosenDate>='2014/04/01' and  Series='BCAPP' and Trans_Type='1' and BranchID=" + Session["Branchid"]);
                int voucherNo = int.Parse(vouch_no);

                var qry = "select * from svcf.mobileappvoucher where AppReceiptno='" + appReceiptno + "' and Voucher_Type='C' and BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + ";";
                DataTable dtVou = balayer.GetDataTable(qry);
                DateTime choosenDate;
                string drawNo = balayer.GetSingleValue("select DrawNO from auctiondetails where GroupID=" + chitGrpId + " and AuctionDate>now() limit 1;");
                string membername = balayer.GetSingleValue("select MemberName from membertogroupmaster where Head_Id=" + balayer.ToobjectstrEvenNull(Gridview1.DataKeys[gvRow.RowIndex]["Head_Id"]));
                string TokenNo = balayer.GetSingleValue("select GrpMemberID from membertogroupmaster where Head_Id=" + balayer.ToobjectstrEvenNull(Gridview1.DataKeys[gvRow.RowIndex]["Head_Id"]));
                string narration = "Recd From:- " + appReceiptno + " " + membername + ":" + TokenNo + ":" + " For Inst No:";
                string narr1 = "";
                string RootID = "12";
                string trans_medium = "0";
                string cashNarration = "";
                string tonarration = "";
                //12/05/2021
                string Due = "";
                Int64 firstValue = 0;
                Int64 secondValue = 0;
                string maxDraw = "";

                //
                string intNarration = "PROFIT AND LOSS ACCOUNT>>Interest on Chit Debts Recd From" + membername + ":" + TokenNo + ":" + appReceiptno;

                for (int i = 0; i < dtVou.Rows.Count; i++)
                {
                    //choosenDate = DateTime.Now;
                    choosenDate = Convert.ToDateTime(lblAppDate.Text);
                    //Branch wise 
                    int branch_id = int.Parse(balayer.GetSingleValue("select BranchID from groupmaster where Head_Id=" + chitGrpId));
                    type = dtVou.Rows[i]["Type"].ToString();
                    if (type == "Cash")
                    {
                        try
                        {
                            #region Narration
                            //Narration
                            decimal TotalPaidAmount = decimal.Parse(balayer.GetSingleValue("SELECT ifnull( (sum(IF(Voucher_Type = 'C',Amount,0.00)) -sum(IF(Voucher_Type = 'D',Amount,0.00)) ),0.00) AS TransactionAmount FROM `svcf`.`voucher` where  Head_Id=" + Head_Id + " and  (Trans_Type<>2) and Other_Trans_Type<>5"));
                            decimal AddTotalPaidAmount = TotalPaidAmount;
                            string FromNarration = "";
                            string ToNarration = "";
                            int FromDraw = 0;
                            int ToDraw = 0;
                            decimal excess = 0; //12/05/2021
                            Due = balayer.GetSingleValue("Select currentdueamount from auctiondetails where GroupID=" + chitGrpId + " and DrawNO=1 and AuctionDate<='" + DateTime.Now.ToString("yyyy/MM/dd") + "'");   //12/05/2021
                            maxDraw = balayer.GetSingleValue("select max(DrawNO) from auctiondetails where GroupID=" + chitGrpId);
                            //
                            if (TotalPaidAmount == 0.00M)
                            {
                                FromNarration = "1";
                                FromDraw = 1;
                                TotalPaidAmount = TotalPaidAmount + Convert.ToDecimal(dtVou.Rows[i]["Amount"]);
                                qry = "SELECT DrawNO,CurrentDueAmount FROM `svcf`.`auctiondetails` where GroupID=" + chitGrpId + " and CurrentDueAmount<>'0.00' order by DrawNO";
                                DataTable dtAuction = balayer.GetDataTable(qry);
                                for (int iAuc = 0; iAuc < dtAuction.Rows.Count; iAuc++)
                                {
                                    decimal currentDueAmt = decimal.Parse(dtAuction.Rows[iAuc]["CurrentDueAmount"].ToString());
                                    TotalPaidAmount = TotalPaidAmount - currentDueAmt;
                                    decimal tempDueAmt = TotalPaidAmount;
                                    if (tempDueAmt == 0.00M)
                                    {
                                        ToNarration = (iAuc + 1).ToString();
                                        ToDraw = iAuc + 1;
                                        break;
                                    }
                                    else if (tempDueAmt < 0.00M)
                                    {
                                        ToNarration = (iAuc + 1) + "PP";
                                        ToDraw = iAuc + 1;
                                        break;
                                    }

                                    excess = tempDueAmt;    //12/05/2021
                                }

                                if (ToNarration == "")
                                {
                                    //12/05/2021//FromNarration += "To" + (int.Parse(dtAuction.Rows[dtAuction.Rows.Count - 1]["DrawNO"].ToString()) + 1).ToString() + "+ Excess Payment";
                                    decimal excessCount = excess / Convert.ToDecimal(Due);
                                    var values = excessCount.ToString(CultureInfo.InvariantCulture).Split('.');

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
                                        ToNarration = "-" + Convert.ToInt16(dtAuction.Rows[dtAuction.Rows.Count - 1]["DrawNo"]) + firstValue + "F";
                                    }
                                    else
                                    {
                                        ToNarration = "-" + Convert.ToInt16(dtAuction.Rows[dtAuction.Rows.Count - 1]["DrawNo"]) + firstValue + 1 + "P";
                                    }

                                    FromNarration += ToNarration;
                                }
                                if (FromDraw != ToDraw)
                                {
                                    FromNarration += " To " + ToNarration;
                                }

                                cashNarration = FromNarration;
                            }
                            else
                            {
                                DataTable dtAuction = balayer.GetDataTable("SELECT DrawNO,CurrentDueAmount FROM `svcf`.`auctiondetails` where GroupID=" + chitGrpId + " and CurrentDueAmount<>'0.00' order by DrawNO");
                                TotalPaidAmount = AddTotalPaidAmount;
                                for (int iAuc = 0; iAuc < dtAuction.Rows.Count; iAuc++)
                                {
                                    decimal currentDueAmount = decimal.Parse(dtAuction.Rows[iAuc]["CurrentDueAmount"].ToString());
                                    TotalPaidAmount = TotalPaidAmount - currentDueAmount;
                                    decimal tempDueAmount = TotalPaidAmount;
                                    if (tempDueAmount == 0.00M)
                                    {
                                        FromNarration = (iAuc + 2).ToString() + "F";
                                        FromDraw = iAuc + 2;
                                        break;
                                    }
                                    else if (tempDueAmount < 0.00M)
                                    {
                                        FromNarration = iAuc + 1 + " PP";
                                        FromDraw = iAuc + 1;
                                        break;
                                    }
                                    else
                                    {
                                        excess = tempDueAmount;
                                    }
                                }
                                if (FromNarration == "")
                                {
                                    if (dtAuction.Rows.Count > 0)
                                    {
                                        //12/05/2021//FromNarration = (int.Parse(dtAuction.Rows[dtAuction.Rows.Count - 1]["DrawNO"].ToString()) + 1).ToString() + "+ Excess Payment";
                                        decimal excessCount = excess / Convert.ToDecimal(Due);
                                        var values = excessCount.ToString(CultureInfo.InvariantCulture).Split('.');

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
                                            if (Convert.ToInt16(dtAuction.Rows[dtAuction.Rows.Count - 1]["DrawNo"]) == Convert.ToInt16(maxDraw))
                                            {
                                                ToNarration = Convert.ToInt16(maxDraw) + " + Excess";
                                            }
                                            else
                                            {
                                                ToNarration = Convert.ToInt16(dtAuction.Rows[dtAuction.Rows.Count - 1]["DrawNo"]) + firstValue + "F";
                                            }
                                        }
                                        else
                                        {
                                            if (Convert.ToInt16(dtAuction.Rows[dtAuction.Rows.Count - 1]["DrawNo"]) + firstValue == Convert.ToInt16(maxDraw))
                                            {
                                                ToNarration = Convert.ToInt16(maxDraw) + " + Excess";
                                            }
                                            else
                                            {
                                                ToNarration = Convert.ToInt16(dtAuction.Rows[dtAuction.Rows.Count - 1]["DrawNo"]) + firstValue + 1 + "P";
                                            }
                                        }

                                        FromNarration = ToNarration;
                                        //
                                    }
                                }
                                else
                                {
                                    TotalPaidAmount = AddTotalPaidAmount;
                                    TotalPaidAmount = TotalPaidAmount + decimal.Parse(dtVou.Rows[i]["Amount"].ToString());
                                    for (int iAuc = 0; iAuc < dtAuction.Rows.Count; iAuc++)
                                    {
                                        decimal currentDueAmount = decimal.Parse(dtAuction.Rows[iAuc]["CurrentDueAmount"].ToString());
                                        TotalPaidAmount = TotalPaidAmount - currentDueAmount;
                                        decimal tempDueAmount = TotalPaidAmount;
                                        if (tempDueAmount == 0.00M)
                                        {
                                            ToNarration = (iAuc + 1).ToString() + "F";
                                            ToDraw = iAuc + 1;
                                            break;
                                        }
                                        else if (tempDueAmount < 0.00M)
                                        {
                                            ToDraw = iAuc + 1;
                                            ToNarration = iAuc + 1 + " PP";
                                            break;
                                        }
                                        else
                                        {
                                            excess = tempDueAmount;
                                        }
                                    }
                                    if (ToNarration == "")
                                    {
                                        //12/05/2021
                                        //ToNarration = "+ Excess Payment";
                                        decimal excessCount = excess / Convert.ToDecimal(Due);
                                        var values = excessCount.ToString(CultureInfo.InvariantCulture).Split('.');

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
                                            ToNarration = Convert.ToInt16(dtAuction.Rows[dtAuction.Rows.Count - 1]["DrawNo"]) + firstValue + "F";
                                        }
                                        else
                                        {
                                            ToNarration = Convert.ToInt16(dtAuction.Rows[dtAuction.Rows.Count - 1]["DrawNo"]) + firstValue + 1 + "P";
                                        }
                                        //
                                    }
                                }
                                if (ToNarration != "")
                                {
                                    if (FromDraw != ToDraw)
                                    {
                                        //RowddlToken.ToolTip = FromNarration + " To " + ToNarration;
                                        //12/05/2021
                                        if (FromNarration.Contains("P"))
                                            FromNarration = FromNarration.Trim('P');
                                        else if (FromNarration.Contains("F"))
                                            FromNarration = FromNarration.Trim('F');
                                        //paidInstalment = instFrom + "F" + "-" + instTo;
                                        //
                                        tonarration = FromNarration + "F To " + ToNarration;
                                        cashNarration = tonarration;
                                    }
                                    else
                                    {
                                        //RowddlToken.ToolTip = ToNarration;
                                        tonarration = ToNarration;
                                        cashNarration = ToNarration;
                                    }
                                }
                                else
                                {
                                    //RowddlToken.ToolTip = FromNarration;
                                    tonarration = FromNarration;
                                    cashNarration = FromNarration;
                                }
                            }
                            #endregion
                        }
                        catch (Exception ex)
                        {
                            throw;
                        }
                        try
                        {

                            if (Convert.ToString(branch_id) == balayer.TostrEvenNull(Convert.ToString(Session["Branchid"])))
                            {
                                //Head_Id =Convert.ToString( branch_id);

                                string query = "INSERT INTO `svcf`.`voucher` (`DualTransactionKey`,`BranchID`,`CurrDate`,`Voucher_No`,`Voucher_Type`,`Head_Id`,`ChoosenDate`,`Narration`," +
                                "`Amount`,`Series`,`ReceievedBy`,`Trans_Type`,`T_Day`,`T_Month`,`T_Year`,`MemberID`,`Trans_Medium`,`RootID`,`ChitGroupId`,`Other_Trans_Type`,`AppReceiptno`,`IsDeleted`,`IsAccepted`,`M_Id`,`LoginIP`) VALUES (" + DualTransactionKey + "," +
                                balayer.TostrEvenNull(Convert.ToString(Session["Branchid"])) + ",'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," + voucherNo + ",'C'," + Head_Id + ",'" + choosenDate.ToString("yyyy-MM-dd") + "','" + narration + cashNarration +
                                "'," + Convert.ToString(dtVou.Rows[i]["Amount"]) + ",'" + series + "','" + Convert.ToString(dtVou.Rows[i]["ReceievedBy"]) + "',1," + choosenDate.Day + "," + choosenDate.Month + "," +
                                choosenDate.Year + "," + memberId + "," + trans_medium + ",5," + chitGrpId + ",0,'" + appReceiptno + "',0,0," + dtVou.Rows[i]["MoneyCollId"] + ",'" + objSession.LoginIp + "');";

                                string query1 = "INSERT INTO `svcf`.`voucher` (`DualTransactionKey`,`BranchID`,`CurrDate`,`Voucher_No`,`Voucher_Type`,`Head_Id`,`ChoosenDate`,`Narration`," +
                                "`Amount`,`Series`,`ReceievedBy`,`Trans_Type`,`T_Day`,`T_Month`,`T_Year`,`MemberID`,`Trans_Medium`,`RootID`,`ChitGroupId`,`Other_Trans_Type`,`AppReceiptno`,`IsDeleted`,`IsAccepted`,`M_Id`,`LoginIP`) VALUES (" + DualTransactionKey + "," +
                                balayer.TostrEvenNull(Convert.ToString(Session["Branchid"])) + ",'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," + voucherNo + ",'D', '12','" + choosenDate.ToString("yyyy-MM-dd") + "','" + narration + cashNarration +
                                "'," + Convert.ToString(dtVou.Rows[i]["Amount"]) + ",'" + series + "','" + Convert.ToString(dtVou.Rows[i]["ReceievedBy"]) + "',1," + choosenDate.Day + "," + choosenDate.Month + "," +
                                choosenDate.Year + "," + memberId + "," + trans_medium + ", " + RootID + "," + chitGrpId + ",0,'" + appReceiptno + "',0,0," + dtVou.Rows[i]["MoneyCollId"] + ",'" + objSession.LoginIp + "');";

                                long insert = trn.insertorupdateTrn(query);
                                long insert1 = trn.insertorupdateTrn(query1);

                            }

                            else if (Convert.ToString(branch_id) != balayer.TostrEvenNull(Convert.ToString(Session["Branchid"])))
                            {

                                //string query = "INSERT INTO `svcf`.`voucher` (`DualTransactionKey`,`BranchID`,`CurrDate`,`Voucher_No`,`Voucher_Type`,`Head_Id`,`ChoosenDate`,`Narration`," +
                                //"`Amount`,`Series`,`ReceievedBy`,`Trans_Type`,`T_Day`,`T_Month`,`T_Year`,`MemberID`,`Trans_Medium`,`RootID`,`ChitGroupId`,`Other_Trans_Type`,`AppReceiptno`,`IsDeleted`,`IsAccepted`,`M_Id`,`LoginIP`) VALUES ('" + DualTransactionKey + "'," +
                                //balayer.TostrEvenNull(Convert.ToString(Session["Branchid"])) + ",'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," + voucherNo + ",'C'," + branchId + ",'" + choosenDate.ToString("yyyy-MM-dd") + "','" + "Recd From:- - :-:" + appReceiptno + "-" + membername + ":" + TokenNo +
                                //"'," + Convert.ToString(dtVou.Rows[i]["Amount"]) + ",'" + series + "','" + Convert.ToString(dtVou.Rows[i]["ReceievedBy"]) + "',1," + choosenDate.Day + "," + choosenDate.Month + "," +
                                //choosenDate.Year + "," + memberId + "," + trans_medium + ",1," + chitGrpId + ",1,'" + appReceiptno + "',0,0," + dtVou.Rows[i]["MoneyCollId"] + ",'" + objSession.LoginIp + "');";
                                string query = "INSERT INTO `svcf`.`voucher` (`DualTransactionKey`,`BranchID`,`CurrDate`,`Voucher_No`,`Voucher_Type`,`Head_Id`,`ChoosenDate`,`Narration`," +
                                "`Amount`,`Series`,`ReceievedBy`,`Trans_Type`,`T_Day`,`T_Month`,`T_Year`,`MemberID`,`Trans_Medium`,`RootID`,`ChitGroupId`,`Other_Trans_Type`,`AppReceiptno`,`IsDeleted`,`IsAccepted`,`M_Id`,`LoginIP`) VALUES (" + DualTransactionKey + "," +
                                balayer.TostrEvenNull(Convert.ToString(Session["Branchid"])) + ",'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," + voucherNo + ",'C'," + Convert.ToString(branch_id) + ",'" + choosenDate.ToString("yyyy-MM-dd") + "','" + "Recd From:- - :-:" + appReceiptno + "-" + membername + ":" + TokenNo + ":For Inst No:" + cashNarration +
                                "'," + Convert.ToString(dtVou.Rows[i]["Amount"]) + ",'" + series + "','" + Convert.ToString(dtVou.Rows[i]["ReceievedBy"]) + "',1," + choosenDate.Day + "," + choosenDate.Month + "," +
                                choosenDate.Year + "," + memberId + "," + trans_medium + ",1," + chitGrpId + ",1,'" + appReceiptno + "',0,0," + dtVou.Rows[i]["MoneyCollId"] + ",'" + objSession.LoginIp + "');";

                                string query1 = "INSERT INTO `svcf`.`voucher` (`DualTransactionKey`,`BranchID`,`CurrDate`,`Voucher_No`,`Voucher_Type`,`Head_Id`,`ChoosenDate`,`Narration`," +
                                "`Amount`,`Series`,`ReceievedBy`,`Trans_Type`,`T_Day`,`T_Month`,`T_Year`,`MemberID`,`Trans_Medium`,`RootID`,`ChitGroupId`,`Other_Trans_Type`,`AppReceiptno`,`IsDeleted`,`IsAccepted`,`M_Id`,`LoginIP`) VALUES (" + DualTransactionKey + "," +
                                balayer.TostrEvenNull(Convert.ToString(Session["Branchid"])) + ",'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," + voucherNo + ",'D', '12','" + choosenDate.ToString("yyyy-MM-dd") + "','" + "Recd From:- - :-:" + appReceiptno + "-" + membername + ":" + TokenNo + " :For Inst No:" + cashNarration +
                                "'," + Convert.ToString(dtVou.Rows[i]["Amount"]) + ",'" + series + "','" + Convert.ToString(dtVou.Rows[i]["ReceievedBy"]) + "',1," + choosenDate.Day + "," + choosenDate.Month + "," +
                                choosenDate.Year + "," + memberId + "," + trans_medium + ", " + RootID + "," + chitGrpId + ",1,'" + appReceiptno + "',0,0," + dtVou.Rows[i]["MoneyCollId"] + ",'" + objSession.LoginIp + "');";

                                long insert = trn.insertorupdateTrn(query);
                                long insert1 = trn.insertorupdateTrn(query1);

                            }
                        }
                        catch (Exception ex)
                        {
                            throw;
                        }
                    }
                    else if (type == "DefaultInterest")
                    {
                        string Narrat = "";
                        if (Convert.ToString(branch_id) == balayer.TostrEvenNull(Convert.ToString(Session["Branchid"])))
                        {
                            Narrat = intNarration;
                            try
                            {
                                string qry1 = "INSERT INTO `svcf`.`voucher` (`DualTransactionKey`,`BranchID`,`CurrDate`,`Voucher_No`,`Voucher_Type`,`Head_Id`,`ChoosenDate`,`Narration`," +
                                    "`Amount`,`Series`,`ReceievedBy`,`Trans_Type`,`T_Day`,`T_Month`,`T_Year`,`MemberID`,`Trans_Medium`,`RootID`,`ChitGroupId`,`Other_Trans_Type`,`AppReceiptno`,`IsDeleted`,`IsAccepted`,`M_Id`,`LoginIP`) VALUES (" + DualTransactionKey + "," +
                                    balayer.TostrEvenNull(Convert.ToString(Session["Branchid"])) + ",'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," + voucherNo + ",'C', 1131147,'" + choosenDate.ToString("yyyy-MM-dd") + "','" + Narrat +
                                    "'," + Convert.ToString(dtVou.Rows[i]["Amount"]) + ",'" + series + "','" + Convert.ToString(dtVou.Rows[i]["ReceievedBy"]) + "',1," + choosenDate.Day + "," + choosenDate.Month + "," +
                                    choosenDate.Year + "," + memberId + "," + trans_medium + ",11," + chitGrpId + ",0,'" + appReceiptno + "',0,0," + dtVou.Rows[i]["MoneyCollId"] + ",'" + objSession.LoginIp + "');";

                                string qry2 = "INSERT INTO `svcf`.`voucher` (`DualTransactionKey`,`BranchID`,`CurrDate`,`Voucher_No`,`Voucher_Type`,`Head_Id`,`ChoosenDate`,`Narration`," +
                                "`Amount`,`Series`,`ReceievedBy`,`Trans_Type`,`T_Day`,`T_Month`,`T_Year`,`MemberID`,`Trans_Medium`,`RootID`,`ChitGroupId`,`Other_Trans_Type`,`AppReceiptno`,`IsDeleted`,`IsAccepted`,`M_Id`,`LoginIP`) VALUES (" + DualTransactionKey + "," +
                                balayer.TostrEvenNull(Convert.ToString(Session["Branchid"])) + ",'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," + voucherNo + ",'D', '12','" + choosenDate.ToString("yyyy-MM-dd") + "','" + Narrat +
                                "'," + Convert.ToString(dtVou.Rows[i]["Amount"]) + ",'" + series + "','" + Convert.ToString(dtVou.Rows[i]["ReceievedBy"]) + "',1," + choosenDate.Day + "," + choosenDate.Month + "," +
                                choosenDate.Year + "," + memberId + "," + trans_medium + ", 12, " + chitGrpId + ",0,'" + appReceiptno + "',0,0," + dtVou.Rows[i]["MoneyCollId"] + ",'" + objSession.LoginIp + "');";

                                long insert = trn.insertorupdateTrn(qry1);
                                long insert1 = trn.insertorupdateTrn(qry2);
                            }
                            catch (Exception ex)
                            {
                                throw;
                            }
                        }
                        else
                        {
                            Narrat = "Misc :-" + intNarration;

                            try
                            {
                                string qry1 = "INSERT INTO `svcf`.`voucher` (`DualTransactionKey`,`BranchID`,`CurrDate`,`Voucher_No`,`Voucher_Type`,`Head_Id`,`ChoosenDate`,`Narration`," +
                                    "`Amount`,`Series`,`ReceievedBy`,`Trans_Type`,`T_Day`,`T_Month`,`T_Year`,`MemberID`,`Trans_Medium`,`RootID`,`ChitGroupId`,`Other_Trans_Type`,`AppReceiptno`,`IsDeleted`,`IsAccepted`,`M_Id`,`LoginIP`) VALUES (" + DualTransactionKey + "," +
                                    balayer.TostrEvenNull(Convert.ToString(Session["Branchid"])) + ",'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," + voucherNo + ",'C', 1131147,'" + choosenDate.ToString("yyyy-MM-dd") + "','" + Narrat +
                                    "'," + Convert.ToString(dtVou.Rows[i]["Amount"]) + ",'" + series + "','" + Convert.ToString(dtVou.Rows[i]["ReceievedBy"]) + "',1," + choosenDate.Day + "," + choosenDate.Month + "," +
                                    choosenDate.Year + "," + memberId + "," + trans_medium + ",11," + chitGrpId + ",1,'" + appReceiptno + "',0,0," + dtVou.Rows[i]["MoneyCollId"] + ",'" + objSession.LoginIp + "');";

                                string qry2 = "INSERT INTO `svcf`.`voucher` (`DualTransactionKey`,`BranchID`,`CurrDate`,`Voucher_No`,`Voucher_Type`,`Head_Id`,`ChoosenDate`,`Narration`," +
                                "`Amount`,`Series`,`ReceievedBy`,`Trans_Type`,`T_Day`,`T_Month`,`T_Year`,`MemberID`,`Trans_Medium`,`RootID`,`ChitGroupId`,`Other_Trans_Type`,`AppReceiptno`,`IsDeleted`,`IsAccepted`,`M_Id`,`LoginIP`) VALUES (" + DualTransactionKey + "," +
                                balayer.TostrEvenNull(Convert.ToString(Session["Branchid"])) + ",'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," + voucherNo + ",'D', '12','" + choosenDate.ToString("yyyy-MM-dd") + "','" + Narrat +
                                "'," + Convert.ToString(dtVou.Rows[i]["Amount"]) + ",'" + series + "','" + Convert.ToString(dtVou.Rows[i]["ReceievedBy"]) + "',1," + choosenDate.Day + "," + choosenDate.Month + "," +
                                choosenDate.Year + "," + memberId + "," + trans_medium + ", 12, " + chitGrpId + ",1,'" + appReceiptno + "',0,0," + dtVou.Rows[i]["MoneyCollId"] + ",'" + objSession.LoginIp + "');";

                                long insert = trn.insertorupdateTrn(qry1);
                                long insert1 = trn.insertorupdateTrn(qry2);
                            }
                            catch (Exception ex)
                            {
                                throw;
                            }
                        }
                    }

                }
                //26/12/2020
                trn.insertorupdateTrn("update mobileappvoucher set IsAccepted=" + true + " ,ModifiedDate='" + balayer.changedateformat(DateTime.Now, 1) + "' where AppReceiptno='" + appReceiptno + "' and Head_Id=" + Head_Id + ";");
                lblWarning.Text = "Approved Successfully!!";
                lblWarning.ForeColor = System.Drawing.Color.Green;
                ModalPopupExtender1.PopupControlID = "warning";
                warning.Visible = true;
                ModalPopupExtender1.Show();

                trn.CommitTrn();
            }
            catch (Exception ex)
            {
                try
                {
                    trn.RollbackTrn();
                }
                catch (Exception err)
                {
                    logger.Info("AppVoucherMoneyCollector.aspx - btnConfirm_Click() - Error: " + err.Message + ": " + DateTime.Now + " by: " + Convert.ToString(Session["UserName"]) + "");
                }
                finally
                {
                    lblWarning.Text = "Approval Unsuccess!\n" + ex.Message;
                    lblWarning.ForeColor = System.Drawing.Color.Red;
                    ModalPopupExtender1.PopupControlID = "warning";
                    warning.Visible = true;
                    ModalPopupExtender1.Show();
                }
            }
            finally
            {
                trn.DisposeTrn();
            }

        }

        protected void btnRejectConfirm_Click(object sender, EventArgs e)
        {
            try
            {

                GridViewRow row = (GridViewRow)Session["Row"];
                string transactionkey = balayer.ToobjectstrEvenNull(Gridview1.DataKeys[row.RowIndex]["TransactionKey"]);
                string appReceiptno = balayer.ToobjectstrEvenNull(Gridview1.DataKeys[row.RowIndex]["AppReceiptno"]);
                var qry = "Update svcf.mobileappvoucher set IsAccepted=2,RejectReason='" + txtReason.Text + "' where AppReceiptno='" + appReceiptno + "';";
                trn.insertorupdateTrn(qry);

                //09/08/2021 - Access denied to cancelled receipt from mobile app server
                var qry1 = "Update svcf.receiptprint_pdf set Status=1 where AppReceiptno='" + appReceiptno + "';";
                trn.insertorupdateTrn(qry1);

                lblWarning.Text = "Receipt Rejected Successfully!";
                lblWarning.ForeColor = System.Drawing.Color.Green;
                ModalPopupExtender1.PopupControlID = "warning";
                warning.Visible = true;
                ModalPopupExtender1.Show();
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
                    ModalPopupExtender1.PopupControlID = "warning";
                    warning.Visible = true;
                    ModalPopupExtender1.Show();
                }
            }
            finally
            {
                trn.DisposeTrn();
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            //Response.Redirect(Request.Url.AbsolutePath.ToString(), false);
            ModalPopupExtender1.Hide();
            DataTable dt = getReceipts();
            Gridview1.Visible = true;
            Gridview1.DataSource = dt;
            Gridview1.DataBind();
            decimal sum = 0;
            for (int i = 0; i < Gridview1.Rows.Count; i++)
            {
                sum += Convert.ToDecimal(Gridview1.Rows[i].Cells[5].Text);
            }
            lblTotal.Visible = true;
            lblTotalAmt.Visible = true;
            lblTotalAmt.Text = sum.ToString();
        }

        protected void btnOk_Click(object sender, EventArgs e)
        {
            ModalPopupExtender1.Hide();
            //Response.Redirect(Request.Url.AbsolutePath.ToString(), false);
            DataTable dt = getReceipts();
            Gridview1.Visible = true;
            Gridview1.DataSource = dt;
            Gridview1.DataBind();
            decimal sum = 0;
            for (int i = 0; i < Gridview1.Rows.Count; i++)
            {
                sum += Convert.ToDecimal(Gridview1.Rows[i].Cells[5].Text);
            }
            lblTotal.Visible = true;
            lblTotalAmt.Visible = true;
            lblTotalAmt.Text = sum.ToString();
        }

        //protected void ddlMoneyColl_SelectedIndexChanged(object sender, EventArgs e)

        //{
        //    int moneycoll = Convert.ToInt16(ddlMoneyColl.SelectedValue);
        //    if (RadApprove.Checked == true)
        //    {
        //        DataTable dt = getReceipts(moneycoll);
        //        Gridview1.Visible = true;
        //        Gridview1.DataSource = dt;
        //        Gridview1.DataBind();
        //        Appvoucherapproval.Visible = true;
        //        ViewApproved.Visible = false;
        //    }
        //    else if (RadView.Checked == true)
        //    {
        //        ViewApproved.Visible = true;
        //        Appvoucherapproval.Visible = false;
        //        string moneyCollId = ddlMoneyColl.SelectedValue;
        //        string series = "BCAPP";
        //        approvalview.LoadData(series, moneyCollId);

        //    }
        //}

        protected void btnSelect_Click(object sender, EventArgs e)
        {
            userinfo = HttpContext.Current.User.Identity.Name;
            qry = "select rs.name from roles as rs inner join rights as rt on (rt.roleid=rs.id) where memberid=" + userinfo + "";
            usrRole = balayer.GetSingleValue(qry);

            if (RadApprove.Checked == true)
            {
                DataTable dtReceipts = getReceipts();
                Gridview1.Visible = true;
                Gridview1.DataSource = dtReceipts;
                Gridview1.DataBind();
                if (usrRole == "Administrator")
                {
                    Gridview1.Columns[1].Visible = true;
                }
                else
                {
                    Gridview1.Columns[1].Visible = false;
                }
                decimal sum = 0;
                for (int i = 0; i < Gridview1.Rows.Count; i++)
                {
                    sum += Convert.ToDecimal(Gridview1.Rows[i].Cells[5].Text);
                }
                lblTotal.Visible = true;
                lblTotalAmt.Visible = true;
                lblTotalAmt.Text = sum.ToString();
                Appvoucherapproval.Visible = true;
                ViewApproved.Visible = false;
                imgexport.Visible = false;
            }
            else if (RadView.Checked == true)
            {
                ViewApproved.Visible = true;
                Appvoucherapproval.Visible = false;
                imgexport.Visible = true;
                string moneyCollId = ddlMoneyColl.SelectedValue;
                approvalview.LoadData(moneyCollId, txtFrom.Text, txtTo.Text);
            }
        }

        protected void imgexport_Click(object sender, ImageClickEventArgs e)
        {

            DataTable dt = approvalview.loadGridExport();

            Workbook workbook = new Workbook();
            workbook.CreateEmptySheets(1);
            Worksheet sheet = workbook.Worksheets[0];

            ExcelFont fontbold = workbook.CreateFont();
            fontbold.IsBold = true;

            sheet.Name = "ApprovedReceipts";
            string branchname = "";

            branchname = balayer.GetSingleValue("select Node from svcf.headstree where ParentID=1 and NodeID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]));

            sheet.Range["E1"].Value = "Sree Visalam Chit Fund Ltd.,";
            RichText richText1 = sheet.Range["E1"].RichText;
            richText1.SetFont(0, richText1.Text.Length - 1, fontbold);
            var bb = 2;
            if (branchname == "Triplicane")
            {

                sheet.Range["E" + bb + ""].Value = "Branch: " + "Mount Road";
                RichText richText02 = sheet.Range["E" + bb + ""].RichText;
                richText02.SetFont(0, richText02.Text.Length - 1, fontbold);

            }
            else if (branchname == "Pallathur II")
            {
                sheet.Range["E" + bb + ""].Value = "Branch: " + "Pallathur";
                RichText richText02 = sheet.Range["E" + bb + ""].RichText;
                richText02.SetFont(0, richText02.Text.Length - 1, fontbold);
            }
            else
            {
                sheet.Range["E" + bb + ""].Value = "Branch: " + branchname;
                RichText richText02 = sheet.Range["E" + bb + ""].RichText;
                richText02.SetFont(0, richText02.Text.Length - 1, fontbold);
            }

            sheet.Range["E3"].Value = "Approved Receipts";
            RichText richText03 = sheet.Range["E3"].RichText;
            richText03.SetFont(0, richText03.Text.Length - 1, fontbold);

            sheet.Range["E4"].Value = "Bill Collector Name : " + ddlMoneyColl.SelectedItem.Text;
            RichText richText04 = sheet.Range["E4"].RichText;
            richText04.SetFont(0, richText04.Text.Length - 1, fontbold);

            sheet.Range["A6"].Value = "SNo.";
            RichText richText06 = sheet.Range["A6"].RichText;
            richText06.SetFont(0, richText06.Text.Length - 1, fontbold);
            sheet.Range["A6"].Style.HorizontalAlignment = HorizontalAlignType.Center;
            sheet.Range["A6"].Style.VerticalAlignment = VerticalAlignType.Center;

            sheet.Range["B6"].Value2 = "Branch Name";
            RichText richText07 = sheet.Range["B6"].RichText;
            richText07.SetFont(0, richText07.Text.Length - 1, fontbold);
            sheet.Range["B6"].Style.HorizontalAlignment = HorizontalAlignType.Center;
            sheet.Range["B6"].Style.VerticalAlignment = VerticalAlignType.Center;

            //sheet.Range["C6"].Value2 = "Approved Date";
            //RichText richText08 = sheet.Range["C6"].RichText;
            //richText08.SetFont(0, richText08.Text.Length - 1, fontbold);
            //sheet.Range["C6"].Style.HorizontalAlignment = HorizontalAlignType.Center;
            //sheet.Range["C6"].Style.VerticalAlignment = VerticalAlignType.Center;

            sheet.Range["C6"].Value2 = "Date of Remitted By Subscriber";
            RichText richText08 = sheet.Range["C6"].RichText;
            richText08.SetFont(0, richText08.Text.Length - 1, fontbold);
            sheet.Range["C6"].Style.HorizontalAlignment = HorizontalAlignType.Center;
            sheet.Range["C6"].Style.VerticalAlignment = VerticalAlignType.Center;
            sheet.Range["C6"].Style.WrapText = true;

            sheet.Range["D6"].Value2 = "Chit Number";
            RichText richText09 = sheet.Range["D6"].RichText;
            richText09.SetFont(0, richText09.Text.Length - 1, fontbold);
            sheet.Range["D6"].Style.HorizontalAlignment = HorizontalAlignType.Center;
            sheet.Range["D6"].Style.VerticalAlignment = VerticalAlignType.Center;

            sheet.Range["E6"].Value2 = "Receipt Number";
            RichText richText10 = sheet.Range["E6"].RichText;
            richText10.SetFont(0, richText10.Text.Length - 1, fontbold);
            sheet.Range["E6"].Style.HorizontalAlignment = HorizontalAlignType.Center;
            sheet.Range["E6"].Style.VerticalAlignment = VerticalAlignType.Center;

            sheet.Range["F6"].Value2 = "Instalment No";
            RichText richText11 = sheet.Range["F6"].RichText;
            richText11.SetFont(0, richText11.Text.Length - 1, fontbold);
            sheet.Range["F6"].Style.HorizontalAlignment = HorizontalAlignType.Center;
            sheet.Range["F6"].Style.VerticalAlignment = VerticalAlignType.Center;

            sheet.Range["G6"].Value2 = "Current Branch Amount";
            RichText richText12 = sheet.Range["G6"].RichText;
            richText12.SetFont(0, richText12.Text.Length - 1, fontbold);
            sheet.Range["G6"].Style.HorizontalAlignment = HorizontalAlignType.Center;
            sheet.Range["G6"].Style.VerticalAlignment = VerticalAlignType.Center;

            sheet.Range["H6"].Value2 = "Other Branch Amount";
            RichText richText13 = sheet.Range["H6"].RichText;
            richText13.SetFont(0, richText13.Text.Length - 1, fontbold);
            sheet.Range["H6"].Style.HorizontalAlignment = HorizontalAlignType.Center;
            sheet.Range["H6"].Style.VerticalAlignment = VerticalAlignType.Center;

            sheet.Range["I6"].Value2 = "P&L";
            RichText richText14 = sheet.Range["I6"].RichText;
            richText14.SetFont(0, richText14.Text.Length - 1, fontbold);
            sheet.Range["I6"].Style.HorizontalAlignment = HorizontalAlignType.Center;
            sheet.Range["I6"].Style.VerticalAlignment = VerticalAlignType.Center;

            sheet.Range["J6"].Value2 = "Due Date";
            RichText richText15 = sheet.Range["J6"].RichText;
            richText15.SetFont(0, richText15.Text.Length - 1, fontbold);
            sheet.Range["J6"].Style.HorizontalAlignment = HorizontalAlignType.Center;
            sheet.Range["J6"].Style.VerticalAlignment = VerticalAlignType.Center;

            int rowcount = 6;
            int sno = 1;
            decimal amt = 0; decimal amt1 = 0; decimal amt2 = 0;
            foreach (DataRow row in dt.Rows)
            {
                rowcount = rowcount + 1;

                sheet.Range["A" + rowcount].Value = sno.ToString();
                sheet.Range["B" + rowcount].Value = row.ItemArray[7].ToString();
                //sheet.Range["C" + rowcount].Value = row.ItemArray[8].ToString();  06/12/2021
                //sheet.Range["D" + rowcount].Value = row.ItemArray[8].ToString();
                sheet.Range["C" + rowcount].DateTimeValue = (Convert.ToDateTime(row.ItemArray[9]));
                sheet.Range["C" + rowcount].NumberFormat = "dd-MM-yyyy hh:mm:ss";
                sheet.Range["D" + rowcount].Value = row.ItemArray[5].ToString();
                sheet.Range["E" + rowcount].Value = row.ItemArray[0].ToString();
                //06/12/2021
                string narration = row.ItemArray[12].ToString();
                string[] narrat = narration.Split(':');
                var instNo = "";
                if (narrat.Length == 5)
                    instNo = narrat[4];
                else if (narrat.Length == 6)
                    instNo = narrat[5];
                else if (narrat.Length == 7)
                    instNo = narrat[6];

                sheet.Range["F" + rowcount].Value = instNo;
                sheet.Range["G" + rowcount].NumberValue = Convert.ToDouble(row.ItemArray[10]);
                sheet.Range["G" + rowcount].NumberFormat = "#,##0.00";
                amt += Convert.ToDecimal(row.ItemArray[10]);
                sheet.Range["H" + rowcount].NumberValue = Convert.ToDouble(row.ItemArray[11]);
                sheet.Range["H" + rowcount].NumberFormat = "#,##0.00";
                amt1 += Convert.ToDecimal(row.ItemArray[11]);
                sheet.Range["I" + rowcount].NumberValue = Convert.ToDouble(row.ItemArray[13]);
                sheet.Range["I" + rowcount].NumberFormat = "#,##0.00";
                amt2 += Convert.ToDecimal(row.ItemArray[13]);
                //sheet.Range["H" + rowcount].Value = row.ItemArray[10].ToString();
                sheet.Range["J" + rowcount].Value = "";

                sno++;
            }

            rowcount = rowcount + 1;

            sheet.Range["F" + rowcount].Value = "Total";
            RichText richText16 = sheet.Range["F" + rowcount].RichText;
            richText16.SetFont(0, richText16.Text.Length - 1, fontbold);
            sheet.Range["F" + rowcount].Style.HorizontalAlignment = HorizontalAlignType.Center;
            sheet.Range["F" + rowcount].Style.VerticalAlignment = VerticalAlignType.Center;

            sheet.Range["G" + rowcount].NumberValue = Convert.ToDouble(amt);
            sheet.Range["G" + rowcount].NumberFormat = "#,##0.00";
            //RichText richText15 = sheet.Range["G" + rowcount].RichText;
            //richText15.SetFont(0, richText15.Text.Length - 1, fontbold);
            sheet.Range["G" + rowcount].Style.HorizontalAlignment = HorizontalAlignType.Center;
            sheet.Range["G" + rowcount].Style.VerticalAlignment = VerticalAlignType.Center;

            sheet.Range["H" + rowcount].NumberValue = Convert.ToDouble(amt1);
            sheet.Range["H" + rowcount].NumberFormat = "#,##0.00";
            sheet.Range["H" + rowcount].Style.HorizontalAlignment = HorizontalAlignType.Center;
            sheet.Range["H" + rowcount].Style.VerticalAlignment = VerticalAlignType.Center;

            sheet.Range["I" + rowcount].NumberValue = Convert.ToDouble(amt2);
            sheet.Range["I" + rowcount].NumberFormat = "#,##0.00";
            sheet.Range["I" + rowcount].Style.HorizontalAlignment = HorizontalAlignType.Center;
            sheet.Range["I" + rowcount].Style.VerticalAlignment = VerticalAlignType.Center;

            CellRange range21 = sheet.Range["A6:" + "J" + rowcount];
            range21.Borders.LineStyle = LineStyleType.Thin;
            range21.Borders[BordersLineType.DiagonalDown].LineStyle = LineStyleType.None;
            range21.Borders[BordersLineType.DiagonalUp].LineStyle = LineStyleType.None;

            rowcount = rowcount + 2;

            sheet.AllocatedRange.AutoFitColumns();
            sheet.AllocatedRange.AutoFitRows();

            sheet.SetRowHeight(6, 38);

            sheet.SetColumnWidth(1, 8);
            sheet.SetColumnWidth(2, 17);
            sheet.SetColumnWidth(3, 20);
            sheet.SetColumnWidth(4, 10);
            sheet.SetColumnWidth(5, 20);
            sheet.SetColumnWidth(6, 15);
            sheet.SetColumnWidth(7, 12);
            sheet.SetColumnWidth(8, 12);
            sheet.SetColumnWidth(9, 12);
            sheet.SetColumnWidth(10, 15);


            workbook.SaveToHttpResponse("ApprovedReceipts.xlsx", HttpContext.Current.Response);
        }
    }
}