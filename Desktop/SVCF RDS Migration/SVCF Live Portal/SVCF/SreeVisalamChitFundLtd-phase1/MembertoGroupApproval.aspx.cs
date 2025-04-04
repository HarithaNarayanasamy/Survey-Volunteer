using System;
using System.Data;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.IO;
using DevExpress.XtraReports.UI;
using SVCF_BusinessAccessLayer;
using SVCF_TransactionLayer;
using SVCF_DataAccessLayer;
using log4net;
using log4net.Config;

namespace SreeVisalamChitFundLtd_phase1
{
    public partial class MembertoGroupApproval : System.Web.UI.Page
    {
        #region VarDeclaration
        BusinessLayer balayer = new BusinessLayer();
        TransactionLayer tranlayer = new TransactionLayer();

        #endregion

        ILog logger = log4net.LogManager.GetLogger(typeof(MembertoGroupApproval));

        DataTable dt;      

        public void GroupDetails()
        {
            string strQueryable = "SELECT distinct ms1.GroupNo as Head_Id,g1.GROUPNO FROM svcf.membersuggestion  as ms1 join groupmaster as g1 on   ms1.GroupNo =g1.Head_Id where ms1.NoofRemainingTokens<>0";
           
            dt = balayer.GetDataTable(strQueryable);
            DataRow dr = dt.NewRow();
            dr[0] = "0";
            dr[1] = "--Select--";
            ddlChitGroup.DataSource = dt;
            ddlChitGroup.DataTextField = "GROUPNO";
            ddlChitGroup.DataValueField = "Head_Id";
            dt.Rows.InsertAt(dr, 0);
            ddlChitGroup.DataBind();
        }

