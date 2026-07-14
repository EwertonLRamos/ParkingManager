using ParkingManager.Application.DependencyInjection.Interfaces;

namespace ParkingManager.Application.Commands.Parked;

public class ParkedCommandHandler : ICommandHandler<ParkedCommand>
{
    public Task HandleAsync(ParkedCommand command)
    {
        // Implementation for handling parked command
        return Task.CompletedTask;
    }
}