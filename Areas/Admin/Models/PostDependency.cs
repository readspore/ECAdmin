using ECAdmin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECAdmin.Areas.Admin.Models
{
    public class PostDependency
    {
        public int PostId { get; set; }
        public Post Post { get; set; }

        public int DependencyId { get; set; }
        public Dependency Dependency { get; set; }
    }
}
