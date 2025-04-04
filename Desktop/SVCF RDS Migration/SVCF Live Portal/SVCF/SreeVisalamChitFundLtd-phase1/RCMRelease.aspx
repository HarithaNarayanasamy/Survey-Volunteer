<%@ Page Title="" Culture="en-GB"  Language="C#" MasterPageFile="~/Branch.Master" AutoEventWireup="true" CodeBehind="RCMRelease.aspx.cs" Inherits="SreeVisalamChitFundLtd_phase1.RCMRelease" %>
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
                        Fd Release</p>
                </div>
                <div class="box_c_content">
                    <asp:Panel ID="Panel1" runat="server" DefaultButton="btnPayment">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <div class="row">
                                    <div style="width: 100%">
                                        <table style="display: table; margin: 0px auto;">
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label3" runat="server" Text="Chit Number"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList onchange="clearValidationErrors();" TabIndex="1" Width="240px"
                                                        CssClass="chzn-select" ID="ddlGroupNumber" runat="server" >
                                                    </asp:DropDownList>
                                                    <asp:CompareValidator ForeColor="Red" ID="CompareValidator11" ValueToCompare="0"
                                                        ValidationGroup="pa" ControlToValidate="ddlGroupNumber" Display="Dynamic" Operator="NotEqual"
                                                        runat="server" ErrorMessage="Select Chit Group"></asp:CompareValidator>
                                                </td>
                                               
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="label8" runat="server" Text="Amount"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox TabIndex="2" ToolTip="Ex. 1000.00" placeholder="Amount" CssClass="input-text sp_float"
                                                        ID="txtAmount" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ForeColor="Red" ControlToValidate="txtAmount" Display="Dynamic"
                                                        ValidationGroup="pa" ID="RequiredFieldValidator14" runat="server" ErrorMessage="Enter Amount"></asp:RequiredFieldValidator>
                                                </td>
                                                
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="label13" runat="server" Text="Release Date"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtDepositDate" TabIndex="3" CssClass="input-text maskdate" placeholder="Deposit Date"
                                                        runat="server">
                                                    </asp:TextBox>
                                                    <asp:RequiredFieldValidator ForeColor="Red" ID="RequiredFieldValidator5" ErrorMessage="Enter Deposit Date"
                                                        ControlToValidate="txtDepositDate" ValidationGroup="pa" Display="Dynamic" runat="server"></asp:RequiredFieldValidator>
                                                    <asp:CompareValidator ForeColor="Red" Type="Date" ID="CompareValidator2" ValidationGroup="pa"
                                                        ControlToValidate="txtDepositDate" Display="Dynamic" Operator="DataTypeCheck"
                                                        runat="server" ErrorMessage="Incorrect!!!"></asp:CompareValidator>
                                                    <asp:RangeValidator ID="rvDepositDate" runat="server" Type="Date" ControlToValidate="txtDepositDate"
                                                        ValidationGroup="pa" ForeColor="Red" Display="Dynamic" ErrorMessage="*"></asp:RangeValidator>
                                                </td>
                                               
                                            </tr>
                                            <tr>
                                                
                                                <td style="vertical-align: top; padding-right: 5px; padding-top: 6px;">
                                                    <asp:Label ID="label14" runat="server" Text="Bank"></asp:Label>
                                                </td>
                                                <td style="vertical-align: top; padding-right: 5px;">
                                                    <asp:DropDownList TabIndex="4" Width="240px" CssClass="chzn-select" ID="ddlBank"
                                                        runat="server">
                                                    </asp:DropDownList>
                                                    <asp:CompareValidator ForeColor="Red" ID="CompareValidator4" ValueToCompare="0" ValidationGroup="pa"
                                                        ControlToValidate="ddlBank" Display="Dynamic" Operator="NotEqual" runat="server"
                                                        ErrorMessage="Select Bank"></asp:CompareValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="label2" runat="server" Text="Cheque Number"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox MaxLength="6" autocomplete="off" TabIndex="5" placeholder="Cheque Number"
                                                        ToolTip="Ex. 50" CssClass="input-text sp_number" ID="txtChequeNumber" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ForeColor="Red" ID="RequiredFieldValidator3" ErrorMessage="Enter Cheque Number"
                                                        ControlToValidate="txtChequeNumber" ValidationGroup="pa" Display="Dynamic" runat="server"></asp:RequiredFieldValidator>
                                                </td>
                                                
                                            </tr>
                                             <tr>
                                                <td>
                                                    <asp:Label ID="label1" runat="server" Text="Narration"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox TextMode="MultiLine" autocomplete="off" TabIndex="6" placeholder="Narration"
                                                         CssClass="input-text" ID="txtNarration" runat="server"></asp:TextBox>
                                                </td>
                                                
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </asp:Panel>
                    <div id="Div1" runat="server" style="margin: 0px auto; display: table;">
                        <asp:Button TabIndex="7" CausesValidation="true" ID="btnPayment" runat="server"
                            ValidationGroup="pa" CssClass="GreenyPushButton" OnClick="btnPayment_Click" Text="Add">
                        </asp:Button>
                        <asp:Button TabIndex="8" Style="margin-left: 10px;" CausesValidation="false" ID="btnCancel"
                            runat="server" CssClass="GreenyPushButton" OnClientClick="clearValidationErrors();"
                            OnClick="btnCancel_Click" Text="Cancel"></asp:Button>
                    </div>
                    <ajax:ModalPopupExtender ID="ModalPopupExtender1" TargetControlID="show" PopupControlID="pnlmsg"
                        BackgroundCssClass="modalBackground" runat="server">
                    </ajax:ModalPopupExtender>
                    <asp:LinkButton ID="show" runat="server"></asp:LinkButton>
                    <asp:Panel CssClass="raised" ID="pnlmsg" runat="server" Visible="false" Width="350px"
                        Style="min-height: 100px">
                        <div style="top: 0px; height: 50px; text-align: center; padding: auto 0; width: 100%"
                            class="boxheader">
                            <asp:Label runat="server" ID="lblh" Text=""> </asp:Label>
                        </div>
                        <div style="min-height: 100px; text-align: center;">
                            <br />
                            <br />
                            <asp:Label runat="server" ID="lblcon" Text=""> </asp:Label>
                            <br />
                            <br />
                        </div>
                        <div class="boxheader">
                            <div style="margin: 0 auto;">
                                <asp:Button Style="margin: 0 auto" CssClass="GreenyPushButton" OnClick="btnOK_Click"
                                    ID="btnOK" CausesValidation="false" OnClientClick="clearValidationErrors();"
                                    runat="server" Text="Yes" />
                            </div>
                        </div>
                    </asp:Panel>
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        $(document).ready(function () {
            $(".chzn-select").chosen({ search_contains: true });
            //            $(".sp_floats").spinner({
            //                decimals: 2,
            //                stepping: 0.50,
            //                min: 0.00
            //            });

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
            //            $(".sp_floats").spinner({
            //                decimals: 2,
            //                stepping: 0.50,
            //                min: 0.00
            //            });
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
