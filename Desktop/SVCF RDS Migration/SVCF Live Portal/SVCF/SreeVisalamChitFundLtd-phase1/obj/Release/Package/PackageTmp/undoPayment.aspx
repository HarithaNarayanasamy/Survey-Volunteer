<%@ Page Language="C#" MasterPageFile="~/Branch.Master" AutoEventWireup="true" CodeBehind="undoPayment.aspx.cs"
    Inherits="SreeVisalamChitFundLtd_phase1.undoPayment" Title="SVCF Admin Panel" %>

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
        #ctl00_cphMainContent_ddlGroup_chzn .chzn-drop .chzn-search input[type="text"]
        {
            height:15px;
        }
        #ctl00_cphMainContent_ddlToken_chzn .chzn-drop .chzn-search input[type="text"]
        {
            height:15px;
        }
    </style>
    <ajax:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </ajax:ToolkitScriptManager>
    <div class="row">
        <div class="twelve columns">
            <div class="box_c">
                <div class="box_c_heading  box_actions">
                    <p>
                        Undo Payment</p>
                </div>
                <div class="box_c_content">
                    <div class="row">
                        <br />
                        <div style="width: 100%">
                            <asp:Panel runat="server" DefaultButton="Button3">
                                <asp:UpdatePanel runat="server">
                                    <ContentTemplate>
                                        <table runat="server" style="display: table; margin: 0px auto;">
                                            <tr>
                                                <td style="vertical-align: top; padding-top: 6px; padding-right: 5px;">
                                                    <asp:Label runat="server" Visible="false" ID="lbDual"></asp:Label>
                                                    <asp:Label runat="server" Text="Chit Group" ID="Label3"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList onchange="clearValidationErrors();" Width="240px" CssClass="chzn-select" ID="ddlGroup" runat="server"
                                                        AutoPostBack="true" OnSelectedIndexChanged="load_ddlGroup">
                                                    </asp:DropDownList>
                                                    <asp:CompareValidator ID="CompareValidator11" ValidationGroup="undo"
                                                        ValueToCompare="0" SetFocusOnError="true" ControlToValidate="ddlGroup"
                                                        Display="Dynamic" Operator="NotEqual" runat="server" ErrorMessage="Select Chit Group"></asp:CompareValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="vertical-align: top; padding-top: 6px; padding-right: 5px;">
                                                    <asp:Label runat="server" Text="Token" ID="Label4"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList  onchange="clearValidationErrors();"  AutoPostBack="true" Width="240px" CssClass="chzn-select" ID="ddlToken"
                                                        runat="server" OnSelectedIndexChanged="load_ddlToken">
                                                    </asp:DropDownList>
                                                    <asp:CompareValidator ValidationGroup="undo" ID="CompareValidator1"
                                                        ValueToCompare="--Select--" SetFocusOnError="true" ControlToValidate="ddlToken"
                                                        Display="Dynamic" Operator="NotEqual" runat="server" ErrorMessage="Select Token"></asp:CompareValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="vertical-align: top; padding-top: 6px; padding-right: 5px;">
                                                    <asp:Label runat="server" Text="Draw Number" ID="Label5"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtDrawNumber" CssClass="input-text" ReadOnly="true" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ValidationGroup="undo" ID="RequiredFieldValidator1"
                                                        ErrorMessage="Select Draw Number" ControlToValidate="txtDrawNumber" Display="Dynamic"
                                                        runat="server"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                        </table>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                <table runat="server" style="display: table; margin: 0px auto;">
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                            <asp:Button ValidationGroup="undo" ID="Button3" runat="server" CssClass="GreenyPushButton"
                                                Text="Undo" OnClick="load_btnPayment"></asp:Button>
                                            <asp:Button ID="Button4" CausesValidation="false" OnClientClick="clearValidationErrors();"
                                                runat="server" CssClass="GreenyPushButton" Text="Cancel" OnClick="load_btnCancel">
                                            </asp:Button>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
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
                                    <asp:Button CssClass="GreenyPushButton" ID="BtnOK" runat="server" OnClick="load_btnconfirm"
                                        Text="Confirm" />
                                    <asp:Button CssClass="GreenyPushButton" ID="Button2" runat="server" OnClick="load_btnexit"
                                        Text="Cancel" />
                                </div>
                            </div>
                        </asp:Panel>
                        <asp:Panel CssClass="raised" ID="Panel1" runat="server" Visible="false" Width="350px"
                            Style="min-height: 100px">
                            <div style="top: 0px; height: 50px; text-align: center; padding: auto 0; width: 100%"
                                class="boxheader">
                                <asp:Label runat="server" ID="Label1" Text=""> </asp:Label>
                            </div>
                            <div style="min-height: 100px; text-align: center;">
                                <br />
                                <br />
                                <asp:Label runat="server" ID="Label2" Text=""> </asp:Label>
                                <br />
                                <br />
                            </div>
                            <div class="boxheader">
                                <div style="margin: 0 auto;">
                                    <asp:Button CssClass="GreenyPushButton" ID="Button1" runat="server" OnClick="load_btnconfirm1"
                                        Text="Ok" />
                                </div>
                            </div>
                        </asp:Panel>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        $(document).ready(function () {
            $(".chzn-select").chosen({ search_contains: true });
        });

        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $(".chzn-select").chosen({ search_contains: true });
        });
    </script>
</asp:Content>
