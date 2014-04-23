using Yintai.Partner.WebChatPay.Config;
using Yintai.Partner.WebChatPay.Dto.Response;
using Yintai.Partner.WebChatPay.WxPay;

namespace Yintai.Partner.WebChatPay.Worker
{
    public class AlarmWorker
    {
        WxPayHelper wxPayHelper = new WxPayHelper();
        WxPayConfig wxPayConfig = new WxPayConfig();
        private AlarmResponse _alarmResponse = new AlarmResponse();

        public AlarmWorker(AlarmResponse alarmResponse)
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
            
            this._alarmResponse = alarmResponse;
        }

        public bool IsPaySignPassed(string notifyPaySign)
        {
            var computeSign = wxPayHelper.GetBizSign(this._alarmResponse.GetSignSource());
            return computeSign.ToUpper().Equals(notifyPaySign.ToUpper());
        }

        
    }
}
