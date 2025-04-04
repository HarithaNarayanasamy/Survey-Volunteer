using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SVCF_BusinessAccessLayer;
using SVCF_DataAccessLayer;
using SVCF_TransactionLayer;
using System.Data;
using System.IO;
using System.Text;
using System.Configuration;
using MySql.Data.MySqlClient;
using Microsoft.Win32;

namespace SreeVisalamChitFundLtd_phase1
{
    public partial class Notification : System.Web.UI.Page
    {
        BusinessLayer balayer = new BusinessLayer();
        TransactionLayer trn = new TransactionLayer();
        CommonVariables objCOM = new CommonVariables();
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var userinfo = HttpContext.Current.User.Identity.Name;
                var qry = "select rs.name from roles as rs inner join rights as rt on (rt.roleid=rs.id) where memberid=" + userinfo + "";
                var usrRole = balayer.GetSingleValue(qry);
                int branchId = Convert.ToInt32(Session["Branchid"]);
                txtTamil.Visible = false;
                txtScroll.Visible = false;
                label1.Visible = false;
                Label2.Visible = false;
                btnRed.Visible = false;
                btnGreen.Visible = false;
                btnBlue.Visible = false;
              
                //gv_editNotificaion.Visible = false;
                if (branchId == 161 && usrRole == "Administrator")
                {
                    txtScroll.Visible = true;
                    gv_editNotificaion.Visible = true;
                        gvUploadsUsers.Visible = false;
                    FileUpload1.Visible = true;
                    btnUpload.Visible = true;
                        //loadGrid();
                        loadUploads();
                    txtDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    ddlLanguage.Visible = true;
                    //ddlLanguage.Visible = false;
                    ddlLanguage.Items.Insert(0, "--Select--");
                    ddlLanguage.Items.Add("--Select--");
                    ddlLanguage.Items.Add("Tamil");
                    ddlLanguage.Items.Add("English");
                    ddlLanguage.SelectedIndex = 0;
                    label1.Visible = true;
                    Label2.Visible = true;
                    btnRed.Visible = true;
                    btnGreen.Visible = true;
                    btnBlue.Visible = true;
                }
                else 
                {
                    msgPanel.Visible = false;
                    gv_editNotificaion.Visible = false;
                    panelUpload.Visible = false;
                    gvUploads.Visible = false;
                    gvUploadsUsers.Visible = true;
                    loadUploadsUser();
                }
                
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtScroll.Text))
            {
                string msg= txtScroll.Text;
                string msgLang = "";
                string msgColor = txtScroll.ForeColor.Name;
                string msgFont = ddlLanguage.SelectedItem.ToString();
                if (msgFont == "--Select--")
                    msgLang = "English";
                else if (msgFont == "Tamil")
                    msgLang = "Tamil";
                else if (msgFont == "English")
                    msgLang = "English";
                
                //var msg1 = Encoding.UTF8.GetBytes(txtScroll.Text).ToString();
                //var msgTamil = System.Uri.UnescapeDataString(msg1);
                
                var msgCount = balayer.GetSingleValue("select count(Message) as count from notifications where Message='" + msg+"';");
                if (Convert.ToInt32(msgCount) > 0)
                {
                    Response.Write("<script>alert('Notification Already Exists');</script>");
                }
                else
                {
                    var qry = "insert into notifications (Message,Active,Font,Color) values ('" + msg + "'," + "1,'"+msgLang+"','"+msgColor+"')";
                    var iResult = trn.insertorupdateTrn(qry);
                    Response.Write("<script>alert('Notification Updated successfully');</script>");
                }
                txtScroll.Text = "";
            }
            if (!string.IsNullOrEmpty(txtTamil.Text))
            {
                string msg = txtTamil.Text;
                string msgLang = "";
                string msgColor = txtTamil.ForeColor.Name;
                string msgFont = ddlLanguage.SelectedItem.ToString();
                if (msgFont == "--Select--")
                    msgLang = "English";
                else if (msgFont == "Tamil")
                    msgLang = "Tamil";
                else if (msgFont == "English")
                    msgLang = "English";

                //var msg1 = Encoding.UTF8.GetBytes(txtScroll.Text).ToString();
                //var msgTamil = System.Uri.UnescapeDataString(msg1);

                var msgCount = balayer.GetSingleValue("select count(Message) as count from notifications where Message='" + msg + "';");
                if (Convert.ToInt32(msgCount) > 0)
                {
                    Response.Write("<script>alert('Notification Already Exists');</script>");
                }
                else
                {
                    var qry = "insert into notifications (Message,Active,Font,Color) values ('" + msg + "'," + "1,'" + msgLang + "','" + msgColor + "')";
                    var iResult = trn.insertorupdateTrn(qry);
                    Response.Write("<script>alert('Notification Updated successfully');</script>");
                }
                txtTamil.Text = "";
            }

            loadGrid();
            Response.Redirect(Request.RawUrl);
        }
        protected void clear()
        {
            Response.Redirect(Request.Url.AbsolutePath.ToString());
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            clear();
        }

        public void loadGrid()
        {
            try
            {
                DataTable dt = balayer.GetDataTable("Select * from notifications;");
                DataTable dtBind = new DataTable();
                dtBind.Columns.Add("ID");
                dtBind.Columns.Add("Message");
                dtBind.Columns.Add("Status");
                dtBind.Columns.Add("Font");

                DataRow dr = dtBind.NewRow();
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        dr["ID"] = dt.Rows[i]["ID"];
                        dr["Message"] = dt.Rows[i]["Message"];
                        dr["Status"] = ActiveToStatus( dt.Rows[i]["Active"].ToString());
                        dr["Font"] = dt.Rows[i]["Font"];
                        dtBind.Rows.Add(dr.ItemArray);
                    }
                }
                gv_editNotificaion.DataSource = dtBind;
                gv_editNotificaion.DataBind();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        private string ActiveToStatus(string active)
        {
            string active1 = "";
            switch(active)
            {
                case "1":
                    active1 = "Active";
                    break;
                case "0":
                    active1 = "InActive";
                    break;
            }
            return active1;
        }
        protected void gv_editNotificaion_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if(e.CommandName=="EditRow")
            {
                int rowIndex = ((GridViewRow)((LinkButton)e.CommandSource).NamingContainer).RowIndex;
                gv_editNotificaion.EditIndex = rowIndex;
                loadGrid();
            }
            else if(e.CommandName=="DeleteRow")
            {
                int id = Convert.ToInt32(e.CommandArgument);
                balayer.ExecuteQuery("delete from notifications where ID=" + id);
                gv_editNotificaion.EditIndex = -1;
                loadGrid();
            }
            else if(e.CommandName=="UpdateCancel")
            {
                gv_editNotificaion.EditIndex = -1;
                loadGrid();
            }
            else if(e.CommandName=="UpdateRow")
            {
                int rowIndex = ((GridViewRow)((LinkButton)e.CommandSource).NamingContainer).RowIndex;
                int id = Convert.ToInt32(e.CommandArgument);
                string msg = ((TextBox)gv_editNotificaion.Rows[rowIndex].FindControl("txtMsg")).Text;
                //string status = ((TextBox)gv_editNotificaion.Rows[rowIndex].FindControl("txtStatus")).Text;
                var status = ((RadioButtonList)gv_editNotificaion.Rows[rowIndex].FindControl("RadioButtonList1")).SelectedItem.Value;
                string qry1= "update notifications set Message='" + msg + "' , Active='" + status + "' where ID=" + id;
                balayer.ExecuteQuery(qry1);
                gv_editNotificaion.EditIndex = -1;
                loadGrid();
            }
            
        }

        protected void gvUploads_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if(e.CommandName=="Download")
            {
                Response.Clear();
                Response.ContentType = "application/octect-stream";
                Response.AppendHeader("content-disposition", "filename=" + e.CommandArgument);
                Response.TransmitFile(Server.MapPath("~/UploadsCircular/") + e.CommandArgument);
                Response.End();
            }
            else if(e.CommandName=="DeleteFile")
            {
                string DeleteFile = e.CommandArgument.ToString();
                string filePath= Server.MapPath("~/UploadsCircular/") + DeleteFile;
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                    balayer.ExecuteQuery("delete from circularuploads where FileName='" + DeleteFile + "';");
                    Response.Write("<script>alert('" + DeleteFile + " deleted Successfully.');</script>");
                    loadUploads();
                }
                else
                {
                    Response.Write("<script>alert('" + DeleteFile + " not Exists.');</script>");
                }
                
            }
            Response.Redirect(Request.RawUrl);
        }
       
        protected void btnUpload_Click(object sender, EventArgs e)
        {
            
            if(FileUpload1.HasFile)
            {
                var uploadDate = txtDate.Text;
                //var fileName = FileUpload1.FileName;
                FileInfo fileInfo = new FileInfo(FileUpload1.FileName);
                var query = "insert into circularuploads(FileName,FileExtension,Date) values('" + fileInfo.Name + "','" + fileInfo.Extension + "','" + balayer.indiandateToMysqlDate(uploadDate) + "');";
                FileUpload1.PostedFile.SaveAs(Server.MapPath("~/UploadsCircular/") + FileUpload1.FileName);
                Response.Write("<script>alert('File Uploaded Successfully.');</script>");
                var iResult1 = trn.insertorupdateTrn(query);
                loadUploads();
            }
            else
            {
                Response.Write("<script>alert('Please select a file to Upload.');</script>");
            }
            Response.Redirect(Request.RawUrl);
        }

        //protected void btnCirculars_Click(object sender, EventArgs e)
        //{
        //    FileUpload1.Visible = true;
        //    btnUpload.Visible = true;
        //    loadUploads();
        //}
        public void loadUploads()
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("File Name", typeof(string));
                //dt.Columns.Add("Size of the File", typeof(string));
                dt.Columns.Add("File Extension", typeof(string));
                dt.Columns.Add("Date1", typeof(string));
                DataRow dr = dt.NewRow();
                DataTable dtUploads = balayer.GetDataTable("select * from circularuploads;");
                if (dtUploads.Rows.Count > 0)
                {
                    for(int i=0;i<dtUploads.Rows.Count;i++)
                    {
                        dr["File Name"] = dtUploads.Rows[i]["FileName"];
                        dr["File Extension"] = dtUploads.Rows[i]["FileExtension"];
                        dr["Date1"] = dtUploads.Rows[i]["Date"];
                        dt.Rows.Add(dr.ItemArray);
                    }
                }
                //var files = Directory.GetFiles(Server.MapPath("~/UploadsCircular")).ToList();
                //if (files.Count > 0)
                //{
                //    foreach (string file in files)
                //    {
                //        FileInfo fi = new FileInfo(file);
                //        dt.Rows.Add(fi.Name, fi.Extension);
                //    }
                //}
                gvUploads.DataSource = dt;
                gvUploads.DataBind();
            }
            
            catch(Exception ex)
            {
                throw;
            }
        }
        public void loadUploadsUser()
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("File Name", typeof(string));
                //dt.Columns.Add("Size of the File", typeof(string));
                dt.Columns.Add("File Extension", typeof(string));
                dt.Columns.Add("Date1", typeof(string));
                DataRow dr = dt.NewRow();
                DataTable dtUploads = balayer.GetDataTable("select * from circularuploads;");
                if (dtUploads.Rows.Count > 0)
                {
                    for (int i = 0; i < dtUploads.Rows.Count; i++)
                    {
                        dr["File Name"] = dtUploads.Rows[i]["FileName"];
                        dr["File Extension"] = dtUploads.Rows[i]["FileExtension"];
                        dr["Date1"] = dtUploads.Rows[i]["Date"];
                        dt.Rows.Add(dr.ItemArray);
                    }
                }
                gvUploadsUsers.DataSource = dt;
                gvUploadsUsers.DataBind();
            }
            
            catch (Exception ex)
            {
                throw;
            }
        }

        protected void gvUploadsUsers_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Download")
            {
                Response.Clear();
                Response.ContentType = "application/octect-stream";
                Response.AppendHeader("content-disposition", "filename=" + e.CommandArgument);
                Response.TransmitFile(Server.MapPath("~/UploadsCircular/") + e.CommandArgument);
                Response.End();
            }
        }

        protected void btnFont_Click(object sender, EventArgs e)
        {
            //Response.Clear();
            //Response.ContentType = "application/octect-stream";
            //Response.TransmitFile(Server.MapPath("~/Content/metroui/fonts/Bamini.ttf"));
            //Response.End();
            //File.Copy("Bamini.ttf", Path.Combine("C:\\Windows", "Fonts" + "Bamini.ttf"), true);
            //RegistryKey fontkey = Microsoft.Win32.Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Fonts");

            //fontkey.SetValue("Bamini", "Bamini.ttf");
            //fontkey.Close();
            var fontLink = "https://www.freetamilfont.com/download.php?id=735930";
            Response.Redirect(fontLink);
        }

        protected void ddlLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selLang = ddlLanguage.SelectedItem.ToString();
            if (selLang == "--Select--")
            {
                Response.Write("<script>alert('Please select Language.');</script>");
            }
            if (selLang == "Tamil")
            {
                label1.Visible = true;
                txtTamil.Visible = true;
                txtScroll.Visible = false;
            }
            else if (selLang == "English")
            {
                label1.Visible = true;
                txtTamil.Visible = false;
                txtScroll.Visible = true;
            }
        }

        protected void btnOk_Click(object sender, EventArgs e)
        {
            Label2.Visible = true;
            btnRed.Visible = true;
            btnGreen.Visible = true;
            btnBlue.Visible = true;
        }

        protected void btnRed_Click(object sender, EventArgs e)
        {
            if(txtTamil.Visible)
            {
                txtTamil.ForeColor = System.Drawing.Color.Red;
            }
            else if(txtScroll.Visible)
            {
                txtScroll.ForeColor = System.Drawing.Color.Red;
            }
        }

        protected void btnGreen_Click(object sender, EventArgs e)
        {
            if (txtTamil.Visible)
            {
                txtTamil.ForeColor = System.Drawing.Color.Green;
            }
            else if (txtScroll.Visible)
            {
                txtScroll.ForeColor = System.Drawing.Color.Green;
            }
        }

        protected void btnBlue_Click(object sender, EventArgs e)
        {
            if (txtTamil.Visible)
            {
                txtTamil.ForeColor = System.Drawing.Color.Blue;
            }
            else if (txtScroll.Visible)
            {
                txtScroll.ForeColor = System.Drawing.Color.Blue;
            }
        }

        protected void gv_editNotificaion_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if(e.Row.RowType==DataControlRowType.DataRow)
                {
                    
                        HiddenField fontLang = (HiddenField)e.Row.FindControl("hfMsg");
                        var msglbl = (Label)e.Row.FindControl("lblMsg");
                        string fontType = fontLang.Value.ToString();
                        if(fontType=="Tamil")
                        {
                            msglbl.Font.Name = "Bamini";
                        }
                }
            }
            catch(Exception ex)
            {

            }
        }

        protected void btnAll_Click(object sender, EventArgs e)
        {
            gv_editNotificaion.Visible = true;
            loadGrid();
        }
    }
}