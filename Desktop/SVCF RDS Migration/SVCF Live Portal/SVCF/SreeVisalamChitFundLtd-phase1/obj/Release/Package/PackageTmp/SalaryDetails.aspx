<%@ Page Title="SVCF Admin Panel" Culture="en-GB" Language="C#" MasterPageFile="~/Branch.Master"
    AutoEventWireup="true" CodeBehind="SalaryDetails.aspx.cs" Inherits="SreeVisalamChitFundLtd_phase1.SalaryDetails" %>

<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="cphMainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="row">
        <div class="twelve columns">
            <div class="box_c">
                <div class="box_c_heading box_actions">
                    <p>
                        Salary Details
                    </p>
                </div>
                <div class="box_c_content">
                    <div class="row">
                        <div style="margin: 0px auto; width: 100%;">

                            <div style="display: table-cell; vertical-align: top;">
                                <%-- <asp:Button TabIndex="2" ID="BtnStatisticsGo" runat="server" class="GreenyPushButton" OnClick="Button1_Click"
                                    Text="New Create"></asp:Button>--%>

                                <input type="button" class="GreenyPushButton" onclick="SalaryCreate();" value="New create" />
                            </div>

                            <br />
                        </div>


                        <div style="width: 100%; margin: 0 auto;">
                            <dx:ASPxGridView ID="gridSalary" Style="margin: 0 auto;" ClientInstanceName="grid" runat="server" DataSourceID="DataSourceSalary"
                                KeyFieldName="DualTransactionKey" AutoGenerateColumns="False" Width="100%" EnableRowsCache="False"
                                OnRowDeleting="gridSalaryDetails_RowDeleting">
                                <%--OnRowUpdating="gridSalaryDetails_RowUpdating"--%>
                                <Columns>
                                    <dx:GridViewCommandColumn Caption="Action" ButtonType="Image" VisibleIndex="0">
                                        <%--<EditButton  Visible="True" Text="Edit" Image-Url="Styles/Image/Edit16.png"></EditButton>--%>
                                        <DeleteButton Visible="true" Text="Delete" Image-Url="Styles/Image/del16.png">
                                        </DeleteButton>
                                    </dx:GridViewCommandColumn>
                                    <dx:GridViewDataTextColumn FieldName="DualTransactionKey" Visible="false" Caption="DualTransactionKey" />
                                    <dx:GridViewDataTextColumn FieldName="EmployeeHeadId" Visible="false" Caption="EmployeeHeadId" />
                                    <dx:GridViewDataTextColumn FieldName="EmployeeName" Caption="Employee Name" />
                                    <dx:GridViewDataTextColumn FieldName="ChoosenDate"  Caption="Date" />
                                    <dx:GridViewDataTextColumn FieldName="CreditAmount" Caption="Credit Amount" />
                                    <dx:GridViewDataTextColumn FieldName="DebitAmount" Caption="Debit Amount" />
                                </Columns>
                                <Settings ShowTitlePanel="true"
                                    ShowHeaderFilterButton="true" ShowFilterRow="true" />
                                <SettingsPager Mode="ShowPager" Position="TopAndBottom" />
                                <Styles Header-Wrap="True" HeaderPanel-Wrap="True" Header-HorizontalAlign="Center" Header-VerticalAlign="Middle" CommandColumn-Paddings-Padding="10">
                                </Styles>
                                <SettingsBehavior ConfirmDelete="true" />
                                <SettingsText Title="Salary Details" ConfirmDelete="Are You Sure You Want To Delete Salary Details?" />
                            </dx:ASPxGridView>

                        </div>
                        <asp:SqlDataSource runat="server" ID="DataSourceSalary" ConnectionString="server=db.sreevisalam.internal;database=svcf;UID=svcf_admin;PWD=5buo0e0i495Cpnn;Allow Zero Datetime=true;Convert Zero Datetime=True;port=3306"
                            ProviderName="MySql.Data.MySqlClient"></asp:SqlDataSource>


                    </div>
                </div>
            </div>
        </div>
    </div>


    <script type="text/javascript">
        function SalaryCreate() {
            window.location.href = "SalaryCreate.aspx"
        }
    </script>
</asp:Content>
