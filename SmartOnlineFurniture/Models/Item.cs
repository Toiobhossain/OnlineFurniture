using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SmartOnlineFurniture.Models;

namespace SmartOnlineFurniture.Models
{
    public class Item
    {
        public Products Product { get; set; }
        public int Quantity { get; set; }
    }
}