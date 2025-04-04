<%@ Page Title="" Language="C#" MasterPageFile="~/Branch.Master" AutoEventWireup="true" CodeBehind="EditMemberMaster2.aspx.cs" 
    Inherits="SreeVisalamChitFundLtd_phase1.EditMemberMaster2" EnableEventValidation="false" ValidateRequest="false" %>

<asp:Content ID="Content2" ContentPlaceHolderID="cphHead" runat="server">

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

        table > * > tr > * {
            padding-bottom: 15px !important;
        }


            td {
  text-align: left !important;
}
    </style>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphMainContent" runat="server">
    <link href="Styles/bootstrap.min.css" rel="stylesheet" />
    <div class="row">
        <div class="twelve columns">
            <div class="box_c">
                <div class="box_c_heading  box_actions">
                    <p>
                        Edit Member Master
                    </p>
                </div>
                
                <div class="box_c_content">
                  
                    <div style="width:100%;padding-top:25px;">
                            <div style="display: table-cell; vertical-align: top; float: right; padding-right: 5px;
                                    text-align: right; margin-top: -35px;">

                                    <a class="noprint" style="cursor: hand; cursor: pointer;" id="btnExport" >
                                        <img alt="Print" class="noprint" src="Styles/Img/pf-button-both.gif" style="border: none;
                                            cursor: hand; cursor: pointer;" />
                                    </a>                                   
                                </div>
                    </div>

                    <div style="width: 100%" >
                        
                          
                         <div id="printdiv" class="printable">
                        <table class="table table-bordered table-striped table-highlight"">
                            <tr>
                                <td style="width:100px">
                                    <label for="name">Image <span style="color: red;">*</span></label>
                                </td>
                                <td>
                                    <asp:Image ID="CustImage" Height="80px" Width="81px" runat="server"></asp:Image>
                                </td>
                              
                                <td>
                                    <label for="name" >Select New Image <span style="color: red;">*</span></label>
                                </td>
                                <td>
                                    <asp:FileUpload ID="fileuploadEmpImage" runat="server" Width="400px" />
                                </td>
                            </tr>
                          
                            <tr>
                                <td>
                                    <label for="name" >Branch <span style="color: red;">*</span></label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="drpdownbranchList"  Width="230px" runat="server" OnChange="getbranchid();"></asp:DropDownList>
                                    <asp:HiddenField ID="HD_branchval" runat="server" />
                                </td>                              
                           
                                <td>
                                    <label for="name">Title <span style="color: red;">*</span></label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="edTitle" runat="server"  Width="230px">
                                        <Items>
                                            <asp:ListItem Text="Mr." Value="Mr." />
                                            <asp:ListItem Text="Ms." Value="Ms." />
                                            <asp:ListItem Text="Mrs." Value="Mrs." />
                                        </Items>
                                    </asp:DropDownList>
                                </td>
                                 </tr>
                            <tr>
                                <td>
                                    <label for="name" >Customer Name<span style="color: red;">*</span></label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtCustomername"  Width="230px" runat="server"> </asp:TextBox>
                                </td>
                         
                                <td>
                                    <label for="name" >Type Of Member <span style="color: red;">*</span></label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txttypeofmember"  Width="230px" runat="server"></asp:TextBox>
                                </td>
                                   </tr>
                            <tr>
                                <td>
                                    <label for="name" >Age<span style="color: red;">*</span></label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtage"  Width="230px" runat="server"> </asp:TextBox>
                                </td>
                           
                                <td>
                                    <label for="name" >DOB<span style="color: red;">*</span></label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtdob"  Width="230px" runat="server"> </asp:TextBox>
                                </td>
                                 </tr>
                            <tr>
                                <td>
                                    <label for="name" >Father/Husband Name<span style="color: red;">*</span></label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtfatherhusname"  Width="230px" runat="server"></asp:TextBox>
                                </td>
                           
                                <td>
                                    <label for="name" >Mother/Wife Name<span style="color: red;">*</span></label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtmotherwifename" Width="230px" runat="server"></asp:TextBox>
                                </td>
 </tr>
                            <tr>
                                <td>
                                    <label for="name" >Identity Proof<span style="color: red;">*</span></label>
                                </td>
                                <td>
                                     <asp:TextBox ID="txtProof"  Width="230px" runat="server"></asp:TextBox>
                                    <%--<asp:DropDownList ID="dropproof" runat="server"  Width="230px">
                                        <Items>
                                            <asp:ListItem Text="Passport" Value="Passport" />
                                            <asp:ListItem Text="Voters ID" Value="Voters ID" />
                                            <asp:ListItem Text="Driving Licence" Value="Driving Licence" />
                                            <asp:ListItem Text="Ration Card" Value="Ration Card" />
                                            <asp:ListItem Text="Post Office Idendity" Value="Post Office Idendity" />
                                            <asp:ListItem Text="Aadhar Card No" Value="Aadhar Card No" />
                                            <asp:ListItem Text="Other" Value="Other" />
                                        </Items>
                                    </asp:DropDownList>--%>
                                </td>
                         
                                <td>
                                    <label for="name" >Date of Resolution<span style="color: red;">*</span></label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtdateofresolution"  Width="230px" runat="server"></asp:TextBox>
                                </td>
                                   </tr>
                            <tr>
                                <td>
                                    <label for="name" >Proprietor Name<span style="color: red;">*</span></label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtproprietorName" Width="230px" runat="server"> </asp:TextBox>
                                </td>
                          
                                <td>
                                    <label for="name" >Partners Name<span style="color: red;">*</span></label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtpartnersname"  Width="230px" runat="server"> </asp:TextBox>
                                </td>
                                  </tr>
                            <tr>
                                <td>
                                    <label for="name" >Date of Partnership<span style="color: red;">*</span></label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtdateofpartnership"  Width="230px" runat="server"> </asp:TextBox>
                                </td>
                           
                                <td>
                                    <label for="name" >File Ref. No.<span style="color: red;">*</span></label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtcompxerox"  Width="230px" runat="server"> </asp:TextBox>
                                </td>
                                 </tr>
                            <tr>
                                <td>
                                    <label for="name" >Profession Business<span style="color: red;">*</span></label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtprofessionbusiness"  Width="230px" runat="server"> </asp:TextBox>
                                </td>
                           
                                <td>
                                    <label for="name" >Nature of Profession<span style="color: red;">*</span></label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtnatureofprofession"  Width="230px" runat="server"> </asp:TextBox>
                                </td>
                                 </tr>
                            <tr>
                                <td>
                                    <label for="name" >Residential Address<span style="color: red;">*</span></label>
                                </td>
                                <td>
                                   <asp:TextBox ID="txtresidential"  Width="200px" runat="server" TextMode="MultiLine"> </asp:TextBox>
                                </td>
                          
                                <td>
                                    <label for="name" >Aadhar Card No.<span style="color: red;">*</span></label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtadhar"  Width="230px" runat="server"> </asp:TextBox>
                                </td>
                                  </tr>
                            <tr>
                                <td>
                                    <label for="name" >Address For Communication<span style="color: red;">*</span></label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtaddressforcommunication" Width="200px"  runat="server" TextMode="MultiLine"> </asp:TextBox>
                                </td>
                           
                                <td>
                                    <label for="name" >Address For Profession Business<span style="color: red;">*</span></label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtaddressforprofessionbusiness" Width="200px" runat="server" TextMode="MultiLine"> </asp:TextBox>
                                </td>
 </tr>
                            <tr>
                                <td>
                                    <label for="name" >Profession<span style="color: red;">*</span></label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtphoneno" Width="230px" runat="server"></asp:TextBox>
                                </td>
                       
                                <td class="auto-style1">
                                    <label for="name" >Phone No.<span style="color: red;">*</span></label>
                                </td>
                                <td class="auto-style1">
                                    <asp:TextBox ID="txtphonenoprofession" Width="230px" runat="server"></asp:TextBox>
                                </td>
                                     </tr>
                            <tr>
                                <td class="auto-style1">
                                    <label for="name" >Monthly Income<span style="color: red;">*</span></label>
                                </td>
                                <td class="auto-style1">
                                    <asp:TextBox ID="txtmothlyincome"  Width="230px" runat="server"> </asp:TextBox>
                                </td>
                           
                                <td>
                                    <label for="name" >Mobile No.<span style="color: red;">*</span></label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtmobileno" Width="230px" runat="server"> </asp:TextBox>
                                </td>
                                 </tr>
                            <tr>
                                <td>
                                    <label for="name" >Sales Tax Reg.No.<span style="color: red;">*</span></label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txttaxreg"  Width="230px" runat="server"> </asp:TextBox>
                                </td>
                         
                                <td>
                                    <label for="name" >CST Reg. No.</label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtcstreg"  Width="230px" runat="server"> </asp:TextBox>
                                </td>
                                   </tr>
                            <tr>
                                <td>
                                    <label for="name" >Income Tax P.A.No.</label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtincome" Width="230px" runat="server"> </asp:TextBox>
                                </td>
                        
                                <td>
                                    <label for="name" >Bank Name</label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtbankname"  Width="230px" runat="server"> </asp:TextBox>
                                </td>
                                    </tr>
                            <tr>
                                <td>
                                    <label for="name">Branch Name<span style="color: red;">*</span></label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtbranchname"  Width="230px" runat="server"> </asp:TextBox>
                                </td>
                         
                                <td>
                                    <label for="name" >Savings/Current AccNo.<span style="color: red;">*</span></label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtsavingcurrentaccountno"  Width="230px" runat="server"> </asp:TextBox>
                                </td>
                            </tr>
                                <tr>
                                <td>
                                    <label for="name">Member Id<span style="color: red;">*</span></label>
                                </td>
                                <td>
                                    <asp:Label ID="lblMemberid" runat="server" Text=""></asp:Label>                              
                                </td>
                             
                            </tr>
                         
                        </table>
                            </div>
                         <div class="boxheader" style="width: 100%; bottom: 0px; height: 50px; position: absolute">
                        <div class="form-group text-center">
                              <asp:HyperLink ID="Back" runat="server" CssClass="btn btn-orange-md roboto" NavigateUrl="~/EditMemberMaster3.aspx"
                                        Text="Back" />
                            <asp:Button ID="BtnUpdate" runat="server" Text="Update" CssClass="btn btn-orange-md roboto" OnClick="BtnUpdate_Click" />

                            <asp:Button ID="Button1" runat="server" Text="Delete" CssClass="btn btn-orange-md roboto" OnClick="Button1_Click" />
                        </div>
                    </div>
                   </div>
                 
                   <asp:HiddenField ID="hfTableHtml" runat="server" />
                </div>
            </div>
        </div>
    </div>


     <script type="text/javascript">
         $(document).ready(function () {
             //LoadBranchList();        
  
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

      


</script>
     
    <script type="text/javascript">
        $(document).ready(function () {          
            $("#btnExport").click(function (e) {             
                PrintDocument('printdiv');
                e.preventDefault();
            });
        });

        function getbranchid(){            
            var branchid = $("#<%= drpdownbranchList.ClientID %>").val();
            alert(branchid);
            $("#<%= HD_branchval.ClientID %>").val(branchid);           
        }

        function PrintDocument(printdiv) {           
            var printContents = document.getElementById(printdiv).innerHTML;
            var originalContents = document.body.innerHTML;
            document.body.innerHTML = printContents;
            window.print();
            document.body.innerHTML = originalContents;
            return true;
         }
</script>
</asp:Content>
