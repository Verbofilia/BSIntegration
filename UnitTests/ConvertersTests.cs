using System;
using System.Linq;
using BaselinkerConnector;
using BaselinkerConnector.Dto;
using Common.Dto;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    [TestClass]
    public class ConvertersTests
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
            var sourceProduct = GetProductMock();
            var targetProduct = connector.Convert<Product, ProductDto>(sourceProduct);
            Assert.IsNotNull(targetProduct);
            Assert.AreEqual(sourceProduct.name, targetProduct.Name);
            Assert.AreEqual(sourceProduct.quantity, targetProduct.Quantity);
        }

        [TestMethod]
        public void EmptyOrderTest()
        {
            var sourceOrder = GetOrderMock();
            var targetOrder = connector.Convert<Order, OrderDto>(sourceOrder);
            Assert.IsNotNull(targetOrder);
            Assert.AreEqual(sourceOrder.order_id, targetOrder.Id);
            Assert.AreEqual(sourceOrder.date_add, targetOrder.DateAdd);
            Assert.IsNull(sourceOrder.products);
        }

        [TestMethod]
        public void FilledOrderTest()
        {
            var sourceOrder = GetOrderMock();
            sourceOrder.products = new[]
            {
                GetProductMock(11),
                GetProductMock(22)
            };
            var targetOrder = connector.Convert<Order, OrderDto>(sourceOrder);
            Assert.IsNotNull(targetOrder);
            Assert.IsNotNull(sourceOrder.products);  
            Assert.AreEqual(2, targetOrder.Products.Length);
        }

        private Product GetProductMock(int i = 1)
        {
            return new Product { name = $"Product name {i}", quantity = 10 * i };
        }

        private Order GetOrderMock()
        {
            return new Order { order_id = 1, date_add = DateTime.Now, shop_order_id = 11 };
        }

    }
}
