<%@ Page Title="SVCF - Admin Page" Culture="en-GB" Language="C#" MasterPageFile="~/Branch.Master"
    AutoEventWireup="true" CodeBehind="topcolumndaybookreport.aspx.cs" Inherits="SreeVisalamChitFundLtd_phase1.topcolumndaybookreport" %>

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
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGlobalEvents" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <style type="text/css">
        .chzn-results
        {
            text-align: center;
        }
        #ctl00_cphMainContent_ddlChit_chzn .chzn-drop .chzn-search input[type="text"]
        {
            height: 15px !important;
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
    <div class="box_c">
        <div class="box_c_heading cf">
            <p class="sepV_a">
                Day Book</p>
        </div>
        <div class="box_c_content">
            <div style="margin-top: -0.5em; margin-bottom: 0.6em;">
                <asp:Panel ID="Panel1" runat="server" DefaultButton="BtnStatisticsGo">
                    <div style="display: table-cell; padding-right: 5px !important;">
                        <asp:Label ID="Label2" runat="server" Text="From Date : "></asp:Label>
                    </div>
                    <div style="display: table-cell; padding-right: 5px;">
                        <asp:TextBox TabIndex="1" Width="100px" class="input-text maskdate" ID="txtFromDate"
                            runat="server" placeholder="From Date">
                        </asp:TextBox>
                        <asp:RequiredFieldValidator ValidationGroup="twelvehead" Display="Dynamic" ID="RequiredFieldValidator3"
                            runat="server" ControlToValidate="txtFromDate" ErrorMessage=""></asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="CompareValidator10" ValidationGroup="twelvehead" runat="server"
                            Display="Dynamic" ControlToValidate="txtFromDate" Operator="DataTypeCheck" Type="Date"></asp:CompareValidator>
                    </div>
                    <div style="display: table-cell; padding-right: 5px;">
                        <asp:Label ID="Label3" runat="server" Text="To Date : "></asp:Label>
                    </div>
                    <div style="display: table-cell; padding-right: 5px;">
                        <asp:TextBox TabIndex="2" Width="100px" class="input-text maskdate" ID="txtToDate"
                            placeholder="To Date" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ValidationGroup="twelvehead" Display="Dynamic" ID="RequiredFieldValidator1"
                            runat="server" ControlToValidate="txtToDate" ErrorMessage=""></asp:RequiredFieldValidator>
                        <asp:CompareValidator ValidationGroup="twelvehead" ID="CompareValidator11" ControlToValidate="txtToDate"
                            ControlToCompare="txtFromDate" Display="Dynamic" runat="server" Operator="GreaterThanEqual"
                            Type="Date"></asp:CompareValidator>
                    </div>
                    <div style="display: table-cell; padding-right: 5px; vertical-align: top;">
                        <asp:Button ValidationGroup="twelvehead" TabIndex="3" ID="BtnStatisticsGo" CssClass="GreenyPushButton"
                            runat="server" class="btn" OnClick="BtnStatisticsGo_Click" Text="Go!"></asp:Button>
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
                                        
                                    </Items>
                                </dx:MenuItem>
                            </Items>
                        </dx:ASPxMenu>
                    </div>
                </asp:Panel>
            </div>
            <dx:ASPxPageControl Width="100%" ID="carTabPage" TabAlign="Left" TabPosition="Top"
                ActivateTabPageAction="Click" runat="server" ActiveTabIndex="0" EnableHierarchyRecreation="True">
                <TabPages>
                    <dx:TabPage Text="Credit">
                        <ContentCollection>
                            <dx:ContentControl ID="ContentControl1" runat="server">
                                <div style="width: 100%; margin: 0px auto;">
                                    <dx:ASPxGridView Styles-AlternatingRow-BackColor="LightGray" Style="margin: 0 auto;
                                        width: 100%;" OnCustomUnboundColumnData="gridCredit_CustomUnboundColumnData"
                                        ID="gridCredit" ClientInstanceName="gridCredit" runat="server" DataSourceID="dsCredit">
                                        <Settings ShowFilterRowMenu="true" ShowHorizontalScrollBar="true" ShowTitlePanel="true"
                                            ShowHeaderFilterButton="true" ShowGroupFooter="Hidden" ShowFooter="true" ShowGroupedColumns="true"
                                            ShowGroupPanel="false" ShowFilterBar="Visible" ShowFilterRow="true" />
                                        <SettingsPager Mode="ShowAllRecords">
                                        </SettingsPager>
                                        <SettingsText Title="Day Book Credit" />
                                        <Columns>
                                            <dx:GridViewDataTextColumn FieldName="Sl. No." Width="7%" VisibleIndex="0" UnboundType="String">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataColumn FieldName="Date" Width="7%" Caption="Date">
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn FieldName="BranchCredit" Width="7%" Caption="Branch">
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn FieldName="InvestmentCredit" Width="7%" Caption="Invest- ments">
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn FieldName="BankCredit" Width="7%" Caption="Banks">
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn FieldName="OtherItemsCredit" Width="7%" Caption="Other Items">
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn FieldName="ChitsCredit" Width="7%" Caption="Chits">
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn FieldName="ForemanCredit" Width="7%" Caption="Foreman chits">
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn FieldName="DecreeDebtorsCredit" Width="7%" Caption="Decree Debtors">
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn FieldName="LoansCredit" Width="7%" Caption="Loans">
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn FieldName="AdvancesCredit" Width="7%" Caption="Advances">
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn FieldName="StampsCredit" Width="7%" Caption="Stamps">
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn FieldName="ProfitandlossCredit" Width="7%" Caption="Profit and Loss">
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn FieldName="CashCredit" Width="7%" Caption="Cash">
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn FieldName="Credit" Width="7%" Caption="Credit">
                                            </dx:GridViewDataColumn>
                                        </Columns>
                                        <Styles Footer-HorizontalAlign="Left" Cell-HorizontalAlign="Left" Header-VerticalAlign="Middle"
                                            Header-HorizontalAlign="Center" Header-Wrap="True">
                                            <GroupPanel Wrap="True" HorizontalAlign="Left">
                                            </GroupPanel>
                                            <GroupRow Wrap="True" HorizontalAlign="Left">
                                            </GroupRow>
                                        </Styles>
                                    </dx:ASPxGridView>
                                    <dx:ASPxGridViewExporter ID="gridCreditExport" runat="server" GridViewID="gridCredit">
                                        <Styles>
                                            <Header Wrap="True" HorizontalAlign="Center">
                                            </Header>
                                            <Cell HorizontalAlign="Left" Wrap="True">
                                            </Cell>
                                            <Footer HorizontalAlign="Left" Wrap="True">
                                            </Footer>
                                        </Styles>
                                    </dx:ASPxGridViewExporter>
                                    <asp:SqlDataSource runat="server" ID="dsCredit" ProviderName="MySql.Data.MySqlClient" />
                                </div>
                            </dx:ContentControl>
                        </ContentCollection>
                    </dx:TabPage>
                    <dx:TabPage Text="Debit">
                        <ContentCollection>
                            <dx:ContentControl ID="ContentControl2" runat="server">
                                <div style="width: 100%; margin: 0px auto;">
                                    <dx:ASPxGridView Styles-AlternatingRow-BackColor="LightGray" Style="margin: 0 auto;
                                        width: 100%;" OnCustomUnboundColumnData="gridDebit_CustomUnboundColumnData"
                                        ID="gridDebit" ClientInstanceName="gridDebit" runat="server" DataSourceID="dsDebit">
                                        <Settings ShowFilterRowMenu="true" ShowHorizontalScrollBar="true" ShowTitlePanel="true"
                                            ShowHeaderFilterButton="true" ShowGroupFooter="Hidden" ShowFooter="true" ShowGroupedColumns="true"
                                            ShowGroupPanel="false" ShowFilterBar="Visible" ShowFilterRow="true" />
                                        <SettingsPager Mode="ShowAllRecords">
                                        </SettingsPager>
                                        <SettingsText Title="Day Book Debit" />
                                        <Columns>
                                            <dx:GridViewDataTextColumn FieldName="Sl. No." Width="7%" VisibleIndex="0" UnboundType="String">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataColumn FieldName="Date" Width="7%" Caption="Date">
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn FieldName="BranchDebit" Width="7%" Caption="Branch">
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn FieldName="InvestmentDebit" Width="7%" Caption="Invest- ments">
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn FieldName="BankDebit" Width="7%" Caption="Banks">
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn FieldName="OtherItemsDebit" Width="7%" Caption="Other Items">
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn FieldName="ChitsDebit" Width="7%" Caption="Chits">
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn FieldName="ForemanDebit" Width="7%" Caption="Foreman chits">
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn FieldName="DecreeDebtorsDebit" Width="7%" Caption="Decree Debtors">
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn FieldName="LoansDebit" Width="7%" Caption="Loans">
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn FieldName="AdvancesDebit" Width="7%" Caption="Advances">
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn FieldName="StampsDebit" Width="7%" Caption="Stamps">
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn FieldName="ProfitandlossDebit" Width="7%" Caption="Profit and Loss">
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn FieldName="CashDebit" Width="7%" Caption="Cash">
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn FieldName="Debit" Width="7%" Caption="Debit">
                                            </dx:GridViewDataColumn>
                                        </Columns>
                                        <Styles Footer-HorizontalAlign="Left" Cell-HorizontalAlign="Left" Header-VerticalAlign="Middle"
                                            Header-HorizontalAlign="Center" Header-Wrap="True">
                                            <GroupPanel Wrap="True" HorizontalAlign="Left">
                                            </GroupPanel>
                                            <GroupRow Wrap="True" HorizontalAlign="Left">
                                            </GroupRow>
                                        </Styles>
                                    </dx:ASPxGridView>
                                    <dx:ASPxGridViewExporter ID="gridDebitExport" runat="server" GridViewID="gridDebit">
                                        <Styles>
                                            <Header Wrap="True" HorizontalAlign="Center">
                                            </Header>
                                            <Cell HorizontalAlign="Left" Wrap="True">
                                            </Cell>
                                            <Footer HorizontalAlign="Left" Wrap="True">
                                            </Footer>
                                        </Styles>
                                    </dx:ASPxGridViewExporter>
                                    <asp:SqlDataSource runat="server" ID="dsDebit" ProviderName="MySql.Data.MySqlClient" />
                                </div>
                            </dx:ContentControl>
                        </ContentCollection>
                    </dx:TabPage>
                </TabPages>
            </dx:ASPxPageControl>
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
