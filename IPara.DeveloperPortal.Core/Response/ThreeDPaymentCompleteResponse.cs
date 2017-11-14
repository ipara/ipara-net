using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace IPara.DeveloperPortal.Core.Response
{
    /// <summary>
    ///  3D Secure ile ödeme 2. adımı sonucunda oluşan servis çıktı parametrelerini temsil etmektedir.
    /// </summary>
    [XmlRoot("authResponse")]
    public class ThreeDPaymentCompleteResponse : BaseResponse
    {
        [XmlElement("amount")]
        public string Amount { get; set; }
        [XmlElement("orderId")]
        public string OrderId { get; set; }
    }
}
