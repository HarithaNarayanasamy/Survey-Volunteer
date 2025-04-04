<%@ Page Title="" Language="C#" Culture="en-GB" MasterPageFile="~/Branch.Master" 
    AutoEventWireup="true" CodeBehind="BookletExport.aspx.cs" Inherits="SreeVisalamChitFundLtd_phase1.BookletExport" %>

<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPopupControl" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1.Export, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView.Export" TagPrefix="dx" %>
<%@ Register TagPrefix="dx" Namespace="DevExpress.Web.ASPxEditors" Assembly="DevExpress.Web.ASPxEditors.v11.1" %>
<%@ Register assembly="DevExpress.Web.ASPxGridView.v11.1" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMainContent" runat="server">
    <div class="row">
        <div class="twelve columns">
            <div class="box_c">
                <div class="box_c_heading  box_actions">
                    <p>
                        Booklet</p>
                </div>
                <div class="box_c_content">
                    <asp:Panel ID="Panel1" runat="server">
                        <div style="display: table-cell; vertical-align: top; padding-top: 8px;">
                            <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="Year start Date : ">
                            </dx:ASPxLabel>
                        </div>
                        <div style="display: table-cell; vertical-align: top; padding-left: 8px; padding-top: 4px;
                            padding-right: 10px !important;">
                            <asp:TextBox TabIndex="1" Width="100px" CssClass="input-text maskdate" runat="server"
                                ID="txtFromDate">
                            </asp:TextBox>
                            <asp:RequiredFieldValidator ValidationGroup="a" EnableClientScript="false" ErrorMessage="*"
                                ID="RequiredFieldValidator2" Display="Dynamic" runat="server" ControlToValidate="txtFromDate"></asp:RequiredFieldValidator>
                            <asp:CompareValidator ValidationGroup="a" EnableClientScript="false" ErrorMessage="*"
                                ID="CompareValidator1" runat="server" Display="Dynamic" ControlToValidate="txtFromDate"
                                Operator="DataTypeCheck" Type="Date"></asp:CompareValidator>
                        </div>
                        <div style="display: table-cell; vertical-align: top; padding-top: 8px;">
                            <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="Date : ">
                            </dx:ASPxLabel>
                        </div>
                        <div style="display: table-cell; padding-top: 4px; padding-left: 10px !important;
                            padding-right: 10px !important;">
                            <asp:TextBox TabIndex="2" runat="server" ID="txtToDate" Width="100px" CssClass="input-text maskdate">
                            </asp:TextBox>
                            <asp:RequiredFieldValidator ValidationGroup="a" EnableClientScript="false" ErrorMessage="*"
                                ID="RequiredFieldValidator1" Display="Dynamic" runat="server" ControlToValidate="txtToDate"></asp:RequiredFieldValidator>
                            <asp:CompareValidator ValidationGroup="a" EnableClientScript="false" ErrorMessage="*"
                                ID="CompareValidator10" runat="server" Display="Dynamic" ControlToValidate="txtToDate"
                                Operator="DataTypeCheck" Type="Date"></asp:CompareValidator>
                        </div>
                        <div>
                                <asp:Button ID="openingbal0" runat="server" CssClass="GreenyPushButton" OnClick="openingbal_Click1" TabIndex="3" Text="Opening Balance" />
                             &nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button TabIndex="3" ID="openingbal" runat="server" OnClick="openingbal_Click"
                                CssClass="GreenyPushButton" Text="Closing Balance"></asp:Button>
                        </div>
                       <%-- <div style="display: table-cell; vertical-align: top;">
                            <asp:Button TabIndex="3" ID="openingbal" runat="server" OnClick="openingbal_Click"
                                CssClass="GreenyPushButton" Text="Opening Balance"></asp:Button>
                        </div>--%>
                    </asp:Panel>
                    <table class="tableBorder">
                        <tr>
                            <th>
                                Sl. No.
                            </th>
                            <th>
                                Report
                            </th>
                            <th>
                            </th>
                            <th>
                            </th>
                            <th>
                            </th>
                        </tr>
                        <tr>
                            <td>
                                1.
                            </td>
                            <td>
                                12 Heads
                            </td>
                            <td>
                                <asp:Button CommandName="12Heads" ID="btn12HeadsBind" runat="server" OnClick="Bind_click"
                                    CssClass="GreenyPushButton" Text="Bind"></asp:Button>
                            </td>
                            <td>
                                <asp:Button CommandName="12Heads" ID="btn12HeadsExport" runat="server" OnClick="Export_click"
                                    CssClass="GreenyPushButton" Text="Export"></asp:Button>
                                <asp:HiddenField runat="server" ID="hf12Heads" />
                            </td>
                            <td>
                                   
                                 <asp:Button CommandName="12Heads" ID="btn12ExportExcel" runat="server" OnClick="btnExportExcel_Click1"
                                    CssClass="GreenyPushButton" Text="ExportExcel"></asp:Button>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                2.
                            </td>
                            <td>
                                Trial Balance of Branches
                            </td>
                            <td>
                                <asp:Button CommandName="Branches" ID="btnBranchesBind" runat="server" OnClick="Bind_click"
                                    CssClass="GreenyPushButton" Text="Bind"></asp:Button>
                            </td>
                            <td>
                                <asp:Button CommandName="Branches" ID="btnBranchesExport" runat="server" OnClick="Export_click"
                                    CssClass="GreenyPushButton" Text="Export"></asp:Button>
                                <asp:HiddenField runat="server" ID="hfBranches" />
                            </td>
                              <td>
                                   
                                  <asp:Button CommandName="Branches" ID="btnBranchesExport1" runat="server" OnClick="btnExportExcel_Click2"
                                    CssClass="GreenyPushButton" Text="ExportExcel"></asp:Button>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                3.
                            </td>
                            <td>
                                Investments
                            </td>
                            <td>
                                <asp:Button CommandName="Investments" ID="btnBindInvestments" runat="server" OnClick="Bind_click"
                                    CssClass="GreenyPushButton" Text="Bind"></asp:Button>
                            </td>
                            <td>
                                <asp:Button CommandName="Investments" ID="btnExportInvestments" runat="server" OnClick="Export_click"
                                    CssClass="GreenyPushButton" Text="Export"></asp:Button>
                                <asp:HiddenField runat="server" ID="hfInvestments" />
                            </td>
                            <td>
                             
                         <asp:Button CommandName="Investments" ID="btnExportInvestmentsexcel" runat="server" OnClick="btnExportinvesExcel_Click"
                                    CssClass="GreenyPushButton" Text="ExportExcel"></asp:Button>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                4.
                            </td>
                            <td>
                                Trial Balance of Banks
                            </td>
                            <td>
                                <asp:Button CommandName="Banks" ID="btnBindBanks" runat="server" OnClick="Bind_click"
                                    CssClass="GreenyPushButton" Text="Bind"></asp:Button>
                            </td>
                            <td>
                                <asp:Button CommandName="Banks" ID="btnExportBanks" runat="server" OnClick="Export_click"
                                    CssClass="GreenyPushButton" Text="Export"></asp:Button>
                                <asp:HiddenField runat="server" ID="hfBanks" />
                            </td>
                             <td>
                                <%--      <dx:ASPxButton ID="btnExportExcelbank" Text="ExportExcel"  runat="server" Visible="true"
                            ImageSpacing="0px" EnableTheming="False" EnableDefaultAppearance="True" ToolTip="Export to Excel"
                              Cursor="pointer" OnClick="btnExportbankExcel_Click"  CssClass="GreenyPushButton">
                        </dx:ASPxButton>--%>
                                  <asp:Button CommandName="Banks" ID="btnExportBanksexcel" runat="server" OnClick="btnExportbankExcel_Click"
                                    CssClass="GreenyPushButton" Text="ExportExcel"></asp:Button>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                5.
                            </td>
                            <td>
                                Trial Balance of Other Items
                            </td>
                            <td>
                                <asp:Button CommandName="OtherItems" ID="btnBindOtherItems" runat="server" OnClick="Bind_click"
                                    CssClass="GreenyPushButton" Text="Bind"></asp:Button>
                            </td>
                            <td>
                                <asp:Button CommandName="OtherItems" ID="btnExportOtherItems" runat="server" OnClick="Export_click"
                                    CssClass="GreenyPushButton" Text="Export"></asp:Button>
                                <asp:HiddenField runat="server" ID="hfOtherItems" />
                            </td>
                            <td>
                                 <%--     <dx:ASPxButton ID="btnExportExcelother" Text="ExportExcel"  runat="server" Visible="true"
                            ImageSpacing="0px" EnableTheming="False" EnableDefaultAppearance="True" ToolTip="Export to Excel"
                              Cursor="pointer" OnClick="btnExportotherExcel_Click1"  CssClass="GreenyPushButton">
                        </dx:ASPxButton>--%>
                                <asp:Button CommandName="OtherItems" ID="btnExportExcelotherExcel" runat="server" OnClick="btnExportotherExcel_Click1"
                                    CssClass="GreenyPushButton" Text="ExportExcel"></asp:Button>

                            </td>
                        </tr>
                        <tr>
                            <td>
                                6.
                            </td>
                            <td>
                                Groupwar Chit Control Statement
                            </td>
                            <td>
                                <asp:Button CommandName="GroupwarChitControlStatement" ID="btnBindGroupwarChitControlStatement"
                                    runat="server" OnClick="Bind_click" CssClass="GreenyPushButton" Text="Bind">
                                </asp:Button>
                            </td>
                            <td>
                                <asp:Button CommandName="GroupwarChitControlStatement" ID="btnExportGroupwarChitControlStatement"
                                    runat="server" OnClick="Export_click" CssClass="GreenyPushButton" Text="Export">
                                </asp:Button>
                                <asp:HiddenField runat="server" ID="hfGroupwarChitControlStatement" />
                            </td>
                             <td>
                            <%--          <dx:ASPxButton ID="btnExportExcelgroupwar" Text="ExportExcel"  runat="server" Visible="true"
                            ImageSpacing="0px" EnableTheming="False" EnableDefaultAppearance="True" ToolTip="Export to Excel"
                              Cursor="pointer" OnClick="btnExportgroupwarExcel_Click"  CssClass="GreenyPushButton">
                        </dx:ASPxButton>--%>
                                 <asp:Button CommandName="GroupwarChitControlStatement" ID="btnExportGroupwarChitControlStatementexcel"
                                    runat="server" OnClick="btnExportgroupwarExcel_Click" CssClass="GreenyPushButton" Text="ExportExcel">
                                </asp:Button>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                7.
                            </td>
                            <td>
                                Groupwar Chit Trial Statement
                            </td>
                            <td>
                                <asp:Button ID="btnBindTerChitTrial" CommandName="GroupwarChitTrialStatement" runat="server"
                                    OnClick="Bind_click" CssClass="GreenyPushButton" Text="Bind"></asp:Button>
                            </td>
                            <td>
                                <asp:Button CommandName="GroupwarChitTrialStatement" ID="btnExportTerChitTrial" runat="server"
                                    OnClick="Export_click" CssClass="GreenyPushButton" Text="Export"></asp:Button>
                                <asp:HiddenField runat="server" ID="hfGroupwarChitTrialStatement" />
                            </td>
                             <td>
                                    <%--  <dx:ASPxButton ID="btnExportExcelchit" Text="ExportExcel"  runat="server" Visible="true"
                            ImageSpacing="0px" EnableTheming="False" EnableDefaultAppearance="True" ToolTip="Export to Excel"
                              Cursor="pointer" OnClick="btnExportchitExcel_Click"  CssClass="GreenyPushButton">
                        </dx:ASPxButton>--%>
                                  <asp:Button CommandName="GroupwarChitTrialStatement" ID="btnExportTerChitTrialExcel" runat="server"
                                    OnClick="btnExportchitExcel_Click" CssClass="GreenyPushButton" Text="ExportExcel"></asp:Button>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                8.
                            </td>
                            <td>
                                Groupwar Excess Remittance and Arrears
                            </td>
                            <td>
                                <asp:Button CommandName="GroupwarTerminatedExcessRemittanceandArrears" ID="btnBindExcessTerminated"
                                    runat="server" OnClick="Bind_click" CssClass="GreenyPushButton" Text="Bind">
                                </asp:Button>
                            </td>
                            <td>
                                <asp:Button CommandName="GroupwarTerminatedExcessRemittanceandArrears" ID="btnExportExcessTerminated"
                                    runat="server" OnClick="Export_click" CssClass="GreenyPushButton" Text="Export">
                                </asp:Button>
                                <asp:HiddenField runat="server" ID="hfTerminatedExcess" />
                            </td>
                             <td>
                               <%--       <dx:ASPxButton ID="btnExportExcelexcess" Text="ExportExcel"  runat="server" Visible="true"
                            ImageSpacing="0px" EnableTheming="False" EnableDefaultAppearance="True" ToolTip="Export to Excel"
                              Cursor="pointer" OnClick="btnExportexcessExcel_Click"  CssClass="GreenyPushButton">
                        </dx:ASPxButton>--%>
                                 <asp:Button CommandName="GroupwarTerminatedExcessRemittanceandArrears" ID="btnExportExcessTerminatedExcel"
                                    runat="server" OnClick="btnExportexcessExcel_Click" CssClass="GreenyPushButton" Text="ExportExcel">
                                </asp:Button>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                9.
                            </td>
                            <td>
                                Outstanding and Unpaid Prize money Details
                            </td>
                            <td>
                                <asp:Button CommandName="OutstandingandUnpaidPrizemoneyDetails" ID="btnBindOutstanding"
                                    runat="server" OnClick="Bind_click" CssClass="GreenyPushButton" Text="Bind">
                                </asp:Button>
                            </td>
                            <td>
                                <asp:Button CommandName="OutstandingandUnpaidPrizemoneyDetails" ID="btnExportOutstanding"
                                    runat="server" OnClick="Export_click" CssClass="GreenyPushButton" Text="Export">
                                </asp:Button>
                                <asp:HiddenField runat="server" ID="hfOutstanding" />
                            </td>
                             <td>
                           <%--           <dx:ASPxButton ID="btnExportExcelunpaid" Text="ExportExcel"  runat="server" Visible="true"
                            ImageSpacing="0px" EnableTheming="False" EnableDefaultAppearance="True" ToolTip="Export to Excel"
                              Cursor="pointer" OnClick="btnExportunpaidExcel_Click"  CssClass="GreenyPushButton">
                        </dx:ASPxButton>--%>
                                  <asp:Button CommandName="OutstandingandUnpaidPrizemoneyDetails" ID="btnExportExcelunpaidExcel"
                                    runat="server" OnClick="btnExportunpaidExcel_Click" CssClass="GreenyPushButton" Text="ExportExcel">
                                </asp:Button>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                10.
                            </td>
                            <td>
                                Foreman Chits and Foreman Substitued Chits
                            </td>
                            <td>
                                <asp:Button ID="btnBindForeman" CommandName="ForemanChits" runat="server" OnClick="Bind_click"
                                    CssClass="GreenyPushButton" Text="Bind"></asp:Button>
                            </td>
                            <td>
                                <asp:Button CommandName="ForemanChits" ID="btnExportForeman" runat="server" OnClick="Export_click"
                                    CssClass="GreenyPushButton" Text="Export"></asp:Button>
                                <asp:HiddenField runat="server" ID="hfForeman" />
                            </td>
                             <td>
                                <%--      <dx:ASPxButton ID="btnExportExcelforman" Text="ExportExcel"  runat="server" Visible="true"
                            ImageSpacing="0px" EnableTheming="False" EnableDefaultAppearance="True" ToolTip="Export to Excel"
                              Cursor="pointer" OnClick="btnExportformanExcel_Click"  CssClass="GreenyPushButton">
                        </dx:ASPxButton>--%>
                                 <asp:Button CommandName="ForemanChits" ID="btnExportExcelformanExcel" runat="server" OnClick="btnExportformanExcel_Click"
                                    CssClass="GreenyPushButton" Text="ExportExcel"></asp:Button>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                11.
                            </td>
                            <td>
                                Loans
                            </td>
                            <td>
                                <asp:Button ID="btnBindLoans" CommandName="Loans" runat="server" OnClick="Bind_click"
                                    CssClass="GreenyPushButton" Text="Bind"></asp:Button>
                            </td>
                            <td>
                                <asp:Button ID="btnExportLoans" runat="server" CommandName="Loans" OnClick="Export_click"
                                    CssClass="GreenyPushButton" Text="Export"></asp:Button>
                                <asp:HiddenField runat="server" ID="hfLoans" />
                            </td>
                             <td>
                             <%--         <dx:ASPxButton ID="btnExportExcelloan" Text="ExportExcel"  runat="server" Visible="true"
                            ImageSpacing="0px" EnableTheming="False" EnableDefaultAppearance="True" ToolTip="Export to Excel"
                              Cursor="pointer" OnClick="btnExportloanExcel_Click"  CssClass="GreenyPushButton">
                        </dx:ASPxButton>--%>
                                   <asp:Button ID="btnExportExcelloanExcel" runat="server" CommandName="Loans" OnClick="btnExportloanExcel_Click"
                                    CssClass="GreenyPushButton" Text="ExportExcel"></asp:Button>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                12.
                            </td>
                            <td>
                                Sundries and Advances
                            </td>
                            <td>
                                <asp:Button ID="btnBindAdvances" CommandName="Advances" runat="server" OnClick="Bind_click"
                                    CssClass="GreenyPushButton" Text="Bind"></asp:Button>
                            </td>
                            <td>
                                <asp:Button ID="btnExportAdvances" runat="server" CommandName="Advances" OnClick="Export_click"
                                    CssClass="GreenyPushButton" Text="Export"></asp:Button>
                                <asp:HiddenField runat="server" ID="hfAdvances" />
                            </td>
                            <td>
                                  <%--    <dx:ASPxButton ID="btnExportsundriesExcel" Text="ExportExcel"  runat="server" Visible="true"
                            ImageSpacing="0px" EnableTheming="False" EnableDefaultAppearance="True" ToolTip="Export to Excel"
                              Cursor="pointer" OnClick="btnExportsundriesExcel_Click"  CssClass="GreenyPushButton">
                        </dx:ASPxButton>--%>
                                 <asp:Button ID="btnExportAdvancesExcel" runat="server" CommandName="Advances" OnClick="btnExportsundriesExcel_Click"
                                    CssClass="GreenyPushButton" Text="ExportExcel"></asp:Button>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                13.
                            </td>
                            <td>
                                Profit and Loss
                            </td>
                            <td>
                                <asp:Button ID="btnBindProfitCredit" runat="server" CommandName="pandlcredit" OnClick="Bind_click"
                                    CssClass="GreenyPushButton" Text="Bind"></asp:Button>
                            </td>
                            <td>
                                <asp:Button ID="btnExportProfitCredit" runat="server" CommandName="pandlcredit" OnClick="Export_click"
                                    CssClass="GreenyPushButton" Text="Export"></asp:Button>
                                <asp:HiddenField runat="server" ID="hfProfitCredit" />
                            </td>
                            <td>
                            <%--          <dx:ASPxButton ID="btnExportExcelprofit" Text="ExportExcel"  runat="server" Visible="true"
                            ImageSpacing="0px" EnableTheming="False" EnableDefaultAppearance="True" ToolTip="Export to Excel"
                              Cursor="pointer" OnClick="btnExportprofitExcel_Click"  CssClass="GreenyPushButton">
                        </dx:ASPxButton>--%>
                                 <asp:Button ID="btnExportExcelprofitExcel" runat="server" CommandName="pandlcredit" OnClick="btnExportprofitExcel_Click"
                                    CssClass="GreenyPushButton" Text="ExportExcel"></asp:Button>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                14.
                            </td>
                            <td>
                                Particulars of Chit Prize Money
                            </td>
                            <td>
                                <asp:Button ID="btnBindPayment" runat="server" CommandName="ParticularsofChitPrizeMoneyPaid"
                                    OnClick="Bind_click" CssClass="GreenyPushButton" Text="Bind"></asp:Button>
                            </td>
                            <td>
                                <asp:Button CommandName="ParticularsofChitPrizeMoneyPaid" ID="btnExportPayment" runat="server"
                                    OnClick="Export_click" CssClass="GreenyPushButton" Text="Export"></asp:Button>
                                <asp:HiddenField runat="server" ID="hfPayment" />
                            </td>
                            <td>
                                 <%--     <dx:ASPxButton ID="btnBindPaymentExcel" Text="ExportExcel"  runat="server" Visible="true"
                            ImageSpacing="0px" EnableTheming="False" EnableDefaultAppearance="True" ToolTip="Export to Excel"
                              Cursor="pointer" OnClick="btnExportExcel_Click"   CssClass="GreenyPushButton">
                        </dx:ASPxButton>--%>
                                <asp:Button CommandName="ParticularsofChitPrizeMoneyPaid" ID="btnExportPaymentExcel" runat="server"
                                    OnClick="btnExportExcel_Click" CssClass="GreenyPushButton" Text="ExportExcel"></asp:Button>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                15.
                            </td>
                            <td>
                                Registration of PSO and Chit Agreement
                            </td>
                            <td>
                                <asp:Button CommandName="RegistrationofPSOandChitAgreement" ID="btnBindRegistrationofPSOandChitAgreement"
                                    runat="server" OnClick="Bind_click" CssClass="GreenyPushButton" Text="Bind">
                                </asp:Button>
                            </td>
                            <td>
                                <asp:Button CommandName="RegistrationofPSOandChitAgreement" ID="btnExportRegistrationofPSOandChitAgreement"
                                    runat="server" OnClick="Export_click" CssClass="GreenyPushButton" Text="Export">
                                </asp:Button>
                                <asp:HiddenField runat="server" ID="hfRegistrationofPSOandChitAgreement" />
                            </td>
                            <td>
                                  <%--    <dx:ASPxButton ID="btnExportpsoExcel" Text="ExportExcel"  runat="server" Visible="true"
                            ImageSpacing="0px" EnableTheming="False" EnableDefaultAppearance="True" ToolTip="Export to Excel"
                              Cursor="pointer" OnClick="btnExportpsoExcel_Click"  CssClass="GreenyPushButton">
                        </dx:ASPxButton>--%>
                                 <asp:Button CommandName="RegistrationofPSOandChitAgreement" ID="btnExportRegistrationofPSOandChitAgreementExcel"
                                    runat="server" OnClick="btnExportpsoExcel_Click" CssClass="GreenyPushButton" Text="ExportExcel">
                                </asp:Button>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                16.
                            </td>
                            <td>
                                Chit Drawals and Commision Particulars
                            </td>
                            <td>
                                <asp:Button CommandName="ChitDrawalsandCommisionParticulars" ID="btnBindChitDrawalsandCommisionParticulars"
                                    runat="server" OnClick="Bind_click" CssClass="GreenyPushButton" Text="Bind">
                                </asp:Button>
                            </td>
                            <td>
                                <asp:Button CommandName="ChitDrawalsandCommisionParticulars" ID="btnExportChitDrawalsandCommisionParticulars"
                                    runat="server" OnClick="Export_click" CssClass="GreenyPushButton" Text="Export">
                                </asp:Button>
                                <asp:HiddenField runat="server" ID="hfChitDrawalsandCommisionParticulars" />
                            </td>
                             <td>
                                <%--      <dx:ASPxButton ID="btnExportExceldrawels" Text="ExportExcel"  runat="server" Visible="true"
                            ImageSpacing="0px" EnableTheming="False" EnableDefaultAppearance="True" ToolTip="Export to Excel"
                              Cursor="pointer" OnClick="btnExportdrawelsExcel_Click"  CssClass="GreenyPushButton">
                        </dx:ASPxButton>--%>
                                 <asp:Button CommandName="ChitDrawalsandCommisionParticulars" ID="btnExportChitDrawalsandCommisionParticularsExcel"
                                    runat="server" OnClick="btnExportdrawelsExcel_Click" CssClass="GreenyPushButton" Text="ExportExcel">
                                </asp:Button>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                17.
                            </td>
                            <td>
                                Particulars of Amount at Credit
                            </td>
                            <td>
                                <asp:Button CommandName="ParticularsofAmountatCredit" ID="btnBindRCM" runat="server"
                                    OnClick="Bind_click" CssClass="GreenyPushButton" Text="Bind"></asp:Button>
                            </td>
                            <td>
                                <asp:Button CommandName="ParticularsofAmountatCredit" ID="btnExportRCM" runat="server"
                                    OnClick="Export_click" CssClass="GreenyPushButton" Text="Export"></asp:Button>
                                <asp:HiddenField runat="server" ID="hfRCM" />
                            </td>
                             <td>
                                  <%--    <dx:ASPxButton ID="btnExportExcelrcm" Text="ExportExcel"  runat="server" Visible="true"
                            ImageSpacing="0px" EnableTheming="False" EnableDefaultAppearance="True" ToolTip="Export to Excel"
                              Cursor="pointer" OnClick="btnExportrcmExcel_Click1"  CssClass="GreenyPushButton">
                        </dx:ASPxButton>--%>
                                 <asp:Button CommandName="ParticularsofAmountatCredit" ID="btnExportExcelrcmExcel" runat="server"
                                    OnClick="btnExportrcmExcel_Click1" CssClass="GreenyPushButton" Text="ExportExcel"></asp:Button>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                18.
                            </td>
                            <td>
                                Filing Document
                            </td>
                            <td>
                                <asp:Button CommandName="FilingDocument" ID="btnFilingDocument" runat="server"
                                    OnClick="Bind_click" CssClass="GreenyPushButton" Text="Bind"></asp:Button>
                            </td>
                            <td>
                                <asp:Button CommandName="FilingDocument" ID="btnExportFilingDocument" runat="server"
                                    OnClick="Export_click" CssClass="GreenyPushButton" Text="Export"></asp:Button>
                                <asp:HiddenField runat="server" ID="hfFilingDocument" />
                            </td>
                             <td>
                                      <%--<dx:ASPxButton ID="btnExportExcelFilling" Text="ExportExcel"  runat="server" Visible="true"
                            ImageSpacing="0px" EnableTheming="False" EnableDefaultAppearance="True" ToolTip="Export to Excel"
                              Cursor="pointer" OnClick="btnExportFillingExcel_Click"  CssClass="GreenyPushButton">
                        </dx:ASPxButton>--%>
                                  <asp:Button CommandName="FilingDocument" ID="btnExportExcelFillingExcel" runat="server"
                                    OnClick="btnExportFillingExcel_Click" CssClass="GreenyPushButton" Text="ExportExcel"></asp:Button>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                19.
                            </td>
                            <td>
                                Deposits
                            </td>
                            <td>
                                <asp:Button CommandName="Deposit" ID="btnDeposit" runat="server"
                                    OnClick="Bind_click" CssClass="GreenyPushButton" Text="Bind"></asp:Button>
                            </td>
                            <td>
                                <asp:Button CommandName="Deposit" ID="btnExportDeposit" runat="server"
                                    OnClick="Export_click" CssClass="GreenyPushButton" Text="Export"></asp:Button>
                                <asp:HiddenField runat="server" ID="hfDeposit" />
                            </td>
                            <td>
                               <%--       <dx:ASPxButton ID="btnExportExcelDeposit" Text="ExportExcel"  runat="server" Visible="true"
                            ImageSpacing="0px" EnableTheming="False" EnableDefaultAppearance="True" ToolTip="Export to Excel"
                              Cursor="pointer" OnClick="btnExportDepositExcel_Click"  CssClass="GreenyPushButton">
                        </dx:ASPxButton>--%>
                                <asp:Button CommandName="Deposit" ID="btnExportExcelDepositExcel" runat="server"
                                    OnClick="btnExportDepositExcel_Click" CssClass="GreenyPushButton" Text="ExportExcel"></asp:Button>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                20.
                            </td>
                            <td class="tdLeft">
                                Stamps
                            </td>
                            <td>
                                <asp:Button CommandName="Stamps" ID="btnStamps" runat="server" OnClick="Bind_click"
                                    CssClass="GreenyPushButton" Text="Bind"></asp:Button>
                            </td>
                            <td>
                                <asp:Button CommandName="Stamps" ID="btnExportStamps" runat="server" OnClick="Export_click"
                                    CssClass="GreenyPushButton" Text="Export"></asp:Button>
                                <asp:HiddenField runat="server" ID="hfStamps" />
                            </td>
                             <td>
                                   <%--   <dx:ASPxButton ID="btnExportExcelstamp" Text="ExportExcel"  runat="server" Visible="true"
                            ImageSpacing="0px" EnableTheming="False" EnableDefaultAppearance="True" ToolTip="Export to Excel"
                              Cursor="pointer" OnClick="btnExportStampExcel_Click"  CssClass="GreenyPushButton">
                        </dx:ASPxButton>--%>
                           <asp:Button CommandName="Stamps" ID="btnExportExcelstampExcel" runat="server" OnClick="btnExportStampExcel_Click"
                                    CssClass="GreenyPushButton" Text="ExportExcel"></asp:Button>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                21.
                            </td>
                            <td class="tdLeft">
                                Decree
                            </td>
                            <td>
                                <asp:Button CommandName="Decree" ID="btnDecree" runat="server" OnClick="Bind_click"
                                    CssClass="GreenyPushButton" Text="Bind"></asp:Button>
                            </td>
                            <td>
                                <asp:Button CommandName="Decree" ID="btnExportDecree" runat="server" OnClick="Export_click"
                                    CssClass="GreenyPushButton" Text="Export"></asp:Button>
                                <asp:HiddenField runat="server" ID="hfDecree" />
                            </td>
                             <td>
                               <%--       <dx:ASPxButton ID="btnExportExcelDecree" Text="ExportExcel"  runat="server" Visible="true"
                            ImageSpacing="0px" EnableTheming="False" EnableDefaultAppearance="True" ToolTip="Export to Excel"
                              Cursor="pointer" OnClick="btnExportDecreeExcel_Click"  CssClass="GreenyPushButton">
                        </dx:ASPxButton>--%>
                                 <asp:Button CommandName="Decree" ID="btnExportDecreeExcel" runat="server" OnClick="btnExportDecreeExcel_Click"
                                    CssClass="GreenyPushButton" Text="ExportExcel"></asp:Button>
                            </td>
                        </tr>
                          <tr>
                            <td>
                                22.
                            </td>
                            <td class="tdLeft">
                               	Particulars Of Emoluments And BusinessPerformance
                            </td>
                            <td>
                                <asp:Button CommandName="salary" ID="btnBusinessPerformance" runat="server" OnClick="Bind_click"
                                    CssClass="GreenyPushButton" Text="Bind"></asp:Button>
                            </td>
                            <td>
                                <asp:Button CommandName="salary" ID="btnExportBusinessPerformance" runat="server" OnClick="Export_click"
                                    CssClass="GreenyPushButton" Text="Export"></asp:Button>
                                <asp:HiddenField runat="server" ID="hfBusinessPerformance" />
                            </td>
                              <td>
                                 <%--     <dx:ASPxButton ID="btnExportExcelBusinessPerformance" Text="ExportExcel"  runat="server" Visible="true"
                            ImageSpacing="0px" EnableTheming="False" EnableDefaultAppearance="True" ToolTip="Export to Excel"
                              Cursor="pointer" OnClick="btnExportBusinessPerformanceExcel_Click1" CssClass="GreenyPushButton">
                        </dx:ASPxButton>--%>
                                   <asp:Button CommandName="salary" ID="btnExportBusinessPerformanceExcel" runat="server" OnClick="btnExportBusinessPerformanceExcel_Click1"
                                    CssClass="GreenyPushButton" Text="ExportExcel"></asp:Button>
                            </td>
                        </tr>
                          <tr>
                            <td>
                                23.
                            </td>
                            <td class="tdLeft">
                               	Particulars Of Salary Allowances and Deduction
                            </td>
                            <td>
                                <asp:Button CommandName="salary2" ID="btnsalaryAD" runat="server" OnClick="Bind_click"
                                    CssClass="GreenyPushButton" Text="Bind"></asp:Button>
                            </td>
                            <td>
                                <asp:Button CommandName="salary2" ID="btnExportsalaryAD" runat="server" OnClick="Export_click"
                                    CssClass="GreenyPushButton" Text="Export"></asp:Button>
                                <asp:HiddenField runat="server" ID="hfsalaryAD" />
                            </td>
                               <td>
                                  <%--    <dx:ASPxButton ID="btnExportExcelsalaryAD" Text="ExportExcel"  runat="server" Visible="true"
                            ImageSpacing="0px" EnableTheming="False" EnableDefaultAppearance="True" ToolTip="Export to Excel"
                              Cursor="pointer" OnClick="btnExportsalaryADExcel_Click1" CssClass="GreenyPushButton">
                        </dx:ASPxButton>--%>
                                   <asp:Button CommandName="salary2" ID="btnExportsalaryADExcel" runat="server" OnClick="btnExportsalaryADExcel_Click1"
                                    CssClass="GreenyPushButton" Text="ExportExcel"></asp:Button>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                24.
                            </td>
                            <td class="tdLeft">
                                BPP For Current Branch
                            </td>
                            <td>
                                <asp:Button CommandName="BPPCurrent" ID="btnBpp" runat="server" OnClick="Bind_click"
                                    CssClass="GreenyPushButton" Text="Bind"></asp:Button>
                            </td>
                            <td>
                                <asp:Button CommandName="BPPCurrent" ID="btnExportBpp" runat="server" OnClick="Export_click"
                                    CssClass="GreenyPushButton" Text="Export"></asp:Button>
                                <asp:HiddenField runat="server" ID="hfBpp" />
                            </td>
                             <td>
                               <%--       <dx:ASPxButton ID="btnExportExcelBpp" Text="ExportExcel"  runat="server" Visible="true"
                            ImageSpacing="0px" EnableTheming="False" EnableDefaultAppearance="True" ToolTip="Export to Excel"
                              Cursor="pointer" OnClick="btnExportBppExcel_Click1" CssClass="GreenyPushButton">
                        </dx:ASPxButton>--%>
                                  <asp:Button CommandName="BPPCurrent" ID="btnExportBppExcel" runat="server" OnClick="btnExportBppExcel_Click1"
                                    CssClass="GreenyPushButton" Text="ExportExcel"></asp:Button>
                            </td>
                        </tr>
                          <tr>
                            <td>
                                25.
                            </td>
                            <td class="tdLeft">
                                BPP For M Chits and Other Branches
                            </td>
                            <td>
                                <asp:Button CommandName="BPPChits" ID="btnBppchit" runat="server" OnClick="Bind_click"
                                    CssClass="GreenyPushButton" Text="Bind"></asp:Button>
                            </td>
                            <td>
                                <asp:Button CommandName="BPPChits" ID="btnExportBppchit" runat="server" OnClick="Export_click"
                                    CssClass="GreenyPushButton" Text="Export"></asp:Button>
                                <asp:HiddenField runat="server" ID="hfBppchit" />
                            </td>
                                <td>
                                 <%--     <dx:ASPxButton ID="btnExportExcelBppchit" Text="ExportExcel"  runat="server" Visible="true"
                            ImageSpacing="0px" EnableTheming="False" EnableDefaultAppearance="True" ToolTip="Export to Excel"
                              Cursor="pointer" OnClick="btnExportBppchitExcel_Click" CssClass="GreenyPushButton">
                        </dx:ASPxButton>--%>
                                     <asp:Button CommandName="BPPChits" ID="btnExportBppchitExcel" runat="server" OnClick="btnExportBppchitExcel_Click"
                                    CssClass="GreenyPushButton" Text="ExportExcel"></asp:Button>
                            </td>
                        </tr>
                    </table>
                     <div class="row">
                        <dx:ASPxGridView AutoGenerateColumns="false" Style="margin: 0 auto; width: 100%;"
                            ID="gridTwleveHeads" ClientInstanceName="gridTwleveHeads" runat="server">
                            <Settings ShowTitlePanel="true" ShowFooter="true" ShowFilterBar="Hidden" ShowFilterRow="false" />
                            <SettingsText Title="12 Heads" />
                            <TotalSummary>
                                <dx:ASPxSummaryItem FieldName="Credit" DisplayFormat="{0}" SummaryType="Sum" />
                                <dx:ASPxSummaryItem FieldName="Debit" DisplayFormat="{0}" SummaryType="Sum" />
                                <dx:ASPxSummaryItem FieldName="Heads" DisplayFormat="TOTAL" SummaryType="Sum" />
                            </TotalSummary>
                            <SettingsPager Mode="ShowAllRecords">
                            </SettingsPager>
                            <Styles Footer-HorizontalAlign="Right" Cell-HorizontalAlign="Right" Header-VerticalAlign="Middle"
                                Header-HorizontalAlign="Center" Header-Wrap="True">
                            </Styles>
                            <Columns>
                                <dx:GridViewDataColumn Width="10%" FieldName="SNo" Caption="S.No." CellStyle-HorizontalAlign="Right">
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn Width="30%" FieldName="Heads" FooterCellStyle-HorizontalAlign="Right"
                                    CellStyle-HorizontalAlign="Left">
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn Width="25%" FieldName="Credit" FooterCellStyle-HorizontalAlign="Right"
                                    CellStyle-HorizontalAlign="Right">
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn Width="25%" FieldName="Debit" FooterCellStyle-HorizontalAlign="Right"
                                    CellStyle-HorizontalAlign="Right">
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn Width="20%" FieldName="Remarks" FooterCellStyle-HorizontalAlign="Right"
                                    CellStyle-HorizontalAlign="Right">
                                </dx:GridViewDataColumn>
                            </Columns>
                        </dx:ASPxGridView>
                        <dx:ASPxGridViewExporter ID="exportTwleveHeads" runat="server" GridViewID="gridTwleveHeads">
                            <Styles>
                                <Title Paddings-PaddingBottom="5px" Paddings-PaddingTop="5px"></Title>
                                <Header Wrap="True" Paddings-PaddingBottom="5px" Paddings-PaddingTop="5px" HorizontalAlign="Center">
                                </Header>
                                <Cell Paddings-Padding="5px" Wrap="True">
                                </Cell>
                                <Footer Paddings-Padding="5px" Wrap="True">
                                </Footer>
                            </Styles>
                        </dx:ASPxGridViewExporter>
                          <dx:ASPxGridViewExporter ID="gridTwleveHeadsexcel" GridViewID="gridTwleveHeads" 

    PaperKind="A4" Landscape="True"  runat="server" >
