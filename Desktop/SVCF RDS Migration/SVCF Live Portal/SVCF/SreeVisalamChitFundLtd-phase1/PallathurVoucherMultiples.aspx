<%@ Page Language="C#" Culture="en-GB" MasterPageFile="~/Branch.Master" AutoEventWireup="true"
    CodeBehind="PallathurVoucherMultiples.aspx.cs" EnableEventValidation="false" Inherits="SreeVisalamChitFundLtd_phase1.PallathurVoucherMultiples"
    Title="SVCF Admin Panel" %>

<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxTabControl" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxClasses" TagPrefix="dx" %>
<%--<%@ Register TagPrefix="VM" TagName="VoucherMultiples" Src="~/VoucherMultiples_Base.ascx" %>--%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>

<asp:Content ID="Head1" ContentPlaceHolderID="cphHead" runat="server">
    <link href="Styles/ControlsCss.css" rel="stylesheet" />
    <style type="text/css">
        .answerform {
            position: absolute;
            border: 5px solid gray;
            padding: 0px;
            background: white;
            width: 800px;
            height: 200px;
            overflow-y: scroll;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMainContent" runat="server">
    <ajax:ToolkitScriptManager ID="scm1" runat="server">
    </ajax:ToolkitScriptManager>

    <style type="text/css">
        td {
            vertical-align: middle;
            padding: 0px 2px 0px 2px;
        }
    </style>

    <script type="text/javascript">
        function OnKeyPress(s, e) {
            var charCode = e.htmlEvent.charCode;
            if (String.fromCharCode(charCode) == "/") {
                e.processOnServer = false;
                return false
            }
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
    </script>
    <script type="text/javascript">

        function NumberOnly() {
            //var AsciiValue = event.keyCode
            //if ((AsciiValue >= 48 && AsciiValue <= 57) || (AsciiValue == 8 || AsciiValue == 127))
            //    event.returnValue = true;
            //else
            //    event.returnValue = false;

            var charCode = (evt.which) ? evt.which : event.keyCode
            if (charCode > 31 && (charCode < 48 || charCode > 57) && !(charCode == 46 || charcode == 8))
                return false;
            else {
                var len = $(element).val().length;
                var index = $(element).val().indexOf('.');
                if (index > 0 && charCode == 46) {
                    return false;
                }
                if (index > 0) {
                    var CharAfterdot = (len + 1) - index;
                    if (CharAfterdot > 3) {
                        return false;
                    }
                }

            }
            return true;
        }

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

        function ValidatetCreditHead1() {
            if (document.getElementById('<%=ddlHeads1.ClientID%>').selectedIndex == 0) {
                document.getElementById("lblheaderror1").innerText = "*";
                return false;
            }
            if (document.getElementById('<%=txtAmount1.ClientID%>').value == "") {
                document.getElementById("lblamnterr1").innerText = "*";
                return false;
            }
            if (document.getElementById('<%=txtDescription1.ClientID%>').value == "") {
                document.getElementById("lbldesccr1").innerText = "*";
                return false;
            }

            document.getElementById("lblheaderror1").innerText = "";
            document.getElementById("lblamnterr1").innerText = "";
            document.getElementById("lbldesccr1").innerText = "";
            return true;
        }

        function ValidatetCreditHead2() {
            if (document.getElementById('<%=ddlHeads2.ClientID%>').selectedIndex == 0) {
                document.getElementById("lblheaderror2").innerText = "*";
                return false;
            }
            if (document.getElementById('<%=txtAmount2.ClientID%>').value == "") {
                document.getElementById("lblamnterr2").innerText = "*";
                return false;
            }
            if (document.getElementById('<%=txtDescription2.ClientID%>').value == "") {
                document.getElementById("lbldesccr2").innerText = "*";
                return false;
            }

            document.getElementById("lblheaderror2").innerText = "";
            document.getElementById("lblamnterr2").innerText = "";
            document.getElementById("lbldesccr2").innerText = "";
            return true;
        }

        function ValidatetCreditHead3() {
            if (document.getElementById('<%=ddlHeads3.ClientID%>').selectedIndex == 0) {
                document.getElementById("lblheaderror3").innerText = "*";
                return false;
            }
            if (document.getElementById('<%=txtAmount3.ClientID%>').value == "") {
                document.getElementById("lblamnterr3").innerText = "*";
                return false;
            }
            if (document.getElementById('<%=txtDescription3.ClientID%>').value == "") {
                document.getElementById("lbldesccr3").innerText = "*";
                return false;
            }

            document.getElementById("lblheaderror3").innerText = "";
            document.getElementById("lblamnterr3").innerText = "";
            document.getElementById("lbldesccr3").innerText = "";
            return true;
        }

        function GetDebitcheck1() {

            if (document.getElementById('<%=ddlHeadsDebit1.ClientID%>').selectedIndex == 0) {
                document.getElementById("lbldeberror1").innerText = "*";
                return false;
            }

           <%-- if (document.getElementById('<%=txtAmountDebit.ClientID%>').value == "") {                
                document.getElementById("lbldbamnt").innerText = "*";
                return false;
            }--%>
            if (document.getElementById('<%=txtDebitdesc1.ClientID%>').value == "") {
                document.getElementById("lbldbdesc1").innerText = "*";
                return false;
            }
            document.getElementById("lbldeberror1").innerText = "";
            document.getElementById("lbldbamnt1").innerText = "";
            document.getElementById("lbldbdesc1").innerText = "";
            return true;
        }

        function GetDebitcheck2() {

            if (document.getElementById('<%=ddlHeadsDebit2.ClientID%>').selectedIndex == 0) {
                document.getElementById("lbldeberror2").innerText = "*";
                return false;
            }

            <%-- if (document.getElementById('<%=txtAmountDebit.ClientID%>').value == "") {                
                document.getElementById("lbldbamnt").innerText = "*";
                return false;
            }--%>
            if (document.getElementById('<%=txtDebitdesc2.ClientID%>').value == "") {
                document.getElementById("lbldbdesc2").innerText = "*";
                return false;
            }
            document.getElementById("lbldeberror2").innerText = "";
            document.getElementById("lbldbamnt2").innerText = "";
            document.getElementById("lbldbdesc2").innerText = "";
            return true;
        }

        function GetDebitcheck3() {

            if (document.getElementById('<%=ddlHeadsDebit3.ClientID%>').selectedIndex == 0) {
                document.getElementById("lbldeberror3").innerText = "*";
                return false;
            }

            <%-- if (document.getElementById('<%=txtAmountDebit.ClientID%>').value == "") {                
                document.getElementById("lbldbamnt").innerText = "*";
                return false;
            }--%>
            if (document.getElementById('<%=txtDebitdesc3.ClientID%>').value == "") {
                document.getElementById("lbldbdesc3").innerText = "*";
                return false;
            }
            document.getElementById("lbldeberror3").innerText = "";
            document.getElementById("lbldbamnt3").innerText = "";
            document.getElementById("lbldbdesc3").innerText = "";
            return true;

        }
    </script>
    <script type="text/javascript">
        function GetCustName1(hdid) {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "PallathurVoucherMultiples.aspx/getcustomername",
                data: JSON.stringify({ hdid: hdid }),
                dataType: "json",
                success: function (data) {
                    var msg = data.d.toString();
                    var txtsnumber = $("[id*=txtDescription1]");
                    txtsnumber.val(msg);
                },
                error: function (result) {
                    alert("Error: " + result);
                }
            });
        }

        function GetCustName2(hdid) {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "PallathurVoucherMultiples.aspx/getcustomername",
                data: JSON.stringify({ hdid: hdid }),
                dataType: "json",
                success: function (data) {
                    var msg = data.d.toString();
                    var txtsnumber = $("[id*=txtDescription2]");
                    txtsnumber.val(msg);
                },
                error: function (result) {
                    alert("Error: " + result);
                }
            });
        }

        function GetCustName3(hdid) {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "PallathurVoucherMultiples.aspx/getcustomername",
                data: JSON.stringify({ hdid: hdid }),
                dataType: "json",
                success: function (data) {
                    var msg = data.d.toString();
                    var txtsnumber = $("[id*=txtDescription3]");
                    txtsnumber.val(msg);
                },
                error: function (result) {
                    alert("Error: " + result);
                }
            });
        }

        function GetChequeNumber1() {
            var ser = $("#<%=ddlHeads1.ClientID%>").find("option:selected").text(); //id name for dropdown list              
            var cid = $("#<%=ddlHeads1.ClientID%>").find("option:selected").val(); //text name for dropdown list             
            var rcid = $("#<%=ddlHeads1.ClientID%> option:selected").val();
            var bnktrue = rcid.includes(":");
            var chittrue = rcid.includes("|");
            //alert(chittrue);
            if (bnktrue == true) {
                var rtid = rcid.split(":", 1)
                if (rtid == 3) {
                    $("#<%=txtChequeNO1.ClientID%>").show();
                    $("#<%=lblChequeNO1.ClientID%>").show();
                    document.getElementById('<%=txtDescription1.ClientID %>').value = "";
                }
                else if (rtid == 5) {
                    $("#<%=txtChequeNO1.ClientID%>").hide();
                    $("#<%=lblChequeNO1.ClientID%>").hide();
                    var headid = rcid.split(':')[1];
                    GetCustName1(headid);
                }
        }
        else if (chittrue == true) {
            $("#<%=txtChequeNO1.ClientID%>").hide();
                $("#<%=lblChequeNO1.ClientID%>").hide();
                document.getElementById('<%=txtDescription1.ClientID %>').value = "";
            }
        $("#<%=ddlHeads1.ClientID%>").addClass('chzn-select');
        }

        function GetChequeNumber2() {
            var ser = $("#<%=ddlHeads2.ClientID%>").find("option:selected").text(); //id name for dropdown list              
            var cid = $("#<%=ddlHeads2.ClientID%>").find("option:selected").val(); //text name for dropdown list             
            var rcid = $("#<%=ddlHeads2.ClientID%> option:selected").val();
            var bnktrue = rcid.includes(":");
            var chittrue = rcid.includes("|");
            if (bnktrue == true) {
                var rtid = rcid.split(":", 1)
                if (rtid == 3) {
                    $("#<%=txtChequeNO2.ClientID%>").show();
                    $("#<%=lblChequeNO2.ClientID%>").show();
                    document.getElementById('<%=txtDescription1.ClientID %>').value = "";
                }
                else if (rtid == 5) {
                    $("#<%=txtChequeNO2.ClientID%>").hide();
                    $("#<%=lblChequeNO2.ClientID%>").hide();
                    var headid = rcid.split(':')[1];
                    GetCustName2(headid);
                }
        }
        else if (chittrue == true) {
            $("#<%=txtChequeNO2.ClientID%>").hide();
                $("#<%=lblChequeNO2.ClientID%>").hide();
                document.getElementById('<%=txtDescription2.ClientID %>').value = "";
            }
        $("#<%=ddlHeads2.ClientID%>").addClass('chzn-select');
        }

        function GetChequeNumber3() {
            var ser = $("#<%=ddlHeads3.ClientID%>").find("option:selected").text(); //id name for dropdown list              
            var cid = $("#<%=ddlHeads3.ClientID%>").find("option:selected").val(); //text name for dropdown list             
            var rcid = $("#<%=ddlHeads3.ClientID%> option:selected").val();
            var bnktrue = rcid.includes(":");
            var chittrue = rcid.includes("|");
            if (bnktrue == true) {
                var rtid = rcid.split(":", 1)
                if (rtid == 3) {
                    $("#<%=txtChequeNO3.ClientID%>").show();
                    $("#<%=lblChequeNO3.ClientID%>").show();
                    document.getElementById('<%=txtDescription3.ClientID %>').value = "";
                }
                else if (rtid == 5) {
                    $("#<%=txtChequeNO3.ClientID%>").hide();
                    $("#<%=lblChequeNO3.ClientID%>").hide();
                    var headid = rcid.split(':')[1];
                    GetCustName3(headid);
                }
        }
        else if (chittrue == true) {
            $("#<%=txtChequeNO3.ClientID%>").hide();
                $("#<%=lblChequeNO3.ClientID%>").hide();
                document.getElementById('<%=txtDescription3.ClientID %>').value = "";
            }
        $("#<%=ddlHeads3.ClientID%>").addClass('chzn-select');
        }

    </script>

    <script type="text/javascript">
        function GetValue1() {
            var hds = "";
            var hdsval = "";

            hds = $('#<%=ddlHeads1.ClientID %>').find("option:selected").text();
            hdsval = $('#<%=ddlHeads1.ClientID %>').find("option:selected").val();

            var headstext = $("[id*=HD_Headtxt1]");
            headstext.val(hds);

            var hdval = $("[id*=HD_Headval1]");
            hdval.val(hdsval);
        }


        $(function () {
            $("[id*=btnAdd1]").click(function () {
                //Reference the GridView.
                var gridView = $("[id*=GridClnt1]");

                //Reference the first row.
                var row = gridView.find("tr").eq(1);

                //Check if row is dummy, if yes then remove.
                if ($.trim(row.find("td").eq(0).html()) == "") {
                    row.remove();
                }
                //Clone the reference first row.
                row = row.clone(true);
                GetValue1();

                var amt = document.getElementById('<%=txtAmount1.ClientID %>').value;
                if (amt != "") {
                    //Add the Name value to first cell.
                    var HD_Headtxt = $("[id*=HD_Headtxt1]");

                    SetValue(row, 0, "Heads1", HD_Headtxt);

                    //Add the Country value to second cell.
                    var txtAmount = $("[id*=txtAmount1]");
                    SetValue(row, 1, "Amount1", txtAmount);

                    //Add the Country value to second cell.
                    var txtNarration = $("[id*=txtDescription1]");
                    SetValue(row, 2, "Description1", txtNarration);

                    //Add the Country value to second cell.
                    var txtChequeNO = $("[id*=txtChequeNO1]");
                    SetValue(row, 3, "ChequeNo1", txtChequeNO);

                    //Add the Country value to second cell.
                    var HD_Headval = $("[id*=HD_Headval1]");
                    SetValue(row, 4, "headid1", HD_Headval);


                    // Reset();
                    //Add the row to the GridView.
                    gridView.append(row);
                }

                return false;

            });

            function SetValue(row, index, name, textbox) {
                //Reference the Cell and set the value.
                row.find("td").eq(index).html(textbox.val());

                //Create and add a Hidden Field to send value to server.
                var input = $("<input type = 'hidden' />");
                input.prop("name", name);
                input.val(textbox.val());
                row.find("td").eq(index).append(input);

                //Clear the TextBox.
                textbox.val("");
            }
        });

        function GetValue2() {
            var hds = "";
            var hdsval = "";

            hds = $('#<%=ddlHeads2.ClientID %>').find("option:selected").text();
            hdsval = $('#<%=ddlHeads2.ClientID %>').find("option:selected").val();

            var headstext = $("[id*=HD_Headtxt2]");
            headstext.val(hds);

            var hdval = $("[id*=HD_Headval2]");
            hdval.val(hdsval);
        }


        $(function () {
            $("[id*=btnAdd2]").click(function () {
                //Reference the GridView.
                var gridView = $("[id*=GridClnt2]");

                //Reference the first row.
                var row = gridView.find("tr").eq(1);

                //Check if row is dummy, if yes then remove.
                if ($.trim(row.find("td").eq(0).html()) == "") {
                    row.remove();
                }
                //Clone the reference first row.
                row = row.clone(true);
                GetValue2();

                var amt = document.getElementById('<%=txtAmount2.ClientID %>').value;
                if (amt != "") {
                    //Add the Name value to first cell.
                    var HD_Headtxt = $("[id*=HD_Headtxt2]");

                    SetValue(row, 0, "Heads2", HD_Headtxt);

                    //Add the Country value to second cell.
                    var txtAmount = $("[id*=txtAmount2]");
                    SetValue(row, 1, "Amount2", txtAmount);

                    //Add the Country value to second cell.
                    var txtNarration = $("[id*=txtDescription2]");
                    SetValue(row, 2, "Description2", txtNarration);

                    //Add the Country value to second cell.
                    var txtChequeNO = $("[id*=txtChequeNO2]");
                    SetValue(row, 3, "ChequeNo2", txtChequeNO);

                    //Add the Country value to second cell.
                    var HD_Headval = $("[id*=HD_Headval2]");
                    SetValue(row, 4, "headid2", HD_Headval);


                    // Reset();
                    //Add the row to the GridView.
                    gridView.append(row);
                }

                return false;

            });

            function SetValue(row, index, name, textbox) {
                //Reference the Cell and set the value.
                row.find("td").eq(index).html(textbox.val());

                //Create and add a Hidden Field to send value to server.
                var input = $("<input type = 'hidden' />");
                input.prop("name", name);
                input.val(textbox.val());
                row.find("td").eq(index).append(input);

                //Clear the TextBox.
                textbox.val("");
            }
        });

        function GetValue3() {
            var hds = "";
            var hdsval = "";

            hds = $('#<%=ddlHeads3.ClientID %>').find("option:selected").text();
            hdsval = $('#<%=ddlHeads3.ClientID %>').find("option:selected").val();

            var headstext = $("[id*=HD_Headtxt3]");
            headstext.val(hds);

            var hdval = $("[id*=HD_Headval3]");
            hdval.val(hdsval);
        }


        $(function () {
            $("[id*=btnAdd3]").click(function () {
                //Reference the GridView.
                var gridView = $("[id*=GridClnt3]");

                //Reference the first row.
                var row = gridView.find("tr").eq(1);

                //Check if row is dummy, if yes then remove.
                if ($.trim(row.find("td").eq(0).html()) == "") {
                    row.remove();
                }
                //Clone the reference first row.
                row = row.clone(true);
                GetValue3();

                var amt = document.getElementById('<%=txtAmount3.ClientID %>').value;
                if (amt != "") {
                    //Add the Name value to first cell.
                    var HD_Headtxt = $("[id*=HD_Headtxt3]");

                    SetValue(row, 0, "Heads3", HD_Headtxt);

                    //Add the Country value to second cell.
                    var txtAmount = $("[id*=txtAmount3]");
                    SetValue(row, 1, "Amount3", txtAmount);

                    //Add the Country value to second cell.
                    var txtNarration = $("[id*=txtDescription3]");
                    SetValue(row, 2, "Description3", txtNarration);

                    //Add the Country value to second cell.
                    var txtChequeNO = $("[id*=txtChequeNO3]");
                    SetValue(row, 3, "ChequeNo3", txtChequeNO);

                    //Add the Country value to second cell.
                    var HD_Headval = $("[id*=HD_Headval3]");
                    SetValue(row, 4, "headid3", HD_Headval);


                    // Reset();
                    //Add the row to the GridView.
                    gridView.append(row);
                }

                return false;

            });

            function SetValue(row, index, name, textbox) {
                //Reference the Cell and set the value.
                row.find("td").eq(index).html(textbox.val());

                //Create and add a Hidden Field to send value to server.
                var input = $("<input type = 'hidden' />");
                input.prop("name", name);
                input.val(textbox.val());
                row.find("td").eq(index).append(input);

                //Clear the TextBox.
                textbox.val("");
            }
        });

        function DebGetValue1() {
            var dbhds = "";
            var dbhdsval = "";

            dbhds = $('#<%=ddlHeadsDebit1.ClientID %>').find("option:selected").text();
            dbhdsval = $('#<%=ddlHeadsDebit1.ClientID %>').find("option:selected").val();

            var headstext = $("[id*=hidDb_Headtxt1]");
            headstext.val(dbhds);

            var hdval = $("[id*=hidDb_HeadVal1]");
            hdval.val(dbhdsval);
        }

        $(function () {
            $("[id*=btnDbAdd1]").click(function () {

                //Reference the GridView.
                var gridView = $("[id*=GrdDbClnt1]");

                //Reference the first row.
                var row = gridView.find("tr").eq(1);

                //Check if row is dummy, if yes then remove.
                if ($.trim(row.find("td").eq(0).html()) == "") {
                    row.remove();
                }

                //Clone the reference first row.
                row = row.clone(true);

                DebGetValue1();

                var debamt = document.getElementById('<%=debitAmnt1.ClientID %>').value;
                if (debamt != "") {
                    //Add the bank head value to first cell.
                    var hidDb_Headtxt = $("[id*=hidDb_Headtxt1]");
                    SetValue1(row, 0, "DbHeads1", hidDb_Headtxt);

                    //Add the ddebit amount value to second cell.
                    var debitAmnt = $("[id*=debitAmnt1]");
                    SetValue1(row, 1, "DbAmount1", debitAmnt);

                    //Add the desc to third cell.
                    var txtDebitdesc = $("[id*=txtDebitdesc1]");
                    SetValue1(row, 2, "DbDescription1", txtDebitdesc);

                    //Add the head val to fourth cell.
                    var hidDb_HeadVal = $("[id*=hidDb_HeadVal1]");
                    SetValue1(row, 3, "Dbheadid1", hidDb_HeadVal);


                    // Reset();
                    //Add the row to the GridView.
                    gridView.append(row);
                }

                return false;

            });

            function SetValue1(row, index, name, textbox) {
                //Reference the Cell and set the value.
                row.find("td").eq(index).html(textbox.val());

                //Create and add a Hidden Field to send value to server.
                var input = $("<input type = 'hidden' />");
                input.prop("name", name);
                input.val(textbox.val());
                row.find("td").eq(index).append(input);

                //Clear the TextBox.
                textbox.val("");
            }
        });

        function DebGetValue2() {
            var dbhds = "";
            var dbhdsval = "";

            dbhds = $('#<%=ddlHeadsDebit2.ClientID %>').find("option:selected").text();
            dbhdsval = $('#<%=ddlHeadsDebit2.ClientID %>').find("option:selected").val();

            var headstext = $("[id*=hidDb_Headtxt2]");
            headstext.val(dbhds);

            var hdval = $("[id*=hidDb_HeadVal2]");
            hdval.val(dbhdsval);
        }

        $(function () {
            $("[id*=btnDbAdd2]").click(function () {

                //Reference the GridView.
                var gridView = $("[id*=GrdDbClnt2]");

                //Reference the first row.
                var row = gridView.find("tr").eq(1);

                //Check if row is dummy, if yes then remove.
                if ($.trim(row.find("td").eq(0).html()) == "") {
                    row.remove();
                }

                //Clone the reference first row.
                row = row.clone(true);

                DebGetValue2();

                var debamt = document.getElementById('<%=debitAmnt2.ClientID %>').value;
                if (debamt != "") {
                    //Add the bank head value to first cell.
                    var hidDb_Headtxt = $("[id*=hidDb_Headtxt2]");
                    SetValue2(row, 0, "DbHeads2", hidDb_Headtxt);

                    //Add the ddebit amount value to second cell.
                    var debitAmnt = $("[id*=debitAmnt2]");
                    SetValue2(row, 1, "DbAmount2", debitAmnt);

                    //Add the desc to third cell.
                    var txtDebitdesc = $("[id*=txtDebitdesc2]");
                    SetValue2(row, 2, "DbDescription2", txtDebitdesc);

                    //Add the head val to fourth cell.
                    var hidDb_HeadVal = $("[id*=hidDb_HeadVal2]");
                    SetValue2(row, 3, "Dbheadid2", hidDb_HeadVal);


                    // Reset();
                    //Add the row to the GridView.
                    gridView.append(row);
                }

                return false;

            });

            function SetValue2(row, index, name, textbox) {
                //Reference the Cell and set the value.
                row.find("td").eq(index).html(textbox.val());

                //Create and add a Hidden Field to send value to server.
                var input = $("<input type = 'hidden' />");
                input.prop("name", name);
                input.val(textbox.val());
                row.find("td").eq(index).append(input);

                //Clear the TextBox.
                textbox.val("");
            }
        });

        function DebGetValue3() {
            var dbhds = "";
            var dbhdsval = "";

            dbhds = $('#<%=ddlHeadsDebit3.ClientID %>').find("option:selected").text();
            dbhdsval = $('#<%=ddlHeadsDebit3.ClientID %>').find("option:selected").val();

            var headstext = $("[id*=hidDb_Headtxt3]");
            headstext.val(dbhds);

            var hdval = $("[id*=hidDb_HeadVal3]");
            hdval.val(dbhdsval);
        }

        $(function () {
            $("[id*=btnDbAdd3]").click(function () {

                //Reference the GridView.
                var gridView = $("[id*=GrdDbClnt3]");

                //Reference the first row.
                var row = gridView.find("tr").eq(1);

                //Check if row is dummy, if yes then remove.
                if ($.trim(row.find("td").eq(0).html()) == "") {
                    row.remove();
                }

                //Clone the reference first row.
                row = row.clone(true);

                DebGetValue3();

                var debamt = document.getElementById('<%=debitAmnt3.ClientID %>').value;
                if (debamt != "") {
                    //Add the bank head value to first cell.
                    var hidDb_Headtxt = $("[id*=hidDb_Headtxt3]");
                    SetValue3(row, 0, "DbHeads3", hidDb_Headtxt);

                    //Add the ddebit amount value to second cell.
                    var debitAmnt = $("[id*=debitAmnt3]");
                    SetValue3(row, 1, "DbAmount3", debitAmnt);

                    //Add the desc to third cell.
                    var txtDebitdesc = $("[id*=txtDebitdesc3]");
                    SetValue3(row, 2, "DbDescription3", txtDebitdesc);

                    //Add the head val to fourth cell.
                    var hidDb_HeadVal = $("[id*=hidDb_HeadVal3]");
                    SetValue3(row, 3, "Dbheadid3", hidDb_HeadVal);


                    // Reset();
                    //Add the row to the GridView.
                    gridView.append(row);
                }

                return false;

            });

            function SetValue3(row, index, name, textbox) {
                //Reference the Cell and set the value.
                row.find("td").eq(index).html(textbox.val());

                //Create and add a Hidden Field to send value to server.
                var input = $("<input type = 'hidden' />");
                input.prop("name", name);
                input.val(textbox.val());
                row.find("td").eq(index).append(input);

                //Clear the TextBox.
                textbox.val("");
            }
        });

    </script>

    <asp:Panel CssClass="row" ID="Panel_P1" runat="server" DefaultButton="btnGenerate"
        class="content">
        <div class="twelve columns">
            <div class="box_c">
                <div class="box_c_heading  box_actions">
                    <p>Voucher Details - Branch I</p>
                </div>
                <div class="box_c_content">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <div class="row">
                                <br />
                                <div style="display: inline; zoom: 1; text-align: center;">
                                    <div style="margin-left: auto; margin-right: auto; display: table;">
                                        <table cellspacing="4px" width="100%">
                                            <tr>
                                                <td style="vertical-align: top; padding-top: 6px;">
                                                    <asp:Label ID="lblDate1" runat="server" Text="Date"></asp:Label>
                                                </td>
                                                <td style="vertical-align: top; padding-left: 6px; text-align: left;">
                                                    <asp:TextBox AutoPostBack="true" Width="100" ValidationGroup="Generate" TabIndex="1" ID="txtDate1" onchange="CheckDate1();"
                                                        CssClass="input-text maskdate" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ForeColor="Red" ID="RequiredFieldValidator61" ErrorMessage="Required!!!"
                                                        ControlToValidate="txtDate1" ValidationGroup="Generate" Display="Dynamic" runat="server"></asp:RequiredFieldValidator>
                                                    <asp:CompareValidator ForeColor="Red" Type="Date" ID="CompareValidator111" ValidationGroup="Generate"
                                                        ControlToValidate="txtDate1" Display="Dynamic" Operator="DataTypeCheck" runat="server"
                                                        ErrorMessage="Incorrect!!!"></asp:CompareValidator>
                                                    <asp:RangeValidator ID="rvDate1" EnableClientScript="false" runat="server"
                                                        Type="Date" ControlToValidate="txtDate1" ValidationGroup="Generate" ForeColor="Red" Display="Dynamic"
                                                        ErrorMessage="*"></asp:RangeValidator>
                                                </td>
                                                <td style="vertical-align: top; padding-top: 6px;">
                                                    <asp:Label ID="lblSeries1" runat="server" Text="Series" Visible="false"></asp:Label>
                                                </td>
                                                <td style="vertical-align: top; padding-left: 6px; text-align: left;">
                                                    <asp:TextBox Width="100" ValidationGroup="Generate" ID="txtSeries1" Visible="false"
                                                        CssClass="input-text" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator Display="Dynamic" ValidationGroup="Generate" ControlToValidate="txtSeries1"
                                                        ID="RequiredFieldValidator111" ErrorMessage="Required!!!" runat="server"> </asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator Display="Dynamic" ValidationGroup="Generate" ID="RegularExpressionValidator21"
                                                        runat="server" ControlToValidate="txtSeries1" ErrorMessage="Alphabets only!!!"
                                                        ValidationExpression="^[a-zA-Z]+$"></asp:RegularExpressionValidator>
                                                </td>
                                                <td style="vertical-align: top; padding-top: 6px;">
                                                    <asp:Label ID="lblVoucherNo1" runat="server" Text="Voucher Number" Visible="true"></asp:Label>
                                                </td>
                                                <td style="vertical-align: top; padding-left: 6px; text-align: left;">
                                                    <asp:TextBox Width="100" TabIndex="2" ValidationGroup="Generate" ID="txtVoucherNo1" ReadOnly="true"
                                                        CssClass="input-text sp_number" Visible="true" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator Display="Dynamic" ValidationGroup="Generate" ControlToValidate="txtVoucherNo1"
                                                        ID="RequiredFieldValidator121" ErrorMessage="Required!!!" runat="server"> </asp:RequiredFieldValidator>
                                                </td>
                                                <td style="vertical-align: top; padding-top: 6px;">
                                                    <asp:Label ID="lblReceivedBy1" runat="server" Text="By" Visible="false"></asp:Label>
                                                </td>
                                                <td style="vertical-align: top; padding-left: 6px; text-align: left;">
                                                    <asp:TextBox Width="150" ValidationGroup="Generate" CssClass="input-text" ID="txtReceivedBy1" Visible="false"
                                                        runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ValidationGroup="Generate" ControlToValidate="txtReceivedBy1"
                                                        ID="RequiredFieldValidator11" ErrorMessage="Required!!!" runat="server"> </asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                        </table>
                                        <br />
                                    </div>
                                    <div class="box_c_heading cf">
                                        <div class="box_c_ico">
                                            <asp:Image runat="server" ID="img16List1" ImageUrl="pertho_admin_v1.3/img/ico/icSw2/16-List.png" AlternateText="" />
                                        </div>
                                        <p>Credit Transactions</p>
                                    </div>
                                    <div>
                                    </div>
                                    <table style="border: none;">
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblHeads1" runat="server" Text='<%# Eval("Heads1") %>' Visible="false" />
                                                <asp:Label ID="Label51" runat="server" Text="Heads"></asp:Label>
                                                <asp:DropDownList CssClass="chzn-select" Style="width: 350px !important;"
                                                    TabIndex="3" ID="ddlHeads1"
                                                    runat="server" OnChange="GetChequeNumber1();">
                                                </asp:DropDownList>
                                                <label id="lblheaderror1" style="width: 50px;"></label>
                                                <asp:HiddenField ID="HD_Headtxt1" runat="server" />
                                                <asp:HiddenField ID="HD_Headval1" runat="server" />
                                            </td>
                                            <td></td>
                                            <td>
                                                <asp:Label ID="lblamt1" runat="server" Text="Amount"></asp:Label><br />
                                                <asp:TextBox Width="150" TabIndex="4" Text='<%#Eval("Amount1") %>'
                                                    CssClass="twitterStyleTextbox  sp_currency" ID="txtAmount1" runat="server" onkeypress="NumberOnly(event,this);">
                                                </asp:TextBox>
                                                <label id="lblamnterr1" style="width: 50px;"></label>
                                            </td>
                                            <td></td>
                                            <td>
                                                <asp:Label ID="lblDesc1" runat="server" Text="Description"></asp:Label>
                                                <asp:TextBox TextMode="MultiLine" Height="50" TabIndex="7"
                                                    ValidationGroup="GrpRow1" CssClass="input-text" ID="txtDescription1" runat="server">
                                                </asp:TextBox>
                                                <label id="lbldesccr1" style="width: 50px;"></label>
                                            </td>
                                            <td></td>
                                            <td>
                                                <asp:Label ID="lblChequeNO1" runat="server" Text="Cheque NO"></asp:Label>
                                                <br />
                                                <asp:TextBox Width="100"
                                                    MaxLength="7" TabIndex="5" ID="txtChequeNO1"
                                                    runat="server">
                                                </asp:TextBox>
                                            </td>
                                            <td></td>
                                            <td colspan="7" align="right">
                                                <asp:ImageButton OnClick="btnAdd1_GridGuardians_RowCommand_click" ID="imgBtnAdd1"
                                                    runat="server" CausesValidation="false" Height="24" Visible="true"
                                                    OnClientClick="return ValidatetCreditHead1();"
                                                    ImageUrl="~/Styles/Image/Images/round_plus_16.png" ToolTip="Add New Transaction"
                                                    Width="24" />
                                            </td>
                                            <%-- <td style="width: 100px">           
                                                                        <asp:Button ID="btnAdd1" runat="server" Text="Add" TabIndex="6" />
                                                                        <button type="button" class="delete">Delete</button>
                                                                </td>--%>
                                        </tr>
                                    </table>
                                    <br />
                                    <asp:Label ID="lblcancelmsg1" runat="server" Text="" CssClass="lblstyle"></asp:Label>
                                    <div style="vertical-align: top; overflow: auto; height: 200px; margin-top: 3px; width: 950px; border: 2px solid gray; overflow-y: scroll;">
                                        <asp:GridView ID="GridClnt1" BorderStyle="Solid" runat="server"
                                            CellSpacing="2" Font-Names="Verdana" ForeColor="#333333" GridLines="Both" Height="100px"
                                            AutoGenerateColumns="false" CssClass="Trans twelve columns" Width="850px" OnRowDeleting="GridClnt1_RowDeleting">
                                            <RowStyle BackColor="#F7F6F3" />
                                            <RowStyle CssClass="GridViewRowStyle" />
                                            <HeaderStyle CssClass="GridViewHeaderStyle" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="Heads" ItemStyle-CssClass="Heads"
                                                    HeaderStyle-BorderColor="Gray" HeaderStyle-BorderStyle="Solid" HeaderStyle-BorderWidth="2px"
                                                    ItemStyle-BorderColor="Gray" ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Cr1_lblhd1" runat="server" Text='<%# Eval("Heads1") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle BorderColor="Gray" BorderStyle="Solid" BorderWidth="2px" />
                                                    <ItemStyle BorderColor="Gray" BorderStyle="Solid" BorderWidth="2px" CssClass="Heads" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Amount" ItemStyle-CssClass="Amount"
                                                    HeaderStyle-BorderColor="Gray" HeaderStyle-BorderStyle="Solid" HeaderStyle-BorderWidth="2px"
                                                    ItemStyle-BorderColor="Gray" ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Cr1_lblamnt1" runat="server" Text='<%# Eval("Amount1") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle BorderColor="Gray" BorderStyle="Solid" BorderWidth="2px" />
                                                    <ItemStyle BorderColor="Gray" BorderStyle="Solid" BorderWidth="2px" CssClass="Amount" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Description" ItemStyle-CssClass="Description"
                                                    HeaderStyle-BorderColor="Gray" HeaderStyle-BorderStyle="Solid" HeaderStyle-BorderWidth="2px"
                                                    ItemStyle-BorderColor="Gray" ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Cr1_desc1" runat="server" Text='<%# Eval("Description1") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle BorderColor="Gray" BorderStyle="Solid" BorderWidth="2px" />
                                                    <ItemStyle BorderColor="Gray" BorderStyle="Solid" BorderWidth="2px" CssClass="Description" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="ChequeNo" ItemStyle-CssClass="ChequeNo"
                                                    HeaderStyle-BorderColor="Gray" HeaderStyle-BorderStyle="Solid" HeaderStyle-BorderWidth="2px"
                                                    ItemStyle-BorderColor="Gray" ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Cr1_cheq1" runat="server" Text='<%# Eval("ChequeNo1") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle BorderColor="Gray" BorderStyle="Solid" BorderWidth="2px" />
                                                    <ItemStyle BorderColor="Gray" BorderStyle="Solid" BorderWidth="2px" CssClass="ChequeNo" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="headid" ItemStyle-CssClass="headid"
                                                    HeaderStyle-BorderColor="Gray" HeaderStyle-BorderStyle="Solid" HeaderStyle-BorderWidth="2px"
                                                    ItemStyle-BorderColor="Gray" ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Cr1_hdid1" runat="server" Text='<%# Eval("headid1") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle BorderColor="Gray" BorderStyle="Solid" BorderWidth="2px" />
                                                    <ItemStyle BorderColor="Gray" BorderStyle="Solid" BorderWidth="2px" CssClass="headid" />
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
                                </div>
                                <div></div>
                                <div></div>
                                <div class="box_c_heading cf">
                                    <div class="box_c_ico">
                                        <asp:Image runat="server" ID="img16List11" ImageUrl="pertho_admin_v1.3/img/ico/icSw2/16-List.png" AlternateText="" />
                                    </div>
                                    <p>Debit Transactions</p>
                                </div>
                                <table style="border: none;">
                                    <tr>
                                        <td style="height: 40px;">
                                            <asp:Label ID="lblDBHeads1" runat="server" Text='<%# Eval("Heads1") %>' Visible="false" />
                                            <asp:Label ID="Label61" runat="server" Text="Heads"></asp:Label>
                                            <asp:DropDownList CssClass="chzn-select" Style="width: 350px !important;" OnSelectedIndexChanged="ddlHeadsDebit1_SelectedIndexChanged"
                                                TabIndex="7" AutoPostBack="true" ID="ddlHeadsDebit1" runat="server">
                                            </asp:DropDownList>
                                            <label id="lbldeberror1" style="width: 50px;"></label>
                                            <asp:HiddenField ID="hidDb_Headtxt1" runat="server" />
                                            <asp:HiddenField ID="hidDb_HeadVal1" runat="server" />
                                        </td>
                                        <td></td>
                                        <td style="height: 40px;" align="left">
                                            <asp:Label ID="lbldbamt1" runat="server" Text="Amount"></asp:Label><br />
                                            <asp:TextBox ID="debitAmnt1" runat="server" TabIndex="8"></asp:TextBox>
                                            <label id="lbldbamnt1" style="width: 50px;"></label>
                                            <asp:HiddenField ID="hidden_totalcred1" runat="server" />
                                        </td>
                                        <td style="height: 40px;">
                                            <asp:Label ID="lbldbDesc1" runat="server" Text="Description"></asp:Label>
                                            <asp:TextBox TextMode="MultiLine" Height="50" TabIndex="9"
                                                ID="txtDebitdesc1" runat="server">
                                            </asp:TextBox>
                                            <label id="lbldbdesc1" style="width: 50px;"></label>
                                        </td>
                                        <td style="height: 40px;" align="left"></td>
                                        <td></td>
                                        <td>
                                            <asp:ImageButton OnClick="btnAdd1_GridGuardiansDebit_RowCommand_click" TabIndex="-1"
                                                ID="imgBtnAddDebit1" runat="server" CausesValidation="false" Height="24"
                                                OnClientClick="return GetDebitcheck1();" Visible="true"
                                                ImageUrl="~/Styles/Image/Images/round_plus_16.png" ToolTip="Add New Transaction"
                                                Width="24" />
                                        </td>
                                    </tr>
                                </table>
                                <div style="vertical-align: top; overflow: auto; height: 200px; margin-top: 3px; width: 950px; border: 2px solid gray; overflow-y: scroll;">
                                    <asp:GridView ID="GrdDbClnt1" BorderStyle="Solid" runat="server"
                                        CellSpacing="2" Font-Names="Verdana" ForeColor="#333333" GridLines="Both" Height="100px"
                                        AutoGenerateColumns="false" CssClass="Trans twelve columns" Width="850px" OnRowDeleting="GrdDbClnt1_RowDeleting">
                                        <RowStyle BackColor="#F7F6F3" />
                                        <RowStyle CssClass="GridViewRowStyle" />
                                        <HeaderStyle CssClass="GridViewHeaderStyle" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="DbHeads" ItemStyle-CssClass="DbHeads"
                                                HeaderStyle-BorderColor="Gray" HeaderStyle-BorderStyle="Solid" HeaderStyle-BorderWidth="2px"
                                                ItemStyle-BorderColor="Gray" ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px">
                                                <ItemTemplate>
                                                    <asp:Label ID="Db1_hd1" runat="server" Text='<%# Eval("DbHeads1") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle BorderColor="Gray" BorderStyle="Solid" BorderWidth="2px" />
                                                <ItemStyle BorderColor="Gray" BorderStyle="Solid" BorderWidth="2px" CssClass="DbHeads" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="DbAmount" ItemStyle-CssClass="DbAmount"
                                                HeaderStyle-BorderColor="Gray" HeaderStyle-BorderStyle="Solid" HeaderStyle-BorderWidth="2px"
                                                ItemStyle-BorderColor="Gray" ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px">
                                                <ItemTemplate>
                                                    <asp:Label ID="Db1_amnt1" runat="server" Text='<%# Eval("DbAmount1") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle BorderColor="Gray" BorderStyle="Solid" BorderWidth="2px" />
                                                <ItemStyle BorderColor="Gray" BorderStyle="Solid" BorderWidth="2px" CssClass="DbAmount" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="DbDescription1" ItemStyle-CssClass="DbDescription"
                                                HeaderStyle-BorderColor="Gray" HeaderStyle-BorderStyle="Solid" HeaderStyle-BorderWidth="2px"
                                                ItemStyle-BorderColor="Gray" ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px">
                                                <ItemTemplate>
                                                    <asp:Label ID="Db1_desc1" runat="server" Text='<%# Eval("DbDescription1") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle BorderColor="Gray" BorderStyle="Solid" BorderWidth="2px" />
                                                <ItemStyle BorderColor="Gray" BorderStyle="Solid" BorderWidth="2px" CssClass="DbDescription" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Dbheadid" ItemStyle-CssClass="Dbheadid"
                                                HeaderStyle-BorderColor="Gray" HeaderStyle-BorderStyle="Solid" HeaderStyle-BorderWidth="2px"
                                                ItemStyle-BorderColor="Gray" ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px">
                                                <ItemTemplate>
                                                    <asp:Label ID="Db1_hdid1" runat="server" Text='<%# Eval("Dbheadid1") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle BorderColor="Gray" BorderStyle="Solid" BorderWidth="2px" />
                                                <ItemStyle BorderColor="Gray" BorderStyle="Solid" BorderWidth="2px" CssClass="Dbheadid" />
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
                            </div>

                            <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
                                <ProgressTemplate>
                                    <div>
                                        <div style="position: absolute; left: 50%; margin-left: -50px; top: 186px;">
                                            <asp:Image AlternateText="waiting" runat="server" ID="imgWaiting1" Style="vertical-align: middle;" />
                                        </div>
                                    </div>
                                </ProgressTemplate>
                            </asp:UpdateProgress>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </asp:Panel>
    <asp:Panel CssClass="row" ID="Panel_P2" runat="server" DefaultButton="btnGenerate"
        class="content">
        <div class="twelve columns">
            <div class="box_c">
                <div class="box_c_heading  box_actions">
                    <p>Voucher Details - Branch II</p>
                </div>
                <div class="box_c_content">
                    <div class="row">
                        <br />
                        <div style="display: inline; zoom: 1; text-align: center;">
                            <div style="margin-left: auto; margin-right: auto; display: table;">
                                <table cellspacing="4px" width="100%">
                                    <tr>
                                        <td style="vertical-align: top; padding-top: 6px;">
                                            <asp:Label ID="lblDate2" runat="server" Text="Date"></asp:Label>
                                        </td>
                                        <td style="vertical-align: top; padding-left: 6px; text-align: left;">
                                            <asp:TextBox AutoPostBack="true" Width="100" ValidationGroup="Generate" TabIndex="1" ID="txtDate2" onchange="CheckDate2();"
                                                CssClass="input-text maskdate" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ForeColor="Red" ID="RequiredFieldValidator62" ErrorMessage="Required!!!"
                                                ControlToValidate="txtDate2" ValidationGroup="Generate" Display="Dynamic" runat="server"></asp:RequiredFieldValidator>
                                            <asp:CompareValidator ForeColor="Red" Type="Date" ID="CompareValidator112" ValidationGroup="Generate"
                                                ControlToValidate="txtDate2" Display="Dynamic" Operator="DataTypeCheck" runat="server"
                                                ErrorMessage="Incorrect!!!"></asp:CompareValidator>
                                            <asp:RangeValidator ID="rvDate2" EnableClientScript="false" runat="server"
                                                Type="Date" ControlToValidate="txtDate2" ValidationGroup="Generate" ForeColor="Red" Display="Dynamic"
                                                ErrorMessage="*"></asp:RangeValidator>
                                        </td>
                                        <td style="vertical-align: top; padding-top: 6px;">
                                            <asp:Label ID="lblSeries2" runat="server" Text="Series" Visible="false"></asp:Label>
                                        </td>
                                        <td style="vertical-align: top; padding-left: 6px; text-align: left;">
                                            <asp:TextBox Width="100" ValidationGroup="Generate" ID="txtSeries2" Visible="false"
                                                CssClass="input-text" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator Display="Dynamic" ValidationGroup="Generate" ControlToValidate="txtSeries2"
                                                ID="RequiredFieldValidator112" ErrorMessage="Required!!!" runat="server"> </asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator Display="Dynamic" ValidationGroup="Generate" ID="RegularExpressionValidator22"
                                                runat="server" ControlToValidate="txtSeries2" ErrorMessage="Alphabets only!!!"
                                                ValidationExpression="^[a-zA-Z]+$"></asp:RegularExpressionValidator>
                                        </td>
                                        <td style="vertical-align: top; padding-top: 6px;">
                                            <asp:Label ID="lblVoucherNo2" runat="server" Text="Voucher Number" Visible="true"></asp:Label>
                                        </td>
                                        <td style="vertical-align: top; padding-left: 6px; text-align: left;">
                                            <asp:TextBox Width="100" TabIndex="2" ValidationGroup="Generate" ID="txtVoucherNo2" ReadOnly="true"
                                                CssClass="input-text sp_number" Visible="true" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator Display="Dynamic" ValidationGroup="Generate" ControlToValidate="txtVoucherNo2"
                                                ID="RequiredFieldValidator122" ErrorMessage="Required!!!" runat="server"> </asp:RequiredFieldValidator>
                                        </td>
                                        <td style="vertical-align: top; padding-top: 6px;">
                                            <asp:Label ID="lblReceivedBy2" runat="server" Text="By" Visible="false"></asp:Label>
                                        </td>
                                        <td style="vertical-align: top; padding-left: 6px; text-align: left;">
                                            <asp:TextBox Width="150" ValidationGroup="Generate" CssClass="input-text" ID="txtReceivedBy2" Visible="false"
                                                runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ValidationGroup="Generate" ControlToValidate="txtReceivedBy2"
                                                ID="RequiredFieldValidator12" ErrorMessage="Required!!!" runat="server"> </asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                </table>
                                <br />
                            </div>
                            <div class="box_c_heading cf">
                                <div class="box_c_ico">
                                    <asp:Image runat="server" ID="img16List2" ImageUrl="pertho_admin_v1.3/img/ico/icSw2/16-List.png" AlternateText="" />
                                </div>
                                <p>Credit Transactions</p>
                            </div>
                            <div>
                            </div>
                            <table style="border: none;">
                                <tr>
                                    <td>
                                        <asp:Label ID="lblHeads2" runat="server" Text='<%# Eval("Heads2") %>' Visible="false" />
                                        <asp:Label ID="Label52" runat="server" Text="Heads"></asp:Label>
                                        <asp:DropDownList CssClass="chzn-select" Style="width: 350px !important;"
                                            TabIndex="3" ID="ddlHeads2"
                                            runat="server" OnChange="GetChequeNumber2();">
                                        </asp:DropDownList>
                                        <label id="lblheaderror2" style="width: 50px;"></label>
                                        <asp:HiddenField ID="HD_Headtxt2" runat="server" />
                                        <asp:HiddenField ID="HD_Headval2" runat="server" />
                                    </td>
                                    <td></td>
                                    <td>
                                        <asp:Label ID="lblamt2" runat="server" Text="Amount"></asp:Label><br />
                                        <asp:TextBox Width="150" TabIndex="4" Text='<%#Eval("Amount2") %>'
                                            CssClass="twitterStyleTextbox  sp_currency" ID="txtAmount2" runat="server" onkeypress="NumberOnly(event,this);">
                                        </asp:TextBox>
                                        <label id="lblamnterr2" style="width: 50px;"></label>
                                    </td>
                                    <td></td>
                                    <td>
                                        <asp:Label ID="lblDesc2" runat="server" Text="Description"></asp:Label>
                                        <asp:TextBox TextMode="MultiLine" Height="50" TabIndex="7"
                                            ValidationGroup="GrpRow2" CssClass="input-text" ID="txtDescription2" runat="server">
                                        </asp:TextBox>
                                        <label id="lbldesccr2" style="width: 50px;"></label>
                                    </td>
                                    <td></td>
                                    <td>
                                        <asp:Label ID="lblChequeNO2" runat="server" Text="Cheque NO"></asp:Label>
                                        <br />
                                        <asp:TextBox Width="100"
                                            MaxLength="7" TabIndex="5" ID="txtChequeNO2"
                                            runat="server">
                                        </asp:TextBox>
                                    </td>
                                    <td></td>
                                    <td colspan="7" align="right">
                                        <asp:ImageButton OnClick="btnAdd2_GridGuardians_RowCommand_click" ID="imgBtnAdd2"
                                            runat="server" CausesValidation="false" Height="24" Visible="true"
                                            OnClientClick="return ValidatetCreditHead2();"
                                            ImageUrl="~/Styles/Image/Images/round_plus_16.png" ToolTip="Add New Transaction"
                                            Width="24" />
                                    </td>
                                    <%--     <td style="width: 100px">
                                                <asp:Button ID="btnAdd2" runat="server" Text="Add" TabIndex="6" />
                                                <button type="button" class="delete">Delete</button>
                                            </td>--%>
                                </tr>
                            </table>
                            <br />
                            <asp:Label ID="lblcancelmsg2" runat="server" Text="" CssClass="lblstyle"></asp:Label>
                            <div style="vertical-align: top; overflow: auto; height: 200px; margin-top: 3px; width: 950px; border: 2px solid gray; overflow-y: scroll;">
                                <asp:GridView ID="GridClnt2" BorderStyle="Solid" runat="server"
                                    CellSpacing="2" Font-Names="Verdana" ForeColor="#333333" GridLines="Both" Height="100px"
                                    AutoGenerateColumns="false" CssClass="Trans twelve columns" Width="850px" OnRowDeleting="GridClnt2_RowDeleting">
                                    <RowStyle BackColor="#F7F6F3" />
                                    <RowStyle CssClass="GridViewRowStyle" />
                                    <HeaderStyle CssClass="GridViewHeaderStyle" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Heads2" ItemStyle-CssClass="Heads"
                                            HeaderStyle-BorderColor="Gray" HeaderStyle-BorderStyle="Solid" HeaderStyle-BorderWidth="2px"
                                            ItemStyle-BorderColor="Gray" ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px">
                                            <ItemTemplate>
                                                <asp:Label ID="Cr2_hds2" runat="server" Text='<%# Eval("Heads2") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle BorderColor="Gray" BorderStyle="Solid" BorderWidth="2px" />
                                            <ItemStyle BorderColor="Gray" BorderStyle="Solid" BorderWidth="2px" CssClass="Heads" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Amount2" ItemStyle-CssClass="Amount"
                                            HeaderStyle-BorderColor="Gray" HeaderStyle-BorderStyle="Solid" HeaderStyle-BorderWidth="2px"
                                            ItemStyle-BorderColor="Gray" ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px">
                                            <ItemTemplate>
                                                <asp:Label ID="Cr2_amnt2" runat="server" Text='<%# Eval("Amount2") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle BorderColor="Gray" BorderStyle="Solid" BorderWidth="2px" />
                                            <ItemStyle BorderColor="Gray" BorderStyle="Solid" BorderWidth="2px" CssClass="Amount" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Description2" ItemStyle-CssClass="Description"
                                            HeaderStyle-BorderColor="Gray" HeaderStyle-BorderStyle="Solid" HeaderStyle-BorderWidth="2px"
                                            ItemStyle-BorderColor="Gray" ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px">
                                            <ItemTemplate>
                                                <asp:Label ID="Cr2_desc2" runat="server" Text='<%# Eval("Description2") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle BorderColor="Gray" BorderStyle="Solid" BorderWidth="2px" />
                                            <ItemStyle BorderColor="Gray" BorderStyle="Solid" BorderWidth="2px" CssClass="Description" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ChequeNo2" ItemStyle-CssClass="ChequeNo"
                                            HeaderStyle-BorderColor="Gray" HeaderStyle-BorderStyle="Solid" HeaderStyle-BorderWidth="2px"
                                            ItemStyle-BorderColor="Gray" ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px">
                                            <ItemTemplate>
                                                <asp:Label ID="Cr2_chq2" runat="server" Text='<%# Eval("ChequeNo2") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle BorderColor="Gray" BorderStyle="Solid" BorderWidth="2px" />
                                            <ItemStyle BorderColor="Gray" BorderStyle="Solid" BorderWidth="2px" CssClass="ChequeNo" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="headid2" ItemStyle-CssClass="headid"
                                            HeaderStyle-BorderColor="Gray" HeaderStyle-BorderStyle="Solid" HeaderStyle-BorderWidth="2px"
                                            ItemStyle-BorderColor="Gray" ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px">
                                            <ItemTemplate>
                                                <asp:Label ID="Cr2_hdid2" runat="server" Text='<%# Eval("headid2") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle BorderColor="Gray" BorderStyle="Solid" BorderWidth="2px" />
                                            <ItemStyle BorderColor="Gray" BorderStyle="Solid" BorderWidth="2px" CssClass="headid" />
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
                        </div>
                        <div></div>
                        <div></div>
                        <div class="box_c_heading cf">
                            <div class="box_c_ico">
                                <asp:Image runat="server" ID="img16List12" ImageUrl="pertho_admin_v1.3/img/ico/icSw2/16-List.png" AlternateText="" />
                            </div>
                            <p>Debit Transactions</p>
                        </div>
                        <table style="border: none;">
                            <tr>
                                <td style="height: 40px;">
                                    <asp:Label ID="lblDBHeads2" runat="server" Text='<%# Eval("Heads2") %>' Visible="false" />
                                    <asp:Label ID="Label62" runat="server" Text="Heads"></asp:Label>
                                    <asp:DropDownList CssClass="chzn-select" Style="width: 350px !important;" OnSelectedIndexChanged="ddlHeadsDebit2_SelectedIndexChanged"
                                        TabIndex="7" AutoPostBack="true" ID="ddlHeadsDebit2" runat="server">
                                    </asp:DropDownList>
                                    <label id="lbldeberror2" style="width: 50px;"></label>
                                    <asp:HiddenField ID="hidDb_Headtxt2" runat="server" />
                                    <asp:HiddenField ID="hidDb_HeadVal2" runat="server" />
                                </td>
                                <td></td>
                                <td style="height: 40px;" align="left">
                                    <asp:Label ID="lbldbamt2" runat="server" Text="Amount"></asp:Label><br />
                                    <asp:TextBox ID="debitAmnt2" runat="server" TabIndex="8"></asp:TextBox>
                                    <label id="lbldbamnt2" style="width: 50px;"></label>
                                    <asp:HiddenField ID="hidden_totalcred2" runat="server" />
                                </td>
                                <td style="height: 40px;">
                                    <asp:Label ID="lbldbDesc2" runat="server" Text="Description"></asp:Label>
                                    <asp:TextBox TextMode="MultiLine" Height="50" TabIndex="9"
                                        ID="txtDebitdesc2" runat="server">
                                    </asp:TextBox>
                                    <label id="lbldbdesc2" style="width: 50px;"></label>
                                </td>
                                <td style="height: 40px;" align="left"></td>
                                <td></td>
                                <td>
                                    <asp:ImageButton OnClick="btnAdd2_GridGuardiansDebit_RowCommand_click" TabIndex="-1"
                                        ID="imgBtnAddDebit2" runat="server" CausesValidation="false" Height="24"
                                        OnClientClick="return GetDebitcheck2();" Visible="true"
                                        ImageUrl="~/Styles/Image/Images/round_plus_16.png" ToolTip="Add New Transaction"
                                        Width="24" />
                                </td>
                                <%--  <td style="width: 100px">
                                            <asp:Button ID="btnDbAdd2" runat="server" Text="Add" />
                                            <button type="button" class="delete">Delete</button>
                                        </td>--%>
                            </tr>
                        </table>
                        <div style="vertical-align: top; overflow: auto; height: 200px; margin-top: 3px; width: 950px; border: 2px solid gray; overflow-y: scroll;">
                            <asp:GridView ID="GrdDbClnt2" BorderStyle="Solid" runat="server"
                                CellSpacing="2" Font-Names="Verdana" ForeColor="#333333" GridLines="Both" Height="100px"
                                AutoGenerateColumns="false" CssClass="Trans twelve columns" Width="850px" OnRowDeleting="GrdDbClnt2_RowDeleting">
                                <RowStyle BackColor="#F7F6F3" />
                                <RowStyle CssClass="GridViewRowStyle" />
                                <HeaderStyle CssClass="GridViewHeaderStyle" />
                                <Columns>
                                    <asp:TemplateField HeaderText="DbHeads" ItemStyle-CssClass="DbHeads"
                                        HeaderStyle-BorderColor="Gray" HeaderStyle-BorderStyle="Solid" HeaderStyle-BorderWidth="2px"
                                        ItemStyle-BorderColor="Gray" ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px">
                                        <ItemTemplate>
                                            <asp:Label ID="Db2_hds2" runat="server" Text='<%# Eval("DbHeads2") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle BorderColor="Gray" BorderStyle="Solid" BorderWidth="2px" />
                                        <ItemStyle BorderColor="Gray" BorderStyle="Solid" BorderWidth="2px" CssClass="DbHeads" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="DbAmount" ItemStyle-CssClass="DbAmount"
                                        HeaderStyle-BorderColor="Gray" HeaderStyle-BorderStyle="Solid" HeaderStyle-BorderWidth="2px"
                                        ItemStyle-BorderColor="Gray" ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px">
                                        <ItemTemplate>
                                            <asp:Label ID="Db2_amnt2" runat="server" Text='<%# Eval("DbAmount2") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle BorderColor="Gray" BorderStyle="Solid" BorderWidth="2px" />
                                        <ItemStyle BorderColor="Gray" BorderStyle="Solid" BorderWidth="2px" CssClass="DbAmount" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="DbDescription" ItemStyle-CssClass="DbDescription"
                                        HeaderStyle-BorderColor="Gray" HeaderStyle-BorderStyle="Solid" HeaderStyle-BorderWidth="2px"
                                        ItemStyle-BorderColor="Gray" ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px">
                                        <ItemTemplate>
                                            <asp:Label ID="Db2_desc2" runat="server" Text='<%# Eval("DbDescription2") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle BorderColor="Gray" BorderStyle="Solid" BorderWidth="2px" />
                                        <ItemStyle BorderColor="Gray" BorderStyle="Solid" BorderWidth="2px" CssClass="DbDescription" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Dbheadid" ItemStyle-CssClass="Dbheadid"
                                        HeaderStyle-BorderColor="Gray" HeaderStyle-BorderStyle="Solid" HeaderStyle-BorderWidth="2px"
                                        ItemStyle-BorderColor="Gray" ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px">
                                        <ItemTemplate>
                                            <asp:Label ID="Db2_hdid2" runat="server" Text='<%# Eval("Dbheadid2") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle BorderColor="Gray" BorderStyle="Solid" BorderWidth="2px" />
                                        <ItemStyle BorderColor="Gray" BorderStyle="Solid" BorderWidth="2px" CssClass="Dbheadid" />
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
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>
    <asp:Panel CssClass="row" ID="Panel_P3" runat="server" DefaultButton="btnGenerate"
        class="content">
        <div class="twelve columns">
            <div class="box_c">
                <div class="box_c_heading  box_actions">
                    <p>Voucher Details - Branch III</p>
                </div>
                <div class="box_c_content">
                    <div class="row">
                        <br />
                        <div style="display: inline; zoom: 1; text-align: center;">
                            <div style="margin-left: auto; margin-right: auto; display: table;">
                                <table cellspacing="4px" width="100%">
                                    <tr>
                                        <td style="vertical-align: top; padding-top: 6px;">
                                            <asp:Label ID="lblDate3" runat="server" Text="Date"></asp:Label>
                                        </td>
                                        <td style="vertical-align: top; padding-left: 6px; text-align: left;">
                                            <asp:TextBox AutoPostBack="true" Width="100" ValidationGroup="Generate" TabIndex="1" ID="txtDate3" onchange="CheckDate3();"
                                                CssClass="input-text maskdate" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ForeColor="Red" ID="RequiredFieldValidator63" ErrorMessage="Required!!!"
                                                ControlToValidate="txtDate3" ValidationGroup="Generate" Display="Dynamic" runat="server"></asp:RequiredFieldValidator>
                                            <asp:CompareValidator ForeColor="Red" Type="Date" ID="CompareValidator113" ValidationGroup="Generate"
                                                ControlToValidate="txtDate3" Display="Dynamic" Operator="DataTypeCheck" runat="server"
                                                ErrorMessage="Incorrect!!!"></asp:CompareValidator>
                                            <asp:RangeValidator ID="rvDate3" EnableClientScript="false" runat="server"
                                                Type="Date" ControlToValidate="txtDate3" ValidationGroup="Generate" ForeColor="Red" Display="Dynamic"
                                                ErrorMessage="*"></asp:RangeValidator>
                                        </td>
                                        <td style="vertical-align: top; padding-top: 6px;">
                                            <asp:Label ID="lblSeries3" runat="server" Text="Series" Visible="false"></asp:Label>
                                        </td>
                                        <td style="vertical-align: top; padding-left: 6px; text-align: left;">
                                            <asp:TextBox Width="100" ValidationGroup="Generate" ID="txtSeries3" Visible="false"
                                                CssClass="input-text" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator Display="Dynamic" ValidationGroup="Generate" ControlToValidate="txtSeries3"
                                                ID="RequiredFieldValidator113" ErrorMessage="Required!!!" runat="server"> </asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator Display="Dynamic" ValidationGroup="Generate" ID="RegularExpressionValidator23"
                                                runat="server" ControlToValidate="txtSeries3" ErrorMessage="Alphabets only!!!"
                                                ValidationExpression="^[a-zA-Z]+$"></asp:RegularExpressionValidator>
                                        </td>
                                        <td style="vertical-align: top; padding-top: 6px;">
                                            <asp:Label ID="lblVoucherNo3" runat="server" Text="Voucher Number" Visible="true"></asp:Label>
                                        </td>
                                        <td style="vertical-align: top; padding-left: 6px; text-align: left;">
                                            <asp:TextBox Width="100" TabIndex="2" ValidationGroup="Generate" ID="txtVoucherNo3" ReadOnly="true"
                                                CssClass="input-text sp_number" Visible="true" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator Display="Dynamic" ValidationGroup="Generate" ControlToValidate="txtVoucherNo3"
                                                ID="RequiredFieldValidator123" ErrorMessage="Required!!!" runat="server"> </asp:RequiredFieldValidator>
                                        </td>
                                        <td style="vertical-align: top; padding-top: 6px;">
                                            <asp:Label ID="lblReceivedBy3" runat="server" Text="By" Visible="false"></asp:Label>
                                        </td>
                                        <td style="vertical-align: top; padding-left: 6px; text-align: left;">
                                            <asp:TextBox Width="150" ValidationGroup="Generate" CssClass="input-text" ID="txtReceivedBy3" Visible="false"
                                                runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ValidationGroup="Generate" ControlToValidate="txtReceivedBy3"
                                                ID="RequiredFieldValidator13" ErrorMessage="Required!!!" runat="server"> </asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                </table>
                                <br />
                            </div>
                            <div class="box_c_heading cf">
                                <div class="box_c_ico">
                                    <asp:Image runat="server" ID="img16List3" ImageUrl="pertho_admin_v1.3/img/ico/icSw2/16-List.png" AlternateText="" />
                                </div>
                                <p>Credit Transactions</p>
                            </div>
                            <div>
                            </div>
                            <table style="border: none;">
                                <tr>
                                    <td>
                                        <asp:Label ID="lblHeads3" runat="server" Text='<%# Eval("Heads3") %>' Visible="false" />
                                        <asp:Label ID="Label53" runat="server" Text="Heads"></asp:Label>
                                        <asp:DropDownList CssClass="chzn-select" Style="width: 350px !important;"
                                            TabIndex="3" ID="ddlHeads3"
                                            runat="server" OnChange="GetChequeNumber3();">
                                        </asp:DropDownList>
                                        <label id="lblheaderror3" style="width: 50px;"></label>
                                        <asp:HiddenField ID="HD_Headtxt3" runat="server" />
                                        <asp:HiddenField ID="HD_Headval3" runat="server" />
                                    </td>
                                    <td></td>
                                    <td>
                                        <asp:Label ID="lblamt3" runat="server" Text="Amount"></asp:Label><br />
                                        <asp:TextBox Width="150" TabIndex="4" Text='<%#Eval("Amount3") %>'
                                            CssClass="twitterStyleTextbox  sp_currency" ID="txtAmount3" runat="server" onkeypress="NumberOnly(event,this);">
                                        </asp:TextBox>
                                        <label id="lblamnterr3" style="width: 50px;"></label>
                                    </td>
                                    <td></td>
                                    <td>
                                        <asp:Label ID="lblDesc3" runat="server" Text="Description"></asp:Label>
                                        <asp:TextBox TextMode="MultiLine" Height="50" TabIndex="7"
                                            ValidationGroup="GrpRow3" CssClass="input-text" ID="txtDescription3" runat="server">
                                        </asp:TextBox>
                                        <label id="lbldesccr3" style="width: 50px;"></label>
                                    </td>
                                    <td></td>
                                    <td>
                                        <asp:Label ID="lblChequeNO3" runat="server" Text="Cheque NO"></asp:Label>
                                        <br />
                                        <asp:TextBox Width="100"
                                            MaxLength="7" TabIndex="5" ID="txtChequeNO3"
                                            runat="server">
                                        </asp:TextBox>
                                    </td>
                                    <td></td>
                                    <td colspan="7" align="right">
                                        <asp:ImageButton OnClick="btnAdd3_GridGuardians_RowCommand_click" ID="imgBtnAdd3"
                                            runat="server" CausesValidation="false" Height="24" Visible="true"
                                            OnClientClick="return ValidatetCreditHead3();"
                                            ImageUrl="~/Styles/Image/Images/round_plus_16.png" ToolTip="Add New Transaction"
                                            Width="24" />
                                    </td>
                                    <%-- <td style="width: 100px">
                                                <asp:Button ID="btnAdd3" runat="server" Text="Add" TabIndex="6" />
                                                <button type="button" class="delete">Delete</button>
                                            </td>--%>
                                </tr>
                            </table>
                            <br />
                            <asp:Label ID="lblcancelmsg3" runat="server" Text="" CssClass="lblstyle"></asp:Label>
                            <div style="vertical-align: top; overflow: auto; height: 200px; margin-top: 3px; width: 950px; border: 2px solid gray; overflow-y: scroll;">
                                <asp:GridView ID="GridClnt3" BorderStyle="Solid" runat="server"
                                    CellSpacing="2" Font-Names="Verdana" ForeColor="#333333" GridLines="Both" Height="100px"
                                    AutoGenerateColumns="false" CssClass="Trans twelve columns" Width="850px" OnRowDeleting="GridClnt3_RowDeleting">
                                    <RowStyle BackColor="#F7F6F3" />
                                    <RowStyle CssClass="GridViewRowStyle" />
                                    <HeaderStyle CssClass="GridViewHeaderStyle" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Heads" ItemStyle-CssClass="Heads"
                                            HeaderStyle-BorderColor="Gray" HeaderStyle-BorderStyle="Solid" HeaderStyle-BorderWidth="2px"
                                            ItemStyle-BorderColor="Gray" ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px">
                                            <ItemTemplate>
                                                <asp:Label ID="Cr3_hds3" runat="server" Text='<%# Eval("Heads3") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle BorderColor="Gray" BorderStyle="Solid" BorderWidth="2px" />
                                            <ItemStyle BorderColor="Gray" BorderStyle="Solid" BorderWidth="2px" CssClass="Heads" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Amount" ItemStyle-CssClass="Amount"
                                            HeaderStyle-BorderColor="Gray" HeaderStyle-BorderStyle="Solid" HeaderStyle-BorderWidth="2px"
                                            ItemStyle-BorderColor="Gray" ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px">
                                            <ItemTemplate>
                                                <asp:Label ID="Cr3_amnt3" runat="server" Text='<%# Eval("Amount3") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle BorderColor="Gray" BorderStyle="Solid" BorderWidth="2px" />
                                            <ItemStyle BorderColor="Gray" BorderStyle="Solid" BorderWidth="2px" CssClass="Amount" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Description" ItemStyle-CssClass="Description"
                                            HeaderStyle-BorderColor="Gray" HeaderStyle-BorderStyle="Solid" HeaderStyle-BorderWidth="2px"
                                            ItemStyle-BorderColor="Gray" ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px">
                                            <ItemTemplate>
                                                <asp:Label ID="Cr3_desc3" runat="server" Text='<%# Eval("Description3") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle BorderColor="Gray" BorderStyle="Solid" BorderWidth="2px" />
                                            <ItemStyle BorderColor="Gray" BorderStyle="Solid" BorderWidth="2px" CssClass="Description" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ChequeNo" ItemStyle-CssClass="ChequeNo"
                                            HeaderStyle-BorderColor="Gray" HeaderStyle-BorderStyle="Solid" HeaderStyle-BorderWidth="2px"
                                            ItemStyle-BorderColor="Gray" ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px">
                                            <ItemTemplate>
                                                <asp:Label ID="Cr3_chq3" runat="server" Text='<%# Eval("ChequeNo3") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle BorderColor="Gray" BorderStyle="Solid" BorderWidth="2px" />
                                            <ItemStyle BorderColor="Gray" BorderStyle="Solid" BorderWidth="2px" CssClass="ChequeNo" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="headid" ItemStyle-CssClass="headid"
                                            HeaderStyle-BorderColor="Gray" HeaderStyle-BorderStyle="Solid" HeaderStyle-BorderWidth="2px"
                                            ItemStyle-BorderColor="Gray" ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px">
                                            <ItemTemplate>
                                                <asp:Label ID="Cr3_hdid3" runat="server" Text='<%# Eval("headid3") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle BorderColor="Gray" BorderStyle="Solid" BorderWidth="2px" />
                                            <ItemStyle BorderColor="Gray" BorderStyle="Solid" BorderWidth="2px" CssClass="headid" />
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
                        </div>
                        <div></div>
                        <div></div>
                        <div class="box_c_heading cf">
                            <div class="box_c_ico">
                                <asp:Image runat="server" ID="img16List13" ImageUrl="pertho_admin_v1.3/img/ico/icSw2/16-List.png" AlternateText="" />
                            </div>
                            <p>Debit Transactions</p>
                        </div>
                        <table style="border: none;">
                            <tr>
                                <td style="height: 40px;">
                                    <asp:Label ID="lblDBHeads3" runat="server" Text='<%# Eval("Heads3") %>' Visible="false" />
                                    <asp:Label ID="Label63" runat="server" Text="Heads"></asp:Label>
                                    <asp:DropDownList CssClass="chzn-select" Style="width: 350px !important;" OnSelectedIndexChanged="ddlHeadsDebit3_SelectedIndexChanged"
                                        TabIndex="7" AutoPostBack="true" ID="ddlHeadsDebit3" runat="server">
                                    </asp:DropDownList>
                                    <label id="lbldeberror3" style="width: 50px;"></label>
                                    <asp:HiddenField ID="hidDb_Headtxt3" runat="server" />
                                    <asp:HiddenField ID="hidDb_HeadVal3" runat="server" />
                                </td>
                                <td></td>
                                <td style="height: 40px;" align="left">
                                    <asp:Label ID="lbldbamt3" runat="server" Text="Amount"></asp:Label><br />
                                    <asp:TextBox ID="debitAmnt3" runat="server" TabIndex="8"></asp:TextBox>
                                    <label id="lbldbamnt3" style="width: 50px;"></label>
                                    <asp:HiddenField ID="hidden_totalcred3" runat="server" />
                                </td>
                                <td style="height: 40px;">
                                    <asp:Label ID="lbldbDesc3" runat="server" Text="Description"></asp:Label>
                                    <asp:TextBox TextMode="MultiLine" Height="50" TabIndex="9"
                                        ID="txtDebitdesc3" runat="server">
                                    </asp:TextBox>
                                    <label id="lbldbdesc3" style="width: 50px;"></label>
                                </td>
                                <td style="height: 40px;" align="left"></td>
                                <td></td>
                                <td>
                                    <asp:ImageButton OnClick="btnAdd3_GridGuardiansDebit_RowCommand_click" TabIndex="-1"
                                        ID="imgBtnAddDebit3" runat="server" CausesValidation="false" Height="24"
                                        OnClientClick="return GetDebitcheck3();" Visible="true"
                                        ImageUrl="~/Styles/Image/Images/round_plus_16.png" ToolTip="Add New Transaction"
                                        Width="24" />
                                </td>
                                <%-- <td style="width: 100px">
                                            <asp:Button ID="btnDbAdd3" runat="server" Text="Add" />
                                            <button type="button" class="delete">Delete</button>
                                        </td>--%>
                            </tr>
                        </table>
                        <div style="vertical-align: top; overflow: auto; height: 200px; margin-top: 3px; width: 950px; border: 2px solid gray; overflow-y: scroll;">
                            <asp:GridView ID="GrdDbClnt3" BorderStyle="Solid" runat="server"
                                CellSpacing="2" Font-Names="Verdana" ForeColor="#333333" GridLines="Both" Height="100px"
                                AutoGenerateColumns="false" CssClass="Trans twelve columns" Width="850px" OnRowDeleting="GrdDbClnt3_RowDeleting">
                                <RowStyle BackColor="#F7F6F3" />
                                <RowStyle CssClass="GridViewRowStyle" />
                                <HeaderStyle CssClass="GridViewHeaderStyle" />
                                <Columns>
                                    <asp:TemplateField HeaderText="DbHeads" ItemStyle-CssClass="DbHeads"
                                        HeaderStyle-BorderColor="Gray" HeaderStyle-BorderStyle="Solid" HeaderStyle-BorderWidth="2px"
                                        ItemStyle-BorderColor="Gray" ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px">
                                        <ItemTemplate>
                                            <asp:Label ID="Db3_hds3" runat="server" Text='<%# Eval("DbHeads3") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle BorderColor="Gray" BorderStyle="Solid" BorderWidth="2px" />
                                        <ItemStyle BorderColor="Gray" BorderStyle="Solid" BorderWidth="2px" CssClass="DbHeads" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="DbAmount" ItemStyle-CssClass="DbAmount"
                                        HeaderStyle-BorderColor="Gray" HeaderStyle-BorderStyle="Solid" HeaderStyle-BorderWidth="2px"
                                        ItemStyle-BorderColor="Gray" ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px">
                                        <ItemTemplate>
                                            <asp:Label ID="Db3_amnt3" runat="server" Text='<%# Eval("DbAmount3") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle BorderColor="Gray" BorderStyle="Solid" BorderWidth="2px" />
                                        <ItemStyle BorderColor="Gray" BorderStyle="Solid" BorderWidth="2px" CssClass="DbAmount" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="DbDescription" ItemStyle-CssClass="DbDescription"
                                        HeaderStyle-BorderColor="Gray" HeaderStyle-BorderStyle="Solid" HeaderStyle-BorderWidth="2px"
                                        ItemStyle-BorderColor="Gray" ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px">
                                        <ItemTemplate>
                                            <asp:Label ID="Db3_desc3" runat="server" Text='<%# Eval("DbDescription3") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle BorderColor="Gray" BorderStyle="Solid" BorderWidth="2px" />
                                        <ItemStyle BorderColor="Gray" BorderStyle="Solid" BorderWidth="2px" CssClass="DbDescription" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Dbheadid" ItemStyle-CssClass="Dbheadid"
                                        HeaderStyle-BorderColor="Gray" HeaderStyle-BorderStyle="Solid" HeaderStyle-BorderWidth="2px"
                                        ItemStyle-BorderColor="Gray" ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px">
                                        <ItemTemplate>
                                            <asp:Label ID="Db3_hdid3" runat="server" Text='<%# Eval("Dbheadid3") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle BorderColor="Gray" BorderStyle="Solid" BorderWidth="2px" />
                                        <ItemStyle BorderColor="Gray" BorderStyle="Solid" BorderWidth="2px" CssClass="Dbheadid" />
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
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>
    <div style="margin: 0 auto;">
        <br />
        <asp:Button TabIndex="10" CausesValidation="false" CssClass="GreenyPushButton" Style="display: block; width: 100px; margin: 0 auto;"
            ID="btnGenerate" Visible="true" OnClick="btnGenerate_Click" Text="Generate" ValidationGroup="Generate"
            runat="server" />
    </div>


    <ajax:ModalPopupExtender ID="MpAll" runat="server" TargetControlID="btnShowPopup" PopupControlID="pnlpopup">
