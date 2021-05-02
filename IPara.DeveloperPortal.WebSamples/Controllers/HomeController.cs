using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IPara.DeveloperPortal.Core;
using IPara.DeveloperPortal.Core.Request;
using IPara.DeveloperPortal.Core.Response;
using IPara.DeveloperPortal.Core.Entity;
using Newtonsoft.Json;

namespace IPara.DeveloperPortal.WebSamples.Controllers
{

    /// <summary>
    /// Bu controller sizler için hazırlamış olduğumuz örnek web projesini temsil etmektedir.
    /// Bu controller içerisinde iPara servislerine istek görderme ve gönderilen istekler sonucunda tarafınıza gelen cevapları
    /// görebilirsiniz.
    /// </summary>
    public class HomeController : BaseController
    {

        /// <summary>
        /// 3D ile ödeme 
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 3D ile ödeme Post işlemi
        /// </summary>
        /// <param name="nameSurname"></param>
        /// <param name="cardNumber"></param>
        /// <param name="cvc"></param>
        /// <param name="month"></param>
        /// <param name="year"></param>
        /// <param name="userId"></param>
        /// <param name="cardId"></param>
        /// <param name="installment"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Index(string nameSurname, string cardNumber, string cvc, string month, string year, string userId, string cardId, string installment)
        {
            //3d iki aşamalı bir işlemdir. İlk adımda 3D güvenlik sorgulaması yapılmalıdır. 

            var request = new ThreeDPaymentInitRequest();
            request.OrderId = Guid.NewGuid().ToString();
            request.Echo = "Echo";
            request.Mode = settings.Mode;
            request.Version = settings.Version;
            request.Amount = "10000"; // 100 tL
            request.CardOwnerName = nameSurname;
            request.CardNumber = cardNumber;
            request.CardExpireMonth = month;
            request.CardExpireYear = year;
            request.Installment = installment;
            request.Cvc = cvc;
            request.CardId = cardId;
            request.UserId = userId;


            request.PurchaserName = "Murat";
            request.PurchaserSurname = "Kaya";
            request.PurchaserEmail = "murat@kaya.com";

            request.SuccessUrl = Request.Url + "Home/ThreeDResultSuccess";
            request.FailUrl = Request.Url + "Home/ThreeDResultFail";

            var form = ThreeDPaymentInitRequest.Execute(request, settings);
            System.Web.HttpContext.Current.Response.Clear();
            System.Web.HttpContext.Current.Response.Write(form);
            System.Web.HttpContext.Current.Response.End();

            return View();
        }

