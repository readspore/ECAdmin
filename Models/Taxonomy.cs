using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ECAdmin.Models
{
    public class Taxonomy
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }
        [DisplayName("Название")]
        public string Name { get; set; }
        [DisplayName("Идентификатор")]
        public string Slug { get; set; }
        [DisplayName("Тип")]
        public string Type { get; set; }
        [Display(Name = "Тип поста")]
        public string PostType { get; set; }
        public List<Dependency> Dependencies { get; set; }
    }
}
