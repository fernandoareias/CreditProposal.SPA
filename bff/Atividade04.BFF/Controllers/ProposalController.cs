using System;
using Atividade04.BFF.Hubs;
using Microsoft.AspNetCore.SignalR;
namespace Atividade04.BFF.Controllers
{
    public class ProposalController
    {
        private readonly IHubContext<ProposalHub> _streaming;
      
        public ProposalController(IHubContext<ProposalHub> streaming)
        {
            _streaming = streaming;
        }
    }
}

