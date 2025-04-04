<%@ Page Culture="en-GB" Language="C#" MasterPageFile="~/Branch.Master" AutoEventWireup="true"
    CodeBehind="TopColumnDayBook.aspx.cs" Inherits="SreeVisalamChitFundLtd_phase1.TopColumnDayBook"
    Title="Untitled Page" %>

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
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>SVCF Admin Panel</title>
    <!-- Foundation framework -->
    <link rel="stylesheet" href="pertho_admin_v1.3/foundation/stylesheets/foundation.css" />
    <!-- jquery UI -->
    <link rel="stylesheet" href="pertho_admin_v1.3/lib/jQueryUI/css/Aristo/Aristo.css"
        media="all" />
    <!-- fancybox -->
    <link rel="stylesheet" href="pertho_admin_v1.3/lib/fancybox/jquery.fancybox-1.3.4.css"
        media="all" />
    <!-- syntax highlighter -->
    <link rel="stylesheet" href="pertho_admin_v1.3/lib/syntaxhighlighter/styles/shCoreDefault.css"/>
    <!-- main styles -->
    <link rel="stylesheet" href="pertho_admin_v1.3/css/style.css"/>
    <!-- Google fonts -->
   
    <style type="text/css">
        .style_switcher
        {
            padding:10px 10px 10px 50px !important;
        }
        td[style="cursor:default;"]
        {
            vertical-align:middle;
        }
    </style>
    <script language="javascript" type="text/javascript">
        function CallPrint(strid) {
            var prtContent = document.getElementById(strid);
            var WinPrint = window.open('', '', 'letf=0,top=0,width=800,height=100,toolbar=0,scrollbars=0,status=0,dir=ltr');
            WinPrint.document.write(prtContent.innerHTML);
            WinPrint.document.close();
            WinPrint.focus();
            WinPrint.print();
            WinPrint.close();
            prtContent.innerHTML = strOldOne;
        }

        function Keepopen() {

            var jj = $('#' + '<%=lblMaintain.ClientID %>').text();
            var MaintainPost = $('#' + '<%=lblMaintainPost.ClientID %>').text();
            if (jj != "") {
                var jj1 = "#" + jj + " > a"
                $(jj1).trigger('click');

                if (jj == "li_Branch") {
                    $("html, body").animate({ scrollTop: $('#' + '<%=grid.ClientID %>').offset().top }, 1000);
                }
                else if (jj == "li_InvestMents") {
                    $("html, body").animate({ scrollTop: $('#' + '<%=gridInvestments.ClientID %>').offset().top }, 1000);
                }
                else if (jj == "li_Banks") {
                    $("html, body").animate({ scrollTop: $('#' + '<%=gridBanks.ClientID %>').offset().top }, 1000);
                }
                else if (jj == "li_OtherItems") {
                    $("html, body").animate({ scrollTop: $('#' + '<%=gridOtherItems.ClientID %>').offset().top }, 1000);
                }
                else if (jj == "li_Chits") {
                    $("html, body").animate({ scrollTop: $('#' + '<%=gridChits.ClientID %>').offset().top }, 1000);
                }
                else if (jj == "li_CompanyChits") {
                    $("html, body").animate({ scrollTop: $('#' + '<%=gridForemanChits.ClientID %>').offset().top }, 1000);
                }
                else if (jj == "li_DecreeDebtors") {
                    $("html, body").animate({ scrollTop: $('#' + '<%=gridDecreeDebtors.ClientID %>').offset().top }, 1000);
                }
                else if (jj == "li_Loans") {
                    $("html, body").animate({ scrollTop: $('#' + '<%=gridLoans.ClientID %>').offset().top }, 1000);
                }
                else if (jj == "li_Advances") {
                    $("html, body").animate({ scrollTop: $('#' + '<%=gridSundriesAndAdvances.ClientID %>').offset().top }, 1000);
                }
                else if (jj == "li_Stamps") {
                    $("html, body").animate({ scrollTop: $('#' + '<%=gridStamps.ClientID %>').offset().top }, 1000);
                }
                else if (jj == "li_PandL") {
                    $("html, body").animate({ scrollTop: $('#' + '<%=gridProfitAndLoss.ClientID %>').offset().top }, 1000);
                }
                else if (jj == "li1") {
                    $("html, body").animate({ scrollTop: $('#' + '<%=gridCash.ClientID %>').offset().top }, 1000);
                }
                if (MaintainPost == "post") {
                    __doPostBack('ctl00$cphMainContent$mMain', 'CLICK:0');
                }
            }
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMainContent" runat="server">
    <ajax:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </ajax:ToolkitScriptManager>
    <asp:Label Style="display: none;" runat="server" ID="lblMaintain" Text=""></asp:Label>
    <asp:Label Style="display: none;" runat="server" ID="lblMaintainPost" Text=""></asp:Label>
    <asp:HiddenField runat="server" ID="hfBranch" Value="" />
    <asp:HiddenField runat="server" ID="hfInvestment" Value="" />
    <asp:HiddenField runat="server" ID="hfBank" Value="" />
    <asp:HiddenField runat="server" ID="hfOtherItems" Value="" />
    <asp:HiddenField runat="server" ID="hfChits" Value="" />
    <asp:HiddenField runat="server" ID="hfForemanChits" Value="" />
    <asp:HiddenField runat="server" ID="hfDecreeDebtors" Value="" />
    <asp:HiddenField runat="server" ID="hfLoans" Value="" />
    <asp:HiddenField runat="server" ID="hfAdvances" Value="" />
    <asp:HiddenField runat="server" ID="hfStamps" Value="" />
    <asp:HiddenField runat="server" ID="hfProfitAndLoss" Value="" />
    <asp:HiddenField runat="server" ID="hfCash" Value="" />
    <a class="ssw_trigger" href="javascript:void(0)"></a>
    <div class="style_switcher">
    <asp:LinkButton ID="LinkButton1" CssClass="gh_button" runat="server" OnClick="btnConsolidatedView_Click" Text="Consolidated View"></asp:LinkButton>
    </div>
    <div class="container">
        <div class="row">
            <div class="twelve columns">
                <div class="box_c">
                    <div class="box_c_heading cf box_actions">
                        <p>
                            Top Column Daybook</p>
                    </div>
                    <div id="bill" class="box_c_content">
                        <h3 class="sepH_a_line">
                            Sree Visalam Chit Funds Detailed View</h3>
                        <div class="row">
                            <div style="margin: 0 auto;">
                                <div style="width: 100%;">
                                    <asp:Panel ID="Panel1" runat="server" DefaultButton="btnLoanConsolidated1">
                                        <div style="display: table-cell; padding-right: 8px;">
                                            <asp:Label Font-Size="Small" Font-Bold="true" runat="server" Text="Date:" ID="fdjfhfhf"></asp:Label>
                                        </div>
                                        <div style="display: table-cell; padding-right: 8px;">
                                            <asp:TextBox TabIndex="1" placeholder="Date" ID="dateToConsolidated" runat="server"
                                                Width="100px" CssClass="input-text maskdate">
                                            </asp:TextBox>
                                            <asp:RequiredFieldValidator runat="server" Display="Dynamic" ControlToValidate="dateToConsolidated"
                                                ID="ssssss"></asp:RequiredFieldValidator>
                                            <asp:CompareValidator ID="CompareValidator1" ControlToValidate="dateToConsolidated" Display="Dynamic" runat="server"
                                                Operator="DataTypeCheck" Type="Date"></asp:CompareValidator>
                                        </div>
                                        <div style="display: table-cell; width: 100%;">
                                            <asp:Button TabIndex="2" CssClass="GreenyPushButton" ID="btnLoanConsolidated1" OnClick="btnLoanConsolidated_Click"
                                                runat="server" Text="Load"></asp:Button>
                                        </div>
                                        <div style="display: table-cell; vertical-align: top; float: right; padding-right: 5px;
                                            text-align: right; margin-top: -35px;">
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
                                <div id="st-documentation" class="st-accordion">
                                    <ul>
                                        <li id="li_Branch" class="top_H"><a href="#" class="top_Ha">01. Branch.<span class="st-arrow">Open
                                            or Close</span></a>
                                            <asp:Panel ID="pnlBranch" CssClass="st-content" runat="server">
                                                <div style="background: #fff !important;" class="row display">
                                                    <div style="background: #fff !important;" class="four columns">
                                                        <p class="inner_heading">
                                                            Yesterday Balance On Branch</p>
                                                        <ul class="overview_list">
                                                            <li>
                                                                <asp:LinkButton runat="server" OnClick="aCrBranchAmountprDay_Click" ID="aCrBranchAmountprDay">
                                                                    <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                                        alt="" />
                                                                    <asp:Label runat="server" ID="lblCrBranchAmountprDay" Text="0.00" class="ov_nb"></asp:Label>
                                                                    <span class="ov_text">Cr Amount</span>
                                                                </asp:LinkButton></li>
                                                            <li>
                                                                <asp:LinkButton runat="server" OnClick="aDrBranchAmountprDay_Click" ID="aDrBranchAmountprDay">
                                                                    <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                                        alt="" />
                                                                    <asp:Label runat="server" ID="lblDrBranchAmountprDay" Text="0.00" class="ov_nb"></asp:Label>
                                                                    <span class="ov_text">Dr Amount</span>
                                                                </asp:LinkButton></li>
                                                            <li>
                                                                <asp:LinkButton runat="server" OnClick="TotalBranchpr_Click" ID="aCrbBranchAmountprDay">
                                                                    <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                                        alt="" />
                                                                    <asp:Label runat="server" ID="lblCrBranchBalanceprDay" Text="0.00" class="ov_nb"></asp:Label>
                                                                    <span class="ov_text">Cr Balance</span>
                                                                </asp:LinkButton></li>
                                                            <li>
                                                                <asp:LinkButton runat="server" OnClick="TotalBranchpr_Click" ID="aDrbBranchAmountprDay">
                                                                    <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                                        alt="" />
                                                                    <asp:Label runat="server" ID="lblDrBranchBalanceprDay" Text="0.00" class="ov_nb"></asp:Label>
                                                                    <span class="ov_text">Dr Balance </span>
                                                                </asp:LinkButton></li>
                                                        </ul>
                                                    </div>
                                                    <div style="background: #fff !important;" class="four columns">
                                                        <p class="inner_heading">
                                                            Today Balance On Branch</p>
                                                        <ul class="overview_list">
                                                            <li>
                                                                <asp:LinkButton runat="server" OnClick="aCrBranchAmountcrDay_Click" ID="aCrBranchAmountcrDay">
                                                                    <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                                        alt="" />
                                                                    <asp:Label runat="server" ID="lblCrBranchAmountFinal" Text="0.00" class="ov_nb"></asp:Label>
                                                                    <span class="ov_text">Cr Amount</span>
                                                                </asp:LinkButton></li>
                                                            <li>
                                                                <asp:LinkButton runat="server" OnClick="aDrBranchAmountcrDay_Click" ID="aDrBranchAmountcrDay">
                                                                    <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                                        alt="" />
                                                                    <asp:Label runat="server" ID="lblDrBranchAmountFinal" Text="0.00" class="ov_nb"></asp:Label>
                                                                    <span class="ov_text">Dr Amount</span>
                                                                </asp:LinkButton></li>
                                                            <li>
                                                                <asp:LinkButton runat="server" OnClick="TotalBranchcr_Click" ID="aCrbBranchAmountcrDay">
                                                                    <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                                        alt="" />
                                                                    <asp:Label runat="server" ID="lblCrBranchBalanceFinal" Text="0.00" class="ov_nb"></asp:Label>
                                                                    <span class="ov_text">Cr Balance</span>
                                                                </asp:LinkButton></li>
                                                            <li>
                                                                <asp:LinkButton runat="server" OnClick="TotalBranchcr_Click" ID="aDrbBranchAmountcrDay">
                                                                    <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                                        alt="" />
                                                                    <asp:Label runat="server" ID="lblDrBranchBalanceFinal" Text="0.00" class="ov_nb"></asp:Label>
                                                                    <span class="ov_text">Dr Balance</span>
                                                                </asp:LinkButton></li>
                                                        </ul>
                                                    </div>
                                                    <div style="background: #fff !important;" class="four columns">
                                                        <p class="inner_heading">
                                                            Today Transaction On Branch</p>
                                                        <ul class="overview_list">
                                                            <li>
                                                                <asp:LinkButton runat="server" OnClick="aCrBranchAmounttoDay_Click" ID="aCrBranchAmounttoDay">
                                                                    <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                                        alt="" />
                                                                    <asp:Label runat="server" ID="lblCrBranchAmount" Text="0.00" class="ov_nb"></asp:Label>
                                                                    <span class="ov_text">Cr Amount</span>
                                                                </asp:LinkButton></li>
                                                            <li>
                                                                <asp:LinkButton runat="server" OnClick="aDrBranchAmounttoDay_Click" ID="aDrBranchAmounttoDay">
                                                                    <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                                        alt="" />
                                                                    <asp:Label runat="server" ID="lblDrBranchAmount" Text="0.00" class="ov_nb"></asp:Label>
                                                                    <span class="ov_text">Dr Amount</span>
                                                                </asp:LinkButton></li>
                                                            <li>
                                                                <asp:LinkButton runat="server" OnClick="TotalBranchto_Click" ID="aCrbBranchAmounttoDay">
                                                                    <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                                        alt="" />
                                                                    <asp:Label runat="server" ID="lblCrBranchBalance" Text="0.00" class="ov_nb"></asp:Label>
                                                                    <span class="ov_text">Cr Balance</span>
                                                                </asp:LinkButton></li>
                                                            <li>
                                                                <asp:LinkButton runat="server" OnClick="TotalBranchto_Click" ID="aDrbBranchAmounttoDay">
                                                                    <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                                        alt="" />
                                                                    <asp:Label runat="server" ID="lblDrBranchBalance" Text="0.00" class="ov_nb"></asp:Label>
                                                                    <span class="ov_text">Dr Balance</span>
                                                                </asp:LinkButton></li>
                                                        </ul>
                                                    </div>
                                                </div>
                                                <dx:ASPxGridView EnableRowsCache="true" 
                                                    Style="margin: 0 auto; width: 100%;" OnSummaryDisplayText="ASPxGridView1_SummaryDisplayTextBranch"
                                                    OnCustomSummaryCalculate="ASPxGridView1_OnCustomSummaryCalculateBranch" ID="grid"
                                                    ClientInstanceName="grid" runat="server" DataSourceID="MySqldsBranch" OnCustomCallback="grid_CustomCallback">
                                                    <Settings ShowTitlePanel="true" ShowVerticalScrollBar="true" VerticalScrollBarStyle="Virtual"
                                                        VerticalScrollableHeight="300" ShowHeaderFilterButton="true" ShowFooter="true"
                                                        ShowGroupFooter="Hidden" ShowGroupPanel="false" ShowGroupedColumns="true" ShowFilterBar="Visible"
                                                        ShowFilterRow="true" />
                                                    <SettingsText Title="Branches" />
                                                    <Templates>
                                                        <GroupRowContent>
                                                            <dx:ASPxLabel ID="GroupLabel" runat="server" Text='<%# GetLabelText(Container)%>'>
                                                            </dx:ASPxLabel>
                                                        </GroupRowContent>
                                                    </Templates>
                                                    <TotalSummary>
                                                        <dx:ASPxSummaryItem FieldName="Credit" SummaryType="Sum" />
                                                        <dx:ASPxSummaryItem FieldName="Debit" SummaryType="Sum" />
                                                        <dx:ASPxSummaryItem FieldName="Narration" SummaryType="Custom" />
                                                    </TotalSummary>
                                                    <SettingsPager Mode="ShowAllRecords">
                                                    </SettingsPager>
                                                    <Columns>
                                                        <dx:GridViewDataColumn FieldName="Date">
                                                        </dx:GridViewDataColumn>
                                                        <dx:GridViewDataColumn FieldName="Branch">
                                                        </dx:GridViewDataColumn>
                                                        <dx:GridViewDataColumn FieldName="LedgerHead">
                                                        </dx:GridViewDataColumn>
                                                        <dx:GridViewDataColumn FieldName="Narration">
                                                        </dx:GridViewDataColumn>
                                                        <dx:GridViewDataColumn FieldName="Credit">
                                                        </dx:GridViewDataColumn>
                                                        <dx:GridViewDataColumn FieldName="Debit">
                                                        </dx:GridViewDataColumn>
                                                    </Columns>
                                                    <Styles  Footer-HorizontalAlign="Left" Cell-HorizontalAlign="Left" Header-VerticalAlign="Middle" Header-HorizontalAlign="Center"
                                                        Header-Wrap="True">
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
                                                        <Footer Wrap="True" HorizontalAlign="Left"></Footer>
                                                    </Styles>
                                                </dx:ASPxGridViewExporter>
                                                <asp:SqlDataSource runat="server" ID="MySqldsBranch" 
                                                    ProviderName="MySql.Data.MySqlClient" />
                                            </asp:Panel>
                                        </li>
                                        <li id="li_InvestMents" class="top_H"><a href="#" class="top_Ha">02. Investments.<span
                                            class="st-arrow">Open or Close</span></a>
                                            <div class="st-content">
                                                <div style="background: #fff !important;" class="row display">
                                                    <div style="background: #fff !important;" class="four columns">
                                                        <p class="inner_heading">
                                                            Yesterday Balance On Investments</p>
                                                        <ul class="overview_list">
                                                            <li>
                                                                <asp:LinkButton runat="server" OnClick="aCrInvestmentAmountprDay_Click" ID="aCrInvestmentAmountprDay">
                                                                    <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                                        alt="" />
                                                                    <asp:Label runat="server" ID="lblCrInvestmentsAmountprDay" Text="0.00" class="ov_nb"></asp:Label>
                                                                    <span class="ov_text">Cr Amount</span>
                                                                </asp:LinkButton></li>
                                                            <li>
                                                                <asp:LinkButton runat="server" OnClick="aDrInvestmentAmountprDay_Click" ID="aDrInvestmentAmountprDay">
                                                                    <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                                        alt="" />
                                                                    <asp:Label runat="server" ID="lblDrInvestmentsAmountprDay" Text="0.00" class="ov_nb"></asp:Label>
                                                                    <span class="ov_text">Dr Amount</span>
                                                                </asp:LinkButton></li>
                                                            <li>
                                                                <asp:LinkButton runat="server" OnClick="TotalInvestmentpr_Click" ID="aCrbInvestmentAmountprDay">
                                                                    <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                                        alt="" />
                                                                    <asp:Label runat="server" ID="lblCrInvestmentsBalanceprDay" Text="0.00" class="ov_nb"></asp:Label>
                                                                    <span class="ov_text">Cr Balance</span>
                                                                </asp:LinkButton></li>
                                                            <li>
                                                                <asp:LinkButton runat="server" OnClick="TotalInvestmentpr_Click" ID="aDrbInvestmentAmountprDay">
                                                                    <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                                        alt="" />
                                                                    <asp:Label runat="server" ID="lblDrInvestmentsBalanceprDay" Text="0.00" class="ov_nb"></asp:Label>
                                                                    <span class="ov_text">Dr Balance </span>
                                                                </asp:LinkButton></li>
                                                        </ul>
                                                    </div>
                                                    <div style="background: #fff !important;" class="four columns">
                                                        <p class="inner_heading">
                                                            Today Balance On Investments</p>
                                                        <ul class="overview_list">
                                                            <li>
                                                                <asp:LinkButton runat="server" OnClick="aCrInvestmentsAmountcrDay_Click" ID="aCrInvestmentsAmountcrDay">
                                                                    <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                                        alt="" />
                                                                    <asp:Label runat="server" ID="lblCrInvestmentsAmountFinal" Text="0.00" class="ov_nb"></asp:Label>
                                                                    <span class="ov_text">Cr Amount</span>
                                                                </asp:LinkButton></li>
                                                            <li>
                                                                <asp:LinkButton runat="server" OnClick="aDrInvestmentsAmountcrDay_Click" ID="aDrInvestmentsAmountcrDay">
                                                                    <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                                        alt="" />
                                                                    <asp:Label runat="server" ID="lblDrInvestmentsAmountFinal" Text="0.00" class="ov_nb"></asp:Label>
                                                                    <span class="ov_text">Dr Amount</span>
                                                                </asp:LinkButton></li>
                                                            <li>
                                                                <asp:LinkButton runat="server" OnClick="TotalInvestmentcr_Click" ID="aCrbInvestmentsAmountcrDay">
                                                                    <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                                        alt="" />
                                                                    <asp:Label runat="server" ID="lblCrInvestmentsBalanceFinal" Text="0.00" class="ov_nb"></asp:Label>
                                                                    <span class="ov_text">Cr Balance</span>
                                                                </asp:LinkButton></li>
                                                            <li>
                                                                <asp:LinkButton runat="server" OnClick="TotalInvestmentcr_Click" ID="aDrbInvestmentsAmountcrDay">
                                                                    <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                                        alt="" />
                                                                    <asp:Label runat="server" ID="lblDrInvestmentsBalanceFinal" Text="0.00" class="ov_nb"></asp:Label>
                                                                    <span class="ov_text">Dr Balance</span>
                                                                </asp:LinkButton></li>
                                                        </ul>
                                                    </div>
                                                    <div style="background: #fff !important;" class="four columns">
                                                        <p class="inner_heading">
                                                            Today Transaction On Investments</p>
                                                        <ul class="overview_list">
                                                            <li>
                                                                <asp:LinkButton runat="server" OnClick="aCrInvestmentsAmounttoDay_Click" ID="aCrInvestmentsAmounttoDay">
                                                                    <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                                        alt="" />
                                                                    <asp:Label runat="server" ID="lblCrInvestmentsAmount" Text="0.00" class="ov_nb"></asp:Label>
                                                                    <span class="ov_text">Cr Amount</span>
                                                                </asp:LinkButton></li>
                                                            <li>
                                                                <asp:LinkButton runat="server" OnClick="aDrInvestmentsAmounttoDay_Click" ID="aDrInvestmentsAmounttoDay">
                                                                    <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                                        alt="" />
                                                                    <asp:Label runat="server" ID="lblDrInvestmentsAmount" Text="0.00" class="ov_nb"></asp:Label>
                                                                    <span class="ov_text">Dr Amount</span>
                                                                </asp:LinkButton></li>
                                                            <li>
                                                                <asp:LinkButton runat="server" OnClick="TotalInvestmentto_Click" ID="aCrbInvestmentsAmounttoDay">
                                                                    <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                                        alt="" />
                                                                    <asp:Label runat="server" ID="lblCrInvestmentsBalance" Text="0.00" class="ov_nb"></asp:Label>
                                                                    <span class="ov_text">Cr Balance</span>
                                                                </asp:LinkButton></li>
                                                            <li>
                                                                <asp:LinkButton runat="server" OnClick="TotalInvestmentto_Click" ID="aDrbInvestmentsAmounttoDay">
                                                                    <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                                        alt="" />
                                                                    <asp:Label runat="server" ID="lblDrInvestmentsBalance" Text="0.00" class="ov_nb"></asp:Label>
                                                                    <span class="ov_text">Dr Balance</span>
                                                                </asp:LinkButton></li>
                                                        </ul>
                                                    </div>
                                                </div>
                                                <dx:ASPxGridView  Style="margin: 0 auto;
                                                    width: 100%;" OnSummaryDisplayText="ASPxGridView1_SummaryDisplayTextInvestments"
                                                    OnCustomSummaryCalculate="ASPxGridView1_OnCustomSummaryCalculateInvestment" ID="gridInvestments"
                                                    ClientInstanceName="gridInvestments" runat="server" DataSourceID="MySqldsInvestments"
                                                    OnCustomCallback="grid_CustomCallback">
                                                    <Settings ShowTitlePanel="true" VerticalScrollableHeight="300" ShowVerticalScrollBar="true"
                                                        VerticalScrollBarStyle="Standard" ShowHeaderFilterButton="true" ShowFooter="true"
                                                        ShowGroupFooter="Hidden" ShowGroupPanel="false" ShowGroupedColumns="true" ShowFilterBar="Visible"
                                                        ShowFilterRow="true" />
                                                    <SettingsText Title="Investments" />
                                                    <TotalSummary>
                                                        <dx:ASPxSummaryItem FieldName="Credit" SummaryType="Sum" />
                                                        <dx:ASPxSummaryItem FieldName="Debit" SummaryType="Sum" />
                                                        <dx:ASPxSummaryItem FieldName="Narration" SummaryType="Custom" />
                                                    </TotalSummary>
                                                    <SettingsPager Mode="ShowAllRecords">
                                                    </SettingsPager>
                                                    <Templates>
                                                        <GroupRowContent>
                                                            <dx:ASPxLabel ID="GroupLabel" runat="server" Text='<%# GetLabelText(Container)%>'>
                                                            </dx:ASPxLabel>
                                                        </GroupRowContent>
                                                    </Templates>
                                                    <Columns>
                                                        <dx:GridViewDataColumn FieldName="Date">
                                                        </dx:GridViewDataColumn>
                                                        <dx:GridViewDataColumn FieldName="Head">
                                                        </dx:GridViewDataColumn>
                                                        <dx:GridViewDataColumn FieldName="LedgerHead">
                                                        </dx:GridViewDataColumn>
                                                        <dx:GridViewDataColumn FieldName="Narration">
                                                        </dx:GridViewDataColumn>
                                                        <dx:GridViewDataColumn FieldName="Credit">
                                                        </dx:GridViewDataColumn>
                                                        <dx:GridViewDataColumn FieldName="Debit">
                                                        </dx:GridViewDataColumn>
                                                    </Columns>
                                                    <Styles  Footer-HorizontalAlign="Left" Cell-HorizontalAlign="Left" Header-VerticalAlign="Middle" Header-HorizontalAlign="Center"
                                                        Header-Wrap="True">
                                                        <GroupPanel Wrap="True" HorizontalAlign="Left">
                                                        </GroupPanel>
                                                        <GroupRow Wrap="True" HorizontalAlign="Left">
                                                        </GroupRow>
                                                    </Styles>
                                                </dx:ASPxGridView>
                                                <dx:ASPxGridViewExporter ID="geInvestmentsExport" runat="server" GridViewID="gridInvestments">
                                                    <Styles>
                                                        <Header Wrap="True" HorizontalAlign="Center">
                                                        </Header>
                                                        <Cell HorizontalAlign="Left" Wrap="True">
                                                        </Cell>
                                                        <Footer Wrap="True" HorizontalAlign="Left"></Footer>
                                                    </Styles>
                                                </dx:ASPxGridViewExporter>
                                                <asp:SqlDataSource runat="server" ID="MySqldsInvestments" 
                                                    ProviderName="MySql.Data.MySqlClient" />
                                            </div>
                                        </li>
                                        <li id="li_Banks" class="top_H"><a href="#" class="top_Ha">03. Banks.<span class="st-arrow">Open
                                            or Close</span></a>
                                            <div class="st-content">
                                                <div style="background: #fff !important;" class="row display">
                                                    <div style="background: #fff !important;" class="four columns">
                                                        <p class="inner_heading">
                                                            Yesterday Balance On Banks</p>
                                                        <ul class="overview_list">
                                                            <li>
                                                                <asp:LinkButton runat="server" OnClick="aCrBanksAmountprDay_Click" ID="aCrBanksAmountprDay">
                                                                    <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                                        alt="" />
                                                                    <asp:Label runat="server" ID="lblCrBanksAmountprDay" Text="0.00" class="ov_nb"></asp:Label>
                                                                    <span class="ov_text">Cr Amount</span>
                                                                </asp:LinkButton></li>
                                                            <li>
                                                                <asp:LinkButton runat="server" OnClick="aDrBanksAmountprDay_Click" ID="aDrBanksAmountprDay">
                                                                    <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                                        alt="" />
                                                                    <asp:Label runat="server" ID="lblDrBanksAmountprDay" Text="0.00" class="ov_nb"></asp:Label>
                                                                    <span class="ov_text">Dr Amount</span>
                                                                </asp:LinkButton></li>
                                                            <li>
                                                                <asp:LinkButton runat="server" OnClick="TotalBankspr_Click" ID="aCrbBanksAmountprDay">
                                                                    <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                                        alt="" />
                                                                    <asp:Label runat="server" ID="lblCrBanksBalanceprDay" Text="0.00" class="ov_nb"></asp:Label>
                                                                    <span class="ov_text">Cr Balance</span>
                                                                </asp:LinkButton></li>
                                                            <li>
                                                                <asp:LinkButton runat="server" OnClick="TotalBankspr_Click" ID="aDrbBanksAmountprDay">
                                                                    <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                                        alt="" />
                                                                    <asp:Label runat="server" ID="lblDrBanksBalanceprDay" Text="0.00" class="ov_nb"></asp:Label>
                                                                    <span class="ov_text">Dr Balance </span>
                                                                </asp:LinkButton></li>
                                                        </ul>
                                                    </div>
                                                    <div style="background: #fff !important;" class="four columns">
                                                        <p class="inner_heading">
                                                            Today Balance On Banks</p>
                                                        <ul class="overview_list">
                                                            <li>
                                                                <asp:LinkButton runat="server" OnClick="aCrBanksAmountcrDay_Click" ID="aCrBanksAmountcrDay">
                                                                    <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                                        alt="" />
                                                                    <asp:Label runat="server" ID="lblCrBanksAmountFinal" Text="0.00" class="ov_nb"></asp:Label>
                                                                    <span class="ov_text">Cr Amount</span>
                                                                </asp:LinkButton></li>
                                                            <li>
                                                                <asp:LinkButton runat="server" OnClick="aDrBanksAmountcrDay_Click" ID="aDrBanksAmountcrDay">
                                                                    <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                                        alt="" />
                                                                    <asp:Label runat="server" ID="lblDrBanksAmountFinal" Text="0.00" class="ov_nb"></asp:Label>
                                                                    <span class="ov_text">Dr Amount</span>
                                                                </asp:LinkButton></li>
                                                            <li>
                                                                <asp:LinkButton runat="server" OnClick="TotalBankscr_Click" ID="aCrbBanksAmountcrDay">
                                                                    <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                                        alt="" />
                                                                    <asp:Label runat="server" ID="lblCrBanksBalanceFinal" Text="0.00" class="ov_nb"></asp:Label>
                                                                    <span class="ov_text">Cr Balance</span>
                                                                </asp:LinkButton></li>
                                                            <li>
                                                                <asp:LinkButton runat="server" OnClick="TotalBankscr_Click" ID="aDrbBanksAmountcrDay">
                                                                    <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                                        alt="" />
                                                                    <asp:Label runat="server" ID="lblDrBanksBalanceFinal" Text="0.00" class="ov_nb"></asp:Label>
                                                                    <span class="ov_text">Dr Balance</span>
                                                                </asp:LinkButton></li>
                                                        </ul>
                                                    </div>
                                                    <div style="background: #fff !important;" class="four columns">
                                                        <p class="inner_heading">
                                                            Today Transaction On Banks</p>
                                                        <ul class="overview_list">
                                                            <li>
                                                                <asp:LinkButton runat="server" OnClick="aCrBanksAmounttoDay_Click" ID="aCrBanksAmounttoDay">
                                                                    <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                                        alt="" />
                                                                    <asp:Label runat="server" ID="lblCrBanksAmount" Text="0.00" class="ov_nb"></asp:Label>
                                                                    <span class="ov_text">Cr Amount</span>
                                                                </asp:LinkButton></li>
                                                            <li>
                                                                <asp:LinkButton runat="server" OnClick="aDrBanksAmounttoDay_Click" ID="aDrBanksAmounttoDay">
                                                                    <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                                        alt="" />
                                                                    <asp:Label runat="server" ID="lblDrBanksAmount" Text="0.00" class="ov_nb"></asp:Label>
                                                                    <span class="ov_text">Dr Amount</span>
                                                                </asp:LinkButton></li>
                                                            <li>
                                                                <asp:LinkButton runat="server" OnClick="TotalBanksto_Click" ID="aCrbBanksAmounttoDay">
                                                                    <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                                        alt="" />
                                                                    <asp:Label runat="server" ID="lblCrBanksBalance" Text="0.00" class="ov_nb"></asp:Label>
                                                                    <span class="ov_text">Cr Balance</span>
                                                                </asp:LinkButton></li>
                                                            <li>
                                                                <asp:LinkButton runat="server" OnClick="TotalBanksto_Click" ID="aDrbBanksAmounttoDay">
                                                                    <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                                        alt="" />
                                                                    <asp:Label runat="server" ID="lblDrBanksBalance" Text="0.00" class="ov_nb"></asp:Label>
                                                                    <span class="ov_text">Dr Balance</span>
                                                                </asp:LinkButton></li>
                                                        </ul>
                                                    </div>
                                                </div>
                                                <dx:ASPxGridView  Style="margin: 0 auto;
                                                    width: 100%;" OnSummaryDisplayText="ASPxGridView1_SummaryDisplayTextBanks" OnCustomSummaryCalculate="ASPxGridView1_OnCustomSummaryCalculateBanks"
                                                    ID="gridBanks" ClientInstanceName="gridBanks" runat="server" DataSourceID="MySqldsBanks"
                                                    OnCustomCallback="grid_CustomCallback">
                                                    <Settings ShowTitlePanel="true" VerticalScrollableHeight="300" ShowVerticalScrollBar="true"
                                                        VerticalScrollBarStyle="Standard" ShowHeaderFilterButton="true" ShowGroupFooter="Hidden"
                                                        ShowFooter="true" ShowGroupedColumns="true" ShowGroupPanel="false" ShowFilterBar="Visible"
                                                        ShowFilterRow="true" />
                                                    <SettingsText Title="Banks" />
                                                    <Templates>
                                                        <GroupRowContent>
                                                            <dx:ASPxLabel ID="GroupLabel" runat="server" Text='<%# GetLabelText(Container)%>'>
                                                            </dx:ASPxLabel>
                                                        </GroupRowContent>
                                                    </Templates>
                                                    <TotalSummary>
                                                        <dx:ASPxSummaryItem FieldName="Credit" SummaryType="Sum" />
                                                        <dx:ASPxSummaryItem FieldName="Debit" SummaryType="Sum" />
                                                        <dx:ASPxSummaryItem FieldName="Narration" SummaryType="Custom" />
                                                        <%--<dx:ASPxSummaryItem FieldName="ChequeNo." SummaryType="Custom" />--%>
                                                    </TotalSummary>
                                                    <SettingsPager Mode="ShowAllRecords">
                                                    </SettingsPager>
                                                    <Columns>
                                                        <dx:GridViewDataColumn FieldName="Date">
                                                        </dx:GridViewDataColumn>
                                                        <dx:GridViewDataColumn FieldName="LedgerHead">
                                                        </dx:GridViewDataColumn>
                                                        <dx:GridViewDataColumn FieldName="Bank">
                                                        </dx:GridViewDataColumn>
                                                        <dx:GridViewDataColumn FieldName="AccountNo.">
                                                        </dx:GridViewDataColumn>
                                                        <dx:GridViewDataColumn FieldName="CustomersBank">
                                                        </dx:GridViewDataColumn>
                                                        <dx:GridViewDataColumn FieldName="ChequeNo.">
                                                        </dx:GridViewDataColumn>
                                                        <dx:GridViewDataColumn FieldName="Narration">
                                                        </dx:GridViewDataColumn>
                                                        <dx:GridViewDataColumn FieldName="Credit">
                                                        </dx:GridViewDataColumn>
                                                        <dx:GridViewDataColumn FieldName="Debit">
                                                        </dx:GridViewDataColumn>
                                                    </Columns>
                                                    <Styles  Footer-HorizontalAlign="Left" Cell-HorizontalAlign="Left" Header-VerticalAlign="Middle" Header-HorizontalAlign="Center"
                                                        Header-Wrap="True">
                                                        <GroupPanel Wrap="True" HorizontalAlign="Left">
                                                        </GroupPanel>
                                                        <GroupRow Wrap="True" HorizontalAlign="Left">
                                                        </GroupRow>
                                                    </Styles>
                                                </dx:ASPxGridView>
                                                <dx:ASPxGridViewExporter ID="geBanksExport" runat="server" GridViewID="gridBanks">
                                                    <Styles>
                                                        <Header Wrap="True" HorizontalAlign="Center">
                                                        </Header>
                                                        <Cell HorizontalAlign="Left" Wrap="True">
                                                        </Cell>
                                                        <Footer Wrap="True" HorizontalAlign="Left"></Footer>
                                                    </Styles>
                                                </dx:ASPxGridViewExporter>
                                                <asp:SqlDataSource runat="server" ID="MySqldsBanks" 
                                                    ProviderName="MySql.Data.MySqlClient" />
                                            </div>
                                        </li>
                                        <li id="li_OtherItems" class="top_H"><a href="#" class="top_Ha">04. Other Items.<span
                                            class="st-arrow">Open or Close</span></a>
                                            <div class="st-content">
                                                <div style="background: #fff !important;" class="row display">
                                                    <div style="background: #fff !important;" class="four columns">
                                                        <p class="inner_heading">
                                                            Yesterday Balance On Other Items</p>
                                                        <ul class="overview_list">
                                                            <li>
                                                                <asp:LinkButton runat="server" OnClick="aCrOtherItemsAmountprDay_Click" ID="aCrOtherItemsAmountprDay">
                                                                    <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                                        alt="" />
                                                                    <asp:Label runat="server" ID="lblCrOtherItemsAmountprDay" Text="0.00" class="ov_nb"></asp:Label>
                                                                    <span class="ov_text">Cr Amount</span>
                                                                </asp:LinkButton></li>
                                                            <li>
                                                                <asp:LinkButton runat="server" OnClick="aDrOtherItemsAmountprDay_Click" ID="aDrOtherItemsAmountprDay">
                                                                    <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                                        alt="" />
                                                                    <asp:Label runat="server" ID="lblDrOtherItemsAmountprDay" Text="0.00" class="ov_nb"></asp:Label>
                                                                    <span class="ov_text">Dr Amount</span>
                                                                </asp:LinkButton></li>
                                                            <li>
                                                                <asp:LinkButton runat="server" OnClick="TotalOtherItemspr_Click" ID="aCrbOtherItemsAmountprDay">
                                                                    <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                                        alt="" />
                                                                    <asp:Label runat="server" ID="lblCrOtherItemsBalanceprDay" Text="0.00" class="ov_nb"></asp:Label>
                                                                    <span class="ov_text">Cr Balance</span>
                                                                </asp:LinkButton></li>
                                                            <li>
                                                                <asp:LinkButton runat="server" OnClick="TotalOtherItemspr_Click" ID="aDrbOtherItemsAmountprDay">
                                                                    <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                                        alt="" />
                                                                    <asp:Label runat="server" ID="lblDrOtherItemsBalanceprDay" Text="0.00" class="ov_nb"></asp:Label>
                                                                    <span class="ov_text">Dr Balance </span>
                                                                </asp:LinkButton></li>
                                                        </ul>
                                                    </div>
                                                    <div style="background: #fff !important;" class="four columns">
                                                        <p class="inner_heading">
                                                            Today Balance On Other Items</p>
                                                        <ul class="overview_list">
                                                            <li>
                                                                <asp:LinkButton runat="server" OnClick="aCrOtherItemsAmountcrDay_Click" ID="aCrOtherItemsAmountcrDay">
                                                                    <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                                        alt="" />
                                                                    <asp:Label runat="server" ID="lblCrOtherItemsAmountFinal" Text="0.00" class="ov_nb"></asp:Label>
                                                                    <span class="ov_text">Cr Amount</span>
                                                                </asp:LinkButton></li>
                                                            <li>
                                                                <asp:LinkButton runat="server" OnClick="aDrOtherItemsAmountcrDay_Click" ID="aDrOtherItemsAmountcrDay">
                                                                    <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                                        alt="" />
                                                                    <asp:Label runat="server" ID="lblDrOtherItemsAmountFinal" Text="0.00" class="ov_nb"></asp:Label>
                                                                    <span class="ov_text">Dr Amount</span>
                                                                </asp:LinkButton></li>
                                                            <li>
                                                                <asp:LinkButton runat="server" OnClick="TotalOtherItemscr_Click" ID="aCrbOtherItemsAmountcrDay">
                                                                    <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                                        alt="" />
                                                                    <asp:Label runat="server" ID="lblCrOtherItemsBalanceFinal" Text="0.00" class="ov_nb"></asp:Label>
                                                                    <span class="ov_text">Cr Balance</span>
                                                                </asp:LinkButton></li>
                                                            <li>
                                                                <asp:LinkButton runat="server" OnClick="TotalOtherItemscr_Click" ID="aDrbOtherItemsAmountcrDay">
                                                                    <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                                        alt="" />
                                                                    <asp:Label runat="server" ID="lblDrOtherItemsBalanceFinal" Text="0.00" class="ov_nb"></asp:Label>
                                                                    <span class="ov_text">Dr Balance</span>
                                                                </asp:LinkButton></li>
                                                        </ul>
                                                    </div>
                                                    <div style="background: #fff !important;" class="four columns">
                                                        <p class="inner_heading">
                                                            Today Transaction On Other Items</p>
                                                        <ul class="overview_list">
                                                            <li>
                                                                <asp:LinkButton runat="server" OnClick="aCrOtherItemsAmounttoDay_Click" ID="aCrOtherItemsAmounttoDay">
                                                                    <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                                        alt="" />
                                                                    <asp:Label runat="server" ID="lblCrOtherItemsAmount" Text="0.00" class="ov_nb"></asp:Label>
                                                                    <span class="ov_text">Cr Amount</span>
                                                                </asp:LinkButton></li>
                                                            <li>
                                                                <asp:LinkButton runat="server" OnClick="aDrOtherItemsAmounttoDay_Click" ID="aDrOtherItemsAmounttoDay">
                                                                    <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                                        alt="" />
                                                                    <asp:Label runat="server" ID="lblDrOtherItemsAmount" Text="0.00" class="ov_nb"></asp:Label>
                                                                    <span class="ov_text">Dr Amount</span>
                                                                </asp:LinkButton></li>
                                                            <li>
                                                                <asp:LinkButton runat="server" OnClick="TotalOtherItemsto_Click" ID="aCrbOtherItemsAmounttoDay">
                                                                    <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                                        alt="" />
                                                                    <asp:Label runat="server" ID="lblCrOtherItemsBalance" Text="0.00" class="ov_nb"></asp:Label>
                                                                    <span class="ov_text">Cr Balance</span>
                                                                </asp:LinkButton></li>
                                                            <li>
                                                                <asp:LinkButton runat="server" OnClick="TotalOtherItemsto_Click" ID="aDrbOtherItemsAmounttoDay">
                                                                    <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                                        alt="" />
                                                                    <asp:Label runat="server" ID="lblDrOtherItemsBalance" Text="0.00" class="ov_nb"></asp:Label>
                                                                    <span class="ov_text">Dr Balance</span>
                                                                </asp:LinkButton></li>
                                                        </ul>
                                                    </div>
                                                </div>
                                                <dx:ASPxGridView Style="margin: 0 auto; width: 100%;" 
                                                    OnSummaryDisplayText="ASPxGridView1_SummaryDisplayTextOtherItems" OnCustomSummaryCalculate="ASPxGridView1_OnCustomSummaryCalculateOtherItems"
                                                    ID="gridOtherItems" ClientInstanceName="gridOtherItems" runat="server" DataSourceID="MySqldsOtherItems"
                                                    OnCustomCallback="grid_CustomCallback">
                                                    <Settings ShowTitlePanel="true" VerticalScrollableHeight="300" ShowVerticalScrollBar="true"
                                                        VerticalScrollBarStyle="Standard" ShowHeaderFilterButton="true" ShowGroupFooter="Hidden"
                                                        ShowFooter="true" ShowGroupedColumns="true" ShowGroupPanel="false" ShowFilterBar="Visible"
                                                        ShowFilterRow="true" />
                                                    <SettingsText Title="Other Items" />
                                                    <Templates>
                                                        <GroupRowContent>
                                                            <dx:ASPxLabel ID="GroupLabel" runat="server" Text='<%# GetLabelText(Container)%>'>
                                                            </dx:ASPxLabel>
                                                        </GroupRowContent>
                                                    </Templates>
                                                    <TotalSummary>
                                                        <dx:ASPxSummaryItem FieldName="Credit" SummaryType="Sum" />
                                                        <dx:ASPxSummaryItem FieldName="Debit" SummaryType="Sum" />
                                                        <dx:ASPxSummaryItem FieldName="Narration" SummaryType="Custom" />
                                                    </TotalSummary>
                                                    <SettingsPager Mode="ShowAllRecords">
                                                    </SettingsPager>
                                                    <Columns>
                                                        <dx:GridViewDataColumn FieldName="Date">
                                                        </dx:GridViewDataColumn>
                                                        <dx:GridViewDataColumn FieldName="LedgerHead">
                                                        </dx:GridViewDataColumn>
                                                        <dx:GridViewDataColumn FieldName="Narration">
                                                        </dx:GridViewDataColumn>
                                                        <dx:GridViewDataColumn FieldName="Credit">
                                                        </dx:GridViewDataColumn>
                                                        <dx:GridViewDataColumn FieldName="Debit">
                                                        </dx:GridViewDataColumn>
                                                    </Columns>
                                                    <Styles Footer-HorizontalAlign="Left"  Cell-HorizontalAlign="Left" Header-VerticalAlign="Middle" Header-HorizontalAlign="Center"
                                                        Header-Wrap="True">
                                                        <GroupPanel Wrap="True" HorizontalAlign="Left">
                                                        </GroupPanel>
                                                        <GroupRow Wrap="True" HorizontalAlign="Left">
                                                        </GroupRow>
                                                    </Styles>
                                                </dx:ASPxGridView>
                                                <dx:ASPxGridViewExporter ID="geOtherItemsExport" runat="server" GridViewID="gridOtherItems">
                                                    <Styles>
                                                        <Header Wrap="True" HorizontalAlign="Center">
                                                        </Header>
                                                        <Cell HorizontalAlign="Left" Wrap="True">
                                                        </Cell>
                                                        <Footer Wrap="True" HorizontalAlign="Left"></Footer>
                                                    </Styles>
                                                </dx:ASPxGridViewExporter>
                                                <asp:SqlDataSource runat="server" ID="MySqldsOtherItems" 
                                                    ProviderName="MySql.Data.MySqlClient" />
                                            </div>
                                        </li>
                                        <li id="li_Chits" class="top_H"><a href="#" class="top_Ha">05. Chits.<span class="st-arrow">Open
                                            or Close</span></a>
                                            <div class="st-content">
                                                <div style="background: #fff !important;" class="row display">
                                                    <div style="background: #fff !important;" class="four columns">
                                                        <p class="inner_heading">
                                                            Yesterday Balance On Chits</p>
                                                        <ul class="overview_list">
                                                            <li>
                                                                <asp:LinkButton runat="server" OnClick="aCrChitsAmountprDay_Click" ID="aCrChitsAmountprDay">
                                                                    <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                                        alt="" />
                                                                    <asp:Label runat="server" ID="lblCrChitsAmountprDay" Text="0.00" class="ov_nb"></asp:Label>
                                                                    <span class="ov_text">Cr Amount</span>
                                                                </asp:LinkButton></li>
                                                            <li>
                                                                <asp:LinkButton runat="server" OnClick="aDrChitsAmountprDay_Click" ID="aDrChitsAmountprDay">
                                                                    <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                                        alt="" />
                                                                    <asp:Label runat="server" ID="lblDrChitsAmountprDay" Text="0.00" class="ov_nb"></asp:Label>
                                                                    <span class="ov_text">Dr Amount</span>
                                                                </asp:LinkButton></li>
                                                            <li>
                                                                <asp:LinkButton runat="server" OnClick="TotalChitsprDay_Click" ID="aCrbChitsAmountprDay">
                                                                    <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                                        alt="" />
                                                                    <asp:Label runat="server" ID="lblCrChitsBalanceprDay" Text="0.00" class="ov_nb"></asp:Label>
                                                                    <span class="ov_text">Cr Balance</span>
                                                                </asp:LinkButton></li>
                                                            <li>
                                                                <asp:LinkButton runat="server" OnClick="TotalChitsprDay_Click" ID="aDrbChitsAmountprDay">
                                                                    <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                                        alt="" />
                                                                    <asp:Label runat="server" ID="lblDrChitsBalanceprDay" Text="0.00" class="ov_nb"></asp:Label>
                                                                    <span class="ov_text">Dr Balance </span>
                                                                </asp:LinkButton></li>
                                                        </ul>
                                                    </div>
                                                    <div style="background: #fff !important;" class="four columns">
                                                        <p class="inner_heading">
                                                            Today Balance On Chits</p>
                                                        <ul class="overview_list">
                                                            <li>
                                                                <asp:LinkButton runat="server" OnClick="aCrChitsAmountcrDay_Click" ID="aCrChitsAmountcrDay">
                                                                    <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                                        alt="" />
                                                                    <asp:Label runat="server" ID="lblCrChitsAmountFinal" Text="0.00" class="ov_nb"></asp:Label>
                                                                    <span class="ov_text">Cr Amount</span>
                                                                </asp:LinkButton></li>
                                                            <li>
                                                                <asp:LinkButton runat="server" OnClick="aDrChitsAmountcrDay_Click" ID="aDrChitsAmountcrDay">
                                                                    <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                                        alt="" />
                                                                    <asp:Label runat="server" ID="lblDrChitsAmountFinal" Text="0.00" class="ov_nb"></asp:Label>
                                                                    <span class="ov_text">Dr Amount</span>
                                                                </asp:LinkButton></li>
                                                            <li>
                                                                <asp:LinkButton runat="server" OnClick="TotalChitscr_Click" ID="aCrbChitsAmountcrDay">
                                                                    <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                                        alt="" />
                                                                    <asp:Label runat="server" ID="lblCrChitsBalanceFinal" Text="0.00" class="ov_nb"></asp:Label>
                                                                    <span class="ov_text">Cr Balance</span>
                                                                </asp:LinkButton></li>
                                                            <li>
                                                                <asp:LinkButton runat="server" OnClick="TotalChitscr_Click" ID="aDrbChitsAmountcrDay">
                                                                    <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                                        alt="" />
                                                                    <asp:Label runat="server" ID="lblDrChitsBalanceFinal" Text="0.00" class="ov_nb"></asp:Label>
                                                                    <span class="ov_text">Dr Balance</span>
                                                                </asp:LinkButton></li>
                                                        </ul>
                                                    </div>
                                                    <div style="background: #fff !important;" class="four columns">
                                                        <p class="inner_heading">
                                                            Today Transaction On Chits</p>
                                                        <ul class="overview_list">
                                                            <li>
                                                                <asp:LinkButton runat="server" OnClick="aCrChitsAmounttoDay_Click" ID="aCrChitsAmounttoDay">
                                                                    <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                                        alt="" />
                                                                    <asp:Label runat="server" ID="lblCrChitsAmount" Text="0.00" class="ov_nb"></asp:Label>
                                                                    <span class="ov_text">Cr Amount</span>
                                                                </asp:LinkButton></li>
                                                            <li>
                                                                <asp:LinkButton runat="server" OnClick="aDrChitstoDay_Click" ID="aDrChitsAmounttoDay">
                                                                    <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                                        alt="" />
                                                                    <asp:Label runat="server" ID="lblDrChitsAmount" Text="0.00" class="ov_nb"></asp:Label>
                                                                    <span class="ov_text">Dr Amount</span>
                                                                </asp:LinkButton></li>
                                                            <li>
                                                                <asp:LinkButton runat="server" OnClick="TotalChitsto_Click" ID="aCrbChitsAmounttoDay">
                                                                    <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                                        alt="" />
                                                                    <asp:Label runat="server" ID="lblCrChitsBalance" Text="0.00" class="ov_nb"></asp:Label>
                                                                    <span class="ov_text">Cr Balance</span>
                                                                </asp:LinkButton></li>
                                                            <li>
                                                                <asp:LinkButton runat="server" OnClick="TotalChitsto_Click" ID="aDrbChitsAmounttoDay">
                                                                    <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                                        alt="" />
                                                                    <asp:Label runat="server" ID="lblDrChitsBalance" Text="0.00" class="ov_nb"></asp:Label>
                                                                    <span class="ov_text">Dr Balance</span>
                                                                </asp:LinkButton></li>
                                                        </ul>
                                                    </div>
                                                </div>
                                                <dx:ASPxGridView  Style="margin: 0 auto;
                                                    width: 100%;" OnSummaryDisplayText="ASPxGridView1_SummaryDisplayTextChits" OnCustomSummaryCalculate="ASPxGridView1_OnCustomSummaryCalculateChits"
                                                    ID="gridChits" ClientInstanceName="gridChits" runat="server" DataSourceID="MySqldsChits"
                                                    OnCustomCallback="grid_CustomCallback">
                                                    <Settings ShowTitlePanel="true" ShowVerticalScrollBar="true" VerticalScrollBarStyle="Virtual"
                                                        VerticalScrollableHeight="300" ShowHeaderFilterButton="true" ShowFooter="true"
                                                        ShowGroupFooter="Hidden" ShowGroupPanel="false" ShowGroupedColumns="true" ShowFilterBar="Visible"
                                                        ShowFilterRow="true" />
                                                    <SettingsText Title="Chits" />
                                                    <Templates>
                                                        <GroupRowContent>
                                                            <dx:ASPxLabel ID="GroupLabel" runat="server" Text='<%# GetLabelText(Container)%>'>
                                                            </dx:ASPxLabel>
                                                        </GroupRowContent>
                                                    </Templates>
                                                    <TotalSummary>
                                                        <dx:ASPxSummaryItem FieldName="Credit" SummaryType="Sum" />
                                                        <dx:ASPxSummaryItem FieldName="Debit" SummaryType="Sum" />
                                                        <dx:ASPxSummaryItem SummaryType="Custom" FieldName="Narration" />
                                                    </TotalSummary>
                                                    <SettingsPager Mode="ShowAllRecords">
                                                    </SettingsPager>
                                                    <Columns>
                                                        <dx:GridViewDataColumn FieldName="Date">
                                                        </dx:GridViewDataColumn>
                                                        <dx:GridViewDataColumn FieldName="Token">
                                                        </dx:GridViewDataColumn>
                                                        <dx:GridViewDataColumn FieldName="LedgerHead">
                                                        </dx:GridViewDataColumn>
                                                        <dx:GridViewDataColumn FieldName="Narration">
                                                        </dx:GridViewDataColumn>
                                                        <dx:GridViewDataColumn FieldName="Credit">
                                                        </dx:GridViewDataColumn>
                                                        <dx:GridViewDataColumn FieldName="Debit">
                                                        </dx:GridViewDataColumn>
                                                    </Columns>
                                                    <Styles  Footer-HorizontalAlign="Left" Cell-HorizontalAlign="Left" Header-VerticalAlign="Middle" Header-HorizontalAlign="Center"
                                                        Header-Wrap="True">
                                                        <GroupPanel Wrap="True" HorizontalAlign="Left">
                                                        </GroupPanel>
                                                        <GroupRow Wrap="True" HorizontalAlign="Left">
                                                        </GroupRow>
                                                    </Styles>
                                                </dx:ASPxGridView>
                                                <dx:ASPxGridViewExporter ID="geChitsExport" runat="server" GridViewID="gridChits">
                                                    <Styles>
                                                        <Header Wrap="True" HorizontalAlign="Center">
                                                        </Header>
                                                        <Cell HorizontalAlign="Left" Wrap="True">
                                                        </Cell>
                                                        <Footer Wrap="True" HorizontalAlign="Left"></Footer>
                                                    </Styles>
                                                </dx:ASPxGridViewExporter>
                                                <asp:SqlDataSource runat="server" ID="MySqldsChits" 
                                                    ProviderName="MySql.Data.MySqlClient" />
                                            </div>
                                        </li>
                                        <li id="li_CompanyChits" class="top_H"><a href="#" class="top_Ha">06. Company Chits<span
                                            class="st-arrow">Open or Close</span></a>
                                            <div class="st-content">
                                                <div style="background: #fff !important;" class="row display">
                                                    <div style="background: #fff !important;" class="four columns">
                                                        <p class="inner_heading">
                                                            Yesterday Balance On Foreman Chits</p>
                                                        <ul class="overview_list">
                                                            <li>
                                                                <asp:LinkButton runat="server" OnClick="aCrForemanChitsAmountprDay_Click" ID="aCrForemanChitsAmountprDay">
                                                                    <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                                        alt="" />
                                                                    <asp:Label runat="server" ID="lblCrForemanChitsAmountprDay" Text="0.00" class="ov_nb"></asp:Label>
                                                                    <span class="ov_text">Cr Amount</span>
                                                                </asp:LinkButton></li>
                                                            <li>
                                                                <asp:LinkButton runat="server" OnClick="aDrForemanChitsAmountprDay_Click" ID="aDrForemanChitsAmountprDay">
                                                                    <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                                        alt="" />
                                                                    <asp:Label runat="server" ID="lblDrForemanChitsAmountprDay" Text="0.00" class="ov_nb"></asp:Label>
                                                                    <span class="ov_text">Dr Amount</span>
                                                                </asp:LinkButton></li>
                                                            <li>
                                                                <asp:LinkButton runat="server" OnClick="TotalForemanChitspr_Click" ID="aCrbForemanChitsAmountprDay">
                                                                    <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                                        alt="" />
                                                                    <asp:Label runat="server" ID="lblCrForemanChitsBalanceprDay" Text="0.00" class="ov_nb"></asp:Label>
                                                                    <span class="ov_text">Cr Balance</span>
                                                                </asp:LinkButton></li>
                                                            <li>
                                                                <asp:LinkButton runat="server" OnClick="TotalForemanChitspr_Click" ID="aDrbForemanChitsAmountprDay">
                                                                    <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                                        alt="" />
                                                                    <asp:Label runat="server" ID="lblDrForemanChitsBalanceprDay" Text="0.00" class="ov_nb"></asp:Label>
                                                                    <span class="ov_text">Dr Balance </span>
                                                                </asp:LinkButton>
                                                            </li>
                                                        </ul>
                                                    </div>
                                                    <div style="background: #fff !important;" class="four columns">
                                                        <p class="inner_heading">
                                                            Today Balance On Foreman Chits</p>
                                                        <ul class="overview_list">
                                                            <li>
                                                                <asp:LinkButton runat="server" OnClick="aCrForemanChitsAmountcrDay_Click" ID="aCrForemanChitsAmountcrDay">
                                                                    <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                                        alt="" />
                                                                    <asp:Label runat="server" ID="lblCrForemanChitsAmountFinal" Text="0.00" class="ov_nb"></asp:Label>
                                                                    <span class="ov_text">Cr Amount</span>
                                                                </asp:LinkButton></li>
                                                            <li>
                                                                <asp:LinkButton runat="server" OnClick="aDrForemanChitsAmountcrDay_Click" ID="aDrForemanChitsAmountcrDay">
                                                                    <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                                        alt="" />
                                                                    <asp:Label runat="server" ID="lblDrForemanChitsAmountFinal" Text="0.00" class="ov_nb"></asp:Label>
                                                                    <span class="ov_text">Dr Amount</span>
                                                                </asp:LinkButton></li>
                                                            <li>
                                                                <asp:LinkButton runat="server" OnClick="TotalForemanChitscr_Click" ID="aCrbFormanChitsAmountcrDay">
                                                                    <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                                        alt="" />
                                                                    <asp:Label runat="server" ID="lblCrForemanChitsBalanceFinal" Text="0.00" class="ov_nb"></asp:Label>
                                                                    <span class="ov_text">Cr Balance</span>
                                                                </asp:LinkButton></li>
                                                            <li>
                                                                <asp:LinkButton runat="server" OnClick="TotalForemanChitscr_Click" ID="aDrbForemanChitsAmountcrDay">
                                                                    <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                                        alt="" />
                                                                    <asp:Label runat="server" ID="lblDrForemanChitsBalanceFinal" Text="0.00" class="ov_nb"></asp:Label>
                                                                    <span class="ov_text">Dr Balance</span>
                                                                </asp:LinkButton></li>
                                                        </ul>
                                                    </div>
                                                    <div style="background: #fff !important;" class="four columns">
                                                        <p class="inner_heading">
                                                            Today Transaction On Foreman Chits</p>
                                                        <ul class="overview_list">
                                                            <li>
                                                                <asp:LinkButton runat="server" OnClick="aCrForemanChitsAmounttoDay_Click" ID="aCrForemanChitsAmounttoDay">
                                                                    <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                                        alt="" />
                                                                    <asp:Label runat="server" ID="lblCrForemanChitsAmount" Text="0.00" class="ov_nb"></asp:Label>
                                                                    <span class="ov_text">Cr Amount</span>
                                                                </asp:LinkButton></li>
                                                            <li>
                                                                <asp:LinkButton runat="server" OnClick="aDrForemanChitsAmounttoDay_Click" ID="aDrForemanChitsAmounttoDay">
                                                                    <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                                        alt="" />
                                                                    <asp:Label runat="server" ID="lblDrForemanChitsAmount" Text="0.00" class="ov_nb"></asp:Label>
                                                                    <span class="ov_text">Dr Amount</span>
                                                                </asp:LinkButton></li>
                                                            <li>
                                                                <asp:LinkButton runat="server" OnClick="TotalForemanChitsto_Click" ID="aCrbForemanChitsAmounttoDay">
                                                                    <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                                        alt="" />
                                                                    <asp:Label runat="server" ID="lblCrForemanChitsBalance" Text="0.00" class="ov_nb"></asp:Label>
                                                                    <span class="ov_text">Cr Balance</span>
                                                                </asp:LinkButton></li>
                                                            <li>
                                                                <asp:LinkButton runat="server" OnClick="TotalForemanChitsto_Click" ID="aDrbForemanChitsAmounttoDay">
                                                                    <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                                        alt="" />
                                                                    <asp:Label runat="server" ID="lblDrForemanChitsBalance" Text="0.00" class="ov_nb"></asp:Label>
                                                                    <span class="ov_text">Dr Balance</span>
                                                                </asp:LinkButton></li>
                                                        </ul>
                                                    </div>
                                                </div>
                                                <dx:ASPxGridView  Style="margin: 0 auto;
                                                    width: 100%;" OnSummaryDisplayText="ASPxGridView1_SummaryDisplayTextForemanChits"
                                                    OnCustomSummaryCalculate="ASPxGridView1_OnCustomSummaryCalculateForemanChits"
                                                    ID="gridForemanChits" ClientInstanceName="gridForemanChits" runat="server" DataSourceID="MySqldsForemanChits"
                                                    OnCustomCallback="grid_CustomCallback">
                                                    <Settings ShowTitlePanel="true" ShowVerticalScrollBar="true" VerticalScrollBarStyle="Virtual"
                                                        VerticalScrollableHeight="300" ShowHeaderFilterButton="true" ShowFooter="true"
                                                        ShowGroupFooter="Hidden" ShowGroupPanel="false" ShowGroupedColumns="true" ShowFilterBar="Visible"
                                                        ShowFilterRow="true" />
                                                    <SettingsText Title="Foreman Chits" />
                                                    <TotalSummary>
                                                        <dx:ASPxSummaryItem FieldName="Credit" SummaryType="Sum" />
                                                        <dx:ASPxSummaryItem FieldName="Debit" SummaryType="Sum" />
                                                        <dx:ASPxSummaryItem FieldName="Narration" SummaryType="Custom" />
                                                    </TotalSummary>
                                                    <SettingsPager Mode="ShowAllRecords">
                                                    </SettingsPager>
                                                    <Templates>
                                                        <GroupRowContent>
                                                            <dx:ASPxLabel ID="GroupLabel" runat="server" Text='<%# GetLabelText(Container)%>'>
                                                            </dx:ASPxLabel>
                                                        </GroupRowContent>
                                                    </Templates>
                                                    <Columns>
                                                        <dx:GridViewDataColumn FieldName="Date">
                                                        </dx:GridViewDataColumn>
                                                        <dx:GridViewDataColumn FieldName="Token">
                                                        </dx:GridViewDataColumn>
                                                        <dx:GridViewDataColumn FieldName="Narration">
                                                        </dx:GridViewDataColumn>
                                                        <dx:GridViewDataColumn FieldName="Credit">
                                                        </dx:GridViewDataColumn>
                                                        <dx:GridViewDataColumn FieldName="Debit">
                                                        </dx:GridViewDataColumn>
                                                    </Columns>
                                                    <Styles  Footer-HorizontalAlign="Left" Cell-HorizontalAlign="Left" Header-VerticalAlign="Middle" Header-HorizontalAlign="Center"
                                                        Header-Wrap="True">
                                                        <GroupPanel Wrap="True" HorizontalAlign="Left">
                                                        </GroupPanel>
                                                        <GroupRow Wrap="True" HorizontalAlign="Left">
                                                        </GroupRow>
                                                    </Styles>
                                                </dx:ASPxGridView>
                                                <dx:ASPxGridViewExporter ID="geForemanChitsExport" runat="server" GridViewID="gridForemanChits">
                                                    <Styles>
                                                        <Header Wrap="True" HorizontalAlign="Center">
                                                        </Header>
                                                        <Cell HorizontalAlign="Left" Wrap="True">
                                                        </Cell>
                                                        <Footer Wrap="True" HorizontalAlign="Left"></Footer>
                                                    </Styles>
                                                </dx:ASPxGridViewExporter>
                                                <asp:SqlDataSource runat="server" ID="MySqldsForemanChits" 
                                                    ProviderName="MySql.Data.MySqlClient" />
                                            </div>
                                        </li>
                                        <li id="li_DecreeDebtors" class="top_H"><a href="#" class="top_Ha">07. Decree Debtors.<span
                                            class="st-arrow">Open or Close</span></a>
                                            <div class="st-content">
                                                <div style="background: #fff !important;" class="row display">
                                                    <div style="background: #fff !important;" class="four columns">
                                                        <p class="inner_heading">
                                                            Yesterday Balance On Decree Debtors</p>
                                                        <ul class="overview_list">
                                                            <li>
                                                                <asp:LinkButton runat="server" OnClick="aCrDecreeDebtorsAmountprDay_Click" ID="aCrDecreeDebtorsAmountprDay">
                                                                    <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                                        alt="" />
                                                                    <asp:Label runat="server" ID="lblCrDecreeDebtorsAmountprDay" Text="0.00" class="ov_nb"></asp:Label>
                                                                    <span class="ov_text">Cr Amount</span>
                                                                </asp:LinkButton></li>
                                                            <li>
                                                                <asp:LinkButton runat="server" OnClick="aDrDecreeDebtorsAmountprDay_Click" ID="aDrDecreeDebtorsAmountprDay">
                                                                    <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                                        alt="" />
                                                                    <asp:Label runat="server" ID="lblDrDecreeDebtorsAmountprDay" Text="0.00" class="ov_nb"></asp:Label>
                                                                    <span class="ov_text">Dr Amount</span>
                                                                </asp:LinkButton></li>
                                                            <li>
                                                                <asp:LinkButton runat="server" OnClick="TotalDecreeDebtorspr_Click" ID="aCrbDecreeDebtorsAmountprDay">
                                                                    <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                                        alt="" />
                                                                    <asp:Label runat="server" ID="lblCrDecreeDebtorsBalanceprDay" Text="0.00" class="ov_nb"></asp:Label>
                                                                    <span class="ov_text">Cr Balance</span>
                                                                </asp:LinkButton></li>
                                                            <li>
                                                                <asp:LinkButton runat="server" OnClick="TotalDecreeDebtorspr_Click" ID="aDrbDecreeDebtorsAmountprDay">
                                                                    <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                                        alt="" />
                                                                    <asp:Label runat="server" ID="lblDrDecreeDebtorsBalanceprDay" Text="0.00" class="ov_nb"></asp:Label>
                                                                    <span class="ov_text">Dr Balance </span>
                                                                </asp:LinkButton></li>
                                                        </ul>
                                                    </div>
                                                    <div style="background: #fff !important;" class="four columns">
                                                        <p class="inner_heading">
                                                            Today Balance On Decree Debtors</p>
                                                        <ul class="overview_list">
                                                            <li>
                                                                <asp:LinkButton runat="server" OnClick="aCrDecreeDebtorsAmountcrDay_Click" ID="aCrDecreeDebtorsAmountcrDay">
                                                                    <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                                        alt="" />
                                                                    <asp:Label runat="server" ID="lblCrDecreeDebtorsAmountFinal" Text="0.00" class="ov_nb"></asp:Label>
                                                                    <span class="ov_text">Cr Amount</span>
                                                                </asp:LinkButton></li>
                                                            <li>
                                                                <asp:LinkButton runat="server" OnClick="aDrDecreeDebtorsAmountcrDay_Click" ID="aDrDecreeDebtorsAmountcrDay">
                                                                    <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                                        alt="" />
                                                                    <asp:Label runat="server" ID="lblDrDecreeDebtorsAmountFinal" Text="0.00" class="ov_nb"></asp:Label>
                                                                    <span class="ov_text">Dr Amount</span>
                                                                </asp:LinkButton></li>
                                                            <li>
                                                                <asp:LinkButton runat="server" OnClick="TotalDecreeDebtorscr_Click" ID="aCrbDecreeDebtorsAmountcrDay">
                                                                    <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                                        alt="" />
                                                                    <asp:Label runat="server" ID="lblCrDecreeDebtorsBalanceFinal" Text="0.00" class="ov_nb"></asp:Label>
                                                                    <span class="ov_text">Cr Balance</span>
                                                                </asp:LinkButton></li>
                                                            <li>
                                                                <asp:LinkButton runat="server" OnClick="TotalDecreeDebtorscr_Click" ID="aDrbDecreeDebtorsAmountcrDay">
                                                                    <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                                        alt="" />
                                                                    <asp:Label runat="server" ID="lblDrDecreeDebtorsBalanceFinal" Text="0.00" class="ov_nb"></asp:Label>
                                                                    <span class="ov_text">Dr Balance</span>
                                                                </asp:LinkButton></li>
                                                        </ul>
                                                    </div>
                                                    <div style="background: #fff !important;" class="four columns">
                                                        <p class="inner_heading">
                                                            Today Transaction On Decree Debtors</p>
                                                        <ul class="overview_list">
                                                            <li>
                                                                <asp:LinkButton runat="server" OnClick="aCrDecreeDebtorsAmounttoDay_Click" ID="aCrDecreeDebtorsAmounttoDay">
                                                                    <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                                        alt="" />
                                                                    <asp:Label runat="server" ID="lblCrDecreeDebtorsAmount" Text="0.00" class="ov_nb"></asp:Label>
                                                                    <span class="ov_text">Cr Amount</span>
                                                                </asp:LinkButton></li>
                                                            <li>
                                                                <asp:LinkButton runat="server" OnClick="aDrDecreeDebtorsAmounttoDay_Click" ID="aDrDecreeDebtorsAmounttoDay">
                                                                    <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                                        alt="" />
                                                                    <asp:Label runat="server" ID="lblDrDecreeDebtorsAmount" Text="0.00" class="ov_nb"></asp:Label>
                                                                    <span class="ov_text">Dr Amount</span>
                                                                </asp:LinkButton></li>
                                                            <li>
                                                                <asp:LinkButton runat="server" OnClick="TotalDecreeDebtorsto_Click" ID="aCrbDecreeDebtorsAmounttoDay">
                                                                    <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                                        alt="" />
                                                                    <asp:Label runat="server" ID="lblCrDecreeDebtorsBalance" Text="0.00" class="ov_nb"></asp:Label>
                                                                    <span class="ov_text">Cr Balance</span>
                                                                </asp:LinkButton></li>
                                                            <li>
                                                                <asp:LinkButton runat="server" OnClick="TotalDecreeDebtorsto_Click" ID="aDrbDecreeDebtorsAmounttoDay">
                                                                    <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                                        alt="" />
                                                                    <asp:Label runat="server" ID="lblDrDecreeDebtorsBalance" Text="0.00" class="ov_nb"></asp:Label>
                                                                    <span class="ov_text">Dr Balance</span>
                                                                </asp:LinkButton></li>
                                                        </ul>
                                                    </div>
                                                </div>
                                                <dx:ASPxGridView  Style="margin: 0 auto;
                                                    width: 100%;" OnSummaryDisplayText="ASPxGridView1_SummaryDisplayTextDecreeDebtors"
                                                    OnCustomSummaryCalculate="ASPxGridView1_OnCustomSummaryCalculateDecreeDebtors"
                                                    ID="gridDecreeDebtors" ClientInstanceName="gridDecreeDebtors" runat="server"
                                                    DataSourceID="MySqldsDecreeDebtors" OnCustomCallback="grid_CustomCallback">
                                                    <Settings ShowTitlePanel="true" ShowVerticalScrollBar="true" VerticalScrollBarStyle="Virtual"
                                                        VerticalScrollableHeight="300" ShowHeaderFilterButton="true" ShowFooter="true"
                                                        ShowGroupFooter="Hidden" ShowGroupPanel="false" ShowGroupedColumns="true" ShowFilterBar="Visible"
                                                        ShowFilterRow="true" />
                                                    <SettingsText Title="Decree Debtors" />
                                                    <Templates>
                                                        <GroupRowContent>
                                                            <dx:ASPxLabel ID="GroupLabel" runat="server" Text='<%# GetLabelText(Container)%>'>
                                                            </dx:ASPxLabel>
                                                        </GroupRowContent>
                                                    </Templates>
                                                    <TotalSummary>
                                                        <dx:ASPxSummaryItem FieldName="Credit" SummaryType="Sum" />
                                                        <dx:ASPxSummaryItem FieldName="Debit" SummaryType="Sum" />
                                                        <dx:ASPxSummaryItem FieldName="Narration" SummaryType="Custom" />
                                                    </TotalSummary>
                                                    <SettingsPager Mode="ShowAllRecords">
                                                    </SettingsPager>
                                                    <Columns>
                                                        <dx:GridViewDataColumn FieldName="Date">
                                                        </dx:GridViewDataColumn>
                                                        <dx:GridViewDataColumn FieldName="Head">
                                                        </dx:GridViewDataColumn>
                                                        <dx:GridViewDataColumn FieldName="Narration">
                                                        </dx:GridViewDataColumn>
                                                        <dx:GridViewDataColumn FieldName="Credit">
                                                        </dx:GridViewDataColumn>
                                                        <dx:GridViewDataColumn FieldName="Debit">
                                                        </dx:GridViewDataColumn>
                                                    </Columns>
                                                    <Styles  Footer-HorizontalAlign="Left" Cell-HorizontalAlign="Left" Header-VerticalAlign="Middle" Header-HorizontalAlign="Center"
                                                        Header-Wrap="True">
                                                        <GroupPanel Wrap="True" HorizontalAlign="Left">
                                                        </GroupPanel>
                                                        <GroupRow Wrap="True" HorizontalAlign="Left">
                                                        </GroupRow>
                                                    </Styles>
                                                </dx:ASPxGridView>
                                                <dx:ASPxGridViewExporter ID="geDecreeDebtorsExport" runat="server" GridViewID="gridDecreeDebtors">
                                                    <Styles>
                                                        <Header Wrap="True" HorizontalAlign="Center">
                                                        </Header>
                                                        <Cell HorizontalAlign="Left" Wrap="True">
                                                        </Cell>
                                                        <Footer Wrap="True" HorizontalAlign="Left"></Footer>
                                                    </Styles>
                                                </dx:ASPxGridViewExporter>
                                                <asp:SqlDataSource runat="server" ID="MySqldsDecreeDebtors" 
                                                    ProviderName="MySql.Data.MySqlClient" />
                                            </div>
                                        </li>
                                        <li id="li_Loans" class="top_H"><a href="#" class="top_Ha">08. Loans<span class="st-arrow">Open
                                            or Close</span></a>
                                            <div class="st-content">
                                                <div style="background: #fff !important;" class="row display">
                                                    <div style="background: #fff !important;" class="four columns">
                                                        <p class="inner_heading">
                                                            Yesterday Balance On Loans</p>
                                                        <ul class="overview_list">
                                                            <li>
                                                                <asp:LinkButton runat="server" OnClick="aCrLoansAmountprDay_Click" ID="aCrLoansAmountprDay">
                                                                    <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                                        alt="" />
                                                                    <asp:Label runat="server" ID="lblCrLoansAmountprDay" Text="0.00" class="ov_nb"></asp:Label>
                                                                    <span class="ov_text">Cr Amount</span>
                                                                </asp:LinkButton></li>
                                                            <li>
                                                                <asp:LinkButton runat="server" OnClick="aDrLoansAmountprDay_Click" ID="aDrLoansAmountprDay">
                                                                    <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                                        alt="" />
                                                                    <asp:Label runat="server" ID="lblDrLoansAmountprDay" Text="0.00" class="ov_nb"></asp:Label>
                                                                    <span class="ov_text">Dr Amount</span>
                                                                </asp:LinkButton></li>
                                                            <li>
                                                                <asp:LinkButton runat="server" OnClick="TotalLoanspr_Click" ID="aCrbLoansAmountprDay">
                                                                    <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                                        alt="" />
                                                                    <asp:Label runat="server" ID="lblCrLoansBalanceprDay" Text="0.00" class="ov_nb"></asp:Label>
                                                                    <span class="ov_text">Cr Balance</span>
                                                                </asp:LinkButton></li>
                                                            <li>
                                                                <asp:LinkButton runat="server" OnClick="TotalLoanspr_Click" ID="aDrbLoansAmountprDay">
                                                                    <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                                        alt="" />
                                                                    <asp:Label runat="server" ID="lblDrLoansBalanceprDay" Text="0.00" class="ov_nb"></asp:Label>
                                                                    <span class="ov_text">Dr Balance</span></asp:LinkButton></li>
                                                        </ul>
                                                    </div>
                                                    <div style="background: #fff !important;" class="four columns">
                                                        <p class="inner_heading">
                                                            Today Balance On Loans</p>
                                                        <ul class="overview_list">
                                                            <li>
                                                                <asp:LinkButton runat="server" OnClick="aCrLoansAmountcrDay_Click" ID="aCrLoansAmountcrDay">
                                                                    <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                                        alt="" />
                                                                    <asp:Label runat="server" ID="lblCrLoansAmountFinal" Text="0.00" class="ov_nb"></asp:Label>
                                                                    <span class="ov_text">Cr Amount</span>
                                                                </asp:LinkButton></li>
                                                            <li>
                                                                <asp:LinkButton runat="server" OnClick="aDrLoansAmountcrDay_Click" ID="aDrLoansAmountcrDay">
                                                                    <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                                        alt="" />
                                                                    <asp:Label runat="server" ID="lblDrLoansAmountFinal" Text="0.00" class="ov_nb"></asp:Label>
                                                                    <span class="ov_text">Dr Amount</span>
                                                                </asp:LinkButton></li>
                                                            <li>
                                                                <asp:LinkButton runat="server" OnClick="TotalLoanscr_Click" ID="aCrbLoansAmountcrDay">
                                                                    <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                                        alt="" />
                                                                    <asp:Label runat="server" ID="lblCrLoansBalanceFinal" Text="0.00" class="ov_nb"></asp:Label>
                                                                    <span class="ov_text">Cr Balance</span>
                                                                </asp:LinkButton></li>
                                                            <li>
                                                                <asp:LinkButton runat="server" OnClick="TotalLoanscr_Click" ID="aDrbLoansAmountcrDay">
                                                                    <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                                        alt="" />
                                                                    <asp:Label runat="server" ID="lblDrLoansBalanceFinal" Text="0.00" class="ov_nb"></asp:Label>
                                                                    <span class="ov_text">Dr Balance</span>
                                                                </asp:LinkButton></li>
                                                        </ul>
                                                    </div>
                                                    <div style="background: #fff !important;" class="four columns">
                                                        <p class="inner_heading">
                                                            Today Transaction On Loans</p>
                                                        <ul class="overview_list">
                                                            <li>
                                                                <asp:LinkButton runat="server" OnClick="aCrLoansAmounttoDay_Click" ID="aCrLoansAmounttoDay">
                                                                    <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                                        alt="" />
                                                                    <asp:Label runat="server" ID="lblCrLoansAmount" Text="0.00" class="ov_nb"></asp:Label>
                                                                    <span class="ov_text">Cr Amount</span>
                                                                </asp:LinkButton></li>
                                                            <li>
                                                                <asp:LinkButton runat="server" OnClick="aDrLoansAmounttoDay_Click" ID="aDrLoansAmounttoDay">
                                                                    <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                                        alt="" />
                                                                    <asp:Label runat="server" ID="lblDrLoansAmount" Text="0.00" class="ov_nb"></asp:Label>
                                                                    <span class="ov_text">Dr Amount</span>
                                                                </asp:LinkButton></li>
                                                            <li>
                                                                <asp:LinkButton runat="server" OnClick="TotalLoansto_Click" ID="aCrbLoansAmounttoDay">
                                                                    <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                                        alt="" />
                                                                    <asp:Label runat="server" ID="lblCrLoansBalance" Text="0.00" class="ov_nb"></asp:Label>
                                                                    <span class="ov_text">Cr Balance</span>
                                                                </asp:LinkButton></li>
                                                            <li>
                                                                <asp:LinkButton runat="server" OnClick="TotalLoansto_Click" ID="aDrbLoansAmounttoDay">
                                                                    <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                                        alt="" />
                                                                    <asp:Label runat="server" ID="lblDrLoansBalance" Text="0.00" class="ov_nb"></asp:Label>
                                                                    <span class="ov_text">Dr Balance</span>
                                                                </asp:LinkButton></li>
                                                        </ul>
                                                    </div>
                                                </div>
                                                <dx:ASPxGridView  Style="margin: 0 auto;
                                                    width: 100%;" OnSummaryDisplayText="ASPxGridView1_SummaryDisplayTextLoans" OnCustomSummaryCalculate="ASPxGridView1_OnCustomSummaryCalculateLoans"
                                                    ID="gridLoans" ClientInstanceName="gridLoans" runat="server" DataSourceID="MySqldsLoans"
                                                    OnCustomCallback="grid_CustomCallback">
                                                    <Settings ShowTitlePanel="true" ShowVerticalScrollBar="true" VerticalScrollBarStyle="Virtual"
                                                        VerticalScrollableHeight="300" ShowHeaderFilterButton="true" ShowFooter="true"
                                                        ShowGroupFooter="Hidden" ShowGroupPanel="false" ShowGroupedColumns="true" ShowFilterBar="Visible"
                                                        ShowFilterRow="true" />
                                                    <SettingsText Title="Loans" />
                                                    <Templates>
                                                        <GroupRowContent>
                                                            <dx:ASPxLabel ID="GroupLabel" runat="server" Text='<%# GetLabelText(Container)%>'>
                                                            </dx:ASPxLabel>
                                                        </GroupRowContent>
                                                    </Templates>
                                                    <TotalSummary>
                                                        <dx:ASPxSummaryItem FieldName="Credit" SummaryType="Sum" />
                                                        <dx:ASPxSummaryItem FieldName="Debit" SummaryType="Sum" />
                                                        <dx:ASPxSummaryItem FieldName="Narration" SummaryType="Custom" />
                                                    </TotalSummary>
                                                    <SettingsPager Mode="ShowAllRecords">
                                                    </SettingsPager>
                                                    <Columns>
                                                        <dx:GridViewDataColumn FieldName="Date">
                                                        </dx:GridViewDataColumn>
                                                        <dx:GridViewDataColumn FieldName="LedgerHead">
                                                        </dx:GridViewDataColumn>
                                                        <dx:GridViewDataColumn FieldName="Narration">
                                                        </dx:GridViewDataColumn>
                                                        <dx:GridViewDataColumn FieldName="Credit">
                                                        </dx:GridViewDataColumn>
                                                        <dx:GridViewDataColumn FieldName="Debit">
                                                        </dx:GridViewDataColumn>
                                                    </Columns>
                                                    <Styles  Footer-HorizontalAlign="Left" Cell-HorizontalAlign="Left" Header-VerticalAlign="Middle" Header-HorizontalAlign="Center"
                                                        Header-Wrap="True">
                                                        <GroupPanel Wrap="True" HorizontalAlign="Left">
                                                        </GroupPanel>
                                                        <GroupRow Wrap="True" HorizontalAlign="Left">
                                                        </GroupRow>
                                                    </Styles>
                                                </dx:ASPxGridView>
                                                <dx:ASPxGridViewExporter ID="geLoansExport" runat="server" GridViewID="gridLoans">
                                                    <Styles>
                                                        <Header Wrap="True" HorizontalAlign="Center">
                                                        </Header>
                                                        <Cell HorizontalAlign="Left" Wrap="True">
                                                        </Cell>
                                                        <Footer Wrap="True" HorizontalAlign="Left"></Footer>
                                                    </Styles>
                                                </dx:ASPxGridViewExporter>
                                                <asp:SqlDataSource runat="server" ID="MySqldsLoans" 
                                                    ProviderName="MySql.Data.MySqlClient" />
                                            </div>
                                        </li>
                                        <li id="li_Advances" class="top_H"><a href="#" class="top_Ha">09. Advances<span class="st-arrow">Open
                                            or Close</span></a>
                                            <div class="st-content">
                                                <div style="background: #fff !important;" class="row display">
                                                    <div style="background: #fff !important;" class="four columns">
                                                        <p class="inner_heading">
                                                            Yesterday Balance On Advances</p>
                                                        <ul class="overview_list">
                                                            <li>
                                                                <asp:LinkButton runat="server" OnClick="aCrAdvancesAmountprDay_Click" ID="aCrAdvancesAmountprDay">
                                                                    <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                                        alt="" />
                                                                    <asp:Label runat="server" ID="lblCrSundriesAndAdvancesAmountprDay" Text="0.00" class="ov_nb"></asp:Label>
                                                                    <span class="ov_text">Cr Amount</span>
                                                                </asp:LinkButton></li>
                                                            <li>
                                                                <asp:LinkButton runat="server" OnClick="aDrAdvancesAmountprDay_Click" ID="aDrAdvancesAmountprDay">
                                                                    <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                                        alt="" />
                                                                    <asp:Label runat="server" ID="lblDrSundriesAndAdvancesAmountprDay" Text="0.00" class="ov_nb"></asp:Label>
                                                                    <span class="ov_text">Dr Amount</span>
                                                                </asp:LinkButton></li>
                                                            <li>
                                                                <asp:LinkButton runat="server" OnClick="TotalAdvancespr_Click" ID="aCrbAdvancesAmountprDay">
                                                                    <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                                        alt="" />
                                                                    <asp:Label runat="server" ID="lblCrSundriesAndAdvancesBalanceprDay" Text="0.00" class="ov_nb"></asp:Label>
                                                                    <span class="ov_text">Cr Balance</span>
                                                                </asp:LinkButton></li>
                                                            <li>
                                                                <asp:LinkButton runat="server" OnClick="TotalAdvancespr_Click" ID="aDrbAdvancesAmountprDay">
                                                                    <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                                        alt="" />
                                                                    <asp:Label runat="server" ID="lblDrSundriesAndAdvancesBalanceprDay" Text="0.00" class="ov_nb"></asp:Label>
                                                                    <span class="ov_text">Dr Balance </span>
                                                                </asp:LinkButton></li>
                                                        </ul>
                                                    </div>
                                                    <div style="background: #fff !important;" class="four columns">
                                                        <p class="inner_heading">
                                                            Today Balance On Advances</p>
                                                        <ul class="overview_list">
                                                            <li>
                                                                <asp:LinkButton runat="server" OnClick="aCrAdvancesAmountcrDay_Click" ID="aCrAdvancesAmountcrDay">
                                                                    <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                                        alt="" />
                                                                    <asp:Label runat="server" ID="lblCrSundriesAndAdvancesAmountFinal" Text="0.00" class="ov_nb"></asp:Label>
                                                                    <span class="ov_text">Cr Amount</span>
                                                                </asp:LinkButton></li>
                                                            <li>
                                                                <asp:LinkButton runat="server" OnClick="aDrAdvancesAmountcrDay_Click" ID="aDrAdvancesAmountcrDay">
                                                                    <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                                        alt="" />
                                                                    <asp:Label runat="server" ID="lblDrSundriesAndAdvancesAmountFinal" Text="0.00" class="ov_nb"></asp:Label>
                                                                    <span class="ov_text">Dr Amount</span>
                                                                </asp:LinkButton></li>
                                                            <li>
                                                                <asp:LinkButton runat="server" OnClick="TotalAdvancescr_Click" ID="aCrbAdvancesAmountcrDay">
                                                                    <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                                        alt="" />
                                                                    <asp:Label runat="server" ID="lblCrSundriesAndAdvancesBalanceFinal" Text="0.00" class="ov_nb"></asp:Label>
                                                                    <span class="ov_text">Cr Balance</span>
                                                                </asp:LinkButton></li>
                                                            <li>
                                                                <asp:LinkButton runat="server" OnClick="TotalAdvancescr_Click" ID="aDrbAdvancesAmountcrDay">
                                                                    <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                                        alt="" />
                                                                    <asp:Label runat="server" ID="lblDrSundriesAndAdvancesBalanceFinal" Text="0.00" class="ov_nb"></asp:Label>
                                                                    <span class="ov_text">Dr Balance</span>
                                                                </asp:LinkButton></li>
                                                        </ul>
                                                    </div>
                                                    <div style="background: #fff !important;" class="four columns">
                                                        <p class="inner_heading">
                                                            Today Transaction On Advances</p>
                                                        <ul class="overview_list">
                                                            <li>
                                                                <asp:LinkButton runat="server" OnClick="aCrAdvancesAmounttoDay_Click" ID="aCrAdvancesAmounttoDay">
                                                                    <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                                        alt="" />
                                                                    <asp:Label runat="server" ID="lblCrSundriesAndAdvancesAmount" Text="0.00" class="ov_nb"></asp:Label>
                                                                    <span class="ov_text">Cr Amount</span>
                                                                </asp:LinkButton></li>
                                                            <li>
                                                                <asp:LinkButton runat="server" OnClick="aDrAdvancesAmounttoDay_Click" ID="aDrAdvancesAmounttoDay">
                                                                    <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                                        alt="" />
                                                                    <asp:Label runat="server" ID="lblDrSundriesAndAdvancesAmount" Text="0.00" class="ov_nb"></asp:Label>
                                                                    <span class="ov_text">Dr Amount</span>
                                                                </asp:LinkButton></li>
                                                            <li>
                                                                <asp:LinkButton runat="server" OnClick="TotalAdvancesto_Click" ID="aCrbAdvancesAmounttoDay">
                                                                    <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                                        alt="" />
                                                                    <asp:Label runat="server" ID="lblCrSundriesAndAdvancesBalance" Text="0.00" class="ov_nb"></asp:Label>
                                                                    <span class="ov_text">Cr Balance</span>
                                                                </asp:LinkButton></li>
                                                            <li>
                                                                <asp:LinkButton runat="server" OnClick="TotalAdvancesto_Click" ID="aDrbAdvancesAmounttoDay">
                                                                    <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                                        alt="" />
                                                                    <asp:Label runat="server" ID="lblDrSundriesAndAdvancesBalance" Text="0.00" class="ov_nb"></asp:Label>
                                                                    <span class="ov_text">Dr Balance</span>
                                                                </asp:LinkButton></li>
                                                        </ul>
                                                    </div>
                                                </div>
                                                <dx:ASPxGridView  Style="margin: 0 auto;
                                                    width: 100%;" OnSummaryDisplayText="ASPxGridView1_SummaryDisplayTextAdvances"
                                                    OnCustomSummaryCalculate="ASPxGridView1_OnCustomSummaryCalculateAdvances" ID="gridSundriesAndAdvances"
                                                    ClientInstanceName="gridSundriesAndAdvances" runat="server" DataSourceID="MySqldsSundriesAndAdvances"
                                                    OnCustomCallback="grid_CustomCallback">
                                                    <Settings ShowTitlePanel="true" ShowVerticalScrollBar="true" VerticalScrollBarStyle="Virtual"
                                                        VerticalScrollableHeight="300" ShowHeaderFilterButton="true" ShowFooter="true"
                                                        ShowGroupFooter="Hidden" ShowGroupPanel="false" ShowGroupedColumns="true" ShowFilterBar="Visible"
                                                        ShowFilterRow="true" />
                                                    <SettingsText Title="Sundries And Advances" />
                                                    <Templates>
                                                        <GroupRowContent>
                                                            <dx:ASPxLabel ID="GroupLabel" runat="server" Text='<%# GetLabelText(Container)%>'>
                                                            </dx:ASPxLabel>
                                                        </GroupRowContent>
                                                    </Templates>
                                                    <TotalSummary>
                                                        <dx:ASPxSummaryItem FieldName="Credit" SummaryType="Sum" />
                                                        <dx:ASPxSummaryItem FieldName="Debit" SummaryType="Sum" />
                                                        <dx:ASPxSummaryItem FieldName="Narration" SummaryType="Custom" />
                                                    </TotalSummary>
                                                    <SettingsPager Mode="ShowAllRecords">
                                                    </SettingsPager>
                                                    <Columns>
                                                        <dx:GridViewDataColumn FieldName="Date">
                                                        </dx:GridViewDataColumn>
                                                        <dx:GridViewDataColumn FieldName="LedgerHead">
                                                        </dx:GridViewDataColumn>
                                                        <dx:GridViewDataColumn FieldName="Narration">
                                                        </dx:GridViewDataColumn>
                                                        <dx:GridViewDataColumn FieldName="Credit">
                                                        </dx:GridViewDataColumn>
                                                        <dx:GridViewDataColumn FieldName="Debit">
                                                        </dx:GridViewDataColumn>
                                                    </Columns>
                                                    <Styles  Footer-HorizontalAlign="Left" Cell-HorizontalAlign="Left" Header-VerticalAlign="Middle" Header-HorizontalAlign="Center"
                                                        Header-Wrap="True">
                                                        <GroupPanel Wrap="True" HorizontalAlign="Left">
                                                        </GroupPanel>
                                                        <GroupRow Wrap="True" HorizontalAlign="Left">
                                                        </GroupRow>
                                                    </Styles>
                                                </dx:ASPxGridView>
                                                <dx:ASPxGridViewExporter ID="geSundriesAndAdvancesExport" runat="server" GridViewID="gridSundriesAndAdvances">
                                                    <Styles>
                                                        <Header Wrap="True" HorizontalAlign="Center">
                                                        </Header>
                                                        <Cell HorizontalAlign="Left" Wrap="True">
                                                        </Cell>
                                                        <Footer Wrap="True" HorizontalAlign="Left"></Footer>
                                                    </Styles>
                                                </dx:ASPxGridViewExporter>
                                                <asp:SqlDataSource runat="server" ID="MySqldsSundriesAndAdvances" 
                                                    ProviderName="MySql.Data.MySqlClient" />
                                            </div>
                                        </li>
                                        <li id="li_Stamps" class="top_H"><a href="#" class="top_Ha">10. Stamps<span class="st-arrow">Open
                                            or Close</span></a>
                                            <div class="st-content">
                                                <div style="background: #fff !important;" class="row display">
                                                    <div style="background: #fff !important;" class="four columns">
                                                        <p class="inner_heading">
                                                            Yesterday Balance On Stamps</p>
                                                        <ul class="overview_list">
                                                            <li>
                                                                <asp:LinkButton runat="server" OnClick="aCrStampsAmountprDay_Click" ID="aCrStampsAmountprDay">
                                                                    <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                                        alt="" />
                                                                    <asp:Label runat="server" ID="lblCrStampsAmountprDay" Text="0.00" class="ov_nb"></asp:Label>
                                                                    <span class="ov_text">Cr Amount</span>
                                                                </asp:LinkButton></li>
                                                            <li>
                                                                <asp:LinkButton runat="server" OnClick="aDrStampsAmountprDay_Click" ID="aDrStampsAmountprDay">
                                                                    <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                                        alt="" />
                                                                    <asp:Label runat="server" ID="lblDrStampsAmountprDay" Text="0.00" class="ov_nb"></asp:Label>
                                                                    <span class="ov_text">Dr Amount</span>
                                                                </asp:LinkButton></li>
                                                            <li>
                                                                <asp:LinkButton runat="server" OnClick="TotalStampspr_Click" ID="aCrbStampsAmountprDay">
                                                                    <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                                        alt="" />
                                                                    <asp:Label runat="server" ID="lblCrStampsBalanceprDay" Text="0.00" class="ov_nb"></asp:Label>
                                                                    <span class="ov_text">Cr Balance</span>
                                                                </asp:LinkButton></li>
                                                            <li>
                                                                <asp:LinkButton runat="server" OnClick="TotalStampspr_Click" ID="aDrbStampsAmountprDay">
                                                                    <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                                        alt="" />
                                                                    <asp:Label runat="server" ID="lblDrStampsBalanceprDay" Text="0.00" class="ov_nb"></asp:Label>
                                                                    <span class="ov_text">Dr Balance </span>
                                                                </asp:LinkButton></li>
                                                        </ul>
                                                    </div>
                                                    <div style="background: #fff !important;" class="four columns">
                                                        <p class="inner_heading">
                                                            Today Balance On Stamps</p>
                                                        <ul class="overview_list">
                                                            <li>
                                                                <asp:LinkButton runat="server" OnClick="aCrStampsAmountcrDay_Click" ID="aCrStampsAmountcrDay">
                                                                    <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                                        alt="" />
                                                                    <asp:Label runat="server" ID="lblCrStampsAmountFinal" Text="0.00" class="ov_nb"></asp:Label>
                                                                    <span class="ov_text">Cr Amount</span>
                                                                </asp:LinkButton></li>
                                                            <li>
                                                                <asp:LinkButton runat="server" OnClick="aDrStampsAmountcrDay_Click" ID="aDrStampsAmountcrDay">
                                                                    <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                                        alt="" />
                                                                    <asp:Label runat="server" ID="lblDrStampsAmountFinal" Text="0.00" class="ov_nb"></asp:Label>
                                                                    <span class="ov_text">Dr Amount</span>
                                                                </asp:LinkButton></li>
                                                            <li>
                                                                <asp:LinkButton runat="server" OnClick="TotalStampscr_Click" ID="aCrbStampsAmountcrDay">
                                                                    <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                                        alt="" />
                                                                    <asp:Label runat="server" ID="lblCrStampsBalanceFinal" Text="0.00" class="ov_nb"></asp:Label>
                                                                    <span class="ov_text">Cr Balance</span>
                                                                </asp:LinkButton></li>
                                                            <li>
                                                                <asp:LinkButton runat="server" OnClick="TotalStampscr_Click" ID="aDrbStampsAmountcrDay">
                                                                    <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                                        alt="" />
                                                                    <asp:Label runat="server" ID="lblDrStampsBalanceFinal" Text="0.00" class="ov_nb"></asp:Label>
                                                                    <span class="ov_text">Dr Balance</span>
                                                                </asp:LinkButton></li>
                                                        </ul>
                                                    </div>
                                                    <div style="background: #fff !important;" class="four columns">
                                                        <p class="inner_heading">
                                                            Today Transaction On Stamps</p>
                                                        <ul class="overview_list">
                                                            <li>
                                                                <asp:LinkButton runat="server" OnClick="aCrStampsAmounttoDay_Click" ID="aCrStampsAmounttoDay">
                                                                    <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                                        alt="" />
                                                                    <asp:Label runat="server" ID="lblCrStampsAmount" Text="0.00" class="ov_nb"></asp:Label>
                                                                    <span class="ov_text">Cr Amount</span>
                                                                </asp:LinkButton></li>
                                                            <li>
                                                                <asp:LinkButton runat="server" OnClick="aDrStampsAmounttoDay_Click" ID="aDrStampsAmounttoDay">
                                                                    <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                                        alt="" />
                                                                    <asp:Label runat="server" ID="lblDrStampsAmount" Text="0.00" class="ov_nb"></asp:Label>
                                                                    <span class="ov_text">Dr Amount</span>
                                                                </asp:LinkButton></li>
                                                            <li>
                                                                <asp:LinkButton runat="server" OnClick="TotalStampsto_Click" ID="aCrbStampsAmounttoDay">
                                                                    <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                                        alt="" />
                                                                    <asp:Label runat="server" ID="lblCrStampsBalance" Text="0.00" class="ov_nb"></asp:Label>
                                                                    <span class="ov_text">Cr Balance</span>
                                                                </asp:LinkButton></li>
                                                            <li>
                                                                <asp:LinkButton runat="server" OnClick="TotalStampsto_Click" ID="aDrbStampsAmounttoDay">
                                                                    <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                                        alt="" />
                                                                    <asp:Label runat="server" ID="lblDrStampsBalance" Text="0.00" class="ov_nb"></asp:Label>
                                                                    <span class="ov_text">Dr Balance</span>
                                                                </asp:LinkButton></li>
                                                        </ul>
                                                    </div>
                                                </div>
                                                <dx:ASPxGridView  Style="margin: 0 auto;
                                                    width: 100%;" OnSummaryDisplayText="ASPxGridView1_SummaryDisplayTextStamps" ID="gridStamps"
                                                    OnCustomSummaryCalculate="ASPxGridView1_OnCustomSummaryCalculateStamps" ClientInstanceName="gridStamps"
                                                    runat="server" DataSourceID="MySqldsStamps" OnCustomCallback="grid_CustomCallback">
                                                    <Settings ShowTitlePanel="true" ShowVerticalScrollBar="true" VerticalScrollBarStyle="Virtual"
                                                        VerticalScrollableHeight="300" ShowHeaderFilterButton="true" ShowFooter="true"
                                                        ShowGroupFooter="Hidden" ShowGroupPanel="false" ShowGroupedColumns="true" ShowFilterBar="Visible"
                                                        ShowFilterRow="true" />
                                                    <SettingsText Title="Stamps" />
                                                    <Templates>
                                                        <GroupRowContent>
                                                            <dx:ASPxLabel ID="GroupLabel" runat="server" Text='<%# GetLabelText(Container)%>'>
                                                            </dx:ASPxLabel>
                                                        </GroupRowContent>
                                                    </Templates>
                                                    <TotalSummary>
                                                        <dx:ASPxSummaryItem FieldName="Credit" SummaryType="Sum" />
                                                        <dx:ASPxSummaryItem FieldName="Debit" SummaryType="Sum" />
                                                    </TotalSummary>
                                                    <SettingsPager Mode="ShowAllRecords">
                                                    </SettingsPager>
                                                    <Columns>
                                                        <dx:GridViewDataColumn FieldName="Date">
                                                        </dx:GridViewDataColumn>
                                                        <dx:GridViewDataColumn FieldName="Head">
                                                        </dx:GridViewDataColumn>
                                                        <dx:GridViewDataColumn FieldName="LedgerHead">
                                                        </dx:GridViewDataColumn>
                                                        <dx:GridViewDataColumn FieldName="Narration">
                                                        </dx:GridViewDataColumn>
                                                        <dx:GridViewDataColumn FieldName="Credit">
                                                        </dx:GridViewDataColumn>
                                                        <dx:GridViewDataColumn FieldName="Debit">
                                                        </dx:GridViewDataColumn>
                                                    </Columns>
                                                    <Styles  Footer-HorizontalAlign="Left" Cell-HorizontalAlign="Left" Header-VerticalAlign="Middle" Header-HorizontalAlign="Center"
                                                        Header-Wrap="True">
                                                        <GroupPanel Wrap="True" HorizontalAlign="Left">
                                                        </GroupPanel>
                                                        <GroupRow Wrap="True" HorizontalAlign="Left">
                                                        </GroupRow>
                                                    </Styles>
                                                </dx:ASPxGridView>
                                                <dx:ASPxGridViewExporter ID="geStampsExport" runat="server" GridViewID="gridStamps">
                                                    <Styles>
                                                        <Header Wrap="True" HorizontalAlign="Center">
                                                        </Header>
                                                        <Cell HorizontalAlign="Left" Wrap="True">
                                                        </Cell>
                                                        <Footer Wrap="True" HorizontalAlign="Left"></Footer>
                                                    </Styles>
                                                </dx:ASPxGridViewExporter>
                                                <asp:SqlDataSource runat="server" ID="MySqldsStamps" 
                                                    ProviderName="MySql.Data.MySqlClient" />
                                            </div>
                                        </li>
                                        <li id="li_PandL" class="top_H"><a href="#" class="top_Ha">11. Profit And Loss Account<span
                                            class="st-arrow">Open or Close</span></a>
                                            <div class="st-content">
                                                <div style="background: #fff !important;" class="row display">
                                                    <div style="background: #fff !important;" class="four columns">
                                                        <p class="inner_heading">
                                                            Yesterday Balance On Profit And Loss</p>
                                                        <ul class="overview_list">
                                                            <li>
                                                                <asp:LinkButton runat="server" OnClick="aCrProfitLossAmountprDay_Click" ID="aCrProfitLossAmountprDay">
                                                                    <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                                        alt="" />
                                                                    <asp:Label runat="server" ID="lblCrProfitAndLossAmountprDay" Text="0.00" class="ov_nb"></asp:Label>
                                                                    <span class="ov_text">Cr Amount</span>
                                                                </asp:LinkButton></li>
                                                            <li>
                                                                <asp:LinkButton runat="server" OnClick="aDrProfitLossAmountprDay_Click" ID="aDrProfitLossAmountprDay">
                                                                    <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                                        alt="" />
                                                                    <asp:Label runat="server" ID="lblDrProfitAndLossAmountprDay" Text="0.00" class="ov_nb"></asp:Label>
                                                                    <span class="ov_text">Dr Amount</span>
                                                                </asp:LinkButton></li>
                                                            <li>
                                                                <asp:LinkButton runat="server" OnClick="TotalProfitLosspr_Click" ID="aCrbProfitLossAmountprDay">
                                                                    <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                                        alt="" />
                                                                    <asp:Label runat="server" ID="lblCrProfitAndLossBalanceprDay" Text="0.00" class="ov_nb"></asp:Label>
                                                                    <span class="ov_text">Cr Balance</span>
                                                                </asp:LinkButton></li>
                                                            <li>
                                                                <asp:LinkButton runat="server" OnClick="TotalProfitLosspr_Click" ID="aDrbProfitLossAmountprDay">
                                                                    <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                                        alt="" />
                                                                    <asp:Label runat="server" ID="lblDrProfitAndLossBalanceprDay" Text="0.00" class="ov_nb"></asp:Label>
                                                                    <span class="ov_text">Dr Balance </span>
                                                                </asp:LinkButton></li>
                                                        </ul>
                                                    </div>
                                                    <div style="background: #fff !important;" class="four columns">
                                                        <p class="inner_heading">
                                                            Today Balance On Profit And Loss</p>
                                                        <ul class="overview_list">
                                                            <li>
                                                                <asp:LinkButton runat="server" OnClick="aCrProfitLossAmountcrDay_Click" ID="aCrProfitLossAmountcrDay">
                                                                    <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                                        alt="" />
                                                                    <asp:Label runat="server" ID="lblCrProfitAndLossAmountFinal" Text="0.00" class="ov_nb"></asp:Label>
                                                                    <span class="ov_text">Cr Amount</span>
                                                                </asp:LinkButton></li>
                                                            <li>
                                                                <asp:LinkButton runat="server" OnClick="aDrProfitLossAmountcrDay_Click" ID="aDrProfitLossAmountcrDay">
                                                                    <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                                        alt="" />
                                                                    <asp:Label runat="server" ID="lblDrProfitAndLossAmountFinal" Text="0.00" class="ov_nb"></asp:Label>
                                                                    <span class="ov_text">Dr Amount</span>
                                                                </asp:LinkButton></li>
                                                            <li>
                                                                <asp:LinkButton runat="server" OnClick="TotalProfitLosscr_Click" ID="aCrbProfitLossAmountcrDay">
                                                                    <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                                        alt="" />
                                                                    <asp:Label runat="server" ID="lblCrProfitAndLossBalanceFinal" Text="0.00" class="ov_nb"></asp:Label>
                                                                    <span class="ov_text">Cr Balance</span>
                                                                </asp:LinkButton></li>
                                                            <li>
                                                                <asp:LinkButton runat="server" OnClick="TotalProfitLosscr_Click" ID="aDrbProfitLossAmountcrDay">
                                                                    <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                                        alt="" />
                                                                    <asp:Label runat="server" ID="lblDrProfitAndLossBalanceFinal" Text="0.00" class="ov_nb"></asp:Label>
                                                                    <span class="ov_text">Dr Balance</span>
                                                                </asp:LinkButton></li>
                                                        </ul>
                                                    </div>
                                                    <div style="background: #fff !important;" class="four columns">
                                                        <p class="inner_heading">
                                                            Today Transaction On Profit And Loss</p>
                                                        <ul class="overview_list">
                                                            <li>
                                                                <asp:LinkButton runat="server" OnClick="aCrProfitLossAmounttoDay_Click" ID="aCrProfitLossAmounttoDay">
                                                                    <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                                        alt="" />
                                                                    <asp:Label runat="server" ID="lblCrProfitAndLossAmount" Text="0.00" class="ov_nb"></asp:Label>
                                                                    <span class="ov_text">Cr Amount</span>
                                                                </asp:LinkButton></li>
                                                            <li>
                                                                <asp:LinkButton runat="server" OnClick="aDrProfitLossAmounttoDay_Click" ID="aDrProfitLossAmounttoDay">
                                                                    <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                                        alt="" />
                                                                    <asp:Label runat="server" ID="lblDrProfitAndLossAmount" Text="0.00" class="ov_nb"></asp:Label>
                                                                    <span class="ov_text">Dr Amount</span>
                                                                </asp:LinkButton></li>
                                                            <li>
                                                                <asp:LinkButton runat="server" OnClick="TotalProfitLossto_Click" ID="aCrbProfitLossAmounttoDay">
                                                                    <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                                        alt="" />
                                                                    <asp:Label runat="server" ID="lblCrProfitAndLossBalance" Text="0.00" class="ov_nb"></asp:Label>
                                                                    <span class="ov_text">Cr Balance</span>
                                                                </asp:LinkButton></li>
                                                            <li>
                                                                <asp:LinkButton runat="server" OnClick="TotalProfitLossto_Click" ID="aDrbProfitLossAmounttoDay">
                                                                    <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                                        alt="" />
                                                                    <asp:Label runat="server" ID="lblDrProfitAndLossBalance" Text="0.00" class="ov_nb"></asp:Label>
                                                                    <span class="ov_text">Dr Balance</span>
                                                                </asp:LinkButton></li>
                                                        </ul>
                                                    </div>
                                                </div>
                                                <dx:ASPxGridView  Style="margin: 0 auto;
                                                    width: 100%;" OnSummaryDisplayText="ASPxGridView1_SummaryDisplayTextProfitandLoss"
                                                    OnCustomSummaryCalculate="ASPxGridView1_OnCustomSummaryCalculateProfitandLoss"
                                                    ID="gridProfitAndLoss" ClientInstanceName="gridProfitAndLoss" runat="server"
                                                    DataSourceID="MySqldsProfitAndLoss" OnCustomCallback="grid_CustomCallback">
                                                    <Settings ShowTitlePanel="true" ShowVerticalScrollBar="true" VerticalScrollBarStyle="Virtual"
                                                        VerticalScrollableHeight="300" ShowHeaderFilterButton="true" ShowFooter="true"
                                                        ShowGroupFooter="Hidden" ShowGroupPanel="false" ShowGroupedColumns="true" ShowFilterBar="Visible"
                                                        ShowFilterRow="true" />
                                                    <SettingsText Title="Profit And Loss" />
                                                    <TotalSummary>
                                                        <dx:ASPxSummaryItem FieldName="Credit" SummaryType="Sum" />
                                                        <dx:ASPxSummaryItem FieldName="Debit" SummaryType="Sum" />
                                                        <dx:ASPxSummaryItem FieldName="Narration" SummaryType="Custom" />
                                                    </TotalSummary>
                                                    <SettingsPager Mode="ShowAllRecords">
                                                    </SettingsPager>
                                                    <Templates>
                                                        <GroupRowContent>
                                                            <dx:ASPxLabel ID="GroupLabel" runat="server" Text='<%# GetLabelText(Container)%>'>
                                                            </dx:ASPxLabel>
                                                        </GroupRowContent>
                                                    </Templates>
                                                    <Columns>
                                                        <dx:GridViewDataColumn FieldName="Date">
                                                        </dx:GridViewDataColumn>
                                                        <dx:GridViewDataColumn FieldName="LedgerHead">
                                                        </dx:GridViewDataColumn>
                                                        <dx:GridViewDataColumn FieldName="Narration">
                                                        </dx:GridViewDataColumn>
                                                        <dx:GridViewDataColumn FieldName="Credit">
                                                        </dx:GridViewDataColumn>
                                                        <dx:GridViewDataColumn FieldName="Debit">
                                                        </dx:GridViewDataColumn>
                                                    </Columns>
                                                    <Styles  Footer-HorizontalAlign="Left" Cell-HorizontalAlign="Left" Header-VerticalAlign="Middle" Header-HorizontalAlign="Center"
                                                        Header-Wrap="True">
                                                        <GroupPanel Wrap="True" HorizontalAlign="Left">
                                                        </GroupPanel>
                                                        <GroupRow Wrap="True" HorizontalAlign="Left">
                                                        </GroupRow>
                                                    </Styles>
                                                </dx:ASPxGridView>
                                                <dx:ASPxGridViewExporter ID="geProfitAndLossExport" runat="server" GridViewID="gridProfitAndLoss">
                                                    <Styles>
                                                        <Header Wrap="True" HorizontalAlign="Center">
                                                        </Header>
                                                        <Cell HorizontalAlign="Left" Wrap="True">
                                                        </Cell>
                                                        <Footer Wrap="True" HorizontalAlign="Left"></Footer>
                                                    </Styles>
                                                </dx:ASPxGridViewExporter>
                                                <asp:SqlDataSource runat="server" ID="MySqldsProfitAndLoss" 
                                                    ProviderName="MySql.Data.MySqlClient" />
                                            </div>
                                        </li>
                                        <li id="li1" class="top_H"><a href="#" class="top_Ha">12. Cash On Hand<span class="st-arrow">Open
                                            or Close</span></a>
                                            <div class="st-content">
                                                <div style="background: #fff !important;" class="row display">
                                                    <div style="background: #fff !important;" class="four columns">
                                                        <p class="inner_heading">
                                                            Yesterday Balance On Cash</p>
                                                        <ul class="overview_list">
                                                            <li>
                                                                <asp:LinkButton runat="server" OnClick="aCrCashprDay_Click" ID="aCrCashprDay">
                                                                    <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                                        alt="" />
                                                                    <asp:Label runat="server" ID="lblCrCashAmountprDay" Text="0.00" class="ov_nb"></asp:Label>
                                                                    <span class="ov_text">Cr Amount</span>
                                                                </asp:LinkButton></li>
                                                            <li>
                                                                <asp:LinkButton runat="server" OnClick="aDrCashprDay_Click" ID="aDrCashprDay">
                                                                    <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                                        alt="" />
                                                                    <asp:Label runat="server" ID="lblDrCashAmountprDay" Text="0.00" class="ov_nb"></asp:Label>
                                                                    <span class="ov_text">Dr Amount</span>
                                                                </asp:LinkButton></li>
                                                            <li>
                                                                <asp:LinkButton runat="server" OnClick="TotalCashpr_Click" ID="aCrbCashprDay">
                                                                    <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                                        alt="" />
                                                                    <asp:Label runat="server" ID="lblCrCashBalanceprDay" Text="0.00" class="ov_nb"></asp:Label>
                                                                    <span class="ov_text">Cr Balance</span>
                                                                </asp:LinkButton></li>
                                                            <li>
                                                                <asp:LinkButton runat="server" OnClick="TotalCashpr_Click" ID="aDrbCashprDay">
                                                                    <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                                        alt="" />
                                                                    <asp:Label runat="server" ID="lblDrCashBalanceprDay" Text="0.00" class="ov_nb"></asp:Label>
                                                                    <span class="ov_text">Dr Balance</span></asp:LinkButton></li>
                                                        </ul>
                                                    </div>
                                                    <div style="background: #fff !important;" class="four columns">
                                                        <p class="inner_heading">
                                                            Today Balance On Cash</p>
                                                        <ul class="overview_list">
                                                            <li>
                                                                <asp:LinkButton runat="server" OnClick="aCrCashAmountcrDay_Click" ID="aCrCashAmountcrDay">
                                                                    <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                                        alt="" />
                                                                    <asp:Label runat="server" ID="lblCrCashAmountFinal" Text="0.00" class="ov_nb"></asp:Label>
                                                                    <span class="ov_text">Cr Amount</span>
                                                                </asp:LinkButton></li>
                                                            <li>
                                                                <asp:LinkButton runat="server" OnClick="aDrCashAmountcrDay_Click" ID="aDrCashAmountcrDay">
                                                                    <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                                        alt="" />
                                                                    <asp:Label runat="server" ID="lblDrCashAmountFinal" Text="0.00" class="ov_nb"></asp:Label>
                                                                    <span class="ov_text">Dr Amount</span>
                                                                </asp:LinkButton></li>
                                                            <li>
                                                                <asp:LinkButton runat="server" OnClick="TotalCashcr_Click" ID="aCrbCashAmountcrDay">
                                                                    <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                                        alt="" />
                                                                    <asp:Label runat="server" ID="lblCrCashBalanceFinal" Text="0.00" class="ov_nb"></asp:Label>
                                                                    <span class="ov_text">Cr Balance</span>
                                                                </asp:LinkButton></li>
                                                            <li>
                                                                <asp:LinkButton runat="server" OnClick="TotalCashcr_Click" ID="aDrbCashAmountcrDay">
                                                                    <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                                        alt="" />
                                                                    <asp:Label runat="server" ID="lblDrCashBalanceFinal" Text="0.00" class="ov_nb"></asp:Label>
                                                                    <span class="ov_text">Dr Balance</span>
                                                                </asp:LinkButton></li>
                                                        </ul>
                                                    </div>
                                                    <div style="background: #fff !important;" class="four columns">
                                                        <p class="inner_heading">
                                                            Today Transaction On Cash</p>
                                                        <ul class="overview_list">
                                                            <li>
                                                                <asp:LinkButton runat="server" OnClick="aCrCashAmounttoDay_Click" ID="aCrCashAmounttoDay">
                                                                    <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                                        alt="" />
                                                                    <asp:Label runat="server" ID="lblCrCashAmount" Text="0.00" class="ov_nb"></asp:Label>
                                                                    <span class="ov_text">Cr Amount</span>
                                                                </asp:LinkButton></li>
                                                            <li>
                                                                <asp:LinkButton runat="server" OnClick="aDrCashAmounttoDay_Click" ID="aDrCashAmounttoDay">
                                                                    <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                                        alt="" />
                                                                    <asp:Label runat="server" ID="lblDrCashAmount" Text="0.00" class="ov_nb"></asp:Label>
                                                                    <span class="ov_text">Dr Amount</span>
                                                                </asp:LinkButton></li>
                                                            <li>
                                                                <asp:LinkButton runat="server" OnClick="TotalCashto_Click" ID="aCrbCashAmounttoDay">
                                                                    <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                                        alt="" />
                                                                    <asp:Label runat="server" ID="lblCrCashBalance" Text="0.00" class="ov_nb"></asp:Label>
                                                                    <span class="ov_text">Cr Balance</span>
                                                                </asp:LinkButton></li>
                                                            <li>
                                                                <asp:LinkButton runat="server" OnClick="TotalCashto_Click" ID="aDrbCashAmounttoDay">
                                                                    <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                                        alt="" />
                                                                    <asp:Label runat="server" ID="lblDrCashBalance" Text="0.00" class="ov_nb"></asp:Label>
                                                                    <span class="ov_text">Dr Balance</span>
                                                                </asp:LinkButton></li>
                                                        </ul>
                                                    </div>
                                                </div>
                                                <dx:ASPxGridView  Style="margin: 0 auto;
                                                    width: 100%;" OnSummaryDisplayText="ASPxGridView1_SummaryDisplayTextCash" OnCustomSummaryCalculate="ASPxGridView1_OnCustomSummaryCalculateCash"
                                                    ID="gridCash" ClientInstanceName="gridCash" runat="server" DataSourceID="MySqldsCash"
                                                    OnCustomCallback="grid_CustomCallback">
                                                    <Settings ShowTitlePanel="true" ShowVerticalScrollBar="true" VerticalScrollBarStyle="Virtual"
                                                        VerticalScrollableHeight="300" ShowHeaderFilterButton="true" ShowFooter="true"
                                                        ShowGroupFooter="Hidden" ShowGroupPanel="false" ShowGroupedColumns="true" ShowFilterBar="Visible"
                                                        ShowFilterRow="true" />
                                                    <SettingsText Title="Cash" />
                                                    <Templates>
                                                        <GroupRowContent>
                                                            <dx:ASPxLabel ID="GroupLabel" runat="server" Text='<%# GetLabelText(Container)%>'>
                                                            </dx:ASPxLabel>
                                                        </GroupRowContent>
                                                    </Templates>
                                                    <TotalSummary>
                                                        <dx:ASPxSummaryItem FieldName="Credit" SummaryType="Sum" />
                                                        <dx:ASPxSummaryItem FieldName="Debit" SummaryType="Sum" />
                                                        <dx:ASPxSummaryItem FieldName="Narration" SummaryType="Custom" />
                                                    </TotalSummary>
                                                    <SettingsPager Mode="ShowAllRecords">
                                                    </SettingsPager>
                                                    <Templates>
                                                        <GroupRowContent>
                                                            <dx:ASPxLabel ID="GroupLabel" runat="server" Text='<%# GetLabelText(Container)%>'>
                                                            </dx:ASPxLabel>
                                                        </GroupRowContent>
                                                    </Templates>
                                                    <Columns>
                                                        <dx:GridViewDataColumn FieldName="Date">
                                                        </dx:GridViewDataColumn>
                                                        <dx:GridViewDataColumn FieldName="LedgerHead">
                                                        </dx:GridViewDataColumn>
                                                        <dx:GridViewDataColumn FieldName="Narration">
                                                        </dx:GridViewDataColumn>
                                                        <dx:GridViewDataColumn FieldName="Credit">
                                                        </dx:GridViewDataColumn>
                                                        <dx:GridViewDataColumn FieldName="Debit">
                                                        </dx:GridViewDataColumn>
                                                    </Columns>
                                                    <Styles  Footer-HorizontalAlign="Left" Cell-HorizontalAlign="Left" Header-VerticalAlign="Middle" Header-HorizontalAlign="Center"
                                                        Header-Wrap="True">
                                                        <GroupPanel Wrap="True" HorizontalAlign="Left">
                                                        </GroupPanel>
                                                        <GroupRow Wrap="True" HorizontalAlign="Left">
                                                        </GroupRow>
                                                    </Styles>
                                                </dx:ASPxGridView>
                                                <dx:ASPxGridViewExporter ID="geCashExport" runat="server" GridViewID="gridCash">
                                                <Styles>
                                                        <Header Wrap="True" HorizontalAlign="Center">
                                                        </Header>
                                                        <Cell HorizontalAlign="Left" Wrap="True">
                                                        </Cell>
                                                        <Footer Wrap="True" HorizontalAlign="Left"></Footer>
                                                    </Styles>
                                                </dx:ASPxGridViewExporter>
                                                <asp:SqlDataSource runat="server" ID="MySqldsCash" 
                                                    ProviderName="MySql.Data.MySqlClient" />
                                            </div>
                                        </li>
                                    </ul>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <script type="text/javascript">
        $(document).ready(function () {
            prth_documentation.navigator();
            prth_documentation.init();
            SyntaxHighlighter.defaults['toolbar'] = false;
            SyntaxHighlighter.all();
            Keepopen();
            $(".chzn-select").chosen({ search_contains: true });
            prth_mask_input.init();
            if (!is_touch_device) {
                prth_style_sw.init();
            };
        });
        function ResolvePostBack() {
            __doPostBack('ctl00$cphMainContent$mMain', 'CLICK:0');
        }
    </script>
</asp:Content>
