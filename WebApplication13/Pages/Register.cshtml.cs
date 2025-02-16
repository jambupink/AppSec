using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication13.Model;
using WebApplication13.ViewModels;
using Newtonsoft.Json;
using Ganss.Xss;

namespace WebApplication13.Pages
{
    public class RegisterModel : PageModel
    {
        private UserManager<ApplicationUser> userManager { get; }
        private SignInManager<ApplicationUser> signInManager { get; }

        [BindProperty]
        public Register RModel { get; set; }

        public RegisterModel(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            this.userManager = userManager; 
            this.signInManager = signInManager;
        }
        private async Task<bool> ValidateRecaptcha()
        {
            var recaptchaResponse = Request.Form["g-recaptcha-response"];
            using HttpClient client = new();
            var response = await client.GetStringAsync($"https://www.google.com/recaptcha/api/siteverify?secret=6Lfv0NQqAAAAAAo5CBMDedlaAMbyfNyZ6XDmRm3y&response={recaptchaResponse}");
            return JsonConvert.DeserializeObject<RecaptchaResponse>(response).Success;
        }

        public void OnGet()
        {

        }

        //Save data into the database
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                if (!await ValidateRecaptcha())
                {
                    ModelState.AddModelError("", "CAPTCHA validation failed.");
                    return Page();
                }

                //Check email
                var existingUser = await userManager.FindByEmailAsync(RModel.Email);
                if (existingUser != null)
                {
                    ModelState.AddModelError("RModel.Email", "Email is already registered");
                    return Page();
                }
                ////handle photo
                //if (RModel.Photo == null || RModel.Photo.Length == 0)
                //{
                //    ModelState.AddModelError("RModel.Photo", "Photo is required.");
                //    return Page();
                //}

                //// Handle valid photo upload
                //var uploadsFolder = Path.Combine("wwwroot", "uploads");
                //if (!Directory.Exists(uploadsFolder))
                //{
                //    Directory.CreateDirectory(uploadsFolder);
                //}

                //var fileName = Guid.NewGuid().ToString() + Path.GetExtension(RModel.Photo.FileName);
                //var filePath = Path.Combine(uploadsFolder, fileName);

                //using (var stream = new FileStream(filePath, FileMode.Create))
                //{
                //    await RModel.Photo.CopyToAsync(stream);
                //}
                var sanitizer = new HtmlSanitizer();
                var user = new ApplicationUser
                {
                    UserName = RModel.Email,
                    Email = RModel.Email,
                    FullName = RModel.FullName,
                    CreditCardNo = RModel.CreditCardNo, 
                    Gender = RModel.Gender,
                    MobileNumber = RModel.MobileNumber,
                    DeliveryAddress = RModel.DeliveryAddress,
                    //PhotoPath = $"/uploads/{fileName}",
                    AboutMe = sanitizer.Sanitize(RModel.AboutMe)

                };
                var result = await userManager.CreateAsync(user, RModel.Password); 
                
                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(user, false); 
                    return RedirectToPage("Index");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return Page();
        }

    }
}
