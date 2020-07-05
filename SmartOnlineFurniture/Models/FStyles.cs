using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SmartOnlineFurniture.Models
{
    public class FStyles
    {
        [Key]
        public int StyleId { get; set; }
        public string StyleName { get; set; }
    }
}