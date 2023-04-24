using FluentValidation.Results;
using MediatR;
using NS.Core.Messages;

namespace NS.Core.Mediator;

public class MediatorHandler : IMediatorHandler
{
    private readonly IMediator _mediator;

    public MediatorHandler(IMediator mediator) => _mediator = mediator;


    public async Task PublishEvent<T>(T eventMessage) where T : Event => await _mediator.Publish(eventMessage);

    public async Task<ValidationResult> SendCommand<T>(T command) where T : Command => await _mediator.Send(command);
}
