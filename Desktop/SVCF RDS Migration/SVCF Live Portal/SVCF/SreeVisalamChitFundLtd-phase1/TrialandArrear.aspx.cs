
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
using System.IO;
using DevExpress.XtraPrintingLinks;
using DevExpress.XtraPrinting;
using System.Drawing;
using System.Globalization;
using System.Text.RegularExpressions;
using DevExpress.XtraReports.UI;
using DevExpress.XtraPrinting.Shape;
using DevExpress.Web.ASPxEditors;
using SVCF_BusinessAccessLayer;
using SVCF_DataAccessLayer;
using SVCF_TransactionLayer;
using System.Data.Entity;
using System.Collections.Generic;
using SVCF_PropertyLayer;
using System.Text;
using iTextSharp.text;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf;
using iTextSharp.text.html;
using iTextSharp.text.html.simpleparser;
using System.Drawing.Printing;
using System.Drawing;
using log4net;
using log4net.Config;


namespace SreeVisalamChitFundLtd_phase1
{
    public partial class TrialAndArrear : System.Web.UI.Page
    {

        #region Object

        BusinessLayer balayer = new BusinessLayer();
        TransactionLayer trn = new TransactionLayer();
        CommonVariables objCOM = new CommonVariables();

        #endregion
        ILog logger = log4net.LogManager.GetLogger(typeof(TrialAndArrear));

