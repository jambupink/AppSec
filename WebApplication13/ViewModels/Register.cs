using System.ComponentModel.DataAnnotations;

namespace WebApplication13.ViewModels
{
    public class Register
    {
        [Required(ErrorMessage = "Full name is required")]
        [DataType(DataType.Text)]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Full name must be between 2-100 characters")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Credit card number is required")  ]
        [DataType(DataType.CreditCard, ErrorMessage = "Invalid credit card number")]
        [RegularExpression(@"^\d{13,19}$", ErrorMessage = "Invalid credit card number")]
        public string CreditCardNo { get; set; }

        [Required(ErrorMessage = "Gender is required")]
        [DataType(DataType.Text)]
        [RegularExpression(@"^(Male|Female|Other)$", ErrorMessage = "Invalid gender selection, only (Male,Female,Other)")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "Mobile number is required")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\d{8}$", ErrorMessage = "Invalid phone number format, only 8 digit")]
        public string MobileNumber { get; set; }

        [Required(ErrorMessage = "Email address is required")]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Invalid email format")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 12, ErrorMessage = "Password must be at least 12 characters")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{12,}$", ErrorMessage = "Password must contain uppercase, lowercase, number, and special character")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "Passwords does not match")]
        public string ConfirmPassword { get; set; }

        //[Required]
        //[DataType(DataType.ImageUrl)]
        //public string Photo { get; set; }

        [Required(ErrorMessage = "About Me is required")]
        [DataType(DataType.MultilineText)]
        [StringLength(500, ErrorMessage = "About Me cannot exceed 500 characters")]
        public string AboutMe { get; set; }

    }
}
