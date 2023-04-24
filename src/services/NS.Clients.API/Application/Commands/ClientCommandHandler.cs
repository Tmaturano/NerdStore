using FluentValidation.Results;
using MediatR;
using NS.Clients.API.Models;
using NS.Core.Messages;

namespace NS.Clients.API.Application.Commands;

public class ClientCommandHandler : CommandHandler, IRequestHandler<AddClientCommand, ValidationResult>
{
    private readonly IClientRepository _clientRepository;

    public ClientCommandHandler(IClientRepository clientRepository) => _clientRepository = clientRepository;

    public async Task<ValidationResult> Handle(AddClientCommand message, CancellationToken cancellationToken)
    {
        if (!message.IsValid()) return message.ValidationResult;

        var client = new Client(message.Id, message.Name, message.Email, message.CPF);

        var existingClient = await _clientRepository.GetByCPFASync(client.Cpf.Number);        
        if (existingClient is not null) 
        {
            AddError("This CPF has been already choosen");
            return ValidationResult;
        }

        await _clientRepository.AddAsync(client);
        return await PersistData(_clientRepository.UnitOfWork);
    }
}
