using System;
using System.Threading.Tasks;
using BaselinkerConnector;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IntegrationTests
{
    [TestClass]
    public class BaselinkerConnectorTests
    {
        private static BaselinkerClient connector;

        [ClassInitialize]
        public static void Initialize(TestContext context)
        {
            connector = new BaselinkerClient();
        }

        [TestMethod]
        public async Task TestBaselinkerConnection()
        {
            var res = await connector.TestConnection();
            Assert.AreEqual(true, res);
        }
    }
}
