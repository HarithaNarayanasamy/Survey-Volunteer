<%@ Page Language="C#" MasterPageFile="~/Branch.Master" AutoEventWireup="true" CodeBehind="ChitPoolFinder.aspx.cs"
    Inherits="SreeVisalamChitFundLtd_phase1.ChitPoolFinder" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <title>Find Pool</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMainContent" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {

            gridviewScroll();
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
        function gridviewScroll() {
            $('#<%=GridView1.ClientID%>').gridviewScroll({
                width: $(window).width() - 370,
                height: $(window).height() - 380,
                freezesize: 0,
                arrowsize: 30,

                headerrowcount: 1
            });
        } 
    </script>
    <script language="javascript" type="text/javascript">
        function Confirm() {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("Do you want Delete data?")) {
                confirm_value.value = "Yes";
            } else {
                confirm_value.value = "No";
            }
            document.forms[0].appendChild(confirm_value);
        }
    </script>
    <div class="content">
        <h1 class="ribbon" style="padding-left: 3.2%; padding-top: 2px; text-align: left;">
            Find Pool
        </h1>
        <%--<asp:Label ID="Label8" runat="server" Font-Names="Liberation Sans Narrow" 
        Font-Size="16px" ForeColor="#333300" style="font-size: large" 
        Text="Edit Branch Details"></asp:Label>--%>
        <div>
            <asp:Label runat="server" ID="Label9" Text="SearchText"></asp:Label>
            <asp:TextBox ID="txtSearch" runat="server" CssClass="twitterStyleTextbox"></asp:TextBox>
            <asp:Label runat="server" ForeColor="Red" ID="lblNoRecords" Text="No Records found"></asp:Label>
            <br />
            <br />
            <div>
                <asp:GridView Style="margin: 0 auto" ID="GridView1" runat="server" AutoGenerateColumns="true"
                    GridLines="None" ShowFooter="True" Width="100%">
                    <FooterStyle VerticalAlign="Middle" HorizontalAlign="Center" CssClass="GridviewScrollC2Header" />
                    <HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center" CssClass="GridviewScrollC2Header" />
                    <RowStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="GridviewScrollC2Item" />
                    <PagerStyle CssClass="GridviewScrollC2Pager" />
                    <%--<RowStyle CssClass="GridviewScrollC2Item" /> 
            <PagerStyle CssClass="GridviewScrollC2Pager" /> 
            <HeaderStyle BackColor="#6B696B" Font-Names="DejaVu Sans Condensed"  CssClass="GridviewScrollC2Header"
                ForeColor="White" Height="50px" HorizontalAlign="Center" VerticalAlign="Middle" 
                Width="40px" Wrap="False" />
            <AlternatingRowStyle BackColor="White" />--%>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>
