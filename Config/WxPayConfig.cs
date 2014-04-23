using System;

namespace Yintai.Partner.WebChatPay.Config
{
    [Serializable]
    public class WxCommonConfig
    {
        /// <summary>
        /// 支付编码，对应银泰www的
        /// </summary>
        public static string PayCode
        {
            get { return ""; }
        }

        /// <summary>
        /// 支付编码，对应银泰www的
        /// </summary>
        public static string PaymentName
        {
            get { return "微信支付"; }
        }

        /// <summary>
        /// 公众号 id
        /// </summary>
        public string AppId
        {
            get { return ""; } 
            }

        /// <summary>
        /// 公众号key 也即接口文档中的paySignKey
        /// </summary>
        public string AppKey
        {
            get
            {
                return ""; }
            }

        /// <summary>
        /// AppSecret 用来换取access_token
        /// </summary>
        public string AppSecret
        {
            get { return ""; } 
        }

        /// <summary>
        /// 签名方式，目前仅支持 SHA1
        /// </summary>
        public string SignType
        {
            get { return "SHA1"; }
        }

        /// <summary>
        /// 最低微信版本要求
        /// </summary>
        public string MiniTargetVersion
        {
            get { return "5.0"; }
        }
    }

    public class WxPayConfig : WxCommonConfig
    {
        /// <summary>
        /// 银行通道类型，微信固定为 "WX"
        /// </summary>
        public string BankType
        {
            get { return "WX"; }
        }

        /// <summary>
        /// 商户号
        /// </summary>
        public string Partner
        {
            get { return ""; } 
            }


        /// <summary>
        /// 商户key
        /// </summary>
        public string PartnerKey
        {
            get { return ""; } 
            }

        /// <summary>
        /// 支付币种 取值： 1（人民币），暂只支持 1；
        /// </summary>
        public string FeeType
        {
            get { return "1"; }
        }

        /// <summary>
        /// 通知 URL
        /// </summary>
        public string NotifyUrl
        {
            get { return "http://www.xx.com"; }
        }

        /// <summary>
        /// 传入参数字符编码 ，默认为GBK，input_charset，取值范围： "GBK"、"UTF-8" ，默认： "GBK"
        /// </summary>
        public string InputCharset
        {
            get { return "UTF-8"; }
        }

    }
}
