using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SVCF_BusinessAccessLayer;
using SVCF_TransactionLayer;
using SVCF_DataAccessLayer;

namespace SreeVisalamChitFundLtd_phase1
{
    public partial class MoneyCollectorArrears : System.Web.UI.Page
    {
        #region Object
        BusinessLayer balayer = new BusinessLayer();
        TransactionLayer trn = new TransactionLayer();
        Dictionary<string, string> Tempdic = new Dictionary<string, string>();
        string qry = "";
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DateTime dddd = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
               // txtFromDate.Text = dddd.ToString("dd/MM/yyyy");
                txtToDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                CollectorName();
            }
        }
        string Sname;
        string head;
        string draw;
        string val, np;
        int sum = 0;
        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (Session["UserName"] == null || Session["Branchid"] == null || Session["BranchName"] == null)
            {
                Response.Redirect(Page.ResolveUrl("~/Login.aspx"), true);
            }
        }
       // 
        public void CollectorName()
        {
            qry = "Select moneycollid,moneycollname from moneycollector where BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]);
            Tempdic.Clear();
            Tempdic = balayer.CmnList(qry);
            ddlMname.DataValueField = "Key";
            ddlMname.DataTextField = "Value";
            ddlMname.DataSource = Tempdic;
            ddlMname.DataBind();
        }

        public void LoadGrid()
        {
            try
            {
                #region Vardeclaration
                double chitpaidamount = 0;
                DataTable findrdt = null;
                double auctiondueamnt = 0;
                double arrearamnt = 0;
                int maxdrawNo = 0;
                int insno = 0;
                DataTable paiddt = new DataTable();
                DataTable lastpaiddt =null;
                string NP = "";
                #endregion
                DataTable chitdt = new DataTable();
                DataTable dtBind = new DataTable();
                dtBind.Columns.Add("slno", typeof(int));
                dtBind.Columns.Add("GrpMemberID");
                dtBind.Columns.Add("MemberName");
                dtBind.Columns.Add("ArrAmount", typeof(double));
                dtBind.Columns.Add("Date");
                dtBind.Columns.Add("Collected");
                dtBind.Columns.Add("DrawNO");
                dtBind.Columns.Add("IsPrized");               
                dtBind.Columns.Add("AmountCollected");
                DataRow dr = dtBind.NewRow();
                string tt = ddlMname.SelectedItem.Text;
                auctiondueamnt = 0;
                qry = "select Head_Id,GrpMemberID,BranchId,GroupID,MemberName from membertogroupmaster where M_Id=" + ddlMname.SelectedValue +"";
                for (int row = 0; row <= chitdt.Rows.Count - 1; row++)
                {
                    dr["slno"] = row + 1;
                    dr["GrpMemberID"] = chitdt.Rows[row]["GrpMemberID"].ToString();
                    dr["GrpMemberID"] = chitdt.Rows[row]["MemberName"].ToString();
                    qry = "select sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end)as Amount,max(ChoosenDate) FROM svcf.voucher as t1 where " +
                          "ChitGroupID=" + chitdt.Rows[row]["GroupID"].ToString() + " and t1.RootID=5 and t1.Other_Trans_Type<>5 and t1.Voucher_Type='C' " +
                           "and t1.Head_ID=" + chitdt.Rows[row]["Head_Id"].ToString() + " and `t1`.`BranchID`=" + chitdt.Rows[row]["BranchId"].ToString() + " " +
                           " and ChoosenDate<='" + balayer.indiandateToMysqlDate(txtToDate.Text) + "' order by t1.ChoosenDate asc";

                    paiddt = balayer.GetDataTable(qry);
                    chitpaidamount = balayer.GetScalarDataDbl(qry);
                    qry = "";
                    qry = "select DrawNO,CurrentDueAmount from auctiondetails where GroupID=" + chitdt.Rows[row]["GroupID"].ToString();
                    findrdt = balayer.GetDataTable(qry);                   
                    for (int lt = 0; lt <= findrdt.Rows.Count - 1; lt++)
                    {
                        // tempDueAmount = tempDueAmount + decimal.Parse(findrdt.Rows[lt]["CurrentDueAmount"].ToString());
                        auctiondueamnt = auctiondueamnt + Convert.ToDouble(findrdt.Rows[lt]["CurrentDueAmount"]);
                        if (auctiondueamnt >= chitpaidamount)
                        {
                            insno = Convert.ToInt32(findrdt.Rows[lt]["DrawNO"].ToString());
                            //insno[insloopcnt] = Convert.ToInt32(findrdt.Rows[lt]["DrawNO"]);
                            //insloopcnt = insloopcnt + 1;
                            break;
                        }
                        else
                        {
                            auctiondueamnt = auctiondueamnt + Convert.ToDouble(findrdt.Rows[lt]["CurrentDueAmount"]);
                            if (auctiondueamnt >= chitpaidamount)
                            {
                                insno = Convert.ToInt32(findrdt.Rows[lt]["DrawNO"].ToString());
                                //insno[insloopcnt] = Convert.ToInt32(findrdt.Rows[lt]["DrawNO"]);
                                //insloopcnt = insloopcnt + 1;
                                break;
                            }
                        }
                    }
                    qry = "select max(DrawNO) from auctiondetails where GroupID=" + chitdt.Rows[row]["GroupID"].ToString() + " and CurrentDueAmount>0";
                    maxdrawNo = balayer.GetScalarDataInt(qry);
                    if (insno < maxdrawNo)
                    {
                        dr["DrawNO"] = insno.ToString() + "-" + maxdrawNo.ToString();
                    }
                    else
                    {
                        dr["DrawNO"] = maxdrawNo.ToString();
                    }
                    qry = "";
                    qry = "select  (case when( (vgwd1.TotaldueAmount-sum(case when (v1.Voucher_Type='C' and " +
                        "v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) +sum(case when (v1.Voucher_Type='D' and v1.trans_Type<>2 and " +
                        "v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) )>0.00) then (vgwd1.TotaldueAmount-sum(case when (v1.Voucher_Type='C' and " +
                        "v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) +sum(case when (v1.Voucher_Type='D' and v1.trans_Type<>2 and " +
                        "v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) ) else 0.00 end) as 'Arrear Amount' from membertogroupmaster as mg1 join " +
                        "voucher as v1 on v1.Head_Id=mg1.Head_Id join view_groupwisedue as vgwd1 on vgwd1.`GroupId`=" + chitdt.Rows[row]["GroupID"].ToString() + " left join trans_payment as tp1 on " +
                        "v1.Head_Id =tp1.TokenNumber join membermaster as mm on (mg1.MemberID=mm.MemberIDNew) where mg1.BranchID=" + chitdt.Rows[row]["BranchId"].ToString() + " and mg1.GroupID=" + chitdt.Rows[row]["GroupID"].ToString() + " and " +
                        "v1.Head_Id=" + chitdt.Rows[row]["Head_Id"].ToString() + " and v1.ChoosenDate<='" + balayer.indiandateToMysqlDate(txtToDate.Text) + "' group by v1.Head_Id order by cast(digits(mg1.GrpMemberID) as unsigned),v1.Voucher_Type DESC";
                    arrearamnt = balayer.GetScalarDataDbl(qry);
                    dr["ArrAmount"] = maxdrawNo;
                    qry = "";
                    NP = balayer.GetSingleValue("select  Replace(Replace (IsPrized,'Y','P'),'N','NP') as IsPrized,GroupID from svcf.auctiondetails where MemberID='" + draw + "' and GroupID='" + Sname + "'  and AuctionDate<='" + balayer.indiandateToMysqlDate(txtToDate.Text) + "'  group by GroupID");
                    if (string.IsNullOrEmpty(NP))
                    {
                        dr["IsPrized"] = string.Empty;
                    }
                    else
                    {
                        dr["IsPrized"] = NP;
                    }
                    qry = "";
                    qry = "select case when t1.Voucher_Type='C' then t1.Amount else 0.00 end as Amount,ChoosenDate FROM svcf.voucher as t1 where " +
                        "ChitGroupID=" + chitdt.Rows[row]["GroupID"].ToString() + " and t1.RootID=5 and t1.Other_Trans_Type<>5 and t1.Voucher_Type='C'and " +
                        "t1.Head_ID=" + chitdt.Rows[row]["Head_Id"].ToString() + " and `t1`.`BranchID`=" + chitdt.Rows[row]["BranchId"].ToString() + " "+
                        " and ChoosenDate<='" + balayer.indiandateToMysqlDate(txtToDate.Text) + "' order by t1.ChoosenDate desc";
                    lastpaiddt = new DataTable();
                    lastpaiddt = balayer.GetDataTable(qry);
                    dr["Date"] = lastpaiddt.Rows[0]["ChoosenDate"].ToString();
                    dr["AmountCollected"] = lastpaiddt.Rows[0]["Amount"].ToString();

                    dtBind.Rows.Add(dr.ItemArray);
                }
                GV_MCArrear.DataSource = dtBind;
                GV_MCArrear.DataBind();


            }
            catch (Exception ex)
            {

            }
        }

        protected void select()
        {
            DataTable dtBind = new DataTable();
            dtBind.Columns.Add("slno", typeof(int));
            dtBind.Columns.Add("GrpMemberID");
            dtBind.Columns.Add("MemberName");
            dtBind.Columns.Add("Amount",typeof(decimal));
            dtBind.Columns.Add("Date");
            dtBind.Columns.Add("Collected");
            dtBind.Columns.Add("DrawNO");
            dtBind.Columns.Add("IsPrized");
            DataRow dr = dtBind.NewRow();
            balayer.GetInsertItem("CREATE OR REPLACE VIEW `unpaidprizedmoney` AS select `voucher`.`Head_Id` AS `Head_Id`, `membertogroupmaster`.`GrpMemberID` AS `GrpMemberID`, (case when (`voucher`.`Voucher_Type` = 'C') then sum(`voucher`.`Amount`) else 0.00 end) AS `Credit`, (case when (`voucher`.`Voucher_Type` = 'D') then sum(`voucher`.`Amount`) else 0.00 end) AS `Debit`, ((case when (`voucher`.`Voucher_Type` = 'C') then sum(`voucher`.`Amount`) else 0.00 end) - (case when (`voucher`.`Voucher_Type` = 'D') then sum(`voucher`.`Amount`) else 0.00 end)) AS `AmountActuallyremittedbytheParty` from (`voucher` join `membertogroupmaster` ON ((`voucher`.`Head_Id` = `membertogroupmaster`.`Head_Id`))) group by `voucher`.`Head_Id`");

            DataTable dtDate = balayer.GetDataTable("select distinct ChoosenDate from voucher where Trans_Type<>2 and Voucher_Type='C' and BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and choosenDate between '" + balayer.indiandateToMysqlDate(txtToDate.Text) + "' and '" + balayer.indiandateToMysqlDate(txtToDate.Text) + "' and Trans_Medium=0 and Other_Trans_Type not in (3,5) order by ChoosenDate asc");
            int count = 0;
            //for (int i = 0; i < dtDate.Rows.Count; i++)
            //{
            DataTable dtInit = balayer.GetDataTable("select DISTINCT m1.GrpMemberID,m1.MemberName ,m1.GroupID,m1.head_Id,m1.MemberID from svcf.voucher v1 join svcf.membertogroupmaster m1 on v1.MemberID=m1.MemberID join svcf.moneycollector m2 on  m1.M_Id=m2.moneycollid where m2.moneycollname='" + ddlMname.SelectedItem.Text + "'");

           // DataTable dtInit = balayer.GetDataTable("select DISTINCT case when  P.DrawNO='' then 0 else P.DrawNO  end as DrawNO,m1.GrpMemberID,m1.MemberName ,m1.GroupID,m1.head_Id,m1.MemberID from svcf.voucher v1 join svcf.membertogroupmaster m1 on v1.MemberID=m1.MemberID join svcf.auctiondetails P on P.MemberID=m1.MemberID join svcf.moneycollector m2 on  m1.M_Id=m2.moneycollid where m2.moneycollname='" + ddlChit.SelectedItem.Text + "' and P.AuctionDate<='" + balayer.indiandateToMysqlDate(txtFromDate.Text) + "'");


            //DataTable dtInit = balayer.GetDataTable("select DISTINCT  sum(v1.Amount)as Amount,max(v1.ChoosenDate) as ChoosenDate m1.GrpMemberID,m1.MemberName from svcf.voucher v1 join svcf.membertogroupmaster m1 on v1.MemberID=m1.MemberID join svcf.moneycollector m2 on  m1.M_Id=m2.moneycollid where m2.moneycollname='" + ddlChit.SelectedItem.Text + "' and v1.voucher_type='C' and ChoosenDate<='" + balayer.indiandateToMysqlDate(txtFromDate.Text) + "'");

            //DataTable dtInit = balayer.GetDataTable(" select sum(v1.Amount) as Amount,max(v1.ChoosenDate) as ChoosenDate, m1.GrpMemberID,m1.MemberName from svcf.voucher v1 join svcf.membertogroupmaster m1 on v1.MemberID=m1.MemberID join svcf.moneycollector m2 on  m1.M_Id=m2.moneycollid where m2.moneycollname='" + ddlChit.SelectedItem.Text + "' and v1.voucher_type='C' and ChoosenDate<='" + balayer.indiandateToMysqlDate(txtFromDate.Text) + "' group by GrpMemberID");

            //DataTable dtInit = balayer.GetDataTable("select  sum(a1.currentdueamount)as Collected,sum(v1.Amount) as Amount,max(v1.ChoosenDate) as ChoosenDate,m1.GrpMemberID,m1.MemberName from svcf.voucher v1 join svcf.auctiondetails a1 on a1.Groupid=v1.ChitGroupId join svcf.membertogroupmaster m1 on v1.MemberID=m1.MemberID join svcf.moneycollector m2 on  m1.M_Id=m2.moneycollid where m2.moneycollname='" + ddlChit.SelectedItem.Text + "' and v1.voucher_type='C' and ChoosenDate<='" + balayer.indiandateToMysqlDate(txtFromDate.Text) + "' and a1.AuctionDate<='" + balayer.indiandateToMysqlDate(txtFromDate.Text) + "'group by GrpMemberID");

            //DataTable dt = balayer.GetDataTable("select DISTINCT sum(currentdueamount) as Amount from svcf.auctiondetails where Groupid='" + Sname + "' and AuctionDate<='" + balayer.indiandateToMysqlDate(txtFromDate.Text) + "' group by Groupid");

             count += 1;
             for (int j = 0; j < dtInit.Rows.Count; j++)
             {
                 dr["slno"] = j + 1;
                 dr["GrpMemberID"] = dtInit.Rows[j]["GrpMemberID"];
                 dr["MemberName"] = dtInit.Rows[j]["MemberName"];
                 //dr["Call"] = dtInit.Rows[j]["Call"];
                 Sname = dtInit.Rows[j]["GroupID"].ToString();
                 head = dtInit.Rows[j]["head_Id"].ToString();
                 draw = dtInit.Rows[j]["MemberID"].ToString();
                 DataTable dt = balayer.GetDataTable("select DISTINCT sum(currentdueamount) as Amount from svcf.auctiondetails where Groupid='" + Sname + "' and AuctionDate<='" + balayer.indiandateToMysqlDate(txtToDate.Text) + "' group by Groupid");
                 DataTable dt1 = balayer.GetDataTable("SELECT DISTINCT sum(Amount) as Collected,max(ChoosenDate) as Date FROM svcf.voucher where head_Id='" + head + "' and voucher_type='C' and ChoosenDate<='" + balayer.indiandateToMysqlDate(txtToDate.Text) + "' group by head_Id");
                 string date = dt1.Rows[0]["Date"].ToString();

                 string DRN = balayer.GetSingleValue("select  case when DrawNO=' ' then 0 else DrawNO end as DrawNO,GroupID from svcf.auctiondetails  where MemberID='" + draw + "' and GroupID='" + Sname + "'  and AuctionDate<='" + balayer.indiandateToMysqlDate(txtToDate.Text) + "'  group by GroupID");
                 string NP = balayer.GetSingleValue("select  Replace(Replace (IsPrized,'Y','P'),'N','NP') as IsPrized,GroupID from svcf.auctiondetails where MemberID='" + draw + "' and GroupID='" + Sname + "'  and AuctionDate<='" + balayer.indiandateToMysqlDate(txtToDate.Text) + "'  group by GroupID");

                 string lostcoll = balayer.GetSingleValue("SELECT Amount  FROM svcf.voucher where head_Id='" + head + "' and voucher_type='C' and ChoosenDate='" + balayer.indiandateToMysqlDate(date) + "'");

                 dr["Amount"] = dt.Rows[0]["Amount"];
                 dr["Collected"] = lostcoll.ToString();
                 dr["Date"] = dt1.Rows[0]["Date"];

                 if (string.IsNullOrEmpty(DRN))
                 {
                     dr["DrawNO"] = string.Empty;
                 }
                 else
                 {
                     dr["DrawNO"] = DRN;
                 }

                 if (string.IsNullOrEmpty(NP))
                 {
                     dr["IsPrized"] = string.Empty;
                 }
                 else
                 {
                     dr["IsPrized"] = NP;
                 }
                 dtBind.Rows.Add(dr.ItemArray);
             }
                // SELECT sum(Amount),max(ChoosenDate) FROM svcf.voucher where head_Id=203 and voucher_type='C' and ChoosenDate<='2013-07-31';(amt collect)

                //select  sum(currentdueamount) from svcf.auctiondetails where Groupid=178 and AuctionDate<='2013-07-31';(arreear amt)

                ViewState["Amount"] = dtBind.Compute("Sum(Amount)", "");
                GV_MCArrear.DataSource = dtBind;
                GV_MCArrear.DataBind();


                DataTable dtBind1 = new DataTable();
                DataRow drrr = dtBind1.NewRow();


                DataView dv = new DataView(dtBind);


                dtBind1 = dv.ToTable(true, "slno", "IsPrized", "Amount");
                string pri = "p";
                List<int> li = new List<int>();
                for (int i = 0; i < dtBind1.Rows.Count; i++)
                {
                    string a = dtBind1.Rows[i]["IsPrized"].ToString();

                    if (pri == a)
                    {
                        int b = Convert.ToInt32(dtBind1.Rows[i]["Amount"]);
                        li.Add(b);
                    }
                }
                int sum = li.Sum();


        }

        protected void tot_Load(object sender, EventArgs e)
        {
            Label label = (Label)sender;
            if (ViewState["Amount"] != null)
                label.Text = Convert.ToString(ViewState["Amount"]);
            
        }
        protected void BtnStatisticsGo_Click(object sender, EventArgs e)
        {
            //select();
            LoadGrid();

        }
    }
} 

 