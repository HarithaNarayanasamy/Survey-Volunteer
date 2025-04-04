<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UndoReceipt.aspx.cs" Inherits="SreeVisalamChitFundLtd_phase1.UndoReceipt" %>

<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1.Export, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView.Export" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Undo Receipt Transaction</title>
</head>
<body>
    <form id="form1" runat="server">
    
  <table >
        <tr>
         <td>
                <dx:ASPxLabel  ID="lblTokenNo" runat="server" 
                    Text="Token No">
                    
                </dx:ASPxLabel>
            </td>
             <td>
                <dx:ASPxLabel  ID="ASPxLabel1" runat="server" 
                    Text="Token No">
                    
                </dx:ASPxLabel>
            </td>
              <td style="padding-left: 4px">
              <asp:DropDownList style="width:120px !important;" width="120" TabIndex=1  ID="ddlGroupNo" CssClass="twitterStyleTextbox"   
        runat="server" AutoPostBack="True"  CausesValidation="false" 
        OnSelectedIndexChanged="ddlGroupNo_SelectedIndexChanged"></asp:DropDownList>
               <asp:DropDownList style="width:120px !important;" width="120" TabIndex=1  ID="ddlTokenNo" CssClass="twitterStyleTextbox"   
        runat="server" AutoPostBack="True"  CausesValidation="false" 
        OnSelectedIndexChanged="ddlTokenNo_SelectedIndexChanged"></asp:DropDownList>
            </td>
            <td style="padding-left: 4px">
                <dx:ASPxButton ID="btnLoad" runat="server" OnClick="btnLoad_click" AutoPostBack="true" 
                    Text="Load" >
                    
                </dx:ASPxButton>
            </td>
            <td>
                <dx:ASPxButton ID="ASPxButton1" runat="server" AutoPostBack="false" UseSubmitBehavior="false"
                    Text="Select All on Page">
                    <ClientSideEvents Click="function() { grid.SelectAllRowsOnPage() }" />
                </dx:ASPxButton>
            </td>
            <td style="padding-left: 4px">
                <dx:ASPxButton ID="ASPxButton2" runat="server" AutoPostBack="false" UseSubmitBehavior="false"
                    Text="Unselect All on Page">
                    <ClientSideEvents Click="function() { grid.UnselectAllRowsOnPage() }" />
                </dx:ASPxButton>
            </td>
        </tr>
    </table>
    <dx:ASPxGridView ID="grid" ClientInstanceName="grid" runat="server" DataSourceID="AccessDataSource1"
        KeyFieldName="CustomerID" Width="100%">
        <SettingsBehavior AllowGroup="false" AllowDragDrop="false" />
        <Columns>
            <dx:GridViewCommandColumn ShowSelectCheckbox="True" VisibleIndex="0">
                <HeaderTemplate>
                    <dx:ASPxCheckBox ID="SelectAllCheckBox" runat="server" ToolTip="Select/Unselect all rows on the page"
                        ClientSideEvents-CheckedChanged="function(s, e) { grid.SelectAllRowsOnPage(s.GetChecked()); }" />
                </HeaderTemplate>
                <HeaderStyle HorizontalAlign="Center" />
            </dx:GridViewCommandColumn>
            <dx:GridViewDataColumn FieldName="ContactName" VisibleIndex="1" />
            <dx:GridViewDataColumn FieldName="CompanyName" VisibleIndex="2" />
            <dx:GridViewDataColumn FieldName="City" VisibleIndex="3" />
            <dx:GridViewDataColumn FieldName="Region" VisibleIndex="4" />
            <dx:GridViewDataColumn FieldName="Country" VisibleIndex="5" />
        </Columns>
    </dx:ASPxGridView>
     <asp:SqlDataSource runat="server" ID="AccessDataSource1"    
    ProviderName="MySql.Data.MySqlClient"
    SelectCommand="" />
    </form>
</body>
</html>
