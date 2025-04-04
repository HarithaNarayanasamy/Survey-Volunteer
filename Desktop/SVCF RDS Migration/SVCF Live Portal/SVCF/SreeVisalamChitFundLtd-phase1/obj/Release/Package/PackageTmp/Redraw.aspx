<%@ Page Language="C#" AutoEventWireup="true" Culture="en-GB" MasterPageFile="~/Branch.Master"
CodeBehind="Redraw.aspx.cs" Inherits="SreeVisalamChitFundLtd_phase1.Redraw" %>



<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMainContent" runat="server">
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
    <ajax:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </ajax:ToolkitScriptManager>
    <div class="row">
        <div class="twelve columns">
            <div class="box_c">
                <div class="box_c_heading  box_actions">
                    <p>
                        Redraw</p>
                </div>
                <div class="box_c_content">
                    <asp:Panel ID="Panel1" runat="server" DefaultButton="btnPayment">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <div class="row">
                                    <div style="width: 100%">
                                        <table style="display: table; margin: 0px auto;">
                                          <tr>
                                         <td>
                                                    <asp:Label ID="Label1" runat="server" Text="Chit Group"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList onchange="clearValidationErrors();" TabIndex="3" Width="240px" CssClass="chzn-select" ID="ddlGroupNumber"
                                                        runat="server" AutoPostBack="true" OnSelectedIndexChanged="load_ddlGroupNumber" >
                                                    </asp:DropDownList>
                                                    <asp:CompareValidator ID="CompareValidator11" ValueToCompare="0" ValidationGroup="pa"
                                                        ControlToValidate="ddlGroupNumber" Display="Dynamic" Operator="NotEqual" runat="server"
                                                        ErrorMessage="Select Chit Group"></asp:CompareValidator>
                                                </td>
                                          </tr>
                                         <tr>
                                         <td>
                                                    <asp:Label ID="Label5" runat="server" Text="Re-Action No."></asp:Label>
                                                </td>
                                                <td>
                                                     <asp:TextBox AutoPostBack="true" TabIndex="4" CssClass="input-text sp_float"
                                                        ToolTip="Ex. 10" placeholder="Action No" ID="txtReauctionNo" 
                                                        runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ControlToValidate="txtReauctionNo" Display="Dynamic"
                                                        ValidationGroup="pa" ID="RequiredFieldValidator5" runat="server" ErrorMessage="Enter Auction No."></asp:RequiredFieldValidator>
                                                </td>
                                          </tr>
                                          <tr>
                                          <td>
                                                    <asp:Label ID="Label2" runat="server" Text="Member Name"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList  onchange="clearValidationErrors();"  TabIndex="5" AutoPostBack="true" 
                                                        Width="240px" CssClass="chzn-select" ID="ddlMemberName" runat="server">
                                                    </asp:DropDownList>
                                                    <asp:CompareValidator ID="CompareValidator10" ValueToCompare="--Select--" ControlToValidate="ddlMemberName"
                                                        ValidationGroup="pa" Display="Dynamic" Operator="NotEqual" runat="server" ErrorMessage="Select Member Name"></asp:CompareValidator>
                                                </td>
                                          </tr>
                                         <tr>
                                          <td style="padding-right: 5px;">
                                                    <asp:Label ID="label15" runat="server" Text="Prized Amount"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox AutoPostBack="true" TabIndex="6" CssClass="input-text sp_float"
                                                        ToolTip="Ex. 1000.00" placeholder="Amount" ID="txtAmount"
                                                        runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ControlToValidate="txtAmount" Display="Dynamic"
                                                        ValidationGroup="pa" ID="RequiredFieldValidator3" runat="server" ErrorMessage="Enter Prized Amount"></asp:RequiredFieldValidator>
                                                </td>
                                         </tr>
                                         <tr>
                                          <td style="padding-right: 5px;">
                                                    <asp:Label ID="label6" runat="server" Text="Total Commision"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox AutoPostBack="true" TabIndex="6" CssClass="input-text sp_float"
                                                        ToolTip="Ex. 1000.00" placeholder="Commission" ID="TxtCommission"
                                                        runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ControlToValidate="TxtCommission" Display="Dynamic"
                                                        ValidationGroup="pa" ID="RequiredFieldValidator6" runat="server" ErrorMessage="Enter Commission Amount"></asp:RequiredFieldValidator>
                                                </td>
                                         </tr>
                                         <tr>
                                          <td style="padding-right: 5px;">
                                                    <asp:Label ID="label4" runat="server" Text="Kasar Amount"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox AutoPostBack="true" TabIndex="7" CssClass="input-text sp_float"
                                                        ToolTip="Ex. 1000.00" placeholder="Amount" ID="txtkasarAmount"
                                                        runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ControlToValidate="txtkasarAmount" Display="Dynamic"
                                                        ValidationGroup="pa" ID="RequiredFieldValidator4" runat="server" ErrorMessage="Enter Kasar Amount"></asp:RequiredFieldValidator>
                                                </td>
                                         </tr>
                                         <tr>
                                         <td>
                                                    <asp:Label ID="label8" runat="server" Text="Date"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtDate" TabIndex="8" CssClass="input-text maskdate" placeholder="Date"
                                                        runat="server">
                                                    </asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtDate" runat="server" 
                                                     Display="Dynamic"  ValidationGroup="pa" ErrorMessage="Enter Redraw Date">
                                                    </asp:RequiredFieldValidator>
                                                      <asp:CompareValidator ForeColor="Red" Type="Date" ID="CompareValidator1" ValidationGroup="pa"
                                                        ControlToValidate="txtDate" Display="Dynamic" Operator="DataTypeCheck" runat="server"
                                                        ErrorMessage="Incorrect!!!"></asp:CompareValidator>
                                                    <asp:RangeValidator ID="rvDate" runat="server" Type="Date" ControlToValidate="txtDate" ValidationGroup="pa"
                                                        ForeColor="Red" Display="Dynamic" ErrorMessage="*"></asp:RangeValidator>
                                     
                                                  </td>
                                         </tr>

                                         <tr>
                                         <td>
                                          <asp:Label ID="label3" runat="server" Text="Narration"></asp:Label>
                                         </td>
                                         <td>
                                             <asp:TextBox ID="txtNarration" runat="server" TabIndex="9" CssClass="input-text maskdate" TextMode="MultiLine"></asp:TextBox>
                                             <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                              ControlToValidate="txtNarration" Display="Dynamic" ValidationGroup="pa" ErrorMessage="Enter Narration"></asp:RequiredFieldValidator>
                                         </td>
                                         </tr>
                                        </table>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </asp:Panel>
                    <div id="Div1" runat="server" style="margin: 0px auto; display: table;">
                        <asp:Button TabIndex="7" CausesValidation="true" ID="btnPayment" runat="server"
                            ValidationGroup="pa" CssClass="GreenyPushButton"  Text="Add" 
                            onclick="btnPayment_Click">
                        </asp:Button>
                        <asp:Button TabIndex="8" Style="margin-left: 10px;" CausesValidation="false" ID="btnCancel"
                            runat="server" CssClass="GreenyPushButton" OnClientClick="clearValidationErrors();"
                           Text="Cancel" onclick="btnCancel_Click"></asp:Button>
                    </div>
                    <ajax:ModalPopupExtender ID="ModalPopupExtender1" TargetControlID="show" PopupControlID="pnlmsg"
                        BackgroundCssClass="modalBackground" runat="server">
                    </ajax:ModalPopupExtender>
                    <asp:LinkButton ID="show" runat="server"></asp:LinkButton>
                    <asp:Panel CssClass="raised" ID="pnlmsg" runat="server" Visible="false" Width="350px"
                        Style="min-height: 100px">
                        <div style="top: 0px; height: 50px; text-align: center; padding: auto 0; width: 100%"
                            class="boxheader">
                            <asp:Label runat="server" ID="lblh" Text=""> </asp:Label>
                        </div>
                        <div style="min-height: 100px; text-align: center;">
                            <br />
                            <br />
                            <asp:Label runat="server" ID="lblcon" Text=""> </asp:Label>
                            <br />
                            <br />
                        </div>
                        <div class="boxheader">
                            <div style="margin: 0 auto;">
                                <asp:Button Style="margin: 0 auto" CssClass="GreenyPushButton" 
                                    ID="btnOK" CausesValidation="false" OnClientClick="clearValidationErrors();"
                                    runat="server" Text="Yes" onclick="btnOK_Click" />
                            </div>
                        </div>
                    </asp:Panel>
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
    </script>
</asp:Content>
