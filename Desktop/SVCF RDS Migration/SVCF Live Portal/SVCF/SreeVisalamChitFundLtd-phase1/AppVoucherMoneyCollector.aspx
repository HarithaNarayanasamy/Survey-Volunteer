<%@ Page Title="" Language="C#" MasterPageFile="~/Branch.Master" AutoEventWireup="true" CodeBehind="AppVoucherMoneyCollector.aspx.cs" Inherits="SreeVisalamChitFundLtd_phase1.AppVoucherMoneyCollector" %>

<%@ Register TagName="Approval" TagPrefix="Appvoc" Src="~/UserControl/Approvedvoucher.ascx" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <script type="text/javascript">

        function clearValidationErrors() {
            //Hide all validation errors
            if (window.Page_Validators)
                for (var vI = 0; vI < Page_Validators.length; vI++) {
                    var vValidator = Page_Validators[vI];
                    vValidator.isvalid = true;
                    ValidatorUpdateDisplay(vValidator);
                }
            //Hide all validaiton summaries
            if (typeof (Page_ValidationSummaries) != "undefined") { //hide the validation summaries
                for (sums = 0; sums < Page_ValidationSummaries.length; sums++) {
                    summary = Page_ValidationSummaries[sums];
                    summary.style.display = "none";
                }
            }
        }

    </script>

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


            <table>
                <tr>
                    <td>
                        <asp:RadioButton ID="RadApprove" runat="server" Text="Approve App Voucher" GroupName="radiobtn" AutoPostBack="True" OnCheckedChanged="RadApprove_CheckedChanged" /></td>
                    <td></td>
                    <td></td>
                    <td>
                        <asp:RadioButton ID="RadView" runat="server" Text="View Approved Voucher" GroupName="radiobtn" AutoPostBack="True" OnCheckedChanged="RadView_CheckedChanged" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblMoneyColl" runat="server" Text="Select Money Collector "></asp:Label>
                    </td>
                    <td></td>
                    <td></td>
                    <td>
                        <asp:DropDownList ID="ddlMoneyColl" runat="server" CssClass="chzn-select" Width="200px" AutoPostBack="true"></asp:DropDownList>
                    </td>
                    <td></td>
                    <td>
                        <asp:Button ID="btnSelect" runat="server" Text="GO" CssClass="GreenyPushButton" OnClick="btnSelect_Click" />
                    </td>
                </tr>
            </table>
            <%--<div style="display: table-cell; vertical-align: top; float: right; padding-right: 7px; text-align: right; margin-top: -35px;">--%>
                <asp:ImageButton ID="imgexport" runat="server" ImageUrl="~/Styles/Image/document_export.png" OnClick="imgexport_Click"
                    Height="33px" Width="30px" Visible="false" />
            <%--</div>--%>
        </div>
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
                            AutoGenerateColumns="false" CssClass="Trans twelve columns" Width="900px">
                            <RowStyle BackColor="#F7F6F3" />
                            <RowStyle CssClass="GridViewRowStyle" />
                            <HeaderStyle CssClass="GridViewHeaderStyle" />
                            <Columns>
                                <asp:TemplateField HeaderText="Approve">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnImg" runat="server" ImageUrl="~/Images/thumb.png" OnClick="Approve_Click" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%--20/07/2021--%>
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
                                        <asp:Label ID="lblDate" runat="server"></asp:Label>
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
                        <%-- <div style="min-height: 20px; max-height: 200px; overflow: hidden; max-width: 880px;
                        padding: 20px;">
                                <div id="dialogue" align="center">
                                    Do you want to approve this voucher?
                                </div>
                            </div>--%>
                        <div class="box_c_heading">
                            <div style="float: right;">
                                <asp:Button ID="btnConfirm" runat="server" CssClass="GreenyPushButton" Text="Confirm" OnClick="btnConfirm_Click" Visible="false" />
                                <asp:Button ID="btnRejectConfirm" runat="server" CssClass="GreenyPushButton" Text="Reject" CausesValidation="true" ValidationGroup="aa" OnClick="btnRejectConfirm_Click" Visible="false" />
                                <asp:Button ID="Button1" runat="server" CssClass="GreenyPushButton" Text="Cancel" OnClick="btnCancel_Click" CausesValidation="false" />
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

        <%--        <div class="panel panel-primary" id="PnlProvide" runat="server" style="display: none;">
            <div class="panel-heading">
                <h3 class="panel-title">
                    <span class="glyphicon glyphicon-circle-arrow-right"></span>App Voucher Approval</h3>
            </div>
            <div class="panel-body two-col">
                <div class="row">
                    <div class="col-md-6">
                        <div class="well well-sm">
                            <div class="checkbox">
                                <asp:Label ID="Label1" Text="Date" runat="server"> </asp:Label>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="well well-sm">
                            <div class="checkbox">
                                <asp:TextBox TabIndex="1" ID="txtDate" class="input-text maskdate" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ForeColor="Red" ID="RequiredFieldValidator6" ErrorMessage="Rquired!!!"
                                    ControlToValidate="txtDate" ValidationGroup="Rej" Display="Dynamic" runat="server"></asp:RequiredFieldValidator>
                                <asp:CompareValidator ForeColor="Red" Type="Date" ID="CompareValidator11" ValidationGroup="Rej"
                                    ControlToValidate="txtDate" Display="Dynamic" Operator="DataTypeCheck" runat="server"
                                    ErrorMessage="Incorrect!!!"></asp:CompareValidator>
                                <asp:RangeValidator ID="rvDate" EnableClientScript="false" runat="server"
                                    Type="Date" ControlToValidate="txtDate" ValidationGroup="Rej" ForeColor="Red" Display="Dynamic"
                                    ErrorMessage="*"></asp:RangeValidator>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-6">
                        <div class="well well-sm margin-bottom-none">
                            <div class="checkbox">
                                <asp:Label ID="Label2" Text="Bank Head" runat="server"> </asp:Label>
                            </div>
                        </div>
                    </div>
