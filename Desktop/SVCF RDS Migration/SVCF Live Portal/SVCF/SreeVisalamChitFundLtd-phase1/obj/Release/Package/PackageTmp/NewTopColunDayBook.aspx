<%@ Page Culture="en-GB" Language="C#" MasterPageFile="~/Branch.Master" AutoEventWireup="true"
    CodeBehind="NewTopColunDayBook.aspx.cs" Inherits="SreeVisalamChitFundLtd_phase1.NewTopColunDayBook"
    Title="DayBook" %>

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
    <title>Pertho Admin Panel</title>
    <!-- Foundation framework -->
    <link rel="stylesheet" href="pertho_admin_v1.3/foundation/stylesheets/foundation.css" />
    <!-- jquery UI -->
    <link rel="stylesheet" href="pertho_admin_v1.3/lib/jQueryUI/css/Aristo/Aristo.css"
        media="all" />
    <!-- fancybox -->
    <link rel="stylesheet" href="pertho_admin_v1.3/lib/fancybox/jquery.fancybox-1.3.4.css"
        media="all" />
    <!-- syntax highlighter -->
    <link rel="stylesheet" href="pertho_admin_v1.3/lib/syntaxhighlighter/styles/shCoreDefault.css">
    <!-- main styles -->
    <link rel="stylesheet" href="pertho_admin_v1.3/css/style.css">
    <!-- Google fonts -->
    
    <!-- Favicons and the like (avoid using transparent .png) -->
    <link rel="shortcut icon" href="favicon.ico" />
    <link rel="apple-touch-icon-precomposed" href="icon.png" />

		
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
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMainContent" runat="server">
    <ajax:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </ajax:ToolkitScriptManager>
    <div class="container">
        <div class="row">
            <div class="twelve columns">
                <div class="box_c">
                    <div class="box_c_heading cf box_actions">
                        <p>
                            Top Column Day Book</p>
                    </div>
                    
                    <asp:button id="BtnPrint" runat="server" onclientclick="javascript:CallPrint('bill');" text="Print" xmlns:asp="#unknown" />
                    <div id="bill" class="box_c_content">
                        <h3 class="sepH_a_line">
                            Sree Visalam Chit Funds Top Column Day Book</h3>
                        <table>
                            <tr>
                                <td>
                                    <dx:ASPxDateEdit ID="dateFromConsolidated" runat="server">
                                    </dx:ASPxDateEdit>
                                </td>
                                <td>
                                    <dx:ASPxDateEdit ID="dateToConsolidated" runat="server">
                                    </dx:ASPxDateEdit>
                                </td>
                                <td style="width: 20px;">
                                </td>
                                <td>
                                    <dx:ASPxButton ID="btnLoanConsolidated" OnClick="btnLoanConsolidated_Click" runat="server"
                                        Text="Load">
                                    </dx:ASPxButton>
                                </td>
                                                                <td style="width:25px;"></td>
                                <td>
                                <dx:ASPxButton ID="ExportButton" runat="server" Text="Export" 
            OnClick="Export_click" />
                                </td>
                            </tr>
                        </table>
                        <br />
                      
                        <div id="st-navBox" class="sepH_a_line">
                        </div>
                        <div id="st-documentation" class="st-accordion">
                            <ul>
                                <li id="li_Branch" class="top_H"><a href="#" class="top_Ha">01. Branch.<span class="st-arrow">Open
                                    or Close</span></a>
                                    <div class="st-content">
                                        <div style="background: #fff !important;" class="row display">
                                            <div style="background: #fff !important;" class="four columns">
                                                <p class="inner_heading">
                                                    Previous Day Balance On Branch</p>
                                                <ul class="overview_list">
                                                    <li><a href="#">
                                                        <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                            alt="">
                                                        <asp:Label runat="server" ID="lblCrBranchAmountprDay" Text="0.00" class="ov_nb"></asp:Label>
                                                        <span class="ov_text">Cr Amount</span> </a></li>
                                                    <li><a href="#">
                                                        <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                            alt="">
                                                        <asp:Label runat="server" ID="lblDrBranchAmountprDay" Text="0.00" class="ov_nb"></asp:Label>
                                                        <span class="ov_text">Dr Amount</span> </a></li>
                                                    <li><a href="#">
                                                        <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                            alt="">
                                                        <asp:Label runat="server" ID="lblCrBranchBalanceprDay" Text="0.00" class="ov_nb"></asp:Label>
                                                        <span class="ov_text">Cr Balance</span> </a></li>
                                                    <li><a href="#">
                                                        <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                            alt="">
                                                        <asp:Label runat="server" ID="lblDrBranchBalanceprDay" Text="0.00" class="ov_nb"></asp:Label>
                                                        <span class="ov_text">Dr Balance </span></a></li>
                                                </ul>
                                            </div>
                                            <div style="background: #fff !important;" class="four columns">
                                                <p class="inner_heading">
                                                    Today Balance On Branch</p>
                                                <ul class="overview_list">
                                                    <li><a href="#">
                                                        <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                            alt="">
                                                        <asp:Label runat="server" ID="lblCrBranchAmountFinal" Text="0.00" class="ov_nb"></asp:Label>
                                                        <span class="ov_text">Cr Amountt</span> </a></li>
                                                    <li><a href="#">
                                                        <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                            alt="">
                                                        <asp:Label runat="server" ID="lblDrBranchAmountFinal" Text="0.00" class="ov_nb"></asp:Label>
                                                        <span class="ov_text">Dr Amount</span> </a></li>
                                                    <li><a href="#">
                                                        <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                            alt="">
                                                        <asp:Label runat="server" ID="lblCrBranchBalanceFinal" Text="0.00" class="ov_nb"></asp:Label>
                                                        <span class="ov_text">Cr Balance</span> </a></li>
                                                    <li><a href="#">
                                                        <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                            alt="">
                                                        <asp:Label runat="server" ID="lblDrBranchBalanceFinal" Text="0.00" class="ov_nb"></asp:Label>
                                                        <span class="ov_text">Dr Balance</span> </a></li>
                                                </ul>
                                            </div>
                                       
                                            <div style="background: #fff !important;" class="four columns">
                                                <p class="inner_heading">
                                                    Today Transaction On Branch</p>
                                                <ul class="overview_list">
                                                    <li><a href="#">
                                                        <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                            alt="">
                                                        <asp:Label runat="server" ID="lblCrBranchAmount" Text="0.00" class="ov_nb"></asp:Label>
                                                        <span class="ov_text">Cr Amountt</span> </a></li>
                                                    <li><a href="#">
                                                        <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                            alt="">
                                                        <asp:Label runat="server" ID="lblDrBranchAmount" Text="0.00" class="ov_nb"></asp:Label>
                                                        <span class="ov_text">Dr Amount</span> </a></li>
                                                    <li><a href="#">
                                                        <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                            alt="">
                                                        <asp:Label runat="server" ID="lblCrBranchBalance" Text="0.00" class="ov_nb"></asp:Label>
                                                        <span class="ov_text">Cr Balance</span> </a></li>
                                                    <li><a href="#">
                                                        <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                            alt="">
                                                        <asp:Label runat="server" ID="lblDrBranchBalance" Text="0.00" class="ov_nb"></asp:Label>
                                                        <span class="ov_text">Dr Balance</span> </a></li>
                                                </ul>
                                            </div>
                                            </div>
                                            <dx:ASPxGridView Styles-AlternatingRow-BackColor="LightGray" Style="margin: 0 auto;
                                                width: 100%;" OnSummaryDisplayText="ASPxGridView1_SummaryDisplayText" OnCustomSummaryCalculate="ASPxGridView1_OnCustomSummaryCalculate"
                                                ID="grid" ClientInstanceName="grid" runat="server" DataSourceID="MySqldsBranch"
                                                OnCustomCallback="grid_CustomCallback">
                                                <Settings ShowVerticalScrollBar="true" VerticalScrollBarStyle="Virtual" VerticalScrollableHeight="430"
                                                    ShowHeaderFilterButton="true" ShowFooter="true" ShowGroupFooter="Hidden" ShowGroupPanel="false"
                                                    ShowGroupedColumns="true" ShowFilterBar="Visible" ShowFilterRow="true" />
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
                                                <Styles>
                                                    <GroupPanel Wrap="True" HorizontalAlign="Left">
                                                    </GroupPanel>
                                                    <GroupRow Wrap="True" HorizontalAlign="Left">
                                                    </GroupRow>
                                                </Styles>
                                            </dx:ASPxGridView>
                                            <dx:ASPxGridViewExporter ID="gridExport" runat="server" GridViewID="grid">
                                            </dx:ASPxGridViewExporter>
                                            <asp:SqlDataSource runat="server" ID="MySqldsBranch" 
                                                ProviderName="MySql.Data.MySqlClient" SelectCommand=" " />
                                        </div>
                                    
                                </li>
                                <li id="li_InvestMents" class="top_H"><a href="#" class="top_Ha">02. InvestMents.<span
                                    class="st-arrow">Open or Close</span></a>
                                    <div class="st-content">
                                        <div style="background: #fff !important;" class="row display">
                                            <div style="background: #fff !important;" class="four columns">
                                                <p class="inner_heading">
                                                    Previous Day Balance On Investments</p>
                                                <ul class="overview_list">
                                                    <li><a href="#">
                                                        <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                            alt="">
                                                        <asp:Label runat="server" ID="lblCrInvestmentsAmountprDay" Text="0.00" class="ov_nb"></asp:Label>
                                                        <span class="ov_text">Cr Amount</span> </a></li>
                                                    <li><a href="#">
                                                        <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                            alt="">
                                                        <asp:Label runat="server" ID="lblDrInvestmentsAmountprDay" Text="0.00" class="ov_nb"></asp:Label>
                                                        <span class="ov_text">Dr Amount</span> </a></li>
                                                    <li><a href="#">
                                                        <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                            alt="">
                                                        <asp:Label runat="server" ID="lblCrInvestmentsBalanceprDay" Text="0.00" class="ov_nb"></asp:Label>
                                                        <span class="ov_text">Cr Balance</span> </a></li>
                                                    <li><a href="#">
                                                        <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                            alt="">
                                                        <asp:Label runat="server" ID="lblDrInvestmentsBalanceprDay" Text="0.00" class="ov_nb"></asp:Label>
                                                        <span class="ov_text">Dr Balance </span></a></li>
                                                </ul>
                                            </div>
                                            <div style="background: #fff !important;" class="four columns">
                                                <p class="inner_heading">
                                                    Today Balance On Investments</p>
                                                <ul class="overview_list">
                                                    <li><a href="#">
                                                        <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                            alt="">
                                                        <asp:Label runat="server" ID="lblCrInvestmentsAmountFinal" Text="0.00" class="ov_nb"></asp:Label>
                                                        <span class="ov_text">Cr Amountt</span> </a></li>
                                                    <li><a href="#">
                                                        <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                            alt="">
                                                        <asp:Label runat="server" ID="lblDrInvestmentsAmountFinal" Text="0.00" class="ov_nb"></asp:Label>
                                                        <span class="ov_text">Dr Amount</span> </a></li>
                                                    <li><a href="#">
                                                        <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                            alt="">
                                                        <asp:Label runat="server" ID="lblCrInvestmentsBalanceFinal" Text="0.00" class="ov_nb"></asp:Label>
                                                        <span class="ov_text">Cr Balance</span> </a></li>
                                                    <li><a href="#">
                                                        <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                            alt="">
                                                        <asp:Label runat="server" ID="lblDrInvestmentsBalanceFinal" Text="0.00" class="ov_nb"></asp:Label>
                                                        <span class="ov_text">Dr Balance</span> </a></li>
                                                </ul>
                                            </div>
                                        
                                            <div style="background: #fff !important;" class="four columns">
                                                <p class="inner_heading">
                                                    Today Transaction On Investments</p>
                                                <ul class="overview_list">
                                                    <li><a href="#">
                                                        <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                            alt="">
                                                        <asp:Label runat="server" ID="lblCrInvestmentsAmount" Text="0.00" class="ov_nb"></asp:Label>
                                                        <span class="ov_text">Cr Amountt</span> </a></li>
                                                    <li><a href="#">
                                                        <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                            alt="">
                                                        <asp:Label runat="server" ID="lblDrInvestmentsAmount" Text="0.00" class="ov_nb"></asp:Label>
                                                        <span class="ov_text">Dr Amount</span> </a></li>
                                                    <li><a href="#">
                                                        <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                            alt="">
                                                        <asp:Label runat="server" ID="lblCrInvestmentsBalance" Text="0.00" class="ov_nb"></asp:Label>
                                                        <span class="ov_text">Cr Balance</span> </a></li>
                                                    <li><a href="#">
                                                        <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                            alt="">
                                                        <asp:Label runat="server" ID="lblDrInvestmentsBalance" Text="0.00" class="ov_nb"></asp:Label>
                                                        <span class="ov_text">Dr Balance</span> </a></li>
                                                </ul>
                                            </div>
                                            </div>
                                            <dx:ASPxGridView Styles-AlternatingRow-BackColor="LightGray" Style="margin: 0 auto;
                                                width: 100%;" OnSummaryDisplayText="ASPxGridView1_SummaryDisplayText" OnCustomSummaryCalculate="ASPxGridView1_OnCustomSummaryCalculate"
                                                ID="gridInvestments" ClientInstanceName="gridInvestments" runat="server" DataSourceID="MySqldsInvestments"
                                                OnCustomCallback="grid_CustomCallback">
                                                <Settings VerticalScrollableHeight="272" ShowVerticalScrollBar="true" VerticalScrollBarStyle="Standard"
                                                    ShowHeaderFilterButton="true" ShowFooter="true" ShowGroupFooter="Hidden" ShowGroupPanel="false"
                                                    ShowGroupedColumns="true" ShowFilterBar="Visible" ShowFilterRow="true" />


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
                                                <Styles>
                                                    <GroupPanel Wrap="True" HorizontalAlign="Left">
                                                    </GroupPanel>
                                                    <GroupRow Wrap="True" HorizontalAlign="Left">
                                                    </GroupRow>
                                                </Styles>
                                            </dx:ASPxGridView>
                                            <dx:ASPxGridViewExporter ID="geInvestmentsExport" runat="server" GridViewID="gridInvestments">
                                            </dx:ASPxGridViewExporter>
                                            <asp:SqlDataSource runat="server" ID="MySqldsInvestments" 
                                                ProviderName="MySql.Data.MySqlClient" SelectCommand=" " />
                                        
                                    </div>
                                </li>
                                <li id="li_Banks" class="top_H"><a href="#" class="top_Ha">03. Banks.<span class="st-arrow">Open
                                    or Close</span></a>
                                    <div class="st-content">
                                        <div style="background: #fff !important;" class="row display">
                                            <div style="background: #fff !important;" class="four columns">
                                                <p class="inner_heading">
                                                    Previous Day Balance On Banks</p>
                                                <ul class="overview_list">
                                                    <li><a href="#">
                                                        <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                            alt="">
                                                        <asp:Label runat="server" ID="lblCrBanksAmountprDay" Text="0.00" class="ov_nb"></asp:Label>
                                                        <span class="ov_text">Cr Amount</span> </a></li>
                                                    <li><a href="#">
                                                        <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                            alt="">
                                                        <asp:Label runat="server" ID="lblDrBanksAmountprDay" Text="0.00" class="ov_nb"></asp:Label>
                                                        <span class="ov_text">Dr Amount</span> </a></li>
                                                    <li><a href="#">
                                                        <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                            alt="">
                                                        <asp:Label runat="server" ID="lblCrBanksBalanceprDay" Text="0.00" class="ov_nb"></asp:Label>
                                                        <span class="ov_text">Cr Balance</span> </a></li>
                                                    <li><a href="#">
                                                        <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                            alt="">
                                                        <asp:Label runat="server" ID="lblDrBanksBalanceprDay" Text="0.00" class="ov_nb"></asp:Label>
                                                        <span class="ov_text">Dr Balance </span></a></li>
                                                </ul>
                                            </div>
                                            <div style="background: #fff !important;" class="four columns">
                                                <p class="inner_heading">
                                                    Today Balance On Banks</p>
                                                <ul class="overview_list">
                                                    <li><a href="#">
                                                        <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                            alt="">
                                                        <asp:Label runat="server" ID="lblCrBanksAmountFinal" Text="0.00" class="ov_nb"></asp:Label>
                                                        <span class="ov_text">Cr Amountt</span> </a></li>
                                                    <li><a href="#">
                                                        <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                            alt="">
                                                        <asp:Label runat="server" ID="lblDrBanksAmountFinal" Text="0.00" class="ov_nb"></asp:Label>
                                                        <span class="ov_text">Dr Amount</span> </a></li>
                                                    <li><a href="#">
                                                        <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                            alt="">
                                                        <asp:Label runat="server" ID="lblCrBanksBalanceFinal" Text="0.00" class="ov_nb"></asp:Label>
                                                        <span class="ov_text">Cr Balance</span> </a></li>
                                                    <li><a href="#">
                                                        <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                            alt="">
                                                        <asp:Label runat="server" ID="lblDrBanksBalanceFinal" Text="0.00" class="ov_nb"></asp:Label>
                                                        <span class="ov_text">Dr Balance</span> </a></li>
                                                </ul>
                                            </div>
                                        
                                            <div style="background: #fff !important;" class="four columns">
                                                <p class="inner_heading">
                                                    Today Transaction On Banks</p>
                                                <ul class="overview_list">
                                                    <li><a href="#">
                                                        <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                            alt="">
                                                        <asp:Label runat="server" ID="lblCrBanksAmount" Text="0.00" class="ov_nb"></asp:Label>
                                                        <span class="ov_text">Cr Amountt</span> </a></li>
                                                    <li><a href="#">
                                                        <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                            alt="">
                                                        <asp:Label runat="server" ID="lblDrBanksAmount" Text="0.00" class="ov_nb"></asp:Label>
                                                        <span class="ov_text">Dr Amount</span> </a></li>
                                                    <li><a href="#">
                                                        <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                            alt="">
                                                        <asp:Label runat="server" ID="lblCrBanksBalance" Text="0.00" class="ov_nb"></asp:Label>
                                                        <span class="ov_text">Cr Balance</span> </a></li>
                                                    <li><a href="#">
                                                        <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                            alt="">
                                                        <asp:Label runat="server" ID="lblDrBanksBalance" Text="0.00" class="ov_nb"></asp:Label>
                                                        <span class="ov_text">Dr Balance</span> </a></li>
                                                </ul>
                                            </div>
                                            </div>
                                            <dx:ASPxGridView Styles-AlternatingRow-BackColor="LightGray" Style="margin: 0 auto;
                                                width: 100%;" OnSummaryDisplayText="ASPxGridView1_SummaryDisplayText" OnCustomSummaryCalculate="ASPxGridView1_OnCustomSummaryCalculate"
                                                ID="gridBanks" ClientInstanceName="gridBanks" runat="server" DataSourceID="MySqldsBanks"
                                                OnCustomCallback="grid_CustomCallback">
                                                <Settings VerticalScrollableHeight="272" ShowVerticalScrollBar="true" VerticalScrollBarStyle="Standard"
                                                    ShowHeaderFilterButton="true" ShowGroupFooter="Hidden" ShowFooter="true" ShowGroupedColumns="true"
                                                    ShowGroupPanel="false" ShowFilterBar="Visible" ShowFilterRow="true" />
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
                                                <Styles>
                                                    <GroupPanel Wrap="True" HorizontalAlign="Left">
                                                    </GroupPanel>
                                                    <GroupRow Wrap="True" HorizontalAlign="Left">
                                                    </GroupRow>
                                                </Styles>
                                            </dx:ASPxGridView>
                                            <dx:ASPxGridViewExporter ID="geBanksExport" runat="server" GridViewID="gridBanks">
                                            </dx:ASPxGridViewExporter>
                                            <asp:SqlDataSource runat="server" ID="MySqldsBanks" 
                                                ProviderName="MySql.Data.MySqlClient" SelectCommand=" " />
                                       
                                    </div>
                                </li>
                                <li id="li_Other Items" class="top_H"><a href="#" class="top_Ha">04. Other Items.<span
                                    class="st-arrow">Open or Close</span></a>
                                    <div class="st-content">
                                        <div style="background: #fff !important;" class="row display">
                                            <div style="background: #fff !important;" class="four columns">
                                                <p class="inner_heading">
                                                    Previous Day Balance On OtherItems</p>
                                                <ul class="overview_list">
                                                    <li><a href="#">
                                                        <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                            alt="">
                                                        <asp:Label runat="server" ID="lblCrOtherItemsAmountprDay" Text="0.00" class="ov_nb"></asp:Label>
                                                        <span class="ov_text">Cr Amount</span> </a></li>
                                                    <li><a href="#">
                                                        <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                            alt="">
                                                        <asp:Label runat="server" ID="lblDrOtherItemsAmountprDay" Text="0.00" class="ov_nb"></asp:Label>
                                                        <span class="ov_text">Dr Amount</span> </a></li>
                                                    <li><a href="#">
                                                        <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                            alt="">
                                                        <asp:Label runat="server" ID="lblCrOtherItemsBalanceprDay" Text="0.00" class="ov_nb"></asp:Label>
                                                        <span class="ov_text">Cr Balance</span> </a></li>
                                                    <li><a href="#">
                                                        <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                            alt="">
                                                        <asp:Label runat="server" ID="lblDrOtherItemsBalanceprDay" Text="0.00" class="ov_nb"></asp:Label>
                                                        <span class="ov_text">Dr Balance </span></a></li>
                                                </ul>
                                            </div>
                                            <div style="background: #fff !important;" class="four columns">
                                                <p class="inner_heading">
                                                    Today Balance On OtherItems</p>
                                                <ul class="overview_list">
                                                    <li><a href="#">
                                                        <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                            alt="">
                                                        <asp:Label runat="server" ID="lblCrOtherItemsAmountFinal" Text="0.00" class="ov_nb"></asp:Label>
                                                        <span class="ov_text">Cr Amountt</span> </a></li>
                                                    <li><a href="#">
                                                        <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                            alt="">
                                                        <asp:Label runat="server" ID="lblDrOtherItemsAmountFinal" Text="0.00" class="ov_nb"></asp:Label>
                                                        <span class="ov_text">Dr Amount</span> </a></li>
                                                    <li><a href="#">
                                                        <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                            alt="">
                                                        <asp:Label runat="server" ID="lblCrOtherItemsBalanceFinal" Text="0.00" class="ov_nb"></asp:Label>
                                                        <span class="ov_text">Cr Balance</span> </a></li>
                                                    <li><a href="#">
                                                        <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                            alt="">
                                                        <asp:Label runat="server" ID="lblDrOtherItemsBalanceFinal" Text="0.00" class="ov_nb"></asp:Label>
                                                        <span class="ov_text">Dr Balance</span> </a></li>
                                                </ul>
                                            </div>
                                       
                                            <div style="background: #fff !important;" class="four columns">
                                                <p class="inner_heading">
                                                    Today Transaction On OtherItems</p>
                                                <ul class="overview_list">
                                                    <li><a href="#">
                                                        <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                            alt="">
                                                        <asp:Label runat="server" ID="lblCrOtherItemsAmount" Text="0.00" class="ov_nb"></asp:Label>
                                                        <span class="ov_text">Cr Amountt</span> </a></li>
                                                    <li><a href="#">
                                                        <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                            alt="">
                                                        <asp:Label runat="server" ID="lblDrOtherItemsAmount" Text="0.00" class="ov_nb"></asp:Label>
                                                        <span class="ov_text">Dr Amount</span> </a></li>
                                                    <li><a href="#">
                                                        <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                            alt="">
                                                        <asp:Label runat="server" ID="lblCrOtherItemsBalance" Text="0.00" class="ov_nb"></asp:Label>
                                                        <span class="ov_text">Cr Balance</span> </a></li>
                                                    <li><a href="#">
                                                        <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                            alt="">
                                                        <asp:Label runat="server" ID="lblDrOtherItemsBalance" Text="0.00" class="ov_nb"></asp:Label>
                                                        <span class="ov_text">Dr Balance</span> </a></li>
                                                </ul>
                                            </div>
                                            </div>
                                            <dx:ASPxGridView Style="margin: 0 auto; width: 100%;" Styles-AlternatingRow-BackColor="LightGray"
                                                OnSummaryDisplayText="ASPxGridView1_SummaryDisplayText" OnCustomSummaryCalculate="ASPxGridView1_OnCustomSummaryCalculate"
                                                ID="gridOtherItems" ClientInstanceName="gridOtherItems" runat="server" DataSourceID="MySqldsOtherItems"
                                                OnCustomCallback="grid_CustomCallback">
                                                <Settings VerticalScrollableHeight="272" ShowVerticalScrollBar="true" VerticalScrollBarStyle="Standard"
                                                    ShowHeaderFilterButton="true" ShowGroupFooter="Hidden" ShowFooter="true" ShowGroupedColumns="true"
                                                    ShowGroupPanel="false" ShowFilterBar="Visible" ShowFilterRow="true" />
                <Templates>
        <GroupRowContent><dx:ASPxLabel ID="GroupLabel" runat="server" Text='<%# GetLabelText(Container)%>' ></dx:ASPxLabel>
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
                                                <Styles>
                                                    <GroupPanel Wrap="True" HorizontalAlign="Left">
                                                    </GroupPanel>
                                                    <GroupRow Wrap="True" HorizontalAlign="Left">
                                                    </GroupRow>
                                                </Styles>
                                            </dx:ASPxGridView>
                                            <dx:ASPxGridViewExporter ID="geOtherItemsExport" runat="server" GridViewID="gridOtherItems">
                                            </dx:ASPxGridViewExporter>
                                            <asp:SqlDataSource runat="server" ID="MySqldsOtherItems" 
                                                ProviderName="MySql.Data.MySqlClient" SelectCommand=" " />
                                        </div>
                                  
                                </li>
                                <li id="li_Chits" class="top_H"><a href="#" class="top_Ha">05. Chits.<span class="st-arrow">Open
                                    or Close</span></a>
                                    <div class="st-content">
                                        <div style="background: #fff !important;" class="row display">
                                            <div style="background: #fff !important;" class="four columns">
                                                <p class="inner_heading">
                                                    Previous Day Balance On Chits</p>
                                                <ul class="overview_list">
                                                    <li><a href="#">
                                                        <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                            alt="">
                                                        <asp:Label runat="server" ID="lblCrChitsAmountprDay" Text="0.00" class="ov_nb"></asp:Label>
                                                        <span class="ov_text">Cr Amount</span> </a></li>
                                                    <li><a href="#">
                                                        <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                            alt="">
                                                        <asp:Label runat="server" ID="lblDrChitsAmountprDay" Text="0.00" class="ov_nb"></asp:Label>
                                                        <span class="ov_text">Dr Amount</span> </a></li>
                                                    <li><a href="#">
                                                        <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                            alt="">
                                                        <asp:Label runat="server" ID="lblCrChitsBalanceprDay" Text="0.00" class="ov_nb"></asp:Label>
                                                        <span class="ov_text">Cr Balance</span> </a></li>
                                                    <li><a href="#">
                                                        <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                            alt="">
                                                        <asp:Label runat="server" ID="lblDrChitsBalanceprDay" Text="0.00" class="ov_nb"></asp:Label>
                                                        <span class="ov_text">Dr Balance </span></a></li>
                                                </ul>
                                            </div>
                                            <div style="background: #fff !important;" class="four columns">
                                                <p class="inner_heading">
                                                    Today Balance On Chits</p>
                                                <ul class="overview_list">
                                                    <li><a href="#">
                                                        <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                            alt="">
                                                        <asp:Label runat="server" ID="lblCrChitsAmountFinal" Text="0.00" class="ov_nb"></asp:Label>
                                                        <span class="ov_text">Cr Amountt</span> </a></li>
                                                    <li><a href="#">
                                                        <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                            alt="">
                                                        <asp:Label runat="server" ID="lblDrChitsAmountFinal" Text="0.00" class="ov_nb"></asp:Label>
                                                        <span class="ov_text">Dr Amount</span> </a></li>
                                                    <li><a href="#">
                                                        <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                            alt="">
                                                        <asp:Label runat="server" ID="lblCrChitsBalanceFinal" Text="0.00" class="ov_nb"></asp:Label>
                                                        <span class="ov_text">Cr Balance</span> </a></li>
                                                    <li><a href="#">
                                                        <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                            alt="">
                                                        <asp:Label runat="server" ID="lblDrChitsBalanceFinal" Text="0.00" class="ov_nb"></asp:Label>
                                                        <span class="ov_text">Dr Balance</span> </a></li>
                                                </ul>
                                            </div>
                                        
                                            <div style="background: #fff !important;" class="four columns">
                                                <p class="inner_heading">
                                                    Today Transaction On Chits</p>
                                                <ul class="overview_list">
                                                    <li><a href="#">
                                                        <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                            alt="">
                                                        <asp:Label runat="server" ID="lblCrChitsAmount" Text="0.00" class="ov_nb"></asp:Label>
                                                        <span class="ov_text">Cr Amountt</span> </a></li>
                                                    <li><a href="#">
                                                        <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                            alt="">
                                                        <asp:Label runat="server" ID="lblDrChitsAmount" Text="0.00" class="ov_nb"></asp:Label>
                                                        <span class="ov_text">Dr Amount</span> </a></li>
                                                    <li><a href="#">
                                                        <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                            alt="">
                                                        <asp:Label runat="server" ID="lblCrChitsBalance" Text="0.00" class="ov_nb"></asp:Label>
                                                        <span class="ov_text">Cr Balance</span> </a></li>
                                                    <li><a href="#">
                                                        <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                            alt="">
                                                        <asp:Label runat="server" ID="lblDrChitsBalance" Text="0.00" class="ov_nb"></asp:Label>
                                                        <span class="ov_text">Dr Balance</span> </a></li>
                                                </ul>
                                            </div>
                                            </div>
                                            <dx:ASPxGridView Styles-AlternatingRow-BackColor="LightGray" Style="margin: 0 auto;
                                                width: 100%;" OnSummaryDisplayText="ASPxGridView1_SummaryDisplayText" OnCustomSummaryCalculate="ASPxGridView1_OnCustomSummaryCalculate"
                                                ID="gridChits" ClientInstanceName="gridChits" runat="server" DataSourceID="MySqldsChits"
                                                OnCustomCallback="grid_CustomCallback">
                                                <Settings ShowVerticalScrollBar="true" VerticalScrollBarStyle="Virtual" VerticalScrollableHeight="430"
                                                    ShowHeaderFilterButton="true" ShowFooter="true" ShowGroupFooter="Hidden" ShowGroupPanel="false"
                                                    ShowGroupedColumns="true" ShowFilterBar="Visible" ShowFilterRow="true" />
        
        <Templates>
        <GroupRowContent><dx:ASPxLabel ID="GroupLabel" runat="server" Text='<%# GetLabelText(Container)%>' ></dx:ASPxLabel>
        </GroupRowContent>
        </Templates>
        
        <TotalSummary>
            <dx:ASPxSummaryItem  FieldName="Credit" SummaryType="Sum" />
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
                                                <Styles>
                                                    <GroupPanel Wrap="True" HorizontalAlign="Left">
                                                    </GroupPanel>
                                                    <GroupRow Wrap="True" HorizontalAlign="Left">
                                                    </GroupRow>
                                                </Styles>
                                            </dx:ASPxGridView>
                                            <dx:ASPxGridViewExporter ID="geChitsExport" runat="server" GridViewID="gridChits">
                                            </dx:ASPxGridViewExporter>
                                            <asp:SqlDataSource runat="server" ID="MySqldsChits" 
                                                ProviderName="MySql.Data.MySqlClient" SelectCommand=" " />
                                        </div>
                                  
                                </li>
                                <li id="li_CompanyChits" class="top_H"><a href="#" class="top_Ha">06. Company Chits<span
                                    class="st-arrow">Open or Close</span></a>
                                    <div class="st-content">
                                        <div style="background: #fff !important;" class="row display">
                                            <div style="background: #fff !important;" class="four columns">
                                                <p class="inner_heading">
                                                    Previous Day Balance On ForemanChits</p>
                                                <ul class="overview_list">
                                                    <li><a href="#">
                                                        <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                            alt="">
                                                        <asp:Label runat="server" ID="lblCrForemanChitsAmountprDay" Text="0.00" class="ov_nb"></asp:Label>
                                                        <span class="ov_text">Cr Amount</span> </a></li>
                                                    <li><a href="#">
                                                        <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                            alt="">
                                                        <asp:Label runat="server" ID="lblDrForemanChitsAmountprDay" Text="0.00" class="ov_nb"></asp:Label>
                                                        <span class="ov_text">Dr Amount</span> </a></li>
                                                    <li><a href="#">
                                                        <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                            alt="">
                                                        <asp:Label runat="server" ID="lblCrForemanChitsBalanceprDay" Text="0.00" class="ov_nb"></asp:Label>
                                                        <span class="ov_text">Cr Balance</span> </a></li>
                                                    <li><a href="#">
                                                        <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                            alt="">
                                                        <asp:Label runat="server" ID="lblDrForemanChitsBalanceprDay" Text="0.00" class="ov_nb"></asp:Label>
                                                        <span class="ov_text">Dr Balance </span></a></li>
                                                </ul>
                                            </div>
                                            <div style="background: #fff !important;" class="four columns">
                                                <p class="inner_heading">
                                                    Today Balance On ForemanChits</p>
                                                <ul class="overview_list">
                                                    <li><a href="#">
                                                        <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                            alt="">
                                                        <asp:Label runat="server" ID="lblCrForemanChitsAmountFinal" Text="0.00" class="ov_nb"></asp:Label>
                                                        <span class="ov_text">Cr Amountt</span> </a></li>
                                                    <li><a href="#">
                                                        <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                            alt="">
                                                        <asp:Label runat="server" ID="lblDrForemanChitsAmountFinal" Text="0.00" class="ov_nb"></asp:Label>
                                                        <span class="ov_text">Dr Amount</span> </a></li>
                                                    <li><a href="#">
                                                        <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                            alt="">
                                                        <asp:Label runat="server" ID="lblCrForemanChitsBalanceFinal" Text="0.00" class="ov_nb"></asp:Label>
                                                        <span class="ov_text">Cr Balance</span> </a></li>
                                                    <li><a href="#">
                                                        <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                            alt="">
                                                        <asp:Label runat="server" ID="lblDrForemanChitsBalanceFinal" Text="0.00" class="ov_nb"></asp:Label>
                                                        <span class="ov_text">Dr Balance</span> </a></li>
                                                </ul>
                                            </div>
                                       
                                            <div style="background: #fff !important;" class="four columns">
                                                <p class="inner_heading">
                                                    Today Transaction On ForemanChits</p>
                                                <ul class="overview_list">
                                                    <li><a href="#">
                                                        <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                            alt="">
                                                        <asp:Label runat="server" ID="lblCrForemanChitsAmount" Text="0.00" class="ov_nb"></asp:Label>
                                                        <span class="ov_text">Cr Amountt</span> </a></li>
                                                    <li><a href="#">
                                                        <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                            alt="">
                                                        <asp:Label runat="server" ID="lblDrForemanChitsAmount" Text="0.00" class="ov_nb"></asp:Label>
                                                        <span class="ov_text">Dr Amount</span> </a></li>
                                                    <li><a href="#">
                                                        <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                            alt="">
                                                        <asp:Label runat="server" ID="lblCrForemanChitsBalance" Text="0.00" class="ov_nb"></asp:Label>
                                                        <span class="ov_text">Cr Balance</span> </a></li>
                                                    <li><a href="#">
                                                        <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                            alt="">
                                                        <asp:Label runat="server" ID="lblDrForemanChitsBalance" Text="0.00" class="ov_nb"></asp:Label>
                                                        <span class="ov_text">Dr Balance</span> </a></li>
                                                </ul>
                                            </div>
                                            </div>
                                            <dx:ASPxGridView Styles-AlternatingRow-BackColor="LightGray" Style="margin: 0 auto;
                                                width: 100%;" OnSummaryDisplayText="ASPxGridView1_SummaryDisplayText" OnCustomSummaryCalculate="ASPxGridView1_OnCustomSummaryCalculate"
                                                ID="gridForemanChits" ClientInstanceName="gridForemanChits" runat="server" DataSourceID="MySqldsForemanChits"
                                                OnCustomCallback="grid_CustomCallback">
                                                <Settings ShowVerticalScrollBar="true" VerticalScrollBarStyle="Virtual" VerticalScrollableHeight="430"
                                                    ShowHeaderFilterButton="true" ShowFooter="true" ShowGroupFooter="Hidden" ShowGroupPanel="false"
                                                    ShowGroupedColumns="true" ShowFilterBar="Visible" ShowFilterRow="true" />
        
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
        
                                                <Styles>
                                                    <GroupPanel Wrap="True" HorizontalAlign="Left">
                                                    </GroupPanel>
                                                    <GroupRow Wrap="True" HorizontalAlign="Left">
                                                    </GroupRow>
                                                </Styles>
                                            </dx:ASPxGridView>
                                            <dx:ASPxGridViewExporter ID="geForemanChitsExport" runat="server" GridViewID="gridForemanChits">
                                            </dx:ASPxGridViewExporter>
                                            <asp:SqlDataSource runat="server" ID="MySqldsForemanChits" 
                                                ProviderName="MySql.Data.MySqlClient" SelectCommand=" " />
                                        </div>
                                   
                                </li>
                                <li id="li_DecreeDebtors" class="top_H"><a href="#" class="top_Ha">07. Decree Debtors.<span
                                    class="st-arrow">Open or Close</span></a>
