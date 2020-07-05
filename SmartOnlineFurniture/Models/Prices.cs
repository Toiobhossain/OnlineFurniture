using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SmartOnlineFurniture.Models
{
    public class Prices
    {
        [Key]
        public int PriceID { get; set; }
        public string CustomerName { get; set; }
        public int TotalPrice { get; set; }
        public int ProductQuantity { get; set; }
        public int PID { get; set; }
      
    }
}