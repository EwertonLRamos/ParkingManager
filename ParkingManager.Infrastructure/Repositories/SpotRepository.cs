using Microsoft.EntityFrameworkCore;
using ParkingManager.Domain.Entities;
using ParkingManager.Domain.Repositories;
using ParkingManager.Infrastructure.Data;

namespace ParkingManager.Infrastructure.Repositories;

public class SpotRepository(ParkingManagerDbContext context) : ISpotRepository
{
    private readonly ParkingManagerDbContext _context = context;

    public async Task<int> GetTotalOccupiedSpotsAsync()
        => await _context.Spots.CountAsync(ps => ps.IsOccupied);

    public async Task<Spot?> GetByCoordinatesAsync(decimal latitude, decimal longitude)
        => await _context.Spots.FirstOrDefaultAsync(s => 
                s.Latitude == latitude && 
                s.Longitude == longitude
            );

    public async Task AddAsync(Spot spot)
    {
        await _context.Spots.AddAsync(spot);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Spot spot)
    {
        _context.Spots.Update(spot);
        await _context.SaveChangesAsync();
    }
}