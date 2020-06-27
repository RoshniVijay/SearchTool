using System;
using System.Threading.Tasks;
using SearchTool.Common;
using SearchTool.SearchComponent;
using SearchTool.SearchComponent.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Reflection;
using SearchTool.DataModel;

namespace UnitTests.SearchComponent
{
    [TestClass]
    public class TestSearchComponents
    {
        /// <summary>
        /// Tests the create factory  method to check if correct instance are returned during repeated calls
        /// </summary>
        [TestMethod]
        public void Test_CreateSearchComponent_Positive()
        {
            ISearchComponentFactory serviceFactory = new SearchComponentFactory();
            ISearchComponent SearchComponent = serviceFactory.CreateSearchComponent(DataSources.Flicker);
            Assert.IsTrue(SearchComponent is FlickerSearchSearchComponent);

            ISearchComponent SearchComponent2 = serviceFactory.CreateSearchComponent(DataSources.Flicker);
            Assert.IsTrue(SearchComponent2 is FlickerSearchSearchComponent);
            Assert.AreSame(SearchComponent, SearchComponent2);

            ISearchComponent SearchComponent3 = serviceFactory.CreateSearchComponent(DataSources.Flicker);
            Assert.IsTrue(SearchComponent3 is FlickerSearchSearchComponent);
            Assert.AreSame(SearchComponent, SearchComponent3);

            ISearchComponent SearchComponent4 = serviceFactory.CreateSearchComponent(DataSources.NewsAPI);
            Assert.IsTrue(SearchComponent4 is NewsAPISearchhComponent);

            ISearchComponent SearchComponent5 = serviceFactory.CreateSearchComponent(DataSources.NewsAPI);
            Assert.IsTrue(SearchComponent5 is NewsAPISearchhComponent);
            Assert.AreSame(SearchComponent5, SearchComponent4);

            ISearchComponentFactory serviceFactory6 = new SearchComponentFactory();
            ISearchComponent SearchComponent6 = serviceFactory.CreateSearchComponent(DataSources.Flicker);
            Assert.IsTrue(SearchComponent6 is FlickerSearchSearchComponent);
            Assert.AreSame(SearchComponent, SearchComponent6);
        }

        /// <summary>
        /// Tests the create factory  method to check if correct instance aare reurned during repeated calls
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public async Task Test_FlickerSearchComponent_Exception_Negative()
        {
            ISearchComponentFactory serviceFactory = new SearchComponentFactory();
            ISearchComponent SearchComponent = serviceFactory.CreateSearchComponent(DataSources.Flicker);
            IQueryContext queryContext = new QueryContext();
            queryContext.ApplicationConfiguration = null;
            queryContext.QueryParam = null;

            IResponseContext rc = await SearchComponent.PerformSearch(queryContext);
        }

        /// <summary>
        /// Tests the create factory  method to check if correct instance are returned during repeated calls
        /// </summary>
        [TestMethod]
        public async Task Test_FlickerSearchComponent_Positive()
        {
            IQueryContext qc = new QueryContext();
            qc.QueryParam = "Nature";
            qc.ApplicationConfiguration = new ApplicationConfiguration();
            IDataSource data = qc.ApplicationConfiguration.GetDataSource(DataSources.Flicker);

            ISearchComponent SearchComponent = new FlickerSearchSearchComponent(new MockHTTPAPIHelper("Test_FlickerSearchComponent_Positive"));
            IResponseContext rc = await SearchComponent.PerformSearch(qc);
            ImageResponseDataModel responseModel = rc as ImageResponseDataModel;
            Assert.IsNotNull(responseModel);

            Assert.AreEqual(responseModel.URI.Count, 1);
            Assert.IsNotNull(rc.Status);
        }

