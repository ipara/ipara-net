using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using IPara.DeveloperPortal.Core.Entity;
using IPara.DeveloperPortal.Core.Response;

namespace IPara.DeveloperPortal.Core.Request
{
    /// <summary>
    /// 3D Secure Olmadan Ödeme için gerekli olan servis girdi parametrelerini temsil eder.
    /// </summary>
    [XmlRoot("auth")]
    public class ApiPaymentRequest : BaseRequest
    {

        [XmlElement("threeD")]
        public string ThreeD { get; set; }

        [XmlElement("orderId")]
        public string OrderId { get; set; }

        [XmlElement("amount")]
        public string Amount { get; set; }


        [XmlElement("cardOwnerName")]
        public string CardOwnerName { get; set; }

        [XmlElement("cardNumber")]
        public string CardNumber { get; set; }

        [XmlElement("cardExpireMonth")]
        public string CardExpireMonth { get; set; }

        [XmlElement("cardExpireYear")]
        public string CardExpireYear { get; set; }

        [XmlElement("installment")]
        public string Installment { get; set; }

        [XmlElement("cardCvc")]
        public string Cvc { get; set; }


        [XmlElement("vendorId")]
        public string VendorId { get; set; }
        [XmlElement("userId")]
        public string UserId { get; set; }
        [XmlElement("cardId")]
        public string CardId { get; set; }

        [XmlElement("threeDSecureCode")]
        public string ThreeDSecureCode { get; set; }

        [XmlArray("products"), XmlArrayItem(typeof(Product), ElementName = "product", IsNullable = false)]
        public List<Product> Products { get; set; }

        [XmlElement("purchaser")]
        public Purchaser Purchaser { get; set; }
        /// <summary>
        /// 3D Secure Olmadan Ödeme Servis çağrısını temsil eder.
        /// </summary>
        /// <param name="request">3D Secure olmadan gerekli olan servis girdi parametrelerini temsil eder.</param>
        /// <param name="options">Kullanıcıya özel olarak belirlenen ayarları temsil eder.</param>
        /// <returns></returns>
        public static ApiPaymentResponse Execute(ApiPaymentRequest request, Settings options)
        {
            options.TransactionDate = Helper.GetTransactionDateString();
            options.HashString = options.PrivateKey + request.OrderId + request.Amount + request.Mode + request.CardOwnerName + request.CardNumber + request.CardExpireMonth + request.CardExpireYear + request.Cvc + request.UserId + request.CardId+ request.Purchaser.Name + request.Purchaser.SurName + request.Purchaser.Email + options.TransactionDate;
            return RestHttpCaller.Create().PostXML<ApiPaymentResponse>(options.BaseUrl + "rest/payment/auth", Helper.GetHttpHeaders(options, Helper.application_xml), request);
        }
    }




}

