using Microsoft.EntityFrameworkCore;
using NZWalksAPI.Models.Domain;

namespace NZWalksAPI.Data
{
    public class NZWalksDbContext : DbContext
    {
        public NZWalksDbContext(DbContextOptions options) : base(options)
        {
            
        }

        public DbSet<Difficulty> Difficulties { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Walk> Walks { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Image> Images { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Seed data for difficulties
            //Easy, Medium and Hard

            var difficulties = new List<Difficulty>()
            {
                new Difficulty()
                {
                    Id = Guid.Parse("0814f5be-5747-4cc3-94e3-75bb92d7ccfc"),
                     Name = "Easy"
                },
                new Difficulty()
                {
                    Id = Guid.Parse("c4b44165-a58d-4ae4-bf91-76f431861afd"),
                     Name = "Medium"
                },
                new Difficulty()
                {
                    Id = Guid.Parse("ac0c64f8-7957-4535-94d4-992212cbd3ad"),
                     Name = "Hard"
                }
            };

            // Seed Difficulties to the Database
            modelBuilder.Entity<Difficulty>().HasData(difficulties);

            //Seed data for regions

            var regions = new List<Region>() 
            {
                 new Region
                {
                    Id = Guid.Parse("f7248fc3-2585-4efb-8d1d-1c555f4087f6"),
                    Name = "Auckland",
                    Code = "AKL",
                    RegionImageUrl = "https://images.pexels.com/photos/5169056/pexels-photo-5169056.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1"
                },
                new Region
                {
                    Id = Guid.Parse("6884f7d7-ad1f-4101-8df3-7a6fa7387d81"),
                    Name = "Northland",
                    Code = "NTL",
                    RegionImageUrl = null
                },
                new Region
                {
                    Id = Guid.Parse("14ceba71-4b51-4777-9b17-46602cf66153"),
                    Name = "Bay Of Plenty",
                    Code = "BOP",
                    RegionImageUrl = null
                },
                new Region
                {
                    Id = Guid.Parse("cfa06ed2-bf65-4b65-93ed-c9d286ddb0de"),
                    Name = "Wellington",
                    Code = "WGN",
                    RegionImageUrl = "https://images.pexels.com/photos/4350631/pexels-photo-4350631.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1"
                },
                new Region
                {
                    Id = Guid.Parse("906cb139-415a-4bbb-a174-1a1faf9fb1f6"),
                    Name = "Nelson",
                    Code = "NSN",
                    RegionImageUrl = "https://images.pexels.com/photos/13918194/pexels-photo-13918194.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1"
                },
                new Region
                {
                    Id = Guid.Parse("f077a22e-4248-4bf6-b564-c7cf4e250263"),
                    Name = "Southland",
                    Code = "STL",
                    RegionImageUrl = null
                }
            };

            modelBuilder.Entity<Region>().HasData(regions);
            // seed data for Users

            var users = new List<User>()
            {
                new User()
                {
                    UserId = 1,
                    UserName = "Srikanth",
                    Email = "sri@gmail.com",
                    Password = "sri@1",
                    Role = "Admin"

                },
                new User()
                {
                    UserId = 2,
                    UserName = "Yamini",
                    Email = "yam@gmail.com",
                    Password = "sri@1",
                    Role = "User"

                }
            };
            modelBuilder.Entity<User>().HasData(users);
        }
    }
}
