using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Yintai.Partner.WebChatPay.Dto.Request
{
    public class AddressRequest
    {
        public string AppId { get; set; }
        public string Scope { get; set; }
        public string SignType { get; set; }
        public string AddrSign { get; set; }
        public string TimeStamp { get; set; }
        public string NonceStr { get; set; }
        public string Url { get; set; }
        public string AccessToken { get; set; }

        public Dictionary<string, string> GetSignSource()
        {
            var source = new Dictionary<string, string>();
            source.Add("appid", this.AppId);
            source.Add("url", this.Url);
            source.Add("timestamp", this.TimeStamp);
            source.Add("noncestr", this.NonceStr);
            source.Add("accessToken", this.AccessToken);
            return source;
        }
    }
}
