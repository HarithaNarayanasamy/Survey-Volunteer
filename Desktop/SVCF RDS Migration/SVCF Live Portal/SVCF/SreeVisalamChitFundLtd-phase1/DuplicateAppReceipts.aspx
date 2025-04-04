<%@ Page Title="" Language="C#" MasterPageFile="~/Branch.Master" AutoEventWireup="true" CodeBehind="DuplicateAppReceipts.aspx.cs" Inherits="SreeVisalamChitFundLtd_phase1.DuplicateAppReceipts" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMainContent" runat="server">
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
                        <asp:TextBox ID="txtTo" runat="server" CssClass="input-text maskdate" Width="100px" ></asp:TextBox>
                    </td>
                    </tr>
                </table>
            <table>
                <tr>
                    <%--<td>
                        <asp:Label ID="lblBranch" runat="server" Text="Select Branch" Visible="False"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlBranch" runat="server" CssClass="chzn-select" Width="200px" AutoPostBack="true" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged" Visible="False">
                    </asp:DropDownList>
                    </td>
                    <td></td>
                    <td>
                        <asp:Label ID="lblMoneyCollector" runat="server" Text="Select MoneyCollector" Visible="False"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlMoneyCollector" runat="server" CssClass="chzn-select" Width="200px" AutoPostBack="true" OnSelectedIndexChanged="ddlMoneyCollector_SelectedIndexChanged" Visible="False"></asp:DropDownList>
                        <asp:CompareValidator ID="CompareValidator2" runat="server" ErrorMessage="*" ControlToValidate="ddlMoneyCollector" ValueToCompare="--Select--"
                             Display="Dynamic" Operator="NotEqual"></asp:CompareValidator>
                    </td>--%>
                    <td>
                        <asp:Label ID="lblSeries" runat="server" Text="Select Mode"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlSeries" runat="server" CssClass="chzn-select" Width="200px" AutoPostBack="true">
                            
                        </asp:DropDownList>
                        <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="*" ValueToCompare="--Select--" ControlToValidate="ddlSeries"
                             Display="Dynamic" Operator="NotEqual"></asp:CompareValidator>
                    </td>
                   
                    <td>
                         &nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnSubmit" runat="server" Text="Show" CssClass="GreenyPushButton" OnClick="btnSubmit_Click" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <br />

    <div class="receiptlist" runat="server">
        <div class="row">
            <div class="twelve columns">
                <div class="box_c">
                    <div class="box_c_heading  box_actions">
                        <asp:Label ID="lblHeading" runat="server"></asp:Label>
                        <%--<p>App & Web Receipts</p>--%>
                    </div>
                    <div class="twelve columns centered">
                        <div style="margin: 0px auto; width: 100%; overflow: auto !important;">
                            <br />
                        </div>
                        <asp:GridView ID="gridReceipts" BorderStyle="Solid" runat="server"
                            CellSpacing="3" Font-Names="Verdana" ForeColor="#333333" GridLines="None" Height="100px" TabIndex="15"
                            AutoGenerateColumns="false" CssClass="Trans twelve columns" Width="900px" OnRowCommand="gridReceipts_RowCommand">
                            <RowStyle BackColor="#F7F6F3" />
                            <RowStyle CssClass="GridViewRowStyle" />
                            <HeaderStyle CssClass="GridViewHeaderStyle" />
                            <Columns>
                                <asp:BoundField DataField="ChoosenDate" HeaderText="Date" />
                                <asp:BoundField DataField="AppReceiptno" HeaderText="AppReceipt No" />
                                <asp:BoundField DataField="GrpMemberID" HeaderText="Chit Number" />
                                <asp:BoundField DataField="Amount" HeaderText="Amount" />
                                <asp:BoundField DataField="Type" HeaderText="Type" />
                                <asp:TemplateField HeaderText="Receipt PDF" >
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkPdf" runat="server" Text="Download" CommandArgument='<%# Eval("AppReceiptno")%>' 
                                                CommandName="Download"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Status" HeaderText="Status" />
                                <%--<asp:BoundField DataField="Pdf_Location" HeaderText="Receipt Link" />--%>
                                <asp:BoundField DataField="Mobileno" HeaderText="Mobile Number" />
                            </Columns>
                            <FooterStyle VerticalAlign="Middle" HorizontalAlign="Center" CssClass="GridviewScrollC2Header" />
                            <HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center" CssClass="GridviewScrollC2Header" />
                            <RowStyle VerticalAlign="Middle" HorizontalAlign="Center" CssClass="GridviewScrollC2Item" />
                            <PagerStyle CssClass="GridviewScrollC2Pager" />
                        </asp:GridView>
                    </div>
                </div>
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

</asp:Content>
