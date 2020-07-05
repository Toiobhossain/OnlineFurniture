using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartOnlineFurniture.Models
{
    public class Stocks
    {
        public int StockId { get; set; }
        public int CompanyId { get; set; }
        public int CategoryId { get; set; }
        public int ProductId { get; set; }

    }
}