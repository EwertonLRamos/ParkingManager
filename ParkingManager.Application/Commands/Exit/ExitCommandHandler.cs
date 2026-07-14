using ParkingManager.Application.DependencyInjection.Interfaces;
using ParkingManager.Domain.Entities;
using ParkingManager.Domain.Repositories;
using ParkingManager.Domain.Services;

namespace ParkingManager.Application.Commands.Exit;

public class ExitCommandHandler(
    IParkingSessionRepository parkingSessionRepository,
    ISectorRepository sectorRepository,
    ISpotRepository spotRepository) : ICommandHandler<ExitCommand>
{
    private readonly IParkingSessionRepository _parkingSessionRepository = parkingSessionRepository;
    private readonly ISectorRepository _sectorRepository = sectorRepository;
    private readonly ISpotRepository _spotRepository = spotRepository;

    public async Task HandleAsync(ExitCommand command)
    {
        var parkingSession = await _parkingSessionRepository.GetActiveSessionByLicensePlateAsync(command.LicensePlate) ?? 
            throw new InvalidOperationException($"No active parking session found for license plate {command.LicensePlate}.");

        decimal basePrice = 0m;
        
        if(HasParked(parkingSession))
        {
            var spot = await _spotRepository.GetByCoordinatesAsync(parkingSession.SpotLat!.Value, parkingSession.SpotLng!.Value) ?? 
                throw new InvalidOperationException($"Não existe nenhum ponto com as coordenadas: Lat. {parkingSession.SpotLat.Value} e Long. {parkingSession.SpotLng.Value}.");

            spot.Release();

            await _spotRepository.UpdateAsync(spot);

            var sector = await _sectorRepository.GetByIdAsync(spot.SectorId) ?? 
                throw new InvalidOperationException($"No sector found with ID {spot.SectorId}.");

            basePrice = sector.BasePrice;
        }

        var finalPrice = PricingService.CalculatePrice(parkingSession.EntryTime, command.ExitTime, basePrice, parkingSession.OccupancyRate);

        parkingSession.Finish(command.ExitTime, finalPrice);

        await _parkingSessionRepository.UpdateAsync(parkingSession);
    }

    private bool HasParked(ParkingSession parkingSession)
        => parkingSession.SpotLat is not null && parkingSession.SpotLng is not null;
}