using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmartOnlineFurniture.Models
{
    public class Companies
    {
        [Key]
        public int CompanyId { get; set; }
        [Required(ErrorMessage = "Company Name is required")]
        [Display(Name = "Company Name")]
        [Remote("IsCompanyNameExist", "Admin", ErrorMessage = "Name already exists!")]
        public string CompanyName { get; set; }
    }
}