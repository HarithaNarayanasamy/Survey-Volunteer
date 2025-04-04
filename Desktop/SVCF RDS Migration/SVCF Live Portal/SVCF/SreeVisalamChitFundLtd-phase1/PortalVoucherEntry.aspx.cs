using log4net;
using SVCF_BusinessAccessLayer;
using SVCF_TransactionLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.Drawing.Imaging;
using System.IO;
using System.Globalization;

namespace SreeVisalamChitFundLtd_phase1
{
    public partial class PortalVoucherEntry : System.Web.UI.Page
    {
        private const int V = 0;
        #region VarDeclaration
        Dictionary<string, string> Tempdic = new Dictionary<string, string>();
        List<string> TempList = new List<string>();
        List<string> TL = new List<string>();
        string maxdt;
        static string[] ddltooltip;
        int cash; int cheque;
        string series = "PORTAL";
        #endregion

        #region ObjectDecl
        BusinessLayer balayer = new BusinessLayer();
        TransactionLayer trn = new TransactionLayer();
        #endregion

        string userinfo = "";
        string qry = "";
        string usrRole = ""; int myRandomNo;
        DataTable dtCurrentTable = new DataTable();
        ILog logger = log4net.LogManager.GetLogger(typeof(crrnew));
        private object img16List;

