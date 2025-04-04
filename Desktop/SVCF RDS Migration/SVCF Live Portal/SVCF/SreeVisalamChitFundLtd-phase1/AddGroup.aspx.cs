using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data;
using MySql.Data.MySqlClient;
using SVCF_BusinessAccessLayer;
using SVCF_DataAccessLayer;
using log4net;
using log4net.Config;

namespace SreeVisalamChitFundLtd_phase1
{
    public partial class WebForm5 : System.Web.UI.Page
    {
        #region Object
        BusinessLayer balayer = new BusinessLayer();
        #endregion
        ILog logger = log4net.LogManager.GetLogger(typeof(WebForm5));
        static DataTable DaTab;
        static List<string> lstDupName = new List<string>();
        static int noofm;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                String BrId = balayer.ToobjectstrEvenNull(balayer.ToobjectstrEvenNull(Session["Branchid"]));
                using (MySqlConnection conDaTab = balayer.OpenConnection())   // CommonClassFile.openConnection()
                {
                    DaTab = balayer.GetDataTable("select MemberID,CustomerName,AddressForCommunication from membermaster where CustomerName=any(select CustomerName from membermaster group by CustomerName having COUNT(*)<>1)");
                    lstDupName = (from dr in DaTab.AsEnumerable()
                                  select dr.Field<string>("CustomerName")).ToList();
                    lstDupName.Sort();
                }
                DataTable ChitGrp = new DataTable();
                MySqlConnection con;
                using (con = balayer.OpenConnection())
                {
                    try
                    {
                        MySqlDataAdapter mem2GrpAdp = new MySqlDataAdapter("select GROUPNO,NoofMembers from groupmaster where BranchID='" + BrId + "'", con);
                        mem2GrpAdp.Fill(ChitGrp);
                        //DataRow dr = ChitGrp.NewRow();
                        //dr[0] = "--select--";
                        //ChitGrp.Rows.InsertAt(dr, 0);
                        for (int i = 0; i < ChitGrp.Rows.Count; i++)
                        {
                            noofm = Convert.ToInt32(ChitGrp.Rows[i]["NoofMembers"]);
                            string GropuLoad = "select count(GroupID) from membertogroupmaster where GroupID='" + ChitGrp.Rows[i]["GROUPNO"] + "'";
                            int cntGrpID = Convert.ToInt32(balayer.GetSingleValue(GropuLoad));
                            if (noofm > cntGrpID)
                            {
                                ddlGrpID.Items.Add(balayer.ToobjectstrEvenNull(ChitGrp.Rows[i]["GROUPNO"]));
                            }
                        }
                        ddlGrpID.Items.Insert(0, "--select--");
                        //ddlGrpID.DataSource = ChitGrp;
                        //ddlGrpID.DataTextField = "GROUPNO";
                        //ddlGrpID.DataValueField = "NoofMembers";
                        //ddlGrpID.DataBind();
                        DataTable memTab = balayer.GetDataTable("select CustomerName,MemberID from membermaster where Branchid='" + BrId + "' order by CustomerName ");
                        DataRow dr1 = memTab.NewRow();
                        dr1[0] = "--select--";
                        dr1[1] = "--select--";
                        memTab.Rows.InsertAt(dr1, 0);
                        ddlMembName.DataSource = memTab;
                        ddlMembName.DataTextField = "CustomerName";
                        ddlMembName.DataValueField = "MemberID";
                        ddlMembName.DataBind();
                        ddlGrpID.SelectedIndex = 0;
                        ddlMembName.SelectedIndex = 0;
                        ddlMembName.Enabled = false;
                        ddlNocard.SelectedIndex = 0;
                        System.Web.UI.WebControls.ListItem l1 = new ListItem("Yes", "Yes");
                        System.Web.UI.WebControls.ListItem l2 = new ListItem("No", "No");

                        ddlNocard.Items.Add(l1);
                        ddlNocard.Items.Add(l2);
                        ddlNocard.DataTextField = "Nocard";
                        ddlNocard.DataValueField = "Nocard";
                        ddlNocard.SelectedIndex = 1;


                        DataTable dtMoney = balayer.GetDataTable("SELECT moneycollid,moneycollname FROM moneycollector where BranchID='" + BrId + "'");
                        DataRow dr2 = dtMoney.NewRow();
                        dr2[0] = "--select--";
                        dr2[1] = "--select--";

                        dtMoney.Rows.InsertAt(dr2, 0);
                        ddlMoneyCollector.DataSource = dtMoney;
                        ddlMoneyCollector.DataTextField = "moneycollname";
                        ddlMoneyCollector.DataValueField = "moneycollid";
                        ddlMoneyCollector.DataBind();
                        ddlMoneyCollector.SelectedIndex = 0;
                    }
                    catch (Exception ex)
                    {
                        // Handle the error
                    }
                }
            }
        }

        protected void AddChit_Click(object sender, EventArgs e)
        {
            if (txtMemaddr.Text.Trim() != "")
            {
                string brid = balayer.ToobjectstrEvenNull(balayer.ToobjectstrEvenNull(Session["Branchid"]));

                string strCommunicationAddressUpdteQuery = "Update membermaster set AddressForCommunication='" + balayer.MySQLEscapeString(txtMemaddr.Text) + "' where MemberID='" + balayer.MySQLEscapeString(ddlMembName.SelectedValue) + "'";

                balayer.GetInsertItem(strCommunicationAddressUpdteQuery);
                string insertGroup = "insert into membertogroupmaster(`BranchID`,`MemberName`,`MemberID`,`MemberAddress`,`GroupID`,`GrpMemberID`,`NomineeName`,`NomineeAddress`,`Nocard`,`EstCallNoOfAuction`,`EstSuretyDocument`) values('" + balayer.MySQLEscapeString(balayer.ToobjectstrEvenNull(brid)) + "','" + balayer.MySQLEscapeString(balayer.ToobjectstrEvenNull(ddlMembName.SelectedItem)) + "','" + balayer.MySQLEscapeString(txtMembID.Text) + "','" + balayer.MySQLEscapeString(txtMemaddr.Text) + "','" + balayer.MySQLEscapeString(balayer.ToobjectstrEvenNull(ddlGrpID.SelectedItem)) + "','" + balayer.ToobjectstrEvenNull(ddlgrpmemid.SelectedItem) + "','" + balayer.MySQLEscapeString(txtNominee.Text) + "','" + balayer.MySQLEscapeString(txtNomiaddr.Text) + "','" + balayer.MySQLEscapeString(balayer.ToobjectstrEvenNull(ddlNocard.SelectedItem)) + "','" + balayer.MySQLEscapeString(balayer.ToobjectstrEvenNull(txtEstCallAuction.Text)) + "','" + balayer.MySQLEscapeString(balayer.ToobjectstrEvenNull(txtSuretyDocument.Text)) + "')";
                int incmd = balayer.GetInsertItem(insertGroup);
                string insertnominee = "insert into nomineetable(`MemberID`,`NomineeName`,`NomineeAge`,`Relation`,`NomineeAddress`,`NomineeTelephoneNo`,`NomineeMobileNo`,`GrpMemberID`) values('" + balayer.MySQLEscapeString(txtMembID.Text) + "','" + balayer.MySQLEscapeString(txtNominee.Text) + "','" + balayer.MySQLEscapeString(txtnomineeAge.Text) + "','" + balayer.MySQLEscapeString(txtNominrela.Text) + "','" + balayer.MySQLEscapeString(txtNomiaddr.Text) + "','" + balayer.MySQLEscapeString(txtnomiTele.Text) + "','" + balayer.MySQLEscapeString(txtNomimobno.Text) + "','" + balayer.MySQLEscapeString(balayer.ToobjectstrEvenNull(ddlgrpmemid.SelectedItem)) + "')";
                int innominee = balayer.GetInsertItem(insertnominee);

                balayer.GetInsertItem("Update membertogroupmaster set M_Id='" + balayer.MySQLEscapeString(balayer.ToobjectstrEvenNull(ddlMoneyCollector.SelectedValue )) + "' where GrpMemberID='" + balayer.MySQLEscapeString(balayer.ToobjectstrEvenNull(ddlgrpmemid.SelectedItem)) + "'");


                int parentID = int.Parse(balayer.GetSingleValue("select NodeID from headstree Where Node ='" + balayer.MySQLEscapeString(balayer.ToobjectstrEvenNull(ddlGrpID.SelectedItem)) + "'"));

                string strSelectQueryPre = "select  TreeHint    from headstree  WHERE NodeID='" + parentID + "'";

                string strPreviousID = balayer.GetSingleValue(strSelectQueryPre);


                int iresult = balayer.GetInsertItem("insert into headstree(ParentID, Node, TreeHint) values('" + parentID + "','" + balayer.MySQLEscapeString(balayer.ToobjectstrEvenNull(ddlgrpmemid .SelectedItem)) + "','Null')");
                string strSelectQuery = "select  max(NodeID)   from headstree ";
                string strCurrentID = balayer.GetSingleValue(strSelectQuery);
                string strIdtoInsert = strPreviousID.Trim(',') + "," + strCurrentID.Trim(',');
                strIdtoInsert = strIdtoInsert.Trim(',');
                string strUpdateQuery = "UPDATE headstree SET TreeHint='" + strIdtoInsert + "'" + "WHERE NodeID='" + strCurrentID + "'";

                balayer.GetInsertItem(strUpdateQuery);

                balayer.GetInsertItem("UPDATE membertogroupmaster SET Head_Id='" + balayer.GetSingleValue("select  max(NodeID)   from headstree ") + "'" + "WHERE GroupID='" + balayer.MySQLEscapeString(balayer.ToobjectstrEvenNull(ddlGrpID.SelectedItem)) + "'");

                ScriptManager.RegisterStartupScript(this, GetType(), "Warning", "alert('" + balayer.ToobjectstrEvenNull(ddlgrpmemid.SelectedItem) + " " + "Added successfully!!!');", true);
                clear();
                logger.Info("AddGroup.aspx - AddChit_Click() - Completed: " + DateTime.Now + " by: " + Convert.ToString(Session["UserName"]) + "");
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Warning", "alert(' Member Address Cannot be Empty!!! ');", true);
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            clear();
        }

        protected void ddlMembName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!balayer.ToobjectstrEvenNull(ddlMembName.SelectedItem).Equals("--select--"))
            {
                string memdet = "select MemberID,CustomerName,AddressForCommunication from membermaster where CustomerName='" + balayer.MySQLEscapeString(balayer.ToobjectstrEvenNull(ddlMembName.SelectedItem) ) + "'";
                DataTable memdetTab = balayer.GetDataTable(memdet);
                if (memdetTab.Rows.Count > 0)
                {
                    if (memdetTab.Rows.Count > 1)
                    {
                        if (lstDupName.Contains(balayer.ToobjectstrEvenNull(ddlMembName.SelectedItem)))
                        {
                            ModalPopup.PopupControlID = "pandupName";
                            this.ModalPopup.Show();
                            pandupName.Visible = true;
                            GridView1.Visible = true;
                            GridView1.DataSource = memdetTab;
                            GridView1.DataBind();
                            Btncan.Visible = true;
                        }
                    }
                    else
                    {
                        txtMembID.Text = balayer.ToobjectstrEvenNull(memdetTab.Rows[0]["MemberID"]);
                        txtMemaddr.Text = balayer.ToobjectstrEvenNull(memdetTab.Rows[0]["AddressForCommunication"]);
                        txtMembID.ReadOnly = true;

                        string selNominee = "select count(MemberID),MemberID from nomineetable where MemberID='" + balayer.ToobjectstrEvenNull(ddlMembName.SelectedValue) + "'";
                        int ContMemberID = Convert.ToInt32(balayer.GetSingleValue(selNominee));
                        DataTable MemberIDTab = balayer.GetDataTable(selNominee);
                        string memberID = balayer.ToobjectstrEvenNull(MemberIDTab.Rows[0]["MemberID"]);
                        if (ContMemberID > 0)
                        {
                            string Nominee = "select `MemberID`,`NomineeName`,`NomineeAge`,`Relation`,`NomineeAddress`,`NomineeTelephoneNo`,`NomineeMobileNo` from nomineetable where MemberID='" + memberID + "'";
                            DataTable NomineeDetails = balayer.GetDataTable(Nominee);
                            ModalPopup.PopupControlID = "PanGrid";
                            this.ModalPopup.Show();
                            PanGrid.Visible = true;
                            GridNominee.Visible = true;
                            GridNominee.DataSource = NomineeDetails;
                            GridNominee.DataBind();
                            BtnNominCancel.Visible = true;
                        }
                        //txtMemaddr.ReadOnly = true;
                    }
                }
            }
            else
            {
                txtMembID.Text = "";
                txtMemaddr.Text = "";
                txtNominee.Text = "";
                txtNomiaddr.Text = "";
                ddlNocard.ClearSelection();
                txtNomimobno.Text = "";
                txtNominrela.Text = "";
                txtnomiTele.Text = "";
                txtnomineeAge.Text = "";
            }
        }
        protected void Btncan_Click(object sender, EventArgs e)
        {
            GridView1.Visible = false;
            this.ModalPopup.Hide();
            ddlMembName.ClearSelection();
        }
        protected void btnAdd_click(object sender, EventArgs e)
        {
            Button selbtn = (Button)sender;
            GridViewRow gr = (GridViewRow)selbtn.Parent.Parent;
            txtMembID.Text = ((Label)GridView1.Rows[gr.RowIndex].FindControl("lblMemberID")).Text;
            txtMemaddr.Text = ((Label)GridView1.Rows[gr.RowIndex].FindControl("lblMemAddr")).Text;
            txtMembID.ReadOnly = true;
            //txtMemaddr.ReadOnly = true;
            GridView1.Visible = false;
            this.ModalPopup.Hide();
            string selNominee = "select count(MemberID),MemberID from nomineetable where MemberID='" + balayer.ToobjectstrEvenNull(ddlMembName.SelectedValue) + "'";
            int ContMemberID = Convert.ToInt32(balayer.GetSingleValue(selNominee));
            DataTable MemberIDTab = balayer.GetDataTable(selNominee);
            string memberID = balayer.ToobjectstrEvenNull(MemberIDTab.Rows[0]["MemberID"]);
            if (ContMemberID > 0)
            {
                string Nominee = "select `MemberID`,`NomineeName`,`NomineeAge`,`Relation`,`NomineeAddress`,`NomineeTelephoneNo`,`NomineeMobileNo` from nomineetable where MemberID='" + memberID + "'";
                DataTable NomineeDetails = balayer.GetDataTable(Nominee);
                ModalPopup.PopupControlID = "PanGrid";
                this.ModalPopup.Show();
                PanGrid.Visible = true;
                GridNominee.Visible = true;
                GridNominee.DataSource = NomineeDetails;
                GridNominee.DataBind();
                BtnNominCancel.Visible = true;
            }
            pandupName.Visible = false;
        }

        protected void BtnNominCancel_Click(object sender, EventArgs e)
        {
            ModalPopup.Hide();
        }
        protected void ddlGrpID_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!balayer.ToobjectstrEvenNull(ddlGrpID.SelectedItem).Equals("--select--"))
            {
                List<string> loadGrpMemid = new List<string>();
                ddlMembName.Enabled = true;
                int noOfMembers;
                string GrpMemID = "select mg.GrpMemberID,g.NoofMembers from membertogroupmaster as mg inner join groupmaster as g on mg.GroupID='" + balayer.ToobjectstrEvenNull(ddlGrpID.SelectedItem) + "' and g.GROUPNO='" + balayer.ToobjectstrEvenNull(ddlGrpID.SelectedItem) + "'";
                DataTable grpmemIDTab = balayer.GetDataTable(GrpMemID);
                if (grpmemIDTab.Rows.Count != 0)
                {
                    noOfMembers  = Convert.ToInt32(balayer.ToobjectstrEvenNull(grpmemIDTab.Rows[0]["NoofMembers"]));
                }
                else
                {
                    noOfMembers = 0;
                }

                if (noOfMembers > 0)
                {
                    /*Load all grpId initially and add it to seperate list*/
                    for (int loadid = 1; loadid <= noOfMembers; loadid++)
                    {
                        loadGrpMemid.Add(balayer.ToobjectstrEvenNull(ddlGrpID.SelectedItem) + "-" + loadid);
                    }
                    /*Load existing grpId from membertogroupmaster*/
                    List<string> existingGrpMemID = new List<string>();

                    if (grpmemIDTab.Rows.Count > 0)
                    {
                        for (int noid = 0; noid < grpmemIDTab.Rows.Count; noid++)
                        {
                            existingGrpMemID.Add(balayer.ToobjectstrEvenNull(grpmemIDTab.Rows[noid]["GrpMemberID"]));
                        }
                        int toloadno = noofm - existingGrpMemID.Count;
                        List<string> remGrpID = new List<string>();
                        remGrpID = loadGrpMemid.Except(existingGrpMemID).ToList();
                        ddlgrpmemid.Items.Clear();
                        //if (remGrpID.Count == 0)
                        //{

                        //    ddlgrpmemid.Items.Add("completed");
                        //    ddlMembName.SelectedIndex = -1;
                        //    ddlMembName.Enabled = false;
                        //}
                        //else
                        //{
                        ddlgrpmemid.Items.Add("--select--");
                        ddlMembName.Enabled = true;
                        ddlMembName.SelectedIndex = 0;
                        foreach (string grpid in remGrpID)
                        {
                            ddlgrpmemid.Items.Add(grpid);
                        }
                        //}
                        ddlgrpmemid.SelectedIndex = 0;
                    }
                    else
                    {
                        ddlgrpmemid.Items.Add("--select--");
                        ddlMembName.SelectedIndex = 0;
                        foreach (string grpid in loadGrpMemid)
                        {
                            ddlgrpmemid.Items.Add(grpid);
                        }
                    }
                }
            }
            else
            {
                ddlgrpmemid.Items.Clear();
                ddlMembName.SelectedIndex = 0;
                ddlMembName.Enabled = false;
            }
        }

        protected void clear()
        {
            ddlGrpID.SelectedIndex = 0;
            ddlgrpmemid.Items.Clear();
            ddlMembName.SelectedIndex = 0;
            ddlMembName.Enabled = false;
            txtMembID.Text = "";
            txtMemaddr.Text = "";
            txtNominee.Text = "";
            txtNomiaddr.Text = "";
            ddlNocard.SelectedIndex = 1;
            txtNomimobno.Text = "";
            txtNominrela.Text = "";
            txtnomiTele.Text = "";
            txtnomineeAge.Text = "";
            txtEstCallAuction.Text = "";
            txtSuretyDocument.Text = "";
            ddlMoneyCollector.ClearSelection();
        }


        protected void btnNomineeAdd_Click(object sender, EventArgs e)
        {
            Button selBut = (Button)sender;
            GridViewRow selRow = (GridViewRow)selBut.Parent.Parent;
            txtNominee.Text = (GridNominee.Rows[selRow.RowIndex].FindControl("lblNomiName") as Label).Text;
            txtNomiaddr.Text = (GridNominee.Rows[selRow.RowIndex].FindControl("lblNominAddr") as Label).Text;
            txtnomineeAge.Text = (GridNominee.Rows[selRow.RowIndex].FindControl("lblNominage") as Label).Text;
            txtNominrela.Text = (GridNominee.Rows[selRow.RowIndex].FindControl("lblRelation") as Label).Text;
            txtnomiTele.Text = (GridNominee.Rows[selRow.RowIndex].FindControl("lblNomiTele") as Label).Text;
            txtNomimobno.Text = (GridNominee.Rows[selRow.RowIndex].FindControl("lblNomiMob") as Label).Text;
            PanGrid.Visible = false;
            ModalPopup.Hide();
        }
    }
}
