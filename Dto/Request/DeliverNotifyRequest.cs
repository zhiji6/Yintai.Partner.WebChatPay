using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Yintai.Partner.WebChatPay.Config;

namespace Yintai.Partner.WebChatPay.Dto.Request
{
    public class DeliverNotifyRequest
    {
        public DeliverNotifyRequest()
        {
            WxCommonConfig wxCommonConfig= new WxCommonConfig();
            this.AppId = wxCommonConfig.AppId;
        }

        ////PostData
        public string AppId { get; set; }
        public string OpenId { get; set; }
        public string TransId { get; set; }
        public string OutTradeNo { get; set; }
        public string DeliverTimestamp { get; set; }
        /// <summary>
        /// 是发货状态，  1表明成功， 0表明失败， 失败时需要在 deliver_msg 填上失败原因
        /// </summary>
        public string DeliverStatus { get; set; }
        /// <summary>
        /// 发货状态信息，失败时可以填上utf8编码的错误信息提醒，比如该商品已退款
        /// </summary>
        public string DeliverMsg { get; set; }
        public string AppSignature { get; set; } 
        public string SignMethod { get; set; }
        public string ApiUrl { get { return string.Format("https://api.weixin.qq.com/pay/delivernotify?access_token={0}", AccessToken); } }
        public string AccessToken { get; set; }

        public Dictionary<string, string> GetSignSource()
        {
            var source = new Dictionary<string, string>();
            source.Add("appid", this.AppId);
            source.Add("openid", this.OpenId);
            source.Add("transid", this.TransId);
            source.Add("out_trade_no", this.OutTradeNo);
            source.Add("deliver_timestamp", this.DeliverTimestamp);
            source.Add("deliver_status", this.DeliverStatus);
            source.Add("deliver_msg", this.DeliverMsg);
            return source;
        }

    }
}
