<%@ Page Title="SVCF Admin Panel" Culture="en-GB" Language="C#" MasterPageFile="~/Branch.Master"
    AutoEventWireup="true" CodeBehind="ChitGropMemberDetails.aspx.cs" Inherits="SreeVisalamChitFundLtd_phase1.ChitGropMemberDetails" %>

<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <style type="text/css">
        td[style="cursor:default;"]
        {
            vertical-align: middle;
        }
        .chzn-results
        {
            text-align:center;
        }
        #ctl00_cphMainContent_ddlGroupNumber_chzn .chzn-drop .chzn-search input[type="text"]
        {
            height:15px;
        }
        #ctl00_cphMainContent_gridDetails_DXMainTable
        {
            display:none;
        }
        #ctl00_cphMainContent_gridDetails
        {
            border:0px ! important;
        }
        #ctl00_cphMainContent_gridDetails_DXTitle tbody tr td.dxgvTitlePanel
        {
            background-color:White !important;
            color:Black;
            border-color:White !important;
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="row">
        <div class="twelve columns">
            <div class="box_c">
                <div class="box_c_heading box_actions">
                    <p>
                        Group Member Details</p>
                </div>
                <div class="box_c_content">
                    <div class="row">
                        <div style="margin: 0px auto; width: 100%;">
                            <div style="display: table-cell; vertical-align: top; padding-top: 10px; padding-right: 5px !important;">
                                <asp:Label ID="Label3" runat="server" Text="Select Chit Group :"></asp:Label>
                            </div>
                            <div style="display: table-cell; vertical-align: top; padding-top: 5px; padding-right: 5px !important;">
                                <asp:DropDownList Width="240px" class="chzn-select" OnSelectedIndexChanged="ddlGroupNumber_OnSelectedIndexChanged"
                                    ID="ddlGroupNumber" runat="server" TabIndex="1">
                                </asp:DropDownList>
                            </div>
                            <div style="display: table-cell; vertical-align: top;">
                                <asp:Button TabIndex="2" ID="BtnStatisticsGo" runat="server" class="GreenyPushButton" OnClick="BtnStatisticsGo_Click"
                                    Text="Go!"></asp:Button>
                            </div>
                            
                            <br />
                        </div>
                        <dx:ASPxGridView Style="margin: 0 auto; width: 100%;" ID="gridDetails" ClientInstanceName="gridDetails"
                                runat="server" AutoGenerateColumns="false">
                                <Settings ShowGroupPanel="false"  ShowTitlePanel="true" />
                                </dx:ASPxGridView>
                      <%--  <asp:SqlDataSource runat="server" ID="SqlDataSource1" ConnectionString="server=192.168.0.36;database=svcf;pwd=sqlaltius;UID=root;Allow Zero Datetime=true;port=3306;Old Guids=true;pooling=true;Max Pool Size=200"
                                ProviderName="MySql.Data.MySqlClient" />  --%>
                                <br />      
                        <div style="width: 100%; margin: 0 auto;">
                            <dx:ASPxGridView Style="margin: 0 auto; width: 100%;" ID="grid" ClientInstanceName="grid"
                                runat="server">
                                <Settings ShowFilterRow="true" ShowTitlePanel="true" />
                                <SettingsText Title="Chit Group Member Details" />
                                <SettingsPager Mode="ShowAllRecords">
                                </SettingsPager>
                                <Columns>
                                    <dx:GridViewDataTextColumn FieldName="GrpMemberID" Caption="Token Number">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn FieldName="MemberName" Caption="Member Name">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn FieldName="MemberAddress" Width="100px" Caption="Member Address">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn FieldName="NomineeName" Caption="NomineeName">
                                    </dx:GridViewDataTextColumn>
                                      <dx:GridViewDataTextColumn FieldName="EstCallNoOfAuction" Caption="Est Call No Of Auction">
                                    </dx:GridViewDataTextColumn>
                                      <dx:GridViewDataTextColumn FieldName="EstSuretyDocument" Caption="Est Surety Document">
                                    </dx:GridViewDataTextColumn>
                                </Columns>
                            </dx:ASPxGridView>
                            
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        $(document).ready(function () {
            $(".chzn-select").chosen({ search_contains: true });
        });
    </script>
</asp:Content>
