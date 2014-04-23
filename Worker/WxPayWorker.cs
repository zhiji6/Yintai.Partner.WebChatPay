using Yintai.Partner.WebChatPay.Common;
using Yintai.Partner.WebChatPay.Config;
using Yintai.Partner.WebChatPay.WxPay;

namespace Yintai.Partner.WebChatPay.Worker
{
    public class WxPayWorker
    {
        WxPayHelper wxPayHelper = new WxPayHelper();

        WxPayConfig wxPayConfig = new WxPayConfig();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderNum"></param>
        /// <param name="totalFee"></param>
        /// <param name="body"></param>
        /// <param name="ip"></param>
        /// <param name="attach">附加数据，原样返回</param>
        public WxPayWorker(string orderNum, decimal totalFee, string body, string ip,string attach)
        {
            wxPayHelper.SetAppId(wxPayConfig.AppId);////""
            wxPayHelper.SetAppKey(wxPayConfig.AppKey);////""
            wxPayHelper.SetPartnerKey(wxPayConfig.PartnerKey);////""
            wxPayHelper.SetSignType(wxPayConfig.SignType);////"sha1"

            wxPayHelper.SetParameter("bank_type", wxPayConfig.BankType);////"WX"
            ////wxPayHelper.SetParameter("body", "test");
            wxPayHelper.SetParameter("body", body);
            if (!string.IsNullOrEmpty(attach))
            {
                wxPayHelper.SetParameter("attach", attach);
            }

            wxPayHelper.SetParameter("partner", wxPayConfig.Partner);////""
            ////wxPayHelper.SetParameter("out_trade_no", CommonUtil.CreateNoncestr());
            wxPayHelper.SetParameter("out_trade_no", orderNum);
            ////wxPayHelper.SetParameter("total_fee", "1");
            wxPayHelper.SetParameter("total_fee", WebUtil.Feng4Decimal(totalFee).ToString());
            wxPayHelper.SetParameter("fee_type", wxPayConfig.FeeType);////"1"
            wxPayHelper.SetParameter("notify_url", wxPayConfig.NotifyUrl);////"htttp://www.baidu.com"
            ////wxPayHelper.SetParameter("spbill_create_ip", "127.0.0.1");
            wxPayHelper.SetParameter("spbill_create_ip", ip);
            wxPayHelper.SetParameter("input_charset", "UTF-8"); //wxPayConfig.InputCharset);////"GBK"
        }

        /// <summary>
        /// 生成jsapi支付Json数据
        /// </summary>
        /// <returns></returns>
        public string GetJsonDataForJsApi()
        {
            return wxPayHelper.CreateBizPackage();
        }

        /*
         WeixinJsBridge.invoke('getBrandWCPayRequest',{
           "appId":"wxf000000000000000",
           "timeStamp":"189026618",
           "nonceStr":"adssdasssd13d",
           "Package":"",
           "signType":"SHA1",
           "paySign":""
         },function(res){
           //返回res.err_msg,取值
           //get_brand_wcpay_request:cancel  用户取消
           //get_brand_wcpay_request:fail  发送失败
           //get_brand_wcpay_request:ok  发送成功
         * })
         */

        /// <summary>
        /// 生成app支付package
        /// </summary>
        /// <param name="traceid"></param>
        /// <returns></returns>
        public string CreateAppPackage(string traceid)
        {
            return wxPayHelper.CreateAppPackage(traceid);
        }

        /// <summary>
        /// 生成原生支付url
        /// </summary>
        /// <param name="productid"></param>
        /// <returns></returns>
        public string CreateNativeUrl(string productid)
        {
            return wxPayHelper.CreateNativeUrl("abc");
        }

        /// <summary>
        /// 生成原生支付package
        /// </summary>
        /// <param name="retcode"></param>
        /// <param name="reterrmsg"></param>
        /// <returns></returns>
        public string CreateNativePackage(string retcode, string reterrmsg)
        {
            return wxPayHelper.CreateNativePackage("0", "ok");
        }
        
    }
}
