using FluentValidation.Results;
using MediatR;
using NS.Core.Mediator;
using NS.Orders.API.Application.Commands;
using NS.Orders.API.Application.Events;
using NS.Orders.API.Application.Queries;
using NS.Orders.Domain;
using NS.Orders.Domain.Orders;
using NS.Orders.Infra.Data;
using NS.Orders.Infra.Data.Repository;
using NS.WebApi.Core.User;

namespace NS.Orders.API.Configuration;

public static class DependencyInjectionConfig
{
    public static void RegisterServices(this IServiceCollection services)
    {
        // API
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        services.AddScoped<IAspNetUser, AspNetUser>();

        // Commands
        services.AddScoped<IRequestHandler<AddOrderCommand, ValidationResult>, OrderCommandHandler>();

        // Events
        services.AddScoped<INotificationHandler<OrderPlacedEvent>, OrderEventHandler>();

        // Application
        services.AddScoped<IMediatorHandler, MediatorHandler>();
        services.AddScoped<IVoucherQueries, VoucherQueries>();
        services.AddScoped<IOrderQueries, OrderQueries>();

        // Data
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IVoucherRepository, VoucherRepository>();
        services.AddScoped<OrdersContext>();
    }
}
