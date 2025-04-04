<%@ Page Title="" Language="C#" MasterPageFile="~/Branch.Master" AutoEventWireup="true" CodeBehind="FrmYearEndingBooklet.aspx.cs" Inherits="SreeVisalamChitFundLtd_phase1.FrmYearEndingBooklet" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMainContent" runat="server">
    <div class="twelve columns" style="margin-top: -0.5em; margin-bottom: 0.6em;">
        <div class="box_c">
            <div class="box_c_content">
                <div>
                    <table>
                        <tr>
                            <td>
                                <label>Select From Date</label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtFromDate" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>Select To Date</label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtToDate" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Button ID="BtnView" runat="server" Text="Go!"
                                    CssClass="GreenyPushButton active" OnClick="BtnView_Click"  />
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
