using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SVCF_DataAccessLayer;
using SVCF_TransactionLayer;
using SVCF_BusinessAccessLayer;
using log4net;
using log4net.Config;

namespace SreeVisalamChitFundLtd_phase1
{
    public partial class ChitGropMemberDetails : System.Web.UI.Page
    {
        ILog logger = log4net.LogManager.GetLogger(typeof(ChitGropMemberDetails));

        #region Object
        BusinessLayer balayer = new BusinessLayer();
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                getGroup();
                select();
            }
            //select();
        }
        void getGroup()
        {
            try
            {
                DataTable dtchitgrpno = balayer.GetDataTable("SELECT `groupmaster`.`GROUPNO`,`groupmaster`.`Head_Id` FROM svcf.groupmaster where `groupmaster`.`BranchID`=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and `groupmaster`.`IsFinished`=0");
                ddlGroupNumber.DataSource = dtchitgrpno;
                ddlGroupNumber.DataTextField = "GROUPNO";
                ddlGroupNumber.DataValueField = "Head_Id";
                ddlGroupNumber.DataBind();
                ListItem lst1 = new ListItem("--Select--", "--Select--"); ;
                ddlGroupNumber.Items.Insert(0, lst1);
            }
            catch (Exception err)
            {
                logger.Error(DateTime.Now + " | " + "ChitGroupMemberDetails.aspx - getGroup():  Error: " + err.Message + " |  by: " + Convert.ToString(Session["Branchid"]) + "");
            }
        }
        protected void ddlGroupNumber_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlGroupNumber.SelectedItem.Value != "--Select--")
            {
                DataTable dt = balayer.GetDataTable("SELECT `groupmaster`.`ChitStartDate`,`groupmaster`.`GROUPNO`,`groupmaster`.`PSOOrderNo`,`groupmaster`.`PSOOrderDate`,concat(`groupmaster`.`ChitAgreementNo`,'/',`groupmaster`.`ChitAgreementYear`) as AgreementNo,`groupmaster`.`AgreementDate`,`groupmaster`.`ChitValue`,`groupmaster`.`NoofMembers`,`groupmaster`.`ChitPeriod`,`groupmaster`.`ChitEndDate` FROM groupmaster where `groupmaster`.`BranchID`=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and `groupmaster`.`Head_Id`=" + ddlGroupNumber.SelectedItem.Value + "");
                gridDetails.Visible = true;
                gridDetails.SettingsText.Title = "Group No : " + dt.Rows[0]["GROUPNO"].ToString() + "<br>PSO NO : " + dt.Rows[0]["PSOOrderNo"].ToString() + "     dt : " + dt.Rows[0]["PSOOrderDate"].ToString() + "<br>Agreement No : " + dt.Rows[0]["AgreementNo"].ToString() + "       dt : " + dt.Rows[0]["AgreementDate"].ToString() + "<br>Chit Value : " + dt.Rows[0]["ChitValue"].ToString() + "<br>Members : " + dt.Rows[0]["NoofMembers"].ToString() + " Member" + "<br>Duration : " + dt.Rows[0]["ChitPeriod"].ToString() + " Months" + "<br>Installments : " + dt.Rows[0]["NoofMembers"].ToString() + " Installment" + "<br>Installment Amount Rs. " + (Convert.ToDecimal(dt.Rows[0]["ChitValue"]) / Convert.ToDecimal(dt.Rows[0]["NoofMembers"])).ToString() + "/-" + "<br>Ceiling : 30%" + "<br>Date of Commencement : " + dt.Rows[0]["ChitStartDate"] + "<br>Date of Termination : " + dt.Rows[0]["ChitEndDate"].ToString();
            }
            else
            {
                gridDetails.Visible = false;
                gridDetails.SettingsText.Title = "";
            }
        }
        void select()
        {
            
            try
            {
                DataTable dtBind = new DataTable();
                dtBind.Columns.Add("GrpMemberID", typeof(int));
                dtBind.Columns.Add("MemberName");
                dtBind.Columns.Add("MemberAddress");
                dtBind.Columns.Add("EstCallNoOfAuction");
                dtBind.Columns.Add("NomineeName");
                dtBind.Columns.Add("EstSuretyDocument");
                var GrpMemberid = string.Empty;
                DataRow drBind = dtBind.NewRow();

                if (ddlGroupNumber.SelectedItem.Value != "--Select--")
                {
                    var dtHeads = balayer.GetDataTable(" select NodeID from headstree where ParentID=" + ddlGroupNumber.SelectedItem.Value);
                    for (int j = 0; j < dtHeads.Rows.Count; j++)
                    {
                        GrpMemberid = string.Empty;
                        string tokennumber = balayer.GetSingleValue(@"select cast(digits(mg1.GrpMemberID) as unsigned)as ChitNo1 from membertogroupmaster as mg1 join voucher as v1 on v1.Head_Id=mg1.Head_Id join view_groupwisedue as vgwd1 on vgwd1.`GroupId`=" + ddlGroupNumber.SelectedItem.Value + " left join trans_payment as tp1 on v1.Head_Id =tp1.TokenNumber join membermaster as mm on (mg1.MemberID=mm.MemberIDNew) where mg1.BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and mg1.GroupID=" + ddlGroupNumber.SelectedItem.Value + " and v1.Head_Id=" + dtHeads.Rows[j]["NodeID"] + " group by v1.Head_Id order by cast(digits(mg1.GrpMemberID) as unsigned),v1.Voucher_Type DESC ;");
                        if (string.IsNullOrEmpty(tokennumber))
                        {
                            GrpMemberid = balayer.GetSingleValue("select cast(digits(GrpMemberID) as unsigned) as GrpMemberID  from membertogroupmaster where Head_Id=" + dtHeads.Rows[j]["NodeID"]);
                            if (!(string.IsNullOrEmpty(GrpMemberid)))
                            {
                                tokennumber = GrpMemberid;
                                drBind["GrpMemberID"] = GrpMemberid;
                            }
                        }
                        else
                        {
                            drBind["GrpMemberID"] = tokennumber;
                        }
                        if (!(string.IsNullOrEmpty(tokennumber)))
                        {
                            string MemberName = balayer.GetSingleValue(@"select concat(mg1.MemberName) as `MemberName` from membertogroupmaster as mg1 join voucher as v1 on v1.Head_Id=mg1.Head_Id join view_groupwisedue as vgwd1 on vgwd1.`GroupId`=" + ddlGroupNumber.SelectedItem.Value + " left join trans_payment as tp1 on v1.Head_Id =tp1.TokenNumber join membermaster as mm on (mg1.MemberID=mm.MemberIDNew) where mg1.BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and mg1.GroupID=" + ddlGroupNumber.SelectedItem.Value + " and v1.Head_Id=" + dtHeads.Rows[j]["NodeID"] + " group by v1.Head_Id order by v1.ChoosenDate DESC ;");
                            if (string.IsNullOrEmpty(MemberName))
                            {
                                string strMemID = balayer.GetSingleValue("SELECT MemberID FROM svcf.membertogroupmaster where Head_id=" + dtHeads.Rows[j]["NodeID"]);
                                drBind["MemberName"] = balayer.GetSingleValue("SELECT CustomerName FROM svcf.membermaster where MemberIDNew=" + strMemID);
                            }
                            else
                            {
                                drBind["MemberName"] = MemberName;
                            }
                            string memberaddress = balayer.GetSingleValue(@"select concat(m1.AddressForCommunication) as `AddressForCommunication` from membertogroupmaster as mg1 join membermaster as m1 on (mg1.MemberID=m1.MemberIDNew) join voucher as v1 on v1.Head_Id=mg1.Head_Id join view_groupwisedue as vgwd1 on vgwd1.`GroupId`=" + ddlGroupNumber.SelectedItem.Value + " left join trans_payment as tp1 on v1.Head_Id =tp1.TokenNumber join membermaster as mm on (mg1.MemberID=mm.MemberIDNew) where mg1.BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and mg1.GroupID=" + ddlGroupNumber.SelectedItem.Value + " and v1.Head_Id=" + dtHeads.Rows[j]["NodeID"] + " group by v1.Head_Id order by v1.ChoosenDate DESC ;");
                            if (string.IsNullOrEmpty(memberaddress))
                            {
                                drBind["MemberAddress"] = balayer.GetSingleValue("select MemberAddress from membertogroupmaster where Head_Id=" + dtHeads.Rows[j]["NodeID"]);
                            }
                            else
                            {
                                drBind["MemberAddress"] = memberaddress;
                            }
                            //string memberaddress = balayer.GetSingleValue(@"select concat(mg1.MemberAddress) as `MemberAddress` from membertogroupmaster as mg1 join voucher as v1 on v1.Head_Id=mg1.Head_Id join view_groupwisedue as vgwd1 on vgwd1.`GroupId`=" + ddlGroupNumber.SelectedItem.Value + " left join trans_payment as tp1 on v1.Head_Id =tp1.TokenNumber join membermaster as mm on (mg1.MemberID=mm.MemberIDNew) where mg1.BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and mg1.GroupID=" + ddlGroupNumber.SelectedItem.Value + " and v1.Head_Id=" + dtHeads.Rows[j]["NodeID"] + " group by v1.Head_Id order by v1.ChoosenDate DESC ;");
                            //if (string.IsNullOrEmpty(memberaddress))
                            //{
                            //    drBind["MemberAddress"] = balayer.GetSingleValue("select MemberAddress from membertogroupmaster where Head_Id=" + dtHeads.Rows[j]["NodeID"]);
                            //}
                            //else
                            //{
                            //    drBind["MemberAddress"] = memberaddress;
                            //}
                            string estCallNoOfAuction = balayer.GetSingleValue(@"select concat(mg1.EstCallNoOfAuction) as `EstCallNoOfAuction` from membertogroupmaster as mg1 join voucher as v1 on v1.Head_Id=mg1.Head_Id join view_groupwisedue as vgwd1 on vgwd1.`GroupId`=" + ddlGroupNumber.SelectedItem.Value + " left join trans_payment as tp1 on v1.Head_Id =tp1.TokenNumber join membermaster as mm on (mg1.MemberID=mm.MemberIDNew) where mg1.BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and mg1.GroupID=" + ddlGroupNumber.SelectedItem.Value + " and v1.Head_Id=" + dtHeads.Rows[j]["NodeID"] + " group by v1.Head_Id order by v1.ChoosenDate DESC ;");
                            if (string.IsNullOrEmpty(estCallNoOfAuction))
                            {
                                drBind["EstCallNoOfAuction"] = balayer.GetSingleValue("select EstCallNoOfAuction from membertogroupmaster where Head_Id=" + dtHeads.Rows[j]["NodeID"]);
                            }
                            else
                            {
                                drBind["EstCallNoOfAuction"] = estCallNoOfAuction;
                            }
                            string estSuretyDocument = balayer.GetSingleValue(@"select concat(mg1.EstSuretyDocument) as `EstSuretyDocument` from membertogroupmaster as mg1 join voucher as v1 on v1.Head_Id=mg1.Head_Id join view_groupwisedue as vgwd1 on vgwd1.`GroupId`=" + ddlGroupNumber.SelectedItem.Value + " left join trans_payment as tp1 on v1.Head_Id =tp1.TokenNumber join membermaster as mm on (mg1.MemberID=mm.MemberIDNew) where mg1.BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and mg1.GroupID=" + ddlGroupNumber.SelectedItem.Value + " and v1.Head_Id=" + dtHeads.Rows[j]["NodeID"] + " group by v1.Head_Id order by v1.ChoosenDate DESC ;");
                            if (string.IsNullOrEmpty(estSuretyDocument))
                            {
                                drBind["EstSuretyDocument"] = balayer.GetSingleValue("select EstSuretyDocument from membertogroupmaster where Head_Id=" + dtHeads.Rows[j]["NodeID"]);
                            }
                            else
                            {
                                drBind["EstSuretyDocument"] = estSuretyDocument;
                            }
                            string NomineeName = balayer.GetSingleValue(@"select concat(mg1.NomineeName) as `NomineeName` from membertogroupmaster as mg1 join voucher as v1 on v1.Head_Id=mg1.Head_Id join view_groupwisedue as vgwd1 on vgwd1.`GroupId`=" + ddlGroupNumber.SelectedItem.Value + " left join trans_payment as tp1 on v1.Head_Id =tp1.TokenNumber join membermaster as mm on (mg1.MemberID=mm.MemberIDNew) where mg1.BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and mg1.GroupID=" + ddlGroupNumber.SelectedItem.Value + " and v1.Head_Id=" + dtHeads.Rows[j]["NodeID"] + " group by v1.Head_Id order by v1.ChoosenDate DESC ;");
                            if (string.IsNullOrEmpty(NomineeName))
                            {
                                drBind["NomineeName"] = balayer.GetSingleValue("select NomineeName from membertogroupmaster where Head_Id=" + dtHeads.Rows[j]["NodeID"]);
                            }
                            else
                            {
                                drBind["NomineeName"] = NomineeName;
                            }

                            dtBind.Rows.Add(drBind.ItemArray);
                        }
                    }
                    DataView dv = dtBind.DefaultView;
                    dv.Sort = "GrpMemberID asc";
                    DataTable sortedDT = dv.ToTable();
                    grid.DataSource = sortedDT;
                }

                //grid.DataSource = dtBind;
                grid.DataBind();
                gridDetails.DataBind();
            }
            catch (Exception err)
            {
                logger.Error(DateTime.Now + " | " + "ChitGroupMemberDetails.aspx - select():  Error: " + err.Message + " |  by: " + Convert.ToString(Session["Branchid"]) + "");
            }

        }
        protected void BtnStatisticsGo_Click(object sender, EventArgs e)
        {
            select();
        }
    }
}