<div class="st-content">
                                       
                                       <div style="background:#fff !important;" class="row display">
						
                                          <div style="background:#fff !important;" class="four columns">
                                          <p class="inner_heading">Previous Day Balance On DecreeDebtors</p>
                                                                                    <ul class="overview_list">
								<li>
									<a href="#">
										<img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)" alt="">
										<asp:Label runat="server" ID="lblCrDecreeDebtorsAmountprDay" Text="0.00" class="ov_nb"></asp:Label>
										<span class="ov_text">Cr Amount</span>
									</a>
								</li>
								<li>
									<a href="#">
										<img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)" alt="">
										<asp:Label runat="server" ID="lblDrDecreeDebtorsAmountprDay" Text="0.00" class="ov_nb"></asp:Label>
										<span class="ov_text">Dr Amount</span>
									</a>
								</li>
								<li>
									<a href="#">
										<img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)" alt="">
										<asp:Label runat="server" ID="lblCrDecreeDebtorsBalanceprDay" Text="0.00" class="ov_nb"></asp:Label>
										<span class="ov_text">Cr Balance</span>
									</a>
								</li>
								<li>
									<a href="#">
										<img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)" alt="">
										<asp:Label runat="server" ID="lblDrDecreeDebtorsBalanceprDay" Text="0.00" class="ov_nb"></asp:Label>
										
										<span class="ov_text">Dr Balance </span>
									</a>
								</li>
							</ul>
                                          
                                          </div> 
                                          <div style="background:#fff !important;" class="four columns">
                                           <p class="inner_heading">Today Balance On DecreeDebtors</p>
                                                                                     <ul class="overview_list">
								<li>
									<a href="#">
										<img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)" alt="">
										<asp:Label runat="server" ID="lblCrDecreeDebtorsAmountFinal" Text="0.00" class="ov_nb"></asp:Label>
										<span class="ov_text">Cr Amountt</span>
									</a>
								</li>
								<li>
									<a href="#">
										<img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)" alt="">
										<asp:Label runat="server" ID="lblDrDecreeDebtorsAmountFinal" Text="0.00" class="ov_nb"></asp:Label>
										<span class="ov_text">Dr Amount</span>
									</a>
								</li>
								<li>
									<a href="#">
										<img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)" alt="">
										<asp:Label runat="server" ID="lblCrDecreeDebtorsBalanceFinal" Text="0.00" class="ov_nb"></asp:Label>
										<span class="ov_text">Cr Balance</span>
									</a>
								</li>
								<li>
									<a href="#">
										<img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)" alt="">
										<asp:Label runat="server" ID="lblDrDecreeDebtorsBalanceFinal" Text="0.00" class="ov_nb"></asp:Label>
										
										<span class="ov_text">Dr Balance</span>
									</a>
								</li>
							</ul>
                                           
                                          </div>
                                         
                                          <div style="background:#fff !important;" class="four columns">
                                           <p class="inner_heading">Today Transaction On DecreeDebtors</p>
                                                                                     <ul class="overview_list">
								<li>
									<a href="#">
										<img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)" alt="">
										<asp:Label runat="server" ID="lblCrDecreeDebtorsAmount" Text="0.00" class="ov_nb"></asp:Label>
										<span class="ov_text">Cr Amountt</span>
									</a>
								</li>
								<li>
									<a href="#">
										<img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)" alt="">
										<asp:Label runat="server" ID="lblDrDecreeDebtorsAmount" Text="0.00" class="ov_nb"></asp:Label>
										<span class="ov_text">Dr Amount</span>
									</a>
								</li>
								<li>
									<a href="#">
										<img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)" alt="">
										<asp:Label runat="server" ID="lblCrDecreeDebtorsBalance" Text="0.00" class="ov_nb"></asp:Label>
										<span class="ov_text">Cr Balance</span>
									</a>
								</li>
								<li>
									<a href="#">
										<img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)" alt="">
										<asp:Label runat="server" ID="lblDrDecreeDebtorsBalance" Text="0.00" class="ov_nb"></asp:Label>
										
										<span class="ov_text">Dr Balance</span>
									</a>
								</li>
							</ul>
                                          </div>
                                          
                                          </div>
                                           <dx:ASPxGridView Styles-AlternatingRow-BackColor="LightGray" Style="margin: 0 auto; width: 100%;" OnSummaryDisplayText="ASPxGridView1_SummaryDisplayText" OnCustomSummaryCalculate="ASPxGridView1_OnCustomSummaryCalculate"
        ID="gridDecreeDebtors" ClientInstanceName="gridDecreeDebtors" runat="server" DataSourceID="MySqldsDecreeDebtors"
        OnCustomCallback="grid_CustomCallback"> 
        <Settings  ShowVerticalScrollBar="true" VerticalScrollBarStyle="Virtual" VerticalScrollableHeight="430"
            ShowHeaderFilterButton="true" ShowFooter="true" ShowGroupFooter="Hidden" ShowGroupPanel="false" ShowGroupedColumns="true" ShowFilterBar="Visible"
            ShowFilterRow="true" />
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
        <Styles>
            <GroupPanel Wrap="True" HorizontalAlign="Left">
            </GroupPanel>
            <GroupRow Wrap="True" HorizontalAlign="Left">
            </GroupRow>
        </Styles>
    </dx:ASPxGridView>
    <dx:ASPxGridViewExporter ID="geDecreeDebtorsExport" runat="server" GridViewID="gridDecreeDebtors">
    </dx:ASPxGridViewExporter>
    <asp:SqlDataSource runat="server" ID="MySqldsDecreeDebtors" 
        ProviderName="MySql.Data.MySqlClient" SelectCommand=" " />
                
                                          </div>
                                          
                                        
                                </li>
                                <li id="li_Loans" class="top_H"><a href="#" class="top_Ha">08. Loans<span class="st-arrow">Open
                                    or Close</span></a>
