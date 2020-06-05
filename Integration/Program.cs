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

            var runner = new MainRunner();
            runner.RunOnce();
        }
    }
}
