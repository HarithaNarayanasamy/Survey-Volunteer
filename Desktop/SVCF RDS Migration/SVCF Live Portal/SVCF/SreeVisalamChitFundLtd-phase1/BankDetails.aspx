<%@ Page Culture="en-GB" Title="SVCF Admin Panel" Language="C#" MasterPageFile="~/Branch.Master"
    AutoEventWireup="true" CodeBehind="BankDetails.aspx.cs" Inherits="SreeVisalamChitFundLtd_phase1.Admin.BankDetails" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Head1" ContentPlaceHolderID="cphHead" runat="server">
    <style type="text/css">
        .chzn-results
        {
            text-align: center;
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
    <style type="text/css">
        .chzn-drop
        {
            border: 1px solid #aaa !important;
            padding-top: 5px !important;
            width: 254px !important;
        }
        #ctl00_cphMainContent_ddlTypeBank_chzn .chzn-drop .chzn-search input[type="text"]
        {
            height: 15px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMainContent" runat="server">
    <ajax:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </ajax:ToolkitScriptManager>
    <div class="row">
        <div class="twelve columns">
            <div class="box_c">
                <div class="box_c_heading  box_actions">
                    <p>
                        Bank Addition</p>
                </div>
                <div class="box_c_content">
                    <div class="row">
                        <asp:Panel runat="server" DefaultButton="btnAdd">
                            <div style="width: 100%;">
                                <table cellspacing="3" style="margin: 0 auto;">
                                    <tr>
                                        <td style="vertical-align: top; padding-top: 6px; padding-right: 5px;">
                                            <asp:Label ID="LblBankName" Text="Bank Name" runat="server"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox TabIndex="1" ID="txtBankName" runat="server" ToolTip="Ex. Indian Overseas Bank"
                                                placeholder="Bank Name" CssClass="input-text ttip_r"></asp:TextBox>
                                            <asp:RequiredFieldValidator ValidationGroup="bankgroup" ID="RequiredFieldValidator1"
                                                runat="server" ControlToValidate="txtBankName" Display="Dynamic" ErrorMessage="Enter Bank Name"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="vertical-align: top; padding-top: 6px; padding-right: 5px;">
                                            <asp:Label ID="Label1" Text="Bank Location " runat="server"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox TabIndex="2" ID="txtbanklocation" runat="server" ToolTip="Ex. Chennai"
                                                placeholder="Bank Location" CssClass="input-text ttip_r"></asp:TextBox>
                                            <asp:RequiredFieldValidator ValidationGroup="bankgroup" ID="RequiredFieldValidator6"
                                                runat="server" ControlToValidate="txtbanklocation" Display="Dynamic" ErrorMessage="Enter Bank Location"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="vertical-align: top; padding-top: 6px; padding-right: 5px;">
                                            <asp:Label ID="Label2" Text="IFSC Code" runat="server"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox TabIndex="3" ID="txtIFCcode" runat="server" CssClass="input-text ttip_r"
                                                ToolTip="Ex. IOBA0000420" placeholder="IFSC Code"></asp:TextBox>
                                            <asp:RequiredFieldValidator ValidationGroup="bankgroup" ID="rfvIFCCode" runat="server"
                                                ControlToValidate="txtIFCcode" Display="Dynamic" ErrorMessage="Enter IFSC Code"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="vertical-align: top; padding-top: 6px; padding-right: 5px;">
                                            <asp:Label ID="Label6" Text="Type of Bank" runat="server"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:DropDownList TabIndex="4" ID="ddlTypeBank" CssClass="chzn-select" Width="240px"
                                                runat="server">
                                                <asp:ListItem Text="--select--"></asp:ListItem>
                                                <asp:ListItem Text="Scheduled Banks"></asp:ListItem>
                                                <asp:ListItem Text="Non Scheduled Banks"></asp:ListItem>
                                                <asp:ListItem Text="Fixed deposits with Banks"></asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:CompareValidator ValidationGroup="bankgroup" ControlToValidate="ddlTypeBank"
                                                ValueToCompare="--select--" Operator="NotEqual" Display="Dynamic" ID="CompareValidator1"
                                                runat="server" ErrorMessage="Select Type of Bank"></asp:CompareValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="vertical-align: top; padding-top: 6px; padding-right: 5px;">
                                            <asp:Label ID="Label3" Text="Account Number" runat="server"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox TabIndex="5" ID="txtAccountNo" MaxLength="16" runat="server" CssClass="input-text sp_number ttip_r"
                                                ToolTip="Ex. 0000010566812834" placeholder="Account Number"></asp:TextBox>
                                            <asp:RequiredFieldValidator ValidationGroup="bankgroup" ID="RequiredFieldValidator3"
                                                runat="server" ControlToValidate="txtAccountNo" Display="Dynamic" ErrorMessage="Enter Account Number"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="vertical-align: top; padding-top: 46px; padding-right: 5px;">
                                            <asp:Label ID="Label4" Text="Address" runat="server"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtAddress" TabIndex="6" runat="server" TextMode="MultiLine" CssClass="input-text ttip_r"
                                                ToolTip="Ex. M L Road,<br/> Panbazar,<br> Guwahati-781001." placeholder="Address"></asp:TextBox>
                                            <asp:RequiredFieldValidator ValidationGroup="bankgroup" ID="RequiredFieldValidator4"
                                                runat="server" ControlToValidate="txtAddress" Display="Dynamic" ErrorMessage="Enter Address"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="vertical-align: top; padding-top: 6px; padding-right: 5px;">
                                            <asp:Label ID="Label5" Text="Date of Account" runat="server"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox TabIndex="7" mask="mask_date" ID="txtDoAccount" runat="server" CssClass="input-text ttip_r maskdate"
                                                ToolTip="" placeholder="Date of Account"></asp:TextBox>
                                            <asp:RequiredFieldValidator ValidationGroup="bankgroup" ID="Rfvdate" runat="server"
                                                ControlToValidate="txtDoAccount" Display="Dynamic" ErrorMessage="Enter Date of Account"></asp:RequiredFieldValidator>
                                            <asp:CompareValidator ValidationGroup="bankgroup" runat="server" Display="Dynamic"
                                                ErrorMessage="Enter Valid Date" ControlToValidate="txtDoAccount" Operator="DataTypeCheck"
                                                Type="Date"></asp:CompareValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                            <asp:Button TabIndex="8" ValidationGroup="bankgroup" ID="btnAdd" runat="server" Text="Add"
                                                OnClick="btnAdd_Click" CssClass="GreenyPushButton" Style="margin: 0px auto;" />
                                            <asp:Button TabIndex="9" ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click"
                                                CssClass="GreenyPushButton" Style="margin: 0px auto;" OnClientClick="clearValidationErrors();" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </asp:Panel>
                    </div>
                    <ajax:ModalPopupExtender ID="ModalPopupExtender1" BackgroundCssClass="modalBackground"
                        TargetControlID="lb" PopupControlID="Pnlmsg" runat="server">
                    </ajax:ModalPopupExtender>
                    <asp:LinkButton ID="lb" Text="" runat="server"></asp:LinkButton>
                    <asp:Panel CssClass="raised" ID="Pnlmsg" runat="server" Visible="false" Width="350px"
                        Style="min-height: 100px">
                        <asp:Label runat="server" ID="lblHint" Text="" Visible="false"> </asp:Label>
                        <div class="boxfooter">
                            <asp:Label ID="lblHeading" runat="server" Text=""> </asp:Label>
                        </div>
                        <div id="Div1" style="text-align: center;">
                            <br />
                            <br />
                            <asp:Label ID="lblContent" runat="server" Text=""> </asp:Label>
                            <br />
                            <br />
                            <asp:PlaceHolder ID="DynamicControlsHolder" runat="server"></asp:PlaceHolder>
                        </div>
                        <div class="boxheader" style="width: 100%; position: absolute">
                            <asp:Button OnClick="btn_ok" CssClass="GreenyPushButton" ID="Button2" runat="server"
                                Text="Ok" />
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
    </script>
</asp:Content>
