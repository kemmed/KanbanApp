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
            var boards = _context.Board.Where(x => x.CreatorID == userLoggedInID || _context.UserBoard.Any(ub => ub.BoardID == x.ID && ub.UserID == userLoggedInID)).ToList();


            return View(boards);
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
            Column ToDoColumn = new Column();
            ToDoColumn.Name = "Сделать";
            ToDoColumn.BoardID = board.ID;
            _context.Add(ToDoColumn);

            Column InProcessColumn = new Column();
            InProcessColumn.Name = "В процессе";
            InProcessColumn.BoardID = board.ID;
            _context.Add(InProcessColumn);

            Column ReviewColumn = new Column();
            ReviewColumn.Name = "Проверяется";
            ReviewColumn.BoardID = board.ID;
            _context.Add(ReviewColumn);

            Column CompletedColumn = new Column();
            CompletedColumn.Name = "Выполнена";
            CompletedColumn.BoardID = board.ID;
            _context.Add(CompletedColumn);

            await _context.SaveChangesAsync();

            return Redirect("/");
        }

    }
}
