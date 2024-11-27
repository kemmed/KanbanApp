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
    public class IssuesController : Controller
    {
        private readonly KanbanAppContext _context;

        public IssuesController(KanbanAppContext context)
        {
            _context = context;
        }

        // GET: Issues
        public async Task<IActionResult> Index()
        {
            var kanbanAppContext = _context.Issue.Include(i => i.Creator).Include(i => i.Performer).Include(i => i.PriorityType);
            return View(await kanbanAppContext.ToListAsync());
        }

        // GET: Issues/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var issue = await _context.Issue
                .Include(i => i.Creator)
                .Include(i => i.Performer)
                .Include(i => i.PriorityType)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (issue == null)
            {
                return NotFound();
            }

            return View(issue);
        }

        // GET: Issues/Create
        public IActionResult Create()
        {
            ViewData["CreatorID"] = new SelectList(_context.Set<User>(), "ID", "ID");
            ViewData["PerformerID"] = new SelectList(_context.Set<User>(), "ID", "ID");
            ViewData["PriorityTypeID"] = new SelectList(_context.Set<PriorityType>(), "ID", "ID");
            return View();
        }

        // POST: Issues/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,Description,CreateDate,EndDate,DeadlineDate,Status,CreatorID,PerformerID,PriorityTypeID")] Issue issue)
        {
            if (ModelState.IsValid)
            {
                _context.Add(issue);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CreatorID"] = new SelectList(_context.Set<User>(), "ID", "ID", issue.CreatorID);
            ViewData["PerformerID"] = new SelectList(_context.Set<User>(), "ID", "ID", issue.PerformerID);
            ViewData["PriorityTypeID"] = new SelectList(_context.Set<PriorityType>(), "ID", "ID", issue.PriorityTypeID);
            return View(issue);
        }

        // GET: Issues/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var issue = await _context.Issue.FindAsync(id);
            if (issue == null)
            {
                return NotFound();
            }
            ViewData["CreatorID"] = new SelectList(_context.Set<User>(), "ID", "ID", issue.CreatorID);
            ViewData["PerformerID"] = new SelectList(_context.Set<User>(), "ID", "ID", issue.PerformerID);
            ViewData["PriorityTypeID"] = new SelectList(_context.Set<PriorityType>(), "ID", "ID", issue.PriorityTypeID);
            return View(issue);
        }

        // POST: Issues/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,Description,CreateDate,EndDate,DeadlineDate,Status,CreatorID,PerformerID,PriorityTypeID")] Issue issue)
        {
            if (id != issue.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(issue);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IssueExists(issue.ID))
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
            ViewData["CreatorID"] = new SelectList(_context.Set<User>(), "ID", "ID", issue.CreatorID);
            ViewData["PerformerID"] = new SelectList(_context.Set<User>(), "ID", "ID", issue.PerformerID);
            ViewData["PriorityTypeID"] = new SelectList(_context.Set<PriorityType>(), "ID", "ID", issue.PriorityTypeID);
            return View(issue);
        }

        // GET: Issues/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var issue = await _context.Issue
                .Include(i => i.Creator)
                .Include(i => i.Performer)
                .Include(i => i.PriorityType)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (issue == null)
            {
                return NotFound();
            }

            return View(issue);
        }

        // POST: Issues/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var issue = await _context.Issue.FindAsync(id);
            if (issue != null)
            {
                _context.Issue.Remove(issue);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IssueExists(int id)
        {
            return _context.Issue.Any(e => e.ID == id);
        }
    }
}