</dx:ASPxGridViewExporter>
                    </div>
                    <div class="row">
                        <dx:ASPxGridView AutoGenerateColumns="false" Style="margin: 0 auto; width: 100%;"
                            ID="gridBranches" ClientInstanceName="gridBranches" runat="server">
                            <Settings ShowTitlePanel="true" ShowFooter="true" ShowFilterBar="Hidden" ShowFilterRow="false" />
                            <SettingsText Title="Trial Balance Of Branches" />
                            <SettingsPager Mode="ShowAllRecords">
                            </SettingsPager>
                            <Styles Footer-HorizontalAlign="Right" Cell-HorizontalAlign="Right" Header-VerticalAlign="Middle"
                                Header-HorizontalAlign="Center" Header-Wrap="True">
                            </Styles>
                            <Columns>
                                <dx:GridViewDataColumn Width="10%" ExportWidth="30" FieldName="SNo" Caption="S.No." CellStyle-HorizontalAlign="Right">
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn Width="30%" ExportWidth="120" FieldName="Heads" Caption="Name of Branch" FooterCellStyle-HorizontalAlign="Right"
                                    CellStyle-HorizontalAlign="Left">
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn Width="25%" ExportWidth="60" FieldName="Credit" FooterCellStyle-HorizontalAlign="Right"
                                    CellStyle-HorizontalAlign="Right">
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn Width="25%" ExportWidth="60" FieldName="Debit" FooterCellStyle-HorizontalAlign="Right"
                                    CellStyle-HorizontalAlign="Right">
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn Width="20%" ExportWidth="50" FieldName="Remarks" FooterCellStyle-HorizontalAlign="Right"
                                    CellStyle-HorizontalAlign="Right">
                                </dx:GridViewDataColumn>
                            </Columns>
                        </dx:ASPxGridView>
                        <dx:ASPxGridViewExporter ID="exportBranches"  runat="server" GridViewID="gridBranches">
                            <Styles>
                                <Title Paddings-PaddingBottom="5px" Font-Size="Smaller" Paddings-PaddingTop="5px"></Title>
                                <Header Wrap="True" Paddings-PaddingBottom="3px" Font-Size="Smaller" Paddings-PaddingTop="3px" HorizontalAlign="Center">
                                </Header>
                                <Cell Paddings-Padding="4px" Font-Size="Smaller" Wrap="True">
                                </Cell>
                                <Footer Paddings-Padding="4px" Font-Size="Smaller" Wrap="True">
                                </Footer>
                                
                            </Styles>
                        </dx:ASPxGridViewExporter>
                         <dx:ASPxGridViewExporter ID="gridBranchesexcel" GridViewID="gridBranches" 

    PaperKind="A4" Landscape="True"  runat="server" >
</dx:ASPxGridViewExporter>
                    </div>


                    <div class="row" id="divpal1" visible="false" runat="server">
                        <dx:ASPxGridView AutoGenerateColumns="false" Style="margin: 0 auto; width: 100%;"
                            ID="ASPxGridView1" ClientInstanceName="gridBranches" runat="server">
                            <Settings ShowTitlePanel="true" ShowFooter="false" ShowFilterBar="Hidden" ShowFilterRow="false" />
                            <SettingsText Title="Trial Balance Of Pallatur-I" />
                            <SettingsPager Mode="ShowAllRecords">
                            </SettingsPager>
                            <Styles Footer-HorizontalAlign="Right" Cell-HorizontalAlign="Right" Header-VerticalAlign="Middle"
                                Header-HorizontalAlign="Center" Header-Wrap="True">
                            </Styles>
                            <Columns>
                                <dx:GridViewDataColumn Width="10%" ExportWidth="30" FieldName="SNo" Caption="S.No." CellStyle-HorizontalAlign="Right">
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn Width="30%" ExportWidth="120" FieldName="Heads" Caption="Name of Branch" FooterCellStyle-HorizontalAlign="Right"
                                    CellStyle-HorizontalAlign="Left">
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn Width="25%" ExportWidth="60" FieldName="Credit" FooterCellStyle-HorizontalAlign="Right"
                                    CellStyle-HorizontalAlign="Right">
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn Width="25%" ExportWidth="60" FieldName="Debit" FooterCellStyle-HorizontalAlign="Right"
                                    CellStyle-HorizontalAlign="Right">
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn Width="20%" ExportWidth="50" FieldName="Remarks" FooterCellStyle-HorizontalAlign="Right"
                                    CellStyle-HorizontalAlign="Right">
                                </dx:GridViewDataColumn>
                            </Columns>
                        </dx:ASPxGridView>
                        <dx:ASPxGridViewExporter ID="ASPxGridViewExporter1"  runat="server" GridViewID="ASPxGridView1">
                            <Styles>
                                <Title Paddings-PaddingBottom="5px" Font-Size="Smaller" Paddings-PaddingTop="5px"></Title>
                                <Header Wrap="True" Paddings-PaddingBottom="3px" Paddings-PaddingTop="3px" HorizontalAlign="Center">
                                </Header>
                                <Cell Paddings-Padding="4px" Font-Size="Smaller" Wrap="True">
                                </Cell>
                                <Footer Paddings-Padding="4px" Font-Size="Smaller" Wrap="True">
                                </Footer>
                            </Styles>
                        </dx:ASPxGridViewExporter>
                          <dx:ASPxGridViewExporter ID="gridtriexcel" GridViewID="ASPxGridView1" 

    PaperKind="A4" Landscape="True"  runat="server" >
