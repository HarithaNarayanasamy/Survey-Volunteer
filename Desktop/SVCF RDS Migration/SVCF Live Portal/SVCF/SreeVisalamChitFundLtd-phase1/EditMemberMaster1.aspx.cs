using System;
using System.Data;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxUploadControl;
using System.IO;
using System.Text;
using DevExpress.Web.ASPxCallbackPanel;
using DevExpress.Web.ASPxEditors;
using System.Drawing;
using SVCF_BusinessAccessLayer;
using SVCF_DataAccessLayer;
using SVCF_TransactionLayer;
using log4net;
using log4net.Config;

namespace SreeVisalamChitFundLtd_phase1
{
    public partial class WebForm8 : System.Web.UI.Page
    {
        #region Object
        BusinessLayer balayer = new BusinessLayer();
        TransactionLayer trn = new TransactionLayer();
        #endregion
        ILog logger = log4net.LogManager.GetLogger(typeof(WebForm8));
        string userinfo = "";
        string qry = "";
        string usrRole = "";
        protected void callbackPanel_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            ASPxCallbackPanel panel = (ASPxCallbackPanel)sender;
            ASPxBinaryImage bImage = (ASPxBinaryImage)panel.FindControl("previewImage");
            bImage.ContentBytes = (byte[])Session["uploadedFileData"];
            bImage.Visible = true;
        }
        protected void ucImage_FileUploadComplete(object sender, DevExpress.Web.ASPxUploadControl.FileUploadCompleteEventArgs e)
        {
            ASPxUploadControl update = (ASPxUploadControl)sender;
            if (update.HasFile)
            {              

                CommonClassFile.IMGusersPhoto = update.PostedFile;
                //CommonClassFile.IMGusersPhoto = e.UploadedFile.FileBytes;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "javascript:alert(' photo uploaded successfully')", true);
                Session["uploadedFileData"] = e.UploadedFile.FileBytes;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            select();
            gridBranch.DataBind();

            if (!IsPostBack)
            {
                foreach (GridViewColumn column in gridBranch.Columns)
                {
                    if (column is GridViewDataColumn)
                    {
                        ((GridViewDataColumn)column).Settings.AutoFilterCondition = AutoFilterCondition.Contains;
                    }
                }

                userinfo = HttpContext.Current.User.Identity.Name;
                qry = "select rs.name from roles as rs inner join rights as rt on (rt.roleid=rs.id) where memberid=" + userinfo + "";
                usrRole = balayer.GetSingleValue(qry);
                if (usrRole == "Administrator")
                {
                    gridBranch.Columns[0].Visible = true;
                }
                else
                {
                    gridBranch.Columns[0].Visible = false;
                }

            }
           
        }
        protected void select()
        {
            DataSourceMember.SelectCommand = @"select `membermaster`.`MemberID`, `membermaster`.`BranchId` AS `BranchId`,`membermaster`.`Title`,`membermaster`.`CustomerName` AS  `CustomerName`,concat(ifnull(`membermaster`.`Title`,''),'',`membermaster`.`CustomerName`) as CustomerName1, `membermaster`.`TypeOfMember` AS `TypeOfMember`, `membermaster`.`Gender` AS `Gender`, `membermaster`.`Age` AS `Age`, date_format( `membermaster`.`DOB`,'%d/%m/%Y') AS `DOB`, `membermaster`.`FatherHusbandName` AS `FatherHusbandName`, `membermaster`.`MotherWifeName` AS `MotherWifeName`, `membermaster`.`ProprietorName` AS `ProprietorName`, `membermaster`.`PartnersName` AS `PartnersName`, date_format(`membermaster`.`DateOfPartnershipWithAmendment`,'%d/%m/%Y') AS `DateOfPartnershipWithAmendment`, `membermaster`.`compxerox` AS `compxerox`, date_format(`membermaster`.`dateofResol`,'%d/%m/%Y') AS `dateofResol`, `membermaster`.`ProfessionBusiness`  AS `ProfessionBusiness`, `membermaster`.`NatureofProfessionBusiness` AS `NatureofProfessionBusiness`, `membermaster`.`ResidentialAddress` AS `ResidentialAddress`, `membermaster`.`AddressForCommunication` AS `AddressForCommunication`, `membermaster`.`ProofofResidence` AS `ProofofResidence`, `membermaster`.`AddressProfessionBusiness` AS `AddressProfessionBusiness`,`membermaster`.`TelephoneNoProfessionBusiness` AS `TelephoneNoProfessionBusiness`, `membermaster`.`TelephoneNoResidence` AS `TelephoneNoResidence`, `membermaster`.`MobileNo` AS `MobileNo`, `membermaster`.`MonthlyIncome` AS `MonthlyIncome`, `membermaster`.`SalesTaxRegistrationNoTNGST` AS `SalesTaxRegistrationNoTNGST`, `membermaster`.`CSTRegistrationNumber` AS `CSTRegistrationNumber`, `membermaster`.`IncomeTaxPANoWardandCircle` AS `IncomeTaxPANoWardandCircle`, `membermaster`.`BankName` AS `BankName`, `membermaster`.`BranchName` AS `BranchName`, `membermaster`.`SavingsCurrentAccountNo` AS `SavingsCurrentAccountNo`,`membermaster`.`AadharNumber` as 'AadharNumber',`membersdocuments`.`MemberID` as `MemberID`,`membermaster`.`MemberIDNew` AS `MemberIDNew`,`membersdocuments`.`Photo` from (`membermaster` left join `membersdocuments` ON ((`membermaster`.`MemberIDNew` = `membersdocuments`.`MemberID`)))";
        }
        protected void gridBranch_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            balayer.GetInsertItem("delete FROM svcf.membermaster where MemberIDNew=" + e.Keys["MemberIDNew"]);
            balayer.GetInsertItem("delete from membersdocuments where MemberID=" + e.Keys["MemberIDNew"] + "");
            ASPxGridView grid = (sender as ASPxGridView);
            select();
            grid.DataBind();
            e.Cancel = true;
            logger.Info("EditMemberMaster1.aspx - gridBranch_RowDeleting():  Completed: " + DateTime.Now + " by: " + Convert.ToString(Session["UserName"]) + "");
        }

