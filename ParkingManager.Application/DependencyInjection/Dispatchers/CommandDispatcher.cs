using Microsoft.Extensions.DependencyInjection;
using ParkingManager.Application.DependencyInjection.Interfaces;

namespace ParkingManager.Application.DependencyInjection.Dispatchers;

public class CommandDispatcher
{
    private readonly IServiceProvider _serviceProvider;

    public CommandDispatcher(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public void Dispatch<TCommand>(TCommand command) where TCommand : ICommand
    {
        var handler = _serviceProvider.GetRequiredService<ICommandHandler<TCommand>>();
        handler.Handle(command);
    }
}