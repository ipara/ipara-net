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
    public class Api3DPaymentInOneStepRequest : ThreeDPaymentInitRequest
    {
        [JsonProperty("language")]
        public string Language { get; set; }

        [JsonProperty("products")]
        public List<Product> Products { get; set; }

        [JsonProperty("purchaser")]
        public Purchaser Purchaser { get; set; }

        public static string CreateThreeDPaymentForm(String parameters, Settings options)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("<!DOCTYPE HTML PUBLIC \"-//W3C//DTD HTML 4.0 Transitional//EN\">");
            builder.Append("<html>");
            builder.Append("<body>");
            builder.Append("<form //action=\"" + options.BaseUrl + "rest/payment/threed" + "\" method=\"post\" id=\"three_d_form\" >");
            builder.Append("<input type=\"hidden\" name=\"parameters\" value=\"" + HttpContext.Current.Server.HtmlEncode(parameters) + "\"/>");
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

        public static string Execute(Api3DPaymentInOneStepRequest request, Settings options)
        {
            request.TransactionDate = Helper.GetTransactionDateString();
            options.HashString = options.PrivateKey + request.OrderId + request.Amount + request.Mode + request.CardOwnerName + request.CardNumber + request.CardExpireMonth + request.CardExpireYear + request.Cvc + request.UserId + request.CardId + request.Purchaser.Name + request.Purchaser.SurName + request.Purchaser.Email + request.TransactionDate;
            request.Token = Helper.CreateToken(options.PublicKey, options.HashString);
            var parameters = JsonBuilder.SerializeToJsonString(request);
            return CreateThreeDPaymentForm(parameters, options);
        }

    }

}

