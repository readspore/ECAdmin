using ECAdmin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECAdmin.Areas.Admin.ViewModels
{
    public class PostProductViewModel
    {
        public Post Post { get; set; }
        public Product Product { get; set; }
    }
}
