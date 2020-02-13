using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ECAdmin.Models
{
    public class Dependency
    {
        public int Id { get; set; }
        public int TaxonomyId { get; set; }
        
        [ForeignKey("TaxonomyId")]
        public Taxonomy Taxonomy { get; set; }
        public string Slug { get; set; }
        public string Name { get; set; }
//        public int? ParentDependencyId { get; set; }
        public virtual Dependency Parent { get; set; }
        public virtual ICollection<Dependency> Children { get; set; }
    }
}
