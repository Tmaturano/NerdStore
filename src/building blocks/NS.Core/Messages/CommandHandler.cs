using FluentValidation.Results;
using NS.Core.Data;

namespace NS.Core.Messages
{
    public abstract class CommandHandler
    {
        protected ValidationResult ValidationResult;

        protected CommandHandler() => ValidationResult = new ValidationResult();

        protected void AddError(string message) => ValidationResult.Errors.Add(new ValidationFailure(propertyName: string.Empty, errorMessage: message));

        protected async Task<ValidationResult> PersistData(IUnitOfWork uow)
        {
            if (!await uow.CommitAsync()) AddError("There was an error persisting the data");

            return ValidationResult;
        }
    }
}
