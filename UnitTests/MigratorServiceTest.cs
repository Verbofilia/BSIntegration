using System;
using System.Linq;
using System.Threading.Tasks;
using BaselinkerConnector;
using BaselinkerConnector.Dto;
using Common.Dto;
using Common.Interfaces;
using Common.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace UnitTests
{
    [TestClass]
    public class MigratorServiceTest
    {
        private static IMigrator migrator;

        [ClassInitialize]
        public static void Initialize(TestContext context)
        {
            var sourceMock = new Mock<ISourceConnector>();
            sourceMock.Setup(m => m.GetOrder(It.IsAny<int>()))
                .Returns(Task.FromResult( MocksProvider.GetEmptyOrderDtoMock()));
            var targetMock = new Mock<ITargetConnector>();
            targetMock.Setup(m => m.SubmitOrder(It.IsAny<OrderDto>()))
                .Returns(Task.FromResult(true));
            migrator = new MigratorService(sourceMock.Object, targetMock.Object);
        }

        [TestMethod]
        public async Task ProductTest()
        {
            var res = await migrator.MigrateOrder(1);
            Assert.IsNotNull(res);
            Assert.IsNull(res.ErrorMessage);
            Assert.AreEqual(true, res.IsSuccess);
        }


    }
}
