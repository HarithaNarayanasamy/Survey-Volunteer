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
using System.Text.RegularExpressions;
using SVCF_BusinessAccessLayer;
using SVCF_TransactionLayer;
using log4net;
using log4net.Config;

namespace SreeVisalamChitFundLtd_phase1
{
    public partial class ReceivedAdvices : System.Web.UI.Page
    {
        #region VarDeclaration
       // CommonClassFile objcls = new CommonClassFile();
        #endregion

        #region VarDeclaration
        BusinessLayer balayer = new BusinessLayer();
        TransactionLayer tranlayer = new TransactionLayer();
        int hdid = 0;
        #endregion

        ILog logger = log4net.LogManager.GetLogger(typeof(ReceivedAdvices));

        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (Session["UserName"] == null || Session["Branchid"] == null || Session["BranchName"] == null)
            {
                Response.Redirect(Page.ResolveUrl("~/Login.aspx"), true);
            }
        }
        protected void btnRejectCancel_Click(object sender, EventArgs e)
        {
        }
        protected void btnRejectOK_Click(object sender, EventArgs e)
        {
        }
        protected void btnMsgCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.Url.AbsolutePath);
            
        }
        protected void btnMsgOK_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.Url.AbsolutePath);
        }

        protected void Approve_Click(object sender, EventArgs e)
        {
            if (Session["CheckRefresh"].ToString() != ViewState["CheckRefresh"].ToString())
            {
                return;
            }
            bool isFinished = true;

            //Transcation trn = new Transcation(true);
            try
            {
                string chittkn = "";
                
                ImageButton btndetails = sender as ImageButton;
                GridViewRow gvRow = (GridViewRow)btndetails.NamingContainer;
                //string Series = balayer.ToobjectstrEvenNull(GridView1.DataKeys[gvRow.RowIndex]["Series"]);
                string Series = "ADVICE";
                string Voucher_No = balayer.ToobjectstrEvenNull(GridView1.DataKeys[gvRow.RowIndex]["ReceiptNumber"]);
                string GrpID = balayer.ToobjectstrEvenNull(GridView1.DataKeys[gvRow.RowIndex]["ChitGroupID"]);
                ViewState["GPID"] = balayer.ToobjectstrEvenNull(GridView1.DataKeys[gvRow.RowIndex]["ChitGroupID"]);
                string GrpMemberID = balayer.ToobjectstrEvenNull(GridView1.DataKeys[gvRow.RowIndex]["Token"]);
                string MemberID = balayer.ToobjectstrEvenNull(GridView1.DataKeys[gvRow.RowIndex]["MemberID"]);
                string Amount = balayer.ToobjectstrEvenNull(GridView1.DataKeys[gvRow.RowIndex]["Amount"]);
                string BranchHead = balayer.ToobjectstrEvenNull(GridView1.DataKeys[gvRow.RowIndex]["CollectedBranchID"]);
                string DualTransactionKey = balayer.ToobjectstrEvenNull(GridView1.DataKeys[gvRow.RowIndex]["DualTransactionKey"]);
                string Narration = balayer.ToobjectstrEvenNull(GridView1.DataKeys[gvRow.RowIndex]["Description"]);
                DateTime dtChoosenDate = DateTime.Parse(balayer.ToobjectstrEvenNull(GridView1.DataKeys[gvRow.RowIndex]["ChoosenDate"]));
                string TransactionKey = balayer.ToobjectstrEvenNull(GridView1.DataKeys[gvRow.RowIndex]["TransactionKey"]);
               // string chitnumber = balayer.ToobjectstrEvenNull(GridView1.DataKeys[gvRow.RowIndex]["GrpMemberID"]);
                string[] chitToken = Narration.Split(':');
                rvDate.MaximumValue = DateTime.Now.ToString("dd/MM/yyyy");
                rvDate.MinimumValue = balayer.GetSingleValue("SELECT DATE_FORMAT( MinimumDate, '%d/%m/%Y') MinimumDate FROM svcf.restrictionmaster where BranchID=" + BranchHead); 
                lblGroupID.Text = GrpID;
                if (Regex.IsMatch(GrpMemberID, "^(?=.*[a-zA-Z])"))
                {
                    GrpMemberID="0";
                }
                ViewState["Receipt_No"] = Voucher_No;
                lblGrpMemID.Text = GrpMemberID;
                lblSeries.Text = Series;
               // lblVoucher.Text = Voucher_No;
                //lblVoucher.Text = chitToken[0];
                lblDebit.Text = balayer.ToobjectstrEvenNull(GridView1.DataKeys[gvRow.RowIndex]["BranchName"]);
                lblDebitID.Text = balayer.ToobjectstrEvenNull(GridView1.DataKeys[gvRow.RowIndex]["CollectedBranchID"]);
                lblAmount.Text = Amount;
                lblDual.Text = TransactionKey;
                txtCredit.Text=chitToken[0].Trim();
                txtDesc.Text = Narration;
                var el = from s in chitToken
                        select s;

                int c = el.Count();
                if(Narration.Contains("Misc"))
                {
                    txtCredit.Text = "Misc";
                    lblVoucher.Text = txtCredit.Text;
                }
                else if (c ==2)				

                {
                    txtCredit.Text = chitToken[0];
                    lblVoucher.Text = txtCredit.Text;
                }
                else if(c==5)
                {
                    txtCredit.Text = chitToken[4];
                    lblVoucher.Text = txtCredit.Text;
                }
                else if (c == 6)
                {
                    txtCredit.Text = chitToken[5];
                    lblVoucher.Text = txtCredit.Text;
                }
              
                
                // string branchname = balayer.GetSingleValue("select * From headstree where ParentID = 1 and Node = '"+ txtCredit.Text + "'");
                string chosendt = balayer.GetSingleValue("SELECT DATE_FORMAT( CurrDate, '%d/%m/%Y') CurrDate FROM svcf.voucher where BranchID=" + BranchHead + " and Voucher_No =" + Voucher_No + " and Voucher_Type = 'c' ");

               //DateTime t1= Convert.ToDateTime(chosendt);
                DateTime t1 = dtChoosenDate;
               DateTime t2 = Convert.ToDateTime("2017-10-24 13:08:08");
                if(t1 < t2)
                {
                    ddlMisc.Visible = true;
                    FillDropDownList(ddlMisc, 2, "");
                }
                else if (Narration.Contains("Misc"))
                {
                    ddlMisc.Visible = true;
                    FillDropDownList(ddlMisc, 2, "");
                }
                else
                {
                    ddlMisc.Visible = false;
                }

              //  FillDropDownList(ddlMisc, 2, "");
                
                
                //DataTable dtAllHeads = balayer.GetDataTable("SELECT concat(cast(  v1.`RootID` as char),',',cast(v1.`TreeID` as char)) as ID,v1.`TREE` FROM `svcf`.`view_parent` as v1  where v1.BranchID is null or v1.BranchID=" + Session["Branchid"]);
                //DataRow dr = dtAllHeads.NewRow();
                //dr[0] = "--Select--";
                //dr[1] = "--Select--";
                //dtAllHeads.Rows.InsertAt(dr, 0);
                //ddlCredit.DataSource = dtAllHeads;
                //ddlCredit.DataTextField = "TREE";
                //ddlCredit.DataValueField = "ID";
                //ddlCredit.DataBind();
                //if (Narration.Contains(":"))
                //{
                //    chittkn = Narration.Split(':')[0].Trim();
                //}
                //hdid = balayer.GetScalarDataInt("select Head_Id from membertogroupmaster where GrpMemberID='" + chittkn + "'");
                //string strChitHeadQuery = "INSERT INTO `voucher` (`DualTransactionKey`,`BranchID`,`Current_Date`,`Voucher_No`,`Voucher_Type`,`Head_Id`,`ChoosenDate`,`Narration`,`Amount`,`Series`,`ReceievedBy`,`Trans_Type`,`T_Day`,`T_Month`,`T_Year`,`MemberID`,`Trans_Medium`,`RootID`,`ChitGroupId`,Other_Trans_Type) VALUES (" + DualTransactionKey + "," + balayer.ToobjectstrEvenNull(Session["Branchid"]) + ",'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," + Voucher_No + ",'D'," + BranchHead + ",'" + dtChoosenDate.ToString("yyyy-MM-dd") + "','" + Narration + "'," + Amount + ",'" + Series + "','" + balayer.ToobjectstrEvenNull(Session["UserName"]) + "',1," + dtChoosenDate.Day + "," + dtChoosenDate.Month + "," + dtChoosenDate.Year + "," + MemberID + "," + "2" + ",1," + GrpID + ",2)";
                //string strCashHeadQuery = "INSERT INTO `voucher` (`DualTransactionKey`,`BranchID`,`Current_Date`,`Voucher_No`,`Voucher_Type`,`Head_Id`,`ChoosenDate`,`Narration`,`Amount`,`Series`,`ReceievedBy`,`Trans_Type`,`T_Day`,`T_Month`,`T_Year`,`MemberID`,`Trans_Medium`,`RootID`,`ChitGroupId`,Other_Trans_Type) VALUES (" + DualTransactionKey + "," + balayer.ToobjectstrEvenNull(Session["Branchid"]) + ",'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," + Voucher_No + ",'C'," + GrpMemberID + ",'" + dtChoosenDate.ToString("yyyy-MM-dd") + "','" + Narration + "'," + Amount + ",'" + Series + "','" + balayer.ToobjectstrEvenNull(Session["UserName"]) + "',1," + dtChoosenDate.Day + "," + dtChoosenDate.Month + "," + dtChoosenDate.Year + "," + MemberID + "," + "2" + "," + parentID + "," + GrpID + ",2)";
                //string strUpdateHeadQuery = "update  `voucher` set Other_Trans_Type=2 where DualTransactionKey=" + DualTransactionKey;
                //long iResultDebit= trn.insertorupdate(strChitHeadQuery);
                //long iResultCredit=trn.insertorupdate(strCashHeadQuery);
                //long uResult=trn.insertorupdate(strUpdateHeadQuery);
                tranlayer.CommitTrn();
                //trn.Commit();
                ////GrpMemberID
            }
            catch
            {
                try
                {
                    tranlayer.RollbackTrn();
                }
                catch { }
                finally
                {
                    //lblT.Text = "Status";
                    //lblContent.Text = error.Message;
                    //lblContent.ForeColor = System.Drawing.Color.Red;
                    //ModalPopupExtender1.PopupControlID = "PnlApprove";
                    //ModalPopupExtender1.Show();
                    //PnlApprove.Visible = true;
                    ModalPopupExtender1.PopupControlID = "PnlProvide";
                    ModalPopupExtender1.Show();
                    PnlProvide.Visible = true;
                    isFinished = false;
                }
            }
            finally
            {
                tranlayer.DisposeTrn();
                if (isFinished == true)
                {
                    //lblT.Text = "Status";
                    //lblContent.Text = "Advice Accepted!!!";
                    //lblContent.ForeColor = System.Drawing.Color.Green;
                    //ModalPopupExtender1.PopupControlID = "PnlApprove";
                    //ModalPopupExtender1.Show();
                    //PnlApprove.Visible = true;
                    ModalPopupExtender1.PopupControlID = "PnlProvide";
                    ModalPopupExtender1.Show();
                    PnlProvide.Visible = true;
                }
            }
        }

        private void FillDropDownList(DropDownList ddl, int iType, string MemberID)
        {  //chit
            if (iType == 0)
            {
                ddl.DataSource = null;
                DataTable dtgroupno = null;
                dtgroupno = balayer.GetDataTable("select NodeID,Node from headstree where ParentID=1 and NodeID<>" + Session["Branchid"]);
                DataRow dr = dtgroupno.NewRow();
                dr[0] = "0";
                dr[1] = "--Select--";
                ddl.DataValueField = "NodeID";
                ddl.DataTextField = "Node";
                dtgroupno.Rows.InsertAt(dr, 0);
                ddl.DataSource = dtgroupno;

                ddl.DataBind();
            }
            //token
            else if (iType == 1)
            {
                //DropDownList ddlMemberName = (DropDownList)GridView1.Rows[0].Cells[1].FindControl("ddlMemberName");
                //if (ddlMemberName.SelectedItem.Text != "--Select--")
                //{
                //    ddl.DataSource = null;
                //    DataTable dt = null;
                //    dt = balayer.GetDataTable(@"SELECT GrpMemberID,Head_Id FROM membertogroupmaster where  MemberID=" + MemberID );
                //    DataRow dr = dt.NewRow();
                //    dr[0] = "--Select--";
                //    dr[1] = "0";
                //    dt.Rows.InsertAt(dr, 0);
                //    ddl.DataSource = dt;
                //    ddl.DataValueField = "Head_Id";
                //    ddl.DataTextField = "GrpMemberID";
                //    ddl.DataBind();
                //}
                //else
                //{
                //    ddl.DataSource = null;
                //    ddl.DataSource = null;
                //    DataTable dt = balayer.GetDataTable(@"SELECT GrpMemberID,Head_Id FROM membertogroupmaster where  MemberID=0");
                //    DataRow dr = dt.NewRow();
                //    dr[0] = "--Select--";
                //    dr[1] = "0";
                //    dt.Rows.InsertAt(dr, 0);
                //    ddl.DataBind();
                //}
            }
            //misc
            else if (iType == 2)
            {
                ddl.DataSource = null;
                DataTable dtgroupno = null;
                dtgroupno = balayer.GetDataTable("SELECT concat(cast(TreeID as char),',',cast(RootID as char)) as TreeID,TREE FROM svcf.view_parent where RootID not in(3,1,2,4,6,7,10,12) and (BranchID is null or BranchID=" + Session["Branchid"] + ")");
               // dtgroupno = balayer.GetDataTable("SELECT concat(cast(TreeID as char),',',cast(RootID as char)) as TreeID,TREE FROM svcf.view_parent where RootID = 11 and (BranchID is null or BranchID=" + Session["Branchid"] + ")");
                DataRow dr = dtgroupno.NewRow();
                dr[0] = "0,0";
                dr[1] = "--Select--";
                ddl.DataValueField = "TreeID";
                ddl.DataTextField = "TREE";
                dtgroupno.Rows.InsertAt(dr, 0);
                ddl.DataSource = dtgroupno;
                ddl.DataBind();
            }
        }






        protected void dis_Approve_Click(object sender, EventArgs e)
        {
           // lblContent.Text = "Advice Accepted!!!";
            ImageButton btndetails = sender as ImageButton;
            GridViewRow gvRow = (GridViewRow)btndetails.NamingContainer;
            Session["RowIndex"] = gvRow.RowIndex;
            ModalPopupExtender1.PopupControlID = "PnlReject";
            ModalPopupExtender1.Show();
            PnlReject.Visible = true;
        }
        protected void Page_PreRender(object sender, EventArgs e)
        {
            ViewState["CheckRefresh"] = Session["CheckRefresh"];
            //rvDate.MaximumValue = DateTime.Now.ToString("dd/MM/yyyy");
            //rvDate.MinimumValue = balayer.GetSingleValue("SELECT DATE_FORMAT( MinimumDate, '%d/%m/%Y') MinimumDate FROM svcf.restrictionmaster where BranchID=" + Session["Branchid"]); 
        }
        public DataTable GetData(int iFlag)
        {
            DataTable Loadt = new DataTable();
            if (iFlag == 1)
            {
                Loadt = balayer.GetDataTable("SELECT t1.TransactionKey,t3.B_Name as BranchName, date_format( t1.ChoosenDate,'%d/%m/%Y') as ChoosenDate,g1.GROUPNO,m1.Node as GrpMemberID,t1.Series,t1.Voucher_No as ReceiptNumber,t2.CustomerName,t1.Amount,t1.Narration as Description,insertkey_from_bin(t1.DualTransactionKey) as DualTransactionKey,t1.BranchID as CollectedBranchID,t1.MemberID,t1.ChitGroupID, get_ref_no(t1.Narration) as Token,t1.AppReceiptno as 'AppReceiptno' FROM `svcf`.`voucher` as t1 left join membermaster as t2  on  t1.MemberID=t2.MemberIDNew join branchdetails as t3 on (t3.Head_Id=t1.BranchID) left join headstree as m1 on (get_ref_no(t1.Narration)=m1.NodeID) left join groupmaster as g1 on (t1.ChitGroupId=g1.Head_Id)  where t1.Trans_Type=1 and t1.Other_Trans_Type=" + iFlag + " and t1.Head_Id=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + "");
            }
            else if (iFlag == 2)
            {
//                Loadt = balayer.GetDataTable("SELECT t1.TransactionKey,t3.B_Name as BranchName, date_format( t1.ChoosenDate,'%d/%m/%Y') as ChoosenDate,g1.GROUPNO,m1.Node as GrpMemberID,t1.Series,t1.Voucher_No as ReceiptNumber,t2.CustomerName,t1.Amount,t1.Narration as Description,insertkey_from_bin(t1.DualTransactionKey) as DualTransactionKey,t1.BranchID as CollectedBranchID,t1.MemberID,t1.ChitGroupID, get_ref_no(t1.Narration) as Token,t1.AppReceiptno as 'AppReceiptno' FROM `svcf`.`voucher` as t1 left join membermaster as t2  on  t1.MemberID=t2.MemberIDNew join branchdetails as t3 on (t3.Head_Id=t1.BranchID) left join headstree as m1 on (get_ref_no(t1.Narration)=m1.NodeID) left join groupmaster as g1 on (t1.ChitGroupId=g1.Head_Id)  where t1.Trans_Type=1 and t1.Other_Trans_Type=" + iFlag + " and t1.Head_Id=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + "");
                Loadt = balayer.GetDataTable("SELECT t1.TransactionKey,t3.B_Name as BranchName, date_format( t1.ChoosenDate,'%d/%m/%Y') as ChoosenDate,g1.GROUPNO,m1.Node as GrpMemberID,t1.Series,t1.Voucher_No as ReceiptNumber,t2.CustomerName,t1.Amount,t1.Narration as Description,insertkey_from_bin(t1.DualTransactionKey) as DualTransactionKey,t1.BranchID as CollectedBranchID,t1.MemberID,t1.ChitGroupID, get_ref_no(t1.Narration) as Token,t1.AppReceiptno as 'AppReceiptno' FROM `svcf`.`voucher` as t1 left join membermaster as t2  on  " +
                    "t1.MemberID=t2.MemberIDNew join branchdetails as t3 on (t3.Head_Id=t1.BranchID) left join headstree as m1 on (get_ref_no(t1.Narration)=m1.NodeID) left join groupmaster as g1 on (t1.ChitGroupId=g1.Head_Id)  where t1.Trans_Type=1 and t1.Other_Trans_Type=" + iFlag + " and t1.BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and month(ChoosenDate)=" + DrpdwnMonth.SelectedValue + " and t1.Voucher_Type='C'  and Series='ADVICE'");
            }
            return Loadt;
        }

        protected void btnLoad_Click(object sender, EventArgs e)
        {
            DataTable dtAcc = null;

            dtAcc = GetData(1);
            DataTable dtCloned = dtAcc.Clone();
            dtCloned.Columns["ChoosenDate"].DataType = typeof(string);
            foreach (DataRow row in dtAcc.Rows)
            {
                dtCloned.ImportRow(row);
            }

            GridAccepted.Visible = false;
            GridView1.Visible = true;

            GridView1.DataSource = dtCloned;
            GridView1.DataBind();

        }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["CheckRefresh"] = System.Guid.NewGuid().ToString();
                //DataTable dtAcc = GetData(1);
                //DataTable dtCloned = dtAcc.Clone();
                //dtCloned.Columns["ChoosenDate"].DataType = typeof(string);
                ////dtCloned.Columns["ReceiptDate"].DataType = typeof(string);
                //foreach (DataRow row in dtAcc.Rows)
                //{
                //    dtCloned.ImportRow(row);
                //}
                //GridView1.DataSource = dtCloned;
                //GridView1.DataBind();              
            }
        
            logger.Info("Received Advices - at: " + DateTime.Now);
        }

     
        protected void btnAcceptOK_Click(object sender, EventArgs e)
        {
            bool isFinished = true;         
            try
            {
                System.Guid guid = Guid.NewGuid();
                // Prepare GUID values in SQL format
                string hexstring = BitConverter.ToString(guid.ToByteArray());
                string guidForBinary16 = "0x" + hexstring.Replace("-", string.Empty);
             //   string DualTransactionKey = guidForBinary16;
                string DualTransactionKey = Convert.ToString(balayer.sp_gendratedt_key());
                string receipt_no = Convert.ToString(ViewState["Receipt_No"]);
                //txtDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                DateTime dtChoosenDate = DateTime.Parse(txtDate.Text);

                string MemberID = balayer.GetSingleValue("SELECT MemberID FROM svcf.membertogroupmaster where Head_id=" + lblGrpMemID.Text);
                if (string.IsNullOrEmpty(MemberID))
                {
                    MemberID = "0";
                }
          
                if (!ddlMisc.Visible)
                {                 
                    string Nm = (lblVoucher.Text).Split('/')[0];
                    string gyu = "Select Head_Id from groupmaster where GROUPNO ='" + Nm + "';";
                    string gpid = balayer.GetSingleValue(gyu);
                    if (gpid == "")
                    {
                        gyu = "select GroupID from membertogroupmaster where GrpMemberID='" + Nm + "';";
                        gpid = balayer.GetSingleValue(gyu);
                    }

                    // string gpid = Convert.ToString(ViewState["GPID"]);

                    DataTable token = balayer.GetDataTable("SELECT Head_Id,GroupID FROM svcf.membertogroupmaster where GrpMemberID='" + txtCredit.Text + "'");
                    string strChitHeadQuery = "INSERT INTO `voucher` (`DualTransactionKey`,`BranchID`,`CurrDate`,`Voucher_No`,`Voucher_Type`,`Head_Id`,`ChoosenDate`,`Narration`,`Amount`,`Series`,`ReceievedBy`,`Trans_Type`,`T_Day`,`T_Month`,`T_Year`,`MemberID`,`Trans_Medium`,`RootID`,`ChitGroupId`,Other_Trans_Type) VALUES (" + DualTransactionKey + "," + balayer.ToobjectstrEvenNull(Session["Branchid"]) + ",'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + receipt_no + "','D'," + lblDebitID.Text + ",'" + balayer.changedateformat(Convert.ToDateTime(txtDate.Text), 2) + "','" + balayer.MySQLEscapeString(txtDesc.Text) + "'," + lblAmount.Text + ",'" + lblSeries.Text + "','" + balayer.ToobjectstrEvenNull(Session["UserName"]) + "',1," + dtChoosenDate.Day + "," + dtChoosenDate.Month + "," + dtChoosenDate.Year + "," + MemberID + "," + "2" + ",1," + gpid + ",2)";
                    ////string strCashHeadQuery = "INSERT INTO `voucher` (`DualTransactionKey`,`BranchID`,`Current_Date`,`Voucher_No`,`Voucher_Type`,`Head_Id`,`ChoosenDate`,`Narration`,`Amount`,`Series`,`ReceievedBy`,`Trans_Type`,`T_Day`,`T_Month`,`T_Year`,`MemberID`,`Trans_Medium`,`RootID`,`ChitGroupId`,Other_Trans_Type) VALUES (" + DualTransactionKey + "," + balayer.ToobjectstrEvenNull(Session["Branchid"]) + ",'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," + lblVoucher.Text + ",'C'," + hdid + ",'" + dtChoosenDate.ToString("yyyy-MM-dd") + "','" + balayer.MySQLEscapeString(txtDesc.Text) + "'," + lblAmount.Text + ",'" + lblSeries.Text + "','" + balayer.ToobjectstrEvenNull(Session["UserName"]) + "',1," + dtChoosenDate.Day + "," + dtChoosenDate.Month + "," + dtChoosenDate.Year + "," + MemberID + "," + "2" + "," + 5 + "," + lblGroupID.Text + ",2)";
                    string strCashHeadQuery = "INSERT INTO `voucher` (`DualTransactionKey`,`BranchID`,`CurrDate`,`Voucher_No`,`Voucher_Type`,`Head_Id`,`ChoosenDate`,`Narration`,`Amount`,`Series`,`ReceievedBy`,`Trans_Type`,`T_Day`,`T_Month`,`T_Year`,`MemberID`,`Trans_Medium`,`RootID`,`ChitGroupId`,Other_Trans_Type) VALUES (" + DualTransactionKey + "," + balayer.ToobjectstrEvenNull(Session["Branchid"]) + ",'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + receipt_no + "','C'," + token.Rows[0][0].ToString() + ",'" + balayer.changedateformat(Convert.ToDateTime(txtDate.Text), 2) + "','" + balayer.MySQLEscapeString(txtDesc.Text) + "'," + lblAmount.Text + ",'" + lblSeries.Text + "','" + balayer.ToobjectstrEvenNull(Session["UserName"]) + "',1," + dtChoosenDate.Day + "," + dtChoosenDate.Month + "," + dtChoosenDate.Year + "," + MemberID + "," + "2" + "," + 5 + "," + gpid + ",2)";

                    string strUpdateHeadQuery = "update  `voucher` set Other_Trans_Type=2 and IsAccepted=1 where TransactionKey=" + lblDual.Text;
                    long iResultDebit = tranlayer.insertorupdateTrn(strChitHeadQuery);
                    long iResultCredit = tranlayer.insertorupdateTrn(strCashHeadQuery);
                    long uResult = tranlayer.insertorupdateTrn(strUpdateHeadQuery);
                    tranlayer.CommitTrn();
                    logger.Info("ReceivedAdvices.aspx - btnAcceptOK_click():  Completed: " + DateTime.Now + " by: " + Convert.ToString(Session["UserName"]) + "");

                    lblT.Text = "Status";
                    lblContent.Text = "Advice Accepted!!!";
                    lblContent.ForeColor = System.Drawing.Color.Green;
                    ModalPopupExtender1.PopupControlID = "PnlApprove";
                    ModalPopupExtender1.Show();
                    PnlApprove.Visible = true;


                }
                else if (ddlMisc.SelectedItem != null && ddlMisc.SelectedItem.Text != "--Select--")
                {

                    string strChitHeadQuery = "INSERT INTO `voucher` (`DualTransactionKey`,`BranchID`,`CurrDate`,`Voucher_No`,`Voucher_Type`,`Head_Id`,`ChoosenDate`,`Narration`,`Amount`,`Series`,`ReceievedBy`,`Trans_Type`,`T_Day`,`T_Month`,`T_Year`,`MemberID`,`Trans_Medium`,`RootID`,`ChitGroupId`,Other_Trans_Type) VALUES (" + DualTransactionKey + "," + balayer.ToobjectstrEvenNull(Session["Branchid"]) + ",'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + receipt_no + "','D'," + lblDebitID.Text + ",'" + balayer.changedateformat(Convert.ToDateTime(txtDate.Text), 2) + "','" + balayer.MySQLEscapeString(txtDesc.Text) + "'," + lblAmount.Text + ",'" + lblSeries.Text + "','" + balayer.ToobjectstrEvenNull(Session["UserName"]) + "',1," + dtChoosenDate.Day + "," + dtChoosenDate.Month + "," + dtChoosenDate.Year + "," + MemberID + "," + "2" + ",1," + lblGroupID.Text + ",2)";
                    string strCashHeadQuery = "INSERT INTO `voucher` (`DualTransactionKey`,`BranchID`,`CurrDate`,`Voucher_No`,`Voucher_Type`,`Head_Id`,`ChoosenDate`,`Narration`,`Amount`,`Series`,`ReceievedBy`,`Trans_Type`,`T_Day`,`T_Month`,`T_Year`,`MemberID`,`Trans_Medium`,`RootID`,`ChitGroupId`,Other_Trans_Type) VALUES (" + DualTransactionKey + "," + balayer.ToobjectstrEvenNull(Session["Branchid"]) + ",'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + receipt_no + "','C'," + ddlMisc.SelectedValue.Split(',')[0].ToString() + ",'" + balayer.changedateformat(Convert.ToDateTime(txtDate.Text), 2) + "','" + balayer.MySQLEscapeString(txtDesc.Text) + "'," + lblAmount.Text + ",'" + lblSeries.Text + "','" + balayer.ToobjectstrEvenNull(Session["UserName"]) + "',1," + dtChoosenDate.Day + "," + dtChoosenDate.Month + "," + dtChoosenDate.Year + "," + MemberID + "," + "2" + "," + ddlMisc.SelectedValue.Split(',')[1].ToString() + "," + lblGroupID.Text + ",2)";


                    string strUpdateHeadQuery = "update  `voucher` set Other_Trans_Type=2 where TransactionKey=" + lblDual.Text;
                    long iResultDebit = tranlayer.insertorupdateTrn(strChitHeadQuery);
                    long iResultCredit = tranlayer.insertorupdateTrn(strCashHeadQuery);
                    long uResult = tranlayer.insertorupdateTrn(strUpdateHeadQuery);
                    tranlayer.CommitTrn();
                    logger.Info("ReceivedAdvices.aspx - btnAcceptOK_click():  Completed: " + DateTime.Now + " by: " + Convert.ToString(Session["UserName"]) + "");


                    lblT.Text = "Status";
                    lblContent.Text = "Advice Accepted!!!";
                    lblContent.ForeColor = System.Drawing.Color.Green;
                    ModalPopupExtender1.PopupControlID = "PnlApprove";
                    ModalPopupExtender1.Show();
                    PnlApprove.Visible = true;
                }
            }
            //}
            catch (Exception error)
            {
                try
                {
                    tranlayer.RollbackTrn();
                    logger.Info("ReceivedAdvices.aspx - btnAcceptOK_click():  Error: " + error.Message + ": " + DateTime.Now + " by: " + Convert.ToString(Session["UserName"]) + "");
                }
                catch { }
                finally
                {
                    lblT.Text = "Status";
                    lblContent.Text = error.Message;
                    lblContent.ForeColor = System.Drawing.Color.Red;
                    ModalPopupExtender1.PopupControlID = "PnlApprove";
                    ModalPopupExtender1.Show();
                    PnlApprove.Visible = true;
                    isFinished = false;
                }
            }
          
        }
        protected void btnAcceptCancel_Click(object sender, EventArgs e)
        {
        }

        private void BindAcceptedGrid()
        {
            try
            {
                DataTable Loadt = balayer.GetDataTable("SELECT t1.TransactionKey,t3.B_Name as BranchName, date_format( t1.ChoosenDate,'%d/%m/%Y') as ChoosenDate,g1.GROUPNO,m1.Node as GrpMemberID,t1.Series,t1.Voucher_No as ReceiptNumber,t2.CustomerName,t1.Amount,t1.Narration as Description,insertkey_from_bin(t1.DualTransactionKey) as DualTransactionKey,t1.BranchID as CollectedBranchID,t1.MemberID,t1.ChitGroupID, get_ref_no(t1.Narration) as Token,t1.AppReceiptno as 'AppReceiptno' FROM `svcf`.`voucher` as t1 left join membermaster as t2  on  " +
                        "t1.MemberID=t2.MemberIDNew join branchdetails as t3 on (t3.Head_Id=t1.BranchID) left join headstree as m1 on (get_ref_no(t1.Narration)=m1.NodeID) left join groupmaster as g1 on (t1.ChitGroupId=g1.Head_Id)  where t1.Trans_Type=1 and t1.Other_Trans_Type=2 and t1.BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and month(ChoosenDate)=" + DrpdwnMonth.SelectedValue + " and t1.Voucher_Type='C'  and Series='ADVICE'");

                GridAccepted.Visible = true;
                GridView1.Visible = false;
                GridAccepted.DataSource = Loadt;
                GridAccepted.DataBind();
            }
            catch (Exception err) { }
        }

        protected void BtnViewAcceptedAdvice_Click(object sender, EventArgs e)
        {
            try
            {
                this.BindAcceptedGrid();
            }
            catch (Exception err) { }
        }


        private void BindSearchAcceptedAdvice()
        {
            try
            {
                if (txtSearchReceiptno.Text != "")
                {
                    DataTable Loadt = balayer.GetDataTable("SELECT t1.TransactionKey,t3.B_Name as BranchName, date_format( t1.ChoosenDate,'%d/%m/%Y') as ChoosenDate,g1.GROUPNO,m1.Node as GrpMemberID,t1.Series,t1.Voucher_No as ReceiptNumber,t2.CustomerName,t1.Amount,t1.Narration as Description,insertkey_from_bin(t1.DualTransactionKey) as DualTransactionKey,t1.BranchID as CollectedBranchID,t1.MemberID,t1.ChitGroupID, get_ref_no(t1.Narration) as Token,t1.AppReceiptno as 'AppReceiptno' FROM `svcf`.`voucher` as t1 left join membermaster as t2  on  " +
                            "t1.MemberID=t2.MemberIDNew join branchdetails as t3 on (t3.Head_Id=t1.BranchID) left join headstree as m1 on (get_ref_no(t1.Narration)=m1.NodeID) left join groupmaster as g1 on (t1.ChitGroupId=g1.Head_Id)  where t1.Trans_Type=1 and t1.Other_Trans_Type=2 and t1.BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and month(ChoosenDate)=" + DrpdwnMonth.SelectedValue + " and t1.Voucher_Type='C'  and Series='ADVICE' and t1.Voucher_No=" + txtSearchReceiptno.Text + "");

                    GridAccepted.Visible = true;
                    GridView1.Visible = false;
                    GridAccepted.DataSource = Loadt;
                    GridAccepted.DataBind();
                }
                else
                {
                    DataTable Loadt = balayer.GetDataTable("SELECT t1.TransactionKey,t3.B_Name as BranchName, date_format( t1.ChoosenDate,'%d/%m/%Y') as ChoosenDate,g1.GROUPNO,m1.Node as GrpMemberID,t1.Series,t1.Voucher_No as ReceiptNumber,t2.CustomerName,t1.Amount,t1.Narration as Description,insertkey_from_bin(t1.DualTransactionKey) as DualTransactionKey,t1.BranchID as CollectedBranchID,t1.MemberID,t1.ChitGroupID, get_ref_no(t1.Narration) as Token,t1.AppReceiptno as 'AppReceiptno' FROM `svcf`.`voucher` as t1 left join membermaster as t2  on  " +
                           "t1.MemberID=t2.MemberIDNew join branchdetails as t3 on (t3.Head_Id=t1.BranchID) left join headstree as m1 on (get_ref_no(t1.Narration)=m1.NodeID) left join groupmaster as g1 on (t1.ChitGroupId=g1.Head_Id)  where t1.Trans_Type=1 and t1.Other_Trans_Type=2 and t1.BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and month(ChoosenDate)=" + DrpdwnMonth.SelectedValue + " and t1.Voucher_Type='C'  and Series='ADVICE'");

                    GridAccepted.Visible = true;
                    GridView1.Visible = false;
                    GridAccepted.DataSource = Loadt;
                    GridAccepted.DataBind();
                }
            }
            catch (Exception err) { }
        }
        protected void btnSearchAccepted_Click(object sender, EventArgs e)
        {
            try
            {
                this.BindSearchAcceptedAdvice();
            }
            catch(Exception err)
            {

            }
        }

        protected void GridAccepted_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (txtSearchReceiptno.Text != "")
                {
                    e.Row.Cells[3].Text = Regex.Replace(e.Row.Cells[3].Text, txtSearchReceiptno.Text.Trim(), delegate(Match match)
                    {
                        return string.Format("<span style = 'background-color:#D9EDF7'>{0}</span>", match.Value);
                    }, RegexOptions.IgnoreCase);
                }
            }
        }
    }
}
