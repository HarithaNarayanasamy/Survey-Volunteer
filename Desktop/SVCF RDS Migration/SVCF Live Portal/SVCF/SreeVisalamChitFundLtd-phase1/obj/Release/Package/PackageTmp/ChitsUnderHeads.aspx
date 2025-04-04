<%@ Page Title="" Language="C#" MasterPageFile="~/Branch.Master" AutoEventWireup="true"
    CodeBehind="ChitsUnderHeads.aspx.cs" Inherits="SreeVisalamChitFundLtd_phase1.ChitsUnderHeads" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMainContent" runat="server">
    
    <ajax:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </ajax:ToolkitScriptManager>
    <div class="row">
        <div class="twelve columns">
            <div class="box_c">
                <div class="box_c_heading  box_actions">
                    <p>
                        Chits Under Particular Heads</p>
                </div>
                <div class="box_c_content">
                    <div class="row">
                        <asp:Panel ID="Panel1" DefaultButton="btnAdd" runat="server">
                            <div style="width: 100%;">
                                <table cellspacing="3" style="margin: 0 auto;">
                                    <tr>
                                        <td style="vertical-align: top; padding-top: 6px; padding-right: 6px;">
                                            <asp:Label ID="Label1" runat="server" Text="Heads"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:DropDownList TabIndex="1" ID="ddlHeads" CssClass="chzn-select" Width="240px"
                                                runat="server">
                                                <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                <asp:ListItem Text="Chit Collection to be Accounted" Value="43"></asp:ListItem>
                                                <asp:ListItem Text="Removed Chit members Account – I" Value="44"></asp:ListItem>
                                                <asp:ListItem Text="Removed Chit members Account – II" Value="45"></asp:ListItem>
                                                <asp:ListItem Text="Balance in Decree A/c, Court cost & Advocate Fees" Value="51"></asp:ListItem>
                                                <asp:ListItem Text="Chit Loan" Value="53"></asp:ListItem>
                                                <asp:ListItem Text="Sundry Creditors" Value="58"></asp:ListItem>
                                                <asp:ListItem Text="Degree Advance" Value="59"></asp:ListItem>
                                                <asp:ListItem Text="Advocate Advance" Value="60"></asp:ListItem>
                                                <asp:ListItem Text="Sundry Debtors" Value="1061"></asp:ListItem>
                                                <%--<asp:ListItem Text="Accrued Interest on Loan" Value="2076"></asp:ListItem>--%>
                                            </asp:DropDownList>
                                            <asp:CompareValidator ForeColor="Red" ValidationGroup="group" ControlToValidate="ddlHeads"
                                                ValueToCompare="0" Operator="NotEqual" Display="Dynamic" ID="CompareValidator2"
                                                runat="server" ErrorMessage="Select Head"></asp:CompareValidator>
                                        </td>
                                    </tr>
                                      <tr>
                                             <td style="vertical-align: top; padding-top: 6px; padding-right: 6px;">
                                            <asp:Label ID="lblbranch" runat="server" Text="Branch"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlbranch" Width="150px" CssClass="chzn-select" AutoPostBack="true" 
                                  runat="server" OnSelectedIndexChanged="ddlbranch_SelectedIndexChanged"></asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="vertical-align: top; padding-top: 6px; padding-right: 6px;">
                                            <asp:Label ID="Label2" runat="server" Text="Chit Number"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:DropDownList onchange="getname();"
                                                TabIndex="2" ID="ddlChitNumber" CssClass="chzn-select" Width="240px" runat="server">
                                            </asp:DropDownList>
                                            <asp:CompareValidator ForeColor="Red" ValidationGroup="group" ControlToValidate="ddlChitNumber"
                                                ValueToCompare="0" Operator="NotEqual" Display="Dynamic" ID="CompareValidator1"
                                                runat="server" ErrorMessage="Select Chit"></asp:CompareValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="vertical-align: top; padding-top: 6px; padding-right: 6px;">
                                            <asp:Label ID="Label3" runat="server" Text="Member Name"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox 
                                                TabIndex="3" ID="txtName" CssClass="input-text" runat="server">
                                            </asp:TextBox>
                                            <asp:RequiredFieldValidator ForeColor="Red"  ID="RequiredFieldValidator6" ErrorMessage="*"
                                                ControlToValidate="txtName" ValidationGroup="Generate" Display="Dynamic" runat="server"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="vertical-align: top; padding-top: 6px; padding-right: 6px;">
                                            <asp:Label ID="Label4" runat="server" Text="Chit Number"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox 
                                                TabIndex="3" ID="txtChitNumber" CssClass="input-text" runat="server">
                                            </asp:TextBox>
                                            <asp:RequiredFieldValidator ForeColor="Red"  ID="RequiredFieldValidator1" ErrorMessage="*"
                                                ControlToValidate="txtChitNumber" ValidationGroup="Generate" Display="Dynamic" runat="server"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td style="padding-top: 10px;">
                                            <asp:Button TabIndex="8" ID="btnAdd" Text="Add" ValidationGroup="BranchValid" runat="server"
                                                CssClass="GreenyPushButton" Style="margin: 0px auto;" OnClick="btnAdd_Click">
                                            </asp:Button>
                                            <asp:Button TabIndex="9" ID="btnCancel" Style="margin: 0px auto;" Text="Cancel" OnClick="btnCancel_Click"
                                                OnClientClick="clearValidationErrors();" CausesValidation="false" runat="server"
                                                CssClass="GreenyPushButton"></asp:Button>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </asp:Panel>
                    </div>
                </div>
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
                    <div style="min-height: 100px; text-align: center;">
                        <br />
                        <br />
                        <asp:Label runat="server" ID="lblContent" Text=""> </asp:Label>
                        <br />
                        <br />
                    </div>
                    <div class="boxheader">
                        <div style="margin: 0 auto;">
                            <asp:Button Style="margin: 0 auto" CssClass="GreenyPushButton" OnClick="btnCancel_Click"
                                CausesValidation="false" ID="BtnOK" runat="server" Text="Ok"></asp:Button>
                        </div>
                    </div>
                </asp:Panel>
            </div>
        </div>
    </div>
    <%-- <script src="Scripts/jquery-1.8.3.min.js"></script>--%>
    <script type="text/javascript">

        $(document).ready(function () {
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_endRequest(function () {
                $(".chzn-select").chosen({ search_contains: true });
                $(".sp_currency").numeric({ negative: false });
                $(".sp_number").numeric({ decimal: false, negative: false }, function () { alert("Positive integers only"); this.value = ""; this.focus(); });
                //            $(".sp_number").spinner({min: 0,
                //                numberFormat: "d",
                //                culture:"en-GB"
                //            });
                prth_mask_input.init();
                //validatorOverrideScripts();
            });
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

        function getname() {
            
            var chitmemtxt = $("#<%=ddlChitNumber.ClientID%>").find("option:selected").text(); //id name for dropdown list    
            
            chitmem = chitmemtxt.split('|')[1];


            var chitmemtxt = $("#<%=txtName.ClientID%>");

             chitmemtxt.val(chitmem);


            var chitmemval=$("#<%=ddlChitNumber.ClientID%>").find("option:selected").val(); //id name for dropdown list    


            var memid = chitmemval.split('|')[1];
            debugger;
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "ChitsUnderHeads.aspx/getmembername",
                data: JSON.stringify({ MemberID: memid }),
                dataType: "json",
                success: function (data) {
                    
                    var msg = data.d.toString();
                    //alert(msg);
                    var txttoken = $("#<%=txtChitNumber.ClientID%>");
                    txttoken.val(msg);
                },
                error: function (ex) {
                    // alert('Failed to retrieve states.' + ex);
                }
            });

            $("#<%=ddlChitNumber.ClientID%>").addClass('chzn-select');
            $("#<%=ddlHeads.ClientID%>").addClass('chzn-select');
        }
</script>
</asp:Content>
