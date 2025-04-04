<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FrmLedger.ascx.cs" Inherits="SreeVisalamChitFundLtd_phase1.UserControl.FrmLedger" %>
<link href="../Styles/Stage.css" rel="stylesheet" type="text/css" />
<script src="../Scripts/commonjs.js" type="text/javascript"></script>
<%--<script src="../Scripts/jquery-ui.min.js" type="text/javascript"></script>--%>

<link href="../Styles/ControlsCss.css" rel="stylesheet" type="text/css" />
<link href="../Styles/GridviewScroll.css" rel="stylesheet" type="text/css" />
<link href="../Styles/GVScrolll.css" rel="stylesheet" type="text/css" />
<link href="../Styles/MsgBox.css" rel="stylesheet" type="text/css" />
<link href="../Styles/PopUpMessageBox.css" rel="stylesheet" type="text/css" />
<link href="../Styles/Stage.css" rel="stylesheet" type="text/css" />
<link href="../Styles/tablecss.css" rel="stylesheet" type="text/css" />
<link href="../Styles/Validation.css" rel="stylesheet" type="text/css" />


<script type="text/javascript">

    function clearValidationErrors() {
        //Hide all validation errors
        if (window.Page_Validators)
            for (var vI = 0; vI < Page_Validators.length; vI++) {
                var vValidator = Page_Validators[vI];
                vValidator.isvalid = true;
                ValidatorUpdateDisplay(vValidator);
            }
        //Hide all validaiton summaries
        if (typeof (Page_ValidationSummaries) != "undefined") { //hide the validation summaries
            for (sums = 0; sums < Page_ValidationSummaries.length; sums++) {
                summary = Page_ValidationSummaries[sums];
                summary.style.display = "none";
            }
        }
    }
    
    </script>
  <%--  <ajax:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </ajax:ToolkitScriptManager>--%>
<style type="text/css">
        .chzn-results
        {
            text-align: center;
        }
        #ctl00_cphMainContent_ddlChit_chzn .chzn-drop .chzn-search input[type="text"]
        {
            height: 16px;
        }
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

<div class="bkgrnd row" style="padding:55px; margin-right: 0px;width:850px;">
<div class="twelve columns" style="margin-top: -0.5em; margin-bottom: 0.6em;">
 <div class="box_c">
  <div class="box_c_content">

  
  <div>
     <table>
       <tr>
         <td>
           <label>Select Group No.</label>
         </td>
         <td>
             <asp:DropDownList ID="DD_GP" runat="server"  CssClass="chzn-select" 
                 Width="200px" AutoPostBack="True" 
                 onselectedindexchanged="DD_GP_SelectedIndexChanged">
             </asp:DropDownList>
         </td>
       </tr>
       <tr>
         <td>
           <label>Member Details:</label>
         </td>
         <td>
             <asp:DropDownList ID="DD_GPMem" runat="server" CssClass="chzn-select" Width="200px">
             </asp:DropDownList>
         </td>
       </tr>
       <tr>
         <td colspan="2">
            <asp:Button ID="BtnView" runat="server" Text="Go!" 
                           CssClass="GreenyPushButton active" onclick="BtnView_Click" />
         </td>
       </tr>
     </table>
  </div> 
       <div style="display: table-cell; vertical-align: top; float: right; padding-right: 5px;
                                    text-align: right; margin-top: -35px;">
