using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace CommonHelper
{
    public class EncryptHelper
    {
        //加密矢量
        private static byte[] IV = { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
        private static string _key = "w9s7d_w918@#$80q2!@#s970";

        public static string Encrypt3DES(string a_strString)
        {
            return Encrypt3DES(a_strString, _key);
        }

        public static string Decrypt3DES(string a_strString)
        {
            return Decrypt3DES(a_strString);
        }

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="a_strString"></param>
        /// <param name="a_strKey"></param>
        /// <returns></returns>
        public static string Encrypt3DES(string a_strString, string a_strKey)
        {
            TripleDESCryptoServiceProvider DES = new TripleDESCryptoServiceProvider();

            byte[] b = ASCIIEncoding.ASCII.GetBytes(a_strKey);
            DES.Key = ASCIIEncoding.ASCII.GetBytes(a_strKey);
            DES.IV = IV;
            DES.Mode = CipherMode.ECB;
            ICryptoTransform DESEncrypt = DES.CreateEncryptor();
            byte[] Buffer = ASCIIEncoding.ASCII.GetBytes(a_strString);
            return Convert.ToBase64String(DESEncrypt.TransformFinalBlock(Buffer, 0, Buffer.Length));
        }

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="a_strString"></param>
        /// <param name="a_strKey"></param>
        /// <returns></returns>
        public static string Decrypt3DES(string a_strString, string a_strKey)
        {
            TripleDESCryptoServiceProvider DES = new TripleDESCryptoServiceProvider();
            DES.Key = ASCIIEncoding.ASCII.GetBytes(a_strKey);
            DES.IV = IV;
            DES.Mode = CipherMode.ECB;
            DES.Padding = System.Security.Cryptography.PaddingMode.PKCS7;
            ICryptoTransform DESDecrypt = DES.CreateDecryptor();
            string result = "";
            try
            {
                byte[] Buffer = Convert.FromBase64String(a_strString);
                result = ASCIIEncoding.ASCII.GetString(DESDecrypt.TransformFinalBlock(Buffer, 0, Buffer.Length));
            }
            catch (Exception e)
            {

            }
            return result;
        }

        public static string Md5(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return "";
            }

            byte[] hashvalue = (new MD5CryptoServiceProvider()).ComputeHash(Encoding.UTF8.GetBytes(text));
            return BitConverter.ToString(hashvalue).Replace("-", "");
        }
    }
}
