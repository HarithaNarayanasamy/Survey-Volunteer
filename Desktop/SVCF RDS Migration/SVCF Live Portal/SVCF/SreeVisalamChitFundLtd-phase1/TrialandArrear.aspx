<%@ Page Culture="en-GB" Language="C#" MasterPageFile="~/Branch.Master" AutoEventWireup="true"
    CodeBehind="TrialAndArrear.aspx.cs" EnableEventValidation="false" Inherits="SreeVisalamChitFundLtd_phase1.TrialAndArrear"
    Title="SVCF Admin Panel" %>

<asp:Content ID="Content2" ContentPlaceHolderID="cphHead" runat="server">
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
<asp:Content ID="Content3" ContentPlaceHolderID="cphMainContent" runat="server">
    <ajax:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </ajax:ToolkitScriptManager>
    
    <div class="container">
    <div class="row">
        <div class="twelve columns">
            <div class="box_c">
                <div class="box_c_heading cf noprint">
                    <p class="sepV_a">
                        Trial And Arrear</p>
                </div>
                <div class="box_c_content">
                    <div class="row">
                        <div class="noprint" style="margin-top: -0.5em; margin-bottom: 0.6em;">
                            <asp:Panel ID="Panel1" runat="server" DefaultButton="BtnStatisticsGo">
                                <div style="display: table-cell; padding-right: 5px !important;">
                                    <asp:Label ID="Label1" runat="server" Text="Select Chit Group"></asp:Label>
                                </div>
                                <div style="display: table-cell; padding-right: 5px !important;">
                                    <asp:DropDownList ID="ddlChit" Width="150px" Class="chzn-select" runat="server">
                                    </asp:DropDownList>
                                </div>
                                <div style="display: table-cell; padding-right: 5px !important;">
                                    <asp:Label ID="Label2" runat="server" Text="Date : "></asp:Label>
                                </div>
                                <div style="display: table-cell; padding-right: 5px;">
                                    <asp:TextBox TabIndex="1" Width="100px" class="input-text maskdate" ID="txtFromDate"
                                        runat="server" placeholder="From Date">
                                    </asp:TextBox>
                                    <asp:RequiredFieldValidator ValidationGroup="twelvehead" Display="Dynamic" ID="RequiredFieldValidator3"
                                        runat="server" ControlToValidate="txtFromDate" ErrorMessage=""></asp:RequiredFieldValidator>
                                    <asp:CompareValidator ID="CompareValidator10" ValidationGroup="twelvehead" runat="server"
                                        Display="Dynamic" ErrorMessage="" ControlToValidate="txtFromDate" Operator="DataTypeCheck"
                                        Type="Date"></asp:CompareValidator>
                                </div>
                                <div style="display: table-cell; padding-right: 5px; vertical-align: top;">
                                    <asp:Button ValidationGroup="twelvehead" TabIndex="3" ID="BtnStatisticsGo" CssClass="GreenyPushButton"
                                        runat="server" class="btn" OnClick="BtnStatisticsGo_Click" Text="Go!"></asp:Button>
                                </div>
                                <div style="display: table-cell; vertical-align: top; float: right; padding-right: 5px;
                                    text-align: right; margin-top: -35px;display:none">

                                    <a class="noprint" style="cursor: hand; cursor: pointer;" id="btnExport" >
                                        <img alt="Print" class="noprint" src="Styles/Img/pf-button-both.gif" style="border: none;
                                            cursor: hand; cursor: pointer;" />
                                    </a>                                   
                                </div>
                            </asp:Panel>
                        </div>                   
                    </div>
                <div style="display: table-cell; vertical-align: top; float: right; padding-right: 12px;
                          text-align: right; margin-top: -35px;">
                    <asp:ImageButton ID="imgpdf" runat="server" OnClick="imgpdf_Click" ImageUrl="Styles/Image/pdfexp.png"
                                         Height="33px" Width="34px"  />
                    </div>
               <asp:Label ID="Hiddentcap" Visible="false"  runat="server"></asp:Label> 
         
                     <asp:Panel runat="server" ID="PrintPanel1">
            <table style="width: 210px">
        <tr>
            <td style="background-color:#FDF5E6; border: 1px solid black" align="center">
                <asp:Label ID="lblCaption" runat="server" Style="font-weight: bold;
                    color:#800000;"></asp:Label>
            </td>
        </tr>
    </table>
                    <asp:GridView ID="gridTA" runat="server" BorderStyle="None" CssClass="Grid" HeaderStyle-Font-Bold="true" Font-Names="Verdana" ForeColor="Black" GridLines="Both" CellSpacing="2"                                         
                        AutoGenerateColumns="False" DataKeyNames="MemberId" PageSize="50" OnRowDataBound="gridTA_RowDataBound" ShowFooter="True" BackColor="White" BorderColor="Red" BorderWidth="1px" CellPadding="2" Width="788px" Font-Size="10.2pt">
                        <Columns>                       
                           <asp:TemplateField HeaderText="SNo." ItemStyle-HorizontalAlign="Left" ItemStyle-Width="2px">
                                 <ItemTemplate>
                                   <asp:Label ID="lblchit" runat="server" Text='<%#Eval("ChitNo1")%>'></asp:Label>
                                 </ItemTemplate>
                                 
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Member Name" ItemStyle-Width="9px">
                                 <ItemTemplate>
                                     <asp:Label ID="lblmember" runat="server" Text='<%#Eval("MemberName")%>'></asp:Label>
                                 </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lbltexttotal" runat="server" Text="Total"></asp:Label>
                                </FooterTemplate>
                            </asp:TemplateField>

                             <%--sivanesan  added hdden memberid     Visible="false"  --%>

                            <asp:TemplateField HeaderText="Member Id" ItemStyle-Width="9px"  Visible="false">
                                 <ItemTemplate>
                                     <asp:Label ID="memberid" runat="server" Text='<%#Eval("MemberId")%>'></asp:Label>
                                 </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="memberid" runat="server" Text="Total"></asp:Label>
                                </FooterTemplate>
                            </asp:TemplateField>

                             <asp:TemplateField HeaderText="Credit" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="5px">
                                 <ItemTemplate>
                                     <asp:Label ID="lblcredit" runat="server" Text='<%#Eval("Credit")%>'></asp:Label>
                                 </ItemTemplate>
                                  <FooterTemplate>
                                    <asp:Label ID="totalcredit" runat="server"></asp:Label>
                                </FooterTemplate>
                             </asp:TemplateField>

                         
                            <asp:TemplateField HeaderText="Debit" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="5px">
                                 <ItemTemplate>
                                     <asp:Label ID="lbldebit" runat="server" Text='<%#Eval("Debit")%>' BackColor='<%# GetDocumentColor(Eval("MemberId"),Eval("Debit")) %>'></asp:Label></ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="totaldebit" runat="server"> </asp:Label>
                               </FooterTemplate>
                            </asp:TemplateField>

                             <asp:TemplateField HeaderText="Ex Remit." ItemStyle-HorizontalAlign="Right" ItemStyle-Width="5px">
                                 <ItemTemplate>
                                     <asp:Label ID="lblexremit" runat="server" Text='<%#Eval("ExcessRemittance")%>'></asp:Label>
                                 </ItemTemplate>
                               <FooterTemplate >
                                    <asp:Label ID="totalexcessremittance" runat="server"></asp:Label>
                                </FooterTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="N-P.Arrear" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="5px">
                                <ItemTemplate>
                                    <asp:Label ID="lblnparrear" runat="server" Text='<%#Eval("NPArrier")%>'></asp:Label>
                                </ItemTemplate>
                                 <FooterTemplate>
                                    <asp:Label ID="totalnparrier" runat="server"></asp:Label>
                                </FooterTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="P.Arrear" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="5px">
                                <ItemTemplate>
                                    <asp:Label ID="lblparrear" runat="server" Text='<%#Eval("PArrier")%>'></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="totalpaarrier" runat="server"></asp:Label>
                                </FooterTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="Branches" ItemStyle-Width="6px">
                                <ItemTemplate>
                                    <asp:Label ID="lblbrches" runat="server" Text='<%#Eval("Branches")%>'></asp:Label>
                                </ItemTemplate>
                             </asp:TemplateField>  
                            <asp:TemplateField HeaderText="Mobile No" ItemStyle-Width="5px">
                                <ItemTemplate>
                                    <asp:Label ID="lblmobile" runat="server" Text='<%#Eval("MobileNumber")%>'></asp:Label>
                                </ItemTemplate>
                             </asp:TemplateField>                             
                        </Columns>                 
                        <FooterStyle BackColor="#CCCC99" ForeColor="Black" />

                         <HeaderStyle Font-Bold="True" BackColor="#333333" ForeColor="White"></HeaderStyle>
                        <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
                        <RowStyle BorderColor="#333300" BorderStyle="Solid" BorderWidth="1px" />
                        <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
                        <SortedAscendingCellStyle BackColor="#F7F7F7" />
                        <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
                        <SortedDescendingCellStyle BackColor="#E5E5E5" />
                        <SortedDescendingHeaderStyle BackColor="#242121" />
                        


                    </asp:GridView>
                                     
                    <asp:Panel ID="PanelSummary"  BorderStyle="Solid" runat="server" CssClass="panelstyle">
                        <%--<fieldset style="margin-top:0px;margin-left:20px;width:800px;color:#696969;">
                       <legend style="font-weight:200;color:#333333;"></legend>--%>
                        <table id="tble" runat="server">
                            <tr>
                               <td>
                                <asp:label ID="Label3" runat="server" Text="Non Prized Kasar " Font-Bold="true"> </asp:label>
                               </td>                             
                                <td class="paddingtd">
                                   <asp:label ID="lblsummary_NPkasar" runat="server"> </asp:label>
                                </td>
                            </tr> 
                            <tr>
                                <td>
                        <asp:label ID="Label4" runat="server" Text="Prized Kasar " Font-Bold="true"> </asp:label>
                                </td>                              
                                <td class="paddingtd">
                         <asp:label ID="lblsummary_PKasar" runat="server"> </asp:label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                      <asp:label ID="Label5" runat="server" Text="Grand Total " Font-Bold="true" > </asp:label>
                                </td>                              
                                <td class="paddingtd">
                      <asp:label ID="lblsummary_GrandTotal" runat="server"> </asp:label>
                                </td>
                            </tr>  
                            <tr>
                               <td>
                         <asp:label ID="lblsummary_DR" runat="server" Text="Balance DR " Font-Bold="true"  Visible="true"> </asp:label>
                               </td>                              
                                <td class="paddingtd">
                    <asp:label ID="lblsummary_BalanceDR" runat="server" > </asp:label>
                                </td>
                              </tr>       
                             <tr>
                               <td>
                         <asp:label ID="lblsummary_CR" runat="server" Text="Balance CR " Font-Bold="true" Visible="true"  > </asp:label>
                               </td>                              
                                <td class="paddingtd">
                    <asp:label ID="lblsummary_BalanceCR" runat="server" > </asp:label>
                                </td>
                              </tr>     
                            <tr>
                               <td>
                         <asp:label ID="Label8" runat="server" Text="Balance " Font-Bold="true" Visible="false"> </asp:label>
                               </td>                              
                                <td class="paddingtd">
                    <asp:label ID="BalanceDR" runat="server" visible="false"> </asp:label>
                                </td>
                              </tr>   
                                                        <tr>
                               <td>
                         <asp:label ID="Label6" runat="server" Text="Balance DR " Font-Bold="true" Visible="false"> </asp:label>
                               </td>                              
                                <td class="paddingtd">
                    <asp:label ID="Label7" runat="server" > </asp:label>
                                </td>
                              </tr> 
                           
                                                        <tr>
                               <td>
                         <asp:label ID="Label9" runat="server" Text="grandtotalbal " Font-Bold="true" Visible="false"> </asp:label>
                               </td>                              
                                <td class="paddingtd">
                    <asp:label ID="Ibl_grandtotalbal" runat="server" Visible="false"> </asp:label>
                                </td>
                              </tr> 
                                
                                                                                                     
                            </table>                                       
                           
                             <%--</fieldset>--%>
                    </asp:Panel>
                       
                </asp:Panel>               
