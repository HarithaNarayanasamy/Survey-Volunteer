using DevExpress.Web.ASPxGridView;
using log4net;
using SVCF_BusinessAccessLayer;
using SVCF_TransactionLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClosedXML.Excel;
using System.Xml;
using Spire.Xls;
using System.IO;

namespace SreeVisalamChitFundLtd_phase1
{
    public partial class CancelledReceipts : System.Web.UI.Page
    {
        private string usrRole;
        //private object balayer;
        #region Object
        BusinessLayer balayer = new BusinessLayer();
        TransactionLayer trn = new TransactionLayer();
        #endregion
        int memid; int bname; string m1;
        ILog logger = log4net.LogManager.GetLogger(typeof(WebForm4));

        int branch;
        int id;

        protected void Page_Load(object sender, EventArgs e)
        {


            if (!IsPostBack)
            {
                id = Convert.ToInt16(Request.QueryString["id"]);
                branch = Convert.ToInt32(Session["Branchid"]);
                try
                {
                    txtFrom.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    txtTo.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    gridBranch1.Visible = false;
                    loadBranch();

                    //ViewState["CurrentData"] = null;    //05/10/2021
                }
                catch (Exception ex) { }
            }
        }

        public void loadBranch()
        {
            if (branch == 161)
            {
                DataTable dtBranch = balayer.GetDataTable("select NodeID,Node from headstree where ParentID=1;");
                DataRow dr = dtBranch.NewRow();
                dr[0] = "0";
                dr[1] = "--Select--";
                ddlBranch.DataValueField = "NodeID";
                ddlBranch.DataTextField = "Node";
                dtBranch.Rows.InsertAt(dr, 0);
                ddlBranch.DataSource = dtBranch;
                ddlBranch.DataBind();
            }
            else
            {
                DataTable dtBranch = balayer.GetDataTable("select NodeID,Node from headstree where NodeID=" + Session["Branchid"]);
                DataRow dr = dtBranch.NewRow();
                dr[0] = "0";
                dr[1] = "--Select--";
                ddlBranch.DataValueField = "NodeID";
                ddlBranch.DataTextField = "Node";
                dtBranch.Rows.InsertAt(dr, 0);
                ddlBranch.DataSource = dtBranch;
                ddlBranch.DataBind();
            }
        }

        public void moneycollector()
        {
            DataTable dtgroupno = balayer.GetDataTable("select moneycollid,moneycollname from svcf.moneycollector mc where mc.employeeid not in(select em.Emp_ID from employee_details em where em.Emp_Designation='Transfered' or em.Emp_Designation='Resigned') and mc.BranchID=" + ddlBranch.SelectedItem.Value);

            DataRow dr = dtgroupno.NewRow();
            dr[0] = "0";
            dr[1] = "--Select--";
            DDLmid.DataValueField = "MoneyCollId";
            DDLmid.DataTextField = "moneycollname";

            dtgroupno.Rows.InsertAt(dr, 0);
            DDLmid.DataSource = dtgroupno;
            DDLmid.DataBind();
        }

        protected void DDLmid_Click(object sender, EventArgs e)
        {
            ddlseries.Items.Clear();

            if (branch == 161 && id == 2)
            {
                ddlseries.Items.Add(new ListItem("--Select--", "--Select--"));
                ddlseries.Items.Add(new ListItem("Customer App", "CPAPP"));
                ddlseries.Items.Add(new ListItem("Website", "CPWEB"));
            }
            else
            {
                ddlseries.Items.Add(new ListItem("--Select--", "--Select--"));
                ddlseries.Items.Add(new ListItem("Money Collector App", "BCAPP"));
            }
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (Session["UserName"] == null || Session["Branchid"] == null || Session["BranchName"] == null)
            {
                Response.Redirect(Page.ResolveUrl("~/Login.aspx"), true);
            }

        }

