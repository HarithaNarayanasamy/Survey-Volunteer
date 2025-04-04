<%@ Page Title="" Language="C#" MasterPageFile="~/Branch.Master" AutoEventWireup="true" CodeBehind="SubscribersPassBook.aspx.cs" Inherits="SreeVisalamChitFundLtd_phase1.SubscribersPassBook" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMainContent" runat="server">
    <div>
      <div>
          <table>
              <tr>
                  <td>
                      CHIT AGREEMENT No.
                  </td>
                  <td>
                      <asp:Label ID="lblChitAgNo" runat="server" Text=""></asp:Label>
                  </td>
                  <td></td><td></td>
                  <td>
                     Date
                  </td>
                  <td>
                      <asp:Label ID="lblChitdt" runat="server" Text=""></asp:Label>
                  </td>
                </tr>
                <tr><td></td></tr>
                 <tr>
                   <td>
                     Date of Commencement
                  </td>                
                  <td>
                      <asp:Label ID="lblChitCommence" runat="server" Text=""></asp:Label>
                  </td>
                     <td></td><td></td>
                  <td>
                    Total Chit Value Rs.
                  </td>
                  <td>
                      <asp:Label ID="lblChitTotVal" runat="server" Text=""></asp:Label>
                  </td>
                 </tr>
                <tr><td></td></tr>
                 <tr>
                  <td>
                    Subscription Amount Rs.
                  </td>
                  <td>
                      <asp:Label ID="lblChitSubscAmnt" runat="server" Text=""></asp:Label>
                  </td>
                     <td></td><td></td>
                  <td>
                    No. of installments 
                  </td>
                  <td>
                      <asp:Label ID="lblChitNoOfInst" runat="server" Text=""></asp:Label>  Monthly Instaments
                  </td>
                 </tr>
                <tr><td></td></tr>
                 <tr>
                  <td>
                     Date Of Prizing
                  </td>
                  <td>
                      <asp:Label ID="lblChitPriceDt" runat="server" Text=""></asp:Label>
                  </td>
                     <td></td><td></td>
                  <td>
                     Prized Amount 
                  </td>
                  <td>
                      <asp:Label ID="lblChitPrizedAmnt" runat="server" Text=""></asp:Label>
                  </td>
                </tr>
                <tr><td></td></tr>
                <tr>
                  <td>
                     Installment No.
                  </td>
                  <td>
                      <asp:Label ID="lblChitInstNo" runat="server" Text=""></asp:Label>
                  </td>
                    <td></td><td></td>
                  <td>
                     Loan No.
                  </td>
                  <td>
                      <asp:Label ID="lblChitLoanNo" runat="server" Text=""></asp:Label>
                  </td>
                </tr>
                <tr><td></td></tr>
                <tr>
                  <td>
                     Date Of Loan
                  </td>
                  <td>
                      <asp:Label ID="lblChitLoanDt" runat="server" Text=""></asp:Label>
                  </td>
                    <td></td><td></td>
                   <td>
                     Loan Amount
                  </td>
                  <td>
                      <asp:Label ID="lblChitLoanAmnt" runat="server" Text=""></asp:Label>
                  </td>
              </tr>
          </table>
      </div>
      <div>
          <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false">
              <Columns>
                  <asp:BoundField HeaderText="Date" DataField="Date" />
                  <asp:BoundField HeaderText="Cash or Cheque" DataField="CashorCheq" />
                  <asp:BoundField HeaderText="Receipt No." DataField="ReceiptNo" />
                  <asp:BoundField HeaderText="Call No." DataField="CallNo" />
                  <asp:BoundField HeaderText="Cr or Dr" DataField="CrorDeb" />
                  <asp:BoundField HeaderText="Received or Paid Amount" DataField="ReceivedorPaid" />
                  <asp:BoundField HeaderText="Kasar" DataField="Kasar" />
                  <asp:BoundField HeaderText="Total Cr. or Db." DataField="TotCrorDr" />
                  <asp:BoundField HeaderText="Total Balance" DataField="Totbal" />
              </Columns>
         <%--Date, Cash or Cheq,Receipt No, call No,Cr.orDr.,Received or Paid Amount Rs. Ps., Kasar Rs. Ps, Total Cr. or Dr., Total balance Rs. Ps.--%>
              
          </asp:GridView>
      </div>
    </div>
</asp:Content>
