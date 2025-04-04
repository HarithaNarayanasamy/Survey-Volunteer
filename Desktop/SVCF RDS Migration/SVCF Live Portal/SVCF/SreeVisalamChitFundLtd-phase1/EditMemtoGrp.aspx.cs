using SVCF_BusinessAccessLayer;
using SVCF_TransactionLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using log4net;
using log4net.Config;

namespace SreeVisalamChitFundLtd_phase1
{
    public partial class EditMemtoGrp : System.Web.UI.Page
    {
        #region Object

        BusinessLayer balayer = new BusinessLayer();
        TransactionLayer trn = new TransactionLayer();

        #endregion
        DataTable dtChitGrp = new DataTable();
        DataTable dtbran = new DataTable();
        string data; DataRow dr;
        int headid; string groupid;
        int B_id;
        ILog logger = log4net.LogManager.GetLogger(typeof(EditMemtoGrp));

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var userinfo = HttpContext.Current.User.Identity.Name;
                var qry = "select rs.name from roles as rs inner join rights as rt on (rt.roleid=rs.id) where memberid=" + userinfo + "";
                var usrRole = balayer.GetSingleValue(qry);
                if (usrRole == "Administrator")
                {
                    GetGroupMember();
                }
                else
                {
                    Response.Redirect("Home.aspx", false);
                }
            }
            logger.Info("Edit member to group- at: " + DateTime.Now);
        }
        protected void BtnStatisticsGo_Click(object sender, EventArgs e)
        {
            BindGridview();
        }

        public void GetGroupMember()
        {
            ddlChit.DataSource = null;
            ddlChit.DataBind();
            data = "SELECT `groupmaster`.`GROUPNO`,`groupmaster`.`Head_Id` FROM `svcf`.`groupmaster` where `groupmaster`.`IsFinished`=0 and `groupmaster`.`BranchID`=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + "";
            dtChitGrp = balayer.GetDataTable(data);
            ddlChit.DataSource = dtChitGrp;
            dr = dtChitGrp.NewRow();
            dr[0] = "--select--";
            dr[1] = "0";
            ddlChit.DataTextField = "GROUPNO";
            ddlChit.DataValueField = "Head_Id";
            dtChitGrp.Rows.InsertAt(dr, 0);
            ddlChit.DataBind();
        }

        //public void BindGridview()
        //{
        //    data = "select m1.BranchID, mm.CustomerName as MemberName, m1.MemberID,mm.MemberID as Membermasterid, m1.MemberAddress, m1.Head_Id, m1.GroupID, m1.GrpMemberID, m1.B_Id,b1.B_Name from svcf.membertogroupmaster m1 join svcf.membermaster mm on m1.MemberID=mm.MemberIDNew left join svcf.branchdetails b1 on b1.Head_Id=m1.B_Id where m1.GroupID=" + ddlChit.SelectedItem.Value + "";
        //    dtChitGrp = balayer.GetDataTable(data);
        //    gvDetails.DataSource = dtChitGrp;
        //    gvDetails.DataBind();
        //}
        public void BindGridview()
        {
            try
            {
                DataTable dtBind = new DataTable();
                dtBind.Columns.Add("GrpMemberID", typeof(int));
                dtBind.Columns.Add("TokenNumber");
                dtBind.Columns.Add("MemberName");
                dtBind.Columns.Add("B_Name");
                dtBind.Columns.Add("Head_Id");
                dtBind.Columns.Add("Membermasterid");
                var GrpMemberid = string.Empty;
                DataRow drBind = dtBind.NewRow();
                if (ddlChit.SelectedItem.Value != "--Select--")
                {
                    var dtHeads = balayer.GetDataTable(" select NodeID from headstree where ParentID=" + ddlChit.SelectedItem.Value);
                    for (int j = 0; j < dtHeads.Rows.Count; j++)
                    {
                        GrpMemberid = string.Empty;
                        string tokennumber = balayer.GetSingleValue(@"select cast(digits(mg1.GrpMemberID) as unsigned)as ChitNo1 from membertogroupmaster as mg1 join voucher as v1 on v1.Head_Id=mg1.Head_Id join view_groupwisedue as vgwd1 on vgwd1.`GroupId`=" + ddlChit.SelectedItem.Value + " left join trans_payment as tp1 on v1.Head_Id =tp1.TokenNumber join membermaster as mm on (mg1.MemberID=mm.MemberIDNew) where mg1.BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and mg1.GroupID=" + ddlChit.SelectedItem.Value + " and v1.Head_Id=" + dtHeads.Rows[j]["NodeID"] + " group by v1.Head_Id order by cast(digits(mg1.GrpMemberID) as unsigned),v1.Voucher_Type DESC ;");
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
                            drBind["TokenNumber"] = balayer.GetSingleValue("select GrpMemberID from membertogroupmaster where Head_Id=" + dtHeads.Rows[j]["NodeID"]);
                            string MemberName = balayer.GetSingleValue(@"select concat(mg1.MemberName) as `MemberName` from membertogroupmaster as mg1 join voucher as v1 on v1.Head_Id=mg1.Head_Id join view_groupwisedue as vgwd1 on vgwd1.`GroupId`=" + ddlChit.SelectedItem.Value + " left join trans_payment as tp1 on v1.Head_Id =tp1.TokenNumber join membermaster as mm on (mg1.MemberID=mm.MemberIDNew) where mg1.BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and mg1.GroupID=" + ddlChit.SelectedItem.Value + " and v1.Head_Id=" + dtHeads.Rows[j]["NodeID"] + " group by v1.Head_Id order by v1.ChoosenDate DESC ;");
                            if (string.IsNullOrEmpty(MemberName))
                            {
                                string strMemID = balayer.GetSingleValue("SELECT MemberID FROM svcf.membertogroupmaster where Head_id=" + dtHeads.Rows[j]["NodeID"]);
                                drBind["MemberName"] = balayer.GetSingleValue("SELECT CustomerName FROM svcf.membermaster where MemberIDNew=" + strMemID);
                            }
                            else
                            {
                                drBind["MemberName"] = MemberName;
                            }
                            string Branchname = balayer.GetSingleValue(@"select b1.B_Name as branchname from membertogroupmaster as mg1 join voucher as v1 on v1.Head_Id=mg1.Head_Id join view_groupwisedue as vgwd1 on vgwd1.`GroupId`=" + ddlChit.SelectedItem.Value + " left join trans_payment as tp1 on v1.Head_Id =tp1.TokenNumber join membermaster as mm on (mg1.MemberID=mm.MemberIDNew)left join svcf.branchdetails b1 on b1.Head_Id=mg1.B_Id where mg1.BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and mg1.GroupID=" + ddlChit.SelectedItem.Value + " and v1.Head_Id=" + dtHeads.Rows[j]["NodeID"] + " group by v1.Head_Id order by v1.ChoosenDate DESC ;");
                            if (string.IsNullOrEmpty(Branchname))
                            {
                                string branchid = balayer.GetSingleValue("select BranchID from membertogroupmaster where Head_Id=" + dtHeads.Rows[j]["NodeID"]);
                                drBind["B_Name"] = balayer.GetSingleValue("select B_Name from branchdetails where Head_Id=" + branchid);
                            }
                            else
                            {
                                drBind["B_Name"] = Branchname;
                            }
                            string memberid = balayer.GetSingleValue(@"select mm.MemberID as Membermasterid from membertogroupmaster as mg1 join voucher as v1 on v1.Head_Id=mg1.Head_Id join view_groupwisedue as vgwd1 on vgwd1.`GroupId`=" + ddlChit.SelectedItem.Value + " left join moneycollector as mc on mg1.M_Id=mc.moneycollid left join trans_payment as tp1 on v1.Head_Id =tp1.TokenNumber join membermaster as mm on (mg1.MemberID=mm.MemberIDNew) where mg1.BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and mg1.GroupID=" + ddlChit.SelectedItem.Value + " and v1.Head_Id=" + dtHeads.Rows[j]["NodeID"] + " group by v1.Head_Id order by v1.ChoosenDate DESC ");
                            if (string.IsNullOrEmpty(memberid))
                            {
                                string moneyname = balayer.GetSingleValue("SELECT MemberID FROM svcf.membertogroupmaster where Head_id=" + dtHeads.Rows[j]["NodeID"]);
                                drBind["Membermasterid"] = balayer.GetSingleValue("SELECT MemberID FROM svcf.membermaster where MemberIDNew=" + moneyname);
                            }
                            else
                            {
                                drBind["Membermasterid"] = memberid;
                            }


                            string headid = balayer.GetSingleValue(@"select mg1.Head_Id as Membermasterid from membertogroupmaster as mg1 join voucher as v1 on v1.Head_Id=mg1.Head_Id join view_groupwisedue as vgwd1 on vgwd1.`GroupId`=" + ddlChit.SelectedItem.Value + " left join moneycollector as mc on mg1.M_Id=mc.moneycollid left join trans_payment as tp1 on v1.Head_Id =tp1.TokenNumber join membermaster as mm on (mg1.MemberID=mm.MemberIDNew) where mg1.BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and mg1.GroupID=" + ddlChit.SelectedItem.Value + " and v1.Head_Id=" + dtHeads.Rows[j]["NodeID"] + " group by v1.Head_Id order by v1.ChoosenDate DESC ");
                            if (string.IsNullOrEmpty(memberid))
                            {
                                drBind["Head_Id"] = balayer.GetSingleValue("SELECT Head_Id FROM svcf.membertogroupmaster where Head_id=" + dtHeads.Rows[j]["NodeID"]);
                                // drBind["Membermasterid"] = balayer.GetSingleValue("SELECT MemberID FROM svcf.membermaster where MemberIDNew=" + moneyname);
                            }
                            else
                            {
                                drBind["Head_Id"] = headid;
                            }

                            dtBind.Rows.Add(drBind.ItemArray);
                        }
                    }
                    DataView dv = dtBind.DefaultView;
                    dv.Sort = "GrpMemberID asc";
                    DataTable sortedDT = dv.ToTable();
                    gvDetails.DataSource = sortedDT;
                }

                gvDetails.DataBind();
            }
            catch (Exception err)
            {
                logger.Error(DateTime.Now + " | " + "EditMemtoGp.aspx - BindGridview():  Error: " + err.Message + " |  by: " + Convert.ToString(Session["Branchid"]) + "");
            }
        }

        public void gvDetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow && gvDetails.EditIndex == e.Row.RowIndex)
            {
                try
                {
                    DropDownList ddlprice = (DropDownList)e.Row.FindControl("ddlmembername");
                    HiddenField hdnval = (HiddenField)e.Row.FindControl("hdmemb");

                    ddlprice.DataSource = null;
                    ddlprice.DataBind();
                    //data = "SELECT concat(CustomerName,'|',MemberID) as MemberName,MemberIDNew FROM svcf.membermaster where CustomerName<>'Sree Visalam Chit Fund Ltd'";
                    data = "SELECT concat(CustomerName,'|',MemberID) as MemberName,MemberIDNew FROM svcf.membermaster";
                    dtChitGrp = balayer.GetDataTable(data);
                    ddlprice.DataSource = dtChitGrp;
                    dr = dtChitGrp.NewRow();
                    dr[0] = "--select--";
                    dr[1] = "0";
                    dtChitGrp.Rows.InsertAt(dr, 0);
                    ddlprice.DataTextField = "MemberName";
                    ddlprice.DataValueField = "MemberIDNew";
                    ddlprice.DataBind();
                    //ddlprice.Items.Insert(0, new ListItem("--Select--", "0"));
                    if (ddlprice.Items.FindByText(hdnval.Value) != null)
                        ddlprice.Items.FindByText(hdnval.Value).Selected = true;

                    DropDownList ddlbranchname = (DropDownList)e.Row.FindControl("ddlbranchname");
                    HiddenField hdbranchname = (HiddenField)e.Row.FindControl("hdbranchname");

                    ddlbranchname.DataSource = null;
                    ddlbranchname.DataBind();
                    data = "select NodeID,Node from headstree where ParentID=1;";
                    dtbran = balayer.GetDataTable(data);
                    ddlbranchname.DataSource = dtbran;
                    dr = dtbran.NewRow();
                    dr[1] = "--select--";
                    dr[0] = "0";
                    dtbran.Rows.InsertAt(dr, 0);
                    ddlbranchname.DataTextField = "Node";
                    ddlbranchname.DataValueField = "NodeID";
                    ddlbranchname.DataBind();
                    //ddlmembername.Items.Insert(0, new ListItem("--Select--", "0"));
                    if (ddlbranchname.Items.FindByText(hdbranchname.Value) != null)
                        ddlbranchname.Items.FindByText(hdbranchname.Value).Selected = true;
                }
                catch(Exception err)
                {
                    logger.Error(DateTime.Now + " | " + "EditMemtoGp.aspx - gvDetails_RowDataBound():  Error: " + err.Message + " |  by: " + Convert.ToString(Session["Branchid"]) + "");
                }

            }
        }

        protected void gvDetails_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvDetails.EditIndex = e.NewEditIndex;
            BindGridview();
        }

        protected void gvDetails_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvDetails.EditIndex = -1;
            BindGridview();
        }
        protected void gvDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvDetails.PageIndex = e.NewPageIndex;
            BindGridview();
        }

        protected void gvDetails_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                int productid = Convert.ToInt32(gvDetails.DataKeys[e.RowIndex].Value.ToString());
                int iColHeadID = -1;
                string strHeadLabel = "";
                int iColGrpMemberID = -1;
                string strGrpMemberIDLabel = "";
                for (int iCol = 0; iCol < gvDetails.Columns.Count; iCol++)
                {

                    if (gvDetails.Columns[iCol].HeaderText == "Token")
                    {
                        iColGrpMemberID = iCol;
                        foreach (Control cntTemp in gvDetails.Rows[e.RowIndex].Cells[iCol].Controls)
                            if (cntTemp.GetType().FullName.EndsWith(".Label"))
                                strGrpMemberIDLabel = cntTemp.ID;
                    }
                    if (gvDetails.Columns[iCol].HeaderText == "Head_Id")
                    {
                        iColHeadID = iCol;
                        foreach (Control cntTemp in gvDetails.Rows[e.RowIndex].Cells[iCol].Controls)
                            if (cntTemp.GetType().FullName.EndsWith(".Label"))
                                strHeadLabel = cntTemp.ID;
                    }
                }
                if (iColHeadID >= 0 && iColGrpMemberID >= 0)
                {
                    DropDownList ddlmembername = (DropDownList)gvDetails.Rows[e.RowIndex].FindControl("ddlmembername");
                    DropDownList ddlbranchname = (DropDownList)gvDetails.Rows[e.RowIndex].FindControl("ddlbranchname");
                    string strBranchID = "";
                    string strMemAddres = "";
                    string Membername = "";
                    string ipaddress;
                    ipaddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                    if (ipaddress == "" || ipaddress == null)
                        ipaddress = Request.ServerVariables["REMOTE_ADDR"];
                    string hostname = Request.UserHostName;
                    hostname = hostname + ":" + ipaddress;
                    DateTime presentdt = DateTime.Now;

                    DataTable dtMemDetails = balayer.GetDataTable("Select BranchId, AddressForCommunication From membermaster Where MemberIDNew = " + ddlmembername.SelectedValue.ToString());

                    if (ddlmembername.SelectedItem.Text != "--select--" && ddlbranchname.SelectedItem.Text != "--select--")
                    {
                        strBranchID = Convert.ToString(dtMemDetails.Rows[0]["BranchId"]);
                        strMemAddres = Convert.ToString(dtMemDetails.Rows[0]["AddressForCommunication"]);
                        Membername = ddlmembername.SelectedItem.Text.Split('|')[0];

                        string getExistingMemberId = Convert.ToString(balayer.GetScalarDataInt("select MemberID from svcf.membertogroupmaster where BranchID=" + Session["BranchId"] + " and Head_Id=" + ((Label)gvDetails.Rows[e.RowIndex].Cells[iColHeadID].FindControl(strHeadLabel)).Text + ""));

                        headid = Convert.ToInt32(((Label)gvDetails.Rows[e.RowIndex].Cells[iColHeadID].FindControl(strHeadLabel)).Text);

                        groupid = ((Label)gvDetails.Rows[e.RowIndex].Cells[iColGrpMemberID].FindControl(strGrpMemberIDLabel)).Text;

                        string GrpId = balayer.GetSingleValue("select GroupID from svcf.membertogroupmaster where head_id=" + headid + "");
                        balayer.Updatemembertogroup_st(Membername, ddlbranchname.SelectedValue.ToString(), strMemAddres, ddlmembername.SelectedValue.ToString(), headid, GrpId);


                        //data = "Update svcf.membertogroupmaster set MemberName='" + Membername + "', B_Id = " + ddlbranchname.SelectedValue.ToString() + ", MemberAddress = '" + strMemAddres + "', MemberID=" + ddlmembername.SelectedValue.ToString() + " where Head_Id=" + ((Label)gvDetails.Rows[e.RowIndex].Cells[iColHeadID].FindControl(strHeadLabel)).Text + " and GrpMemberID='" + ((Label)gvDetails.Rows[e.RowIndex].Cells[iColGrpMemberID].FindControl(strGrpMemberIDLabel)).Text + "'";
                        //trn.insertorupdateTrn(data);
                        //trn.CommitTrn();


                        string query = "";

                        query = "insert into svcf.EditMembertoGroupLogg(BranchId, OldMemberId, NewMemberId, HostingIP, EditedDate, Head_Id) values(" + strBranchID + "," + getExistingMemberId + "," +
                              "" + ddlmembername.SelectedValue + ",'" + hostname + "','" + balayer.changedateformat(presentdt, 1) + "'," + ((Label)gvDetails.Rows[e.RowIndex].Cells[iColHeadID].FindControl(strHeadLabel)).Text + ")";
                        balayer.ExecuteQuery(query);

                    }
                    else if (ddlbranchname.SelectedItem.Text == "--select--")
                    {
                        strMemAddres = Convert.ToString(dtMemDetails.Rows[0]["AddressForCommunication"]);
                        Membername = ddlmembername.SelectedItem.Text.Split('|')[0];
                        string getExistingMemberId = Convert.ToString(balayer.GetScalarDataInt("select MemberID from svcf.membertogroupmaster where BranchID=" + Session["BranchId"] + " and Head_Id=" + ((Label)gvDetails.Rows[e.RowIndex].Cells[iColHeadID].FindControl(strHeadLabel)).Text + ""));



                        headid = Convert.ToInt32(((Label)gvDetails.Rows[e.RowIndex].Cells[iColHeadID].FindControl(strHeadLabel)).Text);
                        //groupid = ((Label)gvDetails.Rows[e.RowIndex].Cells[iColGrpMemberID].FindControl(strGrpMemberIDLabel)).Text;
                        string GrpId = balayer.GetSingleValue("select GroupID from svcf.membertogroupmaster where head_id=" + headid + "");
                        B_id = Convert.ToInt32(Session["Branchid"]);

                        balayer.Updatemembertogroup_st(Membername, Convert.ToString(B_id), strMemAddres, ddlmembername.SelectedValue.ToString(), headid, GrpId);



                        //data = "Update svcf.membertogroupmaster set MemberName='" + Membername + "', MemberAddress = '" + strMemAddres + "', MemberID=" + ddlmembername.SelectedValue.ToString() + " where Head_Id=" + ((Label)gvDetails.Rows[e.RowIndex].Cells[iColHeadID].FindControl(strHeadLabel)).Text + " and GrpMemberID='" + ((Label)gvDetails.Rows[e.RowIndex].Cells[iColGrpMemberID].FindControl(strGrpMemberIDLabel)).Text + "'";
                        //trn.insertorupdateTrn(data);
                        //trn.CommitTrn();


                        string query = "";
                        query = "insert into svcf.EditMembertoGroupLogg(BranchId, OldMemberId, NewMemberId, HostingIP, EditedDate, Head_Id) values(" + strBranchID + "," + getExistingMemberId + "," +
                              "" + ddlmembername.SelectedValue + ",'" + hostname + "','" + balayer.changedateformat(presentdt, 1) + "'," + ((Label)gvDetails.Rows[e.RowIndex].Cells[iColHeadID].FindControl(strHeadLabel)).Text + ")";
                        balayer.ExecuteQuery(query);

                    }
                    else if (ddlmembername.SelectedItem.Text == "--select--")
                    {

                        string GrpmemberId = balayer.GetSingleValue("select GrpMemberID from svcf.membertogroupmaster where head_id=" + headid + "");

                        data = "Update svcf.membertogroupmaster set  B_Id = " + ddlbranchname.SelectedValue.ToString() + " where Head_Id=" + ((Label)gvDetails.Rows[e.RowIndex].Cells[iColHeadID].FindControl(strHeadLabel)).Text + " and GrpMemberID='" + GrpmemberId + "'";
                        trn.insertorupdateTrn(data);
                        trn.CommitTrn();
                    }
                    logger.Info("EditMemtoGrp.aspx - gvDetails_RowUpdating():  Completed: " + DateTime.Now + " by: " + Convert.ToString(Session["UserName"]) + "");

                }

                gvDetails.EditIndex = -1;
                BindGridview();
            }
            catch (Exception err)
            {
                logger.Error(DateTime.Now + " | " + "EditMemtoGrpnew.aspx - Rowupdating():  Error: " + err.Message + " |  by: " + Convert.ToString(Session["Branchid"]) + "");
            }

        }

        protected void ddlbranchname_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DropDownList ddlBranch = (DropDownList)sender;
                string branchid = ddlBranch.SelectedValue;

                // Get the GridViewRow in which this master DropDownList exists
                GridViewRow row = (GridViewRow)ddlBranch.NamingContainer;

                // Find all of the other DropDownLists within the same row and bind them
                DropDownList ddlmembername = (DropDownList)row.FindControl("ddlmembername");
                var MemberList = balayer.GetDataTable("SELECT concat(CustomerName,'|',MemberID) as MemberName,MemberIDNew FROM svcf.membermaster where  BranchId=" + branchid + "");
                ddlmembername.DataSource = MemberList;              
                ddlmembername.DataValueField = "MemberIDNew";
                ddlmembername.DataTextField = "MemberName";
                ddlmembername.DataBind();
            }
            catch (Exception err)
            {
                logger.Error(DateTime.Now + " | " + "EditMemtoGrpnew.aspx - ddlbranchname_SelectedIndexChanged():  Error: " + err.Message + " |  by: " + Convert.ToString(Session["Branchid"]) + "");
            }
        }
            
        }
    }
//}