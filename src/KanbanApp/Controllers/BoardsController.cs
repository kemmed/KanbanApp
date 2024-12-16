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
using NuGet.Packaging;

namespace KanbanApp.Controllers
{
    public class BoardsController : Controller
    {
        private readonly KanbanAppContext _context;

        public BoardsController(KanbanAppContext context)
        {
            _context = context;
        }

        private bool BoardExists(int id)
        {
            return _context.Board.Any(e => e.ID == id);
        }

        [HttpGet("Boards/Board/{boardID}")]
        public async Task<IActionResult> Board(int boardID)
        {
            if (_context.Board.FirstOrDefault(x => x.ID == boardID) == null || 
                HttpContext.Session.GetInt32("UserID") == null || 
                _context.UserBoard.FirstOrDefault(x => x.UserID == HttpContext.Session.GetInt32("UserID")) == null)
            {
                return NotFound();
            }
            List<Column> columns = _context.Column.Where(x => x.BoardID == boardID).Include(x => x.IssueColumns.Where(y => !y.IsDeleted)).ToList();
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
            if (_context.UserBoard.FirstOrDefault(x => x.UserID == HttpContext.Session.GetInt32("UserID") && x.BoardID == boardID).UserRole == UserRoles.Admin)
            {
                ViewBag.UserAdmin = true;
            }
            else
            {
                ViewBag.UserAdmin = false;
            }

            List<Issue> issues = new List<Issue>();

            foreach(var column in columns)
            {
                issues.AddRange(column.IssueColumns.Select(x => x.Issue));
            }
            ViewBag.BoardTasks = issues;

            return View(columns);
        }

        [HttpPost]
        public IActionResult CreateTask([Bind("ID, Name, Description, DeadlineDate, Status, Priority, PerformerID")] Issue issue, IFormCollection formData)
        {
            int? userSessionID = HttpContext.Session.GetInt32("UserID");

            User creator = _context.User.FirstOrDefault(x => x.ID == userSessionID);

            Issue newIssue = new Issue();
            newIssue.Name = issue.Name;
            newIssue.Description = issue.Description ?? "";
            if(issue.DeadlineDate.Date < DateTime.Now.Date)
                newIssue.DeadlineDate = DateTime.Now.Date;
            else
                newIssue.DeadlineDate = issue.DeadlineDate.Date;
            newIssue.Status = issue.Status;
            newIssue.Priority = issue.Priority;
            newIssue.PerformerID = issue.PerformerID;
            newIssue.CreatorID = (int)userSessionID;
            _context.Add(newIssue);
            _context.SaveChanges();

            IssueColumn issueColumn = new IssueColumn();
            issueColumn.IssueID = newIssue.ID;
            issueColumn.ColumnID = _context.Column.FirstOrDefault(x => x.BoardID == int.Parse(formData["boardID"]) && x.Name == @Issue.StatusToString(newIssue.Status)).ID;
            issueColumn.DeleteDate = DateTime.Now;
            issueColumn.IsDeleted = false;
            _context.Add(issueColumn);
            _context.SaveChanges();

            return RedirectToAction("Board", new { boardID = int.Parse(formData["boardID"]) });

        }

