using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace racewrangler
{
    public partial class autocrossContext : DbContext
    {
        public autocrossContext()
        {
        }

        public autocrossContext(DbContextOptions<autocrossContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Car> Car { get; set; }
        public virtual DbSet<Classification> Classification { get; set; }
        public virtual DbSet<Competition> Competition { get; set; }
        public virtual DbSet<Entrant> Entrant { get; set; }
        public virtual DbSet<Location> Location { get; set; }
        public virtual DbSet<Run> Run { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if(null == optionsBuilder)
            {
                throw new ArgumentNullException(nameof(optionsBuilder));
            }
            if (!optionsBuilder.IsConfigured)
            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseNpgsql("Host=racewranglerdb.cq8mrpiprozz.us-east-2.rds.amazonaws.com; Database=autocross;user id=postgres;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            if (null == modelBuilder)
            {
                throw new ArgumentNullException(nameof(modelBuilder));
            }

            modelBuilder.Entity<Car>(entity =>
            {
                entity.ToTable("car", "autocross");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasColumnType("character varying");

                entity.Property(e => e.Make)
                    .HasColumnName("make")
                    .HasColumnType("character varying");

                entity.Property(e => e.Model)
                    .HasColumnName("model")
                    .HasColumnType("character varying");

                entity.Property(e => e.Number)
                    .IsRequired()
                    .HasColumnName("number")
                    .HasColumnType("character varying");

                entity.Property(e => e.Year)
                    .HasColumnName("year")
                    .HasColumnType("character varying");
            });

            modelBuilder.Entity<Classification>(entity =>
            {
                entity.ToTable("classification", "autocross");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Abreviation)
                    .HasColumnName("abreviation")
                    .HasColumnType("character varying");

                entity.Property(e => e.Handicap).HasColumnName("handicap");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("character varying");
            });

            modelBuilder.Entity<Competition>(entity =>
            {
                entity.ToTable("competition", "autocross");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Date)
                    .HasColumnName("date")
                    .HasColumnType("date");

                entity.Property(e => e.LocationId).HasColumnName("locationId");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("character varying");

                entity.Property(e => e.PenaltyCost).HasColumnName("penaltyCost");

                entity.HasOne(d => d.Location)
                    .WithMany(p => p.Competition)
                    .HasForeignKey(d => d.LocationId)
                    .HasConstraintName("locationId");
            });

            modelBuilder.Entity<Entrant>(entity =>
            {
                entity.HasKey(e => new { e.Id, e.CompId })
                    .HasName("entry_pkey");

                entity.ToTable("entrant", "autocross");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CompId).HasColumnName("compId");

                entity.Property(e => e.CarId).HasColumnName("carId");

                entity.Property(e => e.ClassId).HasColumnName("classId");

                entity.Property(e => e.FirstName)
                    .HasColumnName("firstName")
                    .HasColumnType("character varying");

                entity.Property(e => e.LastName)
                    .HasColumnName("lastName")
                    .HasColumnType("character varying");

                entity.Property(e => e.SeasonPoints).HasColumnName("seasonPoints");

                entity.HasOne(d => d.Comp)
                    .WithMany(p => p.Entrant)
                    .HasForeignKey(d => d.CompId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("eventId_fkey");
            });

            modelBuilder.Entity<Location>(entity =>
            {
                entity.ToTable("location", "autocross");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.City)
                    .HasColumnName("city")
                    .HasColumnType("character varying");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("character varying");

                entity.Property(e => e.State)
                    .HasColumnName("state")
                    .HasColumnType("character varying");

                entity.Property(e => e.Zip)
                    .HasColumnName("zip")
                    .HasColumnType("character varying");
            });

            modelBuilder.Entity<Run>(entity =>
            {
                entity.HasKey(e => new { e.Id, e.CompId, e.EntryId })
                    .HasName("run_pkey");

                entity.ToTable("run", "autocross");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CompId).HasColumnName("compId");

                entity.Property(e => e.EntryId).HasColumnName("entryId");

                entity.Property(e => e.Disqualified).HasColumnName("disqualified");

                entity.Property(e => e.PenaltyCount).HasColumnName("penaltyCount");

                entity.Property(e => e.RunTime).HasColumnName("runTime");

                entity.Property(e => e.TimeStart).HasColumnName("timeStart");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