        public object UpdateProgress1 { get; private set; }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (Session["UserName"] == null || Session["Branchid"] == null || Session["BranchName"] == null)
            {
                Response.Redirect(Page.ResolveUrl("~/Login.aspx"), true);
            }
        }
        protected void Page_Init(object sender, EventArgs e)
        {
            //img16List.ImageUrl = Page.ResolveUrl("~/pertho_admin_v1.3/img/ico/icSw2/16-List.png");
            //Image img = (Image)UpdateProgress1.FindControl("imgWaiting");
            //img.ImageUrl = Page.ResolveUrl("~/Styles/Image/waiting.gif");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //pnlmsg.Visible = false;
            //Pnlgendrate.Visible = false;
            ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
            scriptManager.RegisterPostBackControl(this.Print);
            scriptManager.RegisterPostBackControl(this.bt1Generate);
            scriptManager.RegisterPostBackControl(this.IDAdd);
            scriptManager.RegisterPostBackControl(this.Idother);

            rvDate.MinimumValue = balayer.GetSingleValue("SELECT DATE_FORMAT( MinimumDate, '%d/%m/%Y') MinimumDate FROM svcf.restrictionmaster where BranchID=" + Session["Branchid"]);
            rvDate.MaximumValue = DateTime.Now.ToString("dd/MM/yyyy");
            txtDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txtChequeDate.Text = DateTime.Now.ToString("dd/MM/yyyy");


            if (!Page.IsPostBack)
            {
                try
                {
                    Session["CheckRefresh"] = System.Guid.NewGuid().ToString();

                    SetInitialRow();
                    SetInitialRowCheque();
                    //SetInitialRowCheque();

                    //  TxtSNo.Text = balayer.ToobjectstrEvenNull(Session["Branchid"]) + "OFF" + Convert.ToInt32(reciprnum.ToString());
                    CollectorName();
                    //Chitno();
                    BranchName();
                    fillBankHead();
                    bankdetails();
                    string branchcode = balayer.GetSingleValue("select B_Prefix from svcf.branchdetails where Head_Id=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + "");
                    //Random rnd = new Random();
                    string rnQry = "select ifnull( max(Voucher_No),0)+1 from voucher where BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and Series='PORTAL' and Trans_Type=1";
                    int voucherno = int.Parse(balayer.GetSingleValue(rnQry));
                    //myRandomNo = rnd.Next(100000, 999999);
                    //Console.WriteLine("A" + "-" + myRandomNo);
                    //TxtSNo.Text = branchcode + '/' + "OFF" + '/' + 'A' + '-' + myRandomNo;
                    TxtSNo.Text = branchcode + "/OFF/A-" + voucherno.ToString().PadLeft(6, '0');

                }
                catch (Exception) { }
            }
            logger.Info("CRR - current page- at: " + DateTime.Now);
        }

        public void CollectorName()
        {

            Tempdic.Clear();

            Tempdic = balayer.CmnList("Select distinct asr.moneycollid,mc.moneycollname from moneycollector as mc join assignreceiptbook as asr " +
                    "on (asr.moneycollid=mc.moneycollid) where asr.BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]));
            dlBranchcollectorrName.DataValueField = "Key";
            dlBranchcollectorrName.DataTextField = "Value";

            dlBranchcollectorrName.DataSource = Tempdic;
            dlBranchcollectorrName.DataBind();
            dlBranchcollectorrName.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
            //ddlReceiptSeries.Focus();

        }

        void fillBankHead()
        {
            DataTable dtBank = balayer.GetDataTable("SELECT concat( concat(t1.BankName,' _ ',t1.IFCCode),' _ ',t1.AccountNo) as BankDetails, t1.Head_Id as Head_Id, t1.Banklocation as Banklocation FROM svcf.bankdetails as t1 where t1.BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]));
            DataRow dr = dtBank.NewRow();
            dr[0] = "--Select--";
            dr[1] = "0";
            ddlBankHead.DataValueField = "Head_Id";
            ddlBankHead.DataTextField = "BankDetails";
            dtBank.Rows.InsertAt(dr, 0);
            ddlBankHead.DataSource = dtBank;
            ddlBankHead.DataBind();
        }

        protected void ddlBankDetails_Click(object sender, EventArgs e)
        {
            LabBankDetailsText.Text = ddlBankDetails.SelectedItem.Text;
            LabBankDetailsID.Text = ddlBankDetails.SelectedItem.Value;
        }

        protected void ddlBankHead_Click(object sender, EventArgs e)
        {
            LabBankHeadText.Text = ddlBankHead.SelectedItem.Text;
            LabBankHeadID.Text = ddlBankHead.SelectedItem.Value;
        }

        public void bankdetails()
        {

            DataTable dtBank = balayer.GetDataTable("select BankName, Head_Id from svcf.bankdetails");
            DataRow dr = dtBank.NewRow();
            dr[0] = "--Select--";
            dr[1] = "0";
            ddlBankDetails.DataValueField = "Head_Id";
            ddlBankDetails.DataTextField = "BankName";
            dtBank.Rows.InsertAt(dr, 0);
            ddlBankDetails.DataSource = dtBank;
            ddlBankDetails.DataBind();
        }

        public void BranchName()
        {

            Tempdic.Clear();

            Tempdic = balayer.CmnList("SELECT Head_Id,B_Name FROM branchdetails");

            ddlbranchList1.DataValueField = "Key";
            ddlbranchList1.DataTextField = "Value";

            ddlbranchList1.DataSource = Tempdic;
            ddlbranchList1.DataBind();
            ddlbranchList1.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));


        }


        protected void dlBranchcollectorrName_Click(object sender, EventArgs e)
        {
            //LabBanmelist.Text = ddlbranchList1.SelectedItem.Text;
            LabBname.Text = dlBranchcollectorrName.SelectedItem.Text;
            LabBnameID.Text = dlBranchcollectorrName.SelectedItem.Value;

        }

        protected void ddlbranchList1_Click(object sender, EventArgs e)
        {
            LabBanmelist.Text = ddlbranchList1.SelectedItem.Text;
            LabBanmelistID.Text = ddlbranchList1.SelectedItem.Value;


            //DataTable dtmemTab;
            //dtmemTab = balayer.GetDataTable("select GrpMemberID ,Head_Id  from svcf.membertogroupmaster where BranchID=" + ddlbranchList1.SelectedItem.Value + "");
            //DddlChitNO.DataSource = dtmemTab;
            //DataRow dr = dtmemTab.NewRow();
            //dr[0] = "--Select--";
            //dr[1] = "0";
            //DddlChitNO.DataTextField = "GrpMemberID";
            //DddlChitNO.DataValueField = "Head_Id";
            //dtmemTab.Rows.InsertAt(dr, 0);
            //DddlChitNO.DataBind();
            //DddlChitNO.Focus();
            Tempdic.Clear();
            Tempdic = balayer.CmnList(@"SELECT Head_Id,GrpMemberID FROM membertogroupmaster where  BranchID=" + ddlbranchList1.SelectedItem.Value);
            DddlChitNO.DataValueField = "Key";
            DddlChitNO.DataTextField = "Value";

            DddlChitNO.DataSource = Tempdic;
            DddlChitNO.DataBind();

            DddlChitNO.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));


        }

        protected void DddlChitNO_SelectedIndexChanged(object sender, EventArgs e)
        {
            LabChit.Text = DddlChitNO.SelectedItem.Text;
            LabChitID.Text = DddlChitNO.SelectedItem.Value;


            DataTable dtmemTab;

            dtmemTab = balayer.GetDataTable("select MemberName,MemberID from svcf.membertogroupmaster where GrpMemberID='" + DddlChitNO.SelectedItem.Text + "'");
            ddlMembername.DataSource = dtmemTab;
            DataRow dr = dtmemTab.NewRow();
            dr[0] = "--Select--";
            dr[1] = "0";
            ddlMembername.DataTextField = "MemberName";
            ddlMembername.DataValueField = "MemberID";
            dtmemTab.Rows.InsertAt(dr, 0);
            ddlMembername.DataBind();

            string name = balayer.GetSingleValue("select MemberName from svcf.membertogroupmaster where GrpMemberID='" + DddlChitNO.SelectedItem.Text + "'");
            string Memid = balayer.GetSingleValue("select MemberID from svcf.membertogroupmaster where GrpMemberID='" + DddlChitNO.SelectedItem.Text + "'");
            LabMemname.Text = name;
            LabMemnameID.Text = Memid;

        }
        //protected void ddlMembername_click(object sender, EventArgs e)
        //{
        //    LabMemname.Text = ddlMembername.SelectedItem.Text;
        //    LabMemnameID.Text = ddlMembername.SelectedItem.Value;
        //}

        private void SetInitialRow()
        {
            DataTable bindgrid = new DataTable();
            bindgrid.Columns.Add("SeriesNumber");
            bindgrid.Columns.Add("Date");
            bindgrid.Columns.Add("ReceivedByID");
            bindgrid.Columns.Add("ReceivedBy");
            bindgrid.Columns.Add("ChitnoID");
            bindgrid.Columns.Add("Chitno");
            bindgrid.Columns.Add("MemberNameID");
            bindgrid.Columns.Add("MemberName");
            bindgrid.Columns.Add("BranchNameID");
            bindgrid.Columns.Add("BranchName");
            bindgrid.Columns.Add("ChitAmount");
            bindgrid.Columns.Add("MiscAmount");
            bindgrid.Columns.Add("OtherAmount");
            bindgrid.Columns.Add("TotalAmount");
            bindgrid.Columns.Add("SeriesNum");

            GridView2.DataSource = bindgrid;
            GridView2.DataBind();
            ViewState["CurrentTable"] = bindgrid;
        }
        protected void IDAdd_Click(object sender, EventArgs e)
        {
            if (CheckCash.Checked == true)
            {

                Gridbined();


            }
            else
            {
                // ClientScript.RegisterStartupScript(this.GetType(), "Alert", "Please Check Your Values", true);
                Response.Write("<script>alert('Please fill out all required fields');</script>");
            }
            //txtDate.Text = "";
            //TxtSNo.Text = "";



        }
        void Gridbined()
        {
            string DualTransactionKey = "";

            System.Guid guid = Guid.NewGuid();
            string guidForChar36 = guid.ToString();
            string hexstring = BitConverter.ToString(guid.ToByteArray());
            string guidForBinary16 = "0x" + hexstring.Replace("-", string.Empty);
            DualTransactionKey = guidForBinary16;
            DateTime dtChoosenDate = DateTime.Parse(txtChequeDate.Text);
            DateTime dtChoosenDate1 = DateTime.Parse(txtDate.Text);
            string details = "details";
            string bankhead = "bankhead";
            string voucherno = "";
            Random r = new Random();
            int genRand = r.Next(1, 100000);

            voucherno = Convert.ToString(myRandomNo);


            if (ViewState["CurrentTable"] != null)
            {
                dtCurrentTable = (DataTable)ViewState["CurrentTable"];
                DataRow dr = null;

                dr = dtCurrentTable.NewRow();
                //dr["SeriesNumber"] = TxtSNo.Text;
                ////dr[0] = TxtSNo.Text;
                //dr["Date"]= txtDate.Text;
                ////dr[1] = txtDate.Text;
                //dr[2] = LabBnameID.Text;
                //dr[3] = LabBname.Text;
                //dr[4] = LabChitID.Text;
                //dr[5] = LabChit.Text;
                //dr[6] = LabMemnameID.Text;
                //dr[7] = LabMemname.Text;
                //dr[8] = LabBanmelistID.Text;
                //dr[9] = LabBanmelist.Text;
                //if (txtchit.Text == "")
                //    txtchit.Text = "0.00";
                //dr[10] = txtchit.Text;
                //if (txtmisc.Text == "")
                //    txtmisc.Text = "0.00";
                //dr[11] = txtmisc.Text;
                //if (txtother.Text == "")
                //    txtother.Text = "0.00";
                //dr[12] = txtother.Text;
                //dr[13] = txttotalamount.Text;
                //dr[14] = TxtSNo.Text;
                dr["SeriesNumber"] = TxtSNo.Text;
                dr["Date"] = txtDate.Text;
                dr["ReceivedByID"] = LabBnameID.Text;
                dr["ReceivedBy"] = LabBname.Text;
                dr["ChitnoID"] = LabChitID.Text;
                dr["Chitno"] = LabChit.Text;
                dr["MemberNameID"] = LabMemnameID.Text;
                dr["MemberName"] = LabMemname.Text;
                dr["BranchNameID"] = LabBanmelistID.Text;
                dr["BranchName"] = LabBanmelist.Text;
                if (txtchit.Text == "")
                    txtchit.Text = "0.00";
                dr[10] = txtchit.Text;
                if (txtmisc.Text == "")
                    txtmisc.Text = "0.00";
                dr[11] = txtmisc.Text;
                if (txtother.Text == "")
                    txtother.Text = "0.00";
                dr[12] = txtother.Text;
                dr[13] = txttotalamount.Text;
                dr[14] = TxtSNo.Text;
                dtCurrentTable.Rows.Add(dr);
                string narration = LabChit.Text + ":" + LabMemname.Text;
                //int amount = Convert.ToInt32(txtmisc.Text) + Convert.ToInt32(txtother.Text);  //04/10/2021
                GridView2.DataSource = dtCurrentTable;
                GridView2.DataBind();
                ViewState["CurrentTable"] = dtCurrentTable;


            }

        }

        protected void bt1Generate_Onclick(object sender, EventArgs e)
        {
            string DualTransactionKey = "";

            System.Guid guid = Guid.NewGuid();
            string guidForChar36 = guid.ToString();
            string hexstring = BitConverter.ToString(guid.ToByteArray());
            string guidForBinary16 = "0x" + hexstring.Replace("-", string.Empty);
            DualTransactionKey = guidForBinary16;
            DateTime dtChoosenDate = DateTime.Parse(txtChequeDate.Text);
            DateTime dtChoosenDate1 = DateTime.Parse(txtDate.Text);
            string details = "details";
            string bankhead = "bankhead";
            //string voucherno = "";
            Random r = new Random();
            int genRand = r.Next(1, 100000);
            string narration1 = LabChit.Text + ":" + LabMemname.Text;

            // voucherno = Convert.ToString(genRand);
            //24/09/2021 - Bala
            //voucherno = Convert.ToString(myRandomNo);
            string rnQry = "select ifnull( max(Voucher_No)+1,0) from voucher where BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and Series='PORTAL' and Trans_Type=1";
            int voucherno = int.Parse(balayer.GetSingleValue(rnQry));
            string txtmiscAmt, txtOtherAmt;

            //


            if (GridView2.Rows.Count > 0)
            {
                try
                {

                    for (int i = 0; i < GridView2.Rows.Count; i++)

                    {
                        //04/10/2021 -Bala
                        string narration = "Recd From:- " + TxtSNo.Text + " " + GridView2.Rows[i].Cells[8].Text + ":" + GridView2.Rows[i].Cells[6].Text + ":" + " For Inst No:";
                        string cashNarration = "";
                        string tonarration = "";
                        string Due = "";
                        Int64 firstValue = 0;
                        Int64 secondValue = 0;
                        string maxDraw = "";

                        //Label tokenid = (Label)GridView2.Rows[i].FindControl("ChitnoID");
                        string tokenid = LabChitID.Text;
                        string branchId = LabBnameID.Text;
                        string memberId = LabMemnameID.Text;
                        string receivedbyId = LabBnameID.Text;

                        //var memtoken1 = tokenid.Text;
                        string intNarration = "PROFIT AND LOSS ACCOUNT>>Interest on Chit Debts Recd From" + GridView2.Rows[i].Cells[8].Text + ":" + GridView2.Rows[i].Cells[6].Text + ":" + TxtSNo.Text;

                        //
                        decimal amount = Convert.ToDecimal(GridView2.Rows[i].Cells[12].Text) + Convert.ToDecimal(GridView2.Rows[i].Cells[13].Text);
                        //string groupid = balayer.GetSingleValue("select GroupID from svcf.membertogroupmaster where Head_Id=" + GridView2.Rows[i].Cells[5].Text + "");
                        string groupid = balayer.GetSingleValue("select GroupID from svcf.membertogroupmaster where Head_Id=" + tokenid + "");
                        string Drawno = balayer.GetSingleValue("select max(DrawNO + 1) from svcf.auctiondetails where GroupID = " + groupid + " and IsPrized = 'Y'");
                        string Draw = "DrawNO :" + Drawno;
                        //04/10/2021 - Bala
                        try
                        {
                            #region Narration
                            //Narration
                            decimal TotalPaidAmount = decimal.Parse(balayer.GetSingleValue("SELECT ifnull( (sum(IF(Voucher_Type = 'C',Amount,0.00)) -sum(IF(Voucher_Type = 'D',Amount,0.00)) ),0.00) AS TransactionAmount FROM `svcf`.`voucher` where  Head_Id=" + tokenid + " and  (Trans_Type<>2) and Other_Trans_Type<>5"));
                            decimal AddTotalPaidAmount = TotalPaidAmount;
                            string FromNarration = "";
                            string ToNarration = "";
                            int FromDraw = 0;
                            int ToDraw = 0;
                            decimal excess = 0; //12/05/2021
                            Due = balayer.GetSingleValue("Select currentdueamount from auctiondetails where GroupID=" + groupid + " and DrawNO=1 and AuctionDate<='" + DateTime.Now.ToString("yyyy/MM/dd") + "'");   //12/05/2021
                            maxDraw = balayer.GetSingleValue("select max(DrawNO) from auctiondetails where GroupID=" + groupid);
                            //
                            if (TotalPaidAmount == 0.00M)
                            {
                                FromNarration = "1";
                                FromDraw = 1;
                                TotalPaidAmount = TotalPaidAmount + Convert.ToDecimal(txtchit.Text);
                                qry = "SELECT DrawNO,CurrentDueAmount FROM `svcf`.`auctiondetails` where GroupID=" + groupid + " and CurrentDueAmount<>'0.00' order by DrawNO";
                                DataTable dtAuction = balayer.GetDataTable(qry);
                                for (int iAuc = 0; iAuc < dtAuction.Rows.Count; iAuc++)
                                {
                                    decimal currentDueAmt = decimal.Parse(dtAuction.Rows[iAuc]["CurrentDueAmount"].ToString());
                                    TotalPaidAmount = TotalPaidAmount - currentDueAmt;
                                    decimal tempDueAmt = TotalPaidAmount;
                                    if (tempDueAmt == 0.00M)
                                    {
                                        ToNarration = (iAuc + 1).ToString();
                                        ToDraw = iAuc + 1;
                                        break;
                                    }
                                    else if (tempDueAmt < 0.00M)
                                    {
                                        ToNarration = (iAuc + 1) + "PP";
                                        ToDraw = iAuc + 1;
                                        break;
                                    }

                                    excess = tempDueAmt;    //12/05/2021
                                }

                                if (ToNarration == "")
                                {
                                    //12/05/2021//FromNarration += "To" + (int.Parse(dtAuction.Rows[dtAuction.Rows.Count - 1]["DrawNO"].ToString()) + 1).ToString() + "+ Excess Payment";
                                    decimal excessCount = excess / Convert.ToDecimal(Due);
                                    var values = excessCount.ToString(CultureInfo.InvariantCulture).Split('.');

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
                                        ToNarration = "-" + Convert.ToInt16(dtAuction.Rows[dtAuction.Rows.Count - 1]["DrawNo"]) + firstValue + "F";
                                    }
                                    else
                                    {
                                        ToNarration = "-" + Convert.ToInt16(dtAuction.Rows[dtAuction.Rows.Count - 1]["DrawNo"]) + firstValue + 1 + "P";
                                    }

                                    FromNarration += ToNarration;
                                }
                                if (FromDraw != ToDraw)
                                {
                                    FromNarration += " To " + ToNarration;
                                }

                                cashNarration = FromNarration;
                            }
                            else
                            {
                                DataTable dtAuction = balayer.GetDataTable("SELECT DrawNO,CurrentDueAmount FROM `svcf`.`auctiondetails` where GroupID=" + groupid + " and CurrentDueAmount<>'0.00' order by DrawNO");
                                TotalPaidAmount = AddTotalPaidAmount;
                                for (int iAuc = 0; iAuc < dtAuction.Rows.Count; iAuc++)
                                {
                                    decimal currentDueAmount = decimal.Parse(dtAuction.Rows[iAuc]["CurrentDueAmount"].ToString());
                                    TotalPaidAmount = TotalPaidAmount - currentDueAmount;
                                    decimal tempDueAmount = TotalPaidAmount;
                                    if (tempDueAmount == 0.00M)
                                    {
                                        FromNarration = (iAuc + 2).ToString() + "F";
                                        FromDraw = iAuc + 2;
                                        break;
                                    }
                                    else if (tempDueAmount < 0.00M)
                                    {
                                        FromNarration = iAuc + 1 + " PP";
                                        FromDraw = iAuc + 1;
                                        break;
                                    }
                                    else
                                    {
                                        excess = tempDueAmount;
                                    }
                                }
                                if (FromNarration == "")
                                {
                                    if (dtAuction.Rows.Count > 0)
                                    {
                                        //12/05/2021//FromNarration = (int.Parse(dtAuction.Rows[dtAuction.Rows.Count - 1]["DrawNO"].ToString()) + 1).ToString() + "+ Excess Payment";
                                        decimal excessCount = excess / Convert.ToDecimal(Due);
                                        var values = excessCount.ToString(CultureInfo.InvariantCulture).Split('.');

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
                                            if (Convert.ToInt16(dtAuction.Rows[dtAuction.Rows.Count - 1]["DrawNo"]) == Convert.ToInt16(maxDraw))
                                            {
                                                ToNarration = Convert.ToInt16(maxDraw) + " + Excess";
                                            }
                                            else
                                            {
                                                ToNarration = Convert.ToInt16(dtAuction.Rows[dtAuction.Rows.Count - 1]["DrawNo"]) + firstValue + "F";
                                            }
                                        }
                                        else
                                        {
                                            if (Convert.ToInt16(dtAuction.Rows[dtAuction.Rows.Count - 1]["DrawNo"]) + firstValue == Convert.ToInt16(maxDraw))
                                            {
                                                ToNarration = Convert.ToInt16(maxDraw) + " + Excess";
                                            }
                                            else
                                            {
                                                ToNarration = Convert.ToInt16(dtAuction.Rows[dtAuction.Rows.Count - 1]["DrawNo"]) + firstValue + 1 + "P";
                                            }
                                        }

                                        FromNarration = ToNarration;
                                        //
                                    }
                                }
                                else
                                {
                                    TotalPaidAmount = AddTotalPaidAmount;
                                    TotalPaidAmount = TotalPaidAmount + decimal.Parse(txtchit.Text);
                                    for (int iAuc = 0; iAuc < dtAuction.Rows.Count; iAuc++)
                                    {
                                        decimal currentDueAmount = decimal.Parse(dtAuction.Rows[iAuc]["CurrentDueAmount"].ToString());
                                        TotalPaidAmount = TotalPaidAmount - currentDueAmount;
                                        decimal tempDueAmount = TotalPaidAmount;
                                        if (tempDueAmount == 0.00M)
                                        {
                                            ToNarration = (iAuc + 1).ToString() + "F";
                                            ToDraw = iAuc + 1;
                                            break;
                                        }
                                        else if (tempDueAmount < 0.00M)
                                        {
                                            ToDraw = iAuc + 1;
                                            ToNarration = iAuc + 1 + " PP";
                                            break;
                                        }
                                        else
                                        {
                                            excess = tempDueAmount;
                                        }
                                    }
                                    if (ToNarration == "")
                                    {
                                        //12/05/2021
                                        //ToNarration = "+ Excess Payment";
                                        decimal excessCount = excess / Convert.ToDecimal(Due);
                                        var values = excessCount.ToString(CultureInfo.InvariantCulture).Split('.');

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
                                            ToNarration = Convert.ToInt16(dtAuction.Rows[dtAuction.Rows.Count - 1]["DrawNo"]) + firstValue + "F";
                                        }
                                        else
                                        {
                                            ToNarration = Convert.ToInt16(dtAuction.Rows[dtAuction.Rows.Count - 1]["DrawNo"]) + firstValue + 1 + "P";
                                        }
                                        //
                                    }
                                }
                                if (ToNarration != "")
                                {
                                    if (FromDraw != ToDraw)
                                    {
                                        //RowddlToken.ToolTip = FromNarration + " To " + ToNarration;
                                        //12/05/2021
                                        if (FromNarration.Contains("P"))
                                            FromNarration = FromNarration.Trim('P');
                                        else if (FromNarration.Contains("F"))
                                            FromNarration = FromNarration.Trim('F');
                                        //paidInstalment = instFrom + "F" + "-" + instTo;
                                        //
                                        tonarration = FromNarration + "F To " + ToNarration;
                                        cashNarration = tonarration;
                                    }
                                    else
                                    {
                                        //RowddlToken.ToolTip = ToNarration;
                                        tonarration = ToNarration;
                                        cashNarration = ToNarration;
                                    }
                                }
                                else
                                {
                                    //RowddlToken.ToolTip = FromNarration;
                                    tonarration = FromNarration;
                                    cashNarration = FromNarration;
                                }
                            }
                            #endregion
                        }
                        catch (Exception ex)
                        {
                            throw;
                        }


                        trn.insertorupdateTrn("INSERT INTO `svcf`.`portalentry`(`DualTransactionKey`,`CurrentBranchID`,`OtherBranchID`,`ReceivedbyID`,`ChitID`,`PaymentCasecheque`,`MemberID`,`ChitAmount`,`MiscAmount`,`OtherAmount`,`TotalAmount`,`ChequeNumber`,`BankDetails`,`ChequeDate`,`BankHead`,`SeriesNumber`,`SeriesReceipt`,`ChoosenDate`)VALUES('"
                            + DualTransactionKey + "', " + balayer.ToobjectstrEvenNull(Session["Branchid"]) + ", " + branchId + ", " + receivedbyId + ", " + tokenid + ", " + cash + ", " + memberId + ", " + GridView2.Rows[i].Cells[11].Text + ", " + GridView2.Rows[i].Cells[12].Text + ", " + GridView2.Rows[i].Cells[13].Text + ", " + GridView2.Rows[i].Cells[14].Text + ", " + 0000 + ",' " + details + "', " + 1111 / 11 / 11 + ", '" + bankhead + "','" + GridView2.Rows[i].Cells[1].Text + "','" + series + "', '" + dtChoosenDate1.ToString("yyyy-MM-dd") + "')");

                        if (LabBanmelistID.Text == balayer.ToobjectstrEvenNull(Session["Branchid"]))
                        {


                            //trn.insertorupdateTrn("INSERT INTO `svcf`.`voucher`(`DualTransactionKey`,`BranchID`,`CurrDate`,`Voucher_No`,`Voucher_Type`,`Head_Id`,`ChoosenDate`,`Narration`,`Amount`,`Series`,`ReceievedBy`,`Trans_Type`,`T_Day`,`T_Month`,`T_Year`,`MemberID`,`Trans_Medium`,`RootID`,`ChitGroupId`,`Other_Trans_Type`,`IsDeleted`,`IsAccepted`,`AppReceiptno`)VALUES(" + DualTransactionKey + "," + balayer.ToobjectstrEvenNull(Session["Branchid"]) + ",'" + DateTime.Now.ToString("yyyy-MM-dd") + "','" + voucherno + "','C'," + GridView2.Rows[i].Cells[5].Text + ", '" + dtChoosenDate1.ToString("yyyy-MM-dd") + "','" + narration+cashNarration + "'," + GridView2.Rows[i].Cells[11].Text + ",' PORTAL'," + GridView2.Rows[i].Cells[3].Text + "," + 1 + "," + dtChoosenDate1.Day + "," + dtChoosenDate1.Month + "," + dtChoosenDate1.Year + "," + GridView2.Rows[i].Cells[7].Text + "," + 0 + "," + 5 + "," + GridView2.Rows[i].Cells[5].Text + "," + 0 + "," + 0 + "," + 0 + ",'" + GridView2.Rows[i].Cells[1].Text + "')");
                            //trn.insertorupdateTrn("INSERT INTO `svcf`.`voucher`(`DualTransactionKey`,`BranchID`,`CurrDate`,`Voucher_No`,`Voucher_Type`,`Head_Id`,`ChoosenDate`,`Narration`,`Amount`,`Series`,`ReceievedBy`,`Trans_Type`,`T_Day`,`T_Month`,`T_Year`,`MemberID`,`Trans_Medium`,`RootID`,`ChitGroupId`,`Other_Trans_Type`,`IsDeleted`,`IsAccepted`,`AppReceiptno`)VALUES(" + DualTransactionKey + "," + balayer.ToobjectstrEvenNull(Session["Branchid"]) + ",'" + DateTime.Now.ToString("yyyy-MM-dd") + "','" + voucherno + "','D'," + 12 + ",'" + dtChoosenDate1.ToString("yyyy-MM-dd") + "','" + narration+cashNarration + "'," + GridView2.Rows[i].Cells[11].Text + ",'PORTAL'," + GridView2.Rows[i].Cells[3].Text + "," + 1 + "," + dtChoosenDate1.Day + "," + dtChoosenDate1.Month + "," + dtChoosenDate1.Year + "," + GridView2.Rows[i].Cells[7].Text + "," + 0 + "," + 12 + "," + GridView2.Rows[i].Cells[5].Text + "," + 0 + "," + 0 + "," + 0 + ",'" + GridView2.Rows[i].Cells[1].Text + "')");
                            trn.insertorupdateTrn("INSERT INTO `svcf`.`voucher`(`DualTransactionKey`,`BranchID`,`CurrDate`,`Voucher_No`,`Voucher_Type`,`Head_Id`,`ChoosenDate`,`Narration`,`Amount`,`Series`,`ReceievedBy`,`Trans_Type`,`T_Day`,`T_Month`,`T_Year`,`MemberID`,`Trans_Medium`,`RootID`,`ChitGroupId`,`Other_Trans_Type`,`IsDeleted`,`IsAccepted`,`AppReceiptno`)VALUES(" + DualTransactionKey + "," + balayer.ToobjectstrEvenNull(Session["Branchid"]) + ",'" + DateTime.Now.ToString("yyyy-MM-dd") + "','" + voucherno + "','C'," + tokenid + ", '" + dtChoosenDate1.ToString("yyyy-MM-dd") + "','" + narration + cashNarration + "'," + GridView2.Rows[i].Cells[11].Text + ",'PORTAL'," + receivedbyId + "," + 1 + "," + dtChoosenDate1.Day + "," + dtChoosenDate1.Month + "," + dtChoosenDate1.Year + "," + memberId + "," + 0 + "," + 5 + "," + tokenid + "," + 0 + "," + 0 + "," + 0 + ",'" + GridView2.Rows[i].Cells[1].Text + "')");
                            trn.insertorupdateTrn("INSERT INTO `svcf`.`voucher`(`DualTransactionKey`,`BranchID`,`CurrDate`,`Voucher_No`,`Voucher_Type`,`Head_Id`,`ChoosenDate`,`Narration`,`Amount`,`Series`,`ReceievedBy`,`Trans_Type`,`T_Day`,`T_Month`,`T_Year`,`MemberID`,`Trans_Medium`,`RootID`,`ChitGroupId`,`Other_Trans_Type`,`IsDeleted`,`IsAccepted`,`AppReceiptno`)VALUES(" + DualTransactionKey + "," + balayer.ToobjectstrEvenNull(Session["Branchid"]) + ",'" + DateTime.Now.ToString("yyyy-MM-dd") + "','" + voucherno + "','D'," + 12 + ",'" + dtChoosenDate1.ToString("yyyy-MM-dd") + "','" + narration + cashNarration + "'," + GridView2.Rows[i].Cells[11].Text + ",'PORTAL'," + receivedbyId + "," + 1 + "," + dtChoosenDate1.Day + "," + dtChoosenDate1.Month + "," + dtChoosenDate1.Year + "," + memberId + "," + 0 + "," + 12 + "," + tokenid + "," + 0 + "," + 0 + "," + 0 + ",'" + GridView2.Rows[i].Cells[1].Text + "')");


                            //04/10/2021 -Bala
                            txtmiscAmt = GridView2.Rows[i].Cells[12].Text;
                            txtOtherAmt = GridView2.Rows[i].Cells[13].Text;
                            if (decimal.Parse(txtmiscAmt) != 0.00M || decimal.Parse(txtOtherAmt) != 0.00M)
                            {
                                //trn.insertorupdateTrn("INSERT INTO `svcf`.`voucher`(`DualTransactionKey`,`BranchID`,`CurrDate`,`Voucher_No`,`Voucher_Type`,`Head_Id`,`ChoosenDate`,`Narration`,`Amount`,`Series`,`ReceievedBy`,`Trans_Type`,`T_Day`,`T_Month`,`T_Year`,`MemberID`,`Trans_Medium`,`RootID`,`ChitGroupId`,`Other_Trans_Type`,`IsDeleted`,`IsAccepted`,`AppReceiptno`)VALUES(" + DualTransactionKey + "," + balayer.ToobjectstrEvenNull(Session["Branchid"]) + ",'" + DateTime.Now.ToString("yyyy-MM-dd") + "','" + voucherno + "','C'," + GridView2.Rows[i].Cells[5].Text + ",'" + dtChoosenDate1.ToString("yyyy-MM-dd") + "','" + intNarration + "'," + amount + ",'PORTAL'," + GridView2.Rows[i].Cells[3].Text + "," + 1 + "," + dtChoosenDate1.Day + "," + dtChoosenDate1.Month + "," + dtChoosenDate1.Year + "," + GridView2.Rows[i].Cells[7].Text + "," + 0 + "," + 11 + "," + GridView2.Rows[i].Cells[5].Text + "," + 0 + "," + 0 + "," + 0 + ",'" + GridView2.Rows[i].Cells[1].Text + "')");
                                //trn.insertorupdateTrn("INSERT INTO `svcf`.`voucher`(`DualTransactionKey`,`BranchID`,`CurrDate`,`Voucher_No`,`Voucher_Type`,`Head_Id`,`ChoosenDate`,`Narration`,`Amount`,`Series`,`ReceievedBy`,`Trans_Type`,`T_Day`,`T_Month`,`T_Year`,`MemberID`,`Trans_Medium`,`RootID`,`ChitGroupId`,`Other_Trans_Type`,`IsDeleted`,`IsAccepted`,`AppReceiptno`)VALUES(" + DualTransactionKey + "," + balayer.ToobjectstrEvenNull(Session["Branchid"]) + ",'" + DateTime.Now.ToString("yyyy-MM-dd") + "','" + voucherno + "','D'," + 12 + ",'" + dtChoosenDate1.ToString("yyyy-MM-dd") + "','" + intNarration + "'," + amount + ",'PORTAL'," + GridView2.Rows[i].Cells[3].Text + "," + 1 + "," + dtChoosenDate1.Day + "," + dtChoosenDate1.Month + "," + dtChoosenDate1.Year + "," + GridView2.Rows[i].Cells[7].Text + "," + 0 + "," + 12 + "," + GridView2.Rows[i].Cells[5].Text + "," + 0 + "," + 0 + "," + 0 + ",'" + GridView2.Rows[i].Cells[1].Text + "')");
                                trn.insertorupdateTrn("INSERT INTO `svcf`.`voucher`(`DualTransactionKey`,`BranchID`,`CurrDate`,`Voucher_No`,`Voucher_Type`,`Head_Id`,`ChoosenDate`,`Narration`,`Amount`,`Series`,`ReceievedBy`,`Trans_Type`,`T_Day`,`T_Month`,`T_Year`,`MemberID`,`Trans_Medium`,`RootID`,`ChitGroupId`,`Other_Trans_Type`,`IsDeleted`,`IsAccepted`,`AppReceiptno`)VALUES(" + DualTransactionKey + "," + balayer.ToobjectstrEvenNull(Session["Branchid"]) + ",'" + DateTime.Now.ToString("yyyy-MM-dd") + "','" + voucherno + "','C'," + tokenid + ",'" + dtChoosenDate1.ToString("yyyy-MM-dd") + "','" + intNarration + "'," + amount + ",'PORTAL'," + receivedbyId + "," + 1 + "," + dtChoosenDate1.Day + "," + dtChoosenDate1.Month + "," + dtChoosenDate1.Year + "," + memberId + "," + 0 + "," + 11 + "," + tokenid + "," + 0 + "," + 0 + "," + 0 + ",'" + GridView2.Rows[i].Cells[1].Text + "')");
                                trn.insertorupdateTrn("INSERT INTO `svcf`.`voucher`(`DualTransactionKey`,`BranchID`,`CurrDate`,`Voucher_No`,`Voucher_Type`,`Head_Id`,`ChoosenDate`,`Narration`,`Amount`,`Series`,`ReceievedBy`,`Trans_Type`,`T_Day`,`T_Month`,`T_Year`,`MemberID`,`Trans_Medium`,`RootID`,`ChitGroupId`,`Other_Trans_Type`,`IsDeleted`,`IsAccepted`,`AppReceiptno`)VALUES(" + DualTransactionKey + "," + balayer.ToobjectstrEvenNull(Session["Branchid"]) + ",'" + DateTime.Now.ToString("yyyy-MM-dd") + "','" + voucherno + "','D'," + 12 + ",'" + dtChoosenDate1.ToString("yyyy-MM-dd") + "','" + intNarration + "'," + amount + ",'PORTAL'," + receivedbyId + "," + 1 + "," + dtChoosenDate1.Day + "," + dtChoosenDate1.Month + "," + dtChoosenDate1.Year + "," + memberId + "," + 0 + "," + 12 + "," + tokenid + "," + 0 + "," + 0 + "," + 0 + ",'" + GridView2.Rows[i].Cells[1].Text + "')");
                            }
                        }
                        else
                        {

                            //trn.insertorupdateTrn("INSERT INTO `svcf`.`voucher`(`DualTransactionKey`,`BranchID`,`CurrDate`,`Voucher_No`,`Voucher_Type`,`Head_Id`,`ChoosenDate`,`Narration`,`Amount`,`Series`,`ReceievedBy`,`Trans_Type`,`T_Day`,`T_Month`,`T_Year`,`MemberID`,`Trans_Medium`,`RootID`,`ChitGroupId`,`Other_Trans_Type`,`IsDeleted`,`IsAccepted`,`AppReceiptno`)VALUES(" + DualTransactionKey + "," + balayer.ToobjectstrEvenNull(Session["Branchid"]) + ",'" + DateTime.Now.ToString("yyyy-MM-dd") + "','" + voucherno + "','C'," + GridView2.Rows[i].Cells[9].Text + ", '" + dtChoosenDate1.ToString("yyyy-MM-dd") + "','" +  narration+cashNarration + "'," + GridView2.Rows[i].Cells[11].Text + ",' PORTAL'," + GridView2.Rows[i].Cells[3].Text + "," + 1 + "," + dtChoosenDate1.Day + "," + dtChoosenDate1.Month + "," + dtChoosenDate1.Year + "," + GridView2.Rows[i].Cells[7].Text + "," + 0 + "," + 5 + "," + GridView2.Rows[i].Cells[5].Text + "," + 1 + "," + 0 + "," + 0 + ",'" + GridView2.Rows[i].Cells[1].Text + "')");
                            //trn.insertorupdateTrn("INSERT INTO `svcf`.`voucher`(`DualTransactionKey`,`BranchID`,`CurrDate`,`Voucher_No`,`Voucher_Type`,`Head_Id`,`ChoosenDate`,`Narration`,`Amount`,`Series`,`ReceievedBy`,`Trans_Type`,`T_Day`,`T_Month`,`T_Year`,`MemberID`,`Trans_Medium`,`RootID`,`ChitGroupId`,`Other_Trans_Type`,`IsDeleted`,`IsAccepted`,`AppReceiptno`)VALUES(" + DualTransactionKey + "," + balayer.ToobjectstrEvenNull(Session["Branchid"]) + ",'" + DateTime.Now.ToString("yyyy-MM-dd") + "'," + voucherno + ",'D'," + 12 + ",'" + dtChoosenDate1.ToString("yyyy-MM-dd") + "','"  + narration+cashNarration + "'," + GridView2.Rows[i].Cells[11].Text + ",'PORTAL'," + GridView2.Rows[i].Cells[3].Text + "," + 1 + "," + dtChoosenDate1.Day + "," + dtChoosenDate1.Month + "," + dtChoosenDate1.Year + "," + GridView2.Rows[i].Cells[7].Text + "," + 0 + "," + 12 + "," + GridView2.Rows[i].Cells[5].Text + "," + 1 + "," + 0 + "," + 0 + ",'" + GridView2.Rows[i].Cells[1].Text + "')");
                            trn.insertorupdateTrn("INSERT INTO `svcf`.`voucher`(`DualTransactionKey`,`BranchID`,`CurrDate`,`Voucher_No`,`Voucher_Type`,`Head_Id`,`ChoosenDate`,`Narration`,`Amount`,`Series`,`ReceievedBy`,`Trans_Type`,`T_Day`,`T_Month`,`T_Year`,`MemberID`,`Trans_Medium`,`RootID`,`ChitGroupId`,`Other_Trans_Type`,`IsDeleted`,`IsAccepted`,`AppReceiptno`)VALUES(" + DualTransactionKey + "," + balayer.ToobjectstrEvenNull(Session["Branchid"]) + ",'" + DateTime.Now.ToString("yyyy-MM-dd") + "','" + voucherno + "','C'," + GridView2.Rows[i].Cells[9].Text + ", '" + dtChoosenDate1.ToString("yyyy-MM-dd") + "','" + narration + cashNarration + "'," + GridView2.Rows[i].Cells[11].Text + ",'PORTAL'," + receivedbyId + "," + 1 + "," + dtChoosenDate1.Day + "," + dtChoosenDate1.Month + "," + dtChoosenDate1.Year + "," + memberId + "," + 0 + "," + 5 + "," + tokenid + "," + 1 + "," + 0 + "," + 0 + ",'" + GridView2.Rows[i].Cells[1].Text + "')");
                            trn.insertorupdateTrn("INSERT INTO `svcf`.`voucher`(`DualTransactionKey`,`BranchID`,`CurrDate`,`Voucher_No`,`Voucher_Type`,`Head_Id`,`ChoosenDate`,`Narration`,`Amount`,`Series`,`ReceievedBy`,`Trans_Type`,`T_Day`,`T_Month`,`T_Year`,`MemberID`,`Trans_Medium`,`RootID`,`ChitGroupId`,`Other_Trans_Type`,`IsDeleted`,`IsAccepted`,`AppReceiptno`)VALUES(" + DualTransactionKey + "," + balayer.ToobjectstrEvenNull(Session["Branchid"]) + ",'" + DateTime.Now.ToString("yyyy-MM-dd") + "'," + voucherno + ",'D'," + 12 + ",'" + dtChoosenDate1.ToString("yyyy-MM-dd") + "','" + narration + cashNarration + "'," + GridView2.Rows[i].Cells[11].Text + ",'PORTAL'," + receivedbyId + "," + 1 + "," + dtChoosenDate1.Day + "," + dtChoosenDate1.Month + "," + dtChoosenDate1.Year + "," + memberId + "," + 0 + "," + 12 + "," + tokenid + "," + 1 + "," + 0 + "," + 0 + ",'" + GridView2.Rows[i].Cells[1].Text + "')");
                            //04/10/2021 -Bala
                            txtmiscAmt = GridView2.Rows[i].Cells[12].Text;
                            txtOtherAmt = GridView2.Rows[i].Cells[13].Text;
                            if (decimal.Parse(txtmiscAmt) != 0.00M || decimal.Parse(txtOtherAmt) != 0.00M)
                            {
                                //trn.insertorupdateTrn("INSERT INTO `svcf`.`voucher`(`DualTransactionKey`,`BranchID`,`CurrDate`,`Voucher_No`,`Voucher_Type`,`Head_Id`,`ChoosenDate`,`Narration`,`Amount`,`Series`,`ReceievedBy`,`Trans_Type`,`T_Day`,`T_Month`,`T_Year`,`MemberID`,`Trans_Medium`,`RootID`,`ChitGroupId`,`Other_Trans_Type`,`IsDeleted`,`IsAccepted`,`AppReceiptno`)VALUES(" + DualTransactionKey + "," + balayer.ToobjectstrEvenNull(Session["Branchid"]) + ",'" + DateTime.Now.ToString("yyyy-MM-dd") + "','" + voucherno + "','C'," + GridView2.Rows[i].Cells[9].Text + ",'" + dtChoosenDate1.ToString("yyyy-MM-dd") + "','" + "Misc :-"+intNarration + "'," + amount + ",'PORTAL'," + GridView2.Rows[i].Cells[3].Text + "," + 1 + "," + dtChoosenDate1.Day + "," + dtChoosenDate1.Month + "," + dtChoosenDate1.Year + "," + GridView2.Rows[i].Cells[7].Text + "," + 0 + "," + 11 + "," + GridView2.Rows[i].Cells[5].Text + "," + 1 + "," + 0 + "," + 0 + ",'" + GridView2.Rows[i].Cells[1].Text + "')");
                                //trn.insertorupdateTrn("INSERT INTO `svcf`.`voucher`(`DualTransactionKey`,`BranchID`,`CurrDate`,`Voucher_No`,`Voucher_Type`,`Head_Id`,`ChoosenDate`,`Narration`,`Amount`,`Series`,`ReceievedBy`,`Trans_Type`,`T_Day`,`T_Month`,`T_Year`,`MemberID`,`Trans_Medium`,`RootID`,`ChitGroupId`,`Other_Trans_Type`,`IsDeleted`,`IsAccepted`,`AppReceiptno`)VALUES(" + DualTransactionKey + "," + balayer.ToobjectstrEvenNull(Session["Branchid"]) + ",'" + DateTime.Now.ToString("yyyy-MM-dd") + "','" + voucherno + "','D'," + 12 + ",'" + dtChoosenDate1.ToString("yyyy-MM-dd") + "','" + "Misc :-"+intNarration + "'," + amount + ",'PORTAL'," + GridView2.Rows[i].Cells[3].Text + "," + 1 + "," + dtChoosenDate1.Day + "," + dtChoosenDate1.Month + "," + dtChoosenDate1.Year + "," + GridView2.Rows[i].Cells[7].Text + "," + 0 + "," + 12 + "," + GridView2.Rows[i].Cells[5].Text + "," + 1 + "," + 0 + "," + 0 + ",'" + GridView2.Rows[i].Cells[1].Text + "')");
                                trn.insertorupdateTrn("INSERT INTO `svcf`.`voucher`(`DualTransactionKey`,`BranchID`,`CurrDate`,`Voucher_No`,`Voucher_Type`,`Head_Id`,`ChoosenDate`,`Narration`,`Amount`,`Series`,`ReceievedBy`,`Trans_Type`,`T_Day`,`T_Month`,`T_Year`,`MemberID`,`Trans_Medium`,`RootID`,`ChitGroupId`,`Other_Trans_Type`,`IsDeleted`,`IsAccepted`,`AppReceiptno`)VALUES(" + DualTransactionKey + "," + balayer.ToobjectstrEvenNull(Session["Branchid"]) + ",'" + DateTime.Now.ToString("yyyy-MM-dd") + "','" + voucherno + "','C'," + GridView2.Rows[i].Cells[9].Text + ",'" + dtChoosenDate1.ToString("yyyy-MM-dd") + "','" + "Misc :-" + intNarration + "'," + amount + ",'PORTAL'," + receivedbyId + "," + 1 + "," + dtChoosenDate1.Day + "," + dtChoosenDate1.Month + "," + dtChoosenDate1.Year + "," + memberId + "," + 0 + "," + 11 + "," + tokenid + "," + 1 + "," + 0 + "," + 0 + ",'" + GridView2.Rows[i].Cells[1].Text + "')");
                                trn.insertorupdateTrn("INSERT INTO `svcf`.`voucher`(`DualTransactionKey`,`BranchID`,`CurrDate`,`Voucher_No`,`Voucher_Type`,`Head_Id`,`ChoosenDate`,`Narration`,`Amount`,`Series`,`ReceievedBy`,`Trans_Type`,`T_Day`,`T_Month`,`T_Year`,`MemberID`,`Trans_Medium`,`RootID`,`ChitGroupId`,`Other_Trans_Type`,`IsDeleted`,`IsAccepted`,`AppReceiptno`)VALUES(" + DualTransactionKey + "," + balayer.ToobjectstrEvenNull(Session["Branchid"]) + ",'" + DateTime.Now.ToString("yyyy-MM-dd") + "','" + voucherno + "','D'," + 12 + ",'" + dtChoosenDate1.ToString("yyyy-MM-dd") + "','" + "Misc :-" + intNarration + "'," + amount + ",'PORTAL'," + receivedbyId + "," + 1 + "," + dtChoosenDate1.Day + "," + dtChoosenDate1.Month + "," + dtChoosenDate1.Year + "," + memberId + "," + 0 + "," + 12 + "," + tokenid + "," + 1 + "," + 0 + "," + 0 + ",'" + GridView2.Rows[i].Cells[1].Text + "')");
                            }

                        }
                    }
                }
                catch (Exception error)
                {
                    try
                    {
                        trn.RollbackTrn();
                    }
                    catch
                    { }
                    finally
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "Warning", "alert('" + error.Message.ToString().Replace("'", "\\'") + "');", true);
                    }
                }
                finally
                {
                    trn.DisposeTrn();
                }
            }
            if (Gridtrr.Rows.Count > 0)
            {
                for (int i = 0; i < Gridtrr.Rows.Count; i++)
                {
                    try
                    {
                        //04/10/2021 -Bala
                        string cashNarration = "";
                        string tonarration = "";
                        string Due = "";
                        Int64 firstValue = 0;
                        Int64 secondValue = 0;
                        string maxDraw = "";

                        string tokenid = LabChitID.Text;
                        string branchId = LabBnameID.Text;
                        string memberId = LabMemnameID.Text;
                        string receivedbyId = LabBnameID.Text;

                        string intNarration = "PROFIT AND LOSS ACCOUNT>>Interest on Chit Debts Recd From" + Gridtrr.Rows[i].Cells[8].Text + ":" + receivedbyId + ":" + TxtSNo.Text;
                        //
                        decimal amount1 = Convert.ToDecimal(Gridtrr.Rows[i].Cells[12].Text) + Convert.ToDecimal(Gridtrr.Rows[i].Cells[13].Text);
                        string groupid1 = balayer.GetSingleValue("select GroupID from svcf.membertogroupmaster where Head_Id=" + tokenid + "");
                        string Drawno1 = balayer.GetSingleValue("select max(DrawNO + 1) from svcf.auctiondetails where GroupID = " + groupid1 + " and IsPrized = 'Y'");
                        string Draw1 = "DrawNO :" + Drawno1;

                        //04/10/2021 - Bala
                        try
                        {
                            #region Narration
                            //Narration
                            decimal TotalPaidAmount = decimal.Parse(balayer.GetSingleValue("SELECT ifnull( (sum(IF(Voucher_Type = 'C',Amount,0.00)) -sum(IF(Voucher_Type = 'D',Amount,0.00)) ),0.00) AS TransactionAmount FROM `svcf`.`voucher` where  Head_Id=" + tokenid + " and  (Trans_Type<>2) and Other_Trans_Type<>5"));
                            decimal AddTotalPaidAmount = TotalPaidAmount;
                            string FromNarration = "";
                            string ToNarration = "";
                            int FromDraw = 0;
                            int ToDraw = 0;
                            decimal excess = 0; //12/05/2021
                            Due = balayer.GetSingleValue("Select currentdueamount from auctiondetails where GroupID=" + groupid1 + " and DrawNO=1 and AuctionDate<='" + DateTime.Now.ToString("yyyy/MM/dd") + "'");
                            maxDraw = balayer.GetSingleValue("select max(DrawNO) from auctiondetails where GroupID=" + groupid1);
                            //
                            if (TotalPaidAmount == 0.00M)
                            {
                                FromNarration = "1";
                                FromDraw = 1;
                                TotalPaidAmount = TotalPaidAmount + Convert.ToDecimal(txtchit.Text);
                                qry = "SELECT DrawNO,CurrentDueAmount FROM `svcf`.`auctiondetails` where GroupID=" + groupid1 + " and CurrentDueAmount<>'0.00' order by DrawNO";
                                DataTable dtAuction = balayer.GetDataTable(qry);
                                for (int iAuc = 0; iAuc < dtAuction.Rows.Count; iAuc++)
                                {
                                    decimal currentDueAmt = decimal.Parse(dtAuction.Rows[iAuc]["CurrentDueAmount"].ToString());
                                    TotalPaidAmount = TotalPaidAmount - currentDueAmt;
                                    decimal tempDueAmt = TotalPaidAmount;
                                    if (tempDueAmt == 0.00M)
                                    {
                                        ToNarration = (iAuc + 1).ToString();
                                        ToDraw = iAuc + 1;
                                        break;
                                    }
                                    else if (tempDueAmt < 0.00M)
                                    {
                                        ToNarration = (iAuc + 1) + "PP";
                                        ToDraw = iAuc + 1;
                                        break;
                                    }

                                    excess = tempDueAmt;    //12/05/2021
                                }

                                if (ToNarration == "")
                                {
                                    //12/05/2021//FromNarration += "To" + (int.Parse(dtAuction.Rows[dtAuction.Rows.Count - 1]["DrawNO"].ToString()) + 1).ToString() + "+ Excess Payment";
                                    decimal excessCount = excess / Convert.ToDecimal(Due);
                                    var values = excessCount.ToString(CultureInfo.InvariantCulture).Split('.');

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
                                        ToNarration = "-" + Convert.ToInt16(dtAuction.Rows[dtAuction.Rows.Count - 1]["DrawNo"]) + firstValue + "F";
                                    }
                                    else
                                    {
                                        ToNarration = "-" + Convert.ToInt16(dtAuction.Rows[dtAuction.Rows.Count - 1]["DrawNo"]) + firstValue + 1 + "P";
                                    }

                                    FromNarration += ToNarration;
                                }
                                if (FromDraw != ToDraw)
                                {
                                    FromNarration += " To " + ToNarration;
                                }

                                cashNarration = FromNarration;
                            }
                            else
                            {
                                DataTable dtAuction = balayer.GetDataTable("SELECT DrawNO,CurrentDueAmount FROM `svcf`.`auctiondetails` where GroupID=" + groupid1 + " and CurrentDueAmount<>'0.00' order by DrawNO");
                                TotalPaidAmount = AddTotalPaidAmount;
                                for (int iAuc = 0; iAuc < dtAuction.Rows.Count; iAuc++)
                                {
                                    decimal currentDueAmount = decimal.Parse(dtAuction.Rows[iAuc]["CurrentDueAmount"].ToString());
                                    TotalPaidAmount = TotalPaidAmount - currentDueAmount;
                                    decimal tempDueAmount = TotalPaidAmount;
                                    if (tempDueAmount == 0.00M)
                                    {
                                        FromNarration = (iAuc + 2).ToString() + "F";
                                        FromDraw = iAuc + 2;
                                        break;
                                    }
                                    else if (tempDueAmount < 0.00M)
                                    {
                                        FromNarration = iAuc + 1 + " PP";
                                        FromDraw = iAuc + 1;
                                        break;
                                    }
                                    else
                                    {
                                        excess = tempDueAmount;
                                    }
                                }
                                if (FromNarration == "")
                                {
                                    if (dtAuction.Rows.Count > 0)
                                    {
                                        //12/05/2021//FromNarration = (int.Parse(dtAuction.Rows[dtAuction.Rows.Count - 1]["DrawNO"].ToString()) + 1).ToString() + "+ Excess Payment";
                                        decimal excessCount = excess / Convert.ToDecimal(Due);
                                        var values = excessCount.ToString(CultureInfo.InvariantCulture).Split('.');

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
                                            if (Convert.ToInt16(dtAuction.Rows[dtAuction.Rows.Count - 1]["DrawNo"]) == Convert.ToInt16(maxDraw))
                                            {
                                                ToNarration = Convert.ToInt16(maxDraw) + " + Excess";
                                            }
                                            else
                                            {
                                                ToNarration = Convert.ToInt16(dtAuction.Rows[dtAuction.Rows.Count - 1]["DrawNo"]) + firstValue + "F";
                                            }
                                        }
                                        else
                                        {
                                            if (Convert.ToInt16(dtAuction.Rows[dtAuction.Rows.Count - 1]["DrawNo"]) + firstValue == Convert.ToInt16(maxDraw))
                                            {
                                                ToNarration = Convert.ToInt16(maxDraw) + " + Excess";
                                            }
                                            else
                                            {
                                                ToNarration = Convert.ToInt16(dtAuction.Rows[dtAuction.Rows.Count - 1]["DrawNo"]) + firstValue + 1 + "P";
                                            }
                                        }

                                        FromNarration = ToNarration;
                                        //
                                    }
                                }
                                else
                                {
                                    TotalPaidAmount = AddTotalPaidAmount;
                                    TotalPaidAmount = TotalPaidAmount + decimal.Parse(txtchit.Text);
                                    for (int iAuc = 0; iAuc < dtAuction.Rows.Count; iAuc++)
                                    {
                                        decimal currentDueAmount = decimal.Parse(dtAuction.Rows[iAuc]["CurrentDueAmount"].ToString());
                                        TotalPaidAmount = TotalPaidAmount - currentDueAmount;
                                        decimal tempDueAmount = TotalPaidAmount;
                                        if (tempDueAmount == 0.00M)
                                        {
                                            ToNarration = (iAuc + 1).ToString() + "F";
                                            ToDraw = iAuc + 1;
                                            break;
                                        }
                                        else if (tempDueAmount < 0.00M)
                                        {
                                            ToDraw = iAuc + 1;
                                            ToNarration = iAuc + 1 + " PP";
                                            break;
                                        }
                                        else
                                        {
                                            excess = tempDueAmount;
                                        }
                                    }
                                    if (ToNarration == "")
                                    {
                                        //12/05/2021
                                        //ToNarration = "+ Excess Payment";
                                        decimal excessCount = excess / Convert.ToDecimal(Due);
                                        var values = excessCount.ToString(CultureInfo.InvariantCulture).Split('.');

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
                                            ToNarration = Convert.ToInt16(dtAuction.Rows[dtAuction.Rows.Count - 1]["DrawNo"]) + firstValue + "F";
                                        }
                                        else
                                        {
                                            ToNarration = Convert.ToInt16(dtAuction.Rows[dtAuction.Rows.Count - 1]["DrawNo"]) + firstValue + 1 + "P";
                                        }
                                        //
                                    }
                                }
                                if (ToNarration != "")
                                {
                                    if (FromDraw != ToDraw)
                                    {
                                        //RowddlToken.ToolTip = FromNarration + " To " + ToNarration;
                                        //12/05/2021
                                        if (FromNarration.Contains("P"))
                                            FromNarration = FromNarration.Trim('P');
                                        else if (FromNarration.Contains("F"))
                                            FromNarration = FromNarration.Trim('F');
                                        //paidInstalment = instFrom + "F" + "-" + instTo;
                                        //
                                        tonarration = FromNarration + "F To " + ToNarration;
                                        cashNarration = tonarration;
                                    }
                                    else
                                    {
                                        //RowddlToken.ToolTip = ToNarration;
                                        tonarration = ToNarration;
                                        cashNarration = ToNarration;
                                    }
                                }
                                else
                                {
                                    //RowddlToken.ToolTip = FromNarration;
                                    tonarration = FromNarration;
                                    cashNarration = FromNarration;
                                }
                            }
                            #endregion
                        }
                        catch (Exception ex)
                        {
                            throw;
                        }

                        //DateTime dtChoosenDate = DateTime.Parse(Gridtrr.Rows[i].Cells[18].Text);
                        string bankdetailsId = LabBankDetailsID.Text;
                        string bankheadId = LabBankHeadID.Text;

                        trn.insertorupdateTrn("INSERT INTO `svcf`.`portalentry`(`DualTransactionKey`,`CurrentBranchID`,`OtherBranchID`,`ReceivedbyID`,`ChitID`,`PaymentCasecheque`,`MemberID`,`ChitAmount`,`MiscAmount`,`OtherAmount`,`TotalAmount`,`ChequeNumber`,`BankDetails`,`ChequeDate`,`BankHead`,`SeriesNumber`,`SeriesReceipt`,`ChoosenDate`)VALUES('" + DualTransactionKey + "', " + balayer.ToobjectstrEvenNull(Session["Branchid"]) + ", " + branchId + ", " + receivedbyId + ", " + tokenid + ", " + cheque + ", " + memberId + ", " + Gridtrr.Rows[i].Cells[11].Text + ", " + Gridtrr.Rows[i].Cells[12].Text + ", " + Gridtrr.Rows[i].Cells[13].Text + ", " + Gridtrr.Rows[i].Cells[14].Text + ", " + Gridtrr.Rows[i].Cells[15].Text + ", " + bankdetailsId + ", '" + dtChoosenDate.ToString("yyyy-MM-dd") + "', " + bankheadId + ",'" + Gridtrr.Rows[i].Cells[1].Text + "','" + series + "', '" + dtChoosenDate1.ToString("yyyy-MM-dd") + "')");

                        if (LabBanmelistID.Text == balayer.ToobjectstrEvenNull(Session["Branchid"]))
                        {
                            trn.insertorupdateTrn("INSERT INTO `svcf`.`voucher`(`DualTransactionKey`,`BranchID`,`CurrDate`,`Voucher_No`,`Voucher_Type`,`Head_Id`,`ChoosenDate`,`Narration`,`Amount`,`Series`,`ReceievedBy`,`Trans_Type`,`T_Day`,`T_Month`,`T_Year`,`MemberID`,`Trans_Medium`,`RootID`,`ChitGroupId`,`Other_Trans_Type`,`IsDeleted`,`IsAccepted`,`AppReceiptno`)VALUES(" + DualTransactionKey + "," + balayer.ToobjectstrEvenNull(Session["Branchid"]) + ",'" + DateTime.Now.ToString("yyyy-MM-dd") + "','" + voucherno + "','C'," + tokenid + ",'" + dtChoosenDate1.ToString("yyyy-MM-dd") + "','" + "Recd From:" + Gridtrr.Rows[i].Cells[8].Text + ":" + Gridtrr.Rows[i].Cells[3].Text + ":" + Gridtrr.Rows[i].Cells[0].Text + " For DrawNo:" + cashNarration + "'," + Gridtrr.Rows[i].Cells[11].Text + ",'PORTAL'," + Gridtrr.Rows[i].Cells[3].Text + "," + 1 + "," + dtChoosenDate1.Day + "," + dtChoosenDate1.Month + "," + dtChoosenDate1.Year + "," + Gridtrr.Rows[i].Cells[7].Text + "," + 1 + "," + 5 + "," + tokenid + "," + 0 + "," + 0 + "," + 0 + ",'" + Gridtrr.Rows[i].Cells[1].Text + "')");
                            trn.insertorupdateTrn("INSERT INTO `svcf`.`voucher`(`DualTransactionKey`,`BranchID`,`CurrDate`,`Voucher_No`,`Voucher_Type`,`Head_Id`,`ChoosenDate`,`Narration`,`Amount`,`Series`,`ReceievedBy`,`Trans_Type`,`T_Day`,`T_Month`,`T_Year`,`MemberID`,`Trans_Medium`,`RootID`,`ChitGroupId`,`Other_Trans_Type`,`IsDeleted`,`IsAccepted`,`AppReceiptno`)VALUES(" + DualTransactionKey + "," + balayer.ToobjectstrEvenNull(Session["Branchid"]) + ",'" + DateTime.Now.ToString("yyyy-MM-dd") + "','" + voucherno + "','D'," + bankheadId + ",'" + dtChoosenDate1.ToString("yyyy-MM-dd") + "','" + "Recd From:" + Gridtrr.Rows[i].Cells[8].Text + ":" + Gridtrr.Rows[i].Cells[3].Text + ":" + Gridtrr.Rows[i].Cells[0].Text + " For DrawNo:" + cashNarration + "'," + Gridtrr.Rows[i].Cells[11].Text + ",'PORTAL'," + Gridtrr.Rows[i].Cells[3].Text + "," + 1 + "," + dtChoosenDate1.Day + "," + dtChoosenDate1.Month + "," + dtChoosenDate1.Year + "," + Gridtrr.Rows[i].Cells[7].Text + "," + 1 + "," + 3 + "," + tokenid + "," + 0 + "," + 0 + "," + 0 + ",'" + Gridtrr.Rows[i].Cells[1].Text + "')");

                            //04/10/2021 -Bala
                            txtmiscAmt = Gridtrr.Rows[i].Cells[8].Text;
                            txtOtherAmt = Gridtrr.Rows[i].Cells[9].Text;
                            if (decimal.Parse(txtmiscAmt) != 0.00M || decimal.Parse(txtOtherAmt) != 0.00M)
                            {
                                //trn.insertorupdateTrn("INSERT INTO `svcf`.`voucher`(`DualTransactionKey`,`BranchID`,`CurrDate`,`Voucher_No`,`Voucher_Type`,`Head_Id`,`ChoosenDate`,`Narration`,`Amount`,`Series`,`ReceievedBy`,`Trans_Type`,`T_Day`,`T_Month`,`T_Year`,`MemberID`,`Trans_Medium`,`RootID`,`ChitGroupId`,`Other_Trans_Type`,`IsDeleted`,`IsAccepted`,`AppReceiptno`)VALUES(" + DualTransactionKey + "," + balayer.ToobjectstrEvenNull(Session["Branchid"]) + ",'" + DateTime.Now.ToString("yyyy-MM-dd") + "','" + voucherno + "','C'," + Gridtrr.Rows[i].Cells[5].Text + ",'" + dtChoosenDate1.ToString("yyyy-MM-dd") + "','" + intNarration + "'," + amount1 + ",'PORTAL'," + Gridtrr.Rows[i].Cells[3].Text + "," + 1 + "," + dtChoosenDate1.Day + "," + dtChoosenDate1.Month + "," + dtChoosenDate1.Year + "," + Gridtrr.Rows[i].Cells[7].Text + "," + 1 + "," + 11 + "," + Gridtrr.Rows[i].Cells[5].Text + "," + 0 + "," + 0 + "," + 0 + ",'" + Gridtrr.Rows[i].Cells[1].Text + "')");
                                //trn.insertorupdateTrn("INSERT INTO `svcf`.`voucher`(`DualTransactionKey`,`BranchID`,`CurrDate`,`Voucher_No`,`Voucher_Type`,`Head_Id`,`ChoosenDate`,`Narration`,`Amount`,`Series`,`ReceievedBy`,`Trans_Type`,`T_Day`,`T_Month`,`T_Year`,`MemberID`,`Trans_Medium`,`RootID`,`ChitGroupId`,`Other_Trans_Type`,`IsDeleted`,`IsAccepted`,`AppReceiptno`)VALUES(" + DualTransactionKey + "," + balayer.ToobjectstrEvenNull(Session["Branchid"]) + ",'" + DateTime.Now.ToString("yyyy-MM-dd") + "','" + voucherno + "','D'," + Gridtrr.Rows[i].Cells[19].Text + ",'" + dtChoosenDate1.ToString("yyyy-MM-dd") + "','" + intNarration + "'," + amount1 + ",'PORTAL'," + Gridtrr.Rows[i].Cells[3].Text + "," + 1 + "," + dtChoosenDate1.Day + "," + dtChoosenDate1.Month + "," + dtChoosenDate1.Year + "," + Gridtrr.Rows[i].Cells[7].Text + "," + 1 + "," + 3 + "," + Gridtrr.Rows[i].Cells[5].Text + "," + 0 + "," + 0 + "," + 0 + ",'" + Gridtrr.Rows[i].Cells[1].Text + "')");
                                trn.insertorupdateTrn("INSERT INTO `svcf`.`voucher`(`DualTransactionKey`,`BranchID`,`CurrDate`,`Voucher_No`,`Voucher_Type`,`Head_Id`,`ChoosenDate`,`Narration`,`Amount`,`Series`,`ReceievedBy`,`Trans_Type`,`T_Day`,`T_Month`,`T_Year`,`MemberID`,`Trans_Medium`,`RootID`,`ChitGroupId`,`Other_Trans_Type`,`IsDeleted`,`IsAccepted`,`AppReceiptno`)VALUES(" + DualTransactionKey + "," + balayer.ToobjectstrEvenNull(Session["Branchid"]) + ",'" + DateTime.Now.ToString("yyyy-MM-dd") + "','" + voucherno + "','C'," + tokenid + ",'" + dtChoosenDate1.ToString("yyyy-MM-dd") + "','" + intNarration + "'," + amount1 + ",'PORTAL'," + receivedbyId + "," + 1 + "," + dtChoosenDate1.Day + "," + dtChoosenDate1.Month + "," + dtChoosenDate1.Year + "," + memberId + "," + 1 + "," + 11 + "," + tokenid + "," + 0 + "," + 0 + "," + 0 + ",'" + Gridtrr.Rows[i].Cells[1].Text + "')");
                                trn.insertorupdateTrn("INSERT INTO `svcf`.`voucher`(`DualTransactionKey`,`BranchID`,`CurrDate`,`Voucher_No`,`Voucher_Type`,`Head_Id`,`ChoosenDate`,`Narration`,`Amount`,`Series`,`ReceievedBy`,`Trans_Type`,`T_Day`,`T_Month`,`T_Year`,`MemberID`,`Trans_Medium`,`RootID`,`ChitGroupId`,`Other_Trans_Type`,`IsDeleted`,`IsAccepted`,`AppReceiptno`)VALUES(" + DualTransactionKey + "," + balayer.ToobjectstrEvenNull(Session["Branchid"]) + ",'" + DateTime.Now.ToString("yyyy-MM-dd") + "','" + voucherno + "','D'," + bankheadId + ",'" + dtChoosenDate1.ToString("yyyy-MM-dd") + "','" + intNarration + "'," + amount1 + ",'PORTAL'," + receivedbyId + "," + 1 + "," + dtChoosenDate1.Day + "," + dtChoosenDate1.Month + "," + dtChoosenDate1.Year + "," + memberId + "," + 1 + "," + 3 + "," + tokenid + "," + 0 + "," + 0 + "," + 0 + ",'" + Gridtrr.Rows[i].Cells[1].Text + "')");
                            }
                        }
                        else
                        {
                            //trn.insertorupdateTrn("INSERT INTO `svcf`.`voucher`(`DualTransactionKey`,`BranchID`,`CurrDate`,`Voucher_No`,`Voucher_Type`,`Head_Id`,`ChoosenDate`,`Narration`,`Amount`,`Series`,`ReceievedBy`,`Trans_Type`,`T_Day`,`T_Month`,`T_Year`,`MemberID`,`Trans_Medium`,`RootID`,`ChitGroupId`,`Other_Trans_Type`,`IsDeleted`,`IsAccepted`,`AppReceiptno`)VALUES(" + DualTransactionKey + "," + balayer.ToobjectstrEvenNull(Session["Branchid"]) + ",'" + DateTime.Now.ToString("yyyy-MM-dd") + "','" + voucherno + "','C'," + Gridtrr.Rows[i].Cells[9].Text + ",'" + dtChoosenDate1.ToString("yyyy-MM-dd") + "','" + Gridtrr.Rows[i].Cells[1].Text + ":" + Gridtrr.Rows[i].Cells[8].Text + ":" + Draw1 + ":" + Gridtrr.Rows[i].Cells[6].Text + "'," + Gridtrr.Rows[i].Cells[11].Text + ",'PORTAL'," + Gridtrr.Rows[i].Cells[3].Text + "," + 1 + "," + dtChoosenDate1.Day + "," + dtChoosenDate1.Month + "," + dtChoosenDate1.Year + "," + Gridtrr.Rows[i].Cells[7].Text + "," + 1 + "," + 5 + "," + Gridtrr.Rows[i].Cells[5].Text + "," + 1 + "," + 0 + "," + 0 + ",'" + Gridtrr.Rows[i].Cells[1].Text + "')");
                            //trn.insertorupdateTrn("INSERT INTO `svcf`.`voucher`(`DualTransactionKey`,`BranchID`,`CurrDate`,`Voucher_No`,`Voucher_Type`,`Head_Id`,`ChoosenDate`,`Narration`,`Amount`,`Series`,`ReceievedBy`,`Trans_Type`,`T_Day`,`T_Month`,`T_Year`,`MemberID`,`Trans_Medium`,`RootID`,`ChitGroupId`,`Other_Trans_Type`,`IsDeleted`,`IsAccepted`,`AppReceiptno`)VALUES(" + DualTransactionKey + "," + balayer.ToobjectstrEvenNull(Session["Branchid"]) + ",'" + DateTime.Now.ToString("yyyy-MM-dd") + "','" + voucherno + "','D'," + Gridtrr.Rows[i].Cells[19].Text + ",'" + dtChoosenDate1.ToString("yyyy-MM-dd") + "','" + Gridtrr.Rows[i].Cells[1].Text + ":" + Gridtrr.Rows[i].Cells[8].Text + ":" + Draw1 + ":" + Gridtrr.Rows[i].Cells[6].Text + "'," + Gridtrr.Rows[i].Cells[11].Text + ",'PORTAL'," + Gridtrr.Rows[i].Cells[3].Text + "," + 1 + "," + dtChoosenDate1.Day + "," + dtChoosenDate1.Month + "," + dtChoosenDate1.Year + "," + Gridtrr.Rows[i].Cells[7].Text + "," + 1 + "," + 3 + "," + Gridtrr.Rows[i].Cells[5].Text + "," + 1 + "," + 0 + "," + 0 + ",'" + Gridtrr.Rows[i].Cells[1].Text + "')");
                            trn.insertorupdateTrn("INSERT INTO `svcf`.`voucher`(`DualTransactionKey`,`BranchID`,`CurrDate`,`Voucher_No`,`Voucher_Type`,`Head_Id`,`ChoosenDate`,`Narration`,`Amount`,`Series`,`ReceievedBy`,`Trans_Type`,`T_Day`,`T_Month`,`T_Year`,`MemberID`,`Trans_Medium`,`RootID`,`ChitGroupId`,`Other_Trans_Type`,`IsDeleted`,`IsAccepted`,`AppReceiptno`)VALUES(" + DualTransactionKey + "," + balayer.ToobjectstrEvenNull(Session["Branchid"]) + ",'" + DateTime.Now.ToString("yyyy-MM-dd") + "','" + voucherno + "','C'," + branchId + ",'" + dtChoosenDate1.ToString("yyyy-MM-dd") + "','" + "Ref No:" + receivedbyId + " |Recd From:" + Gridtrr.Rows[i].Cells[8].Text + ":" + Gridtrr.Rows[i].Cells[3].Text + ":" + Gridtrr.Rows[i].Cells[0].Text + " For DrawNo:" + cashNarration + " (aprox) Received By: " + balayer.ToobjectstrEvenNull(Session["BranchName"]) + "'," + Gridtrr.Rows[i].Cells[11].Text + ",'PORTAL'," + receivedbyId + "," + 1 + "," + dtChoosenDate1.Day + "," + dtChoosenDate1.Month + "," + dtChoosenDate1.Year + "," + memberId + "," + 1 + "," + 5 + "," + tokenid + "," + 1 + "," + 0 + "," + 0 + ",'" + Gridtrr.Rows[i].Cells[1].Text + "')");
                            trn.insertorupdateTrn("INSERT INTO `svcf`.`voucher`(`DualTransactionKey`,`BranchID`,`CurrDate`,`Voucher_No`,`Voucher_Type`,`Head_Id`,`ChoosenDate`,`Narration`,`Amount`,`Series`,`ReceievedBy`,`Trans_Type`,`T_Day`,`T_Month`,`T_Year`,`MemberID`,`Trans_Medium`,`RootID`,`ChitGroupId`,`Other_Trans_Type`,`IsDeleted`,`IsAccepted`,`AppReceiptno`)VALUES(" + DualTransactionKey + "," + balayer.ToobjectstrEvenNull(Session["Branchid"]) + ",'" + DateTime.Now.ToString("yyyy-MM-dd") + "','" + voucherno + "','D'," + bankheadId + ",'" + dtChoosenDate1.ToString("yyyy-MM-dd") + "','" + "Ref No:" + receivedbyId + " |Recd From:" + Gridtrr.Rows[i].Cells[8].Text + ":" + Gridtrr.Rows[i].Cells[3].Text + ":" + Gridtrr.Rows[i].Cells[0].Text + " For DrawNo:" + cashNarration + " (aprox) Received By: " + balayer.ToobjectstrEvenNull(Session["BranchName"]) + "'," + Gridtrr.Rows[i].Cells[11].Text + ",'PORTAL'," + receivedbyId + "," + 1 + "," + dtChoosenDate1.Day + "," + dtChoosenDate1.Month + "," + dtChoosenDate1.Year + "," + memberId + "," + 1 + "," + 3 + "," + tokenid + "," + 1 + "," + 0 + "," + 0 + ",'" + Gridtrr.Rows[i].Cells[1].Text + "')");

                            //04/10/2021 -Bala
                            txtmiscAmt = Gridtrr.Rows[i].Cells[12].Text;
                            txtOtherAmt = Gridtrr.Rows[i].Cells[13].Text;
                            if (decimal.Parse(txtmiscAmt) != 0.00M || decimal.Parse(txtOtherAmt) != 0.00M)
                            {
                                //    trn.insertorupdateTrn("INSERT INTO `svcf`.`voucher`(`DualTransactionKey`,`BranchID`,`CurrDate`,`Voucher_No`,`Voucher_Type`,`Head_Id`,`ChoosenDate`,`Narration`,`Amount`,`Series`,`ReceievedBy`,`Trans_Type`,`T_Day`,`T_Month`,`T_Year`,`MemberID`,`Trans_Medium`,`RootID`,`ChitGroupId`,`Other_Trans_Type`,`IsDeleted`,`IsAccepted`,`AppReceiptno`)VALUES(" + DualTransactionKey + "," + balayer.ToobjectstrEvenNull(Session["Branchid"]) + ",'" + DateTime.Now.ToString("yyyy-MM-dd") + "','" + voucherno + "','C'," + Gridtrr.Rows[i].Cells[9].Text + ",'" + dtChoosenDate1.ToString("yyyy-MM-dd") + "','" + "Misc :-"+intNarration + "'," + amount1 + ",'PORTAL'," + Gridtrr.Rows[i].Cells[3].Text + "," + 1 + "," + dtChoosenDate1.Day + "," + dtChoosenDate1.Month + "," + dtChoosenDate1.Year + "," + Gridtrr.Rows[i].Cells[7].Text + "," + 1 + "," + 11 + "," + tokenid + "," + 1 + "," + 0 + "," + 0 + ",'" + Gridtrr.Rows[i].Cells[1].Text + "')");
                                //    trn.insertorupdateTrn("INSERT INTO `svcf`.`voucher`(`DualTransactionKey`,`BranchID`,`CurrDate`,`Voucher_No`,`Voucher_Type`,`Head_Id`,`ChoosenDate`,`Narration`,`Amount`,`Series`,`ReceievedBy`,`Trans_Type`,`T_Day`,`T_Month`,`T_Year`,`MemberID`,`Trans_Medium`,`RootID`,`ChitGroupId`,`Other_Trans_Type`,`IsDeleted`,`IsAccepted`,`AppReceiptno`)VALUES(" + DualTransactionKey + "," + balayer.ToobjectstrEvenNull(Session["Branchid"]) + ",'" + DateTime.Now.ToString("yyyy-MM-dd") + "','" + voucherno + "','D'," + Gridtrr.Rows[i].Cells[19].Text + ",'" + dtChoosenDate1.ToString("yyyy-MM-dd") + "','" + "Misc :-"+intNarration + "'," + amount1 + ",'PORTAL'," + Gridtrr.Rows[i].Cells[3].Text + "," + 1 + "," + dtChoosenDate1.Day + "," + dtChoosenDate1.Month + "," + dtChoosenDate1.Year + "," + Gridtrr.Rows[i].Cells[7].Text + "," + 1 + "," + 3 + "," + tokenid + "," + 1 + "," + 0 + "," + 0 + ",'" + Gridtrr.Rows[i].Cells[1].Text + "')");
                                trn.insertorupdateTrn("INSERT INTO `svcf`.`voucher`(`DualTransactionKey`,`BranchID`,`CurrDate`,`Voucher_No`,`Voucher_Type`,`Head_Id`,`ChoosenDate`,`Narration`,`Amount`,`Series`,`ReceievedBy`,`Trans_Type`,`T_Day`,`T_Month`,`T_Year`,`MemberID`,`Trans_Medium`,`RootID`,`ChitGroupId`,`Other_Trans_Type`,`IsDeleted`,`IsAccepted`,`AppReceiptno`)VALUES(" + DualTransactionKey + "," + balayer.ToobjectstrEvenNull(Session["Branchid"]) + ",'" + DateTime.Now.ToString("yyyy-MM-dd") + "','" + voucherno + "','C'," + branchId + ",'" + dtChoosenDate1.ToString("yyyy-MM-dd") + "','" + "Misc :-" + intNarration + "'," + amount1 + ",'PORTAL'," + receivedbyId + "," + 1 + "," + dtChoosenDate1.Day + "," + dtChoosenDate1.Month + "," + dtChoosenDate1.Year + "," + memberId + "," + 1 + "," + 11 + "," + tokenid + "," + 1 + "," + 0 + "," + 0 + ",'" + Gridtrr.Rows[i].Cells[1].Text + "')");
                                trn.insertorupdateTrn("INSERT INTO `svcf`.`voucher`(`DualTransactionKey`,`BranchID`,`CurrDate`,`Voucher_No`,`Voucher_Type`,`Head_Id`,`ChoosenDate`,`Narration`,`Amount`,`Series`,`ReceievedBy`,`Trans_Type`,`T_Day`,`T_Month`,`T_Year`,`MemberID`,`Trans_Medium`,`RootID`,`ChitGroupId`,`Other_Trans_Type`,`IsDeleted`,`IsAccepted`,`AppReceiptno`)VALUES(" + DualTransactionKey + "," + balayer.ToobjectstrEvenNull(Session["Branchid"]) + ",'" + DateTime.Now.ToString("yyyy-MM-dd") + "','" + voucherno + "','D'," + bankheadId + ",'" + dtChoosenDate1.ToString("yyyy-MM-dd") + "','" + "Misc :-" + intNarration + "'," + amount1 + ",'PORTAL'," + receivedbyId + "," + 1 + "," + dtChoosenDate1.Day + "," + dtChoosenDate1.Month + "," + dtChoosenDate1.Year + "," + memberId + "," + 1 + "," + 3 + "," + tokenid + "," + 1 + "," + 0 + "," + 0 + ",'" + Gridtrr.Rows[i].Cells[1].Text + "')");
                            }
                        }

                        trn.CommitTrn();
                    }
                    catch (Exception error)
                    {
                        try
                        {
                            trn.RollbackTrn();
                        }
                        catch
                        { }
                        finally
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "Warning", "alert('" + error.Message.ToString().Replace("'", "\\'") + "');", true);
                        }
                    }
                    finally
                    {
                        trn.DisposeTrn();
                    }
                }
            }
            Response.Write("<script>alert('Your amount has been saved successfully');</script>");

            Session["CheckRefresh"] = System.Guid.NewGuid().ToString();

            SetInitialRow();
            SetInitialRowCheque();
            //SetInitialRowCheque();

            //  TxtSNo.Text = balayer.ToobjectstrEvenNull(Session["Branchid"]) + "OFF" + Convert.ToInt32(reciprnum.ToString());
            CollectorName();
            //Chitno();
            BranchName();
            fillBankHead();
            bankdetails();



            string branchcode = balayer.GetSingleValue("select B_Prefix from svcf.branchdetails where Head_Id=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + "");
            Random rnd = new Random();
            int myRandomNo1 = rnd.Next(100000, 999999);
            //Console.WriteLine("A" + "-" + myRandomNo);
            //24/09/2021 - Bala
            //TxtSNo.Text = branchcode + '/' + "OFF" + '/' + 'A' + '-' + myRandomNo1;
            string rnQry1 = "select ifnull( max(Voucher_No)+1,0) from voucher where BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and Series='PORTAL' and Trans_Type=1";
            int voucherno1 = int.Parse(balayer.GetSingleValue(rnQry1));
            TxtSNo.Text = branchcode + '/' + "OFF/A-" + voucherno1.ToString().PadLeft(6, '0');

            txtDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txtchit.Text = "";
            txtother.Text = "";
            txtmisc.Text = "";
            LabMemname.Text = "";
            LabMemnameID.Text = "";
            txttotalamount.Text = "";
            txtChequeNumber.Text = "";

        }

        protected void txtchit_TextChanged(object sender, EventArgs e)

        {
            //25/09/2021
            decimal chitAmt = 0;
            decimal miscAmt = 0;
            decimal otherAmt = 0;
            if (!string.IsNullOrEmpty(txtchit.Text))
            {
                chitAmt = Convert.ToDecimal(txtchit.Text);
            }
            if (!string.IsNullOrEmpty(txtmisc.Text))
            {
                miscAmt = Convert.ToDecimal(txtmisc.Text);
            }
            if (!string.IsNullOrEmpty(txtother.Text))
            {
                otherAmt = Convert.ToDecimal(txtother.Text);
            }
            txttotalamount.Text = Convert.ToString(chitAmt + miscAmt + otherAmt);
        }
        //protected void txtmisc_TextChanged(object sender, EventArgs e)
        //{
        //    //25/09/2021
        //    decimal chitAmt = 0;
        //    decimal miscAmt = 0;
        //    decimal otherAmt = 0;
        //    if (!string.IsNullOrEmpty(txtchit.Text))
        //    {
        //        chitAmt = Convert.ToDecimal(txtchit.Text);
        //    }
        //    if (!string.IsNullOrEmpty(txtmisc.Text))
        //    {
        //        miscAmt = Convert.ToDecimal(txtmisc.Text);
        //    }
        //    if (!string.IsNullOrEmpty(txtother.Text))
        //    {
        //        otherAmt = Convert.ToDecimal(txtother.Text);
        //    }
        //    txttotalamount.Text = Convert.ToString(chitAmt + miscAmt + otherAmt);
        //}
        //protected void txtother_TextChanged(object sender, EventArgs e)
        //{
        //    //25/09/2021
        //    decimal chitAmt = 0;
        //    decimal miscAmt = 0;
        //    decimal otherAmt = 0;
        //    if (!string.IsNullOrEmpty(txtchit.Text))
        //    {
        //        chitAmt = Convert.ToDecimal(txtchit.Text);
        //    }
        //    if (!string.IsNullOrEmpty(txtmisc.Text))
        //    {
        //        miscAmt = Convert.ToDecimal(txtmisc.Text);
        //    }
        //    if (!string.IsNullOrEmpty(txtother.Text))
        //    {
        //        otherAmt = Convert.ToDecimal(txtother.Text);
        //    }
        //    txttotalamount.Text = Convert.ToString(chitAmt + miscAmt + otherAmt);
        //}

        protected void Idother_Cick(object sender, EventArgs e)
        {
            if (CheckCheque.Checked == true)
            {
                Gridbinedtrr();
            }
            else
            {
                //ClientScript.RegisterStartupScript(this.GetType(), "Alert", "Please Check Your Values", true);
                Response.Write("<script>alert('Please fill out all required fields');</script>");

            }
        }
        protected void btnGenerte_Click(object sender, EventArgs e)
        {
            Session["CheckRefresh"] = System.Guid.NewGuid().ToString();

            SetInitialRow();
            SetInitialRowCheque();
            //SetInitialRowCheque();

            //  TxtSNo.Text = balayer.ToobjectstrEvenNull(Session["Branchid"]) + "OFF" + Convert.ToInt32(reciprnum.ToString());
            CollectorName();
            //Chitno();
            BranchName();
            fillBankHead();
            bankdetails();



            string branchcode = balayer.GetSingleValue("select B_Prefix from svcf.branchdetails where Head_Id=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + "");
            Random rnd = new Random();
            int myRandomNo = rnd.Next(100000, 999999);
            //Console.WriteLine("A" + "-" + myRandomNo);
            TxtSNo.Text = branchcode + '/' + "OFF" + '/' + 'A' + '-' + myRandomNo;

            txtDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txtchit.Text = "";
            txtother.Text = "";
            txtmisc.Text = "";
            LabMemname.Text = "";
            LabMemnameID.Text = "";
            txttotalamount.Text = "";
            txtChequeNumber.Text = "";


        }
        protected void OnPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView2.PageIndex = e.NewPageIndex;

            this.Gridbined();
        }

        //public override void VerifyRenderingInServerForm(Control control)
        //{
        //    /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
        //       server control at run time. */
        //}

        protected void CheckCash_click(object sender, EventArgs e)
        {
            cash = 0;
            if (CheckCash.Checked == true)
            {
                myDIV.Visible = false;
                chequetab.Visible = false;
                CheckCheque.Checked = false;
                //printcheque.Visible = false;
                IDAdd.Visible = true;
                Panel1.Visible = true;


            }


        }

        protected void CheckCheque_CheckedChanged(object sender, EventArgs e)
        {
            cheque = 1;
            if (CheckCheque.Checked == true)
            {
                CheckCash.Checked = false;
                myDIV.Visible = true;
                chequetab.Visible = true;
                //printcase.Visible = false;
                IDAdd.Visible = false;
            }

        }

        private void SetInitialRowCheque()
        {
            DataTable bindgrid1 = new DataTable();
            bindgrid1.Columns.Add("SeriesNumber");
            bindgrid1.Columns.Add("Date");
            bindgrid1.Columns.Add("ReceivedByID");
            bindgrid1.Columns.Add("ReceivedBy");
            bindgrid1.Columns.Add("ChitnoID");
            bindgrid1.Columns.Add("Chitno");
            bindgrid1.Columns.Add("MemberNameID");
            bindgrid1.Columns.Add("MemberName");
            bindgrid1.Columns.Add("BranchNameID");
            bindgrid1.Columns.Add("BranchName");
            bindgrid1.Columns.Add("ChitAmount");
            bindgrid1.Columns.Add("MiscAmount");
            bindgrid1.Columns.Add("OtherAmount");
            bindgrid1.Columns.Add("TotalAmount");

            bindgrid1.Columns.Add("ChequeNumber");
            bindgrid1.Columns.Add("BankDetailsID");
            bindgrid1.Columns.Add("BankDetails");
            bindgrid1.Columns.Add("ChequeDate");
            bindgrid1.Columns.Add("BankHeadId");
            bindgrid1.Columns.Add("BankHead");


            Gridtrr.DataSource = bindgrid1;
            Gridtrr.DataBind();
            ViewState["CurrentTable1"] = bindgrid1;
        }

        void Gridbinedtrr()
        {
            string DualTransactionKey = "";

            System.Guid guid = Guid.NewGuid();
            string guidForChar36 = guid.ToString();
            string hexstring = BitConverter.ToString(guid.ToByteArray());
            string guidForBinary16 = "0x" + hexstring.Replace("-", string.Empty);
            DualTransactionKey = guidForBinary16;
            DateTime dtChoosenDate = DateTime.Parse(txtChequeDate.Text);
            DateTime dtChoosenDate1 = DateTime.Parse(txtDate.Text);
            string voucherno = "";

            //string v1 = balayer.GetSingleValue("select concat(max(Voucher_No) + 1)  from svcf.voucher where Series='PORTAL'");
            //if (voucherno == v1)
            //{
            //    voucherno = v1;
            //}
            //else
            //{
            //    voucherno = "1";
            //}
            Random r = new Random();
            int genRand = r.Next(1, 100000);

            voucherno = Convert.ToString(myRandomNo);
            if (ViewState["CurrentTable1"] != null)
            {
                DataTable dtCurrentTable1 = (DataTable)ViewState["CurrentTable1"];
                DataRow dr = null;

                dr = dtCurrentTable1.NewRow();
                dr[0] = TxtSNo.Text;
                dr[1] = txtDate.Text;
                dr[2] = LabBnameID.Text;
                dr[3] = LabBname.Text;
                dr[4] = LabChitID.Text;
                dr[5] = LabChit.Text;
                dr[6] = LabMemnameID.Text;
                dr[7] = LabMemname.Text;
                dr[8] = LabBanmelistID.Text;
                dr[9] = LabBanmelist.Text;
                if (txtchit.Text == "")
                    txtchit.Text = "0.00";
                dr[10] = txtchit.Text;
                if (txtmisc.Text == "")
                    txtmisc.Text = "0.00";
                dr[11] = txtmisc.Text;
                if (txtother.Text == "")
                    txtother.Text = "0.00";
                dr[12] = txtother.Text;
                dr[13] = txttotalamount.Text;
                dr[14] = txtChequeNumber.Text;
                dr[15] = LabBankDetailsID.Text;
                dr[16] = LabBankDetailsText.Text;
                dr[17] = txtChequeDate.Text;
                dr[18] = LabBankHeadID.Text;
                dr[19] = LabBankHeadText.Text;
                dtCurrentTable1.Rows.Add(dr);
                string narration = LabChit.Text + ":" + LabMemname.Text;
                decimal amount1 = Convert.ToDecimal(txtmisc.Text) + Convert.ToDecimal(txtother.Text);

                Gridtrr.DataSource = dtCurrentTable1;
                Gridtrr.DataBind();
                ViewState["CurrentTable1"] = dtCurrentTable1;


            }

        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            //return;
        }

        protected void Print_Onlick(object sender, EventArgs e)
        {
            string imageFile = (Server.MapPath("~\\Images\\logo_New.png"));
            Document doc = new Document();
            DateTime dtChoosenDate1 = DateTime.Parse(txtDate.Text);
            string Branchname = balayer.GetSingleValue("select B_Name from svcf.branchdetails where Head_Id=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + "");
            try
            {
                using (StringWriter sw = new StringWriter())
                {
                    using (HtmlTextWriter hw = new HtmlTextWriter(sw))
                    {

                        System.Text.StringBuilder sb = new System.Text.StringBuilder();

                        sb.Append(@"<html><body><table font-family width='100%' cellpadding='1'><tbody><tr><td style='width:50%'> <strong>Estd:1947</strong></td><td style='text-align: right;width:30%'> <strong>Form : IX</strong></td></tr><tr align='center'><td colspan='2' style='line-height:75pt;'><img src=" + imageFile + "></td></tr></tbody></table>");
                        for (int i = 0; i < GridView2.Rows.Count; i++)
                        {
                            sb.Append(@"<center>
<h2 style='text-align:center;'>SREE VISALAM CHIT FUND LTD.</h2>

<p style='text-align:center;color:lightgrey;'>Registerd Office: Tirunelveli - 6</p>
<p style='text-align:center;color:lightgrey;'>(See Section 23 And Rule 25)</p>
<p style='text-align:center;color:lightgrey;'>Receipt Issued Under The Chit Funds Act 1982</p>
<p style='text-align:center;'><b>Branch Name: " + Branchname + "</b>" +
        "</p></center><br/><body><center><div style='background-color:lightgrey;text-align:center;padding:5px;width:40%'>" +
        "<table><tr><td align='left'><b>CD/REC.SERIES </b></td><td>Date: " + dtChoosenDate1 + "</td></tr><tr><td>Payment : Cash</td></tr>" +
        "<tr><td align='left'>Receipt No: " + GridView2.Rows[i].Cells[1].Text + "</td></tr></table>" +
        "<p style='text-align:left;'>----------------------------------------------------------------------" +
        "--------------------------------</p><table align='left' cellspacing='0' cellpadding='2'>" +
        "<tr><td> Branch Name :</td>" +
        "<td>" + GridView2.Rows[i].Cells[10].Text + "</td></tr>" +
        "<tr><td>Received by :</td>" +
        "<td>" + GridView2.Rows[i].Cells[4].Text + "</td></tr>" +
        "<tr><td>Chit No :</td>" +
        "<td>" + GridView2.Rows[i].Cells[6].Text + "</td></tr>" +
        "< tr><td>Member Name :</td>" +
        "<td>" + GridView2.Rows[i].Cells[8].Text + "</td></tr>" +
        "<tr><td>Chit Amount :</td>" +
        "<td>" + GridView2.Rows[i].Cells[11].Text + "</td></tr>" +
        "<tr><td> Amount :</td>" +
        "<td>" + GridView2.Rows[i].Cells[12].Text + "</td></tr>" +
        "<tr><td>Other Amount :</td>" +
        "<td>" + GridView2.Rows[i].Cells[13].Text + "</td></tr>" +
        " <tr><td>Total Amount</td>" +
        "<td>" + GridView2.Rows[i].Cells[14].Text + "</td></tr>" +
        "</table></center></body></html>");

                        }

                        for (int i = 0; i < Gridtrr.Rows.Count; i++)
                        {
                            sb.Append(@"<center>
<h2 style='text-align:center;'>SREE VISALAM CHIT FUND LTD.</h2>

<p style='text-align:center;color:lightgrey;'>Registerd Office: Tirunelveli - 6</p>
<p style='text-align:center;color:lightgrey;'>(See Section 23 And Rule 25)</p>
<p style='text-align:center;color:lightgrey;'>Receipt Issued Under The Chit Funds Act 1982</p>
<p style='text-align:center;'><b>Branch Name: " + Branchname + "</b>" +
       "</p></center><br/><body><center><div style='background-color:lightgrey;text-align:center;padding:5px;width:40%'>" +
       "<table><tr><td align='left'><b>CD/REC.SERIES</b></td><td>Date: " + dtChoosenDate1 + "</td></tr><tr><td>Payment : Cheque</td</tr>" +
       "<tr><td align='left'>Receipt No: " + Gridtrr.Rows[i].Cells[1].Text + "</td></tr></table>" +
       "<p style='text-align:left;'>----------------------------------------------------------------------" +
       "--------------------------------</p><table align='left' cellspacing='0' cellpadding='2'>" +
       "<tr><td> Branch Name :</td>" +
       "<td>" + (Gridtrr.Rows[i].Cells[10].Text) + "</td></tr>" +
       "<tr><td>Received by :</td>" +
      "<td>" + (Gridtrr.Rows[i].Cells[4].Text) + "</td></tr>" +
       "<tr><td>Chit No :</td>" +
       " <td>" + (Gridtrr.Rows[i].Cells[6].Text) + "</td></tr>" +
       "<tr><td>Member Name :</td>" +
       "<td>" + (Gridtrr.Rows[i].Cells[8].Text) + "</td></tr>" +
       "<tr><td>Chit Amount :</td>" +
       "<td>" + (Gridtrr.Rows[i].Cells[11].Text) + "</td></tr>" +
       "<tr><td> Amount :</td>" +
       "<td>" + (Gridtrr.Rows[i].Cells[12].Text) + "</td></tr>" +
       "<tr><td>Other Amount :</td>" +
       "<td>" + (Gridtrr.Rows[i].Cells[13].Text) + "</td></tr>" +
       "<tr><td>Total Amount</td>" +
       "<td>" + (Gridtrr.Rows[i].Cells[14].Text) + "</td></tr>" +
       "<tr><td>Cheque Number :</td>" +
       "<td>" + (Gridtrr.Rows[i].Cells[15].Text) + "</td></tr>" +
       "<tr><td>Cheque Date :</td>" +
       "<td>" + (Gridtrr.Rows[i].Cells[18].Text) + "</td></tr>" +
       "<tr><td>Bank :</td>" +
       "<td>" + (Gridtrr.Rows[i].Cells[17].Text) + "</td></tr>" +
       "</table></center></body></html>");

                        }

                        StringReader sr = new StringReader(sb.ToString());

                        iTextSharp.text.Document pdfDoc = new iTextSharp.text.Document(PageSize.EXECUTIVE, 25f, 25f, 25f, 25f);
                        HTMLWorker htmlparser = new HTMLWorker(pdfDoc);

                        PdfWriter writer = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                        pdfDoc.Open();
                        htmlparser.Parse(sr);
                        pdfDoc.Close();
                        Response.ContentType = "application/pdf";
                        Response.AddHeader("content-disposition", "attachment;filename='" + TxtSNo.Text + ".pdf");
                        Response.Cache.SetCacheability(HttpCacheability.NoCache);
                        Response.Write(pdfDoc);
                        HttpContext.Current.ApplicationInstance.CompleteRequest();
                    }

                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"The file was not found");
            }

        }

        protected void ddlbranchList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            LabBanmelist.Text = ddlbranchList1.SelectedItem.Text;
            LabBanmelistID.Text = ddlbranchList1.SelectedItem.Value;

            Tempdic.Clear();
            Tempdic = balayer.CmnList(@"SELECT Head_Id,GrpMemberID FROM membertogroupmaster where  BranchID=" + ddlbranchList1.SelectedItem.Value);
            DddlChitNO.DataValueField = "Key";
            DddlChitNO.DataTextField = "Value";

            DddlChitNO.DataSource = Tempdic;
            DddlChitNO.DataBind();

            DddlChitNO.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));

        }
    }
}