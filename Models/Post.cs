using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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
        public DateTime DateAdded { get; set; }
        public DateTime DateModified { get; set; }
        public string Status { get; set; } //Options: draft, pending, private and publish
        public string Description { get; set; }
        public string ShortDescription { get; set; }
        public string ImageId { get; set; }
        [ForeignKey("ImageId")]
        public Image Image { get; set; }
        public List<Product> Products { get; set; }
    }
}
