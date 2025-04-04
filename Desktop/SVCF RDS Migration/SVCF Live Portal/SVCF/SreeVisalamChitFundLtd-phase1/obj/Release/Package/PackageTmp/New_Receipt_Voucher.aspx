<%@ Page Title="SVCF Admin Panel" Culture="en-GB" Language="C#" MaintainScrollPositionOnPostback="true"
    MasterPageFile="~/Branch.Master" AutoEventWireup="true" CodeBehind="New_Receipt_Voucher.aspx.cs"
    Inherits="SreeVisalamChitFundLtd_phase1.New_Receipt_Voucher" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMainContent" runat="server">
    <link href="Styles/tablecss.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        input[type="text"]
        {
            margin-bottom: 3px;
        }
        
        input[type="image"]:active
        {
            -moz-box-shadow: inset 0 0 10px rgba(0,0,0,.06);
            -webkit-box-shadow: inset 0 0 10px rgba(0,0,0,.06);
            box-shadow: inset 0 0 10px rgba(0,0,0,.06);
            z-index: 6;
            border: 2px solid #006469 !important;
        }
        .Trans div[id*="chzn"]
        {
            width: 100% !important;
        }
        .Trans div[id*="chzn"] span
        {
            width: 140px !important;
            overflow: hidden;
        }
        .Trans td
        {
            width: 14% !important;
            padding-left: 8px !important;
            padding-right: 8px !important;
        }
        .Trans td > input[type="text"]
        {
            width: 100% !important;
        }
        
        .Trans td div[class="ui-spinner"] > input[type="text"]
        {
            width: 100% !important;
        }
        .Trans select
        {
            width: 100% !important;
        }
        .hidable table
        {
            vertical-align: middle;
        }
        .hidable td
        {
            padding: 3px;
            magin: 3px;
            vertical-align: middle !important;
        }
        .hidable div
        {
            vertical-align: middle;
        }
        .Date
        {
            position: absolute;
            z-index: 9999px;
        }
        
        .chzn-drop
        {
            border: 1px solid #aaa !important;
            padding-top: 5px !important;
            width: 210px !important;
        }
        div[id*="ddlMemberName_chzn"] .chzn-drop
        {
            width: 300% !important;
        }
        div[id*="ddlMisc"] .chzn-drop
        {
            width: 200% !important;
        }
    </style>
    <ajax:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </ajax:ToolkitScriptManager>
    <div class="row">
        <div class="twelve columns">
            <div class="box_c">
                <div class="box_c_heading  box_actions">
                    <p>
                        Receipt Details</p>
                </div>
                <div class="box_c_content">
                    <div class="row">
                        <br />
                        <div style="display: inline; zoom: 1; text-align: center; width: 100%;">
                            <div style="margin-left: auto; margin-right: auto; display: table; width: 100%;">
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <div>
                                            <div style="width: 100%;text-align:center !important; margin: 0 auto;">
                                                
                                                <table cellspacing="5" width="100%">
                                                    <tr>
                                                        <td style="padding:5px !important;vertical-align:middle !important;">
                                                            <asp:CheckBox Style="font-size: 13px; font-weight: bold; font-family: Helvetica, Arial, sans-serif;
                                                                color: #484848;" ID="chkLoadAllChit" runat="server" TabIndex="1" CausesValidation="false"
                                                                AutoPostBack="true" OnCheckedChanged="chkLoadAllChit_CheckedChanged" Text="Load All Branch Chits" />
                                                        </td>
                                                        <td style="padding:5px !important;">
                                                            <asp:Label ID="Label1" runat="server" Text="Collector Name"></asp:Label>
                                                            <br />
                                                            <asp:DropDownList Style="width: 200px !important;" TabIndex="2" ID="ddlColloctorName"
                                                                CssClass="chzn-select" runat="server" AutoPostBack="True" CausesValidation="false"
                                                                OnSelectedIndexChanged="ddlColloctorName_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                            <asp:CompareValidator Style="margin-top: -2px;" EnableClientScript="false" ID="CVddlColloctorName"
                                                                ValueToCompare="0" ValidationGroup="a" ControlToValidate="ddlColloctorName" ForeColor="Red"
                                                                SetFocusOnError="true" Display="Static" Operator="NotEqual" runat="server" ErrorMessage="*"></asp:CompareValidator>
                                                        </td>
                                                        <td style="padding:5px !important;">
                                                            <asp:Label ID="Label2" runat="server" Text="Receipt Series"></asp:Label>
                                                            <br />
                                                            <asp:DropDownList Style="width: 150px !important;" ID="ddlReceiptSeries" CssClass="chzn-select"
                                                                TabIndex="3" runat="server" AutoPostBack="True" CausesValidation="false" OnSelectedIndexChanged="ddlReceiptSeries_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                            <asp:CompareValidator EnableClientScript="false" ID="CVddlReceiptSeries" ValueToCompare="--Select--" ForeColor="Red"
                                                                ControlToValidate="ddlReceiptSeries" Display="Static" Operator="NotEqual" SetFocusOnError="true"
                                                                ValidationGroup="a" runat="server" ErrorMessage="*"></asp:CompareValidator>
                                                        </td>
                                                        <td style="padding:5px !important;">
                                                            <asp:Label ID="Label3" runat="server" Text="Received By"></asp:Label>
                                                            <br />
                                                            <asp:TextBox CausesValidation="false" ID="txtReceivedBy" runat="server" TabIndex="4"
                                                                ValidationGroup="a" CssClass="twitterStyleTextbox"></asp:TextBox>
                                                            <asp:RequiredFieldValidator EnableClientScript="false" ID="RFVtxtReceivedBy" runat="server"
                                                                Display="Static" SetFocusOnError="true" ControlToValidate="txtReceivedBy" ValidationGroup="a"
                                                                ForeColor="Red" ErrorMessage="*"></asp:RequiredFieldValidator>
                                                        </td>
                                                        <td style="padding:5px !important;">
                                                            <asp:Label ID="Label6" runat="server" Text="Total Amount"></asp:Label>
                                                            <br />
                                                            <asp:TextBox ID="txtTotalAmount" CausesValidation="false" runat="server" TabIndex="5" Width="120"
                                                                ValidationGroup="a" CssClass="twitterStyleTextbox sp_currency" autocomplete="off"></asp:TextBox>
                                                            <asp:RequiredFieldValidator EnableClientScript="false" ID="RFVtxtTotalAmount" runat="server"
                                                                Display="Static" SetFocusOnError="true" ControlToValidate="txtTotalAmount" ValidationGroup="a"
                                                                ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                                            <asp:RegularExpressionValidator EnableClientScript="false" ID="REVtxtTotalAmount" ForeColor="Red"
                                                                runat="server" Display="Dynamic" SetFocusOnError="true" ValidationExpression="^\d+(\.\d{1,2})?$"
                                                                ControlToValidate="txtTotalAmount" ValidationGroup="a" ErrorMessage="Invalid Amount!!!"></asp:RegularExpressionValidator>
                                                        </td>
                                                        <td style="padding:5px !important;">
                                                            <asp:Label ID="Label5" runat="server" Text="Receipt Date"></asp:Label>
                                                            <br />
                                                            <asp:TextBox ID="txtReceivedDate" Width="90" CausesValidation="false" runat="server"
                                                                TabIndex="6" ValidationGroup="a" CssClass="twitterStyleTextbox maskdate"></asp:TextBox>
                                                            <br />
                                                            <asp:RequiredFieldValidator EnableClientScript="false" ID="RFVtxtReceivedDate" runat="server"
                                                                Display="Static" SetFocusOnError="true" ControlToValidate="txtReceivedDate" ValidationGroup="a"
                                                                ForeColor="Red" ErrorMessage="*"></asp:RequiredFieldValidator>
                                                            <asp:CompareValidator EnableClientScript="false" ID="CVtxtReceivedDate" runat="server"
                                                                ControlToValidate="txtReceivedDate" ValidationGroup="a" Type="Date" ErrorMessage="Invalid!!!"
                                                                ForeColor="Red" Display="Dynamic" Operator="DataTypeCheck"></asp:CompareValidator>
                                                            <asp:RangeValidator ID="RangeValidator2" EnableClientScript="false" runat="server" MaximumValue="31/12/2015" MinimumValue="01/01/2013"
                                                                Type="Date" ControlToValidate="txtReceivedDate" ValidationGroup="a" ForeColor="Red" Display="Dynamic" 
                                                                ErrorMessage="*"></asp:RangeValidator>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                            <div class="box_c_heading cf">
                                                <div class="box_c_ico">
                                                    <asp:Image runat="server" ID="img16List" ImageUrl="pertho_admin_v1.3/img/ico/icSw2/16-List.png" AlternateText=""/></div>
                                                <p>
                                                    Transactions</p>
                                            </div>
                                            <asp:CheckBox Visible="false" Style="font-size: 13px; font-weight: bold; font-family: Helvetica, Arial, sans-serif;
                                                color: #484848;" ID="chkLoadAllChit1" runat="server" TabIndex="6" CausesValidation="false"
                                                Text="Load All Branch Chits" />
                                            <%--<asp:SqlDataSource runat="server" ID="dsMisc" ConnectionString="server=sreevisalam-prod-dec.cluster-crxftl2ep348.ap-south-1.rds.amazonaws.com;database=svcf;UID=svcf_admin;PWD=svcf1234;Allow Zero Datetime=true;port=3306;Old Guids=true;pooling=true;Max Pool Size=200"
        ProviderName="MySql.Data.MySqlClient" SelectCommand="select  11 as RootID,'--Select--' as Head,0000 as NodeID union SELECT * FROM svcf.view_parent where RootID=11" />--%>
                                            <asp:GridView CssClass="Trans twelve columns" OnRowDataBound="OrderGrid_RowDataBound"
                                                ID="GridView1" AutoGenerateColumns="False" ShowFooter="True" BorderStyle="Solid"
                                                CellSpacing="11" Font-Names="Verdana" ForeColor="#333333" GridLines="None" runat="server">
                                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Receipt Number">
                                                        <ItemTemplate>
                                                            <asp:TextBox CausesValidation="false" ID="txtReceiptNo" runat="server" TabIndex="7"
                                                                ValidationGroup="a" CssClass="twitterStyleTextbox sp_number"></asp:TextBox>
                                                            <asp:RequiredFieldValidator EnableClientScript="false" ID="RFVGrpRowtxtReceiptNo"
                                                                runat="server" Display="Static" SetFocusOnError="true" ControlToValidate="txtReceiptNo"
                                                                ValidationGroup="GrpRow" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                                            <asp:CompareValidator EnableClientScript="false" ID="CVGrpRowtxtReceiptNo" runat="server"
                                                                ControlToValidate="txtReceiptNo" ValidationGroup="GrpRow" Type="Integer" ErrorMessage="*"
                                                                Display="Dynamic" Operator="DataTypeCheck" ForeColor="Red"></asp:CompareValidator>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Chit Group Number">
                                                        <ItemTemplate>
                                                            <asp:DropDownList TabIndex="8" CausesValidation="false" ID="ddlGroupNo" runat="server"
                                                                ValidationGroup="GrpRow" CssClass="chzn-select" AutoPostBack="true" OnSelectedIndexChanged="ddlGroupNo_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                            <asp:CompareValidator EnableClientScript="false" ID="CVddlGroupNo" runat="server" ForeColor="Red"
                                                                ControlToValidate="ddlGroupNo" Display="Static" SetFocusOnError="true" ErrorMessage="*"
                                                                Operator="NotEqual" ValidationGroup="GrpRow" ValueToCompare="--Select--"> </asp:CompareValidator>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Member Name">
                                                        <ItemTemplate>
                                                            <asp:DropDownList TabIndex="9" CausesValidation="false" ID="ddlMemberName" OnSelectedIndexChanged="ddlMemberName_IndexChanged"
                                                                ValidationGroup="GrpRow" CssClass="chzn-select" runat="server">
                                                            </asp:DropDownList>
                                                            <asp:CompareValidator EnableClientScript="false" ID="CVddlMemberName" runat="server" ForeColor="Red"
                                                                ControlToValidate="ddlMemberName" Display="Static" SetFocusOnError="true" ErrorMessage="*"
                                                                Operator="NotEqual" ValidationGroup="GrpRow" ValueToCompare="--Select--"> </asp:CompareValidator>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Amount">
                                                        <ItemTemplate>
                                                            <asp:TextBox TabIndex="10" runat="server" CausesValidation="false" CssClass="twitterStyleTextbox sp_currency"
                                                                ValidationGroup="GrpRow" ID="txtAmount"></asp:TextBox>
                                                            <asp:RequiredFieldValidator EnableClientScript="false" ID="RFVtxtAmount" ControlToValidate="txtAmount"
                                                                Display="Static" ForeColor="Red" SetFocusOnError="true" ValidationGroup="GrpRow" runat="server"
                                                                ErrorMessage="*"></asp:RequiredFieldValidator>
                                                            <asp:RegularExpressionValidator EnableClientScript="false" ID="REVtxtAmount" runat="server"
                                                                Display="Dynamic" SetFocusOnError="true" ValidationExpression="^\d+(\.\d{1,2})?$"
                                                                ControlToValidate="txtAmount" ValidationGroup="GrpRow" ErrorMessage="Invalid Amount!!!"></asp:RegularExpressionValidator>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Other">
                                                        <ItemTemplate>
                                                            <asp:DropDownList TabIndex="11" CausesValidation="false" ID="ddlMisc" ValidationGroup="GrpRowC"
                                                                CssClass="chzn-select" runat="server">
                                                            </asp:DropDownList>
                                                            <asp:CompareValidator EnableClientScript="false" ID="CVddlMisc" runat="server" ControlToValidate="ddlMisc"
                                                                Display="Static" SetFocusOnError="true" ErrorMessage="*Required" Operator="NotEqual"
                                                                ValidationGroup="GrpRowC" ValueToCompare="--Select--"> </asp:CompareValidator>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Misc Amount">
                                                        <ItemTemplate>
                                                            <asp:TextBox TabIndex="12" runat="server" CausesValidation="false" CssClass="twitterStyleTextbox sp_currency"
                                                                ValidationGroup="GrpRowA" ID="txtMisc"></asp:TextBox>
                                                        </ItemTemplate>
                                                        <FooterStyle HorizontalAlign="Right" />
                                                        <FooterTemplate>
                                                            <div style="text-align: right; display: table-row">
                                                                <asp:Panel runat="server" ID="aDDpAN" Style="display: table-cell" DefaultButton="ButtonAdd">
                                                                    <asp:ImageButton Width="24" Height="24" ImageUrl="~/Styles/Image/Images/AddReceipt.png"
                                                                        ID="ButtonAdd" runat="server" CausesValidation="true" ValidationGroup="GrpRow"
                                                                        TabIndex="13" CssClass="GreenyPushButton" ToolTip="Add" OnClick="ButtonAdd_Click" />
                                                                </asp:Panel>
                                                                <asp:Panel Style="display: table-cell" ID="Panel1" runat="server" DefaultButton="ButtonRemove">
                                                                    <asp:ImageButton Width="24" Height="24" ImageUrl="~/Styles/Image/Images/RemoveReceipt.png"
                                                                        ID="ButtonRemove" runat="server" CausesValidation="false" TabIndex="14" CssClass="GreenyPushButton"
                                                                        ToolTip="Remove" OnClick="ButtonRemove_Click" />
                                                                </asp:Panel>
                                                            </div>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                            <asp:CheckBox Style="font-size: 13px; font-weight: bold; font-family: Helvetica, Arial, sans-serif;
                                                color: #484848;" ID="CheckBox1" runat="server" TabIndex="15" CausesValidation="false"
                                                AutoPostBack="true" OnCheckedChanged="CheckBox1_CheckedChanged" Text="Bank Details" />
                                            <div style="width:70%;text-align:center !important; margin: 0 auto;">
                                                <table class="hidable">
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblIFSC" runat="server" Visible="false" Text="Cheque Number"></asp:Label>
                                                            <br />
                                                            <asp:TextBox CausesValidation="false" ID="txtIfsc" CssClass="twitterStyleTextbox sp_number"
                                                                TabIndex="16" Visible="false" runat="server"></asp:TextBox>
                                                            <asp:RequiredFieldValidator EnableClientScript="false" Visible="false" ID="RFVtxtIfsc"
                                                                ControlToValidate="txtIfsc" Display="Static" SetFocusOnError="true" ValidationGroup="b"
                                                                runat="server" ForeColor="Red" ErrorMessage="*"></asp:RequiredFieldValidator>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblBL" runat="server" Visible="false" Text="Bank Details"></asp:Label>
                                                            <br />
                                                            <asp:TextBox CausesValidation="false" ID="txtBankLocation" CssClass="twitterStyleTextbox"
                                                                TabIndex="17" Visible="false" runat="server"></asp:TextBox>
                                                            <asp:RequiredFieldValidator EnableClientScript="false" Visible="false" ID="RFVtxtBankLocation"
                                                                ControlToValidate="txtBankLocation" Display="Static" SetFocusOnError="true" ValidationGroup="b"
                                                                runat="server" ForeColor="Red" ErrorMessage="*"></asp:RequiredFieldValidator>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblBankHead" Visible="false" runat="server" Text="Bank Head"></asp:Label>
                                                            <br />
                                                            <asp:DropDownList Visible="false" Style="width: 200px !important;" ID="ddlBankHead"
                                                                CssClass="chzn-select" ValidationGroup="b" TabIndex="19" runat="server" AutoPostBack="false"
                                                                CausesValidation="false">
                                                            </asp:DropDownList>
                                                            <asp:CompareValidator Visible="false" EnableClientScript="false" ID="CVddlBankHead" ForeColor="Red"
                                                                ValueToCompare="0" ControlToValidate="ddlBankHead" Display="Dynamic"
                                                                Operator="NotEqual" SetFocusOnError="true" ValidationGroup="b" runat="server"
                                                                ErrorMessage="*"></asp:CompareValidator>
                                                        </td>
                                                        <td style="vertical-align: middle;">
                                                            <asp:Label ID="lblDate" runat="server" Visible="false" Text="Cheque Date"></asp:Label>
                                                            <br />
                                                            <asp:TextBox CausesValidation="false" ID="txtDate_in_Cheque"
                                                                Visible="false" TabIndex="18" CssClass="twitterStyleTextbox maskdate" Width="100"
                                                                runat="server"></asp:TextBox>
                                                            <asp:RequiredFieldValidator EnableClientScript="false" Visible="false" ID="RFVtxtDate_in_Cheque"
                                                                ControlToValidate="txtDate_in_Cheque" Display="Static" SetFocusOnError="true" ForeColor="Red"
                                                                ValidationGroup="b" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                                                            <asp:CompareValidator EnableClientScript="false" ID="CompareValidator1" runat="server"
                                                                ControlToValidate="txtDate_in_Cheque" ValidationGroup="b" Type="Date" ErrorMessage="Invalid!!!"
                                                                ForeColor="Red" Display="Dynamic" Operator="DataTypeCheck"></asp:CompareValidator>
                                                            <asp:RangeValidator ID="RangeValidator1" EnableClientScript="false" runat="server" MaximumValue="31/12/2015" MinimumValue="01/01/2013"
                                                                Type="Date" ControlToValidate="txtDate_in_Cheque" ValidationGroup="b" ForeColor="Red" Display="Dynamic" 
                                                                ErrorMessage="*"></asp:RangeValidator>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                    <Triggers>
                                    </Triggers>
                                </asp:UpdatePanel>
                                <asp:Button ID="btnGenerate" runat="server" CssClass="GreenyPushButton" CausesValidation="true"
                                    TabIndex="20" Style="margin: 0px auto; display: table;" ValidationGroup="a" Text="Generate"
                                    OnClick="btnGenerate_Click"></asp:Button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
        <ProgressTemplate>
            <div>
                <div style="position: absolute; left: 50%; margin-left: -50px; top: 186px;">
                    <asp:Image runat="server" ID="imgWaiting" AlternateText="waiting" ImageUrl="Styles/Image/waiting.gif" style="vertical-align: middle;" />
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <ajax:ModalPopupExtender ID="ModalPopupExtender1" TargetControlID="show" PopupControlID="Pnlgendrate"
        BackgroundCssClass="modalBackground" runat="server">
    </ajax:ModalPopupExtender>
    <%--<asp:UpdatePanel ID="UpdatePanel3" runat="server" >
