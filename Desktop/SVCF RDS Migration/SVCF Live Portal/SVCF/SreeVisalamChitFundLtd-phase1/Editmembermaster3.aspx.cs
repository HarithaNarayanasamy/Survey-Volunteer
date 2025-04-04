using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using SVCF_BusinessAccessLayer;
using SVCF_DataAccessLayer;
using SVCF_TransactionLayer;
using System.IO;
using System.Text;
using System.Data;
using System.Text.RegularExpressions;
using log4net;
using log4net.Config;
using System.Configuration;

namespace SreeVisalamChitFundLtd_phase1
{
    public partial class Editmembermaster3 : System.Web.UI.Page
    {
        DataTable dsgrid = new DataTable();
        DataTable dsbranch = new DataTable();
        #region Object
        BusinessLayer balayer = new BusinessLayer();
        CommonClassFile objCommonClass = new CommonClassFile();
        TransactionLayer trn = new TransactionLayer();
        #endregion
        //   private string SearchString = "";
        string userinfo = "";
        string qry = "";
        string usrRole = "";
        private int PageSize = 10;
        DataTable dsGridBrnach = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
         
            if (!IsPostBack)
            {
                // ddlbranch.Visible = false;
                userinfo = HttpContext.Current.User.Identity.Name;
                qry = "select rs.name from roles as rs inner join rights as rt on (rt.roleid=rs.id) where memberid=" + userinfo + "";
                usrRole = balayer.GetSingleValue(qry);
                //if (usrRole == "Administrator")
                //{
                    GetDropDown();
                    fillgrid();
                    if (Convert.ToInt32(Session["Branchid"]) == 161)
                    {
                        ddlbranch.Visible = true;
                        Label5.Visible = true;
                    }
                    else
                    {
                        ddlbranch.Visible = false;
                        Label5.Visible = false;
                    }
              //  }
                //else
                //{
                //    Response.Redirect("Home.aspx", true);
                //}              
            }
           // fillgrid();

        }