</ajax:ModalPopupExtender>
     <asp:LinkButton Text="" runat="server" ID="btnShowPopup"></asp:LinkButton>
    <asp:Panel Visible="false" ID="pnlpopup" Style="background: #fff; border: 4px solid #ccc; border: 8px solid rgba(0, 0, 0, 0.5); background-clip: padding-box; -webkit-border-radius: 10px; -moz-border-radius: 10px; border-radius: 10px; min-width: 300px; max-width: 900px;"
        CssClass="raised" runat="server">
        <asp:Label runat="server" ID="lblHint" Text="" Visible="false"> </asp:Label>
        <div class=" box_c_heading">
            <asp:Label CssClass="inner_heading" runat="server" ID="lblHeading" Text=""> </asp:Label>
        </div>
        <div style="min-height: 20px; overflow: hidden; max-width: 880px; padding: 20px;">
            <div style="min-height: 20px; overflow: auto; max-width: 840px;">
                <br />
                <asp:Label runat="server" Style="margin-top: 10px; font-size: 14px;" ID="lblContent"
                    Text=""> </asp:Label>
                <br />
                <asp:PlaceHolder ID="DynamicControlsHolder" runat="server"></asp:PlaceHolder>
                <asp:GridView Width="800" ID="gvoldmember1" runat="server" AutoGenerateColumns="False"
                    HeaderStyle-BackColor="#61A6F8" ShowFooter="True" HeaderStyle-Font-Bold="true"
                    HeaderStyle-ForeColor="White" PageSize="20" BackColor="White" BorderWidth="1px"
                    CellPadding="4" BorderColor="#DEDFDE" BorderStyle="None" ForeColor="Black" GridLines="None">
                    <RowStyle BackColor="#F7F7DE" />
                    <Columns>
                        <asp:TemplateField HeaderText="Heads">
                            <ItemTemplate>
                                <asp:Label Style="width: 150px;" ID="lblitemoldname" runat="server" Text='<%#Eval("Heads1") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Credit Amount">
                            <ItemTemplate>
                                <asp:Label Style="width: 150px;" ID="lblCredit" runat="server" Text='<%#Eval("Credit1") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Debit Amount">
                            <ItemTemplate>
                                <asp:Label Style="width: 150px;" ID="lblDebit" runat="server" Text='<%#Eval("Debit1") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <HeaderStyle VerticalAlign="Middle" HorizontalAlign="left" CssClass="GridviewScrollC2Header" />
                    <RowStyle HorizontalAlign="left" VerticalAlign="Middle" CssClass="GridviewScrollC2Item" />
                    <PagerStyle CssClass="GridviewScrollC2Pager" />
                </asp:GridView>

                <asp:GridView Width="800" ID="gvoldmember2" runat="server" AutoGenerateColumns="False"
                    HeaderStyle-BackColor="#61A6F8" ShowFooter="True" HeaderStyle-Font-Bold="true"
                    HeaderStyle-ForeColor="White" PageSize="20" BackColor="White" BorderWidth="1px"
                    CellPadding="4" BorderColor="#DEDFDE" BorderStyle="None" ForeColor="Black" GridLines="None">
                    <RowStyle BackColor="#F7F7DE" />
                    <Columns>
                        <asp:TemplateField HeaderText="Heads">
                            <ItemTemplate>
                                <asp:Label Style="width: 150px;" ID="lblitemoldname" runat="server" Text='<%#Eval("Heads2") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Credit Amount">
                            <ItemTemplate>
                                <asp:Label Style="width: 150px;" ID="lblCredit" runat="server" Text='<%#Eval("Credit2") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Debit Amount">
                            <ItemTemplate>
                                <asp:Label Style="width: 150px;" ID="lblDebit" runat="server" Text='<%#Eval("Debit2") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <HeaderStyle VerticalAlign="Middle" HorizontalAlign="left" CssClass="GridviewScrollC2Header" />
                    <RowStyle HorizontalAlign="left" VerticalAlign="Middle" CssClass="GridviewScrollC2Item" />
                    <PagerStyle CssClass="GridviewScrollC2Pager" />
                </asp:GridView>

                <asp:GridView Width="800" ID="gvoldmember3" runat="server" AutoGenerateColumns="False"
                    HeaderStyle-BackColor="#61A6F8" ShowFooter="True" HeaderStyle-Font-Bold="true"
                    HeaderStyle-ForeColor="White" PageSize="20" BackColor="White" BorderWidth="1px"
                    CellPadding="4" BorderColor="#DEDFDE" BorderStyle="None" ForeColor="Black" GridLines="None">
                    <RowStyle BackColor="#F7F7DE" />
                    <Columns>
                        <asp:TemplateField HeaderText="Heads">
                            <ItemTemplate>
                                <asp:Label Style="width: 150px;" ID="lblitemoldname" runat="server" Text='<%#Eval("Heads3") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Credit Amount">
                            <ItemTemplate>
                                <asp:Label Style="width: 150px;" ID="lblCredit" runat="server" Text='<%#Eval("Credit3") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Debit Amount">
                            <ItemTemplate>
                                <asp:Label Style="width: 150px;" ID="lblDebit" runat="server" Text='<%#Eval("Debit3") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <HeaderStyle VerticalAlign="Middle" HorizontalAlign="left" CssClass="GridviewScrollC2Header" />
                    <RowStyle HorizontalAlign="left" VerticalAlign="Middle" CssClass="GridviewScrollC2Item" />
                    <PagerStyle CssClass="GridviewScrollC2Pager" />
                </asp:GridView>
            </div>
        </div>
        <div class=" box_c_heading">
            <div style="float: right;">
                <asp:Button Width="100" Style="margin: 0 auto" CssClass="GreenyPushButton" ID="btnYes"
                    OnClick="btnYes_Click" runat="server" Text="yes" />
                <asp:Button Width="100" Style="margin: 0 auto" CssClass="GreenyPushButton" ID="btnNo"
                    OnClick="btnNo_Click" runat="server" Text="No" />
            </div>
        </div>
    </asp:Panel>
    <%--<asp:LinkButton Text="" runat="server" ID="btnShowPopupCheque"></asp:LinkButton>
            <ajax:ModalPopupExtender ID="MpAll" runat="server" TargetControlID="btnShowPopupCheque"
                PopupControlID="panCheque" BackgroundCssClass="modalBackground">
            </ajax:ModalPopupExtender>--%>
    <script src="Scripts/jquery-1.8.3.min.js"></script>
    <script type="text/javascript">
        function GetVisible1() {
            var ser = $("#<%=ddlHeads1.ClientID%>").find("option:selected").text(); //id name for dropdown list                       
            var cid = $("#<%=ddlHeads1.ClientID%>").find("option:selected").val(); //text name for dropdown list             
            var rcid = $("#<%=ddlHeads1.ClientID%> option:selected").val();
            var bnktrue = rcid.includes(":");
            if (bnktrue == true) {
                var rtid = rcid.split(":", 1);
                if (rtid == 3) {
                    $("#<%=txtChequeNO1.ClientID%>").show();
                    $("#<%=lblChequeNO1.ClientID%>").show();

                }
                else if (rtid == 5) {
                    $("#<%=txtChequeNO1.ClientID%>").hide();
                    $("#<%=lblChequeNO1.ClientID%>").hide();
                    var headid = rcid.split(':')[1];
                    GetCustName1(headid);
                }
        }
        else {
            $("#<%=txtChequeNO1.ClientID%>").hide();
                $("#<%=lblChequeNO1.ClientID%>").hide();
            }
            $("#<%=ddlHeads1.ClientID%>").addClass('chzn-select');
        }

        function GetVisible2() {
            var ser = $("#<%=ddlHeads2.ClientID%>").find("option:selected").text(); //id name for dropdown list                       
            var cid = $("#<%=ddlHeads2.ClientID%>").find("option:selected").val(); //text name for dropdown list             
            var rcid = $("#<%=ddlHeads2.ClientID%> option:selected").val();
            var bnktrue = rcid.includes(":");
            if (bnktrue == true) {
                var rtid = rcid.split(":", 1);
                if (rtid == 3) {
                    $("#<%=txtChequeNO2.ClientID%>").show();
                    $("#<%=lblChequeNO2.ClientID%>").show();

                }
                else if (rtid == 5) {
                    $("#<%=txtChequeNO2.ClientID%>").hide();
                    $("#<%=lblChequeNO2.ClientID%>").hide();
                    var headid = rcid.split(':')[1];
                    GetCustName2(headid);
                }
        }
        else {
            $("#<%=txtChequeNO2.ClientID%>").hide();
                $("#<%=lblChequeNO2.ClientID%>").hide();
            }
            $("#<%=ddlHeads2.ClientID%>").addClass('chzn-select');
        }

        function GetVisible3() {
            var ser = $("#<%=ddlHeads3.ClientID%>").find("option:selected").text(); //id name for dropdown list                       
            var cid = $("#<%=ddlHeads3.ClientID%>").find("option:selected").val(); //text name for dropdown list             
            var rcid = $("#<%=ddlHeads3.ClientID%> option:selected").val();
            var bnktrue = rcid.includes(":");
            if (bnktrue == true) {
                var rtid = rcid.split(":", 1);
                if (rtid == 3) {
                    $("#<%=txtChequeNO3.ClientID%>").show();
                    $("#<%=lblChequeNO3.ClientID%>").show();

                }
                else if (rtid == 5) {
                    $("#<%=txtChequeNO3.ClientID%>").hide();
                    $("#<%=lblChequeNO3.ClientID%>").hide();
                    var headid = rcid.split(':')[1];
                    GetCustName3(headid);
                }
        }
        else {
            $("#<%=txtChequeNO3.ClientID%>").hide();
                $("#<%=lblChequeNO3.ClientID%>").hide();
            }
            $("#<%=ddlHeads3.ClientID%>").addClass('chzn-select');
        }

        $(document).ready(function () {
            $("#<%=ddlHeadsDebit1.ClientID%>").change(function () {
                var cid = $("#<%=ddlHeadsDebit1.ClientID%>").find("option:selected").val(); //id for dropdown list  
                var rcid = $("#<%=ddlHeadsDebit1.ClientID%> option:selected").val();
                var rtid = rcid.split(":", 1)
                if (rtid == 5) {
                    var headid = rcid.split(':')[1];
                    DebitCustName1(headid);
                }
            });
        });

        $(document).ready(function () {
            $("#<%=ddlHeadsDebit2.ClientID%>").change(function () {
                var cid = $("#<%=ddlHeadsDebit2.ClientID%>").find("option:selected").val(); //id for dropdown list  
                var rcid = $("#<%=ddlHeadsDebit2.ClientID%> option:selected").val();
                var rtid = rcid.split(":", 1)
                if (rtid == 5) {
                    var headid = rcid.split(':')[1];
                    DebitCustName2(headid);
                }
            });
        });

        $(document).ready(function () {
            $("#<%=ddlHeadsDebit3.ClientID%>").change(function () {
                var cid = $("#<%=ddlHeadsDebit3.ClientID%>").find("option:selected").val(); //id for dropdown list  
                var rcid = $("#<%=ddlHeadsDebit3.ClientID%> option:selected").val();
                var rtid = rcid.split(":", 1)
                if (rtid == 5) {
                    var headid = rcid.split(':')[1];
                    DebitCustName3(headid);
                }
            });
        });

        $(document).ready(function () {
            $("#<%=ddlHeads1.ClientID%>").change(function () {
                var ser = $("#<%=ddlHeads1.ClientID%>").find("option:selected").text(); //id name for dropdown list              
                var cid = $("#<%=ddlHeads1.ClientID%>").find("option:selected").val(); //text name for dropdown list             
                var rcid = $("#<%=ddlHeads1.ClientID%> option:selected").val();

                var bnktrue = rcid.includes(":");
                var chittrue = rcid.includes("|");
                if (bnktrue == true) {
                    var rtid = rcid.split(":", 1)
                    if (rtid == 3) {
                        $("#<%=txtChequeNO1.ClientID%>").show();
                        $("#<%=lblChequeNO1.ClientID%>").show();
                        document.getElementById('<%=txtDescription1.ClientID %>').value = "";
                    }
                    else if (rtid == 5) {
                        $("#<%=txtChequeNO1.ClientID%>").hide();
                        $("#<%=lblChequeNO1.ClientID%>").hide();
                        var headid = rcid.split(':')[1];
                        GetCustName1(headid);
                    }
            }
            else if (chittrue == true) {
                $("#<%=txtChequeNO1.ClientID%>").hide();
                $("#<%=lblChequeNO1.ClientID%>").hide();
                document.getElementById('<%=txtDescription1.ClientID %>').value = "";
            }
            });
            $("#<%=ddlHeads1.ClientID%>").addClass('chzn-select');
        });

        $(document).ready(function () {
            $("#<%=ddlHeads2.ClientID%>").change(function () {
                var ser = $("#<%=ddlHeads2.ClientID%>").find("option:selected").text(); //id name for dropdown list              
                var cid = $("#<%=ddlHeads2.ClientID%>").find("option:selected").val(); //text name for dropdown list             
                var rcid = $("#<%=ddlHeads2.ClientID%> option:selected").val();

                var bnktrue = rcid.includes(":");
                var chittrue = rcid.includes("|");
                if (bnktrue == true) {
                    var rtid = rcid.split(":", 1)
                    if (rtid == 3) {
                        $("#<%=txtChequeNO2.ClientID%>").show();
                        $("#<%=lblChequeNO2.ClientID%>").show();
                        document.getElementById('<%=txtDescription2.ClientID %>').value = "";
                    }
                    else if (rtid == 5) {
                        $("#<%=txtChequeNO2.ClientID%>").hide();
                        $("#<%=lblChequeNO2.ClientID%>").hide();
                        var headid = rcid.split(':')[1];
                        GetCustName2(headid);
                    }
            }
            else if (chittrue == true) {
                $("#<%=txtChequeNO2.ClientID%>").hide();
                $("#<%=lblChequeNO2.ClientID%>").hide();
                document.getElementById('<%=txtDescription2.ClientID %>').value = "";
            }
            });
            $("#<%=ddlHeads2.ClientID%>").addClass('chzn-select');
        });

        $(document).ready(function () {
            $("#<%=ddlHeads3.ClientID%>").change(function () {
                var ser = $("#<%=ddlHeads3.ClientID%>").find("option:selected").text(); //id name for dropdown list              
                var cid = $("#<%=ddlHeads3.ClientID%>").find("option:selected").val(); //text name for dropdown list             
                var rcid = $("#<%=ddlHeads3.ClientID%> option:selected").val();

                var bnktrue = rcid.includes(":");
                var chittrue = rcid.includes("|");
                if (bnktrue == true) {
                    var rtid = rcid.split(":", 1)
                    if (rtid == 3) {
                        $("#<%=txtChequeNO3.ClientID%>").show();
                        $("#<%=lblChequeNO3.ClientID%>").show();
                        document.getElementById('<%=txtDescription3.ClientID %>').value = "";
                    }
                    else if (rtid == 5) {
                        $("#<%=txtChequeNO3.ClientID%>").hide();
                        $("#<%=lblChequeNO3.ClientID%>").hide();
                        var headid = rcid.split(':')[1];
                        GetCustName3(headid);
                    }
            }
            else if (chittrue == true) {
                $("#<%=txtChequeNO3.ClientID%>").hide();
                $("#<%=lblChequeNO3.ClientID%>").hide();
                document.getElementById('<%=txtDescription3.ClientID %>').value = "";
            }
            });
            $("#<%=ddlHeads3.ClientID%>").addClass('chzn-select');
        });

        function DebitCustName1(hdid) {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "PallathurVoucherMultiples.aspx/getcustomername",
                data: JSON.stringify({ hdid: hdid }),
                dataType: "json",
                success: function (data) {
                    var msg = data.d.toString();
                    var txtsnumber = $("[id*=txtDebitdesc1]");
                    txtsnumber.val(msg);
                },
                error: function (result) {
                    alert("Error: " + result);
                }
            });
        }

        function DebitCustName2(hdid) {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "PallathurVoucherMultiples.aspx/getcustomername",
                data: JSON.stringify({ hdid: hdid }),
                dataType: "json",
                success: function (data) {
                    var msg = data.d.toString();
                    var txtsnumber = $("[id*=txtDebitdesc2]");
                    txtsnumber.val(msg);
                },
                error: function (result) {
                    alert("Error: " + result);
                }
            });
        }

        function DebitCustName3(hdid) {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "PallathurVoucherMultiples.aspx/getcustomername",
                data: JSON.stringify({ hdid: hdid }),
                dataType: "json",
                success: function (data) {
                    var msg = data.d.toString();
                    var txtsnumber = $("[id*=txtDebitdesc3]");
                    txtsnumber.val(msg);
                },
                error: function (result) {
                    alert("Error: " + result);
                }
            });
        }

        $(document).ready(function () {
            var ser = $("#<%=ddlHeads1.ClientID%>").text(); //id name for dropdown list              
            //var cid = $("#<%=ddlHeads1.ClientID%>").val(); //text name for dropdown list  
            var cid = $("#<%=ddlHeads1.ClientID%>").find("option:selected").val(); //text name for dropdown list             
            var rcid = $("#<%=ddlHeads1.ClientID%> option:selected").val();
            if (!ser == "--Select--") {
                var bnktrue = rcid.includes(":");
                if (bnktrue == true) {
                    var rtid = rcid.split(":", 1)
                    if (rtid == 3) {
                        $("#<%=txtChequeNO1.ClientID%>").show();
                        $("#<%=lblChequeNO1.ClientID%>").show();
                        document.getElementById('<%=txtDescription1.ClientID %>').value = "";
                    }
                    else if (rtid == 5) {
                        $("#<%=txtChequeNO1.ClientID%>").hide();
                        $("#<%=lblChequeNO1.ClientID%>").hide();
                        var headid = rcid.split(':')[1];
                        GetCustName1(headid);
                    }
            }
            else {
                $("#<%=txtChequeNO1.ClientID%>").hide();
                    $("#<%=lblChequeNO1.ClientID%>").hide();
                    document.getElementById('<%=txtDescription1.ClientID %>').value = "";
                }
            }
            else {
                $("#<%=txtChequeNO1.ClientID%>").hide();
                $("#<%=lblChequeNO1.ClientID%>").hide();
                document.getElementById('<%=txtDescription1.ClientID %>').value = "";
            }
            $("#<%=ddlHeads1.ClientID%>").addClass('chzn-select');
        });

        $(document).ready(function () {
            var ser = $("#<%=ddlHeads2.ClientID%>").text(); //id name for dropdown list              
            var cid = $("#<%=ddlHeads2.ClientID%>").find("option:selected").val(); //text name for dropdown list             
            var rcid = $("#<%=ddlHeads2.ClientID%> option:selected").val();
            if (!ser == "--Select--") {
                var bnktrue = rcid.includes(":");
                if (bnktrue == true) {
                    var rtid = rcid.split(":", 1)
                    if (rtid == 3) {
                        $("#<%=txtChequeNO2.ClientID%>").show();
                        $("#<%=lblChequeNO2.ClientID%>").show();
                        document.getElementById('<%=txtDescription2.ClientID %>').value = "";
                    }
                    else if (rtid == 5) {
                        $("#<%=txtChequeNO2.ClientID%>").hide();
                        $("#<%=lblChequeNO2.ClientID%>").hide();
                        var headid = rcid.split(':')[1];
                        GetCustName2(headid);
                    }
            }
            else {
                $("#<%=txtChequeNO2.ClientID%>").hide();
                    $("#<%=lblChequeNO2.ClientID%>").hide();
                    document.getElementById('<%=txtDescription2.ClientID %>').value = "";
                }
            }
            else {
                $("#<%=txtChequeNO2.ClientID%>").hide();
                $("#<%=lblChequeNO2.ClientID%>").hide();
                document.getElementById('<%=txtDescription2.ClientID %>').value = "";
            }
            $("#<%=ddlHeads2.ClientID%>").addClass('chzn-select');
        });

        $(document).ready(function () {
            var ser = $("#<%=ddlHeads3.ClientID%>").text(); //id name for dropdown list              
            var cid = $("#<%=ddlHeads3.ClientID%>").find("option:selected").val(); //text name for dropdown list             
            var rcid = $("#<%=ddlHeads3.ClientID%> option:selected").val();
            if (!ser == "--Select--") {
                var bnktrue = rcid.includes(":");
                if (bnktrue == true) {
                    var rtid = rcid.split(":", 1)
                    if (rtid == 3) {
                        $("#<%=txtChequeNO3.ClientID%>").show();
                        $("#<%=lblChequeNO3.ClientID%>").show();
                        document.getElementById('<%=txtDescription3.ClientID %>').value = "";
                    }
                    else if (rtid == 5) {
                        $("#<%=txtChequeNO3.ClientID%>").hide();
                        $("#<%=lblChequeNO3.ClientID%>").hide();
                        var headid = rcid.split(':')[1];
                        GetCustName(headid);
                    }
            }
            else {
                $("#<%=txtChequeNO3.ClientID%>").hide();
                    $("#<%=lblChequeNO3.ClientID%>").hide();
                    document.getElementById('<%=txtDescription3.ClientID %>').value = "";
                }
            }
            else {
                $("#<%=txtChequeNO3.ClientID%>").hide();
                $("#<%=lblChequeNO3.ClientID%>").hide();
                document.getElementById('<%=txtDescription3.ClientID %>').value = "";
            }
            $("#<%=ddlHeads3.ClientID%>").addClass('chzn-select');
        });

        function CheckDate1() {
            var inputDate = $('#<%=txtDate1.ClientID %>').val();
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
                    document.getElementById('<%=txtDate1.ClientID %>').value = "";
                    alert('Incorrect Date Format..');
                }
            }
            else {
                document.getElementById('<%=txtDate1.ClientID %>').value = "";
                alert('Incorrect Date Format..');
            }
        }

        function CheckDate2() {
            var inputDate = $('#<%=txtDate2.ClientID %>').val();
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
                    document.getElementById('<%=txtDate2.ClientID %>').value = "";
                    alert('Incorrect Date Format..');
                }
            }
            else {
                document.getElementById('<%=txtDate2.ClientID %>').value = "";
                alert('Incorrect Date Format..');
            }
        }

        function CheckDate3() {
            var inputDate = $('#<%=txtDate3.ClientID %>').val();
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
                     document.getElementById('<%=txtDate3.ClientID %>').value = "";
                    alert('Incorrect Date Format..');
                }
            }
            else {
                document.getElementById('<%=txtDate3.ClientID %>').value = "";
                alert('Incorrect Date Format..');
            }
        }

    </script>
    <script type="text/javascript">
