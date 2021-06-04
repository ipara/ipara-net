using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Xml.Serialization;
using IPara.DeveloperPortal.Core.Entity;
using IPara.DeveloperPortal.Core.Response;
using Newtonsoft.Json;

namespace IPara.DeveloperPortal.Core.Request
{
    /// <summary>
    /// Linkle Ödeme -> Link Gönderimi Servisi içerisinde kullanılacak alanları temsil eder.
    /// </summary>
    public class LinkPaymentCreateRequest : BaseRequest
    {
        public string name { get; set; }
        public string surname { get; set; }
        public string tcCertificate { get; set; }
        public string taxNumber { get; set; }
        public string email { get; set; }
        public string gsm { get; set; }
        public int amount { get; set; }
        public string threeD { get; set; }
        public string expireDate { get; set; }
        public int[] installmentList { get; set; }
        public string sendEmail { get; set; }
        public string commissionType { get; set; }
        public string clientIp { get; set; }

        public static LinkPaymentCreateResponse Execute(LinkPaymentCreateRequest request, Settings options)
        {
            options.TransactionDate = Helper.GetTransactionDateString();
            options.HashString = options.PrivateKey + request.name + request.surname + request.email + request.amount + request.clientIp + options.TransactionDate;
            LinkPaymentCreateResponse response = RestHttpCaller.Create().PostJson<LinkPaymentCreateResponse>(options.BaseUrl + "corporate/merchant/linkpayment/create", Helper.GetHttpHeaders(options, Helper.application_json), request);
            return response;
        }
    }

}

