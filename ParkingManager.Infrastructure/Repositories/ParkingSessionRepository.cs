using Microsoft.EntityFrameworkCore;
using ParkingManager.Domain.Entities;
using ParkingManager.Domain.Enums;
using ParkingManager.Domain.Repositories;
using ParkingManager.Infrastructure.Data;

namespace ParkingManager.Infrastructure.Repositories;

public class ParkingSessionRepository(ParkingManagerDbContext context) : IParkingSessionRepository
{
    private readonly ParkingManagerDbContext _context = context;

    public async Task AddAsync(ParkingSession session)
    {
        await _context.ParkingSessions.AddAsync(session);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(ParkingSession session)
    {
        _context.ParkingSessions.Update(session);
        await _context.SaveChangesAsync();
    }

    public async Task<ParkingSession?> GetActiveSessionByLicensePlateAsync(string licensePlate)
    {
        return await _context.ParkingSessions
            .FirstOrDefaultAsync(ps => 
                ps.LicensePlate == licensePlate && 
                ps.Status == SessionStatus.Active
            );
    }
}