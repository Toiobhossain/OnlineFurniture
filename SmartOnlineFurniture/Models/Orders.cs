using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SmartOnlineFurniture.Models
{
    public class Orders
    {
        [Key]
        public int orderId { get; set; }
        public string CustomerName { get; set; }
        public int ProductPrice { get; set; }
        public int Quantity { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string PaymentMethod { get; set; }
        public int ProductID { get; set; }
    }
}