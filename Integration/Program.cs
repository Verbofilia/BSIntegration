using BaselinkerConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integration
{
    class Program
    {
        static void Main(string[] args)
        {
            var c = new BaselinkerClient();
            var s = c.TestConnection().Result;
            var res = c.GetOrder(6534119).Result;
        }
    }
}
