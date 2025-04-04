<%@ Page Title="SVCF - Admin Page" Culture="en-GB" Language="C#" MasterPageFile="~/Branch.Master" AutoEventWireup="true" CodeBehind="crrnew.aspx.cs" MaintainScrollPositionOnPostback="true"
    Inherits="SreeVisalamChitFundLtd_phase1.crrnew" EnableSessionState="True" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
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
            height:30px;
            width:100px;
        }

        .btn-custom:hover {
            background-color: #1f72ae;
            color: #FFF;
        }

            .btn-custom:active {
                background-color: #ff3b3b!important;
                color: #fff;
            }

             .btn-custom:focus {
                background-color: #ff3b3b!important;
                color: #fff;
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
                            Cash Receipt Details
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
                                                    <table width="100%">
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label1" runat="server" Text="Collector Name"></asp:Label>
                                                                <br />
                                                                <asp:DropDownList Style="width: 200px !important;" TabIndex="1" ID="ddlColloctorName"
                                                                    CssClass="chzn-select" runat="server"
                                                                    OnChange="GetRSeries();">
                                                                </asp:DropDownList>
                                                            

                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label2" runat="server" Text="Receipt Series"></asp:Label>
                                                                <br />
                                                                <asp:DropDownList Style="width: 150px !important;" ID="ddlReceiptSeries"
                                                                    TabIndex="2" runat="server" OnChange="GetRcptNumber();">
                                                                </asp:DropDownList>
                                                                <asp:HiddenField ID="HD_RSeriesid" runat="server" />

                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label3" runat="server" Text="Received By"></asp:Label>
                                                                <br />
                                                                <asp:DropDownList runat="server" ID="ddlEmployee"  TabIndex="3" OnChange="Getempname();"
                                                                    CausesValidation="false">
                                                                </asp:DropDownList>
                                                                <asp:HiddenField ID="HD_Empname" runat="server" />
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label4" runat="server" Text="Receipt Number"></asp:Label>
                                                                <br />
                                                                <asp:TextBox runat="server" ID="txtReceiptNumber" TabIndex="4"
                                                                    Width="120" CssClass="twitterStyleTextbox sp_number"></asp:TextBox>

                                                            </td>
                                                            <td style="padding: 5px !important;">
                                                                <asp:Label ID="Label6" runat="server" Text="Total Amount"></asp:Label>
                                                                <br />
                                                                <asp:TextBox ID="txtTotalAmount" runat="server" TabIndex="5"
                                                                    Width="120" CssClass="twitterStyleTextbox sp_currency" autocomplete="off"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label5" runat="server" Text="Receipt Date"></asp:Label>
                                                                <br />
                                                                <asp:TextBox ID="txtReceivedDate" Width="90" runat="server" onchange="CheckDate();" ValidationGroup="a"
                                                                    TabIndex="5" autoComplete="off" CssClass="twitterStyleTextbox maskdate"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ForeColor="Red" ID="RequiredFieldValidator6" ErrorMessage="Required!!!"
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
                                        </ContentTemplate>
                                    </asp:UpdatePanel>

                                </div>

                                <div id="divcancel" runat="server" visible="false">
                                    <div class="box_c_ico">
                                        <asp:Image runat="server" ID="Image1" ImageUrl="pertho_admin_v1.3/img/ico/icSw2/16-List.png"
                                            AlternateText="" />
                                    </div>
                                    <p>Cancel Receipt</p>
                                    <asp:ImageButton ID="ImgCancelRcpt" runat="server" ImageUrl="~/Styles/Image/Images/RemoveReceipt.png" OnClick="ImgCancelRcpt_Click" />
                                    <br />
                                    <asp:Label ID="lblcancelmsg" runat="server" Text="" CssClass="lblstyle"></asp:Label>

                                </div>

                                <div class="box_c_heading cf">
                                    <div class="box_c_ico">
                                        <asp:Image runat="server" ID="img16List" ImageUrl="pertho_admin_v1.3/img/ico/icSw2/16-List.png"
                                            AlternateText="" />
                                    </div>
                                    <p>Transactions</p>
                                </div>
                                <div>

                                    <table style="width: 900px;">
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label8" runat="server" Text="Token"></asp:Label>
                                                <br />
                                                <asp:DropDownList ID="ddlTokennew" runat="server" Width="300px" CssClass="chzn-select" TabIndex="7"
                                                    OnChange="GetMemberName();">
                                                </asp:DropDownList>

                                            </td>
                                            <td>
                                                <asp:Label ID="Label7" runat="server" Text="Member Name"></asp:Label>
                                                <br />
                                                <asp:TextBox ID="TxtMemberName" runat="server" Width="300px" TabIndex="8"></asp:TextBox>
                                                <asp:HiddenField ID="hiddenmemberid" runat="server" />
                                            </td>
                                           
                                            <td>
                                                <asp:Label ID="Label9" runat="server" Text="Amount"></asp:Label>
                                                <br />
                                                <asp:TextBox autocomplete="off" TabIndex="9" runat="server"
                                                    CssClass="twitterStyleTextbox sp_currency" ID="txtAmount" Width="300px"></asp:TextBox>
                                            </td>
                                            </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label10" runat="server" Text="Other"></asp:Label>
                                                <br />
                                                <asp:DropDownList TabIndex="10" ID="ddlMisc" CssClass="chzn-select"
                                                    runat="server" OnChange="GetMisc(this)" Width="300px">
                                                </asp:DropDownList>
                                            </td>
                                            
                                            <td>
                                                <asp:Label ID="Label11" runat="server" Text="Misc Amount"></asp:Label>
                                                <br />
                                                <asp:TextBox TabIndex="11" runat="server" CssClass="twitterStyleTextbox sp_currency"
                                                    ID="txtMisc" Width="300px"></asp:TextBox>
                                            </td>
                                           
                                            <td>
                                                 <br />
                                                 <asp:Button ID="ButtonAdd" runat="server" Text="Add"
                                                    TabIndex="12" CssClass="btn-custom" ToolTip="Add" OnClick="ButtonAdd_Click1" />
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <asp:CheckBox Checked="false" Visible="false" ID="CheckBox1" runat="server" />


                                <div>

                                    <asp:GridView ID="GView_Selected" BorderStyle="Solid" runat="server" OnRowDeleting="GView_Selected_RowDeleting"
                                        CellSpacing="11" Font-Names="Verdana" ForeColor="#333333" GridLines="Both" TabIndex="13"
                                        AutoGenerateColumns="false" Width="900px" CssClass="Trans twelve columns">
                                        <RowStyle BackColor="#F7F6F3" />
                                        <RowStyle CssClass="GridViewRowStyle" />
                                        <HeaderStyle CssClass="GridViewHeaderStyle" />
                                        <Columns>

                                            <asp:BoundField HeaderText="RowNumber" DataField="RowNumber" Visible="false"
                                                HeaderStyle-BorderColor="Gray" HeaderStyle-BorderStyle="Solid" HeaderStyle-BorderWidth="2px"
                                                ItemStyle-BorderColor="Gray" ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px" />

                                            <asp:BoundField HeaderText="MemberName" DataField="MemberName"
                                                HeaderStyle-BorderColor="Gray" HeaderStyle-BorderStyle="Solid" HeaderStyle-BorderWidth="2px"
                                                ItemStyle-BorderColor="Gray" ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px" />

                                            <asp:BoundField HeaderText="Token" DataField="Token"
                                                HeaderStyle-BorderColor="Gray" HeaderStyle-BorderStyle="Solid" HeaderStyle-BorderWidth="2px"
                                                ItemStyle-BorderColor="Gray" ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px" />

                                            <asp:BoundField HeaderText="Amount" DataField="Amount"
                                                HeaderStyle-BorderColor="Gray" HeaderStyle-BorderStyle="Solid" HeaderStyle-BorderWidth="2px"
                                                ItemStyle-BorderColor="Gray" ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px" />

                                            <asp:BoundField HeaderText="MiscHead" DataField="MiscHead"
                                                HeaderStyle-BorderColor="Gray" HeaderStyle-BorderStyle="Solid" HeaderStyle-BorderWidth="2px"
                                                ItemStyle-BorderColor="Gray" ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px" />

                                            <asp:BoundField HeaderText="MiscAmount" DataField="MiscAmount" HeaderStyle-BorderColor="Gray" HeaderStyle-BorderStyle="Solid" HeaderStyle-BorderWidth="2px"
                                                ItemStyle-BorderColor="Gray" ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px" />

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


                                            <asp:BoundField HeaderText="ReceiptNo." DataField="RcNumber"
                                                HeaderStyle-BorderColor="Gray" HeaderStyle-BorderStyle="Solid" HeaderStyle-BorderWidth="2px"
                                                ItemStyle-BorderColor="Gray" ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px" />

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

                                            <asp:TemplateField HeaderText="Delete" ItemStyle-BorderColor="Gray" ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px"
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

                                <asp:Button ID="btnGenerate" runat="server" CssClass="btn-custom" CausesValidation="true"
                                        EnableViewState="false"
                                        TabIndex="14" Style="margin: 0px auto; display: table;" ValidationGroup="a" Text="Generate"
                                        OnClick="btnGenerate_Click"></asp:Button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <%-- <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
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
    <asp:Panel Style="background: #fff; border: 4px solid #ccc; border: 8px solid rgba(0, 0, 0, 0.5); background-clip: padding-box; -webkit-border-radius: 10px; -moz-border-radius: 10px; border-radius: 10px; min-width: 300px; max-width: 900px;"
        CssClass="raised" ID="pnlConfirmation"
        runat="server" Visible="false" Width="561px">
        <asp:Label runat="server" ID="lblHintConfirmation" Text="" Visible="false" EnableViewState="false"> </asp:Label>
        <div class=" box_c_heading" style="text-align: center !important;">
            <p>
                <asp:Label runat="server" ID="lblHeadingConfirmation" Text="" EnableViewState="false"> </asp:Label>
            </p>
        </div>
        <div style="min-height: 20px; overflow: hidden; max-width: 880px; padding: 20px;">
            <div style="min-height: 20px; overflow: auto; max-width: 840px;">
                <br />
                <br />
                <asp:Label CssClass="inner_heading" runat="server" Style="margin-top: 10px; font-size: 14px;" EnableViewState="false"
                    ID="lblContentConfirmation" Text="Please Confirm Your Transaction???"> </asp:Label>
                <br />
                <br />
                <asp:GridView ID="gvConfirm" Width="500px" runat="server" AutoGenerateColumns="true"
                    HeaderStyle-BackColor="#61A6F8" ShowFooter="True" HeaderStyle-Font-Bold="true"
                    HeaderStyle-ForeColor="White" PageSize="20" BackColor="White" BorderWidth="1px"
                    CellPadding="4" BorderColor="#DEDFDE" BorderStyle="None" ForeColor="Black"
                    GridLines="None">
                    <RowStyle BackColor="#F7F7DE" />
                    <HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center" CssClass="GridviewScrollC2Header" />
                    <RowStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="GridviewScrollC2Item" />
                    <PagerStyle CssClass="GridviewScrollC2Pager" />
                </asp:GridView>
            </div>
        </div>
        <div class=" box_c_heading">
            <div style="float: right;">
                <asp:Button Style="margin: 0 auto" CssClass="btn-custom" ID="Button1" TabIndex="1" OnClick="btnConfirmationYes_Click" EnableViewState="false"
                    runat="server" Text="yes" />
                <asp:Button Style="margin: 0 auto" CssClass="btn-custom" ID="Button2" TabIndex="2" OnClick="btnConfirmationNo_Click" EnableViewState="false"
                    runat="server" Text="No" />
            </div>
        </div>
    </asp:Panel>
    <asp:Panel ID="Pnlgendrate" Style="background: #fff; border: 4px solid #ccc; border: 8px solid rgba(0, 0, 0, 0.5); background-clip: padding-box; -webkit-border-radius: 10px; -moz-border-radius: 10px; border-radius: 10px; min-width: 300px; max-width: 900px;"
        CssClass="raised" runat="server"
        Visible="false">
        <asp:Label runat="server" ID="lblHint" Text="" Visible="false" EnableViewState="false"> </asp:Label>
        <div class=" box_c_heading">
            <asp:Label CssClass="inner_heading" runat="server" ID="lblHD" Text="" EnableViewState="false"> </asp:Label>
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
                <asp:Button Style="margin: 0 auto" CssClass="btn-custom" ID="btnyes" OnClick="btnyes_Click" EnableViewState="false"
                    runat="server" Text="yes" />
                <asp:Button Style="margin: 0 auto" CssClass="btn-custom" ID="btnNo" OnClick="btnNo_Click" EnableViewState="false"
                    runat="server" Text="No" />
            </div>
        </div>
    </asp:Panel>
    <asp:Panel CssClass="raised" ID="pnlmsg" runat="server" Visible="false" Width="300px"
        Style="min-height: 100px">
        <%--<div  style="background-color:#3979BA;width: 100%; height: 40px;  top: 0px;"  >--%>
        <div style="top: 0px; height: 50px; text-align: center; padding: auto 0; width: 100%"
            class="boxheader">
            <asp:Label runat="server" ID="lblh" Text="" EnableViewState="false"> </asp:Label>
        </div>
        <div style="min-height: 100px;">
            <asp:Label runat="server" ID="lblcon" Text="" EnableViewState="false"> </asp:Label>
        </div>
        <div class="boxheader">
            <div style="margin: 0 auto;">
                <asp:Button Style="margin: 0 auto" CssClass="GreenyPushButton" ID="BtnOK" OnClick="btnOK_Click" EnableViewState="false"
                    runat="server" Text="Ok" />
            </div>
        </div>
    </asp:Panel>





    <asp:LinkButton runat="server" ID="show" Text="" EnableViewState="false"></asp:LinkButton>

    <script type="text/javascript">
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
       
        $(document).ready(function () {
            $(".chzn-select").chosen({ search_contains: true });
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



        function GetMemberName() {
            var myparam = $("#<%=ddlTokennew.ClientID%>").val(); //id name for dropdown list   
            var selser = "";
            selser = $("#<%=ddlReceiptSeries.ClientID%>").find("option:selected").text();
            selser = selser.replace(/(\r\n\t|\n|\r|\t)/gm, "");
            selser = selser.trim();
            $("#<%= HD_RSeriesid.ClientID %>").val(selser);

            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "crrnew.aspx/GetCustomername",
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
                url: "crrnew.aspx/GetMemberid",
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

           $("#<%=ddlTokennew.ClientID%>").addClass('chzn-select');
        }

        function GetRSeries() {
            var myparam = $("#<%=ddlColloctorName.ClientID%>").val(); //id name for dropdown list            
            
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "crrnew.aspx/PopulateRSeries",
                data: "{mcid:" + myparam + "}",
                dataType: "json",
                success: function (data) {
                    //alert("success");
                    //$.each(data.d, function (key, value) {                                             
                    var ddrseries = $("[id*=ddlReceiptSeries]");
                    ddrseries.empty()
                    //ddrseries.empty().append('<option selected="selected" value="0">Please select</option>');                                              
                    $.each(data.d, function () {
                        ddrseries.append($("<option></option>").val(this['Value']).html(this['Text']));
                    });
                },
                error: function (result) {
                    alert("Error: " + result);
                }
            });
            $("#<%=ddlColloctorName.ClientID%>").addClass('chzn-select');
            GetRcNum();
          
        }


        function GetRcNum() {
            var ser = $("#<%=ddlReceiptSeries.ClientID%>").text(); //id name for dropdown list              
            //var cid = $("#<%=ddlReceiptSeries.ClientID%>").val(); //text name for dropdown list  Label4
            var colname = $("#<%=ddlColloctorName.ClientID%>").find("option:selected").text();
            var cid = $("#<%=ddlColloctorName.ClientID%>").find("option:selected").val(); //text name for dropdown list           
          
           $.ajax({
               type: "POST",
               contentType: "application/json; charset=utf-8",
               url: "crrnew.aspx/getRcptNumber",
               data: JSON.stringify({ Series: ser, CollectorID: cid }),
               //data: "{Series:" + ser + ",CollectorID:" + cid + "}",
               //data: {"Series:" + series + ",CollectorID:" + collid + },
               // data: JSON.stringify(obj),
               //data: {},
               dataType: "json",
               success: function (data) {
                   var msg = data.d.toString();
                   var txtsnumber = $("[id*=txtReceiptNumber]");
                   txtsnumber.val(msg);
               },
               error: function (result) {
                   alert("Error: " + result);
               }
           });

           $('#ctl00_cphMainContent_ddlEmployee').val(colname);
           $("#<%=ddlEmployee.ClientID%>").find(selectvalue).attr("selected", "selected");
           $("#<%= HD_Empname.ClientID %>").val(colname);

        }

        function Getempname()
        {
            var empname = $("#<%=ddlEmployee.ClientID%>").find("option:selected").text();
            $("#<%= HD_Empname.ClientID %>").val(empname);           
        }

       function GetRcptNumber() {
           var ser = $("#<%=ddlReceiptSeries.ClientID%>").find("option:selected").text(); //id name for dropdown list              
           //var cid = $("#<%=ddlReceiptSeries.ClientID%>").val(); //text name for dropdown list  
           var cid = $("#<%=ddlReceiptSeries.ClientID%>").find("option:selected").val(); //text name for dropdown list             
           var rcid = $("#<%=ddlReceiptSeries.ClientID%> option:selected").val();
           //alert(rcid);


           //var obj = {};
           //obj.series = ser;
           //obj.collid = cid;
           $.ajax({
               type: "POST",
               contentType: "application/json; charset=utf-8",
               url: "crrnew.aspx/gtRcptBkNumber",
               data: JSON.stringify({ Series: ser, CollectorID: cid }),
               //data: "{Series:" + ser + ",CollectorID:" + cid + "}",
               //data: {"Series:" + series + ",CollectorID:" + collid + },
               // data: JSON.stringify(obj),
               //data: {},
               dataType: "json",
               success: function (data) {
                   var msg = data.d.toString();
                   var txtsnumber = $("[id*=txtReceiptNumber]");
                   txtsnumber.val(msg);
               },
               error: function (result) {
                   alert("Error: " + result);
               }
           });

       }


       $(document).ready(function () {
           $("#<%=ddlTokennew.ClientID%>").change(function () {
                   var myparam = $("#<%=ddlTokennew.ClientID%>").val(); //id name for dropdown list              
                   $.ajax({
                       type: "POST",
                       contentType: "application/json; charset=utf-8",
                       url: "crrnew.aspx/GetCustomername",
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
                       url: "crrnew.aspx/GetMemberid",
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

                   $("#<%=ddlTokennew.ClientID%>").addClass('chzn-select');
               });
           });

    </script>
</asp:Content>
