using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace IPara.DeveloperPortal.Core.Response
{
    /// <summary>
    ///  Tek Adımda 3D Secure ile ödeme sonucunda oluşan servis çıktı parametrelerini temsil etmektedir.
    /// </summary>
    /// 

    public class Api3DPaymentInOneStepResponse : BaseResponse
    {
        public string Amount { get; set; }
        public string OrderId { get; set; }
        public string PublicKey { get; set; }
        public string CommissionRate { get; set; }
        public string ThreeDSecureCode { get; set; }
    }
}
