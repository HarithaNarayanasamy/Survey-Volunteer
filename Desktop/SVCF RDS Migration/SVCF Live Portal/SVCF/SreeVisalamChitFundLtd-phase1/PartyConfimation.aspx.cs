using SVCF_BusinessAccessLayer;
using SVCF_TransactionLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using log4net;
using log4net.Config;
using System.Text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.Drawing.Imaging;
using SVCF_DataAccessLayer;

namespace SreeVisalamChitFundLtd_phase1
{
    public partial class PartyConfimation : System.Web.UI.Page
    {
        #region Object
        BusinessLayer balayer = new BusinessLayer();
        TransactionLayer trn = new TransactionLayer();

        string qry = "";
        #endregion

        #region VarDeclaration
        CommonVariables objCOM = new CommonVariables();
        string imagetype = "";
        string filename = "";
        string contentType = "";
        string firstassvalue = "";
        string secondvalue = "";
        string assvalue = "";
        string assvalue1 = "";

        int countvalue;
        #endregion

        ILog logger = log4net.LogManager.GetLogger(typeof(PartyConfimation));

        protected void Page_Load(object sender, EventArgs e)
        {
            int gpval;
            if (!IsPostBack)
            {
                //BindTreeView();
                if (Convert.ToInt32(Session["Branchid"]) == 161)
                {
                    LoadbranchList();
                    listbranch.Visible = true;
                    lblbranchname.Visible = true;
                }
                else
                {
                    LoadbranchList1();
                    listbranch.Visible = true;
                    lblbranchname.Visible = true;
                }
                gpval  = 0;
                LoadGroupList(gpval);
                // MenuView.Attributes.Add("onclick", "OnTreeClick(event)");
            }
        }
        public void LoadbranchList1()
        {
            listbranch.DataSource = null;
            DataTable dtgroupno = null;

            dtgroupno = balayer.GetDataTable("select Head_Id, B_Name from svcf.branchdetails where Head_Id=" + balayer.ToobjectstrEvenNull(Session["Branchid"])+"");
            DataRow dr = dtgroupno.NewRow();
            dr[0] = "0";
            dr[1] = "--Select--";
            listbranch.DataValueField = "Head_Id";
            listbranch.DataTextField = "B_Name";
            dtgroupno.Rows.InsertAt(dr, 0);
            listbranch.DataSource = dtgroupno;
            listbranch.DataBind();

        }
        public void LoadbranchList()
        {
            listbranch.DataSource = null;
            DataTable dtgroupno = null;

            dtgroupno = balayer.GetDataTable("select NodeID,Node from headstree where ParentID=1 ");
            DataRow dr = dtgroupno.NewRow();
            dr[0] = "0";
            dr[1] = "--Select--";
            listbranch.DataValueField = "NodeID";
            listbranch.DataTextField = "Node";
            dtgroupno.Rows.InsertAt(dr, 0);
            listbranch.DataSource = dtgroupno;
            listbranch.DataBind();
            
        }

        public void LoadGroupList(int id)
         {
            listGroup.DataSource = null;
            DataTable dtgrp = null;
            listGroup.Items.Insert(0, "--select--");
           
            if(id!=0)
            {

                DateTime today =   DateTime.Today;
                string dte =(Convert.ToString(today).Split(' ')[0]);
                string dtw = balayer.IndiandyeToMysqlDate(dte.Replace("-", "/"));
                if(dtw !="")
                {
                    //dtgrp = balayer.GetDataTable("select Head_id,GROUPNO from groupmaster where BranchId=" + id + " and ChitEndDate >='"+dtw+"' ;");

                    dtgrp = balayer.GetDataTable("select Head_id,GROUPNO from groupmaster where BranchId=" + id + "  ;");
                }
                else
                {
                    dtgrp = balayer.GetDataTable("select Head_id,GROUPNO from groupmaster where BranchId=" + id + ";");
                }
               
                DataRow dr = dtgrp.NewRow();
                dr[0] = "0";
                dr[1] = "--Select--";
                listGroup.DataValueField = "Head_id";
                listGroup.DataTextField = "GROUPNO";
                string value = listGroup.SelectedItem.Value;
                dtgrp.Rows.InsertAt(dr, 0);
                listGroup.DataSource = dtgrp;
                listGroup.DataBind();
                
            
            }

        }
        protected void id_oNSelected(object sender, EventArgs e)
        {
            if (listGroup.SelectedIndex != 0)
            {
                string value = listGroup.SelectedItem.Value;
                string text = listGroup.SelectedItem.Text;
                string value1 = listbranch.SelectedItem.Value;
                string text1 = listbranch.SelectedItem.Text;
                int ival = Convert.ToInt32(value1);
                loadgetvalue(value, ival);

            }
        }
        public void loadgetvalue(string value,int id)
        {
            listvalue.DataSource = null;
            DataTable dtgrp = null;
            listvalue.Items.Insert(0, "--select--");

            if (value != "")
            {
                dtgrp = balayer.GetDataTable("select Head_Id,GrpMemberID as 'CustomerName' from membertogroupmaster where BranchID=" + id + " and GroupID='" + value + "'");
            }
            else
            {
                dtgrp = balayer.GetDataTable("select Head_Id,GrpMemberID as 'CustomerName' from membertogroupmaster where BranchID=" + id + "");
            }
            DataRow dr = dtgrp.NewRow();
            dr[0] = "0";
            dr[1] = "--Select--";
            listvalue.DataValueField = "Head_Id";
            listvalue.DataTextField = "CustomerName";
            string value11 = listvalue.SelectedItem.Value;
            dtgrp.Rows.InsertAt(dr, 0);
            listvalue.DataSource = dtgrp;
            listvalue.DataBind();

        }

        protected void branch_oNSelected(object sender, EventArgs e)
        {
            if(listbranch.SelectedIndex!=0)
            {
                string value = listbranch.SelectedItem.Value;
                string text = listbranch.SelectedItem.Text;
                int ival = Convert.ToInt32(value);
                LoadGroupList(ival);
           
            }


        }

