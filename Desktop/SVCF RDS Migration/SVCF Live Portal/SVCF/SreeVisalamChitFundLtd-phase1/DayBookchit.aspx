<%@ Page Culture="en-GB" Language="C#" MasterPageFile="~/Branch.Master" AutoEventWireup="true" CodeBehind="DayBookchit.aspx.cs" 
Inherits="SreeVisalamChitFundLtd_phase1.DayBookchit" %>


<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxMenu" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1.Export, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView.Export" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<asp:Content ID="Content2" ContentPlaceHolderID="cphHead" runat="server">
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



<asp:Content ID="Content3" ContentPlaceHolderID="cphMainContent" runat="server">
    <ajax:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </ajax:ToolkitScriptManager>
    <div >
        <div >
            <div class="box_c">
                <div class="box_c_heading  box_actions noprint">
                    <p >
                        Day Book</p>
                </div>
                <div class="box_c_content">
                    <div class="row">
                        <div class="noprint" style="margin-top: -0.5em; margin-bottom: 0.6em;">
                            <asp:Panel ID="Panel1" runat="server" DefaultButton="BtnStatisticsGo">
                                <div style="display: table-cell; padding-right: 5px !important;">
                                    <asp:Label ID="Label1" runat="server" Text="Select Chit Group"></asp:Label>
                                </div>
                                <div style="display: table-cell; padding-right: 5px !important;">
                                    <asp:DropDownList ID="ddlChit" Width="150px" Class="chzn-select" runat="server">
                                    </asp:DropDownList>
                                </div>
                                <div style="display: table-cell; padding-right: 5px !important;">
                                    <asp:Label ID="Label2" runat="server" Text="From Date : "></asp:Label>
                                </div>
                                <div style="display: table-cell; padding-right: 5px;">
                                    <asp:TextBox TabIndex="1" Width="100px" class="input-text maskdate" ID="txtFromDate"
                                        runat="server" placeholder="From Date">
                                    </asp:TextBox>
                                                 </div>

                               <div style="display: table-cell; padding-right: 5px !important;">
                                    <asp:Label ID="Label3" runat="server" Text="To Date : "></asp:Label>
                                </div>
                                <div style="display: table-cell; padding-right: 5px;">
                                    <asp:TextBox TabIndex="1" Width="100px" class="input-text maskdate" ID="txtToDate"
                                        runat="server" placeholder="To Date">
                                    </asp:TextBox>
                                                 </div>


                                <div style="display: table-cell; padding-right: 5px; vertical-align: top;">
                                    <asp:Button ValidationGroup="twelvehead" TabIndex="3" ID="BtnStatisticsGo" CssClass="GreenyPushButton"
                                        runat="server" class="btn"  Text="Go!" onclick="BtnStatisticsGo_Click"></asp:Button>
                                </div>
                                <div style="display: table-cell; vertical-align: top; float: right; padding-right: 5px;
                                    text-align: right; margin-top: -35px;">
                                    <a class="noprint" style="cursor: hand; cursor: pointer;" id="btnExport">
                                        <img alt="Print" class="noprint" src="Styles/Img/pf-button-both.gif" style="border: none;
                                            cursor: hand; cursor: pointer;" />
                                    </a>
                                   
                                </div>
                            </asp:Panel>
                        </div>
                        <div id="printdiv" class="printable">
                            <dx:ASPxGridView Style="margin: 0 auto; width:100%;" ID="grid" ClientInstanceName="grid" runat="server">
               

                         
                                <SettingsPager Mode="ShowAllRecords">
                                </SettingsPager>
                             
                                <Columns>
                                     <dx:GridViewDataColumn FieldName="Date" Width="1" Caption="Date" >
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn FieldName="GeneralNumber" Width="1" Caption="General Number" >
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn FieldName="ChitNo1" Width="2"   Caption="Chit NO" >
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn FieldName="MemberName" Width="7"  Caption="On what account received or paid">                                       
                                    </dx:GridViewDataColumn>
                                  
                                
                                    <dx:GridViewBandColumn Caption="RECEIPT">
                                        <Columns>
                                            <dx:GridViewDataColumn FieldName="Subscription" Width="10"
                                             Caption="Subscription" FooterCellStyle-HorizontalAlign="Right"
                                                CellStyle-HorizontalAlign="Right">
                                            </dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn FieldName="Interest" Width="10" Caption="Interest" FooterCellStyle-HorizontalAlign="Right"
                                                CellStyle-HorizontalAlign="Right">

                                            </dx:GridViewDataColumn>
                                              <dx:GridViewDataColumn FieldName="withdrawbank" Width="10" Caption="With drawal from Bank" FooterCellStyle-HorizontalAlign="Right"
                                                CellStyle-HorizontalAlign="Right">

                                            </dx:GridViewDataColumn>

                                                   <dx:GridViewDataColumn FieldName="OtherItems" Width="10" Caption="Other Items" FooterCellStyle-HorizontalAlign="Right"
                                                CellStyle-HorizontalAlign="Right">
                                            </dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn FieldName="TotalReceipts" Width="10"  Caption="Total Receipts" FooterCellStyle-HorizontalAlign="Right"
                                                CellStyle-HorizontalAlign="Right">
                                            </dx:GridViewDataColumn>

                                        </Columns>
                                    </dx:GridViewBandColumn>

                                    <dx:GridViewDataColumn FieldName="ReceiptNo" Width="10"  Caption="Reference to receipt in the receipt book">
                                    </dx:GridViewDataColumn>

                                       <dx:GridViewBandColumn Caption="EXPENDITURE">
                                       <Columns >
                                        <dx:GridViewDataColumn FieldName="Amountpaidtosubscriber" Width="10"   Caption="Amount paid to subscriber" FooterCellStyle-HorizontalAlign="Right"
                                                CellStyle-HorizontalAlign="Right">
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn FieldName="Foremanscommision" Width="10"  Caption="Foreman's commision" FooterCellStyle-HorizontalAlign="Right"
                                                CellStyle-HorizontalAlign="Right">
                                            </dx:GridViewDataColumn>
                                         <dx:GridViewDataColumn FieldName="Depositbank" Width="10"  Caption="Deposit in the Bank" FooterCellStyle-HorizontalAlign="Right"
                                                CellStyle-HorizontalAlign="Right">
                                            </dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn FieldName="OtherItems1" Width="10"  Caption="Other Items" FooterCellStyle-HorizontalAlign="Right"
                                                CellStyle-HorizontalAlign="Right">
                                            </dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn FieldName="TotalExpenditore" Width="10"  Caption="Total Expenditore" FooterCellStyle-HorizontalAlign="Right"
                                                CellStyle-HorizontalAlign="Right">
                                            </dx:GridViewDataColumn>
                                       </Columns>
                                       </dx:GridViewBandColumn> 

                                       
                                    <dx:GridViewDataColumn FieldName="Balance" Width="10"  Caption="Balance">
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn FieldName="CRDR" Width="10"   Caption="Reference in the page No of the voucher ">
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn FieldName="Signatur"  Width="10"  Caption="Signature of Foreman">
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn FieldName="Remarks"  Width="10"  Caption="Remarks">
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

