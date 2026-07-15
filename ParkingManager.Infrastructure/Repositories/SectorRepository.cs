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

    public async Task AddRangeByBatchAsync(IEnumerable<Sector> sectors)
    {
        const int batchSize = 500;

        _context.ChangeTracker.AutoDetectChangesEnabled = false;

        var batch = new List<Sector>(batchSize);

        try
        {    
            foreach (var sector in sectors)
            {
                batch.Add(sector);

                if (batch.Count == batchSize)
                {
                    await _context.Sectors.AddRangeAsync(batch);
                    await _context.SaveChangesAsync();

                    _context.ChangeTracker.Clear();
                    batch.Clear();
                }
            }

            if (batch.Count != 0)
            {
                await _context.Sectors.AddRangeAsync(batch);
                await _context.SaveChangesAsync();

                _context.ChangeTracker.Clear();
            }
        }
        finally
        {
            _context.ChangeTracker.AutoDetectChangesEnabled = true;
        }
    }
}