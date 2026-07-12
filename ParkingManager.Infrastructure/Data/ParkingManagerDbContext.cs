using Microsoft.EntityFrameworkCore;
using ParkingManager.Domain.Entities;

namespace ParkingManager.Infrastructure.Data;

public class ParkingManagerDbContext : DbContext
{
    public ParkingManagerDbContext(DbContextOptions<ParkingManagerDbContext> options) : base(options)
    {
    }

    public DbSet<Sector> Sectors { get; set; }
    public DbSet<Spot> Spots { get; set; }
    public DbSet<Vehicle> Vehicles { get; set; }
    public DbSet<ParkingSession> ParkingSessions { get; set; }
}