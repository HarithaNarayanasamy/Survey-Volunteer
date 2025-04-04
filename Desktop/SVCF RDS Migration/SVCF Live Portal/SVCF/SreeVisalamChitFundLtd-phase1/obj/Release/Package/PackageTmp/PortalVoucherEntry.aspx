<%@ Page Title="" Language="C#" MasterPageFile="~/Branch.Master" AutoEventWireup="true" CodeBehind="PortalVoucherEntry.aspx.cs" Inherits="SreeVisalamChitFundLtd_phase1.PortalVoucherEntry" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMainContent" runat="server">
    <link href="Styles/tablecss.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        input[type="text"] {
            margin-bottom: 3px;
        }

        input[type="image"]:active {
            -moz-box-shadow: inset 0 0 10px rgba(0,0,0,.06);
            -webkit-box-shadow: inset 0 0 10px rgba(0,0,0,.06);
            box-shadow: inset 0 0 10px rgba(0,0,0,.06);
            z-index: 6;
            border: 2px solid #006469 !important;
        }

        .Trans div[id*="chzn"] {
            width: 100% !important;
        }

            .Trans div[id*="chzn"] span {
                width: 140px !important;
                overflow: hidden;
            }

        .Trans td {
            width: 14% !important;
            padding-left: 8px !important;
            padding-right: 8px !important;
        }

            .Trans td > input[type="text"] {
                width: 100% !important;
            }

            .Trans td div[class="ui-spinner"] > input[type="text"] {
                width: 100% !important;
            }

        .Trans select {
            width: 100% !important;
        }

        .hidable table {
            vertical-align: middle;
        }

        .hidable td {
            text-align: left;
            padding: 3px;
            margin: 3px;
        }

        .chzn-drop {
            border: 1px solid #aaa !important;
            padding-top: 5px !important;
        }

        div[id*="ddlToken_chzn"] .chzn-drop {
            width: 100% !important;
        }

        div[id*="ddlMisc"] .chzn-drop {
            width: 200% !important;
        }

        .btn-custom {
            background-color: #0488e8;
            color: #FFF;
            cursor: pointer;
            height: 30px;
            width: 100px;
        }

            .btn-custom:hover {
                background-color: #1f72ae;
                color: #FFF;
            }

            .btn-custom:active {
                background-color: #ff3b3b !important;
                color: #fff;
            }

            .btn-custom:focus {
                background-color: #ff3b3b !important;
                color: #fff;
            }

        .center {
            margin-left: auto;
            margin-right: auto;
        }
    </style>
    <ajax:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </ajax:ToolkitScriptManager>
    <div class="row">
        <asp:HiddenField ID="rcptfrmrange" runat="server" />
        <asp:HiddenField ID="rcpttorange" runat="server" />
        <div class="row">
            <div class="twelve columns">
                <div class="box_c">
                    <div class="box_c_heading  box_actions">
                        <p>
                            Cash Receipt Details / Cheque Receipt Details
                        </p>
                    </div>
                    <div class="box_c_content">
                        <div class="row">
                            <br />
                            <div>
                                <div>
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <ContentTemplate>
                                            <div>
                                                <div style="width: 100%; margin: 0 auto;">
                                                    <table width="80%">
                                                      
                                                        </tr>
                                                        <tr>

                                                            <td>
                                                                <asp:CheckBox runat="server" ID="CheckCash" TabIndex="4"
                                                                    Width="120" CssClass="twitterStyleTextbox sp_number" Text="Cash"
                                                                    OnCheckedChanged="CheckCash_click" AutoPostBack="true"/>
                                                            </td>
                                                            <td>
                                                                <asp:CheckBox runat="server" ID="CheckCheque" TabIndex="4"
                                                                    Width="120" CssClass="twitterStyleTextbox sp_number" Text="Cheque" ClientIDMode="Static"
                                                                     AutoPostBack="true" OnCheckedChanged="CheckCheque_CheckedChanged"/>

                                                            </td>
                                                           
                                                              <td>
                                                                <asp:Label ID="Label10" runat="server" Text="Date"></asp:Label>
                                                                <br />
                                                                <asp:TextBox runat="server" ID="txtDate"  Width="90" onchange="CheckDate();" ValidationGroup="a" runat="server"
                                                            TabIndex="6" CssClass="twitterStyleTextbox maskdate"></asp:TextBox>
                                                                 <asp:RequiredFieldValidator ForeColor="Red" ID="RequiredFieldValidator6" ErrorMessage="Required!!!"
                                                            ControlToValidate="txtDate" ValidationGroup="a" Display="Dynamic" runat="server"></asp:RequiredFieldValidator>
                                                        <asp:CompareValidator ForeColor="Red" Type="Date" ID="CompareValidator11" ValidationGroup="a"
                                                            ControlToValidate="txtDate" Display="Dynamic" Operator="DataTypeCheck" runat="server"
                                                            ErrorMessage="Incorrect"></asp:CompareValidator>
                                                        <asp:RangeValidator ID="rvDate" EnableClientScript="false" runat="server"
                                                            Type="Date" ControlToValidate="txtDate" ValidationGroup="a" ForeColor="Red" Display="Dynamic"
                                                            ErrorMessage="*"></asp:RangeValidator>

                                                            </td> 
                                                            <td>
                                                                <asp:Label ID="Label11" runat="server" Text="Receipt Number"></asp:Label>
                                                                <br />
                                                                <asp:TextBox runat="server" ID="TxtSNo" TabIndex="4" 
                                                                    Width="120" CssClass="twitterStyleTextbox sp_number"></asp:TextBox>
                                                                  
                                                            </td>
                                                            
                                                        </tr></table> <br /><br />
                                                    <div class="box_c">
                    <div class="box_c_heading  box_actions">
                        <p>
                           Member / Chit Details
                        </p>
                    </div
                                                        <br /><br />
                                                        <table style="width: 80%;" ><tr>

                                                            <td>
                                                                <asp:Label ID="Label1" runat="server" Text=" Cash/Cheque Received by"></asp:Label>
                                                                <br />
                                                                <asp:DropDownList ID="dlBranchcollectorrName" runat="server" CssClass="chzn-select" AutoPostBack="true"
                                                                    Style="width: 200px !important;" TabIndex="1" OnSelectedIndexChanged="dlBranchcollectorrName_Click">
                                                                </asp:DropDownList>
                                                                <asp:Label ID="LabBname" runat="server" Visible="false"></asp:Label>
                                                                <asp:Label ID="LabBnameID" runat="server" Visible="false"></asp:Label>
                                                            </td>
                                                              <td>

                                                                <asp:Label ID="Label4" runat="server" Text="Branch Name"></asp:Label>
                                                                <br />
                                                                <asp:DropDownList ID="ddlbranchList1" runat="server" CssClass="chzn-select"
                                                                    Style="width: 200px !important;"  AutoPostBack="true" OnSelectedIndexChanged="ddlbranchList1_SelectedIndexChanged">
                                                                    
                                                                </asp:DropDownList>
                                                                <asp:Label ID="LabBanmelist" runat="server" Visible="False"></asp:Label>
                                                                <asp:Label ID="LabBanmelistID" runat="server" Visible="false"></asp:Label>

                                                            </td>
                                                            <td>

                                                                <asp:Label ID="Label2" runat="server" Text="Chit No" ></asp:Label>
                                                                <br />
                                                                <asp:DropDownList ID="DddlChitNO" runat="server" CssClass="chzn-select"  
                                                                    Style="width: 200px !important;" TabIndex="1"  AutoPostBack="true"
                                                                    OnSelectedIndexChanged="DddlChitNO_SelectedIndexChanged">
                                                                    
                                                                </asp:DropDownList>
                                                                   
                                                                <asp:Label ID="LabChit" runat="server" Visible="false"></asp:Label>
                                                                <asp:Label ID="LabChitID" runat="server" Visible="false"></asp:Label>

                                                            </td>
                                                           </tr>
                                                            <tr>
                                                                <td>
                                                                   

                                                                <asp:Label ID="Label3" runat="server" Text="Member Name"></asp:Label>
                                                                <br />
                                                                 <asp:DropDownList ID="ddlMembername" runat="server" CssClass="chzn-select" 
                                                                     Visible="false" 
                                                                   Style="width: 200px !important;" TabIndex="3" AutoPostBack="true" >
                                                                </asp:DropDownList>
                                                                <asp:Label ID="LabMemname" runat="server" Visible="true"></asp:Label>
                                                               

                                                           
                                                                </td>
                                                                <td>   <asp:Label ID="Label9" runat="server" Text="Member ID"></asp:Label>
                                                                    <br />
                                                                       <asp:Label ID="LabMemnameID" runat="server" Visible="true"></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table></div><br /><br />
                                                   
                                                    
                                                    
                                                    <div class="box_c">
                    <div class="box_c_heading  box_actions">
                        <p>Transaction </p></div>
                                                         <br /><br /><table  style="width: 80%;" >
                                                        
                                                    
                                                        <tr>


                                                            <td style="padding: 5px !important;">
                                                                <asp:Label ID="Label6" runat="server" Text="Chit Amount"></asp:Label>
                                                                <br />

                                                                <%--<input type="text" id="txtChitAmount" placeholder="pleaseenterFirst Number" onkeyup="sum()"  />--%>
                                                                <asp:TextBox ID="txtchit" AutoCompleteType="Disabled" runat="server" OnTextChanged="txtchit_TextChanged" AutoPostBack="true"></asp:TextBox>


                                                            </td>

                                                            <td>
                                                                <asp:Label ID="lab1" Visible="false" runat="server"></asp:Label>
                                                                <br />
                                                                <asp:dropdownlist runat="server" id="ddlTest"> 
                                                                    <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                                    <asp:listitem text="Misc Amount" value="1"></asp:listitem>
                                                                          <asp:listitem text="Intrest Amount" value="2"></asp:listitem>
    
                                                                                           </asp:dropdownlist>
                     
                                                <%--<asp:Label ID="Label13" runat="server" Text="Misc Amount"></asp:Label>--%>
                                                               
                                                                <%--<asp:TextBox runat="server" ID="TxtMiscAmount" TabIndex="4"  onblur="reSum();"
                                                                    Width="120" CssClass="twitterStyleTextbox sp_number"></asp:TextBox>--%>

                                                                <%--<input type="text" id="TxtMiscAmount" placeholder="pleaseenterFirs" onkeyup="sum()"  />--%>
                                                              
                                                            </td>
                                                            <td>
                                                                 <asp:Label ID="Label13"  runat="server" Text="Amount"></asp:Label>
                                                                <br />
                                                                  <asp:TextBox ID="txtmisc" AutoCompleteType="Disabled" runat="server" OnTextChanged="txtchit_TextChanged" AutoPostBack="true"></asp:TextBox>

                                                            </td>
                                                            <td>

                                                                <asp:Label ID="Label5" runat="server" Text="Other Amount" ></asp:Label>
                                                                <br />
                                                                <%--<asp:TextBox runat="server" ID="TxtOtherAmount" TabIndex="4"  onblur="reSum();"
                                                                    Width="120" CssClass="twitterStyleTextbox sp_number"></asp:TextBox>--%>

                                                                <%-- <input type="text" id="TxtOtherAmount" placeholder="pleaseenterFirst Number" onkeyup="sum()"/>--%>
                                                                <asp:TextBox ID="txtother" runat="server" OnTextChanged="txtchit_TextChanged" AutoPostBack="true" AutoCompleteType="Disabled"></asp:TextBox>


                                                            </td>
                                                            <td>

                                                                <asp:Label ID="Label12" runat="server" Text="Total Amount"></asp:Label>
                                                                <br />


                                                                <%--   <input type="text" id="TxtTotal" runat="server"/>--%>
                                                                <asp:TextBox ID="txttotalamount" runat="server" AutoPostBack="true" ReadOnly="true"></asp:TextBox>


                                                            </td>  
                                                        </tr>
                                                            <caption>
                                                                <br />
                                                                <tr>
                                                                    <td>
                                                                        <asp:Button ID="IDAdd" runat="server" CssClass="btn-custom" OnClick="IDAdd_Click" Style="margin: 0px auto; display: table;" TabIndex="14" Text="Add-Cash" />
                                                                    </td>
                                                                </tr>
                                                            </caption>
                                                                     </table></div>

                                                        <li id="chequelid">
                                                        <div class="box_c">
                    <div class="box_c_heading  box_actions" id="myDIV" runat="server">
                        <p>
                                                                     Cheque Receipt Details
                                                               </p></div>
                                                            <br />
                                                            <br /><table  style="width: 80%;" runat="server" id="chequetab">
                                                           
                                                        <tr>
                                                            <td> 
                                                                <asp:Label ID="Label14" runat="server" Text="Cheque Number"></asp:Label>
                                                                <br />

                                                                <asp:TextBox runat="server" ID="txtChequeNumber" TabIndex="4"
                                                                    Width="120" CssClass="twitterStyleTextbox sp_number"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label15" runat="server" Text="Bank Details"></asp:Label>
                                                                <br />
                                                                <asp:DropDownList runat="server" ID="ddlBankDetails" TabIndex="3" 
                                                                    CssClass="chzn-select" OnSelectedIndexChanged="ddlBankDetails_Click">
                                                                </asp:DropDownList><br />
                                                                 <asp:Label ID="LabBankDetailsText" runat="server" Visible="False"></asp:Label>
                                                                <asp:Label ID="LabBankDetailsID" runat="server" Visible="false"></asp:Label>


                                                            </td>


                                                            <td>
                                                                <asp:Label ID="Label7" runat="server" Text="Cheque Date"></asp:Label>
                                                                <br />

                                                                <asp:TextBox runat="server" ID="txtChequeDate" TabIndex="4" TextMode="Date"
                                                                    Width="120" CssClass="twitterStyleTextbox sp_number" ></asp:TextBox>
                                                              
                                                                   
                                                                  
                                                                 
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label8" runat="server" Text="Bank Head"></asp:Label>
                                                                <br />
                                                                <asp:DropDownList runat="server" ID="ddlBankHead" TabIndex="3" CssClass="chzn-select"
                                                                    Width="200" OnSelectedIndexChanged="ddlBankHead_Click">
                                                                </asp:DropDownList><br />
                                                                  <asp:Label ID="LabBankHeadText" runat="server" Visible="False"></asp:Label>
                                                                <asp:Label ID="LabBankHeadID" runat="server" Visible="false"></asp:Label>



                                                            </td>
                                                          

                                                        </tr>
                                                                <caption>
                                                                    <br />
                                                                    <tr>
                                                                        <td>
                                                                            <asp:Button ID="Idother" runat="server" CssClass="btn-custom" OnClick="Idother_Cick" Style="margin: 0px auto; display: table;" TabIndex="14" Text="Add-Cheque" />
                                                                        </td>
                                                                    </tr>
                                                                </caption>
                                                                  </table></div>
                                                   
                                              
                                           </li>
                                            <asp:Panel ID="Panel1" runat="server">
                                                <div style="overflow: auto; width: 900px; height: 200px" ><table  id="PrintGridData" runat="server"></table>
                                                    <asp:GridView ID="GridView2" runat="server" BorderStyle="Solid" OnPageIndexChanging="OnPageIndexChanging"
                                                        CellSpacing="4" Font-Names="Verdana" ForeColor="#333333" Height="100px" TabIndex="15"
                                                        AutoGenerateColumns="False" CssClass="Trans twelve columns"  Width="200%" >
                                                       
                                                      <%-- DataKeyNames="SeriesNumber"  OnRowDeleting="OnRowDeleting"--%>
                                                      
                                                        <RowStyle BackColor="#F7F6F3" />
                                                        <RowStyle CssClass="GridViewRowStyle" />
                                                        <HeaderStyle CssClass="GridViewHeaderStyle" Font-Bold="True" Width="200%"/>
                                                        <Columns>
                                                       <asp:TemplateField HeaderText = "Row Number" ItemStyle-Width="50" visible="false">
                                                                           <ItemTemplate>
            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
        </ItemTemplate>
    </asp:TemplateField>               
                                                             
                                                            <asp:BoundField HeaderText="Receipt Number" DataField="SeriesNumber"
                                                                HeaderStyle-BorderColor="Gray" HeaderStyle-BorderStyle="Solid" HeaderStyle-BorderWidth="2px"
                                                                ItemStyle-BorderColor="Gray" ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px" />

                                                            <%--<asp:BoundField HeaderText="Date" DataField="Date"/> --%>
                                                            <asp:BoundField HeaderText="Date" DataField="Date"
                                                                HeaderStyle-BorderColor="Gray" HeaderStyle-BorderStyle="Solid" HeaderStyle-BorderWidth="2px"
                                                                ItemStyle-BorderColor="Gray" ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px" />

                                                            <%-- <asp:BoundField HeaderText="ReceivedBy" DataField="ReceivedBy"/> --%>
                                                            <asp:BoundField HeaderText="ReceivedByID" DataField="ReceivedByID" visible="false" 
                                                                HeaderStyle-BorderColor="Gray" HeaderStyle-BorderStyle="Solid" HeaderStyle-BorderWidth="2px"
                                                               
 ItemStyle-BorderColor="Gray" ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px"  />
                                                            <asp:BoundField HeaderText="ReceivedBy" DataField="ReceivedBy"
                                                                HeaderStyle-BorderColor="Gray" HeaderStyle-BorderStyle="Solid" HeaderStyle-BorderWidth="2px"
                                                                ItemStyle-BorderColor="Gray" ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px" />


                                                            <asp:BoundField HeaderText="ChitnoID" DataField="ChitnoID" Visible="false"
                                                                HeaderStyle-BorderColor="Gray" HeaderStyle-BorderStyle="Solid" HeaderStyle-BorderWidth="2px"
                                                               
 ItemStyle-BorderColor="Gray" ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px"/>
                                                            <asp:BoundField HeaderText="Chitno" DataField="Chitno"
                                                                HeaderStyle-BorderColor="Gray" HeaderStyle-BorderStyle="Solid" HeaderStyle-BorderWidth="2px"
                                                                ItemStyle-BorderColor="Gray" ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px" />

                                                            <asp:BoundField HeaderText="MemberNameID" DataField="MemberNameID" Visible="false" 
                                                                HeaderStyle-BorderColor="Gray" HeaderStyle-BorderStyle="Solid" HeaderStyle-BorderWidth="3px"
                                                               
 ItemStyle-BorderColor="Gray" ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="3px"/>
                                                            <asp:BoundField HeaderText="MemberName" DataField="MemberName"
                                                                HeaderStyle-BorderColor="Gray" HeaderStyle-BorderStyle="Solid" HeaderStyle-BorderWidth="3px"  ControlStyle-Width="100%"
                                                                ItemStyle-BorderColor="Gray" ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="3px" />

                                                            <asp:BoundField HeaderText="BranchNameID" DataField="BranchNameID" Visible="false" 
                                                                HeaderStyle-BorderColor="Gray" HeaderStyle-BorderStyle="Solid" HeaderStyle-BorderWidth="2px"
                                                               
 ItemStyle-BorderColor="Gray" ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px"/>
                                                            <asp:BoundField HeaderText="BranchName" DataField="BranchName"
                                                                HeaderStyle-BorderColor="Gray" HeaderStyle-BorderStyle="Solid" HeaderStyle-BorderWidth="2px"
                                                                ItemStyle-BorderColor="Gray" ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px" />

                                                            <%--<asp:BoundField HeaderText="ChitAmount" DataField="ChitAmount"/> --%>
                                                            <asp:BoundField HeaderText="ChitAmount" DataField="ChitAmount"
                                                                HeaderStyle-BorderColor="Gray" HeaderStyle-BorderStyle="Solid" HeaderStyle-BorderWidth="2px"
                                                                ItemStyle-BorderColor="Gray" ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px" />

                                                            <%--<asp:BoundField HeaderText="MiscAmount" DataField="MiscAmount"/>  --%>
                                                            <asp:BoundField HeaderText="MiscAmount" DataField="MiscAmount"
                                                                HeaderStyle-BorderColor="Gray" HeaderStyle-BorderStyle="Solid" HeaderStyle-BorderWidth="2px"
                                                                ItemStyle-BorderColor="Gray" ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px" />

                                                            <%--<asp:BoundField HeaderText="OtherAmount" DataField="OtherAmount"/>  --%>
                                                            <asp:BoundField HeaderText="OtherAmount" DataField="OtherAmount"
                                                                HeaderStyle-BorderColor="Gray" HeaderStyle-BorderStyle="Solid" HeaderStyle-BorderWidth="2px"
                                                                ItemStyle-BorderColor="Gray" ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px" />

                                                            <%--<asp:BoundField HeaderText="TotalAmount" DataField="TotalAmount"/>--%>
                                                            <asp:BoundField HeaderText="TotalAmount" DataField="TotalAmount"
                                                                HeaderStyle-BorderColor="Gray" HeaderStyle-BorderStyle="Solid" HeaderStyle-BorderWidth="2px"
                                                                ItemStyle-BorderColor="Gray" ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px" />
                                                         
                                                            <%--<asp:TemplateField HeaderText="ref1" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSeriesNum" runat="server" Text='<%#Eval("SeriesNumber") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
                                                      <%--<asp:CommandField ShowDeleteButton="True" ButtonType="Button" />--%>

                                                          <%--  <asp:TemplateField HeaderText="Delete" ItemStyle-BorderColor="Gray" ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px"
                                                HeaderStyle-BorderColor="Gray" HeaderStyle-BorderStyle="Solid" HeaderStyle-BorderWidth="2px">
                                                <ItemTemplate>
                                                    <span onclick="return confirm('Are you sure want to delete?')">
                                                        <asp:LinkButton ID="btnDelete" runat="server" CommandName="Delete">
                                                            <asp:Image ID="img1" runat="server" ImageUrl="Images/Close.gif" />
                                                        </asp:LinkButton>
                                                    </span>
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>

                                                        </Columns>  

