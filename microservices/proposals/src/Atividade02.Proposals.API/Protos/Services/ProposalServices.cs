using Atividade02.Proposals.Domain.Proposals.Repositories;
using Atividade04.BFF.Protos.Services;
using Grpc.Core;
using static Atividade04.BFF.Protos.Services.ProposalsService;
using Google.Protobuf.WellKnownTypes;

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
                    AggregateId = proposal.AggregateId,
                    CreatedAt = proposal.CreatedAt.ToString(),
                    UpdatedAt = proposal.UpdatedAt.HasValue ? proposal.UpdatedAt.Value.ToString() : string.Empty,
                    Code = proposal.Code,
                    Fullname = proposal.Proponent.Name.Value,
                    Cpf = proposal.Proponent.CPF.Number,
                    Cellphone = proposal.Proponent.Cellphone.ToString(),
                    Status = proposal.Status.ToString(),
                    CreaditLimit = new Random().Next(100, 1001).ToString()
                });
            }


            return response;

        }
    }
}