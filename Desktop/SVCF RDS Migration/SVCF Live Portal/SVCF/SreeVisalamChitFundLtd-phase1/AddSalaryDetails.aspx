<%@ Page Culture="en-GB" Title="SVCF Admin Panel" Language="C#" MasterPageFile="~/Branch.Master" EnableEventValidation="false"
    AutoEventWireup="true" CodeBehind="AddSalaryDetails.aspx.cs" Inherits="SreeVisalamChitFundLtd_phase1.SalaryCreate" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Head1" ContentPlaceHolderID="cphHead" runat="server">
    
    <style type="text/css">
        .chzn-results {
            text-align: center;
        }
    </style>
    <script type="text/javascript">
    </script>
    <style type="text/css">
       

        .btn-custom {
            background-color: #0488e8;
            color: #FFF;
            cursor: pointer;
            height: 30px;
            width: 100px;
        }

            .btn-custom:hover {
                background-color: #1f72ae;
                color: #FFF;
            }

            .btn-custom:active {
                background-color: #ff3b3b !important;
                color: #fff;
            }

            .btn-custom:focus {
                background-color: #ff3b3b !important;
                color: #fff;
            }
    </style>
    <link href="Styles/StyleSheet1.css" rel="stylesheet" />
    <link href="Styles/bootstrap.min.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMainContent" runat="server">
  <%--  <ajax:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </ajax:ToolkitScriptManager>--%>
   <%-- <ajax:ToolkitScriptManager ID="test" runat="server"></ajax:ToolkitScriptManager>--%>
    <%--<style type="text/css">
        td {
            vertical-align:top;
            padding: 0px 2px 0px 2px;
        }
    </style>--%>

   <%-- <asp:Panel CssClass="raised" ID="PnlApprove" runat="server" Visible="false"
        Style="max-height: 500px; min-height: 180px; min-width: 300px; max-width: 500px">
        <asp:Label runat="server" ID="Label15" Text="" Visible="false"> </asp:Label>
        <div style="top: 0px; height: 50px; text-align: center; padding: auto 0; width: 100%"
            class="boxfooter">
            <asp:Label ID="lblT" runat="server" Text=""> </asp:Label>
        </div>
        <div id="Div1" style="max-height: 400px; min-height: 80px; text-align: center;">
            <br />
            <br />
            <asp:Label ID="lblContent1" runat="server" Text="" Style="text-align: justify; vertical-align: middle; margin-left: 10px"> </asp:Label>
            <br />
            <br />
            <asp:PlaceHolder ID="DynamicControlsHolder1" runat="server"></asp:PlaceHolder>
        </div>
        <div class="boxheader" style="width: 100%; bottom: 0px; height: 50px; position: absolute">
            <asp:Button Width="100" Style="margin: 0 auto" OnClick="btnMsgOkCancel_Click" CssClass="GreenyPushButton"
                ID="btnMsgOK" runat="server" Text="OK"  />
            <asp:Button Width="100" Style="margin: 0 auto" OnClick="btnMsgOkCancel_Click" CssClass="GreenyPushButton"
                ID="btnMsgCancel" runat="server" Text="Cancel" />
        </div>
    </asp:Panel>--%>
   <%--  <asp:Panel CssClass="row" ID="Panel1" runat="server" DefaultButton="BtnSaveEntry"
        class="content">--%>
    <div class="row">
        <div class="twelve columns">
            <div class="box_c">
                <div class="box_c_heading  box_actions">
                    <p>
                        Salary Details Create
                    </p>
                </div>
                <div class="box_c_content">
                    <div class="row">
                        <asp:Panel runat="server">
                            <div style="width: 100%;">
                                <table class="table table-bordered table-condensed">
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label6" Text="Staff Name" runat="server"></asp:Label>
                                         </td>
                                        <td>
                                            <asp:CheckBox runat="server" ID="chkgetotherbranchlist" Text="Select to Load Other branch Members" AutoPostBack="true" OnCheckedChanged="chkgetotherbranchlist_CheckedChanged" />
                                            <asp:DropDownList runat="server" ID="ddlEmployee" CssClass="chzn-select" TabIndex="1"
                                                CausesValidation="true" Style="width: 100%">
                                            </asp:DropDownList>
                                            <asp:CompareValidator ValidationGroup="salarydetailsgroup" ControlToValidate="ddlEmployee"
                                                ValueToCompare="--select--" Operator="NotEqual" Display="Dynamic" ID="CompareValidator1"
                                                runat="server" ErrorMessage="Select the Employee Name"></asp:CompareValidator>

                                        </td>
                                        <td>
                                            <asp:Label ID="Label9" Text="Date" runat="server"></asp:Label>
                                        <td>
                                        <td>
                                            <asp:TextBox Style="width: 150px" TabIndex="2" ID="txtDate" runat="server" ToolTip="" onchange="CheckDate();" ValidationGroup="salarydetailsgroup"
                                                placeholder="" autoComplete="off" CssClass="twitterStyleTextbox maskdate"></asp:TextBox><br />
                                            <asp:RequiredFieldValidator ForeColor="Red" ID="RequiredFieldValidator6" ErrorMessage="Required!!!"
                                                ControlToValidate="txtDate" ValidationGroup="salarydetailsgroup" Display="Dynamic" runat="server"></asp:RequiredFieldValidator>
                                                <asp:RangeValidator ID="rvDate" EnableClientScript="false" runat="server" 
                                                                 Type="Date" ControlToValidate="txtDate" ValidationGroup="salarydetailsgroup" ForeColor="Red" Display="Dynamic" 
                                                                 ErrorMessage="*"></asp:RangeValidator>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label1" Text="Voucher Number" runat="server"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox Style="width: 150px" TabIndex="3" ID="txtReceiptNumber" runat="server" ToolTip=""
                                                placeholder="" CssClass="input-text ttip_r" ReadOnly="true"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </div>

                            <div class="box_c_heading ">
                                <p>
                                    Credit Details 
                                </p>
                            </div>

                            <div style="width: 100%;">
                                <table class="table table-bordered table-condensed">
                                    <tr>
                                        <td style="vertical-align: top; padding-top: 6px; padding-right: 5px;">
                                            <asp:Label ID="Label2" Text="Select Head" runat="server"></asp:Label>
                                            <asp:DropDownList Style="width: 70%" runat="server" ID="Cr_DDLHead" CssClass="chzn-select" TabIndex="4" onchange="GetChequeNumber();"
                                                CausesValidation="false">
                                            </asp:DropDownList>

                                        </td>

                                        <td style="vertical-align: top; padding-top: 6px; padding-right: 5px;">
                                            <asp:Label ID="Label5" Text="Amount" runat="server"></asp:Label>
                                            <asp:TextBox Style="width: 150px" TabIndex="5" ID="CrtxtAmount" runat="server" ToolTip=""
                                                placeholder="" CssClass="input-text ttip_r"></asp:TextBox>
                                        </td>

                                        <td style="vertical-align: top; padding-top: 6px; padding-right: 5px;">
                                            <asp:Label ID="Label17" Text="Narration" runat="server"></asp:Label>
                                            <asp:TextBox Style="width: 200px" TabIndex="6" ID="CrTxtNarration" runat="server" ToolTip="" TextMode="MultiLine"
                                                placeholder="" CssClass="input-text ttip_r"></asp:TextBox>
                                        </td>
                                        <td style="vertical-align: top; padding-top: 6px; padding-right: 5px;">
                                            <asp:Label ID="Label3" Text="Cheque No" runat="server"></asp:Label>

                                            <asp:TextBox Style="width: 150px" TabIndex="7" ID="txtChequeNumber" runat="server" ToolTip=""
                                                placeholder="" CssClass="input-text ttip_r"></asp:TextBox>
                                        </td>


                                        <td>
                                            <br />
                                            <asp:Button ID="BtnAddCredit" runat="server" Text="Add" TabIndex="8" OnClientClick="return CheckValidation();"
                                                CssClass="btn-custom" Style="margin: 0px auto;" OnClick="BtnAddCredit_Click" />
                                            <br />
                                            <br />
                                            <asp:Button ID="Button2" runat="server" Text="Cancel" TabIndex="9"
                                                CssClass="btn-custom" Style="margin: 0px auto;" />
                                        </td>
                                    </tr>
                                </table>
                                <br />
                                <asp:Label ID="lblcancelmsg" runat="server" Text="" CssClass="lblstyle"></asp:Label>
                                <asp:HiddenField ID="CRDHD_TotalAmount" runat="server" />
                                <div class="row" style="height: 280px">
                                    <div class="col-lg-12 ">
                                        <div class="table-responsive" style="height: 250px; overflow: scroll">
                                            <asp:GridView ID="Credit_GView" BorderStyle="Solid" runat="server" TabIndex="10"
                                                CellSpacing="11" Font-Names="Verdana" ForeColor="#333333" GridLines="Both" Height="15%"
                                                AutoGenerateColumns="false" Width="98%" CssClass="table table-striped table-bordered table-hover"
                                                OnRowDeleting="Credit_GView_RowDeleting">
                                                <RowStyle BackColor="#F7F6F3" />
                                                <RowStyle CssClass="GridViewRowStyle" />
                                                <HeaderStyle CssClass="GridViewHeaderStyle" />
                                                <Columns>
                                                    <asp:BoundField HeaderText="ReceiptNumber" DataField="CrRecptNumber" Visible="true" ItemStyle-Width="7%"
                                                        HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />

                                                    <asp:BoundField HeaderText="Employee Name" DataField="CrEmpName"
                                                        ItemStyle-CssClass="hidden-xs" HeaderStyle-CssClass="hidden-xs" />

                                                    <asp:BoundField HeaderText="Amount" DataField="CrAmount"
                                                        ItemStyle-CssClass="hidden-xs" HeaderStyle-CssClass="hidden-xs" />

                                                    <asp:BoundField HeaderText="Narration" DataField="CrNarration"
                                                        ItemStyle-CssClass="hidden-xs" HeaderStyle-CssClass="hidden-xs" />

                                                    <asp:BoundField HeaderText="Cheqno" DataField="CrCheqno"
                                                        ItemStyle-CssClass="hidden-xs" HeaderStyle-CssClass="hidden-xs" />

                                                    <asp:BoundField HeaderText="HeadName" DataField="CrHead"
                                                        ItemStyle-CssClass="hidden-xs" HeaderStyle-CssClass="hidden-xs" />

                                                    <asp:TemplateField HeaderText="HeadId" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Crlblheadid" runat="server" Text='<%#Eval("CrHead_Id") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="MemberId" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Crlblmemberid" runat="server" Text='<%#Eval("CrMemberId") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField ItemStyle-CssClass="hidden-xs" HeaderStyle-CssClass="hidden-xs" ItemStyle-Width="4%">
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
                                    </div>
                                </div>
                            </div>



                            <div class="box_c_heading" style="margin-top: 5%;">
                                <p>
                                    Debit Details 
                                </p>
                            </div>

                            <div style="width: 100%;">
                                <table class="table table-bordered table-condensed">
                                    <tr>
                                        <td style="vertical-align: top; padding-top: 6px; padding-right: 5px;">
                                            <asp:Label ID="Label24" Text="Select Head" runat="server"></asp:Label>
                                            <asp:DropDownList Style="width: 60%" runat="server" ID="Db_DDLHead" CssClass="chzn-select" TabIndex="11"
                                                OnSelectedIndexChanged="Onselect_headsChanged" AutoPostBack="true" CausesValidation="false">
                                            </asp:DropDownList>
                                            <asp:Label ID="NumberLabel" Text="Approval Number" runat="server"></asp:Label>

                                            <asp:TextBox Style="width: 150px" TabIndex="12" ID="Approval_numberID" runat="server" ToolTip=""
                                                placeholder="" CssClass="input-text ttip_r"></asp:TextBox>

                                        </td>

                                        <td style="vertical-align: top; padding-top: 6px; padding-right: 5px;">
                                            <asp:Label ID="Label26" Text="Amount" runat="server"></asp:Label>

                                            <asp:TextBox Style="width: 150px" TabIndex="13" ID="DbTxtAmount" runat="server" ToolTip=""
                                                placeholder="" CssClass="input-text ttip_r"></asp:TextBox>


                                            <asp:Label ID="Datelabel" Text="Approval Date" runat="server"></asp:Label><br />

                                            <asp:TextBox Style="width: 150px" TabIndex="14" ID="Approval_DateID" runat="server" ToolTip=""
                                                placeholder="" CssClass="twitterStyleTextbox maskdate"></asp:TextBox>
                                        </td>

                                        <td style="vertical-align: top; padding-top: 6px; padding-right: 5px;">
                                            <asp:Label ID="Label27" Text="Narration" runat="server"></asp:Label>
                                            <asp:TextBox Style="width: 200px" TabIndex="15" ID="DbtxtNarration" TextMode="MultiLine" runat="server" ToolTip=""
                                                placeholder="" CssClass="input-text ttip_r"></asp:TextBox>
                                        </td>
                                        <%--<td style="vertical-align: top; padding-top: 6px; padding-right: 5px;">
                                            <asp:Label ID="Label4" Text="Approval Number" runat="server"></asp:Label>

                                            <asp:TextBox Style="width: 150px" TabIndex="1" ID="Approval_numberID" runat="server" ToolTip=""
                                                placeholder="" CssClass="input-text ttip_r"></asp:TextBox>

                                            <asp:Label ID="Label7" Text="Approval Date" runat="server"></asp:Label>

                                            <asp:TextBox Style="width: 150px" TabIndex="1" ID="Approval_DateID" runat="server" ToolTip=""
                                                placeholder="" CssClass="input-text ttip_r"></asp:TextBox>

                                        </td>--%>

                                        <td>
                                            <br />
                                            <asp:Button ID="btnDebitAdd" runat="server" Text="Add" TabIndex="16" OnClientClick="return DbCheckValidation();"
                                                OnClick="btnDebitAdd_Click" CssClass="btn-custom" Style="margin: 0px auto;" />
                                            <br />
                                            <br />
                                            <asp:Button ID="btnDebitCancel" runat="server" Text="Cancel" TabIndex="17"
                                                OnClick="btnDebitCancel_Click" CssClass="btn-custom" Style="margin: 0px auto;" />
                                        </td>
                                    </tr>

                                </table>
                                <div class="row" style="height: 280px">
                                    <div class="col-lg-12">
                                        <div class="table-responsive" style="height: 250px; overflow: scroll">
                                            <asp:GridView ID="Debit_GView" BorderStyle="Solid" runat="server" TabIndex="18"
                                                CellSpacing="11" Font-Names="Verdana" ForeColor="#333333" GridLines="Both" Height="10%"
                                                AutoGenerateColumns="false" Width="900px" CssClass="table table-striped table-bordered table-hover"
                                                OnRowDeleting="Debit_GView_RowDeleting">
                                                <RowStyle BackColor="#F7F6F3" />
                                                <RowStyle CssClass="GridViewRowStyle" />
                                                <HeaderStyle CssClass="GridViewHeaderStyle" />
                                                <Columns>
                                                    <asp:BoundField HeaderText="ReceiptNumber" DataField="DbRecptNumber" Visible="true"
                                                        ItemStyle-Width="7%" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />

                                                    <asp:BoundField HeaderText="Employee Name" DataField="DbEmpName"
                                                        ItemStyle-CssClass="hidden-xs" HeaderStyle-CssClass="hidden-xs" />

                                                    <asp:BoundField HeaderText="Amount" DataField="DbAmount"
                                                        ItemStyle-CssClass="hidden-xs" HeaderStyle-CssClass="hidden-xs" />

                                                    <asp:BoundField HeaderText="Narration" DataField="DbNarration"
                                                        ItemStyle-CssClass="hidden-xs" HeaderStyle-CssClass="hidden-xs" />

                                                    <asp:BoundField HeaderText="Head" DataField="DbHead"
                                                        ItemStyle-CssClass="hidden-xs" HeaderStyle-CssClass="hidden-xs" />


                                                    <asp:TemplateField HeaderText="HeadId" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Dblblheadid" runat="server" Text='<%#Eval("DbHead_Id") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="MemberId" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Dblblmemberid" runat="server" Text='<%#Eval("DbMemberId") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField ItemStyle-CssClass="hidden-xs" HeaderStyle-CssClass="hidden-xs" ItemStyle-Width="4%">
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
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>

                        <table style="margin: 2%;">
                            <tr>
                                <td></td>
                                <td>

                                    <asp:Button ID="BtnSaveEntry" runat="server" Text="Save" TabIndex="19" OnClientClick="return CheckValidation();" ValidationGroup="salarydetailsgroup" CausesValidation="true"
                                        CssClass="btn-custom" Style="margin: 0px auto;" OnClick="btnAdd_Click" />
                                    <asp:Button ID="BtnCancelSave" runat="server" Text="Cancel" TabIndex="20"
                                        CssClass="btn-custom" Style="margin: 0px auto;" />
                                </td>
                            </tr>
                        </table>


                        <asp:HiddenField ID="DBRHD_TotalAmount" runat="server" />

                    </div>
                  <%--  <ajax:ModalPopupExtender ID="ModalPopupExtender1" BackgroundCssClass="modalBackground"
                        TargetControlID="lb" PopupControlID="Pnlmsg" runat="server">
                    </ajax:ModalPopupExtender>
                    <asp:LinkButton ID="lb" Text="" runat="server"></asp:LinkButton>--%>
                </div>
            </div>
        </div>
    </div>
       <%--  </asp:Panel>--%>
    <%--   <asp:LinkButton Text="" runat="server" ID="btnShowPopupCheque"></asp:LinkButton>
     <ajax:ModalPopupExtender ID="MpAll" runat="server" TargetControlID="btnShowPopupCheque"
        PopupControlID="pnlpopup" BackgroundCssClass="modalBackground">
    </ajax:ModalPopupExtender>--%>
     <ajax:ToolkitScriptManager ID="test" runat="server"></ajax:ToolkitScriptManager>
    <asp:LinkButton ID="lnkDummy" runat="server"></asp:LinkButton>