<%--                                                          <asp:TemplateField HeaderText="Action" HeaderStyle-Width="15%">
                        <ItemTemplate>
                            <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/images/edit.png" Width="15" Height="15" />
                            <span onclick="return confirm('Are you sure want to delete?')">
                                <asp:ImageButton ID="btnDelete" AlternateText="Delete" ImageUrl="~/images/delete.png" runat="server" Width="15" Height="15" CommandName="DeleteRow" />
                            </span>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:LinkButton ID="btnUpdate" Text="Update" runat="server" CommandName="Update" />
                            <asp:LinkButton ID="btnCancel" Text="Cancel" runat="server" CommandName="Cancel" />
                        </EditItemTemplate>
                    </asp:TemplateField>--%>
                                                    </asp:GridView>
                                                  
                                                </div>


                                               <%-- <br />
                                                <br />
                                                <div id="printcase" runat="server">
                                                  <input type="button" id="btnPrint" value="CashPrint" onclick="PrintGridData()" />

                                                </div>
                                                <div>
                                                   <asp:Label ID="lblresult" runat="server"></asp:Label>
                                                    <br />
                                                    <br />
                                                        <asp:Button ID="bt1Generate" runat="server" Text="Generate" OnClick="bt1Generate_Onclick" />
                                                        
                                                </div>--%>

                                                <br /><br />
                                            </asp:Panel>
                                            <asp:Panel runat="server" ID="panel2">
                                                <div style="overflow: auto; width: 900px; height: 200px"><table  id="PrintGridData1" runat="server"></table>
                                                    <asp:GridView ID="Gridtrr" runat="server" BorderStyle="Solid" OnPageIndexChanging="OnPageIndexChanging"
                                                        CellSpacing="3" Font-Names="Verdana" ForeColor="#333333" GridLines="Both" 
                                                        Height="100px" TabIndex="15"
                                                        AutoGenerateColumns="false" CssClass="Trans twelve columns" Width="200%">
                                                        <AlternatingRowStyle BackColor="PaleGoldenrod" />
                                                        <RowStyle BackColor="#F7F6F3" />
                                                        <RowStyle CssClass="GridViewRowStyle" />
                                                        <HeaderStyle CssClass="GridViewHeaderStyle" Font-Bold="true" />
                                                        <Columns>
                                                        <asp:TemplateField HeaderText = "Row Number" ItemStyle-Width="100" Visible="false">
                                                             <ItemTemplate>
            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
        </ItemTemplate>
    </asp:TemplateField>               
                            
                                                            <%--<asp:BoundField HeaderText="SeriesNumber" DataField="SeriesNumber"/> --%>
                                                            <asp:BoundField HeaderText="Receipt Number" DataField="SeriesNumber"
                                                                HeaderStyle-BorderColor="Gray" HeaderStyle-BorderStyle="Solid" HeaderStyle-BorderWidth="2px"
                                                                ItemStyle-BorderColor="Gray" ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px" />

                                                            <%--<asp:BoundField HeaderText="Date" DataField="Date"/> --%>
                                                            <asp:BoundField HeaderText="Date" DataField="Date"
                                                                HeaderStyle-BorderColor="Gray" HeaderStyle-BorderStyle="Solid" HeaderStyle-BorderWidth="2px"
                                                                ItemStyle-BorderColor="Gray" ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px" />

                                                            <%-- <asp:BoundField HeaderText="ReceivedBy" DataField="ReceivedBy"/> --%>
                                                            <asp:BoundField HeaderText="ReceivedByID" DataField="ReceivedByID" Visible="false"
                                                                HeaderStyle-BorderColor="Gray" HeaderStyle-BorderStyle="Solid" HeaderStyle-BorderWidth="2px"
 ItemStyle-BorderColor="Gray" ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px" />
                                                            <asp:BoundField HeaderText="ReceivedBy" DataField="ReceivedBy"
                                                                HeaderStyle-BorderColor="Gray" HeaderStyle-BorderStyle="Solid" HeaderStyle-BorderWidth="2px"
                                                                ItemStyle-BorderColor="Gray" ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px" />


                                                            <asp:BoundField HeaderText="ChitnoID" DataField="ChitnoID" 
                                                                HeaderStyle-BorderColor="Gray" HeaderStyle-BorderStyle="Solid" HeaderStyle-BorderWidth="2px"
 ItemStyle-BorderColor="Gray" ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px" />
                                                            <asp:BoundField HeaderText="Chitno" DataField="Chitno"
                                                                HeaderStyle-BorderColor="Gray" HeaderStyle-BorderStyle="Solid" HeaderStyle-BorderWidth="2px"
                                                                ItemStyle-BorderColor="Gray" ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px" />

                                                            <asp:BoundField HeaderText="MemberNameID" DataField="MemberNameID" Visible="false"
                                                                HeaderStyle-BorderColor="Gray" HeaderStyle-BorderStyle="Solid" HeaderStyle-BorderWidth="2px"
 ItemStyle-BorderColor="Gray" ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px" />
                                                            <asp:BoundField HeaderText="MemberName" DataField="MemberName"
                                                                HeaderStyle-BorderColor="Gray" HeaderStyle-BorderStyle="Solid" HeaderStyle-BorderWidth="2px"
                                                                ItemStyle-BorderColor="Gray" ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px" />

                                                              <asp:BoundField HeaderText="BranchNameID" DataField="BranchNameID" Visible="false" 
                                                                HeaderStyle-BorderColor="Gray" HeaderStyle-BorderStyle="Solid" HeaderStyle-BorderWidth="2px"
                                                               
                                                                ItemStyle-BorderColor="Gray" ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px"/>
                                                            <asp:BoundField HeaderText="BranchName" DataField="BranchName"
                                                                HeaderStyle-BorderColor="Gray" HeaderStyle-BorderStyle="Solid" HeaderStyle-BorderWidth="2px"
                                                                ItemStyle-BorderColor="Gray" ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px" />

                                                            <%--<asp:BoundField HeaderText="ChitAmount" DataField="ChitAmount"/> --%>
                                                            <asp:BoundField HeaderText="ChitAmount" DataField="ChitAmount"
                                                                HeaderStyle-BorderColor="Gray" HeaderStyle-BorderStyle="Solid" HeaderStyle-BorderWidth="2px"
                                                                ItemStyle-BorderColor="Gray" ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px" />

                                                            <%--<asp:BoundField HeaderText="MiscAmount" DataField="MiscAmount"/>  --%>
                                                            <asp:BoundField HeaderText="MiscAmount" DataField="MiscAmount"
                                                                HeaderStyle-BorderColor="Gray" HeaderStyle-BorderStyle="Solid" HeaderStyle-BorderWidth="2px"
                                                                ItemStyle-BorderColor="Gray" ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px" />

                                                            <%--<asp:BoundField HeaderText="OtherAmount" DataField="OtherAmount"/>  --%>
                                                            <asp:BoundField HeaderText="OtherAmount" DataField="OtherAmount"
                                                                HeaderStyle-BorderColor="Gray" HeaderStyle-BorderStyle="Solid" HeaderStyle-BorderWidth="2px"
                                                                ItemStyle-BorderColor="Gray" ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px" />

                                                            <%--<asp:BoundField HeaderText="TotalAmount" DataField="TotalAmount"/>--%>
                                                            <asp:BoundField HeaderText="TotalAmount" DataField="TotalAmount"
                                                                HeaderStyle-BorderColor="Gray" HeaderStyle-BorderStyle="Solid" HeaderStyle-BorderWidth="2px"
                                                                ItemStyle-BorderColor="Gray" ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px" />

                                                                <asp:BoundField HeaderText="ChequeNumber" DataField="ChequeNumber"
                                                                HeaderStyle-BorderColor="Gray" HeaderStyle-BorderStyle="Solid" HeaderStyle-BorderWidth="2px"
                                                                ItemStyle-BorderColor="Gray" ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px" />
                                                           
                                                            <asp:BoundField HeaderText="BankDetailsID" DataField="BankDetailsID" Visible="false"
 HeaderStyle-BorderColor="Gray" HeaderStyle-BorderStyle="Solid" HeaderStyle-BorderWidth="2px"
 ItemStyle-BorderColor="Gray" ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px" /> 

                                                               <asp:BoundField HeaderText="BankDetails" DataField="BankDetails"
                                                                HeaderStyle-BorderColor="Gray" HeaderStyle-BorderStyle="Solid" HeaderStyle-BorderWidth="2px"
                                                                ItemStyle-BorderColor="Gray" ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px" />

                                                               <asp:BoundField HeaderText="ChequeDate" DataField="ChequeDate"
                                                                HeaderStyle-BorderColor="Gray" HeaderStyle-BorderStyle="Solid" HeaderStyle-BorderWidth="2px"
                                                                ItemStyle-BorderColor="Gray" ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px" />

                                                            <asp:BoundField HeaderText="BankHeadId" DataField="BankHeadId" Visible="false"
 HeaderStyle-BorderColor="Gray" HeaderStyle-BorderStyle="Solid" HeaderStyle-BorderWidth="2px"
 ItemStyle-BorderColor="Gray" ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px" /> 

                                                               <asp:BoundField HeaderText="BankHead" DataField="BankHead"
                                                                HeaderStyle-BorderColor="Gray" HeaderStyle-BorderStyle="Solid" HeaderStyle-BorderWidth="2px"
                                                                ItemStyle-BorderColor="Gray" ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px" />
                                                            <%-- <asp:TemplateField HeaderText="ref1" Visible="false">
                                                    <ItemTemplate  OnRowDeleting="OnRowDeleting1">
                                                        <asp:Label ID="lblSeriesNum1" runat="server" Text='<%#Eval("SeriesNumber") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
                                                     <%-- <asp:CommandField ShowDeleteButton="True" ButtonType="Button" />--%>
                                                              <%-- <asp:TemplateField HeaderText="Delete" ItemStyle-BorderColor="Gray" ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px"
                                                HeaderStyle-BorderColor="Gray" HeaderStyle-BorderStyle="Solid" HeaderStyle-BorderWidth="2px">
                                                <ItemTemplate>
                                                    <span onclick="return confirm('Are you sure want to delete?')">
                                                        <asp:LinkButton ID="btnDelete" runat="server" CommandName="Delete">
                                                            <asp:Image ID="img1" runat="server" ImageUrl="Images/Close.gif" />
                                                        </asp:LinkButton>
                                                    </span>
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                                <br />
                                                <br />
                                               
                                            </asp:Panel>
                                            <div class="center">
                                               <table  width:"70%;" border-collapse: separate;>
                                                   <tr>

                                                      
                                                       <td  style="text-align:left;width:50%">
                                                             <asp:Button ID="btnGenerate" runat="server" CausesValidation="true" 
                                                                 CssClass="btn-custom" EnableViewState="false" OnClick="btnGenerte_Click" 
                                                                 Style="margin: 0px auto; display: table;text-align:center" TabIndex="14" 
                                                                 Text="Clear Data" ValidationGroup="a" />

                                                       </td>
                                                     

                                                            
                                                       <td  style="text-align:center;width:50%">
                                                             <asp:Button ID="bt1Generate" runat="server" Text="Generate"
                                                                 CssClass="btn-custom" CausesValidation="true" TabIndex="14" 
                                                                 Style="margin: 0px auto;text-align:center; display: table" 
                                                                 OnClick="bt1Generate_Onclick" />
                                                      
                                                       </td>
                                                       
                                                      
                                                       <td  style="text-align:right;width:50%">
                                                             <asp:Button ID="Print" runat="server" Text="Print"  CssClass="btn-custom"  
                                                                 CausesValidation="true" TabIndex="14"  Style="margin: 0px auto;text-align:center; 
                                                                  display: table" OnClick="Print_Onlick" AutoPostBack="true" ClientIDMode="Static"/> 
                                                       
                                                       </td>
                                                   </tr>
                                               </table>

                                              

                                              
                                                     <asp:Label ID="lblresult" runat="server"></asp:Label>
                                                   
                                                          
                                            </div>


                                        </ContentTemplate>
                                    </asp:UpdatePanel>

                                </div>

                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div>
    </div>

    <script runat="server">

        void DeleteRowButton_Click(Object sender, EventArgs e)
        {
            // Programmatically delete the selected record.
            GridView2.DeleteRow(GridView2.SelectedIndex);
        }

