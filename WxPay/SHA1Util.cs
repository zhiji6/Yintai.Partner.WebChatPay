using System;
using System.Security.Cryptography;

namespace Yintai.Partner.WebChatPay.WxPay {
    public class SHA1Util {
		public static String Sha1(String s) {
			char[] hexDigits = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
					'a', 'b', 'c', 'd', 'e', 'f' };
			try {
                byte[] btInput = System.Text.Encoding.Default.GetBytes(s);
				SHA1 sha = new System.Security.Cryptography.SHA1CryptoServiceProvider(); 
			
				byte[] md = sha.ComputeHash(btInput);
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
	}
}
