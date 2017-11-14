using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace IPara.DeveloperPortal.Core.Response
{
    /// <summary>
    /// 3D secure 1. Adımı sonucunda oluşan servis çıktı parametrelerini temsil etmektedir.
    /// </summary>
    [XmlRoot("authResponse")]
    public class ThreeDPaymentInitResponse : BaseResponse
    {
        [XmlElement("amount")]
        public string Amount { get; set; }
        [XmlElement("orderId")]
        public string OrderId { get; set; }
    }
}
