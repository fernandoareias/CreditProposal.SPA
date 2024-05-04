using System;
using Atividade02.Core.MessageBus.Services.Interfaces;
using Atividade04.BFF.ExternalServices.Proposals.Responses;
using Atividade04.BFF.Protos.Services;
using Grpc.Net.Client;
using Microsoft.AspNetCore.SignalR;

namespace Atividade04.BFF.Hubs
{
    public class ProposalHub : Hub
    {
        private readonly IMessageBus _messageBus;
        private bool _firstTime = false;
        private readonly ILogger<ProposalHub> _logger;

        public ProposalHub(IMessageBus messageBus, ILogger<ProposalHub> logger)
        {
            _messageBus = messageBus;
            _logger = logger;

            _logger.LogInformation("Creating hub...");
        }

        public async override Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();
            _logger.LogInformation("Connected at hub...");
            await Clients.Caller.SendAsync("Message", "Connected successfully!");
        }

        //IAsyncEnumerable<ProposalsListResponse>
        public async Task Streaming(string request)
        {
            _logger.LogInformation("Starting straming...");
            await base.OnConnectedAsync();

            if (!_firstTime)
            {
                var channel = GrpcChannel.ForAddress("https://localhost:7001");

                var client = new ProposalsService.ProposalsServiceClient(channel);

                var query = new ProposalsConsultaQuery();

                var resposta = await client.ConsultarProposalsAsync(query);

                await Clients.Caller.SendAsync("ReceberPropostas", null);

                _firstTime = true;
            }

            _messageBus.Subscribe<int>("proposals", "", async (x) =>
            {

            }, default);

            await Task.Delay(-1);
        }

    }
}

