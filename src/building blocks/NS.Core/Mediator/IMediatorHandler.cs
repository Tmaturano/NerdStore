using FluentValidation.Results;
using NS.Core.Messages;

namespace NS.Core.Mediator;

/// <summary>
/// Abstraction for mediator pattern
/// </summary>
public interface IMediatorHandler
{
    Task PublishEvent<T>(T eventMessage) where T : Event;
    Task<ValidationResult> SendCommand<T>(T command) where T : Command;
}
