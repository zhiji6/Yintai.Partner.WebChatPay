using System.Collections.Generic;
using Yintai.Partner.WebChatPay.Config;
using Yintai.Partner.WebChatPay.Dto.Response;
using Yintai.Partner.WebChatPay.WxPay;

namespace Yintai.Partner.WebChatPay.Worker
{
    public class WxPayNotifyWorker
    {
        WxPayHelper wxPayHelper = new WxPayHelper();
        WxPayConfig wxPayConfig = new WxPayConfig();
        private PostData postData = new PostData();
        private Dictionary<string, string> webParams = new Dictionary<string, string>();

        public WxPayNotifyWorker(Dictionary<string, string> webParams, PostData postData)
        {
            wxPayHelper.SetAppId(wxPayConfig.AppId);////""
            wxPayHelper.SetAppKey(wxPayConfig.AppKey);////""
            wxPayHelper.SetPartnerKey(wxPayConfig.PartnerKey);////""
            ////wxPayHelper.SetSignType(wxPayConfig.SignType);////"sha1"

            ////wxPayHelper.SetParameter("bank_type", wxPayConfig.BankType);
            ////wxPayHelper.SetParameter("partner", wxPayConfig.Partner);
            ////wxPayHelper.SetParameter("fee_type", wxPayConfig.FeeType);
            ////wxPayHelper.SetParameter("notify_url", wxPayConfig.NotifyUrl);
            ////wxPayHelper.SetParameter("input_charset", wxPayConfig.InputCharset);
            foreach (var webParam in webParams)
            {
                wxPayHelper.SetParameter(webParam.Key, webParam.Value);
            }

            this.webParams = webParams;
            this.postData = postData;
        }

        public bool IsPaySignPassed(string notifyPaySign)
        {
            var computeSign = wxPayHelper.GetBizSign(this.postData.GetSignSource());
            return computeSign.ToUpper().Equals(notifyPaySign.ToUpper());
        }

        public bool IsSignPassed(string notifySign)
        {
            ////var notifySign = this.webParams["sign"];
            this.webParams.Remove("sign");
            string unSignParaString = CommonUtil.FormatBizQueryParaMap(this.webParams,
                    false);
            var computeSign = MD5SignUtil.Sign(unSignParaString, wxPayConfig.PartnerKey);
            return computeSign.ToUpper().Equals(notifySign.ToUpper());
        }

        public bool IsAllSignPassed(string notifyPaySign, string notifySign)
        {
            return IsPaySignPassed(notifyPaySign) && IsSignPassed(notifySign);
        }
    }
}