        /// <summary>
        /// 3D başarılı ise yönlendirilecek ve Ödemenin tamamlanacağı sayfayı temsil eder.
        /// </summary>
        /// <returns></returns>
        public ActionResult ThreeDResultSuccess()
        {

            ThreeDPaymentInitResponse paymentResponse = new ThreeDPaymentInitResponse();
            paymentResponse.OrderId = Request.Form["orderId"];
            paymentResponse.Result = Request.Form["result"];
            paymentResponse.Amount = Request.Form["amount"];
            paymentResponse.Mode = Request.Form["mode"];
            if (Request.Form["errorCode"] != null)
                paymentResponse.ErrorCode = Request.Form["errorCode"];

            if (Request.Form["errorMessage"] != null)
                paymentResponse.ErrorMessage = Request.Form["errorMessage"];

            if (Request.Form["transactionDate"] != null)
                paymentResponse.TransactionDate = Request.Form["transactionDate"];

            if (Request.Form["hash"] != null)
                paymentResponse.Hash = Request.Form["hash"];

            if (Helper.Validate3DReturn(paymentResponse, settings))
            {
                var request = new ThreeDPaymentCompleteRequest();

                #region Request New
                request.OrderId = Request.Form["orderId"];
                request.Echo = "Echo";
                request.Mode = settings.Mode;
                request.Amount = "10000"; // 100 tL
                request.CardOwnerName = "Fatih Coşkun";
                request.CardNumber = "4282209027132016";
                request.CardExpireMonth = "05";
                request.CardExpireYear = "18";
                request.Installment = "1";
                request.Cvc = "000";
                request.ThreeD = "true";
                request.ThreeDSecureCode = Request.Form["threeDSecureCode"];
                #endregion

                #region Sipariş veren bilgileri
                request.Purchaser = new Purchaser();
                request.Purchaser.BirthDate = "1986-07-11";
                request.Purchaser.GsmPhone = "5881231212";
                request.Purchaser.IdentityNumber = "1234567890";
                #endregion

                #region Fatura bilgileri
                request.Purchaser.InvoiceAddress = new PurchaserAddress();
                request.Purchaser.InvoiceAddress.Name = "Murat";
                request.Purchaser.InvoiceAddress.SurName = "Kaya";
                request.Purchaser.InvoiceAddress.Address = "Mevlüt Pehlivan Mah. Multinet Plaza Şişli";
                request.Purchaser.InvoiceAddress.ZipCode = "34782";
                request.Purchaser.InvoiceAddress.CityCode = "34";
                request.Purchaser.InvoiceAddress.IdentityNumber = "1234567890";
                request.Purchaser.InvoiceAddress.CountryCode = "TR";
                request.Purchaser.InvoiceAddress.TaxNumber = "123456";
                request.Purchaser.InvoiceAddress.TaxOffice = "Kozyatağı";
                request.Purchaser.InvoiceAddress.CompanyName = "iPara";
                request.Purchaser.InvoiceAddress.PhoneNumber = "2122222222";
                #endregion

                #region Kargo Adresi bilgileri
                request.Purchaser.ShippingAddress = new PurchaserAddress();
                request.Purchaser.ShippingAddress.Name = "Murat";
                request.Purchaser.ShippingAddress.SurName = "Kaya";
                request.Purchaser.ShippingAddress.Address = "Mevlüt Pehlivan Mah. Multinet Plaza Şişli";
                request.Purchaser.ShippingAddress.ZipCode = "34782";
                request.Purchaser.ShippingAddress.CityCode = "34";
                request.Purchaser.ShippingAddress.IdentityNumber = "1234567890";
                request.Purchaser.ShippingAddress.CountryCode = "TR";
                request.Purchaser.ShippingAddress.PhoneNumber = "2122222222";
                #endregion

                #region Ürün bilgileri
                request.Products = new List<Product>();
                Product p = new Product();
                p.Title = "Telefon";
                p.Code = "TLF0001";
                p.Price = "5000";
                p.Quantity = 1;
                request.Products.Add(p);
                p = new Product();
                p.Title = "Bilgisayar";
                p.Code = "BLG0001";
                p.Price = "5000";
                p.Quantity = 1;
                request.Products.Add(p);
                #endregion

                var response = ThreeDPaymentCompleteRequest.Execute(request, settings);
                return View(response);

            }
            else
            {
                return RedirectToAction("ThreeDResultFail");
            }

        }

        /// <summary>
        /// 3D başarısız olursa başarısız olduğu sonucunun ekrana yazdırıldığı sayfayı temsil eder.
        /// </summary>
        /// <returns></returns>
        public ActionResult ThreeDResultFail()
        {
            ThreeDPaymentInitResponse response = new ThreeDPaymentInitResponse();
            response.OrderId = Request.Form["orderId"];
            response.Result = Request.Form["result"];
            response.Amount = Request.Form["amount"];
            response.Mode = Request.Form["mode"];
            if (Request.Form["errorCode"] != null)
                response.ErrorCode = Request.Form["errorCode"];

            if (Request.Form["errorMessage"] != null)
                response.ErrorMessage = Request.Form["errorMessage"];

            if (Request.Form["transactionDate"] != null)
                response.TransactionDate = Request.Form["transactionDate"];

            if (Request.Form["hash"] != null)
                response.Hash = Request.Form["hash"];
            return View(response);
        }

        /// <summary>
        /// Bin sorgulama sayfasını temsil eder.
        /// </summary>
        /// <returns></returns>
        public ActionResult BinInqury()
        {
            return View();
        }

