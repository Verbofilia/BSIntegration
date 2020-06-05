using Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Interfaces
{
    public interface IMigrator
    {
        Task<OperationResult> MigrateOrder(int id);
        OperationResult TestConnections();
    }
}
