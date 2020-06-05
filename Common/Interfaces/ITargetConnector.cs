using Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Interfaces
{
    public interface ITargetConnector:IBaseConnector
    {
        Task<bool> SubmitOrder(OrderDto order);
    }
}
