using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KanbanApp.Data;
using KanbanApp.Models;

namespace KanbanApp.Controllers
{
    public class UserBoardsController : Controller
    {
        private readonly KanbanAppContext _context;

        public UserBoardsController(KanbanAppContext context)
        {
            _context = context;
        }

        // GET: UserBoards
        public async Task<IActionResult> Index()
        {
            var kanbanAppContext = _context.UserBoard.Include(u => u.Board).Include(u => u.User);
            return View(await kanbanAppContext.ToListAsync());
        }

        // GET: UserBoards/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userBoard = await _context.UserBoard
                .Include(u => u.Board)
                .Include(u => u.User)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (userBoard == null)
            {
                return NotFound();
            }

            return View(userBoard);
        }

        // GET: UserBoards/Create
        public IActionResult Create()
        {
            ViewData["BoardID"] = new SelectList(_context.Board, "ID", "ID");
            ViewData["UserID"] = new SelectList(_context.User, "ID", "ID");
            return View();
        }

        // POST: UserBoards/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,UserID,BoardID,UserRole")] UserBoard userBoard)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userBoard);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BoardID"] = new SelectList(_context.Board, "ID", "ID", userBoard.BoardID);
            ViewData["UserID"] = new SelectList(_context.User, "ID", "ID", userBoard.UserID);
            return View(userBoard);
        }

        // GET: UserBoards/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userBoard = await _context.UserBoard.FindAsync(id);
            if (userBoard == null)
            {
                return NotFound();
            }
            ViewData["BoardID"] = new SelectList(_context.Board, "ID", "ID", userBoard.BoardID);
            ViewData["UserID"] = new SelectList(_context.User, "ID", "ID", userBoard.UserID);
            return View(userBoard);
        }

        // POST: UserBoards/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,UserID,BoardID,UserRole")] UserBoard userBoard)
        {
            if (id != userBoard.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userBoard);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserBoardExists(userBoard.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["BoardID"] = new SelectList(_context.Board, "ID", "ID", userBoard.BoardID);
            ViewData["UserID"] = new SelectList(_context.User, "ID", "ID", userBoard.UserID);
            return View(userBoard);
        }

        // GET: UserBoards/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userBoard = await _context.UserBoard
                .Include(u => u.Board)
                .Include(u => u.User)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (userBoard == null)
            {
                return NotFound();
            }

            return View(userBoard);
        }

        // POST: UserBoards/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userBoard = await _context.UserBoard.FindAsync(id);
            if (userBoard != null)
            {
                _context.UserBoard.Remove(userBoard);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserBoardExists(int id)
        {
            return _context.UserBoard.Any(e => e.ID == id);
        }
    }
}
