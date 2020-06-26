using System;
using System.Threading.Tasks;
using ImageSearch.Common;
using ImageSearch.DataModel;
using ImageSearch.DataModel.Contracts;
using ImageSearch.ServiceComponent;
using ImageSearch.ServiceComponent.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NMock;

namespace UnitTests.ServiceComponent
{
    [TestClass]
    public class TestServiceComponents
    {
        /// <summary>
        /// Tests the create factory  method to check if correct instance are returned during repeated calls
        /// </summary>
        [TestMethod]
        public void Test_CreateServiceComponent_Positive()
        {
            IServiceComponentFactory serviceFactory = new ServiceComponentFactory();
            IServiceComponent serviceComponent = serviceFactory.CreateServiceComponent(DataSources.Flicker);
            Assert.IsTrue(serviceComponent is FlickerSearchServiceComponent);

            IServiceComponent serviceComponent2 = serviceFactory.CreateServiceComponent(DataSources.Flicker);
            Assert.IsTrue(serviceComponent2 is FlickerSearchServiceComponent);
            Assert.AreSame(serviceComponent, serviceComponent2);

            IServiceComponent serviceComponent3 = serviceFactory.CreateServiceComponent(DataSources.Flicker);
            Assert.IsTrue(serviceComponent3 is FlickerSearchServiceComponent);
            Assert.AreSame(serviceComponent, serviceComponent3);

            IServiceComponent serviceComponent4 = serviceFactory.CreateServiceComponent(DataSources.NewsAPI);
            Assert.IsTrue(serviceComponent4 is NewsAPISearchServiceComponent);

            IServiceComponent serviceComponent5 = serviceFactory.CreateServiceComponent(DataSources.NewsAPI);
            Assert.IsTrue(serviceComponent5 is NewsAPISearchServiceComponent);
            Assert.AreSame(serviceComponent5, serviceComponent4);

            IServiceComponentFactory serviceFactory6 = new ServiceComponentFactory();
            IServiceComponent serviceComponent6 = serviceFactory.CreateServiceComponent(DataSources.Flicker);
            Assert.IsTrue(serviceComponent6 is FlickerSearchServiceComponent);
            Assert.AreSame(serviceComponent, serviceComponent6);
        }

        /// <summary>
        /// Tests the create factory  method to check if correct instance aare reurned during repeated calls
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public async Task Test_FlickerServiceComponent_Negative()
        {
            IServiceComponentFactory serviceFactory = new ServiceComponentFactory();
            IServiceComponent serviceComponent = serviceFactory.CreateServiceComponent(DataSources.Flicker);
            IQueryContext queryContext = new QueryContext();
            queryContext.ApplicationConfiguration = null;
            queryContext.QueryParam = null;

            IResponseContext rc = await serviceComponent.PerformSearch(queryContext);
        }

        /// <summary>
        /// Tests the create factory  method to check if correct instance are returned during repeated calls
        /// </summary>
        [TestMethod]
        public async Task Test_FlickerServiceComponent_Positive()
        {
            var mockHttpHelper = new MockFactory().CreateMock<IHttpRestAPIHelper>();
            IQueryContext qc = new QueryContext();
            qc.QueryParam = "Nature";
            qc.ApplicationConfiguration = new ApplicationConfiguration();
            IDataSource data = qc.ApplicationConfiguration.GetDataSource(DataSources.Flicker);

            var mockHttpResponse = new MockFactory().CreateMock<IHttpAPIResponse>();
            Task<IHttpAPIResponse> mockHttpResponseTask = new Task<IHttpAPIResponse>(MockCallback);
            mockHttpHelper.Expects.One.Method(s => s.Get(data.DataSourceURI)).WillReturn(mockHttpResponseTask);

            IServiceComponentFactory serviceFactory = new ServiceComponentFactory();
            IServiceComponent serviceComponent = new FlickerSearchServiceComponent();
            IResponseContext rc = await serviceComponent.PerformSearch(qc);

            Assert.IsNotNull(rc);
            Assert.IsNotNull(rc.Status);
        }

        /// <summary>
        /// Tests the create factory  method to check if correct instance are returned during repeated calls
        /// </summary>
        [TestMethod]
        public async Task Test_NewsAPIServiceComponent_Positive()
        {
            var mockHttpHelper = new MockFactory().CreateMock<IHttpRestAPIHelper>();
            IQueryContext qc = new QueryContext();
            qc.QueryParam = "Nature";
            qc.ApplicationConfiguration = new ApplicationConfiguration();
            IDataSource data = qc.ApplicationConfiguration.GetDataSource(DataSources.NewsAPI);

            var mockHttpResponse = new MockFactory().CreateMock<IHttpAPIResponse>();
            Task<IHttpAPIResponse> mockHttpResponseTask = new Task<IHttpAPIResponse>(MockCallback);
            mockHttpHelper.Expects.One.Method(s => s.Get(data.DataSourceURI)).WillReturn(mockHttpResponseTask);

            IServiceComponentFactory serviceFactory = new ServiceComponentFactory();
            IServiceComponent serviceComponent = new NewsAPISearchServiceComponent();
            IResponseContext rc = await serviceComponent.PerformSearch(qc);

            Assert.IsNotNull(rc);
            Assert.IsNotNull(rc.Status);
        }

        private IHttpAPIResponse MockCallback()
        {
            var mockHttpResponse = new MockFactory().CreateMock<IHttpAPIResponse>();
            Assert.IsNotNull(mockHttpResponse);
            return mockHttpResponse as IHttpAPIResponse;
        }
    }
}
