using System;
using System.Security.Cryptography;
using System.Text;
using System.Web.Script.Serialization;
using SVCF_BusinessAccessLayer;
using SVCF_TransactionLayer;
using System.IO;
using System.Collections.Generic;

namespace SreeVisalamChitFundLtd_phase1
{
    public partial class WebForm6 : System.Web.UI.Page
    {


        protected void Page_Load(object sender, EventArgs e)
        {
            
        }
        BusinessLayer balayer = new BusinessLayer();
        TransactionLayer trn = new TransactionLayer();
        public string strinput;
        //public string Key = "8UHjPgXZzXCGkhxV2QCnooyJexUzvJrO";
        public string Key = "F3229A0B371ED2D9441B830D21A390C3";
        public string strencrypted;
       
        protected void Button1_Click(object sender, EventArgs e)
        {
           
            EncryptAesManaged(strinput);
        }
        protected void Button3_Click(object sender, EventArgs e)
        {
            strinput = TextBox1.Text;
            strencrypted = trn.GetStringTrn("Select encrypteddata from encryptdata where actualdata ='" + strinput + "'");
            DecryptAesManaged(strencrypted);
        }




        public void EncryptAesManaged(string raw)
        {
            string Encrypted = Aes256CbcEncrypter.Encrypt(raw, Key);
            trn.insertorupdateTrn("insert into encryptdata (encrypteddata,actualdata) values('" + Encrypted + "','" + strinput + "')");
            strencrypted = trn.GetStringTrn("Select encrypteddata from encryptdata where actualdata ='" + strinput + "'");
            Label1.Text = "Encrypted data : " + strencrypted;
                                  
        }
        public void DecryptAesManaged(string raw)
        {
            string Decrypted = Aes256CbcEncrypter.Decrypt(raw, Key);
            Label2.Text = "Decrypted data : " + Decrypted;
        }

        class Aes256CbcEncrypter
        {
            private static readonly Encoding encoding = Encoding.UTF8;

            public static string Encrypt(string plainText, string key)
            {
                try
                {
                    RijndaelManaged aes = new RijndaelManaged();
                    aes.KeySize = 256;
                    aes.BlockSize = 128;
                    aes.Padding = PaddingMode.PKCS7;
                    aes.Mode = CipherMode.CBC;

                    aes.Key = encoding.GetBytes(key);
                    aes.GenerateIV();

                    ICryptoTransform AESEncrypt = aes.CreateEncryptor(aes.Key, aes.IV);
                    byte[] buffer = encoding.GetBytes(plainText);

                    string encryptedText = Convert.ToBase64String(AESEncrypt.TransformFinalBlock(buffer, 0, buffer.Length));

                    String mac = "";

                    mac = BitConverter.ToString(HmacSHA256(Convert.ToBase64String(aes.IV) + encryptedText, key)).Replace("-", "").ToLower();

                    var keyValues = new Dictionary<string, object>
                {
                    { "iv", Convert.ToBase64String(aes.IV) },
                    { "value", encryptedText },
                    { "mac", mac },
                };

                    JavaScriptSerializer serializer = new JavaScriptSerializer();

                    return Convert.ToBase64String(encoding.GetBytes(serializer.Serialize(keyValues)));
                }
                catch (Exception e)
                {
                    throw new Exception("Error encrypting: " + e.Message);
                }
            }

            public static string Decrypt(string plainText, string key)
            {
                try
                {
                    RijndaelManaged aes = new RijndaelManaged();
                    aes.KeySize = 256;
                    aes.BlockSize = 128;
                    aes.Padding = PaddingMode.PKCS7;
                    aes.Mode = CipherMode.CBC;
                    aes.Key = encoding.GetBytes(key);

                    // Base 64 decode
                    byte[] base64Decoded = Convert.FromBase64String(plainText);
                    string base64DecodedStr = encoding.GetString(base64Decoded);

                    // JSON Decode base64Str
                    JavaScriptSerializer ser = new JavaScriptSerializer();
                    var payload = ser.Deserialize<Dictionary<string, string>>(base64DecodedStr);

                    aes.IV = Convert.FromBase64String(payload["iv"]);

                    ICryptoTransform AESDecrypt = aes.CreateDecryptor(aes.Key, aes.IV);
                    byte[] buffer = Convert.FromBase64String(payload["value"]);

                    return encoding.GetString(AESDecrypt.TransformFinalBlock(buffer, 0, buffer.Length));
                }
                catch (Exception e)
                {
                    throw new Exception("Error decrypting: " + e.Message);
                }
            }


