using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPara.DeveloperPortal.Core
{
    /// <summary>
    /// Tüm çağrılarda kullanılacak ayarların tutulduğu sınıftır. 
    /// Bu sınıf üzerinde size özel parametreler fonksiyonlar arasında taşınabilir.
    /// Bu sınıf üzerinde tüm sistemde kullanacağımız ayarları tutar ve bunlara göre işlem yaparız.
    /// Bu sınıf örnek projemizde BaseController içerisinde kullanılmıştır. Ve tüm ayarların kullanılacağı yerde karşımıza çıkmaktadır.
    /// </summary>
    public class Settings
    {      
        public string PublicKey { get; set; }           //"Public Magaza Anahtarı - size mağaza başvurunuz sonucunda gönderilen public key (açık anahtar) bilgisini kullanınız.",
        public string PrivateKey { get; set; }          //"Private Magaza Anahtarı  - size mağaza başvurunuz sonucunda gönderilen privaye key (gizli anahtar) bilgisini kullanınız.",
        public string BaseUrl { get; set; }             //iPara web servisleri API url'lerinin başlangıç bilgisidir.
        public string ThreeDInquiryUrl { get; set; }    //iPara 3D operasyonu için güvenlik sorgusu servisinin URL bilgisi.
        public string Mode { get; set; }                // Test -> T, entegrasyon testlerinin sırasında "T" modunu, canlı sisteme entegre olarak ödeme almaya başlamak için ise Prod -> "P" modunu kullanınız.
        public string Version { get; set; }             // Kullandığınız iPara API versiyonudur. 
        public string HashString { get; set; }          // Kullanacağınız hash bilgisini, bağlanmak istediğiniz web servis bilgisine göre doldurulmalıdır.
        public string TransactionDate { get; set; }     //IPara.DeveloperPortal.Core.Helper#GetTransactionDateString() ile transaction'in gerçekleştiği zaman bilgisini iletmekte kullanılır.
    }
}