        /// <summary>
        /// Bin sorgulama sayfasından post edilen bin numarasının ilgili serviste işlenip sonucunun ekranda gösterildiği sayfayı temsil eder.
        /// </summary>
        /// <param name="binNumber"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult BinInqury(string binNumber)
        {
            BinNumberInquiryRequest request = new BinNumberInquiryRequest();
            request.binNumber = binNumber;
            BinNumberInquiryResponse response = BinNumberInquiryRequest.Execute(request, settings);
            return View(response);
        }

        /// <summary>
        /// Cüzdana kart ekleme sayfasını temsil eder.
        /// </summary>
        /// <returns></returns>
        public ActionResult AddCardToWallet()
        {
            return View();
        }

        /// <summary>
        /// Cüzdana kart ekleme sayfasından post edilen değerlerle ilgili servise istek bilgisinin gönderilip sonucunun ekrana yazdırıldığı sayfayı temsil eder.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="nameSurname"></param>
        /// <param name="cardNumber"></param>
        /// <param name="cardAlias"></param>
        /// <param name="month"></param>
        /// <param name="year"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddCardToWallet(string userId, string nameSurname, string cardNumber, string cardAlias, string month, string year)
        {
            BankCardCreateRequest request = new BankCardCreateRequest();
            request.userId = userId;
            request.cardOwnerName = nameSurname;
            request.cardNumber = cardNumber;
            request.cardAlias = cardAlias;
            request.cardExpireMonth = month;
            request.cardExpireYear = year;
            request.clientIp = "127.0.0.1";
            return View(BankCardCreateRequest.Execute(request, settings));
        }

        /// <summary>
        /// Cüzdandaki Kartları Listeleme sayfasını temsil eder.
        /// </summary>
        /// <returns></returns>
        public ActionResult GetCardFromWallet()
        {
            return View();
        }

        /// <summary>
        /// Cüzdandaki Kartları Listele sayfasından post edilen değerlerle ilgili servise istek bilgisinin gönderilip sonucunun ekrana yazdırıldığı sayfayı temsil eder.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="cardId"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetCardFromWallet(string userId, string cardId)
        {
            BankCardInquiryRequest request = new BankCardInquiryRequest();
            request.userId = userId;
            request.cardId = cardId;
            request.clientIp = "127.0.0.1";

            BankCardInquryResponse response = BankCardInquiryRequest.Execute(request, settings);
            return View(response);
        }

        /// <summary>
        /// Cüzdandaki kartları Silme sayfasını temsil eder.
        /// </summary>
        /// <returns></returns>
        public ActionResult DeleteCardFromWallet()
        {
            return View();
        }

        /// <summary>
        /// Cüzdandaki kartları Silme sayfasından post edilen değerlerle ilgili servise istek bilgisinin gönderildiği sonucunun ekrana yazdırıldığı sayfayı temsil eder.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="cardId"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DeleteCardFromWallet(string userId, string cardId)
        {
            BankCardDeleteRequest request = new BankCardDeleteRequest();
            request.userId = userId;
            request.cardId = cardId;
            request.clientIp = "127.0.0.1";
            BankCardDeleteResponse response = BankCardDeleteRequest.Execute(request, settings);
            return View(response);
        }

        /// <summary>
        /// Ödeme sorgulama sayfasını temsil eder.
        /// </summary>
        /// <returns></returns>
        public ActionResult PaymentInqury()
        {
            return View();
        }

        /// <summary>
        /// ödeme sorgulama sonucu post edilen değerlerle ilgili servise istek bilgisinin gönderildiği sonucunun ekrana yazdırıldığı sayfayı temsil eder.
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        [HttpPost]

        public ActionResult PaymentInqury(string orderId)
        {
            PaymentInquiryRequest request = new PaymentInquiryRequest();
            request.orderId = orderId;
            request.Mode = settings.Mode;
            request.Echo = "Echo";
            PaymentInquiryResponse response = PaymentInquiryRequest.Execute(request, settings);
            return View(response);
        }

