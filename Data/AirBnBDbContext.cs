using Microsoft.EntityFrameworkCore;
using AirBnb.Models;
using System.Collections.Generic;

namespace AirBnb.Data
{
    public class AirBnBDbContext : DbContext
    {
        public AirBnBDbContext(DbContextOptions<AirBnBDbContext> options)
            : base(options)
        {
        }

        public DbSet<Landlord> Landlords { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Customers> Customers { get; set; }
        public DbSet<Reservations> Reservations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Location>()
                .HasMany(l => l.Images)
                .WithOne(i => i.Location)
                .HasForeignKey(i => i.LocationId);

            modelBuilder.Entity<Location>()
                .HasMany(l => l.Reservations)
                .WithOne(r => r.Location)
                .HasForeignKey(r => r.LocationId);

            modelBuilder.Entity<Landlord>()
                .HasMany(l => l.Locations)
                .WithOne(loc => loc.Landlord)
                .HasForeignKey(loc => loc.LandlordId);

            modelBuilder.Entity<Customers>()
                .HasMany(c => c.Reservations)
                .WithOne(r => r.Customer)
                .HasForeignKey(r => r.CustomerId);

            modelBuilder.Entity<Landlord>()
                .HasOne(l => l.Avatar)
                .WithOne()
                .HasForeignKey<Landlord>(l => l.AvatarId);

            base.OnModelCreating(modelBuilder);

            // Seeding data
            modelBuilder.Entity<Landlord>().HasData(
                new Landlord { Id = 1, FirstName = "John", LastName = "Doe", AvatarId = 1 },
                new Landlord { Id = 2, FirstName = "Jane", LastName = "Doe", AvatarId = 2 }
            );

            modelBuilder.Entity<Image>().HasData(
                new Image { Id = 1, Url = "https://example.com/avatar1.jpg" },
                new Image { Id = 2, Url = "https://example.com/avatar2.jpg" },
                new Image { Id = 3, Url = "https://example.com/location1.jpg", IsCover = true, LocationId = 1 },
                new Image { Id = 4, Url = "https://example.com/location2.jpg", IsCover = true, LocationId = 2 }
            );

            modelBuilder.Entity<Location>().HasData(
                new Location { Id = 1, Title = "Beautiful Beach House", SubTitle = "Near the sea", Description = "A beautiful beach house.", LandlordId = 1, PricePerDay = 200, Type = LocationType.Cottage, Features = Features.Smoking },
                new Location { Id = 2, Title = "Mountain Cabin", SubTitle = "In the mountains", Description = "A cozy mountain cabin.", LandlordId = 2, PricePerDay = 150, Type = LocationType.Apartment, Features = Features.Bath },
                new Location
                {
                    Id = 3,
                    Title = "The Farmhouse",
                    SubTitle = "Plenty of space",
                    Description = "The campsite is hidden behind the farm in the polder. Within cycling distance (5 minutes) are the village of Nieuwvliet, the sea, the beach, the Erasmus forest and the Knokkert nature reserve.",
                    Rooms = 5,
                    NumberOfGuests = 12,
                    PricePerDay = 300,
                    Type = LocationType.Cottage,
                    Features = Features.Smoking,
                    LandlordId = 1
                },
                new Location
                {
                    Id = 4,
                    Title = "Frankie's Penthouse",
                    SubTitle = "Amazing view",
                    Description = "No, this excellent penthouse that has been for sale for years and has now been sold is not the most expensive home in our country. Not by a long shot. However, the more than €30,000 per square meter of living space is a record in our country. country.",
                    Rooms = 2,
                    NumberOfGuests = 4,
                    PricePerDay = 400,
                    Type = LocationType.Apartment,
                    Features = Features.Bath,
                    LandlordId = 2
                }
            );

            modelBuilder.Entity<Image>().HasData(
                new Image { Id = 5, Url = "url-to-avatar", IsCover = true, LocationId = 3 },
                new Image { Id = 6, Url = "url-to-avatar-2", IsCover = true, LocationId = 4 }
            );
        }
    }
}