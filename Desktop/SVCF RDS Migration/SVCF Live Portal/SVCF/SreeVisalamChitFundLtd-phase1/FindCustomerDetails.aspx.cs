using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DevExpress.Web.ASPxMenu;
using DevExpress.Web.ASPxGridView;
using DevExpress.Data;
using SVCF_BusinessAccessLayer;
using SVCF_DataAccessLayer;
using System.IO;
using MySql.Data.MySqlClient;
using log4net;


namespace SreeVisalamChitFundLtd_phase1
{
    public partial class FindCustomerDetails : System.Web.UI.Page
    {
        #region Object
        BusinessLayer balayer = new BusinessLayer();
        ILog logger = log4net.LogManager.GetLogger(typeof(FindCustomerDetails));
        CommonVariables objCOM = new CommonVariables();
        #endregion

        protected virtual string GetLabelText(GridViewGroupRowTemplateContainer container)
        {
            return container.GroupText;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                if (!IsPostBack)
                {
                    select("1203456789");
                    //  balayer.GetInsertItem("create or replace view `view_groupwisedue` as select `groupmaster`.`GROUPNO` AS `GroupIDOriginal`, `auctiondetails`.`GroupID` AS `GroupId`, sum(`auctiondetails`.`CurrentDueAmount`) AS `TotaldueAmount` from (`auctiondetails` join `groupmaster` ON ((`auctiondetails`.`GroupID` = `groupmaster`.`Head_Id`))) where `auctiondetails`.`branchid`="+ balayer.ToobjectstrEvenNull(Session["Branchid"]) + "and (`auctiondetails`.`AuctionDate` <= '" + DateTime.Now.ToString("yyyy/MM/dd") + "') group by `auctiondetails`.`GroupID`");


                    balayer.GetInsertItem(" select `groupmaster`.`GROUPNO` AS `GroupIDOriginal`, `auctiondetails`.`GroupID` AS `GroupId`, sum(`auctiondetails`.`CurrentDueAmount`) AS `TotaldueAmount` from (`auctiondetails` join `groupmaster` ON ((`auctiondetails`.`GroupID` = `groupmaster`.`Head_Id`))) where `auctiondetails`.`branchid`=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and (`auctiondetails`.`AuctionDate` <= '" + DateTime.Now.ToString("yyyy/MM/dd") + "') group by `auctiondetails`.`GroupID`");
                    //add membermaster branchid
                    //change on 21/08/2018
                    string SelectCommand = "SELECT mtg.MemberID, mtg.MemberName,concat(mtg.MemberName,',',mtg.MemberAddress,',',IFNULL(mm.AadharNumber,'Not Available')) as MemberAddress,mm.AadharNumber as AadharNumber from membertogroupmaster as mtg join membermaster as mm on(mtg.MemberID=mm.MemberIDNew) where  mm.BranchId=" + (Session["Branchid"]) + " group by mtg.MemberID";
                    MysqlDscustomer.ConnectionString = CommonClassFile.ConnectionString;
                    MysqlDscustomer.SelectCommand = "SELECT mtg.MemberID, mtg.MemberName,concat(mtg.MemberName,',',mtg.MemberAddress,'^',IFNULL(mm.AadharNumber,'Not Available')) as MemberAddress,mm.AadharNumber as AadharNumber from membertogroupmaster as mtg join membermaster as mm on(mtg.MemberID=mm.MemberIDNew) where  mm.BranchId=" + (Session["Branchid"]) + " group by mtg.MemberID";
                    //add membermaster branchid
                    //change on 21/08/2018
                    this.UploadMember.Attributes["onclick"] = string.Format("document.getElementById('{0}').value= document.getElementById('{1}').value;", this.HiddenField1.ClientID, this.FileUpload1.ClientID);
                }
                if (this.FileUpload1.PostedFile != null && this.FileUpload1.PostedFile.ContentLength > 0)
                {
                    this.PostedFile = this.FileUpload1.PostedFile;
                }
                if (!string.IsNullOrEmpty(this.HiddenField1.Value))
                {

                    string[] strPat = this.HiddenField1.Value.Split('\\');
                    this.lblCurrentFile.Text = strPat[strPat.Length - 1];
                }

            }
            catch (Exception ex)
            {
                logger.Info("FindCustomerDetails  - Page_Load() - Catch: " + ex.Message + ":" + DateTime.Now + " by: " + Convert.ToString(Session["UserName"]) + "");
            }

        }

