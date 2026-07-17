using ParkingManager.API.DTOs.Garage;
using ParkingManager.Domain.Entities;
using ParkingManager.Domain.Repositories;

namespace ParkingManager.API.Extensions;

public static class HostExtensions
{
    public static async Task<IHost> FillDatabaseAsync(this IHost host)
    {
        using var scope = host.Services.CreateScope();
        var services = scope.ServiceProvider;

        var sectorRepository = services.GetRequiredService<ISectorRepository>();
        var spotRepository = services.GetRequiredService<ISpotRepository>();
        var httpClientFactory = services.GetRequiredService<IHttpClientFactory>();

        var existingSectors = await sectorRepository.GetAllAsync();
        if (existingSectors.Count != 0)
            return host;

        try
        {
            var client = httpClientFactory.CreateClient();
            
            //var response = await client.GetFromJsonAsync<GarageResponseDTO>("http://localhost:5000/garage");
            var response = await GetMockGarageAsync();

            if (response != null)
            {
                var sectors = response.Sectors
                    .Select(s => new Sector(s.Name, s.BasePrice, s.MaxCapacity))
                    .ToList();

                await sectorRepository.AddRangeByBatchAsync(sectors);

                var sectorsIds = (await sectorRepository.GetAllAsync())
                    .ToDictionary(s => s.Name, s => s.Id);

                var spots = response.Spots
                    .Select(s => new Spot((decimal)s.Latitude, (decimal)s.Longitude, sectorsIds[s.SectorName]))
                    .ToList();

                await spotRepository.AddRangeByBatchAsync(spots);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao sincronizar garagem: {ex.Message}");
        }

        return host;
    }

    private static Task<GarageResponseDTO> GetMockGarageAsync()
    {
        var mock = new GarageResponseDTO
        {
            Sectors =
            [
                new() { Name = "A", BasePrice = 10.0m, MaxCapacity = 100 },
                new() { Name = "B", BasePrice = 12.5m, MaxCapacity = 80 },
                new() { Name = "C", BasePrice = 8.0m,  MaxCapacity = 50 }
            ],

            Spots =
            [
                new() { Id = 1, SectorName = "A", Latitude = -23.561684, Longitude = -46.655981 },
                new() { Id = 2, SectorName = "A", Latitude = -23.561700, Longitude = -46.655990 },
                new() { Id = 3, SectorName = "A", Latitude = -23.561720, Longitude = -46.656000 },

                new() { Id = 4, SectorName = "B", Latitude = -23.562000, Longitude = -46.656200 },
                new() { Id = 5, SectorName = "B", Latitude = -23.562020, Longitude = -46.656220 },
                new() { Id = 6, SectorName = "B", Latitude = -23.562040, Longitude = -46.656240 },

                new() { Id = 7, SectorName = "C", Latitude = -23.563000, Longitude = -46.657000 },
                new() { Id = 8, SectorName = "C", Latitude = -23.563020, Longitude = -46.657020 }
            ]
        };

        return Task.FromResult(mock);
    }
}