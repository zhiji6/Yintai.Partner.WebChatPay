using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Yintai.Partner.WebChatPay.Config;

namespace Yintai.Partner.WebChatPay.Dto.Request
{
    public class OrderQueryRequest
    {
        public OrderQueryRequest()
        {
            Package = new OrderQueryPackage();
        }

        ////PostData
        public string AppId { get; set; }
        public OrderQueryPackage Package { get; set; }
        public string Timestamp { get; set; }
        public string AppSignature { get; set; } 
        public string SignMethod { get; set; }
        public string ApiUrl { get { return string.Format("https://api.weixin.qq.com/pay/orderquery?access_token={0}", AccessToken); } }
        public string AccessToken { get; set; }

        public Dictionary<string, string> GetSignSource()
        {
            var source = new Dictionary<string, string>();
            source.Add("appid", this.AppId);
            source.Add("package", this.Package.GetPackageStr());
            source.Add("timestamp", this.Timestamp);
            //source.Add("app_signature", this.AppSignature);
            //source.Add("sign_method", this.SignMethod);

            return source;
        }
    }

    public class OrderQueryPackage
    {
        public string OutTradeNo { get; set; }
        public string Partner { get; set; }
        public string Sign { get; set; }

        public string GetPackageStr()
        {
            return string.Format("out_trade_no={0}&partner={1}&sign={2}", OutTradeNo, Partner, Sign);
        }

        public Dictionary<string, string> GetSignSource()
        {
            var source = new Dictionary<string, string>();
            source.Add("out_trade_no", this.OutTradeNo);
            source.Add("partner", this.Partner);
            return source;
        }
    }
}
