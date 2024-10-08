﻿using Library.Model.Exceptions;
using Library.Model.Model;
using Library.Model.Validators;
using Library.Data.Repositories.Interfaces;
using Library.Data.Services.Interfaces;

namespace Library.Data.Services
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _clientRepository;

        public ClientService(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public async Task<Client> GetClientByIdAsync(int idClient)
        {
            var client = await _clientRepository.GetClientByIdAsync(idClient);
            
            if(client is null)
            {
                throw new BusinessException($"Not found loan info for this idLoan {idClient}.");
            }

            return client;
        }

        public async Task<IEnumerable<Client>> GetClientsAsync()
        {
            var clients = await _clientRepository.GetClientsAsync();

            if (clients.Count() < 1)
            {
                throw new BusinessException($"No clients found.");
            }

            return clients;
        }

        public async Task<int> InsertNewClientAsync(Client client)
        {
            var validator = new ClientValidator();
            var result = validator.Validate(client);

            if (!result.IsValid)
            {
                var propertiesError = validator.ReturnPropertiesWithError(result.Errors);

                throw new BusinessException($"The request is invalid. {propertiesError}");
            }

            return await _clientRepository.InsertNewClientAsync(client);
        }
    }
}
