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
    public partial class DayBookchit : System.Web.UI.Page
    {
        #region Object
        BusinessLayer balayer = new BusinessLayer();
        TransactionLayer trn = new TransactionLayer();
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //DateTime dddd = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                //txtFromDate.Text = dddd.ToString("dd/MM/yyyy");
                //// txtToDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                group();

            }
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (Session["UserName"] == null || Session["Branchid"] == null || Session["BranchName"] == null)
            {
                Response.Redirect(Page.ResolveUrl("~/Login.aspx"), true);
            }
        }

        public void group()
        {
            DataTable dtchit = balayer.GetDataTable("select Head_Id,GROUPNO from svcf.groupmaster where BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]));
            DataRow dr = dtchit.NewRow();

            ddlChit.DataValueField = "Head_Id";
            ddlChit.DataTextField = "GROUPNO";
            dtchit.Rows.InsertAt(dr, 0);
            ddlChit.DataSource = dtchit;
            ddlChit.DataBind();
        }


        protected void select()
        {
            double bal, totrecept, prevbal = 0, debbal = 0;
            DataTable dtBind = new DataTable();
            dtBind.Columns.Add("GeneralNumber", typeof(int));
            dtBind.Columns.Add("ChitNo1");
            dtBind.Columns.Add("MemberName");
            dtBind.Columns.Add("Date");
            dtBind.Columns.Add("Subscription");

            dtBind.Columns.Add("OtherItems");
            dtBind.Columns.Add("TotalReceipts");
            dtBind.Columns.Add("ReceiptNo");
            dtBind.Columns.Add("Amountpaidtosubscriber");

            dtBind.Columns.Add("Foremanscommision");
            dtBind.Columns.Add("OtherItems1");
            dtBind.Columns.Add("TotalExpenditore");
            dtBind.Columns.Add("Balance");
            dtBind.Columns.Add("CRDR");
            dtBind.Columns.Add("Signatur");
            dtBind.Columns.Add("Remarks");

            dtBind.Columns.Add("Interest");
            dtBind.Columns.Add("withdrawbank");
            dtBind.Columns.Add("Depositbank");


            DataRow dr = dtBind.NewRow();
            int count = 0;

            //DataTable tab3 = balayer.GetDataTable("select v1.ChoosenDate ,v1.Amount ,sum(v1.Amount) as totalamount FROM svcf.voucher v1 join svcf.membertogroupmaster mg1 on mg1.Head_Id=v1.Head_Id where v1.ChitGroupID=190 and v1.BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and  v1.Trans_Type<>1 and v1.Other_Trans_Type<>5 and Voucher_Type='C' and   v1.ChoosenDate between  '" + balayer.indiandateToMysqlDate(txtFromDate.Text) + "' and '" + balayer.indiandateToMysqlDate(txtToDate.Text) + "' group by v1.ChoosenDate ");

            //for (int k = 0; k < tab3.Rows.Count; k++)
            //{

            DataTable tab1 = balayer.GetDataTable("select v1.ChoosenDate ,mg1.MemberName ,mg1.Head_ID,mg1.GrpMemberID,v1.Narration,v1.Voucher_No,v1.Amount FROM svcf.voucher v1 join svcf.membertogroupmaster mg1 on mg1.Head_Id=v1.Head_Id where v1.ChitGroupID=190 and v1.BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and  v1.Trans_Type<>1 and v1.Other_Trans_Type<>5 and Voucher_Type='C' and   v1.ChoosenDate between '" + balayer.indiandateToMysqlDate(txtFromDate.Text) + "' and '" + balayer.indiandateToMysqlDate(txtToDate.Text) + "'");

            DataTable tab2 = balayer.GetDataTable("select t1.Description ,t1.PaymentDate ,t1.PrizedAmount,t1.ChitAmount,a1.KasarAmount,a1.TotalCommission,g1.ChitPeriod,Round(a1.KasarAmount/g1.ChitPeriod,2) as total,mg1.GrpMemberID from svcf.trans_payment t1  join svcf.auctiondetails a1 on a1.PrizedMemberID=t1.TokenNumber join svcf.groupmaster g1 on g1.Head_ID=t1.ChitGroupID join svcf.membertogroupmaster mg1 on mg1.Head_Id=a1.PrizedMemberID where t1.ChitGroupID=190 and t1.PaymentDate  between '" + balayer.indiandateToMysqlDate(txtFromDate.Text) + "' and '" + balayer.indiandateToMysqlDate(txtToDate.Text) + "' and t1.BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + "");


                tab1.Merge(tab2);
                tab1.DefaultView.Sort = "ChoosenDate ASC";
                // tab1.DefaultView.Sort = "PaymentDate ASC";
                tab1 = tab1.DefaultView.ToTable(true);

                decimal decgrptotal = 0.00m;
                count += 1;
                for (int i = 0; i < tab1.Rows.Count; i++)
                {
                    dr["GeneralNumber"] = i + 1;
                    dr["ChitNo1"] = tab1.Rows[i]["GrpMemberID"].ToString();

                    string name = tab1.Rows[i]["MemberName"].ToString();

                    string kasar = tab1.Rows[i]["ChitPeriod"].ToString() + "/n" + "Members Kasar";
                    if (string.IsNullOrEmpty(name))
                    {
                        dr["MemberName"] = tab1.Rows[i]["Description"].ToString() + "\n" + kasar;
                    }

                    else
                    {
                        dr["MemberName"] = tab1.Rows[i]["MemberName"].ToString();
                    }

                    string date = tab1.Rows[i]["ChoosenDate"].ToString();
                    string datecheck = "";
                    if (string.IsNullOrEmpty(date))
                    {
                        dr["Date"] = tab1.Rows[i]["PaymentDate"].ToString();
                        date = tab1.Rows[i]["PaymentDate"].ToString();
                        if (tab1.Rows.Count > (i + 1))
                        {
                            datecheck = tab1.Rows[i + 1]["PaymentDate"].ToString();
                            if (string.IsNullOrEmpty(datecheck))
                            {
                                datecheck = tab1.Rows[i + 1]["ChoosenDate"].ToString();
                            }
                        }
                        else
                        {
                            datecheck = "";
                        }
                    }
                    else
                    {
                        if (tab1.Rows.Count > (i + 1))
                        {
                            datecheck = tab1.Rows[i + 1]["ChoosenDate"].ToString();
                            if (string.IsNullOrEmpty(datecheck))
                            {
                                datecheck = tab1.Rows[i + 1]["PaymentDate"].ToString();
                            }
                        }
                        else
                        {
                            datecheck = "";
                        }

                        dr["Date"] = tab1.Rows[i]["ChoosenDate"].ToString();
                    }

                    dr["OtherItems"] = (tab1.Rows[i]["total"]).ToString();
                    //if (!string.IsNullOrEmpty(datecheck) && !string.IsNullOrEmpty(date))
                    //{
                    //    if (Convert.ToDateTime(datecheck).ToString("dd/MM/yyyy") == Convert.ToDateTime(date).ToString("dd/MM/yyyy"))
                    //    {
                    //        if (!string.IsNullOrEmpty(Convert.ToString(tab1.Rows[i]["Amount"])))
                    //        {
                    //            //decgrptotal += Convert.ToDecimal(tab1.Rows[i]["Amount"]);
                    //            decgrptotal = Convert.ToDecimal(tab1.Rows[i]["Amount"]);
                    //        }
                    //        dr["OtherItems"]="";                            
                    //    }
                    //    else
                    //    {
                    //        if (!string.IsNullOrEmpty(Convert.ToString(tab1.Rows[i]["Amount"])))
                    //        {
                    //            //dr["OtherItems"] = (decgrptotal + Convert.ToDecimal(tab1.Rows[i]["Amount"])).ToString();
                    //            dr["OtherItems"] = Convert.ToDecimal(tab1.Rows[i]["Amount"]).ToString();                                
                    //        }
                    //        else
                    //        {

                    //            dr["OtherItems"] = (decgrptotal ).ToString();                               
                    //        }

                    //        decgrptotal = 0.00m;
                    //    }
                    //}
                    //else
                    //{
                    //   dr["OtherItems"] = decgrptotal.ToString();                      
                    //   decgrptotal = 0.00m;
                    //}

                    if (!string.IsNullOrEmpty(datecheck) && !string.IsNullOrEmpty(date))
                    {
                        if (Convert.ToDateTime(datecheck).ToString("dd/MM/yyyy") == Convert.ToDateTime(date).ToString("dd/MM/yyyy"))
                        {
                            if (!string.IsNullOrEmpty(Convert.ToString(tab1.Rows[i]["Amount"])))
                            {
                                decgrptotal += Convert.ToDecimal(tab1.Rows[i]["Amount"]);
                            }
                            //dr["OtherItems"]="";
                            dr["TotalReceipts"] = "";
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(Convert.ToString(tab1.Rows[i]["Amount"])))
                            {
                                //dr["OtherItems"] = (decgrptotal + Convert.ToDecimal(tab1.Rows[i]["Amount"])).ToString();
                                dr["TotalReceipts"] = (decgrptotal + Convert.ToDecimal(tab1.Rows[i]["Amount"])).ToString();
                            }
                            else
                            {

                                //dr["OtherItems"] = (decgrptotal ).ToString();
                                dr["TotalReceipts"] = (decgrptotal).ToString();
                            }

                            decgrptotal = 0.00m;
                        }
                    }
                    else
                    {
                        //dr["OtherItems"] = decgrptotal.ToString();
                        dr["TotalReceipts"] = decgrptotal.ToString();

                        decgrptotal = 0.00m;
                    }

                    dr["Subscription"] = tab1.Rows[i]["Amount"].ToString();
                    dr["ReceiptNo"] = tab1.Rows[i]["Voucher_No"].ToString();
                    dr["Amountpaidtosubscriber"] = tab1.Rows[i]["PrizedAmount"].ToString();
                    dr["Foremanscommision"] = tab1.Rows[i]["TotalCommission"].ToString();
                    dr["TotalExpenditore"] = tab1.Rows[i]["ChitAmount"].ToString();

                    if (i == 0)
                    {
                        if (dr["TotalReceipts"] != "")
                        {
                            dr["Balance"] = "";
                        }
                        else
                        {
                            dr["Balance"] = dr["TotalReceipts"];
                        }

                    }
                    else
                    {
                        if (dtBind.Rows[i - 1]["Balance"] != "")
                        {
                            bal = Convert.ToDouble(dtBind.Rows[i - 1]["Balance"]);
                        }
                        else
                        {
                            bal = 0;
                        }
                        if (dr["TotalReceipts"] != "")
                        {
                            totrecept = Convert.ToDouble(dr["TotalReceipts"]);
                        }
                        else
                        {
                            totrecept = 0;
                        }
                        prevbal = prevbal + totrecept;
                        if (totrecept == 0)
                        {
                            dr["Balance"] = "";
                        }
                        else
                        {
                            dr["Balance"] = prevbal;
                        }


                    }
                    if (dr["TotalExpenditore"] != "")
                    {

                        if (dr["Balance"] != "")
                        {
                            debbal = Convert.ToDouble(dr["Balance"]);
                        }
                        else
                        {
                            debbal = 0;
                        }
                        if (Convert.ToDouble(dr["TotalExpenditore"]) > debbal)
                        {
                            dr["CRDR"] = "DR";
                        }
                        else
                        {
                            dr["CRDR"] = "CR";
                        }
                    }
                    else
                    {
                        dr["CRDR"] = "CR";
                    }


                    dtBind.Rows.Add(dr.ItemArray);


                }

            //    object smtab1;
            //    smtab1 = tab1.Compute("sum(ChitAmount)", "");
            //    if (string.IsNullOrEmpty(balayer.ToobjectstrEvenNull(smtab1)))
            //        smtab1 = 0.00M;
            //    dr["TotalExpenditore"] = smtab1;

            //    dtBind.Rows.Add(dr.ItemArray);
                grid.DataSource = dtBind;
                grid.DataBind();

            //}


           

        }


        protected void BtnStatisticsGo_Click(object sender, EventArgs e)
        {
            select();
        }
    }
}