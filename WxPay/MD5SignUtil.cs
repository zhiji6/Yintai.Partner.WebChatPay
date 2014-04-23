using System;

namespace Yintai.Partner.WebChatPay.WxPay {
    public class MD5SignUtil {
		public static String Sign(String content, String key) {
			String signStr = "";
	
			if ("" == key) {
				throw new SDKRuntimeException("财付通签名key不能为空！");
			}
			if ("" == content) {
				throw new SDKRuntimeException("财付通签名内容不能为空");
			}
			signStr = content + "&key=" + key;

            var bb = MD5Util.MD5(signStr, System.Text.Encoding.Default);
            var cc = MD5Util.MD5(signStr, System.Text.Encoding.UTF8);
            var dd = MD5Util.MD5(signStr, System.Text.Encoding.ASCII);
            var ee = MD5Util.MD5(signStr, System.Text.Encoding.GetEncoding("GBK"));
			//return MD5Util.MD5(signStr).ToUpper();
            return MD5Util.MD5(signStr, System.Text.Encoding.UTF8).ToUpper();
	
		}
	
		public static bool VerifySignature(String content, String sign,
				String md5Key) {
			String signStr = content + "&key=" + md5Key;
			String calculateSign = MD5Util.MD5(signStr).ToUpper();
			String tenpaySign = sign.ToUpper();
			return (calculateSign == tenpaySign);
		}
	}
}
