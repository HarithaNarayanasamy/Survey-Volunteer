using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SVCF_TransactionLayer;
using SVCF_DataAccessLayer;
using SVCF_BusinessAccessLayer;
using System.Data;
namespace SreeVisalamChitFundLtd_phase1
{
    public partial class taarrear : System.Web.UI.Page
    {
        string data;
        DataTable dtChitGrp = new DataTable();
        DataRow dr;
        BusinessLayer balayer = new BusinessLayer();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetGroupMember();               
            }
        }
        public void GetGroupMember()
        {
            ddlChit.DataSource = null;
            ddlChit.DataBind();
            data = "SELECT `groupmaster`.`GROUPNO`,`groupmaster`.`Head_Id` FROM `svcf`.`groupmaster` where `groupmaster`.`IsFinished`=0 and `groupmaster`.`BranchID`=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + "";
            dtChitGrp = balayer.GetDataTable(data);
            ddlChit.DataSource = dtChitGrp;
            dr = dtChitGrp.NewRow();
            dr[0] = "--select--";
            dr[1] = "0";
            ddlChit.DataTextField = "GROUPNO";
            ddlChit.DataValueField = "Head_Id";
            dtChitGrp.Rows.InsertAt(dr, 0);
            ddlChit.DataBind();
        }

        [System.Web.Services.WebMethod]
        public static string VCList_Date(string frmdt,int gpid)
        {
            DataSet ds = new DataSet();
            try
            {
                BusinessLayer balayer = new BusinessLayer();
                IFormatProvider culture3 = new System.Globalization.CultureInfo("fr-FR", true);
                DateTime fromdate = DateTime.Parse(frmdt, culture3, System.Globalization.DateTimeStyles.AssumeLocal);

                DataTable dtBind = new DataTable();
                DataTable dtInit = new DataTable();
                DataTable dtHeads = new DataTable();
                DataTable gts = new DataTable();
                string sss, strName, strMemID, credit, debit, excess, nparr, parr, npkas, pkas, strMember, strBranches;

                DataRow drBind;
                dtBind.Columns.Add("ChitNo1", typeof(int));
                dtBind.Columns.Add("MemberName");
                dtBind.Columns.Add("Credit", typeof(decimal));
                dtBind.Columns.Add("Debit", typeof(decimal));
                dtBind.Columns.Add("ExcessRemittance", typeof(decimal));
                dtBind.Columns.Add("NPArrier", typeof(decimal));
                dtBind.Columns.Add("PArrier", typeof(decimal));
                dtBind.Columns.Add("NPKasar", typeof(decimal));
                dtBind.Columns.Add("PKasar", typeof(decimal));
                dtBind.Columns.Add("Branches");
                drBind = dtBind.NewRow();

                balayer.GetInsertItem("CREATE OR REPLACE VIEW `unpaidprizedmoney` AS select `voucher`.`Head_Id` AS `Head_Id`, `membertogroupmaster`.`GrpMemberID` AS `GrpMemberID`, (case when (`voucher`.`Voucher_Type` = 'C') then sum(`voucher`.`Amount`) else 0.00 end) AS `Credit`, (case when (`voucher`.`Voucher_Type` = 'D') then sum(`voucher`.`Amount`) else 0.00 end) AS `Debit`, ((case when (`voucher`.`Voucher_Type` = 'C') then sum(`voucher`.`Amount`) else 0.00 end) - (case when (`voucher`.`Voucher_Type` = 'D') then sum(`voucher`.`Amount`) else 0.00 end)) AS `AmountActuallyremittedbytheParty` from (`voucher` join `membertogroupmaster` ON ((`voucher`.`Head_Id` = `membertogroupmaster`.`Head_Id`))) group by `voucher`.`Head_Id`");
                balayer.GetInsertItem("create or replace view `view_groupwisedue` as select `groupmaster`.`GROUPNO` AS `GroupIDOriginal`,`groupmaster`.`IsFinished`, `auctiondetails`.`GroupID` AS `GroupId`, sum(`auctiondetails`.`CurrentDueAmount`) AS `TotaldueAmount` from (`auctiondetails` join `groupmaster` ON ((`auctiondetails`.`GroupID` = `groupmaster`.`Head_Id`))) where (`auctiondetails`.`AuctionDate` <= '" + balayer.indiandateToMysqlDate(frmdt) + "') group by `auctiondetails`.`GroupID`");
                if (gpid == 0)
                {
                    //grid.SettingsText.Title = "Trial And Arrear";
                    //grid.DataSource = dtBind;
                }
                else
                {
                    dtInit = balayer.GetDataTable("select * from auctiondetails where IsPrized='Y' and AuctionDate<='" + balayer.indiandateToMysqlDate(frmdt) + "' and GroupID=" + gpid);
                    if (dtInit.Rows.Count > 0)
                    {
                        dtHeads = balayer.GetDataTable(" select NodeID from headstree where ParentID=" + gpid);
                        for (int j = 0; j < dtHeads.Rows.Count; j++)
                        {
                            sss = balayer.GetSingleValue(@"select cast(digits(mg1.GrpMemberID) as unsigned) as ChitNo1 from membertogroupmaster as mg1 join voucher as v1 on v1.Head_Id=mg1.Head_Id join view_groupwisedue as vgwd1 on vgwd1.`GroupId`=" + gpid + " left join trans_payment as tp1 on v1.Head_Id =tp1.TokenNumber join membermaster as mm on (mg1.MemberID=mm.MemberIDNew) where mg1.BranchID=" + balayer.ToobjectstrEvenNull(HttpContext.Current.Session["Branchid"]) + " and mg1.GroupID=" + gpid + " and v1.Head_Id=" + dtHeads.Rows[j]["NodeID"] + " and v1.ChoosenDate<= '" + balayer.indiandateToMysqlDate(frmdt) + "' group by v1.Head_Id order by cast(digits(mg1.GrpMemberID) as unsigned),v1.Voucher_Type DESC ;");
                            if (string.IsNullOrEmpty(sss))
                            {
                                drBind["ChitNo1"] = balayer.GetSingleValue("select cast(digits(GrpMemberID) as unsigned) as ChitNo1 from membertogroupmaster where Head_Id=" + dtHeads.Rows[j]["NodeID"]);
                            }
                            else
                            {
                                drBind["ChitNo1"] = sss;
                            }
                            strName = balayer.GetSingleValue(@"select concat(mg1.MemberName) as `MemberName` from membertogroupmaster as mg1 join voucher as v1 on v1.Head_Id=mg1.Head_Id join view_groupwisedue as vgwd1 on vgwd1.`GroupId`=" + gpid + " left join trans_payment as tp1 on v1.Head_Id =tp1.TokenNumber join membermaster as mm on (mg1.MemberID=mm.MemberIDNew) where mg1.BranchID=" + balayer.ToobjectstrEvenNull(HttpContext.Current.Session["Branchid"]) + " and mg1.GroupID=" + gpid + " and v1.Head_Id=" + dtHeads.Rows[j]["NodeID"] + " and v1.ChoosenDate<= '" + balayer.indiandateToMysqlDate(frmdt) + "' group by v1.Head_Id order by v1.ChoosenDate DESC ;");

                            if (string.IsNullOrEmpty(strName))
                            {
                                strMemID = balayer.GetSingleValue("SELECT MemberID FROM svcf.membertogroupmaster where Head_id=" + dtHeads.Rows[j]["NodeID"]);
                                drBind["MemberName"] = balayer.GetSingleValue("SELECT CustomerName FROM svcf.membermaster where MemberIDNew=" + strMemID);
                            }
                            else
                            {
                                drBind["MemberName"] = strName;
                            }
                            credit = balayer.GetSingleValue(@"select (case when( tp1.PaymentDate is null or tp1.PaymentDate >'" + balayer.indiandateToMysqlDate(frmdt) + "') then sum(case when (v1.Voucher_Type='C' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) -sum(case when (v1.Voucher_Type='D' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) else 0.00 end ) as Credit from membertogroupmaster as mg1 join voucher as v1 on v1.Head_Id=mg1.Head_Id join view_groupwisedue as vgwd1 on vgwd1.`GroupId`=" + gpid + " left join trans_payment as tp1 on v1.Head_Id =tp1.TokenNumber join membermaster as mm on (mg1.MemberID=mm.MemberIDNew) where mg1.BranchID=" + balayer.ToobjectstrEvenNull(HttpContext.Current.Session["Branchid"]) + " and mg1.GroupID=" + gpid + " and v1.Head_Id=" + dtHeads.Rows[j]["NodeID"] + " and v1.ChoosenDate<= '" + balayer.indiandateToMysqlDate(frmdt) + "' group by v1.Head_Id order by cast(digits(mg1.GrpMemberID) as unsigned),v1.Voucher_Type DESC ;");
                            if (string.IsNullOrEmpty(credit))
                            {
                                drBind["Credit"] = "0.00";
                            }
                            else
                            {
                                drBind["Credit"] = credit;
                            }
                            debit = balayer.GetSingleValue(@"select (case when( tp1.PaymentDate<='" + balayer.indiandateToMysqlDate(frmdt) + "' ) then sum((case when (v1.Voucher_Type='D' and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) -case when (v1.Voucher_Type='C' and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) else 0.00 end ) as Debit from membertogroupmaster as mg1 join voucher as v1 on v1.Head_Id=mg1.Head_Id join view_groupwisedue as vgwd1 on vgwd1.`GroupId`=" + gpid+ " left join trans_payment as tp1 on v1.Head_Id =tp1.TokenNumber join membermaster as mm on (mg1.MemberID=mm.MemberIDNew) where mg1.BranchID=" + balayer.ToobjectstrEvenNull(HttpContext.Current.Session["Branchid"]) + " and mg1.GroupID=" + gpid+ " and v1.Head_Id=" + dtHeads.Rows[j]["NodeID"] + " and v1.ChoosenDate<= '" + balayer.indiandateToMysqlDate(frmdt) + "' group by v1.Head_Id order by cast(digits(mg1.GrpMemberID) as unsigned),v1.Voucher_Type DESC ;");
                            if (string.IsNullOrEmpty(debit))
                            {
                                drBind["Debit"] = "0.00";
                            }
                            else
                            {
                                drBind["Debit"] = debit;
                            }
                            excess = balayer.GetSingleValue(@"select  (case when( (sum(case when (v1.Voucher_Type='C' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) -sum(case when (v1.Voucher_Type='D' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) -vgwd1.TotaldueAmount)>0.00) then (sum(case when (v1.Voucher_Type='C' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) -sum(case when (v1.Voucher_Type='D' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) -vgwd1.TotaldueAmount) else 0.00 end) as ExcessRemittance from membertogroupmaster as mg1 join voucher as v1 on v1.Head_Id=mg1.Head_Id join view_groupwisedue as vgwd1 on vgwd1.`GroupId`=" + gpid + " left join trans_payment as tp1 on v1.Head_Id =tp1.TokenNumber join membermaster as mm on (mg1.MemberID=mm.MemberIDNew) where mg1.BranchID=" + balayer.ToobjectstrEvenNull(HttpContext.Current.Session["Branchid"]) + " and mg1.GroupID=" + gpid + " and v1.Head_Id=" + dtHeads.Rows[j]["NodeID"] + " and v1.ChoosenDate<= '" + balayer.indiandateToMysqlDate(frmdt) + "' group by v1.Head_Id order by cast(digits(mg1.GrpMemberID) as unsigned),v1.Voucher_Type DESC ;");
                            if (string.IsNullOrEmpty(excess))
                            {
                                drBind["ExcessRemittance"] = "0.00";
                            }
                            else
                            {
                                drBind["ExcessRemittance"] = excess;
                            }
                            nparr = balayer.GetSingleValue(@"select (case when( tp1.PaymentDate is null  or tp1.PaymentDate >'" + balayer.indiandateToMysqlDate(frmdt) + "') then (case when( (vgwd1.TotaldueAmount-sum(case when (v1.Voucher_Type='C' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) +sum(case when (v1.Voucher_Type='D' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) )>0.00) then (vgwd1.TotaldueAmount-sum(case when (v1.Voucher_Type='C' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) +sum(case when (v1.Voucher_Type='D' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) ) else 0.00 end) else 0.00 end) as NPArrier from membertogroupmaster as mg1 join voucher as v1 on v1.Head_Id=mg1.Head_Id join view_groupwisedue as vgwd1 on vgwd1.`GroupId`=" + gpid + " left join trans_payment as tp1 on v1.Head_Id =tp1.TokenNumber join membermaster as mm on (mg1.MemberID=mm.MemberIDNew) where mg1.BranchID=" + balayer.ToobjectstrEvenNull(HttpContext.Current.Session["Branchid"]) + " and mg1.GroupID=" + gpid + " and v1.Head_Id=" + dtHeads.Rows[j]["NodeID"] + " and v1.ChoosenDate<= '" + balayer.indiandateToMysqlDate(frmdt) + "' group by v1.Head_Id order by cast(digits(mg1.GrpMemberID) as unsigned),v1.Voucher_Type DESC ;");
                            if (string.IsNullOrEmpty(nparr))
                            {
                                drBind["NPArrier"] = "0.00";
                            }
                            else
                            {
                                drBind["NPArrier"] = nparr;
                            }
                            parr = balayer.GetSingleValue(@"select (case when( tp1.PaymentDate<='" + balayer.indiandateToMysqlDate(frmdt) + "' ) then (case when( (vgwd1.TotaldueAmount-sum(case when (v1.Voucher_Type='C' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) +sum(case when (v1.Voucher_Type='D' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) )>0.00) then (vgwd1.TotaldueAmount-sum(case when (v1.Voucher_Type='C' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) +sum(case when (v1.Voucher_Type='D' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) ) else 0.00 end) else 0.00 end ) as PArrier from membertogroupmaster as mg1 join voucher as v1 on v1.Head_Id=mg1.Head_Id join view_groupwisedue as vgwd1 on vgwd1.`GroupId`=" + gpid + " left join trans_payment as tp1 on v1.Head_Id =tp1.TokenNumber join membermaster as mm on (mg1.MemberID=mm.MemberIDNew) where mg1.BranchID=" + balayer.ToobjectstrEvenNull(HttpContext.Current.Session["Branchid"]) + " and mg1.GroupID=" + gpid + " and v1.Head_Id=" + dtHeads.Rows[j]["NodeID"] + " and v1.ChoosenDate<= '" + balayer.indiandateToMysqlDate(frmdt) + "' group by v1.Head_Id order by cast(digits(mg1.GrpMemberID) as unsigned),v1.Voucher_Type DESC ;");
                            if (string.IsNullOrEmpty(excess))
                            {
                                drBind["PArrier"] = "0.00";
                            }
                            else
                            {
                                drBind["PArrier"] = parr;
                            }
                            npkas = balayer.GetSingleValue(@"select (case when( tp1.PaymentDate is null  or tp1.PaymentDate >'" + balayer.indiandateToMysqlDate(frmdt) + "') then sum((case when (v1.Voucher_Type='C' and v1.Other_Trans_Type=5 ) then v1.Amount else 0.00 end)) else 0.00 end ) as NPKasar from membertogroupmaster as mg1 join voucher as v1 on v1.Head_Id=mg1.Head_Id join view_groupwisedue as vgwd1 on vgwd1.`GroupId`=" + gpid + " left join trans_payment as tp1 on v1.Head_Id =tp1.TokenNumber join membermaster as mm on (mg1.MemberID=mm.MemberIDNew) where mg1.BranchID=" + balayer.ToobjectstrEvenNull(HttpContext.Current.Session["Branchid"]) + " and mg1.GroupID=" + gpid + " and v1.Head_Id=" + dtHeads.Rows[j]["NodeID"] + " and v1.ChoosenDate<= '" + balayer.indiandateToMysqlDate(frmdt) + "' group by v1.Head_Id order by cast(digits(mg1.GrpMemberID) as unsigned),v1.Voucher_Type DESC ;");
                            if (string.IsNullOrEmpty(excess))
                            {
                                drBind["NPKasar"] = "0.00";
                            }
                            else
                            {
                                drBind["NPKasar"] = npkas;
                            }
                            pkas = balayer.GetSingleValue(@"select (case when( tp1.PaymentDate<='" + balayer.indiandateToMysqlDate(frmdt) + "' ) then sum((case when (v1.Voucher_Type='C' and v1.Other_Trans_Type=5 ) then v1.Amount else 0.00 end)) else 0.00 end ) as PKasar from membertogroupmaster as mg1 join voucher as v1 on v1.Head_Id=mg1.Head_Id join view_groupwisedue as vgwd1 on vgwd1.`GroupId`=" + gpid + " left join trans_payment as tp1 on v1.Head_Id =tp1.TokenNumber join membermaster as mm on (mg1.MemberID=mm.MemberIDNew) where mg1.BranchID=" + balayer.ToobjectstrEvenNull(HttpContext.Current.Session["Branchid"]) + " and mg1.GroupID=" + gpid + " and v1.Head_Id=" + dtHeads.Rows[j]["NodeID"] + " and v1.ChoosenDate<= '" + balayer.indiandateToMysqlDate(frmdt) + "' group by v1.Head_Id order by cast(digits(mg1.GrpMemberID) as unsigned),v1.Voucher_Type DESC ;");
                            if (string.IsNullOrEmpty(excess))
                            {
                                drBind["PKasar"] = "0.00";
                            }
                            else
                            {
                                drBind["PKasar"] = pkas;
                            }
                            strMember = balayer.GetSingleValue("SELECT Head_Id FROM svcf.voucher where Head_id=" + dtHeads.Rows[j]["NodeID"] + " and ChoosenDate<='" + balayer.indiandateToMysqlDate(frmdt) + "' order by ChoosenDate desc");

                            if (string.IsNullOrEmpty(strMember))
                            {
                                strMember = "0";
                            }
                            strBranches = balayer.GetSingleValue("SELECT b1.Place FROM svcf.membermaster as m1 join svcf.branchdetails as b1 on (m1.BranchID=b1.Head_Id) join svcf.membertogroupmaster as mg1 on mg1.MemberID=m1.MemberIDNew where mg1.Head_Id=" + strMember);


                            if (string.IsNullOrEmpty(strBranches))
                            {
                                drBind["Branches"] = balayer.GetSingleValue("SELECT b1.Place FROM svcf.membertogroupmaster as m1 join branchdetails as b1 on (m1.B_Id=b1.Head_Id) where m1.Head_id=" + dtHeads.Rows[j]["NodeID"]);
                            }
                            else
                            {
                                drBind["Branches"] = strBranches;
                            }
                            dtBind.Rows.Add(drBind.ItemArray);
                        }
                        // ViewState["Credit"] = dtBind.Compute("Sum(Credit)", "");
                        //  ViewState["Debit"] = dtBind.Compute("Sum(Debit)", "");
                        // ViewState["NPKasar"] = dtBind.Compute("Sum(NPKasar)", "");
                        //  ViewState["PKasar"] = dtBind.Compute("Sum(PKasar)", "");
                    }
                    else
                    {
                        // DataTable dtHeads = balayer.GetDataTable(" select NodeID from headstree where ParentID=" + ddlChit.SelectedItem.Value);
                        DataTable dtHds1 = new DataTable();
                        strName = "";
                        decimal decCredit = 0;
                        decimal decDebit = 0;
                        dtHds1 = balayer.GetDataTable(" select h1.NodeID from svcf.headstree h1 right join svcf.membertogroupmaster mg1 on mg1.Head_Id=h1.NodeID where h1.ParentID='" + gpid + "' group by h1.NodeID");


                        for (int j = 0; j < dtHds1.Rows.Count; j++)
                        {
                            sss = balayer.GetSingleValue(@"select cast(digits(mg1.GrpMemberID) as unsigned) as ChitNo1 from membertogroupmaster as mg1 join voucher as v1 on v1.Head_Id=mg1.Head_Id join view_groupwisedue as vgwd1 on vgwd1.`GroupId`=" + gpid + " left join trans_payment as tp1 on v1.Head_Id =tp1.TokenNumber join membermaster as mm on (mg1.MemberID=mm.MemberIDNew) where mg1.BranchID=" + balayer.ToobjectstrEvenNull(HttpContext.Current.Session["Branchid"]) + " and mg1.GroupID=" + gpid + " and v1.Head_Id=" + dtHds1.Rows[j]["NodeID"] + " and v1.ChoosenDate<= '" + balayer.indiandateToMysqlDate(frmdt) + "' group by v1.Head_Id order by cast(digits(mg1.GrpMemberID) as unsigned),v1.Voucher_Type DESC ;");
                            if (string.IsNullOrEmpty(sss))
                            {
                                drBind["ChitNo1"] = balayer.GetSingleValue("select cast(digits(GrpMemberID) as unsigned) as ChitNo1 from membertogroupmaster where Head_Id=" + dtHds1.Rows[j]["NodeID"]);
                            }
                            else
                            {
                                drBind["ChitNo1"] = sss;
                            }
                            strName = balayer.GetSingleValue(@"select concat(mg1.MemberName) as `MemberName` from membertogroupmaster as mg1 join voucher as v1 on v1.Head_Id=mg1.Head_Id join view_groupwisedue as vgwd1 on vgwd1.`GroupId`=" + gpid + " left join trans_payment as tp1 on v1.Head_Id =tp1.TokenNumber join membermaster as mm on (mg1.MemberID=mm.MemberIDNew) where mg1.BranchID=" + balayer.ToobjectstrEvenNull(HttpContext.Current.Session["Branchid"]) + " and mg1.GroupID=" + gpid + " and v1.Head_Id=" + dtHds1.Rows[j]["NodeID"] + " and v1.ChoosenDate<= '" + balayer.indiandateToMysqlDate(frmdt) + "' group by v1.Head_Id order by v1.ChoosenDate DESC ;");
                            if (string.IsNullOrEmpty(strName))
                            {
                                strMemID = balayer.GetSingleValue("SELECT MemberID FROM svcf.membertogroupmaster where Head_id=" + dtHds1.Rows[j]["NodeID"]);
                                drBind["MemberName"] = balayer.GetSingleValue("SELECT CustomerName FROM svcf.membermaster where MemberIDNew=" + strMemID);
                            }
                            else
                            {
                                drBind["MemberName"] = strName;
                            }
                            credit = balayer.GetSingleValue("SELECT sum(case when(Voucher_Type='C') then Amount else 0.00 end) as Credit FROM svcf.voucher where ChoosenDate<='" + balayer.indiandateToMysqlDate(frmdt) + "' and Head_id=" + dtHeads.Rows[j]["NodeID"]);
                            debit = balayer.GetSingleValue("SELECT sum(case when(Voucher_Type='D') then Amount else 0.00 end) as Debit FROM svcf.voucher where ChoosenDate<='" + balayer.indiandateToMysqlDate(frmdt) + "' and Head_id=" + dtHeads.Rows[j]["NodeID"]);
                            if (string.IsNullOrEmpty(credit))
                                credit = "0.00";
                            if (string.IsNullOrEmpty(debit))
                                debit = "0.00";
                            decCredit = Convert.ToDecimal(credit);
                            decCredit = Convert.ToDecimal(debit);
                            if (decCredit > decDebit)
                            {
                                drBind["Credit"] = decCredit - decDebit;
                                drBind["Debit"] = "0.00";
                            }
                            else
                            {
                                drBind["Credit"] = "0.00";
                                drBind["Debit"] = decDebit - decCredit;
                            }
                            excess = null;
                            if (string.IsNullOrEmpty(excess))
                            {
                                drBind["ExcessRemittance"] = "0.00";
                            }
                            else
                            {
                                drBind["ExcessRemittance"] = excess;
                            }
                            nparr = null;
                            if (string.IsNullOrEmpty(nparr))
                            {
                                drBind["NPArrier"] = "0.00";
                            }
                            else
                            {
                                drBind["NPArrier"] = nparr;
                            }
                            parr = null;
                            if (string.IsNullOrEmpty(excess))
                            {
                                drBind["PArrier"] = "0.00";
                            }
                            else
                            {
                                drBind["PArrier"] = parr;
                            }
                            npkas = null;
                            if (string.IsNullOrEmpty(excess))
                            {
                                drBind["NPKasar"] = "0.00";
                            }
                            else
                            {
                                drBind["NPKasar"] = npkas;
                            }
                            pkas = null;
                            if (string.IsNullOrEmpty(excess))
                            {
                                drBind["PKasar"] = "0.00";
                            }
                            else
                            {
                                drBind["PKasar"] = pkas;
                            }
                            strMember = balayer.GetSingleValue("SELECT Head_id FROM svcf.voucher where Head_id=" + dtHds1.Rows[j]["NodeID"] + " and ChoosenDate<='" + balayer.indiandateToMysqlDate(frmdt) + "' order by ChoosenDate desc");
                            if (string.IsNullOrEmpty(strMember))
                            {
                                strMember = "0";
                            }
                            strBranches = balayer.GetSingleValue("SELECT b1.Place FROM svcf.membermaster as m1 join svcf.branchdetails as b1 on (m1.BranchID=b1.Head_Id) join svcf.membertogroupmaster as mg1 on mg1.MemberID=m1.MemberIDNew where mg1.Head_Id=" + strMember);

                            if (string.IsNullOrEmpty(strBranches))
                            {
                                drBind["Branches"] = balayer.GetSingleValue("SELECT b1.Place FROM svcf.membertogroupmaster as m1 join branchdetails as b1 on (m1.B_Id=b1.Head_Id) where m1.Head_id=" + dtHds1.Rows[j]["NodeID"]);
                            }
                            else
                            {
                                drBind["Branches"] = strBranches;
                            }
                            dtBind.Rows.Add(drBind.ItemArray);
                        }
                        //ViewState["Credit"] = dtBind.Compute("Sum(Credit)", "");
                        //ViewState["Debit"] = dtBind.Compute("Sum(Debit)", "");
                        //ViewState["NPKasar"] = dtBind.Compute("Sum(NPKasar)", "");
                        //ViewState["PKasar"] = dtBind.Compute("Sum(PKasar)", "");
                    }
                    string str = "";
                    gts = balayer.GetDataTable("SELECT `groupmaster`.`NoofMembers`,`groupmaster`.`GROUPNO`,`branchdetails`.`B_Name`,max(`auctiondetails`.`DrawNO`) as `RunningCall`,`groupmaster`.`ChitValue` FROM svcf.groupmaster join auctiondetails on (`groupmaster`.`Head_Id`=`auctiondetails`.`GroupID`) join `branchdetails` on (`groupmaster`.`BranchID`=`branchdetails`.`Head_Id`) where `groupmaster`.`Head_Id`=" + gpid + " and `auctiondetails`.`AuctionDate`<='" + balayer.indiandateToMysqlDate(frmdt) + "'  and `auctiondetails`.`BranchID`=" + balayer.ToobjectstrEvenNull(HttpContext.Current.Session["Branchid"]) + ";");
                    str = Convert.ToString(gts.Rows[0]["RunningCall"]) == "" ? "0" : Convert.ToString(gts.Rows[0]["RunningCall"]);
                    
                    // grid.SettingsText.Title = "Sree Visalam Chit Fund Limited.,<br/>Trial And Arrear <br/> Branch Name : " + sssssssss.Rows[0]["B_Name"].ToString() + "; \t Running Call : " + str + " <br/> Group No : " + sssssssss.Rows[0]["GROUPNO"].ToString() + "; \t Chit Amount : " + sssssssss.Rows[0]["ChitValue"].ToString() + "; \t for the month of " + Convert.ToDateTime(frmdt).ToString("MMMM yyyy");
                    //grid.Settings.ShowTitlePanel = true;
                    // dv = dtBind.DefaultView;
                    // dv.Sort = "ChitNo1 asc";
                    // sortedDT = dv.ToTable();
                    //grid.DataSource = sortedDT;
                }
                ds.Tables.Add(dtBind);
                //grid.DataBind();
            }
            catch (Exception) { }
            return ds.GetXml();
        }

    }
}