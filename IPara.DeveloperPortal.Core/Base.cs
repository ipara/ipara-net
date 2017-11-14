using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace IPara.DeveloperPortal.Core
{

    public abstract class  Base
    {
     
    }
    /// <summary>
    /// Tüm Request Sınıflarındaki Ortak Alanlar
    /// Tüm Request Sınıflarında zorunlu olarak kullanılacak alanları temsil eder.
    /// Ortak alanları tekrar tekrar kullanmak yerine bu sınıftan kalıtım alarak kullanım sağlanır.
    /// </summary>
    public class BaseRequest :Base
    {
        [XmlElement("echo")]
        public string Echo { get; set; }
        [XmlElement("mode")]
        public string Mode { get; set; }
    }

    /// <summary>
    /// Tüm Response Sınıflarındaki Ortak Alanlar 
    /// Tümn Response Sınıflarında zorunlu olarak kullanılacak alanları temsil eder.
    /// Ortak olan bu alanları tekrar tekrar kullanmak yerine bu sınıftan kalıtım alarak kullanım sağlanır.
    /// </summary>
    public class BaseResponse:Base
    {
        [XmlElement("result")]
        public string Result { get; set; }
        [XmlElement("errorCode")]
        public string ErrorCode { get; set; }
        [XmlElement("errorMessage")]
        public string ErrorMessage { get; set; }

        [XmlElement("responseMessage")]
        public string ResponseMessage { get; set; }

        //XML Servisler için Gerekli
        [XmlElement("mode")]
        public string Mode { get; set; }
        [XmlElement("echo")]
        public string Echo { get; set; }
        [XmlElement("hash")]
        public string Hash { get; set; }
        [XmlElement("transactionDate")]
        public string TransactionDate { get; set; }

    }
}