<div class="st-content">
                                       
                                       <div style="background:#fff !important;" class="row display">
						
                                          <div style="background:#fff !important;" class="four columns">
                                          <p class="inner_heading">Previous Day Balance On Loans</p>
                                                                                    <ul class="overview_list">
								<li>
									<a href="#">
										<img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)" alt="">
										<asp:Label runat="server" ID="lblCrLoansAmountprDay" Text="0.00" class="ov_nb"></asp:Label>
										<span class="ov_text">Cr Amount</span>
									</a>
								</li>
								<li>
									<a href="#">
										<img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)" alt="">
										<asp:Label runat="server" ID="lblDrLoansAmountprDay" Text="0.00" class="ov_nb"></asp:Label>
										<span class="ov_text">Dr Amount</span>
									</a>
								</li>
								<li>
									<a href="#">
										<img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)" alt="">
										<asp:Label runat="server" ID="lblCrLoansBalanceprDay" Text="0.00" class="ov_nb"></asp:Label>
										<span class="ov_text">Cr Balance</span>
									</a>
								</li>
								<li>
									<a href="#">
										<img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)" alt="">
										<asp:Label runat="server" ID="lblDrLoansBalanceprDay" Text="0.00" class="ov_nb"></asp:Label>
										
										<span class="ov_text">Dr Balance </span>
									</a>
								</li>
							</ul>
                                          
                                          </div> 
                                          <div style="background:#fff !important;" class="four columns">
                                           <p class="inner_heading">Today Balance On Loans</p>
                                                                                     <ul class="overview_list">
								<li>
									<a href="#">
										<img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)" alt="">
										<asp:Label runat="server" ID="lblCrLoansAmountFinal" Text="0.00" class="ov_nb"></asp:Label>
										<span class="ov_text">Cr Amountt</span>
									</a>
								</li>
								<li>
									<a href="#">
										<img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)" alt="">
										<asp:Label runat="server" ID="lblDrLoansAmountFinal" Text="0.00" class="ov_nb"></asp:Label>
										<span class="ov_text">Dr Amount</span>
									</a>
								</li>
								<li>
									<a href="#">
										<img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)" alt="">
										<asp:Label runat="server" ID="lblCrLoansBalanceFinal" Text="0.00" class="ov_nb"></asp:Label>
										<span class="ov_text">Cr Balance</span>
									</a>
								</li>
								<li>
									<a href="#">
										<img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)" alt="">
										<asp:Label runat="server" ID="lblDrLoansBalanceFinal" Text="0.00" class="ov_nb"></asp:Label>
										
										<span class="ov_text">Dr Balance</span>
									</a>
								</li>
							</ul>
                                           
                                          </div>
                                         
                                          <div style="background:#fff !important;" class="four columns">
                                           <p class="inner_heading">Today Transaction On Loans</p>
                                                                                     <ul class="overview_list">
								<li>
									<a href="#">
										<img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)" alt="">
										<asp:Label runat="server" ID="lblCrLoansAmount" Text="0.00" class="ov_nb"></asp:Label>
										<span class="ov_text">Cr Amountt</span>
									</a>
								</li>
								<li>
									<a href="#">
										<img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)" alt="">
										<asp:Label runat="server" ID="lblDrLoansAmount" Text="0.00" class="ov_nb"></asp:Label>
										<span class="ov_text">Dr Amount</span>
									</a>
								</li>
								<li>
									<a href="#">
										<img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)" alt="">
										<asp:Label runat="server" ID="lblCrLoansBalance" Text="0.00" class="ov_nb"></asp:Label>
										<span class="ov_text">Cr Balance</span>
									</a>
								</li>
								<li>
									<a href="#">
										<img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)" alt="">
										<asp:Label runat="server" ID="lblDrLoansBalance" Text="0.00" class="ov_nb"></asp:Label>
										
										<span class="ov_text">Dr Balance</span>
									</a>
								</li>
							</ul>
                                          </div>
                                          </div>
                                           <dx:ASPxGridView Styles-AlternatingRow-BackColor="LightGray" Style="margin: 0 auto; width: 100%;" OnSummaryDisplayText="ASPxGridView1_SummaryDisplayText" OnCustomSummaryCalculate="ASPxGridView1_OnCustomSummaryCalculate"
        ID="gridLoans" ClientInstanceName="gridLoans" runat="server" DataSourceID="MySqldsLoans"
        OnCustomCallback="grid_CustomCallback"> 
        <Settings  ShowVerticalScrollBar="true" VerticalScrollBarStyle="Virtual" VerticalScrollableHeight="430"
            ShowHeaderFilterButton="true" ShowFooter="true" ShowGroupFooter="Hidden" ShowGroupPanel="false" ShowGroupedColumns="true" ShowFilterBar="Visible"
            ShowFilterRow="true" />
        <Templates>
        <GroupRowContent><dx:ASPxLabel ID="GroupLabel" runat="server" Text='<%# GetLabelText(Container)%>' ></dx:ASPxLabel>
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
        <Styles>
            <GroupPanel Wrap="True" HorizontalAlign="Left">
            </GroupPanel>
            <GroupRow Wrap="True" HorizontalAlign="Left">
            </GroupRow>
        </Styles>
    </dx:ASPxGridView>
    <dx:ASPxGridViewExporter ID="geLoansExport" runat="server" GridViewID="gridLoans">
    </dx:ASPxGridViewExporter>
    <asp:SqlDataSource runat="server" ID="MySqldsLoans" 
        ProviderName="MySql.Data.MySqlClient" SelectCommand=" " />
                
                                          </div>
                                          
                                       
                                </li>
                                <li id="li_Advances" class="top_H"><a href="#" class="top_Ha">09. Advances<span class="st-arrow">Open
                                    or Close</span></a>
