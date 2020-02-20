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
        [ScaffoldColumn(false)]
        public int Id { get; set; }
        [Display(Name = "Название")]
        public string Name { get; set; }
        [Display(Name = "Идентификатор")]
        public string Slug { get; set; }
        [Display(Name = "Таксономия")]
        public int TaxonomyId { get; set; }
        
        [ForeignKey("TaxonomyId")]
        [Display(Name = "Таксономия")]
        public Taxonomy Taxonomy { get; set; }
        [Display(Name = "Наследуется")]
        public int? ParentDependencyId { get; set; }

        [ForeignKey("ParentDependencyId")]
        [Display(Name = "Наследуется")]
        public virtual Dependency Parent { get; set; }
        public virtual ICollection<Dependency> Children { get; set; }
        public virtual List<PostDependency> PostDependency { get; set; }
    }
}
