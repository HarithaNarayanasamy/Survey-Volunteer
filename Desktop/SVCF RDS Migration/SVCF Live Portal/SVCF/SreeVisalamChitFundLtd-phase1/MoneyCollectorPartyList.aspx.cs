using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SVCF_BusinessAccessLayer;
using SVCF_DataAccessLayer;
using SVCF_TransactionLayer;
using System.Data;
using System.IO;
using DevExpress.Data;
using DevExpress.Web.ASPxGridView.Export;
using ClosedXML.Excel;

namespace SreeVisalamChitFundLtd_phase1
{
    public partial class MoneyCollectorPartyList : System.Web.UI.Page
    {
        #region Object
        BusinessLayer balayer = new BusinessLayer();
        TransactionLayer trn = new TransactionLayer();
        CommonVariables objCOM = new CommonVariables();
        Dictionary<string, string> Tempdic = new Dictionary<string, string>();
        string qry = "";
        string date = DateTime.Now.ToString("dd\\/MM\\/yyyy");
        ASPxGridViewExporter gridViewExporter = new ASPxGridViewExporter();
        #endregion


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BranchNames();
            }
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (Session["UserName"] == null || Session["Branchid"] == null || Session["BranchName"] == null)
            {
                Response.Redirect(Page.ResolveUrl("~/Login.aspx"), true);
            }
        }
        public void BranchNames()
        {
            DataTable dtbranch = balayer.GetDataTable("select NodeID,Node from svcf.headstree where ParentID=1");
            DataRow dr = dtbranch.NewRow();
            ddlBranch.DataTextField = "Node";
            ddlBranch.DataValueField = "NodeID";
            dtbranch.Rows.InsertAt(dr, 0);
            ddlBranch.DataSource = dtbranch;
            ddlBranch.DataBind();

        }
        protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
        {

            CollectorName();
        }
        public void CollectorName()
        {
            //qry= "select moneycollid,moneycollname from moneycollector where BranchID='" + balayer.ToobjectstrEvenNull(Session["Branchid"])+"'";
            qry = "select moneycollid,moneycollname from moneycollector where BranchID=" + ddlBranch.SelectedItem.Value;
            Tempdic.Clear();
            Tempdic = balayer.CmnList(qry);
            ddlCollector.DataValueField = "Key";
            ddlCollector.DataTextField = "Value";
            ddlCollector.DataSource = Tempdic;
            ddlCollector.DataBind();
        }
        public void LoadGrid()
        {
            try
            {
                ViewState["CurrentData"] = null;
                DataTable dtbind = new DataTable();
                //dtbind.Columns.Add("S_No");
                dtbind.Columns.Add("Chit No");
                dtbind.Columns.Add("Name Of the Subscriber");
                dtbind.Columns.Add("Current Installment No");
                dtbind.Columns.Add("IsPrized");
                dtbind.Columns.Add("Mobile No");
                dtbind.Columns.Add("Address");
                DataRow dr = dtbind.NewRow();
                DataTable chitdt = new DataTable();
                DataTable insdt = new DataTable();
                string grpId = "";
                string drawNo = "";
                string noofMembers = "";
                string MobileNumber = "";
                string NP = "";
                string P = "";
                string strMember = "";
                string address = "";
                var query1 = "";
                query1 = "select m.Head_Id,g.GROUPNO,m.GrpMemberID,m.BranchId,m.GroupID,m.MemberName from membertogroupmaster as m join groupmaster as g on (m.GroupID=g.Head_Id) where M_Id='" + ddlCollector.SelectedValue + "' and ChitStartDate<='" + balayer.indiandateToMysqlDate(date) + "' order by GroupID;";
                chitdt = balayer.GetDataTable(query1);
                for (int row = 0; row < chitdt.Rows.Count; row++)
                {
                    //dr["S_No"] = row + 1;
                    dr["Chit No"] = chitdt.Rows[row]["GrpMemberID"].ToString();
                    dr["Name of the Subscriber"] = chitdt.Rows[row]["MemberName"];
                    grpId = chitdt.Rows[row]["GroupID"].ToString();
                    strMember = chitdt.Rows[row]["Head_Id"].ToString();
                    //qry = "";
                    //qry = "select max(DrawNo) as DrawNo,NoofMembers from auctiondetails as a join groupmaster as g on a.GroupID=g.Head_Id where GroupID="+grpId+" and a.AuctionDate<=" + balayer.indiandateToMysqlDate(date) + ";";
                    //insdt = balayer.GetDataTable(qry);
                    //drawNo = Convert.ToInt32(insdt.Rows[0]["DrawNo"]);
                    //noofMembers = Convert.ToInt32(insdt.Rows[0]["NoofMembers"]);
                    drawNo = balayer.GetSingleValue("select max(DrawNo) as DrawNo from auctiondetails where GroupID='" + grpId + "' and AuctionDate<='" + balayer.indiandateToMysqlDate(date) + "';");
                    if (!string.IsNullOrEmpty(drawNo))
                    {
                        noofMembers = balayer.GetSingleValue("select NoofMembers from groupmaster where Head_Id='" + grpId + "' and PSOOrderDate<='" + balayer.indiandateToMysqlDate(date) + "';");
                        if (Convert.ToInt32(noofMembers) == Convert.ToInt32(drawNo))
                        {
                            drawNo = "T";
                        }
                    }
                    if (drawNo == "")
                    {
                        dr["Current Installment No"] = "0";
                    }
                    else
                    {
                        dr["Current Installment No"] = drawNo;
                    }
                    /*Checking for Arrear amounts */

                    qry = "";
                    decimal arrearamt = 0;
                    decimal amnt = 0;
                    try
                    {
                        string TotaldueAmount = balayer.GetSingleValue(@"select sum(CurrentDueAmount) from auctiondetails where GroupID=" + chitdt.Rows[row]["GroupID"].ToString() + " and AuctionDate<='" + balayer.indiandateToMysqlDate(date) + "'");
                        qry = "select  (case when( (" + TotaldueAmount + "-sum(case when (v1.Voucher_Type='C' and " +
                            "v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) +sum(case when (v1.Voucher_Type='D' and v1.trans_Type<>2 and " +
                            "v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) )>0.00) then (" + TotaldueAmount + "-sum(case when (v1.Voucher_Type='C' and " +
                            "v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) +sum(case when (v1.Voucher_Type='D' and v1.trans_Type<>2 and " +
                            "v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) ) else 0.00 end) as 'Arrear Amount' from membertogroupmaster as mg1 join " +
                            "voucher as v1 on v1.Head_Id=mg1.Head_Id left join trans_payment as tp1 on " +
                            "v1.Head_Id =tp1.TokenNumber join membermaster as mm on (mg1.MemberID=mm.MemberIDNew) where mg1.BranchID=" + chitdt.Rows[row]["BranchId"].ToString() + " and mg1.GroupID=" + chitdt.Rows[row]["GroupID"].ToString() + " and " +
                            "v1.Head_Id=" + chitdt.Rows[row]["Head_Id"].ToString() + " and v1.ChoosenDate<='" + balayer.indiandateToMysqlDate(date) + "' group by v1.Head_Id order by cast(digits(mg1.GrpMemberID) as unsigned),v1.Voucher_Type DESC";
                        var arrear = Convert.ToString(balayer.GetSingleValue(qry));
                        if (TotaldueAmount != "" && arrear == "")
                        {
                            amnt = Convert.ToDecimal(TotaldueAmount);
                        }
                        else
                        {
                            if (arrear == "")
                                continue;
                        }
                        arrearamt = Convert.ToDecimal(balayer.GetScalarDecimal(qry));
                        if (arrear != "")
                        {
                            if (arrearamt <= 0 && drawNo=="T")
                                continue;
                            amnt = arrearamt;
                        }
                        arrearamt = 0;
                    }
                    catch (Exception ex)
                    {
                        Response.Write(ex.Message);
                    }
                    NP = balayer.GetSingleValue("select  Replace(Replace(Replace (IsPrized,'Y','P'),'I','NP'),'N','NP') as IsPrized,GroupID from svcf.auctiondetails where PrizedMemberID='" + chitdt.Rows[row]["Head_Id"].ToString() + "' and GroupID='" + chitdt.Rows[row]["GroupID"].ToString() + "'  and AuctionDate<='" + balayer.indiandateToMysqlDate(date) + "'  group by GroupID");
                    if (string.IsNullOrEmpty(NP))
                    {
                        dr["IsPrized"] = "NP";
                    }
                    else
                    {
                        dr["IsPrized"] = "P";
                    }
                    MobileNumber = balayer.GetSingleValue("select m1.MobileNo from svcf.membermaster as m1 join svcf.branchdetails as b1 on (m1.BranchID=b1.Head_Id) join svcf.membertogroupmaster as mg1 on (mg1.MemberID=m1.MemberIDNew) where mg1.Head_Id='" + strMember + "';");
                    if (string.IsNullOrEmpty(MobileNumber))
                    {
                        string strMemberID = balayer.GetSingleValue("select MemberID from svcf.membertogroupmaster where Head_Id='" + strMember + "'");
                        dr["Mobile No"] = balayer.GetSingleValue("select MobileNo from svcf.membermaster where MemberIDNew='" + strMemberID + "'");
                    }
                    else
                    {
                        dr["Mobile No"] = MobileNumber;
                    }
                    address = balayer.GetSingleValue("select m1.AddressForCommunication from svcf.membermaster as m1 join svcf.branchdetails as b1 on (m1.BranchID=b1.Head_Id) join svcf.membertogroupmaster as mg1 on (mg1.MemberID=m1.MemberIDNew) where mg1.Head_Id='" + strMember + "';");
                    if (string.IsNullOrEmpty(address))
                    {
                        string strMemberID = balayer.GetSingleValue("select MemberID from svcf.membertogroupmaster where Head_Id='" + strMember + "'");
                        dr["Address"] = balayer.GetSingleValue("select AddressForCommunication from svcf.membermaster where MemberIDNew='" + strMemberID + "'");
                    }
                    else
                    {
                        dr["Address"] = address;
                    }
                    dtbind.Rows.Add(dr.ItemArray);
                }
                string query = "select Node from svcf.headstree where ParentID=1 and NodeID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + "";
                string branchName = balayer.GetSingleValue(query);
                lblCaption.Text = "Sree Visalam Chit Fund Limited.,<br/>Trial And Arrear <br/> Branch Name : " + branchName + "; \t Bill Collector Name : " + ddlCollector.SelectedItem.Text;
                ViewState["CurrentData"] = dtbind;
                gv_BCPartyList.DataSource = dtbind;
                gv_BCPartyList.DataBind();
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }
        protected void btnOk_Click(object sender, EventArgs e)
        {
            LoadGrid();
        }

        protected void btnExportExcel_Click(object sender, EventArgs e)
        {

            try
            {

                Response.Clear();
                Response.Buffer = true;

                Response.AddHeader("content-disposition",
                "attachment;filename=GridViewExport.xls");
                Response.Charset = "";
                Response.ContentType = "application/vnd.ms-excel";
                StringWriter sw = new StringWriter();
                HtmlTextWriter hw = new HtmlTextWriter(sw);
                gv_BCPartyList.AllowPaging = false;
                DataTable expdt = new DataTable();
                expdt = (DataTable)ViewState["CurrentData"];
                gv_BCPartyList.DataSource = expdt;
                gv_BCPartyList.DataBind();
                expdt.Dispose();
                gv_BCPartyList.HeaderRow.Style.Add("background-color", "#FFFFFF");
                gv_BCPartyList.HeaderRow.Cells[0].Style.Add("background-color", "green");
                gv_BCPartyList.HeaderRow.Cells[1].Style.Add("background-color", "green");
                gv_BCPartyList.HeaderRow.Cells[2].Style.Add("background-color", "green");
                gv_BCPartyList.HeaderRow.Cells[3].Style.Add("background-color", "green");
                gv_BCPartyList.HeaderRow.Cells[4].Style.Add("background-color", "green");
                gv_BCPartyList.HeaderRow.Cells[5].Style.Add("background-color", "green");

                for (int i = 0; i < gv_BCPartyList.Rows.Count; i++)
                {
                    GridViewRow row = gv_BCPartyList.Rows[i];

                    //Change Color back to white
                    row.BackColor = System.Drawing.Color.White;

                    //Apply text style to each Row
                    row.Attributes.Add("class", "textmode");

                    //Apply style to Individual Cells of Alternating Row
                    if (i % 2 != 0)
                    {
                        row.Cells[0].Style.Add("background-color", "#C2D69B");
                        row.Cells[1].Style.Add("background-color", "#C2D69B");
                        row.Cells[2].Style.Add("background-color", "#C2D69B");
                        row.Cells[3].Style.Add("background-color", "#C2D69B");
                        row.Cells[4].Style.Add("background-color", "#C2D69B");
                        row.Cells[5].Style.Add("background-color", "#C2D69B");
                    }
                }
                gv_BCPartyList.RenderControl(hw);
                string style = @"<style> .textmode { mso-number-format:\@; } </style>";
                Response.Write(style);
                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
            // required to avoid the run time error "  
            //Control 'GridView1' of type 'Grid View' must be placed inside a form tag with runat=server."  
        }
    }
}