        public void selectgroup()
        {
            if (listGroup.SelectedIndex != 0)
            {
                try
                {
                    int branchvalue = Convert.ToInt32(listbranch.SelectedItem.Value);
                    int value = Convert.ToInt32(listGroup.SelectedItem.Value);
                    // DataTable dtg = balayer.GetDataTable("select MemberName,MemberID,MemberAddress,GrpMemberID  from membertogroupmaster where GroupID=" + value + ";");
                    //DataTable dtg = balayer.GetDataTable("SELECT mtg.MemberName as MemberName,mtg.MemberAddress as MemberAddress,mtg.BranchID as branch,mtg.Head_Id as chitNo,mm.MemberID as ChitAgrNo,auction.DrawNO as DrawNO,mtg.GroupID as GroupID," 
                    //                                           +" bd.B_Address as branchAddress   FROM svcf.membertogroupmaster as mtg  left join svcf.branchdetails as bd on (mtg.BranchID=bd.Head_Id) join svcf.membermaster as mm on (mtg.BranchID= mm.BranchId) "
                    //                              + " join svcf.auctiondetails as auction on (auction.GroupID=mtg.GroupID) where mtg.BranchId= " + branchvalue + " and mtg.GroupID=" + value + " Group by mtg.MemberID;");

                    //DateTime dte = DateTime.Today;
                    //string today = (Convert.ToString(dte).Split(' ')[0]);
                    string today = DateTime.Now.ToString("dd/MM/yyyy");
                    DataTable chitvalue = balayer.GetDataTable("select BranchID,GROUPNO,ChitAgreementNo,ChitAgreementYear,AgreementDate,ChitCategory,AuctionTime,AuctionEndTime from groupmaster where Head_id=" + value + ";");
                    string drawcount = balayer.GetSingleValue("select count(*) from auctiondetails where groupid=" + value + " and IsPrized in('y','n');");
                    string prized = balayer.GetSingleValue("select PrizedAmount from svcf.auctiondetails where groupid=" + value + " and IsPrized in('y');");
                    DateTime AgreementDate1 = Convert.ToDateTime(chitvalue.Rows[0]["AgreementDate"]);
                    string AgreementDate = AgreementDate1.ToString("dd/MM/yyyy");
                    string AuctionTime1 = chitvalue.Rows[0]["AuctionTime"].ToString();
                    //Auctiondt = Convert.ToString(Auctiondt2).Split(' ')[0];
                    string AuctionTime = AuctionTime1.Replace(":00", "");
                    string AuctionEndTime1 = chitvalue.Rows[0]["AuctionEndTime"].ToString();
                    string AuctionEndTime = AuctionEndTime1.Replace(":00", "");
                    int countval = Convert.ToInt16(drawcount);
                    countvalue = countval + 1;
                    DataTable Auctiondt1 = balayer.GetDataTable("select AuctionDate from auctiondetails where groupid=" + value + " and drawno=" + (countval + 1) + ";");
                    string Auctiondt = "";
                    string Auctiondt3 = "";
                    //var month = new DateTime(Auctiondt1.year, Auctiondt1.Month, 1);
                    //var first = month.AddDays(-1).ToString("dd/MM/yyyy");
                    if (Auctiondt1.Rows.Count != 0)
                    {
                        DateTime Auctiondt2 = Convert.ToDateTime(Auctiondt1.Rows[0]["AuctionDate"]);
                        Auctiondt = Auctiondt2.ToString("dd/MM/yyyy");
                        DateTime Auctiondt4 = Auctiondt2.AddDays(-1);
                        Auctiondt3 = Auctiondt4.ToString("dd/MM/yyyy");
                    }

                    if (countval < 150)
                    {
                        // Check the last digit.
                        switch (countval % 10)
                        {
                            case 0:
                                assvalue1 = "th";
                                break;
                            case 1:
                                assvalue1 = "st";
                                break;
                            case 2:
                                assvalue1 = "nd";
                                break;
                            case 3:
                                assvalue1 = "rd";
                                break;
                            case 4:
                                assvalue1 = "th";
                                break;
                            case 5:
                                assvalue1 = "th";
                                break;
                            case 6:
                                assvalue1 = "th";
                                break;
                            case 7:
                                assvalue1 = "th";
                                break;
                            case 8:
                                assvalue1 = "th";
                                break;
                            case 9:
                                assvalue1 = "th";
                                break;
                        }
                    }
                    if (countvalue < 150)
                    {
                        // Check the last digit.
                        switch (countvalue % 10)
                        {
                            case 0:
                                assvalue = "th";
                                break;
                            case 1:
                                assvalue = "st";
                                break;
                            case 2:
                                assvalue = "nd";
                                break;
                            case 3:
                                assvalue = "rd";
                                break;
                            case 4:
                                assvalue = "th";
                                break;
                            case 5:
                                assvalue = "th";
                                break;
                            case 6:
                                assvalue = "th";
                                break;
                            case 7:
                                assvalue = "th";
                                break;
                            case 8:
                                assvalue = "th";
                                break;
                            case 9:
                                assvalue = "th";
                                break;
                        }
                    }
                    // return assvalue;
                    DataTable auctionval = balayer.GetDataTable("select PrizedAmount,Dividend,NextDueAmount from auctiondetails where groupid=" + value + " and drawno=" + countval + "; ");
                    DataTable getbranchdt = balayer.GetDataTable("SELECT B_Name,B_Address FROM svcf.branchdetails where Head_id =" + branchvalue + ";");
                    //string imageFile = @"C:\SVCF_09.04.2017\SVCF\SreeVisalamChitFundLtd-phase1\Images\visalam.png";
                    string number = listvalue.SelectedItem.Text;
                    string number1 = listvalue.SelectedValue;
                    if (Convert.ToInt32(number1) > 0)
                    {
                        string name = getbranchdt.Rows[0]["B_Name"].ToString();
                        string imageFile = (Server.MapPath("~\\Images\\logo_New.png"));
                        string border = (Server.MapPath("~\\Images\\border-1px.png"));
                        string VISALAM = (Server.MapPath("~\\Images\\VISALAM-LOGO_WTXT.png"));
                        string ddd = idTextbox.Text;
                        string aaa = balayer.GetSingleValue("select MemberAddress from svcf.membertogroupmaster a join svcf.groupmaster b on a.GroupID=b.Head_id where a.GrpMemberID='" + number + "';");
                        string money = balayer.GetSingleValue("select (case when( tp1.PaymentDate is null or tp1.PaymentDate >'" + balayer.indiandateToMysqlDate(ddd) + "') then sum(case when (v1.Voucher_Type='C' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) -sum(case when (v1.Voucher_Type='D' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) else 0.00 end ) as Credit from membertogroupmaster as mg1 join voucher as v1 on v1.Head_Id=mg1.Head_Id join groupmaster as vgwd1 on vgwd1.`Head_Id`=" + listvalue.SelectedValue + " left join trans_payment as tp1 on v1.Head_Id =tp1.TokenNumber join membermaster as mm on (mg1.MemberID=mm.MemberIDNew) where mg1.BranchID=" + branchvalue + " and mg1.GroupID=" + listGroup.SelectedValue + " and v1.Head_Id=" + listvalue.SelectedValue + " and v1.ChoosenDate<= '" + balayer.indiandateToMysqlDate(ddd) + "' group by v1.Head_Id order by cast(digits(mg1.GrpMemberID) as unsigned),v1.Voucher_Type DESC ;");
                        Document doc = new Document();
                        using (StringWriter sw = new StringWriter())
                        {
                            using (HtmlTextWriter hw = new HtmlTextWriter(sw))
                            {
                                StringBuilder sb = new StringBuilder();
                                sb.Append(@"<html><table style='width: 100%;'> <tr> <td style='padding: 0;'> <table width='90%' style='margin:30px;padding:30px;'> <thead> <tr align='center'> <th colspan='3' style='width: 3%;text-align:center;line-height:42pt;'> <img src='" + VISALAM + "' width='70px'></th> </tr><tr> <th colspan='3' style='text-align:center'> <span style='margin:0;padding-top:6px;font-size:12pt;text-align: center;line-height:-5px;'><strong>Branch:</strong>" + name + "</span></p></th> </tr></thead> <tbody> <tr> <td colspan='2' style='text-align: left;font-size: 10px;'><strong>To:</strong>" + aaa + "</td><td style='text-align: right;font-size: 10px;'><strong>Date: " + ddd + "</strong></td></tr><tr> <td colspan='3' style='text-align: center;padding-top:6px;'> <div style='text-align: left; font-size:10pt;margin: 5px 0 0 0;'> Dear Sir/s / Madam,<br><div style='text-align: center;font-size: 10px;line-height:12pt;'><strong>Ref. : Chit No. : " + number + "</strong> <br><span>*****</span></div></div></td></tr><tr> <td colspan='3' style='padding: 0px 0 10px 0px; font-size: 10px;text-align: justify;line-height:0pt;'> <p>     We wish to inform you that you have remitted a sum of<strong> Rs" + money + "/- </strong>(excluding dividend) towards your above mentioned Non-Prized Chit upto " + ddd + " .We request you to confirm the same.</p><p>       In the absence of the receipt of the above , Within one month from this date , the said sum will be taken as acknowledged by you to be correct.</p></td></tr><tr> <td colspan='3' style='text-align: right;padding-top:6px;line-height12px'> <div style='text-align: right;font-size: 10px;line-height10px'> Yours Faithfully               .<br><div style='text-align: right;font-size: 10px;line-height:10pt;'><span>for SREE VISALAM CHIT FUND LIMITED<span> </div></div></td></tr><tr> <td colspan='3' style='width: 50%;border-bottom: 2px solid black;text-align: right;font-size: 10px;padding:30px 0 10px 0;'> <br>Authorised Signatory           .</td></tr><tr><td colspan='3'><img src='" + border + "' width='75%'> </td></tr><tr> <td colspan='3' style='text-align: left;font-size: 10px;padding: 20px 0;line-height:5px'> <div style='line-height=0px'>TO <br><p style='margin: 6px 0;font-size: 10px;line-height=5px'>    SREE VISALAM CHIT FUND LIMITED, <br>   " + name + " Branch</p></div></td></tr><tr> <td colspan='3' style='text-align: left;font-size: 10px;padding: 20px 0;line-height:5px'> <div style='padding-top: 15px;'><strong style='padding-bottom: 6px;line-height:5px'>Sirs,</strong> <br><p style='margin: 6px 0;'>     I / We hereby confirm that the and the amount mentioned as above are correct.</p></div></td></tr><tr> <td> <br><span> Date:</span></td><td colspan='2'><span style='text-align:right;'>SIGNATURE</span></td></tr><tr><td colspan='3'><img src='" + border + "' width='75%'> </td></tr><tr> <td colspan='3'> <div style='font-size: 10px;padding-top: 10px;line-height:0px'><strong style='float: left;width: 10%;font-size: 10px;'>N.B. :</strong> <ul style='list-style: none;float: left;width: 90%;padding: 0;margin: 0;font-size: 10px;line-height:12px'> <li>1) Please return entire letter duly fillled in and signed.</li><li>2) Any discrepancy regarding the amount   Mentioned above shall immediately be intimated to our office with details.</li></ul> </div><hr> </td></tr></tbody> </table> </td><td style='padding: 0;'> <table width='90%' style='margin:30px;padding:30px;'> <thead> <tr align='center'> <th colspan='3' style='width: 3%;text-align:center;line-height:42pt;'> <img src='" + VISALAM + "' width='70px'></th> </tr><tr> <th colspan='3' style='text-align:center'> <span style='margin:0;padding-top:6px;font-size:12pt;text-align: center;line-height:-5px;'><strong>Branch:</strong>" + name + "</span></p></th> </tr></thead> <tbody> <tr> <td colspan='2' style='text-align: left;font-size: 10px;'><strong>To:</strong>" + aaa + "</td><td style='text-align: right;font-size: 10px;'><strong>Date: " + ddd + "</strong></td></tr><tr> <td colspan='3' style='text-align: center;padding-top:6px;'> <div style='text-align: left; font-size:10pt;margin: 5px 0 0 0;'> Dear Sir/s / Madam,<br><div style='text-align: center;font-size: 10px;line-height:12pt;'><strong>Ref. : Chit No. : " + number + "</strong> <br><span>*****</span></div></div></td></tr><tr> <td colspan='3' style='padding: 0px 0 10px 0px; font-size: 10px;text-align: justify;line-height:0pt;'> <p>     We wish to inform you that you have remitted a sum of <strong> Rs" + money + "/- </strong>(excluding dividend) towards your above mentioned Non-Prized Chit upto " + ddd + ".We request you to confirm the same.</p><p>     In the absence of the receipt of the above , Within one month from this date , the said sum will be taken as acknowledged by you to be correct.</p></td></tr><tr> <td colspan='3' style='text-align: right;padding-top:6px;line-height12px'> <div style='text-align: right;font-size: 10px;line-height10px'> Yours Faithfully                .<br><div style='text-align: right;font-size: 10px;line-height:10pt;'><span>for SREE VISALAM CHIT FUND LIMITED<span> </div></div></td></tr><tr> <td colspan='3' style='width: 50%;border-bottom: 2px solid black;text-align: right;font-size: 10px;padding:30px 0 10px 0;'> <br>Authorised Signatory           .</td></tr><tr><td colspan='3'><img src='" + border + "' width='75%'> </td></tr><tr> <td colspan='3' style='text-align: left;font-size: 10px;padding: 20px 0;line-height:5px'> <div style='line-height=0px'>TO <br><p style='margin: 6px 0;font-size: 10px;line-height=5px'>    SREE VISALAM CHIT FUND LIMITED, <br>   " + name + " Branch</p></div></td></tr><tr> <td colspan='3' style='text-align: left;font-size: 10px;padding: 20px 0;line-height:5px'> <div style='padding-top: 15px;'><strong style='padding-bottom: 6px;line-height:5px'>Sirs,</strong> <br><p style='margin: 6px 0;'>     I / We hereby confirm that the and the amount mentioned as above are correct.</p></div></td></tr><tr> <td> <br><span> Date:</span></td><td colspan='2'><span style='text-align:right;'>SIGNATURE</span></td></tr><tr><td colspan='3'><img src='" + border + "' width='75%'> </td></tr><tr> <td colspan='3'> <div style='font-size: 10px;padding-top: 10px;line-height:0px'><strong style='float: left;width: 10%;font-size: 10px;'>N.B. :</strong> <ul style='list-style: none;float: left;width: 90%;padding: 0;margin: 0;font-size: 10px;line-height:12px'> <li>1) Please return entire letter duly filled in and signed.</li><li>2) Any discrepancy regarding the amount  Mentioned above shall immediately be intimated to our office with details.</li></ul> </div><hr> </td></tr></tbody> </table> </td></tr></table></html>");

                                StringReader sr = new StringReader(sb.ToString());
                                iTextSharp.text.Document pdfDoc = new iTextSharp.text.Document();
                                iTextSharp.text.pdf.PdfDocument pdf;

                                iTextSharp.text.Rectangle rec = new iTextSharp.text.Rectangle(1009, 612);
                                pdfDoc.SetPageSize(rec);
                                pdfDoc.SetMargins(5f, 0, 0f, 0);
                                HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
                                PdfWriter writer = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                                pdfDoc.Open();
                                htmlparser.Parse(sr);
                                pdfDoc.Close();
                                Response.ContentType = "application/pdf";
                                Response.AddHeader("content-disposition", "attachment;filename=PartyConfirmation.pdf");
                                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                                Response.Write(pdfDoc);
                                HttpContext.Current.ApplicationInstance.CompleteRequest();

                            }
                        }
                    }
                    else
                    {
                        DataTable getallauction = new DataTable();
                        Decimal adddueamount = 0;
                        Decimal amountdiff = 0;
                        int paidinstno = 0;
                        decimal sumAmount = 0;
                        string DrawNO = "";
                        double arrearamnt = 0;
                        decimal arrtotal = 0;
                        double value1 = 0;
                        Int64 firstValue = 0;
                        decimal dueamount1 = 0;
                        string money = "";
                        string dueamount = "";
                        string membernamecount = "";
                        Int64 secondValue = 0;
                        string Maxdraw = "";
                        string memberaddress = "";
                        decimal moe = 0;
                        string name = "";
                        string MemberName = "";
                        decimal moneey2 = 0;
                        StringBuilder ss = new StringBuilder();
                        string name1 = getbranchdt.Rows[0]["B_Name"].ToString();
                        if(name1 == "Triplicane")
                        {
                            name = "Mount road Chennai-2";
                        }
                        else
                        {
                            name = name1;
                        }
                        string imageFile = (Server.MapPath("~\\Images\\logo_New.png"));
                        string border = (Server.MapPath("~\\Images\\border-1px.png"));
                        string VISALAM = (Server.MapPath("~\\Images\\VISALAM-LOGO_WTXT.png"));
                        string aaa = balayer.GetSingleValue("select MemberAddress from svcf.membertogroupmaster a join svcf.groupmaster b on a.GroupID=b.Head_id where a.GrpMemberID='" + number + "';");
                        string ddd = idTextbox.Text;
                        string sas = "";
                        string mon = "";
                        string dueamount2 = balayer.GetSingleValue("select CurrentDueAmount from svcf.auctiondetails where BranchID=" + branchvalue + " and GroupID=" + listGroup.SelectedValue + "  and DrawNO=1;");
                        dueamount1 = decimal.Parse(dueamount2);
                        string dueamount3 = dueamount1.ToString();
                        if(dueamount3=="")
                        {
                            dueamount = Convert.ToString(0);
                        }
                        else
                        {
                            dueamount = dueamount3;
                        }
                        DataTable dtgrp = balayer.GetDataTable("select MemberName,MemberAddress,GrpMemberID,a.Head_Id,cast(digits(GrpMemberID) as unsigned) as ChitNo1  from svcf.membertogroupmaster a join svcf.groupmaster b on a.GroupID=b.Head_id where a.GroupID='" + value + "' and a.BranchID=" + branchvalue + " order by ChitNo1;");

                        //  DataTable dtgrp = balayer.GetDataTable("select mg.MemberName, mg.MemberAddress,mg.GrpMemberID,mg.Head_Id from svcf.auctiondetails as ad join svcf.membertogroupmaster as mg on ad.PrizedMemberID=mg.Head_Id where ad.GroupID='" + value + "' and ad.BranchID=" + branchvalue + " and ad.IsPrized<>'Y'");
                        foreach (DataRow dtrow in dtgrp.Rows)
                        {
                            try
                            {
                                string aution = balayer.GetSingleValue("select IsPrized from svcf.auctiondetails where PrizedMemberID=" + dtrow.ItemArray[3] + " and IsPrized='Y' ");
                                if (aution != "Y")
                                {
                                    memberaddress = dtrow.ItemArray[1].ToString();
                                    if (memberaddress == "")
                                    {
                                        memberaddress = "null";
                                    }
                                    else
                                    {
                                        memberaddress = dtrow.ItemArray[1].ToString();
                                    }
                                    MemberName = dtrow.ItemArray[0].ToString();
                                    string money1 = balayer.GetSingleValue("select (case when( tp1.PaymentDate is null or tp1.PaymentDate >'" + balayer.indiandateToMysqlDate(ddd) + "') then sum(case when (v1.Voucher_Type='C' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) -sum(case when (v1.Voucher_Type='D' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) else 0.00 end ) as Credit from membertogroupmaster as mg1 join voucher as v1 on v1.Head_Id=mg1.Head_Id join groupmaster as vgwd1 on vgwd1.`Head_Id`=" + listGroup.SelectedValue + " left join trans_payment as tp1 on v1.Head_Id =tp1.TokenNumber join membermaster as mm on (mg1.MemberID=mm.MemberIDNew) where mg1.BranchID=" + branchvalue + " and mg1.GroupID=" + listGroup.SelectedValue + " and v1.Head_Id=" + dtrow.ItemArray[3] + " and v1.ChoosenDate<= '" + balayer.indiandateToMysqlDate(ddd) + "' group by v1.Head_Id order by cast(digits(mg1.GrpMemberID) as unsigned),v1.Voucher_Type DESC ;");
                                     moneey2 = decimal.Parse(money1);
                                    string moneey = moneey2.ToString();
                                    if(moneey=="")
                                    {
                                         mon =Convert.ToString( 0) ;
                                    }
                                    else
                                    {
                                        mon = moneey;
                                    }
                                    ss.Append(@"<table style='width: 100%;'> <tr> <td style='padding: 0;'> <table width='90%' style='padding:30px;'> <thead> <tr align='center'> <th colspan='3' style='width: 3%;text-align:center;line-height:42pt;'> <img src='" + VISALAM + "' width='70px'></th> </tr><tr> <th colspan='3' style='text-align:center'> <span style='margin:0;padding-top:6px;font-size:12pt;text-align: center;line-height:-5px;'><strong>Branch :</strong> " + name + "</span></p></th> </tr></thead> <tbody> <tr> <td colspan='2' style='text-align: left;font-size: 10px;'><strong>To:</strong><br><div style='width:60%; line-height:18px'>" + MemberName + " ,<br>" + memberaddress + "</div></td><td style='text-align: right;font-size: 10px;'><strong>Date: " + ddd + "</strong></td></tr><tr> <td colspan='3' style='text-align: center;padding-top:6px;'> <div style='text-align: left; font-size:10pt;margin: 5px 0 0 0;'> Dear Sir/s / Madam,<br><div style='text-align: center;font-size: 10px;line-height:12pt;'><strong>Ref. : Chit No. : " + dtrow["GrpMemberID"].ToString() + "</strong> <br><span>*****</span></div></div></td></tr><tr> <td colspan='3' style='padding: 0px 0 10px 0px; font-size: 10px;text-align: justify;line-height:0pt;'> <p>     We wish to inform you that you have remitted a sum of<strong> Rs " + balayer.ConvertToIndCurrency(mon) + "/-  </strong>(excluding dividend) towards your above mentioned Non-Prized Chit upto " + ddd + ".  We request you to confirm the same.</p><p>       In the absence of the receipt of the above  Within one month from this date , the said sum will be taken as acknowledged by you to be correct.</p></td></tr><tr> <td colspan='3' style='text-align: right;padding-top:6px;line-height12px'> <div style='text-align: right;font-size: 10px;line-height10px'> Yours Faithfully               .<br><div style='text-align: right;font-size: 10px;line-height:10pt;'><span>for SREE VISALAM CHIT FUND LIMITED<span> </div></div></td></tr><tr> <td colspan='3' style='width: 50%;border-bottom: 2px solid black;text-align: right;font-size: 10px;padding:30px 0 10px 0;'> <br>Authorised Signatory           .</td></tr><tr><td colspan='3'><img src='" + border + "' width='75%'> </td></tr><tr> <td colspan='3' style='text-align: left;font-size: 10px;padding: 20px 0;line-height:5px'> <div style='line-height=0px'>TO <br><p style='margin: 6px 0;font-size: 10px;line-height=5px'>    SREE VISALAM CHIT FUND LIMITED, <br>   " + name + " Branch</p></div></td></tr><tr> <td colspan='3' style='text-align: left;font-size: 10px;padding: 20px 0;line-height:5px'> <div style='padding-top: 15px;'><strong style='padding-bottom: 6px;line-height:5px'>Sirs,</strong> <br><p style='margin: 6px 0;'>     I / We hereby confirm that the amount mentioned as above is correct.</p></div></td></tr><tr> <td> <br><span> Date:</span></td><td colspan='2'><span style='text-align:right;'>SIGNATURE</span></td></tr><tr><td colspan='3'><img src='" + border + "' width='75%'> </td></tr><tr>  <td colspan='3' style='height:20px;'> <ul style='list-style: none;float: left;width: 90%;padding: 0;margin: 0;font-size: 7px;'> <li> <strong style='float: left;width: 10%;font-size: 10px;'>N.B. :</strong>1) Please return entire letter duly filled in and signed.2) Any discrepancy regarding the amount  Mentioned above shall immediately be intimated to our office with details.</li></ul> </div><hr> </td></tr></tbody> </table> </td><td style='padding: 0;'> <table width='90%' style='padding:30px;'> <thead> <tr align='center'> <th colspan='3' style='width: 3%;text-align:center;line-height:42pt;'> <img src='" + VISALAM + "' width='70px'></th> </tr><tr> <th colspan='3' style='text-align:center'> <span style='margin:0;padding-top:6px;font-size:12pt;text-align: center;line-height:-5px;'><strong>Branch :</strong> " + name + "</span></p></th> </tr></thead> <tbody> <tr> <td colspan='2' style='text-align: left;font-size: 10px;'><strong>To:</strong><br><div style='width:60%; line-height:18px'>" + MemberName + " ,<br>" + memberaddress + "</div></td><td style='text-align: right;font-size: 10px;'><strong>Date: " + ddd + "</strong></td></tr><tr> <td colspan='3' style='text-align: center;padding-top:6px;'> <div style='text-align: left; font-size:10pt;margin: 5px 0 0 0;'> Dear Sir/s / Madam,<br><div style='text-align: center;fontze: 10px;line-height:12pt;'><strong>Ref. : Chit No. : " + dtrow["GrpMemberID"].ToString() + "</strong> <br><span>*****</span></div></div></td></tr><tr> <td colspan='3' style='padding: 0px 0 10px 0px; font-size: 10px;text-align: justify;line-height:0pt;'> <p>     We wish to inform you that you have remitted a sum of <strong> Rs " + balayer.ConvertToIndCurrency(mon) + "/-  </strong>(excluding dividend) towards your above mentioned Non-Prized Chit upto " + ddd + ". We request you to confirm the same.</p><p>     In the absence of the receipt of the above  Within one month from this date , the said sum will be taken as acknowledged by you to be correct.</p></td></tr><tr> <td colspan='3' style='text-align: right;padding-top:6px;line-height12px'> <div style='text-align: right;font-size: 10px;line-height10px'> Yours Faithfully                .<br><div style='text-align: right;font-size: 10px;line-height:10pt;'><span>for SREE VISALAM CHIT FUND LIMITED<span> </div></div></td></tr><tr> <td colspan='3' style='width: 50%;border-bottom: 2px solid black;text-align: right;font-size: 10px;padding:30px 0 10px 0;'> <br>Authorised Signatory           .</td></tr><tr><td colspan='3'><img src='" + border + "' width='75%'> </td></tr><tr> <td colspan='3' style='text-align: left;font-size: 10px;padding: 20px 0;line-height:5px'> <div style='line-height=0px'>TO <br><p style='margin: 6px 0;font-size: 10px;line-height=5px'>    SREE VISALAM CHIT FUND LIMITED, <br>   " + name + " Branch</p></div></td></tr><tr> <td colspan='3' style='text-align: left;font-size: 10px;padding: 20px 0;line-height:5px'> <div style='padding-top: 15px;'><strong style='padding-bottom: 6px;line-height:5px'>Sirs,</strong> <br><p style='margin: 6px 0;'>     I / We hereby confirm that the amount mentioned as above is correct.</p></div></td></tr><tr> <td> <br><span> Date:</span></td><td colspan='2'><span style='text-align:right;'>SIGNATURE</span></td></tr><tr><td colspan='3'><img src='" + border + "' width='75%'> </td></tr><tr> <td colspan='3' style='height:20px;'> <ul style='list-style: none;float: left;width: 90%;padding: 0;margin: 0;font-size: 7px;'> <li> <strong style='float: left;width: 10%;font-size: 10px;'>N.B. :</strong>1) Please return entire letter duly filled in and signed.2) Any discrepancy regarding the amount  Mentioned above shall immediately be intimated to our office with details.</li></ul> </div><hr> </td></tr></tbody> </table> </td></tr></table>");
                                }
                                else
                                {
                                    memberaddress = dtrow.ItemArray[1].ToString();
                                    if (memberaddress == "")
                                    {
                                        memberaddress = "null";
                                    }
                                    else
                                    {
                                        memberaddress = dtrow.ItemArray[1].ToString();
                                    }
                                    MemberName = dtrow.ItemArray[0].ToString();
                                    membernamecount = balayer.GetSingleValue("select Count(*) from svcf.membertogroupmaster where GroupID='" + listGroup.SelectedValue + "'");
                                    Maxdraw = balayer.GetSingleValue("SELECT max(DrawNO) FROM svcf.auctiondetails where GroupID='" + listGroup.SelectedValue + "' and AuctionDate <= '" + balayer.indiandateToMysqlDate(ddd) + "'");
                                    string money23 = balayer.GetSingleValue("select  sum(case when (v1.Voucher_Type='C' and v1.trans_Type<>2 and v1.Other_Trans_Type<>5 ) then v1.Amount else 0.00 end) -sum(case when (v1.Voucher_Type='D' and v1.trans_Type<>2  ) then v1.Amount else 0.00 end) as Credit from membertogroupmaster as mg1 join voucher as v1 on v1.Head_Id=mg1.Head_Id join groupmaster as vgwd1 on vgwd1.`Head_Id`=" + listGroup.SelectedValue + " left join trans_payment as tp1 on v1.Head_Id =tp1.TokenNumber join membermaster as mm on (mg1.MemberID=mm.MemberIDNew) where mg1.BranchID=" + branchvalue + " and mg1.GroupID=" + listGroup.SelectedValue + " and v1.Head_Id=" + dtrow.ItemArray[3] + " and v1.ChoosenDate<='" + balayer.indiandateToMysqlDate(ddd) + "' group by v1.Head_Id order by cast(digits(mg1.GrpMemberID) as unsigned),v1.Voucher_Type DESC ;");
                                //    moe = decimal.Parse(money23);
                                    string money0 = money23.ToString();
                                    if(money23 == "")
                                    {
                                        money = "0";
                                    }
                                    else
                                    {
                                        money = money0;
                                    }
                                    getallauction = balayer.GetDataTable("select * from svcf.auctiondetails where GroupID=" + listGroup.SelectedValue + " and AuctionDate <= '" + balayer.indiandateToMysqlDate(ddd) + "'");

                                    adddueamount = 0;
                                    for (int i = 0; i < getallauction.Rows.Count; i++)
                                    {
                                        try
                                        {
                                            if (Convert.ToString(getallauction.Rows[i]["CurrentDueAmount"]) != "")
                                            {
                                                if (adddueamount == 0)
                                                    adddueamount = Convert.ToDecimal(getallauction.Rows[i]["CurrentDueAmount"]);
                                                else
                                                    adddueamount = adddueamount + Convert.ToDecimal(getallauction.Rows[i]["CurrentDueAmount"]);


                                                if (Convert.ToDecimal(money) == adddueamount)
                                                {
                                                    paidinstno = Convert.ToInt16(getallauction.Rows[i]["DrawNO"]);
                                                    if (paidinstno != Convert.ToInt32(getallauction.Rows.Count))
                                                    {
                                                        paidinstno = paidinstno + 1;
                                                    }
                                                    else
                                                    {
                                                        paidinstno = paidinstno + 1;
                                                    }
                                                    adddueamount = 0;

                                                    break;
                                                }
                                                if (Convert.ToDecimal(money) < adddueamount)
                                                {
                                                    amountdiff = (adddueamount - Convert.ToDecimal(money));

                                                    if (amountdiff < Convert.ToDecimal(getallauction.Rows[i]["CurrentDueAmount"]))
                                                    //if (!(Convert.ToDecimal(amountdiff) < Convert.ToDecimal(getallauction.Rows[i]["CurrentDueAmount"])))
                                                    {
                                                        paidinstno = Convert.ToInt16(getallauction.Rows[i]["DrawNO"]);

                                                        adddueamount = 0;

                                                        break;
                                                    }
                                                    if (!(Convert.ToDecimal(amountdiff) < Convert.ToDecimal(getallauction.Rows[i]["CurrentDueAmount"])))
                                                    {
                                                        paidinstno = Convert.ToInt16(getallauction.Rows[i]["DrawNO"]);
                                                        adddueamount = 0;

                                                        break;
                                                    }
                                                }

                                                if (!(Convert.ToDecimal(money) == adddueamount) && !(Convert.ToDecimal(money) < adddueamount))
                                                {
                                                    paidinstno = 0;
                                                }
                                            }
                                        }
                                        catch (Exception ee)
                                        {
                                            string rr = ee.Message;
                                        }

                                    }
                                    sumAmount = Convert.ToDecimal(money) / Convert.ToDecimal(dueamount);

                                    value1 = Convert.ToDouble(sumAmount);
                                    var values = value.ToString(CultureInfo.InvariantCulture).Split('.');


                                    if (values.Length > 1)
                                    {
                                        firstValue = int.Parse(values[0]);
                                        secondValue = long.Parse(values[1]);
                                    }
                                    else
                                    {
                                        firstValue = int.Parse(values[0]);
                                    }
                                    if (secondValue == '0')
                                    {
                                        firstValue++;

                                        //dr["DrawNO"] = firstValue + " - " + Maxdraw;
                                        DrawNO = paidinstno + "-" + membernamecount;
                                    }
                                    else if (paidinstno == 0)
                                    {
                                        firstValue++;
                                        //dr["DrawNO"] = firstValue + " + " + part + " - " + Maxdraw;
                                        //  dr["DrawNO"] = part + " - " + Maxdraw;
                                        DrawNO = Maxdraw + "-" + membernamecount;

                                    }
                                    else
                                    {
                                        firstValue++;
                                        DrawNO = paidinstno + "-" + membernamecount;

                                    }
                                    ss.Append(@"<table style='width: 100%;'> <tr> <td style='padding: 0;'> <table width='90%' style='padding:30px;'> <thead> <tr align='center'> <th colspan='3' style='width: 3%;text-align:center;line-height:42pt;'> <img src='" + VISALAM + "' width='70px'></th> </tr><tr> <th colspan='3' style='text-align:center'> <span style='margin:0;padding-top:6px;font-size:12pt;text-align: center;line-height:-5px;'><strong>Branch :</strong> " + name + "</span></p></th> </tr></thead> <tbody> <tr> <td colspan='2' style='text-align: left;font-size: 10px;'><strong>To:</strong> <br><div style='width:60%;'>" + MemberName + " ,<br>" + memberaddress + "</div></td><td style='text-align: right;font-size: 10px;'><strong>Date: " + ddd + "</strong></td></tr><tr> <td colspan='3' style='text-align: center;padding-top:6px;'> <div style='text-align: left; font-size:10pt;margin: 5px 0 0 0;'> Dear Sir/s / Madam, <br><div style='text-align: center;font-size: 10px;line-height:12pt;'><strong>Ref. : Chit No. : " + dtrow["GrpMemberID"].ToString() + "</strong> <br><span>*****</span></div></div></td></tr><tr> <td colspan='3' style='padding: 0px 0 0px 0px; font-size: 10px;text-align: justify;line-height:0pt;'> <p style='padding-bottom:2px;'>&nbsp;&nbsp;&nbsp;We wish to inform you that you have remitted a sum of<strong> Rs " + balayer.ConvertToIndCurrency( money) + "/- </strong>(excluding dividend) towards your above mentioned Prized Chit upto " + ddd + " .</p><p style='padding-bottom:2px;'>&nbsp;&nbsp;&nbsp;We also wish to inform you that  " + DrawNO + "  Part/Full instalment/s<strong> (Rs " + balayer.ConvertToIndCurrency( dueamount) + "/- per instalment)</strong> is/are payable towards future instalment/s of subscription/s of the above Prized Chit. We request you to confirm the same.</p><p style='padding-bottom:2px;'>&nbsp;&nbsp;&nbsp;In the absence of the receipt of the above  Within one month from this date , the said sum will be taken as acknowledged by you to be correct.</p></td></tr><tr> <td colspan='3' style='text-align: right;padding-top:2px;line-height:8;'> <div style='text-align: right;font-size: 10px;'> Yours Faithfully . <br><div style='text-align: right;font-size: 10px;line-height:10pt;'><span>for SREE VISALAM CHIT FUND LIMITED<span> </div></div></td></tr><tr> <td colspan='3' style='width: 50%;border-bottom: 2px solid black;text-align: right;font-size: 10px;padding:30px 0 10px 0;'> <br>Authorised Signatory .</td></tr><tr><td colspan='3'><img src='" + border + "' width='75%'> </td></tr><tr> <td colspan='3' style='text-align: left;font-size: 10px;padding: 20px 0;line-height:5px'> <div style='line-height=0px'>TO <br><p style='margin: 6px 0;font-size: 10px;line-height=5px'> SREE VISALAM CHIT FUND LIMITED, <br>" + name + " Branch</p></div></td></tr><tr> <td colspan='3' style='text-align: left;font-size: 10px;padding: 20px 0;line-height:5px'> <div style='padding-top: 15px;'><strong style='padding-bottom: 6px;line-height:5px'>Sirs,</strong> <br><p style='margin: 6px 0;'> I / We hereby confirm that the instalement/s and the amount mentioned as above are correct.</p></div></td></tr><tr><td colspan='3'><br></td></tr><tr> <td> <span> Date:</span></td><td colspan='2'><span style='text-align:right;'>SIGNATURE</span></td></tr><tr> <td colspan='3'><img src='" + border + "' width='75%'> </td></tr><tr> <td colspan='3' style='height:20px;'> <ul style='list-style: none;float: left;width: 90%;padding: 0;margin: 0;font-size: 7px;'> <li><strong style='float: left;width: 10%;font-size: 10px;'>N.B. :</strong>1) Please return entire letter duly filled in and signed. 2) Any discrepancy regarding the amount or instalment/s Mentioned above shall immediately be intimated to our office with details.</li></ul></td></tr></tbody> </table> </td><td style='padding: 0;'> <table width='90%' style='padding:30px;'> <thead> <tr align='center'> <th colspan='3' style='width: 3%;text-align:center;line-height:42pt;'> <img src='" + VISALAM + "' width='70px'></th> </tr><tr> <th colspan='3' style='text-align:center'> <span style='margin:0;padding-top:6px;font-size:12pt;text-align: center;line-height:-5px;'><strong>Branch :</strong> " + name + "</span></p></th> </tr></thead> <tbody> <tr> <td colspan='2' style='text-align: left;font-size: 10px;'><strong>To:</strong> <br><div style='width:60%;'>" + MemberName + " ,<br>" + memberaddress + "</div></td><td style='text-align: right;font-size: 10px;'><strong>Date: " + ddd + "</strong></td></tr><tr> <td colspan='3' style='text-align: center;padding-top:6px;'> <div style='text-align: left; font-size:10pt;margin: 5px 0 0 0;'> Dear Sir/s / Madam, <br><div style='text-align: center;font-size: 10px;line-height:12pt;'><strong>Ref. : Chit No. : " + dtrow["GrpMemberID"].ToString() + "</strong> <br><span>*****</span></div></div></td></tr><tr> <td colspan='3' style='padding: 0px 0 0px 0px; font-size: 10px;text-align: justify;line-height:0pt;'> <p style='padding-bottom:2px;'>&nbsp;&nbsp;&nbsp;We wish to inform you that you have remitted a sum of<strong> Rs " + balayer.ConvertToIndCurrency( money) + "/- </strong>(excluding dividend) towards your above mentioned Prized Chit upto " + ddd + " .</p><p style='padding-bottom:2px;'>&nbsp;&nbsp;&nbsp;We also wish to inform you that " + DrawNO + "  Part/Full instalment/s<strong> (Rs " + balayer.ConvertToIndCurrency( dueamount) + "/- per instalment)</strong> is/are payable towards future instalment/s of subscription/s of the above Prized Chit. We request you to confirm the same.</p><p style='padding-bottom:2px;'>&nbsp;&nbsp;&nbsp;In the absence of the receipt of the above Within one month from this date , the said sum will be taken as acknowledged by you to be correct.</p></td></tr><tr> <td colspan='3' style='text-align: right;padding-top:2px;line-height:8;'> <div style='text-align: right;font-size: 10px;'> Yours Faithfully . <br><div style='text-align: right;font-size: 10px;line-height:10pt;'><span>for SREE VISALAM CHIT FUND LIMITED<span> </div></div></td></tr><tr> <td colspan='3' style='width: 50%;border-bottom: 2px solid black;text-align: right;font-size: 10px;padding:30px 0 10px 0;'> <br>Authorised Signatory .</td></tr><tr><td colspan='3'><img src='" + border + "' width='75%'> </td></tr><tr> <td colspan='3' style='text-align: left;font-size: 10px;padding: 20px 0;line-height:5px'> <div style='line-height=0px'>TO <br><p style='margin: 6px 0;font-size: 10px;line-height=5px'> SREE VISALAM CHIT FUND LIMITED, <br>" + name + " Branch</p></div></td></tr><tr> <td colspan='3' style='text-align: left;font-size: 10px;padding: 20px 0;line-height:5px'> <div style='padding-top: 15px;'><strong style='padding-bottom: 6px;line-height:5px'>Sirs,</strong> <br><p style='margin: 6px 0;'> I / We hereby confirm that the instalement/s and the amount mentioned as above are correct.</p></div></td></tr><tr><td colspan='3'><br></td></tr><tr> <td> <span> Date:</span></td><td colspan='2'><span style='text-align:right;'>SIGNATURE</span></td></tr><tr> <td colspan='3'><img src='" + border + "' width='75%'> </td></tr><tr> <td colspan='3' style='height:20px;'> <ul style='list-style: none;float: left;width: 90%;padding: 0;margin: 0;font-size: 7px;'> <li><strong style='float: left;width: 10%;font-size: 10px;'>N.B. :</strong>1) Please return entire letter duly filled in and signed. 2) Any discrepancy regarding the amount or instalment/s Mentioned above shall immediately be intimated to our office with details.</li></ul></td></tr></tbody> </table> </td></tr></table>");
                                }
                            }
                            catch(Exception u)
                            {
                                string me = u.Message;
                            }
                            
                        }

                        using (StringWriter sw = new StringWriter())
                        {
                            using (HtmlTextWriter hw = new HtmlTextWriter(sw))
                            {
                                StringReader sr = new StringReader(ss.ToString());

                                iTextSharp.text.Document pdfDoc = new iTextSharp.text.Document();
                                //iTextSharp.text.Rectangle rec = new iTextSharp.text.Rectangle(1009, 612);
                                iTextSharp.text.Rectangle rec = new iTextSharp.text.Rectangle(1020, 650);
                                pdfDoc.SetPageSize(rec);
                                pdfDoc.SetMargins(5f, 0, 0f, 0);
                                HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
                                PdfWriter writer = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);

                                pdfDoc.Open();
                                htmlparser.Parse(sr);
                                pdfDoc.Close();

                                Response.ContentType = "application/pdf";
                                Response.AddHeader("content-disposition", "attachment;filename=PartyConfirmationNonPrized.pdf");
                                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                                Response.Write(pdfDoc);
                                HttpContext.Current.ApplicationInstance.CompleteRequest();
                            }
                        }
                    }
                }
                catch(Exception e)
                {
                    string ee = e.Message;
                }
            }
        }


