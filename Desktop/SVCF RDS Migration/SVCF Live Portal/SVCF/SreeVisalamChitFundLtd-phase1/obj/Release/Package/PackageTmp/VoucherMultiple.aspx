<%@ Page Language="C#" Culture="en-GB" MasterPageFile="~/Branch.Master" AutoEventWireup="true"
    CodeBehind="VoucherMultiple.aspx.cs" Inherits="SreeVisalamChitFundLtd_phase1.VoucherMultiple"
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
            //var AsciiValue = event.keyCode
            //if ((AsciiValue >= 48 && AsciiValue <= 57) || (AsciiValue == 8 || AsciiValue == 127))
            //    event.returnValue = true;
            //else
            //    event.returnValue = false;

          
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

           <%-- if (document.getElementById('<%=txtAmountDebit.ClientID%>').value == "") {                
                document.getElementById("lbldbamnt").innerText = "*";
                return false;
            }--%>
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
                        Voucher Details
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
                                                    <asp:TextBox AutoPostBack="true" Width="100" ValidationGroup="Generate" TabIndex="1" ID="txtDate" onchange="CheckDate();"
                                                        CssClass="input-text maskdate" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ForeColor="Red" ID="RequiredFieldValidator6" ErrorMessage="Rquired!!!"
                                                        ControlToValidate="txtDate" ValidationGroup="Generate" Display="Dynamic" runat="server"></asp:RequiredFieldValidator>
                                                    <asp:CompareValidator ForeColor="Red" Type="Date" ID="CompareValidator11" ValidationGroup="Generate"
                                                        ControlToValidate="txtDate" Display="Dynamic" Operator="DataTypeCheck" runat="server"
                                                        ErrorMessage="Incorrect!!!"></asp:CompareValidator>
                                                    <asp:RangeValidator ID="rvDate" EnableClientScript="false" runat="server"
                                                        Type="Date" ControlToValidate="txtDate" ValidationGroup="Generate" ForeColor="Red" Display="Dynamic"
                                                        ErrorMessage="*"></asp:RangeValidator>
                                                    
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
                                            <td></td>
                                            <td>
                                                <asp:Label ID="lblamt" runat="server" Text="Amount"></asp:Label><br />
                                                <asp:TextBox Width="150" TabIndex="4" Text='<%#Eval("Amount") %>'
                                                    CssClass="twitterStyleTextbox  sp_currency" ID="txtAmount" runat="server" onkeypress="NumberOnly(event,this);">
                                                </asp:TextBox>
                                                <label id="lblamnterr" style="width: 50px;"></label>
                                            </td>
                                            <td></td>
                                            <td>
                                                <asp:Label ID="lblDesc" runat="server" Text="Description"></asp:Label>
                                                <asp:TextBox TextMode="MultiLine" Height="50" TabIndex="5"
                                                    ValidationGroup="GrpRow" CssClass="input-text" ID="txtDescription" runat="server">
                                                </asp:TextBox>
                                                <label id="lbldesccr" style="width: 50px;"></label>
                                            </td>
                                            <td></td>

                                            <td>
                                                <asp:Label ID="lblChequeNO" runat="server" Text="Cheque NO"></asp:Label>
                                                <br />
                                                <asp:TextBox Width="100"
                                                    MaxLength="7" ID="txtChequeNO"
                                                    runat="server">
                                                </asp:TextBox>
                                            </td>
                                            <td></td>

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
                                <div></div>

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
                                                TabIndex="7" AutoPostBack="false" ID="ddlHeadsDebit" runat="server">
                                            </asp:DropDownList>
                                            <label id="lbldeberror" style="width: 50px;"></label>
                                            <asp:HiddenField ID="hidDb_Headtxt" runat="server" />
                                            <asp:HiddenField ID="hidDb_HeadVal" runat="server" />
                                        </td>
                                        <td></td>



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

                                            <asp:TemplateField HeaderStyle-BackColor="Gray" HeaderStyle-BorderStyle="Solid" HeaderStyle-BorderWidth="2px"
                                                ItemStyle-BorderColor="Gray" ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px">
                                                <ItemTemplate>
                                                    <input type='checkbox' class='dbcase' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>

                                <%-- <div style="vertical-align:top; overflow:auto;border-color:Silver;height:200px; margin-top: 3px; 
                                                            width:950px; overflow-y: scroll;border: 2px solid gray;">

                                           <%--<asp:GridView ID="GViewDB_Selected" BorderStyle="Solid" runat="server" 
                                  CellSpacing="2" Font-Names="Verdana" ForeColor="#333333" GridLines="Both" Height="100px"
                                  AutoGenerateColumns="false" CssClass="Trans twelve columns" Width="900px" onrowdeleting="GViewDB_Selected_RowDeleting">                                <RowStyle BackColor="#F7F6F3" />
                                    <RowStyle CssClass="GridViewRowStyle" />  
                                    <HeaderStyle CssClass="GridViewHeaderStyle" /> 
                                    <Columns>
                                       <asp:BoundField HeaderText="RowNumber"  DataField="RowNumber" Visible="false"
                                       HeaderStyle-BorderColor="Gray" HeaderStyle-BorderStyle="Solid" HeaderStyle-BorderWidth="2px"
                                       ItemStyle-BorderColor="Gray" ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px" />
                                       
                                       <asp:BoundField HeaderText="Heads" DataField="Heads" 
                                       HeaderStyle-BorderColor="Gray" HeaderStyle-BorderStyle="Solid" HeaderStyle-BorderWidth="2px"
                                       ItemStyle-BorderColor="Gray" ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px"/>  
                                       
                                       <asp:BoundField HeaderText="Amount" DataField="Amount" 
                                       HeaderStyle-BorderColor="Gray" HeaderStyle-BorderStyle="Solid" HeaderStyle-BorderWidth="2px"
                                       ItemStyle-BorderColor="Gray" ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px"/>                                                   
                                       
                                        <asp:BoundField HeaderText="Description"  DataField="Description" Visible="true"
                                       HeaderStyle-BorderColor="Gray" HeaderStyle-BorderStyle="Solid" HeaderStyle-BorderWidth="2px"
                                       ItemStyle-BorderColor="Gray" ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px" /> 
                                                                       

                                       <asp:TemplateField HeaderText="CrHeadId" Visible="false">
                                          <ItemTemplate>
                                            <asp:Label ID="lbldbhdid" runat="server" Text='<%#Eval("headid") %>'></asp:Label>
                                          </ItemTemplate>
                                       </asp:TemplateField>

                                       <asp:TemplateField ItemStyle-BorderColor="Gray" ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px"
                                            HeaderStyle-BorderColor="Gray" HeaderStyle-BorderStyle="Solid" HeaderStyle-BorderWidth="2px">
                                           <ItemTemplate>
                                             <span onclick="return confirm('Are you sure want to delete?')">             
                                             <asp:LinkButton ID="btnDelete" runat="server" CommandName="Delete">
                                               <asp:Image ID="img1" runat="server" ImageUrl="Images/Close.gif" />
                                             </asp:LinkButton>
                                            </span>
                                         </ItemTemplate>
                                       </asp:TemplateField>
                                    </Columns>                                   
                                  </asp:GridView>                     
                            </div>  --%>
                            </div>
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
                        <asp:Button TabIndex="11" CausesValidation="false" CssClass="btn-custom" ToolTip="Add" Style="display: block; width: 100px; margin: 0 auto;"
                            ID="btnGenerate" OnClick="btnGenerate_Click" Text="Generate" ValidationGroup="Generate"
                            runat="server" />
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>
    <asp:LinkButton Text="" runat="server" ID="btnShowPopup"></asp:LinkButton>
    <%--<ajax:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="btnShowPopup" PopupControlID="pnlpopup"
