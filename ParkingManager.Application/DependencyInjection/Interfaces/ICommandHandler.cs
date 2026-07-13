namespace ParkingManager.Application.DependencyInjection.Interfaces;
public interface ICommandHandler<in TCommand>
{
    void Handle(TCommand command);
}