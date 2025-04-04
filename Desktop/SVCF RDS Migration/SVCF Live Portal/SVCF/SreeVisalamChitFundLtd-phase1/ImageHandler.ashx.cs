using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using MySql.Data.MySqlClient;
using System.Data;
using SVCF_BusinessAccessLayer;
using SVCF_DataAccessLayer;
using SVCF_TransactionLayer;
namespace GridviewwithImages

//namespace SreeVisalamChitFundLtd_phase1
{
    /// <summary>
    /// Summary description for $codebehindclassname$
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class ImageHandler : IHttpHandler
    {
        #region Object
        BusinessLayer balayer = new BusinessLayer();
        TransactionLayer trn = new TransactionLayer();
        #endregion

        DataSet dsPics;
        MySqlDataAdapter  daPics;
        byte[] ImgContent;
        DataRow drPics;
        string strSql, strImgContentType;
        string con = CommonClassFile.ConnectionString;
        string strcon = CommonClassFile.ConnectionString;
        public void ProcessRequest(HttpContext context)
        {
            string imageid = balayer.ToobjectstrEvenNull(context.Request.QueryString["ImID"]);
            if (imageid.Trim() == "")
            {
                return;
            }
            strSql = "select Photo from membersdocuments where MemberID='" + imageid + "'";
            daPics = new MySqlDataAdapter(strSql, con);
            dsPics = new DataSet();
            daPics.Fill(dsPics);
            drPics = dsPics.Tables[0].Rows[0];
            ImgContent = (byte[])drPics["Photo"];
            System.IO.MemoryStream mstream =
            new System.IO.MemoryStream(ImgContent, 0, ImgContent.Length);
            System.Drawing.Image dbImage =
            System.Drawing.Image.FromStream(new System.IO.MemoryStream(ImgContent));
            System.Drawing.Image thumbnailImage =
            dbImage.GetThumbnailImage(100, 100, null, new System.IntPtr());
            thumbnailImage.Save(mstream, dbImage.RawFormat);
            Byte[] thumbnailByteArray = new Byte[mstream.Length];
            mstream.Position = 0;
            mstream.Read(thumbnailByteArray, 0, Convert.ToInt32(mstream.Length));
            context.Response.Clear();
            context.Response.ContentType = "image/jpeg";
            context.Response.BinaryWrite(thumbnailByteArray);

            context.Response.End();
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
