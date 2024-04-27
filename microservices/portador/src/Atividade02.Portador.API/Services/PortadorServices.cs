using System;
using Atividade02.Proponent.API.Events;
using Atividade02.Proponent.API.Models.Interfaces;
using Atividade02.Proponent.API.Models.Interfaces.Services;

namespace Atividade02.Proponent.API.Services
{
    public class PortadorServices : IPortadorServices
    {
        private readonly ILogger<PortadorServices> _logger;
        private readonly IPortadorRepository _proponentRepository;
       
        public PortadorServices(ILogger<PortadorServices> logger, IPortadorRepository proponentRepository)
        {
            _logger = logger;
            _proponentRepository = proponentRepository;
        }

        public async Task Create(ProposalApprovedEvent request)
        {
            _logger.LogInformation("Init create portador...");

            var proponente = new Models.Portador(Guid.NewGuid().ToString(), request.Proponent.Name, request.Proponent.CPF, request.Proponent.DDD, request.Proponent.CellphoneNumber);

            _proponentRepository.Add(proponente);

            await _proponentRepository.unitOfWork.Commit();

            _logger.LogInformation($"Portador {proponente.CPF} created.");
        }
    }
}

