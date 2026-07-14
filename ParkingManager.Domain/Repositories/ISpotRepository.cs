using ParkingManager.Domain.Entities;

namespace ParkingManager.Domain.Repositories;

public interface ISpotRepository
{
    Task<int> GetTotalOccupiedSpotsAsync();
    Task<Spot?> GetByCoordinatesAsync(decimal latitude, decimal longitude);
}