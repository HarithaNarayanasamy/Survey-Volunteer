<%@ Page Language="C#" Culture="en-GB" MasterPageFile="~/Branch.Master" AutoEventWireup="true"
    CodeBehind="SentAdvices.aspx.cs" Inherits="SreeVisalamChitFundLtd_phase1.SentAdvices"
    Title="SVCF - Admin Panel" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Head1" ContentPlaceHolderID="cphHead" runat="server">
    <link href="Styles/aspxtable.css" rel="stylesheet" type="text/css" />
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
                        Sent Advises</p>
                </div>
                <div class="box_c_content">
                    <div class="row">
                        <table class="aligned" style="margin: 0px auto;">
                            <tr>
                                <td style="padding-right: 5px;">
                                    <asp:Label ID="Label3" runat="server" Text="Select : "></asp:Label>
                                </td>
                                <td style="padding-right: 5px;">
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
                                    <asp:Label ID="Label2" Text="Search Text :" runat="server"> </asp:Label>
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
                            <asp:GridView ID="GridView1" CssClass="aspxtable" runat="server" AutoGenerateColumns="false"
                                GridLines="None" Width="100%" Style="margin: 0px auto; display: table;">
                                <Columns>
                                    <asp:TemplateField HeaderText="Approve" Visible="false">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnReject" runat="server" CausesValidation="false" OnClick="Approve_Click"
                                                ImageUrl="~/Images/like.jpg" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Reject" Visible="false">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnRejectorig" runat="server" CausesValidation="false" OnClick="dis_Approve_Click"
                                                ImageUrl="~/Images/unlike.jpg" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="BranchName" HeaderText="Branch" />
                                    <asp:BoundField DataField="ChoosenDate" HeaderText="Date" />
                                    <asp:BoundField DataField="GROUPNO" HeaderText="Chit" />
                                    <asp:BoundField DataField="GrpMemberID" HeaderText="Token" />
                                    <asp:BoundField DataField="CustomerName" HeaderText="Customer Name" />
                                    <asp:BoundField DataField="Series" HeaderText="Series" />
                                    <asp:BoundField DataField="ReceiptNumber" HeaderText="Receipt Number" />
                                    <asp:BoundField DataField="Amount" HeaderText="Amount" />
                                    <asp:BoundField DataField="Description" HeaderText="Description" />
                                </Columns>
                                <FooterStyle VerticalAlign="Middle" HorizontalAlign="Center" CssClass="GridviewScrollC2Header" />
                                <HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center" CssClass="GridviewScrollC2Header" />
                                <RowStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="GridviewScrollC2Item" />
                                <PagerStyle CssClass="GridviewScrollC2Pager" />
                            </asp:GridView>
                            <br />
                            <br />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        $(document).ready(function () {
            $(".chzn-select").chosen({ search_contains: true });
        });
    </script>
</asp:Content>
