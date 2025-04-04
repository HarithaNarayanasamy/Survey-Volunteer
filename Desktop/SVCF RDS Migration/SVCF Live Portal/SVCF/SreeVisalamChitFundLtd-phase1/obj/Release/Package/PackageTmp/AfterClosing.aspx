<%@ Page Title="" Language="C#" MasterPageFile="~/Branch.Master" AutoEventWireup="true" CodeBehind="AfterClosing.aspx.cs" Inherits="SreeVisalamChitFundLtd_phase1.AfterClosing" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
       <%-- <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script src="Scripts/jquery.searchabledropdown-1.0.8.min.js" type="text/javascript"></script>--%>
    <script type="text/javascript">
  $(document).ready(function () {
    //$("ddlHeads").searchable({
    //  maxListSize: 200, // if list size are less than maxListSize, show them all
    //  maxMultiMatch: 300, // how many matching entries should be displayed
    //  exactMatch: false, // Exact matching on search
    //  wildcards: true, // Support for wildcard characters (*, ?)
    //  ignoreCase: true, // Ignore case sensitivity
    //  latency: 200, // how many millis to wait until starting search
    //  warnMultiMatch: 'top {0} matches ...',
    //  warnNoMatch: 'no matches ...',
    //  zIndex: 'auto'
    //      });
       });

    </script>
  <%--  <script src="pertho_admin_v1.3/js/jquery.min.js" type="text/javascript"></script>
    <script src="pertho_admin_v1.3/js/jquery.inputmask.js" type="text/javascript"></script>
    <script src="pertho_admin_v1.3/js/jquery.inputmask.extentions.js" type="text/javascript"></script>
    <script src="https://code.jquery.com/jquery-2.2.4.js"></script>
    $.noConflict();--%>

    <link href="Styles/ControlsCss.css" rel="stylesheet" />

    <style type="text/css">
        .btn-group .btn {
            transition: background-color .3s ease;
        }

        .panel-table .panel-body {
            padding: 0;
        }

        .table-responsive {
            height: 634px;
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

        .box_cheading {
            height: 30px;
            border: 1px solid #dcdcdc;
            font-size: 16px;
        }
    </style>
   <%-- <script src="pertho_admin_v1.3/js/jquery.mask.js" type="text/javascript"></script>
    <script src="pertho_admin_v1.3/js/jquery.inputmask.js" type="text/javascript"></script>
    <script src="pertho_admin_v1.3/js/jquery.inputmask.extentions.js" type="text/javascript"></script>--%>
     <link href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" rel="stylesheet" />
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.4.0/css/font-awesome.min.css" rel='stylesheet' type='text/css' />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMainContent" runat="server">
   <ajax:ToolkitScriptManager ID="scm1" runat="server">
    </ajax:ToolkitScriptManager>
    <asp:Panel CssClass="row" ID="Panel1" runat="server" class="content">
        <div class="twelve columns">
            <div class="box_c">
                <div class="box_c_heading ">
                    <span class="bAct_hide">
                        <img src="img/blank.gif" class="bAct_x" alt="" />
                    </span>
                    <span class="bAct_toggle">
                        <img src="img/blank.gif" class="bAct_minus" alt="" />
                    </span>
                    <h2 style="font-size: 20px;">Voucher Details
                    </h2>
                </div>
                <div class="box_c_content">

                    <%--Date and Voucher no.--%>
                    <div class="row">
                        <div class="col-xs-4">
                            <label>Date</label>
                            <div class="form-group">
                                <asp:TextBox AutoPostBack="true" ValidationGroup="Generate" TabIndex="1" ID="txtDate" onchange="CheckDate();"
                                    CssClass="form-control maskdate" Style="height: 30px;" runat="server"></asp:TextBox>
                               <asp:RequiredFieldValidator ForeColor="Red" ID="RequiredFieldValidator6" ErrorMessage="Rquired!!!"
                                                        ControlToValidate="txtDate" ValidationGroup="Generate" Display="Dynamic" runat="server"></asp:RequiredFieldValidator>
                                                    <asp:CompareValidator ForeColor="Red" Type="Date" ID="CompareValidator11" ValidationGroup="Generate"
                                                        ControlToValidate="txtDate" Display="Dynamic" Operator="DataTypeCheck" runat="server"
                                                        ErrorMessage="Incorrect!!!"></asp:CompareValidator>
                                                    <asp:RangeValidator ID="rvDate" EnableClientScript="false" runat="server"
                                                        Type="Date" ControlToValidate="txtDate" ValidationGroup="Generate" ForeColor="Red" Display="Dynamic"
                                                        ErrorMessage="*"></asp:RangeValidator>
                            </div>
                        </div>
                        <div class="col-xs-4">

                            <label>Voucher Number</label>
                            <div class="form-group">
                                <asp:TextBox TabIndex="2" ValidationGroup="Generate" ID="txtVoucherNo" ReadOnly="true" Style="height: 30px;"
                                    CssClass="form-control sp_number" Visible="true" runat="server"></asp:TextBox>

                                <asp:RequiredFieldValidator Display="Dynamic" ValidationGroup="Generate" ControlToValidate="txtVoucherNo"
                                    ID="RequiredFieldValidator12" ErrorMessage="Rquired!!!" runat="server"> </asp:RequiredFieldValidator>
                                
                                <asp:Label ID="lblReceivedBy" runat="server" Text="By" Visible="false"></asp:Label>
                                   <asp:TextBox Width="150" ValidationGroup="Generate" CssClass="input-text" ID="txtReceivedBy" Visible="false"
                                                        runat="server"></asp:TextBox>
                                 <asp:Label ID="lblSeries" runat="server" Text="Series" Visible="false"></asp:Label>
                                <asp:TextBox Width="100" ValidationGroup="Generate" ID="txtSeries" Visible="false"
                                                        CssClass="input-text" runat="server"></asp:TextBox>

                            </div>

                        </div>

                    </div>

                    <%--Credit Transaction  : Heading.--%>
                    <div class="row">
                        <div class="col-lg-4">
                            <asp:Image runat="server" ID="img16List" ImageUrl="pertho_admin_v1.3/img/ico/icSw2/16-List.png" AlternateText="" />
                            <span>
                                Credit Transactions
                            </span>
                        </div>
                    </div>
                    
                    <%--Credit Transaction started.--%>
                    <div class="row"  style="height:200px;">
                        <div class="panel-body table-responsive">
                            
                            <table class="table table-striped table-bordered table-list">
                                <thead>
                                    <tr>
                                        <th>Heads</th>
                                        <th>Amount</th>
                                        <th>Description</th>
                                        <th>Cheque NO</th>
                                        <th>Action</th>
                                    </tr>
                                </thead>
                                <tbody>

                                    <tr>
                                        <td style="width:300px;">
                                            <asp:DropDownList CssClass="chzn-select" ClientIDMode="Static" Style="width: 350px !important;"
                                                    TabIndex="3" ID="ddlHeads"
                                                    runat="server" OnChange="GetChequeNumber();">
                                                </asp:DropDownList>
                                        </td>
                                        <td style="width:100px;">
                                            <asp:TextBox Width="150" TabIndex="4"
                                                CssClass="form-control  sp_currency number" ID="txtAmount" runat="server">
                                            </asp:TextBox>
                                        </td>
                                        <td style="width:300px;">
                                            <asp:TextBox TextMode="MultiLine" Height="50" TabIndex="5"
                                                ValidationGroup="GrpRow" CssClass="form-control" ID="txtDescription" runat="server">
                                            </asp:TextBox>
                                        </td>
                                           <td>
                                                <asp:Label ID="lblChequeNO" runat="server" Text="Cheque NO"></asp:Label>
                                                <br />
                                                <asp:TextBox Width="100"
                                                    MaxLength="7" ID="txtChequeNO"
                                                    runat="server">
                                                </asp:TextBox>
                                            </td>
                                       <%-- <td style="width:100px;">
                                            <asp:TextBox Width="100" MaxLength="7" ID="txtChequeNO" runat="server" CssClass="number form-control">
                                            </asp:TextBox>
                                        </td>--%>
                                        <td>
                                            <asp:Button ID="crbtnAdd" style="width:100px;" TabIndex="6" CssClass="btn-custom form-control" OnClick="btn_crAdd" ToolTip="Add" runat="server" Text="Add" />
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                            </div>
                           </div>
                            <div class="row">
                                <asp:GridView ID="GridCr" OnRowDeleting="GridCr_RowDeleting" BorderStyle="Solid" runat="server" 
                                    CellSpacing="2" Font-Names="Verdana" ForeColor="#333333" GridLines="Both" Height="100px"
                                    AutoGenerateColumns="false" CssClass="table table-bordered" Width="850px">
                                    <RowStyle BackColor="#F7F6F3" />
                                    <RowStyle CssClass="GridViewRowStyle" />
                                    <HeaderStyle CssClass="GridViewHeaderStyle" />
                                    <Columns>
                                        
<asp:BoundField HeaderText="RowNumber" DataField="RowNumber"
                                               ItemStyle-CssClass="visible-lg" HeaderStyle-CssClass="visible-lg" >
                                            </asp:BoundField>                                           
<asp:BoundField HeaderText="Heads" DataField="Heads"
                                               ItemStyle-CssClass="visible-lg" HeaderStyle-CssClass="visible-lg" >
                                            </asp:BoundField>                                        
<asp:BoundField HeaderText="Amount" DataField="Amount"
                                               ItemStyle-CssClass="visible-lg" HeaderStyle-CssClass="visible-lg" >
                                            </asp:BoundField>                                        
<asp:BoundField HeaderText="Description" DataField="Description"
                                               ItemStyle-CssClass="visible-lg" HeaderStyle-CssClass="visible-lg" >
                                            </asp:BoundField>                                        
<asp:BoundField HeaderText="ChequeNo" DataField="ChequeNo"
                                               ItemStyle-CssClass="visible-lg" HeaderStyle-CssClass="visible-lg" >
                                            </asp:BoundField>                                        
<asp:BoundField HeaderText="headid" DataField="headid"
                                               ItemStyle-CssClass="visible-lg" HeaderStyle-CssClass="visible-lg" >
                                            </asp:BoundField>
                                       
                                        <asp:TemplateField HeaderText="Delete" ItemStyle-CssClass="visible-lg" HeaderStyle-CssClass="visible-lg">
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
                            </div>
                    <%--Credit Transaction ends here--%>

                    <%--Debit Transaction  : Heading.--%>
                     <div class="row">
                        <div class="col-lg-4">
                            <asp:Image runat="server" ID="Image1" ImageUrl="pertho_admin_v1.3/img/ico/icSw2/16-List.png" AlternateText="" />
                            <span>
                                Debit Transactions
                            </span>
                        </div>
                    </div>
                    <div class="row" style="height:200px;">
                        <div class="panel-body table-responsive">
                            <table class="table table-striped table-bordered table-list">
                                <thead>
                                    <tr>
                                        <th>Heads</th>
                                        <th>Amount</th>
                                        <th>Description</th>                                        
                                        <th>Action</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr>
                                        <td  style="width:200px;">
                                              <asp:DropDownList CssClass="chzn-select form-control" Style="width: 300px !important;"
                                                TabIndex="7" AutoPostBack="false" ID="ddlHeadsDebit" runat="server" onchange="ondebitchange();">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width:150px;">
                                              <asp:TextBox ID="debitAmnt" runat="server" TabIndex="8" CssClass="number form-control"></asp:TextBox>
                                          <%--     <asp:HiddenField ID="hidden_totalcred" runat="server" />--%>
                                        </td>
                                        <td style="width:300px;">
                                             <asp:TextBox TextMode="MultiLine" Height="50" TabIndex="9" CssClass="form-control"
                                                ID="txtDebitdesc" style="width:350px;" runat="server">
                                            </asp:TextBox>
                                        </td>                                    
                                        <td  style="width:100px;" >
                                             <asp:Button ID="DbbtnAdd" CssClass="btn-custom form-control"  style="width:100px;"  OnClick="btn_debit" ToolTip="Add" TabIndex="10" runat="server" Text="Add" />
                                        </td>
                                       
                                   

                                </tbody>
                            </table>
</div>
                        </div>
                    <div class="row">
                        <asp:GridView ID="GrdDbClnt" OnRowDeleting="GrdDbClnt_RowDeleting" BorderStyle="Solid" runat="server"
                                        CellSpacing="2" Font-Names="Verdana" ForeColor="#333333" GridLines="Both" Height="100px"
                                        AutoGenerateColumns="false" CssClass="table table-bordered" Width="850px" >
                                        <RowStyle BackColor="#F7F6F3" />
                                        <RowStyle CssClass="GridViewRowStyle" />
                                        <HeaderStyle CssClass="GridViewHeaderStyle" />
                                        <Columns>
                                         <asp:BoundField HeaderText="RowNumber" DataField="RowNumber"
                                               ItemStyle-CssClass="visible-lg" HeaderStyle-CssClass="visible-lg" >
                                            </asp:BoundField>    
<asp:BoundField HeaderText="DbHeads" DataField="DbHeads"
                                               ItemStyle-CssClass="visible-lg" HeaderStyle-CssClass="visible-lg" >
                                            </asp:BoundField>
<asp:BoundField HeaderText="DbAmount" DataField="DbAmount"
                                               ItemStyle-CssClass="visible-lg" HeaderStyle-CssClass="visible-lg" >
                                            </asp:BoundField>
<asp:BoundField HeaderText="DbDescription" DataField="DbDescription"
                                               ItemStyle-CssClass="visible-lg" HeaderStyle-CssClass="visible-lg" >
                                            </asp:BoundField>
<asp:BoundField HeaderText="Dbheadid" DataField="Dbheadid"
                                               ItemStyle-CssClass="visible-lg" HeaderStyle-CssClass="visible-lg" >
                                            </asp:BoundField>
                                             <asp:TemplateField HeaderText="Delete" ItemStyle-CssClass="visible-lg" HeaderStyle-CssClass="visible-lg">
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
                    </div>

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



    <%-- <script src="Scripts/jquery-1.8.3.min.js"></script>--%>

        
    <script type="text/javascript">

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
       

         $(document).ready(function () {
            $(".chzn-select").chosen({ search_contains: true });
        });


        function OnKeyPress(s, e) {
            var charCode = e.htmlEvent.charCode;
            if (String.fromCharCode(charCode) == "/") {
                e.processOnServer = false;
                return false
            }
        }
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
   <%-- <script src="Scripts/jquery-1.8.3.min.js"></script>--%>
</asp:Content>