        private void GetDropDown()
        {
            ddlbranch.DataSource = null;
            ddlbranch.DataBind();
            DataTable Accbank1 = balayer.GetDataTable("select B_Name,Head_Id from branchdetails order by B_Name asc");
            DataRow drinvestment;
            drinvestment = Accbank1.NewRow();
            drinvestment[0] = "--select--";
            drinvestment[1] = "0";
            Accbank1.Rows.InsertAt(drinvestment, 0);
            ddlbranch.DataSource = Accbank1;    
            ddlbranch.DataTextField = "B_Name";
            ddlbranch.DataValueField = "Head_Id";
            ddlbranch.DataBind();
        }
        protected void Page_Changed(object sender, EventArgs e)
        {
            int pageIndex = int.Parse((sender as LinkButton).CommandArgument);
            //   this.GetCustomersPageWise(pageIndex);

          // dsGridBrnach = GetCustomersPageWise(pageIndex);
            dsGridBrnach = balayer.GetCustomersPageWisestore(pageIndex);
            this.PopulatePager(CommonClassFile.recordCount, CommonClassFile.pageIn);
            if (dsGridBrnach.Rows.Count > 0)
            {
                dsbranch.Columns.Add("MemberIDNew");
                dsbranch.Columns.Add("BranchId");
                dsbranch.Columns.Add("ImageUrl");
                dsbranch.Columns.Add("MemberID");
                dsbranch.Columns.Add("CustomerName");
                dsbranch.Columns.Add("FatherHusbandName");
                dsbranch.Columns.Add("AadharNumber");
                dsbranch.Columns.Add("MobileNo");
                dsbranch.Columns.Add("TokenNo");
                DataTable dsgrid1 = new DataTable();
                DataTable grp = new DataTable();
                DataTable dsgrid = new DataTable();
                DataRow dr = dsbranch.NewRow();
                List<string> token = new List<string>();
                List<string> emp1 = new List<string>();
                for (int i = 0; i < dsGridBrnach.Rows.Count; i++)
                {
                    try
                    {
                        //12/07/2021
                        //if (ddlbranch.SelectedItem.Text != "--select--")
                        //{
                        //    grp = balayer.GetDataTable("select mg.GrpMemberID,ms.MemberIDNew,mg.MemberID,ms.MemberID as mem,ms.BranchId,ms.Title,ms.CustomerName,ms.TypeOfMember,ms.Gender,ms.Age,ms.FatherHusbandName,ms.AadharNumber,ms.MobileNo,md.ImageUrl from svcf.membermaster as ms left join membersdocuments as md on ms.MemberIDNew = md.MemberID join svcf.membertogroupmaster as mg on(ms.MemberIDNew = mg.MemberID) where ms.BranchId = " + ddlbranch.SelectedItem.Value + " and mg.MemberID = " + dsGridBrnach.Rows[i]["MemberIDNew"] + " order by ms.CustomerName asc");
                        //}
                        if (Convert.ToInt32(Session["Branchid"]) == 161)
                        {
                            //12/07/2021
                            if (ddlbranch.SelectedItem.Text != "--select--")
                            {
                                grp = balayer.GetDataTable("select mg.GrpMemberID,ms.MemberIDNew,mg.MemberID,ms.MemberID as mem,ms.BranchId,ms.Title,ms.CustomerName,ms.TypeOfMember,ms.Gender,ms.Age,ms.FatherHusbandName,ms.AadharNumber,ms.MobileNo,md.ImageUrl from svcf.membermaster as ms left join membersdocuments as md on ms.MemberIDNew = md.MemberID join svcf.membertogroupmaster as mg on(ms.MemberIDNew = mg.MemberID) where ms.BranchId = " + ddlbranch.SelectedItem.Value + " and mg.MemberID = " + dsGridBrnach.Rows[i]["MemberIDNew"] + " order by ms.CustomerName asc");
                            }
                            else
                            {
                                grp = balayer.GetDataTable("select mg.GrpMemberID,ms.MemberIDNew,mg.MemberID,ms.MemberID as mem ,ms.BranchId,ms.Title,ms.CustomerName,ms.TypeOfMember,ms.Gender,ms.Age,ms.FatherHusbandName,ms.AadharNumber,ms.MobileNo,md.ImageUrl from svcf.membermaster as ms left join membersdocuments as md on ms.MemberIDNew = md.MemberID join svcf.membertogroupmaster as mg on(ms.MemberIDNew = mg.MemberID) where  mg.MemberID = " + dsGridBrnach.Rows[i]["MemberIDNew"] + " order by ms.CustomerName asc ");
                            }
                        }
                        else
                        {
                            grp = balayer.GetDataTable("select mg.GrpMemberID,ms.MemberIDNew,mg.MemberID,ms.MemberID as mem ,ms.BranchId,ms.Title,ms.CustomerName,ms.TypeOfMember,ms.Gender,ms.Age,ms.FatherHusbandName,ms.AadharNumber,ms.MobileNo,md.ImageUrl from svcf.membermaster as ms left join membersdocuments as md on ms.MemberIDNew = md.MemberID join svcf.membertogroupmaster as mg on(ms.MemberIDNew = mg.MemberID) where ms.BranchId = " + Session["Branchid"] + " and mg.MemberID = " + dsGridBrnach.Rows[i]["MemberIDNew"] + " order by ms.CustomerName asc ");
                        }

                        if (grp.Rows.Count > 0)
                        {
                            

                            dr["MemberID"] = grp.Rows[0]["mem"];
                            dr["CustomerName"] = grp.Rows[0]["CustomerName"];
                            dr["FatherHusbandName"] = grp.Rows[0]["FatherHusbandName"];
                            dr["AadharNumber"] = grp.Rows[0]["AadharNumber"];
                            dr["MobileNo"] = grp.Rows[0]["MobileNo"];
                            dr["ImageUrl"] = grp.Rows[0]["ImageUrl"];
                           
                            emp1 = balayer.RetrveList("select GrpMemberID from svcf.membertogroupmaster where  MemberID=" + dsGridBrnach.Rows[i]["MemberIDNew"] + " ");
                          
                            StringBuilder sb = new StringBuilder();
                            foreach (var emp in emp1)
                            {
                                // string finaldatenum = emp.Split('#')[1].Trim(',');
                                sb.Append(emp + ",");
                            }
                            string empeee = sb.ToString();
                            string tokenno = empeee.TrimEnd(',');
                            dr["TokenNo"] = tokenno;
                            dr["MemberIDNew"] = grp.Rows[0]["MemberIDNew"];
                            dr["BranchId"] = grp.Rows[0]["BranchId"];
                            dsbranch.Rows.Add(dr.ItemArray);
                        }
                        else
                        {
                           


                            dr["MemberID"] = dsGridBrnach.Rows[i]["mem"];
                            dr["CustomerName"] = dsGridBrnach.Rows[i]["CustomerName"];
                            dr["FatherHusbandName"] = dsGridBrnach.Rows[i]["FatherHusbandName"];
                            dr["AadharNumber"] = dsGridBrnach.Rows[i]["AadharNumber"];
                            dr["MobileNo"] = dsGridBrnach.Rows[i]["MobileNo"];
                            dr["ImageUrl"] = dsGridBrnach.Rows[i]["ImageUrl"];
                            dr["TokenNo"] = "";
                            dr["MemberIDNew"] = dsGridBrnach.Rows[i]["MemberIDNew"];
                            dr["BranchId"] = dsGridBrnach.Rows[i]["BranchId"];
                            dsbranch.Rows.Add(dr.ItemArray);
                        }
                    }
                    catch (Exception ex)
                    {
                        string hh = ex.Message;
                    }

                }
            }
            gridBranch.DataSource = dsbranch;
            gridBranch.DataBind();
          
        }
        protected void Clear(object sender, EventArgs e)
        {
            txtSearch.Text = "";
            fillgrid();
        }
        protected void Search(object sender, EventArgs e)
        {
            try
            {
                string CustomerName = txtSearch.Text.Trim();
                List<ClsMemberList> SearchGroupList = null;
                if (Convert.ToInt32(Session["Branchid"]) == 161)
                {
                    string query = @"select mg1.GrpMemberID, ms.MemberIDNew,ms.MemberID as mem ,ms.BranchId,ms.Title,ms.CustomerName,ms.TypeOfMember,ms.Gender,ms.Age,ms.FatherHusbandName,ms.AadharNumber,ms.MobileNo," +
                               "md.ImageUrl from svcf.membermaster as ms left join membersdocuments as md on ms.MemberIDNew=md.MemberID left join membertogroupmaster as mg1 on(ms.MemberIDNew=mg1.MemberID)  where ms.CustomerName LIKE '%" + CustomerName + "%' or ms.MemberID like '%" + CustomerName + "%' or ms.FatherHusbandName like '%" + CustomerName + "%' or ms.AadharNumber like'%" + CustomerName + "%' or ms.MobileNo like '%" + CustomerName + "%' or mg1.GrpMemberID like '%" + CustomerName + "%'";

                    dsGridBrnach = balayer.GetDataTable(query);
                    SearchGroupList = new List<ClsMemberList>();
                    SearchGroupList = dsGridBrnach.DataTableToList<ClsMemberList>();
                }
                else
                {
                    string query = @"select mg1.GrpMemberID,ms.MemberIDNew,ms.MemberID as mem ,ms.BranchId,ms.Title,ms.CustomerName,ms.TypeOfMember,ms.Gender,ms.Age,ms.FatherHusbandName,ms.AadharNumber,ms.MobileNo," +
                               "md.ImageUrl from svcf.membermaster as ms left join membersdocuments as md on ms.MemberIDNew=md.MemberID left join membertogroupmaster as mg1 on(ms.MemberIDNew=mg1.MemberID) where ms.BranchId=" + Session["Branchid"] + " and ms.CustomerName LIKE '%" + CustomerName + "%' or ms.MemberID like '%" + CustomerName + "%' or ms.FatherHusbandName like '%" + CustomerName + "%' or ms.AadharNumber like'%" + CustomerName + "%' or ms.MobileNo like '%" + CustomerName + "%' or mg1.GrpMemberID like '%" + CustomerName + "%'";
                    dsGridBrnach = balayer.GetDataTable(query);
                    SearchGroupList = new List<ClsMemberList>();
                    SearchGroupList = dsGridBrnach.DataTableToList<ClsMemberList>();
                }

               // List<ClsMemberList> Member_FilteredList = null;
                var memtogrpquery = "select GrpMemberID,MemberID from svcf.membertogroupmaster";
                var MemGrpList = balayer.GetDataTable(memtogrpquery);
                List<ClsMemtoGroup> AllMemtoGrpList = MemGrpList.DataTableToList<ClsMemtoGroup>();

                if (SearchGroupList.Count > 0)
                {
                    dsbranch.Columns.Add("MemberIDNew");
                    dsbranch.Columns.Add("BranchId");
                    dsbranch.Columns.Add("ImageUrl");
                    dsbranch.Columns.Add("MemberID");
                    dsbranch.Columns.Add("CustomerName");
                    dsbranch.Columns.Add("FatherHusbandName");
                    dsbranch.Columns.Add("AadharNumber");
                    dsbranch.Columns.Add("MobileNo");
                    dsbranch.Columns.Add("TokenNo");
                    DataTable dsgrid1 = new DataTable();
                    DataTable grp = null;
                    DataTable dsgrid = new DataTable();
                    DataRow dr = dsbranch.NewRow();
                    List<string> token = new List<string>();
                    List<string> emp1 = new List<string>();

                    //if (Convert.ToInt32(Session["Branchid"]) == 161)
                    //{
                    //    grp = new DataTable();
                    //    grp = balayer.GetDataTable("select mg.GrpMemberID,ms.MemberIDNew,mg.MemberID,ms.MemberID as mem ,ms.BranchId,ms.Title,ms.CustomerName,ms.TypeOfMember,ms.Gender,ms.Age,ms.FatherHusbandName,ms.AadharNumber,ms.MobileNo,md.ImageUrl " +
                    //    "from svcf.membermaster as ms left join membersdocuments as md on ms.MemberIDNew = md.MemberID join svcf.membertogroupmaster " +
                    //    "as mg on(ms.MemberIDNew = mg.MemberID) order by ms.CustomerName asc ");
                    //}
                    //else
                    //{
                    //    grp = new DataTable();
                    //    grp = balayer.GetDataTable("select mg.GrpMemberID,ms.MemberIDNew,mg.MemberID,ms.MemberID as mem,ms.BranchId,ms.Title," +
                    //        "ms.CustomerName,ms.TypeOfMember,ms.Gender,ms.Age,ms.FatherHusbandName,ms.AadharNumber,ms.MobileNo,md.ImageUrl from " +
                    //        "svcf.membermaster as ms left join membersdocuments as md on ms.MemberIDNew = md.MemberID join svcf.membertogroupmaster " +
                    //        "as mg on(ms.MemberIDNew = mg.MemberID) where ms.BranchId = " + Session["Branchid"] + " order by ms.CustomerName asc ");
                    //}
                    //List<ClsMembers> Members = grp.DataTableToList<ClsMembers>();

                    var distinctMemberIdnewList = SearchGroupList.Select(x => x.MemberIDNew).Distinct();
                    
                    foreach (var MemidNew in distinctMemberIdnewList)
                    {
                        try
                        {
                            //if (ddlbranch.SelectedItem.Text != "--select--")
                            //{
                            //    grp = balayer.GetDataTable("select mg.GrpMemberID,ms.MemberIDNew,mg.MemberID,ms.MemberID as mem,ms.BranchId,ms.Title,ms.CustomerName,ms.TypeOfMember,ms.Gender,ms.Age,ms.FatherHusbandName,ms.AadharNumber,ms.MobileNo,md.ImageUrl from svcf.membermaster as ms left join membersdocuments as md on ms.MemberIDNew = md.MemberID join svcf.membertogroupmaster as mg on(ms.MemberIDNew = mg.MemberID) where ms.BranchId = " + ddlbranch.SelectedItem.Value + " and mg.MemberID = " + dsGridBrnach.Rows[i]["MemberIDNew"] + " order by ms.CustomerName asc");
                            //}
                            if (Convert.ToInt32(Session["Branchid"]) == 161)
                            {
                                var allmatch_list = (from t1 in SearchGroupList
                                                     where t1.MemberIDNew == Convert.ToInt32(MemidNew)
                                                     orderby t1.CustomerName ascending
                                                     select t1).ToList();
                                if (allmatch_list.Count > 0)
                                {
                                    dr["MemberID"] = allmatch_list[0].mem;
                                    dr["CustomerName"] = allmatch_list[0].CustomerName;
                                    dr["FatherHusbandName"] = allmatch_list[0].FatherHusbandName;
                                    dr["AadharNumber"] = allmatch_list[0].AadharNumber;
                                    dr["MobileNo"] = allmatch_list[0].MobileNo;
                                    dr["ImageUrl"] = allmatch_list[0].ImageUrl;

                                    // emp1 = balayer.RetrveList("select GrpMemberID from svcf.membertogroupmaster where  MemberID=" + dsGridBrnach.Rows[i]["MemberIDNew"] + " ");
                                    var grpmemberid_list = (from mem1 in AllMemtoGrpList
                                                            where mem1.MemberID == Convert.ToInt32(MemidNew)
                                                            select mem1.GrpMemberID).ToList();


                                    StringBuilder sb = new StringBuilder();
                                    foreach (var emp in grpmemberid_list)
                                    {
                                        // string finaldatenum = emp.Split('#')[1].Trim(',');
                                        sb.Append(emp + ",");
                                    }
                                    string empeee = sb.ToString();
                                    string tokenno = empeee.TrimEnd(',');
                                    dr["TokenNo"] = tokenno;
                                    dr["MemberIDNew"] = allmatch_list[0].MemberIDNew; //grp.Rows[0]["MemberIDNew"];
                                    dr["BranchId"] = allmatch_list[0].BranchId; //grp.Rows[0]["BranchId"];
                                    dsbranch.Rows.Add(dr.ItemArray);
                                }
                                else
                                {
                                    //dr["MemberID"] = member.mem;
                                    //dr["CustomerName"] = member.CustomerName;
                                    //dr["FatherHusbandName"] = member.FatherHusbandName;
                                    //dr["AadharNumber"] = member.AadharNumber;
                                    //dr["MobileNo"] = member.MobileNo;
                                    //dr["ImageUrl"] = member.ImageUrl;
                                    //dr["TokenNo"] = "";
                                    //dr["MemberIDNew"] = member.MemberIDNew;
                                    //dr["BranchId"] = member.BranchId;
                                    //dsbranch.Rows.Add(dr.ItemArray);
                                }
                                // grp = balayer.GetDataTable("select mg.GrpMemberID,ms.MemberIDNew,mg.MemberID,ms.MemberID as mem ,ms.BranchId,ms.Title,ms.CustomerName,ms.TypeOfMember,ms.Gender,ms.Age,ms.FatherHusbandName,ms.AadharNumber,ms.MobileNo,md.ImageUrl from svcf.membermaster as ms left join membersdocuments as md on ms.MemberIDNew = md.MemberID join svcf.membertogroupmaster as mg on(ms.MemberIDNew = mg.MemberID) where  mg.MemberID = " + dsGridBrnach.Rows[i]["MemberIDNew"] + " order by ms.CustomerName asc ");
                            }
                            else
                            {
                                //grp = balayer.GetDataTable("select mg.GrpMemberID,ms.MemberIDNew,mg.MemberID,ms.MemberID as mem,ms.BranchId,ms.Title,ms.CustomerName,ms.TypeOfMember,ms.Gender,ms.Age,ms.FatherHusbandName,ms.AadharNumber,ms.MobileNo,md.ImageUrl from svcf.membermaster as ms left join membersdocuments as md on ms.MemberIDNew = md.MemberID join svcf.membertogroupmaster as mg on(ms.MemberIDNew = mg.MemberID) where ms.BranchId = " + Session["Branchid"] + " and mg.MemberID = " + dsGridBrnach.Rows[i]["MemberIDNew"] + " order by ms.CustomerName asc ");
                                var match_list = (from t1 in SearchGroupList
                                                  where t1.MemberIDNew == Convert.ToInt32(MemidNew)
                                                  && t1.BranchId == Convert.ToInt32(Session["Branchid"])
                                                  orderby t1.CustomerName ascending
                                                  select t1).ToList();
                                if (match_list.Count > 0)
                                {
                                    dr["MemberID"] = match_list[0].mem;
                                    dr["CustomerName"] = match_list[0].CustomerName;
                                    dr["FatherHusbandName"] = match_list[0].FatherHusbandName;
                                    dr["AadharNumber"] = match_list[0].AadharNumber;
                                    dr["MobileNo"] = match_list[0].MobileNo;
                                    dr["ImageUrl"] = match_list[0].ImageUrl;

                                    // emp1 = balayer.RetrveList("select GrpMemberID from svcf.membertogroupmaster where  MemberID=" + dsGridBrnach.Rows[i]["MemberIDNew"] + " ");
                                    var grpmemberid_list = (from mem1 in AllMemtoGrpList
                                                            where mem1.MemberID == Convert.ToInt32(MemidNew)
                                                            select mem1.GrpMemberID).ToList();

                                    StringBuilder sb = new StringBuilder();
                                    foreach (var emp in grpmemberid_list)
                                    {
                                        // string finaldatenum = emp.Split('#')[1].Trim(',');
                                        sb.Append(emp + ",");
                                    }
                                    string empeee = sb.ToString();
                                    string tokenno = empeee.TrimEnd(',');
                                    dr["TokenNo"] = tokenno;
                                    dr["MemberIDNew"] = match_list[0].MemberIDNew;
                                    dr["BranchId"] = match_list[0].BranchId;
                                    dsbranch.Rows.Add(dr.ItemArray);
                                }
                                else
                                {
                                    //dr["MemberID"] = member.mem;
                                    //dr["CustomerName"] = member.CustomerName;
                                    //dr["FatherHusbandName"] = member.FatherHusbandName;
                                    //dr["AadharNumber"] = member.AadharNumber;
                                    //dr["MobileNo"] = member.MobileNo;
                                    //dr["ImageUrl"] = member.ImageUrl;
                                    //dr["TokenNo"] = "";
                                    //dr["MemberIDNew"] = member.MemberIDNew;
                                    //dr["BranchId"] = member.BranchId;
                                    //dsbranch.Rows.Add(dr.ItemArray);
                                }

                            }

                        }
                        catch (Exception ex)
                        {
                            string hh = ex.Message;
                        }

                    }
                }
                gridBranch.DataSource = dsbranch;
                gridBranch.DataBind();
            }
            catch (Exception err)
            {
                LogError(err, "Edit Membermaster - Search");
            }

        }

