using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Yintai.Partner.WebChatPay.Dto.Response
{
    public class AddressResponse : ResponseBase
    {
        public string userName { get; set; }
        public string telNumber { get; set; }
        public string addressPostalCode { get; set; }
        public string proviceFirstStageName { get; set; }
        public string addressCitySecondStageName { get; set; }
        public string addressCountiesThirdStageName { get; set; }
        public string addressDetailInfo { get; set; }
        public string nationalCode { get; set; }
    }
}
