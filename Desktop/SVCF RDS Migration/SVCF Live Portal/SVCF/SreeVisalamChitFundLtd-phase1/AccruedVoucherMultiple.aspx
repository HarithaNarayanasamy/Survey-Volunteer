<%@ Page Language="C#" Culture="en-GB" MasterPageFile="~/Branch.Master" AutoEventWireup="true"
    CodeBehind="AccruedVoucherMultiple.aspx.cs" Inherits="SreeVisalamChitFundLtd_phase1.AccruedVoucherMultiple"
    Title="SVCF Admin Panel" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Head1" ContentPlaceHolderID="cphHead" runat="server">
    <link href="Styles/ControlsCss.css" rel="stylesheet" />
    <style type="text/css">
        .answerform {
            position: absolute;
            border: 5px solid gray;
            padding: 0px;
            background: white;
            width: 800px;
            height: 200px;
            overflow-y: scroll;
        }

        .btn-custom {
            background-color: #0488e8;
            color: #FFF;
            cursor: pointer;
        }

            .btn-custom:hover {
                background-color: #1f72ae;
                color: #FFF;
            }

            .btn-custom:active {
                background-color: #ff3b3b !important;
                color: #fff;
            }

            .btn-custom:focus {
                background-color: #ff3b3b !important;
                color: #fff;
            }
    </style>
    <link href="Styles/StyleSheet1.css" rel="stylesheet" />
    <link href="Styles/bootstrap.min.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMainContent" runat="server">
    <ajax:ToolkitScriptManager ID="scm1" runat="server">
    </ajax:ToolkitScriptManager>

    <style type="text/css">
        td {
            vertical-align: middle;
            padding: 0px 2px 0px 2px;
        }
    </style>
    <%--<asp:ScriptManager ID="scm1" runat="server"></asp:ScriptManager>--%>
    <script type="text/javascript">

        function NumberOnly() {
           
          
            var charCode = (evt.which) ? evt.which : event.keyCode

            if (charCode > 31 && (charCode < 48 || charCode > 57) && !(charCode == 46 || charcode == 8))
                return false;
            else {
                var len = $(element).val().length;
                var index = $(element).val().indexOf('.');
                if (index > 0 && charCode == 46) {
                    return false;
                }
                if (index > 0) {
                    var CharAfterdot = (len + 1) - index;
                    if (CharAfterdot > 3) {
                        return false;
                    }
                }

            }
            return true;
        }
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

        function ValidatetCreditHead() {
            if (document.getElementById('<%=ddlHeads.ClientID%>').selectedIndex == 0) {
                document.getElementById("lblheaderror").innerText = "*";
                return false;
            }
            if (document.getElementById('<%=txtAmount.ClientID%>').value == "") {
                document.getElementById("lblamnterr").innerText = "*";
                return false;
            }
            if (document.getElementById('<%=txtDescription.ClientID%>').value == "") {
                document.getElementById("lbldesccr").innerText = "*";
                return false;
            }

            document.getElementById("lblheaderror").innerText = "";
            document.getElementById("lblamnterr").innerText = "";
            document.getElementById("lbldesccr").innerText = "";
            return true;
        }

        function GetDebitcheck() {

            if (document.getElementById('<%=ddlHeadsDebit.ClientID%>').selectedIndex == 0) {
                document.getElementById("lbldeberror").innerText = "*";
                return false;
            }
            
            if (document.getElementById('<%=txtDebitdesc.ClientID%>').value == "") {
                document.getElementById("lbldbdesc").innerText = "*";
                return false;
            }
            document.getElementById("lbldeberror").innerText = "";
            document.getElementById("lbldbamnt").innerText = "";
            document.getElementById("lbldbdesc").innerText = "";
            return true;

        }

    </script>
    <asp:Panel CssClass="row" ID="Panel1" runat="server" DefaultButton="btnGenerate"
        class="content">
        <div class="twelve columns">
            <div class="box_c">
                <div class="box_c_heading  box_actions">
                    <p>
                        Accrued Interest & Investment Details
                    </p>
                </div>

                <div class="box_c_content">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <div class="row">
                                <br />
                                <div style="display: inline; zoom: 1; text-align: center;">
                                    <div style="margin-left: auto; margin-right: auto; display: table;">
                                        <table cellspacing="4px" width="100%">
                                            <tr>
                                                <td style="vertical-align: top; padding-top: 6px;">
                                                    <asp:Label ID="lblDate" runat="server" Text="Date"></asp:Label>
                                                </td>
                                                <td style="vertical-align: top; padding-left: 6px; text-align: left;">
                                                    <asp:TextBox Width="100" ValidationGroup="a" TabIndex="1" ID="txtDate" OnChange="CheckDate();"
                                                        CssClass="input-text maskdate" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ForeColor="Red" ID="RequiredFieldValidator6" ErrorMessage="Rquired!!!"
                                                        ControlToValidate="txtDate" ValidationGroup="Generate" Display="Dynamic" runat="server"></asp:RequiredFieldValidator>
                                                    <asp:CompareValidator ForeColor="Red" Type="Date" ID="CompareValidator11" ValidationGroup="Generate"
                                                        ControlToValidate="txtDate" Display="Dynamic" Operator="DataTypeCheck" runat="server"
                                                        ErrorMessage="Incorrect!!!"></asp:CompareValidator>
                                                    <asp:RangeValidator ID="rvDate" EnableClientScript="false" runat="server"
                                                        Type="Date" ControlToValidate="txtDate" ValidationGroup="Generate" ForeColor="Red" Display="Dynamic"
                                                        ErrorMessage="*"></asp:RangeValidator>

                                                    <asp:HiddenField ID="hfTxtDate" runat="server"/>
                                                </td>
                                                <td style="vertical-align: top; padding-top: 6px;">
                                                    <asp:Label ID="Label1" runat="server" Text="Voucher Number" Visible="true"></asp:Label>
                                                </td>
                                                <td style="vertical-align: top; padding-left: 6px; text-align: left;">
                                                    <asp:TextBox Width="100" TabIndex="2" ValidationGroup="Generate" ID="txtVoucherNo" ReadOnly="true"
                                                        CssClass="input-text sp_number" Visible="true" runat="server"></asp:TextBox>
                                                    
                                                    <asp:RequiredFieldValidator Display="Dynamic" ValidationGroup="Generate" ControlToValidate="txtVoucherNo"
                                                        ID="RequiredFieldValidator12" ErrorMessage="Rquired!!!" runat="server"> </asp:RequiredFieldValidator>
                                                </td>
                                                <td style="vertical-align: top; padding-top: 6px;">
                                                    <asp:Label ID="lblReceivedBy" runat="server" Text="By" Visible="false"></asp:Label>
                                                </td>
                                                <td style="vertical-align: top; padding-left: 6px; text-align: left;">
                                                    <asp:TextBox Width="150" ValidationGroup="Generate" CssClass="input-text" ID="txtReceivedBy" Visible="false"
                                                        runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ValidationGroup="Generate" ControlToValidate="txtReceivedBy"
                                                        ID="RequiredFieldValidator1" ErrorMessage="Rquired!!!" runat="server"> </asp:RequiredFieldValidator>
                                                </td>
                                                 <td style="vertical-align: top; padding-top: 6px;">
                                                    <asp:Label ID="lblSeries" runat="server" Text="Series" Visible="false"></asp:Label>
                                                </td>
                                                <td style="vertical-align: top; padding-left: 6px; text-align: left;">
                                                    <asp:TextBox Width="100" ValidationGroup="Generate" ID="txtSeries" Visible="false"
                                                        CssClass="input-text" runat="server"></asp:TextBox>

                                                    <asp:RequiredFieldValidator Display="Dynamic" ValidationGroup="Generate" ControlToValidate="txtSeries"
                                                        ID="RequiredFieldValidator11" ErrorMessage="Rquired!!!" runat="server"> </asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator Display="Dynamic" ValidationGroup="Generate" ID="RegularExpressionValidator2"
                                                        runat="server" ControlToValidate="txtSeries" ErrorMessage="AlphaPets only!!!"
                                                        ValidationExpression="^[a-zA-Z]+$"></asp:RegularExpressionValidator>
                                                </td>
                                            </tr>
                                        </table>
                                        <br />
                                    </div>
                                    <div class="box_c_heading cf">
                                        <div class="box_c_ico">
                                            <asp:Image runat="server" ID="img16List" ImageUrl="pertho_admin_v1.3/img/ico/icSw2/16-List.png" AlternateText="" />
                                        </div>
                                        <p>
                                            Credit Transactions
                                        </p>
                                    </div>
                                    <div>
                                    </div>
                                    <table style="border: none;">
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblHeads" runat="server" Text='<%# Eval("Heads") %>' Visible="false" />
                                                <asp:Label ID="Label5" runat="server" Text="Heads"></asp:Label>
                                                <asp:DropDownList CssClass="chzn-select" Style="width: 350px !important;"
                                                    TabIndex="3" ID="ddlHeads"
                                                    runat="server" OnChange="GetChequeNumber();">
                                                </asp:DropDownList>
                                                <label id="lblheaderror" style="width: 50px;"></label>
                                                <asp:HiddenField ID="HD_Headtxt" runat="server" />
                                                <asp:HiddenField ID="HD_Headval" runat="server" />
                                            </td>
                                            <td>   
                                                <asp:Label ID="lblamt" runat="server" Text="Amount"></asp:Label><br />
                                                <asp:TextBox TabIndex="4" Text='<%#Eval("Amount") %>'
                                                    CssClass="twitterStyleTextbox  sp_currency" ID="txtAmount" runat="server" onkeypress="NumberOnly(event,this);">
                                                </asp:TextBox>
                                                <label id="lblamnterr" style="width: 50px;"></label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblDesc" runat="server" Text="Description"></asp:Label>
                                                <asp:TextBox TextMode="MultiLine" Height="50" TabIndex="5"
                                                    ValidationGroup="GrpRow" CssClass="input-text" ID="txtDescription" runat="server">
                                                </asp:TextBox>
                                                <label id="lbldesccr" style="width: 50px;"></label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblChequeNO" runat="server" Text="Cheque NO"></asp:Label>
                                                <br />
                                                <asp:TextBox Width="100"
                                                    MaxLength="7" ID="txtChequeNO"
                                                    runat="server">
                                                </asp:TextBox>
                                            </td>
                                            <td colspan="7" align="right">
                                                <asp:ImageButton OnClick="btnAdd_GridGuardians_RowCommand_click" ID="imgBtnAdd"
                                                    runat="server" CausesValidation="false" Height="24" Visible="false"
                                                    OnClientClick="return ValidatetCreditHead();"
                                                    ImageUrl="~/Styles/Image/Images/round_plus_16.png" ToolTip="Add New Transaction"
                                                    Width="24" />
                                            </td>
                                            <td style="width: 100px">
                                                <asp:Button ID="crbtnAdd" TabIndex="6" CssClass="btn-custom" ToolTip="Add" runat="server" Text="Add" />
                                                <button type="button" class="crdelete">Delete</button>
                                            </td>
                                            <td>
                                                <div id="canceldiv" runat="server" visible="false">
                                                    <asp:ImageButton ID="ImgCancelRcpt" runat="server" ImageUrl="~/Styles/Image/Images/RemoveReceipt.png" ToolTip="Cancel Receipt"
                                                        OnClick="ImgCancelRcpt_Click" />
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="rbutton">
                                                <asp:RadioButton ID="rbtndep" runat="server" GroupName="invest" Text="Depreciation" />
                                                <asp:RadioButton ID="rbtnsale" runat="server" GroupName="invest" Text="Sale" />
                                            </td>
                                             <td >
                                           <asp:Label ID="lblcrfrom" runat="server" Text="From"></asp:Label><br />
                                            <asp:TextBox ID="txtcrfrom"  CssClass="input-text maskdate" runat="server" onchange="creditfromdate();" TabIndex="12"></asp:TextBox>

                                           <asp:Label ID="lblsaleval" runat="server" Text="Sale Value"></asp:Label><br />
                                           <asp:TextBox ID="txtsaleval"  CssClass="input-text " runat="server" TabIndex="12"></asp:TextBox>
                                        </td>
                                        <td >
                                           <asp:Label ID="lblcrto" runat="server" Text="To"></asp:Label><br />
                                            <asp:TextBox ID="txtcrto"  CssClass="input-text maskdate" runat="server" onchange="credittodate();" TabIndex="13"></asp:TextBox>
                                        
                                            <asp:Label ID="lblsaledate" runat="server" Text="Sale Date"></asp:Label><br />
                                            <asp:TextBox ID="txtsaledate"  CssClass="input-text maskdate" runat="server" onchange="saledate();" TabIndex="12"></asp:TextBox>
                                        
                                        
                                        </td>
                                        </tr>
                                    </table>
                                    <br />
                                    <asp:Label ID="lblcancelmsg" runat="server" Text="" CssClass="lblstyle"></asp:Label>

                                    <div style="vertical-align: top; overflow: auto; height: 200px; margin-top: 3px; width: 950px; border: 2px solid gray; overflow-y: scroll;">
                                        <asp:GridView ID="GridClnt" BorderStyle="Solid" runat="server"
                                            CellSpacing="2" Font-Names="Verdana" ForeColor="#333333" GridLines="Both" Height="100px"
                                            AutoGenerateColumns="false" CssClass="Trans twelve columns" Width="850px">
                                            <RowStyle BackColor="#F7F6F3" />
                                            <RowStyle CssClass="GridViewRowStyle" />
                                            <HeaderStyle CssClass="GridViewHeaderStyle" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="Heads" ItemStyle-CssClass="Heads"
                                                    HeaderStyle-BorderColor="Gray" HeaderStyle-BorderStyle="Solid" HeaderStyle-BorderWidth="2px"
                                                    ItemStyle-BorderColor="Gray" ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px">
                                                    <ItemTemplate>
                                                        <%# Eval("Heads") %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Amount" ItemStyle-CssClass="Amount"
                                                    HeaderStyle-BorderColor="Gray" HeaderStyle-BorderStyle="Solid" HeaderStyle-BorderWidth="2px"
                                                    ItemStyle-BorderColor="Gray" ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px">
                                                    <ItemTemplate>
                                                        <%# Eval("Amount") %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Description" ItemStyle-CssClass="Description"
                                                    HeaderStyle-BorderColor="Gray" HeaderStyle-BorderStyle="Solid" HeaderStyle-BorderWidth="2px"
                                                    ItemStyle-BorderColor="Gray" ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px">
                                                    <ItemTemplate>
                                                        <%# Eval("Description") %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="ChequeNo" ItemStyle-CssClass="ChequeNo"
                                                    HeaderStyle-BorderColor="Gray" HeaderStyle-BorderStyle="Solid" HeaderStyle-BorderWidth="2px"
                                                    ItemStyle-BorderColor="Gray" ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px">
                                                    <ItemTemplate>
                                                        <%# Eval("ChequeNo") %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="headid" ItemStyle-CssClass="headid"
                                                    HeaderStyle-BorderColor="Gray" HeaderStyle-BorderStyle="Solid" HeaderStyle-BorderWidth="2px"
                                                    ItemStyle-BorderColor="Gray" ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px">
                                                    <ItemTemplate>
                                                        <%# Eval("headid") %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="From" ItemStyle-CssClass="From"
                                                    HeaderStyle-BorderColor="Gray" HeaderStyle-BorderStyle="Solid" HeaderStyle-BorderWidth="2px"
                                                    ItemStyle-BorderColor="Gray" ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px">
                                                    <ItemTemplate>
                                                        <%# Eval("From") %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="To" ItemStyle-CssClass="To"
                                                    HeaderStyle-BorderColor="Gray" HeaderStyle-BorderStyle="Solid" HeaderStyle-BorderWidth="2px"
                                                    ItemStyle-BorderColor="Gray" ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px">
                                                    <ItemTemplate>
                                                        <%# Eval("To") %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="SaleValue" ItemStyle-CssClass="SaleValue"
                                                    HeaderStyle-BorderColor="Gray" HeaderStyle-BorderStyle="Solid" HeaderStyle-BorderWidth="2px"
                                                    ItemStyle-BorderColor="Gray" ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px">
                                                    <ItemTemplate>
                                                        <%# Eval("SaleValue") %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="SaleDate" ItemStyle-CssClass="SaleDate"
                                                    HeaderStyle-BorderColor="Gray" HeaderStyle-BorderStyle="Solid" HeaderStyle-BorderWidth="2px"
                                                    ItemStyle-BorderColor="Gray" ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px">
                                                    <ItemTemplate>
                                                        <%# Eval("SaleDate") %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderStyle-BackColor="Gray" HeaderStyle-BorderStyle="Solid" HeaderStyle-BorderWidth="2px"
                                                    ItemStyle-BorderColor="Gray" ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px">
                                                    <ItemTemplate>
                                                        <input type='checkbox' class='case' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>

                                </div>
                                <div></div>
                               <br>
                                <div class="box_c_heading cf">
                                    <div class="box_c_ico">
                                        <asp:Image runat="server" ID="img16List1" ImageUrl="pertho_admin_v1.3/img/ico/icSw2/16-List.png" AlternateText="" />
                                    </div>
                                    <p>
                                        Debit Transactions
                                    </p>
                                </div>

                                <table style="border: none;">
                                    <tr>
                                        <td style="height: 40px;">
                                            <asp:Label ID="lblDBHeads" runat="server" Text='<%# Eval("Heads") %>' Visible="false" />
                                            <asp:Label ID="Label6" runat="server" Text="Heads"></asp:Label>
                                            <asp:DropDownList CssClass="chzn-select" Style="width: 350px !important;"
                                                TabIndex="7" AutoPostBack="false" ID="ddlHeadsDebit" runat="server" onchange="ondebitchange();">
                                            </asp:DropDownList>
                                            <label id="lbldeberror" style="width: 50px;"></label>
                                            <asp:HiddenField ID="hidDb_Headtxt" runat="server" />
                                            <asp:HiddenField ID="hidDb_HeadVal" runat="server" />
                                        </td>


                                        <td style="height: 40px;" align="left">
                                            <asp:Label ID="lbldbamt" runat="server" Text="Amount"></asp:Label><br />
                                            <asp:TextBox ID="debitAmnt" runat="server" TabIndex="8"></asp:TextBox>
                                            <label id="lbldbamnt" style="width: 50px;"></label>
                                            <asp:HiddenField ID="hidden_totalcred" runat="server" />
                                        </td>

                                        <td style="height: 40px;">
                                            <asp:Label ID="lbldbDesc" runat="server" Text="Description"></asp:Label>
                                            <asp:TextBox TextMode="MultiLine" Height="50" TabIndex="9"
                                                ID="txtDebitdesc" runat="server">
                                            </asp:TextBox>
                                            <label id="lbldbdesc" style="width: 50px;"></label>
                                        </td>
                                        <td style="height: 40px;" align="left"></td>
                                        <td></td>
                                        <td>
                                            <asp:ImageButton OnClick="btnAdd_GridGuardiansDebit_RowCommand_click" TabIndex="-1"
                                                ID="imgBtnAddDebit" runat="server" CausesValidation="false" Height="24"
                                                OnClientClick="return GetDebitcheck();" Visible="false"
                                                ImageUrl="~/Styles/Image/Images/round_plus_16.png" ToolTip="Add New Transaction"
                                                Width="24" />
                                        </td>
                                        <td style="width: 100px">
                                            <asp:Button ID="DbbtnAdd" CssClass="btn-custom" ToolTip="Add" TabIndex="10" runat="server" Text="Add" />
                                            <button type="button" class="dbdelete">Delete</button>
                                        </td>
                                    </tr>


                                    <tr>
                                        <td style="height: 40px;">
                                         <asp:Label ID="lblaccloan" runat="server" Text="Loan Heads"></asp:Label>
                                        <asp:DropDownList CssClass="chzn-single" Style="width: 350px !important;"
                                                TabIndex="11" AutoPostBack="false" ID="ddlaccruedloan" runat="server">
                                            </asp:DropDownList>
                                            <asp:HiddenField ID="hidDb_LoanHeadtxt" runat="server" />
                                            <asp:HiddenField ID="hidDb_LoanHeadval" runat="server" />

                                            <asp:Label ID="lblaccbank" runat="server" Text="Bank Heads"></asp:Label>
                                        <asp:DropDownList CssClass="chzn-single" Style="width: 350px !important;"
                                                TabIndex="11" AutoPostBack="false" ID="ddlaccruedbank" runat="server">
                                            </asp:DropDownList>
                                           <asp:HiddenField ID="hidDb_BankHeadtxt" runat="server" />
                                            <asp:HiddenField ID="hidDb_BankHeadval" runat="server" />

                                            <asp:Label ID="lblaccrent" runat="server" Text="Purpose"></asp:Label>
                                            <asp:DropDownList CssClass="chzn-single" Style="width: 350px !important;"
                                                TabIndex="11" AutoPostBack="false" ID="ddlaccruedrent" runat="server">
                                            </asp:DropDownList>
                                            <asp:HiddenField ID="hidDb_RentHeadtxt" runat="server" />
                                            <asp:HiddenField ID="hidDb_RentHeadval" runat="server" />
                                            </td>
                                        <td style="height: 40px;">
                                            <asp:Label ID="lblaccpurchase" runat="server" Text="Purchase Date"></asp:Label><br />
                                            <asp:TextBox ID="txtaccdatepur" CssClass="input-text maskdate" onchange="purchasedate();" runat="server"></asp:TextBox>

                                           <asp:Label ID="lblaccdatefrom" runat="server" Text="From"></asp:Label><br />
                                           <asp:TextBox ID="txtaccdatefrom" CssClass="input-text maskdate" onchange="fromdate();" runat="server" ></asp:TextBox>
                                        </td>

                                        <td style="height: 40px;">
                                           <asp:Label ID="lblaccdateto" runat="server" Text="To"></asp:Label><br />
                                            <asp:TextBox ID="txtaccdateto" CssClass="input-text maskdate" onchange="todate();" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="height: 40px;">
                                            <asp:Label ID="lblaccrentrate" runat="server" Text="Rate of Rent"></asp:Label><br />
                                            <asp:TextBox ID="txtrentrate" CssClass="input-text"  runat="server"  TabIndex="12"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>

                                <div style="vertical-align: top; overflow: auto; height: 200px; margin-top: 3px; width: 950px; border: 2px solid gray; overflow-y: scroll;">
                                    <asp:GridView ID="GrdDbClnt" BorderStyle="Solid" runat="server"
                                        CellSpacing="2" Font-Names="Verdana" ForeColor="#333333" GridLines="Both" Height="100px"
                                        AutoGenerateColumns="false" CssClass="Trans twelve columns" Width="850px">
                                        <RowStyle BackColor="#F7F6F3" />
                                        <RowStyle CssClass="GridViewRowStyle" />
                                        <HeaderStyle CssClass="GridViewHeaderStyle" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="DbHeads" ItemStyle-CssClass="DbHeads"
                                                HeaderStyle-BorderColor="Gray" HeaderStyle-BorderStyle="Solid" HeaderStyle-BorderWidth="2px"
                                                ItemStyle-BorderColor="Gray" ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px">
                                                <ItemTemplate>
                                                    <%# Eval("DbHeads") %>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="DbAmount" ItemStyle-CssClass="DbAmount"
                                                HeaderStyle-BorderColor="Gray" HeaderStyle-BorderStyle="Solid" HeaderStyle-BorderWidth="2px"
                                                ItemStyle-BorderColor="Gray" ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px">
                                                <ItemTemplate>
                                                    <%# Eval("DbAmount") %>
                                                </ItemTemplate>
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="DbDescription" ItemStyle-CssClass="DbDescription"
                                                HeaderStyle-BorderColor="Gray" HeaderStyle-BorderStyle="Solid" HeaderStyle-BorderWidth="2px"
                                                ItemStyle-BorderColor="Gray" ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px">
                                                <ItemTemplate>
                                                    <%# Eval("DbDescription") %>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Dbheadid" ItemStyle-CssClass="Dbheadid"
                                                HeaderStyle-BorderColor="Gray" HeaderStyle-BorderStyle="Solid" HeaderStyle-BorderWidth="2px"
                                                ItemStyle-BorderColor="Gray" ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px">
                                                <ItemTemplate>
                                                    <%# Eval("Dbheadid") %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                           

                                            <asp:TemplateField HeaderText="Dbhead" ItemStyle-CssClass="Dbhead"
                                                HeaderStyle-BorderColor="Gray" HeaderStyle-BorderStyle="Solid" HeaderStyle-BorderWidth="2px"
                                                ItemStyle-BorderColor="Gray" ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px">
                                                <ItemTemplate>
                                                    <%# Eval("Dbhead") %>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                             <asp:TemplateField HeaderText="Dbheadaccid" ItemStyle-CssClass="Dbheadaccid"
                                                HeaderStyle-BorderColor="Gray" HeaderStyle-BorderStyle="Solid" HeaderStyle-BorderWidth="2px"
                                                ItemStyle-BorderColor="Gray" ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px">
                                                <ItemTemplate>
                                                    <%# Eval("Dbheadaccid") %>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Dbpurchasedt" ItemStyle-CssClass="Dbpurchasedt"
                                                HeaderStyle-BorderColor="Gray" HeaderStyle-BorderStyle="Solid" HeaderStyle-BorderWidth="2px"
                                                ItemStyle-BorderColor="Gray" ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px">
                                                <ItemTemplate>
                                                    <%# Eval("Dbpurchasedt") %>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Dbfrom" ItemStyle-CssClass="Dbfrom"
                                                HeaderStyle-BorderColor="Gray" HeaderStyle-BorderStyle="Solid" HeaderStyle-BorderWidth="2px"
                                                ItemStyle-BorderColor="Gray" ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px">
                                                <ItemTemplate>
                                                    <%# Eval("Dbfrom") %>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Dbto" ItemStyle-CssClass="Dbto"
                                                HeaderStyle-BorderColor="Gray" HeaderStyle-BorderStyle="Solid" HeaderStyle-BorderWidth="2px"
                                                ItemStyle-BorderColor="Gray" ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px">
                                                <ItemTemplate>
                                                    <%# Eval("Dbto") %>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Dbrent" ItemStyle-CssClass="Dbrent"
                                                HeaderStyle-BorderColor="Gray" HeaderStyle-BorderStyle="Solid" HeaderStyle-BorderWidth="2px"
                                                ItemStyle-BorderColor="Gray" ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px">
                                                <ItemTemplate>
                                                    <%# Eval("Dbrent") %>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderStyle-BackColor="Gray" HeaderStyle-BorderStyle="Solid" HeaderStyle-BorderWidth="2px"
                                                ItemStyle-BorderColor="Gray" ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px">
                                                <ItemTemplate>
                                                    <input type='checkbox' class='dbcase' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
                        <ProgressTemplate>
                            <div>
                                <div style="position: absolute; left: 50%; margin-left: -50px; top: 186px;">
                                    <asp:Image AlternateText="waiting" runat="server" ID="imgWaiting" Style="vertical-align: middle;" />
                                </div>
                            </div>
                        </ProgressTemplate>
                    </asp:UpdateProgress>
                    <div style="margin: 0 auto;">
                        <br />
                        <asp:Button TabIndex="11" CausesValidation="true" CssClass="btn-custom" ToolTip="Generate" Style="display: block; width: 100px; margin: 0 auto;"
                            ID="btnGenerate" Text="Generate" ValidationGroup="Generate"
                            runat="server" />
                    </div>
                    <div id="dialog" style="display: none" align="center">
                    Do you want to save this entry?</div>
                </div>
            </div>
        </div>
    </asp:Panel>
    <asp:LinkButton Text="" runat="server" ID="btnShowPopup"></asp:LinkButton>
   
    <asp:Panel Visible="false" ID="pnlpopup" Style="background: #fff; border: 4px solid #ccc; border: 8px solid rgba(0, 0, 0, 0.5); background-clip: padding-box; -webkit-border-radius: 10px; -moz-border-radius: 10px; border-radius: 10px; min-width: 300px; max-width: 900px;"
        CssClass="raised" runat="server">
        <asp:Label runat="server" ID="lblHint" Text="" Visible="false"> </asp:Label>
        <div class=" box_c_heading">
            <asp:Label CssClass="inner_heading" runat="server" ID="lblHeading" Text=""> </asp:Label>
        </div>
        <div style="min-height: 20px; overflow: hidden; max-width: 880px; padding: 20px;">
            <div style="min-height: 20px; overflow: auto; max-width: 840px;">
                <br />
                <asp:Label runat="server" Style="margin-top: 10px; font-size: 14px;" ID="lblContent"
                    Text=""> </asp:Label>
                <br />
                <asp:PlaceHolder ID="DynamicControlsHolder" runat="server"></asp:PlaceHolder>
                <asp:GridView Width="800" ID="gvoldmember" runat="server" AutoGenerateColumns="False"
                    HeaderStyle-BackColor="#61A6F8" ShowFooter="True" HeaderStyle-Font-Bold="true"
                    HeaderStyle-ForeColor="White" PageSize="20" BackColor="White" BorderWidth="1px"
                    CellPadding="4" BorderColor="#DEDFDE" BorderStyle="None" ForeColor="Black" GridLines="None">
                    <RowStyle BackColor="#F7F7DE" />
                    <Columns>
                        <asp:TemplateField HeaderText="Heads">
                            <ItemTemplate>
                                <asp:Label Style="width: 150px;" ID="lblitemoldname" runat="server" Text='<%#Eval("Heads") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Credit Amount">
                            <ItemTemplate>
                                <asp:Label Style="width: 150px;" ID="lblCredit" runat="server" Text='<%#Eval("Credit") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Debit Amount">
                            <ItemTemplate>
                                <asp:Label Style="width: 150px;" ID="lblDebit" runat="server" Text='<%#Eval("Debit") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <HeaderStyle VerticalAlign="Middle" HorizontalAlign="left" CssClass="GridviewScrollC2Header" />
                    <RowStyle HorizontalAlign="left" VerticalAlign="Middle" CssClass="GridviewScrollC2Item" />
                    <PagerStyle CssClass="GridviewScrollC2Pager" />
                </asp:GridView>
            </div>
        </div>
        <div class=" box_c_heading">
            <div style="float: right;">
                <asp:Button Width="100" Style="margin: 0 auto" CssClass="GreenyPushButton" ID="btnYes"
                    OnClick="btnYes_Click" runat="server" Text="yes" />
                <asp:Button Width="100" Style="margin: 0 auto" CssClass="GreenyPushButton" ID="btnNo"
                    OnClick="btnNo_Click" runat="server" Text="No" />
            </div>
        </div>
    </asp:Panel>
    <asp:LinkButton Text="" runat="server" ID="btnShowPopupCheque"></asp:LinkButton>
    <ajax:ModalPopupExtender ID="MpAll" runat="server" TargetControlID="btnShowPopupCheque"
        PopupControlID="panCheque" BackgroundCssClass="modalBackground">
    </ajax:ModalPopupExtender>
  
    <script src="Scripts/jquery-1.8.3.min.js"></script>
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
            //            $(".sp_number").spinner({min: 0,
            //                numberFormat: "d",
            //                culture:"en-GB"
            //            });
            prth_mask_input.init();
        });


        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $(".chzn-select").chosen({ search_contains: true });
            $(".sp_currency").numeric({ negative: false });
            $(".sp_number").numeric({ decimal: false, negative: false }, function () { alert("Positive integers only"); this.value = ""; this.focus(); });
         
            prth_mask_input.init();
           
        });

       /////
       

        function GetChequeNumber() {
            var ser = $("#<%=ddlHeads.ClientID%>").find("option:selected").text(); //id name for dropdown list              
            //var cid = $("#<%=ddlHeads.ClientID%>").val(); //text name for dropdown list  
            var cid = $("#<%=ddlHeads.ClientID%>").find("option:selected").val(); //text name for dropdown list  
            var rcid = $("#<%=ddlHeads.ClientID%> option:selected").val();

           

            var bnktrue = rcid.includes(":");
            var chittrue = rcid.includes("|");
            if (bnktrue == true) {
                var rtid = rcid.split(":", 1)
                if (rtid == 3)
                {
                    $("#<%=txtChequeNO.ClientID%>").show();
                    $("#<%=lblChequeNO.ClientID%>").show();
                    document.getElementById('<%=txtDescription.ClientID %>').value = "";

                    $(".rbutton").hide();

                    $("#<%=lblcrfrom.ClientID%>").hide();
                    $("#<%=txtcrfrom.ClientID%>").hide();
                    $("#<%=lblcrto.ClientID%>").hide();
                    $("#<%=txtcrto.ClientID%>").hide();

                    $("#<%=lblsaleval.ClientID%>").hide();
                    $("#<%=txtsaleval.ClientID%>").hide();
                    $("#<%=lblsaledate.ClientID%>").hide();
                    $("#<%=txtsaledate.ClientID%>").hide();

                }
                else if (rtid == 5) {
                    $("#<%=txtChequeNO.ClientID%>").hide();
                    $("#<%=lblChequeNO.ClientID%>").hide();
                    var headid = rcid.split(':')[1];
                    GetCustName(headid);

                    $(".rbutton").hide();

                    $("#<%=lblcrfrom.ClientID%>").hide();
                    $("#<%=txtcrfrom.ClientID%>").hide();
                     $("#<%=lblcrto.ClientID%>").hide();
                     $("#<%=txtcrto.ClientID%>").hide();

                     $("#<%=lblsaleval.ClientID%>").hide();
                     $("#<%=txtsaleval.ClientID%>").hide();
                    $("#<%=lblsaledate.ClientID%>").hide();
                    $("#<%=txtsaledate.ClientID%>").hide();

                }
                else if (rtid == 2) {

                    $(".rbutton").show();
                   

                    $("#<%=txtChequeNO.ClientID%>").hide();
                    $("#<%=lblChequeNO.ClientID%>").hide();
                }
                else
                {
                    $(".rbutton").hide();

                    $("#<%=lblcrfrom.ClientID%>").hide();
                    $("#<%=txtcrfrom.ClientID%>").hide();
                     $("#<%=lblcrto.ClientID%>").hide();
                     $("#<%=txtcrto.ClientID%>").hide();


                    $("#<%=lblsaleval.ClientID%>").hide();
                    $("#<%=txtsaleval.ClientID%>").hide();
                    $("#<%=lblsaledate.ClientID%>").hide();
                    $("#<%=txtsaledate.ClientID%>").hide();



                     $("#<%=txtChequeNO.ClientID%>").hide();
                     $("#<%=lblChequeNO.ClientID%>").hide();

                }
        }
        else if (chittrue == true) {
            $("#<%=txtChequeNO.ClientID%>").hide();
            $("#<%=lblChequeNO.ClientID%>").hide();
            document.getElementById('<%=txtDescription.ClientID %>').value = "";
        }
    $("#<%=ddlHeads.ClientID%>").addClass('chzn-select');
        }


        function GetVisible() {
            var ser = $("#<%=ddlHeads.ClientID%>").find("option:selected").text(); //id name for dropdown list                       
            //var cid = $("#<%=ddlHeads.ClientID%>").val(); //text name for dropdown list  
            var cid = $("#<%=ddlHeads.ClientID%>").find("option:selected").val(); //text name for dropdown list             
            var rcid = $("#<%=ddlHeads.ClientID%> option:selected").val();
            var bnktrue = rcid.includes(":");
            if (bnktrue == true) {
                var rtid = rcid.split(":", 1);
                if (rtid == 3) {
                    $("#<%=txtChequeNO.ClientID%>").show();
                    $("#<%=lblChequeNO.ClientID%>").show();

                }
                else if (rtid == 5) {
                    $("#<%=txtChequeNO.ClientID%>").hide();
                    $("#<%=lblChequeNO.ClientID%>").hide();
                    var headid = rcid.split(':')[1];
                    GetCustName(headid);
                }
        }
        else {
            $("#<%=txtChequeNO.ClientID%>").hide();
                $("#<%=lblChequeNO.ClientID%>").hide();
            }
            $("#<%=ddlHeads.ClientID%>").addClass('chzn-select');
        }

        $(document).ready(function () {
            $("#<%=ddlHeadsDebit.ClientID%>").change(function () {
                var cid = $("#<%=ddlHeadsDebit.ClientID%>").find("option:selected").val(); //id for dropdown list  
                var rcid = $("#<%=ddlHeadsDebit.ClientID%> option:selected").val();
                var rtid = rcid.split(":", 1)
                if (rtid == 5) {
                    var headid = rcid.split(':')[1];
                    DebitCustName(headid);
                }
            });
        });


        $(document).ready(function () {
            $("#<%=ddlHeads.ClientID%>").change(function () {
                var ser = $("#<%=ddlHeads.ClientID%>").find("option:selected").text(); //id name for dropdown list              
                //var cid = $("#<%=ddlHeads.ClientID%>").val(); //text name for dropdown list  
                var cid = $("#<%=ddlHeads.ClientID%>").find("option:selected").val(); //text name for dropdown list             
                var rcid = $("#<%=ddlHeads.ClientID%> option:selected").val();

                //var bnktrue = rcid.indexOf(":") == 1 ? true : false;
                //var bnktrue = rcid.indexOf(":") == 1 ? true : false;
                var bnktrue = rcid.includes(":");
                var chittrue = rcid.includes("|");

                if (bnktrue == true) {
                    var rtid = rcid.split(":", 1)
                    if (rtid == 3) {
                        $("#<%=txtChequeNO.ClientID%>").show();
                        $("#<%=lblChequeNO.ClientID%>").show();
                        document.getElementById('<%=txtDescription.ClientID %>').value = "";
                    }
                    else if (rtid == 5) {
                        $("#<%=txtChequeNO.ClientID%>").hide();
                        $("#<%=lblChequeNO.ClientID%>").hide();
                        var headid = rcid.split(':')[1];
                        GetCustName(headid);
                    }
                }
                else if (chittrue == true) {
                    $("#<%=txtChequeNO.ClientID%>").hide();
                    $("#<%=lblChequeNO.ClientID%>").hide();
                    document.getElementById('<%=txtDescription.ClientID %>').value = "";
                }
            });
            $("#<%=ddlHeads.ClientID%>").addClass('chzn-select');
        });


        function GetCustName(hdid) {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "AccruedVoucherMultiple.aspx/getcustomername",
                data: JSON.stringify({ hdid: hdid }),
                dataType: "json",
                success: function (data) {
                    var msg = data.d.toString();
                    var txtsnumber = $("[id*=txtDescription]");
                    txtsnumber.val(msg);
                },
                error: function (result) {
                    alert("Error: " + result);
                }
            });
        }

        function DebitCustName(hdid) {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "AccruedVoucherMultiple.aspx/getcustomername",
                data: JSON.stringify({ hdid: hdid }),
                dataType: "json",
                success: function (data) {
                    var msg = data.d.toString();
                    var txtsnumber = $("[id*=txtDebitdesc]");
                    txtsnumber.val(msg);
                },
                error: function (result) {
                    alert("Error: " + result);
                }
            });
        }


        $(document).ready(function () {
            var ser = $("#<%=ddlHeads.ClientID%>").text(); //id name for dropdown list              
            //var cid = $("#<%=ddlHeads.ClientID%>").val(); //text name for dropdown list  
            var cid = $("#<%=ddlHeads.ClientID%>").find("option:selected").val(); //text name for dropdown list             
            var rcid = $("#<%=ddlHeads.ClientID%> option:selected").val();
            if (!ser == "--Select--") {
                var bnktrue = rcid.includes(":");
                if (bnktrue == true) {
                    var rtid = rcid.split(":", 1)
                    if (rtid == 3) {
                        $("#<%=txtChequeNO.ClientID%>").show();
                        $("#<%=lblChequeNO.ClientID%>").show();
                        document.getElementById('<%=txtDescription.ClientID %>').value = "";
                    }
                    else if (rtid == 5) {
                        $("#<%=txtChequeNO.ClientID%>").hide();
                        $("#<%=lblChequeNO.ClientID%>").hide();
                        var headid = rcid.split(':')[1];
                        GetCustName(headid);
                    }
            }
            else {
                $("#<%=txtChequeNO.ClientID%>").hide();
                    $("#<%=lblChequeNO.ClientID%>").hide();
                    document.getElementById('<%=txtDescription.ClientID %>').value = "";
                }
            }
            else {
                $("#<%=txtChequeNO.ClientID%>").hide();
                $("#<%=lblChequeNO.ClientID%>").hide();
                document.getElementById('<%=txtDescription.ClientID %>').value = "";
            }
            $("#<%=ddlHeads.ClientID%>").addClass('chzn-select');
        
        });



        function CheckDate() {
            var inputDate = $('#<%=txtDate.ClientID %>').val();
            var Reg_Expression = /^\d{1,2}\/\d{1,2}\/\d{4}$/;
            if (Reg_Expression.test(inputDate)) {
                var partitionedDate = inputDate.split('/');
                var nDay = parseInt(partitionedDate[0], 10);
                var nMonth = parseInt(partitionedDate[1], 10);
                var nYear = parseInt(partitionedDate[2], 10);
                var dDate = new Date(nYear, nMonth - 1, nDay);
                if ((dDate.getFullYear() == nYear) && (dDate.getMonth() == nMonth - 1) && (dDate.getDate() == nDay)) {
                }
                else {
                    document.getElementById('<%=txtDate.ClientID %>').value = "";
                    alert('Incorrect Date Format..');
                }
            }
            else {
                document.getElementById('<%=txtDate.ClientID %>').value = "";
                alert('Incorrect Date Format..');
            }
        }
 //Bala
        $(document).ready(function () {

            $("#<%=txtDate.ClientID%>").change(function () {
                var inputDate = $('#<%=txtDate.ClientID %>').val();
                var Reg_Expression = /^\d{1,2}\/\d{1,2}\/\d{4}$/;
                if (Reg_Expression.test(inputDate)) {
                    var partitionedDate = inputDate.split('/');
                    var nDay = parseInt(partitionedDate[0], 10);
                    var nMonth = parseInt(partitionedDate[1], 10);
                    var nYear = parseInt(partitionedDate[2], 10);
                    var dDate = new Date(nYear, nMonth - 1, nDay);
                    
                    if ((dDate.getFullYear() == nYear) && (dDate.getMonth() == nMonth - 1) && (dDate.getDate() == nDay)) {
                        var mDate = $("#<%=hfTxtDate.ClientID%>").val();
                        var partDate = mDate.split('/');
                        var mDay = parseInt(partDate[0], 10);
                        var mMonth = parseInt(partDate[1], 10);
                        var mYear = parseInt(partDate[2], 10);
                        var miniDate = new Date(mYear, mMonth - 1, mDay);
                        if (miniDate > dDate) { 
                        alert('You have entered the date less than block date!');
                        $("#<%=txtDate.ClientID%>").val("");
                        $("#<%=txtDate.ClientID%>").focus();
                    }
                        //alert('Input Date :' +new Date( inputDate) + 'Minimum Date : ' + new Date( $("#<%=hfTxtDate.ClientID%>").val()));
                }
                else {
                    document.getElementById('<%=txtDate.ClientID %>').value = "";
                    alert('Incorrect Date Format..');
                    $("#<%=txtDate.ClientID%>").val("");
                    $("#<%=txtDate.ClientID%>").focus();
                    }
                }
                else {
                    document.getElementById('<%=txtDate.ClientID %>').value = "";
                alert('Incorrect Date Format..');
                $("#<%=txtDate.ClientID%>").val("");
                    $("#<%=txtDate.ClientID%>").focus();
            }

                
                
            });
        });
        //

    </script>


    <script src="Scripts/jquery-1.8.3.min.js"></script>

    <script type="text/javascript">

        function GetValue() {
            var hds = "";
            var hdsval = "";

            hds = $('#<%=ddlHeads.ClientID %>').find("option:selected").text();
            hdsval = $('#<%=ddlHeads.ClientID %>').find("option:selected").val();

            var headstext = $("[id*=HD_Headtxt]");
            headstext.val(hds);

            var hdval = $("[id*=HD_Headval]");
            hdval.val(hdsval);
        }

        function Reset() {
            var ddlhds = document.getElementById('#<%=ddlHeads.ClientID %>');
            ddlhds.selectedIndex = 0;

            return false;
        }

        var creditCashCnt = 0;
        $(function () {
            $("[id*=crbtnAdd]").click(function () {

                //Reference the GridView.
                var gridView = $("[id*=GridClnt]");

                //Reference the first row.
                var row = gridView.find("tr").eq(1);

                //Check if row is dummy, if yes then remove.

                var vvv = $.trim(row.find("td").eq(0).html());
                var amt = document.getElementById('<%=txtAmount.ClientID %>').value;

                if ($.trim(row.find("td").eq(0).html()) == "" && amt != "")
                    row.remove();
                else if (amt != "" && creditCashCnt == 0)
                    row.remove();
                //Clone the reference first row.
                row = row.clone(true);

                GetValue();

                if (amt != "") {
                    creditCashCnt++;
                    //Add the Name value to first cell.
                    var HD_Headtxt = $("[id*=HD_Headtxt]");
                    SetValue(row, 0, "Heads", HD_Headtxt);

                    //Add the Country value to second cell.
                    var txtAmount = $("[id*=txtAmount]");
                    SetValue(row, 1, "Amount", txtAmount);

                    //Add the Country value to second cell.
                    var txtNarration = $("[id*=txtDescription]");
                    SetValue(row, 2, "Description", txtNarration);

                    //Add the Country value to second cell.
                    var txtChequeNO = $("[id*=txtChequeNO]");
                    SetValue(row, 3, "ChequeNo", txtChequeNO);

                    //Add the Country value to second cell.
                    var HD_Headval = $("[id*=HD_Headval]");
                    SetValue(row, 4, "headid", HD_Headval);



                    //Add the Country value to second cell.
                    var txtcrfrom = $("[id*=txtcrfrom]");
                    SetValue(row, 5, "From", txtcrfrom);

                    //Add the Country value to second cell.
                    var txtcrto = $("[id*=txtcrto]");
                    SetValue(row, 6, "To", txtcrto);

                    //Add the Country value to second cell.
                    var txtsaleval = $("[id*=txtsaleval]");
                    SetValue(row, 7, "SaleValue", txtsaleval);

                    //Add the Country value to second cell.
                    var txtsaledate = $("[id*=txtsaledate]");
                    SetValue(row, 8, "SaleDate", txtsaledate);


                    // Reset();
                    //Add the row to the GridView.
                    gridView.append(row);
                }

                return false;

            });

            function SetValue(row, index, name, textbox) {
                //Reference the Cell and set the value.
                row.find("td").eq(index).html(textbox.val().replace(",", ";"));

                //Create and add a Hidden Field to send value to server.
                var input = $("<input type = 'hidden' />");
                input.prop("name", name);
                input.val(textbox.val().replace(",", ";"));
                row.find("td").eq(index).append(input);

                //Clear the TextBox.
                textbox.val("");
            }
        });

        $.noConflict();
        $(".crdelete").on('click', function () {
            alert("Credit Delete");
            if (creditCashCnt > 1) {
                $('.case:checkbox:checked').parents("tr").remove();
                creditCashCnt--;
                return;
            }
            else if ($('.case').is(":checked") == true) {
                $('.case').prop("checked", false);
                creditCashCnt = 0;
                var gridView = $("[id*=GridClnt]");
                var row = gridView.find("tr").eq(1);
                row.remove();
                row = row.clone(true);

                ClearValue(row, 0, "Heads", '');
                ClearValue(row, 1, "Amount", '');
                ClearValue(row, 2, "Description", '');
                ClearValue(row, 3, "ChequeNo", '');
                ClearValue(row, 4, "headid", '');
                ClearValue(row, 5, "From", '');
                ClearValue(row, 6, "To", '');
                ClearValue(row, 7, "SaleValue", '');
                ClearValue(row, 8, "SaleDate", '');
                gridView.append(row);

            }
        });

        function ClearValue(row, index, name, textbox) {
            //Reference the Cell and set the value.
            row.find("td").eq(index).html(textbox);
            //Create and add a Hidden Field to send value to server.
            var input = $("<input type = 'hidden' />");
            input.prop("name", name);
            input.val(textbox);
            row.find("td").eq(index).append(input);
        }


        $.noConflict();
        function select_all() {
            $('input[class=case]:checkbox').each(function () {
                if ($('input[class=check_all]:checkbox:checked').length == 0) {
                    $(this).prop("checked", false);
                } else {
                    $(this).prop("checked", true);
                }
            });
        }


        function DebGetValue()
        {

            var dbhds = "";
            var dbhdsval = "";

            dbhds = $('#<%=ddlHeadsDebit.ClientID %>').find("option:selected").text();
            dbhdsval = $('#<%=ddlHeadsDebit.ClientID %>').find("option:selected").val();

            var headstext = $("[id*=hidDb_Headtxt]");
            headstext.val(dbhds);

            var hdval = $("[id*=hidDb_HeadVal]");
            hdval.val(dbhdsval);


            var dbloanhd = "";
            var dbloanhdval = "";

            dbloanhd = $('#<%=ddlaccruedloan.ClientID %>').find("option:selected").text();
            dbloanhdval = $('#<%=ddlaccruedloan.ClientID %>').find("option:selected").val();

            var headstext1 = $("[id*=hidDb_LoanHeadtxt]");
            headstext1.val(dbloanhd);

            var hdval1 = $("[id*=hidDb_LoanHeadval]");
            hdval1.val(dbloanhdval);


            var dbbankhd = "";
            var dbbankhdval = "";

            dbbankhd = $('#<%=ddlaccruedbank.ClientID %>').find("option:selected").text();
            dbbankhdval = $('#<%=ddlaccruedbank.ClientID %>').find("option:selected").val();

            var headstext2 = $("[id*=hidDb_BankHeadtxt]");
            headstext2.val(dbbankhd);

            var hdval2 = $("[id*=hidDb_BankHeadval]");
            hdval2.val(dbbankhdval);


            var dbrenthd = "";
            var dbrenthdval = "";

            dbrenthd = $('#<%=ddlaccruedrent.ClientID %>').find("option:selected").text();
            dbrenthdval = $('#<%=ddlaccruedrent.ClientID %>').find("option:selected").val();

            var headstext3 = $("[id*=hidDb_RentHeadtxt]");
            headstext3.val(dbrenthd);

            var hdval3 = $("[id*=hidDb_RentHeadval]");
            hdval3.val(dbrenthdval);


        }

        function Reset() {
            var dbddlhds = document.getElementById('#<%=ddlHeadsDebit.ClientID %>');
            dbddlhds.selectedIndex = 0;

            return false;
        }

        var debitCashCnt = 0;
        $(function () {
            $("[id*=DbbtnAdd]").click(function () {

                //Reference the GridView.
                var gridView = $("[id*=GrdDbClnt]");

                //Reference the first row.
                var row = gridView.find("tr").eq(1);

                //Check if row is dummy, if yes then remove.
                var debamt = document.getElementById('<%=debitAmnt.ClientID %>').value;

                if ($.trim(row.find("td").eq(0).html()) == "" && debamt != "")
                    row.remove();
                else if (debamt != "" && debitCashCnt == 0)
                    row.remove();

                //Clone the reference first row.
                row = row.clone(true);

                DebGetValue();

                if (debamt != "") {
                    debitCashCnt++;
                    //Add the bank head value to first cell.
                    var hidDb_Headtxt = $("[id*=hidDb_Headtxt]");
                    SetValue1(row, 0, "DbHeads", hidDb_Headtxt);

                    //Add the ddebit amount value to second cell.
                    var debitAmnt = $("[id*=debitAmnt]");
                    SetValue1(row, 1, "DbAmount", debitAmnt);

                    //Add the desc to third cell.
                    var txtDebitdesc = $("[id*=txtDebitdesc]");
                    SetValue1(row, 2, "DbDescription", txtDebitdesc);

                    //Add the head val to fourth cell.
                    var hidDb_HeadVal = $("[id*=hidDb_HeadVal]");
                    SetValue1(row, 3, "Dbheadid", hidDb_HeadVal);


                    var loanisVisible = $('#<%=ddlaccruedloan.ClientID %>').is(':visible');
                    var bankisVisible = $('#<%=ddlaccruedbank.ClientID %>').is(':visible');
                    var rentisVisible = $('#<%=ddlaccruedrent.ClientID %>').is(':visible');

                    
                    if (loanisVisible == true) {
                        var hidDb_LoanHeadtxt = $("[id*=hidDb_LoanHeadtxt]");
                        SetValue1(row, 4, "Dbhead", hidDb_LoanHeadtxt);

                        var hidDb_LoanHeadval = $("[id*=hidDb_LoanHeadval]");
                        SetValue1(row, 5, "Dbheadaccid", hidDb_LoanHeadval);
                    }
                    else if (bankisVisible == true) {
                        var hidDb_BankHeadtxt = $("[id*=hidDb_BankHeadtxt]");
                        SetValue1(row, 4, "Dbhead", hidDb_BankHeadtxt);

                        var hidDb_BankHeadval = $("[id*=hidDb_BankHeadval]");
                        SetValue1(row, 5, "Dbheadaccid", hidDb_BankHeadval);
                    }
                    else if (rentisVisible == true)
                    {
                        var hidDb_RentHeadtxt = $("[id*=hidDb_RentHeadtxt]");
                        SetValue1(row, 4, "Dbhead", hidDb_RentHeadtxt);

                        var hidDb_RentHeadval = $("[id*=hidDb_RentHeadval]");
                        SetValue1(row, 5, "Dbheadaccid", hidDb_RentHeadval);
                    }


                        var txtaccdatepur = $("[id*=txtaccdatepur]");
                        SetValue1(row, 6, "Dbpurchasedt", txtaccdatepur);
                      

                    var txtaccdatefrom = $("[id*=txtaccdatefrom]");
                    SetValue1(row, 7, "Dbfrom", txtaccdatefrom);
                    
                    var txtaccdateto = $("[id*=txtaccdateto]");
                    SetValue1(row, 8, "Dbto", txtaccdateto);

                    var txtrentrate = $("[id*=txtrentrate]");
                    SetValue1(row, 9, "Dbrent", txtrentrate);

                    // Reset();
                    //Add the row to the GridView.
                    gridView.append(row);
                }

                return false;

            });

            function SetValue1(row, index, name, textbox) {
                //Reference the Cell and set the value.
                row.find("td").eq(index).html(textbox.val().replace(",", ";"));

                //Create and add a Hidden Field to send value to server.
                var input = $("<input type = 'hidden' />");
                input.prop("name", name);
                input.val(textbox.val().replace(",", ";"));
                row.find("td").eq(index).append(input);

                //Clear the TextBox.
                textbox.val("");
            }
        });


        $(".dbdelete").on('click', function () {
            alert("Debit Delete");
            if (debitCashCnt > 1) {
                $('.dbcase:checkbox:checked').parents("tr").remove();
                debitCashCnt--;
                return;
            }
            else if ($('.dbcase').is(":checked") == true) {
                $('.dbcase').prop("checked", false);
                debitCashCnt = 0;
                var gridView = $("[id*=GrdDbClnt]");
                var row = gridView.find("tr").eq(1);
                row.remove();
                row = row.clone(true);

                ClearValue(row, 0, "DbHeads", '');
                ClearValue(row, 1, "DbAmount", '');
                ClearValue(row, 2, "DbDescription", '');
                ClearValue(row, 3, "Dbheadid", '');


                ClearValue(row, 4, "Dbhead", ''); 
                ClearValue(row, 5, "Dbheadaccid", '');
                ClearValue(row, 6, "Dbpurchasedt", '');
                ClearValue(row, 7, "Dbfrom", '');
                ClearValue(row, 8, "Dbto", '');
                ClearValue(row, 9, "Dbrent", '');
                gridView.append(row);
            }


        });

        function select_all() {
            $('input[class=case]:checkbox').each(function () {
                if ($('input[class=check_all]:checkbox:checked').length == 0) {
                    $(this).prop("checked", false);
                } else {
                    $(this).prop("checked", true);
                }
            });
        }


    </script>

    
     <script type="text/javascript">
         $(document).ready(function () {
             $("#<%=rbtndep.ClientID%>").change(
                 function () {
                     $("#<%=lblsaleval.ClientID%>").hide();
                     $("#<%=txtsaleval.ClientID%>").hide();
                      $("#<%=lblsaledate.ClientID%>").hide();
                      $("#<%=txtsaledate.ClientID%>").hide();

                      $("#<%=lblcrfrom.ClientID%>").show();
                      $("#<%=txtcrfrom.ClientID%>").show();
                      $("#<%=lblcrto.ClientID%>").show();
                      $("#<%=txtcrto.ClientID%>").show();
                 });
             $("#<%=rbtnsale.ClientID%>").change(
                 function () {

                     $("#<%=lblcrfrom.ClientID%>").hide();
                     $("#<%=txtcrfrom.ClientID%>").hide();
                     $("#<%=lblcrto.ClientID%>").hide();
                    $("#<%=txtcrto.ClientID%>").hide();


                     $("#<%=lblsaleval.ClientID%>").show();
                     $("#<%=txtsaleval.ClientID%>").show();
                     $("#<%=lblsaledate.ClientID%>").show();
                     $("#<%=txtsaledate.ClientID%>").show();
                 });

             $("#<%=lblaccbank.ClientID%>").hide();
            $("#<%=ddlaccruedbank.ClientID%>").hide();

            $("#<%=lblaccloan.ClientID%>").hide();
            $("#<%=ddlaccruedloan.ClientID%>").hide();


            $("#<%=lblaccdatefrom.ClientID%>").hide();
            $("#<%=txtaccdatefrom.ClientID%>").hide();

            $("#<%=lblaccrentrate.ClientID%>").hide();
            $("#<%=txtrentrate.ClientID%>").hide();

             $("#<%=lblaccdateto.ClientID%>").hide();
             $("#<%=txtaccdateto.ClientID%>").hide();

             $("#<%=lblaccpurchase.ClientID%>").hide();
             $("#<%=txtaccdatepur.ClientID%>").hide();

             $("#<%=lblaccrent.ClientID%>").hide();
             $("#<%=ddlaccruedrent.ClientID%>").hide();


             $(".rbutton").hide();
             

             $("#<%=lblcrfrom.ClientID%>").hide();
             $("#<%=txtcrfrom.ClientID%>").hide();
             $("#<%=lblcrto.ClientID%>").hide();
             $("#<%=txtcrto.ClientID%>").hide();


             $("#<%=lblsaleval.ClientID%>").hide();
             $("#<%=txtsaleval.ClientID%>").hide();
             $("#<%=lblsaledate.ClientID%>").hide();
             $("#<%=txtsaledate.ClientID%>").hide();

             $(function () {
                 $("[id*=btnDelete]").removeAttr("onclick");
                 $("#dialog").dialog({
                     modal: true,
                     autoOpen: false,
                     title: "Confirmation",
                     width: 350,
                     height: 160,
                     buttons: [
                         {
                             id: "Yes",
                             text: "Yes",
                             click: function () {
                                 $("[id*=btnsave]").attr("rel", "save");
                                 $("[id*=btnsave]").click();
                                 yesclick();
                                 $(this).dialog('close');

                             }
                         },
                         {
                             id: "No",
                             text: "No",
                             click: function () {
                                 $(this).dialog('close');
                             }
                         }

                     ]
                 });

             });

         });
         
         $("#<%=btnGenerate.ClientID %>").click(function () {
             if ($(this).attr("rel") != "save") {
                 $('#dialog').dialog('open');
                 return false;
             } else {
                 __doPostBack(this.name, '');
             }
         });



        <%-- $("#<%=btnGenerate.ClientID %>").click(function ()
         {--%>
             function yesclick() {

             var todate = $('#<%=txtDate.ClientID %>').val();
             var voucnumber = $('#<%=txtVoucherNo.ClientID %>').val();
             var series = $('#<%=txtSeries.ClientID %>').val();
                 //series ="VOUCHER"
                 series = "INVEST"

             var recby = $('#<%=txtReceivedBy.ClientID %>').val();

             recby = "ADMIN";

             //credit table
          
             var table, tbody, i, rowLen, row, j, colLen, cell;

             var items = [];

             var inneritems = [];

             table = document.getElementById('ctl00_cphMainContent_GridClnt');

             //alert(table.rows.length);

             tbody = table.tBodies[0];

             for (i = 0, rowLen = tbody.rows.length; i < rowLen; i++) {
                 row = tbody.rows[i];
                 if (i > 0) {
                     for (j = 0, colLen = row.cells.length - 1; j < colLen; j++) {

                         cell = row.cells[j].innerHTML;

                         cell.replace(/^\s+|\s+$/gm, '');

                         var oldString = cell.trim();

                         var newString = "";

                         var match = "";

                         if (oldString != "") {

                             newString = oldString.split('value=')[1];

                             match = newString.match(/(?:"[^"]*"|^[^"]*$)/)[0].replace(/"/g, "");
                         }
                         else {
                             match = "";

                         }
                         inneritems.push(match);
                     }

                     console.log(inneritems);

                     items.push(inneritems);

                     inneritems = [];
                 }

             }
             //console.log(items);
             //alert(items);


             //debit table 
            
             var dtable, dtbody, di, drowLen, drow, dj, dcolLen, dcell;

             var ditems = [];

             var dinneritems = [];

             dtable = document.getElementById('ctl00_cphMainContent_GrdDbClnt');

             //alert(dtable.rows.length);

             dtbody = dtable.tBodies[0];

             for (i = 0, rowLen = dtbody.rows.length; i < rowLen; i++) {
                 drow = dtbody.rows[i];

                 if (i > 0) {
                     for (j = 0, dcolLen = drow.cells.length - 1; j < dcolLen; j++) {

                         dcell = drow.cells[j].innerHTML;

                         dcell.replace(/^\s+|\s+$/gm, '');

                         var oldString = dcell.trim();

                         var newString = "";

                         var match = "";

                         if (oldString != "") {

                             newString = oldString.split('value=')[1];

                             match = newString.match(/(?:"[^"]*"|^[^"]*$)/)[0].replace(/"/g, "");
                         } else {
                             match = "";

                         }

                         dinneritems.push(match);
                     }

                     console.log(dinneritems);

                     ditems.push(dinneritems);

                     dinneritems = [];
                 }

             }
             $.ajax(
                 {
                 type: "POST",
                 contentType: "application/json; charset=utf-8",
                 url: "AccruedVoucherMultiple.aspx/voucherinsert",
                 data: JSON.stringify({ creditarray: items, debitarray: ditems, todate: todate, VoucherNo: voucnumber, Series: series, ReceivedBy: recby }),
                 dataType: "json",
                 success: function (response) {

                     if (response.d == "Mismatch") {
                         alert("Voucher Mismatch");
                     }
                     else {
                         alert("Inserted Successfully");
                         location.reload(true);
                     }

                 },
                 error: function (Result) {
                     alert("Error");
                 }
             });

         };


         function ondebitchange() {


            var ser = $("#<%=ddlHeadsDebit.ClientID%>").find("option:selected").text(); //id name for dropdown list   
            var cid = $("#<%=ddlHeadsDebit.ClientID%>").find("option:selected").val(); //text name for dropdown list  
            var rcid = $("#<%=ddlHeadsDebit.ClientID%> option:selected").val();

            var branchid = '<%=Session["Branchid"]%>';
            var date = $('#<%=txtDate.ClientID %>').val();
             
            
            var bnktrue = rcid.includes(":");
          
            var chittrue = rcid.includes("|");
           

            if (bnktrue == true) {

                var rtid = rcid.split(":", 1);

                var valrt = rcid.split(":").pop(-1);

                if (rtid == 8) {

                    $("#<%=lblaccloan.ClientID%>").show();
                    $("#<%=ddlaccruedloan.ClientID%>").show();

                    $("#<%=lblaccdatefrom.ClientID%>").show();
                    $("#<%=txtaccdatefrom.ClientID%>").show();

                    $("#<%=lblaccdateto.ClientID%>").show();
                    $("#<%=txtaccdateto.ClientID%>").show();

                    $("#<%=lblaccbank.ClientID%>").hide();
                    $("#<%=ddlaccruedbank.ClientID%>").hide();

                    $("#<%=lblaccrent.ClientID%>").hide();
                    $("#<%=ddlaccruedrent.ClientID%>").hide();


                    $("#<%=lblaccrentrate.ClientID%>").hide();
                    $("#<%=txtrentrate.ClientID%>").hide();

                    getaccruedloan(rtid.toString(), branchid.toString(), date.toString());
                   

                    $("#<%=lblaccpurchase.ClientID%>").hide();
                    $("#<%=txtaccdatepur.ClientID%>").hide();

                }
                else if (rtid == 4) {
                   

                    $("#<%=lblaccloan.ClientID%>").hide();
                    $("#<%=ddlaccruedloan.ClientID%>").hide();

                    <%--$("#<%=lblaccbank.ClientID%>").show();
                    $("#<%=ddlaccruedbank.ClientID%>").show();--%>
                    $("#<%=lblaccbank.ClientID%>").hide();
                    $("#<%=ddlaccruedbank.ClientID%>").hide();

                    $("#<%=lblaccdatefrom.ClientID%>").hide();
                    $("#<%=txtaccdatefrom.ClientID%>").hide();

                    $("#<%=lblaccdateto.ClientID%>").hide();
                    $("#<%=txtaccdateto.ClientID%>").hide();

                    getaccruedbank(rtid.toString(), branchid.toString(), date.toString());

                    $("#<%=lblaccpurchase.ClientID%>").hide();
                    $("#<%=txtaccdatepur.ClientID%>").hide();

                    $("#<%=lblaccrent.ClientID%>").hide();
                    $("#<%=ddlaccruedrent.ClientID%>").hide();


                     $("#<%=lblaccrentrate.ClientID%>").hide();
                    $("#<%=txtrentrate.ClientID%>").hide();

                }
                else if (rtid == 2) {
                    
                    
                    $("#<%=lblaccpurchase.ClientID%>").show();
                    $("#<%=txtaccdatepur.ClientID%>").show();


                    $("#<%=lblaccbank.ClientID%>").hide();
                    $("#<%=ddlaccruedbank.ClientID%>").hide();

                   $("#<%=lblaccloan.ClientID%>").hide();
                    $("#<%=ddlaccruedloan.ClientID%>").hide();

                    $("#<%=lblaccdatefrom.ClientID%>").hide();
                    $("#<%=txtaccdatefrom.ClientID%>").hide();

                    $("#<%=lblaccdateto.ClientID%>").hide();
                    $("#<%=txtaccdateto.ClientID%>").hide();  


                    $("#<%=lblaccrent.ClientID%>").hide();
                    $("#<%=ddlaccruedrent.ClientID%>").hide();


                     $("#<%=lblaccrentrate.ClientID%>").hide();
                    $("#<%=txtrentrate.ClientID%>").hide();

                }
                else if (rtid == 11) {

                    if (valrt == 94 || valrt == 1132559 || valrt == 1132561)
                    {

                        $("#<%=lblaccbank.ClientID%>").hide();
                        $("#<%=ddlaccruedbank.ClientID%>").hide();

                        $("#<%=lblaccpurchase.ClientID%>").hide();
                        $("#<%=txtaccdatepur.ClientID%>").hide();

                        $("#<%=lblaccloan.ClientID%>").hide();
                        $("#<%=ddlaccruedloan.ClientID%>").hide();

                        getaccruedrent(rtid.toString(), branchid.toString(), date.toString());

                        $("#<%=lblaccrent.ClientID%>").show();
                        $("#<%=ddlaccruedrent.ClientID%>").show();

                        $("#<%=lblaccdatefrom.ClientID%>").show();
                        $("#<%=txtaccdatefrom.ClientID%>").show();

                        $("#<%=lblaccdateto.ClientID%>").show();
                        $("#<%=txtaccdateto.ClientID%>").show();

                        $("#<%=lblaccrentrate.ClientID%>").show();
                        $("#<%=txtrentrate.ClientID%>").show();

                    }
                    else
                    {
                        $("#<%=lblaccbank.ClientID%>").hide();
                        $("#<%=ddlaccruedbank.ClientID%>").hide();

                         $("#<%=lblaccpurchase.ClientID%>").hide();
                        $("#<%=txtaccdatepur.ClientID%>").hide();

                        $("#<%=lblaccloan.ClientID%>").hide();
                        $("#<%=ddlaccruedloan.ClientID%>").hide();


                        $("#<%=lblaccrent.ClientID%>").hide();
                        $("#<%=ddlaccruedrent.ClientID%>").hide();


                        $("#<%=lblaccrentrate.ClientID%>").hide();
                        $("#<%=txtrentrate.ClientID%>").hide();


                        $("#<%=lblaccdatefrom.ClientID%>").show();
                        $("#<%=txtaccdatefrom.ClientID%>").show();

                         $("#<%=lblaccdateto.ClientID%>").show();
                        $("#<%=txtaccdateto.ClientID%>").show();

                    }
                }

                else {

                    $("#<%=lblaccbank.ClientID%>").hide();
                    $("#<%=ddlaccruedbank.ClientID%>").hide();


                    $("#<%=lblaccloan.ClientID%>").hide();
                    $("#<%=ddlaccruedloan.ClientID%>").hide();

                    $("#<%=lblaccdatefrom.ClientID%>").hide();
                    $("#<%=txtaccdatefrom.ClientID%>").hide();

                    $("#<%=lblaccdateto.ClientID%>").hide();
                    $("#<%=txtaccdateto.ClientID%>").hide();

                    $("#<%=lblaccpurchase.ClientID%>").hide();
                    $("#<%=txtaccdatepur.ClientID%>").hide();


                    $("#<%=lblaccrent.ClientID%>").hide();
                    $("#<%=ddlaccruedrent.ClientID%>").hide();


                    $("#<%=lblaccrentrate.ClientID%>").hide();
                        $("#<%=txtrentrate.ClientID%>").hide();
                }
            }
            else if (chittrue == true) {

                $("#<%=txtChequeNO.ClientID%>").hide();
                $("#<%=lblChequeNO.ClientID%>").hide();

                document.getElementById('<%=txtDescription.ClientID %>').value = "";
            }
            $("#<%=ddlaccruedloan.ClientID%>").addClass('chzn-container chzn-container-single');
            $("#<%=ddlaccruedbank.ClientID%>").addClass('chzn-container chzn-container-single');

         }


         function getaccruedbank(hdid, branchid, date) {
             $.ajax({
                 type: "POST",
                 contentType: "application/json; charset=utf-8",
                 url: "AccruedVoucherMultiple.aspx/fillaccured",
                 data: JSON.stringify({ hdid: hdid, branchid: branchid, date: date }),
                 dataType: "json",
                 success: function (data) {

                     var ddlaccruedbank = $("#<%=ddlaccruedbank.ClientID%>");

                    $.each(data.d, function () {
                        ddlaccruedbank.append($("<option></option>").val(this['Value']).html(this['Text']));
                    });
                },
                error: function (Result) {
                    alert("Error");
                }
            });
            $("#<%=ddlaccruedbank.ClientID%>").addClass('chzn-select');
         }

         function getaccruedrent(hdid, branchid, date) {
             $.ajax({
                 type: "POST",
                 contentType: "application/json; charset=utf-8",
                 url: "AccruedVoucherMultiple.aspx/fillaccured",
                 data: JSON.stringify({ hdid: hdid, branchid: branchid, date: date }),
                 dataType: "json",
                 success: function (data) {

                     var ddlaccruedrent = $("#<%=ddlaccruedrent.ClientID%>");

                     $(ddlaccruedrent).empty();

                     $.each(data.d, function () {
                         ddlaccruedrent.append($("<option></option>").val(this['Value']).html(this['Text']));
                     });
                 },
                 error: function (Result) {
                     alert("Error");
                 }
             });
             $("#<%=ddlaccruedrent.ClientID%>").addClass('chzn-select');
         }

         /////

         function getaccruedloan(hdid, branchid, date) {
             $.ajax(
                 {
                 type: "POST",
                 contentType: "application/json; charset=utf-8",
                 url: "AccruedVoucherMultiple.aspx/fillaccured",
                 data: JSON.stringify({ hdid: hdid, branchid: branchid, date: date }),
                 dataType: "json",
                 success: function (data) {

                     var ddlaccruedloan = $("#<%=ddlaccruedloan.ClientID%>");

                    $.each(data.d, function () {
                        ddlaccruedloan.append($("<option></option>").val(this['Value']).html(this['Text']));
                    });
                },
                error: function (Result) {
                    alert("Error");
                }
                 }
             );
            $("#<%=ddlaccruedloan.ClientID%>").addClass('chzn-select');
         }



        function purchasedate() {
             var inputDate = $('#<%=txtaccdatepur.ClientID %>').val();
             var Reg_Expression = /^\d{1,2}\/\d{1,2}\/\d{4}$/;
             if (Reg_Expression.test(inputDate)) {
                 var partitionedDate = inputDate.split('/');
                 var nDay = parseInt(partitionedDate[0], 10);
                 var nMonth = parseInt(partitionedDate[1], 10);
                 var nYear = parseInt(partitionedDate[2], 10);
                 var dDate = new Date(nYear, nMonth - 1, nDay);
                 if ((dDate.getFullYear() == nYear) && (dDate.getMonth() == nMonth - 1) && (dDate.getDate() == nDay)) {
                 }
                 else {
                     document.getElementById('<%=txtaccdatepur.ClientID %>').value = "";
                    alert('Incorrect Date Format..');
                }
            }
            else {
                document.getElementById('<%=txtaccdatepur.ClientID %>').value = "";
                alert('Incorrect Date Format..');
            }
         }

         function fromdate() {
             var inputDate = $('#<%=txtaccdatefrom.ClientID %>').val();
            var Reg_Expression = /^\d{1,2}\/\d{1,2}\/\d{4}$/;
            if (Reg_Expression.test(inputDate)) {
                var partitionedDate = inputDate.split('/');
                var nDay = parseInt(partitionedDate[0], 10);
                var nMonth = parseInt(partitionedDate[1], 10);
                var nYear = parseInt(partitionedDate[2], 10);
                var dDate = new Date(nYear, nMonth - 1, nDay);
                if ((dDate.getFullYear() == nYear) && (dDate.getMonth() == nMonth - 1) && (dDate.getDate() == nDay)) {
                }
                else {
                    document.getElementById('<%=txtaccdatefrom.ClientID %>').value = "";
                     alert('Incorrect Date Format..');
                 }
             }
             else {
                 document.getElementById('<%=txtaccdatefrom.ClientID %>').value = "";
                 alert('Incorrect Date Format..');
             }
         }
         


        function todate() {
             var inputDate = $('#<%=txtaccdateto.ClientID %>').val();
            var Reg_Expression = /^\d{1,2}\/\d{1,2}\/\d{4}$/;
            if (Reg_Expression.test(inputDate)) {
                var partitionedDate = inputDate.split('/');
                var nDay = parseInt(partitionedDate[0], 10);
                var nMonth = parseInt(partitionedDate[1], 10);
                var nYear = parseInt(partitionedDate[2], 10);
                var dDate = new Date(nYear, nMonth - 1, nDay);
                if ((dDate.getFullYear() == nYear) && (dDate.getMonth() == nMonth - 1) && (dDate.getDate() == nDay)) {
                }
                else {
                    document.getElementById('<%=txtaccdateto.ClientID %>').value = "";
                    alert('Incorrect Date Format..');
                }
            }
            else {
                document.getElementById('<%=txtaccdateto.ClientID %>').value = "";
                alert('Incorrect Date Format..');
            }
         }






         function creditfromdate() {
             var inputDate = $('#<%=txtcrfrom.ClientID %>').val();
             var Reg_Expression = /^\d{1,2}\/\d{1,2}\/\d{4}$/;
             if (Reg_Expression.test(inputDate)) {
                 var partitionedDate = inputDate.split('/');
                 var nDay = parseInt(partitionedDate[0], 10);
                 var nMonth = parseInt(partitionedDate[1], 10);
                 var nYear = parseInt(partitionedDate[2], 10);
                 var dDate = new Date(nYear, nMonth - 1, nDay);
                 if ((dDate.getFullYear() == nYear) && (dDate.getMonth() == nMonth - 1) && (dDate.getDate() == nDay)) {
                 }
                 else {
                     document.getElementById('<%=txtcrfrom.ClientID %>').value = "";
                    alert('Incorrect Date Format..');
                }
            }
            else {
                document.getElementById('<%=txtcrfrom.ClientID %>').value = "";
                alert('Incorrect Date Format..');
            }
         }



         function credittodate() {
             var inputDate = $('#<%=txtcrto.ClientID %>').val();
             var Reg_Expression = /^\d{1,2}\/\d{1,2}\/\d{4}$/;
             if (Reg_Expression.test(inputDate)) {
                 var partitionedDate = inputDate.split('/');
                 var nDay = parseInt(partitionedDate[0], 10);
                 var nMonth = parseInt(partitionedDate[1], 10);
                 var nYear = parseInt(partitionedDate[2], 10);
                 var dDate = new Date(nYear, nMonth - 1, nDay);
                 if ((dDate.getFullYear() == nYear) && (dDate.getMonth() == nMonth - 1) && (dDate.getDate() == nDay)) {
                 }
                 else {
                     document.getElementById('<%=txtcrto.ClientID %>').value = "";
                    alert('Incorrect Date Format..');
                }
            }
            else {
                document.getElementById('<%=txtcrto.ClientID %>').value = "";
                alert('Incorrect Date Format..');
            }
         }
         


         function saledate() {
             var inputDate = $('#<%=txtsaledate.ClientID %>').val();
             var Reg_Expression = /^\d{1,2}\/\d{1,2}\/\d{4}$/;
             if (Reg_Expression.test(inputDate)) {
                 var partitionedDate = inputDate.split('/');
                 var nDay = parseInt(partitionedDate[0], 10);
                 var nMonth = parseInt(partitionedDate[1], 10);
                 var nYear = parseInt(partitionedDate[2], 10);
                 var dDate = new Date(nYear, nMonth - 1, nDay);
                 if ((dDate.getFullYear() == nYear) && (dDate.getMonth() == nMonth - 1) && (dDate.getDate() == nDay)) {
                 }
                 else {
                     document.getElementById('<%=txtsaledate.ClientID %>').value = "";
                     alert('Incorrect Date Format..');
                 }
             }
             else {
                 document.getElementById('<%=txtsaledate.ClientID %>').value = "";
                 alert('Incorrect Date Format..');
             }
         }
        
        </script>

</asp:Content>
