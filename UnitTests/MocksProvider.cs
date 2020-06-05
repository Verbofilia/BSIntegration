using BaselinkerConnector.Dto;
using Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests
{
    public static class MocksProvider
    {

        public static Product GetProductMock(int i = 1)
        {
            return new Product { name = $"Product name {i}", quantity = 10 * i };
        }

        public static Order GetEmptyOrderMock()
        {
            return new Order { order_id = 1, date_add = DateTime.Now, shop_order_id = 11 };
        }

        public static Order GetFilledOrderMock()
        {
            return new Order
            {
                order_id = 1,
                date_add = DateTime.Now,
                shop_order_id = 11,
                products = new[]{
                GetProductMock(1),
                GetProductMock(2)
            }};
        }

        public static OrderDto GetEmptyOrderDtoMock()
        {
            return new OrderDto { Id = 1,  DateAdd = DateTime.Now };
        }
    }
}
