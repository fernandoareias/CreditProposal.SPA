using System;
using Atividade04.BFF.ExternalServices.Proposals.Responses;
using Microsoft.AspNetCore.SignalR;

namespace Atividade04.BFF.Hubs
{
    public class ProposalHub : Hub
    {
        public async IAsyncEnumerable<ProposalsListResponse> Streaming(CancellationToken cancellationToken)
        {

            yield return new ProposalsListResponse();
            await Task.Delay(1000, cancellationToken);
        }
    }
}

