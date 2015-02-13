using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ToDo.WebUI.Models
{
    public class LoginModel
    {
        [DisplayName("Username")]
        [Required(ErrorMessage = "Please enter Username")]
        public string Username { set; get; }

        [DisplayName("Password")]
        [Required(ErrorMessage = "Please enter password")]
        public string Password { set; get; }

        public bool IsPersistent { set; get; }
    }
}