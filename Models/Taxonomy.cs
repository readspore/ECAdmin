using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECAdmin.Models
{
    public class Taxonomy
    {
        public int Id { get; set; }
        public string Slug { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string PostType { get; set; }
        public List<Dependency> Dependencies { get; set; }
    }
}
