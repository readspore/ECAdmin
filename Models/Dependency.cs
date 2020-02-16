using ECAdmin.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ECAdmin.Models
{
    public class Dependency
    {
        public int Id { get; set; }
        [Display(Name = "Taxonomy Id")]
        public int TaxonomyId { get; set; }
        
        [ForeignKey("TaxonomyId")]
        public Taxonomy Taxonomy { get; set; }
        public string Slug { get; set; }
        public string Name { get; set; }
        [Display(Name = "Parent Dependency Id")]
        public int? ParentDependencyId { get; set; }

        [ForeignKey("ParentDependencyId")]
        public virtual Dependency Parent { get; set; }
        public virtual ICollection<Dependency> Children { get; set; }
        public List<PostDependency> PostDependencies { get; set; }

        public Dependency()
        {
            PostDependencies = new List<PostDependency>();
        }
    }
}