        /// <summary>
        /// 3D olmadan ödeme sayfasını temsil eder.
        /// </summary>
        /// <returns></returns>
        public ActionResult ApiPayment()
        {
            return View();
        }

        /// <summary>
        /// 3D olmadan ödeme sayfasından post edilen değerlerle ilgili servise istek bilgisinin gönderildiği sonucunun ekrana yazdırıldığı sayfayı temsil eder.
        /// </summary>
        /// <param name="nameSurname"></param>
        /// <param name="cardNumber"></param>
        /// <param name="cvc"></param>
        /// <param name="month"></param>
        /// <param name="year"></param>
        /// <param name="installment"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ApiPayment(string nameSurname, string cardNumber, string cvc, string month, string year, string installment)
        {
            var request = new ApiPaymentRequest();

            #region Request New
            request.OrderId = Guid.NewGuid().ToString();
            request.Echo = "Echo";
            request.Mode = settings.Mode;
            request.Amount = "10000"; // 100.00 tL
            request.CardOwnerName = nameSurname;
            request.CardNumber = cardNumber;
            request.CardExpireMonth = month;
            request.CardExpireYear = year;
            request.Installment = installment;
            request.Cvc = cvc;
            request.ThreeD = "false";
            request.CardId = "";
            request.UserId = "";

            #endregion

            #region Sipariş veren bilgileri
            request.Purchaser = new Purchaser();
            request.Purchaser.Name = "Murat";
            request.Purchaser.SurName = "Kaya";
            request.Purchaser.BirthDate = "1986-07-11";
            request.Purchaser.Email = "murat@kaya.com";
            request.Purchaser.GsmPhone = "5881231212";
            request.Purchaser.IdentityNumber = "1234567890";
            request.Purchaser.ClientIp = "127.0.0.1";
            #endregion

            #region Fatura bilgileri

            request.Purchaser.InvoiceAddress = new PurchaserAddress();
            request.Purchaser.InvoiceAddress.Name = "Murat";
            request.Purchaser.InvoiceAddress.SurName = "Kaya";
            request.Purchaser.InvoiceAddress.Address = "Mevlüt Pehlivan Mah. Multinet Plaza Şişli";
            request.Purchaser.InvoiceAddress.ZipCode = "34782";
            request.Purchaser.InvoiceAddress.CityCode = "34";
            request.Purchaser.InvoiceAddress.IdentityNumber = "1234567890";
            request.Purchaser.InvoiceAddress.CountryCode = "TR";
            request.Purchaser.InvoiceAddress.TaxNumber = "123456";
            request.Purchaser.InvoiceAddress.TaxOffice = "Kozyatağı";
            request.Purchaser.InvoiceAddress.CompanyName = "iPara";
            request.Purchaser.InvoiceAddress.PhoneNumber = "2122222222";

            #endregion

            #region Kargo Adresi bilgileri

            request.Purchaser.ShippingAddress = new PurchaserAddress();
            request.Purchaser.ShippingAddress.Name = "Murat";
            request.Purchaser.ShippingAddress.SurName = "Kaya";
            request.Purchaser.ShippingAddress.Address = "Mevlüt Pehlivan Mah. Multinet Plaza Şişli";
            request.Purchaser.ShippingAddress.ZipCode = "34782";
            request.Purchaser.ShippingAddress.CityCode = "34";
            request.Purchaser.ShippingAddress.IdentityNumber = "1234567890";
            request.Purchaser.ShippingAddress.CountryCode = "TR";
            request.Purchaser.ShippingAddress.PhoneNumber = "2122222222";

            #endregion

            #region Ürün bilgileri

            request.Products = new List<Product>();
            Product p = new Product();
            p.Title = "Telefon";
            p.Code = "TLF0001";
            p.Price = "5000"; //50.00 TL 
            p.Quantity = 1;
            request.Products.Add(p);
            p = new Product();
            p.Title = "Bilgisayar";
            p.Code = "BLG0001";
            p.Price = "5000"; //50.00 TL 
            p.Quantity = 1;
            request.Products.Add(p);

            ApiPaymentResponse response = ApiPaymentRequest.Execute(request, settings);

            return View(response);

            #endregion
        }

