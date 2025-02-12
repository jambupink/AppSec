using Microsoft.AspNetCore.Identity;

namespace WebApplication13.Model
{
    public class ApplicationUser : IdentityUser
    {
        [PersonalData]
        public string FullName { get; set; }

        [PersonalData]
        public string CreditCardNo { get; set; }  // Will encrypt this later

        [PersonalData]
        public string Gender { get; set; }

        [PersonalData]
        public string MobileNumber { get; set; }

        [PersonalData]
        public string DeliveryAddress { get; set; }

        //public string PhotoPath { get; set; }  // Stores path to uploaded photo

        [PersonalData]
        public string AboutMe { get; set; }
    }
}
