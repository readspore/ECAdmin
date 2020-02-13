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
                    new Taxonomy { 
                        Name = "Категория 1 из сидера", 
                        PostType = "Post", 
                        Slug = "category", 
                        Type = "tree" 
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
