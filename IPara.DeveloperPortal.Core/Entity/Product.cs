using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace IPara.DeveloperPortal.Core.Entity
{
    /// <summary>
    /// Bu sınıf 3D secure olmadan ödeme kısmında ürün bilgisinin kullanılacağı yerde ve
    /// 3D Secure ile Ödeme'nin 2. adımında ürün bilgisinin istendiği yerde kullanılır.
    /// </summary>
    public class Product
    {   
        [XmlElement("productCode")]
        public string Code { get; set; }

        [XmlElement("productName")]
        public string Title { get; set; }

        [XmlElement("quantity")]
        public int Quantity { get; set; }

        [XmlElement("price")]
        public string Price { get; set; }
    }
}