<div class="st-content">
                                       
                                       <div style="background:#fff !important;" class="row display">
						
                                          <div style="background:#fff !important;" class="four columns">
                                          <p class="inner_heading">Previous Day Balance On SundriesAndAdvances</p>
                                                                                    <ul class="overview_list">
								<li>
									<a href="#">
										<img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)" alt="">
										<asp:Label runat="server" ID="lblCrSundriesAndAdvancesAmountprDay" Text="0.00" class="ov_nb"></asp:Label>
										<span class="ov_text">Cr Amount</span>
									</a>
								</li>
								<li>
									<a href="#">
										<img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)" alt="">
										<asp:Label runat="server" ID="lblDrSundriesAndAdvancesAmountprDay" Text="0.00" class="ov_nb"></asp:Label>
										<span class="ov_text">Dr Amount</span>
									</a>
								</li>
								<li>
									<a href="#">
										<img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)" alt="">
										<asp:Label runat="server" ID="lblCrSundriesAndAdvancesBalanceprDay" Text="0.00" class="ov_nb"></asp:Label>
										<span class="ov_text">Cr Balance</span>
									</a>
								</li>
								<li>
									<a href="#">
										<img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)" alt="">
										<asp:Label runat="server" ID="lblDrSundriesAndAdvancesBalanceprDay" Text="0.00" class="ov_nb"></asp:Label>
										
										<span class="ov_text">Dr Balance </span>
									</a>
								</li>
							</ul>
                                          
                                          </div> 
                                          <div style="background:#fff !important;" class="four columns">
                                           <p class="inner_heading">Today Balance On SundriesAndAdvances</p>
                                                                                     <ul class="overview_list">
								<li>
									<a href="#">
										<img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)" alt="">
										<asp:Label runat="server" ID="lblCrSundriesAndAdvancesAmountFinal" Text="0.00" class="ov_nb"></asp:Label>
										<span class="ov_text">Cr Amountt</span>
									</a>
								</li>
								<li>
									<a href="#">
										<img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)" alt="">
										<asp:Label runat="server" ID="lblDrSundriesAndAdvancesAmountFinal" Text="0.00" class="ov_nb"></asp:Label>
										<span class="ov_text">Dr Amount</span>
									</a>
								</li>
								<li>
									<a href="#">
										<img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)" alt="">
										<asp:Label runat="server" ID="lblCrSundriesAndAdvancesBalanceFinal" Text="0.00" class="ov_nb"></asp:Label>
										<span class="ov_text">Cr Balance</span>
									</a>
								</li>
								<li>
									<a href="#">
										<img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)" alt="">
										<asp:Label runat="server" ID="lblDrSundriesAndAdvancesBalanceFinal" Text="0.00" class="ov_nb"></asp:Label>
										
										<span class="ov_text">Dr Balance</span>
									</a>
								</li>
							</ul>
                                           
                                          </div>
                                         
                                          <div style="background:#fff !important;" class="four columns">
                                           <p class="inner_heading">Today Transaction On SundriesAndAdvances</p>
                                                                                     <ul class="overview_list">
								<li>
									<a href="#">
										<img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)" alt="">
										<asp:Label runat="server" ID="lblCrSundriesAndAdvancesAmount" Text="0.00" class="ov_nb"></asp:Label>
										<span class="ov_text">Cr Amountt</span>
									</a>
								</li>
								<li>
									<a href="#">
										<img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)" alt="">
										<asp:Label runat="server" ID="lblDrSundriesAndAdvancesAmount" Text="0.00" class="ov_nb"></asp:Label>
										<span class="ov_text">Dr Amount</span>
									</a>
								</li>
								<li>
									<a href="#">
										<img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)" alt="">
										<asp:Label runat="server" ID="lblCrSundriesAndAdvancesBalance" Text="0.00" class="ov_nb"></asp:Label>
										<span class="ov_text">Cr Balance</span>
									</a>
								</li>
								<li>
									<a href="#">
										<img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)" alt="">
										<asp:Label runat="server" ID="lblDrSundriesAndAdvancesBalance" Text="0.00" class="ov_nb"></asp:Label>
										
										<span class="ov_text">Dr Balance</span>
									</a>
								</li>
							</ul>
                                          </div>
                                          
                                          </div>
                                           <dx:ASPxGridView Styles-AlternatingRow-BackColor="LightGray" Style="margin: 0 auto; width: 100%;" OnSummaryDisplayText="ASPxGridView1_SummaryDisplayText" OnCustomSummaryCalculate="ASPxGridView1_OnCustomSummaryCalculate"
        ID="gridSundriesAndAdvances" ClientInstanceName="gridSundriesAndAdvances" runat="server" DataSourceID="MySqldsSundriesAndAdvances"
        OnCustomCallback="grid_CustomCallback"> 
        <Settings  ShowVerticalScrollBar="true" VerticalScrollBarStyle="Virtual" VerticalScrollableHeight="430"
            ShowHeaderFilterButton="true" ShowFooter="true" ShowGroupFooter="Hidden" ShowGroupPanel="false" ShowGroupedColumns="true" ShowFilterBar="Visible"
            ShowFilterRow="true" />
        <Templates>
        <GroupRowContent><dx:ASPxLabel ID="GroupLabel" runat="server" Text='<%# GetLabelText(Container)%>' ></dx:ASPxLabel>
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
        <Styles>
            <GroupPanel Wrap="True" HorizontalAlign="Left">
            </GroupPanel>
            <GroupRow Wrap="True" HorizontalAlign="Left">
            </GroupRow>
        </Styles>
    </dx:ASPxGridView>
    <dx:ASPxGridViewExporter ID="geSundriesAndAdvancesExport" runat="server" GridViewID="gridSundriesAndAdvances">
    </dx:ASPxGridViewExporter>
    <asp:SqlDataSource runat="server" ID="MySqldsSundriesAndAdvances" 
        ProviderName="MySql.Data.MySqlClient" SelectCommand=" " />
                
                                                                                    
                                        </div>
                                </li>
                                <li id="li_Stamps" class="top_H"><a href="#" class="top_Ha">10. Stamps<span class="st-arrow">Open
                                    or Close</span></a>
