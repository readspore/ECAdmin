using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECAdmin.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationContext(
                                        serviceProvider.GetRequiredService<DbContextOptions<ApplicationContext>>()
                )
            )
            {
                // Look for any movies.
                if (context.Taxonomies.Any())
                {
                    return;   // DB has been seeded
                }

                context.Taxonomies.AddRange(
                    new Taxonomy
                    {
                        Name = "Категория 1 из сидера",
                        PostType = "Post",
                        Slug = "category",
                        Type = "tree"
                    }
                );
                context.Images.AddRange(
                    new Image
                    {
                        Name = "Whyyyy!",
                        Src = "https://medialeaks.ru/wp-content/uploads/2019/08/2-33.jpg",
                        Alt = "Meh"
                    }
                );
                context.Posts.AddRange(
                    new Post
                    {
                        Name = "Yuk",
                        Slug = "Curious",
                        DateAdded = DateTime.Now,
                        Type = "Post",
                        Status = "draft",
                        Description = "What is wrong with you humab?",
                        ShortDescription = "What?",
                        ImageId = 1
                    });
                context.Products.AddRange(
                    new Product
                    {
                        RegularPrice = 24,
                        SalePrice = 12,
                        IsShippigRequired = true,
                        PostId = 1,
                        Sku = "sdsdada"
                    });
                context.SaveChanges();
            }
        }
    }
}
