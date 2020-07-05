using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SmartOnlineFurniture.Models
{
    public class DrawingOrders
    {
        [Key]
        public int DrawingId { get; set; }
        [Required(ErrorMessage = "Wood is required")]
        [Display(Name = "Wood Name")]
        public int WoodId { get; set; }
        [ForeignKey("WoodId")]
        public virtual Woods woods { get; set; }

        [Required(ErrorMessage = "Style is required")]
        [Display(Name = "Style Name")]
        public int StyleId { get; set; }
        [ForeignKey("StyleId")]
        public virtual FStyles fStyles { get; set; }
        [Required(ErrorMessage = "Height is required")]
        [Range(4.0, 9.0, ErrorMessage = "Credit must be within 4.0 to 9.0")]
        public double Height { get; set; }
        [Required(ErrorMessage = "Width is required")]
        [Range(4.0, 9.0, ErrorMessage = "Credit must be within 4.0 to 9.0")]
        public double Width { get; set; }
        [Required(ErrorMessage = "Quantity is required")]
        public int Quantity { get; set; }
        [Required(ErrorMessage = "Price is required")]
        public int Price { get; set; }
        [Required(ErrorMessage = "Product Details is required")]
        public string Details { get; set; }
        public string Image { get; set; }

        public string Status { get; set; }
        public string Name { get; set; }



    }
}