<div class="st-content">
                                       
                                       <div style="background:#fff !important;" class="row display">
						
                                          <div style="background:#fff !important;" class="four columns">
                                          <p class="inner_heading">Previous Day Balance On Stamps</p>
                                                                                    <ul class="overview_list">
								<li>
									<a href="#">
										<img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)" alt="">
										<asp:Label runat="server" ID="lblCrStampsAmountprDay" Text="0.00" class="ov_nb"></asp:Label>
										<span class="ov_text">Cr Amount</span>
									</a>
								</li>
								<li>
									<a href="#">
										<img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)" alt="">
										<asp:Label runat="server" ID="lblDrStampsAmountprDay" Text="0.00" class="ov_nb"></asp:Label>
										<span class="ov_text">Dr Amount</span>
									</a>
								</li>
								<li>
									<a href="#">
										<img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)" alt="">
										<asp:Label runat="server" ID="lblCrStampsBalanceprDay" Text="0.00" class="ov_nb"></asp:Label>
										<span class="ov_text">Cr Balance</span>
									</a>
								</li>
								<li>
									<a href="#">
										<img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)" alt="">
										<asp:Label runat="server" ID="lblDrStampsBalanceprDay" Text="0.00" class="ov_nb"></asp:Label>
										
										<span class="ov_text">Dr Balance </span>
									</a>
								</li>
							</ul>
                                          
                                          </div> 
                                          <div style="background:#fff !important;" class="four columns">
                                           <p class="inner_heading">Today Balance On Stamps</p>
                                                                                     <ul class="overview_list">
								<li>
									<a href="#">
										<img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)" alt="">
										<asp:Label runat="server" ID="lblCrStampsAmountFinal" Text="0.00" class="ov_nb"></asp:Label>
										<span class="ov_text">Cr Amountt</span>
									</a>
								</li>
								<li>
									<a href="#">
										<img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)" alt="">
										<asp:Label runat="server" ID="lblDrStampsAmountFinal" Text="0.00" class="ov_nb"></asp:Label>
										<span class="ov_text">Dr Amount</span>
									</a>
								</li>
								<li>
									<a href="#">
										<img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)" alt="">
										<asp:Label runat="server" ID="lblCrStampsBalanceFinal" Text="0.00" class="ov_nb"></asp:Label>
										<span class="ov_text">Cr Balance</span>
									</a>
								</li>
								<li>
									<a href="#">
										<img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)" alt="">
										<asp:Label runat="server" ID="lblDrStampsBalanceFinal" Text="0.00" class="ov_nb"></asp:Label>
										
										<span class="ov_text">Dr Balance</span>
									</a>
								</li>
							</ul>
                                           
                                          </div>
                                       
                                          <div style="background:#fff !important;" class="four columns">
                                           <p class="inner_heading">Today Transaction On Stamps</p>
                                                                                     <ul class="overview_list">
								<li>
									<a href="#">
										<img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)" alt="">
										<asp:Label runat="server" ID="lblCrStampsAmount" Text="0.00" class="ov_nb"></asp:Label>
										<span class="ov_text">Cr Amountt</span>
									</a>
								</li>
								<li>
									<a href="#">
										<img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)" alt="">
										<asp:Label runat="server" ID="lblDrStampsAmount" Text="0.00" class="ov_nb"></asp:Label>
										<span class="ov_text">Dr Amount</span>
									</a>
								</li>
								<li>
									<a href="#">
										<img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)" alt="">
										<asp:Label runat="server" ID="lblCrStampsBalance" Text="0.00" class="ov_nb"></asp:Label>
										<span class="ov_text">Cr Balance</span>
									</a>
								</li>
								<li>
									<a href="#">
										<img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)" alt="">
										<asp:Label runat="server" ID="lblDrStampsBalance" Text="0.00" class="ov_nb"></asp:Label>
										
										<span class="ov_text">Dr Balance</span>
									</a>
								</li>
							</ul>
                                          </div>
                                          
                                          </div>
                                           <dx:ASPxGridView Styles-AlternatingRow-BackColor="LightGray" Style="margin: 0 auto; width: 100%;" OnSummaryDisplayText="ASPxGridView1_SummaryDisplayText" 
        ID="gridStamps" ClientInstanceName="gridStamps" runat="server" DataSourceID="MySqldsStamps"
        OnCustomCallback="grid_CustomCallback"> 
        <Settings  ShowVerticalScrollBar="true" VerticalScrollBarStyle="Virtual" VerticalScrollableHeight="430"
            ShowHeaderFilterButton="true" ShowFooter="true" ShowGroupFooter="Hidden" ShowGroupPanel="false" ShowGroupedColumns="true" ShowFilterBar="Visible"
            ShowFilterRow="true" />
        <Templates>
        <GroupRowContent><dx:ASPxLabel ID="GroupLabel" runat="server" Text='<%# GetLabelText(Container)%>' ></dx:ASPxLabel>
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
        <Styles>
            <GroupPanel Wrap="True" HorizontalAlign="Left">
            </GroupPanel>
            <GroupRow Wrap="True" HorizontalAlign="Left">
            </GroupRow>
        </Styles>
    </dx:ASPxGridView>
    <dx:ASPxGridViewExporter ID="geStampsExport" runat="server" GridViewID="gridStamps">
    </dx:ASPxGridViewExporter>
    <asp:SqlDataSource runat="server" ID="MySqldsStamps" 
        ProviderName="MySql.Data.MySqlClient" SelectCommand=" " />
                
                                          </div>
                                          
                                        </li>
                                <li id="li_PandL" class="top_H"><a href="#" class="top_Ha">11. Profit And Loss Account<span
                                    class="st-arrow">Open or Close</span></a>
