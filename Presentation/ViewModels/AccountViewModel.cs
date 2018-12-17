using System.ComponentModel.DataAnnotations;

namespace Presentation.ViewModels
{
    public class AccountViewModel
    {

        public LoginViewModel LoginViewModel { get; set; }
        public RegisterViewModel RegisterViewModel { get; set; }

    }


    public class LoginViewModel
    {
        [Display(Name = "Email")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Email is required")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }


    }

    public class RegisterViewModel
    {
   

        [Display(Name = "CNP")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "CNP is required")]
        public string CNP { get; set; }

        [Display(Name = "Email")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Email is required")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}