        public void MemberDetails()
        {
            string strQueryable = "";
            if (ddlChitGroup.SelectedItem.Text == "All")
            {
                strQueryable = "SELECT t2.B_Name as `SuggestedBranch`,t1.BranchName,t1.CustomerName,g1.GROUPNO,t1.GroupNo as GroupID,t1.EstSuretyDocument,t1.EstCallNoOfAuction,t1.NoofTokens,t1.SuggestedDate,t1.TypeOfMember,t1.Gender,t1.Age,t1.DOB,t1.FatherHusbandName,t1.MotherWifeName,t1.ProprietorName,t1.PartnersName,t1.DateOfPartnershipWithAmendment,t1.compxerox,t1.dateofResol,t1.ProfessionBusiness,t1.NatureofProfessionBusiness,t1.ResidentialAddress,t1.AddressForCommunication,t1.ProofofResidence,t1.AddressProfessionBusiness,t1.TelephoneNoProfessionBusiness,t1.TelephoneNoResidence,t1.MobileNo,t1.MonthlyIncome,t1.SalesTaxRegistrationNoTNGST,t1.CSTRegistrationNumber,t1.IncomeTaxPANoWardandCircle,t1.BankName,t1.SavingsCurrentAccountNo,t1.MemberID,t1.NoofRemainingTokens FROM `view_unapprovedtoken` as t1 join `branchdetails` t2 on  (t1.SuggestedBranchId=t2.Head_Id) join `groupmaster` as g1 on (t1.GroupNo=g1.Head_Id) where t1.NoofRemainingTokens<>0";
            }
            else
            {
                strQueryable = "SELECT t2.B_Name as `SuggestedBranch`,t1.NoofTokensApproved,t1.BranchName,t1.CustomerName,g1.GROUPNO,t1.GroupNo as GroupID,t1.EstSuretyDocument,t1.EstCallNoOfAuction,t1.NoofTokens,DATE_FORMAT( t1.SuggestedDate, '%d/%m/%Y') as SuggestedDate,t1.TypeOfMember,t1.Gender,t1.Age,t1.DOB,t1.FatherHusbandName,t1.MotherWifeName,t1.ProprietorName,t1.PartnersName,t1.DateOfPartnershipWithAmendment,t1.compxerox,t1.dateofResol,t1.ProfessionBusiness,t1.NatureofProfessionBusiness,t1.ResidentialAddress,t1.AddressForCommunication,t1.ProofofResidence,t1.AddressProfessionBusiness,t1.TelephoneNoProfessionBusiness,t1.TelephoneNoResidence,t1.MobileNo,t1.MonthlyIncome,t1.SalesTaxRegistrationNoTNGST,t1.CSTRegistrationNumber,t1.IncomeTaxPANoWardandCircle,t1.BankName,t1.SavingsCurrentAccountNo,t1.MemberID,t1.NoofRemainingTokens,t1.M_ID,m1.moneycollname,t1.slno FROM `view_unapprovedtoken` as t1 join `branchdetails` t2 on  (t1.SuggestedBranchId=t2.Head_Id) join `groupmaster` as g1 on (t1.GroupNo=g1.Head_Id) join moneycollector as m1 on (t1.M_ID=m1.moneycollid) where t1.GroupNo=" + ddlChitGroup.SelectedValue + " and t1.NoofRemainingTokens<>0";
            }
            dt = balayer.GetDataTable(strQueryable);
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }
        protected void btnInfo_yes_Click(object sender, EventArgs e)
        {
            TransactionLayer trn = new TransactionLayer();
            GridViewRow gvRow = (GridViewRow)Session["Row"];
            ModalPopupExtender1.Hide();
            try
            {
                if (lblHint.Text == "Approve")
                {
                    lblHint.Text = "";
                    Page.Validate("not");
                    if (Page.IsValid == false)
                    {
                        ModalPopupExtender1.Show();
                        return;
                    }
                    //string MemberID =balayer.ToobjectstrEvenNull( GridView1.DataKeys[gvRow.RowIndex]["MemberID"]);
                    //string GroupID =balayer.ToobjectstrEvenNull( GridView1.DataKeys[gvRow.RowIndex]["GroupNo"]);

                    if (int.Parse(txtApprovedNumberofTokens.Text) > int.Parse(balayer.ToobjectstrEvenNull(GridView1.DataKeys[gvRow.RowIndex]["NoofRemainingTokens"])))
                    {
                        lblMsgInfoContent.Text = "Plaese Enter a Number Less Than Suggested No of Members!!!";

                        // cnt1.Attributes["class"] = "errormp";
                        txtReasonForRejection.Visible = false;
                        ModalPopupExtender1.PopupControlID = "msgbox";
                        msgbox.Visible = true;
                        ModalPopupExtender1.Show();
                        lblHint.Text = "gt";
                    }
                    else
                    {
                        string slno = balayer.ToobjectstrEvenNull(GridView1.DataKeys[gvRow.RowIndex]["slno"]);
                        //string GroupID = balayer.ToobjectstrEvenNull(GridView1.DataKeys[gvRow.RowIndex]["GroupID"]);

                        string strquery = "update `membersuggestion` set ApprovedDate=now() ,NoofTokensApproved=ifnull(NoofTokensApproved,0)+" + int.Parse(txtApprovedNumberofTokens.Text) + ",NoofRemainingTokens=ifnull(NoofRemainingTokens,0)-" + int.Parse(txtApprovedNumberofTokens.Text) + ",Suggessionifany='"+balayer.MySQLEscapeString(txtSuggession.Text)+"' where slno=" + slno;
                        long sa=trn.insertorupdateTrn(strquery);
                        lblMsgInfoContent.Text = "Approval Finished Successfullly!!!";

                        // cnt1.Attributes["class"] = "success";
                        txtReasonForRejection.Visible = false;
                        ModalPopupExtender1.PopupControlID = "msgbox";
                        msgbox.Visible = true;
                        ModalPopupExtender1.Show();
                        MemberDetails();
                    }
                    trn.CommitTrn();
                    logger.Info("MembertoGroupApproval.aspx - btnInfo_yes_Click():  Completed: " + DateTime.Now + " by: " + Convert.ToString(Session["UserName"]) + "");
                }
                else
                    if (lblHint.Text == "Reject")
                    {
                        lblHint.Text = "";

                        string slno = balayer.ToobjectstrEvenNull(GridView1.DataKeys[gvRow.RowIndex]["slno"]);
                        //string GroupID = balayer.ToobjectstrEvenNull(GridView1.DataKeys[gvRow.RowIndex]["GroupID"]);
                        string strquery = "update `membersuggestion` set RejectedDate=now() ,Reason='" + balayer.MySQLEscapeString(txtReasonForRejection.Text) + "',NoofRemainingTokens=0 where slno=" + slno ;
                        long sa = trn.insertorupdateTrn(strquery);
                        lblMsgInfoContent.Text = "Rejected  Successfullly!!!";

                        // cnt1.Attributes["class"] = "success";
                        txtReasonForRejection.Visible = false;
                        ModalPopupExtender1.PopupControlID = "msgbox";
                        msgbox.Visible = true;
                        ModalPopupExtender1.Show();
                        MemberDetails();
                        trn.CommitTrn();
                        logger.Info("MembertoGroupApproval.aspx - btnInfo_yes_Click(): lblHint.Text : " + DateTime.Now + " by: " + Convert.ToString(Session["UserName"]) + "");
                    }

                    else
                        if (lblHint.Text == "gt")
                        {
                            //lblHint.Text = "";
                            lblHint.Text = "Approve";
                            txtApprovedNumberofTokens.Text = balayer.ToobjectstrEvenNull(GridView1.DataKeys[gvRow.RowIndex]["NoofRemainingTokens"]);
                            ModalPopupExtender1.PopupControlID = "msgboxNoTokens";
                            msgboxNoTokens.Visible = true;
                            ModalPopupExtender1.Show();
                        }
                        else
                        {
                            Response.Redirect(Request.Url.AbsolutePath);
                        }

            }
            catch (Exception ex)
            {
                try
                {
                   trn.RollbackTrn();
                }
                catch (Exception error)
                { }
                finally {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Warning", "alert('" + ex.Message.ToString().Replace("'", "\\'") + "');", true);
                }
            }
            finally
            {
                trn.DisposeTrn();
            }
        }
        protected void IbtnInfo_no_Click(object sender, ImageClickEventArgs e)
        {
            if (lblHint.Text == "")
            {
                Response.Redirect(Request.Url.AbsolutePath);
            }
            lblHint.Text = "";
            ModalPopupExtender1.Hide();
        }
        protected void btnInfo_no_Click(object sender, EventArgs e)
        {
            ModalPopupExtender1.Hide();
        }
        protected void Audit_Click(object sender, EventArgs e)
        {
            ImageButton btndetails = sender as ImageButton;
            GridViewRow gvRow = (GridViewRow)btndetails.NamingContainer;
            //Session["crRow"] = gvrow;

            Session["crMemberID"] = balayer.ToobjectstrEvenNull(GridView1.DataKeys[gvRow.RowIndex]["MemberID"]);
            Session["crGroupID"] = balayer.ToobjectstrEvenNull(GridView1.DataKeys[gvRow.RowIndex]["GroupNo"]);
            //Response.Redirect("TraceCustomer.aspx");
            //Response.Redirect("TraceCustomer.aspx");
            //string newWin = "window.open('" + "TraceCustomer.aspx" + "');";
           // StoreReport(report);
            string newWin = "window.open('" + "webtracecustomer.aspx" + "');";
            ClientScript.RegisterStartupScript(this.GetType(), "pop", newWin, true);
        }

