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

    public class TaxonomyController : Controller
    {
        private readonly ApplicationContext _context;

        public TaxonomyController(ApplicationContext context)
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
        public async Task<IActionResult> Create([Bind("Id,Slug,Name,Type,PostType")] Taxonomy taxonomy)
        {
            if (ModelState.IsValid)
            {
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
        public async Task<IActionResult> Edit(int id, [Bind("Id,Slug,Name,Type,PostType")] Taxonomy taxonomy)
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