</dx:ASPxGridViewExporter>
                    </div>                  
                    <div class="row" id="divpal2" visible="false" runat="server">
                        <dx:ASPxGridView AutoGenerateColumns="false" Style="margin: 0 auto; width: 100%;"
                            ID="ASPxGridView2" ClientInstanceName="gridBranches" runat="server">
                            <Settings ShowTitlePanel="true" ShowFooter="false" ShowFilterBar="Hidden" ShowFilterRow="false" />
                            <SettingsText Title="Trial Balance Of Pallatur-II" />
                            <SettingsPager Mode="ShowAllRecords">
                            </SettingsPager>
                            <Styles Footer-HorizontalAlign="Right" Cell-HorizontalAlign="Right" Header-VerticalAlign="Middle"
                                Header-HorizontalAlign="Center" Header-Wrap="True">
                            </Styles>
                            <Columns>
                                <dx:GridViewDataColumn Width="10%" ExportWidth="30" FieldName="SNo" Caption="S.No." CellStyle-HorizontalAlign="Right">
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn Width="30%" ExportWidth="120" FieldName="Heads" Caption="Name of Branch" FooterCellStyle-HorizontalAlign="Right"
                                    CellStyle-HorizontalAlign="Left">
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn Width="25%" ExportWidth="60" FieldName="Credit" FooterCellStyle-HorizontalAlign="Right"
                                    CellStyle-HorizontalAlign="Right">
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn Width="25%" ExportWidth="60" FieldName="Debit" FooterCellStyle-HorizontalAlign="Right"
                                    CellStyle-HorizontalAlign="Right">
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn Width="20%" ExportWidth="50" FieldName="Remarks" FooterCellStyle-HorizontalAlign="Right"
                                    CellStyle-HorizontalAlign="Right">
                                </dx:GridViewDataColumn>
                            </Columns>
                        </dx:ASPxGridView>
                        <dx:ASPxGridViewExporter ID="ASPxGridViewExporter2"  runat="server" GridViewID="ASPxGridView2">
                            <Styles>
                                <Title Paddings-PaddingBottom="5px" Font-Size="Smaller" Paddings-PaddingTop="5px"></Title>
                                <Header Wrap="True" Paddings-PaddingBottom="3px"  Paddings-PaddingTop="3px" HorizontalAlign="Center">
                                </Header>
                                <Cell Paddings-Padding="4px" Font-Size="Smaller" Wrap="True">
                                </Cell>
                                <Footer Paddings-Padding="4px" Font-Size="Smaller" Wrap="True">
                                </Footer>
                            </Styles>
                        </dx:ASPxGridViewExporter>
                          <dx:ASPxGridViewExporter ID="gridtribalanceex" GridViewID="ASPxGridView2" 

    PaperKind="A4" Landscape="True"  runat="server" >
</dx:ASPxGridViewExporter>

                    </div>
                    <div class="row" id="divpal3" visible="false" runat="server">
                        <dx:ASPxGridView AutoGenerateColumns="false" Style="margin: 0 auto; width: 100%;"
                            ID="ASPxGridView3" ClientInstanceName="gridBranches" runat="server">
                            <Settings ShowTitlePanel="true" ShowFooter="false" ShowFilterBar="Hidden" ShowFilterRow="false" />
                            <SettingsText Title="Trial Balance Of Pallatur-III" />
                            <SettingsPager Mode="ShowAllRecords">
                            </SettingsPager>
                            <Styles Footer-HorizontalAlign="Right" Cell-HorizontalAlign="Right" Header-VerticalAlign="Middle"
                                Header-HorizontalAlign="Center" Header-Wrap="True">
                            </Styles>
                            <Columns>
                                <dx:GridViewDataColumn Width="10%" ExportWidth="30" FieldName="SNo" Caption="S.No." CellStyle-HorizontalAlign="Right">
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn Width="30%" ExportWidth="120" FieldName="Heads" Caption="Name of Branch" FooterCellStyle-HorizontalAlign="Right"
                                    CellStyle-HorizontalAlign="Left">
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn Width="25%" ExportWidth="60" FieldName="Credit" FooterCellStyle-HorizontalAlign="Right"
                                    CellStyle-HorizontalAlign="Right">
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn Width="25%" ExportWidth="60" FieldName="Debit" FooterCellStyle-HorizontalAlign="Right"
                                    CellStyle-HorizontalAlign="Right">
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn Width="20%" ExportWidth="50" FieldName="Remarks" FooterCellStyle-HorizontalAlign="Right"
                                    CellStyle-HorizontalAlign="Right">
                                </dx:GridViewDataColumn>
                            </Columns>
                        </dx:ASPxGridView>
                        <dx:ASPxGridViewExporter ID="ASPxGridViewExporter3" PageHeader-VerticalAlignment="Center" runat="server" GridViewID="ASPxGridView3">
                            <Styles>
                                <Title Paddings-PaddingBottom="5px" Font-Size="Smaller" Paddings-PaddingTop="5px"></Title>
                                <Header Wrap="True" Paddings-PaddingBottom="3px"  Paddings-PaddingTop="3px" HorizontalAlign="Center">
                                </Header>
                                <Cell Paddings-Padding="5px"  Wrap="True">
                                </Cell>
                                <Footer Paddings-Padding="5px" Font-Size="Medium" Wrap="True">
                                </Footer>
                            </Styles>
                        </dx:ASPxGridViewExporter>
                         <dx:ASPxGridViewExporter ID="gridtrialbalanceexcel" GridViewID="ASPxGridView3" 

    PaperKind="A4" Landscape="True"  runat="server" >
</dx:ASPxGridViewExporter>
                    </div>


                    <div class="row">
                        <dx:ASPxGridView AutoGenerateColumns="false" Style="margin: 0 auto; width: 100%;"
                            ID="gridChitDrawalsandCommisionParticulars" ClientInstanceName="gridChitDrawalsandCommisionParticulars"
                            runat="server">
                            <Settings ShowTitlePanel="true" ShowFooter="true" ShowFilterBar="Hidden" ShowFilterRow="false" />
                            <SettingsText Title="Chit Drawals and Commision Particulars" />
                            <TotalSummary>
                                <dx:ASPxSummaryItem FieldName="Commision" DisplayFormat="{0}" SummaryType="Sum" />
                                <dx:ASPxSummaryItem FieldName="ChitAgreementNumber" DisplayFormat="TOTAL" SummaryType="Sum" />
                            </TotalSummary>
                            <SettingsPager Mode="ShowAllRecords">
                            </SettingsPager>
                            <Styles Footer-HorizontalAlign="Right" Cell-HorizontalAlign="Right" Header-VerticalAlign="Middle"
                                Header-HorizontalAlign="Center" Header-Wrap="True">
                            </Styles>
                            <Columns>
                                <dx:GridViewDataColumn Width="10%" FieldName="SlNo" Caption="Sl. No." ExportWidth="30" CellStyle-HorizontalAlign="Right">
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn Width="10%" FieldName="GroupNumber" ExportWidth="70" Caption="Group Number"
                                    CellStyle-HorizontalAlign="Left">
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn Width="10%" FieldName="InstalmentNumber" ExportWidth="60" Caption="Instalment No."
                                    CellStyle-HorizontalAlign="Center">
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn Width="15%" FieldName="CallAmount" ExportWidth="70" Caption="Call Amount" CellStyle-HorizontalAlign="Right">
                                </dx:GridViewDataColumn>
                                <dx:GridViewBandColumn Caption="Drawal">
                                    <Columns>
                                        <dx:GridViewDataColumn Width="10%" ExportWidth="80" FieldName="DrawalDate" Caption="Date" CellStyle-HorizontalAlign="Center">
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn Width="10%" ExportWidth="80" FieldName="DrawalTime" Caption="Time" CellStyle-HorizontalAlign="Center">
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn Width="15%" ExportWidth="90" FieldName="DrawalDay" Caption="Day" CellStyle-HorizontalAlign="Left">
                                        </dx:GridViewDataColumn>
                                    </Columns>
                                </dx:GridViewBandColumn>
                                <dx:GridViewDataColumn Width="10%" ExportWidth="90" FieldName="ChitAgreementNumber" Caption="Chit Agreement Number"
                                    FooterCellStyle-HorizontalAlign="Right" CellStyle-HorizontalAlign="Left">
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn Width="10%" ExportWidth="90" FieldName="Commision" FooterCellStyle-HorizontalAlign="Right"
                                    CellStyle-HorizontalAlign="Right">
                                </dx:GridViewDataColumn>
                            </Columns>
                        </dx:ASPxGridView>
                        <dx:ASPxGridViewExporter ID="exportChitDrawalsandCommisionParticulars" runat="server"
                            GridViewID="gridChitDrawalsandCommisionParticulars">
                            <Styles>
                                <Title Paddings-PaddingBottom="3px" Paddings-PaddingTop="3px"></Title>
                                <Header Wrap="True" Paddings-PaddingBottom="3px" Paddings-PaddingTop="3px" HorizontalAlign="Center">
                                </Header>
                                <Cell Paddings-Padding="3px" Wrap="True">
                                </Cell>
                                <Footer Paddings-Padding="3px" Wrap="True">
                                </Footer>
                            </Styles>
                        </dx:ASPxGridViewExporter>
                         <dx:ASPxGridViewExporter ID="gridChitDrawalsandCommisionParticularsexcel" GridViewID="gridChitDrawalsandCommisionParticulars" 

    PaperKind="A4" Landscape="True"  runat="server" >
</dx:ASPxGridViewExporter>
                    </div>
                    <div class="row">
                        <dx:ASPxGridView AutoGenerateColumns="false" Style="margin: 0 auto; width: 100%;"
                            ID="gridRegistrationofPSOandChitAgreement" ClientInstanceName="gridRegistrationofPSOandChitAgreement"
                            runat="server">
                            <Settings ShowTitlePanel="true" ShowFooter="false" ShowFilterBar="Hidden" ShowFilterRow="false" />
                            <SettingsText Title="PARTICULARS OF REGISTRATION OF P.S.O.AND CHIT AGREEMENT WITH REGISTRAR" />
                            <SettingsPager Mode="ShowAllRecords">
                            </SettingsPager>
                            <Styles Footer-HorizontalAlign="Right" Cell-HorizontalAlign="Right" Header-VerticalAlign="Middle"
                                Header-HorizontalAlign="Center" Header-Wrap="True">
                            </Styles>
                            <Columns>
                                <dx:GridViewDataColumn FieldName="SNo" Caption="S. No." CellStyle-HorizontalAlign="Right">
                                </dx:GridViewDataColumn>
                                <dx:GridViewBandColumn Caption="PARTICULARS OF GROUPS COMMENCED">
                                    <Columns>
                                        <dx:GridViewDataColumn FieldName="GroupNo" Caption="Group No." CellStyle-HorizontalAlign="Center">
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn FieldName="Category" Caption="Category" CellStyle-HorizontalAlign="Center">
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn FieldName="Members" Caption="Members" CellStyle-HorizontalAlign="Left">
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn FieldName="Value" Caption="Value" CellStyle-HorizontalAlign="Center">
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn FieldName="DurationinMonths" Caption="Duration in Months"
                                            CellStyle-HorizontalAlign="Left">
                                        </dx:GridViewDataColumn>
                                    </Columns>
                                </dx:GridViewBandColumn>
                                <dx:GridViewDataColumn FieldName="SubscriptionperInstalment" Caption="Subscription per Instalment"
                                    FooterCellStyle-HorizontalAlign="Right" CellStyle-HorizontalAlign="Left">
                                </dx:GridViewDataColumn>
                                <dx:GridViewBandColumn Caption="P.S.O. Registration Particulars">
                                    <Columns>
                                        <dx:GridViewDataColumn FieldName="DROffice" Caption="D.R. Office" CellStyle-HorizontalAlign="Center">
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn FieldName="DateofOrder" Caption="Date of Order" CellStyle-HorizontalAlign="Center">
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn FieldName="OrderNo" Caption="Order No." CellStyle-HorizontalAlign="Left">
                                        </dx:GridViewDataColumn>
                                    </Columns>
                                </dx:GridViewBandColumn>
                                <dx:GridViewBandColumn Caption="Chit Agreement Registration Particulars">
                                    <Columns>
                                        <dx:GridViewDataColumn FieldName="RegrOffice" Caption="Regr. Office" CellStyle-HorizontalAlign="Center">
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn FieldName="DateofAgreement" Caption="Date of Agreement" CellStyle-HorizontalAlign="Center">
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewBandColumn Caption="Agreement">
                                            <Columns>
                                                <dx:GridViewDataColumn FieldName="AgreementNo" Caption="No." CellStyle-HorizontalAlign="Left">
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn FieldName="AgreementYear" Caption="Year" CellStyle-HorizontalAlign="Left">
                                                </dx:GridViewDataColumn>
                                            </Columns>
                                        </dx:GridViewBandColumn>
                                    </Columns>
                                </dx:GridViewBandColumn>
                                <dx:GridViewDataColumn FieldName="DateofCommencement" Caption="Date of Commencement"
                                    FooterCellStyle-HorizontalAlign="Right" CellStyle-HorizontalAlign="Right">
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="Remarks" FooterCellStyle-HorizontalAlign="Right"
                                    CellStyle-HorizontalAlign="Right">
                                </dx:GridViewDataColumn>
                            </Columns>
                        </dx:ASPxGridView>
                        <dx:ASPxGridViewExporter ID="exportRegistrationofPSOandChitAgreement" runat="server"
                            GridViewID="gridRegistrationofPSOandChitAgreement">
                            <Styles>
                                <Title Paddings-PaddingBottom="5px" Paddings-PaddingTop="5px"></Title>
                                <Header Wrap="True" Paddings-PaddingBottom="5px" Paddings-PaddingTop="5px" HorizontalAlign="Center">
                                </Header>
                                <Cell Paddings-Padding="5px" Wrap="True">
                                </Cell>
                                <Footer Paddings-Padding="5px" Wrap="True">
                                </Footer>
                            </Styles>
                        </dx:ASPxGridViewExporter>
                          <dx:ASPxGridViewExporter ID="gridRegistrationofPSOandChitAgreementexcel" GridViewID="gridRegistrationofPSOandChitAgreement" 

    PaperKind="A4" Landscape="True"  runat="server" >
</dx:ASPxGridViewExporter>
                    </div>
                    <div class="row">
                        <dx:ASPxGridView AutoGenerateColumns="false" Style="margin: 0 auto; width: 100%;"
                            ID="gridParticularsofChitPrizeMoney" ClientInstanceName="gridParticularsofChitPrizeMoney"
                            runat="server">
                            <Settings ShowTitlePanel="true" ShowFooter="false" ShowFilterBar="Hidden" ShowFilterRow="false" />
                            <SettingsText Title="PARTICULARS OF CHIT PRIZE MONEY PAID" />
                            <SettingsPager Mode="ShowAllRecords">
                            </SettingsPager>
                            <Styles Footer-HorizontalAlign="Right" Cell-HorizontalAlign="Right" Header-VerticalAlign="Middle"
                                Header-HorizontalAlign="Center" Header-Wrap="True">
                            </Styles>
                            <Columns>
                                <dx:GridViewDataColumn FieldName="SNo" ExportWidth="30" Caption="S. No." CellStyle-HorizontalAlign="Right">
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="ChitNo" ExportWidth="80" Caption="Chit No." CellStyle-HorizontalAlign="Left">
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="Name" ExportWidth="120" Caption="Name" CellStyle-HorizontalAlign="Left">
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="InstalmentNo" ExportWidth="60" Caption="Instalment No." CellStyle-HorizontalAlign="Center">
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="DateofAuction" ExportWidth="70" Caption="Date of Auction" CellStyle-HorizontalAlign="Left">
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="PrizeMoney" ExportWidth="85" Caption="Prize Money" CellStyle-HorizontalAlign="Right">
                                </dx:GridViewDataColumn>
                                <dx:GridViewBandColumn Caption="Prize Money Disbursement Particulars">
                                    <Columns>
                                        <dx:GridViewDataColumn FieldName="DateofPayment" ExportWidth="70" Caption="Date of Payment" CellStyle-HorizontalAlign="Left">
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn FieldName="ChequeNo" ExportWidth="70" Caption="Cheque No." CellStyle-HorizontalAlign="Center">
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn FieldName="Bank" Caption="Bank" ExportWidth="70" CellStyle-HorizontalAlign="Left">
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn FieldName="Place" Caption="Place" ExportWidth="90" CellStyle-HorizontalAlign="Left">
                                        </dx:GridViewDataColumn>
                                    </Columns>
                                </dx:GridViewBandColumn>
                                <dx:GridViewDataColumn FieldName="ABFormSanctionNo" ExportWidth="70" Caption="AB Form Sanction No."
                                    FooterCellStyle-HorizontalAlign="Left" CellStyle-HorizontalAlign="Left">
                                </dx:GridViewDataColumn>                               
                                 <dx:GridViewDataColumn FieldName="GST" ExportWidth="70" Caption="GST(CGST+SGST)"
                                    FooterCellStyle-HorizontalAlign="Right" CellStyle-HorizontalAlign="Right">
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="IGST" ExportWidth="70" Caption="IGST"
                                    FooterCellStyle-HorizontalAlign="Right" CellStyle-HorizontalAlign="Right">
                                </dx:GridViewDataColumn>
                              <%--  <dx:GridViewDataColumn FieldName="DocumnetChargeRs" ExportWidth="60" Caption="Documnet Charge Rs."
                                    FooterCellStyle-HorizontalAlign="Right" CellStyle-HorizontalAlign="Right">
                                </dx:GridViewDataColumn>--%>
                                <dx:GridViewDataColumn FieldName="commision" ExportWidth="90" HeaderStyle-HorizontalAlign="Center"
                                    Caption="Company Commision" FooterCellStyle-HorizontalAlign="Right" CellStyle-HorizontalAlign="Right">
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="DateCredited" ExportWidth="100" HeaderStyle-HorizontalAlign="Center"
                                    Caption="Date Credited" FooterCellStyle-HorizontalAlign="Left" CellStyle-HorizontalAlign="Left">
                                </dx:GridViewDataColumn>
                            </Columns>
                        </dx:ASPxGridView>
                        <dx:ASPxGridViewExporter ID="exportParticularsofChitPrizeMoney" runat="server" GridViewID="gridParticularsofChitPrizeMoney">
                            <Styles>
                                <Title Paddings-PaddingBottom="3px" Paddings-PaddingTop="3px"></Title>
                                <Header Wrap="True" Paddings-PaddingBottom="3px" Paddings-PaddingTop="3px" HorizontalAlign="Center">
                                </Header>
                                <Cell Paddings-Padding="3px" Font-Size="Medium" Wrap="True">
                                </Cell>
                                <Footer Paddings-Padding="3px" Wrap="True">
                                </Footer>
                            </Styles>
                        </dx:ASPxGridViewExporter>
                         <dx:ASPxGridViewExporter ID="gridChitPrizeMoneyexcel" GridViewID="gridParticularsofChitPrizeMoney" 

    PaperKind="A4" Landscape="True"  runat="server" >
</dx:ASPxGridViewExporter>
                    </div>
                    <div class="row" style="margin: 0px auto; width: 100%; overflow: auto !important;">
                        <dx:ASPxGridView AutoGenerateColumns="false" Style="margin: 0 auto; width:auto"
                            ID="gridGroupwarChitControlStatement" ClientInstanceName="gridGroupwarChitControlStatement"
                            runat="server">
                            <TotalSummary>
                                <dx:ASPxSummaryItem FieldName="Value" DisplayFormat="{0}" SummaryType="Sum" />
                                <dx:ASPxSummaryItem FieldName="SDP_Amount" DisplayFormat="{0}" SummaryType="Sum" />
                            </TotalSummary>
                            <Settings ShowTitlePanel="true" ShowFooter="true" ShowFilterBar="Hidden" ShowFilterRow="false" />
                            <SettingsText Title="GROUPWAR CHIT CONTROL STATEMENT" />
                            <SettingsPager Mode="ShowAllRecords">
                            </SettingsPager>
                            <Styles Footer-HorizontalAlign="Right" Cell-HorizontalAlign="Right" Header-VerticalAlign="Middle"
                                Header-HorizontalAlign="Center" Header-Wrap="True">
                            </Styles>
                            <Columns>
                                <dx:GridViewDataColumn FieldName="SNo" ExportWidth="30" Caption="S. No." CellStyle-HorizontalAlign="Right">
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="GROUPNO" ExportWidth="50" Caption="Group No." CellStyle-HorizontalAlign="Left">
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="Category" ExportWidth="60" Caption="Category" CellStyle-HorizontalAlign="Left">
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="TotalMembers" ExportWidth="40" Caption="Total Member" CellStyle-HorizontalAlign="Center">
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="Instalment" ExportWidth="40" Caption="Instalment" CellStyle-HorizontalAlign="Left">
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="Value" ExportWidth="80" Caption="Value" CellStyle-HorizontalAlign="Right">
                                </dx:GridViewDataColumn>
                                <dx:GridViewBandColumn Caption="Date of">
                                    <Columns>
                                        <dx:GridViewDataColumn FieldName="Commencement" ExportWidth="70" Caption="Commencement" CellStyle-HorizontalAlign="Left">
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn FieldName="Termination" ExportWidth="70" Caption="Termination" CellStyle-HorizontalAlign="Center">
                                        </dx:GridViewDataColumn>
                                    </Columns>
                                </dx:GridViewBandColumn>
                                <dx:GridViewBandColumn Caption="Date of">
                                    <Columns>
                                        <dx:GridViewDataColumn FieldName="SDP_FDRNO" ExportWidth="60"  Caption="F.D.R. No." CellStyle-HorizontalAlign="Left">
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn FieldName="SDP_Bank" ExportWidth="70"  Caption="Bank" CellStyle-HorizontalAlign="Center">
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn FieldName="SDP_BankPlace" ExportWidth="70"  Caption="Place" CellStyle-HorizontalAlign="Left">
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn FieldName="SDP_Commencement" ExportWidth="70" Caption="Commencement" CellStyle-HorizontalAlign="Center">
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn FieldName="SDP_Maturity" ExportWidth="70" Caption="Maturity" CellStyle-HorizontalAlign="Left">
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn FieldName="SDP_RateofInterest" ExportWidth="50" Caption="Rate of Interset"
                                            CellStyle-HorizontalAlign="Center">
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn FieldName="SDP_PeriodinMonths" ExportWidth="50" Caption="Period in Months"
                                            CellStyle-HorizontalAlign="Left">
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn FieldName="SDP_Amount" ExportWidth="80" Caption="Amount" CellStyle-HorizontalAlign="Center">
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn FieldName="MaturityValue" ExportWidth="30" Caption="Maturity Value" CellStyle-HorizontalAlign="Center">
                                        </dx:GridViewDataColumn>
                                    </Columns>
                                </dx:GridViewBandColumn>
                                <dx:GridViewBandColumn Caption="Chit Agreement">
                                    <Columns>
                                        <dx:GridViewDataColumn FieldName="ChitAgreementNo" ExportWidth="40" Caption="No." FooterCellStyle-HorizontalAlign="Left"
                                            CellStyle-HorizontalAlign="Right">
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn FieldName="ChitAgreementYear" ExportWidth="40" Caption="Year" FooterCellStyle-HorizontalAlign="Right"
                                            CellStyle-HorizontalAlign="Right">
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn FieldName="AgreementDate" ExportWidth="70" Caption="Date" FooterCellStyle-HorizontalAlign="Right"
                                            CellStyle-HorizontalAlign="Right">
                                        </dx:GridViewDataColumn>
                                    </Columns>
                                </dx:GridViewBandColumn>
                                <dx:GridViewDataColumn FieldName="DateofSubmissionofBalanceSheet" ExportWidth="70" HeaderStyle-HorizontalAlign="Center"
                                    Caption="Date of Submission of Balance Sheet" FooterCellStyle-HorizontalAlign="Right"
                                    CellStyle-HorizontalAlign="Right">
                                </dx:GridViewDataColumn>
                            </Columns>
                        </dx:ASPxGridView>
                        <dx:ASPxGridViewExporter ID="exportGroupwarChitControlStatement" runat="server" GridViewID="gridGroupwarChitControlStatement">
                            <Styles>
                                <Title Paddings-PaddingBottom="2px" Paddings-PaddingTop="3px"></Title>
                                <Header Wrap="True" Paddings-PaddingBottom="2px" Paddings-PaddingTop="2px" HorizontalAlign="Center">
                                </Header>
                                <Cell Paddings-Padding="2px" Wrap="True">
                                </Cell>
                                <Footer Paddings-Padding="2px" Wrap="True">
                                </Footer>
                            </Styles>
                        </dx:ASPxGridViewExporter>
                         <dx:ASPxGridViewExporter ID="gridGroupwarChitControlStatementexcel" GridViewID="gridGroupwarChitControlStatement" 

    PaperKind="A4" Landscape="True"  runat="server" >
