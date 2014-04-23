using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Yintai.Partner.WebChatPay.Dto.Request
{
    public class PayRequest
    {
        public string appId { get; set; }
        public string timeStamp { get; set; }
        public string nonceStr { get; set; }
        public PayPackage package { get; set; }
        public string signType { get; set; }
        public string paySign { get; set; }
    }

    public class PayPackage
    {
        public string bank_type { get; set; }
        public string body { get; set; }
        public string attach { get; set; }
        public string partner { get; set; }
        public string out_trade_no { get; set; }
        public string total_fee { get; set; }
        ////public string fee_type { get; set; }
        ////public string notity_url { get; set; }
        public string spbill_create_ip { get; set; }
        public string time_start { get; set; }
        public string time_expire { get; set; }
        public string transport_fee { get; set; }
        public string product_fee { get; set; }
        public string goods_tag { get; set; }
        ////public string input_charset { get; set; }
    }
}
