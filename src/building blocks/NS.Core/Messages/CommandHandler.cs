using FluentValidation.Results;

namespace NS.Core.Messages
{
    public abstract class CommandHandler
    {
        protected ValidationResult ValidationResult;

        protected CommandHandler() => ValidationResult = new ValidationResult();

        protected void AddError(string message) => ValidationResult.Errors.Add(new ValidationFailure(propertyName: string.Empty, errorMessage: message));
    }
}
