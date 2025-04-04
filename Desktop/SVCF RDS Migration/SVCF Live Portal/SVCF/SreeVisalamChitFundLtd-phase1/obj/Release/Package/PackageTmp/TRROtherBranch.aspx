<%@ Page Title="SVCF - Admin Page" Culture="en-GB" Language="C#" MasterPageFile="~/Branch.Master"
    AutoEventWireup="true" MaintainScrollPositionOnPostback="true" EnableEventValidation="false"
    CodeBehind="TRROtherBranch.aspx.cs" Inherits="SreeVisalamChitFundLtd_phase1.TRROtherBranch" %>



<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMainContent" runat="server">
    <link href="Styles/tablecss.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        input[type="text"] {
            margin-bottom: 3px;
        }

        input[type="image"]:active {
            -moz-box-shadow: inset 0 0 10px rgba(0,0,0,.06);
            -webkit-box-shadow: inset 0 0 10px rgba(0,0,0,.06);
            box-shadow: inset 0 0 10px rgba(0,0,0,.06);
            z-index: 6;
            border: 2px solid #006469 !important;
        }

        .Trans div[id*="chzn"] {
            width: 100% !important;
        }

            .Trans div[id*="chzn"] span {
                width: 140px !important;
                overflow: hidden;
            }

        .Trans td {
            width: 14% !important;
            padding-left: 8px !important;
            padding-right: 8px !important;
        }

            .Trans td > input[type="text"] {
                width: 100% !important;
            }

            .Trans td div[class="ui-spinner"] > input[type="text"] {
                width: 100% !important;
            }

        .Trans select {
            width: 100% !important;
        }

        .hidable table {
            vertical-align: middle;
        }

        .hidable td {
            text-align: left;
            padding: 3px;
            margin: 3px;
        }

        .chzn-drop {
            border: 1px solid #aaa !important;
            padding-top: 5px !important;
        }

        div[id*="ddlMemberName_chzn"] .chzn-drop {
            width: 300% !important;
        }

        div[id*="ddlToken_chzn"] .chzn-drop {
            width: 100% !important;
        }

        div[id*="ddlMisc"] .chzn-drop {
            width: 200% !important;
        }
    </style>
    <ajax:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </ajax:ToolkitScriptManager>
    <asp:HiddenField ID="rcptfrmrange" runat="server" />
    <asp:HiddenField ID="rcpttorange" runat="server" />
    <div class="row">
        <div class="row">
            <div class="twelve columns">
                <div class="box_c">
                    <div class="box_c_heading  box_actions">
                        <p>
                            Cheque Receipt(Other Branch) Details
                        </p>
                    </div>
                    <div class="box_c_content">

                        <div class="row">
                             &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:CheckBox ID="CheckBox2" runat="server" Text="Chit Collection to be Accounted" />
                            <br />
                            <div style="display: inline; zoom: 1; text-align: center; width: 100%;">
                                <div style="margin-left: auto; margin-right: auto; display: table; width: 100%;">
                                    <div>
                                        <div style="width: 80%;">
                                            <table cellspacing="5" width="100%">
                                                <tr>
                                                    <td style="padding: 5px !important;">
                                                        <asp:Label ID="Label1" runat="server" Text="Collector Name"></asp:Label>
                                                        <br />
                                                        <asp:DropDownList Style="width:auto;" TabIndex="1" ID="ddlColloctorName"
                                                            CssClass="chzn-select" runat="server" AutoPostBack="false" CausesValidation="false"
                                                            OnChange="GetRSeries();">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="padding: 5px !important;">
                                                        <asp:Label ID="Label2" runat="server" Text="Receipt Series"></asp:Label>
                                                        <br />
                                                        <asp:DropDownList Style="width: 150px !important;" ID="ddlReceiptSeries" OnChange="GetRcptNumber();"
                                                            TabIndex="2" runat="server" AutoPostBack="false" CausesValidation="false">
                                                        </asp:DropDownList>
                                                        <asp:HiddenField ID="HD_RSeriesid" runat="server" />
                                                        <asp:HiddenField ID="HD_SID" runat="server" />
                                                    </td>
                                                    <td style="padding: 5px !important;">
                                                        <asp:Label ID="Label3" runat="server" Text="Received By"></asp:Label>
                                                        <br />
                                                        <asp:DropDownList runat="server" ID="ddlEmployee" OnChange="GetEmpname();" TabIndex="3"
                                                            CssClass="chzn-select" CausesValidation="false">
                                                        </asp:DropDownList>
                                                        <asp:HiddenField ID="HD_Empname" runat="server" />
                                                    </td>
                                                    <td style="padding: 5px !important;">
                                                        <asp:Label ID="Label5" runat="server" Text="Receipt Number"></asp:Label>
                                                        <br />
                                                        <asp:TextBox runat="server" ID="txtReceiptNumber" TabIndex="4"
                                                            Width="120" CausesValidation="false" CssClass="twitterStyleTextbox sp_number" autocomplete="off"></asp:TextBox>
                                                    </td>
                                                    <td style="padding: 5px !important;">
                                                        <asp:Label ID="Label6" runat="server" Text="Total Amount"></asp:Label>
                                                        <br />
                                                        <asp:TextBox ID="txtTotalAmount" CausesValidation="false" runat="server" TabIndex="5"
                                                            Width="120" CssClass="twitterStyleTextbox sp_currency" autocomplete="off"></asp:TextBox>
                                                    </td>
                                                    <td style="padding: 5px !important;">
                                                        <asp:Label ID="Label7" runat="server" Text="Receipt Date"></asp:Label>
                                                        <br />
                                                        <asp:TextBox ID="txtReceivedDate" Width="90" onchange="CheckDate();" ValidationGroup="a" runat="server"
                                                            TabIndex="6" CssClass="twitterStyleTextbox maskdate"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ForeColor="Red" ID="RequiredFieldValidator6" ErrorMessage="Required!!!"
                                                            ControlToValidate="txtReceivedDate" ValidationGroup="a" Display="Dynamic" runat="server"></asp:RequiredFieldValidator>
                                                        <asp:CompareValidator ForeColor="Red" Type="Date" ID="CompareValidator11" ValidationGroup="a"
                                                            ControlToValidate="txtReceivedDate" Display="Dynamic" Operator="DataTypeCheck" runat="server"
                                                            ErrorMessage="Incorrect!!!"></asp:CompareValidator>
                                                        <asp:RangeValidator ID="rvDate" EnableClientScript="false" runat="server"
                                                            Type="Date" ControlToValidate="txtReceivedDate" ValidationGroup="a" ForeColor="Red" Display="Dynamic"
                                                            ErrorMessage="*"></asp:RangeValidator>
                                                        <br />
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </div>
                                    <div class="box_c_heading cf">
                                        <div class="box_c_ico">
                                            <asp:Image runat="server" ID="img16List" ImageUrl="pertho_admin_v1.3/img/ico/icSw2/16-List.png"
                                                AlternateText="" />
                                        </div>
                                        <p>
                                            Transactions
                                        </p>
                                    </div>
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblbranchname" Text="Branch Name" runat="server"></asp:Label><br />
                                                <asp:DropDownList ID="listbranch" TabIndex="7" runat="server" OnChange="ListTkn();" CssClass="chzn-select" Width="300px">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label11" Text="ChitNo" runat="server"></asp:Label><br />
                                                <input id="txtsrch" tabindex="8" type="text" style="width:auto; height: 15px;" />
                                                <select id="abc1" tabindex="9" style="width:300px;" onchange="FindTokenName();"></select>
                                                <asp:HiddenField ID="tkn_id" runat="server" />
                                                <asp:HiddenField ID="hiddentkn_text" runat="server" />
                                            </td>
                                            <td>
                                                <asp:Label ID="Label8" Text="Narration" runat="server"></asp:Label><br />
                                                <asp:TextBox TabIndex="10" CausesValidation="false" ID="txtNarration" Width="300px"
                                                    CssClass="twitterStyleTextbox" runat="server"> </asp:TextBox>
                                            </td>
                                            </tr>
                                           <tr>
                                            <td>
                                                <asp:Label ID="Label9" Text="Amount" runat="server"></asp:Label><br />
                                                <asp:TextBox autocomplete="off" TabIndex="11" Width="300px" runat="server" CausesValidation="false"
                                                    CssClass="twitterStyleTextbox sp_currency" ID="txtAmount"></asp:TextBox>
                                            </td>
                                            
                                         
                                            <td>
                                                <asp:Label ID="Label10" Text="Other" runat="server"></asp:Label><br />
                                                <asp:DropDownList TabIndex="12" Width="300px" CausesValidation="false" ID="ddlMisc" 
                                                    CssClass="chzn-select" runat="server">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label12" Text="Misc Amount" runat="server"></asp:Label><br />
                                                <asp:TextBox TabIndex="13" Width="300px" runat="server" CausesValidation="false" CssClass="twitterStyleTextbox sp_currency" 
                                                    ID="txtMisc"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label13" Text="" runat="server"></asp:Label><br />
                                                <asp:Button ID="Button3" runat="server" Text="Add"
                                                    TabIndex="14" CssClass="btn-custom" ToolTip="Add" OnClick="Button3_Click" />
                                                <%--  <asp:ImageButton Width="20" Height="20" ImageUrl="~/Styles/Image/Images/AddReceipt.png"
                                                                 ID="ButtonAdd" runat="server" CausesValidation="false" 
                                                                TabIndex="13" CssClass="btn-custom" ToolTip="Add" OnClick="ButtonAdd_Click" />  --%>                                                            
                                            </td>
                                        </tr>
                                    </table>
                                    <div>
                                        <asp:GridView ID="GViewCROther_Selected" BorderStyle="Solid" runat="server" OnRowDeleting="GView_Selected_RowDeleting"
                                            CellSpacing="3" Font-Names="Verdana" ForeColor="#333333" GridLines="Both" Height="100px" TabIndex="15"
                                            AutoGenerateColumns="false" CssClass="Trans twelve columns" Width="900px" OnSelectedIndexChanged="GViewCROther_Selected_SelectedIndexChanged">
                                            <RowStyle BackColor="#F7F6F3" />
                                            <RowStyle CssClass="GridViewRowStyle" />
                                            <HeaderStyle CssClass="GridViewHeaderStyle" />
                                            <Columns>
                                                <asp:BoundField HeaderText="RowNumber" DataField="RowNumber" Visible="false"
                                                    HeaderStyle-BorderColor="Gray" HeaderStyle-BorderStyle="Solid" HeaderStyle-BorderWidth="2px"
                                                    ItemStyle-BorderColor="Gray" ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px" />

                                                <asp:BoundField HeaderText="Branch Name" DataField="BranchName"
                                                    HeaderStyle-BorderColor="Gray" HeaderStyle-BorderStyle="Solid" HeaderStyle-BorderWidth="2px"
                                                    ItemStyle-BorderColor="Gray" ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px" />

                                                <asp:BoundField HeaderText="ChitToken" DataField="chittoken"
                                                    HeaderStyle-BorderColor="Gray" HeaderStyle-BorderStyle="Solid" HeaderStyle-BorderWidth="2px"
                                                    ItemStyle-BorderColor="Gray" ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px" />

                                                <asp:BoundField HeaderText="Narration" DataField="Narration"
                                                    HeaderStyle-BorderColor="Gray" HeaderStyle-BorderStyle="Solid" HeaderStyle-BorderWidth="2px"
                                                    ItemStyle-BorderColor="Gray" ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px" />

                                                <asp:BoundField HeaderText="Amount" DataField="Amount" Visible="true"
                                                    HeaderStyle-BorderColor="Gray" HeaderStyle-BorderStyle="Solid" HeaderStyle-BorderWidth="2px"
                                                    ItemStyle-BorderColor="Gray" ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px" />

                                                <asp:BoundField HeaderText="Other" DataField="MiscHead"
                                                    HeaderStyle-BorderColor="Gray" HeaderStyle-BorderStyle="Solid" HeaderStyle-BorderWidth="2px"
                                                    ItemStyle-BorderColor="Gray" ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px" />

                                                <asp:BoundField HeaderText="Misc Amount" DataField="MiscAmount"
                                                    HeaderStyle-BorderColor="Gray" HeaderStyle-BorderStyle="Solid" HeaderStyle-BorderWidth="2px"
                                                    ItemStyle-BorderColor="Gray" ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px" />

                                                <asp:TemplateField HeaderText="Tokenid" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgp_tokenid" runat="server" Text='<%#Eval("GrpTokenid") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Branchid" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblBranchid" runat="server" Text='<%#Eval("Branchid") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="ReceiptNumber" Visible="true" HeaderStyle-BorderColor="Gray" HeaderStyle-BorderStyle="Solid"
                                                    HeaderStyle-BorderWidth="2px" ControlStyle-BorderStyle="Solid" ItemStyle-BorderWidth="2px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblrcnumber" runat="server" Text='<%#Eval("RCNumber") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="ref" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblref1" runat="server" Text='<%#Eval("firstmisc") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="ref1" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblref2" runat="server" Text='<%#Eval("secmisc") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField ItemStyle-BorderColor="Gray" ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px"
                                                    HeaderStyle-BorderColor="Gray" HeaderStyle-BorderStyle="Solid" HeaderStyle-BorderWidth="2px">
                                                    <ItemTemplate>
                                                        <span onclick="return confirm('Are you sure want to delete?')">
                                                            <asp:LinkButton ID="btnDelete" runat="server" CommandName="Delete">
                                                                <asp:Image ID="img1" runat="server" ImageUrl="Images/Close.gif" />
                                                            </asp:LinkButton>
                                                        </span>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                   <%-- <asp:GridView CssClass="trans twelve columns" OnRowDataBound="ordergrid_rowdatabound"
                                        ID="GridView1" AutoGenerateColumns="false" ShowFooter="true" BorderStyle="solid"
                                        CellSpacing="11" Font-Names="verdana" ForeColor="#333333" GridLines="none" runat="server" OnSelectedIndexChanged="gridview1_SelectedIndexChanged">
                                        <RowStyle BackColor="#f7f6f3" ForeColor="#333333" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="receipt number">
                                                <ItemTemplate>
                                                    <asp:TextBox autocomplete="off" CausesValidation="false" ID="txtreceiptno" runat="server" TabIndex="6"
                                                        ValidationGroup="a" CssClass="twitterstyletextbox sp_number"></asp:TextBox>
                                                    <asp:RequiredFieldValidator EnableClientScript="false" ID="rfvgrprowtxtreceiptno"
                                                        runat="server" Display="static" SetFocusOnError="true" ControlToValidate="txtreceiptno"
                                                        ValidationGroup="grprow" ErrorMessage="*" ForeColor="red"></asp:RequiredFieldValidator>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="branch name">
                                                <ItemTemplate>
                                                    <asp:DropDownList TabIndex="7" CausesValidation="false" ID="ddlbranchname" runat="server"
                                                        ValidationGroup="grprow" CssClass="chzn-select">
                                                    </asp:DropDownList>
                                                    <asp:CompareValidator EnableClientScript="false" ID="cvddlbranchname" runat="server"
                                                        ForeColor="red" ControlToValidate="ddlbranchname" Display="static" SetFocusOnError="true"
                                                        ErrorMessage="*" Operator="notequal" ValidationGroup="grprow" ValueToCompare="0"> </asp:CompareValidator>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="narration">
                                                <ItemTemplate>
                                                    <asp:TextBox TabIndex="8" CausesValidation="false" ID="txtnarration"
                                                        ValidationGroup="grprow" CssClass="twitterstyletextbox" runat="server">
                                                    </asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="amount">
                                                <ItemTemplate>
                                                    <asp:TextBox autocomplete="off" TabIndex="9" runat="server" CausesValidation="false" CssClass="twitterstyletextbox sp_currency"
                                                        ValidationGroup="grprow" ID="txtamount"></asp:TextBox>
                                                    <asp:RequiredFieldValidator EnableClientScript="false" ID="rfvtxtamount" ControlToValidate="txtamount"
                                                        Display="static" ForeColor="red" SetFocusOnError="true" ValidationGroup="grprow"
                                                        runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="other">
                                                <ItemTemplate>
                                                    <asp:DropDownList TabIndex="10" CausesValidation="false" ID="ddlmisc" ValidationGroup="grprowc"
                                                        CssClass="chzn-select" runat="server">
                                                    </asp:DropDownList>
                                                    <asp:CompareValidator EnableClientScript="false" ID="cvddlmisc" runat="server" ControlToValidate="ddlmisc"
                                                        Display="static" SetFocusOnError="true" ErrorMessage="*required" Operator="notequal"
                                                        ValidationGroup="grprowc" ValueToCompare="0"> </asp:CompareValidator>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="misc amount">
                                                <ItemTemplate>
                                                    <asp:TextBox TabIndex="11" runat="server" CausesValidation="false" CssClass="twitterstyletextbox sp_currency"
                                                        ValidationGroup="grprowa" ID="txtmisc"></asp:TextBox>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="right" />
                                                <FooterTemplate>
                                                    <div style="text-align: right; display: table-row">
                                                        <asp:Panel runat="server" ID="addpan" Style="display: table-cell" DefaultButton="buttonadd">
                                                            <asp:ImageButton Width="24" Height="24" ImageUrl="~/styles/image/images/addreceipt.png"
                                                                ID="buttonadd" runat="server" CausesValidation="true" ValidationGroup="grprow"
                                                                TabIndex="12" CssClass="greenypushbutton" ToolTip="add"/>
                                                        </asp:Panel>
                                                        <asp:Panel Style="display: table-cell" ID="panel1" runat="server" DefaultButton="buttonremove">
                                                            <asp:ImageButton Width="24" Height="24" ImageUrl="~/styles/image/images/removereceipt.png"
                                                                ID="buttonremove" runat="server" CausesValidation="false" TabIndex="13" CssClass="greenypushbutton"
                                                                ToolTip="remove" />
                                                        </asp:Panel>
                                                    </div>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>--%>
                                    <asp:CheckBox Checked="true" Visible="false" ID="CheckBox1" runat="server" />
                                    <div style="width: 70%; text-align: center !important; margin: 0 auto;">
                                        <table class="hidable">
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label4" runat="server" Text="Cheque Number"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox autocomplete="off" AutoPostBack="true" OnTextChanged="Cheque_TextCahanged" CausesValidation="false" ID="txtCheque" CssClass="twitterStyleTextbox sp_number"
                                                        TabIndex="14" MaxLength="6" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator EnableClientScript="false" ID="RFVtxtCheque" ControlToValidate="txtCheque"
                                                        Display="Static" SetFocusOnError="true" ValidationGroup="b" runat="server" ForeColor="Red"
                                                        ErrorMessage="*"></asp:RequiredFieldValidator>
                                                    <br />
                                                    <asp:Label runat="server" ID="lbVisible" ForeColor="Red" Text="Cheque Number already Exists." Visible="false"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblBL" runat="server" Text="Bank Details"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList Style="width: 200px !important;" AutoPostBack="true" OnTextChanged="Cheque_TextCahanged" TabIndex="15" CausesValidation="false"
                                                        runat="server" ID="ddlBanklDetails" CssClass="chzn-select">
                                                    </asp:DropDownList>
                                                    <asp:CompareValidator EnableClientScript="false" ID="RFVddlBanklDetails" ControlToValidate="ddlBanklDetails"
                                                        ValueToCompare="--Select--" Display="Static" SetFocusOnError="true" ValidationGroup="b"
                                                        runat="server" ForeColor="Red" Operator="NotEqual" ErrorMessage="*"></asp:CompareValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblBankHead" runat="server" Text="Bank Head"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList Style="width: 200px !important;" ID="ddlBankHead" CssClass="chzn-select"
                                                        ValidationGroup="b" TabIndex="16" runat="server" CausesValidation="false">
                                                    </asp:DropDownList>
                                                    <asp:CompareValidator EnableClientScript="false" ID="CVddlBankHead" ForeColor="Red"
                                                        ValueToCompare="0" ControlToValidate="ddlBankHead" Display="Dynamic" Operator="NotEqual"
                                                        SetFocusOnError="true" ValidationGroup="b" runat="server" ErrorMessage="*"></asp:CompareValidator>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblDate" runat="server" Text="Cheque Date"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox CausesValidation="false" ID="txtDateinCheque" TabIndex="17" CssClass="twitterStyleTextbox maskdate"
                                                        Width="100" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator EnableClientScript="false" ID="RFVtxtDateinCheque" ControlToValidate="txtDateinCheque"
                                                        Display="Static" SetFocusOnError="true" ForeColor="Red" ValidationGroup="b" runat="server"
                                                        ErrorMessage="*"></asp:RequiredFieldValidator>
                                                    <asp:CompareValidator EnableClientScript="false" ID="CompareValidator2" runat="server"
                                                        ControlToValidate="txtDateinCheque" ValidationGroup="b" Type="Date" ErrorMessage="*"
                                                        ForeColor="Red" Display="Dynamic" Operator="DataTypeCheck"></asp:CompareValidator>
                                                    <asp:RangeValidator ID="rvChequeDate" EnableClientScript="false" runat="server" Type="Date"
                                                        ControlToValidate="txtDateinCheque" ValidationGroup="b" ForeColor="Red" Display="Dynamic"
                                                        ErrorMessage="*"></asp:RangeValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label runat="server" ID="lbIdCardNumber" Text="Card Number"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox runat="server" ID="txtIdcardNumber" TabIndex="18" autocomplete="off"
                                                        CssClass="twitterStyleTextbox"></asp:TextBox>
                                                    <br />
                                                    <asp:RequiredFieldValidator ControlToValidate="txtIdcardNumber" Display="Dynamic"
                                                        EnableClientScript="false" ErrorMessage="*" ForeColor="Red" ID="rvCardNumber"
                                                        runat="server" ValidationGroup="b"></asp:RequiredFieldValidator>
                                                </td>
                                                <td>
                                                    <asp:Label runat="server" ID="lbCardType" Text="Card Type"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList Style="width: 200px !important;" ID="ddlCardType" CssClass="chzn-select"
                                                        ValidationGroup="b" TabIndex="19" runat="server" CausesValidation="false">
                                                        <asp:ListItem Text="--Select--" Value="--Select--"></asp:ListItem>
                                                        <asp:ListItem Text="Driving Licence" Value="Driving Licence"></asp:ListItem>
                                                        <asp:ListItem Text="Passport" Value="Passport"></asp:ListItem>
                                                        <asp:ListItem Text="PAN Card" Value="PAN Card"></asp:ListItem>
                                                        <asp:ListItem Text="Voter ID Card" Value="Voter ID Card"></asp:ListItem>
                                                        <asp:ListItem Text="Bank Passbook" Value="Bank Passbook"></asp:ListItem>
                                                        <asp:ListItem Text="Aadhaar ID" Value="Aadhaar ID"></asp:ListItem>
                                                        <asp:ListItem Text="Ration Card" Value="Ration Card"></asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:CompareValidator EnableClientScript="false" ID="cvddlCardType" ForeColor="Red"
                                                        ValueToCompare="--Select--" ControlToValidate="ddlCardType" Display="Dynamic"
                                                        Operator="NotEqual" SetFocusOnError="true" ValidationGroup="b" runat="server"
                                                        ErrorMessage="*"></asp:CompareValidator>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
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
    <ajax:ModalPopupExtender ID="ModalPopupExtender1" TargetControlID="show" PopupControlID="Pnlgendrate"
        BackgroundCssClass="modalBackground" runat="server">
    </ajax:ModalPopupExtender>
    <asp:Panel Style="background: #fff; border: 4px solid #ccc; border: 8px solid rgba(0, 0, 0, 0.5); background-clip: padding-box; -webkit-border-radius: 10px; -moz-border-radius: 10px; border-radius: 10px; min-width: 300px; max-width: 900px;"
        CssClass="raised" ID="pnlConfirmation"
        runat="server" Visible="false">
        <asp:Label runat="server" ID="lblHintConfirmation" Text="" Visible="false"> </asp:Label>
        <div class=" box_c_heading" style="text-align: center !important;">
            <p>
                <asp:Label runat="server" ID="lblHeadingConfirmation" Text=""> </asp:Label>
            </p>
        </div>
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
                </asp:GridView>
            </div>
        </div>
        <div class=" box_c_heading">
            <div style="float: right;">
                <asp:Button Style="margin: 0 auto" CssClass="GreenyPushButton" ID="Button1" OnClick="btnConfirmationYes_Click"
                    runat="server" Text="yes" />
                <asp:Button Style="margin: 0 auto" CssClass="GreenyPushButton" ID="Button2" OnClick="btnConfirmationNo_Click"
                    runat="server" Text="No" />
            </div>
        </div>
    </asp:Panel>
    <asp:Panel ID="Pnlgendrate" Style="background: #fff; border: 4px solid #ccc; border: 8px solid rgba(0, 0, 0, 0.5); background-clip: padding-box; -webkit-border-radius: 10px; -moz-border-radius: 10px; border-radius: 10px; min-width: 300px; max-width: 900px;"
        CssClass="raised" runat="server"
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
                <asp:Button Style="margin: 0 auto" CssClass="GreenyPushButton" ID="btnyes" OnClick="btnyes_Click"
                    runat="server" Text="yes" />
                <asp:Button Style="margin: 0 auto" CssClass="GreenyPushButton" ID="btnNo" OnClick="btnNo_Click"
                    runat="server" Text="No" />
            </div>
        </div>
    </asp:Panel>
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
                <asp:Button Style="margin: 0 auto" CssClass="GreenyPushButton" ID="BtnOK" OnClick="btnOK_Click"
                    runat="server" Text="Ok" />
            </div>
        </div>
    </asp:Panel>
    <asp:LinkButton runat="server" ID="show" Text=""></asp:LinkButton>
    <script type="text/javascript">
        function checkchqdate(txt) {
            $('#<%=txtDateinCheque.ClientID%>').val(txt.value);
        }
        function checkdate(txt) {
            $('#<%=txtReceivedDate.ClientID%>').val(txt.value);
        }
        $(document).ready(function () {
            $(".chzn-select").chosen({ search_contains: true });

            $(".sp_currency").numeric({ negative: false });
            $(".sp_number").numeric({ decimal: false, negative: false }, function () { alert("Positive integers only"); this.value = ""; this.focus(); });
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
    <script type = "text/javascript">
      
        function DisableButton() {
            debugger;
    document.getElementById("<%=Button1.ClientID %>").disabled = true;
}
window.onbeforeunload = DisableButton;
</script>
    <script type="text/javascript">
        function CheckDate() {
            var inputDate = $('#<%=txtReceivedDate.ClientID %>').val();
            var Reg_Expression = /^\d{1,2}\/\d{1,2}\/\d{4}$/;
            if (Reg_Expression.test(inputDate)) {
                var partitionedDate = inputDate.split('/');
                var nDay = parseInt(partitionedDate[0], 10);
                var nMonth = parseInt(partitionedDate[1], 10);
                var nYear = parseInt(partitionedDate[2], 10);
                var dDate = new Date(nYear, nMonth - 1, nDay);
                if ((dDate.getFullYear() == nYear) && (dDate.getMonth() == nMonth - 1) && (dDate.getDate() == nDay)) {
                }
                else {
                    document.getElementById('<%=txtReceivedDate.ClientID %>').value = "";
                     alert('Incorrect Date Format..');
                 }
             }
             else {
                 document.getElementById('<%=txtReceivedDate.ClientID %>').value = "";
                 alert('Incorrect Date Format..');
             }
         }


         function GetRSeries() {
             $("#<%=ddlColloctorName.ClientID%>").change(function () {
               var myparam = $("#<%=ddlColloctorName.ClientID%>").val(); //id name for dropdown list              
               $.ajax({
                   type: "POST",
                   contentType: "application/json; charset=utf-8",
                   url: "TRROtherBranch.aspx/CRSeries_OtherBranch",
                   data: "{mcid:" + myparam + "}",
                   dataType: "json",
                   success: function (data) {
                       var ddrseries = $("[id*=ddlReceiptSeries]");
                       ddrseries.empty();
                       $.each(data.d, function () {
                           ddrseries.append($("<option></option>").val(this['Value']).html(this['Text']));
                       });
                   },
                   error: function (result) {
                       alert("Error: " + result);
                   }
               });
               $("#<%=ddlColloctorName.ClientID%>").addClass('chzn-select');

           });

           GetRcNum();
       }

       function GetRcNum() {
           var ser = $("#<%=ddlReceiptSeries.ClientID%>").text(); //id name for dropdown list              
           //var cid = $("#<%=ddlReceiptSeries.ClientID%>").val(); //text name for dropdown list  
           var cid = $("#<%=ddlColloctorName.ClientID%>").find("option:selected").val(); //text name for dropdown list  
           var colname = $("#<%=ddlColloctorName.ClientID%>").find("option:selected").text(); //text name for dropdown list  
           $.ajax({
               type: "POST",
               contentType: "application/json; charset=utf-8",
               url: "TRROtherBranch.aspx/getRcptNumber",
               data: JSON.stringify({ Series: ser, CollectorID: cid }),
               //data: "{Series:" + ser + ",CollectorID:" + cid + "}",
               //data: {"Series:" + series + ",CollectorID:" + collid + },
               // data: JSON.stringify(obj),
               //data: {},
               dataType: "json",
               success: function (data) {
                   var msg = data.d.toString();
                   var txtsnumber = $("[id*=txtReceiptNumber]");
                   txtsnumber.val(msg);
               },
               error: function (result) {
                   alert("Error: " + result);
               }
           });
           $('#ctl00_cphMainContent_ddlEmployee').val(colname);
           $("#<%= HD_Empname.ClientID %>").val(colname);
        }

        $(document).ready(function () {
            $("#<%=ddlColloctorName.ClientID%>").change(function () {
                var myparam = $("#<%=ddlColloctorName.ClientID%>").val(); //id name for dropdown list              
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    url: "TRROtherBranch.aspx/CRSeries_OtherBranch",
                    data: "{mcid:" + myparam + "}",
                    dataType: "json",
                    success: function (data) {
                        var ddrseries = $("[id*=ddlReceiptSeries]");
                        ddrseries.empty();
                        $.each(data.d, function () {
                            ddrseries.append($("<option></option>").val(this['Value']).html(this['Text']));
                        });
                    },
                    error: function (result) {
                        alert("Error: " + result);
                    }
                });
                $("#<%=ddlColloctorName.ClientID%>").addClass('chzn-select');
            });

            GetRcNum();
        });


       function GetRcptNumber() {
           var ser = $("#<%=ddlReceiptSeries.ClientID%>").find("option:selected").text(); //id name for dropdown list              
           var cid = $("#<%=ddlReceiptSeries.ClientID%>").val(); //text name for dropdown list    
           var colname = $("#<%=ddlColloctorName.ClientID%>").find("option:selected").text(); //text name for dropdown list  
           $.ajax({
               type: "POST",
               contentType: "application/json; charset=utf-8",
               url: "TRROtherBranch.aspx/gtRcptBkNumber",
               data: JSON.stringify({ Series: ser, CollectorID: cid }),
               dataType: "json",
               success: function (data) {
                   var msg = data.d.toString();
                   var txtsnumber = $("[id*=txtReceiptNumber]");
                   txtsnumber.val(msg);
               },
               error: function (result) {
                   alert("Error: " + result);
               }
           });

           $('#ctl00_cphMainContent_ddlEmployee').val(colname);
           $("#<%=ddlEmployee.ClientID%>").find(selectvalue).attr("selected", "selected");
            $("#<%= HD_Empname.ClientID %>").val(colname);
       }

       function GetEmpname() {
           var ser = $("#<%=ddlEmployee.ClientID%>").find("option:selected").text();
            $("#<%= HD_Empname.ClientID %>").val(ser);
        }

        $(document).ready(function () {

            $("#<%=ddlReceiptSeries.ClientID%>").change(function () {
                 var ser = $("#<%=ddlReceiptSeries.ClientID%>").find("option:selected").text(); //id name for dropdown list              
                 var cid = $("#<%=ddlReceiptSeries.ClientID%>").val(); //text name for dropdown list               
                 $.ajax({
                     type: "POST",
                     contentType: "application/json; charset=utf-8",
                     url: "TRROtherBranch.aspx/gtRcptBkNumber",
                     data: JSON.stringify({ Series: ser, CollectorID: cid }),
                     dataType: "json",
                     success: function (data) {
                         var msg = data.d.toString();
                         var txtsnumber = $("[id*=txtReceiptNumber]");
                         txtsnumber.val(msg);
                     },
                     error: function (result) {
                         alert("Error: " + result);
                     }
                 });
             });
         });


        <%--<%--<%-- $(document).ready(function () {
            $("#<%=ddlBranchName.ClientID%>").change(function () {
                var myparam = $("#<%=ddlBranchName.ClientID%>").find("option:selected").val(); //id name for dropdown list              
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    url: "CRROtherBranch.aspx/ChitToken",
                    data: "{branchid:" + myparam + "}",
                    dataType: "json",
                    success: function (data) {
                        var ddrseries = $("[id*=ddlChitno]");
                        ddrseries.empty().append('<option selected="selected" value="0">Please select</option>');
                        $.each(data.d, function () {
                            ddrseries.append($("<option></option>").val(this['Value']).html(this['Text']));
                        });
                    },
                    error: function (result) {
                        alert("Error: " + result);
                    }
                });
                $("#<%=ddlChitno.ClientID%>").addClass('chzn-select');               
            });
        });--%>


        <%--    function GetChitToken() {
         <%--  $("#<%=ddlBranchName.ClientID%>").change(function () {
           var myparam = $("#<%=ddlBranchName.ClientID%>").find("option:selected").val(); //id name for dropdown list  
         <%-- var selser = $("#<%=ddlBranchName.ClientID%>").find("option:selected").val();   --%>          
             <%--  $("#<%= HD_SelectedChit.ClientID %>").val(selser);
             
               $.ajax({
                   type: "POST",
                   contentType: "application/json; charset=utf-8",
                   url: "CRROtherBranch.aspx/ChitToken",
                   data: "{branchid:" + myparam + "}",
                   dataType: "json",
                   success: function (data) {
                       //var ddrseries = $("[id*=ddlChitno]");
                       //ddrseries.empty().append('<option selected="selected" value="0">Please select</option>');
                       //$.each(data.d, function () {
                       //    ddrseries.append($("<option></option>").val(this['Value']).html(this['Text']));
                       //});
                       var options = $("#abc1");
                       //$.each(data.d, function () {
                       //    options.append(new Option(this.text, this.value));
                       //});
                       for (var i = 0; i < data.d.length; i++) {
                           $("#abc1").append("<option value='" + data[i].Head_Id + "'> " + data[i].GrpMemberID + "</option>");
                       }
                   },
                   error: function (result) {
                       alert("Error: " + result);
                   }
               });
               $("#<%=ddlChitno.ClientID%>").addClass('chzn-select');              
             
           //});
       }--%>


        function ListTkn() {
             <%--  $("#<%=ddlBranchName.ClientID%>").change(function () {--%>
            var gt = $("#<%=listbranch.ClientID%>").find("option:selected").val(); //id name for dropdown list  
            <%-- var selser = $("#<%=ddlBranchName.ClientID%>").find("option:selected").val();   --%>
             <%--  $("#<%= HD_SelectedChit.ClientID %>").val(selser);
            --%>
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "TRROtherBranch.aspx/ChitToken",
                data: "{branchid:" + gt + "}",
                dataType: "json",
                success: function (data) {
                    var options = $("#abc1");
                    options.empty().append('<option selected="selected" value="0">Please select</option>');
                    for (var i = 0; i < data.d.length; i++) {
                        $("#abc1").append("<option value='" + data.d[i].Value + "'> " + data.d[i].Text + "</option>");
                    }
                },
                error: function (result) {
                    alert("Error: " + result);
                }
            });

            //});
        }

        function FindTokenName() {
            var tkid = $('#abc1').find('option:selected').val();
            var tkntext = $('#abc1').find('option:selected').text();

            $("#<%= hiddentkn_text.ClientID %>").val("");
             $("#<%= hiddentkn_text.ClientID %>").val(tkntext);


             $("#<%= tkn_id.ClientID %>").val("");
             $("#<%= tkn_id.ClientID %>").val(tkid);

             $.ajax({
                 type: "POST",
                 contentType: "application/json; charset=utf-8",
                 url: "TRROtherBranch.aspx/GetCustomername",
                 data: "{hdid:" + tkid + "}",
                 dataType: "json",
                 success: function (data) {
                     var msg = data.d.toString();
                     var txtsnumber = $("[id*=txtNarration]");
                     txtsnumber.val(msg);
                 },
                 error: function (result) {
                     alert("Error: " + result);
                 }
             });

             var selser = "";
             selser = $("#<%=ddlReceiptSeries.ClientID%>").find("option:selected").text();

             selid = $("#<%=ddlReceiptSeries.ClientID%>").find("option:selected").val(); //text name for dropdown list  
             selser = selser.replace(/(\r\n\t|\n|\r|\t)/gm, "");
             selser = selser.trim();
             $("#<%= HD_RSeriesid.ClientID %>").val("");
             $("#<%= HD_RSeriesid.ClientID %>").val(selser);

         }

         $(document).ready(function () {
             $('#txtsrch').change(function () {
                 var gt = $("#<%=listbranch.ClientID%>").find("option:selected").val(); //id name for dropdown list  
                 <%-- var selser = $("#<%=ddlBranchName.ClientID%>").find("option:selected").val();   --%>
                 var txt = $("#txtsrch").val();
                 if (txt != "") {
                     $.ajax({
                         type: "POST",
                         contentType: "application/json; charset=utf-8",
                         url: "TRROtherBranch.aspx/Getsrchlist",
                         data: JSON.stringify({ branchid: gt, seltext: txt }),
                         dataType: "json",
                         success: function (data) {
                             var options = $("#abc1");
                             options.empty().append('<option selected="selected" value="0">Please select</option>');
                             for (var i = 0; i < data.d.length; i++) {
                                 $("#abc1").append("<option value='" + data.d[i].Value + "'> " + data.d[i].Text + "</option>");
                             }
                         },
                         error: function (result) {
                             alert("Error: " + result);
                         }
                     });
                 }
                 else {
                     ListTkn();
                 }

             });
         });

    </script>
</asp:Content>
