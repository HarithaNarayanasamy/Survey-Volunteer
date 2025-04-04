<%@ Page Title=" Edit Money Collector" Language="C#" MasterPageFile="~/Branch.Master"
    AutoEventWireup="true" CodeBehind="EditMonyCollector.aspx.cs" Inherits="SreeVisalamChitFundLtd_phase1.EditMonyCollector" %>

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
<asp:Content ID="Content2" ContentPlaceHolderID="cphMainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="row">
        <div class="twelve columns">
            <div class="box_c">
                <div class="box_c_heading box_actions">
                    <p>
                        Edit Money Collector Details</p>
                </div>
                <div class="box_c_content">
                    <div class="row">
                        <div style="width: 100%; margin: 0 auto;">
                            <dx:ASPxGridView ID="gridBranch" Style="margin: 0 auto;" ClientInstanceName="grid"
                                runat="server" DataSourceID="DataSourceEmployee" KeyFieldName="moneycollid" AutoGenerateColumns="False"
                                Width="100%" EnableRowsCache="False" OnStartRowEditing="gridBranch_StartRowEditing"
                                OnRowDeleting="gridBranch_RowDeleting"
                                OnRowUpdating="gridBranch_RowUpdating">
                                <Columns>
                                    <dx:GridViewCommandColumn Caption="Action" ButtonType="Image" VisibleIndex="0">
                                        <EditButton Visible="True" Text="Edit" Image-Url="Styles/Image/Edit16.png" />
                                        <DeleteButton Visible="true" Text="Delete" Image-Url="Styles/Image/del16.png">
                                        </DeleteButton>
                                    </dx:GridViewCommandColumn>
                                    <dx:GridViewDataTextColumn Visible="false" FieldName="BranchID" Caption="Branch Code" />
                                    <dx:GridViewDataTextColumn FieldName="moneycollid" Caption="Money Collector ID" />
                                    <dx:GridViewDataTextColumn FieldName="moneycollname" Caption="Money Collector Name" />
                                    <dx:GridViewDataTextColumn FieldName="moneycolladdress" Caption="Money Collector Address" />
                                    <dx:GridViewDataTextColumn FieldName="moneycollphno" Caption="Money Collector Phone Number" />
                                </Columns>
                                <Settings ShowTitlePanel="true" ShowHeaderFilterButton="true" ShowFilterRow="true" />
                                <SettingsPager Mode="ShowPager" Position="TopAndBottom" />
                                <Styles Header-Wrap="True" Header-HorizontalAlign="Center" Header-VerticalAlign="Middle" CommandColumn-Paddings-Padding="10">
                                </Styles>
                                <SettingsBehavior ConfirmDelete="true" />
                                <SettingsText Title="Edit Money Collector Details" ConfirmDelete="Are You Sure You Want To Delete Branch?" />
                                <Templates>
                                    <EditForm>
                                        <table style="width: 100%; margin: 0 auto;">
                                            <tr>
                                                <td>
                                                    <dx:ASPxLabel ID="lbmoneycollid" runat="server" Width="100%" Text="Money Collector ID">
                                                    </dx:ASPxLabel>
                                                </td>
                                                <td>
                                                    <dx:ASPxTextBox runat="server" ID="edmoneycollid" ReadOnly="true" Text='<%# Bind("moneycollid") %>'
                                                        Width="100%" ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                                    </dx:ASPxTextBox>
                                                </td>
                                                <td style="padding-left:10px;">
                                                    <dx:ASPxLabel ID="lbmoneycollname" Width="100%" runat="server" Text="Money Collector Name">
                                                    </dx:ASPxLabel>
                                                </td>
                                                <td>
                                                    <dx:ASPxTextBox runat="server" ID="edmoneycollname" Text='<%# Bind("moneycollname") %>'
                                                        Width="100%" ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                                        <ValidationSettings Display="Dynamic">
                                                            <RequiredField IsRequired="true" />
                                                        </ValidationSettings>
                                                    </dx:ASPxTextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="vertical-align:middle;">
                                                    <dx:ASPxLabel ID="lbmoneycolladdress" Width="100%" runat="server" Text="Money Collector Address">
                                                    </dx:ASPxLabel>
                                                </td>
                                                <td>
                                                    <dx:ASPxMemo Height="60px" runat="server" ID="edmoneycolladdress" Text='<%# Bind("moneycolladdress") %>'
                                                        Width="100%" ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                                        <ValidationSettings Display="Dynamic">
                                                            <RequiredField IsRequired="true" />
                                                        </ValidationSettings>
                                                    </dx:ASPxMemo>
                                                    </ValidationSettings>
                                                </td>
                                                <td style="padding-left:10px;vertical-align:middle;">
                                                    <dx:ASPxLabel ID="lbmoneycollphno" Width="100%" runat="server" Text="Money Collector Phone Number">
                                                    </dx:ASPxLabel>
                                                </td>
                                                <td style="vertical-align:middle;">
                                                    <dx:ASPxTextBox runat="server" ID="edmoneycollphno" Text='<%# Bind("moneycollphno") %>'
                                                        Width="100%" ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                                        <ValidationSettings Display="Dynamic">
                                                            <RequiredField IsRequired="true" />
                                                            <RegularExpression ErrorText="Invalid" ValidationExpression="(\+91)?\d{4,}(-)?\d{6,}" />
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
                            <asp:SqlDataSource runat="server" ID="DataSourceEmployee" 
                                ProviderName="MySql.Data.MySqlClient"></asp:SqlDataSource>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
