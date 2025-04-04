<%@ Page Title="SVCF Admin Panel" Culture="en-GB" Language="C#" MasterPageFile="~/Branch.Master" EnableEventValidation="false"
    AutoEventWireup="true" CodeBehind="DayBook.aspx.cs" Inherits="SreeVisalamChitFundLtd_phase1.DayBook" %>

 <%@ Register TagName="UsrLed" TagPrefix="Ldger" Src="~/UserControl/FrmLedger.ascx" %>

<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxMenu" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1.Export, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView.Export" TagPrefix="dx" %>
    

<%@ Register assembly="DevExpress.Web.v11.1" namespace="DevExpress.Web.ASPxMenu" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxEditors.v11.1" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxGridView.v11.1" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>
    
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>

<asp:Content ID="Head1" ContentPlaceHolderID="cphHead" runat="server">
    <link href="Styles/aspxtable.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .aligned td
        {
            padding-left: 10px;
            padding-right: 10px;
        }
        #ctl00_cphMainContent_ddlChitGroup_chzn .chzn-drop .chzn-search input[type="text"]
        {
            height: 15px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMainContent" runat="server">
    <div class="row">
      <table>
        <tr>
          <td> <asp:RadioButton ID="RadDayBook" runat="server" Text="Day Book" oncheckedchanged="RadDayBook_CheckedChanged" AutoPostBack="True" /></td>
          <td></td><td></td>
          <td> <asp:RadioButton ID="RadLedger" runat="server" Text="Ledger" 
                  AutoPostBack="True" oncheckedchanged="RadLedger_CheckedChanged" /> </td>
        </tr>       
      </table>
    </div>
 <div id="divDayBook" runat="server" visible="false">
    <div class="row">
        <div class="twelve columns">
            <div class="box_c">
                <div class="box_c_heading  box_actions" >
                    <p>
                        Day Book</p>
                </div>
                <div class="box_c_content">
                    <div class="row">
                        <br />
                        <div style="margin-top: -0.5em; margin-bottom: 0.6em;" class="Sub-heading">
                            <div style="display: table-cell; vertical-align: top; padding-top: 10px; padding-right: 5px !important;">
                                <asp:Label ID="Label3" runat="server" Text="From Date :"></asp:Label>
                            </div>
                            <div style="display: table-cell; vertical-align: top; padding-top: 5px; padding-right: 5px !important;">
                                <asp:TextBox class="input-text maskdate" Width="80px" ID="dateFromConsolidated" runat="server"
                                    placeholder="From Date">
                                </asp:TextBox>
                                <asp:RequiredFieldValidator ValidationGroup="twelvehead" Display="Dynamic" ID="CompareValidatoxr1"
                                    runat="server" ControlToValidate="dateFromConsolidated" ErrorMessage=""></asp:RequiredFieldValidator>
                                <asp:CompareValidator ValidationGroup="twelvehead" Display="Dynamic" ID="cmpStartDate"
                                    runat="server" ControlToValidate="dateFromConsolidated" Operator="DataTypeCheck"
                                    Type="Date"></asp:CompareValidator>
                            </div>
                            <div style="display: table-cell; vertical-align: top; padding-top: 10px; padding-right: 5px !important;">
                                <asp:Label ID="Label1" runat="server" Text="To Date :"></asp:Label>
                            </div>
                            <div style="display: table-cell; vertical-align: top; padding-top: 5px; padding-right: 5px !important;">
                                <asp:TextBox class="input-text maskdate" Width="80px" ID="dateToConsolidated" placeholder="To Date"
                                    runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ValidationGroup="twelvehead" Display="Dynamic" ID="RequiredFieldValidator1"
                                    runat="server" ControlToValidate="dateToConsolidated" ErrorMessage=""></asp:RequiredFieldValidator>
                                <asp:CompareValidator ValidationGroup="twelvehead" Display="Dynamic" ID="cmpEndDate"
                                    runat="server" ControlToCompare="dateFromConsolidated" ControlToValidate="dateToConsolidated"
                                    Operator="GreaterThanEqual" Type="Date"></asp:CompareValidator>
                            </div>
                            <div>


                            <label>Group No:</label>
                           <asp:DropDownList ID="DD_GP" runat="server"  CssClass="chzn-select" 
                             Width="200px" AutoPostBack="True" OnSelectedIndexChanged="DD_GP_SelectedIndexChanged">
                             </asp:DropDownList>
                             </div>

                            <div style="display: table-cell; vertical-align: top; padding-top: 5px; padding-right: 5px !important;">
                                <asp:Button ID="BtnStatisticsGo" ValidationGroup="twelvehead" OnClick="BtnStatisticsGo_Click"
                                    runat="server" class="GreenyPushButton" Text="Go!"></asp:Button>
                            </div>
                            <div style="display: table-cell; vertical-align: top; float: right; padding-right: 5px;
                                text-align: right; margin-top: -35px;">
                                <div style="display: table-cell;">
                                     <%--<a class="noprint" visible="false" style="cursor: hand; cursor: pointer;" id="btnExport" onclick="return PrintDocument();">
                                        <img alt="Print"  class="noprint" src="Styles/Img/pf-button-both.gif" style="border: none;
                                            cursor: hand; cursor: pointer;" />
                                    </a>--%>
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
                            <div style="display:table-cell; float:right; padding-left:5px; text-align:right; margin-top:-0px; ">
                                      <dx:ASPxButton ID="btnExportExcel" Text="ExportExcel"  runat="server" Visible="true"

                            ImageSpacing="0px" EnableTheming="False" EnableDefaultAppearance="True" ToolTip="Export to Excel"

                              Cursor="pointer" OnClick="btnExportExcel_Click1">
                           
                           
                        </dx:ASPxButton>
                            </div>
                    </div>
                    <br />

                    <div class="twelve columns centered">
                        <div style="margin: 0px auto; overflow: auto !important;">
                        <%--<dx:ASPxGridView Styles-AlternatingRow-BackColor="LightGray"
                                  ID="gridprev" ClientInstanceName="gridprev" runat="server" Style="margin: 0 auto; 
                                                width: 100%;" settings-showcolumnheaders="false" >
                         

                                                <settings groupformat="{1}{2}" showfilterrowmenu="false"
                                                    showheaderfilterbutton="false" showfooter="true" showgroupfooter="visiblealways" 
                                                    showgrouppanel="false" showgroupedcolumns="true" showfilterbar="hidden" showfilterrow="false" />
                                                
                                               
                                                <settingstext title="bank ledger" />
                                                <settingspager visible="false" position="bottom" />
                                                <clientsideevents init="oninit" endcallback="onendcallback" />
                                                  <Columns>
                                    <dx:GridViewDataColumn Width="100px" FieldName="Date" Caption="Day" />
                                    <dx:GridViewDataColumn Caption="General Number" Visible="false" />
                                
                                        <dx:GridViewDataColumn Width="100px" FieldName="Groupno" Caption="On what account received or paid" />
                                    <dx:GridViewDataColumn Width="100px" FieldName="Amount" Caption="Subscription" />
                                    <dx:GridViewDataColumn Caption="Interest" />
                                    <dx:GridViewDataColumn Caption="With Drawal from Bank" />
                                    <dx:GridViewDataColumn Width="100px" FieldName="KasarAmount" Caption="Other Items" />
                                    <dx:GridViewDataColumn Width="100px" FieldName="Total" Caption="Total Receipts" />
                                    <dx:GridViewDataColumn Width="100px" FieldName="Voucher_No" Caption="Reference to receipt in the receipt Book" />
                                    <dx:GridViewDataColumn Width="100px" FieldName="PrizedAmount" Caption="Amount Paid to subscriber" />
                                    <dx:GridViewDataColumn Width="100px" FieldName="TotalCommission" Caption="Foreman's Commission" />
                                    <dx:GridViewDataColumn Caption="Deposit in the Bank" />
                                    <dx:GridViewDataColumn Width="100px" FieldName="KasarAmount" Caption="Other Items" />
                                    <dx:GridViewDataColumn width="100px" FieldName="Totalexp" Caption="Total expenditure" />
                                    <dx:GridViewDataColumn Width="100px" FieldName="Balance" Caption="Balance" />
                                    <dx:GridViewDataColumn Caption="reference to the Page No. of the Voucher in the file of voucher" />
                                    <dx:GridViewDataColumn Caption="Signature of Foreman" />
                                    <dx:GridViewDataColumn Caption="Remarks" />
                                </Columns>
                            <Styles Header-Wrap="True">
                                </Styles>
                                     
                                            </dx:aspxgridview>--%>
                          <%--    <dx:ASPxGridViewExporter ID="gridExportprv" runat="server" GridViewID="gridprev">
                                <Styles>
                                 
                                      <Header Wrap="True" HorizontalAlign="Center" Font-Size="25">
                                    </Header>
                                   
                                     <Cell HorizontalAlign="Right" Wrap="True" Font-Size="20" >
                                    </Cell>
                                    <Footer HorizontalAlign="Right" Wrap="True" Font-Size="19" >
                                    </Footer>
                                  
                                </Styles>
                            </dx:ASPxGridViewExporter>--%>






                     
                            <div id="printdiv" class="printable">
                            <dx:ASPxGridView Styles-AlternatingRow-BackColor="LightGray"
                                  ID="grid" ClientInstanceName="grid" runat="server" Style="margin: 0 auto; 
                                                width: 100%;" OnSummaryDisplayText="ASPxGridView1_SummaryDisplayText" OnCustomSummaryCalculate="ASPxGridView1_OnCustomSummaryCalculate">
                                <Settings ShowTitlePanel="true" ShowHeaderFilterButton="true" ShowFooter="true" ShowFilterBar="Visible"
                                    ShowFilterRow="true" ShowFilterRowMenu="true" ShowVerticalScrollBar="true" VerticalScrollableHeight="250"/>
                                <TotalSummary>
                               <dx:ASPxSummaryItem FieldName="Amount" ShowInColumn="" SummaryType="Sum" />
                                     <dx:ASPxSummaryItem FieldName="KasarAmountc" ShowInColumn="" SummaryType="Sum" />
                               <dx:ASPxSummaryItem FieldName="KasarAmount" ShowInColumn="" SummaryType="Sum" />
                               <dx:ASPxSummaryItem FieldName="Total" ShowInColumn="" SummaryType="Sum" />
                               <dx:ASPxSummaryItem FieldName="PrizedAmount" ShowInColumn="" SummaryType="Sum" />
                               <dx:ASPxSummaryItem FieldName="TotalCommission" ShowInColumn="" SummaryType="Sum" />
                               <dx:ASPxSummaryItem FieldName="Totalexp" ShowInColumn="" SummaryType="Sum" />
                               <dx:ASPxSummaryItem FieldName="Balance" ShowInColumn="" SummaryType="custom" />

                                     <dx:ASPxSummaryItem FieldName="Groupno" SummaryType="Custom" />
                               </TotalSummary>
                                <%--<SettingsText Title="Day Book"  />--%>
                               <%-- <SettingsPager Mode="ShowAllRecords" Position="Bottom">
                                </SettingsPager>--%>
                                <Columns>
                                    <dx:GridViewDataColumn Width="100px" FieldName="Date" Caption="Date of account" />
                                    <dx:GridViewDataColumn Caption="General Number" Visible="false" />
                                <%--    <dx:GridViewDataColumn Width="100px" FieldName="GROUPNO" Caption="On what account received or paid" />--%>
                                        <dx:GridViewDataColumn Width="100px" FieldName="Groupno"  Caption="On what account received or paid" CellStyle-HorizontalAlign="left"/>
                                    <dx:GridViewDataColumn Width="100px" FieldName="Amount" Caption="Subscription" />
                                    <dx:GridViewDataColumn Caption="Interest" />
                                    <dx:GridViewDataColumn Caption="With Drawal from Bank" />
                                    <dx:GridViewDataColumn Width="100px" FieldName="KasarAmountc" Caption="Other Items" />
                                    <dx:GridViewDataColumn Width="100px" FieldName="Total" Caption="Total Receipts" />
                                    <dx:GridViewDataColumn Width="100px" FieldName="Voucher_No" Caption="Reference to receipt in the receipt Book" />
                                    <dx:GridViewDataColumn Width="100px" FieldName="PrizedAmount" Caption="Amount Paid to subscriber" />
                                    <dx:GridViewDataColumn Width="100px" FieldName="TotalCommission" Caption="Foreman's Commission" />
                                    <dx:GridViewDataColumn Caption="Deposit in the Bank" />
                                    <dx:GridViewDataColumn Width="100px" FieldName="KasarAmount" Caption="Other Items" />
                                    <dx:GridViewDataColumn width="100px" FieldName="Totalexp" Caption="Total expenditure" />
                                    <dx:GridViewDataColumn Width="100px" FieldName="Balance" Caption="Balance" />
                                    <dx:GridViewDataColumn Caption="reference to the Page No. of the Voucher in the file of voucher" />
                                    <dx:GridViewDataColumn Caption="Signature of Foreman" />
                                    <dx:GridViewDataColumn Caption="Remarks" />
                                </Columns>
                                <Styles Header-Wrap="True">
                                </Styles>
                               
                                  
                                 <ClientSideEvents EndCallback="function(s,e){gridprev.PerformCallback();}" />
                            </dx:ASPxGridView>
                              

                                       <asp:SqlDataSource runat="server" ID="AccessDataSource2" 
                                                ProviderName="MySql.Data.MySqlClient" SelectCommand=" " />

                            </div>
                            <dx:ASPxGridViewExporter ID="gridExport" runat="server" GridViewID="grid">
                                <Styles>
                                  <%--  <Header Wrap="True" HorizontalAlign="Center" Font-Size="21">
                                    </Header>--%>
                                      <Header Wrap="True" HorizontalAlign="Center" Font-Size="25">
                                    </Header>
                                    <%--<Cell HorizontalAlign="Right" Wrap="True" Font-Size="19" >
                                    </Cell>
                                    <Footer HorizontalAlign="Right" Wrap="True" Font-Size="18" >
                                    </Footer>--%>
                                     <Cell HorizontalAlign="Right" Wrap="True" Font-Size="20" >
                                    </Cell>
                                    <Footer HorizontalAlign="Right" Wrap="True" Font-Size="19" >
                                    </Footer>
                                  <%--   <Cell Font-Size="12" HorizontalAlign="Left" Wrap="True" Paddings-PaddingBottom="4px" Paddings-PaddingTop="4px" >
                                                         </Cell>
                                                     <Footer Font-Size="10" HorizontalAlign="Right" Wrap="True" >
                                                     </Footer>
                                
                                <Title Font-Size="12" VerticalAlign="Middle" HorizontalAlign="Center" Paddings-PaddingBottom="2px" Paddings-PaddingTop="2px">
                                </Title>--%>
                                </Styles>
                            </dx:ASPxGridViewExporter>
                            <dx:ASPxGridViewExporter ID="gridexcel" 
    PaperKind="A4" Landscape="True"  runat="server" >
                                 
</dx:ASPxGridViewExporter>
                        </div>
                    </div>
                </div>
              </div>
            </div>
        </div>
    </div>
 </div>
 <div id="divLedger" runat="server" visible="false" class="row" >
    <div class="box_c_heading  box_actions">
       <p>Ledger</p>
    </div>
    <div>
      <Ldger:UsrLed ID="UCLedger" runat="server" />
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
        function PrintDocument() {
            var printContents = document.getElementById("printdiv").innerHTML;
            var originalContents = document.body.innerHTML;
            document.body.innerHTML = printContents;
            window.print();
            document.body.innerHTML = originalContents;
            return true;
        }
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
