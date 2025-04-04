using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SVCF_BusinessAccessLayer;
using SVCF_TransactionLayer;
using SVCF_DataAccessLayer;
using System.Data;
using System.Text;
using System.Configuration;
using MySql.Data.MySqlClient;
using System.Net;   //13/07/2021
using log4net;  //29/07/2021
using log4net.Config;


namespace SreeVisalamChitFundLtd_phase1
{
    public partial class WebAdminEdit : System.Web.UI.Page
    {
        BusinessLayer balayer = new BusinessLayer();
        TransactionLayer trn = new TransactionLayer();
        CommonVariables objCOM = new CommonVariables();

        ILog logger = log4net.LogManager.GetLogger(typeof(WebAdminEdit));

        string domain = "http://3.7.244.11/WebsiteImages/";
        string imgPath = "";
        string fileName = "";
        string username = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PwdPanel.Visible = false;

            }
        }
        protected void btnPassword_Click(object sender, EventArgs e)
        {
            PwdPanel.Visible = true;
            pnlWebsite.Visible = false;
            pnlChitBlock.Visible = false;
            lblEmployee.Visible = false;
            ddlEmployee.Visible = false;
            lblUsername.Visible = false;
            txtUsername.Visible = false;
            lblPwd.Visible = false;
            txtPwd.Visible = false;
            btnBlock.Visible = false;
            btnUnBlock.Visible = false;
            btnUpdate.Visible = false;
            gridEmpReport.Visible = false;
            loadBranch();

        }
        public void loadBranch()
        {
            DataTable dtBranch = balayer.GetDataTable("SELECT NodeID,Node FROM svcf.headstree where ParentID=1;");
            DataRow dr = dtBranch.NewRow();
            dr[0] = "0";
            dr[1] = "--Select--";
            ddlBranch.DataValueField = "NodeID";
            ddlBranch.DataTextField = "Node";
            dtBranch.Rows.InsertAt(dr, 0);
            ddlBranch.DataSource = dtBranch;
            ddlBranch.DataBind();
        }
        public void loadMoneyColl()
        {
            DataTable dtMoneyColl = balayer.GetDataTable("SELECT moneycollid,moneycollname FROM svcf.moneycollector where BranchID=" + ddlBranch.SelectedValue + ";");
            DataRow dr = dtMoneyColl.NewRow();
            dr[0] = "0";
            dr[1] = "--Select--";
            ddlEmployee.DataValueField = "moneycollid";
            ddlEmployee.DataTextField = "moneycollname";
            dtMoneyColl.Rows.InsertAt(dr, 0);
            ddlEmployee.DataSource = dtMoneyColl;
            ddlEmployee.DataBind();
        }

        protected void ddlEmployee_SelectedIndexChanged(object sender, EventArgs e)
        {
            gridEmpReport.Visible = false;
            lblUsername.Visible = true;
            txtUsername.Visible = true;
            lblPwd.Visible = true;
            txtPwd.Visible = true;
            btnUpdate.Visible = true;
            btnBlock.Visible = true;
            btnUnBlock.Visible = true;
            username = balayer.GetSingleValue("Select username from svcf.moneycollector where moneycollid=" + ddlEmployee.SelectedValue);
            txtUsername.Text = username;
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                var moneycoll = ddlEmployee.SelectedItem.Value;
                //28/11/2022 bagya - username password changes
                username = balayer.GetSingleValue("Select username from svcf.moneycollector where moneycollid=" + ddlEmployee.SelectedValue);
                if (username != txtUsername.Text)
                {
                    var available = balayer.GetSingleValue("select 1 from membermaster where username='" + txtUsername.Text + "' union select 1 from moneycollector where username='" + txtUsername.Text + "';");
                if (available == "1")
                {
                    Response.Write("<script>alert('Username already exists.Please try with another!');</script>");
                }
                else
                {
                    string qry = "update svcf.moneycollector set username='" + txtUsername.Text + "', password='" + txtPwd.Text + "' where moneycollid=" + moneycoll + ";";
                    trn.insertorupdateTrn(qry);
                    txtUsername.Text = "";
                    txtPwd.Text = "";
                    Response.Write("<script>alert('Username & Password updated successfully.');</script>");
                }
                   
                }
                else
                {
                    string qry = "update svcf.moneycollector set password='" + txtPwd.Text + "' where moneycollid=" + moneycoll + ";";
                    trn.insertorupdateTrn(qry);
                    txtUsername.Text = "";
                    txtPwd.Text = "";
                    Response.Write("<script>alert('Password updated successfully.');</script>");
                }
               
            }
            catch (Exception ex)
            {

            }
        }

        protected void ddlBranch_SelectedItemChanged(object sender, EventArgs e)
        {
            loadMoneyColl();
            lblUsername.Visible = true;
            txtUsername.Visible = true;
            lblPwd.Visible = true;
            txtPwd.Visible = true;
            btnUpdate.Visible = true;
            btnBlock.Visible = true;
            btnUnBlock.Visible = true;
        }

        protected void btnGo_Click(object sender, EventArgs e)
        {
            if (ddlBranch.SelectedItem.Text == "--Select--")
            {
                Response.Write("<script>alert('Please select Branch');</script>");
            }
            else
            {
                lblEmployee.Visible = true;
                ddlEmployee.Visible = true;
            }
        }

        protected void btnBlock_Click(object sender, EventArgs e)
        {
            if (ddlEmployee.SelectedItem.Text == "--Select--")
            {
                Response.Write("<script>alert('Please select Money Collector');</script>");
            }
            else
            {
                var blockStatus = balayer.GetSingleValue("select IsBlocked from moneycollector where moneycollid=" + ddlEmployee.SelectedItem.Value);
                if (Convert.ToInt16(blockStatus) == 1)
                {
                    Response.Write("<script>alert('Money Collector is already blocked!');</script>");
                }
                else
                {
                    btnOk.Visible = true;
                    btnConfirm.Visible = false;
                    lblMsg.Text = "Are you sure to Block this Money Collector for Mobile App?";
                    modalPopup1.TargetControlID = "show";
                    modalPopup1.PopupControlID = "popUp";
                    modalPopup1.Show();
                }
            }
        }

        protected void btnOk_Click(object sender, EventArgs e)
        {
            modalPopup1.Hide();
            var qry = "update svcf.moneycollector set IsBlocked=true where moneycollid=" + ddlEmployee.SelectedItem.Value + ";";
            trn.insertorupdateTrn(qry);

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            modalPopup1.Hide();
        }

        protected void btnUnBlock_Click(object sender, EventArgs e)
        {
            if (ddlEmployee.SelectedItem.Text == "--Select--")
            {
                Response.Write("<script>alert('Please select Money Collector');</script>");
            }
            else
            {
                var blockStatus = balayer.GetSingleValue("select IsBlocked from moneycollector where moneycollid=" + ddlEmployee.SelectedItem.Value);
                if (Convert.ToInt16(blockStatus) == 0)
                {
                    Response.Write("<script>alert('Money Collector is already Unblocked!');</script>");
                }
                else
                {
                    btnConfirm.Visible = true;
                    btnOk.Visible = false;
                    lblMsg.Text = "Are you sure to UnBlock this Money Collector for Mobile App?";
                    modalPopup1.TargetControlID = "show";
                    modalPopup1.PopupControlID = "popUp";
                    modalPopup1.Show();
                }
            }
        }

        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            modalPopup1.Hide();
            var qry = "update svcf.moneycollector set IsBlocked=false where moneycollid=" + ddlEmployee.SelectedItem.Value + ";";
            trn.insertorupdateTrn(qry);
        }

        protected void btnScroll_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dtScroll = balayer.GetDataTable("select ImgPath,FromDate,ToDate from websiteimages where MenuNames='ScrollMessage';");

                if (dtScroll.Rows.Count > 0)
                {
                    string qry1 = "update websiteimages set ImgPath='" + txtScrollMsg.Text + "',FromDate='" + Convert.ToDateTime(txtFrom.Text).ToString("yyyy/MM/dd") + "',ToDate='" + Convert.ToDateTime(txtTo.Text).ToString("yyyy/MM/dd") + "' where MenuNames='ScrollMessage';";
                    var ins = trn.insertorupdateTrn(qry1);
                    Response.Write("<script>alert('Updated Successfully!');</script>");

                }
            }
            catch (Exception ex)
            { }
        }

        protected void btnWebsite_Click(object sender, EventArgs e)
        {
            pnlWebsite.Visible = true;
            PwdPanel.Visible = false;
            pnlChitBlock.Visible = false;
            DataTable dtScroll = balayer.GetDataTable("select ImgPath,FromDate,ToDate from websiteimages where MenuNames='ScrollMessage';");
            if (!string.IsNullOrEmpty(dtScroll.Rows[0]["ImgPath"].ToString()))
            {
                txtScrollMsg.Text = dtScroll.Rows[0]["ImgPath"].ToString();
                string frmDate = dtScroll.Rows[0]["FromDate"].ToString();
                if (!string.IsNullOrEmpty(frmDate))
                    txtFrom.Text = Convert.ToDateTime(dtScroll.Rows[0]["FromDate"].ToString()).ToString("dd/MM/yyyy");
                string toDate = dtScroll.Rows[0]["ToDate"].ToString();
                if (!string.IsNullOrEmpty(toDate))
                    txtTo.Text = Convert.ToDateTime(dtScroll.Rows[0]["ToDate"].ToString()).ToString("dd/MM/yyyy");
            }
            else
            {
                //Response.Redirect(Request.RawUrl);
                Response.Write("<script>alert('There is no information to scroll in the Website!');</script>");
            }

        }

        protected void btnHome_Click(object sender, EventArgs e)
        {

            try
            {
                if (FileUploadHome.HasFile)
                {
                    fileName = FileUploadHome.FileName;
                    FileUploadHome.PostedFile.SaveAs(Server.MapPath("~/WebsiteImages/") + FileUploadHome.FileName);
                    imgPath = domain + fileName;

                    string qry = "update websiteimages set ImgPath='" + imgPath + "' where MenuNames='Home';";
                    var update = trn.insertorupdateTrn(qry);
                    Response.Write("<script>alert('Image update successfully!');</script>");
                }
                else
                {
                    Response.Write("<script>alert('Please select a file to update');</script>");
                }
            }
            catch (Exception ex)
            { }
        }

        protected void btnAbout_Click(object sender, EventArgs e)
        {
            try
            {
                if (FileUploadAbout.HasFile)
                {
                    fileName = FileUploadAbout.FileName;
                    FileUploadAbout.PostedFile.SaveAs(Server.MapPath("~/WebsiteImages/") + FileUploadAbout.FileName);
                    imgPath = domain + fileName;
                    string qry = "update websiteimages set ImgPath='" + imgPath + "' where MenuNames='About Us';";
                    var update = trn.insertorupdateTrn(qry);
                    Response.Write("<script>alert('Image update successfully!');</script>");
                }
                else
                {
                    Response.Write("<script>alert('Please select a file to update');</script>");
                }
            }
            catch (Exception ex)
            { }
        }

        protected void btnGeneral_Click(object sender, EventArgs e)
        {
            try
            {
                if (FileUploadGeneral.HasFile)
                {
                    fileName = FileUploadGeneral.FileName;
                    FileUploadGeneral.PostedFile.SaveAs(Server.MapPath("~/WebsiteImages/") + FileUploadGeneral.FileName);
                    imgPath = domain + fileName;
                    string qry = "update websiteimages set ImgPath='" + imgPath + "' where MenuNames='General Info';";
                    var update = trn.insertorupdateTrn(qry);
                    Response.Write("<script>alert('Image update successfully!');</script>");
                }
                else
                {
                    Response.Write("<script>alert('Please select a file to update');</script>");
                }
            }
            catch (Exception ex)
            { }
        }

        protected void btnBranches_Click(object sender, EventArgs e)
        {
            try
            {
                if (FileUploadBranches.HasFile)
                {
                    fileName = FileUploadBranches.FileName;
                    FileUploadBranches.PostedFile.SaveAs(Server.MapPath("~/WebsiteImages/") + FileUploadBranches.FileName);
                    imgPath = domain + fileName;
                    string qry = "update websiteimages set ImgPath='" + imgPath + "' where MenuNames='Branches';";
                    var update = trn.insertorupdateTrn(qry);
                    Response.Write("<script>alert('Image update successfully!');</script>");
                }
                else
                {
                    Response.Write("<script>alert('Please select a file to update');</script>");
                }
            }
            catch (Exception ex)
            { }
        }

        protected void btnProducts_Click(object sender, EventArgs e)
        {
            try
            {
                if (FileUploadProducts.HasFile)
                {
                    fileName = FileUploadProducts.FileName;
                    FileUploadProducts.PostedFile.SaveAs(Server.MapPath("~/WebsiteImages/") + FileUploadProducts.FileName);
                    imgPath = domain + fileName;
                    string qry = "update websiteimages set ImgPath='" + imgPath + "' where MenuNames='Products';";
                    var update = trn.insertorupdateTrn(qry);
                    Response.Write("<script>alert('Image update successfully!');</script>");
                }
                else
                {
                    Response.Write("<script>alert('Please select a file to update');</script>");
                }
            }
            catch (Exception ex)
            { }
        }

        protected void btnContact_Click(object sender, EventArgs e)
        {
            try
            {
                if (FileUploadContact.HasFile)
                {
                    fileName = FileUploadContact.FileName;
                    FileUploadContact.PostedFile.SaveAs(Server.MapPath("~/WebsiteImages/") + FileUploadContact.FileName);
                    imgPath = domain + fileName;
                    string qry = "update websiteimages set ImgPath='" + imgPath + "' where MenuNames='Contact';";
                    var update = trn.insertorupdateTrn(qry);
                    Response.Write("<script>alert('Image update successfully!');</script>");
                }
                else
                {
                    Response.Write("<script>alert('Please select a file to update');</script>");
                }
            }
            catch (Exception ex)
            { }
        }

        protected void btnCareer_Click(object sender, EventArgs e)
        {
            try
            {
                if (FileUploadCareer.HasFile)
                {
                    fileName = FileUploadHome.FileName;
                    FileUploadCareer.PostedFile.SaveAs(Server.MapPath("~/WebsiteImages/") + FileUploadCareer.FileName);
                    imgPath = domain + fileName;
                    string qry = "update websiteimages set ImgPath='" + imgPath + "' where MenuNames='Career';";
                    var update = trn.insertorupdateTrn(qry);
                    Response.Write("<script>alert('Image update successfully!');</script>");
                }
                else
                {
                    Response.Write("<script>alert('Please select a file to update');</script>");
                }
            }
            catch (Exception ex)
            { }
        }

        protected void btnChitBlock_Click(object sender, EventArgs e)
        {
            pnlChitBlock.Visible = true;
            pnlWebsite.Visible = false;
            PwdPanel.Visible = false;
            gridReport.Visible = false;
            LoadBranchList();
        }

        public void LoadBranchList()
        {
            DataTable dtBranch = balayer.GetDataTable("SELECT NodeID,Node FROM svcf.headstree where ParentID=1;");
            DataRow dr = dtBranch.NewRow();
            dr[0] = "0";
            dr[1] = "--Select--";
            ddlBranchList.DataValueField = "NodeID";
            ddlBranchList.DataTextField = "Node";
            dtBranch.Rows.InsertAt(dr, 0);
            ddlBranchList.DataSource = dtBranch;
            ddlBranchList.DataBind();
        }

        [System.Web.Services.WebMethod]
        public static List<ListItem> ChitToken(string branchid)
        {
            BusinessLayer blayer = new BusinessLayer();
            List<ListItem> TList = new List<ListItem>();
            TList.Clear();
            TList = blayer.BindTokenist("SELECT Head_Id,GrpMemberID FROM membertogroupmaster where BranchID=" + branchid + " ");
            return TList;
        }

        [System.Web.Services.WebMethod]
        public static List<ListItem> Getsrchlist(string branchid, string seltext)
        {
            BusinessLayer blayer = new BusinessLayer();
            List<ListItem> TList = new List<ListItem>();
            TList.Clear();
            TList = blayer.BindTokenist("SELECT Head_Id,GrpMemberID FROM membertogroupmaster where BranchID=" + branchid + " and  GrpMemberID like '%" + seltext + "%'");
            return TList;
        }

        [System.Web.Services.WebMethod]
        public static string GetCustomerName(string hdid)
        {
            string custname = "";
            try
            {
                BusinessLayer blayer = new BusinessLayer();
                custname = blayer.GetSingleValue("select MemberName from membertogroupmaster where Head_Id=" + hdid + "");
                string _filePath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

            }
            catch (Exception ex) { }
            return custname;
        }

        protected void btnBlockToken_Click(object sender, EventArgs e)
        {
            //27/07/2021
            if (tkn_id.Value == "")
            {
                Response.Write("<script>alert('Please select a Chit!.');</script>");
            }
            //21/07/2021
            string qry = "update svcf.membertogroupmaster set IsBlocked=1,BlockReason='" + txtBlockReason.Text + "',BlockedOn='" + DateTime.Now.ToString("yyyy-MM-dd") + "' where Head_Id=" + tkn_id.Value;
            trn.insertorupdateTrn(qry);
            Response.Write("<script>alert('The selected chit token has been Blocked!.');</script>");
            //bagya 01/02/2023
            txtBlockReason.Text = "";
            txtCustomerName.Text = "";


        }

        protected void btnUnBlockToken_Click(object sender, EventArgs e)
        {
            //27/07/2021
            if (tkn_id.Value == "")
            {
                Response.Write("<script>alert('Please select a Chit!.');</script>");
            }
            string qry = "update svcf.membertogroupmaster set IsBlocked=0,UnBlockedOn='" + DateTime.Now.ToString("yyyy-MM-dd") + "' where Head_Id=" + tkn_id.Value;
            trn.insertorupdateTrn(qry);
            Response.Write("<script>alert('The selected chit token has been UnBlocked!.');</script>");
            //bagya 01/02/2023
            txtBlockReason.Text = "";
            txtCustomerName.Text = "";

        }

        protected void btnBlockReport_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlBranchList.SelectedItem.Text == "--Select--")
                {
                    Response.Write("<script>alert('Please select a branch');</script>");
                }
                else
                {
                    //01/02/2023 bagya
                    gridReport.Visible = true;
                    var qry = @"SELECT b1.B_Name,mg1.MemberName,mg1.GrpMemberID,case when mg1.IsBlocked=1 then 'Blocked' else 'UnBlocked' end as Status,mg1.BlockReason,mg1.BlockedOn,mg1.UnBlockedOn FROM svcf.membertogroupmaster as mg1 join svcf.branchdetails as b1 on (mg1.BranchID=b1.Head_Id) where mg1.BlockReason<>'' and mg1.BranchID=" + ddlBranchList.SelectedItem.Value + " ;";
                    DataTable dt = balayer.GetDataTable(qry);
                    gridReport.DataSource = dt;
                    gridReport.DataBind();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnEmployeeReport_Click(object sender, EventArgs e)
        {
            try
            {
                lblEmployee.Visible = false;
                ddlEmployee.Visible = false;
                lblUsername.Visible = false;
                txtUsername.Visible = false;
                lblPwd.Visible = false;
                txtPwd.Visible = false;
                btnBlock.Visible = false;
                btnUnBlock.Visible = false;
                btnUpdate.Visible = false;
                if (ddlBranch.SelectedItem.Text == "--Select--")
                {
                    gridEmpReport.Visible = true;
                    DataTable dt = balayer.GetDataTable("select b1.B_Name, mc1.moneycollname, case when mc1.IsBlocked=1 then 'Blocked' else 'UnBlocked' end as Status from moneycollector as mc1 join svcf.branchdetails as b1 on (mc1.BranchID=b1.Head_Id) ;");
                    gridEmpReport.DataSource = dt;
                    gridEmpReport.DataBind();
                }
                else
                {
                    gridEmpReport.Visible = true;
                    gridEmpReport.Visible = true;
                    DataTable dt = balayer.GetDataTable("select b1.B_Name, mc1.moneycollname, case when mc1.IsBlocked=1 then 'Blocked' else 'UnBlocked' end as Status from moneycollector as mc1 join svcf.branchdetails as b1 on (mc1.BranchID=b1.Head_Id) where mc1.BranchID=" + ddlBranch.SelectedItem.Value + " ;");
                    gridEmpReport.DataSource = dt;
                    gridEmpReport.DataBind();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void gridEmpReport_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    TableCell cell = e.Row.Cells[3];
                    string status = cell.Text.ToString();
                    if (status == "Blocked")
                    {
                        cell.BackColor = System.Drawing.Color.Red;
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        
    }
}