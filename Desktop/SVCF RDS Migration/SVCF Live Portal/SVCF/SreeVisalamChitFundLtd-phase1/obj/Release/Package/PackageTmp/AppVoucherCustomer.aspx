<%@ Page Title="" Language="C#" MasterPageFile="~/Branch.Master" AutoEventWireup="true" CodeBehind="AppVoucherCustomer.aspx.cs" Inherits="SreeVisalamChitFundLtd_phase1.AppVoucherCustomer" %>

<%@ Register TagName="Approval" TagPrefix="Appvoc" Src="~/UserControl/Approvedvoucher.ascx" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>


    <div class="row">
        <div style="display: table-cell; vertical-align: top; padding-top: 8px;">
            <table>
                <tr>
                    <td>
                        <asp:Label ID="lblFrom" runat="server" Text="From:"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtFrom" runat="server" CssClass="input-text maskdate" Width="100px"></asp:TextBox>
                    </td>
                    <td></td>
                    <td>
                        <asp:Label ID="lblTo" runat="server" Text="To:"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtTo" runat="server" CssClass="input-text maskdate" Width="100px"></asp:TextBox>
                    </td>
                </tr>
            </table>
            <br />
<%--            <table>
                <tr>
                    <td>
                        <asp:Label ID="lblBranch" runat="server" Text="Select Branch "></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlBranch" runat="server" CssClass="chzn-select" Width="200px" AutoPostBack="true" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged"></asp:DropDownList>
                    </td>
                    <td></td>
                    <td>
                        <asp:Label ID="lblMoneyCollector" runat="server" Text="Select Money Collector "></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlMoneyCollector" runat="server" CssClass="chzn-select" Width="200px" AutoPostBack="true" OnSelectedIndexChanged="ddlMoneyCollector_SelectedIndexChanged"></asp:DropDownList>
                        <asp:CompareValidator ID="CompareValidator2" runat="server" ErrorMessage="*" ControlToValidate="ddlMoneyCollector" ValueToCompare="--Select--"
                            Display="Dynamic" Operator="NotEqual"></asp:CompareValidator>
                    </td>
                    <td></td>
                    <td>
                        <asp:Label ID="lblSeries" runat="server" Text="Select Mode"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="DropDownList1" runat="server" CssClass="chzn-select" Width="200px" AutoPostBack="true">
                        </asp:DropDownList>
                        <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="*" ValueToCompare="--Select--" ControlToValidate="ddlSeries"
                             Display="Dynamic" Operator="NotEqual"></asp:CompareValidator>
                    </td>
                </tr>
            </table>--%>
        </div>

        <table>
            <tr>
                <td>
                    <asp:RadioButton ID="RadApprove" runat="server" Text="Approve App Voucher" GroupName="Approvals" AutoPostBack="True" OnCheckedChanged="RadApprove_CheckedChanged" /></td>
                <td></td>
                <td></td>
                <td>
                    <asp:RadioButton ID="RadView" runat="server" Text="View Approved Voucher" GroupName="Approvals" AutoPostBack="True" OnCheckedChanged="RadApprove_CheckedChanged" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblSeriesSelect" runat="server" Text="Select Series"></asp:Label>
                </td>
                <td></td>
                <td></td>
                <td>
                    <asp:DropDownList ID="ddlSeries" runat="server" CssClass="chzn-select" Width="200px" AutoPostBack="true">
                        <asp:ListItem Value="--Select--">--Select--</asp:ListItem>
                        <asp:ListItem Value="CPAPP">Customer App</asp:ListItem>
                        <asp:ListItem Value="CPWEB">Website</asp:ListItem>
                    </asp:DropDownList>


                </td>
                <td></td>
                <td>
                    <asp:Button ID="btnSelect" runat="server" Text="GO" CssClass="GreenyPushButton" OnClick="btnSelect_Click" />
                </td>
            </tr>
        </table>
        <asp:ImageButton ID="imgexport" runat="server" ImageUrl="~/Styles/Image/document_export.png" OnClick="imgexport_Click"
                    Height="33px" Width="30px" Visible="false" />
    </div>

    <div id="Appvoucherapproval" runat="server" visible="false">
        <div class="row">
            <div class="twelve columns">
                <div class="box_c">
                    <div class="box_c_heading  box_actions">
                        <p>AppVoucherApproval</p>
                    </div>
                    <div class="twelve columns centered">
                        <div style="margin: 0px auto; width: 100%; overflow: auto !important;">
                            <br />
                        </div>
                        <asp:GridView ID="Gridview1" BorderStyle="Solid" runat="server"
                            CellSpacing="3" Font-Names="Verdana" ForeColor="#333333" GridLines="None" Height="100px" TabIndex="15"
                            DataKeyNames="DualTransactionKey,Series,ChoosenDate,M_Id,ChitGroupId,MoneyCollId,Amount,AppReceiptno,TransactionKey,BranchName,Head_Id,CurrDate,Interest,ChitNo,BranchID,Type"
                            AutoGenerateColumns="false" CssClass="Trans twelve columns" Width="900px" AutoPostBack="true">
                            <RowStyle BackColor="#F7F6F3" />
                            <RowStyle CssClass="GridViewRowStyle" />
                            <HeaderStyle CssClass="GridViewHeaderStyle" />
                            <Columns>
                                <asp:TemplateField HeaderText="Approve">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnImg" runat="server" ImageUrl="~/Images/thumb.png" OnClick="Approve_Click" />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Reject" Visible="false">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnCancel" runat="server" ImageUrl="~/Images/Reject.png" OnClick="Reject_Click" />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:BoundField DataField="CurrDate" HeaderText="Date" />
                                <%--<asp:BoundField DataField="VouTime" HeaderText="Time" />--%>
                                <asp:BoundField DataField="AppReceiptno" HeaderText="AppReceipt No" />
                                <asp:BoundField DataField="ChitNo" HeaderText="Chit Number" />
                                <asp:BoundField DataField="Amount" HeaderText="Amount" />
                                <asp:BoundField DataField="Type" HeaderText="Type" />
                            </Columns>
                            <FooterStyle VerticalAlign="Middle" HorizontalAlign="Center" CssClass="GridviewScrollC2Header" />
                            <HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center" CssClass="GridviewScrollC2Header" />
                            <RowStyle VerticalAlign="Middle" HorizontalAlign="Center" CssClass="GridviewScrollC2Item" />
                            <PagerStyle CssClass="GridviewScrollC2Pager" />
                        </asp:GridView>
                    </div>
                    <br />
                    <div style="float: right;">
                        <table>
                            <tr>
                                <td>
                                    <asp:Label ID="lblTotal" runat="server" Text="Total" Visible="false" Font-Bold="true" Font-Size="Large"></asp:Label>
                                </td>
                                <td></td>
                                <td>
                                    <asp:Label ID="lblTotalAmt" runat="server" Visible="false" Font-Bold="true" Font-Size="Large"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </div>


                    <ajax:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="show" PopupControlID="msgbox" BackgroundCssClass="modalBackground">
                    </ajax:ModalPopupExtender>
                    <asp:LinkButton ID="show" runat="server"></asp:LinkButton>
                    <div id="msgbox" style="display: none; background: #fff; border: 4px solid #ccc; border: 8px solid rgba(0, 0, 0, 0.5); background-clip: padding-box; -webkit-border-radius: 10px; -moz-border-radius: 10px; border-radius: 10px; min-width: 300px; max-width: 900px;"
                        cssclass="raised" runat="server">
                        <div class=" box_c_heading">
                            <span class="inner_heading" style="text-align: center;">Confirmation </span>
                        </div>
                        <div>
                            <table>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblDate" runat="server" Text="Approval date :"></asp:Label>
                                    </td>
                                    <td>
                                        <%--<asp:TextBox ID="txtAppDate" runat="server" CssClass="input-text maskdate"></asp:TextBox>--%>
                                        <asp:Label ID="lblAppDate" runat="server" Visible="false"></asp:Label>
                                        <%--20/07/2021 --%>
                                        <asp:TextBox ID="txtReason" runat="server" Visible="false"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="aa" ControlToValidate="txtReason" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div style="min-height: 40px; overflow: auto; max-width: 840px;">
                            <asp:Label ID="lblContent" runat="server"></asp:Label>
                        </div>
                        <div class="box_c_heading">
                            <div style="float: right;">
                                <asp:Button ID="btnConfirm" runat="server" CssClass="GreenyPushButton" Text="Confirm" OnClick="btnConfirm_Click" />
                                <asp:Button ID="btnRejectConfirm" runat="server" CssClass="GreenyPushButton" Text="Reject" CausesValidation="true" ValidationGroup="aa" OnClick="btnRejectConfirm_Click" Visible="false" />
                                <asp:Button ID="Button1" runat="server" CssClass="GreenyPushButton" Text="Cancel" OnClick="btnCancel_Click" />
                            </div>
                        </div>
                    </div>

                    <div id="warning" style="display: none; background: #fff; border: 4px solid #ccc; border: 8px solid rgba(0, 0, 0, 0.5); background-clip: padding-box; -webkit-border-radius: 10px; -moz-border-radius: 10px; border-radius: 10px; min-width: 300px; max-width: 900px;"
                        cssclass="raised" runat="server">
                        <div class=" box_c_heading">
                            <span class="inner_heading" style="text-align: center;">Confirmation </span>
                        </div>
                        <div style="min-height: 40px; overflow: auto; max-width: 840px;">
                            <asp:Label ID="lblWarning" runat="server"></asp:Label>
                        </div>
                        <div class="box_c_heading">
                            <div style="float: right;">
                                <asp:Button ID="btnOk" runat="server" CssClass="GreenyPushButton" Text="Ok" OnClick="btnOk_Click" />
                            </div>
                        </div>

                    </div>
                </div>
            </div>
        </div>

    </div>

    <div id="ViewApproved" runat="server" visible="false" class="row">
        <div class="box_c_heading box_actions">
            <p>Approved Vouchers </p>
        </div>
        <div class="twelve columns centered">
            <Appvoc:Approval ID="approvalview" runat="server" />
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

</asp:Content>
