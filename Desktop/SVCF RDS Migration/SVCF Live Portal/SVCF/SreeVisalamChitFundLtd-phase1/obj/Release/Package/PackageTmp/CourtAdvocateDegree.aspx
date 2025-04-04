<%@ Page Title="" Language="C#" MasterPageFile="~/Branch.Master" AutoEventWireup="true" CodeBehind="CourtAdvocateDegree.aspx.cs" Inherits="SreeVisalamChitFundLtd_phase1.CourtAdvocateDegree" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
     <style type="text/css">
        .chzn-results {
            text-align: center;
        }

        .panel-table .panel-body {
            padding: 0;
        }
        
            .panel-table .panel-body .table-bordered {
                border-style: none;
                margin: 0;
            }

                .panel-table .panel-body .table-bordered > thead > tr > th:first-of-type {
                    text-align: center;
                    width: 100px;
                }

                .panel-table .panel-body .table-bordered > thead > tr > th:last-of-type,
                .panel-table .panel-body .table-bordered > tbody > tr > td:last-of-type {
                    border-right: 0px;
                }

                .panel-table .panel-body .table-bordered > thead > tr > th:first-of-type,
                .panel-table .panel-body .table-bordered > tbody > tr > td:first-of-type {
                    border-left: 0px;
                }

                .panel-table .panel-body .table-bordered > tbody > tr:first-of-type > td {
                    border-bottom: 0px;
                }

                .panel-table .panel-body .table-bordered > thead > tr:first-of-type > th {
                    border-top: 0px;
                }

        .panel-table .panel-footer .pagination {
            margin: 0;
        }

        /*
used to vertically center elements, may need modification if you're not using default sizes.
*/
        .panel-table .panel-footer .col {
            line-height: 34px;
            height: 34px;
        }

        .panel-table .panel-heading .col h3 {
            line-height: 30px;
            height: 30px;
        }

        .panel-table .panel-body .table-bordered > tbody > tr > td {
            line-height: 34px;
        }
    </style>
  <%--  <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.4.0/css/font-awesome.min.css" rel='stylesheet' type='text/css'>--%>
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.4.0/css/font-awesome.min.css" rel='stylesheet' type='text/css'>
<%--     <meta name="viewport" content="width=device-width, initial-scale=1">--%>
 <%-- <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css">--%>
 <%-- <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>
  <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMainContent" runat="server">
<ajax:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </ajax:ToolkitScriptManager>
    <script type="text/javascript">
        $(document).ready(function () {
            gridviewSearch();
        });

        function gridviewSearch() {
            $('#<%=lblNoRecords.ClientID%>').css('display', 'none');
            $('#<%=txtSearch.ClientID%>').keyup(function () {
                $('#<%=lblNoRecords.ClientID%>').css('display', 'none'); // Hide No records to display label.
                $("#<%=EditCourt.ClientID%> tr:has(td)").hide(); // Hide all the rows.

                var iCounter = 0;
                var sSearchTerm = $('#<%=txtSearch.ClientID%>').val(); //Get the search box value

                if (sSearchTerm.length == 0) //if nothing is entered then show all the rows.
                {
                    $("#<%=EditCourt.ClientID%> tr:has(td)").show();
                    return false;
                }
                //Iterate through all the td.
                $("#<%=EditCourt.ClientID%> tr:has(td)").children().each(function () {
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
    <style type="text/css">
    body
    {
        font-family: Arial;
        font-size: 10pt;
    }
    .GridPager a, .GridPager span
    {
        display: block;
        height: 15px;
        width: 15px;
        font-weight: bold;
        text-align: center;
        text-decoration: none;
    }
    .GridPager a
    {
        background-color: #f5f5f5;
        color: #969696;
        border: 1px solid #969696;
    }
    .GridPager span
    {
        background-color: #A1DCF2;
        color: #000;
        border: 1px solid #3AC0F2;
    }
</style>
     <div class="row">

        <div class="col-md-10 col-md-offset-1">
            <div class="panel panel-default panel-table">
                <div class="panel-heading">
                     <div class="row">
                        <asp:Label ID="Label1" Text="Search Text :" runat="server"> </asp:Label>
                        <asp:TextBox ID="txtSearch" runat="server" CssClass="twitterStyleTextbox "></asp:TextBox>
                        <asp:Label ID="lblNoRecords" runat="server"></asp:Label>
                    </div>
                     <div style="display: table-cell; vertical-align: top; padding-top: 10px; padding-right: 5px !important;">
                                <asp:Label ID="Label5" runat="server" Text="Branch: "></asp:Label>
                            </div>
                    <div style="display:table-cell; vertical-align:top; padding-top:4px; padding-right:5px !important;">
                                <asp:DropDownList ID="ddlbranch" Width="150px" CssClass="chzn-select" AutoPostBack="true" 
                                  runat="server"></asp:DropDownList>   
                     </div>

                </div>
                <div class="panel-body">
                    <div class="box_c_heading box_actions">
                        <p>
                            Edit Court Advocate Decree
                        </p>
                    </div>
                     <div class="box_c_content">
                        <div style="width: 100%; margin: 0 auto;">
                            <asp:GridView ID="EditCourt" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-list" HeaderStyle-Font-Bold="true" Font-Names="Verdana" ForeColor="#333333"
                                GridLines="Both" ShowFooter="True" CellPadding="4" CellSpacing="2" Width="867px" Font-Size="9.2pt"  AllowPaging="true" Font-Bold="True" PageSize="50" OnPageIndexChanging="OnPageIndexChanging">
                                <AlternatingRowStyle BackColor="White" ForeColor="#284775"/>
                                <PagerStyle HorizontalAlign = "Right" CssClass = "GridPager" />
                                <Columns>
                                <asp:TemplateField HeaderText="HEAD Name" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="10px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblheadid" runat="server" Text='<%#Eval("head")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" Width="10px"></ItemStyle>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="CC No" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="10px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblcc" runat="server" Text='<%#Eval("cc")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" Width="10px"></ItemStyle>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Chit Name" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="10px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblchitnm" runat="server" Text='<%#Eval("ChitName")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" Width="10px"></ItemStyle>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Member Name" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="10px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblmemnm" runat="server" Text='<%#Eval("MemberName")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" Width="10px"></ItemStyle>
                                </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Action" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="10px">
                                        <ItemTemplate>
                                           <asp:HyperLink ID="hyEdit" runat="server" CssClass="btn btn-default" NavigateUrl='<%# "~/EditCourtMemberDetails.aspx?Hid=" + Eval("HeadId")+"&name="+Eval("MemberName")+"&token="+Eval("ChitName") +"&parentidpa="+Eval("ParentID")+""%>' 
                                                Text="Edit"><em class="fa fa-pencil"></em></asp:HyperLink>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" Width="10px"></ItemStyle>
                                    </asp:TemplateField>
                                
                                
                                </Columns>
                            </asp:GridView>
                        </div>
                     </div>
                </div>
            </div>
       </div>
    </div>
</asp:Content>
