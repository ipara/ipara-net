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
    /// Linkle Ödeme -> Link Sorgulama/Listeleme Servisi içerisinde kullanılacak alanları temsil eder.
    /// </summary>

    public class LinkPaymentListRequest : BaseRequest
    {
        public string email { get; set; }
        public string gsm { get; set; }
        public string linkState { get; set; }
        public string startDate { get; set; }
        public string endDate { get; set; }
        public string pageSize { get; set; }
        public string pageIndex { get; set; }
        public string clientIp { get; set; }
        public static LinkPaymentListResponse Execute(LinkPaymentListRequest request, Settings options)
        {
            options.TransactionDate = Helper.GetTransactionDateString();
            options.HashString = options.PrivateKey + request.clientIp + options.TransactionDate;
            LinkPaymentListResponse response = RestHttpCaller.Create().PostJson<LinkPaymentListResponse>(options.BaseUrl + "corporate/merchant/linkpayment/list", Helper.GetHttpHeaders(options, Helper.application_json), request);
            return response;
        }
    }
}