         static byte[] HmacSHA256(String data, String key)
            {
               using (HMACSHA256 hmac = new HMACSHA256(encoding.GetBytes(key)))
                {
            return hmac.ComputeHash(encoding.GetBytes(data));
                }
            }


        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            TextBox1.Text = "";
            Label1.Text = "Label1";
            Label2.Text = "Label2";

        }

        protected void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        protected void Button4_Click(object sender, EventArgs e)
        {
           // hexEncryptedStr = crypt.MySqlAesEncrypt(clearText, password);
            Label3.Text = balayer.GetSingleValue("select  sum(CAST(AES_DECRYPT(encrypteddata,UNHEX('F3229A0B371ED2D9441B830D21A390C3')) AS DECIMAL(6,2))) from encryptdata  where actualdata =123.45 or actualdata = 250 or actualdata = 55.25");

            //string parr = balayer.GetSingleValue("select(case when(tp1.PaymentDate is null) then (case when((39360.00 - sum(case when(v1.Voucher_Type = 'C' and v1.trans_Type <> 2 and v1.Other_Trans_Type <> 5) then v1.Amount else 0.00 end) + sum(case when(v1.Voucher_Type = 'D' and v1.trans_Type <> 2 and v1.Other_Trans_Type <> 5) then v1.Amount else 0.00 end)) > 0.00) then (39360.00 - sum(case when(v1.Voucher_Type = 'C' and v1.trans_Type <> 2 and v1.Other_Trans_Type <> 5) then v1.Amount else 0.00 end)+ sum(case when(v1.Voucher_Type = 'D' and v1.trans_Type <> 2 and v1.Other_Trans_Type <> 5) then v1.Amount else 0.00 end)) else 0.00 end) else 0.00 end  ) as PArrier from membertogroupmaster as mg1 join voucher as v1 on v1.Head_Id = mg1.Head_Id join groupmaster as vgwd1 on vgwd1.`Head_Id`= 1152452 left join trans_payment as tp1 on v1.Head_Id = tp1.TokenNumber join membermaster as mm on(mg1.MemberID = mm.MemberIDNew) where mg1.B_Id = 03370 and mg1.GroupID = 1152452 and v1.Head_Id = 1152605 and v1.ChoosenDate <= '2022-10-13' ");
        }

        protected void Button5_Click(object sender, EventArgs e)
        {
            strinput = TextBox1.Text;
            trn.insertorupdateTrn("insert into encryptdata(encrypteddata, actualdata) values(AES_ENCRYPT(" + strinput + ", UNHEX('F3229A0B371ED2D9441B830D21A390C3')), " + strinput + ")");
            strencrypted = trn.GetStringTrn("Select encrypteddata from encryptdata where actualdata ='" + strinput + "'");
            Label1.Text = "Encrypted data : " + strencrypted;
        }

        protected void Button6_Click(object sender, EventArgs e)
        {
            strinput = TextBox1.Text;
            strencrypted = trn.GetStringTrn("Select encrypteddata from encryptdata where actualdata ='" + strinput + "'");
            Label2.Text = trn.GetStringTrn("select CAST(AES_DECRYPT(encrypteddata, UNHEX('F3229A0B371ED2D9441B830D21A390C3')) AS DECIMAL(6, 2)) from encryptdata where actualdata = '" + strinput + "'");                    
        }
    }
}
           

   