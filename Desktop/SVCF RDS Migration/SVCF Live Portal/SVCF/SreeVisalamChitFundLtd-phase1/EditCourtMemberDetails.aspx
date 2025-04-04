<%@ Page Title="" Language="C#" MasterPageFile="~/Branch.Master" AutoEventWireup="true" CodeBehind="EditCourtMemberDetails.aspx.cs" Inherits="SreeVisalamChitFundLtd_phase1.EditCourtMemberDetails" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <style type="text/css">
        .auto-style1 {
            height: 23px;
        }

        .roboto {
            font-family: 'Roboto', sans-serif !important;
        }
        .btn-orange-md {
            background: #FF791F !important;
            border-bottom: 3px solid #ae4d13 !important;
            color: white;
        }

            .btn-orange-md:hover {
                background: #d86016 !important;
                color: white !important;
            }
           
            table > * > tr > *
{
   padding-bottom: 15px !important;
}


            td {
  text-align: left !important;
}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMainContent" runat="server">
    <link href="Styles/bootstrap.min.css" rel="stylesheet" />
   <ajax:ToolkitScriptManager ID="scm1" runat="server">
    </ajax:ToolkitScriptManager>
     <div class="row">
        <div class="twelve columns">
            <div class="box_c">
                <div class="box_c_heading  box_actions">
                    <p>
                       Edit Court Member Details
                    </p>
                </div>
                
                <div class="box_c_content"> 
                      <div id="printdiv" class="printable">
                        <table class="table table-bordered table-striped table-highlight">
                        <tr>
                                <td style="width:100px">
                                    <label for="name" >CC Number <span style="color: red;">*</span></label>
                                </td>

                                <td>
                                    <asp:TextBox ID="txtccnumber"  Width="230px" runat="server"> </asp:TextBox>
                                </td>     
                            <td>
                                    <label for="name">Date of Bad Debts<span style="color: red;">*</span></label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtdate_debts"  Width="230px" runat="server"> </asp:TextBox>
                                </td>
                           </tr>
                            <tr>
                                <td>
                                    <label for="name">Member Name <span style="color: red;">*</span></label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtmembername"  Wrap="true" Width="230px" runat="server"> </asp:TextBox>
                                </td>
                                  <td>
                                    <label for="name">EPNo Or OS No<span style="color: red;">*</span></label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtepno"  Width="230px" runat="server"> </asp:TextBox>
                                  
                                </td>
                         </tr>

                           <tr>
                                <td>
                                    <label for="name" >Chit Name <span style="color: red;">*</span></label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtchitname"  Width="230px" runat="server"> </asp:TextBox>
                                </td>    
                               
                               <td>
                                    <label for="name">Court Place<span style="color: red;">*</span></label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtCourtPlace"  Width="230px" runat="server"> </asp:TextBox>
                                    <asp:Label ID="hdidlbl" runat="server" Text="HeadID" Visible="false"></asp:Label>
                                </td>
                          </tr>
                            <tr>
                                <td>
                                    <label for="name">ARC Number<span style="color: red;">*</span></label>
                                </td>
                                <td>
                                   <asp:TextBox ID="txtArcNumber"  Width="230px" runat="server"> </asp:TextBox>
                                </td>
                                <td>
                                    <label for="name">Court Complex<span style="color: red;">*</span></label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtCourt_Complex"  Width="230px" runat="server"> </asp:TextBox>
                                </td>
                                </tr>
                            <tr>
                               <td>
                                    <label for="name">ARC year<span style="color: red;">*</span></label>
                                </td>
                                <td>
                                   <asp:TextBox ID="txtArcyear"  Width="230px" runat="server"> </asp:TextBox>
                                </td>
                         </tr>

                            <tr>
                                <%--<td>
                                    <label for="name" >Branch Name <span style="color: red;">*</span></label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="DropDownList3"  Width="230px" runat="server" OnChange="getbranchid();"></asp:DropDownList>
                                    <asp:HiddenField ID="HiddenField2" runat="server" />
                                </td> --%>                             
                           
                                <td>
                                    <label for="name">Party Address<span style="color: red;">*</span></label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtPartyAddress"  Width="230px" runat="server"> </asp:TextBox>
                                </td>

                                 

                               
                         </tr>
                            <tr>
                                <td>
                                    <label for="name" >Court<span style="color: red;">*</span></label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtCourt"  Width="230px" runat="server"> </asp:TextBox>
                                </td>                              

                         </tr>
                            <tr>
                                 <td>
                                    <label for="name">Suit year<span style="color: red;">*</span></label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtsuityear"  Width="230px" runat="server"> </asp:TextBox>
                                  
                                </td>
                            </tr>
                        </table>
                       
                              <div class="boxheader" style="width: 100%; bottom: 0px; height: 50px; position: absolute">
                        <div class="form-group text-center">
                              <asp:HyperLink ID="Back" runat="server" CssClass="btn btn-orange-md roboto" NavigateUrl="~/CourtAdvocateDegree.aspx"
                                        Text="Back" />
                            <asp:Button ID="updbtn" runat="server" Text="Update" CssClass="btn btn-orange-md roboto"  OnClick="Btnupdate_click" />
                        </div>
                    </div>
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
                        <div style="min-height: 100px;text-align: center;">
                            <br />
                            <br />
                            <asp:Label runat="server" ID="lblcon" Text=""> </asp:Label>
                            <br />
                            <br />
                        </div>
                        <div class="boxheader">
                            <div style="margin: 0 auto;">
                                <asp:Button TabIndex="21" Visible="false" Style="margin: 0 auto" CssClass="GreenyPushButton" 
                                    ID="BtnOK" CausesValidation="false" OnClientClick="clearValidationErrors();"
                                    runat="server" Text="Yes" />
                                <asp:Button TabIndex="21" Visible="false" Style="margin: 0 auto" CssClass="GreenyPushButton" OnClick="load_cancel"
                                    ID="Button2" CausesValidation="false" OnClientClick="clearValidationErrors();"
                                    runat="server" Text="Ok" />
                                <asp:Button TabIndex="21" Visible="false" Style="margin: 0 auto" CssClass="GreenyPushButton" OnClick="load_cancel"
                                    ID="Button3" CausesValidation="false" OnClientClick="clearValidationErrors();"
                                    runat="server" Text="Cancel" />
                                <asp:Button Visible="false" Style="margin: 0 auto" CssClass="GreenyPushButton" 
                                    ID="btnHide" CausesValidation="false" OnClientClick="clearValidationErrors();"
                                    runat="server" Text="Ok" />
                            </div>
                        </div>
                    </asp:Panel>

                </div>
          </div>
       </div>
   </div>


     <%--<script type="text/javascript">
         $(document).ready(function () {
             LoadBranchList();        
  
         });      

         function LoadBranchList() {           
            var queries = [];
             $.each(document.location.search.substr(1).split('&'), function (c, q) {

                 var i = q.split('=');
                 queries[0] = i[1].toString();
             });

             var result = queries[0];

             var branchid = result.replace(/%20/g, " ");

             $.ajax({
                 type: "POST",
                 contentType: "application/json; charset=utf-8",
                 url: "EditMemberMaster2.aspx/Getbranchlist",
                 data: "{BranchId:" + branchid + "}",
                 dataType: "json",
                 success: function (data) {
                     var branchlist = $("[id*=drpdownbranchList]");
                     branchlist.empty().append('<option selected="selected" value="0">Please select</option>');
                     $.each(data.d, function () {
                         branchlist.append($("<option></option>").val(this['Value']).html(this['Text']));
                     });
                 },
                 error: function (result) {
                     alert("Error: " + result);
                 }

             });

         }

      


</script>--%>
</asp:Content>
