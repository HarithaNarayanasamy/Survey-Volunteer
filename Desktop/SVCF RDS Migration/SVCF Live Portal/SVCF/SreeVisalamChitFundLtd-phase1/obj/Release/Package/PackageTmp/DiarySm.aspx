<%@ Page Title="SVCF Diary" EnableViewState="false" Culture="en-GB" Language="C#"
    MasterPageFile="~/Branch.Master" AutoEventWireup="true" CodeBehind="DiarySm.aspx.cs"
    Inherits="SreeVisalamChitFundLtd_phase1.DiarySm" %>

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
        td[style="cursor:default;"]
        {
            vertical-align: middle;
        }
        #ctl00_cphMainContent_gridCash_DXHeadersRow0
        {
            display: none;
        }
        .dxgvHeader
        {
            cursor: default;
        }
        .dxgvEmptyDataRow
        {
            display: none;
        }
        
        .dxGridView_gvExpandedButton
        {
            visibility: hidden;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMainContent" runat="server">
    <div class="row" id="sortable_panels">
        <div class="twelve columns">
            <div class="box_c">
                <div class="box_c_heading cf box_actions noprint">
                    <p>
                        SVCF Diary</p>
                </div>
                <div class="box_c_content">
                    <div class="row">
                        <div class="twelve columns">
                            <div class="noprint" style="margin-top: -0.5em; margin-bottom: 0.6em;">
                                <asp:Panel ID="Panel2" runat="server" DefaultButton="BtnStatisticsGo">
                                    <div style="display: table-cell; vertical-align: top; padding-top: 10px; padding-right: 5px !important;">
                                        <asp:Label ID="Label4" runat="server" Text="Date :"></asp:Label>
                                    </div>
                                    <div style="display: table-cell; vertical-align: top; padding-top: 5px; padding-right: 5px !important;">
                                        <asp:TextBox TabIndex="2" placeholder="To Date" CssClass="input-text maskdate" Width="100px"
                                            ID="txtToDate" runat="server">
                                        </asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" Display="Dynamic"
                                            ValidationGroup="directbranch1" ControlToValidate="txtToDate"></asp:RequiredFieldValidator>
                                        <asp:CompareValidator ValidationGroup="directbranch1" ID="CompareValidator5" ControlToValidate="txtToDate"
                                            Display="Dynamic" runat="server" Operator="DataTypeCheck" Type="Date"></asp:CompareValidator>
                                    </div>
                                    <div style="display: table-cell; vertical-align: top; padding-top: 5px; padding-right: 5px !important;">
                                        <asp:Button TabIndex="3" ValidationGroup="directbranch1" CssClass="GreenyPushButton"
                                            ID="BtnStatisticsGo" runat="server" class="btn" OnClick="BtnStatisticsGo_Click"
                                            Text="Load"></asp:Button>
                                    </div>
                                    <div style="display: table-cell; vertical-align: top; float: right; padding-right: 5px;
                                        text-align: right; margin-top: -35px;">
                                        <a class="noprint" style="cursor: hand; cursor: pointer;" id="btnExport">
                                            <img  alt="Print" class="noprint" src="Styles/Img/pf-button-both.gif" style="border: none;
                                                cursor: hand; cursor: pointer;" />
                                        </a>
                                        <div>
                                            <%--<dx:ASPxMenu ID="ASPxMenu1" runat="server" OnItemClick="Export_click" AllowSelectItem="True"
                                                ShowPopOutImages="True">
                                                <Items>
                                                    <dx:MenuItem Text="Export">
                                                        <Items>
                                                            <dx:MenuItem Text="PDF">
                                                            </dx:MenuItem>
                                                           
                                                        </Items>
                                                    </dx:MenuItem>
                                                </Items>
                                            </dx:ASPxMenu>--%>
                                        </div>
                                    </div>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                </div>
                <br />
                <div id="printdiv" class="printable">
                    <div class="row">
                        <div class="twelve columns sortable ui-sortable dragsortable">
                            <div class="box_c">
                                <div class="box_c_content">
                                    <div class="row">
                                        <div class="twelve columns">
                                            <div class="aaaaaa">
                                                <div class="three columns">
                                                    <asp:Label ID="Label5" runat="server" Text="Form No. S-26"></asp:Label>
                                                    <div style="padding-left: 20px;">
                                                        <asp:Image runat="server" ID="imgVisalam" Height="70" Width="70" ImageUrl='<%# Page.ResolveUrl("~/Styles/Image/logo_New.png")%>'
                                                            AlternateText="SVCF Admin" />
                                                    </div>
                                                </div>
                                                <div class="nine columns">
                                                    <table style="width: 100%;">
                                                        <tr>
                                                            <td>
                                                                <table style="width: 70%;">
                                                                    <tr align="center">
                                                                        <td colspan="2">
                                                                            <asp:Label runat="server" Text="SREE VISALAM CHIT FUND LIMITED"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr align="center">
                                                                        <td align="center">
                                                                            <asp:Label ID="Label2" runat="server" Text="Regd. Office : TIRUNELVELI - 6."></asp:Label>
                                                                        </td>
                                                                        <td align="center">
                                                                            <asp:Label ID="Label3" runat="server" Text="Administrative Office : PALLATTUR"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr align="center">
                                                                        <td colspan="2">
                                                                            <asp:Label runat="server" ID="lblBranch"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                            <td>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </div>
                                            <table width="100%">
                                                <tr>
                                                    <td colspan="2">
                                                        <asp:Label ID="Label1" runat="server" Text="1. Staff Present No: "></asp:Label><asp:TextBox ID="txtStaff" width="80px" runat="server"></asp:TextBox><asp:Label ID="lblStafflve" Text="On Leave: " runat="server"></asp:Label><asp:TextBox ID="txtStaff1" width="80px" runat="server"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2">
                                                        <asp:Label ID="lbl2" runat="server" Text=""></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lbl3" runat="server" Text="3. Bank Balance Total Rs. ______________"></asp:Label>
                                                    </td>
                                                    <td style="float: right;">
                                                        <asp:Label ID="lblDate" runat="server" Text="Date : ______________"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="six columns sortable ui-sortable dragsortable">
                            <div class="box_c">
                                <div class="box_c_heading cf box_actions noprint">
                                    <p>
                                        Bank</p>
                                </div>
                                <div class="box_c_content">
                                    <dx:ASPxGridView Style="margin: 0 auto; width: 100%;" OnSummaryDisplayText="gridBankCollection_SummaryDisplayText"
                                        ID="gridBankCollection" ClientInstanceName="gridBankCollection" runat="server"
                                        DataSourceID="AccessDataSource3">
                                        <Settings ShowTitlePanel="true" ShowFooter="true" />
                                        <TotalSummary>
                                            <dx:ASPxSummaryItem DisplayFormat="{0}" FieldName="CrAmount" SummaryType="Sum" />
                                            <dx:ASPxSummaryItem DisplayFormat="{0}" FieldName="DrAmount" SummaryType="Sum" />
                                            <dx:ASPxSummaryItem FieldName="Bank" SummaryType="Custom" />
                                        </TotalSummary>
                                        <SettingsText Title="Bank Ledger" />
                                        <SettingsPager Mode="ShowAllRecords">
                                        </SettingsPager>
                                        <Columns>
                                            <dx:GridViewDataColumn FieldName="Bank" Caption="Name of the Bank" FooterCellStyle-HorizontalAlign="Right">
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn FieldName="CrAmount" Caption="Credit" CellStyle-HorizontalAlign="Right" FooterCellStyle-HorizontalAlign="Right">
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn FieldName="DrAmount" Caption="Debit" CellStyle-HorizontalAlign="Right" FooterCellStyle-HorizontalAlign="Right">
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
                            </div>
                        </div>
                        <div class="six columns sortable ui-sortable dragsortable">
                            <div class="box_c">
                                <div class="box_c_heading cf box_actions noprint">
                                    <p>
                                        Branch</p>
                                </div>
                                <div class="box_c_content">
                                    <dx:ASPxGridView Style="margin: 0 auto; width: 100%;" ID="gridBranchCollection" ClientInstanceName="gridBranchCollection"
                                        runat="server" >
                                        <Settings ShowTitlePanel="true" />
                                        <SettingsText Title="Bank Ledger" />
                                        <SettingsPager Mode="ShowAllRecords">
                                        </SettingsPager>
                                        <Columns>
                                            <dx:GridViewDataColumn FieldName="Branch" Caption="Name of the Branch" FooterCellStyle-HorizontalAlign="Right">
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn FieldName="Amount" CellStyle-HorizontalAlign="Right" FooterCellStyle-HorizontalAlign="Right">
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
                            </div>
                        </div>
                    </div>
                    <div class="row">
                       <%-- <div class="six columns sortable ui-sortable dragsortable">
                            <div class="box_c">
                                <div class="box_c_heading cf box_actions noprint">
                                    <p>
                                        Chit Collection Particulars (Credit Part of 5)</p>
                                </div>
                                <div class="box_c_content">
                                    <dx:ASPxGridView Style="margin: 0 auto; width: 200%;" OnSummaryDisplayText="gridChitCollection_SummaryDisplayText"
                                        OnCustomSummaryCalculate="gridChitCollection_OnCustomSummaryCalculate" ID="gridChitCollection"
                                        ClientInstanceName="gridChitCollection" runat="server" DataSourceID="AccessDataSource2">
                                        <Settings ShowTitlePanel="true" ShowFooter="true" />
                                        <TotalSummary>
                                            <dx:ASPxSummaryItem FieldName="CRR" SummaryType="Sum" />
                                            <dx:ASPxSummaryItem FieldName="TRR" SummaryType="Sum" />
                                            <dx:ASPxSummaryItem FieldName="PR" SummaryType="Sum" />
                                            <dx:ASPxSummaryItem FieldName="Group" SummaryType="Custom" />
                                        </TotalSummary>
                                        <SettingsText Title="Bank Ledger" />
                                        <SettingsPager Mode="ShowAllRecords">
                                        </SettingsPager>
                                        <Columns>
                                           <%-- <dx:GridViewDataColumn FieldName="Group">
                                            </dx:GridViewDataColumn>--%>
                                           <%-- <dx:GridViewDataColumn FieldName="CRR" CellStyle-HorizontalAlign="Right" FooterCellStyle-HorizontalAlign="Right">
                                            </dx:GridViewDataColumn>--%>
                                           <%-- <dx:GridViewDataColumn FieldName="TRR" CellStyle-HorizontalAlign="Right" FooterCellStyle-HorizontalAlign="Right">
                                            </dx:GridViewDataColumn>--%>
                                            <%-- <dx:GridViewDataColumn FieldName="ChitNumber" CellStyle-HorizontalAlign="Right" FooterCellStyle-HorizontalAlign="Right">
                                            </dx:GridViewDataColumn>
                                              <dx:GridViewDataColumn FieldName="TRR1" CellStyle-HorizontalAlign="Right" FooterCellStyle-HorizontalAlign="Right">
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn FieldName="TRR2" CellStyle-HorizontalAlign="Right" FooterCellStyle-HorizontalAlign="Right">
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn FieldName="RCM" CellStyle-HorizontalAlign="Right" FooterCellStyle-HorizontalAlign="Right">
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn FieldName="CCA" CellStyle-HorizontalAlign="Right" FooterCellStyle-HorizontalAlign="Right">
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn FieldName="OPM" CellStyle-HorizontalAlign="Right" FooterCellStyle-HorizontalAlign="Right">
                                            </dx:GridViewDataColumn>
                                             <dx:GridViewDataColumn FieldName="UPM" CellStyle-HorizontalAlign="Right" FooterCellStyle-HorizontalAlign="Right">
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn FieldName="UPMLedger" CellStyle-HorizontalAlign="Right" FooterCellStyle-HorizontalAlign="Right">
                                            </dx:GridViewDataColumn>--%>
                                         <%--   <dx:GridViewDataColumn FieldName="PR" CellStyle-HorizontalAlign="Right" FooterCellStyle-HorizontalAlign="Right">
                                            </dx:GridViewDataColumn>--%>
                                          <%--  <dx:GridViewDataColumn FieldName="PR" CellStyle-HorizontalAlign="Right" FooterCellStyle-HorizontalAlign="Right">
                                            </dx:GridViewDataColumn>--%>
                                       <%-- </Columns>
                                        <Styles Footer-HorizontalAlign="Left" Cell-HorizontalAlign="Left" Header-VerticalAlign="Middle"
                                            Header-HorizontalAlign="Center" Header-Wrap="True">
                                            <GroupPanel Wrap="True" HorizontalAlign="Left">
                                            </GroupPanel>
                                            <GroupRow Wrap="True" HorizontalAlign="Left">
                                            </GroupRow>
                                        </Styles>
                                    </dx:ASPxGridView>
                                </div>
                            </div>
                        </div>--%>
                        <div class="six columns sortable ui-sortable dragsortable">
                            <div class="box_c">
                                <div class="box_c_heading cf box_actions noprint">
                                    <p>
                                        Trial Balance</p>
                                </div>
                                <div class="box_c_content">
                                    <dx:ASPxGridView OnSummaryDisplayText="gridTrialBalance_OnSummaryDisplayText" Style="margin: 0 auto;
                                        width: 100%;" ID="gridTrialBalance" ClientInstanceName="gridTrialBalance" runat="server"
                                        >
                                        <Settings ShowFooter="true" ShowTitlePanel="true" />
                                        <SettingsText Title="Bank Ledger" />
                                        <SettingsPager Mode="ShowAllRecords">
                                        </SettingsPager>
                                        <TotalSummary>
                                            <dx:ASPxSummaryItem FieldName="Credit" SummaryType="Sum" />
                                            <dx:ASPxSummaryItem FieldName="Debit" SummaryType="Sum" />
                                            <dx:ASPxSummaryItem FieldName="Head" SummaryType="Custom" />
                                        </TotalSummary>
                                        <Columns>
                                            <dx:GridViewDataColumn FieldName="Head" FooterCellStyle-HorizontalAlign="Right">
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn FieldName="Credit" CellStyle-HorizontalAlign="Right" FooterCellStyle-HorizontalAlign="Right">
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn FieldName="Debit" CellStyle-HorizontalAlign="Right" FooterCellStyle-HorizontalAlign="Right">
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
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="twelve columns sortable ui-sortable dragsortable">
                        <%-- <div class="six columns sortable ui-sortable dragsortable">--%>
                            <div class="box_c">
                                <div class="box_c_heading cf box_actions noprint">
                                    <p>
                                        Chit Collection Particulars (Credit Part of 5)</p>
                                </div>
                                <div class="box_c_content">
                                    <dx:ASPxGridView Style="margin: 0 auto; width: 100%;" OnSummaryDisplayText="gridChitCollection_SummaryDisplayText"
                                        OnCustomSummaryCalculate="gridChitCollection_OnCustomSummaryCalculate" ID="gridChitCollection"
                                        ClientInstanceName="gridChitCollection" runat="server" DataSourceID="AccessDataSource2">
                                        <Settings ShowTitlePanel="true" ShowFooter="true" />
                                        <TotalSummary>
                                            <%--<dx:ASPxSummaryItem FieldName="CRR" SummaryType="Sum" />
                                            <dx:ASPxSummaryItem FieldName="TRR" SummaryType="Sum" />
                                            <dx:ASPxSummaryItem FieldName="PR" SummaryType="Sum" />
                                            <dx:ASPxSummaryItem FieldName="Group" SummaryType="Custom" />--%>
                                            <dx:ASPxSummaryItem FieldName="PR" SummaryType="Sum" />
                                            <dx:ASPxSummaryItem FieldName="CRR" SummaryType="Sum" />
                                            <dx:ASPxSummaryItem FieldName="TRR1" SummaryType="Sum" />
                                            <dx:ASPxSummaryItem FieldName="TRR2" SummaryType="Sum" />
                                             <dx:ASPxSummaryItem FieldName="RCM" SummaryType="Sum" />
                                             <dx:ASPxSummaryItem FieldName="CCA" SummaryType="Sum" />
                                           <dx:ASPxSummaryItem FieldName="OPM" SummaryType="Sum" />
                                            
                                             <dx:ASPxSummaryItem FieldName="UPM" SummaryType="Sum" />
                                             <dx:ASPxSummaryItem FieldName="UPMLedger" SummaryType="Sum" />
                                            <dx:ASPxSummaryItem FieldName="ChitNumber" SummaryType="Custom" />
                                           <%-- <dx:ASPxSummaryItem FieldName="PR" SummaryType="Sum" />
                                            <dx:ASPxSummaryItem FieldName="ChitNumber" SummaryType="Custom" />--%>

                                        </TotalSummary>
                                        <SettingsText Title="Bank Ledger" />
                                        <SettingsPager Mode="ShowAllRecords">
                                        </SettingsPager>
                                        <Columns>
                                           <%-- <dx:GridViewDataColumn FieldName="Group">
                                            </dx:GridViewDataColumn>--%>
                                           <%-- <dx:GridViewDataColumn FieldName="CRR" CellStyle-HorizontalAlign="Right" FooterCellStyle-HorizontalAlign="Right">
                                            </dx:GridViewDataColumn>--%>
                                           <%-- <dx:GridViewDataColumn FieldName="TRR" CellStyle-HorizontalAlign="Right" FooterCellStyle-HorizontalAlign="Right">
                                            </dx:GridViewDataColumn>--%>
                                             <dx:GridViewDataColumn FieldName="ChitNumber"  Width="16%" >
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn FieldName="CRR" FooterCellStyle-HorizontalAlign="Left">
                                            </dx:GridViewDataColumn>
                                              <dx:GridViewDataColumn FieldName="TRR1" FooterCellStyle-HorizontalAlign="Left">
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn FieldName="TRR2"  FooterCellStyle-HorizontalAlign="Left">
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn FieldName="PR"  FooterCellStyle-HorizontalAlign="Left">
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn FieldName="RCM"  FooterCellStyle-HorizontalAlign="Left">
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn FieldName="CCA"   FooterCellStyle-HorizontalAlign="Left">
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn FieldName="OPM"   FooterCellStyle-HorizontalAlign="Left">
                                            </dx:GridViewDataColumn>
                                             <dx:GridViewDataColumn FieldName="UPM" FooterCellStyle-HorizontalAlign="Left">
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn FieldName="UPMLedger"  FooterCellStyle-HorizontalAlign="Left">
                                            </dx:GridViewDataColumn>
                                            
                                          <%--  <dx:GridViewDataColumn FieldName="PR" CellStyle-HorizontalAlign="Right" FooterCellStyle-HorizontalAlign="Right">
                                            </dx:GridViewDataColumn>--%>
                                        </Columns>
                                        <%--<Styles Footer-HorizontalAlign="Left" Cell-HorizontalAlign="Left" Header-VerticalAlign="Middle"
                                            Header-HorizontalAlign="Center" Header-Wrap="True">
                                            <GroupPanel Wrap="True" HorizontalAlign="Left">
                                            </GroupPanel>
                                            <GroupRow Wrap="True" HorizontalAlign="Left">
                                            </GroupRow>
                                        </Styles>--%>
                                         <Styles Footer-HorizontalAlign="Right" Header-VerticalAlign="Middle" Header-HorizontalAlign="Center"
                                            Header-Wrap="True">
                                            <GroupPanel Wrap="True" HorizontalAlign="Left">
                                            </GroupPanel>
                                            <GroupRow Wrap="True" HorizontalAlign="Left">
                                            </GroupRow>
                                            <Header Wrap="True">
                                            </Header>
                                             </Styles>
                                    </dx:ASPxGridView>
                                </div>
                            </div>
                            </div>
                       <%-- </div>--%>
                        <div class="twelve columns sortable ui-sortable dragsortable">
                            <div class="box_c">
                                <div class="box_c_heading cf box_actions noprint">
                                    <p>
                                        Cash Received Register</p>
                                </div>
                                <div class="box_c_content">
                                    <dx:ASPxGridView Style="margin: 0 auto;
                                        width: 100%;" ID="gridCRR" ClientInstanceName="grid" runat="server">
                                        <Settings ShowTitlePanel="true" ShowHeaderFilterButton="false" 
                                            ShowFilterBar="Hidden" ShowFilterRow="false" ShowFilterRowMenu="false" />
                                        <SettingsText Title="Cash Received Register" />
                                        <SettingsPager Mode="ShowAllRecords">
                                        </SettingsPager>
                                        <Columns>
                                            <dx:GridViewDataColumn FieldName="slno" VisibleIndex="1" Width="5%" Caption="S. No.">
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn FieldName="Voucher_No" VisibleIndex="2" Width="7%" CellStyle-HorizontalAlign="Left"
                                                Caption="Receipt or Reference No.">
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn VisibleIndex="3" FieldName="ChitNumber" Width="7%" Caption="Chit Number">
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn FieldName="Narration" VisibleIndex="4" Width="16%" Caption="Call No.">
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn FieldName="MemberName" VisibleIndex="5" Width="10%" Caption="Name"
                                                GroupFooterCellStyle-HorizontalAlign="Right">
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn VisibleIndex="6" FieldName="ChitAmount" Width="9%" Caption="Amount"
                                                CellStyle-HorizontalAlign="Right">
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewBandColumn Caption="P & L Account" VisibleIndex="7">
                                                <Columns>
                                                    <dx:GridViewDataColumn FieldName="Amount" Caption="Amount" Width="9%" CellStyle-HorizontalAlign="Right">
                                                    </dx:GridViewDataColumn>
                                                </Columns>
                                            </dx:GridViewBandColumn>
                                            <dx:GridViewBandColumn Caption="Other Branch" VisibleIndex="8">
                                                <Columns>
                                                    <dx:GridViewDataColumn FieldName="OtherAmount" Caption="Amount" Width="9%" CellStyle-HorizontalAlign="Right">
                                                    </dx:GridViewDataColumn>
                                                </Columns>
                                            </dx:GridViewBandColumn>
                                            <dx:GridViewDataColumn FieldName="Heads" VisibleIndex="9"  Caption="Heads" Width="9%">
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn VisibleIndex="10" FieldName="GrandTotal" Width="10%" Caption="Grand Total">
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn VisibleIndex="11" FieldName="Remarks" Width="8%" Caption="Remarks">
                                            </dx:GridViewDataColumn>
                                        </Columns>
                                        <Styles Footer-HorizontalAlign="Right" Header-VerticalAlign="Middle" Header-HorizontalAlign="Center"
                                            Header-Wrap="True">
                                            <GroupPanel Wrap="True" HorizontalAlign="Left">
                                            </GroupPanel>
                                            <GroupRow Wrap="True" HorizontalAlign="Left">
                                            </GroupRow>
                                            <Header Wrap="True">
                                            </Header>
                                        </Styles>
                                    </dx:ASPxGridView>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="twelve columns sortable ui-sortable dragsortable">
                            <div class="box_c">
                                <div class="box_c_heading cf box_actions noprint">
                                    <p>
                                        Transfer Received Register</p>
                                </div>
                                <div class="box_c_content">
                                    <dx:ASPxGridView Style="margin: 0 auto;
                                        width: 100%;" ID="gridtrr" ClientInstanceName="gridtrr" runat="server">
                                        <Settings ShowTitlePanel="true"
                                            ShowHeaderFilterButton="false" 
                                            ShowFilterBar="Hidden" ShowFilterRow="false" ShowFilterRowMenu="false" />
                                        <GroupSummary>
                                            <dx:ASPxSummaryItem DisplayFormat="{0:n2}" FieldName="ChitAmount" ShowInGroupFooterColumn="ChitAmount"
                                                SummaryType="Sum" />
                                            <dx:ASPxSummaryItem DisplayFormat="{0:n2}" FieldName="BranchAmount" ShowInGroupFooterColumn="BranchAmount"
                                                SummaryType="Sum" />
                                            <dx:ASPxSummaryItem DisplayFormat="{0:n2}" FieldName="HeadAmount" ShowInGroupFooterColumn="HeadAmount"
                                                SummaryType="Sum" />
                                            <dx:ASPxSummaryItem DisplayFormat="Total : " FieldName="MemberName" ShowInGroupFooterColumn="MemberName"
                                                SummaryType="Sum" />
                                        </GroupSummary>
                                        <SettingsText Title="Transfer Remittance Received Register" />
                                        <SettingsPager Mode="ShowAllRecords">
                                        </SettingsPager>
                                        <Columns>
                                            <dx:GridViewDataColumn FieldName="slno" Width="10" Caption="S. No.">
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn FieldName="Voucher_No" VisibleIndex="1" Width="10%" CellStyle-HorizontalAlign="Left"
                                                Caption="Receipt or Reference No.">
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn FieldName="ChitNumber" VisibleIndex="2" Width="10%" Caption="Chit Number">
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn FieldName="Narration" VisibleIndex="3" Width="10%" Caption="Call No.">
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn FieldName="MemberName" VisibleIndex="4" Width="10%" Caption="Name"
                                                GroupFooterCellStyle-HorizontalAlign="Right">
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn FieldName="ChitAmount" VisibleIndex="5" Width="10%" Caption="Amount"
                                                CellStyle-HorizontalAlign="Right">
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewBandColumn Caption="P & L A/c" VisibleIndex="6">
                                                <Columns>
                                                    <dx:GridViewDataColumn FieldName="Amount" Caption="Amount" Width="10%" CellStyle-HorizontalAlign="Right">
                                                    </dx:GridViewDataColumn>
                                                </Columns>
                                            </dx:GridViewBandColumn>
                                             <dx:GridViewBandColumn Caption="Other Branch" VisibleIndex="7">
                                                <Columns>
                                                    <dx:GridViewDataColumn FieldName="OtherAmount" Caption="Amount" Width="10%" CellStyle-HorizontalAlign="Right">
                                                    </dx:GridViewDataColumn>
                                                </Columns>
                                            </dx:GridViewBandColumn>
                                            <dx:GridViewDataColumn  VisibleIndex="8" FieldName="AdviceAmount" Caption="AdviceAmount" Width="10%">
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn  VisibleIndex="8" FieldName="Heads" Caption="Heads" Width="1%" CellStyle-HorizontalAlign="Left">
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn FieldName="GrandTotal" VisibleIndex="9" Width="1%" Caption="Grand Total"
                                                CellStyle-HorizontalAlign="Right">
                                            </dx:GridViewDataColumn>
                                        </Columns>
                                        <Styles Footer-HorizontalAlign="Right" Header-VerticalAlign="Middle" Header-HorizontalAlign="Center"
                                            Header-Wrap="True">
                                            <GroupPanel Wrap="True" HorizontalAlign="Left">
                                            </GroupPanel>
                                            <GroupRow Wrap="True" HorizontalAlign="Left">
                                            </GroupRow>
                                            <Header Wrap="True">
                                            </Header>
                                        </Styles>
                                    </dx:ASPxGridView>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="six columns sortable ui-sortable dragsortable">
                            <div class="box_c">
                                <div class="box_c_heading cf box_actions noprint">
                                    <p>
                                        Today Auction Particulars</p>
                                </div>
                                <div class="box_c_content">
                                    <dx:ASPxGridView Style="margin: 0 auto; width: 100%;" ID="gridYesterday1" ClientInstanceName="gridYesterday1"
                                        runat="server" OnSummaryDisplayText="gridYesterday1_OnSummaryDisplayText" DataSourceID="AccessDataSource13">
                                        <Settings ShowFooter="true" ShowTitlePanel="true" />
                                        <SettingsText Title="Bank Ledger" />
                                        <SettingsPager Mode="ShowAllRecords">
                                        </SettingsPager>
                                        <TotalSummary>
                                            <dx:ASPxSummaryItem FieldName="prizedamount" SummaryType="Sum" />
                                            <dx:ASPxSummaryItem FieldName="nameofthesubscriber" SummaryType="Custom" />
                                        </TotalSummary>
                                        <Columns>
                                            <dx:GridViewDataColumn FieldName="ChitNo" Caption="Chit No.">
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn FieldName="InstNo" Caption="Inst No." FooterCellStyle-HorizontalAlign="Right">
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn FieldName="nameofthesubscriber" Caption="Name of the Subscriber"
                                                FooterCellStyle-HorizontalAlign="Right">
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn FieldName="prizedamount" Caption="Prized Amount" CellStyle-HorizontalAlign="Right"
                                                FooterCellStyle-HorizontalAlign="Right">
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
                            </div>
                        </div>
                        <div class="six columns sortable ui-sortable dragsortable">
                            <div class="box_c">
                                <div class="box_c_heading cf box_actions noprint">
                                    <p>
                                        Investments</p>
                                </div>
                                <div class="box_c_content">
                                    <dx:ASPxGridView Style="margin: 0 auto; width: 100%;" OnSummaryDisplayText="gridInvestments_OnSummaryDisplayText"
                                        ID="gridInvestments" ClientInstanceName="gridInvestments" runat="server" DataSourceID="AccessDataSource9">
                                        <Settings ShowFooter="true" ShowTitlePanel="true" />
                                        <SettingsText Title="Bank Ledger" />
                                        <SettingsPager Mode="ShowAllRecords">
                                        </SettingsPager>
                                        <TotalSummary>
                                            <dx:ASPxSummaryItem FieldName="Credit" SummaryType="Sum" />
                                            <dx:ASPxSummaryItem FieldName="Debit" SummaryType="Sum" />
                                            <dx:ASPxSummaryItem FieldName="onaccountof" SummaryType="Custom" />
                                        </TotalSummary>
                                        <Columns>
                                            <dx:GridViewDataColumn FieldName="Sub-Head">
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn Caption="On Account of" FieldName="onaccountof" FooterCellStyle-HorizontalAlign="Right">
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn FieldName="Credit" CellStyle-HorizontalAlign="Right" FooterCellStyle-HorizontalAlign="Right">
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn FieldName="Debit" CellStyle-HorizontalAlign="Right" FooterCellStyle-HorizontalAlign="Right">
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
                            </div>
                        </div>
                    </div>
                    <div style="page-break-after: always !important;">
                    </div>
                    <div class="row">
                        <div class="twelve columns sortable ui-sortable">
                            <div class="box_c">
                                <div class="box_c_heading cf box_actions noprint">
                                    <p>
                                        Branch</p>
                                </div>
                                <div class="box_c_content">
                                    <dx:ASPxGridView Style="margin: 0 auto; width: 100%;" OnSummaryDisplayText="gridBranches_SummaryDisplayText"
                                        ID="gridBranches" ClientInstanceName="gridBranches" runat="server" DataSourceID="AccessDataSource17">
                                        <Settings ShowTitlePanel="true" ShowFooter="true" />
                                        <TotalSummary>
                                            <dx:ASPxSummaryItem FieldName="Credit" SummaryType="Sum" />
                                            <dx:ASPxSummaryItem FieldName="Debit" SummaryType="Sum" />
                                            <dx:ASPxSummaryItem FieldName="Narration" SummaryType="Custom" />
                                        </TotalSummary>
                                        <SettingsText Title="1. Branch" />
                                        <SettingsPager Mode="ShowAllRecords">
                                        </SettingsPager>
                                         <Columns>
                                             <dx:GridViewDataColumn FieldName="BranchID" Width="20%" FooterCellStyle-HorizontalAlign="Right"  >
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn FieldName="Branch" Width="20%" FooterCellStyle-HorizontalAlign="Right">
                                            </dx:GridViewDataColumn>
                                            <%--<dx:GridViewDataColumn Width="16%" FieldName="LedgerHead" Caption="Ledger Head" FooterCellStyle-HorizontalAlign="Right">
                                            </dx:GridViewDataColumn>--%>
                                            <dx:GridViewDataColumn Width="30%" FieldName="Narration" Caption="On a/c of">
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn Width="17%" FieldName="Credit" CellStyle-HorizontalAlign="Right"
                                                FooterCellStyle-HorizontalAlign="Right">
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn Width="17%" FieldName="Debit" CellStyle-HorizontalAlign="Right"
                                                FooterCellStyle-HorizontalAlign="Right">
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
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="twelve columns sortable ui-sortable">
                            <div class="box_c">
                                <div class="box_c_heading cf box_actions noprint">
                                    <p>
                                        Banks</p>
                                </div>
                                <div class="box_c_content">
                                    <dx:ASPxGridView Style="margin: 0 auto; width: 100%;" OnSummaryDisplayText="banks3_SummaryDisplayText"
                                        ID="grid" ClientInstanceName="grid" runat="server" DataSourceID="AccessDataSource1">
                                        <Settings GroupFormat="{1}{2}" ShowTitlePanel="true" ShowFooter="true" ShowGroupedColumns="true"
                                            ShowGroupPanel="false" />
                                        <GroupSummary>
                                            <dx:ASPxSummaryItem DisplayFormat="Cr. {0:n2}" FieldName="Credit" SummaryType="Sum" />
                                            <dx:ASPxSummaryItem DisplayFormat="Dr. {0:n2}" FieldName="Debit" SummaryType="Sum" />
                                        </GroupSummary>
                                        <TotalSummary>
                                            <dx:ASPxSummaryItem FieldName="Credit" SummaryType="Sum" />
                                            <dx:ASPxSummaryItem FieldName="Debit" SummaryType="Sum" />
                                            <dx:ASPxSummaryItem FieldName="Narration" SummaryType="Custom" />
                                        </TotalSummary>
                                        <SettingsText Title="Bank Ledger" />
                                        <SettingsPager Mode="ShowAllRecords">
                                        </SettingsPager>
                                        <Columns>
                                             <%-- <dx:GridViewDataColumn FieldName="onaccountof" Width="15%" Caption="On a/c of">
                                            </dx:GridViewDataColumn>--%>
                                            <dx:GridViewDataColumn FieldName="onaccountof" Width="15%" Caption="Name of the Bank">
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn FieldName="Head" Caption="On a/c of" Width="15%">
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn Visible="false" FieldName="NameoftheBank" Caption="Name of the Bank">
                                            </dx:GridViewDataColumn>
                                          
                                            <dx:GridViewDataColumn FieldName="CashorCheque" Width="15%" Caption="Cash or Cheque">
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn FieldName="Narration" Width="25%">
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn FieldName="Credit" Width="15%" CellStyle-HorizontalAlign="Right"
                                                GroupFooterCellStyle-HorizontalAlign="Right" FooterCellStyle-HorizontalAlign="Right">
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn FieldName="Debit" Width="15%" CellStyle-HorizontalAlign="Right"
                                                GroupFooterCellStyle-HorizontalAlign="Right" FooterCellStyle-HorizontalAlign="Right">
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
                            </div>
                        </div>
                    </div>
                    <%--<div class="row">
                        <div class="twelve columns sortable ui-sortable">
                            <div class="box_c">
                                <div class="box_c_heading cf box_actions noprint">
                                    <p>
                                        Arrear</p>
                                </div>
                                <div class="box_c_content">
                                    <dx:ASPxGridView Style="margin: 0 auto; width: 100%;" ID="gridArrear" ClientInstanceName="gridArrear"
                                        runat="server">
                                        <Settings ShowHeaderFilterButton="false" ShowFilterRow="false" ShowFilterRowMenu="false"
                                            ShowTitlePanel="true" ShowFooter="true" />
                                        <Styles Cell-HorizontalAlign="Left" Header-VerticalAlign="Middle" Header-HorizontalAlign="Center"
                                            Header-Wrap="True">
                                            <Header Wrap="True">
                                            </Header>
                                        </Styles>
                                        <SettingsPager Mode="ShowAllRecords">
                                        </SettingsPager>
                                        <SettingsText Title="Arrear List" />
                                        <TotalSummary>
                                            <dx:ASPxSummaryItem DisplayFormat="{0}" FieldName="NPArrier" SummaryType="Sum" />
                                            <dx:ASPxSummaryItem DisplayFormat="{0}" FieldName="PArrier" SummaryType="Sum" />
                                            <dx:ASPxSummaryItem DisplayFormat="{0}" FieldName="MemberName" SummaryType="Custom" />
                                        </TotalSummary>
                                        <Columns>
                                            <dx:GridViewDataColumn FieldName="ChitNo" Caption="Chit Number">
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn FieldName="MemberName" Caption="Member Name">
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewBandColumn Caption="Arrear">
                                                <Columns>
                                                    <dx:GridViewDataColumn FieldName="NPArrier" Caption="Non-Prized" FooterCellStyle-HorizontalAlign="Right"
                                                        CellStyle-HorizontalAlign="Right">
                                                    </dx:GridViewDataColumn>
                                                    <dx:GridViewDataColumn FieldName="PArrier" Caption="Prized" FooterCellStyle-HorizontalAlign="Right"
                                                        CellStyle-HorizontalAlign="Right">
                                                    </dx:GridViewDataColumn>
                                                </Columns>
                                            </dx:GridViewBandColumn>
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
                            </div>
                        </div>
                    </div>--%>
                    <div class="row">
                        <div class="twelve columns sortable ui-sortable">
                            <div class="box_c">
                                <div class="box_c_heading cf box_actions noprint">
                                    <p>
                                        Details for Debit in Chit Account (Debit Part of 5)</p>
                                </div>
                                <div class="box_c_content">
                                    <dx:ASPxGridView Style="margin: 0 auto; width: 100%;" OnSummaryDisplayText="gridDebit_OnSummaryDisplayText"
                                        ID="gridDebit" ClientInstanceName="gridDebit" runat="server">
                                        <Settings ShowFooter="true" ShowTitlePanel="true" />
                                        <SettingsText Title="Details for Debit in Chit Account (Debit Part of 5)" />
                                        <SettingsPager Mode="ShowAllRecords">
                                        </SettingsPager>
                                        <TotalSummary>
                                            <dx:ASPxSummaryItem FieldName="Commision" SummaryType="Sum" />
                                            <dx:ASPxSummaryItem FieldName="ServiceTax" SummaryType="Sum" />
                                            <dx:ASPxSummaryItem FieldName="Auction No" SummaryType="Sum"/>                                 
                                            <dx:ASPxSummaryItem FieldName="ChitKasarAmount" SummaryType="Sum" />
                                            <dx:ASPxSummaryItem FieldName="Prized" SummaryType="Sum" />
                                            <dx:ASPxSummaryItem FieldName="Loan" SummaryType="Sum" />
                                            <dx:ASPxSummaryItem FieldName="FutureCallAmount" SummaryType="Sum" />
                                            <dx:ASPxSummaryItem FieldName="Total" SummaryType="Sum" />
                                            <dx:ASPxSummaryItem FieldName="Narration" SummaryType="Sum" />
                                            <dx:ASPxSummaryItem FieldName="GST" SummaryType="Sum" />
                                             <dx:ASPxSummaryItem FieldName="IGST" SummaryType="Sum" />  
                                            <dx:ASPxSummaryItem FieldName="Cash/Cheque" SummaryType="Custom" />
                                        </TotalSummary>
                                        <Columns>
                                            <dx:GridViewDataColumn FieldName="ChitNo" Caption="Chit No.">
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn FieldName="Name" Caption="Name">
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn FieldName="Cash/Cheque" Caption="Cash / Cheque No. and Bank"
                                                FooterCellStyle-HorizontalAlign="Right">
                                            </dx:GridViewDataColumn>
                                              <dx:GridViewDataColumn FieldName ="AuctionNo" Caption="Auction No" FooterCellStyle-HorizontalAlign ="Right"></dx:GridViewDataColumn>
                                        
                                            <dx:GridViewBandColumn Caption="Actual Amount Paid">
                                                <Columns>
                                                    <dx:GridViewDataColumn FieldName="Prized" CellStyle-HorizontalAlign="Right" Caption="Paid Amount"
                                                        FooterCellStyle-HorizontalAlign="Right">
                                                    </dx:GridViewDataColumn>
                                                    <dx:GridViewDataColumn FieldName="Commision" CellStyle-HorizontalAlign="Right" Caption="Commision"
                                                        FooterCellStyle-HorizontalAlign="Right">
                                                    </dx:GridViewDataColumn>
                                                    <dx:GridViewDataColumn Visible="false" FieldName="ServiceTax" CellStyle-HorizontalAlign="Right" Caption="Service Tax"
                                                        FooterCellStyle-HorizontalAlign="Right">
                                                    </dx:GridViewDataColumn>
                                                      <dx:GridViewDataColumn FieldName="GST" CellStyle-HorizontalAlign="Right" Caption="GST(CGST+SGST)"
                                                        FooterCellStyle-HorizontalAlign="Right">
                                                    </dx:GridViewDataColumn>
                                                    <dx:GridViewDataColumn FieldName="IGST" CellStyle-HorizontalAlign="Right"
                                                        Caption="IGST" FooterCellStyle-HorizontalAlign="Right">
                                                    </dx:GridViewDataColumn>
                                                  
                                                </Columns>
                                            </dx:GridViewBandColumn>
                                            <dx:GridViewBandColumn Caption="Adjustment if any">
                                                <Columns>
                                                    <dx:GridViewDataColumn Caption="Chit Dividend Amount" CellStyle-HorizontalAlign="Right"
                                                        FieldName="ChitKasarAmount" FooterCellStyle-HorizontalAlign="Right">
                                                    </dx:GridViewDataColumn>
                                                    <dx:GridViewDataColumn FieldName="Loan" Caption="Loan" CellStyle-HorizontalAlign="Right"
                                                        FooterCellStyle-HorizontalAlign="Right">
                                                    </dx:GridViewDataColumn>
                                                    <dx:GridViewDataColumn FieldName="FutureCallAmount" CellStyle-HorizontalAlign="Right"
                                                        Caption="Future Call Amount" FooterCellStyle-HorizontalAlign="Right">
                                                    </dx:GridViewDataColumn>
                                                </Columns>
                                            </dx:GridViewBandColumn>
                                            <dx:GridViewDataColumn FieldName="Total" Caption="Total" CellStyle-HorizontalAlign="Right"
                                                FooterCellStyle-HorizontalAlign="Right">
                                            </dx:GridViewDataColumn>
                                              <dx:GridViewDataColumn FieldName ="Narration" Caption="Narration" CellStyle-HorizontalAlign ="Right" 
                                            FooterCellStyle-HorizontalAlign ="Right" ></dx:GridViewDataColumn> 
                                        

                                            <dx:GridViewDataColumn FieldName="AOSanctionNo" Caption="A.O. Sanction No." FooterCellStyle-HorizontalAlign="Right">
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
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="twelve columns sortable ui-sortable">
                            <div class="box_c">
                                <div class="box_c_heading cf box_actions noprint">
                                    <p>
                                        Other Items</p>
                                </div>
                                <div class="box_c_content">
                                    <dx:ASPxGridView Style="margin: 0 auto; width: 100%;" OnSummaryDisplayText="gridOther_SummaryDisplayText"
                                        ID="gridOther" ClientInstanceName="gridOther" runat="server" DataSourceID="AccessDataSource6">
                                        <Settings ShowTitlePanel="true" ShowFooter="true" />
                                        <TotalSummary>
                                            <dx:ASPxSummaryItem FieldName="Credit" SummaryType="Sum" />
                                            <dx:ASPxSummaryItem FieldName="Debit" SummaryType="Sum" />
                                            <dx:ASPxSummaryItem FieldName="Sub-Head" SummaryType="Custom" />
                                        </TotalSummary>
                                        <SettingsText Title="Bank Ledger" />
                                        <SettingsPager Mode="ShowAllRecords">
                                        </SettingsPager>
                                        <Columns>
                                            <dx:GridViewDataColumn FieldName="Sub-Head" FooterCellStyle-HorizontalAlign="Right">
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn FieldName="Credit" CellStyle-HorizontalAlign="Right" FooterCellStyle-HorizontalAlign="Right">
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn FieldName="Debit" CellStyle-HorizontalAlign="Right" FooterCellStyle-HorizontalAlign="Right">
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn FieldName="onaccountof" Caption="On a/c of">
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
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="twelve columns sortable ui-sortable">
                            <div class="box_c">
                                <div class="box_c_heading cf box_actions noprint">
                                    <p>
                                        Company Chits</p>
                                </div>
                                <div class="box_c_content">
                                    <dx:ASPxGridView Style="margin: 0 auto; width: 100%;" ID="gridCompanyChits" ClientInstanceName="gridCompanyChits"
                                        OnSummaryDisplayText="gridCompanyChits_OnSummaryDisplayText" runat="server" DataSourceID="AccessDataSource14">
                                        <Settings ShowFooter="true" ShowTitlePanel="true" />
                                        <SettingsText Title="Bank Ledger" />
                                        <SettingsPager Mode="ShowAllRecords">
                                        </SettingsPager>
                                        <TotalSummary>
                                            <dx:ASPxSummaryItem FieldName="PrizeAmount" SummaryType="Sum" />
                                            <dx:ASPxSummaryItem FieldName="CallAmount" SummaryType="Sum" />
                                            <dx:ASPxSummaryItem FieldName="CallNo" SummaryType="Custom" />
                                        </TotalSummary>
                                        <Columns>
                                            <dx:GridViewDataColumn FieldName="ChitNo" Caption="Chit No.">
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn FieldName="C.C. / C.S. Chit" Caption="C.C. / C.S. Chit" FooterCellStyle-HorizontalAlign="Right">
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn FieldName="CallNo" Caption="Call No." FooterCellStyle-HorizontalAlign="Right">
                                            </dx:GridViewDataColumn>
                                          <%--  <dx:GridViewDataColumn FieldName="PrizeAmount" Caption="Prized Amount" FooterCellStyle-HorizontalAlign="Right">
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn FieldName="CallAmount" Caption="Call Amount" FooterCellStyle-HorizontalAlign="Right">
                                            </dx:GridViewDataColumn>--%>
                                                <dx:GridViewDataColumn FieldName="PrizeAmount" Caption="Credit Amount" FooterCellStyle-HorizontalAlign="Right">
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn FieldName="CallAmount" Caption="Debit Amount" FooterCellStyle-HorizontalAlign="Right">
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
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="twelve columns sortable ui-sortable dragsortable">
                            <div class="box_c">
                                <div class="box_c_heading cf box_actions noprint">
                                    <p>
                                        Decree Debtors</p>
                                </div>
                                <div class="box_c_content">
                                    <dx:ASPxGridView AutoGenerateColumns="false" Style="margin: 0 auto; width: 100%;"
                                        ID="gridDecreeDebtors" ClientInstanceName="gridDecreeDebtors" runat="server">
                                        <Settings ShowTitlePanel="true" ShowFooter="true" ShowFilterBar="Hidden" ShowFilterRow="false" />
                                        <SettingsText Title="Decree" />
                                        <SettingsPager Mode="ShowAllRecords">
                                        </SettingsPager>
                                        <Styles Footer-HorizontalAlign="Right" Cell-HorizontalAlign="Right" Header-VerticalAlign="Middle"
                                            Header-HorizontalAlign="Center" Header-Wrap="True">
                                        </Styles>
                                        <Columns>
                                            <dx:GridViewDataColumn Width="10%" FieldName="SlNo" ExportWidth="50" Caption="S.No."
                                                CellStyle-HorizontalAlign="Right">
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn Width="20%" FieldName="CC No" ExportWidth="150" FooterCellStyle-HorizontalAlign="Right"
                                                CellStyle-HorizontalAlign="Left">
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn Width="20%" FieldName="EP No./OS No./ARC No./ARB No." ExportWidth="100"
                                                FooterCellStyle-HorizontalAlign="Left" CellStyle-HorizontalAlign="Left">
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn Width="20%" FieldName="ChitName" ExportWidth="100" FooterCellStyle-HorizontalAlign="Right"
                                                CellStyle-HorizontalAlign="Left">
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn Width="20%" FieldName="Name" ExportWidth="100" FooterCellStyle-HorizontalAlign="Right"
                                                CellStyle-HorizontalAlign="Left">
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewBandColumn Caption="DECREE">
                                                <Columns>
                                                    <dx:GridViewDataColumn Width="30%" FieldName="CreditDECREE" ExportWidth="100" FooterCellStyle-HorizontalAlign="Right"
                                                        Caption="Credit" CellStyle-HorizontalAlign="Right">
                                                    </dx:GridViewDataColumn>
                                                    <dx:GridViewDataColumn Width="30%" FieldName="DebitDECREE" ExportWidth="100" FooterCellStyle-HorizontalAlign="Right"
                                                        Caption="Debit" CellStyle-HorizontalAlign="Right">
                                                    </dx:GridViewDataColumn>
                                                </Columns>
                                            </dx:GridViewBandColumn>
                                            <dx:GridViewBandColumn Caption="COST">
                                                <Columns>
                                                    <dx:GridViewDataColumn Width="30%" FieldName="CreditCOST" ExportWidth="100" FooterCellStyle-HorizontalAlign="Right"
                                                        Caption="Credit" CellStyle-HorizontalAlign="Right">
                                                    </dx:GridViewDataColumn>
                                                    <dx:GridViewDataColumn Width="30%" FieldName="DebitCOST" ExportWidth="100" FooterCellStyle-HorizontalAlign="Right"
                                                        Caption="Debit" CellStyle-HorizontalAlign="Right">
                                                    </dx:GridViewDataColumn>
                                                </Columns>
                                            </dx:GridViewBandColumn>
                                            <dx:GridViewDataColumn Width="20%" FieldName="" ExportWidth="100" FooterCellStyle-HorizontalAlign="Right"
                                                Caption="Description" CellStyle-HorizontalAlign="Right">
                                            </dx:GridViewDataColumn>
                                        </Columns>
                                    </dx:ASPxGridView>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="six columns sortable ui-sortable dragsortable">
                            <div class="box_c">
                                <div class="box_c_heading cf box_actions noprint">
                                    <p>
                                        Sundries & Advances</p>
                                </div>
                                <div class="box_c_content">
                                    <dx:ASPxGridView Style="margin: 0 auto; width: 100%;" ID="gridAdvances" ClientInstanceName="gridAdvances"
                                        runat="server" DataSourceID="AccessDataSource8" OnSummaryDisplayText="gridAdvances_SummaryDisplayText">
                                        <Settings GroupFormat="{1}{2}" ShowFooter="true" ShowTitlePanel="true" />
                                        <SettingsText Title="Bank Ledger" />
                                        <SettingsPager Mode="ShowAllRecords">
                                        </SettingsPager>
                                        <TotalSummary>
                                            <dx:ASPxSummaryItem FieldName="Credit" SummaryType="Sum" />
                                            <dx:ASPxSummaryItem FieldName="Debit" SummaryType="Sum" />
                                            <dx:ASPxSummaryItem FieldName="Name" SummaryType="Custom" />
                                        </TotalSummary>
                                        <Columns>
                                            <dx:GridViewDataColumn FieldName="Sub-Head">
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn FieldName="Name" FooterCellStyle-HorizontalAlign="Right">
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn FieldName="Credit" CellStyle-HorizontalAlign="Right" FooterCellStyle-HorizontalAlign="Right">
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn FieldName="Debit" CellStyle-HorizontalAlign="Right" FooterCellStyle-HorizontalAlign="Right">
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
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="twelve columns sortable ui-sortable">
                            <div class="box_c">
                                <div class="box_c_heading cf box_actions noprint">
                                    <p>
                                        Loan</p>
                                </div>
                                <div class="box_c_content">
                                    <dx:ASPxGridView Style="margin: 0 auto; width: 100%;" OnSummaryDisplayText="gridLoan_OnSummaryDisplayText"
                                        ID="gridLoan" ClientInstanceName="gridLoan" runat="server" DataSourceID="AccessDataSource7">
                                        <Settings ShowFooter="true" ShowTitlePanel="true" />
                                        <SettingsText Title="Bank Ledger" />
                                        <SettingsPager Mode="ShowAllRecords">
                                        </SettingsPager>
                                        <TotalSummary>
                                            <dx:ASPxSummaryItem FieldName="LoanAmountReceivedBack" SummaryType="Sum" />
                                            <dx:ASPxSummaryItem FieldName="LoanAmountPaid" SummaryType="Sum" />
                                            <dx:ASPxSummaryItem FieldName="ChitNo" SummaryType="Custom" />
                                        </TotalSummary>
                                        <Columns>
                                            <dx:GridViewDataColumn Caption="Loan No.">
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn FieldName="Name" FooterCellStyle-HorizontalAlign="Left">
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn FieldName="Chit/StaffLoan" Caption="Chit / Staff Loan">
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn FieldName="ChitNo" Caption="Chit No.">
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn FieldName="LoanAmountReceivedBack" CellStyle-HorizontalAlign="Right"
                                                FooterCellStyle-HorizontalAlign="Right">
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn FieldName="LoanAmountPaid" CellStyle-HorizontalAlign="Right"
                                                FooterCellStyle-HorizontalAlign="Right">
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn Caption="Sub Head">
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
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="twelve columns sortable ui-sortable">
                            <div class="box_c">
                                <div class="box_c_heading cf box_actions noprint">
                                    <p>
                                        Profit and Loss</p>
                                </div>
                                <div class="box_c_content">
                                    <dx:ASPxGridView Style="margin: 0 auto; width: 100%;" OnSummaryDisplayText="gridProfit_SummaryDisplayText"
                                        ID="gridProfit" ClientInstanceName="gridProfit" runat="server" DataSourceID="AccessDataSource5">
                                        <Settings ShowTitlePanel="true" ShowFooter="true" />
                                        <TotalSummary>
                                            <dx:ASPxSummaryItem FieldName="Credit" SummaryType="Sum" />
                                            <dx:ASPxSummaryItem FieldName="Debit" SummaryType="Sum" />
                                            <dx:ASPxSummaryItem FieldName="Sub-Head" SummaryType="Custom" />
                                        </TotalSummary>
                                        <SettingsText Title="Bank Ledger" />
                                        <SettingsPager Mode="ShowAllRecords">
                                        </SettingsPager>
                                        <Columns>
                                            <dx:GridViewDataColumn FieldName="Sub-Head" FooterCellStyle-HorizontalAlign="Right">
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn FieldName="Credit" CellStyle-HorizontalAlign="Right" FooterCellStyle-HorizontalAlign="Right">
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn FieldName="Debit" CellStyle-HorizontalAlign="Right" FooterCellStyle-HorizontalAlign="Right">
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn FieldName="onaccountof" Caption="On a/c of">
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
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        
                        <div class="six columns sortable ui-sortable dragsortable">
                            <div class="box_c">
                                <div class="box_c_heading cf box_actions noprint">
                                    <p>
                                        Stamps</p>
                                </div>
                                <div class="box_c_content">
                                    <dx:ASPxGridView Style="margin: 0 auto; width: 100%;" ID="gridStamps" ClientInstanceName="gridStamps"
                                        runat="server" OnSummaryDisplayText="gridStamps_OnSummaryDisplayText" DataSourceID="AccessDataSource10">
                                        <Settings ShowFooter="true" ShowTitlePanel="true" />
                                        <SettingsText Title="Bank Ledger" />
                                        <SettingsPager Mode="ShowAllRecords">
                                        </SettingsPager>
                                        <TotalSummary>
                                            <dx:ASPxSummaryItem FieldName="Credit" SummaryType="Sum" />
                                            <dx:ASPxSummaryItem FieldName="Debit" SummaryType="Sum" />
                                            <dx:ASPxSummaryItem FieldName="onaccountof" SummaryType="Custom" />
                                        </TotalSummary>
                                        <Columns>
                                            <dx:GridViewDataColumn FieldName="Sub-Head">
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn Caption="On Account of" FieldName="onaccountof" FooterCellStyle-HorizontalAlign="Right">
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn FieldName="Credit" CellStyle-HorizontalAlign="Right" FooterCellStyle-HorizontalAlign="Right">
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn FieldName="Debit" CellStyle-HorizontalAlign="Right" FooterCellStyle-HorizontalAlign="Right">
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
                            </div>
                        </div>
                        <div class="six columns sortable ui-sortable dragsortable">
                            <div class="box_c">
                                <div class="box_c_heading cf box_actions noprint">
                                    <p>
                                        Cash</p>
                                </div>
                                <div class="box_c_content">
                                    <dx:ASPxGridView Style="margin: 0 auto; width: 100%;" ID="gridCash" ClientInstanceName="gridCash"
                                        runat="server" DataSourceID="AccessDataSource16">
                                        <Settings ShowTitlePanel="true" />
                                        <SettingsText Title="12. Cash" />
                                        <SettingsPager Mode="ShowAllRecords">
                                        </SettingsPager>
                                        <Columns>
                                            <dx:GridViewDataColumn FieldName="Title" FooterCellStyle-HorizontalAlign="Left">
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn FieldName="Cash">
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
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <asp:SqlDataSource runat="server" ID="AccessDataSource1" ProviderName="MySql.Data.MySqlClient"
        SelectCommand=" " />
    <asp:SqlDataSource runat="server" ID="AccessDataSource2" ProviderName="MySql.Data.MySqlClient"
        SelectCommand=" " />
    <asp:SqlDataSource runat="server" ID="AccessDataSource3" ProviderName="MySql.Data.MySqlClient"
        SelectCommand=" " />
    <asp:SqlDataSource runat="server" ID="AccessDataSource5" ProviderName="MySql.Data.MySqlClient"
        SelectCommand=" " />
    <asp:SqlDataSource runat="server" ID="AccessDataSource6" ProviderName="MySql.Data.MySqlClient"
        SelectCommand=" " />
    <asp:SqlDataSource runat="server" ID="AccessDataSource7" ProviderName="MySql.Data.MySqlClient"
        SelectCommand=" " />
    <asp:SqlDataSource runat="server" ID="AccessDataSource8" ProviderName="MySql.Data.MySqlClient"
        SelectCommand=" " />
    <asp:SqlDataSource runat="server" ID="AccessDataSource9" ProviderName="MySql.Data.MySqlClient"
        SelectCommand=" " />
    <asp:SqlDataSource runat="server" ID="AccessDataSource10" ProviderName="MySql.Data.MySqlClient"
        SelectCommand=" " />
    <asp:SqlDataSource runat="server" ID="AccessDataSource13" ProviderName="MySql.Data.MySqlClient"
        SelectCommand=" " />
    <asp:SqlDataSource runat="server" ID="AccessDataSource14" ProviderName="MySql.Data.MySqlClient"
        SelectCommand=" " />
    <asp:SqlDataSource runat="server" ID="AccessDataSource16" ProviderName="MySql.Data.MySqlClient"
        SelectCommand=" " />
    <asp:SqlDataSource runat="server" ID="AccessDataSource17" ProviderName="MySql.Data.MySqlClient"
        SelectCommand=" " />
    <dx:ASPxGridViewExporter ID="exportBanks" runat="server" GridViewID="gridBankCollection">
        <Styles>
            <Header Wrap="True" HorizontalAlign="Center">
            </Header>
            <Cell HorizontalAlign="Left" Wrap="True">
            </Cell>
            <Footer Wrap="True" HorizontalAlign="Left">
            </Footer>
        </Styles>
    </dx:ASPxGridViewExporter>
    <dx:ASPxGridViewExporter ID="exportYesterday" runat="server" GridViewID="gridYesterday1">
        <Styles>
            <Header Wrap="True" HorizontalAlign="Center">
            </Header>
            <Cell HorizontalAlign="Left" Wrap="True">
            </Cell>
            <Footer Wrap="True" HorizontalAlign="Left">
            </Footer>
        </Styles>
    </dx:ASPxGridViewExporter>
    <dx:ASPxGridViewExporter ID="exportBranches" runat="server" GridViewID="gridBranchCollection">
        <Styles>
            <Header Wrap="True" HorizontalAlign="Center">
            </Header>
            <Cell HorizontalAlign="Left" Wrap="True">
            </Cell>
            <Footer Wrap="True" HorizontalAlign="Left">
            </Footer>
        </Styles>
    </dx:ASPxGridViewExporter>
    <dx:ASPxGridViewExporter ID="exportChits" runat="server" GridViewID="gridChitCollection">
        <Styles>
            <Header Wrap="True" HorizontalAlign="Center">
            </Header>
            <Cell HorizontalAlign="Left" Wrap="True">
            </Cell>
            <Footer Wrap="True" HorizontalAlign="Left">
            </Footer>
        </Styles>
    </dx:ASPxGridViewExporter>
    <dx:ASPxGridViewExporter ID="exportTrialBalance" runat="server" GridViewID="gridTrialBalance">
        <Styles>
            <Header Wrap="True" HorizontalAlign="Center">
            </Header>
            <Cell HorizontalAlign="Left" Wrap="True">
            </Cell>
            <Footer Wrap="True" HorizontalAlign="Left">
            </Footer>
        </Styles>
    </dx:ASPxGridViewExporter>
    <dx:ASPxGridViewExporter ID="exportBank3" runat="server" GridViewID="grid">
        <Styles>
            <Header Wrap="True" HorizontalAlign="Center">
            </Header>
            <Cell HorizontalAlign="Left" Wrap="True">
            </Cell>
            <Footer Wrap="True" HorizontalAlign="Left">
            </Footer>
        </Styles>
    </dx:ASPxGridViewExporter>
    <dx:ASPxGridViewExporter ID="exportDebit" runat="server" GridViewID="gridDebit">
        <Styles>
            <Header Wrap="True" HorizontalAlign="Center">
            </Header>
            <Cell HorizontalAlign="Left" Wrap="True">
            </Cell>
            <Footer Wrap="True" HorizontalAlign="Left">
            </Footer>
        </Styles>
    </dx:ASPxGridViewExporter>
    <dx:ASPxGridViewExporter ID="exportProfit" runat="server" GridViewID="gridProfit">
        <Styles>
            <Header Wrap="True" HorizontalAlign="Center">
            </Header>
            <Cell HorizontalAlign="Left" Wrap="True">
            </Cell>
            <Footer Wrap="True" HorizontalAlign="Left">
            </Footer>
        </Styles>
    </dx:ASPxGridViewExporter>
    <dx:ASPxGridViewExporter ID="exportOther" runat="server" GridViewID="gridOther">
        <Styles>
            <Header Wrap="True" HorizontalAlign="Center">
            </Header>
            <Cell HorizontalAlign="Left" Wrap="True">
            </Cell>
            <Footer Wrap="True" HorizontalAlign="Left">
            </Footer>
        </Styles>
    </dx:ASPxGridViewExporter>
    <dx:ASPxGridViewExporter ID="exportLoan" runat="server" GridViewID="gridLoan">
        <Styles>
            <Header Wrap="True" HorizontalAlign="Center">
            </Header>
            <Cell HorizontalAlign="Left" Wrap="True">
            </Cell>
            <Footer Wrap="True" HorizontalAlign="Left">
            </Footer>
        </Styles>
    </dx:ASPxGridViewExporter>
    <dx:ASPxGridViewExporter ID="exportAdvances" runat="server" GridViewID="gridAdvances">
        <Styles>
            <Header Wrap="True" HorizontalAlign="Center">
            </Header>
            <Cell HorizontalAlign="Left" Wrap="True">
            </Cell>
            <Footer Wrap="True" HorizontalAlign="Left">
            </Footer>
        </Styles>
    </dx:ASPxGridViewExporter>
    <dx:ASPxGridViewExporter ID="exportInvestments" runat="server" GridViewID="gridInvestments">
        <Styles>
            <Header Wrap="True" HorizontalAlign="Center">
            </Header>
            <Cell HorizontalAlign="Left" Wrap="True">
            </Cell>
            <Footer Wrap="True" HorizontalAlign="Left">
            </Footer>
        </Styles>
    </dx:ASPxGridViewExporter>
    <dx:ASPxGridViewExporter ID="exportCompanyChits" runat="server" GridViewID="gridCompanyChits">
        <Styles>
            <Header Wrap="True" HorizontalAlign="Center">
            </Header>
            <Cell HorizontalAlign="Left" Wrap="True">
            </Cell>
            <Footer Wrap="True" HorizontalAlign="Left">
            </Footer>
        </Styles>
    </dx:ASPxGridViewExporter>
    <dx:ASPxGridViewExporter ID="exportDecreeDebtors" runat="server" GridViewID="gridDecreeDebtors">
        <Styles>
            <Header Wrap="True" HorizontalAlign="Center">
            </Header>
            <Cell HorizontalAlign="Left" Wrap="True">
            </Cell>
            <Footer Wrap="True" HorizontalAlign="Left">
            </Footer>
        </Styles>
    </dx:ASPxGridViewExporter>
    <dx:ASPxGridViewExporter ID="exportStamps" runat="server" GridViewID="gridStamps">
        <Styles>
            <Header Wrap="True" HorizontalAlign="Center">
            </Header>
            <Cell HorizontalAlign="Left" Wrap="True">
            </Cell>
            <Footer Wrap="True" HorizontalAlign="Left">
            </Footer>
        </Styles>
    </dx:ASPxGridViewExporter>
    <dx:ASPxGridViewExporter ID="exportCash" runat="server" GridViewID="gridCash">
        <Styles>
            <Header Wrap="True" HorizontalAlign="Center">
            </Header>
            <Cell HorizontalAlign="Left" Wrap="True">
            </Cell>
            <Footer Wrap="True" HorizontalAlign="Left">
            </Footer>
        </Styles>
    </dx:ASPxGridViewExporter>
    <dx:ASPxGridViewExporter ID="exportBranches1" runat="server" GridViewID="gridBranches">
        <Styles>
            <Header Wrap="True" HorizontalAlign="Center">
            </Header>
            <Cell HorizontalAlign="Left" Wrap="True">
            </Cell>
            <Footer Wrap="True" HorizontalAlign="Left">
            </Footer>
        </Styles>
    </dx:ASPxGridViewExporter>
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
            $("#btnExport").click(function (e) {
                printDiv('printdiv');
                e.preventDefault();
            });
            prth_mask_input.init();
        });
    </script>
    <style type="text/css">
        @media screen
        {
            .aaaaaa
            {
                display: none;
            }
        }
        @media print
        {
            .aaaaaa
            {
                width: 100%;
                display: inline;
                display: inline-block;
            }
            .box_c
            {
                margin-bottom: 0px !important;
            }
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
            .box_c_content
            {
                padding: 0px !important;
            }
        }
    </style>
</asp:Content>
