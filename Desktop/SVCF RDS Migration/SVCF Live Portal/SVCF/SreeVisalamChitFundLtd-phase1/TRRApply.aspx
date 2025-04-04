<%@ Page Language="C#" Culture="en-GB" MasterPageFile="~/Branch.Master" AutoEventWireup="true"
    CodeBehind="TRRApply.aspx.cs" Inherits="SreeVisalamChitFundLtd_phase1.WebForm1" %>

<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxMenu" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1.Export, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView.Export" TagPrefix="dx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <style type="text/css">
        .aaaaaa
        {
            display: none;
        }
        #ctl00_cphMainContent_ASPxListBox1
        {
            display: none;
        }
    </style>
    <script type="text/javascript">
        function grid_SelectionChanged(s, e) {
            s.GetSelectedFieldValues("TransactionKey", GetSelectedFieldValuesCallback);
        }
        function GetSelectedFieldValuesCallback(values) {
            selList.BeginUpdate();
            try {
                debugger;
                if (values.length > 0) {
                    $(".btnvisible").show();
                }
                else {
                    $(".btnvisible").hide();
                }
                selList.ClearItems();
                for (var i = 0; i < values.length; i++) {
                    selList.AddItem(values[i]);
                }
            } finally {
                selList.EndUpdate();
            }

        }


        function Onclick(s, e) {
            if (grid.GetSelectedRowCount() == 0)
                e.processOnServer = false;
        }
        

      
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMainContent" runat="server">
    <ajax:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </ajax:ToolkitScriptManager>
    <div>
        <div>
            <div class="box_c">
                <div class="box_c_heading  box_actions noprint">
                    <p>
                        Transfer Remittance Received Register
                    </p>
                </div>
                <div class="box_c_content">
                    <div class="noprint" style="margin-top: -0.5em; margin-bottom: 0.6em;">
                        <asp:Panel ID="Panel1" runat="server" DefaultButton="BtnStatisticsGo">
                            <div style="display: table-cell; padding-right: 5px !important;">
                                <asp:Label ID="Label9" runat="server" Text="Date :"></asp:Label>
                            </div>
                            <div style="display: table-cell; padding-right: 5px;">
                                <asp:TextBox TabIndex="1" Width="100px" class="input-text maskdate" ID="txtFromDate"
                                    runat="server" placeholder="From Date"></asp:TextBox>
                                <asp:RequiredFieldValidator ValidationGroup="twelvehead" Display="Dynamic" ID="RequiredFieldValidator1"
                                    runat="server" ErrorMessage="" ControlToValidate="txtFromDate"></asp:RequiredFieldValidator>
                                <asp:CompareValidator ID="CompareValidator3" runat="server" ValidationGroup="twelvehead"
                                    Display="Dynamic" ControlToValidate="txtFromDate" Operator="DataTypeCheck" Type="Date"></asp:CompareValidator>
                            </div>
                            <div style="display: table-cell; padding-right: 5px; vertical-align: top;">
                                <asp:Button ValidationGroup="twelvehead" TabIndex="3" ID="BtnStatisticsGo" runat="server"
                                    Text="Go!" class="btn" CssClass="GreenyPushButton" OnClick="BtnStatisticsGo_Click" />
                            </div>
                            <div style="display: table-cell; padding-right: 5px; vertical-align: top;">
                                <asp:Button ID="btnAccept" runat="server" Text="Accept" Style="display: none;" CssClass="GreenyPushButton btn btnvisible"
                                    ValidationGroup="twelvehead" TabIndex="3" OnClick="btnAccept_Click" />
                                <dx:ASPxListBox ID="ASPxListBox1" runat="server" ClientInstanceName="selList" Width="150px"
                                    Style="display: none;" ForeColor="White">
                                </dx:ASPxListBox>
                            </div>
                        </asp:Panel>
                    </div>
                    <div id="printdiv" class="printable">
                        <dx:ASPxGridView Style="margin: 0 auto; width: 100%;" ID="grid" runat="server" ClientInstanceName="grid"
                            OnHtmlRowPrepared="grid_HtmlRowPrepared" KeyFieldName="TransactionKey">
                            <Settings ShowTitlePanel="true" ShowHeaderFilterButton="true" ShowFilterBar="Visible"
                                ShowFilterRow="true" ShowFilterRowMenu="true" />
                            <SettingsText Title="Cash Received Register" />
                            <SettingsPager Mode="ShowAllRecords">
                            </SettingsPager>
                            <Columns>
                                <dx:GridViewCommandColumn ShowSelectCheckbox="True" VisibleIndex="0">
                                    <HeaderTemplate>
                                        <dx:ASPxCheckBox ID="cbPage" runat="server" ClientInstanceName="cbPage" ToolTip="Select all rows within the page"
                                            OnInit="cbPage_Init" Visible="false">
                                            <ClientSideEvents CheckedChanged="OnPageCheckedChanged" />
                                        </dx:ASPxCheckBox>
                                    </HeaderTemplate>
                                </dx:GridViewCommandColumn>
                                <dx:GridViewDataColumn FieldName="Voucher_No" VisibleIndex="2" Width="7%" CellStyle-HorizontalAlign="Left"
                                    Caption="Receipt or Reference No.">
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="TransactionKey" Visible="false" VisibleIndex="2" Width="7%" Caption="TransactionKey">
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
                                <dx:GridViewDataColumn FieldName="Heads" VisibleIndex="9" Caption="Heads" Width="9%">
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn VisibleIndex="10" FieldName="GrandTotal" Width="10%" Caption="Grand Total">
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn VisibleIndex="11" FieldName="moneycollector" Width="8%" Caption="Collector Name">
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
                            <ClientSideEvents SelectionChanged="grid_SelectionChanged" />
                        </dx:ASPxGridView>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <asp:Label runat="server" ID="lblHide"></asp:Label>
    <ajax:ModalPopupExtender ID="ModalPopupExtender2" CancelControlID="btnCancelPopup"
        BackgroundCssClass="modalBackground" TargetControlID="ShowPopup1" PopupControlID="Pnlmsg"
        runat="server">
    </ajax:ModalPopupExtender>
    <asp:LinkButton ID="ShowPopup1" runat="server"></asp:LinkButton>
    <asp:Panel CssClass="raised" ID="Pnlmsg" runat="server" Visible="false" Width="550px"
        Style="min-height: 100px">
        <asp:Label runat="server" ID="lblHint" Text="" Visible="false"> </asp:Label>
        <div style="top: 0px; height: 50px; text-align: center; padding: auto 0; width: 100%"
            class="boxheader">
            <asp:Label runat="server" ID="lblT" Text=""> </asp:Label>
        </div>
        <div style="min-height: 100px; text-align: center;">
            <br />
            <br />
            <asp:Label ID="lblcont" runat="server" Text="Label" Font-Size="Large"></asp:Label><br />
            <asp:GridView ID="GridView1" runat="server" Visible="false" ClientInstanceName="selList">
            </asp:GridView>
            <asp:Label runat="server" ID="lblContent" Text=""> </asp:Label>
            <br />
            <br />
        </div>
        <div class="boxheader">
            <div style="margin: 0 auto;">
                <asp:Button Style="margin: 0 auto" CssClass="GreenyPushButton" ID="BtnOk" runat="server"
                    Text="OK" CausesValidation="false" OnClick="BtnOk_Click" />
                <asp:Button ID="btnCancelPopup" runat="server" Text="Cancel" Style="margin: 0 auto"
                    CssClass="GreenyPushButton" />
            </div>
        </div>
    </asp:Panel>
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
    </script>
</asp:Content>