</script>


    <script type="text/javascript">
        function PrintGridData() {
            var prtGrid = document.getElementById('<%=GridView2.ClientID %>');

            prtGrid.border = 0;
            var prtwin = window.open('', 'PrintGridViewData', 'left=100,top=100,width=1000,height=1000,tollbar=0,scrollbars=1,status=0,resizable=1');
            prtwin.document.write(prtGrid.outerHTML);
            prtwin.document.close();
            prtwin.focus();
            prtwin.print();
            prtwin.close();
        }
    </script>

      <script type="text/javascript">
          function PrintGridData1() {
              var prtGrid = document.getElementById('<%=Gridtrr.ClientID %>');

              prtGrid.border = 0;
              var prtwin = window.open('', 'PrintGridViewData1', 'left=100,top=100,width=1000,height=1000,tollbar=0,scrollbars=1,status=0,resizable=1');
              prtwin.document.write(prtGrid.outerHTML);
              prtwin.document.close();
              prtwin.focus();
              prtwin.print();
              prtwin.close();
          }
    </script>



    <script>
        function sum() {
            var txtChitAmount = document.getElementById('txtChitAmount').value;
            var TxtMiscAmount = document.getElementById('TxtMiscAmount').value;
            var TxtOtherAmount = document.getElementById('TxtOtherAmount').value;


            var result = parseInt(txtChitAmount) + parseInt(TxtMiscAmount) + parseInt(TxtOtherAmount);
            if (!isNaN(result)) {
                document.getElementById('TxtTotal').value = result;
            }
        }
    </script>
    <script type="text/javascript">
        $(document).ready(function () {

            $(".chzn-select").chosen({ search_contains: true });
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

        function CheckDate() {
            var inputDate = $('#<%=txtDate.ClientID %>').val();
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
                    document.getElementById('<%=txtDate.ClientID %>').value = "";
                     alert('Incorrect Date Format..');
                 }
             }
             else {
                 document.getElementById('<%=txtDate.ClientID %>').value = "";
                alert('Incorrect Date Format..');
            }
        }

        $(document).ready(function () {
            $(function () {
                $('#<%=txtChequeDate.ClientID%>').datepicker();
            });
        });

        function CheckCheque_click() {
            var x = document.getElementById("myDIV");
            if (x.style.display === "none") {
                x.style.display = "block";
            }
            else {
                x.style.display = "none";
            }
        }


    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#<%=Gridtrr.ClientID %>').Scrollable({
                ScrollHeight: 300,
                IsInUpdatePanel: true
            });
        });
</script>
    <script>
        function MshowHide() {
            var mgift = $('#CheckCheque input[type=checkbox]');
            var chequelid = $('#chequelid');

            if (mgift.checked) {
                chequelid.show();
            }
            else {
                chequelid.hide();
            }

        }</script>

    <script>
        function update() {
            var select = document.getElementById('DddlChitNO');
            var option = select.options[select.selectedIndex];

            document.getElementById('value').value = option.LabMemname;
            document.getElementById('text').value = option.LabMemnameID;
        }

        update();
		</script>

</asp:Content>
