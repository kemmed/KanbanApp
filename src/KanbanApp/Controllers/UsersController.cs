using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KanbanApp.Data;
using KanbanApp.Models;
using System.Text;
using System.Security.Cryptography;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

namespace KanbanApp.Controllers
{
    public class UsersController : Controller
    {
        private readonly KanbanAppContext _context;

        public UsersController(KanbanAppContext context)
        {
            _context = context;
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
            return View(await _context.User.ToListAsync());
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.User
                .FirstOrDefaultAsync(m => m.ID == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Email,HashPass")] User user)
        {
            user.HashPass = HashPassword(user.HashPass);
            _context.Add(user);
            await _context.SaveChangesAsync();
            return Redirect("/");
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.User.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Email,HashPass")] User user)
        {
            if (id != user.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.ID))
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
            return View(user);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.User
                .FirstOrDefaultAsync(m => m.ID == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await _context.User.FindAsync(id);
            if (user != null)
            {
                _context.User.Remove(user);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        //POST проверка при авторизации
        [HttpPost]

        public async Task<IActionResult> Authorization([Bind("ID,Email,HashPass")] User user)
        {
            List<User> users = await _context.User.ToListAsync();
            var logUser = await _context.User
                .FirstOrDefaultAsync(m => m.Email == user.Email && m.HashPass == HashPassword(user.HashPass));
            if (logUser == null)
            {
                
                return Redirect("/Users/AuthError?error=wrongemail");
            }
            else
            {
                HttpContext.Session.SetInt32("UserID", logUser.ID);
                return Redirect("/");

                //return Redirect("/Boards/Board");
            }
            
            //HttpContext.Session.Remove("UserID");
        }

        private bool UserExists(int id)
        {
            return _context.User.Any(e => e.ID == id);
        }


        static string HashPassword(string password)
        {
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashBytes = sha256.ComputeHash(passwordBytes);

                StringBuilder hashString = new StringBuilder();
                foreach (byte b in hashBytes)
                {
                    hashString.Append(b.ToString("x2"));
                }

                return hashString.ToString();
            }
        }

        public IActionResult AuthError(string error)
        {
            if(error == "wrongemail")
                ViewBag.error = "Неправильный email или пароль.";
            return View("Authorization");
        } 
        
        public IActionResult Authorization()
        {
            return View();
        }
        public IActionResult Registration()
        {
            return View("Registration");
        }

        public IActionResult UserProfile()
        {
            return View("UserProfile");
        }

        public IActionResult UserList()
        {
            return View("UserList");
        }
    }
}
