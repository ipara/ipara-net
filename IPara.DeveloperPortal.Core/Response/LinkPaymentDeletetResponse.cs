using IPara.DeveloperPortal.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace IPara.DeveloperPortal.Core.Response
{
    /// <summary>
    /// Linkle Ödeme -> Link Silme Servisi çıktı parametre alanları temsil eder.
    /// </summary>
    public class LinkPaymentDeleteResponse : BaseResponse
    {
        public List<PaymentLink> linkPaymentRecordList { get; set; }

        public string pageIndex { get; set; }
        public string pageSize { get; set; }
        public string pageCount { get; set; }

    }
}
