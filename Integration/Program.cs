using BaselinkerConnector;
using SubiektConnector;
using Common.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integration
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {


            var mi = new MigratorService(new BaselinkerClient(), new SubiektClient());

            var resc = mi.TestConnections();

            var c = new BaselinkerClient();
         //   var res3 = c.GetOrder(6561127).Result;
            var s = c.TestConnection().Result;
            var res = c.GetOrder(6534119).Result;

            var sc = new SubiektConnector.SubiektClient();
         //   var rr = sc.Test(res).Result;
        }
    }
}
