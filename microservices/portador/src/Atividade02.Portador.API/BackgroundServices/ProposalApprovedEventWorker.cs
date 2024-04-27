using System;
using Atividade02.Core.Mediator.Interfaces;
using Atividade02.Core.MessageBus.Services.Interfaces;
using Atividade02.Proponent.API.Events;
using Atividade02.Proponent.API.Models.Interfaces.Services;

namespace Atividade02.Proponent.API.BackgroundServices
{
    public class ProposalApprovedEventWorker : BackgroundService
    {
        public ProposalApprovedEventWorker(ILogger<ProposalApprovedEventWorker> logger, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;

        }

        private readonly ILogger<ProposalApprovedEventWorker> _logger;
        private readonly IServiceProvider _serviceProvider;

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("[WORKER][PROPOSAL-APPROVED] - Creating process...");
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("[WORKER][PROPOSAL-APPROVED] - Starting process...");
                using (var scope = _serviceProvider.CreateScope())
                {
                    var messageBus = scope.ServiceProvider.GetRequiredService<IMessageBus>();

                    messageBus.Subscribe<ProposalApprovedEvent>("proposals", "proposals-approved", Process,
                        stoppingToken);

                    await Task.Delay(-1, stoppingToken);
                }
            }
        }


        private async Task Process(ProposalApprovedEvent request)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var services = scope.ServiceProvider.GetRequiredService<IPortadorServices>();
                await services.Create(request);
            }
        }
    }
}

