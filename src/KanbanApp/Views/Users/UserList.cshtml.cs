using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KanbanApp.Views.Users
{
    public class UserListModel : PageModel
    {
        private readonly ILogger<UserListModel> _logger;

        public UserListModel(ILogger<UserListModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }
    }
}
