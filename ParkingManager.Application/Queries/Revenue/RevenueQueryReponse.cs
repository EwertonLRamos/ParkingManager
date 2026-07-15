using ParkingManager.Application.DependencyInjection.Interfaces;

namespace ParkingManager.Application.Queries.Revenue;   

public record RevenueQueryResponse(decimal Amount, string Currency, DateTime Timestamp) : ICommandResponse;