        protected void pdfgenonclick(object sender, EventArgs e)
        {
            selectgroup();

        }
        protected void ExportToExcel(object sender, EventArgs e)
        {
            Response.ContentType = "application/x-msexcel";
            Response.AddHeader("Content-Disposition", "attachment;filename = ExcelFile.xls");
            Response.ContentEncoding = Encoding.UTF8;
            StringWriter tw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(tw);
            tble.RenderControl(hw);
            Response.Write(tw.ToString());
            Response.End();
        }

        //public void BindTreeView()
        //{
        //    qry = "SELECT NodeID,Node FROM svcf.headstree where ParentID=1;";
        //    //qry = "";
        //    DataTable getMenu = new DataTable();
        //    getMenu = balayer.GetDataTable(qry);
        //    this.PopulateTreeView(getMenu, 0, null);
        //}

        //public void PopulateTreeView(DataTable dt, int parentId, TreeNode treeNode)
        //{
        //    foreach (DataRow row in dt.Rows)
        //    {
        //        TreeNode Parent = new TreeNode
        //        {
        //            Text = row["Node"].ToString(),
        //            Value = row["NodeID"].ToString()
        //        };
        //        if (parentId == 0)
        //        {
        //            MenuView.Nodes.Add(Parent);
        //            DataTable dtChild = balayer.GetDataTable("SELECT Head_Id,GROUPNO FROM svcf.groupmaster where BranchID= " + Parent.Value);
        //            PopulateSubNodeView(dtChild, int.Parse(Parent.Value), Parent);
        //        }
        //        else
        //        {
        //            treeNode.ChildNodes.Add(Parent);
        //        }
        //    }
        //    MenuView.CollapseAll();
        //}

