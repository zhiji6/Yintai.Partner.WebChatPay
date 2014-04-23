using System.Collections.Generic;
using System.Text;
using Yintai.Partner.WebChatPay.Config;
using Yintai.Partner.WebChatPay.Dto.Request;
using Yintai.Partner.WebChatPay.WxPay;

namespace Yintai.Partner.WebChatPay.Worker
{
    public class OrderQueryWorker
    {
        WxPayHelper wxPayHelper = new WxPayHelper();
        WxPayConfig wxPayConfig = new WxPayConfig();
        private OrderQueryRequest _orderQueryRequest = new OrderQueryRequest();

        public OrderQueryWorker(OrderQueryRequest orderQueryRequest)
        {
            wxPayHelper.SetAppId(wxPayConfig.AppId);////""
            wxPayHelper.SetAppKey(wxPayConfig.AppKey);////""
            wxPayHelper.SetPartnerKey(wxPayConfig.PartnerKey);////""
           
            this._orderQueryRequest = orderQueryRequest;
            this._orderQueryRequest.AppId = wxPayConfig.AppId;
            this._orderQueryRequest.Package.Partner = wxPayConfig.Partner;
            this._orderQueryRequest.SignMethod = "sha1";
            SetPackageSign();
        }

        private void SetPackageSign()
        {
            var  source = _orderQueryRequest.Package.GetSignSource();
            source.Add("key", wxPayConfig.PartnerKey);
            string unSignParaString = CommonUtil.FormatBizQueryParaMap(source,
                    false);
            var computeSign = MD5SignUtil.Sign(unSignParaString, wxPayConfig.PartnerKey);
            _orderQueryRequest.Package.Sign = computeSign.ToUpper();
        }

        public string GetPaySign()
        {
            var source = this._orderQueryRequest.GetSignSource();
            ////source.Add("appkey", wxPayConfig.AppKey);
            var computeSign = wxPayHelper.GetBizSign(source);
            return computeSign.ToUpper();
        }

        public Dictionary<string, string> GetPostData()
        {
            var data = _orderQueryRequest.GetSignSource();
            data.Add("app_signature", GetPaySign());
            data.Add("sign_method", _orderQueryRequest.SignMethod);
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
