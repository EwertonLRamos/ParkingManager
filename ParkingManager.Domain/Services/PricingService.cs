namespace ParkingManager.Domain.Services;

public static class PricingService
{
    public static decimal CalculatePrice(DateTime entryTime, DateTime exitTime, decimal basePrice, double occupancyRate)
    {
        var duration = exitTime - entryTime;

        if(duration.TotalMinutes <= 30)
            return 0m;

        decimal multiplier = 1.0m;

        if(occupancyRate < 0.25)
            multiplier = 0.9m;
        
        else if(occupancyRate <= 0.50)
            multiplier = 1.0m;
        
        else if(occupancyRate <= 0.75)
            multiplier = 1.1m;

        else if(occupancyRate > 0.75)
            multiplier = 1.25m; 

        var totalHours = Math.Ceiling(duration.TotalHours);

        return (decimal)totalHours * basePrice * multiplier;
    }
}