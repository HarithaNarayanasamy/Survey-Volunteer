<%@ Page Language="C#" MasterPageFile="~/Branch.Master" AutoEventWireup="true" CodeBehind="Subscriber.aspx.cs" Inherits="SreeVisalamChitFundLtd_phase1.Subscriber" Title="Subscriber" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
<style type="text/css" >

td
{
	align:left;
	
}
</style> 
<script type="text/javascript" src="JqueryTab/JqueryTab_Min.js"></script>
<script src="JqueryTab/JqueryTab.js" type="text/javascript"></script>
<link href="JqueryTab/Tabcontainer.css"
    rel="stylesheet" type="text/css" />
<script type="text/javascript">
    var selected_tab = 1;
    $(function () {
        var tabs = $('#<%=tabs.ClientID%>').tabs({
            select: function (e, i) {
                selected_tab = i.index;
            }
        });
        selected_tab = $("[id$=selected_tab]").val() != "" ? parseInt($("[id$=selected_tab]").val()) : 0;
        tabs.tabs('select', selected_tab);
        $("form").submit(function () {
            $("[id$=selected_tab]").val(selected_tab);
        });
    });
   
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMainContent" runat="server">
<div style="width:100%;height:500px;">




    <ajax:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </ajax:ToolkitScriptManager>
