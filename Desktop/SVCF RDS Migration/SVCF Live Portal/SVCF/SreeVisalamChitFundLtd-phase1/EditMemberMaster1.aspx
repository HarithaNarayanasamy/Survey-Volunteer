<%@ Page Title="SVCF Admin Panel" Culture="en-GB" Language="C#" MasterPageFile="~/Branch.Master" AutoEventWireup="true"
    CodeBehind="EditMemberMaster1.aspx.cs" Inherits="SreeVisalamChitFundLtd_phase1.WebForm8" %>

<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxRoundPanel" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxUploadControl" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Head1" ContentPlaceHolderID="cphHead" runat="server">
    <script language="javascript" type="text/javascript">
        var isValidUpload;
    </script>
    <style type="text/css">
        .dxeSBC
        {
            vertical-align: top;
        }
        .dxeEditArea 
        {
            height:20px !important;
        }
        
        .dxeEditAreaSys 
        {
            height:20px !important;
        }
        td[style="cursor:default;"]
        {
            vertical-align:middle;
        }
    </style>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="box_c">
        <div class="box_c_heading box_actions">
            <p>
                Edit Member Details</p>
        </div>
        <div class="box_c_content">
            <div style="width: 100%; margin: 0 auto;">
                <dx:ASPxGridView ID="gridBranch" Style="margin: 0 auto;" ClientInstanceName="grid" 
                    Width="100%" runat="server" DataSourceID="DataSourceMember" KeyFieldName="MemberIDNew" AutoGenerateColumns="False"
                    EnableRowsCache="False" OnStartRowEditing="gridBranch_StartRowEditing" OnRowValidating="gridBranch_RowValidating"
                    OnRowDeleting="gridBranch_RowDeleting" OnRowUpdating="gridBranch_RowUpdating">
                    <Settings ShowTitlePanel="true" ShowHeaderFilterButton="true" ShowFilterRow="true" ShowHorizontalScrollBar="true" />
                    <Columns>
                        <dx:GridViewCommandColumn Caption="Action" ButtonType="Image" VisibleIndex="0">
                            <EditButton Visible="True" Text="Edit" Image-Url="Styles/Image/Edit16.png" />
                        </dx:GridViewCommandColumn>
                        
                        <dx:GridViewDataBinaryImageColumn ShowInCustomizationForm="true" FieldName="Photo"
                            Caption="Photo" />
                        <dx:GridViewDataTextColumn FieldName="BranchId" Visible="false" Caption="BranchID" />
                        <dx:GridViewDataTextColumn FieldName="MemberID" Caption="Member ID" />
                        <dx:GridViewDataTextColumn FieldName="CustomerName1" Caption="Customer Name" />
                        <dx:GridViewDataTextColumn FieldName="TypeOfMember" Caption="Type Of Member" />
                        <dx:GridViewDataTextColumn FieldName="Gender" Caption="Gender" />
                        <dx:GridViewDataTextColumn FieldName="Age" ></dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="DOB" Caption="DOB" />
                        <dx:GridViewDataTextColumn FieldName="FatherHusbandName" Caption="Father/ Husband Name" />
                        <dx:GridViewDataTextColumn FieldName="MotherWifeName" Caption="Mother/ Wife Name" />
                        <dx:GridViewDataTextColumn FieldName="ProprietorName" Caption="Proprietor Name" />
                        <dx:GridViewDataTextColumn FieldName="PartnersName" Caption="Partners Name" />
                        <dx:GridViewDataTextColumn FieldName="DateOfPartnershipWithAmendment" Caption="Date of Partnership" />
                        <dx:GridViewDataTextColumn FieldName="compxerox" Caption="File Ref. No." />
                        <dx:GridViewDataTextColumn FieldName="dateofResol" Caption="Date of Resolution" />
                        <dx:GridViewDataTextColumn FieldName="ProfessionBusiness" Caption="Profession Business" />
                        <dx:GridViewDataTextColumn FieldName="NatureofProfessionBusiness" Caption="Nature of Business" />
                        <dx:GridViewDataTextColumn FieldName="ResidentialAddress" Caption="Residential Address" />
                        <dx:GridViewDataTextColumn FieldName="AddressForCommunication" Caption="Address for Communication" />
                        <dx:GridViewDataTextColumn FieldName="ProofofResidence" Caption="Proof of Residence" />
                        <dx:GridViewDataTextColumn FieldName="AddressProfessionBusiness" Caption="Address Profession Business" />
                        <dx:GridViewDataTextColumn FieldName="TelephoneNoProfessionBusiness" Caption="Phone No. Profession Business" />
                        <dx:GridViewDataTextColumn FieldName="TelephoneNoResidence" Caption="Phone No. Residence" />
                        <dx:GridViewDataTextColumn FieldName="MobileNo" Caption="Mobile No." />
                        <dx:GridViewDataTextColumn FieldName="MonthlyIncome" Caption="Monthly Income" />
                        <dx:GridViewDataTextColumn FieldName="SalesTaxRegistrationNoTNGST" Caption="Sales Tax Registration No." />
                        <dx:GridViewDataTextColumn FieldName="CSTRegistrationNumber" Caption="CST Registration No." />
                        <dx:GridViewDataTextColumn FieldName="IncomeTaxPANoWardandCircle" Caption="Income Tax P.A. No." />
                        <dx:GridViewDataTextColumn FieldName="BankName" Caption="Bank Name" />
                        <dx:GridViewDataTextColumn FieldName="BranchName" Caption="Branch Name" />
                        <dx:GridViewDataTextColumn FieldName="SavingsCurrentAccountNo" Caption="Savings/ Current Account No." />
                         <dx:GridViewDataTextColumn FieldName="AadharNumber" Caption="Aadhar Number" />
                    </Columns>
                    <SettingsPager Mode="ShowPager" PageSize="10" Position="TopAndBottom" />
                    <Styles Cell-HorizontalAlign="Left" Header-Wrap="True" Header-HorizontalAlign="Center" Header-VerticalAlign="Middle" CommandColumn-Paddings-Padding="10">
                    </Styles>
                    <SettingsBehavior ConfirmDelete="true" />
                    <SettingsText Title="Edit Member Details" ConfirmDelete="Are You Sure You Want To Delete Bank?" />
                    <Templates>
                        <EditForm>
                            <table style="width: 100%; margin: 0 auto;">
                                <tr>
                                    <td valign="top">
                                        <dx:ASPxUploadControl ID="ucImage" runat="server" ClientInstanceName="uploadControl"
                                            OnFileUploadComplete="ucImage_FileUploadComplete" ShowUploadButton="True">
                                            <ClientSideEvents FilesUploadComplete="function(s, e) { if (isValidUpload) { efPanel.PerformCallback();} }"
                                                FileUploadComplete="function(s, e) { isValidUpload = e.isValid; }" Init="function(s, e) { isValidUpload = false; }" />
                                            <UploadButton Text="Upload">
                                            </UploadButton>
                                            <ValidationSettings AllowedFileExtensions=".jpg,.jpeg,.jpe,.gif,.png" MaxFileSize="4194304">
                                            </ValidationSettings>
                                        </dx:ASPxUploadControl>
                                        <dx:ASPxLabel ID="lblAllowebMimeType" runat="server" Text="Allowed image types: jpeg, gif, png">
                                        </dx:ASPxLabel>
                                        <br />
                                        <dx:ASPxLabel ID="lblMaxFileSize" runat="server" Text="Maximum file size: 4Mb">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td>
                                        <dx:ASPxCallbackPanel ID="ASPxRoundPanel1" runat="server" ClientInstanceName="efPanel"
                                            OnCallback="callbackPanel_Callback">
                                            <PanelCollection>
                                                <dx:PanelContent ID="PanelContent1" runat="server">
                                                    <dx:ASPxBinaryImage Height="150" Width="150" ID="previewImage" runat="server" Visible="False">
                                                    </dx:ASPxBinaryImage>
                                                </dx:PanelContent>
                                            </PanelCollection>
                                        </dx:ASPxCallbackPanel>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <dx:ASPxLabel Width="100%" Text="BranchID" runat="server" ID="lbBranchID" Visible="false">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td>
                                        <dx:ASPxTextBox runat="server" ID="edBranchID" Visible="false" Text='<%# Bind("BranchId") %>'
                                            Width="100%" ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                            <ValidationSettings Display="Dynamic">
                                                <RequiredField IsRequired="true" />
                                            </ValidationSettings>
                                        </dx:ASPxTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <dx:ASPxLabel Width="100%" Text="Member ID" runat="server" ID="lbMemberID">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td  style="padding-right:5px;" >
                                        <dx:ASPxTextBox runat="server" ReadOnly="true" ID="edMemberID" Text='<%# Bind("MemberID") %>'
                                            Width="100%" ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                            <ValidationSettings Display="Dynamic">
                                                <RequiredField IsRequired="true" />
                                            </ValidationSettings>
                                        </dx:ASPxTextBox>
                                    </td>
                                    <td>
                                        <dx:ASPxLabel Width="100%" Text="Title" runat="server" ID="lbTitle">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td style="padding-right:5px;">
                                        <dx:ASPxComboBox runat="server" ID="edTitle" Text='<%# Bind("Title") %>' Width="100%"
                                            ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                            <Items>
                                                <dx:ListEditItem Text="Mr." Value="Mr." />
                                                <dx:ListEditItem Text="Ms." Value="Ms." />
                                                <dx:ListEditItem Text="Mrs." Value="Mrs." />
                                            </Items>
                                            <ValidationSettings Display="Dynamic">
                                                <RequiredField IsRequired="true" />
                                            </ValidationSettings>
                                        </dx:ASPxComboBox>
                                    </td>
                                    <td>
                                        <dx:ASPxLabel Width="100%" Text="Customer Name" runat="server" ID="lbCustomerName">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td style="padding-right:5px;">
                                        <dx:ASPxTextBox runat="server" ID="edCustomerName" Text='<%# Bind("CustomerName") %>'
                                            Width="100%" ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                            <ValidationSettings Display="Dynamic">
                                                <RequiredField IsRequired="true" />
                                            </ValidationSettings>
                                        </dx:ASPxTextBox>
                                    </td>
                                    <td>
                                        <dx:ASPxLabel Width="100%" Text="Type Of Member" runat="server" ID="lbTypeOfMember">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td style="padding-right:5px;">
                                        <dx:ASPxComboBox runat="server" ID="edTypeOfMember" Text='<%# Bind("TypeOfMember") %>'
                                            Width="100%" ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                            <Items>
                                                <dx:ListEditItem Text="Individual" Value="Individual" />
                                                <dx:ListEditItem Text="Proprietary Concern" Value="Proprietary Concern" />
                                                <dx:ListEditItem Text="Company" Value="Company" />
                                                <dx:ListEditItem Text="Partnership Firm" Value="Partnership Firm" />
                                            </Items>
                                            <ValidationSettings Display="Dynamic">
                                                <RequiredField IsRequired="true" />
                                            </ValidationSettings>
                                        </dx:ASPxComboBox>
                                    </td>
                                    <td>
                                        <dx:ASPxLabel Width="100%" Text="Age" runat="server" ID="lbAge">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td style="padding-right:5px;">
                                        <dx:ASPxSpinEdit NumberType="Integer" MinValue="0" MaxLength="2" runat="server" ID="edAge" Text='<%# Bind("Age") %>'
                                            Width="100%" ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                            
                                        </dx:ASPxSpinEdit>
                                    </td>
                                    <td>
                                        <dx:ASPxLabel Width="100%" Text="DOB" runat="server" ID="lbDOB">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td style="padding-right:5px;">
                                        <dx:ASPxDateEdit EditFormat="Date" EditFormatString="dd/MM/yyyy" UseMaskBehavior="true"
                                            runat="server" ID="edDOB" Text='<%# Bind("DOB") %>' Width="100%" ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                        </dx:ASPxDateEdit>
                                    </td>
                                    <td>
                                        <dx:ASPxLabel Width="100%" Text="Father/Husband Name" runat="server" ID="lbFatherHusbandName">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td style="padding-right:5px;">
                                        <dx:ASPxTextBox runat="server" ID="edFatherHusbandName" Text='<%# Bind("FatherHusbandName") %>'
                                            Width="100%" >
                                            <ValidationSettings Display="Dynamic">
                                                <RequiredField IsRequired="true" />
                                            </ValidationSettings>
                                        </dx:ASPxTextBox>
                                    </td>
                                    <td>
                                        <dx:ASPxLabel Width="100%" Text="Mother/Wife Name" runat="server" ID="lbMotherWifeName">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td>
                                        <dx:ASPxTextBox runat="server" ID="edMotherWifeName" Text='<%# Bind("MotherWifeName") %>'
                                            Width="100%" ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                            
                                        </dx:ASPxTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <dx:ASPxLabel Width="100%" Text="Proof of Residence and Identity" runat="server"
                                            ID="lbProofofResidence">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td style="padding-right:5px;">
                                        <dx:ASPxComboBox runat="server" ID="edProofofResidence" Text='<%# Bind("ProofofResidence") %>'
                                            Width="100%" ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                            <Items>
                                                <dx:ListEditItem Text="Passport" Value="Passport" />
                                                <dx:ListEditItem Text="Voters ID" Value="Voters ID" />
                                                <dx:ListEditItem Text="Driving Licence" Value="Driving Licence" />
                                                <dx:ListEditItem Text="Ration Card" Value="Ration Card" />
                                                <dx:ListEditItem Text="Post Office Idendity" Value="Post Office Idendity" />
                                                <dx:ListEditItem Text="Aadhar Card No." Value="Aadhar Card No." />
                                                <dx:ListEditItem Text="Other" Value="Other" />
                                            </Items>
                                            
                                        </dx:ASPxComboBox>
                                    </td>
                                    <td>
                                        <dx:ASPxLabel Width="100%" Text="Date of Resolution" runat="server" ID="lbdateofResol">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td style="padding-right:5px;">
                                        <dx:ASPxDateEdit EditFormat="Date" EditFormatString="dd/MM/yyyy" UseMaskBehavior="true"
                                            runat="server" ID="eddateofResol" Text='<%# Bind("dateofResol") %>' Width="100%"
                                            ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">

                                        </dx:ASPxDateEdit>
                                    </td>
                                    <td>
                                        <dx:ASPxLabel Width="100%" Text="Proprietor Name" runat="server" ID="lbProprietorName">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td style="padding-right:5px;">
                                        <dx:ASPxTextBox runat="server" ID="edProprietorName" Text='<%# Bind("ProprietorName") %>'
                                            Width="100%" ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                        </dx:ASPxTextBox>
                                    </td>
                                    <td>
                                        <dx:ASPxLabel Width="100%" Text="Partners Name" runat="server" ID="lbPartnersName">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td style="padding-right:5px;">
                                        <dx:ASPxTextBox runat="server" ID="edPartnersName" Text='<%# Bind("PartnersName") %>'
                                            Width="100%" ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                        </dx:ASPxTextBox>
                                    </td>
                                    <td>
                                        <dx:ASPxLabel Width="100%" Text="Date of Partnership" runat="server" ID="lbDateOfPartnershipWithAmendment">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td style="padding-right:5px;">
                                        <dx:ASPxDateEdit EditFormat="Date" EditFormatString="dd/MM/yyyy" UseMaskBehavior="true"
                                            runat="server" ID="edDateOfPartnershipWithAmendment" Text='<%# Bind("DateOfPartnershipWithAmendment") %>'
                                            Width="100%" ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                        </dx:ASPxDateEdit>
                                    </td>
                                    <td>
                                        <dx:ASPxLabel Width="100%" Text="File Ref. No." runat="server" ID="lbcompxerox">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td style="padding-right:5px;">
                                        <dx:ASPxTextBox runat="server" ID="edcompxerox" Text='<%# Bind("compxerox") %>' Width="100%"
                                            ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                            
                                        </dx:ASPxTextBox>
                                    </td>
                                    <td>
                                        <dx:ASPxLabel Width="100%" Text="Profession Business" runat="server" ID="lbProfessionBusiness">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td style="padding-right:5px;">
                                        <dx:ASPxTextBox runat="server" ID="edProfessionBusiness" Text='<%# Bind("ProfessionBusiness") %>'
                                            Width="100%" ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                            
                                        </dx:ASPxTextBox>
                                    </td>
                                    <td>
                                        <dx:ASPxLabel Width="100%" Text="Nature of Profession" runat="server" ID="lbNatureofProfessionBusiness">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td>
                                        <dx:ASPxTextBox runat="server" ID="edNatureofProfessionBusiness" Text='<%# Bind("NatureofProfessionBusiness") %>'
                                            Width="100%" ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                            
                                        </dx:ASPxTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="vertical-align:middle;">
                                        <dx:ASPxLabel Width="100%" Text="Residential Address" runat="server" ID="lbResidentialAddress">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td style="padding-right:5px;padding-bottom:5px;">
                                        <dx:ASPxMemo Height="50" runat="server" ID="edResidentialAddress" Value='<%# Bind("ResidentialAddress") %>'
                                            Width="100%" ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                            <ValidationSettings Display="Dynamic">
                                                <RequiredField IsRequired="true" />
                                            </ValidationSettings>
                                        </dx:ASPxMemo>
                                    </td>
                                    <td style="vertical-align:middle;">
                                        <dx:ASPxLabel Width="100%" Text="Aadhar Card No." runat="server" ID="ASPxLabel1">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td  style="padding-right:0px;">
                                          <dx:ASPxTextBox runat="server" ID="eAadharcardnum" Text='<%# Bind("AadharNumber") %>' Width="100%">
                                        </dx:ASPxTextBox>                                      
                                    </td>
                                    <td style="vertical-align:middle;">
                                        <dx:ASPxLabel Width="100%" Text="Address For Communication" runat="server" ID="lbAddressForCommunication">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td colspan="3"  style="padding-right:5px;">
                                        <dx:ASPxMemo Height="50" runat="server" ID="edAddressForCommunication" Value='<%# Bind("AddressForCommunication") %>'
                                            Width="100%" ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                            <ValidationSettings Display="Dynamic">
                                                <RequiredField IsRequired="true" />
                                            </ValidationSettings>
                                        </dx:ASPxMemo>
                                    </td>
                                    <td style="vertical-align:middle;">
                                        <dx:ASPxLabel Width="100%" Text="Address For Profession Business" runat="server"
                                            ID="lbAddressProfessionBusiness">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td colspan="3" style="padding-right:5px;">
                                        <dx:ASPxMemo Height="50" runat="server" ID="edAddressProfessionBusiness" Value='<%# Bind("AddressProfessionBusiness") %>'
                                            Width="100%" ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                            <ValidationSettings Display="Dynamic">
                                                <RequiredField IsRequired="true" />
                                            </ValidationSettings>
                                        </dx:ASPxMemo>
                                    </td>
                                    <td>
                                        <dx:ASPxLabel Width="100%" Text="Phone No. Profession" runat="server" ID="lbTelephoneNoProfessionBusiness">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td  style="padding-right:5px;">
                                        <dx:ASPxTextBox runat="server" ID="edTelephoneNoProfessionBusiness" Text='<%# Bind("TelephoneNoProfessionBusiness") %>'
                                            Width="100%" ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                            
                                        </dx:ASPxTextBox>
                                    </td>
                                   
                                </tr>
                                <tr>
                                     <td>
                                        <dx:ASPxLabel Width="100%" Text="Phone No. Residence" runat="server" ID="lbTelephoneNoResidence">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td>
                                        <dx:ASPxTextBox runat="server" ID="edTelephoneNoResidence" Text='<%# Bind("TelephoneNoResidence") %>'
                                            Width="100%" ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                            
                                        </dx:ASPxTextBox>
                                    </td>
                                    <td>
                                        <dx:ASPxLabel Width="100%" Text="Monthly Income" runat="server" ID="lbMonthlyIncome">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td  style="padding-right:5px;">
                                        <dx:ASPxSpinEdit Increment=".50" NumberType="Float" runat="server" ID="edMonthlyIncome" Text='<%# Bind("MonthlyIncome") %>'
                                            Width="100%" ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                            <ValidationSettings Display="Dynamic">
                                                <RequiredField IsRequired="true" />
                                                <RegularExpression ErrorText="Invalid" ValidationExpression="^\d*(\.?\d{0,2})$" />
                                            </ValidationSettings>
                                        </dx:ASPxSpinEdit>
                                    </td>
                                    <td>
                                        <dx:ASPxLabel Width="100%" Text="Mobile No." runat="server" ID="lbMobileNo">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td  style="padding-right:5px;">
                                        <dx:ASPxSpinEdit NumberType="Integer" MaxLength="13" runat="server" ID="edMobileNo"
                                            Text='<%# Bind("MobileNo") %>' Width="100%" ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                        </dx:ASPxSpinEdit>
                                    </td>
                                    <td>
                                        <dx:ASPxLabel Width="100%" Text="Tax Registration No." runat="server" ID="lbSalesTaxRegistrationNoTNGST">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td  style="padding-right:5px;">
                                        <dx:ASPxTextBox runat="server" ID="edSalesTaxRegistrationNoTNGST" Text='<%# Bind("SalesTaxRegistrationNoTNGST") %>'
                                            Width="100%" ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                        </dx:ASPxTextBox>
                                    </td>
                                    <td>
                                        <dx:ASPxLabel Width="100%" Text="CST Registration No." runat="server" ID="lbCSTRegistrationNumber">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td style="padding-right:5px;">
                                        <dx:ASPxTextBox runat="server" ID="edCSTRegistrationNumber" Text='<%# Bind("CSTRegistrationNumber") %>'
                                            Width="100%" ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                        </dx:ASPxTextBox>
                                    </td>
                                    <td>
                                        <dx:ASPxLabel Width="100%" Text="Income Tax P.A.No." runat="server" ID="lbIncomeTaxPANoWardandCircle">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td style="padding-right:5px;">
                                        <dx:ASPxTextBox runat="server" ID="edIncomeTaxPANoWardandCircle" Text='<%# Bind("IncomeTaxPANoWardandCircle") %>'
                                            Width="100%" ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                        </dx:ASPxTextBox>
                                    </td>
                                    <td>
                                        <dx:ASPxLabel Width="100%" Text="Bank Name" runat="server" ID="lbBankName">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td style="padding-right:5px;">
                                        <dx:ASPxTextBox runat="server" ID="edBankName" Text='<%# Bind("BankName") %>' Width="100%"
                                            ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                        </dx:ASPxTextBox>
                                    </td>
                                    <td>
                                        <dx:ASPxLabel Width="100%" Text="Branch Name" runat="server" ID="lbBranchName">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td style="padding-right:5px;">
                                        <dx:ASPxTextBox runat="server" ID="edBranchName" Text='<%# Bind("BranchName") %>'
                                            Width="100%" ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                        </dx:ASPxTextBox>
                                    </td>
                                    <td>
                                        <dx:ASPxLabel Width="100%" Text="Savings/Current Account No." runat="server" ID="lbSavingsCurrentAccountNo">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td>
                                        <dx:ASPxTextBox runat="server" ID="edSavingsCurrentAccountNo" Text='<%# Bind("SavingsCurrentAccountNo") %>'
                                            Width="100%" ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                        </dx:ASPxTextBox>
                                    </td>
                                </tr>
                            </table>
                            <div style="text-align: right; padding: 2px; float: left;">
                                <dx:ASPxGridViewTemplateReplacement ID="UpdateButton" ReplacementType="EditFormUpdateButton"
                                    runat="server"></dx:ASPxGridViewTemplateReplacement>
                                <dx:ASPxGridViewTemplateReplacement ID="CancelButton" ReplacementType="EditFormCancelButton"
                                    runat="server"></dx:ASPxGridViewTemplateReplacement>
                            </div>
                        </EditForm>
                    </Templates>
                </dx:ASPxGridView>
                <asp:SqlDataSource runat="server" ID="DataSourceMember" ConnectionString="server=db.sreevisalam.internal;database=svcf;UID=svcf_admin;PWD=5buo0e0i495Cpnn;Allow Zero Datetime=true;Convert Zero Datetime=True;port=3306"
                    ProviderName="MySql.Data.MySqlClient" SelectCommand="" DeleteCommand="" UpdateCommand="">
                </asp:SqlDataSource>
            </div>
        </div>
    </div>
</asp:Content>
