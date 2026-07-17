using Microsoft.EntityFrameworkCore;
using ParkingManager.Domain.Entities;

namespace ParkingManager.Infrastructure.Data;

public class ParkingManagerDbContext(DbContextOptions<ParkingManagerDbContext> options) : DbContext(options)
{
    public DbSet<Sector> Sectors { get; set; }
    public DbSet<Spot> Spots { get; set; }
    public DbSet<ParkingSession> ParkingSessions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Sector>(entity =>
        {
            entity.ToTable("Sectors");


            entity.HasKey(s => s.Id);

            entity.Property(s => s.Name)
                .IsRequired()
                .HasMaxLength(5);

            entity.Property(s => s.BasePrice)
                .HasPrecision(10, 2)
                .IsRequired();

            entity.Property(s => s.MaxCapacity)
                .IsRequired();


            entity.HasMany(s => s.Spots)
                .WithOne(s => s.Sector)
                .HasForeignKey(s => s.SectorId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasMany(s => s.ParkingSessions)
                .WithOne(ps => ps.Sector)
                .HasForeignKey(ps => ps.SectorId)
                .OnDelete(DeleteBehavior.SetNull);


            entity.HasIndex(s => s.Name).IsUnique();
        });

        modelBuilder.Entity<Spot>(entity =>
        {
            entity.ToTable("Spots");


            entity.HasKey(s => s.Id);

            entity.Property(s => s.Latitude)
                .HasPrecision(12, 6)
                .IsRequired();

            entity.Property(s => s.Longitude)
                .HasPrecision(12, 6)
                .IsRequired();

            entity.Property(s => s.SectorId)
                .IsRequired();

            entity.Property(s => s.IsOccupied)
                .IsRequired();


            entity.HasOne(s => s.Sector)
                .WithMany(s => s.Spots)
                .HasForeignKey(s => s.SectorId);

            entity.HasMany(s => s.ParkingSessions)
                .WithOne(ps => ps.Spot)
                .HasForeignKey(ps => ps.SpotId)
                .OnDelete(DeleteBehavior.Restrict);


            entity.HasIndex(s => new { s.Latitude, s.Longitude });
        });

        modelBuilder.Entity<ParkingSession>(entity =>
        {
            entity.ToTable("ParkingSessions");


            entity.HasKey(ps => ps.Id);

            entity.Property(ps => ps.LicensePlate)
                .IsRequired()
                .HasMaxLength(7);

            entity.Property(ps => ps.EntryTime)
                .IsRequired();

            entity.Property(ps => ps.ExitTime)
                .IsRequired(false);

            entity.Property(ps => ps.OccupancyRate)
                .IsRequired();

            entity.Property(ps => ps.Status)
                .IsRequired()
                .HasConversion<string>();

            entity.Property(ps => ps.TotalAmount)
                .HasPrecision(10, 2)
                .IsRequired(false);

            
            entity.HasOne(ps => ps.Spot)
                .WithMany(s => s.ParkingSessions)
                .HasForeignKey(ps => ps.SpotId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(ps => ps.Sector)
                .WithMany(s => s.ParkingSessions)
                .HasForeignKey(ps => ps.SectorId)
                .OnDelete(DeleteBehavior.Restrict);


            entity.HasIndex(ps => ps.LicensePlate);

            entity.HasIndex(ps => ps.Status);

            entity.HasIndex(ps => ps.EntryTime);

            entity.HasIndex(ps => new { ps.Status, ps.ExitTime });
        });
    }
}