<%@ Page Title="" Language="C#" MasterPageFile="~/Branch.Master" AutoEventWireup="true" CodeBehind="Addcustomerbank.aspx.cs" Inherits="SreeVisalamChitFundLtd_phase1.Addcustomerbank" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMainContent" runat="server">
   
    <div class="row">
        <div class="twelve columns">
            <div class="box_c">
                <div class="box_c_heading  box_actions">
                    <p>
                        Customer Bank Addition
                    </p>
                </div>

                <div class="box_c_heading  box_actions">
                    <p>
                        Add Customer Bank
                    </p>
                </div>
                <div class="box_c_content">
                    <div class="row">
                        <br />

                        <asp:Panel runat="server" DefaultButton="btnParentInsert">
                            <table cellpadding="10px" cellspacing="10px" style="margin: 0 auto;">

                                <tr>
                                    <td>
                                        <asp:Label ID="Label8" runat="server" Text="Customer Bank"></asp:Label>

                                    </td>
                                    <td style="padding-left: 10px;" class="auto-style1">
                                        <asp:TextBox placeholder="Bank Name" CssClass="input-text" ID="txtbank_name" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator Display="Dynamic" Text="Enter customer bank name" runat="server"
                                            ValidationGroup="Parent" ControlToValidate="txtbank_name"></asp:RequiredFieldValidator>
                                    </td>
                                    <td style="padding-left: 10px;" class="auto-style1"></td>
                                </tr>

                                <tr>
                                    <td></td>
                                    <td class="auto-style1">
                                        <asp:Button OnClick="btnbankInsert_OnClick" TabIndex="3" Text="Insert" Style="margin: 0 auto;"
                                            ValidationGroup="Parent" ID="btnParentInsert" runat="server" CssClass="GreenyPushButton"></asp:Button>
                                        <asp:Button OnClientClick="clearValidationErrors();" TabIndex="4" Text="Cancel" OnClick="btnNo_Click"
                                            CausesValidation="false" CssClass="GreenyPushButton" ID="Button4" runat="server"></asp:Button>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                         <asp:Label ID="lblContent" runat="server" Text=""> </asp:Label>                  
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
