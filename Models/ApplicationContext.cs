using ECAdmin.Areas.Admin.Models;
using ECAdmin.ContextInializers;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECAdmin.Models
{
    public class ApplicationContext : IdentityDbContext<User>
    {
        public DbSet<Taxonomy> Taxonomies { get; set; }
        public DbSet<Dependency> Dependencies { get; set; }
        public DbSet<Post> Posts { get; set; }
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            //Database.EnsureDeleted();
            //Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<PostDependency>()
                .HasKey(t => new { t.PostId, t.DependencyId });

            builder.Entity<PostDependency>()
                .HasOne(pc => pc.Post)
                .WithMany(s => s.PostDependencies)
                .HasForeignKey(pc => pc.PostId);

            builder.Entity<PostDependency>()
                .HasOne(pc => pc.Dependency)
                .WithMany(c => c.PostDependencies)
                .HasForeignKey(pc => pc.DependencyId);
        }
    }
}
