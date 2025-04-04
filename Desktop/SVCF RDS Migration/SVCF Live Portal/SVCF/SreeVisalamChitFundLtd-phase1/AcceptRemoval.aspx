<%@ Page Title="SVCF Admin Panel" Culture="en-GB" Language="C#" MasterPageFile="~/Branch.Master"
    AutoEventWireup="true" CodeBehind="AcceptRemoval.aspx.cs" Inherits="SreeVisalamChitFundLtd_phase1.AcceptRemoval" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <link href="Styles/aspxtable.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .aligned td
        {
            padding-left: 10px;
            padding-right: 10px;
            text-align: center;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMainContent" runat="server">
    <ajax:ToolkitScriptManager ID="ToolkitScriptManager2" runat="server">
    </ajax:ToolkitScriptManager>
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
                        Removal Approval</p>
                </div>
                <div class="box_c_content">
                    <div class="row">
                        <table class="aligned" style="margin: 0px auto;">
                            <tr>
                                <td>
                                    <asp:Label ID="Label1" runat="server" Text="Select Chit Group"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList Width="200" ID="ddlChitgroupNo" CssClass="chzn-select" runat="server">
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <asp:Button ID="btnLoad" runat="server" CssClass="GreenyPushButton" Text="Load" Height="28px"
                                        OnClick="btnLoad_Click" Width="95px"></asp:Button>
                                </td>
                                <td>
                                    <asp:Label ID="lblSearch" Text="Search Text :" runat="server"> </asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtSearch" runat="server" 
                                        CssClass="twitterStyleTextbox "></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Label ID="lblNoRecords" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </table>
                        <div class="twelve columns centered">
                            <div style="margin: 0px auto; width: 100%;overflow:auto !important;">
                                <asp:GridView CssClass="aspxtable" GridLines="Both" ID="GridView1" DataKeyNames="BranchId,ChitNO,OldMemberID,GroupMemberID,NewMemberID,DateOfRemoval,ReasonForRemoval,1stNoticeDate,2ndNoticeDate,commision,CurrentDrawNumber,EstimatedDrawNumber,EstimatedSurityDetails,M_ID,B_ID,Flag,SNo,removalamount,kasar,monthlyincome,profession"
                                    AutoGenerateColumns="false" Style="margin: 0px auto; display: table-cell;" runat="server"
                                    ShowFooter="True" ForeColor="#333333">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Approve">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="btnReject" runat="server" CausesValidation="false" OnClick="Approve_Click"
                                                    ImageUrl="~/Images/Approve.png" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField Visible="true" HeaderText="Reject">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="btnRejectorig" runat="server" CausesValidation="false" OnClick="dis_Approve_Click"
                                                    ImageUrl="~/Images/Reject.png" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Audit">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="btnAudit" runat="server" CausesValidation="false" OnClick="Audit_Click"
                                                    ImageUrl="~/Images/Audit.png" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="Branch Name" DataField="BranchName" />
                                        <asp:BoundField Visible="false" HeaderText="Group" DataField="GROUPNO" />
                                        <asp:BoundField HeaderText="Token" DataField="GrpMemberID" />
                                        <asp:BoundField HeaderText="New Member Name" DataField="NewMember" />
                                        <asp:BoundField HeaderText="Old Member Name" DataField="OldMember" />
                                        <asp:BoundField HeaderText="Money Collector" DataField="moneycollname" />
                                        <asp:BoundField HeaderText="Commision" DataField="commision" />
                                        <asp:BoundField HeaderText="Kasar Amount" DataField="kasar" />
                                        <asp:BoundField HeaderText="Profession Business" DataField="profession" />
                                        <asp:BoundField HeaderText="Removal Amount" DataField="removalamount" />
                                        <asp:BoundField HeaderText="Monthly Income" DataField="monthlyincome" />
                                        <asp:BoundField HeaderText="Estimated Surity Details" DataField="EstimatedSurityDetails" />
                                        <asp:BoundField HeaderText="Member's Branch Name" DataField="MembersBranchName" />
                                        <asp:BoundField HeaderText="Reason for Removal" DataField="ReasonForRemoval" />
                                    </Columns>
                                    <FooterStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <RowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                    <%--</div>--%>
                </div>
                <ajax:ModalPopupExtender ID="ModalPopupExtender1" TargetControlID="show" PopupControlID="msgbox"
                    BackgroundCssClass="modalBackground" runat="server">
                </ajax:ModalPopupExtender>
                <asp:LinkButton ID="show" runat="server"></asp:LinkButton>
                <div id="msgbox" style="display: none; background: #fff; border: 4px solid #ccc;
                    border: 8px solid rgba(0, 0, 0, 0.5); background-clip: padding-box; -webkit-border-radius: 10px;
                    -moz-border-radius: 10px; border-radius: 10px; min-width: 300px; max-width: 900px;"
                    cssclass="raised" runat="server">
                    <div class=" box_c_heading">
                        <span class="inner_heading" style="text-align: center;">Confirmation </span>
                    </div>
                    <div style="min-height: 20px; max-height: 200px; overflow: hidden; max-width: 880px;
                        padding: 20px;">
                        <div style="min-height: 20px; overflow: auto; max-width: 840px;">
                            <asp:Label runat="server" Style="display: none" ID="lblHint"></asp:Label>
                            <asp:Label runat="server" ID="lblMsgInfoContent"></asp:Label>
                            <br />
                            <br />
                            <table>
                                <tr>
                                    <td style="vertical-align: middle; padding-right: 5px;">
                                        <asp:Label runat="server" Text="Suggession if any" ID="lblSuggession"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox Width="230px" Height="80px" runat="server" ID="txtSuggession" TextMode="MultiLine"
                                            CssClass="input-text"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                            <br />
                        </div>
                    </div>
                    <div class=" box_c_heading">
                        <div style="float: right;">
                            <asp:Button ID="btnInfo_yes" CssClass="GreenyPushButton" OnClick="btnInfo_yes_Click"
                                runat="server" Style="margin: 0px auto;" Text="Yes"></asp:Button>
                            <asp:Button ID="Button1" CssClass="GreenyPushButton" Style="margin: 0px auto;" runat="server"
                                Text="Exit" OnClick="exit_Click"></asp:Button>
                        </div>
                    </div>
                </div>
                <asp:Panel ID="pnAudit" runat="server" Style="display: none; background: #fff; border: 4px solid #ccc;
                    border: 8px solid rgba(0, 0, 0, 0.5); background-clip: padding-box; -webkit-border-radius: 10px;
                    -moz-border-radius: 10px; border-radius: 10px; min-width: 300px; max-width: 900px;">
                    <div class=" box_c_heading">
                        <span style="text-align: center;">Audit</span>
                    </div>
                    <div style="min-height: 20px; overflow: hidden; max-width: 880px; padding: 20px;">
                        <asp:GridView CssClass="aspxtable" runat="server" ID="gvAudit">
                        </asp:GridView>
                    </div>
                    <div class="box_c_heading">
                        <div style="float: right;">
                            <asp:Button ID="btnOk" CssClass="GreenyPushButton" OnClick="btnOk_Click" runat="server"
                                Text="Yes"></asp:Button>
                        </div>
                    </div>
                </asp:Panel>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        $(document).ready(function () {
            $(".chzn-select").chosen({ search_contains: true });
        });
            </script>
</asp:Content>