        protected void gridBranch_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        {

        }
        void AddError(Dictionary<GridViewColumn, string> errors, GridViewColumn column, string errorText)
        {
            if (errors.ContainsKey(column)) return;
            errors[column] = errorText;
        }

        protected void gridBranch_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            string gender = "";
            int num1;
	        bool res = int.TryParse( balayer.ToobjectstrEvenNull( e.NewValues["Age"]), out num1);
	        if (!res)
	        {
                e.NewValues["Age"] = "0";
	        }
            if (balayer.ToobjectstrEvenNull(e.NewValues["Title"]) == "Ms." || balayer.ToobjectstrEvenNull(e.NewValues["Title"]) == "Mrs.")
            {
                gender = "Female";
            }
            else
            {
                gender = "Male";
            }

            int UpdateIndidual = balayer.GetInsertItem("update membermaster set CustomerName='" + balayer.MySQLEscapeString(balayer.ToobjectstrEvenNull(e.NewValues["CustomerName"])) + "',TypeOfMember='" + balayer.MySQLEscapeString(balayer.ToobjectstrEvenNull(e.NewValues["TypeOfMember"])) + "',Title='" + balayer.MySQLEscapeString(balayer.ToobjectstrEvenNull(e.NewValues["Title"])) + "',Gender='" + gender + "',Age='" + e.NewValues["Age"] + "',FatherHusbandName='" + balayer.MySQLEscapeString(balayer.ToobjectstrEvenNull(e.NewValues["FatherHusbandName"])) + "',MotherWifeName='" + balayer.MySQLEscapeString(balayer.ToobjectstrEvenNull(e.NewValues["MotherWifeName"])) + "',ProprietorName='" + balayer.MySQLEscapeString(balayer.ToobjectstrEvenNull(e.NewValues["ProprietorName"])) + "',PartnersName='" + balayer.MySQLEscapeString(balayer.ToobjectstrEvenNull(e.NewValues["PartnersName"])) + "',ProfessionBusiness='" + balayer.MySQLEscapeString(balayer.ToobjectstrEvenNull(e.NewValues["ProfessionBusiness"])) + "',NatureofProfessionBusiness='" + balayer.MySQLEscapeString(balayer.ToobjectstrEvenNull(e.NewValues["NatureofProfessionBusiness"])) + "',ResidentialAddress='" + balayer.MySQLEscapeString(balayer.ToobjectstrEvenNull(e.NewValues["ResidentialAddress"])) + "',AddressForCommunication='" + balayer.MySQLEscapeString(balayer.ToobjectstrEvenNull(e.NewValues["AddressForCommunication"])) + "',AddressProfessionBusiness='" + balayer.MySQLEscapeString(balayer.ToobjectstrEvenNull(e.NewValues["AddressProfessionBusiness"])) + "',ProofofResidence='" + balayer.MySQLEscapeString(balayer.ToobjectstrEvenNull(e.NewValues["ProofofResidence"])) + "',AddressProfessionBusiness='" + balayer.MySQLEscapeString(balayer.ToobjectstrEvenNull(e.NewValues["AddressProfessionBusiness"])) + "',TelephoneNoProfessionBusiness='" + balayer.MySQLEscapeString(balayer.ToobjectstrEvenNull(e.NewValues["TelephoneNoProfessionBusiness"])) + "',TelephoneNoResidence='" + balayer.MySQLEscapeString(balayer.ToobjectstrEvenNull(e.NewValues["TelephoneNoResidence"])) + "',MobileNo='" + balayer.MySQLEscapeString(balayer.ToobjectstrEvenNull(e.NewValues["MobileNo"])) + "',MonthlyIncome=" + balayer.MySQLEscapeString(balayer.ToobjectstrEvenNull(e.NewValues["MonthlyIncome"])) + ",SalesTaxRegistrationNoTNGST='" + balayer.MySQLEscapeString(balayer.ToobjectstrEvenNull(e.NewValues["SalesTaxRegistrationNoTNGST"])) + "',CSTRegistrationNumber='" + balayer.MySQLEscapeString(balayer.ToobjectstrEvenNull(e.NewValues["CSTRegistrationNumber"])) + "',IncomeTaxPANoWardandCircle='" + balayer.MySQLEscapeString(balayer.ToobjectstrEvenNull(e.NewValues["IncomeTaxPANoWardandCircle"])) + "',"+
                "BankName='" + balayer.MySQLEscapeString(balayer.ToobjectstrEvenNull(e.NewValues["BankName"])) + "',BranchName='" + balayer.MySQLEscapeString(balayer.ToobjectstrEvenNull(e.NewValues["BranchName"])) + "',"+
                "SavingsCurrentAccountNo='" + balayer.MySQLEscapeString(balayer.ToobjectstrEvenNull(e.NewValues["SavingsCurrentAccountNo"])) + "',AadharNumber='" + balayer.MySQLEscapeString(balayer.ToobjectstrEvenNull(e.NewValues["AadharNumber"])) + "' where MemberIDNew=" + e.Keys["MemberIDNew"] + "");
            balayer.GetInsertItem("update svcf.membertogroupmaster set MemberName='" + balayer.MySQLEscapeString(balayer.ToobjectstrEvenNull(e.NewValues["CustomerName"])) + "', MemberAddress='" + balayer.MySQLEscapeString(balayer.ToobjectstrEvenNull(e.NewValues["SavingsCurrentAccountNo"])) + "' where MemberID='" + e.Keys["MemberIDNew"] + "'");
            if (CommonClassFile.IMGusersPhoto!=null)
            {
                int deleteMemberDocuments = balayer.GetInsertItem("delete from membersdocuments where MemberID=" + e.Keys["MemberIDNew"] + "");
                int length = CommonClassFile.IMGusersPhoto.ContentLength;
                byte[] imgbyte = new byte[length];
                using (var binaryReader = new BinaryReader(CommonClassFile.IMGusersPhoto.InputStream))
                {
                    imgbyte = binaryReader.ReadBytes(CommonClassFile.IMGusersPhoto.ContentLength);
                }
                MySqlConnection connection = new MySqlConnection(CommonClassFile.ConnectionString);
                connection.Open();
                MySqlCommand cmd = new MySqlCommand("INSERT INTO membersdocuments (MemberID ,Photo,ImageTYpe) VALUES (?MemberID,?Photo,?ImageTYpe)", connection);
                cmd.Parameters.Add("?MemberID", MySqlDbType.Int32, 45).Value = e.Keys["MemberIDNew"];
                cmd.Parameters.Add("?Photo", MySqlDbType.Blob).Value = imgbyte;
                cmd.Parameters.Add("?ImageTYpe", MySqlDbType.VarChar, 15).Value = CommonClassFile.IMGusersPhoto.ContentType;
                int count = cmd.ExecuteNonQuery();
                connection.Close();
                CommonClassFile.IMGusersPhoto = null;
            }
            ASPxGridView grid = (sender as ASPxGridView);
            select();
            grid.DataBind();
            e.Cancel = true;
            logger.Info("EditMemberMaster1.aspx - gridBranch_RowUpdating():  Completed: " + DateTime.Now + " by: " + Convert.ToString(Session["UserName"]) + "");
        }
        protected void gridBranch_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
        {
            ASPxGridView grid = (sender as ASPxGridView);
            if (!grid.IsNewRowEditing)
            {
                grid.DoRowValidation();
            }
        }
    }
}
