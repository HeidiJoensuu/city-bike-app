using Api.Models.Models;
using Api.Models.Models.Tables;
using Microsoft.EntityFrameworkCore;

namespace Api.Models
{
    public class CityBikesDBContext : DbContext
    {
        public CityBikesDBContext(DbContextOptions options) : base(options) { }

        public DbSet<Journey> Journeys { get; set; }
        public DbSet<June> June { get; set; }
        public DbSet<July> July { get; set; }
        //public DbSet<Station> Stations { get; set; }
        //public DbSet<StationInfo> StationsInfo { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Journey>();
            modelBuilder.Entity<June>();
            modelBuilder.Entity<July>();
        }


    }
}
