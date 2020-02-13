using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECAdmin.Models;
using ECAdmin.Models.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ECAdmin.Areas.Admin.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(Roles = "admin")]
    [Area("admin")]
    public class DependencyController : Controller
    {
        private readonly ApplicationContext _context;

        public DependencyController(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Dependencies.OrderByDescending(c => c.Id).ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dependency = await _context.Dependencies
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dependency == null)
            {
                return NotFound();
            }

            return View(dependency);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Slug")] Dependency dependency)
        {
            if (ModelState.IsValid)
            {
                var rawSlug = dependency.Slug == null | dependency.Slug?.Trim().Length != 0
                            ? dependency.Name
                            : dependency.Slug;
                dependency.Slug = Slug.GetUniqSlug(rawSlug, _context.Dependencies.Select(c => c.Slug).ToList());
                _context.Add(dependency);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(dependency);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dependency = await _context.Dependencies.FindAsync(id);
            if (dependency == null)
            {
                return NotFound();
            }
            return View(dependency);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Slug")] Dependency dependency)
        {
            if (id != dependency.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dependency);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DependencyExists(dependency.Id))
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
            return View(dependency);
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dependency = await _context.Dependencies
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dependency == null)
            {
                return NotFound();
            }

            return View(dependency);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dependency = await _context.Dependencies.FindAsync(id);
            _context.Dependencies.Remove(dependency);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        private bool DependencyExists(int id)
        {
            return _context.Dependencies.Any(e => e.Id == id);
        }
    }
}
