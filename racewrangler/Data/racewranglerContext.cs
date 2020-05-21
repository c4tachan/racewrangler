using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using racewrangler.Models;

namespace racewrangler.Data
{
    public class racewranglerContext : DbContext
    {
        public racewranglerContext (DbContextOptions<racewranglerContext> options)
            : base(options)
        {
        }

        public DbSet<racewrangler.Models.Site> Sites { get; set; }

        public DbSet<racewrangler.Models.Car> Cars { get; set; }

        public DbSet<racewrangler.Models.Driver> Drivers { get; set; }

        public DbSet<racewrangler.Models.Season> Seasons { get; set; }

        public DbSet<racewrangler.Models.Competition> Competitions { get; set; }

        public DbSet<racewrangler.Models.Organizer> Organizers { get; set; }

        public DbSet<racewrangler.Models.Classification> Classes { get; set; }

        public DbSet<racewrangler.Models.RaceEntry> RaceEntries { get; set; }
        public DbSet<racewrangler.Models.Run> Runs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            if (null == modelBuilder)
            {
                throw new ArgumentNullException(nameof(modelBuilder));
            }

            modelBuilder.Entity<Site>().ToTable("Sites");
            modelBuilder.Entity<Car>().ToTable("Cars");
            modelBuilder.Entity<Driver>().ToTable("Drivers");
            modelBuilder.Entity<Season>().ToTable("Seasons");
            modelBuilder.Entity<Competition>().ToTable("Competitions");
            modelBuilder.Entity<Organizer>().ToTable("Organizers");
            modelBuilder.Entity<Classification>().ToTable("Classes");
            modelBuilder.Entity<RaceEntry>().ToTable("RaceEntries");
            modelBuilder.Entity<Run>().ToTable("Runs");
        }

    }
}
