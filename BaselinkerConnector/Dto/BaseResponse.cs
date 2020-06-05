using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaselinkerConnector.Dto
{
    public class BaseResponse
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public Status status { get; set; }
        public string error_code { get; set; }
        public string error_message { get; set; }
    }
}
