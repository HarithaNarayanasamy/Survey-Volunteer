<%@ Page Title="" Language="C#" MasterPageFile="~/Branch.Master" AutoEventWireup="true" CodeBehind="CancelledReceipts.aspx.cs" Inherits="SreeVisalamChitFundLtd_phase1.CancelledReceipts" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
     <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
    <%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
        Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
    <%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
        Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
    <style type="text/css">
        td[style="cursor:default;"] {
            vertical-align: middle;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMainContent" runat="server">
    <link href="Styles/tablecss.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        input[type="text"] {
            margin-bottom: 3px;
        }

        input[type="image"]:active {
            -moz-box-shadow: inset 0 0 10px rgba(0,0,0,.06);
            -webkit-box-shadow: inset 0 0 10px rgba(0,0,0,.06);
            box-shadow: inset 0 0 10px rgba(0,0,0,.06);
            z-index: 6;
            border: 2px solid #006469 !important;
        }

        .Trans div[id*="chzn"] {
            width: 100% !important;
        }

            .Trans div[id*="chzn"] span {
                width: 140px !important;
                overflow: hidden;
            }

        .Trans td {
            width: 14% !important;
            padding-left: 8px !important;
            padding-right: 8px !important;
        }

            .Trans td > input[type="text"] {
                width: 100% !important;
            }

            .Trans td div[class="ui-spinner"] > input[type="text"] {
                width: 100% !important;
            }

        .Trans select {
            width: 100% !important;
        }

        .hidable table {
            vertical-align: middle;
        }

        .hidable td {
            text-align: left;
            padding: 3px;
            margin: 3px;
        }

        .chzn-drop {
            border: 1px solid #aaa !important;
            padding-top: 5px !important;
        }

        div[id*="ddlToken_chzn"] .chzn-drop {
            width: 100% !important;
        }

        div[id*="ddlMisc"] .chzn-drop {
            width: 200% !important;
        }

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

        .center {
            margin-left: auto;
            margin-right: auto;
        }
    </style>
    <ajax:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </ajax:ToolkitScriptManager>
    <div class="row">
        <div class="row">
            <div class="twelve columns">
                <div class="box_c">
                    <div class="box_c_heading  box_actions">
                        <%--<asp:Label ID="lblHeading" runat="server"></asp:Label>--%>
                        <p>View Canceled Receipts</p>
                    </div>
                    <div class="box_c_content">
                        <div class="row">
                            <br />
                            <div>
                                <div>
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <ContentTemplate>
                                            <div>
                                                <div style="width: 100%; margin: 0 auto;">
                                                    <table style="width: 90%">
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

                                                    <table style="margin: 0 auto; width: 90%">

                                                        <tr>

                                                            <td style="vertical-align: top; padding-top: 6px !important;">
                                                                <asp:Label ID="lblBranch" runat="server" Text="Select Branch"></asp:Label>
                                                            </td>
                                                            <td style="vertical-align: top;">
                                                                <asp:DropDownList ID="ddlBranch" runat="server" CssClass="chzn-select" Width="240px" AutoPostBack="true" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged">
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="vertical-align: top; padding-top: 6px !important;">
                                                                <asp:Label ID="Labmid" runat="server" Text="Received by"></asp:Label>
                                                            </td>
                                                            <td style="vertical-align: top;">
                                                                <asp:DropDownList Width="240px" CssClass="chzn-select"
                                                                    runat="server" ID="DDLmid" AutoPostBack="true" OnSelectedIndexChanged="DDLmid_Click">
                                                                </asp:DropDownList>
                                                                <asp:Label ID="lbn1" runat="server" Visible="false"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>

                                                            <td style="vertical-align: top; padding-top: 6px !important;">
                                                                <asp:Label ID="Labseries" runat="server" Text="Series"></asp:Label>
                                                            </td>
                                                            <td style="vertical-align: top;">
                                                                <asp:DropDownList Width="240px" CssClass="chzn-select"
                                                                    runat="server" ID="ddlseries" AutoPostBack="true"
                                                                    TabIndex="2" OnSelectedIndexChanged="ddlseries_Click">
                                                                </asp:DropDownList>

                                                            </td>

                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Button ID="BtnStatisticsGo" runat="server" Text="Go!" ValidationGroup="twelvehead"
                                                                     CssClass="GreenyPushButton" class="btn" OnClick="BtnStatisticsGo_Click" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <br />
                                                    <br />
                                                    <br />
                                                    <div style="display: table-cell; vertical-align: top; float: right; padding-right: 7px; text-align: right; margin-top: -35px;">
                                                        <asp:ImageButton ID="imgexport" runat="server" ImageUrl="~/Styles/Image/document_export.png" OnClick="imgexport_Click"
                                                            Height="33px" Width="30px" PostBackUrl="~/CancelledReceipts.aspx" />
                                                    </div>
                                                    <asp:Panel ID="Panel1" runat="server">


                                                        <asp:GridView ID="gridBranch1" runat="server" BorderStyle="Solid"
                                                            CellSpacing="4" Font-Names="Verdana" ForeColor="#333333" Height="100px" TabIndex="15"
                                                            AutoGenerateColumns="false" CssClass="Trans twelve columns" Width="100%" OnPageIndexChanging="gvpageindex">

                                                            <RowStyle BackColor="#F7F6F3" />
                                                            <RowStyle CssClass="GridViewRowStyle" />
                                                            <HeaderStyle CssClass="GridViewHeaderStyle" Font-Bold="True" Width="100%" />
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Serial No" HeaderStyle-BorderColor="Gray" HeaderStyle-BorderStyle="Solid" HeaderStyle-BorderWidth="2px"
                                                                    ItemStyle-BorderColor="Gray" ItemStyle-BorderStyle="Solid" ItemStyle-BorderWidth="2px" HeaderStyle-Width="30">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:BoundField HeaderText="Receipt Date" DataField="ChoosenDate"
                                                                    HeaderStyle-BorderColor="Gray" HeaderStyle-BorderStyle="Solid" HeaderStyle-BorderWidth="2px"
                                                                    ItemStyle-BorderColor="Gray" ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px" />


                                                                <asp:BoundField HeaderText="Cancelled Date" DataField="ModifiedDate"
                                                                    HeaderStyle-BorderColor="Gray" HeaderStyle-BorderStyle="Solid" HeaderStyle-BorderWidth="2px"
                                                                    ItemStyle-BorderColor="Gray" ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px" />


                                                                <asp:BoundField HeaderText="Receipt No" DataField="AppReceiptno"
                                                                    HeaderStyle-BorderColor="Gray" HeaderStyle-BorderStyle="Solid" HeaderStyle-BorderWidth="2px"
                                                                    ItemStyle-BorderColor="Gray" ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px" />


                                                                <asp:BoundField HeaderText="Chit Number" DataField="GrpMemberID"
                                                                    HeaderStyle-BorderColor="Gray" HeaderStyle-BorderStyle="Solid" HeaderStyle-BorderWidth="2px"
                                                                    ItemStyle-BorderColor="Gray" ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px" />


                                                                <asp:BoundField HeaderText="Amount" DataField="Amount"
                                                                    HeaderStyle-BorderColor="Gray" HeaderStyle-BorderStyle="Solid" HeaderStyle-BorderWidth="2px"
                                                                    ItemStyle-BorderColor="Gray" ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px" />



                                                                <asp:BoundField HeaderText="RejectReason" DataField="RejectReason"
                                                                    HeaderStyle-BorderColor="Gray" HeaderStyle-BorderStyle="Solid" HeaderStyle-BorderWidth="2px"
                                                                    ItemStyle-BorderColor="Gray" ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px" />

                                                            </Columns>
                                                            <%--<FooterStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                            <HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                            <RowStyle HorizontalAlign="Center" VerticalAlign="Middle" />--%>
                                                        </asp:GridView>


                                                    </asp:Panel>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>

                                </div>

                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div>
    </div>
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
