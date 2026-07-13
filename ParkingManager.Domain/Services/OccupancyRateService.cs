namespace ParkingManager.Domain.Services;

public static class OccupancyRateService
{
    public static double CalculateOccupancyRate(int occupiedSpots, int totalSpots)
        => (double) occupiedSpots / totalSpots;
}