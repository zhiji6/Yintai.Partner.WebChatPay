using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Yintai.Partner.WebChatPay.Config;
using Yintai.Partner.WebChatPay.Dto.Request;
using Yintai.Partner.WebChatPay.WxPay;

namespace Yintai.Partner.WebChatPay.Worker
{
    public class AddressWorker
    {
        WxPayHelper wxPayHelper = new WxPayHelper();
        WxPayConfig wxPayConfig = new WxPayConfig();
        private AddressRequest _addressRequest = new AddressRequest();

        public AddressWorker(AddressRequest addressRequest)
        {
            wxPayHelper.SetAppId(wxPayConfig.AppId);////""
            wxPayHelper.SetAppKey(wxPayConfig.AppKey);////""
            wxPayHelper.SetPartnerKey(wxPayConfig.PartnerKey);////""
            
            this._addressRequest = addressRequest;
            this._addressRequest.SignType = "sha1";
            this._addressRequest.AppId = wxPayConfig.AppId;
        }

        

        public string GetPaySign()
        {
            var source = this._addressRequest.GetSignSource();
            var  bizParameters = new Dictionary<string,string>();
            foreach (KeyValuePair<string, string> item in source)
            {
                if (item.Key != "")
                {
                    bizParameters.Add(item.Key.ToLower(), item.Value);
                }
            }
            
            string bizString = CommonUtil.FormatBizQueryParaMap(bizParameters, false);

            return SHA1Util.Sha1(bizString);
        }

        public Dictionary<string, string> GetPostData()
        {
            var data = new Dictionary<string,string>();
            data.Add("appId", _addressRequest.AppId);
            data.Add("scope", _addressRequest.Scope);
            data.Add("signType", _addressRequest.SignType);
            data.Add("addrSign", GetPaySign());
            data.Add("timeStamp", _addressRequest.TimeStamp);
            data.Add("nonceStr", _addressRequest.NonceStr);
            return data;
        }

        public string GetPostDataJson()
        {
            var data = GetPostData();
            StringBuilder stringBuilder = new StringBuilder();
            foreach (var key in data.Keys)
            {
                stringBuilder.Append(string.Format("{0}:{1},", key, data[key]));
            }

            var resultStr = stringBuilder.ToString().Remove(stringBuilder.Length - 1, 1);
            resultStr = "{" + resultStr + "}";
            return resultStr;
        }
    }
}
