<%@ Page Title="" Language="C#" MasterPageFile="~/Branch.Master" AutoEventWireup="true" CodeBehind="updateMID.aspx.cs" Inherits="SreeVisalamChitFundLtd_phase1.updateMID" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMainContent" runat="server">
    
    <style>
        .hdskipLink {
            background-color: #c1c1c1;
            width: 100%;
            margin-top: 25px;
            height: 30px;
            text-align: center;
            padding-top: 6px;
            color: #fff;
            font-weight: bold;
            font-size: 18px;
        }

       


    </style>
     <link href="Styles/StyleSheet1.css" rel="stylesheet" />
    <link href="Styles/bootstrap.min.css" rel="stylesheet" />

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
      <div class="twelve columns" style="margin-top: -0.5em; margin-bottom: 0.6em;">
       <div class="box_c">
        <div class="box_c_content">
           <asp:UpdatePanel ID="UpdatePanel1" runat="server">
             <ContentTemplate>            

             <asp:Panel ID="Panel1" runat="server">
              <table>
                 <tr>
                   <td>
                     <label> Group Id: </label>
                   </td>
                   <td>
                       <asp:DropDownList ID="DD_Gid" runat="server" AutoPostBack="true" TabIndex="7" CssClass="chzn-select"
                           onselectedindexchanged="DD_Gid_SelectedIndexChanged" Width="200px">
                       </asp:DropDownList>
                   </td>
                 </tr>
                 <tr>
                   <td>
                     <label> Money Collector List: </label>
                   </td>
                   <td>
                       <asp:DropDownList ID="DD_MCList" runat="server" TabIndex="7" CssClass="chzn-select" Width="200px">
                       </asp:DropDownList>
                   </td>
                   <td>
                       <asp:Button ID="BtnUpdate" runat="server" Text="Update" 
                           CssClass="GreenyPushButton active" onclick="BtnUpdate_Click" />
                   </td>
                 </tr>
              </table>
        </asp:Panel>
     <div style="height:700px; overflow:auto;">       
         <div>      
            <header>Money collector</header>                
            </div>         
          <div>
           <asp:GridView ID="GridUpdateMID"  runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-hover"
                Font-Names="Verdana" ForeColor="#333333" GridLines="Both"
            EmptyDataText="No Records Found" Width="100%" BorderStyle="Solid" CellSpacing="11"  >
        
            <Columns>
               <asp:TemplateField HeaderText="Select">
                 <ItemTemplate>
                     <asp:CheckBox ID="ChkMid" runat="server" Checked="false" />
                 </ItemTemplate>                
               </asp:TemplateField>

                 <asp:TemplateField HeaderText="HeadID" Visible="false"
                HeaderStyle-BorderColor="Gray" HeaderStyle-BorderStyle="Solid" HeaderStyle-BorderWidth="2px">
                 <ItemTemplate>
                     <asp:Label ID="lblheadid" runat="server" Text='<%#Eval("Head_Id") %>'></asp:Label>
                 </ItemTemplate>
               </asp:TemplateField>      

                <asp:BoundField HeaderText="ID" DataField="GrpMemberID" > </asp:BoundField>

               <asp:BoundField HeaderText="Token" DataField="TokenNumber" ItemStyle-Width="100px"> 
                </asp:BoundField>
            
               <asp:BoundField HeaderText="Name" DataField="MemberName">
                </asp:BoundField>

               <asp:BoundField HeaderText="Address" DataField="Address">             
                </asp:BoundField>
             
               <asp:BoundField HeaderText="Money-collector Name" DataField="Money-collectorName">     
                </asp:BoundField>
               
            </Columns>
         </asp:GridView>
        </div>
       </div>
      
  </ContentTemplate>
</asp:UpdatePanel>
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
