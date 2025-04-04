<%@ Page Culture="en-GB" Language="C#" MasterPageFile="~/Branch.Master" AutoEventWireup="true"
    CodeBehind="DeleteVoucher.aspx.cs" Inherits="SreeVisalamChitFundLtd_phase1.DeleteVoucher"
    Title="SVCF Admin Panel" %>

<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1.Export, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView.Export" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxEditors.v11.1" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxGridView.v11.1" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <style type="text/css">
        td[style="cursor:default;"]
        {
            vertical-align:middle !important;
        }
        .auto-style1 {
            width: 1225px;
            height: 57px;
        }
        .auto-style2 {
            margin-top: 0px;
        }
        .auto-style3 {
            height: 57px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMainContent" runat="server">
    <ajax:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </ajax:ToolkitScriptManager>
    <script type="text/javascript">
    // <![CDATA[
        function grid_SelectionChanged(s, e) {
            s.GetSelectedFieldValues("ContactName", GetSelectedFieldValuesCallback);
        }
        function GetSelectedFieldValuesCallback(values) {
            selList.BeginUpdate();
            try {
                selList.ClearItems();
                for (var i = 0; i < values.length; i++) {
                    selList.AddItem(values[i]);
                }
            } finally {
                selList.EndUpdate();
            }
            document.getElementById("selCount").innerHTML = grid.GetSelectedRowCount();
        }
      // ]]> 
    </script>
    <div class="row">
        <div class="twelve columns">
            <div class="box_c">
                <div class="box_c_heading  box_actions">
                    <p>
                        Undo Voucher</p>
                </div>
                <div class="box_c_content">
                    <asp:Panel runat="server" DefaultButton="btnLoad">
                        <table>
                            <tr>
                                <td style="vertical-align:top;padding-top:6px;" class="auto-style3">
                                    <asp:Label ID="ASPxLabel1" runat="server" Text="Choose Date :">
                                    </asp:Label>
                                </td>
                                <td style="padding-left: 4px;vertical-align:top;" class="auto-style3">
                                    <asp:TextBox TabIndex="1" Width="100px" CssClass="input-text maskdate" ID="deChoosenDate"
                                        runat="server">
                                    </asp:TextBox>                                                                              
                                      <br />                                              

                                </td>
                                <td style="padding-left: 4px;vertical-align:top;" class="auto-style3">
                                    <asp:TextBox TabIndex="2" Width="100px" CssClass="input-text maskdate" ID="txtToDate"
                                        runat="server">
                                    </asp:TextBox>                                      
                                      <br />

                                </td>
                                <td style="padding-left: 4px;vertical-align:top;" class="auto-style3">
                                    <asp:Button TabIndex="2" ID="btnLoad" CssClass="GreenyPushButton" runat="server"
                                        OnClick="btnLoad_click" Text="Load"></asp:Button>
                                </td>
                                <td style="padding-left: 4px;vertical-align:top;" class="auto-style3">
                                    <asp:Button TabIndex="3" ID="ASPxButton1" runat="server" OnClick="btnDelete_click"
                                        CssClass="GreenyPushButton" Text="Delete" Visible="False"></asp:Button>
                                </td>
                                  <td style="padding-left: 4px;vertical-align:top;" class="auto-style3">                              
                                        <asp:Button ID="Edit"  TabIndex="3" runat="server" CssClass="GreenyPushButton" 
                                            Text="Edit"  CommandName="ThisBtnClick" OnClick="btnEdit_click" />
                                </td>
                                 <td class="auto-style1">
                                        <dx:ASPxButton ID="btnExportExcel" Text="ExportExcel"  runat="server" Visible="true"

                            ImageSpacing="0px" EnableTheming="False" EnableDefaultAppearance="True" ToolTip="Export to Excel"

                              Cursor="pointer" OnClick="btnExportExcel_Click1">
                           
                           </dx:ASPxButton>
                                        <asp:Label ID="Label1" runat="server" Text="Reason : " Visible="False"></asp:Label>
                                        <asp:TextBox ID="txt_Reason" runat="server" CssClass="auto-style2" Width="309px" Visible="False"></asp:TextBox>
                                       <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" controltovalidate ="txt_Reason" display ="Dynamic" ErrorMessage="Enter Reason"></asp:RequiredFieldValidator>--%>
                        <%--</dx:ASPxButton>--%>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>

                     <dx:ASPxGridView Style="margin: 0 auto;" ID="grid" ClientInstanceName="grid" runat="server" 
                        DataSourceID="AccessDataSource1" Width="100%" OnCommandButtonInitialize="ASPxGridView1_CommandButtonInitialize"  OnSummaryDisplayText="grid_SummaryDisplayText"
                        AutoGenerateColumns="true" ShowSelectCheckbox="true" KeyFieldName="key1;TransactionKey;ChoosenDate;" OnCustomSummaryCalculate="grid_CustomSummaryCalculate">
                        <SettingsBehavior ProcessSelectionChangedOnServer="True" AllowGroup="false" AllowDragDrop="false" />
                        <SettingsPager Mode="ShowPager" PageSize="100"> 
                        </SettingsPager>
                        <Settings ShowFilterBar="Visible" ShowFilterRowMenu="true" ShowHeaderFilterButton="true"
                            ShowFilterRow="true" ShowFooter="true" />
                         <TotalSummary>
                             <dx:ASPxSummaryItem FieldName="Amount" ShowInColumn="Amount" SummaryType="Sum" />
                         </TotalSummary>
                        <Columns>
                            <dx:GridViewCommandColumn ShowSelectCheckbox="True" VisibleIndex="0">
                                 
                                <%-- <HeaderTemplate>
                    <dx:ASPxCheckBox ID="SelectAllCheckBox" runat="server" ToolTip="Select/Unselect all rows on the page"
                        ClientSideEvents-CheckedChanged="function(s, e) { grid.SelectAllRowsOnPage(s.GetChecked()); }" />
                </HeaderTemplate>--%>
                                <HeaderStyle HorizontalAlign="Center" />
                            </dx:GridViewCommandColumn>
                            <dx:GridViewDataColumn FieldName="ChoosenDate" Width="10%" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-VerticalAlign="Middle" Caption="Date" VisibleIndex="1">
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataColumn CellStyle-HorizontalAlign="Left" FieldName="Voucher_No" Width="7%"
                                HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" HeaderStyle-Wrap="True"
                                Caption="Vour/Rcpt Number" VisibleIndex="2" />
                            <dx:GridViewDataColumn FieldName="Series" Width="10%" VisibleIndex="3" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-VerticalAlign="Middle" />
                            <dx:GridViewDataColumn FieldName="Head" Width="15%" VisibleIndex="4" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-VerticalAlign="Middle" />
                            <dx:GridViewDataColumn FieldName="Amount" Width="10%" VisibleIndex="5" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-VerticalAlign="Middle" />
                            <dx:GridViewDataColumn FieldName="Narration" Width="25%" VisibleIndex="6" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-VerticalAlign="Middle" />
                            <dx:GridViewDataColumn FieldName="Voucher_Type" Width="7%" Caption="Credit/Debit"
                                VisibleIndex="7" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" />
                            <dx:GridViewDataColumn FieldName="TransationType" Caption="Transaction Type" HeaderStyle-Wrap="True" Width="10%" VisibleIndex="8" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-VerticalAlign="Middle" />
                            <dx:GridViewDataColumn FieldName="CurrentDate" Width="6%" VisibleIndex="8" Visible="false" Caption="Created Date"
                                HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" />
                        </Columns>
                    </dx:ASPxGridView>
                    <dx:ASPxGridViewExporter ID="gridexcel" GridViewID="grid" 

    PaperKind="A4" Landscape="True"  runat="server" >
</dx:ASPxGridViewExporter>
                    <asp:SqlDataSource runat="server" ID="AccessDataSource1" 
                        ProviderName="MySql.Data.MySqlClient" SelectCommand="" />
                </div>
            </div>
        </div>
    </div>
    <asp:LinkButton runat="server" ID="show" Text=""></asp:LinkButton>
    <ajax:ModalPopupExtender ID="ModalPopupExtender1" TargetControlID="show" PopupControlID="pnlConfirmation"
        BackgroundCssClass="modalBackground" runat="server">
    </ajax:ModalPopupExtender>
    <asp:Panel CssClass="raised" ID="pnlConfirmation" runat="server" Visible="false" Width="100%"
        Style="min-height: 100px; min-width: 300px; max-width: 900px;top:0px; max-width:900px;max-height:500px;overflow-y:scroll;">
        <asp:Label runat="server" ID="lblHintConfirmation" Text="" Visible="false"> </asp:Label>
        <asp:Label runat="server" ID="lblChoosenDate" Text="" Visible="false"></asp:Label>
        <div class="boxheader">
            <asp:Label runat="server" ID="lblHeadingConfirmation" Text=""> </asp:Label>
        </div>
        <div style="min-height: 100px; text-align: center; padding-left: 10px; padding-right: 10px;">
            <br />
            <asp:Label runat="server" ID="lblContentConfirmation" Text="Please Confirm Your Transaction???"> </asp:Label>
            <asp:GridView ID="gvConfirm" Width="100%" runat="server" AutoGenerateColumns="true" Height="500px" AlternatingRowStyle-Width="600px"
                HeaderStyle-BackColor="#61A6F8" HeaderStyle-Font-Bold="true" HeaderStyle-ForeColor="White"
                PageSize="20" BackColor="White" BorderWidth="1px" CellPadding="4" BorderColor="#DEDFDE"
                BorderStyle="None" CssClass="aspxtable" ForeColor="Black" GridLines="None">
                <RowStyle BackColor="#F7F7DE" />
                <HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                <RowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                <%-- <PagerStyle CssClass="GridviewScrollC2Pager" />
                <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White"></HeaderStyle>
                <AlternatingRowStyle BackColor="White" />--%>
            </asp:GridView>
            <br />
        </div>
        <div class="boxheader" style="margin: 0 auto;">
            <asp:Button Width="100" Style="margin: 0 auto" CssClass="GreenyPushButton" ID="Button1"
                OnClick="btnyes_Click" runat="server" Text="yes" />
            <asp:Button Width="100" Style="margin: 0 auto" CssClass="GreenyPushButton" ID="Button2"
                OnClick="btnno_Click" runat="server" Text="No" />
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
