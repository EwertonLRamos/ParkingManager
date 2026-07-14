using ParkingManager.Application.DependencyInjection.Interfaces;

namespace ParkingManager.Application.Commands.Exit;

public class ExitCommandHandler : ICommandHandler<ExitCommand>
{
    public Task HandleAsync(ExitCommand command)
    {
        // Implementation for handling exit command
        return Task.CompletedTask;
    }
}