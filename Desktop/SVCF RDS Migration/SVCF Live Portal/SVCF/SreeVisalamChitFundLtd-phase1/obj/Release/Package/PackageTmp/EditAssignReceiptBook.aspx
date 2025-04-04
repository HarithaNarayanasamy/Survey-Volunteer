<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditAssignReceiptBook.aspx.cs" Inherits="SreeVisalamChitFundLtd_phase1.EditAssignReceiptBook"
MasterPageFile="~/Branch.Master" %>


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
                        Edit ReceiptBook</p>
                </div>
                <div class="box_c_content">
                    <div class="row">
                        <div style="width: 100%; margin: 0 auto;">
                            <dx:ASPxGridView ID="gridBranch" Style="margin: 0 auto;" ClientInstanceName="grid"
                                runat="server"  AutoGenerateColumns="False" DataSourceID="DataSourceEmployee" KeyFieldName="slNO"
                                Width="100%" EnableRowsCache="False"  OnStartRowEditing="gridBranch_StartRowEditing"
                                OnRowDeleting="gridBranch_RowDeleting"
                                OnRowUpdating="gridBranch_RowUpdating" OnRowValidating="gridBranch_RowValidating">


                              
                                <Columns>
                                    <dx:GridViewCommandColumn Caption="Action" ButtonType="Image" VisibleIndex="0">
                                        <EditButton Visible="True" Text="Edit" Image-Url="Styles/Image/Edit16.png" />
                                        <DeleteButton Visible="false" Text="Delete" Image-Url="Styles/Image/del16.png">
                                        </DeleteButton>
                                    </dx:GridViewCommandColumn>
                                    <dx:GridViewDataTextColumn Visible="false" FieldName="BranchID" Caption="Branch Code" />
                                    <dx:GridViewDataTextColumn FieldName="moneycollid" Caption="Money Collector ID" />
                                    <dx:GridViewDataTextColumn FieldName="moneycollname" Caption="Name" />
                                    <dx:GridViewDataTextColumn FieldName="moneycolladdress" Caption="Address" />
                                    <dx:GridViewDataTextColumn FieldName="moneycollphno" Caption=" Phone Number" />



                                    <dx:GridViewDataTextColumn FieldName="receiptseries" Caption="Receipt Series" />
                                    <dx:GridViewDataTextColumn FieldName="receiptnofrom" Caption="Receipt Number From" />
                                    <dx:GridViewDataTextColumn FieldName="receiptnoto" Caption="Receipt Number To" />
                                    <dx:GridViewDataTextColumn FieldName="alreadyusedReceipts" Caption=" Last Used Receipt No." />




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
                                               
                                            </tr>



                                             <tr>
                                                <td style="vertical-align:middle;">
                                                    <dx:ASPxLabel ID="ASPxLabel1" Width="100%" runat="server" Text="Receipt Series">
                                                    </dx:ASPxLabel>
                                                </td>
                                                <td>
                                                    <dx:ASPxMemo Height="60px" runat="server" ID="edReceipt" Text='<%# Bind("receiptseries") %>'
                                                        Width="100%" ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                                        <ValidationSettings Display="Dynamic">
                                                            <RequiredField IsRequired="false" />
                                                        </ValidationSettings>
                                                    </dx:ASPxMemo>
                                                    </ValidationSettings>
                                                </td>
                                                <td style="padding-left:10px;vertical-align:middle;">
                                                    <dx:ASPxLabel ID="ASPxLabel2" Width="100%" runat="server" Text="Receipt Number From">
                                                    </dx:ASPxLabel>
                                                </td>
                                                <td style="vertical-align:middle;">
                                                    <dx:ASPxTextBox runat="server" ID="edFrom" Text='<%# Bind("receiptnofrom") %>'
                                                        Width="100%" ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                                        <ValidationSettings Display="Dynamic">
                                                            <RequiredField IsRequired="false" />
                                                           
                                                        </ValidationSettings>
                                                    </dx:ASPxTextBox>
                                                </td>
                                            </tr>



                                             <tr>
                                                <td style="vertical-align:middle;">
                                                    <dx:ASPxLabel ID="ASPxLabel3" Width="100%" runat="server" Text="Receipt Number To">
                                                    </dx:ASPxLabel>
                                                </td>
                                                <td>
                                                    <dx:ASPxMemo Height="60px" runat="server" ID="edTo" Text='<%# Bind("receiptnoto") %>'
                                                        Width="100%" ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                                        <ValidationSettings Display="Dynamic">
                                                            <RequiredField IsRequired="false" />
                                                        </ValidationSettings>
                                                    </dx:ASPxMemo>
                                                    </ValidationSettings>
                                                </td>
                                                <td style="padding-left:10px;vertical-align:middle;">
                                                    <dx:ASPxLabel ID="ASPxLabel4" Width="100%" runat="server" Text="Last Used Receipt No.">
                                                    </dx:ASPxLabel>
                                                </td>
                                                <td style="vertical-align:middle;">
                                                    <dx:ASPxTextBox runat="server" ID="edLastUsed" Text='<%# Bind("alreadyusedReceipts") %>'
                                                        Width="100%" ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                                        <ValidationSettings Display="Dynamic">
                                                            <RequiredField IsRequired="false" />
                                                           
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
                           <asp:SqlDataSource runat="server" ID="DataSourceEmployee" ConnectionString="server=db.sreevisalam.internal;database=svcf;UID=svcf_admin;PWD=5buo0e0i495Cpnn;Allow Zero Datetime=true;Convert Zero Datetime=True;port=3306"
                                ProviderName="MySql.Data.MySqlClient"></asp:SqlDataSource>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>