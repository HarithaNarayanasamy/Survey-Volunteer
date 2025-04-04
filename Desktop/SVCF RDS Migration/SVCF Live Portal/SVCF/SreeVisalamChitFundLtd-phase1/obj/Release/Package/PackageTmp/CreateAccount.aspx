<%@ Page Title="SVCF Admin Panel" Language="C#" MasterPageFile="~/Branch.Master"
    AutoEventWireup="true" CodeBehind="CreateAccount.aspx.cs" Inherits="SreeVisalamChitFundLtd_phase1.CreateAccount" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Head1" ContentPlaceHolderID="cphHead" runat="server">
    <style type="text/css">
        .chzn-drop
        {
            border: 1px solid #aaa !important;
            padding-top: 5px !important;
            width: 240px !important;
        }
        #ctl00_cphMainContent_DDlBranchName_chzn .chzn-results
        {
           min-height: 10px !important;
           max-height: 100px !important;
        }
        .chzn-results
        {
            text-align: center;
        }
        #ctl00_cphMainContent_DDlBranchName_chzn .chzn-drop .chzn-search input[type="text"]
        {
            height: 15px;
        }
        .PnlDesign
        {
            border: solid 1px #000000;
            height: 100px;
            width: 246px;
            overflow-y: scroll;
            background-color:White;
            font-size: 15px;
            font-family: Arial;
        }
        .txtbox
        {
            background-position: right top;
            background-repeat: no-repeat;
            cursor: pointer;
            cursor: hand;
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
                        Create Account</p>
                </div>
                <div class="box_c_content">
                    <div class="row">
                        <asp:Panel runat="server" DefaultButton="btnCreate">
                            <div style="width: 100%;">
                                <table style="margin: 0px auto;" cellspacing="3">
                                    <tr>
                                        <td style="vertical-align: top; padding-top: 6px; padding-right: 6px;">
                                            <asp:Label ID="LblBranch" Text="Branch Name" runat="server"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:DropDownList TabIndex="1" Width="240px" ID="DDlBranchName" runat="server" CssClass="chzn-select">
                                            </asp:DropDownList>
                                            <asp:CompareValidator ValidationGroup="aaa" ID="CompareValidator11" ValueToCompare="0"
                                                SetFocusOnError="true" ControlToValidate="DDlBranchName" Display="Dynamic" Operator="NotEqual"
                                                runat="server" ErrorMessage="Invalid Branch Name"></asp:CompareValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="vertical-align: top; padding-top: 6px; padding-right: 6px;">
                                            <asp:Label ID="LblEmailID" Text="E-mail ID" runat="server"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox TabIndex="2" ID="TxtEmailID" placeholder="E-mail" runat="server" ToolTip="Ex. unknown@unknown.com"
                                                CssClass="input-text ttip_r"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredEmailID" ControlToValidate="TxtEmailID" ErrorMessage="Enter E-mail ID"
                                                runat="server" ValidationGroup="aaa" Display="Dynamic"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularEmailID" runat="server" ValidationGroup="aaa"
                                                ControlToValidate="TxtEmailID" ErrorMessage="Invaild E-mail!" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                                Display="Dynamic"></asp:RegularExpressionValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="vertical-align: top; padding-top: 6px; padding-right: 6px;">
                                            <asp:Label ID="LblUserName" Text="Username" runat="server"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox AccessKey="S" TabIndex="3" ID="TxtUserName" placeholder="Username" ToolTip="Ex. Sudhakar"
                                                runat="server" CssClass="input-text ttip_r"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="ReqUserName" ValidationGroup="aaa" ControlToValidate="TxtUserName"
                                                ErrorMessage="Enter UserName" runat="server" Display="Dynamic"></asp:RequiredFieldValidator>
                                           <%-- <asp:RegularExpressionValidator ID="RegularExpressionValidator2" ValidationGroup="aaa"
                                                Display="Dynamic" runat="server" ControlToValidate="TxtUserName" ErrorMessage="Enter valid Username (Do not use <br/> Single Quotes and Spaces)"
                                                ValidationExpression="^([a-zA-Z0-9_\-\.]+)"></asp:RegularExpressionValidator>--%>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ValidationGroup="aaa"
                                                ControlToValidate="TxtEmailID" ErrorMessage="Invaild E-mail!" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                                Display="Dynamic"></asp:RegularExpressionValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="vertical-align: top; padding-top: 6px; padding-right: 6px;">
                                            <asp:Label ID="LblPassword" Text="Password" runat="server"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox TabIndex="4" ID="TxtPassword" runat="server" ToolTip="Ex. visalam@123"
                                                CssClass="input-text ttip_r" TextMode="Password" placeholder="Password"></asp:TextBox>
                                            <asp:RequiredFieldValidator ValidationGroup="aaa" ID="ReqPassword" ControlToValidate="TxtPassword"
                                                ErrorMessage="Enter Password" runat="server" Display="Dynamic"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ValidationGroup="aaa"
                                                Display="Dynamic" runat="server" ControlToValidate="TxtPassword" ErrorMessage="Must have at least 1 special character,<br>1 number, Do not use Single Quotes, Semi-colon, <br/>Spaces and more than 6 characters."
                                                ValidationExpression="(?=^.{6,}$)(?=.*\d)(?=.*[a-zA-Z])(?=.*\W+)(?![.\n])^(?!.*\s)(?!.*;)((?!\x27).)*$"></asp:RegularExpressionValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="vertical-align: top; padding-top: 6px; padding-right: 6px;">
                                            <asp:Label ID="Label1" Text="Roles" runat="server"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                <ContentTemplate>
                                                    <asp:TextBox TabIndex="5" ID="txtCustomer" ReadOnly="true" Text="Select Roles" runat="server"
                                                        CssClass="txtbox input-text"></asp:TextBox>
                                                    <asp:Panel ID="PnlCust" runat="server" CssClass="PnlDesign">
                                                        <asp:CheckBoxList CausesValidation="false" AutoPostBack="true" ID="chkRoles" OnSelectedIndexChanged="chkRoles_OnSelectedIndexChanged" runat="server">
                                                            <%--<asp:ListItem Text="Manager" Value="1"></asp:ListItem>--%>
                                                            <asp:ListItem Text="Administrator" Value="2"></asp:ListItem>
                                                           <%-- <asp:ListItem Text="Cashier" Value="3"></asp:ListItem>
                                                            <asp:ListItem Text="Head" Value="4"></asp:ListItem>--%>
                                                            <asp:ListItem Text="User" Value="5"></asp:ListItem>
                                                             <asp:ListItem Text="Report" Value="5"></asp:ListItem>
                                                        </asp:CheckBoxList>
                                                    </asp:Panel>
                                                    <ajax:PopupControlExtender ID="PceSelectCustomer" runat="server" TargetControlID="txtCustomer"
                                                        PopupControlID="PnlCust" Position="Bottom">
                                                    </ajax:PopupControlExtender>
                                                    <asp:CompareValidator  ValidationGroup="aaa" ID="RequiredFieldValidator1" ControlToValidate="txtCustomer"
                                                        ErrorMessage="Select Atleast One Role" ValueToCompare="Select Roles" Operator="NotEqual" runat="server" Display="Dynamic"></asp:CompareValidator>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td style="padding-top: 6px;">
                                            <asp:Button TabIndex="6" ValidationGroup="aaa" ID="btnCreate" Text="Create" runat="server"
                                                CssClass="GreenyPushButton" OnClick="btnCreate_Click"></asp:Button>
                                            <asp:Button TabIndex="7" ID="Button1" Text="Cancel" runat="server" CssClass="GreenyPushButton"
                                                OnClientClick="clearValidationErrors();" OnClick="btnCancel_Click"></asp:Button>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </asp:Panel>
                    </div>
                    <asp:Panel CssClass="raised" ID="Pnlmsg" runat="server" Visible="false" Width="600px"
                        Style="min-height: 100px">
                        <asp:Label runat="server" ID="lblHint" Visible="false"> </asp:Label>
                        <div style="height: 50px; text-align: center; width: 100%" class="boxfooter">
                            <asp:Label ID="lblTs" runat="server" Text=""> </asp:Label>
                        </div>
                        <div id="Div1" style="min-height: 100px; text-align: center;">
                            <br />
                            <br />
                            <asp:Label ID="lblContent" runat="server" Text=""> </asp:Label>
                            <br />
                            <br />
                            <asp:PlaceHolder ID="DynamicControlsHolder" runat="server"></asp:PlaceHolder>
                        </div>
                        <div class="boxheader">
                            <div style="margin: 0 auto;">
                                <asp:Button Style="margin: 0 auto" CssClass="GreenyPushButton" ID="btnOK" OnClick="btnCancel_Click"
                                    runat="server" CausesValidation="false" Text="Ok" />
                            </div>
                        </div>
                    </asp:Panel>
                    <ajax:ModalPopupExtender ID="ModalPopupExtender2" BackgroundCssClass="modalBackground"
                        TargetControlID="lnkPop" PopupControlID="Pnlmsg" runat="server">
                    </ajax:ModalPopupExtender>
                    <asp:LinkButton ID="lnkPop" runat="server"></asp:LinkButton>
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript">

        $(document).ready(function () {
            $(".chzn-select").chosen({ search_contains: true });
        });
         
    </script>
</asp:Content>
