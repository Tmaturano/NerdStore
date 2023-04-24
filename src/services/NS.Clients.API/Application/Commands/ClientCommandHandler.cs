using FluentValidation.Results;
using MediatR;
using NS.Clients.API.Models;
using NS.Core.Messages;

namespace NS.Clients.API.Application.Commands;

public class ClientCommandHandler : CommandHandler, IRequestHandler<AddClientCommand, ValidationResult>
{
    public async Task<ValidationResult> Handle(AddClientCommand message, CancellationToken cancellationToken)
    {
        if (!message.IsValid()) return message.ValidationResult;

        var client = new Client(message.Id, message.Name, message.Email, message.CPF);
        //business logic

        //persist in db
        if (true) //client already exists in db with the given CPF
        {
            AddError("This CPF has been already choosen");
            return ValidationResult;
        }

        return message.ValidationResult;
    }
}
