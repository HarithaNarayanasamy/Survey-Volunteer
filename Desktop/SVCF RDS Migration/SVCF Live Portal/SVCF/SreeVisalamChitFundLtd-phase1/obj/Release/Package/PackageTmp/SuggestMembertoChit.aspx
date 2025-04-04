<%@ Page Language="C#" Culture="en-GB" MasterPageFile="~/Branch.Master" AutoEventWireup="true"
    CodeBehind="SuggestMembertoChit.aspx.cs" Inherits="SreeVisalamChitFundLtd_phase1.SuggestMembertoChit"
    Title="SVCF Admin Panel" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <style type="text/css">
        input[type="image"]:active
        {
            -moz-box-shadow: inset 0 0 10px rgba(0,0,0,.06);
            -webkit-box-shadow: inset 0 0 10px rgba(0,0,0,.06);
            box-shadow: inset 0 0 10px rgba(0,0,0,.06);
            z-index: 6;
            border: 2px solid #006469 !important;
        }
        #ctl00_cphMainContent_ddlChitGroup_chzn .chzn-drop .chzn-search input[type="text"]
        {
            height: 15px;
        }
        #ctl00_cphMainContent_ddlMembName_chzn .chzn-drop .chzn-search input[type="text"]
        {
            height: 15px;
        }
        #ctl00_cphMainContent_ddlMoneyCollector_chzn .chzn-drop .chzn-search input[type="text"]
        {
            height: 15px;
        }
        .chzn-drop
        {
            border: 1px solid #aaa !important;
            padding-top: 5px !important;
            width: 240px !important;
        }
        .chzn-results
        {
            text-align: center !important;
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
                        Suggest Member to Chit</p>
                </div>
                <div class="box_c_content">
                    <div class="row">
                        <br />
                        <div style="width: 100%">
                            <asp:UpdatePanel runat="server" ID="up1">
                                <ContentTemplate>
                                    <table style="margin: 0 auto; width: 90%">
                                        <tr>
                                            <td colspan="4">
                                                <asp:CheckBox onclick="clearValidationErrors();" Style="float: left;" Visible="false" AutoPostBack="true"
                                                    ID="ckkLoadAllCommon" Text="Load From All Branch" runat="server" OnCheckedChanged="chkLoadAll_CheckedChanged" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td >
                                            </td>
                                            <td>
                                            </td>
                                            <td style="vertical-align: top; padding-top: 6px !important;">
                                                <asp:Label ID="Label14" runat="server" Text="Branch Name"></asp:Label>
                                            </td>
                                            <td style="vertical-align: top;">
                                                <asp:DropDownList onchange="clearValidationErrors();" Width="240px" CssClass="chzn-select"
                                                    runat="server" ID="ddlBranchName" AutoPostBack="true"
                                                    TabIndex="2" OnSelectedIndexChanged="ddlBranchName_SelectedIndexChanged">
                                                </asp:DropDownList>
                                                <%--<asp:CompareValidator ValidationGroup="sug" ID="CompareValidator6" runat="server"
                                                    Display="Dynamic" ControlToValidate="ddlBranchName" ErrorMessage="*" Operator="NotEqual"
                                                    ValueToCompare="0"></asp:CompareValidator>--%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="vertical-align: top; padding-top: 6px !important;">
                                                <asp:Label ID="Label1" runat="server" Text="Chit Group"></asp:Label>
                                            </td>
                                            <td style="vertical-align: top;">
                                                <asp:DropDownList Width="240px" onchange="clearValidationErrors();" CssClass="chzn-select"
                                                    runat="server" ID="ddlChitGroup" TabIndex="1">
                                                </asp:DropDownList>
                                                <asp:CompareValidator ValidationGroup="sug" ID="CompareValidator2" runat="server"
                                                    Display="Dynamic" ControlToValidate="ddlChitGroup" ErrorMessage="*" Operator="NotEqual"
                                                    ValueToCompare="0"></asp:CompareValidator>
                                            </td>
                                            <td style="vertical-align: top; padding-top: 6px !important;">
                                                <asp:Label ID="Label12" runat="server" Text="Member Name"></asp:Label>
                                            </td>
                                            <td style="vertical-align: top;">
                                                <asp:DropDownList onchange="clearValidationErrors();" Width="240px" CssClass="chzn-select"
                                                    runat="server" ID="ddlMembName" AutoPostBack="true" OnSelectedIndexChanged="ddlMembName_SelectedIndexChanged"
                                                    TabIndex="2">
                                                </asp:DropDownList>
                                                <asp:CompareValidator ValidationGroup="sug" ID="CompareValidator1" runat="server"
                                                    Display="Dynamic" ControlToValidate="ddlMembName" ErrorMessage="*" Operator="NotEqual"
                                                    ValueToCompare="0"></asp:CompareValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label Visible="false" ID="Label4" runat="server" Text="Chit pool"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList Visible="false" CssClass="chzn-select" runat="server" ID="ddlChitPool">
                                                </asp:DropDownList>
                                                <asp:CompareValidator Visible="false" ID="CompareValidator3" runat="server" Display="Dynamic"
                                                    ControlToValidate="ddlChitPool" ErrorMessage="*Required" Operator="NotEqual"
                                                    ValueToCompare="--Select--"></asp:CompareValidator>
                                                <asp:HyperLink Visible="false" CssClass="gh_button icon search" ID="hlDemo" runat="server"
                                                    onclick="openNewWindows()">Find By Details Pool</asp:HyperLink>
                                                <asp:LinkButton Visible="false" Style="margin: 3px;" CssClass="gh_button icon search"
                                                    CausesValidation="false" ID="lbFindbyName" runat="server" OnClick="lbFindpool_Click"
                                                    Text="Show Relations"></asp:LinkButton>
                                                <asp:DropDownList CssClass="chzn-select" AutoPostBack="true" Visible="false" ID="ddlFindbyName"
                                                    runat="server" OnSelectedIndexChanged="ddlFindbyName_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:Label Visible="false" ID="Label5" runat="server" Text="Relation"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox Visible="false" runat="server" CssClass="twitterStyleTextbox" ID="txtAddress"
                                                    TextMode="MultiLine" TabIndex="19"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="vertical-align: top; padding-top: 6px !important;">
                                                <asp:Label ID="Label6" runat="server" Text="No of Tokens"></asp:Label>
                                            </td>
                                            <td style="vertical-align: top;">
                                                <asp:TextBox ID="txtNoofTokens" runat="server" CssClass="input-text sp_number" TabIndex="3"></asp:TextBox>
                                                <asp:RequiredFieldValidator ValidationGroup="sug" ID="rv" runat="server" Display="Dynamic"
                                                    ErrorMessage="*" ControlToValidate="txtNoofTokens">
                                                </asp:RequiredFieldValidator>
                                            </td>
                                            <td style="vertical-align: top; padding-top: 6px !important;">
                                                <asp:Label ID="lblIncome" runat="server" Text="Income(Monthly)"></asp:Label>
                                            </td>
                                            <td style="vertical-align: top;">
                                                <asp:TextBox ID="txtIncome" runat="server" CssClass="input-text sp_currency" TabIndex="4"></asp:TextBox>
                                                <asp:RequiredFieldValidator ValidationGroup="sug" ID="RequiredFieldValidator3" runat="server"
                                                    Display="Dynamic" ErrorMessage="*" ControlToValidate="txtIncome">
                                                </asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ValidationGroup="sug" Display="Dynamic" ID="RegularExpressionValidator4"
                                                    runat="server" ControlToValidate="txtIncome" ErrorMessage="Invalid Amount!!!"
                                                    ValidationExpression="^\d+(\.\d{1,2})?$"></asp:RegularExpressionValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="vertical-align: top; padding-top: 6px !important;">
                                                <asp:Label runat="server" ID="lblMoneyCollector" Text="Money Collector "></asp:Label>
                                            </td>
                                            <td style="vertical-align: top;">
                                                <asp:DropDownList onchange="clearValidationErrors();" Width="240px" AutoPostBack="true"
                                                    ID="ddlMoneyCollector" runat="server" CssClass="chzn-select" TabIndex="5" OnSelectedIndexChanged="ddlMoneyCollector_SelectedIndexChanged">
                                                </asp:DropDownList>
                                                <asp:CompareValidator ValidationGroup="sug" ID="CompareValidator4" runat="server"
                                                    Display="Dynamic" ControlToValidate="ddlMoneyCollector" ErrorMessage="*" Operator="NotEqual"
                                                    ValueToCompare="0"></asp:CompareValidator>
                                            </td>
                                            <td style="vertical-align: top; padding-top: 6px !important;">
                                                <asp:Label ID="Label2" runat="server" Text="Est.Call No Of Auction"></asp:Label>
                                            </td>
                                            <td style="vertical-align: top;">
                                                <asp:TextBox runat="server" CssClass="input-text" ID="txtEstCallNoofAuction" TabIndex="6"></asp:TextBox>
                                                <asp:RequiredFieldValidator ValidationGroup="sug" ID="RequiredFieldValidator1" runat="server"
                                                    ErrorMessage="*" Display="Dynamic" ControlToValidate="txtEstCallNoofAuction"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="vertical-align: top; padding-top: 35px !important;">
                                                <asp:Label ID="Label3" runat="server" Text="Est. Surety Document"></asp:Label>
                                            </td>
                                            <td style="vertical-align: top;">
                                                <asp:TextBox runat="server" TextMode="MultiLine" CssClass="input-text" ID="txtEstSuretyDocument"
                                                    TabIndex="7"></asp:TextBox>
                                                <asp:RequiredFieldValidator ValidationGroup="sug" Display="Dynamic" ID="RequiredFieldValidator4"
                                                    runat="server" ErrorMessage="*" ControlToValidate="txtEstSuretyDocument"></asp:RequiredFieldValidator>
                                            </td>
                                            <td style="vertical-align: top; padding-top: 35px !important;">
                                                <asp:Label runat="server" ID="lblmcDesc" Text="Description"></asp:Label>
                                            </td>
                                            <td style="vertical-align: top;">
                                                <asp:TextBox runat="server" CssClass="input-text" ID="txtMcDesc" TextMode="MultiLine"
                                                    TabIndex="8"></asp:TextBox>
                                                <asp:RequiredFieldValidator ValidationGroup="sug" ID="RequiredFieldValidator2" runat="server"
                                                    ErrorMessage="*" Display="Dynamic" ControlToValidate="txtMcDesc"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                    </table>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <div style="width: 100% !important;">
                                <asp:Button ID="btnSuggest" ValidationGroup="sug" CausesValidation="True" CssClass="GreenyPushButton"
                                    Text="Suggest" Style="width: 120px; float: right;" runat="server" OnClick="btnSuggest_Click">
                                </asp:Button>
                            </div>
                        </div>
                    </div>
                    <a href="#" id="lk" runat="server"></a>
                    <ajax:ModalPopupExtender ID="mpAll" runat="server" BackgroundCssClass="modalBackground"
                        TargetControlID="lk" PopupControlID="pandupName">
                    </ajax:ModalPopupExtender>
                    <asp:Panel ID="pandupName" Style="background: #fff; border: 4px solid #ccc; border: 8px solid rgba(0, 0, 0, 0.5);
                        background-clip: padding-box; -webkit-border-radius: 10px; -moz-border-radius: 10px;
                        border-radius: 10px; min-width: 300px; max-width: 900px;" CssClass="raised" runat="server"
                        Visible="false">
                        <asp:Label runat="server" ID="Label7" Text="" Visible="false"> </asp:Label>
                        <div class=" box_c_heading">
                            <asp:Label ID="Label11" runat="server" Text=" Choose Member"> </asp:Label>
                        </div>
                        <div style="min-height: 20px; overflow: hidden; max-width: 880px; padding: 20px;">
                            <div style="min-height: 20px; overflow: auto; max-width: 840px;">
                                <br />
                                <br />
                                <asp:Label ID="lblHeadingdub" runat="server" Text="" Style="text-align: justify;
                                    vertical-align: middle; margin-left: 10px"> </asp:Label>
                                <asp:GridView ID="gvDubName" DataKeyNames="MemberID" Visible="false" runat="server"
                                    ShowFooter="True" AutoGenerateColumns="False" Width="100%">
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:Button CausesValidation="false" ID="btnAdd" OnClick="btnChoose_click" CssClass="GreenyPushButton"
                                                    CommandName="Choose" runat="server" Text="Choose" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="MemberID">
                                            <ItemTemplate>
                                                <asp:Label ID="lblMemberID" runat="server" ReadOnly="true" Text='<%#Eval("MemberID")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="MemberName">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtMemName" runat="server" 
                                                    ReadOnly="true" Text='<%#Eval("CustomerName")%>'></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="MemberAddress">
                                            <ItemTemplate>
                                                <asp:Label ID="lblMemAddr" runat="server" ReadOnly="true" 
                                                    TextMode="MultiLine" Text='<%#Eval("AddressForCommunication")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle VerticalAlign="Middle" HorizontalAlign="Center" CssClass="GridviewScrollC2Header" />
                                    <HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center" CssClass="GridviewScrollC2Header" />
                                    <RowStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="GridviewScrollC2Item" />
                                    <PagerStyle CssClass="GridviewScrollC2Pager" />
                                </asp:GridView>
                            </div>
                        </div>
                        <div class=" box_c_heading">
                            <div style="float: right;">
                                <asp:Button ID="Btncan" runat="server" CssClass="GreenyPushButton" Visible="false"
                                    Text="Cancel" OnClick="Btncan_Click" /></p>
                            </div>
                        </div>
                        <%--   <div class="boxheader" style="top: 0px; height: 50px; text-align: center; padding: auto 0;
                            width: 100%" class="boxheader">
                            <asp:Label runat="server" ID="lblHintdub" Text="" Visible="false"> </asp:Label>
                        </div>
                        <div id="content" style="max-height: 500px; min-height: 200px; overflow: auto; width: auto;">
                            <asp:Label ID="lblHeadingdub1" runat="server" Text="Choose Member"> </asp:Label>
                            <asp:GridView ID="gvDubName1" DataKeyNames="MemberID" Visible="false" runat="server"
                                ShowFooter="True" AutoGenerateColumns="False" Width="100%">
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:Button CausesValidation="false" ID="btnAdd" OnClick="btnChoose_click" CssClass="GreenyPushButton"
                                                CommandName="Choose" runat="server" Text="Choose" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="MemberID">
                                        <ItemTemplate>
                                            <asp:Label ID="lblMemberID" runat="server" ReadOnly="true" Text='<%#Eval("MemberID")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="MemberName">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtMemName" runat="server" Style="background: url('Images/abc.png') no-repeat scroll right center #FFFFFF;"
                                                ReadOnly="true" Text='<%#Eval("CustomerName")%>'></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="MemberAddress">
                                        <ItemTemplate>
                                            <asp:Label ID="lblMemAddr" runat="server" ReadOnly="true" Style="background: url('Images/ad.png') no-repeat scroll right center #FFFFFF;"
                                                TextMode="MultiLine" Text='<%#Eval("AddressForCommunication")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <FooterStyle VerticalAlign="Middle" HorizontalAlign="Center" CssClass="GridviewScrollC2Header" />
                                <HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center" CssClass="GridviewScrollC2Header" />
                                <RowStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="GridviewScrollC2Item" />
                                <PagerStyle CssClass="GridviewScrollC2Pager" />
                            </asp:GridView>
                        </div>
                        <div class="boxheader" style="width: 100%; bottom: 0px; height: 50px;">
                            <p style="text-align: center">
                                <asp:Button ID="Btncan" runat="server" CssClass="GreenyPushButton" Visible="false"
                                    Text="Cancel" OnClick="Btncan_Click" /></p>
                        </div>--%>
                    </asp:Panel>
                    <asp:Panel ID="Pnlmsg" Style="background: #fff; border: 4px solid #ccc; border: 8px solid rgba(0, 0, 0, 0.5);
                        background-clip: padding-box; -webkit-border-radius: 10px; -moz-border-radius: 10px;
                        border-radius: 10px; min-width: 300px; max-width: 900px;" CssClass="raised" runat="server"
                        Visible="false">
                        <asp:Label runat="server" ID="lblHint" Text="" Visible="false"> </asp:Label>
                        <div class=" box_c_heading">
                            <asp:Label ID="lblHeading" runat="server" Text=""> </asp:Label>
                        </div>
                        <div style="min-height: 20px; overflow: hidden; max-width: 880px; padding: 20px;">
                            <div style="min-height: 20px; overflow: auto; max-width: 840px;">
                                <br />
                                <br />
                                <asp:Label ID="lblContent" runat="server" Text="" Style="text-align: justify; vertical-align: middle;
                                    margin-left: 10px"> </asp:Label>
                            </div>
                        </div>
                        <div class=" box_c_heading">
                            <div style="float: right;">
                                <asp:Button OnClick="btn_Yesy_Click" Width="100" Style="margin: 0 auto" CssClass="GreenyPushButton"
                                    ID="Button1" runat="server" Text="Ok" />
                                <asp:Button OnClick="btn_Yesy_Click" Width="100" Style="margin: 0 auto" CssClass="GreenyPushButton"
                                    ID="Button2" runat="server" Text="Cancel" />
                            </div>
                        </div>
                    </asp:Panel>
                    <%--      <asp:Panel CssClass="raised" ID="Pnlmsg1" runat="server" Visible="false" Width="600px"
                        Style="min-height: 200px">
                        <asp:Label runat="server" ID="lblHint1" Text="" Visible="false"> </asp:Label>
                        <div style="top: 0px; height: 50px; text-align: center; padding: auto 0; width: 100%"
                            class="boxfooter">
                            <asp:Label ID="lblHeading1" runat="server" Text=""> </asp:Label>
                        </div>
                        <div id="Div1" style="min-height: 200px;">
                            <br />
                            <br />
                            <asp:Label ID="lblContent1" runat="server" Text="" Style="text-align: justify; vertical-align: middle;
                                margin-left: 10px"> </asp:Label>
                            <br />
                            <br />
                            <asp:PlaceHolder ID="DynamicControlsHolder" runat="server"></asp:PlaceHolder>
                        </div>
                        <div class="boxheader">
                            <div style="margin: 0 auto;">
                                <asp:Button OnClick="btn_Yesy_Click" Width="100" Style="margin: 0 auto" CssClass="GreenyPushButton"
                                    ID="Button1x" runat="server" Text="Ok" />
                                <asp:Button OnClick="btn_Yesy_Click" Width="100" Style="margin: 0 auto" CssClass="GreenyPushButton"
                                    ID="Button2x" runat="server" Text="Cancel" />
                            </div>
                        </div>
                    </asp:Panel>--%>
                    <asp:Panel ID="pnlMCDub" Style="background: #fff; border: 4px solid #ccc; border: 8px solid rgba(0, 0, 0, 0.5);
                        background-clip: padding-box; -webkit-border-radius: 10px; -moz-border-radius: 10px;
                        border-radius: 10px; min-width: 300px; max-width: 900px;" CssClass="raised" runat="server"
                        Visible="false">
                        <asp:Label runat="server" ID="Label8" Text="" Visible="false"> </asp:Label>
                        <div class=" box_c_heading">
                            <asp:Label ID="Label9" runat="server" Text="Choose"> </asp:Label>
                        </div>
                        <div style="min-height: 20px; overflow: hidden; max-width: 880px; padding: 20px;">
                            <div style="min-height: 20px; overflow: auto; max-width: 840px;">
                                <br />
                                <br />
                                <asp:Label ID="Label10" runat="server" Text="Please Choose Money Collector Name"
                                    Style="text-align: justify; vertical-align: middle; margin-left: 10px"> </asp:Label>
                                <asp:GridView ID="gvMCDub" DataKeyNames="moneycollid" runat="server" AutoGenerateColumns="False"
                                    Width="100%">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Action">
                                            <ItemTemplate>
                                                <asp:Button Style="width: 100px;" ID="oldbtnadd" CommandName="Add" runat="server"
                                                    CssClass="GreenyPushButton" Text="Choose" CausesValidation="False" OnClick="btnChooseMC_click" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Collector ID">
                                            <ItemTemplate>
                                                <asp:Label Style="width: 150px;" ID="lblcollectorid" runat="server" Text='<%#Eval("moneycollid") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Collector Name">
                                            <ItemTemplate>
                                                <asp:Label Style="width: 150px;" ID="lblcollectorname" runat="server" Text='<%#Eval("moneycollname") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Address">
                                            <ItemTemplate>
                                                <asp:Label Style="width: 150px;" ID="lblcollectoraddress" runat="server" Text='<%#Eval("moneycolladdress") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Ph.No">
                                            <ItemTemplate>
                                                <asp:Label Style="width: 300px;" ID="lblcollectorphno" runat="server" Text='<%#Eval("moneycollphno") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle VerticalAlign="Middle" HorizontalAlign="Center" CssClass="GridviewScrollC2Header" />
                                    <HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center" CssClass="GridviewScrollC2Header" />
                                    <RowStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="GridviewScrollC2Item" />
                                    <PagerStyle CssClass="GridviewScrollC2Pager" />
                                </asp:GridView>
                            </div>
                        </div>
                        <div class=" box_c_heading">
                            <div style="float: right;">
                                <asp:Button ID="Button3" runat="server" Text="Cancel" OnClick="btn_col_cancel_click"
                                    CssClass="GreenyPushButton" CausesValidation="False" />
                            </div>
                        </div>
                    </asp:Panel>
                </div>
            </div>
        </div>
    </div>
    <script>
        $(document).ready(function () {
            $(".chzn-select").chosen({ search_contains: true });
            //            $(".sp_currency").spinner({
            //                decimals: 2,
            //                stepping: 0.25,
            //                min: 0.00
            //            });
            $(".sp_currency").numeric({ negative: false });
            $(".sp_number").numeric({ decimal: false, negative: false }, function () { alert("Positive integers only"); this.value = ""; this.focus(); });
            //            $(".sp_number").spinner({min: 0,
            //                numberFormat: "d",
            //                culture:"en-GB"
            //            });
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
