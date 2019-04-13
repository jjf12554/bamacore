using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace WEBTools
{
    public class EncryptTool
    {
        /// <summary>
        /// MD5(64)
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public static string MD5Encrypt64(string password)
        {
            string cl = password;
            //string pwd = "";
            MD5 md5 = MD5.Create(); //实例化一个md5对像
                                    // 加密后是一个字节类型的数组，这里要注意编码UTF8/Unicode等的选择　
            byte[] s = md5.ComputeHash(Encoding.UTF8.GetBytes(cl));
            return Convert.ToBase64String(s);
        }
        /// <summary>
        /// RSA加密(较长)
        /// </summary>
        /// <param name="express"></param>
        /// <returns></returns>
        public static string RSAEncrypt(string express)
        {
            CspParameters param = new CspParameters();
            param.KeyContainerName = "jxbama";//密匙容器的名称，保持加密解密一致才能解密成功
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(param))
            {
                byte[] plaindata = Encoding.Default.GetBytes(express);//将要加密的字符串转换为字节数组
                byte[] encryptdata = rsa.Encrypt(plaindata, false);//将加密后的字节数据转换为新的加密字节数组
                return Convert.ToBase64String(encryptdata);//将加密后的字节数组转换为字符串
            }
        }

        /// <summary>
        /// RSA解密
        /// </summary>
        /// <param name="ciphertext"></param>
        /// <returns></returns>
        public static string RSADecrypt(string ciphertext)
        {
            CspParameters param = new CspParameters();
            param.KeyContainerName = "jxbama";
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(param))
            {
                byte[] encryptdata = Convert.FromBase64String(ciphertext);
                byte[] decryptdata = rsa.Decrypt(encryptdata, false);
                return Encoding.Default.GetString(decryptdata);
            }
        }

        #region DES 加密解密

        /// <summary>
        /// DES加密
        /// </summary>
        /// <param name="pToEncrypt"></param>
        /// <param name="sKey">密钥(8位字符)</param>
        /// <returns></returns>
        public static string DESEncrypt(string pToEncrypt, string sKey = "jxbama12")
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            byte[] inputByteArray = Encoding.Default.GetBytes(pToEncrypt);
            des.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
            des.IV = ASCIIEncoding.ASCII.GetBytes(sKey);
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            StringBuilder ret = new StringBuilder();
            foreach (byte b in ms.ToArray())
            {
                ret.AppendFormat("{0:X2}", b);
            }
            ret.ToString();
            return ret.ToString();


        }
        /// <summary>
        /// DES解密
        /// </summary>
        /// <param name="pToDecrypt"></param>
        /// <param name="sKey">密钥(8位字符)</param>
        /// <returns></returns>
        public static string DESDecrypt(string pToDecrypt, string sKey = "jxbama12")
        {
            if (string.IsNullOrEmpty(pToDecrypt))
            {
                return "";
            }
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();

            byte[] inputByteArray = new byte[pToDecrypt.Length / 2];
            for (int x = 0; x < pToDecrypt.Length / 2; x++)
            {
                int i = (Convert.ToInt32(pToDecrypt.Substring(x * 2, 2), 16));
                inputByteArray[x] = (byte)i;
            }

            des.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
            des.IV = ASCIIEncoding.ASCII.GetBytes(sKey);
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();

            StringBuilder ret = new StringBuilder();

            return System.Text.Encoding.Default.GetString(ms.ToArray());
        }
        #endregion
    }
}
