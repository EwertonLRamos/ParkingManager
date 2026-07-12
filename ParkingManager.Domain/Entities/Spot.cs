namespace ParkingManager.Domain.Entities;

public class Spot
{
    public int Id { get; private set; }
    public string Name { get; private set; }
    public ParkingSession? CurrentSession { get; private set; }

    public Spot(int id, string name)
    {
        Id = id;
        Name = name;
    }

    public void StartSession(ParkingSession session)
    {
        CurrentSession = session;
    }

    public void EndSession()
    {
        CurrentSession = null;
    }
}