namespace ParkingManager.Domain.Entities;

public class Spot
{
    public int Id { get; private set; }
    public string Name { get; private set; }
    public ParkingSession? CurrentSession { get; private set; }
    public bool IsOcupied { get; private set; }

    public Spot(int id, string name)
    {
        Id = id;
        Name = name;
        IsOcupied = false;
    }

    public void Occupy(ParkingSession session)
    {
        CurrentSession = session;
        IsOcupied = true;
    }

    public void Release()
    {
        CurrentSession = null;
        IsOcupied = false;
    }
}