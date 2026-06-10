using CineTup.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CineTup.Infrastructure.Persistance
{
    public class CineTupDbContext : DbContext
    {
        public DbSet<Movie> Movies { get; set; }

        public CineTupDbContext(DbContextOptions<CineTupDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}