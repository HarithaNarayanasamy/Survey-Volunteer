using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using SVCF_BusinessAccessLayer;
using SVCF_DataAccessLayer;
using SVCF_TransactionLayer;
using log4net;
using log4net.Config;

namespace SreeVisalamChitFundLtd_phase1
{
    public partial class UndoReceipt : System.Web.UI.Page
    {
        #region Object
        BusinessLayer balayer = new BusinessLayer();
        TransactionLayer trn = new TransactionLayer();
        #endregion
        ILog logger = log4net.LogManager.GetLogger(typeof(UndoReceipt));

        protected void Page_Load(object sender, EventArgs e)
        {
            ddlGroupNo.DataSource = null;
            DataTable dtgroupno = null;
            //if (chkLoadAllChit.Checked == false)
            //{

                dtgroupno = balayer.GetDataTable("select Head_Id,GROUPNO from groupmaster where BranchID='" +balayer.ToobjectstrEvenNull(Session["Branchid"]) + "'");

            //}
            //else
            //{
            //    dtgroupno = SreeVisalamChitFundLtd_phase1.balayer.GetDataTable("select Head_Id,GROUPNO from groupmaster");
            //}

            ddlGroupNo.DataSource = dtgroupno;
            ddlGroupNo.DataValueField = "Head_Id";
            ddlGroupNo.DataTextField = "GROUPNO";


            ddlGroupNo.DataBind();
            ddlGroupNo.Items.Insert(0, "--Select--");

            logger.Info("Undo Receipt- at: " + DateTime.Now + " by: " + Convert.ToString(Session["UserName"]) + "");

        }
         protected void ddlTokenNo_SelectedIndexChanged(object sender, EventArgs e)
        {


        }

         protected void ddlGroupNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dtgroupno = balayer.GetDataTable(@"SELECT concat(cast(Head_Id as char),'|',cast(MemberID as char)) as IDS,concat(GrpMemberID,'--- ',MemberName) as MemberName FROM membertogroupmaster where  GroupID=" + ddlGroupNo.SelectedValue + ";");
            // DataTable dtgroupno = SreeVisalamChitFundLtd_phase1.balayer.GetDataTable("select Head_Id,GROUPNO from groupmaster where Head_Id='" + SreeVisalamChitFundLtd_phase1.balayer.ToobjectstrEvenNull(Session["Branchid"]) + "'");
            ddlTokenNo.DataSource = dtgroupno;
            ddlTokenNo.DataValueField = "IDS";
            ddlTokenNo.DataTextField = "MemberName";


            ddlTokenNo.DataBind();
            ddlTokenNo.Items.Insert(0, "--Select--");
        }
        

         protected void btnLoad_click(object sender, EventArgs e)
        {

            AccessDataSource1.ConnectionString = CommonClassFile.ConnectionString;
            AccessDataSource1.SelectCommand = @"SELECT uuid_from_bin(v1.DualTransactionKey),m1.CustomerName,CustomersBankName,t1.DateInCheque,t1.ChequeDDNO,t1.GivenAmount,t1.TotalChequeDDAmount,v2.*     FROM voucher v1 INNER JOIN voucher v2 ON v1.DualTransactionKey = v2.DualTransactionKey left Join membermaster as m1  on  v2.MemberID=m1.MemberIDNew left Join transbank as t1  on  v2.TransactionKey=t1.TransactionKey where v1.Head_Id=" + ddlTokenNo.SelectedValue + " and  v2.Voucher_Type='C';";
        }

        

    }
}
