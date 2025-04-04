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
using SVCF_TransactionLayer;
using SVCF_DataAccessLayer;

namespace SreeVisalamChitFundLtd_phase1
{
    public partial class ChitAbstract : System.Web.UI.Page
    {

        #region VarDeclaration
        int maxDrawNo = 0;
        int ChitmaxNos = 0;
        string gpid_completed = "";
        int gpcompleted = 0;
       // CommonClassFile objcls = new CommonClassFile();
        #endregion

        #region VarDeclaration
        private System.Drawing.Image headerImage;
        BusinessLayer balayer = new BusinessLayer();
        TransactionLayer tranlayer = new TransactionLayer();

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtFromDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
               // select();
            }
           
        }
        protected void select()
        {
            try
            {
                grid.SettingsText.Title = "GROUPWAR CHIT TRIAL STATEMENT AS ON " + txtFromDate.Text + "";
                grid.Visible = true;
                balayer.GetInsertItem("CREATE OR REPLACE VIEW `unpaidprizedmoney` AS select `voucher`.`Head_Id` AS `Head_Id`, `membertogroupmaster`.`GrpMemberID` AS `GrpMemberID`, (case when (`voucher`.`Voucher_Type` = 'C') then sum(`voucher`.`Amount`) else 0.00 end) AS `Credit`, (case when (`voucher`.`Voucher_Type` = 'D') then sum(`voucher`.`Amount`) else 0.00 end) AS `Debit`, ((case when (`voucher`.`Voucher_Type` = 'C') then sum(`voucher`.`Amount`) else 0.00 end) - (case when (`voucher`.`Voucher_Type` = 'D') then sum(`voucher`.`Amount`) else 0.00 end)) AS `AmountActuallyremittedbytheParty` from (`voucher` join `membertogroupmaster` ON ((`voucher`.`Head_Id` = `membertogroupmaster`.`Head_Id`))) group by `voucher`.`Head_Id`");
                balayer.GetInsertItem("create or replace view `view_groupwisedue` as select `groupmaster`.`GROUPNO` AS `GroupIDOriginal`,`groupmaster`.`IsFinished`, `auctiondetails`.`GroupID` AS `GroupId`, sum(`auctiondetails`.`CurrentDueAmount`) AS `TotaldueAmount` from (`auctiondetails` join `groupmaster` ON ((`auctiondetails`.`GroupID` = `groupmaster`.`Head_Id`))) where (`auctiondetails`.`AuctionDate` <= '" + balayer.indiandateToMysqlDate(txtFromDate.Text) + "') group by `auctiondetails`.`GroupID`");
                ///Display new group
                ///change on 10/08/2018
                DataTable dt = balayer.GetDataTable("select * from groupmaster where BranchID=" + Session["Branchid"] + " and ChitStartDate<='" + balayer.indiandateToMysqlDate(txtFromDate.Text) + "' and ChitStartDate<>'0000-00-00';");
                DataTable dtB = new DataTable();
                dtB.Columns.Add("Head_Id");
                dtB.Columns.Add("GROUPNO");
                DataRow drB = dtB.NewRow();
                for (int i = 0; i < dt.Rows.Count; i++) 
                {
                    if (dt.Rows[i].ItemArray[13].ToString() != "00-00-0000")
                    {
                        DataTable dtTerminated = balayer.GetDataTable("SELECT * FROM svcf.auctiondetails where AuctionDate <='" + balayer.indiandateToMysqlDate(txtFromDate.Text) + "' and  GroupID=" + dt.Rows[i]["Head_Id"]);

                        string drawno = balayer.GetSingleValue("SELECT max(DrawNO) FROM svcf.auctiondetails where AuctionDate <='" + balayer.indiandateToMysqlDate(txtFromDate.Text) + "' and  GroupID=" + dt.Rows[i]["Head_Id"]);
                        string Noofmem = balayer.GetSingleValue("select NoofMembers from groupmaster where Head_Id=" + dt.Rows[i]["Head_Id"]);
                        if (drawno == Noofmem)
                        {
                            drB["Head_Id"] = dt.Rows[i]["Head_Id"];
                            drB["GROUPNO"] = dt.Rows[i]["GROUPNO"];
                            dtB.Rows.Add(drB.ItemArray);
                        }
                    }

                }

                DataTable dtBind = new DataTable();
                dtBind.Columns.Add("SNo");
                dtBind.Columns.Add("GroupNo");
                dtBind.Columns.Add("Head_Id");
                dtBind.Columns.Add("I_Credit", typeof(decimal));
                dtBind.Columns.Add("I_Debit", typeof(decimal));
                dtBind.Columns.Add("E_Credit", typeof(decimal));
                dtBind.Columns.Add("E_Debit", typeof(decimal));
                dtBind.Columns.Add("N_Credit", typeof(decimal));
                dtBind.Columns.Add("N_Debit", typeof(decimal));
                dtBind.Columns.Add("NonPrized", typeof(decimal));
                dtBind.Columns.Add("Prized", typeof(decimal));
                dtBind.Columns.Add("TotalAmountofKasar", typeof(decimal));
                dtBind.Columns.Add("NP", typeof(int));
                dtBind.Columns.Add("P", typeof(int));
                dtBind.Columns.Add("Remarks");
                DataRow drBind = dtBind.NewRow();

                int iCount = 0;
                for (int i = 0; i < dtB.Rows.Count; i++)
                {

                    //DataTable dt1 = balayer.GetDataTable(@"select mg1.Head_Id,cast(digits(mg1.GrpMemberID) as unsigned) as ChitNo1, concat(mm.MemberID, ' | ', mg1.MemberName) as `MemberName`,sum(v1.Amount) , sum(case when (v1.Voucher_Type='C' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) -(case when (v1.Voucher_Type='D' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) as paidAmount,  (case when( tp1.PaymentDate is null or tp1.PaymentDate >'" + balayer.indiandateToMysqlDate(txtFromDate.Text) + "') then sum(case when (v1.Voucher_Type='C' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) -sum(case when (v1.Voucher_Type='D' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) else 0.00 end ) as Credit, (case when( tp1.PaymentDate<='" + balayer.indiandateToMysqlDate(txtFromDate.Text) + "' ) then sum((case when (v1.Voucher_Type='D' and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) -case when (v1.Voucher_Type='C' and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) else 0.00 end ) as Debit, (case when( tp1.PaymentDate<='" + balayer.indiandateToMysqlDate(txtFromDate.Text) + "' ) then sum((case when (v1.Voucher_Type='C' and v1.Other_Trans_Type=5 ) then v1.Amount else 0.00 end)) else 0.00 end ) as PKasar, (case when( tp1.PaymentDate is null  or tp1.PaymentDate >'" + balayer.indiandateToMysqlDate(txtFromDate.Text) + "') then sum((case when (v1.Voucher_Type='C' and v1.Other_Trans_Type=5 ) then v1.Amount else 0.00 end)) else 0.00 end ) as NPKasar, (case when( (sum(case when (v1.Voucher_Type='C' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) -sum(case when (v1.Voucher_Type='D' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) -vgwd1.TotaldueAmount)>0.00) then (sum(case when (v1.Voucher_Type='C' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) -sum(case when (v1.Voucher_Type='D' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) -vgwd1.TotaldueAmount) else 0.00 end) as ExcessRemittance ,vgwd1.TotaldueAmount, (case when( tp1.PaymentDate<='" + balayer.indiandateToMysqlDate(txtFromDate.Text) + "' ) then (case when( (vgwd1.TotaldueAmount-sum(case when (v1.Voucher_Type='C' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) +sum(case when (v1.Voucher_Type='D' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) )>0.00) then (vgwd1.TotaldueAmount-sum(case when (v1.Voucher_Type='C' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) +sum(case when (v1.Voucher_Type='D' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) ) else 0.00 end) else 0.00 end ) as PArrier, (case when( tp1.PaymentDate is null  or tp1.PaymentDate >'" + balayer.indiandateToMysqlDate(txtFromDate.Text) + "') then (case when( (vgwd1.TotaldueAmount-sum(case when (v1.Voucher_Type='C' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) +sum(case when (v1.Voucher_Type='D' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) )>0.00) then (vgwd1.TotaldueAmount-sum(case when (v1.Voucher_Type='C' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) +sum(case when (v1.Voucher_Type='D' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) ) else 0.00 end) else 0.00 end) as NPArrier from membertogroupmaster as mg1 join voucher as v1 on v1.Head_Id=mg1.Head_Id join view_groupwisedue as vgwd1 on vgwd1.`GroupId`=" + dtB.Rows[i]["Head_Id"] + " left join trans_payment as tp1 on v1.Head_Id =tp1.TokenNumber join membermaster as mm on (mg1.MemberID=mm.MemberIDNew) where mg1.BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and mg1.GroupID=" + dtB.Rows[i]["Head_Id"] + " and v1.Head_Id in (select NodeID from headstree where ParentID=" + dtB.Rows[i]["Head_Id"] + " ) and v1.ChoosenDate<= '" + balayer.indiandateToMysqlDate(txtFromDate.Text) + "' group by v1.Head_Id order by cast(digits(mg1.GrpMemberID) as unsigned) ;");
                    DataTable dt1 = balayer.GetDataTable(@"select mg1.Head_Id,cast(digits(mg1.GrpMemberID) as unsigned) as ChitNo1, concat(mm.MemberID, ' | ', mg1.MemberName) as `MemberName`,sum(v1.Amount) , sum(case when (v1.Voucher_Type='C' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) -(case when (v1.Voucher_Type='D' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) as paidAmount,  (case when( tp1.PaymentDate is null or tp1.PaymentDate >'" + balayer.indiandateToMysqlDate(txtFromDate.Text) + "') then sum(case when (v1.Voucher_Type='C' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) -sum(case when (v1.Voucher_Type='D' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) else 0.00 end ) as Credit, (case when( tp1.PaymentDate<='" + balayer.indiandateToMysqlDate(txtFromDate.Text) + "' ) then sum((case when (v1.Voucher_Type='D' and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) -case when (v1.Voucher_Type='C' and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) else 0.00 end ) as Debit, (case when( tp1.PaymentDate<='" + balayer.indiandateToMysqlDate(txtFromDate.Text) + "' ) then sum((case when (v1.Voucher_Type='C' and v1.Other_Trans_Type=5 ) then v1.Amount else 0.00 end)) else 0.00 end ) as PKasar, (case when( tp1.PaymentDate is null  or tp1.PaymentDate >'" + balayer.indiandateToMysqlDate(txtFromDate.Text) + "') then sum((case when (v1.Voucher_Type='C' and v1.Other_Trans_Type=5 ) then v1.Amount else 0.00 end)) else 0.00 end ) as NPKasar, (case when( (sum(case when (v1.Voucher_Type='C' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) -sum(case when (v1.Voucher_Type='D' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) -vgwd1.TotaldueAmount)>0.00) then (sum(case when (v1.Voucher_Type='C' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) -sum(case when (v1.Voucher_Type='D' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) -vgwd1.TotaldueAmount) else 0.00 end) as ExcessRemittance ,vgwd1.TotaldueAmount, (case when( tp1.PaymentDate<='" + balayer.indiandateToMysqlDate(txtFromDate.Text) + "' ) then (case when( (vgwd1.TotaldueAmount-sum(case when (v1.Voucher_Type='C' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) +sum(case when (v1.Voucher_Type='D' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) )>0.00) then (vgwd1.TotaldueAmount-sum(case when (v1.Voucher_Type='C' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) +sum(case when (v1.Voucher_Type='D' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) ) else 0.00 end) else 0.00 end ) as PArrier, (case when( tp1.PaymentDate is null  or tp1.PaymentDate >'" + balayer.indiandateToMysqlDate(txtFromDate.Text) + "') then (case when( (vgwd1.TotaldueAmount-sum(case when (v1.Voucher_Type='C' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) +sum(case when (v1.Voucher_Type='D' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) )>0.00) then (vgwd1.TotaldueAmount-sum(case when (v1.Voucher_Type='C' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) +sum(case when (v1.Voucher_Type='D' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) ) else 0.00 end) else 0.00 end) as NPArrier from membertogroupmaster as mg1 join voucher as v1 on v1.Head_Id=mg1.Head_Id join view_groupwisedue as vgwd1 on vgwd1.`GroupId`=" + dtB.Rows[i]["Head_Id"] + " left join trans_payment as tp1 on v1.Head_Id =tp1.TokenNumber join membermaster as mm on (mg1.MemberID=mm.MemberIDNew) where mg1.BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and mg1.GroupID=" + dtB.Rows[i]["Head_Id"] + " and v1.Head_Id in (select NodeID from headstree where ParentID=" + dtB.Rows[i]["Head_Id"] + " ) and v1.ChoosenDate<= '" + balayer.indiandateToMysqlDate(txtFromDate.Text) + "' group by v1.Head_Id order by cast(digits(mg1.GrpMemberID) as unsigned) ;");
                    if (dt1.Rows.Count > 0)
                    {
                        #region start
                        if (((Convert.ToDecimal(dt1.Compute("Sum(Credit)", "")) + Convert.ToDecimal(dt1.Compute("Sum(PKasar)", "")) + Convert.ToDecimal(dt1.Compute("Sum(NPKasar)", ""))) - Convert.ToDecimal(dt1.Compute("Sum(Debit)", "")) == 0.00M) && Convert.ToDecimal(dt1.Compute("Sum(ExcessRemittance)", "")) == 0.00M && Convert.ToDecimal(dt1.Compute("Sum(PArrier)", "")) == 0.00M && Convert.ToDecimal(dt1.Compute("Sum(NPArrier)", "")) == 0.00M)
                        {

                        }
                        else
                        {
                            if (Convert.ToInt32(dtB.Rows[i]["Head_Id"]) == 01193)
                            {
                                DataTable dtS = balayer.GetDataTable("SELECT sum((case when (Voucher_Type='C') then Amount else 0.00 end)) as Credit,sum((case when (Voucher_Type='D') then Amount else 0.00 end)) as Debit FROM svcf.voucher where ChoosenDate<='" + balayer.indiandateToMysqlDate(txtFromDate.Text) + "' and Head_id=1194");
                                if (dtS.Rows.Count > 0)
                                {
                                    if (Convert.ToDecimal(dtS.Rows[0]["Credit"]) < Convert.ToDecimal(dtS.Rows[0]["Debit"]))
                                    {
                                        drBind["SNo"] = iCount + 1;
                                        drBind["GroupNo"] = dtB.Rows[i]["GROUPNO"];
                                        drBind["Head_Id"] = dtB.Rows[i]["Head_Id"];
                                        drBind["I_Credit"] = 0.00;
                                        drBind["I_Debit"] = Convert.ToDecimal(dtS.Rows[0]["Debit"]) - Convert.ToDecimal(dtS.Rows[0]["Credit"]);
                                        drBind["E_Credit"] = 0.00;
                                        drBind["E_Debit"] = Convert.ToDecimal(dtS.Rows[0]["Debit"]) - Convert.ToDecimal(dtS.Rows[0]["Credit"]);
                                        drBind["N_Credit"] = 0.00;
                                        drBind["N_Debit"] = Convert.ToDecimal(dtS.Rows[0]["Debit"]) - Convert.ToDecimal(dtS.Rows[0]["Credit"]);
                                        drBind["NonPrized"] = "0.00";
                                        drBind["Prized"] = "0.00";
                                        drBind["TotalAmountofKasar"] = "0.00";
                                        drBind["NP"] = "0";
                                        drBind["P"] = "0";
                                        drBind["Remarks"] = "T";
                                        iCount++;
                                        dtBind.Rows.Add(drBind.ItemArray);
                                    }
                                    else if (Convert.ToDecimal(dtS.Rows[0]["Credit"]) > Convert.ToDecimal(dtS.Rows[0]["Debit"]))
                                    {
                                        drBind["SNo"] = iCount + 1;
                                        drBind["GroupNo"] = dtB.Rows[i]["GROUPNO"];
                                        drBind["Head_Id"] = dtB.Rows[i]["Head_Id"];
                                        drBind["I_Credit"] = Convert.ToDecimal(dtS.Rows[0]["Credit"]) - Convert.ToDecimal(dtS.Rows[0]["Debit"]);
                                        drBind["I_Debit"] = 0.00;
                                        drBind["E_Credit"] = Convert.ToDecimal(dtS.Rows[0]["Credit"]) - Convert.ToDecimal(dtS.Rows[0]["Debit"]);
                                        drBind["E_Debit"] = 0.00;
                                        drBind["N_Credit"] = Convert.ToDecimal(dtS.Rows[0]["Credit"]) - Convert.ToDecimal(dtS.Rows[0]["Debit"]);
                                        drBind["N_Debit"] = 0.00;
                                        drBind["NonPrized"] = "0.00";
                                        drBind["Prized"] = "0.00";
                                        drBind["TotalAmountofKasar"] = "0.00";
                                        drBind["NP"] = "0";
                                        drBind["P"] = "0";
                                        drBind["Remarks"] = "T";
                                        iCount++;
                                        dtBind.Rows.Add(drBind.ItemArray);
                                    }
                                }
                            }
                            else
                            {
                                drBind["SNo"] = iCount + 1;
                                drBind["GroupNo"] = dtB.Rows[i]["GROUPNO"];
                                drBind["Head_Id"] = dtB.Rows[i]["Head_Id"];
                                drBind["I_Credit"] = Convert.ToDecimal(dt1.Compute("Sum(ExcessRemittance)", ""));
                                drBind["I_Debit"] = Convert.ToDecimal(dt1.Compute("Sum(PArrier)", ""));
                                drBind["E_Credit"] = Convert.ToDecimal(dt1.Compute("Sum(ExcessRemittance)", ""));
                                drBind["E_Debit"] = Convert.ToDecimal(dt1.Compute("Sum(PArrier)", "")) - Convert.ToDecimal(dt1.Compute("Sum(NPArrier)", ""));

                                if ((Convert.ToDecimal(dt1.Compute("Sum(Credit)", "")) + Convert.ToDecimal(dt1.Compute("Sum(PKasar)", "")) + Convert.ToDecimal(dt1.Compute("Sum(NPKasar)", ""))) > Convert.ToDecimal(dt1.Compute("Sum(Debit)", "")))
                                {
                                    drBind["N_Credit"] = (Convert.ToDecimal(dt1.Compute("Sum(Credit)", "")) + Convert.ToDecimal(dt1.Compute("Sum(PKasar)", "")) + Convert.ToDecimal(dt1.Compute("Sum(NPKasar)", ""))) - Convert.ToDecimal(dt1.Compute("Sum(Debit)", ""));
                                    drBind["N_Debit"] = "0.00";
                                }
                                else if ((Convert.ToDecimal(dt1.Compute("Sum(Credit)", "")) + Convert.ToDecimal(dt1.Compute("Sum(PKasar)", "")) + Convert.ToDecimal(dt1.Compute("Sum(NPKasar)", ""))) < Convert.ToDecimal(dt1.Compute("Sum(Debit)", "")))
                                {
                                    drBind["N_Credit"] = "0.00";
                                    drBind["N_Debit"] = Convert.ToDecimal(dt1.Compute("Sum(Debit)", "")) - (Convert.ToDecimal(dt1.Compute("Sum(Credit)", "")) + Convert.ToDecimal(dt1.Compute("Sum(PKasar)", "")) + Convert.ToDecimal(dt1.Compute("Sum(NPKasar)", "")));
                                }
                                drBind["NonPrized"] = "0.00";
                                drBind["Prized"] = "0.00";
                                drBind["TotalAmountofKasar"] = "0.00";
                                drBind["NP"] = "0";
                                drBind["P"] = "0";
                                drBind["Remarks"] = "T";
                                iCount++;
                                dtBind.Rows.Add(drBind.ItemArray);
                            }
                        }
                        #endregion
                    }
                }
                dt = balayer.GetDataTable("select * from groupmaster where `PSOOrderDate` <= '" + balayer.indiandateToMysqlDate(txtFromDate.Text) + "' and PSOOrderDate<>'0000-00-00' and BranchID=" + Session["Branchid"]);
                if (dt.Rows.Count != 0)
                {

                    dtB = new DataTable();
                    dtB.Columns.Add("Head_Id");
                    dtB.Columns.Add("GROUPNO");
                    drB = dtB.NewRow();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        //DataTable dtTerminated = balayer.GetDataTable("SELECT * FROM svcf.auctiondetails where IsPrized='I' and GroupID=" + dt.Rows[i]["Head_Id"]);
                        //if (dtTerminated.Rows.Count > 0)
                        DataTable dtTerminated = balayer.GetDataTable("SELECT * FROM svcf.auctiondetails where AuctionDate <='" + balayer.indiandateToMysqlDate(txtFromDate.Text) + "' and  GroupID=" + dt.Rows[i]["Head_Id"]);

                        string drawno = balayer.GetSingleValue("SELECT max(DrawNO) FROM svcf.auctiondetails where AuctionDate <='" + balayer.indiandateToMysqlDate(txtFromDate.Text) + "' and  GroupID=" + dt.Rows[i]["Head_Id"]);
                        string Noofmem = balayer.GetSingleValue("select NoofMembers from groupmaster where Head_Id=" + dt.Rows[i]["Head_Id"]);

                        if (drawno != Noofmem)
                        {
                            drB["Head_Id"] = dt.Rows[i]["Head_Id"];
                            drB["GROUPNO"] = dt.Rows[i]["GROUPNO"];
                            dtB.Rows.Add(drB.ItemArray);
                        }
                    }
                    //     DataRow drBind1 = dtBind.NewRow();
                    //drBind1["SNo"] = "";
                    //drBind1["GroupNo"] = "Running";
                    // drBind1["Remarks"] = "";
                    // dtBind.Rows.Add(drBind1.ItemArray);

                    iCount = 0;
                    for (int i = 0; i < dtB.Rows.Count; i++)
                    {
                        //DataTable dtInit = balayer.GetDataTable("select * from auctiondetails where IsPrized='Y' and AuctionDate<='" + balayer.indiandateToMysqlDate(txtToDate.Text) + "' and GroupID=" + dtB.Rows[i]["Head_Id"]);
                        //if (dtInit.Rows.Count > 0)

                        DataTable dtInit = balayer.GetDataTable("select * from auctiondetails where AuctionDate<='" + balayer.indiandateToMysqlDate(txtFromDate.Text) + "' and GroupID=" + dtB.Rows[i]["Head_Id"]);
                        //jeya --------------
                        string prizecount = balayer.GetSingleValue("select count(*) from trans_payment where ChitGroupID = " + dtB.Rows[i]["Head_Id"] + " and PaymentDate <='" + balayer.indiandateToMysqlDate(txtFromDate.Text) + "';");
                        string Totalcount = balayer.GetSingleValue("select count(*) from auctiondetails where GroupID=" + dtB.Rows[i]["Head_Id"] + ";");
                        //jeya -----------------
                        string drawno = balayer.GetSingleValue("SELECT max(DrawNO) FROM svcf.auctiondetails where AuctionDate <='" + balayer.indiandateToMysqlDate(txtFromDate.Text) + "' and  GroupID=" + dtB.Rows[i]["Head_Id"]);
                        string Noofmem = balayer.GetSingleValue("select NoofMembers from groupmaster where Head_Id=" + dtB.Rows[i]["Head_Id"]);
                        DataTable dt1 = balayer.GetDataTable(@"select cast(digits(mg1.GrpMemberID) as unsigned) as ChitNo1, concat(mm.MemberID, ' | ', mg1.MemberName) as `MemberName`,sum(v1.Amount) , sum(case when (v1.Voucher_Type='C' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) -(case when (v1.Voucher_Type='D' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) as paidAmount,  (case when( tp1.PaymentDate is null or tp1.PaymentDate >'" + balayer.indiandateToMysqlDate(txtFromDate.Text) + "') then sum(case when (v1.Voucher_Type='C' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) -sum(case when (v1.Voucher_Type='D' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) else 0.00 end ) as Credit, (case when( tp1.PaymentDate<='" + balayer.indiandateToMysqlDate(txtFromDate.Text) + "' ) then sum((case when (v1.Voucher_Type='D' and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) -case when (v1.Voucher_Type='C' and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) else 0.00 end ) as Debit, (case when( tp1.PaymentDate<='" + balayer.indiandateToMysqlDate(txtFromDate.Text) + "' ) then sum((case when (v1.Voucher_Type='C' and v1.Other_Trans_Type=5 ) then v1.Amount else 0.00 end)) else 0.00 end ) as PKasar, (case when( tp1.PaymentDate is null  or tp1.PaymentDate >'" + balayer.indiandateToMysqlDate(txtFromDate.Text) + "') then sum((case when (v1.Voucher_Type='C' and v1.Other_Trans_Type=5 ) then v1.Amount else 0.00 end)) else 0.00 end ) as NPKasar, (case when( (sum(case when (v1.Voucher_Type='C' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) -sum(case when (v1.Voucher_Type='D' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) -vgwd1.TotaldueAmount)>0.00) then (sum(case when (v1.Voucher_Type='C' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) -sum(case when (v1.Voucher_Type='D' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) -vgwd1.TotaldueAmount) else 0.00 end) as ExcessRemittance ,vgwd1.TotaldueAmount, (case when( tp1.PaymentDate<='" + balayer.indiandateToMysqlDate(txtFromDate.Text) + "' ) then (case when( (vgwd1.TotaldueAmount-sum(case when (v1.Voucher_Type='C' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) +sum(case when (v1.Voucher_Type='D' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) )>0.00) then (vgwd1.TotaldueAmount-sum(case when (v1.Voucher_Type='C' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) +sum(case when (v1.Voucher_Type='D' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) ) else 0.00 end) else 0.00 end ) as PArrier, (case when( tp1.PaymentDate is null  or tp1.PaymentDate >'" + balayer.indiandateToMysqlDate(txtFromDate.Text) + "') then (case when( (vgwd1.TotaldueAmount-sum(case when (v1.Voucher_Type='C' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) +sum(case when (v1.Voucher_Type='D' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) )>0.00) then (vgwd1.TotaldueAmount-sum(case when (v1.Voucher_Type='C' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) +sum(case when (v1.Voucher_Type='D' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) ) else 0.00 end) else 0.00 end) as NPArrier from membertogroupmaster as mg1 join voucher as v1 on v1.Head_Id=mg1.Head_Id join view_groupwisedue as vgwd1 on vgwd1.`GroupId`=" + dtB.Rows[i]["Head_Id"] + " left join trans_payment as tp1 on v1.Head_Id =tp1.TokenNumber join membermaster as mm on (mg1.MemberID=mm.MemberIDNew) where mg1.BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and mg1.GroupID=" + dtB.Rows[i]["Head_Id"] + " and v1.Head_Id in (select NodeID from headstree where ParentID=" + dtB.Rows[i]["Head_Id"] + " ) and v1.ChoosenDate<= '" + balayer.indiandateToMysqlDate(txtFromDate.Text) + "' group by v1.Head_Id order by cast(digits(mg1.GrpMemberID) as unsigned) ;");

                        if (drawno != Noofmem && dt1.Rows.Count > 0)
                        {
                            //if (dt1.Rows.Count > 0)
                            //{
                            //if ((Convert.ToDecimal(dt1.Compute("Sum(Credit)", "")) + Convert.ToDecimal(dt1.Compute("Sum(PKasar)", "")) + Convert.ToDecimal(dt1.Compute("Sum(NPKasar)", ""))) - Convert.ToDecimal(dt1.Compute("Sum(Debit)", "")) == 0.00M)
                            //{

                            //}
                            //else
                            //{
                            drBind["SNo"] = iCount + 1;
                            drBind["GroupNo"] = dtB.Rows[i]["GROUPNO"];
                            drBind["Head_Id"] = dtB.Rows[i]["Head_Id"];
                            drBind["I_Credit"] = Convert.ToDecimal(dt1.Compute("Sum(Credit)", "")) + Convert.ToDecimal(dt1.Compute("Sum(PKasar)", "")) + Convert.ToDecimal(dt1.Compute("Sum(NPKasar)", ""));
                            drBind["I_Debit"] = Convert.ToDecimal(dt1.Compute("Sum(Debit)", ""));
                            drBind["E_Credit"] = Convert.ToDecimal(dt1.Compute("Sum(Credit)", ""));
                            drBind["E_Debit"] = Convert.ToDecimal(dt1.Compute("Sum(Debit)", "")) - Convert.ToDecimal(dt1.Compute("Sum(PKasar)", ""));
                            if ((Convert.ToDecimal(dt1.Compute("Sum(Credit)", "")) + Convert.ToDecimal(dt1.Compute("Sum(PKasar)", "")) + Convert.ToDecimal(dt1.Compute("Sum(NPKasar)", ""))) > Convert.ToDecimal(dt1.Compute("Sum(Debit)", "")))
                            {
                                drBind["N_Credit"] = (Convert.ToDecimal(dt1.Compute("Sum(Credit)", "")) + Convert.ToDecimal(dt1.Compute("Sum(PKasar)", "")) + Convert.ToDecimal(dt1.Compute("Sum(NPKasar)", ""))) - (Convert.ToDecimal(dt1.Compute("Sum(Debit)", "")));
                                drBind["N_Debit"] = "0.00";
                            }
                            else if ((Convert.ToDecimal(dt1.Compute("Sum(Credit)", "")) + Convert.ToDecimal(dt1.Compute("Sum(PKasar)", "")) + Convert.ToDecimal(dt1.Compute("Sum(NPKasar)", ""))) < Convert.ToDecimal(dt1.Compute("Sum(Debit)", "")))
                            {
                                drBind["N_Credit"] = "0.00";
                                drBind["N_Debit"] = Convert.ToDecimal(dt1.Compute("Sum(Debit)", "")) - (Convert.ToDecimal(dt1.Compute("Sum(Credit)", "")) + Convert.ToDecimal(dt1.Compute("Sum(PKasar)", "")) + Convert.ToDecimal(dt1.Compute("Sum(NPKasar)", "")));
                            }
                            ///////////////////////////-------------------------////////////////
                            else
                            {
                                drBind["N_Credit"] = "0.00";
                                drBind["N_Debit"] = "0.00";
                            }
                            /////////////////////////--------==================//////////////////
                            drBind["NonPrized"] = Convert.ToDecimal(dt1.Compute("Sum(NPKasar)", ""));

                            //jeya
                            if (prizecount == "1")
                            {
                                drBind["Prized"] = Convert.ToDecimal(dt1.Compute("Sum(PKasar)", ""));
                                drBind["TotalAmountofKasar"] = Convert.ToDecimal(dt1.Compute("Sum(NPKasar)", "")) + Convert.ToDecimal(dt1.Compute("Sum(PKasar)", ""));
                                drBind["P"] = (prizecount);
                                drBind["NP"] = Convert.ToInt16(Totalcount) - Convert.ToInt16(prizecount);
                            }
                            else if (prizecount != "1")
                            {
                                drBind["Prized"] = Convert.ToDecimal(dt1.Compute("Sum(PKasar)", ""));
                                drBind["TotalAmountofKasar"] = Convert.ToDecimal(dt1.Compute("Sum(NPKasar)", "")) + Convert.ToDecimal(dt1.Compute("Sum(PKasar)", ""));
                                drBind["NP"] = dt1.AsEnumerable()
                                        .Where(r => (decimal)r["NPKasar"] != 0.00m)
                                        .Count();
                                drBind["P"] = dt1.AsEnumerable()
                                            .Where(r => (decimal)r["PKasar"] != 0.00m)
                                            .Count();
                            }
                            else if (prizecount == "0")
                            {
                                drBind["NP"] = Convert.ToInt16(Totalcount);
                            }
                            //jeya


                            drBind["Remarks"] = "";
                            iCount++;
                            dtBind.Rows.Add(drBind.ItemArray);
                        //}
                            //}
                        }
                        else
                        {
                            drBind["SNo"] = iCount + 1;
                            drBind["GroupNo"] = dtB.Rows[i]["GROUPNO"];
                            drBind["Head_Id"] = dtB.Rows[i]["Head_Id"];


                            string strChits = "";
                            DataTable dtC = new DataTable();
                            dtC = balayer.GetDataTable("SELECT concat(Cast(NodeID as char(10)),',') FROM svcf.headstree where ParentID=" + dtB.Rows[i]["Head_Id"]);
                            for (int k = 0; k < dtC.Rows.Count; k++)
                            {
                                strChits = strChits + dtC.Rows[k][0];
                            }
                            if (string.IsNullOrEmpty(strChits))
                            {
                                strChits = "0";
                            }
                            else
                            {
                                strChits = strChits.TrimEnd(',');
                            }
                            //  string credit = balayer.GetSingleValue("SELECT sum(case when(Voucher_Type='C') then Amount else 0.00 end) as Credit FROM svcf.voucher where ChoosenDate<='" + balayer.indiandateToMysqlDate(txtFromDate.Text) + "' and Head_id in (" + strChits + ")");
                            string credit = balayer.GetSingleValue("SELECT sum(case when(Voucher_Type='C') then Amount else 0.00 end) as Credit FROM svcf.voucher where ChoosenDate<='" + balayer.indiandateToMysqlDate(txtFromDate.Text) + "' and Head_id in (" + strChits + ")");
                            string debit = "";
                            //   string debit = balayer.GetSingleValue("SELECT sum(case when(Voucher_Type='D') then Amount else 0.00 end) as Debit FROM svcf.voucher where ChoosenDate<='" + balayer.indiandateToMysqlDate(txtFromDate.Text) + "' and Head_id in (" + strChits + ")");


                            if (string.IsNullOrEmpty(credit))
                                credit = "0.00";
                            if (string.IsNullOrEmpty(debit))
                                debit = "0.00";
                            decimal decCredit = Convert.ToDecimal(credit);
                            decimal decDebit = Convert.ToDecimal(debit);

                            drBind["I_Credit"] = decCredit;
                            drBind["I_Debit"] = 0.00;
                            drBind["E_Credit"] = decCredit;
                            drBind["E_Debit"] = 0.00;
                            drBind["N_Credit"] = decCredit;
                            drBind["N_Debit"] = 0.00;
                            drBind["NonPrized"] = "0.00";
                            drBind["Prized"] = "0.00";
                            drBind["TotalAmountofKasar"] = "0.00";
                            drBind["NP"] = "0";
                            drBind["P"] = "0";
                            drBind["Remarks"] = "T";
                            iCount++;
                            //   if (Convert.ToDecimal(decCredit) != 0.00M || Convert.ToDecimal(decDebit) != 0.00M)
                            //  {
                            dtBind.Rows.Add(drBind.ItemArray);
                            //  }

                        }
                    }
                }
                decimal I_Credit = Convert.ToDecimal(dtBind.Compute("sum(I_Credit)", ""));
                decimal I_Debit = Convert.ToDecimal(dtBind.Compute("sum(I_Debit)", ""));
                decimal E_Credit = Convert.ToDecimal(dtBind.Compute("sum(E_Credit)", ""));
                decimal E_Debit = Convert.ToDecimal(dtBind.Compute("sum(E_Debit)", ""));
                decimal N_Credit = Convert.ToDecimal(dtBind.Compute("sum(N_Credit)", ""));
                decimal N_Debit = Convert.ToDecimal(dtBind.Compute("sum(N_Debit)", ""));
                decimal NonPrized = Convert.ToDecimal(dtBind.Compute("sum(NonPrized)", ""));
                decimal Prized = Convert.ToDecimal(dtBind.Compute("sum(Prized)", ""));
                decimal TotalAmountofKasar = Convert.ToDecimal(dtBind.Compute("sum(TotalAmountofKasar)", ""));
                decimal NP = Convert.ToDecimal(dtBind.Compute("sum(NP)", ""));
                decimal P = Convert.ToDecimal(dtBind.Compute("sum(P)", ""));
                ///Display new group
                ///change on 10/08/2018
                DataTable dtGrid = new DataTable();
                dtGrid.Columns.Add("Slno");
                dtGrid.Columns.Add("Chit");
                dtGrid.Columns.Add("Credit", typeof(decimal));
                dtGrid.Columns.Add("Debit", typeof(decimal));
                dtGrid.Columns.Add("Running");
                dtGrid.Columns.Add("ChitValue", typeof(decimal));
                DataRow drGrid = dtGrid.NewRow();
                for (int i = 0; i < dtBind.Rows.Count; i++)
                {
                    string strValue = balayer.GetSingleValue("SELECT ChitValue FROM svcf.groupmaster where Head_id=" + dtBind.Rows[i]["Head_Id"]);
                    string strRunning = balayer.GetSingleValue("SELECT max(DrawNo) FROM svcf.auctiondetails where GroupID=" + dtBind.Rows[i]["Head_Id"] + " and AuctionDate<='" + balayer.indiandateToMysqlDate(txtFromDate.Text) + "'");
                    if (!string.IsNullOrEmpty(strRunning))
                    {
                        string strTotal = balayer.GetSingleValue("SELECT NoofMembers FROM svcf.groupmaster where Head_id=" + dtBind.Rows[i]["Head_Id"] + " and PSOOrderDate<='" + balayer.indiandateToMysqlDate(txtFromDate.Text) + "';");
                        if (Convert.ToInt32(strTotal) == Convert.ToInt32(strRunning))
                        {
                            strRunning = "T";
                        }
                    }
                    drGrid["Slno"] = i + 1;
                    if (Convert.ToDecimal(dtBind.Rows[i]["N_Credit"]) >= Convert.ToDecimal(dtBind.Rows[i]["N_Debit"]))
                    {
                        drGrid["Chit"] = dtBind.Rows[i]["GROUPNO"];
                        drGrid["Credit"] = Convert.ToDecimal(dtBind.Rows[i]["N_Credit"]) - Convert.ToDecimal(dtBind.Rows[i]["N_Debit"]);
                        drGrid["Debit"] = 0.00;
                    }
                    else
                    {
                        drGrid["Chit"] = dtBind.Rows[i]["GROUPNO"];
                        drGrid["Credit"] = 0.00;
                        drGrid["Debit"] = Convert.ToDecimal(dtBind.Rows[i]["N_Debit"]) - Convert.ToDecimal(dtBind.Rows[i]["N_Credit"]);
                    }
                    if (strRunning == "")
                    {
                        drGrid["Running"] = "0";
                    }
                    else
                    {
                        drGrid["Running"] = strRunning;
                    }


                    drGrid["ChitValue"] = strValue;
                    dtGrid.Rows.Add(drGrid.ItemArray);
                }
                if (dtGrid.Rows.Count > 0)
                {
                    var deccredit = Convert.ToDecimal(dtGrid.Compute("sum(Credit)", ""));
                    var decdebit = Convert.ToDecimal(dtGrid.Compute("sum(Debit)", ""));
                    var chitvalue = Convert.ToDecimal(dtGrid.Compute("sum(ChitValue)", ""));

                    drGrid["Slno"] = "";
                    drGrid["Chit"] = "TOTAL";
                    drGrid["Credit"] = deccredit;
                    drGrid["Debit"] = decdebit;
                    drGrid["ChitValue"] = chitvalue;
                    drGrid["Running"] = "";
                    // drGrid["Remarks"] = "";
                    dtGrid.Rows.Add(drGrid.ItemArray);

                    drGrid["Slno"] = "";
                    if (deccredit > decdebit)
                    {
                        drGrid["Chit"] = "Net Balance";
                        drGrid["Credit"] = deccredit - decdebit;
                        drGrid["Debit"] = "0.00";
                        drGrid["ChitValue"] = "0.00";
                    }
                    else
                    {
                        drGrid["Chit"] = "Net Loss";
                        drGrid["Credit"] = "0.00";
                        drGrid["Debit"] = deccredit - decdebit;
                        drGrid["ChitValue"] = "0.00";
                    }
                    dtGrid.Rows.Add(drGrid.ItemArray);
                }


                ViewState["Exportdt"] = dtGrid;
                grid.SettingsText.Title = "Sree Visalam Chit Fund Limited.,<br/>Branch Name : " + Session["BranchName"] + "; <br/> Trial Balance of Chit for the Month of " + Convert.ToDateTime(txtFromDate.Text).ToString("MMMM yyyy") + "<br/> Chit Abstract";
                grid.Settings.ShowTitlePanel = true;

                grid.DataSource = dtGrid;
                grid.DataBind();
            }
            catch (Exception err)
            {
                LogCls.LogError(err, "Chit abstract");
            }
        }


        protected void btnExportExcel_Click1(object sender, EventArgs e)
        {
            gridexcel.FileName = "Chitabstract" + DateTime.Now.Millisecond.ToString();
            // grid.DataSource = LoadGrid();
            grid.DataBind();
            gridexcel.WriteXlsToResponse();
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }
        void header_CreateDetailArea(object sender, CreateAreaEventArgs e)
        {
            //e.Graph.BorderWidth = 0;
            //Rectangle r = new Rectangle(0, 0, 40, 40);
            //e.Graph.DrawImage(headerImage, r);
            //TextBrick tb = new TextBrick();
            //tb.Text = "Sree Visalam Chit Fund Limited";
            //tb.Font = new Font("Arial", 10);
            //tb.Rect = new RectangleF(150, 8, (e.Graph.ClientPageSize.Width / 2), 20);
            //tb.BorderWidth = 0;
            //tb.BackColor = Color.Transparent;
            //tb.HorzAlignment = DevExpress.Utils.HorzAlignment.Center;
            //tb.VertAlignment = DevExpress.Utils.VertAlignment.Top;
            //e.Graph.DrawBrick(tb);

            ////TextBrick tb1 = new TextBrick();
            ////objCOM.RegOfcename = balayer.GetSingleValue("select PSODROffice from svcf.groupmaster where Head_Id=" + DD_GP.SelectedValue + "");
            ////tb1.Text = "Registered Office : " + objCOM.RegOfcename;
            ////tb1.Font = new Font("Arial", 8);
            ////tb1.Rect = new RectangleF(150, 28, (e.Graph.ClientPageSize.Width / 2), 20);
            ////tb1.BorderWidth = 0;
            ////tb1.BackColor = Color.Transparent;
            ////tb1.HorzAlignment = DevExpress.Utils.HorzAlignment.Center;
            ////tb1.VertAlignment = DevExpress.Utils.VertAlignment.Top;
            ////e.Graph.DrawBrick(tb1);

            //TextBrick tb2 = new TextBrick();

            //tb2.Text = "Branch : " + Session["BranchName"];
            //tb2.Font = new Font("Arial", 8);
            //tb2.Rect = new RectangleF(150, 48, (e.Graph.ClientPageSize.Width / 2), 20);
            //tb2.BorderWidth = 0;
            //tb2.BackColor = Color.Transparent;
            //tb2.HorzAlignment = DevExpress.Utils.HorzAlignment.Center;
            //tb2.VertAlignment = DevExpress.Utils.VertAlignment.Top;
            //e.Graph.DrawBrick(tb2);
        }

        protected void Export_click(object sender, MenuItemEventArgs e)
        {
            using (this.headerImage = System.Drawing.Image.FromFile(Server.MapPath("~\\Images\\visalam.png")))
            {
                if (e.Item.Text.ToString() == "PDF")
                {
                    grid.DataSource = (DataTable)ViewState["Exportdt"];
                    grid.SettingsText.Title = "Sree Visalam Chit Fund Limited..," + "Branch Name : " + Session["BranchName"] + "                                                      Trial Balance of Chit for the Month of " + Convert.ToDateTime(txtFromDate.Text).ToString("MMMM yyyy");

                    grid.DataBind();
                    gridExport.DataBind();
                    gridExport.Styles.Header.HorizontalAlign = HorizontalAlign.Center;
                    gridExport.Styles.Header.VerticalAlign = VerticalAlign.Middle;
                    gridExport.Styles.Header.Wrap = DevExpress.Utils.DefaultBoolean.True;
                    gridExport.Styles.Footer.HorizontalAlign = HorizontalAlign.Right;
                    PrintingSystem ps = new PrintingSystem();
                    PrintableComponentLink gridPayment = new PrintableComponentLink(ps);
                    gridPayment.Component = gridExport;
                    Link header = new Link();
                    CompositeLink compositeLink = new CompositeLink(ps);
                    header.CreateDetailArea += new CreateAreaEventHandler(header_CreateDetailArea);

                    compositeLink.Links.AddRange(new object[] { header, gridPayment });

                    string leftColumn = "Pages : [Page # of Pages #]";
                    // string rightColumn = "Date: [Date Printed]\r\nTime: " + DateTime.Now.ToString("hh:mm tt");
                    string rightColumn = "Date: [Date Printed]\r\nTime: " + DateTime.Now;
                    PageHeaderFooter phf = compositeLink.PageHeaderFooter as PageHeaderFooter;
                    phf.Footer.Content.Clear();
                    phf.Footer.Content.AddRange(new string[] { rightColumn, leftColumn });
                    phf.Footer.LineAlignment = BrickAlignment.Center;

                    using (MemoryStream stream = new MemoryStream())
                    {
                        compositeLink.PaperKind = System.Drawing.Printing.PaperKind.Legal;
                        compositeLink.Landscape = false;
                        compositeLink.CreateDocument(false);
                        compositeLink.PrintingSystem.Document.AutoFitToPagesWidth = 1;
                        compositeLink.PrintingSystem.ExportToPdf(stream);
                        WriteToResponse("Chit Abstract", true, "pdf", stream);
                    }
                }

                else if (e.Item.Text.ToString() == "XLSX")
                {
                    gridExport.WriteXlsxToResponse();
                }
            }
            ViewState["Exportdt"] = null;
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

        protected void BtnStatisticsGo_Click(object sender, EventArgs e)
        {
            select();
        }
        protected void lbBalanceCR_Load(object sender, EventArgs e)
        {
            Label label = (Label)sender;
            if (ViewState["Credit"] != null)
                label.Text = Math.Abs((Convert.ToDecimal(ViewState["Credit"]) + Convert.ToDecimal(ViewState["NPKasar"]) + Convert.ToDecimal(ViewState["PKasar"])) - Convert.ToDecimal(ViewState["Debit"])).ToString();
        }
        protected void lbBalanceText_Load(object sender, EventArgs e)
        {
            Label label = (Label)sender;
            if (ViewState["Credit"] != null)
            {
                decimal dddd = (Convert.ToDecimal(ViewState["Credit"]) + Convert.ToDecimal(ViewState["NPKasar"]) + Convert.ToDecimal(ViewState["PKasar"])) - Convert.ToDecimal(ViewState["Debit"]);
                if (dddd < 0.00M)
                {
                    label.Text = "Balance DR";
                }
                else
                {
                    label.Text = "Balance CR";
                }
            }
            else
            {

            }
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
    }
}