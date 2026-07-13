using ParkingManager.Application.DependencyInjection.Interfaces;

namespace ParkingManager.Application.Commands.Parked;

public class ParkedCommandHandler : ICommandHandler<ParkedCommand>
{
    public void Handle(ParkedCommand command)
    {
    }
}