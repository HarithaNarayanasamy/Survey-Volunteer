using SVCF_BusinessAccessLayer;
using SVCF_DataAccessLayer;
using SVCF_TransactionLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Services;

namespace SreeVisalamChitFundLtd_phase1
{
    public partial class EditCourtMemberDetails : System.Web.UI.Page
    {
        #region Object
        BusinessLayer balayer = new BusinessLayer();
        CommonClassFile objCommonClass = new CommonClassFile();
        TransactionLayer trn = new TransactionLayer();
        
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                if((Request.QueryString["Hid"].ToString()!="") && (Request.QueryString["name"].ToString() != "")&& (Request.QueryString["token"].ToString() != ""))
                {
                    string parentid = Request.QueryString["Hid"].ToString();
                    string membername= Request.QueryString["name"].ToString();
                    string chitname = Request.QueryString["token"].ToString();
                    editvalget(parentid,membername,chitname);
                }
            }

        }

        protected void editvalget(string headid,string name,string chitnumber)
        {
            string query = @"select Distinct ch.HeadId ,ch.ChitName,ch.MemberName,tc.cc,cd.EPNo,cd.CourtComplex,cd.DateofBadDebts,cd.ARCyear,cd.Suityear,(select Node from headstree where NodeID= ch.ParentID) as head,ch.ParentID,tc.Number as arc from chitheads as ch Right join headstree as hs on (ch.HeadId =hs.NodeID) 
left join transcourt as tc on(ch.HeadId =tc.HeadID) join svcf.courtdetails as cd on(ch.HeadId=cd.Head_ID)   where ch.ParentID in (51,52,4638) and ch.branchid='" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + "' and ch.MemberName='" + name + "' and ch.ChitName ='"+chitnumber+"' and ch.HeadId='"+headid+"'  order by ch.chitname;";
            DataTable getvalue = balayer.GetDataTable(query);
            txtchitname.Text = chitnumber;
            txtmembername.Text = name;
            //txtchitname.Text = name;
            //txtmembername.Text = chitnumber;
            if (getvalue.Rows.Count>0)
            {
                txtccnumber.Text =Convert.ToString(getvalue.Rows[0]["cc"]);
                txtArcNumber.Text = Convert.ToString(getvalue.Rows[0]["arc"]);
                hdidlbl.Text = Convert.ToString(getvalue.Rows[0]["HeadId"]);
                txtepno.Text = Convert.ToString(getvalue.Rows[0]["EPNo"]);
                txtCourt_Complex.Text= Convert.ToString(getvalue.Rows[0]["CourtComplex"]);
                txtdate_debts.Text= Convert.ToString(getvalue.Rows[0]["DateofBadDebts"]);
                txtArcyear.Text = Convert.ToString(getvalue.Rows[0]["ARCyear"]);
                txtsuityear.Text = Convert.ToString(getvalue.Rows[0]["Suityear"]);
            }
            string query1 = @"SELECT * FROM svcf.courtdetails where Head_ID='"+ headid + "'";
            DataTable getval1 = balayer.GetDataTable(query1);
            if(getval1.Rows.Count>0)
            {
                if(txtccnumber.Text=="")
                {
                    txtccnumber.Text = Convert.ToString(getval1.Rows[0]["CC_Number"]);
                }
                if(txtArcNumber.Text =="")
                {
                    txtArcNumber.Text = Convert.ToString(getval1.Rows[0]["Arc_Number"]);
                }
                txtPartyAddress.Text = Convert.ToString(getval1.Rows[0]["Partyaddress"]);
                txtCourt.Text = Convert.ToString(getval1.Rows[0]["Court"]);
                txtCourtPlace.Text = Convert.ToString(getval1.Rows[0]["Court_Place"]);
            }
        }

        //[WebMethod]
        //public static List<System.Web.UI.WebControls.ListItem> Getbranchlist(string BranchId)
        //{

        //    BusinessLayer balayer = new BusinessLayer();
        //    string query = string.Empty;
        //    query = "SELECT NodeID,Node FROM svcf.headstree where ParentID=1";


        //    List<System.Web.UI.WebControls.ListItem> branches = new List<System.Web.UI.WebControls.ListItem>();

        //    branches = balayer.Getlistdata(query);

        //    return branches;

        //}
        protected void load_cancel(object sender, EventArgs e)
        {
            Response.Redirect("CourtAdvocateDegree.aspx");
        }


        protected void Btnupdate_click(object sender, EventArgs e)
        {
            string headidquery = Request.QueryString["Hid"].ToString();
            var membername = txtmembername.Text;
            var chitnumber = txtchitname.Text;
            var cc = txtccnumber.Text;
            var arcnum = txtArcNumber.Text;
            var partyaddress = txtPartyAddress.Text;
            var court = txtCourt.Text;
            var courtplace = txtCourtPlace.Text;
            var branchid = balayer.ToobjectstrEvenNull(Session["Branchid"]);
            var EPNoOrOSNo = txtepno.Text;
            var CourtComplex = txtCourt_Complex.Text;
            var DateofBadDebts = txtdate_debts.Text;
            var ARCyear = txtArcyear.Text;
            var Suityear = txtsuityear.Text;
            var headid = headidquery;

            var headcheck = balayer.GetDataTable("SELECT * FROM svcf.courtdetails where branchid='"+ branchid + "' and Head_ID='"+ headid + "';");
            if(headcheck.Rows.Count == 0)
            {
                var query = @"insert into courtdetails(Head_ID,ChitNumber,MemberName,CC_Number,Arc_Number,Partyaddress,Court,Court_Place,Branchid,EPNo,CourtComplex, DateofBadDebts,ARCyear, Suityear) values
                      ('" + headid + "','"+ chitnumber + "','"+membername+"','"+cc+"','"+ arcnum + "','"+partyaddress+"','"+court+"','"+courtplace+"','"+ branchid + "','"+ EPNoOrOSNo + "','"+ CourtComplex + "','"+ DateofBadDebts + "','"+ ARCyear + "','"+ Suityear + "')";

                trn.insertorupdateTrn(query);

                var transcheckquery = balayer.GetDataTable(@"SELECT * FROM svcf.transcourt where branchid='"+ branchid + "' and HeadId='"+ headid + "';");

                if(transcheckquery.Rows.Count!=0)
                {
                    var query1 = @"update svcf.transcourt set CC='" + cc + "', Number='" + arcnum + "' Where branchid='" + branchid + "' and HeadId='" + headid + "';";
                    trn.insertorupdateTrn(query1);
                }

                

                ModalPopupExtender1.PopupControlID = "pnlmsg";
                ModalPopupExtender1.Show();
                pnlmsg.Visible = true;
                lblh.Text = "Status";
                lblcon.Text = "Court Details Update Successfully";
                lblcon.ForeColor = System.Drawing.Color.Green;
                BtnOK.Visible = false;
                Button2.Visible = true;
                Button3.Visible = false;
                btnHide.Visible = false;


            }
            else
            {
                var query = "UPDATE svcf.courtdetails set ChitNumber='"+chitnumber+ "',MemberName='"+membername+ "',CC_Number='"+cc+ "',Arc_Number='"+ arcnum + "',"
                    + "Partyaddress='"+ partyaddress + "',Court='"+ court + "',Court_Place='"+ courtplace + "', EPNo='" + EPNoOrOSNo + "',CourtComplex='"+ CourtComplex + "',DateofBadDebts='"+ DateofBadDebts + "',ARCyear='"+ ARCyear + "',Suityear='"+ Suityear + "' where  Branchid='" + branchid + "' and Head_ID='"+headid+"';"; 
                trn.insertorupdateTrn(query);

                var transcheckquery = balayer.GetDataTable(@"SELECT * FROM svcf.transcourt where branchid='" + branchid + "' and HeadId='" + headid + "';");

                if (transcheckquery.Rows.Count != 0)
                {
                    var query1 = @"update svcf.transcourt set CC='" + cc + "', Number='" + arcnum + "' Where branchid='" + branchid + "' and HeadId='" + headid + "';";
                    trn.insertorupdateTrn(query1);
                }

                ModalPopupExtender1.PopupControlID = "pnlmsg";
                ModalPopupExtender1.Show();
                pnlmsg.Visible = true;
                lblh.Text = "Status";
                lblcon.Text = "Court Details Update Successfully";
                lblcon.ForeColor = System.Drawing.Color.Green;
                BtnOK.Visible = false;
                Button2.Visible = true;
                Button3.Visible = false;
                btnHide.Visible = false;

            }

        }


    }
}