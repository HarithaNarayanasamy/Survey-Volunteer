<%@ Page Culture="en-GB" Language="C#" MasterPageFile="~/Branch.Master" AutoEventWireup="true"
    CodeBehind="Guarantor2.aspx.cs" Inherits="SreeVisalamChitFundLtd_phase1.Guarantor2"
    Title="SVCF Admin Panel" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <script type="text/javascript" src="JqueryTab/JqueryTab_Min.js"></script>
    <script src="JqueryTab/JqueryTab.js" type="text/javascript"></script>
    <link href="JqueryTab/Tabcontainer.css" rel="stylesheet" type="text/css" />
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
    <style type="text/css">
    #ctl00_cphMainContent_ddlChitGroup1_chzn .chzn-drop .chzn-search input[type="text"]
    {
        height:15px;
    }
    #ctl00_cphMainContent_ddlChitGroup2_chzn .chzn-drop .chzn-search input[type="text"]
    {
        height:15px;
    }
    #ctl00_cphMainContent_ddlRightsofporperty_chzn .chzn-drop .chzn-search input[type="text"]
    {
        height:15px;
    }
    .chzn-results
    {
        text-align:center;
        height:130px;
    }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMainContent" runat="server">
    <link href="select2-3.4.2/select2.css" rel="stylesheet" type="text/css" />
    <script src="select2-3.4.2/select2.js" type="text/javascript"></script>
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
        <div class="twelve columns">
            <div class="box_c">
                <div class="box_c_heading  box_actions">
                    <p>
                        Guarantor Addition</p>
                </div>
                <div class="box_c_content">
                    <div class="row">
                        <ajax:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
                        </ajax:ToolkitScriptManager>
                        <asp:Panel ID="Panel2" runat="server" DefaultButton="BtnGuarantor">
                        <div style="margin: 0px auto; display: table; width: 100%;">
                        
                            <div id="tabs" runat="server" style="margin: 0px auto; display: table-cell;">
                                <ul>
                                    <li><a href="#tabs-1">Guarntor Surety</a></li>
                                    <li><a href="#tabs-2">Description and value of Immovable Properties</a></li>
                                    <li><a href="#tabs-3">Particulars of Business</a></li>
                                    <li><a href="#tabs-4">Other Liabilities</a></li>
                                </ul>
                                <div id="tabs-1">
                                    <div style="overflow: scroll; display: table; margin: 0px auto;">
                                        <table style="margin: 0px auto;">
                                            <tr>
                                                <td style="vertical-align:top;padding-top:6px;padding-right:5px;">
                                                    <asp:Label ID="Label1" runat="server" Text="Guarantor Name"></asp:Label>
                                                </td>
                                                <td style="vertical-align:top;padding-right:5px;">
                                                    <asp:TextBox TabIndex="1" placeholder="Guarantor Name" ToolTip="Ex. Muthu Pazhaniappan" ID="txtName" runat="server" CssClass="input-text ttip_r"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" Display="Dynamic" runat="Server"
                                                        ValidationGroup="aa" ControlToValidate="txtName" ErrorMessage="Enter Name"></asp:RequiredFieldValidator>
                                                </td>
                                                <td style="vertical-align:top;padding-top:6px;padding-right:5px;">
                                                    <asp:Label ID="Label2" runat="server" Text="Age"></asp:Label>
                                                </td>
                                                <td style="vertical-align:top;">
                                                    <asp:TextBox  TabIndex="2" MaxLength="2" onchange="check(this);" placeholder="Age" ToolTip="Ex. 50" ID="txtAge" runat="server" CssClass="input-text ttip_r sp_number"></asp:TextBox>
                                                </td>
                                                
                                            </tr>
                                            <tr>
                                                <td  style="vertical-align:top;padding-top:6px;padding-right:5px;">
                                                    <asp:Label ID="Label3" runat="server" Text="DOB"></asp:Label>
                                                </td>
                                                <td style="vertical-align:top;padding-right:5px;">
                                                        <asp:TextBox TabIndex="3" placeholder="DOB" ToolTip=""  ID="txtDOB" runat="server" CssClass="input-text ttip_r maskdate"></asp:TextBox>
                                                    <asp:CompareValidator Display="Dynamic" Type="Date" ControlToValidate="txtDOB"
                                                        Operator="DataTypeCheck" ID="CompareValidator1" runat="server" ErrorMessage="Enter Valid Date"></asp:CompareValidator>
                                                </td>
                                                <td style="vertical-align:top;padding-top:6px;padding-right:5px;">
                                                    <asp:Label ID="Label4" runat="server" Text="Father / Husband's Name"></asp:Label>
                                                </td>
                                                <td style="vertical-align:top;">
                                                    <asp:TextBox ID="txtFHusbandName" TabIndex="4" placeholder="Father / Husband's Name" ToolTip="Ex. Murugesan" runat="server" CssClass="input-text ttip_r"></asp:TextBox>
                                                </td>
                                                
                                            </tr>
                                            <tr>
                                                <td  style="vertical-align:top;padding-top:6px;padding-right:5px;">
                                                    <asp:Label ID="Label8" runat="server" Text="Phone Number"></asp:Label>
                                                </td>
                                                <td  style="vertical-align:top;padding-right:5px;">
                                                    <asp:TextBox MaxLength="15" TabIndex="5" placeholder="Phone Number" ToolTip="Ex. 0422-7575757 or 9876543210" ID="txtTelephoneNo" runat="server" CssClass="input-text sp_number ttip_r"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" Display="Dynamic" runat="Server"
                                                        ValidationGroup="aa" ControlToValidate="txtTelephoneNo" ErrorMessage="Enter Mobile Number"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator Display="Dynamic" ValidationGroup="aa" ValidationExpression="(\+91)?\d{2,}(-)?\d{6,}"
                                                        ControlToValidate="txtTelephoneNo" ID="RegularExpressionValidatoraaaaa" runat="server"
                                                        ErrorMessage="Enter Valid Phone Number"></asp:RegularExpressionValidator>
                                                </td>
                                                <td style="vertical-align:top;padding-top:6px;padding-right:5px;">
                                                    <asp:Label ID="Label9" runat="server" Text="Monthly Income"></asp:Label>
                                                </td>
                                                <td style="vertical-align:top;">
                                                    <asp:TextBox  TabIndex="6" placeholder="Monthly Income"  onchange="check(this);" ToolTip="Ex. 10000.00" ID="txtIncomeofMonth" runat="server" CssClass="input-text ttip_r sp_float"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td  style="vertical-align:top;padding-top:40px;padding-right:5px;">
                                                    <asp:Label ID="Label5" runat="server" Text="Native Address"></asp:Label>
                                                </td>
                                                <td  style="vertical-align:top;padding-right:5px;">
                                                    <asp:TextBox  TabIndex="7" placeholder="Native Address" ToolTip="Ex. 196/23, Thiruvenkatasamy Road (West),<br/> R.S.Puram,<br/> Coimbatore - 641002" ID="txtNativeAddress" runat="server" CssClass="input-text ttip_r" TextMode="MultiLine"></asp:TextBox>
                                                </td>
                                                <td style="vertical-align:top;padding-top:40px;padding-right:5px;">
                                                    <asp:Label ID="Label6" runat="server" Text="Residential Address"></asp:Label>
                                                </td>
                                                <td  style="vertical-align:top;">
                                                    <asp:TextBox  TabIndex="8" placeholder="Residential Address" ToolTip="Ex. 196/23, Thiruvenkatasamy Road (West),<br/> R.S.Puram,<br/> Coimbatore - 641002" ID="txtResidentialAddress" runat="server" CssClass="input-text ttip_r"
                                                        TextMode="MultiLine"></asp:TextBox>
                                                </td>
                                                
                                            </tr>
                                            <tr>
                                                <td  style="vertical-align:top;padding-top:40px;padding-right:5px;">
                                                    <asp:Label ID="Label7" runat="server" Text="Office Address"></asp:Label>
                                                </td>
                                                <td style="vertical-align:top;padding-right:5px;">
                                                    <asp:TextBox  TabIndex="9" placeholder="Office Address" ToolTip="Ex. 196/23, Thiruvenkatasamy Road (West),<br/> R.S.Puram,<br/> Coimbatore - 641002" ID="txtDesginationOfficeAddress" runat="server" CssClass="input-text ttip_r"
                                                        TextMode="MultiLine"></asp:TextBox>
                                                </td>
                                                <td style="vertical-align:top;padding-top:6px;padding-right:5px;">
                                                    <asp:Label ID="Label10" runat="server" Text="Basic Pay"></asp:Label>
                                                </td>
                                                <td style="vertical-align:top;">
                                                    <asp:TextBox  TabIndex="10" placeholder="Basic Pay" onchange="check(this);" ToolTip="Ex. 1000.00" ID="txtBasicPay" runat="server" CssClass="input-text ttip_r sp_float"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="vertical-align:top;padding-top:6px;padding-right:5px;">
                                                    <asp:Label ID="Label11" runat="server" Text="D.A."></asp:Label>
                                                </td>
                                                <td style="vertical-align:top;padding-right:5px;">
                                                    <asp:TextBox  TabIndex="11" placeholder="D.A." onchange="check(this);" ToolTip="Ex. 1000.00" ID="txtDARs" runat="server" CssClass="input-text ttip_r sp_float"></asp:TextBox>
                                                </td>
                                                <td  style="vertical-align:top;padding-top:6px;padding-right:5px;">
                                                    <asp:Label ID="Label12" runat="server" Text="O.A."></asp:Label>
                                                </td>
                                                <td style="vertical-align:top;">
                                                    <asp:TextBox  TabIndex="12" placeholder="O.A." onchange="check(this);" ToolTip="Ex. 1000.00" ID="txtOARs" runat="server" CssClass="input-text ttip_r sp_float"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="vertical-align:top;padding-top:6px;padding-right:5px;">
                                                    <asp:Label ID="Label66" runat="server" Text="Other Income"></asp:Label>
                                                </td>
                                                <td style="vertical-align:top;padding-right:5px;">
                                                    <asp:TextBox  TabIndex="13" placeholder="Other Income" onchange="check(this);" ToolTip="Ex.1000.00" ID="txtOtherIncome" runat="server" CssClass="input-text ttip_r sp_float"></asp:TextBox>
                                                </td>
                                                <td style="vertical-align:top;padding-top:6px;padding-right:5px;">
                                                    <asp:Label ID="Label67" runat="server" Text="AY"></asp:Label>
                                                </td>
                                                <td style="vertical-align:top;">
                                                    <asp:TextBox  TabIndex="14" placeholder="AY" ToolTip="-" ID="txtAY" runat="server" CssClass="input-text ttip_r"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="vertical-align:top;padding-top:6px;padding-right:5px;">
                                                    <asp:Label ID="Label68" runat="server" Text="AY Amount"></asp:Label>
                                                </td>
                                                <td style="vertical-align:top;padding-right:5px;">
                                                    <asp:TextBox  TabIndex="15" placeholder="AY Amount" onchange="check(this);" ToolTip="Ex.1000.00" ID="txtAY_Amount" runat="server" CssClass="input-text ttip_r sp_float"></asp:TextBox>
                                                </td>
                                                <td  style="vertical-align:top;padding-top:6px;padding-right:5px;">
                                                    <asp:Label ID="Label65" runat="server" Text="Relationship with Subscriber"></asp:Label>
                                                </td>
                                                <td  style="vertical-align:top;">
                                                    <asp:TextBox  TabIndex="16" placeholder="Relationship with Subscriber" ToolTip="Ex. Husband" ID="txtSubscriberandGuarantor" runat="server" CssClass="input-text ttip_r"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="vertical-align:top;padding-top:6px;padding-right:5px;">
                                                    <asp:Label ID="Label69" runat="server" Text="AY P.A. Number"></asp:Label>
                                                </td>
                                                <td style="vertical-align:top;padding-right:5px;">
                                                    <asp:TextBox  TabIndex="17" placeholder="AY P.A. Number" ToolTip="-" ID="txtAy_PANo" runat="server" CssClass="input-text ttip_r"></asp:TextBox>
                                                </td>
                                                <td style="vertical-align:top;padding-top:6px;padding-right:5px;">
                                                    <asp:Label ID="Label70" runat="server" Text="AY Office"></asp:Label>
                                                </td>
                                                <td style="vertical-align:top;">
                                                    <asp:TextBox  TabIndex="18" placeholder="AY Office" ToolTip="-" ID="txtAY_Office" runat="server" CssClass="input-text ttip_r"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                                <div id="tabs-2" >
                                    <div style="overflow: scroll; display: table; margin: 0px auto;">
                                        <table style="margin: 0px auto; display: table-cell;">
                                            <tr>
                                                <td style="vertical-align:top;padding-top:6px;padding-right:5px;">
                                                    <asp:Label ID="Label13" runat="server" Text="District"></asp:Label>
                                                </td>
                                                <td style="vertical-align:top;padding-right:5px;">
                                                    <asp:TextBox TabIndex="1" placeholder="District" ToolTip="Ex. Tirupur" ID="txtDistrict" runat="server" CssClass="input-text ttip_r"></asp:TextBox>
                                                </td>
                                                <td style="vertical-align:top;padding-top:6px;padding-right:5px;">
                                                    <asp:Label ID="Label14" runat="server" Text="Taluk"></asp:Label>
                                                </td>
                                                <td style="vertical-align:top;">
                                                    <asp:TextBox ID="txtTaluk" TabIndex="2" placeholder="Taluk" ToolTip="Ex. Pollachi" runat="server" CssClass="input-text ttip_r"></asp:TextBox>
                                                </td>
                                                
                                            </tr>
                                            <tr>
                                                <td style="vertical-align:top;padding-top:6px;padding-right:5px;">
                                                    <asp:Label ID="Label15" runat="server" Text="Village/Town"></asp:Label>
                                                </td>
                                                <td style="vertical-align:top;padding-right:5px;">
                                                    <asp:TextBox TabIndex="3" placeholder="Village/Town" ToolTip="Ex. Veethampatti" ID="txtVillageTown" runat="server" CssClass="input-text ttip_r"></asp:TextBox>
                                                </td>
                                                <td style="vertical-align:top;padding-top:6px;padding-right:5px;">
                                                    <asp:Label ID="Label16" runat="server" Text="Street"></asp:Label>
                                                </td>
                                                <td style="vertical-align:top;">
                                                    <asp:TextBox TabIndex="4" placeholder="Street" ToolTip="Ex. Mariamman Temple Street" ID="txtStreet" runat="server" CssClass="input-text ttip_r"></asp:TextBox>
                                                </td>
                                                
                                            </tr>
                                            <tr>
                                                <td style="vertical-align:top;padding-top:6px;padding-right:5px;">
                                                    <asp:Label ID="Label17" runat="server" Text="Ward Number"></asp:Label>
                                                </td>
                                                <td style="vertical-align:top;padding-right:5px;">
                                                    <asp:TextBox ID="txtWardNo" TabIndex="5" onchange="check(this);" ToolTip="Ex. 1" placeholder="Ward Number" runat="server" CssClass="input-text ttip_r sp_number"></asp:TextBox>
                                                </td>
                                                <td style="vertical-align:top;padding-top:6px;padding-right:5px;">
                                                    <asp:Label ID="Label18" runat="server" Text="Door Number"></asp:Label>
                                                </td>
                                                <td style="vertical-align:top;">
                                                    <asp:TextBox ID="txtDoorNo" TabIndex="6" onchange="check(this);" placeholder="Door Number" ToolTip="Ex. 202" runat="server" CssClass="input-text ttip_r sp_number"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="vertical-align:top;padding-top:6px;padding-right:5px;">
                                                    <asp:Label ID="Label19" runat="server" Text="Town Survey Number"></asp:Label>
                                                </td>
                                                <td style="vertical-align:top;padding-right:5px;">
                                                    <asp:TextBox ToolTip="-" placeholder="Town Survey Number" TabIndex="7" ID="txtTownSurveyNo" runat="server" CssClass="input-text ttip_r"></asp:TextBox>
                                                </td>
                                                <td style="vertical-align:top;padding-top:6px;padding-right:5px;">
                                                    <asp:Label ID="Label20" runat="server" Text="Registrar's Office"></asp:Label>
                                                </td>
                                                <td style="vertical-align:top;">
                                                    <asp:TextBox ID="txtRegisterOffice" TabIndex="8" placeholder="Registrar's Office" ToolTip="-" runat="server" CssClass="input-text ttip_r"></asp:TextBox>
                                                </td>
                                                
                                            </tr>
                                            <tr>
                                                <td style="vertical-align:top;padding-top:6px;padding-right:5px;">
                                                    <asp:Label ID="Label21" runat="server" Text="Sale Value of Property"></asp:Label>
                                                </td>
                                                <td style="vertical-align:top;padding-right:5px;">
                                                    <asp:TextBox ID="txtSaleValueofProperty" onchange="check(this);" ToolTip="Ex. 1000.00" placeholder="Sale Value of Property" TabIndex="9" runat="server" CssClass="input-text ttip_r sp_float"></asp:TextBox>
                                                </td>
                                                <td style="vertical-align:top;padding-top:6px;padding-right:5px;">
                                                    <asp:Label ID="Label22" runat="server" Text="Market Value as on"></asp:Label>
                                                </td>
                                                <td style="vertical-align:top;">
                                                        <asp:TextBox TabIndex="10" placeholder="Market Value as on" ToolTip="" ID="txtMarketValue" runat="server" CssClass="input-text ttip_r maskdate"></asp:TextBox>
                                                     <asp:CompareValidator ID="CompareValidator21" runat="server" Display="Dynamic" ControlToValidate="txtMarketValue"
                                                        ErrorMessage="Enter Valid Date" Operator="DataTypeCheck" Type="Date"></asp:CompareValidator>
                                                </td>
                                                
                                            </tr>
                                            <tr>
                                                <td style="vertical-align:top;padding-top:6px;padding-right:5px;">
                                                    <asp:Label ID="Label23" runat="server" Text="Rs"></asp:Label>
                                                </td>
                                                <td style="vertical-align:top;padding-right:5px;">
                                                    <asp:TextBox ID="txtRs1" runat="server" onchange="check(this);" placeholder="Rs." ToolTip="Ex.1000.00" TabIndex="11" CssClass="input-text ttip_r sp_float"></asp:TextBox>
                                                </td>
                                                <td style="vertical-align:top;padding-top:6px;padding-right:5px;">
                                                    <asp:Label ID="Label24" runat="server" Text="Tax Per Half Year"></asp:Label>
                                                </td>
                                                <td style="vertical-align:top;">
                                                    <asp:TextBox ID="txtTaxperHalfYear" onchange="check(this);" TabIndex="12" placeholder="Tax Per Half Year" ToolTip="Ex. 1000.00" runat="server" CssClass="input-text ttip_r sp_float"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="vertical-align:top;padding-top:6px;padding-right:5px;">
                                                    <asp:Label ID="Label25" runat="server" Text="Tax Recepit Number"></asp:Label>
                                                </td>
                                                <td style="vertical-align:top;padding-right:5px;">
                                                    <asp:TextBox ID="txtTaxReceiptNo" placeholder="Tax Recepit Number" ToolTip="-" TabIndex="13" runat="server" CssClass="input-text ttip_r"></asp:TextBox>
                                                </td>
                                                <td style="vertical-align:top;padding-top:6px;padding-right:5px;">
                                                    <asp:Label ID="Label26" runat="server" Text="Tax Date"></asp:Label>
                                                </td>
                                                <td style="vertical-align:top;">
                                                        <asp:TextBox ID="txtReceiptDate" placeholder="Tax Date" TabIndex="14" runat="server" CssClass="input-text ttip_r maskdate"></asp:TextBox>
                                                    <asp:CompareValidator ID="CompareValidator16" runat="server" Display="Dynamic" ControlToValidate="txtReceiptDate"
                                                        Operator="DataTypeCheck" Type="Date" ErrorMessage="Enter Valid Date"></asp:CompareValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="vertical-align:top;padding-top:6px;padding-right:5px;">
                                                    <asp:Label ID="Label27" runat="server" Text="Tax Amount"></asp:Label>
                                                </td>
                                                <td style="vertical-align:top;padding-right:5px;">
                                                    <asp:TextBox ID="txtTax_Amount" onchange="check(this);" placeholder="Tax Amount" TabIndex="15" ToolTip="Ex. 1000.00" runat="server" CssClass="input-text ttip_r sp_float"></asp:TextBox>
                                                </td>
                                                <td style="vertical-align:top;padding-top:6px;padding-right:5px;">
                                                    <asp:Label ID="Label28" runat="server" Text="Rental Income per Month"></asp:Label>
                                                </td>
                                                <td style="vertical-align:top;">
                                                    <asp:TextBox ID="txtRentalIncome" onchange="check(this);" placeholder="Rental Income per Month" ToolTip="Ex. 1000.00" TabIndex="16" runat="server" CssClass="input-text ttip_r sp_float"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="vertical-align:top;padding-top:6px;padding-right:5px;">
                                                    <asp:Label ID="Label29" runat="server" Text="Encumbrance Certificate"></asp:Label>
                                                </td>
                                                <td style="vertical-align:top;padding-right:5px;">
                                                    <asp:TextBox placeholder="Encumbrance Certificate" ToolTip="-" TabIndex="17"  ID="txtEncumbrance" runat="server" CssClass="input-text ttip_r"></asp:TextBox>
                                                </td>
                                                <td style="vertical-align:top;padding-top:6px;padding-right:5px;">
                                                    <asp:Label ID="Label30" runat="server" Text="Rights of Property"></asp:Label>
                                                </td>
                                                <td style="vertical-align:top;">
                                                    <asp:DropDownList TabIndex="18" Width="240px" ID="ddlRightsofporperty" runat="server" CssClass="chzn-select">
                                                        <asp:ListItem Text="--select--" Value="--select--"></asp:ListItem>
                                                        <asp:ListItem Text="Full" Value="Full"></asp:ListItem>
                                                        <asp:ListItem Text="Three fourth" Value="Three fourth"></asp:ListItem>
                                                        <asp:ListItem Text="Half" Value="Half"></asp:ListItem>
                                                        <asp:ListItem Text="Quarter Portion" Value="Quarter Portion"></asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:CompareValidator ID="CompareValidator5" runat="server" ControlToValidate="ddlRightsofporperty" Display="Dynamic"
                                                        Operator="NotEqual" ValueToCompare="--select--" ErrorMessage="Select Valid one"></asp:CompareValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="vertical-align:top;padding-right:5px;">
                                                    <asp:Label ID="Label31" runat="server" Text="Whether any Condition is attached to property"></asp:Label>
                                                </td>
                                                <td style="vertical-align:top;padding-right:5px;">
                                                    <asp:TextBox  placeholder="Condition attached to property" ToolTip="-" TabIndex="19"  ID="txtConditionAttachedProperty" runat="server" CssClass="input-text ttip_r"></asp:TextBox>
                                                </td>
                                                <td style="vertical-align:top;padding-top:6px;padding-right:5px;">
                                                    <asp:Label ID="Label36" runat="server" Text="Other Details Regarding House Properties"></asp:Label>
                                                </td>
                                                <td style="vertical-align:top;">
                                                    <asp:TextBox  placeholder="Details Regarding House Properties" ToolTip="-" TabIndex="20"  ID="txtRegardingHouseProperty" runat="server" CssClass="input-text ttip_r"></asp:TextBox>
                                                </td>
                                                
                                            </tr>
                                            <tr>
                                                <td style="vertical-align:top;padding-top:6px;padding-right:5px;">
                                                    <asp:Label ID="Label37" runat="server" Text="Details of Assets Value"></asp:Label>
                                                </td>
                                                <td style="vertical-align:top;padding-right:5px;">
                                                    <asp:TextBox  placeholder="Details of Assets Value" ToolTip="-" TabIndex="21"  ID="txtDetailsAssetValue" runat="server" CssClass="input-text ttip_r"></asp:TextBox>
                                                </td>
                                                <td style="vertical-align:top;padding-top:6px;padding-right:5px;">
                                                    <asp:Label ID="Label32" runat="server" Text="East of"></asp:Label>
                                                </td>
                                                <td style="vertical-align:top;">
                                                    <asp:TextBox placeholder="East of" ToolTip="-" TabIndex="22"  ID="txtEast" runat="server" CssClass="input-text ttip_r"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="vertical-align:top;padding-top:6px;padding-right:5px;">
                                                    <asp:Label ID="Label33" runat="server" Text="West of"></asp:Label>
                                                </td>
                                                <td style="vertical-align:top;padding-right:5px;">
                                                    <asp:TextBox  placeholder="West of" ToolTip="-" TabIndex="23"  ID="txtWest" runat="server" CssClass="input-text ttip_r"></asp:TextBox>
                                                </td>
                                                <td style="vertical-align:top;padding-top:6px;padding-right:5px;">
                                                    <asp:Label ID="Label34" runat="server" Text="South of"></asp:Label>
                                                </td>
                                                <td style="vertical-align:top;">
                                                    <asp:TextBox placeholder="South of" ToolTip="-" TabIndex="24"  ID="txtSouth" runat="server" CssClass="input-text ttip_r"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="vertical-align:top;padding-top:6px;padding-right:5px;">
                                                    <asp:Label ID="Label35" runat="server" Text="North of"></asp:Label>
                                                </td>
                                                <td style="vertical-align:top;padding-right:5px;">
                                                    <asp:TextBox  placeholder="North of" ToolTip="-" TabIndex="25"  ID="txtNorth" runat="server" CssClass="input-text ttip_r"></asp:TextBox>
                                                </td>
                                                <td>
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                                <div id="tabs-3">
                                    <div style="overflow: scroll; display: table; margin: 0px auto;">
                                        <table cellspacing="3" style="margin: 0px auto; display: table-cell;">
                                            <tr>
                                                <td style="vertical-align: top; padding-top: 40px; padding-right: 5px;">
                                                    <asp:Label ID="Label38" runat="server" Text="Name of the Firm and Address"></asp:Label>
                                                </td>
                                                <td style="padding-right:5px;">
                                                    <asp:TextBox TabIndex="1" ID="txtFirmAddress" placeholder="Name of the Firm and Address" ToolTip="Ex. ALTIUS TECHNOLOGIES<br/>#56-C bharathi park,<br/> 2 nd cross road,<br/> Saibaba Colony,<br/>Coimbatore-641011" runat="server" CssClass="input-text ttip_r" TextMode="MultiLine"></asp:TextBox>
                                                </td>
                                                <td style="vertical-align:top;padding-right:5px;padding-top:6px;">
                                                    <asp:Label ID="Label39" runat="server" Text="Business Started Year"></asp:Label>
                                                </td>
                                                <td style="vertical-align:top;">
                                                    <asp:TextBox TabIndex="2" ID="txtBusinessStartedYear" onchange="check(this);" placeholder="Business Started Year" ToolTip="Ex. 1999" runat="server" MaxLength="4" CssClass="input-text ttip_r sp_number"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="vertical-align:top;padding-top:6px;padding-right:5px;">
                                                    <asp:Label ID="Label40" runat="server" Text="Nature of Business"></asp:Label>
                                                </td>
                                                <td style="vertical-align:top;padding-right:5px;">
                                                    <asp:TextBox TabIndex="3" placeholder="Nature of Business" ToolTip="Ex. IT" ID="txtNatureofBusiness" runat="server" CssClass="input-text ttip_r"></asp:TextBox>
                                                </td>
                                                <td style="vertical-align:top;padding-top:6px;padding-right:5px;">
                                                    <asp:Label ID="Label41" runat="server" Text="Firm Regn.No/Trade Licence No."></asp:Label>
                                                </td>
                                                <td style="vertical-align:top;">
                                                    <asp:TextBox TabIndex="4" ID="txtTradeLicenceNo" placeholder="Firm Regn.No/Trade Licence No." ToolTip="-" runat="server" CssClass="input-text ttip_r"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="vertical-align:top;padding-right:5px;padding-top:6px;">
                                                    <asp:Label ID="Label42" runat="server" Text="Office"></asp:Label>
                                                </td>
                                                <td style="vertical-align:top;padding-right:5px;">
                                                    <asp:TextBox TabIndex="5" ID="txtPBOffice1" placeholder="Office" ToolTip="-" runat="server" CssClass="input-text ttip_r"></asp:TextBox>
                                                </td>
                                                <td style="vertical-align:top;padding-top:6px;padding-right:5px;">
                                                    <asp:Label ID="Label43" runat="server" Text="Central Sales Tax Regn.No"></asp:Label>
                                                </td>
                                                <td  style="vertical-align:top;">
                                                    <asp:TextBox TabIndex="6" ID="txtCentralSalesTaxRegnNo" placeholder="Central Sales Tax Regn.No" ToolTip="-" runat="server" CssClass="input-text ttip_r"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="vertical-align:top;padding-right:5px;padding-top:6px;">
                                                    <asp:Label ID="Label44" runat="server" Text="Tin.Reg. Number"></asp:Label>
                                                </td>
                                                <td  style="vertical-align:top;padding-right:5px;">
                                                    <asp:TextBox TabIndex="7" ID="txtTinRegNo" ToolTip="-" placeholder="Tin.Reg.No." runat="server" CssClass="input-text ttip_r"></asp:TextBox>
                                                </td>
                                                <td style="vertical-align:top;padding-right:5px;padding-top:6px;">
                                                    <asp:Label ID="Label45" runat="server" Text="Average Commercial Tax per Year"></asp:Label>
                                                </td>
                                                <td style="vertical-align:top;">
                                                    <asp:TextBox TabIndex="8" ToolTip="Ex. 1000.00" onchange="check(this);" placeholder="Average Commercial Tax per Year" ID="txtCommericalTaxperYear" runat="server" CssClass="input-text ttip_r sp_float"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="vertical-align:top;padding-right:5px;padding-top:6px;">
                                                    <asp:Label ID="Label46" runat="server" Text="Share"></asp:Label>
                                                </td>
                                                <td style="vertical-align:top;padding-right:5px;">
                                                    <asp:TextBox TabIndex="9" placeholder="Share" onchange="check(this);" ID="txtShare" ToolTip="-" runat="server" CssClass="input-text ttip_r sp_float"></asp:TextBox>
                                                </td>
                                                <td style="vertical-align:top;padding-right:5px;padding-top:6px;">
                                                    <asp:Label ID="Label47" runat="server" Text="Capital"></asp:Label>
                                                </td>
                                                <td style="vertical-align:top;">
                                                    <asp:TextBox TabIndex="10" ID="txtCaptial" placeholder="Capital" ToolTip="-" runat="server" CssClass="input-text ttip_r"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="vertical-align:top;padding-right:5px;padding-top:6px;">
                                                    <asp:Label ID="Label48" runat="server" Text="Designation"></asp:Label>
                                                </td>
                                                <td style="vertical-align:top;padding-right:5px;">
                                                    <asp:TextBox TabIndex="11" placeholder="Designation" ToolTip="-" ID="txtDesignation" runat="server" CssClass="input-text ttip_r"></asp:TextBox>
                                                </td>
                                                <td style="vertical-align:top;padding-right:5px;padding-top:6px;">
                                                    <asp:Label ID="Label49" runat="server" Text="Partner's Details"></asp:Label>
                                                </td>
                                                <td style="vertical-align:top;">
                                                    <asp:TextBox TabIndex="12" placeholder="Partner's Details" ToolTip="-" ID="txtPartnersDetails" runat="server" CssClass="input-text ttip_r"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="vertical-align:top;padding-right:5px;padding-top:6px;">
                                                    <asp:Label ID="Label56" runat="server" Text="Chit Number"></asp:Label>
                                                </td>
                                                <td style="vertical-align:top;padding-right:5px;">
                                                    <asp:DropDownList TabIndex="13" Width="240px" ID="ddlChitGroup1" CssClass="chzn-select" runat="server">
                                                    </asp:DropDownList>
                                                </td>
                                                <td style="vertical-align:top;padding-right:5px;padding-top:6px;">
                                                    <asp:Label ID="Label57" runat="server" Text="Value"></asp:Label>
                                                </td>
                                                <td style="vertical-align:top;">
                                                    <asp:TextBox TabIndex="14" ID="txtPBValue" onchange="check(this);" placeholder="Value" ToolTip="Ex. 1000.00" runat="server" CssClass="input-text ttip_r sp_float"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="vertical-align:top;padding-right:5px;padding-top:6px;">
                                                    <asp:Label ID="Label58" runat="server" Text="Amount Remitted upto"></asp:Label>
                                                </td>
                                                <td style="vertical-align:top;padding-right:5px;">
                                                    <asp:TextBox TabIndex="15" onchange="check(this);" placeholder="Amount Remitted upto" ToolTip="Ex. 1000.00"  ID="txtAmountRemitted" runat="server" CssClass="input-text ttip_r sp_float"></asp:TextBox>
                                                </td>
                                                <td style="vertical-align:top;padding-right:5px;padding-top:6px;">
                                                    <asp:Label ID="Label59" runat="server" Text="Installment"></asp:Label>
                                                </td>
                                                <td style="vertical-align:top;">
                                                    <asp:TextBox TabIndex="16" onchange="check(this);" ID="txtPBInstallment" ToolTip="Ex. 1000.00" placeholder="Instalment" runat="server" CssClass="input-text ttip_r sp_float"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="vertical-align:top;padding-right:5px;padding-top:6px;">
                                                    <asp:Label ID="Label60" runat="server" Text="Future Instalment Payable"></asp:Label>
                                                </td>
                                                <td style="vertical-align:top;padding-right:5px;">
                                                    <asp:TextBox TabIndex="17" onchange="check(this);" placeholder="Future Instalment Payable" ToolTip="Ex. 1000.00" ID="txtFutuerInstalmentPayable1" runat="server" CssClass="input-text ttip_r sp_float"></asp:TextBox>
                                                </td>
                                                <td style="vertical-align:top;padding-right:5px;padding-top:6px;">
                                                    <asp:Label ID="Label61" runat="server" Text="Stands as Surety of Chit Number"></asp:Label>
                                                </td>
                                                <td style="vertical-align:top;">
                                                    <asp:DropDownList TabIndex="18" Width="240px" ID="ddlChitGroup2" CssClass="chzn-select" runat="server">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="vertical-align:top;padding-right:5px;padding-top:6px;">
                                                    <asp:Label ID="Label62" runat="server" Text="Value"></asp:Label>
                                                </td>
                                                <td style="vertical-align:top;padding-right:5px;">
                                                    <asp:TextBox TabIndex="19" ID="txtChitNoValue" onchange="check(this);" ToolTip="Ex. 1000.00" placeholder="Value" runat="server" CssClass="input-text ttip_r sp_float"></asp:TextBox>
                                                </td>
                                                <td style="vertical-align:top;padding-right:5px;padding-top:6px;">
                                                    <asp:Label ID="Label63" runat="server" Text="Future Instalment Payable"></asp:Label>
                                                </td>
                                                <td style="vertical-align:top;">
                                                    <asp:TextBox TabIndex="20" ID="txtFutureInstallmentPayable2" onchange="check(this);" placeholder="Future Instalment Payable" ToolTip="Ex.1000.00" runat="server" CssClass="input-text ttip_r sp_float"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="vertical-align:top;padding-right:5px;padding-top:6px;">
                                                    <asp:Label ID="Label50" runat="server" Text="Networth of the Business"></asp:Label>
                                                </td>
                                                <td style="vertical-align:top;padding-right:5px;">
                                                    <asp:TextBox TabIndex="21" onchange="check(this);" placeholder="Networth of the Business" ToolTip="Ex. 1000.00" ID="txtNetWorthBusiness" runat="server" CssClass="input-text ttip_r sp_float"></asp:TextBox>
                                                </td>
                                                <td style="vertical-align:top;padding-right:5px;padding-top:6px;">
                                                    <asp:Label ID="Label51" runat="server" Text="Average Annual Income"></asp:Label>
                                                </td>
                                                <td style="vertical-align:top;">
                                                    <asp:TextBox TabIndex="22" onchange="check(this);" ID="txtAverageAnnualIncome" placeholder="Average Annual Income" ToolTip="Ex. 1000.00" runat="server" CssClass="input-text ttip_r sp_float"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="vertical-align:top;padding-right:5px;padding-top:6px;">
                                                    <asp:Label ID="Label52" runat="server" Text="IT P.A.No.of the Firm"></asp:Label>
                                                </td>
                                                <td style="vertical-align:top;padding-right:5px;">
                                                    <asp:TextBox TabIndex="23" ToolTip="-" placeholder="IT P.A.No.of the Firm" ID="txtIncomeTaxPANoofFirm" runat="server" CssClass="input-text ttip_r"></asp:TextBox>
                                                </td>
                                                <td style="vertical-align:top;padding-right:5px;padding-top:6px;">
                                                    <asp:Label ID="Label53" runat="server" Text="Office"></asp:Label>
                                                </td>
                                                <td style="vertical-align:top;">
                                                    <asp:TextBox TabIndex="24" ToolTip="-" placeholder="Office" ID="txtIncomeTaxOffice" runat="server" CssClass="input-text ttip_r"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="vertical-align:top;padding-right:5px;padding-top:6px;">
                                                    <asp:Label ID="Label54" runat="server" Text="IT paid for last Two Years"></asp:Label>
                                                </td>
                                                <td style="vertical-align:top;padding-right:5px;">
                                                    <asp:TextBox TabIndex="25" onchange="check(this);" placeholder="IT paid for last Two Years" ToolTip="Ex. 1000.00" ID="txtAssessmentYear" runat="server" CssClass="input-text ttip_r sp_float"></asp:TextBox>
                                                </td>
                                                <td style="vertical-align:top;padding-right:5px;padding-top:6px;">
                                                    <asp:Label ID="Label55" runat="server" Text="Rs"></asp:Label>
                                                </td>
                                                <td style="vertical-align:top;">
                                                    <asp:TextBox TabIndex="26" onchange="check(this);" ID="txtAssessment_Year_Rs1" ToolTip="Ex.1000.00" placeholder="Rs." runat="server" CssClass="input-text ttip_r sp_float"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                                <div id="tabs-4">
                                    <div style="padding-bottom: 200px; overflow: scroll; display: table; margin: 0px auto;">
                                        <table cellspacing="3" style="margin: 0px auto; display: table-cell;">
                                            <tr>
                                                <td style="padding: 5px; padding-top: 40px; vertical-align: top;">
                                                    <asp:Label ID="Label64" runat="server" Text="Other Liabilites if any"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox TabIndex="1" ID="txtLiabilities" runat="server" CssClass="input-text ttip_r" ToolTip="-"
                                                        placeholder="Other Liabilites" TextMode="MultiLine"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="padding: 5px;">
                                                    <asp:Label ID="Label72" runat="server" Text="Date"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox TabIndex="2" ID="txtLiability_Date" runat="server" placeholder="Date" CssClass="input-text ttip_r maskdate"></asp:TextBox>
                                                    <asp:CompareValidator Display="Dynamic" Type="Date" ControlToValidate="txtLiability_Date"
                                                        Operator="DataTypeCheck" ID="CompareValida2" runat="server" ErrorMessage="Enter Valid Date"></asp:CompareValidator>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div style="text-align:center;">
                            <asp:Button ID="BtnGuarantor" runat="server" CssClass="GreenyPushButton" OnClick="BtnGuarantor_Click"
                                Style="margin: 0px auto;" ValidationGroup="aa" Text="Generate"></asp:Button>
                            <asp:Button ID="btncancel" runat="server" CausesValidation="false" CssClass="GreenyPushButton"
                                OnClientClick="clearValidationErrors();" OnClick="btnCancel_load" Style="margin: 0px auto;"
                                Text="cancel"></asp:Button>
                         </div>
                         </asp:Panel>
                    </div>
                    
                </div>
            </div>
        </div>
    </div>
    <asp:LinkButton ID="a" runat="server"></asp:LinkButton>
    <ajax:ModalPopupExtender ID="ModalPopupExtender1"
        TargetControlID="a" BackgroundCssClass="modalBackground" PopupControlID="Pnlmsg"
        runat="server">
    </ajax:ModalPopupExtender>
    <asp:Panel CssClass="raised" ID="Pnlmsg" runat="server" Visible="false" Width="500px"
        Style="margin: 0px auto;">
        <asp:Label runat="server" ID="lblHint" Text="" Visible="false"></asp:Label>
        <div style="top: 0px; height: 50px; text-align: center; padding: auto 0; width: 100%"
            class="boxfooter">
            <asp:Label ID="lblHead" runat="server" Text=""></asp:Label>
        </div>
        <div id="Div1" style="max-height: 200px; text-align:center; overflow: auto; width: 100%;">
            <br />
            <br />
            <asp:Label ID="lblContent" runat="server" Text=""></asp:Label>
            <br />
            <br />
            <asp:PlaceHolder ID="DynamicControlsHolder" runat="server"></asp:PlaceHolder>
        </div>
        <div class="boxheader">
            <asp:Button Style="margin: 0 auto" CssClass="GreenyPushButton" ID="Button2" CausesValidation="false"
                OnClick="btnyes_Click" runat="server" Text="OK" />
        </div>
    </asp:Panel>
    <asp:Panel CssClass="raised" ID="Panel1" runat="server" Visible="false" Width="600px"
        Style="margin: 0px auto;">
        <asp:Label runat="server" ID="Label71" Text="" Visible="false"></asp:Label>
        <div style="top: 0px; height: 50px; text-align: center; padding: auto 0; width: 100%"
            class="boxfooter">
            <asp:Label ID="Label73" runat="server" Text=""></asp:Label>
        </div>
        <div id="Div2" style="max-height: 200px;text-align:center;">
            <br />
            <br />
            <asp:Label ID="Label74" runat="server" Text="" ></asp:Label>
            <br />
            <br />
            <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
        </div>
        <div class="boxheader">
            <%--<asp:Button Style="margin: 0 auto" CssClass="GreenyPushButton" ID="Button1" CausesValidation="false"
                OnClick="btnConfirm_Click" runat="server" Text="Ok" />--%>
            <asp:Button Style="margin: 0 auto" CssClass="GreenyPushButton" ID="Button3" CausesValidation="false"
                OnClick="btnno_Click" runat="server" Text="Cancel" />
        </div>
    </asp:Panel>
    <script type="text/javascript">
        $(document).ready(function () {

            $(".chzn-select").chosen({ search_contains: true });
            $(".sp_number").numeric({
                decimal: false,
                negative: false
            },
                function () {
                    alert("Positive integers only");
                    this.value = "";
                    this.focus();
                });
            $(".sp_float").numeric({
                negative: false
            },
                function () {
                    alert("Positive only");
                    this.value = "";
                    this.focus();
                });
            prth_mask_input.init();
        });

        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {

            $(".chzn-select").chosen({ search_contains: true });
            $(".sp_number").numeric({
                decimal: false,
                negative: false
            },
                function () {
                    alert("Positive integers only");
                    this.value = "";
                    this.focus();
                });
            $(".sp_float").numeric({
                negative: false
            },
                function () {
                    alert("Positive only");
                    this.value = "";
                    this.focus();
                });
            prth_mask_input.init();
            prth_tips.init();
        });

    </script>
    <script type="text/javascript">
        function check(txt) {
            if (txt.value.toString() == "") {
                document.getElementById('<%= txtAge.ClientID %>').value = 0;
            }
            if (txt.value.toString() == "") {
                document.getElementById('<%= txtIncomeofMonth.ClientID %>').value = 0;
            }
            if (txt.value.toString() == "") {
                document.getElementById('<%= txtBasicPay.ClientID %>').value = 0;
            }
            if (txt.value.toString() == "") {
                document.getElementById('<%= txtDARs.ClientID %>').value = 0;
            }
            if (txt.value.toString() == "") {
                document.getElementById('<%= txtOARs.ClientID %>').value = 0;
            }
            if (txt.value.toString() == "") {
                document.getElementById('<%= txtOtherIncome.ClientID %>').value = 0;
            }
            if (txt.value.toString() == "") {
                document.getElementById('<%= txtAY_Amount.ClientID %>').value = 0;
            }
            if (txt.value.toString() == "") {
                document.getElementById('<%= txtWardNo.ClientID %>').value = 0;
            }
            if (txt.value.toString() == "") {
                document.getElementById('<%= txtDoorNo.ClientID %>').value = 0;
            }
            if (txt.value.toString() == "") {
                document.getElementById('<%= txtSaleValueofProperty.ClientID %>').value = 0;
            }
            if (txt.value.toString() == "") {
                document.getElementById('<%= txtRs1.ClientID %>').value = 0;
            }
            if (txt.value.toString() == "") {
                document.getElementById('<%= txtTaxperHalfYear.ClientID %>').value = 0;
            }
            if (txt.value.toString() == "") {
                document.getElementById('<%= txtTax_Amount.ClientID %>').value = 0;
            }
            if (txt.value.toString() == "") {
                document.getElementById('<%= txtRentalIncome.ClientID %>').value = 0;
            }
            if (txt.value.toString() == "") {
                document.getElementById('<%= txtBusinessStartedYear.ClientID %>').value = 0;
            }
            if (txt.value.toString() == "") {
                document.getElementById('<%= txtCommericalTaxperYear.ClientID %>').value = 0;
            }
            if (txt.value.toString() == "") {
                document.getElementById('<%= txtShare.ClientID %>').value = 0;
            }
            if (txt.value.toString() == "") {
                document.getElementById('<%= txtPBValue.ClientID %>').value = 0;
            }
            if (txt.value.toString() == "") {
                document.getElementById('<%= txtAmountRemitted.ClientID %>').value = 0;
            }
            if (txt.value.toString() == "") {
                document.getElementById('<%= txtPBInstallment.ClientID %>').value = 0;
            }
            if (txt.value.toString() == "") {
                document.getElementById('<%= txtFutuerInstalmentPayable1.ClientID %>').value = 0;
            }
            if (txt.value.toString() == "") {
                document.getElementById('<%= txtChitNoValue.ClientID %>').value = 0;
            }
            if (txt.value.toString() == "") {
                document.getElementById('<%= txtFutureInstallmentPayable2.ClientID %>').value = 0;
            }
            if (txt.value.toString() == "") {
                document.getElementById('<%= txtNetWorthBusiness.ClientID %>').value = 0;
            }
            if (txt.value.toString() == "") {
                document.getElementById('<%= txtAverageAnnualIncome.ClientID %>').value = 0;
            }
            if (txt.value.toString() == "") {
                document.getElementById('<%= txtAssessmentYear.ClientID %>').value = 0;
            }
            if (txt.value.toString() == "") {
                document.getElementById('<%= txtAssessment_Year_Rs1.ClientID %>').value = 0;
            }
        }
    </script>
</asp:Content>