        //public void PopulateSubNodeView(DataTable dt, int parentId, TreeNode treeNode)
        //{
        //    foreach (DataRow row in dt.Rows)
        //    {
        //        TreeNode child = new TreeNode
        //        {
        //            Text = row["GROUPNO"].ToString(),
        //            Value = row["Head_Id"].ToString()
        //        };
        //        if (parentId == 0)
        //        {
        //            treeNode.ChildNodes.Add(child);

        //        }
        //        else
        //        {
        //            treeNode.ChildNodes.Add(child);
        //            DataTable dtChild2 = balayer.GetDataTable("SELECT Head_Id,GrpMemberID FROM svcf.membertogroupmaster where GroupID= " + child.Value);
        //            PopulateSubNodeView2(dtChild2, int.Parse(child.Value), child);

        //        }

        //    }
        //}

        //public void PopulateSubNodeView2(DataTable dtChild2, int p, TreeNode treeNode1)
        //{
        //    foreach (DataRow row in dtChild2.Rows)
        //    {
        //        TreeNode child2 = new TreeNode
        //        {
        //            Text = row["GrpMemberID"].ToString(),
        //            Value = row["Head_Id"].ToString()
        //        };
        //        treeNode1.ChildNodes.Add(child2);
        //    }
        //}

        //protected void btnGenerate_Click(object sender, EventArgs e)
        //{
        //    string CheckedParentNodes = "";
        //    string CheckedChildNodes = "";
        //    string CheckedChildNodes2 = "";
        //    string condition = "";
        //    foreach (TreeNode item in MenuView.CheckedNodes)
        //    {
        //        if ((item.Checked == true) && (item.DataItem == item.Parent))
        //        {
        //            CheckedParentNodes += item.Value + ",";
        //        }


