<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Approvedvoucher.ascx.cs" Inherits="SreeVisalamChitFundLtd_phase1.UserControl.WebUserControl1" %>

<style>

</style>
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
    <%--<ajax:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </ajax:ToolkitScriptManager>--%>

<style>
    
   /*.table tr {
    transition: background 0.2s ease-in;
}

.table tr:nth-child(odd) {
    background: #f9fbe7;
    margin:20px;
}

.table tr:hover {
    background: silver;
    cursor: pointer;
    margin:20px;
}*/

   .table tr:hover:not(:first-child) {
    background-color: silver;
}

</style>








<div class="twelve columns centered">
    <asp:GridView ID="Gd_ViewVoucher" CssClass="Grid" runat="server"  AutoGenerateColumns="false" Width="100%" CellSpacing="2" BorderStyle="none" GridLines="Both" BorderColor="White" BorderWidth="1px" CellPadding="2" 
        DataKeyNames="DualKey,Series,ApprovedDate,AppReceiptno,Head_Id" >
        <HeaderStyle HorizontalAlign="center" Height="30px" BackColor="Black" ForeColor="WhiteSmoke" />
        <EditRowStyle HorizontalAlign="Justify" BorderColor="Black" BorderWidth="1px" BorderStyle="Solid" />
        <%--<Columns>
            <asp:BoundField HeaderText="BranchName" DataField="BranchName" />
            <asp:BoundField HeaderText="ApprovedDate" DataField="ApprovedDate" />
            <asp:BoundField HeaderText="ChequeDate" DataField="ChequeDate" />
            <asp:BoundField HeaderText="ChequeDDNO" DataField="ChequeDDNO" />
            <asp:BoundField HeaderText="Choosendate" DataField="Choosendate" />
            <asp:BoundField HeaderText="GrpMemberID" DataField="GrpMemberID" />
            <asp:BoundField HeaderText="Head_Id" DataField="Head_Id" />
            <asp:BoundField HeaderText="ReceiptNumber" DataField="ReceiptNumber" />
            <asp:BoundField HeaderText="Amount" DataField="Amount" />
            <asp:BoundField HeaderText="Description" DataField="Description" />
            <asp:BoundField HeaderText="AppReceiptno" DataField="AppReceiptno" />            
        </Columns>--%>
        <Columns>
            <asp:BoundField HeaderText="Branch Name" DataField="BranchName" HeaderStyle-Width="10%" />
            <asp:BoundField HeaderText="Approved Date" DataField="ApprovedDate" HeaderStyle-Width="9%" />
            <asp:BoundField HeaderText="Date of Remitted By Subscriber" DataField="RemittanceDate" HeaderStyle-Width="12%" />
            <asp:BoundField HeaderText="Chit No" DataField="ChitNo" HeaderStyle-Width="8%"/>
            <asp:BoundField HeaderText="Receipt Number" DataField="AppReceiptno" HeaderStyle-Width="11%"/>
            <asp:BoundField HeaderText="Current Branch Amount" DataField="CurrentBranchAmount" HeaderStyle-Width="5%"/>
            <asp:BoundField HeaderText="Other Branch Amount" DataField="OtherBranchAmount" HeaderStyle-Width="5%"/>
            <%--06/12/2021--%>
            <%--<asp:BoundField HeaderText="P&L" DataField="P&L" />--%>
            <asp:BoundField HeaderText="Narration" DataField="Narration" HeaderStyle-Width="38%"/>
            
            <%--12/08/2021- Reject/Cancel a receipt --%>
            <asp:TemplateField HeaderText="Cancel" Visible="false" HeaderStyle-Width="2%">
                <ItemTemplate>
                    <asp:ImageButton ID="btnCancel" runat="server" ImageUrl="~/Images/Reject.png" OnClick="btnCancel_Click" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</div>
