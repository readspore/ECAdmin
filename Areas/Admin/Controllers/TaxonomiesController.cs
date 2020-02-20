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
    public class TaxonomiesController : Controller
    {
        private readonly ApplicationContext _context;

        public TaxonomiesController(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Taxonomies.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taxonomy = await _context.Taxonomies
                .FirstOrDefaultAsync(m => m.Id == id);
            if (taxonomy == null)
            {
                return NotFound();
            }

            return View(taxonomy);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Slug,Type,PostType")] Taxonomy taxonomy)
        {
            if (ModelState.IsValid)
            {
                var rawSlug = taxonomy.Slug == null | taxonomy.Slug?.Trim().Length != 0
                          ? taxonomy.Name
                          : taxonomy.Slug;
                taxonomy.Slug = Slug.GetUniqSlug(rawSlug, _context.Taxonomies.Select(c => c.Slug).ToList());
                _context.Add(taxonomy);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(taxonomy);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taxonomy = await _context.Taxonomies.FindAsync(id);
            if (taxonomy == null)
            {
                return NotFound();
            }
            return View(taxonomy);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,Slug,Type,PostType")] Taxonomy taxonomy)
        {
            if (id != taxonomy.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(taxonomy);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TaxonomyExists(taxonomy.Id))
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
            return View(taxonomy);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taxonomy = await _context.Taxonomies
                .FirstOrDefaultAsync(m => m.Id == id);
            if (taxonomy == null)
            {
                return NotFound();
            }

            return View(taxonomy);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var taxonomy = await _context.Taxonomies.FindAsync(id);
            _context.Taxonomies.Remove(taxonomy);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TaxonomyExists(int id)
        {
            return _context.Taxonomies.Any(e => e.Id == id);
        }
    }
}
