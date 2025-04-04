using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxGridView;
using System.Drawing;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrintingLinks;
using System.IO;
using DevExpress.Web.ASPxGridView.Export;
using System.Data;
using DevExpress.Web.ASPxEditors;
using DevExpress.Web.Data;
using System.Text;
using System.Collections;
using MySql.Data.MySqlClient;
using SVCF_BusinessAccessLayer;
using SVCF_DataAccessLayer;
using SVCF_TransactionLayer;

namespace SreeVisalamChitFundLtd_phase1
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        #region Object
        BusinessLayer balayer = new BusinessLayer();
        TransactionLayer trn = new TransactionLayer();
        #endregion
        string Sname;
        string s = null;
        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (Session["UserName"] == null || Session["Branchid"] == null || Session["BranchName"] == null)
            {
                Response.Redirect(Page.ResolveUrl("~/Login.aspx"), true);
            }
        }
        private System.Drawing.Image headerImage;
        protected void Page_Load(object sender, EventArgs e)
        {

            Pnlmsg.Visible = false;
            if (!IsPostBack)
            {
                if (string.IsNullOrEmpty(Request.QueryString["Date"]))
                {
                    txtFromDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                }
                else
                {
                    txtFromDate.Text = Convert.ToString(Request.QueryString["Date"]);
                }
            }
            select();
        }
        protected void select()
        {
            DataTable dtTRR = new DataTable();
            DataTable dtOther = new DataTable();
            DataTable dtPandL = new DataTable();
            DataTable dtBind = new DataTable();
            dtBind.Columns.Add("TransactionKey");
            dtBind.Columns.Add("slno");
            dtBind.Columns.Add("Voucher_No", typeof(string));
            dtBind.Columns.Add("Narration", typeof(string));
            dtBind.Columns.Add("ChitNumber", typeof(string));
            dtBind.Columns.Add("MemberName");
            dtBind.Columns.Add("ChitAmount");
            dtBind.Columns.Add("Heads");
            dtBind.Columns.Add("Amount");
            dtBind.Columns.Add("GrandTotal");
            dtBind.Columns.Add("OtherAmount");
            dtBind.Columns.Add("moneycollector");
            DataRow dr = dtBind.NewRow();
            DataTable dtM1 = balayer.GetDataTable("select m1.GrpMemberID,m1.MemberName,m2.moneycollname from svcf.membertogroupmaster m1  LEFT JOIN  svcf.moneycollector m2 on m1.M_Id=m2.moneycollid");
            DataTable dtDate = balayer.GetDataTable("select distinct ChoosenDate from voucher where Trans_Type<>2 and Voucher_Type='C' and BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and choosenDate='" + balayer.indiandateToMysqlDate(txtFromDate.Text) + "' and Trans_Medium=0 and Other_Trans_Type not in (3,5) order by ChoosenDate asc");
            for (int i = 0; i < dtDate.Rows.Count; i++)
            {
                dtTRR = balayer.GetDataTable("select distinct v1.TransactionKey,v1.`CurrDate`,v1.`Voucher_No`,v1.Series,ht1.Node as ChitNumber, v1.`ChoosenDate`, REPLACE( v1.Narration,'  ','') as Narration,concat( m2.MemberID,' | ', m1.MemberName) as MemberName,  v1.Amount as ChitAmount from voucher as v1  join headstree as ht1 on (v1.Head_Id=ht1.NodeID) left join `membertogroupmaster` as m1 on (v1.Head_Id=m1.Head_Id) left join membermaster as m2 on (v1.MemberID=m2.MemberIdNew) where v1.RootID=5 and v1.IsAccepted=0 and v1.Trans_Type<>2 and Voucher_Type='C' and v1.BranchID =" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and v1.choosenDate ='" + balayer.indiandateToMysqlDate(balayer.ToobjectstrEvenNull(dtDate.Rows[i][0])) + "' and v1.Trans_Medium=1 and v1.Other_Trans_Type not in (3,5)");
                dtOther = balayer.GetDataTable("SELECT distinct v1.TransactionKey,v1.`CurrDate`,v1.`Voucher_No`,v1.Series,ht1.Node as OtherBranch, v1.`ChoosenDate`,REPLACE( v1.Narration,'  ','') as Narration, v1.Amount as BranchAmount FROM svcf.voucher as v1 join voucher as v2 on (v1.DualTransactionKey=v2.DualTransactionKey) join headstree as ht1 on (v1.Head_Id=ht1.NodeID) where v1.Rootid=1 and v1.Trans_Medium=1 and v1.Trans_Type=1 and v1.IsAccepted=0 and v1.ChoosenDate='" + balayer.indiandateToMysqlDate(balayer.ToobjectstrEvenNull(dtDate.Rows[i][0])) + "'  and v1.branchid=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and v1.voucher_type='C' and v2.rootid=3");
                dtPandL = balayer.GetDataTable("SELECT distinct v1.TransactionKey,v1.`CurrDate`,v1.`Voucher_No`,v1.Series,h1.Node as Heads, v1.`ChoosenDate`, REPLACE( v1.Narration,'  ','') as Narration, v1.Amount as HeadAmount FROM svcf.voucher as v1 join headstree as h1 on (v1.Head_Id=h1.NodeID) join headstree as h2 on (h1.ParentID=h2.NodeID) where v1.Rootid not in (1,5) and v1.Trans_Medium in (1,3) and v1.Trans_Type=1 and v1.Voucher_Type='C' and v1.IsAccepted=0 and v1.BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and v1.ChoosenDate='" + balayer.indiandateToMysqlDate(balayer.ToobjectstrEvenNull(dtDate.Rows[i][0])) + "'");

                int count = 0;
                for (int j = 0; j < dtTRR.Rows.Count; j++)
                {
                    string strRange = balayer.GetSingleValue("SELECT moneycollid FROM svcf.assignreceiptbook WHERE " + dtTRR.Rows[j]["Voucher_No"] + " between receiptnofrom  and receiptnoto and receiptseries='" + dtTRR.Rows[j]["Series"] + "'");
                    string strName = "";
                    if (!string.IsNullOrEmpty(strRange))
                    {
                        strName = balayer.GetSingleValue("SELECT moneycollname FROM svcf.moneycollector where moneyCollid=" + strRange + "");
                    }
                    count += 1;
                    dr["TransactionKey"] = dtTRR.Rows[j]["TransactionKey"];
                    dr["Voucher_No"] = dtTRR.Rows[j]["Voucher_No"];
                    dr["Narration"] = dtTRR.Rows[j]["Narration"]; ;
                    dr["ChitNumber"] = dtTRR.Rows[j]["ChitNumber"]; ;
                    dr["MemberName"] = dtTRR.Rows[j]["MemberName"]; ;
                    dr["ChitAmount"] = dtTRR.Rows[j]["ChitAmount"];
                    dr["Heads"] = "";
                    dr["Amount"] = "";
                    dr["OtherAmount"] = "";
                    dr["GrandTotal"] = dtTRR.Rows[j]["ChitAmount"];

                    dr["moneycollector"] = strName;
                    dtBind.Rows.Add(dr.ItemArray);
                }
                for (int j = 0; j < dtOther.Rows.Count; j++)
                {
                    string strRange = balayer.GetSingleValue("SELECT moneycollid FROM svcf.assignreceiptbook WHERE " + dtOther.Rows[j]["Voucher_No"] + " between receiptnofrom  and receiptnoto and receiptseries='" + dtOther.Rows[j]["Series"] + "'");
                    string strName = "";
                    if (!string.IsNullOrEmpty(strRange))
                    {
                        strName = balayer.GetSingleValue("SELECT moneycollname FROM svcf.moneycollector where moneyCollid=" + strRange + "");
                    }
                    count += 1;
                    dr["TransactionKey"] = dtOther.Rows[j]["TransactionKey"];
                    dr["Voucher_No"] = dtOther.Rows[j]["Voucher_No"];
                    dr["Narration"] = dtOther.Rows[j]["Narration"]; ;
                    dr["ChitNumber"] = "";
                    dr["MemberName"] = "";
                    dr["ChitAmount"] = "";
                    dr["Heads"] = dtOther.Rows[j]["OtherBranch"];
                    dr["Amount"] = "";
                    dr["OtherAmount"] = dtOther.Rows[j]["BranchAmount"];
                    dr["GrandTotal"] = dtOther.Rows[j]["BranchAmount"];

                    dr["moneycollector"] = strName;
                    dtBind.Rows.Add(dr.ItemArray);
                }
                for (int j = 0; j < dtPandL.Rows.Count; j++)
                {
                    string strRange = balayer.GetSingleValue("SELECT moneycollid FROM svcf.assignreceiptbook WHERE " + dtPandL.Rows[j]["Voucher_No"] + " between receiptnofrom  and receiptnoto and receiptseries='" + dtPandL.Rows[j]["Series"] + "'");
                    string strName = "";
                    if (!string.IsNullOrEmpty(strRange))
                    {
                        strName = balayer.GetSingleValue("SELECT moneycollname FROM svcf.moneycollector where moneyCollid=" + strRange + "");
                    }
                    count += 1;
                    dr["TransactionKey"] = dtPandL.Rows[j]["TransactionKey"];
                    dr["Voucher_No"] = dtPandL.Rows[j]["Voucher_No"];
                    dr["Narration"] = dtPandL.Rows[j]["Narration"]; ;
                    dr["ChitNumber"] = "";
                    dr["MemberName"] = "";
                    dr["ChitAmount"] = "";
                    dr["Heads"] = dtPandL.Rows[j]["Heads"];
                    dr["Amount"] = dtPandL.Rows[j]["HeadAmount"];
                    dr["GrandTotal"] = dtPandL.Rows[j]["HeadAmount"];
                    dr["OtherAmount"] = "";

                    dr["moneycollector"] = strName;
                    dtBind.Rows.Add(dr.ItemArray);
                }
                grid.DataSource = dtBind;
                grid.DataBind();
            }
        }


        protected void grid_HtmlRowPrepared(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewTableRowEventArgs e)
        {
            if (e.RowType != GridViewRowType.Data) return;
            string Voucher = balayer.ToobjectstrEvenNull(e.GetValue("Voucher_No"));
            string narration = balayer.ToobjectstrEvenNull(e.GetValue("Narration"));
            if (string.IsNullOrEmpty(Voucher) | string.IsNullOrEmpty(narration))
            {
                e.Row.ForeColor = Color.Red;
                e.Row.Font.Bold = true;
            }
        }
        protected void BtnStatisticsGo_Click(object sender, EventArgs e)
        {
            select();
        }
        protected void cbPage_Init(object sender, EventArgs e)
        {
            ASPxCheckBox chk = sender as ASPxCheckBox;
            ASPxGridView grid = (chk.NamingContainer as GridViewHeaderTemplateContainer).Grid;

            Boolean cbChecked = true;
            Int32 start = grid.VisibleStartIndex;
            Int32 end = grid.VisibleStartIndex + grid.SettingsPager.PageSize;
            end = (end > grid.VisibleRowCount ? grid.VisibleRowCount : end);

            for (int i = start; i < end; i++)
                if (!grid.Selection.IsRowSelected(i))
                {
                    cbChecked = false;
                    break;
                }

            chk.Checked = cbChecked;
        }

        protected void btnAccept_Click(object sender, EventArgs e)
        {
            DataTable dtMC = new DataTable();
            DataTable dtBind = new DataTable();
            DataTable dt = new DataTable();
            DataRow row = dt.NewRow();
            dt.Columns.Add("TransactionKey");
            dt.Columns.Add("Voucher_No");
            dt.Columns.Add("Voucher_Type");
            dt.Columns.Add("Head_Id");
            //DataRow row = dt.NewRow();
            for (int i = 0; i < ASPxListBox1.Items.Count; i++)
            {
                dtMC = balayer.GetDataTable("select TransactionKey,Voucher_No,Voucher_Type,Head_Id from svcf.voucher where TransactionKey ='" + ASPxListBox1.Items[i].Value.ToString() + "'");

                for (int j = 0; j < dtMC.Rows.Count; j++)
                {
                    row["TransactionKey"] = dtMC.Rows[j]["TransactionKey"].ToString();
                    row["Voucher_No"] = dtMC.Rows[j]["Voucher_No"].ToString();
                    row["Voucher_Type"] = dtMC.Rows[j]["Voucher_Type"].ToString();
                    row["Head_Id"] = dtMC.Rows[j]["Head_Id"].ToString();
                    dt.Rows.Add(row.ItemArray);
                }
            }
            ModalPopupExtender2.PopupControlID = "Pnlmsg";
            this.ModalPopupExtender2.Show();
            Pnlmsg.Visible = true;
            lblT.Text = "Status";


            GridView1.DataSource = dt;
            GridView1.DataBind();
            lblcont.Text = "Do you want To accept following Cheque receipts? ";

            lblcont.Visible = true;
            lblContent.Visible = false;
            btnCancelPopup.Visible = true;
            lblHide.Visible = true;
        }

        protected void BtnOk_Click(object sender, EventArgs e)
        {
           TransactionLayer trn = new TransactionLayer();
            try
            {
                for (int i = 0; i < ASPxListBox1.Items.Count; i++)
                {
                    trn.insertorupdateTrn("update  svcf.voucher set IsAccepted=1 where TransactionKey='" + ASPxListBox1.Items[i].Value + "'");
                }
                trn.CommitTrn();
            }
            catch
            {
                try
                {
                   trn.RollbackTrn();
                }
                catch { }
            }
            finally
            {
                trn.DisposeTrn();
                GridView1.Visible = false;
                lblcont.Visible = false;
                lblContent.Visible = true;
                btnCancelPopup.Visible = false;
                lblHide.Visible = false;

                ModalPopupExtender2.PopupControlID = "pnl";
                this.ModalPopupExtender2.Show();
                lblT.Text = "Status";
                lblContent.Text = "Your Data Has Been Saved Successfully!...";
                lblContent.ForeColor = System.Drawing.Color.Green;
                Response.Redirect(Request.Url.AbsolutePath.ToString() + "?Date=" + txtFromDate.Text);

            }
        }
    }
}