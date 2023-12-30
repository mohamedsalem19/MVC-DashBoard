﻿using System.ComponentModel.DataAnnotations;

namespace MVC_Project.PL.ViewModels
{
    public class ResetPasswordViewModel
    {
        [Required(ErrorMessage = "Password is Required ")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Confirm Password is Required ")]
        [Compare("NewPassword", ErrorMessage = "Confirm Password does not match Password")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }



	}
}