<div class="st-content">
                                       
                                       <div style="background:#fff !important;" class="row display">
						
                                          <div style="background:#fff !important;" class="four columns">
                                          <p class="inner_heading">Previous Day Balance On ProfitAndLoss</p>
                                                                                    <ul class="overview_list">
								<li>
									<a href="#">
										<img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)" alt="">
										<asp:Label runat="server" ID="lblCrProfitAndLossAmountprDay" Text="0.00" class="ov_nb"></asp:Label>
										<span class="ov_text">Cr Amount</span>
									</a>
								</li>
								<li>
									<a href="#">
										<img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)" alt="">
										<asp:Label runat="server" ID="lblDrProfitAndLossAmountprDay" Text="0.00" class="ov_nb"></asp:Label>
										<span class="ov_text">Dr Amount</span>
									</a>
								</li>
								<li>
									<a href="#">
										<img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)" alt="">
										<asp:Label runat="server" ID="lblCrProfitAndLossBalanceprDay" Text="0.00" class="ov_nb"></asp:Label>
										<span class="ov_text">Cr Balance</span>
									</a>
								</li>
								<li>
									<a href="#">
										<img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)" alt="">
										<asp:Label runat="server" ID="lblDrProfitAndLossBalanceprDay" Text="0.00" class="ov_nb"></asp:Label>
										
										<span class="ov_text">Dr Balance </span>
									</a>
								</li>
							</ul>
                                          
                                          </div> 
                                          <div style="background:#fff !important;" class="four columns">
                                           <p class="inner_heading">Today Balance On ProfitAndLoss</p>
                                                                                     <ul class="overview_list">
								<li>
									<a href="#">
										<img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)" alt="">
										<asp:Label runat="server" ID="lblCrProfitAndLossAmountFinal" Text="0.00" class="ov_nb"></asp:Label>
										<span class="ov_text">Cr Amountt</span>
									</a>
								</li>
								<li>
									<a href="#">
										<img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)" alt="">
										<asp:Label runat="server" ID="lblDrProfitAndLossAmountFinal" Text="0.00" class="ov_nb"></asp:Label>
										<span class="ov_text">Dr Amount</span>
									</a>
								</li>
								<li>
									<a href="#">
										<img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)" alt="">
										<asp:Label runat="server" ID="lblCrProfitAndLossBalanceFinal" Text="0.00" class="ov_nb"></asp:Label>
										<span class="ov_text">Cr Balance</span>
									</a>
								</li>
								<li>
									<a href="#">
										<img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)" alt="">
										<asp:Label runat="server" ID="lblDrProfitAndLossBalanceFinal" Text="0.00" class="ov_nb"></asp:Label>
										
										<span class="ov_text">Dr Balance</span>
									</a>
								</li>
							</ul>
                                           
                                          </div>
                                         
                                          <div style="background:#fff !important;" class="four columns">
                                           <p class="inner_heading">Today Transaction On ProfitAndLoss</p>
                                                                                     <ul class="overview_list">
								<li>
									<a href="#">
										<img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)" alt="">
										<asp:Label runat="server" ID="lblCrProfitAndLossAmount" Text="0.00" class="ov_nb"></asp:Label>
										<span class="ov_text">Cr Amountt</span>
									</a>
								</li>
								<li>
									<a href="#">
										<img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)" alt="">
										<asp:Label runat="server" ID="lblDrProfitAndLossAmount" Text="0.00" class="ov_nb"></asp:Label>
										<span class="ov_text">Dr Amount</span>
									</a>
								</li>
								<li>
									<a href="#">
										<img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)" alt="">
										<asp:Label runat="server" ID="lblCrProfitAndLossBalance" Text="0.00" class="ov_nb"></asp:Label>
										<span class="ov_text">Cr Balance</span>
									</a>
								</li>
								<li>
									<a href="#">
										<img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)" alt="">
										<asp:Label runat="server" ID="lblDrProfitAndLossBalance" Text="0.00" class="ov_nb"></asp:Label>
										
										<span class="ov_text">Dr Balance</span>
									</a>
								</li>
							</ul>
                                          </div>
                                          </div>
                                           <dx:ASPxGridView Styles-AlternatingRow-BackColor="LightGray" Style="margin: 0 auto; width: 100%;" OnSummaryDisplayText="ASPxGridView1_SummaryDisplayText" OnCustomSummaryCalculate="ASPxGridView1_OnCustomSummaryCalculate"
        ID="gridProfitAndLoss" ClientInstanceName="gridProfitAndLoss" runat="server" DataSourceID="MySqldsProfitAndLoss"
        OnCustomCallback="grid_CustomCallback"> 
        <Settings  ShowVerticalScrollBar="true" VerticalScrollBarStyle="Virtual" VerticalScrollableHeight="430"
            ShowHeaderFilterButton="true" ShowFooter="true" ShowGroupFooter="Hidden" ShowGroupPanel="false" ShowGroupedColumns="true" ShowFilterBar="Visible"
            ShowFilterRow="true" />
        <TotalSummary>
            <dx:ASPxSummaryItem FieldName="Credit" SummaryType="Sum" />
            <dx:ASPxSummaryItem FieldName="Debit" SummaryType="Sum" />
            <dx:ASPxSummaryItem FieldName="Narration" SummaryType="Custom" />
        </TotalSummary>
        <SettingsPager Mode="ShowAllRecords">
        </SettingsPager>
        <Templates>
        <GroupRowContent><dx:ASPxLabel ID="GroupLabel" runat="server" Text='<%# GetLabelText(Container)%>' ></dx:ASPxLabel>
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

        <Styles>
            <GroupPanel Wrap="True" HorizontalAlign="Left">
            </GroupPanel>
            <GroupRow Wrap="True" HorizontalAlign="Left">
            </GroupRow>
        </Styles>
    </dx:ASPxGridView>
    <dx:ASPxGridViewExporter ID="geProfitAndLossExport" runat="server" GridViewID="gridProfitAndLoss">
    </dx:ASPxGridViewExporter>
    <asp:SqlDataSource runat="server" ID="MySqldsProfitAndLoss" 
        ProviderName="MySql.Data.MySqlClient" SelectCommand=" " />
                
                                                                                   
                                        </div>
                                </li>
                                <li id="li1" class="top_H"><a href="#" class="top_Ha">12. Cash On Hand<span class="st-arrow">Open
                                    or Close</span></a>
