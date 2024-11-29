using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KanbanApp.Views.Home
{
    public class UserProfile : PageModel
    {
        private readonly ILogger<UserProfile> _logger;

        public UserProfile(ILogger<UserProfile> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }
    }
}
