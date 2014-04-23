using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using Yintai.Partner.WebChatPay.Config;

namespace Yintai.Partner.WebChatPay.Dto.Response
{
    public class PayNotifyResponse
    {
        public PayNotifyResponse()
        {
            
        }

        public PayNotifyResponse(NameValueCollection webParamsSource)
        {
            this.sign_type = webParamsSource.Get("sign_type") ?? "";

            this.service_version = webParamsSource.Get("service_version") ?? "";

            this.input_charset = webParamsSource.Get("input_charset") ?? "";

            this.sign = webParamsSource.Get("sign") ?? "";

            int sign_key_index = 1;
            int.TryParse(webParamsSource.Get("sign_key_index"), out sign_key_index);
            this.sign_key_index = sign_key_index;

            var trade_mode = 1;
            int.TryParse(webParamsSource.Get("trade_mode"), out trade_mode);
            this.trade_mode = trade_mode;

            var trade_state = -1;
            int.TryParse(webParamsSource.Get("trade_state"), out trade_state);
            this.trade_state = trade_state;

            this.pay_info = webParamsSource.Get("pay_info") ?? "";

            this.partner = webParamsSource.Get("partner") ?? "";

            this.bank_type = webParamsSource.Get("bank_type") ?? "";

            this.bank_billno = webParamsSource.Get("bank_billno") ?? "";

            var total_fee = 0; //0分，分为单位钱数
            int.TryParse(webParamsSource.Get("total_fee"), out total_fee);
            this.total_fee = total_fee;

            var fee_type = 1;
            int.TryParse(webParamsSource.Get("fee_type"), out fee_type);
            this.fee_type = fee_type;

            this.notify_id = webParamsSource.Get("notify_id") ?? "";

            this.transaction_id = webParamsSource.Get("transaction_id") ?? "";

            this.out_trade_no = webParamsSource.Get("out_trade_no") ?? "";

            this.attach = webParamsSource.Get("attach") ?? "";

            this.time_end = webParamsSource.Get("time_end") ?? "";

            var transport_fee = 0;
            int.TryParse(webParamsSource.Get("transport_fee"), out transport_fee);
            this.transport_fee = transport_fee;

            var product_fee = 0;
            int.TryParse(webParamsSource.Get("product_fee"), out product_fee);
            this.product_fee = product_fee;

            var discount = 0;
            int.TryParse(webParamsSource.Get("discount"), out discount);
            this.discount = discount;

            this.buyer_alias = webParamsSource.Get("buyer_alias") ?? "";
        }

        public string sign_type { get; set; }
        public string service_version { get; set; }
        public string input_charset { get; set; }
        public string sign { get; set; }
        public int sign_key_index { get; set; }

        public int trade_mode { get; set; }
        public int trade_state { get; set; }
        public string pay_info { get; set; }
        public string partner { get; set; }
        public string bank_type { get; set; }

        public string bank_billno { get; set; }
        public int total_fee { get; set; }
        public int fee_type { get; set; }
        public string notify_id { get; set; }
        public string transaction_id { get; set; }
        public string out_trade_no { get; set; }
        public string attach { get; set; }
        public string time_end { get; set; }
        public int transport_fee { get; set; }
        public int product_fee { get; set; }
        public int discount { get; set; }
        public string buyer_alias { get; set; }

        public override string ToString()
        {
            return
                string.Format(
                    @"sign_type={0}&service_version={1}&input_charset={2}&sign={3}&sign_key_index={4}&trade_mode={5}&trade_state={6}&pay_info={7}
                     &partner={8}&bank_type={9}&bank_billno={10}&total_fee={11}&fee_type={12}&notify_id={13}&transaction_id={14}&out_trade_no={15}
                     &attach={16}&time_end={17}&transport_fee={18}&product_fee={19}&discount={20}&buyer_alias={21}",
                    sign_type, service_version, input_charset, sign, sign_key_index, trade_mode, trade_state, pay_info,
                    partner, bank_type, bank_billno, total_fee, fee_type, notify_id, transaction_id, out_trade_no,
                    attach, time_end, transport_fee, product_fee, discount, buyer_alias);
        }
    }

    [Serializable]
    [XmlRoot("xml")]
    public class PostData : WxCommonConfig
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        [XmlElement("OpenId")]
        public string OpenId { get; set; }
        [XmlElement("AppId")]
        public string AppId { get; set; }
        [XmlElement("IsSubscribe")]
        public int IsSubscribe { get; set; }
        [XmlElement("TimeStamp")]
        public int TimeStamp { get; set; }
        [XmlElement("NonceStr")]
        public string NonceStr { get; set; }

        [XmlElement("AppSignature")]
        public string AppSignature { get; set; }
        [XmlElement("SignMethod")]
        public string SignMethod { get; set; }

        public Dictionary<string, string> GetSignSource()
        {
            var source = new Dictionary<string, string>();
            source.Add("appid",this.AppId);
            //source.Add("appkey", this.AppKey);
            source.Add("timestamp", this.TimeStamp.ToString());
            source.Add("noncestr",this.NonceStr);
            source.Add("openid",this.OpenId);
            source.Add("issubscribe",this.IsSubscribe.ToString());
            return source;
        }

        public override string ToString()
        {
            return
                string.Format(
                    "OpenId={0}&AppId={1}&IsSubscribe={2}&TimeStamp={3}&NonceStr={4}&AppSignature={5}&SignMethod={6}",
                    OpenId, AppId, IsSubscribe, TimeStamp, NonceStr, AppSignature, SignMethod);
        }
    }
}
