using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApplication13.Model;
using WebApplication13.ViewModels;

namespace WebApplication13.Pages
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public Login LModel { get; set; }

        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly AuthDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public LoginModel(
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager,
            AuthDbContext context)
        {
            this.signInManager = signInManager;
            _userManager = userManager;
            _context = context;
        }


        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var userId = _userManager.GetUserId(User); // Get actual user ID
                var existingSessions = _context.UserSessions.Where(us => us.UserId == userId).ToList();
                if (existingSessions.Count >= 1) // Allow only 1 session
                {
                    ModelState.AddModelError("", "Multiple logins detected.");
                    return Page();
                }
                var identityResult = await signInManager.PasswordSignInAsync(LModel.Email, LModel.Password, LModel.RememberMe, false);
                if (identityResult.Succeeded)
                {
                    return RedirectToPage("Index");
                }
                ModelState.AddModelError("", "Username or Password incorrect");
            }
            return Page();
        }

    }
}
