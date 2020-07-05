using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmartOnlineFurniture.Models
{
    public class Categories
    {
        [Key]
        public int CategoryId { get; set; }
        [Required(ErrorMessage = "Category is required")]
        [Display(Name = "Category Name")]
        [Remote("IsCategoryExist", "Admin", ErrorMessage = "Category already exists!")]
        public string CategoryName { get; set; }
    }
}