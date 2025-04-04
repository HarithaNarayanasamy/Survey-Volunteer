<%@ Page Language="C#" MasterPageFile="~/Branch.Master" AutoEventWireup="true" CodeBehind="MoneyCollector.aspx.cs"
    Inherits="SreeVisalamChitFundLtd_phase1.WebForm10" Title="SVCF Admin Panel" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Head1" ContentPlaceHolderID="cphHead" runat="server">
    <style type="text/css">
        .chzn-drop
        {
            border: 1px solid #aaa !important;
            padding-top: 5px !important;
            width: 240px !important;
        }
        #ctl00_cphMainContent_ddlMoneyCollectorID_chzn .chzn-results
        {
            height: 150px;
        }
        .chzn-results
        {
            text-align: center;
        }
        #ctl00_cphMainContent_ddlMoneyCollectorID_chzn .chzn-drop .chzn-search input[type="text"]
        {
            height: 15px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMainContent" runat="server">
    <ajax:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </ajax:ToolkitScriptManager>
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
    <div class="row">
        <div class="twelve columns">
            <div class="box_c">
                <div class="box_c_heading  box_actions">
                    <p>
                        Money Collector Details</p>
                </div>
                <div class="box_c_content">
                    <div class="row">
                        <asp:Panel runat="server" DefaultButton="btnAddcollector">
                            <asp:UpdatePanel runat="server">
                                <ContentTemplate>
                                    <div style="width: 100%;">
                                        <table style="margin: 0px auto;">
                                            <tr>
                                                <td style="vertical-align: top; padding-top: 5px; padding-right: 8px;">
                                                    <asp:Label ID="label2" runat="server" Text="Money Collector Name"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList onchange="clearValidationErrors();" AutoPostBack="true" OnSelectedIndexChanged="ddlMoneyCollector_SelectedIndexChanged"
                                                        TabIndex="1" Width="240px" CssClass="chzn-select" ID="ddlMoneyCollector" runat="server">
                                                    </asp:DropDownList>
                                                    <asp:CompareValidator ID="CompareValidator11" ValueToCompare="0" ValidationGroup="mid"
                                                        ControlToValidate="ddlMoneyCollector" Display="Dynamic" Operator="NotEqual" runat="server"
                                                        ErrorMessage="Select Money Collector"></asp:CompareValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="vertical-align: top; padding-top: 46px; padding-right: 8px;">
                                                    <asp:Label ID="label3" runat="server" Text="Money Collector Address"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox TabIndex="2" ID="txtcollectoradd" runat="server" TextMode="MultiLine"
                                                        placeholder="Address" ToolTip="Ex. 196/23, Thiruvenkatasamy Road (West),<br/> R.S.Puram,<br/>  Coimbatore - 641002."
                                                        CssClass="input-text ttip_r"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ValidationGroup="mid" ID="RequiredFieldValidator3" Display="Dynamic"
                                                        runat="server" ControlToValidate="txtcollectoradd" ErrorMessage="Enter Address"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="vertical-align: top; padding-top: 5px; padding-right: 8px;">
                                                    <asp:Label ID="label4" runat="server" Text="Money Collector Phone Number"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox TabIndex="3" ID="txtcollecorphoneno" runat="server" ToolTip="Ex. 0422-7575757 or 9876543210"
                                                        CssClass="input-text ttip_r" placeholder="Phone Number"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ValidationGroup="mid" ID="RequiredFieldValidator4" Display="Dynamic"
                                                        runat="server" ControlToValidate="txtcollecorphoneno" ErrorMessage="Enter Phone Number"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ValidationGroup="mid" ID="RegularExpressionValidator1"
                                                        runat="server" Display="Dynamic" ControlToValidate="txtcollecorphoneno" ValidationExpression="(\+91)?\d{2,}(-)?\d{6,}"
                                                        ErrorMessage="Invalid Phone Number"></asp:RegularExpressionValidator>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <table style="margin: 0px auto; padding-left: 2px;">
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                        <asp:Button ID="btnAddcollector" CausesValidation="true" Text="Add" runat="server"
                                            TabIndex="4" ValidationGroup="mid" CssClass="GreenyPushButton" OnClick="btnAddcollector_Click">
                                        </asp:Button>
                                        <asp:Button CausesValidation="false" ID="btnCancel" Text="Cancel" runat="server"
                                            TabIndex="5" OnClientClick="clearValidationErrors();" CssClass="GreenyPushButton"
                                            OnClick="btnCan_Click"></asp:Button>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </div>
                    <asp:Panel CssClass="raised" ID="Pnlmsg1" runat="server" Visible="false" Width="600px"
                        Style="display: table;">
                        <asp:Label runat="server" ID="lblHint" Text="" Visible="false"> </asp:Label>
                        <div style="top: 0px; height: 60px; text-align: center; padding: auto 0; width: 100%"
                            class="boxfooter">
                            <asp:Label ID="lblT" runat="server" Text=""> </asp:Label>
                        </div>
                        <div style="min-height: 100px; text-align: center">
                            <br />
                            <br />
                            <asp:Label ID="lblContent" runat="server" Text=""> </asp:Label>
                            <br />
                            <br />
                            <asp:PlaceHolder ID="DynamicControlsHolder" runat="server"></asp:PlaceHolder>
                        </div>
                        <div class="boxheader">
                            <div style="margin: 0 auto;">
                                <asp:Button CssClass="GreenyPushButton" ID="btnOK" runat="server" OnClick="btnOK_Click"
                                    CausesValidation="false" Text="Ok" />
                            </div>
                        </div>
                    </asp:Panel>
                    <ajax:ModalPopupExtender ID="ModalPopupExtender2" BackgroundCssClass="modalBackground"
                        TargetControlID="ShowPopup1" PopupControlID="Pnlmsg1" runat="server">
                    </ajax:ModalPopupExtender>
                    <asp:LinkButton ID="ShowPopup1" runat="server"></asp:LinkButton>
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
            $(".sp_number").numeric({
                decimal: false,
                negative: false
            },
                function () {
                    alert("Positive integers only");
                    this.value = "";
                    this.focus()
                });
            prth_mask_input.init();
            prth_tips.init();
        });
    </script>
</asp:Content>
