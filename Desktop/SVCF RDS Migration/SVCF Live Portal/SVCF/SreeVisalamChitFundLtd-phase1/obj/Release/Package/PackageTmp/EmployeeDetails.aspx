<%@ Page Title="Employee Details" Language="C#" MasterPageFile="~/Branch.Master"
    AutoEventWireup="true" CodeBehind="EmployeeDetails.aspx.cs" Inherits="SreeVisalamChitFundLtd_phase1.EmployeeDetails" %>
<%--<%@ Register TagName="UsrLed" TagPrefix="Revert" Src="~/UserControl/AddNewDesignation.ascx" %>--%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>

<asp:Content ID="Head1" ContentPlaceHolderID="cphHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMainContent" runat="server">
    <link href="select2-3.4.2/select2.css" rel="stylesheet" type="text/css" />
    <script src="select2-3.4.2/select2.js" type="text/javascript"></script>
    <style type="text/css">
        .chzn-drop
        {
            border: 1px solid #aaa !important;
            padding-top: 5px !important;
            width: 240px !important;
        }
        .chzn-results
        {
            text-align:center;
        }
        #ctl00_cphMainContent_ddlempID_chzn .chzn-drop .chzn-search input[type="text"]
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
      <div class="row">
      <table>
        <tr>
         <%-- <td> <asp:RadioButton ID="AssiggnMembertogroup" runat="server" Text="AssiggnMembertogroup" oncheckedchanged="AssiggnMembertogroup_CheckedChanged" AutoPostBack="True" /></td>--%>

        <%--     <td> <asp:RadioButton ID="Employeedeatils" runat="server" Text="Employeedeatils" OnCheckedChanged="Employeedeatils_CheckedChanged" AutoPostBack="True" /></td>
          <td> <asp:RadioButton ID="AddNewDesignation" runat="server" Text="AddNewDesignation" 
                  AutoPostBack="True" oncheckedchanged="AddNewDesignation_CheckedChanged" /> </td>--%>
        </tr>       
      </table>
    </div>
    <ajax:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </ajax:ToolkitScriptManager>
    <div class="row">
        <div class="twelve columns">
            <div class="box_c">
                <div class="box_c_heading  box_actions">
                    <p>
                        Employee Addition</p>
                </div>
                <div class="box_c_content">
                    <div class="row">
                        <asp:Panel runat="server" DefaultButton="btnEmployee">
                            <div style="width: 100%;">
                                <table style="margin: 0px auto;">
                                    <tr>
                                        <td style="vertical-align: top; padding-top: 6px; padding-right: 6px;">
                                            <asp:Label ID="label2" runat="server" Text="Employee Name"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtemp_Name" TabIndex="1" runat="server" ToolTip="Ex. Sudhakar"
                                                placeholder="Employee Name" CssClass="input-text "></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="add" runat="server"
                                                Display="Dynamic" ControlToValidate="txtemp_Name" ErrorMessage="Enter Employee Name"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="vertical-align: top; padding-top: 6px; padding-right: 6px;">
                                            <asp:Label ID="label1" runat="server" Text="SR Number"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtSrNumber" TabIndex="2" runat="server" placeholder="SR Number" CssClass="input-text "></asp:TextBox>                                         
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="vertical-align: top; padding-top: 6px; padding-right: 6px;">
                                            <asp:Label ID="label7" runat="server" Text="Date of Joining"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtDateofJoining" TabIndex="3" runat="server" CssClass="twitterStyleTextbox maskdate" onchange="CheckDate();"
                                                autoComplete="off" ></asp:TextBox>              
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="add" Display="Dynamic"
                                                runat="server" ControlToValidate="txtDateofJoining" ErrorMessage="Enter Date of Joining"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="vertical-align: top; padding-top: 46px; padding-right: 6px;">
                                            <asp:Label ID="label3" runat="server" Text="Address"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtemp_Address" TabIndex="4" runat="server" TextMode="MultiLine"
                                                placeholder="Address" ToolTip="Ex. 196/23, Thiruvenkatasamy Road (West),<br/> R.S.Puram,<br/>  Coimbatore - 641002."
                                                CssClass="input-text "></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="add" Display="Dynamic"
                                                runat="server" ControlToValidate="txtemp_Address" ErrorMessage="Enter Employee Address"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="vertical-align: top; padding-top: 6px; padding-right: 6px;">
                                            <asp:Label ID="label6" runat="server" Text="Designation"></asp:Label>
                                        </td>
                                        <td>
                                           <asp:DropDownList ID="DD_GP" runat="server"  CssClass="chzn-select" 
                             Width="200px" AutoPostBack="True" >
                             </asp:DropDownList>
                                          <%--  <asp:TextBox ID="txtEmp_Designation" TabIndex="5" runat="server" ToolTip="Ex. Money Collector"
                                                placeholder="Designation" CssClass="input-text "></asp:TextBox>
                                            <asp:RequiredFieldValidator ValidationGroup="add" ID="RequiredFieldValidator4" runat="server"
                                                Display="Dynamic" ControlToValidate="txtEmp_Designation" ErrorMessage="Enter Designation"></asp:RequiredFieldValidator>--%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="vertical-align: top; padding-top: 6px; padding-right: 6px;">
                                            <asp:Label ID="label8" runat="server" Text="E-mail ID"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtEmail" runat="server" TabIndex="6" placeholder="E-mail-ID" ToolTip="Ex. unknown@unknown.com"
                                                CssClass="input-text "></asp:TextBox>
                                                           </td>
                                    </tr>
                                    <tr>
                                        <td style="vertical-align: top; padding-top: 6px; padding-right: 6px;">
                                            <asp:Label ID="label4" runat="server" Text="Phone Number"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtemp_PhoneNo" TabIndex="7" runat="server" placeholder="Phone Number"
                                                ToolTip="Ex. 0422-7575757 or 9876543210" CssClass="input-text "></asp:TextBox>
                                            <asp:RequiredFieldValidator ValidationGroup="add" ID="RequiredFieldValidator6" runat="server"
                                                Display="Dynamic" ControlToValidate="txtemp_PhoneNo" ErrorMessage="Enter Phone No."></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" ControlToValidate="txtemp_PhoneNo"
                                                Display="Dynamic" ValidationExpression="(\+91)?\d{2,}(-)?\d{6,}" runat="server"
                                                ErrorMessage="Invalid Phone No."></asp:RegularExpressionValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="vertical-align: top; padding-top: 6px; padding-right: 6px;">
                                            <asp:Label ID="label5" runat="server" Text="Salary(In Months)"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtemp_Salary" TabIndex="8" runat="server" placeholder="Salary"
                                                ToolTip="Ex. 10000.00" CssClass="input-text  sp_float"></asp:TextBox>
                                            <asp:RequiredFieldValidator ValidationGroup="add" ID="RequiredFieldValidator5" runat="server"
                                                Display="Dynamic" ControlToValidate="txtemp_Salary" ErrorMessage="Enter Salary"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                            <asp:Button Style="margin: 0 auto" TabIndex="9" ID="btnEmployee" Text="Add" ValidationGroup="add"
                                                runat="server" CssClass="GreenyPushButton" OnClick="btnEmployee_Click"></asp:Button>
                                            <asp:Button Style="margin: 0 auto" TabIndex="8" ID="Button1" Text="Cancel" runat="server"
                                                CssClass="GreenyPushButton" OnClientClick="clearValidationErrors();" OnClick="btn_Yes_Click">
                                            </asp:Button>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </asp:Panel>
                    </div>
                    <asp:Panel CssClass="raised" ID="Pnlmsg" runat="server" Visible="false" Width="600px"
                        Style="min-height: 100px">
                        <asp:Label runat="server" ID="lblHint" Text="" Visible="false"> </asp:Label>
                        <div style="top: 0px; height: 50px; text-align: center; padding: auto 0; width: 100%"
                            class="boxfooter">
                            <asp:Label ID="lblHeading" runat="server" Text=""> </asp:Label>
                        </div>
                        <div id="Div1" style="min-height: 100px; text-align: center;">
                            <br />
                            <br />
                            <asp:Label ID="lblContent" runat="server" Text=""> </asp:Label>
                            <br />
                            <br />
                            <asp:PlaceHolder ID="DynamicControlsHolder" runat="server"></asp:PlaceHolder>
                        </div>
                        <div class="boxheader">
                            <div style="margin: 0 auto;">
                                <asp:Button OnClick="btn_Yes_Click" CssClass="GreenyPushButton" ID="btn_Yes" runat="server"
                                    Text="Ok" />
                            </div>
                        </div>
                    </asp:Panel>
                    <ajax:ModalPopupExtender ID="ModalPopupExtender1" BackgroundCssClass="modalBackground"
                        TargetControlID="btnEmployee" PopupControlID="Pnlmsg" runat="server">
                    </ajax:ModalPopupExtender>
                </div>
            </div>
        </div>
    </div>
 <%--     <div id="divRevert" runat="server" visible="false" class="row" >
    <div class="box_c_heading  box_actions">
       <p>Revert Option ForNewGroup</p>
    </div>
    <div>
  <Revert:UsrLed ID="UCLedger" runat="server" />
    </div>
 </div>--%>
    <script type="text/javascript">
        $(document).ready(function () {
            $(".chzn-select").chosen({ search_contains: true });
        });
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $(".chzn-select").chosen({ search_contains: true });
            $(".sp_float").numeric({
                negative: false
            }, function () {
                alert("Positive only");
                this.value = "";
                this.focus()
            });
        });

        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $(".chzn-select").chosen({ search_contains: true });
            $(".sp_float").numeric({
                negative: false
            }, function () {
                alert("Positive only");
                this.value = "";
                this.focus()
            });
        });

        function checkdate(txt) {
            $('#<%=txtDateofJoining.ClientID%>').val(txt.value);
        }

    </script>

    <script type="text/javascript">
        function CheckDate() {
            var inputDate = $('#<%=txtDateofJoining.ClientID %>').val();
            var Reg_Expression = /^\d{1,2}\/\d{1,2}\/\d{4}$/;
            if (Reg_Expression.test(inputDate)) {
                var partitionedDate = inputDate.split('/');
                var nDay = parseInt(partitionedDate[0], 10);
                var nMonth = parseInt(partitionedDate[1], 10);
                var nYear = parseInt(partitionedDate[2], 10);
                var dDate = new Date(nYear, nMonth - 1, nDay);
                if ((dDate.getFullYear() == nYear) && (dDate.getMonth() == nMonth - 1) && (dDate.getDate() == nDay)) {
                }
                else {
                    document.getElementById('<%=txtDateofJoining.ClientID %>').value = "";
                    alert('Incorrect Date Format..');
                }
            }
            else {
                document.getElementById('<%=txtDateofJoining.ClientID %>').value = "";
                alert('Incorrect Date Format..');
            }
        }
    </script>

</asp:Content>
