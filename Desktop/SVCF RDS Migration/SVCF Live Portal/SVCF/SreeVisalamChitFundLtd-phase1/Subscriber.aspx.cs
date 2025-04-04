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
using SVCF_TransactionLayer;


namespace SreeVisalamChitFundLtd_phase1
{
    public partial class Subscriber : System.Web.UI.Page
    {
        #region Object
        BusinessLayer balayer = new BusinessLayer();
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void BtnSubscriber_Click(object sender, EventArgs e)
        {
            string SubscriberID = "select count(*)+1 from Subscriber where BranchId='" + balayer.MySQLEscapeString(balayer.ToobjectstrEvenNull(Session["Branchid"])) + "'";
            string Subscriber = balayer.GetSingleValue(SubscriberID);



            string Sub = balayer.ToobjectstrEvenNull(balayer.ToobjectstrEvenNull(Session["Branchid"])) + "-" + (Subscriber);
            int result = balayer.GetInsertItem("Insert into Subscriber(SubscriberID,SubscriberName,Age,DateofBirth,Fathers_HusbandName,NativeAddress,ResidentialAddress,Designation_OfficeAddress,TelephoneNo,IncomefromBusiness,MonthlyBasicPay,DA,OA,OtherIncome,AY,AYAmount,AYPANo,AYOffice,B_H_District,B_H_Taluk,B_H_Village_Town,B_H_Street,B_H_WardNo,B_H_DoorNo,B_H_TownSurveyNo,B_H_RegistarOffice,B_H_SaleValue,B_H_MarketValueDate,B_H_Tax,B_H_TaxReceiptNo,B_H_TaxReceiptDate,B_H_TaxAmount,B_H_RentalIncome,B_H_EncumCertificate,B_H_PropertyRights,B_H_ConditionOnProperty,B_H_Boundary_East,B_H_Boundary_West,B_H_Boundary_South,B_H_Boundary_North,B_H_OtherDetails,B_H_DetailsofAssetsandValue,BusinessName,BusinessStartedYear,NatureofBusiness,FirmRegnNo,Office,CentralSalesTaxRegnNo,TINRegno,AverageCommericalTax,Share,Capital,Designation,PartnerDetails,NetworthofBusiness,AveAnnualIncome,IncomeTaxPANo,IncomeTaxPaidYear1,IncomeTaxOffice,IncomeTaxPaidYear2,ChitNo,ChitValue,AmountRemitted_Installment,AmountRemitted_Amount,FutureInstalmentPayable,SuretyChitNo,SuretyChitValue,SuretyFutureInstalmentPayabel,Date,Relationship,Liabilities,isDeleted,BranchID) values ('" + Sub + "','" + balayer.MySQLEscapeString(txtName.Text) + "','" + balayer.MySQLEscapeString(txtAge.Text) + "','" + balayer.indiandateToMysqlDate(txtDOB.Text) + "','" + balayer.MySQLEscapeString(txtFHusbandName.Text) + "','" + balayer.MySQLEscapeString(txtNativeAddress.Text) + "','" + balayer.MySQLEscapeString(txtResidentialAddress.Text) + "','" + balayer.MySQLEscapeString(txtDesginationOfficeAddress.Text) + "','" + balayer.MySQLEscapeString(txtTelephoneNo.Text) + "','" + balayer.MySQLEscapeString(txtIncomeofMonth.Text) + "','" + balayer.MySQLEscapeString(txtBasicPay.Text) + "','" + balayer.MySQLEscapeString(txtDARs.Text) + "','" + balayer.MySQLEscapeString(txtOARs.Text) + "','" + balayer.MySQLEscapeString(txtOtherIncome.Text) + "','" + balayer.MySQLEscapeString(txtAY.Text) + "','" + balayer.MySQLEscapeString(txtAY_Amount.Text) + "','" + balayer.MySQLEscapeString(txtAy_PANo.Text) + "','" + balayer.MySQLEscapeString(txtAY_Office.Text) + "','" + balayer.MySQLEscapeString(txtDistrict.Text) + "','" + balayer.MySQLEscapeString(txtTaluk.Text) + "','" + balayer.MySQLEscapeString(txtVillageTown.Text) + "','" + balayer.MySQLEscapeString(txtStreet.Text) + "','" + balayer.MySQLEscapeString(txtWardNo.Text) + "','" + balayer.MySQLEscapeString(txtDoorNo.Text) + "','" + balayer.MySQLEscapeString(txtTownSurveyNo.Text) + "','" + balayer.MySQLEscapeString(txtRegisterOffice.Text) + "','" + balayer.MySQLEscapeString(txtSaleValueofProperty.Text) + "','" + balayer.indiandateToMysqlDate(txtMarketValue.Text) + "','" + balayer.MySQLEscapeString(txtTaxperHalfYear.Text) + "','" + balayer.MySQLEscapeString(txtTaxReceiptNo.Text) + "','" + balayer.indiandateToMysqlDate(txtReceiptDate.Text) + "','" + balayer.MySQLEscapeString(txtTax_Amount.Text) + "','" + balayer.MySQLEscapeString(txtRentalIncome.Text) + "','" + balayer.MySQLEscapeString(txtEncumbrance.Text) + "','" + balayer.MySQLEscapeString(ddlRightsofporperty.SelectedItem.Text) + "','" + balayer.MySQLEscapeString(txtConditionAttachedProperty.Text) + "','" + balayer.MySQLEscapeString(txtEast.Text) + "','" + balayer.MySQLEscapeString(txtWest.Text) + "','" + balayer.MySQLEscapeString(txtSouth.Text) + "','" + balayer.MySQLEscapeString(txtNorth.Text) + "','" + balayer.MySQLEscapeString(txtRegardingHouseProperty.Text) + "','" + balayer.MySQLEscapeString(txtDetailsAssetValue.Text) + "','" + balayer.MySQLEscapeString(txtFirmAddress.Text) + "','" + balayer.MySQLEscapeString(txtBusinessStartedYear.Text) + "','" + balayer.MySQLEscapeString(txtNatureofBusiness.Text) + "','" + balayer.MySQLEscapeString(txtTradeLicenceNo.Text) + "','" + balayer.MySQLEscapeString(txtPBOffice1.Text) + "','" + balayer.MySQLEscapeString(txtCentralSalesTaxRegnNo.Text) + "','" + balayer.MySQLEscapeString(txtTinRegNo.Text) + "','" + balayer.MySQLEscapeString(txtCommericalTaxperYear.Text) + "','" + balayer.MySQLEscapeString(txtShare.Text) + "','" + balayer.MySQLEscapeString(txtCaptial.Text) + "','" + balayer.MySQLEscapeString(txtDesignation.Text) + "','" + balayer.MySQLEscapeString(txtPartnersDetails.Text) + "','" + balayer.MySQLEscapeString(txtNetWorthBusiness.Text) + "','" + balayer.MySQLEscapeString(txtAverageAnnualIncome.Text) + "','" + balayer.MySQLEscapeString(txtIncomeTaxPANoofFirm.Text) + "','" + balayer.MySQLEscapeString(txtIncomeTaxOffice.Text) + "','" + balayer.MySQLEscapeString(txtAssessmentYear.Text) + "','" + balayer.MySQLEscapeString(txtAssessment_Year_Rs1.Text) + "','" + balayer.MySQLEscapeString(txtChitNo.Text) + "','" + balayer.MySQLEscapeString(txtPBValue.Text) + "','" + balayer.MySQLEscapeString(txtPBInstallment.Text) + "','" + balayer.MySQLEscapeString(txtAmountRemitted.Text) + "','" + balayer.MySQLEscapeString(txtFutuerInstalmentPayable1.Text) + "','" + balayer.MySQLEscapeString(txtStandsSuretyofChitNo.Text) + "','" + balayer.MySQLEscapeString(txtChitNoValue.Text) + "','" + balayer.MySQLEscapeString(txtFutureInstallmentPayable2.Text) + "','" + balayer.indiandateToMysqlDate(txtLiability_Date.Text) + "','" + txtSubscriberandGuarantor.Text + "','" + balayer.MySQLEscapeString(txtLiabilities.Text) + "','0','" +balayer.ToobjectstrEvenNull(Session["Branchid"]) + "')");

            if (result == 1)
            {
                Pnlmsg.Visible = true;
                ModalPopupExtender1.PopupControlID = "Pnlmsg";
                this.ModalPopupExtender1.Show();

                lblHead.Text = "Status";
                lblContent.Text = "( " + txtName.Text + " )" + "Inserted Successfully";
                lblContent.ForeColor = System.Drawing.Color.Blue;
                ClientScript.RegisterStartupScript(this.Page.GetType(), "Validation", "<script Language='Javascript'>alert('Subscriber Data's Inserted Successfully')</script>");
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
    }
}
