<%@ Page Title="SVCF Admin Panel" Culture="en-GB" Language="C#" MasterPageFile="~/Branch.Master"
    AutoEventWireup="true" CodeBehind="CRRReport.aspx.cs" Inherits="SreeVisalamChitFundLtd_phase1.CRRReport" %>


<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxMenu" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1.Export, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView.Export" TagPrefix="dx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v11.1" namespace="DevExpress.Web.ASPxMenu" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxEditors.v11.1" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxGridView.v11.1" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <style type="text/css">
        .aaaaaa
        {
            display: none;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMainContent" runat="server">
    <ajax:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </ajax:ToolkitScriptManager>
    <div>
        <div>
            <div class="box_c">
                <div class="box_c_heading  box_actions noprint">
                    <p>
                        Cash Received Register
                    </p>
                </div>
                <div class="box_c_content">
                    <div class="noprint" style="margin-top: -0.5em; margin-bottom: 0.6em;">
                        <asp:Panel ID="Panel1" runat="server" DefaultButton="BtnStatisticsGo">
                            <div style="display: table-cell; padding-right: 5px !important;">
                                <asp:Label ID="Label1" runat="server" Text="From Date : "></asp:Label>
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
                                <asp:Label ID="Label2" runat="server" Text="To Date : "></asp:Label>
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
                                                <%--<dx:MenuItem Text="XLSX">
                                        </dx:MenuItem>--%>
                                            </Items>
                                        </dx:MenuItem>
                                    </Items>
                                </dx:ASPxMenu>
                            </div>
                        </asp:Panel>
                    </div>
                    <div id="printdiv" class="printable">
                        <div class="twelve columns aaaaaa">
                            <asp:Label ID="Label8" runat="server" Text="Form No. R 7"></asp:Label>
                            <div class="sss">
                                <div class="one columns">
                                    <div style="padding-left: 10px;">
                                        <asp:Image runat="server" ID="imgVisalam" Height="70" Width="70" ImageUrl='<%# Page.ResolveUrl("~/Styles/Image/logo_New.png")%>'
                                            AlternateText="SVCF Admin" />
                                    </div>
                                </div>
                                <div class="seven columns">
                                    <table width="100%" style="text-align: center;">
                                        <tr>
                                            <td>
                                                <table style="width: 100%;">
                                                    <tr align="center">
                                                        <td colspan="2">
                                                            <asp:Label ID="Label3" runat="server" Text="SREE VISALAM CHIT FUND LIMITED"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr align="center">
                                                        <td align="center">
                                                            <asp:Label ID="Label4" runat="server" Text="Regd. Office : TIRUNELVELI - 6."></asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="Label5" runat="server" Text="Admn. Office : Pallattur"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr align="center">
                                                        <td colspan="2">
                                                            <asp:Label runat="server" ID="lblBranch"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr align="center">
                                                        <td colspan="2">
                                                            <asp:Label runat="server" Text="CASH REMITTANCE RECEIVED REGISTER" ID="Label7"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div class="four columns">
                                    <table width="100%">
                                        <tr>
                                            <td style="width: 15%;">
                                                <asp:Label runat="server" Text="Note:"></asp:Label>
                                            </td>
                                            <td style="width: 5%">
                                                <asp:Label runat="server" Text="1."></asp:Label>
                                            </td>
                                            <td style="width: 80%;">
                                                <asp:Label runat="server" Text="The total for each chit group collection to be given separately for each day after closing"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 15%;">
                                            </td>
                                            <td style="width: 5%">
                                                <asp:Label ID="Label11" runat="server" Text="2."></asp:Label>
                                            </td>
                                            <td style="width: 80%;">
                                                <asp:Label ID="Label10" runat="server" Text="The totals of individual sections P&L A/c to be posted in the P&L section ledger for each date"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                            <table width="100%">
                                <tr>
                                    <td>
                                        <asp:Label ID="Label6" runat="server" Text="N. B. :- One entry for one ticket should be given"></asp:Label>
                                    </td>
                                    <td style="float: right;">
                                        <asp:Label runat="server" ID="lblDate" Text="Date : ......................"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <dx:ASPxGridView OnHtmlRowPrepared="grid_HtmlRowPrepared" Style="margin: 0 auto;
                            width: 100%;" ID="grid" ClientInstanceName="grid" runat="server">
                            <Settings ShowTitlePanel="true" ShowHeaderFilterButton="true" 
                                ShowFilterBar="Visible" ShowFilterRow="true" ShowFilterRowMenu="true" />
                            <SettingsText Title="Cash Received Register" />
                            <SettingsPager Mode="ShowAllRecords">
                            </SettingsPager>
                            <Columns>
                                <dx:GridViewDataColumn FieldName="slno" VisibleIndex="1" Width="5%" Caption="S. No.">
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="Voucher_No" VisibleIndex="2" Width="7%" CellStyle-HorizontalAlign="Left"
                                    Caption="Receipt or Reference No.">
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn VisibleIndex="3" FieldName="ChitNumber" Width="7%" Caption="Chit Number">
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="Narration" VisibleIndex="4" Width="16%" Caption="Call No.">
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="MemberName" VisibleIndex="5" Width="10%" Caption="Name"
                                    GroupFooterCellStyle-HorizontalAlign="Right">
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn VisibleIndex="6" FieldName="ChitAmount" Width="9%" Caption="Amount"
                                    CellStyle-HorizontalAlign="Right">
                                </dx:GridViewDataColumn>
                                <dx:GridViewBandColumn Caption="P & L Account" VisibleIndex="7">
                                    <Columns>
                                        <dx:GridViewDataColumn FieldName="Amount" Caption="Amount" Width="9%" CellStyle-HorizontalAlign="Right">
                                        </dx:GridViewDataColumn>
                                    </Columns>
                                </dx:GridViewBandColumn>
                                <dx:GridViewBandColumn Caption="Other Branch" VisibleIndex="8">
                                    <Columns>
                                        <dx:GridViewDataColumn FieldName="OtherAmount" Caption="Amount" Width="9%" CellStyle-HorizontalAlign="Right">
                                        </dx:GridViewDataColumn>
                                    </Columns>
                                </dx:GridViewBandColumn>
                                <dx:GridViewDataColumn FieldName="Heads" VisibleIndex="9"  Caption="Heads" Width="9%">
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn VisibleIndex="10" FieldName="GrandTotal" Width="10%" Caption="Grand Total">
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn VisibleIndex="11" FieldName="Remarks" Width="8%" Caption="Money Collector">
                                </dx:GridViewDataColumn>
                            </Columns>
                            <Styles Footer-HorizontalAlign="Right" Header-VerticalAlign="Middle" Header-HorizontalAlign="Center"
                                Header-Wrap="True">
                                <GroupPanel Wrap="True" HorizontalAlign="Left">
                                </GroupPanel>
                                <GroupRow Wrap="True" HorizontalAlign="Left">
                                </GroupRow>
                                <Header Wrap="True">
                                </Header>
                            </Styles>
                        </dx:ASPxGridView>
                        <dx:ASPxGridViewExporter OnRenderBrick="exporter_RenderBrick" ID="gridExport" runat="server"
                            GridViewID="grid">
                            <Styles>
                                <Header Wrap="True" HorizontalAlign="Center">
                                </Header>
                                <Cell HorizontalAlign="Left" Wrap="True">
                                </Cell>
                                <Footer Wrap="True" HorizontalAlign="Left">
                                </Footer>
                            </Styles>
                        </dx:ASPxGridViewExporter>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        function htmlDecode(value) {
            var returnDecoadedValue = $('<div />').html(value).text();
            return returnDecodedValue;
        }
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
