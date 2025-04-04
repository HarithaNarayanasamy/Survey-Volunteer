using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient ;
using System.Data;
using SVCF_BusinessAccessLayer;
using SVCF_DataAccessLayer;
using log4net;
using log4net.Config;

namespace SreeVisalamChitFundLtd_phase1
{
    public partial class Action_form : System.Web.UI.Page
    {
        #region Object
        BusinessLayer balayer = new BusinessLayer();
        #endregion

        ILog logger = log4net.LogManager.GetLogger(typeof(Action_form));

        IntimationLetter cr;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataTable dtcmpGroup = balayer.GetDataTable("Select GROUPNO from groupmaster Where BranchID='" + "44" + "'");
                cmbGroup.DataSource = dtcmpGroup;
                cmbGroup.DataBind();
                //da.Fill(dtcmpGroup);
                //for (int i = 0; i < dtcmpGroup.Rows.Count; i++)
                //{
                //    cmbGroup.Items.Add(dtcmpGroup.Rows[i][0].ToString());
                //}
            }
        }
        //IntimationLetter cr;
        private void SetParamValue(string paramName, string paramValue)
        {
            for (int i = 0; i < cr.DataDefinition.FormulaFields.Count; i++)

                if (cr.DataDefinition.FormulaFields[i].FormulaName == "{" + paramName + "}")

                    cr.DataDefinition.FormulaFields[i].Text = "\"" + paramValue + "\"";
        }
        protected void btnGenerate_Click(object sender, EventArgs e)
        {
            if (cmbGroup.SelectedIndex != -1)
            {
                string strquery = "SELECT t2.B_Name,t2.B_Address,t1.DueDate,t1.ChitAgreementNo,t1.ChitAgreementYear,t1.ChitPeriod,t1.ChitCategory,t1.NoofMembers,t1.AuctionDate,TIME_FORMAT(t1.AuctionTime,'%r') as AuctionTime  FROM  `groupmaster` as t1 inner join branchdetails as t2 on t1.BranchID=t2.B_CODE where t1.GROUPNO='" + cmbGroup.SelectedItem.ToString() + "'";

                // MySqlConnection con = SreeVisalamChitFundLtd_phase1.CommonClassFile.openConnection();

                //MySqlDataAdapter da = new MySqlDataAdapter(strquery, con);
                DataTable dtReport = new DataTable();
                // da.Fill(dtReport);

                dtReport = balayer.GetDataTable(strquery);

                //da.Dispose();
                //dtReport.Dispose();
                //con.Close();
                // con.Dispose();

                cr = new IntimationLetter();
                //string reportPath = @"C:\Documents and Settings\Administrator\Desktop\Sree Visalam Chit Fund  Ltd\Sree Visalam Chit Fund  Ltd\GroupIntimationLetter.rpt";
                string reportPath = Server.MapPath("GroupIntimationLetter.rpt");
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

                SetParamValue("@ChitNumber", cmbGroup.SelectedItem.ToString());
                SetParamValue("@BranchName", dtReport.Rows[0]["B_Name"].ToString());
                SetParamValue("@NumberofDraw", dtReport.Rows[0]["ChitPeriod"].ToString());
                SetParamValue("@BranchAddress", "  " + dtReport.Rows[0]["B_Address"].ToString() + "  ");
                SetParamValue("@AgrementNumber", dtReport.Rows[0]["ChitAgreementNo"].ToString());
                SetParamValue("@Agrementyear", dtReport.Rows[0]["ChitAgreementYear"].ToString());
                SetParamValue("@Today", DateTime.Now.ToString("dd/MM/yy"));
                SetParamValue("@ChitCategory", dtReport.Rows[0]["ChitCategory"].ToString());

                //due to iis issue
                //System.Globalization.DateTimeFormatInfo usDtfi = new System.Globalization.CultureInfo("en-NZ", false).DateTimeFormat;

                // DateTime TempDate = Convert.ToDateTime(dtReport.Rows[0]["AuctionDate"].ToString(), usDtfi);

                // DateTime TempDate = DateTime.Parse(Convert.ToDateTime(dtReport.Rows[0]["AuctionDate"], usDtfi).ToString("mm/dd/yy"));
                //

                DateTime TempDate = DateTime.Parse(dtReport.Rows[0]["AuctionDate"].ToString());
                DateTime AuctionDate = new DateTime( DateTime.Now.Year, TempDate.Month, TempDate.Day);
                DateTime dueDate = DateTime.Parse(dtReport.Rows[0]["DueDate"].ToString());
                // DateTime AuctionDate = dueDate.AddDays(1);
                // string strDate = dueDate.ToString("yy/MM/dd");
                Double Drawo = (DateTime.Today - TempDate).TotalDays;


                int DrawNumber = int.Parse(Math.Ceiling(Drawo / 30.0).ToString()) + 1;
                string  strDrawNo = "";
                string strPrevDrawNo = "";

                if (DrawNumber != 2 || DrawNumber != 2)
                {
                    strDrawNo = DrawNumber.ToString() + "th";
                }
                else
                {
                    if (DrawNumber == 2)
                    {
                        strDrawNo = DrawNumber.ToString() + "nd";
                    }
                    else
                    {
                        strDrawNo = DrawNumber.ToString() + "rd";
                    }
                }

                DrawNumber = DrawNumber - 1;
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
                //DateTime dtAuctionTime = DateTime.ParseExact(dtReport.Rows[0]["AuctionTime"].ToString(),"HHmmss", null);
                string convertedAuctionTime = dtReport.Rows[0]["AuctionTime"].ToString();
                SetParamValue("@CurrentDrawNo", strDrawNo);
                SetParamValue("@AuctionDate", AuctionDate.ToString("dd/MM/yy"));
                SetParamValue("@AuctionTime", convertedAuctionTime);
                SetParamValue("@PInstallamount", balayer.ConvertToIndCurrency( txtInstalmentAmount.Text ));
                SetParamValue("@PrevDrawNO", strPrevDrawNo);
                SetParamValue("@PrevDivident", balayer.ConvertToIndCurrency(txtPreviousDivident.Text ));
                SetParamValue("@DueDate", dueDate.ToString("dd/MM/yy"));
                SetParamValue("@PrizeAmount", balayer.ConvertToIndCurrency(txt_price.Text));
                //string p1 = "This is to inform you that the" + DrawNumber.ToString().Trim() + " draw in " + dtReport.Rows[0]["ChitCategory"].ToString().Trim() + ", Agreement No." + dtReport.Rows[0]["ChitAgreementNo"].ToString().Trim() + " of " + dtReport.Rows[0]["ChitAgreementYear"].ToString().Trim() + " in which you are one of the subscribers, will be held on " + AuctionDate.ToString("dd/MM/yy").Trim() + " at " + dtReport.Rows[0]["AuctionTime"].ToString().Trim() + " at " + dtReport.Rows[0]["B_Address"].ToString().Trim();
                //SetParamValue("@p1", p1);
                //string p2 = "You may kindly make it convenient to be present at the draw in person or by tender or by your duly authorised agent. The details of subscription payable is noted hereunder. " + DrawNumber.ToString().Trim() + " Instalment payable Rs. " + txtInstalmentAmount.Text.Trim() + "." + (DrawNumber - 1).ToString().Trim() + " Draw dividend Rs. " + txtPreviousDivident.Text.Trim();
                //SetParamValue("@p2", p2);


                Session["myRbt"] = cr;
                //CrystalReportViewer1.ReportSource = cr;
                //CrystalReportViewer1.RefreshReport();

                Response.Redirect("Report.aspx", false);
            }
        }

        protected void cmbGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            txt_price.Text = string.Empty;
            txtInstalmentAmount.Text = string.Empty;
            txtPreviousDivident.Text = string.Empty;
        }
    }
}