</dx:ASPxGridViewExporter>
                    </div>
                    <div class="row">
                        <dx:ASPxGridView AutoGenerateColumns="false" Style="margin: 0 auto; width: 100%;"
                            ID="gridOtherItems" ClientInstanceName="gridOtherItems" runat="server">
                            <Settings ShowTitlePanel="true" ShowFooter="false" ShowFilterBar="Hidden" ShowFilterRow="false" />
                            <SettingsText Title="Trial Balance Of Other Items" />
                            <SettingsPager Mode="ShowAllRecords">
                            </SettingsPager>
                            <Styles Footer-HorizontalAlign="Right" Cell-HorizontalAlign="Right" Header-VerticalAlign="Middle"
                                Header-HorizontalAlign="Center" Header-Wrap="True">
                            </Styles>
                            <Columns>
                                <dx:GridViewDataColumn Width="10%" FieldName="SNo" Caption="S.No." CellStyle-HorizontalAlign="Right">
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn Width="30%" FieldName="Heads" FooterCellStyle-HorizontalAlign="Right"
                                    CellStyle-HorizontalAlign="Left">
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn Width="20%" FieldName="Credit" FooterCellStyle-HorizontalAlign="Right"
                                    CellStyle-HorizontalAlign="Right">
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn Width="20%" FieldName="Debit" FooterCellStyle-HorizontalAlign="Right"
                                    CellStyle-HorizontalAlign="Right">
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn Width="20%" FieldName="Remarks" FooterCellStyle-HorizontalAlign="Right"
                                    CellStyle-HorizontalAlign="Right">
                                </dx:GridViewDataColumn>
                            </Columns>
                        </dx:ASPxGridView>
                        <dx:ASPxGridViewExporter ID="exportOtherItems" runat="server" GridViewID="gridOtherItems">
                            <Styles>
                                <Title Paddings-PaddingBottom="5px" Paddings-PaddingTop="5px"></Title>
                                <Header Wrap="True" Paddings-PaddingBottom="5px" Paddings-PaddingTop="5px" HorizontalAlign="Center">
                                </Header>
                                <Cell Paddings-Padding="5px" Wrap="True">
                                </Cell>
                                <Footer Paddings-Padding="5px" Wrap="True">
                                </Footer>
                            </Styles>
                        </dx:ASPxGridViewExporter>
                         <dx:ASPxGridViewExporter ID="gridOtherItemsexcel" GridViewID="gridOtherItems" 

    PaperKind="A4" Landscape="True"  runat="server" >
</dx:ASPxGridViewExporter>
                    </div>
                    <div class="row">
                        <dx:ASPxGridView AutoGenerateColumns="false" Style="margin: 0 auto; width: 100%;"
                            ID="gridInvestments" ClientInstanceName="gridInvestments" runat="server">
                            <Settings ShowTitlePanel="true" ShowFooter="false" ShowFilterBar="Hidden" ShowFilterRow="false" />
                            <SettingsText Title="Trial Balance Of Investments" />
                            <SettingsPager Mode="ShowAllRecords">
                            </SettingsPager>
                            <Styles Footer-HorizontalAlign="Right" Cell-HorizontalAlign="Right" Header-VerticalAlign="Middle"
                                Header-HorizontalAlign="Center" Header-Wrap="True">
                            </Styles>
                            <Columns>
                                <dx:GridViewDataColumn Width="10%" ExportWidth="50" FieldName="SNo" Caption="S.No." CellStyle-HorizontalAlign="Right">
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn Width="20%" ExportWidth="150" FieldName="Heads" Caption="MAIN ITEM OF INVESTMENT" FooterCellStyle-HorizontalAlign="Right"
                                    CellStyle-HorizontalAlign="Left">
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn Width="30%" ExportWidth="150" FieldName="Narration" Caption="DESCRIPTION" FooterCellStyle-HorizontalAlign="Right"
                                    CellStyle-HorizontalAlign="Left">
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn Width="20%" ExportWidth="75" FieldName="Credit" FooterCellStyle-HorizontalAlign="Right"
                                    CellStyle-HorizontalAlign="Right">
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn Width="20%" ExportWidth="75" FieldName="Debit" FooterCellStyle-HorizontalAlign="Right"
                                    CellStyle-HorizontalAlign="Right">
                                </dx:GridViewDataColumn>
                            </Columns>
                        </dx:ASPxGridView>
                        <dx:ASPxGridViewExporter ID="exportInvestments" runat="server" GridViewID="gridInvestments">
                            <Styles>
                                <Title Paddings-PaddingBottom="5px" Paddings-PaddingTop="5px"></Title>
                                <Header Wrap="True" Paddings-PaddingBottom="5px" Paddings-PaddingTop="5px" HorizontalAlign="Center">
                                </Header>
                                <Cell Paddings-Padding="5px" Wrap="True">
                                </Cell>
                                <Footer Paddings-Padding="5px" Wrap="True">
                                </Footer>
                            </Styles>
                        </dx:ASPxGridViewExporter>
                         <dx:ASPxGridViewExporter ID="gridInvestmentsexcel" GridViewID="gridInvestments" 

    PaperKind="A4" Landscape="True"  runat="server" >
</dx:ASPxGridViewExporter>
                    </div>
                    <div class="row">
                        <dx:ASPxGridView AutoGenerateColumns="false" Style="margin: 0 auto; width: 100%;"
                            ID="gridBanks" ClientInstanceName="gridBanks" runat="server">
                            <Settings ShowTitlePanel="true" ShowFooter="false" ShowFilterBar="Hidden" ShowFilterRow="false" />
                            <SettingsText Title="Trial Balance Of Banks" />
                            <SettingsPager Mode="ShowAllRecords">
                            </SettingsPager>
                            <Styles Footer-HorizontalAlign="Right" Cell-HorizontalAlign="Right" Header-VerticalAlign="Middle"
                                Header-HorizontalAlign="Center" Header-Wrap="True">
                            </Styles>
                            <Columns>
                                <dx:GridViewDataColumn Width="10%" FieldName="SNo" Caption="S.No." CellStyle-HorizontalAlign="Right">
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn Width="20%" FieldName="BankName" Caption="BANK" FooterCellStyle-HorizontalAlign="Right"
                                    CellStyle-HorizontalAlign="Left">
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn Width="20%" FieldName="BankLocation" Caption="PLACE" FooterCellStyle-HorizontalAlign="Right"
                                    CellStyle-HorizontalAlign="Left">
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn Width="15%" FieldName="Credit" FooterCellStyle-HorizontalAlign="Right"
                                    CellStyle-HorizontalAlign="Right">
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn Width="15%" FieldName="Debit" FooterCellStyle-HorizontalAlign="Right"
                                    CellStyle-HorizontalAlign="Right">
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn Width="20%" FieldName="AccountNo" Caption="A/c Number" FooterCellStyle-HorizontalAlign="Right"
                                    CellStyle-HorizontalAlign="Right">
                                </dx:GridViewDataColumn>
                            </Columns>
                        </dx:ASPxGridView>
                        <dx:ASPxGridViewExporter ID="exportBanks" runat="server" GridViewID="gridBanks">
                            <Styles>
                                <Title Paddings-PaddingBottom="5px" Paddings-PaddingTop="5px"></Title>
                                <Header Wrap="True" Paddings-PaddingBottom="5px" Paddings-PaddingTop="5px" HorizontalAlign="Center">
                                </Header>
                                <Cell Paddings-Padding="5px" Wrap="True">
                                </Cell>
                                <Footer Paddings-Padding="5px" Wrap="True">
                                </Footer>
                            </Styles>
                        </dx:ASPxGridViewExporter>
                         <dx:ASPxGridViewExporter ID="gridBanksexcel" GridViewID="gridBanks" 

    PaperKind="A4" Landscape="True"  runat="server" >
</dx:ASPxGridViewExporter>
                    </div>
                    <div class="row">
                        <dx:ASPxGridView AutoGenerateColumns="false" Style="margin: 0 auto; width: 100%;"
                            ID="gridProfitCredit" ClientInstanceName="gridProfitCredit" runat="server">
                            <Settings ShowTitlePanel="true" ShowFooter="false" ShowFilterBar="Hidden" ShowFilterRow="false" />
                            <SettingsText Title="Profit and Loss Credit" />
                            <TotalSummary>
                                <dx:ASPxSummaryItem FieldName="Credit" DisplayFormat="{0}" SummaryType="Sum" />
                                <dx:ASPxSummaryItem FieldName="Debit" DisplayFormat="{0}" SummaryType="Sum" />
                                <dx:ASPxSummaryItem FieldName="Heads" DisplayFormat="TOTAL" SummaryType="Sum" />
                            </TotalSummary>
                            <SettingsPager Mode="ShowAllRecords">
                            </SettingsPager>
                            <Styles Footer-HorizontalAlign="Right" Cell-HorizontalAlign="Right" Header-VerticalAlign="Middle"
                                Header-HorizontalAlign="Center" Header-Wrap="True">
                            </Styles>
                            <Columns>
                                <dx:GridViewDataColumn Width="10%" FieldName="SNo" Caption="S.No." ExportWidth="50" CellStyle-HorizontalAlign="Right">
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn Width="40%" FieldName="Heads" ExportWidth="150" FooterCellStyle-HorizontalAlign="Right"
                                    CellStyle-HorizontalAlign="Left">
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn Width="10%" FieldName="Credit" ExportWidth="100" FooterCellStyle-HorizontalAlign="Right"
                                    CellStyle-HorizontalAlign="Right">
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn Width="10%" FieldName="Debit" ExportWidth="100" FooterCellStyle-HorizontalAlign="Right"
                                    CellStyle-HorizontalAlign="Right">
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn Width="30%" FieldName="Remarks" ExportWidth="100" FooterCellStyle-HorizontalAlign="Right"
                                    CellStyle-HorizontalAlign="Right">
                                </dx:GridViewDataColumn>
                            </Columns>
                        </dx:ASPxGridView>
                        <dx:ASPxGridViewExporter ID="exportProfitCredit" runat="server" GridViewID="gridProfitCredit">
                            <Styles>
                                <Title Paddings-PaddingBottom="5px" Paddings-PaddingTop="5px"></Title>
                                <Header Wrap="True" Paddings-PaddingBottom="5px" Paddings-PaddingTop="5px" HorizontalAlign="Center">
                                </Header>
                                <Cell Paddings-Padding="5px" Wrap="True">
                                </Cell>
                                <Footer Paddings-Padding="5px" Wrap="True">
                                </Footer>
                            </Styles>
                        </dx:ASPxGridViewExporter>
                         <dx:ASPxGridViewExporter ID="gridProfitCreditexcel" GridViewID="gridProfitCredit" 

    PaperKind="A4" Landscape="True"  runat="server" >
</dx:ASPxGridViewExporter>
                    </div>
                    <div class="row">
                        <dx:ASPxGridView AutoGenerateColumns="false" Style="margin: 0 auto; width: 100%;"
                            ID="gridProfitDebit" ClientInstanceName="gridProfitDebit" runat="server">
                            <Settings ShowTitlePanel="true" ShowFooter="false" ShowFilterBar="Hidden" ShowFilterRow="false" />
                            <SettingsText Title="Profit and Loss Debit" />
                            <SettingsPager Mode="ShowAllRecords">
                            </SettingsPager>
                            <Styles Footer-HorizontalAlign="Right" Cell-HorizontalAlign="Right" Header-VerticalAlign="Middle"
                                Header-HorizontalAlign="Center" Header-Wrap="True">
                            </Styles>
                            <Columns>
                                <dx:GridViewDataColumn Width="10%" FieldName="SNo" ExportWidth="50" Caption="S.No." CellStyle-HorizontalAlign="Right">
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn Width="40%" FieldName="Heads" ExportWidth="150" FooterCellStyle-HorizontalAlign="Right"
                                    CellStyle-HorizontalAlign="Left">
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn Width="10%" FieldName="Credit" ExportWidth="100" FooterCellStyle-HorizontalAlign="Right"
                                    CellStyle-HorizontalAlign="Right">
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn Width="10%" FieldName="Debit" ExportWidth="100" FooterCellStyle-HorizontalAlign="Right"
                                    CellStyle-HorizontalAlign="Right">
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn Width="30%" FieldName="Remarks" ExportWidth="100" FooterCellStyle-HorizontalAlign="Right"
                                    CellStyle-HorizontalAlign="Right">
                                </dx:GridViewDataColumn>
                            </Columns>
                        </dx:ASPxGridView>
                        <dx:ASPxGridViewExporter ID="exportProfitDebit" runat="server" GridViewID="gridProfitDebit">
                            <Styles>
                                <Title Paddings-PaddingBottom="5px" Paddings-PaddingTop="5px"></Title>
                                <Header Wrap="True" Paddings-PaddingBottom="5px" Paddings-PaddingTop="5px" HorizontalAlign="Center">
                                </Header>
                                <Cell Paddings-Padding="5px" Wrap="True">
                                </Cell>
                                <Footer Paddings-Padding="5px" Wrap="True">
                                </Footer>
                            </Styles>
                        </dx:ASPxGridViewExporter>
                         <dx:ASPxGridViewExporter ID="gridProfitDebitexcel" GridViewID="gridProfitDebit" 

    PaperKind="A4" Landscape="True"  runat="server" >
</dx:ASPxGridViewExporter>
                    </div>
                    <div class="row">
                        <dx:ASPxGridView AutoGenerateColumns="false" Style="margin: 0 auto; width: 100%;"
                            ID="gridOutstandingandUnpaid" ClientInstanceName="gridOutstandingandUnpaid" runat="server">
                            <TotalSummary>
                                <dx:ASPxSummaryItem FieldName="OutPrizedMoney" DisplayFormat="{0}" SummaryType="Sum" />
                                <dx:ASPxSummaryItem FieldName="OutKasar" DisplayFormat="{0}" SummaryType="Sum" />
                                <dx:ASPxSummaryItem FieldName="OutTotal" DisplayFormat="{0}" SummaryType="Sum" />
                                <dx:ASPxSummaryItem FieldName="UnpaidCommision" DisplayFormat="{0}" SummaryType="Sum" />
                                <dx:ASPxSummaryItem FieldName="UnpaidPrizeMoney" DisplayFormat="{0}" SummaryType="Sum" />
                                <dx:ASPxSummaryItem FieldName="AmountActuallyremittedbytheParty" DisplayFormat="{0}"
                                    SummaryType="Sum" />
                            </TotalSummary>
                            <Settings ShowTitlePanel="true" ShowFooter="true" ShowFilterBar="Hidden" ShowFilterRow="false" />
                            <SettingsText Title="PARTICULARS OF OUTSTANDING & UNPAID PRIZE MONEY PAYABLE" />
                            <SettingsPager Mode="ShowAllRecords">
                            </SettingsPager>
                            <Styles Footer-HorizontalAlign="Right" Cell-HorizontalAlign="Right" Header-VerticalAlign="Middle"
                                Header-HorizontalAlign="Center" Header-Wrap="True">
                            </Styles>
                            <Columns>
                                <dx:GridViewDataColumn FieldName="SNo" ExportWidth="30" Caption="S. No." CellStyle-HorizontalAlign="Right">
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="ChitNumber" ExportWidth="70" Caption="Chit Number" CellStyle-HorizontalAlign="Left">
                                </dx:GridViewDataColumn>
                                <dx:GridViewBandColumn Caption="DRAWAL">
                                    <Columns>
                                        <dx:GridViewDataColumn FieldName="Instmnt" ExportWidth="50" Caption="Instmnt" CellStyle-HorizontalAlign="Center">
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn FieldName="Date" ExportWidth="70" Caption="Date" CellStyle-HorizontalAlign="Center">
                                        </dx:GridViewDataColumn>
                                    </Columns>
                                </dx:GridViewBandColumn>
                                <dx:GridViewDataColumn FieldName="NameoftheSubscriber" ExportWidth="120" Caption="Name of the Subscriber"
                                    CellStyle-HorizontalAlign="Left">
                                </dx:GridViewDataColumn>
                                <dx:GridViewBandColumn Caption="OUT STANDING">
                                    <Columns>
                                        <dx:GridViewDataColumn FieldName="OutPrizedMoney" ExportWidth="90" Caption="Prize Money" CellStyle-HorizontalAlign="Right">
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn FieldName="OutKasar" ExportWidth="60" Caption="Kasar" CellStyle-HorizontalAlign="Right">
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn FieldName="OutTotal" Caption="Total" ExportWidth="90"  CellStyle-HorizontalAlign="Right">
                                        </dx:GridViewDataColumn>
                                    </Columns>
                                </dx:GridViewBandColumn>
                                <dx:GridViewBandColumn Caption="UNPAID">
                                    <Columns>
                                        <dx:GridViewDataColumn FieldName="UnpaidCommision" ExportWidth="70" Caption="Commision" CellStyle-HorizontalAlign="Right">
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn FieldName="UnpaidPrizeMoney" ExportWidth="80" Caption="Prize Money" CellStyle-HorizontalAlign="Right">
                                        </dx:GridViewDataColumn>
                                    </Columns>
                                </dx:GridViewBandColumn>
                                <dx:GridViewDataColumn FieldName="AmountActuallyremittedbytheParty" ExportWidth="100" Caption="Amount Actually remitted by the Party"
                                    CellStyle-HorizontalAlign="Right">
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="Arrears" ExportWidth="60" Caption="Arrears" CellStyle-HorizontalAlign="Right">
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="UnpaidPrizeMoneyPayable" ExportWidth="60" Caption="Unpaid Prize Money Payable"
                                    CellStyle-HorizontalAlign="Left">
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="Rebitdate" Caption="Rebit Date" ExportWidth="50" CellStyle-HorizontalAlign="Left">
                                </dx:GridViewDataColumn>
                            </Columns>
                        </dx:ASPxGridView>
                        <dx:ASPxGridViewExporter ID="exportOutstandingandUnpaid" runat="server" GridViewID="gridOutstandingandUnpaid">
                            <Styles>
                                <Title Paddings-PaddingBottom="3px" Paddings-PaddingTop="3px"></Title>
                                <Header Wrap="True" Paddings-PaddingBottom="3px" Paddings-PaddingTop="3px" HorizontalAlign="Center">
                                </Header>
                                <Cell Paddings-Padding="3px" Wrap="True">
                                </Cell>
                                <Footer Paddings-Padding="3px" Wrap="True">
                                </Footer>
                            </Styles>
                        </dx:ASPxGridViewExporter>
                        <dx:ASPxGridViewExporter ID="gridOutstandingandUnpaidexcel" GridViewID="gridOutstandingandUnpaid" 

    PaperKind="A4" Landscape="True"  runat="server" >
</dx:ASPxGridViewExporter>
                    </div>
                    <div class="row">
                        <dx:ASPxGridView AutoGenerateColumns="false" Style="margin: 0 auto; width: 100%;"
                            ID="gridLoans" ClientInstanceName="gridLoans" runat="server">
                            <Settings ShowTitlePanel="true" ShowFooter="false" ShowFilterBar="Hidden" ShowFilterRow="false" />
                            <SettingsText Title="Particulars of Loans Out Standing" />
                            <SettingsPager Mode="ShowAllRecords">
                            </SettingsPager>
                            <Styles Footer-HorizontalAlign="Right" Cell-HorizontalAlign="Right" Header-VerticalAlign="Middle"
                                Header-HorizontalAlign="Center" Header-Wrap="True">
                            </Styles>
                            <Columns>
                                <dx:GridViewDataColumn  FieldName="SNo" ExportWidth="30" Caption="S.No." CellStyle-HorizontalAlign="Right">
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn Width="15%" FieldName="Name" Caption="Name" FooterCellStyle-HorizontalAlign="Right"
                                    CellStyle-HorizontalAlign="Left">
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn Width="10%" FieldName="Credit" Caption="Credit" FooterCellStyle-HorizontalAlign="Right"
                                    CellStyle-HorizontalAlign="Right">
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn Width="10%"  FieldName="Debit"  Caption="Debit" FooterCellStyle-HorizontalAlign="Right"
                                    CellStyle-HorizontalAlign="Right">
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn Width="10%" FieldName="Date" Caption="Date of Loan" FooterCellStyle-HorizontalAlign="Center"
                                    CellStyle-HorizontalAlign="Center">
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn Width="10%" FieldName="RateofInt" Caption="Rate of Int." FooterCellStyle-HorizontalAlign="Center"
                                    CellStyle-HorizontalAlign="Center">
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn Width="10%" FieldName="Period" Caption="Period of Accured Int.in days"
                                    FooterCellStyle-HorizontalAlign="Center" CellStyle-HorizontalAlign="Right">
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn Width="10%" FieldName="Interest" Caption="Accured Interest"
                                    FooterCellStyle-HorizontalAlign="Center" CellStyle-HorizontalAlign="Right">
                                </dx:GridViewDataColumn>
                                <dx:GridViewBandColumn Caption="Principal Amount at Credit in">
                                    <Columns>
                                        <dx:GridViewDataColumn Width="10%" FieldName="chitno" Caption="Chit.No." FooterCellStyle-HorizontalAlign="Left"
                                            CellStyle-HorizontalAlign="Left">
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn Width="10%" FieldName="amount" Caption="Amount" FooterCellStyle-HorizontalAlign="Right"
                                            CellStyle-HorizontalAlign="Right">
                                        </dx:GridViewDataColumn>
                                    </Columns>
                                </dx:GridViewBandColumn>
                            </Columns>
                        </dx:ASPxGridView>
                        <dx:ASPxGridViewExporter ID="exportLoans" runat="server" GridViewID="gridLoans">
                            <Styles>
                                <Title Paddings-PaddingBottom="4px" Paddings-PaddingTop="4px"></Title>
                                <Header Wrap="True" Paddings-PaddingBottom="4px" Paddings-PaddingTop="4px" HorizontalAlign="Center">
                                </Header>
                                <Cell Paddings-Padding="5px" Wrap="True">
                                </Cell>
                                <Footer Paddings-Padding="5px" Wrap="True">
                                </Footer>
                            </Styles>
                        </dx:ASPxGridViewExporter>
                        <dx:ASPxGridViewExporter ID="gridLoansexcel" GridViewID="gridLoans" 

    PaperKind="A4" Landscape="True"  runat="server" >
