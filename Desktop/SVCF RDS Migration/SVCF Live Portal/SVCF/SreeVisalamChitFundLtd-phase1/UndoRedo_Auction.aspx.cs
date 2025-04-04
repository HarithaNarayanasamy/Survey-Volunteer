using System;
using System.Collections;
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
using MySql.Data.MySqlClient;
using SVCF_BusinessAccessLayer;
using SVCF_DataAccessLayer;
using SVCF_TransactionLayer;
using log4net;
using log4net.Config;

namespace SreeVisalamChitFundLtd_phase1
{
    public partial class UndoRedo_Auction : System.Web.UI.Page
    {
        #region Object
        BusinessLayer balayer = new BusinessLayer();
        TransactionLayer trn = new TransactionLayer();
        #endregion
        ILog logger = log4net.LogManager.GetLogger(typeof(UndoRedo_Auction));

        protected void Page_Load(object sender, EventArgs e)
        {
            Pnlundo.Visible = false;
            if (!IsPostBack)
            {
                var userinfo = HttpContext.Current.User.Identity.Name;
                var qry = "select rs.name from roles as rs inner join rights as rt on (rt.roleid=rs.id) where memberid=" + userinfo + "";
                var usrRole = balayer.GetSingleValue(qry);
                if (usrRole == "Report")
                {
                    Response.Redirect("Home.aspx", false);
                }

                DataTable dt = balayer.GetDataTable("SELECT Distinct GROUPNO,Head_Id FROM svcf.groupmaster where BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and isFinished='0'");
                ddlGroupNo.DataSource = dt;
                ddlGroupNo.DataValueField = "Head_Id";
                ddlGroupNo.DataTextField = "GROUPNO";
                ddlGroupNo.DataBind();
            }
        }
        protected void btnload_Click(object sender, EventArgs e)
        {
            string cmd = "Select Max(DrawNO) from svcf.auctiondetails where GroupID=" + ddlGroupNo.SelectedValue + " and  PrizedMemberID is not null and IsPrized<>'Y'";
            string drawNo = balayer.GetSingleValue(cmd);
            string drawDate = balayer.GetSingleValue("select date_format(max(AuctionDate),'%d/%m/%Y') from `auctiondetails` where GroupID=" + ddlGroupNo.SelectedValue + " and  PrizedMemberID is not null and IsPrized<>'Y'");
            Pnlundo.Visible = true;
            if (string.IsNullOrEmpty(drawNo))
            {
                lblHint.Text = "Confirm1";
                lblContent.Text = "Undo payment! Then only you can undo an auction.";
                lblContent.ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                lblHint.Text = "Confirm";
                if (drawNo.EndsWith("1"))
                {
                    drawNo = drawNo + "st";
                }
                else if (drawNo.EndsWith("2"))
                {
                    drawNo = drawNo + "nd";
                }
                else if (drawNo.EndsWith("3"))
                {
                    drawNo = drawNo + "rd";
                }
                else
                {
                    drawNo = drawNo + "th";
                }

                lblContent.Text = "Are you sure you want to undo auction. </br> Held on :" + drawDate + "</br> Draw number:" + drawNo + "</br> Chit number :" + ddlGroupNo.SelectedItem.Text + "???";
                lblContent.ForeColor = System.Drawing.Color.Green;
            }
            ModalPopupExtender1.PopupControlID = "Pnlundo";
            this.ModalPopupExtender1.Show();
            Pnlundo.Visible = true;
            lblHeading.Text = "Status";
        }
        protected void btnundo_Click(object sender, EventArgs e)
        {
            if (lblHint.Text == "Confirm")
            {
                string cmd = "Select Max(DrawNO) from svcf.auctiondetails where GroupID=" + ddlGroupNo.SelectedValue + " and  PrizedMemberID is not null and IsPrized<>'Y'";
                string drawNo = balayer.GetSingleValue(cmd);
                string cmd1 = "Select Max(DrawNO)+1 from svcf.auctiondetails where GroupID=" + ddlGroupNo.SelectedValue + " and  PrizedMemberID is not null and IsPrized<>'Y'";
                string drawNo1 = balayer.GetSingleValue(cmd1);
                bool isVal = true;
                TransactionLayer trn = new TransactionLayer();
                try
                {
                    long result = trn.insertorupdateTrn("Update svcf.auctiondetails set MemberID=null,PrizedMemberID=null,PrizedAmount=0.00,TotalCommission=0.00,Dividend=0.00,NextDueAmount=0.00,KasarAmount=0.00,IsPrized='I',Rebiddate1=null,Rebiddate2=null where GroupID=" + ddlGroupNo.SelectedValue + " and DrawNO='" + drawNo + "'");
                    long result1 = trn.insertorupdateTrn("Update svcf.auctiondetails set CurrentDueAmount=0.00 where GroupID=" + ddlGroupNo.SelectedValue + " and DrawNO='" + drawNo1 + "'");
                    trn.CommitTrn();
                    logger.Info("undoRedo_Auction.aspx - load_btnconfirm():  Completed: " + DateTime.Now + " by: " + Convert.ToString(Session["UserName"]) + "");
                }
                catch (Exception ex)
                {
                   trn.RollbackTrn();
                    logger.Info("undoRedo_Auction.aspx - load_btnconfirm():  Error: " + ex.Message + ":  " + DateTime.Now + " by: " + Convert.ToString(Session["UserName"]) + "");
                    Pnlundo.Visible = true;
                    ModalPopupExtender1.PopupControlID = "Pnlundo";
                    this.ModalPopupExtender1.Show();
                    lblHeading.Text = "Status";
                    lblContent.Text = ex.Message;
                    lblContent.ForeColor = System.Drawing.Color.Red;
                    isVal = false;
                }
                finally
                {
                    trn.DisposeTrn();
                    lblHint.Text = "";
                    if (isVal)
                    {
                        Pnlundo.Visible = true;
                        ModalPopupExtender1.PopupControlID = "Pnlundo";
                        this.ModalPopupExtender1.Show();
                        lblHeading.Text = "Status";
                        lblContent.Text = drawNo + " Action of chit number : " + ddlGroupNo.SelectedItem.Text + " has been undo successfully.";
                        lblContent.ForeColor = System.Drawing.Color.Green;

                    }
                }
            }
            else
            {
                Pnlundo.Visible = false;
                this.ModalPopupExtender1.Hide();
            }
        }
        protected void btnNo_Click(object sender, EventArgs e)
        {
            if (lblHint.Text != "Confirm")
            {
                Response.Redirect(Request.Url.AbsolutePath);
            }
        }
    }
}
