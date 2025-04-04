<%@ Page Title="" Language="C#" MasterPageFile="~/Branch.Master" AutoEventWireup="true" CodeBehind="WebAdminEdit.aspx.cs" Inherits="SreeVisalamChitFundLtd_phase1.WebAdminEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <style type="text/css">
        .chzn-drop {
            border: 1px solid #aaa !important;
            padding-top: 5px !important;
        }

        .Trans div[id*="chzn"] {
            width: 100% !important;
        }

            .Trans div[id*="chzn"] span {
                width: 140px !important;
                overflow: hidden;
            }

        .Grid, .Grid th, .Grid td {
            border: 1px solid #2F4F4F;
        }

        .panelstyle {
            margin-left: 80px;
        }

        .paddingtd {
            padding-left: 10px;
        }
    </style>
    <div class="row">
        <div class="twelve columns">
            <div>
                <br />
                <table>
                    <tr>
                        <td>
                            <asp:Button ID="btnPassword" runat="server" Text="Bill Collector Password" CssClass="GreenyPushButton" OnClick="btnPassword_Click" />
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        </td>

                        <td>
                            <asp:Button ID="btnWebsite" runat="server" Text="Update Website" CssClass="GreenyPushButton" OnClick="btnWebsite_Click" />
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        </td>

                        <td>
                            <asp:Button ID="btnChitBlock" runat="server" Text="Chit Token Block" CssClass="GreenyPushButton" OnClick="btnChitBlock_Click" />
                        </td>

                    </tr>
                </table>
                <br />
                <asp:Panel ID="PwdPanel" runat="server" BackColor="WhiteSmoke">
                    <table>

                        <tr>
                            <td style="vertical-align: top; padding-right: 5px; padding-top: 3px;">
                                <asp:Label ID="lblBranch" runat="server" Text="Select Branch"></asp:Label>
                            <td style="vertical-align: top; padding-right: 5px; padding-top: 3px;">
                                <asp:DropDownList ID="ddlBranch" runat="server" CssClass="chzn-select" OnSelectedIndexChanged="ddlBranch_SelectedItemChanged" Height="35px" Width="200px"></asp:DropDownList>
                            </td>
                            <td style="vertical-align: top; padding-right: 5px; padding-top: 3px;">
                                <asp:Button ID="btnGo" runat="server" Text="GO" CssClass="GreenyPushButton" OnClick="btnGo_Click" />
                            </td>
                            <td style="vertical-align: top; padding-right: 5px; padding-top: 3px;">
                                <asp:Label runat="server" ID="lblEmployee" Text="Select Money Collector" Visible="false"></asp:Label>
                            </td>
                            <td style="vertical-align: top; padding-right: 5px; padding-top: 3px;">
                                <asp:DropDownList ID="ddlEmployee" runat="server" CssClass="chzn-select" Height="35px" Width="200px" Visible="false" OnSelectedIndexChanged="ddlEmployee_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td style="vertical-align: top; padding-right: 5px; padding-top: 3px;">
                                <asp:Label ID="lblUsername" runat="server" Text="Username" Visible="false"></asp:Label>
                            </td>
                            <td style="vertical-align: top; padding-right: 5px; padding-top: 3px;">
                                <asp:TextBox ID="txtUsername" runat="server" Height="32px" Visible="false"></asp:TextBox>
                            </td>
                            <td style="vertical-align: top; padding-right: 5px; padding-top: 3px;">
                                <asp:Label ID="lblPwd" runat="server" Text="New Password" Visible="false"></asp:Label>
                            </td>
                            <td style="vertical-align: top; padding-right: 5px; padding-top: 3px;">
                                <asp:TextBox ID="txtPwd" runat="server" Height="32px" Visible="false"></asp:TextBox>
                            </td>
                            <td style="vertical-align: top; padding-right: 5px; padding-top: 3px;">
                                <asp:Button ID="btnUpdate" runat="server" Text="Update Password" CssClass="GreenyPushButton" OnClick="btnUpdate_Click" Visible="false" />
                            </td>
                            <td style="vertical-align: top; padding-right: 5px; padding-top: 3px;">
                                <asp:Button ID="btnBlock" runat="server" Text="Block" CssClass="GreenyPushButton" Visible="false" OnClick="btnBlock_Click" />
                            </td>
                            <td style="vertical-align: top; padding-right: 5px; padding-top: 3px;">
                                <asp:Button ID="btnUnBlock" runat="server" Text="Unblock" CssClass="GreenyPushButton" Visible="false" OnClick="btnUnBlock_Click" />
                            </td>
                            <td style="vertical-align: top; padding-right: 5px; padding-top: 3px; float: right">
                                <asp:Button ID="btnEmployeeReport" runat="server" Text="Employee Block Report" CssClass="GreenyPushButton" OnClick="btnEmployeeReport_Click" />
                            </td>
                        </tr>
                    </table>
                    <div style="padding: 5px 5px 5px 5px;">
                        <asp:GridView ID="gridEmpReport" runat="server" BorderStyle="None" CssClass="Grid" HeaderStyle-Font-Bold="true" Font-Names="Verdana" ForeColor="Black" GridLines="Both" CellSpacing="2"
                            AutoGenerateColumns="false" PageSize="50" ShowFooter="true" BackColor="White" BorderColor="Red" BorderWidth="1px" CellPadding="2" Width="800px" Font-Size="10.2pt" OnRowDataBound="gridEmpReport_RowDataBound">
                            <Columns>
                                <asp:TemplateField HeaderText="SNO" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="15px" >
                                    <ItemTemplate>
                                        <asp:Label ID="labelSno" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="B_Name" HeaderText="Branch Name" />
                                <asp:BoundField DataField="moneycollname" HeaderText="MoneyCollector Name" />
                                <asp:BoundField DataField="Status" HeaderText="Status" />
                            </Columns>
                        </asp:GridView>
                    </div>
                </asp:Panel>

                <ajax:ModalPopupExtender ID="modalPopup1" runat="server" TargetControlID="show" PopupControlID="popUp" BackgroundCssClass="modalBackground">
                </ajax:ModalPopupExtender>
                <asp:LinkButton ID="show" runat="server"></asp:LinkButton>
                <div id="popUp" style="display: none; background: #fff; border: 4px solid #ccc; border: 8px solid rgba(0, 0, 0, 0.5); background-clip: padding-box; -webkit-border-radius: 10px; -moz-border-radius: 10px; border-radius: 10px; min-width: 300px; max-width: 900px;"
                    cssclass="raised" runat="server">
                    <div class=" box_c_heading">
                        <span class="inner_heading" style="text-align: center;">Confirmation </span>
                    </div>
                    <div style="min-height: 40px; overflow: auto; max-width: 840px;">
                        <asp:Label ID="lblMsg" runat="server"></asp:Label>
                    </div>
                    <div class="box_c_heading">
                        <div style="float: right;">
                            <asp:Button ID="btnOk" runat="server" CssClass="GreenyPushButton" Text="Ok" OnClick="btnOk_Click" Visible="false" />
                            <asp:Button ID="btnConfirm" runat="server" CssClass="GreenyPushButton" Text="Confirm" OnClick="btnConfirm_Click" Visible="false" />
                            <asp:Button ID="btnCancel" runat="server" CssClass="GreenyPushButton" Text="Cancel" OnClick="btnCancel_Click" />
                        </div>
                    </div>

                </div>



                <div>
                    <asp:Panel ID="pnlWebsite" runat="server" BackColor="WhiteSmoke" Visible="false">
                        <table>
                            <tr>
                                <td>
                                    <asp:Label ID="lblScrollMessage" runat="server" Text="Enter the scrolling Message "></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtScrollMsg" runat="server" TextMode="MultiLine"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtScrollMsg" ErrorMessage="Please enter a message" ForeColor="Red" ValidationGroup="myValidation"></asp:RequiredFieldValidator>
                                </td>
                                <td></td>
                                <td>
                                    <asp:Label ID="lblFrom" runat="server" Text="From"></asp:Label>
                                </td>
                                <td></td>
                                <td>
                                    <asp:TextBox ID="txtFrom" runat="server" CssClass="input-text maskdate" Width="70%"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtFrom" ErrorMessage="*" ForeColor="Red" ValidationGroup="myValidation"></asp:RequiredFieldValidator>
                                    <%--<asp:CompareValidator ID="CompareValidator1" runat="server" Type="Date" ControlToValidate="txtFrom" ForeColor="Red" ErrorMessage="Enter a valid From Date"></asp:CompareValidator>--%>
                                </td>
                                <td>
                                    <asp:Label ID="lblTo" runat="server" Text="To"></asp:Label>
                                </td>
                                <td></td>
                                <td>
                                    <asp:TextBox ID="txtTo" runat="server" CssClass="input-text maskdate" Width="70%"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtTo" ErrorMessage="*" ForeColor="Red" ValidationGroup="myValidation"></asp:RequiredFieldValidator>
                                    <%--<asp:CompareValidator ID="CompareValidator2" runat="server" Type="Date" ControlToValidate="txtTo" ForeColor="Red" ErrorMessage="Enter a valid To Date"></asp:CompareValidator>--%>
                                </td>
                                <td>
                                    <asp:Button ID="btnScroll" runat="server" Text="Scroll Update" OnClick="btnScroll_Click" ValidationGroup="myValidation" CssClass="GreenyPushButton" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblHome" runat="server" Text="Select Image for Home"></asp:Label>
                                </td>
                                <td>
                                    <asp:FileUpload ID="FileUploadHome" runat="server" />
                                </td>
                                <td>
                                    <asp:Button ID="btnHome" runat="server" Text="Ok" CssClass="GreenyPushButton" OnClick="btnHome_Click" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblAbout" runat="server" Text="Select Image for About Us"></asp:Label>
                                </td>
                                <td>
                                    <asp:FileUpload ID="FileUploadAbout" runat="server" />
                                </td>
                                <td>
                                    <asp:Button ID="btnAbout" runat="server" Text="Ok" CssClass="GreenyPushButton" OnClick="btnAbout_Click" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblGeneral" runat="server" Text="Select Image for General Info"></asp:Label>
                                </td>
                                <td>
                                    <asp:FileUpload ID="FileUploadGeneral" runat="server" />
                                </td>
                                <td>
                                    <asp:Button ID="btnGeneral" runat="server" Text="Ok" CssClass="GreenyPushButton" OnClick="btnGeneral_Click" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblBranches" runat="server" Text="Select Image for Branches"></asp:Label>
                                </td>
                                <td>
                                    <asp:FileUpload ID="FileUploadBranches" runat="server" />
                                </td>
                                <td>
                                    <asp:Button ID="btnBranches" runat="server" Text="Ok" CssClass="GreenyPushButton" OnClick="btnBranches_Click" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblProducts" runat="server" Text="Select Image for Products"></asp:Label>
                                </td>
                                <td>
                                    <asp:FileUpload ID="FileUploadProducts" runat="server" />
                                </td>
                                <td>
                                    <asp:Button ID="btnProducts" runat="server" Text="Ok" CssClass="GreenyPushButton" OnClick="btnProducts_Click" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblContact" runat="server" Text="Select Image for Contact"></asp:Label>
                                </td>
                                <td>
                                    <asp:FileUpload ID="FileUploadContact" runat="server" />
                                </td>
                                <td>
                                    <asp:Button ID="btnContact" runat="server" Text="Ok" CssClass="GreenyPushButton" OnClick="btnContact_Click" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblCareer" runat="server" Text="Select Image for Career"></asp:Label>
                                </td>
                                <td>
                                    <asp:FileUpload ID="FileUploadCareer" runat="server" />
                                </td>
                                <td>
                                    <asp:Button ID="btnCareer" runat="server" Text="Ok" CssClass="GreenyPushButton" OnClick="btnCareer_Click" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </div>

                <!--25/06/2021 Blocking a Chit token( with Case)-->
                <div>
                    <br />
                    <asp:Panel ID="pnlChitBlock" runat="server" CssClass="raised" Visible="false">
                        <br />
                        <table>
                            <tr>
                                <td style="padding-left: 5px;">
                                    <asp:Label ID="lblBr" runat="server" Text="Select Branch"></asp:Label>
                                    <asp:DropDownList ID="ddlBranchList" runat="server" CssClass="chzn-select" Height="35px" Width="200px" OnChange="ListTkn();"></asp:DropDownList>
                                </td>
                                <td>
                                    <%--<asp:DropDownList ID="ddlChitGroups" runat="server" CssClass="chzn-select" Height="35px" Width="200px"></asp:DropDownList>--%>
                                    <asp:Label ID="lblChit" runat="server" Text="ChitNo"></asp:Label><br />
                                    <input id="txtSrch" type="text" style="width: 75px; height: 15px;" />
                                    <select id="abcd1" style="width: 150px;" onchange="FindTokenName();"></select>
                                    <asp:HiddenField ID="tkn_id" runat="server" />
                                    <asp:HiddenField ID="hiddentkn_text" runat="server" />
                                </td>

                                <td></td>
                                <td>
                                    <asp:Label ID="lblCustomerName" runat="server" Text="CustomerName"></asp:Label>
                                    <asp:TextBox ID="txtCustomerName" runat="server"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Label ID="lblReason" runat="server" Text="*Reason "></asp:Label>
                                    <asp:TextBox ID="txtBlockReason" runat="server"></asp:TextBox>
                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="*" ControlToValidate="txtBlockReason" ValidationGroup="bb"></asp:RequiredFieldValidator>--%>
                                </td>
                            </tr>
                            <tr>
                                <td></td>
                                <td>
                                    <asp:Button ID="btnBlockToken" runat="server" Text="Block Token" CssClass="GreenyPushButton" OnClick="btnBlockToken_Click" CausesValidation="true" ValidationGroup="bb" />
                                </td>
                                <td></td>
                                <td>
                                    <asp:Button ID="btnUnBlockToken" runat="server" Text="UnBlock Token" CssClass="GreenyPushButton" OnClick="btnUnBlockToken_Click" />
                                </td>
                                <td></td>
                                <%--21/07/2021--%>
                                <td style="float: right;">
                                    <asp:Button ID="Button1" runat="server" Text="Chit Block Report" OnClick="btnBlockReport_Click" CssClass="GreenyPushButton" />
                                </td>
                            </tr>
                        </table>
                        <br />
                        <br />
                        <div style="padding: 5px 5px 5px 5px;">
                            <asp:GridView ID="gridReport" runat="server" BorderStyle="None" CssClass="Grid" HeaderStyle-Font-Bold="true" Font-Names="Verdana" ForeColor="Black" GridLines="Both" CellSpacing="2"
                                AutoGenerateColumns="false" PageSize="50" ShowFooter="true" BackColor="White" BorderColor="Red" BorderWidth="1px" CellPadding="2" Width="800px" Font-Size="10.2pt">
                                <Columns>
                                    <asp:TemplateField HeaderText="SNO" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="15px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSno" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="B_Name" HeaderText="Branch Name" />
                                    <asp:BoundField DataField="MemberName" HeaderText="MemberName" />
                                    <asp:BoundField DataField="GrpMemberID" HeaderText="Chit Number" />
                                    <asp:BoundField DataField="Status" HeaderText="Status" />
                                    <asp:BoundField DataField="BlockReason" HeaderText="Reason" />
                                    <asp:BoundField DataField="BlockedOn" HeaderText="Blocked On" />
                                    <asp:BoundField DataField="UnBlockedOn" HeaderText="UnBlocked On" />
                                </Columns>
                            </asp:GridView>
                        </div>
                        <br />
                    </asp:Panel>
                </div>
            </div>
        </div>
        <script type="text/javascript">
            $(document).ready(function () {
                prth_mask_input.init();
            });
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_endRequest(function () {
                prth_mask_input.init();
            });
        </script>

        <script type="text/javascript">
            function ListTkn() {

                var gt = $("#<%=ddlBranchList.ClientID%>").find("option:selected").val();

                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    url: "WebAdminEdit.aspx/ChitToken",
                    data: "{branchid:" + gt + "}",
                    dataType: "json",
                    success: function (data) {
                        var options = $("#abcd1");
                        options.empty().append('<option selected="selected" value="0">Please Select</option>');
                        for (var i = 0; i < data.d.length; i++) {
                            $("#abcd1").append("<option value='" + data.d[i].Value + "'>" + data.d[i].Text + "</option>");
                        }
                    },
                    errror: function (result) {
                        alert("Error:" + result);
                    }
                });
            }

            $(document).ready(function () {
                $('#txtSrch').change(function () {
                    var gt = $("#<%=ddlBranchList.ClientID%>").find("option:selected").val();

                    var txt = $('#txtSrch').val();
                    if (txt != "") {
                        $.ajax({
                            type: "POST",
                            contentType: "application/json; charset=utf-8",
                            url: "WebAdminEdit.aspx/Getsrchlist",
                            data: JSON.stringify({ branchid: gt, seltext: txt }),
                            dataType: "json",
                            success: function (data) {
                                var options = $("#abcd1");
                                options.empty().append('<option selected="selected" value="0">Please Select</option>');
                                for (var i = 0; i < data.d.length; i++) {
                                    $("#abcd1").append("<option value='" + data.d[i].Value + "'>" + data.d[i].Text + "</option>");
                                }
                            },
                            error: function (result) {
                                alert("Error:" + result);
                            }
                        });
                    }
                    else {
                        ListTkn();
                    }
                });
            });

            function FindTokenName() {
                var tkid = $('#abcd1').find('option:selected').val();
                var tkntext = $('#abcd1').find('option:selected').text();

                $("#<%= hiddentkn_text.ClientID %>").val("");
                $("#<%= hiddentkn_text.ClientID %>").val(tkntext);


                $("#<%= tkn_id.ClientID %>").val("");
                $("#<%= tkn_id.ClientID %>").val(tkid);

                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    url: "WebAdminEdit.aspx/GetCustomerName",
                    data: "{hdid:" + tkid + "}",
                    dataType: "json",
                    success: function (data) {
                        var msg = data.d.toString();
                        var txtCustomer = $("[id*=txtCustomerName]");
                        txtCustomer.val(msg);
                    },
                    error: function (result) {
                        alert("Error:" + result);
                    }
                });
            }
        </script>
</asp:Content>
