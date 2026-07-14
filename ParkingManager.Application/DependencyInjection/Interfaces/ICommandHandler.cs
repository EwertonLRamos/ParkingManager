namespace ParkingManager.Application.DependencyInjection.Interfaces;
public interface ICommandHandler<in TCommand>
{
    Task HandleAsync(TCommand command);
}