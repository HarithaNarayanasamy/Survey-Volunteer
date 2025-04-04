<%@ Page  Language="C#" MasterPageFile="~/Branch.Master" AutoEventWireup="true"
    CodeBehind="Limit.aspx.cs" Inherits="SreeVisalamChitFundLtd_phase1.Limit" %>

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
                        Blocking</p>
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
                                                    <asp:Label ID="labelSeries" runat="server" Text="Branch"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList TabIndex="1" ID="ddlBranch" Width="240px" runat="server"
                                                        CssClass="chzn-select" AutoPostBack="true" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                    <asp:CompareValidator EnableClientScript="false" ID="CVddlGroupNo" runat="server"
                                                        ForeColor="Red" ValidationGroup="chit" ControlToValidate="ddlBranch" Display="Dynamic"
                                                        SetFocusOnError="true" ErrorMessage="Select Branch" Operator="NotEqual" ValueToCompare="0"> </asp:CompareValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="vertical-align: top; padding-top: 6px; padding-right: 5px;">
                                                    <asp:Label ID="label2" runat="server" Text="Minimum Date"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox TabIndex="3" runat="server" CssClass="input-text maskdate" 
                                                        placeholder="Date" ID="txtDate"></asp:TextBox>
                                                    <asp:RequiredFieldValidator EnableClientScript="false" ID="RFVtxtAmount" ControlToValidate="txtDate"
                                                        ValidationGroup="chit" Display="Dynamic" SetFocusOnError="true" runat="server"
                                                        ForeColor="Red" ErrorMessage="Enter Date"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="vertical-align: top; padding-top: 6px; padding-right: 5px;">
                                                    
                                                </td>
                                                <td>
                                                   <asp:Label ID="lbError" runat="server" ></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                <div style="margin: 0px auto; text-align: center;">
                                    <asp:Button TabIndex="11" CausesValidation="true" ValidationGroup="chit" ID="btnPayLoan"
                                        Text="Ok" OnClick="btnOk_Click" CssClass="GreenyPushButton" runat="server" />
                                    <asp:Button TabIndex="12" CausesValidation="false" OnClientClick="clearValidationErrors();"
                                        ID="btnCancel" Text="Cancel" OnClick="btncancel1_Click" CssClass="GreenyPushButton"
                                        runat="server" />
                                </div>
                            </asp:Panel>
                        </div>
                    </div>
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