        //        foreach (TreeNode node in item.ChildNodes)
        //        {
        //            if (node.Checked == true)
        //            {

        //                CheckedChildNodes += node.Value + ",";

        //                foreach (TreeNode child in node.ChildNodes)
        //                {
        //                    if (child.Checked == true)
        //                    {
        //                        CheckedChildNodes2 += child.Value + ",";
        //                    }

        //                }

        //            }


        //        }
        //    }

        //    if (CheckedParentNodes.Length > 0)
        //    {
        //        CheckedParentNodes = CheckedParentNodes.Remove(CheckedParentNodes.Length - 1);
        //        condition = " where branch.NodeID in (" + CheckedParentNodes.ToString() + ")";
        //    }

        //    if (CheckedChildNodes.Length > 0)
        //    {
        //        CheckedChildNodes = CheckedChildNodes.Remove(CheckedChildNodes.Length - 1);
        //        condition = " where mgrpmstr.GroupID in (" + CheckedChildNodes.ToString() + ")";
        //    }


        //    if (CheckedChildNodes2.Length > 0)
        //    {
        //        CheckedChildNodes2 = CheckedChildNodes2.Remove(CheckedChildNodes2.Length - 1);
        //        condition = " where mgrpmstr.Head_Id in (" + CheckedChildNodes2.ToString() + ")";
        //    }

