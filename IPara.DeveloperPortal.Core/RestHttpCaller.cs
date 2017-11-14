using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using IPara.DeveloperPortal.Core.Request;
using Newtonsoft.Json;

namespace IPara.DeveloperPortal.Core
{
    /// <summary>
    /// JSON ve XML'leri verilen adreslere post eden sınıftır. Verilen Response sınıfına göre geri dönüş yapar, 
    /// aynen kopyalanarak kullanılabilir
    /// </summary>
    public class RestHttpCaller
    {
        public static RestHttpCaller Create()
        {
            return new RestHttpCaller();
        }

        public T GetJson<T>(String url)
        {
            HttpClient httpClient = new HttpClient();
            HttpResponseMessage httpResponseMessage = httpClient.GetAsync(url).Result;

            return JsonConvert.DeserializeObject<T>(httpResponseMessage.Content.ReadAsStringAsync().Result);
        }
      
        /// <summary>
        /// Alanların Json olarak post edilmesine olanak sağlar.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="headers"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        public T PostJson<T>(String url, WebHeaderCollection headers, BaseRequest request)
        {
            HttpClient httpClient = new HttpClient();
            foreach (String key in headers.Keys)
            {
                httpClient.DefaultRequestHeaders.Add(key, headers.Get(key));
            }
            HttpResponseMessage httpResponseMessage = httpClient.PostAsync(url, JsonBuilder.ToJsonString(request)).Result;
            var a = httpResponseMessage.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<T>(a);
        }

        public T GetXML<T>(String url)
        {
            HttpClient httpClient = new HttpClient();
            HttpResponseMessage httpResponseMessage = httpClient.GetAsync(url).Result;

            return JsonConvert.DeserializeObject<T>(httpResponseMessage.Content.ReadAsStringAsync().Result);
        }

        /// <summary>
        /// Xml olarak post edilmesi istenen istek sınıflarında gönderilen değerlerin xml olarak post edilmesine
        /// imkan sağlayan metodu temsil eder.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="headers"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        public T PostXML<T>(String url, WebHeaderCollection headers, BaseRequest request)
        {
            HttpClient httpClient = new HttpClient();
            foreach (String key in headers.Keys)
            {
                httpClient.DefaultRequestHeaders.Add(key, headers.Get(key));
            }
            var xml = XmlBuilder.SerializeToXMLString(request);
            HttpResponseMessage httpResponseMessage = httpClient.PostAsync(url, xml).Result;
            var a = httpResponseMessage.Content.ReadAsStringAsync().Result;
            return XmlBuilder.DeserializeObject<T>(a);
        }

       


    }
}
    