<div class="st-content">
                                       
                                       <div style="background:#fff !important;" class="row display">
						
                                          <div style="background:#fff !important;" class="four columns">
                                          <p class="inner_heading">Previous Day Balance On Cash</p>
                                                                                    <ul class="overview_list">
								<li>
									<a href="#">
										<img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)" alt="">
										<asp:Label runat="server" ID="lblCrCashAmountprDay" Text="0.00" class="ov_nb"></asp:Label>
										<span class="ov_text">Cr Amount</span>
									</a>
								</li>
								<li>
									<a href="#">
										<img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)" alt="">
										<asp:Label runat="server" ID="lblDrCashAmountprDay" Text="0.00" class="ov_nb"></asp:Label>
										<span class="ov_text">Dr Amount</span>
									</a>
								</li>
								<li>
									<a href="#">
										<img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)" alt="">
										<asp:Label runat="server" ID="lblCrCashBalanceprDay" Text="0.00" class="ov_nb"></asp:Label>
										<span class="ov_text">Cr Balance</span>
									</a>
								</li>
								<li>
									<a href="#">
										<img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)" alt="">
										<asp:Label runat="server" ID="lblDrCashBalanceprDay" Text="0.00" class="ov_nb"></asp:Label>
										
										<span class="ov_text">Dr Balance </span>
									</a>
								</li>
							</ul>
                                          
                                          </div> 
                                          <div style="background:#fff !important;" class="four columns">
                                           <p class="inner_heading">Today Balance On Cash</p>
                                                                                     <ul class="overview_list">
								<li>
									<a href="#">
										<img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)" alt="">
										<asp:Label runat="server" ID="lblCrCashAmountFinal" Text="0.00" class="ov_nb"></asp:Label>
										<span class="ov_text">Cr Amountt</span>
									</a>
								</li>
								<li>
									<a href="#">
										<img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)" alt="">
										<asp:Label runat="server" ID="lblDrCashAmountFinal" Text="0.00" class="ov_nb"></asp:Label>
										<span class="ov_text">Dr Amount</span>
									</a>
								</li>
								<li>
									<a href="#">
										<img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)" alt="">
										<asp:Label runat="server" ID="lblCrCashBalanceFinal" Text="0.00" class="ov_nb"></asp:Label>
										<span class="ov_text">Cr Balance</span>
									</a>
								</li>
								<li>
									<a href="#">
										<img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)" alt="">
										<asp:Label runat="server" ID="lblDrCashBalanceFinal" Text="0.00" class="ov_nb"></asp:Label>
										
										<span class="ov_text">Dr Balance</span>
									</a>
								</li>
							</ul>
                                           
                                          </div>
                                     
                                          <div style="background:#fff !important;" class="four columns">
                                           <p class="inner_heading">Today Transaction On Cash</p>
                                                                                     <ul class="overview_list">
								<li>
									<a href="#">
										<img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)" alt="">
										<asp:Label runat="server" ID="lblCrCashAmount" Text="0.00" class="ov_nb"></asp:Label>
										<span class="ov_text">Cr Amountt</span>
									</a>
								</li>
								<li>
									<a href="#">
										<img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)" alt="">
										<asp:Label runat="server" ID="lblDrCashAmount" Text="0.00" class="ov_nb"></asp:Label>
										<span class="ov_text">Dr Amount</span>
									</a>
								</li>
								<li>
									<a href="#">
										<img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)" alt="">
										<asp:Label runat="server" ID="lblCrCashBalance" Text="0.00" class="ov_nb"></asp:Label>
										<span class="ov_text">Cr Balance</span>
									</a>
								</li>
								<li>
									<a href="#">
										<img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)" alt="">
										<asp:Label runat="server" ID="lblDrCashBalance" Text="0.00" class="ov_nb"></asp:Label>
										
										<span class="ov_text">Dr Balance</span>
									</a>
								</li>
							</ul>
                                          </div>
                                          </div>
                                           <dx:ASPxGridView Styles-AlternatingRow-BackColor="LightGray" Style="margin: 0 auto; width: 100%;" OnSummaryDisplayText="ASPxGridView1_SummaryDisplayText" OnCustomSummaryCalculate="ASPxGridView1_OnCustomSummaryCalculate"
        ID="gridCash" ClientInstanceName="gridCash" runat="server" DataSourceID="MySqldsCash"
        OnCustomCallback="grid_CustomCallback"> 
        <Settings  ShowVerticalScrollBar="true" VerticalScrollBarStyle="Virtual" VerticalScrollableHeight="430"
            ShowHeaderFilterButton="true" ShowFooter="true" ShowGroupFooter="Hidden" ShowGroupPanel="false" ShowGroupedColumns="true" ShowFilterBar="Visible"
            ShowFilterRow="true" />
        <Templates>
        <GroupRowContent><dx:ASPxLabel ID="GroupLabel" runat="server" Text='<%# GetLabelText(Container)%>' ></dx:ASPxLabel>
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
        <Styles>
            <GroupPanel Wrap="True" HorizontalAlign="Left">
            </GroupPanel>
            <GroupRow Wrap="True" HorizontalAlign="Left">
            </GroupRow>
        </Styles>
    </dx:ASPxGridView>
    <dx:ASPxGridViewExporter ID="geCashExport" runat="server" GridViewID="gridCash">
    </dx:ASPxGridViewExporter>
    <asp:SqlDataSource runat="server" ID="MySqldsCash" 
        ProviderName="MySql.Data.MySqlClient" SelectCommand=" " />
                
                                         
                                          
                                        </div>
                                </li>
                            </ul>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <script>
			$(document).ready(function() {
				//* common functions
				
				prth_documentation.navigator();
				prth_documentation.init();
				SyntaxHighlighter.defaults['toolbar'] = false;
				SyntaxHighlighter.all();
			});
    </script>

</asp:Content>
