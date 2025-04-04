<%@ Page Culture="en-GB" Title="SVCF Admin Panel" Language="C#" MasterPageFile="~/Branch.Master"
    AutoEventWireup="true" CodeBehind="CompanySubstitutedChitPayment.aspx.cs" Inherits="SreeVisalamChitFundLtd_phase1.CompanySubstitutedChitPayment" %>



<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
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
        #ctl00_cphMainContent_ddlGroupNumber_chzn .chzn-drop .chzn-search input[type="text"]
        {
            height: 15px;
        }
        #ctl00_cphMainContent_ddlMemberName_chzn .chzn-drop .chzn-search input[type="text"]
        {
            height: 15px;
        }
        #ctl00_cphMainContent_ddlGuarantor_chzn .chzn-drop .chzn-search input[type="text"]
        {
            height: 15px;
        }
        #ctl00_cphMainContent_ddlBankName_chzn .chzn-drop .chzn-search input[type="text"]
        {
            height: 15px;
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
                <div class="box_c_heading  box_actions">
                    <p>
                        Company Substituted Chit Payment</p>
                </div>
                <div class="box_c_content">
                    <asp:Panel ID="Panel1" runat="server" DefaultButton="btnPayment">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <div class="row">
                                    <div style="width: 100%">
                                        <table style="display: table; margin: 0px auto;">
                                            <tr>
                                                <td>
                                                    <asp:Label ID="labelSeries" runat="server" Text="Receipt Series"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox TabIndex="1" placeholder="Payment" ToolTip="Ex. KR" CssClass="input-text"
                                                        ID="txtSeries" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ErrorMessage="Invalid Receipt Series"
                                                        ControlToValidate="txtSeries" Display="Dynamic" ValidationGroup="pa" runat="server"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ControlToValidate="txtSeries" ValidationExpression="^[A-Z]+$"
                                                        ID="RegularExpressionValidator1" Display="Dynamic" runat="server" ErrorMessage="Enter Capital Letters"></asp:RegularExpressionValidator>
                                                </td>
                                                <td>
                                                    <asp:Label ID="labelPaymentNumber" runat="server" Text="Payment Number"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ToolTip="Ex. 1000" placeholder="Payment Number" TabIndex="2" runat="server"
                                                        ID="txtPaymentNumber" CssClass="input-text sp_number">
                                                    </asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator13" ErrorMessage="Enter Payment Number"
                                                        ControlToValidate="txtPaymentNumber" Display="Dynamic" ValidationGroup="pa" runat="server"></asp:RequiredFieldValidator>
                                                    <asp:CustomValidator ID="CustomValidator2" runat="server" ValidationGroup="pa" ControlToValidate="txtPaymentNumber"
                                                        OnServerValidate="txtPaymentNumber_Validate" Display="Dynamic"></asp:CustomValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label3" runat="server" Text="Chit Group"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList  onchange="clearValidationErrors();" TabIndex="3" Width="240px" CssClass="chzn-select" ID="ddlGroupNumber"
                                                        runat="server" AutoPostBack="true" OnSelectedIndexChanged="load_ddlGroupNumber">
                                                    </asp:DropDownList>
                                                    <asp:CompareValidator ID="CompareValidator11" ValueToCompare="0" ValidationGroup="pa"
                                                        ControlToValidate="ddlGroupNumber" Display="Dynamic" Operator="NotEqual" runat="server"
                                                        ErrorMessage="Select Chit Group"></asp:CompareValidator>
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label4" runat="server" Text="Member Name"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList onchange="clearValidationErrors();"  TabIndex="4" AutoPostBack="true" OnSelectedIndexChanged="load_ddlMemberName"
                                                        Width="240px" CssClass="chzn-select" ID="ddlMemberName" runat="server">
                                                    </asp:DropDownList>
                                                    <asp:CompareValidator ID="CompareValidator10" ValueToCompare="--Select--" ControlToValidate="ddlMemberName"
                                                        ValidationGroup="pa" Display="Dynamic" Operator="NotEqual" runat="server" ErrorMessage="Select Member Name"></asp:CompareValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="label11" runat="server" Text="Draw Number"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox TabIndex="5" placeholder="Draw Number" ToolTip="Ex. 50" CssClass="input-text"
                                                        ID="txtDrawNumber" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" ErrorMessage="Enter Draw Number"
                                                        ControlToValidate="txtDrawNumber" ValidationGroup="pa" Display="Dynamic" runat="server"></asp:RequiredFieldValidator>
                                                </td>
                                                <td>
                                                    <asp:Label ID="label8" runat="server" Text="Payment On"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtDate" TabIndex="6" CssClass="input-text maskdate" placeholder="Payment Date"
                                                        runat="server">
                                                    </asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ErrorMessage="Enter Payment Date"
                                                        ControlToValidate="txtDate" ValidationGroup="pa" Display="Dynamic" runat="server"></asp:RequiredFieldValidator>
                                                    <asp:CompareValidator ForeColor="Red" Type="Date" ID="CompareValidator1" ValidationGroup="pa"
                                                        ControlToValidate="txtDate" Display="Dynamic" Operator="DataTypeCheck" runat="server"
                                                        ErrorMessage="Incorrect!!!"></asp:CompareValidator>
                                                    <asp:RangeValidator ID="rvDate" runat="server"  Type="Date" ControlToValidate="txtDate" ValidationGroup="pa"
                                                        ForeColor="Red" Display="Dynamic" ErrorMessage="*"></asp:RangeValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="label13" runat="server" Text="Approved On"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtPaymentonDate" TabIndex="7" CssClass="input-text maskdate"
                                                        placeholder="Approved Date" runat="server">
                                                    </asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ErrorMessage="Enter Payment Date"
                                                        ControlToValidate="txtPaymentonDate" ValidationGroup="pa" Display="Dynamic" runat="server"></asp:RequiredFieldValidator>
                                                    <asp:CompareValidator ForeColor="Red" Type="Date" ID="CompareValidator2" ValidationGroup="pa"
                                                        ControlToValidate="txtPaymentonDate" Display="Dynamic" Operator="DataTypeCheck"
                                                        runat="server" ErrorMessage="Incorrect!!!"></asp:CompareValidator>
                                              
                                                </td>
                                                <td style="vertical-align: top; padding-right: 5px; padding-top: 6px;">
                                                    <asp:Label ID="label17" runat="server" Text="Applied On"></asp:Label>
                                                </td>
                                                <td style="vertical-align: top;">
                                                    <asp:TextBox TabIndex="8" ID="txtApplyedOn" CssClass="input-text maskdate"
                                                        placeholder="Approved Date" runat="server">
                                                    </asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ErrorMessage="Enter Applied Date"
                                                        ControlToValidate="txtApplyedOn" ValidationGroup="pa" Display="Dynamic" runat="server"></asp:RequiredFieldValidator>
                                                    <asp:CompareValidator ForeColor="Red" Type="Date" ID="CompareValidator3" ValidationGroup="pa"
                                                        ControlToValidate="txtApplyedOn" Display="Dynamic" Operator="DataTypeCheck" runat="server"
                                                        ErrorMessage="Incorrect!!!"></asp:CompareValidator>                                                
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="vertical-align: top; padding-right: 5px; padding-top: 46px;">
                                                    <asp:Label ID="label18" runat="server" Text="Description"></asp:Label>
                                                </td>
                                                <td style="vertical-align: top; padding-right: 5px;">
                                                    <asp:TextBox TabIndex="9" CssClass="input-text" placeholder="Description"
                                                        ToolTip="-" TextMode="MultiLine" ID="txtDescription" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" ErrorMessage="Enter Description"
                                                        ControlToValidate="txtDescription" ValidationGroup="pa" Display="Dynamic" runat="server"></asp:RequiredFieldValidator>
                                                </td>
                                                <td style="vertical-align: top; padding-right: 5px; padding-top: 46px;">
                                                    <asp:Label ID="label19" runat="server" Text="Admin Sanction Number"></asp:Label>
                                                </td>
                                                <td style="vertical-align: top; padding-top: 42px;">
                                                    <asp:TextBox TabIndex="10" MaxLength="4" CssClass="input-text sp_number" placeholder="Admin Sanction Number"
                                                        ToolTip="Ex. 1000" ID="txtAOSanctiion" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator12" ErrorMessage="Enter Admin Sanction Number"
                                                        ControlToValidate="txtAOSanctiion" ValidationGroup="pa" Display="Dynamic" runat="server"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                        </table>
                                        <br />
                                        <table style="display: table; margin: 0px auto;">
                                            <tr>
                                                <td align="center" colspan="4">
                                                    <div style="font-size: 16px;">
                                                        <b>SUMMARY FORM PAYMENT DETAILS </b>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" colspan="4">
                                                    <div style="font-size: 14px; color: Black;">
                                                        <b>CREDIT TRANSACTION</b></div>
                                                </td>
                                            </tr>
                                            <tr style="background-color: Gray; height: 25px">
                                                <td align="center">
                                                    <asp:Label ID="labelTitle" runat="server" Text="Head"></asp:Label>
                                                </td>
                                                <td align="center">
                                                    <asp:Label ID="labelCredit" runat="server" Text="Amount"></asp:Label>
                                                </td>
                                                <td align="center">
                                                    <asp:Label ID="label2" runat="server" Text="Head"></asp:Label>
                                                </td>
                                                <td align="center">
                                                    <asp:Label ID="label6" runat="server" Text="Amount"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="padding-right: 5px; padding-top: 20px;">
                                                    <asp:Label ID="label1" runat="server" Text="Foreman Substituted Chit Prized Amount"></asp:Label>
                                                </td>
                                                <td style="padding-right: 5px;">
                                                    <asp:TextBox TabIndex="11" ToolTip="Ex. 1000.00" placeholder="Foreman Substituted Chit Prized Amount"
                                                        CssClass="input-text sp_float" ID="txtBankAmount" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ControlToValidate="txtBankAmount" Display="Dynamic" ValidationGroup="pa"
                                                        ID="RequiredFieldValidator9" runat="server" ErrorMessage="Enter Foreman Substituted Chit Prized Amount"></asp:RequiredFieldValidator>
                                                </td>
                                                <td style="padding-right: 5px;">
                                                    <asp:Label ID="label12" runat="server" Text="Commission"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox TabIndex="12" ToolTip="Ex. 1000.00" placeholder="Commission" CssClass="input-text sp_float"
                                                        ID="txtCommision" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ControlToValidate="txtCommision" Display="Dynamic" ValidationGroup="pa"
                                                        ID="RequiredFieldValidator7" runat="server" ErrorMessage="Enter Commission"></asp:RequiredFieldValidator>
                                                    <asp:Label Visible="false" ID="lbTool" runat="server"></asp:Label>
                                                    <asp:Label ID="lbFuture" runat="server" Visible="false" Text="0.00"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="4" align="center">
                                                    <div style="font-size: 14px; color: Black;">
                                                        <b>DEBIT TRANSACTION</b></div>
                                                </td>
                                            </tr>
                                            <tr style="background-color: Gray; height: 25px">
                                                <td align="center">
                                                    <asp:Label ID="label5" runat="server" Text="Head"></asp:Label>
                                                </td>
                                                <td align="center">
                                                    <asp:Label ID="label7" runat="server" Text="Amount"></asp:Label>
                                                </td>
                                                <td>
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="padding-top: 20px; padding-right: 5px;">
                                                    <asp:Label ID="LabelPrizedAmount" runat="server"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox TabIndex="13" ReadOnly="true" ToolTip="Ex. 1000.00" CssClass="input-text sp_float"
                                                        ID="txtDebitAmount" runat="server"></asp:TextBox>
                                                </td>
                                                <td>
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </asp:Panel>
                    <div id="Div1" runat="server" style="margin: 0px auto; display: table;">
                        <asp:Button TabIndex="14" CausesValidation="true" ID="btnPayment" runat="server"
                            ValidationGroup="pa" CssClass="GreenyPushButton" OnClick="load_Payment" Text="Payment">
                        </asp:Button>
                        <asp:Button TabIndex="15" CausesValidation="false" ID="Button1" runat="server" CssClass="GreenyPushButton"
                            OnClientClick="clearValidationErrors();" OnClick="load_cancel" Text="Cancel">
                        </asp:Button>
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
                                <asp:Button Style="margin: 0 auto" CssClass="GreenyPushButton" OnClick="btn_ok"
                                    ID="BtnOK" CausesValidation="false" OnClientClick="clearValidationErrors();"
                                    runat="server" Text="Ok" />
                                <asp:Button Visible="false" Style="margin: 0 auto" CssClass="GreenyPushButton" OnClick="load_cancel"
                                    ID="Button2" CausesValidation="false" OnClientClick="clearValidationErrors();"
                                    runat="server" Text="Ok" />
                                <asp:Button Visible="false" Style="margin: 0 auto" CssClass="GreenyPushButton" OnClick="load_cancel"
                                    ID="Button3" CausesValidation="false" OnClientClick="clearValidationErrors();"
                                    runat="server" Text="Cancel" />
                                <asp:Button Visible="false" Style="margin: 0 auto" CssClass="GreenyPushButton" OnClick="load_hidePanel"
                                    ID="btnHide" CausesValidation="false" OnClientClick="clearValidationErrors();"
                                    runat="server" Text="Ok" />
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


            <%--  Button disable coding--%>     
            function DisableButton() {            
                document.getElementById("<%=BtnOK.ClientID %>").disabled = true;
            }
        window.onbeforeunload = DisableButton;
        <%--  Button disable coding--%>

    </script>
</asp:Content>
