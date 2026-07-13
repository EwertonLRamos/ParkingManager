using System.ComponentModel.DataAnnotations;

namespace ParkingManager.Domain.Entities;

public class Sector
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public decimal BasePrice { get; private set; }
    public int MaxCapacity { get; private set; }

    public List<Spot> Spots { get; private set; }

    public Sector(string name, decimal basePrice, int maxCapacity)
    {
        Id = Guid.NewGuid();
        Name = name;
        BasePrice = basePrice;
        MaxCapacity = maxCapacity;
    }
}