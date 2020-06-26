using System;
using ImageSearch.Common;
using ImageSearch.DataModel;
using ImageSearch.DataModel.Contracts;
using ImageSearch.ServiceComponent;
using ImageSearch.ServiceComponent.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests.ServiceComponent
{
    [TestClass]
    public class TestServiceComponents
    { 
        /// <summary>
        /// Tests the create factory  method to check if correct instance aare reurned during repeated calls
        /// </summary>
        [TestMethod]
        public void Test_CreateSingleton_Positive()
        {
            AbstractServiceComponentFactory serviceFactory = new ServiceComponentFactory();
            IServiceComponent serviceComponent = serviceFactory.CreateSingleton(DataSources.Flicker);
            Assert.IsTrue(serviceComponent is FlickerSearchServiceComponent);

            IServiceComponent serviceComponent2 = serviceFactory.CreateSingleton(DataSources.Flicker);
            Assert.IsTrue(serviceComponent2 is FlickerSearchServiceComponent);
            Assert.AreSame(serviceComponent, serviceComponent2);

            IServiceComponent serviceComponent3 = serviceFactory.CreateSingleton(DataSources.Flicker);
            Assert.IsTrue(serviceComponent3 is FlickerSearchServiceComponent);
            Assert.AreSame(serviceComponent, serviceComponent3);

            IServiceComponent serviceComponent4 = serviceFactory.CreateSingleton(DataSources.NewsAPI);
            Assert.IsTrue(serviceComponent4 is NewsAPISearchServiceComponent);

            IServiceComponent serviceComponent5 = serviceFactory.CreateSingleton(DataSources.NewsAPI);
            Assert.IsTrue(serviceComponent5 is NewsAPISearchServiceComponent);
            Assert.AreSame(serviceComponent5, serviceComponent4);

            AbstractServiceComponentFactory serviceFactory6 = new ServiceComponentFactory();
            IServiceComponent serviceComponent6 = serviceFactory.CreateSingleton(DataSources.Flicker);
            Assert.IsTrue(serviceComponent6 is FlickerSearchServiceComponent);
            Assert.AreSame(serviceComponent, serviceComponent6);
        }

        /// <summary>
        /// Tests the create factory  method to check if correct instance aare reurned during repeated calls
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Invalid argument passed to method FlickerSearchServiceComponent.PerformSearch")]
        public void Test_FlickerServiceComponent_Negative()
        {
            AbstractServiceComponentFactory serviceFactory = new ServiceComponentFactory();
            IServiceComponent serviceComponent = serviceFactory.CreateSingleton(DataSources.Flicker);
            IQueryContext queryContext = new QueryContext();
            queryContext.ApplicationConfiguration = null;
            queryContext.QueryParam = null;

            serviceComponent.PerformSearch(queryContext);
        }

        }
}
