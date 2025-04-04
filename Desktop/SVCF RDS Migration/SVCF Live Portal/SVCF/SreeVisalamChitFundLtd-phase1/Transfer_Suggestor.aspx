<%@ Page Title="SVCF Admin Panel" Language="C#" MasterPageFile="~/Branch.Master"
    AutoEventWireup="true" Culture="en-GB" CodeBehind="Transfer_Suggestor.aspx.cs"
    Inherits="SreeVisalamChitFundLtd_phase1.Transfer_Suggestor" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
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
        #ctl00_cphMainContent_ddlChitGroupNo_chzn .chzn-drop .chzn-search input[type="text"]
        {
            height: 15px;
        }
        #ctl00_cphMainContent_ddlOldMember_chzn .chzn-drop .chzn-search input[type="text"]
        {
            height: 15px;
        }
        #ctl00_cphMainContent_ddlNewMember_chzn .chzn-drop .chzn-search input[type="text"]
        {
            height: 15px;
        }
        #ctl00_cphMainContent_ddlMonyCollector_Name_chzn .chzn-drop .chzn-search input[type="text"]
        {
            height: 15px;
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMainContent" runat="server">
    
    <ajax:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </ajax:ToolkitScriptManager>
    <div class="row">
        <div class="twelve columns">
            <div class="box_c">
                <div class="box_c_heading  box_actions">
                    <p>
                        Transfer Suggestion</p>
                </div>
                <div class="box_c_content">
                    <div class="row">
                        <asp:Panel runat="server" DefaultButton="btnTransfer">
                            <asp:UpdatePanel runat="server">
                                <ContentTemplate>
                                    <table class="Trans" cellspacing="3px" style="margin: 0px auto;" width="90%">
                                        <tr>
                                            <td style="vertical-align: top; padding-top: 6px;">
                                                <asp:Label ID="Label1" runat="server" Text="Chit Group"></asp:Label>
                                                <asp:Label ID="lblCheck" runat="server"></asp:Label>
                                            </td>
                                            <td style="vertical-align: top;">
                                                <asp:DropDownList Width="240px" onchange="clearValidationErrors();" ID="ddlChitGroupNo"
                                                    CssClass="chzn-select" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlChitGroupNo_SelectedIndexChanged">
                                                </asp:DropDownList>
                                                <asp:CompareValidator ForeColor="Red" ID="CompareValidator1" Operator="NotEqual"
                                                    runat="server" ControlToValidate="ddlChitGroupNo" Display="Dynamic" ErrorMessage="Select Chit Group"
                                                    ValueToCompare="0" ValidationGroup="ts"></asp:CompareValidator>
                                            </td>
                                            <td style="vertical-align: top; padding-top: 6px;">
                                                <asp:Label ID="Label2" runat="server" Text="Draw Number"></asp:Label>
                                            </td>
                                            <td style="vertical-align: top;">
                                                <asp:TextBox placeholder="Draw Number" ID="txtCurrent_DrawNo" runat="server" CssClass="input-text sp_number"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txtCurrent_DrawNo"
                                                    Display="Dynamic" ValidationGroup="ts" ForeColor="Red" ErrorMessage="*"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                         <tr>
                                            <td >
                                            </td>
                                            <td>
                                            </td>
                                            <td style="vertical-align: top; padding-top: 6px !important;">
                                                <asp:Label ID="Label15" runat="server" Text="Branch Name"></asp:Label>
                                            </td>
                                            <td style="vertical-align: top;">
                                                <asp:DropDownList onchange="clearValidationErrors();" Width="240px" CssClass="chzn-select"
                                                    runat="server" ID="ddlBranchName" AutoPostBack="true"
                                                    TabIndex="2" OnSelectedIndexChanged="ddlBranchName_SelectedIndexChanged" >
                                                </asp:DropDownList>
                                                <%--<asp:CompareValidator ValidationGroup="sug" ID="CompareValidator6" runat="server"
                                                    Display="Dynamic" ControlToValidate="ddlBranchName" ErrorMessage="*" Operator="NotEqual"
                                                    ValueToCompare="0"></asp:CompareValidator>--%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="vertical-align: top; padding-top: 6px;">
                                                <asp:Label ID="Label3" runat="server" Text="Old Member"></asp:Label>
                                            </td>
                                            <td style="vertical-align: top;">
                                                <asp:DropDownList Width="240px" ID="ddlOldMember" CssClass="chzn-select" runat="server">
                                                </asp:DropDownList>
                                                <asp:CompareValidator ID="CompareValidator2" Operator="NotEqual" runat="server" ControlToValidate="ddlOldMember"
                                                    Display="Dynamic" ErrorMessage="Select Old Member" ValueToCompare="--Select--"
                                                    ForeColor="Red" ValidationGroup="ts"></asp:CompareValidator>
                                            </td>
                                            <td style="vertical-align: top; padding-top: 6px;">
                                                <asp:Label ID="Label4" runat="server" Text="New Member"></asp:Label>
                                            </td>
                                            <td style="vertical-align: top;">
                                                <asp:DropDownList Width="240px" ID="ddlNewMember" 
                                                    CssClass="chzn-select" runat="server">
                                                </asp:DropDownList>
                                                <asp:CompareValidator ID="CompareValidator3" Operator="NotEqual" runat="server" ControlToValidate="ddlNewMember"
                                                    Display="Dynamic" ErrorMessage="Select New Member" ForeColor="Red" ValueToCompare="--Select--"
                                                    ValidationGroup="ts"></asp:CompareValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="vertical-align: top; padding-top: 6px;">
                                                <asp:Label ID="Label5" runat="server" Text="Kasar Amount"></asp:Label>
                                            </td>
                                            <td style="vertical-align: top;">
                                                <asp:TextBox placeholder="Kasar Amount" ID="txtKasar"
                                                    CssClass="input-text sp_float" runat="server"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ForeColor="Red" ControlToValidate="txtKasar"
                                                    runat="server" Display="Dynamic" ValidationGroup="ts" ErrorMessage="*"></asp:RequiredFieldValidator>
                                            </td>
                                            <td style="vertical-align: top; padding-top: 6px;">
                                                <asp:Label ID="Label9" runat="server" Text="Transfer Amount"></asp:Label>
                                            </td>
                                            <td style="vertical-align: top;">
                                                <asp:TextBox placeholder="Transfer Amount" ID="txtTransfer"
                                                    CssClass="input-text sp_float" runat="server"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="txtTransfer"
                                                    runat="server" Display="Dynamic" ForeColor="Red" ValidationGroup="ts" ErrorMessage="*"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="vertical-align: top; padding-top: 6px;">
                                                <asp:Label ID="Label6" runat="server" Text="Income(per Month)"></asp:Label>
                                            </td>
                                            <td style="vertical-align: top;">
                                                <asp:TextBox placeholder="Income" ID="txtIncome"
                                                    CssClass="input-text sp_float" runat="server"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ForeColor="Red" ControlToValidate="txtIncome"
                                                    runat="server" Display="Dynamic" ValidationGroup="ts" ErrorMessage="*"></asp:RequiredFieldValidator>
                                            </td>
                                            <td style="vertical-align: top; padding-top: 6px;">
                                                <asp:Label ID="Label10" runat="server" Text="Commision"></asp:Label>
                                            </td>
                                            <td style="vertical-align: top;">
                                                <asp:TextBox placeholder="Commision" ID="txtCommision"
                                                    CssClass="input-text sp_float" runat="server"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ControlToValidate="txtCommision"
                                                    runat="server" Display="Dynamic" ForeColor="Red" ValidationGroup="ts" ErrorMessage="*"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="vertical-align: top; padding-top: 6px;">
                                                <asp:Label ID="Label7" runat="server" Text="Estimated Call Number"></asp:Label>
                                            </td>
                                            <td style="vertical-align: top;">
                                                <asp:TextBox placeholder="Est. Call No for Auction" ID="txtEstimated_DrawNo_for_Auction"
                                                    CssClass="input-text" runat="server"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtEstimated_DrawNo_for_Auction"
                                                    runat="server" Display="Dynamic" ForeColor="Red" ValidationGroup="ts" ErrorMessage="*"></asp:RequiredFieldValidator>
                                            </td>
                                            <td style="vertical-align: top; padding-top: 6px;">
                                                <asp:Label ID="Label12" runat="server" Text="Transfer Date"></asp:Label>
                                            </td>
                                            <td style="vertical-align: top;">
                                                <asp:TextBox placeholder="Transfer Date" ID="txtSuggestionDate" CssClass="input-text maskdate"
                                                    runat="server"></asp:TextBox>
                                                <asp:RequiredFieldValidator ValidationGroup="ts" ID="RequiredFieldValidator2" ControlToValidate="txtSuggestionDate"
                                                    runat="server" Display="Dynamic" EnableClientScript="false" ForeColor="Red" ErrorMessage="Enter Transfered Date"></asp:RequiredFieldValidator>
                                                <asp:CompareValidator ValidationGroup="ts" EnableClientScript="false" ID="CompareValidator6" runat="server"
                                                    ErrorMessage="Enter Valid Date" Display="Dynamic" ForeColor="Red" ControlToValidate="txtSuggestionDate"
                                                    Operator="DataTypeCheck" Type="Date"></asp:CompareValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="vertical-align: top; padding-top: 6px;">
                                                <asp:Label ID="Label14" runat="server" Text="Reason for Transfer"></asp:Label>
                                            </td>
                                            <td style="vertical-align: top;">
                                                <asp:TextBox placeholder="Reason" ID="txtReason" CssClass="input-text" TextMode="MultiLine"
                                                    runat="server"></asp:TextBox>
                                                <asp:RequiredFieldValidator ValidationGroup="ts" ID="RequiredFieldValidator10" ControlToValidate="txtReason"
                                                    runat="server" Display="Dynamic" ErrorMessage="*"></asp:RequiredFieldValidator>
                                            </td>
                                            <td style="vertical-align: top; padding-top: 6px;">
                                                <asp:Label ID="Label8" runat="server" Text="Est. Surety Details"></asp:Label>
                                            </td>
                                            <td style="vertical-align: top;">
                                                <asp:TextBox placeholder="Est. Surety Details" ID="txtEstimated_Surety_Details" CssClass="input-text"
                                                    TextMode="MultiLine" runat="server"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="txtEstimated_Surety_Details"
                                                    runat="server" ForeColor="Red" ValidationGroup="ts" Display="Dynamic" ErrorMessage="*"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="vertical-align: top; padding-top: 6px;">
                                                <asp:Label ID="Label11" runat="server" Text="Money Collector Name"></asp:Label>
                                            </td>
                                            <td style="vertical-align: top;">
                                                <asp:DropDownList Width="240px" ID="ddlMonyCollector_Name" CssClass="chzn-select"
                                                    runat="server">
                                                </asp:DropDownList>
                                                <asp:CompareValidator ID="CompareValidator4" Operator="NotEqual" runat="server" ControlToValidate="ddlMonyCollector_Name"
                                                    Display="Dynamic" ErrorMessage="Select Money Collector" ValueToCompare="0" ValidationGroup="ts"></asp:CompareValidator>
                                            </td>
                                            <td style="vertical-align: top; padding-top: 6px;">
                                                <asp:Label ID="Label13" runat="server" Text="Profession Bussiness"></asp:Label>
                                            </td>
                                            <td style="vertical-align: top;">
                                                <asp:TextBox placeholder="Profession Business" ID="txtProfession" CssClass="input-text"
                                                     runat="server"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" ControlToValidate="txtProfession"
                                                    runat="server" ForeColor="Red" ValidationGroup="ts" Display="Dynamic" ErrorMessage="*"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                    </table>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </asp:Panel>
                        <div runat="server" style="float: right;">
                            <br />
                            <br />
                            <asp:Button CausesValidation="true" ID="btnTransfer" runat="server" ValidationGroup="ts"
                                CssClass="GreenyPushButton" Style="margin: 0px auto;" Text="Transfer" OnClick="btnTransfer_Click">
                            </asp:Button>
                        </div>
                    </div>
                    <ajax:ModalPopupExtender ID="ModalPopupExtender1" TargetControlID="show" PopupControlID="Pnlmsg1"
                        BackgroundCssClass="modalBackground" runat="server">
                    </ajax:ModalPopupExtender>
                    <asp:LinkButton ID="show" runat="server"></asp:LinkButton>
                    <asp:Panel ID="Pnlmsg1" Style="background: #fff; border: 4px solid #ccc; border: 8px solid rgba(0, 0, 0, 0.5);
                        background-clip: padding-box; -webkit-border-radius: 10px; -moz-border-radius: 10px;
                        border-radius: 10px; min-width: 300px; max-width: 900px;" CssClass="raised" runat="server"
                        Visible="false">
                        <%-- <asp:Panel CssClass="raised" ID="Pnlmsg1" runat="server" Visible="false" Width="600px"
                    Style="max-height: 200px; min-height: 100px">--%>
                        <asp:Label runat="server" ID="lblHint" Text="" Visible="false"> </asp:Label>
                        <div class=" box_c_heading">
                            <asp:Label ID="lblT" runat="server" Text=""> </asp:Label>
                        </div>
                        <div style="min-height: 20px; overflow: hidden; max-width: 880px; padding: 20px;">
                            <div style="min-height: 20px; overflow: auto; max-width: 840px;">
                                <br />
                                <br />
                                <asp:Label ID="lblContent" runat="server" Text="" Style="text-align: justify; vertical-align: middle;
                                    margin-left: 10px"> </asp:Label>
                                <br />
                                <br />
                                <asp:PlaceHolder ID="DynamicControlsHolder" runat="server"></asp:PlaceHolder>
                            </div>
                        </div>
                        <div class=" box_c_heading">
                            <div style="float: right;">
                                <asp:Button CausesValidation="false" Width="100" Style="margin: 0 auto" CssClass="GreenyPushButton"
                                    ID="btnok" OnClick="btnok_Click" runat="server" Text="OK" />
                                <asp:Button CausesValidation="false" Width="100" Style="margin: 0 auto" CssClass="GreenyPushButton"
                                    ID="btncancel" OnClick="btncancel_Click" runat="server" Text="Cancel" />
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