<%--        $(function () {
            $("[id*=btnAdd1]").click(function () {
                alert(document.getElementById('<%=txtAmount1.ClientID %>').value);

                //Reference the GridView.
                var gridView = $("[id*=GridClnt1]");

                //Reference the first row.
                var row = gridView.find("tr").eq(1);

                //Check if row is dummy, if yes then remove.
                if ($.trim(row.find("td").eq(0).html()) == "") {
                    row.remove();
                }

                //Clone the reference first row.
                row = row.clone(true);
                GetValue1();

                var amt = document.getElementById('<%=txtAmount1.ClientID %>').value;
                if (amt != "") {
                    //Add the Name value to first cell.
                    var HD_Headtxt = $("[id*=HD_Headtxt1]");
                    SetValue(row, 0, "Heads1", HD_Headtxt);

                    //Add the Country value to second cell.
                    var txtAmount = $("[id*=txtAmount1]");
                    SetValue(row, 1, "Amount1", txtAmount);

                    //Add the Country value to second cell.
                    var txtNarration = $("[id*=txtDescription1]");
                    SetValue(row, 2, "Description1", txtNarration);

                    //Add the Country value to second cell.
                    var txtChequeNO = $("[id*=txtChequeNO1]");
                    SetValue(row, 3, "ChequeNo1", txtChequeNO);

                    //Add the Country value to second cell.
                    var HD_Headval = $("[id*=HD_Headval1]");
                    SetValue(row, 4, "headid1", HD_Headval);


                    // Reset();
                    //Add the row to the GridView.
                    gridView.append(row);
                }

                return false;

            });

            function SetValue(row, index, name, textbox) {
                //Reference the Cell and set the value.
                row.find("td").eq(index).html(textbox.val());

                //Create and add a Hidden Field to send value to server.
                var input = $("<input type = 'hidden' />");
                input.prop("name", name);
                input.val(textbox.val());
                row.find("td").eq(index).append(input);

                //Clear the TextBox.
                textbox.val("");
            }
        });

        function GetValue1() {
            var hds = "";
            var hdsval = "";

            hds = $('#<%=ddlHeads1.ClientID %>').find("option:selected").text();
            hdsval = $('#<%=ddlHeads1.ClientID %>').find("option:selected").val();

            var headstext = $("[id*=HD_Headtxt1]");
            headstext.val(hds);

            var hdval = $("[id*=HD_Headval1]");
            hdval.val(hdsval);
        }

        function GetValue2() {
            var hds = "";
            var hdsval = "";

            hds = $('#<%=ddlHeads2.ClientID %>').find("option:selected").text();
            hdsval = $('#<%=ddlHeads2.ClientID %>').find("option:selected").val();

            var headstext = $("[id*=HD_Headtxt2]");
            headstext.val(hds);

            var hdval = $("[id*=HD_Headval2]");
            hdval.val(hdsval);
        }

        function GetValue3() {
            var hds = "";
            var hdsval = "";

            hds = $('#<%=ddlHeads3.ClientID %>').find("option:selected").text();
            hdsval = $('#<%=ddlHeads3.ClientID %>').find("option:selected").val();

            var headstext = $("[id*=HD_Headtxt3]");
            headstext.val(hds);

            var hdval = $("[id*=HD_Headval3]");
            hdval.val(hdsval);
        }