        protected void ASPxGridView1_SummaryDisplayText(object sender, ASPxGridViewSummaryDisplayTextEventArgs e)
        {
            if (e.Item.FieldName == "Credit")
                e.Text = string.Format("{0}", Convert.ToDouble(e.Value));
            if (e.Item.FieldName == "Debit")
                e.Text = string.Format("{0}", Convert.ToDouble(e.Value));
            if (e.Item.FieldName == "Narration")
                e.Text = string.Format(balayer.ToobjectstrEvenNull(ViewState["crdr"]), Convert.ToDouble(e.Value));
        }
        void ApplyLayout(int layoutIndex)
        {
            grid.BeginUpdate();
            try
            {
                grid.ClearSort();
                switch (layoutIndex)
                {
                    case 0:
                        grid.GroupBy(grid.Columns["LedgerHead"]);
                        grid.GroupBy(grid.Columns["Date"]);
                        break;
                    case 1:
                        grid.GroupBy(grid.Columns["Chits"]);
                        grid.GroupBy(grid.Columns["SubHead"]);
                        break;
                    case 2:
                        break;
                }
            }
            finally
            {
                grid.EndUpdate();
            }
            grid.CollapseAll();
        }
        protected void grid_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            ApplyLayout(Int32.Parse(e.Parameters));
        }

        protected void grid_CustomUnboundColumnData(object sender, ASPxGridViewColumnDataEventArgs e)
        {

        }

        private HttpPostedFile PostedFile
        {
            get
            {
                if (Page.Session["postedFile"] != null)
                {
                    return (HttpPostedFile)Page.Session["postedFile"];
                }
                return null;
            }
            set { Page.Session["postedFile"] = value; }
        }

        protected void UploadMember_click(object sender, EventArgs e)
        {
            var id = "";
            if (Session["Memberid"]!=null)
            {
                id = Session["Memberid"].ToString();
            
            if (FileUpload1.HasFile)
            {
                CommonClassFile.IMGusersPhoto = FileUpload1.PostedFile;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "javascript:alert(' photo uploaded successfully')", true);
            }
                if (CommonClassFile.IMGusersPhoto != null)
                {
                    int length = CommonClassFile.IMGusersPhoto.ContentLength;
                    byte[] imgbyte = new byte[length];
                    using (var binaryReader = new BinaryReader(CommonClassFile.IMGusersPhoto.InputStream))
                    {
                        imgbyte = binaryReader.ReadBytes(CommonClassFile.IMGusersPhoto.ContentLength);
                    }
                    //memberIDnew = balayer.GetSingleValue("SELECT MemberIDNew FROM svcf.membermaster where MemberID='" + MemberID + "' and BranchId=" + BranchID + "");
                    MySqlConnection connection = new MySqlConnection(CommonClassFile.ConnectionString);
                    connection.Open();

                    string checkid = balayer.GetSingleValue("Select Count(*) from membersdocuments where MemberID ='" + id + "'");
                    if (checkid == "0")
                    {
                        MySqlCommand cmd = new MySqlCommand("INSERT INTO membersdocuments (MemberID ,Photo,ImageTYpe) VALUES (?MemberID,?Photo,?ImageTYpe)", connection);
                        cmd.Parameters.Add("?MemberID", MySqlDbType.Int32, 45).Value = id;
                        cmd.Parameters.Add("?Photo", MySqlDbType.Blob).Value = imgbyte;
                        cmd.Parameters.Add("?ImageTYpe", MySqlDbType.VarChar, 15).Value = CommonClassFile.IMGusersPhoto.ContentType;
                        int count = cmd.ExecuteNonQuery();
                    }
                    else
                    {
                        //  MySqlCommand cmd = new MySqlCommand("INSERT INTO membersdocuments (MemberID ,Photo,ImageTYpe) VALUES (?MemberID,?Photo,?ImageTYpe)", connection);
                        MySqlCommand cmd = new MySqlCommand("Update membersdocuments Set Photo=?Photo,ImageTYpe=?ImageTYpe where MemberID=?MemberID", connection);
                        cmd.Parameters.Add("?MemberID", MySqlDbType.Int32, 45).Value = id;
                        cmd.Parameters.Add("?Photo", MySqlDbType.Blob).Value = imgbyte;
                        cmd.Parameters.Add("?ImageTYpe", MySqlDbType.VarChar, 15).Value = CommonClassFile.IMGusersPhoto.ContentType;
                        int count = cmd.ExecuteNonQuery();
                    }




                    connection.Close();
                    CommonClassFile.IMGusersPhoto = null;
                    Response.Redirect("FindCustomerDetails.aspx");

                }
            }
        }


        protected void ASPxGridView1_OnCustomSummaryCalculate(object sender, CustomSummaryEventArgs e)
        {
            ASPxSummaryItem credit = (sender as ASPxGridView).TotalSummary["Credit"];
            ASPxSummaryItem debit = (sender as ASPxGridView).TotalSummary["Debit"];
            Decimal income = Convert.ToDecimal(((ASPxGridView)sender).GetTotalSummaryValue(credit));
            Decimal expense = Convert.ToDecimal(((ASPxGridView)sender).GetTotalSummaryValue(debit));
            if (income > expense)
            {
                ViewState["crdr"] = "Cr. Balance:";
                e.TotalValue = income - expense;
            }
            else if (income < expense)
            {
                ViewState["crdr"] = "Dr. Balance:";
                e.TotalValue = expense - income;
            }
            else
            {
                ViewState["crdr"] = "";
                e.TotalValue = 0.00;
            }
            ViewState["crdr"] += e.TotalValue.ToString();
        }

        protected void chkWithAddress_CheckedChanged(object sender, EventArgs e)
        {

            if (chkWithAddress.Checked)
            {
                string SelectCommand = "SELECT mtg.MemberID, mtg.MemberName,concat(mtg.MemberName,',',mtg.MemberAddress,'^',IFNULL(mm.AadharNumber,'Not Available')) as MemberAddress,mm.AadharNumber from membertogroupmaster as mtg join membermaster as mm on(mtg.MemberID=mm.MemberIDNew) where mtg.BranchID=" + (Session["Branchid"]) + " group by mtg.MemberID";
                MysqlDscustomer.ConnectionString = CommonClassFile.ConnectionString;
                MysqlDscustomer.SelectCommand = "SELECT mtg.MemberID, mtg.MemberName,concat(mtg.MemberName,',',mtg.MemberAddress,'^',IFNULL(mm.AadharNumber,'Not Available')) as MemberAddress,mm.AadharNumber from membertogroupmaster as mtg join membermaster as mm on(mtg.MemberID=mm.MemberIDNew) where mtg.BranchID=" + (Session["Branchid"]) + " group by mtg.MemberID";
                titleIndex.TextField = "MemberAddress";

            }
            else
            {
                string SelectCommand = "SELECT MemberID,MemberName,concat(MemberName,',',MemberAddress,'^',IFNULL(mm.AadharNumber,'Not Available')) as MemberAddress from membertogroupmaster where BranchID=" + (Session["Branchid"]) + " group by MemberID";
                MysqlDscustomer.ConnectionString = CommonClassFile.ConnectionString;
                MysqlDscustomer.SelectCommand = "SELECT mtg.MemberID, mtg.MemberName,concat(mtg.MemberName,',',mtg.MemberAddress,'^',IFNULL(mm.AadharNumber,'Not Available')) as MemberAddress,mm.AadharNumber as AadharNumber from membertogroupmaster as mtg join membermaster as mm on(mtg.MemberID=mm.MemberIDNew) where mtg.BranchID=" + (Session["Branchid"]) + " group by mtg.MemberID";
                titleIndex.TextField = "MemberName";
            }

            titleIndex.DataBind();


        }

        void ResetTrans()
        {
            lblCrAmount.Text = "0.00";
            lblDrAmount.Text = "0.00";
            lblExcess.Text = "0.00";
            lblPrizedArrier.Text = "0.00";
            lblNonPrizedArrier.Text = "0.00";
            lblByNumber.Text = "N/A";

            lblGuarantorName.Text = "Nill";
            lblDrawNo.Text = "Nill";
            lblPaymentDate.Text = "Nill";
            lblAuctionDate.Text = "Nill";

        }

        protected void Export_click(object sender, MenuItemEventArgs e)
        {
            if (e.Item.Text.ToString() == "PDF")
            {
                gridExport.PaperKind = System.Drawing.Printing.PaperKind.B4;
                gridExport.PreserveGroupRowStates = true;
                gridExport.WritePdfToResponse();

            }
            else if (e.Item.Text.ToString() == "XLSX")
            {
                gridExport.PaperKind = System.Drawing.Printing.PaperKind.A2;
                gridExport.MaxColumnWidth = 300;
                gridExport.RightMargin = 0;
                gridExport.WriteXlsxToResponse();
            }
        }
        protected void titleIndex_ItemClick(object source, DevExpress.Web.ASPxTitleIndex.TitleIndexItemEventArgs e)
        {
            try
            {
                string strMemberID = balayer.ToobjectstrEvenNull(e.Item.Name);
                Session["Memberid"] = strMemberID;
                string checkid = balayer.GetSingleValue("Select Count(*) from membersdocuments where MemberID ='" + strMemberID + "'");

                if (checkid!="0")
                {
                    var mm = balayer.GetDataTable("SELECT * FROM svcf.membersdocuments where MemberID='"+ strMemberID + "';");

                    if (mm.Rows.Count!=0)
                    {
                        if (mm.Rows[0]["Photo"] != System.DBNull.Value)
                        {
                        var rr =(byte[]) mm.Rows[0]["Photo"];
                        //Image.FromStream(rr);
                        //string ds = Convert.ToBase64String(mm);
                        Image1.ImageUrl = "data:image;base64," + Convert.ToBase64String(rr); 
                    }
                }
                }
                objCOM.DtBind.Columns.Add("BranchName");
                objCOM.DtBind.Columns.Add("ChitNo");
                objCOM.DtBind.Columns.Add("status");
                objCOM.DtBind.Columns.Add("Credit");
                objCOM.DtBind.Columns.Add("Debit" );
                objCOM.DtBind.Columns.Add("ExcessRemittance");
                objCOM.DtBind.Columns.Add("PArrier");
                objCOM.DtBind.Columns.Add("NPArrier");
                objCOM.DtBind.Columns.Add("Interest");
                objCOM.DtBind.Columns.Add("Runningcall");
                objCOM.DtBind.Columns.Add("AuctionDate");
                objCOM.DtBind.Columns.Add("DrawNo");
                objCOM.DtBind.Columns.Add("Auctionamount");
                objCOM.DtBind.Columns.Add("PaymentDate");
                objCOM.DtBind.Columns.Add("GuarantorName");
                objCOM.DtBind.Columns.Add("documentno");
                ResetTrans();
                pnlTransactions.Visible = true;
                TIChit.Visible = false;
                //string strMemberID = balayer.ToobjectstrEvenNull(e.Item.Name);
                string mob = balayer.GetSingleValue("SELECT MobileNo FROM svcf.membermaster where MemberIDNew='"+ strMemberID + "';");
                dxpcMember.ActiveTabIndex = 1;
                string splitdata = (e.Item.Description).Split('^')[0];
                lblCustomerName.Text = "<b>   Customer Name    : </b>" + e.Item.Text;
                lblCustomerName0.Text ="<b>   Customer No      : </b>" +strMemberID;
              lblCustomerAdress.Text = "<b>   Customer Address : </b>" + e.Item.Description;
                lblAadharNumber.Text = "<b>   AadharNumber     : </b>" + (e.Item.Description).Split('^')[1];
               lblAadharNumber0.Text = "<b>   MobileNumber     : </b>" + mob;
                DataTable dtM = null;
                if (strMemberID != "")
                {
                    MysqlDChit.ConnectionString = CommonClassFile.ConnectionString;
                    MysqlDChit.SelectCommand = "SELECT GroupID,GrpMemberID,Head_ID FROM `svcf`.`membertogroupmaster`  where memberID=" + strMemberID;
                    dtM = balayer.GetDataTable("SELECT GroupID,GrpMemberID,Head_ID FROM `svcf`.`membertogroupmaster`  where memberID=" + strMemberID);
                    string strGroupMemberID = string.Empty;
                    if (dtM.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtM.Rows.Count; i++)
                        {
                            if (string.IsNullOrEmpty(strGroupMemberID))
                            {
                                strGroupMemberID = balayer.ToobjectstrEvenNull(dtM.Rows[i]["Head_ID"]);
                            }
                            else
                            {
                                strGroupMemberID = strGroupMemberID + "," + balayer.ToobjectstrEvenNull(dtM.Rows[i]["Head_ID"]);
                            }
                        }
                    }

                    string strGroupID = balayer.ToobjectstrEvenNull(dtM.Rows[0]["GroupID"]);
                    string dd = DateTime.Now.ToString("yyyy-MM-dd");

                    var temp = "";
                    var temp1 = "";
                    DataTable s = balayer.GetDataTable("select distinct `m1`.`GroupID`,`m1`.BranchID,`m1`.`MemberID` from `membertogroupmaster` `m1` where `m1`.`Head_Id` in(" + strGroupMemberID + ") order by `m1`.BranchID");
                    for(int i=0;i<s.Rows.Count;)
                    {
                        //temp= s.Rows[i]["BranchID"].ToString();
                        //if (temp!=temp1)
                        //{
                        //    objCOM.DrBind = objCOM.DtBind.NewRow();
                        //    var brr= balayer.GetSingleValue("SELECT Node FROM svcf.headstree where NodeID ='" + temp + "'");
                        //    objCOM.DrBind["ChitNo"] = brr.ToString() ;
                        //    objCOM.DrBind["status"] = "";
                        //    objCOM.DrBind["Credit"] = "";
                        //    objCOM.DrBind["Debit"] = "";
                        //    objCOM.DrBind["ExcessRemittance"] = "";


                        //    objCOM.DrBind["PArrier"] = "";
                        //    objCOM.DrBind["NPArrier"] = "";
                        //    objCOM.DrBind["Runningcall"] = "";
                        //    objCOM.DrBind["AuctionDate"] = "";
                        //    objCOM.DrBind["DrawNo"] = "";
                        //    objCOM.DrBind["Auctionamount"] = "";
                        //    objCOM.DrBind["PaymentDate"] = "";
                        //    objCOM.DrBind["GuarantorName"] = "";
                        //    objCOM.DrBind["documentno"] = "";


                        //    objCOM.DtBind.Rows.Add(objCOM.DrBind.ItemArray);
                        //    temp1 = s.Rows[i]["BranchID"].ToString();
                        //}
                        //else
                        //{
                        temp= s.Rows[i]["BranchID"].ToString();
                            string s1 = s.Rows[i]["GroupID"].ToString();
                            string s2 = s.Rows[i]["MemberID"].ToString();
                            string sss = balayer.GetSingleValue("select sum(CurrentDueAmount) from auctiondetails where GroupID='" + s1 + "' and AuctionDate<='" + dd + "';");
                            string strQuery = @"SELECT (select documentno from `svcf`.`documentdetails` where '" + s2 + "'=`documentdetails`.`prizedmemberid` and '" + s1 + "'=`documentdetails`.`groupid` limit 1) as documentno,(SELECT max(`auctiondetails`.`DrawNO`) as `RunningCall` FROM svcf.groupmaster join auctiondetails on (`groupmaster`.`Head_Id`=`auctiondetails`.`GroupID`) join `branchdetails` on (`groupmaster`.`BranchID`=`branchdetails`.`Head_Id`) where `groupmaster`.`Head_Id`='" + s1 + "' and `auctiondetails`.`AuctionDate`<='" + dd + "' ) as 'RunningCall',b1.B_Name as `BranchName`,CASE WHEN ChitEndDate > '" + dd + "' THEN 'R' WHEN ChitEndDate = '" + dd + "' THEN 'R'  ELSE 'T' end as status,(select guaranteer from `svcf`.`documentdetails` where '" + s2 + "'=`documentdetails`.`prizedmemberid` and '" + s1 + "'=`documentdetails`.`groupid` limit 1) as GuarantorName,tp1.DrawNo as DrawNo,tp1.PrizedAmount as AuctionAmount  ,date_format(tp1.PaymentDate,'%d/%m/%Y') as PaymentDate,date_format(a1.AuctionDate,'%d/%m/%Y') as AuctionDate,gm1.GROUPNO as `GroupNo`,gm1.ChitValue, ht1.Node  as `ChitNo`,m1.MemberName,sum(case when (v1.Voucher_Type='C' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) -(case when (v1.Voucher_Type='D' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) as paidAmount,(case when( ((sum(case when (v1.Voucher_Type='C' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) -(case when (v1.Voucher_Type='D' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end))-'" + sss + "') >0) then  ((sum(case when (v1.Voucher_Type='C' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) -(case when (v1.Voucher_Type='D' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end))-'" + sss + "') else 0.00 end) as `ExcessRemittance`,(case when( (a1.AuctionDate>'2013/03/01' or a1.AuctionDate is null) and (tp1.PaymentDate is null or tp1.PaymentDate>'" + DateTime.Now.ToString("yyyy/MM/dd") + "' ) and(('" + sss + "'-(sum(case when (v1.Voucher_Type='C' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) -(case when (v1.Voucher_Type='D' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end)))>0.00 ) ) then ('" + sss + "'-(sum(case when (v1.Voucher_Type='C' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) -(case when (v1.Voucher_Type='D' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end))) else 0.00 end) as `NPArrier`,(case when( ( a1.AuctionDate is not null and (a1.AuctionDate<'2013/03/01' or (tp1.PaymentDate<'" + DateTime.Now.ToString("yyyy/MM/dd") + "' )) ) and(('" + sss + "'-(sum(case when (v1.Voucher_Type='C' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) -(case when (v1.Voucher_Type='D' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end)))>0.00 ) ) then ('" + sss + "'-(sum(case when (v1.Voucher_Type='C' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) -(case when (v1.Voucher_Type='D' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end))) else 0.00 end) as `PArrier`,(case when( (a1.AuctionDate>'2013/03/01' or a1.AuctionDate is null) and (tp1.PaymentDate is null or tp1.PaymentDate>'" + DateTime.Now.ToString("yyyy/MM/dd") + "' ) ) then sum((case when (v1.Voucher_Type='C' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) -(case when (v1.Voucher_Type='D' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end)) else 0.00 end) as Credit, (case when((case when( a1.AuctionDate is not null and (a1.AuctionDate<'2013/03/01' or (tp1.PaymentDate<'" + DateTime.Now.ToString("yyyy/MM/dd") + "' )) ) then sum((case when (v1.Voucher_Type='C' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) -(case when (v1.Voucher_Type='D' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end)) else 0.00 end) <>0.00 ) then gm1.ChitValue-(case when( a1.AuctionDate is not null and (a1.AuctionDate<'2013/03/01' or (tp1.PaymentDate<'" + DateTime.Now.ToString("yyyy/MM/dd") + "' )) ) then sum((case when (v1.Voucher_Type='C' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) -(case when (v1.Voucher_Type='D' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end)) else 0.00 end) else 0.00 end) as Debit, (case when( (a1.AuctionDate>'2013/03/01' or a1.AuctionDate is null) and (tp1.PaymentDate is null or tp1.PaymentDate>'" + DateTime.Now.ToString("yyyy/MM/dd") + "' ) ) then sum((case when (v1.Voucher_Type='C' and v1.Other_Trans_Type=5 ) then v1.Amount else 0.00 end) -(case when (v1.Voucher_Type='D' and v1.Other_Trans_Type=5 ) then v1.Amount else 0.00 end)) else 0.00 end) as `NPKasar` ,(case when( a1.AuctionDate is not null and (a1.AuctionDate<'2013/03/01' or tp1.PaymentDate<'" + DateTime.Now.ToString("yyyy/MM/dd") + "' ) ) then  (sum(case when (v1.Voucher_Type='C' and v1.Other_Trans_Type=5 ) then v1.Amount else 0.00 end)) else 0.00 end) as `PKasar`,m1.Head_ID FROM svcf.voucher as v1 join headstree as ht1 on ht1.NodeID=v1.Head_Id left join trans_payment as tp1 on v1.Head_Id=tp1.TokenNumber left join auctiondetails as a1 on v1.Head_Id =a1.PrizedMemberID join `groupmaster` `gm1` ON " + s1 + " = `gm1`.`Head_Id` join `membertogroupmaster` `m1` on (`v1`.`Head_Id`=`m1`.`Head_Id`) join `branchdetails` `b1` on (`v1`.`BranchID`=`b1`.`Head_Id`) where v1.RootID=5 and v1.Head_Id in (" + strGroupMemberID + ") and ht1.ParentID=" + s1 + " and v1.ChoosenDate<= '" + DateTime.Now.ToString("yyyy/MM/dd") + "' group by v1.Head_Id  order by cast(digits(ht1.Node) as unsigned) ;";
                            DataTable dtTrans = balayer.GetDataTable(strQuery);

                        string Due = "";
                        string maxDraw = "";
                        string amount = "";
                        decimal adddueamount;
                        int paidInstNo=0;
                        int diffInst=0;
                        int totalInst = 0;
                        string IsPrized = "";
                        decimal interest = 0;
                        decimal payInterest = 0;
                        DataTable getAllAuction = new DataTable();

                            for (int j = 0; j < dtTrans.Rows.Count; j++)
                            {
                                //if (dtTrans.Rows.Count!=0)
                                //{
                                objCOM.DrBind = objCOM.DtBind.NewRow();
                            var brr = balayer.GetSingleValue("SELECT Node FROM svcf.headstree where NodeID ='" + temp + "'");
                            objCOM.DrBind["BranchName"] = brr.ToString();
                                objCOM.DrBind["ChitNo"] = dtTrans.Rows[j]["ChitNo"].ToString();
                                objCOM.DrBind["status"] = dtTrans.Rows[j]["status"].ToString();
                                objCOM.DrBind["Credit"] = dtTrans.Rows[j]["Credit"].ToString();
                                objCOM.DrBind["Debit"] = dtTrans.Rows[j]["Debit"].ToString();

                            //objCOM.Excess = balayer.GetSingleValue(@"select  (case when( (sum(case when (v1.Voucher_Type='C' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) -sum(case when (v1.Voucher_Type='D' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) -'" + sss + "')>0.00) then (sum(case when (v1.Voucher_Type='C' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) -sum(case when (v1.Voucher_Type='D' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) -'" + sss + "') else 0.00 end) as ExcessRemittance from membertogroupmaster as mg1 join voucher as v1 on v1.Head_Id=mg1.Head_Id join groupmaster as vgwd1 on vgwd1.`Head_Id`=" + s1 + " left join trans_payment as tp1 on v1.Head_Id =tp1.TokenNumber join membermaster as mm on (mg1.MemberID=mm.MemberIDNew) where mg1.BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and mg1.GroupID=" + s1 + " and v1.Head_Id=" + dtTrans.Rows[j]["Head_ID"] + " and v1.ChoosenDate<= '" + DateTime.Now.ToString("yyyy/MM/dd") + "' group by v1.Head_Id order by cast(digits(mg1.GrpMemberID) as unsigned),v1.Voucher_Type DESC ;");
                            objCOM.Excess = balayer.GetSingleValue(@"select  (case when( (sum(case when (v1.Voucher_Type='C' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) -sum(case when (v1.Voucher_Type='D' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) -'" + sss + "')>0.00) then (sum(case when (v1.Voucher_Type='C' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) -sum(case when (v1.Voucher_Type='D' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) -'" + sss + "') else 0.00 end) as ExcessRemittance from membertogroupmaster as mg1 join voucher as v1 on v1.Head_Id=mg1.Head_Id join groupmaster as vgwd1 on vgwd1.`Head_Id`=" + s1 + " left join trans_payment as tp1 on v1.Head_Id =tp1.TokenNumber join membermaster as mm on (mg1.MemberID=mm.MemberIDNew) where mg1.B_Id=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and mg1.GroupID=" + s1 + " and v1.Head_Id=" + dtTrans.Rows[j]["Head_ID"] + " and v1.ChoosenDate<= '" + DateTime.Now.ToString("yyyy/MM/dd") + "' group by v1.Head_Id order by cast(digits(mg1.GrpMemberID) as unsigned),v1.Voucher_Type DESC ;");
                            if (string.IsNullOrEmpty(objCOM.Excess))
                                {
                                    objCOM.DrBind["ExcessRemittance"] = "0.00";
                                }
                                else
                                {
                                    objCOM.DrBind["ExcessRemittance"] = objCOM.Excess + ".00";
                                }
                            //objCOM.Parr = balayer.GetSingleValue(@"select (case when( tp1.PaymentDate<='" + DateTime.Now.ToString("yyyy/MM/dd") + "' ) then (case when( (" + sss + "-sum(case when (v1.Voucher_Type='C' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) +sum(case when (v1.Voucher_Type='D' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) )>0.00) then (" + sss + "-sum(case when (v1.Voucher_Type='C' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) +sum(case when (v1.Voucher_Type='D' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) ) else 0.00 end) else 0.00 end ) as PArrier from membertogroupmaster as mg1 join voucher as v1 on v1.Head_Id=mg1.Head_Id join groupmaster as vgwd1 on vgwd1.`Head_Id`=" + s1 + " left join trans_payment as tp1 on v1.Head_Id =tp1.TokenNumber join membermaster as mm on (mg1.MemberID=mm.MemberIDNew) where mg1.BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and mg1.GroupID=" + s1 + " and v1.Head_Id=" + dtTrans.Rows[j]["Head_ID"] + " and v1.ChoosenDate<= '" + DateTime.Now.ToString("yyyy/MM/dd") + "' group by v1.Head_Id order by cast(digits(mg1.GrpMemberID) as unsigned),v1.Voucher_Type DESC ;");
                            objCOM.Parr = balayer.GetSingleValue(@"select (case when( tp1.PaymentDate<='" + DateTime.Now.ToString("yyyy/MM/dd") + "' ) then (case when( (" + sss + "-sum(case when (v1.Voucher_Type='C' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) +sum(case when (v1.Voucher_Type='D' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) )>0.00) then (" + sss + "-sum(case when (v1.Voucher_Type='C' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) +sum(case when (v1.Voucher_Type='D' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) ) else 0.00 end) else 0.00 end ) as PArrier from membertogroupmaster as mg1 join voucher as v1 on v1.Head_Id=mg1.Head_Id join groupmaster as vgwd1 on vgwd1.`Head_Id`=" + s1 + " left join trans_payment as tp1 on v1.Head_Id =tp1.TokenNumber join membermaster as mm on (mg1.MemberID=mm.MemberIDNew) where mg1.B_Id=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and mg1.GroupID=" + s1 + " and v1.Head_Id=" + dtTrans.Rows[j]["Head_ID"] + " and v1.ChoosenDate<= '" + DateTime.Now.ToString("yyyy/MM/dd") + "' group by v1.Head_Id order by cast(digits(mg1.GrpMemberID) as unsigned),v1.Voucher_Type DESC ;");
                            if (string.IsNullOrEmpty(objCOM.Parr))
                                {
                                    objCOM.DrBind["PArrier"] = "0.00";
                                }
                                else
                                {
                                    objCOM.DrBind["PArrier"] = objCOM.Parr + ".00"; 
                                }
                            //objCOM.Nparr = balayer.GetSingleValue(@"select (case when( tp1.PaymentDate is null  or tp1.PaymentDate >'" + DateTime.Now.ToString("yyyy/MM/dd") + "') then (case when( (" + sss + "-sum(case when (v1.Voucher_Type='C' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) +sum(case when (v1.Voucher_Type='D' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) )>0.00) then (" + sss + "-sum(case when (v1.Voucher_Type='C' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) +sum(case when (v1.Voucher_Type='D' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) ) else 0.00 end) else 0.00 end) as NPArrier from membertogroupmaster as mg1 join voucher as v1 on v1.Head_Id=mg1.Head_Id join groupmaster as vgwd1 on vgwd1.`Head_Id`=" + s1 + " left join trans_payment as tp1 on v1.Head_Id =tp1.TokenNumber join membermaster as mm on (mg1.MemberID=mm.MemberIDNew) where mg1.BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and mg1.GroupID=" + s1 + " and v1.Head_Id=" + dtTrans.Rows[j]["Head_ID"]  + " and v1.ChoosenDate<= '" + DateTime.Now.ToString("yyyy/MM/dd") + "' group by v1.Head_Id order by cast(digits(mg1.GrpMemberID) as unsigned),v1.Voucher_Type DESC ;");
                            objCOM.Nparr = balayer.GetSingleValue(@"select (case when( tp1.PaymentDate is null  or tp1.PaymentDate >'" + DateTime.Now.ToString("yyyy/MM/dd") + "') then (case when( (" + sss + "-sum(case when (v1.Voucher_Type='C' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) +sum(case when (v1.Voucher_Type='D' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) )>0.00) then (" + sss + "-sum(case when (v1.Voucher_Type='C' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) +sum(case when (v1.Voucher_Type='D' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) ) else 0.00 end) else 0.00 end) as NPArrier from membertogroupmaster as mg1 join voucher as v1 on v1.Head_Id=mg1.Head_Id join groupmaster as vgwd1 on vgwd1.`Head_Id`=" + s1 + " left join trans_payment as tp1 on v1.Head_Id =tp1.TokenNumber join membermaster as mm on (mg1.MemberID=mm.MemberIDNew) where mg1.B_Id=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and mg1.GroupID=" + s1 + " and v1.Head_Id=" + dtTrans.Rows[j]["Head_ID"] + " and v1.ChoosenDate<= '" + DateTime.Now.ToString("yyyy/MM/dd") + "' group by v1.Head_Id order by cast(digits(mg1.GrpMemberID) as unsigned),v1.Voucher_Type DESC ;");
                            if (string.IsNullOrEmpty(objCOM.Nparr))
                                {
                                    objCOM.Dtnullck = balayer.GetDataTable(@"SELECT * FROM voucher WHERE Head_Id = " + dtTrans.Rows[j]["Head_ID"] + " and ChoosenDate <= '" + DateTime.Now.ToString("yyyy/MM/dd") + "';");
                                    if (objCOM.Dtnullck.Rows.Count <= 0)
                                    {
                                        objCOM.DrBind["NPArrier"] = sss + ".00";
                                    }
                                }
                                else
                                {
                                    objCOM.DrBind["NPArrier"] = objCOM.Nparr + ".00";
                                }

                            //12/01/2021
                            #region Inerest Calculation
                            Due = balayer.GetSingleValue("Select currentdueamount from auctiondetails where GroupID=" + s1 + " and DrawNO=1 and AuctionDate<='" + DateTime.Now.ToString("yyyy/MM/dd") + "'");
                            maxDraw = balayer.GetSingleValue("select max(DrawNO) from svcf.auctiondetails where GroupID=" + s1 + " and AuctionDate<='" + DateTime.Now.ToString("yyyy/MM/dd") + "'");

                            amount = balayer.GetSingleValue("Select coalesce((Select sum(Amount) from voucher where Head_Id=" + dtTrans.Rows[j]["Head_ID"] + " and Voucher_Type='C' and ChoosenDate<='" + DateTime.Now.ToString("yyyy/MM/dd") + "' and Other_Trans_Type<>5),0)"
                                + "-coalesce((select sum(amount) from voucher where Head_Id=" + dtTrans.Rows[j]["Head_ID"] + " and Voucher_Type='D' and Trans_Type=0 and ChoosenDate<='" + DateTime.Now.ToString("yyyy/MM/dd") + "' and Other_Trans_Type<>5),0) as amount");

                            getAllAuction = balayer.GetDataTable("Select DrawNO,coalesce(CurrentDueAmount,0) as CurrentDueAmount from auctiondetails where GroupID=" + s1 + " and PrizedMemberID<>0 and AuctionDate<='" + DateTime.Now.ToString("yyyy/MM/dd") + "'");
                            IsPrized = balayer.GetSingleValue("select IsPrized from auctiondetails where PrizedMemberID=" + dtTrans.Rows[j]["Head_ID"] + " and GroupID=" + s1);

                            adddueamount = 0;
                            for(int amt=0;amt<getAllAuction.Rows.Count;amt++)
                            {
                                if(Convert.ToString(getAllAuction.Rows[amt]["CurrentDueAmount"])!="0.00")
                                {
                                    if (adddueamount == 0)
                                        adddueamount = Convert.ToDecimal(getAllAuction.Rows[amt]["CurrentDueAmount"]);
                                    else
                                        adddueamount = adddueamount + Convert.ToDecimal(getAllAuction.Rows[amt]["CurrentDueAmount"]);

                                    if(Convert.ToDecimal(amount)==adddueamount)
                                    {
                                        paidInstNo = Convert.ToInt32(getAllAuction.Rows[amt]["DrawNO"]);
                                        if(paidInstNo != getAllAuction.Rows.Count)
                                        {
                                            diffInst = getAllAuction.Rows.Count - paidInstNo;
                                            totalInst = (diffInst * (diffInst + 1)) / 2;
                                            if (diffInst > 1)
                                            {
                                                
                                                if(string.IsNullOrEmpty(IsPrized) || IsPrized !="Y")
                                                {
                                                    interest = ((Convert.ToDecimal(Due) * 12) / 100);
                                                    payInterest = (interest / 12) * (totalInst-1);
                                                }
                                                else if(IsPrized=="Y")
                                                {
                                                    interest = ((Convert.ToDecimal(Due) * 24) / 100);
                                                    payInterest = (interest / 12) * (totalInst-1);
                                                }
                                            }
                                        }
                                        break;
                                    }
                                    if(Convert.ToDecimal(amount)< adddueamount)
                                    {
                                        paidInstNo = Convert.ToInt32(getAllAuction.Rows[amt]["DrawNO"]) - 1;
                                        diffInst = getAllAuction.Rows.Count - paidInstNo;
                                        totalInst = (diffInst * (diffInst + 1)) / 2;
                                        if (diffInst > 1)
                                        {

                                            if (string.IsNullOrEmpty(IsPrized) || IsPrized != "Y")
                                            {
                                                interest = ((Convert.ToDecimal(Due) * 12) / 100);
                                                payInterest = (interest / 12) * (totalInst-1);
                                            }
                                            else if (IsPrized == "Y")
                                            {
                                                interest = ((Convert.ToDecimal(Due) * 24) / 100);
                                                payInterest = (interest / 12) * (totalInst-1);
                                            }
                                        }
                                        break;
                                    }
                                }
                            }


                            #endregion
                            //objCOM.DrBind["Interest"] = "0.00";//11/01/2021
                            objCOM.DrBind["Interest"] = payInterest;
                                objCOM.DrBind["Runningcall"] = dtTrans.Rows[j]["Runningcall"].ToString();
                                objCOM.DrBind["AuctionDate"] = dtTrans.Rows[j]["AuctionDate"].ToString();
                                objCOM.DrBind["DrawNo"] = dtTrans.Rows[j]["DrawNo"].ToString();
                                objCOM.DrBind["Auctionamount"] = dtTrans.Rows[j]["Auctionamount"].ToString();
                                objCOM.DrBind["PaymentDate"] = dtTrans.Rows[j]["PaymentDate"].ToString();
                                objCOM.DrBind["GuarantorName"] = dtTrans.Rows[j]["GuarantorName"].ToString();
                                objCOM.DrBind["documentno"] = dtTrans.Rows[j]["documentno"].ToString();


                                objCOM.DtBind.Rows.Add(objCOM.DrBind.ItemArray);
                            }
                          //  }
                            i++;
                        

                    }

                    objCOM.Dv = objCOM.DtBind.DefaultView;
              //      objCOM.Dv.Sort = "ChitNo1 asc";

                    objCOM.Dv.Sort = "BranchName asc,status desc";

                    objCOM.SortedDT = objCOM.Dv.ToTable();
                    gridViewAll.DataSource = objCOM.SortedDT;
                    gridViewAll.DataBind();
                }

                if (dtM.Rows.Count == 1)
                {
                    string strGroupID = balayer.ToobjectstrEvenNull(dtM.Rows[0]["GroupID"]);
                    string strGroupMemberID = balayer.ToobjectstrEvenNull(dtM.Rows[0]["Head_ID"]);
                    select(strGroupMemberID);
                    //string strGroupID=  TIChit.Items[0].Description;
                    //string strGroupMemberID = TIChit.Items[0].Name;
                    // string strQuery = @"SELECT b1.B_Name as `BranchName`,tp1.PaymentDate,a1.AuctionDate,gm1.GROUPNO as `GroupNo`,gm1.ChitValue, ht1.Node  as `ChitNo`,m1.MemberName,sum(case when (v1.Voucher_Type='C' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) -(case when (v1.Voucher_Type='D' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) as paidAmount,(case when( ((sum(case when (v1.Voucher_Type='C' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) -(case when (v1.Voucher_Type='D' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end))-'"+sss+"') >0) then  ((sum(case when (v1.Voucher_Type='C' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) -(case when (v1.Voucher_Type='D' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end))-'"+sss+"') else 0.00 end) as `ExcessRemittance`,(case when( (a1.AuctionDate>'2013/03/01' or a1.AuctionDate is null) and (tp1.PaymentDate is null or tp1.PaymentDate>'" + DateTime.Now.ToString("yyyy/MM/dd") + "' ) and(('"+sss+"'-(sum(case when (v1.Voucher_Type='C' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) -(case when (v1.Voucher_Type='D' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end)))>0.00 ) ) then ('"+sss+"'-(sum(case when (v1.Voucher_Type='C' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) -(case when (v1.Voucher_Type='D' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end))) else 0.00 end) as `NPArrier`,(case when( ( a1.AuctionDate is not null and (a1.AuctionDate<'2013/03/01' or (tp1.PaymentDate<'" + DateTime.Now.ToString("yyyy/MM/dd") + "' )) ) and(('"+sss+"'-(sum(case when (v1.Voucher_Type='C' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) -(case when (v1.Voucher_Type='D' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end)))>0.00 ) ) then ('"+sss+"'-(sum(case when (v1.Voucher_Type='C' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) -(case when (v1.Voucher_Type='D' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end))) else 0.00 end) as `PArrier`,(case when( (a1.AuctionDate>'2013/03/01' or a1.AuctionDate is null) and (tp1.PaymentDate is null or tp1.PaymentDate>'" + DateTime.Now.ToString("yyyy/MM/dd") + "' ) ) then sum((case when (v1.Voucher_Type='C' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) -(case when (v1.Voucher_Type='D' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end)) else 0.00 end) as Credit, (case when((case when( a1.AuctionDate is not null and (a1.AuctionDate<'2013/03/01' or (tp1.PaymentDate<'" + DateTime.Now.ToString("yyyy/MM/dd") + "' )) ) then sum((case when (v1.Voucher_Type='C' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) -(case when (v1.Voucher_Type='D' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end)) else 0.00 end) <>0.00 ) then gm1.ChitValue-(case when( a1.AuctionDate is not null and (a1.AuctionDate<'2013/03/01' or (tp1.PaymentDate<'" + DateTime.Now.ToString("yyyy/MM/dd") + "' )) ) then sum((case when (v1.Voucher_Type='C' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) -(case when (v1.Voucher_Type='D' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end)) else 0.00 end) else 0.00 end) as Debit, (case when( (a1.AuctionDate>'2013/03/01' or a1.AuctionDate is null) and (tp1.PaymentDate is null or tp1.PaymentDate>'" + DateTime.Now.ToString("yyyy/MM/dd") + "' ) ) then sum((case when (v1.Voucher_Type='C' and v1.Other_Trans_Type=5 ) then v1.Amount else 0.00 end) -(case when (v1.Voucher_Type='D' and v1.Other_Trans_Type=5 ) then v1.Amount else 0.00 end)) else 0.00 end) as `NPKasar` ,(case when( a1.AuctionDate is not null and (a1.AuctionDate<'2013/03/01' or tp1.PaymentDate<'" + DateTime.Now.ToString("yyyy/MM/dd") + "' ) ) then  (sum(case when (v1.Voucher_Type='C' and v1.Other_Trans_Type=5 ) then v1.Amount else 0.00 end)) else 0.00 end) as `PKasar` FROM svcf.voucher as v1 join headstree as ht1 on ht1.NodeID=v1.Head_Id left join trans_payment as tp1 on v1.Head_Id=tp1.TokenNumber left join auctiondetails as a1 on v1.Head_Id =a1.PrizedMemberID join `groupmaster` `gm1` ON " + strGroupID + " = `gm1`.`Head_Id` join `view_groupwisedue` as vgwd on " + strGroupID + "=vgwd.GroupId join `membertogroupmaster` `m1` on (`v1`.`Head_Id`=`m1`.`Head_Id`) join `branchdetails` `b1` on (`v1`.`BranchID`=`b1`.`Head_Id`) where v1.RootID=5 and v1.Head_Id  =" + strGroupMemberID + " and ht1.ParentID=" + strGroupID + " and v1.ChoosenDate<= '" + DateTime.Now.ToString("yyyy/MM/dd") + "' group by v1.Head_Id  order by cast(digits(ht1.Node) as unsigned) ;";

                    string dd = DateTime.Now.ToString("yyyy-MM-dd");
                    string sss = balayer.GetSingleValue("select sum(CurrentDueAmount) from auctiondetails where GroupID='" + strGroupID + "' and AuctionDate<='" + dd + "';");

                    string strQuery = @"SELECT b1.B_Name as `BranchName`,tp1.GuarantorName as GuarantorName,tp1.DrawNo as DrawNo,tp1.PrizedAmount  ,tp1.PaymentDate as PaymentDate,a1.AuctionDate as AuctionDate,gm1.GROUPNO as `GroupNo`,gm1.ChitValue, ht1.Node  as `ChitNo`,m1.MemberName,sum(case when (v1.Voucher_Type='C' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) -(case when (v1.Voucher_Type='D' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) as paidAmount,(case when( ((sum(case when (v1.Voucher_Type='C' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) -(case when (v1.Voucher_Type='D' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end))-'"+sss+"') >0) then  ((sum(case when (v1.Voucher_Type='C' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) -(case when (v1.Voucher_Type='D' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end))-'"+sss+"') else 0.00 end) as `ExcessRemittance`,(case when( (a1.AuctionDate>'2013/03/01' or a1.AuctionDate is null) and (tp1.PaymentDate is null or tp1.PaymentDate>'" + DateTime.Now.ToString("yyyy/MM/dd") + "' ) and(('"+sss+"'-(sum(case when (v1.Voucher_Type='C' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) -(case when (v1.Voucher_Type='D' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end)))>0.00 ) ) then ('"+sss+"'-(sum(case when (v1.Voucher_Type='C' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) -(case when (v1.Voucher_Type='D' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end))) else 0.00 end) as `NPArrier`,(case when( ( a1.AuctionDate is not null and (a1.AuctionDate<'2013/03/01' or (tp1.PaymentDate<'" + DateTime.Now.ToString("yyyy/MM/dd") + "' )) ) and(('"+sss+"'-(sum(case when (v1.Voucher_Type='C' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) -(case when (v1.Voucher_Type='D' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end)))>0.00 ) ) then ('"+sss+"'-(sum(case when (v1.Voucher_Type='C' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) -(case when (v1.Voucher_Type='D' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end))) else 0.00 end) as `PArrier`,(case when( (a1.AuctionDate>'2013/03/01' or a1.AuctionDate is null) and (tp1.PaymentDate is null or tp1.PaymentDate>'" + DateTime.Now.ToString("yyyy/MM/dd") + "' ) ) then sum((case when (v1.Voucher_Type='C' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) -(case when (v1.Voucher_Type='D' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end)) else 0.00 end) as Credit, (case when((case when( a1.AuctionDate is not null and (a1.AuctionDate<'2013/03/01' or (tp1.PaymentDate<'" + DateTime.Now.ToString("yyyy/MM/dd") + "' )) ) then sum((case when (v1.Voucher_Type='C' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) -(case when (v1.Voucher_Type='D' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end)) else 0.00 end) <>0.00 ) then gm1.ChitValue-(case when( a1.AuctionDate is not null and (a1.AuctionDate<'2013/03/01' or (tp1.PaymentDate<'" + DateTime.Now.ToString("yyyy/MM/dd") + "' )) ) then sum((case when (v1.Voucher_Type='C' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) -(case when (v1.Voucher_Type='D' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end)) else 0.00 end) else 0.00 end) as Debit, (case when( (a1.AuctionDate>'2013/03/01' or a1.AuctionDate is null) and (tp1.PaymentDate is null or tp1.PaymentDate>'" + DateTime.Now.ToString("yyyy/MM/dd") + "' ) ) then sum((case when (v1.Voucher_Type='C' and v1.Other_Trans_Type=5 ) then v1.Amount else 0.00 end) -(case when (v1.Voucher_Type='D' and v1.Other_Trans_Type=5 ) then v1.Amount else 0.00 end)) else 0.00 end) as `NPKasar` ,(case when( a1.AuctionDate is not null and (a1.AuctionDate<'2013/03/01' or tp1.PaymentDate<'" + DateTime.Now.ToString("yyyy/MM/dd") + "' ) ) then  (sum(case when (v1.Voucher_Type='C' and v1.Other_Trans_Type=5 ) then v1.Amount else 0.00 end)) else 0.00 end) as `PKasar` FROM svcf.voucher as v1 join headstree as ht1 on ht1.NodeID=v1.Head_Id left join trans_payment as tp1 on v1.Head_Id=tp1.TokenNumber left join auctiondetails as a1 on v1.Head_Id =a1.PrizedMemberID join `groupmaster` `gm1` ON " + strGroupID + " = `gm1`.`Head_Id` join `view_groupwisedue` as vgwd on " + strGroupID + "=vgwd.GroupId join `membertogroupmaster` `m1` on (`v1`.`Head_Id`=`m1`.`Head_Id`) join `branchdetails` `b1` on (`v1`.`BranchID`=`b1`.`Head_Id`) where v1.RootID=5 and v1.Head_Id  =" + strGroupMemberID + " and ht1.ParentID=" + strGroupID + " and v1.ChoosenDate<= '" + DateTime.Now.ToString("yyyy/MM/dd") + "' group by v1.Head_Id  order by cast(digits(ht1.Node) as unsigned) ;";


                    decimal NPArrier = 0.00M;
                    decimal PArrier = 0.00M;
                    decimal ExcessRemittance = 0.00M;
                    decimal Debit = 0.00M;
                    decimal Credit = 0.00M;

                    string GuarantorName = string.Empty;
                    string DrawNo = String.Empty;
                    string PaymentDate = string.Empty;
                    string AuctionDate = string.Empty;



                    DataTable dtTrans = balayer.GetDataTable(strQuery);
                    if (dtTrans.Rows.Count > 0)
                    {
                        decimal.TryParse(balayer.ToobjectstrEvenNull(dtTrans.Rows[0]["ExcessRemittance"]), out ExcessRemittance);
                        decimal.TryParse(balayer.ToobjectstrEvenNull(dtTrans.Rows[0]["Credit"]), out Credit);
                        decimal.TryParse(balayer.ToobjectstrEvenNull(dtTrans.Rows[0]["Debit"]), out Debit);
                        decimal.TryParse(balayer.ToobjectstrEvenNull(dtTrans.Rows[0]["PArrier"]), out PArrier);
                        decimal.TryParse(balayer.ToobjectstrEvenNull(dtTrans.Rows[0]["NPArrier"]), out NPArrier);

                        string gname = (balayer.ToobjectstrEvenNull(dtTrans.Rows[0]["GuarantorName"]).ToString());
                        string DNo = (balayer.ToobjectstrEvenNull(dtTrans.Rows[0]["DrawNo"]).ToString());
                        string PayDate = (balayer.ToobjectstrEvenNull(dtTrans.Rows[0]["PaymentDate"]).ToString());

                        string AucDate = (balayer.ToobjectstrEvenNull(dtTrans.Rows[0]["AuctionDate"]).ToString());




                        lblCrAmount.Text = Credit.ToString();
                        lblDrAmount.Text = Debit.ToString();
                        lblExcess.Text = ExcessRemittance.ToString();
                        lblPrizedArrier.Text = PArrier.ToString();
                        lblNonPrizedArrier.Text = NPArrier.ToString();
                        lblByNumber.Text = balayer.ToobjectstrEvenNull(dtTrans.Rows[0]["ChitNo"]);
                        lblGuarantorName.Text = gname.ToString();
                        lblDrawNo.Text = DNo.ToString();
                        lblPaymentDate.Text = PayDate.ToString();
                        lblAuctionDate.Text = AucDate.ToString();
                    }
                    TIChit.Visible = false;
                    pnlTransactions.Visible = true;

                }

                if (TIChit.IndexPanel.Characters.Count() == 1)
                {
                    TIChit.IndexPanel.Visible = false;
                }
                TIChit.Visible = false;
                pnlTransactions.Visible = true;

            }
            catch (Exception ex)
            {

                logger.Info("FindCustomerDetails  - titleIndex_ItemClick() - Catch: " + ex.Message + ":" + DateTime.Now + " by: " + Convert.ToString(Session["UserName"]) + "");
            }
        }
        protected void select(string headID)
        {
            AccessDataSource1.ConnectionString = CommonClassFile.ConnectionString;
            AccessDataSource1.SelectCommand = @"select t1.ChoosenDate as `Date`,t8.Node as LedgerHead ,t3.Node as Token ,t1.Narration,(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )as `Credit`,(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )as `Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left Join headstree as t8 on t1.ChitGroupID=t8.NodeID where `t1`.`BranchID` = " + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and `t1`.`Head_Id` =" + headID + " ";
        }
        protected void TIChit_ItemClick(object source, DevExpress.Web.ASPxTitleIndex.TitleIndexItemEventArgs e)
        {
            ResetTrans();
            //  pnlTransactions.Visible = true;
            //string strGroupID = balayer.ToobjectstrEvenNull(dtM.Rows[0]["GroupID"]);
            //string strGroupMemberID = balayer.ToobjectstrEvenNull(dtM.Rows[0]["GrpMemberID"]);
            string strGroupID = e.Item.Description;
            string strGroupMemberID = e.Item.Name;
            select(strGroupMemberID);
            // string strQuery = @"SELECT b1.B_Name as `BranchName`,tp1.PaymentDate,a1.AuctionDate,gm1.GROUPNO as `GroupNo`,gm1.ChitValue, ht1.Node  as `ChitNo`,m1.MemberName,sum(case when (v1.Voucher_Type='C' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) -(case when (v1.Voucher_Type='D' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) as paidAmount,(case when( ((sum(case when (v1.Voucher_Type='C' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) -(case when (v1.Voucher_Type='D' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end))-'"+sss+"') >0) then  ((sum(case when (v1.Voucher_Type='C' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) -(case when (v1.Voucher_Type='D' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end))-'"+sss+"') else 0.00 end) as `ExcessRemittance`,(case when( (a1.AuctionDate>'2013/03/01' or a1.AuctionDate is null) and (tp1.PaymentDate is null or tp1.PaymentDate>'" + DateTime.Now.ToString("yyyy/MM/dd") + "' ) and(('"+sss+"'-(sum(case when (v1.Voucher_Type='C' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) -(case when (v1.Voucher_Type='D' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end)))>0.00 ) ) then ('"+sss+"'-(sum(case when (v1.Voucher_Type='C' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) -(case when (v1.Voucher_Type='D' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end))) else 0.00 end) as `NPArrier`,(case when( ( a1.AuctionDate is not null and (a1.AuctionDate<'2013/03/01' or (tp1.PaymentDate<'" + DateTime.Now.ToString("yyyy/MM/dd") + "' )) ) and(('"+sss+"'-(sum(case when (v1.Voucher_Type='C' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) -(case when (v1.Voucher_Type='D' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end)))>0.00 ) ) then ('"+sss+"'-(sum(case when (v1.Voucher_Type='C' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) -(case when (v1.Voucher_Type='D' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end))) else 0.00 end) as `PArrier`,(case when( (a1.AuctionDate>'2013/03/01' or a1.AuctionDate is null) and (tp1.PaymentDate is null or tp1.PaymentDate>'" + DateTime.Now.ToString("yyyy/MM/dd") + "' ) ) then sum((case when (v1.Voucher_Type='C' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) -(case when (v1.Voucher_Type='D' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end)) else 0.00 end) as Credit, (case when((case when( a1.AuctionDate is not null and (a1.AuctionDate<'2013/03/01' or (tp1.PaymentDate<'" + DateTime.Now.ToString("yyyy/MM/dd") + "' )) ) then sum((case when (v1.Voucher_Type='C' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) -(case when (v1.Voucher_Type='D' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end)) else 0.00 end) <>0.00 ) then gm1.ChitValue-(case when( a1.AuctionDate is not null and (a1.AuctionDate<'2013/03/01' or (tp1.PaymentDate<'" + DateTime.Now.ToString("yyyy/MM/dd") + "' )) ) then sum((case when (v1.Voucher_Type='C' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) -(case when (v1.Voucher_Type='D' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end)) else 0.00 end) else 0.00 end) as Debit, (case when( (a1.AuctionDate>'2013/03/01' or a1.AuctionDate is null) and (tp1.PaymentDate is null or tp1.PaymentDate>'" + DateTime.Now.ToString("yyyy/MM/dd") + "' ) ) then sum((case when (v1.Voucher_Type='C' and v1.Other_Trans_Type=5 ) then v1.Amount else 0.00 end) -(case when (v1.Voucher_Type='D' and v1.Other_Trans_Type=5 ) then v1.Amount else 0.00 end)) else 0.00 end) as `NPKasar` ,(case when( a1.AuctionDate is not null and (a1.AuctionDate<'2013/03/01' or tp1.PaymentDate<'" + DateTime.Now.ToString("yyyy/MM/dd") + "' ) ) then  (sum(case when (v1.Voucher_Type='C' and v1.Other_Trans_Type=5 ) then v1.Amount else 0.00 end)) else 0.00 end) as `PKasar` FROM svcf.voucher as v1 join headstree as ht1 on ht1.NodeID=v1.Head_Id left join trans_payment as tp1 on v1.Head_Id=tp1.TokenNumber left join auctiondetails as a1 on v1.Head_Id =a1.PrizedMemberID join `groupmaster` `gm1` ON " + strGroupID + " = `gm1`.`Head_Id` join `view_groupwisedue` as vgwd on " + strGroupID + "=vgwd.GroupId join `membertogroupmaster` `m1` on (`v1`.`Head_Id`=`m1`.`Head_Id`) join `branchdetails` `b1` on (`v1`.`BranchID`=`b1`.`Head_Id`) where v1.RootID=5 and v1.Head_Id =" + strGroupMemberID + " and ht1.ParentID=" + strGroupID + " and v1.ChoosenDate<= '" + DateTime.Now.ToString("yyyy/MM/dd") + "' group by v1.Head_Id  order by cast(digits(ht1.Node) as unsigned) ;";
            string dd = DateTime.Now.ToString("yyyy-MM-dd");
            string sss = balayer.GetSingleValue("select sum(CurrentDueAmount) from auctiondetails where GroupID='" + strGroupID + "' and AuctionDate<='" + dd + "';");

            string strQuery = @"SELECT b1.B_Name as `BranchName`,tp1.GuarantorName as GuarantorName,tp1.DrawNo as DrawNo,tp1.PrizedAmount  ,tp1.PaymentDate as PaymentDate,a1.AuctionDate as AuctionDate,gm1.GROUPNO as `GroupNo`,gm1.ChitValue, ht1.Node  as `ChitNo`,m1.MemberName,sum(case when (v1.Voucher_Type='C' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) -(case when (v1.Voucher_Type='D' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) as paidAmount,(case when( ((sum(case when (v1.Voucher_Type='C' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) -(case when (v1.Voucher_Type='D' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end))-'"+sss+"') >0) then  ((sum(case when (v1.Voucher_Type='C' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) -(case when (v1.Voucher_Type='D' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end))-'"+sss+"') else 0.00 end) as `ExcessRemittance`,(case when( (a1.AuctionDate>'2013/03/01' or a1.AuctionDate is null) and (tp1.PaymentDate is null or tp1.PaymentDate>'" + DateTime.Now.ToString("yyyy/MM/dd") + "' ) and(('"+sss+"'-(sum(case when (v1.Voucher_Type='C' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) -(case when (v1.Voucher_Type='D' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end)))>0.00 ) ) then ('"+sss+"'-(sum(case when (v1.Voucher_Type='C' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) -(case when (v1.Voucher_Type='D' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end))) else 0.00 end) as `NPArrier`,(case when( ( a1.AuctionDate is not null and (a1.AuctionDate<'2013/03/01' or (tp1.PaymentDate<'" + DateTime.Now.ToString("yyyy/MM/dd") + "' )) ) and(('"+sss+"'-(sum(case when (v1.Voucher_Type='C' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) -(case when (v1.Voucher_Type='D' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end)))>0.00 ) ) then ('"+sss+"'-(sum(case when (v1.Voucher_Type='C' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) -(case when (v1.Voucher_Type='D' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end))) else 0.00 end) as `PArrier`,(case when( (a1.AuctionDate>'2013/03/01' or a1.AuctionDate is null) and (tp1.PaymentDate is null or tp1.PaymentDate>'" + DateTime.Now.ToString("yyyy/MM/dd") + "' ) ) then sum((case when (v1.Voucher_Type='C' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) -(case when (v1.Voucher_Type='D' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end)) else 0.00 end) as Credit, (case when((case when( a1.AuctionDate is not null and (a1.AuctionDate<'2013/03/01' or (tp1.PaymentDate<'" + DateTime.Now.ToString("yyyy/MM/dd") + "' )) ) then sum((case when (v1.Voucher_Type='C' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) -(case when (v1.Voucher_Type='D' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end)) else 0.00 end) <>0.00 ) then gm1.ChitValue-(case when( a1.AuctionDate is not null and (a1.AuctionDate<'2013/03/01' or (tp1.PaymentDate<'" + DateTime.Now.ToString("yyyy/MM/dd") + "' )) ) then sum((case when (v1.Voucher_Type='C' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) -(case when (v1.Voucher_Type='D' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end)) else 0.00 end) else 0.00 end) as Debit, (case when( (a1.AuctionDate>'2013/03/01' or a1.AuctionDate is null) and (tp1.PaymentDate is null or tp1.PaymentDate>'" + DateTime.Now.ToString("yyyy/MM/dd") + "' ) ) then sum((case when (v1.Voucher_Type='C' and v1.Other_Trans_Type=5 ) then v1.Amount else 0.00 end) -(case when (v1.Voucher_Type='D' and v1.Other_Trans_Type=5 ) then v1.Amount else 0.00 end)) else 0.00 end) as `NPKasar` ,(case when( a1.AuctionDate is not null and (a1.AuctionDate<'2013/03/01' or tp1.PaymentDate<'" + DateTime.Now.ToString("yyyy/MM/dd") + "' ) ) then  (sum(case when (v1.Voucher_Type='C' and v1.Other_Trans_Type=5 ) then v1.Amount else 0.00 end)) else 0.00 end) as `PKasar` FROM svcf.voucher as v1 join headstree as ht1 on ht1.NodeID=v1.Head_Id left join trans_payment as tp1 on v1.Head_Id=tp1.TokenNumber left join auctiondetails as a1 on v1.Head_Id =a1.PrizedMemberID join `groupmaster` `gm1` ON " + strGroupID + " = `gm1`.`Head_Id` join `view_groupwisedue` as vgwd on " + strGroupID + "=vgwd.GroupId join `membertogroupmaster` `m1` on (`v1`.`Head_Id`=`m1`.`Head_Id`) join `branchdetails` `b1` on (`v1`.`BranchID`=`b1`.`Head_Id`) where v1.RootID=5 and v1.Head_Id  =" + strGroupMemberID + " and ht1.ParentID=" + strGroupID + " and v1.ChoosenDate<= '" + DateTime.Now.ToString("yyyy/MM/dd") + "' group by v1.Head_Id  order by cast(digits(ht1.Node) as unsigned) ;";


            decimal NPArrier = 0.00M;
            decimal PArrier = 0.00M;
            decimal ExcessRemittance = 0.00M;
            decimal Debit = 0.00M;
            decimal Credit = 0.00M;

            string GuarantorName = string.Empty;
            string DrawNo = String.Empty;
            string PaymentDate = string.Empty;
            string AuctionDate = string.Empty;

            DataTable dtTrans = balayer.GetDataTable(strQuery);
            decimal.TryParse(balayer.ToobjectstrEvenNull(dtTrans.Rows[0]["ExcessRemittance"]), out ExcessRemittance);
            decimal.TryParse(balayer.ToobjectstrEvenNull(dtTrans.Rows[0]["Credit"]), out Credit);
            decimal.TryParse(balayer.ToobjectstrEvenNull(dtTrans.Rows[0]["Debit"]), out Debit);
            decimal.TryParse(balayer.ToobjectstrEvenNull(dtTrans.Rows[0]["PArrier"]), out PArrier);
            decimal.TryParse(balayer.ToobjectstrEvenNull(dtTrans.Rows[0]["NPArrier"]), out NPArrier);

            string gname = (balayer.ToobjectstrEvenNull(dtTrans.Rows[0]["GuarantorName"]).ToString());
            string DNo = (balayer.ToobjectstrEvenNull(dtTrans.Rows[0]["DrawNo"]).ToString());
            string PayDate = (balayer.ToobjectstrEvenNull(dtTrans.Rows[0]["PaymentDate"]).ToString());

            string AucDate = (balayer.ToobjectstrEvenNull(dtTrans.Rows[0]["AuctionDate"]).ToString());


            lblCrAmount.Text = Credit.ToString();
            lblDrAmount.Text = Debit.ToString();
            lblExcess.Text = ExcessRemittance.ToString();
            lblPrizedArrier.Text = PArrier.ToString();
            lblNonPrizedArrier.Text = NPArrier.ToString();
            lblByNumber.Text = balayer.ToobjectstrEvenNull(dtTrans.Rows[0]["ChitNo"]);
            lblGuarantorName.Text = gname.ToString();
            lblDrawNo.Text = DNo.ToString();
            lblPaymentDate.Text = PayDate.ToString();
            lblAuctionDate.Text = AucDate.ToString();


            TIChit.Visible = false;
            pnlTransactions.Visible = true;

            //string strMemberID = balayer.ToobjectstrEvenNull(e.Item.Name);
            //dxpcMember.ActiveTabIndex = 1;
            //  lblCustomerName.Text = "<b>Customer Name : </b>" +e.Item.Text;

            //lblCustomerAdress.Text = "<b>Customer Address : </b>" +  e.Item.Description;
            //if (strMemberID!="")
            //{
            //MysqlDChit.SelectCommand = "SELECT GrpMemberID,Head_ID FROM `svcf`.`membertogroupmaster`  where memberID=" + strMemberID;
            //}
            //if (TIChit.Items.Count == 1)
            //{
            //    TIChit.Visible = false;

            //}

        }

        protected void CheckBox2_CheckedChanged(object sender, EventArgs e)
        {

        }

    }
}