<div class="content">
<h2 class ="ribbon">Subscriber Addition</h2>
 <div id="tabs" runat="server">
    <ul>
        <li><a href="#tabs-1">Subscriber</a></li>
        <li><a href="#tabs-2">Description and value of Immovable Properties</a></li>
        <li><a href="#tabs-3">Particulars of Business</a></li>
         <li><a href="#tabs-4">Other Liabilities</a></li>
    </ul>
    <div id="tabs-1" >
    <div style="overflow:scroll;">
       <table  >
          <tr>
              <td>
                  <asp:Label ID="Label1" runat="server" Text="SubscriberName"></asp:Label>
              </td>
              <td>
                  <asp:TextBox ID="txtName" runat="server" CssClass="twitterStyleTextbox"></asp:TextBox>
              
              <asp:RequiredFieldValidator ID="RequiredFieldValidator1" Display="Dynamic" runat="Server" CssClass ="bubble" ValidationGroup="aa" ControlToValidate ="txtName" ErrorMessage="*"></asp:RequiredFieldValidator>
              </td>
              <td>
                  <asp:Label ID="Label2" runat="server" Text="Age"></asp:Label>
              </td>
              <td>
                  <asp:TextBox ID="txtAge" runat="server" CssClass="twitterStyleTextbox"></asp:TextBox>
              </td>
              <td>
                  &nbsp;</td>
              <td>
                  <asp:Label ID="Label3" runat="server" Text="DOB"></asp:Label>
              </td>
              <td>
              <div style="position:relative">
                  <asp:TextBox ID="txtDOB" runat="server" CssClass="twitterStyleTextbox"></asp:TextBox>
            <ajax:CalendarExtender  CssClass="ajaxcalendar"  ID="CalendarExtender2" Format="dd/MM/yyyy" TargetControlID="txtDOB" runat="server"></ajax:CalendarExtender>
                  </div>
              </td>
              <td>
                  &nbsp;</td>
          </tr>
          <tr>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
          </tr>
          <tr>
              <td>
                  <asp:Label ID="Label4" runat="server" Text="Father/Husband's Name"></asp:Label>
              </td>
              <td>
                  <asp:TextBox ID="txtFHusbandName" runat="server" CssClass="twitterStyleTextbox"></asp:TextBox>
              </td>
              <td>
                  <asp:Label ID="Label5" runat="server" Text="Native Address"></asp:Label>
              </td>
              <td>
                  <asp:TextBox ID="txtNativeAddress" runat="server" CssClass="twitterStyleTextbox" 
                      Height="50px" TextMode="MultiLine" Width="152px"></asp:TextBox>
              </td>
              <td>
                  &nbsp;</td>
              <td>
                  <asp:Label ID="Label6" runat="server" Text="Residential Address"></asp:Label>
              </td>
              <td>
                  <asp:TextBox ID="txtResidentialAddress" runat="server" CssClass="twitterStyleTextbox" 
                      Height="50px" TextMode="MultiLine" Width="152px" 
                     ></asp:TextBox>
              </td>
              <td>
                  &nbsp;</td>
          </tr>
          <tr>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
          </tr>
          <tr>
              <td>
                  <asp:Label ID="Label7" runat="server" Text="Office Address"></asp:Label>
              </td>
              <td>
                  <asp:TextBox ID="txtDesginationOfficeAddress" runat="server" CssClass="twitterStyleTextbox" 
                      Height="50px" TextMode="MultiLine" Width="152px"></asp:TextBox>
              </td>
              <td>
                  <asp:Label ID="Label8" runat="server" Text="Telephone No"></asp:Label>
              </td>
              <td>
                  <asp:TextBox ID="txtTelephoneNo" runat="server" CssClass="twitterStyleTextbox"></asp:TextBox>
              </td>
              <td>
                  &nbsp;</td>
              <td>
                  <asp:Label ID="Label9" runat="server" Text="Income for Month"></asp:Label>
              </td>
              <td>
                  <asp:TextBox ID="txtIncomeofMonth" runat="server" 
                      CssClass="twitterStyleTextbox"></asp:TextBox>
              </td>
              <td>
                  <asp:RegularExpressionValidator ID="RegularExpressionValidator5"  Display="Dynamic"      ValidationExpression="^\d+(\.\d{1,2})?$"   ControlToValidate="txtIncomeofMonth" ValidationGroup="e" runat="server" CssClass="bubble" ErrorMessage="Invalid Amount"></asp:RegularExpressionValidator></td>
          </tr>
          <tr>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
          </tr>
          <tr>
              <td>
                  <asp:Label ID="Label10" runat="server" Text="Basic Pay Rs"></asp:Label>
              </td>
              <td>
                  <asp:TextBox ID="txtBasicPay" runat="server" CssClass="twitterStyleTextbox"></asp:TextBox>
                   <br />
                   <asp:RegularExpressionValidator ID="RegularExpressionValidator17"  Display="Dynamic"      ValidationExpression="^\d+(\.\d{1,2})?$"   ControlToValidate="txtBasicPay" ValidationGroup="e" runat="server" CssClass="bubble" ErrorMessage="Invalid Amount"></asp:RegularExpressionValidator>
              </td>
              <td>
                  <asp:Label ID="Label11" runat="server" Text="D.A.Rs"></asp:Label>
              </td>
              <td>
                  <asp:TextBox ID="txtDARs" runat="server" CssClass="twitterStyleTextbox"></asp:TextBox>
                 
              </td>
              <td>
                   <asp:RegularExpressionValidator ID="RegularExpressionValidator4"  Display="Dynamic"      ValidationExpression="^\d+(\.\d{1,2})?$"   ControlToValidate="txtDARs" ValidationGroup="e" runat="server" CssClass="bubble" ErrorMessage="Invalid Amount"></asp:RegularExpressionValidator></td>
              <td>
                  <asp:Label ID="Label12" runat="server" Text="O.A.Rs"></asp:Label>
              </td>
              <td>
                  <asp:TextBox ID="txtOARs" runat="server" CssClass="twitterStyleTextbox"></asp:TextBox>
                 
              </td>
              <td>
                 <asp:RegularExpressionValidator ID="RegularExpressionValidator3"  Display="Dynamic"      ValidationExpression="^\d+(\.\d{1,2})?$"   ControlToValidate="txtOARs" ValidationGroup="e" runat="server" CssClass="bubble" ErrorMessage="Invalid Amount"></asp:RegularExpressionValidator></td>
          </tr>
          <tr>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
          </tr>
          <tr>
              <td>
                  <asp:Label ID="Label66" runat="server" Text="Other Income"></asp:Label>
              </td>
              <td>
                  <asp:TextBox ID="txtOtherIncome" runat="server" CssClass="twitterStyleTextbox"></asp:TextBox>
                  <br />
                  <asp:RegularExpressionValidator ID="RegularExpressionValidator1"  Display="Dynamic"      ValidationExpression="^\d+(\.\d{1,2})?$"   ControlToValidate="txtOtherIncome" ValidationGroup="e" runat="server" CssClass="bubble" ErrorMessage="Invalid Amount"></asp:RegularExpressionValidator>
              </td>
              <td>
                  <asp:Label ID="Label67" runat="server" Text="AY"></asp:Label>
              </td>
              <td>
                  <asp:TextBox ID="txtAY" runat="server" CssClass="twitterStyleTextbox"></asp:TextBox>
              </td>
              <td>
                  &nbsp;</td>
              <td>
                  <asp:Label ID="Label68" runat="server" Text="AY Amount"></asp:Label>
              </td>
              <td>
                  <asp:TextBox ID="txtAY_Amount" runat="server" CssClass="twitterStyleTextbox"></asp:TextBox>
                    <br />
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2"  Display="Dynamic"      ValidationExpression="^\d+(\.\d{1,2})?$"   ControlToValidate="txtAY_Amount" ValidationGroup="e" runat="server" CssClass="bubble" ErrorMessage="Invalid Amount"></asp:RegularExpressionValidator>
              </td>
              <td>
                  &nbsp;</td>
          </tr>
          <tr>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
          </tr>
          <tr>
              <td colspan="3">
                  <asp:Label ID="Label65" runat="server" 
                      Text="Relationship between Subscriber &amp; Guarantor(Surety)"></asp:Label>
              </td>
              <td>
                  <asp:TextBox ID="txtSubscriberandGuarantor" runat="server" 
                      CssClass="twitterStyleTextbox"></asp:TextBox>
              </td>
              <td>
                  &nbsp;</td>
              <td>
                  <asp:Label ID="Label69" runat="server" Text="AyP.A.No"></asp:Label>
              </td>
              <td>
                  <asp:TextBox ID="txtAy_PANo" runat="server" CssClass="twitterStyleTextbox"></asp:TextBox>
              </td>
              <td>
                  &nbsp;</td>
          </tr>
          <tr>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  <asp:Label ID="Label70" runat="server" Text="AY Office"></asp:Label>
              </td>
              <td>
                  <asp:TextBox ID="txtAY_Office" runat="server" CssClass="twitterStyleTextbox"></asp:TextBox>
              </td>
              <td>
                  &nbsp;</td>
          </tr>
          <tr>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
          </tr>
          <tr>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
          </tr>
          <tr>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
          </tr>
          <tr>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
          </tr>
        
          
      </table>
    </div>
     </div>
    <div id="tabs-2">
    <div style="height:500px; overflow:scroll;">
          <table >
          <tr>
              <td class="style2">
                  <asp:Label ID="Label13" runat="server" Text="District"></asp:Label>
              </td>
              <td>
                  <asp:TextBox ID="txtDistrict" runat="server" CssClass="twitterStyleTextbox"></asp:TextBox>
              </td>
              <td>
                  &nbsp;</td>
              <td>
                  <asp:Label ID="Label14" runat="server" Text="Taluk"></asp:Label>
              </td>
              <td>
                  <asp:TextBox ID="txtTaluk" runat="server" CssClass="twitterStyleTextbox"></asp:TextBox>
              </td>
              <td>
                  &nbsp;</td>
              <td>
                  <asp:Label ID="Label15" runat="server" Text="Village/Town"></asp:Label>
              </td>
              <td>
                  <asp:TextBox ID="txtVillageTown" runat="server" CssClass="twitterStyleTextbox"></asp:TextBox>
              </td>
              <td>
                  &nbsp;</td>
          </tr>
          <tr>
              <td class="style2">
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
          </tr>
          <tr>
              <td class="style2">
                  <asp:Label ID="Label16" runat="server" Text="Street"></asp:Label>
              </td>
              <td>
                  <asp:TextBox ID="txtStreet" runat="server" CssClass="twitterStyleTextbox"></asp:TextBox>
              </td>
              <td>
                  &nbsp;</td>
              <td>
                  <asp:Label ID="Label17" runat="server" Text="Ward No"></asp:Label>
              </td>
              <td>
                  <asp:TextBox ID="txtWardNo" runat="server" CssClass="twitterStyleTextbox"></asp:TextBox>
              </td>
              <td>
                  &nbsp;</td>
              <td>
                  <asp:Label ID="Label18" runat="server" Text="Door No"></asp:Label>
              </td>
              <td>
                  <asp:TextBox ID="txtDoorNo" runat="server" CssClass="twitterStyleTextbox"></asp:TextBox>
              </td>
              <td>
                  &nbsp;</td>
          </tr>
          <tr>
              <td class="style2">
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
          </tr>
          <tr>
              <td class="style2">
                  <asp:Label ID="Label19" runat="server" Text="Town Survey No"></asp:Label>
              </td>
              <td>
                  <asp:TextBox ID="txtTownSurveyNo" runat="server" CssClass="twitterStyleTextbox"></asp:TextBox>
              </td>
              <td>
                  &nbsp;</td>
              <td>
                  <asp:Label ID="Label20" runat="server" Text="Registrar's Office"></asp:Label>
              </td>
              <td>
                  <asp:TextBox ID="txtRegisterOffice" runat="server" 
                      CssClass="twitterStyleTextbox"></asp:TextBox>
              </td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
          </tr>
          <tr>
              <td class="style2">
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
          </tr>
          <tr>
              <td class="style2">
                  <asp:Label ID="Label21" runat="server" Text="Sale Value of Property Rs"></asp:Label>
              </td>
              <td>
                  <asp:TextBox ID="txtSaleValueofProperty" runat="server" 
                      CssClass="twitterStyleTextbox"></asp:TextBox>
              </td>
              <td>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator6"  Display="Dynamic"      ValidationExpression="^\d+(\.\d{1,2})?$"   ControlToValidate="txtSaleValueofProperty" ValidationGroup="e" runat="server" CssClass="bubble" ErrorMessage="Invalid Amount"></asp:RegularExpressionValidator>
                </td>
              <td>
                  <asp:Label ID="Label22" runat="server" Text="Market Value as on"></asp:Label>
              </td>
              <td>
              <div style="position:relative">
                  <asp:TextBox ID="txtMarketValue" runat="server" CssClass="twitterStyleTextbox"></asp:TextBox>
  <ajax:CalendarExtender  CssClass="ajaxcalendar"  ID="CalendarExtender1" Format="dd/MM/yyyy" TargetControlID="txtMarketValue" runat="server"></ajax:CalendarExtender>
             </div>
              </td>
              
              <td>
                  &nbsp;</td>
              <td>
                  <asp:Label ID="Label23" runat="server" Text="Rs"></asp:Label>
              </td>
              <td>
                  <asp:TextBox ID="txtRs1" runat="server" CssClass="twitterStyleTextbox"></asp:TextBox>
              </td>
              <td>
                  &nbsp;</td>
          </tr>
          <tr>
              <td class="style2">
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
          </tr>
          <tr>
              <td class="style2">
                  <asp:Label ID="Label24" runat="server" 
                      Text="Annual/Corporation/Municipal/Panchayat/Land/Tax Per Half Year Rs"></asp:Label>
              </td>
              <td>
                  <asp:TextBox ID="txtTaxperHalfYear" runat="server" 
                      CssClass="twitterStyleTextbox"></asp:TextBox>
              </td>
              <td>
                 <asp:RegularExpressionValidator ID="RegularExpressionValidator7"  Display="Dynamic"      ValidationExpression="^\d+(\.\d{1,2})?$"   ControlToValidate="txtTaxperHalfYear" ValidationGroup="e" runat="server" CssClass="bubble" ErrorMessage="Invalid Amount"></asp:RegularExpressionValidator>
                 </td>
              <td>
               
              </td>
              <td>
                
                    
                      
              </td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
          </tr>
          <tr>
              <td class="style2">
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
          </tr>
          <tr>
              <td >
                  <asp:Label ID="Label25" runat="server" Text="Tax Recepit No"></asp:Label>
              </td>
              <td>
                  <asp:TextBox ID="txtTaxReceiptNo" runat="server" CssClass="twitterStyleTextbox"></asp:TextBox>
              </td>
              <td>
                  &nbsp;</td>
              <td>
                  <asp:Label ID="Label26" runat="server" Text="Tax Date"></asp:Label>
              </td>
              <td>
              <div style="position:relative">
                  <asp:TextBox ID="txtReceiptDate" runat="server" CssClass="twitterStyleTextbox"></asp:TextBox>
               
                 <ajax:CalendarExtender  CssClass="ajaxcalendar"  ID="CalendarExtender3" Format="dd/MM/yyyy" TargetControlID="txtReceiptDate" runat="server"></ajax:CalendarExtender>
                  </div>
              </td>
              <td>
                  &nbsp;</td>
              <td>
                  <asp:Label ID="Label27" runat="server" Text="Tax Amount"></asp:Label>
              </td>
              <td>
                  <asp:TextBox ID="txtTax_Amount" runat="server" CssClass="twitterStyleTextbox"></asp:TextBox>
              </td>
              <td>
                 <asp:RegularExpressionValidator ID="RegularExpressionValidator8"  Display="Dynamic"      ValidationExpression="^\d+(\.\d{1,2})?$"   ControlToValidate="txtTax_Amount" ValidationGroup="e" runat="server" CssClass="bubble" ErrorMessage="Invalid Amount"></asp:RegularExpressionValidator>
                 </td>
          </tr>
          <tr>
              <td >
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
          </tr>
          <tr>
              <td class="style2">
                  <asp:Label ID="Label28" runat="server" Text="Rental Income per Month"></asp:Label>
              </td>
              <td>
                  <asp:TextBox ID="txtRentalIncome" runat="server" CssClass="twitterStyleTextbox"></asp:TextBox>
              </td>
              <td>
                  <asp:RegularExpressionValidator ID="RegularExpressionValidator9"  Display="Dynamic"      ValidationExpression="^\d+(\.\d{1,2})?$"   ControlToValidate="txtRentalIncome" ValidationGroup="e" runat="server" CssClass="bubble" ErrorMessage="Invalid Amount"></asp:RegularExpressionValidator></td>
              <td>
                  <asp:Label ID="Label29" runat="server" Text="Encumbrance Certificate"></asp:Label>
              </td>
              <td>
                  <asp:TextBox ID="txtEncumbrance" runat="server" CssClass="twitterStyleTextbox"></asp:TextBox>
              </td>
              <td>
                  &nbsp;</td>
              <td>
                  <asp:Label ID="Label30" runat="server" Text="Rights of Property"></asp:Label>
              </td>
              <td>
                  <asp:DropDownList ID="ddlRightsofporperty" runat="server" 
                      CssClass="twitterStyleTextbox" Height="28px" Width="164px">
                      <asp:ListItem>Select</asp:ListItem>
                      <asp:ListItem>Full</asp:ListItem>
                      <asp:ListItem>Three fourth</asp:ListItem>
                      <asp:ListItem>Half</asp:ListItem>
                      <asp:ListItem>Quarter Portion</asp:ListItem>
                  </asp:DropDownList>
              </td>
              <td>
                  &nbsp;</td>
          </tr>
          <tr>
              <td class="style2">
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
          </tr>
          <tr>
              <td class="style2">
                  <asp:Label ID="Label31" runat="server" 
                      Text="Whether any Condition is attached to property"></asp:Label>
              </td>
              <td>
                  <asp:TextBox ID="txtConditionAttachedProperty" runat="server" 
                      CssClass="twitterStyleTextbox"></asp:TextBox>
              </td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
          </tr>
          <tr>
              <td>
                  <asp:Label ID="Label32" runat="server" Text="East of"></asp:Label>
              </td>
              <td>
                  <asp:TextBox ID="txtEast" runat="server" CssClass="twitterStyleTextbox"></asp:TextBox>
              </td>
              <td>
                  &nbsp;</td>
              <td>
                  <asp:Label ID="Label33" runat="server" Text="West of"></asp:Label>
              </td>
              <td>
                  <asp:TextBox ID="txtWest" runat="server" CssClass="twitterStyleTextbox"></asp:TextBox>
              </td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
          </tr>
          <tr>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
          </tr>
          <tr>
              <td>
                  <asp:Label ID="Label34" runat="server" Text="South of"></asp:Label>
              </td>
              <td>
                  <asp:TextBox ID="txtSouth" runat="server" CssClass="twitterStyleTextbox"></asp:TextBox>
              </td>
              <td>
                  &nbsp;</td>
              <td>
                  <asp:Label ID="Label35" runat="server" Text="North of"></asp:Label>
              </td>
              <td>
                  <asp:TextBox ID="txtNorth" runat="server" CssClass="twitterStyleTextbox"></asp:TextBox>
              </td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
          </tr>
          <tr>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
          </tr>
          <tr>
              <td>
                  <asp:Label ID="Label36" runat="server" 
                      Text="Other Details Regarding House Properties"></asp:Label>
              </td>
              <td>
                  <asp:TextBox ID="txtRegardingHouseProperty" runat="server" 
                      CssClass="twitterStyleTextbox"></asp:TextBox>
              </td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
          </tr>
          <tr>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
          </tr>
          <tr>
              <td>
                  <asp:Label ID="Label37" runat="server" Text="Details of Assets  Value"></asp:Label>
              </td>
              <td>
                  <asp:TextBox ID="txtDetailsAssetValue" runat="server" 
                      CssClass="twitterStyleTextbox"></asp:TextBox>
              </td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
          </tr>
          <tr>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
          </tr>
      </table>
      </div>
    </div>
    <div id="tabs-3">
    <div style="height:500px; overflow:scroll;" >
        <table class="style1">
          <tr>
              <td>
                  <asp:Label ID="Label38" runat="server" Text="Name of the Firm and Address"></asp:Label>
              </td>
              <td>
                  <asp:TextBox ID="txtFirmAddress" runat="server" CssClass="twitterStyleTextbox" 
                      Height="60px" TextMode="MultiLine" Width="157px"></asp:TextBox>
              </td>
              <td>
                  &nbsp;</td>
              <td>
                  <asp:Label ID="Label39" runat="server" Text="Business Started Year"></asp:Label>
              </td>
              <td>
                  <asp:TextBox ID="txtBusinessStartedYear" runat="server" 
                      CssClass="twitterStyleTextbox"></asp:TextBox>
              </td>
              <td>
                  &nbsp;</td>
          </tr>
          <tr>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
          </tr>
          <tr>
              <td>
                  <asp:Label ID="Label40" runat="server" Text="Nature of Business"></asp:Label>
              </td>
              <td>
                  <asp:TextBox ID="txtNatureofBusiness" runat="server" 
                      CssClass="twitterStyleTextbox"></asp:TextBox>
              </td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
          </tr>
          <tr>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
          </tr>
          <tr>
              <td>
                  <asp:Label ID="Label41" runat="server" Text="Firm Regn.No/Trad Licence No"></asp:Label>
              </td>
              <td>
                  <asp:TextBox ID="txtTradeLicenceNo" runat="server" 
                      CssClass="twitterStyleTextbox"></asp:TextBox>
              </td>
              <td>
                  &nbsp;</td>
              <td>
                  <asp:Label ID="Label42" runat="server" Text="Office"></asp:Label>
              </td>
              <td>
                  <asp:TextBox ID="txtPBOffice1" runat="server" CssClass="twitterStyleTextbox"></asp:TextBox>
              </td>
              <td>
                  &nbsp;</td>
          </tr>
          <tr>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
          </tr>
          <tr>
              <td>
                  <asp:Label ID="Label43" runat="server" Text="Central Sales Tax Regn.No"></asp:Label>
              </td>
              <td>
                  <asp:TextBox ID="txtCentralSalesTaxRegnNo" runat="server" 
                      CssClass="twitterStyleTextbox"></asp:TextBox>
              </td>
              <td>
                  &nbsp;</td>
              <td>
                  <asp:Label ID="Label44" runat="server" Text="Tin.Reg.No"></asp:Label>
              </td>
              <td>
                  <asp:TextBox ID="txtTinRegNo" runat="server" CssClass="twitterStyleTextbox"></asp:TextBox>
              </td>
              <td>
                  &nbsp;</td>
          </tr>
          <tr>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
          </tr>
          <tr>
              <td>
                  <asp:Label ID="Label45" runat="server" 
                      Text="Average Commercial Tax per Year Rs"></asp:Label>
              </td>
              <td>
                  <asp:TextBox ID="txtCommericalTaxperYear" runat="server" 
                      CssClass="twitterStyleTextbox"></asp:TextBox>
              </td>
              <td>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator10"  Display="Dynamic"      ValidationExpression="^\d+(\.\d{1,2})?$"   ControlToValidate="txtCommericalTaxperYear" ValidationGroup="e" runat="server" CssClass="bubble" ErrorMessage="Invalid Amount"></asp:RegularExpressionValidator>
                    </td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
          </tr>
          <tr>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
          </tr>
          <tr>
              <td>
                  <asp:Label ID="Label46" runat="server" Text="Share"></asp:Label>
              </td>
              <td>
                  <asp:TextBox ID="txtShare" runat="server" CssClass="twitterStyleTextbox"></asp:TextBox>
              </td>
              <td>
                  &nbsp;</td>
              <td>
                  <asp:Label ID="Label47" runat="server" Text="Capital"></asp:Label>
              </td>
              <td>
                  <asp:TextBox ID="txtCaptial" runat="server" CssClass="twitterStyleTextbox"></asp:TextBox>
              </td>
              <td>
                  <asp:RegularExpressionValidator ID="RegularExpressionValidator11"  Display="Dynamic"      ValidationExpression="^\d+(\.\d{1,2})?$"   ControlToValidate="txtCaptial" ValidationGroup="e" runat="server" CssClass="bubble" ErrorMessage="Invalid Amount"></asp:RegularExpressionValidator></td>
          </tr>
          <tr>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
          </tr>
          <tr>
              <td>
                  <asp:Label ID="Label48" runat="server" Text="Designation"></asp:Label>
              </td>
              <td>
                  <asp:TextBox ID="txtDesignation" runat="server" CssClass="twitterStyleTextbox"></asp:TextBox>
              </td>
              <td>
                  &nbsp;</td>
              <td>
                  <asp:Label ID="Label49" runat="server" Text="Partner's Details"></asp:Label>
              </td>
              <td>
                  <asp:TextBox ID="txtPartnersDetails" runat="server" 
                      CssClass="twitterStyleTextbox"></asp:TextBox>
              </td>
              <td>
                  &nbsp;</td>
          </tr>
          <tr>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
          </tr>
          <tr>
              <td>
                  <asp:Label ID="Label50" runat="server" Text="Net worth of the Business Rs"></asp:Label>
              </td>
              <td>
                  <asp:TextBox ID="txtNetWorthBusiness" runat="server" 
                      CssClass="twitterStyleTextbox"></asp:TextBox>
              </td>
              <td>
                  <asp:RegularExpressionValidator ID="RegularExpressionValidator12"  Display="Dynamic"      ValidationExpression="^\d+(\.\d{1,2})?$"   ControlToValidate="txtNetWorthBusiness" ValidationGroup="e" runat="server" CssClass="bubble" ErrorMessage="Invalid Amount"></asp:RegularExpressionValidator></td>
              <td>
                  <asp:Label ID="Label51" runat="server" Text="Average Annual Income Rs"></asp:Label>
              </td>
              <td>
                  <asp:TextBox ID="txtAverageAnnualIncome" runat="server" 
                      CssClass="twitterStyleTextbox"></asp:TextBox>
              </td>
              <td>
                  &nbsp;</td>
          </tr>
          <tr>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
          </tr>
          <tr>
              <td>
                  <asp:Label ID="Label52" runat="server" Text="Income Tax P.A.No.of the Firm"></asp:Label>
              </td>
              <td>
                  <asp:TextBox ID="txtIncomeTaxPANoofFirm" runat="server" 
                      CssClass="twitterStyleTextbox"></asp:TextBox>
              </td>
              <td>
                  &nbsp;</td>
              <td>
                  <asp:Label ID="Label53" runat="server" Text="Office"></asp:Label>
              </td>
              <td>
                  <asp:TextBox ID="txtIncomeTaxOffice" runat="server" 
                      CssClass="twitterStyleTextbox"></asp:TextBox>
              </td>
              <td>
                  &nbsp;</td>
          </tr>
          <tr>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
          </tr>
          <tr>
              <td>
                  <asp:Label ID="Label54" runat="server" 
                      Text="Income Tax paid for last Two Assessment Year Rs"></asp:Label>
              </td>
              <td>
                  <asp:TextBox ID="txtAssessmentYear" runat="server" 
                      CssClass="twitterStyleTextbox"></asp:TextBox>
              </td>
              <td>
                  <asp:RegularExpressionValidator ID="RegularExpressionValidator13"  Display="Dynamic"      ValidationExpression="^\d+(\.\d{1,2})?$"   ControlToValidate="txtAssessmentYear" ValidationGroup="e" runat="server" CssClass="bubble" ErrorMessage="Invalid Amount"></asp:RegularExpressionValidator></td>
              <td>
                  <asp:Label ID="Label55" runat="server" Text="Rs"></asp:Label>
              </td>
              <td>
                  <asp:TextBox ID="txtAssessment_Year_Rs1" runat="server" 
                      CssClass="twitterStyleTextbox"></asp:TextBox>
              </td>
              <td>
                  <asp:RegularExpressionValidator ID="RegularExpressionValidator14"  Display="Dynamic"      ValidationExpression="^\d+(\.\d{1,2})?$"   ControlToValidate="txtAssessment_Year_Rs1" ValidationGroup="e" runat="server" CssClass="bubble" ErrorMessage="Invalid Amount"></asp:RegularExpressionValidator></td>
          </tr>
          <tr>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
          </tr>
          <tr>
              <td>
                  <asp:Label ID="Label56" runat="server" Text="Chit No"></asp:Label>
              </td>
              <td>
                  <asp:TextBox ID="txtChitNo" runat="server" CssClass="twitterStyleTextbox"></asp:TextBox>
              </td>
              <td>
                  &nbsp;</td>
              <td>
                  <asp:Label ID="Label57" runat="server" Text="Value"></asp:Label>
              </td>
              <td>
                  <asp:TextBox ID="txtPBValue" runat="server" CssClass="twitterStyleTextbox"></asp:TextBox>
              </td>
              <td>
                  <asp:RegularExpressionValidator ID="RegularExpressionValidator15"  Display="Dynamic"      ValidationExpression="^\d+(\.\d{1,2})?$"   ControlToValidate="txtPBValue" ValidationGroup="e" runat="server" CssClass="bubble" ErrorMessage="Invalid Amount"></asp:RegularExpressionValidator></td>
          </tr>
          <tr>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
          </tr>
          <tr>
              <td>
                  <asp:Label ID="Label58" runat="server" Text="Amount Remitted upto"></asp:Label>
              </td>
              <td>
                  <asp:TextBox ID="txtAmountRemitted" runat="server" 
                      CssClass="twitterStyleTextbox"></asp:TextBox>
              </td>
              <td>
                  <asp:RegularExpressionValidator ID="RegularExpressionValidator16"  Display="Dynamic"      ValidationExpression="^\d+(\.\d{1,2})?$"   ControlToValidate="txtAmountRemitted" ValidationGroup="e" runat="server" CssClass="bubble" ErrorMessage="Invalid Amount"></asp:RegularExpressionValidator></td>
              <td>
                  <asp:Label ID="Label59" runat="server" Text="Installment Rs"></asp:Label>
              </td>
              <td>
                  <asp:TextBox ID="txtPBInstallment" runat="server" 
                      CssClass="twitterStyleTextbox"></asp:TextBox>
              </td>
              <td>
                  <asp:RegularExpressionValidator ID="RegularExpressionValidator18"  Display="Dynamic"      ValidationExpression="^\d+(\.\d{1,2})?$"   ControlToValidate="txtPBInstallment" ValidationGroup="e" runat="server" CssClass="bubble" ErrorMessage="Invalid Amount"></asp:RegularExpressionValidator></td>
          </tr>
          <tr>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
          </tr>
          <tr>
              <td>
                  <asp:Label ID="Label60" runat="server" Text="Future Instalment Payable Rs"></asp:Label>
              </td>
              <td>
                  <asp:TextBox ID="txtFutuerInstalmentPayable1" runat="server" 
                      CssClass="twitterStyleTextbox"></asp:TextBox>
              </td>
              <td>
                   <asp:RegularExpressionValidator ID="RegularExpressionValidator19"  Display="Dynamic"      ValidationExpression="^\d+(\.\d{1,2})?$"   ControlToValidate="txtFutuerInstalmentPayable1" ValidationGroup="e" runat="server" CssClass="bubble" ErrorMessage="Invalid Amount"></asp:RegularExpressionValidator></td>
              <td>
                  <asp:Label ID="Label61" runat="server" Text="Stands as Surety of Chit No"></asp:Label>
              </td>
              <td>
                  <asp:TextBox ID="txtStandsSuretyofChitNo" runat="server" 
                      CssClass="twitterStyleTextbox"></asp:TextBox>
              </td>
              <td>
                  &nbsp;</td>
          </tr>
          <tr>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
          </tr>
          <tr>
              <td>
                  <asp:Label ID="Label62" runat="server" Text="Chit No : Value"></asp:Label>
              </td>
              <td>
                  <asp:TextBox ID="txtChitNoValue" runat="server" CssClass="twitterStyleTextbox"></asp:TextBox>
              </td>
              <td>
                  &nbsp;</td>
              <td>
                  <asp:Label ID="Label63" runat="server" Text="Future Installment Payable Rs"></asp:Label>
              </td>
              <td>
                  <asp:TextBox ID="txtFutureInstallmentPayable2" runat="server" 
                      CssClass="twitterStyleTextbox"></asp:TextBox>
              </td>
              <td>
                  <asp:RegularExpressionValidator ID="RegularExpressionValidator20"  Display="Dynamic"      ValidationExpression="^\d+(\.\d{1,2})?$"   ControlToValidate="txtFutureInstallmentPayable2" ValidationGroup="e" runat="server" CssClass="bubble" ErrorMessage="Invalid Amount"></asp:RegularExpressionValidator></td>
          </tr>
          <tr>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
          </tr>
      </table>
      </div>
    </div>
    <div id="tabs-4">
        <div style="height:500px; overflow:scroll;">
        
        <table >
            <tr>
                <td class="style3">
                    <asp:Label ID="Label64" runat="server" Text="Other Liabilites if any"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtLiabilities" runat="server" CssClass="twitterStyleTextbox" 
                        Height="100px" TextMode="MultiLine" Width="400px"></asp:TextBox>
                       
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style3">
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style3">
                    <asp:Label ID="Label72" runat="server" Text="Date"></asp:Label>
                </td>
                <td>
                 <div style="position:relative">
                    <asp:TextBox ID="txtLiability_Date" runat="server" 
                        CssClass="twitterStyleTextbox"></asp:TextBox>
                   <ajax:CalendarExtender  CssClass="ajaxcalendar"  ID="txtLiability_Date_CalendarExtender" runat="server"  Format="dd/MM/yyyy"
                        Enabled="True" TargetControlID="txtLiability_Date">
                    </ajax:CalendarExtender>
                    </div>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            
            <tr>
            <td>
            
            </td>
            <td>
            
            </td>
            <td>
           
            </td>
            </tr>
        </table>
            </div>