<%--                                    <a class="noprint" style="cursor: hand; cursor: pointer;" id="btnExport" onclick="Btndown_Click;">
                                        <img alt="Print" class="noprint" src="Styles/Img/downpdf.png" onclick="btn_click" style="border: none;
                                            cursor: hand; cursor: pointer; width:50px; height:30px"; />
                                    </a>    --%>
                <asp:ImageButton ID="imgpdf" runat="server" OnClick="imgpdf_Click" ImageUrl="~/Styles/Image/Images/downpdf.png"
                                         Height="30px" Width="50px" />                             
                                </div> 
  <div style="width:824px;">
      <div id="printdiv" class="printable" runat="server">
         <asp:Panel runat="server" ID="PrintPanel1">

    <%-- <asp:GridView ID="GD_Ledger" runat="server"  BorderStyle="None" CssClass="Grid" HeaderStyle-Font-Bold="true" Font-Names="Verdana" ForeColor="Black" GridLines="Both" CellSpacing="2"                                         
                        AutoGenerateColumns="False" PageSize="50" OnRowCreated="GD_Ledger_RowCreated"   OnRowDataBound="GD_Ledger_RowDataBound" ShowFooter="True" BackColor="White" BorderColor="Red" BorderWidth="1px" CellPadding="4" Width="788px" Font-Size="9.2pt">--%>
              <asp:GridView ID="GD_Ledger" runat="server"  BorderStyle="None" CssClass="Grid" HeaderStyle-Font-Bold="true" Font-Names="Verdana" ForeColor="Black" GridLines="Both" CellSpacing="2"                                         
                        AutoGenerateColumns="False" PageSize="50"    OnRowDataBound="GD_Ledger_RowDataBound" ShowFooter="True" BackColor="White" BorderColor="Red" BorderWidth="1px" CellPadding="4" Width="788px" Font-Size="9.2pt">
                        <Columns>                       
                           <asp:TemplateField HeaderText="Date" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="5px">
                                 <ItemTemplate>
                                   <asp:Label ID="lbldate" runat="server" Text='<%#Eval("Date")%>'></asp:Label>
                                 </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="On what account received or paid by the Foreman" ItemStyle-Width="10px">
                                 <ItemTemplate>
                                     <asp:Label ID="lblnarration" runat="server" Text='<%#Eval("Narration")%>'></asp:Label>
                                 </ItemTemplate>
                               
                            </asp:TemplateField>

                             <asp:TemplateField HeaderText="No.of inst" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="4px">
                                 <ItemTemplate>
                                     <asp:Label ID="lblnoof" runat="server" Text='<%#Eval("Noofinstallments")%>'></asp:Label>
                                 </ItemTemplate>
                                 
                             </asp:TemplateField>

                         
                            <asp:TemplateField HeaderText="Amount of Subscription of each instalment" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="4px">
                                 <ItemTemplate>
                                     <asp:Label ID="lblsubam" runat="server" Text='<%#Eval("Subscription")%>'></asp:Label></ItemTemplate>
                               
                            </asp:TemplateField>

                             <asp:TemplateField HeaderText="Dividend Due to the Subscribers for Each Instalment"  ItemStyle-Width="5px">
                                 <ItemTemplate>
                                     <asp:Label ID="lbldue" runat="server" Text='<%#Eval("KasarAmount")%>'></asp:Label>
                                 </ItemTemplate>
                              
                            </asp:TemplateField>
                            
        
                              <%--<asp:TemplateField>
                                  <HeaderTemplate>
                                      <table>
                                       <tr>
                                           <th>
                                               Amount Paid By Subscriber
                                           </th>
                                           </tr>
                                          <tr>
                                               <th colspan="3">Share Amount</th>
                                           
                                           <th colspan="2">Total</th>
                                          </tr>
                                          
                                           
                                       
                                         
                                      </table>
                                  </HeaderTemplate>
                                  <ItemTemplate>
                                      <table>
                                          <td  >
                                              <asp:Label ID="lblpaidamt" runat="server" Text='<%#Eval("ShareAmount")%>'></asp:Label>
                                          </td>
                                          <td>
                                               <asp:Label ID="lbltotal" runat="server" Text='<%#Eval("Total")%>'></asp:Label>
                                          </td>
                                      </table>
                                  </ItemTemplate>
                              </asp:TemplateField>
                   --%>
                                
                            
                            <asp:TemplateField HeaderText="Share Amount" ItemStyle-Width="4px">
                                <ItemTemplate>
                                    <asp:Label ID="lblpaidamt" runat="server" Text='<%#Eval("ShareAmount")%>'></asp:Label>
                                </ItemTemplate>
                                
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Total" ItemStyle-Width="4px">
                                <ItemTemplate>
                                    <asp:Label ID="lbltotal" runat="server" Text='<%#Eval("Total")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="Amnt rcvd back by Subscriber" ItemStyle-Width="5px">
                                <ItemTemplate>
                                    <asp:Label ID="lblpriamt" runat="server" Text='<%#Eval("PrizedAmount")%>'></asp:Label>
                                </ItemTemplate>
                             </asp:TemplateField>        
                                <asp:TemplateField HeaderText="Balance payable" ItemStyle-Width="5px">
                                <ItemTemplate>
                                    <asp:Label ID="lblbalance" runat="server" Text='<%#Eval("Balance")%>'></asp:Label>
                                </ItemTemplate>
                             </asp:TemplateField>                        
                        </Columns>   
          <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
          <SortedAscendingCellStyle BackColor="#FBFBF2" />
          <SortedAscendingHeaderStyle BackColor="#848384" />
          <SortedDescendingCellStyle BackColor="#EAEAD3" />
          <SortedDescendingHeaderStyle BackColor="#575357" />
      </asp:GridView>
                                    </asp:Panel>
          </div>
  </div>
  
  
</div>
</div>
</div>
</div>

<script type="text/javascript">
    $(document).ready(function () {
        $(".chzn-select").chosen({ search_contains: true });
        //            $(".sp_floats").spinner({
        //                decimals: 2,
        //                stepping: 0.50,
        //                min: 0.00
        //            });

        $(".sp_number").numeric({
            decimal: false,
            negative: false
        },
                function () {
                    alert("Positive integers only");
                    this.value = "";
                    this.focus();
                });
        $(".sp_float").numeric({
            negative: false
        },
                function () {
                    alert("Positive only");
                    this.value = "";
                    this.focus();
                });
        prth_mask_input.init();
    });

    var prm = Sys.WebForms.PageRequestManager.getInstance();
    prm.add_endRequest(function () {
        $(".chzn-select").chosen({ search_contains: true });
        //            $(".sp_floats").spinner({
        //                decimals: 2,
        //                stepping: 0.50,
        //                min: 0.00
        //            });
        $(".sp_number").numeric({
            decimal: false,
            negative: false
        },
                function () {
                    alert("Positive integers only");
                    this.value = "";
                    this.focus();
                });
        $(".sp_float").numeric({
            negative: false
        },
                function () {
                    alert("Positive only");
                    this.value = "";
                    this.focus();
                });
        prth_mask_input.init();
        prth_tips.init();
    });

    //function htmlDecode(value) {
    //    var returnDecoadedValue = $('<div />').html(value).text();
    //    return returnDecodedValue;
    //}
    //function PrintDocument() {
    //    var printContents = document.getElementById("printdiv").innerHTML;
    //    var originalContents = document.body.innerHTML;
    //    document.body.innerHTML = printContents;
    //    window.print();
    //    document.body.innerHTML = originalContents;
    //    return true;
    //}

    </script>
    <style type="text/css">
        @media print
        {
            header
            {
                display: none;
            }
            .noprint
            {
                display: none;
            }
            div
            {
                border: none !important;
            }
        }
        #printdiv {
            width: 821px;
        }
    </style>
   