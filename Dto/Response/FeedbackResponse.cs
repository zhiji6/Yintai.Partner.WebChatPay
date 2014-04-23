using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Yintai.Partner.WebChatPay.Dto.Response
{
    [Serializable]
    [XmlRoot("xml")]
    public class FeedbackResponse
    {
        [XmlElement("AppId")]
        public string AppId { get; set; }

        [XmlElement("OpenId")]
        public string OpenId { get; set; }

        [XmlElement("TimeStamp")]
        public string TimeStamp { get; set; }

        [XmlElement("MsgType")]
        public string MsgType { get; set; }

        [XmlElement("FeedBackId")]
        public string FeedBackId { get; set; }

        [XmlElement("TransId")]
        public string TransId { get; set; }

        [XmlElement("Reason")]
        public string Reason { get; set; }

        [XmlElement("Solution")]
        public string Solution { get; set; }

        [XmlElement("ExtInfo")]
        public string ExtInfo { get; set; }

        [XmlElement("AppSignature")]
        public string AppSignature { get; set; }

        [XmlElement("SignMethod")]
        public string SignMethod { get; set; }

        [XmlElement("PicInfo")]
        public PicInfo PicInfo { get; set; }

        public Dictionary<string, string> GetSignSource()
        {
            var source = new Dictionary<string, string>();
            source.Add("appid", this.AppId);
            source.Add("timestamp", this.TimeStamp);
            source.Add("openid", this.OpenId);
            
            return source;
        }
    }

    [Serializable]
    public class PicInfo
    {
        [XmlArray("PicInfo")]
        [XmlArrayItem(ElementName = "PicItem")]
        public List<PicItem> Items { get; set; }

        public override string ToString()
        {
            var str = string.Empty;
            foreach (var picItem in Items)
            {
                str += picItem.PicUrl+";";
            }

            return str;
        }
    }

    public class PicItem
    {
        [XmlElement("PicUrl")]
        public string PicUrl { get; set; }
    }
}
