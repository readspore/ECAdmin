using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ECAdmin.Models;
using Microsoft.AspNetCore.Authorization;
using ECAdmin.Models.Helpers;

namespace ECAdmin.Areas.Admin.Controllers
{
    [Authorize(Roles = "admin")]
    [Area("Admin")]
    public class DependenciesController : Controller
    {
        private readonly ApplicationContext _context;

        public DependenciesController(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var applicationContext = _context.Dependencies.Include(d => d.Parent).Include(d => d.Taxonomy);
            return View(await applicationContext.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dependency = await _context.Dependencies
                .Include(d => d.Parent)
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
            ViewData["ParentDependencyId"] = new SelectList(_context.Dependencies, "Id", "Name");
            ViewData["TaxonomyId"] = new SelectList(_context.Taxonomies, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Slug,TaxonomyId,ParentDependencyId")] Dependency dependency)
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
            ViewData["ParentDependencyId"] = new SelectList(_context.Dependencies, "Id", "Name", dependency.ParentDependencyId);
            ViewData["TaxonomyId"] = new SelectList(_context.Taxonomies, "Id", "Name", dependency.TaxonomyId);
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
            ViewData["ParentDependencyId"] = new SelectList(_context.Dependencies, "Id", "Name", dependency.ParentDependencyId);
            ViewData["TaxonomyId"] = new SelectList(_context.Taxonomies, "Id", "Name", dependency.TaxonomyId);
            return View(dependency);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,Slug,TaxonomyId,ParentDependencyId")] Dependency dependency)
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
            ViewData["ParentDependencyId"] = new SelectList(_context.Dependencies, "Id", "Name", dependency.ParentDependencyId);
            ViewData["TaxonomyId"] = new SelectList(_context.Taxonomies, "Id", "Name", dependency.TaxonomyId);
            return View(dependency);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dependency = await _context.Dependencies
                .Include(d => d.Parent)
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
