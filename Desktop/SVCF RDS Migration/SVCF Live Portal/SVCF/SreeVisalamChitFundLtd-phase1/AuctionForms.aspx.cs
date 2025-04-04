using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using MySql.Data;
using MySql.Data.MySqlClient;
using SVCF_BusinessAccessLayer;
using SVCF_TransactionLayer;
using SVCF_DataAccessLayer;
using log4net;
using log4net.Config;

namespace SreeVisalamChitFundLtd_phase1
{
    public partial class AuctionForms : System.Web.UI.Page
    {
        #region Object
        BusinessLayer balayer = new BusinessLayer();
        TransactionLayer trn = new TransactionLayer();
        #endregion

        ILog logger = log4net.LogManager.GetLogger(typeof(AuctionForms));

        protected void Page_Load(object sender, EventArgs e)
        {
            Pnlmsg1.Visible = false;
            panchitHistory.Visible = false;
            if (!IsPostBack)
            {
                var userinfo = HttpContext.Current.User.Identity.Name;
                var qry = "select rs.name from roles as rs inner join rights as rt on (rt.roleid=rs.id) where memberid=" + userinfo + "";
                var usrRole = balayer.GetSingleValue(qry);
                if (usrRole == "Report")
                {
                    Response.Redirect("Home.aspx", false);
                }


                lblRebiddate.Visible = false;
                Pnlmsg1.Visible = false;
                panchitHistory.Visible = false;
                 //ddlDrawNo.Visible = false;
                DataTable ChitGrp = new DataTable();
                ChitGrp = balayer.GetDataTable("select distinct t1.GroupID as HeadId,t2.GROUPNO as ChitGroupNo from auctiondetails   as t1 join groupmaster as t2 on t1.GroupID=t2.Head_Id where t1.BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and   IsPrized <> 'Y'");
                ddlChitNo.DataSource = ChitGrp;
                ddlChitNo.DataValueField = "HeadId";
                ddlChitNo.DataTextField = "ChitGroupNo";
                ddlChitNo.DataBind();
                ddlChitNo.Items.Insert(0, "--select--");
                ddlAuction.Items.Add("--select--");
                ddlAuction.Items.Add("New Auction");
                ddlAuction.Items.Add("Re-Bid");
                ddlAuction.SelectedIndex = 0;
                ddlAuction.Enabled = false;
              
            }
        }
        protected void ddlDrawNo_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
        protected void ddlChitNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ("--select--" == balayer.ToobjectstrEvenNull(ddlChitNo.SelectedItem))
            {
                ddlBenefName.SelectedIndex = 0;
                txtPrizamt.Text = "";
                ddlAuction.SelectedIndex = 0;
                ddlAuction.Enabled = false;
                ddlDrawNo.Enabled = false;
                ddlBenefName.Items.Clear();
                txtDrawNo.Text = "";
            }
            else
            {
                //DataTable loadPrizedMemberID = balayer.GetDataTable("SELECT concat(GrpMemberID,' , ',MemberName) as MemberName,concat(cast(Head_Id as char),'  , ',cast(MemberID as char)) as GrpMemberID FROM `svcf`.`membertogroupmaster` where GroupID=" + ddlChitNo.SelectedValue + " and Head_Id not in(select PrizedMemberID  from  `svcf`.`auctiondetails` where GroupID=" + ddlChitNo.SelectedValue + " and PrizedMemberID is not null)    order by   cast(digits(GrpMemberID)  as unsigned)");
                //DataTable loadPrizedMemberID = balayer.GetDataTable("SELECT concat(GrpMemberID,' , ',MemberName) as MemberName,concat(cast(Head_Id as char),'  , ',cast(MemberID as char)) as GrpMemberID FROM `svcf`.`membertogroupmaster` where GroupID=" + ddlChitNo.SelectedValue + "     order by   cast(digits(GrpMemberID)  as unsigned)");
                //DataTable loadPrizedMemberID = balayer.GetDataTable("SELECT concat(GrpMemberID,' , ',MemberName) as MemberName,concat(cast(Head_Id as char),'  , ',cast(MemberID as char)) as GrpMemberID FROM `svcf`.`membertogroupmaster` where GroupID=" + ddlChitNo.SelectedValue + " and Head_Id  in(select PrizedMemberID  from  `svcf`.`auctiondetails` where GroupID=" + ddlChitNo.SelectedValue + " and IsPrized in('N','I'))    order by   cast(digits(GrpMemberID)  as unsigned)");
                //ddlBenefName.DataSource = loadPrizedMemberID;
                //ddlBenefName.DataTextField = "MemberName";
                //ddlBenefName.DataValueField = "GrpMemberID";
                //ddlBenefName.DataBind();
                //ddlBenefName.Items.Insert(0, "--select--");
                //ddlBenefName.SelectedIndex = 0;
                ddlBenefName.Enabled = false;
                ddlAuction.Enabled = true;
                ddlDrawNo.Enabled = false;
                btnViewHistory.Enabled = true;
                txtRebiddate.Enabled = false;
                txtPrizamt.Enabled = false;
                txtDefaultInterset.Enabled = false;
                txtDrawNo.Enabled = false;
                txtDefaultInterset.Text = "";
                txtPrizamt.Text = "";
                txtRebiddate.Text = "";
                txtDrawNo.Text = "";
           //     ddlDrawNo.ClearSelection();
                ddlAuction.ClearSelection();
            }
        }
        protected void txtPrizamt_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtPrizamt.Text))
            {
                return;
            }
            if (string.IsNullOrEmpty(txtDefaultInterset.Text))
            {
                return;
            }
            int finalDrawNo = 0;
            finalDrawNo = balayer.GetScalarDataInt("select count(*) from svcf.membertogroupmaster where GroupID=" + Convert.ToInt32(ddlChitNo.SelectedValue) + "");


            decimal PrizedAmt;
            PrizedAmt = Convert.ToDecimal(balayer.MySQLEscapeString(txtPrizamt.Text));

            string selGroup = "select g.ChitValue,g.NoofMembers,c.Commission,c.IncidentalCharges from  groupmaster as g inner join commissiondetails as c on g.ChitValue=c.ChitValue and g.Commission_ID=c.Commission_ID where  g.head_ID=" + ddlChitNo.SelectedValue;
            DataTable selgrouptab = balayer.GetDataTable(selGroup);
            decimal chitvalue = selgrouptab.Rows[0].Field<decimal>("ChitValue");

            if (chitvalue < PrizedAmt)
            {
                Pnlmsg1.Visible = true;
                this.ModalPopup.PopupControlID = "Pnlmsg1";
                ModalPopup.Show();
                lblcon.Text = "Enter the prized amount less than chit value.";
                lblcon.ForeColor = System.Drawing.Color.Red;
                return;
            }

            int noofmem = Convert.ToInt32(selgrouptab.Rows[0]["NoofMembers"]);
            decimal commission = Convert.ToDecimal(selgrouptab.Rows[0]["Commission"]);
            decimal IncidentalCharge = Convert.ToDecimal(selgrouptab.Rows[0]["IncidentalCharges"]);
            string BrID = balayer.ToobjectstrEvenNull(Session["Branchid"]);

            decimal dividend, kasardAmt, nxtdueamt, defaultinterest;
            defaultinterest = Convert.ToDecimal(txtDefaultInterset.Text);
            kasardAmt = chitvalue - (Convert.ToDecimal(txtPrizamt.Text) + commission);
            dividend = ((chitvalue - Convert.ToDecimal(txtPrizamt.Text)) - (commission + defaultinterest)) / noofmem;
            nxtdueamt = (chitvalue / noofmem) - dividend;

            if (Convert.ToBoolean(txtDrawNo.Text == ""))
            {
                txtDrawNo.Text = "0";
            }
            if (Convert.ToInt32(txtDrawNo.Text) < finalDrawNo)
            {
                txtDueAmount.Text = nxtdueamt.ToString();
            }
            else
            {
                txtDueAmount.Text = "0";
            }
        }
        
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            decimal PrizedAmt;
            PrizedAmt = Convert.ToDecimal(balayer.MySQLEscapeString(txtPrizamt.Text));

            string selGroup = "select g.ChitValue,g.NoofMembers,c.Commission,c.IncidentalCharges from  groupmaster as g inner join commissiondetails as c on g.ChitValue=c.ChitValue and g.Commission_ID=c.Commission_ID where  g.head_ID=" + ddlChitNo.SelectedValue;
            DataTable selgrouptab = balayer.GetDataTable(selGroup);
            decimal chitvalue = selgrouptab.Rows[0].Field<decimal>("ChitValue");

            if (chitvalue < PrizedAmt)
            {
                Pnlmsg1.Visible = true;
                this.ModalPopup.PopupControlID = "Pnlmsg1";
                ModalPopup.Show();
                lblcon.Text = "Enter the prized amount less than chit value.";
                lblcon.ForeColor = System.Drawing.Color.Red;
                return;
            }

            int noofmem = Convert.ToInt32( selgrouptab.Rows[0]["NoofMembers"]);
            decimal commission =Convert.ToDecimal( selgrouptab.Rows[0]["Commission"]);
            decimal IncidentalCharge = Convert.ToDecimal(selgrouptab.Rows[0]["IncidentalCharges"]);
            string BrID = balayer.ToobjectstrEvenNull(Session["Branchid"]);

            decimal dividend, kasardAmt, nxtdueamt,defaultinterest;
            defaultinterest = Convert.ToDecimal(txtDefaultInterset.Text);
            kasardAmt = chitvalue - (Convert.ToDecimal(txtPrizamt.Text) + commission);
            dividend = ((chitvalue - Convert.ToDecimal(txtPrizamt.Text)) - (commission + defaultinterest) ) / noofmem;
            nxtdueamt = Convert.ToDecimal(txtDueAmount.Text);

            if ("New Auction" == balayer.ToobjectstrEvenNull(ddlAuction.SelectedItem))
            {
                TransactionLayer trn = new TransactionLayer();
                try
                {
                    string updateauction = "update auctiondetails set ReBidNO=0,MemberID=" + ddlBenefName.SelectedValue.Split(',')[1] + ",PrizedMemberID='" + balayer.MySQLEscapeString(balayer.ToobjectstrEvenNull(ddlBenefName.SelectedValue.Split(',')[0])) + "',PrizedAmount='" + PrizedAmt + "',DefaultInterest=" + txtDefaultInterset.Text + ",TotalCommission='" + commission + "',Dividend='" + dividend + "',KasarAmount='" + kasardAmt + "',NextDueAmount='" + nxtdueamt + "',IsPrized='N' where GroupID='" + balayer.MySQLEscapeString(balayer.ToobjectstrEvenNull(ddlChitNo.SelectedValue)) + "' and DrawNO=" + Convert.ToInt32(txtDrawNo.Text) + "";
                    long cmd = trn.insertorupdateTrn(updateauction);
                    string updateNxtDue = "update auctiondetails set CurrentDueAmount='" + nxtdueamt + "' where GroupID='" + balayer.MySQLEscapeString(balayer.ToobjectstrEvenNull(ddlChitNo.SelectedValue)) + "' and DrawNO='" + (Convert.ToInt32(txtDrawNo.Text) + 1) + "'";
                    long cmdUpd = trn.insertorupdateTrn(updateNxtDue);
                    trn.CommitTrn();
                    logger.Info("AuctionForms.aspx - btnAdd_Click() - New Auction: " + DateTime.Now + " by: " + Convert.ToString(Session["UserName"]) + "");
                }
                catch(Exception err)
                {
                   trn.RollbackTrn();
                   logger.Info("AuctionForms.aspx - btnAdd_Click() - Error: " + err.Message + ": " + DateTime.Now + " by: " + Convert.ToString(Session["UserName"]) + "");
                }
                finally
                {
                    trn.DisposeTrn();
                }
                
                clear();
            }
            else if ("Re-Bid" == balayer.ToobjectstrEvenNull(ddlAuction.SelectedItem))
            {
                try
                {
                    string rebid = balayer.GetSingleValue("select Rebiddate1 from svcf.auctiondetails where PrizedMemberID=" + ddlBenefName.SelectedValue.Split(',')[0].Trim() + " and GroupID=" + ddlChitNo.SelectedValue + "");
                    if(rebid=="")
                    {
                        DataTable dtDetails = balayer.GetDataTable("Select inccolumn,DATE_FORMAT(AuctionDate, '%d/%m/%Y') as AuctionDate,PrizedMemberID,MemberID,PrizedAmount from auctiondetails where DrawNO=" + ddlDrawNo.SelectedItem.Value + " and GroupID=" + ddlChitNo.SelectedValue);
                        //string strAuctionDetails = "update  `svcf`.`auctiondetails` set PrizedMemberID=" + ddlBenefName.SelectedValue.Split(',')[0].Trim() + "," +
                        //   "MemberID=" + ddlBenefName.SelectedValue.Split(',')[1].Trim() + ",IsReAuction=1,PrizedAmount=" + PrizedAmt + " ,DefaultInterest=" + txtDefaultInterset.Text + "," +
                        //   "TotalCommission='" + commission + "',Dividend=" + dividend + ",KasarAmount=" + kasardAmt + "  " +
                        //   "where DrawNO=" + txtDrawNo.Text + " and GroupID=" + ddlChitNo.SelectedValue; 

                        string strAuctionDetails = "update  `svcf`.`auctiondetails` set PrizedMemberID=" + ddlBenefName.SelectedValue.Split(',')[0].Trim() + "," +
                           "MemberID=" + ddlBenefName.SelectedValue.Split(',')[1].Trim() + ",IsReAuction=1,PrizedAmount=" + PrizedAmt + " ,DefaultInterest=" + txtDefaultInterset.Text + "," +
                           "TotalCommission='" + commission + "',Dividend=" + dividend + ",Rebiddate1='" + balayer.indiandateToMysqlDate(txtRebiddate.Text) + "',KasarAmount=" + kasardAmt + "  " +
                           "where DrawNO=" + ddlDrawNo.SelectedItem.Value + " and GroupID=" + ddlChitNo.SelectedValue;
                        trn.insertorupdateTrn(strAuctionDetails);

                        string oldAuctionDate = balayer.indiandateToMysqlDate(Convert.ToString(dtDetails.Rows[0]["AuctionDate"]));
                        string strreauctionparticulars = "INSERT INTO `svcf`.`reauctionparticulars` (`RefNo`, `OldAuctionDate`,`OldPrizedValue`,`OldPrizedToken`, `OldPrizedMember`, `GroupID`, `BranchId`) VALUES (" + balayer.ToobjectstrEvenNull(dtDetails.Rows[0]["inccolumn"]) + ",'" + oldAuctionDate + "'," + balayer.ToobjectstrEvenNull(dtDetails.Rows[0]["PrizedAmount"]) + "," + balayer.ToobjectstrEvenNull(dtDetails.Rows[0]["PrizedMemberID"]) + "," + balayer.ToobjectstrEvenNull(dtDetails.Rows[0]["MemberID"]) + "," + ddlChitNo.SelectedValue + "," + Session["Branchid"].ToString() + ")";
                        trn.insertorupdateTrn(strreauctionparticulars);
                        trn.CommitTrn();
                        logger.Info("AuctionForms.aspx - btnAdd_Click() - Re-big: Completed -  " + DateTime.Now + " by: " + Convert.ToString(Session["UserName"]) + "");
                    }
                    else
                    {
                        DataTable dtDetails = balayer.GetDataTable("Select inccolumn,DATE_FORMAT(AuctionDate, '%d/%m/%Y') as AuctionDate,PrizedMemberID,MemberID,PrizedAmount from auctiondetails where DrawNO=" + ddlDrawNo.SelectedItem.Value + " and GroupID=" + ddlChitNo.SelectedValue);
                        //string strAuctionDetails = "update  `svcf`.`auctiondetails` set PrizedMemberID=" + ddlBenefName.SelectedValue.Split(',')[0].Trim() + "," +
                        //   "MemberID=" + ddlBenefName.SelectedValue.Split(',')[1].Trim() + ",IsReAuction=1,PrizedAmount=" + PrizedAmt + " ,DefaultInterest=" + txtDefaultInterset.Text + "," +
                        //   "TotalCommission='" + commission + "',Dividend=" + dividend + ",KasarAmount=" + kasardAmt + "  " +
                        //   "where DrawNO=" + txtDrawNo.Text + " and GroupID=" + ddlChitNo.SelectedValue; 

                        string strAuctionDetails = "update  `svcf`.`auctiondetails` set PrizedMemberID=" + ddlBenefName.SelectedValue.Split(',')[0].Trim() + "," +
                           "MemberID=" + ddlBenefName.SelectedValue.Split(',')[1].Trim() + ",IsReAuction=1,PrizedAmount=" + PrizedAmt + " ,DefaultInterest=" + txtDefaultInterset.Text + "," +
                           "TotalCommission='" + commission + "',Dividend=" + dividend + ",Rebiddate2='" + balayer.indiandateToMysqlDate(txtRebiddate.Text) + "',KasarAmount=" + kasardAmt + "  " +
                           "where DrawNO=" + ddlDrawNo.SelectedItem.Value + " and GroupID=" + ddlChitNo.SelectedValue;
                        trn.insertorupdateTrn(strAuctionDetails);

                        string oldAuctionDate = balayer.indiandateToMysqlDate(Convert.ToString(dtDetails.Rows[0]["AuctionDate"]));
                        string strreauctionparticulars = "INSERT INTO `svcf`.`reauctionparticulars` (`RefNo`, `OldAuctionDate`,`OldPrizedValue`,`OldPrizedToken`, `OldPrizedMember`, `GroupID`, `BranchId`) VALUES (" + balayer.ToobjectstrEvenNull(dtDetails.Rows[0]["inccolumn"]) + ",'" + oldAuctionDate + "'," + balayer.ToobjectstrEvenNull(dtDetails.Rows[0]["PrizedAmount"]) + "," + balayer.ToobjectstrEvenNull(dtDetails.Rows[0]["PrizedMemberID"]) + "," + balayer.ToobjectstrEvenNull(dtDetails.Rows[0]["MemberID"]) + "," + ddlChitNo.SelectedValue + "," + Session["Branchid"].ToString() + ")";
                        trn.insertorupdateTrn(strreauctionparticulars);
                        trn.CommitTrn();
                        logger.Info("AuctionForms.aspx - btnAdd_Click() - Re-big: Completed -  " + DateTime.Now + " by: " + Convert.ToString(Session["UserName"]) + "");
                    }
                   
                }
                catch(Exception err)
                {
                    trn.RollbackTrn();
                    logger.Info("AuctionForms.aspx - btnAdd_Click() - Re-Bid: Error: " + err.Message + ": " + DateTime.Now + " by: " + Convert.ToString(Session["UserName"]) + "");
                }
                finally
                {
                    trn.DisposeTrn();
                }
            }
            Pnlmsg1.Visible = true;
            this.ModalPopup.PopupControlID = "Pnlmsg1";
            ModalPopup.Show();
            lblcon.Text = "Auction details added successfully.";
            lblcon.ForeColor = System.Drawing.Color.Green;
            clear();
        }

        protected void btnViewHistory_Click(object sender, EventArgs e)
        {
            if (ddlChitNo.SelectedIndex < 1)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Warning", "alert('Choose chit group.');", true);
                return;
            }
            string viewHistory = "select date_format(`AuctionDate`,'%d/%m/%Y') as AuctionDate,`DrawNO`,`Node`,`PrizedAmount`,`TotalCommission`,`Dividend`,`KasarAmount`,`CurrentDueAmount`,`NextDueAmount`,`AdditionalKasarAmount` from auctiondetails join headstree on (auctiondetails.PrizedMemberID=headstree.NodeID) where GroupID=" + balayer.ToobjectstrEvenNull(ddlChitNo.SelectedItem.Value) + " and IsPrized<>'I' and AuctionDate between date(AuctionDate) and curdate()";
            DataTable tempTable = balayer.GetDataTable(viewHistory);
            tempTable.DefaultView.Sort = "DrawNO";
            gridHistory.AutoGenerateColumns = true;
            gridHistory.DataSource = tempTable;
            gridHistory.DataBind();
            ModalPopup.Show();
            panchitHistory.Visible = true;
            gridHistory.Visible = true;
            lbl.Visible = true;
        }

        protected void BtnOK_Click(object sender, EventArgs e)
        {
            lbl.Visible = false;
            gridHistory.Visible = false;
            panchitHistory.Visible = false;
            ModalPopup.Hide();
        }
        private void clear()
        {
            txtDrawNo.Text = "";
            txtPrizamt.Text = "";
            txtDefaultInterset.Text = "";
            ddlBenefName.SelectedIndex = 0;
            ddlChitNo.SelectedIndex = 0;
            ddlAuction.SelectedIndex = 0;
            ddlBenefName.Enabled = false;
            ddlAuction.Enabled = false;
            txtRebiddate.Text = "";
            txtDueAmount.Text = "";
        }

        public string ReacutionValue(object xcv)
        {
            if (balayer.ToobjectstrEvenNull(xcv).Trim() =="1")
            {
                return "yes";
            }
            else
            {
                return "no";
            }
        }

        //protected void ddlAuction_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    int DrawNo;
        //    if ("New Auction".Equals(balayer.ToobjectstrEvenNull(ddlAuction.SelectedItem)))
        //    {
        //        lblRebiddate.Visible = false;
        //        txtRebiddate.Visible = false;
        //        txtDrawNo.ReadOnly = true;
        //        ddlBenefName.SelectedIndex = 0;
        //        txtPrizamt.Text = "";
        //        string seldraw = "select min(DrawNO) from auctiondetails where GroupID='" + balayer.ToobjectstrEvenNull(ddlChitNo.SelectedValue) + "' and IsPrized='I' ";
        //        DrawNo = Convert.ToInt32(balayer.GetSingleValue(seldraw));
        //        txtDrawNo.Text = balayer.ToobjectstrEvenNull(DrawNo);
        //        lblErrorMsg.Visible = false;
        //        ddlBenefName.Enabled = true;
        //        txtRebiddate.Enabled = true;
        //        txtPrizamt.Enabled = true;
        //        txtDrawNo.Enabled = true;
        //        txtDefaultInterset.Enabled = true;
        //        // ddlDrawNo.Enabled = false;
        //        ddlDrawNo.Visible = false;
        //        Label5.Visible = false;
        //    }
        //    else
        //        if ("Re-Bid".Equals(balayer.ToobjectstrEvenNull(ddlAuction.SelectedItem)))
        //    {
        //        // txtDrawNo.ReadOnly = true;
        //        lblRebiddate.Visible = true;
        //        txtRebiddate.Visible = true;
        //        ddlBenefName.SelectedIndex = 0;
        //        txtPrizamt.Text = "";
        //        string RebidQuery = "select min(DrawNO) from auctiondetails where GroupID=" + balayer.ToobjectstrEvenNull(ddlChitNo.SelectedValue) + " and IsPrized='N' and AuctionDate between date(AuctionDate) and curdate()";
        //        string DrNo = balayer.ToobjectstrEvenNull(balayer.GetSingleValue(RebidQuery));
        //        if ("".Equals(DrNo))
        //        {
        //            lblErrorMsg.Visible = true;
        //            ddlBenefName.Enabled = false;
        //            txtRebiddate.Enabled = false;
        //            txtPrizamt.Enabled = false;
        //            txtDefaultInterset.Enabled = false;
        //            txtDrawNo.Visible = false;
        //            ddlDrawNo.Enabled = false;
        //        }
        //        else
        //        {

        //            //txtDrawNo.Visible = false;
        //            //lblDrawNo.Visible = false;
        //            //Label5.Enabled = true;
        //            //ddlDrawNo.Enabled = true;
        //            //Label5.Visible = true;
        //            //ddlDrawNo.Visible = true;
        //            //ddlBenefName.Enabled = true;
        //            //txtRebiddate.Enabled = true;
        //            //txtPrizamt.Enabled = true;
        //            //txtDefaultInterset.Enabled = true;
        //            //ddlDrawNo.DataSource = null;
        //            //ddlDrawNo.DataBind();
        //            //DataTable dt = balayer.GetDataTable("select  DrawNO from auctiondetails where GroupID=" + balayer.ToobjectstrEvenNull(ddlChitNo.SelectedValue) + " and IsPrized='N' and AuctionDate between date(AuctionDate) and curdate()");
        //            //DataRow dr = dt.NewRow();
        //            ////dr[0] = "--Select--";
        //            ////   dr[0] = "";
        //            ////dt.Rows.InsertAt(dr, 0);
        //            //dt.Rows.InsertAt(dr, 0);
        //            //ddlDrawNo.DataSource = dt;
        //            ////   ddlDrawNo.DataTextField = "IsPrized";
        //            //ddlDrawNo.DataValueField = "DrawNO";
        //            //ddlDrawNo.DataBind();


        //        }

        //    }
        //    else
        //            if ("--select--".Equals(balayer.ToobjectstrEvenNull(ddlAuction.SelectedItem)))
        //    {
        //        txtDrawNo.Text = "";
        //        ddlBenefName.SelectedIndex = 0;
        //        txtPrizamt.Text = "";
        //        txtRebiddate.Text = "";
        //        txtDefaultInterset.Text = "";
        //        ddlBenefName.Enabled = false;
        //    }
        //}
        protected void ddlAuction_SelectedIndexChanged(object sender, EventArgs e)
        {
            int DrawNo;
            if ("New Auction".Equals(balayer.ToobjectstrEvenNull(ddlAuction.SelectedItem)))
            {
                lblRebiddate.Visible = false;
                txtRebiddate.Visible = false;
                txtDrawNo.ReadOnly = true;
             //   ddlBenefName.SelectedIndex = 0;
                txtPrizamt.Text = "";
                string seldraw = "select min(DrawNO) from auctiondetails where GroupID='" + balayer.ToobjectstrEvenNull(ddlChitNo.SelectedValue) + "' and IsPrized='I' ";
                DrawNo = Convert.ToInt32(balayer.GetSingleValue(seldraw));
                txtDrawNo.Text = balayer.ToobjectstrEvenNull(DrawNo);
                lblErrorMsg.Visible = false;
                ddlBenefName.Enabled = true;
                txtRebiddate.Enabled = true;
                txtPrizamt.Enabled = true;
                txtDrawNo.Enabled = true;
                txtDefaultInterset.Enabled = true;
                ddlDrawNo.Enabled = false;


                //DataTable loadPrizedMemberID = balayer.GetDataTable("SELECT concat(GrpMemberID,' , ',MemberName) as MemberName,concat(cast(Head_Id as char),'  , ',cast(MemberID as char)) as GrpMemberID FROM `svcf`.`membertogroupmaster` where GroupID=" + ddlChitNo.SelectedValue + " and Head_Id not in(select PrizedMemberID  from  `svcf`.`auctiondetails` where GroupID=" + ddlChitNo.SelectedValue + " and PrizedMemberID is not null)    order by   cast(digits(GrpMemberID)  as unsigned)");
               DataTable loadPrizedMemberID = balayer.GetDataTable("SELECT concat(GrpMemberID,' , ',MemberName) as MemberName,concat(cast(Head_Id as char),'  , ',cast(MemberID as char)) as GrpMemberID FROM `svcf`.`membertogroupmaster` where GroupID=" + ddlChitNo.SelectedValue + "     order by   cast(digits(GrpMemberID)  as unsigned)");
               // DataTable loadPrizedMemberID = balayer.GetDataTable("SELECT concat(GrpMemberID,' , ',MemberName) as MemberName,concat(cast(Head_Id as char),'  , ',cast(MemberID as char)) as GrpMemberID FROM `svcf`.`membertogroupmaster` where GroupID=" + ddlChitNo.SelectedValue + " and Head_Id  in(select PrizedMemberID  from  `svcf`.`auctiondetails` where GroupID=" + ddlChitNo.SelectedValue + " and IsPrized in('N','I'))    order by   cast(digits(GrpMemberID)  as unsigned)");
                ddlBenefName.DataSource = loadPrizedMemberID;
                ddlBenefName.DataTextField = "MemberName";
                ddlBenefName.DataValueField = "GrpMemberID";
                ddlBenefName.DataBind();
                ddlBenefName.Items.Insert(0, "--select--");
                ddlBenefName.SelectedIndex = 0;

            }
            else
                if ("Re-Bid".Equals(balayer.ToobjectstrEvenNull(ddlAuction.SelectedItem)))
            {
                txtDrawNo.ReadOnly = true;
                lblRebiddate.Visible = true;
                txtRebiddate.Visible = true;
              //  ddlBenefName.SelectedIndex = 0;
                txtPrizamt.Text = "";
                string RebidQuery = "select min(DrawNO) from auctiondetails where GroupID=" + balayer.ToobjectstrEvenNull(ddlChitNo.SelectedValue) + " and IsPrized='N' and AuctionDate between date(AuctionDate) and curdate()";


                //DataTable loadPrizedMemberID = balayer.GetDataTable("SELECT concat(GrpMemberID,' , ',MemberName) as MemberName,concat(cast(Head_Id as char),'  , ',cast(MemberID as char)) as GrpMemberID FROM `svcf`.`membertogroupmaster` where GroupID=" + ddlChitNo.SelectedValue + " and Head_Id not in(select PrizedMemberID  from  `svcf`.`auctiondetails` where GroupID=" + ddlChitNo.SelectedValue + " and PrizedMemberID is not null)    order by   cast(digits(GrpMemberID)  as unsigned)");
               DataTable loadPrizedMemberID = balayer.GetDataTable("SELECT concat(GrpMemberID,' , ',MemberName) as MemberName,concat(cast(Head_Id as char),'  , ',cast(MemberID as char)) as GrpMemberID FROM `svcf`.`membertogroupmaster` where GroupID=" + ddlChitNo.SelectedValue + "     order by   cast(digits(GrpMemberID)  as unsigned)");
                  // DataTable loadPrizedMemberID = balayer.GetDataTable("SELECT concat(GrpMemberID,' , ',MemberName) as MemberName,concat(cast(Head_Id as char),'  , ',cast(MemberID as char)) as GrpMemberID FROM `svcf`.`membertogroupmaster` where GroupID=" + ddlChitNo.SelectedValue + " and Head_Id  in(select PrizedMemberID  from  `svcf`.`auctiondetails` where GroupID=" + ddlChitNo.SelectedValue + " and IsPrized in('N','I'))    order by   cast(digits(GrpMemberID)  as unsigned)");
                ddlBenefName.DataSource = loadPrizedMemberID;
                ddlBenefName.DataTextField = "MemberName";
                ddlBenefName.DataValueField = "GrpMemberID";
                ddlBenefName.DataBind();
                ddlBenefName.Items.Insert(0, "--select--");
                ddlBenefName.SelectedIndex = 0;


                string DrNo = balayer.ToobjectstrEvenNull(balayer.GetSingleValue(RebidQuery));
                if ("".Equals(DrNo))
                {
                    lblErrorMsg.Visible = true;
                    ddlBenefName.Enabled = false;
                    txtRebiddate.Enabled = false;
                    txtPrizamt.Enabled = false;
                    txtDefaultInterset.Enabled = false;
                    txtDrawNo.Enabled = false;
                   ddlDrawNo.Enabled = false;
                }
                else
                {
                    //txtDrawNo.Text = DrNo;
                    txtDrawNo.Text = "";
                    //txtDrawNo.Enabled = true;
                    ddlDrawNo.Enabled = true;
                    ddlBenefName.Enabled = true;
                    txtRebiddate.Enabled = true;
                    txtPrizamt.Enabled = true;
                    txtDefaultInterset.Enabled = true;
                    ddlDrawNo.DataSource = null;
                    //ddlDrawNo.DataBind();
                    DataTable dt = balayer.GetDataTable("select  DrawNO from auctiondetails where GroupID=" + balayer.ToobjectstrEvenNull(ddlChitNo.SelectedValue) + " and IsPrized='N' and AuctionDate between date(AuctionDate) and curdate()");
                    DataRow dr = dt.NewRow();
                    //dr[0] = "--Select--";
                    //   dr[0] = "";
                    //dt.Rows.InsertAt(dr, 0);
                    dt.Rows.InsertAt(dr, 0);
                    ddlDrawNo.DataSource = dt;
                    //   ddlDrawNo.DataTextField = "IsPrized";
                    ddlDrawNo.DataValueField = "DrawNO";
                    ddlDrawNo.DataBind();
                }
              
            }
            else
                    if ("--select--".Equals(balayer.ToobjectstrEvenNull(ddlAuction.SelectedItem)))
            {
                txtDrawNo.Text = "";
                ddlBenefName.SelectedIndex = 0;
                txtPrizamt.Text = "";
                txtRebiddate.Text = "";
                txtDefaultInterset.Text = "";
                ddlBenefName.Enabled = false;
            }
        }
    }
}
