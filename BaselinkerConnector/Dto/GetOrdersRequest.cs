using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaselinkerConnector.Dto
{
    public class GetOrdersRequest
    {
        public int order_id { get; set; }
        public int date_confirmed_from { get; set; }
        public int date_from { get; set; }
        public int id_from { get; set; }
        public bool get_unconfirmed_orders { get; set; }
        public int status_id { get; set; }
        public string filter_email { get; set; }
    }
}
