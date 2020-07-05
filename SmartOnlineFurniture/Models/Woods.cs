using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SmartOnlineFurniture.Models
{
    public class Woods
    {
        [Key]
        public int WoodId { get; set; }
        public string WoodName { get; set; }
        public int Price { get; set; }
    }
}