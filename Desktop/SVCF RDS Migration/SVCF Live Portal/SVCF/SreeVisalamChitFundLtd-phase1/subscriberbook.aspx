<%@ Page Culture="en-GB" Title="SVCF Admin Panel" Language="C#" MasterPageFile="~/Branch.Master"
    AutoEventWireup="true" CodeBehind="subscriberbook.aspx.cs" Inherits="SreeVisalamChitFundLtd_phase1.subscriberbook" %>

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
                        Subscriber Book</p>
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
                                    <asp:DropDownList ID="ddlGroupNumber" Width="150px" CssClass="chzn-select" 
                                        runat="server">
                                        
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
                                <dx:TabPage Text="Subscribed Members">
                                    <ContentCollection>
                                        <dx:ContentControl ID="ContentControl1" runat="server">
                                            <dx:ASPxGridView Style="margin: 0 auto;" ID="gridSubscribed" ClientInstanceName="grid" runat="server"
                                                DataSourceID="dsSubscribed" Width="100%" AutoGenerateColumns="true" ShowSelectCheckbox="true"
                                                >
                                                <SettingsBehavior ProcessSelectionChangedOnServer="True" AllowGroup="false" AllowDragDrop="false" />
                                                <SettingsPager Mode="ShowPager" PageSize="100">
                                                </SettingsPager>
                                                <Styles Header-Wrap="True"></Styles>
                                                <Settings ShowFooter="true" ShowFilterBar="Visible" ShowFilterRowMenu="true" ShowHeaderFilterButton="true"
                                                        ShowFilterRow="true" />
                                                <SettingsText Title="Suggested Members" />
                                                <Columns>
                                                    <dx:GridViewDataColumn CellStyle-HorizontalAlign="Left" FieldName="GrpMemberID" Width="10%"
                                                        HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" HeaderStyle-Wrap="True"
                                                        Caption="GrpMemberID"  />
                                                        <dx:GridViewDataColumn FieldName="MemberID" Width="10%" HeaderStyle-HorizontalAlign="Center"
                                                        HeaderStyle-VerticalAlign="Middle" Caption="Member ID" />
                                                        <dx:GridViewDataColumn FieldName="MemberName" Width="15%" HeaderStyle-HorizontalAlign="Center"
                                                        HeaderStyle-VerticalAlign="Middle" Caption="Member Name" />
                                                    <dx:GridViewDataColumn FieldName="MemberAddress" Width="20%" HeaderStyle-HorizontalAlign="Center"
                                                        HeaderStyle-VerticalAlign="Middle" Caption="Member Address" />
                                                    <dx:GridViewDataColumn FieldName="Dateofsigningthechitagreement" Width="10%"  HeaderStyle-HorizontalAlign="Center"
                                                        HeaderStyle-VerticalAlign="Middle" Caption="Date Agreement" />
                                                    <dx:GridViewDataColumn FieldName="dateofreceiptofcopyofthechitagreement" Width="10%" 
                                                        HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" Caption="Date of receipt" />
                                                    <dx:GridViewDataColumn FieldName="NoofTickets" Width="10%" 
                                                        HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" Caption="No.of Tickets" />
                                                    <dx:GridViewDataColumn FieldName="Amount" Width="10%" 
                                                        HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" Caption="Amount" />
                                                </Columns>
                                            </dx:ASPxGridView>
                                            <dx:ASPxGridViewExporter ID="gridSubscribedExport" runat="server" GridViewID="gridSubscribed">
                                                <Styles>
                                                    <Header Wrap="True" HorizontalAlign="Center">
                                                    </Header>
                                                    <Cell HorizontalAlign="Left" Wrap="True">
                                                    </Cell>
                                                    <Footer HorizontalAlign="Left" Wrap="True">
                                                    </Footer>
                                                </Styles>
                                            </dx:ASPxGridViewExporter>
                                            <asp:SqlDataSource runat="server" ID="dsSubscribed" 
                                                ProviderName="MySql.Data.MySqlClient" SelectCommand=" " />
                                        </dx:ContentControl>
                                    </ContentCollection>
                                </dx:TabPage>
                                <dx:TabPage Text="Transfer Members">
                                    <ContentCollection>
                                        <dx:ContentControl ID="ContentControl2" runat="server">
                                            <dx:ASPxGridView Style="margin: 0 auto;" ID="gridAssigned" ClientInstanceName="grid"
                                                runat="server" DataSourceID="dsAssigned" Width="100%" AutoGenerateColumns="false"
                                                >
                                                <SettingsBehavior ProcessSelectionChangedOnServer="True" AllowGroup="false" AllowDragDrop="false" />
                                                <SettingsPager Mode="ShowPager" PageSize="100">
                                                </SettingsPager>
                                                <Settings ShowFilterBar="Visible" ShowFilterRowMenu="true" ShowHeaderFilterButton="true"
                                                    ShowFilterRow="true" />
                                                <SettingsText Title="Transfer Members" />
                                                <Columns>
                                                    <dx:GridViewDataColumn CellStyle-HorizontalAlign="Left" FieldName="GrpMemberID" Width="10%"
                                                        HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" HeaderStyle-Wrap="True"
                                                        Caption="Token" VisibleIndex="2" />
                                                    <dx:GridViewDataColumn FieldName="OldMemberDetails" Width="25%" VisibleIndex="4" HeaderStyle-HorizontalAlign="Center"
                                                        HeaderStyle-VerticalAlign="Middle" Caption="Old Member Details" />
                                                    <dx:GridViewDataColumn FieldName="NewMemberDetails" Width="25%" VisibleIndex="5" HeaderStyle-HorizontalAlign="Center"
                                                        HeaderStyle-VerticalAlign="Middle" Caption="New Member Details" />
                                                    <dx:GridViewDataColumn FieldName="assigndate" Width="10%" VisibleIndex="6"
                                                        HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" Caption="Assign Date" />
                                                    <dx:GridViewDataColumn FieldName="NoofTickets" Width="10%" VisibleIndex="6"
                                                        HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" Caption="No.of Tickets " />
                                                    <dx:GridViewDataColumn FieldName="ChitValue" Width="10%" VisibleIndex="6"
                                                        HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" Caption="Amount" />
                                                    <dx:GridViewDataColumn FieldName="foremandate" Width="10%" VisibleIndex="6"
                                                        HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" Caption="Foreman Date" />
                                                </Columns>
                                            </dx:ASPxGridView>
                                            <dx:ASPxGridViewExporter ID="gridAssignedExport" runat="server" GridViewID="gridAssigned">
                                                <Styles>
                                                    <Header Wrap="True" HorizontalAlign="Center">
                                                    </Header>
                                                    <Cell HorizontalAlign="Left" Wrap="True">
                                                    </Cell>
                                                    <Footer HorizontalAlign="Left" Wrap="True">
                                                    </Footer>
                                                </Styles>
                                            </dx:ASPxGridViewExporter>
                                            <asp:SqlDataSource runat="server" ID="dsAssigned" 
                                                ProviderName="MySql.Data.MySqlClient" SelectCommand=" " />
                                        </dx:ContentControl>
                                    </ContentCollection>
                                </dx:TabPage>
                                <dx:TabPage Text="Removed Members">
                                    <ContentCollection>
                                        <dx:ContentControl ID="ContentControl3" runat="server">
                                            <dx:ASPxGridView Style="margin: 0 auto;" ID="gridSubstitued" ClientInstanceName="grid"
                                                runat="server" DataSourceID="dsSubstituted" Width="100%" AutoGenerateColumns="true"
                                                >
                                                <SettingsBehavior ProcessSelectionChangedOnServer="True" AllowGroup="false" AllowDragDrop="false" />
                                                <SettingsPager Mode="ShowPager" PageSize="100">
                                                </SettingsPager>
                                                <Settings ShowFilterBar="Visible" ShowFilterRowMenu="true" ShowHeaderFilterButton="true"
                                                    ShowFilterRow="true" />
                                                <SettingsText Title="Removed Members" />
                                                <Columns>
                                                    <dx:GridViewDataColumn FieldName="GrpMemberId" Width="10%" HeaderStyle-HorizontalAlign="Center"
                                                        HeaderStyle-VerticalAlign="Middle" Caption="Token" VisibleIndex="1">
                                                    </dx:GridViewDataColumn>
                                                    <dx:GridViewDataColumn CellStyle-HorizontalAlign="Left" FieldName="OldMember" Width="20%"
                                                        HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" HeaderStyle-Wrap="True"
                                                        Caption="Old Member Details" VisibleIndex="2" />
                                                    <dx:GridViewDataColumn FieldName="NewMember" Width="20%" VisibleIndex="4" HeaderStyle-HorizontalAlign="Center"
                                                        HeaderStyle-VerticalAlign="Middle" Caption="New Member Details" />
                                                    <dx:GridViewDataColumn FieldName="ReasonForRemoval" Width="16%" VisibleIndex="5" HeaderStyle-HorizontalAlign="Center"
                                                        HeaderStyle-VerticalAlign="Middle" Caption="Reason For Removal" />
                                                    <dx:GridViewDataColumn FieldName="substitutedate" Width="10%" VisibleIndex="6"
                                                        HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" Caption="Substitute date" />
                                                    <dx:GridViewDataColumn FieldName="Nooftickets" Width="8%" VisibleIndex="6"
                                                        HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" Caption="No.of tickets" />
                                                    <dx:GridViewDataColumn FieldName="ChitValue" Width="8%" VisibleIndex="6"
                                                        HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" Caption="Removal Date" />
                                                    <dx:GridViewDataColumn FieldName="intimationdate" Width="8%" VisibleIndex="6"
                                                        HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" Caption="Intimation Date" />

                                                </Columns>
                                            </dx:ASPxGridView>
                                            <dx:ASPxGridViewExporter ID="gridSubstituedExport" runat="server" GridViewID="gridSubstitued">
                                                <Styles>
                                                    <Header Wrap="True" HorizontalAlign="Center">
                                                    </Header>
                                                    <Cell HorizontalAlign="Left" Wrap="True">
                                                    </Cell>
                                                    <Footer HorizontalAlign="Left" Wrap="True">
                                                    </Footer>
                                                </Styles>
                                            </dx:ASPxGridViewExporter>
                                            <asp:SqlDataSource runat="server" ID="dsSubstituted" 
                                                ProviderName="MySql.Data.MySqlClient" SelectCommand=" " />
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