--%>
        function Reset1() {
            var ddlhds = document.getElementById('#<%=ddlHeads1.ClientID %>');
            ddlhds.selectedIndex = 0;

            return false;
        }

        function Reset2() {
            var ddlhds = document.getElementById('#<%=ddlHeads2.ClientID %>');
            ddlhds.selectedIndex = 0;

            return false;
        }

        function Reset3() {
            var ddlhds = document.getElementById('#<%=ddlHeads3.ClientID %>');
            ddlhds.selectedIndex = 0;

            return false;
        }

<%--        $(function () {
            $("[id*=btnAdd2]").click(function () {

                //Reference the GridView.
                var gridView = $("[id*=GridClnt2]");

                //Reference the first row.
                var row = gridView.find("tr").eq(1);

                //Check if row is dummy, if yes then remove.
                if ($.trim(row.find("td").eq(0).html()) == "") {
                    row.remove();
                }

                //Clone the reference first row.
                row = row.clone(true);

                GetValue2();

                var amt = document.getElementById('<%=txtAmount2.ClientID %>').value;
                if (amt != "") {
                    //Add the Name value to first cell.
                    var HD_Headtxt = $("[id*=HD_Headtxt2]");
                    SetValue(row, 0, "Heads2", HD_Headtxt);

                    //Add the Country value to second cell.
                    var txtAmount = $("[id*=txtAmount2]");
                    SetValue(row, 1, "Amount2", txtAmount);

                    //Add the Country value to second cell.
                    var txtNarration = $("[id*=txtDescription2]");
                    SetValue(row, 2, "Description2", txtNarration);

                    //Add the Country value to second cell.
                    var txtChequeNO = $("[id*=txtChequeNO2]");
                    SetValue(row, 3, "ChequeNo2", txtChequeNO);

                    //Add the Country value to second cell.
                    var HD_Headval = $("[id*=HD_Headval2]");
                    SetValue(row, 4, "headid2", HD_Headval);


                    // Reset();
                    //Add the row to the GridView.
                    gridView.append(row);
                }

                return false;

            });
            function SetValue(row, index, name, textbox) {
                //Reference the Cell and set the value.
                row.find("td").eq(index).html(textbox.val());

                //Create and add a Hidden Field to send value to server.
                var input = $("<input type = 'hidden' />");
                input.prop("name", name);
                input.val(textbox.val());
                row.find("td").eq(index).append(input);

                //Clear the TextBox.
                textbox.val("");
            }
        });

        $(function () {
            $("[id*=btnAdd3]").click(function () {

                //Reference the GridView.
                var gridView = $("[id*=GridClnt3]");

                //Reference the first row.
                var row = gridView.find("tr").eq(1);

                //Check if row is dummy, if yes then remove.
                if ($.trim(row.find("td").eq(0).html()) == "") {
                    row.remove();
                }

                //Clone the reference first row.
                row = row.clone(true);

                GetValue3();

                var amt = document.getElementById('<%=txtAmount3.ClientID %>').value;
                    if (amt != "") {
                        //Add the Name value to first cell.
                        var HD_Headtxt = $("[id*=HD_Headtxt3]");
                        SetValue(row, 0, "Heads3", HD_Headtxt);

                        //Add the Country value to second cell.
                        var txtAmount = $("[id*=txtAmount3]");
                        SetValue(row, 1, "Amount3", txtAmount);

                        //Add the Country value to second cell.
                        var txtNarration = $("[id*=txtDescription3]");
                        SetValue(row, 2, "Description3", txtNarration);

                        //Add the Country value to second cell.
                        var txtChequeNO = $("[id*=txtChequeNO3]");
                        SetValue(row, 3, "ChequeNo3", txtChequeNO);

                        //Add the Country value to second cell.
                        var HD_Headval = $("[id*=HD_Headval3]");
                        SetValue(row, 4, "headid3", HD_Headval);


                        // Reset();
                        //Add the row to the GridView.
                        gridView.append(row);
                    }

                    return false;

                });

                function SetValue(row, index, name, textbox) {
                    //Reference the Cell and set the value.
                    row.find("td").eq(index).html(textbox.val());

                    //Create and add a Hidden Field to send value to server.
                    var input = $("<input type = 'hidden' />");
                    input.prop("name", name);
                    input.val(textbox.val());
                    row.find("td").eq(index).append(input);

                    //Clear the TextBox.
                    textbox.val("");
                }
            });
