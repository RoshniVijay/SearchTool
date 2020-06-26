using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using ImageSearch.Common;
using ImageSearch.DataModel;
using ImageSearch.ServiceComponent.Contracts;
using Newtonsoft.Json;

namespace ImageSearch.ServiceComponent
{
    /// <summary>
    /// Component to make specific query to NewsAPI server
    /// </summary>
    public class NewsAPISearchServiceComponent : BaseServiceComponent, IServiceComponent
    {
        /// <summary>
        /// API to perform NewsAPI search
        /// </summary>
        /// <param name="queryContext"></param>
        public async Task<IResponseContext> PerformSearch(IQueryContext queryContext)
        {
            ApplicationConfiguration appConfig = queryContext.ApplicationConfiguration;
            IDataSource ds = appConfig.GetDataSource(DataSources.NewsAPI);
            if (string.IsNullOrEmpty(queryContext.QueryParam))
                queryContext.QueryParam = "Trending";
            //sample query : "http://newsapi.org/v2/everything?" + "q=Nature&apiKey=a56eae9e984a40b2a88c2eb097427e4b";
            string finalURI = ds.DataSourceURI + "?q=" + queryContext.QueryParam + "&apiKey=a56eae9e984a40b2a88c2eb097427e4b";
            HTTPAPIResponse response = await m_HttpAPIHelper.Get(finalURI) as HTTPAPIResponse;

            IResponseContext searchResponse = ParseResponse(response);
            return searchResponse;
        }

        private IResponseContext ParseResponse(HTTPAPIResponse response)
        {
            TextResponseDataModel responseDataModel = new TextResponseDataModel();
            if(response.Code != ErrorCodes.NoError)
            {
                responseDataModel.NewsItems = new ObservableCollection<Articles>();
                responseDataModel.Status = "Error in receiving response from server";
                return responseDataModel;//in case of exception, list wil be empty
            }

            try
            {
                //TODO: Add sample response here
                var apiResponseJson = JsonConvert.DeserializeObject<NewsApiResponse>(response.ResponseString);
                ObservableCollection<Articles> articleCol = new ObservableCollection<Articles>();
                foreach (Articles article in apiResponseJson.Articles)
                {
                    articleCol.Add(article);
                }
                
                responseDataModel.NewsItems = articleCol;
                responseDataModel.Status = "Parsed response successfully";
            }
            catch (Exception exp)
            {
                Logger.Log("Exception while parsing response from NewsAPI. Details:" + exp);
                responseDataModel.NewsItems = new ObservableCollection<Articles>();
                responseDataModel.Status = "Error in parsing response from server";

                //in case of exception list wil be empty
            }

            return responseDataModel;
        }
    }
}
