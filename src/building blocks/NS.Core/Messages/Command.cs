using FluentValidation.Results;

namespace NS.Core.Messages;

public abstract class Command : Message
{
    public DateTime Timestamp { get; private set; }
    public ValidationResult ValidationResult { get; set; }

    protected Command() => Timestamp = DateTime.UtcNow;

    public virtual bool IsValid() => throw new NotImplementedException();
}
