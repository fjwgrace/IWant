using System;
using System.IO;
using System.Security.Cryptography;  
using System.Text;
namespace Core.Common
{
	/// <summary>
	/// DES加密/解密类。
	/// </summary>
	public class DESEncrypt
	{
		public DESEncrypt()
		{			
		}

		#region ========加密======== 
 
        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="Text"></param>
        /// <returns></returns>
		public  string Encrypt(string Text) 
		{
			return Encrypt(Text,"MATICSOFT");
		}
		/// <summary> 
		/// 加密数据 
		/// </summary> 
		/// <param name="Text"></param> 
		/// <param name="sKey"></param> 
		/// <returns></returns> 
		public  string Encrypt(string Text,string sKey) 
		{
            StringBuilder strRetValue = new StringBuilder();
            byte[] keyBytes = Encoding.UTF8.GetBytes(sKey.Substring(0, 8));
            byte[] keyIV = keyBytes;
            byte[] inputByteArray = Encoding.UTF8.GetBytes(Text);
            DESCryptoServiceProvider provider = new DESCryptoServiceProvider();

            provider.Mode = CipherMode.ECB;//兼容其他语言的Des加密算法  
            provider.Padding = PaddingMode.PKCS7;//自动补0

            MemoryStream mStream = new MemoryStream();
            CryptoStream cStream = new CryptoStream(mStream, provider.CreateEncryptor(keyBytes, keyIV), CryptoStreamMode.Write);
            cStream.Write(inputByteArray, 0, inputByteArray.Length);
            cStream.FlushFinalBlock();
            //组织成16进制字符串            
            foreach (byte b in mStream.ToArray())
            {
                strRetValue.AppendFormat("{0:x2}", b);
            }
            return strRetValue.ToString();
        }

        #endregion

        #region ========解密======== 


        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="Text"></param>
        /// <returns></returns>
        public  string Decrypt(string Text) 
		{
			return Decrypt(Text,"MATICSOFT");
		}
		/// <summary> 
		/// 解密数据 
		/// </summary> 
		/// <param name="Text"></param> 
		/// <param name="sKey"></param> 
		/// <returns></returns> 
		public  string Decrypt(string strDecryptString, string sKey) 
		{
            string strRetValue = "";

            byte[] keyBytes = Encoding.UTF8.GetBytes(sKey.Substring(0, 8));
            byte[] keyIV = keyBytes;

            //不使用base64解码
            //byte[] inputByteArray = Convert.FromBase64String(decryptString);

            //16进制转换为byte字节
            byte[] inputByteArray = new byte[strDecryptString.Length / 2];
            for (int x = 0; x < strDecryptString.Length / 2; x++)
            {
                int i = (Convert.ToInt32(strDecryptString.Substring(x * 2, 2), 16));
                inputByteArray[x] = (byte)i;
            }

            DESCryptoServiceProvider provider = new DESCryptoServiceProvider();

            provider.Mode = CipherMode.ECB;//兼容其他语言的Des加密算法  
            provider.Padding = PaddingMode.PKCS7;//

            MemoryStream mStream = new MemoryStream();
            CryptoStream cStream = new CryptoStream(mStream, provider.CreateDecryptor(keyBytes, keyIV), CryptoStreamMode.Write);
            cStream.Write(inputByteArray, 0, inputByteArray.Length);
            cStream.FlushFinalBlock();

            //需要去掉结尾的null字符
            //strRetValue = Encoding.UTF8.GetString(mStream.ToArray());
            strRetValue = Encoding.UTF8.GetString(mStream.ToArray()).TrimEnd('\0');

            return strRetValue;
        }
        #endregion
    }
}