</dx:ASPxGridViewExporter>
                    </div>
                    <div class="row">
                        <dx:ASPxGridView AutoGenerateColumns="false" Style="margin: 0 auto; width: 100%;"
                            ID="gridTerminatedExcess" ClientInstanceName="gridTerminatedExcess" runat="server">
                            <Settings ShowTitlePanel="true" ShowFooter="true" ShowFilterBar="Hidden" ShowFilterRow="false" />
                            <SettingsText Title="Groupwar Particulars of Excess Remittance and Arrears(Terminated)" />
                            <TotalSummary>
                                <dx:ASPxSummaryItem FieldName="Excess" DisplayFormat="{0}" SummaryType="Sum" />
                                <dx:ASPxSummaryItem FieldName="NP" DisplayFormat="{0}" SummaryType="Sum" />
                                <dx:ASPxSummaryItem FieldName="P" DisplayFormat="{0}" SummaryType="Sum" />
                                <dx:ASPxSummaryItem FieldName="GroupNo" DisplayFormat="TOTAL" SummaryType="Sum" />
                            </TotalSummary>
                            <SettingsPager Mode="ShowAllRecords">
                            </SettingsPager>
                            <Styles Footer-HorizontalAlign="Right" Cell-HorizontalAlign="Right" Header-VerticalAlign="Middle"
                                Header-HorizontalAlign="Center" Header-Wrap="True">
                            </Styles>
                            <Columns>
                                <dx:GridViewDataColumn Width="20%" FieldName="SNo" ExportWidth="50" HeaderStyle-Wrap="True"
                                    Caption="S.No." CellStyle-HorizontalAlign="Right">
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn Width="20%" FieldName="GroupNo" ExportWidth="100" HeaderStyle-Wrap="True"
                                    Caption="Group No." FooterCellStyle-HorizontalAlign="Right" CellStyle-HorizontalAlign="Left">
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn Width="20%" FieldName="Excess" ExportWidth="100" HeaderStyle-Wrap="True"
                                    Caption="Excess Remittance" FooterCellStyle-HorizontalAlign="Right" CellStyle-HorizontalAlign="Right">
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn Width="20%" FieldName="NP" ExportWidth="100" HeaderStyle-Wrap="True"
                                    Caption="Non-Prized Chits Arrear" FooterCellStyle-HorizontalAlign="Right" CellStyle-HorizontalAlign="Right">
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn Width="20%" FieldName="P" ExportWidth="100" HeaderStyle-Wrap="True"
                                    Caption="Prized Chits Arrear" FooterCellStyle-HorizontalAlign="Right" CellStyle-HorizontalAlign="Right">
                                </dx:GridViewDataColumn>
                            </Columns>
                        </dx:ASPxGridView>
                        <dx:ASPxGridViewExporter ID="exportTerminatedExcess" runat="server" GridViewID="gridTerminatedExcess">
                            <Styles>
                                <Title Paddings-PaddingBottom="5px" Paddings-PaddingTop="5px"></Title>
                                <Header Wrap="True" VerticalAlign="Middle" Paddings-PaddingBottom="5px" Paddings-PaddingTop="5px" HorizontalAlign="Center">
                                </Header>
                                <Cell Paddings-Padding="5px" Wrap="True">
                                </Cell>
                                <Footer Paddings-Padding="5px" Wrap="True">
                                </Footer>
                            </Styles>
                        </dx:ASPxGridViewExporter>
                        <dx:ASPxGridViewExporter ID="gridTerminatedExcessexcel" GridViewID="gridTerminatedExcess" 

    PaperKind="A4" Landscape="True"  runat="server" >
</dx:ASPxGridViewExporter>
                    </div>
                    <div class="row">
                        <dx:ASPxGridView AutoGenerateColumns="false" Style="margin: 0 auto; width: 100%;"
                            ID="gridRCM" ClientInstanceName="gridRCM" runat="server">
                            <Settings ShowTitlePanel="true" ShowFooter="true" ShowFilterBar="Hidden" ShowFilterRow="false" />
                            <SettingsText Title="Particulars of Amount at Credit" />
                            <TotalSummary>
                                <dx:ASPxSummaryItem FieldName="rcm1" DisplayFormat="{0}" SummaryType="Sum" />
                                <dx:ASPxSummaryItem FieldName="rcm2" DisplayFormat="{0}" SummaryType="Sum" />
                                <dx:ASPxSummaryItem FieldName="cc" DisplayFormat="{0}" SummaryType="Sum" />
                                <dx:ASPxSummaryItem FieldName="Heads" DisplayFormat="TOTAL" SummaryType="Sum" />
                            </TotalSummary>
                            <SettingsPager Mode="ShowAllRecords">
                            </SettingsPager>
                            <Styles Footer-HorizontalAlign="Right" Cell-HorizontalAlign="Right" Header-VerticalAlign="Middle"
                                Header-HorizontalAlign="Center" Header-Wrap="True">
                            </Styles>
                            <Columns>
                                <dx:GridViewDataColumn Width="10%" FieldName="SNo" ExportWidth="30" Caption="S.No." CellStyle-HorizontalAlign="Right">
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn Width="20%" FieldName="Heads" ExportWidth="100" Caption="Name" FooterCellStyle-HorizontalAlign="Right"
                                    CellStyle-HorizontalAlign="Left">
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn Width="15%" FieldName="rcm1" ExportWidth="90" Caption="RCM A/C I" FooterCellStyle-HorizontalAlign="Right"
                                    CellStyle-HorizontalAlign="Right">
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn Width="15%" FieldName="rcm2" ExportWidth="90" Caption="RCM A/C II" FooterCellStyle-HorizontalAlign="Right"
                                    CellStyle-HorizontalAlign="Right">
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn Width="20%" FieldName="cc" ExportWidth="100" Caption="Chit Collection to be Accounted"
                                    FooterCellStyle-HorizontalAlign="Right" CellStyle-HorizontalAlign="Right">
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn Width="20%" FieldName="description" ExportWidth="100" Caption="Full Details"
                                    FooterCellStyle-HorizontalAlign="Right" CellStyle-HorizontalAlign="Left">
                                </dx:GridViewDataColumn>
                            </Columns>
                        </dx:ASPxGridView>
                        <dx:ASPxGridViewExporter ID="exportRCM" runat="server" GridViewID="gridRCM">
                            <Styles>
                                <Title Paddings-PaddingBottom="3px" Paddings-PaddingTop="3px"></Title>
                                <Header Wrap="True" Paddings-PaddingBottom="3px" Paddings-PaddingTop="3px" HorizontalAlign="Center">
                                </Header>
                                <Cell Paddings-Padding="3px" Wrap="True">
                                </Cell>
                                <Footer Paddings-Padding="3px" Wrap="True">
                                </Footer>
                            </Styles>
                        </dx:ASPxGridViewExporter>
                         <dx:ASPxGridViewExporter ID="gridRCMexcel" GridViewID="gridRCM" 

    PaperKind="A4" Landscape="True"  runat="server" >
</dx:ASPxGridViewExporter>
                    </div>







                  <div class="row">
                    <dx:ASPxGridView AutoGenerateColumns="false" Style="margin: 0 auto; width: 100%;"
                            ID="gridSundriesandAdvances" ClientInstanceName="gridSundriesandAdvances" runat="server">
                            <SettingsText Title="PARTICULARS OF SUNDRIES AND ADVANCES" />
                            <Settings ShowTitlePanel="true" ShowFooter="false" ShowFilterBar="Hidden" ShowFilterRow="false" />
                            <SettingsPager Mode="ShowAllRecords">
                            </SettingsPager>
                            <Styles Footer-HorizontalAlign="Right" Cell-HorizontalAlign="Right" Header-VerticalAlign="Middle"
                                Header-HorizontalAlign="Center" Header-Wrap="True">
                            </Styles>
                            <Columns>
                                <dx:GridViewDataColumn FieldName="SNo" ExportWidth="30" Caption="S.No." CellStyle-HorizontalAlign="Right">
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="Heads" ExportWidth="100" Caption="Name" FooterCellStyle-HorizontalAlign="Right"
                                    CellStyle-HorizontalAlign="Left">
                                </dx:GridViewDataColumn>
                                <dx:GridViewBandColumn Caption="EB Deposit">
                                    <Columns>
                                        <dx:GridViewDataColumn FieldName="EB_Credit" ExportWidth="50" Caption="Credit" FooterCellStyle-HorizontalAlign="Right"
                                            CellStyle-HorizontalAlign="Right">
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn FieldName="EB_Debit" ExportWidth="50" Caption="Debit" FooterCellStyle-HorizontalAlign="Right"
                                            CellStyle-HorizontalAlign="Right">
                                        </dx:GridViewDataColumn>
                                    </Columns>
                                </dx:GridViewBandColumn>
                                <dx:GridViewBandColumn Caption="Sundry Creditors">
                                    <Columns>
                                        <dx:GridViewDataColumn FieldName="SC_Credit" ExportWidth="35" Caption="Credit" FooterCellStyle-HorizontalAlign="Right"
                                            CellStyle-HorizontalAlign="Right">
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn FieldName="SC_Debit" ExportWidth="35" Caption="Debit" FooterCellStyle-HorizontalAlign="Right"
                                            CellStyle-HorizontalAlign="Right">
                                        </dx:GridViewDataColumn>
                                    </Columns>
                                </dx:GridViewBandColumn>
                                <dx:GridViewBandColumn Caption="Sundry Debtors">
                                    <Columns>
                                        <dx:GridViewDataColumn FieldName="SDeb_Credit" ExportWidth="30" Caption="Credit"
                                            FooterCellStyle-HorizontalAlign="Right" CellStyle-HorizontalAlign="Right">
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn FieldName="SDeb_Debit" ExportWidth="30" Caption="Debit" FooterCellStyle-HorizontalAlign="Right"
                                            CellStyle-HorizontalAlign="Right">
                                        </dx:GridViewDataColumn>
                                    </Columns>
                                </dx:GridViewBandColumn>
                                <dx:GridViewBandColumn Caption="Staff Advances">
                                    <Columns>
                                        <dx:GridViewDataColumn FieldName="S_Credit" ExportWidth="30" Caption="Credit" FooterCellStyle-HorizontalAlign="Right"
                                            CellStyle-HorizontalAlign="Right">
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn FieldName="S_Debit" ExportWidth="30" Caption="Debit" FooterCellStyle-HorizontalAlign="Right"
                                            CellStyle-HorizontalAlign="Right">
                                        </dx:GridViewDataColumn>
                                    </Columns>
                                </dx:GridViewBandColumn>
                                <dx:GridViewBandColumn Caption="Degree Advances">
                                    <Columns>
                                        <dx:GridViewDataColumn FieldName="Degree_Credit" ExportWidth="30" Caption="Credit"
                                            FooterCellStyle-HorizontalAlign="Right" CellStyle-HorizontalAlign="Right">
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn FieldName="Degree_Debit" ExportWidth="30" Caption="Debit"
                                            FooterCellStyle-HorizontalAlign="Right" CellStyle-HorizontalAlign="Right">
                                        </dx:GridViewDataColumn>
                                    </Columns>
                                </dx:GridViewBandColumn>
                                <dx:GridViewBandColumn Caption="Advocate Advances">
                                    <Columns>
                                        <dx:GridViewDataColumn FieldName="Advocate_Credit" ExportWidth="30" Caption="Credit"
                                            FooterCellStyle-HorizontalAlign="Right" CellStyle-HorizontalAlign="Right">
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn FieldName="Advocate_Debit" ExportWidth="30" Caption="Debit"
                                            FooterCellStyle-HorizontalAlign="Right" CellStyle-HorizontalAlign="Right">
                                        </dx:GridViewDataColumn>
                                    </Columns>
                                </dx:GridViewBandColumn>
                                <dx:GridViewBandColumn Caption="Vehicle Recovery Advances">
                                    <Columns>
                                        <dx:GridViewDataColumn FieldName="VRA_Credit" ExportWidth="30" Caption="Credit" FooterCellStyle-HorizontalAlign="Right"
                                            CellStyle-HorizontalAlign="Right">
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn FieldName="VRA_Debit" ExportWidth="30" Caption="Debit" FooterCellStyle-HorizontalAlign="Right"
                                            CellStyle-HorizontalAlign="Right">
                                        </dx:GridViewDataColumn>                                       

                                    </Columns>


                                </dx:GridViewBandColumn>
                                <dx:GridViewDataColumn FieldName="Remarks" ExportWidth="100" Caption="On account of"
                                    FooterCellStyle-HorizontalAlign="Right" CellStyle-HorizontalAlign="Left">
                                </dx:GridViewDataColumn>
                            </Columns>
                        </dx:ASPxGridView>
                       <dx:ASPxGridView AutoGenerateColumns="false" Style="margin: 0 auto; width: 100%;"
                            ID="gridSundriesandAdvancesPart1" ClientInstanceName="gridSundriesandAdvancesPart1"
                            runat="server">
                            <SettingsText Title="PARTICULARS OF SUNDRIES AND ADVANCES" />
                            <Settings ShowTitlePanel="true" ShowFooter="false" ShowFilterBar="Hidden" ShowFilterRow="false" />
                            <SettingsPager Mode="ShowAllRecords">
                            </SettingsPager>
                           
                            <Styles Footer-HorizontalAlign="Right" Cell-HorizontalAlign="Right" Header-VerticalAlign="Middle"
                                Header-HorizontalAlign="Center" Header-Wrap="True">
                            </Styles>
                            <Columns>
                                <dx:GridViewDataColumn FieldName="SNo" ExportWidth="30" Width="30" Caption="S.No."
                                    CellStyle-HorizontalAlign="Right">
                                    <CellStyle HorizontalAlign="Right">
                                    </CellStyle>
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="Heads" ExportWidth="100" Width="100" Caption="Name"
                                    FooterCellStyle-HorizontalAlign="Right" CellStyle-HorizontalAlign="Left">
                                    <CellStyle HorizontalAlign="Left">
                                    </CellStyle>
                                    <FooterCellStyle HorizontalAlign="Right">
                                    </FooterCellStyle>
                                </dx:GridViewDataColumn>
                                <dx:GridViewBandColumn Caption="EB Deposit">
                                    <Columns>
                                        <dx:GridViewDataColumn FieldName="EB_Credit" ExportWidth="50" Width="50" Caption="Credit"
                                            FooterCellStyle-HorizontalAlign="Right" CellStyle-HorizontalAlign="Right">
                                            <CellStyle HorizontalAlign="Right">
                                            </CellStyle>
                                            <FooterCellStyle HorizontalAlign="Right">
                                            </FooterCellStyle>
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn FieldName="EB_Debit" ExportWidth="50" Width="50" Caption="Debit"
                                            FooterCellStyle-HorizontalAlign="Right" CellStyle-HorizontalAlign="Right">
                                            <CellStyle HorizontalAlign="Right">
                                            </CellStyle>
                                            <FooterCellStyle HorizontalAlign="Right">
                                            </FooterCellStyle>
                                        </dx:GridViewDataColumn>
                                    </Columns>
                                </dx:GridViewBandColumn>
                                <dx:GridViewBandColumn Caption="Telephone Deposit">
                                    <Columns>
                                        <dx:GridViewDataColumn FieldName="TD_Credit" ExportWidth="35" Width="35" Caption="Credit"
                                            FooterCellStyle-HorizontalAlign="Right" CellStyle-HorizontalAlign="Right">
                                            <CellStyle HorizontalAlign="Right">
                                            </CellStyle>
                                            <FooterCellStyle HorizontalAlign="Right">
                                            </FooterCellStyle>
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn FieldName="TD_Debit" ExportWidth="35" Width="35" Caption="Debit"
                                            FooterCellStyle-HorizontalAlign="Right" CellStyle-HorizontalAlign="Right">
                                            <CellStyle HorizontalAlign="Right">
                                            </CellStyle>
                                            <FooterCellStyle HorizontalAlign="Right">
                                            </FooterCellStyle>
                                        </dx:GridViewDataColumn>
                                    </Columns>
                                </dx:GridViewBandColumn>
                                <dx:GridViewBandColumn Caption="Rent Advance">
                                    <Columns>
                                        <dx:GridViewDataColumn FieldName="RA_Credit" ExportWidth="35" Width="35" Caption="Credit"
                                            FooterCellStyle-HorizontalAlign="Right" CellStyle-HorizontalAlign="Right">
                                            <CellStyle HorizontalAlign="Right">
                                            </CellStyle>
                                            <FooterCellStyle HorizontalAlign="Right">
                                            </FooterCellStyle>
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn FieldName="RA_Debit" ExportWidth="35" Width="35" Caption="Debit"
                                            FooterCellStyle-HorizontalAlign="Right" CellStyle-HorizontalAlign="Right">
                                            <CellStyle HorizontalAlign="Right">
                                            </CellStyle>
                                            <FooterCellStyle HorizontalAlign="Right">
                                            </FooterCellStyle>
                                        </dx:GridViewDataColumn>
                                    </Columns>
                                </dx:GridViewBandColumn>
                                <dx:GridViewBandColumn Caption="Staff Advance">
                                    <Columns>
                                        <dx:GridViewDataColumn FieldName="S_Credit" ExportWidth="35" Width="35" Caption="Credit"
                                            FooterCellStyle-HorizontalAlign="Right" CellStyle-HorizontalAlign="Right">
                                            <CellStyle HorizontalAlign="Right">
                                            </CellStyle>
                                            <FooterCellStyle HorizontalAlign="Right">
                                            </FooterCellStyle>
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn FieldName="S_Debit" ExportWidth="35" Width="35" Caption="Debit"
                                            FooterCellStyle-HorizontalAlign="Right" CellStyle-HorizontalAlign="Right">
                                            <CellStyle HorizontalAlign="Right">
                                            </CellStyle>
                                            <FooterCellStyle HorizontalAlign="Right">
                                            </FooterCellStyle>
                                        </dx:GridViewDataColumn>
                                    </Columns>
                                </dx:GridViewBandColumn>
                                <dx:GridViewBandColumn Caption="Prepaid Advance">
                                    <Columns>
                                        <dx:GridViewDataColumn FieldName="PPA_Credit" ExportWidth="40" Width="40" Caption="Credit"
                                            FooterCellStyle-HorizontalAlign="Right" CellStyle-HorizontalAlign="Right">
                                            <CellStyle HorizontalAlign="Right">
                                            </CellStyle>
                                            <FooterCellStyle HorizontalAlign="Right">
                                            </FooterCellStyle>
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn FieldName="PPA_Debit" ExportWidth="40" Width="40" Caption="Debit"
                                            FooterCellStyle-HorizontalAlign="Right" CellStyle-HorizontalAlign="Right">
                                            <CellStyle HorizontalAlign="Right">
                                            </CellStyle>
                                            <FooterCellStyle HorizontalAlign="Right">
                                            </FooterCellStyle>
                                        </dx:GridViewDataColumn>
                                    </Columns>
                                </dx:GridViewBandColumn>
                                <dx:GridViewBandColumn Caption="Vehicle Recovery Advances">
                                    <Columns>
                                        <dx:GridViewDataColumn FieldName="VRA_Credit" ExportWidth="30" Caption="Credit" FooterCellStyle-HorizontalAlign="Right"
                                            CellStyle-HorizontalAlign="Right">
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn FieldName="VRA_Debit" ExportWidth="30" Caption="Debit" FooterCellStyle-HorizontalAlign="Right"
                                            CellStyle-HorizontalAlign="Right">
                                        </dx:GridViewDataColumn>                                       

                                    </Columns>


                                </dx:GridViewBandColumn>
                               


                                <dx:GridViewBandColumn Caption="Sundry Creditors">
                                    <Columns>
                                        <dx:GridViewDataColumn FieldName="SC_Credit" ExportWidth="40" Width="40" Caption="Credit"
                                            FooterCellStyle-HorizontalAlign="Right" CellStyle-HorizontalAlign="Right">
                                            <CellStyle HorizontalAlign="Right">
                                            </CellStyle>
                                            <FooterCellStyle HorizontalAlign="Right">
                                            </FooterCellStyle>
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn FieldName="SC_Debit" ExportWidth="40" Width="40" Caption="Debit"
                                            FooterCellStyle-HorizontalAlign="Right" CellStyle-HorizontalAlign="Right">
                                            <CellStyle HorizontalAlign="Right">
                                            </CellStyle>
                                            <FooterCellStyle HorizontalAlign="Right">
                                            </FooterCellStyle>
                                        </dx:GridViewDataColumn>
                                    </Columns>
                                 </dx:GridViewBandColumn>
                                 <dx:GridViewBandColumn Caption="Sundry Debtors">
                                    <Columns>
                                        <dx:GridViewDataColumn FieldName="SDeb_Credit" ExportWidth="50" Width="50" Caption="Credit"
                                            FooterCellStyle-HorizontalAlign="Right" CellStyle-HorizontalAlign="Right">
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn FieldName="SDeb_Debit" ExportWidth="50" Width="50" Caption="Debit"
                                            FooterCellStyle-HorizontalAlign="Right" CellStyle-HorizontalAlign="Right">
                                        </dx:GridViewDataColumn>
                                    </Columns>
                                </dx:GridViewBandColumn>

                                <dx:GridViewDataColumn FieldName="Remarks" ExportWidth="100" Width="100" Caption="On account of"
                                    FooterCellStyle-HorizontalAlign="Right" CellStyle-HorizontalAlign="Left">
                                    <CellStyle HorizontalAlign="Left">
                                    </CellStyle>
                                    <FooterCellStyle HorizontalAlign="Right">
                                    </FooterCellStyle>
                                </dx:GridViewDataColumn>
                            </Columns>
                       
                        </dx:ASPxGridView>
                      <dx:ASPxGridView AutoGenerateColumns="False" Style="margin: 0 auto; width: 100%;"
                            ID="gridSundriesandAdvancesPart2" ClientInstanceName="gridSundriesandAdvancesPart2_HtmlRowCreated"
                            runat="server">
                               <Settings ShowTitlePanel="false" ShowFooter="false" ShowFilterBar="Hidden" ShowFilterRow="false" />
                            <SettingsPager Mode="ShowAllRecords">
                            </SettingsPager>
                           
                            <Styles Footer-HorizontalAlign="Right" Cell-HorizontalAlign="Right" Header-VerticalAlign="Middle"
                                Header-HorizontalAlign="Center" Header-Wrap="True">
                            </Styles>
                            <Columns>
                                <dx:GridViewDataColumn FieldName="SNo" ExportWidth="30" Width="30" Caption="S.No."
                                    CellStyle-HorizontalAlign="Right">
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="Heads" ExportWidth="100" Width="110" Caption="Name"
                                    FooterCellStyle-HorizontalAlign="Right" CellStyle-HorizontalAlign="Left">
                                </dx:GridViewDataColumn>
                               
                                <dx:GridViewBandColumn Caption="Decree Advances">
                                    <Columns>
                                        <dx:GridViewDataColumn FieldName="Degree_Credit" ExportWidth="35" Width="35" Caption="Credit"
                                            FooterCellStyle-HorizontalAlign="Right" CellStyle-HorizontalAlign="Right">
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn FieldName="Degree_Debit" ExportWidth="35" Width="35" Caption="Debit"
                                            FooterCellStyle-HorizontalAlign="Right" CellStyle-HorizontalAlign="Right">
                                        </dx:GridViewDataColumn>
                                    </Columns>
                                </dx:GridViewBandColumn>
                                <dx:GridViewBandColumn Caption="Advocate Advance">
                                    <Columns>
                                        <dx:GridViewDataColumn FieldName="Advocate_Credit" ExportWidth="30" Width="30" Caption="Credit"
                                            FooterCellStyle-HorizontalAlign="Right" CellStyle-HorizontalAlign="Right">
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn FieldName="Advocate_Debit" ExportWidth="30" Width="30" Caption="Debit"
                                            FooterCellStyle-HorizontalAlign="Right" CellStyle-HorizontalAlign="Right">
                                        </dx:GridViewDataColumn>
                                    </Columns>
                                </dx:GridViewBandColumn>
                                <dx:GridViewBandColumn Caption="A/C Advances">
                                    <Columns>
                                        <dx:GridViewDataColumn FieldName="ACA_Credit" ExportWidth="30" Width="30" Caption="Credit"
                                            FooterCellStyle-HorizontalAlign="Right" CellStyle-HorizontalAlign="Right">
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn FieldName="ACA_Debit" ExportWidth="30" Width="30" Caption="Debit"
                                            FooterCellStyle-HorizontalAlign="Right" CellStyle-HorizontalAlign="Right">
                                        </dx:GridViewDataColumn>
                                    </Columns>
                                </dx:GridViewBandColumn>
                                 <dx:GridViewBandColumn Caption="Vehicle Advance">
                                    <Columns>
                                        <dx:GridViewDataColumn FieldName="VA_Credit" ExportWidth="40" Width="40" Caption="Credit"
                                            FooterCellStyle-HorizontalAlign="Right" CellStyle-HorizontalAlign="Right">
                                            <CellStyle HorizontalAlign="Right">
                                            </CellStyle>
                                            <FooterCellStyle HorizontalAlign="Right">
                                            </FooterCellStyle>
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn FieldName="VA_Debit" ExportWidth="40" Width="40" Caption="Debit"
                                            FooterCellStyle-HorizontalAlign="Right" CellStyle-HorizontalAlign="Right">
                                            <CellStyle HorizontalAlign="Right">
                                            </CellStyle>
                                            <FooterCellStyle HorizontalAlign="Right">
                                            </FooterCellStyle>
                                        </dx:GridViewDataColumn>
                                    </Columns>
                                 </dx:GridViewBandColumn>
                               <%-- <dx:GridViewBandColumn Caption="Degree Advances">
                                    <Columns>
                                        <dx:GridViewDataColumn FieldName="Degree_Credit" ExportWidth="30" Width="30" Caption="Credit"
                                            FooterCellStyle-HorizontalAlign="Right" CellStyle-HorizontalAlign="Right">
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn FieldName="Degree_Debit" ExportWidth="30" Width="30" Caption="Debit"
                                            FooterCellStyle-HorizontalAlign="Right" CellStyle-HorizontalAlign="Right">
                                        </dx:GridViewDataColumn>
                                    </Columns>
                                </dx:GridViewBandColumn>--%>
                               <%-- <dx:GridViewBandColumn Caption="Advocate Advances">
                                    <Columns>
                                        <dx:GridViewDataColumn FieldName="Advocate_Credit" ExportWidth="30" Width="30" Caption="Credit"
                                            FooterCellStyle-HorizontalAlign="Right" CellStyle-HorizontalAlign="Right">
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn FieldName="Advocate_Debit" ExportWidth="30" Width="30" Caption="Debit"
                                            FooterCellStyle-HorizontalAlign="Right" CellStyle-HorizontalAlign="Right">
                                        </dx:GridViewDataColumn>
                                    </Columns>
                                </dx:GridViewBandColumn>--%>
                                <%--<dx:GridViewBandColumn Caption="Vehicle Recovery Advances">
                                    <Columns>
                                        <dx:GridViewDataColumn FieldName="VRA_Credit" ExportWidth="30" Caption="Credit" FooterCellStyle-HorizontalAlign="Right"
                                            CellStyle-HorizontalAlign="Right">
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn FieldName="VRA_Debit" ExportWidth="30" Caption="Debit" FooterCellStyle-HorizontalAlign="Right"
                                            CellStyle-HorizontalAlign="Right">
                                        </dx:GridViewDataColumn>
                                    </Columns>
                                </dx:GridViewBandColumn>--%>
                                 <dx:GridViewBandColumn Caption="Court Advances">
                                    <Columns>
                                        <dx:GridViewDataColumn FieldName="Cort_Credit" ExportWidth="30" Caption="Credit" FooterCellStyle-HorizontalAlign="Right"
                                            CellStyle-HorizontalAlign="Right">
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn FieldName="Cort_Debit" ExportWidth="30" Caption="Debit" FooterCellStyle-HorizontalAlign="Right"
                                            CellStyle-HorizontalAlign="Right">
                                        </dx:GridViewDataColumn>
                                    </Columns>
                                </dx:GridViewBandColumn>

                                <dx:GridViewBandColumn Caption="Press Advances">
                                    <Columns>
                                        <dx:GridViewDataColumn FieldName="PA_Credit" ExportWidth="30" Caption="Credit" FooterCellStyle-HorizontalAlign="Right"
                                            CellStyle-HorizontalAlign="Right">
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn FieldName="PA_Debit" ExportWidth="30" Caption="Debit" FooterCellStyle-HorizontalAlign="Right"
                                            CellStyle-HorizontalAlign="Right">
                                        </dx:GridViewDataColumn>
                                    </Columns>
                                </dx:GridViewBandColumn>

                                <dx:GridViewBandColumn Caption="Calendar Advances">
                                    <Columns>
                                        <dx:GridViewDataColumn FieldName="CAL_Credit" ExportWidth="30" Caption="Credit" FooterCellStyle-HorizontalAlign="Right"
                                            CellStyle-HorizontalAlign="Right">
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn FieldName="CAL_Debit" ExportWidth="30" Caption="Debit" FooterCellStyle-HorizontalAlign="Right"
                                            CellStyle-HorizontalAlign="Right">
                                        </dx:GridViewDataColumn>
                                    </Columns>
                                </dx:GridViewBandColumn>

                                  <dx:GridViewBandColumn Caption="Staff Missapropriation A/C">
                                    <Columns>
                                        <dx:GridViewDataColumn FieldName="STMISS_Credit" ExportWidth="30" Caption="Credit" FooterCellStyle-HorizontalAlign="Right"
                                            CellStyle-HorizontalAlign="Right">
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn FieldName="STMISS_Debit" ExportWidth="30" Caption="Debit" FooterCellStyle-HorizontalAlign="Right"
                                            CellStyle-HorizontalAlign="Right">
                                        </dx:GridViewDataColumn>
                                    </Columns>
                                </dx:GridViewBandColumn>


                                <dx:GridViewDataColumn FieldName="Remarks" ExportWidth="100" Width="100" Caption="On account of"
                                    FooterCellStyle-HorizontalAlign="Right" CellStyle-HorizontalAlign="Left">
                                </dx:GridViewDataColumn>
                            </Columns>
                        </dx:ASPxGridView>
                        <dx:ASPxGridViewExporter ID="exportSundriesandAdvancesPart1" runat="server" GridViewID="gridSundriesandAdvancesPart1">
                            <Styles>
                                <Title Font-Size="Smaller" Paddings-PaddingBottom="3px" Paddings-PaddingTop="3px">
                                </Title>
                                <Header Wrap="True" Font-Size="Smaller" Paddings-PaddingBottom="3px" Paddings-PaddingTop="3px"
                                    HorizontalAlign="Center">
                                </Header>
                                <Cell Paddings-Padding="3px" Font-Size="Smaller" Wrap="True">
                                </Cell>
                                <Footer Font-Size="Smaller" Paddings-Padding="3px" Wrap="True">
                                </Footer>
                            </Styles>
                        </dx:ASPxGridViewExporter>
                       <dx:ASPxGridViewExporter ID="gridSundriesandAdvancesPart1excel" GridViewID="gridSundriesandAdvancesPart1" 

    PaperKind="A4" Landscape="True"  runat="server" >
