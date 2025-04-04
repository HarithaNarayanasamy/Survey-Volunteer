<%@ Page Title="" Language="C#" MasterPageFile="~/Branch.Master" AutoEventWireup="true" CodeBehind="Subscriber_PaidDetails.aspx.cs" Inherits="SreeVisalamChitFundLtd_phase1.Subscriber_PaidDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <script src="pertho_admin_v1.3/js/jquery.mask.js" type="text/javascript"></script>
    <script src="pertho_admin_v1.3/js/jquery.inputmask.js" type="text/javascript"></script>
    <script src="pertho_admin_v1.3/js/jquery.inputmask.extentions.js" type="text/javascript"></script>
    <script src="pertho_admin_v1.3/js/jquery.min.js" type="text/javascript"></script>
    <script src="pertho_admin_v1.3/js/jquery.inputmask.js" type="text/javascript"></script>
    <script src="pertho_admin_v1.3/js/jquery.inputmask.extentions.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMainContent" runat="server">
    <style type="text/css">
        td[style="cursor:default;"] {
            vertical-align: middle;
        }
    </style>
    <style type="text/css">
        .header {
            background-color: #646464;
            font-family: Arial;
            color: White;
            border: none 0px transparent;
            height: 25px;
            text-align: center;
            font-size: 16px;
        }

        .pager {
            background-color: #5badff;
            font-family: Arial;
            color: White;
            height: 30px;
            text-align: left;
        }
    </style>
    <style type="text/css">
        .chzn-results {
            text-align: center;
        }

        #ctl00_cphMainContent_ddlChit_chzn .chzn-drop .chzn-search input[type="text"] {
            height: 16px;
        }
    </style>

    <style type="text/css">
        .Grid {
            background-color: #fff;
            margin: 5px 0 10px 0;
            border: solid 1px #525252;
            border-collapse: collapse;
            font-family: Calibri;
            color: #474747;
        }

            .Grid td {
                padding: 2px;
                border: solid 1px #c1c1c1;
            }

            .Grid th {
                padding: 4px 2px;
                color: #fff;
                background: #A9A9A9;
                /*#363670 url(Images/grid-header.png) repeat-x top;*/
                border-left: solid 1px #525252;
                font-size: medium;
            }

            .Grid .alt {
                background: #fcfcfc url(Images/grid-alt.png) repeat-x top;
            }

            .Grid .pgr {
                background: #363670 url(Images/grid-pgr.png) repeat-x top;
            }

                .Grid .pgr table {
                    margin: 3px 0;
                }

                .Grid .pgr td {
                    border-width: 0;
                    padding: 0 6px;
                    border-left: solid 1px #666;
                    font-weight: bold;
                    color: #fff;
                    line-height: 18px;
                }

                .Grid .pgr a {
                    color: Gray;
                    text-decoration: none;
                }

                    .Grid .pgr a:hover {
                        color: #000;
                        text-decoration: none;
                    }

        .wrapper2 {
            width: 880px;
            height: 800px;
        }

        .GridviewDiv {
            width: 950px;
            height: 800px;
            overflow: auto;
        }
    </style>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="row">
        <div class="twelve columns">
            <div class="box_c">
                <div class="box_c_heading  box_actions">
                    <p>
                        Panel List Group with Expandable Detail Section
                    </p>
                </div>
                <div class="box_c_content">
                    <div class="row">
                        <div style="width: 100%; margin: 0 auto;">
                            <asp:DropDownList ID="ddlGroup" runat="server" CssClass="chzn-select" Width="300px"></asp:DropDownList>
                            <asp:Button ID="BtnGo" OnClick="BtnGo_Click"
                                runat="server" class="GreenyPushButton" Text="Go!"></asp:Button>
                            <asp:Button ID="BtnSave" runat="server" class="GreenyPushButton pull-eight" Text="Save" OnClientClick="return SaveGrpMember();"></asp:Button><br />
                            <asp:Button ID="BtnExport" runat="server" class="GreenyPushButton pull-eight" OnClick="BtnExport_Click" Text="Save"></asp:Button><br />

                            <asp:CheckBox ID="chkSelectAll" runat="server" Text="Select All" OnCheckedChanged="chkSelectAll_CheckedChanged" onclick="javascript: CheckAll();" />
                            <asp:HiddenField ID="hdRowCount" runat="server" />
                            <div class="wrapper2">
                                <div class="GridviewDiv">
                                    <asp:GridView runat="server" ID="gvDetails" AllowPaging="true" PageSize="50" AutoGenerateColumns="false"
                                        CssClass="Grid" FooterStyle-Font-Size="Medium" PagerStyle-CssClass="pager" RowStyle-CssClass="rows" Font-Size="Large">
                                        <HeaderStyle BackColor="Gray" ForeColor="White" Font-Size="Medium" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Select">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkSelect" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="ICM">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblicm" runat="server" Text='<%# Eval("ICM") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Token" ItemStyle-Width="150px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblticketnumber" runat="server" Text='<%# Eval("TicketNumber") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Member Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblmembername" runat="server" Text='<%# Eval("MemberName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Mobile Number">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtMobile" runat="server" Text='<%# Eval("MobileNumber") %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Address">
                                                <ItemTemplate>
                                                    <asp:TextBox TextMode="MultiLine" ID="txtAddress" runat="server" Text='<%# Eval("Address") %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Installment No">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblinsNo" runat="server" Text='<%# Eval("InstallmentNo") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="ChitAg. No">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAgreementNo" runat="server" Text='<%# Eval("ChitAgreementNo") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Due Amnt">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtDueAmount" runat="server" Text='<%# Eval("DueAmount") %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Default Interest">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtinterest" runat="server" Text='<%# Eval("DefaultInterest") %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
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


        function CheckAll() {
            var totalRowCount = 0;
            var rowCount = 0;
            var checkedCheckBox;
            var totalRowCount = $("#<%= hdRowCount.ClientID %>").val();
            var dataGrid = document.all['gvDetails'];
            for (var index = 1; index <= (totalRowCount + 1) ; index++) {
                var cnt = (index < 10) ? "0" + index + "" : index;
                if ($('#ctl00_cphMainContent_chkSelectAll').is(":checked"))
                    $('#ctl00_cphMainContent_gvDetails_ctl' + cnt + '_chkSelect').attr("checked", true);
                else
                    $('#ctl00_cphMainContent_gvDetails_ctl' + cnt + '_chkSelect').attr("checked", false);
            }
            return false;
        }

        function SaveGrpMember() {
            debugger;
            var gridCheckedList = [];

            var grd = $("[id*=gvDetails]");
            // var row = grd.find("tr").eq(1);
            var gridCount = grd.find("tr").length;
            for (var i = 1; i <= (gridCount + 1) ; i++) {
                var cnt = (i < 10) ? "0" + i + "" : i;
                if ($('#ctl00_cphMainContent_gvDetails_ctl' + cnt + '_chkSelect').is(":checked")) {
                    gridCheckedList.push({
                        "lblicm": $('#ctl00_cphMainContent_gvDetails_ctl' + cnt + '_lblicm').text(),
                        "lblticketnumber": $('#ctl00_cphMainContent_gvDetails_ctl' + cnt + '_lblticketnumber').text(),
                        "lblmembername": $('#ctl00_cphMainContent_gvDetails_ctl' + cnt + '_lblmembername').text(),
                        "txtMobile": $('#ctl00_cphMainContent_gvDetails_ctl' + cnt + '_txtMobile').val(),
                        "txtAddress": $('#ctl00_cphMainContent_gvDetails_ctl' + cnt + '_txtAddress').val(),
                        "lblinsNo": $('#ctl00_cphMainContent_gvDetails_ctl' + cnt + '_lblinsNo').text(),
                        "lblAgreementNo": $('#ctl00_cphMainContent_gvDetails_ctl' + cnt + '_lblAgreementNo').text(),
                        "txtDueAmount": $('#ctl00_cphMainContent_gvDetails_ctl' + cnt + '_txtDueAmount').val(),
                        "txtinterest": $('#ctl00_cphMainContent_gvDetails_ctl' + cnt + '_txtinterest').val(),

                    });

                }
            }

            $.ajax({
                type: "POST",
                url: "Subscriber_PaidDetails.aspx/GetMemberid",
                data: "{'data':'" + JSON.stringify(gridCheckedList) + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    alert(msg.d);
                },
                error: function (result) {
                    alert("Error: " + result);
                }
            });
            return true;
        }
        

    </script>
</asp:Content>
