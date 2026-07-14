using ParkingManager.Application.DependencyInjection.Interfaces;
using ParkingManager.Domain.Entities;
using ParkingManager.Domain.Repositories;
using ParkingManager.Domain.Services;

namespace ParkingManager.Application.Commands.Entry;

public class EntryCommandHandler(
    IParkingSessionRepository parkingSessionRepository,
    ISectorRepository sectorRepository,
    ISpotRepository spotRepository) : ICommandHandler<EntryCommand>
{
    private readonly IParkingSessionRepository _parkingSessionRepository = parkingSessionRepository;
    private readonly ISectorRepository _sectorRepository = sectorRepository;
    private readonly ISpotRepository _spotRepository = spotRepository;

    public async Task HandleAsync(EntryCommand command)
    {
        var occupiedSpots = await _spotRepository.GetTotalOccupiedSpotsAsync();
        var totalSpots = await _sectorRepository.GetTotalSpotsAsync();

        var occupancyRate = OccupancyRateService.CalculateOccupancyRate(occupiedSpots, totalSpots);

        if(occupancyRate >= 1.0)
            throw new InvalidOperationException("The parking lot is at 100% capacity.");

        var parkingSession = new ParkingSession(command.LicensePlate, command.EntryTime, occupancyRate);

        await _parkingSessionRepository.AddAsync(parkingSession);
    }
}