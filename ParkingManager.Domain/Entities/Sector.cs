namespace ParkingManager.Domain.Entities;

public class Sector
{
    public int Id { get; private set; }
    public string Name { get; private set; }
    public List<Spot> Spots { get; private set; }

    public Sector(int id, string name)
    {
        Id = id;
        Name = name;
        Spots = new List<Spot>();
    }

    public void AddSpot(Spot spot)
    {
        Spots.Add(spot);
    }
}