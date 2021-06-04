using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace IPara.DeveloperPortal.Core.Entity
{
    /// <summary>
    /// Linkle Ödeme (Link Sorgulama) servisi sonucu dönen PaymentLink bilgisini tutar.
    /// </summary>
    public class PaymentLink
    {
        public string amount { set; get; }
        public string creationDate { set; get; }
        public string description { set; get; }
        public string email { set; get; }
        public string gsm { set; get; }
        public string linkId { set; get; }
        public string linkState { set; get; }
        public string name { set; get; }
        public string surname { set; get; }
        public string taxNumber { set; get; }
        public string tcCertificate { set; get; }
    }
}