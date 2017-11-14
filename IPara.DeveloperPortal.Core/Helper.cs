using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using IPara.DeveloperPortal.Core.Response;

namespace IPara.DeveloperPortal.Core
{
   public static class Helper
    {
       
        /// <summary>
        /// Constant sabitler
        /// Aşağıdaki sabitler projenin her kısmında bu değişken isimleri ile çağırılmak zorundadır.
        /// Bu değişkenler iPara'nın Request veya Response anında bizlerden beklediği alanları ifade eder.
        /// Bu alanların kesinlikle değiştirilmemesi gereklidir.
        /// </summary>
        private const string transactionDate = "transactionDate";
        private const string version = "version";
        private const string token = "token";
        private const string Accept = "Accept";
        public const string application_xml = "application/xml";
        public const string application_json = "application/json";

        /// <summary>
        /// Doğru formatta tarih döndüren yardımcı sınıftır. Isteklerde tarih istenen noktalarda bu fonksiyon sonucu kullanılır. 
        /// Servis çağrılarında kullanılacak istek zamanı için istenen tarih formatında bu fonksiyon kullanılmalıdır.
        /// Bu fonksiyon verdiğimiz tarih değerini iPara'nın bizden beklemiş olduğu tarih formatına değiştirmektedir.
        /// </summary>
        /// <returns></returns>
        public static string GetTransactionDateString()
        {
            return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }

        /// <summary>
        /// Çağrılarda kullanılacak Tokenları oluşturan yardımcı metotdur. 
        /// İstek güvenlik bilgisi kullanılacak tüm çağrılarda token oluşturmamız gerekmektedir.
        /// Token oluştururken hash bilgisi ve public key alanlarının parametre olarak gönderilmesi gerekmektedir.
        /// hashstring alanı servise ait birden fazla alanın birleşmesi sonucu oluşan verileri ve public key mağaza açık anahtarını 
        /// kullanarak bizlere token üretmemizi sağlar.
        /// </summary>
        /// <param name="publicKey">Mağaza Açık Anahtarınız</param>
        /// <param name="hashString">Servise özel bir çok alanın birleştirilmesiyle oluşturulan veriler bütünü</param>
        /// <returns></returns>
        public static string CreateToken(string publicKey, string hashString)
        {
            HashAlgorithm algorithm = new SHA1Managed();
            var hashbytes = Encoding.UTF8.GetBytes(hashString);
            var inputbytes = algorithm.ComputeHash(hashbytes);
          //  var aftersha1= System.Text.Encoding.UTF8.GetString(inputbytes);
            var inputHashString = Convert.ToBase64String(inputbytes);
            return publicKey + ":" + inputHashString;
        }

        /// <summary>
        /// Verilen string'i SHA1 ile hashleyip Base64 formatına çeviren fonksiyondur. 
        /// CreateToken'dan farklı olarak token oluşturmaz sadece hash hesaplar
        /// </summary>
        /// <param name="hashString"></param>
        /// <returns></returns>
        public static string ComputeHash( string hashString)
        {
            HashAlgorithm algorithm = new SHA1Managed();
            var hashbytes = Encoding.UTF8.GetBytes(hashString);
            var inputbytes = algorithm.ComputeHash(hashbytes);
            var inputHashString = Convert.ToBase64String(inputbytes);
            return inputHashString;
        }

        /// <summary>
        /// Bir çok çağrıda kullanılan HTTP Header bilgilerini otomatik olarak ekleyen fonksiyondur. 
        /// </summary>
        /// <param name="settings"></param>
        /// <param name="acceptType"></param>
        /// <returns></returns>
        public static WebHeaderCollection GetHttpHeaders(Settings settings, string acceptType)
        {
            WebHeaderCollection headers = new WebHeaderCollection();
            headers.Add(Accept, acceptType);
            headers.Add(version, settings.Version);
            headers.Add(token,CreateToken(settings.PublicKey,settings.HashString));
            headers.Add(transactionDate, settings.transactionDate);

            return headers;
        }

        /// <summary>
        /// 3D akışının ilk adımında yapılan işlemin ardından gelen cevabın doğrulanması adına kullanılacak
        /// fonksiyondur. 
        /// </summary>
        /// <param name="paymentResponse"></param>
        /// <param name="settings"></param>
        /// <returns></returns>
        public static bool Validate3DReturn(ThreeDPaymentInitResponse paymentResponse, Settings settings)
        {
            if (String.IsNullOrEmpty(paymentResponse.Hash))
                throw new Exception("Ödeme cevabı hash bilgisi boş. [result : " + paymentResponse.Result + ",error_code : " + paymentResponse.ErrorCode + ",error_message : " + paymentResponse.ErrorMessage + "]");

            string hashText = paymentResponse.OrderId + paymentResponse.Result + paymentResponse.Amount + paymentResponse.Mode + paymentResponse.ErrorCode +
     paymentResponse.ErrorMessage + paymentResponse.TransactionDate + settings.PublicKey + settings.PrivateKey;
            var hashedText = Helper.ComputeHash(hashText);
            if (hashedText != paymentResponse.Hash)
                throw new Exception("Ödeme cevabı hash doğrulaması hatalı. [result : " + paymentResponse.Result + ",error_code : " + paymentResponse.ErrorCode + ",error_message : " + paymentResponse.ErrorMessage + "]");

            return true;
        }
    }
}
