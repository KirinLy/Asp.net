using MagicVilla_VillaApi.Models;
using Microsoft.EntityFrameworkCore;

namespace MagicVilla_VillaApi.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
        }

        public DbSet<Villa> Villas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Villa>().HasData(
                new Villa
                {
                    Id = 1,
                    Name = "Villa 1",
                    Description = "Villa 1 Description",
                    Location = "Villa 1 Location",
                    Price = 100,
                    Rating = 1,
                    ImageUrl = "Villa 1 ImageUrl",
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                },
                new Villa
                {
                    Id = 2,
                    Name = "Villa 2",
                    Description = "Villa 2 Description",
                    Location = "Villa 2 Location",
                    Price = 200,
                    Rating = 2,
                    ImageUrl = "Villa 2 ImageUrl",
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                },
                new Villa
                {
                    Id = 3,
                    Name = "Villa 3",
                    Description = "Villa 3 Description",
                    Location = "Villa 3 Location",
                    Price = 300,
                    Rating = 3,
                    ImageUrl = "Villa 3 ImageUrl",
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                },
                new Villa
                {
                    Id = 4,
                    Name = "Villa 4",
                    Description = "Villa 4 Description",
                    Location = "Villa 4 Location",
                    Price = 400,
                    Rating = 4,
                    ImageUrl = "Villa 4 ImageUrl",
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                },
                new Villa
                {
                    Id = 5,
                    Name = "Villa 5",
                    Description = "Villa 5 Description",
                    Location = "Villa 5 Location",
                    Price = 500,
                    Rating = 5,
                    ImageUrl = "Villa 5 ImageUrl",
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                }
            );
        }
    }
}