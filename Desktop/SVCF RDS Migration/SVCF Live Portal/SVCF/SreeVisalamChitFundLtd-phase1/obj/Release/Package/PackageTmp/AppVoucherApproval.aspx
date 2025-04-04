<%@ Page Title="SVCF Admin Panel" Language="C#" Culture="en-GB" AutoEventWireup="true" CodeBehind="AppVoucherApproval.aspx.cs"
    MasterPageFile="~/Branch.Master" Inherits="SreeVisalamChitFundLtd_phase1.AppVoucherApproval" %>


<%@ Register TagName="Approvel" TagPrefix="Appvoc" Src="~/UserControl/Approvedvoucher.ascx" %>

<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxMenu" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1.Export, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView.Export" TagPrefix="dx" %>


 

<asp:Content ID="Head1" ContentPlaceHolderID="cphHead" runat="server">
    <link href="Styles/aspxtable.css" rel="stylesheet" type="text/css" />
   <style>
        /*.aligned td
        {
            padding-left: 10px;
            padding-right: 10px;
        }
        #ctl00_cphMainContent_ddlChitGroup_chzn .chzn-drop .chzn-search input[type="text"]
        {
            height: 15px;
        }*/

        .glyphicon {
            margin-right: 5px;
        }

        .glyphicon-new-window {
            margin-left: 5px;
        }



        /*.body {
            margin-top: 20px;
        }*/

        .panel-body:not(.two-col) {
            padding: 0px;
        }

        .panel-body .radio, .panel-body .checkbox {
            margin-top: 0px;
            margin-bottom: 0px;
        }

        .panel-body .list-group {
            margin-bottom: 0;
        }

        .margin-bottom-none {
            margin-bottom: 0;
        }

        .panel-body .radio label, .panel-body .checkbox label {
            display: block;
        }
        .well-sm {
            height: 42px !important;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMainContent" runat="server">

    <ajax:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </ajax:ToolkitScriptManager>

    <asp:Button ID="btnShowPopup" runat="server" Style="display: none" />
    <ajax:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="btnShowPopup"
        PopupControlID="PnlApprove" BackgroundCssClass="modalBackground">
    </ajax:ModalPopupExtender>

    <div class="row">
      <table>
        <tr>
          <td> <asp:RadioButton ID="RadApprove" runat="server" Text="Approve App Voucher" OnCheckedChanged="RadApprove_CheckedChanged"  AutoPostBack="True" /></td>
          <td></td><td></td>
          <td> <asp:RadioButton ID="RadView" runat="server" Text="View Approved Voucher" OnCheckedChanged="RadView_CheckedChanged" AutoPostBack="True" /> </td>
        </tr>       
      </table>
    </div>

    <div id="Appvoucherapproval" runat="server" visible="false" >

    <div class="row">
        <div class="twelve columns">
            <div class="box_c">
                <div class="box_c_heading  box_actions">
                    <p>AppVoucherApproval</p>
                </div>
                <div class="twelve columns centered">
                    <div style="margin: 0px auto; width: 100%; overflow: auto !important;">
                        <br />
                        <asp:GridView ID="Gridview1" BorderStyle="Solid" runat="server"
                            CellSpacing="3" Font-Names="Verdana" ForeColor="#333333" GridLines="None" Height="100px" TabIndex="15"
                            DataKeyNames="DualTransactionKey,ChoosenDate,ReceiptNumber,Series,MemberID,ChitGroupID,CollectedBranchID,Token,Description,Amount,AppReceiptno,TransactionKey,BranchName"
                            AutoGenerateColumns="false" CssClass="Trans twelve columns" Width="900px">
                            <RowStyle BackColor="#F7F6F3" />
                            <RowStyle CssClass="GridViewRowStyle" />
                            <HeaderStyle CssClass="GridViewHeaderStyle" />
                            <Columns>
                                <asp:TemplateField HeaderText="Approve">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnimg" runat="server" OnClick="Approve_Click" ImageUrl="~/Images/thumb.png" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="BranchName" HeaderText="Branch Name" />
                                <asp:BoundField DataField="ChoosenDate" HeaderText="Choosen Date" />
                                <asp:BoundField DataField="GROUPNO" HeaderText="Group" />
                                <asp:BoundField DataField="GrpMemberID" HeaderText="Token" />
                                <asp:BoundField DataField="CustomerName" HeaderText="Customer Name" />
                                <asp:BoundField DataField="Series" HeaderText="Series" />
                                <asp:BoundField DataField="ReceiptNumber" HeaderText="Receipt Number" />
                                <asp:BoundField DataField="Amount" HeaderText="Amount" />
                                <asp:BoundField DataField="CustomersBankName" HeaderText="CustomersBankName" />
                                <asp:BoundField DataField="DateInCheque" HeaderText="DateInCheque" />
                                <asp:BoundField DataField="ChequeDDNO" HeaderText="ChequeDDNO" />
                                <asp:BoundField DataField="AppReceiptno" HeaderText="AppReceiptno" />
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

    <div class="panel panel-primary" id="PnlProvide" runat="server" style="display:none;">
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
                <div class="col-md-6">
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
                </div>
            </div>
        </div>
        <div class="panel-footer">
            <asp:Button ID="BtnOk" runat="server" CssClass="btn btn-success btn-sm" OnClick="btnAcceptOK_Click" Text="OK" />            
            <asp:Button ID="BtnCancel" runat="server" CssClass="btn btn-primary btn-sm" OnClick="btnAcceptCancel_Click" Text="Cancel" />            
        </div>
    </div>


    <asp:Panel CssClass="raised" ID="PnlApprove" runat="server" Visible="false"
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
    </asp:Panel>
  </div>
  

    <div id="Viewapproved" runat="server" visible="false" class="row">
        <div class="box_c_heading  box_actions">
       <p>View Approved</p>
    </div>
    <div class="box_c_content">
      <Appvoc:Approvel ID="approvalview" runat="server" />
     </div>
    </div>

    <script type="text/javascript">
        $(document).ready(function () {
            $(".chzn-select").chosen({ search_contains: true });
            //            $(".sp_floats").spinner({
            //                decimals: 2,
            //                stepping: 0.50,
            //                min: 0.00
            //            });

            $(".sp_number").numeric({
                decimal: false,
                negative: false
            },
                function () {
                    alert("Positive integers only");
                    this.value = "";
                    this.focus();
                });
            $(".sp_float").numeric({
                negative: false
            },
                function () {
                    alert("Positive only");
                    this.value = "";
                    this.focus();
                });
            prth_mask_input.init();
        });
    </script>
</asp:Content>
