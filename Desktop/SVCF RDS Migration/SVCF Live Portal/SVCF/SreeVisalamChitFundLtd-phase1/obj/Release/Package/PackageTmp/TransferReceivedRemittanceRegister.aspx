<%@ Page Culture="en-GB" Language="C#" MasterPageFile="~/Branch.Master" AutoEventWireup="true"
    CodeBehind="TransferReceivedRemittanceRegister.aspx.cs" Inherits="SreeVisalamChitFundLtd_phase1.TransferReceivedRemittanceRegister"
    Title="SVCF Admin Panel" %>

<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxMenu" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1.Export, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView.Export" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<asp:Content ID="Content2" ContentPlaceHolderID="cphHead" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphMainContent" runat="server">
    <ajax:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </ajax:ToolkitScriptManager>
    <div class="row">
        <div class="twelve columns">
            <div class="box_c">
                <div class="box_c_heading cf">
                    <p class="sepV_a">
                        Cheque Received Register</p>
                </div>
                <div class="box_c_content">
                    <div class="row">
                        <div style="margin-top: -0.5em; margin-bottom: 0.6em;">
                            <asp:Panel runat="server" DefaultButton="BtnStatisticsGo">
                                <div style="display: table-cell; padding-right: 5px !important;">
                                    <asp:Label ID="Label1" runat="server" Text="From Date : "></asp:Label>
                                </div>
                                <div style="display: table-cell; padding-right: 5px;">
                                    <asp:TextBox TabIndex="1" Width="100px" class="input-text maskdate" ID="txtFromDate"
                                        runat="server" placeholder="From Date">
                                    </asp:TextBox>
                                    <asp:RequiredFieldValidator ValidationGroup="twelvehead" Display="Dynamic" ID="RequiredFieldValidator3"
                                        runat="server" ControlToValidate="txtFromDate" ErrorMessage=""></asp:RequiredFieldValidator>
                                    <asp:CompareValidator ID="CompareValidator10" ValidationGroup="twelvehead" runat="server" Display="Dynamic" ControlToValidate="txtFromDate" Operator="DataTypeCheck" Type="Date"></asp:CompareValidator>
                                </div>
                                <div style="display: table-cell; padding-right: 5px;">
                                    <asp:Label ID="Label2" runat="server" Text="To Date : "></asp:Label>
                                </div>
                                <div style="display: table-cell; padding-right: 5px;">
                                    <asp:TextBox TabIndex="2" Width="100px" class="input-text maskdate" ID="txtToDate"
                                        placeholder="To Date" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ValidationGroup="twelvehead" Display="Dynamic" ID="RequiredFieldValidator1"
                                        runat="server" ControlToValidate="txtToDate" ErrorMessage=""></asp:RequiredFieldValidator>
                                    <asp:CompareValidator ValidationGroup="twelvehead" ID="CompareValidator11" ControlToValidate="txtToDate" ControlToCompare="txtFromDate" Display="Dynamic" runat="server" Operator="GreaterThanEqual" Type="Date"></asp:CompareValidator>
                                </div>
                                <div style="display: table-cell; padding-right: 5px; vertical-align: top;">
                                    <asp:Button TabIndex="3" ID="BtnStatisticsGo" ValidationGroup="twelvehead" runat="server"
                                        class="GreenyPushButton" OnClick="BtnStatisticsGo_Click" Text="Go!"></asp:Button>
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
                                                    <dx:MenuItem Text="XLSX">
                                                    </dx:MenuItem>
                                                </Items>
                                            </dx:MenuItem>
                                        </Items>
                                    </dx:ASPxMenu>
                                </div>
                            </asp:Panel>
                        </div>
                        <dx:ASPxGridView Styles-AlternatingRow-BackColor="LightGray" Style="margin: 0 auto;
                            width: 100%;" OnSummaryDisplayText="ASPxGridView1_SummaryDisplayText" OnCustomSummaryCalculate="ASPxGridView1_OnCustomSummaryCalculate"
                            ID="grid" ClientInstanceName="grid" runat="server" DataSourceID="AccessDataSource1"
                            OnCustomCallback="grid_CustomCallback">
                            <Settings ShowTitlePanel="true" ShowHeaderFilterButton="true" ShowGroupFooter="Hidden"
                                ShowFooter="true" ShowGroupedColumns="true" ShowGroupPanel="false" ShowFilterBar="Visible"
                                ShowFilterRow="true" />
                            <Templates>
                                <GroupRowContent>
                                    <dx:ASPxLabel ID="GroupLabel" runat="server" Text='<%# GetLabelText(Container)%>'>
                                    </dx:ASPxLabel>
                                </GroupRowContent>
                            </Templates>
                            <SettingsText Title="Cheque Received Register" />
                            <GroupSummary>
                                <dx:ASPxSummaryItem FieldName="Amount" ShowInGroupFooterColumn="Amount" SummaryType="Sum" />
                                <dx:ASPxSummaryItem FieldName="PLAmount" ShowInGroupFooterColumn="PLAmount" SummaryType="Sum" />
                                <dx:ASPxSummaryItem FieldName="Narration" ShowInGroupFooterColumn="Narration" SummaryType="Sum" />
                            </GroupSummary>
                            <TotalSummary>
                                <dx:ASPxSummaryItem FieldName="Amount" SummaryType="Sum" />
                                <dx:ASPxSummaryItem FieldName="PLAmount" SummaryType="Sum" />
                                <dx:ASPxSummaryItem FieldName="Narration" SummaryType="Custom" />
                            </TotalSummary>
                            <SettingsPager Mode="ShowPager" Position="TopAndBottom">
                            </SettingsPager>
                            <Columns>
                                <dx:GridViewDataColumn FieldName="ChoosenDate" Caption="Date">
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="GROUPNO" Caption="Group Name">
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="MemberName" Caption="Member Name">
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="ChequeDDNO" Caption="Cheque Number">
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="BankName" Caption="Bank Name">
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="Voucher_No" Caption="Voucher Number">
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="Series" Caption="Series">
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="Narration" Caption="Narration">
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="PLAmount" Caption="PL Amount">
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="Amount" Caption="Amount">
                                </dx:GridViewDataColumn>
                            </Columns>
                            <Styles Footer-HorizontalAlign="Left" Cell-HorizontalAlign="Left" Header-VerticalAlign="Middle" Header-HorizontalAlign="Center"
                                Header-Wrap="True">
                                <GroupPanel Wrap="True" HorizontalAlign="Left">
                                </GroupPanel>
                                <GroupRow Wrap="True" HorizontalAlign="Left">
                                </GroupRow>
                            </Styles>
                        </dx:ASPxGridView>
                        <dx:ASPxGridViewExporter ID="gridExport" runat="server" GridViewID="grid">
                            <Styles>
                                <Header VerticalAlign="Middle" HorizontalAlign="Center" Wrap="True">
                                </Header>
                                <Cell HorizontalAlign="Left" Wrap="True">
                                </Cell>
                                <Footer HorizontalAlign="Left" Wrap="True"></Footer>
                            </Styles>
                        </dx:ASPxGridViewExporter>
                        <asp:SqlDataSource runat="server" ID="AccessDataSource1" 
                            ProviderName="MySql.Data.MySqlClient" />
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        $(document).ready(function () {
            prth_mask_input.init();
        });
    </script>
</asp:Content>
