<%@ Page Title="" Language="C#" MasterPageFile="~/Branch.Master" AutoEventWireup="true" CodeBehind="FdrandPsoDetails.aspx.cs" Inherits="SreeVisalamChitFundLtd_phase1.FdrandPsoDetails" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<%@ Register Assembly="TimePicker" Namespace="MKB.TimePicker" TagPrefix="MKB" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <title>FDR AND PSO Details Add</title>
    <style type="text/css">
        .dxic
        {
            width: 100%;
            padding-left: 0px !important;
            padding-right: 0px !important;
            padding-top: 0px !important;
            padding-bottom: 0px !important;
        }
        #ctl00_cphMainContent_TimeSelAuction_I
        {
            height: 28px !important;
            width: 240px !important;
        }
        .dxeSBC
        {
            vertical-align: top;
        }
        #ctl00_cphMainContent_txtchit_value_chzn .chzn-results
        {
            height: 165px !important;
        }
        .chzn-results
        {
            text-align:center;
        }
        #ctl00_cphMainContent_txtchit_value_chzn .chzn-drop .chzn-search input[type="text"]
        {
            height:15px;
        }
        #ctl00_cphMainContent_cmbchit_category_chzn .chzn-drop .chzn-search input[type="text"]
        {
            height:15px;
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
<link href="select2-3.4.2/select2.css" rel="stylesheet" type="text/css" />
    <script src="select2-3.4.2/select2.js" type="text/javascript"></script>
    <ajax:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </ajax:ToolkitScriptManager>
    <div class="row">
        <div class="twelve columns">
            <div class="box_c">
                <div class="box_c_heading  box_actions">
                    <p>
                        Add FDR AND PSO Details </p>
                </div>
                <div class="box_c_content">
                    <div class="row">
                        <asp:Panel runat="server" DefaultButton="btnaddGroup">
                            <div style="width: 100%">
                                <table id="tblContent" style="margin: 0 auto;" cellspacing="10px">
                                    <tr>
                                        <td style="vertical-align: top; padding-top: 6px; padding-right: 5px;">
                                            <asp:Label ID="lblgroupno" runat="server" Text="Group Name"></asp:Label>
                                        </td>
                                        <td style="vertical-align: top; padding-right: 5px;">
                                            <%--<asp:TextBox MaxLength="13" ID="txtgroup_no" TabIndex="2" placeholder="Group Name"
                                                ToolTip="Ex. CLV-24" CssClass="input-text ttip_r" runat="server" Visible="false"></asp:TextBox>--%>
                                              <asp:DropDownList Width="240px" TabIndex="11" CssClass="chzn-select" ID="ddlChitGroup"
                                                AutoPostBack="false" runat="server">
                                            </asp:DropDownList>
                                           <%-- <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlChitGroup" ErrorMessage="Enter Group Name"
                                                ValidationGroup="group" Display="Dynamic"></asp:RequiredFieldValidator>--%>
                                        </td>
                                        
                                    </tr>
                                  
                                    <tr>
                                        <td style="vertical-align: top; padding-top: 6px; padding-right: 5px;">
                                            <asp:Label ID="lblpso_order_no" runat="server" Text="PSO Order Number"></asp:Label>
                                        </td>
                                        <td style="vertical-align: top; padding-right: 5px;">
                                            <asp:TextBox ID="txtpso_order_no" TabIndex="3" ToolTip="Ex. 2270/12" placeholder="PSO Order Number"
                                                runat="server" CssClass="input-text ttip_r"> 
                                            </asp:TextBox>
                                            <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidator2" ValidationGroup="group"
                                                runat="server" ControlToValidate="txtpso_order_no" ErrorMessage="Enter PSO Order Number"></asp:RequiredFieldValidator>
                                        </td>

                                         <td style="vertical-align: top; padding-top: 6px; padding-right: 5px;">
                                            <asp:Label ID="lblpso_order_date" runat="server" Text="PSO Order Date"></asp:Label>
                                        </td>
                                        <td style="vertical-align: top; padding-right: 5px;">
                                            <asp:TextBox ID="txtpso_order_date" TabIndex="4" placeholder="PSO Order Date" runat="server"
                                                CssClass="input-text ttip_r maskdate"></asp:TextBox>
                                            <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidator4" ValidationGroup="group"
                                                runat="server" ControlToValidate="txtpso_order_date" ErrorMessage="Enter PSO Order Date"></asp:RequiredFieldValidator>
                                            <asp:CompareValidator ID="CompareValidator2" ValidationGroup="group" runat="server" Display="Dynamic" ErrorMessage="Enter Valid Date" ControlToValidate="txtpso_order_date" Operator="DataTypeCheck" Type="Date"></asp:CompareValidator>
                                        </td>
                                       
                                    </tr>
                                  
                                    <tr>
                                        <td style="vertical-align: top; padding-top: 6px; padding-right: 5px;">
                                            <asp:Label ID="lblpso_dr_office" runat="server" Text="PSO D.R Office"></asp:Label>
                                        </td>
                                        <td style="vertical-align: top; padding-right: 5px;">
                                            <asp:TextBox ID="txtpso_dr_office" TabIndex="5" runat="server" ToolTip="Ex. COIMBATORE"
                                                placeholder="PSO D.R Office" CssClass="input-text ttip_r"></asp:TextBox>
                                            <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidator5" ValidationGroup="group"
                                                runat="server" ControlToValidate="txtpso_dr_office" ErrorMessage="Enter PSO D.R Office"></asp:RequiredFieldValidator>
                                        </td>
                                        <td style="vertical-align: top; padding-top: 6px; padding-right: 5px;">
                                            <asp:Label ID="Label2" runat="server" Text="CS_FDR Number"></asp:Label>
                                        </td>
                                        <td style="vertical-align: top;">
                                            <asp:TextBox ValidationGroup="group" placeholder="CS_FDR Number" ToolTip="Ex. 1965925"
                                                ID="txtSDPFDR" TabIndex="21" CssClass="input-text ttip_r" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidator10" ValidationGroup="group"
                                                runat="server" ControlToValidate="txtSDPFDR" ErrorMessage="Enter CS_FDR Number"></asp:RequiredFieldValidator>
                                        </td>
                                        
                                    </tr>
                                   
                                    <tr>
                                       
                                        <td style="vertical-align: top; padding-top: 6px; padding-right: 5px;">
                                            <asp:Label ID="Label3" runat="server" Text="CS Bank"></asp:Label>
                                        </td>
                                        <td style="vertical-align: top;">
                                            <asp:TextBox ID="txtSDP_bank" TabIndex="22" runat="server" ToolTip="Ex. State Bank of India"
                                                placeholder="CS Bank" CssClass="input-text ttip_r"></asp:TextBox>
                                            <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidator12" ValidationGroup="group"
                                                runat="server" ControlToValidate="txtSDP_bank" ErrorMessage="Enter CS Bank"></asp:RequiredFieldValidator>
                                        </td>
                                         <td style="vertical-align: top; padding-top: 6px; padding-right: 5px;">
                                            <asp:Label ID="Label1" runat="server" Text="CS Bank Place"></asp:Label>
                                        </td>
                                        <td style="vertical-align: top;">
                                            <asp:TextBox ID="txtsdpbankPlace" placeholder="CS Bank Place" ToolTip="Ex. Pollachi"
                                                TabIndex="23" runat="server" CssClass="input-text ttip_r"></asp:TextBox>
                                            <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidator1" ValidationGroup="group"
                                                runat="server" ControlToValidate="txtsdpbankPlace" ErrorMessage="Enter CS Bank Place"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                  
                                    <tr>
                                       
                                        <td style="vertical-align: top; padding-top: 6px; padding-right: 5px;">
                                            <asp:Label ID="Label5" runat="server" Text="FD Commencement"></asp:Label>
                                        </td>
                                        <td style="vertical-align: top;">
                                            <asp:TextBox ID="txtsdpComm" TabIndex="24" placeholder="CS Commencement" runat="server"
                                                CssClass="input-text ttip_r maskdate"></asp:TextBox>
                                            <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidator16" ValidationGroup="group"
                                                runat="server" ControlToValidate="txtsdpComm" ErrorMessage="Enter FD Commencement"></asp:RequiredFieldValidator>
                                            <asp:CompareValidator ID="CompareValidator7" ValidationGroup="group" runat="server" Display="Dynamic" ErrorMessage="Enter Valid Date" ControlToValidate="txtsdpComm" Operator="DataTypeCheck" Type="Date"></asp:CompareValidator>
                                        </td>
                                        <td style="vertical-align: top; padding-top: 6px; padding-right: 5px;">
                                            <asp:Label ID="Label6" runat="server" Text="FD Maturity"></asp:Label>
                                        </td>
                                        <td style="vertical-align: top;">
                                            <asp:TextBox ID="txtsdpMatur" TabIndex="25" runat="server" placeholder="CS Maturity"
                                                CssClass="input-text ttip_r maskdate"></asp:TextBox>
                                            <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidator17" ValidationGroup="group"
                                                runat="server" ControlToValidate="txtsdpMatur" ErrorMessage="Enter FD Maturity"></asp:RequiredFieldValidator>
                                            <asp:CompareValidator ID="CompareValidator8" ValidationGroup="group" runat="server" Display="Dynamic" ErrorMessage="Enter Valid Date" ControlToValidate="txtsdpMatur" Operator="DataTypeCheck" Type="Date"></asp:CompareValidator>
                                        </td>
                                    </tr>
                                   
                                    <tr>
                                        <td style="vertical-align: top; padding-top: 6px; padding-right: 5px;">
                                            <asp:Label ID="Label7" runat="server" Text="FD Interest"></asp:Label>
                                        </td>
                                        <td style="vertical-align: top;">
                                            <asp:TextBox ID="txtInterest" TabIndex="26" placeholder="FD Interest" ToolTip="Ex. 50"
                                                runat="server" CssClass="input-text ttip_r sp_float"></asp:TextBox>
                                            <asp:RequiredFieldValidator ValidationGroup="group" Display="Dynamic" ID="RequiredFieldValidator19"
                                                runat="server" ControlToValidate="txtInterest" ErrorMessage="Enter FD Interest"></asp:RequiredFieldValidator>
                                        </td>
                                        <td style="vertical-align: top; padding-top: 6px; padding-right: 5px;">
                                            <asp:Label ID="Label8" runat="server" Text="FD Period(in Months)"></asp:Label>
                                        </td>
                                        <td style="vertical-align: top;">
                                            <asp:TextBox ID="txtPeriodMonths" MaxLength="2" placeholder="FD Period(in Months)" ToolTip="Ex. 50"
                                                TabIndex="27" runat="server" CssClass="input-text ttip_r sp_number"></asp:TextBox>
                                            <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidator21" ValidationGroup="group"
                                                runat="server" ControlToValidate="txtPeriodMonths" ErrorMessage="Enter FD Period(in Months)"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                  
                                    <tr>
                                        
                                        <td style="vertical-align: top; padding-top: 6px; padding-right: 5px;">
                                            <asp:Label ID="Label9" runat="server" Text="FD Amount"></asp:Label>
                                        </td>
                                        <td style="vertical-align: top;">
                                            <asp:TextBox ID="txtAmount" placeholder="FD Amount" ToolTip="Ex. 10000.00" TabIndex="28"
                                                CssClass="input-text ttip_r sp_float" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidator23" ValidationGroup="group"
                                                runat="server" ControlToValidate="txtAmount" ErrorMessage="Enter FD Amount"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4" align="center">
                                            <div style="margin: 0 auto;">
                                                <asp:Button ID="btnaddGroup" runat="server" CssClass="GreenyPushButton" ValidationGroup="group"
                                                    TabIndex="28" Text="Add Group" OnClick="btnaddGroup_click" />
                                                <asp:Button CausesValidation="false" ID="BtnCancel" runat="server" CssClass="GreenyPushButton"
                                                    TabIndex="29" Text="Cancel" OnClientClick="clearValidationErrors();" OnClick="BtnCancel_click" />
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </asp:Panel>
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