        /// <summary>
        /// Cüzdandaki kart ile ödeme sayfasını temsil eder.
        /// </summary>
        /// <returns></returns>
        public ActionResult ApiPaymentWithWallet()
        {
            return View();
        }

        /// <summary>
        /// Cüzdandaki kart sayfasından post edilen değerlerle ilgili servise istek bilgisinin gönderildiği sonucunun ekrana yazdırıldığı sayfayı temsil eder.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="cardId"></param>
        /// <param name="installment"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ApiPaymentWithWallet(string userId, string cardId, string installment)
        {
            var request = new ApiPaymentRequest();
            #region Request New
            request.OrderId = Guid.NewGuid().ToString();
            request.Echo = "Echo"; // Cevap anında geri gelecek işlemi ayırt etmeye yarayacak alan
            request.Mode = settings.Mode;
            request.Amount = "10000"; // 100.00 tL
            request.CardOwnerName = "";
            request.CardNumber = "";
            request.CardExpireMonth = "";
            request.CardExpireYear = "";
            request.Installment = installment;
            request.Cvc = "";
            request.ThreeD = "false";
            request.CardId = cardId;
            request.UserId = userId;

            #endregion
            //Buradaki bilgilerin sizin tablolarınız veya ekranlarınızdan gelmesi gerekmektedir. 
            #region Sipariş veren bilgileri
            request.Purchaser = new Purchaser();
            request.Purchaser.Name = "Murat";
            request.Purchaser.SurName = "Kaya";
            request.Purchaser.BirthDate = "1986-07-11";
            request.Purchaser.Email = "murat@kaya.com";
            request.Purchaser.GsmPhone = "5881231212";
            request.Purchaser.IdentityNumber = "1234567890";
            request.Purchaser.ClientIp = "127.0.0.1";
            #endregion
            //Buradaki bilgilerin sizin tablolarınız veya ekranlarınızdan gelmesi gerekmektedir. 
            #region Fatura bilgileri

            request.Purchaser.InvoiceAddress = new PurchaserAddress();
            request.Purchaser.InvoiceAddress.Name = "Murat";
            request.Purchaser.InvoiceAddress.SurName = "Kaya";
            request.Purchaser.InvoiceAddress.Address = "Mevlüt Pehlivan Mah. Multinet Plaza Şişli";
            request.Purchaser.InvoiceAddress.ZipCode = "34782";
            request.Purchaser.InvoiceAddress.CityCode = "34";
            request.Purchaser.InvoiceAddress.IdentityNumber = "1234567890";
            request.Purchaser.InvoiceAddress.CountryCode = "TR";
            request.Purchaser.InvoiceAddress.TaxNumber = "123456";
            request.Purchaser.InvoiceAddress.TaxOffice = "Kozyatağı";
            request.Purchaser.InvoiceAddress.CompanyName = "iPara";
            request.Purchaser.InvoiceAddress.PhoneNumber = "2122222222";

            #endregion
            //Buradaki bilgilerin sizin tablolarınız veya ekranlarınızdan gelmesi gerekmektedir. 
            #region Kargo Adresi bilgileri

            request.Purchaser.ShippingAddress = new PurchaserAddress();
            request.Purchaser.ShippingAddress.Name = "Murat";
            request.Purchaser.ShippingAddress.SurName = "Kaya";
            request.Purchaser.ShippingAddress.Address = "Mevlüt Pehlivan Mah. Multinet Plaza Şişli";
            request.Purchaser.ShippingAddress.ZipCode = "34782";
            request.Purchaser.ShippingAddress.CityCode = "34";
            request.Purchaser.ShippingAddress.IdentityNumber = "1234567890";
            request.Purchaser.ShippingAddress.CountryCode = "TR";
            request.Purchaser.ShippingAddress.PhoneNumber = "2122222222";

            #endregion
            //Buradaki bilgilerin sizin tablolarınız veya ekranlarınızdan gelmesi gerekmektedir. 
            #region Ürün bilgileri

            request.Products = new List<Product>();
            Product p = new Product();
            p.Title = "Telefon";
            p.Code = "TLF0001";
            p.Price = "5000"; //50.00 TL 
            p.Quantity = 1;
            request.Products.Add(p);
            p = new Product();
            p.Title = "Bilgisayar";
            p.Code = "BLG0001";
            p.Price = "5000"; //50.00 TL 
            p.Quantity = 1;
            request.Products.Add(p);

            ApiPaymentResponse response = ApiPaymentRequest.Execute(request, settings);

            return View(response);

            #endregion
        }

