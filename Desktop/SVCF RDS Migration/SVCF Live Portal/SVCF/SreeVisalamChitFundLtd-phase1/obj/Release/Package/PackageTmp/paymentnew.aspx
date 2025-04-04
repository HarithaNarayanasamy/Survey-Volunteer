<%@ Page Culture="en-GB" Language="C#" MasterPageFile="~/Branch.Master" AutoEventWireup="true"
    CodeBehind="paymentnew.aspx.cs" Inherits="SreeVisalamChitFundLtd_phase1.paymentnew"
    Title="SVCF Admin Panel" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <style type="text/css">
        .chzn-drop
        {
            border: 1px solid #aaa !important;
            padding-top: 5px !important;
            width: 240px !important;
        }

        .chzn-results
        {
            text-align:center;
        }
        #ctl00_cphMainContent_ddlGroupNumber_chzn .chzn-drop .chzn-search input[type="text"]
        {
            height:15px;
        }
        #ctl00_cphMainContent_ddlMemberName_chzn .chzn-drop .chzn-search input[type="text"]
        {
            height:15px;
        }
        #ctl00_cphMainContent_ddlGuarantor_chzn .chzn-drop .chzn-search input[type="text"]
        {
            height:15px;
        }
        #ctl00_cphMainContent_ddlBankName_chzn .chzn-drop .chzn-search input[type="text"]
        {
            height:15px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMainContent" runat="server">
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
    <ajax:ToolkitScriptManager ID="scm1" runat="server">
    </ajax:ToolkitScriptManager>
    <div class="row">
        <div class="twelve columns">
            <div class="box_c">
                <div class="box_c_heading  box_actions">
                    <p>
                        Payment</p>
                </div>
                <div class="box_c_content">
                    <asp:Panel runat="server" DefaultButton="btnPayment">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <div class="row">
                                    <div style="width: 100%">
                                        <table style="display: table; margin: 0px auto;">
                                            <tr>
                                                <td>
                                                    <asp:Label ID="labelSeries" runat="server" Text="Receipt Series"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox TabIndex="1" placeholder="Payment" ToolTip="Ex. KR" CssClass="input-text"
                                                        ID="txtSeries" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ErrorMessage="Invalid Receipt Series"
                                                        ControlToValidate="txtSeries" Display="Dynamic" ValidationGroup="pa" runat="server"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ControlToValidate="txtSeries" ValidationExpression="^[A-Z]+$"
                                                        ID="RegularExpressionValidator1" Display="Dynamic" runat="server" ErrorMessage="Enter Capital Letters"></asp:RegularExpressionValidator>
                                                </td>
                                                <td>
                                                    <asp:Label ID="labelPaymentNumber" runat="server" Text="Payment Number"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox CausesValidation="true" ToolTip="Ex. 1000" placeholder="Payment Number" TabIndex="2" runat="server"
                                                        ID="txtPaymentNumber" CssClass="input-text sp_number">
                                                    </asp:TextBox>
                                                   <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator13" ForeColor="Red" ErrorMessage="Enter Payment Number"
                                                        ControlToValidate="txtPaymentNumber" Display="Dynamic" ValidationGroup="pa" runat="server"></asp:RequiredFieldValidator>
                                                    <asp:CustomValidator ID="CustomValidator2" runat="server" ForeColor="Red" ValidationGroup="pa" ControlToValidate="txtPaymentNumber"
                                                        OnServerValidate="txtPaymentNumber_Validate" Display="Dynamic"></asp:CustomValidator>--%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label3" runat="server" Text="Chit Group"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList onchange="clearValidationErrors();" TabIndex="3" Width="240px" CssClass="chzn-select" ID="ddlGroupNumber"
                                                        runat="server" AutoPostBack="true" OnSelectedIndexChanged="load_ddlGroupNumber">
                                                    </asp:DropDownList>
                                                    <asp:CompareValidator ID="CompareValidator11" ValueToCompare="0" ValidationGroup="pa"
                                                        ControlToValidate="ddlGroupNumber" Display="Dynamic" Operator="NotEqual" runat="server"
                                                        ErrorMessage="Select Chit Group"></asp:CompareValidator>
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label4" runat="server" Text="Member Name"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList  onchange="clearValidationErrors();"  TabIndex="4" AutoPostBack="true" OnSelectedIndexChanged="load_ddlMemberName"
                                                        Width="240px" CssClass="chzn-select" ID="ddlMemberName" runat="server">
                                                    </asp:DropDownList>
                                                    <asp:CompareValidator ID="CompareValidator10" ValueToCompare="--Select--" ControlToValidate="ddlMemberName"
                                                        ValidationGroup="pa" Display="Dynamic" Operator="NotEqual" runat="server" ErrorMessage="Select Member Name"></asp:CompareValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="label11" runat="server" Text="Draw Number"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox TabIndex="5" placeholder="Draw Number" ToolTip="Ex. 50" CssClass="input-text"
                                                        ID="txtDrawNumber" runat="server" ReadOnly="True" ></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" ErrorMessage="Enter Draw Number"
                                                        ControlToValidate="txtDrawNumber" ValidationGroup="pa" Display="Dynamic" runat="server"></asp:RequiredFieldValidator>
                                                </td>
                                                <td>
                                                    <asp:Label ID="label10" runat="server" Text="Guarantor Name"></asp:Label>
                                                </td>
                                                <td>
                                                  <asp:RadioButtonList ID="GR_Type" AutoPostBack="true" OnSelectedIndexChanged="GR_Type_SelectedIndexChanged" 
                                                        runat="server" RepeatDirection="Horizontal">
                                                         <asp:ListItem Text="Join" Value="Join"></asp:ListItem>
                                                        <asp:ListItem  Text="Single" Value="Single"></asp:ListItem>                                                       
                                                    </asp:RadioButtonList>                                       
                                                    <asp:RequiredFieldValidator runat="server" ID="rvgrtype" Display="Dynamic"
                                                            ControlToValidate="GR_Type" ErrorMessage="Select Guarantor"
                                                            ValidationGroup="pa">*</asp:RequiredFieldValidator>
                                                      <asp:TextBox TabIndex="5" placeholder="Gurantor Name" ToolTip="Ex. 50" CssClass="input-text"
                                                        ID="txtGurantor" runat="server" Visible="false" ></asp:TextBox>                                                 
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="label8" runat="server" Text="Payment On"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtDate" TabIndex="7" CssClass="input-text maskdate" placeholder="Payment Date"
                                                        runat="server">
                                                    </asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ErrorMessage="Enter Payment Date"
                                                        ControlToValidate="txtDate" ValidationGroup="pa" Display="Dynamic" runat="server"></asp:RequiredFieldValidator>
                                                    <asp:CompareValidator ForeColor="Red" Type="Date" ID="CompareValidator1" ValidationGroup="pa"
                                                        ControlToValidate="txtDate" Display="Dynamic" Operator="DataTypeCheck" runat="server"
                                                        ErrorMessage="Incorrect!!!"></asp:CompareValidator>
                                                    <asp:RangeValidator ID="rvDate" Type="Date"  runat="server" ControlToValidate="txtDate" ValidationGroup="pa"
                                                        ForeColor="Red" Display="Dynamic" ErrorMessage="*"></asp:RangeValidator>
                                                </td>
                                                <td>
                                                    <asp:Label ID="label13" runat="server" Text="Approved On"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtPaymentonDate" TabIndex="8" CssClass="input-text maskdate" placeholder="Approved Date"
                                                        runat="server">
                                                    </asp:TextBox>
                                              
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="vertical-align: top; padding-right: 5px; padding-top: 6px;">
                                                    <asp:Label ID="label14" runat="server" Text="Cheque Number"></asp:Label>
                                                </td>
                                                <td style="vertical-align: top; padding-right: 5px;">
                                                    <asp:TextBox MaxLength="7" ToolTip="Ex. 987654" TabIndex="9" CssClass="input-text sp_number"
                                                        placeholder="Cheque Number" ID="txtChequeDDno" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" ErrorMessage="Enter Cheque Number"
                                                        ControlToValidate="txtChequeDDno" ValidationGroup="pa" Display="Dynamic" runat="server"></asp:RequiredFieldValidator>
                                                </td>
                                                <td style="vertical-align: top; padding-right: 5px; padding-top: 6px;">
                                                    <asp:Label ID="label17" runat="server" Text="Applied On"></asp:Label>
                                                </td>
                                                <td style="vertical-align: top;">
                                                    <asp:TextBox TabIndex="10" ID="txtApplyedOn" CssClass="input-text maskdate" placeholder="Applied Date"
                                                        runat="server">
                                                    </asp:TextBox>                                        
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="vertical-align: top; padding-right: 5px; padding-top: 46px;">
                                                    <asp:Label ID="label18" runat="server" Text="Description"></asp:Label>
                                                </td>
                                                <td style="vertical-align: top; padding-right: 5px;">
                                                    <asp:TextBox TabIndex="11" CssClass="input-text" placeholder="Description" ToolTip="-"
                                                        TextMode="MultiLine" ID="txtDescription" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" ErrorMessage="Enter Description"
                                                        ControlToValidate="txtDescription" ValidationGroup="pa" Display="Dynamic" runat="server"></asp:RequiredFieldValidator>
                                                </td>
                                                <td style="vertical-align: top; padding-right: 5px; padding-top: 46px;">
                                                    <asp:Label ID="label19" runat="server" Text="Admin Sanction Number"></asp:Label>
                                                </td>
                                                <td style="vertical-align: top; padding-top: 42px;">
                                                    <asp:TextBox TabIndex="12" MaxLength="4" CssClass="input-text sp_number" placeholder="Admin Sanction Number"
                                                        ToolTip="Ex. 1000" ID="txtAOSanctiion" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator12" ErrorMessage="Enter Admin Sanction Number"
                                                        ControlToValidate="txtAOSanctiion" ValidationGroup="pa" Display="Dynamic" runat="server"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                              <tr>
                                                <td style="vertical-align: top; padding-right: 5px; padding-top: 6px;">
                                                     <asp:Label ID="Guarantortyp" runat="server" Text="Document Detail"></asp:Label> 
                                                </td>
                                                <td style="vertical-align: top; padding-right: 5px;">                                                                                               
                                              <asp:RadioButtonList ID="GrDocument" AutoPostBack="true" OnSelectedIndexChanged="GrDocument_SelectedIndexChanged" 
                                                        runat="server" RepeatDirection="Horizontal">
                                                         <asp:ListItem Text="Document" Value="Document"></asp:ListItem>        
                                                         <asp:ListItem Text="Clean" Value="Clean"></asp:ListItem>                                                                                             
                                                    </asp:RadioButtonList>           
                                                       <asp:RequiredFieldValidator runat="server" ID="rvdocument" Display="Dynamic"
                                                            ControlToValidate="GrDocument" ErrorMessage="Please Select Document"
                                                            ValidationGroup="pa">*</asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                        </table>
                                        <br />

                                        <asp:Panel ID="Guadetailpanel" runat="server" Visible="false">
                                             <table style="display: table; margin: 0px auto;">
                                             <tr>
                                                 <td>
                                                     <asp:Label ID="Documentno" runat="server" Text="Document Number"></asp:Label>
                                                 </td>
                                                 <td>
                                                     <asp:TextBox ID="Docnotxt" runat="server" CssClass="input-text"></asp:TextBox>
                                                 </td>
                                                 <td>
                                                     <asp:Label ID="InFav" runat="server" Text="In Favour"></asp:Label>
                                                 </td>
                                                 <td>
                                                     <asp:TextBox ID="InFavtxt" runat="server" CssClass="input-text"></asp:TextBox>
                                                 </td>
                                             </tr>
                                                 <tr>
                                                     <td>
                                                           <asp:Label ID="Propval" runat="server" Text="Property Value"></asp:Label>
                                                     </td>
                                                     <td>
                                                         <asp:TextBox ID="Propvaltxt" runat="server" CssClass="input-text"></asp:TextBox>
                                                     </td>
                                                     <td>
                                                         <asp:Label ID="Registmode" runat="server" Text="Registration" ></asp:Label>
                                                     </td>
                                                     <td>
                                                         <asp:DropDownList ID="DropRegistmode" runat="server"  CssClass="chzn-select">
                                                             <asp:ListItem Text="Please Select" />  
                                                             <asp:ListItem Text="Security Bond" />
                                                             <asp:ListItem Text="MOD[Memorandum of Deposit of title deed]" />
                                                              <asp:ListItem Text="UnRegistered"/>
                                                         </asp:DropDownList>

                                                     </td>
                                                 </tr>                                           
                                             </table>

                                        </asp:Panel>

                                        <table style="display: table; margin: 0px auto;">
                                            <tr>
                                                <td align="center" colspan="3">
                                                    <div style="font-size:16px;">
                                                        <b>SUMMARY FORM PAYMENT DETAILS </b>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" colspan="3">
                                                    <div style="font-size:14px;color:Black;">
                                                        <b>CREDIT TRANSACTION</b></div>
                                                </td>
                                            </tr>
                                            <tr style="background-color: Gray; height: 25px">
                                                <td align="center">
                                                    <asp:Label ID="labelTitle" runat="server" Text="Head"></asp:Label>
                                                </td>
                                                <td colspan="2">
                                                    <asp:Label ID="labelCredit" runat="server" Text="Amount"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="padding-right: 5px;padding-top:20px;">
                                                    <asp:Label ID="label1" runat="server" Text="Bank Amount & Name"></asp:Label>
                                                </td>
                                                <td style="padding-right: 5px;">
                                                    <asp:TextBox TabIndex="13" ToolTip="Ex. 1000.00" placeholder="Bank Amount" CssClass="input-text sp_float"
                                                        ID="txtBankAmount" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ControlToValidate="txtBankAmount" Display="Dynamic" ValidationGroup="pa"
                                                        ID="RequiredFieldValidator9" runat="server" ErrorMessage="Enter Amount"></asp:RequiredFieldValidator>
                                                </td>
                                                <td>
                                                    <asp:DropDownList TabIndex="14" CssClass="chzn-select" Width="240px" ID="ddlBankName"
                                                        runat="server">
                                                    </asp:DropDownList>
                                                    <asp:CompareValidator ID="CompareValidator12" ValueToCompare="0" ControlToValidate="ddlBankName"
                                                        ValidationGroup="pa" Display="Dynamic" Operator="NotEqual" runat="server" ErrorMessage="Select Bank Name"></asp:CompareValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="padding-right: 5px;">
                                                    <asp:Label ID="label6" runat="server" Text="Incidential Charge"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox TabIndex="15" CssClass="input-text sp_float" ToolTip="Ex. 1000.00"
                                                        placeholder="Incidential Charge" ID="txtIncidentialCharge" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ControlToValidate="txtIncidentialCharge" Display="Dynamic"
                                                        ValidationGroup="pa" ID="RequiredFieldValidator4" runat="server" ErrorMessage="Enter Incidential Charge"></asp:RequiredFieldValidator>
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                             <tr>
                                                <td style="padding-right: 5px;">
                                                    <asp:Label ID="label" runat="server" Text="GST(Goods and service tax)"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox TabIndex="15" CssClass="input-text sp_float" ToolTip="Ex. 1000.00"
                                                        placeholder="GST" ID="txtGST" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ControlToValidate="txtGST" Display="Dynamic"
                                                        ValidationGroup="pa" ID="RequiredFieldValidator13" runat="server" ErrorMessage="Enter GST"></asp:RequiredFieldValidator>
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="padding-right: 5px;">
                                                    <asp:Label ID="label9" runat="server" Text="CGST(Goods and Service Tax)"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox TabIndex="16" ToolTip="Ex. 1000.00" CssClass="input-text sp_float"
                                                        ID="txtCgst" runat="server"></asp:TextBox>
                                                </td>
                                                <td rowspan="3">
                                                    <asp:CheckBox ID="chkRegisteredGST" runat="server" Text="Registered" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="padding-right: 5px;">
                                                    <asp:Label ID="label16" runat="server" Text="SGST(Goods and Service Tax)"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox TabIndex="16" ToolTip="Ex. 1000.00" CssClass="input-text sp_float"
                                                         ID="txtSgst" runat="server"></asp:TextBox>
                                                </td>
                                                </tr>
                                            <tr>
                                                <td style="padding-right: 5px;">
                                                    <asp:Label ID="label22" runat="server" Text="IGST(Goods and Service Tax)"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox TabIndex="16" ToolTip="Ex. 1000.00" CssClass="input-text sp_float"
                                                       ID="txtIgst" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <%-- //Default Interest 31/07/2019--%>
                                              <%--  <td style="padding-right: 5px;">
                                                    <asp:Label ID="label21" runat="server" Text="Default Interest"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox TabIndex="15" CssClass="input-text sp_float" ToolTip="Ex. 1000.00"
                                                        placeholder="Default Interest" ID="txtDefault" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ControlToValidate="txtDefault" Display="Dynamic"
                                                        ValidationGroup="pa" ID="RequiredFieldValidator14" runat="server" ErrorMessage="Enter Default Interst"></asp:RequiredFieldValidator>
                                                </td>
                                              --%>
                                          <%-- //Default Interest 31/07/2019--%>
                                                <td style="padding-right: 5px;">
                                                    <asp:Label ID="label12" runat="server" Text="Commission"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox TabIndex="17" ToolTip="Ex. 1000.00" placeholder="Commission" CssClass="input-text sp_float"
                                                        ID="txtCommision" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ControlToValidate="txtCommision" Display="Dynamic" ValidationGroup="pa"
                                                        ID="RequiredFieldValidator7" runat="server" ErrorMessage="Enter Commission"></asp:RequiredFieldValidator>
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="padding-right: 5px;">
                                                    <asp:Label ID="label15" runat="server" Text="Chit Dividend Amount"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox AutoPostBack="true" TabIndex="18" CssClass="input-text sp_float"
                                                        ToolTip="Ex. 1000.00" placeholder="Chit Dividend Amount" ID="txtChitKasarAmount"
                                                        OnTextChanged="KasarAmount_OnTextChanged" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ControlToValidate="txtChitKasarAmount" Display="Dynamic"
                                                        ValidationGroup="pa" ID="RequiredFieldValidator3" runat="server" ErrorMessage="Enter Chit Dividend Amount"></asp:RequiredFieldValidator>
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                            <tr style="padding-right: 5px;">
                                                <td>
                                                    <asp:Label ID="label2" runat="server" Text="Loan Amount"></asp:Label>
                                                    <asp:Label Visible="false" ID="lbTool" runat="server"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox TabIndex="19" ToolTip="Ex. 1000.00" placeholder="Loan Amount" AutoPostBack="true"
                                                        OnTextChanged="LoanAmount_OnTextChanged" CssClass="input-text sp_float"
                                                        ID="txtLoanAmount" runat="server"></asp:TextBox>
                                                </td>
                                                <td>
                                                <asp:Label ID="lbFuture" runat="server" Visible="false" Text="0.00"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="3">
                                                    <asp:GridView ID="GridGuardians" runat="server" AutoGenerateColumns="False" BorderStyle="None"
                                                        CellSpacing="10" Font-Names="Verdana" Font-Size="9pt" ForeColor="#333333" GridLines="None"
                                                        OnRowDataBound="GridGuardians_RowDataBound" Width="100%" ShowHeader="false" ShowFooter="true">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Future call">
                                                                <ItemTemplate>
                                                                    <table style="border: none;">
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="lblHeads" runat="server" Text='<%# Eval("Heads") %>' Visible="false" />
                                                                                <asp:Label ID="Label5" runat="server" Text="Heads"></asp:Label>
                                                                                <asp:DropDownList CssClass="chzn-select" Style="width: 150px !important;" 
                                                                                    TabIndex="58" ValidationGroup="GrpRow" ID="ddlRangeHeads"
                                                                                    runat="server">
                                                                                </asp:DropDownList>
<%--                                                                                <asp:CompareValidator Display="Dynamic" ValidationGroup="GrpRow" Operator="NotEqual"
                                                                                    ControlToValidate="ddlRangeHeads" ID="CompareValidator2" ValueToCompare="--Select--"
                                                                                    ErrorMessage="*" runat="server"> </asp:CompareValidator>--%>
                                                                            </td>
                                                                            <td>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </ItemTemplate>
                                                                <FooterStyle HorizontalAlign="Left" />
                                                                <FooterTemplate>
                                                                    <table style="border: none;">
                                                                        <tr>
                                                                            <td>
                                                                                <asp:ImageButton OnClick="btnAdd_GridGuardians_RowCommand_click" TabIndex="-1" ID="imgBtnAdd"
                                                                                    runat="server" CausesValidation="True" Height="24" Visible="true" ValidationGroup="GrpRow"
                                                                                    ImageUrl="~/Styles/Image/Images/round_plus_16.png" ToolTip="Add New Transaction"
                                                                                    Width="24" />
                                                                            </td>
                                                                            <td>
                                                                                <asp:ImageButton OnClick="btnRemove_GridGuardians_RowCommand_click" TabIndex="-1"
                                                                                    OnClientClick="clearValidationErrors();" ID="imgBtnRemove" runat="server" CausesValidation="false"
                                                                                    Height="24" ImageUrl="~/Styles/Image/Images/round_minus_16.png" ToolTip="Remove New Transaction"
                                                                                    Visible="true" Width="24" />
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                    <br />
                                                                </FooterTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Amount">
                                                                <ItemTemplate>
                                                                    <table style="border: none;">
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="lblamt" runat="server" Text="Amount"></asp:Label>
                                                                                <asp:TextBox Width="150" TabIndex="59" Text='<%#Eval("Amount") %>' ValidationGroup="GrpRow"
                                                                                   CssClass="input-text sp_float" ID="txtRangeAmount" runat="server">
                                                                                </asp:TextBox>
<%--                                                                                <asp:RequiredFieldValidator Display="Dynamic" ValidationGroup="GrpRow" ControlToValidate="txtRangeAmount"
                                                                                    ID="rfvAmtx" ErrorMessage="*" runat="server"> </asp:RequiredFieldValidator>--%>
                                                                            </td>
                                                                            <td>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Range">
                                                                <ItemTemplate>
                                                                    <table style="border: none;">
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="lblDesc" runat="server" Text="Range"></asp:Label>
                                                                                <asp:TextBox TabIndex="60" Text='<%#Eval("Description") %>'
                                                                                    ValidationGroup="GrpRow" CssClass="input-text" ID="txtRange" runat="server">
                                                                                </asp:TextBox>
                                                                            </td>
                                                                            <td>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </td>
                                            </tr>
                                            <%--<tr>
                                                <td style="padding-right: 5px;">
                                                    <asp:Label ID="label16" runat="server" Text="Future Call Amount & Range"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox TabIndex="20" AutoPostBack="true" placeholder="Future Call Amount" ToolTip="Ex. 1000.00"
                                                        OnTextChanged="LoanAmount_OnTextChanged" CssClass="input-text sp_float" ID="txtFutureColumnAmount"
                                                        runat="server"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox TabIndex="21" ID="txtFutureCallDescription" CssClass="input-text" placeholder="Range"
                                                        runat="server"></asp:TextBox>
                                                </td>
                                            </tr>--%>
                                            <tr>
                                                <td style="padding-right: 5px;">
                                                    <asp:Label ID="label20" runat="server" Text="Loan Interest"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox TabIndex="22" ToolTip="Ex. 1000.00" placeholder="Loan Interest" CssClass="input-text sp_float"
                                                        ID="txtLoanInterest" runat="server"></asp:TextBox>
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="3" align="center">
                                                    <div style="font-size: 14px;color:Black;">
                                                        <b>DEBIT TRANSACTION</b></div>
                                                </td>
                                            </tr>
                                            <tr style="background-color: Gray; height: 25px">
                                                <td align="center">
                                                    <asp:Label ID="label5" runat="server" Text="Head"></asp:Label>
                                                </td>
                                                <td colspan="2">
                                                    <asp:Label ID="label7" runat="server" Text="Amount"></asp:Label>
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="padding-top: 20px; padding-right: 5px;">
                                                    <asp:Label ID="LabelPrizedAmount" runat="server"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox TabIndex="23" ReadOnly="true" ToolTip="Ex. 1000.00" CssClass="input-text sp_float"
                                                        ID="txtDebitAmount" runat="server"></asp:TextBox>
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                           </ContentTemplate>
                            </asp:UpdatePanel>

                    </asp:Panel>
                    <div runat="server" style="margin: 0px auto; display: table;">
                        <asp:Button TabIndex="24" CausesValidation="true" ID="btnPayment" runat="server"
                            ValidationGroup="pa" CssClass="GreenyPushButton" OnClick="load_Payment" Text="Payment">
                        </asp:Button>
                        <asp:Button TabIndex="25" CausesValidation="false" ID="Button1" runat="server" CssClass="GreenyPushButton"
                            OnClientClick="clearValidationErrors();" OnClick="load_cancel" Text="Cancel">
                        </asp:Button>
                    </div>
                    <ajax:ModalPopupExtender ID="ModalPopupExtender1" TargetControlID="show" PopupControlID="pnlmsg"
                        BackgroundCssClass="modalBackground" runat="server">
                    </ajax:ModalPopupExtender>
                    <asp:LinkButton ID="show" runat="server"></asp:LinkButton>
                    <asp:Panel CssClass="raised" ID="pnlmsg" runat="server" Visible="false" Width="350px"
                        Style="min-height: 100px">
                        <div style="top: 0px; height: 50px; text-align: center; padding: auto 0; width: 100%"
                            class="boxheader">
                            <asp:Label runat="server" ID="lblh" Text=""> </asp:Label>
                        </div>
                        <div style="min-height: 100px;text-align: center;">
                            <br />
                            <br />
                            <asp:Label runat="server" ID="lblcon" Text=""> </asp:Label>
                            <br />
                            <br />
                        </div>
                        <div class="boxheader">
                            <div style="margin: 0 auto;">
                                <asp:Button TabIndex="21" Visible="false" Style="margin: 0 auto" CssClass="GreenyPushButton" OnClick="btn_ok"
                                    ID="BtnOK" CausesValidation="false" OnClientClick="clearValidationErrors(); this.disabled='true'; " UseSubmitBehavior="false"
                                    runat="server" Text="Yes" />
                                <asp:Button TabIndex="21" Visible="false" Style="margin: 0 auto" CssClass="GreenyPushButton" OnClick="load_cancel"
                                    ID="Button2" CausesValidation="false" OnClientClick="clearValidationErrors();"
                                    runat="server" Text="Ok" />
                                <asp:Button TabIndex="21" Visible="false" Style="margin: 0 auto" CssClass="GreenyPushButton" OnClick="load_cancel"
                                    ID="Button3" CausesValidation="false" OnClientClick="clearValidationErrors();"
                                    runat="server" Text="Cancel" />
                                <asp:Button Visible="false" Style="margin: 0 auto" CssClass="GreenyPushButton" OnClick="load_hidePanel"
                                    ID="btnHide" CausesValidation="false" OnClientClick="clearValidationErrors();"
                                    runat="server" Text="Ok" />
                            </div>
                        </div>
                    </asp:Panel>
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        $(document).ready(function () {
            $(".chzn-select").chosen({ search_contains: true });
            //            $(".sp_floats").spinner({
            //                decimals: 2,
            //                stepping: 0.50,
            //                min: 0.00
            //            });

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
            //            $(".sp_floats").spinner({
            //                decimals: 2,
            //                stepping: 0.50,
            //                min: 0.00
            //            });
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
</asp:Content>
