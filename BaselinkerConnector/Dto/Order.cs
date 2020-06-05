using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaselinkerConnector.Dto
{
    public class Order
    {
        public int order_id { get; set; }
        public int? shop_order_id { get; set; }

        [JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTime date_add { get; set; }

        public Product[] products { get; set; }
    }
}
