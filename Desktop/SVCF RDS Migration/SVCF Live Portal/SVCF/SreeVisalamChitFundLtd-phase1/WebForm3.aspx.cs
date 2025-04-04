using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SVCF_TransactionLayer;
using SVCF_BusinessAccessLayer;
using System.Security.Cryptography;
using System.IO;

namespace SreeVisalamChitFundLtd_phase1
{
    public partial class WebForm3 : System.Web.UI.Page
    {
        BusinessLayer balayer = new BusinessLayer();
        TransactionLayer trn = new TransactionLayer();
        public byte[] retencrypted;
        public byte[] encrypted;
        public string strinput, strencrypted , decrypted;
        protected void Page_Load(object sender, EventArgs e)
        {
          
        }
         protected void Button1_Click(object sender, EventArgs e)
        {
            strinput = TextBox1.Text;                    
            EncryptAesManaged(strinput);
        }
        public void EncryptAesManaged(string raw)
        {
            try
            {
                using (AesManaged aes = new AesManaged())
                {
                    byte[] encrypted = Encrypt(raw, aes.Key, aes.IV);
                    trn.insertorupdateTrn("insert into encryptdata (encrypteddata,actualdata) values('" + System.Text.Encoding.UTF8.GetString(encrypted) + "','" + strinput + "')");
                    strencrypted = trn.GetStringTrn("Select encrypteddata from encryptdata where actualdata ='" + strinput + "'");
                    Label1.Text = "Encrypted data : " + strencrypted;
                    // Decrypt the bytes to a string.    
                    retencrypted = System.Text.Encoding.UTF32.GetBytes(strencrypted);
                    decrypted = Decrypt(encrypted, aes.Key, aes.IV);
                    Label2.Text = "Decrypted data : " + decrypted;
              }
            }
            catch (Exception exp)
            { 
                Label2.Text = exp.Message;
                
            }

        }
        public byte[] Encrypt(string plainText, byte[] Key, byte[] IV)
        {
            
            // Create a new AesManaged.    
            using (AesManaged aes = new AesManaged())
            {
                // Create encryptor    
                ICryptoTransform encryptor = aes.CreateEncryptor(Key, IV);
                // Create MemoryStream    
                using (MemoryStream ms = new MemoryStream())
                {
                    // Create crypto stream using the CryptoStream class. This class is the key to encryption    
                    // and encrypts and decrypts data from any given stream. In this case, we will pass a memory stream    
                    // to encrypt    
                    using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    {
                        // Create StreamWriter and write data to a stream    
                        using (StreamWriter sw = new StreamWriter(cs))
                            sw.Write(plainText);
                        encrypted = ms.ToArray();
                    }
                }

            }
            // Return encrypted data    
            return encrypted;
        }
        public string Decrypt(byte[] cipherText, byte[] Key, byte[] IV)
        {
            string plaintext = null;
            // Create AesManaged    
            using (AesManaged aes = new AesManaged())
            {
                // Create a decryptor    
                ICryptoTransform decryptor = aes.CreateDecryptor(Key, IV);
                // Create the streams used for decryption.    
                using (MemoryStream ms = new MemoryStream(cipherText))
                {
                    // Create crypto stream    
                    using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                    {
                        // Read crypto stream    
                        using (StreamReader reader = new StreamReader(cs))
                            plaintext = reader.ReadToEnd();
                    }
                }
            }
            return plaintext;
        }
    }
}