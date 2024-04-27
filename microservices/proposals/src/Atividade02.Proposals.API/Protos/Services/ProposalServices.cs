using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Atividade02.Proposals.Domain.Proposals.Repositories;
using Atividade04.Proposals.API.Protos;
using Atividade04.Proposals.API.Protos.Services;
using Grpc.Core;
using static Atividade04.Proposals.API.Protos.Services.ProposalsService;

namespace Atividade02.Proposals.API.Protos.Services
{

    public class ProposalServices : ProposalsServiceBase
    {
        private readonly IProposalRepository _proposalRepository;

        public ProposalServices(IProposalRepository proposalRepository)
        {
            _proposalRepository = proposalRepository;
        }

        public override Task<ProposalsQueryResponse> ConsultarProposals(ProposalsConsultaQuery request, ServerCallContext context)
        {

            return null;
        }
    }
}