<ContentTemplate>--%>
    <asp:Panel Style="background: #fff; border: 4px solid #ccc; border: 8px solid rgba(0, 0, 0, 0.5);
        background-clip: padding-box; -webkit-border-radius: 10px; -moz-border-radius: 10px;
        border-radius: 10px; min-width: 300px; max-width: 900px;" CssClass="raised" ID="pnlConfirmation"
        runat="server" Visible="false">
        <asp:Label runat="server" ID="lblHintConfirmation" Text="" Visible="false"> </asp:Label>
        <%-- <div style="top: 0px; height: 50px; text-align: center; padding: auto 0; width: 100%"
            class="boxheader">--%>
        <div class=" box_c_heading" style="text-align: center !important;">
            <p>
                <asp:Label runat="server" ID="lblHeadingConfirmation" Text=""> </asp:Label></p>
        </div>
        <%-- </div>--%>
        <div style="min-height: 20px; overflow: hidden; max-width: 880px; padding: 20px;">
            <div style="min-height: 20px; overflow: auto; max-width: 840px;">
                <br />
                <br />
                <asp:Label CssClass="inner_heading" runat="server" Style="margin-top: 10px; font-size: 14px;"
                    ID="lblContentConfirmation" Text="Please Confirm Your Transaction???"> </asp:Label>
                <br />
                <br />
                <asp:GridView ID="gvConfirm" Width="810" runat="server" AutoGenerateColumns="true"
                    HeaderStyle-BackColor="#61A6F8" ShowFooter="True" HeaderStyle-Font-Bold="true"
                    HeaderStyle-ForeColor="White" PageSize="20" BackColor="White" BorderWidth="1px"
                    CellPadding="4" BorderColor="#DEDFDE" BorderStyle="None" ForeColor="Black" GridLines="None">
                    <RowStyle BackColor="#F7F7DE" />
                    <HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center" CssClass="GridviewScrollC2Header" />
                    <RowStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="GridviewScrollC2Item" />
                    <PagerStyle CssClass="GridviewScrollC2Pager" />
                    <%--  <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White"></HeaderStyle>
                <AlternatingRowStyle BackColor="White" />--%>
                </asp:GridView>
            </div>
        </div>
        <div class=" box_c_heading">
            <div style="float: right;">
                <asp:Button Width="100" Style="margin: 0 auto" CssClass="GreenyPushButton" ID="Button1"
                    OnClick="btnConfirmationYes_Click" runat="server" Text="yes" />
                <asp:Button Width="100" Style="margin: 0 auto" CssClass="GreenyPushButton" ID="Button2"
                    OnClick="btnConfirmationNo_Click" runat="server" Text="No" />
            </div>
        </div>
    </asp:Panel>
    <asp:Panel ID="Pnlgendrate" Style="background: #fff; border: 4px solid #ccc; border: 8px solid rgba(0, 0, 0, 0.5);
        background-clip: padding-box; -webkit-border-radius: 10px; -moz-border-radius: 10px;
        border-radius: 10px; min-width: 300px; max-width: 900px;" CssClass="raised" runat="server"
        Visible="false">
        <asp:Label runat="server" ID="lblHint" Text="" Visible="false"> </asp:Label>
        <div class=" box_c_heading">
            <asp:Label CssClass="inner_heading" runat="server" ID="lblHD" Text=""> </asp:Label>
        </div>
        <div style="min-height: 20px; overflow: hidden; max-width: 880px; padding: 20px;">
            <div style="min-height: 20px; overflow: auto; max-width: 840px;">
                <br />
                <br />
                <asp:Label runat="server" Style="margin-top: 10px; font-size: 14px;" ID="lblContent"
                    Text=""> </asp:Label>
            </div>
        </div>
        <div class=" box_c_heading">
            <div style="float: right;">
                <asp:Button Width="100" Style="margin: 0 auto" CssClass="GreenyPushButton" ID="btnyes"
                    OnClick="btnyes_Click" runat="server" Text="yes" />
                <asp:Button Width="100" Style="margin: 0 auto" CssClass="GreenyPushButton" ID="btnNo"
                    OnClick="btnNo_Click" runat="server" Text="No" />
            </div>
        </div>
    </asp:Panel>
    <%--</ContentTemplate>
