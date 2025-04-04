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

namespace SreeVisalamChitFundLtd_phase1
{
    public partial class AuctionInitimationLetter : System.Web.UI.Page
    {
        #region Object
        BusinessLayer balayer = new BusinessLayer();
        TransactionLayer trn = new TransactionLayer();

        string qry = "";
        #endregion

        #region VarDeclaration

        string imagetype = "";
        string filename = "";
        string contentType = "";
        string firstassvalue = "";
        string secondvalue = "";
        string assvalue = "";
        string assvalue1 = "";

        int countvalue;
        #endregion

        ILog logger = log4net.LogManager.GetLogger(typeof(AuctionInitimationLetter));

        protected void Page_Load(object sender, EventArgs e)
        {
            int gpval;
            if (!IsPostBack)
            {
                //BindTreeView();
                LoadbranchList();
               
                gpval  = 0;
                LoadGroupList(gpval);
                // MenuView.Attributes.Add("onclick", "OnTreeClick(event)");
            }
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
                    dtgrp = balayer.GetDataTable("select Head_id,GROUPNO from groupmaster where BranchId=" + id + " and ChitEndDate >='"+dtw+"' ;");
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
                dtgrp.Rows.InsertAt(dr, 0);
                listGroup.DataSource = dtgrp;
                listGroup.DataBind();
            
            
            }

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
                DateTime AgreementDate1 = Convert.ToDateTime(chitvalue.Rows[0]["AgreementDate"]);
                string AgreementDate= AgreementDate1.ToString("dd/MM/yyyy");
                string AuctionTime1 = chitvalue.Rows[0]["AuctionTime"].ToString();
                //Auctiondt = Convert.ToString(Auctiondt2).Split(' ')[0];
                string AuctionTime = AuctionTime1.Replace(":00","");
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
                    DateTime Auctiondt2 =Convert.ToDateTime( Auctiondt1.Rows[0]["AuctionDate"]);
                    Auctiondt = Auctiondt2.ToString("dd/MM/yyyy");
                    DateTime Auctiondt4 = Auctiondt2.AddDays(-1);
                    Auctiondt3= Auctiondt4.ToString("dd/MM/yyyy");
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
                if (countvalue < 150 )
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
                  string imageFile = (Server.MapPath("~\\Images\\logo_New.png"));
                Document doc = new Document();


                
                using (StringWriter sw = new StringWriter())
               {
                   using (HtmlTextWriter hw = new HtmlTextWriter(sw))
                   {
                      // iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(imageFile);
                      
                       
                       StringBuilder sb = new StringBuilder();



                        //      sb.Append(@"<html> <body>
                        //                <h1 align='center'>Sree Visalam Chit Fund Ltd..,</h1>
                        //                <h6  align='Top'>Estd:1947</h6>
                        //                <h4  align='center'>Regd. Off:Tirunelveli -627006.Admn. Off:Pallathur -630 107</h4>
                        //                <h4 style=""word - spacing: 2px; "" align='center'> <b>Branch:   " + getbranchdt.Rows[0]["B_Name"]
                        //                + " </b></h4><h3  align='right' >Date:" + today + "</h3> <h3 style='word - spacing: 2px;'    align='left'>CHIT NO:" + chitvalue.Rows[0]["GROUPNO"] 
                        //                + "</h3> <h4  align='center'> NOTICE TO SUBSCRIBERS OF CHIT AGREEMENT NO:" + chitvalue.Rows[0]["ChitAgreementNo"] + " OF " + chitvalue.Rows[0]["ChitAgreementYear"]
                        //                + "</h4> <h4  align='center'>(Section 16 and Rule 16)</h4>  <h3  align='center'>******</h3><br>"
                        //               + "<h4 align='left'>           This is to inform you that the " + (countval + 1) + "<sup style='font-size: 5px'>"+ assvalue + "</sup> draw in Monthly / Fortnightly /Tri-Monthly Chit,  Agreement No:" + chitvalue.Rows[0]["ChitAgreementNo"] + " of " + chitvalue.Rows[0]["ChitAgreementYear"] + " Dated: " + chitvalue.Rows[0]["AgreementDate"] + "  in Which you are one of the subscribers will be Held on <b>" +Auctiondt +" at " + getbranchdt.Rows[0]["B_Address"] +
                        //               "</b></h4><h4 align='left'>          You May kindly make it convenient to be present at the draw in person or by tender or by your duly authorised agent. The details of Subscription Payable is noted hereunder. </h4><h5>" + (countval + 1) + "<sup style='font-size: 5px'>" + assvalue + "</sup> Installment payable Rs.<b>" + auctionval.Rows[0]["NextDueAmount"] + "/-</b>     " + countval + " <sup style='font-size: 5px'>" + assvalue + "</sup> Draw divident Rs.<b>" + auctionval.Rows[0]["Dividend"] + "/-</b></h5>"
                        //               + "<h5>Due Date: " + countval + "<sup style='font-size: 5px'>" + assvalue + "</sup> Draw Prize Amount Rs.<b>" + auctionval.Rows[0]["PrizedAmount"] + "/-</b> </h5><h5>3<sup style='font-size: 5px'>th</sup>Instalment Due Date:09.06.2018</h5>  <h4  align='center'>Thanking you </h4>"
                        //               + " <h5 align='Right'> Yours Faithfully</h5> <br><br> <h5 align='Right'>For and on behalf of FOREMAN.</h5>"
                        //               + "<h3> Note:-</h3>"
                        //               + "<ol>"
                        //               + "<li>This intimation is being sent only for information.</li><li>Non receipt of this intimation will not affect the conditions for default in remitting the instalments. </li> "
                        //               +" <li>The subscriber or his Guarantors have no right to claim any relief on the basis of this intimation letter during course of Dispute if any relating to this Chit.</li>"
                        //               +" <li>Subscribers are requested to remit the previous arrears if any with interest and penalty as per the terms of the Chit Agreement.</li></ol>"
                        //+"</body> </html>");
                        sb.Append(@"<html><body><table font-family width='100%' cellpadding='1'><tbody><tr><td style='width:50%'> <strong>Estd:1947</strong></td><td style='text-align: right;width:50%'> <strong>Form : IX</strong></td></tr><tr align='center'><td colspan='2' style='line-height:75pt;'><img src="+ imageFile + "></td></tr><tr><td colspan='2' style='text-align: center;'><center><p style='font-size:20pt;margin:0;text-align: center;line-height:14pt'><strong>SREE VISALAM CHIT FUND LIMITED..,</strong></p><p style='margin:0;text-align: center'><strong>Regd.Off:Tirunelveli -627006. Admn.Off:Pallathur -630 107</strong></p><p style='font-size:13pt;margin:0;text-align: center'>Branch: " + getbranchdt.Rows[0]["B_Name"] + "</p></center></td></tr><tr><td colspan='2' style='text-align: right;'>Date:"+ today + "</td></tr><tr><td colspan='2'>CHIT NO: " + chitvalue.Rows[0]["GROUPNO"] + "/</td></tr><tr><td colspan='2' style='text-align: center;padding-top:6px;'><center><div><strong>NOTICE TO SUBSCRIBERS OF CHIT AGREEMENT NO:" + chitvalue.Rows[0]["ChitAgreementNo"] + " OF " + chitvalue.Rows[0]["ChitAgreementYear"] + "</strong></div><div style='font-size: 9pt;'>(Section 16 and Rule 16)</div> <span>*****</span></center></td></tr><tr><td colspan='2' style='padding: 10px 40px;line-height: 14pt;font-size: 12pt;text-align:justify;'colspan='2'>                     This is to inform you that the " + (countval + 1) + " <sup style='font-size: 5px'>" + assvalue + "</sup> draw in " + chitvalue.Rows[0]["ChitCategory"] + " chit,  Agreement No: " + chitvalue.Rows[0]["ChitAgreementNo"] + " of " + chitvalue.Rows[0]["ChitAgreementYear"] + " Dated: "  + AgreementDate + "  in Which you are one of the subscribers will be Held on " + Auctiondt + " between "+ AuctionTime + " and "+ AuctionEndTime + " at " + getbranchdt.Rows[0]["B_Address"] + "<br><br>You May kindly make it convenient to be present at the draw in person or by tender or by your duly authorised agent. The details of Subscription Payable is noted hereunder.</td></tr><tr><td style='font-size: 10pt;width: 50%;'>" + (countval + 1) + " <sup style = 'font-size: 5px'> " + assvalue + " </sup> Installment payable Rs. " + auctionval.Rows[0]["NextDueAmount"] + "/- <br>" + (countval + 1) + " <sup style = 'font-size: 5px'> " + assvalue + " </sup> Instalment Due Date: "+ Auctiondt3 + " </td><td style='font-size: 10pt;width: 50%;'>" + countval + " <sup style = 'font-size: 5px'> " + assvalue1 + " </sup> Draw dividend Rs." + auctionval.Rows[0]["Dividend"] + "/-<br>" + countval + " <sup style = 'font-size: 5px'> " + assvalue1 + " </sup> Draw Prize Amount Rs." + auctionval.Rows[0]["PrizedAmount"] + "/-</td></tr><tr><td colspan='2' style='text-align: center'>Thanking you</td></tr><tr><td colspan='2'><p style='text-align:right;margin: 0;'>Yours Faithfully<br><br><br><span>For and on behalf of FOREMAN.</span></p></td></tr><tr><td colspan='2'><div style='font-size: 9pt;'> <strong font-size:16px;>Note:</strong><br><br><ol><li>This intimation is being sent only for information.</li><li>Non receipt of this intimation will not affect the conditions for default in remitting the instalments.</li><li>The subscriber or his Guarantors have no right to claim any relief on the basis of this intimation letter during course of Disputes if any relating to this Chit.</li><li>Subscribers are requested to remit the previous arrears if any with interest and penalty as per the terms of the Chit Agreement.</li></ol></div></td></tr></tbody></table></body></html>");



                        StringReader sr = new StringReader(sb.ToString());
                       //iTextSharp.text.Document pdfDoc = new iTextSharp.text.Document(PageSize.A4, 10f, 10f, 10f, 0f);
                       iTextSharp.text.Document pdfDoc = new iTextSharp.text.Document(PageSize.EXECUTIVE, 50f, 50f, 30f, 10f);
                       HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
                       PdfWriter writer = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                      // iTextSharp.text.Image myImage = iTextSharp.text.Image.GetInstance(imageFile);
                       //myImage.ScaleToFit(300f, 250f);
                       //myImage.SpacingBefore = 50f;
                      // myImage.SpacingAfter = 10f;
                      // myImage.Alignment = Element.ALIGN_CENTER;
                       //myImage.ScalePercent(50f);
                       pdfDoc.Open();
                      // pdfDoc.Add(myImage);
                       htmlparser.Parse(sr);
                       pdfDoc.Close();
                       Response.ContentType = "application/pdf";
                       Response.AddHeader("content-disposition", "attachment;filename=AuctionIntimation.pdf");
                       Response.Cache.SetCacheability(HttpCacheability.NoCache);
                       Response.Write(pdfDoc);
                       //Response.End();
                       HttpContext.Current.ApplicationInstance.CompleteRequest();
                   
                   
                   
                   }
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