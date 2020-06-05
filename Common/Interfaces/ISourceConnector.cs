using Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Interfaces
{
    public interface ISourceConnector
    {
        Task<bool> TestConnection();
        Task<OrderDto> GetOrder(int orderId);
    }
}
