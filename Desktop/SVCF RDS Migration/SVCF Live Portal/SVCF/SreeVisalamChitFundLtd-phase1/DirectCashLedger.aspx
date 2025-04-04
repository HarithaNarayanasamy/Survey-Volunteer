<%@ Page Culture="en-GB" Language="C#" MasterPageFile="~/Branch.Master" AutoEventWireup="true"
    CodeBehind="DirectCashLedger.aspx.cs" Inherits="SreeVisalamChitFundLtd_phase1.DirectCashLedger"
    Title="Cash Ledger" %>


<asp:Content ID="Content2" ContentPlaceHolderID="cphHead" runat="server">
    <script src="pertho_admin_v1.3/js/jquery.min.js" type="text/javascript"></script>
    <script src="pertho_admin_v1.3/js/jquery.inputmask.js" type="text/javascript"></script>
    <script src="pertho_admin_v1.3/js/jquery.inputmask.extentions.js" type="text/javascript"></script>
    <title>SVCF Admin Panel</title>
    <style type="text/css">
        td[style="cursor:default;"] {
            vertical-align: middle;
        }
    </style>
    <script type="text/javascript">
        function OnInit(s, e) {
            AdjustSize();
        }
        function OnEndCallback(s, e) {
            AdjustSize();
        }
        function OnControlsInitialized(s, e) {
            ASPxClientUtils.AttachEventToElement(window, "resize", function (evt) {
                AdjustSize();
            });
        }
        function AdjustSize() {
            var height = Math.max(0, document.documentElement.clientHeight);
            grid.SetHeight(height);
        }

        prth_mask_input = {
            init: function () {
                $(".maskdate").inputmask("99/99/9999", { placeholder: "dd/mm/yyyy" });
            }
        };
        //$(".chzn-select").chosen();

        $(document).ready(function () {
            prth_mask_input.init();
        });
        $(document).ready(function () {
            $(".chzn-select").chosen({ search_contains: true });
        });
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $(".chzn-select").chosen({ search_contains: true });
        });
    </script>
    
  <%--  <link href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-BVYiiSIFeK1dGmJRAkycuHAHRg32OmUcww7on3RYdg4Va+PmSTsz/K68vbdEjh4u" crossorigin="anonymous" />
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.4.0/css/font-awesome.min.css" rel='stylesheet' type='text/css'>
       
