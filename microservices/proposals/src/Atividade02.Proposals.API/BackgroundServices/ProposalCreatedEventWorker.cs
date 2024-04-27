using System;
using System.Diagnostics;
using Atividade02.Core.Mediator.Interfaces;
using Atividade02.Core.MessageBus.Services.Interfaces;
using Atividade02.Proposals.Domain.Proposals.Events;

namespace Atividade02.Proposals.API.BackgroundServices
{
    public class ProposalCreatedEventWorker : BackgroundService
    {
        public ProposalCreatedEventWorker(ILogger<ProposalCreatedEventWorker> logger, IServiceProvider serviceProvider) 
        {
            _logger = logger;
            _serviceProvider = serviceProvider;

        }

        private readonly ILogger<ProposalCreatedEventWorker> _logger;
        private readonly IServiceProvider _serviceProvider;

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("[WORKER][PROPOSAL-CREATED] - Creating process...");
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("[WORKER][PROPOSAL-CREATED] - Starting process...");
                using (var scope = _serviceProvider.CreateScope())
                {
                    var messageBus = scope.ServiceProvider.GetRequiredService<IMessageBus>();

                    messageBus.Subscribe<ProposalSentEvent>("proposals", "proposal-create", Process,
                        stoppingToken);

                    await Task.Delay(-1, stoppingToken);
                }
            }
        }


        private async Task Process(ProposalSentEvent request)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var mediatorHandler = scope.ServiceProvider.GetRequiredService<IMediatorHandler>();
                await mediatorHandler.Publish(request);
            }
        }
    }
}

