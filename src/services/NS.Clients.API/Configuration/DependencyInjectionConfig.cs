using FluentValidation.Results;
using MediatR;
using NS.Clients.API.Application.Commands;
using NS.Clients.API.Application.Events;
using NS.Clients.API.Data;
using NS.Clients.API.Data.Repository;
using NS.Clients.API.Models;
using NS.Core.Mediator;

namespace NS.Clients.API.Configuration;

public static class DependencyInjectionConfig
{
    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<ClientContext>();
        services.AddScoped<IClientRepository, ClientRepository>();

        services.AddScoped<IMediatorHandler, MediatorHandler>();
        services.AddScoped<IRequestHandler<AddClientCommand, ValidationResult>, ClientCommandHandler>();

        services.AddScoped<INotificationHandler<ClientAddedEvent>, ClientEventHandler>();
    }
}
