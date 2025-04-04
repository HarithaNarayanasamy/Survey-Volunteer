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
using MySql.Data.MySqlClient;
using SVCF_BusinessAccessLayer;
using SVCF_DataAccessLayer;
using SVCF_TransactionLayer;
namespace SreeVisalamChitFundLtd_phase1
{
    public partial class FirstGroupIntimation : System.Web.UI.Page
    {
        #region Object
        BusinessLayer balayer = new BusinessLayer();
        TransactionLayer trn = new TransactionLayer();
        #endregion
        //Visalam_Chit.CommonClassFile obj = new Visalam_Chit.CommonClassFile();
        FirstGroupIntimationLetter cr;
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
                MySqlConnection con = balayer.OpenConnection();
                //SELECT column_name(s)FROM table_name1 INNER JOIN table_name2 ON table_name1.column_name=table_name2.column_name
                MySqlDataAdapter da = new MySqlDataAdapter("Select GROUPNO from groupmaster where BranchID='" +balayer.ToobjectstrEvenNull(Session["Branchid"]) + "'", con);
                DataTable  dtcmpGroup = new DataTable();
                da.Fill(dtcmpGroup);
                for (int i = 0; i < dtcmpGroup.Rows.Count; i++)
                {
                    cmbGroup.Items.Add(dtcmpGroup.Rows[i][0].ToString());
                }
                da.Dispose();
                dtcmpGroup.Dispose();
                con.Close();
                con.Dispose();
            }
        }

        protected void btnGenerate_Click(object sender, EventArgs e)
        {
            if (cmbGroup.SelectedIndex != -1)
            {
                string strquery = "SELECT t2.B_Name,t2.B_Address,t1.ChitAgreementNo,t1.ChitAgreementYear,t1.ChitPeriod,t1.ChitCategory,t1.NoofMembers,t1.AuctionDate,TIME_FORMAT(t1.AuctionTime,'%r') as AuctionTime  FROM  `groupmaster` as t1 inner join branchdetails as t2 on t1.BranchID=t2.B_CODE where t1.GROUPNO='" + cmbGroup.SelectedItem.ToString() + "'";

                MySqlConnection con = balayer.OpenConnection(); 

                MySqlDataAdapter da = new MySqlDataAdapter(strquery, con);
                DataTable dtReport = new DataTable();
                da.Fill(dtReport);



                da.Dispose();
                //dtReport.Dispose();
                con.Close();
                con.Dispose();

                DataSet1 ds = new DataSet1();
                DataTable dtRpt = ds.Tables["dtRpt"];
                int  numberofDraw = int.Parse(dtReport.Rows[0]["ChitPeriod"].ToString());
                double x = numberofDraw % 2;
                double y = numberofDraw / 2;
                int NumberofRows = int.Parse(Math.Ceiling(y).ToString());


                //System.Globalization.DateTimeFormatInfo usDtfi = new System.Globalization.CultureInfo("en-NZ", false).DateTimeFormat;

                //string selDate = Convert.ToDateTime(dtReport.Rows[0]["AuctionDate"].ToString(), usDtfi).ToString("yy/MM/dd");
                DateTime dFirstAuctionDate = Convert.ToDateTime(dtReport.Rows[0]["AuctionDate"].ToString());
                dFirstAuctionDate = dFirstAuctionDate.AddMonths(-1);
                int sn = 1;
                for (int nr = 0; nr < NumberofRows; nr++)
                {
                    if (nr != NumberofRows - 1)
                    {
                        sn += 1;
                        dFirstAuctionDate = dFirstAuctionDate.AddMonths(1);
                        dtRpt.Rows.Add();
                        dtRpt.Rows[nr][0] = sn;
                        dtRpt.Rows[nr][1] = dFirstAuctionDate.ToString("dd/MM/yy");
                        dtRpt.Rows[nr][2] = dtReport.Rows[0]["AuctionTime"].ToString();
                        dFirstAuctionDate = dFirstAuctionDate.AddMonths(1);
                        sn += 1;
                        dtRpt.Rows[nr][3] = sn ;
                        dtRpt.Rows[nr][4] = dFirstAuctionDate.ToString("dd/MM/yy");
                        dtRpt.Rows[nr][5] = dtReport.Rows[0]["AuctionTime"].ToString();
                    }
                    else
                    {
                        dtRpt.Rows.Add();
                        if (x == 1)
                        {
                            //    sn += 1;
                            //    dFirstAuctionDate = dFirstAuctionDate.AddMonths(1);
                            //    dtRpt.Rows[nr][0] = sn ;
                            //    dtRpt.Rows[nr][1] = dFirstAuctionDate.ToString("dd/MM/yy");
                            //    dtRpt.Rows[nr][2] = dtReport.Rows[0]["AuctionTime"].ToString(); ;
                        }
                        else
                        {
                            sn += 1;
                            dFirstAuctionDate = dFirstAuctionDate.AddMonths(1);
                            dtRpt.Rows[nr][0] = sn;
                            dtRpt.Rows[nr][1] = dFirstAuctionDate.ToString("dd/MM/yy");
                            dtRpt.Rows[nr][2] = dtReport.Rows[0]["AuctionTime"].ToString();

                            //dFirstAuctionDate = dFirstAuctionDate.AddMonths(1);
                            //sn += 1;
                            //dtRpt.Rows[nr][3] = sn;
                            //dtRpt.Rows[nr][4] = dFirstAuctionDate.ToString("dd/MM/yy");
                            //dtRpt.Rows[nr][5] = dtReport.Rows[0]["AuctionTime"].ToString(); ;

                        }
                    }
                }



                cr = new FirstGroupIntimationLetter();
                //string reportPath = @"C:\Documents and Settings\Administrator\Desktop\Sree Visalam Chit Fund  Ltd\Sree Visalam Chit Fund  Ltd\GroupIntimationLetter.rpt";
                string reportPath = Server.MapPath("GroupIntimationLetter.rpt");
                cr.Load(reportPath);
                //cr.SetParameterValue(@"ChitNumber", "181/13");
                cr.SetDataSource(ds);
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
                SetParamValue("@Branch Address", "  " + dtReport.Rows[0]["B_Address"].ToString() + "  ");
                SetParamValue("@AgrementNumber", dtReport.Rows[0]["ChitAgreementNo"].ToString());
                SetParamValue("@Agrementyear", dtReport.Rows[0]["ChitAgreementYear"].ToString());
                SetParamValue("@Today", DateTime.Now.ToString("dd/MM/yy"));
                SetParamValue("@ChitCategory", dtReport.Rows[0]["ChitCategory"].ToString());
                Session["myRbt"] = cr;
                //CrystalReportViewer1.ReportSource = cr;
                //CrystalReportViewer1.RefreshReport();

                Response.Redirect("Report.aspx");
            }
        }
    }
}
