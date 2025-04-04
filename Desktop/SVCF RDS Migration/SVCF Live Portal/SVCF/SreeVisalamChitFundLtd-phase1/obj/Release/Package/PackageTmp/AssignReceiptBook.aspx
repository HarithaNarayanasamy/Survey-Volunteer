<%@ Page Language="C#" MasterPageFile="~/Branch.Master" AutoEventWireup="true" CodeBehind="AssignReceiptBook.aspx.cs"
    Inherits="SreeVisalamChitFundLtd_phase1.AssignReceiptBook" Title="Assign Receipt Book" %>

<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Head1" ContentPlaceHolderID="cphHead" runat="server">
    <style type="text/css">
        .GridviewScrollC2Header TH, .GridviewScrollC2Header TD
        {
            background-color: Gray !important;
        }
        #ctl00_cphMainContent_ddlMoneyCollectorName_chzn .chzn-drop .chzn-search input[type="text"]
        {
            height: 15px;
        }
        .chzn-results
        {
            text-align: center;
        }
    </style>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMainContent" runat="server">
    <asp:Panel runat="server" ID="gshshs" DefaultButton="btnAddcollector">
        <ajax:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </ajax:ToolkitScriptManager>
        <div class="row">
            <div class="twelve columns">
                <div class="box_c">
                    <div class="box_c_heading  box_actions">
                        <p>
                            Assign Receipt Book</p>
                    </div>
                    <div class="box_c_content">
                        <div class="row">
                            <div style="width: 100%; margin: 0 auto;">
                                <asp:UpdatePanel runat="server">
                                    <ContentTemplate>
                                        <table style="margin: 0px auto; padding-left: 2px;">
                                            <tr>
                                                <td style="vertical-align: top; padding-top: 6px; padding-right: 6px;">
                                                    <asp:Label ID="Label1" runat="server" Text="Select "></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlMoneyCollectorName" runat="server" TabIndex="1" OnSelectedIndexChanged="ddlMoneyCollectorName_SelectedIndexChanged"
                                                        Width="240px" Style="width: 240px !important;" CssClass="chzn-select" onchange="clearValidationErrors();"
                                                        AutoPostBack="true" ValidationGroup="add">
                                                    </asp:DropDownList>
                                                    <asp:CompareValidator ID="CompareValidator11" ValueToCompare="0" ValidationGroup="aaa"
                                                        ControlToValidate="ddlMoneyCollectorName" Display="Dynamic" Operator="NotEqual"
                                                        runat="server" ErrorMessage="Select Money Collector"></asp:CompareValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="vertical-align: top; padding-top: 6px; padding-right: 6px;">
                                                    <asp:Label ID="label2" runat="server" Text="Money Collector Name"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="TxtcollectorID" ReadOnly="true" runat="server" TabIndex="2" CssClass="input-text"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ValidationGroup="aaa" ID="RequiredFieldValidator2" runat="server"
                                                        Display="Dynamic" ControlToValidate="TxtcollectorID" ErrorMessage="Invalid Money Collector ID"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="vertical-align: top; padding-top: 46px; padding-right: 6px;">
                                                    <asp:Label ID="label3" runat="server" Text="Money Collector Address"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtcollectoradd" TabIndex="3" ReadOnly="true" runat="server" TextMode="MultiLine"
                                                        CssClass="input-text"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ValidationGroup="aaa" ID="RequiredFieldValidator3" Display="Dynamic"
                                                        runat="server" ControlToValidate="txtcollectoradd" ErrorMessage="Invalid Address"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="vertical-align: top; padding-top: 6px; padding-right: 6px;">
                                                    <asp:Label ID="label4" runat="server" Text="Money Collector Phone Number"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtcollecorphoneno" TabIndex="4" ReadOnly="true" runat="server"
                                                        CssClass="input-text"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ValidationGroup="aaa" ID="RequiredFieldValidator4" Display="Dynamic"
                                                        runat="server" ControlToValidate="txtcollecorphoneno" ErrorMessage="Invalid Phone Number"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="vertical-align: top; padding-top: 6px; padding-right: 6px;">
                                                    <asp:Label ID="label5" runat="server" Text="Receipt Book Series"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtbookseries" TabIndex="5" runat="server" placeholder="Receipt Series"
                                                        CssClass="input-text ttip_r" ToolTip="Ex. KR123"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ValidationGroup="aaa" ID="RequiredFieldValidator1" runat="server"
                                                        ControlToValidate="txtbookseries" Display="Dynamic" ErrorMessage="Enter Receipt Series"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="vertical-align: top; padding-top: 6px; padding-right: 6px;">
                                                    <asp:Label ID="Label6" runat="server" Text="Receipt Number From"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox TabIndex="7" CssClass="input-text ttip_r sp_number" onkeyup="receiptfrom(this);"
                                                        onchange="receiptfrom(this);" runat="server" placeholder="Receipt Number From"
                                                        ToolTip="Ex. 200" ID="txtreceiptnofrom">
                                                    </asp:TextBox>
                                                    <asp:RequiredFieldValidator ValidationGroup="aaa" ID="RequiredFieldValidator6" runat="server"
                                                        ControlToValidate="txtreceiptnofrom" Display="Dynamic" ErrorMessage="Enter Receipt Starting From"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="vertical-align: top; padding-top: 6px !important; padding-right: 6px;">
                                                    <asp:Label ID="Label7" runat="server" Text="Receipt Number To"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox TabIndex="8" ReadOnly="true" CssClass="input-text ttip_r sp_number"
                                                        runat="server" placeholder="Receipt Number To" ToolTip="Ex. 200" ID="txtreceiptnoto">
                                                    </asp:TextBox>
                                                    <asp:HiddenField ID="txtreceiptnoto_h" runat="server" />
                                                    <asp:RequiredFieldValidator ValidationGroup="aaa" ID="RequiredFieldValidator7" runat="server"
                                                        ControlToValidate="txtreceiptnoto" Display="Dynamic" ErrorMessage="Enter Receipt To"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="vertical-align: top; padding-top: 6px !important; padding-right: 6px;">
                                                    <asp:Label ID="Label10" runat="server" Text="Last Used Receipt No."></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox Text="0" TabIndex="9" CssClass="input-text ttip_r sp_number" runat="server"
                                                        placeholder="Last Used Receipt Number" ToolTip="Should be 0 or between Receipt Number from and to."
                                                        ID="txtLastUsed">
                                                    </asp:TextBox>
                                                    <asp:RequiredFieldValidator ValidationGroup="aaa" ID="RequiredFieldValidator8" runat="server"
                                                        ForeColor="Red" ControlToValidate="txtLastUsed" Display="Dynamic" ErrorMessage="Enter Last Used Receipt Number"></asp:RequiredFieldValidator>
                                                    <asp:CustomValidator Display="Dynamic" ForeColor="Red" runat="server" ID="cusCustom"
                                                        ValidationGroup="aaa" ControlToValidate="txtLastUsed" OnServerValidate="cusRange_ServerValidate"
                                                        ErrorMessage="The value must be in selected range !" />
                                                </td>
                                            </tr>
                                        </table>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                <table style="margin: 0px auto; padding-left: 2px;">
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                            <asp:Button ValidationGroup="aaa" TabIndex="9" ID="btnAddcollector" Text="Assign"
                                                Style="margin: 0px auto;" runat="server" CssClass="GreenyPushButton" OnClick="btnAddcollector_Click">
                                            </asp:Button>
                                            <asp:Button TabIndex="10" ID="btnCancel" Style="margin: 0px auto;" Text="Cancel"
                                                OnClick="btnCancel_Click" OnClientClick="clearValidationErrors();" CausesValidation="false"
                                                runat="server" CssClass="GreenyPushButton"></asp:Button>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <a href="#" id="lk" runat="server"></a>
        <asp:ModalPopupExtender ID="ModalPopup" runat="server" BackgroundCssClass="modalBackground"
            TargetControlID="lk" PopupControlID="pandupName">
        </asp:ModalPopupExtender>
        <asp:Panel CssClass="raised" ID="pandupName" runat="server" Visible="false" Style="max-height: 450px;
            min-height: 350px">
            <asp:Label runat="server" ID="lblHint" Text="" Visible="false"> </asp:Label>
            <div style="top: 0px; height: 50px; text-align: center; padding: auto 0; width: 100%"
                class="boxfo">
                <asp:Label ID="Label8" runat="server" Text="Money Collector Name"> </asp:Label>
            </div>
            <div id="Div1" style="max-height: 350px; min-height: 250px; overflow: auto; width: 100%;">
                <div style="width: 100%; margin-left: 1%; padding-top: 1%;">
                    <asp:GridView ID="GridView1" DataKeyNames="moneycollid" Visible="false" runat="server"
                        ShowFooter="True" AutoGenerateColumns="False" CellPadding="4" GridLines="None"
                        CssClass="popup" Width="450px">
                        <Columns>
                            <asp:TemplateField HeaderText="Money Collector ID">
                                <ItemTemplate>
                                    <asp:Label ID="lblMoneyCollID" runat="server" ReadOnly="true" Text='<%#Eval("moneycollid")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Money Collector Name">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtMoneyCollName" runat="server" ReadOnly="true" Text='<%#Eval("moneycollname")%>'></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Address">
                                <ItemTemplate>
                                    <asp:Label ID="lblMoneycollAddr" runat="server" TextMode="MultiLine" Text='<%#Eval("moneycolladdress")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="PhoneNo.">
                                <ItemTemplate>
                                    <asp:Label ID="lblphno" runat="server" Text='<%#Eval("moneycollphno")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Button ID="btnAdd" OnClick="btnAdd_click" CausesValidation="false" CssClass="GreenyPushButton"
                                        CommandName="Add" runat="server" Text="Add" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <FooterStyle VerticalAlign="Middle" HorizontalAlign="Center" CssClass="GridviewScrollC2Header" />
                        <HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center" CssClass="GridviewScrollC2Header" />
                        <RowStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="GridviewScrollC2Item" />
                    </asp:GridView>
                </div>
            </div>
            <div class="boxhe" style="width: 100%; bottom: 0px; height: 50px; position: absolute">
                <div style="padding-left: 40%;">
                    <asp:Button Style="margin: 0 auto" CssClass="GreenyPushButton" OnClick="cancel_OnClick"
                        ID="BtnNo" CausesValidation="false" runat="server" Text="No" />
                </div>
            </div>
        </asp:Panel>
        <asp:Panel CssClass="raised" ID="Pnlmsg1" runat="server" Visible="false" Width="350px"
            Style="min-height: 100px">
            <asp:Label runat="server" ID="Label9" Text="" Visible="false"> </asp:Label>
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
                    <asp:Button Style="margin: 0 auto" CssClass="GreenyPushButton" ID="btnOK" OnClick="btnOK_Click"
                        runat="server" Text="yes" />
                    <asp:Button Style="margin: 0 auto" CssClass="GreenyPushButton" ID="btnCa" runat="server"
                        OnClick="btnCa_Click" Text="No" />
                </div>
            </div>
        </asp:Panel>
    </asp:Panel>
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
        filterInt = function (value) {
            value = value.replace(',', '');
            if (/^(\-|\+)?([0-9]+|Infinity)$/.test(value))
                return Number(value);
            return 0;
        }

    </script>
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
                    this.focus()
                });
            prth_mask_input.init();

        });
        function receiptfrom(txt) {
            var sssss = txt.value;
            if (sssss > 0 && sssss != null) {


                document.getElementById('<%= txtreceiptnoto.ClientID %>').value = filterInt(sssss) + 199;
                document.getElementById('<%= txtreceiptnoto_h.ClientID %>').value = filterInt(sssss) + 199;
            }
            else {
                document.getElementById('<%= txtreceiptnoto.ClientID %>').value = 0;
                document.getElementById('<%= txtreceiptnoto_h.ClientID %>').value = 0;
            }
        }

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
                    this.focus()
                });
            prth_mask_input.init();
            prth_tips.init();
        });
    </script>
</asp:Content>
