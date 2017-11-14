using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPara.DeveloperPortal.Core.Entity
{
    /// <summary>
    /// Bu sınıf cüzdana kart ekleme servisi isteği sonucunda ve cüzdandaki kartları getir isteği sonucunda bize döndürülen 
    /// alanları temsil etmektedir. 
    /// </summary>
    public class BankCard 
    {
        public string cardId { get; set; }

        public string maskNumber { get; set; }

        public string alias { get; set; }

        public string bankId { get; set; }

        public string bankName { get; set; }

        public string cardFamilyName { get; set; }

        public string supportsInstallment { get; set; }
        public List<string> supportedInstallments { get; set; }
        public string type { get; set; }

        public string serviceProvider { get; set; }

        public string threeDSecureMandatory { get; set; }
        public string cvcMandatory { get; set; }
    }
}