<script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js" integrity="sha384-Tc5IQib027qvyjSMfHjOMaLkfuWVxZxUPnCJA7l2mCWNIpG9mGCD8wGNIcPD7Txa" crossorigin="anonymous"></script>--%>
     <script src="https://code.jquery.com/jquery-2.2.4.js"></script>
     


    <style type="text/css">
        .btn-group .btn {
            transition: background-color .3s ease;
        }

        .panel-table .panel-body {
            padding: 0;
        }

        .table-responsive {
            height: 700px;
        }

        .panel-table .panel-body .table-bordered {
            border-style: none;
            margin: 0;
        }

            .panel-table .panel-body .table-bordered > thead > tr > th:first-of-type {
                text-align: center;
                width: 100px;
            }

            .panel-table .panel-body .table-bordered > thead > tr > th:last-of-type,
            .panel-table .panel-body .table-bordered > tbody > tr > td:last-of-type {
                border-right: 0px;
            }

            .panel-table .panel-body .table-bordered > thead > tr > th:first-of-type,
            .panel-table .panel-body .table-bordered > tbody > tr > td:first-of-type {
                border-left: 0px;
            }

            .panel-table .panel-body .table-bordered > tbody > tr:first-of-type > td {
                border-bottom: 0px;
            }

            .panel-table .panel-body .table-bordered > thead > tr:first-of-type > th {
                border-top: 0px;
            }

        .panel-table .panel-footer .pagination {
            margin: 0;
        }

        .panel-footer, .panel-table .panel-body .table-bordered {
            border-style: none;
            margin: 0;
        }

        .pagination > li > a, .pagination > li > span {
            border-radius: 50% !important;
            margin: 0 5px;
        }

        .pagination {
            margin: 0;
        }


        /*
used to vertically center elements, may need modification if you're not using default sizes.
*/
        .panel-table .panel-footer .col {
            line-height: 34px;
            height: 34px;
        }

        .panel-table .panel-heading .col h3 {
            line-height: 30px;
            height: 30px;
        }

        .panel-table .panel-body .table-bordered > tbody > tr > td {
            line-height: 34px;
        }

        @media (min-width: 1200px) {
            .container {
                width: 100%;
            }
        }

        #loading {
            display: none;
            width: 100px;
            height: 100px;
            position: fixed;
            top: 50%;
            left: 40%;
            background: url(Images/loading2.gif) no-repeat center #fff;
            text-align: center;
            padding: 10px;
            font: normal 16px Tahoma, Geneva, sans-serif;
            border: 1px solid #666;
            margin-left: -50px;
            margin-top: -50px;
            z-index: 2;
            overflow: auto;
        }


        .table-responsive2 {
            width: 100%;
            margin-bottom: 15px;
            overflow-x: auto;
            overflow-y: hidden;
            -webkit-overflow-scrolling: touch;
            -ms-overflow-style: -ms-autohiding-scrollbar;
            border: 1px solid #ddd;
        }

            .table-responsive2 table {
                table-layout: fixed;
            }

        .tableheader {
            width: 900px;
            margin-bottom: 0px;
            border: 1px solid #999;
        }

        .tablebody {
            height: 400px;
            overflow-y: auto;
            width: 900px;
            margin-bottom: 20px;
        }

        .btn-label {
            position: relative;
            left: -12px;
            display: inline-block;
            padding: 6px 12px;
            background: rgba(0,0,0,0.15);
            border-radius: 3px 0 0 3px;
        }

        .btn-labeled {
            padding-top: 0;
            padding-bottom: 0;
        }

        .btn {
            margin-bottom: 10px;
        }

        .btn-info {
            box-shadow: 0 0 0 1px #5bc0de inset, 0 0 0 2px rgba(255,255,255,0.15) inset, 0 8px 0 0 #46b8da, 0 8px 0 1px rgba(0,0,0,0.4), 0 8px 8px 1px rgba(0,0,0,0.5);
            background-color: #5bc0de;
        }
    </style>

    <script type="text/javascript">
        $(document).ready(function () {
     $('.btn-filter').on('click', function () {
         var $target = $(this).data('target');
         if ($target != 'all') {
             $('.table tbody tr').css('display', 'none');
             $('.table tr[data-status="' + $target + '"]').fadeIn('slow');
         } else {
             $('.table tbody tr').css('display', 'none').fadeIn('slow');
         }
     });

     $('#checkall').on('click', function () {
         if ($("#mytable #checkall").is(':checked')) {
             $("#mytable input[type=checkbox]").each(function () {
                 $(this).prop("checked", true);
             });

         } else {
             $("#mytable input[type=checkbox]").each(function () {
                 $(this).prop("checked", false);
             });
         }
     });
 });

   <%--  $(document).ready(function () {
            var frmdt = $("#<%=dateFromConsolidated.ClientID%>").val(); //id name for dropdown list     
         var todt = $("#<%=dateToConsolidated.ClientID%>").val(); //id name for dropdown list   
         $("#loading").show();
         $("#tbleCashLedger").append(" ");
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "DirectCashLedger.aspx/GetCashLedger",
                data: JSON.stringify({ frmdt: frmdt, todt: todt }),
                dataType: "json",
                success: function (data) {
                    for (var i = 0; i < data.d.length; i++) {
                        $("#tbleCashLedger").append("<tr><td>" + data.d[i].Date + "</td><td>" + data.d[i].OpeningBalance + "</td><td>" + data.d[i].Narration + "</td><td>" + data.d[i].Credit + "</td><td>" + data.d[i].Debit + "</td><td>" + data.d[i].ClosingBalance + "</td></tr>");
                    }
                    $("#loading").hide();
                },
                error: function (result) {
                    alert("Error");
                }
            });
        });--%>

       

        function LedgerData() {

            var frmdt = $("#<%=dateFromConsolidated.ClientID%>").val(); //id name for dropdown list     
            var todt = $("#<%=dateToConsolidated.ClientID%>").val(); //id name for dropdown list  
            $("#loading").show();
            $("#tbleCashLedger").html('');
            //$("#tbleCashLedger").append(" ");
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "DirectCashLedger.aspx/GetCashLedger",
                data: JSON.stringify({ frmdt: frmdt, todt: todt }),
                dataType: "json",
                success: function (data) {
                    for (var i = 0; i < data.d.length; i++) {
                        //$("#tbleCashLedger").append("<tr><td>" + data.d[i].Date + "</td><td>" + data.d[i].OpeningBalance + "</td><td>" + data.d[i].Narration + "</td><td>" + data.d[i].Credit + "</td><td>" + data.d[i].Debit + "</td><td>" + data.d[i].ClosingBalance + "</td></tr>");
                        $("#tbleCashLedger").append("<tr><td>" + data.d[i].Date + "</td><td>" + data.d[i].Heads + "</td><td>" + data.d[i].Narration + "</td><td>" + data.d[i].Credit + "</td><td>" + data.d[i].Debit + "</td></tr>");
                    }
                    $("#loading").hide();
                },
                error: function (r, ajaxOptions, thrownError) {
                    alert("Error: " + xhr.responseText);
                }
            });
        }      
       
        </script>

    <script src="pertho_admin_v1.3/js/jquery.mask.js" type="text/javascript"></script>
    <script src="pertho_admin_v1.3/js/jquery.inputmask.js" type="text/javascript"></script>
    <script src="pertho_admin_v1.3/js/jquery.inputmask.extentions.js" type="text/javascript"></script>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphMainContent" runat="server">
    <link href="Styles/bootstrap.min.css" rel="stylesheet" />
     <div id="loading" style="display: none;">
       <%-- <img src="Images/loading2.gif" />--%>
    </div>
    <div class="row">
        <div class="twelve columns">
            <div class="box_c">
                <div class="box_c_heading cf">
                    <p class="sepV_a">
                        Cash Ledger
                    </p>
                </div>
                
                <div class="box_c_content">
                    <div class="row">
                        <div style="margin-top: -0.5em; margin-bottom: 0.6em;" class="Sub-heading">
                            <div style="display: table-cell; vertical-align: top; padding-top: 10px; padding-right: 5px !important;">
                                <asp:Label ID="Label3" runat="server" Text="From Date :"></asp:Label>
                            </div>
                            <div style="display: table-cell; vertical-align: top; padding-top: 5px; padding-right: 5px !important;">
                                <asp:TextBox class="input-text maskdate" ID="dateFromConsolidated" runat="server"
                                   placeholder="From Date">
                                </asp:TextBox>
                                <asp:RequiredFieldValidator ValidationGroup="twelvehead" Display="Dynamic" ID="CompareValidatoxr1"
                                    runat="server" ControlToValidate="dateFromConsolidated" ErrorMessage=""></asp:RequiredFieldValidator>
                                <asp:CompareValidator ValidationGroup="twelvehead" Display="Dynamic" ID="cmpStartDate"
                                    runat="server" ControlToValidate="dateFromConsolidated" Operator="DataTypeCheck" Type="Date"></asp:CompareValidator>
                            </div>
                            <div style="display: table-cell; vertical-align: top; padding-top: 10px; padding-right: 5px !important;">
                                <asp:Label ID="Label1" runat="server" Text="To Date :"></asp:Label>
                            </div>
                            <div style="display: table-cell; vertical-align: top; padding-top: 5px; padding-right: 5px !important;">
                                <asp:TextBox class="input-text maskdate" ID="dateToConsolidated" placeholder="To Date"
                                    runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ValidationGroup="twelvehead" Display="Dynamic" ID="RequiredFieldValidator1"
                                    runat="server" ControlToValidate="dateToConsolidated" ErrorMessage=""></asp:RequiredFieldValidator>
                                <asp:CompareValidator ValidationGroup="twelvehead" Display="Dynamic" ID="cmpEndDate"
                                    runat="server" ControlToCompare="dateFromConsolidated" ControlToValidate="dateToConsolidated" Operator="GreaterThanEqual"
                                    Type="Date"></asp:CompareValidator>
                            </div>
                            <div style="display: table-cell; vertical-align: top; padding-top: 2px; padding-right: 5px !important;">                            
                                <button type="button" value="GO!" onclick="LedgerData();" class="btn btn-labeled btn-info">
                                    <span class="btn-label"><i class="glyphicon glyphicon-refresh"></i></span>Go!</button>
                            </div>
                              <div style="display: table-cell; vertical-align: top; padding-top: 2px; padding-right: 5px !important;">                            
                                <asp:ImageButton ID="imgpdf" runat="server" ImageUrl="Styles/Image/pdfexp.png"
                                    Height="33px" Width="34px" OnClick="imgpdf_Click" />
                            </div>
                            <div style="display: table-cell; vertical-align: top; padding-top: 2px; padding-right: 5px !important;">                            
                            <asp:Button Text="Export" OnClick="ExportExcel" runat="server" />
                                </div>
                        </div>
                    </div>

                    <%--Bootstrap starts here--%>
                    
                        <div class="panel panel-default panel-table">
                            <div class="panel-heading">
                                <div class="row">
                                    <div class="col col-xs-6">
                                        <h3 class="panel-title">Date wise Cash Ledger</h3>
                                    </div>
                                    <%--  <div class="col col-xs-6 text-right">
                    <button type="button" class="btn btn-sm btn-primary btn-create">Create New</button>
                  </div>--%>
                                </div>
                            </div>
                            <div class="panel-body table-responsive">
                                <table  id="tbl"   class="table table-striped table-bordered table-list">
                                    <thead>
                                        <tr>
                                            <th>Date</th>
                                            <th>Heads</th>
                                            <th>Narration</th>
                                            <th>Credit</th>
                                            <th>Debit</th>                                           
                                        </tr>
                                    </thead>
                                    <tbody id="tbleCashLedger">
                                        <tr style="background: #EAEAEA;">                                          
                                            <td>Date</td>
                                            <td>Heads</td>
                                            <td>Narration</td>
                                            <td>Credit</td>
                                            <td>Debit</td>                                         
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                            <%--<div class="panel-body  table-responsive">
                <div class="panel-body table-responsive2">
                    <div class="panel-body table-responsive">
               <table class="table table-striped table-bordered table-list">
                <%--<table class="table table-bordered table-striped tableheader">
                  <thead>
                    <tr>
                        
                        <th>Date</th>
                        <th>OpeningBalance</th>
                        <th>Narration</th>
                        <th>Credit</th>
                        <th>Debit</th>
                        <th>ClosingBalance</th>                        
                    </tr> 
                  </thead>
                </table>
                <div class="tablebody">
                    <table class="table table-bordered table-striped" >
                  <tbody id="tbleCashLedger">                
                          <tr style="background:#EAEAEA;">
                            <%--<td align="center"> SNo </td>
                            <td>Date</td>
                            <td>OpeningBalance</td>
                            <td>Narration</td>
                            <td>Credit</td>
                            <td>Debit</td>
                            <td>ClosingBalance</td>                            
                          </tr>                        
                        </tbody>
                </table>            
              </div>--%>
                        </div>
                  


                </div>

            </div>
        </div>
    </div>

        <%--<script type="text/javascript">
            $(document).ready(function () {
         $(".chzn-select").chosen({ search_contains: true });
         prth_mask_input.init();
     });
                 var prm = Sys.WebForms.PageRequestManager.getInstance();
                 prm.add_endRequest(function () {
                     $(".chzn-select").chosen({ search_contains: true });
                 });
                 </script>--%>
    
   
</asp:Content>
