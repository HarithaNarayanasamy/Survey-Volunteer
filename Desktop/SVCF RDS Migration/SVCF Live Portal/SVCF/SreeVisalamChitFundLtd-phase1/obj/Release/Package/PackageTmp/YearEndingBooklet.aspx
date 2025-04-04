<%@ Page Title="" Language="C#" MasterPageFile="~/Branch.Master" AutoEventWireup="true" CodeBehind="YearEndingBooklet.aspx.cs" Inherits="SreeVisalamChitFundLtd_phase1.YearEndingBooklet" %>



<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMainContent" runat="server">
    &nbsp;<link href="Styles/aspxtable.css" rel="stylesheet" type="text/css" /><style type="text/css">
        .tg {
            border-collapse: collapse;
            border-spacing: 0;
            margin: 0px auto;
           }

            .tg td {
                font-family: Arial, sans-serif;
                font-size: 14px;
                padding: 10px 5px;
                border-style: solid;
                border-width: 1px;
                overflow: hidden;
                word-break: normal;
            }

            .tg th {
                font-family: Arial, sans-serif;
                font-size: 14px;
                font-weight: normal;
                padding: 10px 5px;
                border-style: solid;
                border-width: 1px;
                overflow: hidden;
                word-break: normal;
            }

            .tg .tg-yw4l {
                vertical-align: top;
                text-align: center;
                display: table-cell;
                padding: 8px;
                box-sizing: border-box;
                vertical-align: middle;
            }

        @media screen and (max-width: 767px) {
            .tg {
                width: auto !important;
            }

                .tg col {
                    width: auto !important;
                }

            .tg-wrap {
                overflow-x: auto;
                -webkit-overflow-scrolling: touch;
                margin: auto 0px;
            }
        }
                                                                                      .auto-style1 {
                                                                                        width: 299px;
                                                                                    }
                                                                                    .auto-style2 {
                                                                                        width: 207px;
                                                                                    }
                                                                                    .auto-style3 {
                                                                                        width: 192px;
                                                                                    }
                                                                                                .auto-style4 {
                                                                                                    width: 216px;
                                                                                                }
                                                                                      </style><link href="Content/bootstrap.min.css" rel="stylesheet" /><div class="row">
        <div class="twelve columns">
            <div class="box_c">
                <div class="box_c_heading  box_actions">
                    <p>
                        Year Ending Booklet
                    </p>
                </div>
                <div class="box_c_content">
                    <div style="display: table-cell; vertical-align: top; padding-top: 8px;">
                        <label>Year Start Date :</label>

                    </div>
                    <div style="display: table-cell; vertical-align: top; padding-left: 8px; padding-top: 4px; padding-right: 10px !important;">
                        <asp:TextBox TabIndex="1" Width="100px" CssClass="twitterStyleTextbox maskdate" runat="server"
                            ID="txtFrmDate">
                        </asp:TextBox>
                        <asp:RequiredFieldValidator ValidationGroup="a" EnableClientScript="false" ErrorMessage="*"
                            ID="RequiredFieldValidator2" Display="Dynamic" runat="server" ControlToValidate="txtFrmDate"></asp:RequiredFieldValidator>
                        <asp:CompareValidator ValidationGroup="a" EnableClientScript="false" ErrorMessage="*"
                            ID="CompareValidator1" runat="server" Display="Dynamic" ControlToValidate="txtFrmDate"
                            Operator="DataTypeCheck" Type="Date"></asp:CompareValidator>
                    </div>
                    <div style="display: table-cell; vertical-align: top; padding-top: 8px;">
                        <label>Year End Date :</label>
                    </div>
                    <div style="display: table-cell; vertical-align: top; padding-left: 8px; padding-top: 4px; padding-right: 10px !important;">
                        <asp:TextBox TabIndex="1" Width="100px" CssClass="twitterStyleTextbox maskdate" runat="server"
                            ID="txtToDate">
                        </asp:TextBox>
                        <asp:RequiredFieldValidator ValidationGroup="a" EnableClientScript="false" ErrorMessage="*"
                            ID="RequiredFieldValidator1" Display="Dynamic" runat="server" ControlToValidate="txtToDate"></asp:RequiredFieldValidator>
                        <asp:CompareValidator ValidationGroup="a" EnableClientScript="false" ErrorMessage="*"
                            ID="CompareValidator10" runat="server" Display="Dynamic" ControlToValidate="txtToDate"
                            Operator="DataTypeCheck" Type="Date"></asp:CompareValidator>
                    </div>
                    <div>
                        <label>Select Branch</label>
                        <asp:DropDownList ID="drpdownBranchlist" CssClass="chzn-select" runat="server" AutoPostBack="true"></asp:DropDownList>
                    </div>


                    <div class="tg-wrap">
                        <table class="tg">
                            <tr>
                                <th class="tg-yw4l">S.No.</th>
                                <th class="tg-yw4l" style="width: 299px">Sheet Name</th>
                                <th class="tg-yw4l" style="width: 192px">Export</th>
                            </tr>
                            <tr>
                                <td class="tg-yw4l">1</td>
                                <td class="auto-style1">
                                    <div style="width: 100%;" class="col-md-4">
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        <input id="txtBs01" name="txtNama0" type="text" value="BS-St-01" class="auto-style2"
                                            required="" readonly="readonly">&nbsp;
                                    </div>
                                </td>
                                <td class="auto-style3">
                                    <asp:Button ID="BtnSt_01" runat="server" Text="Export" Width="250px" CssClass="GreenyPushButton"
                                        OnClick="BtnSt_01_Click" Style="margin-bottom: 0px" />
                                </td>

                            </tr>
                            <tr>
                                <td class="tg-yw4l">2</td>
                               <td class="tg-yw4l" style="width: 299px">
                                    <div class="ui-accordion">
                                        <input id="txtPLSt02" name="txtNama2" type="text" style="width: 200px;" value="P&L St-02"
                                            class="form-control input-md" required="" readonly="readonly">
                                    </div>
                                </td>
                                <td class="auto-style3">
                                    <asp:Button ID="BtnTrPandLSt02" runat="server" Text="Export" Width="250px" CssClass="GreenyPushButton" OnClick="BtnTrPandLSt02_Click" />
                                </td>
                            </tr>
                            <tr>
                                <td class="tg-yw4l">3</td>
                                 <td class="tg-yw4l" style="width: 299px">
                                    <div style="width: 100%;" class="col-md-4">
                                        <input id="txtTrSt02" name="txtNama11" style="width: 200px;" type="text" value="Tr. St-02"
                                            class="form-control input-md" required="" readonly="readonly" />
                                    </div>
                                </td>
                                <td class="tg-yw4l" style="width: 192px">
                                    <asp:Button ID="btnTrSt02" runat="server" Text="Export" CssClass="GreenyPushButton" Width="250px" Style="margin-bottom: 0px" OnClick="btnTrSt02_Click" />
                                </td>
                            </tr>
							   <tr>
                                <td class="tg-yw4l">4</td>
                                <td class="tg-yw4l" style="width: 299px">
                                    <div style="width: 100%;" class="col-md-4">
                                        <input id="STThree" name="txtNama11" style="width: 200px;" type="text"
                                            value="Abstract Investment St-03" class="form-control input-md" required="" readonly="readonly" />
                                    </div>
                                </td>
                                <td class="tg-yw4l" style="width: 192px">
                                    <asp:Button ID="STThreebtn" runat="server" Text="Export" CssClass="GreenyPushButton" Width="250px" Style="margin-bottom: 0px" OnClick="ST03_Click" />
                                </td>
                            </tr>

                            <tr>
                                <td class="tg-yw4l">5</td>
                                <td class="tg-yw4l" style="width: 299px">
                                    <div style="width: 100%;" class="col-md-4">
                                        <input id="ST03A" name="txtNama11" style="width: 200px;" type="text"
                                            value="Invest & Dp St-03A to I" class="form-control input-md" required="" readonly="readonly" />
                                    </div>
                                </td>
                                <td class="tg-yw4l" style="width: 192px">
                                    <asp:Button ID="ST03Abtn" runat="server" Text="Export" CssClass="GreenyPushButton" Width="250px" Style="margin-bottom: 0px" OnClick="ST03A_Click" />
                                </td>
                            </tr>
                            <tr>
                                <td class="tg-yw4l">6</td>
                                <td class="tg-yw4l" style="width: 299px">
                                    <div style="width: 100%;" class="col-md-4">
                                        <input id="txtBankSt04" name="txtNama3" type="text" style="width: 200px;" value="Tr. Bl. Bank St-04"
                                            class="form-control input-md" required="" readonly="readonly">
                                    </div>
                                </td>
                                <td class="auto-style3">
                                    <asp:Button ID="BtnTrBlBankSt04" runat="server" Text="Export" Width="250px" CssClass="GreenyPushButton" OnClick="BtnTrBlBankSt04_Click" />
                                </td>
                            </tr>
							<tr>
                                <td class="tg-yw4l">7</td>
                                <td class="tg-yw4l" style="width: 299px">
                                    <div style="width: 100%;" class="col-md-4">
                                        <input id="txtBankSt04A" name="txtNama3-4A" type="text" style="width: 200px;" value="Chit Sec Dep Bank&Accrued Int"
                                            class="form-control input-md" required="" readonly="readonly">
                                    </div>
                                </td>
                                <td class="auto-style3">
                                    <asp:Button ID="BtnTrBlBankSt04A" runat="server" Text="Export" Width="250px" CssClass="GreenyPushButton" OnClick="BtnTrBlBankSt04A_Click" />
                                </td>
                            </tr>
							<tr>
                                <td class="tg-yw4l">8</td>
                                <td class="tg-yw4l" style="width: 299px">
                                    <div style="width: 100%;" class="col-md-4">
                                        <input id="txtReconDiff" name="txtNama8" style="width: 200px;" type="text"
                                            value="Recon Diff Bank Bal St-04 Contd" class="form-control input-md" required="" readonly="readonly" />
                                    </div>
                                </td>
                                <td class="auto-style3">
                                    <asp:Button ID="BtnReconDiffSt04" runat="server" Text="Export" Width="250px" CssClass="GreenyPushButton" OnClick="BtnReconDiffSt04_Click" Style="margin-bottom: 0px" />
                                </td>
                            </tr>
							<tr>
                                <td class="tg-yw4l">9</td>
                                <td class="tg-yw4l" style="width: 299px">
                                    <div style="width: 100%;" class="col-md-4">
                                        <input id="STfiveA" name="txtNama11" style="width: 200px;" type="text"
                                            value="Abstract Tr. Bal St-05(A)" class="form-control input-md" required="" readonly="readonly" />
                                    </div>
                                </td>
                                <td class="tg-yw4l" style="width: 192px">
                                    <asp:Button ID="STfiveAbtn" runat="server" Text="Export" CssClass="GreenyPushButton" Width="250px" OnClick="AbstractST05A_Click" />
                                </td>
                            </tr>
							 <tr>
                                <td class="tg-yw4l">10</td>
                                <td class="tg-yw4l" style="width: 299px">
                                    <div style="width: 100%;" class="col-md-4">
                                        <input id="txtgroupwarest5B" name="txtNama10" style="width: 200px;" type="text"
                                            value="Groupware ERA St-5B & 5C" class="form-control input-md" required="" readonly="readonly" />

                                    </div>
                                </td>
                                <td class="auto-style3">
                                    <asp:Button ID="BtnGroupwareSt5B" runat="server" Text="Export" Width="250px" CssClass="GreenyPushButton" Style="margin-bottom: 0px" OnClick="BtnGroupwareSt5B_Click" />
                                </td>
                            </tr>
							<tr>
                                <td class="tg-yw4l">11</td>
                                <td class="tg-yw4l" style="width: 299px">
                                    <div style="width: 100%;" class="col-md-4">
                                        <input id="txtChitCollect" name="txtNama9" style="width: 200px;" type="text"
                                            value="Chit Collec St-5D" class="form-control input-md" required="" readonly="readonly" />

                                    </div>
                                </td>
                                <td class="auto-style3">
                                    <asp:Button ID="BtnChitcollectSt5D" runat="server" Text="Export" Width="250px" CssClass="GreenyPushButton"
                                        Style="margin-bottom: 0px" OnClick="BtnChitcollectSt5D_Click" />
                                </td>
                            </tr>
							 <tr>
                                <td class="tg-yw4l">12</td>
                                <td class="tg-yw4l" style="width: 299px">
                                    <div style="width: 100%;" class="col-md-4">
                                        <input id="txtOutPrize" name="txtNama11" style="width: 200px;" type="text"
                                            value="Out. Prize & unprize St-5(E)" class="form-control input-md" required="" readonly="readonly" />
                                    </div>
                                </td>
                                <td class="tg-yw4l" style="width: 192px">
                                    <asp:Button ID="btnOutPrize" runat="server" Text="Export" CssClass="GreenyPushButton" Width="250px" Style="margin-bottom: 0px" OnClick="btnOutPrize_Click" />
                                </td>
                            </tr>
							<tr>
                                <td class="tg-yw4l">13</td>
                                <td class="tg-yw4l" style="width: 299px">
                                    <div style="width: 100%;" class="col-md-4">
                                        <input id="txtAbstractSt05F" name="txtNama4" type="text" style="width: 200px;"
                                            value="Abstract Chit St-5(F)" class="form-control input-md" required="" readonly="readonly">
                                    </div>
                                </td>
                                <td class="auto-style3">
                                    <asp:Button ID="BtnAbsChitSt5" runat="server" Text="Export" Width="250px" CssClass="GreenyPushButton" OnClick="BtnAbsChitSt5_Click" />
                                </td>
                            </tr>
                            <tr>
                                <td class="tg-yw4l">14</td>
                                <td class="tg-yw4l" style="width: 299px">
                                    <div style="width: 100%;" class="col-md-4">
                                        <input id="txtFandCSC06" name="txtNama5" type="text" style="width: 200px;"
                                            value="Abst. foreman & Sub chit St-06" class="form-control input-md" required="" readonly="readonly">
                                    </div>
                                </td>
                                <td class="auto-style3">
                                    <asp:Button ID="BtnAbsForemanandSubstitutedSt06" runat="server" Width="250px" CssClass="GreenyPushButton" Text="Export" OnClick="BtnAbsForemanandSubstitutedSt06_Click" />
                                </td>
                            </tr>							
							   <tr>
                                <td class="tg-yw4l">15</td>
                                <td class="tg-yw4l" style="width: 299px">
                                    <div style="width: 100%;" class="col-md-4">
                                        <input id="ST6A&B" name="txtNama11" style="width: 200px;" type="text" value="Forman Chit st 6A & B" class="form-control input-md" required="" />
                                    </div>
                                </td>
                                <td class="tg-yw4l" style="width: 192px">
                                    <asp:Button ID="BtnSt6AB" runat="server" Text="Export" CssClass="GreenyPushButton" Width="250px" Style="margin-bottom: 0px" OnClick="BtnSt6AB_Click" />
                                </td>
                            </tr>
							   <tr>
                                <td class="tg-yw4l">16</td>
                                <td class="tg-yw4l" style="width: 299px">
                                    <div style="width: 100%;" class="col-md-4">
                                        <input id="txtDecreeCourcostST07" name="txtNama11" style="width: 200px;" type="text"
                                            value="Decree & Court cost St-07" class="form-control input-md" required="" readonly="readonly" />
                                    </div>

                                </td>
                                <td class="auto-style3">
                                    <asp:Button ID="BtnDecreeCourcostSt07" runat="server" Text="Export" CssClass="GreenyPushButton" Width="250px" Style="margin-bottom: 0px" OnClick="BtnDecreeCourcostSt07_Click" />
                                </td>
                            </tr>
							  <tr>
                                <td class="tg-yw4l">17</td>
                                <td class="tg-yw4l" style="width: 299px">
                                    <div style="width: 100%;" class="col-md-4">
                                        <input id="txtLoansOutAccrued" name="txtNama11" style="width: 200px;" type="text"
                                            value="Loans Out & Accrued int. St-08" class="form-control input-md" required="" readonly="readonly" />
                                    </div>
                                </td>
                                <td class="tg-yw4l" style="width: 192px">
                                    <asp:Button ID="btnLoansOutAccrued" runat="server" Text="Export" CssClass="GreenyPushButton" Width="250px" Style="margin-bottom: 0px" OnClick="btnLoansOutAccrued_Click" />
                                </td>
                            </tr>
							 <tr>
                                <td class="tg-yw4l">18</td>
                                <td class="tg-yw4l" style="width: 299px">
                                    <div style="width: 100%;" class="col-md-4">
                                        <input id="txtSundriesAdvance" name="txtNama11" style="width: 200px;" type="text"
                                            value="Sundries & Advance St-09" class="form-control input-md" required="" readonly="readonly" />
                                    </div>
                                </td>
                                <td class="tg-yw4l" style="width: 192px">
                                    <asp:Button ID="btnSundriesAdvance" runat="server" Text="Export" CssClass="GreenyPushButton" Width="250px" Style="margin-bottom: 0px" OnClick="btnSundriesAdvance_Click" />
                                </td>
                            </tr>
							 <tr>
                                <td class="tg-yw4l">19</td>
                                <td class="tg-yw4l" style="width: 299px">
                                    <div style="width: 100%;" class="col-md-4">
                                        <input id="txtRecoverySt11" name="txtNama11" style="width: 200px;" type="text"
                                            value="Recovery BDW St-11" class="form-control input-md" required="" readonly="readonly" />

                                    </div>

                                </td>
                                <td class="auto-style3">
                                    <asp:Button ID="BtnRecoverySt11" runat="server" Text="Export" Width="250px" CssClass="GreenyPushButton" Style="margin-bottom: 0px" OnClick="BtnRecoverySt11_Click" />
                                </td>
                            </tr>
							     <tr>
                                <td class="tg-yw4l">20</td>
                                <td class="tg-yw4l" style="width: 299px">
                                    <div style="width: 100%;" class="col-md-4">
                                        <input id="txtEmSt12" name="txtNama6" type="text" style="width: 200px;" value="Emoluments St-12"
                                            class="form-control input-md" required="" readonly="readonly">
                                    </div>
                                </td>
                                <td class="auto-style3">
                                    <asp:Button ID="BtnEmolumentsSt12" runat="server" Text="Export" Width="250px" CssClass="GreenyPushButton" OnClick="BtnEmolumentsSt12_Click" />
                                </td>
                            </tr>
							<tr>
                                <td class="tg-yw4l">21</td>
                                <td class="tg-yw4l" style="width: 299px">
                                    <div style="width: 100%;" class="col-md-4">
                                        <input id="deduction12" name="txtNama11" style="width: 200px;" type="text" value="Deduction St- 12" class="form-control input-md" required="" />
                                    </div>
                                </td>
                                <td class="tg-yw4l" style="width: 192px">
                                    <asp:Button ID="Btndeduction12" runat="server" Text="Export" CssClass="GreenyPushButton" Width="250px" Style="margin-bottom: 0px" 
                                        OnClick="Btndeduction12_Click" />
                                </td>
                            </tr>
							 <tr>
                                <td class="tg-yw4l">22</td>
                                <td class="tg-yw4l" style="width: 299px">
                                    <div style="width: 100%;" class="col-md-4">
                                        <input id="ST1314" name="txtNama11" style="width: 200px;" type="text" value="EPF & PS St-13 & 14"
                                            class="form-control input-md" required="" readonly="readonly" />
                                    </div>
                                </td>
                                <td class="tg-yw4l" style="width: 192px">
                                    <asp:Button ID="ST1314btn" runat="server" Text="Export" CssClass="GreenyPushButton" Width="250px" Style="margin-bottom: 0px" OnClick="ST1314_Click" />
                                </td>
                            </tr>
							  <tr>
                                <td class="tg-yw4l">23</td>
                                <td class="tg-yw4l" style="width: 299px">
                                    <div style="width: 100%;" class="col-md-4">
                                        <input id="ST14" name="txtNama11" style="width: 200px;" type="text" value="ESIC St-14A"
                                            class="form-control input-md" required="" readonly="readonly" />
                                    </div>
                                </td>
                                <td class="tg-yw4l" style="width: 192px">
                                    <asp:Button ID="ST14btn" runat="server" Text="Export" CssClass="GreenyPushButton" Width="250px" Style="margin-bottom: 0px" OnClick="ST14_Click" />
                                </td>
                            </tr>
							 <tr>
                                <td class="tg-yw4l">24</td>
                                <td class="tg-yw4l" style="width: 299px">
                                    <div style="width: 100%;" class="col-md-4">
                                        <input id="txtRentpaidSt15" name="txtNama11" style="width: 200px;" type="text"
                                            value="Rent paid St- 15" class="form-control input-md" required="" readonly="readonly" />
                                    </div>
                                </td>
                                <td class="tg-yw4l" style="width: 192px">
                                    <asp:Button ID="btnRentpaidSt15" runat="server" Text="Export" CssClass="GreenyPushButton" Width="250px" Style="margin-bottom: 0px" OnClick="btnRentpaidSt15_Click" />
                                </td>
                            </tr>
							 <tr>
                                <td class="tg-yw4l">25</td>
                                <td class="tg-yw4l" style="width: 299px">
                                    <div style="width: 100%;" class="col-md-4">
                                        <input id="txtInterestPaid" name="txtNama11" style="width: 200px;" type="text"
                                            value="Interest paid St- 16" class="form-control input-md" required="" readonly="readonly" />
                                    </div>
                                </td>
                                <td class="auto-style3">
                                    <asp:Button ID="BtnInterestPaidSt16" runat="server" Text="Export" Width="250px" CssClass="GreenyPushButton" Style="margin-bottom: 0px" OnClick="BtnInterestPaidSt16_Click" />
                                </td>
                            </tr>
							<tr>
                                <td class="tg-yw4l">26</td>
                                <td class="tg-yw4l" style="width: 299px">
                                    <div style="width: 100%;" class="col-md-4">
                                        <input id="txtSt17" name="txtNama7" type="text" style="width: 200px;" value="Business Perform St- 17"
                                            class="form-control input-md" required="" readonly="readonly">
                                    </div>
                                </td>
                                <td class="auto-style3">
                                    <asp:Button ID="BtnBPPSt17" runat="server" Text="Export" Width="250px" CssClass="GreenyPushButton" OnClick="BtnBPPSt17_Click" Style="margin-bottom: 0px" />
                                </td>
                            </tr>
							<tr>
                                <td class="tg-yw4l">27</td>
                                <td class="tg-yw4l" style="width: 299px">
                                    <div style="width: 100%;" class="col-md-4">
                                        <input id="txtBaddebtswrittenoffSt18" name="txtNama11" style="width: 200px;" type="text"
                                            value="Baddebts written off St- 18" class="form-control input-md" required="" readonly="readonly" />
                                    </div>
                                </td>
                                <td class="tg-yw4l" style="width: 192px">
                                    <asp:Button ID="btnBaddebtswrittenoffSt18" runat="server" Text="Export"
                                        CssClass="GreenyPushButton" Width="250px" Style="margin-bottom: 0px" OnClick="btnBaddebtswrittenoffSt18_Click" />
                                </td>
                            </tr>
 
							<tr>
                                <td class="tg-yw4l">28</td>
                                <td class="tg-yw4l" style="width: 299px">
                                    <div style="width: 100%;" class="col-md-4">
                                        <input id="txtAbstractChitSt19" name="txtNama11" style="width: 200px;" type="text"
                                            value="Abstract Chit Debtor St- 19" class="form-control input-md" required="" readonly="readonly" />
                                    </div>
                                </td>
                                <td class="auto-style3">
                                    <asp:Button ID="BtnAbstractChitSt19" runat="server" Text="Export" Width="250px" CssClass="GreenyPushButton" Style="margin-bottom: 0px" OnClick="BtnAbstractChitSt19_Click" />
                                </td>
                            </tr>
							 <tr>
                                <td class="tg-yw4l">29</td>
                                <td class="tg-yw4l" style="width: 299px">
                                    <div style="width: 100%;" class="col-md-4">
                                        <input id="ST20" name="txtNama11" style="width: 200px;" type="text" value="Groupwar Chit Debtors"
                                            class="form-control input-md" required="" readonly="readonly" />
                                    </div>
                                </td>
                                <td class="tg-yw4l" style="width: 192px">
                                    <asp:Button ID="ST20btn" runat="server" Text="Export" CssClass="GreenyPushButton" Width="250px" Style="margin-bottom: 0px" OnClick="ST20_Click" />
                                </td>
                            </tr>
							<tr>
                                <td class="tg-yw4l">30</td>
                                <td class="tg-yw4l" style="width: 299px">
                                    <div style="width: 100%;" class="col-md-4">
                                        <input id="txtNewChitSt21" name="txtNama11" style="width: 200px;" type="text"
                                            value="New Chit Group Commd St- 21" class="form-control input-md" required="" readonly="readonly" />
                                    </div>

                                </td>
                                <td class="auto-style3">
                                    <asp:Button ID="BtnNewChitSt21" runat="server" Text="Export" Width="250px" CssClass="GreenyPushButton" Style="margin-bottom: 0px" OnClick="BtnNewChitSt21_Click" />
                                </td>
                            </tr>
							<tr>
                                <td class="tg-yw4l">31</td>
                                <td class="tg-yw4l" style="width: 299px">
                                    <div style="width: 100%;" class="col-md-4">
                                        &nbsp;&nbsp;&nbsp;
                                        <input id="ST22" name="txtNama11" type="text"
                                            value="Groupwar Commission earned St- 22" class="auto-style4" required="" readonly="readonly" />
                                    </div>
                                </td>
                                <td class="tg-yw4l" style="width: 192px">
                                    <asp:Button ID="ST22btn" runat="server" Text="Export" CssClass="GreenyPushButton" Width="250px" Style="margin-bottom: 0px" OnClick="ST22_Click" />
                                </td>
                            </tr>
							<tr>
                                <td class="tg-yw4l">32</td>
                                <td class="tg-yw4l" style="width: 299px">
                                    <div style="width: 100%;" class="col-md-4">
                                        <input id="txtLiabilitySt23" name="txtNama11" style="width: 200px;" type="text"
                                            value="Liability in Chit St - 23" class="form-control input-md" required="" readonly="readonly" />
                                    </div>
                                </td>
                                <td class="auto-style3">
                                    <asp:Button ID="BtnLiability23" runat="server" Text="Export" CssClass="GreenyPushButton" Width="250px" Style="margin-bottom: 0px" OnClick="BtnLiability23_Click" />
                                </td>
                            </tr>
							 <tr>
                                <td class="tg-yw4l">33</td>
                                <td class="tg-yw4l" style="width: 299px">
                                    <div style="width: 100%;" class="col-md-4">
                                        <input id="txtChitDebit" name="txtNama11" style="width: 200px;" type="text"
                                            value="Chit Debit St-  24" class="form-control input-md" required="" readonly="readonly" />
                                    </div>
                                </td>
                                <td class="tg-yw4l" style="width: 192px">
                                    <asp:Button ID="btnChitDebit" runat="server" Text="Export" CssClass="GreenyPushButton" Width="250px" Style="margin-bottom: 0px" OnClick="btnChitDebit_Click" />
                                </td>
                            </tr>
							<tr>
                                <td class="tg-yw4l">34</td>
                                <td class="tg-yw4l" style="width: 299px">
                                    <div style="width: 100%;" class="col-md-4">
                                        <input id="ST24" name="txtNama11" style="width: 200px;" type="text" value="Statement 24A"
                                            class="form-control input-md" required="" readonly="readonly" />
                                    </div>
                                </td>
                                <td class="tg-yw4l" style="width: 192px">
                                    <asp:Button ID="ST24btn" runat="server" Text="Export" CssClass="GreenyPushButton" Width="250px" Style="margin-bottom: 0px" OnClick="ST24_Click" />
                                </td>
                            </tr>
							<tr>
                                <td class="tg-yw4l">35</td>
                                <td class="tg-yw4l" style="width: 299px">
                                    <div style="width: 100%;" class="col-md-4">
                                        <input id="txtExceed6months" name="txtNama1" type="text" style="width: 200px;"
                                            value="Debts outs Exceed 6month St- 25" class="form-control input-md" required="" readonly="readonly">
                                    </div>
                                </td>
                                <td class="auto-style3">
                                    <asp:Button ID="BtnDebtsoutsExceed" runat="server" Text="Export" Width="250px"
                                        CssClass="GreenyPushButton" OnClick="BtnDebtsoutsExceed_Click" Style="margin-bottom: 0px" />
                                </td>
                            </tr>
						 </table>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript">


        $(document).ready(function () {
            $(".chzn-select").chosen({ search_contains: true });
        });

        $(document).ready(function () {
            $(".htmlselect").chosen({ search_contains: true });
            prth_mask_input.init();
        });
    </script>
   
</asp:Content>
