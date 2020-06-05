using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaselinkerConnector.Dto
{
    public class GetOrdersResponse:BaseResponse
    {
        public Order[] orders;
    }
}
