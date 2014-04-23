using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Yintai.Partner.WebChatPay.Config;
using Yintai.Partner.WebChatPay.Dto.Response;
using Yintai.Partner.WebChatPay.WxPay;

namespace Yintai.Partner.WebChatPay.Worker
{
    public class FeedBackWorker
    {
        WxPayHelper wxPayHelper = new WxPayHelper();
        WxPayConfig wxPayConfig = new WxPayConfig();
        private FeedbackResponse _feedbackResponse = new FeedbackResponse();

        public FeedBackWorker(FeedbackResponse feedbackResponse)
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
            
            this._feedbackResponse = feedbackResponse;
        }

        public bool IsPaySignPassed(string notifyPaySign)
        {
            var computeSign = wxPayHelper.GetBizSign(this._feedbackResponse.GetSignSource());
            return computeSign.ToUpper().Equals(notifyPaySign.ToUpper());
        }
    }
}
