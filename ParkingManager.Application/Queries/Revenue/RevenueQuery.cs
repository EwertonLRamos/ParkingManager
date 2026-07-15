using ParkingManager.Application.DependencyInjection.Interfaces;

namespace ParkingManager.Application.Queries.Revenue;

public record RevenueQuery(DateOnly Date, string Sector) : ICommand;