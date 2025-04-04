<%@ Page Title="" Language="C#" MasterPageFile="~/Branch.Master" AutoEventWireup="true" CodeBehind="EditHead.aspx.cs" Inherits="SreeVisalamChitFundLtd_phase1.EditHead" %>

<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax"%> 

<asp:Content ID="Head1" runat="server" ContentPlaceHolderID="cphHead">
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
                <div class="box_c_heading  box_actions">
                    <p>
                        Edit Head</p>
                </div>
                <div class="box_c_content">
                    <div class="row">
                        <div style="width:100%; margin: 0 auto;">
                            <dx:ASPxGridView ID="gridHead"  Style="margin: 0 auto; width: 100%;" ClientInstanceName="grid"
                                runat="server" DataSourceID="DataSourceHead" KeyFieldName="HeadID" AutoGenerateColumns="False"
                                EnableRowsCache="False" OnStartRowEditing="gridHead_StartRowEditing"
                                OnRowDeleting="gridHead_RowDeleting"
                                OnRowUpdating="gridHead_RowUpdating">
                                <Columns>
                                    <dx:GridViewCommandColumn Caption="Action" ButtonType="Image" VisibleIndex="0">
                                        <EditButton Visible="True" Text="Edit" Image-Url="Styles/Image/Edit16.png" />
                                       <%-- <DeleteButton Visible="true" Text="Delete" Image-Url="Styles/Image/del16.png">
                                        </DeleteButton>--%>
                                    </dx:GridViewCommandColumn>
                                    <dx:GridViewDataTextColumn FieldName="NodeId" Caption="Parent ID" />
                                    <dx:GridViewDataTextColumn FieldName="Mainhead" Caption="Head Name" />
                                    <dx:GridViewDataTextColumn FieldName="Sub1" Caption="Subhead-I" />
                                    <dx:GridViewDataTextColumn FieldName="Sub2" Caption="Subhead-II" />
                                    <dx:GridViewDataTextColumn FieldName="Sub3" Caption="Subhead-III" />
                                </Columns>
                                <Settings ShowTitlePanel="true" VerticalScrollableHeight="430" ShowHeaderFilterButton="true"
                                    ShowFilterRow="true" />
                                <SettingsPager Mode="ShowPager" Position="TopAndBottom" />
                                <SettingsText Title="Edit Head" />
                                <Styles Cell-HorizontalAlign="Left" Footer-HorizontalAlign="Left" Header-Wrap="True" HeaderPanel-Wrap="True" Header-HorizontalAlign="Center" Header-VerticalAlign="Middle" CommandColumn-Paddings-Padding="10">
                                </Styles>
                                <SettingsBehavior ConfirmDelete="true" />
                                <SettingsText ConfirmDelete="Are You Sure You Want To Delete Head?" />
                                <Templates>
                                    <EditForm>
                                        <table style="width: 100%; margin: 0 auto;">
                                            <tr>
                                                <td style="vertical-align:middle;">
                                                    <dx:ASPxLabel  Width="100%" ID="lbHeadID" runat="server" Text="Head ID">
                                                    </dx:ASPxLabel>
                                                </td>
                                                <td>
                                                    <dx:ASPxTextBox runat="server" ReadOnly="true" ID="edFirst" Text='<%# Bind("NodeId") %>'
                                                        Width="100%" ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                                        <ValidationSettings Display="Dynamic">
                                                            <RequiredField  IsRequired="true" />
                                                        </ValidationSettings>
                                                    </dx:ASPxTextBox>
                                                </td>
                                                <td style="vertical-align:middle;padding-left:5px;">
                                                    <dx:ASPxLabel Width="100%" ID="lbHeadName" runat="server" Text="Head Name">
                                                    </dx:ASPxLabel>
                                                </td>
                                                <td>
                                                    <dx:ASPxTextBox runat="server" ID="lbMainhead" ReadOnly="true" Text='<%# Bind("Mainhead") %>'
                                                        Width="100%" ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                                        <ValidationSettings Display="Dynamic">
                                                            <RequiredField IsRequired="true" />
                                                        </ValidationSettings>
                                                    </dx:ASPxTextBox>
                                                </td>
                                                <td style="vertical-align:middle;padding-left:5px;">
                                                    <dx:ASPxLabel Width="100%" ID="lbSub1" runat="server" Text="Subhead-I">
                                                    </dx:ASPxLabel>
                                                </td>
                                                <td>
                                                    <dx:ASPxTextBox runat="server" ID="edTitle" Text='<%# Bind("Sub1") %>' Width="100%"
                                                        ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                                        <ValidationSettings Display="Dynamic">
                                                            <RequiredField IsRequired="true" />
                                                        </ValidationSettings>
                                                    </dx:ASPxTextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="vertical-align:middle;">
                                                    <dx:ASPxLabel Width="100%" ID="lbSub2" runat="server" Text="Subhead-II">
                                                    </dx:ASPxLabel>
                                                </td>
                                                <td colspan="5">
                                                    <dx:ASPxTextBox runat="server" ID="edBirth" Text='<%# Bind("Sub2") %>' Width="100%" 
                                                        ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                                        <ValidationSettings Display="Dynamic">
                                                            <RequiredField IsRequired="false" />
                                                        </ValidationSettings>
                                                    </dx:ASPxTextBox>
                                                    </ValidationSettings>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="vertical-align:middle;">
                                                    <dx:ASPxLabel Width="100%" ID="lbSub3" runat="server" Text="Subhead-III">
                                                    </dx:ASPxLabel>
                                                </td>
                                                <td colspan="5">
                                                    <dx:ASPxTextBox runat="server" ID="ASPxTextBox1" Text='<%# Bind("Sub3") %>' Width="100%" 
                                                        ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                                        <ValidationSettings Display="Dynamic">
                                                            <RequiredField IsRequired="false" />
                                                        </ValidationSettings>
                                                    </dx:ASPxTextBox>
                                                    </ValidationSettings>
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
                <asp:SqlDataSource runat="server" ID="DataSourceHead" 
                    ProviderName="MySql.Data.MySqlClient" SelectCommand="">
                </asp:SqlDataSource>
            </div>
        </div>
    </div>
</asp:Content>