        [HttpGet("Boards/JoinBoard/{boardID}")]
        public async Task<IActionResult> JoinBoard(int boardID)
        {
            User currUser = _context.User.FirstOrDefault(x => x.ID == HttpContext.Session.GetInt32("UserID"));
            if (currUser != null)
            {
                UserBoard userBoard = new UserBoard();
                userBoard.BoardID = boardID;
                userBoard.UserID = currUser.ID;
                userBoard.UserRole = UserRoles.Editor;
                _context.Add(userBoard);
                _context.SaveChanges();
                return RedirectToAction("Board", new { boardID = boardID });
            }
            else if (_context.UserBoard.FirstOrDefault(x=> x.UserID == currUser.ID && x.BoardID==boardID) != null)
            {
                return RedirectToAction("Board", new { boardID = boardID });
            }
            else
            {
                return NotFound();
            }
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
        public async Task<IActionResult> EditBoard([Bind("ID,Name")] Board board, IFormCollection formData)
        {
            var currBoard = await _context.Board.FindAsync(int.Parse(formData["boardID"]));
            currBoard.Name = board.Name;

            await _context.SaveChangesAsync();
            return RedirectToAction("Board", new { boardID = int.Parse(formData["boardID"]) });
        }

       
        [HttpPost]
        public async Task<IActionResult> EditTask([Bind("ID, Name, Description, DeadlineDate, Status, Priority, PerformerID")] Issue issue, IFormCollection formData, int taskID)
        {
            Issue currIssue = await _context.Issue.FindAsync(int.Parse(formData["taskID"]));
            currIssue.Name = issue.Name;
            currIssue.Description = issue.Description ?? "";
            if (issue.DeadlineDate.Date >= DateTime.Now.Date || issue.DeadlineDate.Date > currIssue.DeadlineDate.Date)
                currIssue.DeadlineDate = issue.DeadlineDate;
            currIssue.Status = issue.Status;
            if (issue.Status == IssueStatus.Completed)
            {
                currIssue.EndDate = DateTime.Now.Date;
            }
            currIssue.Priority = issue.Priority;
            currIssue.PerformerID = issue.PerformerID;
            _context.SaveChanges();
            await _context.SaveChangesAsync();
            IssueColumn issueColumn = _context.IssueColumn.FirstOrDefault(x => x.IssueID == currIssue.ID);
            issueColumn.ColumnID = _context.Column.FirstOrDefault(x => x.BoardID == int.Parse(formData["boardID"]) && x.Name == @Issue.StatusToString(currIssue.Status)).ID;

            await _context.SaveChangesAsync();
            return RedirectToAction("Board", new { boardID = int.Parse(formData["boardID"]) });
        }

        [HttpPost]
        public async Task<IActionResult> DeleteUser(IFormCollection formData, int userID)
        {
            int boardID = int.Parse(formData["boardID"]);
            UserBoard userBoard = await _context.UserBoard.FirstOrDefaultAsync(x => x.UserID == userID && x.BoardID == boardID);

            if (userBoard != null)
            {
                _context.Remove(userBoard);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("UserList", "Users", new { boardID });
        }


        [HttpPost]
        public async Task<IActionResult> DeleteTask(IFormCollection formData)
        {
            var taskCol = _context.IssueColumn.FirstOrDefault(x => x.IssueID == int.Parse(formData["taskID"]));
            if (taskCol != null)
            {
                taskCol.IsDeleted = true;
                taskCol.DeleteDate = DateTime.Now.Date; 
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Board", new { boardID = int.Parse(formData["boardID"]) });
        }

        [HttpGet("/Boards/ArchiveOfTasks/{boardID}")]
        public async Task<IActionResult> ArchiveOfTasks(int boardID)
        {
            int? userSessionID = HttpContext.Session.GetInt32("UserID");
            User currentUser = await _context.User.FindAsync(userSessionID);
            var userOnBoard = _context.UserBoard.FirstOrDefault(x => x.UserID == currentUser.ID);
            if (currentUser == null || userOnBoard == null || userOnBoard.UserRole != UserRoles.Admin)
            {
                return NotFound();
            }
            ViewBag.BoardName = _context.Board.FirstOrDefault(x => x.ID == boardID).Name;
            ViewBag.BoardID = _context.Board.FirstOrDefault(x => x.ID == boardID).ID;
            List<Column> columns = _context.Column.Where(x => x.BoardID == boardID).Include(x => x.IssueColumns.Where(y => y.IsDeleted)).ToList();
            columns.ForEach(x => x.IssueColumns.ForEach(y => y.Issue = _context.Issue.Include(w => w.Performer).Include(w => w.Creator).FirstOrDefault(z => z.ID == y.IssueID)));

            return View(columns);
        }
        [HttpGet("/Boards/RestoreTask/{taskID}")]
        public async Task<IActionResult> RestoreTask(int taskID, int boardID)
        {
            var taskCol = _context.IssueColumn.FirstOrDefault(x => x.IssueID == taskID);
            if (taskCol != null)
            {
                taskCol.IsDeleted = false;
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("ArchiveOfTasks", new {boardID = boardID});
        }
    }

}
