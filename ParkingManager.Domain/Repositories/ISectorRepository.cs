using ParkingManager.Domain.Entities;

namespace ParkingManager.Domain.Repositories;

public interface ISectorRepository
{
    Task<int> GetTotalSpotsAsync();
    Task<Sector?> GetByIdAsync(Guid sectorId);
    Task<List<Sector>> GetAllAsync();
    Task AddRangeByBatchAsync(IEnumerable<Sector> sectors);
}