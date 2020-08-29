﻿using IMDb.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace IMDb.Data.Context
{
    public class IMDbContext : DbContext
    {
        public IMDbContext(DbContextOptions<IMDbContext> options) : base(options) { }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<RatingOfMovie> MovieRatings { get; set; }
        public DbSet<Cast> Cast { get; set; }
        public DbSet<CastOfMovie> CastOfMovies { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}
