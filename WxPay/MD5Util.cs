using System;
using System.Security.Cryptography;
using System.Text;

namespace Yintai.Partner.WebChatPay.WxPay {
    public class MD5Util {
		public static String MD5(String s) {
			char[] hexDigits = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
					'A', 'B', 'C', 'D', 'E', 'F' };
			try {

                byte[] btInput = System.Text.Encoding.Default.GetBytes(s);
				// 获得MD5摘要算法的 MessageDigest 对象
				MD5 mdInst = System.Security.Cryptography.MD5.Create();
				// 使用指定的字节更新摘要
				mdInst.ComputeHash(btInput);
				// 获得密文
				byte[] md = mdInst.Hash;
				// 把密文转换成十六进制的字符串形式
				int j = md.Length;
				char[] str = new char[j * 2];
				int k = 0;
				for (int i = 0; i < j; i++) {
					byte byte0 = md[i];
					str[k++] = hexDigits[(int) (((byte) byte0) >> 4) & 0xf];
					str[k++] = hexDigits[byte0 & 0xf];
				}
                return new string(str); 
			} catch (Exception e) {
				Console.Error.WriteLine(e.StackTrace);
				return null;
			}
		}

        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="s">字符串</param>
        /// <param name="encoding">字符集编码</param>
        /// <returns></returns>
        public static String MD5(String s, Encoding encoding)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] bytValue = encoding.GetBytes(s);
            byte[] bytHash = md5.ComputeHash(bytValue);
            md5.Clear();

            StringBuilder result = new StringBuilder();
            for (int i = 0; i < bytHash.Length; i++)
            {
                result.Append(bytHash[i].ToString("x").PadLeft(2, '0'));
            }
            return result.ToString();
        }
	}
}
