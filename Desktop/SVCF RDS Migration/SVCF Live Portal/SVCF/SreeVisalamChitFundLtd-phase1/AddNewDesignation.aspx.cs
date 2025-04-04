using log4net;
using SVCF_BusinessAccessLayer;
using SVCF_TransactionLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SreeVisalamChitFundLtd_phase1
{
    public partial class AddNewDesignation : System.Web.UI.Page
    {
        #region Object
        BusinessLayer balayer = new BusinessLayer();
        TransactionLayer trn = new TransactionLayer();
        #endregion

        ILog logger = log4net.LogManager.GetLogger(typeof(EmployeeDetails));
        protected void Page_Load(object sender, EventArgs e)
        {
            Pnlmsg.Visible = false;
            if (!IsPostBack)
            {
                Session["CheckRefresh"] = System.Guid.NewGuid().ToString();
            }
        }
        protected void btn_Yes_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.Url.AbsolutePath.ToString());
        }
        protected void btnEmployee_Click(object sender, EventArgs e)
        {
            Page.Validate("add");
            if (!Page.IsValid)
            {
                return;
            }
            //if (Session["CheckRefresh"].ToString() != ViewState["CheckRefresh"].ToString())
            //{
            //    return;
            //}

            try
            {
                if (balayer.ExecuteReader("SELECT * FROM svcf.employeedesignation where Designationname='" + balayer.MySQLEscapeString(txtemp_Des.Text) + "'").HasRows)
                {
                    //DataTable dt = balayer.GetDataTable("SELECT * FROM svcf.employee_details where BranchID=" + Session["Branchid"] + " and Emp_PhoneNo='" + balayer.MySQLEscapeString(txtemp_PhoneNo.Text) + "' and Emp_Name='" + balayer.MySQLEscapeString(txtemp_Name.Text) + "'");


                    Pnlmsg.Visible = true;
                    btn_Yes.Text = "Ok";
                    this.ModalPopupExtender1.PopupControlID = "Pnlmsg";
                    this.ModalPopupExtender1.Show();
                    lblHeading.Text = "Status";
                    lblContent.Text = " Employee Name : " + txtemp_Des.Text + " Already Exists!!!";
                    lblContent.ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    trn.insertorupdateTrn("insert into employeedesignation (Designationname) values( '" + balayer.MySQLEscapeString(balayer.ReplaceJnk(txtemp_Des.Text)) + "' )");
                    Pnlmsg.Visible = true;
                    btn_Yes.Text = "Ok";
                    this.ModalPopupExtender1.PopupControlID = "Pnlmsg";
                    this.ModalPopupExtender1.Show();
                    lblHeading.Text = "Status";
                    lblContent.Text = " Designationname : " + txtemp_Des.Text + " Inserted Successfully";
                    lblContent.ForeColor = System.Drawing.Color.Green;
                }
                trn.CommitTrn();
                logger.Info("EmployeeDetails.aspx - btnEmployee_Click():  Completed: " + DateTime.Now + " by: " + Convert.ToString(Session["UserName"]) + "");

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
}