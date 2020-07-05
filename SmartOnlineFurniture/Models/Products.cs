using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmartOnlineFurniture.Models
{
    public class Products
    {
        [Key]
        public int ProductId { get; set; }
        [Required(ErrorMessage = "Company Name is required")]
        [Display(Name = "Company Name")]
        public int CompanyId { get; set; }
        [ForeignKey("CompanyId")]
        public virtual Companies Companies { get; set; }
        [Required(ErrorMessage = "Category is required")]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public virtual Categories Categories { get; set; }
        [Required(ErrorMessage = "Product Name is required")]
        [Display(Name = "Product Name")]
        [Remote("IsProductNameExist", "Products", ErrorMessage = "Product Name already exists!")]
        public string ProductName { get; set; }
        [Display(Name = "Quantity")]
        public int Quantity { get; set; }
        [Required(ErrorMessage = "Price is required")]
        [Display(Name = "Price")]
        public int Price { get; set; }
        [Required(ErrorMessage = "Product Details is required")]
        [Display(Name = "Product Details")]
        public string ProductDetails { get; set; }
        [Required(ErrorMessage = "Product Image is required")]
        [Display(Name = "Product Image")]
        public string ProductImage { get; set; }
    }
}