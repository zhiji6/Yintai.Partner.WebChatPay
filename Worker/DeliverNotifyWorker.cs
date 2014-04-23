using System.Collections.Generic;
using System.Text;
using Yintai.Partner.WebChatPay.Config;
using Yintai.Partner.WebChatPay.Dto.Request;
using Yintai.Partner.WebChatPay.WxPay;

namespace Yintai.Partner.WebChatPay.Worker
{
    public class DeliverNotifyWorker
    {
        WxPayHelper wxPayHelper = new WxPayHelper();
        WxPayConfig wxPayConfig = new WxPayConfig();
        private DeliverNotifyRequest _deliverNotifyRequest = new DeliverNotifyRequest();

        public DeliverNotifyWorker(DeliverNotifyRequest deliverNotifyRequest)
        {
            wxPayHelper.SetAppId(wxPayConfig.AppId);////""
            wxPayHelper.SetAppKey(wxPayConfig.AppKey);////""
            wxPayHelper.SetPartnerKey(wxPayConfig.PartnerKey);////""
           
            this._deliverNotifyRequest = deliverNotifyRequest;
            this._deliverNotifyRequest.AppId = wxPayConfig.AppId;
            this._deliverNotifyRequest.SignMethod = "sha1";
        }

        public string GetPaySign()
        {
            var source = this._deliverNotifyRequest.GetSignSource();
            //// source.Add("appkey", wxPayConfig.AppKey);
            var computeSign = wxPayHelper.GetBizSign(source);
            return computeSign.ToUpper();
        }

        public Dictionary<string, string> GetPostData()
        {
            var data = _deliverNotifyRequest.GetSignSource();
            data.Add("app_signature", GetPaySign().ToLower());
            data.Add("sign_method", _deliverNotifyRequest.SignMethod);
            return data;
        }

        public string GetPostDataJson()
        {
            var data = GetPostData();
            StringBuilder stringBuilder = new StringBuilder();
            foreach (var key in data.Keys)
            {
                stringBuilder.Append(string.Format("\"{0}\":\"{1}\",", key, data[key]));
            }

            var resultStr = stringBuilder.ToString().Remove(stringBuilder.Length - 1, 1);
            resultStr = "{" + resultStr + "}";
            return resultStr;
        }
        
    }
}
