using Microsoft.AspNetCore.Mvc;

namespace KanbanApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            if (true)
                return RedirectToAction("Authorization");
            return View();
        }

        public IActionResult Authorization()
        {
            return View();
        }
        public IActionResult Registration()
        {
            return View();
        }

        public IActionResult UserProfile()
        {
            return View();
        }
    }
}
