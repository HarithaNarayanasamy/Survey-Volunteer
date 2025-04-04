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
using SVCF_DataAccessLayer;
using SVCF_TransactionLayer;
namespace SreeVisalamChitFundLtd_phase1
{

    public partial class AddressLable : System.Web.UI.Page
    {
        #region Object
        BusinessLayer balayer = new BusinessLayer();
        TransactionLayer trn = new TransactionLayer();
        #endregion
        protected void BindCustomerDetails()
        {
            DataTable dt = balayer.GetDataTable("SELECT SlNo,GrpMemberID,MemberName,MemberAddress,NoCard From membertogroupmaster where GroupID='" + ddlChitNo.SelectedItem.ToString() + "'");


            if (dt.Rows.Count > 0)
            {
                gvDetails.DataSource = dt;
                gvDetails.DataBind();
                foreach (GridViewRow gvRow in gvDetails.Rows)
                {
                    if (((Label)gvRow.FindControl("lblNoCard")).Text == "N")
                    {
                        CheckBox chkSel =
                        (CheckBox)gvRow.FindControl("chkSelect");
                        chkSel.Checked = true;
                        Label txtname = (Label)gvRow.FindControl("lblName");
                        Label txtlocation = (Label)gvRow.FindControl("lblAddressForCommunication");

                        txtname.ForeColor = System.Drawing.Color.Black;
                        txtlocation.ForeColor = System.Drawing.Color.Black;
                    }
                }
            }
            else
            {
                dt.Rows.Add();
                gvDetails.DataSource = dt;
                gvDetails.DataBind();
                int columncount = gvDetails.Rows[0].Cells.Count;
                gvDetails.Rows[0].Cells.Clear();
                gvDetails.Rows[0].Cells.Add(new TableCell());
                gvDetails.Rows[0].Cells[0].ColumnSpan = columncount;
                gvDetails.Rows[0].Cells[0].Text = "No Records Found";
            }
        }

        protected void btnLoad_OnClick
        (object sender, EventArgs e)
        {
            BindCustomerDetails();
        }
        protected void chkSelectAll_CheckedChanged
        (object sender, EventArgs e)
        {
            CheckBox chkAll =
            (CheckBox)gvDetails.HeaderRow.FindControl("chkSelectAll");
            if (chkAll.Checked == true)
            {
                foreach (GridViewRow gvRow in gvDetails.Rows)
                {
                    CheckBox chkSel =
                    (CheckBox)gvRow.FindControl("chkSelect");
                    chkSel.Checked = true;
                    Label txtname = (Label)gvRow.FindControl("lblName");
                    Label txtlocation = (Label)gvRow.FindControl("lblAddressForCommunication");
                    txtname.ForeColor = System.Drawing.Color.Black;
                    txtlocation.ForeColor = System.Drawing.Color.Black;
                }
            }
            else
            {
                foreach (GridViewRow gvRow in gvDetails.Rows)
                {
                    CheckBox chkSel = (CheckBox)gvRow.FindControl("chkSelect");
                    chkSel.Checked = false;
                    Label txtname = (Label)gvRow.FindControl("lblName");
                    Label txtlocation = (Label)gvRow.FindControl("lblAddressForCommunication");
                    txtname.ForeColor = System.Drawing.Color.Blue;
                    txtlocation.ForeColor = System.Drawing.Color.Blue;
                }
            }
        }



        protected void ddlChitNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            gvDetails.DataSource = null;
            gvDetails.DataBind();
        }
        //ddlChitNo_SelectedIndexChanged
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataTable ChitGrp = new DataTable();
                try
                {
                    //MySqlDataAdapter mem2GrpAdp = new MySqlDataAdapter("select GROUPNO,NoofMembers from groupmaster", con);

                    ChitGrp = balayer.GetDataTable("select GROUPNO from groupmaster  where BranchID='" +balayer.ToobjectstrEvenNull(Session["Branchid"]) + "'");
                    //mem2GrpAdp.Fill(ChitGrp);
                    DataRow dr = ChitGrp.NewRow();
                    dr[0] = "--select--";
                    ChitGrp.Rows.InsertAt(dr, 0);
                    ddlChitNo.DataSource = ChitGrp;
                    ddlChitNo.DataTextField = "GROUPNO";
                    ddlChitNo.DataValueField = "GROUPNO";
                    ddlChitNo.DataBind();
                }
                catch (Exception ex)
                {
                    // Handle the error
                }
            }
        }

        protected void btnGenerate_Click(object sender, EventArgs e)
        {
            AddressLabel cr = new AddressLabel();
            //string reportPath = @"C:\Documents and Settings\Administrator\Desktop\Sree Visalam Chit Fund  Ltd\Sree Visalam Chit Fund  Ltd\GroupIntimationLetter.rpt";
            string reportPath = Server.MapPath("AddressLabel.rpt");
            cr.Load(reportPath);
            Session["myRbt"] = cr;
            string strValuelist = "";
            for (int iRow = 0; iRow < gvDetails.Rows.Count; iRow++)
            {
                CheckBox chkSel = (CheckBox)gvDetails.Rows[iRow].FindControl("chkSelect");
                if (chkSel.Checked == true)
                {
                    strValuelist += "\"" + gvDetails.DataKeys[iRow].Value.ToString() + "\", ";
                }
            }
            strValuelist = strValuelist.Trim().Trim(',');
            string formula = "{membertogroupmaster.GrpMemberID} in [" + strValuelist + "]";
            Session["myFilter"] = formula;
            Response.Redirect("Report.aspx", false);
        }
    }
}