<%--                    <div class="col-md-6">
                        <div class="well well-sm margin-bottom-none">
                            <div class="checkbox">
                                <asp:DropDownList ID="ddlBankHead" CssClass="chzn-search form-control" runat="server" Style="height: 28px !important; margin-bottom: 0px !important; max-width: 100% !important;"></asp:DropDownList>
                                <asp:Label Visible="false" ID="lbldual" runat="server"> </asp:Label>
                                <asp:Label Visible="false" ID="lblchoosendate" runat="server"> </asp:Label>
                                <asp:Label Visible="false" ID="lblapprec" runat="server"> </asp:Label>
                                <asp:Label Visible="false" ID="lblSeries" runat="server"> </asp:Label>
                                <asp:Label Visible="false" ID="lblVoucher" runat="server"> </asp:Label>
                                <asp:Label Visible="false" ID="lblGroupID" runat="server"> </asp:Label>
                                <asp:Label Visible="false" ID="lblGrpMemID" runat="server"> </asp:Label>
                            </div>
                        </div>
                    </div>--%>
        <%--         </div>
            </div>
            <div class="panel-footer">
                <asp:Button ID="BtnOk" runat="server" CssClass="btn btn-success btn-sm" OnClick="btnAcceptOK_Click" Text="OK" />
                <asp:Button ID="BtnCancel" runat="server" CssClass="btn btn-primary btn-sm" OnClick="btnAcceptCancel_Click" Text="Cancel" />
            </div>
        </div>--%>

        <%--        <asp:Panel CssClass="raised" ID="PnlApprove" runat="server" Visible="false"
            Style="max-height: 500px; min-height: 180px; min-width: 300px; max-width: 500px">
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
                <asp:PlaceHolder ID="DynamicControlsHolder" runat="server"></asp:PlaceHolder>
            </div>
            <div class="boxheader" style="width: 100%; bottom: 0px; height: 50px; position: absolute">
                <asp:Button Width="100" Style="margin: 0 auto" CssClass="GreenyPushButton"
                    ID="btnMsgOK" runat="server" Text="OK" />
                <asp:Button Width="100" Style="margin: 0 auto" CssClass="GreenyPushButton"
                    ID="btnMsgCancel" runat="server" Text="Cancel" />
            </div>
        </asp:Panel>--%>
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
