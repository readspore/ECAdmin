using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ECAdmin.Models
{
    public class Product
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }
        [DisplayName("Обычная цена")]
        public decimal RegularPrice { get; set; }
        [DisplayName("Цена со скидкой")]
        public decimal SalePrice { get; set; }
        [DisplayName("Идентификатор")]
        public string Sku { get; set; }

        [DisplayName("Требуется ли доставка")]
        public bool IsShippigRequired { get; set; }
        [DisplayName("Пост")]
        public int PostId { get; set; }
        [ForeignKey("PostId")]
        [DisplayName("Пост")]
        public Post Post { get; set; }
    }
}
