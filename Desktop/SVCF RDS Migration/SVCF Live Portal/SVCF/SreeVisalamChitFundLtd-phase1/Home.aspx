<%@ Page Title="SVCF Admin Page" Language="C#" MasterPageFile="~/Branch.Master" AutoEventWireup="true"
    CodeBehind="Home.aspx.cs" Inherits="SreeVisalamChitFundLtd_phase1.Home" %>


<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPopupControl" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxGauges.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGauges" TagPrefix="dx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <style>
        .statfadeheading
        {
            text-transform: uppercase;
            font-size: 11px;
            opacity: 0.75;
            display: block;
            line-height: normal;
            margin-bottom: 2px;
            color: #fff;
        }
        .statbrightheading
        {
            text-transform: uppercase;
            font-size: 32px;
            opacity: 2;
            display: block;
            line-height: normal;
            margin-bottom: 2px;
            color: #fff;
            text-shadow: 0 1px 0 #ccc, 0 2px 0 #c9c9c9, 0 3px 0 #bbb, 0 4px 0 #b9b9b9, 0 5px 0 #aaa, 0 6px 1px rgba(0,0,0,.1), 0 0 5px rgba(0,0,0,.1), 0 1px 3px rgba(0,0,0,.3), 0 3px 5px rgba(0,0,0,.2), 0 5px 10px rgba(0,0,0,.25),;
        }
        
        .head1
        {
            line-height: 36px;
            font-family: 'Helvetica Neue' , sans-serif;
            font-size: 25px;
            color: #fff;
            opacity: 0.75;
        }
        .bigvalue
        {
            line-height: 36px;
            font-family: 'Helvetica Neue' , sans-serif;
            font-size: 33px;
            color: #fff;
        }
        .btn
        {
            cursor: pointer;
            cursor: Hand;
            border: 1px solid #999999;
            -webkit-border-radius: 42px;
            -moz-border-radius: 42px;
            border-radius: 42px;
            font-family: arial, helvetica, sans-serif;
            padding: 5px 15px 5px 15px;
            text-shadow: 1px 1px 0 rgba(255,255,255,0.3);
            text-decoration: none;
            display: inline-block;
            font-weight: bold;
            color: #000000;
            background-color: #FFFFFF;
            font-size: 12px;
            background-image: -webkit-gradient(linear, left top, left bottom, from(#FFFFFF), to(#CFCFCF));
            background-image: -webkit-linear-gradient(top, #FFFFFF, #CFCFCF);
            background-image: -moz-linear-gradient(top, #FFFFFF, #CFCFCF);
            background-image: -ms-linear-gradient(top, #FFFFFF, #CFCFCF);
            background-image: -o-linear-gradient(top, #FFFFFF, #CFCFCF);
            background-image: linear-gradient(to bottom, #FFFFFF, #CFCFCF);
            filter: progid:DXImageTransform.Microsoft.gradient(GradientType=0,startColorstr=#FFFFFF, endColorstr=#CFCFCF);
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMainContent" runat="server">
    <ajax:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </ajax:ToolkitScriptManager>
    <asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="Conditional">
        <ContentTemplate>
            <dx:ASPxPopupControl Modal="true" ID="dxPopup" runat="server" AllowDragging="true"
                DragElement="Window" AllowResize="false" ShowCloseButton="true" EnableViewState="False"
                CloseAction="CloseButton" ContentUrl="javascript:void(0);" Opacity="100" PopupHorizontalAlign="WindowCenter"
                PopupVerticalAlign="WindowCenter" ShowFooter="false" ShowOnPageLoad="false" MinWidth="1024"
                MaxWidth="1200" MinHeight="590" MaxHeight="590" FooterText="Try to resize the control using the resize grip or the control's edges"
                ClientInstanceName="FeedPopupControl" EnableHierarchyRecreation="True">
                <HeaderStyle Paddings-PaddingRight="20" ForeColor="#666677" Font-Size="12" />
            </dx:ASPxPopupControl>
            <asp:Timer ID="Timer1" runat="server" Interval="120000" OnTick="tup_Tick">
            </asp:Timer>
            <div class="row">
                <div id="cashbank" class="twelve columns" runat="server">
                    <div class="box_c">
                        <div  class="box_c_heading  box_actions" >
                            <p>
                                Cash And Bank Balance</p>
                        </div>
                        <div  class="box_c_content" >
                            <div class="row">
                                <div class="row display" style="background-color: white !important;">
                                    <div class="six columns" style="background-color: #1d2939 !important; min-height: 80px;">
                                        <div class="row" style="background-color: #1d2939 !important;">
                                            <div class="two columns" style="background-color: #1d2939 !important;">
                                                <img alt="" src="pertho_admin_v1.3/img/is-money.png" />
                                            </div>
                                            <div class="ten columns" style="background-color: #1d2939 !important;">
                                                <span class="head1">Today Balance On Cash</span>
                                                <br />
                                                <asp:Label CssClass="bigvalue" ID="lblCashTodayBalance" Text="" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="row" style="background-color: #1d2939 !important;">
                                            <div class="twelve columns" style="background-color: #1d2939 !important; text-align: center;
                                                height: 15px !important;">
                                            </div>
                                        </div>
                                        <div class="row" style="background-color: #1d2939 !important;">
                                            <div class="eleven columns centered" style="background-color: #1d2939 !important;">
                                                <div class="six columns " style="background-color: #1d2939 !important; text-align: center;
                                                    height: 15px !important;">
                                                    <asp:Button OnClick="btn_RefreshCash_Click" Style="width: 100%" ID="btnCashRefresh"
                                                        Text="Refresh" CssClass="btn" runat="server" />
                                                </div>
                                                <div class="six columns " style="background-color: #1d2939 !important; text-align: center;
                                                    height: 15px !important;">
                                                    <asp:Button OnClick="btn_ViewMoreCash_Click" Style="width: 100%" ID="Button1" Text="View Details"
                                                        CssClass="btn" runat="server" />
                                                </div>
                                            </div>
                                        </div>
                                        <br />
                                        <div class="row" style="background-color: #1d2939 !important;">
                                            <div class="ten columns centered" style="background-color: #1d2939 !important; text-align: center">
                                                <asp:Label runat="server" ID="lblLastUpdatedCash" Style="color: #fff; margin: 0 auto;
                                                    text-shadow: 0px 2px 2px rgba(255, 255, 255, 0.4);"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <%--	2nd area--%>
                                    <div class="six columns" style="background-color: #1d2939 !important; min-height: 80px;">
                                        <div class="row" style="background-color: #1d2939 !important;">
                                            <div class="two columns" style="background-color: #1d2939 !important;">
                                                <img src="pertho_admin_v1.3/img/is-money.png" />
                                            </div>
                                            <div class="ten columns" style="background-color: #1d2939 !important;">
                                                <span class="head1">Today Balance On bank</span>
                                                <br />
                                                <asp:Label CssClass="bigvalue" ID="lblbankTodayBalance" Text="" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="row" style="background-color: #1d2939 !important;">
                                            <div class="twelve columns" style="background-color: #1d2939 !important; text-align: center;
                                                height: 15px !important;">
                                            </div>
                                        </div>
                                        <div class="row" style="background-color: #1d2939 !important;">
                                            <div class="eleven columns centered" style="background-color: #1d2939 !important;">
                                                <div class="six columns " style="background-color: #1d2939 !important; text-align: center;
                                                    height: 15px !important;">
                                                    <asp:Button OnClick="btn_Refreshbank_Click" Style="width: 100%" ID="btnbankRefresh"
                                                        Text="Refresh" CssClass="btn" runat="server" />
                                                </div>
                                                <div class="six columns " style="background-color: #1d2939 !important; text-align: center;
                                                    height: 15px !important;">
                                                    <asp:Button OnClick="btn_ViewMoreBank_Click" Style="width: 100%" ID="Button2" Text="View Details"
                                                        CssClass="btn" runat="server" />
                                                </div>
                                            </div>
                                        </div>
                                        <br />
                                        <div class="row" style="background-color: #1d2939 !important;">
                                            <div class="ten columns centered" style="background-color: #1d2939 !important; text-align: center">
                                                <asp:Label runat="server" ID="lblLastUpdatedbank" Style="color: #fff; margin: 0 auto;
                                                    text-shadow: 0px 2px 2px rgba(255, 255, 255, 0.4);"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<%--	<div class="row" style="background-color:#1d2939 !important;">
						<div class="six columns" style="background-color:#1d2939 !important;">	
							 <span class="head1">Today Credit </span> 
                        <br />
                        <asp:Label CssClass="bigvalue" ID="lblyesterdayCashBalance" text="Cr.655"  runat="server"></asp:Label>	
					    </div>
						<div class="six columns" style="background-color:#1d2939 !important;">	
							 <span class="head1">Today Debit </span> 
                        <br />
                        <asp:Label CssClass="bigvalue" ID="lblCashTodayAloneBalance" text="Cr.655"  runat="server"></asp:Label>	
					    </div>	
                       
                       
					</div>

                    	<div class="row" style="background-color:#1d2939 !important;">
						<div class="six columns" style="background-color:#1d2939 !important;">	
							 <span class="head1">Total Credit </span> 
                        <br />
                        <asp:Label CssClass="bigvalue" ID="Label1" text="Cr.655"  runat="server"></asp:Label>	
					    </div>
						<div class="six columns" style="background-color:#1d2939 !important;">	
							 <span class="head1">Total Debit </span> 
                        <br />
                        <asp:Label CssClass="bigvalue" ID="Label2" text="Cr.655"  runat="server"></asp:Label>	
					    </div>	
                       
                       
					</div>--%>