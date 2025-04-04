<%@ Page Title="" Language="C#" MasterPageFile="~/Branch.Master" AutoEventWireup="true" CodeBehind="DeleteVocherEdit.aspx.cs" Inherits="SreeVisalamChitFundLtd_phase1.DeleteVocherEdit" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">

    <style type="text/css">
        .chzn-results {
            text-align: center;
        }
        /*#ctl00_cphMainContent_ddlChit_chzn .chzn-drop .chzn-search input[type="text"]
        {
            height: 16px;
        }
        .linecolor tr td
        {
            border-top: 1px solid Gray;
            border-bottom: 1px solid Gray;
        }**/

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
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphMainContent" runat="server">

    <div class="panel-body">

        <div class="row">

            <div class="col-md-10 col-md-offset-1">
                <div class="panel panel-default panel-table">
                    <div class="panel-heading">

                        <div class="row">
                            <asp:Label ID="Label1" Text="Choose Date:" runat="server"> </asp:Label>
                            <asp:TextBox Width="100" ValidationGroup="Generate" TabIndex="1" ID="txtDate" CssClass="input-text maskdate" runat="server"></asp:TextBox>
                            <asp:Button ID="Edit" TabIndex="3" runat="server" CssClass="GreenyPushButton" Text="Update" CommandName="ThisBtnClick" OnClick="BtnUpdate_Click" />
                            <asp:Button ID="Back" TabIndex="3" runat="server" CssClass="GreenyPushButton" Text="Back" CommandName="ThisBtnClick" OnClick="BtnBack_Click" />

                        </div>


                    </div>
                    <div class="panel-body">
                        <div class="box_c_heading box_actions">
                            <p>
                                Edit Undo Vocher
                            </p>
                        </div>

                        <div class="box_c_content">
                            <div style="width: 100%; margin: 0 auto;">

                                <%-- <asp:GridView ID="gridBranch" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-list" HeaderStyle-Font-Bold="true" Font-Names="Verdana" ForeColor="#333333"
                                GridLines="None" ShowFooter="True" CellPadding="4" Width="867px" Font-Size="9.2pt" OnPageIndexChanging="gridBranch_PageIndexChanging" AllowPaging="True" Font-Bold="True" PageSize="20">--%>
                                <asp:GridView ID="gridBranch" runat="server" CssClass="Grid" AutoGenerateColumns="False" HeaderStyle-Font-Bold="true" Font-Names="Verdana" ForeColor="#333333"
                                    GridLines="Both" ShowFooter="True" CellPadding="5" CellSpacing="2" Width="70%" Font-Size="9.2pt" AllowPaging="false" Font-Bold="True" PageSize="20" OnRowDataBound="gridBranch_RowDataBound">
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                    <Columns>

                                        <asp:TemplateField HeaderText="ChoosenDate" ItemStyle-HorizontalAlign="Left" Visible="True" ItemStyle-Width="5px">
                                            <ItemTemplate>
                                                <asp:Label ID="lblChoosenDate" runat="server" Text='<%#Eval("ChoosenDate") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" Width="5px"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Voucher_No" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="100px">
                                            <ItemTemplate>
                                                <asp:Label ID="lblVoucher_No" runat="server" Text='<%#Eval("Voucher_No")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" Width="5px"></ItemStyle>
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Series" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="400px">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSeries" runat="server" Text='<%#Eval("Series")%>'></asp:Label>
                                            </ItemTemplate>

                                            <ItemStyle Width="400px"></ItemStyle>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Head" ItemStyle-Width="100px">
                                            <ItemTemplate>
                                                <asp:Label ID="lblHead" runat="server" Text='<%#Eval("Head")%>' Visible="false"></asp:Label>
                                                <%--<asp:DropDownList ID="ddlHeads" runat="server"></asp:DropDownList>--%>
                                                <asp:DropDownList CssClass="chzn-select" Style="width: 350px !important;" AutoPostBack="true"
                                                    TabIndex="58" ValidationGroup="GrpRow" ID="ddlHeads" 
                                                    runat="server">
                                                </asp:DropDownList>
                                                <asp:CompareValidator Display="Dynamic" ValidationGroup="GrpRow" Operator="NotEqual"
                                                    ControlToValidate="ddlHeads" ID="CompareValidator2" ValueToCompare="--Select--"
                                                    ErrorMessage="*" runat="server"> </asp:CompareValidator>
                                            </ItemTemplate>
                                            <ItemStyle Width="400px"></ItemStyle>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Amount" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="400px">
                                            <ItemTemplate>
                                                <%--<asp:Label ID="IblAmount" runat="server" Text='<%#Eval("Amount")%>'></asp:Label>--%>
                                                <asp:TextBox ID="txtAmount" runat="server" Height="40px" Text='<%#Eval("Amount") %>'></asp:TextBox>
                                            </ItemTemplate>

                                            <ItemStyle Width="400px"></ItemStyle>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Narration" ItemStyle-Width="200px">
                                            <ItemStyle Wrap="true" Width="100px" />
                                            <ItemTemplate>
                                                <%--<asp:TextBox ID="lbl_Narration" runat="server" Text='<%#Eval(" <asp:TextBox ID="TextBox3" runat="server" Visible='<%# IsInEditMode %>' Text='<%# Bind("unit") %>'></asp:TextBox>") %>'></asp:TextBox>  --%>
                                                <asp:TextBox ID="TextBox3" runat="server" Height="200px" Text='<%# Eval("Narration") %>' TextMode="MultiLine"></asp:TextBox>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" Width="100px" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="TransationType" ItemStyle-Width="400px">
                                            <ItemTemplate>
                                                <asp:Label ID="lblTransationType" runat="server" Text='<%#Eval("TransationType")%>'></asp:Label>
                                            </ItemTemplate>

                                            <ItemStyle Width="400px"></ItemStyle>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="TransactionKey" ItemStyle-Width="400px" Visible="false">
                                            <ItemTemplate>
                                                <asp:TextBox ID="TextBox7" Visible="false" runat="server" Text='<%# Bind("TransactionKey") %>'></asp:TextBox>
                                            </ItemTemplate>

                                            <ItemStyle Width="400px"></ItemStyle>
                                        </asp:TemplateField>

                                    </Columns>
                                    <EditRowStyle BackColor="#999999" />
                                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />

                                    <HeaderStyle Font-Bold="True" BackColor="#5D7B9D" ForeColor="White"></HeaderStyle>
                                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" Width="20px" />
                                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>


            </div>
        </div>
    </div>

    <%--  <asp:LinkButton runat="server" ID="show" Text=""></asp:LinkButton>
    <ajax:ModalPopupExtender ID="ModalPopupExtender1" TargetControlID="show" PopupControlID="pnlConfirmation"
        BackgroundCssClass="modalBackground" runat="server">
    </ajax:ModalPopupExtender>--%>
    <asp:Panel CssClass="raised" ID="pnlConfirmation" runat="server" Visible="false" Width="100%"
        Style="min-height: 100px; min-width: 300px; max-width: 900px; top: 0px; max-width: 900px; max-height: 500px; overflow-y: scroll;">
        <asp:Label runat="server" ID="lblHintConfirmation" Text="" Visible="false"> </asp:Label>
        <asp:Label runat="server" ID="lblChoosenDate" Text="" Visible="false"></asp:Label>
        <div class="boxheader">
            <asp:Label runat="server" ID="lblHeadingConfirmation" Text=""> </asp:Label>
        </div>
        <div style="min-height: 100px; text-align: center; padding-left: 10px; padding-right: 10px;">
            <br />
            <asp:Label runat="server" ID="lblContentConfirmation" Text="Please Confirm Your Transaction???"> </asp:Label>
            <%--   <asp:GridView ID="gvConfirm" Width="100%" runat="server" AutoGenerateColumns="true" Height="500px" AlternatingRowStyle-Width="600px"
                HeaderStyle-BackColor="#61A6F8" HeaderStyle-Font-Bold="true" HeaderStyle-ForeColor="White"
                PageSize="20" BackColor="White" BorderWidth="1px" CellPadding="4" BorderColor="#DEDFDE"
                BorderStyle="None" CssClass="aspxtable" ForeColor="Black" GridLines="None">
                <RowStyle BackColor="#F7F7DE" />
                <HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                <RowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
             
            </asp:GridView>--%>
            <br />
        </div>
        <div class="boxheader" style="margin: 0 auto;">
            <%--  <asp:Button Width="100" Style="margin: 0 auto" CssClass="GreenyPushButton" ID="Button1"
                OnClick="btnyes_Click" runat="server" Text="yes" />
            <asp:Button Width="100" Style="margin: 0 auto" CssClass="GreenyPushButton" ID="Button2"
                OnClick="btnno_Click" runat="server" Text="No" />--%>
        </div>
        <%--<asp:ImageButton ID="btnYes" OnClick="btnYes_Click" runat="server" ImageUrl="~/Images/btnyes.jpg"/>
<asp:ImageButton ID="btnNo" runat="server" ImageUrl="~/Images/btnNo.jpg" />--%>
    </asp:Panel>
    <script type="text/javascript">
        $(document).ready(function () {
            $(".chzn-select").chosen({ search_contains: true });
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
            checkWindow();
        });

        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $(".chzn-select").chosen({ search_contains: true });
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
            prth_tips.init();
        });
    </script>

</asp:Content>
