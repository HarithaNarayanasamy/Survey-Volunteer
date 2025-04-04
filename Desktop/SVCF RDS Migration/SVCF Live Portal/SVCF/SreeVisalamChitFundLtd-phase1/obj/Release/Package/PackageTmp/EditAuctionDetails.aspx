<%@ Page Title="SVCF Admin Panel" Culture="en-GB" Language="C#" MasterPageFile="~/Branch.Master"
    AutoEventWireup="true" CodeBehind="EditAuctionDetails.aspx.cs" Inherits="SreeVisalamChitFundLtd_phase1.EditAuctionDetails" %>

<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxTabControl" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxMenu" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1.Export, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView.Export" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxClasses" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>SVCF Admin Panel</title>
    <!-- Foundation framework -->
    <link rel="stylesheet" href="pertho_admin_v1.3/foundation/stylesheets/foundation.css" />
    <!-- jquery UI -->
    <link rel="stylesheet" href="pertho_admin_v1.3/lib/jQueryUI/css/Aristo/Aristo.css"
        media="all" />
    <!-- fancybox -->
    <link rel="stylesheet" href="pertho_admin_v1.3/lib/fancybox/jquery.fancybox-1.3.4.css"
        media="all" />
    <!-- syntax highlighter -->
    <link rel="stylesheet" href="pertho_admin_v1.3/lib/syntaxhighlighter/styles/shCoreDefault.css" />
    <!-- main styles -->
    <link rel="stylesheet" href="pertho_admin_v1.3/css/style.css" />
    <!-- Google fonts -->
    <link href="pertho_admin_v1.3/lib/colorpicker/css/colorpicker.css" rel="stylesheet"
        type="text/css" />
    <style type="text/css">
        div[style="padding: 2px; position: absolute; left: 1px; top: 1px; z-index: 100000; font-family: sans-serif; font-size: 8pt; color: black; background-color: white;"]
        {
            display: none;
        }
        .dxic
        {
            padding: 0px;
            width: 100%;
            }
        .dxeSBC
        {
            vertical-align: top;
        }
        .dxeEditArea
        {
            height: 20px !important;
        }
        
        .dxeEditAreaSys
        {
            height: 20px !important;
        }
        td[style="cursor:default;"]
        {
            vertical-align: middle;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMainContent" runat="server">
    <ajax:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </ajax:ToolkitScriptManager>
    <div class="row" id="Panel1">
        <div class="twelve columns">
            <div class="box_c">
                <div class="box_c_heading box_actions">
                    <p>
                        Edit Auction Details</p>
                </div>
                <div class="box_c_content">
                    <div style="width: 100%; margin: 0 auto;">
                        <dx:ASPxGridView ID="gridBranch" ClientInstanceName="grid"
                            runat="server" DataSourceID="DataSourceBranch" KeyFieldName="inccolumn" AutoGenerateColumns="False"
                            EnableRowsCache="False" OnStartRowEditing="gridBranch_StartRowEditing" Width="100%" 
                            OnRowUpdating="gridBranch_RowUpdating" OnInitNewRow="gridBranch_InitNewRow" OnParseValue="gridBranch_ParseValue">

                            <Columns>
                                <dx:GridViewCommandColumn Caption="Action" ButtonType="Image" VisibleIndex="0">
                                    <EditButton Visible="True" Text="Edit" Image-Url="Styles/Image/Edit16.png" />
                                </dx:GridViewCommandColumn>
                                <dx:GridViewDataTextColumn FieldName="inccolumn" Visible="false" Caption="inccolumn" ></dx:GridViewDataTextColumn>                               
                                <dx:GridViewDataTextColumn FieldName="GroupID" Visible="false" />
                                <dx:GridViewDataTextColumn FieldName="AuctionDate" Caption="Auction Date" />
                                <dx:GridViewDataTextColumn FieldName="DrawNO" Caption="Draw Number" />
                                <dx:GridViewDataTextColumn FieldName="PrizedMemberID" Visible="false" />
                                <dx:GridViewDataTextColumn FieldName="PrizedMemberName" Caption="Prized Member Name" />
                                <dx:GridViewDataTextColumn FieldName="MemberID" Visible="false" />
                                <dx:GridViewDataTextColumn FieldName="CustomerName" Caption="Customer Name" />
                                <dx:GridViewDataTextColumn FieldName="PrizedAmount" Caption="Prized Amount" />
                                <dx:GridViewDataTextColumn FieldName="TotalCommission" Caption="Total Commission" />
                                <dx:GridViewDataTextColumn FieldName="Dividend" Caption="Dividend" />
                                <dx:GridViewDataTextColumn FieldName="KasarAmount" Caption="Kasar Amount" />
                                <dx:GridViewDataTextColumn FieldName="CurrentDueAmount" Caption="Current Due Amount" />
                                <dx:GridViewDataTextColumn FieldName="NextDueAmount" Caption="Next Due Amount" />
                                <dx:GridViewDataTextColumn FieldName="AdditionalKasarAmount" Caption="Additional Kasar Amount" />
                                <dx:GridViewDataTextColumn FieldName="IsPrized" Caption="IsPrized" />
                                <dx:GridViewDataTextColumn FieldName="IsReAuction" Caption="IsReAuction" />
                            </Columns>
                            <Settings ShowHorizontalScrollBar="true" ShowTitlePanel="true" ShowHeaderFilterButton="true"
                                ShowFilterRow="true" />
                            <SettingsPager Mode="ShowPager" Position="TopAndBottom" />
                            <Styles Cell-HorizontalAlign="Left" Header-Wrap="True" CommandColumn-Paddings-Padding="10"
                                Header-HorizontalAlign="Center" Header-VerticalAlign="Middle">
                            </Styles>
                            <SettingsBehavior ConfirmDelete="true" />
                            <SettingsText Title="Edit Auction Details" ConfirmDelete="Are You Sure You Want To Delete Bank?" />
                            <Templates>
                                <EditForm>
                                      <table style="width: 50%; margin: 0 auto;">
                                        <tr>
                                            <td>
                                                <dx:ASPxLabel ID="lbAuctionDate" runat="server" Text="Auction Date">
                                                </dx:ASPxLabel>
                                            </td>
                                            <td style="padding-right: 5px;">
                                                <dx:ASPxDateEdit EditFormat="Date" EditFormatString="dd/MM/yyyy" UseMaskBehavior="true"
                                                    runat="server" ID="AuctionDate" Text='<%# Bind("AuctionDate") %>' Width="100%"
                                                    ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                                    <ValidationSettings Display="Dynamic">
                                                        <RequiredField IsRequired="true" />
                                                    </ValidationSettings>
                                                </dx:ASPxDateEdit>
                                            </td>
                                              <td>
                                                <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="Prized Amount">
                                                </dx:ASPxLabel>
                                            </td>
                                            <td>
                                                 <dx:ASPxMemo Height="60px" runat="server" ID="PrizedAmount" Text='<%# Bind("PrizedAmount") %>'
                                                        Width="100%" ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                                        <ValidationSettings Display="Dynamic">
                                                            <RequiredField IsRequired="false" />
                                                        </ValidationSettings>
                                                    </dx:ASPxMemo>
                                                    </ValidationSettings>
                                            </td>

                                        </tr>
                                        <tr>
                                            <td>
                                                 <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="Total Commission">
                                                </dx:ASPxLabel>
                                            </td>
                                            <td>
                                                 
                                                 <dx:ASPxMemo Height="60px" runat="server" ID="TotalCommission" Text='<%# Bind("TotalCommission") %>'
                                                        Width="100%" ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                                        <ValidationSettings Display="Dynamic">
                                                            <RequiredField IsRequired="false" />
                                                        </ValidationSettings>
                                                    </dx:ASPxMemo>
                                                    </ValidationSettings>
                                           
                                            </td>
                                            <td>
                                                 <dx:ASPxLabel ID="ASPxLabel3" runat="server" Text="Dividend">
                                                </dx:ASPxLabel>
                                            </td>
                                            <td>
                                                  <dx:ASPxMemo Height="60px" runat="server" ID="Dividend" Text='<%# Bind("Dividend") %>'
                                                        Width="100%" ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                                        <ValidationSettings Display="Dynamic">
                                                            <RequiredField IsRequired="false" />
                                                        </ValidationSettings>
                                                    </dx:ASPxMemo>
                                                    </ValidationSettings>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                 <dx:ASPxLabel ID="ASPxLabel4" runat="server" Text="Kasar Amount">
                                                </dx:ASPxLabel>
                                            </td>
                                            <td>
                                                 <dx:ASPxMemo Height="60px" runat="server" ID="KasarAmount" Text='<%# Bind("KasarAmount") %>'
                                                        Width="100%" ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                                        <ValidationSettings Display="Dynamic">
                                                            <RequiredField IsRequired="false" />
                                                        </ValidationSettings>
                                                    </dx:ASPxMemo>
                                                    </ValidationSettings>
                                            </td>
                                            <td>
                                                <dx:ASPxLabel ID="ASPxLabel5" runat="server" Text="Current Due Amount">
                                                </dx:ASPxLabel>
                                            </td>
                                            <td>
                                                  <dx:ASPxMemo Height="60px" runat="server" ID="CurrentDueAmount" Text='<%# Bind("CurrentDueAmount") %>'
                                                        Width="100%" ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                                        <ValidationSettings Display="Dynamic">
                                                            <RequiredField IsRequired="false" />
                                                        </ValidationSettings>
                                                    </dx:ASPxMemo>
                                                    </ValidationSettings>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                 <dx:ASPxLabel ID="ASPxLabel6" runat="server" Text="Next Due Amount">
                                                </dx:ASPxLabel>
                                            </td>
                                            <td>
                                                <dx:ASPxMemo Height="60px" runat="server" ID="NextDueAmount" Text='<%# Bind("NextDueAmount") %>'
                                                        Width="100%" ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                                        <ValidationSettings Display="Dynamic">
                                                            <RequiredField IsRequired="false" />
                                                        </ValidationSettings>
                                                    </dx:ASPxMemo>
                                                    </ValidationSettings>
                                            </td>
                                              <td>
                                                <dx:ASPxLabel ID="ASPxLabel7" runat="server" Text="Prized Member">
                                                </dx:ASPxLabel>
                                            </td>
                                            <td>
                                                <dx:ASPxComboBox runat="server" id="PrizedMember" DropDownStyle="DropDown" OnDataBound="PrizedMember_DataBound" Value='<%# Bind("PrizedMemberID") %>' 
                                                    TextField="PrizedMemberName" ValueField="PrizedMemberID" OnInit="PrizedMember_Init" Width="100%">
                                                    <ValidationSettings Display="Dynamic">
                                                        <RequiredField IsRequired="true" />
                                                    </ValidationSettings>
                                                </dx:ASPxComboBox>

                                            </td>
                                        </tr>
                                    </table>

                                    <div style="text-align: right; padding-left: 2px; padding-top: 5px; float: left;">
                                        <dx:ASPxGridViewTemplateReplacement ID="UpdateButton" ReplacementType="EditFormUpdateButton"
                                            runat="server"></dx:ASPxGridViewTemplateReplacement>
                                        <dx:ASPxGridViewTemplateReplacement ID="CancelButton" ReplacementType="EditFormCancelButton"
                                            runat="server"></dx:ASPxGridViewTemplateReplacement>
                                    </div>
                                </EditForm>
                            </Templates>
                        </dx:ASPxGridView>
                       <%-- <asp:SqlDataSource runat="server" ID="SqlDataSource1" ConnectionString="server=db.sreevisalam.internal;database=svcf;UID=svcf_admin;PWD=5buo0e0i495Cpnn;Allow Zero Datetime=true;Convert Zero Datetime=True;port=3306;"
                        ProviderName="MySql.Data.MySqlClient" /> --%> 
                        <asp:SqlDataSource runat="server" ID="DataSourceBranch" 
                            ProviderName="MySql.Data.MySqlClient"></asp:SqlDataSource>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