        //    string _html = "";
        //    string filepath = "~/IntimationLetter.html";
        //    //string _tr = "";
        //    string Body = "";
        //    using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath(filepath)))
        //    {
        //        _html = reader.ReadToEnd();
        //    }
        //    string[] _Body = _html.Split('|');
        //    DataTable GetEmailDetails = new DataTable();
        //    //DataTable dt = balayer.GetDataTable("SELECT mgrpmstr.MemberName as MemberName,mgrpmstr.MemberAddress as MemberAddress,branch.Node as branch,mgrpmstr.Head_Id as chitNo,mstr.MemberID as ChitAgrNo,auction.DrawNO as DrawNO,mgrpmstr.GroupID as GroupID,details.B_Address as branchAddress"
        //    //                                  + " FROM svcf.membertogroupmaster mgrpmstr"
        //    //                                  + " join svcf.headstree branch on mgrpmstr.BranchID=branch.NodeID"
        //    //                                  + " left join svcf.branchdetails details on branch.NodeID=details.Head_Id"
        //    //                                  + " join svcf.membermaster mstr on mgrpmstr.BranchID= mstr.BranchId"
        //    //                                  + " join svcf.auctiondetails auction on auction.GroupID=mgrpmstr.GroupID " + condition + "  Group by mgrpmstr.MemberID");

