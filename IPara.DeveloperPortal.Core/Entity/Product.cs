using Newtonsoft.Json;
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
        [JsonProperty("productCode")]
        [XmlElement("productCode")]
        public string Code { get; set; }
        
        [JsonProperty("productName")]
        [XmlElement("productName")]
        public string Title { get; set; }

        [JsonProperty("quantity")]
        [XmlElement("quantity")]
        public int Quantity { get; set; }

        [JsonProperty("price")]
        [XmlElement("price")]
        public string Price { get; set; }
    }
}