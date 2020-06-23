using ImageSearch.DataModel.Contracts;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml;

namespace ImageSearch.ServiceComponent.Utilities
{
    public static class HttpHelper
    {
        public const string ContentType = "Content-Type";
        public const string JsonContentType = "application/json";
        public const string XmlContentType = "application/xml";
        private static HttpClient client = new HttpClient();

        public static async Task<List<string>> Get(IQueryContext queryContext)
        {
            try
            {
                string searchString = queryContext.QueryParam;
                string url = "https://www.flickr.com/services/feeds/photos_public.gne";
                string final = url + "?tags=" + searchString;

                HttpResponseMessage response = await client.GetAsync(final);
                if (response.IsSuccessStatusCode)
                {
                    var stringsdf = await response.Content.ReadAsStringAsync();

                    System.IO.Stream str = await response.Content.ReadAsStreamAsync();
                    XmlDocument xDoc = new XmlDocument();
                    xDoc.Load(str);

                    XmlNodeList name = xDoc.GetElementsByTagName("entry");

                    List<string> listURI = new List<string>();
                    foreach (XmlNode xn in name)
                    {
                        var enu = xn.ChildNodes.GetEnumerator();
                        while (enu.MoveNext())
                        {
                            XmlNode xmlnode = (XmlNode)enu.Current;
                            if (xmlnode.Name == "link")
                            {
                                XmlNode hrefNode = xmlnode.Attributes.GetNamedItem("type");
                                if (hrefNode.Value == "image/jpeg")
                                {
                                    Console.WriteLine(xmlnode.Attributes.GetNamedItem("href").Value);
                                    listURI.Add(xmlnode.Attributes.GetNamedItem("href").Value);
                                }
                            }

                        }
                    }
                    return listURI;

                }
                else
                {
                    Console.Write("Error");
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }


}
