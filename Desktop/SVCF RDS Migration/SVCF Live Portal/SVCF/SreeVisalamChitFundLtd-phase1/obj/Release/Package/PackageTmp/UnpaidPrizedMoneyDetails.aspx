<%@ Page Title="SVCF Admin Panel" Culture="en-GB" Language="C#" MasterPageFile="~/Branch.Master"
    AutoEventWireup="true" CodeBehind="UnpaidPrizedMoneyDetails.aspx.cs" Inherits="SreeVisalamChitFundLtd_phase1.UnpaidPrizedMoneyDetails" %>

<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxupmd" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dxupmd" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxMenu" TagPrefix="dxupmd" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1.Export, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView.Export" TagPrefix="dxupmd" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <style type="text/css">
        .dxic
        {
            width: 100%;
            padding-left: 0px !important;
            padding-right: 0px !important;
            padding-top: 0px !important;
            padding-bottom: 0px !important;
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
        .chzn-results
        {
            text-align:center;
        }
        #ctl00_cphMainContent_ddlBranch_chzn .chzn-drop .chzn-search input[type="text"]
        {
            height:15px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="box_c">
        <div class="box_c_heading box_actions">
            <p>
                Particulars of Outstanding & Unpaid Prize Money Details</p>
        </div>
        <div class="box_c_content">
            <div style="display: table-cell; vertical-align: top; padding-top: 10px; padding-right: 5px !important;">
                <asp:Label Font-Size="Small" Font-Bold="true" Text="Branch Name :" runat="server"
                    ID="lblBranch"></asp:Label>
            </div>
            <div style="display: table-cell; vertical-align: top; padding-top: 5px; padding-right: 5px !important;">
                <asp:DropDownList Width="240px" CssClass="chzn-select" runat="server" ID="ddlBranch">
                </asp:DropDownList>
            </div>
            <div style="display: table-cell; vertical-align: top; padding-top: 5px; padding-right: 5px !important;">
                <asp:Button runat="server" CssClass="GreenyPushButton" OnClick="BranchName_OnClick"
                    Text="Load" />
            </div>
            <div style="display: table-cell; vertical-align: top; float: right; padding-right: 5px;
                text-align: right; margin-top: -35px;">
                <dxupmd:ASPxMenu ID="ASPxMenu1" OnItemClick="Export_click" runat="server" AllowSelectItem="True" ShowPopOutImages="True">
                    <Items>
                        <dxupmd:MenuItem Text="Export">
                            <Items>
                                <dxupmd:MenuItem Text="PDF">
                                </dxupmd:MenuItem>
                                <dxupmd:MenuItem Text="XLSX">
                                </dxupmd:MenuItem>
                            </Items>
                        </dxupmd:MenuItem>
                    </Items>
                </dxupmd:ASPxMenu>
            </div>
            <div style="width: 100%; margin: 0 auto; padding: 10px !important;">
                <dxupmd:ASPxGridView ID="gridBranch" ClientInstanceName="grid" Style="margin: 0 auto;"
                    runat="server" DataSourceID="DataSourceEmployee" AutoGenerateColumns="true" Width="100%">
                    <Columns>
                        <%--<dxupmd:GridViewDataTextColumn FieldName="row_number" Caption="S.No">
                        </dxupmd:GridViewDataTextColumn>--%>
                        <dxupmd:GridViewDataTextColumn FieldName="ChitNumber" Caption="Chit Number">
                        </dxupmd:GridViewDataTextColumn>
                        <dxupmd:GridViewBandColumn Caption="DRAWAL">
                            <Columns>
                                <dxupmd:GridViewDataTextColumn FieldName="DRAWAL_Instmnt." Caption="Instmnt">
                                </dxupmd:GridViewDataTextColumn>
                                <dxupmd:GridViewDataTextColumn FieldName="DRAWAL_Date" Caption="Date">
                                </dxupmd:GridViewDataTextColumn>
                            </Columns>
                        </dxupmd:GridViewBandColumn>
                        <dxupmd:GridViewDataTextColumn FieldName="MemberName" Caption="Name of the Subscriber">
                        </dxupmd:GridViewDataTextColumn>
                        <dxupmd:GridViewBandColumn Caption="OUT STANDING">
                            <Columns>
                                <dxupmd:GridViewDataTextColumn FieldName="OUTSTANDING_PrizedAmount" Caption="Prize Money">
                                </dxupmd:GridViewDataTextColumn>
                                <dxupmd:GridViewDataTextColumn FieldName="OUTSTANDING_Kasar" Caption="Kasar">
                                </dxupmd:GridViewDataTextColumn>
                                <dxupmd:GridViewDataTextColumn FieldName="OUTSTANDING_Total" Caption="Total">
                                </dxupmd:GridViewDataTextColumn>
                            </Columns>
                        </dxupmd:GridViewBandColumn>
                        <dxupmd:GridViewBandColumn Caption="UNPAID">
                            <Columns>
                                <dxupmd:GridViewDataTextColumn FieldName="UNPAID_Commision" Caption="Commision">
                                </dxupmd:GridViewDataTextColumn>
                                <dxupmd:GridViewDataTextColumn FieldName="UNPAID_PrizeMoney" Caption="Prize Money">
                                </dxupmd:GridViewDataTextColumn>
                            </Columns>
                        </dxupmd:GridViewBandColumn>
                        <dxupmd:GridViewDataTextColumn FieldName="AmountActuallyremittedbytheParty" Caption="Amount Actually remitted by the party">
                        </dxupmd:GridViewDataTextColumn>
                        <dxupmd:GridViewDataTextColumn FieldName="Arrears" Caption="Arrears">
                        </dxupmd:GridViewDataTextColumn>
                        <dxupmd:GridViewDataTextColumn Caption="Unpaid Prize Money Payable">
                        </dxupmd:GridViewDataTextColumn>
                        <dxupmd:GridViewDataTextColumn Caption="Remarks">
                        </dxupmd:GridViewDataTextColumn>
                    </Columns>
                    <TotalSummary>
                        <dxupmd:ASPxSummaryItem FieldName="OUTSTANDING_PrizedAmount" DisplayFormat="{0}"
                            SummaryType="Sum" />
                        <dxupmd:ASPxSummaryItem FieldName="OUTSTANDING_Kasar" DisplayFormat="{0}" SummaryType="Sum" />
                        <dxupmd:ASPxSummaryItem FieldName="OUTSTANDING_Total" DisplayFormat="{0}" SummaryType="Sum" />
                        <dxupmd:ASPxSummaryItem FieldName="UNPAID_Commision" DisplayFormat="{0}" SummaryType="Sum" />
                        <dxupmd:ASPxSummaryItem FieldName="UNPAID_PrizeMoney" DisplayFormat="{0}" SummaryType="Sum" />
                        <dxupmd:ASPxSummaryItem FieldName="AmountActuallyremittedbytheParty" DisplayFormat="{0}"
                            SummaryType="Sum" />
                        <dxupmd:ASPxSummaryItem FieldName="Arrears" DisplayFormat="{0}" SummaryType="Sum" />
                    </TotalSummary>
                    <Settings ShowFooter="true" ShowHorizontalScrollBar="true" ShowTitlePanel="true"
                        ShowHeaderFilterButton="true" ShowFilterRow="true" />
                    <SettingsPager Mode="ShowAllRecords" />
                    <Styles Footer-HorizontalAlign="Left" Cell-HorizontalAlign="Left" Header-Wrap="True"
                        Header-HorizontalAlign="Center" Header-VerticalAlign="Middle" CommandColumn-Paddings-Padding="10">
                    </Styles>
                    <SettingsText Title="Particulars of Outstanding & Unpaid Prize Money Details" />
                </dxupmd:ASPxGridView>
                <dxupmd:ASPxGridViewExporter ID="gridExport" runat="server" GridViewID="gridBranch">
                    <Styles>
                        <Header Font-Size="9" Wrap="True" VerticalAlign="Middle" HorizontalAlign="Center">
                        </Header>
                        <Cell Font-Size="8" HorizontalAlign="Left" Wrap="True">
                        </Cell>
                        <Footer Font-Size="9" HorizontalAlign="Left" Wrap="True">
                        </Footer>
                        <Title Font-Size="10" VerticalAlign="Middle" HorizontalAlign="Center" Wrap="True">
                        </Title>
                    </Styles>
                </dxupmd:ASPxGridViewExporter>
                <asp:SqlDataSource runat="server" ID="DataSourceEmployee" 
                    ProviderName="MySql.Data.MySqlClient"></asp:SqlDataSource>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        $(document).ready(function () {
            $(".chzn-select").chosen({ search_contains: true });
        });
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $(".chzn-select").chosen({ search_contains: true });
        });
    </script>
</asp:Content>
