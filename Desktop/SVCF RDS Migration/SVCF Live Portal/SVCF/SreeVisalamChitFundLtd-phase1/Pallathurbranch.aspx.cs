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
using DevExpress.Web.ASPxMenu;
using DevExpress.Data;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrintingLinks;
using System.IO;
using System.Drawing;
using SVCF_BusinessAccessLayer;
using SVCF_TransactionLayer;
using SVCF_DataAccessLayer;
using MySql.Data.MySqlClient;
namespace SreeVisalamChitFundLtd_phase1
{
    public partial class Pallathurbranch : System.Web.UI.Page
    {
        #region VarDeclaration
        BusinessLayer balayer = new BusinessLayer();
        TransactionLayer tranlayer = new TransactionLayer();
        DataTable BindPrev_Balance = null;
        #endregion
        private System.Drawing.Image headerImage;
        VarDeclaration objVar = new VarDeclaration();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                String BrId = balayer.ToobjectstrEvenNull(balayer.ToobjectstrEvenNull(Session["Branchid"]));
                DataTable Accbank = new DataTable();
                MySqlConnection con;
                using (con = balayer.OpenConnection())
                {
                    try
                    {
                        MySqlDataAdapter mem2GrpAdp = new MySqlDataAdapter("select distinct t3.Node as Branch from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left join headstree as t8 on (t1.ChitGroupID=t8.NodeID) where  `t1`.`RootID` = 1 and t1.IsDeleted=0  order by t3.Node asc;", con);
                        mem2GrpAdp.Fill(Accbank);
                      
                        ddlbranch.Items.Insert(0, "--select--");
                        for (int i = 0; i < Accbank.Rows.Count; i++)
                        {
                            ddlbranch.Items.Add(balayer.ToobjectstrEvenNull(Accbank.Rows[i]["Branch"]));
                        }
                    }
                    catch (Exception ex)
                    {

                    }
                }
                ddlGroup.SelectedIndex = 1;
                dateFromConsolidated.Text = DateTime.Now.ToString("dd/MM/yyyy");
                dateToConsolidated.Text = DateTime.Now.ToString("dd/MM/yyyy");
            //    setStats(dateFromConsolidated.Text, dateToConsolidated.Text);
                ApplyLayout(1);
            

