using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using System.Text.RegularExpressions;
using SVCF_BusinessAccessLayer;
using SVCF_TransactionLayer;


namespace SreeVisalamChitFundLtd_phase1
{
    public partial class AppVoucherApproval : System.Web.UI.Page
    {
        #region VarDeclaration
        BusinessLayer balayer = new BusinessLayer();
        TransactionLayer tranlayer = new TransactionLayer();
        int hdid = 0;
        #endregion

        protected void Page_PreRender(object sender, EventArgs e)
        {
            ViewState["CheckRefresh"] = Session["CheckRefresh"];
        }
        public DataTable GetData()
        {
            //return balayer.GetDataTable("SELECT t1.TransactionKey,t3.B_Name as BranchName, date_format( t1.ChoosenDate,'%d/%m/%Y') as ChoosenDate,g1.GROUPNO,m1.Node as GrpMemberID,t1.Series,t1.Head_Id,t1.Voucher_No as ReceiptNumber,t2.CustomerName,t1.Amount,t1.Narration as Description,insertkey_from_bin(t1.DualTransactionKey) as DualTransactionKey,t1.BranchID as CollectedBranchID,t1.MemberID,t1.ChitGroupID, get_ref_no(t1.Narration) as Token,t1.AppReceiptno as 'AppReceiptno' FROM `svcf`.`voucher` as t1 left join membermaster as t2  on  t1.MemberID=t2.MemberIDNew join branchdetails as t3 on (t3.Head_Id=t1.BranchID) left join headstree as m1 on (m1.NodeID=t1.Head_Id) left join groupmaster as g1 on (t1.ChitGroupId=g1.Head_Id) where t1.Trans_Type=1 and t1.ISActive=1 and AppReceiptno <> '' and Voucher_Type='C' and t1.Branchid=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + "");
            return balayer.GetDataTable("SELECT t1.TransactionKey,t3.B_Name as BranchName, date_format( t1.ChoosenDate,'%d/%m/%Y') as ChoosenDate,t1.Voucher_Type,t4.CustomersBankName,t4.DateInCheque,t4.ChequeDDNO,g1.GROUPNO,m1.Node as GrpMemberID,t1.Series,t1.Head_Id,t1.Voucher_No as ReceiptNumber,t2.CustomerName,t1.Amount,t1.Narration as Description,insertkey_from_bin(t1.DualTransactionKey) as DualTransactionKey,t1.BranchID as CollectedBranchID,t1.MemberID,t1.ChitGroupID, get_ref_no(t1.Narration) as Token,t1.AppReceiptno as 'AppReceiptno' FROM `svcf`.`voucher` as t1 left join membermaster as t2  on  t1.MemberID=t2.MemberIDNew join branchdetails as t3 on (t3.Head_Id=t1.BranchID) left join headstree as m1 on (m1.NodeID=t1.Head_Id) left join groupmaster as g1 on (t1.ChitGroupId=g1.Head_Id) join transbank as t4 on (t1.DualTransactionKey=t4.DualTransactionKey) where t1.Trans_Type=1 and t1.ISActive=1 and t1.Voucher_Type='C' and t1.AppReceiptno <> '' and t4.ISActive=1 and t1.Branchid=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + ""); 
        
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataTable dtAcc = GetData();
                Gridview1.DataSource = dtAcc;
                Gridview1.DataBind();
                RadApprove.Checked = true;
                Appvoucherapproval.Visible = true;
            }
            rvDate.MaximumValue = DateTime.Now.ToString("dd/MM/yyyy");
            //rvDate.MinimumValue = balayer.GetSingleValue("SELECT DATE_FORMAT( MinimumDate, '%d/%m/%Y') MinimumDate FROM svcf.restrictionmaster where BranchID=" + Session["Branchid"]); ;
            rvDate.MinimumValue = balayer.GetSingleValue("SELECT DATE_FORMAT( min(ChoosenDate), '%d/%m/%Y') MinimumDate FROM svcf.voucher where " +
                "BranchID=" + Session["Branchid"] + " and ChoosenDate<>'0000-00-00'");

        }
        protected void Approve_Click(object sender, EventArgs e)
        {
            if (Convert.ToString(Session["CheckRefresh"]) != (Convert.ToString(ViewState["CheckRefresh"]))) 
            {
                return;
            }
            bool isFinished = true;

            //Transcation trn = new Transcation(true);
            try
            {
                string chittkn = "";

                ImageButton btndetails = sender as ImageButton;
                GridViewRow gvRow = (GridViewRow)btndetails.NamingContainer;
                string Series = balayer.ToobjectstrEvenNull(Gridview1.DataKeys[gvRow.RowIndex]["Series"]);
                string Voucher_No = balayer.ToobjectstrEvenNull(Gridview1.DataKeys[gvRow.RowIndex]["ReceiptNumber"]);
                string GrpID = balayer.ToobjectstrEvenNull(Gridview1.DataKeys[gvRow.RowIndex]["ChitGroupID"]);
                string GrpMemberID = balayer.ToobjectstrEvenNull(Gridview1.DataKeys[gvRow.RowIndex]["Token"]);
                string MemberID = balayer.ToobjectstrEvenNull(Gridview1.DataKeys[gvRow.RowIndex]["MemberID"]);
                string Amount = balayer.ToobjectstrEvenNull(Gridview1.DataKeys[gvRow.RowIndex]["Amount"]);
                string BranchHead = balayer.ToobjectstrEvenNull(Gridview1.DataKeys[gvRow.RowIndex]["CollectedBranchID"]);
                string DualTransactionKey = balayer.ToobjectstrEvenNull(Gridview1.DataKeys[gvRow.RowIndex]["DualTransactionKey"]);
                string Narration = balayer.ToobjectstrEvenNull(Gridview1.DataKeys[gvRow.RowIndex]["Description"]);
                DateTime dtChoosenDate = DateTime.Parse(balayer.ToobjectstrEvenNull(Gridview1.DataKeys[gvRow.RowIndex]["ChoosenDate"]));
                string TransactionKey = balayer.ToobjectstrEvenNull(Gridview1.DataKeys[gvRow.RowIndex]["TransactionKey"]);
                string AppReceiptno = balayer.ToobjectstrEvenNull(Gridview1.DataKeys[gvRow.RowIndex]["AppReceiptno"]);

                string[] chitToken = Narration.Split(':');

                lblGroupID.Text = GrpID;
                if (Regex.IsMatch(GrpMemberID, "^(?=.*[a-zA-Z])"))
                {
                    GrpMemberID = "0";
                }



                lbldual.Text = DualTransactionKey;
                lblchoosendate.Text = dtChoosenDate.ToString();
                lblapprec.Text = AppReceiptno;
                lblSeries.Text = Series;
                lblVoucher.Text = Voucher_No;
                //lblDebitID.Text = balayer.ToobjectstrEvenNull(Gridview1.DataKeys[gvRow.RowIndex]["CollectedBranchID"]);
                //lblAmount.Text = Amount;
                //lblDual.Text = TransactionKey;
                //DataTable dtAllHeads = balayer.GetDataTable("SELECT concat(cast(  v1.`RootID` as char),',',cast(v1.`TreeID` as char)) as ID,v1.`TREE` FROM `svcf`.`view_parent` as v1  where v1.BranchID is null or v1.BranchID=" + Session["Branchid"]);
                //DataRow dr = dtAllHeads.NewRow();
                //dr[0] = "--Select--";
                //dr[1] = "--Select--";
                //dtAllHeads.Rows.InsertAt(dr, 0);
                //ddlCredit.DataSource = dtAllHeads;
                //ddlCredit.DataTextField = "TREE";
                //ddlCredit.DataValueField = "ID";
                //ddlCredit.DataBind();
                //if (Narration.Contains(":"))
                //{
                //    chittkn = Narration.Split(':')[0].Trim();
                //}
                //hdid = balayer.GetScalarDataInt("select Head_Id from membertogroupmaster where GrpMemberID='" + chittkn + "'");
                //string strChitHeadQuery = "INSERT INTO `voucher` (`DualTransactionKey`,`BranchID`,`Current_Date`,`Voucher_No`,`Voucher_Type`,`Head_Id`,`ChoosenDate`,`Narration`,`Amount`,`Series`,`ReceievedBy`,`Trans_Type`,`T_Day`,`T_Month`,`T_Year`,`MemberID`,`Trans_Medium`,`RootID`,`ChitGroupId`,Other_Trans_Type) VALUES (" + DualTransactionKey + "," + balayer.ToobjectstrEvenNull(Session["Branchid"]) + ",'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," + Voucher_No + ",'D'," + BranchHead + ",'" + dtChoosenDate.ToString("yyyy-MM-dd") + "','" + Narration + "'," + Amount + ",'" + Series + "','" + balayer.ToobjectstrEvenNull(Session["UserName"]) + "',1," + dtChoosenDate.Day + "," + dtChoosenDate.Month + "," + dtChoosenDate.Year + "," + MemberID + "," + "2" + ",1," + GrpID + ",2)";
                //string strCashHeadQuery = "INSERT INTO `voucher` (`DualTransactionKey`,`BranchID`,`Current_Date`,`Voucher_No`,`Voucher_Type`,`Head_Id`,`ChoosenDate`,`Narration`,`Amount`,`Series`,`ReceievedBy`,`Trans_Type`,`T_Day`,`T_Month`,`T_Year`,`MemberID`,`Trans_Medium`,`RootID`,`ChitGroupId`,Other_Trans_Type) VALUES (" + DualTransactionKey + "," + balayer.ToobjectstrEvenNull(Session["Branchid"]) + ",'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," + Voucher_No + ",'C'," + GrpMemberID + ",'" + dtChoosenDate.ToString("yyyy-MM-dd") + "','" + Narration + "'," + Amount + ",'" + Series + "','" + balayer.ToobjectstrEvenNull(Session["UserName"]) + "',1," + dtChoosenDate.Day + "," + dtChoosenDate.Month + "," + dtChoosenDate.Year + "," + MemberID + "," + "2" + "," + parentID + "," + GrpID + ",2)";
                //string strUpdateHeadQuery = "update  `voucher` set Other_Trans_Type=2 where DualTransactionKey=" + DualTransactionKey;
                //long iResultDebit= trn.insertorupdate(strChitHeadQuery);
                //long iResultCredit=trn.insertorupdate(strCashHeadQuery);
                //long uResult=trn.insertorupdate(strUpdateHeadQuery);
                tranlayer.CommitTrn();
                fillBankHead();
                //trn.Commit();
                ////GrpMemberID
            }
            catch
            {
                try
                {
                    tranlayer.RollbackTrn();
                }
                catch { }
                finally
                {
                    //lblT.Text = "Status";s
                    //lblContent.Text = error.Message;
                    //lblContent.ForeColor = System.Drawing.Color.Red;
                    //ModalPopupExtender1.PopupControlID = "PnlApprove";
                    //ModalPopupExtender1.Show();
                    //PnlApprove.Visible = true;
                    ModalPopupExtender1.PopupControlID = "PnlProvide";
                    ModalPopupExtender1.Show();
                    PnlProvide.Visible = true;
                    isFinished = false;
                }
            }
            finally
            {
                tranlayer.DisposeTrn();
                if (isFinished == true)
                {
                    //lblT.Text = "Status";
                    //lblContent.Text = "Advice Accepted!!!";
                    //lblContent.ForeColor = System.Drawing.Color.Green;
                    //ModalPopupExtender1.PopupControlID = "PnlApprove";
                    //ModalPopupExtender1.Show();
                    //PnlApprove.Visible = true;
                    ModalPopupExtender1.PopupControlID = "PnlProvide";
                    ModalPopupExtender1.Show();
                    PnlProvide.Visible = true;
                }
            }
        }
        protected void btnAcceptOK_Click(object sender, EventArgs e)
        {
            bool isFinished = true;

            // Transcation trn = new Transcation(true);
            try
            {

                string DualTransactionKey = lbldual.Text;
                DateTime dtChoosenDate = Convert.ToDateTime(lblchoosendate.Text);
                string AppReceiptno = lblapprec.Text;
                DateTime Approveddate = Convert.ToDateTime(txtDate.Text);

              string InsertAppvoucher = "insert into svcf.`appvoucherapproval` (DualTransactionKey,ChoosenDate,BranchID,ApprovedDate,AppReceiptno) values (" + DualTransactionKey + ",'" + balayer.changedateformat(dtChoosenDate, 2) + "'," + balayer.ToobjectstrEvenNull(Session["Branchid"]) + ",'" + balayer.changedateformat(Approveddate, 2) + "','" + AppReceiptno + "')";
              string UpdateVouchertrans = "update `voucher` as t1 join `transbank` as t2 on (insertkey_from_bin(t1.DualTransactionKey) = insertkey_from_bin(t2.DualTransactionKey)) set t1.ISActive=0 , t1.Head_Id=" + ddlBankHead.SelectedItem.Value + " ,t2.BankHeadID=" + ddlBankHead.SelectedItem.Value + " , t2.ISActive=0 , t1.ChoosenDate= '" + balayer.changedateformat(Approveddate,2) + "' where  insertkey_from_bin(t1.DualTransactionKey)='" + DualTransactionKey + "' and t1.ChoosenDate='" + balayer.changedateformat(dtChoosenDate, 2) + "' and t1.BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and t1.Head_Id = 0000 ";
              string UpdateVouchertransbank = "update `voucher` as t1 join `transbank` as t2 on (insertkey_from_bin(t1.DualTransactionKey) = insertkey_from_bin(t2.DualTransactionKey)) set t1.ISActive=0 ,t1.ChoosenDate='" + balayer.changedateformat(Approveddate,2) + "' where  insertkey_from_bin(t1.DualTransactionKey)='" + DualTransactionKey + "' and t1.ChoosenDate='" + balayer.changedateformat(dtChoosenDate, 2) + "' and t1.BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + "";

              long iResultappvoucher = tranlayer.insertorupdateTrn(InsertAppvoucher);
              long iResultupdate = tranlayer.insertorupdateTrn(UpdateVouchertrans);
              long uResultup = tranlayer.insertorupdateTrn(UpdateVouchertransbank);
              tranlayer.CommitTrn();

            }
            catch (Exception error)
            {
                try
                {
                    tranlayer.RollbackTrn();
                }
                catch { }
                finally
                {
                    lblT.Text = "Status";
                    lblContent.Text = error.Message;
                    lblContent.ForeColor = System.Drawing.Color.Red;
                    ModalPopupExtender1.PopupControlID = "PnlApprove";
                    ModalPopupExtender1.Show();                   
                    PnlApprove.Visible = true;
                    isFinished = false;
                }
            }
            finally
            {
                lblT.Text = "Status";
                lblContent.Text = "Advice Accepted!!!";
                lblContent.ForeColor = System.Drawing.Color.Green;
                ModalPopupExtender1.PopupControlID = "PnlApprove";
                ModalPopupExtender1.Show();
                GetData();
                PnlApprove.Visible = true;
            }
        }
        protected void btnAcceptCancel_Click(object sender, EventArgs e)
        {
            GetData();
        }

