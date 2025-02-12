using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication13.Model;
using WebApplication13.ViewModels;

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

        public void OnGet()
        {

        }
        //Save data into the database
        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
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

                var user = new ApplicationUser
                {
                    UserName = RModel.Email,
                    Email = RModel.Email,
                    FullName = RModel.FullName,
                    CreditCardNo = RModel.CreditCardNo,  // Will encrypt later
                    Gender = RModel.Gender,
                    MobileNumber = RModel.MobileNumber,
                    DeliveryAddress = RModel.DeliveryAddress,
                    //PhotoPath = $"/uploads/{fileName}",
                    AboutMe = RModel.AboutMe

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
