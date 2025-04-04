<%@ Page Title="SVCF Admin Panel" Culture="en-GB" Language="C#" MasterPageFile="~/Branch.Master"
    AutoEventWireup="true" CodeBehind="acceptprofitloss.aspx.cs" Inherits="SreeVisalamChitFundLtd_phase1.acceptprofitloss" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <link href="Styles/aspxtable.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        td
        {
            text-align: left;
        }
        .aligned td
        {
            padding-left: 10px;
            padding-right: 10px;
        }
        .chzn-results
        {
            text-align: center;
        }
        #ctl00_cphMainContent_ddlStatus_chzn .chzn-drop .chzn-search input[type="text"]
        {
            height: 15px;
        }
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMainContent" runat="server">
    <ajax:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </ajax:ToolkitScriptManager>
    <asp:Button ID="btnShowPopup" runat="server" Style="display: none" />
    <ajax:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="btnShowPopup"
        PopupControlID="PnlApprove" BackgroundCssClass="modalBackground">
    </ajax:ModalPopupExtender>
    <script type="text/javascript">
        $(document).ready(function () {
            gridviewScroll();
            gridviewSearch();
        });

        function gridviewScroll() {
            $('#<%=GridView1.ClientID%>').gridviewScroll({
                width: $(window).width() - 370,
                height: $(window).height() - 380,
                freezesize: 0,
                arrowsize: 30,
                headerrowcount: 1
            });
        }

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
                <div class="box_c_heading cf">
                    <p class="sepV_a">
                        Accept Profit and Loss</p>
                </div>
                <div class="box_c_content">
                    <div class="row">
                        <div class="row display">
                            <div style="margin: 0px auto;">
                                <div style="display: table-cell; vertical-align: top; padding-top: 10px; padding-right: 5px !important;">
                                    <asp:Label ID="Label1" Text="Select Status :" runat="server"> </asp:Label>
                                </div>
                                <div style="display: table-cell; vertical-align: top; padding-top: 5px; padding-right: 5px !important;">
                                    <asp:DropDownList Width="160px" ID="ddlStatus" runat="server" CssClass="chzn-select">
                                        <asp:ListItem Text="Waiting" Value="Waiting"></asp:ListItem>
                                        <asp:ListItem Text="Accepted" Value="Accepted"></asp:ListItem>
                                        <%--<asp:ListItem Text="Rejected" Value="Rejected"></asp:ListItem>--%>
                                    </asp:DropDownList>
                                </div>
                                <div style="display: table-cell; vertical-align: top; padding-top: 10px; padding-right: 5px !important;">
                                    <asp:Label ID="Label6" Text="Applied Date :" runat="server"> </asp:Label>
                                </div>
                                <div style="display: table-cell; vertical-align: top; padding-top: 5px; padding-right: 5px !important;">
                                    <asp:TextBox runat="server" ID="txtDate" CssClass="twitterStyleTextbox maskdate"></asp:TextBox>
                                    <asp:RequiredFieldValidator ForeColor="Red" ID="RequiredFieldValidator6" ControlToValidate="txtDate"
                                        Display="Dynamic" runat="server"></asp:RequiredFieldValidator>
                                    <asp:CompareValidator ForeColor="Red" Type="Date" ID="CompareValidator1" ControlToValidate="txtDate"
                                        Display="Dynamic" Operator="DataTypeCheck" runat="server"></asp:CompareValidator>
                                </div>
                                <div style="display: table-cell; vertical-align: top; padding-top: 5px; padding-right: 5px !important;">
                                    <asp:Button CssClass="GreenyPushButton" OnClick="btnLoad_OnClick" ID="btnLoad" runat="server"
                                        Text="Load"></asp:Button>
                                </div>
                                <div style="display: table-cell; vertical-align: top; padding-top: 10px; padding-right: 5px !important;">
                                    <asp:Label ID="lblSearch" Text="Search Text :" runat="server"> </asp:Label>
                                </div>
                                <div style="display: table-cell; vertical-align: top; padding-top: 5px; padding-right: 5px !important;">
                                    <asp:TextBox ID="txtSearch" runat="server" CssClass="twitterStyleTextbox "></asp:TextBox>
                                </div>
                                <div style="display: table-cell; vertical-align: top; padding-top: 10px; padding-right: 5px !important;">
                                    <asp:Label ID="lblNoRecords" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="twelve columns centered">
                                <div style="margin: 0px auto; display: table;">
                                    <asp:GridView ID="GridView1" CssClass="aspxtable"
                                        runat="server" AutoGenerateColumns="false" GridLines="Vertical" OnRowDataBound="GridView1_OnRowDataBound" DataKeyNames="BranchID,DualTransactionKey,B_Name,Amount,Applied,Narration,Flag,ano"
                                        Width="100%" Style="margin: 0px auto; display: table-cell;" ShowFooter="True">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Action">
                                                <ItemTemplate>
                                                    <asp:ImageButton ToolTip="View" Height="20px" Width="30px" ID="btnview" runat="server"
                                                        CausesValidation="false" OnClick="btnview_OnClick" ImageUrl="~/Images/eye-24-512.png" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField HeaderText="Heads" DataField="Node" />
                                            <asp:BoundField HeaderText="Branch Name" DataField="B_Name" />
                                            <asp:BoundField HeaderText="Amount" DataField="Amount" />
                                            <asp:BoundField HeaderText="Applyed Date" DataField="Applied" />
                                            <asp:BoundField HeaderText="Narration" DataField="Narration" />
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
        </div>
        <asp:Panel CssClass="raised" ID="PnlApprove" runat="server" Visible="false" Width="600px"
            Style="max-height: 500px; min-height: 100px">
            <asp:Label runat="server" ID="lblHint" Text="" Visible="false"> </asp:Label>
            <div style="top: 0px; height: 50px; text-align: center; padding: auto 0; width: 100%"
                class="boxfooter">
                <asp:Label ID="lblT" runat="server" Text="Status"> </asp:Label>
            </div>
            <div id="Div1" style="max-height: 300px; min-height: 100px; width: 100%; text-align: center;">
                <br />
                <br />
                <asp:Label ID="lblContent" runat="server"> </asp:Label>
                <br />
                <br />
                <asp:PlaceHolder ID="DynamicControlsHolder" runat="server"></asp:PlaceHolder>
            </div>
            <div class="boxheader">
                <asp:Button Style="margin: 0 auto" OnClick="btnMsgOK_Click" CssClass="GreenyPushButton"
                    ID="btnMsgOK" runat="server" Text="OK" />
                <asp:Button Style="margin: 0 auto" OnClick="btnMsgCancel_Click" CssClass="GreenyPushButton"
                    ID="btnMsgCancel" runat="server" Text="Cancel" />
            </div>
        </asp:Panel>
        <asp:Panel CssClass="raised" ID="pnlfinal" runat="server" Visible="false" Width="600px"
            Style="max-height: 500px; min-height: 100px">
            <asp:Label runat="server" ID="Label2" Text="" Visible="false"> </asp:Label>
            <div style="top: 0px; height: 50px; text-align: center; padding: auto 0; width: 100%"
                class="boxfooter">
                <asp:Label ID="Label3" runat="server" Text="Status"> </asp:Label>
            </div>
            <div id="Div2" style="max-height: 300px; min-height: 100px; width: 100%; text-align: center;">
                <br />
                <br />
                <asp:Label Text="P & L Approve Number" runat="server"> </asp:Label>
                <asp:TextBox runat="server" ID="txtApprove"></asp:TextBox>
                <asp:RequiredFieldValidator ValidationGroup="a" Display="Dynamic" ForeColor="Red" ErrorMessage="Enter Approve Number" runat="server" ControlToValidate="txtApprove" ></asp:RequiredFieldValidator>
                <br />
                <br />
                <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
            </div>
            <div class="boxheader">
                <asp:Button ValidationGroup="a" Style="margin: 0 auto" OnClick="btnOK_Click" CssClass="GreenyPushButton"
                    ID="Button1" runat="server" Text="OK" />
                <asp:Button Style="margin: 0 auto" OnClick="btnMsgCancel_Click" CssClass="GreenyPushButton"
                    ID="Button2" runat="server" Text="Cancel" />
            </div>
        </asp:Panel>
    </div>
    <script type="text/javascript">
        $(document).ready(function () {
            $(".chzn-select").chosen({ search_contains: true });
            prth_mask_input.init();
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
        });
    </script>
</asp:Content>
