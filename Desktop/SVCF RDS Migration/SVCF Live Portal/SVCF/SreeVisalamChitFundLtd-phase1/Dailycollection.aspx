<%@ Page Title="SVCF Admin Panel" Language="C#" MasterPageFile="~/Branch.Master"
    AutoEventWireup="true" Culture="en-GB" CodeBehind="Dailycollection.aspx.cs" Inherits="SreeVisalamChitFundLtd_phase1.Dailycollection" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
  
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMainContent" runat="server">

<div class="row">
 <div class="twelve columns" style="margin-top: -0.5em; margin-bottom: 0.6em;">
   <div class="box_c">
    <div class="box_c_content">
        <div>
            <table>
            <tr>
                <td> 
                <asp:Label ID="lblmc" Text="Moneycollector : " runat="server" ></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlmc" runat="server" CssClass="chzn-select"></asp:DropDownList>
                </td>
            </tr>
            <tr>
             <td>
                        <asp:Label ID="lbldate" runat="server" Text="Date : "></asp:Label>
                    </td>
                    <td>
                         
                    <asp:TextBox TabIndex="2" Width="100px" class="input-text maskdate" ID="txtdate" onchange="CheckDate();" 
                        placeholder="Date" runat="server"></asp:TextBox>
                    </td></tr>
            <tr>
                
                <td>
                    <asp:Button ID="btngo" Text="Go" runat="server" CssClass="GreenyPushButton" class="btn" OnClick="btngo_Click"/>

                </td>
            </tr>
                </table>
                <div>
            <asp:GridView ID="GV_MC" runat="server" AutoGenerateColumns="false" BorderStyle="Solid"
                                 CellSpacing="3" Font-Names="Verdana" ForeColor="#333333" GridLines="Both" Height="100px" Width="900px">
                                <Columns>
                                    <asp:BoundField HeaderText="S. No." DataField="slno" ItemStyle-Width="50px"
                                        HeaderStyle-BorderColor="Gray" HeaderStyle-BorderStyle="Solid" HeaderStyle-BorderWidth="2px"
                                       ItemStyle-BorderColor="Gray" ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px"/>

                                    <asp:BoundField HeaderText="Ticket No" DataField="GrpMemberID" ItemStyle-Width="50px" 
                                        HeaderStyle-BorderColor="Gray" HeaderStyle-BorderStyle="Solid" HeaderStyle-BorderWidth="2px"
                                       ItemStyle-BorderColor="Gray" ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px"/>

                                    <asp:BoundField HeaderText="Name" DataField="MemberName" ItemStyle-Width="150px" 
                                        HeaderStyle-BorderColor="Gray" HeaderStyle-BorderStyle="Solid" HeaderStyle-BorderWidth="2px"
                                       ItemStyle-BorderColor="Gray" ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px"/>                                   

                                    <asp:BoundField HeaderText="Amount" DataField="Amount" ItemStyle-Width="75px" 
                                        HeaderStyle-BorderColor="Gray" HeaderStyle-BorderStyle="Solid" HeaderStyle-BorderWidth="2px"
                                       ItemStyle-BorderColor="Gray" ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px"/>

                                    <asp:BoundField HeaderText="Collection Date" DataField="Date" ItemStyle-Width="120px" 
                                        HeaderStyle-BorderColor="Gray" HeaderStyle-BorderStyle="Solid" HeaderStyle-BorderWidth="2px"
                                       ItemStyle-BorderColor="Gray" ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px"/>

                                     <asp:BoundField HeaderText="Collectorname" DataField="Collectorname" ItemStyle-Width="120px" 
                                        HeaderStyle-BorderColor="Gray" HeaderStyle-BorderStyle="Solid" HeaderStyle-BorderWidth="2px"
                                       ItemStyle-BorderColor="Gray" ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px"/>

                                    <asp:BoundField HeaderText="Cash in Hand" DataField="" ItemStyle-Width="120px" 
                                        HeaderStyle-BorderColor="Gray" HeaderStyle-BorderStyle="Solid" HeaderStyle-BorderWidth="2px"
                                       ItemStyle-BorderColor="Gray" ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px"/>
                       
                                    </Columns>
                            </asp:GridView> 
           </div>
        </div>
        </div>
       </div>
     </div>
    </div>
     <script type="text/javascript">

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
    <script type="text/javascript">
        function CheckDate() {
            var inputDate = $('#<%=txtdate.ClientID %>').val();
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
                   document.getElementById('<%=txtdate.ClientID %>').value = "";
                   alert('Incorrect Date Format..');
               }
           }
           else {
               document.getElementById('<%=txtdate.ClientID %>').value = "";
               alert('Incorrect Date Format..');
           }
        }
    </script>
    </asp:Content>