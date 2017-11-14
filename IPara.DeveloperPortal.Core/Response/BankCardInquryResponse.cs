using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IPara.DeveloperPortal.Core.Entity;

namespace IPara.DeveloperPortal.Core.Response
{
    /// <summary>
    /// Cüzdanda bulunan kartları getirmek için kullanılan servis çıktı parametrelerini temsil etmektedir.
    /// </summary>
    public class BankCardInquryResponse: BaseResponse
    {
        public List<BankCard> cards { get; set; }

    }
}
