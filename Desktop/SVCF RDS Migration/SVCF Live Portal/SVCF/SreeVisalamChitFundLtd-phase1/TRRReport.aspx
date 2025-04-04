<%@ Page Title="SVCF Admin Panel" Culture="en-GB" Language="C#" MasterPageFile="~/Branch.Master"
    AutoEventWireup="true" CodeBehind="TRRReport.aspx.cs" Inherits="SreeVisalamChitFundLtd_phase1.TRRReport" %>


<%--<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxMenu" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1.Export, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView.Export" TagPrefix="dx" %>--%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%--<%@ Register assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>--%>
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
    <div class="row">
        <div class="twelve columns">
            <div class="box_c">
                <div class="box_c_heading  box_actions noprint">
                    <p>
                        Transfer Remittance Received Register
                    </p>
                </div>
                <div class="box_c_content">
                    <div class="noprint" style="margin-top: -0.5em; margin-bottom: 0.6em;">
                        <asp:Panel ID="Panel1" runat="server" DefaultButton="BtnStatisticsGo">
                            <div style="display: table-cell; padding-right: 5px !important;">
                                <asp:Label runat="server" Text="From Date : "></asp:Label>
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
                                <asp:Label runat="server" Text="To Date : "></asp:Label>
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
                           <div style="display: table-cell; vertical-align: top; float: right; padding-right: 12px;
                          text-align: right; margin-top: -35px;">
                    <asp:ImageButton ID="ImageButton1" runat="server" OnClick="imgpdf_Click" ImageUrl="Styles/Image/pdfexp.png"
                                         Height="33px" Width="34px"  />
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
                                    <table  style="text-align: center;">
                                        <tr>
                                            <td>
                                                <table style="width: 100%;">
                                                    <tr>
                                                        <td colspan="2">
                                                            <asp:Label runat="server" Text="SREE VISALAM CHIT FUND LIMITED"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label4" runat="server" Text="Regd. Office : TIRUNELVELI - 6."></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label5" runat="server" Text="Admn. Office : Pallattur"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">
                                                            <asp:Label runat="server" ID="lblBranch"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
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
                                                <asp:Label ID="Label1" runat="server" Text="Note:"></asp:Label>
                                            </td>
                                            <td style="width: 5%">
                                                <asp:Label ID="Label2" runat="server" Text="1."></asp:Label>
                                            </td>
                                            <td style="width: 80%;">
                                                <asp:Label ID="Label3" runat="server" Text="The total for each chit group collection to be given separately for each day after closing"></asp:Label>
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
                        
                    </div>
                    <%-- <div style="display: table-cell; vertical-align: top; float: right; padding-right: 12px;
                                    text-align: right; margin-top: -35px;">                                  
                        <asp:ImageButton ID="imgpdf" runat="server" 
                           Height="33px" Width="34px" OnClick="imgpdf_Click" />                                    
                    </div>--%>
                     <asp:Panel runat="server" ID="PrintPanel1">
  <table style="width: 210px">
        <tr>
            <td style="background-color:#FDF5E6; border: 1px solid black" align="center">
                <asp:Label ID="lblCaption" runat="server" Style="font-weight: bold;
                    color:#800000;"></asp:Label>
            </td>
        </tr>
    </table>
                    <div>
                        <asp:GridView ID="GridTRRReport" runat="server" AutoGenerateColumns="false" Height="145px" BorderStyle="Solid"
                            EmptyDataText="No Records Found" CellSpacing="11" Width="830px" CellPadding="15"
                             Font-Names="Verdana" ForeColor="#333333" GridLines="Both">
                                <RowStyle CssClass="GridViewRowStyle" />  
                                <HeaderStyle CssClass="GridViewHeaderStyle" /> 
                            <Columns>
                               <%-- <asp:BoundField HeaderText="ChitAmount" DataField="ChitAmount" />
                                <asp:BoundField HeaderText="BranchAmount" DataField="BranchAmount" />
                                <asp:BoundField HeaderText="HeadAmount" DataField="HeadAmount" />
                                <asp:BoundField HeaderText="MemberName" DataField="MemberName" />--%>
                                <asp:TemplateField HeaderText="S. No."  ItemStyle-BorderColor="Gray" HeaderStyle-BorderColor="Gray" HeaderStyle-BorderStyle="Solid"
                                     HeaderStyle-BorderWidth="2px" ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="1px" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblslno" Text='<%# Eval("slno") %>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Receipt or Reference No." ItemStyle-BorderColor="Gray" HeaderStyle-BorderColor="Gray" HeaderStyle-BorderStyle="Solid"
                                     HeaderStyle-BorderWidth="2px" ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="1px" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblrcno" Text='<%# Eval("Voucher_No") %>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Chit Number" ItemStyle-BorderColor="Gray" HeaderStyle-BorderColor="Gray" HeaderStyle-BorderStyle="Solid"
                                     HeaderStyle-BorderWidth="2px" ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="1px" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblchitno" Text='<%# Eval("ChitNumber") %>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                             <%--   <asp:TemplateField HeaderText="Receipt or Reference No." ItemStyle-BorderColor="Gray" HeaderStyle-BorderColor="Gray" HeaderStyle-BorderStyle="Solid"
                                     HeaderStyle-BorderWidth="2px" ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="1px" ItemStyle-Width="50px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblvc_no" Text='<%# Eval("Voucher_No") %>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                                <asp:TemplateField HeaderText="Name Of the Subscriber(TRR Narration)." ItemStyle-BorderColor="Gray" HeaderStyle-BorderColor="Gray" HeaderStyle-BorderStyle="Solid"
                                     HeaderStyle-BorderWidth="2px" ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="1px" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblnarr" Text='<%# Eval("Narration") %>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Cheque NO" ItemStyle-BorderColor="Gray" HeaderStyle-BorderColor="Gray" HeaderStyle-BorderStyle="Solid"
                                     HeaderStyle-BorderWidth="2px" ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="1px" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblchequeno" Text='<%# Eval("chequeno") %>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Cheque Date" ItemStyle-BorderColor="Gray" HeaderStyle-BorderColor="Gray" HeaderStyle-BorderStyle="Solid"
                                     HeaderStyle-BorderWidth="2px" ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="1px" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblchitdate" Text='<%# Eval("chequedate") %>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                 <asp:TemplateField HeaderText="Name ofthe Bank" ItemStyle-BorderColor="Gray" HeaderStyle-BorderColor="Gray" HeaderStyle-BorderStyle="Solid"
                                     HeaderStyle-BorderWidth="2px" ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="1px" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblchitbank" Text='<%# Eval("Bank") %>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <%--<asp:TemplateField HeaderText="Name" ItemStyle-BorderColor="Gray" HeaderStyle-BorderColor="Gray" HeaderStyle-BorderStyle="Solid"
                                     HeaderStyle-BorderWidth="2px" ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="1px" ItemStyle-Width="50px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblmem_name" Text='<%# Eval("MemberName") %>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                                <asp:TemplateField HeaderText="Amount" ItemStyle-BorderColor="Gray" HeaderStyle-BorderColor="Gray" HeaderStyle-BorderStyle="Solid"
                                     HeaderStyle-BorderWidth="2px" ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="1px" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="right">
                                    <ItemTemplate>
                                        <asp:Label ID="lblchit_amnt" Text='<%# Eval("ChitAmount") %>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                         <asp:Label Text="Chit Amount" runat="server" />
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="P & L A/c-Amount" ItemStyle-BorderColor="Gray" HeaderStyle-BorderColor="Gray" HeaderStyle-BorderStyle="Solid"
                                     HeaderStyle-BorderWidth="2px" ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="1px" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="right">                                 
                                    <ItemTemplate>                                      
                                         <asp:Label ID="lblamnt" Text='<%# Eval("Amount") %>' runat="server"></asp:Label>                                                
                                    </ItemTemplate>
                                    <FooterTemplate>                                      
                                         <asp:Label Text="Head Amount" runat="server" /></div>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="OtherBranch-Amount" ItemStyle-BorderColor="Gray" HeaderStyle-BorderColor="Gray" HeaderStyle-BorderStyle="Solid"
                                     HeaderStyle-BorderWidth="2px" ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="1px" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="right">
                                    <ItemTemplate>
                                        <asp:Label ID="lblothr_amnt" Text='<%# Eval("OtherAmount") %>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Heads" ItemStyle-BorderColor="Gray" HeaderStyle-BorderColor="Gray" HeaderStyle-BorderStyle="Solid"
                                     HeaderStyle-BorderWidth="2px" ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="1px" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblheads" Text='<%# Eval("Heads") %>' runat="server"></asp:Label>
                                    </ItemTemplate>                                     
                                    <FooterTemplate>
                                         <asp:Label Text="Branch Amount" runat="server" /></div>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Grand Total" ItemStyle-BorderColor="Gray" HeaderStyle-BorderColor="Gray" HeaderStyle-BorderStyle="Solid"
                                     HeaderStyle-BorderWidth="2px" ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="1px" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="right">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgrnd_tot" Text='<%# Eval("GrandTotal") %>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Money Collector" ItemStyle-BorderColor="Gray" HeaderStyle-BorderColor="Gray" HeaderStyle-BorderStyle="Solid"
                                     HeaderStyle-BorderWidth="2px" ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="1px" ItemStyle-Width="30px" ItemStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblremarks" Text='<%# Eval("Remarks") %>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>                               
                                <%--<asp:TemplateField>
                                    <FooterTemplate>
                                        <div style="padding:0 0 5px 0"><asp:Label ID="lblChitAmount" runat="server" /></div>
                                        <div style="padding:0 0 5px 0"><asp:Label ID="lblBranchAmount" runat="server" /></div>
                                        <div style="padding:0 0 5px 0"><asp:Label ID="lblHeadAmount" runat="server" /></div>                                        
                                    </FooterTemplate>
                                </asp:TemplateField>        --%>                                                 
                            </Columns>
                        </asp:GridView>
                    </div>
                         </asp:Panel>
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
