using ParkingManager.Domain.Entities;

namespace ParkingManager.Domain.Repositories;

public interface ISpotRepository
{
    Task AddAsync(Spot spot);
    Task UpdateAsync(Spot spot);
    Task<int> GetTotalOccupiedSpotsAsync();
    Task<Spot?> GetByCoordinatesAsync(decimal latitude, decimal longitude);
}