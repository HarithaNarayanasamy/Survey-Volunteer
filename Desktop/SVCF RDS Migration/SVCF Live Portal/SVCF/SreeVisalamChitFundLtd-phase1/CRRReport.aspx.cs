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
using DevExpress.Web.ASPxGridView;
using DevExpress.Data;
using DevExpress.Web.ASPxMenu;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrintingLinks;
using System.IO;
using DevExpress.Web.ASPxGridView.Export;
using System.Drawing;
using SVCF_BusinessAccessLayer;
using SVCF_TransactionLayer;
using SVCF_DataAccessLayer;


namespace SreeVisalamChitFundLtd_phase1
{
    public partial class CRRReport : System.Web.UI.Page
    {
        #region Object
        BusinessLayer balayer = new BusinessLayer();
        TransactionLayer trn = new TransactionLayer();
        #endregion
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
            if (!IsPostBack)
            {
                DateTime dddd = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                txtFromDate.Text = dddd.ToString("dd/MM/yyyy");
                txtToDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                select();
                lblBranch.Text = "Branch : " + balayer.ToobjectstrEvenNull(Session["BranchName"]);
            }
            select();
        }
        protected void select()
        {
            DataTable dtCRR = new DataTable();
            DataTable dtOther = new DataTable();
            DataTable dtPandL = new DataTable();
            DataTable dtBind = new DataTable();
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
            dtBind.Columns.Add("Remarks");
            DataRow dr = dtBind.NewRow();

            //03/12/2021
            DataTable dtSorted=new DataTable();

            DataTable dtDate = balayer.GetDataTable("select distinct ChoosenDate from voucher where Trans_Type<>2 and Voucher_Type='C' and BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and choosenDate between '" + balayer.indiandateToMysqlDate(txtFromDate.Text) + "' and '" + balayer.indiandateToMysqlDate(txtToDate.Text) + "' and Trans_Medium=0 and Other_Trans_Type not in (3,5) order by ChoosenDate asc");
            for (int i = 0; i < dtDate.Rows.Count; i++)
            {
                //dtCRR = balayer.GetDataTable("select distinct v1.TransactionKey,v1.`Voucher_No`,v1.Series,m1.GrpMemberID as ChitNumber, v1.`ChoosenDate`, REPLACE( v1.Narration,'  ','') as Narration,concat( m2.MemberID,' | ', m1.MemberName) as MemberName, v1.Amount as ChitAmount from voucher as v1  join headstree as ht1 on (v1.Head_Id=ht1.NodeID) join `membertogroupmaster` as m1 on (v1.Head_Id=m1.Head_Id) left join membermaster as m2 on (v1.MemberID=m2.MemberIdNew) where v1.RootID=5 and v1.IsAccepted=1 and v1.Trans_Type=1 and Voucher_Type='C' and v1.BranchID =" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and v1.choosenDate='" + balayer.indiandateToMysqlDate(balayer.ToobjectstrEvenNull(dtDate.Rows[i][0])) + "' and v1.Trans_Medium=0 and v1.Other_Trans_Type not in (3,5)");
                //dtOther = balayer.GetDataTable("SELECT distinct v1.TransactionKey,v1.`Voucher_No`,v1.Series,ht1.Node as OtherBranch,v1.`ChoosenDate`,REPLACE( v1.Narration,'  ','') as Narration, v1.Amount as BranchAmount FROM svcf.voucher as v1 join voucher as v2 on (v1.DualTransactionKey=v2.DualTransactionKey) join headstree as ht1 on (v1.Head_Id=ht1.NodeID) where v1.Rootid=1 and v1.Trans_Medium=0 and v1.Trans_Type=1 and v1.ChoosenDate='" + balayer.indiandateToMysqlDate(balayer.ToobjectstrEvenNull(dtDate.Rows[i][0])) + "' and v1.IsAccepted=1 and v1.branchid=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and v1.voucher_type='C' and v2.rootid=12");
                //dtPandL = balayer.GetDataTable("SELECT distinct v1.TransactionKey,v1.`Voucher_No`,v1.Series,h1.Node as Heads, v1.`ChoosenDate`, REPLACE( v1.Narration,'  ','') as Narration, v1.Amount as HeadAmount FROM svcf.voucher as v1 join headstree as h1 on (v1.Head_Id=h1.NodeID) join headstree as h2 on (h1.ParentID=h2.NodeID) where v1.Rootid not in (1,5)  and v1.BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and v1.Trans_Medium=0 and v1.IsAccepted=1 and v1.Trans_Type=1 and v1.Voucher_Type='C' and v1.ChoosenDate='" + balayer.indiandateToMysqlDate(balayer.ToobjectstrEvenNull(dtDate.Rows[i][0])) + "'");
                //21/10/2021 -Bala
                //dtCRR = balayer.GetDataTable("select distinct v1.TransactionKey,v1.`Voucher_No`,v1.Series,m1.GrpMemberID as ChitNumber, v1.`ChoosenDate`, REPLACE( v1.Narration,'  ','') as Narration,concat( m2.MemberID,' | ', m1.MemberName) as MemberName, v1.Amount as ChitAmount from voucher as v1  join headstree as ht1 on (v1.Head_Id=ht1.NodeID) join `membertogroupmaster` as m1 on (v1.Head_Id=m1.Head_Id) left join membermaster as m2 on (v1.MemberID=m2.MemberIdNew) where v1.RootID=5  and v1.Trans_Type=1 and Voucher_Type='C' and v1.BranchID =" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and v1.choosenDate='" + balayer.indiandateToMysqlDate(balayer.ToobjectstrEvenNull(dtDate.Rows[i][0])) + "' and v1.Trans_Medium=0 and v1.Other_Trans_Type not in (3,5)");
                //03/01/2023 - bagya changed appreceiptno to show instead of series
                //dtCRR = balayer.GetDataTable("select distinct v1.TransactionKey,v1.`Voucher_No`,v1.Series,m1.GrpMemberID as ChitNumber, v1.`ChoosenDate`, REPLACE(v1.Narration, '  ', '') as Narration,concat(m2.MemberID, ' | ', m1.MemberName) as MemberName, v1.Amount as ChitAmount,v1.M_Id from voucher as v1  join headstree as ht1 on(v1.Head_Id = ht1.NodeID) join `membertogroupmaster` as m1 on(v1.Head_Id = m1.Head_Id) left join membermaster as m2 on(v1.MemberID = m2.MemberIdNew) where v1.RootID = 5  and v1.Trans_Type = 1 and Voucher_Type = 'C' and v1.BranchID = " + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and v1.choosenDate = '" + balayer.indiandateToMysqlDate(balayer.ToobjectstrEvenNull(dtDate.Rows[i][0])) + "' and v1.Trans_Medium = 0 and v1.Other_Trans_Type not in (3, 5)");
                //dtOther = balayer.GetDataTable("SELECT distinct v1.TransactionKey,v1.`Voucher_No`,v1.Series,ht1.Node as OtherBranch,v1.`ChoosenDate`,REPLACE( v1.Narration,'  ','') as Narration, v1.Amount as BranchAmount,v1.M_Id FROM svcf.voucher as v1 join voucher as v2 on (v1.DualTransactionKey=v2.DualTransactionKey) join headstree as ht1 on (v1.Head_Id=ht1.NodeID) where v1.Rootid=1 and v1.Trans_Medium=0 and v1.Trans_Type=1 and v1.ChoosenDate='" + balayer.indiandateToMysqlDate(balayer.ToobjectstrEvenNull(dtDate.Rows[i][0])) + "'  and v1.branchid=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and v1.voucher_type='C' and v2.rootid=12");
                //dtPandL = balayer.GetDataTable("SELECT distinct v1.TransactionKey,v1.`Voucher_No`,v1.Series,h1.Node as Heads, v1.`ChoosenDate`, REPLACE( v1.Narration,'  ','') as Narration, v1.Amount as HeadAmount,v1.M_Id FROM svcf.voucher as v1 join headstree as h1 on (v1.Head_Id=h1.NodeID) join headstree as h2 on (h1.ParentID=h2.NodeID) where v1.Rootid not in (1,5)  and v1.BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and v1.Trans_Medium=0  and v1.Trans_Type=1 and v1.Voucher_Type='C' and v1.ChoosenDate='" + balayer.indiandateToMysqlDate(balayer.ToobjectstrEvenNull(dtDate.Rows[i][0])) + "'");
                dtCRR = balayer.GetDataTable("select distinct v1.TransactionKey,case when v1.AppReceiptno is null or v1.AppReceiptno='' then v1.Voucher_No else v1.AppReceiptno end as Voucher_No,v1.Series,m1.GrpMemberID as ChitNumber, v1.`ChoosenDate`, REPLACE( v1.Narration,'  ','') as Narration,concat( m2.MemberID,' | ', m1.MemberName) as MemberName, v1.Amount as ChitAmount,v1.M_Id from voucher as v1  join headstree as ht1 on (v1.Head_Id=ht1.NodeID) join `membertogroupmaster` as m1 on (v1.Head_Id=m1.Head_Id) left join membermaster as m2 on (v1.MemberID=m2.MemberIdNew) where v1.RootID=5  and v1.Trans_Type=1 and Voucher_Type='C' and v1.BranchID =" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and v1.choosenDate='" + balayer.indiandateToMysqlDate(balayer.ToobjectstrEvenNull(dtDate.Rows[i][0])) + "' and v1.Trans_Medium=0 and v1.Other_Trans_Type not in (3,5)");
                dtOther = balayer.GetDataTable("SELECT distinct v1.TransactionKey,case when v1.AppReceiptno is null or v1.AppReceiptno='' then v1.Voucher_No else v1.AppReceiptno end as Voucher_No,v1.Series,ht1.Node as OtherBranch,v1.`ChoosenDate`,REPLACE( v1.Narration,'  ','') as Narration, v1.Amount as BranchAmount,v1.M_Id FROM svcf.voucher as v1 join voucher as v2 on (v1.DualTransactionKey=v2.DualTransactionKey) join headstree as ht1 on (v1.Head_Id=ht1.NodeID) where v1.Rootid=1 and v1.Trans_Medium=0 and v1.Trans_Type=1 and v1.ChoosenDate='" + balayer.indiandateToMysqlDate(balayer.ToobjectstrEvenNull(dtDate.Rows[i][0])) + "'  and v1.branchid=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and v1.voucher_type='C' and v2.rootid=12");
                dtPandL = balayer.GetDataTable("SELECT distinct v1.TransactionKey,case when v1.AppReceiptno is null or v1.AppReceiptno='' then v1.Voucher_No else v1.AppReceiptno end as Voucher_No,v1.Series,h1.Node as Heads, v1.`ChoosenDate`, REPLACE( v1.Narration,'  ','') as Narration, v1.Amount as HeadAmount,v1.M_Id FROM svcf.voucher as v1 join headstree as h1 on (v1.Head_Id=h1.NodeID) join headstree as h2 on (h1.ParentID=h2.NodeID) where v1.Rootid not in (1,5)  and v1.BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and v1.Trans_Medium=0  and v1.Trans_Type=1 and v1.Voucher_Type='C' and v1.ChoosenDate='" + balayer.indiandateToMysqlDate(balayer.ToobjectstrEvenNull(dtDate.Rows[i][0])) + "'");
                dr["slno"] = "";
                dr["Voucher_No"] = "";
                dr["Narration"] = "Date : " + dtDate.Rows[i][0];
                dr["ChitNumber"] = "";
                dr["MemberName"] ="";
                dr["ChitAmount"] = "";
                dr["Heads"] = "";
                dr["Amount"] = "";
                dr["GrandTotal"] = "";
                dr["OtherAmount"] = "";
                dr["Remarks"] = "";
                dtBind.Rows.Add(dr.ItemArray);
                int count = 0;
                
                for (int j = 0; j < dtCRR.Rows.Count; j++)
                {
                    //21/10/2021 - Bala
                    //string strRange = balayer.GetSingleValue("SELECT moneycollid FROM svcf.assignreceiptbook WHERE " + dtCRR.Rows[j]["Voucher_No"] + " between receiptnofrom  and receiptnoto and receiptseries='" + dtCRR.Rows[j]["Series"] + "'");
                    string strRange = "";
                    if (dtCRR.Rows[j]["M_Id"].ToString() == "")
                    {
                       strRange = balayer.GetSingleValue("SELECT moneycollid FROM svcf.assignreceiptbook WHERE " + dtCRR.Rows[j]["Voucher_No"] + " between receiptnofrom  and receiptnoto and receiptseries='" + dtCRR.Rows[j]["Series"] + "' and IsFinished!=1");
                    }
                    else
                    {
                        strRange = dtCRR.Rows[j]["M_Id"].ToString();
                    }
                    string strName = "";
                    if (!string.IsNullOrEmpty(strRange))
                    {
                        strName = balayer.GetSingleValue("SELECT moneycollname FROM svcf.moneycollector where moneyCollid=" + strRange + "");
                    }
                    

                    dr["slno"] = j + 1;
                    count += 1;
                    dr["Voucher_No"] = dtCRR.Rows[j]["Voucher_No"];
                    dr["Narration"] = dtCRR.Rows[j]["Narration"]; ;
                    dr["ChitNumber"] = dtCRR.Rows[j]["ChitNumber"]; ;
                    dr["MemberName"] = dtCRR.Rows[j]["MemberName"]; ;
                    dr["ChitAmount"] = dtCRR.Rows[j]["ChitAmount"];
                    dr["Heads"] = "";
                    dr["Amount"] = "";
                    dr["OtherAmount"] = "";
                    dr["GrandTotal"] = dtCRR.Rows[j]["ChitAmount"];
                    dr["Remarks"] = strName;
                    dtBind.Rows.Add(dr.ItemArray);
                }
                for (int j = 0; j < dtOther.Rows.Count; j++)
                {
                    //24/11/2021 
                    //string strRange = balayer.GetSingleValue("SELECT moneycollid FROM svcf.assignreceiptbook WHERE " + dtOther.Rows[j]["Voucher_No"] + " between receiptnofrom  and receiptnoto and receiptseries='" + dtOther.Rows[j]["Series"] + "'");
                    string strRange = "";
                    
                    if(dtOther.Rows[j]["M_Id"].ToString()=="")
                    {
                       strRange = balayer.GetSingleValue("SELECT moneycollid FROM svcf.assignreceiptbook WHERE " + dtOther.Rows[j]["Voucher_No"] + " between receiptnofrom  and receiptnoto and receiptseries='" + dtOther.Rows[j]["Series"] + "'");
                    }
                    else
                    {
                        strRange = dtOther.Rows[j]["M_Id"].ToString();
                    }
                    //
                    string strName = "";
                    if (!string.IsNullOrEmpty(strRange))
                    {
                        strName = balayer.GetSingleValue("SELECT moneycollname FROM svcf.moneycollector where moneyCollid=" + strRange + "");
                    }
                    dr["slno"] = count + 1;
                    count += 1;
                    dr["Voucher_No"] = dtOther.Rows[j]["Voucher_No"];
                    dr["Narration"] = dtOther.Rows[j]["Narration"]; ;
                    dr["ChitNumber"] = "";
                    dr["MemberName"] = "";
                    dr["ChitAmount"] = "";
                    dr["Heads"] = dtOther.Rows[j]["OtherBranch"];
                    dr["Amount"] = "";
                    dr["OtherAmount"] = dtOther.Rows[j]["BranchAmount"];
                    dr["GrandTotal"] = dtOther.Rows[j]["BranchAmount"];
                    dr["Remarks"] = strName;
                    dtBind.Rows.Add(dr.ItemArray);
                }
                for (int j = 0; j < dtPandL.Rows.Count; j++)
                {
                    //24/11/2021
                    //string strRange = balayer.GetSingleValue("SELECT moneycollid FROM svcf.assignreceiptbook WHERE " + dtPandL.Rows[j]["Voucher_No"] + " between receiptnofrom  and receiptnoto and receiptseries='" + dtPandL.Rows[j]["Series"] + "'");
                    string strRange = "";

                    if (dtPandL.Rows[j]["M_Id"].ToString() == "")
                    {
                        strRange = balayer.GetSingleValue("SELECT moneycollid FROM svcf.assignreceiptbook WHERE " + dtOther.Rows[j]["Voucher_No"] + " between receiptnofrom  and receiptnoto and receiptseries='" + dtOther.Rows[j]["Series"] + "'");
                    }
                    else
                    {
                        strRange = dtPandL.Rows[j]["M_Id"].ToString();
                    }
                    //
                    string strName = "";
                    if (!string.IsNullOrEmpty(strRange))
                    {
                        strName = balayer.GetSingleValue("SELECT moneycollname FROM svcf.moneycollector where moneyCollid=" + strRange + "");
                    }
                    dr["slno"] = count + 1;
                    count += 1;
                    dr["Voucher_No"] = dtPandL.Rows[j]["Voucher_No"];
                    dr["Narration"] = dtPandL.Rows[j]["Narration"]; ;
                    dr["ChitNumber"] = "";
                    dr["MemberName"] = "";
                    dr["ChitAmount"] = "";
                    dr["Heads"] = dtPandL.Rows[j]["Heads"];
                    dr["Amount"] = dtPandL.Rows[j]["HeadAmount"];
                    dr["GrandTotal"] = dtPandL.Rows[j]["HeadAmount"];
                    dr["OtherAmount"] = "";
                    dr["Remarks"] = strName;
                    dtBind.Rows.Add(dr.ItemArray);
                }
                object smCRR;
                smCRR = dtCRR.Compute("sum(ChitAmount)", "");
                object smPL;
                smPL = dtPandL.Compute("sum(HeadAmount)", "");
                object smB;
                smB = dtOther.Compute("sum(BranchAmount)", "");
                if (string.IsNullOrEmpty(balayer.ToobjectstrEvenNull(smCRR)))
                    smCRR = 0.00M;
                if (string.IsNullOrEmpty(balayer.ToobjectstrEvenNull(smB)))
                    smB = 0.00M;
                if (string.IsNullOrEmpty(balayer.ToobjectstrEvenNull(smPL)))
                    smPL = 0.00M;
                dr["slno"] = "";
                dr["Voucher_No"] = "";
                dr["Narration"] = "";
                dr["ChitNumber"] = "";
                dr["MemberName"] = "Total";
                dr["ChitAmount"] = smCRR;
                dr["Heads"] = "";
                dr["Amount"] = Convert.ToDecimal(smPL) ;
                dr["OtherAmount"] = Convert.ToDecimal(smB);
                dr["Remarks"] = "";
                dr["GrandTotal"] = Convert.ToDecimal(smCRR) + Convert.ToDecimal(smPL) + Convert.ToDecimal(smB);
                dtBind.Rows.Add(dr.ItemArray);

                DataTable dtClone = new DataTable();
                dtClone.Columns.Add("slno");
                dtClone.Columns.Add("Voucher_No", typeof(string));
                dtClone.Columns.Add("Narration", typeof(string));
                dtClone.Columns.Add("ChitNumber", typeof(string));
                dtClone.Columns.Add("MemberName");
                dtClone.Columns.Add("ChitAmount");
                dtClone.Columns.Add("Heads");
                dtClone.Columns.Add("Amount");
                dtClone.Columns.Add("GrandTotal");
                dtClone.Columns.Add("OtherAmount");
                dtClone.Columns.Add("Remarks");

                DataRow drNew = dtClone.NewRow();
                drNew["slno"] = "";
                drNew["Voucher_No"] = "";
                drNew["Narration"] = "Date : " + dtDate.Rows[i][0];
                drNew["ChitNumber"] = "";
                drNew["MemberName"] = "";
                drNew["ChitAmount"] = "";
                drNew["Heads"] = "";
                drNew["Amount"] = "";
                drNew["GrandTotal"] = "";
                drNew["OtherAmount"] = "";
                drNew["Remarks"] = "";
                dtClone.Rows.Add(drNew.ItemArray);

                DataView dv = dtBind.DefaultView;
                dv.RowFilter = "Voucher_No<>''";
                dv.Sort = "Voucher_No";

                dtClone.Merge(dv.ToTable());

                drNew["slno"] = "";
                drNew["Voucher_No"] = "";
                drNew["Narration"] = "";
                drNew["ChitNumber"] = "";
                drNew["MemberName"] = "Total";
                drNew["ChitAmount"] = smCRR;
                drNew["Heads"] = "";
                drNew["Amount"] = Convert.ToDecimal(smPL);
                drNew["OtherAmount"] = Convert.ToDecimal(smB);
                drNew["Remarks"] = "";
                drNew["GrandTotal"] = Convert.ToDecimal(smCRR) + Convert.ToDecimal(smPL) + Convert.ToDecimal(smB);
                dtClone.Rows.Add(drNew.ItemArray);

                if (dtSorted.Rows.Count == 0)
                {
                    dtSorted = dtClone.Copy();
                }
                else
                {
                    dtSorted.Merge(dtClone);
                }

                dtBind.Clear();
                dtClone.Clear();
                grid.DataSource = dtSorted;
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
        protected void exporter_RenderBrick(object sender, ASPxGridViewExportRenderingEventArgs e)
        {
            GridViewDataColumn dataColumn = e.Column as GridViewDataColumn;
            if (dataColumn != null & e.RowType != GridViewRowType.Header)
            {
                if (dataColumn.FieldName == "slno")
                {
                    if (string.IsNullOrEmpty(balayer.ToobjectstrEvenNull(e.GetValue("Voucher_No"))) | string.IsNullOrEmpty(balayer.ToobjectstrEvenNull(e.GetValue("Narration"))))
                    {
                        e.BrickStyle.BackColor = Color.Wheat;
                        e.BrickStyle.ForeColor = Color.White;
                    }
                }
                if (dataColumn.FieldName == "Voucher_No")
                {
                    if (string.IsNullOrEmpty(balayer.ToobjectstrEvenNull(e.GetValue("Voucher_No"))) | string.IsNullOrEmpty(balayer.ToobjectstrEvenNull(e.GetValue("Narration"))))
                    {
                        e.BrickStyle.BackColor = Color.Wheat;
                        e.BrickStyle.ForeColor = Color.White;
                    }
                }
                if (dataColumn.FieldName == "Narration")
                {
                    if (string.IsNullOrEmpty(balayer.ToobjectstrEvenNull(e.GetValue("Voucher_No"))) | string.IsNullOrEmpty(balayer.ToobjectstrEvenNull(e.GetValue("Narration"))))
                    {
                        e.BrickStyle.BackColor = Color.Wheat;
                        e.BrickStyle.ForeColor = Color.White;
                    }
                }
                if (dataColumn.FieldName == "ChitNumber")
                {
                    if (string.IsNullOrEmpty(balayer.ToobjectstrEvenNull(e.GetValue("Voucher_No"))) | string.IsNullOrEmpty(balayer.ToobjectstrEvenNull(e.GetValue("Narration"))))
                    {
                        e.BrickStyle.BackColor = Color.Wheat;
                        e.BrickStyle.ForeColor = Color.White;
                    }
                }
                if (dataColumn.FieldName == "MemberName")
                {
                    if (string.IsNullOrEmpty(balayer.ToobjectstrEvenNull(e.GetValue("Voucher_No"))) | string.IsNullOrEmpty(balayer.ToobjectstrEvenNull(e.GetValue("Narration"))))
                    {
                        e.BrickStyle.BackColor = Color.Wheat;
                        e.BrickStyle.ForeColor = Color.White;
                    }
                }
                if (dataColumn.FieldName == "ChitAmount")
                {
                    if (string.IsNullOrEmpty(balayer.ToobjectstrEvenNull(e.GetValue("Voucher_No"))) | string.IsNullOrEmpty(balayer.ToobjectstrEvenNull(e.GetValue("Narration"))))
                    {
                        e.BrickStyle.BackColor = Color.Wheat;
                        e.BrickStyle.ForeColor = Color.White;
                    }
                }
                if (dataColumn.FieldName == "Heads")
                {
                    if (string.IsNullOrEmpty(balayer.ToobjectstrEvenNull(e.GetValue("Voucher_No"))) | string.IsNullOrEmpty(balayer.ToobjectstrEvenNull(e.GetValue("Narration"))))
                    {
                        e.BrickStyle.BackColor = Color.Wheat;
                        e.BrickStyle.ForeColor = Color.White;
                    }
                }
                if (dataColumn.FieldName == "Amount")
                {
                    if (string.IsNullOrEmpty(balayer.ToobjectstrEvenNull(e.GetValue("Voucher_No"))) | string.IsNullOrEmpty(balayer.ToobjectstrEvenNull(e.GetValue("Narration"))))
                    {
                        e.BrickStyle.BackColor = Color.Wheat;
                        e.BrickStyle.ForeColor = Color.White;
                    }
                }
                if (dataColumn.FieldName == "GrandTotal")
                {
                    if (string.IsNullOrEmpty(balayer.ToobjectstrEvenNull(e.GetValue("Voucher_No"))) | string.IsNullOrEmpty(balayer.ToobjectstrEvenNull(e.GetValue("Narration"))))
                    {
                        e.BrickStyle.BackColor = Color.Wheat;
                        e.BrickStyle.ForeColor = Color.White;
                    }
                }
                if (dataColumn.FieldName == "slfolio")
                {
                    if (string.IsNullOrEmpty(balayer.ToobjectstrEvenNull(e.GetValue("Voucher_No"))) | string.IsNullOrEmpty(balayer.ToobjectstrEvenNull(e.GetValue("Narration"))))
                    {
                        e.BrickStyle.BackColor = Color.Wheat;
                        e.BrickStyle.ForeColor = Color.White;
                    }
                }
                if (dataColumn.FieldName == "initial")
                {
                    if (string.IsNullOrEmpty(balayer.ToobjectstrEvenNull(e.GetValue("Voucher_No"))) | string.IsNullOrEmpty(balayer.ToobjectstrEvenNull(e.GetValue("Narration"))))
                    {
                        e.BrickStyle.BackColor = Color.Wheat;
                        e.BrickStyle.ForeColor = Color.White;
                    }
                }
                if (dataColumn.FieldName == "Remarks")
                {
                    if (string.IsNullOrEmpty(balayer.ToobjectstrEvenNull(e.GetValue("Voucher_No"))) | string.IsNullOrEmpty(balayer.ToobjectstrEvenNull(e.GetValue("Narration"))))
                    {
                        e.BrickStyle.BackColor = Color.Wheat;
                        e.BrickStyle.ForeColor = Color.White;
                    }
                }
            }
        }
        protected void BtnStatisticsGo_Click(object sender, EventArgs e)
        {
            select();
        }

        protected void Export_click(object sender, MenuItemEventArgs e)
        {
            if (e.Item.Text.ToString() == "PDF")
            {
                using (this.headerImage = System.Drawing.Image.FromFile(Server.MapPath("~\\Images\\visalam.png")))
                {
                    PrintingSystem ps = new PrintingSystem();
                    PrintableComponentLink grid = new PrintableComponentLink(ps);
                    grid.Component = gridExport;

                    Link header = new Link();
                    CompositeLink compositeLink = new CompositeLink(ps);
                    header.CreateDetailArea += new CreateAreaEventHandler(header_CreateDetailArea);
                    compositeLink.Links.AddRange(new object[] { header, grid });
                    string leftColumn = "Pages : [Page # of Pages #]";
                    string rightColumn = "Date from: " + txtFromDate.Text + " to: " + txtToDate.Text;
                    PageHeaderFooter phf = compositeLink.PageHeaderFooter as PageHeaderFooter;
                    phf.Footer.Content.Clear();
                    phf.Footer.Content.AddRange(new string[] { rightColumn, leftColumn });
                    phf.Footer.LineAlignment = BrickAlignment.Center;
                    using (MemoryStream stream = new MemoryStream())
                    {
                        compositeLink.PaperKind = System.Drawing.Printing.PaperKind.A4;
                        //compositeLink.Landscape = true;
                        compositeLink.CreateDocument(false);

                        compositeLink.PrintingSystem.Document.AutoFitToPagesWidth = 1;
                        compositeLink.PrintingSystem.ExportToPdf(stream);
                        WriteToResponse("_10CashReceviedRegister", true, "pdf", stream);
                    }
                }
            }
        }
        void header_CreateDetailArea(object sender, CreateAreaEventArgs e)
        {
            e.Graph.BorderWidth = 0;
            Rectangle r = new Rectangle(0, 0, 50, 50);
            e.Graph.DrawImage(headerImage, r);
            TextBrick tb = new TextBrick();
            tb.Text = "SREE VISALAM CHIT FUND LTD.,";
            tb.Font = new Font("Arial", 10, FontStyle.Bold);
            tb.Rect = new RectangleF(50, 15, 260, 19);
            tb.BorderWidth = 0;
            tb.BackColor = Color.Transparent;
            tb.HorzAlignment = DevExpress.Utils.HorzAlignment.Center;
            tb.VertAlignment = DevExpress.Utils.VertAlignment.Top;
            e.Graph.DrawBrick(tb);
            TextBrick tb1 = new TextBrick();
            tb1.Text = "BRANCH : " + Session["BranchName"];
            tb1.Font = new Font("Arial", 9, FontStyle.Bold);
            tb1.Rect = new RectangleF(50, 34, 260, 25);
            tb1.BorderWidth = 0;
            tb1.BackColor = Color.Transparent;
            tb1.HorzAlignment = DevExpress.Utils.HorzAlignment.Center;
            tb1.VertAlignment = DevExpress.Utils.VertAlignment.Top;
            e.Graph.DrawBrick(tb1);
        }
        void WriteToResponse(string fileName, bool saveAsFile, string fileFormat, MemoryStream stream)
        {
            if (Page == null || Page.Response == null)
                return;
            string disposition = saveAsFile ? "attachment" : "inline";
            Page.Response.Clear();
            Page.Response.Buffer = false;
            Page.Response.AppendHeader("Content-Type", string.Format("application/{0}", fileFormat));
            Page.Response.AppendHeader("Content-Transfer-Encoding", "binary");
            Page.Response.AppendHeader("Content-Disposition",
                string.Format("{0}; filename={1}.{2}", disposition, fileName, fileFormat));
            Page.Response.BinaryWrite(stream.GetBuffer());
            Page.Response.End();
        }
    }
}