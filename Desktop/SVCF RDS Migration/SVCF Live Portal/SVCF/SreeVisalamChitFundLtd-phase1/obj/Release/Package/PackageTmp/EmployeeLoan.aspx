<%@ Page Language="C#" Culture="en-GB" MasterPageFile="~/Branch.Master" AutoEventWireup="true"
    CodeBehind="EmployeeLoan.aspx.cs" Inherits="SreeVisalamChitFundLtd_phase1.EmployeeLoan"
    Title="Employee Loan" %>

<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMainContent" runat="server">
    <style type="text/css">
        .chzn-drop
        {
            border: 1px solid #aaa !important;
            padding-top: 5px !important;
            width: 240px !important;
        }
        .chzn-results
        {
            text-align:center;
        }
        #ctl00_cphMainContent_ddlEmployeeNo_chzn .chzn-drop .chzn-search input[type="text"]
        {
            height:15px;
        }
        #ctl00_cphMainContent_ddlMedium_chzn .chzn-drop .chzn-search input[type="text"]
        {
            height:15px;
        }
        #ctl00_cphMainContent_ddlBankHead_chzn .chzn-drop .chzn-search input[type="text"]
        {
            height:15px;
        }
    </style>
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
                        Employee Loan</p>
                </div>
                <div class="box_c_content">
                    <div class="row">
                        <asp:UpdatePanel runat="server">
                            <ContentTemplate>
                                <div style="width: 100%;">
                                    <table style="margin: 0 auto;">
                                        <tr>
                                            <td>
                                                <asp:Label ID="label1" runat="server" Text="Employee Name"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList TabIndex="1" Width="240px" ID="ddlEmployeeNo" runat="server" CssClass="chzn-select">
                                                </asp:DropDownList>
                                                <asp:CompareValidator ValidationGroup="Employee" EnableClientScript="false" ID="CompareValidator1"
                                                    runat="server" ControlToValidate="ddlEmployeeNo" Display="Dynamic" SetFocusOnError="true"
                                                    ErrorMessage="Select Employee Name" Operator="NotEqual" ValueToCompare="--Select--"> </asp:CompareValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="label2" runat="server" Text="Loan Amount"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox TabIndex="2" runat="server" CssClass="input-text ttip_r sp_float" placeholder="Loan Amount"
                                                    ToolTip="Ex. 1000.00" ID="txtAmount"></asp:TextBox>
                                                <asp:RequiredFieldValidator ValidationGroup="Employee"  EnableClientScript="false" ID="RFVtxtAmount" ControlToValidate="txtAmount"
                                                    Display="Dynamic" SetFocusOnError="true" runat="server" ErrorMessage="Enter Loan Amount"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="label3" runat="server" Text="Applied On"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox TabIndex="3" runat="server" ID="dxAppliedDate" CssClass="input-text ttip_r maskdate"
                                                    placeholder="Applied Date">
                                                </asp:TextBox>
                                                <asp:RequiredFieldValidator ValidationGroup="Employee"  ID="RequiredFieldValidator5" ErrorMessage="Enter Applied Date"
                                                    ControlToValidate="dxAppliedDate" Display="Dynamic" runat="server"></asp:RequiredFieldValidator>
                                                <asp:CompareValidator ID="CompareValidator2" runat="server"
                                                        ValidationGroup="chit" ControlToValidate="dxAppliedDate" Display="Dynamic"
                                                        ErrorMessage="Enter Valid Date" Operator="DataTypeCheck" Type="Date" > </asp:CompareValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="label4" runat="server" Text="Approved On"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox TabIndex="4" runat="server" ID="dxApprovedDate" CssClass="input-text ttip_r maskdate"
                                                    placeholder="Approved Date">
                                                </asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ErrorMessage="Enter Approved Date"
                                                    ValidationGroup="Employee"  ControlToValidate="dxApprovedDate" Display="Dynamic" runat="server"></asp:RequiredFieldValidator>
                                                <asp:CompareValidator ID="CompareValidator3" runat="server"
                                                        ValidationGroup="chit" ControlToValidate="dxApprovedDate" Display="Dynamic"
                                                        ErrorMessage="Enter Valid Date" Operator="DataTypeCheck" Type="Date" > </asp:CompareValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="label5" runat="server" Text="A.O. Sanction Number"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox TabIndex="5" MaxLength="4" ToolTip="Ex. 1000" placeholder="A.O. Sanction Number"
                                                    runat="server" CssClass="input-text ttip_r sp_number" ID="txtAOSanctionNumber">
                                                </asp:TextBox>
                                                <asp:RequiredFieldValidator EnableClientScript="false" ID="RequiredFieldValidator1" ValidationGroup="Employee" 
                                                    ControlToValidate="txtAOSanctionNumber" Display="Dynamic" SetFocusOnError="true"
                                                    runat="server" ErrorMessage="Enter A.O. Sanction Number"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="vertical-align: middle;">
                                                <asp:Label ID="label6" runat="server" Text="Narration"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox TabIndex="6" runat="server" ToolTip="-" placeholder="Narration" CssClass="input-text ttip_r"
                                                    ID="txtNarration" TextMode="MultiLine">
                                                </asp:TextBox>
                                                <asp:RequiredFieldValidator ValidationGroup="Employee"  EnableClientScript="false" ID="RequiredFieldValidator2"
                                                    ControlToValidate="txtNarration" Display="Dynamic" SetFocusOnError="true" runat="server"
                                                    ErrorMessage="Enter Narration"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="label7" runat="server" Text="Transaction Medium"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList TabIndex="7" Width="240px" ID="ddlMedium" OnSelectedIndexChanged="ddlMedium_SelectedIndexChanged"
                                                    CssClass="chzn-select" runat="server" AutoPostBack="true">
                                                    <asp:ListItem Selected="True" Text="Cash" Value="Cash"></asp:ListItem>
                                                    <asp:ListItem Text="Bank" Value="Bank"></asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                    </table>
                                    <table style="margin: 0 auto;">
                                        <tr>
                                            <td style="vertical-align:top;padding-right:5px;padding-top:6px;">
                                                <asp:Label ID="lblChequeNo" runat="server" Visible="false" Text="Cheque Number"></asp:Label>
                                            </td>
                                            <td style="vertical-align:top;padding-right:5px;">
                                                <asp:TextBox TabIndex="8" MaxLength="7" ToolTip="Ex. 654321" placeholder="Cheque Number" 
                                                    ID="txtChequeNo" CssClass="input-text ttip_r sp_number" Visible="false" runat="server"></asp:TextBox>
                                                <asp:RequiredFieldValidator EnableClientScript="false" Visible="false" ID="RFVtxtIfsc"
                                                    ControlToValidate="txtChequeNo" ValidationGroup="Employee"  Display="Dynamic" SetFocusOnError="true" runat="server"
                                                    ErrorMessage="Enter Cheque Number"></asp:RequiredFieldValidator>
                                            </td>
                                            <td style="vertical-align:top;padding-right:5px;padding-top:6px;">
                                                <asp:Label ID="lblBankHead" Visible="false" runat="server" Text="Bank Head"></asp:Label>
                                            </td>
                                            <td style="vertical-align:top;">
                                                <asp:DropDownList TabIndex="9" Width="240px" Visible="false" ID="ddlBankHead" CssClass="chzn-select" runat="server"
                                                    AutoPostBack="false" CausesValidation="false">
                                                </asp:DropDownList>
                                                <asp:CompareValidator Visible="false" EnableClientScript="false" ID="CVddlBankHead" ValidationGroup="Employee" 
                                                    ValueToCompare="--Select--" ControlToValidate="ddlBankHead" Display="Dynamic"
                                                    Operator="NotEqual" SetFocusOnError="true" runat="server" ErrorMessage="Enter Bank Head"></asp:CompareValidator>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <div style="text-align: center; margin: 0px auto;">
                            <asp:Button TabIndex="10" CausesValidation="true" ID="btnPayLoan" Text="Payment" OnClick="btnPayLoan_Click"
                                CssClass="GreenyPushButton" runat="server" />
                            <asp:Button TabIndex="11" CausesValidation="false" OnClientClick="clearValidationErrors();" ID="btnCancel"
                                Text="Cancel" OnClick="btncancel1_Click" CssClass="GreenyPushButton" runat="server" />
                        </div>
                    <ajax:ModalPopupExtender ID="ModalPopupExtender1" TargetControlID="show" PopupControlID="pnlmsg"
                        BackgroundCssClass="modalBackground" runat="server">
                    </ajax:ModalPopupExtender>
                    <asp:LinkButton ID="show" runat="server"></asp:LinkButton>
                    <asp:Panel CssClass="raised" ID="pnlmsg" runat="server" Visible="false" Width="400px"
                        Style="min-height: 150px">
                        <div style="top: 0px; height: 50px; text-align: center; padding: auto 0; width: 100%"
                            class="boxheader">
                            <asp:Label runat="server" ID="lblh" Text=""> </asp:Label>
                        </div>
                        <div style="min-height: 100px;text-align:center;">
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
                    <asp:Panel CssClass="raised" ID="Panel1" runat="server" Visible="false" Width="400px"
                        Style="min-height: 150px">
                        <div style="top: 0px; height: 50px; text-align: center; padding: auto 0; width: 100%"
                            class="boxheader">
                            <asp:Label runat="server" ID="Label8" Text=""> </asp:Label>
                        </div>
                        <div style="min-height: 100px;text-align:center;">
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
