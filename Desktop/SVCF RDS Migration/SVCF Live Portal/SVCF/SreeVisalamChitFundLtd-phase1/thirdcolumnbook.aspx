<%@ Page Title="SVCF - Admin Page" Culture="en-GB" Language="C#" MasterPageFile="~/Branch.Master"
    AutoEventWireup="true" CodeBehind="thirdcolumnbook.aspx.cs" Inherits="SreeVisalamChitFundLtd_phase1.thirdcolumnbook" %>

<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxMenu" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1.Export, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView.Export" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
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
                Third Column Book</p>
        </div>
        <div class="box_c_content">
            <div style="margin-top: -0.5em; margin-bottom: 0.6em;">
                <asp:Panel ID="Panel1" runat="server" DefaultButton="BtnStatisticsGo">
                    <%--<div style="display: table-cell; padding-right: 5px !important;">
                        <asp:Label ID="Label1" runat="server" Text="Select Chit Group"></asp:Label>
                    </div>
                    <div style="display: table-cell; padding-right: 5px !important;">
                        <asp:DropDownList ID="ddlChit" Width="150px" Class="chzn-select" runat="server">
                            <asp:ListItem Value="--Select--" Text="--Select--"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div style="display: table-cell; padding-right: 5px !important;">
                        <asp:Label ID="Label2" runat="server" Text="From Date : "></asp:Label>
                    </div>--%>
                    <div style="display: table-cell; padding-right: 5px;">
                        <asp:TextBox TabIndex="1" Width="100px" class="input-text maskdate" ID="txtFromDate"
                            runat="server" placeholder="Date">
                        </asp:TextBox>
                        <asp:RequiredFieldValidator ValidationGroup="twelvehead" Display="Dynamic" ID="RequiredFieldValidator3"
                            runat="server" ControlToValidate="txtFromDate" ErrorMessage=""></asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="CompareValidator10" ValidationGroup="twelvehead" runat="server"
                            Display="Dynamic" ControlToValidate="txtFromDate" Operator="DataTypeCheck" Type="Date"></asp:CompareValidator>
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
            <div style="width: 100%; margin: 0px auto;">
                <dx:ASPxGridView Styles-AlternatingRow-BackColor="LightGray" Style="margin: 0 auto;
                    width: 100%;" ID="grid" ClientInstanceName="grid" runat="server" DataSourceID="AccessDataSource1"
                    OnCustomCallback="grid_CustomCallback">
                    <Settings ShowHorizontalScrollBar="true" ShowTitlePanel="true" ShowHeaderFilterButton="true"
                        ShowGroupFooter="Hidden" ShowFooter="true" ShowGroupedColumns="true" ShowGroupPanel="false"
                        ShowFilterBar="Visible" ShowFilterRow="true" ShowFilterRowMenu="true" />
                    <SettingsPager Mode="ShowAllRecords">
                    </SettingsPager>
                    <SettingsText Title="Third Column Book" />
                    <Columns>
                        <dx:GridViewDataColumn FieldName="Date" Width="15%" Caption="Date">
                        </dx:GridViewDataColumn>
                        <dx:GridViewDataColumn FieldName="GROUPNO" Width="15%" Caption="Particulars">
                        </dx:GridViewDataColumn>
                        <dx:GridViewDataColumn FieldName="Credit" Width="20%" Caption="Credit">
                        </dx:GridViewDataColumn>
                        <dx:GridViewDataColumn FieldName="Debit" Width="20%" Caption="Debit">
                        </dx:GridViewDataColumn>
                        <dx:GridViewDataColumn FieldName="CrDr" Width="15%" Caption="Cr/Dr">
                        </dx:GridViewDataColumn>
                        <dx:GridViewDataColumn FieldName="Balance" Width="15%" Caption="Balance">
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
                <dx:ASPxGridViewExporter ID="gridExport" runat="server" GridViewID="grid">
                    <Styles>
                        <Header Wrap="True" HorizontalAlign="Center">
                        </Header>
                        <Cell HorizontalAlign="Left" Wrap="True">
                        </Cell>
                        <Footer HorizontalAlign="Left" Wrap="True">
                        </Footer>
                    </Styles>
                </dx:ASPxGridViewExporter>
                <asp:SqlDataSource runat="server" ID="AccessDataSource1" ProviderName="MySql.Data.MySqlClient" />
            </div>
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
