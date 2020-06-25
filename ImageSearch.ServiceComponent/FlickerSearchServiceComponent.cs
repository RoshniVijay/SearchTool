using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Xml;
using ImageSearch.Common;
using ImageSearch.DataModel;
using ImageSearch.DataModel.Contracts;
using ImageSearch.ServiceComponent.Contracts;
using ImageSearch.ServiceComponent.Utilities;

namespace ImageSearch.ServiceComponent
{
    /// <summary>
    /// Component to make specific query to Flicker server
    /// </summary>
    public class FlickerSearchServiceComponent : IServiceComponent
    {
        /// <summary>
        /// API to perform flicker search
        /// </summary>
        /// <param name="queryContext"></param>
        public async Task<IResponseContext> PerformSearch(IQueryContext queryContext)
        {
            ApplicationConfiguration appConfig = queryContext.ApplicationConfiguration;
            IDataSource ds = appConfig.GetDataSource(DataSources.Flicker);
            //sample query : https://www.flickr.com/services/feeds/photos_public.gne?tags=Nature
            string finalURI = ds.DataSourceURI + "?tags=" + queryContext.QueryParam;
            HTTPAPIResponse response = await HttpRestAPIHelper.Get(finalURI) as HTTPAPIResponse;

            IResponseContext searchResponse = ParseResponse(response);
            return searchResponse;
        }

        private IResponseContext ParseResponse(HTTPAPIResponse response)
        {
            ImageResponseDataModel responseDataModel = new ImageResponseDataModel();
            if(response.Code != ErrorCodes.NoError)
            {
                responseDataModel.URI = new ObservableCollection<string>();
                return responseDataModel;//in case of exception list wil be empty
            }

            try
            {
                //TODO: Add sample response here
                XmlDocument xmlResponseDoc = new XmlDocument();
                xmlResponseDoc.LoadXml(response.ResponseString);
                XmlNodeList entryNodes = xmlResponseDoc.GetElementsByTagName("entry");

                ObservableCollection<string> imageURICol = new ObservableCollection<string>();
                foreach (XmlNode entryNode in entryNodes)
                {
                    var childNodes = entryNode.ChildNodes.GetEnumerator();
                    while (childNodes.MoveNext())
                    {
                        XmlNode curNode = (XmlNode)childNodes.Current;
                        if (curNode.Name == "link")
                        {
                            XmlNode hrefNode = curNode.Attributes.GetNamedItem("type");
                            if (hrefNode.Value == "image/jpeg")
                            {
                                imageURICol.Add(curNode.Attributes.GetNamedItem("href").Value);
                            }
                        }
                    }
                }
                responseDataModel.URI = imageURICol;
            }
            catch (Exception exp)
            {
                Logger.Log("Exception while parsing response from flicker. Details:" + exp.ToString());
                responseDataModel.URI = new ObservableCollection<string>();
                //in case of exception list wil be empty
            }

            return responseDataModel;
        }
    }
}
