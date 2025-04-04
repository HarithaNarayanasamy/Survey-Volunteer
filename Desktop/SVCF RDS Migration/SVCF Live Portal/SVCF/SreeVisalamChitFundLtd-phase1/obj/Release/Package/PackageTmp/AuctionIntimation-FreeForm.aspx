<%@ Page Language="C#" MasterPageFile="~/Branch.Master" AutoEventWireup="true" CodeBehind="AuctionIntimation-FreeForm.aspx.cs"
    Inherits="SreeVisalamChitFundLtd_phase1.AuctionIntimation_FreeForm" Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="TimePicker" Namespace="MKB.TimePicker" TagPrefix="MKB" %>
<%@ Register TagPrefix="uc1" TagName="TimePicker" Src="~/ucTimePicker.ascx" %>
<asp:Content ID="Head1" ContentPlaceHolderID="cphHead" runat="server">
    <style type="text/css">
        td
        {
            text-align: left;
        }
    </style>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMainContent" runat="server">
    <ajax:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </ajax:ToolkitScriptManager>
    <div>
        <div class="content" style="height: 450px;">
            <div style="background: white; border-bottom: 10px solid white;">
                <div class="ribheader">
                    Auction Intimation Letter
                </div>
            </div>
            <br />
            <div style="display: table; margin: 0px auto; padding-left: 108px;">
                <table cellspacing="3" style="margin: 0px auto; display: table-cell;">
                    <tr>
                        <td>
                            <asp:Label ID="lblbranch" runat="server" Text="Branch Name"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="branch" TabIndex="1" ValidationGroup="add" runat="server" CssClass="twitterStyleTextbox"></asp:TextBox>
                        </td>
                        <td>
                            <asp:RequiredFieldValidator CssClass="bubble" ID="frvbranch" ValidationGroup="add"
                                runat="server" ControlToValidate="branch" ErrorMessage="*"></asp:RequiredFieldValidator>
                            <td>
                                <asp:Label ID="lblbranchadd" runat="server" Text="Branch Address"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox Width="150" ID="txtbranchaddr" Height="50" TabIndex="2" TextMode="MultiLine"
                                    CssClass="twitterStyleTextbox" ValidationGroup="add" runat="server"></asp:TextBox>
                            </td>
                            <td>
                                <asp:RequiredFieldValidator CssClass="bubble" ID="rfvbranchadd" runat="server" ValidationGroup="add"
                                    ControlToValidate="txtbranchaddr" ErrorMessage="*"></asp:RequiredFieldValidator>
                            </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblgroupno" runat="server" Text="Group Number"></asp:Label>
                        </td>
                        <td align="left">
                            <asp:DropDownList Height="30px" OnSelectedIndexChanged="ddlGroupNO_SelectedIndexChanged"
                                ID="ddlGroupNO" TabIndex="3" AutoPostBack="true" CssClass="twitterStyleTextbox"
                                runat="server">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:RequiredFieldValidator CssClass="bubble" ID="RequiredFieldValidator1" runat="server"
                                ValidationGroup="add" ControlToValidate="ddlGroupNO" ErrorMessage="*"></asp:RequiredFieldValidator>
                        </td>
                        <td>
                            <asp:Label ID="lblselectedgroupno" runat="server" Text="Selected Group Number"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtselgroup_no" TabIndex="4" CssClass="twitterStyleTextbox" ValidationGroup="add"
                                runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:RequiredFieldValidator CssClass="bubble" ID="rfvgroupno" runat="server" ValidationGroup="add"
                                ControlToValidate="txtselgroup_no" ErrorMessage="*"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lbl_chit_agree_no" runat="server" Text="Chit Agreement No"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtchit_agree_no" TabIndex="5" ValidationGroup="add" runat="server"
                                CssClass="twitterStyleTextbox"></asp:TextBox>
                        </td>
                        <td>
                            <asp:RequiredFieldValidator CssClass="bubble" ID="RequiredFieldValidator7" ValidationGroup="add"
                                runat="server" ControlToValidate="txtchit_agree_no" ErrorMessage="*"></asp:RequiredFieldValidator>
                        </td>
                        <td>
                            <asp:Label ID="lbl_chit_agree_year" runat="server" Text="Chit Agreement Year"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txt_chit_agree_year" TabIndex="6" ValidationGroup="add" runat="server"
                                CssClass="twitterStyleTextbox"></asp:TextBox>
                        </td>
                        <td>
                            <asp:RequiredFieldValidator CssClass="bubble" ID="RequiredFieldValidator9" ValidationGroup="add"
                                runat="server" ControlToValidate="txt_chit_agree_year" ErrorMessage="*"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator CssClass="bubble" ID="RegularExpressionValidator5"
                                runat="server" Font-Size="Smaller" ControlToValidate="txt_chit_agree_year" ErrorMessage="Year in No."
                                ValidationGroup="add" ValidationExpression="\d+"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label1" runat="server" Text="Auction Date"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtauction_dt" TabIndex="7" 
                                CssClass="twitterStyleTextbox" runat="server" ValidationGroup="add"></asp:TextBox>
                            <asp:CalendarExtender ID="txtauction_dt_CalendarExtender" runat="server" Format="dd/MM/yy"
                                TargetControlID="txtauction_dt">
                            </asp:CalendarExtender>
                        </td>
                        <td>
                            <asp:RequiredFieldValidator CssClass="bubble" ID="RequiredFieldValidator3" ValidationGroup="add"
                                runat="server" ControlToValidate="txtauction_dt" ErrorMessage="*"></asp:RequiredFieldValidator>
                        </td>
                        <td>
                            <asp:Label ID="lblauction_time" runat="server" Text="Auction Time"></asp:Label>
                        </td>
                        <td style="width: 19%; height: 40px" align="left">
                            <MKB:TimeSelector ID="TimeSelAuction" Width="163px" SelectedTimeFormat="Twelve" TabIndex="8"
                                runat="server" Height="20px">
                            </MKB:TimeSelector>
                        </td>
                        <td>
                            <asp:RequiredFieldValidator CssClass="bubble" ID="RequiredFieldValidator8" ValidationGroup="add"
                                runat="server" ControlToValidate="TimeSelAuction" ErrorMessage="*"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblDrawNo" runat="server" Text="Current Draw NO"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtcurrdraw" TabIndex="9" ValidationGroup="add" runat="server" CssClass="twitterStyleTextbox"></asp:TextBox>
                        </td>
                        <td>
                            <asp:RequiredFieldValidator CssClass="bubble" ID="RequiredFieldValidator22" runat="server"
                                ValidationGroup="add" ControlToValidate="txtcurrdraw" ErrorMessage="*"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator CssClass="bubble" ID="RegularExpressionValidator4"
                                runat="server" Font-Size="Smaller" ControlToValidate="txtcurrdraw" ErrorMessage="* Numeric"
                                ValidationGroup="add" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
                        </td>
                        <td>
                            <asp:Label ID="lblCurrDueAmount" runat="server" Text="Current Due Amount"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtcurrdueamount" TabIndex="10" CssClass="twitterStyleTextbox" 
                                ValidationGroup="add" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:RequiredFieldValidator CssClass="bubble" Style="text-align: center" ID="RequiredFieldValidator23"
                                ValidationGroup="add" runat="server" ControlToValidate="txtcurrdueamount" ErrorMessage="*"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator CssClass="bubble" Style="text-align: center" ID="rvtxtAmount"
                                runat="server" ErrorMessage="Invalid Amount" Font-Size="X-Small" ControlToValidate="txtcurrdueamount"
                                ValidationExpression="^\d+(\.\d{1,2})?$" TabIndex="1"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblprevdraw" runat="server" Text="Chit Category"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList Height="30px" ID="ddlChitCategory" TabIndex="11" AutoPostBack="true"
                                CssClass="twitterStyleTextbox" runat="server">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:RequiredFieldValidator CssClass="bubble" ID="RequiredFieldValidator2" runat="server"
                                ValidationGroup="add" ControlToValidate="ddlChitCategory" ErrorMessage="*"></asp:RequiredFieldValidator>
                        </td>
                        <td>
                            <asp:Label ID="lblprevdividend" runat="server" Text="Prev. Dividend"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtprevdividend" TabIndex="12" CssClass="twitterStyleTextbox" ValidationGroup="add"
                                runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:RequiredFieldValidator CssClass="bubble" ID="RequiredFieldValidator4" ValidationGroup="add"
                                runat="server" ControlToValidate="txtprevdividend" ErrorMessage="*"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator CssClass="bubble" ID="RegularExpressionValidator2"
                                runat="server" ErrorMessage="Invalid Amount" Font-Size="Smaller" ControlToValidate="txtprevdividend"
                                ValidationExpression="^\d+(\.\d{1,2})?$" TabIndex="1"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblduedate" runat="server" Text="Due Date"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtduedate" TabIndex="13" 
                                CssClass="twitterStyleTextbox" runat="server" ValidationGroup="add"></asp:TextBox>
                            <asp:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yy" TargetControlID="txtduedate">
                            </asp:CalendarExtender>
                        </td>
                        <td>
                            <asp:RequiredFieldValidator CssClass="bubble" ID="RequiredFieldValidator6" ValidationGroup="add"
                                runat="server" ControlToValidate="txtduedate" ErrorMessage="*"></asp:RequiredFieldValidator>
                        </td>
                        <td>
                            <asp:Label ID="lblprevprizeamount" runat="server" Text="Prev. Prized Amount"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtprizedamount" TabIndex="14" 
                                CssClass="twitterStyleTextbox" ValidationGroup="add" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:RequiredFieldValidator CssClass="bubble" ID="RequiredFieldValidator5" ValidationGroup="add"
                                runat="server" ControlToValidate="txtprizedamount" ErrorMessage="*"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator CssClass="bubble" ID="RegularExpressionValidator3"
                                ValidationGroup="add" runat="server" ErrorMessage="Invalid Amount" Font-Size="X-Small"
                                ControlToValidate="txtprizedamount" ValidationExpression="^\d+(\.\d{1,2})?$"
                                TabIndex="1"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <%-- <tr style="width: 80%; height: 80px;">
            <td style="width: 19%; height: 80px;"><br /></br></td>
           <td style="width: 19%; height: 80px;" text-align:"center"><asp:Button CausesValidation="false"  ID="btnautofill" Text="Auto Fill" CssClass="btn-style" runat="server" TabIndex="15" onclick="btnautofill_Click" /> </td> 
        <td style="width: 2%; height: 80px;"><br /></br></td>
           <td style="width: 19%; height: 80px;" text-align:"center"><asp:Button ValidationGroup="add" CausesValidation ="true" ID="btngenerate" Text="Generate" CssClass="btn-style" runat="server" TabIndex="16" onclick="btnGenerate_Click" /> </td> 
           <td style="width: 19%; height: 80px;"><br /></br></td>
         </tr>--%>
                </table>
            </div>
            <br />
            <asp:Button CausesValidation="false" ID="btnautofill" runat="server" CssClass="GreenyPushButton"
                TabIndex="15" Style="margin: 0px auto;" Text="Auto Fill" OnClick="btnautofill_Click" />
            <asp:Button ID="Btngenerate" runat="server" CssClass="GreenyPushButton" TabIndex="16"
                Text="Generate" CausesValidation="true" Style="margin: 0px auto;" OnClick="btnGenerate_Click" />
            <br />
        </div>
    </div>
</asp:Content>
