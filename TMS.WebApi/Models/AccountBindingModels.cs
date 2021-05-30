using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace TMS.WebApi.Models
{
    // Models used as parameters to AccountController actions.

    public class AddExternalLoginBindingModel
    {
        [Required]
        [Display(Name = "External access token")]
        public string ExternalAccessToken { get; set; }
    }

    public class ChangePasswordBindingModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class RegisterBindingModel
    {
       

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name ="Employee ID")]
        [RegularExpression(pattern: "^[0-9]+$", ErrorMessage = "Enter Only Numeric Value")]
        public int EmployeeId { get; set; }

        [Required]
        [Display(Name ="First Name")]
        [RegularExpression(pattern:"^[a-zA-Z]+$",ErrorMessage ="Enter Only Alphabetic Value")]
        public string EmployeeFirstName { get; set; }

        [Required]
        [Display(Name ="Last Name")]
        [RegularExpression(pattern: "^[a-zA-Z]+$", ErrorMessage = "Enter Only Alphabetic Value")]
        public string EmployeeLastName { get; set; }

        [Required]
        [Display(Name ="Location")]
        [RegularExpression(pattern: "^[a-zA-Z]+$", ErrorMessage = "Enter Only Alphabetic Value")]
        public string Location { get; set; }

        [Required]
        [Display(Name ="Account Number")]
        [RegularExpression(pattern: "^[0-9]{10}", ErrorMessage = "Account number should be of 10 Digits")]
        public int ReimbursementAccountNo { get; set; }
    }

    public class RegisterExternalBindingModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class RemoveLoginBindingModel
    {
        [Required]
        [Display(Name = "Login provider")]
        public string LoginProvider { get; set; }

        [Required]
        [Display(Name = "Provider key")]
        public string ProviderKey { get; set; }
    }

    public class SetPasswordBindingModel
    {
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
