<%@ Page Title="" Language="C#" MasterPageFile="~/Branch.Master" AutoEventWireup="true" CodeBehind="AddNewDesignation.aspx.cs" Inherits="SreeVisalamChitFundLtd_phase1.AddNewDesignation" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
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
                                            <asp:TextBox ID="txtemp_Des" TabIndex="1" runat="server" ToolTip="Office Assistant"
                                                placeholder="Employee Des" CssClass="input-text "></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="add" runat="server"
                                                Display="Dynamic" ControlToValidate="txtemp_Des" ErrorMessage="Enter Employee Designation"></asp:RequiredFieldValidator>
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

   

    </script>

    
</asp:Content>
