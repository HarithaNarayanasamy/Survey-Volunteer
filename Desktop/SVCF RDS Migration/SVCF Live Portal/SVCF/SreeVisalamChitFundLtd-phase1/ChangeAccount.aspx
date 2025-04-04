<%@ Page Title="SVCF Admin Panel" Language="C#" MasterPageFile="~/Branch.Master"
    AutoEventWireup="true" CodeBehind="ChangeAccount.aspx.cs" Inherits="SreeVisalamChitFundLtd_phase1.ChangeAccount" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Head1" ContentPlaceHolderID="cphHead" runat="server">
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
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMainContent" runat="server">
    <ajax:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </ajax:ToolkitScriptManager>
    <div class="row">
        <div class="twelve columns">
            <div class="box_c">
                <div class="box_c_heading  box_actions">
                    <p>
                        Change Account</p>
                </div>
                <div class="box_c_content">
                    <div class="row">
                        <asp:Panel runat="server" DefaultButton="btnCreate">
                            <div style="width: 100%;">
                                <table style="margin: 0px auto;">
                                    <tr>
                                        <td style="vertical-align: top; padding-top: 6px; padding-right: 10px;">
                                            <asp:Label ID="Label1" Text="Branch Name" runat="server"></asp:Label>
                                        </td>
                                        <td>
                                             <asp:DropDownList ID="ddlBranchs" Width="150px" CssClass="chzn-select" AutoPostBack="true" 
                                                  runat="server"></asp:DropDownList>   
                                        </td>
                                    </tr>
                                     <tr>
                                        <td style="vertical-align: top; padding-top: 6px; padding-right: 10px;">
                                            <asp:Label ID="Label2" Text="Select User Type" runat="server"></asp:Label>
                                        </td>
                                        <td>
                                             <asp:DropDownList ID="ddUserType" Width="150px" CssClass="chzn-select" AutoPostBack="true" 
                                                  runat="server" OnSelectedIndexChanged="ddUserType_SelectedIndexChanged">
                                                 <asp:ListItem>Select</asp:ListItem>
                                                 <asp:ListItem>User</asp:ListItem>
                                                 <asp:ListItem>Admin</asp:ListItem>
                                                 <asp:ListItem>Report</asp:ListItem>
                                             </asp:DropDownList>   
                                        </td>
                                    </tr>


                                    <tr>
                                        <td style="vertical-align: top; padding-top: 6px; padding-right: 10px;">
                                            <asp:Label ID="LblUserName" Text="Username" runat="server"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TxtUserName" TabIndex="1" runat="server" placeholder="Username" ReadOnly="true"
                                                CssClass="input-text"></asp:TextBox>
                                            <asp:RequiredFieldValidator ValidationGroup="aaa" ID="ReqUserName" ControlToValidate="TxtUserName"
                                                ErrorMessage="Enter Username" runat="server" Display="Dynamic"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                  
                                    <tr>
                                        <td style="vertical-align: top; padding-top: 6px; padding-right: 10px;">
                                            <asp:Label ID="LblOldPassword" Text="Old Password" runat="server"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox TabIndex="3" ToolTip="Ex. visalam@123" placeholder="Old Password" ID="TxtOldPassword"
                                                runat="server" CssClass="input-text ttip_r" TextMode="Password"></asp:TextBox>
                                            <asp:RequiredFieldValidator ValidationGroup="aaa" ID="ReqTxtOldPassword" ControlToValidate="TxtOldPassword"
                                                ErrorMessage="Enter Old Password" runat="server" Display="Dynamic"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="vertical-align: top; padding-top: 6px; padding-right: 10px;">
                                            <asp:Label ID="LblNewPassword" Text="New Password" runat="server"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox TabIndex="4" ToolTip="Ex. visalam@123" placeholder="New Password" ID="TxtNewPassword"
                                                runat="server" CssClass="input-text ttip_r" TextMode="Password"></asp:TextBox>
                                            <asp:RequiredFieldValidator ValidationGroup="aaa" ID="ReqNewPassword" ControlToValidate="TxtNewPassword"
                                                ErrorMessage="Please New Password" runat="server" Display="Dynamic"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ValidationGroup="aaa" Display="Dynamic"
                                                runat="server" ControlToValidate="TxtNewPassword" ErrorMessage="Must have at least 1 special character,<br>1 number, Do not use Single Quotes,  <br/>Spaces and more than 6 characters."
                                                ValidationExpression="(?=^.{6,}$)(?=.*\d)(?=.*[a-zA-Z])(?=.*\W+)(?![.\n])^(?!.*\s)(?!.*;)((?!\x27).)*$"></asp:RegularExpressionValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="vertical-align: top; padding-top: 6px; padding-right: 10px;">
                                            <asp:Label ID="LblConfirmPassword" Text="Confirm Password" runat="server"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox TabIndex="5" placeholder="Confirm Password" ID="TxtConfirmPassword" runat="server"
                                                ToolTip="Ex. visalam@123" CssClass="input-text ttip_r" TextMode="Password"></asp:TextBox>
                                            <asp:RequiredFieldValidator ValidationGroup="aaa" ID="ReqConfirmPassword" ControlToValidate="TxtConfirmPassword"
                                                ErrorMessage="Enter Confirm Password" runat="server" Display="Dynamic"></asp:RequiredFieldValidator>
                                            <asp:CompareValidator ID="ComparePassword" runat="server" ControlToCompare="TxtNewPassword" ValidationGroup="aaa" 
                                                Display="Dynamic" ControlToValidate="TxtConfirmPassword" ErrorMessage="Not equal!"></asp:CompareValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                            <div style="margin: 0 auto;">
                                                <asp:Button TabIndex="6" ValidationGroup="aaa" ID="btnCreate" Text="Change" runat="server" CssClass="GreenyPushButton"
                                                    OnClick="btnCreate_Click"></asp:Button>
                                                <asp:Button TabIndex="7" ID="Button1" Text="Cancel" runat="server" CssClass="GreenyPushButton"
                                                    OnClientClick="clearValidationErrors();" OnClick="btnCancel_Click"></asp:Button>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </asp:Panel>
                    </div>
                    <asp:Panel CssClass="raised" ID="Pnlmsg" runat="server" Visible="false" Width="600px"
                        Style="max-height: 500px; min-height: 50px">
                        <asp:Label runat="server" ID="lblHint" Text="" Visible="false"> </asp:Label>
                        <div style="height: 50px; text-align: center; width: 100%" class="boxfooter">
                            <asp:Label ID="lblHeading" runat="server" Text=""> </asp:Label>
                        </div>
                        <div id="Div1" style="text-align: center; min-height: 50px; overflow: auto; width: 100%;">
                            <br />
                            <br />
                            <asp:Label ID="lblContent" runat="server"> </asp:Label>
                            <br />
                            <br />
                            <asp:PlaceHolder ID="DynamicControlsHolder" runat="server"></asp:PlaceHolder>
                        </div>
                        <div class="boxheader">
                            <asp:Button Style="margin: 0 auto" CssClass="GreenyPushButton" ID="Button2" runat="server"
                                Text="Ok" OnClientClick="clearValidationErrors();" OnClick="btnCancel_Click" />
                        </div>
                    </asp:Panel>
                    <ajax:ModalPopupExtender ID="ModalPopupExtender1" BackgroundCssClass="modalBackground"
                        TargetControlID="btnCreate" PopupControlID="Pnlmsg" runat="server">
                    </ajax:ModalPopupExtender>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
