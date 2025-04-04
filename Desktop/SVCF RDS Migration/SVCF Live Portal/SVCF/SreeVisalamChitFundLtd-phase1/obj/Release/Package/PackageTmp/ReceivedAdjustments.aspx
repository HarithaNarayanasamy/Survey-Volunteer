<%@ Page Language="C#" Culture="en-GB" MasterPageFile="~/Branch.Master" AutoEventWireup="true"
    CodeBehind="ReceivedAdjustments.aspx.cs" Inherits="SreeVisalamChitFundLtd_phase1.ReceivedAdjustments" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <link href="Styles/aspxtable.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        a.chzn-single span
        {
            float: right;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMainContent" runat="server">
    <ajax:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </ajax:ToolkitScriptManager>
    <asp:Button ID="btnShowPopup" runat="server" Style="display: none" />
    <ajax:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="btnShowPopup"
        PopupControlID="PnlApprove" BackgroundCssClass="modalBackground">
    </ajax:ModalPopupExtender>
    <script type="text/javascript">
        $(document).ready(function () {
            gridviewSearch();
        });

        function gridviewSearch() {
            $('#<%=lblNoRecords.ClientID%>').css('display', 'none');
            $('#<%=txtSearch.ClientID%>').keyup(function () {
                $('#<%=lblNoRecords.ClientID%>').css('display', 'none'); // Hide No records to display label.
                $("#<%=GridView1.ClientID%> tr:has(td)").hide(); // Hide all the rows.

                var iCounter = 0;
                var sSearchTerm = $('#<%=txtSearch.ClientID%>').val(); //Get the search box value

                if (sSearchTerm.length == 0) //if nothing is entered then show all the rows.
                {
                    $("#<%=GridView1.ClientID%> tr:has(td)").show();
                    return false;
                }
                //Iterate through all the td.
                $("#<%=GridView1.ClientID%> tr:has(td)").children().each(function () {
                    var cellText = $(this).text().toLowerCase();
                    if (cellText.indexOf(sSearchTerm.toLowerCase()) >= 0) //Check if data matches
                    {
                        $(this).parent().show();
                        iCounter++;
                        return true;
                    }
                });
                if (iCounter == 0) {
                    $('#<%=lblNoRecords.ClientID%>').css('display', '');
                }
                e.preventDefault();
            })
        } 
    </script>
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
                        Received Advises</p>
                </div>
                <div class="box_c_content">
                    <div class="row">
                        <table class="aligned" style="margin: 0px auto;">
                            <tr>
                                <td style="padding-right: 5px;">
                                    <asp:Label ID="Label3" runat="server" Text="Select : "></asp:Label>
                                </td>
                                <td style="padding-right: 5px; vertical-align: bottom;">
                                    <asp:DropDownList Width="200" ID="ddlStatus" runat="server" CssClass="chzn-select">
                                        <asp:ListItem Text="Waiting" Value="Waiting"></asp:ListItem>
                                        <asp:ListItem Text="Accepted" Value="Accepted"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td style="padding-right: 5px;">
                                    <asp:Button OnClick="btnLoad_Click" CssClass="GreenyPushButton" ID="btnLoad" runat="server"
                                        Text="Load"></asp:Button>
                                </td>
                                <td style="padding-right: 5px;">
                                    <asp:Label ID="Label1" Text="Search Text :" runat="server"> </asp:Label>
                                </td>
                                <td style="padding-right: 5px;">
                                    <asp:TextBox ID="txtSearch" runat="server" CssClass="twitterStyleTextbox "></asp:TextBox>
                                </td>
                                <td style="padding-right: 5px;">
                                    <asp:Label ID="lblNoRecords" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="twelve columns centered">
                        <div style="margin: 0px auto; width: 100%; overflow: auto !important;">
                            <br />
                            <asp:GridView CssClass="aspxtable" ID="GridView1" runat="server" AutoGenerateColumns="false"
                                GridLines="None" DataKeyNames="DualTransactionKey,TransactionKey,ChoosenDate,CollectedBranchID,Description,Amount,BranchName,Voucher_Type"
                                Width="100%" Style="margin: 0px auto; display: table;">
                                <Columns>
                                    <asp:TemplateField HeaderText="Approve">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnReject" runat="server" CausesValidation="false" OnClick="Approve_Click"
                                                ImageUrl="~/Images/like.jpg" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="BranchName" HeaderText="Branch Name" />
                                    <asp:BoundField DataField="ChoosenDate" HeaderText="Choosen Date" />
                                    <asp:BoundField DataField="Type" HeaderText="Type" Visible="false" />
                                    <asp:BoundField DataField="Amount" HeaderText="Amount" />
                                    <asp:BoundField DataField="Description" HeaderText="Description" />
                                    <asp:BoundField DataField="Voucher_Type" HeaderText="Voucher Type" />
                                </Columns>
                                <FooterStyle VerticalAlign="Middle" HorizontalAlign="Center" CssClass="GridviewScrollC2Header" />
                                <HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center" CssClass="GridviewScrollC2Header" />
                                <RowStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="GridviewScrollC2Item" />
                                <PagerStyle CssClass="GridviewScrollC2Pager" />
                            </asp:GridView>
                            <br />
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <asp:Panel CssClass="raised" ID="PnlApprove" runat="server" Visible="false" Style="max-height: 500px;
            min-height: 180px; min-width: 300px; max-width: 500px">
            <asp:Label runat="server" ID="lblHint" Text="" Visible="false"> </asp:Label>
            <div style="top: 0px; height: 50px; text-align: center; padding: auto 0; width: 100%"
                class="boxfooter">
                <asp:Label ID="lblT" runat="server" Text=""> </asp:Label>
            </div>
            <div id="Div1" style="max-height: 400px; min-height: 80px; text-align: center;">
                <asp:Label ID="lblContent" runat="server" Text="" Style="text-align: justify; vertical-align: middle;
                    margin-left: 10px"> </asp:Label>
                <br />
                <br />
                <asp:PlaceHolder ID="DynamicControlsHolder" runat="server"></asp:PlaceHolder>
            </div>
            <div class="boxheader" style="width: 100%; bottom: 0px; height: 50px; position: absolute">
                <asp:Button Width="100" Style="margin: 0 auto" OnClick="btnMsgOK_Click" CssClass="GreenyPushButton"
                    ID="btnMsgOK" runat="server" Text="OK" />
                <asp:Button Width="100" Style="margin: 0 auto" OnClick="btnMsgCancel_Click" CssClass="GreenyPushButton"
                    ID="btnMsgCancel" runat="server" Text="Cancel" />
            </div>
        </asp:Panel>
        <asp:Panel CssClass="raised" ID="PnlProvide" runat="server" Visible="false" Width="620px"
            Style="max-height: 500px; min-height: 500px">
            <div style="top: 0px; height: 50px; text-align: center; padding: auto 0; width: 100%"
                class="boxfooter">
                <asp:Label ID="Label2" runat="server" Text="Accept"> </asp:Label>
            </div>
            <div style="max-height: 400px; min-height: 80px; text-align: center;">
                <div class="twelve columns">
                    <div class="box_c">
                        <div class="box_c_content">
                            <div class="row" style="width: 100%;">
                                <table>
                                    <tr runat="server" id="CreditID">
                                        <td style="vertical-align: top; padding-top: 6px; padding-right: 6px; text-align: left;
                                            width: 40%">
                                            Credit
                                        </td>
                                        <td style="text-align: left;">
                                            <asp:Label ID="lblCredit" runat="server"> </asp:Label>
                                            <asp:Label Visible="false" ID="lblCreditID" runat="server"> </asp:Label>
                                            <asp:Label Visible="false" ID="lblAmount" runat="server"> </asp:Label>
                                            <asp:Label Visible="false" ID="lblDual" runat="server"> </asp:Label>
                                        </td>
                                    </tr>
                                </table>
                                <asp:UpdatePanel runat="server" UpdateMode="Always">
                                    <ContentTemplate>
                                        <asp:GridView ID="GridGuardians" runat="server" OnRowDataBound="GridGuardians_RowDataBound"
                                            AutoGenerateColumns="False" BorderStyle="None" CellSpacing="3" Font-Names="Verdana"
                                            Font-Size="9pt" ForeColor="#333333" GridLines="None" ShowHeader="false" ShowFooter="true">
                                            <Columns>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                     <table>
                                                         <tr>                                                      
                                                                                                            
                                                        <td>
                                                            Debit
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblDebit1" runat="server" Text='<%# Eval("Heads") %>' Visible="false"></asp:Label>
                                                           
                                                            <asp:DropDownList CssClass="chzn-select" runat="server" Style="width: 300px;" ID="ddlDebit"
                                                                ValidationGroup="Rej" AutoPostBack="true">
                                             
                                                            </asp:DropDownList>
                                                            <asp:CompareValidator ID="CompareValidator2" ValidationGroup="Rej" ControlToValidate="ddlDebit"
                                                                ValueToCompare="--Select--" Operator="NotEqual" Display="Dynamic" SetFocusOnError="true"
                                                                runat="server" ErrorMessage="*"></asp:CompareValidator>
                                                        </td>
                                                               </tr>
                                                         <tr>
                                                        <td>
                                                            <asp:Label ID="Label4" runat="server" Text="Amount"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox autocomplete="off" TabIndex="9" runat="server" CausesValidation="false"
                                                                Text='<%#Eval("Amount") %>' CssClass="twitterStyleTextbox sp_currency" ValidationGroup="Rej"
                                                                ID="txtAmountDebit"></asp:TextBox>
                                                        </td>
                                                             </tr>
                                                         <tr>
                                                        <td>
                                                            <asp:ImageButton TabIndex="-1" ID="imgBtnAdd" runat="server" CausesValidation="True"
                                                                Height="24" ValidationGroup="Rej" ImageUrl="~/Styles/Image/Images/round_plus_16.png"
                                                                ToolTip="Add New Transaction" Width="24" OnClick="btnAdd_GridGuardians_RowCommand_click" />
                                                        </td>
                                                        <td>
                                                            <asp:ImageButton OnClick="btnRemove_GridGuardians_RowCommand_click" TabIndex="-1"
                                                                OnClientClick="clearValidationErrors();" ID="imgBtnRemove" runat="server" CausesValidation="false"
                                                                Height="24" ImageUrl="~/Styles/Image/Images/round_minus_16.png" ToolTip="Remove New Transaction"
                                                                Visible="true" Width="24" />
                                                        </td>
                                                             </tr>
                                                          </table>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                        <asp:GridView ID="GridGuardiansDebit" runat="server" AutoGenerateColumns="False"
                                            BorderStyle="None" BorderWidth="1" CellPadding="4" Font-Names="Verdana" Font-Size="9pt"
                                            ForeColor="#333333" GridLines="None" OnRowDataBound="GridGuardiansDebit_RowDataBound"
                                            ShowHeader="false" ShowFooter="true">
                                            <Columns>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <table>
                                                        <tr>
                                                            <td>
                                                              <asp:Label ID="Label6" runat="server" Text="Credit"></asp:Label>  
                                                            </td>      
                                                            <td>                                                    
                                                            <asp:Label ID="lblCredit1" runat="server" Text='<%# Eval("Heads") %>' Visible="false"></asp:Label>
                                                         
                                                                <asp:DropDownList CssClass="chzn-select" runat="server" Style="width: 250px;" ID="ddlCredit">
                                                                </asp:DropDownList>
                                                                <asp:CompareValidator ID="CompareValidator1" ValidationGroup="Rej" ControlToValidate="ddlCredit"
                                                                    ValueToCompare="--Select--" Operator="NotEqual" Display="Dynamic" SetFocusOnError="true"
                                                                    runat="server" ErrorMessage="*"></asp:CompareValidator>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label5" runat="server" Text="Amount"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtAmount" runat="server" Width="100px" Text='<%#Eval("Amount") %>' CssClass="twitterStyleTextbox sp_currency"
                                                                    ValidationGroup="Rej"></asp:TextBox>
                                                                <td></td>
                                                            </td>
                                                            <td>
                                                                <asp:ImageButton OnClick="btnAdd_GridGuardiansDebit_RowCommand_click" TabIndex="-1"
                                                                    ID="imgBtnAddDebit" runat="server" CausesValidation="True" Height="24" ValidationGroup="Rej"
                                                                    ImageUrl="~/Styles/Image/Images/round_plus_16.png" ToolTip="Add New Transaction"
                                                                    Width="24" />
                                                            </td>
                                                            <td>
                                                                <asp:ImageButton OnClick="btnRemove_GridGuardiansDebit_RowCommand_click" TabIndex="-1"
                                                                    OnClientClick="clearValidationErrors();" ID="imgBtnRemoveDebit" runat="server"
                                                                    CausesValidation="false" Height="24" ImageUrl="~/Styles/Image/Images/round_minus_16.png"
                                                                    ToolTip="Remove New Transaction" Width="24" />
                                                            </td>
                                                        </tr>
                                                            </table>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                <table>
                                    <tr runat="server" id="DebitHead">
                                        <td style="text-align: left;">
                                            Debit
                                        </td>
                                        <td style="text-align: left;">
                                            <asp:Label ID="lblDebit" runat="server"> </asp:Label>
                                            <asp:Label Visible="false" ID="lblDebitID" runat="server"> </asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: left;">Amount </td>  
                                        <td style="text-align: left;">                                             
                                        <asp:Label ID="lbldebitamount" runat="server"> </asp:Label>      
                                            <asp:Label ID="lbldbamount" runat="server" Visible="false"></asp:Label>
                                            </td>                                                        
                                                 
                                    </tr>
                                    <tr>
                                        <td style="text-align: left;">
                                            Date
                                        </td>
                                        <td style="text-align: left;">
                                            <asp:TextBox Width="100" TabIndex="1" ID="txtDate" CssClass="input-text maskdate"
                                                runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ForeColor="Red" ID="RequiredFieldValidator6" ErrorMessage="Rquired!!!"
                                                ControlToValidate="txtDate" ValidationGroup="Rej" Display="Dynamic" runat="server"></asp:RequiredFieldValidator>
                                            <asp:CompareValidator ForeColor="Red" Type="Date" ID="CompareValidator11" ValidationGroup="Rej"
                                                ControlToValidate="txtDate" Display="Dynamic" Operator="DataTypeCheck" runat="server"
                                                ErrorMessage="Incorrect!!!"></asp:CompareValidator>
                                            <asp:RangeValidator ID="rvDate" EnableClientScript="false" runat="server" Type="Date"
                                                ControlToValidate="txtDate" ValidationGroup="Rej" ForeColor="Red" Display="Dynamic"
                                                ErrorMessage="*"></asp:RangeValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: left;">
                                            Description
                                        </td>
                                        <td style="text-align: left;">
                                            <asp:TextBox TextMode="MultiLine" ID="txtDesc" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" SetFocusOnError="true"
                                                ControlToValidate="txtDesc" ErrorMessage="*" ValidationGroup="Rej"> </asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                   <tr ><td >     <asp:Label ID="lblerror" runat="server" Text=""></asp:Label> </td>
                              
                                   </tr>
                                  
                                    
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div> </div>    
            <div class="boxheader" style="width: 100%; bottom: 0px; height: 50px; position: absolute">
                <asp:Button Width="100" Style="margin: 0 auto" CausesValidation="true" ValidationGroup="Rej"
                    OnClick="btnAcceptOK_Click" CssClass="GreenyPushButton" ID="btnAcceptOK" runat="server"
                    Text="OK" />
                <asp:Button Width="100" CausesValidation="false" Style="margin: 0 auto" OnClick="btnAcceptCancel_Click"
                    CssClass="GreenyPushButton" ID="btnAcceptCancel" runat="server" Text="Cancel" />
            </div>
        </asp:Panel>
    </div>
    <script type="text/javascript">
        $(document).ready(function () {
            $(".chzn-select").chosen({ search_contains: true });
            prth_mask_input.init();
        });
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $(".chzn-select").chosen({ search_contains: true });
            prth_mask_input.init();
        });
    </script>
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
            prth_mask_input.init();
        });
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $(".chzn-select").chosen({ search_contains: true });
            $(".sp_currency").numeric({ negative: false });
            $(".sp_number").numeric({ decimal: false, negative: false }, function () { alert("Positive integers only"); this.value = ""; this.focus(); });
            prth_mask_input.init();

        });
    </script>
</asp:Content>
