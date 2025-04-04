using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxGridView;
using SVCF_BusinessAccessLayer;
using SVCF_TransactionLayer;
using SVCF_DataAccessLayer;
using log4net;
using log4net.Config;

namespace SreeVisalamChitFundLtd_phase1
{
    public partial class EditAuctionDetails : System.Web.UI.Page
    {
        #region Object
        BusinessLayer balayer = new BusinessLayer();
        TransactionLayer trn = new TransactionLayer();
        #endregion

        ILog logger = log4net.LogManager.GetLogger(typeof(EditAuctionDetails));

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var userinfo = HttpContext.Current.User.Identity.Name;
                var qry = "select rs.name from roles as rs inner join rights as rt on (rt.roleid=rs.id) where memberid=" + userinfo + "";
                var usrRole = balayer.GetSingleValue(qry);
                //if (usrRole == "Report")
                //{
                //    Response.Redirect("Home.aspx", false);
                //}
                if (usrRole != "Administrator")
                {
                    Response.Redirect("Home.aspx", false);
                }
            }
            select();
            gridBranch.DataBind();
        }
        void select()
        {
            //DataSourceBranch.SelectCommand = @"select `auctiondetails`.`inccolumn` AS `inccolumn`, `auctiondetails`.`BranchID` AS `BranchID`, `auctiondetails`.`GroupID` AS `GroupID`, `headstree`.`Node` AS `ChitGroup`, DATE_FORMAT( `auctiondetails`.`AuctionDate`, '%d/%m/%Y') as `AuctionDate`, `auctiondetails`.`ReBidNO` AS `ReBidNO`, `auctiondetails`.`DrawNO` AS `DrawNO`, `auctiondetails`.`PrizedMemberID` AS `PrizedMemberID`,`ht1`.`Node` as `PrizedMemberName`, `auctiondetails`.`MemberID` AS `MemberID`, `membermaster`.`CustomerName` AS `CustomerName`, `auctiondetails`.`PrizedAmount` AS `PrizedAmount`, `auctiondetails`.`TotalCommission` AS `TotalCommission`, `auctiondetails`.`Dividend` AS `Dividend`, `auctiondetails`.`KasarAmount` AS `KasarAmount`, `auctiondetails`.`CurrentDueAmount` AS `CurrentDueAmount`, `auctiondetails`.`NextDueAmount` AS `NextDueAmount`, `auctiondetails`.`AdditionalKasarAmount` AS `AdditionalKasarAmount`, `auctiondetails`.`IsPrized` AS `IsPrized`, `auctiondetails`.`IsReAuction` AS `IsReAuction` from `auctiondetails` join `headstree` on (`auctiondetails`.`GroupID`=`headstree`.`NodeID`) left join `svcf`.`headstree` as `ht1` on (`auctiondetails`.`PrizedMemberID`=`ht1`.`NodeID`) left join `svcf`.`membermaster` on (`auctiondetails`.`MemberID`=`membermaster`.`MemberIDNew`) where `auctiondetails`.`BranchID`=" + balayer.ToobjectstrEvenNull(Session["Branchid"]);
            DataSourceBranch.ConnectionString = CommonClassFile.ConnectionString;
            //DataSourceBranch.SelectCommand=@"select `auctiondetails`.`inccolumn` AS `inccolumn`, `auctiondetails`.`BranchID` AS `BranchID`, `auctiondetails`.`GroupID` AS `GroupID`, `headstree`.`Node` AS `ChitGroup`, IFNULL(DATE_FORMAT( `auctiondetails`.`AuctionDate`, '%d/%m/%Y'),0) as `AuctionDate`, "+
            //                                "IFNULL(`auctiondetails`.`ReBidNO`,0) AS `ReBidNO`, `auctiondetails`.`DrawNO` AS `DrawNO`, IFNULL(`auctiondetails`.`PrizedMemberID`,0) AS `PrizedMemberID`,IFNULL(`ht1`.`Node`,'-') as `PrizedMemberName`, IFNULL(`auctiondetails`.`MemberID`,'0') AS `MemberID`,"+
            //                                "IFNULL(`membermaster`.`CustomerName`,'-') AS `CustomerName`,IFNULL(`auctiondetails`.`PrizedAmount`,0) AS `PrizedAmount`,IFNULL(`auctiondetails`.`TotalCommission`,0) AS `TotalCommission`, IFNULL(`auctiondetails`.`Dividend`,0) AS `Dividend`, "+
            //                                "IFNULL(`auctiondetails`.`KasarAmount`,0) AS `KasarAmount`, IFNULL(`auctiondetails`.`CurrentDueAmount`,0) AS `CurrentDueAmount`,IFNULL(`auctiondetails`.`NextDueAmount`,0) AS `NextDueAmount`, IFNULL(`auctiondetails`.`AdditionalKasarAmount`,0) AS `AdditionalKasarAmount`, "+
            //                                "IFNULL(`auctiondetails`.`IsPrized`,'-') AS `IsPrized`,IFNULL(`auctiondetails`.`IsReAuction`,'-') AS `IsReAuction` from `auctiondetails` join `headstree` on (`auctiondetails`.`GroupID`=`headstree`.`NodeID`) left join "+
            //                                "`svcf`.`headstree` as `ht1` on (`auctiondetails`.`PrizedMemberID`=`ht1`.`NodeID`) left join `svcf`.`membermaster` on (`auctiondetails`.`MemberID`=`membermaster`.`MemberIDNew`) where `auctiondetails`.`BranchID`=" + balayer.ToobjectstrEvenNull(Session["Branchid"]);
            DataSourceBranch.SelectCommand = @"select `auctiondetails`.`inccolumn` AS `inccolumn`, `auctiondetails`.`BranchID` AS `BranchID`, `auctiondetails`.`GroupID` AS `GroupID`, `headstree`.`Node` AS `ChitGroup`, IFNULL(DATE_FORMAT( `auctiondetails`.`AuctionDate`, '%d/%m/%Y'),0) as `AuctionDate`, " +
                                          "IFNULL(`auctiondetails`.`ReBidNO`,0) AS `ReBidNO`, `auctiondetails`.`DrawNO` AS `DrawNO`, IFNULL(`auctiondetails`.`PrizedMemberID`,0) AS `PrizedMemberID`,IFNULL(`ht1`.`Node`,'-') as `PrizedMemberName`, IFNULL(`auctiondetails`.`MemberID`,'0') AS `MemberID`," +
                                          "IFNULL(`membertogroupmaster`.`MemberName`,'-') AS `CustomerName`,IFNULL(`auctiondetails`.`PrizedAmount`,0) AS `PrizedAmount`,IFNULL(`auctiondetails`.`TotalCommission`,0) AS `TotalCommission`, IFNULL(`auctiondetails`.`Dividend`,0) AS `Dividend`, " +
                                          "IFNULL(`auctiondetails`.`KasarAmount`,0) AS `KasarAmount`, IFNULL(`auctiondetails`.`CurrentDueAmount`,0) AS `CurrentDueAmount`,IFNULL(`auctiondetails`.`NextDueAmount`,0) AS `NextDueAmount`, IFNULL(`auctiondetails`.`AdditionalKasarAmount`,0) AS `AdditionalKasarAmount`, " +
                                          "IFNULL(`auctiondetails`.`IsPrized`,'-') AS `IsPrized`,IFNULL(`auctiondetails`.`IsReAuction`,'-') AS `IsReAuction` from `auctiondetails` join `headstree` on (`auctiondetails`.`GroupID`=`headstree`.`NodeID`) left join " +
                                          "`svcf`.`headstree` as `ht1` on (`auctiondetails`.`PrizedMemberID`=`ht1`.`NodeID`) left join `svcf`.`membertogroupmaster` on (`auctiondetails`.`PrizedMemberID`=`membertogroupmaster`.`Head_Id`) where `auctiondetails`.`BranchID`=" + balayer.ToobjectstrEvenNull(Session["Branchid"]);
        }

        void AddError(Dictionary<GridViewColumn, string> errors, GridViewColumn column, string errorText)
        {
            if (errors.ContainsKey(column)) return;
            errors[column] = errorText;
        }
        protected void gridBranch_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            System.Globalization.DateTimeFormatInfo usDtfi = new System.Globalization.CultureInfo("en-NZ", false).DateTimeFormat;
            string dt = e.NewValues["AuctionDate"].ToString();
            DateTime dFirstAuctionDate = DateTime.Parse(dt, usDtfi);
            balayer.GetInsertItem("update auctiondetails set AuctionDate='" + dFirstAuctionDate.Date.ToString("yyyy/MM/dd") + "', PrizedMemberID=" + e.NewValues["PrizedMemberID"] + ",PrizedAmount='" + balayer.MySQLEscapeString(balayer.ToobjectstrEvenNull(e.NewValues["PrizedAmount"])) + "',TotalCommission='" + balayer.MySQLEscapeString(balayer.ToobjectstrEvenNull(e.NewValues["TotalCommission"])) + "',Dividend='" + balayer.MySQLEscapeString(balayer.ToobjectstrEvenNull(e.NewValues["Dividend"])) + "',KasarAmount='" + balayer.MySQLEscapeString(balayer.ToobjectstrEvenNull(e.NewValues["KasarAmount"])) + "',CurrentDueAmount='" + balayer.MySQLEscapeString(balayer.ToobjectstrEvenNull(e.NewValues["CurrentDueAmount"])) + "',NextDueAmount='" + balayer.MySQLEscapeString(balayer.ToobjectstrEvenNull(e.NewValues["NextDueAmount"])) + "' where inccolumn=" + e.Keys["inccolumn"]);
            ASPxGridView grid = (sender as ASPxGridView);
            select();
            grid.DataBind();
            e.Cancel = true;

            string p = e.NewValues["PrizedAmount"].ToString(); 
            p = "";

            logger.Info("EditAuctionDetails.aspx - gridBranch_RowUpdating():  Completed: " + DateTime.Now + " by: " + Convert.ToString(Session["UserName"]) + "");
        }
        protected void gridBranch_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
        {
            var userinfo = HttpContext.Current.User.Identity.Name;
               var qry = "select rs.name from roles as rs inner join rights as rt on (rt.roleid=rs.id) where memberid=" + userinfo + "";
                var usrRole = balayer.GetSingleValue(qry);
                if (usrRole == "Administrator")
                {
                    ASPxGridView grid = (sender as ASPxGridView);
                    if (!grid.IsNewRowEditing)
                    {
                        grid.DoRowValidation();
                    }
                }
        //}
            //ASPxGridView grid = (sender as ASPxGridView);
            
            //if (!grid.IsNewRowEditing)
            //{
            //    grid.DoRowValidation();
            //}
        }

        protected void PrizedMember_Init(object sender, EventArgs e)
        {
            DevExpress.Web.ASPxEditors.ASPxComboBox cmbPrizedMember = (DevExpress.Web.ASPxEditors.ASPxComboBox)sender;
            int iGroupID = Convert.ToInt32(gridBranch.GetRowValues(gridBranch.EditingRowVisibleIndex, "GroupID"));
            System.Data.DataTable dtPrizedMember = balayer.GetDataTable("select distinct m.MemberName as PrizedMemberName, m.head_id as PrizedMemberID from membertogroupmaster m where m.groupid=" + iGroupID.ToString());
            //cmbPrizedMember.DataBind();
            for (int iRow = 0; iRow < dtPrizedMember.Rows.Count; iRow++)
            {
                cmbPrizedMember.Items.Add(Convert.ToString(dtPrizedMember.Rows[iRow]["PrizedMemberName"]), Convert.ToInt32(dtPrizedMember.Rows[iRow]["PrizedMemberID"]));
            }
            for (int iList = 0; iList < cmbPrizedMember.Items.Count; iList++)
            {
                if (Convert.ToInt32(cmbPrizedMember.Items[iList].Value) == Convert.ToInt32(gridBranch.GetRowValues(gridBranch.EditingRowVisibleIndex, "PrizedMemberID")))
                {
                    cmbPrizedMember.Items[iList].Selected = true;
                    break;
                }
            }
        }

        protected void PrizedMember_DataBound(object sender, EventArgs e)
        {
            DevExpress.Web.ASPxEditors.ASPxComboBox cmbPrizedMember = (DevExpress.Web.ASPxEditors.ASPxComboBox)sender;
            for (int iList = 0; iList < cmbPrizedMember.Items.Count; iList++)
            {
                if (Convert.ToInt32(cmbPrizedMember.Items[iList].Value) == Convert.ToInt32(gridBranch.GetRowValues(gridBranch.EditingRowVisibleIndex, "PrizedMemberID")))
                {
                    cmbPrizedMember.Items[iList].Selected = true;
                    break;
                }
            }
        }

        protected void gridBranch_ParseValue(object sender, DevExpress.Web.Data.ASPxParseValueEventArgs e)
        {
            if (e.Value == null)
                e.Value = DateTime.Today;
        }

        protected void gridBranch_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
        {
            e.NewValues[""] = DateTime.Today;
        }
    }
}