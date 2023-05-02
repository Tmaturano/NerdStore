using EasyNetQ;
using FluentValidation.Results;
using NS.Clients.API.Application.Commands;
using NS.Core.Mediator;
using NS.Core.Messages.Integration;

namespace NS.Clients.API.Services;

/// <summary>
/// Keep alive, listening, on a singleton scope
/// </summary>
public class AddClientIntegrationHandler : BackgroundService
{
    private IBus _bus;
    private readonly IServiceProvider _serviceProvider;

    public AddClientIntegrationHandler(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _bus = RabbitHutch.CreateBus("host=localhost:5672");

        _bus.Rpc.RespondAsync<UserAddedIntegrationEvent, ResponseMessage>(async request =>
            new ResponseMessage(await CreateClient(request)), cancellationToken: stoppingToken);

        return Task.CompletedTask;
    }
        
    private async Task<ValidationResult> CreateClient(UserAddedIntegrationEvent message)
    {
        var clientCommand = new AddClientCommand(message.Id, message.Name, message.Email, message.Cpf);
        ValidationResult result;
        //IMediatorHandler is injected in a scoped scope, to work with it in a singleton scope, we need to manually create it by using the ServiceProvider
        //called: "Service Locator"
        using (var scope = _serviceProvider.CreateScope())
        {
            var mediator = scope.ServiceProvider.GetRequiredService<IMediatorHandler>();
            result = await mediator.SendCommand(clientCommand);
        }

        return result;
    }
}
