using Api.Models.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Models
{
    public class CityBikesDBContext : DbContext
    {
        public CityBikesDBContext(DbContextOptions options) : base(options) { }

        public DbSet<Journey> Journeys { get; set; }
        public DbSet<Station> Stations { get; set; }
        public DbSet<StationInfo> StationsInfo { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Journey>();
            modelBuilder.Entity<Station>();
            modelBuilder.Entity<StationInfo>();
        }


    }
}
