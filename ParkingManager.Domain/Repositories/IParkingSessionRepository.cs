using ParkingManager.Domain.Entities;

namespace ParkingManager.Domain.Repositories;

public interface IParkingSessionRepository
{
    Task AddAsync(ParkingSession session);
    Task UpdateAsync(ParkingSession session);
    Task<ParkingSession?> GetActiveSessionByPlateAsync(string licensePlate);
}