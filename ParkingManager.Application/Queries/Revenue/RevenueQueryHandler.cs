using ParkingManager.Application.DependencyInjection.Interfaces;
using ParkingManager.Domain.Abstractions;
using ParkingManager.Domain.Repositories;

namespace ParkingManager.Application.Queries.Revenue;

public class RevenueQueryHandler(
    ISectorRepository sectorRepository, 
    IParkingSessionRepository parkingSessionRepository,
    IBusinessSettings businessSettings) : ICommandHandler<RevenueQuery, RevenueQueryResponse>
{
    private readonly ISectorRepository _sectorRepository = sectorRepository;
    private readonly IParkingSessionRepository _parkingSessionRepository = parkingSessionRepository;
    private readonly IBusinessSettings _businessSettings = businessSettings;

    public async Task<RevenueQueryResponse> HandleAsync(RevenueQuery query)
    {
        var sector = await _sectorRepository.GetByNameAsync(query.Sector) ?? 
            throw new ArgumentException($"Sector with name {query.Sector} not found.");

        var finishedSessions = await _parkingSessionRepository.GetFinishedSessionsByExitDateAsync(query.Date);

        var revenue = finishedSessions
            .Where(session => session.SectorId == sector.Id)
            .Sum(session => session.TotalAmount);

        return new RevenueQueryResponse(revenue!.Value, _businessSettings.Currency, DateTime.UtcNow);
    }
}