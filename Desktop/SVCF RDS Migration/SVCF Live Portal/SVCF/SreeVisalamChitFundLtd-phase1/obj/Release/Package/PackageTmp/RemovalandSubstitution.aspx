<%@ Page Title="SVCF Admin Panel" Culture="en-GB" Language="C#" MasterPageFile="~/Branch.Master"
    AutoEventWireup="true" CodeBehind="RemovalandSubstitution.aspx.cs" Inherits="SreeVisalamChitFundLtd_phase1.RemovalandSubstitution" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMainContent" runat="server">
    <link href="Styles/aspxtable.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="Scripts/jquery.fixedheader.js"></script>
    <style type="text/css">
        .chzn-results
        {
            text-align: center;
        }
        #ctl00_cphMainContent_ddlChitGroupNo_chzn .chzn-drop .chzn-search input[type="text"]
        {
            height: 15px;
        }
        /*#ctl00_cphMainContent_ddlOldMember_chzn .chzn-drop .chzn-search input[type="text"]
        {
            height: 15px;
        }*/
        #ctl00_cphMainContent_ddlNewMember_chzn .chzn-drop .chzn-search input[type="text"]
        {
            height: 15px;
        }
        #ctl00_cphMainContent_ddlMonyCollector_Name_chzn .chzn-drop .chzn-search input[type="text"]
        {
            height: 15px;
        }
        table["id*="GridGuardians"] td
        {
            vertical-align: top !important;
        }
        div[id*="chzn"]
        {
            min-width: 150px;
        }
        .tabbed td
        {
            padding: 3px;
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
    <ajax:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </ajax:ToolkitScriptManager>
    <div class="row">
        <div class="twelve columns">
            <div class="box_c">
                <div class="box_c_heading  box_actions">
                    <p>
                        Member Removal</p>
                </div>
                <div class="box_c_content">
                    <div class="row">
                        <div id="all" runat="server">
                            <asp:Panel runat="server" DefaultButton="btnTransfer">
                                <asp:UpdatePanel runat="server">
                                    <ContentTemplate>
                                        <table class="Trans" cellspacing="3px" style="margin: 0px auto; padding-left: 2px;">
                                            <tr>
                                                <td style="vertical-align: top; padding-right: 5px; padding-top: 6px;">
                                                    <asp:Label ID="Label1" runat="server" Text="Chit Group"></asp:Label>
                                                </td>
                                                <td style="vertical-align: top; padding-right: 5px;">
                                                    <asp:DropDownList onchange="clearValidationErrors();" TabIndex="1" Width="240px"
                                                        ID="ddlChitGroupNo" CssClass="chzn-select" AutoPostBack="true" runat="server"
                                                        OnSelectedIndexChanged="ddlChitGroupNo_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                    <asp:CompareValidator Operator="NotEqual" runat="server" ControlToValidate="ddlChitGroupNo"
                                                        Display="Dynamic" ForeColor="Red" ErrorMessage="Select Chit Group" ValueToCompare="0"
                                                        ValidationGroup="ras"></asp:CompareValidator>
                                                </td>
                                                <td style="vertical-align: top; padding-right: 5px; padding-top: 6px;">
                                                    <asp:Label ID="Label2" runat="server" Text="Current Draw Number"></asp:Label>
                                                </td>
                                                <td style="vertical-align: top;">
                                                    <asp:TextBox ID="txtCurrent_DrawNo" ReadOnly="true" runat="server" ToolTip="Ex. 23"
                                                        placeholder="Current Draw Number" TabIndex="2" CssClass="input-text sp_number"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ValidationGroup="ras" ID="RequiredFieldValidator11" runat="server"
                                                        ControlToValidate="txtCurrent_DrawNo" Display="Dynamic" ForeColor="Red" ErrorMessage="Enter Draw Number"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                             <tr>
                                            <td >
                                            </td>
                                            <td>
                                            </td>
                                            <td style="vertical-align: top; padding-top: 6px !important;">
                                                <asp:Label ID="Label17" runat="server" Text="Branch Name"></asp:Label>
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
                                                <td style="vertical-align: top; padding-right: 5px; padding-top: 6px;">
                                                    <asp:Label ID="Label3" runat="server" Text="Member Name"></asp:Label>
                                                </td>
                                                <td style="vertical-align: top; padding-right: 5px;">
                                                   <%-- <asp:DropDownList TabIndex="3" Width="240px" ID="ddlOldMember" CssClass="chzn-select" 
                                                        onchange="clearValidationErrors();" runat="server">
                                                    </asp:DropDownList>--%>
                                                    <asp:DropDownList ID="DropDownList1" runat="server"  CssClass="chzn-select" TabIndex="3"  Width="240px" ></asp:DropDownList>
                                                    <asp:CompareValidator Operator="NotEqual" ID="CompareValidator1" runat="server" ControlToValidate="DropDownList1"
                                                        Display="Dynamic" ForeColor="Red" ErrorMessage="Select Old Member Name" ValueToCompare="0"
                                                        ValidationGroup="ras"></asp:CompareValidator>
                                                </td>
                                                <td style="vertical-align: top; padding-right: 5px; padding-top: 6px;">
                                                    <asp:Label ID="Label4" runat="server" Text="New Member Name"></asp:Label>
                                                </td>
                                                <td style="vertical-align: top;">
                                                    <asp:DropDownList onchange="clearValidationErrors();" TabIndex="4" Width="240px"
                                                        ID="ddlNewMember" CssClass="chzn-select" runat="server">
                                                    </asp:DropDownList>
                                                    <asp:CompareValidator Operator="NotEqual" ID="CompareValidator2" runat="server" ControlToValidate="ddlNewMember"
                                                        Display="Dynamic" ForeColor="Red" ErrorMessage="Select New Member Name" ValueToCompare="--Select--"
                                                        ValidationGroup="ras"></asp:CompareValidator>
                                                    <br>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="vertical-align: top; padding-right: 5px; padding-top: 6px;">
                                                    <asp:Label ID="Label13" runat="server" Text="Commision"></asp:Label>
                                                </td>
                                                <td style="vertical-align: top; padding-right: 5px;">
                                                    <asp:TextBox runat="server" placeholder="Commision" TabIndex="5" CssClass="input-text sp_float"
                                                        ID="txtCommision"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ValidationGroup="ras" ID="RequiredFieldValidator5" ControlToValidate="txtCommision"
                                                        runat="server" Display="Dynamic" ForeColor="Red" ErrorMessage="Enter Removal Amount"></asp:RequiredFieldValidator>
                                                </td>
                                                <td style="vertical-align: top; padding-right: 5px; padding-top: 6px;">
                                                    <asp:Label ID="Label15" runat="server" Text="Removal Amount"></asp:Label>
                                                </td>
                                                <td style="vertical-align: top;">
                                                    <asp:TextBox runat="server" TabIndex="6" CssClass="input-text sp_float" placeholder="Removal Amount"
                                                        ID="txtRemovalAmount"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ValidationGroup="ras" ID="RequiredFieldValidator7" ControlToValidate="txtRemovalAmount"
                                                        runat="server" Display="Dynamic" ForeColor="Red" ErrorMessage="Enter Removal Amount"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="vertical-align: top; padding-right: 5px; padding-top: 6px;">
                                                    <asp:Label ID="Label5" runat="server" Text="Date of Removal"></asp:Label>
                                                </td>
                                                <td style="vertical-align: top;">
                                                    <asp:TextBox TabIndex="7" placeholder="Date of Removal" ID="txtDateofRemoval" CssClass="input-text maskdate"
                                                        runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator EnableClientScript="false" ValidationGroup="ras" ID="RequiredFieldValidator6" ControlToValidate="txtDateofRemoval"
                                                        runat="server" Display="Dynamic" ForeColor="Red" ErrorMessage="Enter Date for Removal"></asp:RequiredFieldValidator>
                                                    <asp:CompareValidator EnableClientScript="false" ID="CompareValidator6" ValidationGroup="ras" runat="server"
                                                        ErrorMessage="Enter Valid Date" Display="Dynamic" ForeColor="Red" ControlToValidate="txtDateofRemoval"
                                                        Operator="DataTypeCheck" Type="Date"></asp:CompareValidator>
                                                </td>
                                                <td style="vertical-align: top; padding-right: 5px; padding-top: 6px;">
                                                    <asp:Label ID="Label7" runat="server" Text="Estimated Draw Number"></asp:Label>
                                                </td>
                                                <td style="vertical-align: top;">
                                                    <asp:TextBox TabIndex="8" placeholder="Estimated Draw Number" ToolTip="Ex. 25" ID="txtEstimated_DrawNo_for_Auction"
                                                        CssClass="input-text" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ValidationGroup="ras" ID="RequiredFieldValidator3" ControlToValidate="txtEstimated_DrawNo_for_Auction"
                                                        runat="server" Display="Dynamic" ForeColor="Red" ErrorMessage="Enter Estimated Draw Number"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="vertical-align: top; padding-right: 5px; padding-top: 6px;">
                                                    <asp:Label ID="Label6" runat="server" Text="First Notice Date"></asp:Label>
                                                </td>
                                                <td style="vertical-align: top; padding-right: 5px;">
                                                    <asp:TextBox TabIndex="9" ID="txt1stNoticeDate" placeholder="First Notice Date" CssClass="input-text maskdate"
                                                        runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator EnableClientScript="false" ValidationGroup="ras" ID="RequiredFieldValidator1" ControlToValidate="txt1stNoticeDate"
                                                        runat="server" Display="Dynamic" ForeColor="Red" ErrorMessage="Enter First Notice Date"></asp:RequiredFieldValidator>
                                                    <asp:CompareValidator EnableClientScript="false" ValidationGroup="ras" ID="CompareValidator3" runat="server"
                                                        ErrorMessage="Enter Valid Date" Display="Dynamic" ForeColor="Red" ControlToValidate="txt1stNoticeDate"
                                                        Operator="DataTypeCheck" Type="Date"></asp:CompareValidator>
                                                </td>
                                                <td style="vertical-align: top; padding-right: 5px; padding-top: 6px;">
                                                    <asp:Label ID="Label9" runat="server" Text="Second Notice Date"></asp:Label>
                                                </td>
                                                <td style="vertical-align: top;">
                                                    <asp:TextBox TabIndex="10" ID="txt2ndNoticeDate" placeholder="Second Notice Date"
                                                        CssClass="input-text maskdate" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ValidationGroup="ras" ID="RequiredFieldValidator2" ControlToValidate="txt2ndNoticeDate"
                                                        runat="server" Display="Dynamic" EnableClientScript="false" ForeColor="Red" ErrorMessage="Enter Second Notice Date"></asp:RequiredFieldValidator>
                                                    <asp:CompareValidator ValidationGroup="ras" ID="CompareValidator4" runat="server"
                                                        ErrorMessage="Enter Valid Date" EnableClientScript="false" Display="Dynamic" ForeColor="Red" ControlToValidate="txt2ndNoticeDate"
                                                        Operator="DataTypeCheck" Type="Date"></asp:CompareValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="vertical-align: top; padding-right: 5px; padding-top: 6px;">
                                                    <asp:Label ID="Label12" runat="server" Text="Kasar"></asp:Label>
                                                </td>
                                                <td style="vertical-align: top; padding-right: 5px;">
                                                    <asp:TextBox TabIndex="11" ID="txtKasar" placeholder="Kasar Amount" CssClass="input-text sp_number"
                                                        runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ValidationGroup="ras" ID="RequiredFieldValidator8" ControlToValidate="txtKasar"
                                                        runat="server" Display="Dynamic" ForeColor="Red" ErrorMessage="Enter Kasar Amount"></asp:RequiredFieldValidator>
                                                </td>
                                                <td style="vertical-align: top; padding-right: 5px; padding-top: 6px;">
                                                    <asp:Label ID="Label16" runat="server" Text="Income(per Month)"></asp:Label>
                                                </td>
                                                <td style="vertical-align: top;">
                                                    <asp:TextBox TabIndex="12" ID="txtMonthlyIncome" placeholder="Monthly Income" CssClass="input-text sp_number"
                                                        runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ValidationGroup="ras" ID="RequiredFieldValidator9" ControlToValidate="txtMonthlyIncome"
                                                        runat="server" Display="Dynamic" ForeColor="Red" ErrorMessage="Enter Monthly Income"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="vertical-align: top; padding-right: 5px; padding-top: 40px;">
                                                    <asp:Label ID="Label8" runat="server" Text="Estimated Surety Detail"></asp:Label>
                                                </td>
                                                <td style="vertical-align: top; padding-right: 5px;">
                                                    <asp:TextBox ToolTip="-" placeholder="Estimated Surety Detail" TabIndex="13" ID="txtEstimated_Surety_Details"
                                                        CssClass="input-text" TextMode="MultiLine" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ValidationGroup="ras" ControlToValidate="txtEstimated_Surety_Details"
                                                        runat="server" Display="Dynamic" ForeColor="Red" ErrorMessage="Enter Estimated Surety Details"></asp:RequiredFieldValidator>
                                                </td>
                                                <td style="vertical-align: top; padding-right: 5px; padding-top: 40px;">
                                                    <asp:Label ID="Label14" runat="server" Text="Reason for Removal"></asp:Label>
                                                </td>
                                                <td style="vertical-align: top; padding-right: 5px;">
                                                    <asp:TextBox TabIndex="14" placeholder="Reason for Removal" ToolTip="Instalment<br> Amount not paid"
                                                        ID="txtReason" CssClass="input-text" TextMode="MultiLine" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ValidationGroup="ras" ID="RequiredFieldValidator10" ControlToValidate="txtReason"
                                                        runat="server" Display="Dynamic" ForeColor="Red" ErrorMessage="Enter Reason for Removal"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="vertical-align: top; padding-right: 5px; padding-top: 6px;">
                                                    <asp:Label ID="Label11" runat="server" Text="Money Collector Name"></asp:Label>
                                                </td>
                                                <td style="vertical-align: top; padding-right: 5px;">
                                                    <asp:DropDownList onchange="clearValidationErrors();" TabIndex="15" Width="240px"
                                                        ID="ddlMonyCollector_Name" CssClass="chzn-select" runat="server">
                                                    </asp:DropDownList>
                                                    <asp:CompareValidator Operator="NotEqual" ValidationGroup="ras" ControlToValidate="ddlMonyCollector_Name"
                                                        ValueToCompare="0" runat="server" Display="Dynamic" ForeColor="Red" ErrorMessage="Select Money Collector Name"></asp:CompareValidator>
                                                </td>
                                                <td style="vertical-align: top; padding-right: 5px; padding-top: 6px;">
                                                    <asp:Label ID="Label10" runat="server" Text="Profession Business"></asp:Label>
                                                </td>
                                                <td style="vertical-align: top;">
                                                    <asp:TextBox TabIndex="16" ID="txtProfession" placeholder="Profession Business" CssClass="input-text"
                                                        runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ValidationGroup="ras" ID="RequiredFieldValidator12" ControlToValidate="txtProfession"
                                                        runat="server" Display="Dynamic" ForeColor="Red" ErrorMessage="Enter Profession Business"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                        </table>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </asp:Panel>
                            <br />
                            <div style="text-align: center;">
                                <asp:Button TabIndex="22" CausesValidation="true" ID="btnTransfer" runat="server"
                                    CssClass="GreenyPushButton" ValidationGroup="ras" Style="margin: 0px auto;" Text="Substitute"
                                    OnClick="btnTransfer_Click"></asp:Button>
                                <asp:Button TabIndex="23" CausesValidation="false" ID="btnClose" runat="server" CssClass="GreenyPushButton"
                                    Style="margin: 0px auto;" Text="Cancel" OnClientClick="clearValidationErrors();">
                                </asp:Button>
                            </div>
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
                        <asp:Label runat="server" ID="lblHint" Text="" Visible="false"> </asp:Label>
                        <div class=" box_c_heading">
                            <asp:Label ID="lblT" runat="server" Text=""> </asp:Label>
                        </div>
                        <div style="min-height: 20px; overflow: hidden; max-width: 880px; padding: 20px;">
                            <div style="min-height: 20px; overflow: auto; max-width: 840px;">
                                <br />
                                <asp:Label ID="lblContent" runat="server" Text="" Style="text-align: justify; vertical-align: middle;
                                    margin-left: 10px"> </asp:Label>
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
