using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SVCF_BusinessAccessLayer;
using SVCF_TransactionLayer;
using SVCF_DataAccessLayer;
using System.Data;
using System.IO;
using System.Text;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html;
using iTextSharp.text.html.simpleparser;
using System.Web.UI.HtmlControls;
using System.Globalization;
using System.Drawing.Printing;
using System.Drawing;

namespace SreeVisalamChitFundLtd_phase1
{
    public partial class Dailycollection : System.Web.UI.Page
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
                var userinfo = HttpContext.Current.User.Identity.Name;
                var qry = "select rs.name from roles as rs inner join rights as rt on (rt.roleid=rs.id) where memberid=" + userinfo + "";
                var usrRole = balayer.GetSingleValue(qry);
                if (usrRole == "Report")
                {
                    Response.Redirect("Home.aspx", false);
                }

                txtdate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                loadcollector();
            }
            }
        public void loadcollector()
        {
            DataTable dt = new DataTable();
            dt = balayer.GetDataTable("SELECT moneycollname,moneycollid FROM svcf.moneycollector where BranchID=" + Session["Branchid"]);
            ddlmc.DataValueField = "moneycollid";
            ddlmc.DataTextField = "moneycollname";
            ddlmc.DataSource = dt;
            ddlmc.DataBind();
        }
        public void loadgrid()
        {

            DataTable Mccoll= new DataTable();
            DataTable dtBind = new DataTable();
            dtBind.Columns.Add("slno", typeof(int));
            dtBind.Columns.Add("GrpMemberID");
            dtBind.Columns.Add("MemberName");
            dtBind.Columns.Add("Amount", typeof(double));
            dtBind.Columns.Add("Date");
            dtBind.Columns.Add("Collected");
            dtBind.Columns.Add("DrawNO");
            dtBind.Columns.Add("IsPrized");
            dtBind.Columns.Add("AmountCollected");
            dtBind.Columns.Add("Collectorname");

            DataRow dr = dtBind.NewRow();
            IFormatProvider culture2 = new System.Globalization.CultureInfo("fr-FR", true);
            DateTime indt = DateTime.Parse(txtdate.Text, culture2, System.Globalization.DateTimeStyles.AssumeLocal);

            Mccoll = balayer.GetDataTable("select m1.MemberName,m1.GrpMemberID,v1.Amount,(select moneycollname from moneycollector where moneycollid='" + ddlmc.SelectedItem.Value + "') as Collectorname,date_format(v1.CurrDate,'%d-%m-%Y') as Date from voucher as v1 join membertogroupmaster as m1 on (m1.Head_Id=v1.Head_Id) where m1.M_Id='" + ddlmc.SelectedItem.Value + "' and v1.Voucher_Type='C' and cast(v1.CurrDate as date)='" + balayer.indiandateToMysqlDate(txtdate.Text) + "' order by v1.CurrDate desc");


          int rcnt = 0;
          if (Mccoll.Rows.Count > 0)
          {

              for (int row = 0; row <= Mccoll.Rows.Count - 1; row++)
              {
                  try
                  {
                      rcnt = rcnt + 1;
                      dr["slno"] = rcnt;
                      dr["MemberName"] = Mccoll.Rows[row]["MemberName"].ToString();
                      dr["GrpMemberID"] = Mccoll.Rows[row]["GrpMemberID"].ToString();
                      dr["Amount"] = Mccoll.Rows[row]["Amount"].ToString();
                      dr["Collectorname"] = Mccoll.Rows[row]["Collectorname"].ToString();
                      dr["Date"] = Mccoll.Rows[row]["Date"];
                  }
                  catch (Exception ex)
                  {
                  }
                  dtBind.Rows.Add(dr.ItemArray);
              }
          }
         // dtBind.AcceptChanges();
          GV_MC.DataSource = dtBind;
          GV_MC.DataBind();
        }

        protected void btngo_Click(object sender, EventArgs e)
        {

            loadgrid();
        }       
    }
}