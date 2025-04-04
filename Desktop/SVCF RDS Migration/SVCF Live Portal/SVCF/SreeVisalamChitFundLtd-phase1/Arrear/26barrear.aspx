<%@ Page Title="SVCF - Admin Page" Culture="en-GB" Language="C#" MasterPageFile="~/Branch.Master"
    AutoEventWireup="true" CodeBehind="26barrear.aspx.cs" Inherits="SreeVisalamChitFundLtd_phase1._26barrear" %>
     <%@ register assembly="DevExpress.Web.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
        namespace="DevExpress.Web.ASPxMenu" tagprefix="dx" %>
    <%@ register assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
        namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>
    <%@ register assembly="DevExpress.Web.ASPxGridView.v11.1.Export, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
        namespace="DevExpress.Web.ASPxGridView.Export" tagprefix="dx" %>
    <%@ register assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
        namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>
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
                        Indivdual BC War Arrear Statement </p>
                </div>
                <div class="box_c_content">
                    <div class="row">
                        <div class="noprint" style="margin-top: -0.5em; margin-bottom: 0.6em;">
                            <asp:Panel ID="Panel1" runat="server" DefaultButton="BtnStatisticsGo">
                                <div style="display: table-cell; padding-right: 5px !important;">
                                    <asp:Label ID="Label1" runat="server" Text="Group : "></asp:Label>
                                </div>
                                <div style="display: table-cell; padding-right: 5px;">
                                    <asp:DropDownList runat="server" CssClass="chzn-select" Width="150px" ID="ddlGroup"></asp:DropDownList>
                                </div>
                                <div style="display: table-cell; padding-right: 5px !important;">
                                    <asp:Label ID="Label2" runat="server" Text="Date : "></asp:Label>
                                </div>
                                <div style="display: table-cell; padding-right: 5px;">
                                    <asp:TextBox TabIndex="1" Width="100px" class="input-text maskdate" ID="txtFromDate"
                                        runat="server" placeholder="Date">
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
                                </div>
                            </asp:Panel>
                        </div>
                        <div id="printdiv" class="printable">
                            <dx:ASPxGridView Style="margin: 0 auto;
                                width: 100%;" ID="grid" ClientInstanceName="grid" runat="server">
                                <Settings ShowHeaderFilterButton="true" ShowFilterRow="true" ShowFilterRowMenu="true"
                                    ShowTitlePanel="true" ShowFooter="true" />
                                <Styles Cell-HorizontalAlign="Left" Header-VerticalAlign="Middle" Header-HorizontalAlign="Center"
                                    Header-Wrap="True">
                                    <Header Wrap="True">
                                    </Header>
                                </Styles>
                                <SettingsText Title="Chit Arrear" />
                                <SettingsPager Mode="ShowAllRecords">
                                </SettingsPager>
                                <Columns>
                                    <dx:GridViewDataColumn FieldName="GrpMemberId" Caption="Token Number" FooterCellStyle-HorizontalAlign="Right"
                                        CellStyle-HorizontalAlign="Right">
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn FieldName="Name" Caption="Name of the Subscriber" FooterCellStyle-HorizontalAlign="Right"
                                        CellStyle-HorizontalAlign="Right">
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn FieldName="NoofArrears" Caption="No.of Instalments in Arrrear" FooterCellStyle-HorizontalAlign="Right"
                                        CellStyle-HorizontalAlign="Right">
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn FieldName="ArrearAmount" Caption="Arrears Amount" FooterCellStyle-HorizontalAlign="Right"
                                        CellStyle-HorizontalAlign="Right">
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn FieldName="status" Caption="Whether Terminated/Prized/Non Prized" FooterCellStyle-HorizontalAlign="Right"
                                        CellStyle-HorizontalAlign="Right">
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
                                <Header Font-Size="7" Wrap="True" HorizontalAlign="Center">
                                </Header>
                                <Cell Font-Size="6" Wrap="True">
                                </Cell>
                                <Footer Font-Size="7" HorizontalAlign="Right" Wrap="True">
                                </Footer>
                                <Title Font-Size="9" VerticalAlign="Middle" HorizontalAlign="Center" Wrap="True">
                                </Title>
                            </Styles>
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
