﻿using Api.Models.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Models
{
    public class CityBikesDBContext : DbContext
    {
        public CityBikesDBContext(DbContextOptions options) : base(options) { }
        public CityBikesDBContext() { }
        public virtual DbSet<July> Julys { get; set; }
        public virtual DbSet<June> Junes { get; set; }
        public virtual DbSet<May> Mays { get; set; }
        public virtual DbSet<Journey> Journeys { get; set; }
        public virtual DbSet<Station> Stations { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<July>();
            modelBuilder.Entity<June>();
            modelBuilder.Entity<May>();
            modelBuilder.Entity<Journey>();
            modelBuilder.Entity<Station>();
        }


    }
}
