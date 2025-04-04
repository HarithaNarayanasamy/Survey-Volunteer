<%@ Page Title="SVCF - Admin Page" Culture="en-GB" Language="C#" MasterPageFile="~/Branch.Master"
    MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeBehind="trr.aspx.cs"
    Inherits="SreeVisalamChitFundLtd_phase1.trr" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMainContent" runat="server">
    <link href="Styles/tablecss.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        input[type="text"]
        {
            margin-bottom: 3px;
        }
        
        input[type="image"]:active
        {
            -moz-box-shadow: inset 0 0 10px rgba(0,0,0,.06);
            -webkit-box-shadow: inset 0 0 10px rgba(0,0,0,.06);
            box-shadow: inset 0 0 10px rgba(0,0,0,.06);
            z-index: 6;
            border: 2px solid #006469 !important;
        }
        .Trans div[id*="chzn"]
        {
            width: 100% !important;
        }
        .Trans div[id*="chzn"] span
        {
            width: 140px !important;
            overflow: hidden;
        }
        .Trans td
        {
            width: 14% !important;
            padding-left: 8px !important;
            padding-right: 8px !important;
        }
        .Trans td > input[type="text"]
        {
            width: 100% !important;
        }
        
        .Trans td div[class="ui-spinner"] > input[type="text"]
        {
            width: 100% !important;
        }
        .Trans select
        {
            width: 100% !important;
        }
        .hidable table
        {
            vertical-align: middle;
        }
        .hidable td
        {
            text-align:left;
            padding: 3px;
            margin: 3px;
        }
        .chzn-drop
        {
            border: 1px solid #aaa !important;
            padding-top: 5px !important;
        }
        div[id*="ddlMemberName_chzn"] .chzn-drop
        {
            width: 300% !important;
        }
        div[id*="ddlToken_chzn"] .chzn-drop
        {
            width: 100% !important;
        }
        div[id*="ddlMisc"] .chzn-drop
        {
            width: 200% !important;
        }
    </style>
    <ajax:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </ajax:ToolkitScriptManager>
    <div class="row">
        <div class="row">
            <div class="twelve columns">
                <div class="box_c">
                    <div class="box_c_heading  box_actions">
                        <p>
                           Cheque Receipt Details</p>
                    </div>
                    <div class="box_c_content">
                        <div class="row">
                            <br />
                            <div>   
                                            <div>
                                                <div style="width: 100%; margin: 0 auto;">
                                                    <table width="100%">
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label1" runat="server" Text="Collector Name"></asp:Label>
                                                                <br />
                                                                <asp:DropDownList Style="width: 200px !important;" TabIndex="1" ID="ddlColloctorName"
                                                                    CssClass="chzn-select" runat="server" AutoPostBack="false" 
                                                                    OnSelectedIndexChanged="ddlColloctorName_SelectedIndexChanged" 
                                                                    OnChange="GetMoneycollector(this);GetTRSeries();">
                                                                </asp:DropDownList>                                                                
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label2" runat="server" Text="Receipt Series"></asp:Label>
                                                                <br />
                                                                <asp:DropDownList Style="width: 150px !important;" ID="ddlReceiptSeries"
                                                                    TabIndex="2" runat="server" AutoPostBack="false" CausesValidation="false" 
                                                                    OnSelectedIndexChanged="ddlReceiptSeries_SelectedIndexChanged" Onchange="GetReceiptSeries(this);GetTRcptNumber();">
                                                                </asp:DropDownList>                                                               
                                                                <asp:HiddenField ID="HD_RSeriesid" runat="server" />
                                                            </td>
                                                             <td>                                                      
                                                              <asp:Label ID="Label7" runat="server" Text="Receipt No."></asp:Label>
                                                              <br />
                                                              <asp:TextBox autocomplete="off" CausesValidation="false" Width="100px" ID="txtReceiptNo" runat="server" TabIndex="3"
                                                                 CssClass="twitterStyleTextbox sp_number"></asp:TextBox>   
                                                                 <asp:UpdatePanel ID="updtpanel3" runat="server">
                                                                     <ContentTemplate>
                                                                         <asp:CheckBox ID="Chksamercno" runat="server" Text="Select" Checked="false" AutoPostBack="True" OnCheckedChanged="Chksamercno_CheckedChanged" />
                                                                         <asp:HiddenField ID="Hd_SameRCNo" runat="server" />
                                                                     </ContentTemplate>
                                                                 </asp:UpdatePanel>
                                                                  
                                                            </td>
                                                           
                                                            <td>
                                                                <asp:Label ID="Label3" runat="server" Text="Received By"></asp:Label>
                                                                <br />
                                                                <asp:DropDownList runat="server" ID="ddlEmployee" CssClass="chzn-select" TabIndex="4"
                                                                    CausesValidation="false" OnChange="GetEmployee(this)">
                                                                </asp:DropDownList>                                                                
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label6" runat="server" Text="Total Amount"></asp:Label>
                                                                <br />
                                                                <asp:TextBox ID="txtTotalAmount" CausesValidation="false" runat="server" TabIndex="5"
                                                                    Width="120" CssClass="twitterStyleTextbox sp_currency" autocomplete="off"></asp:TextBox>                                                                
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label5" runat="server" Text="Receipt Date"></asp:Label>
                                                                <br />
                                                                <asp:TextBox ID="txtReceivedDate" Width="90" CausesValidation="true" runat="server"
                                                                    TabIndex="6" autoComplete="off" CssClass="twitterStyleTextbox maskdate" ValidationGroup="a"
                                                                    onchange="CheckDate();CheckValidDate();"> 
                                                                    </asp:TextBox>  
                                                                 <asp:RequiredFieldValidator ForeColor="Red"  ID="RequiredFieldValidator6" ErrorMessage="Required!!!"
                                                                 ControlToValidate="txtReceivedDate" ValidationGroup="a" Display="Dynamic" runat="server"></asp:RequiredFieldValidator>
                                                                 <asp:CompareValidator ForeColor="Red" Type="Date" ID="CompareValidator11" ValidationGroup="a" 
                                                                 ControlToValidate="txtReceivedDate" Display="Dynamic" Operator="DataTypeCheck" runat="server"
                                                                 ErrorMessage="Incorrect!!!"></asp:CompareValidator>
                                                                 <asp:RangeValidator ID="rvDate" EnableClientScript="false" runat="server" 
                                                                 Type="Date" ControlToValidate="txtReceivedDate" ValidationGroup="a" ForeColor="Red" Display="Dynamic" 
                                                                 ErrorMessage="*"></asp:RangeValidator>
                                                                <br />                                   

                                                                                                                             
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>

             <div  id="divcancel" runat="server" visible="false">
                                                <div class="box_c_ico">
                   <asp:Image runat="server" ID="Image1" ImageUrl="pertho_admin_v1.3/img/ico/icSw2/16-List.png"
                         AlternateText="" /></div>
                   <p>Cancel Receipt</p>
                  <asp:ImageButton ID="ImgCancelRcpt" runat="server" ImageUrl="~/Styles/Image/Images/RemoveReceipt.png" OnClick="ImgCancelRcpt_Click"/>
                  <br />
                  <asp:Label ID="lblcancelmsg" runat="server" Text="" CssClass="lblstyle"></asp:Label>
                 </div>
              </div>

                                                  <div>
                
                                                <div class="box_c_heading cf">
                                                    <div class="box_c_ico">
                                                        <asp:Image runat="server" ID="img16List" ImageUrl="pertho_admin_v1.3/img/ico/icSw2/16-List.png"
                                                            AlternateText="" /></div>
                                                    <p>
                                                        Transactions</p>
                                                </div>
                                                 <table style="width:950px;">
                                                  <tr>
                                                  
                                                   <td>
                                                       <asp:Label ID="Label9" runat="server" Text="Token no."></asp:Label>
                                                     <br />
                                                       <asp:DropDownList TabIndex="7" CausesValidation="false" Width="100px" ID="ddlToken"
                                                           runat="server" OnChange="GetMemberName();" CssClass="chzn-select">
                                                       </asp:DropDownList>  
                                                                                                      
                                                    </td>  
                                                     <td></td>                                                                                                                                                            
                                                    <td>
                                                        <asp:Label ID="Label8" runat="server" Text="Member Name"></asp:Label>
                                                    <br />
                                                        <asp:TextBox ID="TxtMemberName" runat="server" Width="150px" TabIndex="8"></asp:TextBox>                                      
                                                         <asp:HiddenField ID="hiddenmemberid" runat="server" />       
                                                    </td>
                                                      <td></td>
                                                    <td>
                                                      <asp:Label ID="Label10" runat="server" Text="Amount"></asp:Label>
                                                      <br />
                                                      <asp:TextBox autocomplete="off" Width="80px" TabIndex="9" runat="server" CausesValidation="false" CssClass="twitterStyleTextbox sp_currency"
                                                       ID="txtAmount"></asp:TextBox>                                                     
                                                    </td>  
                                                    <td></td>       
                                                    <td>
                                                      <asp:Label ID="Label11" runat="server" Text="Other"></asp:Label>
                                                      <br />
                                                      <asp:DropDownList TabIndex="10" CausesValidation="false" ID="ddlMisc" 
                                                       CssClass="chzn-select" Width="200px" runat="server" OnChange="GetMisc(this)">
                                                      </asp:DropDownList>                                                     
                                                     </td>   
                                                     <td>
                                                      <asp:Label ID="Label12" runat="server" Text="Misc Amount"></asp:Label>
                                                       <br />
                                                        <asp:TextBox TabIndex="11" Width="100px" runat="server" CausesValidation="false" CssClass="twitterStyleTextbox sp_currency"
                                                         ID="txtMisc"></asp:TextBox>
                                                     </td>
                                                     <td>     
                                                        <br />                                                 
                                                        <asp:ImageButton Width="24" Height="24" ImageUrl="~/Styles/Image/Images/AddReceipt.png"
                                                          ID="ButtonAdd" runat="server" CausesValidation="true"
                                                          TabIndex="12" CssClass="GreenyPushButton" ToolTip="Add" OnClick="ButtonAdd_Click" />
                                                     </td>                                                               
                                                    </tr>                                                        
                                                           
                                                </table>
                                                <asp:CheckBox Checked="true" Visible="false" ID="CheckBox1" runat="server" />

