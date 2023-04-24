using FluentValidation.Results;
using MediatR;
using NS.Clients.API.Application.Commands;
using NS.Core.Mediator;

namespace NS.Clients.API.Configuration;

public static class DependencyInjectionConfig
{
    public static void RegisterServices(this IServiceCollection services)
    {
        //services.AddScoped<CatalogContext>();
        services.AddScoped<IMediatorHandler, MediatorHandler>();
        services.AddScoped<IRequestHandler<AddClientCommand, ValidationResult>, ClientCommandHandler>();
    }
}