        //    DataTable dt = balayer.GetDataTable("SELECT mtg.MemberName as MemberName,mtg.MemberAddress as MemberAddress,mtg.BranchID as branch,mtg.Head_Id as chitNo,mm.MemberID as ChitAgrNo,auction.DrawNO as DrawNO,mtg.GroupID as GroupID," 
        //                                                   +" bd.B_Address as branchAddress   FROM svcf.membertogroupmaster as mtg  left join svcf.branchdetails as bd on (mtg.BranchID=bd.Head_Id) join svcf.membermaster as mm on (mtg.BranchID= mm.BranchId) "
        //                                        +" join svcf.auctiondetails as auction on (auction.GroupID=mtg.GroupID) where mtg.BranchId= 1481 and mtg.GroupID=1111222 Group by mtg.MemberID;");

        //    if (dt.Rows.Count > 0)
        //    {
        //        for (int i = 0; i < dt.Rows.Count; i++)
        //        {
        //            string _tr = "";

        //            _Body[0] = _Body[0].Replace("$$Branch$$", dt.Rows[i]["branch"].ToString());
        //            _Body[0] = _Body[0].Replace("$$Date$$", System.DateTime.Now.ToString());
        //            _Body[0] = _Body[0].Replace("$$ChitNo$$", dt.Rows[i]["chitNo"].ToString());
        //            _Body[0] = _Body[0].Replace("$$ChitAgreementNo$$", "$ " + dt.Rows[i]["ChitAgrNo"].ToString());
        //            _Body[0] = _Body[0].Replace("$$drawNo$$", dt.Rows[i]["DrawNO"].ToString());
        //            _Body[0] = _Body[0].Replace("$$Address$$", dt.Rows[i]["branchAddress"].ToString());

