using Microsoft.Extensions.DependencyInjection;
using ParkingManager.Application.DependencyInjection.Interfaces;

namespace ParkingManager.Application.DependencyInjection.Dispatchers;

public class CommandDispatcher(IServiceProvider serviceProvider)
{
    private readonly IServiceProvider _serviceProvider = serviceProvider;

    public async Task DispatchAsync<TCommand>(TCommand command) 
        where TCommand : ICommand
    {
        var handler = _serviceProvider.GetRequiredService<ICommandHandler<TCommand>>();
        await handler.HandleAsync(command);
    }

    public async Task<TResponse> DispatchAsync<TCommand, TResponse>(TCommand command)
        where TCommand : ICommand
        where TResponse : ICommandResponse
    {
        var handler = _serviceProvider.GetRequiredService<ICommandHandler<TCommand, TResponse>>();

        return await handler.HandleAsync(command);
    }
}