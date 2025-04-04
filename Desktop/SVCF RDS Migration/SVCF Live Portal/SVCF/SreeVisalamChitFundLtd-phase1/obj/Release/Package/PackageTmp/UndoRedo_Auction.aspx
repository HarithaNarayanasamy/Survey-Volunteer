<%@ Page Language="C#" MasterPageFile="~/Branch.Master" AutoEventWireup="true" CodeBehind="UndoRedo_Auction.aspx.cs"
    Inherits="SreeVisalamChitFundLtd_phase1.UndoRedo_Auction" Title="UndoRedo_Auction" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMainContent" runat="server">
    <div class="row" id="Panel1">
        <div class="twelve columns">
            <div class="box_c">
                <div class="box_c_heading  box_actions">
                    <p>
                        Voucher Details</p>
                </div>
                <div class="box_c_content">
                    <div class="row">
                        <div>
                            <table style="margin: 0 auto;">
                                <tr>
                                    <td style="padding-right: 10px;">
                                        <asp:Label ID="Label1" runat="server" Text="Choose Chit Group "></asp:Label>
                                    </td>
                                    <td style="padding-right: 10px;">
                                        <asp:DropDownList Width="100" ID="ddlGroupNo" CssClass="chzn-select" runat="server">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:Button ID="btnload" runat="server" CssClass="GreenyPushButton" Text="Undoaction"
                                            OnClick="btnload_Click" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <ajax:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </ajax:ToolkitScriptManager>
        <ajax:ModalPopupExtender ID="ModalPopupExtender1" TargetControlID="show" PopupControlID="Pnlundo"
            runat="server">
        </ajax:ModalPopupExtender>
        <asp:LinkButton ID="show" runat="server"></asp:LinkButton>
        <asp:Panel CssClass="raised" ID="Pnlundo" runat="server" Visible="false" Width="300px"
            Style="min-height: 100px">
            <asp:Label runat="server" ID="lblHint" Text="" Visible="false"> </asp:Label>
            <div style="top: 0px; height: 50px; text-align: center; padding: auto 0; width: 100%"
                class="boxfooter">
                <asp:Label ID="lblHeading" runat="server" Text=""> </asp:Label>
            </div>
            <div style="min-height: 100px; overflow: auto; width: 100%;text-align:center;">
                <br />
                <br />
                <asp:Label ID="lblContent" runat="server" Text="" Style="text-align: justify; vertical-align: middle;
                    margin-left: 10px"> </asp:Label>
                <br />
                <br />
                <asp:PlaceHolder ID="DynamicControlsHolder" runat="server"></asp:PlaceHolder>
            </div>
            <div class="boxfooter" style="width: 100%; bottom: 0px; height: 50px;">
                <div >
                    <asp:Button Width="100" OnClick="btnundo_Click" Style="margin: 0 auto" CssClass="GreenyPushButton"
                        ID="btnyes" runat="server" Text="Yes" />
                    <asp:Button Width="100" OnClick="btnNo_Click" Style="margin: 0 auto" CssClass="GreenyPushButton"
                        ID="btnNo" runat="server" Text="No" />
                </div>
            </div>
        </asp:Panel>
    </div>
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
