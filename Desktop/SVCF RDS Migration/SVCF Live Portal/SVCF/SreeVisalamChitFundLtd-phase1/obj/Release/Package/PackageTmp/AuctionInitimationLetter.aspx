<%@ Page Title="" Language="C#" MasterPageFile="~/Branch.Master" AutoEventWireup="true" CodeBehind="AuctionInitimationLetter.aspx.cs" Inherits="SreeVisalamChitFundLtd_phase1.AuctionInitimationLetter" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <link href="Styles/tablecss.css" rel="stylesheet" type="text/css" />
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
    <%-- <style>
        .loading {
            position: fixed;
            z-index: 999;
            height: 2em;
            width: 2em;
            overflow: show;
            margin: auto;
            top: 0;
            left: 0;
            bottom: 0;
            right: 0;
        }
    </style>--%>
    <script type="text/javascript">
        //************************** Treeview Parent-Child check behaviour ****************************//


        function OnTreeClick(evt) {
            var src = window.event != window.undefined ? window.event.srcElement : evt.target;
            var isChkBoxClick = (src.tagName.toLowerCase() == "input" && src.type == "checkbox");
            if (isChkBoxClick) {
                var parentTable = GetParentByTagName("table", src);
                var nxtSibling = parentTable.nextSibling;
                if (nxtSibling && nxtSibling.nodeType == 1)//check if nxt sibling is not null & is an element node
                {
                    if (nxtSibling.tagName.toLowerCase() == "div") //if node has children
                    {
                        //check or uncheck children at all levels
                        CheckUncheckChildren(parentTable.nextSibling, src.checked);
                    }
                }

                //check or uncheck parents at all levels
                // CheckUncheckParents(src, src.checked);
            }
        }


        function CheckUncheckChildren(childContainer, check) {
            var childChkBoxes = childContainer.getElementsByTagName("input");
            var childChkBoxCount = childChkBoxes.length;
            for (var i = 0; i < childChkBoxCount; i++) {
                childChkBoxes[i].checked = check;
            }
        }


        function CheckUncheckParents(srcChild, check) {
            var parentDiv = GetParentByTagName("div", srcChild);
            var parentNodeTable = parentDiv.previousSibling;


            if (parentNodeTable) {
                var checkUncheckSwitch;


                if (check) //checkbox checked
                {
                    var isAllSiblingsChecked = AreAllSiblingsChecked(srcChild);
                    if (isAllSiblingsChecked)
                        checkUncheckSwitch = true;
                    else

                        return; //do not need to check parent if any child is not checked
                }
                else //checkbox unchecked
                {

                    checkUncheckSwitch = false;
                }


                //var inpElemsInParentTable = parentNodeTable.getElementsByTagName("input");
                //if(inpElemsInParentTable.length > 0)
                //{
                //     var parentNodeChkBox = inpElemsInParentTable[0];

                //     if(!parentNodeChkBox.checked)
                //     {
                //         alert('test');
                //     }
                ////    parentNodeChkBox.checked = checkUncheckSwitch;
                ////    //do the same recursively
                ////    CheckUncheckParents(parentNodeChkBox, checkUncheckSwitch);
                //}
            }
        }


        function AreAllSiblingsChecked(chkBox) {
            var parentDiv = GetParentByTagName("div", chkBox);
            var inpElemsInParentTable = parentDiv.getElementsByTagName("input");
            var childCount = parentDiv.childNodes.length;
            for (var i = 0; i < childCount; i++) {
                if (parentDiv.childNodes[i].nodeType == 1) //check if the child node is an element node
                {
                    if (parentDiv.childNodes[i].tagName.toLowerCase() == "table") {
                        var prevChkBox = parentDiv.childNodes[i].getElementsByTagName("input")[0];
                        //if any of sibling nodes are not checked, return false
                        if (!prevChkBox.checked) {

                            return false;
                        }
                    }
                }
            }
            return true;
        }


        //utility function to get the container of an element by tagname
        function GetParentByTagName(parentTagName, childElementObj) {
            var parent = childElementObj.parentNode;
            while (parent.tagName.toLowerCase() != parentTagName.toLowerCase()) {
                parent = parent.parentNode;
            }
            return parent;
        }


    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" />

    <div class="row">
        <div class="twelve columns">
            <div class="box_c">
                <div class="box_c_heading  box_actions">
                    <p>
                        Auction Intimation
                    </p>
                </div>
                <div class="box_c_content">
                    <div class="row">
                        <br />
                        <div>
                            <%--<asp:Label ID="lblmesssage" runat="server" Visible="false" style="color:red;" />
                            <div style="margin: 0 250px; height:500px; background-color:#8288a0;overflow-y:scroll;padding:20px;">
                                <asp:UpdatePanel ID="UpdatePanelTree" runat="server" UpdateMode="Always">
                                    <ContentTemplate>
                                        <asp:TreeView ID="MenuView" runat="server" ShowCheckBoxes="All" ShowExpandCollapse="true" >
                                             <HoverNodeStyle Font-Underline="True" ForeColor="#5831f5" />
                                            <NodeStyle Font-Names="Tahoma" Font-Size="12pt" ForeColor="#fffcd0" Font-Bold="true" HorizontalPadding="20px"
                                                NodeSpacing="20px" VerticalPadding="5px" ></NodeStyle>
                                            <RootNodeStyle Font-Size="12pt"  />
                                            <ParentNodeStyle Font-Size="12pt" />
                                            <LeafNodeStyle Font-Size="12pt" ForeColor="#fffcd0" />
                                           
                                        </asp:TreeView>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                <asp:UpdateProgress ID="UpdProgTree" DisplayAfter="0" runat="server" AssociatedUpdatePanelID="UpdatePanelTree">
                                    <ProgressTemplate>
                                        <div id="loading" class="loading">
                                            <img src="Images/loading2.gif" alt="Loading..." />

                                        </div>

                                    </ProgressTemplate>
                                </asp:UpdateProgress>
                                 </div>
                                <asp:Button ID="Btngenerate" runat="server" CssClass="GreenyPushButton" TabIndex="1"
                                    Text="Generate" CausesValidation="false" Style="float: right; margin-right: 20px;" OnClick="btnGenerate_Click" />--%>
                            <table id="tble" runat="server" >
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblbranchname" Text="Branch Name" runat="server"></asp:Label><br />
                                                <asp:DropDownList ID="listbranch" TabIndex="7" runat="server" OnSelectedIndexChanged="branch_oNSelected" AutoPostBack="true" CssClass="chzn-select" Width="300px">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <%--<asp:Label ID="Label11" Text="ChitNo" runat="server"></asp:Label><br />
                                                <input id="txtsrch" tabindex="8" type="text" style="width:auto; height: 15px;" />
                                                <select id="abc1" tabindex="9" style="width:300px;" onchange="FindTokenName();"></select>
                                                <asp:HiddenField ID="tkn_id" runat="server" />
                                                <asp:HiddenField ID="hiddentkn_text" runat="server" />--%>
                                                <asp:Label ID="lblGroupname" Text="Group Name" runat="server"></asp:Label><br />
                                                 <asp:DropDownList ID="listGroup" TabIndex="7" runat="server" OnChange="ListTkn();" CssClass="chzn-select" Width="300px">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label1" Text="    " runat="server"></asp:Label><br />
                                                <asp:Button ID="pdf_Gen" runat="server" Text="PDF-Generate" OnClick="pdfgenonclick" />

                                            </td>
                                            <%--<td>
                                                 <asp:Button ID="btnExport1" runat="server" Text="ExportToExcel" OnClick = "ExportToExcel" />
                                            </td>--%>
                                            
                                         </tr>
                                          
                                    </table>

                           
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>


</asp:Content>
