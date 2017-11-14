using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace IPara.DeveloperPortal.Core.Entity
{
    /// <summary>
    /// Bu sınıf 3D Secure ile Ödeme işlemlerinin 1. ve 2. adımında kullanılan parametreleri temsil eder.
    /// </summary>
    [Serializable]
    [XmlRoot(ElementName = "auth")]
    public class IparaAuth
    {
        [XmlElement("threeD")]
        public string ThreeD { get; set; }

        [XmlElement("orderId")]
        public string OrderId { get; set; }

        [XmlElement("amount")]
        public string Amount { get; set; }
      
        [XmlElement("echo")]
        public string Echo { get; set; }

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

        [XmlElement("mode")]
        public string Mode { get; set; }

        [XmlElement("vendorId")]
        public string VendorId { get; set; }

        [XmlElement("threeDSecureCode")]
        public string ThreeDSecureCode { get; set; }

        [XmlArray("products"), XmlArrayItem(typeof(Product), ElementName = "product", IsNullable = false)]
        public List<Product> Products { get; set; }

        [XmlElement("purchaser")]
        public Purchaser Purchaser { get; set; }

    }
}