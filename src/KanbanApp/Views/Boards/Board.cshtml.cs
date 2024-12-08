using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KanbanApp.Views.Users
{
    public class BoardModel : PageModel
    {
        private readonly ILogger<BoardModel> _logger;

        public BoardModel(ILogger<BoardModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }
    }
}
