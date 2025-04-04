using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SVCF_BusinessAccessLayer;
using SVCF_DataAccessLayer;
using SVCF_TransactionLayer;

namespace SreeVisalamChitFundLtd_phase1
{
    public partial class Addcustomerbank : System.Web.UI.Page
    {
        #region Object
        BusinessLayer balayer = new BusinessLayer();
        TransactionLayer trn = new TransactionLayer();
        CommonClassFile objCOM = new CommonClassFile();
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var userinfo = HttpContext.Current.User.Identity.Name;
                var qry = "select rs.name from roles as rs inner join rights as rt on (rt.roleid=rs.id) where memberid=" + userinfo + "";
                var usrRole = balayer.GetSingleValue(qry);
                if (usrRole != "Administrator") Response.Redirect("Home.aspx", false);
                txtbank_name.Text = "";
                lblContent.Text = "";
            }
        }

        protected void btnbankInsert_OnClick(object sender, EventArgs e)
        {
            if (txtbank_name.Text != "")
            {
                string BankInsertion = "insert into customerbank(bankname,bankkey,createdat) values ('" + txtbank_name.Text + "','" + txtbank_name.Text + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                try
                {
                    var qry = "select * from svcf.customerbank where bankname='" + txtbank_name.Text + "'";
                    var result = objCOM.GetSingleValue1(qry);
                    if (result == "")
                    {
                        objCOM.InsertOrUpdateorDelete(BankInsertion);
                        lblContent.Text = "Customer Bank Inserted Successfully";
                        lblContent.ForeColor = System.Drawing.Color.Green;
                    }
                    else
                    {
                        lblContent.Text = "Customer bank already exists";
                        lblContent.ForeColor = System.Drawing.Color.Red;   
                    }
                    txtbank_name.Text = "";
                }
                catch (Exception ex)
                {                    
                    lblContent.Text = "Error while adding customer Bank";
                    lblContent.ForeColor = System.Drawing.Color.Red;                  
                    LogCls.LogError(ex, "Customer Bank Addition");
                }
            }
        }

    

        protected void btnNo_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.Url.AbsolutePath.ToString());
        }

    }
}