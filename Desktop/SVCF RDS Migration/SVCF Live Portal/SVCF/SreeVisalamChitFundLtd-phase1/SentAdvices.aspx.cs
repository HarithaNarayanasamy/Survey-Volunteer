using System;
using System.Data;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.IO;
using DevExpress.XtraReports.UI;
using SVCF_BusinessAccessLayer;
using SVCF_DataAccessLayer;
using SVCF_TransactionLayer;
namespace SreeVisalamChitFundLtd_phase1
{
    public partial class SentAdvices : System.Web.UI.Page
    {
        #region Object
        BusinessLayer balayer = new BusinessLayer();
        TransactionLayer trn = new TransactionLayer();
        #endregion
        protected void btnRejectCancel_Click(object sender, EventArgs e)
        {
        }
        protected void btnRejectOK_Click(object sender, EventArgs e)
        {
        }
        protected void btnMsgCancel_Click(object sender, EventArgs e)
        {
        }
        protected void btnMsgOK_Click(object sender, EventArgs e)
        {
            
        }
        void select(int flag)
        {
            DataTable dtAcc = balayer.GetDataTable("SELECT t3.B_Name as BranchName, date_format( t1.ChoosenDate,'%d/%m/%Y') as ChoosenDate,g1.GROUPNO,m1.GrpMemberID,t1.Series,t1.Voucher_No as ReceiptNumber,t2.CustomerName,t1.Amount,t1.Narration as Description,insertkey_from_bin(t1.DualTransactionKey) as DualTransactionKey,t1.BranchID as CollectedBranchID,t1.MemberID,t1.ChitGroupID, get_ref_no(t1.Narration) as Token FROM `svcf`.`voucher` as t1 left join membermaster as t2  on  t1.MemberID=t2.MemberIDNew join branchdetails as t3 on (t3.Head_Id=t1.Head_Id) join membertogroupmaster as m1 on (get_ref_no(t1.Narration)=m1.Head_Id) join groupmaster as g1 on (t1.ChitGroupId=g1.Head_Id) where  t1.Other_Trans_Type=" + flag + " and t1.BranchID=" + Session["Branchid"]);
            DataTable dtCloned = dtAcc.Clone();
            dtCloned.Columns["ChoosenDate"].DataType = typeof(string);
            foreach (DataRow row in dtAcc.Rows)
            {
                dtCloned.ImportRow(row);
            }
            GridView1.DataSource = dtCloned;
            GridView1.DataBind();
        }
        protected void Approve_Click(object sender, EventArgs e)
        {
        }
        protected void dis_Approve_Click(object sender, EventArgs e)
        {
        }
        protected void btnLoad_Click(object sender, EventArgs e)
        {
            if (ddlStatus.SelectedIndex == 0)
            {
                select(1);
            }
            else if (ddlStatus.SelectedIndex == 1)
            {
                select(2);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                select(1);
            }
        }
    }
}
