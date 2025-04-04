<%@ Page Title="SVCF Admin Panel" Culture="en-GB" Language="C#" MasterPageFile="~/Branch.Master"
    AutoEventWireup="true" CodeBehind="Advances.aspx.cs" Inherits="SreeVisalamChitFundLtd_phase1.Advances" %>

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
                        Advances</p>
                </div>
                <div class="box_c_content">
                    <div class="row">
                        <br />
                        <div style="width: 100%;">
                            <asp:Panel ID="Panel1" runat="server" DefaultButton="btnPayLoan">
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <table style="margin: 0 auto;">
                                            <tr>
                                                <td style="vertical-align: top; padding-top: 6px; padding-right: 5px;">
                                                    <asp:Label ID="labelSeries" runat="server" Text="Head"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList TabIndex="1" ID="ddlHeadAdvances" Width="240px" runat="server"
                                                        CssClass="chzn-select" AutoPostBack="true" OnSelectedIndexChanged="ddlHeadAdvances_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                    <asp:CompareValidator EnableClientScript="false" ID="CVddlGroupNo" runat="server"
                                                        ForeColor="Red" ValidationGroup="chit" ControlToValidate="ddlHeadAdvances" Display="Dynamic"
                                                        SetFocusOnError="true" ErrorMessage="Select Head Group" Operator="NotEqual" ValueToCompare="0"> </asp:CompareValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="vertical-align: top; padding-top: 6px; padding-right: 5px;">
                                                    <asp:Label ID="label1" runat="server" Text="Child"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList TabIndex="2" ID="ddlChildHead" Width="240px" runat="server" CssClass="chzn-select"
                                                        AutoPostBack="true">
                                                    </asp:DropDownList>
                                                    <asp:CompareValidator EnableClientScript="false" ID="CompareValidator1" runat="server"
                                                        ForeColor="Red" ValidationGroup="chit" ControlToValidate="ddlChildHead" Display="Dynamic"
                                                        SetFocusOnError="true" ErrorMessage="Select Child Head" Operator="NotEqual" ValueToCompare="--Select--"> </asp:CompareValidator>
                                                    <asp:TextBox runat="server" TabIndex="2" ID="txtChild" CssClass="input-text" placeholder="Child"></asp:TextBox>
                                                    <asp:RequiredFieldValidator EnableClientScript="false" ID="RequiredFieldValidator3"
                                                        ControlToValidate="txtChild" ValidationGroup="chit" Display="Dynamic" SetFocusOnError="true"
                                                        runat="server" ForeColor="Red" ErrorMessage="Enter Child Head"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="vertical-align: top; padding-top: 6px; padding-right: 5px;">
                                                    <asp:Label ID="label2" runat="server" Text="Advance"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox TabIndex="3" runat="server" CssClass="input-text sp_float" ToolTip="Ex. 1000.00"
                                                        placeholder="Advance" ID="txtAmount"></asp:TextBox>
                                                    <asp:RequiredFieldValidator EnableClientScript="false" ID="RFVtxtAmount" ControlToValidate="txtAmount"
                                                        ValidationGroup="chit" Display="Dynamic" SetFocusOnError="true" runat="server"
                                                        ForeColor="Red" ErrorMessage="Enter Advance"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="vertical-align: top; padding-top: 6px; padding-right: 5px;">
                                                    <asp:Label ID="label3" runat="server" Text="Applied On"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox TabIndex="4" runat="server" ID="dxAppliedDate" CssClass="input-text maskdate"
                                                        placeholder="Applied Date">
                                                    </asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ErrorMessage="Enter Applied Date"
                                                        ForeColor="Red" ControlToValidate="dxAppliedDate" ValidationGroup="chit" Display="Dynamic"
                                                        runat="server"></asp:RequiredFieldValidator>
                                                    <asp:CompareValidator ID="CompareValidator2" runat="server" ValidationGroup="chit"
                                                        ForeColor="Red" ControlToValidate="dxAppliedDate" Display="Dynamic" ErrorMessage="Enter Valid Date"
                                                        Operator="DataTypeCheck" Type="Date"> </asp:CompareValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="vertical-align: top; padding-top: 6px; padding-right: 5px;">
                                                    <asp:Label ID="label5" runat="server" Text="A.O. Sanction Number"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox TabIndex="6" MaxLength="4" placeholder="A.O. Sanction Number" ToolTip="Ex. 1234"
                                                        runat="server" CssClass="input-text sp_number" ID="txtAOSanctionNumber">
                                                    </asp:TextBox>
                                                    <asp:RequiredFieldValidator EnableClientScript="false" ID="RequiredFieldValidator1"
                                                        ForeColor="Red" ControlToValidate="txtAOSanctionNumber" Display="Dynamic" SetFocusOnError="true"
                                                        ValidationGroup="chit" runat="server" ErrorMessage="Enter A.O. Sanction Number"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="vertical-align: top; padding-top: 46px; padding-right: 5px;">
                                                    <asp:Label ID="label6" runat="server" Text="Narration"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox TabIndex="7" runat="server" ToolTip="-" placeholder="Narration" CssClass="input-text"
                                                        ID="txtNarration" TextMode="MultiLine">
                                                    </asp:TextBox>
                                                    <asp:RequiredFieldValidator EnableClientScript="false" ID="RequiredFieldValidator2"
                                                        ForeColor="Red" ValidationGroup="chit" ControlToValidate="txtNarration" Display="Dynamic"
                                                        SetFocusOnError="true" runat="server" ErrorMessage="Enter Narration"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                        </table>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                <div style="margin: 0px auto; text-align: center;">
                                    <asp:Button TabIndex="11" CausesValidation="true" ValidationGroup="chit" ID="btnPayLoan"
                                        Text="Add" OnClick="btnPayLoan_Click" CssClass="GreenyPushButton" runat="server" />
                                    <asp:Button TabIndex="12" CausesValidation="false" OnClientClick="clearValidationErrors();"
                                        ID="btnCancel" Text="Cancel" OnClick="btncancel1_Click" CssClass="GreenyPushButton"
                                        runat="server" />
                                </div>
                            </asp:Panel>
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
                            <asp:Label runat="server" ID="lblh" Style="text-align: center;" Text=""> </asp:Label>
                        </div>
                        <div style="min-height: 100px; text-align: center;">
                            <br />
                            <br />
                            <asp:Label runat="server" ID="lblcon" Text=""> </asp:Label>
                            <br />
                            <br />
                        </div>
                        <div class="boxheader">
                            <asp:Button TabIndex="21" Style="margin: 0 auto" CssClass="GreenyPushButton" OnClick="btn_ok"
                                ID="BtnOK" CausesValidation="false" runat="server" Text="Confirm" />
                            <asp:Button TabIndex="21" Style="margin: 0 auto" CssClass="GreenyPushButton" OnClick="btncancel_Click"
                                ID="Button1" CausesValidation="false" OnClientClick="clearValidationErrors();"
                                runat="server" Text="Cancel" />
                        </div>
                    </asp:Panel>
                    <asp:Panel CssClass="raised" ID="Panel2" runat="server" Visible="true" Width="400px"
                        Style="min-height: 150px">
                        <div style="top: 0px; height: 50px; text-align: center; padding: auto 0; width: 100%"
                            class="boxheader">
                            <asp:Label runat="server" ID="Label8" Text=""> </asp:Label>
                        </div>
                        <div style="min-height: 100px; text-align: center;">
                            <br />
                            <br />
                            <asp:Label runat="server" ID="Label9" Text=""> </asp:Label>
                            <br />
                            <br />
                        </div>
                        <div class="boxheader">
                            <asp:Button TabIndex="21" Style="margin: 0 auto" CssClass="GreenyPushButton" OnClick="btncancel_Click"
                                ID="Button3" CausesValidation="false" OnClientClick="clearValidationErrors();"
                                runat="server" Text="Ok" />
                        </div>
                    </asp:Panel>
                </div>
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
