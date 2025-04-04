using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SVCF_BusinessAccessLayer;
using SVCF_DataAccessLayer;
using SVCF_TransactionLayer;
using log4net;
using log4net.Config;
namespace SreeVisalamChitFundLtd_phase1
{
    public partial class Redraw : System.Web.UI.Page
    {

        #region Object
        BusinessLayer balayer = new BusinessLayer();
        TransactionLayer trn = new TransactionLayer();
        #endregion

        ILog logger = log4net.LogManager.GetLogger(typeof(Redraw));
        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (Session["UserName"] == null || Session["Branchid"] == null || Session["BranchName"] == null)
            {
                Response.Redirect(Page.ResolveUrl("~/Login.aspx"), true);
            }
        }
        DataTable dtmember = new DataTable();
        string r1, r2, r3;
        protected void Page_PreRender(object sender, EventArgs e)
        {
            ViewState["CheckRefresh"] = Session["CheckRefresh"];
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                var userinfo = HttpContext.Current.User.Identity.Name;
                var qry = "select rs.name from roles as rs inner join rights as rt on (rt.roleid=rs.id) where memberid=" + userinfo + "";
                var usrRole = balayer.GetSingleValue(qry);
                if (usrRole == "Report")
                {
                    Response.Redirect("Home.aspx", false);
                }
                
                Session["CheckRefresh"] =
                   Server.UrlDecode(System.DateTime.Now.ToString());
                GetGroups();
                //rvDate.MinimumValue = balayer.GetSingleValue("SELECT DATE_FORMAT( min(ChoosenDate), '%d/%m/%Y') MinimumDate FROM svcf.voucher where BranchID=" + Session["Branchid"]);
             
            }
            rvDate.MinimumValue = balayer.GetSingleValue("SELECT DATE_FORMAT( min(ChoosenDate), '%d/%m/%Y') MinimumDate FROM svcf.voucher where " +
                 "BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and ChoosenDate<>'0000-00-00'");
            rvDate.MaximumValue = DateTime.Now.ToString("dd/MM/yyyy");
  

        }

        protected void GetGroups()
        {
            Session["CheckRefresh"] = System.Guid.NewGuid().ToString();
            DataTable dtChit = balayer.GetDataTable("SELECT GroupNO,Head_Id FROM svcf.groupmaster where BranchId=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " order by Head_Id");
            DataRow drChit = dtChit.NewRow();
            drChit[0] = "--Select--";
            //drChit[1] = "0";
            ddlGroupNumber.DataSource = dtChit;
            ddlGroupNumber.DataTextField = "GroupNO";
            ddlGroupNumber.DataValueField = "Head_Id";
            dtChit.Rows.InsertAt(drChit, 0);
            ddlGroupNumber.DataBind();

        }



        protected void load_ddlGroupNumber(object sender, EventArgs e)
        {

            dtmember = balayer.GetDataTable("SELECT  Head_Id, concat(membertogroupmaster.GrpMemberID,'=', membertogroupmaster.MemberName) as MemberName from svcf.membertogroupmaster  where membertogroupmaster.GroupID=" + ddlGroupNumber.SelectedValue + "");
            DataRow drChit = dtmember.NewRow();
            // drChit[0] = "--Select--";
            // drChit[1] = "0";
            ddlMemberName.DataSource = dtmember;
            ddlMemberName.DataTextField = "MemberName";
            ddlMemberName.DataValueField = "Head_Id";
            dtmember.Rows.InsertAt(drChit, 0);
            ddlMemberName.DataBind();
        }

        protected void select()
        {
            Page.Validate("pa");
            bool isVal = true;
            TransactionLayer trn = new TransactionLayer();
            try
            {
                #region VarDeclaration
                string qry = "";
                int OldprizedMembId=0;
                string guidForChar36 = "";
                string hexstring = "";
                string guidForBinary16 = "";
                string DualTransactionKey = "";
                string date = "";
                string vn, vt, nomebers, ser, admin, trans1, Medium, RootID1, Other1, chitkasar;
                int kasramt, nomeb, totamt;
                DataTable grpno;
                string insertvoucher = "";
                #endregion
                DataTable dtmembers = balayer.GetDataTable("SELECT  MemberID,Head_Id, GrpMemberID from svcf.membertogroupmaster  where membertogroupmaster.GroupID=" + ddlGroupNumber.SelectedValue + "");
                insertvoucher = "";
                for (int j = 0; j < dtmembers.Rows.Count; j++)
                {                   
                    r1 = dtmembers.Rows[j]["GrpMemberID"].ToString();
                    r2 = dtmembers.Rows[j]["Head_Id"].ToString();
                    r3 = dtmembers.Rows[j]["MemberID"].ToString();
                    chitkasar = txtNarration.Text;

                    System.Guid guid = Guid.NewGuid();
                    guidForChar36 = guid.ToString();
                    hexstring = BitConverter.ToString(guid.ToByteArray());
                    guidForBinary16 = "0x" + hexstring.Replace("-", string.Empty);
                    DualTransactionKey = guidForBinary16;
                    date = DateTime.Now.ToString("YYYY-mm-dd");
                    vn = "0";
                    vt = "C";
                    grpno = balayer.GetDataTable("SELECT Head_Id,NoofMembers FROM svcf.groupmaster where Head_Id=" + ddlGroupNumber.SelectedValue + "");
                     nomebers = grpno.Rows[0]["NoofMembers"].ToString();
                    kasramt = Convert.ToInt32(txtkasarAmount.Text);
                    nomeb = Convert.ToInt32(nomebers);
                    totamt = kasramt / nomeb;
                    ser = "PAYMENT";
                    admin = "admin";
                    trans1 = "1";
                    Medium = "0";
                    RootID1 = "5";
                    Other1 = "5";
                    insertvoucher = insertvoucher + "insert into  svcf.voucher(`DualTransactionKey`,`BranchID`,`CurrDate`,`Voucher_No`,`Voucher_Type`,`Head_Id`,`ChoosenDate`,`Narration`,`Amount`,`Series`,`ReceievedBy`,`Trans_Type`,`T_Day`,`T_Month`,`T_Year`,`MemberID`,`Trans_Medium`,`RootID`,`ChitGroupId`,`Other_Trans_Type`) values (" + DualTransactionKey + "," + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " ,'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + vn + "','" + vt + "','" + r2 + "','" + Convert.ToDateTime(txtDate.Text).ToString("yyyy-MM-dd") + "','" + chitkasar + "','" + totamt + "','" + ser + "','" + admin + "','" + trans1 + "','" + balayer.indiandateToMysqlDate(txtDate.Text).Split('/')[2] + "','" + balayer.indiandateToMysqlDate(txtDate.Text).Split('/')[1] + "','" + balayer.indiandateToMysqlDate(txtDate.Text).Split('/')[0] + "','" + r3 + "','" + Medium + "','" + RootID1 + "','" + ddlGroupNumber.SelectedValue + "','" + Other1 + "');";
                                      
                    //trn.insertorupdateTrn("insert into  svcf.Redraw(`DualTransactionKey`,`BranchID`,`Current_Date`,`Voucher_No`,`Voucher_Type`,`Head_Id`,`ChoosenDate`,`Narration`,`Amount`,`Series`,`ReceievedBy`,`Trans_Type`,`T_Day`,`T_Month`,`T_Year`,`MemberID`,`Trans_Medium`,`RootID`,`ChitGroupId`,`Other_Trans_Type`) values (" + DualTransactionKey + "," + Session["Branchid"] + " ,'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + vn + "','" + vt + "','" + r2 + "','" + Convert.ToDateTime(txtDate.Text).ToString("yyyy-MM-dd") + "','" + chitkasar + "','" + totamt + "','" + ser + "','" + admin + "','" + trans1 + "','" + balayer.indiandateToMysqlDate(txtDate.Text).Split('/')[2] + "','" + balayer.indiandateToMysqlDate(txtDate.Text).Split('/')[1] + "','" + balayer.indiandateToMysqlDate(txtDate.Text).Split('/')[0] + "','" + r3 + "','" + Medium + "','" + RootID1 + "','" + ddlGroupNumber.SelectedValue + "','" + Other1 + "')");                  
                }

                if (insertvoucher != "")
                {
                    trn.insertorupdateTrn(insertvoucher);
                }
                insertvoucher = "";
                qry="select PrizedMemberID from auctiondetails where branchid=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and GroupId=" + ddlGroupNumber.SelectedValue + " and DrawNO=" + txtReauctionNo.Text + "";
                OldprizedMembId = balayer.GetScalarDataInt(qry);

                qry = "update auctiondetails set IsReAuction=1 where branchid=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and GroupId=" + ddlGroupNumber.SelectedValue + " and DrawNO=" + txtReauctionNo.Text + "";
                balayer.GetInsertItem(qry);

                //BranchID, Current_Date, PrizedHead_Id, ChoosenDate, Narration, Amount, ChitGroupId, RedrawPrizedHeadid, RedrawAuctionNo
                qry = "insert into Redraw(BranchID, Cur_Date, OldPrizedHead_Id, ChoosenDate, Narration, PrizedAmount, ChitGroupId, RedrawPrizedHeadid, RedrawAuctionNo,KasarAmount,Totalcommision) " +
                    "values (" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " ,'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," + OldprizedMembId + ", " +
                    " '" + Convert.ToDateTime(txtDate.Text).ToString("yyyy-MM-dd") + "','" + txtNarration.Text + "'," + txtAmount.Text + ", " +
                    "" + ddlGroupNumber.SelectedValue + "," + ddlMemberName.SelectedValue + "," + txtReauctionNo.Text + "," + txtkasarAmount.Text + "," + TxtCommission.Text  +")";
                balayer.GetInsertItem(qry);

                qry = "";                
                trn.CommitTrn();
                logger.Info("Redraw.aspx - select():  Completed: " + DateTime.Now + " by: " + Convert.ToString(Session["UserName"]) + "");
                // Response.Redirect(Request.Url.AbsoluteUri);
            }

            catch (Exception ex)
            {
                trn.RollbackTrn();
                logger.Info("Redraw.aspx - select():  Error:  " + ex.Message + ": " + DateTime.Now + " by: " + Convert.ToString(Session["UserName"]) + "");
                ModalPopupExtender1.PopupControlID = "pnlmsg";
                ModalPopupExtender1.Show();
                pnlmsg.Visible = true;
                lblh.Text = "Status";
                lblcon.Text = ex.Message;
                lblcon.ForeColor = System.Drawing.Color.Red;
                isVal = false;                               
            }
            finally
            {
                if (isVal)
                {
                    ModalPopupExtender1.PopupControlID = "pnlmsg";
                    ModalPopupExtender1.Show();
                    pnlmsg.Visible = true;
                    lblh.Text = "Status";
                    lblcon.Text = ddlGroupNumber.SelectedItem.Text + " - Redraw Add Successfully";
                    lblcon.ForeColor = System.Drawing.Color.Green;
                }
            }

        }

        protected void btnPayment_Click(object sender, EventArgs e)
        {
            select();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.Url.AbsolutePath.ToString());
        }

        protected void btnOK_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.Url.AbsolutePath.ToString());
        }

    }
    }
