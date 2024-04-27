using System;
using Atividade02.Core.Mediator.Interfaces;
using Atividade02.Core.MessageBus.Services.Interfaces;
using Atividade02.Proposals.Domain.Proposals.Entities.Policies.Events;

namespace Atividade02.Proposals.API.BackgroundServices
{
    public class FraudAnalysisPolicySuccessfullyExecutedEventWorker : BackgroundService
    {
        public FraudAnalysisPolicySuccessfullyExecutedEventWorker(ILogger<FraudAnalysisPolicySuccessfullyExecutedEventWorker> logger, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;

        }

        private readonly ILogger<FraudAnalysisPolicySuccessfullyExecutedEventWorker> _logger;
        private readonly IServiceProvider _serviceProvider;

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("[WORKER][PRE-ANALYSIS-SUCCESSFULLY] - Creating process...");
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("[WORKER][PRE-ANALYSIS-SUCCESSFULLY] - Starting process...");
                using (var scope = _serviceProvider.CreateScope())
                {
                    var messageBus = scope.ServiceProvider.GetRequiredService<IMessageBus>();

                    messageBus.Subscribe<FraudAnalysisPolicySuccessfullyExecutedEvent>("proposals", "policy-fraud-analysis-executed-successfull", Process,
                        stoppingToken);

                    await Task.Delay(-1, stoppingToken);
                }
            }
        }


        private async Task Process(FraudAnalysisPolicySuccessfullyExecutedEvent request)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var mediatorHandler = scope.ServiceProvider.GetRequiredService<IMediatorHandler>();
                await mediatorHandler.Publish(request);
            }
        }
    }
}

