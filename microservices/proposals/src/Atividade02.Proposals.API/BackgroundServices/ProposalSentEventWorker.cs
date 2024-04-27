using System;
using Atividade02.Core.Mediator.Interfaces;
using Atividade02.Core.MessageBus.Services.Interfaces;
using Atividade02.Proposals.Application.Proposals.Commands;
using Atividade02.Proposals.Application.Proposals.Events;
using Atividade02.Proposals.Domain.Proposals.Events;

namespace Atividade02.Proposals.API.BackgroundServices
{
    public class ProposalSentEventWorker : BackgroundService
    {
        public ProposalSentEventWorker(ILogger<ProposalSentEventWorker> logger, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;

        }

        private readonly ILogger<ProposalSentEventWorker> _logger;
        private readonly IServiceProvider _serviceProvider;

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("[WORKER][PROPOSAL-SENT] - Creating process...");
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("[WORKER][PROPOSAL-SENT] - Starting process...");
                using (var scope = _serviceProvider.CreateScope())
                {
                    var messageBus = scope.ServiceProvider.GetRequiredService<IMessageBus>();

                    messageBus.Subscribe<Application.Proposals.Events.ProposalSentEvent>("proposals", "proposal-sent", Process,
                        stoppingToken);

                    await Task.Delay(-1, stoppingToken);
                }
            }
        }


        private async Task Process(Application.Proposals.Events.ProposalSentEvent request)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var mediatorHandler = scope.ServiceProvider.GetRequiredService<IMediatorHandler>();
                await mediatorHandler.Publish(request);
            }
        }
    }
}

