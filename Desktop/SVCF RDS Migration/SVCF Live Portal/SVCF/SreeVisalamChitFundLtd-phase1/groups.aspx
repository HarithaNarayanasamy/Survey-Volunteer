<%@ Page Culture="en-GB" Title="Sree Visalam Chit Fund Limited" Language="C#" EnableViewState="true"
    MasterPageFile="~/Branch.Master" AutoEventWireup="true" CodeBehind="groups.aspx.cs"
    Inherits="SreeVisalamChitFundLtd_phase1.groups" %>

<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<%@ Register Assembly="TimePicker" Namespace="MKB.TimePicker" TagPrefix="MKB" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Head1" ContentPlaceHolderID="cphHead" runat="server">
    <title>Group Addition</title>
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
<asp:Content ID="Content1" ContentPlaceHolderID="cphMainContent" runat="server">
    <link href="select2-3.4.2/select2.css" rel="stylesheet" type="text/css" />
    <script src="select2-3.4.2/select2.js" type="text/javascript"></script>
    <ajax:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </ajax:ToolkitScriptManager>
    <div class="row">
        <div class="twelve columns">
            <div class="box_c">
                <div class="box_c_heading  box_actions">
                    <p>
                        Group Addition</p>
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
                                            <asp:TextBox MaxLength="13" ID="txtgroup_no" TabIndex="2" placeholder="Group Name"
                                                ToolTip="Ex. CLV-24" CssClass="input-text ttip_r" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtgroup_no" ErrorMessage="Enter Group Name"
                                                ValidationGroup="group" Display="Dynamic"></asp:RequiredFieldValidator>
                                        </td>
                                        <td style="vertical-align: top; padding-top: 6px; padding-right: 5px;">
                                            <asp:Label ID="lblno_of_months" runat="server" Text="No.of Installment"></asp:Label>
                                        </td>
                                        <td style="vertical-align: top;">
                                            <asp:TextBox MaxLength="2" ID="txtno_of_months" ToolTip="Ex. 12" TabIndex="15" placeholder="No.of Installment"
                                                runat="server" CssClass="input-text ttip_r sp_number"></asp:TextBox>
                                            <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidator1" ValidationGroup="group"
                                                runat="server" ControlToValidate="txtno_of_months" ErrorMessage="Enter Number of Months"></asp:RequiredFieldValidator>
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
                                            <asp:Label ID="lblauction_dt" runat="server" Text="1st & 2nd Auction Date"></asp:Label>
                                        </td>
                                        <td style="vertical-align: top;">
                                            <div>
                                                <asp:TextBox Width="120px" placeholder="1st Auction Date" CssClass="ttip_r maskdate"
                                                    ID="txtauction_dt1" TabIndex="16" runat="server"></asp:TextBox>
                                                <asp:TextBox Width="120px" placeholder="2nd Auction Date" CssClass="ttip_r maskdate"
                                                    Style="float: right;" ID="txtauction_dt" TabIndex="17" runat="server"></asp:TextBox>
                                            </div>
                                            <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidator3" ValidationGroup="group"
                                                runat="server" ControlToValidate="txtauction_dt1" ErrorMessage="Enter 1st Auction Date"></asp:RequiredFieldValidator>
                                            <asp:CompareValidator ID="CompareValidator5" ValidationGroup="group" runat="server" Display="Dynamic" ErrorMessage="Enter Valid Date" ControlToValidate="txtauction_dt1" Operator="DataTypeCheck" Type="Date"></asp:CompareValidator>
                                            <asp:RequiredFieldValidator Style="float: right;" Display="Dynamic" ID="RequiredFieldValidator24"
                                                ValidationGroup="group" runat="server" ControlToValidate="txtauction_dt" ErrorMessage="Enter 2nd Auction Date"></asp:RequiredFieldValidator>
                                            <asp:CompareValidator Style="float: right;padding-right:25px;" ID="CompareValidator1" ValidationGroup="group" runat="server" Display="Dynamic" ErrorMessage="Enter Valid Date" ControlToValidate="txtauction_dt" Operator="DataTypeCheck" Type="Date"></asp:CompareValidator>
                                            <asp:CompareValidator Style="float: left;" ValidationGroup="group" ID="CompareValidator12" ControlToValidate="txtauction_dt" ControlToCompare="txtauction_dt1" Display="Dynamic" runat="server" Operator="GreaterThanEqual" Type="Date"></asp:CompareValidator>
                                        </td>
                                    </tr>
                                    <tr>
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
                                        <td style="vertical-align: top; padding-top: 6px; padding-right: 5px;">
                                            <asp:Label ID="lblauction_time" runat="server" Text="Auction Time"></asp:Label>
                                        </td>
                                        <td style="vertical-align: top;">
                                            <dx:ASPxTimeEdit TabIndex="18" ID="TimeSelAuction" runat="server" EditFormat="Custom"
                                                EditFormatString="hh:mm tt" DisplayFormatString="hh:mm tt">
                                                <ValidationSettings ErrorDisplayMode="None" />
                                            </dx:ASPxTimeEdit>
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
                                            <asp:Label ID="lbl_chit_agree_no" runat="server" Text="Chit Agreement Number"></asp:Label>
                                        </td>
                                        <td style="vertical-align: top;">
                                        <asp:TextBox ID="txtchit_agree_no" TabIndex="6" runat="server" ToolTip="Ex. 2195"
                                                placeholder="Chit Agreement Number" CssClass="input-text ttip_r"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtchit_agree_no"
                                                ValidationGroup="group" ErrorMessage="Enter Chit Agreement Number" Display="Dynamic"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="vertical-align: top; padding-top: 6px; padding-right: 5px;">
                                            
                                        </td>
                                        <td style="vertical-align: top; padding-right: 5px;">
                                            
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
                                            <asp:Label ID="lbldt_of_agree" runat="server" Text="Date of Agreement"></asp:Label>
                                        </td>
                                        <td style="vertical-align: top; padding-right: 5px;">
                                            <asp:TextBox ID="txtdt_of_agree" TabIndex="8" CssClass="input-text ttip_r maskdate"
                                                placeholder="Date of Agreement" runat="server">  
                                            </asp:TextBox>
                                            <asp:RequiredFieldValidator ValidationGroup="group" Display="Dynamic" ID="RequiredFieldValidator11"
                                                runat="server" ControlToValidate="txtdt_of_agree" ErrorMessage="Enter Date of Agreement"></asp:RequiredFieldValidator>
                                            <asp:CompareValidator ID="CompareValidator6" ValidationGroup="group" runat="server" Display="Dynamic" ErrorMessage="Enter Valid Date" ControlToValidate="txtdt_of_agree" Operator="DataTypeCheck" Type="Date"></asp:CompareValidator>
                                        </td>
                                        <td style="vertical-align: top; padding-top: 6px; padding-right: 5px;">
                                            <asp:Label ID="Label3" runat="server" Text="CS Bank"></asp:Label>
                                        </td>
                                        <td style="vertical-align: top;">
                                            <asp:TextBox ID="txtSDP_bank" TabIndex="22" runat="server" ToolTip="Ex. State Bank of India"
                                                placeholder="CS Bank" CssClass="input-text ttip_r"></asp:TextBox>
                                            <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidator12" ValidationGroup="group"
                                                runat="server" ControlToValidate="txtSDP_bank" ErrorMessage="Enter CS Bank"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="vertical-align: top; padding-top: 6px; padding-right: 5px;">
                                            <asp:Label ID="lblchit_value" runat="server" Text="Chit Value"></asp:Label>
                                        </td>
                                        <td style="vertical-align: top; padding-right: 5px;">
                                            <asp:DropDownList Width="240px" ID="txtchit_value" TabIndex="9" runat="server" CssClass="chzn-select"></asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator13" Display="Dynamic" runat="server" ErrorMessage="Select Chit Value"
                                                ValidationGroup="group" ControlToValidate="txtchit_value" InitialValue="--select--"></asp:RequiredFieldValidator>
                                        </td>
                                        <td style="vertical-align: top; padding-top: 6px; padding-right: 5px;">
                                            <asp:Label ID="Label4" runat="server" Text="CS Bank Place"></asp:Label>
                                        </td>
                                        <td style="vertical-align: top;">
                                            <asp:TextBox ID="txtsdpbankPlace" placeholder="CS Bank Place" ToolTip="Ex. Pollachi"
                                                TabIndex="23" runat="server" CssClass="input-text ttip_r"></asp:TextBox>
                                            <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidator14" ValidationGroup="group"
                                                runat="server" ControlToValidate="txtsdpbankPlace" ErrorMessage="Enter CS Bank Place"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="vertical-align: top; padding-top: 6px; padding-right: 5px;">
                                            <asp:Label ID="lblchit_period" runat="server" Text="Chit Period(in months)"></asp:Label>
                                        </td>
                                        <td style="vertical-align: top; padding-right: 5px;">
                                            <asp:TextBox ID="txtchit_period" TabIndex="10" MaxLength="2" placeholder="Chit Period(in months)"
                                                ToolTip="Ex. 50" runat="server" CssClass="input-text ttip_r sp_number"></asp:TextBox>
                                            <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidator15" ValidationGroup="group"
                                                runat="server" ControlToValidate="txtchit_period" ErrorMessage="Enter CS Bank Place"></asp:RequiredFieldValidator>
                                        </td>
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
                                    </tr>
                                    <tr>
                                        <td style="vertical-align: top; padding-top: 6px; padding-right: 5px;">
                                            <asp:Label ID="lblchit_category" runat="server" Text="Chit Category"></asp:Label>
                                        </td>
                                        <td style="vertical-align: top; padding-right: 5px;">
                                            <asp:DropDownList Width="240px" TabIndex="11" CssClass="chzn-select" ID="cmbchit_category"
                                                AutoPostBack="false" runat="server">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator Display="Dynamic" runat="server" ErrorMessage="Select Chit Category"
                                                ValidationGroup="group" ControlToValidate="cmbchit_category" InitialValue="--select--"></asp:RequiredFieldValidator>
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
                                            <asp:Label ID="lblchit_start_dt" runat="server" Text="Chit Commencement Date"></asp:Label>
                                        </td>
                                        <td style="vertical-align: top; padding-right: 6px;">
                                            <asp:TextBox ID="txtchit_start_dt" TabIndex="12" placeholder="Chit Commencement Date" runat="server"
                                                CssClass="input-text ttip_r maskdate"></asp:TextBox>
                                            <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidator18" ValidationGroup="group"
                                                runat="server" ControlToValidate="txtchit_start_dt" ErrorMessage="Enter Chit Start Date"></asp:RequiredFieldValidator>
                                            <asp:CompareValidator ID="CompareValidator9" ValidationGroup="group" runat="server" Display="Dynamic" ErrorMessage="Enter Valid Date" ControlToValidate="txtchit_start_dt" Operator="DataTypeCheck" Type="Date"></asp:CompareValidator>
                                        </td>
                                        <td style="vertical-align: top; padding-top: 6px; padding-right: 5px;">
                                            <asp:Label ID="Label7" runat="server" Text="FD Interest"></asp:Label>
                                        </td>
                                        <td style="vertical-align: top;">
                                            <asp:TextBox ID="txtInterest" TabIndex="26" placeholder="FD Interest" ToolTip="Ex. 50"
                                                runat="server" CssClass="input-text ttip_r sp_float"></asp:TextBox>
                                            <asp:RequiredFieldValidator ValidationGroup="group" Display="Dynamic" ID="RequiredFieldValidator19"
                                                runat="server" ControlToValidate="txtInterest" ErrorMessage="Enter FD Interest"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="vertical-align: top; padding-top: 6px; padding-right: 5px;">
                                            <asp:Label ID="lblchit_end_dt" runat="server" Text="Chit Termination Date"></asp:Label>
                                        </td>
                                        <td style="vertical-align: top; padding-right: 5px;">
                                            <asp:TextBox ID="txtchit_end_dt" TabIndex="13" placeholder="Chit Termination Date" runat="server"
                                                CssClass="input-text ttip_r maskdate"></asp:TextBox>
                                            <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidator20" ValidationGroup="group"
                                                runat="server" ControlToValidate="txtchit_end_dt" ErrorMessage="Enter Chit End Date"></asp:RequiredFieldValidator>
                                            <asp:CompareValidator ID="CompareValidator10" ValidationGroup="group" runat="server" Display="Dynamic" ErrorMessage="Enter Valid Date" ControlToValidate="txtchit_end_dt" Operator="DataTypeCheck" Type="Date"></asp:CompareValidator>
                                            <asp:CompareValidator ValidationGroup="group" ID="CompareValidator11" ControlToValidate="txtchit_end_dt" ControlToCompare="txtchit_start_dt" Display="Dynamic" runat="server" Operator="GreaterThanEqual" Type="Date"></asp:CompareValidator>
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
                                            <asp:Label ID="lblno_of_members" runat="server" Text="Number of Members"></asp:Label>
                                        </td>
                                        <td style="vertical-align: top; padding-right: 5px;">
                                            <asp:TextBox ToolTip="Ex. 50" MaxLength="2" placeholder="Number of Members" ID="txtno_of_members"
                                                TabIndex="14" runat="server" CssClass="input-text ttip_r sp_number"></asp:TextBox>
                                            <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidator22" ValidationGroup="group"
                                                runat="server" ControlToValidate="txtno_of_members" ErrorMessage="Enter Number of Members"></asp:RequiredFieldValidator>
                                        </td>
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
