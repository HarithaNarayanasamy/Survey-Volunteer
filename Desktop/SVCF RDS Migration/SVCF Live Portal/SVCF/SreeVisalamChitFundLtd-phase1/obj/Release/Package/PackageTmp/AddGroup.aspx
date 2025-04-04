<%@ Page Title="" Language="C#" MasterPageFile="~/Branch.Master" AutoEventWireup="true"
    CodeBehind="AddGroup.aspx.cs" Inherits="SreeVisalamChitFundLtd_phase1.WebForm5" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Head1" ContentPlaceHolderID="cphHead" runat="server">
    <style type="text/css">
        td
        {
            text-align: left;
        }
        
        
        .popupcontent
        {
            background-color: #e2e2e2;
            border-top-left-radius: 0px;
            border-top-right-radius: 0px;
            padding: 0;
            font-color: black;
            box-shadow: 0 2px 6px rgba(0,0,0,0.5), inset 0 1px rgba(255,255,255,0.3), inset 0 10px rgba(255,255,255,0.2), inset 0 10px 20px rgba(255,255,255,0.25), inset 0 -15px 30px rgba(0,0,0,0.3);
            -o-box-shadow: 0 2px 6px rgba(0,0,0,0.5), inset 0 1px rgba(255,255,255,0.3), inset 0 10px rgba(255,255,255,0.2), inset 0 10px 20px rgba(255,255,255,0.25), inset 0 -15px 30px rgba(0,0,0,0.3);
            -webkit-box-shadow: 0 2px 6px rgba(0,0,0,0.5), inset 0 1px rgba(255,255,255,0.3), inset 0 10px rgba(255,255,255,0.2), inset 0 10px 20px rgba(255,255,255,0.25), inset 0 -15px 30px rgba(0,0,0,0.3);
            -moz-box-shadow: 0 2px 6px rgba(0,0,0,0.5), inset 0 1px rgba(255,255,255,0.3), inset 0 10px rgba(255,255,255,0.2), inset 0 10px 20px rgba(255,255,255,0.25), inset 0 -15px 30px rgba(0,0,0,0.3);
        }
        .popup
        {
            padding: 0;
            font-color: black;
            box-shadow: 0 2px 6px rgba(0,0,0,0.5), inset 0 1px rgba(255,255,255,0.3), inset 0 10px rgba(255,255,255,0.2), inset 0 10px 20px rgba(255,255,255,0.25), inset 0 -15px 30px rgba(0,0,0,0.3);
            -o-box-shadow: 0 2px 6px rgba(0,0,0,0.5), inset 0 1px rgba(255,255,255,0.3), inset 0 10px rgba(255,255,255,0.2), inset 0 10px 20px rgba(255,255,255,0.25), inset 0 -15px 30px rgba(0,0,0,0.3);
            -webkit-box-shadow: 0 2px 6px rgba(0,0,0,0.5), inset 0 1px rgba(255,255,255,0.3), inset 0 10px rgba(255,255,255,0.2), inset 0 10px 20px rgba(255,255,255,0.25), inset 0 -15px 30px rgba(0,0,0,0.3);
            -moz-box-shadow: 0 2px 6px rgba(0,0,0,0.5), inset 0 1px rgba(255,255,255,0.3), inset 0 10px rgba(255,255,255,0.2), inset 0 10px 20px rgba(255,255,255,0.25), inset 0 -15px 30px rgba(0,0,0,0.3);
        }
        .popupFooters
        {
            padding: 0;
            font-color: black;
            border-radius: 25px 25px 2px 25px;
            border-top-right-radius: 0px;
            border-top-left-radius: 0px;
            border-bottom-right-radius: 30px;
            border-bottom-left-radius: 30px;
            box-shadow: 0 2px 6px rgba(0,0,0,0.5), inset 0 1px rgba(255,255,255,0.3), inset 0 10px rgba(255,255,255,0.2), inset 0 10px 20px rgba(255,255,255,0.25), inset 0 -15px 30px rgba(0,0,0,0.3);
            -o-box-shadow: 0 2px 6px rgba(0,0,0,0.5), inset 0 1px rgba(255,255,255,0.3), inset 0 10px rgba(255,255,255,0.2), inset 0 10px 20px rgba(255,255,255,0.25), inset 0 -15px 30px rgba(0,0,0,0.3);
            -webkit-box-shadow: 0 2px 6px rgba(0,0,0,0.5), inset 0 1px rgba(255,255,255,0.3), inset 0 10px rgba(255,255,255,0.2), inset 0 10px 20px rgba(255,255,255,0.25), inset 0 -15px 30px rgba(0,0,0,0.3);
            -moz-box-shadow: 0 2px 6px rgba(0,0,0,0.5), inset 0 1px rgba(255,255,255,0.3), inset 0 10px rgba(255,255,255,0.2), inset 0 10px 20px rgba(255,255,255,0.25), inset 0 -15px 30px rgba(0,0,0,0.3);
        }
    </style>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMainContent" runat="server">
    <ajax:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </ajax:ToolkitScriptManager>
    <div>
        <%--<div class="popup1" 
            style="width:40.2%; padding-left:25%; height: 49px; margin-left: 26px; " >
               
                
     </div>--%>
        <%--<div id="contHeader" style="width:350px">
