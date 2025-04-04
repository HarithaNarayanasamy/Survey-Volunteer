<%@ Page Title="" Language="C#" MasterPageFile="~/Branch.Master" AutoEventWireup="true"
    CodeBehind="Action_form.aspx.cs" Inherits="SreeVisalamChitFundLtd_phase1.Action_form" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Head1" ContentPlaceHolderID="cphHead" runat="server">
    <style type="text/css">
        .raised
        {
            background-color: #FFFFFF;
            box-shadow: 0px 15px 10px -10px rgba(0, 0, 0, 0.5), 0px 1px 4px rgba(0, 0, 0, 0.3), 0px 0px 40px rgba(0, 0, 0, 0.1) inset;
        }
        
        .boxheader
        {
            -moz-border-bottom-colors: none;
            -moz-border-left-colors: none;
            -moz-border-right-colors: none;
            -moz-border-top-colors: none;
            background-color: #97B6B9;
            background-image: linear-gradient(rgba(10, 60, 150, 0.8), rgba(10, 60, 150, 0.8));
            border-color: rgba(10, 60, 150, 0.8) rgba(10, 60, 150, 0.8) rgba(10, 60, 150, 0.8);
            border-image: none;
            border-radius: 0.35em 0.35em 0 0;
            border-style: solid;
            border-width: 1px;
            box-shadow: 0 0.1em 0.1em rgba(255, 255, 255, 0.3) inset;
            color: rgba(255, 255, 255, 0.85);
            text-shadow: 0 -0.08em 0 #073D3D;
        }
        
        .boxfooter
        {
            -moz-border-bottom-colors: none;
            -moz-border-left-colors: none;
            -moz-border-right-colors: none;
            -moz-border-top-colors: none;
            background-color: #97B6B9;
            background-image: linear-gradient(rgba(10, 60, 150, 0.8), rgba(10, 60, 150, 0.8));
            border-color: rgba(10, 60, 150, 0.8) rgba(10, 60, 150, 0.8) rgba(10, 60, 150, 0.8);
            border-image: none;
            border-radius: 0.35em 0.35em 0 0;
            border-style: solid;
            border-width: 1px;
            box-shadow: 0 0.1em 0.1em rgba(255, 255, 255, 0.3) inset;
            color: rgba(255, 255, 255, 0.85);
            text-shadow: 0 -0.08em 0 #073D3D;
        }
    </style>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMainContent" runat="server">
    <ajax:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </ajax:ToolkitScriptManager>
    <br />
    <div class="HeaderText">
        <p style="text-align: left">
        Auction Intimation Letter</h1>
    </div>
    <div style="padding-left: 26%">
        <br />
        <br />
        <table align="left" style="width: 500px; height: 33px; margin-left: 11px;">
            <tr style="width: 500px;">
                <td style="width: 272px; height: 33px;">
                    <asp:Label ID="chit_num" Text="Chit Number" CssClass="labels" runat="server"></asp:Label>
                </td>
                <td style="width: 150px; height: 33px">
                    <asp:DropDownList AutoPostBack="true" ID="cmbGroup" runat="server" CssClass="css3-selectbox"
                        OnSelectedIndexChanged="cmbGroup_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr style="width: 500px; height: 33px;">
                <td style="width: 272px; height: 33px;">
                    <asp:Label ID="pre_draw" Text="Prevous Draw Dividened" CssClass="labels" runat="server"></asp:Label>
                </td>
                <td style="width: 150px; height: 33px">
                    <asp:TextBox EnableViewState="false" ID="txtPreviousDivident" CssClass="twitterStyleTextbox"
                        runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr style="width: 500px; height: 33px;">
                <td style="width: 272px; height: 33px;">
                    <asp:Label ID="pre_price" Text="Previous Price Amount" CssClass="labels" runat="server"></asp:Label>
                </td>
                <td style="width: 150px; height: 33px">
                    <asp:TextBox EnableViewState="false" ID="txt_price" CssClass="twitterStyleTextbox"
                        runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr style="width: 500px; height: 33px;">
                <td style="width: 272px; height: 33px;">
                    <asp:Label ID="Label1" Text="InstalMent Amount" CssClass="labels" runat="server"></asp:Label>
                </td>
                <td style="width: 150px; height: 33px">
                    <asp:TextBox EnableViewState="false" ID="txtInstalmentAmount" CssClass="twitterStyleTextbox"
                        runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr style="width: 500px;">
                <td style="width: 500px; text-align: right">
                    <asp:Button ID="btnGenerate" Text="Generate" CssClass="btn-style" runat="server"
                        OnClick="btnGenerate_Click" />
                </td>
            </tr>
        </table>
        <asp:Panel CssClass="raised" ID="Pnlmsg" runat="server" Visible="false" Width="600px"
            Style="max-height: 500px; min-height: 300px">
            <asp:Label runat="server" ID="lblHint" Text="" Visible="false"> </asp:Label>
            <%--<div  style="background-color:#3979BA;width: 100%; height: 40px;  top: 0px;"  >--%>
            <div style="top: 0px; height: 50px; text-align: center; padding: auto 0; width: 100%"
                class="boxfooter">
                <asp:Label ID="lblHeading" runat="server" Text=""> </asp:Label>
            </div>
            <div id="Div1" style="max-height: 400px; min-height: 200px; overflow: auto; width: 100%;">
                <%--<asp:TextBox id="txt1" runat="server" Height="1000"></asp:TextBox>--%>
                <br />
                <br />
                <asp:Label ID="lblContent" runat="server" Text="" Style="text-align: justify; vertical-align: middle;
                    margin-left: 10px"> </asp:Label>
                <br />
                <br />
                <asp:PlaceHolder ID="DynamicControlsHolder" runat="server"></asp:PlaceHolder>
            </div>
            <div class="boxheader" style="width: 100%; bottom: 0px; height: 50px; position: absolute">
                <div style="width: 210px; margin: 0 auto; padding-top: 10px">
                    <asp:Button Width="100" Style="margin: 0 auto" CssClass="GreenyPushButton" ID="Button1"
                        runat="server" Text="yes" />
                    <asp:Button Width="100" Style="margin: 0 auto" CssClass="GreenyPushButton" ID="Button2"
                        runat="server" Text="No" />
                </div>
            </div>
        </asp:Panel>
        <ajax:ModalPopupExtender ID="ModalPopupExtender1" BackgroundCssClass="modalBackground"
            TargetControlID="btnEmployee" PopupControlID="Pnlmsg" runat="server">
        </ajax:ModalPopupExtender>
    </div>
</asp:Content>
