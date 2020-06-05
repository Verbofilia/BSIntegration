using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaselinkerConnector.Dto
{
    public class Product
    {
        public string name { get; set; }
        public int quantity { get; set; }

        public decimal price_brutto { get; set; }
    }
}
