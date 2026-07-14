using ParkingManager.Application.DependencyInjection.Interfaces;
using ParkingManager.Domain.Repositories;

namespace ParkingManager.Application.Commands.Parked;

public class ParkedCommandHandler(IParkingSessionRepository parkingSessionRepository, ISpotRepository spotRepository) : ICommandHandler<ParkedCommand>
{
    private readonly IParkingSessionRepository _parkingSessionRepository = parkingSessionRepository;
    private readonly ISpotRepository _spotRepository = spotRepository;

    public async Task HandleAsync(ParkedCommand command)
    {
        var spot = await _spotRepository.GetByCoordinatesAsync(command.Latitude, command.Longitude) ?? 
            throw new InvalidOperationException($"No spot found with coordinates: Lat. {command.Latitude} and Long. {command.Longitude}.");

        if(spot.IsOccupied)
            throw new InvalidOperationException($"The spot at coordinates: Lat. {command.Latitude} and Long. {command.Longitude} is already occupied.");

        var parkingSession = await _parkingSessionRepository.GetActiveSessionByLicensePlateAsync(command.LicensePlate) ?? 
            throw new InvalidOperationException($"No active parking session found for license plate {command.LicensePlate}.");

        spot.Occupy();

        await _spotRepository.UpdateAsync(spot);

        parkingSession.Park(command.Latitude, command.Longitude);

        await _parkingSessionRepository.UpdateAsync(parkingSession);
    }
}