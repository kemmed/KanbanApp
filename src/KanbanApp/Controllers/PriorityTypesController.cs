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
    public class PriorityTypesController : Controller
    {
        private readonly KanbanAppContext _context;

        public PriorityTypesController(KanbanAppContext context)
        {
            _context = context;
        }

        // GET: PriorityTypes
        public async Task<IActionResult> Index()
        {
            var kanbanAppContext = _context.PriorityType.Include(p => p.Board);
            return View(await kanbanAppContext.ToListAsync());
        }

        // GET: PriorityTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var priorityType = await _context.PriorityType
                .Include(p => p.Board)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (priorityType == null)
            {
                return NotFound();
            }

            return View(priorityType);
        }

        // GET: PriorityTypes/Create
        public IActionResult Create()
        {
            ViewData["BoardID"] = new SelectList(_context.Board, "ID", "ID");
            return View();
        }

        // POST: PriorityTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,BoardID")] PriorityType priorityType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(priorityType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BoardID"] = new SelectList(_context.Board, "ID", "ID", priorityType.BoardID);
            return View(priorityType);
        }

        // GET: PriorityTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var priorityType = await _context.PriorityType.FindAsync(id);
            if (priorityType == null)
            {
                return NotFound();
            }
            ViewData["BoardID"] = new SelectList(_context.Board, "ID", "ID", priorityType.BoardID);
            return View(priorityType);
        }

        // POST: PriorityTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,BoardID")] PriorityType priorityType)
        {
            if (id != priorityType.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(priorityType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PriorityTypeExists(priorityType.ID))
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
            ViewData["BoardID"] = new SelectList(_context.Board, "ID", "ID", priorityType.BoardID);
            return View(priorityType);
        }

        // GET: PriorityTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var priorityType = await _context.PriorityType
                .Include(p => p.Board)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (priorityType == null)
            {
                return NotFound();
            }

            return View(priorityType);
        }

        // POST: PriorityTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var priorityType = await _context.PriorityType.FindAsync(id);
            if (priorityType != null)
            {
                _context.PriorityType.Remove(priorityType);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PriorityTypeExists(int id)
        {
            return _context.PriorityType.Any(e => e.ID == id);
        }
    }
}
