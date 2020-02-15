using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ECAdmin.Models;
using Microsoft.AspNetCore.Authorization;

namespace ECAdmin.Areas.Admin.Controllers
{
    [Authorize(Roles = "admin")]
    [Area("Admin")]

    public class DependencyController : Controller
    {
        private readonly ApplicationContext _context;

        public DependencyController(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var applicationContext = _context.Dependencies.Include(d => d.Taxonomy);
            return View(await applicationContext.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dependency = await _context.Dependencies
                .Include(d => d.Taxonomy)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dependency == null)
            {
                return NotFound();
            }

            return View(dependency);
        }

        public IActionResult Create()
        {
            ViewBag.Taxonomies = new SelectList(_context.Taxonomies, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TaxonomyId,Slug,Name,ParentDependencyId")] Dependency dependency)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dependency);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Taxonomy"] = new SelectList(_context.Taxonomies, "Id", "Id", dependency.Taxonomy);
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
            ViewBag.Taxonomies = new SelectList(_context.Taxonomies, "Id", "Name");
            return View(dependency);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TaxonomyId,Slug,Name,ParentDependencyId")] Dependency dependency)
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
            ViewData["TaxonomyId"] = new SelectList(_context.Taxonomies, "Id", "Id", dependency.TaxonomyId);
            return View(dependency);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dependency = await _context.Dependencies
                .Include(d => d.Taxonomy)
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