<br />
<%--12/08/2021 --%>
<ajax:ModalPopupExtender ID="modalPopupExtender1" Enabled="false" PopupControlID="msgbox" TargetControlID="show" BehaviorID="mpe2" runat="server" BackgroundCssClass="modalBackground">
</ajax:ModalPopupExtender>
<asp:LinkButton ID="show" runat="server"></asp:LinkButton>
<div id="msgbox" style="display: none; background: #fff; border: 4px solid #ccc; border: 8px solid rgba(0, 0, 0, 0.5); background-clip: padding-box; -webkit-border-radius: 10px; -moz-border-radius: 10px; border-radius: 10px; min-width: 300px; max-width: 900px;"
    cssclass="raised" runat="server">
    <div class=" box_c_heading">
        <span class="inner_heading" style="text-align: center;">Confirmation </span>
    </div>
    <div>
        <table>
            <tr>
                <td>
                    <asp:Label ID="lblReceipt" runat="server" Text="AppReceiptno : "></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblReceiptno" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblReason" runat="server" Text="Reason : "></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtReason" runat="server"></asp:TextBox>
                </td>
            </tr>
        </table>
    </div>

    <div style="min-height: 40px; overflow: auto; max-width: 840px;">
        <asp:Label ID="lblContent" runat="server" Text="Are you sure to Cancel this Receipt?"></asp:Label>
    </div>
    <div class="box_c_heading">
        <div style="float: right;">
            <asp:Button ID="btnRejectConfirm" runat="server" CssClass="GreenyPushButton" Text="Reject" CausesValidation="true" OnClick="btnRejectConfirm_Click" />
            <asp:Button ID="btnCancel" runat="server" CssClass="GreenyPushButton" Text="Cancel" OnClick="btnCancel_Click1" />
        </div>

    </div>

</div>

<div id="warning" style="display: none; background: #fff; border: 4px solid #ccc; border: 8px solid rgba(0, 0, 0, 0.5); background-clip: padding-box; -webkit-border-radius: 10px; -moz-border-radius: 10px; border-radius: 10px; min-width: 300px; max-width: 900px;"
    cssclass="raised" runat="server">
    <div class=" box_c_heading">
        <span class="inner_heading" style="text-align: center;">Confirmation </span>
    </div>
    <div style="min-height: 40px; overflow: auto; max-width: 840px;">
        <asp:Label ID="lblWarning" runat="server"></asp:Label>
    </div>
    <div class="box_c_heading">
        <div style="float: right;">
            <asp:Button ID="btnOk" runat="server" CssClass="GreenyPushButton" Text="Ok" OnClick="btnOk_Click" />
        </div>
    </div>

</div>

<div style="float: right;">
    <table>
        <tr>
            <td>
                <asp:Label ID="lblTotal" runat="server" Text="Current Branch Total " Visible="false" Font-Bold="true" Font-Size="Large"></asp:Label>
            </td>
            <td>:</td>
            <td style="text-align:right;">
                <asp:Label ID="lblTotalAmt" runat="server" Visible="false" Font-Bold="true" Font-Size="Large"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label1" runat="server" Text="Other Branch Total " Visible="false" Font-Bold="true" Font-Size="Large"></asp:Label>
            </td>
            <td>:</td>
            <td style="text-align:right;">
                <asp:Label ID="Label2" runat="server" Visible="true" Font-Bold="true" Font-Size="Large"></asp:Label>
            </td>
        </tr>
    </table>
</div>


<link href="../Styles/Stage.css" rel="stylesheet" type="text/css" />
<script src="../Scripts/commonjs.js" type="text/javascript"></script>
<link href="../Styles/ControlsCss.css" rel="stylesheet" type="text/css" />
<link href="../Styles/GridviewScroll.css" rel="stylesheet" type="text/css" />
<link href="../Styles/GVScrolll.css" rel="stylesheet" type="text/css" />
<link href="../Styles/MsgBox.css" rel="stylesheet" type="text/css" />
<link href="../Styles/PopUpMessageBox.css" rel="stylesheet" type="text/css" />
<link href="../Styles/Stage.css" rel="stylesheet" type="text/css" />
<link href="../Styles/tablecss.css" rel="stylesheet" type="text/css" />
<link href="../Styles/Validation.css" rel="stylesheet" type="text/css" />
