<%@ Page Culture="en-GB" Title="Member Addition" Language="C#" MasterPageFile="~/Branch.Master"
    AutoEventWireup="true" CodeBehind="MemberAddition.aspx.cs" Inherits="SreeVisalamChitFundLtd_phase1.Design" %>

<%@ Register Assembly="TimePicker" Namespace="MKB.TimePicker" TagPrefix="MKB" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Head1" ContentPlaceHolderID="cphHead" runat="server">
    <link href="Styles/aspxtable.css" rel="stylesheet" type="text/css" />
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
    <style type="text/css">
        .chzn-drop
        {
            border: 1px solid #aaa !important;
            padding-top: 5px !important;
            width: 240px !important;
        }
        #ctl00_cphMainContent_DdlAddrProof_chzn .chzn-results
        {
            height: 110px !important;
        }
        .chzn-results
        {
            text-align:center;
        }
        #ctl00_cphMainContent_ddlCategory_chzn .chzn-drop .chzn-search input[type="text"]
        {
            height:15px;
        }
        #ctl00_cphMainContent_DdlAddrProof_chzn .chzn-drop .chzn-search input[type="text"]
        {
            height:15px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMainContent" runat="server">
    <ajax:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </ajax:ToolkitScriptManager>
    <asp:LinkButton ID="lk" Text="" runat="server"></asp:LinkButton>
    <ajax:ModalPopupExtender ID="ModalPopup12" runat="server" TargetControlID="lk" BackgroundCssClass="modalBackground"
        PopupControlID="panInd">
    </ajax:ModalPopupExtender>
    <div class="row">
        <div class="twelve columns">
            <div class="box_c">
                <div class="box_c_heading  box_actions">
                    <p>
                        Member Addition</p>
                </div>
                <div class="box_c_content">
                    <div class="row">
                        <div style="width: 100%;">
                            <table cellspacing="3" style="margin: 0 auto;">
                                <tr>
                                    <td>
                                        <span>Customer Photo:</span>
                                    </td>
                                    <td>
                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="always">
                                            <ContentTemplate>
                                                <asp:FileUpload ID="fileuploadImage" AllowMultiple="true" runat="server" CssClass="input-text ttip_r" />
                                                <asp:Label ID="lblCurrentFile" runat="server"></asp:Label><br />
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator21" ValidationGroup="add" ControlToValidate ="fileuploadImage" runat="server" ErrorMessage="Upload image" Display="Dynamic"></asp:RequiredFieldValidator>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:PostBackTrigger ControlID="btnUpload" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                        <asp:HiddenField ID="HiddenField1" runat="server" />
                                    </td>
                                    <td>
                                        <asp:Button ID="btnUpload" OnClick="btnUpload_Click" CssClass="GreenyPushButton"
                                            runat="server" Text="Upload" CausesValidation="False" Visible="False" />
                                    </td>
                                </tr>
                            </table>
                            <table cellspacing="3" style="margin: 0 auto;">
                                <tr>
                                    <td style="vertical-align:top;padding-right:5px;padding-top:6px;">
                                        <asp:Label ID="LblNameoOfTheSubscriber" Text="Subscriber's Name" runat="server"></asp:Label>
                                    </td>
                                    <td style="padding-right:5px;">
                                        <asp:TextBox ID="txtSubscriberName" ToolTip="Ex. Sudhakar" TabIndex="1" placeholder="Subscriber's Name"
                                            CssClass="input-text ttip_r" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="NameRequired" ControlToValidate="txtSubscriberName"
                                            ErrorMessage="Enter Subscriber's Name" runat="server" ValidationGroup="add" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </td>
                                    <td style="vertical-align:top;padding-right:5px;padding-top:6px;">
                                        <asp:Label ID="Label4" Text="Residence Tel. No." runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtResiTeleNo" placeholder="Residence Telelphone Number" ToolTip="Ex. 0422-7575757"
                                            CssClass="input-text ttip_r" runat="server" TabIndex="8"></asp:TextBox>
                                        <asp:RegularExpressionValidator ControlToValidate="txtResiTeleNo" ID="RegularExpressionValidator3"
                                            runat="server" ErrorMessage="(STD)-(Tel-No)" ValidationExpression="\d+(-)\d+"
                                            Display="Dynamic"></asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="vertical-align:top;padding-right:5px;padding-top:6px;">
                                        <asp:Label ID="LblCategory" Text="Category" runat="server"></asp:Label>
                                    </td>
                                    <td style="padding-right:5px;">
                                        <asp:DropDownList ID="ddlCategory" Width="240px" CssClass="chzn-select" runat="server"
                                            ValidationGroup="add" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged"
                                            AutoPostBack="true" TabIndex="2">
                                            <asp:ListItem Text="--select--" Value="--select--"></asp:ListItem>
                                            <asp:ListItem Text="Individual" Value="Individual"></asp:ListItem>
                                            <asp:ListItem Text="Proprietary Concern" Value="Proprietary Concern"></asp:ListItem>
                                            <asp:ListItem Text="Partnership Firm" Value="Partnership Firm"></asp:ListItem>
                                            <asp:ListItem Text="Company" Value="Company"></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="CategoryRequired" ValidationGroup="add" InitialValue="--select--"
                                            ControlToValidate="ddlCategory" ErrorMessage="Select Category" runat="server"
                                            Display="Dynamic"></asp:RequiredFieldValidator>
                                    </td>
                                    <td style="vertical-align:top;padding-right:5px;padding-top:6px;">
                                        <asp:Label ID="LblMobileNumber" Text="Mobile Number" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtMobNo" CssClass="input-text ttip_r sp_number" placeholder="Mobile Number"
                                            ToolTip="Ex. 9876543210" MaxLength="10" runat="server" TabIndex="9"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator20" ValidationGroup="add" ControlToValidate="txtMobNo"
                                            ErrorMessage="Enter Mobile Number" runat="server" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="vertical-align:top;padding-right:5px;padding-top:6px;">
                                        <asp:Label ID="LblProfessionOrBusiness" Text="Profession/Business" runat="server"></asp:Label>
                                    </td>
                                    <td style="padding-right:5px;">
                                        <asp:TextBox ID="txtProfOrBus" CssClass="input-text ttip_r" placeholder="Profession/Bussiness"
                                            ToolTip="Ex. Software Engineer" runat="server" TabIndex="3"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="add" ControlToValidate="txtProfOrBus"
                                            ErrorMessage="Enter Profession/Business" runat="server" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </td>
                                    <td style="vertical-align:top;padding-right:5px;padding-top:6px;">
                                        <asp:Label ID="LblTelephoneNumber" Text="Official Tel.No." runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtOffTeleNo" CssClass="input-text ttip_r" placeholder="Official Telephone Number"
                                            ToolTip="Ex. 0422-7575757" runat="server" TabIndex="10"></asp:TextBox>
                                        <asp:RegularExpressionValidator ValidationExpression="\d+(-)\d+" ID="RegularExpressionValidator4"
                                            runat="server" ErrorMessage="(STD)-(Tel-No)" ControlToValidate="txtOffTeleNo"
                                            Display="Dynamic"></asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="vertical-align:top;padding-right:5px;padding-top:6px;">
                                        <asp:Label ID="LblNatureOfProfessionOrBusiness" Text="Nature of Prof/Business" runat="server"></asp:Label>
                                    </td>
                                    <td style="padding-right:5px;">
                                        <asp:TextBox ID="txtNatureProfOrBusi" CssClass="input-text ttip_r" runat="server"
                                            placeholder="Nature of Profession / Business" ToolTip="Ex. IT" TabIndex="3"></asp:TextBox>
                                    </td>
                                    <td style="vertical-align:top;padding-right:5px;padding-top:6px;">
                                        <asp:Label ID="LblMonthlyIncomeThrough" Text="Monthly Income" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtMonthInc" CssClass="input-text ttip_r sp_float" runat="server" ToolTip="Ex. 10000.00"
                                            placeholder="Monthly Income" TabIndex="11"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ControlToValidate="txtMonthInc"
                                            ErrorMessage="Enter Monthly Income" ValidationGroup="add" runat="server" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="vertical-align:top;padding-right:5px;padding-top:40px;">
                                        <asp:Label ID="LabResidentialAddress" Text="Residential Address" runat="server"></asp:Label>
                                    </td>
                                    <td style="padding-right:5px;">
                                        <asp:TextBox ID="txtResAddr" placeholder="Residential Address" ToolTip="Ex. 196/23, Thiruvenkatasamy Road (West),<br/> R.S.Puram,<br/> Coimbatore - 641002"
                                            CssClass="input-text ttip_r" TextMode="MultiLine" runat="server" TabIndex="4"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="ResidentialRequried" ControlToValidate="txtResAddr"
                                            ErrorMessage="Enter Residential Address" ValidationGroup="add" runat="server"
                                            Display="Dynamic"></asp:RequiredFieldValidator>
                                    </td>
                                    <td style="vertical-align:top;padding-right:5px;padding-top:6px;">
                                        <asp:Label ID="LblSalesTaxRegNo" Text="Sales Tax Reg. No.(TNGST)" runat="server">
                                        </asp:Label>
                                        <br />
                                        <br />
                                        <br />
                                        <asp:Label ID="LblCSTRegNo" Text="C.S.T.Reg. No." runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TxtSTRegNo" placeholder="Sales Tax Reg. Number (TNGST)" CssClass="input-text ttip_r"
                                            runat="server" TabIndex="12"></asp:TextBox>
                                        <br />
                                        <asp:TextBox ID="TxtCSTRegNo" placeholder="C.S.T.Reg. Number" CssClass="input-text ttip_r"
                                            runat="server" TabIndex="13"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="vertical-align:top;padding-right:5px;padding-top:40px;">
                                        <asp:Label ID="Label2" Text="Communication Address" runat="server"></asp:Label>
                                    </td>
                                    <td style="padding-right:5px;">
                                        <asp:TextBox ID="txtAddrComm" placeholder="Communication Address" CssClass="input-text ttip_r"
                                            TextMode="MultiLine" runat="server" ToolTip="Ex. 196/23, Thiruvenkatasamy Road (West),<br/> R.S.Puram,<br/> Coimbatore - 641002"
                                            TabIndex="6"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ValidationGroup="add" ControlToValidate="txtAddrComm"
                                            ErrorMessage="*" runat="server" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </td>
                                    <td style="vertical-align:top;padding-right:5px;padding-top:6px;">
                                        <asp:Label ID="LblIncomeTaxPANoWardAndCircle" Text="Income Tax P.A.No." runat="server"></asp:Label><br />
                                        <br />
                                        <br />
                                        <asp:Label ID="Label1" Text="Bank Name" runat="server"></asp:Label>
                                    </td>
                                    <td style="vertical-align:top;padding-right:5px;padding-top:6px;">
                                        <asp:TextBox ID="TxtIncTaxPANCircle" CssClass="input-text ttip_r" placeholder="Income Tax P.A. Number"
                                            runat="server" TabIndex="14"></asp:TextBox>
                                        <br />
                                        <asp:TextBox CssClass="input-text ttip_r" ID="TxtBankName" ToolTip="Ex. State Bank of India"
                                            placeholder="Bank Name" runat="server" TabIndex="15"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="vertical-align:top;padding-right:5px;padding-top:40px;">
                                        <asp:Label ID="LblAddressOfTheBusinessorProfession" Text="Business/Profession Address"
                                            runat="server"></asp:Label>
                                    </td>
                                    <td style="padding-right:5px;">
                                        <asp:TextBox ID="TxtAddrBusiOrProf" placeholder="Business/Profession Address" CssClass="input-text ttip_r"
                                            TextMode="MultiLine" ToolTip="Ex. 196/23, Thiruvenkatasamy Road (West),<br/> R.S.Puram,<br/> Coimbatore - 641002"
                                            runat="server" TabIndex="7"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ValidationGroup="add" ControlToValidate="TxtAddrBusiOrProf"
                                            ErrorMessage="Enter Business/Profession Address" runat="server" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </td>
                                    <td style="vertical-align:top;padding-right:5px;padding-top:6px;">
                                        <asp:Label ID="Label3" Text="Branch Name" runat="server"></asp:Label><br />
                                        <br />
                                        <br />
                                        <asp:Label ID="lblAccNo" runat="server" Text="Savings/Current Account No."></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox CssClass="input-text ttip_r" placeholder="Branch Name" ToolTip="Ex. Pollachi"
                                            ID="TxtBranch" runat="server" TabIndex="16"></asp:TextBox>
                                        <br />
                                        <asp:TextBox ID="txtAcctNo" runat="server" CssClass="input-text ttip_r" MaxLength="16"
                                            placeholder="Savings/Current Account Number" ToolTip="Ex. 0000010566812834" TabIndex="17"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Branch Name
                                    </td>
                                    <td>
                                        <asp:DropDownList TabIndex="1" ID="ddlBranch" Width="240px" runat="server" CssClass="chzn-select"
                                            >
                                        </asp:DropDownList>
                                        <asp:CompareValidator ID="CVddlGroupNo" runat="server"
                                            ForeColor="Red" ValidationGroup="add" ControlToValidate="ddlBranch" Display="Dynamic"
                                            SetFocusOnError="true" ErrorMessage="Select Branch" Operator="NotEqual" ValueToCompare="0"> </asp:CompareValidator>
                                    </td>
                                    <td></td>
                                    <td></td>
                                </tr>
                            </table>
                        </div>
                        <div style="margin: 0px auto; text-align: center;">
                            <asp:Button ID="btnAdd" CausesValidation="true" runat="server" Text="Add" CssClass="GreenyPushButton"
                                Style="margin: 0px auto:" OnClick="btnAdd_Click" ValidationGroup="add" TabIndex="17" />
                            <asp:Button ID="buttonCancel" runat="server" Text="Cancel" TabIndex="17" CssClass="GreenyPushButton"
                                OnClick="btnClosing_click" OnClientClick="clearValidationErrors();" CausesValidation="false"
                                Style="margin: 0px auto:" />
                        </div>
                    </div>
                    <asp:Panel DefaultButton="buttonAdd" CssClass="raised" ID="panInd" runat="server"
                        Width="600px" Style="display: none; max-height: 500px; min-height: 300px">
                        <asp:Label runat="server" ID="lblHint" Text="" Visible="false"> </asp:Label>
                        <div style="top: 0px; height: 50px; text-align: center; padding: auto 0; width: 100%"
                            class="boxheader">
                            <asp:Label ID="dd" runat="server" Text="Individual"> </asp:Label>
                        </div>
                        <br />
                        <div id="content" style="max-height: 400px; min-height: 200px; overflow: auto; width: 100%;">
                            <table cellspacing="3" style="margin: 0px auto; display: table;">
                                <tr>
                                    <td style="vertical-align:top;padding-right:5px;padding-top:6px;">
                                        <asp:Label ID="LblProofOfResidenceAndIdentity" runat="server" Text="Aadhar Card No."></asp:Label>
                                    </td>
                                    <td> 
                                        <asp:DropDownList ID="DdlAddrProof" TabIndex="1" runat="server" Width="150px" CssClass="chzn-select" AutoPostBack="true"
                                            DropDownStyle="DropDownList" OnSelectedIndexChanged="DdlAddrProof_SelectedIndexChanged">
                                          <%--  <asp:ListItem Value="--select--" Text="--select--"></asp:ListItem>                                            
                                            <asp:ListItem Value="Passport" Text="Passport"></asp:ListItem>
                                            <asp:ListItem Value="Voters ID" Text="Voters ID"></asp:ListItem>
                                            <asp:ListItem Value="Driving Licence" Text="Driving Licence"></asp:ListItem>
                                            <asp:ListItem Value="Ration Card" Text="Ration Card"></asp:ListItem>
                                              <asp:ListItem Value="Other" Text="Other"></asp:ListItem>
                                            <asp:ListItem Value="Post Office Idendity" Text="Post Office Idendity"></asp:ListItem>--%>
                                            <asp:ListItem Value="Aadhar Card No." Text="Aadhar Card No."></asp:ListItem>
                                            <asp:ListItem Value="PAN" Text="PAN Card No."></asp:ListItem>
                                            
                                        </asp:DropDownList>
                                       <asp:TextBox ID="txtAadharno" runat="server" Text="" MaxLength="12"></asp:TextBox>
                                       <asp:RequiredFieldValidator ValidationGroup="sssss" ID="RequiredFieldValidator1"
                                            runat="server" Display="Dynamic" ControlToValidate="txtAadharno" ErrorMessage="Enter Aadhar card no."></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="vertical-align:top;padding-right:5px;padding-top:6px;">
                                        <asp:Label ID="LblSex" runat="server" Text="Gender"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:RadioButtonList TabIndex="2" ID="rbtnList" RepeatDirection="Horizontal" runat="server">
                                            <asp:ListItem Selected="True" Text="Male"></asp:ListItem>
                                            <asp:ListItem Text="Female"></asp:ListItem>
                                        </asp:RadioButtonList>
                                        <asp:RequiredFieldValidator ValidationGroup="sssss" ID="RequiredFieldValidator11"
                                            runat="server" Display="Dynamic" ControlToValidate="rbtnList" ErrorMessage="Enter Gender"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="vertical-align:top;padding-right:5px;padding-top:6px;">
                                        <asp:Label ID="LblAge" runat="server" Text="Age"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox TabIndex="3" ID="TxtAge" runat="server" onchange="check(this);" placeholder="Age" ToolTip="Ex. 35" CssClass="input-text ttip_r sp_number"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="vertical-align:top;padding-right:5px;padding-top:6px;">
                                        <asp:Label ID="LblDateOfBirth" runat="server" Text="Date of Birth"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox TabIndex="4" ID="txtDOB" runat="server" placeholder="Date of Birth" CssClass="input-text ttip_r maskdate"></asp:TextBox>
                                        <asp:CompareValidator ID="CompareValidator1"  ValidationGroup="sssss" runat="server" Display="Dynamic" ErrorMessage="Enter Valid Date" ControlToValidate="txtDOB" Operator="DataTypeCheck" Type="Date"></asp:CompareValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="vertical-align:top;padding-right:5px;padding-top:6px;">
                                        <asp:Label ID="LblFatherssOrHusbandsName" runat="server" Text="Father's/Husband's Name"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox TabIndex="5" ID="TxtFathOrHusbName" placeholder="Father's/Husband's Name" ToolTip="Ex. Murugan"
                                            runat="server" CssClass="input-text ttip_r"></asp:TextBox>
                                        <asp:RequiredFieldValidator ValidationGroup="sssss" runat="server" Display="Dynamic"
                                            ControlToValidate="TxtFathOrHusbName" ErrorMessage="Enter Father's/Husband's Name"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="vertical-align:top;padding-right:5px;padding-top:6px;">
                                        <asp:Label ID="LblMothersName" runat="server" Text="Mother's Name"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox TabIndex="6" ID="TxtMotherName" runat="server" placeholder="Mother's Name" CssClass="input-text ttip_r"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div class="boxheader">
                            <div style="margin: 0 auto;">
                                <asp:Button Style="margin: 0 auto" ID="buttonAdd" runat="server" CssClass="GreenyPushButton"
                                    ValidationGroup="sssss" Text="Add" OnClick="BtnAddition_Click" />
                                <asp:Button Style="margin: 0 auto" ID="btnCancel" runat="server" CausesValidation="false"
                                    OnClientClick="clearValidationErrors();" Text="Cancel" OnClick="BtnCancel_Click"
                                    CssClass="GreenyPushButton" />
                            </div>
                        </div>
                    </asp:Panel>
                    <asp:Panel DefaultButton="button1" CssClass="raised" ID="pnlPro" runat="server" Width="600px"
                        Style="display: none; max-height: 500px; min-height: 300px">
                        <asp:Label runat="server" ID="Label9" Text="" Visible="false"> </asp:Label>
                        <div style="top: 0px; height: 50px; text-align: center; padding: auto 0; width: 100%"
                            class="boxheader">
                            <asp:Label ID="Label10" runat="server" Text="Property Concern"> </asp:Label>
                        </div>
                        <br />
                        <div id="content1" style="max-height: 400px; min-height: 200px; overflow: auto; width: 100%;
                            padding: 10px 10px 10px 10px;">
                            <table cellspacing="3" style="margin: 0px auto; display: table;">
                                <tr>
                                    <td style="vertical-align:top;padding-right:5px;padding-top:6px;">
                                        <asp:Label ID="LblNameOfTheProprietors" runat="server" Text="Proprietor Name"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox TabIndex="1" ID="TxtpropName" runat="server" placeholder="Proprietor Name" ToolTip="Ex. Murugan"
                                            CssClass="input-text ttip_r"></asp:TextBox>
                                        <asp:RequiredFieldValidator ValidationGroup="concern" ID="RequiredFieldValidator12"
                                            runat="server" Display="Dynamic" ControlToValidate="TxtpropName" ErrorMessage="Enter Proprietor Name"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="vertical-align:top;padding-right:5px;padding-top:6px;">
                                        <asp:Label ID="Label6" runat="server" Text="Gender"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:RadioButtonList TabIndex="2" runat="server" ID="Rbtn" RepeatDirection="Horizontal">
                                            <asp:ListItem Selected="True" Text="Male"></asp:ListItem>
                                            <asp:ListItem Text="Female"></asp:ListItem>
                                        </asp:RadioButtonList>
                                        <asp:RequiredFieldValidator ValidationGroup="concern" ID="RequiredFieldValidator3"
                                            runat="server" Display="Dynamic" ControlToValidate="Rbtn" ErrorMessage="Select Gender"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="vertical-align:top;padding-right:5px;padding-top:6px;">
                                        <asp:Label ID="LblAge1" runat="server" Text="Age"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox TabIndex="3" MaxLength="2" onchange="check1(this);" ID="TxtAge1" runat="server" placeholder="Age" ToolTip="Ex. 35"
                                            CssClass="input-text ttip_r sp_number"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="vertical-align:top;padding-right:5px;padding-top:6px;">
                                        <asp:Label ID="LblDateofBirth1" runat="server" Text="Date of Birth"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox TabIndex="4" ID="TxtDOB1" runat="server" placeholder="Date of Birth" CssClass="input-text ttip_r maskdate"></asp:TextBox>
                                        <asp:CompareValidator ID="CompareValidator3"  ValidationGroup="concern"  runat="server" Display="Dynamic" ErrorMessage="Enter Valid Date" ControlToValidate="TxtDOB1" Operator="DataTypeCheck" Type="Date"></asp:CompareValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="vertical-align:top;padding-right:5px;padding-top:6px;">
                                        <asp:Label ID="LblFathersAndHusbandsName1" runat="server" Text="Father's/Husband's Name"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox TabIndex="5" ID="TxtFathOrHusbName1" runat="server" placeholder="Father's/Husband's Name"
                                            ToolTip="Ex. Murugan" CssClass="input-text ttip_r"></asp:TextBox>
                                        <asp:RequiredFieldValidator ValidationGroup="concern" ID="RequiredFieldValidator13"
                                            runat="server" Display="Dynamic" ControlToValidate="TxtFathOrHusbName1" ErrorMessage="Enter Father's/Husband's Name"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="vertical-align:top;padding-right:5px;padding-top:6px;">
                                        <asp:Label ID="LblMothersName1" runat="server" Text="Mother's Name"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox TabIndex="6" ID="TxtMotherName1" placeholder="Mother's Name" ToolTip="Ex. Lakshmi"
                                            runat="server" CssClass="input-text ttip_r"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div class="boxheader">
                            <div style="margin: 0 auto;">
                                <asp:Button TabIndex="7" Style="margin: 0 auto" ID="button1" runat="server" CssClass="GreenyPushButton"
                                    ValidationGroup="concern" Text="Add" OnClick="BtnAddition_Click" />
                                <asp:Button Style="margin: 0 auto" ID="Button2" runat="server" CausesValidation="false"
                                    Text="Cancel" OnClick="BtnCancel_Click" CssClass="GreenyPushButton" OnClientClick="clearValidationErrors();" />
                            </div>
                        </div>
                    </asp:Panel>
                    <asp:Panel DefaultButton="button3" CssClass="raised" ID="pnlpartner" runat="server"
                        Width="600px" Style="display: none; max-height: 500px; min-height: 250px">
                        <asp:Label runat="server" ID="Label11" Visible="false"> </asp:Label>
                        <div style="height: 50px; text-align: center; width: 100%" class="boxheader">
                            <asp:Label ID="Label12" runat="server" Text="Partnership Firm"> </asp:Label>
                        </div>
                        <br />
                        <div id="Div1" style="min-height: 250px;">
                            <table cellspacing="3" style="margin: 0px auto; display: table;">
                                <tr>
                                    <td style="vertical-align:top;padding-right:5px;padding-top:6px;">
                                        <asp:Label ID="LblNameOfThePartners" runat="server" Text="1.Name of the Partner's"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox TabIndex="1" ID="TxtPartnerName1" runat="server" placeholder="First Partner Name"
                                            CssClass="input-text ttip_b" ></asp:TextBox>
                                        <asp:RequiredFieldValidator ValidationGroup="partner" ID="RequiredFieldValidator15"
                                            runat="server" Display="Dynamic" ControlToValidate="TxtPartnerName1" ErrorMessage="Enter Partner Name"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="vertical-align:top;padding-right:5px;padding-top:6px;">
                                        <asp:Label ID="LblPartners2" runat="server" Text="2."></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox TabIndex="2" ID="TxtPartnerName2" runat="server" CssClass="input-text ttip_r" 
                                            placeholder="Second Partner Name"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="vertical-align:top;padding-right:5px;padding-top:6px;"> 
                                        <asp:Label ID="LblPartners3" runat="server" Text="3."></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox TabIndex="3" ID="TxtPartnerName3" runat="server" CssClass="input-text ttip_r"
                                            placeholder="Third Partner Name"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="vertical-align:top;padding-right:5px;padding-top:6px;">
                                        <asp:Label ID="LblParners4" runat="server" Text="4."></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox TabIndex="4" ID="TxtPartnerName4" runat="server" CssClass="input-text ttip_r" 
                                            placeholder="Fourth Partner Name"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="vertical-align:top;padding-right:5px;padding-top:6px;">
                                        <asp:Label ID="LblPartners5" runat="server" Text="5."></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox TabIndex="5" ID="TxtPartnerName5" runat="server" CssClass="input-text ttip_r"
                                            placeholder="Fifth Partner Name"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="vertical-align:top;padding-right:5px;padding-top:6px;">
                                        <asp:Label ID="LblDateOfPartnershipWithEnclose" runat="server" Text="Date of Partnership with Amendment(if any) "></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox TabIndex="6" ID="TxtDOPartWithEncl" runat="server" placeholder="Date of Partnership with Amendment"
                                            CssClass="input-text ttip_r maskdate" >
                                        </asp:TextBox>
                                        <asp:CompareValidator ID="CompareValidator4" ValidationGroup="partner" runat="server" Display="Dynamic" ErrorMessage="Enter Valid Date" ControlToValidate="TxtDOPartWithEncl" Operator="DataTypeCheck" Type="Date"></asp:CompareValidator>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div class="boxheader">
                            <div style="margin: 0 auto;">
                                <asp:Button Style="margin: 0 auto;" ID="button3" runat="server" CssClass="GreenyPushButton"
                                    ValidationGroup="partner" Text="Add" OnClick="BtnAddition_Click" />
                                <asp:Button Style="margin: 0 auto;" ID="Button4" runat="server" CausesValidation="false"
                                    OnClientClick="clearValidationErrors();" Text="Cancel" OnClick="BtnCancel_Click"
                                    CssClass="GreenyPushButton" />
                            </div>
                        </div>
                    </asp:Panel>
                    <asp:Panel DefaultButton="button5" CssClass="raised" ID="pnlcompany" runat="server"
                        Width="500px" Style="min-height: 150px">
                        <asp:Label runat="server" ID="Label13" Text="CompanyDet " Visible="false"> </asp:Label>
                        <div style="height: 50px; text-align: center; width: 100%" class="boxheader">
                            <asp:Label ID="Label14" runat="server" Text="Company"> </asp:Label>
                        </div>
                        <br />
                        <div id="Div2" style="min-height: 100px;">
                            <table cellspacing="3" style="margin: 0px auto; vertical-align: middle; display: table;">
                                <tr style="padding-top: 10px;">
                                    <td style="padding-right: 5px;">
                                        <asp:Label ID="Label17" runat="server" Text="File No. of Certificate of Incorporation"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox TabIndex="1" ID="TxtAttachXeroxCopy" runat="server" placeholder="File No.of Certificate of Incorporation"
                                            CssClass="input-text ttip_r"></asp:TextBox>
                                        <asp:RequiredFieldValidator ValidationGroup="com" ID="RequiredFieldValidator17" runat="server"
                                            Display="Dynamic" ControlToValidate="TxtAttachXeroxCopy" ErrorMessage="Enter File No. of Certificate of Incorporation"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="LblDateOfTheResolution" runat="server" Text="Date of the Resolution"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox TabIndex="2" ID="TxtResolDate" runat="server" placeholder="Date of the Resolution"
                                            CssClass="input-text ttip_r maskdate"></asp:TextBox>
                                        <asp:RequiredFieldValidator ValidationGroup="com" ID="RequiredFieldValidator18" runat="server"
                                            Display="Dynamic" ControlToValidate="TxtResolDate" ErrorMessage="Enter Date of the Resolution"></asp:RequiredFieldValidator>
                                        <asp:CompareValidator ID="CompareValidator5" ValidationGroup="com" runat="server" Display="Dynamic" ErrorMessage="Enter Valid Date" ControlToValidate="TxtResolDate" Operator="DataTypeCheck" Type="Date"></asp:CompareValidator>

                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div class="boxheader">
                            <div style="margin: 0 auto;">
                                <asp:Button Style="margin: 0 auto" ID="button5" runat="server" CssClass="GreenyPushButton"
                                    ValidationGroup="com" Text="Add" OnClick="BtnAddition_Click" />
                                <asp:Button Style="margin: 0 auto" ID="Button6" runat="server" CausesValidation="false"
                                    OnClientClick="clearValidationErrors();" Text="Cancel" OnClick="BtnCancel_Click"
                                    CssClass="GreenyPushButton" />
                            </div>
                        </div>
                    </asp:Panel>
                    <asp:Panel DefaultButton="btnyes" CssClass="raised" ID="Pnlmsg" runat="server" Visible="false" Width="300px"
                        Style="min-height: 100px">
                        <asp:Label runat="server" ID="Label15" Text="" Visible="false"> </asp:Label>
                        <div style="top: 0px; height: 50px; text-align: center; padding: auto 0; width: 100%"
                            class="boxfooter">
                            <asp:Label ID="lblHD" runat="server" Text=""> </asp:Label>
                        </div>
                        <div id="Div3" style="min-height: 100px;text-align:center;">
                            <br />
                            <br />
                            <asp:Label ID="lblContent" runat="server" Text=""> </asp:Label>
                            <br />
                            <br />
                            <asp:PlaceHolder ID="DynamicControlsHolder" runat="server"></asp:PlaceHolder>
                        </div>
                        <div class="boxfooter">
                            <div style="margin: 0 auto; ">
                                <asp:Button Style="margin: 0 auto" CssClass="GreenyPushButton" ID="btnyes" OnClick="btnyes_click"
                                    runat="server" Text="Ok" />
                            </div>
                        </div>
                    </asp:Panel>
                    <asp:Panel DefaultButton="btnY" CssClass="raised" ID="Pnlmsg10" runat="server" Visible="false" Style="max-width: 1024;
                        max-height: 520px; min-width: 300px;">
                        <asp:Label runat="server" ID="Label5" Text="" Visible="false"> </asp:Label>
                        <div style="top: 0px; height: 50px; text-align: center; vertical-align: middle; line-height: 50px;
                            width: 100%" class="boxheader">
                            <asp:Label ID="lblHead" runat="server" Text="Status"> </asp:Label>
                            <br />
                        </div>
                        <div id="Div4" style="overflow: scroll;text-align:center;">
                            <asp:Label ID="lblcon" runat="server" Style="text-align: center; vertical-align: middle;"> </asp:Label>
                            <br />
                            <asp:GridView CssClass="aspxtable" Style="margin: auto; font-size: small; font-family: THelvetica, Arial, sans-serif"
                                ID="gvConfirm" AutoGenerateColumns="true" runat="server" Width="100%">
                            </asp:GridView>
                            <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
                        </div>
                        <div class="boxfooter" style=" width: 100%">
                            <asp:Button CssClass="GreenyPushButton" ID="btnY" OnClick="btnY_Click" runat="server" />
                            <asp:Button CssClass="GreenyPushButton" ID="btnno" OnClick="btnclear_Click" runat="server" />
                        </div>
                    </asp:Panel>
                </div>
            </div>
        </div>
    </div>
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

    </script>
    <script type="text/javascript">
        function check(txt) {
            if (txt.value.toString() == "" ) {
                document.getElementById('<%= TxtAge.ClientID %>').value = 0;
            }
        }
        function check1(txt1) {
            if (txt1.value.toString() == "") {
                document.getElementById('<%= TxtAge1.ClientID %>').value = 0;
            }
        }
    </script>
</asp:Content>
