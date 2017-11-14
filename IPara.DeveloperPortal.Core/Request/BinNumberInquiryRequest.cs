using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IPara.DeveloperPortal.Core.Entity;
using IPara.DeveloperPortal.Core.Response;

namespace IPara.DeveloperPortal.Core.Request
{
    public class BinNumberInquiryRequest :BaseRequest
    {
        /// <summary>
        /// Bin Sorgulama servisleri içerisinde kullanılacak olan bin numarasını temsil eder.
        /// </summary>
        public string binNumber { get; set; }

        /// <summary>
        /// Türkiye genelinde tanımlı olan tüm yerli kartlara ait BIN numaraları için sorgulama yapılmasına izin veren servisi temsil eder. 
        /// </summary>
        /// <param name="request">Istek olarak gelen bin numarasını temsil etmektedir.</param>
        /// <param name="options">Kullanıcıya özel olarak belirlenen ayarları temsil eder.</param>
        /// <returns></returns>
        public static BinNumberInquiryResponse Execute(BinNumberInquiryRequest request, Settings options)
        {
            options.transactionDate = Helper.GetTransactionDateString();
            options.HashString = options.PrivateKey + request.binNumber + options.transactionDate;
            return RestHttpCaller.Create().PostJson<BinNumberInquiryResponse>(options.BaseUrl + "rest/payment/bin/lookup", Helper.GetHttpHeaders( options, Helper.application_json), request);
        }
    }

}
