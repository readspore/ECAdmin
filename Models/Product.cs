using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ECAdmin.Models
{
    public class Product
    {
        public int Id { get; set; }
        public decimal RegularPrice { get; set; }
        public decimal SalePrice { get; set; }
        public string Sku { get; set; }
        public bool IsShippigRequired { get; set; }
        public int PostId { get; set; }
        [ForeignKey ("PostId")]
        public Post Post { get; set; }
    }
}
