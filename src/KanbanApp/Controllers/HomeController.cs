using KanbanApp.Data;
using KanbanApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KanbanApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly KanbanAppContext _context;

        public HomeController(KanbanAppContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            int? userLoggedInID = HttpContext.Session.GetInt32("UserID");
            if (userLoggedInID == null)
                return Redirect("/Users/Authorization");

            return View(await _context.Board.ToListAsync());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateBoard([Bind("ID,Name")] Board board)
        {
            int? userSessionID = HttpContext.Session.GetInt32("UserID");

            User creator = _context.User.FirstOrDefault(x => x.ID == userSessionID);
            board.CreatorUser = creator;
            board.CreatorID = creator.ID;
            _context.Add(board);

            await _context.SaveChangesAsync();

            UserBoard userBoard = new UserBoard();
            userBoard.BoardID = board.ID;
            userBoard.Board = board;
            userBoard.UserID = creator.ID;
            userBoard.User = creator;
            userBoard.UserRole = UserRoles.Admin;
            _context.Add(userBoard);

            await _context.SaveChangesAsync();
            return Redirect("/");
        }

    }
}
