using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace ChickenSimulator.Models
{
    public partial class ChickenSimulatorContext : DbContext
    {
      
        public ChickenSimulatorContext()
        {
        }

        public ChickenSimulatorContext(DbContextOptions<ChickenSimulatorContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Chicken> Chickens { get; set; }
        public virtual DbSet<Farm> Farms { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.\\SQLExpress;Database=ChickenSimulator;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

           

            modelBuilder.Entity<Farm>(entity =>
            {
                entity.ToTable("Farm");

                entity.Property(e => e.Name).HasMaxLength(55);
            });


             modelBuilder.Entity<Chicken>(entity =>
            {
                entity.ToTable("Chicken");

                entity.Property(e => e.Color).HasMaxLength(55);

                entity.Property(e => e.Name).HasMaxLength(55);

                entity.HasOne(d => d.Farm)
                    .WithMany(p => p.Chickens)
                    .HasForeignKey(d => d.FarmId)
                    .HasConstraintName("FK__Chicken__FarmId__29572725");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
