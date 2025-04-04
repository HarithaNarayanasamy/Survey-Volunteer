<%@ Page Title="" Language="C#" MasterPageFile="~/Branch.Master" AutoEventWireup="true" CodeBehind="DeleteVoucherentry.aspx.cs" Inherits="SreeVisalamChitFundLtd_phase1.DeleteVoucherentry" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMainContent" runat="server">   
    <div>
        <p>        
          <label>From Date:</label><br />
          <asp:TextBox ID="txtfrmdt" runat="server"></asp:TextBox>
        </p>
        <p>
            <label>To Date:</label><br />
            <asp:TextBox ID="txttodt" runat="server"></asp:TextBox>
        </p>
        <p>
             <asp:Button ID="btnload" runat="server" Text="Load" />            
            <%--<button id="btnload" onclick="GetVCDate()">Load</button> --%>
        </p>
        <p>
            <asp:HiddenField ID="hdn_rindex" runat="server" />
            <asp:HiddenField ID="hdnd_trkey" runat="server" />
        </p>    
        <p>
            <%--<input id="btnDelete" class="Delete" type="button" value="Delete"  onclick="DeleteVoucher();" />--%>
            <asp:Button ID="BtnDelete" runat="server" Text="Delete" OnClientClick="DeleteVoucher();" />
        </p>
        
      <table id="tblVoucher" border="0" cellpadding="0" cellspacing="0">
        <thead>
            <tr>
                <th>
                    ChoosenDate
                </th>
                <th>
                    TransactionKey
                </th>
                <th>
                    key1
                </th>
                <th>
                    Voucher_No
                </th>
                 <th>
                    Series
                </th>
                 <th>
                    Head
                </th>
                 <th>
                    Amount
                </th>
                 <th>
                    Narration
                </th>
                 <th>
                    Voucher_Type
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
                <td class="ChoosenDate">
                    <span></span>                    
                </td>
                <td class="TransactionKey">
                    <span></span>                    
                </td>
                <td class="key1">
                    <span></span>                    
                </td>
                <td class="Voucher_No">
                    <span></span>                    
                </td>
                <td class="Series">
                    <span></span>                    
                </td>
                <td class="Head">
                    <span></span>                    
                </td>
                <td class="Amount">
                    <span></span>                    
                </td>
                <td class="Narration">
                    <span></span>                    
                </td>
                <td class="Voucher_Type">
                    <span></span>                    
                </td> 
                <td class="Select">
                    <span></span>                    
                </td>             
                <td>
                    <input id="chkselect" type="checkbox" onchange="delcheck();"  />        
                    <%--<input id="chkselect" type="checkbox"/>  --%>                                  
                </td>
            </tr>
        </tbody>       
    </table>
    </div>

     <script src="Scripts/jquery-1.8.3.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        //$(document).ready(function () {
        //    $.ajax({
        //        type: "POST",
        //        url: "af1.aspx/GetVoucherDefault",
        //        data: '{}',
        //        contentType: "application/json; charset=utf-8",
        //        dataType: "json",
        //        success: OnSuccess
        //        //success: function (data) {
        //        //    alert("Success");
        //        //},
        //        //error: function (xhr, ajaxOptions, thrownError) {
        //        //    alert("Error: " + xhr.responseText);
        //        //}
        //    });
        //});



        function OnSuccess(response) {
            alert("success function");
            var xmlDoc = $.parseXML(response.d);
            var xml = $(xmlDoc);
            var vcr = xml.find("Table");
            var row = $("[id*=tblVoucher] > tbody tr:last-child").clone(true);
            $("[id*=tblVoucher] tr").not(':has(th)').remove();
            $.each(vcr, function () {
                var voucher = $(this);
                AppendRow(row, $(this).find("ChoosenDate").text(), $(this).find("TransactionKey").text(), $(this).find("key1").text(),
                               $(this).find("Voucher_No").text(), $(this).find("Series").text(),
                               $(this).find("Head").text(), $(this).find("Amount").text(),
                               $(this).find("Narration").text(), $(this).find("Voucher_Type").text())
                row = $("[id*=tblVoucher] > tbody tr:last-child").clone(true);
            });
        }

       <%--  function GetVCDate() {
             debugger;
             var fdt = $("#<%=txtfrmdt.ClientID%>").val();
             var tdt = $("#<%=txttodt.ClientID%>").val();
             $.ajax({
                 type: "POST",
                 url: "af1.aspx/VCList_Date",
                 data: '{frmdt: "' + fdt + '", todt: "' + tdt + '" }',
                 contentType: "application/json; charset=utf-8",
                 dataType: "json",
                 success: function (data) {
                     var xmlDoc = $.parseXML(data.d);
                     var xml = $(xmlDoc);
                     var vcr = xml.find("Table");
                     var row = $("[id*=tblVoucher] > tbody tr:last-child").clone(true);
                     $("[id*=tblVoucher] tr").not(':has(th)').remove();
                     $.each(vcr, function () {
                         var voucher = $(this);
                         AppendRow(row, $(this).find("ChoosenDate").text(), $(this).find("TransactionKey").text(), $(this).find("key1").text(),
                                        $(this).find("Voucher_No").text(), $(this).find("Series").text(),
                                        $(this).find("Head").text(), $(this).find("Amount").text(),
                                        $(this).find("Narration").text(), $(this).find("Voucher_Type").text())
                         row = $("[id*=tblVoucher] > tbody tr:last-child").clone(true);
                     });
                 },
                 error: function (xhr, ajaxOptions, thrownError) {
                     alert("Error: " + xhr.responseText);
                 }

             });
         }--%>
        //function GetVCDate() {
        $(function () {
            $("[id*=btnload]").click(function () {
                event.preventDefault();
                var fdt = $("#<%=txtfrmdt.ClientID%>").val();
                 var tdt = $("#<%=txttodt.ClientID%>").val();

                 $.ajax({
                     type: "POST",
                     url: "DeleteVoucherentry.aspx/VCList_Date",
                     data: '{frmdt: "' + fdt + '", todt: "' + tdt + '" }',
                     contentType: "application/json; charset=utf-8",
                     dataType: "json",
                     success: function (data) {
                         alert("Success");
                         var xmlDoc = $.parseXML(data.d);
                         var xml = $(xmlDoc);
                         var vcr = xml.find("Table");
                         var row = $("[id*=tblVoucher] > tbody tr:last-child").clone(true);
                         $("[id*=tblVoucher] tr").not(':has(th)').remove();
                         $.each(vcr, function () {
                             var voucher = $(this);
                             AppendRow(row, $(this).find("ChoosenDate").text(), $(this).find("TransactionKey").text(), $(this).find("key1").text(),
                                            $(this).find("Voucher_No").text(), $(this).find("Series").text(),
                                            $(this).find("Head").text(), $(this).find("Amount").text(),
                                            $(this).find("Narration").text(), $(this).find("Voucher_Type").text())
                             row = $("[id*=tblVoucher] > tbody tr:last-child").clone(true);
                         });
                     },
                     error: function (xhr, ajaxOptions, thrownError) {
                         alert("Error: " + xhr.responseText);
                     }
                 });
            });
            return false;
         });


         function AppendRow(row, ChoosenDate, TransactionKey, key1, Voucher_No, Series, Head, Amount, Narration, Voucher_Type) {
             //Bind CustomerId.
             $(".ChoosenDate", row).find("span").html(ChoosenDate);
             //Bind Name.
             $(".TransactionKey", row).find("span").html(TransactionKey);
             //Bind Country.
             $(".key1", row).find("span").html(key1);

             $(".Voucher_No", row).find("span").html(Voucher_No);
             //Bind Name.
             $(".Series", row).find("span").html(Series);
             //Bind Country.
             $(".Head", row).find("span").html(Head);

             $(".Amount", row).find("span").html(Amount);
             //Bind Name.
             $(".Narration", row).find("span").html(Narration);
             //Bind Country.
             $(".Voucher_Type", row).find("span").html(Voucher_Type);

             $("[id*=tblVoucher]").append(row);
         }

         ////Cancel event handler.
         //$("body").on("click", "[id*=tblVoucher] .Cancel", function () {
         //    var row = $(this).closest("tr");
         //    $("td", row).each(function () {
         //        if ($(this).find("input").length > 0) {
         //            var span = $(this).find("span");
         //            var input = $(this).find("input[type='text']");
         //            input.val(span.html());
         //            span.show();
         //            input.hide();
         //        }
         //    });          
         //    row.find(".Delete").show();             
         //    $(this).hide();
         //    return false;
         //});



        <%-- function delcheck() {
             //var id = $(this).closest("tr").find('td:eq(1)').text();
             //str = $(this).closest('tr').text();
             //alert(str);     
             $("#tblVoucher [id*=chkselect]").click(function () {
                 if (this.checked == true) {
                     $('#tblVoucher tr').click(function () {
                         var arr = "";
                         var DualTransactionKey = "";
                         // $('#chkselect').click(function () {
                         // var chk = $(this).find(".chkselect").html();
                         var rowindex = $(this).index() + 1;
                         var ChoosenDate = $(this).find(".ChoosenDate").html();
                         var tkey = $(this).find(".key1").html();
                         tkey = tkey.replace("<span>", "");
                         tkey = tkey.replace("</span>", "");
                         arr = tkey.replace("-", "");
                         arr = arr.replace("-", "");
                         arr = arr.replace("-", "");
                         arr = arr.replace("-", "");
                         arr = arr.replace(/(\r\n\t|\n|\r|\t)/gm, "").trim();
                         DualTransactionKey = "0x" + arr;
                         DualTransactionKey = DualTransactionKey.trim();
                         alert('Dualkey:' + DualTransactionKey);
                         $("#<%= hdnd_trkey.ClientID %>").val("");
                         $("#<%= hdnd_trkey.ClientID %>").val(DualTransactionKey);
                         $("#<%= hdn_rindex.ClientID %>").val("");
                         $("#<%= hdn_rindex.ClientID %>").val(rowindex);
                     });
                 }
                 this.checked = false;

             });
         }--%>

         

         <%-- function DeleteVoucher() {
             var dualkey = $("#<%= hdnd_trkey.ClientID %>").val();

             $.ajax({
                 type: "POST",
                 contentType: "application/json; charset=utf-8",
                 url: "af1.aspx/RemoveVoucher",
                 data: "{dualtkey:" + dualkey + "}",
                 dataType: "json",
                 success: function (data) {
                     alert("Voucher removed successfully");
                 },
                 error: function (result) {
                     alert("Error: " + result);
                 }
             });

             GetVCDate();
         }--%>


        function DeleteVoucher() {
            alert("inside delete voucher");
        }

        ////function delcheck() {
        ////    //var id = $(this).closest("tr").find('td:eq(1)').text();
        ////    //str = $(this).closest('tr').text();
        ////    //alert(str);     

        ////    //$('#tblVoucher tr').click(function () {
        ////    // $('#chkselect').click(function () {
        ////    // var chk = $(this).find(".chkselect").html();
        ////    $("#tblVoucher [id*=chkselect]").click(function () {
        ////        if (this.checked == true) {
        ////            var rowindex = $(this).index() + 1;
        ////            var ChoosenDate = $(this).find(".ChoosenDate").html();
        ////            var tkey = $(this).find(".key1").html();
        ////            tkey = tkey.replace("<span>", "");
        ////            tkey = tkey.replace("</span>", "");
        ////            var arr = tkey.replace("-", "");
        ////            arr = arr.replace("-", "");
        ////            arr = arr.replace("-", "");
        ////            arr = arr.replace("-", "");
        ////            var DualTransactionKey = "0x" + arr;
        ////            DualTransactionKey = DualTransactionKey.trim();
        ////            alert('Dualkey:' + DualTransactionKey);
        ////            this.checked = false;
        ////        }

        ////    });



        ////    //var table = $("[id*=tblVoucher]");
        ////    //var row = $(this).closest("tr");
        ////    //var id = $(this).closest("tr").find('td:eq(2)').text();
        ////}



     </script>
</asp:Content>
