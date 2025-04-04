<%@ Page Title="" Language="C#" MasterPageFile="~/Branch.Master" AutoEventWireup="true" CodeBehind="MoneyCollectorArrear.aspx.cs" Inherits="SreeVisalamChitFundLtd_phase1.MoneyCollectorArrear" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <link href="~/Styles/gridadvice.css" rel="stylesheet" />
        <style type="text/css">
        .chzn-results
        {
            text-align: center;
        }
        /*#ctl00_cphMainContent_ddlChit_chzn .chzn-drop .chzn-search input[type="text"]
        {
            height: 16px;
        }*/
        /*.linecolor tr td
        {
            border-top: 1px solid Gray;
            border-bottom: 1px solid Gray;
        }*/

        .Grid, .Grid th, .Grid td {
            border: 1px solid #2F4F4F;
        }

        .panelstyle
        {
            margin-left:80px;
        }

        .paddingtd
        {
            padding-left:10px;
        }
    </style>
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
                                    <asp:Label ID="Label1" runat="server" Text="Select Money Collector Name">
                                    </asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlMname" runat="server" Width="230px" CssClass="chzn-select">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label7" runat="server" Text="To Date : "></asp:Label>
                                </td>
                                <td>

                                    <%--  <asp:TextBox TabIndex="2" Width="100px" class="input-text maskdate" ID="txtToDate" 
                                        placeholder="To Date" runat="server"></asp:TextBox>--%>
                                    <asp:TextBox TabIndex="2" Width="230px" class="input-text maskdate" ID="txtToDate"
                                        placeholder="To Date" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Button ID="BtnStatisticsGo" runat="server" Text="Go!" ValidationGroup="twelvehead"
                                        TabIndex="3" CssClass="GreenyPushButton" class="btn" OnClick="BtnStatisticsGo_Click" />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div style="display: table-cell; vertical-align: top; float: right; padding-right: 7px; text-align: right; margin-top: -35px;">
                        <asp:ImageButton ID="imgexport" runat="server" ImageUrl="~/Styles/Image/document_export.png" OnClick="imgexport_Click"
                            Height="33px" Width="30px" />
                        <asp:ImageButton ID="imgpdf" runat="server" OnClick="imgpdf_Click" ImageUrl="Styles/Image/pdfexp.png"
                            Height="33px" Width="30px" />
                       <%-- <asp:ImageButton ID="imgprint" runat="server" Height="33px" Width="34px" ImageUrl="~/Styles/Image/printer.png"
                            OnClick="imgprint_Click" />--%>
                    </div>
                    <p style="display:inline-block;align-content:center;"></p>
                    <asp:Panel runat="server" ID="Panel1">
                        <table style="width:100%;">
                            <tr>
                                <td style="background-color: white;" colspan="5">
                                    <asp:Label ID="lblCaption"  runat="server" Style="font-weight:100;text-align:center;padding-left:35%;display:inline-block;"></asp:Label>
                                </td>
                            </tr>
                        </table>

                        <%--<asp:GridView ID="GV_MCArrear" runat="server" OnSelectedIndexChanged="GV_MCArrear_SelectedIndexChanged" AutoGenerateColumns="False"
                            CssClass="mGrid" AlternatingRowStyle-CssClass="alt" OnRowDataBound="GV_MCArrear_RowDataBound">--%>
                      <asp:GridView ID="GV_MCArrear" runat="server" BorderStyle="None" CssClass="Grid" HeaderStyle-Font-Bold="true" Font-Names="Verdana" ForeColor="Black" GridLines="Both" CellSpacing="2"                                         
                        AutoGenerateColumns="False" PageSize="50" OnSelectedIndexChanged="GV_MCArrear_SelectedIndexChanged" OnRowDataBound="GV_MCArrear_RowDataBound" ShowFooter="True" BackColor="White" BorderColor="Red" BorderWidth="1px" CellPadding="2" Width="800px" Font-Size="10.2pt">

                          <%--  <Columns>
                                <asp:BoundField HeaderText="S.No." DataField="slno"/>                                 
                                <asp:BoundField HeaderText="Ticket Number" DataField="GrpMemberID" ItemStyle-Width="100px"/>
                                <asp:BoundField HeaderText="Name" DataField="MemberName" ItemStyle-Width="130px"/>
                                <asp:BoundField HeaderText="Call No" DataField="DrawNO" ItemStyle-Width="100px"/>
                                <asp:BoundField HeaderText="Prized Arrear" DataField="PArr" ItemStyle-HorizontalAlign="Right"/>                               
                                <asp:BoundField HeaderText="NonPrized Arrear " DataField="NPArr" ItemStyle-HorizontalAlign="Right"/>                      
                                <asp:BoundField HeaderText="Date of Last Realization" DataField="Date"/>
                                <asp:BoundField HeaderText="Last Amnt Collected" DataField="AmountCollected" ItemStyle-HorizontalAlign="Right"/>
                                <asp:BoundField HeaderText="Dt ARR Realized" DataField=""/>
                                <asp:BoundField HeaderText="ARR Amt Collected" DataField=""/>
                                <asp:BoundField HeaderText="Default Interest Collected" DataField=""/>
                                <asp:BoundField HeaderText="Report" DataField=""/>
                            </Columns>--%>
                          <Columns>                       
                           <asp:TemplateField HeaderText="SNo." ItemStyle-HorizontalAlign="Left" ItemStyle-Width="15px" ItemStyle-Wrap="false">
                                 <ItemTemplate>
                                   <asp:Label ID="lblsno" runat="server" Text='<%#Eval("slno")%>'></asp:Label>
                                 </ItemTemplate>
                                 
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Ticket Number" ItemStyle-Width="5px" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                     <asp:Label ID="lblTicketNumber" runat="server" Text='<%#Eval("GrpMemberID")%>'></asp:Label>
                                 </ItemTemplate>
                            </asp:TemplateField>

                             <asp:TemplateField HeaderText="Name" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="5px">
                                 <ItemTemplate>
                                     <asp:Label ID="lblMemberName" runat="server" Text='<%#Eval("MemberName")%>'></asp:Label>
                                 </ItemTemplate>
                                  
                             </asp:TemplateField>

                         
                            <asp:TemplateField HeaderText="Call No" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="20px" ItemStyle-Wrap="false">
                                 <ItemTemplate>
                                     <asp:Label ID="lblDrawNO" runat="server" Text='<%#Eval("DrawNO")%>'></asp:Label></ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="totaldebit" runat="server"> </asp:Label>
                               </FooterTemplate>
                            </asp:TemplateField>

                             <asp:TemplateField HeaderText="Prized Arrear" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="5px">
                                 <ItemTemplate>
                                     <asp:Label ID="lblPArr" runat="server" Text='<%#Eval("PArr")%>'></asp:Label>
                                 </ItemTemplate>
                              
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="NonPrized Arrear" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="5px">
                                <ItemTemplate>
                                    <asp:Label ID="lblNPArr" runat="server" Text='<%#Eval("NPArr")%>'></asp:Label>
                                </ItemTemplate>
                                
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Date of Last Realization" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="5px">
                                <ItemTemplate>
                                    <asp:Label ID="lblDate" runat="server" Text='<%#Eval("Date")%>'></asp:Label>
                                </ItemTemplate>
                                
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="Last Amnt Collected" ItemStyle-Width="6px" ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label ID="lblAmountCollected" runat="server" Text='<%#Eval("AmountCollected")%>'></asp:Label>
                                </ItemTemplate>
                             </asp:TemplateField>  
                            <asp:TemplateField HeaderText="Dt ARR Realized" ItemStyle-Width="5px">
                                <ItemTemplate>
                                    <asp:Label ID="lblDtARRRealized" runat="server" ></asp:Label>
                                </ItemTemplate>
                             </asp:TemplateField>   
                              <asp:TemplateField HeaderText="ARR Amt Collected" ItemStyle-Width="5px">
                                <ItemTemplate>
                                    <asp:Label ID="lblARRAmtCollected" runat="server" ></asp:Label>
                                </ItemTemplate>
                             </asp:TemplateField>   
                              <asp:TemplateField HeaderText="Default Interest Collected" ItemStyle-Width="5px">
                                <ItemTemplate>
                                    <asp:Label ID="lblDefaultInterestCollected" runat="server" ></asp:Label>
                                </ItemTemplate>
                             </asp:TemplateField> 
                              <asp:TemplateField HeaderText="Report" ItemStyle-Width="5px">
                                <ItemTemplate>
                                    <asp:Label ID="lblReport" runat="server" ></asp:Label>
                                </ItemTemplate>
                             </asp:TemplateField>                             
                        </Columns>                
                        </asp:GridView>


                    </asp:Panel>

                    <div class="hideSkiplink">
                        <input id="txtarrtotal" runat="server" readonly="readonly" type="text" class="txthtml" />
                        <label class="lblhtml">Arrear Total</label>
                    </div>
                    <div class="hideSkiplink">
                        <input id="txtcoltotal" runat="server" type="text" readonly="readonly" class="txthtml" />
                        <label class="lblhtml">Collected Total</label>
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
            var inputDate = $('#<%=txtToDate.ClientID %>').val();
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
                    document.getElementById('<%=txtToDate.ClientID %>').value = "";
                   alert('Incorrect Date Format..');
               }
           }
           else {
               document.getElementById('<%=txtToDate.ClientID %>').value = "";
               alert('Incorrect Date Format..');
           }
       }
    </script>

</asp:Content>
