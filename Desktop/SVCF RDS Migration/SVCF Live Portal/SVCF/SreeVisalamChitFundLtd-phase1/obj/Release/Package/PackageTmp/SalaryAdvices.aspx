<%@ Page Language="C#" Culture="en-GB" MasterPageFile="~/Branch.Master" AutoEventWireup="true" CodeBehind="SalaryAdvices.aspx.cs"
    Title="SVCF - Admin Panel" Inherits="SreeVisalamChitFundLtd_phase1.SalaryAdvices" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Head1" ContentPlaceHolderID="cphHead" runat="server">
    <link href="Styles/aspxtable.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.2/css/bootstrap.min.css">
    <link href='http://fonts.googleapis.com/css?family=Roboto:100' rel='stylesheet' type='text/css'>
    <style type="text/css">
        .auto-style1 {
            width: 262px;
        }

        .roboto {
            font-family: 'Roboto', sans-serif !important;
        }

        /* custom background for panel  */
        .container {
            padding-top: 50px !important;
            background-color: #f5f5f5 !important;
        }

        /* custom background header panel */
        .custom-header-panel {
            background-color: #004b8e !important;
            border-color: #004b8e !important;
            color: white;
        }

        .no-margin-form-group {
            margin: 0 !important;
        }

        .requerido {
            color: red;
        }

        .btn-orange-md {
            background: #FF791F !important;
            border-bottom: 3px solid #ae4d13 !important;
            color: white;
        }

            .btn-orange-md:hover {
                background: #d86016 !important;
                color: white !important;
            }
    </style>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMainContent" runat="server">
    <ajax:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </ajax:ToolkitScriptManager>
    <asp:Button ID="btnShowPopup" runat="server" Style="display: none" />
    <ajax:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="btnShowPopup"
        PopupControlID="PnlApprove" BackgroundCssClass="modalBackground">
    </ajax:ModalPopupExtender>
    <div class="row">
    <div class="twelve columns">
        <div class="box_c">
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
                            GridLines="None" DataKeyNames="DualTransactionKey,BranchID,Date,Narration,Amount,ApprovedNumber,ApprovedDate,TransactionKey,Voucher_No,StaffName"
                            Width="100%" Style="margin: 0px auto; display: table;">
                            <Columns>
                                <asp:TemplateField HeaderText="Approve">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnReject" runat="server" CausesValidation="false" OnClick="Approve_Click"
                                            ImageUrl="~/Images/like.jpg" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Date" HeaderText="Choosen Date" />
                                <asp:BoundField DataField="StaffName" HeaderText="Staff Name" />
                                <asp:BoundField DataField="Amount" HeaderText="Amount" />
                                <asp:BoundField DataField="Narration" HeaderText="Narration" />
                                <asp:BoundField DataField="ApprovedNumber" HeaderText="Approval Number" />
                                <asp:BoundField DataField="ApprovedDate" HeaderText="Approved Date" />
                                <asp:BoundField DataField="TransactionKey" HeaderText="Receipt Number" Visible="false" />
                                <asp:BoundField DataField="Voucher_No" HeaderText="Amount" Visible="false" />
                                <asp:BoundField DataField="BranchID" HeaderText="Description" Visible="false" />
                            </Columns>
                            <FooterStyle VerticalAlign="Middle" HorizontalAlign="Center" CssClass="GridviewScrollC2Header" />
                            <HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center" CssClass="GridviewScrollC2Header" />
                            <RowStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="GridviewScrollC2Item" />
                            <PagerStyle CssClass="GridviewScrollC2Pager" />
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <asp:Panel CssClass="raised" ID="PnlApprove" runat="server" Visible="false"
        Style="max-height: 200px; min-height: 180px; min-width: 300px; max-width: 500px">
        <asp:Label runat="server" ID="lblHint" Text="" Visible="false"> </asp:Label>
        <div style="top: 0px; height: 50px; text-align: center; padding: auto 0; width: 100%"
            class="boxfooter">
            <asp:Label ID="lblT" runat="server" Text=""> </asp:Label>
        </div>
        <div id="Div1" style="max-height: 400px; min-height: 80px; text-align: center;">
            <br />
            <br />
            <asp:Label ID="lblContent" runat="server" Text="" Style="text-align: justify; vertical-align: middle; margin-left: 10px"> </asp:Label>
            <br />
            <br />
            <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
        </div>
        <div class="boxheader" style="width: 100%; bottom: 0px; height: 50px; position: absolute">
            <asp:Button Width="100" Style="margin: 0 auto" OnClick="btnMsgOK_Click" CssClass="GreenyPushButton"
                ID="btnMsgOK" runat="server" Text="OK" />
        </div>
    </asp:Panel>


    <asp:Panel CssClass="raised" ID="PnlProvide" runat="server" Visible="false" Width="700px"
        Style="max-height: 500px; min-height: 300px">
        <div style="text-align: center;">
            <div class="row">
                <div class="col-md-12">
                    <div class="row">
                        <div class="col-md-8" style="width: 650px;">
                            <div class="panel">
                                <div class="panel-heading custom-header-panel">
                                    <h3 class="panel-title roboto">Business Performance Pay - Approval</h3>
                                </div>
                                <div class="panel-body">
                                    <div class="form-group">
                                        <label class="col-sm-4 control-label" for="name">Credit <span class="requerido">*</span></label>
                                        <div class="col-sm-8">
                                            <asp:TextBox Width="300px" TabIndex="1" ID="txtBranchName" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-4 control-label" for="name">Debit <span class="requerido">*</span></label>
                                        <div class="col-sm-8">
                                            <asp:DropDownList TabIndex="2" Width="300px" ID="accept_ddlMisc"
                                                CssClass="chzn-select" runat="server">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-4 control-label" for="name">Date <span class="requerido">*</span></label>
                                        <div class="col-sm-8">
                                            <asp:TextBox Width="100" TabIndex="1" ID="txtDate" onchange="CheckDate();"
                                                CssClass="input-text maskdate" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-4 control-label" for="name"><span class="requerido"></span></label>
                                        <div class="col-sm-8">
                                            <asp:Label Visible="false" ID="lblTransactionkey" runat="server"> </asp:Label>
                                            <asp:Label Visible="false" ID="lblCrBranchId" runat="server"> </asp:Label>
                                            <asp:Label Visible="false" ID="lblVoucher" runat="server"> </asp:Label>
                                            <asp:Label Visible="false" ID="lblNarration" runat="server"> </asp:Label>
                                            <asp:Label Visible="false" ID="lblamount" runat="server"> </asp:Label>
                                            <asp:Label Visible="false" ID="lblStaffname" runat="server"> </asp:Label>
                                        </div>
                                    </div>


                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="boxheader" style="width: 100%; bottom: 0px; height: 50px; position: absolute">
            <div class="form-group text-center">

                <asp:Button Width="100" Style="margin: 0 auto" CausesValidation="true" ValidationGroup="Rej"
                    OnClick="btnAcceptOK_Click" CssClass="btn btn-orange-md roboto" ID="btnAcceptOK" runat="server"
                    Text="Accept" />
                <asp:Button Width="100" CausesValidation="false" Style="margin: 0 auto" OnClick="btnAcceptCancel_Click" CssClass="btn btn-orange-md roboto"
                    ID="btnAcceptCancel" runat="server" Text="Cancel" />
            </div>
        </div>
    </asp:Panel>
        </div>
    <script type="text/javascript">
        function checkdate(txt) {
            $('#<%=txtDate.ClientID%>').val(txt.value);
        }

        $(document).ready(function () {
            $(".chzn-select").chosen({ search_contains: true });
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

</asp:Content>
