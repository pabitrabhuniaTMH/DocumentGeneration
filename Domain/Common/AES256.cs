using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Common
{
    public class Aes256
    {
        public static string Encrypt(string text)
        {
            string keyValue = "gVkYp3s6v9y$B&E)H@MbQeThWmZq4t7w";
            System.Text.UTF8Encoding UTF8 = new();
            using (AesManaged tdes = new())
            {
                tdes.Key = UTF8.GetBytes(keyValue);
                tdes.Mode = CipherMode.ECB;
                tdes.Padding = PaddingMode.PKCS7;
                ICryptoTransform crypt = tdes.CreateEncryptor();
                byte[] plain = Encoding.UTF8.GetBytes(text);
                byte[] cipher = crypt.TransformFinalBlock(plain, 0, plain.Length);
                String encryptedText = Convert.ToBase64String(cipher);
                return encryptedText;
            }
        }
        public static string Decrypt(string enc_text)
        {
            byte[] text = Convert.FromBase64String(enc_text);
            string keyValue = "gVkYp3s6v9y$B&E)H@MbQeThWmZq4t7w";
            System.Text.UTF8Encoding UTF8 = new();
            using (AesManaged tdes = new())
            {
                tdes.Key = UTF8.GetBytes(keyValue);
                tdes.Mode = CipherMode.ECB;
                tdes.Padding = PaddingMode.PKCS7;
                ICryptoTransform crypt = tdes.CreateDecryptor();

                byte[] resultArray = crypt.TransformFinalBlock(text, 0, text.Length);
                string decodedString = Encoding.UTF8.GetString(resultArray);
                return decodedString;
            }
        }
    }
}
