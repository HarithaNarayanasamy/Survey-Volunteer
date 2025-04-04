<%@ Page Language="C#" MasterPageFile="~/Branch.Master" AutoEventWireup="true" CodeBehind="MembertoGroupApproval.aspx.cs"
    Culture="en-GB" Inherits="SreeVisalamChitFundLtd_phase1.MembertoGroupApproval"
    Title="SVCF Admin Panel" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Head1" ContentPlaceHolderID="cphHead" runat="server">
    <link href="Styles/aspxtable.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .aligned td
        {
            padding-left: 10px;
            padding-right: 10px;
        }
        #ctl00_cphMainContent_ddlChitGroup_chzn .chzn-drop .chzn-search input[type="text"]
        {
            height: 15px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMainContent" runat="server">
    <ajax:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </ajax:ToolkitScriptManager>
    <asp:Button ID="btnShowPopup" runat="server" Style="display: none" />
    <ajax:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="btnShowPopup"
        PopupControlID="pnlpopup" BackgroundCssClass="modalBackground">
    </ajax:ModalPopupExtender>
    <script type="text/javascript">

        $(document).ready(function () {
            
            gridviewSearch();
        });
        function gridviewSearch() {


            $('#<%=lblNoRecords.ClientID%>').css('display', 'none');
            $('#<%=txtSearch.ClientID%>').keyup(function () {
                $('#<%=lblNoRecords.ClientID%>').css('display', 'none'); // Hide No records to display label.
                $("#<%=GridView1.ClientID%> tr:has(td)").hide(); // Hide all the rows.

                var iCounter = 0;
                var sSearchTerm = $('#<%=txtSearch.ClientID%>').val(); //Get the search box value

                if (sSearchTerm.length == 0) //if nothing is entered then show all the rows.
                {
                    $("#<%=GridView1.ClientID%> tr:has(td)").show();
                    return false;
                }
                //Iterate through all the td.
                $("#<%=GridView1.ClientID%> tr:has(td)").children().each(function () {
                    var cellText = $(this).text().toLowerCase();
                    if (cellText.indexOf(sSearchTerm.toLowerCase()) >= 0) //Check if data matches
                    {
                        $(this).parent().show();
                        iCounter++;
                        return true;
                    }
                });
                if (iCounter == 0) {
                    $('#<%=lblNoRecords.ClientID%>').css('display', '');
                }
                e.preventDefault();
            })

        } 
    </script>
    <div class="row">
        <div class="twelve columns">
            <div class="box_c">
                <div class="box_c_heading  box_actions">
                    <p>
                        Member To Group Approval</p>
                </div>
                <div class="box_c_content">
                    <div class="row">
                        <br />
                        <div style="float: left; padding: 5px;">
                            <asp:Label ID="Label1" Text="Chit Group :" runat="server"> </asp:Label>
                            <asp:DropDownList Width="150" ID="ddlChitGroup" runat="server" CssClass="chzn-select">
                            </asp:DropDownList>
                        </div>
                        <div style="margin-top: 12px; float: left; padding: 10px 10px 0px 10px; vertical-align: bottom;">
                            <asp:Button OnClick="btnLoad_Click" CssClass="GreenyPushButton" ID="btnLoad" runat="server"
                                Text="Load Members"></asp:Button>
                        </div>
                        <div style="float: left; padding: 5px">
                            <asp:Label ID="lblSearch" Text="Search Text :" runat="server"> </asp:Label><br />
                            <asp:TextBox ID="txtSearch" runat="server" 
                                CssClass="imput-text"></asp:TextBox>
                            <asp:Label ID="lblNoRecords" runat="server"></asp:Label>
                        </div>
                    </div>
                    <br />
                    <div class="twelve columns centered">
                        <div style="margin: 0px auto;width:100%;overflow:auto !important;">
                            <asp:GridView ID="GridView1" CssClass="aspxtable" runat="server" AutoGenerateColumns="false"
                                DataKeyNames="MemberID,GroupID,slno,NoofRemainingTokens" GridLines="Vertical"
                                Style="margin: 0px auto; display: table-cell;" ShowFooter="True">
                                <Columns>
                                    <asp:TemplateField HeaderText="Approve">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnReject" runat="server" CausesValidation="false" OnClick="Approve_Click"
                                                ImageUrl="~/Images/like.jpg" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Reject">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnRejectorig" runat="server" CausesValidation="false" OnClick="dis_Approve_Click"
                                                ImageUrl="~/Images/unlike.jpg" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField Visible="false" HeaderText="Audit">
                                        <ItemTemplate>
                                            <asp:ImageButton Visible="false" ID="btnAudit" runat="server" CausesValidation="false"
                                                OnClick="Audit_Click" ImageUrl="~/Images/audit32.png" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="Suggested Branch" DataField="SuggestedBranch" />
                                    <asp:BoundField HeaderText="Branch Name" DataField="BranchName" />
                                    <asp:BoundField HeaderText="Customer Name" DataField="CustomerName" />
                                    <asp:BoundField HeaderText="Group" DataField="GROUPNO" />
                                    <asp:BoundField HeaderText="Estimated Surety Document" DataField="EstSuretyDocument" />
                                    <asp:BoundField HeaderText="No.of Tokens" DataField="NoofTokens" />
                                    <asp:BoundField HeaderText="No.of Tokens Approved" DataField="NoofTokensApproved" />
                                    <asp:BoundField HeaderText="Suggested Date" DataField="SuggestedDate" />
                                    <asp:BoundField HeaderText="Profession Business" DataField="ProfessionBusiness" />
                                    <asp:BoundField HeaderText="Residential Address" DataField="ResidentialAddress" />
                                    <asp:BoundField HeaderText="Money Collector" DataField="moneycollname" />
                                </Columns>
                                <FooterStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                <HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                <RowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div id="msgboxNoTokens" style="display: none; background: #fff; border: 4px solid #ccc;
        border: 8px solid rgba(0, 0, 0, 0.5); background-clip: padding-box; -webkit-border-radius: 10px;
        -moz-border-radius: 10px; border-radius: 10px; min-width: 300px; max-width: 900px;"
        cssclass="raised" runat="server">
        <div class=" box_c_heading">
            <asp:Label CssClass="inner_heading" runat="server" ID="Label2" Text="CONFIRMATION"> </asp:Label>
        </div>
        <div style="min-height: 20px; overflow: hidden; max-width: 880px; padding: 20px;">
            <div style="min-height: 20px; overflow: auto; max-width: 840px;">
                <asp:Label runat="server" Style="display: none" ID="lblHintNoofTokens"></asp:Label>
                <asp:Label runat="server" Text="Please Enter No of tokens do you Want to Approve ?"
                    ID="lblMsgInfoContentNoofTokens"></asp:Label>
                <br />
                <table>
                    <tr>
                        <td>
                            <asp:Label runat="server" Text="Tokens"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox runat="server" ValidationGroup="not" CssClass="sp_number" ID="txtApprovedNumberofTokens"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="CompareValidator1" runat="server" ValidationGroup="not"
                                ErrorMessage="*" ControlToValidate="txtApprovedNumberofTokens" ForeColor="Red"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <br />
                            <asp:Label ID="Label3" runat="server" Text="Suggession(if any)"></asp:Label>
                        </td>
                        <td>
                            <br />
                            <asp:TextBox TextMode="MultiLine" Height="60" Width="200" runat="server" ValidationGroup="not"
                                ID="txtSuggession"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <div class=" box_c_heading">
            <div style="float: right;">
                <asp:Button ID="btnInfo_yesnot" CausesValidation="true" ValidationGroup="not" CssClass="GreenyPushButton"
                    OnClick="btnInfo_yes_Click" runat="server" Text="Yes"></asp:Button>
                <asp:Button ID="Button1not" CausesValidation="false" CssClass="GreenyPushButton"
                    OnClick="btnInfo_no_Click" runat="server" Text="No"></asp:Button>
            </div>
        </div>
    </div>
    <div id="msgbox" style="display: none; background: #fff; border: 4px solid #ccc;
        border: 8px solid rgba(0, 0, 0, 0.5); background-clip: padding-box; -webkit-border-radius: 10px;
        -moz-border-radius: 10px; border-radius: 10px; min-width: 300px; max-width: 900px;"
        cssclass="raised" runat="server">
        <%-- <asp:Label runat="server" ID="lblHint" Text="" Visible="false"> </asp:Label>--%>
        <div class=" box_c_heading">
            <asp:Label CssClass="inner_heading" runat="server" ID="lblHD" Text="CONFIRMATION"> </asp:Label>
        </div>
        <div style="min-height: 20px; overflow: hidden; max-width: 880px; padding: 20px;">
            <div style="min-height: 20px; overflow: auto; max-width: 840px;">
                <asp:Label runat="server" Style="display: none" ID="lblHint"></asp:Label>
                <asp:Label CssClass="inner_heading" runat="server" ID="lblMsgInfoContent"></asp:Label>
                <br />
                <%--<span class="inner_heading" >Please Provide the Reason(if Any) </span>--%>
                <asp:TextBox runat="server" placeholder="Reason (if any)" Width="150" TextMode="MultiLine"
                    Height="50" ID="txtReasonForRejection"></asp:TextBox>
            </div>
        </div>
        <div class=" box_c_heading">
            <div style="float: right;">
                <asp:Button ID="btnInfo_yes" CssClass="GreenyPushButton" OnClick="btnInfo_yes_Click"
                    runat="server" Text="Yes"></asp:Button>
                <asp:Button ID="Button1" CssClass="GreenyPushButton" OnClick="btnInfo_no_Click" runat="server"
                    Text="No"></asp:Button>
            </div>
        </div>
    </div>
    <%--     <div id="msgbox" runat="server" style="display: none;">
                    <div style="background: #6c6e74; background: -moz-linear-gradient(top,  #6c6e74 0%, #4b4d51 100%);
                        background: -webkit-gradient(linear, left top, left bottom, color-stop(0%,#6c6e74), color-stop(100%,#4b4d51));
                        background: -webkit-linear-gradient(top,  #6c6e74 0%,#4b4d51 100%); background: -o-linear-gradient(top,  #6c6e74 0%,#4b4d51 100%);
                        background: -ms-linear-gradient(top,  #6c6e74 0%,#4b4d51 100%); background: linear-gradient(top,  #6c6e74 0%,#4b4d51 100%);
                        box-shadow: -1px 2px 3px rgba(0,0,0,0.5); height: 50px; color: White;">
                        <span style="text-align: center;">Confirmation </span>
                        <div style="float: right;">
                            <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="Images/Close.gif" /></div>
                    </div>
                    <div id="cnt1" runat="server" class="info" style="padding: 20px; min-height: 200px;
                        max-height: 500px; min-width: 500px;">
                        <asp:Label runat="server" Style="display: none" ID="lblHint"></asp:Label>
                        <asp:Label runat="server" ID="lblMsgInfoContent"></asp:Label>
                        <br />
                        <asp:TextBox runat="server" Width="150" TextMode="MultiLine" Height="50" ID="txtReasonForRejection"></asp:TextBox>
                        <div runat="server" id="Div1" style="float: right; bottom: 0px; position: absolute;
                            right: 0px;">
                            <asp:Button ID="btnInfo_yes" CssClass="GreenyPushButton" OnClick="btnInfo_yes_Click"
                                runat="server" Text="Yes"></asp:Button>
                            <asp:Button ID="Button1" CssClass="GreenyPushButton" OnClick="btnInfo_no_Click" runat="server"
                                Text="No"></asp:Button>
                        </div>
                    </div>
                </div>--%>
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
        });

    </script>
</asp:Content>
