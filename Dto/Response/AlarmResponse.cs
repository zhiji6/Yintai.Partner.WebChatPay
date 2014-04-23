using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Yintai.Partner.WebChatPay.Dto.Response
{
    [Serializable]
    [XmlRoot("xml")]
    public class AlarmResponse
    {
        [XmlElement("AppId")]
        public string AppId { get; set; }

        [XmlElement("ErrorType")]
        public string ErrorType { get; set; } ////ErrorType:1001  发货超时

        [XmlElement("Description")]
        public string Description { get; set; }

        [XmlElement("AlarmContent")]
        public string AlarmContent { get; set; }

        [XmlElement("TimeStamp")]
        public string TimeStamp { get; set; }

        [XmlElement("AppSignature")]
        public string AppSignature { get; set; }

        [XmlElement("SignMethod")]
        public string SignMethod { get; set; }

        public Dictionary<string, string> GetSignSource()
        {
            var source = new Dictionary<string, string>();
            source.Add("appid", this.AppId);
            source.Add("errortype", this.ErrorType);
            source.Add("description", this.Description);
            source.Add("alarmcontent", this.AlarmContent);
            source.Add("timestamp", this.TimeStamp);
            return source;
        }
    }
}
