using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxGridView.Export;
using System.Data;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrintingLinks;
using System.IO;
using DevExpress.Web.ASPxEditors;
using DevExpress.Web;
using System.Collections.Specialized;

using SVCF_BusinessAccessLayer;
using SVCF_TransactionLayer;
using SVCF_DataAccessLayer;
using log4net;
using log4net.Config;

namespace SreeVisalamChitFundLtd_phase1
{
    public partial class CRRApply : System.Web.UI.Page
    {
        string Sname;
        //#region VarDeclaration
        // CommonClassFile objcls = new CommonClassFile();
        //#endregion
        #region VarDeclaration
        BusinessLayer balayer = new BusinessLayer();
        TransactionLayer tranlayer = new TransactionLayer();
        #endregion

        ILog logger = log4net.LogManager.GetLogger(typeof(CRRApply));

        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (Session["UserName"] == null || Session["Branchid"] == null || Session["BranchName"] == null)
            {
                Response.Redirect(Page.ResolveUrl("~/Login.aspx"), true);
            }
        }

        protected void cbAll_Init(object sender, EventArgs e)
        {

            ASPxCheckBox chk = sender as ASPxCheckBox;

            ASPxGridView grid = (chk.NamingContainer as GridViewHeaderTemplateContainer).Grid;

            chk.Checked = (grid.Selection.Count == grid.VisibleRowCount);

        }

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
                RadList.AutoPostBack = true;
                RadList.SelectedIndexChanged += new EventHandler(RadList_SelectedIndexChanged);
                RadList.SelectedValue = "0";
                select();
            }
            
        }

        protected void select()
        {
            try
            {

                DataTable dtCRR = new DataTable();
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
                DataTable dtDate = balayer.GetDataTable("select distinct ChoosenDate from voucher where Trans_Type<>2 and Voucher_Type='C' and BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and choosenDate='" + balayer.indiandateToMysqlDate(txtFromDate.Text) + "' and Trans_Medium=0 and Other_Trans_Type not in (3,5) order by ChoosenDate asc");
                for (int i = 0; i < dtDate.Rows.Count; i++)
                {
                    dtCRR = balayer.GetDataTable("select distinct v1.TransactionKey,v1.`Voucher_No`,v1.Series,m1.GrpMemberID as ChitNumber, v1.`ChoosenDate`, REPLACE( v1.Narration,'  ','') as Narration,concat( m2.MemberID,' | ', m1.MemberName) as MemberName, v1.Amount as ChitAmount from voucher as v1  join headstree as ht1 on (v1.Head_Id=ht1.NodeID) join `membertogroupmaster` as m1 on (v1.Head_Id=m1.Head_Id) left join membermaster as m2 on (v1.MemberID=m2.MemberIdNew) where v1.RootID=5 and v1.IsAccepted=0 and v1.Trans_Type=1 and Voucher_Type='C' and v1.BranchID =" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and v1.choosenDate='" + balayer.indiandateToMysqlDate(balayer.ToobjectstrEvenNull(dtDate.Rows[i][0])) + "' and v1.Trans_Medium=0 and v1.Other_Trans_Type not in (3,5)");
                    dtOther = balayer.GetDataTable("SELECT distinct v1.TransactionKey,v1.`Voucher_No`,v1.Series,ht1.Node as OtherBranch,v1.`ChoosenDate`,REPLACE( v1.Narration,'  ','') as Narration, v1.Amount as BranchAmount FROM svcf.voucher as v1 join voucher as v2 on (v1.DualTransactionKey=v2.DualTransactionKey) join headstree as ht1 on (v1.Head_Id=ht1.NodeID) where v1.Rootid=1 and v1.Trans_Medium=0 and v1.Trans_Type=1 and v1.IsAccepted=0 and v1.ChoosenDate='" + balayer.indiandateToMysqlDate(balayer.ToobjectstrEvenNull(dtDate.Rows[i][0])) + "'  and v1.branchid=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and v1.voucher_type='C' and v2.rootid=12");
                    dtPandL = balayer.GetDataTable("SELECT distinct v1.TransactionKey,v1.`Voucher_No`,v1.Series,h1.Node as Heads, v1.`ChoosenDate`, REPLACE( v1.Narration,'  ','') as Narration, v1.Amount as HeadAmount FROM svcf.voucher as v1 join headstree as h1 on (v1.Head_Id=h1.NodeID) join headstree as h2 on (h1.ParentID=h2.NodeID) where v1.Rootid not in (1,5)  and v1.BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and v1.IsAccepted=0 and v1.Trans_Medium=0 and v1.Trans_Type=1 and v1.Voucher_Type='C' and v1.ChoosenDate='" + balayer.indiandateToMysqlDate(balayer.ToobjectstrEvenNull(dtDate.Rows[i][0])) + "'");
                    int count = 0;
                    for (int j = 0; j < dtCRR.Rows.Count; j++)
                    {
                        string strRange = balayer.GetSingleValue("SELECT moneycollid FROM svcf.assignreceiptbook WHERE " + dtCRR.Rows[j]["Voucher_No"] + " between receiptnofrom  and receiptnoto and receiptseries='" + dtCRR.Rows[j]["Series"] + "'");
                        string strName = "";
                        if (!string.IsNullOrEmpty(strRange))
                        {
                            strName = balayer.GetSingleValue("SELECT moneycollname FROM svcf.moneycollector where moneyCollid=" + strRange + "");
                        }
                        count += 1;
                        dr["TransactionKey"] = dtCRR.Rows[j]["TransactionKey"];

                        dr["Voucher_No"] = dtCRR.Rows[j]["Voucher_No"];
                        dr["Narration"] = dtCRR.Rows[j]["Narration"]; ;
                        dr["ChitNumber"] = dtCRR.Rows[j]["ChitNumber"]; ;
                        dr["MemberName"] = dtCRR.Rows[j]["MemberName"]; ;
                        dr["ChitAmount"] = dtCRR.Rows[j]["ChitAmount"];
                        dr["Heads"] = "";
                        dr["Amount"] = "";
                        dr["OtherAmount"] = "";
                        dr["GrandTotal"] = dtCRR.Rows[j]["ChitAmount"];
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
                    dtBind.Dispose();
                    dtCRR.Dispose();
                    dtOther.Dispose();
                    dtPandL.Dispose();
                }
            }
            catch (Exception) { }
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
            switch(RadList.SelectedValue)
            {
                case "Pending":                   
                    select();
                    break;

                case "Approved":
                    selectApprvd();
                    break;
            }           
        }

        public void selectApprvd()
        {
            try
            {
                DateTime aprvddt;
                aprvddt = Convert.ToDateTime(txtFromDate.Text);
                DataTable dtCRR = new DataTable();
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
                
                //for (int i = 0; i < dtDate.Rows.Count; i++)
                //{
                dtCRR = balayer.GetDataTable("select distinct v1.TransactionKey,v1.`Voucher_No`,v1.Series,m1.GrpMemberID as ChitNumber, v1.`ChoosenDate`, REPLACE( v1.Narration,'  ','') as Narration,concat( m2.MemberID,' | ', m1.MemberName) as MemberName, v1.Amount as ChitAmount from voucher as v1  join headstree as ht1 on (v1.Head_Id=ht1.NodeID) join `membertogroupmaster` as m1 on (v1.Head_Id=m1.Head_Id) left join membermaster as m2 on (v1.MemberID=m2.MemberIdNew) where v1.RootID=5 and v1.IsAccepted=0 and v1.Trans_Type=1 and Voucher_Type='C' and v1.BranchID =" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and v1.ApprovedDate='" + balayer.GetChangeDatFormat(aprvddt, 2) + "' and v1.Trans_Medium=0 and v1.IsAccepted=1 and v1.Other_Trans_Type not in (3,5)");
                dtOther = balayer.GetDataTable("SELECT distinct v1.TransactionKey,v1.`Voucher_No`,v1.Series,ht1.Node as OtherBranch,v1.`ChoosenDate`,REPLACE( v1.Narration,'  ','') as Narration, v1.Amount as BranchAmount FROM svcf.voucher as v1 join voucher as v2 on (v1.DualTransactionKey=v2.DualTransactionKey) join headstree as ht1 on (v1.Head_Id=ht1.NodeID) where v1.Rootid=1 and v1.Trans_Medium=0 and v1.Trans_Type=1 and v1.IsAccepted=1 and v1.ApprovedDate='" + balayer.GetChangeDatFormat(aprvddt, 2) + "'  and v1.branchid=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and v1.voucher_type='C' and v1.IsAccepted=1 and v2.rootid=12");
                dtPandL = balayer.GetDataTable("SELECT distinct v1.TransactionKey,v1.`Voucher_No`,v1.Series,h1.Node as Heads, v1.`ChoosenDate`, REPLACE( v1.Narration,'  ','') as Narration, v1.Amount as HeadAmount FROM svcf.voucher as v1 join headstree as h1 on (v1.Head_Id=h1.NodeID) join headstree as h2 on (h1.ParentID=h2.NodeID) where v1.Rootid not in (1,5)  and v1.BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and v1.IsAccepted=1 and v1.Trans_Medium=0 and v1.Trans_Type=1 and v1.Voucher_Type='C'and v1.IsAccepted=1 and v1.ApprovedDate='" + balayer.GetChangeDatFormat(aprvddt, 2) + "'");
                    int count = 0;
                    for (int j = 0; j < dtCRR.Rows.Count; j++)
                    {
                        string strRange =balayer.GetSingleValue("SELECT moneycollid FROM svcf.assignreceiptbook WHERE " + dtCRR.Rows[j]["Voucher_No"] + " between receiptnofrom  and receiptnoto and receiptseries='" + dtCRR.Rows[j]["Series"] + "'");
                        string strName = "";
                        if (!string.IsNullOrEmpty(strRange))
                        {
                            strName =balayer.GetSingleValue("SELECT moneycollname FROM svcf.moneycollector where moneyCollid=" + strRange + "");
                        }
                        count += 1;
                        dr["TransactionKey"] = dtCRR.Rows[j]["TransactionKey"];

                        dr["Voucher_No"] = dtCRR.Rows[j]["Voucher_No"];
                        dr["Narration"] = dtCRR.Rows[j]["Narration"]; ;
                        dr["ChitNumber"] = dtCRR.Rows[j]["ChitNumber"]; ;
                        dr["MemberName"] = dtCRR.Rows[j]["MemberName"]; ;
                        dr["ChitAmount"] = dtCRR.Rows[j]["ChitAmount"];
                        dr["Heads"] = "";
                        dr["Amount"] = "";
                        dr["OtherAmount"] = "";
                        dr["GrandTotal"] = dtCRR.Rows[j]["ChitAmount"];
                        dr["moneycollector"] = strName;
                        dtBind.Rows.Add(dr.ItemArray);
                    }
                    for (int j = 0; j < dtOther.Rows.Count; j++)
                    {
                        string strRange =balayer.GetSingleValue("SELECT moneycollid FROM svcf.assignreceiptbook WHERE " + dtOther.Rows[j]["Voucher_No"] + " between receiptnofrom  and receiptnoto and receiptseries='" + dtOther.Rows[j]["Series"] + "'");
                        string strName = "";
                        if (!string.IsNullOrEmpty(strRange))
                        {
                            strName =balayer.GetSingleValue("SELECT moneycollname FROM svcf.moneycollector where moneyCollid=" + strRange + "");
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
                        string strRange =balayer.GetSingleValue("SELECT moneycollid FROM svcf.assignreceiptbook WHERE " + dtPandL.Rows[j]["Voucher_No"] + " between receiptnofrom  and receiptnoto and receiptseries='" + dtPandL.Rows[j]["Series"] + "'");
                        string strName = "";
                        if (!string.IsNullOrEmpty(strRange))
                        {
                            strName =balayer.GetSingleValue("SELECT moneycollname FROM svcf.moneycollector where moneyCollid=" + strRange + "");
                        }
                        count += 1;
                        dr["TransactionKey"] = dtPandL.Rows[j]["TransactionKey"];
                        dr["Voucher_No"] = dtPandL.Rows[j]["Voucher_No"];
                        dr["Narration"] = dtPandL.Rows[j]["Narration"]; 
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
                    grid1.DataSource = dtBind;
                    grid1.DataBind();
                    dtBind.Dispose();
                    dtCRR.Dispose();
                    dtOther.Dispose();
                    dtPandL.Dispose();     
               // }
            }
            catch (Exception) { }
        }
        //protected void cbAll_Init(object sender, EventArgs e)
        //{
        //    ASPxCheckBox chk = sender as ASPxCheckBox;
        //    ASPxGridView grid = (chk.NamingContainer as GridViewHeaderTemplateContainer).Grid;
        //    if (chk.Checked == true)
        //    {
        //        chk.Checked = (grid.Selection.Count == grid.VisibleRowCount);
        //    }
       // }

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

        protected void grid_CustomJSProperties(object sender, ASPxGridViewClientJSPropertiesEventArgs e)
        {
            ASPxGridView grid = sender as ASPxGridView;

            Int32 start = grid.VisibleStartIndex;
            Int32 end = grid.VisibleStartIndex + grid.SettingsPager.PageSize;
            Int32 selectNumbers = 0;
            end = (end > grid.VisibleRowCount ? grid.VisibleRowCount : end);

            for (int i = start; i < end; i++)
                if (grid.Selection.IsRowSelected(i))
                    selectNumbers++;

            e.Properties["cpSelectedRowsOnPage"] = selectNumbers;
            e.Properties["cpVisibleRowCount"] = grid.VisibleRowCount;
        }

        protected void btnAccept_Click(object sender, EventArgs e)
        {

            DataTable dtMC = new DataTable();

            // ArrayList al = new ArrayList();
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
            lblcont.Text = "Do you want To accept following Cash receipts? ";

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
                    trn.insertorupdateTrn("update svcf.voucher set IsAccepted=1,ApprovedDate='" + DateTime.Now.ToString("yyyy/MM/dd") + "' where TransactionKey='" + ASPxListBox1.Items[i].Value + "'");
                }
                trn.CommitTrn();
                logger.Info("CRRApply.aspx - BtnOk_Click() - Completed: " + DateTime.Now + " by: " + Convert.ToString(Session["UserName"]) + "");
            }
            catch(Exception err)
            {
                try
                {
                   trn.RollbackTrn();
                   logger.Info("CRRApply.aspx - BtnOk_Click() - Error: " + err.Message + ": " + DateTime.Now + " by: " + Convert.ToString(Session["UserName"]) + "");
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
                Response.Redirect(Request.Url.AbsolutePath.ToString()+"?Date="+txtFromDate.Text);
            }
        }

        protected void RadList_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                switch(RadList.SelectedValue)
                {
                    case "Pending":
                        printdiv.Visible = true;
                        apprvdDiv.Visible = false;
                        break;

                    case "Approved":
                        apprvdDiv.Visible = true;
                        printdiv.Visible = false;                       
                        break;
                }

            }
            catch (Exception) { }

        }

        protected void grid1_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
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

        protected void grid1_CustomJSProperties(object sender, ASPxGridViewClientJSPropertiesEventArgs e)
        {
            ASPxGridView grid = sender as ASPxGridView;

            Int32 start = grid.VisibleStartIndex;
            Int32 end = grid.VisibleStartIndex + grid.SettingsPager.PageSize;
            Int32 selectNumbers = 0;
            end = (end > grid.VisibleRowCount ? grid.VisibleRowCount : end);

            for (int i = start; i < end; i++)
                if (grid.Selection.IsRowSelected(i))
                    selectNumbers++;

            e.Properties["cpSelectedRowsOnPage"] = selectNumbers;
            e.Properties["cpVisibleRowCount"] = grid.VisibleRowCount;
        }
    }
}