</asp:UpdatePanel>
    --%>
    <%--<asp:UpdatePanel ID="UpdatePanel2" runat="server" >
<ContentTemplate>--%>
    <asp:Panel CssClass="raised" ID="pnlmsg" runat="server" Visible="false" Width="300px"
        Style="min-height: 100px">
        <%--<div  style="background-color:#3979BA;width: 100%; height: 40px;  top: 0px;"  >--%>
        <div style="top: 0px; height: 50px; text-align: center; padding: auto 0; width: 100%"
            class="boxheader">
            <asp:Label runat="server" ID="lblh" Text=""> </asp:Label>
        </div>
        <div style="min-height: 100px;">
            <asp:Label runat="server" ID="lblcon" Text=""> </asp:Label>
        </div>
        <div class="boxheader">
            <div style="margin: 0 auto;">
                <asp:Button Width="100" Style="margin: 0 auto" CssClass="GreenyPushButton" ID="BtnOK"
                    OnClick="btnOK_Click" runat="server" Text="Ok" />
            </div>
        </div>
    </asp:Panel>
    <asp:LinkButton runat="server" ID="show" Text=""></asp:LinkButton>
    <script type="text/javascript">

        function checkchqdate(txt) {


            //  alert('mm');
            $('#<%=txtDate_in_Cheque.ClientID%>').val(txt.value);
        }
        function checkdate(txt) {


            //  alert('mm');
            $('#<%=txtReceivedDate.ClientID%>').val(txt.value);
        }
        $(document).ready(function () {
            $(".chzn-select").chosen({ search_contains: true });
            //            $(".sp_currency").spinner({
            //                decimals: 2,
            //                stepping: 0.25,
            //                min: 0.00
            //            });
            $(".sp_currency").numeric({ negative: false });
            $(".sp_number").numeric({ decimal: false, negative: false }, function () { alert("Positive integers only"); this.value = ""; this.focus(); });
            //            $(".sp_number").spinner({min: 0,
            //                numberFormat: "d",
            //                culture:"en-GB"
            //            });
            prth_mask_input.init();
        });
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $(".chzn-select").chosen({ search_contains: true });

            $(".sp_currency").numeric({ negative: false });

            $(".sp_number").numeric({ decimal: false, negative: false }, function () { alert("Positive integers only"); this.value = ""; this.focus(); });
            prth_mask_input.init();


        });
			
			
    </script>
</asp:Content>
