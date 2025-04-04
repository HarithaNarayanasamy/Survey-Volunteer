<%@ Page Language="C#" Culture="en-GB" AutoEventWireup="true" MasterPageFile="~/Branch.Master"
    CodeBehind="DashBoard.aspx.cs" Title="SVCF Dashboard" Inherits="SreeVisalamChitFundLtd_phase1._DashBoard" %>



<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPopupControl" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<asp:Content ID="Head1" ContentPlaceHolderID="cphHead" runat="server">
    <link href="pertho_admin_v1.3/css/pertho1.css" rel="stylesheet" type="text/css" />
    <title>Svcf DashBoard</title>
    <style type="text/css">
        .title
        {
            font-size: 1.3em !important;
            color: #737373 !important;
        }
        .four
        {
            background: white !important;
        }
        .display
        {
            background: white !important;
        }
        .line
        {
            background: url(pertho_admin_v1.3/img/letterline.png) repeat-x 100% 100%;
            padding: 5px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMainContent" runat="server">
    <ajax:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </ajax:ToolkitScriptManager>
    <dx:ASPxPopupControl Modal="true" ID="dxPopup" runat="server" AllowDragging="true" DragElement="Window"
        AllowResize="false" ShowCloseButton="true" EnableViewState="False" CloseAction="CloseButton"
        ContentUrl="javascript:void(0);" Opacity="100" PopupHorizontalAlign="WindowCenter"
        PopupVerticalAlign="WindowCenter" ShowFooter="false" ShowOnPageLoad="false" MinWidth="1024"
        MaxWidth="1200" MinHeight="590" MaxHeight="590" FooterText="Try to resize the control using the resize grip or the control's edges"
        ClientInstanceName="FeedPopupControl" EnableHierarchyRecreation="True">
        <HeaderStyle Paddings-PaddingRight="20" ForeColor="#666677" Font-Size="12" />
    </dx:ASPxPopupControl>
    <br />
    <div class="row">
        <div class="twelve columns">
            <div class="box_c_content">
                <div class="row">
                    <p class="line">
                    </p>
                    <asp:Panel runat="server" DefaultButton="BtnStatisticsGo">
                        <asp:TextBox Width="100px" CssClass="input-text maskdate" Visible="false" runat="server"
                            ID="txtFromDate">
                        </asp:TextBox>
                        <div style="display: table-cell; vertical-align: top; padding-top: 8px;">
                            <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="Date : ">
                            </dx:ASPxLabel>
                        </div>
                        <div style="display: table-cell; padding-top: 4px; padding-left: 10px !important;
                            padding-right: 10px !important;">
                            <asp:TextBox TabIndex="1" runat="server" ID="txtToDate" Width="100px" CssClass="input-text maskdate">
                            </asp:TextBox>
                            <asp:RequiredFieldValidator Display="Dynamic" runat="server" ControlToValidate="txtToDate"></asp:RequiredFieldValidator>
                            <asp:CompareValidator ID="CompareValidator10" runat="server" Display="Dynamic" ControlToValidate="txtToDate"
                                Operator="DataTypeCheck" Type="Date"></asp:CompareValidator>
                        </div>
                        <div style="display: table-cell; vertical-align: top;">
                            <asp:Button TabIndex="2" ID="BtnStatisticsGo" runat="server" OnClick="BtnStatisticsGo_Click"
                                CssClass="GreenyPushButton" Text="Go!"></asp:Button>
                        </div>
                    </asp:Panel>
                    <p class="line">
                    </p>
                </div>
            </div>
        </div>
    </div>
    <br />
    <div class="row">
        <div class="twelve columns">
            <div class="box_c">
                <div class="box_c_heading  box_actions">
                    <p>
                        Ledger</p>
                </div>
                <div class="box_c_content">
                    <div class="row">
                        <div class="row display">
                            <div class="four columns">
                                <div class="anlt_box">
                                    <p class="anlt_heading">
                                        <asp:Label Style="float: left;" runat="server" ID="lblledgerBRANCHES" Text="BRANCHES"></asp:Label>
                                        <br />
                                        <asp:Label ID="lblledgerBRANCHESCRBDRB" runat="server" Text="0.00" CssClass="down"></asp:Label></p>
                                    <br />
                                    <div style="text-align: center;">
                                        <p class="detail">
                                            BRANCHES</p>
                                        <hr class="style-three" />
                                        <asp:Label class="title" runat="server" ID="lblBranchCr" Text="Cr. 0"></asp:Label>
                                        <hr class="style-three" />
                                        <asp:Label class="title" runat="server" ID="lblBranchDr" Text="Dr. 0"></asp:Label>
                                        <br />
                                        <br />
                                        <asp:Button ID="lbBranch" OnClick="lbBranch_click" CssClass="gh_button icon add"
                                            runat="server" Text="View All"></asp:Button>
                                        <br />
                                        <br />
                                    </div>
                                </div>
                            </div>
                            <div class="four columns" style="text-align: center;">
                                <div class="anlt_box">
                                    <p class="anlt_heading">
                                        <asp:Label Style="float: left;" runat="server" ID="lblledgerINVESTMENTS" Text="INVESTMENTS"></asp:Label>
                                        <br />
                                        <asp:Label ID="lblledgerINVESTMENTSCRBDRB" runat="server" Text="0.00" CssClass="down"></asp:Label></p>
                                    <br />
                                    <div style="text-align: center;">
                                        <p class="detail">
                                            INVESTMENTS</p>
                                        <hr class="style-three" />
                                        <asp:Label class="title" runat="server" ID="lblInvestMentsCr" Text="Cr. 0"></asp:Label>
                                        <hr class="style-three" />
                                        <asp:Label class="title" runat="server" ID="lblInvestMentsDr" Text="Dr. 0"></asp:Label>
                                        <br />
                                        <br />
                                        <asp:Button ID="lbInvestMents" OnClick="lbInvestMents_Click" CssClass="gh_button icon add"
                                            runat="server" Text="View All"></asp:Button>
                                        <br />
                                        <br />
                                    </div>
                                </div>
                            </div>
                            <div class="four columns" style="text-align: center;">
                                <div class="anlt_box">
                                    <p class="anlt_heading">
                                        <asp:Label Style="float: left;" runat="server" ID="lblledgerBANKS" Text="BANKS"></asp:Label>
                                        <br />
                                        <asp:Label ID="lblledgerBANKSCRBDRB" runat="server" Text="0.00" CssClass="down"></asp:Label></p>
                                    <br />
                                    <div style="text-align: center;">
                                        <p class="detail">
                                            BANKS</p>
                                        <hr class="style-three" />
                                        <asp:Label class="title" runat="server" ID="lblBankCr" Text="Cr. 0"></asp:Label>
                                        <hr class="style-three" />
                                        <asp:Label class="title" runat="server" ID="lblBankDr" Text="Dr. 0"></asp:Label>
                                        <br />
                                        <br />
                                        <asp:Button ID="lbBank" OnClick="lbBank_ClicK" CssClass="gh_button icon add" runat="server"
                                            Text="View All"></asp:Button>
                                        <br />
                                        <br />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row display">
                            <div class="four columns" style="text-align: center;">
                                <div class="anlt_box">
                                    <p class="anlt_heading">
                                        <asp:Label Style="float: left;" runat="server" ID="lblledgerOTHERITEMS" Text="OTHER ITEMS"></asp:Label>
                                        <br />
                                        <asp:Label ID="lblledgerOTHERITEMSCRBDRB" runat="server" Text="0.00" CssClass="down"></asp:Label></p>
                                    <br />
                                    <div style="text-align: center;">
                                        <p class="detail">
                                            OTHER ITEMS</p>
                                        <hr class="style-three" />
                                        <asp:Label class="title" runat="server" ID="lblOTHERITEMScr" Text="Cr. 0"></asp:Label>
                                        <hr class="style-three" />
                                        <asp:Label class="title" runat="server" ID="lblOTHERITEMSdr" Text="Dr. 0"></asp:Label>
                                        <br />
                                        <br />
                                        <asp:Button ID="lbOTHERITEMS" OnClick="lbOTHERITEMS_Click" CssClass="gh_button icon add"
                                            runat="server" Text="View All"></asp:Button>
                                        <br />
                                        <br />
                                    </div>
                                </div>
                            </div>
                            <div class="four columns" style="text-align: center;">
                                <div class="anlt_box">
                                    <p class="anlt_heading">
                                        <asp:Label Style="float: left;" runat="server" ID="lblledgerCHITS" Text="CHITS"></asp:Label>
                                        <br />
                                        <asp:Label ID="lblledgerCHITSCRBDRB" runat="server" Text="0.00" CssClass="down"></asp:Label></p>
                                    <br />
                                    <div style="text-align: center;">
                                        <p class="detail">
                                            CHITS</p>
                                        <hr class="style-three" />
                                        <asp:Label class="title" runat="server" ID="lblCHITSCr" Text="Cr. 0"></asp:Label>
                                        <hr class="style-three" />
                                        <asp:Label class="title" runat="server" ID="lblCHITSDr" Text="Dr. 0"></asp:Label>
                                        <br />
                                        <br />
                                        <asp:Button ID="lbCHITS" OnClick="lbCHITS_Click" CssClass="gh_button icon add" runat="server"
                                            Text="View All"></asp:Button>
                                        <br />
                                        <br />
                                    </div>
                                </div>
                            </div>
                            <div class="four columns" style="text-align: center;">
                                <div class="anlt_box">
                                    <p class="anlt_heading">
                                        <asp:Label Style="float: left;" runat="server" ID="lblledgerFOREMANCHITS" Text="FOREMAN CHITS"></asp:Label>
                                        <br />
                                        <asp:Label ID="lblledgerFOREMANCHITSCRBDRB" runat="server" Text="0.00" CssClass="down"></asp:Label></p>
                                    <br />
                                    <div style="text-align: center;">
                                        <p class="detail">
                                            FOREMAN CHITS</p>
                                        <hr class="style-three" />
                                        <asp:Label class="title" runat="server" ID="lblFOREMANCHITSCr" Text="Cr. 0"></asp:Label>
                                        <hr class="style-three" />
                                        <asp:Label class="title" runat="server" ID="lblFOREMANCHITSDr" Text="Dr. 0"></asp:Label>
                                        <br />
                                        <br />
                                        <asp:Button ID="lblOREMANCHITS" OnClick="lblOREMANCHITS_Click" CssClass="gh_button icon add"
                                            runat="server" Text="View All"></asp:Button>
                                        <br />
                                        <br />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row display">
                            <div class="four columns" style="text-align: center;">
                                <div class="anlt_box">
                                    <p class="anlt_heading">
                                        <asp:Label Style="float: left;" runat="server" ID="lblledgerDECREEDEBTORS" Text="DECREE DEBTORS"></asp:Label>
                                        <br />
                                        <asp:Label ID="lblledgerDECREEDEBTORSCRBDRB" runat="server" Text="0.00" CssClass="down"></asp:Label></p>
                                    <br />
                                    <div style="text-align: center;">
                                        <p class="detail">
                                            DECREE DEBTORS</p>
                                        <hr class="style-three" />
                                        <asp:Label class="title" runat="server" ID="lblDECREEDEBTORSCr" Text="Cr. 0"></asp:Label>
                                        <hr class="style-three" />
                                        <asp:Label class="title" runat="server" ID="lblDECREEDEBTORSDr" Text="Dr. 0"></asp:Label>
                                        <br />
                                        <br />
                                        <asp:Button ID="lbDECREEDEBTORS" OnClick="lbDECREEDEBTORS_Click" CssClass="gh_button icon add"
                                            runat="server" Text="View All"></asp:Button>
                                        <br />
                                        <br />
                                    </div>
                                </div>
                            </div>
                            <div class="four columns" style="text-align: center;">
                                <div class="anlt_box">
                                    <p class="anlt_heading">
                                        <asp:Label Style="float: left;" runat="server" ID="lblledgerLOANS" Text="LOANS"></asp:Label>
                                        <br />
                                        <asp:Label ID="lblledgerLOANSCRBDRB" runat="server" Text="0.00" CssClass="down"></asp:Label></p>
                                    <br />
                                    <div style="text-align: center;">
                                        <p class="detail">
                                            LOANS</p>
                                        <hr class="style-three" />
                                        <asp:Label class="title" runat="server" ID="lblLoanCr" Text="Cr. 0"></asp:Label>
                                        <hr class="style-three" />
                                        <asp:Label class="title" runat="server" ID="lblLoanDr" Text="Dr. 0"></asp:Label>
                                        <br />
                                        <br />
                                        <asp:Button ID="lbLoan" OnClick="lbLoan_Click" CssClass="gh_button icon add" runat="server"
                                            Text="View All"></asp:Button>
                                        <br />
                                        <br />
                                    </div>
                                </div>
                            </div>
                            <div class="four columns" style="text-align: center;">
                                <div class="anlt_box">
                                    <p class="anlt_heading">
                                        <asp:Label Style="float: left;" runat="server" ID="lblledgerADVANCES" Text="ADVANCES"></asp:Label>
                                        <br />
                                        <asp:Label ID="lblledgerADVANCESCRBDRB" runat="server" Text="0.00" CssClass="down"></asp:Label></p>
                                    <br />
                                    <div style="text-align: center;">
                                        <p class="detail">
                                            ADVANCES</p>
                                        <hr class="style-three" />
                                        <asp:Label class="title" runat="server" ID="lblSUNDRIESANDADVANCEScr" Text="Cr. 0"></asp:Label>
                                        <hr class="style-three" />
                                        <asp:Label class="title" runat="server" ID="lblSUNDRIESANDADVANCESDr" Text="Dr. 0"></asp:Label>
                                        <br />
                                        <br />
                                        <asp:Button ID="lbSUNDRIESANDADVANCES" OnClick="lbSUNDRIESANDADVANCES_Click" CssClass="gh_button icon add"
                                            runat="server" Text="View All"></asp:Button>
                                        <br />
                                        <br />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row display">
                            <div class="four columns" style="text-align: center;">
                                <div class="anlt_box">
                                    <p class="anlt_heading">
                                        <asp:Label Style="float: left;" runat="server" ID="lblledgerSTAMPS" Text="STAMPS"></asp:Label>
                                        <br />
                                        <asp:Label ID="lblledgerSTAMPSCRBDRB" runat="server" Text="0.00" CssClass="down"></asp:Label></p>
                                    <br />
                                    <div style="text-align: center;">
                                        <p class="detail">
                                            STAMPS</p>
                                        <hr class="style-three" />
                                        <asp:Label class="title" runat="server" ID="lblSTAMPSAndSTAMPPAPERSCr" Text="Cr. 0"></asp:Label>
                                        <hr class="style-three" />
                                        <asp:Label class="title" runat="server" ID="lblSTAMPSAndSTAMPPAPERSDr" Text="Dr. 0"></asp:Label>
                                        <br />
                                        <br />
                                        <asp:Button ID="lbSTAMPSAndSTAMPPAPERS" OnClick="lbSTAMPSAndSTAMPPAPERS_Click" CssClass="gh_button icon add"
                                            runat="server" Text="View All"></asp:Button>
                                        <br />
                                        <br />
                                    </div>
                                </div>
                            </div>
                            <div class="four columns" style="text-align: center;">
                                <div class="anlt_box">
                                    <p class="anlt_heading">
                                        <asp:Label Style="float: left;" runat="server" ID="lblledgerPROFITANDLOSS" Text="PROFIT AND LOSS"></asp:Label>
                                        <br />
                                        <asp:Label ID="lblledgerPROFITANDLOSSCRBDRB" runat="server" Text="0.00" CssClass="down"></asp:Label></p>
                                    <br />
                                    <div style="text-align: center;">
                                        <p class="detail">
                                            PROFIT AND LOSS</p>
                                        <hr class="style-three" />
                                        <asp:Label class="title" runat="server" ID="lblPROFITANDLOSSACCOUNTCr" Text="Cr. 0"></asp:Label>
                                        <hr class="style-three" />
                                        <asp:Label class="title" runat="server" ID="lblPROFITANDLOSSACCOUNTDr" Text="Dr. 0"></asp:Label>
                                        <br />
                                        <br />
                                        <asp:Button ID="lbPROFITANDLOSSACCOUNT" OnClick="lbPROFITANDLOSSACCOUNT_Click" CssClass="gh_button icon add"
                                            runat="server" Text="View All"></asp:Button>
                                        <br />
                                        <br />
                                    </div>
                                </div>
                            </div>
                            <div class="four columns" style="text-align: center;">
                                <div class="anlt_box">
                                    <p class="anlt_heading">
                                        <asp:Label Style="float: left;" runat="server" ID="lblledgerCASH" Text="CASH"></asp:Label>
                                        <br />
                                        <asp:Label ID="lblledgerCASHCRBDRB" runat="server" Text="0.00" CssClass="down"></asp:Label></p>
                                    <br />
                                    <div style="text-align: center;">
                                        <p class="detail">
                                            CASH
                                        </p>
                                        <hr class="style-three" style="margin-top: -2px;" />
                                        <asp:Label class="title" runat="server" ID="lblCashCr" Text="Cr. 0"></asp:Label>
                                        <hr class="style-three" />
                                        <asp:Label class="title" runat="server" ID="lblCashDr" Text="Dr. 0"></asp:Label>
                                        <br />
                                        <br />
                                        <asp:Button ID="lbCashCr" OnClick="lbCashCr_Click" CssClass="gh_button icon add"
                                            runat="server" Text="View All"></asp:Button>
                                        <br />
                                        <br />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="twelve columns">
            <div class="box_c">
                <div class="box_c_heading  box_actions">
                    <p>
                        Notebook View</p>
                </div>
                <div class="box_c_content">
                    <div class="row">
                        <dx:ASPxGridView Style="margin: 0 auto; width: 100%;" OnSummaryDisplayText="ASPxGridView1_SummaryDisplayText"
                            ID="grid" ClientInstanceName="grid" runat="server" DataSourceID="AccessDataSource1">
                            <Settings ShowFooter="true" />
                            <TotalSummary>
                                <dx:ASPxSummaryItem FieldName="Credit" SummaryType="Sum" />
                                <dx:ASPxSummaryItem FieldName="Debit" SummaryType="Sum" />
                                <dx:ASPxSummaryItem FieldName="Credit Balance" SummaryType="Sum" />
                                <dx:ASPxSummaryItem FieldName="Debit Balance" SummaryType="Sum" />
                            </TotalSummary>
                            <SettingsPager Mode="ShowAllRecords">
                            </SettingsPager>
                            <Columns>
                                <dx:GridViewDataColumn FieldName="RootID" Caption="Head ID">
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="Node" Caption="Head">
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="Credit">
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="Debit">
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="Credit Balance">
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="Debit Balance">
                                </dx:GridViewDataColumn>
                            </Columns>
                            <Styles Header-HorizontalAlign="Center" Footer-HorizontalAlign="Left" Cell-HorizontalAlign="Left">
                                <GroupPanel Wrap="True" HorizontalAlign="Left">
                                </GroupPanel>
                                <GroupRow Wrap="True" HorizontalAlign="Left">
                                </GroupRow>
                            </Styles>
                        </dx:ASPxGridView>
                        <asp:SqlDataSource runat="server" ID="AccessDataSource1" ConnectionString="server=svcf.cbbo9wgd70wb.ap-southeast-1.rds.amazonaws.com;database=svcf;UID=root;PWD=rootsvcf;Allow Zero Datetime=true;port=3306"
                            ProviderName="MySql.Data.MySqlClient" SelectCommand=" " />
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        $(document).ready(function () {
            prth_mask_input.init();
        });
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            prth_mask_input.init();
        });
    </script>
</asp:Content>