        //            DataTable dtauction = balayer.GetDataTable("select max(DrawNO) as DrawNo  from auctiondetails where GroupID = (" + dt.Rows[i]["GroupID"] + ")");

        //            DataTable dtValue = balayer.GetDataTable("select MemberID,AuctionDate,CurrentDueAmount,NextDueAmount,PrizedAmount,Dividend From auctiondetails where DrawNo=" + dtauction.Rows[0]["DrawNo"] + " and  GroupID =" + dt.Rows[i]["GroupID"] + "");

        //            _Body[0] = _Body[0].Replace("$$SubscriberNo$$", dtValue.Rows[0]["MemberID"].ToString());
        //            string auctionyear = string.Format("{0:yyyy}", Convert.ToDateTime(dtValue.Rows[0]["AuctionDate"]));
        //            string auctionday = string.Format("{0:dd}", Convert.ToDateTime(dtValue.Rows[0]["AuctionDate"]));
        //            string auctionTime = string.Format("{0:HH:mm:ss tt}", Convert.ToDateTime(dtValue.Rows[0]["AuctionDate"]));
        //            _Body[0] = _Body[0].Replace("$$Year$$", auctionyear);
        //            _Body[0] = _Body[0].Replace("$$Day$$", auctionday);
        //            _Body[0] = _Body[0].Replace("$$Time$$", auctionTime);
        //            _tr += "<tr>" +
        //                                  " <th>Instalment payable Rs." + dtValue.Rows[0]["NextDueAmount"] + "</th>" +
        //                                  " <th>Draw dividend Rs. " + dtValue.Rows[0]["Dividend"] + "</th>" +
        //                                  "</tr>";
        //            _tr += "<tr align='center'>" +
        //                   "<td>Due Date :" + dtValue.Rows[0]["AuctionDate"] + "</td>" +
        //                   "<td style='padding-left:30px;'>Draw Prize Amount Rs." + dtValue.Rows[0]["PrizedAmount"] + "/-</td>" +
        //                    "</tr>";
        //            Body = _Body[0] + _tr + _Body[1];

        //            MailMessage Msg = new MailMessage();
        //            Msg.From = new MailAddress("kalpana.gunasekaran@adcltech.com");
        //            Msg.To.Add("a.tamilselvam@gmail.com");
        //            Msg.CC.Add("jeyashankar.selvaraj@adcltech.com");

        //            Msg.Subject = "Reg:Sree Visalam Chit Fund - Auction Initimation Letter ";

        //            Msg.Body = Body;
        //            Msg.IsBodyHtml = true;
        //            SmtpClient smtp = new SmtpClient();
        //            smtp.Host = "smtp.gmail.com";
        //            smtp.Port = 587;
        //            smtp.EnableSsl = true;
        //            smtp.Credentials = new System.Net.NetworkCredential("priya.kamallesh@altiussolution.com", "vinayagar123");

        //            smtp.Send(Msg);

        //            logger.Info("AuctionIntimationLetter.aspx - btnGenerate_Click() - Completed: " + DateTime.Now + " by: " + Convert.ToString(Session["UserName"]) + "");
        //        }

        //    }
        //    else
        //    {
        //        //No Members found in the group
        //        lblmesssage.Visible = true;
        //        lblmesssage.Text = "No Chit Group to send Auction Intimation Letter.";
        //    }

        //}

    }
}