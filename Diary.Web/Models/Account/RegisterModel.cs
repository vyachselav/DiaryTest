using System.ComponentModel.DataAnnotations;

namespace Diary.Web.Models.Account
{
    public class RegisterModel
    {
        [Required]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Incorrect email adress")]
        public string Email { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [MinLength(6, ErrorMessage = "Your password must be at least 6 characters")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        [DataType(DataType.Password)]
        public string PasswordConfirm { get; set; }
    }
}