</dx:ASPxGridViewExporter>
                        <dx:ASPxGridViewExporter ID="exportSundriesandAdvancesPart2" runat="server" GridViewID="gridSundriesandAdvancesPart2">
                            <Styles>
                                <Title Font-Size="Smaller" Paddings-PaddingBottom="3px" Paddings-PaddingTop="3px">
                                </Title>
                                <Header Wrap="True" Font-Size="Smaller" Paddings-PaddingBottom="3px" Paddings-PaddingTop="3px"
                                    HorizontalAlign="Center">
                                </Header>
                                <Cell Paddings-Padding="3px" Font-Size="Smaller" Wrap="True">
                                </Cell>
                                <Footer Font-Size="Smaller" Paddings-Padding="3px" Wrap="True">
                                </Footer>
                            </Styles>
                        </dx:ASPxGridViewExporter>
                     <%--   <dx:ASPxGridViewExporter ID="gridSundriesandAdvancesPart2excel" GridViewID="gridSundriesandAdvancesPart2" 

    PaperKind="A4" Landscape="True"  runat="server" >
</dx:ASPxGridViewExporter>--%>
                    </div>


                    <div class="row">
                        <dx:ASPxGridView AutoGenerateColumns="false" Style="margin: 0 auto; width: 100%;"
                            ID="gridForeman" ClientInstanceName="gridForeman" runat="server">
                            <SettingsText Title="PARTICULARS OF FOREMAN CHITS" />
                            <Settings ShowTitlePanel="true" ShowFooter="false" ShowFilterBar="Hidden" ShowFilterRow="false" />
                            <SettingsPager Mode="ShowAllRecords">
                            </SettingsPager>
                            <Styles Footer-HorizontalAlign="Right" Cell-HorizontalAlign="Right" Header-VerticalAlign="Middle"
                                Header-HorizontalAlign="Center" Header-Wrap="True">
                            </Styles>
                            <Columns>
                                <dx:GridViewDataColumn FieldName="SNo" Caption="S.No." CellStyle-HorizontalAlign="Right">
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="ChitNumber" Caption="Chit Number" FooterCellStyle-HorizontalAlign="Right"
                                    CellStyle-HorizontalAlign="Left">
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="PrizeMoney" Caption="Prize Money" FooterCellStyle-HorizontalAlign="Right"
                                    CellStyle-HorizontalAlign="Right">
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="CallAmount" ExportWidth="120" HeaderStyle-Wrap="True" Caption="Call Amount Paid for Prized Chits"
                                    FooterCellStyle-HorizontalAlign="Right" CellStyle-HorizontalAlign="Right">
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="BalancePayable" ExportWidth="100" Caption="Balance Payable" FooterCellStyle-HorizontalAlign="Right"
                                    CellStyle-HorizontalAlign="Right">
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn Caption="Call Amount paid for Non Prized Chits" FieldName="CallAmountPaid" ExportWidth="120" HeaderStyle-Wrap="True" FooterCellStyle-HorizontalAlign="Right"
                                    CellStyle-HorizontalAlign="Right">
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="NoofInstalmentsPaid" ExportWidth="100" Caption="No.of Instalments Paid"
                                    FooterCellStyle-HorizontalAlign="Right" CellStyle-HorizontalAlign="Center">
                                </dx:GridViewDataColumn>
                            </Columns>
                        </dx:ASPxGridView>
                        <dx:ASPxGridViewExporter ID="exportForeman" runat="server" GridViewID="gridForeman">
                           <%-- <Styles>
                                <Title Paddings-PaddingBottom="5px" Paddings-PaddingTop="5px" Font-Size="6"></Title>
                                <Header Wrap="True" Paddings-PaddingBottom="5px" Paddings-PaddingTop="5px" HorizontalAlign="Center" Font-Size="6">
                                </Header>
                                <Cell Paddings-PaddingBottom="5px" Wrap="True" Font-Size="6">
                                </Cell>
                                <Footer Paddings-Padding="5px" Wrap="True" Font-Size="6">
                                </Footer>
                            </Styles>--%>
                            <Styles>
                                <Title Paddings-PaddingBottom="5px" Paddings-PaddingTop="5px"></Title>
                                <Header Wrap="True" Paddings-PaddingBottom="5px" Paddings-PaddingTop="5px" HorizontalAlign="Center">
                                </Header>
                                <Cell Paddings-Padding="5px" Wrap="True">
                                </Cell>
                                <Footer Paddings-Padding="5px" Wrap="True">
                                </Footer>
                            </Styles>
                        </dx:ASPxGridViewExporter>
                          <dx:ASPxGridViewExporter ID="gridForemanexcel" GridViewID="gridForeman" 

    PaperKind="A4" Landscape="True"  runat="server" >
</dx:ASPxGridViewExporter>
                    </div>
                    <div class="row">
                        <dx:ASPxGridView AutoGenerateColumns="false" Style="margin: 0 auto; width: 100%;"
                            ID="gridChitTrialRun" ClientInstanceName="gridChitTrialRun" runat="server">
                            <SettingsText Title="GROUPWAR CHIT TRIAL STATEMENT(Running)" />
                            <Settings ShowTitlePanel="true" ShowFooter="false" ShowFilterBar="Hidden" ShowFilterRow="false" />
                            
                            <SettingsPager Mode="ShowAllRecords">
                            </SettingsPager>
                            <Styles Footer-HorizontalAlign="Right" Cell-HorizontalAlign="Right" Header-VerticalAlign="Middle"
                                Header-HorizontalAlign="Center" Header-Wrap="True">
                            </Styles>
                            <Columns>
                                <dx:GridViewDataColumn FieldName="SNo" Caption="S.No." CellStyle-HorizontalAlign="Right">
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="GroupNo" Caption="Name" FooterCellStyle-HorizontalAlign="Right"
                                    CellStyle-HorizontalAlign="Left">
                                </dx:GridViewDataColumn>
                                <dx:GridViewBandColumn Caption="Gross Amount Including Dividend">
                                    <Columns>
                                        <dx:GridViewDataColumn FieldName="I_Credit" Caption="Credit" FooterCellStyle-HorizontalAlign="Right"
                                            CellStyle-HorizontalAlign="Right">
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn FieldName="I_Debit" Caption="Debit" FooterCellStyle-HorizontalAlign="Right"
                                            CellStyle-HorizontalAlign="Right">
                                        </dx:GridViewDataColumn>
                                    </Columns>
                                </dx:GridViewBandColumn>
                                <dx:GridViewBandColumn Caption="Gross Amount Excluding Dividend">
                                    <Columns>
                                        <dx:GridViewDataColumn FieldName="E_Credit" Caption="Credit" FooterCellStyle-HorizontalAlign="Right"
                                            CellStyle-HorizontalAlign="Right">
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn FieldName="E_Debit" Caption="Debit" FooterCellStyle-HorizontalAlign="Right"
                                            CellStyle-HorizontalAlign="Right">
                                        </dx:GridViewDataColumn>
                                    </Columns>
                                </dx:GridViewBandColumn>
                                <dx:GridViewBandColumn Caption="NET">
                                    <Columns>
                                        <dx:GridViewDataColumn FieldName="N_Credit" Caption="Credit" FooterCellStyle-HorizontalAlign="Right"
                                            CellStyle-HorizontalAlign="Right">
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn FieldName="N_Debit" Caption="Debit" FooterCellStyle-HorizontalAlign="Right"
                                            CellStyle-HorizontalAlign="Right">
                                        </dx:GridViewDataColumn>
                                    </Columns>
                                </dx:GridViewBandColumn>
                                <dx:GridViewBandColumn Caption="Dividend">
                                    <Columns>
                                        <dx:GridViewDataColumn FieldName="NonPrized" Caption="Non Prized" FooterCellStyle-HorizontalAlign="Right"
                                            CellStyle-HorizontalAlign="Right">
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn FieldName="Prized" Caption="Prized" FooterCellStyle-HorizontalAlign="Right"
                                            CellStyle-HorizontalAlign="Right">
                                        </dx:GridViewDataColumn>
                                    </Columns>
                                </dx:GridViewBandColumn>
                                <dx:GridViewDataColumn FieldName="TotalAmountofKasar" Caption="Total Amount of Dividend"
                                    FooterCellStyle-HorizontalAlign="Right" CellStyle-HorizontalAlign="Right">
                                </dx:GridViewDataColumn>
                                <dx:GridViewBandColumn Caption="Total Number">
                                    <Columns>
                                        <dx:GridViewDataColumn FieldName="NP" Caption="NP" FooterCellStyle-HorizontalAlign="Right"
                                            CellStyle-HorizontalAlign="Right">
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn FieldName="P" Caption="P" FooterCellStyle-HorizontalAlign="Right"
                                            CellStyle-HorizontalAlign="Right">
                                        </dx:GridViewDataColumn>
                                    </Columns>
                                </dx:GridViewBandColumn>
                                <dx:GridViewDataColumn FieldName="Remarks" Caption="Remarks" FooterCellStyle-HorizontalAlign="Right"
                                    CellStyle-HorizontalAlign="Right">
                                </dx:GridViewDataColumn>
                            </Columns>
                        </dx:ASPxGridView>
                        <dx:ASPxGridViewExporter ID="exportChitTrialRun" runat="server" GridViewID="gridChitTrialRun">
                            <Styles>
                                <Title Paddings-PaddingBottom="5px" Paddings-PaddingTop="5px"></Title>
                                <Header Wrap="True" Paddings-PaddingBottom="5px" Paddings-PaddingTop="5px" HorizontalAlign="Center">
                                </Header>
                                <Cell Paddings-Padding="5px" Wrap="True">
                                </Cell>
                                <Footer Paddings-Padding="5px" Wrap="True">
                                </Footer>
                            </Styles>
                        </dx:ASPxGridViewExporter>
                          <dx:ASPxGridViewExporter ID="gridChitTrialRunexcel" GridViewID="gridChitTrialRun" 

    PaperKind="A4" Landscape="True"  runat="server" >
</dx:ASPxGridViewExporter>
                    </div>
                    <div class="row">
                        <dx:ASPxGridView AutoGenerateColumns="false" Style="margin: 0 auto; width: 100%;"
                            ID="gridAdditions" ClientInstanceName="gridAdditions" runat="server">
                            <Settings ShowTitlePanel="true" ShowFooter="true" ShowFilterBar="Hidden" ShowFilterRow="false" />
                            <SettingsPager Mode="ShowAllRecords">
                            </SettingsPager>
                            <Styles Footer-HorizontalAlign="Right" Cell-HorizontalAlign="Right" Header-VerticalAlign="Middle"
                                Header-HorizontalAlign="Center" Header-Wrap="True">
                            </Styles>
                            <Columns>
                                <dx:GridViewDataColumn FieldName="SNo" ExportWidth="50" Caption="S.No." CellStyle-HorizontalAlign="Right">
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="Investment" ExportWidth="120" Caption="Main Item of Investment" FooterCellStyle-HorizontalAlign="Right"
                                    CellStyle-HorizontalAlign="Left">
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="additions" ExportWidth="120" Caption="ADDITIONS(Description)" FooterCellStyle-HorizontalAlign="Right"
                                    CellStyle-HorizontalAlign="Right">
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="qty" ExportWidth="70" Caption="Qty" FooterCellStyle-HorizontalAlign="Right"
                                    CellStyle-HorizontalAlign="Right">
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="amount" ExportWidth="70" Caption="Amount" FooterCellStyle-HorizontalAlign="Right"
                                    CellStyle-HorizontalAlign="Right">
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="date" ExportWidth="70" Caption="Date of Purchase" FooterCellStyle-HorizontalAlign="Right"
                                    CellStyle-HorizontalAlign="Right">
                                </dx:GridViewDataColumn>
                            </Columns>
                        </dx:ASPxGridView>
                        <dx:ASPxGridViewExporter ID="exportAdditions" runat="server" GridViewID="gridAdditions">
                            <Styles>
                                <Title Paddings-PaddingBottom="5px" Paddings-PaddingTop="5px"></Title>
                                <Header Wrap="True" Paddings-PaddingBottom="5px" Paddings-PaddingTop="5px" HorizontalAlign="Center">
                                </Header>
                                <Cell Paddings-Padding="5px" Wrap="True">
                                </Cell>
                                <Footer Paddings-Padding="5px" Wrap="True">
                                </Footer>
                            </Styles>
                        </dx:ASPxGridViewExporter>
                        <dx:ASPxGridViewExporter ID="gridAdditionsexcel" GridViewID="gridAdditions" 

    PaperKind="A4" Landscape="True"  runat="server" >
