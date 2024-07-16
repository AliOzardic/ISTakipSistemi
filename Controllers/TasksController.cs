using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IsTakipSistemi.Data;
using IsTakipSistemi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace IsTakipSistemi.Controllers
{
    [Authorize]
    public class TasksController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public TasksController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        // KULLANICILAR SADECE KENDİLERİNE ATANAN GÖREVLER HAKKINDA BİLGİ SAHİBİ OLABİLİRLER
        // GET: Tasks
        [Authorize] //kullancıcınn giriş yapılı olması gerekmekte
        public async Task<IActionResult> Index()
        {
            // girili kullanıcıyı al 
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound(); // kullanıcının olmama durumunu denetle
            }

            // Retrieve tasks assigned to the current user
            var userTasks = await _context.Tasks
                .Where(t => t.AssignedUserId == user.Id) //  AssignedUserId nin   ApplicationUser.Id a foreing key oldugu için
                .Include(t => t.Project) // alakalı proje bilgisini dahil et
                .ToListAsync();

            return View(userTasks);
        }

        // GET: Tasks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var task = await _context.Tasks
                .Include(t => t.AssignedUser)
                .Include(t => t.Project)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (task == null)
            {
                return NotFound();
            }

            return View(task);
        }

        // GET: Tasks/Create
        public IActionResult Create()
        {
            ViewData["AssignedUserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id");
            ViewData["ProjectId"] = new SelectList(_context.Projects, "Id", "Id");
            return View();
        }

        // POST: Tasks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description,CreationDate,DueDate,Status,ProjectId,AssignedUserId")] Models.Task task)
        {
            if (ModelState.IsValid)
            {
                _context.Add(task);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AssignedUserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", task.AssignedUserId);
            ViewData["ProjectId"] = new SelectList(_context.Projects, "Id", "Id", task.ProjectId);
            return View(task);
        }

        // GET: Tasks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var task = await _context.Tasks.FindAsync(id);
            if (task == null)
            {
                return NotFound();
            }
            ViewData["AssignedUserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", task.AssignedUserId);
            ViewData["ProjectId"] = new SelectList(_context.Projects, "Id", "Id", task.ProjectId);
            return View(task);
        }

        // POST: Tasks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,CreationDate,DueDate,Status,ProjectId,AssignedUserId")] Models.Task task)
        {
            if (id != task.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(task);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TaskExists(task.Id))
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
            ViewData["AssignedUserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", task.AssignedUserId);
            ViewData["ProjectId"] = new SelectList(_context.Projects, "Id", "Id", task.ProjectId);
            return View(task);
        }

        // GET: Tasks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var task = await _context.Tasks
                .Include(t => t.AssignedUser)
                .Include(t => t.Project)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (task == null)
            {
                return NotFound();
            }

            return View(task);
        }

        // POST: Tasks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task != null)
            {
                _context.Tasks.Remove(task);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TaskExists(int id)
        {
            return _context.Tasks.Any(e => e.Id == id);
        }
    }
}
