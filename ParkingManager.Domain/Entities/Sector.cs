namespace ParkingManager.Domain.Entities;

public class Sector(string name, decimal basePrice, int maxCapacity)
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string Name { get; private set; } = name;
    public decimal BasePrice { get; private set; } = basePrice;
    public int MaxCapacity { get; private set; } = maxCapacity;

    public List<Spot> Spots { get; private set; } = [];
    public List<ParkingSession> ParkingSessions { get; private set; } = [];
}