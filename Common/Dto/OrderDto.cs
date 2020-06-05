using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Dto
{
    public class OrderDto
    {
        public int Id { get; set; }
        public DateTime DateAdd { get; set; }
        public ProductDto[] Products { get; set; }
    }
}
