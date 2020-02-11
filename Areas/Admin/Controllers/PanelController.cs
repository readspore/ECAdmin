using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ECAdmin.Areas.Admin.Controllers
{
    public class PanelController : Controller
    {
        public IActionResult Index()
        {
            return Content("Admin index");
        }
    }
}