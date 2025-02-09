using Microsoft.EntityFrameworkCore;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Data
{
    public class NZWalksDbContext : DbContext
    {
        public NZWalksDbContext(DbContextOptions<NZWalksDbContext> options) : base(options)
        {
            
        }

        public DbSet<Difficulty> Difficulties { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Walk> Walks { get; set; }

        public DbSet<Image> Images { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed Data For Difficulties
            // Easy, Medium, Hard
            var difficulties = new List<Difficulty>()
            {
                new Difficulty
                {
                    Id = Guid.Parse("5301912f-e31e-400b-a5e5-4c1571422007"),
                    Name ="Easy"
                },
                new Difficulty
                {
                    Id = Guid.Parse("c1bb9278-1996-43dd-866c-7a3f59a033d8"),
                    Name = "Medium"
                },
                new Difficulty
                {
                    Id = Guid.Parse("ca797fe4-bf17-4ed1-ab29-26fd20ed1f6a"),
                    Name = "Hard"
                }
            };

            // Seed Difficulties To Database
            modelBuilder.Entity<Difficulty>().HasData(difficulties);

            // Seed Data For Regions
            var regions = new List<Region>() 
            {
                new Region
                {
                    Id = Guid.Parse("f7248fc3-2585-4efb-8d1d-1c555f4087f6"),
                    Name = "Aukland",
                    Code = "AKL",
                    RegionImgUrl = null
                },
                new Region
                {
                    Id = Guid.Parse("6884f7d7-ad1f-4101-8df3-7a6fa7387d81"),
                    Name = "Northland",
                    Code = "NRL",
                    RegionImgUrl= null
                },
                new Region
                {
                    Id = Guid.Parse("14ceba71-4b51-4777-9b17-46602cf66153"),
                    Name = "Bay Of Plenty",
                    Code = "BOP",
                    RegionImgUrl= null
                },
                new Region
                {
                    Id = Guid.Parse("cfa06ed2-bf65-4b65-93ed-c9d286ddb0de"),
                    Name = "Wellington",
                    Code = "WEL",
                    RegionImgUrl= "wel.jpeg"
                },
                new Region
                {
                    Id = Guid.Parse("906cb139-415a-4bbb-a174-1a1faf9fb1f6"),
                    Name = "Nellson",
                    Code = "NSN",
                    RegionImgUrl = null
                },
                new Region
                {
                    Id = Guid.Parse("f077a22e-4248-4bf6-b564-c7cf4e250263"),
                    Name = "Southland",
                    Code = "STL",
                    RegionImgUrl = "stl.jpeg"
                }
            };

            modelBuilder.Entity<Region>().HasData(regions);
        }
    }
}