<div></div>
                              <div>
                                 <asp:GridView ID="GView_Selected" BorderStyle="Solid" runat="server" TabIndex="13"
                                  CellSpacing="11" Font-Names="Verdana" ForeColor="#333333" GridLines="Both" 
                                  AutoGenerateColumns="false" Width="900px" CssClass="Trans twelve columns" 
                                      onrowdeleting="GView_Selected_RowDeleting">
                                   <RowStyle BackColor="#F7F6F3" />
                                    <RowStyle CssClass="GridViewRowStyle" />  
                                    <HeaderStyle CssClass="GridViewHeaderStyle" /> 
                                    <Columns>
                                       <asp:BoundField HeaderText="RowNumber"  DataField="RowNumber" Visible="false"
                                       HeaderStyle-BorderColor="Gray" HeaderStyle-BorderStyle="Solid" HeaderStyle-BorderWidth="2px"
                                       ItemStyle-BorderColor="Gray" ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px" />     
                                                                         
                                        <asp:BoundField HeaderText="ReceiptNumber"  DataField="RecptNumber" Visible="true"
                                       HeaderStyle-BorderColor="Gray" HeaderStyle-BorderStyle="Solid" HeaderStyle-BorderWidth="2px"
                                       ItemStyle-BorderColor="Gray" ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px" /> 

                                       <asp:BoundField HeaderText="MemberName" DataField="MemberName" 
                                       HeaderStyle-BorderColor="Gray" HeaderStyle-BorderStyle="Solid" HeaderStyle-BorderWidth="2px"
                                       ItemStyle-BorderColor="Gray" ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px"/>

                                       <asp:BoundField HeaderText="Token" DataField="Token" 
                                       HeaderStyle-BorderColor="Gray" HeaderStyle-BorderStyle="Solid" HeaderStyle-BorderWidth="2px"
                                       ItemStyle-BorderColor="Gray" ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px"  /> 
                                                                             
                                       <asp:BoundField HeaderText="Amount" DataField="Amount" 
                                       HeaderStyle-BorderColor="Gray" HeaderStyle-BorderStyle="Solid" HeaderStyle-BorderWidth="2px"
                                       ItemStyle-BorderColor="Gray" ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px"/>

                                       <asp:BoundField HeaderText="MiscHead" DataField="MiscHead" 
                                       HeaderStyle-BorderColor="Gray" HeaderStyle-BorderStyle="Solid" HeaderStyle-BorderWidth="2px"
                                       ItemStyle-BorderColor="Gray" ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px"/>

                                       <asp:BoundField HeaderText="MiscAmount" DataField="MiscAmount" HeaderStyle-BorderColor="Gray" HeaderStyle-BorderStyle="Solid" HeaderStyle-BorderWidth="2px"
                                       ItemStyle-BorderColor="Gray" ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px"/>

                                       <asp:TemplateField HeaderText="HeadId" Visible="false">
                                          <ItemTemplate>
                                            <asp:Label ID="lblheadid" runat="server" Text='<%#Eval("Head_Id") %>'></asp:Label>
                                          </ItemTemplate>
                                       </asp:TemplateField>

                                       <asp:TemplateField HeaderText="MemberId" Visible="false">
                                          <ItemTemplate>
                                            <asp:Label ID="lblmemberid" runat="server" Text='<%#Eval("MemberId") %>'></asp:Label>
                                          </ItemTemplate>
                                       </asp:TemplateField>

                                          <asp:TemplateField HeaderText="ref" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblref1" runat="server" Text='<%#Eval("firstmisc") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="ref1" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblref2" runat="server" Text='<%#Eval("secmisc") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                       <asp:TemplateField ItemStyle-BorderColor="Gray" ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px"
                                            HeaderStyle-BorderColor="Gray" HeaderStyle-BorderStyle="Solid" HeaderStyle-BorderWidth="2px">
                                           <ItemTemplate>
                                             <span onclick="return confirm('Are you sure want to delete?')">             
                                             <asp:LinkButton ID="btnDelete" runat="server" CommandName="Delete">
                                               <asp:Image ID="img1" runat="server" ImageUrl="Images/Close.gif" />
                                             </asp:LinkButton>
                                            </span>
                                         </ItemTemplate>
                                       </asp:TemplateField>
                                    </Columns>                                   
                                  </asp:GridView>
                                </div>
                                                <div style="margin-top:52px; width: 70%; text-align: center !important; margin: 0 auto;">
                                                    <table class="hidable">
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label4" runat="server" Text="Cheque Number"></asp:Label>
                                                            </td>
                                                            <td>
                                                              <asp:UpdatePanel ID="updt2" runat="server">
                                                                <ContentTemplate>
                                                                
                                                                <asp:TextBox autocomplete="off" AutoPostBack="true" OnTextChanged="Cheque_TextCahanged" CausesValidation="false" ID="txtCheque" CssClass="twitterStyleTextbox sp_number"
                                                                    TabIndex="14" MaxLength="6" runat="server"></asp:TextBox>
                                                                <asp:RequiredFieldValidator EnableClientScript="false" ID="RFVtxtCheque" ControlToValidate="txtCheque"
                                                                    Display="Static" SetFocusOnError="true" ValidationGroup="b" runat="server" ForeColor="Red"
                                                                    ErrorMessage="*"></asp:RequiredFieldValidator>
                                                                    <br />
                                                                    <asp:Label runat="server" ID="lbVisible" ForeColor="Red" Text="Cheque Number already Exists." Visible="false"></asp:Label>
                                                                </ContentTemplate>
                                                             </asp:UpdatePanel>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblBL" runat="server" Text="Bank Details"></asp:Label>
                                                            </td>
                                                            <td>
                                                              <asp:UpdatePanel ID="updt1" runat="server">
                                                               <ContentTemplate>
                                                               
                                                                <asp:DropDownList Style="width: 200px !important;" AutoPostBack="true" OnTextChanged="Cheque_TextCahanged" TabIndex="15" CausesValidation="false"
                                                                    runat="server" ID="ddlBanklDetails" CssClass="chzn-select"  OnChange="GetBanklDetails(this)">
                                                                </asp:DropDownList>
                                                                <asp:CompareValidator EnableClientScript="false" ID="RFVddlBanklDetails" ControlToValidate="ddlBanklDetails"
                                                                    ValueToCompare="--Select--" Display="Static" SetFocusOnError="true" ValidationGroup="b"
                                                                    runat="server" ForeColor="Red" Operator="NotEqual" ErrorMessage="*"></asp:CompareValidator>
                                                               </ContentTemplate>
                                                             </asp:UpdatePanel>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblBankHead" runat="server" Text="Bank Head"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList Style="width: 200px !important;" ID="ddlBankHead" CssClass="chzn-select"
                                                                    ValidationGroup="b" TabIndex="16" runat="server" CausesValidation="false"  OnChange="GetBankHead(this)">
                                                                </asp:DropDownList>
                                                                <asp:CompareValidator EnableClientScript="false" ID="CVddlBankHead" ForeColor="Red"
                                                                    ValueToCompare="0" ControlToValidate="ddlBankHead" Display="Dynamic" Operator="NotEqual"
                                                                    SetFocusOnError="true" ValidationGroup="b" runat="server" ErrorMessage="*"></asp:CompareValidator>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblDate" runat="server" Text="Cheque Date"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox CausesValidation="false" ID="txtDateinCheque" autoComplete="off" TabIndex="17" CssClass="twitterStyleTextbox maskdate"
                                                                    Width="100" runat="server"></asp:TextBox>                                                                
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label runat="server" ID="lbIdCardNumber" Text="Card Number"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox runat="server" ID="txtIdcardNumber" TabIndex="18" autocomplete="off"
                                                                    CssClass="twitterStyleTextbox"></asp:TextBox>
                                                                <br />
                                                                <asp:RequiredFieldValidator ControlToValidate="txtIdcardNumber" Display="Dynamic"
                                                                    EnableClientScript="false" ErrorMessage="*" ForeColor="Red" ID="rvCardNumber"
                                                                    runat="server" ValidationGroup="b"></asp:RequiredFieldValidator>
                                                            </td>
                                                            <td>
                                                                <asp:Label runat="server" ID="lbCardType" Text="Card Type"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList Style="width: 200px !important;" ID="ddlCardType" CssClass="chzn-select"
                                                                    ValidationGroup="b" TabIndex="19" runat="server" CausesValidation="false">
                                                                    <asp:ListItem Text="--Select--" Value="--Select--"></asp:ListItem>
                                                                    <asp:ListItem Text="Driving Licence" Value="Driving Licence"></asp:ListItem>
                                                                    <asp:ListItem Text="Passport" Value="Passport"></asp:ListItem>
                                                                    <asp:ListItem Text="PAN Card" Value="PAN Card"></asp:ListItem>
                                                                    <asp:ListItem Text="Voter ID Card" Value="Voter ID Card"></asp:ListItem>
                                                                    <asp:ListItem Text="Bank Passbook" Value="Bank Passbook"></asp:ListItem>
                                                                    <asp:ListItem Text="Aadhaar ID" Value="Aadhaar ID"></asp:ListItem>
                                                                    <asp:ListItem Text="Ration Card" Value="Ration Card"></asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:CompareValidator EnableClientScript="false" ID="cvddlCardType" ForeColor="Red"
                                                                    ValueToCompare="--Select--" ControlToValidate="ddlCardType" Display="Dynamic"
                                                                    Operator="NotEqual" SetFocusOnError="true" ValidationGroup="b" runat="server"
                                                                    ErrorMessage="*"></asp:CompareValidator>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </div>
                                        
                                    <asp:Button ID="btnGenerate" runat="server" CssClass="GreenyPushButton" CausesValidation="true"
                                        TabIndex="20" Style="margin: 0px auto; display: table;" Text="Generate" ValidationGroup="a"
                                        OnClick="btnGenerate_Click"></asp:Button>                              
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <%--<asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
        <ProgressTemplate>
            <div>
                <div style="position: absolute; left: 50%; margin-left: -50px; top: 186px;">
                    <asp:Image runat="server" ID="imgWaiting" AlternateText="waiting" ImageUrl="Styles/Image/waiting.gif"
                        Style="vertical-align: middle;" />
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>--%>
    <ajax:ModalPopupExtender ID="ModalPopupExtender1" TargetControlID="show" PopupControlID="Pnlgendrate"
        BackgroundCssClass="modalBackground" runat="server">
    </ajax:ModalPopupExtender>
    <asp:Panel Style="background: #fff; border: 4px solid #ccc; border: 8px solid rgba(0, 0, 0, 0.5);
        background-clip: padding-box; -webkit-border-radius: 10px; -moz-border-radius: 10px;
        border-radius: 10px; min-width: 300px; max-width: 900px;" CssClass="raised" ID="pnlConfirmation"
        runat="server" Visible="false">
        <asp:Label runat="server" ID="lblHintConfirmation" Text="" Visible="false"> </asp:Label>
        <div class=" box_c_heading" style="text-align: center !important;">
            <p>
                <asp:Label runat="server" ID="lblHeadingConfirmation" Text=""> </asp:Label></p>
        </div>
        <div style="min-height: 20px; overflow: hidden; max-width: 880px; padding: 20px;">
            <div style="min-height: 20px; overflow: auto; max-width: 840px;">
                <br />
                <br />
                <asp:Label CssClass="inner_heading" runat="server" Style="margin-top: 10px; font-size: 14px;"
                    ID="lblContentConfirmation" Text="Please Confirm Your Transaction???"> </asp:Label>
                <br />
                <br />
                <asp:GridView ID="gvConfirm" Width="810" runat="server" AutoGenerateColumns="true"
                    HeaderStyle-BackColor="#61A6F8" ShowFooter="True" HeaderStyle-Font-Bold="true"
                    HeaderStyle-ForeColor="White" PageSize="20" BackColor="White" BorderWidth="1px"
                    CellPadding="4" BorderColor="#DEDFDE" BorderStyle="None" ForeColor="Black" GridLines="None">
                    <RowStyle BackColor="#F7F7DE" />
                    <HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center" CssClass="GridviewScrollC2Header" />
                    <RowStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="GridviewScrollC2Item" />
                    <PagerStyle CssClass="GridviewScrollC2Pager" />
                </asp:GridView>
            </div>
        </div>
        <div class=" box_c_heading">
            <div style="float: right;">
                <asp:Button Style="margin: 0 auto" CssClass="GreenyPushButton" ID="Button1" OnClick="btnConfirmationYes_Click"
                    runat="server" Text="yes" />
                <asp:Button Style="margin: 0 auto" CssClass="GreenyPushButton" ID="Button2" OnClick="btnConfirmationNo_Click"
                    runat="server" Text="No" />
            </div>
        </div>
    </asp:Panel>
    <asp:Panel ID="Pnlgendrate" Style="background: #fff; border: 4px solid #ccc; border: 8px solid rgba(0, 0, 0, 0.5);
        background-clip: padding-box; -webkit-border-radius: 10px; -moz-border-radius: 10px;
        border-radius: 10px; min-width: 300px; max-width: 900px;" CssClass="raised" runat="server"
        Visible="false">
        <asp:Label runat="server" ID="lblHint" Text="" Visible="false"> </asp:Label>
        <div class=" box_c_heading">
            <asp:Label CssClass="inner_heading" runat="server" ID="lblHD" Text=""> </asp:Label>
        </div>
        <div style="min-height: 20px; overflow: hidden; max-width: 880px; padding: 20px;">
            <div style="min-height: 20px; overflow: auto; max-width: 840px;">
                <br />
                <br />
                <asp:Label runat="server" Style="margin-top: 10px; font-size: 14px;" ID="lblContent"
                    Text=""> </asp:Label>
            </div>
        </div>
        <div class=" box_c_heading">
            <div style="float: right;">
                <asp:Button Style="margin: 0 auto" CssClass="GreenyPushButton" ID="btnyes" OnClick="btnyes_Click"
                    runat="server" Text="yes" />
                <asp:Button Style="margin: 0 auto" CssClass="GreenyPushButton" ID="btnNo" OnClick="btnNo_Click"
                    runat="server" Text="No" />
            </div>
        </div>
    </asp:Panel>
    <asp:Panel CssClass="raised" ID="pnlmsg" runat="server" Visible="false" Width="300px"
        Style="min-height: 100px">
        <%--<div  style="background-color:#3979BA;width: 100%; height: 40px;  top: 0px;"  >--%>
        <div style="top: 0px; height: 50px; text-align: center; padding: auto 0; width: 100%"
            class="boxheader">
            <asp:Label runat="server" ID="lblh" Text=""> </asp:Label>
        </div>
        <div style="min-height: 100px;">
            <asp:Label runat="server" ID="lblcon" Text=""> </asp:Label>
        </div>
        <div class="boxheader">
            <div style="margin: 0 auto;">
                <asp:Button Style="margin: 0 auto" CssClass="GreenyPushButton" ID="BtnOK" OnClick="btnOK_Click"
                    runat="server" Text="Ok" />
            </div>
        </div>
    </asp:Panel>
    <asp:LinkButton runat="server" ID="show" Text=""></asp:LinkButton>
    
    <script type="text/javascript">
        function checkchqdate(txt) {


            //  alert('mm');
            $('#<%=txtDateinCheque.ClientID%>').val(txt.value);
        }
        function checkdate(txt) {


            //  alert('mm');
            $('#<%=txtReceivedDate.ClientID%>').val(txt.value);
        }
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

          <%--  Button disable coding--%>
        function DisableButton() {
            document.getElementById("<%=Button1.ClientID %>").disabled = true;
        }
        window.onbeforeunload = DisableButton;
        <%--  Button disable coding--%>

    </script>
    
    
  
   <script type="text/javascript">
       function CheckDate() {
           var inputDate = $('#<%=txtReceivedDate.ClientID %>').val();
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
                   document.getElementById('<%=txtReceivedDate.ClientID %>').value = "";
                   alert('Incorrect Date Format..');
               }
           }
           else {
               document.getElementById('<%=txtReceivedDate.ClientID %>').value = "";
               alert('Incorrect Date Format..');
           }
       }
    </script>

      <script type="text/javascript">
          function GetMoneycollector(ddlColloctorName) {
              var selectedText = ddlColloctorName.options[ddlColloctorName.selectedIndex].innerHTML;
              var selectedValue = ddlColloctorName.value;

          }


          function GetReceiptSeries(ddlReceiptSeries) {
              var selectedText = ddlReceiptSeries.options[ddlReceiptSeries.selectedIndex].innerHTML;
              var selectedValue = ddlReceiptSeries.value;

          }



          function GetEmployee(ddlEmployee) {
              var selectedText = ddlEmployee.options[ddlEmployee.selectedIndex].innerHTML;
              var selectedValue = ddlEmployee.value;

          }



          function GetMemberName(ddlMemberName) {
              var selectedText = ddlMemberName.options[ddlMemberName.selectedIndex].innerHTML;
              var selectedValue = ddlMemberName.value;

          }




          function GetToken(ddlToken) {
              var selectedText = ddlToken.options[ddlToken.selectedIndex].innerHTML;
              var selectedValue = ddlToken.value;

          }



          function GetMisc(ddlMisc) {
              var selectedText = ddlMisc.options[ddlMisc.selectedIndex].innerHTML;
              var selectedValue = ddlMisc.value;

          }


          function GetBanklDetails(ddlBanklDetails) {
              var selectedText = ddlBanklDetails.options[ddlBanklDetails.selectedIndex].innerHTML;
              var selectedValue = ddlBanklDetails.value;

          }



          function GetBankHead(ddlBankHead) {
              var selectedText = ddlBankHead.options[ddlBankHead.selectedIndex].innerHTML;
              var selectedValue = ddlBankHead.value;

          }
    </script>

    
    
   
     <script type="text/javascript">
         function CheckValidDate() {
             var inputDate = $('#<%=txtReceivedDate.ClientID %>').val();
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
                     document.getElementById('<%=txtReceivedDate.ClientID %>').value = "";
                     alert('Incorrect Date Format..');
                 }
             }
             else {
                 document.getElementById('<%=txtReceivedDate.ClientID %>').value = "";
                 alert('Incorrect Date Format..');
             }
         }
    </script>
    



   <script type = "text/javascript">
   
       function GetMemberName() {
           var myparam = $("#<%=ddlToken.ClientID%>").val(); //id name for dropdown list   
           var selser = "";
           selser = $("#<%=ddlReceiptSeries.ClientID%>").find("option:selected").text();      
           selser = selser.replace(/(\r\n\t|\n|\r|\t)/gm, "");
           selser = selser.trim();
           $("#<%= HD_RSeriesid.ClientID %>").val("");
           $("#<%= HD_RSeriesid.ClientID %>").val(selser);

           $.ajax({
               type: "POST",
               contentType: "application/json; charset=utf-8",
               url: "trr.aspx/GetCustomername",
               data: "{hdid:" + myparam + "}",
               dataType: "json",
               success: function (data) {
                   var msg = data.d.toString();
                   var txtsnumber = $("[id*=TxtMemberName]");
                   txtsnumber.val(msg);
               },
               error: function (result) {
                   alert("Error: " + result);
               }
           });

           $.ajax({
               type: "POST",
               contentType: "application/json; charset=utf-8",
               url: "trr.aspx/GetMemberid",
               data: "{hdid:" + myparam + "}",
               dataType: "json",
               success: function (data) {
                   var msg = data.d.toString();
                   $("#<%= hiddenmemberid.ClientID %>").val(msg);
               },
               error: function (result) {
                   alert("Error: " + result);
               }
           });

           $("#<%=ddlToken.ClientID%>").addClass('chzn-select');
       }


       function GetTRSeries() {          
               var myparam = $("#<%=ddlColloctorName.ClientID%>").val(); //id name for dropdown list              
               $.ajax({
                   type: "POST",
                   contentType: "application/json; charset=utf-8",
                   url: "trr.aspx/PopulateTRSeries",
                   data: "{mcid:" + myparam + "}",
                   dataType: "json",
                   success: function (data) {
                       //alert("success");
                       //$.each(data.d, function (key, value) {                                             
                       var ddrseries = $("[id*=ddlReceiptSeries]");
                       ddrseries.empty();
                       $.each(data.d, function () {
                           ddrseries.append($("<option></option>").val(this['Value']).html(this['Text']));
                       });
                   },
                   error: function (result) {
                       alert("Error: " + result);
                   }
               });
          
           GetRcNum();
       }

       function GetRcNum() {
           var ser = $("#<%=ddlReceiptSeries.ClientID%>").text(); //id name for dropdown list              
           //var cid = $("#<%=ddlReceiptSeries.ClientID%>").val(); //text name for dropdown list  
           var cid = $("#<%=ddlColloctorName.ClientID%>").find("option:selected").val(); //text name for dropdown list                                
           $.ajax({
               type: "POST",
               contentType: "application/json; charset=utf-8",
               url: "trr.aspx/getRcptNumber",
               data: JSON.stringify({ Series: ser, CollectorID: cid }),
               //data: "{Series:" + ser + ",CollectorID:" + cid + "}",
               //data: {"Series:" + series + ",CollectorID:" + collid + },
               // data: JSON.stringify(obj),
               //data: {},
               dataType: "json",
               success: function (data) {
                   var msg = data.d.toString();
                   var txtsnumber = $("[id*=txtReceiptNo]");
                   txtsnumber.val(msg);                  
               },
               error: function (xhr, ajaxOptions, thrownError) {
                   alert("Error: " + xhr.responseText);
               }
           });
       }

      

       $(document).ready(function () {
           $("#<%=ddlToken.ClientID%>").change(function () {
               var myparam = $("#<%=ddlToken.ClientID%>").val(); //id name for dropdown list              
               $.ajax({
                   type: "POST",
                   contentType: "application/json; charset=utf-8",
                   url: "trr.aspx/GetCustomername",
                   data: "{hdid:" + myparam + "}",
                   dataType: "json",
                   success: function (data) {
                       var ddrseries = $("[id*=ddlMemberName]");
                       $.each(data.d, function () {
                           ddrseries.append($("<option></option>").val(this['Value']).html(this['Text']));
                       });
                   },
                   error: function (result) {
                       alert("Error: " + result);
                   }
               });

               $.ajax({
                   type: "POST",
                   contentType: "application/json; charset=utf-8",
                   url: "trr.aspx/GetMemberid",
                   data: "{hdid:" + myparam + "}",
                   dataType: "json",
                   success: function (data) {
                       var msg = data.d.toString();
                       $("#<%= hiddenmemberid.ClientID %>").val(msg);
                   },
                   error: function (result) {
                       alert("Error: " + result);
                   }
               });

               $("#<%=ddlToken.ClientID%>").addClass('chzn-select');
           });
       });

       function GetTRcptNumber() {
         
               var ser = $("#<%=ddlReceiptSeries.ClientID%>").find("option:selected").text(); //id name for dropdown list              
               var cid = $("#<%=ddlReceiptSeries.ClientID%>").val(); //text name for dropdown list  
            
               //var obj = {};
               //obj.series = ser;
               //obj.collid = cid;
               $.ajax({
                   type: "POST",
                   contentType: "application/json; charset=utf-8",
                   url: "trr.aspx/gtTRcptBkNumber",
                   data: JSON.stringify({ Series: ser, CollectorID: cid }),                   
                   dataType: "json",
                   success: function (data) {
                       var msg = data.d.toString();
                       var txtsnumber = $("[id*=txtReceiptNo]");
                       txtsnumber.val(msg);
                       var hdnumber = $("[id*=HD_RCNumber]");
                       hdnumber.val("");
                       hdnumber.val(msg);
                   },
                   error: function (result) {
                       alert("Error: " + result);
                   }
               });        

       }

       


      <%-- function GetMemberToken() {

           $("#<%=ddlMemberName.ClientID%>").change(function () {
               var myparam = $("#<%=ddlMemberName.ClientID%>").val(); //id name for dropdown list              
               $.ajax({
                   type: "POST",
                   contentType: "application/json; charset=utf-8",
                   url: "trr.aspx/PopulateTToken",
                   data: "{mcid:" + myparam + "}",
                   dataType: "json",
                   success: function (data) {
                       //alert("success");
                       //$.each(data.d, function (key, value) {                                             
                       var ddrseries = $("[id*=ddlToken]");
                       ddrseries.empty().append('<option selected="selected" value="0">Please select</option>');
                       $.each(data.d, function () {
                           ddrseries.append($("<option></option>").val(this['Value']).html(this['Text']));
                       });
                   },
                   error: function (result) {
                       alert("Error: " + result);
                   }
               });
           });

       }

       $(document).ready(function () {
           $("#<%=ddlMemberName.ClientID%>").change(function () {
               var myparam = $("#<%=ddlMemberName.ClientID%>").val(); //id name for dropdown list              
               $.ajax({
                   type: "POST",
                   contentType: "application/json; charset=utf-8",
                   url: "trr.aspx/PopulateTToken",
                   data: "{mcid:" + myparam + "}",
                   dataType: "json",
                   success: function (data) {
                       //alert("success");
                       //$.each(data.d, function (key, value) {                                             
                       var ddrseries = $("[id*=ddlToken]");
                       ddrseries.empty().append('<option selected="selected" value="0">Please select</option>');
                       $.each(data.d, function () {
                           ddrseries.append($("<option></option>").val(this['Value']).html(this['Text']));
                       });
                   },
                   error: function (result) {
                       alert("Error: " + result);
                   }
               });
           });
       });--%>

       </script>

</asp:Content>