        void fillBankHead()
        {
            DataTable dtBank = balayer.GetDataTable("SELECT concat( concat(t1.BankName,' _ ',t1.IFCCode),' _ ',t1.AccountNo) as BankDetails, t1.Head_Id as Head_Id, t1.Banklocation as Banklocation FROM svcf.bankdetails as t1 where t1.BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]));
            DataRow dr = dtBank.NewRow();
            dr[0] = "--Select--";
            dr[1] = "0";
            ddlBankHead.DataValueField = "Head_Id";
            ddlBankHead.DataTextField = "BankDetails";
            dtBank.Rows.InsertAt(dr, 0);
            ddlBankHead.DataSource = dtBank;
            ddlBankHead.DataBind();
        }

        protected void RadApprove_CheckedChanged(object sender, EventArgs e)
        {
            if(RadApprove.Checked==true)
            {
                RadView.Checked = false;
                Appvoucherapproval.Visible = true;
                Viewapproved.Visible = false;
                RadApprove.Checked = true;
            }
        }

        protected void RadView_CheckedChanged(object sender, EventArgs e)
        {
            if (RadView.Checked == true)
            {
                RadView.Checked = true;
                Appvoucherapproval.Visible = false;
                Viewapproved.Visible = true;
                RadApprove.Checked = false;
            }
        }
    }
}