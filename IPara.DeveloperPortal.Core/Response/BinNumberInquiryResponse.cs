using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IPara.DeveloperPortal.Core.Entity;

namespace IPara.DeveloperPortal.Core.Response
{

    /// <summary>
    /// Bin Sorgulama servisi sonucunda oluşan servis çıktı parametre alanlarını temsil etmektedir. 
    /// </summary>
    public class BinNumberInquiryResponse : BaseResponse
    {

        public int bankId { get; set; }
        public string bankName { get; set; }
        public int bankFamilyId { get; set; }
        public string cardFamilyName { get; set; }

        public int supportsInstallment { get; set; }
        public List<int> supportedInstallments { get; set; }

        public List<RequiredAmount> installmentDetail { get; set; }
        public int type { get; set; }

        public int serviceProvider { get; set; }

        public int cardThreeDSecureMandatory { get; set; }
        public int merchantThreeDSecureMandatory { get; set; }
        public int cvcMandatory { get; set; }

        public int businessCard { get; set; }

        public int supportsAgriculture { get; set; }
    }

}
