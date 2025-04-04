<%@ Page Title="SVCF Admin Panel" Culture="en-GB" Language="C#" MasterPageFile="~/Branch.Master"
    AutoEventWireup="true" CodeBehind="ChitAbstract.aspx.cs" Inherits="SreeVisalamChitFundLtd_phase1.ChitAbstract" %>

<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxMenu" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1.Export, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView.Export" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <style type="text/css">
        .chzn-results
        {
            text-align: center;
        }
        #ctl00_cphMainContent_ddlChit_chzn .chzn-drop .chzn-search input[type="text"]
        {
            height: 16px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMainContent" runat="server">
    <ajax:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </ajax:ToolkitScriptManager>
    <div class="row">
        <div class="twelve columns">
            <div class="box_c">
                <div class="box_c_heading cf noprint">
                    <p class="sepV_a">
                        Trial And Arrear</p>
                </div>
                <div class="box_c_content">
                    <div class="row">
                        <div class="noprint" style="margin-top: -0.5em; margin-bottom: 0.6em;">
                            <asp:Panel ID="Panel1" runat="server" DefaultButton="BtnStatisticsGo">
                                <div style="display: table-cell; padding-right: 5px !important;">
                                    <asp:Label ID="Label2" runat="server" Text="Date : "></asp:Label>
                                </div>
                                <div style="display: table-cell; padding-right: 5px;">
                                    <asp:TextBox TabIndex="1" Width="100px" class="input-text maskdate" ID="txtFromDate"
                                        runat="server" placeholder="From Date">
                                    </asp:TextBox>
                                    <asp:RequiredFieldValidator ValidationGroup="twelvehead" Display="Dynamic" ID="RequiredFieldValidator3"
                                        runat="server" ControlToValidate="txtFromDate" ErrorMessage=""></asp:RequiredFieldValidator>
                                    <asp:CompareValidator ID="CompareValidator10" ValidationGroup="twelvehead" runat="server"
                                        Display="Dynamic" ErrorMessage="" ControlToValidate="txtFromDate" Operator="DataTypeCheck"
                                        Type="Date"></asp:CompareValidator>
                                </div>
                                <div style="display: table-cell; padding-right: 5px; vertical-align: top;">
                                    <asp:Button ValidationGroup="twelvehead" TabIndex="3" ID="BtnStatisticsGo" CssClass="GreenyPushButton"
                                        runat="server" class="btn" OnClick="BtnStatisticsGo_Click" Text="Go!"></asp:Button>
                                </div>
                                <div style="display: table-cell; vertical-align: top; float: right; padding-right: 5px;
                                    text-align: right; margin-top: -35px;">
                                  <%--  <a class="noprint" style="cursor: hand; cursor: pointer;" id="btnExport">
                                        <img alt="Print" class="noprint" src="Styles/Img/pf-button-both.gif" style="border: none;
                                            cursor: hand; cursor: pointer;" />
                                    </a>--%>
                                    <div>
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
                                    <div style="display:table-cell; float:right; padding-left:5px; text-align:right; margin-top:-0px; ">
                                      <dx:ASPxButton ID="btnExportExcel" Text="ExportExcel"  runat="server" Visible="true"

                            ImageSpacing="0px" EnableTheming="False" EnableDefaultAppearance="True" ToolTip="Export to Excel"

                              Cursor="pointer" OnClick="btnExportExcel_Click1">
                           
                           
                        </dx:ASPxButton>
                            </div>
                                </div>
                            </asp:Panel>
                        </div>
                        <div id="printdiv" class="printable">
                            <dx:ASPxGridView Style="margin: 0 auto;
                                width: 100%;" ID="grid" ClientInstanceName="grid" runat="server" >
                                <Settings ShowHeaderFilterButton="true" ShowFilterRow="false" ShowFilterRowMenu="true"
                                    ShowTitlePanel="true" ShowFooter="true" />
                                <Styles Cell-HorizontalAlign="Left" Header-VerticalAlign="Middle" Header-HorizontalAlign="Center"
                                    Header-Wrap="True">
                                    <Header Wrap="True">
                                    </Header>
                                </Styles>
                                <SettingsPager Mode="ShowAllRecords">
                                </SettingsPager>
                               <%-- <TotalSummary>
                                    <dx:ASPxSummaryItem DisplayFormat="{0}" FieldName="Credit" SummaryType="Sum" />
                                    <dx:ASPxSummaryItem DisplayFormat="{0}" FieldName="Debit" SummaryType="Sum" />
                                    <dx:ASPxSummaryItem DisplayFormat="Total Rs." FieldName="Chit" SummaryType="Custom" />
                                    <dx:ASPxSummaryItem DisplayFormat="Running Chit Value Rs." FieldName="Running" SummaryType="Custom" />
                                    <dx:ASPxSummaryItem DisplayFormat="{0}" FieldName="ChitValue" SummaryType="Sum" />
                                </TotalSummary>--%>
                                <Columns>
                                    <dx:GridViewDataColumn FieldName="Slno" Width="15%" Caption="Sl No">
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn FieldName="Chit" Width="15%" Caption="Group No" FooterCellStyle-HorizontalAlign="Right">
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn FieldName="Credit" Caption="Credit" Width="20%"  FooterCellStyle-HorizontalAlign="Right"
                                        CellStyle-HorizontalAlign="Right">
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn FieldName="Debit" Caption="Debit" Width="20%"  FooterCellStyle-HorizontalAlign="Right"
                                        CellStyle-HorizontalAlign="Right">
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn FieldName="Running" Width="15%"  Caption="Running Inst" FooterCellStyle-HorizontalAlign="Right">
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn FieldName="ChitValue" Width="15%"  Caption="Chit Value"  FooterCellStyle-HorizontalAlign="Right">
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
                        </div>
                          <dx:ASPxGridViewExporter ID="gridExport" runat="server" GridViewID="grid">
                                <Styles>
                                    <Header Wrap="True" HorizontalAlign="Center">
                                    </Header>
                                    <Cell Font-Size="10" HorizontalAlign="Left" Wrap="True" Paddings-PaddingBottom="2px" Paddings-PaddingTop="2px" >
                                    </Cell>
                                    <Footer HorizontalAlign="Left" Wrap="True">
                                    </Footer>
                                </Styles>
                            </dx:ASPxGridViewExporter>
                        <dx:ASPxGridViewExporter ID="gridexcel" GridViewID="grid" 

    PaperKind="A4" Landscape="True"  runat="server" >
</dx:ASPxGridViewExporter>

                    </div>
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        function printDiv(divname) {
            var printContents = document.getElementById(divname).innerHTML;
            var originalContents = document.body.innerHTML;
            document.body.innerHTML = printContents;
            window.print();
            document.body.innerHTML = originalContents;
        }
     
    </script>
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
        $(document).ready(function () {
            $("#btnExport").click(function (e) {
                printDiv('printdiv');
                e.preventDefault();
            });
        });

       
    </script>
    <style type="text/css">
        @media print
        {
            header
            {
                display: none;
            }
            .noprint
            {
                display: none;
            }
            div
            {
                border: none !important;
            }
        }
    </style>
</asp:Content>
