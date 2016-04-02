using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LogGrabber.Web.ViewModels
{
    public class RegisterViewModel : IValidatableObject
    {
        [Required(ErrorMessage = "Please enter the user's name.")]
        [StringLength(32, ErrorMessage = "The name must be less than {1} characters.")]
        [Display(Name = "User name:")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Please enter the password.")]
        [StringLength(32, ErrorMessage = "The password must be less than {1} characters.")]
        [Display(Name = "Password:")]
        public string Password { get; set; }


        [Required(ErrorMessage = "Please confirm the password.")]
        [StringLength(32, ErrorMessage = "The Last Name must be less than {1} characters.")]
        [Display(Name = "Confirm password:")]
        public string ConfirmPassword { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Password != ConfirmPassword)
            {
                yield return new ValidationResult("Password does not match the confirmation");


            }
        }
    }
}