--%>
        $.noConflict();
        $(".delete").on('click', function () {
            $('.case:checkbox:checked').parents("tr").remove();

        });

        $.noConflict();
        function select_all() {
            $('input[class=case]:checkbox').each(function () {
                if ($('input[class=check_all]:checkbox:checked').length == 0) {
                    $(this).prop("checked", false);
                } else {
                    $(this).prop("checked", true);
                }
            });
        }


<%--            function DebGetValue1() {
                var dbhds = "";
                var dbhdsval = "";

                dbhds = $('#<%=ddlHeadsDebit1.ClientID %>').find("option:selected").text();
                    dbhdsval = $('#<%=ddlHeadsDebit1.ClientID %>').find("option:selected").val();

                    var headstext = $("[id*=hidDb_Headtxt1]");
                    headstext.val(dbhds);

                    var hdval = $("[id*=hidDb_HeadVal1]");
                    hdval.val(dbhdsval);
                }

                function DebGetValue2() {
                    var dbhds = "";
                    var dbhdsval = "";

                    dbhds = $('#<%=ddlHeadsDebit2.ClientID %>').find("option:selected").text();
             dbhdsval = $('#<%=ddlHeadsDebit2.ClientID %>').find("option:selected").val();

             var headstext = $("[id*=hidDb_Headtxt2]");
             headstext.val(dbhds);

             var hdval = $("[id*=hidDb_HeadVal2]");
             hdval.val(dbhdsval);
         }

         function DebGetValue3() {
             var dbhds = "";
             var dbhdsval = "";

             dbhds = $('#<%=ddlHeadsDebit3.ClientID %>').find("option:selected").text();
            dbhdsval = $('#<%=ddlHeadsDebit3.ClientID %>').find("option:selected").val();

            var headstext = $("[id*=hidDb_Headtxt3]");
            headstext.val(dbhds);

            var hdval = $("[id*=hidDb_HeadVal3]");
            hdval.val(dbhdsval);
        }--%>

        function Reset1() {
            var dbddlhds = document.getElementById('#<%=ddlHeadsDebit1.ClientID %>');
            dbddlhds.selectedIndex = 0;

            return false;
        }

        function Reset2() {
            var dbddlhds = document.getElementById('#<%=ddlHeadsDebit2.ClientID %>');
            dbddlhds.selectedIndex = 0;

            return false;
        }

        function Reset3() {
            var dbddlhds = document.getElementById('#<%=ddlHeadsDebit3.ClientID %>');
            dbddlhds.selectedIndex = 0;

            return false;
        }