CancelControlID="btnNo" BackgroundCssClass="modalBackground">
</ajax:ModalPopupExtender>--%>
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
                        <%--  <asp:TemplateField HeaderText="Action">
                        <ItemTemplate>
                            <asp:Button Style="width: 100px;" OnClick="Member_Choose_Click" ID="oldbtnadd" CommandName="Add" CssClass="GreenyPushButton" runat="server"
                                Text="Choose" CausesValidation="False"  />
                        </ItemTemplate>
                    </asp:TemplateField>--%>
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
    <%--<asp:Panel CssClass="raised" ID="panCheque" runat="server" Width="600px" Style="display: none;
        max-height: 500px; min-height: 300px">
        <asp:Label runat="server" ID="Label2" Text="" Visible="false"> </asp:Label>
        <div style="top: 0px; height: 50px; text-align: center; padding: auto 0; width: 100%"
            class="boxheader">
            <asp:Label ID="Label3" runat="server" Text=""> </asp:Label>
        </div>
        <div id="Div1" style="max-height: 400px; min-height: 200px; overflow: auto; width: 100%;">
            <br />
            <br />
            <asp:Label ID="Label4" runat="server" Text="" Style="text-align: justify; vertical-align: middle;
                margin-left: 10px"> </asp:Label>
            <br />
            <br />
            <table cellspacing="10px" style="width: 90%; margin: 0 auto">
                <tr>
                    <td>
                        Type of Bank:
                    </td>
                    <td>
                        <asp:DropDownList Height="28px" AutoPostBack="true" OnSelectedIndexChanged="ddlTypeofBank_SelectedIndexChanged"
                            ReadOnly="true" ValidationGroup="cheque" ID="ddlTypeofBank" runat="server" CssClass="twitterStyleTextbox"
                            ForeColor="Black">
                            <asp:ListItem Text="--Select--" Value="--Select--"></asp:ListItem>
                            <asp:ListItem Text="Scheduled Banks" Value="Scheduled Banks"></asp:ListItem>
                            <asp:ListItem Text="Non Scheduled Banks" Value="Non Scheduled Banks"></asp:ListItem>
                            <asp:ListItem Text="Fixed deposits with Banks" Value="Fixed deposits with Banks"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:CompareValidator   Display="Dynamic"   ValidationGroup="cheque"  
                            Operator="NotEqual" ControlToValidate="ddlTypeofBank" ID="CompareValidator2"
                            ValueToCompare="--Select--" ErrorMessage="*" runat="server"> </asp:CompareValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        Choose Bank
                    </td>
                    <td>
                        <asp:DropDownList Height="28px" ReadOnly="true" ValidationGroup="cheque" ID="ddlListofBanks"
                            runat="server" CssClass="twitterStyleTextbox" ForeColor="Black">
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:CompareValidator   Display="Dynamic"   ValidationGroup="cheque"  
                            Operator="NotEqual" ControlToValidate="ddlListofBanks" ID="CompareValidator3"
                            ValueToCompare="--Select--" ErrorMessage="*" runat="server"> </asp:CompareValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        Account Number
                    </td>
                    <td>
                        <asp:TextBox ValidationGroup="cheque" ID="txtAccountNumber" runat="server" CssClass="twitterStyleTextbox"
                            ForeColor="Black"></asp:TextBox>
                    </td>
                    <td>
                        <asp:RequiredFieldValidator   ValidationGroup="cheque" ControlToValidate="txtAccountNumber"
                            ID="RequiredFieldValidator2"   Display="Dynamic"   runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        ChequeNo/DDNo
                        <asp:TextBox width=100  ValidationGroup="cheque" ID="txtChequeNo" runat="server" CssClass="twitterStyleTextbox"
                            ForeColor="Black"></asp:TextBox>
                    </td>
                    <td>
                        <asp:RequiredFieldValidator   ValidationGroup="cheque" ControlToValidate="txtChequeNo"
                            ID="RequiredFieldValidator3"   Display="Dynamic"   runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        Cheque-Amount
                    </td>
                    <td>
                        <asp:TextBox ValidationGroup="cheque" ID="txtChequeAmount" runat="server" CssClass="twitterStyleTextbox"
                            ForeColor="Black"></asp:TextBox>
                    </td>
                    <td>
                        <asp:RequiredFieldValidator     Display="Dynamic"   ValidationGroup="cheque"
                            ControlToValidate="txtChequeAmount" ID="RequiredFieldValidator4" runat="server"
                            ErrorMessage="*"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator     Display="Dynamic"   ValidationGroup="cheque"
                            ControlToValidate="txtChequeAmount" ValidationExpression="^\d+(\.\d{1,2})?$"
                            ID="RegularExpressionValidator1" runat="server" ErrorMessage="Invalid Amount"></asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        Cheque Date
                    </td>
                    <td>
                        <ajax:CalendarExtender CssClass="ajaxcalendar" ID="CalendarExtender1" TargetControlID="txtChequeDate"
                            Format="dd/MM/yyyy" runat="server">
                        </ajax:CalendarExtender>
                        <asp:TextBox ValidationGroup="cheque" ID="txtChequeDate" runat="server" CssClass="twitterStyleTextbox"
                            ForeColor="Black"></asp:TextBox>
                    </td>
                    <td>
                        <asp:RequiredFieldValidator     Display="Dynamic"   ValidationGroup="cheque"
                            ControlToValidate="txtChequeDate" ID="RequiredFieldValidator5" runat="server"
                            ErrorMessage="*"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        IFSC
                    </td>
                    <td>
                        <asp:TextBox ID="txtIFSC"   Display="Dynamic"   ValidationGroup="cheque" runat="server"
                            CssClass="twitterStyleTextbox" ForeColor="Black"></asp:TextBox>
                    </td>
                    <td>
                        <asp:RequiredFieldValidator   ValidationGroup="cheque" ControlToValidate="txtIFSC"
                            ID="RequiredFieldValidator10"   Display="Dynamic"   runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                    </td>
                </tr>
            </table>
            <br />
            <br />
        </div>
        <div class="boxheader">
            <div style="margin: 0 auto;">
                <asp:Button Width="100" ValidationGroup="cheque" CausesValidation="true" Style="margin: 0 auto"
                    CssClass="GreenyPushButton" ID="BtnChequeUpdate" OnClick="BtnChequeUpdate_Click"
                    runat="server" Text="yes" />
                <asp:Button Width="100" ValidationGroup="cheque" CausesValidation="false" Style="margin: 0 auto"
                    CssClass="GreenyPushButton" ID="BtnChequeCancel" OnClick="BtnChequeCancel_Click"
                    runat="server" Text="No" />
            </div>
        </div>
    </asp:Panel>--%>
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
            //            $(".sp_number").spinner({min: 0,
            //                numberFormat: "d",
            //                culture:"en-GB"
            //            });
            prth_mask_input.init();
            //validatorOverrideScripts();
        });





        function GetChequeNumber() {
            var ser = $("#<%=ddlHeads.ClientID%>").find("option:selected").text(); //id name for dropdown list              
            //var cid = $("#<%=ddlHeads.ClientID%>").val(); //text name for dropdown list  
            var cid = $("#<%=ddlHeads.ClientID%>").find("option:selected").val(); //text name for dropdown list  
            var rcid = $("#<%=ddlHeads.ClientID%> option:selected").val();

            //var bnktrue = rcid.indexOf(":") == 1 ? true : false;
            //var chittrue = rcid.indexOf("|") == 1 ? true : false;

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
                url: "VoucherMultiple.aspx/getcustomername",
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
                url: "VoucherMultiple.aspx/getcustomername",
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


        function DebGetValue() {
            var dbhds = "";
            var dbhdsval = "";

            dbhds = $('#<%=ddlHeadsDebit.ClientID %>').find("option:selected").text();
            dbhdsval = $('#<%=ddlHeadsDebit.ClientID %>').find("option:selected").val();

            var headstext = $("[id*=hidDb_Headtxt]");
            headstext.val(dbhds);

            var hdval = $("[id*=hidDb_HeadVal]");
            hdval.val(dbhdsval);
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

</asp:Content>
