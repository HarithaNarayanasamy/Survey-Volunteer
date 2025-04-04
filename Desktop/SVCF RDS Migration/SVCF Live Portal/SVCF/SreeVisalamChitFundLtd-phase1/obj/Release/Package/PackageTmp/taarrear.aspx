<%@ Page Title="" Language="C#" MasterPageFile="~/Branch.Master" AutoEventWireup="true" CodeBehind="taarrear.aspx.cs" Inherits="SreeVisalamChitFundLtd_phase1.taarrear" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMainContent" runat="server">
    <div>
        <p>       
          <asp:Label ID="Label1" runat="server" Text="Select Chit Group"></asp:Label>
          <asp:DropDownList ID="ddlChit" runat="server"></asp:DropDownList>      
        </p>
        <p>                    
            <asp:Label ID="Label2" runat="server" Text="Date : "></asp:Label>
            <asp:TextBox TabIndex="1" Width="100px" ID="txtFromDate" runat="server" placeholder="From Date">
            </asp:TextBox>
        </p>
        <p>        
            <asp:Button ID="btnload" runat="server" Text="Load" />      
        </p>
       <table id="tbltarrear" border="0" cellpadding="0" cellspacing="0">
            <thead>
            <tr>
                <th>
                    ChitNo1
                </th>
                <th>
                    MemberName
                </th>
                <th>
                    Credit
                </th>
                <th>
                    Debit
                </th>
                 <th>
                    ExcessRemittance
                </th>
                 <th>
                    NPArrier
                </th>
                 <th>
                    PArrier
                </th>
                 <th>
                    NPKasar
                </th>
                 <th>
                    PKasar
                </th>
                <th>
                    Branches
                </th>    
                <th>
                    Select
                </th>      
                 <th>
                </th>
                <th>
                </th>           
            </tr>
        </thead>
        <tbody>
            <tr>
                <td class="ChitNo1">
                    <span></span>                    
                </td>
                <td class="MemberName">
                    <span></span>                    
                </td>
                <td class="Credit">
                    <span></span>                    
                </td>
                <td class="Debit">
                    <span></span>                    
                </td>
                <td class="ExcessRemittance">
                    <span></span>                    
                </td>
                <td class="NPArrier">
                    <span></span>                    
                </td>
                <td class="PArrier">
                    <span></span>                    
                </td>
                <td class="NPKasar">
                    <span></span>                    
                </td>
                <td class="PKasar">
                    <span></span>                    
                </td> 
                <td class="Branches">
                    <span></span>                    
                </td>   
                 <td class="Select">
                    <span></span>                    
                </td>     
                <td>                            
                    <input id="chkselect" type="checkbox"/>                                  
                </td>                        
            </tr>
        </tbody>       
    </table>   
    </div>
     <script src="Scripts/jquery-1.9.0.min.js" type="text/javascript"></script>
    <script type="text/javascript">      
       
       <%-- $(document).ready(function () {
            $("[id*=btnload]").click(function () {
                var fdt = $("#<%=txtFromDate.ClientID%>").val();
                var cid = $("#<%=ddlChit.ClientID%>").find("option:selected").val();
                $("#UpdatePanel").html("Please wait...");
                $.ajax({
                    url: "taarrear.aspx/VCList_Date",
                    type: "GET",
                    data: '{frmdt: "' + fdt + '", gpid: "' + cid + '" }',
                    dataType: "xml",
                    success: OnSuccess,
                    error: OnError
                });
            });
        });

      $(document).ready(function () {
            $("[id*=btnload]").click(function () {
                var fdt = $("#<%=txtFromDate.ClientID%>").val();
                var cid = $("#<%=ddlChit.ClientID%>").find("option:selected").val();
                $("#UpdatePanel").html("Please wait...");
                $.ajax({
                    url: "taarrear.aspx/VCList_Date",
                    type: "GET",
                    data: '{frmdt: "' + fdt + '", gpid: "' + cid + '" }',
                    dataType: "xml",
                    success: OnSuccess,
                    error: OnError
                });
            });
        });

        function OnSuccess(xml) {
            var tableContent = "<table border='1' cellspacing='0' cellpadding='5'>" +
                                "<tr>" +
                                    "<th>ChitNo1</th>" +
                                    "<th>MemberName</th>" +
                                    "<th>Credit</th>" +
                                    "<th>Debit</th>" +
                                    "<th>ExcessRemittance</th>" +
                                    "<th>NPArrier</th>" +
                                    "<th>PArrier</th>" +
                                    "<th>NPKasar</th>" +
                                    "<th>PKasar</th>" +
                                    "<th>Branches</th>"+
                                "</tr>";
            $(xml).find('CD').each(function () {
                tableContent += "<tr>" +
                                    "<td>" + $(this).find('ChitNo1').text() + "</td>" +
                                    "<td>" + $(this).find('MemberName').text() + "</td>" +
                                    "<td>" + $(this).find('Credit').text() + "</td>" +
                                    "<td>" + $(this).find('Debit').text() + "</td>" +
                                    "<td>" + $(this).find('ExcessRemittance').text() + "</td>" +
                                    "<td>" + $(this).find('NPArrier').text() + "</td>" +
                                    "<td>" + $(this).find('PArrier').text() + "</td>" +
                                    "<td>" + $(this).find('NPKasar').text() + "</td>" +
                                    "<td>" + $(this).find('PKasar').text() + "</td>" +
                                    "<td>" + $(this).find('Branches').text() + "</td>" +
                                "</tr>";
            });
            tableContent += "</table>";
            $("#UpdatePanel").html(tableContent);
        }

        function OnError(data) {
            $("#UpdatePanel").html("Error! Please try again.");
        }
        --%>

        $(function () {
            $("[id*=btnload]").click(function () {
                event.preventDefault();
                var fdt = $("#<%=txtFromDate.ClientID%>").val();
                var cid = $("#<%=ddlChit.ClientID%>").find("option:selected").val();

                $.ajax({
                    type: "POST",
                    url: "taarrear.aspx/VCList_Date",
                    data: '{frmdt: "' + fdt + '", gpid: "' + cid + '" }',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data) {
                        var xmlDoc = $.parseXML(data.d);
                        var xml = $(xmlDoc);
                        var vcr = xml.find("Table");
                        var row = $("[id*=tbltarrear] > tbody tr:last-child").clone(true);
                        $("[id*=tbltarrear] tr").not(':has(th)').remove();
                        $.each(vcr, function () {
                            var voucher = $(this);
                            AppendRow(row, $(this).find("ChitNo1").text(), $(this).find("MemberName").text(), $(this).find("Credit").text(),
                                           $(this).find("Debit").text(), $(this).find("ExcessRemittance").text(),
                                           $(this).find("NPArrier").text(), $(this).find("PArrier").text(),
                                           $(this).find("NPKasar").text(), $(this).find("PKasar").text(), $(this).find("Branches").text())
                            row = $("[id*= tbltarrear] > tbody tr:last-child").clone(true);
                        });
                    },
                    error: function (xhr, ajaxOptions, thrownError) {
                        alert("Error: " + xhr.responseText);
                    }
                });
            });
            return false;
        });

    


        function AppendRow(row, ChitNo1, MemberName, Credit, Debit, ExcessRemittance, NPArrier, PArrier, NPKasar, PKasar, Branches) {
            //Bind CustomerId.
            $(".ChitNo1", row).find("span").html(ChitNo1);
            //Bind Name.
            $(".MemberName", row).find("span").html(MemberName);
            //Bind Country.
            $(".Credit", row).find("span").html(Credit);

            $(".Debit", row).find("span").html(Debit);
            //Bind Name.
            $(".ExcessRemittance", row).find("span").html(ExcessRemittance);
            //Bind Country.
            $(".NPArrier", row).find("span").html(NPArrier);

            $(".PArrier", row).find("span").html(PArrier);
            //Bind Name.
            $(".NPKasar", row).find("span").html(NPKasar);
            //Bind Country.
            $(".PKasar", row).find("span").html(PKasar);

            $(".Branches", row).find("span").html(Branches);

            $("[id*=tbltarrear]").append(row);
        }
    </script>
</asp:Content>
