using ParkingManager.Domain.Entities;

namespace ParkingManager.Domain.Repositories;

public interface IParkingSessionRepository
{
    Task AddAsync(ParkingSession session);
    Task UpdateAsync(ParkingSession session);
    Task<ParkingSession?> GetActiveSessionByLicensePlateAsync(string licensePlate);
    Task<List<ParkingSession>> GetFinishedSessionsByExitDateAsync(DateOnly exitDate);
}