<ajax:ModalPopupExtender ID="ModalPopupExtender2" BehaviorID="mpe" runat="server"
    PopupControlID="pnlPopup" TargetControlID="lnkDummy" BackgroundCssClass="modalBackground" >
</ajax:ModalPopupExtender>
      <%-- <asp:Panel Visible="false" ID="pnlpopup" Style="background: #fff; border: 4px solid #ccc; border: 8px solid rgba(0, 0, 0, 0.5); background-clip: padding-box; -webkit-border-radius: 10px; -moz-border-radius: 10px; border-radius: 10px;position: fixed;top: 35%;width: 900px;left: 50%;margin-left: -450px;z-index: 9999;" CssClass="raised" runat="server">--%>
     <%--<asp:Panel Visible="false" ID="pnlpopup" Style="background: #fff; border: 4px solid #ccc; border: 8px solid rgba(0, 0, 0, 0.5); background-clip: padding-box; -webkit-border-radius: 10px; -moz-border-radius: 10px; border-radius: 10px; min-width: 300px; max-width: 900px;"
        CssClass="raised" runat="server">--%>
    <asp:Panel Visible="false" ID="pnlPopup"
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
     <%--<asp:Panel ID="pnl" Style="background: #fff; border: 4px solid #ccc; border: 8px solid rgba(0, 0, 0, 0.5); background-clip: padding-box; -webkit-border-radius: 10px; -moz-border-radius: 10px; border-radius: 10px; min-width: 300px; max-width: 900px;"
        CssClass="raised" runat="server"
        Visible="false">
        <asp:Label runat="server" ID="Label4" Text="" Visible="false"> </asp:Label>
        <div class=" box_c_heading">
            <asp:Label CssClass="inner_heading" runat="server" ID="lblHD" Text=""> </asp:Label>
        </div>
        <div style="min-height: 20px; overflow: hidden; max-width: 880px; padding: 20px;">
            <div style="min-height: 20px; overflow: auto; max-width: 840px;">
                <br />
                <br />
                <asp:Label runat="server" Style="margin-top: 10px; font-size: 14px;" ID="Label7"
                    Text=""> </asp:Label>
            </div>
        </div>
        <div class=" box_c_heading">
            <div style="float: right;">
                <asp:Button Style="margin: 0 auto" CssClass="GreenyPushButton" ID="Button1" OnClick="btnyes_Click"
                    runat="server" Text="yes" />
                <asp:Button Style="margin: 0 auto" CssClass="GreenyPushButton" ID="Button3" OnClick="btnNo_Click"
                    runat="server" Text="No" />
            </div>
        </div>
    </asp:Panel>--%>
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


        $(document).ready(function () {
            $(".htmlselect").chosen({ search_contains: true });
            prth_mask_input.init();
        });
    </script>
    <script type="text/javascript">


        function CheckValidation() {
            if ($('select[id$="ddlEmployee"]').val() == 0) {
                alert("Please select the Employee Name ");
                return false;
            }
            return true;
        }


        function DbCheckValidation() {
            if ($('select[id$="ddlEmployee"]').val() == 0) {
                alert("Please select the Employee Name ");
                return false;
            }
            return true;
        }


        function GetChequeNumber() {
            var ser = $("#<%=Cr_DDLHead.ClientID%>").find("option:selected").text(); //id name for dropdown list             
            var cid = $("#<%=Cr_DDLHead.ClientID%>").find("option:selected").val(); //text name for dropdown list  
            var rcid = $("#<%=Cr_DDLHead.ClientID%> option:selected").val();

            var bnktrue = rcid.includes(":");
            var chittrue = rcid.includes("|");
            if (bnktrue == true) {
                var rtid = rcid.split(":", 1)
                if (rtid == 3) {
                    $("#<%=txtChequeNumber.ClientID%>").show();
                    $("#<%=Label3.ClientID%>").show();
                }
                else if (rtid == 5) {
                    $("#<%=txtChequeNumber.ClientID%>").hide();
                    $("#<%=Label3.ClientID%>").hide();
                }
        }
        $("#<%=Cr_DDLHead.ClientID%>").addClass('chzn-select');
        }


        $(document).ready(function () {
            $("#<%=txtChequeNumber.ClientID%>").hide();
            $("#<%=Label3.ClientID%>").hide();
        });

    </script>
</asp:Content>