</dx:ASPxGridViewExporter>
                    </div>
                    <div class="row">
                        <dx:ASPxGridView AutoGenerateColumns="false" Style="margin: 0 auto; width: 100%;"
                            ID="gridDeductions" ClientInstanceName="gridDeductions" runat="server">
                            <Settings ShowTitlePanel="true" ShowFooter="true" ShowFilterBar="Hidden" ShowFilterRow="false" />
                            <SettingsPager Mode="ShowAllRecords">
                            </SettingsPager>
                            <Styles Footer-HorizontalAlign="Right" Cell-HorizontalAlign="Right" Header-VerticalAlign="Middle"
                                Header-HorizontalAlign="Center" Header-Wrap="True">
                            </Styles>
                            <Columns>
                                <dx:GridViewDataColumn FieldName="SNo" Caption="S.No." ExportWidth="40" CellStyle-HorizontalAlign="Right">
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="Investment" ExportWidth="104" Caption="Main Item of Investment" FooterCellStyle-HorizontalAlign="Right"
                                    CellStyle-HorizontalAlign="Left">
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="deductions" ExportWidth="105" Caption="DEDUCTIONS(Description)" FooterCellStyle-HorizontalAlign="Right"
                                    CellStyle-HorizontalAlign="Right">
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="qty" ExportWidth="60" Caption="Qty" FooterCellStyle-HorizontalAlign="Right"
                                    CellStyle-HorizontalAlign="Right">
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="amount" ExportWidth="60" Caption="Amount" FooterCellStyle-HorizontalAlign="Right"
                                    CellStyle-HorizontalAlign="Right">
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="date" ExportWidth="60" Caption="Date of Sale" FooterCellStyle-HorizontalAlign="Right"
                                    CellStyle-HorizontalAlign="Right">
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn ExportWidth="70" FieldName="saleamount" Caption="Sale amount & Depreciation"
                                    FooterCellStyle-HorizontalAlign="Right" CellStyle-HorizontalAlign="Right">
                                </dx:GridViewDataColumn>
                            </Columns>
                        </dx:ASPxGridView>
                        <dx:ASPxGridViewExporter ID="exportDeductions" runat="server" GridViewID="gridDeductions">
                            <Styles>
                                <Title Paddings-PaddingBottom="5px" Paddings-PaddingTop="5px"></Title>
                                <Header Wrap="True" Paddings-PaddingBottom="5px" Paddings-PaddingTop="5px" HorizontalAlign="Center">
                                </Header>
                                <Cell Paddings-Padding="5px" Wrap="True">
                                </Cell>
                                <Footer Paddings-Padding="5px" Wrap="True">
                                </Footer>
                            </Styles>
                        </dx:ASPxGridViewExporter>
                        <dx:ASPxGridViewExporter ID="gridDeductionsexcel" GridViewID="gridDeductions" 

    PaperKind="A4" Landscape="True"  runat="server" >
</dx:ASPxGridViewExporter>
                    </div>
                       <div class="row">
                        <dx:ASPxGridView AutoGenerateColumns="false" Style="margin: 0 auto; width: 100%;"
                            ID="gridabstract" ClientInstanceName="gridabstract" runat="server">
                            <Settings ShowTitlePanel="true" ShowFooter="false" ShowFilterBar="Hidden" ShowFilterRow="false" />
                            <SettingsText Title="Trial Balance Of abstract" />
                            <SettingsPager Mode="ShowAllRecords">
                            </SettingsPager>
                            <Styles Footer-HorizontalAlign="Right" Cell-HorizontalAlign="Right" Header-VerticalAlign="Middle"
                                Header-HorizontalAlign="Center" Header-Wrap="True">
                            </Styles>
                            <Columns>                         
                                <dx:GridViewDataColumn Width="10%" ExportWidth="50" FieldName="SNo" Caption="S.No." >
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn Width="20%" ExportWidth="150" FieldName="heads" Caption="Abstract" FooterCellStyle-HorizontalAlign="Right" CellStyle-HorizontalAlign="Left">
                                </dx:GridViewDataColumn>
                               <%-- <dx:GridViewDataColumn Width="30%" ExportWidth="150" FieldName="Narration" Caption="DESCRIPTION" FooterCellStyle-HorizontalAlign="Right"
                                    CellStyle-HorizontalAlign="Left" >
                                </dx:GridViewDataColumn>--%>
                                 <dx:GridViewDataColumn Width="25%" ExportWidth="150" FieldName="Credit" FooterCellStyle-HorizontalAlign="Right"
                                    CellStyle-HorizontalAlign="Right">
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn Width="25%" ExportWidth="150" FieldName="Debit" FooterCellStyle-HorizontalAlign="Right"
                                    CellStyle-HorizontalAlign="Right">
                                     </dx:GridViewDataColumn>
                            </Columns>
                        </dx:ASPxGridView>
                        <dx:ASPxGridViewExporter ID="exportabstract" runat="server" GridViewID="gridabstract">
                            <Styles>
                                <Title Paddings-PaddingBottom="5px" Paddings-PaddingTop="5px"></Title>
                                <Header Wrap="True" Paddings-PaddingBottom="5px" Paddings-PaddingTop="5px" >
                                </Header>
                                <Cell  Paddings-Padding="5px" >
                                </Cell>
                                <Footer Paddings-Padding="5px">
                                </Footer>
                            </Styles>
                        </dx:ASPxGridViewExporter>
                         <dx:ASPxGridViewExporter ID="exportabstractexce" GridViewID="gridabstract" 

    PaperKind="A4" Landscape="True"  runat="server" >
</dx:ASPxGridViewExporter>
                    </div>
                    <div class="row">
                        <dx:ASPxGridView AutoGenerateColumns="false" Style="margin: 0 auto; width: 100%;"
                            ID="gridFiling" ClientInstanceName="gridFiling" runat="server">
                            <Settings ShowTitlePanel="true" ShowFooter="true" ShowFilterBar="Hidden" ShowFilterRow="false" />
                            <SettingsPager Mode="ShowAllRecords">
                            </SettingsPager>
                            <Styles Footer-HorizontalAlign="Right" Cell-HorizontalAlign="Right" Header-VerticalAlign="Middle"
                                Header-HorizontalAlign="Center" Header-Wrap="True">
                            </Styles>
                            <TotalSummary>
                                <dx:ASPxSummaryItem FieldName="choosendate" DisplayFormat="Total" SummaryType="Sum" />
                                <dx:ASPxSummaryItem FieldName="fees" DisplayFormat="{0}" SummaryType="Sum" />
                            </TotalSummary>
                            <Columns>
                                <dx:GridViewDataColumn FieldName="SNo" ExportWidth="30" Caption="S.No." CellStyle-HorizontalAlign="Right">
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="ChitNumber" ExportWidth="80" Caption="Chit Number" 
                                    CellStyle-HorizontalAlign="Left">
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="details" ExportWidth="250" Caption="Details of Documents filed like P.S.O. Agreement, Minutes, Removal and substitution"
                                    CellStyle-HorizontalAlign="Left">
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="nameofperson" ExportWidth="90" Caption="Name of the Person" 
                                    CellStyle-HorizontalAlign="Left">
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="foreman" ExportWidth="60" Caption="Recognized by Foreman" 
                                    CellStyle-HorizontalAlign="Left">
                                </dx:GridViewDataColumn>
                                <dx:GridViewBandColumn Caption="In-case of Filing of minutes">
                                    <Columns>
                                        <dx:GridViewDataColumn FieldName="auctiondate" ExportWidth="70" Caption="Date of Auction">
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn FieldName="instnumber" ExportWidth="70" Caption="Instalment No">
                                        </dx:GridViewDataColumn>
                                    </Columns>
                                </dx:GridViewBandColumn>
                                <dx:GridViewDataColumn FieldName="choosendate" ExportWidth="70" Caption="Date of Filing of Document">
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="fees" ExportWidth="80" Caption="Filing Fees" CellStyle-HorizontalAlign="Right">
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="remarks" ExportWidth="60" Caption="Remarks" CellStyle-HorizontalAlign="Left">
                                </dx:GridViewDataColumn>
                            </Columns>
                        </dx:ASPxGridView>
                        <dx:ASPxGridViewExporter ID="exportFiling" runat="server" GridViewID="gridFiling">
                            <Styles>
                                <Title Paddings-PaddingBottom="3px" Paddings-PaddingTop="3px"></Title>
                                <Header Wrap="True" Paddings-PaddingBottom="3px" Paddings-PaddingTop="3px" HorizontalAlign="Center">
                                </Header>
                                <Cell Paddings-Padding="3px" Font-Size="Medium" Wrap="True">
                                </Cell>
                                <Footer Paddings-Padding="3px" Wrap="True">
                                </Footer>
                            </Styles>
                        </dx:ASPxGridViewExporter>
                          <dx:ASPxGridViewExporter ID="gridFilingexcel" GridViewID="gridFiling" 

    PaperKind="A4" Landscape="True"  runat="server" >
</dx:ASPxGridViewExporter>

                    </div>
                    <div class="row">
                        <dx:ASPxGridView AutoGenerateColumns="false" Style="margin: 0 auto; width: 100%;"
                            ID="gridDeposit" ClientInstanceName="gridDeposit" runat="server">
                            <Settings ShowTitlePanel="true" ShowFooter="true" ShowFilterBar="Hidden" ShowFilterRow="false" />
                            <SettingsPager Mode="ShowAllRecords">
                            </SettingsPager>
                            <Styles Footer-HorizontalAlign="Right" Cell-HorizontalAlign="Right" Header-VerticalAlign="Middle"
                                Header-HorizontalAlign="Center" Header-Wrap="True">
                            </Styles>
                            <TotalSummary>
                                <dx:ASPxSummaryItem FieldName="choosendate" DisplayFormat="Total" SummaryType="Sum" />
                                <dx:ASPxSummaryItem FieldName="prizeamount" DisplayFormat="{0}" SummaryType="Sum" />
                                <dx:ASPxSummaryItem FieldName="futureamount" DisplayFormat="{0}" SummaryType="Sum" />
                                <dx:ASPxSummaryItem FieldName="nAmount" DisplayFormat="{0}" SummaryType="Sum" />
                                <dx:ASPxSummaryItem FieldName="amountofwithdrawal" DisplayFormat="{0}" SummaryType="Sum" />
                            </TotalSummary>
                            <Columns>
                                <dx:GridViewDataColumn FieldName="SNo" ExportWidth="30"  Caption="S.No." CellStyle-HorizontalAlign="Right">
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="ChitAgreementNumber" ExportWidth="60" Caption="Chit Agreement Number" 
                                    CellStyle-HorizontalAlign="Left">
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="ChitNumber" ExportWidth="60" Caption="Chit Number" 
                                    CellStyle-HorizontalAlign="Left">
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="nameandaddress" ExportWidth="100" Caption="Name and Full Address of the Subscriber"
                                    CellStyle-HorizontalAlign="Left">
                                </dx:GridViewDataColumn>
                                <dx:GridViewBandColumn Caption="Number of Instalment and Date of Draw">
                                    <Columns>
                                        <dx:GridViewDataColumn FieldName="instno" ExportWidth="30" Caption="No." CellStyle-HorizontalAlign="Left">
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn FieldName="drawrate" ExportWidth="70" Caption="Date" CellStyle-HorizontalAlign="Left">
                                        </dx:GridViewDataColumn>
                                    </Columns>
                                </dx:GridViewBandColumn>
                                <dx:GridViewDataColumn FieldName="prizeamount" ExportWidth="80"  CellStyle-HorizontalAlign="Right" Caption="Amount of Unpaid Prize Amount">
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="futureamount" ExportWidth="70" CellStyle-HorizontalAlign="Right" Caption="Amount of Future subscriptions deducted from the Prize Amount">
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="dateofsubstitute" ExportWidth="80" Caption="Date of Substituting and the amount remitted by the Removed subscriber">
                                </dx:GridViewDataColumn>
                                <dx:GridViewBandColumn Caption="Name of the Approved Bank in which the amounts mentioned in column 7,8 & 9 are deposited">
                                    <Columns>
                                        <dx:GridViewDataColumn FieldName="nbank" ExportWidth="70" Caption="Bank">
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn FieldName="nPlace" ExportWidth="70" Caption="Place">
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn FieldName="nAmount" ExportWidth="70"  CellStyle-HorizontalAlign="Right" Caption="Amount">
                                        </dx:GridViewDataColumn>
                                    </Columns>
                                </dx:GridViewBandColumn>
                                <dx:GridViewDataColumn FieldName="depositdate" ExportWidth="70" Caption="Date of Depostit" CellStyle-HorizontalAlign="Left">
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="datewithdraw" ExportWidth="70" Caption="Date and Purpose of withdrawal" CellStyle-HorizontalAlign="Left">
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="amountofwithdrawal" ExportWidth="70" Caption="Amount of withdrawal" CellStyle-HorizontalAlign="Right">
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="remarks" Caption="Remarks" ExportWidth="60" CellStyle-HorizontalAlign="Left">
                                </dx:GridViewDataColumn>
                            </Columns>
                        </dx:ASPxGridView>
                        <dx:ASPxGridViewExporter ID="exportDeposit" runat="server" GridViewID="gridDeposit">
                            <Styles>
                                <Title Paddings-PaddingBottom="3px" Paddings-PaddingTop="3px"></Title>
                                <Header Wrap="True" Paddings-PaddingBottom="3px" Paddings-PaddingTop="3px" HorizontalAlign="Center">
                                </Header>
                                <Cell Paddings-Padding="3px" Wrap="True">
                                </Cell>
                                <Footer Paddings-Padding="3px" Wrap="True">
                                </Footer>
                            </Styles>
                        </dx:ASPxGridViewExporter>
                          <dx:ASPxGridViewExporter ID="gridDepositexcel" GridViewID="gridDeposit" 

    PaperKind="A4" Landscape="True"  runat="server" >
</dx:ASPxGridViewExporter>

                    </div>
                    <div class="row">
                        <dx:ASPxGridView AutoGenerateColumns="false" Style="margin: 0 auto; width: 100%;"
                            ID="gridDecree" ClientInstanceName="gridDecree" runat="server">
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
                                <dx:GridViewDataColumn Width="90%" FieldName="CC No" ExportWidth="100" FooterCellStyle-HorizontalAlign="Right"
                                    CellStyle-HorizontalAlign="Left">
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn Width="90%" FieldName="EP No./OS No./ARC No./ARB No." ExportWidth="100"
                                    FooterCellStyle-HorizontalAlign="Left" CellStyle-HorizontalAlign="Left">
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn Width="20%" FieldName="ChitName" ExportWidth="100" FooterCellStyle-HorizontalAlign="Right"
                                    CellStyle-HorizontalAlign="Left">
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn Width="20%" FieldName="Name" ExportWidth="150" FooterCellStyle-HorizontalAlign="Right"
                                    CellStyle-HorizontalAlign="Left">
                               </dx:GridViewDataColumn>
                                 <dx:GridViewDataColumn Width="30%" FieldName="Totalamount" ExportWidth="100" FooterCellStyle-HorizontalAlign="Right" 
                                     Caption="Total amount realised upto" CellStyle-HorizontalAlign="Right">
                               </dx:GridViewDataColumn>
                                  <dx:GridViewBandColumn Caption="Last Realisation">
                                    <Columns>
                                        <dx:GridViewDataColumn Width="30%" FieldName="Date" ExportWidth="100" FooterCellStyle-HorizontalAlign="Right"
                                            Caption="Date" CellStyle-HorizontalAlign="Right">
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn Width="30%" FieldName="AmountReceived" ExportWidth="150" FooterCellStyle-HorizontalAlign="Right"
                                            Caption="Amount Received" CellStyle-HorizontalAlign="Right">
                                        </dx:GridViewDataColumn>
                                    </Columns>
                                </dx:GridViewBandColumn>
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
                                <dx:GridViewBandColumn Caption="Advocate Fees">
                                    <Columns>
                                        <dx:GridViewDataColumn Width="30%" FieldName="CreditAdvocate" ExportWidth="100" FooterCellStyle-HorizontalAlign="Right"
                                            Caption="Credit" CellStyle-HorizontalAlign="Right">
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn Width="30%" FieldName="DebitAdvocate" ExportWidth="100" FooterCellStyle-HorizontalAlign="Right"
                                            Caption="Debit" CellStyle-HorizontalAlign="Right">
                                        </dx:GridViewDataColumn>
                                    </Columns>
                                </dx:GridViewBandColumn>
                                <dx:GridViewDataColumn Width="20%" FieldName="" ExportWidth="100" FooterCellStyle-HorizontalAlign="Right"
                                    Caption="Description" CellStyle-HorizontalAlign="Right" Visible="false">
                                </dx:GridViewDataColumn>
                            </Columns>
                        </dx:ASPxGridView>
                        <dx:ASPxGridViewExporter ID="ExportDegree" runat="server" GridViewID="gridDecree">
                            <Styles>
                                <Title Paddings-PaddingBottom="5px" Paddings-PaddingTop="5px"></Title>
                                <Header Wrap="True" Paddings-PaddingBottom="5px" Paddings-PaddingTop="5px" HorizontalAlign="Center">
                                </Header>
                                <Cell Paddings-Padding="6px" Wrap="True">
                                </Cell>
                                <Footer Paddings-Padding="5px" Wrap="True">
                                </Footer>
                            </Styles>
                        </dx:ASPxGridViewExporter>
                          <dx:ASPxGridViewExporter ID="gridDecreeexcel" GridViewID="gridDecree" 

    PaperKind="A4" Landscape="True"  runat="server" >
</dx:ASPxGridViewExporter>

                    </div>
                    <div class="row">
                        <dx:ASPxGridView AutoGenerateColumns="false" Style="margin: 0 auto; width: 100%;"
                            ID="gridStamps" ClientInstanceName="gridStamps" runat="server">
                            <Settings ShowTitlePanel="true" ShowFooter="false" ShowFilterBar="Hidden" ShowFilterRow="false" />
                            <SettingsPager Mode="ShowAllRecords">
                            </SettingsPager>
                            <Styles Footer-HorizontalAlign="Right" Cell-HorizontalAlign="Right" Header-VerticalAlign="Middle"
                                Header-HorizontalAlign="Center" Header-Wrap="True">
                            </Styles>
                            <Columns>
                                <dx:GridViewDataColumn Width="10%" FieldName="SlNo" ExportWidth="50" Caption="S.No."
                                    CellStyle-HorizontalAlign="Right">
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn Width="40%" FieldName="Head" ExportWidth="150" FooterCellStyle-HorizontalAlign="Right"
                                    Caption="PARTICULARS" CellStyle-HorizontalAlign="Left">
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn Width="20%" Caption="Credit" FieldName="Credit" ExportWidth="100" 
                                    FooterCellStyle-HorizontalAlign="Right" CellStyle-HorizontalAlign="Right">
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn Width="20%" FieldName="Debit" Caption="Debit" ExportWidth="100" FooterCellStyle-HorizontalAlign="Right"
                                    CellStyle-HorizontalAlign="Right">
                                </dx:GridViewDataColumn>
                            </Columns>
                        </dx:ASPxGridView>
                        <dx:ASPxGridViewExporter ID="ExportStamps" runat="server" GridViewID="gridStamps">
                            <Styles>
                                <Title Paddings-PaddingBottom="5px" Paddings-PaddingTop="5px"></Title>
                                <Header Wrap="True" Paddings-PaddingBottom="4px" Paddings-PaddingTop="4px" HorizontalAlign="Center">
                                </Header>
                                <Cell Paddings-Padding="4px" Wrap="True">
                                </Cell>
                                <Footer Paddings-Padding="4px" Wrap="True">
                                </Footer>
                            </Styles>
                        </dx:ASPxGridViewExporter>
                          <dx:ASPxGridViewExporter ID="gridStampsexcel" GridViewID="gridStamps" 

    PaperKind="A4" Landscape="True"  runat="server" >
</dx:ASPxGridViewExporter>

                    </div>
                    <div class="row">
                        <dx:ASPxGridView AutoGenerateColumns="false" Style="margin: 0 auto; width: 100%;"
                            ID="gridChitAbstract" ClientInstanceName="gridChitAbstract" runat="server">
                            <Settings ShowTitlePanel="true" ShowFooter="false" ShowFilterBar="Hidden" ShowFilterRow="false" />
                            <SettingsPager Mode="ShowAllRecords">
                            </SettingsPager>
                            <Styles Footer-HorizontalAlign="Right" Cell-HorizontalAlign="Right" Header-VerticalAlign="Middle"
                                Header-HorizontalAlign="Center" Header-Wrap="True">
                            </Styles>
                            <Columns>
                                <dx:GridViewDataColumn Width="10%" FieldName="Slno" Caption="" ExportWidth="50" 
                                    CellStyle-HorizontalAlign="Right">
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn Width="40%" FieldName="Abstract" ExportWidth="150" FooterCellStyle-HorizontalAlign="Right"
                                    Caption="Abstract" CellStyle-HorizontalAlign="Left">
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn Width="20%" Caption="Credit" FieldName="Credit" ExportWidth="100" 
                                    FooterCellStyle-HorizontalAlign="Right" CellStyle-HorizontalAlign="Right">
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn Width="20%" FieldName="Debit" Caption="Debit" ExportWidth="100" FooterCellStyle-HorizontalAlign="Right"
                                    CellStyle-HorizontalAlign="Right">
                                </dx:GridViewDataColumn>
                            </Columns>
                        </dx:ASPxGridView>
                        <dx:ASPxGridViewExporter ID="exportChitAbstract" runat="server" GridViewID="gridChitAbstract">
                            <Styles>
                                <Title Paddings-PaddingBottom="5px" Paddings-PaddingTop="5px"></Title>
                                <Header Wrap="True" Paddings-PaddingBottom="5px" Paddings-PaddingTop="5px" HorizontalAlign="Center">
                                </Header>
                                <Cell Paddings-Padding="5px" Wrap="True">
                                </Cell>
                                <Footer Paddings-Padding="5px" Wrap="True">
                                </Footer>
                            </Styles>
                        </dx:ASPxGridViewExporter>
                          <dx:ASPxGridViewExporter ID="gridChitAbstractexcel" GridViewID="gridChitAbstract" 

    PaperKind="A4" Landscape="True"  runat="server" >
