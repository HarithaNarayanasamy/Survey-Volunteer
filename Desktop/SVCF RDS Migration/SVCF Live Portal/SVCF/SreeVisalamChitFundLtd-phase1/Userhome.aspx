<%@ Page Title="SVCF Admin Page" Language="C#" MasterPageFile="~/Branch.Master" AutoEventWireup="true"
    CodeBehind="Userhome.aspx.cs" Inherits="SreeVisalamChitFundLtd_phase1.Userhome" %>


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
