<%@ Page Title="SVCF - Admin Page" Culture="en-GB" Language="C#" MasterPageFile="~/Branch.Master" AutoEventWireup="true"
    CodeBehind="AdjustmentVoucher.aspx.cs" Inherits="SreeVisalamChitFundLtd_phase1.AdjustmentVoucher" %>



<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMainContent" runat="server">
    <ajax:ToolkitScriptManager ID="scm1" runat="server">
    </ajax:ToolkitScriptManager>
    <style type="text/css">
        td {
            vertical-align: middle;
            padding: 0px 2px 0px 2px;
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
    <asp:Panel CssClass="row" ID="Panel1" runat="server" DefaultButton="btnGenerate"
        class="content">
        <div class="twelve columns">
            <div class="box_c">
                <div class="box_c_heading  box_actions">
                    <p>
                        Voucher Details
                    </p>
                </div>

                <div class="box_c_content">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <div class="row">
                                <br />
                                <div style="display: inline; zoom: 1; text-align: center;">
                                    <div style="margin-left: auto; margin-right: auto; display: table;">
                                        <table cellspacing="4px" width="100%">
                                            <tr>
                                                <td style="vertical-align: top; padding-top: 6px;">
                                                    <asp:Label ID="lblDate" runat="server" Text="Date"></asp:Label>
                                                </td>
                                                <td style="vertical-align: top; padding-left: 6px; text-align: left;">
                                                    <asp:TextBox AutoPostBack="true" Width="100" TabIndex="1" ID="txtDate"
                                                        CssClass="input-text maskdate" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ForeColor="Red" ID="RequiredFieldValidator6" ErrorMessage="Rquired!!!"
                                                        ControlToValidate="txtDate" ValidationGroup="Generate" Display="Dynamic" runat="server"></asp:RequiredFieldValidator>
                                                    <asp:CompareValidator ForeColor="Red" Type="Date" ID="CompareValidator11" ValidationGroup="Generate"
                                                        ControlToValidate="txtDate" Display="Dynamic" Operator="DataTypeCheck" runat="server"
                                                        ErrorMessage="Incorrect!!!"></asp:CompareValidator>
                                                    <asp:RangeValidator ID="rvDate" EnableClientScript="false" runat="server"
                                                        Type="Date" ControlToValidate="txtDate" ValidationGroup="Generate" ForeColor="Red" Display="Dynamic"
                                                        ErrorMessage="*"></asp:RangeValidator>
                                                </td>
                                                <td style="vertical-align: top; padding-top: 6px;">
                                                    <asp:Label ID="lblSeries" runat="server" Text="Series"></asp:Label>
                                                </td>
                                                <td style="vertical-align: top; padding-left: 6px; text-align: left;">
                                                    <asp:TextBox Width="100" TabIndex="2" ValidationGroup="Generate" ID="txtSeries"
                                                        CssClass="input-text" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator Display="Dynamic" ValidationGroup="Generate" ControlToValidate="txtSeries"
                                                        ID="RequiredFieldValidator11" ErrorMessage="Rquired!!!" runat="server"> </asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator Display="Dynamic" ValidationGroup="Generate" ID="RegularExpressionValidator2"
                                                        runat="server" ControlToValidate="txtSeries" ErrorMessage="AlphaPets only!!!"
                                                        ValidationExpression="^[a-zA-Z]+$"></asp:RegularExpressionValidator>
                                                </td>
                                                <td style="vertical-align: top; padding-top: 6px;">
                                                    <asp:Label ID="Label1" runat="server" Text="Voucher Number"></asp:Label>
                                                </td>
                                                <td style="vertical-align: top; padding-left: 6px; text-align: left;">
                                                    <asp:TextBox Width="100" TabIndex="3" ValidationGroup="Generate" ID="txtVoucherNo"
                                                        CssClass="input-text sp_number" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator Display="Dynamic" ValidationGroup="Generate" ControlToValidate="txtVoucherNo"
                                                        ID="RequiredFieldValidator12" ErrorMessage="Rquired!!!" runat="server"> </asp:RequiredFieldValidator>
                                                </td>
                                                <td style="vertical-align: top; padding-top: 6px;">
                                                    <asp:Label ID="lblReceivedBy" runat="server" Text="By"></asp:Label>
                                                </td>
                                                <td style="vertical-align: top; padding-left: 6px; text-align: left;">
                                                    <asp:TextBox Width="150" ValidationGroup="Generate" CssClass="input-text" ID="txtReceivedBy"
                                                        runat="server" TabIndex="4"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ValidationGroup="Generate" ControlToValidate="txtReceivedBy"
                                                        ID="RequiredFieldValidator1" ErrorMessage="Rquired!!!" runat="server"> </asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                        </table>
                                        <br />
                                        <div class="box_c_heading cf">
                                            <div class="box_c_ico">
                                                <asp:Image runat="server" ID="img16List" ImageUrl="pertho_admin_v1.3/img/ico/icSw2/16-List.png" AlternateText="" />
                                            </div>
                                            <p>
                                                Credit Transactions
                                            </p>
                                        </div>
                                     
                                        <asp:GridView ID="GridGuardians" runat="server" AutoGenerateColumns="False" BorderStyle="None"
                                            CellSpacing="3" Font-Names="Verdana" Font-Size="9pt" ForeColor="#333333" GridLines="None"
                                            OnRowDataBound="GridGuardians_RowDataBound" ShowHeader="false" ShowFooter="true">
                                            <Columns>                                                  
                                                <asp:TemplateField HeaderText="Credit Branch">
                                                    <ItemTemplate>
                                                          <table style="border: none;">
                                                            <tr>
                                                                <td>
                                                                     <asp:Label ID="lblCRBranch" runat="server" Text='<%# Eval("Branch") %>' Visible="false" />
                                                                    <asp:Label ID="Label6" runat="server" Text="Credit Branch"></asp:Label>
                                                                    <asp:DropDownList CssClass="chzn-select" Style="width: 250px !important;" AutoPostBack="false"
                                                                        TabIndex="58" ID="ddlCRBranch" runat="server">
                                                                        <asp:ListItem Value="0">Select</asp:ListItem>
                                                                        <asp:ListItem Value="160">Pallathur 1</asp:ListItem>
                                                                        <asp:ListItem Value="162">Pallathur 3</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </td>
                                                                </tr>
                                                              </table>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="ChooseHead">
                                                    <ItemTemplate>
                                                        <table style="border: none;">
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="lblHeads" runat="server" Text='<%# Eval("Heads") %>' Visible="false" />
                                                                    <asp:Label ID="Label5" runat="server" Text="Heads"></asp:Label>
                                                                    <asp:DropDownList CssClass="chzn-select" Style="width: 350px !important;" AutoPostBack="true"
                                                                        TabIndex="58" ValidationGroup="GrpRow" ID="ddlHeads" OnSelectedIndexChanged="ddlBothGridHead_SelectedIndexChanged"
                                                                        runat="server">
                                                                    </asp:DropDownList>
                                                                    <asp:CompareValidator Display="Dynamic" ValidationGroup="GrpRow" Operator="NotEqual"
                                                                        ControlToValidate="ddlHeads" ID="CompareValidator2" ValueToCompare="--Select--"
                                                                        ErrorMessage="*" runat="server"> </asp:CompareValidator>
                                                                </td>
                                                                <td></td>
                                                            </tr>
                                                        </table>
                                                    </ItemTemplate>
                                                    <FooterStyle HorizontalAlign="Left" />
                                                    <FooterTemplate>
                                                        <table style="border: none;">
                                                            <tr>
                                                                <td>
                                                                    <asp:ImageButton OnClick="btnAdd_GridGuardians_RowCommand_click" TabIndex="-1" ID="imgBtnAdd"
                                                                        runat="server" CausesValidation="True" Height="24" Visible="true" ValidationGroup="GrpRow"
                                                                        ImageUrl="~/Styles/Image/Images/round_plus_16.png" ToolTip="Add New Transaction"
                                                                        Width="24" />
                                                                </td>
                                                                <td>
                                                                    <asp:ImageButton OnClick="btnRemove_GridGuardians_RowCommand_click" TabIndex="-1" OnClientClick="clearValidationErrors();"
                                                                        ID="imgBtnRemove" runat="server" CausesValidation="false" Height="24" ImageUrl="~/Styles/Image/Images/round_minus_16.png"
                                                                        ToolTip="Remove New Transaction" Visible="true" Width="24" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                        <br />
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Amount">
                                                    <ItemTemplate>
                                                        <table style="border: none;">
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="lblamt" runat="server" Text="Amount"></asp:Label>
                                                                    <asp:TextBox Width="100" TabIndex="59" Text='<%#Eval("Amount") %>' ValidationGroup="GrpRow"
                                                                        CssClass="twitterStyleTextbox  sp_currency" ID="txtAmount" runat="server">
                                                                    </asp:TextBox>
                                                                    <asp:RequiredFieldValidator Display="Dynamic" ValidationGroup="GrpRow" ControlToValidate="txtAmount"
                                                                        ID="rfvAmtx" ErrorMessage="*" runat="server"> </asp:RequiredFieldValidator>
                                                                    <asp:RegularExpressionValidator Display="Dynamic" ValidationGroup="GrpRow" ID="RegularExpressionValidator7"
                                                                        runat="server" ControlToValidate="txtAmount" ErrorMessage="Invalid Amount!!!"
                                                                        ValidationExpression="^\d+(\.\d{1,2})?$"></asp:RegularExpressionValidator>
                                                                </td>                                                                
                                                            </tr>
                                                        </table>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Description">
                                                    <ItemTemplate>
                                                        <table style="border: none;">                                                            
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="lblDesc" runat="server" Text="Description"></asp:Label>
                                                                    <asp:TextBox TextMode="MultiLine" Height="50" TabIndex="60" Text='<%#Eval("Description") %>'
                                                                        ValidationGroup="GrpRow" CssClass="input-text" ID="txtDescription" runat="server" Width="200px">
                                                                    </asp:TextBox>
                                                                </td>
                                                                <td></td>
                                                            </tr>
                                                        </table>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="ChequeNO">
                                                    <ItemTemplate>
                                                        <table style="border: none;">
                                                            <tr>
                                                                <td>
                                                                    <asp:Label Visible='<%# DecideVisibility(Eval("ChequeNO")) %>' ID="lblChequeNO" runat="server" Text="Cheque NO"></asp:Label>
                                                                    <br />
                                                                    <asp:TextBox Width="100" Visible='<%# DecideVisibility(Eval("ChequeNO")) %>' MaxLength="7" TabIndex="60" Text='<%#Eval("ChequeNO") %>'
                                                                        ValidationGroup="GrpRow" CssClass="input-text sp_number" ID="txtChequeNO"
                                                                        runat="server">
                                                                    </asp:TextBox>
                                                                    <asp:RequiredFieldValidator Display="Dynamic" Visible="false" ValidationGroup="GrpRow" ControlToValidate="txtChequeNO"
                                                                        ID="reqChequeNO" ErrorMessage="*" runat="server"> </asp:RequiredFieldValidator>
                                                                </td>
                                                                <td></td>
                                                            </tr>
                                                        </table>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                        <div class="box_c_heading cf">
                                            <div class="box_c_ico">
                                                <asp:Image runat="server" ID="img16List1" ImageUrl="pertho_admin_v1.3/img/ico/icSw2/16-List.png" AlternateText="" />
                                            </div>
                                            <p>
                                                Debit Transactions
                                            </p>
                                        </div>                                         
                                        <asp:GridView Style="width: auto;" ID="GridGuardiansDebit" runat="server" AutoGenerateColumns="False"
                                            BorderStyle="None" BorderWidth="1" CellPadding="4" Font-Names="Verdana" Font-Size="9pt"
                                            ForeColor="#333333" GridLines="None" OnRowDataBound="GridGuardiansDebit_RowDataBound"
                                            ShowHeader="false" ShowFooter="true">
                                            <Columns>                                            
                                                  <asp:TemplateField HeaderText="Debit Branch">
                                                    <ItemTemplate>
                                                          <table style="border: none;">
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="lblDRBranch" runat="server" Text='<%# Eval("Branch") %>' Visible="false" />
                                                                    <asp:Label ID="Label7" runat="server" Text="Credit Branch"></asp:Label>
                                                                    <asp:DropDownList CssClass="chzn-select" Style="width: 250px !important;" AutoPostBack="false"
                                                                        TabIndex="58" ID="ddlDBBranch" runat="server">
                                                                        <asp:ListItem Value="0">Select</asp:ListItem>
                                                                        <asp:ListItem Value="160">Pallathur 1</asp:ListItem>
                                                                        <asp:ListItem Value="162">Pallathur 3</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </td>
                                                                </tr>
                                                              </table>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="ChooseHead">
                                                    <ItemTemplate>
                                                        <table style="border: none;">
                                                            <tr>
                                                                <td style="height: 40px;">
                                                                    <asp:Label ID="lblDBHeads" runat="server" Text='<%# Eval("Heads") %>' Visible="false" />
                                                                    <asp:Label ID="Label8" runat="server" Text="Heads"></asp:Label>
                                                                    <asp:DropDownList CssClass="chzn-select" Style="width: 350px !important;" OnSelectedIndexChanged="ddlBothGridHead1_SelectedIndexChanged"
                                                                        TabIndex="58" ValidationGroup="GrpRowDebit" AutoPostBack="true" ID="ddlHeadsDebit"
                                                                        runat="server">
                                                                    </asp:DropDownList>
                                                                    <asp:CompareValidator Display="Dynamic" ValidationGroup="GrpRowDebit" Operator="NotEqual"
                                                                        ControlToValidate="ddlHeadsDebit" ID="CompareValidator2" ValueToCompare="--Select--"
                                                                        ErrorMessage="*" runat="server"> </asp:CompareValidator>
                                                                </td>
                                                                <td></td>
                                                            </tr>
                                                        </table>
                                                    </ItemTemplate>
                                                    <FooterStyle HorizontalAlign="Left" />
                                                    <FooterTemplate>
                                                        <table style="border: none;">
                                                            <tr>
                                                                <td>
                                                                    <asp:ImageButton OnClick="btnAdd_GridGuardiansDebit_RowCommand_click" TabIndex="-1"
                                                                        ID="imgBtnAddDebit" runat="server" Visible="true" CausesValidation="True" Height="24" ValidationGroup="GrpRowDebit"
                                                                        ImageUrl="~/Styles/Image/Images/round_plus_16.png" ToolTip="Add New Transaction"
                                                                        Width="24" />
                                                                </td>
                                                                <td>
                                                                    <asp:ImageButton OnClick="btnRemove_GridGuardiansDebit_RowCommand_click" TabIndex="-1" OnClientClick="clearValidationErrors();"
                                                                        ID="imgBtnRemoveDebit" runat="server" CausesValidation="false" Height="24" ImageUrl="~/Styles/Image/Images/round_minus_16.png"
                                                                        ToolTip="Remove New Transaction" Visible="true" Width="24" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                        <br />
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Amount">
                                                    <ItemTemplate>
                                                        <table style="border: none;">
                                                            <tr>
                                                                <td style="height: 40px;" align="left">
                                                                    <asp:Label ID="lblamt" runat="server" Text="Amount"></asp:Label>
                                                                    <asp:TextBox Width="100" TabIndex="59" Text='<%#Eval("Amount") %>' ValidationGroup="GrpRowDebit"
                                                                        CssClass="twitterStyleTextbox sp_currency" ID="txtAmountDebit" runat="server">
                                                                    </asp:TextBox>
                                                                    <asp:RequiredFieldValidator Display="Dynamic" ValidationGroup="GrpRowDebit" ControlToValidate="txtAmountDebit"
                                                                        ID="rfvAmt" ErrorMessage="*" runat="server"> </asp:RequiredFieldValidator>
                                                                    <asp:RegularExpressionValidator Display="Dynamic" ValidationGroup="GrpRowDebit" ID="RegularExpressionValidator7"
                                                                        runat="server" ControlToValidate="txtAmountDebit" ErrorMessage="Invalid Amount!!!"
                                                                        ValidationExpression="^\d+(\.\d{1,2})?$"></asp:RegularExpressionValidator>
                                                                </td>

                                                            </tr>
                                                        </table>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Description">
                                                    <ItemTemplate>
                                                        <table style="border: none;">
                                                            <tr>
                                                                <td style="height: 40px;">
                                                                    <asp:Label ID="lblDesc" runat="server" Text="Description"></asp:Label>
                                                                    <asp:TextBox TextMode="MultiLine" Height="50" TabIndex="60" Text='<%#Eval("Description") %>'
                                                                        ValidationGroup="GrpRowDebit" CssClass="input-text" ID="txtDescriptionDebit" Width="200px"
                                                                        runat="server">
                                                                    </asp:TextBox>
                                                                </td>
                                                                <td style="height: 40px;" align="left"></td>
                                                            </tr>
                                                        </table>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
                        <ProgressTemplate>
                            <div>
                                <div style="position: absolute; left: 50%; margin-left: -50px; top: 186px;">
                                    <asp:Image AlternateText="waiting" runat="server" ID="imgWaiting" Style="vertical-align: middle;" />
                                </div>
                            </div>
                        </ProgressTemplate>
                    </asp:UpdateProgress>
                    <div style="margin: 0 auto;">
                        <br />
                        <asp:Button TabIndex="63" CausesValidation="false" CssClass="GreenyPushButton" Style="display: block; width: 100px; margin: 0 auto;"
                            ID="btnGenerate" OnClick="btnGenerate_Click" Text="Generate"
                            runat="server" />
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>
    <asp:LinkButton Text="" runat="server" ID="btnShowPopup"></asp:LinkButton>

    <asp:Panel Visible="false" ID="pnlpopup" Style="background: #fff; border: 4px solid #ccc; border: 8px solid rgba(0, 0, 0, 0.5); background-clip: padding-box; -webkit-border-radius: 10px; -moz-border-radius: 10px; border-radius: 10px; min-width: 300px; max-width: 900px;"
        CssClass="raised" runat="server">
        <asp:Label runat="server" ID="lblHint" Text="" Visible="false"> </asp:Label>
        <div class=" box_c_heading">
            <asp:Label CssClass="inner_heading" runat="server" ID="lblHeading" Text=""> </asp:Label>
        </div>
        <div style="min-height: 20px; overflow: hidden; max-width: 880px; padding: 20px;">
            <div style="min-height: 20px; overflow: auto; max-width: 840px;">
                <br />
                <asp:Label runat="server" Style="margin-top: 10px; font-size: 14px;" ID="lblContent"
                    Text=""> </asp:Label>
                <br />
                <asp:PlaceHolder ID="DynamicControlsHolder" runat="server"></asp:PlaceHolder>
                <asp:GridView Width="800" ID="gvoldmember" runat="server" AutoGenerateColumns="False"
                    HeaderStyle-BackColor="#61A6F8" ShowFooter="True" HeaderStyle-Font-Bold="true"
                    HeaderStyle-ForeColor="White" PageSize="20" BackColor="White" BorderWidth="1px"
                    CellPadding="4" BorderColor="#DEDFDE" BorderStyle="None" ForeColor="Black" GridLines="None">
                    <RowStyle BackColor="#F7F7DE" />
                    <Columns>
                        <asp:TemplateField HeaderText="Heads">
                            <ItemTemplate>
                                <asp:Label Style="width: 150px;" ID="lblitemoldname" runat="server" Text='<%#Eval("Heads") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Credit Amount">
                            <ItemTemplate>
                                <asp:Label Style="width: 150px;" ID="lblCredit" runat="server" Text='<%#Eval("Credit") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Debit Amount">
                            <ItemTemplate>
                                <asp:Label Style="width: 150px;" ID="lblDebit" runat="server" Text='<%#Eval("Debit") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <HeaderStyle VerticalAlign="Middle" HorizontalAlign="left" CssClass="GridviewScrollC2Header" />
                    <RowStyle HorizontalAlign="left" VerticalAlign="Middle" CssClass="GridviewScrollC2Item" />
                    <PagerStyle CssClass="GridviewScrollC2Pager" />
                </asp:GridView>
            </div>
        </div>
        <div class=" box_c_heading">
            <div style="float: right;">
                <asp:Button Width="100" Style="margin: 0 auto" CssClass="GreenyPushButton" ID="btnYes"
                    OnClick="btnYes_Click" runat="server" Text="yes" />
                <asp:Button Width="100" Style="margin: 0 auto" CssClass="GreenyPushButton" ID="btnNo"
                    OnClick="btnNo_Click" runat="server" Text="No" />
            </div>
        </div>
    </asp:Panel>
    <asp:LinkButton Text="" runat="server" ID="btnShowPopupCheque"></asp:LinkButton>
    <ajax:ModalPopupExtender ID="MpAll" runat="server" TargetControlID="btnShowPopupCheque"
        PopupControlID="panCheque" BackgroundCssClass="modalBackground">
    </ajax:ModalPopupExtender>

    <script type="text/javascript">
        function OnKeyPress(s, e) {
            var charCode = e.htmlEvent.charCode;
            if (String.fromCharCode(charCode) == "/") {
                e.processOnServer = false;
                return false
            }
        }

        $(document).ready(function () {
            $(".chzn-select").chosen({ search_contains: true });
            $(".sp_currency").numeric({ negative: false });
            $(".sp_number").numeric({ decimal: false, negative: false }, function () { alert("Positive integers only"); this.value = ""; this.focus(); });
            prth_mask_input.init();
        });
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $(".chzn-select").chosen({ search_contains: true });
            $(".sp_currency").numeric({ negative: false });
            $(".sp_number").numeric({ decimal: false, negative: false }, function () { alert("Positive integers only"); this.value = ""; this.focus(); });
            prth_mask_input.init();
        });
    </script>
</asp:Content>
