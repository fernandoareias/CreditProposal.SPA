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
        public ProposalHub(IMessageBus messageBus)
        {
            _messageBus = messageBus;
        }
        //IAsyncEnumerable<ProposalsListResponse>
        public async Task Streaming(ProposalsConsultaQuery request, CancellationToken cancellationToken)
        {
            await base.OnConnectedAsync();

            if (!_firstTime)
            {
                var channel = GrpcChannel.ForAddress("https://localhost:7044");

                var client = new ProposalsService.ProposalsServiceClient(channel);

                var query = new ProposalsConsultaQuery();

                var resposta = await client.ConsultarProposalsAsync(query);

                await Clients.Caller.SendAsync("ReceberPropostas", resposta.Proposals);

                _firstTime = true;
            }

            _messageBus.Subscribe<int>("proposals", "", async (x) =>
            {
                await Clients.All.SendAsync("ReceberPropostas", "message");
            }, cancellationToken);

            await Task.Delay(1000);
        }
    }
}

