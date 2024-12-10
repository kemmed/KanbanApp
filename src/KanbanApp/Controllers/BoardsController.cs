using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KanbanApp.Data;
using KanbanApp.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Column = KanbanApp.Models.Column;

namespace KanbanApp.Controllers
{
    public class BoardsController : Controller
    {
        private readonly KanbanAppContext _context;

        public BoardsController(KanbanAppContext context)
        {
            _context = context;
        }

        // GET: Boards
        //public async Task<IActionResult> Index()
        //{
        //    //return View(await _context.Board.ToListAsync());
        //}

        // GET: Boards/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var board = await _context.Board
                .FirstOrDefaultAsync(m => m.ID == id);
            if (board == null)
            {
                return NotFound();
            }

            return View(board);
        }

        // GET: Boards/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Boards/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("ID,Name,CreatorID")] Board board)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(board);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(board);
        //}

        // GET: Boards/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var board = await _context.Board.FindAsync(id);
            if (board == null)
            {
                return NotFound();
            }
            return View(board);
        }

        // POST: Boards/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("ID,Name,CreatorID")] Board board)
        //{
        //    if (id != board.ID)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(board);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!BoardExists(board.ID))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(board);
        //}

        // GET: Boards/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var board = await _context.Board
                .FirstOrDefaultAsync(m => m.ID == id);
            if (board == null)
            {
                return NotFound();
            }

            return View(board);
        }

        // POST: Boards/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var board = await _context.Board.FindAsync(id);
        //    if (board != null)
        //    {
        //        _context.Board.Remove(board);
        //    }

        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        private bool BoardExists(int id)
        {
            return _context.Board.Any(e => e.ID == id);
        }

        [HttpGet("Boards/Board/{boardID}")]
        public async Task<IActionResult> Board(int boardID)
        {
            List<Column> columns = _context.Column.Where(x => x.BoardID == boardID).Include(x=>x.IssueColumns).ToList();
            ViewBag.BoardName = _context.Board.FirstOrDefault(x => x.ID == boardID).Name;
            ViewBag.BoardID = _context.Board.FirstOrDefault(x => x.ID == boardID).ID;

            columns.ForEach(x => x.IssueColumns.ForEach(y => y.Issue = _context.Issue.Include(w => w.Performer).Include(w => w.Creator).FirstOrDefault(z => z.ID == y.IssueID)));

            var reponsibilities = _context.UserBoard.Where(x => x.BoardID == boardID)
                .Select(c => new SelectListItem
                {
                    Value = c.UserID.ToString(),
                    Text = c.User.Email
                }).ToList();

            ViewBag.Responsibilities = reponsibilities;

            return View(columns);
        }
        [HttpPost]
        public IActionResult CreateColumn(IFormCollection formData)
        {
            Column newColumn = new Column();
            newColumn.Name = "Новая колонка";
            newColumn.BoardID = int.Parse(formData["boardID"]);
            _context.Add(newColumn);
            _context.SaveChanges();
            return RedirectToAction("Board", new { boardID = int.Parse(formData["boardID"]) });
        }

        [HttpPost]
        public IActionResult CreateTask([Bind("ID, Name, Description, DeadlineDate, Status, Priority, PerformerID")] Issue issue, IFormCollection formData)
        {
            int? userSessionID = HttpContext.Session.GetInt32("UserID");

            User creator = _context.User.FirstOrDefault(x => x.ID == userSessionID);

            Issue newIssue = new Issue();
            newIssue.Name = issue.Name;
            newIssue.Description = issue.Description ?? "";
            newIssue.DeadlineDate = issue.DeadlineDate;
            newIssue.Status = issue.Status;
            newIssue.Priority = issue.Priority;
            newIssue.PerformerID = issue.PerformerID;
            newIssue.CreatorID = (int)userSessionID;
            _context.Add(newIssue);
            _context.SaveChanges();

            IssueColumn issueColumn = new IssueColumn();
            issueColumn.IssueID = newIssue.ID;
            issueColumn.ColumnID = _context.Column.FirstOrDefault(x => x.BoardID == int.Parse(formData["boardID"])).ID;
            issueColumn.AssignDate = DateTime.Now;
            _context.Add(issueColumn);
            _context.SaveChanges();

            return RedirectToAction("Board", new { boardID = int.Parse(formData["boardID"]) });

        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteColumn(IFormCollection id, IFormCollection formData)
        {
            var col = await _context.Column.FindAsync(int.Parse(formData["id"]));
            if (col != null)
            {
                _context.Column.Remove(col);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction("Board", new { boardID = int.Parse(formData["boardID"]) });
        }


        //[HttpGet("Boards/JoinBoard/{boardID}/{token}")]
        //public async Task<IActionResult> JoinBoard(int boardID, string token)
        //Переход по ссылке

        [HttpPost]
        public async Task<IActionResult> GenerateLink(IFormCollection formData)
        {
            return RedirectToAction("Board", new { boardID = int.Parse(formData["boardID"]) });
        }
        [HttpPost]
        public async Task<IActionResult> DeleteBoard(IFormCollection formData)
        {
            var board = await _context.Board.FindAsync(int.Parse(formData["boardID"]));
            if (board != null)
            {
                _context.Board.Remove(board);
            }

            await _context.SaveChangesAsync();
            return Redirect("/");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteColumn(IFormCollection formData)
        {
            var col = await _context.Column.FindAsync(int.Parse(formData["columnID"]));
            if (col != null)
            {
                _context.Column.Remove(col);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Board", new { boardID = int.Parse(formData["boardID"]) });
        }
        [HttpPost]
        public async Task<IActionResult> EditTask([Bind("ID, Name, Description, DeadlineDate, Status, Priority, PerformerID")] Issue issue, IFormCollection formData, int taskID)
        {
            Issue currIssue = await _context.Issue.FindAsync(int.Parse(formData["taskID"]));
            currIssue.Name = issue.Name;
            currIssue.Description = issue.Description ?? "";
            currIssue.DeadlineDate = issue.DeadlineDate;
            currIssue.Status = issue.Status;
            currIssue.Priority = issue.Priority;
            currIssue.PerformerID = issue.PerformerID;
            _context.SaveChanges();

            await _context.SaveChangesAsync();
            return RedirectToAction("Board", new { boardID = int.Parse(formData["boardID"]) });
        }
        [HttpPost]
        public async Task<IActionResult> DeleteTask(IFormCollection formData)
        {
            var task = await _context.Issue.FindAsync(int.Parse(formData["taskID"]));
            if (task != null)
            {
                _context.Issue.Remove(task);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Board", new { boardID = int.Parse(formData["boardID"]) });
        }
    }

}
