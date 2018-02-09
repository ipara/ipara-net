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
    /// Cüzdana kart ekleme servisi içerisinde kullanılacak alanları temsil etmektedir.
    /// </summary>
    public class BankCardCreateRequest : BaseRequest
    {
        public string userId { get; set; }
        public string cardOwnerName { get; set; }
        public string cardNumber { get; set; }

        public string cardAlias { get; set; }

        public string cardExpireMonth { get; set; }

        public string cardExpireYear { get; set; }

        public string clientIp { get; set; }

      /// <summary>
      /// Cüzdana kart ekleme istek metodur. Bu metod çeşitli kart bilgilerini ve settings sınıfı içerisinde bize özel olarak oluşan alanları kullanarak
      /// cüzdana bir kartı kaydetmemizi sağlar.
      /// </summary>
      /// <param name="request">Cüzdana kart eklemek için gerekli olan girdilerin olduğu sınıfı temsil eder.</param>
      /// <param name="options">Kullanıcıya özel olarak belirlenen ayarları temsil eder.</param>
      /// <returns></returns>
        public static BankCardCreateResponse Execute(BankCardCreateRequest request, Settings options)
        {
            options.TransactionDate = Helper.GetTransactionDateString();
            options.HashString = options.PrivateKey + request.userId + request.cardOwnerName + request.cardNumber +
                                 request.cardExpireMonth + request.cardExpireYear + request.clientIp +
                                 options.TransactionDate;
            return RestHttpCaller.Create()
                .PostJson<BankCardCreateResponse>(options.BaseUrl + "/bankcard/create", Helper.GetHttpHeaders(options,Helper.application_json),
                    request);
        }
    }
}
