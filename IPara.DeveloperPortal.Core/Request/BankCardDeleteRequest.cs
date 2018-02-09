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
    /// Cüzdanda kayıtlı olan kartı silmek için gerekli olan servis girdi parametrelerini temsil eder.
    /// </summary>
    public class BankCardDeleteRequest : BaseRequest
    {
        public string userId { get; set; }
        public string cardId { get; set; }
        public string clientIp { get; set; }


        /// <summary>
        /// Mağazanın, kullanıcının bir kartını veya kayıtlı olan tüm kartlarını silmek istediği zaman kullanabileceği servisi temsil eder.
        /// </summary>
        /// <param name="request">Banka kartı silmek için gerekli olan girdilerin olduğu sınıfı temsil eder. </param>
        /// <param name="options">Kullanıcıya özel olarak belirlenen ayarları temsil eder.</param>
        /// <returns></returns>
        public static BankCardDeleteResponse Execute(BankCardDeleteRequest request, Settings options)
        {
            options.TransactionDate = Helper.GetTransactionDateString();
            options.HashString = options.PrivateKey + request.userId + request.cardId + request.clientIp + options.TransactionDate;
            return RestHttpCaller.Create()
                .PostJson<BankCardDeleteResponse>(options.BaseUrl + "/bankcard/delete", Helper.GetHttpHeaders(options,Helper.application_json),
                    request);
        }
    }
}