</div>
</div>
</div>
        <div class="footerR">
        <asp:Button ID="BtnSubscriber" runat="server" CssClass="GreenyPushButton"  OnClick="BtnSubscriber_Click"  ValidationGroup="aa" Text="Generate"></asp:Button>
          <asp:Button ID="btncancel" runat="server" CssClass="GreenyPushButton" Text="cancel"></asp:Button>

        </div>
        
        


    <asp:LinkButton ID="a" runat="server"></asp:LinkButton>

    <ajax:ModalPopupExtender ID="ModalPopupExtender1" TargetControlID="a" BackgroundCssClass="modalBackground" PopupControlID="Pnlmsg" runat="server">
    </ajax:ModalPopupExtender>

<asp:Panel  CssClass="raised" ID="Pnlmsg" runat="server"  Visible="false"  Width="600px" style="max-height:500px;min-height:300px" >
<asp:Label  runat ="server" ID="lblHint" Text="" Visible="false" ></asp:Label>

<div style="top: 0px;height:50px;text-align:center;padding:auto 0;width:100%"  class="boxfooter"  >

<asp:Label ID="lblHead" runat="server" Text=""  ></asp:Label>
</div>

<div id="Div1" style="max-height :400px; min-height:200px; overflow:auto;width:100%;">

<br />
<asp:Label  ID="lblContent" runat="server" Text="" style="text-align:justify;vertical-align:middle;margin-left:10px"  ></asp:Label>
<br />
<br />
 <asp:PlaceHolder ID="DynamicControlsHolder" runat="server" >
 
 
 
 
 
 
 </asp:PlaceHolder>
</div>
<div  class="boxheader" style="width:100%; bottom:0px;height:50px;position:absolute">

<div style="width:210px;margin:0 auto ;padding-top:10px ">

<asp:Button Width="100" style="margin:0 auto" CssClass="GreenyPushButton" ID="Button2"  OnClick="btnyes_Click" runat="server"  Text="OK"/>

<asp:Button Width="100" style="margin:0 auto" CssClass="GreenyPushButton" ID="Button3" OnClick="btnno_Click" runat="server"  Text="Cancel" />

</div>
</div>

  
  </asp:Panel>




















</div>


</asp:Content>
