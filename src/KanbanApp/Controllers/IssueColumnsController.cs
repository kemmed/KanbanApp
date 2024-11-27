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
    public class IssueColumnsController : Controller
    {
        private readonly KanbanAppContext _context;

        public IssueColumnsController(KanbanAppContext context)
        {
            _context = context;
        }

        // GET: IssueColumns
        public async Task<IActionResult> Index()
        {
            var kanbanAppContext = _context.IssueColumn.Include(i => i.Column).Include(i => i.Issue);
            return View(await kanbanAppContext.ToListAsync());
        }

        // GET: IssueColumns/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var issueColumn = await _context.IssueColumn
                .Include(i => i.Column)
                .Include(i => i.Issue)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (issueColumn == null)
            {
                return NotFound();
            }

            return View(issueColumn);
        }

        // GET: IssueColumns/Create
        public IActionResult Create()
        {
            ViewData["ColumnID"] = new SelectList(_context.Column, "ID", "ID");
            ViewData["IssueID"] = new SelectList(_context.Issue, "ID", "ID");
            return View();
        }

        // POST: IssueColumns/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,AssignDate,IssueID,ColumnID")] IssueColumn issueColumn)
        {
            if (ModelState.IsValid)
            {
                _context.Add(issueColumn);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ColumnID"] = new SelectList(_context.Column, "ID", "ID", issueColumn.ColumnID);
            ViewData["IssueID"] = new SelectList(_context.Issue, "ID", "ID", issueColumn.IssueID);
            return View(issueColumn);
        }

        // GET: IssueColumns/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var issueColumn = await _context.IssueColumn.FindAsync(id);
            if (issueColumn == null)
            {
                return NotFound();
            }
            ViewData["ColumnID"] = new SelectList(_context.Column, "ID", "ID", issueColumn.ColumnID);
            ViewData["IssueID"] = new SelectList(_context.Issue, "ID", "ID", issueColumn.IssueID);
            return View(issueColumn);
        }

        // POST: IssueColumns/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,AssignDate,IssueID,ColumnID")] IssueColumn issueColumn)
        {
            if (id != issueColumn.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(issueColumn);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IssueColumnExists(issueColumn.ID))
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
            ViewData["ColumnID"] = new SelectList(_context.Column, "ID", "ID", issueColumn.ColumnID);
            ViewData["IssueID"] = new SelectList(_context.Issue, "ID", "ID", issueColumn.IssueID);
            return View(issueColumn);
        }

        // GET: IssueColumns/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var issueColumn = await _context.IssueColumn
                .Include(i => i.Column)
                .Include(i => i.Issue)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (issueColumn == null)
            {
                return NotFound();
            }

            return View(issueColumn);
        }

        // POST: IssueColumns/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var issueColumn = await _context.IssueColumn.FindAsync(id);
            if (issueColumn != null)
            {
                _context.IssueColumn.Remove(issueColumn);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IssueColumnExists(int id)
        {
            return _context.IssueColumn.Any(e => e.ID == id);
        }
    }
}
