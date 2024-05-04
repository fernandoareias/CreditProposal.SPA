using Atividade02.Proposals.Domain.Proposals.Repositories;
using Atividade04.BFF.Protos.Services;
using Grpc.Core;
using static Atividade04.BFF.Protos.Services.ProposalsService;

namespace Atividade02.Proposals.API.Protos.Services
{

    public class ProposalServices : ProposalsServiceBase
    {
        private readonly IProposalRepository _proposalRepository;

        public ProposalServices(IProposalRepository proposalRepository)
        {
            _proposalRepository = proposalRepository;
        }

        public override async Task<ProposalsQueryResponse> ConsultarProposals(ProposalsConsultaQuery request, ServerCallContext context)
        {

            var proposals = await _proposalRepository.GetLasts(request.CnpjLoja);
            var response = new ProposalsQueryResponse();

            foreach (var proposal in proposals)
            {
                response.Proposals.Add(new ProposalsResponse
                {
                    Id = proposal.AggregateId,
                    Cpf = proposal.Proponent.CPF.Number,
                    Nome = proposal.Proponent.Name.Value,
                    Product = 1,
                    Status = (int)proposal.Status
                });
            }


            return response;

        }
    }
}