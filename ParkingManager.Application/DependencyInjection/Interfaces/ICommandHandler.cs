namespace ParkingManager.Application.DependencyInjection.Interfaces;

public interface ICommandHandler<in TCommand>
    where TCommand : ICommand
{
    Task HandleAsync(TCommand command);
}

public interface ICommandHandler<in TCommand, TResponse>
    where TCommand : ICommand
    where TResponse : ICommandResponse
{
    Task<TResponse> HandleAsync(TCommand command);
}