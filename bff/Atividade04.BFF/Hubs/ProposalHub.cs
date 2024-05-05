using System;
using Atividade02.Core.MessageBus.Services.Interfaces;
using Atividade04.BFF.DTOs.Requests;
using Atividade04.BFF.ExternalServices.Proposals.Responses;
using Atividade04.BFF.Models.Events;
using Atividade04.BFF.Protos.Services;
using Grpc.Net.Client;
using Microsoft.AspNetCore.SignalR;

namespace Atividade04.BFF.Hubs
{
    public class ProposalHub : Hub
    {
        private readonly IMessageBus _messageBus;
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

        public async Task Streaming(string request)
        {
            _logger.LogInformation("Starting straming...");
            await base.OnConnectedAsync();

            var channel = GrpcChannel.ForAddress("https://localhost:7001");

            var client = new ProposalsService.ProposalsServiceClient(channel);

            var query = new ProposalsConsultaQuery()
            {
                CnpjLoja = request
            };

            var respostaProposal = await client.ConsultarProposalsAsync(query);

            var responseView = new GetProposalListResponse();

            foreach (var proposal in respostaProposal.Proposals)
            {
                responseView.Proposals.Add(new GetProposalListItemResponse
                {
                    AggregateId = proposal.AggregateId,
                    CreatedAt = proposal.CreatedAt,
                    UpdatedAt = proposal.UpdatedAt,
                    Code = proposal.Code,
                    Name = proposal.Fullname,
                    CPF = proposal.Cpf,
                    Cellphone = proposal.Cellphone,
                    Status = proposal.Status,
                    CreditLimit = proposal.CreaditLimit
                });
            }

            await Clients.Caller.SendAsync("ReceberPropostas", responseView);


            _messageBus.Subscribe<ProposalStatusChangedEvent>("proposals", "proposals-status-changed", async (proposal) =>
            {
                responseView.Proposals.Add(new GetProposalListItemResponse
                {
                    AggregateId = proposal.AggregateId,
                    CreatedAt = proposal.CreatedAt.ToString(),
                    UpdatedAt = proposal?.UpdatedAt?.ToString(),
                    Code = proposal!.Code,
                    Name = proposal!.Name,
                    CPF = proposal!.CPF,
                    Cellphone = proposal!.Cellphone,
                    Status = proposal!.Status,
                    CreditLimit = proposal.CreditLimit
                });

                await Clients.Caller.SendAsync("ReceberPropostas", responseView);

            }, default);

            await Task.Delay(-1);
        }

    }
}

