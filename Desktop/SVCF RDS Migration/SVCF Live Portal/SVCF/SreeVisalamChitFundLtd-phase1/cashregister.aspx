<%@ Page Culture="en-GB" Title="SVCF Admin Panel" Language="C#" MasterPageFile="~/Branch.Master"
    AutoEventWireup="true" CodeBehind="cashregister.aspx.cs" Inherits="SreeVisalamChitFundLtd_phase1.cashregister" %>

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
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <!-- Foundation framework -->
    <link rel="stylesheet" href="pertho_admin_v1.3/foundation/stylesheets/foundation.css" />
    <!-- jquery UI -->
    <link rel="stylesheet" href="pertho_admin_v1.3/lib/jQueryUI/css/Aristo/Aristo.css"
        media="all" />
    <!-- fancybox -->
    <link rel="stylesheet" href="pertho_admin_v1.3/lib/fancybox/jquery.fancybox-1.3.4.css"
        media="all" />
    <link rel="stylesheet" href="pertho_admin_v1.3/css/style.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMainContent" runat="server">
    <div class="row">
        <div class="twelve columns">
            <div class="box_c">
                <div class="box_c_heading cf">
                    <p class="sepV_a">
                        Cash Register</p>
                </div>
                <div class="box_c_content">
                    <div class="row">
                        <div style="margin-top: -0.5em; margin-bottom: 0.6em;">
                            <asp:Panel ID="Panel1" runat="server" DefaultButton="BtnStatisticsGo">
                                <div style="display: table-cell; vertical-align: top; padding-top: 7px; padding-right: 5px !important;">
                                    <asp:Label Style="font-family: Times New Roman;" Font-Size="Medium" ID="Label1" runat="server"
                                        Text="From Date :"></asp:Label>
                                </div>
                                <div style="display: table-cell; vertical-align: top; padding-top: 5px; padding-right: 5px !important;">
                                    <asp:TextBox Style="font-family: Times New Roman; font-size: small;" TabIndex="1"
                                        CssClass="input-text maskdate" placeholder="From Date" Width="80px" ID="txtFromDate1"
                                        runat="server">
                                    </asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="Dynamic"
                                        ValidationGroup="directbranch1" ControlToValidate="txtFromDate1"></asp:RequiredFieldValidator>
                                    <asp:CompareValidator ID="CompareValidator1" runat="server" Display="Dynamic" Operator="DataTypeCheck"
                                        Type="Date" ValidationGroup="directbranch1" ControlToValidate="txtFromDate1"></asp:CompareValidator>
                                </div>
                                <div style="display: table-cell; vertical-align: top; padding-top: 7px; padding-right: 5px !important;">
                                    <asp:Label Style="font-family: Times New Roman;" Font-Size="Medium" ID="Label2" runat="server"
                                        Text="To Date :"></asp:Label>
                                </div>
                                <div style="display: table-cell; vertical-align: top; padding-top: 5px; padding-right: 5px !important;">
                                    <asp:TextBox Style="font-family: Times New Roman; font-size: small;" TabIndex="2"
                                        placeholder="To Date" CssClass="input-text maskdate" Width="80px" ID="txtToDate1"
                                        runat="server">
                                    </asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" Display="Dynamic"
                                        ValidationGroup="directbranch1" ControlToValidate="txtToDate1"></asp:RequiredFieldValidator>
                                    <asp:CompareValidator ID="CompareValidator2" ControlToCompare="txtFromDate1" Operator="GreaterThanEqual"
                                        Type="Date" runat="server" Display="Dynamic" ValidationGroup="directbranch1"
                                        ControlToValidate="txtToDate1"></asp:CompareValidator>
                                </div>
                                <div style="display: table-cell; vertical-align: top; padding-top: 2px; padding-right: 5px !important;">
                                    <asp:Button Style="font-family: Times New Roman;" TabIndex="4" ValidationGroup="directbranch1"
                                        CssClass="GreenyPushButton" ID="BtnStatisticsGo" Font-Bold="true" runat="server"
                                        class="btn" OnClick="BtnStatisticsGo_Click1" Text="Load"></asp:Button>
                                </div>
                                <div style="display: table-cell; float: right; padding-right: 5px; text-align: right;
                                             margin-top: -35px;">
                                        <dx:ASPxMenu OnItemClick="Export_click" ID="mMain" runat="server" AllowSelectItem="True"
                                            ShowPopOutImages="True">
                                        <Items>
                                            <dx:MenuItem Text="Export">
                                                <Items>
                                                    <dx:MenuItem Text="PDF">
                                                    </dx:MenuItem>
                                        <%--<dx:MenuItem Text="XLSX">
                                        </dx:MenuItem>--%>
                                                </Items>
                                        </dx:MenuItem>
                                        </Items>
                                        </dx:ASPxMenu>
                                        </div>
                            </asp:Panel>
                        </div>
                        <dx:ASPxGridView Style="margin: 0 auto;" ID="grid" ClientInstanceName="grid" runat="server"
                            DataSourceID="AccessDataSource1" Width="100%" AutoGenerateColumns="true">
                            <SettingsBehavior ProcessSelectionChangedOnServer="True" AllowGroup="false" AllowDragDrop="false" />
                            <SettingsPager Mode="ShowPager" PageSize="100">
                            </SettingsPager>
                            <Settings ShowFooter="true" ShowFilterBar="Visible" ShowFilterRowMenu="true" ShowHeaderFilterButton="true"
                                ShowFilterRow="true" />
                            <Styles Header-Wrap="True" Header-HorizontalAlign="Center" Header-VerticalAlign="Middle"></Styles>
                            <SettingsText Title="Cash Register" />
                            <GroupSummary>
                                <dx:ASPxSummaryItem FieldName="Credit" ShowInGroupFooterColumn="Credit" SummaryType="Sum" />
                                <dx:ASPxSummaryItem FieldName="Debit" ShowInGroupFooterColumn="Debit" SummaryType="Sum" />
                            </GroupSummary>
                            <TotalSummary>
                                <dx:ASPxSummaryItem DisplayFormat="{0}" FieldName="Credit" SummaryType="Sum" />
                                <dx:ASPxSummaryItem DisplayFormat="{0}"  FieldName="Debit" SummaryType="Sum" />
                            </TotalSummary>
                            <Columns>
                                <dx:GridViewDataColumn FieldName="ChoosenDate" Width="10%" Caption="Choosen Date">
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="Voucher_No" Width="10%" Caption="Voucher Number" />
                                <dx:GridViewDataColumn FieldName="Node" Width="30%" Caption="Heading" />
                                <dx:GridViewDataColumn FieldName="Credit" Width="20%" Caption="Credit" />
                                <dx:GridViewDataColumn FieldName="Debit" Width="20%" Caption="Debit" />
                                <dx:GridViewDataColumn Caption="If Necessary Signature of Payee or Remitter" Width="10%" />
                            </Columns>
                        </dx:ASPxGridView>
                         <dx:ASPxGridViewExporter ID="gridExport" runat="server" GridViewID="grid">
                                                    <Styles>
                                                        <Header Wrap="True" HorizontalAlign="Center">
                                                        </Header>
                                                        <Cell HorizontalAlign="Left" Wrap="True">
                                                        </Cell>
                                                        <Footer Wrap="True" HorizontalAlign="Left">
                                                        </Footer>
                                                    </Styles>
                                                </dx:ASPxGridViewExporter>
                        <asp:SqlDataSource runat="server" ID="AccessDataSource1" 
                            ProviderName="MySql.Data.MySqlClient" SelectCommand=" " />
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        $(document).ready(function () {
            $(".chzn-select").chosen({ search_contains: true });
            prth_mask_input.init();
        });
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $(".chzn-select").chosen({ search_contains: true });
        });
    </script>
</asp:Content>
