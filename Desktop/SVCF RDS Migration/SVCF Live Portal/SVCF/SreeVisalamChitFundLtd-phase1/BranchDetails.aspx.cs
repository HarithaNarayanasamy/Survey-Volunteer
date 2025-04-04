using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Text.RegularExpressions;
using SVCF_BusinessAccessLayer;
using SVCF_TransactionLayer;
using SVCF_DataAccessLayer;
using log4net;
using log4net.Config;

namespace SreeVisalamChitFundLtd_phase1
{
    public partial class BranchDetails : System.Web.UI.Page
    {
        #region Object
        BusinessLayer balayer = new BusinessLayer();
        TransactionLayer trn = new TransactionLayer();
        #endregion
        ILog logger = log4net.LogManager.GetLogger(typeof(AuctionForms));

        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (Session["UserName"] == null || Session["Branchid"] == null || Session["BranchName"] == null)
            {
                Response.Redirect(Page.ResolveUrl("~/Login.aspx"), false);
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

                TxtPicker.ToolTip = "Ex. " + DateTime.Now.ToString("dd/MM/yyyy") + " (dd/mm/yyyy)";
                TxtBranchHead.ToolTip = "Ex. " + Session["UserName"];
                txtBranchCode.Focus();
                Session["CheckRefresh"] = System.Guid.NewGuid().ToString();
            }
        }
        protected void Page_PreRender(object sender, EventArgs e)
        {
            ViewState["CheckRefresh"] = Session["CheckRefresh"];
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.Url.AbsolutePath.ToString(),false);
        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Page.Validate("BranchValid");
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
                int iExist = int.Parse(balayer.GetSingleValue("select count(*) from branchdetails where B_Code=" + txtBranchCode.Text + " or  B_Name='" + balayer.MySQLEscapeString(txtBranchName.Text) + "' "));
                if (iExist > 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Warning", "alert('Branch Already Exists!!!!');", true);
                    txtBranchCode.Text = "";
                    txtBranchName.Text = "";
                    txtBranchCode.Focus();
                    return;
                }
                else
                {
                    string a = "Office Cash"; string b = "Office Cheque";
                    string id = "0";


                    long iresult = trn.insertorupdateTrn("insert into headstree(ParentID, Node, TreeHint) values(1,'" + balayer.MySQLEscapeString(txtBranchName.Text) + "',Null)");
                    trn.insertorupdateTrn("UPDATE headstree SET TreeHint='1," + iresult + "'" + " WHERE NodeID=" + iresult + "");
                    //25/01/2021
                    //trn.insertorupdateTrn("insert into branchdetails(Head_Id,B_Code,B_Name,B_Head,B_Address,B_PhoneNo,B_EMail,B_DOC) values(" + iresult + "," + balayer.MySQLEscapeString(txtBranchCode.Text) + ",'" + balayer.MySQLEscapeString(txtBranchName.Text) + "','" + balayer.MySQLEscapeString(TxtBranchHead.Text) + "','" + balayer.MySQLEscapeString(txtBranchAddress.Text) + "','" + balayer.MySQLEscapeString(txtPhoneNumber.Text) + "','" + balayer.MySQLEscapeString(txtEmail.Text) + "','" + balayer.indiandateToMysqlDate(TxtPicker.Text) + "')");
                    trn.insertorupdateTrn("insert into branchdetails(Head_Id,B_Code,B_Name,B_Head,B_Prefix,B_Address,B_PhoneNo,B_EMail,B_DOC) values(" + iresult + "," + balayer.MySQLEscapeString(txtBranchCode.Text) + ",'" + balayer.MySQLEscapeString(txtBranchName.Text) + "','" + balayer.MySQLEscapeString(TxtBranchHead.Text)+"','"+balayer.MySQLEscapeString(txtBranchPrefix.Text) + "','" + balayer.MySQLEscapeString(txtBranchAddress.Text) + "','" + balayer.MySQLEscapeString(txtPhoneNumber.Text) + "','" + balayer.MySQLEscapeString(txtEmail.Text) + "','" + balayer.indiandateToMysqlDate(TxtPicker.Text) + "')");

                    trn.insertorupdateTrn("insert into svcf.moneycollector(BranchID,moneycollname,moneycolladdress,moneycollphno,employeeid)values('" + iresult + "','" + a + "','" + balayer.MySQLEscapeString(txtBranchAddress.Text) + "','" + balayer.MySQLEscapeString(txtPhoneNumber.Text) + "','" + id + "')");
                    trn.insertorupdateTrn("insert into svcf.moneycollector(BranchID,moneycollname,moneycolladdress,moneycollphno,employeeid)values('" + iresult + "','" + b + "','" + balayer.MySQLEscapeString(txtBranchAddress.Text) + "','" + balayer.MySQLEscapeString(txtPhoneNumber.Text) + "','" + id + "')");
                 
                    trn.insertorupdateTrn("insert into membermaster(`BranchId`,`ApprovedStatus`,`CustomerName`,`TypeOfMember`,`compxerox`,`dateofResol`,`ProfessionBusiness`,`NatureofProfessionBusiness`,`ResidentialAddress`,`AddressForCommunication`,`AddressProfessionBusiness`,`TelephoneNoProfessionBusiness`,`TelephoneNoResidence`,`MobileNo`,`MonthlyIncome`,`SalesTaxRegistrationNoTNGST`,`CSTRegistrationNumber`,`IncomeTaxPANoWardandCircle`,`BankName`,`BranchName`,`SavingsCurrentAccountNo`,`MemberID`) values(" + iresult + ",'1','Sree Visalam Chit Fund Ltd','Foreman','','0000-00-00','','','" + balayer.MySQLEscapeString(txtBranchAddress.Text) + "','" + balayer.MySQLEscapeString(txtBranchAddress.Text) + "','" + balayer.MySQLEscapeString(txtBranchAddress.Text) + "','" + balayer.MySQLEscapeString(txtPhoneNumber.Text) + "','" + balayer.MySQLEscapeString(txtPhoneNumber.Text) + "','','0','','','','','','','" + balayer.MySQLEscapeString(txtBranchCode.Text + "-1") + "')");
                    trn.insertorupdateTrn("insert into membermaster(`BranchId`,`ApprovedStatus`,`CustomerName`,`TypeOfMember`,`compxerox`,`dateofResol`,`ProfessionBusiness`,`NatureofProfessionBusiness`,`ResidentialAddress`,`AddressForCommunication`,`AddressProfessionBusiness`,`TelephoneNoProfessionBusiness`,`TelephoneNoResidence`,`MobileNo`,`MonthlyIncome`,`SalesTaxRegistrationNoTNGST`,`CSTRegistrationNumber`,`IncomeTaxPANoWardandCircle`,`BankName`,`BranchName`,`SavingsCurrentAccountNo`,`MemberID`) values(" + iresult + ",'1','Nil','','','0000-00-00','','','" + balayer.MySQLEscapeString(txtBranchAddress.Text) + "','" + balayer.MySQLEscapeString(txtBranchAddress.Text) + "','" + balayer.MySQLEscapeString(txtBranchAddress.Text) + "','" + balayer.MySQLEscapeString(txtPhoneNumber.Text) + "','" + balayer.MySQLEscapeString(txtPhoneNumber.Text) + "','','0','','','','','','','" + balayer.MySQLEscapeString(txtBranchCode.Text + "-0") + "')");
                    trn.insertorupdateTrn("insert into prospect(`BranchId`,`ProspectID`,`CustomerName`,`TypeOfMember`,`ResidentialAddress`,`AddressForCommunication`,`MobileNo`,`Title`,`ApprovedStatus`,`ReffererDetails`,`JoinedDate`,`Gender`,`Age`,`ProfessionBusiness`,`MonthlyIncome`,`DOB`) values (" + iresult + ",'" + txtBranchCode.Text + "-1" + "','" + balayer.MySQLEscapeString("Sree Visalam Chit Fund") + "','Company','" + balayer.MySQLEscapeString(txtBranchAddress.Text) + "','" + balayer.MySQLEscapeString(txtBranchAddress.Text) + "','" + txtPhoneNumber.Text + "','','1','self','" + balayer.indiandateToMysqlDate(TxtPicker.Text) + "','','','','0.00','0000-00-00')");
                    ModalPopupExtender2.PopupControlID = "Pnlmsg";
                    this.ModalPopupExtender2.Show();
                    Pnlmsg.Visible = true;
                    lblT.Text = "Status";
                    lblContent.Text = " Branch Code : " + balayer.MySQLEscapeString(txtBranchCode.Text) + " & Branch Name : " + balayer.MySQLEscapeString(Regex.Replace(txtBranchName.Text, @"\s+", "")) + " Added Successfully";
                    lblContent.ForeColor = System.Drawing.Color.Green;
                }
                trn.CommitTrn();
                logger.Info("BranchDetails.aspx - btnAdd_Click() - Completed: " + DateTime.Now + " by: " + Convert.ToString(Session["UserName"]) + "");
            }
            catch (Exception error)
            {
                try
                {
                    trn.RollbackTrn();
                    logger.Info("BranchDetails.aspx - btnAdd_Click() - Error: " + error.Message + ": " + DateTime.Now + " by: " + Convert.ToString(Session["UserName"]) + "");
                }
                catch (Exception ex) { }
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
        protected void btnclose(object sender, EventArgs e)
        {
            Response.Redirect(Request.Url.AbsolutePath.ToString(), false);
        }
    }
}
