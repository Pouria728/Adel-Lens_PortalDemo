
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Encodings.Web;
using System.Web;


namespace HamrahanSystem.Presntation
{
    public class Helper
    {
   
        public static string DecryptPassword(string Pass, string key)
        {
            byte[] keyArray;
            byte[] toEncryptArray = Convert.FromBase64String(Pass);
            keyArray = UTF8Encoding.UTF8.GetBytes(key.Replace("-", "@*-").Substring(0, 24));
            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            tdes.Key = keyArray;
            tdes.Mode = CipherMode.ECB;
            tdes.Padding = PaddingMode.PKCS7;


            ICryptoTransform cTransform = tdes.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            tdes.Clear();
            return UTF8Encoding.UTF8.GetString(resultArray);
        }
        public static string EncryptPassword(string txt, string key)
        {
            if (string.IsNullOrEmpty(txt) || string.IsNullOrEmpty(key))
            {
                return string.Empty;
            }

            byte[] keyArray;
            byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(txt.ToString());
            var normalizedKey = key.Replace("-", "@*-");
            if (normalizedKey.Length < 24)
            {
                return string.Empty;
            }
            keyArray = UTF8Encoding.UTF8.GetBytes(normalizedKey.Substring(0, 24));
            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            tdes.Key = keyArray;
            tdes.Mode = CipherMode.ECB;
            tdes.Padding = PaddingMode.PKCS7;
            ICryptoTransform cTransform = tdes.CreateEncryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            tdes.Clear();
            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }
        public static string EncryptOrder(string txt)
        {
            byte[] keyArray;
            txt = txt.PadLeft(10, '0');
            byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(txt.ToString());
            keyArray = UTF8Encoding.UTF8.GetBytes("0z0Grtq837hsadfjhadf7bgf");
            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            tdes.Key = keyArray;
            tdes.Mode = CipherMode.ECB;
            tdes.Padding = PaddingMode.PKCS7;
            ICryptoTransform cTransform = tdes.CreateEncryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            tdes.Clear();
            return HttpUtility.UrlEncode( Convert.ToBase64String(resultArray, 0, resultArray.Length));
        }




    }
}
