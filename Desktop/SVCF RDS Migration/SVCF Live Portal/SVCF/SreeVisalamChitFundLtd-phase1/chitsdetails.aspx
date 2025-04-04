<%@ Page Culture="en-GB" Title="SVCF Admin Panel" Language="C#" MasterPageFile="~/Branch.Master"
    AutoEventWireup="true" CodeBehind="chitsdetails.aspx.cs" Inherits="SreeVisalamChitFundLtd_phase1.chitsdetails" %>

<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGlobalEvents" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxTabControl" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxMenu" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1.Export, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView.Export" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxClasses" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <!-- Foundation framework -->
    <link rel="stylesheet" href="pertho_admin_v1.3/foundation/stylesheets/foundation.css" />
    <!-- jquery UI -->
    <link rel="stylesheet" href="pertho_admin_v1.3/lib/jQueryUI/css/Aristo/Aristo.css"
        media="all" />
    <!-- fancybox -->
    <link rel="stylesheet" href="pertho_admin_v1.3/lib/fancybox/jquery.fancybox-1.3.4.css"
        media="all" />
    <link rel="stylesheet" href="pertho_admin_v1.3/css/style.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMainContent" runat="server">
    <div class="row">
        <div class="twelve columns">
            <div class="box_c">
                <div class="box_c_heading cf">
                    <p class="sepV_a">
                        Chits Details</p>
                </div>
                <div class="box_c_content">
                    <div class="row">
                        <div style="margin-top: -0.5em; margin-bottom: 0.6em;">
                            <asp:Panel ID="Panel1" runat="server" DefaultButton="BtnStatisticsGo">
                                <div style="display: table-cell; vertical-align: top; padding-top: 7px; padding-right: 5px !important;">
                                    <asp:Label Style="font-family: Times New Roman;" Font-Size="Medium" ID="Label1" runat="server"
                                        Text="Group "></asp:Label>
                                </div>
                                <div style="display: table-cell; vertical-align: top; padding-top: 4px; padding-right: 5px !important;">
                                    <asp:DropDownList ID="ddlGroupNumber" Width="150px" CssClass="chzn-select" runat="server">
                                        <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div style="display: table-cell; vertical-align: top; padding-top: 2px; padding-right: 5px !important;">
                                    <asp:Button Style="font-family: Times New Roman;" TabIndex="4" ValidationGroup="directbranch1"
                                        CssClass="GreenyPushButton" ID="BtnStatisticsGo" Font-Bold="true" runat="server"
                                        class="btn" OnClick="BtnStatisticsGo_Click" Text="Load"></asp:Button>
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
                        <dx:ASPxPageControl Width="100%" ID="carTabPage" TabAlign="Left" TabPosition="Top"
                            ActivateTabPageAction="Click" runat="server" ActiveTabIndex="0" EnableHierarchyRecreation="True">
                            <TabPages>
                                <dx:TabPage Text="Suggested Members">
                                    <ContentCollection>
                                        <dx:ContentControl ID="ContentControl1" runat="server">
                                            <dx:ASPxGridView Style="margin: 0 auto;" ID="gridSuggest" ClientInstanceName="gridSuggest"
                                                runat="server" DataSourceID="dsSuggested" Width="100%" AutoGenerateColumns="true"
                                                ShowSelectCheckbox="true">
                                                <SettingsBehavior ProcessSelectionChangedOnServer="True" AllowGroup="false" AllowDragDrop="false" />
                                                <SettingsPager Mode="ShowPager" PageSize="100">
                                                </SettingsPager>
                                                <Styles Header-Wrap="True">
                                                </Styles>
                                                <Settings ShowFooter="true" ShowFilterBar="Visible" ShowFilterRowMenu="true" ShowHeaderFilterButton="true"
                                                    ShowFilterRow="true" />
                                                <SettingsText Title="Suggested Members" />
                                                <Columns>
                                                    <dx:GridViewDataColumn CellStyle-HorizontalAlign="Left" FieldName="subscriber" Width="50%"
                                                        HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" HeaderStyle-Wrap="True"
                                                        Caption="Subscriber" />
                                                    <dx:GridViewDataColumn FieldName="SuggestedDate" Width="10%" HeaderStyle-HorizontalAlign="Center"
                                                        HeaderStyle-VerticalAlign="Middle" Caption="Suggested Date" />
                                                    <dx:GridViewDataColumn FieldName="ApprovedDate" Width="10%" HeaderStyle-HorizontalAlign="Center"
                                                        HeaderStyle-VerticalAlign="Middle" Caption="Approved Date" />
                                                    <dx:GridViewDataColumn FieldName="Nooftokens" Width="15%" HeaderStyle-HorizontalAlign="Center"
                                                        HeaderStyle-VerticalAlign="Middle" Caption="No.of tokens Suggested" />
                                                    <dx:GridViewDataColumn FieldName="NoofTokensApproved" Width="15%" HeaderStyle-HorizontalAlign="Center"
                                                        HeaderStyle-VerticalAlign="Middle" Caption="No.of tokens Approved" />
                                                </Columns>
                                            </dx:ASPxGridView>
                                            <dx:ASPxGridViewExporter ID="gridExport" runat="server" GridViewID="gridSuggest">
                                                <Styles>
                                                    <Header Wrap="True" HorizontalAlign="Center">
                                                    </Header>
                                                    <Cell HorizontalAlign="Left" Wrap="True">
                                                    </Cell>
                                                    <Footer Wrap="True" HorizontalAlign="Left">
                                                    </Footer>
                                                </Styles>
                                            </dx:ASPxGridViewExporter>
                                            <asp:SqlDataSource runat="server" ID="dsSuggested" ProviderName="MySql.Data.MySqlClient"
                                                SelectCommand=" " />
                                        </dx:ContentControl>
                                    </ContentCollection>
                                </dx:TabPage>
                                <dx:TabPage Text="Approved Members">
                                    <ContentCollection>
                                        <dx:ContentControl ID="ContentControl2" runat="server">
                                            <dx:ASPxGridView Style="margin: 0 auto;" ID="gridApprove" ClientInstanceName="gridApprove"
                                                runat="server" DataSourceID="dsApproved" Width="100%" AutoGenerateColumns="false">
                                                <SettingsBehavior ProcessSelectionChangedOnServer="True" AllowGroup="false" AllowDragDrop="false" />
                                                <SettingsPager Mode="ShowPager" PageSize="100">
                                                </SettingsPager>
                                                <Settings ShowFilterBar="Visible" ShowFilterRowMenu="true" ShowHeaderFilterButton="true"
                                                    ShowFilterRow="true" />
                                                <SettingsText Title="Current Members" />
                                                <Columns>
                                                    <dx:GridViewDataColumn CellStyle-HorizontalAlign="Left" FieldName="GrpMemberID" Width="15%"
                                                        HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" HeaderStyle-Wrap="True"
                                                        Caption="Token" VisibleIndex="2" />
                                                    <dx:GridViewDataColumn FieldName="MemberID" Width="15%" VisibleIndex="4" HeaderStyle-HorizontalAlign="Center"
                                                        HeaderStyle-VerticalAlign="Middle" Caption="MemberID" />
                                                    <dx:GridViewDataColumn FieldName="CustomerName" Width="25%" VisibleIndex="5" HeaderStyle-HorizontalAlign="Center"
                                                        HeaderStyle-VerticalAlign="Middle" Caption="Name" />
                                                    <dx:GridViewDataColumn FieldName="ResidentialAddress" Width="45%" VisibleIndex="6"
                                                        HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" Caption="Address" />
                                                </Columns>
                                            </dx:ASPxGridView>
                                            <dx:ASPxGridViewExporter ID="gridExport1" runat="server" GridViewID="gridApprove">
                                                <Styles>
                                                    <Header Wrap="True" HorizontalAlign="Center">
                                                    </Header>
                                                    <Cell HorizontalAlign="Left" Wrap="True">
                                                    </Cell>
                                                    <Footer Wrap="True" HorizontalAlign="Left">
                                                    </Footer>
                                                </Styles>
                                            </dx:ASPxGridViewExporter>
                                            <asp:SqlDataSource runat="server" ID="dsApproved" ProviderName="MySql.Data.MySqlClient"
                                                SelectCommand=" " />
                                        </dx:ContentControl>
                                    </ContentCollection>
                                </dx:TabPage>
                            </TabPages>
                        </dx:ASPxPageControl>
                    </div>
                </div>
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
