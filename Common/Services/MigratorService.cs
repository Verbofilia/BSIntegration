using Common.Dto;
using Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Common.Services
{
    public class MigratorService:IMigrator
    {
        ISourceConnector _source;
        ITargetConnector _target;
        public MigratorService(ISourceConnector source, ITargetConnector target)
        {
            _source = source;
            _target = target;
        }

        public async Task<OperationResult> MigrateOrder(int id)
        {
            try
            {
                var order = await _source.GetOrder(id);
                var res = await _target.SubmitOrder(order);
                return new OperationResult(res);
            }
            catch (Exception e)
            {
                return new OperationResult(false, e.Message);
            }
        }

        public OperationResult TestConnections()
        {
            int failed = 0;
            IBaseConnector[] connectors = new IBaseConnector[]
            {
                _source,
                _target
            };
            var tasks = connectors.Select(conn => Task.Run(async () =>
             {
                 var checkRes = await _source.TestConnection();
                 if (!checkRes)
                 {
                     Interlocked.Increment(ref failed);
                 }
             })).ToArray();

            var res = Task.WhenAll(tasks);
            res.Wait();
            return new OperationResult(failed == 0);
        }
    }
}
