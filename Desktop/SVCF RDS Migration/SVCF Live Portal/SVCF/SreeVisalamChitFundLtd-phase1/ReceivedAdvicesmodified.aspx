<%@ Page Language="C#" Culture="en-GB" MasterPageFile="~/Branch.Master" AutoEventWireup="true" CodeBehind="ReceivedAdvicesmodified.aspx.cs"
    Inherits="SreeVisalamChitFundLtd_phase1.ReceivedAdvices" Title="SVCF - Admin Panel" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Head1" ContentPlaceHolderID="cphHead" runat="server">
    <link href="Styles/aspxtable.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .auto-style1 {
            width: 262px;
        }
    </style>
      <link rel="stylesheet" href="css/metro-bootstrap.css">
        <script src="js/jquery/jquery.min.js"></script>
        <script src="js/jquery/jquery.widget.min.js"></script>
        <script src="js/metro/metro.min.js"></script>
    <script src="Scripts/metroui/dropdown.js"></script>
    <link href="Styles/gridadvice.css" rel="stylesheet" />

</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMainContent" runat="server">
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
                                <td>Load Waiting Advices</td>
                                <td style="padding-right:5px;">
                                    <asp:Button OnClick="btnLoad_Click" CssClass="GreenyPushButton" ID="btnLoad" runat="server"
                                        Text="Load Waiting Advices"></asp:Button>
                                </td>
                                <td></td>
                                <td style="padding-right:5px;">
                                    <asp:Label ID="Label1" Text="Search in Accepted Text :" runat="server"> </asp:Label>
                                </td>
                                <td style="padding-right:5px;">
                                    <asp:TextBox ID="txtSearch" runat="server" CssClass="twitterStyleTextbox "></asp:TextBox>
                                </td>
                                <td style="padding-right:5px;">
                                    <asp:Label ID="lblNoRecords" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>Select Accepted adivces for Month</label>                                    
                                </td>
                                <td>
                                     <asp:DropDownList Width="200" ID="DrpdwnMonth" runat="server" CssClass="chzn-select">
                                        <asp:ListItem Text="January" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="February" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="March" Value="3"></asp:ListItem>
                                        <asp:ListItem Text="April" Value="4"></asp:ListItem>
                                        <asp:ListItem Text="May" Value="5"></asp:ListItem>
                                        <asp:ListItem Text="June" Value="6"></asp:ListItem>
                                        <asp:ListItem Text="July" Value="7"></asp:ListItem>
                                        <asp:ListItem Text="August" Value="8"></asp:ListItem>
                                        <asp:ListItem Text="September" Value="9"></asp:ListItem>
                                        <asp:ListItem Text="October" Value="10"></asp:ListItem>
                                        <asp:ListItem Text="November" Value="11"></asp:ListItem>
                                        <asp:ListItem Text="December" Value="12"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td>
                                       <asp:Button CssClass="GreenyPushButton" ID="BtnViewAcceptedAdvice" runat="server" OnClick="BtnViewAcceptedAdvice_Click"
                                        Text="View Accepting Advices"></asp:Button>
                                </td>
                                <td>
                                    Search Receipt Number :
                                </td>
                                <td>
                                    <asp:TextBox ID="txtSearchReceiptno" runat="server" Text=""></asp:TextBox>
                                </td>
                                <td>
                                     <asp:Button CssClass="GreenyPushButton" ID="btnSearchAccepted" runat="server" OnClick="btnSearchAccepted_Click"
                                        Text="View Accepting Advices"></asp:Button>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="twelve columns centered">
                        <div style="margin: 0px auto; width: 100%; overflow: auto !important;">
                        <br />
                            <asp:GridView CssClass="aspxtable" ID="GridView1" runat="server" AutoGenerateColumns="false"
                                GridLines="None" DataKeyNames="DualTransactionKey,ChoosenDate,ReceiptNumber,Series,MemberID,ChitGroupID,CollectedBranchID,Token,Description,Amount,TransactionKey,BranchName"
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
                                    <asp:BoundField DataField="GROUPNO" HeaderText="Group" />
                                    <asp:BoundField DataField="GrpMemberID" HeaderText="Token" />
                                    <asp:BoundField DataField="CustomerName" HeaderText="Customer Name" />
                                    <asp:BoundField DataField="Series" HeaderText="Series" />
                                    <asp:BoundField DataField="ReceiptNumber" HeaderText="Receipt Number" />
                                    <asp:BoundField DataField="Amount" HeaderText="Amount" />
                                    <asp:BoundField DataField="Description" HeaderText="Description" />
                                    <asp:BoundField DataField="AppReceiptno" HeaderText="AppReceiptno" />
                                    
                                </Columns>
                                <FooterStyle VerticalAlign="Middle" HorizontalAlign="Center" CssClass="GridviewScrollC2Header" />
                                <HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center" CssClass="GridviewScrollC2Header" />
                                <RowStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="GridviewScrollC2Item" />
                                <PagerStyle CssClass="GridviewScrollC2Pager" />
                            </asp:GridView>
                            <br />
                        </div>

                        <div style="margin: 0px auto; width: 100%; overflow: auto !important;">
                         <br />
                            <asp:GridView ID="GridAccepted" runat="server" AutoGenerateColumns="false" OnRowDataBound="GridAccepted_RowDataBound"
                                GridLines="None" AllowPaging="false" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt">
                                <Columns>
                                    <asp:BoundField HeaderText="Branch" DataField="BranchName" />
                                    <asp:BoundField HeaderText="Date" DataField="ChoosenDate" />
                                    <asp:BoundField HeaderText="Group Id" DataField="GroupNo" />                                    
                                    <asp:BoundField HeaderText="Receipt Number" DataField="ReceiptNumber" />                                 
                                    <asp:BoundField HeaderText="Amount" DataField="Amount" />
                                    <asp:BoundField HeaderText="Description" DataField="Description" />
                                    <asp:BoundField HeaderText="Token" DataField="Token" />
                                </Columns>
                            </asp:GridView>
                        </div>

                    </div>
                </div>
            </div>
        </div>
        <asp:Panel CssClass="raised" ID="PnlApprove" runat="server" Visible="false"
            Style="max-height: 500px; min-height:180px;min-width:300px;max-width:500px">
            <asp:Label runat="server" ID="lblHint" Text="" Visible="false"> </asp:Label>
            <div style="top: 0px; height: 50px; text-align: center; padding: auto 0; width: 100%"
                class="boxfooter">
                <asp:Label ID="lblT" runat="server" Text=""> </asp:Label>
            </div>
            <div id="Div1" style="max-height: 400px; min-height: 80px; text-align:center;">
                <br />
                <br />
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
        <asp:Panel CssClass="raised" ID="PnlProvide" runat="server" Visible="false" Width="700px"
            Style="max-height: 500px; min-height: 300px">
            <div style="top: 0px; height: 50px; text-align: center; padding: auto 0; width: 100%"
                class="boxfooter">
                <asp:Label ID="Label4" runat="server" Text="Accept"> </asp:Label>
            </div>
            <div style="max-height: 400px; min-height: 80px; text-align: center;">
                <div class="twelve columns">
                    <div class="box_c">
                        <div class="box_c_content">
                            <div class="row" style="width: 650px;">
                                <table>
                                    <tr>
                                        <td style="vertical-align: top; padding-top: 6px; padding-right: 6px;text-align:left;width:40%">
                                            Credit
                                        </td>

                                        <td style="text-align:left;" class="auto-style1">
                                            <asp:TextBox Width="100" TabIndex="1" ID="txtCredit" 
                                                CssClass="input-text " runat="server"></asp:TextBox>
                                            
                                        </td>
                                        <td>
                                             <asp:DropDownList TabIndex="2" Width="300px"  ID="ddlMisc" 
                                                    CssClass="chzn-select" runat="server">
                                                </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr  runat="server" id="DebitHead">
                                        <td style="vertical-align: top; padding-top: 6px; padding-right: 6px;text-align:left;width:40%">
                                            Debit
                                        </td>
                                        <td style="text-align:left;" class="auto-style1">
                                            <asp:Label ID="lblDebit" runat="server"> </asp:Label>
                                            <asp:Label Visible="false" ID="lblDebitID" runat="server"> </asp:Label>
                                            <asp:Label Visible="false" ID="lblAmount" runat="server"> </asp:Label>
                                            <asp:Label Visible="false" ID="lblDual" runat="server"> </asp:Label>
                                            <asp:Label Visible="false" ID="lblSeries" runat="server"> </asp:Label>
                                            <asp:Label Visible="false" ID="lblVoucher" runat="server"> </asp:Label>
                                            <asp:Label Visible="false" ID="lblGroupID" runat="server"> </asp:Label>
                                            <asp:Label Visible="false" ID="lblGrpMemID" runat="server"> </asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="vertical-align: top; padding-top: 6px; padding-right: 6px;text-align:left;width:40%">
                                            Date
                                        </td>
                                        <td class="auto-style1">
                                            <asp:TextBox Width="100" TabIndex="1" ID="txtDate" 
                                                CssClass="input-text maskdate" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ForeColor="Red"  ID="RequiredFieldValidator6" ErrorMessage="Rquired!!!"
                                                ControlToValidate="txtDate" ValidationGroup="Rej" Display="Dynamic" runat="server"></asp:RequiredFieldValidator>
                                            <asp:CompareValidator ForeColor="Red" Type="Date" ID="CompareValidator11" ValidationGroup="Rej" 
                                                ControlToValidate="txtDate" Display="Dynamic" Operator="DataTypeCheck" runat="server"
                                                ErrorMessage="Incorrect!!!"></asp:CompareValidator>
                                            <asp:RangeValidator ID="rvDate"  runat="server" 
                                                Type="Date" ControlToValidate="txtDate" ValidationGroup="Rej" ForeColor="Red" Display="Dynamic" 
                                                ErrorMessage="*"></asp:RangeValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="vertical-align: top; padding-top: 6px; padding-right: 6px;text-align:left;width:40%">
                                            Description
                                        </td>
                                        <td style="text-align:left;" class="auto-style1">
                                            <asp:TextBox TextMode="MultiLine" ID="txtDesc" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" SetFocusOnError="true"
                                                ControlToValidate="txtDesc" ErrorMessage="*" ValidationGroup="Rej"> </asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="boxheader" style="width: 100%; bottom: 0px; height: 50px; position: absolute">
                <asp:Button Width="100" Style="margin: 0 auto" CausesValidation="true" ValidationGroup="Rej"
                    OnClick="btnAcceptOK_Click" CssClass="GreenyPushButton" ID="btnAcceptOK" runat="server"
                    Text="OK" />
                <asp:Button Width="100" CausesValidation="false" Style="margin: 0 auto" OnClick="btnAcceptCancel_Click" CssClass="GreenyPushButton"
                    ID="btnAcceptCancel" runat="server" Text="Cancel" />
            </div>
        </asp:Panel>
        <asp:Panel CssClass="raised" ID="PnlReject" runat="server" Visible="false" Width="600px"
            Style="max-height: 500px; min-height: 300px">
            <div style="top: 0px; height: 50px; text-align: center; padding: auto 0; width: 100%"
                class="boxfooter">
                <asp:Label ID="Label2" runat="server" Text="Rejected Reason"> </asp:Label>
            </div>
            <div id="Div2" style="max-height: 400px; min-height: 200px; overflow: auto; width: 100%;">
                <br />
                <br />
                <asp:Label ID="lblReasonForRejection" runat="server" Text="Please Enter the Reason for Rejection?"
                    Style="text-align: justify; vertical-align: middle; margin-left: 10px"> </asp:Label>
                <asp:TextBox ID="txtReasonForRejection" runat="server" Height="1000"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfv1" runat="server" SetFocusOnError="true" ControlToValidate="txtReasonForRejection"
                    ErrorMessage="*" txtValidationGroup="Rej"> </asp:RequiredFieldValidator>
                <br />
                <br />
            </div>
            <div class="boxheader" style="width: 100%; bottom: 0px; height: 50px; position: absolute">
                <div style="width: 210px; margin: 0 auto; padding-top: 10px">
                    <asp:Button Width="100" Style="margin: 0 auto" CausesValidation="true" ValidationGroup="Rej"
                        OnClick="btnRejectOK_Click" CssClass="GreenyPushButton" ID="btnRejectOK" runat="server"
                        Text="OK" />
                    <asp:Button Width="100" Style="margin: 0 auto" OnClick="btnRejectCancel_Click" CssClass="GreenyPushButton"
                        ID="btnRejectCancel" runat="server" Text="Cancel" />
                </div>
            </div>
        </asp:Panel>
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
    </script>
</asp:Content>