</dx:ASPxGridViewExporter>

                    </div>


                    <div class="row">
                      <div class="six columns sortable ui-sortable dragsortable">
                        <div class="box_c">   
                            <%-- <div class="box_c_heading cf box_actions noprint">
                                    <p>
                                        </p>
                                </div> --%>
                             <%--<div class="box_c_content">  --%>                
                        <dx:ASPxGridView AutoGenerateColumns="false" Style="margin: 0 auto; width: 100%;"
                            ID="gridBPPCurrent" ClientInstanceName="gridBPPCurrent" runat="server">
                            <Settings ShowTitlePanel="true" ShowFooter="true" ShowFilterBar="Hidden" ShowFilterRow="false" />
                             <SettingsText Title="BPPCurrent" />
                            <SettingsPager Mode="ShowAllRecords">
                            </SettingsPager>
                         <%--   <Styles Footer-HorizontalAlign="Right" Cell-HorizontalAlign="Right" Header-VerticalAlign="Middle"
                                Header-HorizontalAlign="Center" Header-Wrap="True">
                            </Styles>--%>
                            <Columns>
                                <dx:GridViewDataColumn Width="05%" FieldName="SlNo"  ExportWidth="50" Caption="S.No."
                                    CellStyle-HorizontalAlign="Right">
                                </dx:GridViewDataColumn>
                                 <dx:GridViewDataColumn Width="10%"  FieldName="Month" ExportWidth="100" Caption="Date of commencement"
                                    CellStyle-HorizontalAlign="Right">
                                </dx:GridViewDataColumn>
                                   <dx:GridViewDataColumn Width="10%" FieldName="GroupNo"  ExportWidth="100" Caption="Group.No"
                                    CellStyle-HorizontalAlign="left">
                                </dx:GridViewDataColumn>
                                  <dx:GridViewDataColumn Width="10%" FieldName="ChitValue"  ExportWidth="100" Caption="ChitValue"
                                    CellStyle-HorizontalAlign="Right">
                                </dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn Width="10%" FieldName="Members"  ExportWidth="60" Caption="Member"
                                    CellStyle-HorizontalAlign="Right">
                                </dx:GridViewDataColumn>
                                   <dx:GridViewDataColumn Width="10%" FieldName="Payable"   ExportWidth="100" Caption="Payable"
                                    CellStyle-HorizontalAlign="Right">
                                </dx:GridViewDataColumn>
                                
                                <dx:GridViewDataColumn Width="10%" FieldName="PaidPayable"   ExportWidth="100" Caption="PAid Upto....."
                                    CellStyle-HorizontalAlign="Right">
                                </dx:GridViewDataColumn>
                                 <%--<dx:GridViewDataColumn Width="0%"   ExportWidth="50" Caption="Balance Payable" 
                                    CellStyle-HorizontalAlign="Left">
                                </dx:GridViewDataColumn>--%>
                                  <dx:GridViewDataColumn Width="100%" FieldName="Name"  ExportWidth="300" Caption="Name Of the staff"
                                    CellStyle-HorizontalAlign="left">
                                </dx:GridViewDataColumn>
                                 <dx:GridViewDataColumn Width="10%" FieldName="Total"  ExportWidth="100" Caption="Total Amount Paid"
                                    CellStyle-HorizontalAlign="Right">
                                </dx:GridViewDataColumn>
                              <%--  <dx:GridViewDataColumn Width="10%"  ExportWidth="50" Caption="Administrative Office Sanction Purticulars"
                                    CellStyle-HorizontalAlign="Right">
                                </dx:GridViewDataColumn>--%>
                                   <dx:GridViewDataColumn Width="10%" FieldName="ApprovalDate"  ExportWidth="300" Caption="Approval Date/Approval Number"
                                    CellStyle-HorizontalAlign="Right">
                                </dx:GridViewDataColumn>
                                 <%--<dx:GridViewDataColumn Width="10%" FieldName="ApprovalNo"  ExportWidth="100" Caption="Approval Number"
                                    CellStyle-HorizontalAlign="Right">
                                </dx:GridViewDataColumn>   --%>                             
                            </Columns>
                               <%--<Styles Footer-HorizontalAlign="Left" Cell-HorizontalAlign="Left" Header-VerticalAlign="Middle"
                                            Header-HorizontalAlign="Center" Header-Wrap="True">
                                            <GroupPanel Wrap="True" HorizontalAlign="Left">
                                            </GroupPanel>
                                            <GroupRow Wrap="True" HorizontalAlign="Left">
                                            </GroupRow>
                                        </Styles>--%>
                        </dx:ASPxGridView>
                                
                            <dx:ASPxGridViewExporter ID="ExportBPP" runat="server" GridViewID="gridBPPCurrent">
                            <Styles>
                                <Title Paddings-PaddingBottom="10px" Paddings-PaddingTop="10px"></Title>
                                <Header Wrap="True" Paddings-PaddingBottom="10px" Paddings-PaddingTop="10px" HorizontalAlign="Center">
                                </Header>
                                <Cell Paddings-Padding="10px" Wrap="True">
                                </Cell>
                                <Footer Paddings-Padding="10px" Wrap="True">
                                </Footer>
                            </Styles>
                            
                        </dx:ASPxGridViewExporter>   
                              <dx:ASPxGridViewExporter ID="gridBPPCurrentexcel" GridViewID="gridBPPCurrent" 

    PaperKind="A4" Landscape="True"  runat="server" >
</dx:ASPxGridViewExporter>

                        <%--</div> --%>                   
                       </div>
                      </div>
              

                    </div>



                     <div class="row">
                        <dx:ASPxGridView AutoGenerateColumns="false" Style="margin: 0 auto; width: 100%;"
                            ID="gridPerformancepay" ClientInstanceName="gridPerformancepay" runat="server">
                            <Settings ShowTitlePanel="true" ShowFooter="true" ShowFilterBar="Hidden" ShowFilterRow="false" />
                            <SettingsPager Mode="ShowAllRecords">
                            </SettingsPager>
                            <Styles Footer-HorizontalAlign="Right" Cell-HorizontalAlign="Right" Header-VerticalAlign="Middle"
                                Header-HorizontalAlign="Center" Header-Wrap="True">
                            </Styles>
                          
                            <Columns>
                                    <dx:GridViewDataColumn FieldName="S.NO" Width="2%" Caption="S.NO">
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn FieldName="Service_no" Width="28%" Caption="Service Roll No">
                                        
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn FieldName="Name" Caption="Name" Width="15%"  FooterCellStyle-HorizontalAlign="Right"
                                        CellStyle-HorizontalAlign="Left">
                                      
                                    </dx:GridViewDataColumn>
                                 
                                   <%-- <dx:GridViewBandColumn Caption="Emoluments Paid">
                                    <Columns>--%>
                                        <dx:GridViewDataColumn FieldName="Salary" ExportWidth="70" Caption="Salary" CellStyle-HorizontalAlign="Left">
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn FieldName="DA" ExportWidth="70" Caption="D A" CellStyle-HorizontalAlign="Left">
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn FieldName="hra" Caption="  HRA" ExportWidth="70" CellStyle-HorizontalAlign="Left">
                                        </dx:GridViewDataColumn>
                                       <%-- <dx:GridViewDataColumn FieldName="ma" Caption=" M A" ExportWidth="90" CellStyle-HorizontalAlign="Left">
                                        </dx:GridViewDataColumn>--%>
                                        <dx:GridViewDataColumn FieldName="Bonus" Caption="Bonus" ExportWidth="90" CellStyle-HorizontalAlign="Left">
                                        </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="Bpp" Caption="Branch BusinessPerformance" ExportWidth="90" CellStyle-HorizontalAlign="Left">
                                        </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="Mbbp" Caption="M Chit BusinessPerformance" ExportWidth="90" CellStyle-HorizontalAlign="Left">
                                        </dx:GridViewDataColumn>

                                 <dx:GridViewDataColumn FieldName="BusIns" Caption="Business Incestive" ExportWidth="90" CellStyle-HorizontalAlign="Left">
                                        </dx:GridViewDataColumn>
                                    <%--</Columns>
                                </dx:GridViewBandColumn>--%>
                                  <%--   <dx:GridViewBandColumn Caption="Other">
                                    <Columns>
                                        <dx:GridViewDataColumn FieldName="OtherAmount" ExportWidth="70" Caption="Amount" CellStyle-HorizontalAlign="Left">
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn FieldName="OtherDetails" ExportWidth="70" Caption="Details" CellStyle-HorizontalAlign="Center">
                                        </dx:GridViewDataColumn>
                                      
                                    </Columns>
                                </dx:GridViewBandColumn>--%>
                                    <dx:GridViewDataColumn FieldName="Totalamountpaid" Width="25%"  Caption="Total Amount Paid">
                                    </dx:GridViewDataColumn>
                                 <dx:GridViewDataColumn FieldName="Epf" ExportWidth="70" Caption="E.P.F" CellStyle-HorizontalAlign="Left">
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn FieldName="Ptax" ExportWidth="70" Caption="PTax" CellStyle-HorizontalAlign="Center">
                                        </dx:GridViewDataColumn>
                                          <dx:GridViewDataColumn FieldName="lic" ExportWidth="70" Caption="L.I.C" CellStyle-HorizontalAlign="Center">
                                        </dx:GridViewDataColumn>
                                 <%-- <dx:GridViewBandColumn Caption="Contribution To">
                                    <Columns>
                                       
                                       
                                    </Columns>
                                </dx:GridViewBandColumn>--%>
                                
                                  <dx:GridViewDataColumn FieldName="Totalded" ExportWidth="70" Caption="Total Deduction" CellStyle-HorizontalAlign="Left">
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn FieldName="Totalafdedu" ExportWidth="100" Caption="Total After Deduction" CellStyle-HorizontalAlign="Center">
                                        </dx:GridViewDataColumn>
                                
                                <%-- <dx:GridViewBandColumn Caption="For Admin Office Use">
                                    <Columns>
                                        <dx:GridViewDataColumn FieldName="adm_Salary" ExportWidth="70" Caption="Salary" CellStyle-HorizontalAlign="Left">
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn FieldName="adm_DA" ExportWidth="70" Caption="D A" CellStyle-HorizontalAlign="Center">
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn FieldName="adm_hra" Caption="  HRA" ExportWidth="70" CellStyle-HorizontalAlign="Left">
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn FieldName="adm_ma" Caption="M A" ExportWidth="90" CellStyle-HorizontalAlign="Left">
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn FieldName="adm_Bonus" Caption="Bonus" ExportWidth="90" CellStyle-HorizontalAlign="Left">
                                        </dx:GridViewDataColumn>
                                    </Columns>
                                </dx:GridViewBandColumn>--%>
                           <%--   <dx:GridViewDataColumn FieldName="NetTotal" Width="100%"  Caption="Total">
                                    </dx:GridViewDataColumn>--%>
                                  
                                      </Columns>
                        </dx:ASPxGridView>
                        <dx:ASPxGridViewExporter ID="ExportgridPerformancepay" runat="server" GridViewID="gridPerformancepay">
                            <Styles>
                                <Title Paddings-PaddingBottom="5px" Paddings-PaddingTop="5px"></Title>
                                <Header Wrap="True" Paddings-PaddingBottom="6px" Paddings-PaddingTop="6px" HorizontalAlign="Center">
                                </Header>
                                <Cell Paddings-Padding="6px" Wrap="True">
                                </Cell>
                                <Footer Paddings-Padding="6px" Wrap="True">
                                </Footer>
                            </Styles>
                        </dx:ASPxGridViewExporter>
                           <dx:ASPxGridViewExporter ID="gridPerformancepayexcel" GridViewID="gridPerformancepay" 

    PaperKind="A4" Landscape="True"  runat="server" >
</dx:ASPxGridViewExporter>

                    </div>

                      <div class="row">
                        <dx:ASPxGridView AutoGenerateColumns="false" Style="margin: 0 auto; width: 100%;"
                            ID="gridsalaryAD" ClientInstanceName="gridsalaryAD" runat="server">
                            <Settings ShowTitlePanel="true" ShowFooter="true" ShowFilterBar="Hidden" ShowFilterRow="false" />
                            <SettingsPager Mode="ShowAllRecords">
                            </SettingsPager>
                            <Styles Footer-HorizontalAlign="Right" Cell-HorizontalAlign="Right" Header-VerticalAlign="Middle"
                                Header-HorizontalAlign="Center" Header-Wrap="True">
                            </Styles>
                          
                            <Columns>
                                    <dx:GridViewDataColumn FieldName="S.NO" Width="2%" Caption="S.NO" FooterCellStyle-HorizontalAlign="left">
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn FieldName="Service_no" Width="28%" Caption="Service Roll No">
                                        
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn FieldName="Name" Caption="Name" Width="15%"  FooterCellStyle-HorizontalAlign="left"
                                        CellStyle-HorizontalAlign="left">
                                      
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn FieldName="Designation" Caption="Designation" Width="15%" FooterCellStyle-HorizontalAlign="Right"
                                        CellStyle-HorizontalAlign="Right">

                                    </dx:GridViewDataColumn>
                                    <dx:GridViewBandColumn Caption="Rate of emoluments">
                                    <Columns>
                                        <dx:GridViewDataColumn FieldName="sta_Salary" ExportWidth="70" Caption="Salary" CellStyle-HorizontalAlign="Left">
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn FieldName="sta_DA" ExportWidth="70" Caption="D A" CellStyle-HorizontalAlign="Center">
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn FieldName="sta_hra" Caption="  HRA" ExportWidth="70" CellStyle-HorizontalAlign="Left">
                                        </dx:GridViewDataColumn>
                                    
                                    </Columns>
                                </dx:GridViewBandColumn>
                                    <dx:GridViewBandColumn Caption="Emoluments Paid">
                                    <Columns>
                                        <dx:GridViewDataColumn FieldName="Salary" ExportWidth="70" Caption="Salary" CellStyle-HorizontalAlign="Left">
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn FieldName="DA" ExportWidth="70" Caption="D A" CellStyle-HorizontalAlign="Center">
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn FieldName="hra" Caption="  HRA" ExportWidth="70" CellStyle-HorizontalAlign="Left">
                                        </dx:GridViewDataColumn>
                                           <dx:GridViewDataColumn FieldName="Total_est" Caption="Total" ExportWidth="90" CellStyle-HorizontalAlign="Left">
                                        </dx:GridViewDataColumn>
                                    </Columns>
                                </dx:GridViewBandColumn>
                                     <dx:GridViewBandColumn Caption="Deductions">
                                    <Columns>
                                    <%--  <dx:GridViewDataColumn FieldName="ESI" ExportWidth="70" Caption="  P.F." CellStyle-HorizontalAlign="Left">
                                        </dx:GridViewDataColumn>--%>
                                      <%--   <dx:GridViewDataColumn FieldName="EPF" ExportWidth="70" Caption="E.P.F." CellStyle-HorizontalAlign="Center">
                                        </dx:GridViewDataColumn>  --%>
                                       <dx:GridViewDataColumn FieldName="EPF" ExportWidth="70" Caption="E.P.F" CellStyle-HorizontalAlign="Center">
                                        </dx:GridViewDataColumn>
                                       
                                        <dx:GridViewDataColumn FieldName="PFLoan" Caption=" PFLOAN" ExportWidth="100" CellStyle-HorizontalAlign="Left">
                                        </dx:GridViewDataColumn>
                                          <dx:GridViewDataColumn FieldName="ESI" ExportWidth="70" Caption="  ESI" CellStyle-HorizontalAlign="Left">

                                        </dx:GridViewDataColumn>
                                       
                                     <%--   <dx:GridViewDataColumn FieldName="PFLoan" Caption=" E.S.I" ExportWidth="70" CellStyle-HorizontalAlign="Left">
                                        </dx:GridViewDataColumn>--%>

                                        <dx:GridViewDataColumn FieldName="other" Caption=" Other" ExportWidth="90" CellStyle-HorizontalAlign="Left">
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn FieldName="Total_det" Caption=" Total" ExportWidth="90" CellStyle-HorizontalAlign="Left">
                                        </dx:GridViewDataColumn>
                                    </Columns>
                                </dx:GridViewBandColumn>
                                    <dx:GridViewDataColumn FieldName="NetTotal" Width="25%"  Caption="Net.Amnt Paid">
                                    </dx:GridViewDataColumn>
                              
                                    
                                      </Columns>
                        </dx:ASPxGridView>
                        <dx:ASPxGridViewExporter ID="ExportgridsalaryAD" runat="server" GridViewID="gridsalaryAD">
                            <Styles>
                                <Title Paddings-PaddingBottom="5px" Paddings-PaddingTop="5px"></Title>
                                <Header Wrap="True" Paddings-PaddingBottom="5px" Paddings-PaddingTop="5px" HorizontalAlign="Center">
                                </Header>
                                <Cell Paddings-Padding="5px" Wrap="True">
                                </Cell>
                                <Footer Paddings-Padding="5px" Wrap="True">
                                </Footer>
                            </Styles>
                        </dx:ASPxGridViewExporter>
                            <dx:ASPxGridViewExporter ID="gridsalaryADexcel" GridViewID="gridsalaryAD" 

    PaperKind="A4" Landscape="True"  runat="server" >
</dx:ASPxGridViewExporter>

                    </div>





                     <div class="row">
                         <dx:ASPxGridView AutoGenerateColumns="false" Style="margin: 0 auto; width: 100%;"
                            ID="gridBPPChits" ClientInstanceName="gridBPPChits" runat="server">
                              <Settings ShowTitlePanel="true" ShowFooter="true" ShowFilterBar="Hidden" ShowFilterRow="false" />
                             <SettingsText Title="BPPChits" />
                             <SettingsPager Mode="ShowAllRecords">
                            </SettingsPager>
                              <Styles Footer-HorizontalAlign="Right" Cell-HorizontalAlign="Right" Header-VerticalAlign="Middle"
                                Header-HorizontalAlign="Center" Header-Wrap="True">
                            </Styles>
                             <Columns>
                                 <dx:GridViewDataColumn Width="10%"  ExportWidth="50" Caption="S.No."
                                    CellStyle-HorizontalAlign="Right">
                                </dx:GridViewDataColumn>
                                  <dx:GridViewDataColumn Width="10%" ExportWidth="50" Caption="Months"
                                    CellStyle-HorizontalAlign="Right">
                                </dx:GridViewDataColumn>
                                  <dx:GridViewBandColumn Caption="DETAILS OF CHITS ENLISTED">
                                    <Columns>
                                        <dx:GridViewDataColumn Width="30%"  ExportWidth="100" FooterCellStyle-HorizontalAlign="Right" Caption="Chit Numbers and Name"
                                             CellStyle-HorizontalAlign="Right">
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn Width="30%"   ExportWidth="100" FooterCellStyle-HorizontalAlign="Right" Caption="Total Tickets"
                                             CellStyle-HorizontalAlign="Right">
                                        </dx:GridViewDataColumn>
                                          <dx:GridViewDataColumn Width="30%"  ExportWidth="100" FooterCellStyle-HorizontalAlign="Right" Caption="Total Chit Value"
                                             CellStyle-HorizontalAlign="Right">
                                        </dx:GridViewDataColumn>
                                         <dx:GridViewDataColumn Width="30%"   ExportWidth="100" FooterCellStyle-HorizontalAlign="Right" Caption="Payable"
                                             CellStyle-HorizontalAlign="Right">
                                        </dx:GridViewDataColumn>
                               
                                    </Columns>
                                </dx:GridViewBandColumn>
                              <dx:GridViewDataColumn Width="30%"   ExportWidth="100" FooterCellStyle-HorizontalAlign="Right" Caption="Paid"
                                             CellStyle-HorizontalAlign="Right">
                                        </dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn Width="30%"  ExportWidth="100" FooterCellStyle-HorizontalAlign="Right" Caption="Balance Payable"
                                             CellStyle-HorizontalAlign="Right">
                                        </dx:GridViewDataColumn>
                                 <dx:GridViewDataColumn Width="30%"  FieldName="SlNo"  ExportWidth="50" Caption="S.No."
                                    CellStyle-HorizontalAlign="Right">
                                </dx:GridViewDataColumn>
                                 <dx:GridViewDataColumn Width="40%"  FieldName="Name"  ExportWidth="200" Caption="Name of the staff"
                                    CellStyle-HorizontalAlign="Right">
                                </dx:GridViewDataColumn>
                                
                                  <dx:GridViewDataColumn Width="30%"  FieldName="Totalamtpaid" ExportWidth="100" FooterCellStyle-HorizontalAlign="Right" Caption="Total Amount Paid"
                                             CellStyle-HorizontalAlign="Right">
                                        </dx:GridViewDataColumn>
                                <%-- <dx:GridViewDataColumn Width="30%"  FieldName="Chitnumber" ExportWidth="100" FooterCellStyle-HorizontalAlign="Right" Caption="Chit Number"
                                             CellStyle-HorizontalAlign="Right">
                                        </dx:GridViewDataColumn>--%>
                                  <dx:GridViewDataColumn Width="10%" FieldName="Adminoffice"  ExportWidth="300" Caption="Admin Office Sanction No"
                                    CellStyle-HorizontalAlign="Right">
                                </dx:GridViewDataColumn>
                             </Columns>
                             </dx:ASPxGridView>
                         <dx:ASPxGridViewExporter ID="ExportgridBPPChits" runat="server" GridViewID="gridBPPChits">
                            <Styles>
                                <Title Paddings-PaddingBottom="6px" Paddings-PaddingTop="6px"></Title>
                                <Header Wrap="True" Paddings-PaddingBottom="6px" Paddings-PaddingTop="6px" HorizontalAlign="Center">
                                </Header>
                                <Cell Paddings-Padding="6px" Wrap="True">
                                </Cell>
                                <Footer Paddings-Padding="6px" Wrap="True">
                                </Footer>
                            </Styles>
                        </dx:ASPxGridViewExporter>
                          <dx:ASPxGridViewExporter ID="gridBPPChitsexcel" GridViewID="gridBPPChits" 

    PaperKind="A4" Landscape="True"  runat="server" >
</dx:ASPxGridViewExporter>
                    </div>

                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        $(document).ready(function () {
            prth_mask_input.init();
        });
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            prth_mask_input.init();
        });
    </script>
    <style type="text/css">
        table.tableBorder tr td
        {
            border: 1px solid black;
            padding: 5px;
        }
        table.tableBorder tr th
        {
            border: 1px solid black;
            padding: 5px;
            font-weight: bold;
        }
        table.tableBorder
        {
            margin-bottom: 30px !important;
        }
    </style>
</asp:Content>
