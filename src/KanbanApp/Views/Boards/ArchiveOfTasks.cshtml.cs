using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KanbanApp.Views.Users
{
    public class ArchiveOfTasksModel : PageModel
    {
        private readonly ILogger<ArchiveOfTasksModel> _logger;

        public ArchiveOfTasksModel(ILogger<ArchiveOfTasksModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }
    }
}
