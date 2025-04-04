<%@ Page Culture="en-GB" Title="Edit Employee" Language="C#" MasterPageFile="~/Branch.Master"
    AutoEventWireup="true" CodeBehind="EditEmpDetails.aspx.cs" Inherits="SreeVisalamChitFundLtd_phase1.EditEmpDetails" %>

<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Head1" ContentPlaceHolderID="cphHead" runat="server">
    <style type="text/css">
        .dxic {
            width: 100%;
            padding-left: 0px !important;
            padding-right: 0px !important;
            padding-top: 0px !important;
            padding-bottom: 0px !important;
        }

        .dxeSBC {
            vertical-align: top;
        }

        .dxeEditArea {
            height: 20px !important;
        }

        .dxeEditAreaSys {
            height: 20px !important;
        }

        td[style="cursor:default;"] {
            vertical-align: middle;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="row">
        <div class="twelve columns">
            <div class="box_c">
                <div class="box_c_heading  box_actions">
                    <p>
                        Edit Employee Details
                    </p>
                </div>
                <div class="box_c_content">
                    <div class="row">
                        <br />
                        <div style="width: 100%; margin: 0 auto;">
                            <dx:ASPxGridView ID="gridBranch" ClientInstanceName="grid" Style="margin: 0 auto;"
                                runat="server" DataSourceID="DataSourceEmployee" KeyFieldName="Emp_ID" AutoGenerateColumns="False"
                                EnableRowsCache="False" OnStartRowEditing="gridBranch_StartRowEditing"
                                OnRowValidating="gridBranch_RowValidating" OnRowDeleting="gridBranch_RowDeleting"
                                OnRowUpdating="gridBranch_RowUpdating">
                                <Columns>
                                    <dx:GridViewCommandColumn Caption="Action" ButtonType="Image" VisibleIndex="0">
                                        <EditButton Visible="True" Text="Edit" Image-Url="~/Styles/Image/Edit16.png" />
                                        <DeleteButton Visible="true" Text="Delete" Image-Url="~/Styles/Image/del16.png">
                                        </DeleteButton>
                                    </dx:GridViewCommandColumn>
                                    <dx:GridViewDataTextColumn Visible="false" FieldName="BranchID" Caption="Branch Code" />
                                    <dx:GridViewDataTextColumn FieldName="Emp_ID" Caption="Employee ID" />
                                    <dx:GridViewDataTextColumn FieldName="Emp_Name" Caption="Name" />
                                    <dx:GridViewDataTextColumn FieldName="Emp_Address" Caption="Address" />
                                    <dx:GridViewDataTextColumn FieldName="Emp_PhoneNo" Caption="Phone Number" />
                                    <dx:GridViewDataTextColumn FieldName="Emp_Salary" Caption="Salary" />
                                    <dx:GridViewDataTextColumn FieldName="Emp_Designation" Caption="Designation" />
                                    <dx:GridViewDataTextColumn FieldName="Emp_Email" Caption="E-mail ID" />
                                    <dx:GridViewDataTextColumn FieldName="Emp_SrNumber" Caption="SR Number" />
                                    <dx:GridViewDataTextColumn FieldName="Emp_DateOfJoining" Caption="Date of Joining" />
                                </Columns>
                                <Settings ShowTitlePanel="true" ShowHeaderFilterButton="true" ShowFilterRow="true" />
                                <SettingsPager Mode="ShowPager" Position="Bottom" />
                                <Styles Cell-HorizontalAlign="Left" Header-Wrap="True" HeaderPanel-Wrap="True" Header-HorizontalAlign="Center" Header-VerticalAlign="Middle" CommandColumn-Paddings-Padding="10">
                                </Styles>
                                <SettingsBehavior ConfirmDelete="true" />
                                <SettingsText Title="Edit Employee Details" ConfirmDelete="Are You Sure You Want To Delete Branch?" />
                                <Templates>
                                    <EditForm>
                                        <table style="width: 100%; margin: 0 auto;">
                                            <tr>
                                                <td>
                                                    <dx:ASPxLabel ID="lbEmployeeID" runat="server" Text="Employee ID">
                                                    </dx:ASPxLabel>
                                                </td>
                                                <td>
                                                    <dx:ASPxTextBox runat="server" ID="edEmp_ID" ReadOnly="true" Text='<%# Bind("Emp_ID") %>'
                                                        Width="100%">
                                                    </dx:ASPxTextBox>
                                                </td>
                                            </tr>
                                            <tr>

                                                <td>
                                                    <dx:ASPxLabel ID="lbEmp_Name" Style="padding-left: 5px;" runat="server" Text="Employee Name">
                                                    </dx:ASPxLabel>
                                                </td>
                                                <td>
                                                    <%-- <dx:ASPxTextBox runat="server" ID="edEmp_Name" Value='<%# Bind("Emp_Name") %>' Width="100%"
                                                        ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                                        <ValidationSettings Display="Dynamic">
                                                            <RequiredField IsRequired="true" />
                                                        </ValidationSettings>
                                                    </dx:ASPxTextBox>--%>

                                                    <dx:ASPxTextBox runat="server" ID="ASPxTextBox4" Value='<%# Bind("Emp_Name") %>' Width="100%">
                                                    </dx:ASPxTextBox>
                                                </td>
                                                <td>
                                                    <dx:ASPxLabel ID="idDesignation" Style="padding-left: 5px;" runat="server" Text="Employee Designation">
                                                    </dx:ASPxLabel>
                                                </td>
                                                <td>
                                                    <%--     <dx:ASPxTextBox runat="server" ID="edDesignation" Value='<%# Bind("Emp_Designation") %>'
                                                        Width="100%" ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                                        <ValidationSettings Display="Dynamic">
                                                            <RequiredField IsRequired="true" />
                                                        </ValidationSettings>
                                                    </dx:ASPxTextBox>--%>
                                                    <dx:ASPxTextBox runat="server" ID="ASPxTextBox3" Value='<%# Bind("Emp_Designation") %>'
                                                        Width="100%">
                                                    </dx:ASPxTextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <dx:ASPxLabel ID="lbEmp_Address" runat="server" Text="Employee Address">
                                                    </dx:ASPxLabel>
                                                </td>
                                                <td colspan="5" style="padding-bottom: 10px;">
                                                    <%-- <dx:ASPxMemo Height="60px" runat="server" ID="edEmp_Address" Text='<%# Bind("Emp_Address") %>'
                                                        Width="100%" ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                                        <ValidationSettings Display="Dynamic">
                                                            <RequiredField IsRequired="true" />
                                                        </ValidationSettings>
                                                    </dx:ASPxMemo>--%>
                                                    <dx:ASPxMemo Height="60px" runat="server" ID="ASPxMemo1" Text='<%# Bind("Emp_Address") %>'
                                                        Width="100%">
                                                    </dx:ASPxMemo>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <dx:ASPxLabel ID="lbEmp_PhoneNo" runat="server" Text="Employee Phone No.">
                                                    </dx:ASPxLabel>
                                                </td>
                                                <td>
                                                    <%-- <dx:ASPxTextBox runat="server" ID="edEmp_PhoneNo" Value='<%# Bind("Emp_PhoneNo") %>'
                                                        Width="100%" ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                                        <ValidationSettings Display="Dynamic">
                                                            <RequiredField IsRequired="true" />
                                                            <RegularExpression ValidationExpression="(\+91)?\d{4,}(-)?\d{6,}" ErrorText="Invalid" />
                                                        </ValidationSettings>
                                                    </dx:ASPxTextBox>--%>
                                                    <dx:ASPxTextBox runat="server" ID="ASPxTextBox2" Value='<%# Bind("Emp_PhoneNo") %>'
                                                        Width="100%">
                                                    </dx:ASPxTextBox>
                                                </td>
                                                <td>
                                                    <dx:ASPxLabel ID="lbEmp_Salary" Style="padding-left: 5px;" runat="server" Text="Employee Salary">
                                                    </dx:ASPxLabel>
                                                </td>

                                                <td>
                                                    <%-- <dx:ASPxSpinEdit NumberType="Float" runat="server" ID="edEmp_Salary" Text='<%# Bind("Emp_Salary")%>'
                                                        Width="100%" ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                                        <ValidationSettings Display="Dynamic">
                                                            <RequiredField IsRequired="true" />
                                                            <RegularExpression ErrorText="Invalid" ValidationExpression="^\d*(\.?\d{0,2})$" />
                                                        </ValidationSettings>
                                                    </dx:ASPxSpinEdit>--%>
                                                    <dx:ASPxSpinEdit NumberType="Float" runat="server" ID="ASPxSpinEdit1" Text='<%# Bind("Emp_Salary")%>'
                                                        Width="100%">
                                                    </dx:ASPxSpinEdit>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <dx:ASPxLabel ID="lbEmail" Style="padding-left: 5px;" runat="server" Text="Employee E-mail ID">
                                                    </dx:ASPxLabel>
                                                </td>
                                                <td>
                                                    <%--<dx:ASPxTextBox runat="server" ID="edEmp_Email" Text='<%# Bind("Emp_Email")%>' Width="100%"
                                                        ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                                        <ValidationSettings Display="Dynamic">
                                                            <RequiredField IsRequired="False" />
                                                            <RegularExpression ValidationExpression="^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$"
                                                                ErrorText="Invalid" />
                                                        </ValidationSettings>
                                                    </dx:ASPxTextBox>--%>
                                                    <dx:ASPxTextBox runat="server" ID="ASPxTextBox1" Text='<%# Bind("Emp_Email")%>' Width="100%">
                                                    </dx:ASPxTextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <dx:ASPxLabel ID="ASPxLabel1" Style="padding-left: 5px;" runat="server" Text="SR Number">
                                                    </dx:ASPxLabel>
                                                </td>
                                                <td>
                                                    <dx:ASPxTextBox runat="server" ID="ASPxTextBox5" Text='<%# Bind("Emp_SrNumber")%>' Width="100%">
                                                    </dx:ASPxTextBox>
                                                </td>
                                                <td>
                                                    <dx:ASPxLabel ID="ASPxLabel2" Style="padding-left: 5px;" runat="server" Text="Date Of Joining">
                                                    </dx:ASPxLabel>
                                                </td>
                                                <td>
                                                    <dx:ASPxTextBox runat="server" ID="ASPxTextBox6" Text='<%# Bind("Emp_DateOfJoining")%>' Width="100%">
                                                    </dx:ASPxTextBox>
                                                </td>
                                            </tr>
                                        </table>
                                        <div style="text-align: right; padding: 2px; float: left;">
                                            <dx:ASPxGridViewTemplateReplacement ID="UpdateButton" ReplacementType="EditFormUpdateButton"
                                                runat="server"></dx:ASPxGridViewTemplateReplacement>
                                            <dx:ASPxGridViewTemplateReplacement ID="CancelButton" ReplacementType="EditFormCancelButton"
                                                runat="server"></dx:ASPxGridViewTemplateReplacement>
                                        </div>
                                    </EditForm>
                                </Templates>
                            </dx:ASPxGridView>
                            <asp:SqlDataSource runat="server" ID="DataSourceEmployee"
                                ProviderName="MySql.Data.MySqlClient" SelectCommand="" DeleteCommand="" UpdateCommand=""></asp:SqlDataSource>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
