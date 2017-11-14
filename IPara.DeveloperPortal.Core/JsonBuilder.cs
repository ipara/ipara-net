using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace IPara.DeveloperPortal.Core
{
    /// <summary>
    /// Istekleri JSON olarak oluşturucak sınıftır. 
    /// JSON olarak istenen  istekleri json olarak oluşturmamıza olanak sağlar.
    /// </summary>
    public class JsonBuilder
    {
        public static string SerializeToJsonString(BaseRequest request)
        {
            return JsonConvert.SerializeObject(request, new JsonSerializerSettings()
            {
                Formatting = Formatting.None,
                NullValueHandling = NullValueHandling.Ignore,
                DefaultValueHandling = DefaultValueHandling.Ignore,
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });
        }

        public static StringContent ToJsonString(BaseRequest request)
        {
            return new StringContent(SerializeToJsonString(request), Encoding.Unicode, "application/json");
        }
    }
}
