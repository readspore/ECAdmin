using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ECAdmin.Controllers
{
    public class TaxonomyController : Controller
    {
        public IActionResult Index()
        {
            //var taxonomy = RouteData.Values["controller"].ToString();
            //var dependensy = RouteData.Values["action"].ToString();
            var path = RouteData.Values["path"].ToString().Split("/");
            var taxonomy = path.ElementAtOrDefault(0);
            var dependensy = path.ElementAtOrDefault(1);
            var dependensy2 = path.ElementAtOrDefault(2);
            return Content($"taxonomy: {taxonomy} | dependensy: {dependensy} | path: {path}");
        }
    }
}