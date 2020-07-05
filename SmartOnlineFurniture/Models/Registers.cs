using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmartOnlineFurniture.Models
{
    public class Registers
    {
        [Key]
        public int RegisterId { get; set; }
        [Required(ErrorMessage ="User Name is Required")]
        [Display(Name ="UserName")]
        [StringLength(maximumLength:15,MinimumLength =3,ErrorMessage ="Username Length must be max 15 & min 3")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Email is Required")]
        [EmailAddress(ErrorMessage = "Invalid Email")]
        [Remote("IsEmailExists", "Home", ErrorMessage = "Email already exists!")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "Invalid Email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Phone number is Required")]
        [Display(Name = "Phone Number")]
        [StringLength(maximumLength: 11, MinimumLength = 11, ErrorMessage = "Invalid Phone number")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "Address is Required")]
        public string Address { get; set; }
        [Required(ErrorMessage = "User Type is Required")]
        public string UserType { get; set; }
        [Required(ErrorMessage = "Password is Required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage = "Re-Password is Required")]
        [Display(Name ="Re-Password")]
        [DataType(DataType.Password)]
        [System.ComponentModel.DataAnnotations.Compare("Password")]
        public string RePassword { get; set; }
        [Required(ErrorMessage = "Gender is Required")]
        public char Gender { get; set; }
        [Required(ErrorMessage = "ProfileImage is Required")]
        public string UserImage { get; set; }

        //public HttpPostedFileBase ImageFile { get; set; }

    }
}