using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using ParkingManager.Application.DependencyInjection.Dispatchers;
using ParkingManager.Application.DependencyInjection.Interfaces;

namespace ParkingManager.Application.DependencyInjection.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCommandHandlers(this IServiceCollection services, Assembly assembly)
    {
        services.AddScoped<CommandDispatcher>();

    var handlerTypes = new[]
    {
        typeof(ICommandHandler<>),
        typeof(ICommandHandler<,>)
    };

    var handlers = assembly.GetTypes()
        .Where(t => t.IsClass && !t.IsAbstract)
        .SelectMany(t => t.GetInterfaces(),
            (type, iface) => new { Type = type, Interface = iface })
        .Where(x =>
            x.Interface.IsGenericType &&
            handlerTypes.Contains(x.Interface.GetGenericTypeDefinition()))
        .ToList();

    foreach (var handler in handlers)
        services.AddScoped(handler.Interface, handler.Type);

    return services;
    }
}