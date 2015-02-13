using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using ToDo.WebUI.Models;

namespace ToDo.WebUI.Validators
{
    public class DoublePasswordCheck : ValidationAttribute
    {
        public DoublePasswordCheck()
            : base("Password do not match")
        {
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var model = (RegisterModel)validationContext.ObjectInstance;


            return model.ConfirmPassword == model.Password ? ValidationResult.Success : new ValidationResult("Password do not match");
        }
    }
}