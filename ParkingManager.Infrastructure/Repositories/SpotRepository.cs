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
}