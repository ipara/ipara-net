using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using IPara.DeveloperPortal.Core.Entity;
using IPara.DeveloperPortal.Core.Response;

namespace IPara.DeveloperPortal.Core.Request
{
    [XmlRoot("auth")]
    public class ThreeDPaymentInitRequest : BaseRequest
    {
        /// <summary>
        /// 3D Ödeme Formu başlatmak için gerekli olan servis girdi parametrelerini temsil eder. 
        /// </summary>
        [XmlElement("orderId")]
        public string OrderId { get; set; }

        [XmlElement("amount")]
        public string Amount { get; set; }
        [XmlElement("userId")]
        public string UserId { get; set; }
        [XmlElement("cardId")]
        public string CardId { get; set; }

        [XmlElement("cardOwnerName")]
        public string CardOwnerName { get; set; }

        [XmlElement("cardNumber")]
        public string CardNumber { get; set; }

        [XmlElement("cardExpireMonth")]
        public string CardExpireMonth { get; set; }

        [XmlElement("cardExpireYear")]
        public string CardExpireYear { get; set; }

        [XmlElement("installment")]
        public string Installment { get; set; }

        [XmlElement("cardCvc")]
        public string Cvc { get; set; }

        [XmlElement("purchaserName")]
        public string PurchaserName { get; set; }
        [XmlElement("purchaserSurname")]
        public string PurchaserSurname { get; set; }
        [XmlElement("purchaserEmail")]
        public string PurchaserEmail { get; set; }
        [XmlElement("successUrl")]
        public string SuccessUrl { get; set; }
        [XmlElement("failUrl")]
        public string FailUrl { get; set; }


        [XmlElement("version")]
        public string Version { get; set; }
        [XmlElement("transactionDate")]
        public string TransactionDate { get; set; }
        [XmlElement("token")]
        public string Token { get; set; }

        /// <summary>
        /// Diğer fonksiyonların aksine 3D Sınıfı bir formun post edilmesi ile başlar 
        /// bu sebeble bu fonksiyon ilgili HTML formu oluşturur ve geri döndürür.
        /// Bu formu mevcut formun üzerine yazmak ilgili formun Javascript ile post edilmesini sağlar. 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static string CreateThreeDPaymentForm(ThreeDPaymentInitRequest request, Settings options)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("<!DOCTYPE HTML PUBLIC \"-//W3C//DTD HTML 4.0 Transitional//EN\">");
            builder.Append("<html>");
            builder.Append("<body>");
            builder.Append("<form //action=\"" + options.BaseUrl + "\" method=\"post\" id=\"three_d_form\" >");
            builder.Append("<input type=\"hidden\" name=\"orderId\" value=\"" + request.OrderId + "\"/>");
            builder.Append("<input type=\"hidden\" name=\"amount\" value=\"" + request.Amount + "\"/>");
            builder.Append("<input type=\"hidden\" name=\"cardOwnerName\" value=\"" + request.CardOwnerName + "\"/>");
            builder.Append("<input type=\"hidden\" name=\"cardNumber\" value=\"" + request.CardNumber + "\"/>");
            builder.Append("<input type=\"hidden\" name=\"userId\" value=\"" + request.UserId + "\"/>");
            builder.Append("<input type=\"hidden\" name=\"cardId\" value=\"" + request.CardId + "\"/>");
            builder.Append("<input type=\"hidden\" name=\"cardExpireMonth\" value=\"" + request.CardExpireMonth + "\"/>");
            builder.Append("<input type=\"hidden\" name=\"cardExpireYear\" value=\"" + request.CardExpireYear + "\"/>");
            builder.Append("<input type=\"hidden\" name=\"installment\" value=\"" + request.Installment + "\"/>");
            builder.Append("<input type=\"hidden\" name=\"cardCvc\" value=\"" + request.Cvc + "\"/>");
            builder.Append("<input type=\"hidden\" name=\"mode\" value=\"" + request.Mode + "\"/>");
            builder.Append("<input type=\"hidden\" name=\"purchaserName\" value=\"" + request.PurchaserName + "\"/>");
            builder.Append("<input type=\"hidden\" name=\"purchaserSurname\" value=\"" + request.PurchaserSurname + "\"/>");
            builder.Append("<input type=\"hidden\" name=\"purchaserEmail\" value=\"" + request.PurchaserEmail + "\"/>");
            builder.Append("<input type=\"hidden\" name=\"successUrl\" value=\"" + request.SuccessUrl + "\"/>");
            builder.Append("<input type=\"hidden\" name=\"failureUrl\" value=\"" + request.FailUrl + "\"/>");
            builder.Append("<input type=\"hidden\" name=\"echo\" value=\"" + request.Echo + "\"/>");
            builder.Append("<input type=\"hidden\" name=\"version\" value=\"" + request.Version + "\"/>");
            builder.Append("<input type=\"hidden\" name=\"transactionDate\" value=\"" + request.TransactionDate + "\"/>");
            builder.Append("<input type=\"hidden\" name=\"token\" value=\"" + request.Token + "\"/>");
            builder.Append("<input type=\"submit\" value=\"Öde\" style=\"display:none;\"/>");
            builder.Append("<noscript>");
            builder.Append("<br/>");
            builder.Append("<br/>");
            builder.Append("<center>");
            builder.Append("<h1>3D Secure Yönlendirme İşlemi</h1>");
            builder.Append("<h2>Javascript internet tarayıcınızda kapatılmış veya desteklenmiyor.<br/></h2>");
            builder.Append("<h3>Lütfen banka 3D Secure sayfasına yönlenmek için tıklayınız.</h3>");
            builder.Append("<input type=\"submit\" value=\"3D Secure Sayfasına Yönlen\">");
            builder.Append("</center>");
            builder.Append("</noscript>");
            builder.Append("</form>");
            builder.Append("</body>");
            builder.Append("<script>document.getElementById(\"three_d_form\").submit();</script>");
            builder.Append("</html>");
             return builder.ToString();
        }

        /// <summary>
        /// 3D secure 1. Adımında kullanıcı bilgileri alındıktan sonra ilgili servis isteğini temsil eder. 
        /// </summary>
        /// <param name="request">3D secure 1. Adımında gerekli olan servis girdi parametrelerini temsil eder.</param>
        /// <param name="options">Kullanıcıya özel olarak belirlenen ayarları temsil eder.</param>
        /// <returns></returns>
        public static string Execute(ThreeDPaymentInitRequest request, Settings options)
        {
            request.TransactionDate = Helper.GetTransactionDateString();
            options.HashString = options.PrivateKey + request.OrderId + request.Amount + request.Mode + request.CardOwnerName + request.CardNumber + request.CardExpireMonth + request.CardExpireYear + request.Cvc + request.UserId + request.CardId + request.PurchaserName + request.PurchaserSurname + request.PurchaserEmail + request.TransactionDate;
            request.Token = Helper.CreateToken(options.PublicKey, options.HashString);
            return CreateThreeDPaymentForm(request, options);
        }
    }




}

