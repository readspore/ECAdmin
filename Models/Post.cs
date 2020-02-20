using ECAdmin.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ECAdmin.Models
{
    public class Post
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }
        [DisplayName("Название")]
        public string Name { get; set; }

        [DisplayName("Идентификатор")]
        public string Slug { get; set; }
        [DisplayName("Тип")]
        public string Type { get; set; }
        [ScaffoldColumn(false)]
        [DisplayName("Дата создания")]
        public DateTime DateAdded { get; set; }
        [ScaffoldColumn(false)]
        [DisplayName("Дата последнего изменения")]
        public DateTime DateModified { get; set; }
        [ScaffoldColumn(false)]
        [DisplayName("Статус")]
        public string Status { get; set; }
        [DisplayName("Описание")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        [DisplayName("Краткое описание")]
        public string ShortDescription { get; set; }
        [DisplayName("Изображение")]
        public int ImageId { get; set; }
        [ForeignKey("ImageId")]
        [DisplayName("Изображение")]
        public Image Image { get; set; }
        public virtual ICollection<Product> Products { get; set; }
        public virtual List<PostDependency> PostDependency { get; set; }
    }
}
