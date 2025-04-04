<%@ Page Culture="en-GB" Title="SVCF Admin Panel" Language="C#" MasterPageFile="~/Branch.Master"
    AutoEventWireup="true" CodeBehind="BranchDetails.aspx.cs" Inherits="SreeVisalamChitFundLtd_phase1.BranchDetails" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<asp:Content ID="Head1" ContentPlaceHolderID="cphHead" runat="server">
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMainContent" runat="server">
   
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
                        Branch Addition</p>
                </div>
                <div class="box_c_content">
                    <div class="row">
                        <asp:Panel DefaultButton="btnAdd" runat="server">
                            <div style="width: 100%;">
                                <table cellspacing="3" style="margin: 0 auto;">
                                    <tr>
                                        <td style="vertical-align: top; padding-top: 6px; padding-right: 6px;">
                                            <asp:Label ID="Label1" runat="server" Text="Branch Code"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox MaxLength="11" ID="txtBranchCode" ToolTip="Ex. 19 ( Digits )" runat="server" TabIndex="1"
                                                placeholder="Branch Code" CssClass="input-text sp_number ttip_r"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="BranchValid"
                                                Display="Dynamic" runat="server" ControlToValidate="txtBranchCode" ErrorMessage="Enter Branch Code"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="vertical-align: top; padding-top: 6px; padding-right: 6px;">
                                            <asp:Label ID="Label2" runat="server" Text="Branch Name"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox MaxLength="100" ToolTip="Ex. Coimbatore-1" ID="txtBranchName" runat="server" TabIndex="2"
                                                PlaceHolder="Branch Name" CssClass="input-text ttip_r"></asp:TextBox>
                                            <asp:RequiredFieldValidator ValidationGroup="BranchValid" Display="Dynamic" runat="server"
                                                ControlToValidate="txtBranchName" ErrorMessage="Enter Branch Name"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="vertical-align: top; padding-top: 6px; padding-right: 6px;">
                                            <asp:Label ID="Label7" runat="server" Text="Branch Head"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TxtBranchHead" runat="server" TabIndex="3" placeholder="Branch Head" CssClass="input-text ttip_r"></asp:TextBox>
                                            <asp:RequiredFieldValidator ValidationGroup="BranchValid" Display="Dynamic" runat="server"
                                                ControlToValidate="TxtBranchHead" ErrorMessage="Enter Branch Head"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="vertical-align: top; padding-top: 45px; padding-right: 6px;">
                                            <asp:Label ID="Label3" runat="server" Text="Branch Address"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtBranchAddress" TabIndex="4" runat="server" ToolTip="Ex. 196/23, Thiruvenkatasamy Road (West),<br/> R.S.Puram,<br/> Coimbatore - 641002"
                                                CssClass="input-text ttip_r" placeholder="Branch Address" TextMode="MultiLine"></asp:TextBox>
                                            <asp:RequiredFieldValidator ValidationGroup="BranchValid" Display="Dynamic" runat="server"
                                                ControlToValidate="txtBranchAddress" ErrorMessage="Enter Branch Address"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="vertical-align: top; padding-top: 6px; padding-right: 6px;">
                                            <asp:Label ID="Label4" runat="server" Text="Branch Ph.No"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtPhoneNumber" TabIndex="5" ToolTip="Ex. 0422-7575757 or 9876543210" runat="server"
                                                placeholder="Phone Number" CssClass="input-text ttip_r"></asp:TextBox>
                                            <asp:RequiredFieldValidator ValidationGroup="BranchValid" runat="server" ControlToValidate="txtPhoneNumber"
                                                Display="Dynamic" ErrorMessage="Enter Phone No."></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ControlToValidate="txtPhoneNumber" ValidationExpression="(\+91)?\d{2,}(-)?\d{6,}" ValidationGroup="BranchValid" 
                                                Display="Dynamic" ID="RegularExpressionValidator1" runat="server" ErrorMessage="Enter valid Phone No."></asp:RegularExpressionValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="vertical-align: top; padding-top: 6px; padding-right: 6px;">
                                            <asp:Label ID="Label5" runat="server" Text="Branch E-mail ID"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ToolTip="Ex. unknown@unknown.com" TabIndex="6" ID="txtEmail" PlaceHolder="E-mail ID" 
                                                runat="server" CssClass="input-text ttip_r "></asp:TextBox>
                                                                      </td>
                                    </tr>
                                    <tr>
                                        <td style="vertical-align: top; padding-top: 6px; padding-right: 6px;">
                                            <asp:Label ID="Label6" runat="server" Text="Date of Commencement"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox placeholder="Date of Commencement" ToolTip="Ex. 31/12/2013 (dd/mm/yyyy)" mask="mask_date" TabIndex="7" CssClass="input-text ttip_r maskdate"
                                                ID="TxtPicker" runat="server">
                                            </asp:TextBox>
                                            <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidator3"
                                                ValidationGroup="BranchValid" runat="server" ControlToValidate="TxtPicker" ErrorMessage="Enter Date"></asp:RequiredFieldValidator>
                                            <asp:CompareValidator Display="Dynamic" Type="Date" Operator="DataTypeCheck" ValidationGroup="BranchValid" 
                                                ControlToValidate="TxtPicker" ID="CompareValidator1" runat="server" ErrorMessage="Enter Valid Date"></asp:CompareValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td style="padding-top: 10px;">
                                            <asp:Button TabIndex="8" ID="btnAdd" Text="Add" ValidationGroup="BranchValid" runat="server" CssClass="GreenyPushButton"
                                                Style="margin: 0px auto;" OnClick="btnAdd_Click"></asp:Button>
                                            <asp:Button TabIndex="9" ID="btnCancel" Style="margin: 0px auto;" Text="Cancel" OnClick="btnCancel_Click"
                                                OnClientClick="clearValidationErrors();" CausesValidation="false" runat="server"
                                                CssClass="GreenyPushButton"></asp:Button>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </asp:Panel>
                    </div>
                </div>
                <ajax:ModalPopupExtender ID="ModalPopupExtender2" BackgroundCssClass="modalBackground"
                    TargetControlID="ShowPopup1" PopupControlID="Pnlmsg" runat="server">
                </ajax:ModalPopupExtender>
                <asp:LinkButton ID="ShowPopup1" runat="server"></asp:LinkButton>
                <asp:Panel CssClass="raised" ID="Pnlmsg" runat="server" Visible="false" Width="350px"
                    Style="min-height: 100px">
                    <asp:Label runat="server" ID="lblHint" Text="" Visible="false"> </asp:Label>
                    <div style="top: 0px; height: 50px; text-align: center; padding: auto 0; width: 100%"
                        class="boxheader">
                        <asp:Label runat="server" ID="lblT" Text=""> </asp:Label>
                    </div>
                    <div style="min-height: 100px; text-align: center;">
                        <br />
                        <br />
                        <asp:Label runat="server" ID="lblContent" Text=""> </asp:Label>
                        <br />
                        <br />
                    </div>
                    <div class="boxheader">
                        <div style="margin: 0 auto;">
                            <asp:Button Style="margin: 0 auto" CssClass="GreenyPushButton" OnClick="btnclose" CausesValidation="false"
                                ID="BtnOK" runat="server" Text="Ok"></asp:Button>
                        </div>
                    </div>
                </asp:Panel>
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
