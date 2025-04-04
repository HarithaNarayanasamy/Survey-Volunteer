<%@ Page Title="SVCF Admin Panel" Language="C#" Culture="en-GB" MasterPageFile="~/Branch.Master"
    AutoEventWireup="true" CodeBehind="EditBankDetails1.aspx.cs" Inherits="SreeVisalamChitFundLtd_phase1.WebForm2" %>

<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Head1" ContentPlaceHolderID="cphHead" runat="server">
<style type="text/css">
td[style="cursor:default;"]
        {
            vertical-align:middle;
        }

</style>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="row">
        <div class="twelve columns">
            <div class="box_c">
                <div class="box_c_heading  box_actions">
                    <p>
                        Edit Bank Details</p>
                </div>
                <div class="box_c_content">
                    <div class="row">
                        <br />
                        <div style="width: 100%;margin:0 auto;">
                            <dx:ASPxGridView ID="gridBranch" style="margin:0 auto;" ClientInstanceName="grid" runat="server" DataSourceID="DataSourceBranch"
                                KeyFieldName="Head_Id" AutoGenerateColumns="False" Width="100%" EnableRowsCache="False"
                                OnStartRowEditing="gridBranch_StartRowEditing" OnRowValidating="gridBranch_RowValidating"
                                OnRowDeleting="gridBranch_RowDeleting" OnRowUpdating="gridBranch_RowUpdating">
                                <Columns>
                                    <dx:GridViewCommandColumn Caption="Action" ButtonType="Image" VisibleIndex="0">
                                        <EditButton Visible="True" Text="Edit" Image-Url="Styles/Image/Edit16.png" />
                                        <DeleteButton Visible="true" Text="Delete" Image-Url="Styles/Image/del16.png">
                                        </DeleteButton>
                                    </dx:GridViewCommandColumn>
                                    <dx:GridViewDataTextColumn FieldName="BranchID" Visible="false" Caption="BranchID" />
                                    <dx:GridViewDataTextColumn FieldName="BankName" Caption="Bank Name" />
                                    <dx:GridViewDataTextColumn FieldName="IFCCode" Caption="IFSC Code" />
                                    <dx:GridViewDataTextColumn FieldName="AccountNo" Caption="Account Number" />
                                    <dx:GridViewDataTextColumn FieldName="Address" Caption="Address" />
                                    <dx:GridViewDataTextColumn FieldName="DateOfAccount" Caption="Date of Account" />
                                    <dx:GridViewDataTextColumn FieldName="BankLocation" Caption="Bank Location" />
                                    <dx:GridViewDataTextColumn FieldName="TypeofBank" Caption="Type of Bank" />
                                </Columns>
                                <Settings ShowTitlePanel="true"
                                    ShowHeaderFilterButton="true" ShowFilterRow="true" />
                                <SettingsPager Mode="ShowPager" Position="TopAndBottom" />
                                <Styles Header-Wrap="True" HeaderPanel-Wrap="True" Header-HorizontalAlign="Center" Header-VerticalAlign="Middle" CommandColumn-Paddings-Padding="10">
                                </Styles>
                                <SettingsBehavior ConfirmDelete="true" />
                                <SettingsText Title="Edit Bank Details" ConfirmDelete="Are You Sure You Want To Delete Bank?" />
                                <Templates>
                                    <EditForm>
                                        <table style="width: 100%; margin: 0 auto;">
                                            <tr>
                                                <td>
                                                    <dx:ASPxLabel ID="lbBranchID" Visible="false" runat="server" Text="BranchID">
                                                    </dx:ASPxLabel>
                                                </td>
                                                <td>
                                                    <dx:ASPxTextBox runat="server" ID="edBranchID" Visible="false" Text='<%# Bind("BranchID") %>'
                                                        Width="100%" ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                                        <ValidationSettings>
                                                            <RequiredField IsRequired="true" />
                                                        </ValidationSettings>
                                                    </dx:ASPxTextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <dx:ASPxLabel ID="lbBankName" runat="server" Text="Bank Name">
                                                    </dx:ASPxLabel>
                                                </td>
                                                <td>
                                                    <dx:ASPxTextBox runat="server" ID="edBankName" Text='<%# Bind("BankName") %>' Width="100%"
                                                        ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                                        <ValidationSettings>
                                                            <RequiredField IsRequired="true" />
                                                        </ValidationSettings>
                                                    </dx:ASPxTextBox>
                                                </td>
                                                <td>
                                                    <dx:ASPxLabel ID="lbIFSCCode" runat="server" Text="IFSC Code">
                                                    </dx:ASPxLabel>
                                                </td>
                                                <td>
                                                    <dx:ASPxTextBox runat="server" ID="edIFSCCode" Text='<%# Bind("IFCCode") %>' Width="100%"
                                                        ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                                        <ValidationSettings>
                                                            <RequiredField IsRequired="true" />
                                                        </ValidationSettings>
                                                    </dx:ASPxTextBox>
                                                </td>
                                                <td>
                                                    <dx:ASPxLabel ID="lbAccountNumber" runat="server" Text="Account Number">
                                                    </dx:ASPxLabel>
                                                </td>
                                                <td>
                                                    <dx:ASPxTextBox runat="server" ID="edAccountNo" Text='<%# Bind("AccountNo") %>' Width="100%"
                                                        ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                                        <ValidationSettings>
                                                            <RequiredField IsRequired="true" />
                                                        </ValidationSettings>
                                                    </dx:ASPxTextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="vertical-align:middle;">
                                                    <dx:ASPxLabel ID="edAddress" runat="server" Text="Address">
                                                    </dx:ASPxLabel>
                                                </td>
                                                <td colspan="5" style="padding-bottom:10px;">
                                                    <dx:ASPxMemo Height="50" runat="server" ID="edBirth" Value='<%# Bind("Address") %>'
                                                        Width="100%" ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                                        <ValidationSettings>
                                                            <RequiredField IsRequired="true" />
                                                        </ValidationSettings>
                                                    </dx:ASPxMemo>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <dx:ASPxLabel ID="lbDateofAccount" runat="server" Text="Date of Account">
                                                    </dx:ASPxLabel>
                                                </td>
                                                <td>
                                                    <dx:ASPxDateEdit EditFormat="Date" EditFormatString="dd/MM/yyyy" UseMaskBehavior="true"
                                                        runat="server" ID="edDateOfAccount" Text='<%# Bind("DateOfAccount") %>' Width="100%"
                                                        ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                                        <ValidationSettings>
                                                            <RequiredField IsRequired="true" />
                                                        </ValidationSettings>
                                                    </dx:ASPxDateEdit>
                                                </td>
                                                <td>
                                                    <dx:ASPxLabel ID="lbBankLocation" runat="server" Text="Bank Location">
                                                    </dx:ASPxLabel>
                                                </td>
                                                <td>
                                                    <dx:ASPxTextBox runat="server" ID="edBankLocation" Text='<%# Bind("BankLocation")%>'
                                                        Width="100%" ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                                        <ValidationSettings>
                                                            <RequiredField IsRequired="true" />
                                                        </ValidationSettings>
                                                    </dx:ASPxTextBox>
                                                </td>
                                                <td>
                                                    <dx:ASPxLabel ID="lbTypeofBank" runat="server" Text="Type of Bank">
                                                    </dx:ASPxLabel>
                                                </td>
                                                <td>
                                                    <dx:ASPxComboBox runat="server" ID="edTypeofBank" Text='<%# Bind("TypeofBank") %>'
                                                        Width="100%" ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                                        <Items>
                                                            <dx:ListEditItem Text="Scheduled Banks" Value="Scheduled Banks" />
                                                            <dx:ListEditItem Text="Non Scheduled Banks" Value="Non Scheduled Banks" />
                                                            <dx:ListEditItem Text="Fixed deposits with Banks" Value="Fixed deposits with Banks" />
                                                        </Items>
                                                        <ValidationSettings>
                                                            <RequiredField IsRequired="true" />
                                                        </ValidationSettings>
                                                    </dx:ASPxComboBox>
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
                            <asp:SqlDataSource runat="server" ID="DataSourceBranch"  ProviderName="MySql.Data.MySqlClient"></asp:SqlDataSource>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
