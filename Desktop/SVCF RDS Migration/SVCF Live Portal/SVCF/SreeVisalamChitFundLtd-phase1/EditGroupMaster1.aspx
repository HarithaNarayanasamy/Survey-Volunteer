<%@ Page Culture="en-GB" Title="SVCF Admin Panel" Language="C#" MasterPageFile="~/Branch.Master"
    AutoEventWireup="true" CodeBehind="EditGroupMaster1.aspx.cs" Inherits="SreeVisalamChitFundLtd_phase1.WebForm7" %>

<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="TimePicker" Namespace="MKB.TimePicker" TagPrefix="MKB" %>

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
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="box_c" >
        <div class="box_c_heading box_actions">
            <p>
                Edit Group Details</p>
        </div>
        <div class="box_c_content">
            <div style="width: 100%; margin: 0 auto;  padding: 10px !important;">
                <dx:ASPxGridView ID="gridBranch" ClientInstanceName="grid" Style="margin: 0 auto;"
                    runat="server" DataSourceID="DataSourceEmployee" KeyFieldName="Head_Id" AutoGenerateColumns="False"
                    Width="100%" OnStartRowEditing="gridBranch_StartRowEditing"
                    OnRowValidating="gridBranch_RowValidating" OnRowDeleting="gridBranch_RowDeleting"
                    OnRowUpdating="gridBranch_RowUpdating">
                    <Columns>
                        <dx:GridViewCommandColumn Caption="Action" ButtonType="Image" VisibleIndex="0">
                            <EditButton Visible="True" Text="Edit" Image-Url="Styles/Image/Edit16.png" />
                            <DeleteButton Visible="True" Text="Delete" Image-Url="Styles/Image/del16.png">
                            </DeleteButton>
                        </dx:GridViewCommandColumn>
                        <dx:GridViewDataTextColumn Visible="false" FieldName="BranchID" Caption="Branch Id" />
                        <dx:GridViewDataTextColumn FieldName="GROUPNO" Caption="Group" />
                        <dx:GridViewDataTextColumn FieldName="PSOOrderNo" Caption="PSO Order Number" />
                        <dx:GridViewDataTextColumn FieldName="PSOOrderDate" Caption="PSO Order Date" />
                        <dx:GridViewDataTextColumn FieldName="PSODROffice" Caption="PSO D.R Office" />
                        <dx:GridViewDataTextColumn FieldName="ChitAgreementNo" Caption="Agreement Number" />
                        
                        <dx:GridViewDataTextColumn FieldName="AgreementDate" Caption="Agreement Date" />
                        <dx:GridViewDataTextColumn FieldName="ChitValue" Caption="Chit Value" />
                        <dx:GridViewDataTextColumn FieldName="ChitPeriod" Caption="Chit Period" />
                        <dx:GridViewDataTextColumn FieldName="ChitCategory" Caption="Chit Category" />
                        <dx:GridViewDataTextColumn FieldName="ChitStartDate" Caption="Chit Commencement Date" />
                        <dx:GridViewDataTextColumn FieldName="ChitEndDate" Caption="Chit Termination Date" />
                        <dx:GridViewDataTextColumn FieldName="NoofMembers" Caption="Number of Members" />
                        <dx:GridViewDataTextColumn FieldName="AuctionDate" Caption="Auction Date" />
                        <dx:GridViewDataTextColumn FieldName="AuctionTime" Caption="Auction Time" />
                        <dx:GridViewDataTextColumn FieldName="SDP_FDRNO" Caption="FDR Number" />
                        <dx:GridViewDataTextColumn FieldName="SDP_Bank" Caption="Bank Name" />
                        <dx:GridViewDataTextColumn FieldName="SDP_BankPlace" Caption="Bank Place" />
                        <dx:GridViewDataTextColumn FieldName="SDP_Commencement" Caption="FD Commencement" />
                        <dx:GridViewDataTextColumn FieldName="SDP_Maturity" Caption="FD Maturity" />
                        <dx:GridViewDataTextColumn FieldName="SDP_RateofInterest" Caption="FD Rate of Interest" />
                        <dx:GridViewDataTextColumn FieldName="SDP_PeriodinMonths" Caption="FD Period (inMonth)" />
                        <dx:GridViewDataTextColumn FieldName="SDP_Amount" Caption="FD Amount" />
                        <dx:GridViewDataTextColumn Visible="false" FieldName="Head_Id" Caption="Head_Id" />
                    </Columns>
                    <Settings ShowHorizontalScrollBar="true" ShowTitlePanel="true" ShowHeaderFilterButton="true" ShowFilterRow="true" />
                    <SettingsPager Mode="ShowPager" Position="TopAndBottom" />
                    <Styles Cell-HorizontalAlign="Left" Header-Wrap="True" Header-HorizontalAlign="Center" Header-VerticalAlign="Middle" CommandColumn-Paddings-Padding="10">
                    </Styles>
                    <SettingsBehavior ConfirmDelete="true" />
                    <SettingsText Title="EditGroupMaster1" ConfirmDelete="Are You Sure You Want Delete To Group Details?" />
                    <Templates>
                        <EditForm>
                            <table style="width: 100%; margin: 0 auto;">
                                <tr>
                                    <td>
                                        <dx:ASPxLabel ID="lbPSOOrderNo" runat="server" Text="PSO Order No.">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td style="padding-right:5px;">
                                        <dx:ASPxTextBox runat="server" ID="edPSOOrderNo" Text='<%# Bind("PSOOrderNo") %>'
                                            Width="100%" ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                            <ValidationSettings Display="Dynamic">
                                                <RequiredField IsRequired="true" />
                                            </ValidationSettings>
                                        </dx:ASPxTextBox>
                                    </td>
                                    <td>
                                        <dx:ASPxLabel ID="lbPSOOrderDate" runat="server" Text="PSO Order Date">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td  style="padding-right:5px;">
                                        <dx:ASPxDateEdit EditFormat="Date" EditFormatString="dd/MM/yyyy" UseMaskBehavior="true"
                                            runat="server" ID="edPSOOrderDate" Text='<%# Bind("PSOOrderDate") %>' Width="100%"
                                            ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                            <ValidationSettings Display="Dynamic">
                                                <RequiredField IsRequired="true" />
                                            </ValidationSettings>
                                        </dx:ASPxDateEdit>
                                    </td>
                                    <td>
                                        <dx:ASPxLabel ID="lbPSODROffice" runat="server" Text="PSO D.R Office">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td  style="padding-right:5px;">
                                        <dx:ASPxTextBox runat="server" ID="edPSODROffice" Text='<%# Bind("PSODROffice") %>'
                                            Width="100%" ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                            <ValidationSettings Display="Dynamic">
                                                <RequiredField IsRequired="true" />
                                            </ValidationSettings>
                                        </dx:ASPxTextBox>
                                    </td>
                                    <td >
                                        <dx:ASPxLabel ID="lbChitAgreementNo" runat="server" Text="Agreement Number">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td  style="padding-right:5px;">
                                        <dx:ASPxTextBox runat="server" ID="edChitAgreementNo" Text='<%# Bind("ChitAgreementNo") %>'
                                            Width="100%" ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                            <ValidationSettings Display="Dynamic">
                                                <RequiredField IsRequired="true" />
                                            </ValidationSettings>
                                        </dx:ASPxTextBox>
                                    </td>
                                    <td>
                                        <dx:ASPxLabel ID="lbAgreementDate" runat="server" Text="Agreement Date">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td  style="padding-right:5px;">
                                        <dx:ASPxDateEdit EditFormat="Date" EditFormatString="dd/MM/yyyy" UseMaskBehavior="true"
                                            runat="server" ID="edAgreementDate" Text='<%# Bind("AgreementDate") %>' Width="100%"
                                            ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                            <ValidationSettings Display="Dynamic">
                                                <RequiredField IsRequired="true" />
                                            </ValidationSettings>
                                        </dx:ASPxDateEdit>
                                    </td>
                                    <td>
                                        <dx:ASPxLabel ID="lblCommissionID" runat="server" Text="Commission %">
                                        </dx:ASPxLabel>
                                     </td>
                                    <td style="padding-right:5px;">
                                        <dx:aspxcombobox ID="edCommissionID" runat="server"  Text='<%# Bind("Commission_ID") %>' 
                                            Width="100%" ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                            
                                            <ValidationSettings Display="Dynamic">
                                                <RequiredField IsRequired="true" />
                                            </ValidationSettings>
                                            <Items>
                                                <dx:ListEditItem Text="5%" Value="5%" />
                                                <dx:ListEditItem Text="6%" value="6%"/>
                                                <dx:ListEditItem Text="7%" value="7%"/>
                                            </Items>
                                        </dx:aspxcombobox>
                                    </td>

                                </tr>
                                <tr>
                                    <td>
                                        <dx:ASPxLabel ID="lbChitValue" runat="server" Text="Chit Value">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td  style="padding-right:5px;">
                                        <dx:ASPxSpinEdit NumberType="Float" runat="server" ID="edChitValue" Text='<%# Bind("ChitValue") %>'
                                            Width="100%" ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                            <ValidationSettings Display="Dynamic">
                                                <RequiredField IsRequired="true" />
                                                <RegularExpression ErrorText="Invalid" ValidationExpression="^\d*(\.?\d{2,2})$" />
                                            </ValidationSettings>
                                        </dx:ASPxSpinEdit>
                                    </td>
                                    <td>
                                        <dx:ASPxLabel ID="lbChitPeriod" runat="server" Text="Chit Period">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td  style="padding-right:5px;">
                                        <dx:ASPxSpinEdit NumberType="Integer" MaxLength="2" runat="server" ID="edChitPeriod"
                                            Text='<%# Bind("ChitPeriod") %>' Width="100%" ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                            <ValidationSettings Display="Dynamic">
                                                <RequiredField IsRequired="true" />
                                                <RegularExpression ErrorText="Invalid" ValidationExpression="^\d{1,2}" />
                                            </ValidationSettings>
                                        </dx:ASPxSpinEdit>
                                    </td>
                                    <td>
                                        <dx:ASPxLabel ID="lbChitCategory" runat="server" Text="ChitCategory">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td  style="padding-right:5px;">
                                        <dx:ASPxComboBox runat="server" ID="edChitCategory" Text='<%# Bind("ChitCategory") %>'
                                            Width="100%" ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                            <Items>
                                                <dx:ListEditItem Text="Monthly" Value="Monthly" />
                                                <dx:ListEditItem Text="Trimonthly" Value="Trimonthly" />
                                                <dx:ListEditItem Text="Fortnightly" Value="Fortnightly" />
                                            </Items>
                                            <ValidationSettings Display="Dynamic">
                                                <RequiredField IsRequired="true" />
                                            </ValidationSettings>
                                        </dx:ASPxComboBox>
                                    </td>
                                    <td>
                                        <dx:ASPxLabel ID="lbChitStartDate" runat="server" Text="Chit Commencement Date">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td  style="padding-right:5px;">
                                        <dx:ASPxDateEdit EditFormat="Date" EditFormatString="dd/MM/yyyy" UseMaskBehavior="true"
                                            runat="server" ID="edChitStartDate" Text='<%# Bind("ChitStartDate") %>' Width="100%"
                                            ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                            <ValidationSettings Display="Dynamic">
                                                <RequiredField IsRequired="true" />
                                            </ValidationSettings>
                                        </dx:ASPxDateEdit>
                                    </td>
                                    <td>
                                        <dx:ASPxLabel ID="lbChitEndDate" runat="server" Text="Chit Termination Date">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td  style="padding-right:5px;">
                                        <dx:ASPxDateEdit EditFormat="Date" EditFormatString="dd/MM/yyyy" UseMaskBehavior="true"
                                            runat="server" ID="edChitEndDate" Text='<%# Bind("ChitEndDate") %>' Width="100%"
                                            ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                            <ValidationSettings Display="Dynamic">
                                                <RequiredField IsRequired="true" />
                                            </ValidationSettings>
                                        </dx:ASPxDateEdit>
                                    </td>
                                    <td>
                                        <dx:ASPxLabel ID="lbNoofMembers" runat="server" Text="No.of Members">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td>
                                        <dx:ASPxSpinEdit NumberType="Integer" MaxLength="2" runat="server" ID="edNoofMembers"
                                            Text='<%# Bind("NoofMembers") %>' Width="100%" ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                            <ValidationSettings Display="Dynamic">
                                                <RequiredField IsRequired="true" />
                                                <RegularExpression ErrorText="Invalid" ValidationExpression="^\d{1,2}" />
                                            </ValidationSettings>
                                        </dx:ASPxSpinEdit>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <dx:ASPxLabel ID="lbAuctionTime" runat="server" Text="Auction Time">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td  style="padding-right:5px;">
                                       <%-- <dx:ASPxTimeEdit runat="server" EditFormatString="hh:mm tt" EditFormat="Time" ID="edAuctionTime"
                                            Text='<%# Bind("AuctionTime")%>' Width="100%" ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                            <ValidationSettings Display="Dynamic">
                                                <RequiredField IsRequired="true" />
                                            </ValidationSettings>
                                        </dx:ASPxTimeEdit>--%>
                                         <dx:ASPxTextBox runat="server" ID="edAuctionTime" Text='<%# Bind("AuctionTime") %>'
                                            Width="100%" ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                            <ValidationSettings Display="Dynamic">
                                                <RequiredField IsRequired="true" />
                                            </ValidationSettings>
                                        </dx:ASPxTextBox>
                                         
                                    </td>

                                    <td>
                                        <dx:ASPxLabel ID="lbAuctionENDTime" runat="server" Text="AuctionEndTime">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td  style="padding-right:5px;">
                                       <%-- <dx:ASPxTimeEdit runat="server" EditFormatString="hh:mm tt" EditFormat="Time" ID="edAuctionTime"
                                            Text='<%# Bind("AuctionTime")%>' Width="100%" ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                            <ValidationSettings Display="Dynamic">
                                                <RequiredField IsRequired="true" />
                                            </ValidationSettings>
                                        </dx:ASPxTimeEdit>--%>
                                         <dx:ASPxTextBox runat="server" ID="edAuctionendTime" Text='<%# Bind("AuctionEndTime") %>'
                                            Width="100%" ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                            <ValidationSettings Display="Dynamic">
                                                <RequiredField IsRequired="true" />
                                            </ValidationSettings>
                                        </dx:ASPxTextBox>
                                         
                                    </td>
                                    <td>
                                        <dx:ASPxLabel ID="lbAuctionDate" runat="server" Text="Auction Date">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td  style="padding-right:5px;">
                                        <dx:ASPxDateEdit EditFormat="Date" EditFormatString="dd/MM/yyyy" UseMaskBehavior="true"
                                            runat="server" ID="edAuctionDate" Text='<%# Bind("AuctionDate") %>' Width="100%"
                                            ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                            <ValidationSettings Display="Dynamic">
                                                <RequiredField IsRequired="true" />
                                            </ValidationSettings>
                                        </dx:ASPxDateEdit>
                                        
                                    </td>
                                    <td>
                                        <dx:ASPxLabel ID="lbSDP_FDRNO" runat="server" Text="FDR Number">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td  style="padding-right:5px;">
                                        <dx:ASPxTextBox runat="server" ID="edSDP_FDRNO" Text='<%# Bind("SDP_FDRNO")%>' Width="100%"
                                            ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                            <ValidationSettings Display="Dynamic">
                                                <RequiredField IsRequired="true" />
                                            </ValidationSettings>
                                        </dx:ASPxTextBox>
                                    </td>
                                    <td>
                                        <dx:ASPxLabel ID="lbSDP_Bank" runat="server" Text="Bank Name">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td>
                                        <dx:ASPxTextBox runat="server" ID="edSDP_Bank" Text='<%# Bind("SDP_Bank")%>' Width="100%"
                                            ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                            <ValidationSettings Display="Dynamic">
                                                <RequiredField IsRequired="true" />
                                            </ValidationSettings>
                                        </dx:ASPxTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <dx:ASPxLabel ID="lbSDP_BankPlace" runat="server" Text="Bank Place">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td  style="padding-right:5px;">
                                        <dx:ASPxTextBox runat="server" ID="edSDP_BankPlace" Text='<%# Bind("SDP_BankPlace")%>'
                                            Width="100%" ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                            <ValidationSettings Display="Dynamic">
                                                <RequiredField IsRequired="true" />
                                            </ValidationSettings>
                                        </dx:ASPxTextBox>
                                    </td>
                                    <td>
                                        <dx:ASPxLabel ID="lbSDP_Commencement" runat="server" Text="FD Commencement">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td  style="padding-right:5px;">
                                        <dx:ASPxDateEdit EditFormat="Date" EditFormatString="dd/MM/yyyy" UseMaskBehavior="true"
                                            runat="server" ID="edSDP_Commencement" Text='<%# Bind("SDP_Commencement") %>'
                                            Width="100%" ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                            <ValidationSettings Display="Dynamic">
                                                <RequiredField IsRequired="true" />
                                            </ValidationSettings>
                                        </dx:ASPxDateEdit>
                                    </td>
                                    <td>
                                        <dx:ASPxLabel ID="lbSDP_Maturity" runat="server" Text="FD Maturity">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td  style="padding-right:5px;">
                                        <dx:ASPxDateEdit runat="server" EditFormat="Date" EditFormatString="dd/MM/yyyy" UseMaskBehavior="true"
                                            ID="edSDP_Maturity" Text='<%# Bind("SDP_Maturity") %>' Width="100%" ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                            <ValidationSettings Display="Dynamic">
                                                <RequiredField IsRequired="true" />
                                            </ValidationSettings>
                                        </dx:ASPxDateEdit>
                                    </td>
                                    <td>
                                        <dx:ASPxLabel ID="lbSDP_RateofInterest" runat="server" Text="FD Interest">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td  style="padding-right:5px;">
                                        <dx:ASPxSpinEdit NumberType="Float" runat="server" ID="ASPxSpinEdit1" Text='<%# Bind("SDP_RateofInterest") %>'
                                            Width="100%" ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                            <ValidationSettings Display="Dynamic">
                                                <RequiredField IsRequired="true" />
                                                <RegularExpression ErrorText="Invalid" ValidationExpression="^\d*(\.?\d{0,2})$" />
                                            </ValidationSettings>
                                        </dx:ASPxSpinEdit>
                                    </td>
                                    <td>
                                        <dx:ASPxLabel ID="lbSDP_PeriodinMonths" runat="server" Text="FD Period (in Months)">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td  style="padding-right:5px;">
                                        <dx:ASPxSpinEdit NumberType="Integer" MaxLength="2" runat="server" ID="edSDP_PeriodinMonths"
                                            Text='<%# Bind("SDP_PeriodinMonths") %>' Width="100%" ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                            <ValidationSettings Display="Dynamic">
                                                <RequiredField IsRequired="true" />
                                                <RegularExpression ErrorText="Invalid" ValidationExpression="^\d{1,2}$" />
                                            </ValidationSettings>
                                        </dx:ASPxSpinEdit>
                                    </td>
                                    <td>
                                        <dx:ASPxLabel ID="lbSDP_Amount" runat="server" Text="FD Amount">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td>
                                        <dx:ASPxSpinEdit NumberType="Float" runat="server" ID="edSDP_Amount" Text='<%# Bind("SDP_Amount") %>'
                                            Width="100%" ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                            <ValidationSettings Display="Dynamic">
                                                <RequiredField IsRequired="true" />
                                                <RegularExpression ErrorText="Invalid" ValidationExpression="^\d*(\.?\d{2,2})$" />
                                            </ValidationSettings>
                                        </dx:ASPxSpinEdit>
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
               <%-- <asp:SqlDataSource runat="server" ID="DataSourceEmployee" ConnectionString="server=192.168.0.36;database=svcf;pwd=sqlaltius;UID=root;Allow Zero Datetime=true;Convert Zero Datetime=True;port=3306" 
                    ProviderName="MySql.Data.MySqlClient"></asp:SqlDataSource>--%>

                  <asp:SqlDataSource runat="server" ID="DataSourceEmployee" 
                    ProviderName="MySql.Data.MySqlClient"></asp:SqlDataSource>
            </div>
        </div>
    </div>
</asp:Content>
