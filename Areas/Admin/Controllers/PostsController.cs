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
using System.IO;
using Microsoft.AspNetCore.Http;
using ECAdmin.Areas.Admin.ViewModels;
using Microsoft.AspNetCore.Hosting;

namespace ECAdmin.Areas.Admin.Controllers
{
    [Authorize(Roles = "admin")]
    [Area("Admin")]
    public class PostsController : Controller
    {
        private readonly ApplicationContext _context;
        readonly IWebHostEnvironment _appEnvironment;

        public PostsController(ApplicationContext context, IWebHostEnvironment appEnvironment)
        {
            _context = context;
            _appEnvironment = appEnvironment;
        }

        public async Task<IActionResult> Index()
        {
            var applicationContext = _context.Posts.Include(p => p.Image);
            return View(await applicationContext.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Posts
                .Include(p => p.Image)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (post == null)
            {
                return NotFound();
            }
            return View(post);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PostImage postImg)
        {
            Post post = postImg.Post;
            post.Image = await SaveTheFile(postImg.file);
            post.ImageId = post.Image.Id;
            if (ModelState.IsValid)
            {
                post.DateAdded = DateTime.Now;
                post.DateModified = DateTime.Now;
                var rawSlug = post.Slug == null | post.Slug?.Trim().Length != 0
                            ? post.Name
                            : post.Slug;
                post.Slug = Slug.GetUniqSlug(rawSlug, _context.Posts.Select(c => c.Slug).ToList());
                _context.Add(post);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(post);
        }

        private async Task<Image> SaveTheFile(IFormFile uploadedFile)
        {
            Image img=new Image();
            string path = "/Files/" + DateTime.Now.ToString("yyyy-MM");
            System.IO.Directory.CreateDirectory(_appEnvironment.WebRootPath + path);
            img.Name = GetUniqNameInDir(path, uploadedFile.FileName);
            path += "/" + img.Name;
            using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
            {
                await uploadedFile.CopyToAsync(fileStream);
            }
            img.Src = path;
            img.Alt = DateTime.Now.ToString();
            _context.Add(img);
            await _context.SaveChangesAsync();
            Image res =await _context.Images.FirstOrDefaultAsync(m => m.Name == img.Name);
            return res;
        }

        private string GetUniqNameInDir(string ditPath, string fileName)
        {
            bool nameNotUniq = true;
            int i = 0;
            var tmpName = fileName;
            while (nameNotUniq)
            {
                ++i;
                if (System.IO.File.Exists(_appEnvironment.WebRootPath + ditPath + "/" + tmpName))
                {
                    tmpName = fileName.Replace(".", $"{i}.");
                    tmpName = tmpName == fileName //if fileName not containsumbol . (extention)
                                ? fileName + i
                                : tmpName;
                }
                else
                {
                    nameNotUniq = false;
                    fileName = tmpName;
                }
            }
            return fileName;
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Posts
                .Include(p => p.Image)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (post == null)
            {
                return NotFound();
            }
            PostImage postImg = new PostImage { Post=post };
            return View(postImg);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,  PostImage postImg)
        {
            if (id != postImg.Post.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    postImg.Post.Image = await SaveTheFile(postImg.file);
                    postImg.Post.ImageId = postImg.Post.Image.Id;
                    postImg.Post.DateModified = DateTime.Now;
                    _context.Update(postImg.Post);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostExists(postImg.Post.Id))
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
            return View(postImg);
        }
        
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Posts
                .Include(p => p.Image)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var post = await _context.Posts.FindAsync(id);
            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PostExists(int id)
        {
            return _context.Posts.Any(e => e.Id == id);
        }
    }
}
