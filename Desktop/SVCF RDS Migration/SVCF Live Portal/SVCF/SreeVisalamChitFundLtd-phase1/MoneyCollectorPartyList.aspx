<%@ Page Title="" Language="C#" MasterPageFile="~/Branch.Master" AutoEventWireup="true" CodeBehind="MoneyCollectorPartyList.aspx.cs" Inherits="SreeVisalamChitFundLtd_phase1.MoneyCollectorPartyList" %>

<%--<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1.Export, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView.Export" TagPrefix="dx" %>--%>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <link href="~/Styles/gridadvice.css" rel="stylesheet" />
        <style type="text/css">
        .chzn-results
        {
            text-align: center;
        }
        /*#ctl00_cphMainContent_ddlChit_chzn .chzn-drop .chzn-search input[type="text"]
        {
            height: 16px;
        }*/
        /*.linecolor tr td
        {
            border-top: 1px solid Gray;
            border-bottom: 1px solid Gray;
        }*/

        .Grid, .Grid th, .Grid td {
            border: 1px solid #2F4F4F;
        }

        .panelstyle
        {
            margin-left:80px;
        }

        .paddingtd
        {
            padding-left:10px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMainContent" runat="server">
    <div class="row">
        <div class="twelve columns" style="margin-top:0.5em; margin-bottom:0.6em;">
            <div class="box_c">
                <div class="box_c_content">
                    <div>
                        <table>
                            <tr>
                                <td>
                                    <asp:Label ID="lblbrch" runat="server" Text="Select Branch" Width="230px"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlBranch" runat="server" Width="230px" CssClass="chzn-select"
                                        AutoPostBack="true" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblMoneycollector" runat="server" Text="Select Money Collector Name" Width="230px"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlCollector" runat="server" Width="230px" CssClass="chzn-select">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Button ID="btnOk" runat="server" Text="Ok" ValidationGroup="twelvehead"
                                        class="btn" CssClass="GreenyPushButton" OnClick="btnOk_Click" />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div style="display:table-cell;vertical-align:top;float:right;padding:5px">
                        <asp:Button ID="btnExportExcel" runat="server" Text="ExportExcel" ValidationGroup="twelvehead" 
                            Class="btn" CssClass="GreenyPushButton" OnClick="btnExportExcel_Click" />
                    </div>
                    <p style="display:inline-block;align-content:center;"></p>
                    <asp:Panel ID="panel1" runat="server">
                        <table style="width:100%;">
                            <tr>
                                <td style="background-color:white;" colspan="5">
                                    <asp:Label ID="lblCaption"  runat="server" Style="font-weight:100;text-align:center;padding-left:35%;display:inline-block;"></asp:Label>
                                </td>
                            </tr>
                        </table>
                        <%--<asp:GridView id="gv_BCPartyList" runat="server" borderstyle="None" CssClass="Grid" HeaderStyle-Font-Bold="true" Font-Names="Verdana" ForeColor="Black" GridLines="Both" CellSpacing="2"
                            autogeneratecolumns="false" pagesize="50" OnSelectedIndexChanged="gv_BCPartyList_SelectedIndexChanged" onrowdatabound="gv_BCPartyList_RowDataBound" ShowFooter="true" BackColor="White" BorderColor="Red" BorderWidth="1px" CellPadding="2" Width="800px" Font-Size="10.2pt">--%>
                        
                            <asp:GridView id="gv_BCPartyList" runat="server" borderstyle="None" CssClass="Grid" HeaderStyle-Font-Bold="true" Font-Names="Verdana" ForeColor="Black" GridLines="Both" CellSpacing="2"
                            autogeneratecolumns="false" pagesize="50" ShowFooter="true" BackColor="White" BorderColor="Red" BorderWidth="1px" CellPadding="2" Width="800px" Font-Size="10.2pt">
                            <Columns>
                                <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="15px" ItemStyle-Wrap="false">
                                    <ItemTemplate>
                                       
                                        <asp:Label ID="lblSno" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Chit No" ItemStyle-Width="5px" ItemStyle-HorizontalAlign="Left">
                                         <ItemTemplate>
                                             <asp:Label ID="lblChitName" runat="server" Text='<%#Eval("Chit No") %>'></asp:Label>
                                         </ItemTemplate>
                                     </asp:TemplateField>
                                <asp:TemplateField HeaderText="Member Name" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="5px" >
                                    <ItemTemplate>
                                        <asp:Label id="lblMemberName" runat="server" Text='<%#Eval("Name Of the Subscriber") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Current Installment No" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="3px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblInstallment" runat="server" Text='<%#Eval("Current Installment No") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Prized/NonPrized" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="3px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPrized" runat="server" Text='<%#Eval("IsPrized") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Cell Number" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="20px" ItemStyle-Wrap="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCallNo" runat="server" Text='<%#Eval("Mobile No") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Address" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="30px" >
                                    <ItemTemplate>
                                        <asp:label ID="lblAddress" runat="server" Text='<%#Eval("Address") %>'></asp:label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        <%--<dx:ASPxGridViewExporter ID="gridviewExporter" runat="server" GridViewID="gv_BCPartyList" ReportHeader="center">
                            <Styles>
                                <Cell HorizontalAlign="Left"></Cell>
                                <Header HorizontalAlign="Center"></Header>
                                <Footer HorizontalAlign="Left"></Footer>
                                <GroupFooter HorizontalAlign="Left"></GroupFooter>
                            </Styles>
                        </dx:ASPxGridViewExporter>--%>
                        <!--<asp:sqldatasource id="AccessDataSource1" runat="server" ProviderName="MySql.Data.MySqlClient" SelectCommand=" "></asp:sqldatasource>-->
                    </asp:Panel>
                </div>
            </div>
        </div> 
    </div>
</asp:Content>
