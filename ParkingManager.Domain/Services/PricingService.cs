namespace ParkingManager.Domain.Services;

public static class PricingService
{
    public static decimal CalculatePrice(DateTime entryTime, DateTime exitTime, decimal basePrice, double occupancyRate)
    {
        decimal multiplier = 1.0m;

        if(occupancyRate < 0.25)
            multiplier = 0.9m;
        
        else if(occupancyRate <= 0.50)
            multiplier = 1.0m;
        
        else if(occupancyRate <= 0.75)
            multiplier = 1.1m;

        else if(occupancyRate > 0.75)
            multiplier = 1.25m;

        var totalHours = CalculateTotalHours(entryTime, exitTime);

        return (decimal)totalHours * basePrice * multiplier;
    }

    private static double CalculateTotalHours(DateTime entryTime, DateTime exitTime)
    {
        var duration = exitTime - entryTime;
        
        if(duration.TotalMinutes <= 30)
            return 0.0;
        
        return Math.Ceiling(duration.TotalHours);
    }
}