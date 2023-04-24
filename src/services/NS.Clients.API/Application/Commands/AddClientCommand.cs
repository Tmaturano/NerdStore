using FluentValidation;
using NS.Core.Messages;

namespace NS.Clients.API.Application.Commands;

public class AddClientCommand : Command
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Email { get; private set; }
    public string Cpf { get; private set; }

    public AddClientCommand(Guid id, string name, string email, string cpf)
    {
        AggregateId = id;
        Id = id;
        Name = name;
        Email = email;
        Cpf = cpf;
    }

    public override bool IsValid()
    {
        ValidationResult = new AddClientCommandValidation().Validate(this);
        return ValidationResult.IsValid;
    }

    public class AddClientCommandValidation : AbstractValidator<AddClientCommand>
    {
        public AddClientCommandValidation()
        {
            RuleFor(c => c.Id)
                .NotEqual(Guid.Empty)
                .WithMessage("Invalid Client Id.");

            RuleFor(c => c.Name)
                .NotEmpty()
                .WithMessage("The client name is required.");


            RuleFor(c => c.Cpf)
                .Must(HasValidCpf)
                .WithMessage("The given CPF is not valid.");

            RuleFor(c => c.Email)
                .Must(HasValidEmail)
                .WithMessage("The given e-mail is not valid.");
        }

        protected static bool HasValidCpf(string cpf) => Core.DomainObjects.Cpf.Validate(cpf);
        protected static bool HasValidEmail(string email) => Core.DomainObjects.Email.Validate(email);
    }
}
