using System;
using System.Linq;
using BaselinkerConnector;
using BaselinkerConnector.Dto;
using Common.Dto;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    [TestClass]
    public class BaselinkerConvertersTests
    {
        private static BaselinkerClient connector;

        [ClassInitialize]
        public static void Initialize(TestContext context)
        {
            connector = new BaselinkerClient();
        }

        [TestMethod]
        public void ProductTest()
        {
            var sourceProduct = MocksProvider.GetProductMock();
            var targetProduct = connector.Convert<Product, ProductDto>(sourceProduct);
            Assert.IsNotNull(targetProduct);
            Assert.AreEqual(sourceProduct.name, targetProduct.Name);
            Assert.AreEqual(sourceProduct.quantity, targetProduct.Quantity);
        }

        [TestMethod]
        public void EmptyOrderTest()
        {
            var sourceOrder = MocksProvider.GetEmptyOrderMock();
            var targetOrder = connector.Convert<Order, OrderDto>(sourceOrder);
            Assert.IsNotNull(targetOrder);
            Assert.AreEqual(sourceOrder.order_id, targetOrder.Id);
            Assert.AreEqual(sourceOrder.date_add, targetOrder.DateAdd);
            Assert.IsNull(sourceOrder.products);
        }

        [TestMethod]
        public void FilledOrderTest()
        {
            var sourceOrder = MocksProvider.GetFilledOrderMock();
            var targetOrder = connector.Convert<Order, OrderDto>(sourceOrder);
            Assert.IsNotNull(targetOrder);
            Assert.IsNotNull(sourceOrder.products);  
            Assert.AreEqual(2, targetOrder.Products.Length);
        }

    }
}
