using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Yintai.Partner.WebChatPay.Dto.Request
{
    public class UpdateFeedBackRequest
    {
        
        ////PostData
        public string OpenId { get; set; }
        public string FeedBackId { get; set; }

        public string ApiUrl
        {
            get
            {
                return
                    string.Format(
                        "https://api.weixin.qq.com/payfeedback/update?access_token={0}&openid={1}&feedbackid={2}",
                        AccessToken, OpenId, FeedBackId);
            }
        }

        public string AccessToken { get; set; }
    }
}
