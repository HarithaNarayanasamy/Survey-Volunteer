using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.Configuration;
using System.IO;
using System.Text;
using System.Data;
using SVCF_BusinessAccessLayer;
using SVCF_TransactionLayer;
using SVCF_DataAccessLayer;

namespace SreeVisalamChitFundLtd_phase1
{


    public class Encryptpwd
    {

        #region VarDeclaration
        BusinessLayer balayer = new BusinessLayer();
        TransactionLayer tranlayer = new TransactionLayer();
        DataTable Getpwd = new DataTable();

        long pwdupdate = 0;
        string encrypwd = "";
        string valueCr = "";
        string valueDr = "";
        decimal decCr = 0;
        decimal decDr = 0;

        #endregion


        private string Encrypt(string clearText)
        {
            string EncryptionKey = "MAKV2SPBNI99212";
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }

        public void UpdateEncrypedPwrd()
        {

            Getpwd = balayer.GetDataTable("select * from login ;");

            for (int i = 0; i < Getpwd.Rows.Count; i++)
            {
                encrypwd = Encrypt(Convert.ToString(Getpwd.Rows[i]["Password"]));
                pwdupdate = tranlayer.insertorupdateTrn("update login set Encryptpwd= '" + encrypwd + "'  where Sl_No=" + Getpwd.Rows[i]["Sl_No"] + "");
            }


        }
    }
}