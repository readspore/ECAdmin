using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ECAdmin.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            var controller = RouteData;
            object slug;
            var hasSlug = "";
            if (RouteData.Values.TryGetValue("slug", out slug))
            {
                hasSlug = "has slug";
            }
            else
            {
                hasSlug = "has not slug";
            }
            return Content($"product index slug: {hasSlug}");
        }
    }
}