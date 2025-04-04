<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm6.aspx.cs" Inherits="SreeVisalamChitFundLtd_phase1.WebForm6" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
        </div>
        <asp:TextBox ID="TextBox1" runat="server" OnTextChanged="TextBox1_TextChanged" Width="552px"></asp:TextBox>
        <br />
        <br />
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Encrypt" />
        <asp:Button ID="Button3" runat="server" OnClick="Button3_Click" Text="Decrypt" />
        <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="Clear" />
        <br />
        <br />
        <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
        <br />
        <br />
        <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>
        <br />
        <br />
        <br />
        <br />
        <asp:Button ID="Button4" runat="server" OnClick="Button4_Click" Text="Sum" />
        <asp:Label ID="Label3" runat="server" Text="Label"></asp:Label>
        <asp:Button ID="Button5" runat="server" OnClick="Button5_Click" Text="Encryptmysql" />
        <asp:Button ID="Button6" runat="server" OnClick="Button6_Click" Text="Decryptymysql" />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
    </form>
</body>
</html>
