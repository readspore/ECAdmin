using ECAdmin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECAdmin.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string Slug { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }

        public List<PostDependency> PostDependencies { get; set; }
        public Post()
        {
            PostDependencies = new List<PostDependency>();
        }
    }
}