        /// <summary>
        /// Tests the create factory  method to check if correct instance are returned during repeated calls
        /// </summary>
        [TestMethod]
        public async Task Test_FlickerSearchComponent_Negative()
        {
            IQueryContext qc = new QueryContext();
            qc.QueryParam = "Nature";
            qc.ApplicationConfiguration = new ApplicationConfiguration();
            IDataSource data = qc.ApplicationConfiguration.GetDataSource(DataSources.Flicker);

            ISearchComponent SearchComponent = new FlickerSearchSearchComponent(new MockHTTPAPIHelper("Test_FlickerSearchComponent_Negative"));
            IResponseContext rc = await SearchComponent.PerformSearch(qc);
            ImageResponseDataModel responseModel = rc as ImageResponseDataModel;
            Assert.IsNotNull(responseModel);

            Assert.AreEqual(responseModel.URI.Count, 0);
            Assert.IsNotNull(rc.Status);
        }

        /// <summary>
        /// Tests the create factory  method to check if correct instance are returned during repeated calls
        /// </summary>
        [TestMethod]
        public async Task Test_NewsAPISearchComponent_Positive()
        {
            IQueryContext qc = new QueryContext();
            qc.QueryParam = "Nature";
            qc.ApplicationConfiguration = new ApplicationConfiguration();
            IDataSource data = qc.ApplicationConfiguration.GetDataSource(DataSources.NewsAPI);

            ISearchComponent SearchComponent = new NewsAPISearchhComponent(new MockHTTPAPIHelper("Test_NewsAPISearchComponent_Positive"));
            IResponseContext rc = await SearchComponent.PerformSearch(qc);
            TextResponseDataModel responseModel = rc as TextResponseDataModel;
            Assert.IsNotNull(responseModel);

            Assert.AreEqual(responseModel.NewsItems.Count, 1);
            Assert.IsNotNull(rc.Status);
        }

        /// <summary>
        /// Tests the create factory  method to check if correct instance are returned during repeated calls
        /// </summary>
        [TestMethod]
        public async Task Test_NewsAPISearchComponent_Negative()
        {
            IQueryContext qc = new QueryContext();
            qc.QueryParam = "Nature";
            qc.ApplicationConfiguration = new ApplicationConfiguration();
            IDataSource data = qc.ApplicationConfiguration.GetDataSource(DataSources.NewsAPI);

            ISearchComponent SearchComponent = new NewsAPISearchhComponent(new MockHTTPAPIHelper("Test_FlickerSearchComponent_Negative"));
            IResponseContext rc = await SearchComponent.PerformSearch(qc);
            TextResponseDataModel responseModel = rc as TextResponseDataModel;
            Assert.IsNotNull(responseModel);

            Assert.AreEqual(responseModel.NewsItems.Count, 0);
            Assert.IsNotNull(rc.Status);
        }

        #region Mock Classes

        private class MockHTTPAPIHelper: ICommunicationHelper
        {
            private static readonly string SampleFilePath_Flicker = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\FlickerSampleResponse.txt";
            private static readonly string SampleFilePath_NewsAPI = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\NewsAPISampleResponse.txt";
            private string m_currentTestCaseName;

            public MockHTTPAPIHelper(string testCaseName)
            {
                m_currentTestCaseName = testCaseName;
            }
            public async Task<IHttpAPIResponse> Get(string url)
            {
                IHttpAPIResponse sampleResponse = new HTTPAPIResponse();
                switch (m_currentTestCaseName)
                {
                    case "Test_FlickerSearchComponent_Positive":
                        sampleResponse.Code = ErrorCodes.NoError;
                        sampleResponse.ResponseString = File.ReadAllText(SampleFilePath_Flicker);
                        break;
                    case "Test_FlickerSearchComponent_Negative":
                    case "Test_NewsAPISearchComponent_Negative":
                        sampleResponse.Code = ErrorCodes.APIErrorResponse;
                        sampleResponse.ResponseString = "DummyResponse";
                        break;
                    case "Test_NewsAPISearchComponent_Positive":
                        sampleResponse.Code = ErrorCodes.NoError;
                        sampleResponse.ResponseString = File.ReadAllText(SampleFilePath_NewsAPI);
                        break;
                    case "MoveNext"://called by runtime to updte async-await statemachine!
                        break;
                    default:
                        Assert.Fail();
                        break;
                }
                
                return sampleResponse;
            }
        }

      
        #endregion Mock Classes
    }
}
