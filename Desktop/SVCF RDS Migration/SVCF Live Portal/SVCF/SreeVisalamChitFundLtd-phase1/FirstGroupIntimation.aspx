<%@ Page Language="C#" MasterPageFile="~/Branch.Master" AutoEventWireup="true" CodeBehind="FirstGroupIntimation.aspx.cs" Inherits="SreeVisalamChitFundLtd_phase1.FirstGroupIntimation" Title="Untitled Page" %>
<asp:Content ID="Head1" ContentPlaceHolderID="cphHead" runat="server">
<style type="text/css" >
td
{
    text-align:left;
}
</style>

</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMainContent" runat="server">
<div class="content">

<div style="background:white;border-bottom:10px solid white;">
<div class="ribheader">
Consolidated Intimation
</div>
</div>
      <br />




<div style="margin:0px auto; display:table;">
<asp:Table runat="server"  ID="cnt1"   style=" margin:0px auto;display:table-cell;">
<asp:TableRow >
<asp:TableCell>
 <asp:Label ID="Label1"  runat="server" Text="Group/Chit No." ></asp:Label>
</asp:TableCell>
<asp:TableCell >
  <asp:DropDownList ID="cmbGroup" runat="server" CssClass="twitterStyleTextbox"></asp:DropDownList>
</asp:TableCell>
 <asp:TableCell >
   <asp:RequiredFieldValidator ID="RequiredFieldValidator1" CssClass="bubble" runat="server" ControlToValidate=cmbGroup ErrorMessage="*BranchCode Required"></asp:RequiredFieldValidator>
      </asp:TableCell>
      <asp:TableCell > <asp:Button ID="btnGenerate" CssClass="GreenyPushButton" Text="Generate" runat="server" 
        onclick="btnGenerate_Click"  /></asp:TableCell>
</asp:TableRow>
</asp:Table> 
</div>
 </div>
</asp:Content>