        protected void ddlseries_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    DataTable dt1 = new DataTable();
            //    dt1 = balayer.GetDataTable("select m1.ChoosenDate,m1.ModifiedDate,m1.AppReceiptno,m1.ChitGroupId,m1.Amount,m1.RejectReason,m1.Head_Id,mg1.Head_Id,mg1.GrpMemberID from svcf.mobileappvoucher as m1 LEFT JOIN svcf.membertogroupmaster as mg1 on m1.Head_Id = mg1.Head_Id where IsAccepted = 2 and Voucher_Type = 'C' and m1.BranchID =" + ddlBranch.SelectedItem.Value + " and Series='" + ddlseries.SelectedItem.Value + "' and MoneyCollId=" + DDLmid.SelectedItem.Value + " and m1.ChoosenDate between '" + balayer.changedateformat(Convert.ToDateTime(txtFrom.Text), 2) + "' and '" + balayer.changedateformat(Convert.ToDateTime(txtTo.Text), 2) + "'");
            //    gridBranch1.Visible = true;
            //    gridBranch1.DataSource = dt1;
            //    gridBranch1.DataBind();
            //}
            //catch (Exception) { }

        }
        public void loadGrid()
        {
            try
            {
                ViewState["CurrentData"] = null;    //05/10/2021
                DataTable dt1 = new DataTable();
                dt1 = balayer.GetDataTable("select m1.ChoosenDate,m1.ModifiedDate,m1.AppReceiptno,m1.ChitGroupId,m1.Amount,m1.RejectReason,m1.Head_Id,mg1.Head_Id,mg1.GrpMemberID from svcf.mobileappvoucher as m1 LEFT JOIN svcf.membertogroupmaster as mg1 on m1.Head_Id = mg1.Head_Id where IsAccepted = 2 and Voucher_Type = 'C' and m1.BranchID =" + ddlBranch.SelectedItem.Value + " and Series='" + ddlseries.SelectedItem.Value + "' and MoneyCollId=" + DDLmid.SelectedItem.Value + " and m1.ChoosenDate between '" + balayer.changedateformat(Convert.ToDateTime(txtFrom.Text), 2) + "' and '" + balayer.changedateformat(Convert.ToDateTime(txtTo.Text), 2) + "'");
                gridBranch1.Visible = true;
                ViewState["CurrentData"] = dt1; //05/10/2021
                gridBranch1.DataSource = dt1;
                gridBranch1.DataBind();
            }
            catch (Exception) { }
        }

        protected void gvpageindex(object sender, GridViewPageEventArgs e)

        {
            gridBranch1.PageIndex = e.NewPageIndex;
            //gridBranch1.DataSource = dt1.GetData();
            gridBranch1.DataBind();
        }

        protected void gridBranch1_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
        {
            ASPxGridView grid = (sender as ASPxGridView);
            if (!grid.IsNewRowEditing)
            {
                grid.DoRowValidation();
            }
        }
        void AddError(Dictionary<GridViewColumn, string> errors, GridViewColumn column, string errorText)
        {
            if (errors.ContainsKey(column)) return;
            errors[column] = errorText;
        }


        protected void gridBranch_CustomUnboundColumnData(object sender, ASPxGridViewColumnDataEventArgs e)
        {
            if (e.Column.FieldName == "Number")
            {
                e.Value = e.ListSourceRowIndex + 1;
            }
        }

        protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            moneycollector();
        }

        protected void BtnStatisticsGo_Click(object sender, EventArgs e)
        {
            loadGrid();
        }

        protected void imgexport_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                DataTable dt = (DataTable)ViewState["CurrentData"];

                Workbook workbook = new Workbook();
                workbook.CreateEmptySheets(1);
                Worksheet sheet = workbook.Worksheets[0];

                ExcelFont fontbold = workbook.CreateFont();
                fontbold.IsBold = true;

                sheet.Name = "CancelledReceipts";
                string branchname = "";
                if (branch != 161)
                {
                    branchname = balayer.GetSingleValue("select Node from svcf.headstree where ParentID=1 and NodeID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]));
                }
                else
                {
                    branchname = balayer.GetSingleValue("select Node from svcf.headstree where ParentID=1 and NodeID=" + ddlBranch.SelectedItem.Value);
                }
                sheet.Range["E1"].Value = "Sree Visalam Chit Fund Ltd.,";
                RichText richText1 = sheet.Range["E1"].RichText;
                richText1.SetFont(0, richText1.Text.Length - 1, fontbold);
                var bb = 2;
                if (branchname == "Triplicane")
                {

                    sheet.Range["E" + bb + ""].Value = "Branch: " + "Mount Road";
                    RichText richText02 = sheet.Range["E" + bb + ""].RichText;
                    richText02.SetFont(0, richText02.Text.Length - 1, fontbold);

                }
                else if (branchname == "Pallathur II")
                {
                    sheet.Range["E" + bb + ""].Value = "Branch: " + "Pallathur";
                    RichText richText02 = sheet.Range["E" + bb + ""].RichText;
                    richText02.SetFont(0, richText02.Text.Length - 1, fontbold);
                }
                else
                {
                    sheet.Range["E" + bb + ""].Value = "Branch: " + branchname;
                    RichText richText02 = sheet.Range["E" + bb + ""].RichText;
                    richText02.SetFont(0, richText02.Text.Length - 1, fontbold);
                }

                sheet.Range["E3"].Value = "Details of Cancelled Receipts";
                RichText richText03 = sheet.Range["E3"].RichText;
                richText03.SetFont(0, richText03.Text.Length - 1, fontbold);

                sheet.Range["E4"].Value = "Bill Collector Name : " + DDLmid.SelectedItem.Text;
                RichText richText04 = sheet.Range["E4"].RichText;
                richText04.SetFont(0, richText04.Text.Length - 1, fontbold);

                sheet.Range["A6"].Value = "SNo.";
                RichText richText06 = sheet.Range["A6"].RichText;
                richText06.SetFont(0, richText06.Text.Length - 1, fontbold);
                sheet.Range["A6"].Style.HorizontalAlignment = HorizontalAlignType.Center;
                sheet.Range["A6"].Style.VerticalAlignment = VerticalAlignType.Center;

                sheet.Range["B6"].Value = "Receipt Date";
                RichText richText07 = sheet.Range["B6"].RichText;
                richText07.SetFont(0, richText07.Text.Length - 1, fontbold);
                sheet.Range["B6"].Style.HorizontalAlignment = HorizontalAlignType.Center;
                sheet.Range["B6"].Style.VerticalAlignment = VerticalAlignType.Center;

                sheet.Range["C6"].Value = "Cancelled Date";
                RichText richText08 = sheet.Range["C6"].RichText;
                richText08.SetFont(0, richText08.Text.Length - 1, fontbold);
                sheet.Range["C6"].Style.HorizontalAlignment = HorizontalAlignType.Center;
                sheet.Range["C6"].Style.VerticalAlignment = VerticalAlignType.Center;

                sheet.Range["D6"].Value = "Receipt No";
                RichText richText09 = sheet.Range["D6"].RichText;
                richText09.SetFont(0, richText09.Text.Length - 1, fontbold);
                sheet.Range["D6"].Style.HorizontalAlignment = HorizontalAlignType.Center;
                sheet.Range["D6"].Style.VerticalAlignment = VerticalAlignType.Center;

                sheet.Range["E6"].Value = "Chit Number";
                RichText richText10 = sheet.Range["E6"].RichText;
                richText10.SetFont(0, richText10.Text.Length - 1, fontbold);
                sheet.Range["E6"].Style.HorizontalAlignment = HorizontalAlignType.Center;
                sheet.Range["E6"].Style.VerticalAlignment = VerticalAlignType.Center;

                sheet.Range["F6"].Value = "Amount";
                RichText richText11 = sheet.Range["F6"].RichText;
                richText11.SetFont(0, richText11.Text.Length - 1, fontbold);
                sheet.Range["F6"].Style.HorizontalAlignment = HorizontalAlignType.Center;
                sheet.Range["F6"].Style.VerticalAlignment = VerticalAlignType.Center;

                sheet.Range["G6"].Value = "Reject Reason";
                RichText richText12 = sheet.Range["G6"].RichText;
                richText12.SetFont(0, richText12.Text.Length - 1, fontbold);
                sheet.Range["G6"].Style.HorizontalAlignment = HorizontalAlignType.Center;
                sheet.Range["G6"].Style.VerticalAlignment = VerticalAlignType.Center;

                int rowcount = 6;
                int sno = 1;
                decimal tot = 0;
                foreach (DataRow row in dt.Rows)
                {
                    rowcount = rowcount + 1;

                    sheet.Range["A" + rowcount].Value = sno.ToString();
                    sheet.Range["B" + rowcount].Value = row.ItemArray[1].ToString();
                    sheet.Range["C" + rowcount].Value = row.ItemArray[2].ToString();
                    sheet.Range["D" + rowcount].Value = row.ItemArray[3].ToString();
                    sheet.Range["E" + rowcount].Value = row.ItemArray[8].ToString();
                    sheet.Range["F" + rowcount].NumberValue = Convert.ToDouble(row.ItemArray[4]);
                    sheet.Range["F" + rowcount].NumberFormat = "#,##0.00";
                    tot += Convert.ToDecimal(row.ItemArray[4]);
                    sheet.Range["G" + rowcount].Value = row.ItemArray[5].ToString();

                    sno++;
                }

                rowcount = rowcount + 1;
                sheet.Range["E" + rowcount].Value = "Total";
                RichText richText13 = sheet.Range["E" + rowcount].RichText;
                richText13.SetFont(0, richText13.Text.Length - 1, fontbold);
                sheet.Range["E" + rowcount].Style.HorizontalAlignment = HorizontalAlignType.Center;
                sheet.Range["E" + rowcount].Style.VerticalAlignment = VerticalAlignType.Center;

                sheet.Range["F" + rowcount].NumberValue = Convert.ToDouble(tot);
                sheet.Range["F" + rowcount].NumberFormat = "#,##0.00";
                //RichText richText14 = sheet.Range["F"+rowcount].RichText;
                //richText14.SetFont(0, richText14.Text.Length - 1, fontbold);
                sheet.Range["F" + rowcount].Style.HorizontalAlignment = HorizontalAlignType.Center;
                sheet.Range["F" + rowcount].Style.VerticalAlignment = VerticalAlignType.Center;

                CellRange range21 = sheet.Range["A6:" + "G" + rowcount];
                range21.Borders.LineStyle = LineStyleType.Thin;
                range21.Borders[BordersLineType.DiagonalDown].LineStyle = LineStyleType.None;
                range21.Borders[BordersLineType.DiagonalUp].LineStyle = LineStyleType.None;

                rowcount = rowcount + 2;

                sheet.AllocatedRange.AutoFitColumns();
                sheet.AllocatedRange.AutoFitRows();

                sheet.SetColumnWidth(1, 6);
                sheet.SetColumnWidth(2, 15);
                sheet.SetColumnWidth(3, 20);
                sheet.SetColumnWidth(4, 20);
                sheet.SetColumnWidth(5, 10);
                sheet.SetColumnWidth(6, 15);
                sheet.SetColumnWidth(7, 25);

                //Response.Clear();
                //Response.Buffer = true;

                ////Response.Headers.Clear();
                //Response.ContentType = "application/vnd.ms-excel";
                //Response.AddHeader("content-disposition", "attachment;filename=GridViewExport.xls");
                //Response.Charset = "";

                //StringWriter sw = new StringWriter();
                //HtmlTextWriter hw = new HtmlTextWriter(sw);

                //gridBranch1.DataSource = dt;
                //gridBranch1.RenderControl(hw);
                //string style = @"<style> .textmode { mso-number-format:\@; } </style>";
                //Response.Write(style);
                //Response.Write(sw.ToString());
                //Response.Flush();
                ////Response.End();
                //Response.Close();
                workbook.SaveToHttpResponse("CancelledReceipts.xlsx", HttpContext.Current.Response, HttpContentType.Excel2010);
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
               server control at run time. */
        }
    }
}