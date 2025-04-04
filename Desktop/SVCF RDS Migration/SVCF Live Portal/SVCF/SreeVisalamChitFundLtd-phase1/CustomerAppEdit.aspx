<%@ Page Title="" Language="C#" MasterPageFile="~/Branch.Master" AutoEventWireup="true" CodeBehind="CustomerAppEdit.aspx.cs" Inherits="SreeVisalamChitFundLtd_phase1.CustomerAppEdit" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
     <style type="text/css">
        input[type="image"]:active
        {
            -moz-box-shadow: inset 0 0 10px rgba(0,0,0,.06);
            -webkit-box-shadow: inset 0 0 10px rgba(0,0,0,.06);
            box-shadow: inset 0 0 10px rgba(0,0,0,.06);
            z-index: 6;
            border: 2px solid #006469 !important;
        }
        #ctl00_cphMainContent_ddlChitGroup_chzn .chzn-drop .chzn-search input[type="text"]
        {
            height: 15px;
        }
        #ctl00_cphMainContent_ddlMembName_chzn .chzn-drop .chzn-search input[type="text"]
        {
            height: 15px;
        }
        #ctl00_cphMainContent_ddlMoneyCollector_chzn .chzn-drop .chzn-search input[type="text"]
        {
            height: 15px;
        }
        .chzn-drop
        {
            border: 1px solid #aaa !important;
            padding-top: 5px !important;
            width: 240px !important;
        }
        .chzn-results
        {
            text-align: center !important;
        }
    </style>
    <script type="text/javascript">

        function clearValidationErrors() {
            //Hide all validation errors
            if (window.Page_Validators)
                for (var vI = 0; vI < Page_Validators.length; vI++) {
                    var vValidator = Page_Validators[vI];
                    vValidator.isvalid = true;
                    ValidatorUpdateDisplay(vValidator);
                }
            //Hide all validaiton summaries
            if (typeof (Page_ValidationSummaries) != "undefined") { //hide the validation summaries
                for (sums = 0; sums < Page_ValidationSummaries.length; sums++) {
                    summary = Page_ValidationSummaries[sums];
                    summary.style.display = "none";
                }
            }
        }
    
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMainContent" runat="server">
      <ajax:ToolkitScriptManager ID="ToolkitScriPTr1" runat="server">
    </ajax:ToolkitScriptManager>
    <div class="row">
        <div class="twelve columns">
            <div class="box_c">
                <div class="box_c_heading  box_actions">
                    <p>
                       Member Login</p>
                </div>
                <div class="box_c_content">
                    <div class="row">
                        <br />
                        <div style="width: 100%">
                            <asp:UpdatePanel runat="server" ID="up1">
                                <ContentTemplate>
                                    <table style="margin: 0 auto; width: 90%">
                                      
                                        <tr>
                                          
                                            <td style="vertical-align: top; padding-top: 6px !important;">
                                                <asp:Label ID="LabBN" runat="server" Text="Branch Name"></asp:Label>
                                            </td>
                                            <td style="vertical-align: top;">
                                                <asp:DropDownList  Width="240px" CssClass="chzn-select"
                                                    runat="server" ID="DDLBName" AutoPostBack="true" OnSelectedIndexChanged="DDLBName_Click" 
                                                    TabIndex="2" >
                                                </asp:DropDownList>
                                               <asp:Label ID="lbn1" runat="server" Visible="false"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                         
                                            <td style="vertical-align: top; padding-top: 6px !important;">
                                                <asp:Label ID="LabMemname" runat="server" Text="Member Name"></asp:Label>
                                            </td>
                                            <td style="vertical-align: top;">
                                                <asp:DropDownList  Width="240px" CssClass="chzn-select"
                                                    runat="server" ID="ddlMName" AutoPostBack="true" 
                                                    TabIndex="2" >
                                                </asp:DropDownList>
                                               <%-- <asp:CompareValidator ValidationGroup="sug" ID="CompareV1" runat="server"
                                                    Display="Dynamic" ControlToValidate="ddlMName" ErrorMessage="*" Operator="NotEqual"
                                                    ValueToCompare="0"></asp:CompareValidator>--%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="vertical-align: top; padding-top: 6px !important;">
                                                <asp:Label ID="labname" runat="server" Text="User Name"></asp:Label>
                                            </td>
                                            <td style="vertical-align: top;">
                                                <asp:TextBox ID="txtusername" runat="server" CssClass="input-text" TabIndex="3"></asp:TextBox>
                                                <%--<input id="txtusername" type="text" style="width: 75px; height: 15px;" />--%>
                                                <%--<select id="abcd1" style="width: 150px;" onchange="FindUserName();"></select>--%>
                                                <asp:RequiredFieldValidator ValidationGroup="sug" ID="rv" runat="server" Display="Dynamic"
                                                    ErrorMessage="*" ControlToValidate="txtusername">
                                                </asp:RequiredFieldValidator>
                                            </td>

                                        </tr>

                                          <tr>
                                            <td style="vertical-align: top; padding-top: 6px !important;">
                                                <asp:Label ID="Labpsw" runat="server" Text="Password"></asp:Label>
                                            </td>
                                            <td style="vertical-align: top;">
                                                <asp:TextBox ID="txtpsw" runat="server" CssClass="input-text" TabIndex="3"></asp:TextBox>
                                                <asp:RequiredFieldValidator ValidationGroup="sug" ID="RequiredFieldValidator1" runat="server" Display="Dynamic"
                                                    ErrorMessage="*" ControlToValidate="txtpsw">
                                                </asp:RequiredFieldValidator>
                                            </td>

                                        </tr>
                                           
                       
                                      
                                    </table>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <div style="width: 100% !important;">
                                <asp:Button ID="btnADD" ValidationGroup="sug" CausesValidation="True" CssClass="GreenyPushButton"
                                    Text="Save" Style="width: 120px; float: right;" runat="server" OnClick="btnADD_Click">
                                </asp:Button>

                                
                            </div>
                        </div>
                    </div>
                 
                </div>
            </div>
        </div>
    </div>
 

     <div class="box_c_content">
                    <div class="row">
                        <div style="width:100%; margin: 0 auto;">
                            <dx:ASPxGridView ID="gridBranch"  Style="margin: 0 auto; width: 100%;" ClientInstanceName="grid"
                                runat="server" DataSourceID="DataSourceBranch" KeyFieldName="MemberIDNew" AutoGenerateColumns="False"
                                EnableRowsCache="False" OnStartRowEditing="gridBranch_StartRowEditing"
                                OnRowDeleting="gridBranch_RowDeleting"
                                OnRowUpdating="gridBranch_RowUpdating">
                                <Columns>
                                    <dx:GridViewCommandColumn Caption="Action" ButtonType="Image" VisibleIndex="0">
                                        <EditButton Visible="True" Text="Edit" Image-Url="Styles/Image/Edit16.png" />
                                        <DeleteButton Visible="false" Text="Delete" Image-Url="Styles/Image/del16.png">
                                        </DeleteButton>
                                    </dx:GridViewCommandColumn>
                                    <dx:GridViewDataTextColumn FieldName="CustomerName" Caption="Customer Name" />
                                    <dx:GridViewDataTextColumn FieldName="username" Caption="User Name" />
                                    <dx:GridViewDataTextColumn FieldName="password" Caption="Password" />
                                    <dx:GridViewDataTextColumn FieldName="B_Name" Caption="Branch Name" />
                                     <dx:GridViewDataTextColumn FieldName="MemberIDNew" Caption="Member ID" />
                                  
                                </Columns>
                                <Settings ShowTitlePanel="true" VerticalScrollableHeight="430" ShowHeaderFilterButton="true"
                                    ShowFilterRow="true" />
                                <SettingsPager Mode="ShowPager" Position="TopAndBottom" />
                                <SettingsText Title="Edit Branch Details" />
                                <Styles Cell-HorizontalAlign="Left" Footer-HorizontalAlign="Left" Header-Wrap="True" HeaderPanel-Wrap="True" Header-HorizontalAlign="Center" Header-VerticalAlign="Middle" CommandColumn-Paddings-Padding="10">
                                </Styles>
                                <SettingsBehavior ConfirmDelete="true" />
                                <SettingsText ConfirmDelete="Are You Sure You Want To Delete Branch?" />
                                <Templates>
                                    <EditForm>
                                        <table style="width: 100%; margin: 0 auto;">
                                            <tr>
                                                <td style="vertical-align:middle;">
                                                    <dx:ASPxLabel  Width="100%" ID="lbCustomerName" runat="server" Text="Customer Name">
                                                    </dx:ASPxLabel>
                                                </td>
                                                <td>
                                                    <dx:ASPxTextBox runat="server" ReadOnly="true" ID="edFirst" Text='<%# Bind("CustomerName") %>'
                                                        Width="100%" ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                                        <ValidationSettings Display="Dynamic">
                                                            <RequiredField  IsRequired="true" />
                                                        </ValidationSettings>
                                                    </dx:ASPxTextBox>
                                                </td>
                                                <td style="vertical-align:middle;padding-left:5px;">
                                                    <dx:ASPxLabel Width="100%" ID="lbusername" runat="server" Text="User Name">
                                                    </dx:ASPxLabel>
                                                </td>
                                                <td>
                                                    <dx:ASPxTextBox runat="server" ID="edLast" Text='<%# Bind("username") %>'
                                                        Width="100%" ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                                        <ValidationSettings Display="Dynamic">
                                                            <RequiredField IsRequired="true" />
                                                        </ValidationSettings>
                                                    </dx:ASPxTextBox>
                                                </td>
                                                <td style="vertical-align:middle;padding-left:5px;">
                                                    <dx:ASPxLabel Width="100%" ID="lbpassword" runat="server" Text="Password">
                                                    </dx:ASPxLabel>
                                                </td>
                                                <td>
                                                    <dx:ASPxTextBox runat="server" ID="edTitle" Text='<%# Bind("password") %>' Width="100%"
                                                        ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                                        <ValidationSettings Display="Dynamic">
                                                            <RequiredField IsRequired="true" />
                                                        </ValidationSettings>
                                                    </dx:ASPxTextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="vertical-align:middle;">
                                                    <dx:ASPxLabel Width="100%" ID="lbB_Name" runat="server" Text="Branch Name">
                                                    </dx:ASPxLabel>
                                                </td>
                                                <td colspan="5">
                                                    <dx:ASPxMemo runat="server" ID="edBirth" Text='<%# Bind("B_Name") %>' Height="60px"
                                                       ReadOnly="true"  Width="100%" ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                                        <ValidationSettings Display="Dynamic">
                                                            <RequiredField IsRequired="true" />
                                                        </ValidationSettings>
                                                    </dx:ASPxMemo>
                                                    </ValidationSettings>
                                                </td>
                                            </tr>
                                            
                                              <tr>
                                                <td style="vertical-align:middle;">
                                                    <dx:ASPxLabel Width="100%" ID="lbMemberIDNew" runat="server" Text="MemberIDNew">
                                                    </dx:ASPxLabel>
                                                </td>
                                                <td colspan="5">
                                                    <dx:ASPxMemo runat="server" ID="ASPxMemo1" Text='<%# Bind("MemberIDNew") %>' Height="60px"
                                                    ReadOnly="true"    Width="100%" ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                                        <ValidationSettings Display="Dynamic">
                                                            <RequiredField IsRequired="true" />
                                                        </ValidationSettings>
                                                    </dx:ASPxMemo>
                                                    </ValidationSettings>
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
                        </div>
                    </div>
                </div>
                <asp:SqlDataSource runat="server" ID="DataSourceBranch" ProviderName="MySql.Data.MySqlClient" >
                </asp:SqlDataSource>


                      <asp:SqlDataSource runat="server" ID="DataSourceMember" ProviderName="MySql.Data.MySqlClient" >
                </asp:SqlDataSource>
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

        //16/07/2021 - Bala
        //$(document).ready(function () {
        //    $('#txtusername').change(function () {
        //        var txt = $('#txtusername').val();
        //        if (txt != "") {
        //            $.ajax({
        //                type: "POST",
        //                contentType: "application/json; charset=utf-8",
        //                url: "CustomerAppEdit.aspx/GetUserName",
        //                data: JSON.stringify({ username: txt }),
        //                dataType: "json",
        //                error: function (result) {
        //                    alert("Error: " + result);
        //                }

        //            })
        //        }
        //    });
        //});

    </script>
</asp:Content>
