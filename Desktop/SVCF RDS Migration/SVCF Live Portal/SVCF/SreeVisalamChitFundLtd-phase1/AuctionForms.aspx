<%@ Page Language="C#" Culture="en-GB" MasterPageFile="~/Branch.Master" AutoEventWireup="true" CodeBehind="AuctionForms.aspx.cs"
    Inherits="SreeVisalamChitFundLtd_phase1.AuctionForms" Title="Auction" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Head1" ContentPlaceHolderID="cphHead" runat="server">
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
                <div class="box_c_heading cf box_actions" style="margin: 0 important;">
                    <div class="box_c_ico">
                        <img src="pertho_admin_v1.3/img/ico/open/arrow-round.png" alt="" /></div>
                    <p>
                        Group Auction
                    </p>
                </div>
                <div class="box_c_content" style="margin:0 important;">
                    <div style="width: 100%">
                        <div style="margin: 0 important;">
                            <div class="formRow">
                                <asp:Label ID="Label2" runat="server" Text="Chit #"></asp:Label>
                                <asp:DropDownList ID="ddlChitNo" ValidationGroup="add" runat="server" CssClass="chzn-select"
                                    AutoPostBack="true" OnSelectedIndexChanged="ddlChitNo_SelectedIndexChanged">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*"
                                    ValidationGroup="add" ControlToValidate="ddlChitNo" InitialValue="--select--"></asp:RequiredFieldValidator>
                            </div>
                            <div class="formRow">
                                <asp:Label ID="lblAction" runat="server" Text="Auction Type"></asp:Label>
                                <asp:DropDownList ID="ddlAuction" runat="server" ValidationGroup="add" CssClass="chzn-select"
                                    OnSelectedIndexChanged="ddlAuction_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" InitialValue="--select--"
                                    ErrorMessage="*" ValidationGroup="add" Height="30px" Display="Dynamic"
                                    ControlToValidate="ddlAuction"></asp:RequiredFieldValidator>
                            </div>
                            <div class="formRow">
                                <asp:Label ID="lblDrawNo" runat="server" Text="Draw #"></asp:Label>
                                <br />
                                <asp:TextBox ID="txtDrawNo" CssClass="twitterStyleTextbox sp_number"
                                    runat="server" ReadOnly="true"></asp:TextBox>
                                <br />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="*"
                                    ValidationGroup="add" Display="Dynamic" ControlToValidate="txtDrawNo"></asp:RequiredFieldValidator>
                                <asp:Label ID="lblErrorMsg" runat="server" Visible="false" ForeColor="Red" Text="Can't Re-Bid,View History for Details "></asp:Label>
                            </div>
                              <div class="formRow">
                                <asp:Label ID="Label5" runat="server" Text="Draw #"></asp:Label>
                                <br />
                                <asp:DropDownList ID="ddlDrawNo" Width="150px" CssClass="chzn-select" 
                                  runat="server" OnSelectedIndexChanged="ddlDrawNo_SelectedIndexChanged"></asp:DropDownList>
                                <br />
                              <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="*"
                                    ValidationGroup="add" Display="Dynamic" ControlToValidate="ddlDrawNo"></asp:RequiredFieldValidator>--%>
                                <asp:Label ID="lblErrorMsg1" runat="server" Visible="false" ForeColor="Red" Text="Can't Re-Bid,View History for Details "></asp:Label>
                            </div>
                            <div class="formRow">
                                <asp:Label ID="lblBenName" runat="server" Text="Beneficiary Name"></asp:Label>
                                <asp:DropDownList ID="ddlBenefName" ValidationGroup="add" runat="server" CssClass="chzn-select"
                                    AutoPostBack="false">
                                </asp:DropDownList>
                                <br />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*"
                                    ValidationGroup="add" ControlToValidate="ddlBenefName" InitialValue="--select--"></asp:RequiredFieldValidator>
                            </div>
                            <div class="formRow">
                                <asp:Label ID="lblPrizAmt" runat="server" Text="Prized Amount"></asp:Label>
                                <br />
                                <asp:TextBox ID="txtPrizamt" AutoPostBack="true" OnTextChanged="txtPrizamt_TextChanged" runat="server" ValidationGroup="add" 
                                    CssClass="twitterStyleTextbox sp_currency"></asp:TextBox>
                                <br />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator41" ErrorMessage="*"
                                    runat="server" ValidationGroup="add" ControlToValidate="txtPrizamt"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ValidationGroup="add"
                                    ErrorMessage="Invalid Amount" runat="server" ControlToValidate="txtPrizamt" ValidationExpression="^^\d+(\.\d{1,2})?$"
                                    Display="Static"></asp:RegularExpressionValidator>
                            </div>
                            
                            <div class="formRow">
                                <asp:Label ID="Label3" runat="server" Text="Default Interest"></asp:Label>
                                <br />
                                <asp:TextBox ID="txtDefaultInterset" AutoPostBack="true" OnTextChanged="txtPrizamt_TextChanged" runat="server" ValidationGroup="add" 
                                    CssClass="twitterStyleTextbox sp_currency"></asp:TextBox>
                                <br />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ErrorMessage="*"
                                    runat="server" ValidationGroup="add" ControlToValidate="txtDefaultInterset"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" ValidationGroup="add"
                                    ErrorMessage="Invalid Amount" runat="server" ControlToValidate="txtDefaultInterset" ValidationExpression="^^\d+(\.\d{1,2})?$"
                                    Display="Static"></asp:RegularExpressionValidator>
                            </div>
                            <div class="formRow">
                                <asp:Label ID="Label4" runat="server" Text="Due Amount"></asp:Label>
                                <br />
                                <asp:TextBox ID="txtDueAmount" runat="server" ValidationGroup="add" 
                                    CssClass="twitterStyleTextbox sp_currency"></asp:TextBox>
                                <br />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ErrorMessage="*"
                                    runat="server" ValidationGroup="add" ControlToValidate="txtDueAmount"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" ValidationGroup="add"
                                    ErrorMessage="Invalid Amount" runat="server" ControlToValidate="txtDueAmount" ValidationExpression="^^\d+(\.\d{1,2})?$"
                                    Display="Static"></asp:RegularExpressionValidator>
                            </div>
                            <div class="formRow">
                                <asp:Label ID="lblRebiddate" runat="server" Text="Re-Bid Date"></asp:Label>
                                <br />
                                <asp:TextBox ID="txtRebiddate" Visible="false" 
                                    runat="server" CssClass="twitterStyleTextbox maskdate"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ValidationGroup="add" ErrorMessage="*"
                                    runat="server" ControlToValidate="txtPrizamt"></asp:RequiredFieldValidator>
                            </div>
                            <div class="formRow">
                                <asp:Button ID="btnAdd" runat="server" CssClass="GreenyPushButton" ValidationGroup="add"
                                    Text="Add" Style="margin: 0px auto;" OnClick="btnAdd_Click" />
                                <asp:Button ID="btnViewHistory" OnClientClick="clearValidationErrors();" runat="server" CssClass="GreenyPushButton" Text="View History"
                                    Style="margin: 0px auto;" OnClick="btnViewHistory_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <ajax:ModalPopupExtender ID="ModalPopup" runat="server" Drag="true" DropShadow="true"
        BackgroundCssClass="modalBackground" PopupControlID="panchitHistory" TargetControlID="btnViewHistory">
    </ajax:ModalPopupExtender>
    <asp:Panel ID="panchitHistory" Style="background: #fff; border: 4px solid #ccc; border: 8px solid rgba(0, 0, 0, 0.5);
        background-clip: padding-box; -webkit-border-radius: 10px; -moz-border-radius: 10px;
        border-radius: 10px; min-width: 300px; max-width: 900px; max-height: 700px;"
        CssClass="raised" runat="server" Visible="false">
        <asp:Label runat="server" ID="lblHint" Text="" Visible="false"> </asp:Label>
        <div class="box_c_heading">
            <asp:Label ID="lbl" Font-Size="Large" CssClass="inset-text" Text="View Auction History"
                runat="Server"></asp:Label>
        </div>
        <div style="max-width: 840px; max-height: 500px !important; overflow: scroll">
            <asp:GridView ID="gridHistory" runat="server" AutoGenerateColumns="False" BorderWidth="1px"
                CellPadding="2">
                <FooterStyle VerticalAlign="Middle" HorizontalAlign="Center" CssClass="GridviewScrollC2Header" />
                <HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center" CssClass="GridviewScrollC2Header" />
                <RowStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="GridviewScrollC2Item" />
                <PagerStyle CssClass="GridviewScrollC2Pager" />
                <Columns>
                    <asp:BoundField HeaderText="AuctionDate" DataField="AuctionDate" />
                    <asp:BoundField HeaderText="DrawNO" DataField="DrawNO" />
                    <asp:BoundField HeaderText="Node" DataField="Node" />
                    <asp:BoundField HeaderText="PrizedAmount" DataField="PrizedAmount" />
                    <asp:BoundField HeaderText="TotalCommission" DataField="TotalCommission" />
                    <asp:BoundField HeaderText="Dividend" DataField="Dividend" />
                    <asp:BoundField HeaderText="KasarAmount" DataField="KasarAmount" />
                    <asp:BoundField HeaderText="CurrentDueAmount" DataField="CurrentDueAmount" />
                    <asp:BoundField HeaderText="NextDueAmount" DataField="NextDueAmount" />
                    <asp:BoundField HeaderText="AdditionalKasarAmount" DataField="AdditionalKasarAmount" />                  
                    <asp:TemplateField HeaderText="PrizedMemberid" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblprizedmemid" runat="server" Text='<%# Eval("PrizedMemberID") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="isreaction" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="reactionval" runat="server" Text='<%# ReacutionValue(Eval("IsReAuction")) %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="ChitGroupid" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblprizedmemid" runat="server" Text='<%# Eval("GroupID") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
        <div class="box_c_heading">
            <div style="float: right">
                <asp:Button Width="100" Style="margin: 0 auto" CssClass="GreenyPushButton" ID="Button1"
                    runat="server" CausesValidation="false" Text="OK" />
                <asp:Button Width="100" Style="margin: 0 auto" CssClass="GreenyPushButton" ID="btnNo"
                    runat="server" CausesValidation="false" Text="Cancel" />
            </div>
        </div>
    </asp:Panel>
    <asp:Panel ID="Pnlmsg1" Style="background: #fff; border: 4px solid #ccc; border: 8px solid rgba(0, 0, 0, 0.5);
        background-clip: padding-box; -webkit-border-radius: 10px; -moz-border-radius: 10px;
        border-radius: 10px; min-width: 300px; max-width: 900px;" CssClass="raised" runat="server"
        Visible="false">
        <asp:Label runat="server" ID="Label1" Text="" Visible="false"> </asp:Label>
        <div class="box_c_heading">
            <asp:Label CssClass="inner_heading" runat="server" ID="lblHeading" Text=""> </asp:Label>
        </div>
        <div style="min-height: 20px; overflow: hidden; max-width: 880px; padding: 20px;">
            <div style="min-height: 20px; overflow: auto; max-width: 840px;">
                <br />
                <br />
                <asp:Label runat="server" Style="margin-top: 10px; font-size: 14px;" ID="lblcon"
                    Text=""> </asp:Label>
                <br />
                <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
            </div>
        </div>
        <div class=" box_c_heading">
            <div style="float: right;">
                <asp:Button Width="100" Style="margin: 0 auto" CssClass="GreenyPushButton" ID="btnOK"
                    runat="server" CausesValidation="false" Text="OK" />
                <asp:Button Width="100" Style="margin: 0 auto" CssClass="GreenyPushButton" ID="Button2"
                    runat="server" CausesValidation="false" Text="Cancel" />
            </div>
        </div>
    </asp:Panel>
    <script type="text/javascript">
        $(document).ready(function () {
            $(".chzn-select").chosen({ search_contains: true });
            $(".sp_currency").numeric({ negative: false });
            $(".sp_number").numeric({ decimal: false, negative: false }, function () { alert("Positive integers only"); this.value = ""; this.focus(); });
            prth_mask_input.init();
        });
    </script>
</asp:Content>
