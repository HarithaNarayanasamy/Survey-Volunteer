<%@ Page Title="SVCF Admin Panel" Language="C#" MasterPageFile="~/Branch.Master"
    AutoEventWireup="true" CodeBehind="profitandloss.aspx.cs" Inherits="SreeVisalamChitFundLtd_phase1.profitandloss" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <style type="text/css">
        .chzn-drop
        {
            border: 1px solid #aaa !important;
            padding-top: 5px !important;
            width: 240px !important;
        }
        textarea
        {
            resize: none;
        }
        .chzn-results
        {
            text-align: center;
        }
        #ctl00_cphMainContent_ddlChitGroupNo_chzn .chzn-drop .chzn-search input[type="text"]
        {
            height: 15px;
        }
        #ctl00_cphMainContent_ddlTokenNo_chzn .chzn-drop .chzn-search input[type="text"]
        {
            height: 15px;
        }
        #ctl00_cphMainContent_ddlMedium_chzn .chzn-drop .chzn-search input[type="text"]
        {
            height: 15px;
        }
        #ctl00_cphMainContent_ddlBankHead_chzn .chzn-drop .chzn-search input[type="text"]
        {
            height: 15px;
        }
        #ctl00_cphMainContent_ddlLoan_chzn .chzn-drop .chzn-search input[type="text"]
        {
            height: 15px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMainContent" runat="server">
    <script type="text/javascript">

        function clearValidationErrors() {
            //Hide all validation errors
            if (window.Page_Validators)
                for (var vI = 0; vI < Page_Validators.length; vI++) {
                    var vValidator = Page_Validators[vI];
                    vValidator.isvalid = true;
                    ValidatorUpdateDisplay(vValidator);
                }
            //Hide all validaiton summaries
            if (typeof (Page_ValidationSummaries) != "undefined") { //hide the validation summaries
                for (sums = 0; sums < Page_ValidationSummaries.length; sums++) {
                    summary = Page_ValidationSummaries[sums];
                    summary.style.display = "none";
                }
            }
        }
    </script>
    <ajax:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </ajax:ToolkitScriptManager>
    <div class="row">
        <div class="twelve columns">
            <div class="box_c">
                <div class="box_c_heading  box_actions">
                    <p>
                        Profit and Loss</p>
                </div>
                <div class="box_c_content">
                    <div class="row">
                        <br />
                        <div style="width: 100%;">
                            <asp:Panel ID="Panel1" runat="server">
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <table style="margin: 0 auto;">
                                            <tr>
                                                <td style="vertical-align: top; padding-top: 6px; padding-right: 5px;">
                                                    <asp:Label ID="label10" runat="server" Text="Head"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList onchange="clearValidationErrors();" ID="ddlLossandprofit" Width="240px"
                                                        runat="server" CssClass="chzn-select" AutoPostBack="true">
                                                    </asp:DropDownList>
                                                    <asp:CompareValidator EnableClientScript="false" ID="CompareValidator1" ValidationGroup="chit"
                                                        ValueToCompare="0" ControlToValidate="ddlLossandprofit" ForeColor="Red" Display="Dynamic"
                                                        Operator="NotEqual" SetFocusOnError="true" runat="server" ErrorMessage="Select"></asp:CompareValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="vertical-align: top; padding-top: 6px; padding-right: 5px;">
                                                    <asp:Label ID="label1" runat="server" Text="A.O Sanction"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox TabIndex="7" runat="server" ToolTip="-" placeholder="A.O Sanction" CssClass="input-text"
                                                        ID="txtAOSanction">
                                                    </asp:TextBox>
                                                    <asp:RequiredFieldValidator EnableClientScript="false" ID="RequiredFieldValidator3"
                                                        ForeColor="Red" ValidationGroup="chit" ControlToValidate="txtAOSanction" Display="Dynamic"
                                                        SetFocusOnError="true" runat="server" ErrorMessage="Enter A.O Sanction"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="vertical-align: top; padding-top: 6px; padding-right: 5px;">
                                                    <asp:Label ID="label4" runat="server" Text="Amount"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox TabIndex="7" runat="server" ToolTip="-" placeholder="Amount" CssClass="input-text"
                                                        ID="txtamount">
                                                    </asp:TextBox>
                                                    <asp:RequiredFieldValidator EnableClientScript="false" ID="RequiredFieldValidator4"
                                                        ForeColor="Red" ValidationGroup="chit" ControlToValidate="txtamount" Display="Dynamic"
                                                        SetFocusOnError="true" runat="server" ErrorMessage="Enter Amount"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="vertical-align: top; padding-top: 46px; padding-right: 5px;">
                                                    <asp:Label ID="label11" runat="server" Text="Narration"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox TabIndex="7" runat="server" ToolTip="-" placeholder="Narration" CssClass="input-text"
                                                        ID="txtNarration" TextMode="MultiLine">
                                                    </asp:TextBox>
                                                    <asp:RequiredFieldValidator EnableClientScript="false" ID="RequiredFieldValidator6"
                                                        ForeColor="Red" ValidationGroup="chit" ControlToValidate="txtNarration" Display="Dynamic"
                                                        SetFocusOnError="true" runat="server" ErrorMessage="Enter Narration"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="vertical-align: top; padding-top: 6px; padding-right: 5px;">
                                                    <asp:Label ID="label12" runat="server" Text="Date"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtDate" runat="server" CssClass="input-text maskdate" placeholder="Date"
                                                        TabIndex="7" ToolTip="-">
                                                    </asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtDate"
                                                        Display="Dynamic" EnableClientScript="false" ErrorMessage="Enter Date" ForeColor="Red"
                                                        SetFocusOnError="true" ValidationGroup="chit"></asp:RequiredFieldValidator>
                                                    <asp:CompareValidator ID="CompareValidator2" runat="server" EnableClientScript="false" ValidationGroup="chit"
                                                        ForeColor="Red" ControlToValidate="txtDate" Display="Dynamic" ErrorMessage="Enter Valid Date"
                                                        Operator="DataTypeCheck" Type="Date"> </asp:CompareValidator>
                                                </td>
                                                <tr>
                                                    <td style="vertical-align: top; padding-top: 6px; padding-right: 5px;">
                                                        <asp:Label ID="label13" runat="server" Text="Transaction Medium"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList onchange="clearValidationErrors();" OnSelectedIndexChanged="ddlTransaction_SelectedIndexChanged"
                                                            TabIndex="8" Width="240px" ID="ddlTransaction" CssClass="chzn-select" runat="server"
                                                            AutoPostBack="true">
                                                            <asp:ListItem Selected="True" Text="Cash" Value="Cash"></asp:ListItem>
                                                            <asp:ListItem Text="Bank" Value="Bank"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                        </table>
                                        <table style="margin: 0 auto;">
                                            <tr>
                                                <td style="vertical-align: top; padding-right: 5px; padding-top: 6px;">
                                                    <asp:Label ID="lblChequeNo" runat="server" Visible="false" Text="Cheque Number"></asp:Label>
                                                </td>
                                                <td style="padding-right: 5px;">
                                                    <asp:TextBox TabIndex="9" MaxLength="7" placeholder="Cheque Number" ToolTip="Ex. 654321"
                                                        CausesValidation="false" ID="txtChequeNo" CssClass="input-text sp_number" Visible="false"
                                                        runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ValidationGroup="chit" EnableClientScript="false" Visible="false"
                                                        ForeColor="Red" ID="RFVtxtIfsc" ControlToValidate="txtChequeNo" Display="Dynamic"
                                                        SetFocusOnError="true" runat="server" ErrorMessage="Enter Cheque Number"></asp:RequiredFieldValidator>
                                                </td>
                                                <td style="vertical-align: top; padding-right: 5px; padding-top: 6px;">
                                                    <asp:Label ID="lblBankHead" Visible="false" runat="server" Text="Bank Name"></asp:Label>
                                                </td>
                                                <td style="vertical-align: top;">
                                                    <asp:DropDownList TabIndex="10" Width="240px" Visible="false" ID="ddlBankHead" CssClass="chzn-select"
                                                        runat="server" AutoPostBack="false" CausesValidation="false">
                                                    </asp:DropDownList>
                                                    <asp:CompareValidator Visible="false" EnableClientScript="false" ID="CVddlBankHead"
                                                        ValidationGroup="chit" ValueToCompare="0" ControlToValidate="ddlBankHead" ForeColor="Red"
                                                        Display="Dynamic" Operator="NotEqual" SetFocusOnError="true" runat="server" ErrorMessage="Select Bank Name"></asp:CompareValidator>
                                                </td>
                                            </tr>
                                        </table>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                <div style="margin: 0px auto; text-align: center;">
                                    <asp:Button TabIndex="11" CausesValidation="true" ValidationGroup="chit" ID="btnPayLoan"
                                        Text="Ok" OnClick="btnPayLoan_Click" CssClass="GreenyPushButton" runat="server" />
                                    <asp:Button TabIndex="12" CausesValidation="false" OnClientClick="clearValidationErrors();"
                                        ID="btnCancel" Text="Cancel" OnClick="btncancel1_Click" CssClass="GreenyPushButton"
                                        runat="server" />
                                </div>
                            </asp:Panel>
                        </div>
                    </div>
                </div>
                <ajax:ModalPopupExtender ID="ModalPopupExtender1" TargetControlID="show" PopupControlID="pnlmsg"
                    BackgroundCssClass="modalBackground" runat="server">
                </ajax:ModalPopupExtender>
                <asp:LinkButton ID="show" runat="server"></asp:LinkButton>
                <asp:Panel CssClass="raised" ID="pnlmsg" runat="server" Visible="false" Width="400px"
                    Style="min-height: 150px">
                    <div style="top: 0px; height: 50px; text-align: center; padding: auto 0; width: 100%"
                        class="boxheader">
                        Status
                    </div>
                    <div style="min-height: 100px; text-align: center;">
                        <br />
                        <br />
                        <asp:Label runat="server" ID="lblcon" Text=""> </asp:Label>
                        <br />
                        <br />
                    </div>
                    <div class="boxheader">
                        <asp:Button Style="margin: 0 auto" CssClass="GreenyPushButton" OnClick="btncancel1_Click"
                            ID="BtnOK" CausesValidation="false" runat="server" Text="Ok" />
                    </div>
                </asp:Panel>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        $(document).ready(function () {
            $(".chzn-select").chosen({ search_contains: true });
            $(".sp_number").numeric({
                decimal: false,
                negative: false
            },
                function () {
                    alert("Positive integers only");
                    this.value = "";
                    this.focus();
                });
            $(".sp_float").numeric({
                negative: false
            },
                function () {
                    alert("Positive only");
                    this.value = "";
                    this.focus();
                });
            prth_mask_input.init();
        });

        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $(".chzn-select").chosen({ search_contains: true });
            $(".sp_number").numeric({
                decimal: false,
                negative: false
            },
                function () {
                    alert("Positive integers only");
                    this.value = "";
                    this.focus();
                });
            $(".sp_float").numeric({
                negative: false
            },
                function () {
                    alert("Positive only");
                    this.value = "";
                    this.focus();
                });
            prth_mask_input.init();
            prth_tips.init();
        });
    </script>
</asp:Content>
