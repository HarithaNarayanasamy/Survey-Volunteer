<%@ Page Title="Commission details" Culture="en-GB" Language="C#" MasterPageFile="~/Branch.Master"
    AutoEventWireup="true" CodeBehind="commissiondetails.aspx.cs" Inherits="SreeVisalamChitFundLtd_phase1.WebForm11" %>

<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Head1" ContentPlaceHolderID="cphHead" runat="server">
<style type="text/css">
        .dxic
        {
            width: 100%;
            padding-left: 0px !important;
            padding-right: 0px !important;
            padding-top: 0px !important;
            padding-bottom: 0px !important;
        }
        .dxeSBC
        {
            vertical-align: top;
        }
        .dxeEditArea 
        {
            height:20px !important;
        }
        
        .dxeEditAreaSys 
        {
            height:20px !important;
        }
        td[style="cursor:default;"]
        {
            vertical-align:middle;
        }
    </style>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMainContent" runat="server">
    <ajax:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </ajax:ToolkitScriptManager>
    <div class="row">
        <div class="twelve columns">
            <div class="box_c">
                <div class="box_c_heading  box_actions">
                    <p>
                        Commision Details</p>
                </div>
                <div class="box_c_content">
                    <div class="row">
                        <br />
                        <div style="width: 100%;margin:0 auto;">
                            <dx:ASPxGridView ID="gridBranch" style="margin:0 auto;" ClientInstanceName="grid" runat="server" DataSourceID="DataSourceBranch"
                                KeyFieldName="SINo" AutoGenerateColumns="False" Width="100%" EnableRowsCache="False"
                                OnStartRowEditing="gridBranch_StartRowEditing" OnRowValidating="gridBranch_RowValidating"
                                OnRowDeleting="gridBranch_RowDeleting" OnRowUpdating="gridBranch_RowUpdating">
                                <Columns>
                                    <dx:GridViewCommandColumn Caption="Action" ButtonType="Image" VisibleIndex="0">
                                        <EditButton Visible="True" Text="Edit" Image-Url="Styles/Image/Edit16.png" />
                                        <DeleteButton Visible="true" Text="Delete" Image-Url="Styles/Image/del16.png">
                                        </DeleteButton>
                                    </dx:GridViewCommandColumn>
                                    <dx:GridViewDataTextColumn FieldName="SINo" Visible="false" Caption="SINo" />
                                    <dx:GridViewDataTextColumn FieldName="ChitValue" Caption="Chit Value" />
                                    <dx:GridViewDataTextColumn FieldName="Commission" Caption="Commission" />
                                    <dx:GridViewDataTextColumn FieldName="IncidentalCharges" Caption="Incidental Charges" />
                                   <%-- <dx:GridViewDataTextColumn FieldName="GstAmount" Caption="GST(Goods and Service Tax)" />--%>
                                    <dx:GridViewDataTextColumn FieldName="CgstAmount" Caption="CGST(Goods and Service Tax)" />
                                    <dx:GridViewDataTextColumn FieldName="SgstAmount" Caption="SGST(Goods and Service Tax)" />
                                    <dx:GridViewDataTextColumn FieldName="IgstAmount" Caption="IGST(Goods and Service Tax)" />
                                    <dx:GridViewDataTextColumn FieldName="Total" Caption="Total" />
                                  <%--  <dx:GridViewDataTextColumn FieldName="ValueOfTheTaxableService" Caption="Value Of Taxable Service" />
                                    <dx:GridViewDataTextColumn FieldName="ServiceTax" Caption="Service Tax" />--%>

                                </Columns>
                                <Settings ShowTitlePanel="true" ShowHeaderFilterButton="true" ShowFilterRow="true" />
                                <SettingsPager Mode="ShowPager" Position="TopAndBottom" />
                                <Styles Cell-HorizontalAlign="Left" CommandColumn-Paddings-Padding="10" Header-HorizontalAlign="Center" Header-VerticalAlign="Middle">
                                </Styles>
                                <SettingsBehavior ConfirmDelete="true" />
                                <SettingsText Title="Commision Details" ConfirmDelete="Are You Sure You Want To Delete Bank?" />
                                <Templates>
                                    <EditForm>
                                        <table style="width: 100%; margin: 0 auto;">
                                            <tr>
                                                <td>
                                                    <dx:ASPxLabel ID="lbSINo" Visible="false" runat="server" Text="SINo">
                                                    </dx:ASPxLabel>
                                                </td>
                                                <td>
                                                    <dx:ASPxSpinEdit NumberType="Integer" runat="server" ID="edSINo" Visible="false"  Text='<%# Bind("SINo") %>'
                                                        Width="100%" ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                                        <ValidationSettings Display="Dynamic">
                                                            <RequiredField IsRequired="true" />
                                                        </ValidationSettings>
                                                    </dx:ASPxSpinEdit >
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <dx:ASPxLabel ID="lbChitValue" runat="server" Text="Chit Value">
                                                    </dx:ASPxLabel>
                                                </td>
                                                <td>
                                                    <dx:ASPxSpinEdit NumberType="Float" runat="server" ID="edChitValue" Width="100%" Text='<%# Bind("ChitValue") %>'>
                                                    </dx:ASPxSpinEdit >
                                                   <%--   <dx:ASPxSpinEdit NumberType="Float" runat="server" ID="ASPxSpinEdit1" Width="100%" ValidationSettings-Display="Static" Text='<%# Bind("ChitValue") %>'
                                                        ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                                        <ValidationSettings Display="Dynamic">
                                                            <RequiredField IsRequired="true" />
                                                            <RegularExpression ErrorText="Invalid" ValidationExpression="^\d*(\.?\d{2,2})$" />
                                                        </ValidationSettings>
                                                    </dx:ASPxSpinEdit >--%>
                                                </td>
                                                <td  style="padding-left:6px;">
                                                    <dx:ASPxLabel ID="lbCommission" runat="server" Text="Commission">
                                                    </dx:ASPxLabel>
                                                </td>
                                                <td>
                                                      <dx:ASPxSpinEdit NumberType="Float" runat="server" ID="ASPxSpinEdit1" Width="100%"  Text='<%# Bind("Commission") %>' >                                                      
                                                    </dx:ASPxSpinEdit >
                                                 <%--   <dx:ASPxSpinEdit NumberType="Float" runat="server" ID="edCommission" Width="100%"  Text='<%# Bind("Commission") %>' ValidationSettings-Display="Static"
                                                        ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                                        <ValidationSettings Display="Dynamic">
                                                            <RequiredField IsRequired="true" />
                                                            <RegularExpression ErrorText="Invalid" ValidationExpression="^\d*(\.?\d{2,2})$" />
                                                        </ValidationSettings>
                                                    </dx:ASPxSpinEdit >--%>
                                                </td>
                                                <td  style="padding-left:6px;">
                                                    <dx:ASPxLabel ID="lbIncidentalCharges" runat="server" Text="Incidental Charges">
                                                    </dx:ASPxLabel>
                                                </td>
                                                <td>
                                                    <dx:ASPxSpinEdit NumberType="Float" runat="server" ID="edIncidentalCharges" Width="100%"  Text='<%# Bind("IncidentalCharges") %>'>                                                    
                                                    </dx:ASPxSpinEdit >
                                                  <%--   <dx:ASPxSpinEdit NumberType="Float" runat="server" ID="ASPxSpinEdit5" Width="100%"  Text='<%# Bind("IncidentalCharges") %>' ValidationSettings-Display="Static"
                                                        ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                                        <ValidationSettings Display="Dynamic">
                                                            <RequiredField IsRequired="true" />
                                                            <RegularExpression ErrorText="Invalid" ValidationExpression="^\d*(\.?\d{2,2})$" />
                                                        </ValidationSettings>
                                                    </dx:ASPxSpinEdit >--%>
                                                </td>
                                                 <%--<td  style="padding-left:6px;">
                                                    <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="Gst Amount">
                                                    </dx:ASPxLabel>
                                                </td>
                                                <td>
                                                    <dx:ASPxSpinEdit NumberType="Float" runat="server" ID="ASPxSpinEdit1" Width="100%"  Text='<%# Bind("GstAmount") %>' ValidationSettings-Display="Static"
                                                        ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                                        <ValidationSettings Display="Dynamic">
                                                            <RequiredField IsRequired="true" />
                                                            <RegularExpression ErrorText="Invalid" ValidationExpression="^\d*(\.?\d{2,2})$" />
                                                        </ValidationSettings>
                                                    </dx:ASPxSpinEdit >
                                                </td>--%>

                                            </tr>

                                            <tr>
                                                 <td  style="padding-left:6px;">
                                                    <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="CGST Amount">
                                                    </dx:ASPxLabel>
                                                </td>
                                                <td>
                                                    <dx:ASPxSpinEdit NumberType="Float" runat="server" ID="ASPxSpinEdit2" Width="100%"  Text='<%# Bind("CgstAmount") %>'>                                                      
                                                    </dx:ASPxSpinEdit >
                                                 <%--   <dx:ASPxSpinEdit NumberType="Float" runat="server" ID="ASPxSpinEdit5" Width="100%"  Text='<%# Bind("CgstAmount") %>' ValidationSettings-Display="Static"
                                                        ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                                        <ValidationSettings Display="Dynamic">
                                                            <RequiredField IsRequired="true" />
                                                            <RegularExpression ErrorText="Invalid" ValidationExpression="^\d*(\.?\d{2,2})$" />
                                                        </ValidationSettings>
                                                    </dx:ASPxSpinEdit >--%>
                                                </td>
                                                 <td  style="padding-left:6px;">
                                                    <dx:ASPxLabel ID="ASPxLabel3" runat="server" Text="SGST Amount">
                                                    </dx:ASPxLabel>
                                                </td>
                                                <td>
                                                    <dx:ASPxSpinEdit NumberType="Float" runat="server" ID="ASPxSpinEdit3" Width="100%"  Text='<%# Bind("SgstAmount") %>'>                                                      
                                                    </dx:ASPxSpinEdit >
                                                 <%--    <dx:ASPxSpinEdit NumberType="Float" runat="server" ID="ASPxSpinEdit5" Width="100%"  Text='<%# Bind("SgstAmount") %>' ValidationSettings-Display="Static"
                                                        ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                                        <ValidationSettings Display="Dynamic">
                                                            <RequiredField IsRequired="true" />
                                                            <RegularExpression ErrorText="Invalid" ValidationExpression="^\d*(\.?\d{2,2})$" />
                                                        </ValidationSettings>
                                                    </dx:ASPxSpinEdit >--%>
                                                </td>
                                                 <td  style="padding-left:6px;">
                                                    <dx:ASPxLabel ID="ASPxLabel4" runat="server" Text="IGST Amount">
                                                    </dx:ASPxLabel>
                                                </td>
                                                <td>
                                                    <dx:ASPxSpinEdit NumberType="Float" runat="server" ID="ASPxSpinEdit4" Width="100%"  Text='<%# Bind("IgstAmount") %>'>                                                      
                                                    </dx:ASPxSpinEdit >
                                                    <%--   <dx:ASPxSpinEdit NumberType="Float" runat="server" ID="ASPxSpinEdit5" Width="100%"  Text='<%# Bind("IgstAmount") %>' ValidationSettings-Display="Static"
                                                        ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                                        <ValidationSettings Display="Dynamic">
                                                            <RequiredField IsRequired="true" />
                                                            <RegularExpression ErrorText="Invalid" ValidationExpression="^\d*(\.?\d{2,2})$" />
                                                        </ValidationSettings>
                                                    </dx:ASPxSpinEdit >--%>
                                                </td>
                                                <td style="padding-top:13px;">
                                                    <dx:ASPxLabel ID="lbTotal" runat="server" Text="Total">
                                                    </dx:ASPxLabel>
                                                </td>
                                                <td style="padding-top:13px;">
                                                    <dx:ASPxSpinEdit NumberType="Float" runat="server" ID="edTotal"  Text='<%# Bind("Total") %>'>                                                      
                                                    </dx:ASPxSpinEdit >
                                                <%--    <dx:ASPxSpinEdit NumberType="Float" runat="server" ID="ASPxSpinEdit5"  Text='<%# Bind("Total") %>' ValidationSettings-Display="Static"
                                                        Width="100%" ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                                        <ValidationSettings Display="Dynamic">
                                                            <RequiredField IsRequired="true" />
                                                            <RegularExpression ErrorText="Invalid" ValidationExpression="^\d*(\.?\d{2,2})$" />
                                                        </ValidationSettings>
                                                    </dx:ASPxSpinEdit >--%>
                                                </td>
                                               <%-- <td  style="padding-left:6px;">
                                                    <dx:ASPxLabel ID="lbValueOfTheTaxableService" runat="server" Text="Value Of Taxable Service">
                                                    </dx:ASPxLabel>
                                                </td>
                                                <td>
                                                    <dx:ASPxSpinEdit NumberType="Float" Text='<%# Bind("ValueOfTheTaxableService") %>'
                                                        runat="server" ID="edValueOfTheTaxableService" Width="100%" ValidationSettings-Display="Static"
                                                        ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                                        <ValidationSettings Display="Dynamic">
                                                            <RequiredField IsRequired="true" />
                                                            <RegularExpression ErrorText="Invalid" ValidationExpression="^\d*(\.?\d{2,2})$" />
                                                        </ValidationSettings>
                                                    </dx:ASPxSpinEdit >
                                                </td>--%>
                                                <%--<td  style="padding-left:6px;">
                                                    <dx:ASPxLabel ID="lbServiceTax" runat="server" Text="Service Tax">
                                                    </dx:ASPxLabel>
                                                </td>
                                                <td>
                                                    <dx:ASPxSpinEdit NumberType="Float" runat="server" ID="edServiceTax" Text='<%# Bind("ServiceTax") %>'
                                                        Width="100%" ValidationSettings-Display="Static" ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                                        <ValidationSettings Display="Dynamic">
                                                            <RequiredField IsRequired="true" />
                                                            <RegularExpression ErrorText="Invalid" ValidationExpression="^\d*(\.?\d{2,2})$" />
                                                        </ValidationSettings>
                                                    </dx:ASPxSpinEdit>
                                                </td>--%>
                                            </tr>
                                        </table>
                                        <div style="text-align: right; padding-left: 2px;padding-top:5px; float: left;">
                                            <dx:ASPxGridViewTemplateReplacement ID="UpdateButton" ReplacementType="EditFormUpdateButton"
                                                runat="server"></dx:ASPxGridViewTemplateReplacement>
                                            <dx:ASPxGridViewTemplateReplacement ID="CancelButton" ReplacementType="EditFormCancelButton"
                                                runat="server"></dx:ASPxGridViewTemplateReplacement>
                                        </div>
                                    </EditForm>
                                </Templates>
                            </dx:ASPxGridView>
                            <asp:SqlDataSource runat="server" ID="DataSourceBranch" ProviderName="MySql.Data.MySqlClient"></asp:SqlDataSource>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
