using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using IPara.DeveloperPortal.Core.Request;

namespace IPara.DeveloperPortal.Core
{
    /// <summary>
    /// Istekleri XML olarak oluşturucak sınıftır. 
    /// </summary>
    public class XmlBuilder
    {

        public static string GetXMLFromObject(object o)
        {
            StringWriter sw = new StringWriter();
            XmlTextWriter tw = null;
            try
            {
                XmlSerializer serializer = new XmlSerializer(o.GetType());
                tw = new XmlTextWriter(sw);
                serializer.Serialize(tw, o);
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
            finally
            {
                sw.Close();
                if (tw != null)
                {
                    tw.Close();
                }
            }
            return sw.ToString();
        }

        public static StringContent SerializeToXMLString(BaseRequest request)
        {
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add("", "");
            var stringwriter = new System.IO.StringWriter();
            var serializer = new XmlSerializer(request.GetType());
            serializer.Serialize(stringwriter, request,ns);
            var str = stringwriter.ToString().Replace("utf-16", "utf-8");
            return new StringContent(str, Encoding.UTF8, "application/xml");
        }
        public static T DeserializeObject<T>(string xmlString)
        {
            try
            {
                XmlSerializer xs = new XmlSerializer(typeof(T));
                MemoryStream memoryStream = new MemoryStream(StringToUTF8ByteArray(xmlString));
                XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8);

                return (T)Convert.ChangeType(xs.Deserialize(memoryStream), typeof(T));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static Byte[] StringToUTF8ByteArray(String xmlString)
        {
            UTF8Encoding encoding = new UTF8Encoding();
            Byte[] byteArray = encoding.GetBytes(xmlString);
            return byteArray;
        }

    


    }
}
