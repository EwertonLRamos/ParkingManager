using ParkingManager.Application.DependencyInjection.Interfaces;

namespace ParkingManager.Application.Commands.Entry;

public class EntryCommandHandler : ICommandHandler<EntryCommand>
{
    public Task HandleAsync(EntryCommand command)
    {
        // Implementation for handling entry command
        return Task.CompletedTask;
    }
}