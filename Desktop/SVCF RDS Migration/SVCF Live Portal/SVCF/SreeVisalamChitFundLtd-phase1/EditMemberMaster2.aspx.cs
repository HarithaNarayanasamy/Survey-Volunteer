
using MySql.Data.MySqlClient;
using SVCF_BusinessAccessLayer;
using SVCF_DataAccessLayer;
using SVCF_TransactionLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Web.Services;
using System.Web.UI.HtmlControls;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html;
using iTextSharp.text.html.simpleparser;


namespace SreeVisalamChitFundLtd_phase1
{
    public partial class EditMemberMaster2 : System.Web.UI.Page
    {
        #region Object
        BusinessLayer balayer = new BusinessLayer();
        CommonClassFile objCommonClass = new CommonClassFile();
        TransactionLayer trn = new TransactionLayer();
        string query="";
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var userinfo = HttpContext.Current.User.Identity.Name;
                var qry = "select rs.name from roles as rs inner join rights as rt on (rt.roleid=rs.id) where memberid=" + userinfo + "";
                var usrRole = balayer.GetSingleValue(qry);
                if (usrRole == "Administrator")
                {
                    if (Request.QueryString["TokenNo"].ToString() != "")
                    {
                        BtnUpdate.Visible = true;
                        Button1.Visible = false;
                    }
                    else
                    {
                        BtnUpdate.Visible = true;
                        Button1.Visible = true;
                    }
                }
                else
                {
                    BtnUpdate.Visible = false;
                    Button1.Visible = false;
                }
                //show();
               if(Request.QueryString["MemberID"].ToString() != "")
               {
                   string Memberid = Request.QueryString["MemberID"];
                   string BranchId = Request.QueryString["BranchId"];
                   BindDetails(Memberid);
               }
            }
        }

        public void BindBranchInfo()
        {
            try
            {
                string query = "select NodeID,Node from headstree where ParentID=1;";
                DataTable BranchList = balayer.GetDataTable(query);
                drpdownbranchList.DataSource = BranchList;
                DataRow dr = null;
                dr = BranchList.NewRow();
                dr[1] = "--select--";
                dr[0] = "0";
                BranchList.Rows.InsertAt(dr, 0);
                drpdownbranchList.DataTextField = "Node";
                drpdownbranchList.DataValueField = "NodeID";
                drpdownbranchList.DataBind();
            }
            catch (Exception) { }
        }

        public void BindDetails(string MemberID)
        {
           BindBranchInfo();
            DataTable dtM = new DataTable();
            string query = "select * from `membermaster` as ms left join `membersdocuments` as md ON `ms`.`MemberIDNew` = `md`.`MemberID`  " +
                "left join headstree on NodeID=ms.BranchId where ms.MemberIDNew='" + MemberID + "'";
            dtM = balayer.GetDataTable(query);
            if (dtM.Rows.Count > 0)
            {
                CustImage.ImageUrl = dtM.Rows[0]["ImageUrl"].ToString();
              //  txtBranchID.Text = dtM.Rows[0]["BranchId"].ToString();
              
                txtCustomername.Text = dtM.Rows[0]["CustomerName"].ToString();
                txttypeofmember.Text = dtM.Rows[0]["TypeOfMember"].ToString();
                txtage.Text = dtM.Rows[0]["Age"].ToString();
                txtdob.Text = dtM.Rows[0]["DOB"].ToString();
                txtfatherhusname.Text = dtM.Rows[0]["FatherHusbandName"].ToString();
                txtmotherwifename.Text = dtM.Rows[0]["MotherWifeName"].ToString();
                txtdateofresolution.Text = dtM.Rows[0]["dateofResol"].ToString();
                txtproprietorName.Text = dtM.Rows[0]["ProprietorName"].ToString();
                txtcompxerox.Text = dtM.Rows[0]["compxerox"].ToString();
                txtprofessionbusiness.Text = dtM.Rows[0]["ProfessionBusiness"].ToString();
                txtnatureofprofession.Text = dtM.Rows[0]["NatureofProfessionBusiness"].ToString();
                txtresidential.Text = dtM.Rows[0]["ResidentialAddress"].ToString();
                txtaddressforprofessionbusiness.Text = dtM.Rows[0]["AddressProfessionBusiness"].ToString();
                txtphoneno.Text = dtM.Rows[0]["TelephoneNoResidence"].ToString();
                txtmothlyincome.Text = dtM.Rows[0]["MonthlyIncome"].ToString();
                txtmobileno.Text = dtM.Rows[0]["MobileNo"].ToString();
                txttaxreg.Text = dtM.Rows[0]["SalesTaxRegistrationNoTNGST"].ToString();
                txtcstreg.Text = dtM.Rows[0]["CSTRegistrationNumber"].ToString();
                txtincome.Text = dtM.Rows[0]["IncomeTaxPANoWardandCircle"].ToString();
                txtbankname.Text = dtM.Rows[0]["BankName"].ToString();
                txtbranchname.Text = dtM.Rows[0]["Node"].ToString();
                txtsavingcurrentaccountno.Text = dtM.Rows[0]["SavingsCurrentAccountNo"].ToString();
                //  txtsavingcurrentaccountno.Text = dtM.Rows[0]["SavingsCurrentAccountNo"].ToString();
                txtadhar.Text = dtM.Rows[0]["AadharNumber"].ToString();
                txtpartnersname.Text = dtM.Rows[0]["PartnersName"].ToString();
                txtdateofpartnership.Text = dtM.Rows[0]["DateOfPartnershipWithAmendment"].ToString();
                // txtresidential.Text = dtM.Rows[0]["ResidentialAddress"].ToString();
                txtaddressforcommunication.Text = dtM.Rows[0]["AddressForCommunication"].ToString();
                txtphonenoprofession.Text = dtM.Rows[0]["TelephoneNoProfessionBusiness"].ToString();
                txttaxreg.Text = dtM.Rows[0]["SalesTaxRegistrationNoTNGST"].ToString();
                drpdownbranchList.SelectedValue = Convert.ToString(dtM.Rows[0]["BranchId"]);
                lblMemberid.Text = Convert.ToString(dtM.Rows[0]["MemberID"]);
                txtProof.Text = Convert.ToString(dtM.Rows[0]["ProofofResidence"]);              
            }

        }


        [WebMethod]
        public static List<System.Web.UI.WebControls.ListItem> Getbranchlist(string BranchId)
        {

            BusinessLayer balayer = new BusinessLayer();
            string query = "";
            query = "SELECT NodeID,Node FROM svcf.headstree where ParentID=1";


            List<System.Web.UI.WebControls.ListItem> branches = new List<System.Web.UI.WebControls.ListItem>();

            branches = balayer.Getlistdata(query);

            return branches;

        }

        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                string uploadedPath = "";
                string filename = "";
                string ext = "";
                string qry = "";
                string selectedfilename = "";
                int MemberID = Convert.ToInt32(Request.QueryString["MemberID"]);
                if (fileuploadEmpImage.HasFile)
                {
                    filename = Path.GetFileName(fileuploadEmpImage.PostedFile.FileName);
                    selectedfilename = Path.GetFileName(fileuploadEmpImage.PostedFile.FileName);
                    ext = System.IO.Path.GetExtension(fileuploadEmpImage.FileName);
                    string DirectoryPath = Server.MapPath(@"~/MemberImages/");
                    if (!Directory.Exists(DirectoryPath))
                    {
                        Directory.CreateDirectory(DirectoryPath);
                    }
                    filename = DirectoryPath + filename;
                    if (File.Exists(filename))
                    {
                        File.Delete(filename);
                    }
                    fileuploadEmpImage.PostedFile.SaveAs(filename);
                    uploadedPath = "~/MemberImages/" + selectedfilename;
                    //Updating Image
                    //  int MemberID = Convert.ToInt32(balayer.GetSingleValue("select MemberIDNew from membermaster where MemberID='" + Request.QueryString["MemberID"].ToString() + "'"));
                   
                    balayer.GetInsertItem("delete from membersdocuments where MemberID=" + MemberID + "");
                    //MemberID, Photo, ImageTYpe, IsDeleted, ImageUrl
                    qry = "insert into membersdocuments(MemberID, ImageTYpe, IsDeleted, ImageUrl) values(" + MemberID + ",'" + fileuploadEmpImage.PostedFile.ContentType + "'," +
                        "" + 0 + ",'" + uploadedPath + "')";
                    balayer.GetInsertItem(qry);
                }
                 string branchid = HD_branchval.Value;
                 string branchname = "";
                 if ((branchid != "") && (branchid != null))
                 {
                     branchname = balayer.GetSingleValue("SELECT Node FROM svcf.headstree where parentid=1 and NodeID=" + branchid + "");
                 }
                 else
                 {
                     branchname = txtbranchname.Text;
                 }
                //string usrRole = "";
                //string userinfo = "";
                //userinfo = HttpContext.Current.User.Identity.Name;
                //qry = "select rs.name from roles as rs inner join rights as rt on (rt.roleid=rs.id) where memberid=" + userinfo + "";
                //usrRole = balayer.GetSingleValue(qry);
                //if (usrRole == "Administrator")
                //{
                    if (txtbranchname.Text == branchname)
                    {
                        int UpdateIndidual = balayer.GetInsertItem("update membermaster as m1,membertogroupmaster as mg1 set m1.CustomerName='" + txtCustomername.Text + "',m1.TypeOfMember='" + txttypeofmember.Text + "',m1.Age='" + txtage.Text + "'," +
                                  "m1.FatherHusbandName='" + txtfatherhusname.Text + "',m1.MotherWifeName='" + txtmotherwifename.Text + "',m1.ProprietorName='" + txtproprietorName.Text + "'," +
                                  "m1.PartnersName='" + txtpartnersname.Text + "',m1.ProfessionBusiness='" + txtprofessionbusiness.Text + "',m1.NatureofProfessionBusiness='" + txtnatureofprofession.Text + "'," +
                                  "m1.ResidentialAddress='" + txtresidential.Text + "',m1.AddressForCommunication='" + txtaddressforcommunication.Text + "',m1.AddressProfessionBusiness='" + txtaddressforprofessionbusiness.Text + "'," +
                                  "m1.TelephoneNoProfessionBusiness='" + txtphonenoprofession.Text + "',m1.TelephoneNoResidence='" + txtphoneno.Text + "',m1.MobileNo='" + txtmobileno.Text + "'," +
                                  "m1.MonthlyIncome=" + txtmothlyincome.Text + ",m1.SalesTaxRegistrationNoTNGST='" + txttaxreg.Text + "',m1.CSTRegistrationNumber='" + txtcstreg.Text + "'," +
                                  "m1.IncomeTaxPANoWardandCircle='" + txtincome.Text + "'," +
                                  "m1.BankName='" + txtbankname.Text + "',m1.SavingsCurrentAccountNo='" + txtsavingcurrentaccountno.Text + "',m1.AadharNumber='" + txtadhar.Text + "',mg1.MemberName='" + txtCustomername.Text + "',mg1.MemberAddress='" + txtaddressforcommunication.Text + "' where (m1.MemberIDNew=mg1.MemberID) and m1.MemberIDNew='" + MemberID + "'");
                    }
                    else
                    {
                        string Memberid = balayer.GetSingleValue("SELECT max(MemberID)+1 FROM svcf.membermaster where branchid=" + branchid + "");
                        int UpdateIndidual = balayer.GetInsertItem("update membermaster set CustomerName='" + txtCustomername.Text + "',TypeOfMember='" + txttypeofmember.Text + "',Age='" + txtage.Text + "'," +
                           "FatherHusbandName='" + txtfatherhusname.Text + "',MotherWifeName='" + txtmotherwifename.Text + "',ProprietorName='" + txtproprietorName.Text + "'," +
                           "PartnersName='" + txtpartnersname.Text + "',ProfessionBusiness='" + txtprofessionbusiness.Text + "',NatureofProfessionBusiness='" + txtnatureofprofession.Text + "'," +
                           "ResidentialAddress='" + txtresidential.Text + "',AddressForCommunication='" + txtaddressforcommunication.Text + "',AddressProfessionBusiness='" + txtaddressforprofessionbusiness.Text + "'," +
                           "TelephoneNoProfessionBusiness='" + txtphonenoprofession.Text + "',TelephoneNoResidence='" + txtphoneno.Text + "',MobileNo='" + txtmobileno.Text + "'," +
                           "MonthlyIncome=" + txtmothlyincome.Text + ",SalesTaxRegistrationNoTNGST='" + txttaxreg.Text + "',CSTRegistrationNumber='" + txtcstreg.Text + "'," +
                           "IncomeTaxPANoWardandCircle='" + txtincome.Text + "',BranchId=" + branchid + ",MemberID=" + Memberid + "," +
                           "BankName='" + txtbankname.Text + "',SavingsCurrentAccountNo='" + txtsavingcurrentaccountno.Text + "',AadharNumber='" + txtadhar.Text + "' where MemberIDNew='" + MemberID + "'");
                    }
                //}
                //else
                //{
                //    Response.Redirect("Home.aspx", true);
                //}

                Button1.Visible = false;
               
            }
            catch (Exception)
            {

            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string Memberid = Request.QueryString["MemberID"];
            string BranchId = Request.QueryString["BranchId"];
            DataTable dtM = new DataTable();
            string query = "select * from `membermaster` as ms left join `membersdocuments` as md ON `ms`.`MemberIDNew` = `md`.`MemberID`  " +
                "left join headstree on NodeID=ms.BranchId where ms.MemberIDNew='" + Memberid + "'";
            dtM = balayer.GetDataTable(query);
            string MemberID = Convert.ToString(dtM.Rows[0]["MemberID"]);
            balayer.GetInsertItem("delete from membermaster where MemberID=" + MemberID + " and BranchID='" + BranchId + "';");
            Response.Redirect("~/EditMemberMaster3.aspx", true);
        }

        void show()
        {
            string Memberid = Request.QueryString["MemberID"];
            string BranchId = Request.QueryString["BranchId"];
            DataTable dtM = new DataTable();
            string query = "select * from svcf.membertogroupmaster where MemberID='" + Memberid + "' and BranchID='"+ BranchId + "';";
            dtM = balayer.GetDataTable(query);
            if(dtM.Rows.Count >0)
            {
                Button1.Visible = false;
            }
            else
            {
                Button1.Visible = true;
            }
        }
    }


  
}