        /// <summary>
        /// Tek adımda 3D ile ödeme 
        /// </summary>
        /// <returns></returns>
        public ActionResult Api3DPaymentInOneStep()
        {
            return View();
        }

        /// <summary>
        /// Tek adımda 3D ile ödeme Post işlemi
        /// </summary>
        /// <param name="nameSurname"></param>
        /// <param name="cardNumber"></param>
        /// <param name="cvc"></param>
        /// <param name="month"></param>
        /// <param name="year"></param>
        /// <param name="userId"></param>
        /// <param name="cardId"></param>
        /// <param name="installment"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Api3DPaymentInOneStep(string nameSurname, string cardNumber, string cvc, string month, string year, string userId, string cardId, string installment)
        {
            var request = new Api3DPaymentInOneStepRequest();
            request.OrderId = Guid.NewGuid().ToString();
            request.Echo = "Echo";
            request.Mode = settings.Mode;
            request.Version = settings.Version;
            request.Amount = "10000"; // 100 tL
            request.CardOwnerName = nameSurname;
            request.CardNumber = cardNumber;
            request.CardExpireMonth = month;
            request.CardExpireYear = year;
            request.Installment = installment;
            request.Cvc = cvc;
            request.CardId = cardId;
            request.UserId = userId;

            request.Language = "tr-TR"; // ext
            request.Purchaser = new Purchaser
            {
                Name = "Murat",
                SurName = "Kaya",
                Email = "murat@kaya.com",
                ClientIp = "127.0.0.1",
                BirthDate = "1980-07-29"
            };

            #region Ürün bilgileri

            request.Products = new List<Product>();
            Product p = new Product();
            p.Title = "Telefon";
            p.Code = "TLF0001";
            p.Price = "5000";
            p.Quantity = 1;
            request.Products.Add(p);

            p = new Product();
            p.Title = "Bilgisayar";
            p.Code = "BLG0001";
            p.Price = "5000";
            p.Quantity = 1;
            request.Products.Add(p);
            #endregion

            request.SuccessUrl = Request.Url.Scheme + "://" + Request.Url.Authority + "/Home/Api3DPaymentInOneStepResult";
            request.FailUrl = Request.Url.Scheme + "://" + Request.Url.Authority + "/Home/Api3DPaymentInOneStepResult";

            var form = Api3DPaymentInOneStepRequest.Execute(request, settings);
            System.Web.HttpContext.Current.Response.Clear();
            System.Web.HttpContext.Current.Response.Write(form);
            System.Web.HttpContext.Current.Response.End();

            return View();
        }


        /// <summary>
        /// Tek Adımda 3D ödeme sonucu yönlendirilecek sayfayı temsil eder. Başarılı ve başarısız cevaplar için ayrı ayrı sayfalar da oluşturulabilir.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Api3DPaymentInOneStepResult()
        {
            Api3DPaymentInOneStepResponse response = new Api3DPaymentInOneStepResponse();
            response.Result = Request.Form["result"];
            response.Amount = Request.Form["amount"];
            response.CommissionRate= Request.Form["commissionRate"];
            response.PublicKey = Request.Form["publicKey"];
            response.OrderId = Request.Form["orderId"];
            response.ErrorCode = Request.Form["errorCode"];
            response.ErrorMessage = Request.Form["errorMessage"];
            response.TransactionDate = Request.Form["transactionDate"];
            response.ThreeDSecureCode = Request.Form["threeDSecureCode"];

            return View(response);
        }
    }
}

