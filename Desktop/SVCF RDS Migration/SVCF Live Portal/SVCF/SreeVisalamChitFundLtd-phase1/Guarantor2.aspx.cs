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
    public partial class Guarantor2 : System.Web.UI.Page
    {
        #region Object
        BusinessLayer balayer = new BusinessLayer();
        TransactionLayer trn = new TransactionLayer();
        #endregion
        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (Session["UserName"] == null || Session["Branchid"] == null || Session["BranchName"] == null)
            {
                Response.Redirect(Page.ResolveUrl("~/Login.aspx"), true);
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                Session["CheckRefresh"] = System.Guid.NewGuid().ToString();
                getgroup();
                txtAge.Text = "0";
                txtIncomeofMonth.Text="0";
                txtBasicPay.Text="0";
                txtDARs.Text="0";
                txtOARs.Text = "0";
                txtOtherIncome.Text = "0";
                txtAY_Amount.Text="0";
                txtWardNo.Text="0";
                txtDoorNo.Text="0";
                txtSaleValueofProperty.Text="0";
                txtRs1.Text="0";
                txtTaxperHalfYear.Text="0";
                txtTax_Amount.Text="0";
                txtRentalIncome.Text="0";
                txtBusinessStartedYear.Text="0";
                txtCommericalTaxperYear.Text = "0";
                txtShare.Text = "0";
                txtPBValue.Text = "0";
                txtAmountRemitted.Text = "0";
                txtPBInstallment.Text = "0";
                txtFutuerInstalmentPayable1.Text = "0";
                txtChitNoValue.Text = "0";
                txtFutureInstallmentPayable2.Text = "0";
                txtNetWorthBusiness.Text = "0";
                txtAverageAnnualIncome.Text = "0";
                txtAssessmentYear.Text = "0";
                txtAssessment_Year_Rs1.Text = "0";
                txtDOB.ToolTip = "Ex. " + DateTime.Now.ToString("dd/MM/yyyy") + " (dd/mm/yyyy)";
                txtMarketValue.ToolTip = "Ex. " + DateTime.Now.ToString("dd/MM/yyyy") + " (dd/mm/yyyy)";
                txtReceiptDate.ToolTip = "Ex. " + DateTime.Now.ToString("dd/MM/yyyy") + " (dd/mm/yyyy)";
                txtLiability_Date.ToolTip = "Ex. " + DateTime.Now.ToString("dd/MM/yyyy") + " (dd/mm/yyyy)";
            }
        }
        protected void Page_PreRender(object sender, EventArgs e)
        {
            ViewState["CheckRefresh"] = Session["CheckRefresh"];
        }
        private void getgroup()
        {
            DataTable dtChitGroup = balayer.GetDataTable("SELECT GROUPNO,Head_Id FROM svcf.groupmaster WHERE BranchID="+ balayer.ToobjectstrEvenNull( Session["Branchid"] )+" AND IsFinished=0");
            DataRow dr = dtChitGroup.NewRow();
            dr[0] = "--Select--";
            dr[1] = "0";
            ddlChitGroup1.DataSource = dtChitGroup;
            ddlChitGroup1.DataTextField = "GROUPNO";
            ddlChitGroup1.DataValueField = "Head_Id";
            dtChitGroup.Rows.InsertAt(dr, 0);
            ddlChitGroup1.DataBind();
            ddlChitGroup2.DataSource = dtChitGroup;
            ddlChitGroup2.DataValueField = "Head_Id";
            ddlChitGroup2.DataTextField = "GROUPNO";
            ddlChitGroup2.DataBind();
        }
        protected void btnCancel_load(object sender,EventArgs e)
        {
            Response.Redirect(Request.Url.AbsolutePath.ToString());
        }
        protected void BtnGuarantor_Click(object sender, EventArgs e)
        {

            Page.Validate("aa");
            if (!Page.IsValid)
            {
                return;
            }
            if (Session["CheckRefresh"].ToString() != ViewState["CheckRefresh"].ToString())
            {
                return;
            }
            bool isFinished = true;

            TransactionLayer trn = new TransactionLayer();
            try
            {
                DataTable dtExist = balayer.GetDataTable("SELECT `guarantormaster`.`GuarantorName`,TelephoneNo FROM svcf.guarantormaster where `guarantormaster`.`GuarantorName`='" + balayer.MySQLEscapeString(txtName.Text) + "' and `guarantormaster`.`TelephoneNo`=" + txtTelephoneNo.Text + "");
                if (dtExist.Rows.Count > 0)
                {
                    isFinished = false;
                    Panel1.Visible = true;
                    ModalPopupExtender1.PopupControlID = "Panel1";
                    this.ModalPopupExtender1.Show();
                    Label73.Text = "Status";
                    Label74.Text = "Guarantor : "+ txtName.Text + " and Mobile Number: "+txtTelephoneNo.Text+" Already Exists!!!";
                    Label74.ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    if (ddlChitGroup1.SelectedIndex == 0)
                    {
                        ddlChitGroup1.SelectedItem.Value = "0";
                    }
                    if (ddlChitGroup2.SelectedIndex == 0)
                    {
                        ddlChitGroup2.SelectedItem.Value = "0";
                    }
                    if (ddlRightsofporperty.SelectedIndex == 0)
                    {
                        ddlRightsofporperty.SelectedItem.Text = "-";
                    }
                    long result = trn.insertorupdateTrn("insert into guarantormaster (GuarantorName,Age,DateofBirth,Fathers_HusbandName,NativeAddress, ResidentialAddress, Designation_OfficeAddress,TelephoneNo,IncomefromBusiness,MonthlyBasicPay,DA,OA,OtherIncome,AY,AYAmount,AYPANo,AYOffice, B_H_District,B_H_Taluk, B_H_Village_Town,B_H_Street,B_H_WardNo,B_H_DoorNo,B_H_TownSurveyNo,B_H_RegistarOffice,B_H_SaleValue,B_H_MarketValueDate,B_H_Tax,B_H_TaxReceiptNo, B_H_TaxReceiptDate,B_H_TaxAmount,B_H_RentalIncome,B_H_EncumCertificate,B_H_PropertyRights,B_H_ConditionOnProperty,B_H_Boundary_East,B_H_Boundary_West,B_H_Boundary_South,B_H_Boundary_North,B_H_OtherDetails,B_H_DetailsofAssetsandValue,BusinessName,BusinessStartedYear,NatureofBusiness,FirmRegnNo, Office,CentralSalesTaxRegnNo,TINRegno,AverageCommericalTax,Share,Capital,Designation,PartnerDetails,NetworthofBusiness,AveAnnualIncome, IncomeTaxPANo,IncomeTaxPaidYear1,IncomeTaxOffice,IncomeTaxPaidYear2,ChitNo,ChitValue,AmountRemitted_Installment,AmountRemitted_Amount, FutureInstalmentPayable,SuretyChitNo,SuretyChitValue,SuretyFutureInstalmentPayabel,Date,Relationship,Liabilities,BranchID,B_H_MarketValueRs) values ('" + balayer.MySQLEscapeString(txtName.Text) + "'," + balayer.MySQLEscapeString(txtAge.Text) + ",'" + balayer.indiandateToMysqlDate(txtDOB.Text) + "','" + balayer.MySQLEscapeString(txtFHusbandName.Text) + "','" + balayer.MySQLEscapeString(txtNativeAddress.Text) + "','" + balayer.MySQLEscapeString(txtResidentialAddress.Text) + "','" + balayer.MySQLEscapeString(txtDesginationOfficeAddress.Text) + "','" + balayer.MySQLEscapeString(txtTelephoneNo.Text) + "'," + balayer.MySQLEscapeString(txtIncomeofMonth.Text) + "," + balayer.MySQLEscapeString(txtBasicPay.Text) + "," + balayer.MySQLEscapeString(txtDARs.Text) + "," + balayer.MySQLEscapeString(txtOARs.Text) + "," + balayer.MySQLEscapeString(txtOtherIncome.Text) + ",'" + balayer.MySQLEscapeString(txtAY.Text) + "'," + balayer.MySQLEscapeString(txtAY_Amount.Text) + ",'" + balayer.MySQLEscapeString(txtAy_PANo.Text) + "','" + balayer.MySQLEscapeString(txtAY_Office.Text) + "','" + balayer.MySQLEscapeString(txtDistrict.Text) + "','" + balayer.MySQLEscapeString(txtTaluk.Text) + "','" + balayer.MySQLEscapeString(txtVillageTown.Text) + "','" + balayer.MySQLEscapeString(txtStreet.Text) + "'," + balayer.MySQLEscapeString(txtWardNo.Text) + "," + balayer.MySQLEscapeString(txtDoorNo.Text) + ",'" + balayer.MySQLEscapeString(txtTownSurveyNo.Text) + "','" + balayer.MySQLEscapeString(txtRegisterOffice.Text) + "'," + balayer.MySQLEscapeString(txtSaleValueofProperty.Text) + ",'" + balayer.indiandateToMysqlDate(txtMarketValue.Text) + "'," + balayer.MySQLEscapeString(txtTaxperHalfYear.Text) + ",'" + balayer.MySQLEscapeString(txtTaxReceiptNo.Text) + "','" + balayer.indiandateToMysqlDate(txtReceiptDate.Text) + "'," + balayer.MySQLEscapeString(txtTax_Amount.Text) + "," + balayer.MySQLEscapeString(txtRentalIncome.Text) + ",'" + balayer.MySQLEscapeString(txtEncumbrance.Text) + "','" + balayer.MySQLEscapeString(ddlRightsofporperty.SelectedItem.Text) + "','" + balayer.MySQLEscapeString(txtConditionAttachedProperty.Text) + "','" + balayer.MySQLEscapeString(txtEast.Text) + "','" + balayer.MySQLEscapeString(txtWest.Text) + "','" + balayer.MySQLEscapeString(txtSouth.Text) + "','" + balayer.MySQLEscapeString(txtNorth.Text) + "','" + balayer.MySQLEscapeString(txtRegardingHouseProperty.Text) + "','" + balayer.MySQLEscapeString(txtDetailsAssetValue.Text) + "','" + balayer.MySQLEscapeString(txtFirmAddress.Text) + "'," + balayer.MySQLEscapeString(txtBusinessStartedYear.Text) + ",'" + balayer.MySQLEscapeString(txtNatureofBusiness.Text) + "','" + balayer.MySQLEscapeString(txtTradeLicenceNo.Text) + "','" + balayer.MySQLEscapeString(txtPBOffice1.Text) + "','" + balayer.MySQLEscapeString(txtCentralSalesTaxRegnNo.Text) + "','" + balayer.MySQLEscapeString(txtTinRegNo.Text) + "'," + balayer.MySQLEscapeString(txtCommericalTaxperYear.Text) + "," + balayer.MySQLEscapeString(txtShare.Text) + ",'" + balayer.MySQLEscapeString(txtCaptial.Text) + "','" + balayer.MySQLEscapeString(txtDesignation.Text) + "','" + balayer.MySQLEscapeString(txtPartnersDetails.Text) + "'," + balayer.MySQLEscapeString(txtNetWorthBusiness.Text) + "," + balayer.MySQLEscapeString(txtAverageAnnualIncome.Text) + ",'" + balayer.MySQLEscapeString(txtIncomeTaxPANoofFirm.Text) + "','" + balayer.MySQLEscapeString(txtAssessmentYear.Text) + "','" + balayer.MySQLEscapeString(txtIncomeTaxOffice.Text) + "'," + balayer.MySQLEscapeString(txtAssessment_Year_Rs1.Text) + "," + balayer.MySQLEscapeString(ddlChitGroup1.SelectedItem.Value) + "," + balayer.MySQLEscapeString(txtPBValue.Text) + "," + balayer.MySQLEscapeString(txtPBInstallment.Text) + "," + balayer.MySQLEscapeString(txtAmountRemitted.Text) + "," + balayer.MySQLEscapeString(txtFutuerInstalmentPayable1.Text) + "," + balayer.MySQLEscapeString(ddlChitGroup2.SelectedItem.Value) + "," + balayer.MySQLEscapeString(txtChitNoValue.Text) + "," + balayer.MySQLEscapeString(txtFutureInstallmentPayable2.Text) + ",'" + balayer.indiandateToMysqlDate(txtLiability_Date.Text) + "','" + balayer.MySQLEscapeString(txtSubscriberandGuarantor.Text) + "','" + balayer.MySQLEscapeString(txtLiabilities.Text) + "'," + balayer.ToobjectstrEvenNull(Session["Branchid"] + "," + txtRs1.Text + ")"));
                }
                trn.CommitTrn();
            }
            catch(Exception error)
            {
                try
                {
                   trn.RollbackTrn();
                }
                catch
                { }
                finally
                {
                    Pnlmsg.Visible = true;
                    ModalPopupExtender1.PopupControlID = "Pnlmsg";
                    this.ModalPopupExtender1.Show();
                    lblHead.Text = "Status";
                    lblContent.Text = error.Message;
                    lblContent.ForeColor = System.Drawing.Color.Red;
                    isFinished = false;
                }
            }
            finally
            {
                trn.DisposeTrn();
                if (isFinished == true)
                {
                    Pnlmsg.Visible = true;
                    ModalPopupExtender1.PopupControlID = "Pnlmsg";
                    this.ModalPopupExtender1.Show();
                    lblHead.Text = "Status";
                    lblContent.Text = "Guarantor : " + txtName.Text + " and Mobile Number : " + txtTelephoneNo.Text + " Inserted Successfully";
                    lblContent.ForeColor = System.Drawing.Color.Green;
                }
                
            }
            foreach (Control ct in tabs.Controls)
            {
                foreach (Control ct1 in ct.Controls)
                {
                    if (ct1.GetType().ToString().ToLower().Contains("textbox"))
                    {
                        ((TextBox)ct1).Text = "";
                    }
                }
            }
        }
        protected void btnyes_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.Url.AbsolutePath.ToString());
        }
        protected void btnno_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.Url.AbsolutePath.ToString());
        }
        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            bool isFinished = true;
            TransactionLayer trn = new TransactionLayer();
            try
            {
                if (ddlChitGroup1.SelectedIndex == 0)
                {
                    ddlChitGroup1.SelectedItem.Value = "0";
                }
                if (ddlChitGroup2.SelectedIndex == 0)
                {
                    ddlChitGroup2.SelectedItem.Value = "0";
                }
                if (ddlRightsofporperty.SelectedIndex == 0)
                {
                    ddlRightsofporperty.SelectedItem.Text = "-";
                }
                long result = trn.insertorupdateTrn("insert into guarantormaster (GuarantorName,Age,DateofBirth,Fathers_HusbandName,NativeAddress, ResidentialAddress, Designation_OfficeAddress,TelephoneNo,IncomefromBusiness,MonthlyBasicPay,DA,OA,OtherIncome,AY,AYAmount,AYPANo,AYOffice, B_H_District,B_H_Taluk, B_H_Village_Town,B_H_Street,B_H_WardNo,B_H_DoorNo,B_H_TownSurveyNo,B_H_RegistarOffice,B_H_SaleValue,B_H_MarketValueDate,B_H_Tax,B_H_TaxReceiptNo, B_H_TaxReceiptDate,B_H_TaxAmount,B_H_RentalIncome,B_H_EncumCertificate,B_H_PropertyRights,B_H_ConditionOnProperty,B_H_Boundary_East,B_H_Boundary_West,B_H_Boundary_South,B_H_Boundary_North,B_H_OtherDetails,B_H_DetailsofAssetsandValue,BusinessName,BusinessStartedYear,NatureofBusiness,FirmRegnNo, Office,CentralSalesTaxRegnNo,TINRegno,AverageCommericalTax,Share,Capital,Designation,PartnerDetails,NetworthofBusiness,AveAnnualIncome, IncomeTaxPANo,IncomeTaxPaidYear1,IncomeTaxOffice,IncomeTaxPaidYear2,ChitNo,ChitValue,AmountRemitted_Installment,AmountRemitted_Amount, FutureInstalmentPayable,SuretyChitNo,SuretyChitValue,SuretyFutureInstalmentPayabel,Date,Relationship,Liabilities,BranchID,B_H_MarketValueRs) values ('" + balayer.MySQLEscapeString(txtName.Text) + "'," + balayer.MySQLEscapeString(txtAge.Text) + ",'" + balayer.indiandateToMysqlDate(txtDOB.Text) + "','" + balayer.MySQLEscapeString(txtFHusbandName.Text) + "','" + balayer.MySQLEscapeString(txtNativeAddress.Text) + "','" + balayer.MySQLEscapeString(txtResidentialAddress.Text) + "','" + balayer.MySQLEscapeString(txtDesginationOfficeAddress.Text) + "','" + balayer.MySQLEscapeString(txtTelephoneNo.Text) + "'," + balayer.MySQLEscapeString(txtIncomeofMonth.Text) + "," + balayer.MySQLEscapeString(txtBasicPay.Text) + "," + balayer.MySQLEscapeString(txtDARs.Text) + "," + balayer.MySQLEscapeString(txtOARs.Text) + "," + balayer.MySQLEscapeString(txtOtherIncome.Text) + ",'" + balayer.MySQLEscapeString(txtAY.Text) + "'," + balayer.MySQLEscapeString(txtAY_Amount.Text) + ",'" + balayer.MySQLEscapeString(txtAy_PANo.Text) + "','" + balayer.MySQLEscapeString(txtAY_Office.Text) + "','" + balayer.MySQLEscapeString(txtDistrict.Text) + "','" + balayer.MySQLEscapeString(txtTaluk.Text) + "','" + balayer.MySQLEscapeString(txtVillageTown.Text) + "','" + balayer.MySQLEscapeString(txtStreet.Text) + "'," + balayer.MySQLEscapeString(txtWardNo.Text) + "," + balayer.MySQLEscapeString(txtDoorNo.Text) + ",'" + balayer.MySQLEscapeString(txtTownSurveyNo.Text) + "','" + balayer.MySQLEscapeString(txtRegisterOffice.Text) + "'," + balayer.MySQLEscapeString(txtSaleValueofProperty.Text) + ",'" + balayer.indiandateToMysqlDate(txtMarketValue.Text) + "'," + balayer.MySQLEscapeString(txtTaxperHalfYear.Text) + ",'" + balayer.MySQLEscapeString(txtTaxReceiptNo.Text) + "','" + balayer.indiandateToMysqlDate(txtReceiptDate.Text) + "'," + balayer.MySQLEscapeString(txtTax_Amount.Text) + "," + balayer.MySQLEscapeString(txtRentalIncome.Text) + ",'" + balayer.MySQLEscapeString(txtEncumbrance.Text) + "','" + balayer.MySQLEscapeString(ddlRightsofporperty.SelectedItem.Text) + "','" + balayer.MySQLEscapeString(txtConditionAttachedProperty.Text) + "','" + balayer.MySQLEscapeString(txtEast.Text) + "','" + balayer.MySQLEscapeString(txtWest.Text) + "','" + balayer.MySQLEscapeString(txtSouth.Text) + "','" + balayer.MySQLEscapeString(txtNorth.Text) + "','" + balayer.MySQLEscapeString(txtRegardingHouseProperty.Text) + "','" + balayer.MySQLEscapeString(txtDetailsAssetValue.Text) + "','" + balayer.MySQLEscapeString(txtFirmAddress.Text) + "'," + balayer.MySQLEscapeString(txtBusinessStartedYear.Text) + ",'" + balayer.MySQLEscapeString(txtNatureofBusiness.Text) + "','" + balayer.MySQLEscapeString(txtTradeLicenceNo.Text) + "','" + balayer.MySQLEscapeString(txtPBOffice1.Text) + "','" + balayer.MySQLEscapeString(txtCentralSalesTaxRegnNo.Text) + "','" + balayer.MySQLEscapeString(txtTinRegNo.Text) + "'," + balayer.MySQLEscapeString(txtCommericalTaxperYear.Text) + "," + balayer.MySQLEscapeString(txtShare.Text) + ",'" + balayer.MySQLEscapeString(txtCaptial.Text) + "','" + balayer.MySQLEscapeString(txtDesignation.Text) + "','" + balayer.MySQLEscapeString(txtPartnersDetails.Text) + "'," + balayer.MySQLEscapeString(txtNetWorthBusiness.Text) + "," + balayer.MySQLEscapeString(txtAverageAnnualIncome.Text) + ",'" + balayer.MySQLEscapeString(txtIncomeTaxPANoofFirm.Text) + "'," + balayer.MySQLEscapeString(txtAssessmentYear.Text) + ",'" + balayer.MySQLEscapeString(txtIncomeTaxOffice.Text) + "'," + balayer.MySQLEscapeString(txtAssessment_Year_Rs1.Text) + "," + balayer.MySQLEscapeString(ddlChitGroup1.SelectedItem.Value) + "," + balayer.MySQLEscapeString(txtPBValue.Text) + "," + balayer.MySQLEscapeString(txtPBInstallment.Text) + "," + balayer.MySQLEscapeString(txtAmountRemitted.Text) + "," + balayer.MySQLEscapeString(txtFutuerInstalmentPayable1.Text) + "," + balayer.MySQLEscapeString(ddlChitGroup2.SelectedItem.Value) + "," + balayer.MySQLEscapeString(txtChitNoValue.Text) + "," + balayer.MySQLEscapeString(txtFutureInstallmentPayable2.Text) + ",'" + balayer.indiandateToMysqlDate(txtLiability_Date.Text) + "','" + balayer.MySQLEscapeString(txtSubscriberandGuarantor.Text) + "','" + balayer.MySQLEscapeString(txtLiabilities.Text) + "'," + balayer.ToobjectstrEvenNull(Session["Branchid"]) + "," + txtRs1.Text + ")");
                
                trn.CommitTrn();
            }
            catch(Exception error)
            {
                try
                {
                   trn.RollbackTrn();
                }
                catch { }
                finally
                {
                    Panel1.Visible = false;
                    Pnlmsg.Visible = true;
                    ModalPopupExtender1.PopupControlID = "Pnlmsg";
                    this.ModalPopupExtender1.Show();
                    lblHead.Text = "Status";
                    lblContent.Text = error.Message;
                    lblContent.ForeColor = System.Drawing.Color.Red;
                    isFinished = false;
                }
            }
            finally
            {
                trn.DisposeTrn();
                if (isFinished == true)
                {
                    Panel1.Visible = false;
                    Pnlmsg.Visible = true;
                    ModalPopupExtender1.PopupControlID = "Pnlmsg";
                    this.ModalPopupExtender1.Show();
                    lblHead.Text = "Status";
                    lblContent.Text = "Guarantor : " + txtName.Text + " Added Successfully";
                    lblContent.ForeColor = System.Drawing.Color.Green;
                }
            }
        }
    }
}