        private void StoreReport(XtraReport report)
        {
            // Create a stream.
            MemoryStream stream = new MemoryStream();

            // Save a report to the stream.
            report.SaveLayout(stream);

            // Save the stream to a session.
            Session["rptcrJoining"] = stream;
        }


        protected void btnLoad_Click(object sender, EventArgs e)
        {
            if (ddlChitGroup.SelectedItem.Text == "--Select--")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "javascript:alert('Please Choose The Chit Group!!!')", true);
                return;
            }
            else
            {
                MemberDetails();
            }
        }
        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            MemberDetails();
        }

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

                balayer.GetInsertItem("create or replace VIEW `view_unapprovedtoken` AS select `t1`.`SuggestedBranchId` AS `SuggestedBranchId`,t1.slno as slno,t1.M_ID as M_ID,t1.NoofTokensApproved as NoofTokensApproved,`t2`.`B_Name` AS `BranchName`,`t3`.`CustomerName` AS `CustomerName`,`t1`.`GroupNo` AS `GroupNo`,`t1`.`PoolNo` AS `PoolNo`,`t1`.`EstSuretyDocument` AS `EstSuretyDocument`,`t1`.`EstCallNoOfAuction` AS `EstCallNoOfAuction`,`t1`.`NoofTokens` AS `NoofTokens`,`t1`.`SuggestedDate` AS `SuggestedDate`,`t3`.`TypeOfMember` AS `TypeOfMember`,`t3`.`Gender` AS `Gender`,`t3`.`Age` AS `Age`,`t3`.`DOB` AS `DOB`,`t3`.`FatherHusbandName` AS `FatherHusbandName`,`t3`.`MotherWifeName` AS `MotherWifeName`,`t3`.`ProprietorName` AS `ProprietorName`,`t3`.`PartnersName` AS `PartnersName`,`t3`.`DateOfPartnershipWithAmendment` AS `DateOfPartnershipWithAmendment`,`t3`.`compxerox` AS `compxerox`,`t3`.`dateofResol` AS `dateofResol`,`t3`.`ProfessionBusiness` AS `ProfessionBusiness`,`t3`.`NatureofProfessionBusiness` AS `NatureofProfessionBusiness`,`t3`.`ResidentialAddress` AS `ResidentialAddress`,`t3`.`AddressForCommunication` AS `AddressForCommunication`,`t3`.`ProofofResidence` AS `ProofofResidence`,`t3`.`AddressProfessionBusiness` AS `AddressProfessionBusiness`,`t3`.`TelephoneNoProfessionBusiness` AS `TelephoneNoProfessionBusiness`,`t3`.`TelephoneNoResidence` AS `TelephoneNoResidence`,`t3`.`MobileNo` AS `MobileNo`,`t3`.`MonthlyIncome` AS `MonthlyIncome`,`t3`.`SalesTaxRegistrationNoTNGST` AS `SalesTaxRegistrationNoTNGST`,`t3`.`CSTRegistrationNumber` AS `CSTRegistrationNumber`,`t3`.`IncomeTaxPANoWardandCircle` AS `IncomeTaxPANoWardandCircle`,`t3`.`BankName` AS `BankName`,`t3`.`SavingsCurrentAccountNo` AS `SavingsCurrentAccountNo`,`t1`.`MemberID` AS `MemberID`,`t1`.`NoofRemainingTokens` AS `NoofRemainingTokens` from ((`membersuggestion` `t1` join `branchdetails` `t2` on((`t2`.`Head_Id` = `t1`.`BranchID`))) join `membermaster` `t3` on((`t3`.`MemberIDNew` = `t1`.`MemberID`))) where (`t1`.`NoofRemainingTokens` <> 0)");
                lblMsgInfoContent.Text = "Page Loaded";
                GroupDetails();
            }
        }

        protected void Approve_Click(object sender, ImageClickEventArgs e)
        {
            lblHint.Text = "Approve";
            ImageButton btndetails = sender as ImageButton;
            GridViewRow gvrow = (GridViewRow)btndetails.NamingContainer;
            Session["Row"] = gvrow;
            string strMemberName = Server.HtmlDecode(gvrow.Cells[5].Text);
            lblMsgInfoContent.Text = "Do You Want to Approve " +  strMemberName + " !!!" ;
            txtApprovedNumberofTokens.Text = balayer.ToobjectstrEvenNull(GridView1.DataKeys[gvrow.RowIndex]["NoofRemainingTokens"]);
            ModalPopupExtender1.PopupControlID = "msgboxNoTokens";
            ModalPopupExtender1.Show();
            msgboxNoTokens.Visible = true;
        }
        protected void dis_Approve_Click(object sender, ImageClickEventArgs e)
        {
            lblHint.Text = "Reject";
            ImageButton btndetails = sender as ImageButton;
            GridViewRow gvrow = (GridViewRow)btndetails.NamingContainer;
            Session["Row"] = gvrow;
            string strMemberName = Server.HtmlDecode(gvrow.Cells[5].Text);
            lblMsgInfoContent.Text = " Do You Want to Reject " + strMemberName + " !!!";
           // cnt1.Attributes["class"] = "info";
            txtReasonForRejection.Visible = true;
            ModalPopupExtender1.PopupControlID = "msgbox";

            ModalPopupExtender1.Show();
            msgbox.Visible = true;
        }
    }
}