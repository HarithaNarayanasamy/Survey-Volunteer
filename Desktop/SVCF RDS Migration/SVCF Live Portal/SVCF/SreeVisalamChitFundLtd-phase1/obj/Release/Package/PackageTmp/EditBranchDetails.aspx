<%@ Page Culture="en-GB" Title="Branch Edit" Language="C#" MasterPageFile="~/Branch.Master"
    AutoEventWireup="true" CodeBehind="EditBranchDetails.aspx.cs" Inherits="SreeVisalamChitFundLtd_phase1.WebForm4" %>

<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Head1" runat="server" ContentPlaceHolderID="cphHead">
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
                        Edit Branch Details</p>
                </div>
                <div class="box_c_content">
                    <div class="row">
                        <div style="width:100%; margin: 0 auto;">
                            <dx:ASPxGridView ID="gridBranch"  Style="margin: 0 auto; width: 100%;" ClientInstanceName="grid"
                                runat="server" DataSourceID="DataSourceBranch" KeyFieldName="Head_Id" AutoGenerateColumns="False"
                                EnableRowsCache="False" OnStartRowEditing="gridBranch_StartRowEditing"
                                OnRowDeleting="gridBranch_RowDeleting"
                                OnRowUpdating="gridBranch_RowUpdating">
                                <Columns>
                                    <dx:GridViewCommandColumn Caption="Action" ButtonType="Image" VisibleIndex="0">
                                        <EditButton Visible="True" Text="Edit" Image-Url="Styles/Image/Edit16.png" />
                                        <DeleteButton Visible="true" Text="Delete" Image-Url="Styles/Image/del16.png">
                                        </DeleteButton>
                                    </dx:GridViewCommandColumn>
                                    <dx:GridViewDataTextColumn FieldName="B_Code" Caption="Branch Code" />
                                    <dx:GridViewDataTextColumn FieldName="B_Name" Caption="Branch Name" />
                                    <dx:GridViewDataTextColumn FieldName="B_Head" Caption="Head" />
                                    <dx:GridViewDataTextColumn FieldName="B_Address" Caption="Address" />
                                    <dx:GridViewDataTextColumn FieldName="B_DOC" Caption="Date of Commencement" />
                                    <dx:GridViewDataTextColumn FieldName="B_PhoneNo" Caption="Phone Number" />
                                    <dx:GridViewDataTextColumn FieldName="B_EMail" Caption="E-mail ID" />
                                </Columns>
                                <Settings ShowTitlePanel="true" VerticalScrollableHeight="430" ShowHeaderFilterButton="true"
                                    ShowFilterRow="true" />
                                <SettingsPager Mode="ShowPager" Position="TopAndBottom" />
                                <SettingsText Title="Edit Branch Details" />
                                <Styles Cell-HorizontalAlign="Left" Footer-HorizontalAlign="Left" Header-Wrap="True" HeaderPanel-Wrap="True" Header-HorizontalAlign="Center" Header-VerticalAlign="Middle" CommandColumn-Paddings-Padding="10">
                                </Styles>
                                <SettingsBehavior ConfirmDelete="true" />
                                <SettingsText ConfirmDelete="Are You Sure You Want To Delete Branch?" />
                                <Templates>
                                    <EditForm>
                                        <table style="width: 100%; margin: 0 auto;">
                                            <tr>
                                                <td style="vertical-align:middle;">
                                                    <dx:ASPxLabel  Width="100%" ID="lbBranchID" runat="server" Text="Branch Code">
                                                    </dx:ASPxLabel>
                                                </td>
                                                <td>
                                                    <dx:ASPxTextBox runat="server" ReadOnly="true" ID="edFirst" Text='<%# Bind("B_Code") %>'
                                                        Width="100%" ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                                        <ValidationSettings Display="Dynamic">
                                                            <RequiredField  IsRequired="true" />
                                                        </ValidationSettings>
                                                    </dx:ASPxTextBox>
                                                </td>
                                                <td style="vertical-align:middle;padding-left:5px;">
                                                    <dx:ASPxLabel Width="100%" ID="lbBranchName" runat="server" Text="Branch Name">
                                                    </dx:ASPxLabel>
                                                </td>
                                                <td>
                                                    <dx:ASPxTextBox runat="server" ID="edLast" ReadOnly="true" Text='<%# Bind("B_Name") %>'
                                                        Width="100%" ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                                        <ValidationSettings Display="Dynamic">
                                                            <RequiredField IsRequired="true" />
                                                        </ValidationSettings>
                                                    </dx:ASPxTextBox>
                                                </td>
                                                <td style="vertical-align:middle;padding-left:5px;">
                                                    <dx:ASPxLabel Width="100%" ID="lbHead" runat="server" Text="Branch Head">
                                                    </dx:ASPxLabel>
                                                </td>
                                                <td>
                                                    <dx:ASPxTextBox runat="server" ID="edTitle" Text='<%# Bind("B_Head") %>' Width="100%"
                                                        ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                                        <ValidationSettings Display="Dynamic">
                                                            <RequiredField IsRequired="true" />
                                                        </ValidationSettings>
                                                    </dx:ASPxTextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="vertical-align:middle;">
                                                    <dx:ASPxLabel Width="100%" ID="lbAddress" runat="server" Text="Branch Address">
                                                    </dx:ASPxLabel>
                                                </td>
                                                <td colspan="5">
                                                    <dx:ASPxMemo runat="server" ID="edBirth" Text='<%# Bind("B_Address") %>' Height="60px"
                                                        Width="100%" ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                                        <ValidationSettings Display="Dynamic">
                                                            <RequiredField IsRequired="true" />
                                                        </ValidationSettings>
                                                    </dx:ASPxMemo>
                                                    </ValidationSettings>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="vertical-align:middle;padding-top:11px;">
                                                    <dx:ASPxLabel Width="100%" ID="lbCommencement" runat="server" Text="Date Of Commencement">
                                                    </dx:ASPxLabel>
                                                </td>
                                                <td style="vertical-align:middle;padding-top:5px;">
                                                    <dx:ASPxDateEdit EditFormat="Date" EditFormatString="dd/MM/yyyy" UseMaskBehavior="true"
                                                        runat="server" ID="edCommencement" Text='<%# Bind("B_DOC") %>' Width="100%" ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                                        <ValidationSettings Display="Dynamic">
                                                            <RequiredField IsRequired="true" />
                                                        </ValidationSettings>
                                                    </dx:ASPxDateEdit>
                                                </td>
                                                <td style="vertical-align:middle;padding-top:11px;padding-left:5px;">
                                                    <dx:ASPxLabel Width="100%" ID="lbPhoneNo" runat="server" Text="Phone Number">
                                                    </dx:ASPxLabel>
                                                </td>
                                                <td style="vertical-align:middle;padding-top:5px;">
                                                    <dx:ASPxTextBox runat="server" ID="edHire" Text='<%# Bind("B_PhoneNo") %>' Width="100%"
                                                        ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                                        <ValidationSettings Display="Dynamic">
                                                            <RequiredField IsRequired="true" />
                                                            <RegularExpression ErrorText="Invalid" ValidationExpression="(\+91)?\d{4,}(-)?\d{6,}" />
                                                        </ValidationSettings>
                                                    </dx:ASPxTextBox>
                                                </td>
                                                <td style="vertical-align:middle;padding-left:5px;padding-top:11px;">
                                                    <dx:ASPxLabel Width="100%" ID="lbEmail" runat="server" Text="E-mail ID">
                                                    </dx:ASPxLabel>
                                                </td>
                                                <td style="vertical-align:middle;padding-top:5px;">
                                                    <dx:ASPxTextBox runat="server" ID="edNotes" Text='<%# Bind("B_EMail")%>' Width="100%"
                                                        ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                                        <ValidationSettings Display="Dynamic">
                                                            <RequiredField IsRequired="true" />
                                                            <RegularExpression ErrorText="Invalid" ValidationExpression="^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$" />
                                                        </ValidationSettings>
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
                        </div>
                    </div>
                </div>
                <asp:SqlDataSource runat="server" ID="DataSourceBranch" ProviderName="MySql.Data.MySqlClient" >
                </asp:SqlDataSource>
            </div>
        </div>
    </div>
</asp:Content>
