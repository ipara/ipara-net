using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using IPara.DeveloperPortal.Core.Response;

namespace IPara.DeveloperPortal.Core.Request
{
    [XmlRoot("inquiry")]
    public class PaymentInquiryRequest:BaseRequest
    {
        /// <summary>
        ///  Ödeme sorugulama servisi için gerekli olan servis girdi parametrelerini temsil eder.
        /// </summary>
        [XmlElement("orderId")]
        public string orderId { get; set; }

        /// <summary>
        /// Bu servise sorgulanmak istenen ödemenin mağaza sipariş numarası ve mode değeri iletilerek, ödemenin durumu ve ödemenin tutarı öğrenilebileceği servisi temsil eder.
        /// </summary>
        /// <param name="request">Ödeme sorgulama servisi için gerekli olan girdilerin olduğu sınıfı temsil eder.</param>
        /// <param name="options">Kullanıcıya özel olarak belirlenen ayarları temsil eder.</param>
        /// <returns></returns>
        public static PaymentInquiryResponse Execute(PaymentInquiryRequest request, Settings options)
        {
            options.TransactionDate = Helper.GetTransactionDateString();
            options.HashString = options.PrivateKey + request.orderId +options.Mode + options.TransactionDate;
            return RestHttpCaller.Create().PostXML<PaymentInquiryResponse>(options.BaseUrl + "rest/payment/inquiry", Helper.GetHttpHeaders(options, Helper.application_xml), request);
        }
    }
}
