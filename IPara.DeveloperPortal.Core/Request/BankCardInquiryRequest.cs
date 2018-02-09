using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IPara.DeveloperPortal.Core.Entity;
using IPara.DeveloperPortal.Core.Response;

namespace IPara.DeveloperPortal.Core.Request
{
    /// <summary>
    /// Cüzdanda bulunan kartları getirmek için gerekli olan servis girdi parametrelerini temsil eder.
    /// </summary>
    public class BankCardInquiryRequest: BaseRequest
    {
        public string userId { get; set; } 
        public string clientIp { get; set; }
        public string cardId { get; set; }

        /// <summary>
        /// Mağazanın, cüzdanda bulunan kartları getirmek için kullandığı servisi temsil eder.
        /// </summary>
        /// <param name="request">Cüzdanda bulunan kartları getirmek için gerekli olan girdilerin olduğu sınıfı temsil eder.</param>
        /// <param name="options">Kullanıcıya özel olarak belirlenen ayarları temsil eder.</param>
        /// <returns></returns>
        public static BankCardInquryResponse Execute(BankCardInquiryRequest request, Settings options)
        {
            options.TransactionDate = Helper.GetTransactionDateString();
            options.HashString = options.PrivateKey + request.userId + request.cardId + request.clientIp + options.TransactionDate;
            return RestHttpCaller.Create().PostJson<BankCardInquryResponse>(options.BaseUrl + "/bankcard/inquiry", Helper.GetHttpHeaders(options, Helper.application_json), request);
        }


    }
}