<%--</asp:Panel>--%>

            </div>
        </div>
    </div>
   </div>
    </div>
    <script type="text/javascript">

        //function printDiv(divname) {
        //    var printContents = document.getElementById(divname).innerHTML;
        //    var originalContents = document.body.innerHTML;
         
        //    document.body.innerHTML =printContents;
           
        //    window.print();
        //    document.body.innerHTML = originalContents;

            //var panel = document.getElementById(divname);
            //var printWindow = window.open('', '', 'height=900,width=800');
            //printWindow.document.write(panel.innerHTML);
            
            //printWindow.print();
        //}

    </script>
    <script type="text/javascript">
        function htmlDecode(value) {
            var returnDecoadedValue = $('<div />').html(value).text();
            return returnDecodedValue;
        }
        $(document).ready(function () {
            $(".chzn-select").chosen({ search_contains: true });
            prth_mask_input.init();
        });
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $(".chzn-select").chosen({ search_contains: true });
        });

       <%-- $(document).ready(function () {
            
            $("#btnExport").click(function (e) {
                printDiv('<%=printdiv2.ClientID%>');
                e.preventDefault();
            });
        });--%>

        //function PrintDocument(printdiv) {
        //    var printContents = document.getElementById("printdiv2").innerHTML;
        //    var originalContents = document.body.innerHTML;
        //    document.body.innerHTML = printContents;
        //    window.print();
        //    document.body.innerHTML = originalContents;
        //    return true;
       // }
    </script>
    <style type="text/css">
        @media print
        {
            header
            {
                display: none;
            }
            body
            {
                font-size:20px;
            }
            .noprint
            {
                display: none;
            }
            div
            {
                border: none !important;
            }
           @page { size:portrait; }
        }
        .auto-style1 {

            height: 18px;
        }
    </style>

</asp:Content>
