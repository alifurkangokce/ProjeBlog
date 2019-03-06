using Blog.Data.Builders;
using Blog.Model;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Data
{
    public class ApplicationDbContext:IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()

            : base("DefaultConnection", throwIfV1Schema: false)

        {
        }
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
        public virtual DbSet<Post> Posts { get; set; }

        public virtual DbSet<Category> Categories { get; set; }

        public virtual DbSet<Galery> Galeries { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            new PostBuilder(modelBuilder.Entity<Post>());
            new GaleryBuilder(modelBuilder.Entity<Galery>());
            new CategoryBuilder(modelBuilder.Entity<Category>());

        }

        
    }
}
