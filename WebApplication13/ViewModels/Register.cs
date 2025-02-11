using System.ComponentModel.DataAnnotations;

namespace WebApplication13.ViewModels
{
    public class Register
    {
        [Required]
        [DataType(DataType.Text)]
        public string FullName { get; set; }

        [Required]
        [DataType(DataType.CreditCard)]
        public string CreditCardNo { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string Gender { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        public string MobileNumber { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)] 
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)] 
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "Password and confirmation password does not match")]
        public string ConfirmPassword { get; set; }

        //[Required]
        //[DataType(DataType.ImageUrl)]
        //public string Photo { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        public string AboutMe { get; set; }

    }
}
