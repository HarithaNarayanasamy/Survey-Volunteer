<%@ Page Language="C#" MasterPageFile="~/Branch.Master" AutoEventWireup="true" CodeBehind="AddressLable.aspx.cs"
    Inherits="SreeVisalamChitFundLtd_phase1.AddressLable" Title="AVCF Admin Panel" %>

<asp:Content ID="Head1" ContentPlaceHolderID="cphHead" runat="server">
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMainContent" runat="server">
    <div class="content">
        <div style="background: white; border-bottom: 10px solid white;">
            <div class="ribheader">
                Address Label
            </div>
        </div>
        <br />
        <asp:Table runat="server" ID="tbl1" Style="margin: 0 auto;">
            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label ID="lblChitNO" Text="Chit NO" runat="server"></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:DropDownList CssClass="twitterStyleTextbox" AutoPostBack="true" ID="ddlChitNo"
                        OnSelectedIndexChanged="ddlChitNo_SelectedIndexChanged" Height="28px" runat="server">
                    </asp:DropDownList>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:Button ID="btnLoad" OnClick="btnLoad_OnClick" Text="Load" CssClass="GreenyPushButton"
                        runat="server"></asp:Button>
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
        <div style="margin: 0px auto; display: table;">
            <asp:GridView ID="gvDetails" runat="server" AutoGenerateColumns="False" CellPadding="4"
                ForeColor="Black" Style="margin: 0px auto; display: table-cell;" DataKeyNames="GrpMemberID"
                BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px"
                ShowFooter="True" GridLines="None">
                <RowStyle BackColor="#F7F7DE" />
                <Columns>
                    <asp:TemplateField HeaderText="CheckAll">
                        <HeaderTemplate>
                            <asp:CheckBox ID="chkSelectAll" runat="server" AutoPostBack="true" OnCheckedChanged="chkSelectAll_CheckedChanged" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="chkSelect" runat="server" AutoPostBack="true" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Customer Name">
                        <ItemTemplate>
                            <asp:Label ID="lblName" runat="server" Text='<%# Bind("MemberName") %>' ForeColor="Blue"
                                BorderStyle="none" BorderWidth="0px" ReadOnly="true">
                            </asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Address">
                        <ItemTemplate>
                            <asp:Label ID="lblAddressForCommunication" runat="server" Text='<%# Bind("MemberAddress") %>'
                                ForeColor="Blue" BorderStyle="none" ReadOnly="true">
                            </asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="No Card">
                        <ItemTemplate>
                            <asp:Label ID="lblNoCard" runat="server" Text='<%# Bind("NoCard") %>' ForeColor="Blue"
                                BorderStyle="none" ReadOnly="true">
                            </asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <FooterStyle BackColor="#CCCC99" />
                <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                <AlternatingRowStyle BackColor="White" />
            </asp:GridView>
        </div>
    </div>
    <p style="text-align: center">
        <asp:Button ID="btnGenerate" Text="Generate" CssClass="btn-style" runat="server"
            OnClick="btnGenerate_Click"></asp:Button></p>
</asp:Content>
