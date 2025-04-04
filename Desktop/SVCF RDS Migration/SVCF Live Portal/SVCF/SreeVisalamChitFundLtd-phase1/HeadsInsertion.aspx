<%@ Page Language="C#" MasterPageFile="~/Branch.Master" AutoEventWireup="true" CodeBehind="HeadsInsertion.aspx.cs"
    Inherits="SreeVisalamChitFundLtd_phase1.HeadsInsertion" Title="SVCF Admin Panel" %>

<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxTabControl" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxMenu" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1.Export, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView.Export" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxClasses" TagPrefix="dx" %>
<asp:Content ID="Head1" ContentPlaceHolderID="cphHead" runat="server">
    <title>SVCF Admin Panel</title>
    <script type="text/javascript">
        function clearValidationErrors() {
            if (window.Page_Validators)
                for (var vI = 0; vI < Page_Validators.length; vI++) {
                    var vValidator = Page_Validators[vI];
                    vValidator.isvalid = true;
                    ValidatorUpdateDisplay(vValidator);
                }
            if (typeof (Page_ValidationSummaries) != "undefined") {
                for (sums = 0; sums < Page_ValidationSummaries.length; sums++) {
                    summary = Page_ValidationSummaries[sums];
                    summary.style.display = "none";
                }
            }
        }

      

        function Getempname() {
            var empname = $("#<%=ddlLoan.ClientID%>").find("option:selected").text();
            $("#<%= Membername.ClientID %>").val(empname);
        }


    </script>
    <<style type="text/css">
         .chzn-drop {
             border: 1px solid #aaa !important;
             padding-top: 5px !important;
             width: 254px !important;
         }

         .chzn-results {
             text-align: center;
         }

         #ctl00_cphMainContent_carTabPage_ddlMaster_chzn .chzn-drop .chzn-search input[type="text"] {
             height: 15px;
         }
     </style>
    <style type="text/css">
        .auto-style1 {
            width: 270px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMainContent" runat="server">
    <ajax:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </ajax:ToolkitScriptManager>


    <div class="row">
        <div class="twelve columns">
            <div class="box_c">
                <div class="box_c_heading  box_actions">
                    <p>
                        Heads Insertion
                    </p>
                </div>
                <div class="box_c_content">
                    <div class="row">
                        <br />

                        <asp:Panel runat="server" DefaultButton="btnChildInsert">
                            <table cellpadding="10px" cellspacing="10px" style="margin: 0 auto;">
                                <tr>
                                    <td>
                                        <asp:Label ID="Label1" runat="server" Text="Choose Master Head"></asp:Label>
                                    </td>
                                    <td style="padding-left: 10px;" class="auto-style1">
                                        <asp:DropDownList Width="240px" TabIndex="1"
                                            ID="ddlMaster" OnSelectedIndexChanged="ddlMaster_SelectedIndexChanged" CssClass="chzn-search" AutoPostBack="true" runat="server">
                                        </asp:DropDownList>
                                        <asp:CompareValidator ValidationGroup="Child" ID="CompareValidator11" ValueToCompare="--Select--"
                                            SetFocusOnError="true" ControlToValidate="ddlMaster" Display="Dynamic" Operator="NotEqual"
                                            runat="server" ErrorMessage="Invalid Master Head"></asp:CompareValidator>
                                    </td>
                                    <td style="padding-left: 10px;" class="auto-style1">
                                        <asp:DropDownList Width="240px" TabIndex="1"
                                            ID="ddlLoan" CssClass="chzn-search" OnChange="Getempname();" runat="server">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label12" runat="server" Text="Member Name"></asp:Label>

                                    </td>
                                    <td style="padding-left: 10px;" class="auto-style1">
                                        <asp:TextBox placeholder="Member Name" CssClass="input-text" ID="Membername" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="vertical-align: top; padding-top: 8px;">
                                        <asp:Label ID="Label2" runat="server" Text="Child Head"></asp:Label>
                                    </td>
                                    <td style="padding-left: 10px;" class="auto-style1">
                                        <asp:TextBox placeholder="Child Head" CssClass="input-text" TabIndex="2" ValidationGroup="Child"
                                            ID="txtChildHead" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator Display="Dynamic" Text="Enter Child Head" runat="server"
                                            ValidationGroup="Child" ControlToValidate="txtChildHead"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="vertical-align: top; padding-top: 8px;">
                                        <asp:Label ID="lblqty" runat="server" Text="Quantity" Visible="false"></asp:Label>
                                    </td>
                                    <td style="padding-left: 10px;" class="auto-style1">
                                        <asp:TextBox placeholder="Quantity" CssClass="input-text" TabIndex="3"
                                            ID="txtqty" runat="server" Width="60px" Visible="false"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td class="auto-style1">
                                        <asp:Button OnClick="btnChildInsert_OnClick" TabIndex="3" Text="Insert" Style="margin: 0 auto;"
                                            ValidationGroup="Child" ID="btnChildInsert" runat="server" CssClass="GreenyPushButton"></asp:Button>
                                        <asp:Button OnClientClick="clearValidationErrors();" TabIndex="4" Text="Cancel" OnClick="btnNo_Click"
                                            CausesValidation="false" CssClass="GreenyPushButton" ID="Button2" runat="server"></asp:Button>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>

                        <asp:LinkButton Text="" runat="server" ID="btnShowPopup"></asp:LinkButton>
                        <ajax:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="btnShowPopup"
                            PopupControlID="pnlpopup" BackgroundCssClass="modalBackground">
                        </ajax:ModalPopupExtender>
                        <asp:Panel CssClass="raised" ID="pnlpopup" runat="server" Width="600px" Style="min-height: 100px; text-align: center;">
                            <asp:Label runat="server" ID="lblHint" Text="" Visible="false"> </asp:Label>
                            <div style="top: 0px; height: 50px; text-align: center; padding: auto 0; width: 100%"
                                class="boxheader">
                                <asp:Label ID="lblHeading" runat="server" Text=""> </asp:Label>
                            </div>
                            <div id="content" style="min-height: 100px; text-align: center; vertical-align: middle;">
                                <br />
                                <br />
                                <asp:Label ID="lblContent" runat="server" Text=""> </asp:Label>
                                <br />
                                <br />
                                <asp:PlaceHolder ID="DynamicControlsHolder" runat="server"></asp:PlaceHolder>
                            </div>
                            <div class="boxheader">
                                <div style="margin: 0 auto;">
                                    <asp:Button Style="margin: 0 auto" CssClass="GreenyPushButton" ID="btnYes" OnClick="btnYes_Click"
                                        runat="server" Text="Ok" />
                                    <asp:Button Style="margin: 0 auto" CssClass="GreenyPushButton" ID="btnNo" Visible="false"
                                        OnClick="btnNo_Click" runat="server" Text="Ok" />
                                </div>
                            </div>
                        </asp:Panel>
                    </div>
                </div>

                <div class="box_c_heading  box_actions">
                    <p>
                        Branch Suboffice Insertion
                    </p>
                </div>

                <div class="box_c_content">
                    <div class="row">
                        <br />

                        <asp:Panel runat="server" DefaultButton="branchsuboffice">
                            <table cellpadding="10px" cellspacing="10px" style="margin: 0 auto;">
                                <tr>
                                    <td>
                                        <asp:Label ID="Label3" runat="server" Text="Office Name"></asp:Label>
                                    </td>
                                    <td style="padding-left: 10px;" class="auto-style1">
                                        <asp:TextBox ID="offinme" runat="server" CssClass="input-text"></asp:TextBox>
                                        <asp:RequiredFieldValidator Display="Dynamic" Text="Enter OfficeName" runat="server"
                                            ValidationGroup="brins" ControlToValidate="offinme"></asp:RequiredFieldValidator>
                                    </td>
                                    <td style="padding-left: 10px;" class="auto-style1"></td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label4" runat="server" Text="Purpose"></asp:Label>

                                    </td>
                                    <td style="padding-left: 10px;" class="auto-style1">
                                        <asp:TextBox CssClass="input-text" ID="Purposeid" runat="server"></asp:TextBox>                                    
                                    </td>
                                </tr>
                                <tr>
                                    <td style="vertical-align: top; padding-top: 8px;">
                                        <asp:Label ID="Label5" runat="server" Text="Residential Address"></asp:Label>
                                    </td>
                                    <td style="padding-left: 10px;" class="auto-style1">
                                        <asp:TextBox CssClass="input-text" TabIndex="2" ID="resaddid" runat="server"></asp:TextBox>                                
                                    </td>
                                </tr>
                                <tr>
                                    <td style="vertical-align: top; padding-top: 8px;">
                                        <asp:Label ID="Label6" runat="server" Text="Office Address" Visible="True"></asp:Label>
                                    </td>
                                    <td style="padding-left: 10px;" class="auto-style1">
                                        <asp:TextBox CssClass="input-text" TabIndex="3"
                                            ID="offaddid" runat="server"></asp:TextBox>                                     
                                    </td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td class="auto-style1">
                                        <asp:Button OnClick="branchsubofficeInsert_OnClick" TabIndex="3" Text="Insert" Style="margin: 0 auto;"
                                            ValidationGroup="brins" ID="branchsuboffice" runat="server" CssClass="GreenyPushButton"></asp:Button>
                                        <asp:Button OnClientClick="clearValidationErrors();" TabIndex="4" Text="Cancel" OnClick="btnNo_Click"
                                            CausesValidation="false" CssClass="GreenyPushButton" ID="Button3" runat="server"></asp:Button>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </div>
                </div>             
            </div>
        </div>
    </div>

    <script type="text/javascript">
        $(document).ready(function () {
            $(".chzn-select").chosen({ search_contains: true });
        });
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $(".chzn-select").chosen({ search_contains: true });
        });

    </script>
</asp:Content>