        private void LogError(Exception ex, string funcname)
        {
            string message = string.Format("Time: {0}", DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt"));
            message += Environment.NewLine;
            message += "------------------------------------------------------------";
            message += "BOOKLET EXPORT - " + funcname;
            message += Environment.NewLine;
            message += string.Format("Message: {0}", ex.Message);
            message += Environment.NewLine;
            message += string.Format("StackTrace: {0}", ex.StackTrace);
            message += Environment.NewLine;
            message += string.Format("Source: {0}", ex.Source);
            message += Environment.NewLine;
            message += string.Format("TargetSite: {0}", ex.TargetSite.ToString());
            message += Environment.NewLine;
            message += "-----------------------------------------------------------";
            message += Environment.NewLine;
            string path = Server.MapPath("~/ErrorLog/ErrorLog.txt");
            using (StreamWriter writer = new StreamWriter(path, true))
            {
                writer.WriteLine(message);
                writer.Close();
            }
        }
        protected void fillgrid()
        {
            string query="";
            try
            {

                //if (ddlbranch.SelectedItem.Text != "--select--")
                //{
                //    query = @"select ms.MemberIDNew,ms.MemberID as mem,ms.BranchId,ms.Title,ms.CustomerName,ms.TypeOfMember,ms.Gender,ms.Age,ms.FatherHusbandName,ms.AadharNumber,ms.MobileNo," +
                //        "md.ImageUrl from svcf.membermaster as ms left join membersdocuments as md on ms.MemberIDNew=md.MemberID  where ms.BranchId=" + ddlbranch.SelectedItem.Value + " order by ms.CustomerName asc";
                //    dsGridBrnach = balayer.GetDataTable(query);
                //}
                if (Convert.ToInt32(Session["Branchid"]) == 161)
                {
                    //query = @"select ms.MemberIDNew,ms.MemberID as mem,ms.BranchId,ms.Title,ms.CustomerName,ms.TypeOfMember,ms.Gender,ms.Age,ms.FatherHusbandName,ms.AadharNumber,ms.MobileNo," +
                    // "md.ImageUrl from svcf.membermaster as ms left join membersdocuments as md on ms.MemberIDNew=md.MemberID    order by ms.CustomerName asc";
                    // dsGridBrnach = GetCustomersPageWise(1);
                    //12/07/2021
                    if (ddlbranch.SelectedItem.Text != "--select--")
                    {
                        query = @"select ms.MemberIDNew,ms.MemberID as mem,ms.BranchId,ms.Title,ms.CustomerName,ms.TypeOfMember,ms.Gender,ms.Age,ms.FatherHusbandName,ms.AadharNumber,ms.MobileNo," +
                            "md.ImageUrl from svcf.membermaster as ms left join membersdocuments as md on ms.MemberIDNew=md.MemberID  where ms.BranchId=" + ddlbranch.SelectedItem.Value + " order by ms.CustomerName asc";
                        dsGridBrnach = balayer.GetDataTable(query);
                    }
                    else
                    {
                        dsGridBrnach = balayer.GetCustomersPageWisestore(1);
                    }
                }
                else
                {
                    query = @"select ms.MemberIDNew,ms.MemberID as mem ,ms.BranchId,ms.Title,ms.CustomerName,ms.TypeOfMember,ms.Gender,ms.Age,ms.FatherHusbandName,ms.AadharNumber,ms.MobileNo," +
                        "md.ImageUrl from svcf.membermaster as ms left join membersdocuments as md on ms.MemberIDNew=md.MemberID  where ms.BranchId=" + Session["Branchid"] + " order by ms.CustomerName asc";
                    dsGridBrnach = balayer.GetDataTable(query);
                }
               
        
              
                if (dsGridBrnach.Rows.Count > 0)
                {
                    dsbranch.Columns.Add("MemberIDNew");
                    dsbranch.Columns.Add("BranchId");
                    dsbranch.Columns.Add("ImageUrl");
                    dsbranch.Columns.Add("MemberID");
                    dsbranch.Columns.Add("CustomerName");
                    dsbranch.Columns.Add("FatherHusbandName");
                    dsbranch.Columns.Add("AadharNumber");
                    dsbranch.Columns.Add("MobileNo");
                    dsbranch.Columns.Add("TokenNo");
                    DataTable dsgrid1 = new DataTable();
                    DataTable grp = new DataTable();
                    DataTable dsgrid = new DataTable();
                    DataRow dr = dsbranch.NewRow();
                    List<string> token = new List<string>();
                    List<string> emp1 = new List<string>();
                    
                    var allgrpquery = "select mg.GrpMemberID,ms.MemberIDNew,mg.MemberID,ms.MemberID as mem,ms.BranchId,ms.Title,ms.CustomerName,ms.TypeOfMember,ms.Gender,ms.Age,ms.FatherHusbandName,ms.AadharNumber,ms.MobileNo,md.ImageUrl from svcf.membermaster as ms left join membersdocuments as md on ms.MemberIDNew = md.MemberID join svcf.membertogroupmaster as mg on(ms.MemberIDNew = mg.MemberID) order by ms.CustomerName asc ";
                    var AllGrp = balayer.GetDataTable(allgrpquery);
                    List<ClsMemberList> AllGroupList = AllGrp.DataTableToList<ClsMemberList>();
                    List<ClsMemberList> Member_FilteredList = null;
                  
                    var memtogrpquery = "select GrpMemberID,MemberID from svcf.membertogroupmaster";
                    var MemGrpList = balayer.GetDataTable(memtogrpquery);
                    List<ClsMemtoGroup> AllMemtoGrpList = MemGrpList.DataTableToList<ClsMemtoGroup>();
                    for (int i = 0; i < dsGridBrnach.Rows.Count; i++)
                    {
                        try
                        {
                           if (Convert.ToInt32(Session["Branchid"]) == 161)
                            {
                                // grp = balayer.GetDataTable("select mg.GrpMemberID,ms.MemberIDNew,mg.MemberID,ms.MemberID as mem,ms.BranchId,ms.Title,ms.CustomerName,ms.TypeOfMember,ms.Gender,ms.Age,ms.FatherHusbandName,ms.AadharNumber,ms.MobileNo,md.ImageUrl from svcf.membermaster as ms"+
                                //" left join membersdocuments as md on ms.MemberIDNew = md.MemberID join svcf.membertogroupmaster as mg "+
                                // "on(ms.MemberIDNew = mg.MemberID) where  mg.MemberID = " + dsGridBrnach.Rows[i]["MemberIDNew"] + " order by ms.CustomerName asc ");

                                //12/07/2021
                                if (ddlbranch.SelectedItem.Text != "--select--")
                                {
                                    Member_FilteredList = new List<ClsMemberList>();
                                    Member_FilteredList = (from t1 in AllGroupList
                                                           where t1.MemberID == Convert.ToInt32(dsGridBrnach.Rows[i]["MemberIDNew"])
                                                           && t1.BranchId==Convert.ToInt32(ddlbranch.SelectedItem.Value)
                                                           orderby t1.CustomerName ascending
                                                           select t1).ToList();
                                }
                                else
                                {

                                    Member_FilteredList = new List<ClsMemberList>();
                                    Member_FilteredList = (from t1 in AllGroupList
                                                           where t1.MemberID == Convert.ToInt32(dsGridBrnach.Rows[i]["MemberIDNew"])
                                                           orderby t1.CustomerName ascending
                                                           select t1).ToList();
                                }
                            }
                            else
                            {
                                //grp = balayer.GetDataTable("select mg.GrpMemberID,ms.MemberIDNew,mg.MemberID,ms.MemberID as mem ,ms.BranchId,ms.Title,ms.CustomerName,ms.TypeOfMember,ms.Gender,ms.Age,ms.FatherHusbandName,ms.AadharNumber,ms.MobileNo,md.ImageUrl from svcf.membermaster as ms left join membersdocuments as md on ms.MemberIDNew = md.MemberID join "+
                                //  "svcf.membertogroupmaster as mg on(ms.MemberIDNew = mg.MemberID) where ms.BranchId = " + Session["Branchid"] + " "+
                                //   "and mg.MemberID = " + dsGridBrnach.Rows[i]["MemberIDNew"] + " order by ms.CustomerName asc ");
                                Member_FilteredList = new List<ClsMemberList>();
                                Member_FilteredList = (from t1 in AllGroupList
                                                       where t1.MemberID == Convert.ToInt32(dsGridBrnach.Rows[i]["MemberIDNew"])
                                                       && t1.BranchId == Convert.ToInt32(Session["Branchid"])
                                                       orderby t1.CustomerName ascending
                                                       select t1).ToList();
                            }

                            if (Member_FilteredList.Count > 0)
                            {

                                dr["MemberID"] = Member_FilteredList[0].mem;  //grp.Rows[0]["mem"];
                                dr["CustomerName"] = Member_FilteredList[0].CustomerName; //grp.Rows[0]["CustomerName"];
                                dr["FatherHusbandName"] = Member_FilteredList[0].FatherHusbandName; //grp.Rows[0]["FatherHusbandName"];
                                dr["AadharNumber"] = Member_FilteredList[0].AadharNumber; //grp.Rows[0]["AadharNumber"];
                                dr["MobileNo"] = Member_FilteredList[0].MobileNo; //grp.Rows[0]["MobileNo"];
                                dr["ImageUrl"] = Member_FilteredList[0].ImageUrl; //grp.Rows[0]["ImageUrl"];
                                                             
                                var grpmemberid_list = (from mem1 in AllMemtoGrpList
                                                       where mem1.MemberID == Convert.ToInt32(dsGridBrnach.Rows[i]["MemberIDNew"])
                                                       select mem1.GrpMemberID).ToList();


                                //emp1 = balayer.RetrveList("select GrpMemberID from svcf.membertogroupmaster where  MemberID=" + dsGridBrnach.Rows[i]["MemberIDNew"] + " ");
                             
                                StringBuilder sb = new StringBuilder();
                                foreach (var emp in grpmemberid_list)
                                {
                                    // string finaldatenum = emp.Split('#')[1].Trim(',');
                                    sb.Append(emp + ",");
                                }
                                string empeee = sb.ToString();
                                string tokenno = empeee.TrimEnd(',');
                                dr["TokenNo"] = tokenno;
                                dr["MemberIDNew"] = Member_FilteredList[0].MemberIDNew; //grp.Rows[0]["MemberIDNew"];
                                dr["BranchId"] = Member_FilteredList[0].BranchId; //grp.Rows[0]["BranchId"];
                                dsbranch.Rows.Add(dr.ItemArray);
                            }
                            else
                            {
                                dr["MemberID"] =  dsGridBrnach.Rows[i]["mem"];
                                dr["CustomerName"] = dsGridBrnach.Rows[i]["CustomerName"];
                                dr["FatherHusbandName"] = dsGridBrnach.Rows[i]["FatherHusbandName"];
                                dr["AadharNumber"] = dsGridBrnach.Rows[i]["AadharNumber"];
                                dr["MobileNo"] = dsGridBrnach.Rows[i]["MobileNo"];
                                dr["ImageUrl"] = dsGridBrnach.Rows[i]["ImageUrl"];
                                dr["TokenNo"] = "";
                                dr["MemberIDNew"] = dsGridBrnach.Rows[i]["MemberIDNew"];
                                dr["BranchId"] = dsGridBrnach.Rows[i]["BranchId"];
                                dsbranch.Rows.Add(dr.ItemArray);
                            }
                        }
                        catch(Exception ex)
                        {
                            string hh = ex.Message;
                        }
                       
                    }
                }
                gridBranch.DataSource = dsbranch;
                gridBranch.DataBind();
            }
            catch (Exception err)
            {
                string jj = err.Message;
            }
            if (Convert.ToInt32(Session["Branchid"]) == 161)
            {
                this.PopulatePager(CommonClassFile.recordCount, CommonClassFile.pageIn);
            }
        }

       
        private void PopulatePager(int recordCount, int currentPage)
        {
            List<ListItem> pages = new List<ListItem>();
            int startIndex, endIndex;
            int pagerSpan = 5;

            //Calculate the Start and End Index of pages to be displayed.
            double dblPageCount = (double)((decimal)recordCount / Convert.ToDecimal(PageSize));
            int pageCount = (int)Math.Ceiling(dblPageCount);
            startIndex = currentPage > 1 && currentPage + pagerSpan - 1 < pagerSpan ? currentPage : 1;
            endIndex = pageCount > pagerSpan ? pagerSpan : pageCount;
            if (currentPage > pagerSpan % 2)
            {
                if (currentPage == 2)
                {
                    endIndex = 5;
                }
                else
                {
                    endIndex = currentPage + 2;
                }
            }
            else
            {
                endIndex = (pagerSpan - currentPage) + 1;
            }

            if (endIndex - (pagerSpan - 1) > startIndex)
            {
                startIndex = endIndex - (pagerSpan - 1);
            }

            if (endIndex > pageCount)
            {
                endIndex = pageCount;
                startIndex = ((endIndex - pagerSpan) + 1) > 0 ? (endIndex - pagerSpan) + 1 : 1;
            }

            //Add the First Page Button.
            if (currentPage > 1)
            {
                pages.Add(new ListItem("First", "1"));
            }

            //Add the Previous Button.
            if (currentPage > 1)
            {
                pages.Add(new ListItem("<<", (currentPage - 1).ToString()));
            }

            for (int i = startIndex; i <= endIndex; i++)
            {
                pages.Add(new ListItem(i.ToString(), i.ToString(), i != currentPage));
            }

            //Add the Next Button.
            if (currentPage < pageCount)
            {
                pages.Add(new ListItem(">>", (currentPage + 1).ToString()));
            }

            //Add the Last Button.
            if (currentPage != pageCount)
            {
                pages.Add(new ListItem("Last", pageCount.ToString()));
            }
            rptPager.DataSource = pages;
            rptPager.DataBind();
        }
        protected void gridBranch_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                DataTable groupid = new DataTable();
               e.Row.Attributes.Add("style", "cursor:help;");
                 if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    Label lblDue = (Label)e.Row.FindControl("lblMemberIDnew");
                    // GridViewRow row = gridBranch.Rows[rowIndex];
                    var jj = lblDue.Text;
                 //   if (ddlbranch.SelectedItem.Text != "--select--")
                 //   {
                 //       groupid = balayer.GetDataTable("select MemberID from svcf.membertogroupmaster where BranchID=" + ddlbranch.SelectedItem.Value + " and MemberID=" + jj);
                 //   }
                  //  else    if (Convert.ToInt32(Session["Branchid"]) == 161)
                  //  {
                      groupid = balayer.GetDataTable("select MemberID from svcf.membertogroupmaster where  MemberID=" + jj);
                   // }
                  //  else
                  //  {
                     //    groupid = balayer.GetDataTable("select MemberID from svcf.membertogroupmaster where BranchID=" + Session["Branchid"] + " and MemberID=" + jj);
                  //  }
                    if (groupid.Rows.Count > 0)
                    {
                          e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor='#ada43d'");
                          e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='#b9e0f5'");
                        e.Row.BackColor = System.Drawing.Color.Silver;
                      //  e.Row.BackColor = System.Drawing.Color.FromName("#E56E94");
                        

                    }
                    else
                    {
                        e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor='white'");
                        e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='#2ac1b3'");
                        //e.Row.BackColor = System.Drawing.Color.FromName("#ffffff");
                        e.Row.BackColor = System.Drawing.Color.White;
                    }
                   // Label lbltoken = (Label)e.Row.FindControl("TokenNo");

                    //lbltoken.Attributes.Add("style", "word-break:break-all;word-wrap:break-word;width:200px");
                }
            }
            catch ( Exception ex)
            {
                string hh = ex.Message;
            }
          

            
        }

        protected void ddlbranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            rptPager.Visible = false;
            fillgrid();

        }
    }
}