        private System.Drawing.Image headerImage;


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetGroupMember();
                //ddlChit.SelectedItem.Value = "0";
                txtFromDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            }
             //select();
            logger.Info("Trial and Arrear - at: " + DateTime.Now + " by: " + Convert.ToString(Session["UserName"]) + "");
        }
        public void GetGroupMember()
        {
            ddlChit.DataSource = null;
            ddlChit.DataBind();
            objCOM.DtChitGrp = null;
            objCOM.Data = "SELECT `groupmaster`.`GROUPNO`,`groupmaster`.`Head_Id` FROM `svcf`.`groupmaster` where `groupmaster`.`IsFinished`=0 and `groupmaster`.`BranchID`=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + "";
            objCOM.DtChitGrp = balayer.GetDataTable(objCOM.Data);
            ddlChit.DataSource = objCOM.DtChitGrp;
            objCOM.Dr = objCOM.DtChitGrp.NewRow();
            objCOM.Dr[0] = "--select--";
            objCOM.Dr[1] = "0";
            ddlChit.DataTextField = "GROUPNO";
            ddlChit.DataValueField = "Head_Id";
            objCOM.DtChitGrp.Rows.InsertAt(objCOM.Dr, 0);
            ddlChit.DataBind();
        }


        public string UpperFirst(string source)
        {
            return source.ToLower().Remove(0, 1)
                    .Insert(0, source.Substring(0, 1).ToUpper());
        }

        protected void select()
        {
            try
            {
                string removalname = "", transfername = "";
                objCOM.DtBind.Columns.Add("ChitNo1", typeof(int));
                objCOM.DtBind.Columns.Add("MemberName");
                // sivanesan added member id
                objCOM.DtBind.Columns.Add("MemberId", typeof(int));
                objCOM.DtBind.Columns.Add("Credit", typeof(decimal));
                objCOM.DtBind.Columns.Add("Debit", typeof(decimal));
                objCOM.DtBind.Columns.Add("ExcessRemittance", typeof(decimal));
                objCOM.DtBind.Columns.Add("NPArrier", typeof(decimal));
                objCOM.DtBind.Columns.Add("PArrier", typeof(decimal));
                objCOM.DtBind.Columns.Add("NPKasar", typeof(decimal));
                objCOM.DtBind.Columns.Add("PKasar", typeof(decimal));
                objCOM.DtBind.Columns.Add("Branches");
                objCOM.DtBind.Columns.Add("MobileNumber");
                objCOM.DrBind = objCOM.DtBind.NewRow();
                string TotaldueAmount = "";
                var GrpMemberid = string.Empty;
                if (ddlChit.SelectedItem.Text == "--select--")
                {
                }
                else
                {
                    objCOM.RowCount = balayer.GetScalarDataInt("select count(*) from auctiondetails where IsPrized='Y' and AuctionDate<='" + balayer.indiandateToMysqlDate(txtFromDate.Text) + "' and GroupID=" + ddlChit.SelectedItem.Value);
                    if (objCOM.RowCount > 0)
                    {
                       var  DtHeads = balayer.GetDataTable("select NodeID from headstree where ParentID=" + ddlChit.SelectedItem.Value);
                        for (int j = 0; j < DtHeads.Rows.Count; j++)
                        {
                            GrpMemberid = string.Empty;    
                            objCOM.Sss = balayer.GetSingleValue(@"select cast(digits(mg1.GrpMemberID) as unsigned) as ChitNo1 from membertogroupmaster as mg1 join voucher as v1 on v1.Head_Id=mg1.Head_Id join groupmaster as vgwd1 on vgwd1.`Head_Id`=" + ddlChit.SelectedItem.Value + " left join trans_payment as tp1 on v1.Head_Id =tp1.TokenNumber join membermaster as mm on (mg1.MemberID=mm.MemberIDNew) where mg1.BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and mg1.GroupID=" + ddlChit.SelectedItem.Value + " and v1.Head_Id=" + DtHeads.Rows[j]["NodeID"] + " and v1.ChoosenDate<= '" + balayer.indiandateToMysqlDate(txtFromDate.Text) + "' group by v1.Head_Id order by cast(digits(mg1.GrpMemberID) as unsigned),v1.Voucher_Type DESC ;");

                            TotaldueAmount = balayer.GetSingleValue(@"select sum(CurrentDueAmount) from auctiondetails where GroupID=" + ddlChit.SelectedItem.Value + " and AuctionDate<='" + balayer.indiandateToMysqlDate(txtFromDate.Text) + "';");

                            if (string.IsNullOrEmpty(objCOM.Sss))
                            {
                                GrpMemberid = balayer.GetSingleValue("select cast(digits(GrpMemberID) as unsigned) as ChitNo1 from membertogroupmaster where Head_Id=" + DtHeads.Rows[j]["NodeID"]);
                                if (!(string.IsNullOrEmpty(GrpMemberid)))
                                {
                                    objCOM.Sss = GrpMemberid;
                                    objCOM.DrBind["ChitNo1"] = GrpMemberid;
                                }
                            }
                            else
                            {
                                objCOM.DrBind["ChitNo1"] = objCOM.Sss;
                            }

                            if (!(string.IsNullOrEmpty(objCOM.Sss)))
                            {
                                ///<summary>
                                ///modified by keerthana : 06/08/2018 change
                                ///Reason : To display removal chit member
                                ///</summary>
                                removalname = balayer.GetSingleValue(@"select (case when(r.DateOfRemoval>'" + balayer.indiandateToMysqlDate(txtFromDate.Text) + "') then concat(m1.CustomerName) else concat(mg1.MemberName) end) as `MemberName` from membertogroupmaster as mg1 left join removal_approval r on mg1.Head_Id=r.GroupMemberID join membermaster m1 on m1.MemberIDNew=r.OldMemberID join voucher as v1 on v1.Head_Id=mg1.Head_Id join groupmaster as vgwd1 on vgwd1.`Head_Id`=" + ddlChit.SelectedItem.Value + " left join trans_payment as tp1 on v1.Head_Id =tp1.TokenNumber join membermaster as mm on (mg1.MemberID=mm.MemberIDNew) where mg1.BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and mg1.GroupID=" + ddlChit.SelectedItem.Value + " and v1.Head_Id=" + DtHeads.Rows[j]["NodeID"] + " and v1.ChoosenDate<= '" + balayer.indiandateToMysqlDate(txtFromDate.Text) + "' group by v1.Head_Id order by v1.ChoosenDate DESC ;");

                                transfername = balayer.GetSingleValue(@"select (case when(r.ApprovedDate>'" + balayer.indiandateToMysqlDate(txtFromDate.Text) + "') then concat(m1.CustomerName) else concat(mg1.MemberName) end) as `MemberName` from membertogroupmaster as mg1 left join transfer_approval r on mg1.Head_Id=r.GrpMemberID join membermaster m1 on m1.MemberIDNew=r.Old_Member join voucher as v1 on v1.Head_Id=mg1.Head_Id join groupmaster as vgwd1 on vgwd1.`Head_Id`=" + ddlChit.SelectedItem.Value + " left join trans_payment as tp1 on v1.Head_Id =tp1.TokenNumber join membermaster as mm on (mg1.MemberID=mm.MemberIDNew) where mg1.BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and mg1.GroupID=" + ddlChit.SelectedItem.Value + " and v1.Head_Id=" + DtHeads.Rows[j]["NodeID"] + " and v1.ChoosenDate<= '" + balayer.indiandateToMysqlDate(txtFromDate.Text) + "' group by v1.Head_Id order by v1.ChoosenDate DESC");

                                if (string.IsNullOrEmpty(removalname) && string.IsNullOrEmpty(transfername))
                                {
                                    objCOM.StrMemID = balayer.GetSingleValue("SELECT MemberID FROM svcf.membertogroupmaster where Head_id=" + DtHeads.Rows[j]["NodeID"]);
                                    objCOM.MemberName = balayer.GetSingleValue("SELECT CustomerName FROM svcf.membermaster where MemberIDNew=" + objCOM.StrMemID);
                                    // membername = UpperFirst(membername);
                                    objCOM.DrBind["MemberName"] = objCOM.MemberName;
                                }
                                else if (removalname == "")
                                {
                                    // strName = UpperFirst(strName);
                                    objCOM.DrBind["MemberName"] = transfername;
                                }
                                else
                                {
                                    objCOM.DrBind["MemberName"] = removalname;
                                }

                                // sivanesan member id
                                var MemberId = balayer.GetSingleValue("SELECT MemberID FROM svcf.membertogroupmaster where Head_id=" + DtHeads.Rows[j]["NodeID"]);
                                if (!string.IsNullOrEmpty(MemberId))
                                {
                                    objCOM.DrBind["MemberId"] = MemberId;
                                }
                                else
                                {
                                    objCOM.DrBind["MemberId"] = "0";
                                }

                                ///<summary>
                                ///modified by keerthana : 06/08/2018 change
                                ///Reason : To display removal chit member
                                ///</summary>

                                //objCOM.StrName = balayer.GetSingleValue(@"select (case when(r.DateOfRemoval>'" + balayer.indiandateToMysqlDate(txtFromDate.Text) + "') then concat(m1.CustomerName) else concat(mg1.MemberName) end) as `MemberName` from membertogroupmaster as mg1 left join removal_approval r on mg1.Head_Id=r.GroupMemberID join membermaster m1 on m1.MemberIDNew=r.OldMemberID join voucher as v1 on v1.Head_Id=mg1.Head_Id join groupmaster as vgwd1 on vgwd1.`Head_Id`=" + ddlChit.SelectedItem.Value + " left join trans_payment as tp1 on v1.Head_Id =tp1.TokenNumber join membermaster as mm on (mg1.MemberID=mm.MemberIDNew) where mg1.BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and mg1.GroupID=" + ddlChit.SelectedItem.Value + " and v1.Head_Id=" + DtHeads.Rows[j]["NodeID"] + " and v1.ChoosenDate<= '" + balayer.indiandateToMysqlDate(txtFromDate.Text) + "' group by v1.Head_Id order by v1.ChoosenDate DESC ;");

                                //if (string.IsNullOrEmpty(objCOM.StrName))
                                //{
                                //    objCOM.StrMemID = balayer.GetSingleValue("SELECT MemberID FROM svcf.membertogroupmaster where Head_id=" + DtHeads.Rows[j]["NodeID"]);
                                //    objCOM.MemberName = balayer.GetSingleValue("SELECT CustomerName FROM svcf.membermaster where MemberIDNew=" + objCOM.StrMemID);
                                //    // membername = UpperFirst(membername);
                                //    objCOM.DrBind["MemberName"] = objCOM.MemberName;
                                //}
                                //else
                                //{
                                //    // strName = UpperFirst(strName);
                                //    objCOM.DrBind["MemberName"] = objCOM.StrName;
                                //}

                                objCOM.Credit = balayer.GetSingleValue(@"select (case when( tp1.PaymentDate is null or tp1.PaymentDate >'" + balayer.indiandateToMysqlDate(txtFromDate.Text) + "') then sum(case when (v1.Voucher_Type='C' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) -sum(case when (v1.Voucher_Type='D' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) else 0.00 end ) as Credit from membertogroupmaster as mg1 join voucher as v1 on v1.Head_Id=mg1.Head_Id join groupmaster as vgwd1 on vgwd1.`Head_Id`=" + ddlChit.SelectedItem.Value + " left join trans_payment as tp1 on v1.Head_Id =tp1.TokenNumber join membermaster as mm on (mg1.MemberID=mm.MemberIDNew) where mg1.BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and mg1.GroupID=" + ddlChit.SelectedItem.Value + " and v1.Head_Id=" + DtHeads.Rows[j]["NodeID"] + " and v1.ChoosenDate<= '" + balayer.indiandateToMysqlDate(txtFromDate.Text) + "' group by v1.Head_Id order by cast(digits(mg1.GrpMemberID) as unsigned),v1.Voucher_Type DESC ;");
                                if (string.IsNullOrEmpty(objCOM.Credit))
                                {
                                    objCOM.DrBind["Credit"] = "0.00";
                                }
                                else
                                {
                                    objCOM.DrBind["Credit"] = objCOM.Credit;
                                }
                                objCOM.Debit = balayer.GetSingleValue(@"select (case when( tp1.PaymentDate<='" + balayer.indiandateToMysqlDate(txtFromDate.Text) + "' ) then sum((case when (v1.Voucher_Type='D' and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) -case when (v1.Voucher_Type='C' and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) else 0.00 end ) as Debit from membertogroupmaster as mg1 join voucher as v1 on v1.Head_Id=mg1.Head_Id join groupmaster as vgwd1 on vgwd1.`Head_Id`=" + ddlChit.SelectedItem.Value + " left join trans_payment as tp1 on v1.Head_Id =tp1.TokenNumber join membermaster as mm on (mg1.MemberID=mm.MemberIDNew) where mg1.BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and mg1.GroupID=" + ddlChit.SelectedItem.Value + " and v1.Head_Id=" + DtHeads.Rows[j]["NodeID"] + " and v1.ChoosenDate<= '" + balayer.indiandateToMysqlDate(txtFromDate.Text) + "' group by v1.Head_Id order by cast(digits(mg1.GrpMemberID) as unsigned),v1.Voucher_Type DESC ;");
                                if (string.IsNullOrEmpty(objCOM.Debit))
                                {
                                    objCOM.DrBind["Debit"] = "0.00";
                                }
                                else
                                {
                                    objCOM.DrBind["Debit"] = objCOM.Debit;
                                }
                                //excess = balayer.GetSingleValue(@"select  (case when( (sum(case when (v1.Voucher_Type='C' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) -sum(case when (v1.Voucher_Type='D' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) -vgwd1.TotaldueAmount)>0.00) then (sum(case when (v1.Voucher_Type='C' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) -sum(case when (v1.Voucher_Type='D' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) -vgwd1.TotaldueAmount) else 0.00 end) as ExcessRemittance from membertogroupmaster as mg1 join voucher as v1 on v1.Head_Id=mg1.Head_Id join view_groupwisedue as vgwd1 on vgwd1.`GroupId`=" + ddlChit.SelectedItem.Value + " left join trans_payment as tp1 on v1.Head_Id =tp1.TokenNumber join membermaster as mm on (mg1.MemberID=mm.MemberIDNew) where mg1.BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and mg1.GroupID=" + ddlChit.SelectedItem.Value + " and v1.Head_Id=" + dtHeads.Rows[j]["NodeID"] + " and v1.ChoosenDate<= '" + balayer.indiandateToMysqlDate(txtFromDate.Text) + "' group by v1.Head_Id order by cast(digits(mg1.GrpMemberID) as unsigned),v1.Voucher_Type DESC ;");

                                objCOM.Excess = balayer.GetSingleValue(@"select  (case when( (sum(case when (v1.Voucher_Type='C' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) -sum(case when (v1.Voucher_Type='D' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) -" + TotaldueAmount + ")>0.00) then (sum(case when (v1.Voucher_Type='C' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) -sum(case when (v1.Voucher_Type='D' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) -" + TotaldueAmount + ") else 0.00 end) as ExcessRemittance from membertogroupmaster as mg1 join voucher as v1 on v1.Head_Id=mg1.Head_Id join groupmaster as vgwd1 on vgwd1.`Head_Id`=" + ddlChit.SelectedItem.Value + " left join trans_payment as tp1 on v1.Head_Id =tp1.TokenNumber join membermaster as mm on (mg1.MemberID=mm.MemberIDNew) where mg1.BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and mg1.GroupID=" + ddlChit.SelectedItem.Value + " and v1.Head_Id=" + DtHeads.Rows[j]["NodeID"] + " and v1.ChoosenDate<= '" + balayer.indiandateToMysqlDate(txtFromDate.Text) + "' group by v1.Head_Id order by cast(digits(mg1.GrpMemberID) as unsigned),v1.Voucher_Type DESC ;");
                                if (string.IsNullOrEmpty(objCOM.Excess))
                                {
                                    objCOM.DrBind["ExcessRemittance"] = "0.00";
                                }
                                else
                                {
                                    objCOM.DrBind["ExcessRemittance"] = objCOM.Excess;
                                }

                                objCOM.Nparr = balayer.GetSingleValue(@"select (case when( tp1.PaymentDate is null  or tp1.PaymentDate >'" + balayer.indiandateToMysqlDate(txtFromDate.Text) + "') then (case when( (" + TotaldueAmount + "-sum(case when (v1.Voucher_Type='C' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) +sum(case when (v1.Voucher_Type='D' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) )>0.00) then (" + TotaldueAmount + "-sum(case when (v1.Voucher_Type='C' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) +sum(case when (v1.Voucher_Type='D' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) ) else 0.00 end) else 0.00 end) as NPArrier from membertogroupmaster as mg1 join voucher as v1 on v1.Head_Id=mg1.Head_Id join groupmaster as vgwd1 on vgwd1.`Head_Id`=" + ddlChit.SelectedItem.Value + " left join trans_payment as tp1 on v1.Head_Id =tp1.TokenNumber join membermaster as mm on (mg1.MemberID=mm.MemberIDNew) where mg1.BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and mg1.GroupID=" + ddlChit.SelectedItem.Value + " and v1.Head_Id=" + DtHeads.Rows[j]["NodeID"] + " and v1.ChoosenDate<= '" + balayer.indiandateToMysqlDate(txtFromDate.Text) + "' group by v1.Head_Id order by cast(digits(mg1.GrpMemberID) as unsigned),v1.Voucher_Type DESC ;");

                                //objCOM.Nparr =( objCOM.Nparr == "0.00" ? null : objCOM.Nparr);
                                if (string.IsNullOrEmpty(objCOM.Nparr))
                                {

                                    objCOM.Dtnullck = balayer.GetDataTable(@"SELECT * FROM voucher WHERE Head_Id = " + DtHeads.Rows[j]["NodeID"] + " and ChoosenDate <= '" + balayer.indiandateToMysqlDate(txtFromDate.Text) + "';");
                                    if (objCOM.Dtnullck.Rows.Count <= 0)
                                    {
                                       // objCOM.Maxdrawno = Convert.ToInt32(balayer.GetSingleValue(@"select max(DrawNO) as Drawno from auctiondetails where GroupId=" + ddlChit.SelectedItem.Value + "  and  (IsPrized='N' or IsPrized='Y') and AuctionDate <='" + balayer.indiandateToMysqlDate(txtFromDate.Text) + "' ;"));
                                      //  objCOM.Totalcr = Convert.ToDecimal(balayer.GetSingleValue(@"select sum(CurrentDueAmount) as Amount from auctiondetails where GroupId=" + ddlChit.SelectedItem.Value + "  and  (IsPrized='N' or IsPrized='Y') and DrawNO <= " + objCOM.Maxdrawno + ""));
                                       // objCOM.DrBind["NPArrier"] = objCOM.Totalcr;
                                        objCOM.DrBind["NPArrier"] = TotaldueAmount;
                                    }
                                    else
                                    {
                                        objCOM.DrBind["NPArrier"] = "0.00";
                                    }
                                }
                                else
                                {
                                    objCOM.DrBind["NPArrier"] = objCOM.Nparr;
                                }

                                objCOM.Parr = balayer.GetSingleValue(@"select (case when( tp1.PaymentDate<='" + balayer.indiandateToMysqlDate(txtFromDate.Text) + "' ) then (case when( (" + TotaldueAmount + "-sum(case when (v1.Voucher_Type='C' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) +sum(case when (v1.Voucher_Type='D' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) )>0.00) then (" + TotaldueAmount + "-sum(case when (v1.Voucher_Type='C' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) +sum(case when (v1.Voucher_Type='D' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) ) else 0.00 end) else 0.00 end ) as PArrier from membertogroupmaster as mg1 join voucher as v1 on v1.Head_Id=mg1.Head_Id join groupmaster as vgwd1 on vgwd1.`Head_Id`=" + ddlChit.SelectedItem.Value + " left join trans_payment as tp1 on v1.Head_Id =tp1.TokenNumber join membermaster as mm on (mg1.MemberID=mm.MemberIDNew) where mg1.BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and mg1.GroupID=" + ddlChit.SelectedItem.Value + " and v1.Head_Id=" + DtHeads.Rows[j]["NodeID"] + " and v1.ChoosenDate<= '" + balayer.indiandateToMysqlDate(txtFromDate.Text) + "' group by v1.Head_Id order by cast(digits(mg1.GrpMemberID) as unsigned),v1.Voucher_Type DESC ;");
                                if (string.IsNullOrEmpty(objCOM.Excess))
                                {
                                    objCOM.DrBind["PArrier"] = "0.00";
                                }
                                else
                                {
                                    objCOM.DrBind["PArrier"] = objCOM.Parr;
                                }
                                objCOM.Npkas = balayer.GetSingleValue(@"select (case when( tp1.PaymentDate is null  or tp1.PaymentDate >'" + balayer.indiandateToMysqlDate(txtFromDate.Text) + "') then sum((case when (v1.Voucher_Type='C' and v1.Other_Trans_Type=5 ) then v1.Amount else 0.00 end)) else 0.00 end ) as NPKasar from membertogroupmaster as mg1 join voucher as v1 on v1.Head_Id=mg1.Head_Id join groupmaster as vgwd1 on vgwd1.`Head_Id`=" + ddlChit.SelectedItem.Value + " left join trans_payment as tp1 on v1.Head_Id =tp1.TokenNumber join membermaster as mm on (mg1.MemberID=mm.MemberIDNew) where mg1.BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and mg1.GroupID=" + ddlChit.SelectedItem.Value + " and v1.Head_Id=" + DtHeads.Rows[j]["NodeID"] + " and v1.ChoosenDate<= '" + balayer.indiandateToMysqlDate(txtFromDate.Text) + "' group by v1.Head_Id order by cast(digits(mg1.GrpMemberID) as unsigned),v1.Voucher_Type DESC ;");
                                if (string.IsNullOrEmpty(objCOM.Excess))
                                {
                                    objCOM.DrBind["NPKasar"] = "0.00";
                                }
                                else
                                {
                                    objCOM.DrBind["NPKasar"] = objCOM.Npkas;
                                }
                                objCOM.Pkas = balayer.GetSingleValue(@"select (case when( tp1.PaymentDate<='" + balayer.indiandateToMysqlDate(txtFromDate.Text) + "' ) then sum((case when (v1.Voucher_Type='C' and v1.Other_Trans_Type=5 ) then v1.Amount else 0.00 end)) else 0.00 end ) as PKasar from membertogroupmaster as mg1 join voucher as v1 on v1.Head_Id=mg1.Head_Id join groupmaster as vgwd1 on vgwd1.`Head_Id`=" + ddlChit.SelectedItem.Value + " left join trans_payment as tp1 on v1.Head_Id =tp1.TokenNumber join membermaster as mm on (mg1.MemberID=mm.MemberIDNew) where mg1.BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and mg1.GroupID=" + ddlChit.SelectedItem.Value + " and v1.Head_Id=" + DtHeads.Rows[j]["NodeID"] + " and v1.ChoosenDate<= '" + balayer.indiandateToMysqlDate(txtFromDate.Text) + "' group by v1.Head_Id order by cast(digits(mg1.GrpMemberID) as unsigned),v1.Voucher_Type DESC ;");
                                if (string.IsNullOrEmpty(objCOM.Excess))
                                {
                                    objCOM.DrBind["PKasar"] = "0.00";
                                }
                                else
                                {
                                    objCOM.DrBind["PKasar"] = objCOM.Pkas;
                                }
                                objCOM.StrMember = balayer.GetSingleValue("SELECT Head_Id FROM svcf.voucher where Head_id=" + DtHeads.Rows[j]["NodeID"] + " and ChoosenDate<='" + balayer.indiandateToMysqlDate(txtFromDate.Text) + "' order by ChoosenDate desc");

                                if (string.IsNullOrEmpty(objCOM.StrMember))
                                {
                                    objCOM.StrMember = "0";
                                }
                              //  objCOM.StrBranches = balayer.GetSingleValue("SELECT b1.Place FROM svcf.membermaster as m1 join svcf.branchdetails as b1 on (m1.BranchID=b1.Head_Id) join svcf.membertogroupmaster as mg1 on mg1.MemberID=m1.MemberIDNew where mg1.Head_Id=" + objCOM.StrMember);
                                objCOM.StrBranches = balayer.GetSingleValue("SELECT b1.B_Name FROM svcf.membermaster as m1 join svcf.branchdetails as b1 on (m1.BranchID=b1.Head_Id) join svcf.membertogroupmaster as mg1 on mg1.MemberID=m1.MemberIDNew where mg1.Head_Id=" + objCOM.StrMember);


                                if (string.IsNullOrEmpty(objCOM.StrBranches))
                                {
                                  //  objCOM.DrBind["Branches"] = balayer.GetSingleValue("SELECT b1.Place FROM svcf.membertogroupmaster as m1 join branchdetails as b1 on (m1.B_Id=b1.Head_Id) where m1.Head_id=" + DtHeads.Rows[j]["NodeID"]);
                                    objCOM.DrBind["Branches"] = balayer.GetSingleValue("SELECT b1.B_Name FROM svcf.membertogroupmaster as m1 join branchdetails as b1 on (m1.B_Id=b1.Head_Id) where m1.Head_id=" + DtHeads.Rows[j]["NodeID"]);
                                }
                                else
                                {
                                    objCOM.DrBind["Branches"] = objCOM.StrBranches;
                                }
                                objCOM.MobileNumber = balayer.GetSingleValue("SELECT m1.MobileNo FROM svcf.membermaster as m1 join svcf.branchdetails as b1 on (m1.BranchID=b1.Head_Id) join svcf.membertogroupmaster as mg1 on mg1.MemberID=m1.MemberIDNew where mg1.Head_Id=" + objCOM.StrMember);
                                if (string.IsNullOrEmpty(objCOM.MobileNumber))
                                {
                                    string strMemID = balayer.GetSingleValue("SELECT MemberID FROM svcf.membertogroupmaster where Head_id=" + DtHeads.Rows[j]["NodeID"]);
                                    objCOM.DrBind["MobileNumber"] = balayer.GetSingleValue("SELECT MobileNo FROM svcf.membermaster where MemberIDNew=" + strMemID);

                                }
                                else
                                {
                                    objCOM.DrBind["MobileNumber"] = objCOM.MobileNumber;
                                }

                                objCOM.DtBind.Rows.Add(objCOM.DrBind.ItemArray);
                            }
                        }
                        ViewState["Credit"] = objCOM.DtBind.Compute("Sum(Credit)", "");
                        ViewState["Debit"] = objCOM.DtBind.Compute("Sum(Debit)", "");
                        ViewState["NPKasar"] = objCOM.DtBind.Compute("Sum(NPKasar)", "");
                        ViewState["PKasar"] = objCOM.DtBind.Compute("Sum(PKasar)", "");
                        ViewState["ExcessRemittance"] = objCOM.DtBind.Compute("Sum(ExcessRemittance)", "");
                        ViewState["PArrier"] = objCOM.DtBind.Compute("Sum(PArrier)", "");
                        ViewState["NPArrier"] = objCOM.DtBind.Compute("Sum(NPArrier)", "");
                    }
                    else
                    {

                       var DtHeads = balayer.GetDataTable(" select h1.NodeID from svcf.headstree h1 right join svcf.membertogroupmaster mg1 on mg1.Head_Id=h1.NodeID where h1.ParentID='" + ddlChit.SelectedItem.Value + "' group by h1.NodeID");

                        for (int j = 0; j < DtHeads.Rows.Count; j++)
                        {
                            objCOM.Sss = balayer.GetSingleValue(@"select cast(digits(mg1.GrpMemberID) as unsigned) as ChitNo1 from membertogroupmaster as mg1 join voucher as v1 on v1.Head_Id=mg1.Head_Id join groupmaster as vgwd1 on vgwd1.`Head_Id`=" + ddlChit.SelectedItem.Value + " left join trans_payment as tp1 on v1.Head_Id =tp1.TokenNumber join membermaster as mm on (mg1.MemberID=mm.MemberIDNew) where mg1.BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and mg1.GroupID=" + ddlChit.SelectedItem.Value + " and v1.Head_Id=" + DtHeads.Rows[j]["NodeID"] + " and v1.ChoosenDate<= '" + balayer.indiandateToMysqlDate(txtFromDate.Text) + "' group by v1.Head_Id order by cast(digits(mg1.GrpMemberID) as unsigned),v1.Voucher_Type DESC ;");
                            GrpMemberid = string.Empty;    
                            if (string.IsNullOrEmpty(objCOM.Sss))
                            {
                                GrpMemberid = balayer.GetSingleValue("select cast(digits(GrpMemberID) as unsigned) as ChitNo1 from membertogroupmaster where Head_Id=" + DtHeads.Rows[j]["NodeID"]);
                                if (!(string.IsNullOrEmpty(GrpMemberid)))
                                {
                                    objCOM.Sss = GrpMemberid;
                                    objCOM.DrBind["ChitNo1"] = GrpMemberid;
                                }                             
                            }
                            else
                            {
                                objCOM.DrBind["ChitNo1"] = objCOM.Sss;
                            }
                            if (!(string.IsNullOrEmpty(objCOM.Sss)))
                            {
                                ///<summary>
                                ///modified by keerthana : 06/08/2018 change
                                ///Reason : To display removal chit member
                                ///</summary>
                                removalname = balayer.GetSingleValue(@"select (case when(r.DateOfRemoval>'" + balayer.indiandateToMysqlDate(txtFromDate.Text) + "') then concat(m1.CustomerName) else concat(mg1.MemberName) end) as `MemberName` from membertogroupmaster as mg1 left join removal_approval r on mg1.Head_Id=r.GroupMemberID join membermaster m1 on m1.MemberIDNew=r.OldMemberID  join groupmaster as vgwd1 on vgwd1.`Head_Id`=" + ddlChit.SelectedItem.Value + "  join membermaster as mm on (mg1.MemberID=mm.MemberIDNew) where mg1.BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and mg1.GroupID=" + ddlChit.SelectedItem.Value + " and r.GroupMemberID=" + DtHeads.Rows[j]["NodeID"] + " ");
                                transfername = balayer.GetSingleValue(@"select (case when(r.ApprovedDate>'" + balayer.indiandateToMysqlDate(txtFromDate.Text) + "') then concat(m1.CustomerName) else concat(mg1.MemberName) end) as `MemberName` from membertogroupmaster as mg1 left join transfer_approval r on mg1.Head_Id=r.GrpMemberID join membermaster m1 on m1.MemberIDNew=r.Old_Member  join groupmaster as vgwd1 on vgwd1.`Head_Id`=" + ddlChit.SelectedItem.Value + " join membermaster as mm on (mg1.MemberID=mm.MemberIDNew) where mg1.BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and mg1.GroupID=" + ddlChit.SelectedItem.Value + " and r.GrpMemberID=" + DtHeads.Rows[j]["NodeID"] + " ");
                                if (string.IsNullOrEmpty(removalname) && string.IsNullOrEmpty(transfername))
                                {
                                    string strMemID = balayer.GetSingleValue("SELECT MemberID FROM svcf.membertogroupmaster where Head_id=" + DtHeads.Rows[j]["NodeID"]);
                                    objCOM.DrBind["MemberName"] = balayer.GetSingleValue("SELECT CustomerName FROM svcf.membermaster where MemberIDNew=" + strMemID);
                                }
                                else if (removalname == "")
                                {
                                    objCOM.DrBind["MemberName"] = transfername;
                                }
                                else
                                {
                                    objCOM.DrBind["MemberName"] = removalname;
                                }
                                ///<summary>
                                ///modified by keerthana : 06/08/2018 change
                                ///Reason : To display removal chit member
                                ///</summary>


                                //objCOM.StrName = balayer.GetSingleValue(@"select concat(mg1.MemberName) as `MemberName` from membertogroupmaster as mg1 join voucher as v1 on v1.Head_Id=mg1.Head_Id join groupmaster as vgwd1 on vgwd1.`Head_Id`=" + ddlChit.SelectedItem.Value + " left join trans_payment as tp1 on v1.Head_Id =tp1.TokenNumber join membermaster as mm on (mg1.MemberID=mm.MemberIDNew) where mg1.BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and mg1.GroupID=" + ddlChit.SelectedItem.Value + " and v1.Head_Id=" + DtHeads.Rows[j]["NodeID"] + " and v1.ChoosenDate<= '" + balayer.indiandateToMysqlDate(txtFromDate.Text) + "' group by v1.Head_Id order by v1.ChoosenDate DESC ;");
                                //if (string.IsNullOrEmpty(objCOM.StrName))
                                //{
                                //    string strMemID = balayer.GetSingleValue("SELECT MemberID FROM svcf.membertogroupmaster where Head_id=" + DtHeads.Rows[j]["NodeID"]);
                                //    objCOM.DrBind["MemberName"] = balayer.GetSingleValue("SELECT CustomerName FROM svcf.membermaster where MemberIDNew=" + strMemID);
                                //}
                                //else
                                //{
                                //    objCOM.DrBind["MemberName"] = objCOM.StrName;
                                //}
                                objCOM.Credit = balayer.GetSingleValue("SELECT sum(case when(Voucher_Type='C') then Amount else 0.00 end) as Credit FROM svcf.voucher where ChoosenDate<='" + balayer.indiandateToMysqlDate(txtFromDate.Text) + "' and Head_id=" + DtHeads.Rows[j]["NodeID"]);
                                objCOM.Debit = balayer.GetSingleValue("SELECT sum(case when(Voucher_Type='D') then Amount else 0.00 end) as Debit FROM svcf.voucher where ChoosenDate<='" + balayer.indiandateToMysqlDate(txtFromDate.Text) + "' and Head_id=" + DtHeads.Rows[j]["NodeID"]);
                                if (string.IsNullOrEmpty(objCOM.Credit))
                                    objCOM.Credit = "0.00";
                                if (string.IsNullOrEmpty(objCOM.Debit))
                                    objCOM.Debit = "0.00";
                                objCOM.DecCredit = Convert.ToDecimal(objCOM.Credit);
                                objCOM.DecDebit = Convert.ToDecimal(objCOM.Debit);
                                if (objCOM.DecCredit > objCOM.DecDebit)
                                {
                                    objCOM.DrBind["Credit"] = objCOM.DecCredit - objCOM.DecDebit;
                                    objCOM.DrBind["Debit"] = "0.00";
                                }
                                else
                                {
                                    objCOM.DrBind["Credit"] = "0.00";
                                    objCOM.DrBind["Debit"] = objCOM.DecDebit - objCOM.DecCredit;
                                }
                                objCOM.Excess = null;
                                if (string.IsNullOrEmpty(objCOM.Excess))
                                {
                                    objCOM.DrBind["ExcessRemittance"] = "0.00";
                                }
                                else
                                {
                                    objCOM.DrBind["ExcessRemittance"] = objCOM.Excess;
                                }
                                objCOM.Nparr = null;
                                if (string.IsNullOrEmpty(objCOM.Nparr))
                                {
                                    objCOM.DrBind["NPArrier"] = "0.00";
                                }
                                else
                                {
                                    objCOM.DrBind["NPArrier"] = objCOM.Nparr;
                                }
                                objCOM.Parr = null;
                                if (string.IsNullOrEmpty(objCOM.Excess))
                                {
                                    objCOM.DrBind["PArrier"] = "0.00";
                                }
                                else
                                {
                                    objCOM.DrBind["PArrier"] = objCOM.Parr;
                                }
                                objCOM.Npkas = null;
                                if (string.IsNullOrEmpty(objCOM.Excess))
                                {
                                    objCOM.DrBind["NPKasar"] = "0.00";
                                }
                                else
                                {
                                    objCOM.DrBind["NPKasar"] = objCOM.Npkas;
                                }
                                objCOM.Pkas = null;
                                if (string.IsNullOrEmpty(objCOM.Excess))
                                {
                                    objCOM.DrBind["PKasar"] = "0.00";
                                }
                                else
                                {
                                    objCOM.DrBind["PKasar"] = objCOM.Pkas;
                                }
                                objCOM.StrMember = balayer.GetSingleValue("SELECT Head_id FROM svcf.voucher where Head_id=" + DtHeads.Rows[j]["NodeID"] + " and ChoosenDate<='" + balayer.indiandateToMysqlDate(txtFromDate.Text) + "' order by ChoosenDate desc");
                                if (string.IsNullOrEmpty(objCOM.StrMember))
                                {
                                    objCOM.StrMember = "0";
                                }
                                objCOM.StrBranches = balayer.GetSingleValue("SELECT b1.B_Name FROM svcf.membermaster as m1 join svcf.branchdetails as b1 on (m1.BranchID=b1.Head_Id) join svcf.membertogroupmaster as mg1 on mg1.MemberID=m1.MemberIDNew where mg1.Head_Id=" + objCOM.StrMember);

                                if (string.IsNullOrEmpty(objCOM.StrBranches))
                                {
                                    objCOM.DrBind["Branches"] = balayer.GetSingleValue("SELECT b1.B_Name FROM svcf.membertogroupmaster as m1 join branchdetails as b1 on (m1.B_Id=b1.Head_Id) where m1.Head_id=" + DtHeads.Rows[j]["NodeID"]);
                                }
                                else
                                {
                                    objCOM.DrBind["Branches"] = objCOM.StrBranches;
                                }
                                objCOM.MobileNumber = balayer.GetSingleValue("SELECT m1.MobileNo FROM svcf.membermaster as m1 join svcf.branchdetails as b1 on (m1.BranchID=b1.Head_Id) join svcf.membertogroupmaster as mg1 on mg1.MemberID=m1.MemberIDNew where mg1.Head_Id=" + objCOM.StrMember);
                                if (string.IsNullOrEmpty(objCOM.MobileNumber))
                                {
                                    string strMemID = balayer.GetSingleValue("SELECT MemberID FROM svcf.membertogroupmaster where Head_id=" + DtHeads.Rows[j]["NodeID"]);
                                    objCOM.DrBind["MobileNumber"] = balayer.GetSingleValue("SELECT MobileNo FROM svcf.membermaster where MemberIDNew=" + strMemID);
                                }
                                else
                                {
                                    objCOM.DrBind["MobileNumber"] = objCOM.MobileNumber;
                                }
                                objCOM.DtBind.Rows.Add(objCOM.DrBind.ItemArray);
                            }
                        }
                        ViewState["Credit"] = objCOM.DtBind.Compute("Sum(Credit)", "");
                        ViewState["Debit"] = objCOM.DtBind.Compute("Sum(Debit)", "");
                        ViewState["NPKasar"] = objCOM.DtBind.Compute("Sum(NPKasar)", "");
                        ViewState["PKasar"] = objCOM.DtBind.Compute("Sum(PKasar)", "");
                        ViewState["ExcessRemittance"] = objCOM.DtBind.Compute("Sum(ExcessRemittance)", "");
                        ViewState["PArrier"] = objCOM.DtBind.Compute("Sum(PArrier)", "");
                        ViewState["NPArrier"] = objCOM.DtBind.Compute("Sum(NPArrier)", "");
                    }
                    objCOM.Sssssssss = balayer.GetDataTable("SELECT `groupmaster`.`NoofMembers`,`groupmaster`.`GROUPNO`,`branchdetails`.`B_Name`,max(`auctiondetails`.`DrawNO`) as `RunningCall`,`groupmaster`.`ChitValue` FROM svcf.groupmaster join auctiondetails on (`groupmaster`.`Head_Id`=`auctiondetails`.`GroupID`) join `branchdetails` on (`groupmaster`.`BranchID`=`branchdetails`.`Head_Id`) where `groupmaster`.`Head_Id`=" + ddlChit.SelectedValue + " and `auctiondetails`.`AuctionDate`<='" + balayer.indiandateToMysqlDate(txtFromDate.Text) + "'  and `auctiondetails`.`BranchID`=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + ";");
                    objCOM.Str = Convert.ToString(objCOM.Sssssssss.Rows[0]["RunningCall"]) == "" ? "0" : Convert.ToString(objCOM.Sssssssss.Rows[0]["RunningCall"]);
                    objCOM.Dv = objCOM.DtBind.DefaultView;
                    objCOM.Dv.Sort = "ChitNo1 asc";
                    objCOM.SortedDT = objCOM.Dv.ToTable();

                    lblCaption.Text = "Sree Visalam Chit Fund Limited.,<br/>Trial And Arrear <br/> Branch Name : " + objCOM.Sssssssss.Rows[0]["B_Name"].ToString() + "; \t Running Call : " + objCOM.Str + " <br/> Group No : " + objCOM.Sssssssss.Rows[0]["GROUPNO"].ToString() + "; \t Chit Amount : " + objCOM.Sssssssss.Rows[0]["ChitValue"].ToString() + "; \t As On " + txtFromDate.Text;
                    gridTA.DataSource = objCOM.SortedDT;
                    lblsummary_NPkasar.Text = Convert.ToString(ViewState["NPKasar"]);
                    lblsummary_PKasar.Text = Convert.ToString(ViewState["PKasar"]);
                    lblsummary_GrandTotal.Text = Convert.ToString((Convert.ToDecimal(ViewState["Credit"]) + Convert.ToDecimal(ViewState["NPKasar"]) + Convert.ToDecimal(ViewState["PKasar"])));
                    Ibl_grandtotalbal.Text = Convert.ToString(Convert.ToDouble(lblsummary_GrandTotal.Text) - Convert.ToDouble(ViewState["Debit"]));
                  //  lblsummary_BalanceDR.Text = Math.Abs((Convert.ToDecimal(ViewState["Credit"]) + Convert.ToDecimal(ViewState["NPKasar"]) + Convert.ToDecimal(ViewState["PKasar"])) - Convert.ToDecimal(ViewState["Debit"])).ToString();
                  //decimal ddd = (Convert.ToDecimal(ViewState["Credit"]) + Convert.ToDecimal(ViewState["NPKasar"]) + Convert.ToDecimal(ViewState["PKasar"])) - Convert.ToDecimal(ViewState["Debit"]);
                    BalanceDR.Text = Math.Abs((Convert.ToDecimal(ViewState["Credit"]) + Convert.ToDecimal(ViewState["NPKasar"]) + Convert.ToDecimal(ViewState["PKasar"])) - Convert.ToDecimal(ViewState["Debit"])).ToString();
                    //if (Convert.ToDecimal(lblsummary_GrandTotal.Text) < 0)
                    if (Convert.ToDecimal(Ibl_grandtotalbal.Text) < 0)
                    {
                        lblsummary_CR.Visible = false;
                    
                        lblsummary_BalanceDR.Text = "" + BalanceDR.Text;
                        
                    }
                    else if (Convert.ToDecimal(Ibl_grandtotalbal.Text) > 0)
                    {
                        lblsummary_DR.Visible = false;
                      
                        lblsummary_BalanceCR.Text = "" + BalanceDR.Text;
                     
                    }
                  
             
                }
                // 
                Hiddentcap.Text = gridTA.Caption;
                ViewState["CurrentData"] = objCOM.SortedDT;
                // grid.DataBind();
                gridTA.DataBind();

            }
            catch (Exception) { }

        }
        //protected void lbBalanceText_Load(object sender, EventArgs e)
        //{
        //    Label label = (Label)sender;
        //    if (ViewState["Credit"] != null)
        //    {
        //        decimal dddd = (Convert.ToDecimal(ViewState["Credit"]) + Convert.ToDecimal(ViewState["NPKasar"]) + Convert.ToDecimal(ViewState["PKasar"])) - Convert.ToDecimal(ViewState["Debit"]);
        //        if (dddd < 0.00M)
        //        {
        //            Label6.Text = "Balance DR";
        //        }
        //        else
        //        {
        //            Label7.Text = "Balance CR";
        //        }
        //    }

        //}



        //sivanesan color returning method
        public System.Drawing.Color GetDocumentColor(object MemberId, object Debit)
        {
            Int32 MemId=0;
            decimal Debit_value = 0;
            if(MemberId !=DBNull.Value)
            {
                MemId = Convert.ToInt32(MemberId);
            }
            if(Debit != DBNull.Value)
            {
                Debit_value = Convert.ToDecimal(Debit);
            }
            if (Debit_value != 0.00m)
            {

                var documentNo = balayer.GetSingleValue("SELECT documentno FROM svcf.documentdetails where prizedmemberid=" + MemId);


                if (string.IsNullOrEmpty(documentNo))
                {
                    return System.Drawing.Color.White;
                }
                else
                {
                    return System.Drawing.Color.LightGray;
                }

            }

            return System.Drawing.Color.White;
            //return System.Drawing.Color.LightGray;
        }





        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }

     
        protected void imgpdf_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                DataTable captiondt = new DataTable();
                string tablecaption = "";
                captiondt = balayer.GetDataTable("SELECT `groupmaster`.`NoofMembers`,`groupmaster`.`GROUPNO`,`branchdetails`.`B_Name`,max(`auctiondetails`.`DrawNO`) as `RunningCall`,`groupmaster`.`ChitValue` FROM svcf.groupmaster join auctiondetails on (`groupmaster`.`Head_Id`=`auctiondetails`.`GroupID`) join `branchdetails` on (`groupmaster`.`BranchID`=`branchdetails`.`Head_Id`) where `groupmaster`.`Head_Id`=" + ddlChit.SelectedValue + " and `auctiondetails`.`AuctionDate`<='" + balayer.indiandateToMysqlDate(txtFromDate.Text) + "'  and `auctiondetails`.`BranchID`=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + ";");
                objCOM.Str = Convert.ToString(captiondt.Rows[0]["RunningCall"]) == "" ? "0" : Convert.ToString(captiondt.Rows[0]["RunningCall"]);
                tablecaption = "Sree Visalam Chit Fund Limited.,<br/>Trial And Arrear <br/> Branch Name : " + captiondt.Rows[0]["B_Name"].ToString() + "; \t Running Call : " + objCOM.Str + " <br/> Group No : " + captiondt.Rows[0]["GROUPNO"].ToString() + "; \t Chit Amount : " + captiondt.Rows[0]["ChitValue"].ToString() + "; \t As On " + txtFromDate.Text;

                //DataTable tt = (DataTable)ViewState["CurrentData"];
                //DataTable dtTemp= (DataTable)ViewState["CurrentData"];
                //tt.Columns.Remove("MemberId");
                gridTA.DataSource = (DataTable)ViewState["CurrentData"];
                gridTA.DataBind();
                Phrase phrase = null;
                StringBuilder sb = new StringBuilder();
               // DataTable GH = (DataTable)ViewState["CurrentData"];

                phrase = new Phrase();               
                phrase.Add(new Chunk("\t\t\t\t\t\t\t\t Sree Visalam Chit Fund Limited.,\n", FontFactory.GetFont("TIMES_ROMAN", 16, 3, iTextSharp.text.Color.BLACK)));
                phrase.Add(new Chunk("\t\t\t\t\t\t\t\tTrial And Arrear\n", FontFactory.GetFont("Arial", 12, iTextSharp.text.Color.BLACK)));
                phrase.Add(new Chunk("\t\t\t\t\t\t\t\t Branch Name : " + captiondt.Rows[0]["B_Name"].ToString() + "; \t Running Call : " + objCOM.Str + "\n", FontFactory.GetFont("Arial", 13, iTextSharp.text.Font.NORMAL, iTextSharp.text.Color.BLACK)));
                phrase.Add(new Chunk("\t\t\t\t\t\t\t\t Group No : " + captiondt.Rows[0]["GROUPNO"].ToString() + ";\t Chit Value : " + captiondt.Rows[0]["ChitValue"].ToString()+"\n", FontFactory.GetFont("Arial", 13, iTextSharp.text.Font.NORMAL, iTextSharp.text.Color.BLACK)));
                phrase.Add(new Chunk("\t\t\t\t\t\t\t\t As On " + txtFromDate.Text, FontFactory.GetFont("Arial", 13, iTextSharp.text.Font.NORMAL, iTextSharp.text.Color.BLACK)));

                BaseFont basefont = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false);
                iTextSharp.text.Font fnt = iTextSharp.text.FontFactory.GetFont(iTextSharp.text.FontFactory.HELVETICA, 12);



                iTextSharp.text.Table table = new iTextSharp.text.Table(gridTA.Columns.Count-1);

                iTextSharp.text.Cell cellcaption = new iTextSharp.text.Cell();
                cellcaption.Add(new Phrase(phrase));
                cellcaption.Colspan = gridTA.Columns.Count-1;           
                cellcaption.HorizontalAlignment = PdfCell.ALIGN_CENTER;
                cellcaption.VerticalAlignment = PdfCell.ALIGN_TOP;
               


                table.AddCell(cellcaption);              
                table.Cellpadding = 2;              
                int[] widths = new int[gridTA.Columns.Count];
             
                for (int x = 0; x < gridTA.Columns.Count; x++)
                {
                    widths[x] = (int)gridTA.Columns[x].ItemStyle.Width.Value;
                    string cellText = Server.HtmlDecode(gridTA.HeaderRow.Cells[x].Text);

                    //sivanesan added if statement to remove member id

                    string cellTextToRemove = "Member Id";

                    if (cellText == cellTextToRemove)
                    {
                        //x--;
                        continue; // Skip this iteration
                    }


                    iTextSharp.text.Cell cell = new iTextSharp.text.Cell(cellText);
                    cell.BackgroundColor = new iTextSharp.text.Color(System
                                       .Drawing.ColorTranslator.FromHtml("#DCDCDC"));
                    table.AddCell(cell);
                }
                List<int> widthList = widths.ToList<int>();
                widthList.RemoveAt(2);
                table.SetWidths(widthList.ToArray<int>());
                //table.SetWidths(widths);

                //Transfer rows from GridView to table
                for (int i = 0; i < gridTA.Rows.Count; i++)
                {
                    BaseFont bfTimes = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false);
                    iTextSharp.text.Font font20 = iTextSharp.text.FontFactory.GetFont
                    (iTextSharp.text.FontFactory.HELVETICA, 12);

                    if (gridTA.Rows[i].RowType == DataControlRowType.DataRow)
                    {
                     
                        Label lblsno = (Label)gridTA.Rows[i].FindControl("lblchit");
                        string cellText1 = Server.HtmlDecode
                                          (lblsno.Text);                     
                          
                        iTextSharp.text.Cell cell1 = new iTextSharp.text.Cell();
                        
                        cell1.Add(new Phrase(cellText1, font20));
                        //Set Color of Alternating row
                        if (i % 2 != 0)
                        {
                            cell1.BackgroundColor = new iTextSharp.text.Color(System.Drawing
                                                .ColorTranslator.FromHtml("#FFFFFF"));
                           
                        }
                        table.AddCell(cell1);

                        //2nd column
                        Label lblmem = (Label)gridTA.Rows[i].FindControl("lblmember");
                        string cellText2 = Server.HtmlDecode
                                          (lblmem.Text);
                        iTextSharp.text.Cell cell2 = new iTextSharp.text.Cell();
                        cell2.Add(new Phrase(cellText2, font20));
                      
                        //Set Color of Alternating row
                        if (i % 2 != 0)
                        {
                            cell2.BackgroundColor = new iTextSharp.text.Color(System.Drawing
                                                .ColorTranslator.FromHtml("#FFFFFF"));
                        }
                        table.AddCell(cell2);

                        //3rd column
                        Label lblcrd = (Label)gridTA.Rows[i].FindControl("lblcredit");
                        string cellText3 = Server.HtmlDecode
                                          (lblcrd.Text);
                        iTextSharp.text.Cell cell3 = new iTextSharp.text.Cell();
                        cell3.HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT;
                        cell3.VerticalAlignment = iTextSharp.text.Element.ALIGN_RIGHT;   
                        cell3.Add(new Phrase(cellText3, font20));
                   
                        //Set Color of Alternating row
                        if (i % 2 != 0)
                        {
                            cell3.BackgroundColor = new iTextSharp.text.Color(System.Drawing
                                                .ColorTranslator.FromHtml("#FFFFFF"));
                        }
                        table.AddCell(cell3);

                        //4th column
                        Label lbldeb = (Label)gridTA.Rows[i].FindControl("lbldebit");
                        string cellText4 = Server.HtmlDecode(lbldeb.Text);

                        iTextSharp.text.Cell cell4 = new iTextSharp.text.Cell();
                        cell4.HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT;
                        cell4.VerticalAlignment = iTextSharp.text.Element.ALIGN_RIGHT;

                        // sivanesan member id
                        //objCOM.MemberId = balayer.GetSingleValue("SELECT MemberID FROM svcf.membertogroupmaster where Head_id=" + DtHeads.Rows[j]["NodeID"]);

                        //sivanesan
                        // Use the GetDebitColor method to set the background color
                        Label lblMemId = (Label)gridTA.Rows[i].FindControl("memberid");

                        //int convert = 0;//lblMemId != null ? Convert.ToInt32(Server.HtmlDecode(lblMemId.Text)) : 0;
                        //int MemId = convert;
                        int MemId = 0;
  

                        if (lblMemId != null)
                        {
                            int.TryParse(Server.HtmlDecode(lblMemId.Text), out MemId);
                        }

                        decimal debitval = decimal.TryParse(cellText4, out decimal tempDebitVal) ? tempDebitVal : 0;

                        System.Drawing.Color bgColor = GetDocumentColor(MemId, debitval);

                        iTextSharp.text.Color itextSharpColor = new iTextSharp.text.Color(bgColor.R, bgColor.G, bgColor.B);

                        // Now you can use itextSharpColor as needed
                        cell4.BackgroundColor = itextSharpColor;

                        cell4.Add(new Phrase(cellText4, font20));
                        
                        //Set Color of Alternating row
                        /*if (i % 2 != 0)
                        {
                            cell4.BackgroundColor = new iTextSharp.text.Color(System.Drawing
                                                .ColorTranslator.FromHtml("#FFFFFF"));
                        }*/
                        table.AddCell(cell4);

                        //5th column
                        Label lblexremit = (Label)gridTA.Rows[i].FindControl("lblexremit");
                        string cellText5 = Server.HtmlDecode(lblexremit.Text);
                       
                        iTextSharp.text.Cell cell5 = new iTextSharp.text.Cell();
                        cell5.HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT;
                        cell5.VerticalAlignment = iTextSharp.text.Element.ALIGN_RIGHT;  
                        cell5.Add(new Phrase(cellText5, font20));
                        
                        //Set Color of Alternating row
                        if (i % 2 != 0)
                        {
                            cell5.BackgroundColor = new iTextSharp.text.Color(System.Drawing
                                                .ColorTranslator.FromHtml("#FFFFFF"));
                        }
                        table.AddCell(cell5);

                        //6th column
                        Label lblnparrear = (Label)gridTA.Rows[i].FindControl("lblnparrear");
                        string cellText6 = Server.HtmlDecode
                                          (lblnparrear.Text);

                        iTextSharp.text.Cell cell6 = new iTextSharp.text.Cell();
                        cell6.Add(new Phrase(cellText6, font20));
                        cell6.HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT;
                        cell6.VerticalAlignment = iTextSharp.text.Element.ALIGN_RIGHT;  
                       
                        //Set Color of Alternating row
                        if (i % 2 != 0)
                        {
                            cell6.BackgroundColor = new iTextSharp.text.Color(System.Drawing
                                                .ColorTranslator.FromHtml("#FFFFFF"));
                        }
                        table.AddCell(cell6);

                        //7th column
                        Label lblparrear = (Label)gridTA.Rows[i].FindControl("lblparrear");
                        string cellText7 = Server.HtmlDecode(lblparrear.Text);

                        iTextSharp.text.Cell cell7 = new iTextSharp.text.Cell();
                        cell7.Add(new Phrase(cellText7, font20));
                        cell7.HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT;
                        cell7.VerticalAlignment = iTextSharp.text.Element.ALIGN_RIGHT;  
                       
                        //Set Color of Alternating row
                        if (i % 2 != 0)
                        {
                            cell7.BackgroundColor = new iTextSharp.text.Color(System.Drawing
                                                .ColorTranslator.FromHtml("#FFFFFF"));
                        }
                        table.AddCell(cell7);

                        //8th column
                        Label lblbranches = (Label)gridTA.Rows[i].FindControl("lblbrches");
                        string cellText8 = Server.HtmlDecode(lblbranches.Text);

                        iTextSharp.text.Cell cell8 = new iTextSharp.text.Cell();
                        cell8.Add(new Phrase(cellText8, font20));
                       
                        //Set Color of Alternating row
                        if (i % 2 != 0)
                        {
                            cell8.BackgroundColor = new iTextSharp.text.Color(System.Drawing
                                                .ColorTranslator.FromHtml("#FFFFFF"));
                        }
                        table.AddCell(cell8);

                        Label lblmobile = (Label)gridTA.Rows[i].FindControl("lblmobile");
                        string cellText9 = Server.HtmlDecode(lblmobile.Text);

                        iTextSharp.text.Cell cell9 = new iTextSharp.text.Cell();
                        cell9.Add(new Phrase(cellText9, font20));
                     
                        //Set Color of Alternating row
                        if (i % 2 != 0)
                        {
                            cell9.BackgroundColor = new iTextSharp.text.Color(System.Drawing
                                                .ColorTranslator.FromHtml("#FFFFFF"));
                        }
                        table.AddCell(cell9);

                        // }
                    }
                }

                BaseFont basefnt = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false);
                iTextSharp.text.Font fnt1 = iTextSharp.text.FontFactory.GetFont(iTextSharp.text.FontFactory.HELVETICA, 12, 3);
       
                GridViewRow row = gridTA.FooterRow;
                //Total
                table.AddCell("");

                Label tot = (Label)row.FindControl("lbltexttotal");
                string celltextfooter1 = Server.HtmlDecode
                                  (tot.Text);
                iTextSharp.text.Cell cellfooter1 = new iTextSharp.text.Cell();
                cellfooter1.Add(new Phrase(celltextfooter1, fnt1));
                table.AddCell(celltextfooter1);

                //Total Credit
                Label totcred = (Label)row.FindControl("totalcredit");
                string celltextfooter2 = Server.HtmlDecode
                                  (totcred.Text);
                iTextSharp.text.Cell cellfooter2 = new iTextSharp.text.Cell();
                cellfooter2.Add(new Phrase(celltextfooter2, fnt1));
                table.AddCell(cellfooter2);

                //Total Debit
                Label totdeb = (Label)row.FindControl("totaldebit");
                string celltextfooter3 = Server.HtmlDecode
                                  (totdeb.Text);
                iTextSharp.text.Cell cellfooter3 = new iTextSharp.text.Cell();
                cellfooter3.Add(new Phrase(celltextfooter3, fnt1));
                table.AddCell(cellfooter3);

                //Total Excess Remittance
                Label totalexcessremittance = (Label)row.FindControl("totalexcessremittance");
                string celltextfooter4 = Server.HtmlDecode
                                  (totalexcessremittance.Text);
                iTextSharp.text.Cell cellfooter4 = new iTextSharp.text.Cell();
                cellfooter4.Add(new Phrase(celltextfooter4, fnt1));
                table.AddCell(cellfooter4);

                //Total total non prized arrier
                Label totalnparrier = (Label)row.FindControl("totalnparrier");
                string celltextfooter5 = Server.HtmlDecode
                                  (totalnparrier.Text);
                iTextSharp.text.Cell cellfooter5 = new iTextSharp.text.Cell();
                cellfooter5.Add(new Phrase(celltextfooter5, fnt1));
                table.AddCell(cellfooter5);

                //Total total prized arrier
                Label totalparrier = (Label)row.FindControl("totalpaarrier");
                string celltextfooter6 = Server.HtmlDecode
                                  (totalparrier.Text);
                iTextSharp.text.Cell cellfooter6 = new iTextSharp.text.Cell();
                cellfooter6.Add(new Phrase(celltextfooter6, fnt1));
                table.AddCell(cellfooter6);
                table.AddCell("");

                //Summary - Non Prized Kasar
                double grandtotalbal = 0;
                double debitsum = 0;
                for (int i = 0; i <= tble.Rows.Count - 1; i++)
                {
                    table.AddCell("");
                    table.AddCell("");
                    switch (i)
                    {
                        case 0:
                            Label nptitle = (Label)tble.Rows[i].FindControl("Label3");
                            iTextSharp.text.Cell cellsummary1 = new iTextSharp.text.Cell();                         
                            cellsummary1.Add(new Phrase(nptitle.Text, fnt1));
                            table.AddCell(cellsummary1);


                            Label npvalue = (Label)tble.Rows[i].FindControl("lblsummary_NPkasar");
                            iTextSharp.text.Cell cellsummaryval1 = new iTextSharp.text.Cell();
                            cellsummaryval1.Colspan = 6;
                            cellsummaryval1.Add(new Phrase(npvalue.Text, fnt1));
                            table.AddCell(cellsummaryval1);
                            break;

                        case 1:
                            //Prized Kasar
                            Label prizedtitle = (Label)tble.Rows[i].FindControl("Label4");
                            iTextSharp.text.Cell cellsummary2 = new iTextSharp.text.Cell();                         
                            cellsummary2.Add(new Phrase(prizedtitle.Text, fnt1));
                            table.AddCell(cellsummary2);

                            Label prizedvalue = (Label)tble.Rows[i].FindControl("lblsummary_PKasar");
                            iTextSharp.text.Cell cellsummaryval2 = new iTextSharp.text.Cell();
                            cellsummaryval2.Colspan = 6;
                            cellsummaryval2.Add(new Phrase(prizedvalue.Text, fnt1));
                            table.AddCell(cellsummaryval2);
                            break;

                        case 2:
                            //Grand Total 
                            Label grndtitle = (Label)tble.Rows[i].FindControl("Label5");
                            iTextSharp.text.Cell cellsummary3 = new iTextSharp.text.Cell();                          
                            cellsummary3.Add(new Phrase(grndtitle.Text, fnt1));
                            table.AddCell(cellsummary3);

                            Label grandvalue = (Label)tble.Rows[i].FindControl("lblsummary_GrandTotal");
                            iTextSharp.text.Cell cellsummaryval3 = new iTextSharp.text.Cell();
                            cellsummaryval3.Colspan = 6;
                            cellsummaryval3.Add(new Phrase(grandvalue.Text, fnt1));
                            grandtotalbal = Convert.ToDouble(grandvalue.Text) - Convert.ToDouble(ViewState["Debit"]);
                            table.AddCell(cellsummaryval3);
                            break;

                        case 3:
                            //Balance DR                           
                            Label baldrtitle = (Label)tble.Rows[i].FindControl("Label6");
                            if (grandtotalbal < 0)
                            {
                                baldrtitle.Text = "Balance DR";
                            }
                            else if (grandtotalbal > 0)
                            {
                                baldrtitle.Text = "Balance CR";
                            }
                            iTextSharp.text.Cell cellsummary4 = new iTextSharp.text.Cell();                        
                            cellsummary4.Add(new Phrase(baldrtitle.Text, fnt1));
                            table.AddCell(cellsummary4);
                            Label baldrvalue = (Label)tble.Rows[i].FindControl("BalanceDR");
                            iTextSharp.text.Cell cellsummaryval4 = new iTextSharp.text.Cell();
                            cellsummaryval4.Colspan = 6;
                            cellsummaryval4.Add(new Phrase(baldrvalue.Text, fnt1));
                            table.AddCell(cellsummaryval4);
                            break;
                    }
                }
                StringWriter sw = new StringWriter();
                HtmlTextWriter hw = new HtmlTextWriter(sw);
                PrintPanel1.RenderControl(hw);
                StringReader sr = new StringReader(sw.ToString());
                iTextSharp.text.Document pdfDoc = new iTextSharp.text.Document(PageSize.LEGAL.Rotate(), 0, 0, 19f, 0);
                // iTextSharp.text.Document pdfDoc = new iTextSharp.text.Document(PageSize.LEGAL.Rotate(), 10f, 10f, 10f, 0f);
                pdfDoc.SetPageSize(new iTextSharp.text.Rectangle(900f, 1150f));
                // pdfDoc.SetPageSize(new iTextSharp.text.Rectangle(520f, 800f));
                //pdfDoc.SetPageSize(new iTextSharp.text.Rectangle(10f, 10f, 10f, 0f));
                //    string leftColumn = "Pages : [Page # of Pages #]";
                pdfDoc.NewPage();
                HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
                PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                pdfDoc.Open();
                pdfDoc.Add(table);
                //       pdfDoc.Add("Left");
                pdfDoc.Close();
                //pdfDoc.Open();
                //htmlparser.Parse(sr);
                Response.ContentType = "application/pdf";
                Response.AddHeader("content-disposition", "attachment;filename=TrialandArrear.pdf");
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.Write(pdfDoc);
                Response.Flush();
                //        StringWriter sw = new StringWriter();
                //        HtmlTextWriter hw = new HtmlTextWriter(sw);
                //        PrintPanel1.RenderControl(hw);
                //        StringReader sr = new StringReader(sw.ToString());
                //       iTextSharp.text.Document pdfDoc = new iTextSharp.text.Document(PageSize.LEGAL.Rotate(), 0, 0, 19f, 0);
                //       // iTextSharp.text.Document pdfDoc = new iTextSharp.text.Document(PageSize.LEGAL.Rotate(), 10f, 10f, 10f, 0f);
                //       //pdfDoc.SetPageSize(new iTextSharp.text.Rectangle(900f, 1150f));
                //     pdfDoc.SetPageSize(new iTextSharp.text.Rectangle(520f, 800f));
                //        //pdfDoc.SetPageSize(new iTextSharp.text.Rectangle(10f, 10f, 10f, 0f));
                ////    string leftColumn = "Pages : [Page # of Pages #]";
                //        pdfDoc.NewPage();
                //        HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
                //        PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                //        pdfDoc.Open();
                //        pdfDoc.Add(table);
                // //       pdfDoc.Add("Left");
                //        pdfDoc.Close();
                //        //pdfDoc.Open();
                //        //htmlparser.Parse(sr);
                //        Response.ContentType = "application/pdf";
                //        Response.AddHeader("content-disposition", "attachment;filename=TrialandArrear.pdf");
                //        Response.Cache.SetCacheability(HttpCacheability.NoCache);
                //        Response.Write(pdfDoc);               
                //        Response.Flush();              
            }
          
            catch (Exception err) { }

          }
        protected void BtnStatisticsGo_Click(object sender, EventArgs e)
        {
            try
            {
                //GetMethods getmethods = new GetMethods();
                //DataTable getta = new DataTable();
                //DataTable sdt = new DataTable();

                //IFormatProvider culture3 = new System.Globalization.CultureInfo("fr-FR", true);
                //DateTime frmdate = DateTime.Parse(txtFromDate.Text, culture3, System.Globalization.DateTimeStyles.AssumeLocal);
                //string frmdt = balayer.changedateformat(frmdate, 2).ToString();

                //getta = getmethods.GetTrialArrear(frmdt, Convert.ToInt32(ddlChit.SelectedValue), Convert.ToInt32(Session["Branchid"]));
                //ViewState["Credit"] = getta.Compute("Sum(Credit)", "");
                //ViewState["Debit"] = getta.Compute("Sum(Debit)", "");
                //ViewState["NPKasar"] = getta.Compute("Sum(NPKasar)", "");
                //ViewState["PKasar"] = getta.Compute("Sum(PKasar)", "");

                //sdt = balayer.GetDataTable("SELECT `groupmaster`.`NoofMembers`,`groupmaster`.`GROUPNO`,`branchdetails`.`B_Name`,max(`auctiondetails`.`DrawNO`) as `RunningCall`,`groupmaster`.`ChitValue` FROM svcf.groupmaster join auctiondetails on (`groupmaster`.`Head_Id`=`auctiondetails`.`GroupID`) join `branchdetails` on (`groupmaster`.`BranchID`=`branchdetails`.`Head_Id`) where `groupmaster`.`Head_Id`=" + ddlChit.SelectedValue + " and `auctiondetails`.`AuctionDate`<='" + balayer.indiandateToMysqlDate(txtFromDate.Text) + "'  and `auctiondetails`.`BranchID`=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + ";");
                //str = Convert.ToString(sdt.Rows[0]["RunningCall"]) == "" ? "0" : Convert.ToString(sdt.Rows[0]["RunningCall"]);
                //grid.SettingsText.Title = "Sree Visalam Chit Fund Limited.,<br/>Trial And Arrear <br/> Branch Name : " + sdt.Rows[0]["B_Name"].ToString() + "; \t Running Call : " + str + " <br/> Group No : " + sdt.Rows[0]["GROUPNO"].ToString() + "; \t Chit Amount : " + sdt.Rows[0]["ChitValue"].ToString() + "; \t for the month of " + Convert.ToDateTime(txtFromDate.Text).ToString("MMMM yyyy");
                //grid.Settings.ShowTitlePanel = true;
                //dv = getta.DefaultView;
                //dv.Sort = "ChitNo1 asc";
                //sortedDT = dv.ToTable();
                //grid.DataSource = sortedDT;
                //grid.DataBind();
                lblsummary_NPkasar.Text = "";
                lblsummary_PKasar.Text = "";
                lblsummary_GrandTotal.Text = "";
                lblsummary_BalanceDR.Text = "";
                //lblsummary_DR.Text = "";
                //lblsummary_CR.Text = "";
                lblsummary_BalanceCR.Text= "";
                select();
            }
            catch (Exception err) { }
            //select();
        }
        protected void lbBalanceCR_Load(object sender, EventArgs e)
        {
            Label label = (Label)sender;
            if (ViewState["Credit"] != null)
                label.Text = Math.Abs((Convert.ToDecimal(ViewState["Credit"]) + Convert.ToDecimal(ViewState["NPKasar"]) + Convert.ToDecimal(ViewState["PKasar"])) - Convert.ToDecimal(ViewState["Debit"])).ToString();
        }
       
        protected void lbDebitTotal_Load(object sender, EventArgs e)
        {
            Label label = (Label)sender;
            if (ViewState["Credit"] != null)
                label.Text = Convert.ToString(ViewState["Debit"]);
        }
        protected void lbCredit_Load(object sender, EventArgs e)
        {
            Label label = (Label)sender;
            if (ViewState["Credit"] != null)
                label.Text = Convert.ToString(ViewState["Credit"]);
        }
        protected void lbNonPrizedKasar_Load(object sender, EventArgs e)
        {
            Label label = (Label)sender;
            if (ViewState["Credit"] != null)
                label.Text = Convert.ToString(ViewState["NPKasar"]);
        }
        protected void lbPrizedKasar_Load(object sender, EventArgs e)
        {
            Label label = (Label)sender;
            if (ViewState["Credit"] != null)
                label.Text = Convert.ToString(ViewState["PKasar"]);
        }
        protected void lbGrantTotal_Load(object sender, EventArgs e)
        {
            Label label = (Label)sender;
            if (ViewState["Credit"] != null)
                label.Text = Convert.ToString((Convert.ToDecimal(ViewState["Credit"]) + Convert.ToDecimal(ViewState["NPKasar"]) + Convert.ToDecimal(ViewState["PKasar"])));
        }


        protected void Export_click(object sender, MenuItemEventArgs e)
        {
            using (this.headerImage = System.Drawing.Image.FromFile(Server.MapPath("~\\Images\\visalam.png")))
            {
                if (e.Item.Text.ToString() == "PDF")
                {
                    if (ddlChit.SelectedItem.Value != "--select--")
                    {
                        DataTable sssssssss = balayer.GetDataTable("SELECT `groupmaster`.`GROUPNO`,`branchdetails`.`B_Name`,max(`auctiondetails`.`DrawNO`) as `RunningCall`,`groupmaster`.`ChitValue` FROM svcf.groupmaster join auctiondetails on (`groupmaster`.`Head_Id`=`auctiondetails`.`GroupID`) join `branchdetails` on (`groupmaster`.`BranchID`=`branchdetails`.`Head_Id`) where `groupmaster`.`Head_Id`=" + ddlChit.SelectedValue + " and `auctiondetails`.`AuctionDate`<='" + balayer.indiandateToMysqlDate(txtFromDate.Text) + "'  and `auctiondetails`.`BranchID`=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + ";");
                        //grid.SettingsText.Title = "Trial And Arrear \n\r Branch Name : " + sssssssss.Rows[0]["B_Name"].ToString() + " \t Running Call : " + sssssssss.Rows[0]["RunningCall"].ToString() + " \n\r Group No : " + sssssssss.Rows[0]["GROUPNO"].ToString() + " \t Chit Amount : " + sssssssss.Rows[0]["ChitValue"].ToString();
                        //grid.Settings.ShowTitlePanel = true;
                        gridTA.Caption = "Trial And Arrear \n\r Branch Name : " + sssssssss.Rows[0]["B_Name"].ToString() + " \t Running Call : " + sssssssss.Rows[0]["RunningCall"].ToString() + " \n\r Group No : " + sssssssss.Rows[0]["GROUPNO"].ToString() + " \t Chit Amount : " + sssssssss.Rows[0]["ChitValue"].ToString();
                    }
                    else
                    {
                        //grid.SettingsText.Title = "Trial And Arrear";
                        gridTA.Caption = "Trial And Arrear";
                        //grid.Settings.ShowTitlePanel = true;
                    }


                    //grid.DataSource = (DataTable)ViewState["Exportdt"];
                    //grid.DataBind();

                    //gridExport.Styles.Header.HorizontalAlign = HorizontalAlign.Center;
                    //gridExport.Styles.Header.VerticalAlign = VerticalAlign.Middle;
                    //gridExport.Styles.Header.Wrap = DevExpress.Utils.DefaultBoolean.True;
                    //gridExport.Styles.Footer.HorizontalAlign = HorizontalAlign.Right;
                    PrintingSystem ps = new PrintingSystem();
                    PrintableComponentLink gridPayment = new PrintableComponentLink(ps);
                    // gridPayment.Component = gridExport;
                    Link header = new Link();
                    CompositeLink compositeLink = new CompositeLink(ps);
                    header.CreateDetailArea += new CreateAreaEventHandler(header_CreateDetailArea);

                    compositeLink.Links.AddRange(new object[] { header, gridPayment });

                    string leftColumn = "Pages : [Page # of Pages #]";
                    string rightColumn = "Date: [Date Printed]\r\nTime: " + DateTime.Now.ToString("hh:mm tt");
                    PageHeaderFooter phf = compositeLink.PageHeaderFooter as PageHeaderFooter;
                    phf.Footer.Content.Clear();
                    phf.Footer.Content.AddRange(new string[] { rightColumn, leftColumn });
                    phf.Footer.LineAlignment = BrickAlignment.Center;

                    using (MemoryStream stream = new MemoryStream())
                    {
                        compositeLink.PaperKind = System.Drawing.Printing.PaperKind.A3;
                        //compositeLink.Landscape = true;
                        compositeLink.CreateDocument(false);
                        compositeLink.PrintingSystem.Document.AutoFitToPagesWidth = 1;
                        compositeLink.PrintingSystem.ExportToPdf(stream);
                        WriteToResponse("TrialandArrear", true, "pdf", stream);
                    }
                }

                else if (e.Item.Text.ToString() == "XLSX")
                {
                    // gridExport.WriteXlsxToResponse();
                }
            }
            //ViewState["Exportdt"] = null;
        }


        protected void Exportold_click(object sender, MenuItemEventArgs e)
        {
            using (this.headerImage = System.Drawing.Image.FromFile(Server.MapPath("~\\Images\\visalam.png")))
            {
                if (e.Item.Text.ToString() == "PDF")
                {
                    if (ddlChit.SelectedItem.Value != "--select--")
                    {
                        DataTable sssssssss = balayer.GetDataTable("SELECT `groupmaster`.`GROUPNO`,`branchdetails`.`B_Name`,max(`auctiondetails`.`DrawNO`) as `RunningCall`,`groupmaster`.`ChitValue` FROM svcf.groupmaster join auctiondetails on (`groupmaster`.`Head_Id`=`auctiondetails`.`GroupID`) join `branchdetails` on (`groupmaster`.`BranchID`=`branchdetails`.`Head_Id`) where `groupmaster`.`Head_Id`=" + ddlChit.SelectedValue + " and `auctiondetails`.`AuctionDate`<='" + balayer.indiandateToMysqlDate(txtFromDate.Text) + "'  and `auctiondetails`.`BranchID`=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + ";");
                        //grid.SettingsText.Title = "Trial And Arrear \n\r Branch Name : " + sssssssss.Rows[0]["B_Name"].ToString() + " \t Running Call : " + sssssssss.Rows[0]["RunningCall"].ToString() + " \n\r Group No : " + sssssssss.Rows[0]["GROUPNO"].ToString() + " \t Chit Amount : " + sssssssss.Rows[0]["ChitValue"].ToString();
                        //grid.Settings.ShowTitlePanel = true;
                        gridTA.Caption = "Trial And Arrear \n\r Branch Name : " + sssssssss.Rows[0]["B_Name"].ToString() + " \t Running Call : " + sssssssss.Rows[0]["RunningCall"].ToString() + " \n\r Group No : " + sssssssss.Rows[0]["GROUPNO"].ToString() + " \t Chit Amount : " + sssssssss.Rows[0]["ChitValue"].ToString();
                    }
                    else
                    {
                        //  grid.SettingsText.Title = "Trial And Arrear";
                        gridTA.Caption = "Trial And Arrear";
                        //  grid.Settings.ShowTitlePanel = true;
                    }
                    //select();                  
                    //grid.DataSource = (DataTable)ViewState["Exportdt"];                   
                    //grid.DataBind();

                    //gridExport.Styles.Header.HorizontalAlign = HorizontalAlign.Center;
                    //gridExport.Styles.Header.VerticalAlign = VerticalAlign.Middle;
                    //gridExport.Styles.Header.Wrap = DevExpress.Utils.DefaultBoolean.True;
                    //gridExport.Styles.Footer.HorizontalAlign = HorizontalAlign.Right;

                    PrintingSystem ps = new PrintingSystem();
                    PrintableComponentLink gridPayment = new PrintableComponentLink(ps);
                    Link header = new Link();
                    //  gridPayment.Component = gridExport;
                    CompositeLink compositeLink = new CompositeLink(ps);
                    header.CreateDetailArea += new CreateAreaEventHandler(header_CreateDetailArea);



                    string leftColumn = "Pages : [Page # of Pages #]";
                    string rightColumn = "Date: [Date Printed]\r\nTime: " + DateTime.Now.ToString("hh:mm tt");
                    PageHeaderFooter phf = compositeLink.PageHeaderFooter as PageHeaderFooter;
                    phf.Footer.Content.Clear();
                    phf.Footer.Content.AddRange(new string[] { rightColumn, leftColumn });
                    phf.Footer.LineAlignment = BrickAlignment.Center;

                    compositeLink.Links.AddRange(new object[] { header, gridPayment, phf });
                    //phf.Header.Font = new Font("Arial", 12, FontStyle.Bold);
                    //phf.Footer.Font = new Font("Arial", 12, FontStyle.Regular);
                    using (MemoryStream stream = new MemoryStream())
                    {
                        compositeLink.PaperKind = System.Drawing.Printing.PaperKind.Legal;
                        compositeLink.EnablePageDialog = true;
                        // compositeLink.Landscape = true;
                        compositeLink.CreateDocument(false);
                        compositeLink.PrintingSystem.Document.AutoFitToPagesWidth = 1;

                        compositeLink.PrintingSystem.ExportToPdf(stream);
                        WriteToResponse("TrailandArrear", true, "pdf", stream);
                    }
                }

                else if (e.Item.Text.ToString() == "XLSX")
                {
                    // gridExport.WriteXlsxToResponse();
                }
            }
            ViewState["Exportdt"] = null;
        }
        void header_CreateDetailArea(object sender, CreateAreaEventArgs e)
        {
            //e.Graph.BorderWidth = 0;
            //Rectangle r = new Rectangle(0, 0, 50, 50);
            //e.Graph.DrawImage(headerImage, r);
            //TextBrick tb = new TextBrick();
            //tb.Text = "SREE VISALAM CHIT FUND LTD.,";
            //tb.Font = new Font("Arial", 10, FontStyle.Bold);
            //tb.Rect = new RectangleF(50, 15, 260, 19);
            //tb.BorderWidth = 0;
            //tb.BackColor = Color.Transparent;
            //tb.HorzAlignment = DevExpress.Utils.HorzAlignment.Center;
            //tb.VertAlignment = DevExpress.Utils.VertAlignment.Top;
            //e.Graph.DrawBrick(tb);
            //TextBrick tb1 = new TextBrick();
            //tb1.Text = "BRANCH : " + Session["BranchName"];
            //tb1.Font = new Font("Arial", 9, FontStyle.Bold);
            //tb1.Rect = new RectangleF(50, 34, 260, 25);
            //tb1.BorderWidth = 0;
            //tb1.BackColor = Color.Transparent;
            //tb1.HorzAlignment = DevExpress.Utils.HorzAlignment.Center;
            //tb1.VertAlignment = DevExpress.Utils.VertAlignment.Top;
            //e.Graph.DrawBrick(tb1);
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

        protected void gridTA_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                if (ViewState["Credit"] != null)
                {
                    // Credit TOTAL.
                    Label lblcredit = (Label)e.Row.FindControl("totalcredit");
                    lblcredit.Text = ViewState["Credit"].ToString();

                    //Debit TOTAL.
                    Label lbltotaldebit = (Label)e.Row.FindControl("totaldebit");
                    lbltotaldebit.Text = ViewState["Debit"].ToString();

                    //Excess Remittance TOTAL.
                    Label lblexcess = (Label)e.Row.FindControl("totalexcessremittance");
                    lblexcess.Text = ViewState["ExcessRemittance"].ToString();

                    //Prized TOTAL.
                    Label lblNParrier = (Label)e.Row.FindControl("totalnparrier");
                    lblNParrier.Text = ViewState["NPArrier"].ToString();

                    //Non Prized TOTAL.
                    Label lbltotalpaarrier = (Label)e.Row.FindControl("totalpaarrier");
                    lbltotalpaarrier.Text = ViewState["PArrier"].ToString();
                }
            }

        }
    }
}
