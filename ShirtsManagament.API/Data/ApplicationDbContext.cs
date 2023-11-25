using Microsoft.EntityFrameworkCore;
using ShirtsManagament.API.Models;

namespace ShirtsManagament.API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        public DbSet<Shirt> Shirts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Shirt>().HasData(new Shirt[]
            {
                new Shirt {Id = 1, Brand = "My brand", Color = "Black", Gender = 'M', Price = 200, Size = 10 },
                new Shirt {Id = 2, Brand = "My brand", Color = "Yellow", Gender = 'M', Price = 250, Size = 12 },
                new Shirt {Id = 3, Brand = "Another brand", Color = "Blue", Gender = 'F', Price = 180, Size = 8 },
                new Shirt {Id = 4, Brand = "Another brand", Gender = 'F', Price = 200, Size = 9 }
            });
        }
    }
}
