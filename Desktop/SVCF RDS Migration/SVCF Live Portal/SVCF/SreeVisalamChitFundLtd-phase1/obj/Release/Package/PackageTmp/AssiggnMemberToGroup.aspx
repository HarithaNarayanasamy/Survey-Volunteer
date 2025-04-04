<%@ Page Language="C#" Culture="en-GB" MasterPageFile="~/Branch.Master" AutoEventWireup="true" CodeBehind="AssiggnMemberToGroup.aspx.cs"
    Inherits="SreeVisalamChitFundLtd_phase1.AssiggnMemberToGroup_" Title="SVCF Admin Panel" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <title>Assign Member To Group</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMainContent" runat="server">
    <script type="text/javascript" src="Scripts/jquery.fixedheader.js"></script>
    <link href="Styles/aspxtable.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
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
        .chzn-results
        {
            text-align: center;
        }
        #ctl00_cphMainContent_ddlGrpID_chzn .chzn-drop .chzn-search input[type="text"]
        {
            height: 15px;
        }
        #ctl00_cphMainContent_ddlGrpMemberID_chzn .chzn-drop .chzn-search input[type="text"]
        {
            height: 15px;
        }
        #ctl00_cphMainContent_ddlMemberName_chzn .chzn-drop .chzn-search input[type="text"]
        {
            height: 15px;
        }
        #ctl00_cphMainContent_ddlNoCard_chzn .chzn-drop .chzn-search input[type="text"]
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
    <script type="text/javascript">
        $(document).ready(function () {
            $(".chzn-select").chosen({ search_contains: true });
            $(".sp_currency").numeric({ negative: false });
            $(".sp_number").numeric({ decimal: false, negative: false }, function () { alert("Positive integers only"); this.value = ""; this.focus(); });
            prth_mask_input.init();
        });
        $(document).ready(function () {
            $('#<%=GridGuardians.ClientID%>').fixedHeader({
                width: $(window).width() - 370, height: 210
            });
        });
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {

            $('#<%=GridGuardians.ClientID%>').fixedHeader({
                width: $(window).width() - 370, height: 210
            });
        });
        function gridviewScroll() {
            $('#<%=GridGuardians.ClientID%>').gridviewScroll({
                width: $(window).width() - 370,
                height: $(window).height() - 380,
                freezesize: 0,
                arrowsize: 30,
                headerrowcount: 1
            });
        }
    </script>
    <ajax:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </ajax:ToolkitScriptManager>
    <div class="row">
        <div class="twelve columns">
            <div class="box_c">
                <div class="box_c_heading  box_actions">
                    <p>
                        Assign Member to Group</p>
                </div>
                <div class="box_c_content">
                    <asp:Panel ID="Panel1" runat="server" DefaultButton="AddChit">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <div class="row" style="padding: 5px;">
                                    <br />
                                    <table class="tabbed" style="margin: 0 auto;">
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label1" runat="server" Text="Group"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList Width="200px" CssClass="chzn-select" ValidationGroup="add" AutoPostBack="true"
                                                    runat="server" ID="ddlGrpID" OnSelectedIndexChanged="ddlGrpID_SelectedIndexChanged"
                                                    TabIndex="1">
                                                </asp:DropDownList>
                                                <asp:CompareValidator ForeColor="Red" ValidationGroup="add" ID="CompareValidator2" runat="server"
                                                    Display="Dynamic" ControlToValidate="ddlGrpID" ErrorMessage="*" Operator="NotEqual"
                                                    ValueToCompare="0"></asp:CompareValidator>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label2" runat="server" Text="Token"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList onchange="clearValidationErrors();" Width="200px" CssClass="chzn-select" ValidationGroup="add" runat="server"
                                                    ID="ddlGrpMemberID" TabIndex="2">
                                                </asp:DropDownList>
                                                <asp:CompareValidator ForeColor="Red" ValidationGroup="add" ID="CompareValidator1" runat="server"
                                                    Display="Dynamic" ControlToValidate="ddlGrpMemberID" ErrorMessage="*" Operator="NotEqual"
                                                    ValueToCompare="--Select--"></asp:CompareValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label4" runat="server" Text="Member Name"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList onchange="clearValidationErrors();" Width="200px" CssClass="chzn-select" ValidationGroup="add" runat="server"
                                                    ID="ddlMemberName" TabIndex="3" OnSelectedIndexChanged="ddlMemberName_SelectedIndexChanged"
                                                    AutoPostBack="true">
                                                </asp:DropDownList>
                                                <asp:CompareValidator ForeColor="Red" ValidationGroup="add" ID="CompareValidator3" runat="server"
                                                    Display="Dynamic" ControlToValidate="ddlMemberName" ErrorMessage="*" Operator="NotEqual"
                                                    ValueToCompare="--Select--"></asp:CompareValidator>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label3" runat="server" Text="Card"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList CssClass="chzn-select" Width="200px" ValidationGroup="add" runat="server" ID="ddlNoCard"
                                                    TabIndex="4">
                                                    <asp:ListItem Text="--Select--" Value="--Select--"></asp:ListItem>
                                                    <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:CompareValidator ForeColor="Red" ValidationGroup="add" ID="CompareValidator4" runat="server"
                                                    Display="Dynamic" ControlToValidate="ddlNoCard" ErrorMessage="*" Operator="NotEqual"
                                                    ValueToCompare="--Select--"></asp:CompareValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            
                                            <td style="vertical-align:top !important;padding-top:5px;">
                                                <asp:Label ID="Label6" runat="server" Text="Date of Receipt"></asp:Label>
                                            </td>
                                            <td style="vertical-align:top !important;">
                                                <asp:TextBox runat="server" TabIndex="6" ID="txtReceiptDate" Width="200px" CssClass="input-text maskdate"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ErrorMessage="Enter Date"
                                                    ControlToValidate="txtReceiptDate" ValidationGroup="add" Display="Dynamic"
                                                    runat="server"></asp:RequiredFieldValidator>
                                                <asp:CompareValidator ForeColor="Red" Type="Date" ID="CompareValidator6" ValidationGroup="add"
                                                    ControlToValidate="txtReceiptDate" Display="Dynamic" Operator="DataTypeCheck"
                                                    runat="server" ErrorMessage="Incorrect!!!"></asp:CompareValidator>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <br />
                                <div class="tsc_accordion">
                                    <h2>
                                        Nominee Details
                                    </h2>
                                    <div style="margin: 0px auto; display: table;">
                                        <asp:GridView CssClass="aspxtable" Style="display: table-cell; margin: 0px auto;"
                                            ID="GridGuardians" runat="server" AutoGenerateColumns="False" BorderColor="#507CD1"
                                            BorderStyle="Solid" BorderWidth="1" Font-Names="Verdana" Font-Size="9pt" ForeColor="#333333"
                                            GridLines="None" OnRowCommand="GridGuardians_RowCommand" Width="100%" ShowHeader="true">
                                            <Columns>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <table style="border: none;">
                                                            <tr>
                                                                <td>
                                                                    <asp:ImageButton TabIndex="61" ID="imgBtnAdd" runat="server" CausesValidation="True"
                                                                        CommandName="Add" Height="24" ValidationGroup="GrpRow" ImageUrl="~/Images/gpls.png"
                                                                        ToolTip="Add New Transaction" Visible='<%# Eval("ShowAddButton")%>' Width="24" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:ImageButton OnClientClick="clearValidationErrors();" TabIndex="62" ID="imgBtnRemove" runat="server" CausesValidation="false"
                                                                        CommandName="Remove" Height="24" ImageUrl="~/Images/gminus.png" ToolTip="Remove New Transaction"
                                                                        Visible='<%# Eval("ShowRemoveButton")%>' Width="24" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Choose" ItemStyle-VerticalAlign="Top">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkNominee" Checked='<%# Eval("IsNominee")%>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Nominee Name" ItemStyle-VerticalAlign="Top">
                                                    <ItemTemplate>
                                                        <asp:TextBox TabIndex="59" Text='<%#Eval("NomineeName") %>' ValidationGroup="GrpRow"
                                                            CssClass="twitterStyleTextbox" ID="txtNomineeName" runat="server"> </asp:TextBox>
                                                        <asp:RequiredFieldValidator ForeColor="Red" Display="Dynamic" ValidationGroup="GrpRow" ControlToValidate="txtNomineeName"
                                                            ID="rfvtxtNomineeAge" ErrorMessage="*Required" runat="server"> </asp:RequiredFieldValidator>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Age" ItemStyle-VerticalAlign="Top">
                                                    <ItemTemplate>
                                                        <asp:TextBox TabIndex="59" Text='<%#Eval("NomineeAge") %>' ValidationGroup="GrpRow"
                                                            CssClass="twitterStyleTextbox sp_number" ID="txtNomineeAge" runat="server"> </asp:TextBox>
                                                        <asp:RequiredFieldValidator ForeColor="Red" Display="Dynamic" ValidationGroup="GrpRow" ControlToValidate="txtNomineeAge"
                                                            ID="rfvtxtNomineeName" ErrorMessage="*Required" runat="server"> </asp:RequiredFieldValidator>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Relation" ItemStyle-VerticalAlign="Top">
                                                    <ItemTemplate>
                                                        <asp:TextBox TabIndex="59" Text='<%#Eval("Relation") %>' ValidationGroup="GrpRow"
                                                            CssClass="twitterStyleTextbox" ID="txtNomineeRelation" runat="server"> </asp:TextBox>
                                                        <asp:RequiredFieldValidator ForeColor="Red" Display="Dynamic" ValidationGroup="GrpRow" ControlToValidate="txtNomineeRelation"
                                                            ID="rfvRelation" ErrorMessage="*Required" runat="server"> </asp:RequiredFieldValidator>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Address" ItemStyle-VerticalAlign="Top">
                                                    <ItemTemplate>
                                                        <br />
                                                        <asp:TextBox Style="margin-top: -22px" TextMode="MultiLine" Height="50" TabIndex="59"
                                                            Text='<%#Eval("NomineeAddress") %>' ValidationGroup="GrpRow" CssClass="twitterStyleTextbox"
                                                            ID="txtNomineeNomineeAddress" runat="server"> </asp:TextBox>
                                                        <asp:RequiredFieldValidator ForeColor="Red" Display="Dynamic" ValidationGroup="GrpRow" ControlToValidate="txtNomineeNomineeAddress"
                                                            ID="rfvNomineeNomineeAddress" ErrorMessage="*Required" runat="server"> </asp:RequiredFieldValidator>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Telephone Number" ItemStyle-VerticalAlign="Top">
                                                    <ItemTemplate>
                                                        <asp:TextBox TabIndex="59" Text='<%#Eval("NomineeTelephoneNo") %>' ValidationGroup="GrpRow"
                                                            CssClass="twitterStyleTextbox sp_number" ID="txtNomineeTelephoneNo" runat="server"> </asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Mobile Number">
                                                    <ItemTemplate>
                                                        <asp:TextBox TabIndex="59" Text='<%#Eval("NomineeMobileNo") %>' ValidationGroup="GrpRow"
                                                            CssClass="twitterStyleTextbox sp_number" ID="txtNomineeMobileNo" runat="server"> </asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </asp:Panel>
                    <br />
                    <div style="float: right;">
                        <asp:Button Width="100" ID="AddChit" ValidationGroup="add" runat="server" CssClass="GreenyPushButton"
                            Text="Assign" OnClick="AddChit_Click" />
                    </div>
                    <br />
                    <br />
                    <br />
                </div>
                <ajax:ModalPopupExtender ID="ModalPopupExtender1" TargetControlID="show" PopupControlID="msgbox"
                    BackgroundCssClass="modalBackground" runat="server">
                </ajax:ModalPopupExtender>
                <asp:LinkButton ID="show" runat="server"></asp:LinkButton>
                <div id="msgbox" style="display: none; background: #fff; border: 4px solid #ccc;
                    border: 8px solid rgba(0, 0, 0, 0.5); background-clip: padding-box; -webkit-border-radius: 10px;
                    -moz-border-radius: 10px; border-radius: 10px; min-width: 300px; max-width: 900px;"
                    cssclass="raised" runat="server">
                    <div class=" box_c_heading">
                        <span class="inner_heading" style="text-align: center;">Confirmation </span>
                    </div>
                    <div style="min-height: 20px; overflow: hidden; max-width: 880px; padding: 20px;">
                        <div style="min-height: 20px; overflow: auto; max-width: 840px;">
                            <asp:Label runat="server" Style="display: none" ID="lblHint"></asp:Label>
                            <asp:Label runat="server" ID="lblMsgInfoContent"></asp:Label>
                            <br />
                        </div>
                    </div>
                    <div class=" box_c_heading">
                        <div style="float: right;">
                            <asp:Button ID="btnInfo_yes" CssClass="GreenyPushButton" OnClick="btnInfo_yes_Click"
                                runat="server" Style="margin: 0px auto;" Text="Ok"></asp:Button>
                        </div>
                    </div>
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
        $(document).ready(function () {
            $('#<%=GridGuardians.ClientID%>').fixedHeader({
                width: $(window).width() - 370, height: 210
            });
        });
   </script>
</asp:Content>
