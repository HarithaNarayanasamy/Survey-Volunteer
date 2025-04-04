<%@ Page Culture="en-GB" Language="C#" MasterPageFile="~/Branch.Master" AutoEventWireup="true"
    CodeBehind="Booklet.aspx.cs" Inherits="SreeVisalamChitFundLtd_phase1.Booklet"
    Title="SVCF Admin Panel" %>



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
    <ajax:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </ajax:ToolkitScriptManager>
    <div class="row">
        <div class="twelve columns">
            <div class="box_c">
                <div class="box_c_heading cf">
                    <p class="sepV_a">
                        Booklet</p>
                </div>
                <div class="box_c_content">
                    <div class="row">
                        <div style="margin-top: -0.5em; margin-bottom: 0.6em;">
                            <asp:Panel ID="Panel1" runat="server" DefaultButton="BtnStatisticsGo">
                                <div style="display: table-cell; vertical-align: top; padding-top: 7px; padding-right: 5px !important;">
                                    <asp:Label style="font-family:Times New Roman;" Font-Size="Medium" ID="Label1" runat="server" Text="From Date :"></asp:Label>
                                </div>
                                <div style="display: table-cell; vertical-align: top; padding-top: 5px; padding-right: 5px !important;">
                                    <asp:TextBox style="font-family:Times New Roman;font-size:small;" TabIndex="1" CssClass="input-text maskdate" placeholder="From Date"
                                        Width="80px" ID="txtFromDate1" runat="server">
                                    </asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="Dynamic"
                                        ValidationGroup="directbranch1" ControlToValidate="txtFromDate1"></asp:RequiredFieldValidator>
                                    <asp:CompareValidator runat="server" Display="Dynamic" Operator="DataTypeCheck" Type="Date" ValidationGroup="directbranch1" ControlToValidate="txtFromDate1"></asp:CompareValidator>
                                </div>
                                <div style="display: table-cell; vertical-align: top; padding-top: 7px; padding-right: 5px !important;">
                                    <asp:Label  style="font-family:Times New Roman;" Font-Size="Medium" ID="Label2" runat="server" Text="To Date :"></asp:Label>
                                </div>
                                <div style="display: table-cell; vertical-align: top; padding-top: 5px; padding-right: 5px !important;">
                                    <asp:TextBox style="font-family:Times New Roman;font-size:small;" TabIndex="2" placeholder="To Date" CssClass="input-text maskdate" Width="80px"
                                        ID="txtToDate1" runat="server">
                                    </asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" Display="Dynamic"
                                        ValidationGroup="directbranch1" ControlToValidate="txtToDate1"></asp:RequiredFieldValidator>
                                    <asp:CompareValidator ID="CompareValidator1" ControlToCompare="txtFromDate1"  Operator="GreaterThanEqual" Type="Date" runat="server" Display="Dynamic" ValidationGroup="directbranch1" ControlToValidate="txtToDate1"></asp:CompareValidator>
                                </div>
                                
                                  
                                <div style="display: table-cell; vertical-align: top; padding-top: 2px; padding-right: 5px !important;">
                                    <asp:Button style="font-family:Times New Roman;" TabIndex="4" ValidationGroup="directbranch1" CssClass="GreenyPushButton"
                                        ID="BtnStatisticsGo" Font-Bold="true" runat="server" class="btn" OnClick="BtnStatisticsGo_Click1"
                                        Text="Load"></asp:Button>
                                </div>
                                <div style="display: table-cell; vertical-align: top; padding-top: 10px; padding-right: 5px !important;">
                                    <asp:CheckBox style="font-family:Times New Roman;font-size:small;" TabIndex="3" ID="aaaaa" AutoPostBack="true" OnCheckedChanged="oncheck_load" Text="Group"
                                        runat="server"></asp:CheckBox>
                                </div>
                                <div style="display: table-cell; vertical-align: top; padding-top: 10px; padding-right: 5px !important;">
                                  <asp:CheckBox ID="chkLoadKasr" Text="Load with kasar" runat="server" AutoPostBack="true" OnCheckedChanged="chkLoadKasr_CheckedChanged" />
                                  </div>
                                <div style="display: table-cell; vertical-align: top; float: right; padding-right: 5px;
                                    text-align: right; margin-top: -35px;">
                                    <dx:ASPxMenu TabIndex="5" OnItemClick="Export_click" ID="ASPxMenu1" runat="server" AllowSelectItem="True"
                                        ShowPopOutImages="True">
                                        <Items>
                                            <dx:MenuItem Text="Export">
                                                <Items>
                                                    <dx:MenuItem Text="PDF">
                                                    </dx:MenuItem>
                                                    <dx:MenuItem Text="XLSX">
                                                    </dx:MenuItem>
                                                </Items>
                                            </dx:MenuItem>
                                        </Items>
                                    </dx:ASPxMenu>
                                </div>
                            </asp:Panel>
                        </div>
                        <dx:ASPxGridView OnCustomColumnSort="gridCustomers_CustomColumnSort" OnCustomSummaryCalculate="ASPxGridView1_OnCustomSummaryCalculate"
                            Style="margin: 0 auto;" Width="100%" OnSummaryDisplayText="ASPxGridView1_SummaryDisplayText"
                            ID="grid" ClientInstanceName="grid" runat="server" DataSourceID="AccessDataSource1"
                            OnCustomCallback="grid_CustomCallback">
                            <Settings GroupFormat="{1}{2}" ShowTitlePanel="true" ShowHeaderFilterButton="true" ShowFooter="true" ShowGroupFooter="VisibleAlways"
                                ShowGroupPanel="false" ShowGroupedColumns="true" ShowFilterBar="Visible" ShowFilterRow="true" />
                            <Styles Cell-HorizontalAlign="Left" Header-VerticalAlign="Middle" Header-HorizontalAlign="Center"
                                Header-Wrap="True">
                            </Styles>
                            <SettingsText Title="Booklet" />
                            <GroupSummary>
                                <dx:ASPxSummaryItem FieldName="Credit" SummaryType="Sum" />
                                <dx:ASPxSummaryItem FieldName="Debit" SummaryType="Sum" />
                                <dx:ASPxSummaryItem FieldName="Date" ShowInGroupFooterColumn="Debit" SummaryType="Custom" />
                                
                                <dx:ASPxSummaryItem FieldName="LedgerHead1" ShowInGroupFooterColumn="Credit" SummaryType="Custom" />
                                <dx:ASPxSummaryItem FieldName="LedgerHead2" ShowInGroupFooterColumn="Credit" SummaryType="Custom" />
                                <dx:ASPxSummaryItem FieldName="LedgerHead3" ShowInGroupFooterColumn="Credit" SummaryType="Custom" />
                                <dx:ASPxSummaryItem FieldName="LedgerHead4" ShowInGroupFooterColumn="Credit" SummaryType="Custom" />
                            </GroupSummary>
                            <TotalSummary>
                                <dx:ASPxSummaryItem FieldName="Credit" SummaryType="Custom" />
                                <dx:ASPxSummaryItem FieldName="Debit" SummaryType="Custom" />
                                <dx:ASPxSummaryItem FieldName="Narration" SummaryType="Custom" />
                            </TotalSummary>
                            <SettingsPager Mode="ShowAllRecords">
                            </SettingsPager>
                            <Columns>
                                <dx:GridViewDataColumn Visible="false" FieldName="RootID">
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="Date">
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn Visible="false" FieldName="Node" Caption="Head">
                                <Settings SortMode="Custom"/>
                                </dx:GridViewDataColumn>
                                                               <dx:GridViewDataColumn FieldName="LedgerHead1">
                                </dx:GridViewDataColumn>
                                                                <dx:GridViewDataColumn FieldName="LedgerHead2">
                                </dx:GridViewDataColumn>
                                                                <dx:GridViewDataColumn FieldName="LedgerHead3">
                                </dx:GridViewDataColumn>
                                                                <dx:GridViewDataColumn FieldName="LedgerHead4">
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="Narration">
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="Credit">
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="Debit">
                                </dx:GridViewDataColumn>
                            </Columns>
                            <Styles GroupFooter-HorizontalAlign="Left" Header-VerticalAlign="Middle" Header-HorizontalAlign="Center" Cell-HorizontalAlign="Left" Footer-HorizontalAlign="Left">
                                <GroupPanel Wrap="True" HorizontalAlign="Left">
                                </GroupPanel>
                                <GroupRow Wrap="True" HorizontalAlign="Left">
                                </GroupRow>
                            </Styles>
                        </dx:ASPxGridView>
                        <dx:ASPxGridViewExporter ID="gridExport" runat="server" GridViewID="grid" ReportHeader="Center">
                        <Styles>
                        <Cell HorizontalAlign="Left"></Cell>
                        <Header HorizontalAlign="Center"></Header>
                        <Footer HorizontalAlign="Left"></Footer>
                        <GroupFooter HorizontalAlign="Left"></GroupFooter>
                        </Styles>
                        </dx:ASPxGridViewExporter>
                      <%--  <asp:SqlDataSource runat="server" ID="AccessDataSource1" ConnectionString="server=183.82.250.137;database=svcf;pwd=sqlaltius;UID=root;Allow Zero Datetime=true;port=3306"
                            ProviderName="MySql.Data.MySqlClient" SelectCommand=" " />--%>
                          <asp:SqlDataSource runat="server" ID="AccessDataSource1" 
                            ProviderName="MySql.Data.MySqlClient" SelectCommand=" " />
                         

                        <dx:ASPxGridView OnCustomColumnSort="gridPalCustomers_CustomColumnSort" OnCustomSummaryCalculate="gridPalCustomers_OnCustomSummaryCalculate"
                            Style="margin: 0 auto;" Width="100%" OnSummaryDisplayText="gridPalCustomers_SummaryDisplayText"
                            ID="gridPalCustomers" ClientInstanceName="gridPalCustomers" runat="server" DataSourceID="AccessDataSource2"
                            OnCustomCallback="gridPalCustomers_CustomCallback">
                            <Settings GroupFormat="{1}{2}" ShowTitlePanel="true" ShowHeaderFilterButton="true" ShowFooter="true" ShowGroupFooter="VisibleAlways"
                                ShowGroupPanel="false" ShowGroupedColumns="true" ShowFilterBar="Visible" ShowFilterRow="true" />
                            <Styles Cell-HorizontalAlign="Left" Header-VerticalAlign="Middle" Header-HorizontalAlign="Center"
                                Header-Wrap="True">
                            </Styles>
                            <SettingsText Title="Booklet - Pallathur I" />
                            <GroupSummary>
                                <dx:ASPxSummaryItem FieldName="Credit" SummaryType="Sum" />
                                <dx:ASPxSummaryItem FieldName="Debit" SummaryType="Sum" />
                                <dx:ASPxSummaryItem FieldName="Date" ShowInGroupFooterColumn="Debit" SummaryType="Custom" />
                                <dx:ASPxSummaryItem FieldName="LedgerHead" ShowInGroupFooterColumn="Credit" SummaryType="Custom" />
                            </GroupSummary>
                            <TotalSummary>
                                <dx:ASPxSummaryItem FieldName="Credit" SummaryType="Custom" />
                                <dx:ASPxSummaryItem FieldName="Debit" SummaryType="Custom" />
                                <dx:ASPxSummaryItem FieldName="Narration" SummaryType="Custom" />
                            </TotalSummary>
                            <SettingsPager Mode="ShowAllRecords">
                            </SettingsPager>
                            <Columns>
                                <dx:GridViewDataColumn Visible="false" FieldName="RootID">
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="Date">
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn Visible="false" FieldName="Node" Caption="Head">
                                <Settings SortMode="Custom"/>
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="LedgerHead">
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="Narration">
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="Credit">
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="Debit">
                                </dx:GridViewDataColumn>
                            </Columns>
                            <Styles GroupFooter-HorizontalAlign="Left" Header-VerticalAlign="Middle" Header-HorizontalAlign="Center" Cell-HorizontalAlign="Left" Footer-HorizontalAlign="Left">
                                <GroupPanel Wrap="True" HorizontalAlign="Left">
                                </GroupPanel>
                                <GroupRow Wrap="True" HorizontalAlign="Left">
                                </GroupRow>
                            </Styles>
                        </dx:ASPxGridView>
                        <dx:ASPxGridViewExporter ID="gridPalCustomersExporter" runat="server" GridViewID="gridPalCustomers" ReportHeader="Center">
                        <Styles>
                        <Cell HorizontalAlign="Left"></Cell>
                        <Header HorizontalAlign="Center"></Header>
                        <Footer HorizontalAlign="Left"></Footer>
                        <GroupFooter HorizontalAlign="Left"></GroupFooter>
                        </Styles>
                        </dx:ASPxGridViewExporter>
                   <%--     <asp:SqlDataSource runat="server" ID="AccessDataSource2" ConnectionString="server=183.82.250.137;database=svcf;pwd=sqlaltius;UID=root;Allow Zero Datetime=true;port=3306"
                            ProviderName="MySql.Data.MySqlClient" SelectCommand=" " />--%>
                             <asp:SqlDataSource runat="server" ID="AccessDataSource2" 
                            ProviderName="MySql.Data.MySqlClient" SelectCommand=" " />

                        <dx:ASPxGridView OnCustomColumnSort="gridPal2Customers_CustomColumnSort" OnCustomSummaryCalculate="gridPal2Customers_OnCustomSummaryCalculate"
                            Style="margin: 0 auto;" Width="100%" OnSummaryDisplayText="gridPal2Customers_SummaryDisplayText"
                            ID="gridPal2Customers" ClientInstanceName="gridPal2Customers" runat="server" DataSourceID="AccessDataSource3"
                            OnCustomCallback="gridPal2Customers_CustomCallback">
                            <Settings GroupFormat="{1}{2}" ShowTitlePanel="true" ShowHeaderFilterButton="true" ShowFooter="true" ShowGroupFooter="VisibleAlways"
                                ShowGroupPanel="false" ShowGroupedColumns="true" ShowFilterBar="Visible" ShowFilterRow="true" />
                            <Styles Cell-HorizontalAlign="Left" Header-VerticalAlign="Middle" Header-HorizontalAlign="Center"
                                Header-Wrap="True">
                            </Styles>
                            <SettingsText Title="Booklet - Pallathur II" />
                            <GroupSummary>
                                <dx:ASPxSummaryItem FieldName="Credit" SummaryType="Sum" />
                                <dx:ASPxSummaryItem FieldName="Debit" SummaryType="Sum" />
                                <dx:ASPxSummaryItem FieldName="Date" ShowInGroupFooterColumn="Debit" SummaryType="Custom" />
                                <dx:ASPxSummaryItem FieldName="LedgerHead1" ShowInGroupFooterColumn="Credit" SummaryType="Custom" />
                                                                <dx:ASPxSummaryItem FieldName="LedgerHead2" ShowInGroupFooterColumn="Credit" SummaryType="Custom" />
                                                                <dx:ASPxSummaryItem FieldName="LedgerHead3" ShowInGroupFooterColumn="Credit" SummaryType="Custom" />
                                                                <dx:ASPxSummaryItem FieldName="LedgerHead4" ShowInGroupFooterColumn="Credit" SummaryType="Custom" />
                            </GroupSummary>
                            <TotalSummary>
                                <dx:ASPxSummaryItem FieldName="Credit" SummaryType="Custom" />
                                <dx:ASPxSummaryItem FieldName="Debit" SummaryType="Custom" />
                                <dx:ASPxSummaryItem FieldName="Narration" SummaryType="Custom" />
                            </TotalSummary>
                            <SettingsPager Mode="ShowAllRecords">
                            </SettingsPager>
                            <Columns>
                                <dx:GridViewDataColumn Visible="false" FieldName="RootID">
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="Date">
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn Visible="false" FieldName="Node" Caption="Head">
                                <Settings SortMode="Custom"/>
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="LedgerHead1">
                                </dx:GridViewDataColumn>
                                                                <dx:GridViewDataColumn FieldName="LedgerHead2">
                                </dx:GridViewDataColumn>
                                                                <dx:GridViewDataColumn FieldName="LedgerHead3">
                                </dx:GridViewDataColumn>
                                                                <dx:GridViewDataColumn FieldName="LedgerHead4">
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="Narration">
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="Credit">
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="Debit">
                                </dx:GridViewDataColumn>
                            </Columns>
                            <Styles GroupFooter-HorizontalAlign="Left" Header-VerticalAlign="Middle" Header-HorizontalAlign="Center" Cell-HorizontalAlign="Left" Footer-HorizontalAlign="Left">
                                <GroupPanel Wrap="True" HorizontalAlign="Left">
                                </GroupPanel>
                                <GroupRow Wrap="True" HorizontalAlign="Left">
                                </GroupRow>
                            </Styles>
                        </dx:ASPxGridView>
                        <dx:ASPxGridViewExporter ID="gridPal2CustomersExporter" runat="server" GridViewID="gridPal2Customers" ReportHeader="Center">
                        <Styles>
                        <Cell HorizontalAlign="Left"></Cell>
                        <Header HorizontalAlign="Center"></Header>
                        <Footer HorizontalAlign="Left"></Footer>
                        <GroupFooter HorizontalAlign="Left"></GroupFooter>
                        </Styles>
                        </dx:ASPxGridViewExporter>
                  <%--      <asp:SqlDataSource runat="server" ID="AccessDataSource3" ConnectionString="server=183.82.250.137;database=svcf;pwd=sqlaltius;UID=root;Allow Zero Datetime=true;port=3306"
                            ProviderName="MySql.Data.MySqlClient" SelectCommand=" " />--%>
                              <asp:SqlDataSource runat="server" ID="AccessDataSource3" 
                            ProviderName="MySql.Data.MySqlClient" SelectCommand=" " />

                        <dx:ASPxGridView OnCustomColumnSort="gridPal3Customers_CustomColumnSort" OnCustomSummaryCalculate="gridPal3Customers_OnCustomSummaryCalculate"
                            Style="margin: 0 auto;" Width="100%" OnSummaryDisplayText="gridPal3Customers_SummaryDisplayText"
                            ID="gridPal3Customers" ClientInstanceName="gridPal3Customers" runat="server" DataSourceID="AccessDataSource4"
                            OnCustomCallback="gridPal3Customers_CustomCallback">
                            <Settings GroupFormat="{1}{2}" ShowTitlePanel="true" ShowHeaderFilterButton="true" ShowFooter="true" ShowGroupFooter="VisibleAlways"
                                ShowGroupPanel="false" ShowGroupedColumns="true" ShowFilterBar="Visible" ShowFilterRow="true" />
                            <Styles Cell-HorizontalAlign="Left" Header-VerticalAlign="Middle" Header-HorizontalAlign="Center"
                                Header-Wrap="True">
                            </Styles>
                            <SettingsText Title="Booklet - Pallathur III" />
                            <GroupSummary>
                                <dx:ASPxSummaryItem FieldName="Credit" SummaryType="Sum" />
                                <dx:ASPxSummaryItem FieldName="Debit" SummaryType="Sum" />
                                <dx:ASPxSummaryItem FieldName="Date" ShowInGroupFooterColumn="Debit" SummaryType="Custom" />
                                <dx:ASPxSummaryItem FieldName="LedgerHead" ShowInGroupFooterColumn="Credit" SummaryType="Custom" />
                            </GroupSummary>
                            <TotalSummary>
                                <dx:ASPxSummaryItem FieldName="Credit" SummaryType="Custom" />
                                <dx:ASPxSummaryItem FieldName="Debit" SummaryType="Custom" />
                                <dx:ASPxSummaryItem FieldName="Narration" SummaryType="Custom" />
                            </TotalSummary>
                            <SettingsPager Mode="ShowAllRecords">
                            </SettingsPager>
                            <Columns>
                                <dx:GridViewDataColumn Visible="false" FieldName="RootID">
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="Date">
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn Visible="false" FieldName="Node" Caption="Head">
                                <Settings SortMode="Custom"/>
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="LedgerHead">
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="Narration">
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="Credit">
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="Debit">
                                </dx:GridViewDataColumn>
                            </Columns>
                            <Styles GroupFooter-HorizontalAlign="Left" Header-VerticalAlign="Middle" Header-HorizontalAlign="Center" Cell-HorizontalAlign="Left" Footer-HorizontalAlign="Left">
                                <GroupPanel Wrap="True" HorizontalAlign="Left">
                                </GroupPanel>
                                <GroupRow Wrap="True" HorizontalAlign="Left">
                                </GroupRow>
                            </Styles>
                        </dx:ASPxGridView>
                        <dx:ASPxGridViewExporter ID="gridPal3CustomersExporter" runat="server" GridViewID="gridPal3Customers" ReportHeader="Center">
                        <Styles>
                        <Cell HorizontalAlign="Left"></Cell>
                        <Header HorizontalAlign="Center"></Header>
                        <Footer HorizontalAlign="Left"></Footer>
                        <GroupFooter HorizontalAlign="Left"></GroupFooter>
                        </Styles>
                        </dx:ASPxGridViewExporter>
                       <%-- <asp:SqlDataSource runat="server" ID="AccessDataSource4" ConnectionString="server=183.82.250.137;database=svcf;pwd=sqlaltius;UID=root;Allow Zero Datetime=true;port=3306;Max Pool Size=200;"
                            ProviderName="MySql.Data.MySqlClient" SelectCommand=" " />--%>
                         <asp:SqlDataSource runat="server" ID="AccessDataSource4" 
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