                foreach (GridViewColumn column in grid.Columns)
                {
                    if (column is GridViewDataColumn)
                    {
                        ((GridViewDataColumn)column).Settings.AutoFilterCondition = AutoFilterCondition.Contains;
                    }
                }

               
            }
            // select(ddlbranch.SelectedIndex);
            grid.DataSource = LoadGrid();
            grid.DataBind();
        }
       
       
        protected void ResetStats()
        {
            if (Convert.ToInt32(Session["Branchid"]) == 161)
            {
              //  Panel_P1.Visible = true;
               

                lblCrAmount_pallathur1.Text = "0.00";
                lblCrBalance_pallathur1.Text = "0.00";
                lblDrAmount_pallathur1.Text = "0.00";
                lblDrBalance_pallathur1.Text = "0.00";

              
            }
            else
            {
              //  Panel_P1.Visible = false;
             
            }
        }
        protected void btnExportExcel_Click1(object sender, EventArgs e)
        {
            gridexcel.FileName = "Direct PallathurbranchFirst" + DateTime.Now.Millisecond.ToString();
            grid.DataSource = LoadGrid();
            grid.DataBind();

            gridexcel.WriteXlsToResponse();
        }
        protected void setStats(string Fromdate, string Todate)
        {
            ResetStats();

            if (Convert.ToInt32(Session["Branchid"]) == 161)
            {
                DataTable dtStats_P1 = balayer.GetDataTable(" SELECT RootID,sum((case when Voucher_Type='C' then Amount else 0.00 end ))as `Credit Amount`,sum((case when Voucher_Type='D' then Amount else 0.00 end ))as `Debit Amount` FROM `svcf`.`voucher` where BranchID=160 and ChoosenDate between '" + balayer.indiandateToMysqlDate(Fromdate) + "' and '" + balayer.indiandateToMysqlDate(Todate) + "' and RootID=1 group by RootID");
                if (dtStats_P1.Rows.Count == 1)
                {
                    decimal decCredit = Convert.ToDecimal(dtStats_P1.Rows[0]["Credit Amount"].ToString());
                    decimal decDebit = Convert.ToDecimal(dtStats_P1.Rows[0]["Debit Amount"].ToString());
                    decimal decDebitBalance = 0.00M;
                    decimal decCreditBalance = 0.00M;
                    decimal.TryParse(balayer.ToobjectstrEvenNull(dtStats_P1.Rows[0][1].ToString()), out decCredit);
                    decimal.TryParse(balayer.ToobjectstrEvenNull(dtStats_P1.Rows[0][2].ToString()), out decDebit);
                    if ((decCredit - decDebit) > 0.00M)
                    {
                        decCreditBalance = decCredit - decDebit;
                    }
                    else if ((decDebit - decCredit) > 0.00M)
                    {
                        decDebitBalance = decDebit - decCredit;
                    }
                    lblCrAmount_pallathur1.Text = decCredit.ToString();
                    lblCrBalance_pallathur1.Text = decCreditBalance.ToString();
                    lblDrAmount_pallathur1.Text = decDebit.ToString();
                    lblDrBalance_pallathur1.Text = decDebitBalance.ToString();
                }


            }
            else
            {
                DataTable dtStats = balayer.GetDataTable(" SELECT RootID,sum((case when Voucher_Type='C' then Amount else 0.00 end ))as `Credit Amount`,sum((case when Voucher_Type='D' then Amount else 0.00 end ))as `Debit Amount` FROM `svcf`.`voucher` where BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and ChoosenDate between '" + balayer.indiandateToMysqlDate(Fromdate) + "' and '" + balayer.indiandateToMysqlDate(Todate) + "' and RootID=1 group by RootID");
                if (dtStats.Rows.Count == 1)
                {
                    decimal decCredit = Convert.ToDecimal(dtStats.Rows[0]["Credit Amount"].ToString());
                    decimal decDebit = Convert.ToDecimal(dtStats.Rows[0]["Debit Amount"].ToString());
                    decimal decDebitBalance = 0.00M;
                    decimal decCreditBalance = 0.00M;
                    decimal.TryParse(balayer.ToobjectstrEvenNull(dtStats.Rows[0][1].ToString()), out decCredit);
                    decimal.TryParse(balayer.ToobjectstrEvenNull(dtStats.Rows[0][2].ToString()), out decDebit);
                    if ((decCredit - decDebit) > 0.00M)
                    {
                        decCreditBalance = decCredit - decDebit;
                    }
                    else if ((decDebit - decCredit) > 0.00M)
                    {
                        decDebitBalance = decDebit - decCredit;
                    }

                }
            }
        }

        protected void ddlbranch_OnSelectedIndexChanged(object sender, EventArgs e)
        {
           if(ddlbranch.SelectedIndex!=0)
           {
              
               //select(ddlbranch.SelectedIndex);

           }
        }
        protected void grid_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
          //  ApplyLayout(Int32.Parse(e.Parameters));
        }
        public DataTable LoadGrid()
        {
            DataTable finalDt = new DataTable();
            if (Convert.ToInt32(Session["Branchid"]) == 161)
            {
                grid.Visible = true;
                if (ddlbranch.SelectedIndex == 0)
                {
                   
                    objVar.varquery = @"select t1.ChoosenDate as `Date`,t8.Node as LedgerHead ,t3.Node as Branch , t1.Narration,(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )as `Credit`,(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )as `Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left join headstree as t8 on (t1.ChitGroupID=t8.NodeID) where `t1`.`BranchID` =160 and `t1`.`RootID` = 1 and t1.ChoosenDate between '" + balayer.indiandateToMysqlDate(dateFromConsolidated.Text) + "' and '" + balayer.indiandateToMysqlDate(dateToConsolidated.Text) + "' and t1.IsDeleted=0 order by t1.ChoosenDate asc";
                    finalDt = balayer.GetDataTable(objVar.varquery);

                  
                }
                else
                {
                    string value = ddlbranch.SelectedItem.Text;
                    objVar.varquery = @"select t1.ChoosenDate as `Date`,t8.Node as LedgerHead ,t3.Node as Branch , t1.Narration,(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )as `Credit`,(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )as `Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left join headstree as t8 on (t1.ChitGroupID=t8.NodeID) where `t1`.`BranchID` =160 and `t1`.`RootID` = 1 and t1.ChoosenDate between '" + balayer.indiandateToMysqlDate(dateFromConsolidated.Text) + "' and '" + balayer.indiandateToMysqlDate(dateToConsolidated.Text) + "' and t1.IsDeleted=0 and t3.Node='" + value + "' order by t1.ChoosenDate asc";
                    finalDt = balayer.GetDataTable(objVar.varquery);

                    

                }

            }
            else
            {
                grid.Visible = false;

                string query = "";

                query = "select 'Previous Net Balance' as 'Title',(case when sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end ) > sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) then CONCAT('Cr.Bal. ',CAST(sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end ) - sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )AS char(50))) else  CONCAT('Dr.Bal.',CAST(sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) - sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )AS char(50)))  end)  as Narration, CONCAT('Cr. INR ', CAST(sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )AS char(50))) as `Credit`,CONCAT('Dr. INR ',CAST(sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )AS char(50))) as `Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left join headstree as t8 on (t1.ChitGroupID=t8.NodeID) where `t1`.`BranchID` =" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + "  and `t1`.`RootID` = 1 and t1.ChoosenDate < '" + balayer.indiandateToMysqlDate(dateFromConsolidated.Text) + "' and t1.IsDeleted=0 order by t1.ChoosenDate asc;";
                BindPrev_Balance = new DataTable();
            }
            LoadGridTemp();

           

            finalDt.Columns[4].DataType = typeof(decimal);
            finalDt.Columns[5].DataType = typeof(decimal);

            if (objVar.tempDt.Rows.Count > 0)
            {
                foreach (DataRow dr in objVar.tempDt.Rows)
                {
                    DataRow newRow = finalDt.NewRow();
                    //onchange in 01/11/2018
                    if (Convert.ToString(dr.ItemArray[2]) != "" || Convert.ToString(dr.ItemArray[3]) != "")
                    {
                        //onchange in 01/11/2018
                        if (Convert.ToDecimal(dr.ItemArray[2]) > Convert.ToDecimal(dr.ItemArray[3]))
                        {
                            newRow[3] = dr.ItemArray[0].ToString();
                            newRow[4] = Convert.ToDecimal(dr.ItemArray[2]) - Convert.ToDecimal(dr.ItemArray[3]);
                            newRow[5] = Convert.ToDecimal("0.00");
                        }
                        else if (Convert.ToDecimal(dr.ItemArray[3]) > Convert.ToDecimal(dr.ItemArray[2]))
                        {
                            newRow[3] = dr.ItemArray[0].ToString();
                            newRow[5] = Convert.ToDecimal(dr.ItemArray[3]) - Convert.ToDecimal(dr.ItemArray[2]);
                            newRow[4] = Convert.ToDecimal("0.00");
                        }
                        else
                        {
                            newRow[3] = dr.ItemArray[0].ToString();
                            newRow[5] = Convert.ToDecimal("0.00");
                            newRow[4] = Convert.ToDecimal("0.00");
                        }
                    }
                    else
                    {
                        newRow[3] = "Previous Net Balance";
                        newRow[5] = Convert.ToDecimal("0.00");
                        newRow[4] = Convert.ToDecimal("0.00");
                    }
                    finalDt.Rows.InsertAt(newRow, 0);
                }
            }
            return finalDt;
        }
      
       
        protected void ddlGroup_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlGroup.SelectedIndex == 0)
            {
                //select(ddlbranch.SelectedIndex);
                grid.TotalSummary["Credit"].SummaryType = SummaryItemType.Sum;
                grid.TotalSummary["Debit"].SummaryType = SummaryItemType.Sum;
                ApplyLayout(0);
              
            }
            else if (ddlGroup.SelectedIndex == 1)
            {
                //  select(ddlbranch.SelectedIndex);
                grid.TotalSummary["Credit"].SummaryType = SummaryItemType.Sum;
                grid.TotalSummary["Debit"].SummaryType = SummaryItemType.Sum;
                ApplyLayout(1);
                
            }
            //else
            //{
            //    select(ddlbranch.SelectedIndex);
            //    ApplyLayout(2);
             
            //}
        }
        protected void ASPxGridView1_SummaryDisplayText(object sender, ASPxGridViewSummaryDisplayTextEventArgs e)
        {
           

        }

        protected void grid_SummaryDisplayText(object sender, ASPxGridViewSummaryDisplayTextEventArgs e)
        {
            if (!(e.Value == null))
            {
                string parsed = balayer.ConvertToIndCurrency(e.Value.ToString());
                if (e.Item.FieldName == "Credit")
                {
                    e.Text = "Cr. " + parsed;
                }
                if (e.Item.FieldName == "Debit")
                {
                    e.Text = "Dr. " + parsed;
                }
                if (e.Item.FieldName == "Narration")
                {
                    e.Text = e.Item.DisplayFormat.ToString() + parsed;
                }
                if (e.Item.FieldName == "Date")
                {
                    e.Text = parsed;
                }
                if (e.Item.FieldName == "Token")
                {
                    e.Text = parsed;
                }
            }
        }


        protected void grid_OnCustomSummaryCalculate(object sender, CustomSummaryEventArgs e)
        {
            if (e.IsTotalSummary)
            {
                ASPxSummaryItem x = (ASPxSummaryItem)e.Item;
                if (x.FieldName.ToString() == "Debit")
                {
                    if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Finalize)
                    {
                        decimal total = 0.00M;
                        for (int i = 0; i < grid.VisibleRowCount; i++)
                        {
                            if (grid.IsGroupRow(i))
                            {
                                total += Convert.ToDecimal(grid.GetGroupSummaryValue(i, grid.GroupSummary["Date"]));
                            }
                        }
                        e.TotalValue = total;
                        e.TotalValueReady = true;
                    }
                }
                if (x.FieldName.ToString() == "Credit")
                {
                    if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Finalize)
                    {
                        decimal total = 0.00M;
                        for (int i = 0; i < grid.VisibleRowCount; i++)
                        {
                            if (grid.IsGroupRow(i))
                            {
                                total += Convert.ToDecimal(grid.GetGroupSummaryValue(i, grid.GroupSummary["Branch"]));
                            }
                        }
                        e.TotalValue = total;
                        e.TotalValueReady = true;
                    }
                }
                if (x.FieldName.ToString() == "Narration")
                {
                    decimal cre = Convert.ToDecimal(grid.GetTotalSummaryValue(grid.TotalSummary["Credit"]));
                    decimal deb = Convert.ToDecimal(grid.GetTotalSummaryValue(grid.TotalSummary["Debit"]));
                    if (cre > deb)
                    {

                        x.DisplayFormat = "Cr.Bal.";

                        e.TotalValue = cre - deb;
                        e.TotalValueReady = true;
                        ViewState["prevcre"] = cre - deb;
                    }
                    else if (cre < deb)
                    {
                        x.DisplayFormat = "Dr.Bal.";

                        e.TotalValue = deb - cre;
                        e.TotalValueReady = true;
                        ViewState["prevdeb"] = deb - cre;
                    }
                    else
                    {
                        e.TotalValue = 0.00;
                        e.TotalValueReady = true;
                    }
                }
            }
            if (e.IsGroupSummary)
            {
                ASPxSummaryItem x = (ASPxSummaryItem)e.Item;
                if (x.FieldName.ToString() == "Date")
                {
                    decimal Credit1 = Convert.ToDecimal(e.GetGroupSummary(e.GroupRowHandle, grid.GroupSummary["Credit"]));
                    decimal Debit1 = Convert.ToDecimal(e.GetGroupSummary(e.GroupRowHandle, grid.GroupSummary["Debit"]));
                    if (Credit1 <= Debit1)
                    {
                        e.TotalValue = Debit1 - Credit1;

                    }
                }
                if (x.FieldName.ToString() == "Branch")
                {
                    decimal Credit1 = Convert.ToDecimal(e.GetGroupSummary(e.GroupRowHandle, grid.GroupSummary["Credit"]));
                    decimal Debit1 = Convert.ToDecimal(e.GetGroupSummary(e.GroupRowHandle, grid.GroupSummary["Debit"]));
                    if (Credit1 >= Debit1)
                    {
                        e.TotalValue = Credit1 - Debit1;

                    }
                }
            }
        }





        protected void Export_click(object sender, MenuItemEventArgs e)
        {
            string Value1 = balayer.ToobjectstrEvenNull(Session["Branchid"]);
            if (Value1 == "161")
            {
                if (e.Item.Text.ToString() == "PDF")
                {
                    using (this.headerImage = System.Drawing.Image.FromFile(Server.MapPath("~\\Images\\visalam.png")))
                    {
                      
                        
                        PrintingSystem ps = new PrintingSystem();
                        PrintableComponentLink gridcheque = new PrintableComponentLink(ps);
                        PrintableComponentLink gridchequeprev = new PrintableComponentLink(ps);
                        gridcheque.Component = gridExport_pallathur1;

                 
                        gridExport_pallathur1.PreserveGroupRowStates = true;

                        grid.DataSource = LoadGrid();
                        grid.SettingsText.Title = "";
                        grid.DataBind();


                        Link header = new Link();
                        CompositeLink compositeLink = new CompositeLink(ps);
                        header.CreateDetailArea += new CreateAreaEventHandler(header_CreateDetailArea);

                        compositeLink.Links.AddRange(new object[] { header, gridcheque });
                        string leftColumn = "Pages : [Page # of Pages #]";
                        string rightColumn = "Date: [Date Printed]\r\nTime: " + DateTime.Now.ToString("hh:mm tt");
                        PageHeaderFooter phf = compositeLink.PageHeaderFooter as PageHeaderFooter;
                        phf.Footer.Content.Clear();
                        phf.Footer.Content.AddRange(new string[] { rightColumn, leftColumn });
                        phf.Footer.LineAlignment = BrickAlignment.Center;
                        using (MemoryStream stream = new MemoryStream())
                        {
                            compositeLink.PaperKind = System.Drawing.Printing.PaperKind.Legal;
                            compositeLink.CreateDocument(false);
                            compositeLink.PrintingSystem.Document.AutoFitToPagesWidth = 1;
                            compositeLink.PrintingSystem.ExportToPdf(stream);
                            WriteToResponse("BranchLedger", true, "pdf", stream);
                        }
                    }
                }
                else if (e.Item.Text.ToString() == "XLSX")
                {
                 
                    gridExport_pallathur1.PreserveGroupRowStates = true;
                    gridExport_pallathur1.WriteXlsxToResponse();

                   
                }
            }
            else
            {
                if (e.Item.Text.ToString() == "PDF")
                {
                    using (this.headerImage = System.Drawing.Image.FromFile(Server.MapPath("~\\Images\\visalam.png")))
                    {
                        PrintingSystem ps = new PrintingSystem();
                        PrintableComponentLink gridcheque = new PrintableComponentLink(ps);
                        PrintableComponentLink gridchequeprev = new PrintableComponentLink(ps);



                    
                        Link header = new Link();
                        CompositeLink compositeLink = new CompositeLink(ps);
                        header.CreateDetailArea += new CreateAreaEventHandler(header_CreateDetailArea);
                        compositeLink.Links.AddRange(new object[] { header, gridchequeprev, gridcheque });
                        string leftColumn = "Pages : [Page # of Pages #]";
                        string rightColumn = "Date: [Date Printed]\r\nTime: " + DateTime.Now.ToString("hh:mm tt");
                        PageHeaderFooter phf = compositeLink.PageHeaderFooter as PageHeaderFooter;
                        phf.Footer.Content.Clear();
                        phf.Footer.Content.AddRange(new string[] { rightColumn, leftColumn });
                        phf.Footer.LineAlignment = BrickAlignment.Center;

                        using (MemoryStream stream = new MemoryStream())
                        {
                            compositeLink.PaperKind = System.Drawing.Printing.PaperKind.A4;
                            compositeLink.CreateDocument(false);
                            compositeLink.PrintingSystem.Document.AutoFitToPagesWidth = 1;
                            compositeLink.PrintingSystem.ExportToPdf(stream);
                            WriteToResponse("BranchLedger", true, "pdf", stream);
                        }
                    }
                }
                else if (e.Item.Text.ToString() == "XLSX")
                {
                    //gridExportprev.PreserveGroupRowStates = true;
                    //gridExportprev.WriteXlsxToResponse();
                    //gridExport.PreserveGroupRowStates = true;
                    //gridExport.WriteXlsxToResponse();
                }
            }
        }
        public DataTable LoadGridTemp()
        {
            objVar.tempDt = new DataTable();
         
               
                if (ddlbranch.SelectedIndex != 0)
                {
                    string value = ddlbranch.SelectedItem.Text;

                    objVar.varquery = "select 'Previous Net Balance' as 'Title',(case when sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end ) > sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) then CONCAT('Cr.Bal. ',CAST(sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end ) - sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )AS char(50))) else  CONCAT('Dr.Bal.',CAST(sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) - sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )AS char(50)))  end)  as Narration,sum((case when t1.Voucher_Type='C' then t1.Amount else 0.00 end ))as `Credit`,sum((case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ))as `Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left join headstree as t8 on (t1.ChitGroupID=t8.NodeID)  where `t1`.`BranchID` = 160  and `t1`.`RootID` = 1 and t1.ChoosenDate < '" + balayer.indiandateToMysqlDate(dateFromConsolidated.Text) + "' and t1.IsDeleted=0 and t3.Node='" + value + "' order by t1.ChoosenDate asc;";
                    // objVar.varquery = "select 'Previous Net Balance' as 'Title',(case when sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end ) > sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) then CONCAT('Cr.Bal. ',CAST(sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end ) - sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )AS char(50))) else  CONCAT('Dr.Bal.',CAST(sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) - sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )AS char(50)))  end)  as Narration, CONCAT('Cr. INR ', CAST(sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )AS char(50))) as `Credit`,CONCAT('Dr. INR ',CAST(sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )AS char(50))) as `Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left join headstree as t8 on (t1.ChitGroupID=t8.NodeID) where `t1`.`BranchID` =" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + "  and `t1`.`RootID` = 1 and t1.ChoosenDate < '" + balayer.indiandateToMysqlDate(dateFromConsolidated.Text) + "' and t1.IsDeleted=0 order by t1.ChoosenDate asc;";
                    objVar.tempDt = balayer.GetDataTable(objVar.varquery);
                   
                }
                else
                {
                    objVar.varquery = "select 'Previous Net Balance' as 'Title',(case when sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end ) > sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) then CONCAT('Cr.Bal. ',CAST(sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end ) - sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )AS char(50))) else  CONCAT('Dr.Bal.',CAST(sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) - sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )AS char(50)))  end)  as Narration,sum((case when t1.Voucher_Type='C' then t1.Amount else 0.00 end ))as `Credit`,sum((case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ))as `Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left join headstree as t8 on (t1.ChitGroupID=t8.NodeID)  where `t1`.`BranchID` = 160  and `t1`.`RootID` = 1 and t1.ChoosenDate < '" + balayer.indiandateToMysqlDate(dateFromConsolidated.Text) + "' and t1.IsDeleted=0 order by t1.ChoosenDate asc;";
                    // objVar.varquery = "select 'Previous Net Balance' as 'Title',(case when sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end ) > sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) then CONCAT('Cr.Bal. ',CAST(sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end ) - sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )AS char(50))) else  CONCAT('Dr.Bal.',CAST(sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end ) - sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )AS char(50)))  end)  as Narration, CONCAT('Cr. INR ', CAST(sum(case when t1.Voucher_Type='C' then t1.Amount else 0.00 end )AS char(50))) as `Credit`,CONCAT('Dr. INR ',CAST(sum(case when t1.Voucher_Type='D' then t1.Amount else 0.00 end )AS char(50))) as `Debit` from voucher as t1  left Join headstree as t3 on t1.Head_ID=t3.NodeID left join membermaster as t4 on t4.MemberIDNew=t1.MemberID left join headstree as t8 on (t1.ChitGroupID=t8.NodeID) where `t1`.`BranchID` =" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + "  and `t1`.`RootID` = 1 and t1.ChoosenDate < '" + balayer.indiandateToMysqlDate(dateFromConsolidated.Text) + "' and t1.IsDeleted=0 order by t1.ChoosenDate asc;";
                    objVar.tempDt = balayer.GetDataTable(objVar.varquery);
                }

            return objVar.tempDt;
        }

        void header_CreateDetailArea(object sender, CreateAreaEventArgs e)
        {
            e.Graph.BorderWidth = 0;
            Rectangle r = new Rectangle(0, 0, 40, 40);
            e.Graph.DrawImage(headerImage, r);
            TextBrick tb = new TextBrick();
            tb.Text = "Sree Visalam Chit Fund Limited";
            tb.Font = new Font("Arial", 10);
            //  tb.Rect = new RectangleF(50, 8, (e.Graph.ClientPageSize.Width / 2), 20);
            tb.Rect = new RectangleF(50, 8, (e.Graph.ClientPageSize.Width / 2), 20);

            tb.BorderWidth = 0;
            tb.BackColor = Color.Transparent;
            tb.HorzAlignment = DevExpress.Utils.HorzAlignment.Center;
            tb.VertAlignment = DevExpress.Utils.VertAlignment.Top;
            e.Graph.DrawBrick(tb);

            TextBrick tb1 = new TextBrick();
            tb1.Text = "Branch : " + Session["BranchName"];
            tb1.Font = new Font("Arial", 9);
            tb1.Rect = new RectangleF(50, 28, (e.Graph.ClientPageSize.Width / 2), 20);
            tb1.BorderWidth = 0;
            tb1.BackColor = Color.Transparent;
            tb1.HorzAlignment = DevExpress.Utils.HorzAlignment.Center;
            tb1.VertAlignment = DevExpress.Utils.VertAlignment.Top;
            e.Graph.DrawBrick(tb1);

            if (ddlbranch.SelectedItem.Text != "--select--")
            {
                TextBrick tb2 = new TextBrick();
                tb2.Text = "Branches I A/c : " + ddlbranch.SelectedItem.Text;
                tb2.Font = new Font("Arial", 9);
                tb2.Rect = new RectangleF(50, 48, (e.Graph.ClientPageSize.Width / 2), 20);
                tb2.BorderWidth = 0;
                tb2.BackColor = Color.Transparent;
                tb2.HorzAlignment = DevExpress.Utils.HorzAlignment.Center;
                tb2.VertAlignment = DevExpress.Utils.VertAlignment.Top;
                e.Graph.DrawBrick(tb2);

                TextBrick tb3 = new TextBrick();
                tb3.Text = "From : " + dateFromConsolidated.Text + " to " + dateToConsolidated.Text;
                tb3.Font = new Font("Arial", 9);
                tb3.Rect = new RectangleF(50, 68, (e.Graph.ClientPageSize.Width / 2), 20);
                tb3.BorderWidth = 0;
                tb3.BackColor = Color.Transparent;
                tb3.HorzAlignment = DevExpress.Utils.HorzAlignment.Center;
                tb3.VertAlignment = DevExpress.Utils.VertAlignment.Top;
                e.Graph.DrawBrick(tb3);
            }
            else
            {
                TextBrick tb3 = new TextBrick();
                tb3.Text = "From : " + dateFromConsolidated.Text + " to " + dateToConsolidated.Text;
                tb3.Font = new Font("Arial", 9);
                tb3.Rect = new RectangleF(50, 48, (e.Graph.ClientPageSize.Width / 2), 20);
                tb3.BorderWidth = 0;
                tb3.BackColor = Color.Transparent;
                tb3.HorzAlignment = DevExpress.Utils.HorzAlignment.Center;
                tb3.VertAlignment = DevExpress.Utils.VertAlignment.Top;
                e.Graph.DrawBrick(tb3);
            }

          

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
        void ApplyLayout(int layoutIndex)
        {
            if (Convert.ToInt32(Session["Branchid"]) == 161)
            {
                grid.BeginUpdate();
                //  grid_pallathur2.BeginUpdate();
                //  grid.BeginUpdate();
                try
                {
                    grid.ClearSort();
                    //  grid_pallathur2.ClearSort();
                    // grid.ClearSort();
                    switch (layoutIndex)
                    {
                        case 0:
                            break;
                        case 1:
                            //   grid_pallathur1.GroupBy(grid_pallathur1.Columns["Branch"]);
                            grid.GroupBy(grid.Columns["Date"]);
                            //grid_pallathur2.GroupBy(grid_pallathur2.Columns["Branch"]);
                            //grid_pallathur2.GroupBy(grid_pallathur2.Columns["Date"]);
                            //grid.GroupBy(grid.Columns["Branch"]);
                            //grid.GroupBy(grid.Columns["Date"]);
                            break;
                            //case 2:
                          //  grid_pallathur1.GroupBy(grid_pallathur1.Columns["Branch"]);
                        //    //grid_pallathur2.GroupBy(grid_pallathur2.Columns["Branch"]);
                        //    //   grid.GroupBy(grid.Columns["Branch"]);
                           // break;
                    }
                }
                finally
                {
                    grid.EndUpdate();
                    //  grid_pallathur2.EndUpdate();
                    // grid.EndUpdate();
                }
                grid.CollapseAll();
                //   grid_pallathur2.CollapseAll();
                //  grid.CollapseAll();
            }
            else
            {
                grid.BeginUpdate();
                try
                {
                    grid.ClearSort();
                    switch (layoutIndex)
                    {
                        case 0:
                            break;
                        case 1:
                            //   grid.GroupBy(grid.Columns["Branch"]);
                            grid.GroupBy(grid.Columns["Date"]);
                            break;
                        case 2:
                            // grid.GroupBy(grid.Columns["Branch"]);
                            break;
                    }
                }
                finally
                {
                    grid.EndUpdate();
                }
                grid.CollapseAll();
            }
        }

        protected void BtnStatisticsGo_Click(object sender, EventArgs e)
        {
            //setStats(dateFromConsolidated.Text, dateToConsolidated.Text);

            grid.DataSource = LoadGrid();

            grid.DataBind();

            if (Convert.ToInt32(Session["Branchid"]) == 161)
            {
                grid.Visible = true;
                grid.SettingsText.Title = "Trial Balance of Branches [Pallatur-I] from " + dateFromConsolidated.Text + " to " + dateToConsolidated.Text;
             
            }
            else
            {
                grid.Visible = false;
             

            }
        }

     

   

    }
}
