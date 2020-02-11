using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ECAdmin.Controllers
{
    public class ShopController : Controller
    {
        public IActionResult Index()
        {
            var controller = RouteData;
            object slugs;
            var hasSlug = "";
            if (RouteData.Values.TryGetValue("slug", out slugs))
            {
                hasSlug = "has slug";
            }
            else
            {
                hasSlug = "has not slug";
            }
            return Content($"Shop index slug: {hasSlug}");
        }
    }
}