using System;
using System.Collections.Generic;
using System.Linq;

namespace Yintai.Partner.WebChatPay.WxPay {
    public class WxPayHelper {
		public WxPayHelper() {
			this.parameters = new Dictionary<string,string>();
			this.AppId = "";
			this.AppKey = "";
			this.SignType = "sha1";
			this.PartnerKey = "";
		}
	
		public sealed class Anonymous_C0 : 
				IComparer<KeyValuePair<string,string> > {
			public int Compare(KeyValuePair<string,string>  o1,
					KeyValuePair<string,string>  o2) {
				return String.CompareOrdinal((o1.Key).ToString(),o2.Key);
			}
		}
	
		private Dictionary<string,string> parameters;
		private string AppId;
		private string AppKey;
		private string SignType;
		private string PartnerKey;
	
		public void SetAppId(string str) {
			AppId = str;
		}
	
		public void SetAppKey(string str) {
			AppKey = str;
		}
	
		public void SetSignType(string str) {
			SignType = str;
		}
	
		public void SetPartnerKey(string str) {
			PartnerKey = str;
		}
	
		public void SetParameter(string key, string value_ren) {
			parameters.Add(key, value_ren);
		}
	
		public string GetParameter(string key) {
			return parameters[key];
		}
	
		private Object CheckCftParameters() {
			if (parameters["bank_type"] == "" || parameters["body"] == "" || parameters["partner"] == "" || parameters["out_trade_no"] == ""
					|| parameters["total_fee"] == "" || parameters["fee_type"] == "" || parameters["notify_url"] == null || parameters["spbill_create_ip"] == ""
					|| parameters["input_charset"] == "") {
				return false;
			}
			return true;
		}
	
		public string GetCftPackage() {
			if ("" == PartnerKey) {
				throw new SDKRuntimeException("密钥不能为空！");
			}
			string unSignParaString = CommonUtil.FormatBizQueryParaMap(parameters,
					false);
			string paraString = CommonUtil.FormatBizQueryParaMap(parameters, true);
			return paraString + "&sign="
					+ MD5SignUtil.Sign(unSignParaString, PartnerKey);
	
		}
	
		public string GetBizSign(Dictionary<string,string> bizObj) {
			Dictionary<string,string> bizParameters = new Dictionary<string,string>();
	
			foreach (KeyValuePair<string, string> item in bizObj) {
				if (item.Key != "") {
					bizParameters.Add(item.Key.ToLower(), item.Value);
				}
			}
	
			if(this.AppKey == "") {
				throw new SDKRuntimeException("APPKEY为空！");
			}
			bizParameters.Add("appkey", AppKey);
			string bizString = CommonUtil.FormatBizQueryParaMap(bizParameters, false);
			return SHA1Util.Sha1(bizString);
	
		}
	
		// 生成app支付请求json
		
		public string CreateAppPackage(string traceid) {
			Dictionary<string,string> nativeObj = new Dictionary<string,string>();
			if ((bool)((CheckCftParameters())) == false) {
				throw new SDKRuntimeException("生成package参数缺失！");
			}
			nativeObj.Add("appid", AppId);
			nativeObj.Add("package", GetCftPackage());
            nativeObj.Add("timestamp", ((DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000000).ToString());
			nativeObj.Add("traceid", traceid);
			nativeObj.Add("noncestr", CommonUtil.CreateNoncestr());
			nativeObj.Add("app_signature",GetBizSign(nativeObj));
			nativeObj.Add("sign_method", SignType);
		
			var entries = nativeObj.Select(d =>string.Format("\"{0}\": \"{1}\"", d.Key , d.Value));
            
			return "{" + string.Join(",", entries.ToArray()) + "}";
		
		}
	
		// 生成jsapi支付请求json
		
		public string CreateBizPackage() {
			Dictionary<string,string> nativeObj = new Dictionary<string,string>();
			if ((bool)((CheckCftParameters())) == false) {
				throw new SDKRuntimeException("生成package参数缺失！");
			}
			nativeObj.Add("appId",AppId);
			nativeObj.Add("package",GetCftPackage());
		    nativeObj.Add("timeStamp", ((DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000000).ToString());//"1397619778");//
		    nativeObj.Add("nonceStr", CommonUtil.CreateNoncestr());//"pvthNriOyr63M6fl");//
			nativeObj.Add("paySign",GetBizSign(nativeObj));
			nativeObj.Add("signType",SignType);
            
			
			var entries = nativeObj.Select(d =>string.Format("\"{0}\": \"{1}\"", d.Key , d.Value));
			return "{" + string.Join(",", entries.ToArray()) + "}";
	
		}
	
		// 生成原生支付url
		/*
		 * weixin://wxpay/bizpayurl?sign=XXXXX&appid=XXXXXX&productid=XXXXXX&timestamp
		 * =XXXXXX&noncestr=XXXXXX
		 */
		public string CreateNativeUrl(string productid) {
			string bizString = "";
			try {
				Dictionary<string,string> nativeObj = new Dictionary<string,string>();
				nativeObj.Add("appid", AppId);
				nativeObj.Add("productid",System.Web.HttpUtility.UrlEncode(productid));
                nativeObj.Add("timestamp", ((DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000000).ToString());
				nativeObj.Add("noncestr", CommonUtil.CreateNoncestr());
				nativeObj.Add("sign", GetBizSign(nativeObj));
				bizString = CommonUtil.FormatBizQueryParaMap(nativeObj, false);
	
			} catch (Exception e) {
				throw new SDKRuntimeException(e.Message);
	
			}
			return "weixin://wxpay/bizpayurl?" + bizString;
		}
	
		// 生成原生支付请求xml
		
		public string CreateNativePackage(string retcode, string reterrmsg) {
			Dictionary<string,string> nativeObj = new Dictionary<string,string>();
			if ((bool)((CheckCftParameters())) == false && retcode == "0") {
				throw new SDKRuntimeException("生成package参数缺失！");
			}
			nativeObj.Add("AppId", AppId);
			nativeObj.Add("Package", GetCftPackage());
            nativeObj.Add("TimeStamp", ((DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000000).ToString());
			nativeObj.Add("RetCode", retcode);
			nativeObj.Add("RetErrMsg", reterrmsg);
			nativeObj.Add("NonceStr", CommonUtil.CreateNoncestr());
			nativeObj.Add("AppSignature",GetBizSign(nativeObj));
			nativeObj.Add("SignMethod",SignType);
			return CommonUtil.ArrayToXml(nativeObj);
	
		}
	}
}
