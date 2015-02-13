using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using ToDo.WebUI.Validators;

namespace ToDo.WebUI.Models
{
    [DoublePasswordCheck()]
    public class RegisterModel
    {
        [DisplayName("Username")]
        [Required(ErrorMessage = "Please enter Username")]
        public string Username { set; get; }

        [DisplayName("Password")]
        [Required(ErrorMessage = "Please enter password")]
        public string Password { set; get; }

        [DisplayName("Confirm Password")]
        [Required(ErrorMessage = "Please repeat password")]
        public string ConfirmPassword { set; get; }

        [DisplayName("E-Mail")]
        [Required(ErrorMessage = "Please repeat email")]
        [RegularExpression(@".+\@.+\..+", ErrorMessage = "Please enter a valid email address")]
        public string Mail { set; get; }

    }
}