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
using SVCF_BusinessAccessLayer;
using SVCF_TransactionLayer;
using SVCF_DataAccessLayer;

namespace SreeVisalamChitFundLtd_phase1
{
    public partial class AuctionIntimation_FreeForm : System.Web.UI.Page
    {
        #region Object
        BusinessLayer balayer = new BusinessLayer();
        TransactionLayer trn = new TransactionLayer();
        #endregion
        IntimationLetter cr;
        private void SetParamValue(string paramName, string paramValue)
        {
            for (int i = 0; i < cr.DataDefinition.FormulaFields.Count; i++)

                if (cr.DataDefinition.FormulaFields[i].FormulaName == "{" + paramName + "}")

                    cr.DataDefinition.FormulaFields[i].Text = "\"" + paramValue + "\"";
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ddlChitCategory.Items.Add("Monthly");
                ddlChitCategory.Items.Add("Tri-Monthly");
                ddlChitCategory.Items.Add("FortNigthly");
                ddlChitCategory.SelectedIndex = 0;
                DataTable dtcmpGroup = balayer.GetDataTable("Select GROUPNO from groupmaster Where BranchID='" + Convert.ToString(balayer.ToobjectstrEvenNull(Session["Branchid"])) + "'");

                branch.Text = balayer.GetSingleValue("select B_Name from branchdetails where B_Code='" + Convert.ToString(balayer.ToobjectstrEvenNull(Session["Branchid"])) + "'");
                ddlGroupNO.DataSource = dtcmpGroup;
                ddlGroupNO.DataTextField = "GROUPNO";
                ddlGroupNO.DataValueField = "GROUPNO";
                ddlGroupNO.DataBind();
                ddlGroupNO.Items.Insert(0, "--select--");
                ddlGroupNO.SelectedIndex = 0;
            }
        }
        protected void ddlGroupNO_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtselgroup_no.Text = ddlGroupNO.SelectedItem.Text;
        }

        //ddlGroupNO_SelectedIndexChanged

        protected void btnautofill_Click(object sender, EventArgs e)
        {
            if (txtselgroup_no.Text.Trim().Equals("") || txtselgroup_no.Text.Trim().Equals("--select--"))
            {
                return;
            }

            //if (!Page.IsValid)
            //{
            //    return;
            //}
            if (ddlGroupNO.SelectedIndex == -1)
            {
                return;
            }
            string strquery = "SELECT t2.B_Name,t2.B_Address,t1.DueDate,t1.ChitAgreementNo,t1.ChitAgreementYear,t1.ChitPeriod,t1.ChitCategory,t1.NoofMembers,t1.AuctionDate,TIME_FORMAT(t1.AuctionTime,'%r') as AuctionTime  FROM  `groupmaster` as t1 inner join branchdetails as t2 on t1.BranchID=t2.B_CODE where t1.GROUPNO='" + ddlGroupNO.SelectedItem.ToString() + "'";




            DataTable dtReport = new DataTable();


            dtReport = balayer.GetDataTable(strquery);


            //   SetParamValue("@ChitNumber", cmbGroup.SelectedItem.ToString());

            txtselgroup_no.Text = ddlGroupNO.SelectedItem.Text;


            ////// SetParamValue("@BranchName", dtReport.Rows[0]["B_Name"].ToString());


            // txt.Text = ;
            string BranchName = dtReport.Rows[0]["B_Name"].ToString();

            //////SetParamValue("@NumberofDraw", dtReport.Rows[0]["ChitPeriod"].ToString());


            // SetParamValue("@BranchAddress", "  " + dtReport.Rows[0]["B_Address"].ToString() + "  ");

            txtbranchaddr.Text = dtReport.Rows[0]["B_Address"].ToString();
            // SetParamValue("@AgrementNumber", dtReport.Rows[0]["ChitAgreementNo"].ToString());
            txtchit_agree_no.Text = dtReport.Rows[0]["ChitAgreementNo"].ToString();

            // SetParamValue("@Agrementyear", dtReport.Rows[0]["ChitAgreementYear"].ToString());
            txt_chit_agree_year.Text  = dtReport.Rows[0]["ChitAgreementYear"].ToString();

            //////SetParamValue("@Today", DateTime.Now.ToString("dd/MM/yy"));


            //////SetParamValue("@ChitCategory", dtReport.Rows[0]["ChitCategory"].ToString());

            DateTime TempDate = DateTime.Parse(dtReport.Rows[0]["AuctionDate"].ToString());
            DateTime AuctionDate = new DateTime(DateTime.Now.Year, TempDate.Month, TempDate.Day);
            DateTime dueDate = DateTime.Parse(dtReport.Rows[0]["DueDate"].ToString());
            // DateTime AuctionDate = dueDate.AddDays(1);
            // string strDate = dueDate.ToString("yy/MM/dd");
            Double Drawo = (DateTime.Today - TempDate).TotalDays;


            int DrawNumber = int.Parse(Math.Ceiling(Drawo / 30.0).ToString()) + 1;



            string strDrawNo = "";
            string strPrevDrawNo = "";

            if (DrawNumber != 2 || DrawNumber != 2)
            {
                strDrawNo = DrawNumber.ToString() ;
            }
            else
            {
                if (DrawNumber == 2)
                {
                    strDrawNo = DrawNumber.ToString() ;
                }
                else
                {
                    strDrawNo = DrawNumber.ToString() ;
                }
            }

            DrawNumber = DrawNumber - 1;
            if (DrawNumber != 2 || DrawNumber != 2)
            {
                strPrevDrawNo = DrawNumber.ToString() ;
            }
            else
            {
                if (DrawNumber == 2)
                {
                    strPrevDrawNo = DrawNumber.ToString();
                }
                else
                {
                    strPrevDrawNo = DrawNumber.ToString() ;
                }
            }
            //DateTime dtAuctionTime = DateTime.ParseExact(dtReport.Rows[0]["AuctionTime"].ToString(),"HHmmss", null);
            string convertedAuctionTime = dtReport.Rows[0]["AuctionTime"].ToString();
            string [] strTime = convertedAuctionTime.Split(':');

            if (strTime[2].ToLower().Contains("am"))
            {
                TimeSelAuction.SetTime(int.Parse(strTime[0]), int.Parse(strTime[1]), 00, MKB.TimePicker.TimeSelector.AmPmSpec.AM );
            }
            else
            {
                TimeSelAuction.SetTime(int.Parse(strTime[0]), int.Parse(strTime[1]), 00, MKB.TimePicker.TimeSelector.AmPmSpec.PM );
            }
            //SetParamValue("@CurrentDrawNo", strDrawNo);
            txtcurrdraw.Text = strDrawNo;

            // SetParamValue("@AuctionDate", AuctionDate.ToString("dd/MM/yy"));

            txtauction_dt.Text = AuctionDate.ToString("dd/MM/yy");

            //SetParamValue("@AuctionTime", convertedAuctionTime);
            //TimeSelAuction.SetTime(


            ////// SetParamValue("@PInstallamount", balayer.ConvertToIndCurrency(txtInstalmentAmount.Text));

            //SetParamValue("@PrevDrawNO", strPrevDrawNo);
            //txtprevdraw.Text = strPrevDrawNo;

            ////////SetParamValue("@PrevDivident", balayer.ConvertToIndCurrency(txtPreviousDivident.Text));
            //txtprevdividend.Text = balayer.ConvertToIndCurrency(txtPreviousDivident.Text);

            //SetParamValue("@DueDate", dueDate.ToString("dd/MM/yy"));
            txtduedate.Text = dueDate.ToString("dd/MM/yy");

            //////SetParamValue("@PrizeAmount", balayer.ConvertToIndCurrency(txt_price.Text));

        }

        protected void btnGenerate_Click(object sender, EventArgs e)
        {
            Page.Validate();
            if (!Page.IsValid)
            {
                return;
            }
            if (ddlGroupNO.SelectedIndex != -1)
            {

                //string strquery = "SELECT t2.B_Name,t2.B_Address,t1.DueDate,t1.ChitAgreementNo,t1.ChitAgreementYear,t1.ChitPeriod,t1.ChitCategory,t1.NoofMembers,t1.AuctionDate,TIME_FORMAT(t1.AuctionTime,'%r') as AuctionTime  FROM  `groupmaster` as t1 inner join BranchDetails as t2 on t1.BranchID=t2.B_CODE where t1.GROUPNO='" + ddlGroupNO .SelectedItem.ToString() + "'";

                //// MySqlConnection con = SreeVisalamChitFundLtd_phase1.CommonClassFile.openConnection();

                ////MySqlDataAdapter da = new MySqlDataAdapter(strquery, con);
                //DataTable dtReport = new DataTable();
                //// da.Fill(dtReport);

                //dtReport = balayer.GetDataTable(strquery);

                //da.Dispose();
                //dtReport.Dispose();
                //con.Close();
                // con.Dispose();

                cr = new IntimationLetter();
                //string reportPath = @"C:\Documents and Settings\Administrator\Desktop\Sree Visalam Chit Fund  Ltd\Sree Visalam Chit Fund  Ltd\GroupIntimationLetter.rpt";
                //string reportPath = Server.MapPath("GroupIntimationLetter.rpt");
                string reportPath = Server.MapPath("IntimationLetter.rpt");
                cr.Load(reportPath);
                //cr.SetParameterValue(@"ChitNumber", "181/13");
                // cr.SetDataSource(ds);
                //ParameterFieldDefinition paramField;
                //ParameterValues currentValues;
                //ParameterDiscreteValue p2;
                //paramField = cr.DataDefinition.ParameterFields[@"ChitNumber"];
                //p2 = new ParameterDiscreteValue();
                //string s1;
                //s1 = "111";
                //p2.Value = s1;
                //currentValues = paramField.CurrentValues;
                //currentValues.Add(p2);
                //paramField.ApplyCurrentValues(currentValues);

                //ParameterDiscreteValue val = new ParameterDiscreteValue();
                //val.Value = "111";

                //ParameterValues paramVals = new ParameterValues();

                //paramVals.Add(val);

                //cr.ParameterFields["ChitNumber"].CurrentValues = paramVals;

                //cr.DataDefinition.ParameterFields[0].ApplyCurrentValues(paramVals);
                //cr.ParameterFields["ChitNumber"].HasCurrentValue = true;

                SetParamValue("@ChitNumber", txtselgroup_no.Text);
                SetParamValue("@BranchName", branch.Text);
                ////// SetParamValue("@NumberofDraw", dtReport.Rows[0]["ChitPeriod"].ToString());
                SetParamValue("@BranchAddress", "  " + balayer.ReplaceJnk(txtbranchaddr.Text) + "  ");
                SetParamValue("@AgrementNumber", txtchit_agree_no.Text);

                string twodigityear = "";
                if (txt_chit_agree_year.Text.Trim() != "")
                {
                    if (txt_chit_agree_year.Text.Trim().Length == 1)
                    {
                        twodigityear = "0" + txt_chit_agree_year.Text.Trim();
                    }
                    else
                        if (txt_chit_agree_year.Text.Trim().Length == 4)
                        {
                            twodigityear = txt_chit_agree_year.Text.Trim().Substring(2, 2);
                        }
                        else
                        {
                            twodigityear = txt_chit_agree_year.Text.Trim();
                        }
                }


                SetParamValue("@Agrementyear", twodigityear);
                SetParamValue("@Today", DateTime.Now.ToString("dd/MM/yy"));
                //un comment this

                if (ddlChitCategory.SelectedItem.Text.ToLower() == "monthly")
                {
                    SetParamValue("@two", "-------------");
                    SetParamValue("@three", "------------");
                }
                else
                    if (ddlChitCategory.SelectedItem.Text.ToLower() == "fortnightly")
                    {
                        SetParamValue("@one", "----------------");
                        SetParamValue("@two", "----------------");
                    }
                    else
                    {
                        SetParamValue("@one", "---------------");
                        SetParamValue("@three", "----------------");
                    }






                //end un comment this
                //  SetParamValue("@ChitCategory", "-------");//comment this

                //due to iis issue
                //System.Globalization.DateTimeFormatInfo usDtfi = new System.Globalization.CultureInfo("en-NZ", false).DateTimeFormat;


                // DateTime TempDate = Convert.ToDateTime(dtReport.Rows[0]["AuctionDate"].ToString(), usDtfi);

                // DateTime TempDate = DateTime.Parse(Convert.ToDateTime(dtReport.Rows[0]["AuctionDate"], usDtfi).ToString("mm/dd/yy"));
                //

                //  DateTime TempDate = DateTime.Parse(dtReport.Rows[0]["AuctionDate"].ToString());
                // DateTime AuctionDate = new DateTime(DateTime.Now.Year, TempDate.Month, TempDate.Day);
                // DateTime dueDate = DateTime.Parse(dtReport.Rows[0]["DueDate"].ToString());
                // DateTime AuctionDate = dueDate.AddDays(1);
                // string strDate = dueDate.ToString("yy/MM/dd");
                // Double Drawo = (DateTime.Today - TempDate).TotalDays;


                //int DrawNumber = int.Parse(Math.Ceiling(Drawo / 30.0).ToString()) + 1;



                //string strDrawNo = "";
                //string strPrevDrawNo = "";

                //if (DrawNumber != 2 || DrawNumber != 2)
                //{
                //    strDrawNo = DrawNumber.ToString() + "th";
                //}
                //else
                //{

                //    if (DrawNumber == 2)
                //    {
                //        strDrawNo = DrawNumber.ToString() + "nd";
                //    }
                //    else
                //    {
                //        strDrawNo = DrawNumber.ToString() + "rd";
                //    }
                //}

                //DrawNumber = DrawNumber - 1;
                //if (DrawNumber != 2 || DrawNumber != 2)
                //{
                //    strPrevDrawNo = DrawNumber.ToString() + "th";
                //}
                //else
                //{

                //    if (DrawNumber == 2)
                //    {
                //        strPrevDrawNo = DrawNumber.ToString() + "nd";
                //    }
                //    else
                //    {
                //        strPrevDrawNo = DrawNumber.ToString() + "rd";
                //    }
                //}
                //DateTime dtAuctionTime = DateTime.ParseExact(dtReport.Rows[0]["AuctionTime"].ToString(),"HHmmss", null);
                //string convertedAuctionTime = dtReport.Rows[0]["AuctionTime"].ToString();
                string strCurrent = "";
                if (txtcurrdraw.Text.Trim() != "")
                {
                    int DrawNumber = int.Parse(txtcurrdraw.Text.Replace("th", "").Replace("nd", "").Replace("rd", ""));
                    if (DrawNumber != 2 || DrawNumber != 2)
                    {
                        strCurrent = DrawNumber.ToString() + "th";
                    }
                    else
                    {
                        if (DrawNumber == 2)
                        {
                            strCurrent = DrawNumber.ToString() + "nd";
                        }
                        else
                        {
                            strCurrent = DrawNumber.ToString() + "rd";
                        }
                    }



                    //int PreviousDraw = int.Parse(txtcurrdraw.Text )- 1;
                    //  SetParamValue("@PrevDrawNO", strPrevDrawNo.ToString());

                    SetParamValue("@CurrentDrawNo", strCurrent);
                }
                SetParamValue("@AuctionDate", txtauction_dt.Text);

                if (txtauction_dt.Text.Trim() != "")
                {
                    DateTime dtAuct = Convert.ToDateTime(txtauction_dt.Text, System.Globalization.CultureInfo.GetCultureInfo("en-NZ"));
                    string[] parts = dtAuct.ToString("dd/MMM/yy").Split('/');
                    SetParamValue("@AuctionDay", parts[0]);
                    SetParamValue("@AuctionMonth", parts[1] );
                    SetParamValue("@AuctionYear", parts[2]);
                }
                if (TimeSelAuction.AmPm.ToString() == "AM")
                {
                    SetParamValue("@PM", "---");
                }
                else
                {
                    SetParamValue("@AM", "---");
                }


                string hour = TimeSelAuction.Hour.ToString();
                string minute = TimeSelAuction.Minute.ToString();
                if (hour.Length == 1)
                {
                    hour = "0" + hour;
                }
                if (minute.Length == 1)
                {
                    minute = "0" + minute;
                }

                SetParamValue("@AuctionTime", hour + ":" + minute) ;
                //////SetParamValue("@PInstallamount", txtprevdividend.Text);
                string strPrevDrawNo = "";
                if (txtcurrdraw.Text.Trim() != "")
                {
                    int DrawNumber = int.Parse(txtcurrdraw.Text.Replace("th", "").Replace("nd", "").Replace("rd", "")) - 1;
                    if (DrawNumber != 2 || DrawNumber != 2)
                    {
                        strPrevDrawNo = DrawNumber.ToString() + "th";
                    }
                    else
                    {
                        if (DrawNumber == 2)
                        {
                            strPrevDrawNo = DrawNumber.ToString() + "nd";
                        }
                        else
                        {
                            strPrevDrawNo = DrawNumber.ToString() + "rd";
                        }
                    }



                    //int PreviousDraw = int.Parse(txtcurrdraw.Text )- 1;
                    SetParamValue("@PrevDrawNO", strPrevDrawNo.ToString());
                }
                SetParamValue("@PrevDivident", balayer.ConvertToIndCurrency(txtprevdividend.Text));
                SetParamValue("@PInstallamount", balayer.ConvertToIndCurrency(txtcurrdueamount.Text));
                SetParamValue("@DueDate", txtduedate.Text);
                SetParamValue("@PrizeAmount", balayer.ConvertToIndCurrency(txtprizedamount.Text));

                //PInstallamount

                //string p1 = "This is to inform you that the" + DrawNumber.ToString().Trim() + " draw in " + dtReport.Rows[0]["ChitCategory"].ToString().Trim() + ", Agreement No." + dtReport.Rows[0]["ChitAgreementNo"].ToString().Trim() + " of " + dtReport.Rows[0]["ChitAgreementYear"].ToString().Trim() + " in which you are one of the subscribers, will be held on " + AuctionDate.ToString("dd/MM/yy").Trim() + " at " + dtReport.Rows[0]["AuctionTime"].ToString().Trim() + " at " + dtReport.Rows[0]["B_Address"].ToString().Trim();
                //SetParamValue("@p1", p1);
                //string p2 = "You may kindly make it convenient to be present at the draw in person or by tender or by your duly authorised agent. The details of subscription payable is noted hereunder. " + DrawNumber.ToString().Trim() + " Instalment payable Rs. " + txtInstalmentAmount.Text.Trim() + "." + (DrawNumber - 1).ToString().Trim() + " Draw dividend Rs. " + txtPreviousDivident.Text.Trim();
                //SetParamValue("@p2", p2);



                Session["myRbt"] = cr;
                //CrystalReportViewer1.ReportSource = cr;
                //CrystalReportViewer1.RefreshReport();

                Response.Redirect("Report.aspx");
            }
        }

        protected void btnautofill_click(object sender, EventArgs e)
        {
        }
    }
}