<%--        $(function () {
            $("[id*=btnDbAdd1]").click(function () {

                //Reference the GridView.
                var gridView = $("[id*=GrdDbClnt1]");

                //Reference the first row.
                var row = gridView.find("tr").eq(1);

                //Check if row is dummy, if yes then remove.
                if ($.trim(row.find("td").eq(0).html()) == "") {
                    row.remove();
                }

                //Clone the reference first row.
                row = row.clone(true);

                DebGetValue1();

                var debamt = document.getElementById('<%=debitAmnt1.ClientID %>').value;
                if (debamt != "") {
                    //Add the bank head value to first cell.
                    var hidDb_Headtxt = $("[id*=hidDb_Headtxt1]");
                    SetValue1(row, 0, "DbHeads1", hidDb_Headtxt);

                    //Add the ddebit amount value to second cell.
                    var debitAmnt = $("[id*=debitAmnt1]");
                    SetValue1(row, 1, "DbAmount1", debitAmnt);

                    //Add the desc to third cell.
                    var txtDebitdesc = $("[id*=txtDebitdesc1]");
                    SetValue1(row, 2, "DbDescription1", txtDebitdesc);

                    //Add the head val to fourth cell.
                    var hidDb_HeadVal = $("[id*=hidDb_HeadVal1]");
                    SetValue1(row, 3, "Dbheadid1", hidDb_HeadVal);


                    // Reset();
                    //Add the row to the GridView.
                    gridView.append(row);
                }

                return false;

            });

            function SetValue1(row, index, name, textbox) {
                //Reference the Cell and set the value.
                row.find("td").eq(index).html(textbox.val());

                //Create and add a Hidden Field to send value to server.
                var input = $("<input type = 'hidden' />");
                input.prop("name", name);
                input.val(textbox.val());
                row.find("td").eq(index).append(input);

                //Clear the TextBox.
                textbox.val("");
            }
        });

        $(function () {
            $("[id*=btnDbAdd2]").click(function () {

                //Reference the GridView.
                var gridView = $("[id*=GrdDbClnt2]");

                //Reference the first row.
                var row = gridView.find("tr").eq(1);

                //Check if row is dummy, if yes then remove.
                if ($.trim(row.find("td").eq(0).html()) == "") {
                    row.remove();
                }

                //Clone the reference first row.
                row = row.clone(true);

                DebGetValue2();

                var debamt = document.getElementById('<%=debitAmnt2.ClientID %>').value;
                if (debamt != "") {
                    //Add the bank head value to first cell.
                    var hidDb_Headtxt = $("[id*=hidDb_Headtxt2]");
                    SetValue1(row, 0, "DbHeads2", hidDb_Headtxt);

                    //Add the ddebit amount value to second cell.
                    var debitAmnt = $("[id*=debitAmnt2]");
                    SetValue1(row, 1, "DbAmount2", debitAmnt);

                    //Add the desc to third cell.
                    var txtDebitdesc = $("[id*=txtDebitdesc2]");
                    SetValue1(row, 2, "DbDescription2", txtDebitdesc);

                    //Add the head val to fourth cell.
                    var hidDb_HeadVal = $("[id*=hidDb_HeadVal2]");
                    SetValue1(row, 3, "Dbheadid2", hidDb_HeadVal);


                    // Reset();
                    //Add the row to the GridView.
                    gridView.append(row);
                }

                return false;

            });

            function SetValue1(row, index, name, textbox) {
                //Reference the Cell and set the value.
                row.find("td").eq(index).html(textbox.val());

                //Create and add a Hidden Field to send value to server.
                var input = $("<input type = 'hidden' />");
                input.prop("name", name);
                input.val(textbox.val());
                row.find("td").eq(index).append(input);

                //Clear the TextBox.
                textbox.val("");
            }
        });

        $(function () {
            $("[id*=btnDbAdd3]").click(function () {

                //Reference the GridView.
                var gridView = $("[id*=GrdDbClnt3]");

                //Reference the first row.
                var row = gridView.find("tr").eq(1);

                //Check if row is dummy, if yes then remove.
                if ($.trim(row.find("td").eq(0).html()) == "") {
                    row.remove();
                }

                //Clone the reference first row.
                row = row.clone(true);

                DebGetValue3();

                var debamt = document.getElementById('<%=debitAmnt3.ClientID %>').value;
                if (debamt != "") {
                    //Add the bank head value to first cell.
                    var hidDb_Headtxt = $("[id*=hidDb_Headtxt3]");
                    SetValue1(row, 0, "DbHeads3", hidDb_Headtxt);

                    //Add the ddebit amount value to second cell.
                    var debitAmnt = $("[id*=debitAmnt3]");
                    SetValue1(row, 1, "DbAmount3", debitAmnt);

                    //Add the desc to third cell.
                    var txtDebitdesc = $("[id*=txtDebitdesc3]");
                    SetValue1(row, 2, "DbDescription3", txtDebitdesc);

                    //Add the head val to fourth cell.
                    var hidDb_HeadVal = $("[id*=hidDb_HeadVal3]");
                    SetValue1(row, 3, "Dbheadid3", hidDb_HeadVal);


                    // Reset();
                    //Add the row to the GridView.
                    gridView.append(row);
                }

                return false;

            });

            function SetValue1(row, index, name, textbox) {
                //Reference the Cell and set the value.
                row.find("td").eq(index).html(textbox.val());

                //Create and add a Hidden Field to send value to server.
                var input = $("<input type = 'hidden' />");
                input.prop("name", name);
                input.val(textbox.val());
                row.find("td").eq(index).append(input);

                //Clear the TextBox.
                textbox.val("");
            }
        });
--%>
        $(".delete").on('click', function () {
            $('.dbcase:checkbox:checked').parents("tr").remove();

        });

        function select_all() {
            $('input[class=case]:checkbox').each(function () {
                if ($('input[class=check_all]:checkbox:checked').length == 0) {
                    $(this).prop("checked", false);
                } else {
                    $(this).prop("checked", true);
                }
            });
        }
    </script>
</asp:Content>
