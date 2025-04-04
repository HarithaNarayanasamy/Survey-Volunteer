<%@ Page Culture="en-GB" Language="C#" MasterPageFile="~/Branch.Master" AutoEventWireup="true"
    CodeBehind="DirectOtherItemsLedger.aspx.cs" Inherits="SreeVisalamChitFundLtd_phase1.DirectOtherItemsLedger"
    Title="SVCF Admin Panel" %>

<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxTabControl" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxMenu" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1.Export, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView.Export" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxClasses" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGlobalEvents" TagPrefix="dx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>

<asp:Content ID="Content2" ContentPlaceHolderID="cphHead" runat="server">
    <title>SVCF Admin Panel</title>
    <script type="text/javascript">
        function OnInit(s, e) {
            AdjustSize();
        }
        function OnEndCallback(s, e) {
            AdjustSize();
        }
        function OnControlsInitialized(s, e) {
            ASPxClientUtils.AttachEventToElement(window, "resize", function (evt) {
                AdjustSize();
            });
        }
        function AdjustSize() {
            var height = Math.max(0, document.documentElement.clientHeight);
            grid.SetHeight(height);
        }
    </script>
    <style type="text/css">

        td[style="cursor:default;"]
        {
            vertical-align:middle;
        }

    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphMainContent" runat="server">
    <ajax:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </ajax:ToolkitScriptManager>
    <div class="row">
        <div class="twelve columns">
            <div class="box_c">
                <div class="box_c_heading cf">
                    <p class="sepV_a">
                        Other Items Ledger</p>
                </div>
                <div class="box_c_content">
                    <div class="row">


                     <div style="margin-top: -0.5em; margin-bottom: 0.6em;" class="Sub-heading">
        <div style="display: table-cell; vertical-align: top; padding-top: 10px; padding-right: 5px !important;">
            <asp:Label ID="Label3" runat="server" Text="From Date :"></asp:Label>
        </div>
        <div style="display: table-cell; vertical-align: top; padding-top: 5px; padding-right: 5px !important;">
            <asp:TextBox class="input-text maskdate" Width="80px" ID="dateFromConsolidated" runat="server"
                placeholder="From Date">
            </asp:TextBox>
            <asp:RequiredFieldValidator ValidationGroup="twelvehead" Display="Dynamic" ID="CompareValidatoxr1"
                runat="server" ControlToValidate="dateFromConsolidated" ErrorMessage=""></asp:RequiredFieldValidator>
            <asp:CompareValidator ValidationGroup="twelvehead" Display="Dynamic" ID="cmpStartDate"
                runat="server" ControlToValidate="dateFromConsolidated" Operator="DataTypeCheck" Type="Date"></asp:CompareValidator>
        </div>
        <div style="display: table-cell; vertical-align: top; padding-top: 10px; padding-right: 5px !important;">
            <asp:Label ID="Label1" runat="server" Text="To Date :"></asp:Label>
        </div>
        <div style="display: table-cell; vertical-align: top; padding-top: 5px; padding-right: 5px !important;">
            <asp:TextBox class="input-text maskdate" Width="80px" ID="dateToConsolidated" placeholder="To Date"
                runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ValidationGroup="twelvehead" Display="Dynamic" ID="RequiredFieldValidator1"
                runat="server" ControlToValidate="dateToConsolidated" ErrorMessage=""></asp:RequiredFieldValidator>
            <asp:CompareValidator ValidationGroup="twelvehead" Display="Dynamic" ID="cmpEndDate"
                runat="server" ControlToCompare="dateFromConsolidated" ControlToValidate="dateToConsolidated" Operator="GreaterThanEqual"
                Type="Date"></asp:CompareValidator>
        </div>
        <div style="display: table-cell; vertical-align: top; padding-top: 5px; padding-right: 5px !important;">
            <asp:Button ID="BtnStatisticsGo" ValidationGroup="twelvehead" runat="server" class="GreenyPushButton"
                OnClick="BtnStatisticsGo_Click" Text="Go!"></asp:Button>
        </div>
        <div style="display: table-cell; vertical-align: top; padding-top: 10px; padding-right: 5px !important;">
            <asp:Label ID="Label2" runat="server" Text="Grouping : "></asp:Label>
        </div>
        <div style="display: table-cell; vertical-align: top; padding-top: 4px; padding-right: 5px !important;">
            <asp:DropDownList ID="ddlGroup" Width="150px" CssClass="chzn-select" AutoPostBack="true"
                OnSelectedIndexChanged="ddlGroup_OnSelectedIndexChanged" runat="server">
                <asp:ListItem Text="Ungroup" Value="Ungroup"></asp:ListItem>
                <asp:ListItem Text="Head and Date" Value="Head and Date"></asp:ListItem>
                <asp:ListItem Text="Head Alone" Value="Head Alone"></asp:ListItem>
            </asp:DropDownList>
        </div>
                           <div style="display: table-cell; vertical-align: top; padding-top: 10px; padding-right: 5px !important;">
                                <asp:Label ID="Label4" runat="server" Text="OtherItem: "></asp:Label>
                            </div>
                            <div style="display:table-cell; vertical-align:top; padding-top:4px; padding-right:5px !important;">
                                <asp:DropDownList ID="ddlotheritem" Width="150px" CssClass="chzn-select" AutoPostBack="true" OnSelectedIndexChanged="ddlotheritem_OnSelectedIndexChanged"
                                  runat="server"></asp:DropDownList> 
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
                             
                            </Items>
                        </dx:MenuItem>
                    </Items>
                </dx:ASPxMenu>
            </div>
        </div>
                          <div style="display:table-cell; float:right; padding-left:5px; text-align:right; margin-top:-0px; ">
                                      <dx:ASPxButton ID="btnExportExcel" Text="ExportExcel"  runat="server" Visible="true"

                            ImageSpacing="0px" EnableTheming="False" EnableDefaultAppearance="True" ToolTip="Export to Excel"

                              Cursor="pointer" OnClick="btnExportExcel_Click1">
                           
                           
                        </dx:ASPxButton>
                            </div>
    </div>
                        <dx:ASPxPageControl Width="100%" ID="carTabPage" TabAlign="Left" TabPosition="Top"
                            ActivateTabPageAction="Click" runat="server" ActiveTabIndex="0" EnableHierarchyRecreation="True">
                            <TabPages>
                                <dx:TabPage Text="Consolidated View">
                                    <ContentCollection>
                                        <dx:ContentControl ID="ContentControl1" runat="server">
                                            
                                            <ul class="overview_list">
                                                <li><a href="#">
                                                    <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                        alt="" />
                                                    <asp:Label runat="server" ID="lblCrAmount" Text="0.00" class="ov_nb"></asp:Label>
                                                    <span class="ov_text">Cr Amount</span> </a></li>
                                                <li><a href="#">
                                                    <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                        alt="" />
                                                    <asp:Label runat="server" ID="lblDrAmount" Text="0.00" class="ov_nb"></asp:Label>
                                                    <span class="ov_text">Dr Amount</span> </a></li>
                                                <li><a href="#">
                                                    <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                        alt="" />
                                                    <asp:Label runat="server" ID="lblCrBalance" Text="0.00" class="ov_nb"></asp:Label>
                                                    <span class="ov_text">Cr Balance</span> </a></li>
                                                <li><a href="#">
                                                    <img src="img/blank.gif" style="background-image: url(pertho_admin_v1.3/img/ico/open/Rs.png)"
                                                        alt="" />
                                                    <asp:Label runat="server" ID="lblDrBalance" Text="0.00" class="ov_nb"></asp:Label>
                                                    <span class="ov_text">Dr Balance</span> </a></li>
                                            </ul>
                                        </dx:ContentControl>
                                    </ContentCollection>
                                </dx:TabPage>
                                <dx:TabPage Text="Detailed View">
                                    <ContentCollection>
                                        <dx:ContentControl ID="ContentControl2" runat="server">
                                            <dx:ASPxGridView Styles-AlternatingRow-BackColor="LightGray" Style="margin: 0 auto;
                                                width: 100%;" ID="gridprev" ClientInstanceName="gridprev" runat="server" Settings-ShowColumnHeaders="false" DataSourceID="AccessDataSource2" 
                                                OnHtmlRowCreated="gridprev_HtmlRowCreated" OnSummaryDisplayText="gridprev_SummaryDisplayText" OnCustomCallback="gridprev_CustomCallback" 
                                                OnCustomSummaryCalculate="gridprev_CustomSummaryCalculate">

                                                <Settings GroupFormat="{1}{2}" ShowFilterRowMenu="false"
                                                    ShowHeaderFilterButton="false" ShowFooter="true" ShowGroupFooter="VisibleAlways"
                                                    ShowGroupPanel="false" ShowGroupedColumns="true" ShowFilterBar="Hidden" ShowFilterRow="false" />
                                                <TotalSummary>
                                                    <dx:ASPxSummaryItem FieldName="Credit" SummaryType="Sum" />
                                                    <dx:ASPxSummaryItem FieldName="Debit" SummaryType="Sum" />
                                                    <dx:ASPxSummaryItem FieldName="Narration" SummaryType="Custom" />
                                                </TotalSummary>
                                                <GroupSummary>
                                                    <dx:ASPxSummaryItem FieldName="Credit" SummaryType="Sum" />
                                                    <dx:ASPxSummaryItem FieldName="Debit" SummaryType="Sum" />
                                                    <dx:ASPxSummaryItem FieldName="Date" ShowInGroupFooterColumn="Debit" SummaryType="Custom" />
                                                    <dx:ASPxSummaryItem FieldName="LedgerHead" ShowInGroupFooterColumn="Credit" SummaryType="Custom" />
                                                </GroupSummary>
                                              <%--  <SettingsText Title="" />--%>
                                             <%--   <SettingsPager Visible="false" Position="Bottom" />--%>
                                                <ClientSideEvents Init="OnInit" EndCallback="OnEndCallback" />
                                                <Columns>
                                                    <dx:GridViewDataColumn FieldName="Date">
                                                    </dx:GridViewDataColumn>
                                                    <dx:GridViewDataColumn FieldName="LedgerHead">
                                                    </dx:GridViewDataColumn>
                                                    <dx:GridViewDataColumn FieldName="Narration">
                                                    </dx:GridViewDataColumn>
                                                    <dx:GridViewDataColumn FieldName="Credit">
                                                    </dx:GridViewDataColumn>
                                                    <dx:GridViewDataColumn FieldName="Debit">
                                                    </dx:GridViewDataColumn>
                                                </Columns>
                                                <Styles Footer-HorizontalAlign="Left"  Header-VerticalAlign="Middle"
                                                    Header-HorizontalAlign="Center" Header-Wrap="True">
                                                    <Header HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True"></Header>
                                                    <AlternatingRow BackColor="LightGray"></AlternatingRow>
                                                    <Cell HorizontalAlign="Left"></Cell>
                                                    <Footer HorizontalAlign="Left"></Footer>
                                                </Styles>
                                            </dx:ASPxGridView>
                                            <dx:ASPxGridViewExporter ID="gridExportprev" runat="server" GridViewID="grdTemp">
                                             <%--   <Styles>
                                                    <Header Wrap="True" HorizontalAlign="Center">
                                                    </Header>
                                                    <Cell HorizontalAlign="Left" Wrap="True">
                                                    </Cell>
                                                    <Footer HorizontalAlign="Left" Wrap="True">
                                                    </Footer>
                                                </Styles>--%>

                                        <Styles>
                                                      <Cell Font-Size="08"  Wrap="True" Paddings-PaddingBottom="3px" Paddings-PaddingRight="3px" >
                                                         </Cell>
                                                     <Footer Font-Size="08" HorizontalAlign="Right" Wrap="True">
                                                     </Footer>
                                
                                <Title Font-Size="07" VerticalAlign="Middle" HorizontalAlign="Center" Paddings-PaddingBottom="2px" Paddings-PaddingTop="2px">
                                </Title>
                                                   
                                                    </Styles>
                                            </dx:ASPxGridViewExporter>
                                            <dx:ASPxGridView Styles-AlternatingRow-BackColor="LightGray" Style="margin: 0 auto;
                                                width: 100%;" OnSummaryDisplayText="ASPxGridView1_SummaryDisplayText" OnCustomSummaryCalculate="ASPxGridView1_OnCustomSummaryCalculate"
                                                ID="grid" ClientInstanceName="grid" runat="server" 
                                                OnCustomCallback="grid_CustomCallback">
                                                <Settings GroupFormat="{1}{2}" VerticalScrollableHeight="303" ShowTitlePanel="true" ShowFilterRowMenu="true"
            ShowVerticalScrollBar="true" VerticalScrollBarStyle="Standard" ShowHeaderFilterButton="true"
            ShowFooter="true" ShowGroupFooter="VisibleAlways" ShowGroupPanel="false" ShowGroupedColumns="true"
            ShowFilterBar="Visible" ShowFilterRow="true" />
        <SettingsText Title="Other Items" />
                                                <GroupSummary>
            <dx:ASPxSummaryItem FieldName="Credit" SummaryType="Sum" />
            <dx:ASPxSummaryItem FieldName="Debit" SummaryType="Sum" />
            <dx:ASPxSummaryItem FieldName="Date" ShowInGroupFooterColumn="Debit" SummaryType="Custom" />
            <dx:ASPxSummaryItem FieldName="LedgerHead" ShowInGroupFooterColumn="Credit" SummaryType="Custom" />
        </GroupSummary>
                                                   <TotalSummary>
            <dx:ASPxSummaryItem FieldName="Credit" SummaryType="Custom" />
            <dx:ASPxSummaryItem FieldName="Debit" SummaryType="Custom" />
            <dx:ASPxSummaryItem FieldName="Narration" SummaryType="Custom" />
        </TotalSummary>
                                                <SettingsPager Mode="ShowPager" Position="Bottom">
                                                </SettingsPager>
                                                <SettingsText Title="Other Items Ledger" />
                                                <ClientSideEvents Init="OnInit" EndCallback="OnEndCallback" />
                                                <Columns>
                                                    <dx:GridViewDataColumn FieldName="Date">
                                                    </dx:GridViewDataColumn>
                                                    <dx:GridViewDataColumn FieldName="LedgerHead">
                                                    </dx:GridViewDataColumn>
                                                    <dx:GridViewDataColumn FieldName="Narration">
                                                    </dx:GridViewDataColumn>
                                                    <dx:GridViewDataColumn FieldName="Credit">
                                                    </dx:GridViewDataColumn>
                                                    <dx:GridViewDataColumn FieldName="Debit">
                                                    </dx:GridViewDataColumn>
                                                </Columns>
                                                <Styles GroupFooter-HorizontalAlign="Left" Footer-HorizontalAlign="Left"     Header-VerticalAlign="Middle"
                                                    Header-HorizontalAlign="Center" Header-Wrap="True">
                                                    <GroupPanel Wrap="True" HorizontalAlign="Left">
                                                    </GroupPanel>
                                                    <GroupRow Wrap="True" HorizontalAlign="Left">
                                                    </GroupRow>
                                                    <GroupFooter HorizontalAlign="Left"></GroupFooter>
                                                </Styles>
                                                <ClientSideEvents EndCallback="function(s,e){gridprev.PerformCallback();}" />
                                            </dx:ASPxGridView>
                                               <dx:ASPxGridView ID="grdTemp" runat="server" Styles-AlternatingRow-BackColor="LightGray" ClientInstanceName="grdTemp" Visible="false" Style="margin: 0 auto; width: 100%;">                                                   
                                                    <SettingsPager Visible="false" Position="Bottom" />
                                                    <ClientSideEvents Init="OnInit" EndCallback="OnEndCallback" />
                                                   <%-- <Columns>                                                        
                                                        <dx:GridViewDataColumn FieldName="Title">
                                                        </dx:GridViewDataColumn>
                                                        <dx:GridViewDataColumn FieldName="Narration">
                                                        </dx:GridViewDataColumn>
                                                        <dx:GridViewDataColumn CellStyle-CssClass="wd"  FieldName="Credit">
                                                           <CellStyle CssClass="wd"></CellStyle>
                                                        </dx:GridViewDataColumn>
                                                        <dx:GridViewDataColumn CellStyle-CssClass="wd" FieldName="Debit">
                                                          <CellStyle CssClass="wd"></CellStyle>
                                                        </dx:GridViewDataColumn>                                                       
                                                    </Columns>--%>
                                                     <Styles Footer-HorizontalAlign="Left"  Header-VerticalAlign="Middle"
                                                        Header-HorizontalAlign="Center" Header-Wrap="True">
                                                        <Header HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True"></Header>
                                                        <AlternatingRow BackColor="LightGray"></AlternatingRow>                                                    

                                                           <Cell HorizontalAlign="Left"></Cell>

                                                        <Footer HorizontalAlign="Left"></Footer>
                                                    </Styles>
                                                </dx:ASPxGridView>
                                            <dx:ASPxGridViewExporter ID="gridExport" runat="server" GridViewID="grid">
                                               <%-- <Styles>
                                                    <Header Wrap="True" HorizontalAlign="Center">
                                                    </Header>
                                                    <Cell HorizontalAlign="Left" Wrap="True">
                                                    </Cell>
                                                    <Footer HorizontalAlign="Left" Wrap="True">
                                                    </Footer>
                                                </Styles>--%>
                                              <Styles>
                                                         <Cell Font-Size="10"  Wrap="True" Paddings-PaddingBottom="4px" Paddings-PaddingTop="4px" >
                                                         </Cell>
                                                     <Footer Font-Size="08" HorizontalAlign="Right" Wrap="True" >
                                                     </Footer>
                                
                                <Title Font-Size="10" VerticalAlign="Middle" HorizontalAlign="Center" Paddings-PaddingBottom="2px" Paddings-PaddingTop="2px">
                                </Title>
                                                    </Styles>
                                            </dx:ASPxGridViewExporter>
                                            <dx:ASPxGridViewExporter ID="gridexcel" GridViewID="grid" 

    PaperKind="A4" Landscape="True"  runat="server" >
</dx:ASPxGridViewExporter>
                                            <asp:SqlDataSource runat="server" ID="AccessDataSource1" 
                                                ProviderName="MySql.Data.MySqlClient" SelectCommand=" " />
                                            <asp:SqlDataSource runat="server" ID="AccessDataSource2" 
                                                ProviderName="MySql.Data.MySqlClient" SelectCommand=" " />
                                            <dx:ASPxGlobalEvents ID="ge" runat="server">
                                                <ClientSideEvents ControlsInitialized="OnControlsInitialized" />
                                            </dx:ASPxGlobalEvents>
                                        </dx:ContentControl>
                                    </ContentCollection>
                                </dx:TabPage>
                            </TabPages>
                        </dx:ASPxPageControl>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        $(document).ready(function () {
            $(".chzn-select").chosen({ search_contains: true });
            prth_mask_input.init();
        });
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $(".chzn-select").chosen({ search_contains: true });
        });
    </script>
</asp:Content>
