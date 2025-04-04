<%@ Page Language="C#" Culture="en-GB" MasterPageFile="~/Branch.Master" AutoEventWireup="true"
    CodeBehind="MoneyCollectorArrears.aspx.cs" EnableViewState="false" Inherits="SreeVisalamChitFundLtd_phase1.MoneyCollectorArrears" %>

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
   
    <div class="row">
        <div class="twelve columns">
            <div class="box_c">
                <div class="box_c_heading cf noprint">
                    <p class="sepV_a">
                        Money Collector Arrear</p>
                </div>
              
                                <div style="display: table-cell; padding-right: 5px !important;">
                                    <asp:Label ID="Label1" runat="server" Text="Select Money Collector Name">
                                    </asp:Label>
                                </div>
                                <div padding-right: 5px !important;">
                                   
                                    <asp:DropDownList ID="ddlMname" runat="server">
                                    </asp:DropDownList>
                                </div>
                                
                                <div style="display: table-cell; padding-right: 5px;">
                                    <asp:Label ID="Label7" runat="server" Text="To Date : "></asp:Label>
                                </div>
                                <div style="display: table-cell; padding-right: 5px;">
                                    <asp:TextBox TabIndex="2" Width="100px" class="input-text maskdate" ID="txtToDate"
                                        placeholder="To Date" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ValidationGroup="twelvehead" Display="Dynamic" ID="RequiredFieldValidator3"
                                        runat="server" ControlToValidate="txtToDate" ErrorMessage=""></asp:RequiredFieldValidator>                                   
                                </div>
                                <div style="display: table-cell; padding-right: 5px; vertical-align: top;">
                                    <asp:Button ID="BtnStatisticsGo" runat="server" Text="Go!" ValidationGroup="twelvehead"
                                        TabIndex="3" CssClass="GreenyPushButton" class="btn" OnClick="BtnStatisticsGo_Click" />
                                </div>
                                <div style="display: table-cell; vertical-align: top; float: right; padding-right: 12px;
                                    text-align: right; margin-top: -35px;">
                                    <a class="noprint" style="cursor: hand; cursor: pointer;" id="btnExport">
                                        <img alt="Print" class="noprint" src="Styles/Img/pf-button-both.gif" style="border: none;
                                            cursor: hand; cursor: pointer;" />
                                    </a>
                                </div>
                           
                            <asp:GridView ID="GV_MCArrear" runat="server" AutoGenerateColumns="false" BorderStyle="Solid"
                                 CellSpacing="3" Font-Names="Verdana" ForeColor="#333333" GridLines="Both" Height="100px" Width="900px">
                                <Columns>
                                    <asp:BoundField HeaderText="S. No." DataField="slno" 
                                        HeaderStyle-BorderColor="Gray" HeaderStyle-BorderStyle="Solid" HeaderStyle-BorderWidth="2px"
                                       ItemStyle-BorderColor="Gray" ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px"/>

                                    <asp:BoundField HeaderText="Ticket No" DataField="GrpMemberID" 
                                        HeaderStyle-BorderColor="Gray" HeaderStyle-BorderStyle="Solid" HeaderStyle-BorderWidth="2px"
                                       ItemStyle-BorderColor="Gray" ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px"/>

                                    <asp:BoundField HeaderText="Name" DataField="MemberName" 
                                        HeaderStyle-BorderColor="Gray" HeaderStyle-BorderStyle="Solid" HeaderStyle-BorderWidth="2px"
                                       ItemStyle-BorderColor="Gray" ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px"/>

                                    <asp:BoundField HeaderText="Call No" DataField="DrawNO" 
                                        HeaderStyle-BorderColor="Gray" HeaderStyle-BorderStyle="Solid" HeaderStyle-BorderWidth="2px"
                                       ItemStyle-BorderColor="Gray" ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px"/>                                   

                                    <asp:BoundField HeaderText="IsPrized" DataField="IsPrized" 
                                        HeaderStyle-BorderColor="Gray" HeaderStyle-BorderStyle="Solid" HeaderStyle-BorderWidth="2px"
                                       ItemStyle-BorderColor="Gray" ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px"/>

                                    <asp:BoundField HeaderText="Arrears Amt" DataField="ArrAmount" 
                                        HeaderStyle-BorderColor="Gray" HeaderStyle-BorderStyle="Solid" HeaderStyle-BorderWidth="2px"
                                       ItemStyle-BorderColor="Gray" ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px"/>

                                    <asp:BoundField HeaderText="Date of Last Realization" DataField="Date" 
                                        HeaderStyle-BorderColor="Gray" HeaderStyle-BorderStyle="Solid" HeaderStyle-BorderWidth="2px"
                                       ItemStyle-BorderColor="Gray" ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px"/>

                                    <asp:BoundField HeaderText="Collected" DataField="AmountCollected" HeaderStyle-BorderColor="Gray" HeaderStyle-BorderStyle="Solid" HeaderStyle-BorderWidth="2px"
                                       ItemStyle-BorderColor="Gray" ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px"/>

                                    <asp:BoundField HeaderText="ARRDtRealized" DataField="Date ARR Realized" 
                                        HeaderStyle-BorderColor="Gray" HeaderStyle-BorderStyle="Solid" HeaderStyle-BorderWidth="2px"
                                       ItemStyle-BorderColor="Gray" ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px"/>

                                    <asp:BoundField HeaderText="ARRAmtRealized" DataField="ARR Amt Realized" HeaderStyle-BorderColor="Gray" HeaderStyle-BorderStyle="Solid" HeaderStyle-BorderWidth="2px"
                                       ItemStyle-BorderColor="Gray" ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px"/>

                                    <asp:BoundField HeaderText="PenaltyCollected" DataField="Penality Collected" HeaderStyle-BorderColor="Gray" HeaderStyle-BorderStyle="Solid" HeaderStyle-BorderWidth="2px"
                                       ItemStyle-BorderColor="Gray" ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px"/>
                                    <asp:BoundField HeaderText="DateofAdvice" DataField="Date of Advice" 
                                        HeaderStyle-BorderColor="Gray" HeaderStyle-BorderStyle="Solid" HeaderStyle-BorderWidth="2px"
                                       ItemStyle-BorderColor="Gray" ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px"/>

                                    <asp:BoundField HeaderText="Report" DataField="Report" HeaderStyle-BorderColor="Gray" HeaderStyle-BorderStyle="Solid" HeaderStyle-BorderWidth="2px"
                                       ItemStyle-BorderColor="Gray" ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px"/>
                                </Columns>
                            </asp:GridView>                            
                        <%--<asp:SqlDataSource runat="server" ID="AccessDataSource1" ProviderName="MySql.Data.MySqlClient" />--%>
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
