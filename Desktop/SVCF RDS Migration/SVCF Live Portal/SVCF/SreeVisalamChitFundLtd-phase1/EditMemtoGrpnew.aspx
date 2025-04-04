<%@ Page Title="" Language="C#" MasterPageFile="~/Branch.Master" AutoEventWireup="true" CodeBehind="EditMemtoGrpnew.aspx.cs" Inherits="SreeVisalamChitFundLtd_phase1.EditMemtoGrpnew" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>

<asp:Content ID="Head1" runat="server" ContentPlaceHolderID="cphHead">
    <style type="text/css">
        td[style="cursor:default;"] {
            vertical-align: middle;
        }
    </style>
    <style type="text/css">
   .header
{
	background-color: #646464;
	font-family: Arial;
	color: White;
	border: none 0px transparent;
	height: 25px;
	text-align: center;
	font-size: 16px;
}
    .pager
{
    background-color: #5badff;
    font-family: Arial;
    color: White;
    height: 30px;
    text-align: left;
}

    </style>
      <style type="text/css">
        .chzn-results
        {
            text-align: center;
        }
        #ctl00_cphMainContent_ddlChit_chzn .chzn-drop .chzn-search input[type="text"]
        {
            height: 16px;
        }


    </style>

      <style type="text/css">
          .Grid {background-color: #fff; margin: 5px 0 10px 0; border: solid 1px #525252; border-collapse:collapse; font-family:Calibri; color: #474747;}
.Grid td {
      padding: 2px;
      border: solid 1px #c1c1c1; }
.Grid th  {
      padding : 4px 2px;
      color: #fff;
      background: 	#A9A9A9;
          /*#363670 url(Images/grid-header.png) repeat-x top;*/
      border-left: solid 1px #525252;
      font-size: medium; }
.Grid .alt {
      background: #fcfcfc url(Images/grid-alt.png) repeat-x top; }
.Grid .pgr {background: #363670 url(Images/grid-pgr.png) repeat-x top; }
.Grid .pgr table { margin: 3px 0; }
.Grid .pgr td { border-width: 0; padding: 0 6px; border-left: solid 1px #666; font-weight: bold; color: #fff; line-height: 18px; }  
.Grid .pgr a { color: Gray; text-decoration: none; }
.Grid .pgr a:hover { color: #000; text-decoration: none; }

      </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphMainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="row">
        <div class="twelve columns">
            <div class="box_c">
                <div class="box_c_heading  box_actions">
                    <p>
                        Edit Member To Group Details
                    </p>
                </div>
                <div class="box_c_content">
                    <div class="row">
                        <div style="width: 100%; margin: 0 auto;">
                             <div style="display: table-cell; padding-right: 5px !important;">
                                    <asp:DropDownList ID="ddlChit" Width="150px" Class="chzn-select" runat="server">
                                    </asp:DropDownList>
                                </div>

                              <div style="display: table-cell; padding-right: 5px; vertical-align: top;">
                                    <asp:Button ValidationGroup="twelvehead" TabIndex="3" ID="BtnStatisticsGo" CssClass="GreenyPushButton"
                                        runat="server" class="btn" OnClick="BtnStatisticsGo_Click" Text="Go!"></asp:Button>
                                </div>
                            <div class="GridviewDiv">
                                <asp:GridView runat="server" ID="gvDetails" AllowPaging="true" PageSize="50" AutoGenerateColumns="false" DataKeyNames="Head_Id" OnPageIndexChanging="gvDetails_PageIndexChanging" 
                                    OnRowCancelingEdit="gvDetails_RowCancelingEdit"
                                    OnRowEditing="gvDetails_RowEditing" OnRowUpdating="gvDetails_RowUpdating" OnRowDataBound="gvDetails_RowDataBound"  CssClass="Grid"  FooterStyle-Font-Size="Medium" PagerStyle-CssClass="pager" RowStyle-CssClass="rows" Font-Size="Large">
                                    <HeaderStyle  BackColor="Gray" ForeColor="White" Font-Size="Medium"/>
                                    <Columns>
                                         <asp:TemplateField HeaderText="Token">
                                                   <ItemTemplate >
                                                          <asp:Label ID="Label1" runat="server" Text='<%# Eval("GrpMemberID") %>' ></asp:Label>
                                                       </ItemTemplate>
                                                 </asp:TemplateField>

                                         <asp:TemplateField HeaderText="GrpMemberID">
                                                   <ItemTemplate >
                                                          <asp:Label ID="lbltoken" runat="server" Text='<%# Eval("TokenNumber") %>' ></asp:Label>
                                                       </ItemTemplate>
                                                 </asp:TemplateField>

                                          <asp:TemplateField HeaderText="BranchName">
                                                   <ItemTemplate >
                                                          <asp:Label ID="Label2" runat="server" Text='<%# Eval("B_Name") %>' ></asp:Label>
                                                       </ItemTemplate>
                                              <EditItemTemplate>
                                                <asp:HiddenField ID="hdbranchname" runat="server" Value='<%#Eval("B_Name") %>' />
                                                <asp:DropDownList ID="ddlbranchname" runat="server" AutoPostBack="true" CssClass="chzn-select" OnSelectedIndexChanged="ddlbranchname_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </EditItemTemplate>
                                                 </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Head_Id">
                                                   <ItemTemplate >
                                                          <asp:Label ID="Label3" runat="server" Text='<%# Eval("Head_Id") %>' ></asp:Label>
                                                       </ItemTemplate>
                                                 </asp:TemplateField>

                                        <asp:TemplateField HeaderText="MemberName">
                                              <ItemTemplate >
                                                          <asp:Label ID="Label4" runat="server" Text='<%# Eval("MemberName") %>' ></asp:Label>
                                                       </ItemTemplate>
                                              <EditItemTemplate>
                                                <asp:HiddenField ID="hdmemb" runat="server" Value='<%#Eval("MemberName") %>' />
                                                <asp:DropDownList ID="ddlmembername" runat="server" CssClass="chzn-select">
                                                </asp:DropDownList>
                                            </EditItemTemplate>
                                               </asp:TemplateField>

                                             <asp:TemplateField HeaderText="MemberID">
                                                   <ItemTemplate >
                                                          <asp:Label ID="Label5" runat="server" Text='<%# Eval("Membermasterid") %>' ></asp:Label>
                                                       </ItemTemplate>
                                            </asp:TemplateField>
                                        <asp:CommandField ShowEditButton="True" />
                                    </Columns>
                                </asp:GridView>
                                <asp:Label ID="lblresult" runat="server"></asp:Label>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    <script type="text/javascript">

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
    </script>
</asp:Content>