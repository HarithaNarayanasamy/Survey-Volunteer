<%@ Page Title="SVCF - Admin Page" Culture="en-GB" Language="C#" MasterPageFile="~/Branch.Master"
    AutoEventWireup="true" CodeBehind="advanceinsertion.aspx.cs" Inherits="SreeVisalamChitFundLtd_phase1.advanceinsertion" %>



<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
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
                        Advance</p>
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
                                                    <asp:Label ID="label10" runat="server" Text="Head"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList runat="server" CssClass="chzn-select" Width="240px" ID="ddlHead">
                                                    </asp:DropDownList>
                                                    <asp:CompareValidator EnableClientScript="false" ID="CVddlGroupNo" runat="server"
                                                        ForeColor="Red" ValidationGroup="chit" ControlToValidate="ddlHead" Display="Dynamic"
                                                        SetFocusOnError="true" ErrorMessage="Select Head" Operator="NotEqual" ValueToCompare="0"> </asp:CompareValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="vertical-align: top; padding-top: 6px; padding-right: 5px;">
                                                    <asp:Label ID="label1" runat="server" Text="Advance Number"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox runat="server" ID="txtAdvanceNumber" CssClass="input-text sp_number"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" EnableClientScript="false"
                                                        ControlToValidate="txtAdvanceNumber" ValidationGroup="chit" Display="Dynamic" SetFocusOnError="true"
                                                        runat="server" ForeColor="Red" ErrorMessage="Enter Advance Number"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="vertical-align: top; padding-top: 6px; padding-right: 5px;">
                                                    <asp:Label ID="label3" runat="server" Text="Date"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox TabIndex="4" runat="server" ID="txtDate" CssClass="input-text maskdate"
                                                        placeholder="Date">
                                                    </asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ErrorMessage="Enter Date"
                                                        ForeColor="Red" ControlToValidate="txtDate" ValidationGroup="chit" Display="Dynamic"
                                                        runat="server"></asp:RequiredFieldValidator>
                                                    <asp:CompareValidator ID="CompareValidator2" runat="server" ValidationGroup="chit"
                                                        ForeColor="Red" ControlToValidate="txtDate" Display="Dynamic" ErrorMessage="Enter Valid Date"
                                                        Operator="DataTypeCheck" Type="Date"> </asp:CompareValidator>
                                                    <asp:RangeValidator ID="rvDate" runat="server" ForeColor="Red" Type="Date" ControlToValidate="txtDate"
                                                        ValidationGroup="chit" Display="Dynamic" ErrorMessage="*"></asp:RangeValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="vertical-align: top; padding-top: 6px; padding-right: 5px;">
                                                    <asp:Label ID="label7" runat="server" Text="Transaction Medium"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList onchange="clearValidationErrors();" TabIndex="8" Width="240px"
                                                        ID="ddlMedium" OnSelectedIndexChanged="ddlMedium_SelectedIndexChanged" CssClass="chzn-select"
                                                        runat="server" AutoPostBack="true">
                                                        <asp:ListItem Selected="True" Text="Cash" Value="Cash"></asp:ListItem>
                                                        <asp:ListItem Text="Bank" Value="Bank"></asp:ListItem>
                                                    </asp:DropDownList>
                                                    <br />
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
                                        ID="btnCancel" Text="Cancel" OnClick="btncancel_Click" CssClass="GreenyPushButton"
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
                            <asp:Button TabIndex="21" Style="margin: 0 auto" CssClass="GreenyPushButton" OnClick="btncancel_Click"
                                ID="BtnOK" CausesValidation="false" runat="server" Text="Ok" />
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
