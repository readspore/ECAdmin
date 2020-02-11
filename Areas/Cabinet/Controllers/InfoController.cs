using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ECAdmin.Areas.Cabinet.Controllers
{
    public class InfoController : Controller
    {
        public IActionResult Index()
        {
            return Content("cabinet info index");
        }
    }
}