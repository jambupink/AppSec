using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication13.Model;

namespace WebApplication13.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly UserManager<ApplicationUser> _userManager;

        public IndexModel(
            ILogger<IndexModel> logger,
            UserManager<ApplicationUser> userManager)
        {
            _logger = logger;
            _userManager = userManager;
        }

        public ApplicationUser CurrentUser { get; set; }

        public async Task OnGet()
        {
            if (User.Identity.IsAuthenticated)
            {
                CurrentUser = await _userManager.GetUserAsync(User);
            }
        }
    }
}
