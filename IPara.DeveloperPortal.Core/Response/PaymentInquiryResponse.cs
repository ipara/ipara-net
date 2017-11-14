using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace IPara.DeveloperPortal.Core.Response
{
    /// <summary>
    ///  Ödeme sorugulama servisi sonucunda oluşan servis çıktı parametrelerini temsil eder.
    /// </summary>
    [XmlRoot("inquiryResponse")]
    public class PaymentInquiryResponse:BaseResponse
    {
        [XmlElement("amount")]
        public string Amount { get; set; }
        [XmlElement("orderId")]
        public string OrderId { get; set; }
  



    }
}
