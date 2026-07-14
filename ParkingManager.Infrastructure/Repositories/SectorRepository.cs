using Microsoft.EntityFrameworkCore;
using ParkingManager.Domain.Entities;
using ParkingManager.Domain.Repositories;
using ParkingManager.Infrastructure.Data;

namespace ParkingManager.Infrastructure.Repositories;

public class SectorRepository(ParkingManagerDbContext context) : ISectorRepository
{
    private readonly ParkingManagerDbContext _context = context;

    public async Task<int> GetTotalSpotsAsync()
        => await _context.Sectors.SumAsync(s => s.MaxCapacity);

    public async Task<Sector?> GetByIdAsync(Guid sectorId)
        => await _context.Sectors.FindAsync(sectorId);

    public async Task<List<Sector>> GetAllAsync()
        => await _context.Sectors.ToListAsync();
}