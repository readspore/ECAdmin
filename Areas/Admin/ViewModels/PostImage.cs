using ECAdmin.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECAdmin.Areas.Admin.ViewModels
{
    public class PostImage
    {
        public Post Post { get; set; }
        public IFormFile file { get; set; }
    }
}
