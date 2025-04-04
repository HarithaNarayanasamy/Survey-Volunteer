<%@ Page Title="" Language="C#" MasterPageFile="~/Branch.Master" AutoEventWireup="true" CodeBehind="NewgroupRevert.aspx.cs" Inherits="SreeVisalamChitFundLtd_phase1.NewgroupRevert" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <style type="text/css">
        div[id*="chzn"]
        {
            min-width: 300px;
        }
        .tabbed td
        {
            padding: 3px;
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
                <div class="box_c_heading cf box_actions" style="margin: 0 important;">
                    <div class="box_c_ico">
                        <img src="pertho_admin_v1.3/img/ico/open/arrow-round.png" alt="" /></div>
                    <p>
                        Revert Auction
                    </p>
                </div>
                <div class="box_c_content" >
                    <div style="width: 100%">
                        <div style="margin: 0 important;">
                            <div class="formRow">
                                <asp:Label ID="Label2" runat="server" Text="GroupNo"></asp:Label>
                                <asp:DropDownList ID="ddlGroupNo" ValidationGroup="add" runat="server" CssClass="chzn-select"
                                    AutoPostBack="true" OnSelectedIndexChanged="ddlGroupNo_SelectedIndexChanged">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*"
                                    ValidationGroup="add" ControlToValidate="ddlGroupNo" InitialValue="--select--"></asp:RequiredFieldValidator>
                            </div>
                            <div class="formRow">
                                <asp:Label ID="lblAction" runat="server" Text="Chit"></asp:Label>
                                <asp:DropDownList ID="ddlChit" runat="server" ValidationGroup="add" CssClass="chzn-select"
                                     AutoPostBack="true">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" InitialValue="--select--"
                                    ErrorMessage="*" ValidationGroup="add" Height="30px" Display="Dynamic"
                                    ControlToValidate="ddlChit"></asp:RequiredFieldValidator>
                            </div>
                            
                            <div class="formRow">
                                <asp:Button ID="btnAdd" runat="server" CssClass="GreenyPushButton" ValidationGroup="add"
                                    Text="Revert" Style="margin: 0px auto;" OnClick="btnrevert_Click" />
                              
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
                            <div style="min-height: 100px;text-align:center;">
                                <br />
                                <br />
                                <asp:Label runat="server" ID="lblContent" Text=""> </asp:Label>
                                <br />
                                <br />
                            </div>
                            <div class="boxheader">
                                <div style="margin: 0 auto;">
                                    <asp:Button Style="margin: 0 auto" CssClass="GreenyPushButton" OnClick="btnclose"
                                        ID="BtnOK" CausesValidation="false" runat="server" Text="Ok" />
                                </div>
                            </div>
                        </asp:Panel>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