<h1 style="text-align:center" class="HeaderText">Branch Details</h1>
</div>
<br />
<br />--%>
        <div class="content">
            <h2 class="ribbon">
                Member Allocation to Group
            </h2>
            <%--<div style="padding-left:22%;">
 
  <div  style="width:735px; height:50px; text-align:center;">
         <h2 class="inset-text" style="padding-top:10px;" >
    
                        Member Allocation to Group
                </h2>
                
        </div>
 
 
 
    <div class="popup" 
         style="padding-left:3%;padding-bottom:3%;padding-right:3%;padding-top:3%; width:69%;">--%>
            <br />
            <br />
            <table style="width: 800px; margin: 0 auto;">
                <tr style="width: 70%; height: 50px">
                    <td style="width: 15%">
                        <asp:Label runat="server" Text="GroupID"></asp:Label>
                    </td>
                    <td style="width: 15%">
                        <asp:DropDownList CssClass="twitterStyleTextbox" Height="30px" ValidationGroup="add"
                            AutoPostBack="true" runat="server" ID="ddlGrpID" OnSelectedIndexChanged="ddlGrpID_SelectedIndexChanged"
                            TabIndex="15">
                        </asp:DropDownList>
                    </td>
                    <td style="width: 1%">
                        <asp:RequiredFieldValidator ValidationGroup="add" runat="server" ErrorMessage="*"
                            ControlToValidate="ddlGrpID" InitialValue="--select--"></asp:RequiredFieldValidator>
                    </td>
                    <td style="width: 15%">
                        <asp:Label ID="Label6" runat="server" Text="Nominee Address"></asp:Label>
                    </td>
                    <td style="width: 15%">
                        <asp:TextBox Width="150px" runat="server" CssClass="twitterStyleTextbox" ID="txtNomiaddr"
                            Height="66px" ValidationGroup="add" TextMode="MultiLine" TabIndex="21"></asp:TextBox>
                    </td>
                    <td style="width: 1%">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="*"
                            ValidationGroup="add" ControlToValidate="txtNomiaddr"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr style="width: 70%; height: 50px">
                    <td style="width: 15%">
                        <asp:Label runat="server" Text="Groupmember ID"></asp:Label>
                    </td>
                    <td style="width: 15%">
                        <asp:DropDownList CssClass="twitterStyleTextbox" Height="30px" ID="ddlgrpmemid" ValidationGroup="add"
                            AutoPostBack="true" runat="server" MaxLength="5" TabIndex="16">
                        </asp:DropDownList>
                    </td>
                    <td style="width: 1%">
                        <asp:RequiredFieldValidator ValidationGroup="add" ID="RequiredFieldValidator2" runat="server"
                            ErrorMessage="*" ControlToValidate="ddlgrpmemid" InitialValue="--select--"></asp:RequiredFieldValidator>
                    </td>
                    <td style="width: 15%">
                        <asp:Label runat="server" Text="Nominee Age"></asp:Label>
                    </td>
                    <td style="width: 15%">
                        <asp:TextBox ID="txtnomineeAge" runat="server" CssClass="twitterStyleTextbox" TabIndex="22"></asp:TextBox>
                    </td>
                    <td style="width: 1%">
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" Display="Static"
                            ControlToValidate="txtnomineeAge" ErrorMessage="Invalid Age" ValidationExpression="\d+"
                            runat="server"></asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr style="width: 70%; height: 50px">
                    <td style="width: 15%">
                        <asp:Label ID="Label1" ValidationGroup="add" runat="server" Text="Member Name"></asp:Label>
                    </td>
                    <td style="width: 15%">
                        <asp:DropDownList CssClass="twitterStyleTextbox" Height="30px" runat="server" ID="ddlMembName"
                            AutoPostBack="true" OnSelectedIndexChanged="ddlMembName_SelectedIndexChanged"
                            CausesValidation="True" TabIndex="17">
                            <asp:ListItem Text="--select--"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td style="width: 1%">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="*"
                            ValidationGroup="add" ControlToValidate="ddlMembName" InitialValue="--select--"></asp:RequiredFieldValidator>
                    </td>
                    <td style="width: 15%">
                        <asp:Label runat="server" Text="Relation" Width="150px"></asp:Label>
                    </td>
                    <td style="width: 15%">
                        <asp:TextBox runat="server" CssClass="twitterStyleTextbox" ID="txtNominrela" TabIndex="26"></asp:TextBox>
                    </td>
                    <td style="width: 1%">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ErrorMessage="*"
                            ValidationGroup="add" ControlToValidate="txtNominrela"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr style="width: 70%; height: 50px">
                    <td style="width: 15%">
                        <asp:Label ID="Label2" runat="server" Text="Member ID"></asp:Label>
                    </td>
                    <td style="width: 15%">
                        <asp:TextBox ID="txtMembID" runat="server" CssClass="twitterStyleTextbox" ValidationGroup="add"
                            TabIndex="18"></asp:TextBox>
                    </td>
                    <td style="width: 1%">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="*"
                            ValidationGroup="add" ControlToValidate="txtMembID"></asp:RequiredFieldValidator>
                    </td>
                    <td style="width: 15%">
                        <asp:Label runat="server" Text="Nominee Tel-Ph.No"></asp:Label>
                    </td>
                    <td style="width: 15%">
                        <asp:TextBox ID="txtnomiTele" runat="server" CssClass="twitterStyleTextbox" ValidationGroup="add"
                            TabIndex="24"></asp:TextBox>
                    </td>
                    <td style="width: 15%">
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" Display="Static"
                            ControlToValidate="txtnomiTele" ErrorMessage="Invalid No." ValidationExpression="\d+"
                            runat="server"></asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr style="width: 70%; height: 50px">
                    <td style="width: 15%">
                        <asp:Label ID="Label4" runat="server" Text="Member Address" Width="150px"></asp:Label>
                    </td>
                    <td style="width: 15%">
                        <asp:TextBox Width="150px" ValidationGroup="add" runat="server" CssClass="twitterStyleTextbox"
                            ID="txtMemaddr" Height="66px" TextMode="MultiLine" TabIndex="19"></asp:TextBox>
                    </td>
                    <td style="width: 1%">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*"
                            ValidationGroup="add" ControlToValidate="txtMemaddr"></asp:RequiredFieldValidator>
                    </td>
                    <td style="width: 15%">
                        <asp:Label runat="server" Text="Nominee Mob.No"></asp:Label>
                    </td>
                    <td style="width: 15%">
                        <asp:TextBox ID="txtNomimobno" runat="server" CssClass="twitterStyleTextbox" ValidationGroup="add"
                            TabIndex="26"></asp:TextBox>
                    </td>
                    <td style="width: 1%">
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" Display="Static"
                            ControlToValidate="txtNomimobno" ErrorMessage="Invalid No." ValidationExpression="^([7-9]{1})([0-9]{9})$"
                            runat="server"></asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr style="width: 70%; height: 50px">
                    <td style="width: 15%">
                        <asp:Label ID="Label5" runat="server" Text="Nominee Name"></asp:Label>
                    </td>
                    <td style="width: 15%">
                        <asp:TextBox runat="server" ValidationGroup="add" CssClass="twitterStyleTextbox"
                            ID="txtNominee" TabIndex="20"></asp:TextBox>
                    </td>
                    <td style="width: 1%">
                        <asp:RequiredFieldValidator ValidationGroup="add" ID="RequiredFieldValidator3" runat="server"
                            ErrorMessage="*" ControlToValidate="txtNominee"></asp:RequiredFieldValidator>
                    </td>
                    <td style="width: 15%">
                        <asp:Label ID="Label3" runat="server" Text="NoCard"></asp:Label>
                    </td>
                    <td style="width: 15%">
                        <asp:DropDownList CssClass="twitterStyleTextbox" Height="30px" ID="ddlNocard" runat="server"
                            ValidationGroup="add" TabIndex="27">
                        </asp:DropDownList>
                    </td>
                    <td style="width: 1%">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="*"
                            ValidationGroup="add" ControlToValidate="ddlNocard" InitialValue="--select--"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr style="width: 70%; height: 50px">
                    <td style="width: 15%">
                        <asp:Label ID="lblEstCallNoOfAuction" runat="server" Text="Est. Call No Of Auction"></asp:Label>
                    </td>
                    <td style="width: 15%">
                        <asp:TextBox runat="server" ValidationGroup="add" CssClass="twitterStyleTextbox"
                            ID="txtEstCallAuction" TabIndex="27"></asp:TextBox>
                    </td>
                    <td style="width: 1%">
                        <asp:RequiredFieldValidator ValidationGroup="add" ID="RequiredFieldValidator8" runat="server"
                            ErrorMessage="*" ControlToValidate="txtEstCallAuction"></asp:RequiredFieldValidator>
                    </td>
                    <td style="width: 15%">
                        <asp:Label ID="lblSuretyDoc" runat="server" Text="Est. Surety Document"></asp:Label>
                    </td>
                    <td style="width: 15%">
                        <asp:TextBox runat="server" ValidationGroup="add" CssClass="twitterStyleTextbox"
                            ID="txtSuretyDocument" TabIndex="28"></asp:TextBox>
                    </td>
                    <td style="width: 1%">
                        <asp:RequiredFieldValidator ValidationGroup="add" ID="RequiredFieldValidator10" runat="server"
                            ErrorMessage="*" ControlToValidate="txtSuretyDocument"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr style="width: 70%; height: 50px">
                    <td>
                        <asp:Label runat="server" ID="lblMoneyCollector" Text="Money Collector : "></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlMoneyCollector" runat="server" Height="36px" CssClass="twitterStyleTextbox"
                            TabIndex="30">
                        </asp:DropDownList>
                    </td>
                    <td style="width: 1%">
                        <asp:RequiredFieldValidator ValidationGroup="add" ID="RequiredFieldValidator11" runat="server"
                            ErrorMessage="*" ControlToValidate="ddlMoneyCollector" InitialValue="--select--"></asp:RequiredFieldValidator>
                    </td>
                </tr>
            </table>
        </div>
        <div class="footerR">
            <asp:Button ID="AddChit" ValidationGroup="add" runat="server" CssClass="GreenyPushButton"
                Text="Add Chit" OnClick="AddChit_Click" TabIndex="27" />
            <asp:Button ID="btnCancel" runat="server" CssClass="GreenyPushButton" Text="Cancel"
                OnClick="btnCancel_Click" TabIndex="31" />
        </div>
    </div>
    <a href="#" id="lk" runat="server"></a>
    <asp:ModalPopupExtender ID="ModalPopup" runat="server" BackgroundCssClass="modalBackground"
        TargetControlID="lk" PopupControlID="pandupName">
    </asp:ModalPopupExtender>
    <asp:Panel Visible="false" CssClass="raised" ID="pandupName" runat="server" Style="min-width: 200px;
        max-width: 900px; max-height: 500px; min-height: 100px">
        <div class="stitched">
            <div style="top: 0px; height: 50px; text-align: center; padding: auto 0; width: 100%"
                class="boxheader">
                <asp:Label ID="lblHeading" CssClass="inset-text" runat="server" Text="Choose Member"> </asp:Label>
            </div>
            <br />
            <asp:GridView ID="GridView1" DataKeyNames="MemberID" Visible="false" runat="server"
                ShowFooter="True" AutoGenerateColumns="False" Width="100%">
                <Columns>
                    <asp:TemplateField HeaderText="MemberID">
                        <ItemTemplate>
                            <asp:Label ID="lblMemberID" runat="server" ReadOnly="true" Text='<%#Eval("MemberID")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="MemberName">
                        <ItemTemplate>
                            <asp:TextBox ID="txtMemName" runat="server" ReadOnly="true" Text='<%#Eval("CustomerName")%>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="MemberAddress">
                        <ItemTemplate>
                            <asp:Label ID="lblMemAddr" runat="server" ReadOnly="true" TextMode="MultiLine" Text='<%#Eval("AddressForCommunication")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Button ID="btnAdd" OnClick="btnAdd_click" CssClass="GreenyPushButton" CommandName="Add"
                                runat="server" Text="Add" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                <EditRowStyle BackColor="#999999" />
                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            </asp:GridView>
            <div class="boxheader" style="width: 100%; bottom: 0px; height: 50px;">
                <p style="text-align: center">
                    <asp:Button ID="Btncan" runat="server" CssClass="GreenyPushButton" Visible="false"
                        Text="Cancel" OnClick="Btncan_Click" /></p>
            </div>
        </div>
    </asp:Panel>
    <a href="#" id="A1" runat="server"></a>
    <%-- <asp:ModalPopupExtender ID="ModalPopupNominee" runat="server" BackgroundCssClass="modalBackground" TargetControlID="A1" PopupControlID="PanGrid"></asp:ModalPopupExtender>  --%>
    <asp:Panel Visible="false" CssClass="raised" ID="PanGrid" runat="server" Style="min-width: 200px;
        max-width: 900px; max-height: 500px; min-height: 100px">
        <div class="stitched">
            <div style="top: 0px; height: 50px; text-align: center; padding: auto 0; width: 100%"
                class="boxheader">
                <asp:Label ID="Label7" CssClass="inset-text" runat="server" Text="Choose Nominee"> </asp:Label>
            </div>
            <asp:GridView ID="GridNominee" CssClass="popup" DataKeyNames="MemberID" Visible="false"
                runat="server" ShowFooter="True" AutoGenerateColumns="False" CellPadding="4"
                ForeColor="#333333" GridLines="None" Width="450px">
                <Columns>
                    <asp:TemplateField HeaderText="MemberID">
                        <ItemTemplate>
                            <asp:Label ID="lblMemberID" runat="server" Text='<%#Eval("MemberID")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="NomineeName">
                        <ItemTemplate>
                            <asp:Label ID="lblNomiName" runat="server" Text='<%#Eval("NomineeName")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="NomineeAge">
                        <ItemTemplate>
                            <asp:Label ID="lblNominage" runat="server" Text='<%#Eval("NomineeAge")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Relation">
                        <ItemTemplate>
                            <asp:Label ID="lblRelation" runat="server" Text='<%#Eval("Relation")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="NomineeAddress">
                        <ItemTemplate>
                            <asp:Label ID="lblNominAddr" runat="server" Text='<%#Eval("NomineeAddress")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Tel-PhNo">
                        <ItemTemplate>
                            <asp:Label ID="lblNomiTele" runat="server" Text='<%#Eval("NomineeTelephoneNo")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="NomineeMobile">
                        <ItemTemplate>
                            <asp:Label ID="lblNomiMob" runat="server" Text='<%#Eval("NomineeMobileNo")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Button ID="btnNomineeAdd" OnClick="btnNomineeAdd_Click" CssClass="GreenyPushButton"
                                CommandName="Add" runat="server" Text="Add" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                <EditRowStyle BackColor="#999999" />
                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            </asp:GridView>
            <div class="boxheader" style="width: 100%; bottom: 0px; height: 50px;">
                <p style="text-align: center; width: 75%">
                    <asp:Button ID="BtnNominCancel" OnClick="BtnNominCancel_Click" runat="server" CssClass="GreenyPushButton"
                        Visible="false" Text="Cancel" /></p>
            </div>
        </div>
    </asp:Panel>
</asp:Content>
