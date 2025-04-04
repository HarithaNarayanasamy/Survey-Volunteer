<%@ Page Title="SVCF - Admin Page" Culture="en-GB" Language="C#" MasterPageFile="~/Branch.Master"
    AutoEventWireup="true" CodeBehind="receiptbook.aspx.cs" Inherits="SreeVisalamChitFundLtd_phase1.receiptbook" %>

<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register assembly="DevExpress.Web.ASPxEditors.v11.1" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxGridView.v11.1" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <style type="text/css">
        td[style="cursor:default;"]
        {
            vertical-align: middle;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="row">
        <div class="twelve columns">
            <div class="box_c">
                <div class="box_c_heading  box_actions">
                    <p>
                        Receipt Book</p>
                </div>
                <div class="box_c_content">
                    <div class="row">
                        <div style="width: 100%; margin: 0 auto;">
                            <dx:ASPxGridView ID="gridBranch" Style="margin: 0 auto; width: 100%;" ClientInstanceName="grid"
                                KeyFieldName="slNO" runat="server" DataSourceID="DataSourceBranch" AutoGenerateColumns="False"
                                EnableRowsCache="False" OnStartRowEditing="gridBranch_StartRowEditing"  OnRowValidating="gridBranch_RowValidating"
                                OnRowUpdating="gridBranch_RowUpdating" OnRowDeleting="gridBranch_RowDeleting">
                                <Columns>
                                    <dx:GridViewCommandColumn Caption="Action" ButtonType="Image" VisibleIndex="0">
                                        <EditButton Visible="True" Text="Edit" Image-Url="Styles/Image/Edit16.png" />
                                     <DeleteButton Visible="true" Text="Delete" Image-Url="Styles/Image/del16.png"> </DeleteButton>
                                    </dx:GridViewCommandColumn>
                                    <dx:GridViewDataTextColumn FieldName="slNO" Caption="slNO" Visible="false" />
                                    <dx:GridViewDataTextColumn FieldName="moneycollname" Caption="Money Collector Name" />
                                    <dx:GridViewDataTextColumn FieldName="receiptseries" Caption="Receipt Series" />
                                    <dx:GridViewDataTextColumn FieldName="receiptnofrom" Caption="From" />
                                    <dx:GridViewDataTextColumn FieldName="receiptnoto" Caption="To" />
                                    <dx:GridViewDataTextColumn FieldName="status" Caption="status" />
                                </Columns>
                                <Settings ShowTitlePanel="true" VerticalScrollableHeight="430" ShowHeaderFilterButton="true"
                                    ShowFilterRow="true" ShowFilterRowMenu="true" />
                                <SettingsPager Mode="ShowPager" Position="TopAndBottom" />
                                <SettingsText Title="Receipt Book" />
                                <Styles Cell-HorizontalAlign="Left" Footer-HorizontalAlign="Left" Header-Wrap="True"
                                    HeaderPanel-Wrap="True" Header-HorizontalAlign="Center" Header-VerticalAlign="Middle"
                                    CommandColumn-Paddings-Padding="10">
                                </Styles>
                                  <SettingsBehavior ConfirmDelete="true" />
                                <SettingsText ConfirmDelete="Are You Sure You Want To Delete recepitbook?" />
                                <Templates>
                                    <EditForm>
                                        <table style="width: 100%; margin: 0 auto;">
                                            <tr>
                                                <td>
                                                    <dx:ASPxLabel ID="lbBranchID" Visible="false" runat="server" Text="BranchID">
                                                    </dx:ASPxLabel>
                                                </td>
                                                <td>
                                                    <dx:ASPxComboBox runat="server" ID="edTypeofBank" Text='<%# Bind("status") %>'
                                                        Width="100px" ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                                        <Items>
                                                            <dx:ListEditItem Text="Finished" Value="1" />
                                                            <dx:ListEditItem Text="Not Finished" Value="0" />
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
                        </div>
                    </div>
                </div>
                <asp:SqlDataSource runat="server" ID="DataSourceBranch" ProviderName="MySql.Data.MySqlClient">
                </asp:SqlDataSource>
            </div>
        </div>
       
</asp:Content>
