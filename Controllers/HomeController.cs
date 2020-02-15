using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ECAdmin.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace ECAdmin.Controllers
{
   // [Authorize(Roles = "admin")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationContext _context;


        public HomeController(ILogger<HomeController> logger, ApplicationContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            
            //var tax1 = new Taxonomy { Name = "Категория 1", PostType = "Post", Slug = "category", Type = "tree" };
            //_context.Taxonomies.AddRange(tax1);
            //var dep1 = new Dependency { Name = "Одежда", Slug = "clothes", Taxonomy = tax1 };
            //var dep2 = new Dependency { Name = "Обувь", Slug = "shoes", Taxonomy = tax1 };
            //_context.Dependencies.AddRange(dep1, dep2);
            //var dep1_1 = new Dependency { Name = "Одежда теплая", Slug = "clothes-warm", Taxonomy = tax1, Parent = dep1 };
            //var dep1_2 = new Dependency { Name = "Одежда летняя", Slug = "clothes-summer", Taxonomy = tax1, Parent = dep1 };
            //_context.Dependencies.AddRange(dep1_1, dep1_2);
            //_context.SaveChanges();
            //var allDeps = _context.Dependencies.ToList();
            //var allTaxes = _context.Taxonomies.ToList();


            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
