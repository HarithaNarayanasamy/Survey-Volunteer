using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.Xml.Linq;
using System.Text.RegularExpressions;
using SVCF_BusinessAccessLayer;
using SVCF_DataAccessLayer;
using SVCF_TransactionLayer;
using log4net;
using log4net.Config;

namespace SreeVisalamChitFundLtd_phase1
{
    public partial class EmployeeDetails : System.Web.UI.Page
    {
        #region Object
        BusinessLayer balayer = new BusinessLayer();
        TransactionLayer trn=new TransactionLayer();
        #endregion

        ILog logger = log4net.LogManager.GetLogger(typeof(EmployeeDetails));

        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (Session["UserName"] == null || Session["Branchid"] == null || Session["BranchName"] == null)
            {
                Response.Redirect(Page.ResolveUrl("~/Login.aspx"), true);
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            Pnlmsg.Visible = false;
            if (!IsPostBack)
            {
                var userinfo = HttpContext.Current.User.Identity.Name;
                var qry = "select rs.name from roles as rs inner join rights as rt on (rt.roleid=rs.id) where memberid=" + userinfo + "";
                var usrRole = balayer.GetSingleValue(qry);
                if (usrRole == "Report")
                {
                    Response.Redirect("Home.aspx", false);
                }

                Session["CheckRefresh"] = System.Guid.NewGuid().ToString();
                employeedes();
            }
        }
        protected void employeedes()
        {
            DataTable Chit = new DataTable();
            Chit = balayer.GetDataTable("select EmpDes_ID,Designationname from svcf.employeedesignation;");
            DD_GP.DataSource = Chit;
            DD_GP.DataValueField = "EmpDes_ID";
            DD_GP.DataTextField = "Designationname";
            DD_GP.DataBind();
            DD_GP.Items.Insert(0, "--select--");
        }
        protected void Page_PreRender(object sender, EventArgs e)
        {
            ViewState["CheckRefresh"] = Session["CheckRefresh"];
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
            if (Session["CheckRefresh"].ToString() != ViewState["CheckRefresh"].ToString())
            {
                return;
            }
            
            try
            {
                if (balayer.ExecuteReader("SELECT * FROM svcf.employee_details where BranchID=" + Session["Branchid"] + " and Emp_PhoneNo='" + balayer.MySQLEscapeString(txtemp_PhoneNo.Text) + "' and Emp_Name='" + balayer.MySQLEscapeString(txtemp_Name.Text) + "'").HasRows)
                {
                    Pnlmsg.Visible = true;
                    btn_Yes.Text = "Ok";
                    this.ModalPopupExtender1.PopupControlID = "Pnlmsg";
                    this.ModalPopupExtender1.Show();
                    lblHeading.Text = "Status";
                    lblContent.Text = " Employee Name : " + txtemp_Name.Text + " Already Exists!!!";
                    lblContent.ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    trn.insertorupdateTrn("insert into employee_details (BranchID,Emp_Name,Emp_Address,Emp_Email,Emp_PhoneNo,Emp_Salary,Emp_Designation,Emp_SrNumber,Emp_DateOfJoining,Designation_ID) values(" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + ", '" + balayer.MySQLEscapeString(balayer.ReplaceJnk(txtemp_Name.Text)) + "', '" + balayer.MySQLEscapeString(balayer.ReplaceJnk(txtemp_Address.Text)) + "'," +
                        "'" + balayer.MySQLEscapeString(txtEmail.Text) + "','" + balayer.ReplaceJnk(txtemp_PhoneNo.Text) + "'," + balayer.ReplaceJnk(Regex.Replace(txtemp_Salary.Text, @",", "")) + "," +
                        "'" + DD_GP.SelectedItem.Text + "','" + txtSrNumber.Text + "','" + balayer.changedateformat(Convert.ToDateTime(txtDateofJoining.Text), 2) + "',"+DD_GP.SelectedValue+")");
                    Pnlmsg.Visible = true;
                    btn_Yes.Text = "Ok";
                    this.ModalPopupExtender1.PopupControlID = "Pnlmsg";
                    this.ModalPopupExtender1.Show();
                    lblHeading.Text = "Status";
                    lblContent.Text = " Employee Name : " + txtemp_Name.Text + " Inserted Successfully";
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
                {}
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
