<%@ Page Culture="en-GB" Language="C#" AutoEventWireup="true" CodeBehind="BranchLedgerNew.aspx.cs"
    Inherits="SreeVisalamChitFundLtd_phase1.BranchLedgerNew" %>

<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxMenu" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1.Export, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView.Export" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html lang="en">
<head id="Head1" runat="server">
    <link rel="stylesheet" href="pertho_admin_v1.3/lib/chosen/chosen.css" media="all" />
    <script src="pertho_admin_v1.3/js/jquery.min.js" type="text/javascript"></script>
    <script src="pertho_admin_v1.3/js/jquery.inputmask.js" type="text/javascript"></script>
    <script src="pertho_admin_v1.3/js/jquery.inputmask.extentions.js" type="text/javascript"></script>
    <link rel="stylesheet" type="text/css" href="Styles/ControlsCss.css" />
    <link href="Styles/Validation.css" rel="stylesheet" type="text/css" />
    <script src="pertho_admin_v1.3/lib/chosen/chosen.jquery.min.js"></script>
    <title>SVCF Admin Panel</title>
    <style type="text/css">
        div.Sub-heading
        {
            padding: 0.3em;
            box-shadow: inset 2px 2px 5px #6c7a95, 2px 2px 5px #6c7a95,2px 2px 5px #6c7a95,2px 2px 5px #6c7a95;
            -webkit-border-radius: 0px;
            -moz-border-radius: 0px;
            border-radius: 0px;
            -moz-background-clip: padding;
            -webkit-background-clip: padding-box;
            background-clip: padding-box;
        }
        div[style="padding: 2px; position: absolute; left: 1px; top: 1px; z-index: 100000; font-family: sans-serif; font-size: 8pt; color: black; background-color: white;"]
        {
            display:none;
        }        
        .chzn-container 
        {
            font-size: 16px;
        }
        .chzn-results
        {
            text-align:center;
        }
        #ddlGroup_chzn .chzn-drop .chzn-search input[type="text"]
        {
            height:16px;
        }
    </style>
    <script type="text/javascript">
        prth_mask_input = {
            init: function () {
                $(".maskdate").inputmask("99/99/9999", { placeholder: "dd/mm/yyyy" });
            }
        };
        $(".chzn-select").chosen();
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <ajax:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </ajax:ToolkitScriptManager>
    <div style="margin-top: -0.5em; margin-bottom: 0.6em;" class="Sub-heading">
        <div style="display: table-cell; vertical-align: top; padding-top: 10px; padding-right: 5px !important;">
            <asp:Label ID="Label3" runat="server" Text="From Date :"></asp:Label>
        </div>
        <div style="display: table-cell; vertical-align: top; padding-top: 5px; padding-right: 5px !important;">
            <asp:TextBox class="input-text maskdate" Width="100px" ID="txtFromDate" runat="server"
                placeholder="From Date">
            </asp:TextBox>
            <asp:RequiredFieldValidator ValidationGroup="twelvehead" Display="Dynamic" ID="CompareValidatoxr1"
                runat="server" ControlToValidate="txtFromDate" ErrorMessage=""></asp:RequiredFieldValidator>
            <asp:CompareValidator ValidationGroup="twelvehead" Display="Dynamic" ID="cmpStartDate"
                runat="server" ControlToValidate="txtFromDate" Operator="DataTypeCheck" Type="Date"></asp:CompareValidator>
        </div>
        <div style="display: table-cell; vertical-align: top; padding-top: 10px; padding-right: 5px !important;">
            <asp:Label ID="Label1" runat="server" Text="To Date :"></asp:Label>
        </div>
        <div style="display: table-cell; vertical-align: top; padding-top: 5px; padding-right: 5px !important;">
            <asp:TextBox class="input-text maskdate" Width="100px" ID="txtToDate" placeholder="To Date"
                runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ValidationGroup="twelvehead" Display="Dynamic" ID="RequiredFieldValidator1"
                runat="server" ControlToValidate="txtToDate" ErrorMessage=""></asp:RequiredFieldValidator>
            <asp:CompareValidator ValidationGroup="twelvehead" Display="Dynamic" ID="cmpEndDate"
                runat="server" ControlToCompare="txtFromDate" ControlToValidate="txtToDate" Operator="GreaterThanEqual"
                Type="Date"></asp:CompareValidator>
        </div>
        <div style="display: table-cell; vertical-align: top; padding-top: 5px; padding-right: 5px !important;">
            <asp:Button ID="BtnStatisticsGo" ValidationGroup="twelvehead" runat="server" class="GreenyPushButton"
                OnClick="BtnStatisticsGo_Click" Text="Go!"></asp:Button>
        </div>
        <div style="display: table-cell; vertical-align: top; padding-top: 10px; padding-right: 5px !important;">
            <asp:Label runat="server" Text="Grouping : "></asp:Label>
        </div>
        <div style="display: table-cell; vertical-align: top;padding-top:4px; padding-right: 5px !important;">
            <asp:DropDownList ID="ddlGroup" Width="150px" CssClass="chzn-select" AutoPostBack="true" OnSelectedIndexChanged="ddlGroup_OnSelectedIndexChanged"
                runat="server">
                <asp:ListItem Text="Ungroup" Value="Ungroup"></asp:ListItem>
                <asp:ListItem Text="Branch and Date" Value="Branch and Date"></asp:ListItem>
                <asp:ListItem Text="Branch alone" Value="Branch alone"></asp:ListItem>
            </asp:DropDownList>
        </div>
        
        <div style="display: table-cell; vertical-align: top; float: right; padding-right: 5px;
            text-align: right; margin-top: -35px;">
            <div style="display: table-cell;">
                <dx:ASPxMenu OnItemClick="Export_click" ID="mMain" runat="server" AllowSelectItem="True"
                    ShowPopOutImages="True">
                    <Items>
                        <dx:MenuItem Text="Export">
                            <Items>
                                <dx:MenuItem Text="PDF">
                                </dx:MenuItem>
                                <dx:MenuItem Text="XLSX">
                                </dx:MenuItem>
                            </Items>
                        </dx:MenuItem>
                    </Items>
                </dx:ASPxMenu>
            </div>
        </div>
    </div>
    <dx:ASPxGridView Style="margin: 0 auto; width: 100%;" OnSummaryDisplayText="ASPxGridView1_SummaryDisplayText"
        OnCustomSummaryCalculate="ASPxGridView1_OnCustomSummaryCalculate" ID="grid" ClientInstanceName="grid"
        runat="server" DataSourceID="AccessDataSource1">
        <Settings GroupFormat="{1}{2}" VerticalScrollableHeight="303" ShowTitlePanel="true"
            ShowVerticalScrollBar="true" VerticalScrollBarStyle="Standard" ShowHeaderFilterButton="true"
            ShowFooter="true" ShowGroupFooter="VisibleAlways" ShowGroupPanel="false" ShowGroupedColumns="true"
            ShowFilterBar="Visible" ShowFilterRow="true" />
        <SettingsText Title="Trial Balance of Branches" />
        <GroupSummary>
            <dx:ASPxSummaryItem FieldName="Credit" SummaryType="Sum" />
            <dx:ASPxSummaryItem FieldName="Debit" SummaryType="Sum" />
            <dx:ASPxSummaryItem FieldName="Date" ShowInGroupFooterColumn="Debit" SummaryType="Custom" />
            <dx:ASPxSummaryItem FieldName="Branch" ShowInGroupFooterColumn="Credit" SummaryType="Custom" />
        </GroupSummary>
        <TotalSummary>
            <dx:ASPxSummaryItem FieldName="Credit" SummaryType="Custom" />
            <dx:ASPxSummaryItem FieldName="Debit" SummaryType="Custom" />
            <dx:ASPxSummaryItem FieldName="Narration" SummaryType="Custom" />
        </TotalSummary>
        <SettingsPager Mode="ShowAllRecords">
        </SettingsPager>
        <Columns>
            <dx:GridViewDataColumn FieldName="Date" CellStyle-HorizontalAlign="Left">
            </dx:GridViewDataColumn>
            <dx:GridViewDataColumn FieldName="Branch" CellStyle-HorizontalAlign="Left">
            </dx:GridViewDataColumn>
            <dx:GridViewDataColumn FieldName="Narration"  FooterCellStyle-HorizontalAlign="Left"  CellStyle-HorizontalAlign="Left">
            </dx:GridViewDataColumn>
            <dx:GridViewDataColumn FieldName="Credit" FooterCellStyle-HorizontalAlign="Right"  GroupFooterCellStyle-HorizontalAlign="Right" CellStyle-HorizontalAlign="Right">
            </dx:GridViewDataColumn>
            <dx:GridViewDataColumn FieldName="Debit" FooterCellStyle-HorizontalAlign="Right" GroupFooterCellStyle-HorizontalAlign="Right" CellStyle-HorizontalAlign="Right">
            </dx:GridViewDataColumn>
        </Columns>
        <Styles GroupFooter-HorizontalAlign="Right" Footer-HorizontalAlign="Right" Cell-HorizontalAlign="Right"
            Header-VerticalAlign="Middle" Header-HorizontalAlign="Center" Header-Wrap="True">
            <GroupPanel Wrap="True" HorizontalAlign="Left">
            </GroupPanel>
            <GroupRow Wrap="True" HorizontalAlign="Left">
            </GroupRow>
        </Styles>
    </dx:ASPxGridView>
    <dx:ASPxGridViewExporter ID="gridExport" runat="server" GridViewID="grid">
        <Styles>
            <Header Font-Size="8" Wrap="True" HorizontalAlign="Center">
            </Header>
            <Cell Font-Size="7" HorizontalAlign="Left" Wrap="True">
            </Cell>
            <Footer Font-Size="8" HorizontalAlign="Left" Wrap="True">
            </Footer>
            <Title Font-Size="9" VerticalAlign="Middle" HorizontalAlign="Center" Wrap="True">
            </Title>
            <GroupFooter Font-Size="5" HorizontalAlign="Left" Wrap="True">
            </GroupFooter>
            <GroupRow Font-Size="5" HorizontalAlign="Left" Wrap="True">
            </GroupRow>
        </Styles>
    </dx:ASPxGridViewExporter>
    <asp:SqlDataSource runat="server" ID="AccessDataSource1" 
        ProviderName="MySql.Data.MySqlClient" SelectCommand=" " />
    </form>
</body>
<script type="text/javascript">
    $(document).ready(function () {
        prth_mask_input.init();
    });
    $(document).ready(function () {
        $(".chzn-select").chosen({ search_contains: true });
    });
    var prm = Sys.WebForms.PageRequestManager.getInstance();
    prm.add_endRequest(function () {
        $(".chzn-select").chosen({ search_contains: true });
    });
</script>
</html>
